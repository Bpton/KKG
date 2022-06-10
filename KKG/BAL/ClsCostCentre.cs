using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsCostCentre
    {
        DBUtils db = new DBUtils();

        public DataTable BindCostCentreMastergrid()
        {
            string sql = "SELECT COSTCENTREID, COSTCENTRENAME, COSTCENTRECODE, COSTCENTREDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='True' then 'Active'  else 'InActive' END AS 'ACTIVE' FROM M_COST_CENTRE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCostCentreMastergrideditbyid(string costid)
        {
            string sql = "SELECT COSTCENTREID, COSTCENTRENAME, COSTCENTRECODE, COSTCENTREDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  CASE WHEN ACTIVE='True' then 'Active'  else 'InActive' END AS 'ACTIVE' from M_COST_CENTRE WHERE COSTCENTREID='" + costid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCostCentreMaster(string ID, string Code, string Name, string Description, string userid, string Mode, string Active)
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
                        strcheck = "select top 1 * from M_COST_CENTRE where COSTCENTRECODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_COST_CENTRE where COSTCENTRENAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }

                    }

                }
                else
                {

                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_COST_CENTRE where COSTCENTRECODE='" + Code + "' and  COSTCENTREID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_COST_CENTRE where COSTCENTRENAME='" + Name + "' and  COSTCENTREID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }

                    }


                }

                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "INSERT INTO M_COST_CENTRE ([COSTCENTREID],[COSTCENTRECODE],[COSTCENTRENAME],[COSTCENTREDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE])" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "'," +
                            " '" + userid + "',GETDATE(),'','','" + Mode + "','N','" + Active + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = " UPDATE M_COST_CENTRE set COSTCENTRECODE='" + Code + "',COSTCENTRENAME='" + Name + "',COSTCENTREDESCRIPTION='" + Description + "'," +
                                 " LMBU='" + userid + "',LDTOM=GETDATE(),STATUS='" + Mode + "'," +
                                 " ACTIVE='" + Active + "' WHERE COSTCENTREID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public int DeleteCostCentreMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = " IF EXISTS(SELECT 1 FROM Acc_CostCenter WHERE CostCenterID= '" + ID + "')" +
                         " BEGIN " +
                          " SELECT -1" +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " DELETE FROM M_COST_CENTRE WHERE COSTCENTREID= '" + ID + "'" +                        
                         " END";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
    }
}
