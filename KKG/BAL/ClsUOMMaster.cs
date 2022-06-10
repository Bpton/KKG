using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsUOMMaster
    {
        DBUtils db = new DBUtils();
        public DataTable BindUOMMasterGrid()
        {
            string sql = "select UOMID, UOMCODE, UOMNAME, UOMDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PREDEFINED from M_UOM ORDER BY UOMNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveUOMMaster(string ID, string Code, string Name, string Description, string Mode, string active)
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
                        strcheck = "select top 1 * from M_UOM where UOMCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_UOM where UOMNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_UOM where UOMCODE='" + Code + "' and  UOMID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_UOM where UOMNAME='" + Name + "' and  UOMID<>'" + ID + "'";
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
                        sqlstr = "insert into M_UOM([UOMID],[UOMCODE],[UOMNAME],[UOMDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],PREDEFINED)" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "', '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + active + "','N')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_UOM set UOMCODE='" + Code + "',UOMNAME='" + Name + "',UOMDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where UOMID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
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
        public int DeleteUOMMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_UOM where UOMID= '" + ID + "'";
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
