using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class ClsPurchaseReturn_MM
    {
        DBUtils db = new DBUtils();
        DataTable dtSaleInvoiceRecord = new DataTable();

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

        public DataTable BindSuppliedItem()
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT ID AS SUPPLIEDITEMID,ITEM_Name AS SUPPLIEDITEMNAME FROM M_SUPLIEDITEM " +
                        " WHERE ID NOT IN ('9','8','5','10','12','15','16')";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindSuppliedItem_NRGP()
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT ID AS SUPPLIEDITEMID,ITEM_Name AS SUPPLIEDITEMNAME FROM M_SUPLIEDITEM " +
                        " WHERE ID NOT IN ('1','9','8','5','10','12','15','16')";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindVendor(string SuppliedItemID)
        {
            string sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WITH(NOLOCK)"+
                         " WHERE SUPLIEDITEMID = '" + SuppliedItemID + "'";
                         //" AND TAG <> 'F'" +
                         //" ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSize(string Po_ID)
        {
            string sql = " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                         " WHERE PSID NOT IN (SELECT PSID FROM  VW_PCS)" +
                         " AND PRODUCTID = '" + Po_ID + "'" +
                         " ORDER BY RANK";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInvoice(string fromdate, string todate, string CustID, string FinYear)
        {
            string sql = " SELECT SALEINVOICEID AS SALEINVOICEID, (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS SALEINVOICENO" +
                         " FROM T_SALEINVOICE_HEADER" +
                         " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                         " AND DISTRIBUTORID = '" + CustID + "'" +
                         " AND FINYEAR = '" + FinYear + "'" +
                         " ORDER BY SALEINVOICEDATE DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet InvoiceDetails(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC sp_SALE_INVOICE_DETAILS '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindPurchaseReturn(string fromdate, string todate, string finyear, string DepotID, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            sql = " EXEC USP_PURCHASERETURN_SEARCH '" + fromdate + "','" + todate + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "','" + DepotID + "'";
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

        public DataSet BindPurchaseInvoiceProductDetails(string InvoiceID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = " EXEC USP_PURCHASE_INVOICE_DETAILS '" + InvoiceID + "'";
                ds = db.GetDataInDataSet(sql);
                ds.Tables[0].TableName = "BillDetails";
                ds.Tables[1].TableName = "TaxCount";
                ds.Tables[2].TableName = "TaxDetails";
                ds.Tables[3].TableName = "Footer";
                ds.Tables[4].TableName = "Header";
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return ds;
        }

        public DataSet BindPurchaseInvoiceRejectionWise(string InvoiceID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = " EXEC USP_PURCHASE_INVOICE_DETAILS_REJECTIONWISE '" + InvoiceID + "'";
                ds = db.GetDataInDataSet(sql);
                ds.Tables[0].TableName = "BillDetails";
                ds.Tables[1].TableName = "TaxCount";
                ds.Tables[2].TableName = "TaxDetails";
                ds.Tables[3].TableName = "Footer";
                ds.Tables[4].TableName = "Header";
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return ds;
        }

        public DataTable BindDepotBasedOnUser(string UserID)
        {
            string sql = " SELECT BRID,BRNAME FROM M_BRANCH INNER JOIN M_TPU_USER_MAPPING " +
                         " ON M_BRANCH.BRID=M_TPU_USER_MAPPING.TPUID " +
                         " AND M_TPU_USER_MAPPING.USERID='" + UserID + "' " +
                         " AND M_BRANCH.BRANCHTAG='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendorInvoice(string VendorID, string FromDate, string ToDate, string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "EXEC USP_VENDOR_INVOICE_DETAILS '" + VendorID + "','" + FromDate + "','" + ToDate + "','" + DepotID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }

        public DataTable BindVendorInvoice(string RejectionID, string VendorID, string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "EXEC USP_REJECTIONWISE_INVOICE_DETAILS '" + RejectionID + "','" + VendorID + "','" + DepotID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }

        public DataTable BindProductOnInvoice(string InvoiceID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM T_SALEINVOICE_DETAILS WHERE SALEINVOICEID='" + InvoiceID + "' ORDER BY PRODUCTNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }

        public DataSet EditPurchaseReturnDetails(string PURCHASERETURNID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_PURCHASE_RETURN_DETAILS '" + PURCHASERETURNID + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "HEADER";
            ds.Tables[1].TableName = "BILLDETAILS";
            ds.Tables[2].TableName = "FOOTERDETAILS";
            ds.Tables[3].TableName = "TAXCOUNT";
            ds.Tables[4].TableName = "TAXDETAILS";
            return ds;
        }

        /*public string Getstatus(string SALEINVOICEID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_PURCHASERETURN_HEADER] WHERE PURCHASERETURNID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCTAG='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }*/

        public decimal QtyInPcs(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        public string GetPrimaryPriceScheme(string ProductID, decimal Qty, string Packsize, string CustomerID, string SaleOrderID, string Date, string BSID, string GroupID)
        {
            string RETURNVALUE = string.Empty;
            string StateID = string.Empty;
            string DistrictID = string.Empty;
            string Zone = string.Empty;
            string Territory = string.Empty;
            DataTable dtCustomer = new DataTable();
            DataTable dtSaleOrder = new DataTable();

            string sql = " SELECT A.BUSINESSSEGMENTID,A.GROUPID,A.STATEID,A.DISTRICTID,B.Zone,ISNULL(C.TERRITORYID,'') AS TERRITORYID" +
                         " FROM M_CUSTOMER AS A " +
                         " INNER JOIN M_REGION AS B ON A.STATEID = B.State_ID" +
                         " LEFT JOIN  M_TERRITORYDETAILS AS C ON A.STATEID = C.STATEID AND A.DISTRICTID = C.DISTRICTID" +
                         " WHERE A.CUSTOMERID='" + CustomerID + "'";
            dtCustomer = db.GetData(sql);
            if (dtCustomer.Rows.Count > 0)
            {
                StateID = Convert.ToString(dtCustomer.Rows[0]["STATEID"]).Trim();
                DistrictID = Convert.ToString(dtCustomer.Rows[0]["DISTRICTID"]).Trim();
                Zone = Convert.ToString(dtCustomer.Rows[0]["Zone"]).Trim();
                Territory = Convert.ToString(dtCustomer.Rows[0]["TERRITORYID"]).Trim();
            }
            string SQLFunction = "SELECT ISNULL(DBO.fn_GetPriceScheme('" + Date + "','" + StateID + "','" + DistrictID + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Qty + ",'" + Packsize + "','" + Zone + "','" + Territory + "' ),'')";
            RETURNVALUE = (string)db.GetSingleValue(SQLFunction);
            return RETURNVALUE;
        }
        public DataTable GetPriceSchemeParam(string Param)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT * FROM dbo.fnSplit('" + Param + "','~')";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetPrimaryQtyScheme(string ProductID, decimal Qty, string Packsize, string CustomerID, string SaleOrderID, string Date, string BSID, string GroupID, string DepotID, string ModuleID, string BatchNo)
        {
            DataTable dt = new DataTable();
            string StateID = string.Empty;
            string DistrictID = string.Empty;
            string Zone = string.Empty;
            string Territory = string.Empty;
            DataTable dtCustomer = new DataTable();
            DataTable dtSaleOrder = new DataTable();
            string sql = " SELECT A.BUSINESSSEGMENTID,A.GROUPID,A.STATEID,A.DISTRICTID,B.Zone,ISNULL(C.TERRITORYID,'') AS TERRITORYID" +
                         " FROM M_CUSTOMER AS A " +
                         " INNER JOIN M_REGION AS B ON A.STATEID = B.State_ID" +
                         " LEFT JOIN  M_TERRITORYDETAILS AS C ON A.STATEID = C.STATEID AND A.DISTRICTID = C.DISTRICTID" +
                         " WHERE A.CUSTOMERID='" + CustomerID + "'";
            dtCustomer = db.GetData(sql);
            if (dtCustomer.Rows.Count > 0)
            {
                StateID = Convert.ToString(dtCustomer.Rows[0]["STATEID"]).Trim();
                DistrictID = Convert.ToString(dtCustomer.Rows[0]["DISTRICTID"]).Trim();
                Zone = Convert.ToString(dtCustomer.Rows[0]["Zone"]).Trim();
                Territory = Convert.ToString(dtCustomer.Rows[0]["TERRITORYID"]).Trim();
            }
            string SQLFunction = "EXEC SP_GetQTYScheme '" + Date + "','" + StateID + "','" + DistrictID + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Qty + ",'" + Packsize + "','" + Zone + "','" + Territory + "','" + CustomerID + "','" + DepotID + "','" + ModuleID + "','" + BatchNo + "',F";
            dt = db.GetData(SQLFunction);
            return dt;
        }
        public decimal CalculateAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            decimal FinalAmount = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

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

        public decimal CalculateTotalMRPInPcs(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = MRP * RETURNVALUE;
            return Amount;
        }
        public decimal CalculateDiscAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            decimal FinalAmount = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = MRP * RETURNVALUE;
            if (Percentage != 0 && Value == 0)
            {
                FinalAmount = (Amount * Percentage) / 100;
            }
            else if (Percentage == 0 && Value != 0)
            {
                FinalAmount = Value;
            }
            else if (Percentage == 0 && Value == 0)
            {
                FinalAmount = 0;
            }
            return FinalAmount;
        }

        public string NetWeight(string ProductID)
        {
            string sql = string.Empty;
            string returnValue = string.Empty;
            sql = " SELECT	ISNULL((B.UNITVALUE +' '+ D.UOMNAME),'')  AS NETWEIGHT" +
                  " FROM M_PRODUCT B  INNER JOIN M_UOM AS D ON D.UOMID = B.UOMID " +
                  " WHERE B.ID = '" + ProductID + "'";
            returnValue = (string)db.GetSingleValue(sql);
            return returnValue;
        }

        public DataTable BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetBaseCostPrice(string CustomerID, string ProductID, string Date, decimal MRP, string DepotID, string MenuID, string BSID, string GroupID)
        {
            string sql = string.Empty;
            sql = " EXEC sp_GetBCP '" + CustomerID + "','" + ProductID + "','" + Date + "'," + MRP + ",'" + DepotID + "','" + MenuID + "','" + BSID + "','" + GroupID + "','F'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string BindRegion_DMS(string distid, string depotid)
        {
            string sql = " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM dbo.M_CUSTOMER " +
                         " WHERE STATEID IN(SELECT STATEID FROM dbo.M_CUSTOMER WHERE CUSTOMERID='" + distid + "')" +
                         " AND CUSTOMERID='" + distid + "'";
            string region = (string)db.GetSingleValue(sql);
            return region;
        }

        public string BindRegion(string distid, string depotid)
        {
            string sql = " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM dbo.M_CUSTOMER " +
                         " WHERE STATEID IN(SELECT STATEID FROM dbo.M_BRANCH WHERE BRID='" + depotid + "')" +
                         " AND CUSTOMERID='" + distid + "'";
            string region = (string)db.GetSingleValue(sql);
            return region;
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
                sql = " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6" +
                      " ORDER BY RELATEDTO ";
            }
            else // Inner State
            {
                sql = " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6" +
                      " ORDER BY RELATEDTO ";
            }

            dt = db.GetData(sql);
            return dt;
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

        #region Insert-Update Without Sale Order Sale Invoice
        public string InsertPurchaseReturnInvoiceDetails(string ReturnDate, string Customerid, string Customername,
                                                            string DepotID, string DepotName, string userID, string finyear,
                                                            string Remarks, string SuppliedItemID, string SuppliedItemName,
                                                            decimal TotalPcs, decimal TotalInvoiceValue,
                                                            decimal Othercharges, decimal Adjustment,
                                                            decimal RoundOff, decimal NetAmount,
                                                            decimal GrossAmount, decimal FreightCharges,
                                                            string xmlReturnInvoiceDetails, string xmlTax,
                                                            string hdnvalue,
                                                            int Invoice_Type,
                                                            string ModuleID,
                                                            string InvoiceNo, string InvoiceDt, string Lrgrno, string LrgrDt,
                                                            string TransporterId, string VehicheleNo, string ReturnType,
                                                            string GrnRejectionNO
                                                        )
        {

            string flag = "";
            string InvoiceID = string.Empty;
            string ReturnNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
                try
                {
                    string sqlprocInvoice = "EXEC [USP_PURCHASE_RETURNINVOICE_INSERT_UPDATE] '','" + flag + "','" + ReturnDate + "'," +
                                            " '" + Customerid + "','" + Customername + "'," +
                                            " '" + DepotID + "','" + DepotName + "','" + userID + "'," +
                                            " '" + finyear + "','" + Remarks + "'," + TotalPcs + "," +
                                            " " + TotalInvoiceValue + "," + Othercharges + "," + Adjustment + "," +
                                            " " + RoundOff + "," + NetAmount + "," + GrossAmount + "," + FreightCharges + "," +
                                            " '" + SuppliedItemID + "','" + SuppliedItemName + "'," +
                                            " '" + xmlReturnInvoiceDetails + "','" + xmlTax + "','" + Invoice_Type + "','" + ModuleID + "'," +
                                            " '" + InvoiceNo + "','" + InvoiceDt + "','" + Lrgrno + "','" + LrgrDt + "','" + TransporterId + "'," +
                                            " '" + VehicheleNo + "','" + ReturnType + "','" + GrnRejectionNO + "'";
                    DataTable dtDespatch = db.GetData(sqlprocInvoice);

                    if (dtDespatch.Rows.Count > 0)
                    {
                        InvoiceID = dtDespatch.Rows[0]["RETURNINVOICEID"].ToString();
                        ReturnNo = dtDespatch.Rows[0]["RETURNINVOICENO"].ToString();
                    }
                    else
                    {
                        ReturnNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            else
            {
                /*dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "U";
                try
                {
                    string sqlprocInvoice = " EXEC [USP_SALE_RETURNINVOICE_INSERT_UPDATE] '" + hdnvalue .Trim()+ "','" + flag + "','" + InvoiceDate + "','" + Customerid + "','" + Customername + "'," +
                                             " '" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," + SpecialDisc + "," +
                                             " '" + strTermsID + "'," +
                                             " '" + bsID + "','" + groupID + "','" + OrderDate + "'," +
                                             "'" + xmlInvoiceDetails + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlQtyScheme + "','" + xmlOrderInvoice + "'," +
                                             "'" + xmlProductDetails + "','" + TypeID + "','" + TypeName + "','" + SFID + "','" + SFName + "'," + NetAmount + "";
                    DataTable dtDespatch = db.GetData(sqlprocInvoice);

                    if (dtDespatch.Rows.Count > 0)
                    {
                        InvoiceID = dtDespatch.Rows[0]["INVOICEID"].ToString();
                        InvoiceNo = dtDespatch.Rows[0]["INVOICENO"].ToString();
                    }
                    else
                    {
                        InvoiceNo = "";
                    }

                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }*/
            }
            return ReturnNo;
        }

        #endregion

        public int InvoiceDelete(string InvoiceID)
        {
            int delflag = 0;
            string sqldeleteInvoice = "EXEC [USP_PURCHASE_RETURN_DELETE] '" + InvoiceID + "'";
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

        public string Getstatus(string SALEINVOICEID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_PURCHASERETURN_HEADER] WHERE PURCHASERETURNID='" + SALEINVOICEID + "' AND ISVERIFIED='Y') BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN  SELECT '0'  END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindTPU_Transporter(string VendorID)
        {
            string sql = " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A " +
                         " INNER JOIN M_TRANSPOTER_TPU_MAP AS B ON A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         " AND B.TPUID='" + VendorID + "'" +

                         " UNION ALL" +

                         " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A " +
                         " WHERE A.ISDELETED = 'N'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_GrnRejection(string MODE,string depotid) /*logic change by p.basu on 20-02-2021*/
        {
            string sql = " USP_REJECTED_QC '"+ MODE + "','"+depotid+"' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurchaseReturnApproval(string fromdate, string todate, string finyear, string depotid, string CheckerFlag, string UserID)    /*Tag (T) = TPU , (F) = Factory */
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT A.PURCHASERETURNID AS SALEINVOICEID, (A.PURCHASERETURNPREFIX + '/' + A.PURCHASERETURNNO) AS SALEINVOICENO, " +
                        " A.VENDORNAME AS DISTRIBUTORNAME,A.VENDORID AS DISTRIBUTORID, " +
                        " CONVERT(VARCHAR(10), CAST(A.PURCHASERETURNDATE AS DATE), 103) AS SALEINVOICEDATE,(ISNULL(A.WAYBILLNO, '') + ' ( ' + A.WAYBILLKEY + ' )') AS GETPASSNO," +
                        " DEPOTID AS DEPOTID, ISNULL(A.FINYEAR, '') AS FINYEAR," +
                        " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE," +
                        " CASE WHEN V.SUPLIEDITEMID = '1' THEN 'FG' WHEN V.SUPLIEDITEMID = '2' THEN 'PM' WHEN V.SUPLIEDITEMID = '3' THEN 'RM'  ELSE 'POP' END BILLTYPE," +
                        " CASE WHEN ISVERIFIED = 'N' THEN 'PENDING'  WHEN ISVERIFIED = 'R' THEN 'REJECTED' WHEN ISVERIFIED = 'H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                        " ISNULL(C.NETAMOUNT, 0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE, ISNULL(C.ROUNDOFFVALUE, 0) AS ROUNDOFFVALUE,0 AS TOTALTDS " +
                        " FROM T_PURCHASERETURN_HEADER AS A " +
                        " INNER JOIN T_PURCHASERETURN_FOOTER AS C ON A.PURCHASERETURNID = C.PURCHASERETURNID INNER JOIN M_TPU_VENDOR V ON A.VENDORID = V.VENDORID " +
                        " WHERE CONVERT(DATE, PURCHASERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                        " AND FINYEAR = '" + finyear + "' " +
                        " AND DEPOTID = '" + depotid + "' " +
                        " AND ISVERIFIED<> 'Y'" +
                        " AND NEXTLEVELID = '" + UserID + "' " +
                        " ORDER BY CONVERT(DATE, PURCHASERETURNDATE, 103) ASC";
            }
            else
            {
                sql = " SELECT A.PURCHASERETURNID AS SALEINVOICEID, (A.PURCHASERETURNPREFIX + '/' + A.PURCHASERETURNNO) AS SALEINVOICENO, " +
                        " A.VENDORNAME AS DISTRIBUTORNAME,A.VENDORID AS DISTRIBUTORID, " +
                        " CONVERT(VARCHAR(10), CAST(A.PURCHASERETURNDATE AS DATE), 103) AS SALEINVOICEDATE,(ISNULL(A.WAYBILLNO, '') + ' ( ' + A.WAYBILLKEY + ' )') AS GETPASSNO," +
                        " DEPOTID AS DEPOTID, ISNULL(A.FINYEAR, '') AS FINYEAR," +
                        " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE," +
                        " CASE WHEN V.SUPLIEDITEMID = '1' THEN 'FG' WHEN V.SUPLIEDITEMID = '2' THEN 'PM' WHEN V.SUPLIEDITEMID = '3' THEN 'RM'  ELSE 'POP' END BILLTYPE," +
                        " CASE WHEN ISVERIFIED = 'N' THEN 'PENDING'  WHEN ISVERIFIED = 'R' THEN 'REJECTED' WHEN ISVERIFIED = 'H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                        " ISNULL(C.NETAMOUNT, 0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE, ISNULL(C.ROUNDOFFVALUE, 0) AS ROUNDOFFVALUE,0 AS TOTALTDS " +
                        " FROM T_PURCHASERETURN_HEADER AS A " +
                        " INNER JOIN T_PURCHASERETURN_FOOTER AS C ON A.PURCHASERETURNID = C.PURCHASERETURNID INNER JOIN M_TPU_VENDOR V ON A.VENDORID = V.VENDORID " +
                        " WHERE CONVERT(DATE, PURCHASERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                        " AND FINYEAR = '" + finyear + "' " +
                        " AND DEPOTID = '" + depotid + "' " +
                        " AND ISVERIFIED<> 'Y'" +
                        " ORDER BY CONVERT(DATE, PURCHASERETURNDATE, 103) ASC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
