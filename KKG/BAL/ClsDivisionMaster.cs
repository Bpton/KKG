using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsDivisionMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindDIVMasterGrid()
        {
            string sql = "select DIVID, DIVCODE, DIVNAME, DIVDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PREDEFINED from M_DIVISION order by DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDIVMastergrideditbyid(string DivID)
        {

            string sql = "select DIVID, DIVCODE, DIVNAME, DIVDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PREDEFINED,ALIASNAME from M_DIVISION WHERE DIVID='" + DivID + "' order by DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveDivMaster(string ID, string Code, string Name, string Description, string Mode, string active)
        {
            int result = 0;
            string sqlstr;
            string sqlstr1;
            string codeupper;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_DIVISION where DIVCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_DIVISION where DIVNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_DIVISION where DIVCODE='" + Code + "' and  DIVID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_DIVISION where DIVNAME='" + Name + "' and  DIVID<>'" + ID + "'";
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
                        sqlstr = "insert into M_DIVISION([DIVID],[DIVCODE],[DIVNAME],[DIVDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],PREDEFINED)" +
                           " values(NEWID(),upper('" + Code + "'), '" + Name + "','" + Description + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + active + "','N')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_DIVISION set DIVCODE=upper('" + Code + "'),DIVNAME='" + Name + "',DIVDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where DIVID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    //result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public int DeleteDIVMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_DIVISION where DIVID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        /* public int Check_Product_Use_in_Transaction(string BrandID)
         {
             int result = 0;
             try
             {
                 string sql = "EXEC USP_PRODUCT_USE_TRANSACTION @P_PRODUCTID='',@P_SEARCHTYPE='BRAND',@P_SEARCHTYPEID='" + BrandID + "'";
                 result = db.HandleData(sql);
             }

             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return result;
         }*/
    }
}
