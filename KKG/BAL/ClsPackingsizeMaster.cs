using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsPackingsizeMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindPackingsizeMasterGrid()
        {
            string sql = "select PSID, PSCODE, PSNAME, PSDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PREDEFINED from M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SavePackingsizeMaster(string ID, string Code, string Name, string Description, string Mode, string active)
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
                        strcheck = "select top 1 * from M_PACKINGSIZE where PSCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PACKINGSIZE where PSNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_PACKINGSIZE where PSCODE='" + Code + "' and  PSID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PACKINGSIZE where DIVNAME='" + Name + "' and  PSID<>'" + ID + "'";
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
                        sqlstr = "insert into M_PACKINGSIZE([PSID],[PSCODE],[PSNAME],[PSDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],PREDEFINED)" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + active + "','N')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_PACKINGSIZE set PSCODE='" + Code + "',PSNAME='" + Name + "',PSDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "'where PSID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    // result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public int DeletePackingsizeMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_PACKINGSIZE where PSID= '" + ID + "'";
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
