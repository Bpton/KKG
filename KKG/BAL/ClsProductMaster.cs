using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsProductMaster
    {
        DBUtils db = new DBUtils();
        DataTable dtProductReorder = new DataTable();
        DataTable dteditProductReorder = new DataTable();
        DataTable dteditProductReorderClone = new DataTable();

        public DataTable BindProductMasterGridbycategory(string brand, string catagory)
        {
            if (brand == "0")
            {
                string sql = " SELECT ID,CODE,NAME,PRODUCTALIAS,DIVID,DIVNAME,CATID,CATNAME,NATUREID,NATURENAME,UOMID,UOMNAME,UNITVALUE,FRGID,FRGNAME," +
                             " ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, " +
                             " CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE, ISNULL(CSDINDEXNO,'') CSDINDEXNO" +
                             " FROM [M_PRODUCT]" +
                             " WHERE SETFLAG = 'P' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            else
            {
                string sql = " SELECT ID,CODE,NAME,PRODUCTALIAS,DIVID,DIVNAME,CATID,CATNAME,NATUREID,NATURENAME,UOMID,UOMNAME,UNITVALUE,FRGID,FRGNAME," +
                             " ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, " +
                             " CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE, ISNULL(CSDINDEXNO,'') CSDINDEXNO" +
                             " FROM [M_PRODUCT]" +
                             " WHERE SETFLAG = 'P' AND DIVID='" + brand + "' AND CATID='" + catagory + "' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
        }
        public DataTable BindProductMasterGrid()
        {
            string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS,DIVID,DIVNAME,CATID,CATNAME,NATUREID,NATURENAME,UOMID,UOMNAME,UNITVALUE,FRGID,FRGNAME," +
                         " ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, " +
                         " CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE, ISNULL(CSDINDEXNO,'') CSDINDEXNO" +
                         " FROM [M_PRODUCT]" +
                         " WHERE SETFLAG = 'P' OR SETFLAG = 'S'" +
                         " AND TYPE = 'FG'" +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMiscProductGrid()
        {
            string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME,UOMID,UOMNAME,UNITVALUE, MRP, DTOC, CBU, STATUS, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE,TYPE FROM [M_PRODUCT]" +
                         " WHERE SETFLAG = 'M'" +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Binditentype()
        {
            string sql = "SELECT ITID,ITNAME FROM M_ITEMTYPE ORDER BY ITNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUOMFORPM()
        {
            string sql = "SELECT UOMID,UOMDESCRIPTION FROM M_UOM WHERE UOMNAME NOT IN('NONE','BINDI SIZE','BOX')ORDER BY UOMDESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUOMFORRM()
        {
            string sql = "SELECT UOMID,UOMDESCRIPTION FROM M_UOM WHERE UOMNAME NOT IN('NONE','BINDI SIZE','BOX')ORDER BY UOMDESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUOMFORSFG()
        {
            string sql = "SELECT UOMID,UOMDESCRIPTION FROM M_UOM WHERE UOMNAME NOT IN('NONE','BINDI SIZE','BOX')ORDER BY UOMDESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMiscitentype()
        {
            string sql = "SELECT ITID,ITNAME FROM M_ITEMTYPE  WHERE ITID IN('A828808D-3F08-48B9-B558-F826F46553E2') ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindComboDivision()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION  WHERE ACTIVE='True' ORDER BY DIVNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindComboCategory()
        {
            string sql = "SELECT CATID,CATNAME FROM M_CATEGORY WHERE ACTIVE='True' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSearchCategory(string ID)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='" + ID + "' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindComboProductMasterGrid()
        {
            string sql = " SELECT ID, CODE, NAME, ASSESSABLEPERCENT, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE FROM [M_PRODUCT]" +
                         " WHERE SETFLAG = 'C'" +
                         " AND TYPE = 'FG'" +
                         " ORDER BY NAME,CODE";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSearchCat(string CATID)
        {
            string sql = " SELECT ID,NAME,CODE,MRP,DIVNAME,CATNAME,CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE," +
                         " COMBONAME=STUFF " +
                         " (  " +
                         " ( " +
                         " SELECT DISTINCT ', '+ CAST(g.PRODUCTNAME AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID " +
                         " FOR XMl PATH('')" +
                         " ),1,1,'' " +
                         " ) ," +
                         " QTY=STUFF " +
                         " (  " +
                         " (  " +
                         " SELECT  ': '+ CAST(g.QTY AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e   " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID  " +
                         "  FOR XMl PATH('')  " +
                         " ),1,1,''  " +
                         " )   " +
                         " FROM M_PRODUCT t1  WHERE SETFLAG='C' AND CATID='" + CATID + "'" +
                         " GROUP BY ID,NAME,CODE,MRP,DIVNAME,CATNAME,ACTIVE ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSearchDiv(string DIVID)
        {
            string sql = " SELECT ID,NAME,CODE,MRP,DIVNAME,CATNAME,CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE," +
                         " COMBONAME=STUFF " +
                         " (  " +
                         " ( " +
                         " SELECT DISTINCT ', '+ CAST(g.PRODUCTNAME AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID " +
                         " FOR XMl PATH('')" +
                         " ),1,1,'' " +
                         " ) ," +
                         " QTY=STUFF " +
                         " (  " +
                         " (  " +
                         " SELECT  ': '+ CAST(g.QTY AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e   " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID  " +
                         "  FOR XMl PATH('')  " +
                         " ),1,1,''  " +
                         " )   " +
                         " FROM M_PRODUCT t1  WHERE SETFLAG='C' AND DIVID='" + DIVID + "'" +
                         " GROUP BY ID,NAME,CODE,MRP,DIVNAME,CATNAME,ACTIVE ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindNewComboProductMasterGrid()
        {
            string sql = " SELECT ID,NAME,CODE,MRP,DIVNAME,CATNAME,CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE,ISNULL(CSDINDEXNO,'') CSDINDEXNO," +
                         " COMBONAME=STUFF " +
                         " (  " +
                         " ( " +
                         " SELECT DISTINCT ', '+ CAST(g.PRODUCTNAME AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID " +
                         " FOR XMl PATH('')" +
                         " ),1,1,'' " +
                         " ) ," +
                         " QTY=STUFF " +
                         " (  " +
                         " (  " +
                         " SELECT  ': '+ CAST(g.QTY AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e   " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID  " +
                         "  FOR XMl PATH('')  " +
                         " ),1,1,''  " +
                         " )   " +
                         " FROM M_PRODUCT t1  WHERE SETFLAG='C'" +
                         " GROUP BY ID,NAME,CODE,MRP,DIVNAME,CATNAME,ACTIVE,CSDINDEXNO ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNewComboProductMasterGridbyID(string divid, string catid)
        {
            string sql = " SELECT ID,NAME,CODE,MRP,DIVNAME,CATNAME,CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE," +
                         " COMBONAME=STUFF " +
                         " (  " +
                         " ( " +
                         " SELECT DISTINCT ', '+ CAST(g.PRODUCTNAME AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID " +
                         " FOR XMl PATH('')" +
                         " ),1,1,'' " +
                         " ) ," +
                         " QTY=STUFF " +
                         " (  " +
                         " (  " +
                         " SELECT  ': '+ CAST(g.QTY AS VARCHAR(MAX))" +
                         " FROM M_COMBO_PRODUCT g,M_PRODUCT e   " +
                         " WHERE g.COMBOPRODUCTID=e.ID and e.ID=t1.ID  " +
                         "  FOR XMl PATH('')  " +
                         " ),1,1,''  " +
                         " )   " +
                         " FROM M_PRODUCT t1  WHERE SETFLAG='C' AND DIVID='" + divid + "' AND CATID='" + catid + "'" +
                         " GROUP BY ID,NAME,CODE,MRP,DIVNAME,CATNAME,ACTIVE ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string BindPrimaryProduct(string pid)
        {
            string Primaryid = string.Empty;
            string sql = "SELECT PRODUCTID FROM [M_COMBO_PRODUCT] WHERE COMBOPRODUCTID='" + pid + "'";
            Primaryid = (string)db.GetSingleValue(sql);
            return Primaryid;
        }


        public DataTable BindProductMasterById(string id)
        {
            string sql = "EXEC [sp_freight_service] '" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindMiscProductMasterById(string id)
        {
            string sql = "SELECT ID, CODE, NAME, DIVID, DIVNAME, CATID,UOMID,UOMNAME,UNITVALUE, CATNAME,ASSESSABLEPERCENT,  MINSTOCKLEVEL, RETURNABLE, MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE,TYPE,ISNULL(ITEMTYPE_ID,'') AS ITEMTYPE_ID ,ISNULL(ITEMTYPE_NAME,'') AS ITEMTYPE_NAME  FROM [M_PRODUCT] WHERE ID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPUMasterGrid()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR where TAG='T' AND SUPLIEDITEM='FG'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //========================================Added By Sourav Mukherjee on 31/03/2016====================================================//
        public DataTable BindGroup(string BSID)
        {
            string sql = "SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY WHERE BUSINESSSEGMENTID= '" + BSID + "' ORDER BY DIS_CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //==================================================================================================================================//
        public DataTable Binditemtype()
        {
            string sql = "select ID,ITEM_Name from M_SUPLIEDITEM where ID in(4,5)  ORDER BY ITEM_Name DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryMasterGrid()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR where TAG='F'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPUMasterGridbyid(string id)
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_PRODUCT_TPU_MAP WHERE PRODUCTID='" + id + "'  and TAG='T' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryMasterGridbyid(string id)
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_PRODUCT_TPU_MAP WHERE PRODUCTID='" + id + "' and TAG='F'ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //========================================Added By Sourav Mukherjee on 31/03/2016====================================================//

        public DataTable BindGroupGridbyid(string id)
        {
            string sql = "SELECT GROUPID,GROUPNAME FROM M_PRODUCT_GROUP_MAPPING WHERE PRODUCTID='" + id + "' ORDER BY GROUPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //==================================================================================================================================//

        //public int SaveComboProduct(string pid, string Code, string pname,string aliasname, string DivId, string DivName, string CatId, string CatName, string NatureId, string NatureName, string UOMId, string UOMName, string UOMvalue, string FrgId, string FrgName, decimal Assessablepercentage, int Reorder, decimal Minstooklevel, string Refundable, decimal MRP, string Mode, string active)
        //{
        //    int result=0;
        //    string newproductid = Convert.ToString(Guid.NewGuid());

        //    if (Refundable == "True")
        //    {
        //        Refundable = "Y";
        //    }
        //    else
        //    {
        //        Refundable = "N";
        //    }

        //    string sqlstr = "insert into M_PRODUCT(ID,CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,ACTIVE,MRP, DTOC, CBU,LMBU,LDTOM,  STATUS,SETFLAG,TYPE)" +
        //                   " values('" + newproductid + "','" + Code + "', '" + pname + "','" + aliasname + "','" + DivId + "','" + DivName + "', " +
        //                   " '" + CatId + "','" + CatName + "','" + NatureId + "','" + NatureName + "','" + UOMId + "','" + UOMName + "','" + UOMvalue + "','" + FrgId + "','" + FrgName + "','" + Assessablepercentage + "','" + Reorder + "','" + Minstooklevel + "','" + Refundable + "','" + active + "','" + MRP + "', GETDATE(),'" + HttpContext.Current.Session["UserID"].ToString() + "','','','" + Mode + "','S','FG')";
        //    result = db.HandleData(sqlstr);
        //    string sql = "INSERT INTO M_COMBO_PRODUCT(COMBOPRODUCTID,PRODUCTID,PRODUCTNAME,QTY) VALUES('" + newproductid + "','" + pid + "','" + pname + "',1)";
        //    result = db.HandleData(sql);
        //    return result;
        //}

        //public int UpdateComboProduct(string pid, string code, string pname, string aliasname, decimal Assessablepercentage, int Reorder, decimal Minstooklevel, string Refundable, decimal MRP, string Mode, string active)
        //{
        //    int result = 0;
        //    string sql = string.Empty;

        //    sql = "UPDATE M_COMBO_PRODUCT SET PRODUCTNAME='" + pname + "' WHERE COMBOPRODUCTID='" + pid + "'";
        //    result = db.HandleData(sql);

        //    if (Refundable == "True")
        //    {
        //        Refundable = "Y";
        //    }
        //    else
        //    {
        //        Refundable = "N";
        //    }

        //    sql = string.Empty;
        //    sql = "update M_PRODUCT set NAME='" + pname + "',PRODUCTALIAS='" + aliasname + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',STATUS='" + Mode + "',LDTOM= GETDATE()," +
        //                         " ASSESSABLEPERCENT='" + Assessablepercentage + "',BESTBEFORE='" + Reorder + "',MINSTOCKLEVEL='" + Minstooklevel + "',RETURNABLE='" + Refundable + "',MRP='" + MRP + "',ACTIVE='" + active + "' where ID= '" + pid + "'";
        //    result = db.HandleData(sql);

        //    return result;
        //}

        public int SaveProductMaster(string guid, string ID, string Code, string FullName, string Name1, string Name2, string DivId, string DivName, string CatId,
                                     string CatName, string NatureId, string NatureName, string UOMId, string UOMName, string UOMvalue, string FrgId,
                                     string FrgName, decimal Assessablepercentage, int Reorder, decimal Minstooklevel, string Refundable, decimal MRP,
                                     string Mode, string active, string shortname, string itemtypeid, string itemtypename, string index, string IP_ADDRESS,
                                     string PowerSKU, string BillingDepot, string BARCODE, string PRODUCTOWNER,string BRANDCATEGORY,string service)
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
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE CODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result =4;
                            return result;
                        }
                    }
                    if (FullName != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE NAME='" + FullName + "'";
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
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE CODE='" + Code + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 4;
                            return result;
                        }
                    }
                    if (FullName != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE NAME='" + FullName + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                if (Refundable == "True")
                {
                    Refundable = "Y";
                }
                else
                {
                    Refundable = "N";
                }
                string date = string.Empty;
                string tag ="P";
                string fg = "FG";
                string cbu = HttpContext.Current.Session["UserID"].ToString();
                if (dt.Rows.Count == 0)
                {

                    sqlstr = "EXEC [USP_INSERT_UPDATE_PRODUCT_MASTER] '" + guid + "','" + ID + "','" + Code + "','" + FullName + "','" + Name1 + "','" + Name2 + "'," +
                        "'" + DivId + "','" + DivName + "','" + CatId + "','" + CatName + "','" + NatureId + "','" + NatureName + "','" + UOMId + "','" + UOMName + "','" + UOMvalue + "', " +
                        "'" + FrgId + "','" + FrgName + "','" + Assessablepercentage + "','" + Reorder + "','" + Minstooklevel + "','" + Refundable + "'," +
                        "'" + active + "','" + MRP + "','" + date + "','" + cbu + "','','','" + Mode + "','" + tag + "','" + fg + "','" + shortname + "','" + itemtypeid + "'," +
                        "'" + itemtypename + "','" + IP_ADDRESS + "','" + PowerSKU + "','" + BillingDepot + "','" + BARCODE + "','" + PRODUCTOWNER + "','" + BRANDCATEGORY + "','" + service + "'";

                    result = db.HandleData(sqlstr);
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
        public int DeleteProductMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "USP_PRODUCT_MASTER_DELETE '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public int SaveMiscellaneousProductMaster(string ID, string Code, string Name, string DivId, string DivName, string CatId, string CatName, string Uomid, string Uomname, string Unitvalue, decimal Minstooklevel, string Returnable, decimal MRP, string Mode, string active, string Type, string miscitemtypeid, string miscitemtypename)
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
                        strcheck = "select top 1 * from M_PRODUCT where CODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PRODUCT where NAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_PRODUCT where CODE='" + Code + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PRODUCT where NAME='" + Name + "' and ID <>'" + ID + "'";
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
                        sqlstr = "insert into M_PRODUCT(ID,CODE, NAME, DIVID, DIVNAME, CATID, CATNAME,UOMID,UOMNAME,UNITVALUE,ASSESSABLEPERCENT, MINSTOCKLEVEL, RETURNABLE,ACTIVE,MRP, DTOC, CBU, STATUS,SETFLAG,TYPE,ITEMTYPE_ID,ITEMTYPE_NAME)" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + DivId + "','" + DivName + "', " +
                           " '" + CatId + "','" + CatName + "','" + Uomid + "','" + Uomname + "','" + Unitvalue + "',0," + Minstooklevel + ",'" + Returnable + "','" + active + "'," + MRP + ", GETDATE()," + Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()) + ",'" + Mode + "','M','" + Type + "','" + miscitemtypeid + "','" + miscitemtypename + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_PRODUCT set CODE='" + Code + "',NAME='" + Name + "',DIVID='" + DivId + "',DIVNAME='" + DivName + "',LMBU=" + Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()) + ",STATUS='" + Mode + "',LDTOM= GETDATE()," +
                                 " CATID='" + CatId + "',CATNAME='" + CatName + "',UOMID='" + Uomid + "',UOMNAME='" + Uomname + "',UNITVALUE='" + Unitvalue + "',MINSTOCKLEVEL=" + Minstooklevel + ",RETURNABLE='" + Returnable + "',MRP=" + MRP + ",ACTIVE='" + active + "',TYPE='" + Type + "',ITEMTYPE_ID='" + miscitemtypeid + "',ITEMTYPE_NAME='" + miscitemtypename + "' where ID= '" + ID + "'";
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

        public int DeleteComboProductMaster(string ComboProductID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_COMBO_DELETE '" + ComboProductID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int SaveComboProductMaster(string Flag, string ID, string Code, string Name, string ComboDivID, string ComboDivName, string ComboCategoryID,
                                            string ComboCategoryName, decimal Assessablepercentage, decimal Minstooklevel, string Refundable, decimal MRP,
                                            int CreatedBy, string xml, string itemtypeid, string itemtypename, string active, decimal bestbefore, string csdindex,
                                            int Singlecartoon, string uomid, string uomname, string unit, string ProductAlias, string GtPrintName,
                                            string MtPrintName, string EXPARAPRINTNAME, string CSDPRINTNAME, string CPCPRINTNAME, string INCSPRINTNAME, string FragranceId, string FragranceName, string BillingDepot, string xmlDepot)
        {
            int result = 0;
            int resultCombo = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_PRODUCT where CODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PRODUCT where NAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_PRODUCT where CODE='" + Code + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_PRODUCT where NAME='" + Name + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                if (Refundable == "True")
                {
                    Refundable = "Y";
                }
                else
                {
                    Refundable = "N";
                }
                if (dt.Rows.Count == 0)
                {

                    string sql = string.Empty;
                    sql = " EXEC SP_COMBO_INSERT_UPDATE '" + ID + "','" + Flag + "','" + Name + "','" + Code + "','" + ComboDivID + "','" + ComboDivName + "'," +
                          " '" + ComboCategoryID + "','" + ComboCategoryName + "'," + MRP + "," + Assessablepercentage + "," +
                          " " + Minstooklevel + ",'" + Refundable + "'," + CreatedBy + ",'" + xml + "','" + itemtypeid + "'," +
                          " '" + itemtypename + "','" + active + "'," + bestbefore + ",'" + csdindex + "'," + Singlecartoon + "," +
                          " '" + uomid + "','" + uomname + "','" + unit + "','" + ProductAlias + "'," +
                          " '" + GtPrintName + "','" + MtPrintName + "','" + EXPARAPRINTNAME + "','" + CSDPRINTNAME + "','" + CPCPRINTNAME + "','" + INCSPRINTNAME + "','" + FragranceId + "','" + FragranceName + "','" + BillingDepot + "','" + xmlDepot + "'";
                    result = db.HandleData(sql);
                    result = 0;
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
        public DataSet ComboDetails(string ComboID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = "EXEC sp_COMBO_DETAILS '" + ComboID + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return ds;
        }
        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID, DIVNAME, DIVCODE FROM M_DIVISION  WHERE ACTIVE='True' ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            //string sql = "SELECT CATID, CATNAME, CATCODE FROM M_CATEGORY  WHERE ACTIVE='True' ORDER BY CATNAME";
            string sql = " SELECT CATID,CASE WHEN HSN IS NULL THEN CATNAME WHEN HSN='' THEN CATNAME ELSE CATNAME+'  ('+HSN+')' END AS CATNAME," +
                         " CATCODE FROM M_CATEGORY " +
                         " WHERE ACTIVE='True'" +
                         " ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindComboCategorybyBrand()
        {
            string sql = "SELECT DISTINCT CATID, CATNAME FROM M_PRODUCT  ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindComboCategorybyBrand(string DIVID)
        {
            string sql = "SELECT DISTINCT CATID, CATNAME FROM M_PRODUCT WHERE DIVID='" + DIVID + "' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindNature()
        {
            string sql = "SELECT NOPID, NOPNAME FROM M_NATUREOFPRODUCT ORDER BY NOPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        
        public DataTable BindFragrance()
        {
            string sql = "SELECT FRGID, FRGNAME, FRGCODE FROM M_FRAGNANCE ORDER BY FRGNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID, BSNAME FROM M_BUSINESSSEGMENT WHERE ACTIVE='True' ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepo()
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindReorderGrid(string ProductID)
        {
            string sql = "SELECT  0 as SLNO,DEPOTID, DEPOTNAME, REORDERLAVEL FROM M_PRODUCT_REORDER_MAP   WHERE PRODUCTID='" + ProductID + "' order by DEPOTNAME";
            DataTable dt = new DataTable();
            dteditProductReorder = db.GetData(sql);

            HttpContext.Current.Session["PRODUCTREORDERMAPPING"] = dteditProductReorder;
            dteditProductReorderClone = ((DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"]).Clone();

            for (int i = 0; i < dteditProductReorder.Rows.Count; i++)
            {
                DataRow dr = dteditProductReorderClone.NewRow();

                dr["SLNO"] = (i + 1).ToString();
                dr["DEPOTID"] = Convert.ToString(dteditProductReorder.Rows[i]["DEPOTID"]);
                dr["DEPOTNAME"] = Convert.ToString(dteditProductReorder.Rows[i]["DEPOTNAME"]);
                dr["REORDERLAVEL"] = Convert.ToString(dteditProductReorder.Rows[i]["REORDERLAVEL"]);

                dteditProductReorderClone.Rows.Add(dr);
                dteditProductReorderClone.AcceptChanges();
            }
            HttpContext.Current.Session["PRODUCTREORDERMAPPING"] = dteditProductReorderClone;

            return dteditProductReorderClone;
        }

        public DataTable BindMappingGridRecords(string DEPOTID, string DEPOTNAME, string REORDERLAVEL, string MODE)
        {
            if (MODE == "A")
            {
                dtProductReorder = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
                DataRow dr = dtProductReorder.NewRow();
                dr["SLNO"] = dtProductReorder.Rows.Count + 1;
                dr["DEPOTID"] = DEPOTID;
                dr["DEPOTNAME"] = DEPOTNAME;
                dr["REORDERLAVEL"] = REORDERLAVEL;

                dtProductReorder.Rows.Add(dr);
                dtProductReorder.AcceptChanges();
            }
            return dtProductReorder;
        }

        public int MappinReorderRecordsCheck(string DEPOTID, string hdnfld)
        {
            int flag = 0;
            if (hdnfld == "")
            {
                dtProductReorder = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
                foreach (DataRow dr in dtProductReorder.Rows)
                {
                    if (Convert.ToString(dr["DEPOTID"]) == DEPOTID)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            else
            {
                //dtProductReorder = (DataTable)HttpContext.Current.Session[""];
                //foreach (DataRow dr in dtProductReorder.Rows)
                //{
                //    if (Convert.ToString(dr["DEPOTID"]) == DEPOTID)
                //    {
                //        flag = 1;
                //        break;
                //    }
                //}
            }
            return flag;
        }
        public void ResetDataTablesForReorder()
        {
            dtProductReorder.Clear();
            dteditProductReorder.Clear();
            dteditProductReorderClone.Clear();
        }
        public int SaveReorderMaster(string Productid, string Productname, string Mode)
        {
            int result = 0;
            string sqlstr;

            DataTable dt = new DataTable();
            DataTable dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
            try
            {
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {

                        sqlstr = "DELETE FROM M_PRODUCT_REORDER_MAP WHERE PRODUCTID='" + Productid + "'";
                        result = db.HandleData(sqlstr);

                        for (int i = 0; i < dtProductMappingRecords.Rows.Count; i++)
                        {
                            sqlstr = "insert into M_PRODUCT_REORDER_MAP(PRODUCTID, PRODUCTNAME, DEPOTID, DEPOTNAME, REORDERLAVEL)" +
                                     " values('" + Productid + "','" + Productname + "','" + dtProductMappingRecords.Rows[i]["DEPOTID"].ToString() + "','" + dtProductMappingRecords.Rows[i]["DEPOTNAME"].ToString() + "','" + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["REORDERLAVEL"]) + "')";
                            result = db.HandleData(sqlstr);
                        }
                    }
                    else
                    {
                        //sqlstr = "update M_TPU__PRODUCT_RATESHEET set VENDORID='" + VenId + "',VENDORNAME='" + VenName + "'  where ID= '" + ID + "'";
                        //result = db.HandleData(sqlstr);
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

        public DataTable BindTerritory()
        {
            //string sql = "SELECT DISTRICTID,DISTRICTNAME,'0' AS TERRITORYID FROM M_TERRITORYDETAILS UNION SELECT 14,'ASFFS','2' FROM M_TERRITORYDETAILS";
            string sql = "SELECT MENUID,MENUNAME,PARENTMENUID FROM (SELECT CAST(State_ID AS VARCHAR) AS MENUID,State_Name AS MENUNAME,'0' AS PARENTMENUID FROM [M_REGION] UNION ALL " +
                         "(SELECT 'D' + CAST(District_ID AS VARCHAR) AS MENUID,District_Name AS MENUNAME,CAST(State_ID AS VARCHAR) AS PARENTMENUID FROM [M_DISTRICT] GROUP BY District_ID,District_Name,State_ID)) AS A ORDER BY MENUNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int InsertTerritorywiseProduct(string pid, string menuid)
        {
            int result = 0;
            string sql = "INSERT INTO M_PRODUCT_TERRITORY_MAP(PRODUCTID,MENUID) VALUES('" + pid + "','" + menuid + "')";
            result = db.HandleData(sql);
            return result;
        }

        public int DeleteTerritorywiseProduct(string pid)
        {
            int result = 0;
            string sql = "DELETE FROM M_PRODUCT_TERRITORY_MAP WHERE PRODUCTID='" + pid + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable PopulateProductMenu(string pid)
        {
            string sql = "SELECT PRODUCTID,MENUID FROM M_PRODUCT_TERRITORY_MAP WHERE PRODUCTID='" + pid + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public string DivCode(string DivID)
        {
            string sql = string.Empty;
            string DivCode = string.Empty;
            try
            {
                sql = "SELECT DIVCODE FROM M_DIVISION WHERE DIVID='" + DivID + "'";
                DivCode = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return DivCode;
        }
        public string CatCode(string CatID)
        {
            string sql = string.Empty;
            string CatCode = string.Empty;
            try
            {
                sql = "SELECT CATCODE FROM M_CATEGORY WHERE CATID='" + CatID + "'";
                CatCode = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return CatCode;
        }
        public string FragCode(string FrgID)
        {
            string sql = string.Empty;
            string FraCode = string.Empty;
            try
            {
                sql = "SELECT FRGCODE FROM M_FRAGNANCE WHERE FRGID='" + FrgID + "'";
                FraCode = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return FraCode;
        }

        public string ProductCode(string DivID, string CatID, string FrgID, string MainGroup, string ItemType, string UnitValue)
        {
            string sql = string.Empty;
            string ProductCode = string.Empty;
            try
            {
                sql = "EXEC SP_M_GENERATEPRODUCTCODE '" + DivID + "','" + CatID + "','" + FrgID + "','" + MainGroup + "','" + ItemType + "','" + UnitValue + "',''";
                ProductCode = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return ProductCode;
        }

        public DataTable BindDistributor()
        {
            string sql = "SELECT USERID,USERNAME FROM DBO.VW_USERLOGININFO WHERE TPU='DI' ORDER BY USERNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SavedDistributorMapping(string ID, string xml, string selftag)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_PRODUCT_DISTRIBUTOR_MAPPING] '" + ID + "','" + xml + "','" + selftag + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable EditdISTRIBUTOR(string id)
        {
            string sql = "SELECT ID,NAME,DISTRIBUTORID,DISTRIBUTORNAME,SELFTAG FROM M_PRODUCT_DISTRIBUTOR_MAPPING WHERE ID='" + id + "'   ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // CHANGED BY SUBHODIP DE ON 17.07.2017
        public DataTable BindPrimaryitemtype()
        {
            //string sql = "SELECT ITEM_NAME+'~'+CAST(ID AS VARCHAR(5)) AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 ORDER BY ITEMDESC";
            string sql = "SELECT ITEMCODE+'~'+CAST(ID AS VARCHAR(50)) AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 UNION SELECT DIVCODE+'~'+DIVID,DIVNAME  FROM M_DIVISION WITH(NOLOCK)";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //END

        public DataTable BindSubitemtype(string PTYPEID)
        {
            string sql = "SELECT CAST(SUBTYPEID AS VARCHAR(50)) SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE CAST(PRIMARYITEMTYPEID AS VARCHAR(50))='"+ PTYPEID + "' AND ACTIVE='Y' UNION SELECT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='"+ PTYPEID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // CHANGED BY SUBHODIP DE ON 17.07.2017
        public int SaveMaterialMaster(string ID, string Code, string Name, string PtypeId, string PtypeName, string SubTypeId, string SubTypeName, string Uomid,
                                      string Uomname, string Unitvalue, decimal Minstooklevel, string Returnable, decimal MRP, string Mode, string active, string Type,
                                      decimal Assessablepercentage, string GTprintname, string MTprintname, string UserId)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC [USP_MATERIAL_MASTER_INSERT_UPDATE] '" + ID + "','" + Code + "','" + Name + "','" + PtypeId + "','" + PtypeName + "','" + SubTypeId + "','" + SubTypeName + "','" + Uomid + "','" + Uomname + "','" + Unitvalue + "','" + Minstooklevel + "','" + Returnable + "','" + MRP + "','" + Mode + "','" + active + "','" + Type + "','" + Assessablepercentage + "','" + GTprintname + "','" + MTprintname + "','" + UserId + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int SaveMaterialMaster_RMPM(string ID, string Code, string Name, string PtypeId, string PtypeName, string SubTypeId, string SubTypeName, string Uomid,
                                           string Uomname, string Unitvalue, decimal Minstooklevel, string Returnable, decimal MRP, string Mode, string active,
                                           string Type, decimal Assessablepercentage, string CBU, string DEPOTID, string FactoryMapID, string FactoryMap,
                                           string VendorMapID, string VendorMap, string UnitValue, string FromPackSizeID, string FromPackSize, string ToPackSizeID,
                                           string ToPackSize, string CustomerID, string CustomerName, string FactoryMapXML, string TpuXML, string CustomerXml, string ProductOwner,
                                           string typeid,string typename,string colorid,string colorname,string Brandid,string Sizeid, string STOREID)
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
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE CODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 1;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE NAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Code != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE CODE='" + Code + "' and ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 1;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_PRODUCT WHERE NAME='" + Name + "' AND ID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    sqlstr = "EXEC SP_MATERIALMASTER_SAVE '" + ID + "','" + Code + "','" + Name + "','" + PtypeId + "','" + PtypeName + "','" + SubTypeId + "','" + SubTypeName + "'," +
                             "'" + Uomid + "','" + Uomname + "'," + Unitvalue + "," + Minstooklevel + ",'" + Returnable + "'," + //
                             "'" + MRP + "','" + Mode + "','" + active + "','" + Type + "','" + Assessablepercentage + "','" + CBU + "','" + DEPOTID + "'," +
                             "'" + FactoryMapID + "','" + FactoryMap + "','" + VendorMapID + "','" + VendorMap + "'," +
                             "'" + UnitValue + "','" + FromPackSizeID + "','" + FromPackSize + "','" + ToPackSizeID + "','" + ToPackSize + "','" + CustomerID + "','" + CustomerName + "'," +
                             "'" + FactoryMapXML + "','" + TpuXML + "','" + CustomerXml + "','" + ProductOwner + "','"+ typeid + "','"+ typename + "','"+ colorid + "','"+ colorname + "','"+ Brandid + "','"+ Sizeid + "','"+STOREID+"'";
                    result = db.HandleData(sqlstr);
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

        public DataTable BindMaterialGrid_RMPM()
        {
            string sql = "EXEC USP_BINDPRODUCT_DEPOTWISE " + "''" + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMaterialGrid_RMPM(string DepotID)
        {
            string sql = "EXEC USP_BINDPRODUCT_DEPOTWISE '" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindMaterialMasterById_RMPM(string id)
        {            
            string sql = "EXEC USP_BIND_MATERIALMASTER '" + id + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable Depot_Accounts(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRANCHNAME FROM M_BRANCH AS A " +
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

        public DataTable BindMaterialGrid()
        {
            string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME,UOMID,UOMNAME,UNITVALUE, MRP, DTOC, CBU, STATUS, " +
                         " CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE,TYPE,ISNULL(ASSESSABLEPERCENT,0) AS ASSESSABLEPERCENT " +
                         " FROM [M_PRODUCT]" +
                         "  WHERE TYPE <>'FG'" +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadCustomer(string FactoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC USP_BIND_CUSTOMER_IN_MATERIALMASTER '" + FactoryID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }

        //END
        public DataTable BindVendorMasterGrid(string SUPLIEDID)
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR where TAG='T'  ORDER BY VENDORNAME";
            // AND SUPLIEDITEMID ='" + SUPLIEDID + "'
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // CHANGED BY SUBHODIP DE ON 17.07.2017
        public DataTable BindMaterialMasterById(string id)
        {
            string sql = " SELECT ID, CODE, NAME, TYPE+'~'+DIVID AS DIVID, DIVNAME, CATID,UOMID,UOMNAME,UNITVALUE, CATNAME,ASSESSABLEPERCENT,  MINSTOCKLEVEL, " +
                         " RETURNABLE, MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,GTPRINTNAME,MTPRINTNAME, " +
                         " CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE,TYPE,ISNULL(ASSESSABLEPERCENT,0) AS ASSESSABLEPERCENT" +
                         " FROM [M_PRODUCT] WHERE ID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //END
        public DataTable BindUOM()
        {
            string sql = "SELECT UOMID,UOMDESCRIPTION FROM M_UOM ORDER BY UOMDESCRIPTION  DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable bindprodctgrd(string DIVID)
        {
            if (DIVID == "0")
            {
                string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE FROM [M_PRODUCT]" +
                             " WHERE SETFLAG = 'P' OR SETFLAG = 'S' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            else
            {
                string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE FROM [M_PRODUCT]" +
                             " WHERE DIVID='" + DIVID + "' AND  SETFLAG = 'P' OR SETFLAG = 'S' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
        }
        public DataTable bindprodctgrdcat(string CATID)
        {
            if (CATID == "0")
            {
                string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE FROM [M_PRODUCT]" +
                             " WHERE SETFLAG = 'P' OR SETFLAG = 'S' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            else
            {
                string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE FROM [M_PRODUCT]" +
                             " WHERE CATID='" + CATID + "' AND  SETFLAG = 'P' OR SETFLAG = 'S' " +
                             " AND TYPE = 'FG'" +
                             " ORDER BY NAME,CODE";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
        }

        public DataTable BindFGProductByBrandCategory(string brand, string catagory)
        {
            string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME, NATUREID, NATURENAME, UOMID, UOMNAME, UNITVALUE, FRGID, FRGNAME,ASSESSABLEPERCENT, BESTBEFORE, MINSTOCKLEVEL, RETURNABLE,MRP, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='T' THEN 'Active' ELSE 'InActive' END AS ACTIVE " +
                         " FROM [M_PRODUCT]" +
                         " WHERE Active = 'T' AND DIVID='" + brand + "' AND CATID='" + catagory + "' " +
                         " AND TYPE = 'FG'" +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindActiveMaterial()
        {
            string sql = " SELECT ID, CODE, NAME,PRODUCTALIAS, DIVID, DIVNAME, CATID, CATNAME,UOMID,UOMNAME,UNITVALUE As Unit, MRP, DTOC, CBU, STATUS,TYPE FROM [M_PRODUCT]" +
                         " WHERE ACTIVE='T' " +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int UpdateLdomProductMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE M_PRODUCT SET LDTOM=GETDATE() WHERE ID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        #region Generate Templete
        public DataTable BindGenerateTemp(string storelocationid)
        {
            string sql = "exec [USP_GENRATE_TEMPLATE_OPENING_STOCK_KKG] '"+ storelocationid + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion 
        #region Generate Templete
        public DataTable BindGenerateTemp()
        {
            string sql = "exec [USP_GENRATE_TEMPLATE_OPENING_STOCK_KKG] ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindMaterialGridForExportExcel()
        {
            string sql = " SELECT ID, CODE, PRODUCTALIAS AS PRODUCT_NAME,TYPE " +
                         " FROM [M_PRODUCT]" +
                         " WHERE TYPE <>'FG'" +
                         " ORDER BY NAME,CODE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable DownloadProductList()
        {
            string sql = " SELECT A.CODE,DV.DIVNAME AS BRAND,CT.CATNAME AS CATEGORY,ISNULL(FRGNAME,'COMBI') VARIENT, " +
                         " (a.UNITVALUE + ' ' + a.UOMNAME) AS SKU, a.PRODUCTALIAS AS[PRODUCT ALIAS], " +
                         " CASE WHEN EXISTS(SELECT 1 FROM VW_SALEUNIT WHERE PSID = 'CE2F6F5B-8130-4B54-A0DC-C3D75FC6CF63' AND PRODUCTID = a.ID) THEN 288 " +
                         " ELSE ISNULL(D.CONVERSIONQTY,0)  END AS[PACK SIZE], " +
                         " ISNULL(a.MRP, 0) as MRP, " +
                         " CONVERT(VARCHAR(10), CAST(a.DTOC AS DATE), 103) AS[CREATION DATE], " +
                         " CONVERT(VARCHAR(10), CAST(a.LDTOM AS DATE), 103) AS[MODIFICATION DATE], " +
                         " ISNULL(STUFF((SELECT ',' + US.BUSINESSSEGMENTNAME " +
                         " FROM M_PRODUCT_BUSINESSSEGMENT_MAP US " +
                         " WHERE US.PRODUCTID = a.id " +
                         " FOR XML PATH('')), 1, 1, ''),'NA') AS[BUSINESS SEGMENTS], " +
                         " CASE WHEN a.ACTIVE = 'T' THEN 'Active' ELSE 'In-Active' end as STATUS " +
                         " FROM M_PRODUCT a INNER JOIN M_DIVISION AS DV " +
                         " ON A.DIVID = DV.DIVID INNER JOIN M_CATEGORY CT " +
                         " ON A.CATID = CT.CATID INNER JOIN VW_SALEUNIT D " +
                         " ON a.ID = D.PRODUCTID " +
                         " WHERE a.TYPE = 'FG' " +
                         " AND DV.DIVID <> '6B869907-14E0-44F6-9BA5-57F4DA726686' " +
                         " AND D.UOMID IS NOT NULL " +
                         " ORDER BY ct.SEQUENCENO,dv.DIVNAME,CAST(a.UNITVALUE AS DECIMAL(18, 2)),a.PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PacksizeValidation(string ProductId)
        {
            string sql = " EXEC USP_CASE_PACK_VALIDATION '" + ProductId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotType()
        {
            string sql = " SELECT BILLINGDEPOTVALUE,BILLINGDEPOTTYPE " +
                         " FROM M_BILLING_DEPOT_TYPE " +
                         " WHERE ACTIVE='Y' " +
                         " ORDER BY BILLINGDEPOTTYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot(string ProductID, string DepotID, string Mode)
        {
            string sql = "EXEC USP_DEPOT_TYPE '" + ProductID + "','" + DepotID + "','" + Mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductList(string type)
        {
            string sql = "EXEC [USP_RPT_BIND_PRODUCT_DETAILS] '" + type + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveDepotMapping(string ProductID, string xml)
        {
            int flag = 0;
            string sql = string.Empty;
            try
            {
                sql = "EXEC [USP_DEPOT_MAPPING_INSERT] '" + ProductID + "','" + xml + "'";
                flag = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }
        public DataTable RptBindMaterialGrid(string DepotID)
        {
            string sql = "EXEC USP_RPT_BINDMATERIAL '" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSuplieditem()
        {
            string sql = " SELECT ID,ITEM_Name FROM M_SUPLIEDITEM";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUVendortList(string type)
        {
            string sql = "EXEC [USP_LOAD_TPU_VENDOR_MASTER] '" + type + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable bindtypr()
        {
            string sql = " SELECT ITID,ITNAME FROM M_ITEMTYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable bindbrand()
        {
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable bindsize()
        {
            string sql = "SELECT ID,ITEM_NAME FROM M_SIZE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable bindColor()
        {
            string sql = " SELECT FRGID,FRGNAME FROM M_FRAGNANCE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable DownloadOpenningProductList()
        {
            string sql = "EXEC [UPS_OPENNING_PRODUCT_LIST]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string MaterialProductCode(string PRIMARYTYPE, string SUBTYPE, string TYPE,string COLOR,string BRAND,string SIZE)
        {
            string sql = string.Empty;
            string ProductCode = string.Empty;
            try
            {
                sql = "EXEC [SP_M_GENERATE_MATERIAL_PRODUCTCODE] '" + PRIMARYTYPE + "','" + SUBTYPE + "','" + TYPE + "','"+ COLOR + "','"+ BRAND + "','"+ SIZE + "'";
                ProductCode = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return ProductCode;
        }

        public string ProducttranCheck(string Productid)
        {
            string sql = string.Empty;
            string Status = string.Empty;
            try
            {
                sql = "EXEC [USP_PRODUCT_TRANS_CHECK] '" + Productid + "'";
                Status = Convert.ToString(db.GetSingleValue(sql));
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return Status;
        }

        public DataTable SaveUploadProduct(string xml, string UserID, string IPAddress)
        {
            DataTable flag = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "EXEC [USP_INSERT_UPLOAD_PRODUCT] '" + xml + "','" + UserID + "','" + IPAddress + "'";
                flag = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }

        public DataTable fetchDetailsFromSubItem(string id)
        {
            DataTable flag = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "EXEC [USP_FETCH_SUBITEMDETAILS] '" + id + "'";
                flag = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }

        public DataTable BindNatureonlyfg()/*NEW ADD BY P.BASU  ONLY FOR FG AS PER SUBHENDHU DA ON 16-10-2021*/
        {
            string sql = " SELECT NOPID, NOPNAME FROM M_NATUREOFPRODUCT WHERE NOPNAME ='FG' ORDER BY NOPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
