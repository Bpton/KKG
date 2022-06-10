using DAL;
using System;
using System.Data;
using System.Web;
namespace BAL
{
    public class clsSaleInvoiceFG
    {
        DBUtils db = new DBUtils();
        public DataTable BindDepot_Transporter(string DepotID)
        {
            string sql = " SELECT A.ID,A.NAME " +
                         " FROM M_TPU_TRANSPORTER AS A " +
                         " INNER JOIN M_TRANSPOTER_DEPOT_MAP AS B " +
                         " ON A.ID=B.TRANSPOTERID" +
                         " WHERE B.DEPOTID='" + DepotID + "'" +
                         " AND B.TAG='D'" +
                         " ORDER BY A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindWaybill(string CustomeID)
        {
            string sql = " SELECT A.WAYBILLNO " +
                         " FROM M_WAYBILL AS A " +
                         " INNER JOIN M_CUSTOMER AS B ON A.STATEID = B.STATEID" +
                         " AND B.CUSTOMERID='" + CustomeID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer(string bsID, string groupid, string FactoryID)
        {
            string sql = "EXEC USP_BIND_CUSTOMER_MMPP_FG '" + FactoryID + "','" + bsID + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindSaleOrder(string CustomerID, string Finyear)
        {
            string sql = string.Empty;
            sql = " EXEC USP_BIND_SALEORDER_FG '" + CustomerID + "','" + Finyear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindBatchDetailsFreeProduct(string DepotID, string ProductID, string PacksizeID, string BatchNo, string CustomerID, string Date, string ModuleID, string BSID, string GroupID)
        {
            string sql = "EXEC SP_FREE_PRODUCT_BCP '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','" + CustomerID + "','" + Date + "','" + ModuleID + "','" + BSID + "','" + GroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindOrderProduct(string SaleOrderID)
        {
            string sql = string.Empty;
            sql = " SELECT A.PRODUCTID,B.PRODUCTALIAS AS PRODUCTNAME,C.GROUPID,CONVERT(VARCHAR(10),C.SALEORDERDATE,103) AS SALEORDERDATE FROM T_MM_SALEORDER_DETAILS A " +
                  " INNER JOIN T_MM_SALEORDER_HEADER C ON A.SALEORDERID=C.SALEORDERID INNER JOIN M_PRODUCT B ON A.PRODUCTID=B.ID " +
                  " WHERE A.SALEORDERID='" + SaleOrderID + "' ORDER BY B.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string Getstatus(string SALEINVOICEID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_MM_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCH_STATUS='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public string BindRegion(string distid, string depotid)
        {
            string sql = " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM dbo.M_CUSTOMER " +
                         " WHERE STATEID IN(SELECT STATEID FROM dbo.M_BRANCH WHERE BRID='" + depotid + "')" +
                         " AND CUSTOMERID='" + distid + "'";
            string region = (string)db.GetSingleValue(sql);
            return region;
        }

        public DataTable BindTax(string menuID, string flag, string VendorID, string ProductID, string CustomerID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            string StateID = string.Empty;
            sqlState = "SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            StateID = (string)db.GetSingleValue(sqlState);

            if (flag == "1") //  Outer State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                      " FROM M_TAX AS A " +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO = 3" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')";

            }
            else  // Inner State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                      " FROM M_TAX AS A " +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO = 3" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')";
            }
            dt = db.GetData(sql);
            return dt;
        }
        public decimal GetHSNTax(string TaxID, string ProductID, string Date)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','','" + ProductID + "','','" + Date + "')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string CustomerID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            string StateID = string.Empty;
            sqlState = "SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            StateID = (string)db.GetSingleValue(sqlState);
            if (flag == "1") //  Outer State
            {
                sql = " SELECT TAXCOUNT,TAXID,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                      " GROUP BY A.ID,A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                      " ORDER BY RELATEDTO ";
            }
            else // Inner State
            {
                sql = " SELECT TAXCOUNT,TAXID,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                      " GROUP BY A.ID,A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                      " ORDER BY RELATEDTO ";
            }
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet EditInvoiceDetails(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_SALE_INVOICE_DETAILS_MM '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
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
        public decimal QtyInCase(string productID, string packsizeFromID, string packsizeToID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + packsizeToID + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }
        public decimal CalculateAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            decimal FinalAmount = 0;
            string PackSizeTo = packsizeFromID;
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = MRP * RETURNVALUE;
            if (Percentage != 0 && Value == 0)
            {
                FinalAmount = (Amount - (Amount * Percentage) / 100);
            }
            else if (Percentage == 0 && Value != 0)
            {
                FinalAmount = Amount - Value;
            }
            else if (Percentage == 0 && Value == 0)
            {
                FinalAmount = Amount;
            }
            return FinalAmount;
        }

        #region Insert-Update Sale Invoice
        public string InsertInvoiceDetails(string InvoiceDate, string Customerid, string Customername, string wayBillNo,
                                           string TransporterID, string Vehicle, string DepotID, string DepotName, string BSID, string GroupID,
                                           string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate, string PaymentMode,
                                           string TransportMode, string userID, string finyear, string Remarks,
                                           decimal TotalValue, decimal Othercharges, decimal Adjustment,
                                           decimal RoundOff, decimal Disc, string strTermsID,
                                           string xmlInvoiceDetails, string xmlTax, string hdnvalue, string ModuleID, string DeliveryAddressID,
                                           string DeliveryAddress, string ICDSNo, string ICDSDate,
                                           string RebateSchemeID, decimal RebatePercentage, decimal RebateValue,
                                           decimal AddRebatePercentage, decimal AddRebateValue,
                                           string SaleOrderID, string CFormFlag, decimal TotalPCS,
                                           decimal TotActualCase, string InvoiceType, string GrossWght, string IsChallan, string Entry_From,
                                           string insCompID, string insCompName, string insuranceNumber, string ShippingAddress)
        {
            string flag = "";
            string InvoiceNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            string sqlprocInvoice = " EXEC [USP_SALE_INVOICE_INSERT_UPDATE_FG_FAC] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "'," +
                                    " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                    " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                    " '" + DepotName + "','" + BSID + "','" + GroupID + "'," +
                                    " '" + LRGRNo + "','" + LRGRDate + "'," +
                                    " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                    " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                    " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," +
                                    " " + Adjustment + "," + RoundOff + "," + Disc + "," +
                                    " '" + strTermsID + "','" + xmlInvoiceDetails + "','" + xmlTax + "'," +
                                    " '" + ModuleID + "'," +
                                    " '" + DeliveryAddressID + "','" + DeliveryAddress + "'," +
                                    " '" + ICDSNo + "','" + ICDSDate + "','" + RebateSchemeID + "'," +
                                    " " + RebatePercentage + "," + RebateValue + "," +
                                    " " + AddRebatePercentage + "," + AddRebateValue + "," +
                                    " '" + SaleOrderID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                    " " + TotActualCase + ",'" + InvoiceType + "'," +
                                    " '" + GrossWght + "','" + IsChallan + "','" + insCompID + "','" + insCompName + "','" + insuranceNumber + "','" + ShippingAddress + "','" + Entry_From + "'";
            DataTable dtDespatch = db.GetData(sqlprocInvoice);
            if (dtDespatch.Rows.Count > 0)
            {
                InvoiceNo = dtDespatch.Rows[0]["INVOICENO"].ToString();
            }
            else
            {
                InvoiceNo = "";
            }
            return InvoiceNo;
        }
        #endregion
        public DataTable BindFG_SaleInvoice(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID, string IsChallan)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_SALEINVOICE_FG '" + fromdate + "','" + todate + "','" + finyear + "','" + BSID + "','" + depotid + "','" + CheckerFlag + "','" + UserID + "','" + IsChallan + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int InvoiceDelete(string InvoiceID)
        {
            int delflag = 0;
            string sqldeleteInvoice = "EXEC [USP_SALE_INVOICE_DELETE_MM] '" + InvoiceID + "'";
            int e = db.HandleData(sqldeleteInvoice);
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
        public DataTable BindInvoiceWaybillFilter(string filterText, string finyear, string BSID, string UserID)
        {
            string sql = string.Empty;
            if (filterText == "1")
            {
                sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO + '/' + A.SALEINVOICESUFFIX) AS SALEINVOICENO," +
                      " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                      " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,ISNULL(A.FORMDATE,'') AS FORMDATE" +
                      " FROM T_MM_SALEINVOICE_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.WAYBILLNO = ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.BSID = '" + BSID + "'" +
                      " AND A.CREATEDBY='" + UserID + "'" +
                      " ORDER BY SALEINVOICEDATE DESC";
            }
            else if (filterText == "2")
            {
                sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO + '/' + A.SALEINVOICESUFFIX) AS SALEINVOICENO," +
                      " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                      " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,ISNULL(A.FORMDATE,'') AS FORMDATE" +
                      " FROM T_MM_SALEINVOICE_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.WAYBILLNO <> ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.BSID = '" + BSID + "'" +
                      " AND A.CREATEDBY='" + UserID + "'" +
                      " ORDER BY SALEINVOICEDATE DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int UpdateWaybillNo(string InvoiceID, string WaybillNo, string CustomerID)
        {
            int updateflag = 0;
            string sqlState = " SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            string StateID = (string)db.GetSingleValue(sqlState);

            string sqlInsert = " INSERT INTO M_WAYBILL(WAYBILLNO,STATEID,CBU,DTOC)" +
                               " VALUES('" + WaybillNo + "','" + StateID + "',0,GETDATE())";
            int e1 = db.HandleData(sqlInsert);

            string sql = " UPDATE T_MM_SALEINVOICE_HEADER SET WAYBILLNO = '" + WaybillNo + "'" +
                         " WHERE SALEINVOICEID ='" + InvoiceID + "'";

            int e = db.HandleData(sql);
            if (e == 0 && e1 == 0)
            {
                updateflag = 0;  // update unsuccessfull
            }
            else if (e > 0 && e1 > 0)
            {
                updateflag = 1;  // update successfull
            }
            return updateflag;
        }
        public DataTable GetBaseCostPrice(string CustomerID, string ProductID, string Date, decimal MRP, string DepotID, string MenuID, string BSID, string GroupID)
        {
            string sql = string.Empty;
            sql = " EXEC sp_GetBCP '" + CustomerID + "','" + ProductID + "','" + Date + "'," + MRP + ",'" + DepotID + "','" + MenuID + "','" + BSID + "','" + GroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSize(string Po_ID)
        {
            string sql = " SELECT PSID,PSNAME FROM Vw_PACKSIZE" +
                         " ORDER BY SEQUENCENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string CheckFormRequired(string DespatchID)
        {
            string result = string.Empty;
            string sql = " IF  EXISTS(SELECT * FROM [T_MM_SALEINVOICE_HEADER] WHERE FORMREQUIRED='Y' AND SALEINVOICEID = '" + DespatchID + "') BEGIN" +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END";
            result = (string)db.GetSingleValue(sql);
            return result;
        }
        public int UpdateCForm(string DespatchID, string CFormNo, string CFormDate)
        {
            int updateflag = 0;

            string sql = " UPDATE T_MM_SALEINVOICE_HEADER SET FORMNO = '" + CFormNo + "',FORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
                         " WHERE SALEINVOICEID ='" + DespatchID + "'";

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
        public DataTable DeliveryAddress(string CustomerID)
        {
            DataTable dtAddress = new DataTable();
            string sql = string.Empty;
            sql = " SELECT ADDRESSID,(ADDRESS+'-'+PINCODE) AS ADDRESS " +
                  " FROM M_CUSTOMER_ADDRESS" +
                  " WHERE CUSTOMERID='" + CustomerID + "'";
            dtAddress = db.GetData(sql);
            return dtAddress;
        }
        public string HSNCode(string ProductID)
        {
            string HSNCODE = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ISNULL(B.HSNCODE,'') AS HSNCODE FROM M_PRODUCT A INNER JOIN M_HSNCODE B ON A.CATID=B.CATID AND A.ID='" + ProductID + "'";
            HSNCODE = (string)db.GetSingleValue(sql);
            return HSNCODE;
        }
        public decimal GetTaxPercentage(string TaxID, string CustomerID, string ProductID, string Date)
        {
            decimal Taxpercent = 0;
            string SQLFunction = "EXEC USP_GETTAXPERCENTAGE '" + TaxID + "','" + CustomerID + "','" + ProductID + "','" + Date + "'";
            Taxpercent = (decimal)db.GetSingleValue(SQLFunction);
            return Taxpercent;
        }

        #region Add by Rajeev For GetHSNTaxOnEdit
        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(TAXPERCENTAGE,0) FROM T_MM_SALEINVOICE_ITEMWISE_TAX " +
                  " WHERE SALEINVOICEID='" + InvoiceID + "'" +
                  " AND TAXID='" + TaxID + "'" +
                  " AND PRODUCTID='" + ProductID + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
        #endregion

        #region Update LR/GR No       
        public string BindTransporter(string SALEINVOICEID, string module)
        {
            string SQL = string.Empty;
            if (module == "1")
            {
                SQL = "SELECT TRANSPORTERID FROM T_MM_SALEINVOICE_HEADER WHERE SALEINVOICEID='" + SALEINVOICEID + "'";
            }
            else if (module == "2")
            {
                SQL = " select  TRANSPORTERID  from [T_STOCKTRANSFER_HEADER] WHERE STOCKTRANSFERID='" + SALEINVOICEID + "'";
            }
            else
            {
                SQL = " select  TRANSPORTERID  from [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID='" + SALEINVOICEID + "'";
            }
            string id = (string)db.GetSingleValue(SQL);
            return id;
        }
        #endregion

        public DataTable BindOrderQty(string CustomerID, string SaleOrderID, string ProductID, string FactoryID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_ORDER_DESPATCH_QTY_FG '" + CustomerID + "','" + SaleOrderID + "','" + ProductID + "','" + FactoryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string CheckVerify(string SaleOrderID)
        {
            string Flag = "";
            string sql = "SELECT ISVERIFIED FROM T_MM_SALEINVOICE_HEADER WHERE SALEINVOICEID = '" + SaleOrderID + "' ";
            Flag = (string)db.GetSingleValue(sql);
            return Flag;
        }

        public decimal ConvertPcsToCase(string ProductID, string FromPackSize, string ToPackSize, decimal PcsQty)
        {
            decimal CaseQty = 0;
            string sql = "SELECT ISNULL((DBO.GETPACKINGSIZE_ONCALL('" + ProductID + "','" + FromPackSize + "','" + ToPackSize + "','" + PcsQty + "')),0)";
            CaseQty = (decimal)db.GetSingleValue(sql);
            return CaseQty;
        }

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

        public DataTable BindInvoiceApproval(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
                      " DEPOTID,SALEINVOICENO,SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE FROM VW_SALEINVOICE_APPROVAL " +
                        " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                        " AND FINYEAR ='" + finyear + "'" +
                        " AND BSID = '" + BSID + "'" +
                        " AND DEPOTID = '" + depotid + "'" +
                        " AND ISVERIFIED <> 'Y'" +
                        " AND NEXTLEVELID = '" + UserID + "'" +
                        " ORDER BY CONVERT(DATE,SALEINVOICEDATE,103) ASC";
            }
            else
            {
                if (depotid == "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670")
                {

                    sql = " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
                          " DEPOTID,SALEINVOICENO,SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE FROM VW_SALEINVOICE_APPROVAL " +
                          " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND BSID = '" + BSID + "'" +
                          " AND DEPOTID = '" + depotid + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " ORDER BY CONVERT(DATE,SALEINVOICEDATE,103) ASC";
                }

                else
                {

                    sql = " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE," +
                          " ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME, DEPOTID,SALEINVOICENO," +
                          " SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE " +
                          " FROM VW_SALEINVOICE_APPROVAL INNER JOIN M_DISTRIBUTER_CATEGORY " +
                          " ON VW_SALEINVOICE_APPROVAL.GROUPID=M_DISTRIBUTER_CATEGORY.DIS_CATID AND ISFINANCE_HO='Y'" +
                          " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                          " AND FINYEAR ='" + finyear + "'" +
                          " AND BSID = '" + BSID + "'" +
                          " AND DEPOTID = '" + depotid + "'" +
                          " AND ISVERIFIED <> 'Y'" +
                          " ORDER BY CONVERT(DATE,SALEINVOICEDATE,103) ASC";
                }
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTradingInvoiceApproval(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
                        " DEPOTID,SALEINVOICENO,SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE FROM VW_TRADING_SALEINVOICE_APPROVAL " +
                        " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                        " AND FINYEAR ='" + finyear + "'" +
                        " AND BSID = '" + BSID + "'" +
                        " AND DEPOTID = '" + depotid + "'" +
                        " AND ISVERIFIED <> 'Y'" +
                        " AND NEXTLEVELID = '" + UserID + "'" +
                        " ORDER BY CONVERT(DATE,SALEINVOICEDATE,103) ASC";
            }
            else
            {
                sql = " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
                        " DEPOTID,SALEINVOICENO,SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE FROM VW_TRADING_SALEINVOICE_APPROVAL " +
                        " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                        " AND FINYEAR ='" + finyear + "'" +
                        " AND BSID = '" + BSID + "'" +
                        " AND DEPOTID = '" + depotid + "'" +
                        " AND ISVERIFIED <> 'Y'" +
                        " ORDER BY CONVERT(DATE,SALEINVOICEDATE,103) ASC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
