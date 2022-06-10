using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsCommonFunction
    {
        DBUtils db = new DBUtils();
       

        #region Bind Reason
        public DataTable BindReason(string MenuID)
        {
            string sql = " SELECT A.ID,A.NAME AS DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON" +
                         " A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N' AND B.MENUID='" + MenuID + "' AND A.STORELOCATIONID <> '0' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Vendor
        public DataTable BindVendor()
        {
            string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEM <>'FG'  AND TAG ='T'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Load Ledger For Grn Approval Page
        public DataTable LoadLedger()
        {
            string sql = " DECLARE @P_APPLEDGER VARCHAR(350)" +
                         " SELECT @P_APPLEDGER= [LEDGER-MM-GRN] FROM P_APPMASTER " +
                         " SELECT ID,NAME FROM ACC_ACCOUNTSINFO " +
                         " WHERE ID IN ( SELECT * FROM DBO.FNSPLIT(@P_APPLEDGER,',')) ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind PackingSize Product And Brand Wise
        public DataTable BindPackingSize(string PID, string DIVID)
        {
            string sql = " IF EXISTS(SELECT 1 FROM  M_PRIMARY_SUB_ITEM_TYPE WHERE CAST (PRIMARYITEMTYPEID AS VARCHAR(50))='" + DIVID + "') " +
                         " BEGIN " +
                         " SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_MMUNIT] WHERE PRODUCTID='" + PID + "'" +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "'" +
                         " END";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind PackingSize Product Wise
        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_MMUNIT] WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Factory User Wise
        public DataTable BindFactory(string UserID)
        {
            string sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE VENDORID IN (SELECT TPUID FROM M_TPU_USER_MAPPING WHERE USERID ='" + UserID + "' AND TAG='F')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindFactory()
        {
            string sql = " SELECT VENDORID AS FACTORYID,VENDORNAME AS VENDORNAME" +
                         " FROM M_TPU_VENDOR" +
                         " WHERE TAG= 'F'" +
                         " ORDER BY VENDORNAME DESC";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        #region Bind Product
        public DataTable BindProductALIAS()
        {
            string sql = "SELECT DISTINCT ID, CASE WHEN PRODUCTALIAS IS NULL THEN NAME ELSE PRODUCTALIAS END AS NAME FROM M_PRODUCT WHERE ACTIVE='T' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind StoreLocation
        public DataTable BindStoreLocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Brand_SupliedItem
        public DataTable BindBrand_SupliedItem()
        {
            string sql = "EXEC USP_MM_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Brand_SupliedItem
        public DataTable BindBrand_NotinFG()
        {
            string sql = "SELECT CAST(ID AS VARCHAR(5)) AS DIVID, ITEMDESC AS DIVNAME,0 AS SEQUENCE FROM M_SUPLIEDITEM WHERE ID NOT IN(1,4,9)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Riya_FGProduct
        public DataTable Bind_RiyaFG_Product()
        {
            string sql = "SELECT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE DIVID='107CBAAA-10B4-406B-B5B9-4C48A77330D9' AND TYPE='FG' ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind FGProduct
        public DataTable BindFGProduct()
        {
            string sql = " SELECT A.ID,A.NAME AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A LEFT JOIN M_CATEGORY AS B " +
                         " ON A.CATID= B.CATID" +
                         " WHERE A.TYPE='FG' " +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind DeptName
        public DataTable BindDeptName(string BRID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'AND BRID='" + BRID + "' order by BRNAME";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Depot
        public DataTable BindDepot()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion       

        #region Bind JC
        public DataTable BindJC()
        {
            string sql = " SELECT JCID,NAME FROM M_JC ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind TimeSpan
        public DataTable BindTimeSpan(string Tag, string FinYear)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "' AND FINYEAR='" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion        

        #region Bind Usertype
        public DataTable BindUsertype()
        {
            string sql = "SELECT UTID,UTNAME FROM DBO.M_USERTYPE ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind PackSize
        public DataTable BindPackSize()
        {
            string sql = " SELECT PACKSIZE AS PSID,PACKSIZENAME AS PSNAME FROM P_APPMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Division
        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID, DIVNAME, DIVCODE FROM M_DIVISION ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Category
        public DataTable BindCategory()
        {
            string sql = "SELECT CATID, CATNAME, CATCODE FROM M_CATEGORY ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind UOM
        public DataTable BindUOM()
        {
            string sql = "SELECT PSID, PSNAME FROM M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Supplieditem
        public DataTable BindSupplieditem()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT ID,ITEM_NAME FROM M_SUPLIEDITEM ORDER BY ITEM_NAME";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Primaryitemtype
        public DataTable BindPrimaryitemtype()
        {
            string sql = "SELECT ITEM_NAME+'~'+CAST(ID AS VARCHAR(5)) AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Subitemtype
        public DataTable BindSubitemtype(string PTYPEID)
        {
            string sql = "SELECT SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID='" + PTYPEID + "' AND ACTIVE='Y' ORDER BY SUBITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Insurancename
        public DataTable BindInsurancename()
        {
            string sql = "SELECT ID,COMPANY_NAME FROM M_INSURANCECOMPANY ORDER BY COMPANY_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Category_SupliedItem Wise
        public DataTable BindCategory_SupliedItem(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable Depot_Accounts(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "' ORDER BY BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindDepot_Primary()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryTypewise_Item(string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MM_PRODUCT_TYPEWISE] '" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string GetStartDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));
            return STARTDATE;
        }

        #region Load MenuName For Back Date Entry Settings
        public DataTable LoadMenu()
        {
            string sql = "EXEC USP_GET_MENUNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Load Back Date Entry Settings
        public DataTable LoadListOfDateSettings()
        {
            string sql = "SELECT MENUID,B.DESCRIPTION AS MENUNAME,CONVERT(VARCHAR,ALLOWDATE,103) AS ALLOWDATE " +
                         "FROM M_BACKENTRYDATE_SETTING AS A " +
                         "INNER JOIN TBLMENULIST1 AS B ON A.MENUID=B.ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Save Back Date Entry Setting
        public int SaveDateSetting(string MENUID, string ALLOWDATE)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                sqlstr = "EXEC USP_BACKENTRYDATE_SETTING '" + MENUID + "','" + ALLOWDATE + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }
            return result;
        }
        #endregion

        #region Delete Bck Date Setting
        public int DeletDateSetting(string MENUID)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                sqlstr = "DELETE M_BACKENTRYDATE_SETTING WHERE MENUID='" + MENUID + "' ";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }
            return result;
        }
        #endregion

        public int CheckDate(string MENUID)
        {
            string sql = string.Empty;
            int flag = 0;
            sql = "SELECT COUNT(*) FROM M_BACKENTRYDATE_SETTING WHERE ALLOWDATE=CONVERT(DATE, GETDATE(), 103) AND MENUID='" + MENUID + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
        }

        //public DataTable BindDepotBasedOnUser(string UserID)
        //{
        //    string sql = " EXEC USP_GET_DEPOT_BASE_ON_USER '" + UserID + "'";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        #region Bind Gross Weight
        public DataTable BindGrossWt(string FactoryID, string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_BIND_GROSSWEIGHT] '" + FactoryID + "','" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Update Gross Weight
        public int UpdateGrossWt(string ProductID, string GrossWt)
        {
            DataTable dt = new DataTable();
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE M_PRODUCT_UOM_MAP SET GROSSWEIGHT='" + GrossWt + "' WHERE PRODUCTID= '" + ProductID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        #endregion

        #region Bind FG SFG Brand
        public DataTable BindFG_SFGBrand()
        {
            string sql = " EXEC USP_BIND_FG_SFGBRAND ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind FG SFG Brand
        public DataTable BindFG_SFGCategory()
        {
            string sql = "USP_BIND_FG_SFGCATEGORY";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region BindProduct CategoryWise
        public DataTable BindProduct_CategoryWise(string FactoryID, string BrandID,string CatID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_MAP_PRODUCT_FOR_PRODUCTION] '" + FactoryID + "','" + BrandID + "','" + CatID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region SaveProductMap_ForProduction
        public int SaveProductMap_ForProduction(string BranchID, string BrandID, string BrandName, string CatID, string CatName, string xml)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC CRUD_PRODUCTMAP_FOR_PRODUCTION '" + BranchID + "','" + BrandID + "','" + BrandName + "','" + CatID + "','" + CatName + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        #region BindProduct CategoryWise
        public DataSet EditProductMap_ForProduction(string BrandID, string CatID, string DepotID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [EDIT_MAP_PRODUCT_FOR_PRODUCTION] '" + BrandID + "','" + CatID + "','" + DepotID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion
    }
}