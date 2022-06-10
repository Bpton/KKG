using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsItemTypeMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindItemTypeMasterGrid()
        {
            string sql = "select ITID, ITCODE, ITNAME, ITDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE' from M_ITEMTYPE order by ITNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveItemTypeMasterGrid(string ID, string txtITCode, string txtITName, string txtDescription, string Mode, string Active)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (txtITCode != "")
                    {
                        strcheck = "select top 1 * from M_ITEMTYPE where ITCODE='" + txtITCode + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtITName != "")
                    {
                        strcheck = "select top 1 * from M_ITEMTYPE where ITNAME='" + txtITName + "'";
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

                    if (txtITCode != "")
                    {
                        strcheck = "select top 1 * from M_ITEMTYPE where ITCODE='" + txtITCode + "' and  ITID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtITName != "")
                    {
                        strcheck = "select top 1 * from M_ITEMTYPE where ITNAME='" + txtITName + "' and  ITID<>'" + ID + "'";
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
                        sqlstr = "insert into M_ITEMTYPE([ITID],[ITCODE],[ITNAME],[ITDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE])" +
                           " values(NEWID(),upper('" + txtITCode + "'), '" + txtITName + "','" + txtDescription + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GetDate(),'','','" + Mode + "','N','" + Active + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_ITEMTYPE set ITCODE=upper('" + txtITCode + "'),ITNAME='" + txtITName + "',ITDESCRIPTION='" + txtDescription + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GetDate(),STATUS='" + Mode + "', ACTIVE='" + Active + "'  where ITID= '" + ID + "'";
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




        public int DeleteITMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_ITEMTYPE where ITID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int SaveSizeTypeMasterGrid(string ID, string txtITCode, string txtITName, string txtDescription, string Mode)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (txtITCode != "")
                    {
                        strcheck = "select top 1 * from M_SIZE where CODE='" + txtITCode + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtITName != "")
                    {
                        strcheck = "select top 1 * from M_SIZE where ITEM_NAME='" + txtITName + "'";
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

                    if (txtITCode != "")
                    {
                        strcheck = "select top 1 * from M_SIZE where CODE='" + txtITCode + "' and  ITID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (txtITName != "")
                    {
                        strcheck = "select top 1 * from M_SIZE where ITEM_NAME='" + txtITName + "' and  ITID<>'" + ID + "'";
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
                        sqlstr = "insert into M_SIZE([CODE],[ITEM_NAME],[ITEMDESC],[DTOC],[CBU],[DTOM],[LCBU])" +
                           "values('" + txtITCode + "', '" + txtITName + "','" + txtDescription + "', " +
                            " GetDate(),'" + HttpContext.Current.Session["UserID"].ToString() + "','','')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_SIZE set CODE='" + txtITCode + "',ITEM_NAME='" + txtITName + "',ITEMDESC='" + txtDescription + "',LCBU='" + HttpContext.Current.Session["UserID"].ToString() + "',DTOM=GetDate()  where ID= '" + ID + "'";
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

        public DataTable BindSizeMasterGrid()
        {
            string sql = "SELECT ID,CODE,ITEM_NAME,ITEMDESC,DTOC,CBU,DTOM,LCBU FROM M_SIZE ORDER BY ITEM_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteSIZEMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_SIZE where ID= '" + ID + "'";
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
