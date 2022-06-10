using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsBankMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindBankMasterGrid()
        {
            string sql = "select ID,BANKNAME,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',ADDRESS,IFSCCODE,SWIFTCODE,BRANCH,ACOUNTNO,EXPORTTAG from M_BANKMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBankMasterGridEdit(string id)
        {
            string sql = "select ID,BANKNAME,ACTIVE,ADDRESS,IFSCCODE,SWIFTCODE,BRANCH,ACOUNTNO,ISNULL(EXPORTTAG,'N') AS EXPORTTAG,ISFORTARGET from M_BANKMASTER " +
                          " WHERE ID='"+id+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public int SaveBankMaster(string ID, string Name, string Mode, string active, string ADDRESS, string IFSCCODE, string SWIFTCODE,
                                  string BRANCH, string ACOUNTNO, string EXPORTTAG, string ISFORTARGET, string BANKCODE,string accgpid,string accgpname,string bankid)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BANKMASTER where BANKNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 6;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BANKMASTER where BANKNAME='" + Name + "' and  ID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 6;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {

                   

                        sqlstr = "EXEC [USP_BANK_MASTER_INSERT_UPDATE] '" + ID + "','" + Name + "','"+ Mode + "','" + active + "','" + ADDRESS + "','" + IFSCCODE + "','"+ SWIFTCODE + "','"+ BRANCH + "'," +
                                 "'"+ ACOUNTNO + "','"+ EXPORTTAG + "','"+ ISFORTARGET + "','"+ BANKCODE + "','"+ accgpid + "','"+ accgpname + "','"+ bankid + "'" ;
                        result = db.HandleData(sqlstr);

                }

                else
                {
                    //result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public string BankDeleteCheck(string bankid)
        {
            string sql = string.Empty;
            string Status = string.Empty;
            try
            {
                sql = "EXEC [USP_CHECK_BANK_TRANC] '" + bankid + "'";
                Status = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return Status;
        }

        public int DeleteBankMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "EXEC [USP_BANK_DETAILS_DELETE]'" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindDepot()
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='DEPOT'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCycle(string FYEARID)
        {
            string sql = "exec [SP_GET_TADA_CYCLE] @P_FYEARID='" + FYEARID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindThirdPartyName()
        {
            string sql = "exec [SP_GET_TADA_ThirdPartyName]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindASM(string DEPOTID)
        {
            string sql = "exec [SP_GET_TADA_ASM] @P_DEPOTID='" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTADAGRID(string THIRDPARTYID,string CYCLEID,string DEPOTID,string ASMID)
        {
            string sql = "exec [SP_GET_TADA_TSI_DETAILS] @P_THIRDPARTYID='" + THIRDPARTYID + "',@P_CYCLEID='" + CYCLEID + "',@P_DEPOTID='" + DEPOTID + "',@P_ASMID='" + ASMID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveTADAMaster(string TSIXML, string CREATEDBY,string P_ACCENTRYID,string J_ACCENTRYID)
        {
            int result = 0;        
            try
            {  

                string sql = "EXEC [SP_INSERT_TADA] @P_TSIDETAILS='" + TSIXML + "',@P_CREATEDBY='" + CREATEDBY + "',@P_P_ACCENTRYID='" + P_ACCENTRYID + "',@P_J_ACCENTRYID='" + J_ACCENTRYID + "'";
                result = (int)db.GetSingleValue(sql);

                return result;
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindTADAAPPROVEDGRID()
        {
            string sql = "exec [SP_GET_TADA_TSI_PREAPPROVED]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTADAVIEW(string ACCENTRYID)
        {
            string sql = "exec [SP_GET_TADA_TSI_VIEW] @P_ACCENTRYID='" + ACCENTRYID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable TADAApproved(string ACCENTRYID,string userid)
        {
            string sql = "exec [SP_APPROVED_TADA] @P_ACCENTRYID='" + ACCENTRYID + "',@P_USERID = '" + userid  + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int TADAApprovedVoucher(string ACCENTRYID, string userid,string STATUS)
        { 
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "exec [SP_APPROVED_TADA_VOUCHER] @P_ACCENTRYID='" + ACCENTRYID + "',@P_USERID = '" + userid + "',@P_STATUS='" + STATUS + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public DataTable GetAccountGroupID()
        {
            string sql = "SELECT CODE,GRPNAME FROM ACC_ACCOUNTGROUP WHERE CODE='3169F389-82E9-4B9C-8FD9-7C9B716C757F'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


    }
}
