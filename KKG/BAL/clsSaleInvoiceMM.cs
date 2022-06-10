using DAL;
using System;
using System.Data;

namespace BAL
{
    public class clsSaleInvoiceMM
    {
        DBUtils db = new DBUtils();
        public DataTable BindDepot_Transporter(string DepotID)
        {
            string sql = " SELECT A.ID,A.NAME " +
                         " FROM M_TPU_TRANSPORTER AS A " +
                         " INNER JOIN M_TRANSPOTER_DEPOT_MAP AS B ON A.ID=B.TRANSPOTERID " +
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
                         " INNER JOIN M_CUSTOMER AS B ON A.STATEID = B.STATEID " +
                         " AND B.CUSTOMERID='" + CustomeID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer(string bsID, string groupid, string FactoryID)
        {
            string sql = "EXEC USP_BIND_CUSTOMER_MMPP '" + FactoryID + "','" + bsID + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindSaleOrder(string CustomerID)
        //{
        //    string sql = string.Empty;
        //    sql = "EXEC USP_BIND_SALORDER_AND_BS '" + CustomerID + "'";                  
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}

        public DataSet BindSaleOrder(string CustomerID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_BIND_SALORDER_AND_BS '" + CustomerID + "'";
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
        public DataTable BindInvoiceProduct(string CustomerID, string BSID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO, A.CATNAME,A.DIVNAME,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF'" +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE ='GIFT' " +
                      " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }


        public DataTable BindStoreDetails(string userid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                //sql = " EXEC USP_SALEINVOICEQTY_BASEDONPRODUCT_ERP '" + InvoiceID + "'";
                sql = " EXEC usp_storelocation_checking '" + userid + "'";
                dt = db.GetData(sql);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }

        public DataTable BindInvoiceCategoryProduct(string CustomerID, string BSID, string CategoryID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO, A.CATNAME,A.DIVNAME,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF'" +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "' AND A.CATID='" + CategoryID + "'" +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE ='GIFT' AND CATID='" + CategoryID + "'" +
                      " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }
        public DataTable BindOrderProduct(string mode,string SaleOrderID)  /*inline to sp convertion by p.basu for kkg on 16-12-2020*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Empty;
                sql = "USP_LOAD_PRODUCT_TRADING_SALE_INVOCIE_KKG '"+ mode + "','" + SaleOrderID + "' ";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }
        public string Getstatus(string SALEINVOICEID)
        {

            string Sql = " IF  EXISTS(SELECT 1 FROM [T_MM_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCH_STATUS='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT UOMID,UOMNAME FROM M_UOM WHERE UOMID NOT IN('21BC00F3-2D4F-497F-BF52-5A8324DD0C37','972E37AF-2A3C-4702-988A-7693E5B26081') ORDER BY UOMNAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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
                                           decimal TotActualCase, string InvoiceType, string GrossWght, string IsChallan,
                                           string insCompID, string insCompName, string insuranceNumber, string billingType, string IsTcs,
                                           decimal TCSPercent, decimal TCSAmount, decimal Netwithtcsamt, decimal TCSApplicable,decimal freightamnt,string billingAdress
                                          ,string ShippingCustomerid)
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
            string sqlprocInvoice = " EXEC [USP_SALE_INVOICE_INSERT_UPDATE_MM_FAC] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "'," +
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
                                    " '" + GrossWght + "','" + IsChallan + "','" + insCompID + "','" + insCompName + "','" + insuranceNumber + "'," +
                                    "'" + billingType + "','" + IsTcs + "'," +
                                    " " + TCSPercent + "," + TCSAmount + "," + Netwithtcsamt + "," + TCSApplicable + "," + freightamnt + ",'"+ billingAdress + "','" + ShippingCustomerid + "'";

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
        public DataTable BindInvoice(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID, string IsChallan)
        {
            string sql = string.Empty;
            sql = " EXEC [USP_BIND_TRADING_SALE_INVOICE_FACTORY] '" + fromdate + "','" + todate + "','" + finyear + "','" + BSID + "','" + depotid + "','" + CheckerFlag + "','" + UserID + "','" + IsChallan + "'";
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
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID " +
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
        public DataTable BindBranch()
        {
            string sql = "SELECT BRID,BRNAME AS BRPREFIX FROM M_BRANCH ORDER BY BRPREFIX";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindOrderQty(string CustomerID, string SaleOrderID, string ProductID, string FactoryID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_ORDER_DESPATCH_QTY_MM '" + CustomerID + "','" + SaleOrderID + "','" + ProductID + "','" + FactoryID + "'";
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

        public DataTable BindTcsPercent(string CustomerID)
        {
            string sql = string.Empty;
            sql = "SELECT TCSPERCENT FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
