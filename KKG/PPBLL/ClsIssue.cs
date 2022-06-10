using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsIssue
    {
        DBUtils db = new DBUtils();
        public DataTable BindDepartment(string userid)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT DEPARTMENTID DEPTID,DEPARTMENTNAME DEPTNAME FROM M_USER WHERE USERID='"+userid+"'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable BindRequistionDepartment()
        {
            DataTable dt = new DataTable();
            try
            {
                string USERID = HttpContext.Current.Session["USERID"].ToString().Trim();
                string sql = "EXEC [USP_BIND_ALL_DEPARTMENT_EXPECT_OWN]'" + USERID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable BindRequesitionDetails(string DEPTID, string REQUISITIONFROMDATE, string REQUISITIONTODATE, string ISSUEID, string DEPOTID,string MODE)
        {
            DataTable dt = new DataTable();
            try
            {
                string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
                string sql = "EXEC [USP_BIND_REQUESITIONDETAILS] '" + DEPTID + "','" + REQUISITIONFROMDATE + "','" + REQUISITIONTODATE + "','" + ISSUEID + "','" + DEPOTID + "','" + MODE + "','" + FINYEAR + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable BindMaterilaDetails(string REQUISITIONID, string ISSUENO, string FACTORYID) /*USERID NEW ADD BY P.BASU ON 20-02-2021*/
        {
            DataTable dt = new DataTable();
            try
            {
                string userid = HttpContext.Current.Session["USERID"].ToString().Trim();
                string sql = "EXEC USP_REQUESITION_MATERIAL_DETAILS '" + REQUISITIONID + "','" + ISSUENO + "','" + FACTORYID + "','"+ userid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public string InsertIssuetDetails(string ISSUEID, string ISSUEDATE, string CREATEDBY, string FINYEAR, string REMARKS, string MATERIALDETAILSxml,
                                        string SELECTREQUISITION, string deptid, string DeptName, string Depotid,string storeId)
        {
            string issueno = string.Empty;
            try
            {
                string sql = "EXEC [USP_ISSUE_INSERT_UPDATE] '" + ISSUEID + "','" + ISSUEDATE + "','" + CREATEDBY + "','" + FINYEAR + "','" + REMARKS + "'," +
                             " '" + MATERIALDETAILSxml + "','" + SELECTREQUISITION + "','" + deptid + "','" + DeptName + "','" + Depotid + "','"+ storeId + "'";
                DataTable dt = db.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    issueno = dt.Rows[0]["ISSUENO"].ToString();
                }
                else
                {
                    issueno = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return issueno;
        }

        public DataSet EditIssueGrid(string ISSUEID)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [USP_ISSUE_EDIT] '" + ISSUEID + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return ds;
        }
        public void Accepted(string IssueID, string depotid)
        {
            string sql = "UPDATE T_ISSUE_HEADER SET ISVERIFY='Y', DEPOTID='" + depotid + "',VERIFIEDDATETIME=GETDATE() WHERE ISSUEID='" + Convert.ToString(IssueID) + "'";
            db.HandleData(sql);
        }
        public DataTable BindIssuegrdid(string Fromdt, string Todt, string Name, string Status, string DepotID, String FinYear)
        {
            DataTable dt = new DataTable();
            try
            {
                string userid = HttpContext.Current.Session["USERID"].ToString().Trim();
                string sql = "EXEC USP_BIND_ISSUE '" + Fromdt + "','" + Todt + "','" + Name + "','" + Status + "','" + DepotID + "','" + FinYear + "','" + userid + "'";/*NEW ADD*/
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public int DeleteIssue(string issueid)
        {
            int delflag = 0;
            try
            {
                string sql = "EXEC [USP_ISSUE_DELETE] '" + issueid + "'";
                int e = db.HandleData(sql);
                if (e == 0)
                {
                    delflag = 0;  // delete unsuccessfull
                }
                else
                {
                    delflag = 1;  // delete successfull
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return delflag;
        }

        public DataTable BindIssueDetails(string IssueID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ISSUE_DETAILS] '" + IssueID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindIssueHeader(string IssueID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ISSUE_HEADER] '" + IssueID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllDepartment()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT DEPTID,DEPTNAME FROM M_DEPARTMENT WITH(NOLOCK)";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable loadstorelocation(string mode,string departMentId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC[USP_BIND_STORELOCATION_DEPARTMENTWISE] '"+mode+"','"+departMentId+"'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public string fetchId(string mode,string id)/*NEW ADD FOR GETTING IDS*/
        {
            string issueno = string.Empty;
            string sql = "EXEC USP_GET_ID '"+ mode + "','" + id + "'";
            DataTable dt = db.GetData(sql);
            issueno = dt.Rows[0]["REQUISITIONID"].ToString();
            return issueno;
        } 
        public DataSet LoadPendingIssue(int Opertion,string FromDate, string ToDate, string USERID, string FINYEAR)/*NEW ADD FOR Issue Approved 17052022 p.basu*/
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_ISSUE_APPROVAL_ALL_OPERTION '"+ Opertion + "','"+ FromDate + "','" + ToDate + "','" + USERID + "','" + FINYEAR + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        } 
       
      
    }
}
