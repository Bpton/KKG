using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class ClsReceivedStock
    {
        DBUtils db = new DBUtils();
        DataTable dtStockReceivedRecord = new DataTable();

        public DataTable BindTPU_Transporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER WHERE ISAPPROVED='Y' AND ISDELETED = 'N' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotBasedOnUser(string UserID)
        {
            string sql = " SELECT BRID,BRNAME FROM M_BRANCH  " +
                         " INNER JOIN M_TPU_USER_MAPPING ON M_BRANCH.BRID=M_TPU_USER_MAPPING.TPUID " +
                         " AND M_TPU_USER_MAPPING.USERID='" + UserID + "' " +
                         " AND M_BRANCH.BRANCHTAG='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInsurancename()
        {
            string sql = "SELECT ID,COMPANY_NAME FROM M_INSURANCECOMPANY ORDER BY COMPANY_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPU()
        {
            string sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE  ISDELETED = 'N' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string DebitedTo(string ReasonID)
        {
            string ReturnValue = string.Empty;
            string sql = "SELECT DEBITEDTOID FROM M_REASON WHERE ID='" + ReasonID + "'";
            ReturnValue = (string)db.GetSingleValue(sql);
            return ReturnValue;
        }

        public decimal BindTPURate(string pid, string VendorID)
        {
            //string sql = "SELECT DISTINCT ISNULL(SUM(RATE),0) FROM [T_STOCKRECEIVED_DETAILS] WHERE PRODUCTID='" + pid + "' AND BATCHNO='" + batchno + "'";
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_TPU__PRODUCT_RATESHEET] where PRODUCTID='" + pid + "' AND VENDORID='" + VendorID + "') BEGIN " +
                       " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));

            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RMCOST+TRANSFERCOST,0) AS RATE FROM M_TPU__PRODUCT_RATESHEET" +
                             " WHERE PRODUCTID = '" + pid + "' AND VENDORID='" + VendorID + "'";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                price = 0;
            }
            return price;
        }

        public DataTable BindDespatchNo(string MOTHERDEPOTID)
        {
            string FinYear = Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim();
            string sql = "EXEC [USP_BIND_TPU_INVOICE_NO] '" + MOTHERDEPOTID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindEditedDespatchNo(string MOTHERDEPOTID)
        {
            string sql = " SELECT STOCKDESPATCHID,INVOICENO " +
                         //" (INVOICENO + ' ('+SUBSTRING(CASE WHEN TPUNAME LIKE '%(FACTORY)%' THEN 'FACTORY-UNIT-1 ' WHEN TPUNAME LIKE '%(FACTORY NEW)%' THEN 'FACTORY-UNIT-2 '" +
                         //"                            ELSE TPUNAME END,1,CHARINDEX(' '," +
                         //"                            CASE WHEN TPUNAME LIKE '%FACTORY%' THEN 'FACTORY-UNIT-1 ' WHEN TPUNAME LIKE '%FACTORY NEW%' THEN 'FACTORY-UNIT-2 ' " +
                         //"                                 ELSE TPUNAME END)-1)+')') AS INVOICENO" +
                         " FROM T_STOCKRECEIVED_HEADER" +
                         " WHERE ISCLOSED = 'N'" +
                         " AND FINYEAR='" + Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim() + "'" +
                         " AND MOTHERDEPOTID='" + MOTHERDEPOTID + "'";
            //" ORDER BY INVOICENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

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

        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "' ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPoDetails(string POID, string productID, string packSizeID)
        {
            string sql = "EXEC sp_PO_QC_ALLOCATED_DESPATCH_DETAILS '" + POID + "','" + productID + "','" + packSizeID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string BindDespatchIDChecked(string DespatchID)
        {
            string sql = "SELECT (STOCKRECEIVEDPREFIX + '/' + STOCKRECEIVEDNO + '/' + STOCKRECEIVEDSUFFIX) AS STOCKRECEIVEDNO FROM T_STOCKRECEIVED_HEADER WHERE STOCKDESPATCHID='" + DespatchID + "'";
            string RECEIVEDNO = (string)db.GetSingleValue(sql);
            return RECEIVEDNO;
        }

        public string Getstatus(string STOCKRECEIVEDID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID='" + STOCKRECEIVEDID + "'  AND DAYENDTAG='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindTax(string menuID)
        {
            string sql = " SELECT DISTINCT A.ID,A.NAME,A.PERCENTAGE,0 AS TAXVALUE" +
                         " FROM M_TAX AS A " +
                         " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                         " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                         " WHERE A.ACTIVE='True'" +
                         " AND A.RELATEDTO = 3" +
                         " AND B.MENUID = '" + menuID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ItemWiseTaxCount(string menuID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,A.NAME,A.PERCENTAGE,B.TAXID " +
                         " FROM M_TAX AS A" +
                         " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                         " WHERE A.ACTIVE='True'" +
                         " AND A.RELATEDTO = 1" +
                         " AND B.MENUID = '" + menuID + "'" +
                         " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID";

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTerms(string DespatchID)
        {
            string sql = " SELECT A.TERMSID,B.NAME,B.DESCRIPTION FROM T_STOCKDESPATCH_TERMS A INNER JOIN M_TERMSANDCONDITIONS B ON A.TERMSID=B.ID WHERE A.STOCKDESPATCHID='" + DespatchID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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

        public DataTable BindDepot()
        {
            string sql = " SELECT '-1' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'0' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-3' as BRID,'---- DEPOT ----' as BRNAME ,'3' AS SEQUENCE UNION SELECT BRID,BRNAME,'5' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Insert-Update Despatch
        public string InsertDespatchDetails(string receivedDate, string despatchID, string despatchDate, string tpuid, string tpuname, string wayBillKey,
                                            string wayBillNo, string InvoiceNo, string InvoiceDate, string TransporterID, string Vehicle, string DepotID,
                                            string DepotName, string LRGRNo, string LRGRDate, string CFormNo, string CFormDate, string GatepassNo,
                                            string GatepassDate, string TransportMode, int userID, string finyear, string Remarks, decimal TotalValue,
                                            decimal Othercharges, decimal Adjustment, decimal RoundOff, string strTermsID, string xml, string xmlTax,
                                            string xmlGrossTax, string xmlRejectionDetails, string hdnvalue, string Insurancecompid, string Insurancecompname,
                                            string Insuranceno, string MenuID, string SaleOrderID, string SaleOrderNo, decimal TotalCase, decimal TotalPCS,
                                            string InvoiceType, string Export, string DeliveryDate)
        {

            string flag = "";
            string ReceivedID = string.Empty;
            string ReceivedNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            dtStockReceivedRecord = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
            string sqlprocDespatch = " EXEC [SP_STOCK_RECEIVED_INSERT_UPDATE_FAC] '" + hdnvalue + "','" + flag + "','" + receivedDate + "','" + despatchID + "','" + despatchDate + "'," +
                                     " '" + tpuid + "','" + tpuname + "','" + wayBillKey + "','" + wayBillNo + "','" + InvoiceNo + "','" + InvoiceDate + "'," +
                                     " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                     " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                     " '" + CFormNo + "','" + CFormDate + "','" + GatepassNo + "','" + GatepassDate + "'," +
                                     " '" + TransportMode + "'," + userID + ",'" + finyear + "'," +
                                     " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                     " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlRejectionDetails + "'," +
                                     " '" + Insurancecompid + "','" + Insurancecompname + "','" + Insuranceno + "','" + MenuID + "'," +
                                     " '" + SaleOrderID + "','" + SaleOrderNo + "'," + TotalCase + "," + TotalPCS + ",'" + InvoiceType + "','" + Export + "','" + DeliveryDate + "'";
            DataTable dtReceived = db.GetData(sqlprocDespatch);

            if (dtReceived.Rows.Count > 0)
            {
                ReceivedID = dtReceived.Rows[0]["STOCKRECEIVEDID"].ToString();
                ReceivedNo = dtReceived.Rows[0]["STOCKRECEIVEDNO"].ToString();
            }
            else
            {
                ReceivedNo = "";
            }
            return ReceivedNo;
        }
        #endregion

        public string TaxID(string TaxName)
        {
            string TaxID = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ID FROM M_TAX" +
                  " WHERE NAME='" + TaxName + "'";
            TaxID = (string)db.GetSingleValue(sql);
            return TaxID;
        }

        public void ResetDataTables()
        {
            dtStockReceivedRecord.Clear();
            //dteditforecastRecord.Clear();
        }

        public DataTable BindReceived(string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_RECEIVED_FROMTPU '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteStockReceived(string ReceivedID)
        {
            int delflag = 0;

            string sqldeleteStockDespatch = "EXEC [SP_STOCK_RECEIVED_DELETE] '" + ReceivedID + "'";

            int e = db.HandleData(sqldeleteStockDespatch);

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

        public int RejectionRecordsDelete(string GUID, DataTable dtinnergrid)
        {
            int delflag = 0;
            int i = dtinnergrid.Rows.Count - 1;

            while (i >= 0)
            {
                if (dtinnergrid.Rows[i]["GUID"].ToString() == GUID)
                {

                    dtinnergrid.Rows[i].Delete();
                    dtinnergrid.AcceptChanges();
                    delflag = 1;
                    break;
                }
                i--;
            }
            return delflag;
        }

        public int UpdateWaybillNo(string DespatchID, string WaybillNo)
        {
            int updateflag = 0;

            string sql = " UPDATE T_STOCKDESPATCH_HEADER SET WAYBILLNO = '" + WaybillNo + "'" +
                         " WHERE STOCKDESPATCHID ='" + DespatchID + "'";

            int e = db.HandleData(sql);

            if (e == 0)
            {
                updateflag = 0;  // delete unsuccessfull
            }
            else
            {
                updateflag = 1;  // delete successfull
            }
            return updateflag;
        }

        public DataSet EditDespatchDetails(string DespatchID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_RECEIVED_DESPATCH_DETAILS '" + DespatchID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindPRoductDetails(string ProductID, string BatchNo)
        {
            string sql = "EXEC SP_PRODUCT_DETAILS '" + ProductID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet EditReceivedDetails(string ReceivedID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_RECEIVED_DETAILS_MMPP '" + ReceivedID + "','T'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable StoreLocationDetails(string ReasonID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT A.ID,A.NAME" +
                         " FROM  M_STORELOCATION AS A " +
                         " INNER JOIN M_REASON AS B ON A.ID=B.STORELOCATIONID" +
                         " WHERE B.ID='" + ReasonID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditModeQtyCheck(string poID, string productID, string despatchID, string packSizeTo)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "',T_STOCKDESPATCH_DETAILS.PACKINGSIZEID,'" + packSizeTo + "',T_STOCKDESPATCH_DETAILS.QTY)),0) AS QTY " +
                  " FROM T_STOCKDESPATCH_DETAILS" +
                  " WHERE POID = '" + poID + "'" +
                  " AND PRODUCTID = '" + productID + "'" +
                  " AND STOCKDESPATCHID <> '" + despatchID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable UpdateStock(string receiveddepotid, string receiveddepotname, string pid, string batchno, string finyear)
        {
            string sqlprocstock = "EXEC [SP_PRODUCTWISE_DEPOT_STOCK_MODIFICATION] '" + receiveddepotid + "','" + receiveddepotname + "','" + pid + "','" + batchno + "','" + finyear + "'";
            DataTable dtflag = db.GetData(sqlprocstock);
            return dtflag;
        }

        public decimal ConvertionQty(string ProductID, string PackSize, string ReceivedPacksize, decimal Qty)
        {
            decimal convertionqty = 0;
            string sql = string.Empty;
            sql = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + ProductID + "','" + PackSize + "','" + ReceivedPacksize + "'," + Qty + ")),0)";
            convertionqty = (decimal)db.GetSingleValue(sql);
            return convertionqty;
        }

        public string GetApproveStatus(string PURCHASERETURNID)
        {
            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID='" + PURCHASERETURNID + "' and ISVERIFIED='Y') BEGIN " +
                         "  SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }
         public string Updateledgerinfo(string receivedID,string ledgerinID)
        {
            
            string sql= "update T_STOCKRECEIVED_HEADER set LEDGERID = '"+ ledgerinID + "' where STOCKRECEIVEDID= '"+ receivedID+"'";

            return ((string)db.GetSingleValue(sql));
        }
        #region Update Exchange Rate
        public int UpdateExchangeRate(string StockReceivedID, string ExchangeRate)
        {
            string sql = string.Empty;
            int result = 0;
            sql = " UPDATE T_STOCKRECEIVED_DETAILS SET EXCHANGERATE = '" + ExchangeRate + " '" +
                  " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        public DataTable BindPurchaseApproval(string fromdate, string todate, string finyear, string depotid, string CheckerFlag, string UserID, string Tag)    /*Tag (T) = TPU , (F) = Factory */
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO + '/' + A.STOCKRECEIVEDSUFFIX) AS SALEINVOICENO,A.TPUNAME AS DISTRIBUTORNAME,TPUID AS DISTRIBUTORID, " +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE,(ISNULL(A.INVOICENO,'') + ' ( ' + CONVERT(VARCHAR(10),CAST(A.INVOICEDATE AS DATE),103) + ' )') AS GETPASSNO,MOTHERDEPOTID AS DEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR, " +
                      " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE," +
                      " CASE WHEN V.SUPLIEDITEMID ='1' THEN 'FG' WHEN V.SUPLIEDITEMID ='2' THEN 'PM' WHEN V.SUPLIEDITEMID ='3' THEN 'RM'  ELSE 'POP' END BILLTYPE, " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END " +
                      " AS ISVERIFIEDDESC,ISNULL(C.TOTALRECEIVEDVALUE,0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE,ISNULL(C.ROUNDOFFVALUE,0) AS ROUNDOFFVALUE,0 AS TOTALTDS FROM T_STOCKRECEIVED_HEADER AS A " +
                      " INNER JOIN T_STOCKRECEIVED_FOOTER AS C ON A.STOCKRECEIVEDID = C.STOCKRECEIVEDID INNER JOIN M_TPU_VENDOR V ON A.TPUID=V.VENDORID AND V.TAG = '" + Tag + "' " +
                      " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                      " AND FINYEAR ='" + finyear + "'" +
                      " AND MOTHERDEPOTID = '" + depotid + "' " +
                      " AND ISVERIFIED <> 'Y' " +
                      " AND NEXTLEVELID = '" + UserID + "'" +
                      " ORDER BY CONVERT(DATE,STOCKRECEIVEDDATE,103) ASC ";
            }
            else
            {
                sql = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO + '/' + A.STOCKRECEIVEDSUFFIX) AS SALEINVOICENO,A.TPUNAME AS DISTRIBUTORNAME,TPUID AS DISTRIBUTORID, " +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE,(ISNULL(A.INVOICENO,'') + ' ( ' + CONVERT(VARCHAR(10),CAST(A.INVOICEDATE AS DATE),103) + ' )') AS GETPASSNO,MOTHERDEPOTID AS DEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR, " +
                      " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE," +
                      " CASE WHEN V.SUPLIEDITEMID ='1' THEN 'FG' WHEN V.SUPLIEDITEMID ='2' THEN 'PM' WHEN V.SUPLIEDITEMID ='3' THEN 'RM'  ELSE 'POP' END BILLTYPE, " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END " +
                      " AS ISVERIFIEDDESC,ISNULL(C.TOTALRECEIVEDVALUE,0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE,ISNULL(C.ROUNDOFFVALUE,0) AS ROUNDOFFVALUE,0 AS TOTALTDS FROM T_STOCKRECEIVED_HEADER AS A " +
                      " INNER JOIN T_STOCKRECEIVED_FOOTER AS C ON A.STOCKRECEIVEDID = C.STOCKRECEIVEDID INNER JOIN M_TPU_VENDOR V ON A.TPUID=V.VENDORID AND V.TAG = '" + Tag + "' " +
                      " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                      " AND FINYEAR ='" + finyear + "'" +
                      " AND MOTHERDEPOTID = '" + depotid + "' " +
                      " AND ISVERIFIED <> 'Y' " +
                      " ORDER BY CONVERT(DATE,STOCKRECEIVEDDATE,103) ASC ";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}