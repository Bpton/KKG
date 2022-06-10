using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ProcessFramework
    {
        DBUtils db = new DBUtils();
        DataTable dtProcessFramework = new DataTable();

        #region Insert & Update
        public int InsertUpdateProcessFramework(string ID, string Code, string Name, string Description, string Mode, bool Active)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                if (Mode == "I")
                {
                    sqlstr = "EXEC SP_PROCESSFRAMEWORKINSERTUPDATEDELETE '" + string.Empty + "','" + Code + "', '" + Name + "','" + Description + "', " +
                             "'" + HttpContext.Current.Session["UserID"].ToString() + "','" + string.Empty + "','I','" + Active + "'";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "EXEC SP_PROCESSFRAMEWORKINSERTUPDATEDELETE '" + ID + "','" + Code + "', '" + Name + "','" + Description + "', " +
                             "'" + HttpContext.Current.Session["UserID"].ToString() + "','" + string.Empty + "','U','" + Active + "'";
                    result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
            }
            return result;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessFrameworkID"></param>
        /// <returns></returns>
        public int DeleteProcessFrameworkRecord(string ProcessFrameworkID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_PROCESSFRAMEWORKINSERTUPDATEDELETE '" + ProcessFrameworkID + "','" + string.Empty + "','" + string.Empty + "'," +
                "'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','D','" + string.Empty + "'";
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

        #region Select

        /// <summary>
        /// Show All Records
        /// </summary>
        /// <returns></returns>
        public DataSet FetchProcessFramework()
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_FETCHPROCESSFRAMEWORKRECORD '" + string.Empty + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet FetchProcessFramework(string ITEMID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_FETCHPROCESSFRAMEWORKRECORD_PRODUCTID  '','" + ITEMID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        /// <summary>
        /// Show All Records
        /// </summary>
        /// <returns></returns>
        public DataSet FetchProcessFrameworkEdit(string ProcessFrameworkID, string ProcessID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_EDITFETCHPROCESSFRAMEWORK '" + ProcessFrameworkID + "','" + ProcessID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
            //DataTable dt = new DataTable();
            //dt = db.GetData(sql);
            //return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessFrameworkID"></param>
        /// <returns></returns>
        public DataSet FetchProcessFrameworkByID(string ProcessFrameworkID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_FETCHPROCESSFRAMEWORKRECORD '" + ProcessFrameworkID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
            //DataTable dt = new DataTable();
            //dt = db.GetData(sql);
            //return dt;
        }
        public DataSet FetchProcessFrameWorkDetailRecord(string ProcessFrameworkID, string ProcessID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC P_FETCH_PROCESS_FRAMEWORK_DETAILS_RECORD '" + ProcessFrameworkID + "','" + ProcessID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        #region Insert-Update Process Framework
        public string InsertUpdateProcessFramework(string ProcessFrameworkName, string ProcessFrameworkCode, string ProcessFrameworkDesc, string Product, string CreatedBy, string ModifiedBy,
                                string xmlProcessFrameworSequence, string xmlProcessFrameworkWorkstation, string xmlProcessFrameworkResource, string xmlProcessFrameworkMaterialInput,
                                string xmlProcessFrameworkMaterialOutput, string xmlProcessFrameworkQC, string xmlFG, string Flag, string ProcessFrameworkID, string ProcessID)
        {

            string InvoiceID = string.Empty;
            string InvoiceNo = string.Empty;
            if (Flag == "I")
            {
                try
                {
                    string sqlprocProcessFramework = " EXEC [SP_PROCESS_FRAMEWORK_INSERT_UPDATE] '" + ProcessFrameworkName + "','" + ProcessFrameworkCode + "','" + ProcessFrameworkDesc + "','" + Product + "'," +
                                                     "'" + CreatedBy + "','" + ModifiedBy + "','" + xmlProcessFrameworSequence + "','" + xmlProcessFrameworkWorkstation + "'," +
                                                     "'" + xmlProcessFrameworkResource + "','" + xmlProcessFrameworkMaterialInput + "','" + xmlProcessFrameworkMaterialOutput + "','" + xmlProcessFrameworkQC + "','" + xmlFG + "'," +
                                                     "'" + Flag + "','" + ProcessFrameworkID + "','" + ProcessID + "'";
                    DataTable dtDespatch = db.GetData(sqlprocProcessFramework);
                    if (dtDespatch.Rows.Count > 0)
                    {
                        InvoiceNo = dtDespatch.Rows[0]["FRAMEWORK"].ToString();
                    }
                    else
                    {
                        InvoiceNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            else if (Flag == "U")
            {
                try
                {
                    string sqlprocProcessFramework = " EXEC [SP_PROCESS_FRAMEWORK_INSERT_UPDATE] '" + ProcessFrameworkName + "','" + ProcessFrameworkCode + "','" + ProcessFrameworkDesc + "','" + Product + "'," +
                                                     "'" + CreatedBy + "','" + ModifiedBy + "','" + xmlProcessFrameworSequence + "','" + xmlProcessFrameworkWorkstation + "'," +
                                                     "'" + xmlProcessFrameworkResource + "','" + xmlProcessFrameworkMaterialInput + "','" + xmlProcessFrameworkMaterialOutput + "','" + xmlProcessFrameworkQC + "','" + xmlFG + "'," +
                                                     "'" + Flag + "','" + ProcessFrameworkID + "','" + ProcessID + "'";
                    DataTable dtDespatch = db.GetData(sqlprocProcessFramework);
                    if (dtDespatch.Rows.Count > 0)
                    {
                        InvoiceNo = dtDespatch.Rows[0]["FRAMEWORK"].ToString();
                    }
                    else
                    {
                        InvoiceNo = "";
                    }

                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            return InvoiceNo;
        }
        #endregion
    }
}
