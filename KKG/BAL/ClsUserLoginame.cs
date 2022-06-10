using DAL;
using System;
using System.Data;
using Utility;

namespace BAL
{
    public class ClsUserLoginame
    {
        DBUtils db = new DBUtils();
        Helper hlp = new Helper();
        public int UserLogin(string UserName, string Password)
        {
            int iSuccess = 0;
            try
            {
                //string sql = "SELECT M_USER.USERNAME FROM M_USER INNER JOIN M_USERTYPE ON M_USER.USERTYPE=M_USERTYPE.UTID	WHERE USERNAME='" + UserName + "'";
                string sql = "Select Name From Vw_User WHERE USER_NAME='" + UserName + "'";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                if (dt.Rows.Count != 0)
                {
                    // string sqlpwd = "SELECT M_USER.[PASSWORD]  FROM M_USER INNER JOIN M_USERTYPE ON M_USER.USERTYPE=M_USERTYPE.UTID	WHERE USERNAME='" + UserName + "'";
                    string sqlpwd = "Select PWD From Vw_User WHERE USER_NAME='" + UserName + "'";
                    DataTable dtpwd = new DataTable();
                    dtpwd = db.GetData(sqlpwd);

                    if (dt.Rows.Count != 0)
                    {
                        // if (Password.ToString().Trim() == dtpwd.Rows[0][0].ToString().Trim())
                        if (Password == hlp.base64Decode(Convert.ToString(dtpwd.Rows[0]["PWD"])))
                        {
                            iSuccess = 2;
                        }
                        else
                        {
                            iSuccess = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return iSuccess;
        }

        public DataTable UserLoginINFO(string UserName, string Password)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM VW_USERLOGININFO WHERE USERNAME='" + UserName + "' AND PASSWORD=dbo.fnMIME64Encode(1,'" + Password + "')";
            dt = db.GetData(sql);
            if (dt.Rows.Count != 0)
            {
                int result = 0;
                string sqlinsert = string.Empty;
                DataTable dt1 = new DataTable();
                string sqluseridcheack = string.Empty;
                string sqluserid = "SELECT USERID FROM M_USER WHERE USERNAME='" + UserName + "'";

                int userid = 0;
                userid = (int)db.GetSingleValue(sqluserid);

                string sqlmaxdate = string.Empty;
                string maxdate = string.Empty;

                string sqltodate = "SELECT CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103) AS TODATE ";
                string todate = (string)db.GetSingleValue(sqltodate);

                sqlmaxdate = "SELECT MAX(CONVERT(VARCHAR(10),CAST(LASTLOGOUTTIME AS DATE),103)) AS LASTLOGOUTTIME FROM  T_USER_LOG";

                DataTable dt2 = db.GetData(sqlmaxdate);
                maxdate = dt2.Rows[0][0].ToString();
                if (maxdate == "")
                {
                    maxdate = todate;
                }
                else
                {
                    maxdate = (string)db.GetSingleValue(sqlmaxdate);
                }
                sqluseridcheack = "SELECT USERID FROM T_USER_LOG WHERE USERID='" + userid + "'";
                dt1 = db.GetData(sqluseridcheack);

                if (dt1.Rows.Count == 0)
                {
                    sqlinsert = "INSERT INTO T_USER_LOG (USERID,LASTLOGINTIME) VALUES('" + userid + "',GETDATE())";
                    result = db.HandleData(sqlinsert);
                }
                else
                {
                    if (maxdate == todate)
                    {
                        sqlinsert = "UPDATE T_USER_LOG  set USERID='" + userid + "',LASTLOGINTIME=GETDATE() where USERID='" + userid + "' ";
                        result = db.HandleData(sqlinsert);
                    }
                    else
                    {
                        sqlinsert = "INSERT INTO T_USER_LOG (USERID,LASTLOGINTIME) VALUES('" + userid + "',GETDATE())";
                        result = db.HandleData(sqlinsert);
                    }
                }
            }
            return dt;
        }

        public DataTable BindFinYear()
        {
            //string sql = "Select  '-1' AS id , '-- SELECT YEAR --' as FinYear union all SELECT [id] ,[FinYear] FROM [M_FINYEAR] WHERE ISClosed='N' ORDER BY FinYear";
            string sql = "SELECT [id] ,[FinYear] FROM [M_FINYEAR] WHERE ISClosed='N' ORDER BY FinYear desc";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBranch()
        {
            string sql = "SELECT '-1' AS  BRID,'-- SELECT BRANCH --' AS BRNAME,'0' AS SEQUENCE UNION SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O' UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotExceptAdmin(string UserID)
        {
            string sql = " SELECT BRID,BRNAME,'6' AS SEQUENCE FROM M_BRANCH INNER JOIN M_TPU_USER_MAPPING " +
                          " ON M_BRANCH.BRID=M_TPU_USER_MAPPING.TPUID " +
                          " WHERE M_TPU_USER_MAPPING.USERID='" + UserID + "' " +
                          " ORDER BY SEQUENCE,BRNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUser(string userid)
        {
            string sql = "SELECT CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END USERID,FNAME + ' ' + LNAME AS NAME FROM M_USER WHERE REFERENCEID='" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGUID_User(string userid)
        {
            string sql = "SELECT CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END USERID,FNAME + ' ' + LNAME AS NAME FROM M_USER WHERE (CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END)='" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOtherUser(string userid, string username)
        {
            string sql = "SELECT '" + username + "' AS UTNAME,'" + userid + "' AS USERID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPU(string userid, string tag)
        {
            string sql = string.Empty;
            if (tag == "QC" || tag == "FQA")
            {
                sql = "SELECT V.VENDORID,V.VENDORNAME FROM [M_TPU_VENDOR] V INNER JOIN [M_TPU_USER_MAPPING] M ON V.VENDORID=M.TPUID WHERE M.USERID='" + userid + "'";
            }
            else
            {
                sql = "SELECT V.VENDORID,V.VENDORNAME FROM [M_TPU_VENDOR] V INNER JOIN [M_TPU_USER_MAPPING] M ON V.VENDORID=M.TPUID WHERE M.USERID='" + userid + "' AND V.TAG='" + tag + "'";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepot(string userid, string tag)
        {
            string sql = "SELECT '-1' AS  BRID,'-- SELECT MOTHERDEPOT/DEPOT --' AS BRNAME,'0' AS SEQUENCE UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'4' AS SEQUENCE   FROM M_BRANCH D " +
                         "INNER JOIN [M_TPU_USER_MAPPING] M ON M.TPUID=D.BRID" +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' AND M.USERID='" + userid + "' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " E INNER JOIN [M_TPU_USER_MAPPING] M ON M.TPUID=E.BRID WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE'  AND M.USERID='" + userid + "' ORDER BY SEQUENCE,BRNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int PasswordChange(string UserID, string CurrentPassword, string ConfirmPassword)
        {
            int result = 0;
            string sql = "update M_USER set PASSWORD=dbo.fnMIME64Encode(1,'" + ConfirmPassword + "'),LASTPASSWORDCHANGE=GETDATE() where USERID='" + UserID + "' AND dbo.fnMIME64Decode(PASSWORD)='" + CurrentPassword + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable BindUserDetails(string userid)
        {
            string sql = string.Empty;
            sql = "SELECT LASTPASSWORDCHANGE,USERNAME,RTRIM(LTRIM(FNAME)) + RTRIM(LTRIM(' ' + MNAME)) + ' ' + RTRIM(LTRIM(LNAME)) AS FULLNAME FROM [M_USER] WHERE USERID='" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable POCOUNT(string userid) /*new added by p.basu on 06122019*/
        {
            string sql = string.Empty;
            sql = "USP_PO_COUNT_USERWISE '" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GRNCOUNT(string FINYEAR) /*new added by p.basu on 11122019*/
        {
            string sql = string.Empty;
            sql = "USP_REJECTION_GRN_COUNT '" + FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        } 
        public DataTable BindDashBoardData(string userid,string finyear) /*new added by p.basu on 16052022*/
        {
            string sql = string.Empty;
            sql = "USP_BIND_DASHBOARD_DATA '" + userid + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
