using System;
using System.Data;
using System.Web;
using DAL;
using Utility;

namespace BAL
{
    public class ClsFactoryReport
    {
        DBUtils db = new DBUtils();
        public DataTable BindCategory_SupliedItem(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindItemLedger(string fromdate, string todate, string depotid, string productid, string storelocation)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_Ledger_Factory] '" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + storelocation + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string catid, string DIVID, string Depotid)
        {
            string sql = "EXEC [USP_LOAD_PRODUCT_ITEM_LEDGER] '"+ catid + "','"+ DIVID + "','"+ Depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Get Tpu Party Name
        public DataTable BindTPUPatyName()
        {
            string Sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE TAG='T' AND SUPLIEDITEMID NOT IN('8','9','10')";
            DataTable dt = new DataTable();
            dt = db.GetData(Sql);
            return dt;
        }
        #endregion

        public DataSet BindSummaryDetails_Factory(string fromdate, string todate, string depotid, string partyid, string FinYear)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_REGISTER_FACTORY '" + fromdate + "','" + todate + "','" + depotid + "','" + partyid + "','" + FinYear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable GetBatchDetails(string fromdate, string todate, string Type, string Product, string Batchno, string FinYear, string DepotID)
        {
            string sql = "EXEC [USP_RPT_MM_BATCHWISE] '" + fromdate + "','" + todate + "','" + Type + "','" + Product + "','" + Batchno + "','" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchDateRange(string span, string Tag, string FinYear)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                sql = " [USP_FETCH_DATERANGE_FAC] '" + span + "','" + Tag + "','" + FinYear + "'";
            }
            else
            {
                sql = " [USP_FETCH_DATERANGE_FAC] '" + span + "','" + Tag + "','" + FinYear + "'";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryItemwiseProduction(string format, string span, string fromdate, string todate, string productid, string tpuid, string showby, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_FACTORY_ITEMWISE_PRODUCTION] '" + format + "','" + span + "','" + fromdate + "','" + todate + "','" + productid + "','" + tpuid + "','" + showby + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryTypewise_Item_Framework_BOM_Details(string frameworkid, string ProcessID, string depotid) /*new parameter add by p.basu*/
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MM_BOM_REPORT] '" + frameworkid + "','" + ProcessID + "','" + depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactoryTypewise_Item_Framework(string itemid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MM_LOAD_BOM_FRAMEWORK] '" + itemid + "' ";
            dt = db.GetData(sql);
            return dt;
        }


