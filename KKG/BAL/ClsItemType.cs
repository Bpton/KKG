using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsItemType
    {
        DBUtils db = new DBUtils();
        public int SavePrimaryItemTypeMaster(string ID, string Code, string Name, string Description, string Mode, string Predefine, string active, string ISSERVICE, string ItemOwner)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_SUPLIEDITEM WHERE ITEM_NAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                    if (Code != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_SUPLIEDITEM WHERE ITEMCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 4;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_SUPLIEDITEM WHERE ITEM_NAME='" + Name + "' AND  ID <>'" + ID + "'";
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
                        sqlstr = " INSERT INTO M_SUPLIEDITEM([ITEMCODE],[ITEM_Name],[ITEMDESC],[PREDEFINE],[ACTIVE],ISSERVICE,ITEMOWNER,[ALTERNATECODE])" +
                                 " values('" + Code + "', '" + Name + "', dbo.fnConvert_TitleCase('" + Description + "'),'Y','" + active + "','" + ISSERVICE + "','" + ItemOwner + "','" + Code + "' )";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "UPDATE M_SUPLIEDITEM SET ITEMCODE='" + Code + "',ITEM_Name='" + Name + "',ISSERVICE='" + ISSERVICE + "',ITEMDESC=dbo.fnConvert_TitleCase('" + Description + "'),ACTIVE='" + active + "',ITEMOWNER='" + ItemOwner + "' WHERE ID= '" + ID + "'";
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

        public int SaveSubItemTypeMaster(string SubID, int PrimaryID, string Code, string Name, string Description, string Mode, string active, string Hsn, string ItemOwner, string BranchID)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            string strcheck1;
            string SQLHESNCODE = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (SubID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRIMARY_SUB_ITEM_TYPE WHERE SUBITEMNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRIMARY_SUB_ITEM_TYPE WHERE SUBITEMCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 4;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRIMARY_SUB_ITEM_TYPE WHERE SUBITEMNAME='" + Name + "' AND  SUBTYPEID <>'" + SubID + "'";
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
                    sqlstr = "EXEC[USP_RPT_SUBITEMTYPE_MASTER_INSDERT_UPDATE]'" + SubID + "','" + PrimaryID + "','" + Code + "','" + Name + "','" + Description + "','" + Mode + "','" + active + "','" + Hsn + "','" + ItemOwner + "','" + BranchID + "'";
                    strcheck1 = (string)db.GetSingleValue(sqlstr);
                    if (strcheck1 == "1")
                    {
                        result = 1;
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

        public int DeletePrimaryItemType(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM M_SUPLIEDITEM WHERE ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int DeleteSubItemType(string SubID)
        {
            int result = 0;
            string sqlstr;
            string sqlstrHSN = string.Empty;
            try
            {
                sqlstr = "DELETE FROM M_PRIMARY_SUB_ITEM_TYPE WHERE SUBTYPEID= '" + SubID + "'";
                result = db.HandleData(sqlstr);

                sqlstrHSN = "Delete from M_HSNCODE where CATID= '" + SubID + "'";
                result = db.HandleData(sqlstrHSN);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindPrimaryItemType()
        {
            string sql = "SELECT ID, ITEMCODE, ITEM_Name, ITEMDESC, PREDEFINE,CASE WHEN ACTIVE='Y' THEN 'ACTIVE'  ELSE 'INACTIVE' END AS STATUS,ACTIVE,CASE WHEN ITEMOWNER='M' THEN 'KKG' END AS ITEMOWNER  from M_SUPLIEDITEM";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSubItemType()
        {
            string sql = " SELECT CASE WHEN A.ITEMOWNER='M' THEN 'KKG' END AS ITEMOWNER ,A.SUBTYPEID, A.PRIMARYITEMTYPEID,B.ITEMDESC,A.SUBITEMCODE, A.SUBITEMNAME, A.SUBITEMDESC," +
                         " CASE WHEN A.ACTIVE='Y' THEN 'ACTIVE'  ELSE 'INACTIVE' END AS STATUS " +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE AS A INNER JOIN M_SUPLIEDITEM AS B" +
                         " ON A.PRIMARYITEMTYPEID = B.ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditPrimaryItemType(string ID)
        {
            string sql = "SELECT ID, ITEMCODE, ITEM_Name, ITEMDESC, PREDEFINE,ACTIVE,ISSERVICE FROM M_SUPLIEDITEM WHERE ID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Itemwnoerload()
        {
            string sql = "SELECT  top 1 ITEMOWNER,CASE WHEN ITEMOWNER='M' THEN 'KKG' END AS ITEMOWNERNAME FROM M_PRIMARY_SUB_ITEM_TYPE ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditSubItemType(string SubID)
        {
            string sql = " SELECT SUBTYPEID, PRIMARYITEMTYPEID,SUBITEMCODE, SUBITEMNAME, SUBITEMDESC,ACTIVE,HSE," +
                         " CASE WHEN ITEMOWNER='M' THEN 'KKG' END AS ITEMOWNER ,BRANCHID" +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE " +
                         " WHERE SUBTYPEID = '" + SubID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadPrimaryItemType()
        {
            string sql = "SELECT DISTINCT A.ID,ITEMDESC FROM M_SUPLIEDITEM A WITH(NOLOCK) LEFT JOIN M_PRODUCT B WITH(NOLOCK) ON A.ITEMCODE=B.TYPE WHERE A.ID <> 1";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable LoadPrimaryItem()
        //{
        //    string sql = "SELECT DISTINCT A.ID,ITEMDESC FROM M_SUPLIEDITEM A WITH(NOLOCK) LEFT JOIN M_PRODUCT B WITH(NOLOCK) ON A.ITEMCODE=B.TYPE WHERE A.ID <> 1";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable GeneratePrimaryItemCode()
        {
            string sql = "EXEC USP_PRIMARYITEM_CODE_GENERATION ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GenerateSubItemCode()
        {
            string sql = "EXEC USP_SUBITEM_CODE_GENERATION ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Depot_Accounts(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH AS A " +
                      " INNER JOIN M_TPU_USER_MAPPING AS B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "'  ORDER BY BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataTable Check_Product_Use_in_Transaction(string SubItemTypeID)
        {

            string sql = "EXEC [SP_CHECK_PRODUCT_USE_IN_TRANSACTION] @p_subtypeid = '" + SubItemTypeID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
   
}
