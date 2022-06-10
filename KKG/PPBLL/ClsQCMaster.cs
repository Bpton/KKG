using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsQCMaster
    {
        DBUtils db = new DBUtils();
        DataTable dt = new DataTable();

        #region Insert & Update
        public int InsertUpdateQCMaster(string ID, string Code, string Name, string Description, string Type, string FromRange, string ToRange,
                                        int Unit, int HourlyTimeEstimate, string Mode, bool Active)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "I")
                {
                    sqlstr = "EXEC SP_ProcessQCMasterInsertUpdateDelete '" + string.Empty + "','" + Code + "', '" + Name + "','" + Description + "', " +
                        "'" + Type + "','" + FromRange + "','" + ToRange + "','" + Unit + "','" + HourlyTimeEstimate + "','" + HttpContext.Current.Session["UserID"].ToString() + "', " +
                        "'" + string.Empty + "','I','" + Active + "'";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "EXEC SP_ProcessQCMasterInsertUpdateDelete '" + ID + "','" + Code + "', '" + Name + "','" + Description + "', " +
                       "'" + Type + "','" + FromRange + "','" + ToRange + "','" + Unit + "','" + HourlyTimeEstimate + "', " +
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
        /// Delete Record Basis On QCID
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public int DeleteQCRecord(string QCID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_ProcessQCMasterInsertUpdateDelete '" + QCID + "','" + string.Empty + "','" + string.Empty + "'," +
                "'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "', " +
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

        #region SELECT
        /// <summary>
        /// Show All Records
        /// </summary>
        /// <returns></returns>
        public DataTable FetchQCMaster()
        {
            string sql = "EXEC SP_FetchQCRecord '" + string.Empty + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /// <summary>
        /// Show Records Basis On QCID
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public DataTable FetchQCMasterByID(string QCID)
        {
            string sql = "EXEC SP_FetchQCRecord '" + QCID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
    }
}
