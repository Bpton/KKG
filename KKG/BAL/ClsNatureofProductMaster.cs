using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsNatureofProductMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindNatureofProductMasterGrid()
        {
            string sql = "select NOPID, NOPCODE, NOPNAME, NOPDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE' from M_NATUREOFPRODUCT";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveNatureofProductMaster(string ID, string txtNOPCode, string txtNOPName, string txtNOPDescription, string Mode, string active)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (txtNOPCode != "")
                    {
                        strcheck = "select top 1 * from M_NATUREOFPRODUCT where NOPCODE='" + txtNOPCode + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtNOPName != "")
                    {
                        strcheck = "select top 1 * from M_NATUREOFPRODUCT where NOPNAME='" + txtNOPName + "'";
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

                    if (txtNOPCode != "")
                    {
                        strcheck = "select top 1 * from M_NATUREOFPRODUCT where NOPCODE='" + txtNOPCode + "' and  NOPID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtNOPName != "")
                    {
                        strcheck = "select top 1 * from M_NATUREOFPRODUCT where NOPNAME='" + txtNOPName + "' and  NOPID<>'" + ID + "'";
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
                        sqlstr = "insert into M_NATUREOFPRODUCT ([NOPID],[NOPCODE],[NOPNAME],[NOPDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE])" +
                           " values(NEWID(),upper('" + txtNOPCode + "'), '" + txtNOPName + "','" + txtNOPDescription + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',Getdate(),'','','" + Mode + "','N','" + active + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_NATUREOFPRODUCT  set NOPCODE=upper('" + txtNOPCode + "'),NOPNAME='" + txtNOPName + "',NOPDESCRIPTION='" + txtNOPDescription + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=Getdate(),STATUS='" + Mode + "',ACTIVE='" + active + "' where NOPID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    //  result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public int DeleteNOPMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_NATUREOFPRODUCT where NOPID= '" + ID + "'";
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

