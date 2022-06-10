using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsStockReport
    {
        DBUtils db = new DBUtils();
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

        public DataTable BindDepot_Primary()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotIncludeExport()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH   ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributor(String CUSTOMERID)
        {
            if (CUSTOMERID == "8")
            {
                string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID='5E24E686-C9F4-4477-B84A-E4639D025135'  ORDER BY CUSTOMERNAME";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            else
            {
                string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + CUSTOMERID + "' ORDER BY CUSTOMERNAME";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
        }

        public DataTable Region(string UserType, string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    sql = " SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                          " WHERE  BRANCHTAG = 'O' UNION " +
                          " SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                          " WHERE BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                          " WHERE BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME";
                }
                else
                {
                    sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "' ORDER BY BRNAME";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataTable BindDepot()
        {
            //string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE  BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            string sql = " SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O' UNION " +
                         " SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot(string BRID)
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID='" + BRID + "' AND BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreLocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGridForDepot(string DistID, string PackSizeID, string StoreID)
        {
            string sql = "EXEC [USP_RPT_DEPOT_STOCK] '" + DistID + "','" + PackSizeID + "','" + StoreID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStkExpiryGrid(string DistID, string PackSizeID, string StoreID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_DEPOT_EXPIRY_STOCK_ERP] '" + DistID + "','" + PackSizeID + "','" + StoreID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment(string BSID)
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + BSID + "'),','))";
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
        public DataTable BindCustomer(string ID)
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + ID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroup(string ID)
        {
            string sql = "SELECT DISTINCT GROUPID,GROUPNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID='" + ID + "' ORDER BY GROUPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindType()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE DISTRIBUTIONCHANNEL='Y' " +
                         "UNION ALL " +
                         "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN('9BF42AA9-0734-4A6A-B835-0885FBCF26F5','DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB') ORDER BY UTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTypeDetails(string ID)
        {
            string sql = "SELECT CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END USERID,USERNAME FROM M_USER WHERE USERTYPE='" + ID + "' ORDER BY USERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand()
        {
            /*string sql = " SELECT DISTINCT DIVID,DIVNAME FROM M_PRODUCT " +
                         " WHERE DIVID NOT IN('6B869907-14E0-44F6-9BA5-57F4DA726686')" +
                         " ORDER BY DIVNAME";*/
            string sql = " SELECT CAST(ID AS VARCHAR(5)) AS DIVID,ITEMDESC AS DIVNAME" +
                         " FROM M_SUPLIEDITEM WHERE ID NOT IN(1,9,10)" +
                         " UNION ALL" +
                         " SELECT DIVID AS DIVID,DIVNAME AS DIVNAME FROM M_DIVISION " +
                         " WHERE DIVID NOT IN('6B869907-14E0-44F6-9BA5-57F4DA726686')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBrand(string typeid)
        {
            string sql = "SELECT DISTINCT DIVID,DIVNAME FROM M_PRODUCT  WHERE TYPEID='" + typeid + "' ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindItemtype()
        {
            string sql = "SELECT DISTINCT TYPEID,TYPENAME FROM M_PRODUCT ORDER BY TYPENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY UNION ALL SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID='4'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='" + brandid + "' AND ACTIVE='T' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNature()
        {
            string sql = "SELECT DISTINCT NATUREID,NATURENAME FROM M_PRODUCT ORDER BY NATURENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPacksize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct(string DepotID)
        {
            string sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME " +
                         " FROM M_PRODUCT AS A " +
                         " INNER JOIN M_PRODUCT_TPU_MAP AS B ON B.PRODUCTID=A.ID " +
                         " WHERE B.VENDORID='" + DepotID + "'" +
                         " AND A.ACTIVE='T'" +
                         " ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductByFiltration(string itemtypeid, string brndid, string catid, string fragranceid)
        {
            string Sql = string.Empty;
            string STRSQL = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE ACTIVE='T'";
            string STRORDERBY = "ORDER BY NAME";
            DataTable dt = new DataTable();

            string TYPEID = "";

            if (itemtypeid != "")
            {
                TYPEID = " AND TYPEID='" + itemtypeid + "'";
            }

            string DIVID = "";

            if (brndid != "")
            {
                DIVID = " AND DIVID='" + brndid + "'";
            }

            string CATID = "";

            if (catid != "")
            {
                CATID = " AND CATID='" + catid + "'";
            }

            string FRGID = "";

            if (fragranceid != "")
            {
                FRGID = " AND FRGID='" + fragranceid + "'";
            }

            Sql = STRSQL + TYPEID + DIVID + CATID + FRGID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;
        }
        public DataTable DepotTransitDetailsReport(string FromDate, string ToDate, string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC SP_RPT_TRANSIT_DEPOT_DETAILS '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet PurchaseReportERP(string FromDate, string ToDate, string DepotID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_RPT_PURCHASE_INVOICE_DETAILS_ERP '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindprogressReportDetails(string fromdate, string todate, string brandid, string catid, string depot)
        {
            string sql = "EXEC USP_RPT_STOCK_MOVEMENT '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategorybybrand(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID IN('" + brandid + "') AND ACTIVE='T' ORDER BY CATNAME";
            //string sql = "SELECT SUBTYPEID AS CATID,SUBITEMNAME AS CATNAME FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('" + brandid + "') ORDER BY SUBITEMNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockReceipt(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_RECEIPT_REGISTER '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockReceipt_Details(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_RECEIPT_REGISTER_DETAILS '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindState()
        {
            string sql = "SELECT STATE_ID,STATE_NAME FROM M_REGION  ORDER BY STATE_NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbybrand(string divid, string catid)
        {
            string sql = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE DIVID IN('" + divid + "') AND CATID IN('" + catid + "') AND ACTIVE='T' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindType(string BSID, string GRPID)
        {
            string sql = "SELECT DISTINCT CHANNELID,CHANNELNAME FROM VW_MARGIN WHERE BSID='" + BSID + "' AND GROUPID='" + GRPID + "' ORDER BY CHANNELNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDeptName(string BRID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'AND BRID='" + BRID + "' ORDER BY BRNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockTransfer(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_TRANSFER_REGISTER_FACTORY '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockTransferDetails(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_TRANSFER_REGISTER_DETAILS_FACTORY '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSummaryDetails_Factory(string fromdate, string todate, string depotid, string partyid)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_REGISTER_FACTORY '" + fromdate + "','" + todate + "','" + depotid + "','" + partyid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindVatStatutory(string fromdate, string todate, string depotid, string bsid, string groupid)
        {
            string sql = "EXEC USP_RPT_VAT_STATUTORY_REPORT '" + fromdate + "','" + todate + "','" + depotid + "','" + bsid + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindStockinHandDetails(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string StockDtls, string MrpDtls, string Finyear, string ProductOwner, string storelocation)
        {
            string sql = "EXEC USP_RPT_DEPOTWISE_STOCK_IN_HAND_FAC '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + StockDtls + "','" + MrpDtls + "','" + Finyear + "','" + ProductOwner + "','" + storelocation + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable GetBatch(string PRODUCTID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT DISTINCT BATCHNO,PRODUCTID FROM T_TPU_PRODUCTION_DETAILS WHERE PRODUCTID='" + PRODUCTID + "'" +
                         " UNION" +
                         " SELECT DISTINCT BATCHNO,PRODUCTID FROM T_BATCHWISEAVAILABLE_STOCK WHERE PRODUCTID='" + PRODUCTID + "'" +
                         " UNION" +
                         " SELECT DISTINCT BATCHNO,PRODUCTID FROM T_STOCKRECEIVED_DETAILS WHERE PRODUCTID='" + PRODUCTID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindDespatchcfrom(string fromdate, string todate, string depotid, string status)
        {
            string sql = "EXEC USP_RPT_DESPATCH_C_FORM '" + fromdate + "','" + todate + "','" + depotid + "','" + status + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindStockReceiptFfrom(string fromdate, string todate, string depotid, string STATUS)
        {
            string sql = "EXEC [USP_RPT_STOCK_RECEIPT_F_FORM] '" + fromdate + "','" + todate + "','" + depotid + "','" + STATUS + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindProductbyBSegment(string divid, string catid, string BSID)
        {
            string STRSQL = string.Empty;
            string STRSQL_CHECK = string.Empty;
            string Sql = string.Empty;
            string str = string.Empty;
            DataTable dt = new DataTable();
            //if (BSID != "0")
            //{
            //    sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID" +
            //                " WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "')" +
            //                " AND BUSNESSSEGMENTID='" + BSID + "'";
            //}
            //else
            //{
            //    sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID" +
            //              " WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "')";
            //}

            //DataTable dt = new DataTable();
            //dt = db.GetData(sql);
            //return dt;
            string BS_SEGMENT = "";
            if (BSID == "0")
            {
                STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE ACTIVE='T'";
            }
            else
            {
                STRSQL_CHECK = "SELECT TOP 1 NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "') AND M_PRODUCT.ACTIVE='T'";

                str = (string)db.GetSingleValue(STRSQL_CHECK);

                if (str == "" || str == null)
                {
                    STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE 1=1  ";
                }
                else
                {
                    STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE 1=1 ";

                    if (BSID != "0")
                    {
                        BS_SEGMENT = " AND BUSNESSSEGMENTID IN ('" + BSID + "')";
                    }
                }
            }
            string STRORDERBY = "ORDER BY NAME";

            string DIVID = "";
            if (divid != "0")
            {
                if (BSID != "0")
                {

                    DIVID = " AND DIVID IN('" + divid + "')";
                }
            }
            string CATID = "";
            if (catid != "0")
            {
                if (BSID != "0")
                {

                    CATID = " AND M_PRODUCT.CATID IN('" + catid + "')";
                }
            }
            Sql = STRSQL + BS_SEGMENT + DIVID + CATID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;
        }

        public DataSet BindSalesTaxSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_TAX_SUMMARY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindVoucherTypes()
        {
            string sql = " SELECT Id,VoucherName FROM Acc_VoucherTypes  ORDER BY VoucherName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSizeInPack()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID IN('1970C78A-D062-4FE9-85C2-3E12490463AF','B9F29D12-DE94-40F1-A668-C79BF1BF4425','71B973E8-28E3-4F3A-A86E-9475AF2D14EE')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClosingStock(string month, string year, string depot)
        {
            string sql = "EXEC [USP_RPT_CLOSING_STOCK_TAKE] '" + month + "','" + year + "','" + depot + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerforAdmin()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID='5E24E686-C9F4-4477-B84A-E4639D025135' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerClosingStock(string customerid)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + customerid + "'" +
                         " UNION ALL" +
                         " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + customerid + "' ORDER BY CUSTOMERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVoucherNo(string VoucherTypeID, string BranchID, string fromdate, string todate)
        {
            string sql = "SELECT AccEntryID,VoucherNo FROM Acc_Entry WHERE VoucherTypeID='" + VoucherTypeID + "' AND BranchID='" + BranchID + "' AND Date between CONVERT(date,'" + fromdate + "',103) and CONVERT(date,'" + todate + "',103)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotOffice()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE  BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLedger(String SessionID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            if (SessionID == "8")
            {
                sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' ORDER BY LedgerName";
            }
            else
            {
                sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' ORDER BY LedgerName";
            }

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLedgerReportGrid(string fromdate, string todate, string legderid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS] '" + fromdate + "','" + todate + "','" + legderid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCountry()
        {
            string sql = "SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY WHERE COUNTRYID <> '8F35D2B4-417C-406A-AD7F-7CB34F4D0B35' ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInvoice(string countryid, string fromdate, string todate)
        {
            string sql = " SELECT SALEORDERID,SALEORDERNO,COUNTRYNAME,SALEORDERDATE FROM T_SALEORDER_HEADER A INNER JOIN M_CUSTOMER B" +
                         " ON A.CUSTOMERID = B.CUSTOMERID WHERE COUNTRYID='" + countryid + "'" +
                         " AND SALEORDERDATE BETWEEN CONVERT(DATE,'" + fromdate + "',103) AND CONVERT(DATE,'" + todate + "',103) ORDER BY SALEORDERNO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable VoucherType()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select Id ,VoucherName from Acc_VoucherTypes Order by VoucherName";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable Region()
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql = "Select [BRID],[BRNAME] from [M_BRANCH] order by BRNAME";
                string sql = " SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                             " WHERE  BRANCHTAG = 'O' UNION " +
                             " SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                             " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                             " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindAccVoucher(string fromdate, string todate, string VoucherTypeID, string Mode, string region)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART1] '" + fromdate + "','" + todate + "','" + VoucherTypeID + "','" + Mode + "', '" + region + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAccVoucher_Ledger(string AccEntryId)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART2] '" + AccEntryId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindAccVoucher_Ledger_Voucher(string LedgerID)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART3] '" + LedgerID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindCategory_Type()
        {
            string sql = "SELECT ADV_CATEGORYID,ADV_CATEGORYNAME FROM M_ADV_CATEGORY ORDER BY ADV_CATEGORYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAdvertisementDetails(string fromdate, string todate, string CategoryID)
        {
            string sql = "EXEC [USP_RPT_ADVERISMENT_DETAILS] '" + CategoryID + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyDepot(string DepotID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyId(string ID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID = '" + ID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDailyReportGrid_TPU()
        {
            string sql = "EXEC [USP_RPT_GET_DAILY_FACTORY_TPU_TRANSACTION_COUNT]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStatewiseSale(string fromdate, string todate, string BSID, int amt)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_STATEWISE] '" + fromdate + "','" + todate + "','" + BSID + "'," + amt + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPartywiseSale(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotwiseSale(string fromdate, string todate, string stateid, string brand, string categoryid, string packsizeid, string bsid, int amt, string unitvalue)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_DEPOTWISE] '" + fromdate + "','" + todate + "','" + stateid + "','" + brand + "','" + categoryid + "','" + packsizeid + "','" + bsid + "'," + amt + ",'" + unitvalue + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductWiseSale(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCitybyHO(string DepotID)
        {
            string sql = " SELECT DISTINCT A.CITYID AS CITYID,City_Name AS CITYNAME FROM M_HEADQUATER A INNER JOIN M_CITY B ON A.CITYID=B.City_ID " +
                         " INNER JOIN M_BRANCH C ON A.HOID=C.CITYID " +
                         " WHERE BRID='" + DepotID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCity()
        {
            string sql = " SELECT DISTINCT City_ID,City_Name FROM M_CITY ORDER BY City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAllCustomers()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID <> (SELECT RETAILERID FROM P_APPMASTER) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotSales(string Month, string Year, string DepotID, string DistributorID)
        {
            string sql = "EXEC USP_RPT_DISTRIBUTORSALESREPORT '" + DepotID + "','" + DistributorID + "','" + Month + "', '" + Year + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPartyOnLoad(string fromdate, string todate, string depot, string Scheme, string bsid, string CustomerID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT] '" + fromdate + "','" + todate + "','" + depot + "','" + Scheme + "','" + bsid + "','1','" + CustomerID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindProductwisePartyOnLoad(string fromdate, string todate, string depot, string bsid, string saletype, string productid, int amt)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISEPARTY] '" + fromdate + "','" + todate + "','" + depot + "','" + bsid + "','" + saletype + "','" + productid + "'," + amt + "";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindProductwithBS(string BSID)
        {
            string sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID " +
                         " WHERE BUSNESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BSID + "',','))  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUserType()
        {
            string sql = " SELECT A.ROLEID,B.UTNAME FROM M_ROLE_HEIRARCHY A INNER JOIN M_USERTYPE B ON A.ROLEID = B.UTID " +
                         " WHERE A.ROLEID NOT IN((SELECT DISTRIBUTORID FROM P_APPMASTER),'DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB') ORDER BY AUTOID DESC";

            //string sql = "select utid as ROLEID,utname from M_USERTYPE where applicableto='C' and utname like '%Sales%'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUserTypeNEW()
        {
            string sql = " SELECT utid AS ROLEID,UTNAME FROM M_USERTYPE WHERE  applicableto='C' and utname like '%Sales%' " +
                         " AND UTID NOT IN ('DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB','E71105C2-64B7-40E9-AEE5-A08F055C6CDC') ORDER BY UTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindEmployeebyType(string typeid)
        {
            string sql = "SELECT USERID,(FNAME+' '+LNAME) AS FULLNAME FROM M_USER WHERE USERTYPE='" + typeid + "' ORDER BY FULLNAME";
            DataTable dt = new DataTable();
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

        public DataTable FetchDateRange(string span, string Tag)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                sql = " [USP_FETCH_DATERANGE] '" + span + "','" + Tag + "'";
            }
            else
            {
                sql = " [USP_FETCH_DATERANGE] '" + span + "','" + Tag + "'";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindJC()
        {
            string sql = " SELECT JCID,NAME FROM M_JC ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPackSizeName()
        {
            string sql = "SELECT DISTINCT UNITVALUE, (UNITVALUE+' '+UOMNAME) AS PACKSIZE FROM M_PRODUCT ORDER BY UNITVALUE ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSizeUOMName()
        {
            string sql = "SELECT DISTINCT UNITVALUE + '~' + UOMID AS PACKSIZEID, (UNITVALUE+' '+UOMNAME) AS PACKSIZENAME FROM M_PRODUCT WHERE UNITVALUE IS NOT NULL ORDER BY UNITVALUE + '~' + UOMID ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindHeadQuarder(string UserTypeID)
        {
            //  string sql = "SELECT DISTINCT HQID,HQNAME FROM VW_HEADQUARTER WHERE USERTYPE='" + UserTypeID + "' ORDER BY HQNAME";
            string sql = " SELECT DISTINCT  HQNAME ,HQID  from m_user where isnull(hqname,'') <>'' AND usertype='" + UserTypeID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAssignto(string ID)
        {
            string sql = "EXEC [USP_RPT_PARTYWISE_ASSIGNTO] '" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment_PrimarySales()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID <> (SELECT EXPORTBSID FROM P_APPMASTER) ORDER BY NAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCitybyHO()
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindLedgerReport(string fromdate, string todate, string ledgerid, string depot, string Sessionid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + depot + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public string GetFinYearID(string Session)
        {
            string sql = "SELECT id FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        public DataTable BindVoucher_credit_debit(string AccEntry_ID)
        {
            string sql = "EXEC [USP_RPT_DEBIT_CREDIT_LEDGER] '" + AccEntry_ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
       
        public DataTable BindTrialBalance(string FromDate, string ToDate, string RegionID, string AccgroupID)
        {

            string sql = "EXEC [USP_RPT_TRAILBALANCE] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        protected string Convert_To_YYYYMMDD(string pDate)
        {
            string sDate = "";
            string dd = "";
            string mm = "";
            string yy = "";
            dd = pDate.Substring(0, 2);
            mm = pDate.Substring(3, 2);
            yy = pDate.Substring(6, 4);
            sDate = yy + "-" + mm + "-" + dd;
            return sDate;
        }

        public DataTable Bind_PO_PRODUCT_SUMMARY(string POID, string TPUID, string fromdate, string todate)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PO_SUMMARY] '" + POID + "','" + TPUID + "','" + fromdate + "','" + todate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_PO_ORDER_SUMMARY(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_PO_SUMMARY] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_CompanyInfo()
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_COMPANY_INFO]";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_CompanyInfofactoryid( string poid) /*NEW SP ADD FOR FACTORY WISE PRINT OUT*/
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_COMPANY_INFO_FOR_FACTORYWISE] '"+ poid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindInvoice_Depot(string ID, string FROMDATE, string TODATE)
        {
            string sql = "SELECT SALEINVOICEID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX) AS SALEINVOICENO FROM T_SALEINVOICE_HEADER " +
                         " WHERE DEPOTID='" + ID + "' AND SALEINVOICEDATE  BETWEEN CONVERT(DATETIME,'" + FROMDATE + "',103) AND CONVERT(DATETIME,'" + TODATE + "',103) " +
                         " ORDER BY SALEINVOICENO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceFooter(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_FOOTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceHeader(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTax(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceTerms(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TERMS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceItemWiseTax(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_ITEMWISE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDistributor(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_DISTRIBUTORINFO] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_TaxComponents(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TAX_COMPONENT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_OutstadingAmount(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOUCE_OUTSTANDING] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindST_Header(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_HEADER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_ToDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_TRANSFER_TODEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_FromDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_TRANSFER_FROMDEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_QC_PRODUCT_SUMMARY(string Product, string TPUID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_PU_SUMMARY] '" + Product + "','" + TPUID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductForReport()
        {
            string sql = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE SETFLAG='P' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDebitNote(string Vouchertype, string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE] '" + Vouchertype + "','" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Depot_Accounts(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "' AND ISFACTORY='Y'" +
                      " ORDER BY BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataTable BindInvoiceNoByUser(string frmdate, string todate, string depotid, string bsid, string isvarified)
        {
            string sql = "";
            DataTable dt1 = new DataTable();

            if (depotid != "0")
            {
                sql = " SELECT (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                      " WHERE DEPOTID IN(SELECT BRID FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + depotid + "')  AND ISVERIFIED='" + isvarified + "' AND BSID='" + bsid + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";
            }
            else
            {
                sql = " SELECT (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                      " WHERE BSID='" + bsid + "' AND ISVERIFIED='" + isvarified + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockJournalReport(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_JOURNAL] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDespatchReport(string fromdate, string todate, string TPUID, string depotid, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_DESPATCH_DETAILS_RMPM] '" + fromdate + "','" + todate + "','" + TPUID + "','" + depotid + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPOHeader(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_POP_PURCHASE_ORDER_HEADER] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPODetails(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_POP_PURCHASE_ORDER_DETAILS] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindJournal(string VoucherTypeId, string VoucherId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACC_JOURNAL]  '" + VoucherTypeId + "','" + VoucherId + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindInvoiceTax(string INVOICEID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_SALE_INVOICE_TAX] '" + INVOICEID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurchaseHeader(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_FROMFACTORY] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseDetails(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_DETAILS] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseFooter(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_FOOTER] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseTax(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_TAX] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_SendDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_SENDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSR_DetailsDS(string STNO)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT] '" + STNO + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "SR_TRANSFER";
            ds.Tables[1].TableName = "SR_ITEMS";
            ds.Tables[2].TableName = "SR_RECEVIEDDEPO";
            ds.Tables[3].TableName = "SR_SENDDEPO";
            ds.Tables[4].TableName = "SR_COMPANYINFO";
            return ds;
        }

        public DataTable BindSR_Items(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_ITEMS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_RecvdDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_RECVDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_Transfer(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_TRANSFER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductForTPU(string TPUID)
        {
            string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM M_PRODUCT_TPU_MAP WHERE VENDORID='" + TPUID + "' ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_Claim(string Vouchertype, string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM] '" + Vouchertype + "','" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_HeaderClaim(string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM_HEADER] '" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_PartyHeaderClaim(string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM__PARTY_HEADER] '" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindClaimHistoryReports(string fromdate, string todate, string depotid, string status, string businesssegment)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_CLAIM_PARTYWISE_HISTORY] '" + fromdate + "','" + todate + "','" + depotid + "','" + status + "','" + businesssegment + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindClaimSummary(string fromdate, string todate, string depotid)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_DEPOTWISE_CLAIM_SUMMARY] '" + fromdate + "','" + todate + "','" + depotid + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindTopSheet_ClaimHeader(string Depotid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT__CLAIM_TOPSHEET_HEADER] '" + Depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTopSheet_ClaimDetails(string claimtypeid, string claimid, string userid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_CLAIM_TOPSHEET_PRINT] '" + claimtypeid + "','" + claimid + "','" + userid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTopSheet_ClaimDetailsPage(string fromdate, string todate, string depotid, string claimtype)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_CLAIM_TOPSHEET] '" + fromdate + "','" + todate + "','" + depotid + "','" + claimtype + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataSet BindSalesTaxSummary_Details(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSummaryDetails(string fromdate, string todate, string depotid, string partyname)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY '" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindDistributorbyAdmin()
        {
            string retailerid = "SELECT RETAILERID FROM P_APPMASTER";
            string retailer = Convert.ToString(db.GetSingleValue(retailerid));

            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN('" + retailer + "') ORDER BY CUSTOMERNAME";
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

        public DataTable BindDepotwiseSales(string caldetails, string spamid, string fromdate, string todate, string groupid, string productid)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALE_REPORT] '" + caldetails + "','" + spamid + "','" + fromdate + "','" + todate + "','" + groupid + "','" + productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroup()
        {
            string sql = "SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY ORDER BY DIS_CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Retrive CategoryWise_Sales
        public DataTable BindCategoryWise_Sales(string fromdate, string todate, string zone)
        {
            string sql = "EXEC [USP_RPT_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Retrive ZoneWise_Sales
        public DataTable BindZoneWise_Sales(string fromdate, string todate, string zone)
        {
            string sql = "EXEC [USP_RPT_CATEGORYWISE_ZONEWISE] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Get Zone
        public DataTable BindZone()
        {
            string sql = "SELECT DISTINCT ZONE AS ZONE_ID,CASE WHEN ZONE='N' THEN 'North'WHEN ZONE='C' THEN 'Central'WHEN ZONE='E' THEN 'East'WHEN ZONE='S' THEN 'South'WHEN ZONE='W' THEN 'West'WHEN ZONE='NE' THEN 'North East'Else 'Central'END AS Zone FROM M_REGION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindProductbybrandandcatid(string divid, string catid, string DepotId)
        {
            string sql = " SELECT DISTINCT A.ID,A.PRODUCTALIAS AS NAME FROM M_PRODUCT AS A" +
                         " INNER JOIN M_PRODUCT_TPU_MAP AS B ON B.PRODUCTID=A.ID " +
                         //" WHERE A.DIVID IN(SELECT * FROM DBO.FNSPLIT('" + divid + "',')) " +
                         " WHERE A.DIVID IN(SELECT * FROM DBO.FNSPLIT('" + divid + "', ','))" +
                         //IN(SELECT * FROM DBO.FNSPLIT('" + BusinessSegmentID + "', ','))
                         " AND A.CATID IN(SELECT * FROM DBO.FNSPLIT('" + catid + "',',')) " +
                         " AND B.VENDORID='" + DepotId + "'" +
                         " AND A.ACTIVE='T' " +
                         " ORDER BY A.PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindDepotwiseProductwiseSales(string fromdate, string todate, string groupid, string productid)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_PRODUCTWISE_SALE_REPORT] '" + fromdate + "','" + todate + "','" + groupid + "','" + productid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindCustomerwiseReport(string fromdate, string todate, string groupid)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerConfirmedInvoice(string depotid, string destid, string userid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_CONFIRMED_INVOICES] '" + depotid + "','" + destid + "','" + userid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindCustomerConfirmedInvoiceDetails(string invoiceid, string finyr)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_PRODUCTWISE_INVOICE_DETAILS] '" + invoiceid + "','" + finyr + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindCustomerPendingInvoice(string depotid, string destid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_PENDING_INVOICES] '" + depotid + "','" + destid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotByUserid(string userid)
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID IN (SELECT TPUID FROM M_TPU_USER_MAPPING WHERE USERID='" + userid + "')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCustomerByUserid(string userid)
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID IN (SELECT * FROM DBO.FNSPLIT((SELECT DISTRIBUTORLIST FROM M_USER WHERE USERID='" + userid + "'),','))ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerInvoiceCount(string userid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_INVOICES_COUNT] '" + userid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotFreeProductSale(string format, string span, string fromdate, string todate, string depotid, string productid, string showby)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOT_FREE_PRODUCT_SALE] '" + format + "','" + span + "','" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + showby + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTrialBalance_tax_payable(string FromDate, string ToDate, string RegionID, string AccgroupID)
        {

            string sql = "EXEC [USP_RPT_TRAILBALANCE_TAX_PAYABLE] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + AccgroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHQbyDepot(string DepotID)
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY HQNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindPrimarySalesTaxSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn, string CustomerID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "','" + CustomerID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindInvoiceDetialsReport(string InvoiceID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_DETAILS] '" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindHQSalesReport(string DepotID, string Bsid, string fromdate, string todate)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_HEADQUARTER_SALES] '" + DepotID + "','" + Bsid + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bind_InvoiceDetails_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceHeader_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTax_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_ITEMWISE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceFooter_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_FOOTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoicePriceScheme_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_PRICE_SCHEME] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDistributor_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_SALE_RETURN_DISTRIBUTORINFO] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Fetch Party For a Business Segment Wise
        public DataTable BindPartyBSWise(string BusinessSegmentID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BusinessSegmentID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPartyBSWise(string BusinessSegmentID, string DEPOTID)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID = B.CUSTOMERID WHERE BUSINESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BusinessSegmentID + "',',')) AND B.DEPOTID='" + DEPOTID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        #region Fetch Party For a HQ Wise
        public DataTable BindPartyHQWise(string HQID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_HQ_MAPPING WHERE HQID IN (SELECT *  FROM DBO.FNSPLIT('" + HQID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable Bind_DepotWise_Statewise(string FROMDATE, string TODATE, string STATEID, string BSID, decimal AMTIN)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PRIMARY_SALE_DEPOT_STATEWISE] '" + FROMDATE + "','" + TODATE + "','" + STATEID + "','" + BSID + "','" + AMTIN + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillEntry(string fromdate, string todate, string depot, string Type)
        {
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_ENTRY] '" + fromdate + "','" + todate + "','" + depot + "','" + Type + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetBatchDetails(string fromdate, string todate)
        {
            string sql = "EXEC [USP_RPT_MM_BATCHWISE] '" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetMMPurchaseReport(string fromdate, string todate, string VendorID, string FactoryID)
        {
            string sql = "EXEC [USP_RPT_MM_PURCHASE_REPORT] '" + fromdate + "','" + todate + "','" + VendorID + "','" + FactoryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBrand_SupliedItem()
        {
            string sql = "EXEC USP_MM_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory_SupliedItem(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactory(string UserID)
        {
            string sql = "select VENDORID, VENDORNAME  from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "'  and Tag='F'  )";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct(string catid, string DIVID)
        {
            string sql = " SELECT A.ID,A.NAME AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A LEFT JOIN M_CATEGORY AS B " +
                         " ON A.CATID= B.CATID" +
                         " WHERE A.CATID='" + catid + "' AND A.DIVID='" + DIVID + "'" +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
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

        public DataTable BindProgressReport(string suplliedid, string finyr, string frmdt, string todate)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [sp_StockPrpgressSummery] '" + suplliedid + "','" + finyr + "','" + frmdt + "','" + todate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand_SupliedItemName()
        {
            string sql = "EXEC USP_MM_LOADBRAND_ITEM";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindSummaryDetails_GST(string fromdate, string todate, string depotid, string partyname)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST_FACTORY'" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /* public DataTable Bind_CompanyInfo()
         {
             DataTable dt = new DataTable();
             string sql = "[dbo].[SP_RPT_COMPANY_INFO]";
             dt = db.GetData(sql);
             return dt;
         }

          public DataTable BindPurchaseHeader(string STCKRID)
          {
              DataTable dt = new DataTable();
              string sql = "EXEC [USP_RPT_STOCK_RECEIVED_HEADER] '" + STCKRID + "'";
              dt = db.GetData(sql);
              return dt;
          }
          public DataTable BindPurchaseDetails(string STCKRID)
          {
              DataTable dt = new DataTable();
              string sql = "EXEC [USP_RPT_STOCK_RECEIVED_DETAILS] '" + STCKRID + "'";
              dt = db.GetData(sql);
              return dt;
          }*/

        public DataTable BindFactoryItemwise(string format, string span, string fromdate, string todate, string productid, string depotid, string tpuid, string showby)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_FACTORY_ITEMWISE_PURCHASE] '" + format + "','" + span + "','" + fromdate + "','" + todate + "','" + productid + "','" + depotid + "','" + tpuid + "','" + showby + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryItemwiseProduction(string format, string span, string fromdate, string todate, string productid, string tpuid, string showby)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_FACTORY_ITEMWISE_PRODUCTION] '" + format + "','" + span + "','" + fromdate + "','" + todate + "','" + productid + "','" + tpuid + "','" + showby + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactoryItemwise_History(string fromdate, string todate, string depotid, string productid, string storelocation)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ITEMWISE_HISTORY_REPORT_FACTORY] '" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + storelocation + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductALIAS()
        {
            string sql = "SELECT DISTINCT ID, CASE WHEN PRODUCTALIAS IS NULL THEN NAME ELSE PRODUCTALIAS END AS NAME FROM M_PRODUCT ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactoryTypewise_Item()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MM_PRODUCT_TYPEWISE]";
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
        public DataTable BindFactoryTypewise_Item_Framework_BOM_Details(string frameworkid, string ProcessID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MM_BOM_REPORT] '" + frameworkid + "','" + ProcessID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBOMProcesSummary(string fromdate, string todate, string productid)
        {
            string sql = "EXEC USP_RPT_ITEM_SUMMARY '" + fromdate + "','" + todate + "','" + productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBOMProcesDetails(string fromdate, string todate, string productid, string factortyid)
        {
            string sql = "EXEC USP_RPT_ITEM_BOM_SUMMARY '" + fromdate + "','" + todate + "','" + productid + "','" + factortyid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDetails_MM_GST(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_DEPOT_MM_GST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceHeader_MM(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_HEADER_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceTax_MM(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_ITEMWISE_TAX_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceFooter_MM(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_FOOTER_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDistributor_MM(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_DISTRIBUTORINFO_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_CompanyInfo_MM()
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_COMPANY_INFO] ";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceFromDepot_MM(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALEINVOICE_FROMDEPOT_DETAILS_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryDespatch_Header(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_HEADER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryDespatch_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactoryDespatch_Footer(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_FOOTER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactoryDespatch_FromDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_FROMDEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryDespatch_ToDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_TODEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindTradingSaleInvoice(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_TREADING_SALE_INVOICE '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindQAReport(string Fromdate, string Todate, string FactoryID)
        {
            string sql = "EXEC USP_RPT_DAILY_QA_REPORT '" + Fromdate + "','" + Todate + "','" + FactoryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Purchase Bill (GRN Print)
        public DataTable BindPurchaseReceived_Header(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_PURCHASERECEIVED_HEADER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseReceived_Details(string InvoiceID, string original)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PURCHASERECEIVED_DETAILS_GST_PRINT] '" + InvoiceID + "','" + original + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Bind_PurchaseReceived_Print(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [dbo].[USP_RPT_PURCHASERECEIVED_HEADER_GST_PRINT] '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion


        public DataTable BindPoStatus(string fromdate, string todate, string vendore,string FactoryId)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string sql = "EXEC [USP_RPT_PO_RECEIVED_RETURN] '" + fromdate + "','" + todate + "','" + vendore + "','"+ FactoryId + "','"+ FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindJoborderStatus(string fromdate, string todate, string vendore, string FactoryId)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string sql = "EXEC [USP_RPT_JOB_ORDER_FACTORY_TEST] '" + fromdate + "','" + todate + "','" + vendore + "','" + FactoryId + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBranch(string mode,string branchid)
        {
            string sql = "EXEC [USP_RPT_BIND_BRANCH] '" + mode + "','"+ branchid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRequistionDetails(string fromdate, string todate,  string FactoryId)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string sql = "EXEC [BIND_REQUISTION_DETAILS] '" + fromdate + "','" + todate + "','" + FactoryId + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductionDetails(string fromdate, string todate, string FactoryId)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string sql = "EXEC [BIND_PRODUCTION_STATUS] '" + fromdate + "','" + todate + "','" + FactoryId + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMaterialRejection(string fromdate, string todate, string FactoryId ,string REJECTIONTYPE,string DIVID)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string sql = "EXEC [USP_RPT_MATERIAL_REJECTION] '" + fromdate + "','" + todate + "','" + FactoryId + "','"+ REJECTIONTYPE + "','"+ DIVID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable loadRequisitionIssuedept(string mode)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [USP_BIND_ID_MODE_WISE] '" + mode + "','" + USerid + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRequisitionVsIssueQty(string fromdate,string todate,string rDeptid,string iDeptid,string productid,string issueType)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [USP_RPT_REQUISITION_VS_ISSUE_QTY] '" + fromdate + "','" + todate + "','" + rDeptid + "','" + iDeptid + "','" + productid + "','" + issueType + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductionReport(string fromdate, string todate, string Deptid, string productid,string details)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [USP_RPT_PRODUCTION_WITH_DETAILS] '" + fromdate + "','" + todate + "','" + Deptid + "','" + productid + "','" + details + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bind_planning_vs_production(string fromdate, string todate,  string productid)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [Usp_planning_vs_production_report] '" + fromdate + "','" + todate + "','" + productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable fetchNonFgProductConsumptionQty(string fromdate, string todate)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [USP_RAW_MATERIAL_COUNSMPTION] '" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bind_Product_wise_bom(string productid, string rDeptId)
        {
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString();
            string USerid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC [Usp_productwise_bom_report] '" + productid + "','" + rDeptId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet RptBindMaterialGrid()
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_RPT_BINDMATERIAL";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindInvoicedetails(string AccEntry_ID)
        {
            string sql = "EXEC [USP_FETCH_INVOICE_FROM_LEDGER] '" + AccEntry_ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLedgerWiseInvoiceReport(string fromdate,string todate,string ledgerid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_WISE_INVOICE_DETAILS] '" + fromdate + "','"+ todate + "','"+ ledgerid + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

    }
}
