using DAL;
using System;
using System.Data;
using Utility;
using System.Web;

namespace PPBLL
{
    public class Process
    {
        DBUtils db = new DBUtils();

        #region Insert & Update 
        public int InsertUpdateProcessMaster(string ID, string Code, string Name, string Description, string Mode, bool Active)
        {
            int result = 0;
            string sqlstr;            
            DataTable dt = new DataTable();
            try
            {
                if (Mode == "I")
                {
                    sqlstr = "EXEC SP_PROCESSMASTERINSERTUPDATEDELETE '" + string.Empty + "','" + Code + "', '" + Name + "','" + Description + "', " +
                        "'" + HttpContext.Current.Session["UserID"].ToString() + "','" + string.Empty + "','I','" + Active + "'";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "EXEC SP_PROCESSMASTERINSERTUPDATEDELETE '" + ID + "','" + Code + "', '" + Name + "','" + Description + "', " +
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
        /// Delete Record Basis On ProcessID
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public int DeleteProcessRecord(string ProcessID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_PROCESSMASTERINSERTUPDATEDELETE '" + ProcessID + "','" + string.Empty + "','" + string.Empty + "'," +
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
        public DataTable FetchProcessMaster()
        {
            string sql = "EXEC SP_FETCHPROCESSRECORD '" + string.Empty + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /// <summary>
        /// Show Records Basis On ProcessId
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        public DataTable FetchProcessMasterByID(string ProcessID)
        {
            string sql = "EXEC SP_FETCHPROCESSRECORD '" + ProcessID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindActive_ProcessMaterial(string DEPOTID)
        {            
            string SQL = "EXEC USP_BIND_PROCESSMATERIAL_INPUT '" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(SQL);
            return dt;
        }
        public DataTable BindActive_ProcessOutputMaterial(string DEPOTID)
        {
            string SQL = "EXEC USP_BIND_PROCESSMATERIAL_OUTPUT '" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(SQL);
            return dt;
        }

        public DataTable BindActiveMaterial(string DEPOTID)
        {
            string sql = " SELECT ID,CODE,NAME,PRODUCTALIAS,DIVID,DIVNAME,CATID,CATNAME,UOMID,UOMNAME,UNITVALUE AS UNIT,MRP,DTOC,CBU,STATUS,TYPE " +
                         " FROM [M_PRODUCT] AS A" +
                         /*" INNER JOIN M_PRODUCT_TPU_MAP	AS B ON PRODUCTID=A.ID" +
                         " WHERE ACTIVE='T' " +
                         " AND B.VENDORID='" + DEPOTID + "'" +*/
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SearchItemFromGrid(string itemname)
        {
            string sql = "[usp_GetFGSFGITEMS_Pager] '"+itemname+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*process framework new by p.basu startdate : 19-12-2020*/
        public DataTable BindOnlyFgProduct(string mode)
        {
            string sql = "[USP_LOAD_ALLMASTER_MODE_WISE_KKG] '" + mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        #endregion
    }
}
