using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsGroupMapping
    {
        DBUtils db = new DBUtils();

        #region Transporter Group Mapping
        public DataTable BindTransporterGroupGrid()
        {
            string sql = " SELECT GROUPID,GROUPNAME,GROUPCODE FROM M_TRANSPORTER_GROUP_HEADER ORDER BY GROUPNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet EditGroupMapping(string ID)
        {
            string sql = "EXEC [USP_TRANSPORTER_GROUP_EDIT] '" + ID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }


        public int SaveTransporterGroup(string ID, string NAME, string CODE, string SESSIONID, string XML)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "USP_TRANSPORTER_GROUP_INSERT '" + ID + "','" + NAME + "','" + CODE + "','" + SESSIONID + "','" + XML + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public int DeleteTransporterGroup(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "EXEC [USP_TRANSPORTER_GROUP_DELETE] '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        #endregion

        #region  Vendor Group Mapping

        public DataTable BindVendorGroupGrid()
        {
            string sql = " SELECT GROUPID,GROUPNAME,GROUPCODE FROM M_VENDOR_GROUP_HEADER ORDER BY GROUPNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindVendor()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR where TAG <> 'F' ORDER BY VENDORNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }



        public int SaveVendorGroup(string ID, string CODE, string NAME, string SESSIONID, string XML)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = " EXEC USP_VENDOR_GROUP_INSERT '" + ID + "','" + NAME + "','" + CODE + "','" + SESSIONID + "','" + XML + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }


        public DataSet VendorGroupEdit(string GROUPID)
        {
            string sql = " EXEC USP_VENDOR_GROUP_EDIT '" + GROUPID + "'";

            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public int DeleteVendorGroup(string GROUPID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = " EXEC USP_VENDOR_GROUP_DELETE '" + GROUPID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        #endregion
    }
}
