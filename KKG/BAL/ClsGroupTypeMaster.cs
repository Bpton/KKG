using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsGroupTypeMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindGroupTypeGridView()
        {
            string sql= "SELECT GROUP_TYPEID, GROUP_CODE, GROUP_TYPENAME, DESCRIPTIONS,CBU, DTOC, MBU, DTOM, STATUS FROM M_GROUP_TYPE ORDER BY GROUP_TYPENAME";
            DataTable dt=db.GetData(sql);
            return dt;
        }

        public int SaveGroupTypeAMaster(string GROUP_TYPEID, string GROUP_CODE, string GROUP_TYPENAME, string DESCRIPTIONS, string CBU, string STATUS, string MODE)
        {
            string sqlchk = string.Empty;
            string sql = string.Empty;
            DataTable dt = new DataTable();
            int result = 0;
            if (GROUP_TYPEID == "")
            {
                if (GROUP_CODE != "")
                {
                    sqlchk = "SELECT TOP 1 * FROM M_GROUP_TYPE WHERE GROUP_CODE='" + GROUP_CODE + "'";
                    dt = db.GetData(sqlchk);
                    if (dt.Rows.Count != 0)
                    {
                        result = 2;
                        return result;
                    }
                }
                if (GROUP_TYPENAME != "")
                {
                    sqlchk = "SELECT TOP 1 * FROM M_GROUP_TYPE WHERE GROUP_TYPENAME='" + GROUP_TYPENAME + "'";
                    dt = db.GetData(sqlchk);
                    if (dt.Rows.Count != 0)
                    {
                        result = 3;
                        return result;
                    }
                }
            }
            else
            {
                if (GROUP_CODE != "")
                {
                    sqlchk = "SELECT TOP 1 * FROM M_GROUP_TYPE WHERE GROUP_CODE='" + GROUP_CODE + "' AND GROUP_TYPEID <> '" + GROUP_TYPEID + "' ";
                    dt = db.GetData(sqlchk);
                    if (dt.Rows.Count != 0)
                    {
                        result = 2;
                        return result;
                    }
                }
                if (GROUP_TYPENAME != "")
                {
                    sqlchk = "SELECT TOP 1 * FROM M_GROUP_TYPE WHERE GROUP_TYPENAME='" + GROUP_TYPENAME + "' AND GROUP_TYPEID <> '" + GROUP_TYPEID + "' ";
                    dt = db.GetData(sqlchk);
                    if (dt.Rows.Count != 0)
                    {
                        result = 3;
                        return result;
                    }
                }
                //else if (DESCRIPTIONS != "")
                //{
                //    sqlchk = "SELECT TOP 1 * FROM M_GROUP_TYPE WHERE DESCRIPTIONS='" + DESCRIPTIONS + "' AND GROUP_TYPEID <> '" + GROUP_TYPEID + "' ";
                //    dt = db.GetData(sqlchk);
                //    if (dt.Rows.Count != 0)
                //    {
                //        result = 3;
                //        return result;
                //    }
                //}
            }

            if (dt.Rows.Count == 0)
            {
                if (MODE == "A")
                {
                    sql = "EXEC[USP_GROUP_TYPE_CODE]'" + GROUP_TYPENAME + "','"+CBU+"','"+STATUS+"','"+MODE+"','"+ DESCRIPTIONS + "'";
                    result = db.HandleData(sql);
                }
                else
                {
                    sql = "UPDATE M_GROUP_TYPE SET GROUP_CODE='" + GROUP_CODE + "', GROUP_TYPENAME='" + GROUP_TYPENAME + "',MBU='" + CBU + "',DTOM=GETDATE(),MODE='" + MODE + "',DESCRIPTIONS='" + DESCRIPTIONS + "'," +
                        " STATUS='" + STATUS + "' WHERE GROUP_TYPEID='" + GROUP_TYPEID + "'";
                    result = db.HandleData(sql);
                }
            }
            return result;
         }

        public DataTable BindGroupTypeGridViewEdit(string GROUP_TYPEID)
        {
            string sql = "SELECT GROUP_TYPEID, GROUP_CODE, GROUP_TYPENAME,DESCRIPTIONS, CBU, DTOC, MBU, DTOM, STATUS FROM M_GROUP_TYPE WHERE GROUP_TYPEID='" + GROUP_TYPEID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public int DeleteGroupTypeMaster(string GROUP_TYPEID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from M_GROUP_TYPE where GROUP_TYPEID= '" + GROUP_TYPEID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public DataTable BindDepotSyncStatus()
        {
            string sql = "USP_LAST_SYNC_DATETIME_DEPOT";
            DataTable dt = db.GetData(sql);
            return dt;
        }
    }
} 
