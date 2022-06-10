using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsWorkstationMaster
    {
        DBUtils db = new DBUtils();
        DataTable dt = new DataTable();

        #region Insert & Update
        public int InsertUpdateWorkstationMaster(string ID, string Code, string Name, string Description, string Consumption, string InHours, string Mode, bool Active, string ProductID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "I")
                {
                    sqlstr = "EXEC SP_PROCESSWORKSTATIONMASTERINSERTUPDATEDELETE '" + string.Empty + "','" + Code + "', '" + Name + "','" + Description + "', " +
                        "'" + Consumption + "','" + InHours + "','" + HttpContext.Current.Session["UserID"].ToString() + "','" + string.Empty + "', " +
                        "'I','" + Active + "','" + ProductID + "'";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "EXEC SP_PROCESSWORKSTATIONMASTERINSERTUPDATEDELETE '" + ID + "','" + Code + "', '" + Name + "','" + Description + "', " +
                       "'" + Consumption + "','" + InHours + "','" + HttpContext.Current.Session["UserID"].ToString() + "','" + string.Empty + "', " +
                       "'U','" + Active + "','" + ProductID + "'";
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
        /// Delete Record Basis On WorkstationID
        /// </summary>
        /// <param name="WorkstationID"></param>
        /// <returns></returns>
        public int DeleteWorkstationRecord(string WorkstationID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_PROCESSWORKSTATIONMASTERINSERTUPDATEDELETE '" + WorkstationID + "','" + string.Empty + "','" + string.Empty + "'," +
                "'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','D','" + string.Empty + "'";
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
        public DataTable FetchWorkstationMaster()
        {
            string sql = "EXEC SP_FETCHWORKSTATIONRECORD '" + string.Empty + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /// <summary>
        /// Show Records Basis On WorkstationID
        /// </summary>
        /// <param name="WorkstationID"></param>
        /// <returns></returns>
        public DataTable FetchWorkstationMasterByID(string WorkstationID)
        {
            string sql = "EXEC SP_FETCHWORKSTATIONRECORD '" + WorkstationID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
    }
}
