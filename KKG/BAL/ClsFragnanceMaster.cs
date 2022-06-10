using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsFragnanceMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindFragnanceMastergrid()
        {
            string sql = "select FRGID, FRGCODE, FRGNAME, FRGDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE' from M_FRAGNANCE order by FRGNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveFragnanceMaster(string ID, string Code, string Name, string Description, string Mode, string active)
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
                        strcheck = "select top 1 * from M_FRAGNANCE where FRGCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_FRAGNANCE where FRGNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_FRAGNANCE where FRGCODE='" + Code + "' and  FRGID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_FRAGNANCE where FRGNAME='" + Name + "' and  FRGID<>'" + ID + "'";
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
                        sqlstr = "insert into M_FRAGNANCE([FRGID],[FRGCODE],[FRGNAME],[FRGDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE])" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + active + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_FRAGNANCE set FRGCODE='" + Code + "',FRGNAME='" + Name + "',FRGDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where FRGID= '" + ID + "'";
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
        public int DeleteFragnanceMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_FRAGNANCE where FRGID= '" + ID + "'";
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
