using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsBusinessSegmentMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindBusnessSegmentMasterGrid()
        {
            string sql = "select BSID, BSCODE, BSNAME, BSDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  " +
                         " case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE' ,PREDEFINED from M_BUSINESSSEGMENT ORDER BY BSNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveBusnessSegmentMaster(string ID, string Code, string Name, string Description, string Mode, string active)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_BUSINESSSEGMENT where BSCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BUSINESSSEGMENT where BSNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 4;
                            return result;
                        }

                    }

                }
                else
                {

                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_BUSINESSSEGMENT where BSCODE='" + Code + "' and  BSID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BUSINESSSEGMENT where BSNAME='" + Name + "' and  BSID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 4;
                            return result;
                        }

                    }


                }


                if (dt.Rows.Count == 0)
                {

                  string USERID = HttpContext.Current.Session["UserID"].ToString();

                  sqlstr = "EXEC[USP_INSERT_UPDATE_BS] '"+ID+"','"+Code+"','"+Name+"','"+Description+"','"+USERID+"','"+Mode+"','"+active+"'";
                  result = db.HandleData(sqlstr);
                   
                }

                else
                {
                    result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int DeleteBusnessSegmentMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from M_BUSINESSSEGMENT where BSID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public DataSet Bind_Bswise_closing(string DateTo, string Depot, string Bsid, string Month, string Finyear)
        {
            string sql = "EXEC [usp_businesssegmentwise_closing_set] '" + DateTo + "','" + Depot + "','" + Bsid + "','" + Month + "','" + Finyear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
    }
}
