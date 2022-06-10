using System;
using System.Data;
using DAL;

namespace BAL
{
    public class ClsFactoryStockTransfer_RMPM
    {
        DBUtils db = new DBUtils();
        public DataTable BindDepo(string depotid)
        {
            string sql = string.Empty;
            if (depotid != "")
            {
                sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' AND BRID='" + depotid + "' ORDER BY BRNAME";
            }
            else
            {
                sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' ORDER BY BRNAME";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string HSNCode(string ProductID)
        {
            string HSNCODE = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ISNULL(B.HSNCODE,'') AS HSNCODE FROM M_PRODUCT A INNER JOIN M_HSNCODE B " +
                  " ON A.CATID=B.CATID " +
                  " AND A.ID='" + ProductID + "'";
            HSNCODE = (string)db.GetSingleValue(sql);
            return HSNCODE;
        }

        public DataTable CheckWaybillNo(string TransferID)
        {
            string sql = " SELECT * FROM T_STOCKTRANSFER_HEADER WITH (NOLOCK)" +
                         " WHERE STOCKTRANSFERID='" + TransferID + "'" +
                         " AND LTRIM(WAYBILLNO)!='' " +
                         " AND LTRIM(RTRIM(WAYBILLNO))!='0' OR LTRIM(RTRIM(WAYBILLNOAPPLICABLE))='0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int FFORM(string FromDepotID, string ToDepotID)
        {
            int flag = 0;
            string sql = " SELECT CASE WHEN A.STATEID=B.STATEID THEN 1 ELSE 0 END SAMESTATE FROM M_BRANCH A,M_BRANCH B WHERE A.BRID='" + FromDepotID + "' AND B.BRID='" + ToDepotID + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
        }

        public DataTable CheckForm(string TransferID)
        {
            string sql = " SELECT * FROM [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) " +
                         " WHERE FORMREQUIRED='Y' " +
                         " AND FFORMNO IS NULL " +
                         " AND STOCKTRANSFERID='" + TransferID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChildDepo(string DEPOID)
        {
            string sql = "SELECT '-1' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'0' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE   FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' AND BRID<>'" + DEPOID + "' UNION SELECT '-3' as BRID,'---- DEPOT ----' as BRNAME ,'3' AS SEQUENCE UNION SELECT BRID,BRNAME,'5' AS SEQUENCE FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND BRID<>'" + DEPOID + "' ORDER BY SEQUENCE,BRNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactoryTransporter(string FactoryID)
        {
            string sql = " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                         " M_TRANSPOTER_TPU_MAP AS B ON" +
                         " A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         " AND  B.TPUID='" + FactoryID + "'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRecentTxnTransporter(string FactoryID, string ToDepotID, string FinYear, string ModuleID)
        {
            string sql = " EXEC USP_FETCH_RECENT_TXN_TRANSPORTER '" + FactoryID + "','" + ToDepotID + "','" + FinYear + "','" + ModuleID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindToDepo(string DEPOID)
        {
            string sql = string.Empty;
            /*string sql = " SELECT BRID,"+
                         " CASE WHEN BRNAME LIKE '%(%' "+
                         " THEN SUBSTRING(BRNAME,CHARINDEX('(',BRNAME,1) + 1,CHARINDEX(')',BRNAME,1) - CHARINDEX('(',BRNAME,1) - 1) "+
                         " ELSE BRNAME END AS BRNAME"+
                         " FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' "+
                         " AND BRID <>'" + DEPOID + "'" +
                         " ORDER BY BRNAME ";*/
            sql = " SELECT BRID, BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' AND BRID <>'" + DEPOID + "' ORDER BY BRNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string CheckLRGR(string LRGR, string TransferID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_STOCKTRANSFER_HEADER] WHERE LRGRNO='" + LRGR + "' AND STOCKTRANSFERID <> '" + TransferID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckInvoiceNo(string ChallanNo, string TransferID, string DepotID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_STOCKTRANSFER_HEADER] WHERE CHALLANNO='" + ChallanNo + "' AND MOTHERDEPOTID='" + DepotID + "' AND STOCKTRANSFERID <> '" + TransferID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }
        public DataTable BindRegion()
        {
            string sql = "SELECT STATE_ID,STATE_NAME FROM M_REGION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Add HSN Code In Category (Rajeev)
        public DataTable BindCATEGORY()
        {
            string sql = " SELECT SUBTYPEID AS CATID,CASE WHEN HSE IS NULL THEN SUBITEMNAME WHEN HSE='' THEN SUBITEMNAME ELSE SUBITEMNAME+'  ('+HSE+')' END AS CATNAME,ISNULL(HSE,'') AS HSE" +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE " +
                         " WHERE HSE <> '' AND HSE IS NOT NULL" +
                         " ORDER BY SUBITEMNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindInsurance()
        {
            string sql = "SELECT INSURANCE_NO FROM M_INSURANCEDOCUMENT";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTranspoter(string DepotID)
        {
            string sql = " SELECT A.ID,A.NAME " +
                         " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                         " M_TRANSPOTER_DEPOT_MAP AS B " +
                         " ON A.ID=B.TRANSPOTERID" +
                         " WHERE B.DEPOTID='" + DepotID + "'" +
                         " AND B.TAG='D'" +
                         " ORDER BY A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTranspoterChecker()
        {
            string sql = " SELECT A.ID,A.NAME " +
                         " FROM M_TPU_TRANSPORTER AS A " +
                         " ORDER BY A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWayBillNo(string TODEPOTID, string TRANSFERID)
        {
            string sql = "SELECT DISTINCT WAYBILLNO FROM M_WAYBILL WHERE STATEID IN(SELECT STATEID FROM [M_BRANCH] WHERE BRID='" + TODEPOTID + "') " +
                         "AND WAYBILLNO NOT IN(SELECT WAYBILLKEY FROM dbo.T_STOCKTRANSFER_HEADER WHERE STOCKTRANSFERID<>'" + TRANSFERID + "') ORDER BY WAYBILLNO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWayBillNoEdit(string TODEPOTID, string TRANSFERID)
        {
            string sql = " SELECT DISTINCT WAYBILLNO FROM M_WAYBILL WHERE STATEID IN(SELECT STATEID FROM [M_BRANCH] WHERE BRID='" + TODEPOTID + "') " +
                         " AND WAYBILLNO NOT IN(SELECT WAYBILLKEY FROM dbo.T_STOCKTRANSFER_HEADER WITH (NOLOCK) WHERE STOCKTRANSFERID<>'" + TRANSFERID + "') " +
                         " UNION ALL" +
                         " SELECT DISTINCT WAYBILLNO FROM M_WAYBILL WHERE STATEID IN(SELECT STATEID FROM [M_BRANCH] WHERE BRID='" + TODEPOTID + "') " +
                         " AND WAYBILLNO IN(SELECT WAYBILLKEY FROM dbo.T_STOCKTRANSFER_HEADER WITH (NOLOCK) WHERE STOCKTRANSFERID<>'" + TRANSFERID + "')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //Change By Rajeev For RMPM
        public DataTable BindProduct(string DepotID)
        {
            string sql = string.Empty;
            sql = " SELECT A.ID AS PRODUCTID,A.PRODUCTALIAS AS PRODUCTNAME,A.CATNAME,A.DIVNAME,A.CATID " +
                  " FROM M_PRODUCT AS A " +
                  " WHERE  A.TYPE NOT IN('FG','GIFT','SFG') " +
                  " ORDER BY A.PRODUCTALIAS ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategoryProduct(string Category, string DepotID)
        {
            string sql = string.Empty;
            sql = " SELECT A.ID AS PRODUCTID,A.PRODUCTALIAS AS PRODUCTNAME,A.CATNAME,A.DIVNAME,A.CATID " +
                  " FROM M_PRODUCT AS A " +
                  " WHERE A.ACTIVE='T' " +
                  " AND A.CATID='" + Category + "' " +
                  " ORDER BY A.PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderProduct(string depotid, string SaleOrderID)
        {
            string sql = string.Empty;
            sql = " SELECT DISTINCT A.PRODUCTID AS PRODUCTID,A.PRODUCTNAME AS PRODUCTNAME," +
                  " C.SEQUENCENO, B.CATNAME,B.DIVNAME" +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN M_PRODUCT AS B" +
                  " ON A.PRODUCTID = B.ID INNER JOIN M_CATEGORY AS C" +
                  " ON C.CATID=B.CATID" +
                  " WHERE A.SALEORDERID='" + SaleOrderID + "'" +
                  " ORDER BY C.SEQUENCENO, B.CATNAME,B.DIVNAME,A.PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSTockQTYBasedONBATCHNO(string depotid, string pid, string batchno, string TOPACKSIZEID, string transferid)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BATCHWISE_FACTORY_STOCK_RMPM '" + depotid + "','" + pid + "','" + TOPACKSIZEID + "','" + batchno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductInfo(string depotid, string pid, string batchno, string TOPACKSIZEID, string transferid)
        {
            string sql = string.Empty;
            sql = "SP_PRODUCTWISE_INFORMATION '" + depotid + "','" + pid + "','" + TOPACKSIZEID + "','" + batchno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSize(string PID)
        {
            //string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE SEQUENCENO IN (3) ORDER BY SEQUENCENO";
            string sql = "SELECT PACKSIZEID_TO AS PSID,PACKSIZEName_TO AS PSNAME FROM M_PRODUCT_UOM_MAP WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            string sql = "EXEC USP_BATCHWISE_FACTORY_STOCK_RMPM '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet GetWeight(string ProductId, string PackSizeid, decimal Qty)
        {
            DataSet ds = new DataSet();
            string sql = "exec [SP_STOCK_WEIGHTCALCULATION_RMPM] '" + ProductId + "','" + PackSizeid + "'," + Qty + "";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindBatchno(string depotid, string pid, string transferid)
        {
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','" + pid + "','" + RETURNPACKSIZE + "','0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal BindDepotRate(string Productid, decimal MRP, string InvoiceDate)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_DEPOT_TRANSFER_RATESHEET] where PRODUCTID='" + Productid + "' AND MRP=" + MRP + " AND CONVERT(DATE,'" + InvoiceDate + "',103) BETWEEN FROMDATE AND TODATE) BEGIN " +
                      " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));

            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_DEPOT_TRANSFER_RATESHEET" +
                            " WHERE PRODUCTID = '" + Productid + "' AND MRP=" + MRP + " AND CONVERT(DATE,'" + InvoiceDate + "',103) BETWEEN FROMDATE AND TODATE ";
                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                //string sql = "EXEC SP_CALCULATE_DEPOT_RATE " + MRP + ",'" + Productid + "'";
                string sql = "USP_CALCULATE_TRANSFER_RATE " + MRP + ",'" + Productid + "','" + InvoiceDate + "'";
                price = (decimal)db.GetSingleValue(sql);
            }
            return price;
        }

        public decimal FetchQtyInPcs(string Productid, string FromPacksizeid, decimal qty)
        {
            string sql = "SP_CONVERTION_INTO_PCS '" + Productid + "','" + FromPacksizeid + "','" + qty + "'";
            decimal returnqty = 0;
            returnqty = (decimal)db.GetSingleValue(sql);
            return returnqty;
        }

        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderType()
        {
            string sql = "SELECT OrderTYPEID,ORDERTYPENAME FROM M_ORDERTYPE WHERE OrderTYPEID='2E96A0A4-6256-472C-BE4F-C59599C948B0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCountry()
        {
            string sql = " SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY" +
                         " WHERE COUNTRYID NOT IN (SELECT COUNTRYID FROM P_APPMASTER)" +
                         " ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSaleOrder(string CountryID)
        {
            string sql = "EXEC USP_EXPORT_SALEORDER_DETAILS '" + CountryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetDespatchstatus(string DespatchID)
        {
            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKDESPATCHID='" + DespatchID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }

        public string GetFinancestatus(string DespatchID)
        {
            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_STOCKDESPATCH_HEADER] WHERE STOCKDESPATCHID='" + DespatchID + "' AND ISVERIFIED='Y') BEGIN " +
                         " SELECT '1'   END  ELSE  BEGIN  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }
        public DataTable BindFactoryDispatch(string fromdate, string todate, string finyear, string CheckerFlag, string UserID, string Depotid)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_FACTORYDISPATCH_RMPM '" + fromdate + "','" + todate + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "','" + Depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDespatchCformFilter(string filterText, string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID, string OrderTypeID)
        {
            string sql = string.Empty;
            if (filterText == "1")
            {
                if (CheckerFlag == "FALSE")
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY,CONVERT(VARCHAR(10),CAST(FFORMDATE AS DATE),103) AS FFORMDATE," +
                          "ISNULL(FFORMNO,'') AS FFORMNO,(CASE WHEN FORMREQUIRED='Y' THEN 'YES' ELSE 'NO' END) AS FORMREQUIRED," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and FFORMNO = '' AND FORMREQUIRED = 'Y'" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND CREATEDBY = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                         "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY,CONVERT(VARCHAR(10),CAST(FFORMDATE AS DATE),103) AS FFORMDATE," +
                         "ISNULL(FFORMNO,'') AS FFORMNO,(CASE WHEN FORMREQUIRED='Y' THEN 'YES' ELSE 'NO' END) AS FORMREQUIRED ," +
                         " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                         " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                         " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                         " MOTHERDEPOTID,TODEPOTID" +
                         " from [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                         " and FFORMNO = '' AND FORMREQUIRED = 'Y'" +
                         " AND FINYEAR ='" + finyear + "'" +
                         " AND ISVERIFIED <> 'Y'" +
                         " AND NEXTLEVELID = '" + UserID + "'" +
                         " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                         " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "2")
            {
                if (CheckerFlag == "FALSE")
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY,CONVERT(VARCHAR(10),CAST(FFORMDATE AS DATE),103) AS FFORMDATE," +
                          "ISNULL(FFORMNO,'') AS FFORMNO,(CASE WHEN FORMREQUIRED='Y' THEN 'YES' ELSE 'NO' END) AS FORMREQUIRED ," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and FFORMNO <> '' AND FORMREQUIRED = 'Y'" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND CREATEDBY = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY,CONVERT(VARCHAR(10),CAST(FFORMDATE AS DATE),103) AS FFORMDATE," +
                          "ISNULL(FFORMNO,'') AS FFORMNO,(CASE WHEN FORMREQUIRED='Y' THEN 'YES' ELSE 'NO' END) AS FORMREQUIRED," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and FFORMNO <> '' AND FORMREQUIRED = 'Y'" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND NEXTLEVELID = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "3")
            {
                if (CheckerFlag == "FALSE")
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and WAYBILLNO = '' OR WAYBILLNO ='0' " +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND CREATEDBY = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          " TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and WAYBILLNO = '' OR WAYBILLNO ='0'" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND NEXTLEVELID = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "4")
            {
                if (CheckerFlag == "FALSE")
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and WAYBILLNO <> '' AND WAYBILLNO !='0' " +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND CREATEDBY = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " select STOCKTRANSFERID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,STOCKTRANSFERNO,MOTHERDEPOTID,MOTHERDEPOTNAME,TODEPOTID," +
                          "TODEPOTNAME,ISNULL(WAYBILLNO,'') AS WAYBILLNO,ISNULL(WAYBILLKEY,'') AS WAYBILLKEY," +
                          " NEXTLEVELID AS NEXTLEVELID,ISVERIFIED," +
                          " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " MOTHERDEPOTID,TODEPOTID" +
                          " from [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                          " and WAYBILLNO <> '' AND WAYBILLNO !='0' " +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " AND NEXTLEVELID = '" + UserID + "'" +
                          " AND ORDERTYPEID = '" + OrderTypeID + "'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int UpdateWaybillNo(string TransferID, string WaybillNo, string WaybillKey)
        {
            int updateflag = 0;
            string sqlCheck = string.Empty;
            int exists = 0;
            int e = 0;

            sqlCheck = " IF  EXISTS(SELECT * FROM [M_WAYBILL] where WAYBILLNO='" + WaybillKey + "') BEGIN " +
                      " SELECT 1   END  ELSE    BEGIN	  SELECT 0   END ";

            exists = ((Int32)db.GetSingleValue(sqlCheck));

            if (exists == 1)
            {
                string sql = " UPDATE T_STOCKTRANSFER_HEADER SET WAYBILLNO = '" + WaybillNo + "',WAYBILLKEY = '" + WaybillKey + "'" +
                             " WHERE STOCKTRANSFERID ='" + TransferID + "'";
                e = db.HandleData(sql);
            }

            else if (exists == 0)
            {
                e = 2;
            }

            if (e == 0)
            {
                updateflag = 0;  // update unsuccessfull
            }
            else if (e == 2)
            {
                updateflag = 2;  // validation
            }
            else
            {
                updateflag = 1;  // update successfull
            }

            return updateflag;
        }

        public DataTable BindProductDetails(string depotid)
        {
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','0','" + RETURNPACKSIZE + "','0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string InsertTransferDetails(string transferdate, string motherdepotid, string motherdepotname, string todepotid, string todepotname, string waybillkey,
                                             string waybillapplicable, string insuranceno, string transporterid, string transportername, string modetransport,
                                             string vehichleno, string lrgrno, string lrgrdate, string challenno, string challendate, string fform, string gatepassno,
                                             string gatepassdate, string createdby, string finyear, string hdnvalue, string xml, string insurancecompid,
                                             string insurancecompname, string Remarks, string MENUID, string OrderTypeID, string OrderTypeName, string CountryID, string CountryName,
                                             string SaleOrderID, string SaleOrderNo, decimal TotalCase, decimal TotalPCS, string xmlTax, decimal GROSSAMOUNT, decimal NETAMOUNT,
                                             decimal TOTALTAXAMT, decimal BASICAMT, decimal Roundoff, int Invoice_Type)
        {
            string flag = "";
            string stocktransferid = string.Empty;
            string stocktransferno = string.Empty;

            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }

            try
            {
                string sqlproctransfer = " EXEC [SP_T_STOCKTRANSFER_HEADER] '" + hdnvalue + "','" + flag + "','" + transferdate + "','" + motherdepotid + "'," +
                                         " '" + motherdepotname + "','" + todepotid + "','" + todepotname + "','" + waybillkey + "','" + waybillapplicable + "'," +
                                         " '" + insuranceno + "','" + transporterid + "','" + transportername + "','" + modetransport + "','" + vehichleno + "'," +
                                         " '" + lrgrno + "','" + lrgrdate + "','" + challenno + "','" + challendate + "','" + fform + "','" + gatepassno + "'," +
                                         " '" + gatepassdate + "','" + createdby + "','" + finyear + "','" + xml + "','" + insurancecompid + "','" + insurancecompname + "'," +
                                         " '" + Remarks + "','" + MENUID + "','" + OrderTypeID + "','" + OrderTypeName + "','" + CountryID + "','" + CountryName + "'," +
                                         " '" + SaleOrderID + "','" + SaleOrderNo + "'," + TotalCase + "," + TotalPCS + ",'" + xmlTax + "'," + GROSSAMOUNT + "," + NETAMOUNT + "," +
                                         " " + TOTALTAXAMT + "," + BASICAMT + "," + Roundoff + "," + Invoice_Type + "";
                DataTable dttransferdetails = db.GetData(sqlproctransfer);

                if (dttransferdetails.Rows.Count > 0)
                {
                    stocktransferid = dttransferdetails.Rows[0]["STOCKTRANSFERID"].ToString();
                    stocktransferno = dttransferdetails.Rows[0]["STOCKTRANSFERNO"].ToString();
                }
                else
                {
                    stocktransferno = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return stocktransferno;
        }
        #region Insert-Update Despatch
        public string InsertFactoryDespatch(string hdnvalue, string despatchDate, string tpuid, string tpuname,
                                             string wayBillNo, string InvoiceNo, string InvoiceDate,
                                             string TransporterID, string Vehicle, string DepotID, string DepotName,
                                             string LRGRNo, string LRGRDate, string TransportMode, int userID, string finyear,
                                             string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment, decimal RoundOff,
                                             string strTermsID, string CFormNo, string CFormDate, string Gatepassno,
                                             string gatepassdate, string FormFlag,
                                             string xml, string xmlTax, string xmlGrossTax, string insCompID,
                                             string insCompName, string insuranceNumber,
                                             decimal TotalCase, decimal TotalPCS, string Export,
                                             string CountryID, string CountryName,
                                             string AllocationID, string AllocationNo, string InvoiceType,string ShippingAd, string ModuleID,string storelocation)
        {

            string flag = "";
            string DespatchID = string.Empty;
            string DespatchNo = string.Empty;


            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }

            try
            {
                string sqlprocDespatch = " EXEC [USP_FACTORY_DESPATCH_INSERT_UPDATE_RMPM] '" + hdnvalue + "','" + flag + "','" + despatchDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "'," +
                                         " '" + InvoiceNo + "','" + InvoiceDate + "'," +
                                         " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                         " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                         " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "','" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                         " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                         " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + insCompID + "','" + insCompName + "'," +
                                         " '" + insuranceNumber + "'," + TotalCase + "," + TotalPCS + ",'" + Export + "','" + CountryID + "','" + CountryName + "'," +
                                         " '" + AllocationID + "','" + AllocationNo + "','" + InvoiceType + "','"+ ShippingAd + "','" + ModuleID + "','" + storelocation + "'";
                DataTable dtDespatch = db.GetData(sqlprocDespatch);

                if (dtDespatch.Rows.Count > 0)
                {
                    DespatchID = dtDespatch.Rows[0]["DESPATCHID"].ToString();
                    DespatchNo = dtDespatch.Rows[0]["DESPATCHNO"].ToString();
                }
                else
                {
                    DespatchNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return DespatchNo;
        }
        #endregion

        public DataTable UpdateStock(string mode, string finyear, string motherdepotid, string motherdepotname, string pid, string pname, string batchno, decimal qty, decimal beforeeditedqty, string frompacksizeid)
        {
            string sqlprocstock = "EXEC [sp_STOCKUPDATE] '" + mode + "','" + finyear + "','" + motherdepotid + "','" + motherdepotname + "','" + pid + "','" + pname + "','" + batchno + "','" + qty + "','" + beforeeditedqty + "','" + frompacksizeid + "'";
            DataTable dtflag = db.GetData(sqlprocstock);
            return dtflag;
        }

        public DataTable BindTransferHeader(string transferid)
        {
            string sql = " SELECT STOCKTRANSFERID,STOCKTRANSFERNO,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,MOTHERDEPOTID,TODEPOTID,ISNULL(WAYBILLKEY,'0') AS WAYBILLKEY,WAYBILLNOAPPLICABLE,INSURANCENO," +
                        " TRANSPORTERID,MODEOFTRANSPORT,VEHICHLENO,LRGRNO,CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) AS LRGRDATE,ISNULL(REMARKS,'') AS REMARKS,ISNULL(NOTE,'') AS NOTE," +
                        " CHALLANNO,CONVERT(VARCHAR(10),CAST(CHALLANDATE AS DATE),103) AS CHALLANDATE,FINYEAR,FORMREQUIRED,GATEPASSNO,CONVERT(VARCHAR(10),CAST(GATEPASSDATE AS DATE),103) AS GATEPASSDATE ,ISNULL(INSURANCECOMPID,'0') AS INSURANCECOMPID," +
                        " ISNULL(ORDERTYPEID,'0') AS ORDERTYPEID,ISNULL(ORDERTYPENAME,'') AS ORDERTYPENAME,ISNULL(COUNTRYID,'0') AS COUNTRYID,ISNULL(COUNTRYNAME,'') AS COUNTRYNAME," +
                        " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS" +
                        " FROM [T_STOCKTRANSFER_HEADER]  WITH (NOLOCK) WHERE STOCKTRANSFERID='" + transferid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            return dt;
        }

        public DataTable BindTransferDetails(string transferid)
        {
            string sql = " SELECT NEWID() AS GUID,PRODUCTID,PRODUCTNAME,BATCHNO,PACKINGSIZEID,PACKINGSIZENAME,TRANSFERQTY,MRP,RATE,AMOUNT,TOTALMRP," +
                         " ASSESMENTPERCENTAGE,TOTALASSESMENTVALUE,CONVERT(VARCHAR(10),CAST(MFDATE AS DATE),103) AS MFDATE," +
                         " CONVERT(VARCHAR(10),CAST(EXPRDATE AS DATE),103) AS EXPRDATE,NETWEIGHT,GROSSWEIGHT,'E' AS TAG,'0' AS BEFOREEDITEDQTY," +
                         " ISNULL(SALEORDERID,'0') AS SALEORDERID,ISNULL(SALEORDERNO,'') AS SALEORDERNO" +
                         " FROM [T_STOCKTRANSFER_DETAILS]  WITH (NOLOCK) WHERE STOCKTRANSFERID='" + transferid + "' ORDER BY SLNO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransferFooter(string transferid)
        {
            string sql = " SELECT ISNULL(GROSSAMOUNT,0) AS GROSSAMOUNT,ISNULL(ROUNDOFFVALUE,0) AS ROUNDOFFVALUE,ISNULL(NETAMOUNT,0) AS NETAMOUNT" +
                            " FROM T_STOCKTRANSFER_FOOTER" +
                            " WHERE STOCKTRANSFERID='" + transferid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteFactoryDispatch(string DispatchID)
        {
            int delflag = 0;

            string sqldeleteStockTransfer = "EXEC [SP_STOCK_DESPATCH_DELETE] '" + DispatchID + "'";

            int e = db.HandleData(sqldeleteStockTransfer);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }

        public string GetStockTransferstatus(string TransferID)
        {

            string Sql = "  IF  EXISTS(SELECT * FROM [T_DEPORECEIVED_STOCK_HEADER] WHERE STOCKTRANSFERID='" + TransferID + "') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";


            return ((string)db.GetSingleValue(Sql));

        }

        public int UpdateCForm(string TransferID, string CFormNo, string CFormDate)
        {
            int updateflag = 0;

            string sql = " UPDATE T_STOCKTRANSFER_HEADER SET FFORMNO = '" + CFormNo + "',FFORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
                         " WHERE STOCKTRANSFERID ='" + TransferID + "'";

            int e = db.HandleData(sql);

            if (e == 0)
            {
                updateflag = 0;  // delete unsuccessfull
            }
            else
            {
                sql = " UPDATE T_DEPORECEIVED_STOCK_HEADER SET FFORMNO = '" + CFormNo + "',FFORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
                         " WHERE STOCKTRANSFERID ='" + TransferID + "'";

                e = db.HandleData(sql);
                updateflag = 1;  // delete successfull
            }

            return updateflag;
        }

        public int CheckFormRequired(string TransferID)
        {
            int updateflag = 0;

            string sql = " SELECT COUNT(*) FROM [T_STOCKTRANSFER_HEADER] WHERE FORMREQUIRED='Y' AND STOCKTRANSFERID='" + TransferID + "'";

            int e = (int)db.GetSingleValue(sql);

            if (e > 0)
            {
                updateflag = 1;  // F FORM REQUIRED
            }
            else
            {
                updateflag = 0;  // F FORM NOT REQUIRED
            }
            return updateflag;
        }

        public decimal CalculateAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal Rate)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;

            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = Rate * RETURNVALUE;
            return Amount;
        }

        public DataTable Bindinscomp()
        {
            DataTable dtins = new DataTable();
            string sql = "SELECT ID,COMPANY_NAME FROM M_INSURANCECOMPANY ORDER BY COMPANY_NAME";
            dtins = db.GetData(sql);
            return dtins;
        }

        public DataTable BindinsNumber(string CompanyID)
        {
            DataTable dtins = new DataTable();
            string sql = " SELECT INSURANCE_NO FROM M_INSURANCEDOCUMENT" +
                         " WHERE COMAPANY_ID='" + CompanyID + "'";
            dtins = db.GetData(sql);
            return dtins;
        }

        public decimal GetPackingSize_OnCallCase(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT CASEPACKSIZEID FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;
        }

        public decimal GetPackingSize_OnCallPCS(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        #region Add By Rajeev For StockTransfer
        public DataSet EditStockTransferDetails(string DispatchID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_FACTORY_DESPATCH_DETAILS_RMPM '" + DispatchID + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "Header";
            ds.Tables[1].TableName = "Details";
            ds.Tables[2].TableName = "TaxCountDetails";
            ds.Tables[3].TableName = "ProductTaxDetails";
            ds.Tables[4].TableName = "FooterDetails";
            return ds;
        }
        #endregion

        #region Retrive HSN Code After Product Selection(Rajeev)
        public DataTable GETHSNCODE(string ProductCode)
        {
            string sql = " SELECT A.CATID,CASE WHEN A.HSNCODE IS NULL THEN A.CATNAME WHEN A.HSN='' THEN A.CATNAME ELSE A.CATNAME+'  ('+A.HSN+')' END AS CATNAME,ISNULL(A.HSN,'') AS HSN"
                        + " FROM M_HSNCODE A "
                        + " INNER JOIN M_PRODUCT AS B ON A.CATID=B.CATID"
                        + " WHERE B.ID='" + ProductCode + "'"
                        + " ORDER BY SEQUENCENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Add ItemWiseTaxCount For StockTransfer (Rajeev)
        public DataTable ItemWiseTaxCount_StockTransfer(string menuID, string flag, string VendorID, string ProductID, string CustomerID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            string StateID = string.Empty;

            sqlState = "SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM M_BRANCH WHERE BRID = '" + CustomerID + "'";
            StateID = (string)db.GetSingleValue(sqlState);

            if (flag == "1") //  Outer State
            {
                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "' " +
                      " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F1" +
                      " ORDER BY RELATEDTO ";
            }
            else // Inner State
            {
                sql = " SELECT TAXCOUNT,NAME ,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "'" +
                      " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F1" +
                      " ORDER BY RELATEDTO ";
            }
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Add BindRegion For StockTransfer(Rajeev)
        public string BindRegion_StockTransfer(string distid, string depotid)
        {
            //string sql = "SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM M_BRANCH WHERE BRID = '" + depotid + "'"; 
            string sql = " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM dbo.M_BRANCH " +
                         " WHERE STATEID IN(SELECT STATEID FROM dbo.M_BRANCH WHERE BRID='" + depotid + "')" +
                         " AND BRID='" + distid + "'";

            string region = (string)db.GetSingleValue(sql);
            return region;
        }
        #endregion

        public DataTable BindStockTranferForUnlock(string fromdate, string todate, string finyear, string depotid)
        {
            string SQL = string.Empty;
            SQL = " select STOCKTRANSFERID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS SALEINVOICEDATE,STOCKTRANSFERNO AS SALEINVOICENO," +
                       " TODEPOTID,TODEPOTNAME AS DISTRIBUTORNAME,'' AS NETAMOUNT,CHALLANNO AS GATEPASSNO" +
                       " from [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between " +
                       " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                       " AND FINYEAR ='" + finyear + "' AND MOTHERDEPOTID='" + depotid + "' AND DAYENDTAG='Y' AND SYNCTAG='Y'" +
                       " ORDER BY CREATEDDATE DESC";
            DataTable DT = db.GetData(SQL);
            return DT;
        }
        public DataTable BindFactoryStockTransfer(string fromdate, string todate, string finyear, string depotid, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            sql = " SELECT A.STOCKDESPATCHID AS SALEINVOICEID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS SALEINVOICENO,D.BRNAME AS DISTRIBUTORNAME,MOTHERDEPOTID AS DISTRIBUTORID, " +
                  " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.INVOICENO,'') AS GETPASSNO,TPUID AS DEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR, " +
                  " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE,CASE WHEN V.SUPLIEDITEMID ='1' THEN 'FG' ELSE 'POP' END BILLTYPE, " +
                  " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END " +
                  " AS ISVERIFIEDDESC,ISNULL(C.TOTALDESPATCHVALUE,0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE,ISNULL(C.ROUNDOFFVALUE,0) AS ROUNDOFFVALUE FROM T_STOCKDESPATCH_HEADER AS A " +
                  " INNER JOIN T_STOCKDESPATCH_FOOTER AS C ON A.STOCKDESPATCHID = C.STOCKDESPATCHID INNER JOIN M_TPU_VENDOR V ON A.TPUID=V.VENDORID " +
                  " INNER JOIN M_BRANCH D ON A.MOTHERDEPOTID=D.BRID " +
                  " WHERE CONVERT(DATE,STOCKDESPATCHDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                  " AND FINYEAR ='" + finyear + "'" +
                  " AND TPUID = '" + depotid + "' " +
                  " AND ISVERIFIED <> 'Y' " +
                  " AND NEXTLEVELID = '" + UserID + "'" +
                  " ORDER BY CONVERT(DATE,STOCKDESPATCHDATE,103) DESC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string TaxID(string TaxName)
        {
            string TaxID = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ID FROM M_TAX" +
                  " WHERE NAME='" + TaxName + "'";
            TaxID = (string)db.GetSingleValue(sql);
            return TaxID;
        }

        public decimal GetHSNTax(string TaxID, string ProductID, string Date)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','','" + ProductID + "','','" + Date + "')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        #region Add by Rajeev For GetHSNTaxOnEdit
        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string Batchno)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(TAXPERCENTAGE,0) FROM T_STOCKDESPATCH_ITEMWISE_TAX " +
                  " WHERE STOCKDESPATCHID='" + InvoiceID + "'" +
                  " AND TAXID='" + TaxID + "'" +
                  " AND PRODUCTID='" + ProductID + "'" +
                  " AND BATCHNO='" + Batchno + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
        #endregion

        public DataTable BindinscompIns(string MENUID)
        {
            DataTable dtins = new DataTable();
            string sql = " SELECT ID,COMPANY_NAME FROM M_INSURANCECOMPANY AS A " +
                         " INNER JOIN M_Insurance_Mappimg AS B ON A.ID=B.InsuranceID WHERE B.MenuID='" + MENUID + "'";
            dtins = db.GetData(sql);
            return dtins;
        }

        public DataTable LoadeStoreLocation(string userid)
        {
            DataTable dtins = new DataTable();
            string sql = "select STORELOCATIONNAME,STORELOCATIONID from M_USER_STORELOCATION_MAPPING where USERID='" + userid + "'";
            dtins = db.GetData(sql);
            return dtins;
        }
    }
}