        public DataSet BindFactoryTypewise_Item_Framework1(string itemid)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC [USP_RPT_MM_LOAD_BOM_FRAMEWORK] '" + itemid + "' ";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindStockinHandDetails(string depotid)
        {
            string sql = "EXEC [sp_Rpt_Stock] '" + depotid + "',''";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindTradingSaleInvoice(string fromdate, string todate, string depotid, string FinYear)
        {
            string sql = "EXEC USP_RPT_TREADING_SALE_INVOICE '" + fromdate + "','" + todate + "','" + depotid + "','" + FinYear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindDespatchReport(string fromdate, string todate, string Fromdepotid, string Todepotid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_DESPATCH_DETAILS_RMPM] '" + fromdate + "','" + todate + "','" + Fromdepotid + "','" + Todepotid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #region Job Order Report by HPDAS on 29.01.2019
        public DataSet BindJobOrder(string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_JOB_ORDER_FACTORY] '" + fromdate + "','" + todate + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindJobOrderV1(string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_JOB_ORDER_FACTORY_V1] '" + fromdate + "','" + todate + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public string GetStartDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));

            return STARTDATE;
        }
        #endregion

        #region Added by HPDAS
        public DataTable BindStockReceiptReport(string FromDate, string ToDate, string DepotId, string PartyID, string ProductID, string BrandID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIPT_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + PartyID + "','" + ProductID + "','" + BrandID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTpu_DepotBrandwise(string brandid, string DepotId)
        {
            string sql = "EXEC [USP_BIND_VENDOR_BRANDWISE] '" + brandid + "','" + DepotId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory_Brandwise(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY_MULTI '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductMultiple(string catid, string DIVID)
        {
            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO,A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A " +
                         " LEFT JOIN M_CATEGORY AS B ON A.CATID= B.CATID" +
                         " WHERE A.CATID IN (SELECT * FROM DBO.fnSplit('" + catid + "',','))" +
                         " AND A.DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',','))" +
                         " AND A.ACTIVE='T'" +
                         " ORDER BY B.SEQUENCENO,A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchInvoiceId(string ACCENTRYID)
        {
            string sql = " EXEC USP_FETCHINVOICE '" + ACCENTRYID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataSet BindWIPReport(string FromDate, string ToDate, string DepotID, string ProductID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_WIP] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + ProductID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindReturn_WastageReport(string FromDate, string ToDate, string DepotID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_RETURN_WASTAGE] '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindProductionLedger(string FromDate, string ToDate, string DepotID, string ProductID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_PRODUCTION_LEDGER] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + ProductID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #region Added for Stock Valuation Report by HPDAS
        public DataTable BindStockValuationReportCategorywise(string DepotId, string AsOnDate, string category)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_VALUATION_CATEGORYWISE_FAC] '" + DepotId + "','" + AsOnDate + "','" + category + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        /*Added By sayan dey on 16/02/2018*/
        #region category
        public DataTable BindCategoryforfactory(string divid, string mode)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [usp_bind_catgory_for_factory]  '" + divid + "','" + mode + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Added for Stock Valuation Report by HPDAS
        public DataTable BindStockValuationReportProductwise(string DepotId, string CategoryId, string AsOnDate)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_VALUATION_PRODUCTWISE_FAC] '" + DepotId + "','" + CategoryId + "','" + AsOnDate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindBatchNo(string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT BATCHNO FROM T_PRODUCTION_ORDER_HEADER WHERE PRODUCTID='" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #region Added for Stock valuation report by HPDAS
        public DataTable BindCategory()
        {
            //string sql = "SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY UNION ALL SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE ";
            /*string sql = " SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY  WHERE CATCODE NOT IN('201','190','20','230','210','60')" +//Add by Rajeev (24-03-2018)
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2')";*/
            string sql = " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY A  " +
                         " INNER JOIN  M_PRODUCT AS B ON A.CATID = B.CATID " +
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockValuationReportMonthwise(string AsOnDate, string DepotId, string ProductId, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_VALUATION_MONTHWISE_FAC] '" + AsOnDate + "','" + DepotId + "','" + ProductId + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockValuationReportDatewise(string StartDate, string EndDate, string DepotId, string ProductId, string FinYear, string MonthID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_VALUATION_DATEWISE_FAC] '" + StartDate + "','" + EndDate + "','" + DepotId + "','" + ProductId + "','" + FinYear + "','" + MonthID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductALIAS()
        {
            //string sql = "SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT ORDER BY NAME";
            string sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT" +//Add by Rajeev (24-03-2018)
                         " WHERE DIVID NOT IN('11','6','13','7')" +
                         " AND ACTIVE='T'" +
                         " ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        public DataTable BindBatchNo(string ProductID, string FromDT, string ToDT, string DepotID, string ProductType)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_RPT_LOAD_BATCH'" + ProductID + "','" + FromDT + "','" + ToDT + "','" + DepotID + "','" + ProductType + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #region  ADDED BY HPDAS
        public DataTable BindCategory_Brandwise_All_Fac()
        {
            string sql = "EXEC USP_MM_LOADCATEGORY_ALL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct_BrandCategorywise_All_Fac()
        {
            string sql = "EXEC USP_MM_LOADPRODUCT_ALL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Added by Dhananjoy
        public DataTable GetSFGProduct(string DepotId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GET_SFG_PRODUCT] '" + DepotId + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLastBatchReport(string DepotId, string ProductID, string FromDate, string ToDate, string Status)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_LAST_BATCH_REPORT] '" + DepotId + "','" + ProductID + "','" + FromDate + "','" + ToDate + "','" + Status + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        /*Added By H.P.Das On 16.02.2019*/
        public DataTable BindDepotwiseSalesReport_FAC(string FromDate, string ToDate, string depotid, string BSId, string Partyid, string SearchTag, string TSID, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE_FACTORY] '" + FromDate + "','" + ToDate + "','" + depotid + "','" + BSId + "','" + Partyid + "','" + SearchTag + "','" + TSID + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTimeSpan(string Tag)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME DESC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Region()
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql = "Select [BRID],[BRNAME] from [M_BRANCH] order by BRNAME";
                string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O'  UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE'  ORDER BY SEQUENCE,BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public string GetUsertype(string USERID)
        {
            string Usertype = string.Empty;
            string sql = "SELECT USERTYPE FROM M_USER WHERE USERID='" + USERID + "'";
            Usertype = Convert.ToString(db.GetSingleValue(sql));
            return Usertype;
        }
        public DataTable BindCustomerbyDepot(string DepotID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyDepotAndASMHQ(string DepotID, string UserID) /*SUNNY*/
        {
            string sql = "  SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID INNER JOIN M_CUSTOMER_HQ_MAPPING HQ ON HQ.CUSTOMERID=A.CUSTOMERID " +
                         "  WHERE A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + UserID + "'),','))" +
                         "  AND B.DEPOTID IN(SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',','))  AND HQ.HQID IN( select *  from dbo.fnSplit((select HQLIST from M_USER WHERE USERID='" + UserID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindConsumptionDetails(string fromdate, string todate, string Type, string Product, string FinYear, string DepotID)
        {
            string sql = "EXEC [USP_RPT_CONSUMPTION] '" + fromdate + "','" + todate + "','" + Type + "','" + Product + "','" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUse_RMBULK(string fromdate, string todate, string Type, string Product, string FinYear, string DepotID)
        {
            string sql = "EXEC [USP_USE_RM_BULK] '" + fromdate + "','" + todate + "','" + Type + "','" + Product + "','" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInHouseBulk(string Type, string DepotID)
        {
            string sql = " EXEC USP_BIND_VARIANT '" + Type + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable UseBulkDetails(string fromdate, string todate, string ProductID, string FinYear, string DepotID)
        {
            string sql = " EXEC USP_BIND_USEBULK_DETAILS '" + fromdate + "','" + todate + "','" + ProductID + "','" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Inventory_StockDetails(string depotid, string productid, string packsize, string batchno, string brand, string category, string ExpFromdate, string ExpTodate, string FromDt, string curdate, string size, string storelocation)
        {
            decimal mrp = 0;
            string sql = "EXEC USP_RPT_INVENTORY_STOCK '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "'," + mrp + ",'" + brand + "','" + category + "','" + ExpFromdate + "','" + ExpTodate + "','" + FromDt + "','" + curdate + "','" + size + "','" + storelocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Riya SubItem Details
        public DataTable BindRiyaSubItemsDetails(string DepotId, string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RIYA_USE_SUBITEM] '" + DepotId + "','" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Riya Ason Stock SubItem Wise
        public DataTable BindRiyaAsonStockSubItemsWise(string DepotId, string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RIYA_STOCK_SUBITEMWISE] '" + DepotId + "','" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable Bind_Requistionno(string FinYear, string Depotid, string FromDate, string ToDate)
        {
            string sql = "EXEC BIND_REQUISTIONNO '" + FinYear + "','" + Depotid + "','" + FromDate + "','" + ToDate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_Requistion_VS_Issue(string RequistionID, string DepotID, string FinYear, string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [BIND_REQUISTION_VS_ISSUE] '" + RequistionID + "','" + DepotID + "','" + FinYear + "','" + FromDate + "','" + ToDate + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet Bind_Requistion_VS_Issue_Details(string RequistionID, string DepotID, string FinYear, string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [BIND_REQUISTION_VS_ISSUE_DETAILS] '" + RequistionID + "','" + DepotID + "','" + FinYear + "','" + FromDate + "','" + ToDate + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindInventory_Consumption(string FromDate, string ToDate, string DepotID, string ProductID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_INVENTORY_CONSUMPTION] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + ProductID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindPoWiseTpu(string factoryid)
        {
            string sql = "SELECT DISTINCT VENDORID,VENDORNAME FROM T_MM_POHEADER WHERE ISWORKORDER='Y' AND FACTORYID='" + factoryid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBrand_Subitem()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT SUBTYPEID AS CATID,SUBITEMNAME AS CATNAME FROM M_PRIMARY_SUB_ITEM_TYPE ";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable LoadProduct()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_LOAD_PRODUCT_LEDGER] ";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSale_Product(string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_BIND_SALE_PRODUCT] '" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAll_SaleDtls(string FromDate, string ToDate, string ProductID, string DepotID, string SaleType)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ALL_SALE_DTLS] '" + FromDate + "','" + ToDate + "','" + ProductID + "','" + DepotID + "','" + SaleType + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_SaleDump(string FromDate, string ToDate, string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_SALES_DUMP] '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }
    }
}
