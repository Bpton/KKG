using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
namespace FactoryDatacontext
{
   public class DButility
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings["Factconn"].ToString();
        public SqlConnection cvCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ToString());
      //  public SqlConnection cvCon1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constringrpt2"].ToString());
        private SqlCommand lvCom;
        public SqlDataReader lvRed;
        public String VarHoldOption;
        private SqlCommand cmd;
        public SqlDataReader rdr;
        public DataSet ds = new DataSet();
        public int i;
        private SqlDataAdapter da;
        public static bool CheckLogin(string userName, string password, int comid, int brid, string type)
        {
            SqlDataReader dr = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand("SPCheckLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Param1 = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
            SqlParameter Param2 = new SqlParameter("@Password", SqlDbType.VarChar, 50);
            SqlParameter Param3 = new SqlParameter("@comid", SqlDbType.BigInt);
            SqlParameter Param4 = new SqlParameter("@Brid", SqlDbType.BigInt);
            SqlParameter Param5 = new SqlParameter("@UType", SqlDbType.VarChar, 50);

            Param1.Value = userName;
            Param2.Value = password;
            Param3.Value = comid;
            Param4.Value = brid;
            Param5.Value = type;
            cmd.Parameters.Add(Param1);
            cmd.Parameters.Add(Param2);
            cmd.Parameters.Add(Param3);
            cmd.Parameters.Add(Param4);
            cmd.Parameters.Add(Param5);
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    con.Close();
                    return true;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception)
            { }
            return false;
        }
        public SqlDataReader SysFetchData(string spName, ref Hashtable ParameterName)
        {
            SqlDataReader dr = null;
            IDictionaryEnumerator lvEnum = ParameterName.GetEnumerator();
            lvCom = new SqlCommand(spName, cvCon);
            lvCom.CommandTimeout = 999999999;
            if (cvCon.State == 0)
            {
                cvCon.Open();
            }
            lvCom.CommandType = CommandType.StoredProcedure;
            while (lvEnum.MoveNext())
            {
                lvCom.Parameters.AddWithValue(lvEnum.Key.ToString(), lvEnum.Value);
            }

            dr = lvCom.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }
        public SqlDataAdapter SysFetchDataInDataAdapter(string spName, ref Hashtable ParameterName)
        {
            if (ds.Tables.Count > 0)
                ds.Tables[0].Clear();
            IDictionaryEnumerator iEnum = ParameterName.GetEnumerator();
            cmd = new SqlCommand(spName, cvCon);
            cmd.CommandTimeout = 999999999;
            if (cvCon.State == ConnectionState.Closed)
                cvCon.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            while (iEnum.MoveNext())
            {
                cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value.ToString());
            }
            da = new SqlDataAdapter(cmd);
            cvCon.Close();
            return da;
        }
        public void SysAddUpdateDelete(string spName, ref Hashtable ParameterName)
        {

            IDictionaryEnumerator lvEnum = ParameterName.GetEnumerator();
            lvCom = new SqlCommand(spName, cvCon);
            lvCom.CommandTimeout = 999999999;
            if (cvCon.State == 0)
            {
                cvCon.Open();
            }
            lvCom.CommandType = CommandType.StoredProcedure;
            while (lvEnum.MoveNext())
            {
                lvCom.Parameters.AddWithValue(lvEnum.Key.ToString(), lvEnum.Value);
            }
            lvRed = lvCom.ExecuteReader();
            lvRed.Close();
        }
        //for inline query
        public DataSet GetDataSet(string query)
        {
            if (ds.Tables.Count > 0)
                ds.Tables[0].Clear();
            //DataSet dataset = new DataSet();
            cmd = new SqlCommand(query, cvCon);
            da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            catch (Exception)
            {
            }
            cvCon.Close();
            return ds;
        }
        //to return datatable from sp
        public DataTable getDataSet(string proc,Hashtable ht)
        {
            if (ds.Tables.Count > 0)
                ds.Tables[0].Clear();
            ds = new DataSet();
            IDictionaryEnumerator iEnum = ht.GetEnumerator();
            cmd = new SqlCommand(proc, cvCon);
            cmd.CommandTimeout = 999999999;
            if (cvCon.State == ConnectionState.Closed)
                cvCon.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            while (iEnum.MoveNext())
            {
                cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value.ToString());
            }

            da = new SqlDataAdapter(cmd);
          
            da.Fill(ds);
            if (cvCon.State == ConnectionState.Open)
                cvCon.Close();
            return ds.Tables[0];
        }
        // for datasets from proc
        public DataSet SysFetchDataInDataSet(string spName, Hashtable htParam)
        {
            try
            {
               // DataSet ds = new DataSet();
                int to = cvCon.ConnectionTimeout;
                // htParam.Add("Company_ID", "1");
                IDictionaryEnumerator iEnum = htParam.GetEnumerator();
                cmd = new SqlCommand(spName, cvCon);
                cmd.CommandTimeout = 999999999;
                if (cvCon.State == ConnectionState.Closed)
                    cvCon.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                while (iEnum.MoveNext())
                {
                    cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value);
                }
                da = new SqlDataAdapter(cmd);

                da.Fill(ds);
                cvCon.Close();
            }
            catch(Exception ex)
            {

            }
            return ds;
        }
        //public DataSet SysFetchDataInDataSetupload(string spName, Hashtable htParam)
        //{
        //    int to = cvCon.ConnectionTimeout;
        //    // htParam.Add("Company_ID", "1");
        //    IDictionaryEnumerator iEnum = htParam.GetEnumerator();
        //    cmd = new SqlCommand(spName, cvCon1);
        //    cmd.CommandTimeout = 999999999;
        //    if (cvCon1.State == ConnectionState.Closed)
        //        cvCon1.Open();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    while (iEnum.MoveNext())
        //    {
        //        cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value);
        //    }
        //    da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    cvCon1.Close();
        //    return ds;
        //}
        public int SysFetchDataInDataSetNew(string spName, Hashtable htParam)
        {
            int to = cvCon.ConnectionTimeout;

            //ds = new DataSet();
            //if (ds.Tables.Count > 0)
            //    ds.Tables[0].Clear();
            IDictionaryEnumerator iEnum = htParam.GetEnumerator();
            cmd = new SqlCommand(spName, cvCon);
            cmd.CommandTimeout = 999999999;
            if (cvCon.State == ConnectionState.Closed)
                cvCon.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            while (iEnum.MoveNext())
            {
                //  cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value.ToString());
                cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value);
            }

            cvCon.Close();
            return i;

        }
        public SqlDataReader RunQuery(string query)
        {
            SqlDataReader dr = null;
            SqlConnection cvCon = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand(query, cvCon);
            cmd.CommandType = CommandType.Text;

            try
            {
                if (cvCon.State == ConnectionState.Closed)
                    cvCon.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception)
            { }

            return dr;
        }
        public SqlDataReader RunSqlQuery1(string query)
        {
            SqlDataReader dr = null;
            SqlConnection cvCon = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(query, cvCon);
            cmd.CommandType = CommandType.Text;

            try
            {
                if (cvCon.State == ConnectionState.Closed)
                    cvCon.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception)
            { }

            return dr;
        }
        public static SqlDataReader RunSqlQuery(string query)
        {
            string conStringx = System.Configuration.ConfigurationManager.ConnectionStrings["SchoolSoft"].ToString();
            SqlDataReader dr = null;
            SqlConnection cvCon = new SqlConnection(conStringx);
            SqlCommand cmd = new SqlCommand(query, cvCon);
            cmd.CommandType = CommandType.Text;

            try
            {
                if (cvCon.State == ConnectionState.Closed)
                    cvCon.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception)
            { }

            return dr;
        }

        public DataTable LoadAllPageNameforSetPermission(Int64 UserID)
        {

            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UserPermissionforPageName"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            //cmd.Parameters.AddWithValue("@LastName", txtlastname);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }
                    }
                }
                catch (Exception Ex)
                {

                }
            }
            return dt;
        }
        public DataTable loadAllProjectByLead(string UserID)
        {

            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectMasterByUserID"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            //cmd.Parameters.AddWithValue("@LastName", txtlastname);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }
                    }
                }
                catch (Exception Ex)
                {

                }
            }
            return dt;
        }
        //public static bool UserAccess(string UID, long MID, string Mode)
        //{
        //    SqlDataReader dr = RunSqlQuery("Select AddPriviledge,EditPriviledge,DeletePriviledge,ViewPriviledge,PrintPriviledge From TblUserPriviledge Where UserID='" + UID + "' And MenuID=" + MID + "");
        //    if (dr.Read())
        //    {
        //        if (Mode == "Add")
        //        {
        //            AddPrivi = dr["AddPriviledge"].ToString();
        //            if (AddPrivi == "False")
        //            {
        //               // //System.Windows.Forms.MessageBox.Show("Permission denied.You cann't perform Insert operation..");
        //                return false;
        //            }
        //        }
        //        else if (Mode == "Edit")
        //        {
        //            EditPrivi = dr["EditPriviledge"].ToString();
        //            if (EditPrivi == "False")
        //            {
        //               // //System.Windows.Forms.MessageBox.Show("Permission denied.You cann't perform Edit/Update operation..");
        //                return false;
        //            }
        //        }
        //        else if (Mode == "Delete")
        //        {
        //            DelPrivi = dr["DeletePriviledge"].ToString();
        //            if (DelPrivi == "False")
        //            {
        //               // //System.Windows.Forms.MessageBox.Show("Permission denied.You cann't perform Delete operation..");
        //                return false;
        //            }
        //        }
        //        else if (Mode == "View")
        //        {
        //            DelPrivi = dr["ViewPriviledge"].ToString();
        //            if (DelPrivi == "False")
        //            {
        //               // //System.Windows.Forms.MessageBox.Show("Permission denied.You cann't view this page..");
        //                return false;
        //            }
        //        }
        //        else if (Mode == "Print")
        //        {
        //            PrintPrivi = dr["PrintPriviledge"].ToString();
        //            if (PrintPrivi == "False")
        //            {
        //               // //System.Windows.Forms.MessageBox.Show("Permission denied.You cann't perform Print operation..");
        //                return false;
        //            }
        //        }

        //    }
        //    dr.Close();
        //    return true;

        //}
        //public DataTable GetDataSet()
        //{
        //    throw new NotImplementedException();
        //}


        public DataTable LoadAllPageNameforSetPermission(Int64 UserID, string cmp_id)
        {

            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UserPermissionforPageName"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@Company_Id", cmp_id);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }
                    }
                }
                catch (Exception Ex)
                {

                }
            }
            return dt;
        }
    }
}
