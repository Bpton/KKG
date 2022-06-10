using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsGRNMM
    {
        DBUtils db = new DBUtils();
        DataTable dtStockDespatchRecord = new DataTable();

        public DataTable BindPOMM(string vendorid, string DepotID, string FinYear) /*NEW CONDITION ADD ISVERIFIED = 'Y' approved by p.basu on 06092019*/
        {
            string sql = "SELECT POID,PONO FROM T_MM_POHEADER WHERE VENDORID='" + vendorid + "' AND ISCLOSED='N'  AND ISVERIFIED ='Y' AND FACTORYID='" + DepotID + "' AND FINYEAR='" + FinYear + "' ORDER BY PONO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPOMMScheduleDateWise(string vendorid, string DepotID, string FinYear, string entrydate) /*NEW CONDITION ADD REQUIREDTODATE BY p.basu on 28122020*/
        {
            string sql = "USP_BIND_PO_REQUIREDTODATE_WISE '" + vendorid + "','" + DepotID + "','" + FinYear + "','" + entrydate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPoWiseProduct(string poid, string pono, string IssueProduct)
        {
            string sql = "sp_COMBO_GRN_DETAILS_MM '" + poid + "','" + pono + "','" + IssueProduct + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadProduct(string tpuid)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "SELECT A.ID,A.NAME FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B ON A.ID=B.PRODUCTID " +
                      "WHERE B.VENDORID='" + tpuid + "' ORDER BY A.NAME";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable GetPO_QtyCombo(string ProductID, string VendorID, string PacksizeID, string Finyear, string Tag)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_GRN_DETAILS '" + ProductID + "','" + VendorID + "','" + PacksizeID + "','" + Finyear + "','" + Tag + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public string GetProductExpirydate(string prodid, string date)
        {
            string Expdate = "";
            Expdate = (string)db.GetSingleValue("exec [sp_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
            return Expdate;
        }
        public int CFORM(string FromVendorID, string ToDepotID)
        {
            int flag = 0;
            string sql = "usp_get_single_value '"+ FromVendorID + "','"+ ToDepotID + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
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
                         //" INNER JOIN M_TRANSPOTER_TPU_MAP AS B ON A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         //" AND B.TPUID='" + VendorID + "'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string CheckLRGR(string LRGR, string DespatchID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_STOCKDESPATCH_HEADER] where LRGRNO='" + LRGR + "' AND STOCKDESPATCHID <> '" + DespatchID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }
        public string CheckInvoiceNo(string Invoiceno, string Tpuid, string Finyear)
        {
            string Sql = " IF EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE TPUID='" + Tpuid + "' AND FINYEAR = '" + Finyear + "' AND INVOICENO='" + Invoiceno + "') BEGIN " +
                         " SELECT '1' END  ELSE  BEGIN	 SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckInvoiceNoGRN(string Invoiceno, string Tpuid, string Finyear, string id) /*new add by p.basu 04032020*/
        {
            string Sql = "EXEC[USP_INVOICE_NO_CHECK_GRN] '" + Invoiceno + "','" + Tpuid + "','" + Finyear + "','" + id + "'";
            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckFormRequired(string DespatchID)
        {
            string result = string.Empty;
            string sql = " IF  EXISTS(SELECT * FROM [T_STOCKDESPATCH_HEADER] WHERE FORMFLAG='Y' AND STOCKDESPATCHID = '" + DespatchID + "') BEGIN" +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END";

            result = (string)db.GetSingleValue(sql);
            return result;
        }
        public DataTable FetchSuppliedItem(string VendorID)
        {
            string sql = " SELECT SUPLIEDITEM,SUPLIEDITEMID FROM M_TPU_VENDOR WHERE VENDORID='" + VendorID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPU(string TAG, string gatePassNo, string DepotID)
        {
            string sql = string.Empty;
            sql = "EXEC[USP_BIND_TPU_FROM_GATEPASSNO] '" + TAG + "','" + gatePassNo + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
            
        public DataTable BindTPU(string TAG, string DepotID)
        {
            string sql = string.Empty;
            if (TAG == "TRUE")
            {
                sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEM='FG' ORDER BY VENDORNAME";
            }
            else
            {                
                sql = "SELECT DISTINCT VENDORID,VENDORNAME FROM T_MM_POHEADER WHERE ISWORKORDER='N' AND FACTORYID='" + DepotID + "' ORDER BY VENDORNAME";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindIssueProduct(string DepotID)
        {
            string sql = string.Empty;
            sql = "SELECT DISTINCT A.ITEMID AS PRODUCTID,C.PRODUCTALIAS " +
                  "FROM T_PROCESSFRAMEWORK_MATERIAL AS A " +
                  "INNER JOIN M_PRODUCT			    AS C ON C.ID=A.ITEMID AND C.TYPE='PM' " +
                  "INNER JOIN M_PRODUCT_TPU_MAP	    AS D ON D.PRODUCTID=C.ID " +
                  "AND D.VENDORID='" + DepotID + "'" +
                  "ORDER BY C.PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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

        public DataTable BindFactory(string UserID)
        {
            string sql = " SELECT '-1' AS VENDORID,'--SELECT FACTORY--' AS VENDORNAME" +
                         " union" +
                         " select VENDORID, VENDORNAME   from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "' )";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPO(string tpuid, string Finyear)
        {
            string sql = " SELECT DISTINCT A.PONO,A.POID,(A.POPREFIX+'/'+A.PONUM+'/'+A.POSUFFIX) AS PONUMBER" +
                         " FROM T_TPU_POHEADER AS A WITH (NOLOCK) INNER JOIN T_TPU_INSTRUCTION_DETAILS AS B WITH (NOLOCK)" +
                         " ON A.POID = B.POID INNER JOIN T_TPU_INSTRUCTION_HEADER AS C " +
                         " ON C.INSTRUCTIONID = B.INSTRUCTIONID AND C.TPUID = '" + tpuid + "'" +
                         " AND A.FINYEAR = '" + Finyear + "'" +
                         " order by A.PONO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchNo(string POID, string PRODUCTID)
        {
            string sql = " SELECT DISTINCT ISNULL(BATCHNO,'') AS BATCHNO FROM T_TPU_QUALITYCONTROL_DETAILS " +
                         " WHERE POID='" + POID + "' AND PRODUCTID = '" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string PO_ID)
        {
            string sql = " SELECT DISTINCT CONVERT(VARCHAR(10),CAST(A.PODATE AS DATE),103) AS PODATE," +
                         " B.PRODUCTID,B.PRODUCTNAME" +
                         " FROM T_TPU_POHEADER A INNER JOIN T_TPU_PODETAILS B" +
                         " ON A.POID=B.POID" +
                         " WHERE B.POID='" + PO_ID + "'" +
                         "Union" +
                         " SELECT DISTINCT CONVERT(VARCHAR(10),CAST(A.PODATE AS DATE),103) AS PODATE," +
                         " B.PRODUCTID,B.PRODUCTNAME" +
                         " FROM T_Factory_POHEADER A INNER JOIN T_Factory_PODETAILS B" +
                         " ON A.POID=B.POID" +
                         " WHERE B.POID='" + PO_ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPackSize(string ProductID, string Tag)
        {
            string sql = string.Empty;
            //string sql = "SELECT PSID,PSNAME FROM Vw_SALEUNIT WHERE PRODUCTID = '" + Po_ID + "'";
            if (Tag == "TRUE")
            {
                sql = " SELECT PSID,PSNAME,RANK FROM  VW_PCS" +
                      " WHERE PRODUCTID = '" + ProductID + "'" +
                      " UNION ALL" +
                      " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                      " WHERE PSID NOT IN (SELECT PSID FROM  VW_PCS)" +
                      " AND PRODUCTID = '" + ProductID + "'" +
                      " ORDER BY RANK DESC";
            }
            else
            {
                sql = "SELECT PSID,PSNAME FROM Vw_MMUNIT WHERE PRODUCTID='" + ProductID + "'";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReason()
        {
            string sql = " SELECT A.ID,A.DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON" +
                         " A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal BindTPURate(string pid, string VendorID, string GRNDate)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_TPU__PRODUCT_RATESHEET] where PRODUCTID='" + pid + "' AND VENDORID='" + VendorID + "'  AND CONVERT(DATE,'" + GRNDate + "',103) BETWEEN FROMDATE AND TODATE) BEGIN " +
                       " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));

            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RMCOST+TRANSFERCOST,0) AS RATE FROM M_TPU__PRODUCT_RATESHEET" +
                             " WHERE PRODUCTID = '" + pid + "' AND VENDORID='" + VendorID + "'" +
                             " AND CONVERT(DATE,'" + GRNDate + "',103) BETWEEN FROMDATE AND TODATE";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                price = 0;
            }
            return price;
        }
        /*Add 2 Parameter FromDt and VendorId (Rajeev)*/
        public DataTable BindPoDetails(string POID, string productID, string packSizeID, string depotid, string TAG, string DespatchID, string Fromdt, string VendorId)
        {
            string sql = "EXEC SP_PO_GRN_DETAILS_MMPP '" + POID + "','" + productID + "','" + packSizeID + "','" + depotid + "','" + TAG + "','" + DespatchID + "','" + Fromdt + "','" + VendorId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string BindGrossWeight(string ProductID, string PacksizeToID)
        {
            string grossWeight = string.Empty;
            string sql = " SELECT ISNULL(CAST(ISNULL(GROSSWEIGHT,0) AS VARCHAR(20)) + ' ' + UOMNAME,'') AS GROSSWEIGHT  FROM M_PRODUCT_UOM_MAP" +
                         " WHERE PRODUCTID ='" + ProductID + "' AND" +
                         " PACKSIZEID_TO = '" + PacksizeToID + "'";
            grossWeight = (string)(db.GetSingleValue(sql));

            return grossWeight;
        }

        public string BindRegion(string tpuid, string depotid)
        {
            string sql = " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM dbo.M_TPU_VENDOR WHERE STATEID IN(SELECT STATEID FROM dbo.M_BRANCH WHERE BRID='" + depotid + "') AND VENDORID='" + tpuid + "'";
            string region = (string)db.GetSingleValue(sql);
            return region;
        }

        public DataTable BindTax(string menuID, string flag, string VendorID, string ProductID, string DepotID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "SELECT STATEID FROM M_TPU_VENDOR WHERE VENDORID = '" + DepotID + "'";
            StateID = (Int32)db.GetSingleValue(sqlState);

            if (flag == "1") //  Outer State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "','" + Date + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                      " FROM M_TAX AS A " +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO = 1" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')";

            }
            else  // Inner State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "','" + Date + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                      " FROM M_TAX AS A " +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO = 1" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')";
            }
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string DepotID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "select STATEID from M_TPU_VENDOR WHERE VENDORID='" + DepotID + "'";
            StateID = (Int32)db.GetSingleValue(sqlState);
            if (flag == "1") //  Outer State
            {
                //sql = " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +
                //      " FROM ( " +
                //      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                //      " FROM M_TAX AS A" +
                //      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                //      " WHERE A.ACTIVE='True'" +
                //      " AND A.RELATEDTO IN (1,4,5,7)" +
                //      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                //      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                //      " ORDER BY RELATEDTO ";

                sql = " SELECT TAXCOUNT ,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5,7)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                      " ORDER BY RELATEDTO ";
            }
            else // Inner State
            {
                //sql = " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +
                //      " FROM ( " +
                //      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                //      " FROM M_TAX AS A" +
                //      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                //      " WHERE A.ACTIVE='True'" +
                //      " AND A.RELATEDTO IN (1,4,5,7)" +
                //      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                //      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                //      " ORDER BY RELATEDTO ";

                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                      " FROM M_TAX AS A" +
                      " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                      " WHERE A.ACTIVE='True'" +
                      " AND A.RELATEDTO IN (1,4,5,7)" +
                      " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                      " ORDER BY RELATEDTO ";
            }
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTerms(string menuID)
        {
            string sql = " SELECT A.ID,A.DESCRIPTION" +
                         " FROM M_TERMSANDCONDITIONS AS A " +
                         " INNER JOIN M_TERMS_MENU_MAPPING AS B ON A.ID = B.TERMSID" +
                         " WHERE A.ISAPPROVED='Y'" +
                         " AND A.ISDELETED = 'N'" +
                         " AND B.MENUID = '" + menuID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPRoductDetails(string ProductID)
        {
            string sql = "EXEC SP_GRN_PRODUCT_DETAILS '" + ProductID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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

        public string DebitedTo(string ReasonID)
        {
            string ReturnValue = string.Empty;
            string sql = "SELECT DEBITEDTOID FROM M_REASON WHERE ID='" + ReasonID + "'";
            ReturnValue = (string)db.GetSingleValue(sql);
            return ReturnValue;
        }

        public DataSet Weight(string POID, string productID, string PacksizeID, decimal DespatchQty, string TAG)
        {
            DataSet dsWeight = new DataSet();
            string sql = string.Empty;
            sql = "EXEC SP_GRN_WEIGHTCALCULATION '" + POID + "','" + productID + "','" + PacksizeID + "'," + DespatchQty + ",'" + TAG + "'";
            dsWeight = db.GetDataInDataSet(sql);
            return dsWeight;
        }

        /*Add Invoice_Type Parameter (Rajeev-01-07-2017)*/
        #region Insert-Update Despatch
        public string InsertDespatchDetails(string GRNDate, string tpuid, string tpuname, string wayBillNo,
                                            string InvoiceNo, string InvoiceDate,
                                            string TransporterID, string Vehicle,
                                            string DepotID, string DepotName,
                                            string LRGRNo, string LRGRDate, string TransportMode,
                                            int userID, string finyear,
                                            string Remarks, decimal TotalValue, decimal Othercharges,
                                            decimal Adjustment, decimal RoundOff,
                                            string strTermsID, string CFormNo, string CFormDate, string Gatepassno,
                                            string gatepassdate, string FormFlag,
                                            string xml, string xmlTax, string xmlGrossTax,
                                            string xmlRejection, string xmladddetails,
                                            decimal AddAmount, string hdnvalue, string insCompID,
                                            string insCompName, string insuranceNumber, string MenuID,
                                            string DespatchID, int Invoice_Type,
                                            string ISVERIFIEDCHECKER1, string ISVERIFIEDSTOCKIN,
                                            string xmlRejectionTax,
                                            decimal TotalItemWiseFreight,
                                            decimal TotalItemWiseAddCost,
                                            decimal TotalItemWiseDisct,
                                            string xmlJobOrderReceive,
                                            string LedgerID, string WayBillDt,
                                            string xmlSampleQty, string xmlSampleQtyFileUpload,
                                            string CapacityFileUpload, string VendorFrom,
                                            decimal txtTCSPercent, decimal txtTCS,
                                            decimal txtTCSNetAmt, decimal txtTCSApplicable,
                                            string storelocation,string txtDevationno,string txtDevationdate
                                           )
        {
            string flag = "";
            string GRNID = string.Empty;
            string GRNNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
                try
                {
                    string sqlprocDespatch = " EXEC [SP_GRN_INSERT_UPDATE_MMPP] '','','" + flag + "','" + GRNDate + "','" + tpuid + "','" + tpuname + "'," +
                                             " '" + wayBillNo + "','" + InvoiceNo + "','" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "'," +
                                             " '" + Gatepassno + "','" + gatepassdate + "','" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlRejection + "'," +
                                             " '" + xmladddetails + "','" + AddAmount + "','" + insCompID + "','" + insCompName + "','" + insuranceNumber + "'," +
                                             " '" + MenuID + "','" + Invoice_Type + "','" + ISVERIFIEDCHECKER1 + "','" + ISVERIFIEDSTOCKIN + "','" + xmlRejectionTax + "'," +
                                             " '" + TotalItemWiseFreight + "','" + TotalItemWiseAddCost + "','" + TotalItemWiseDisct + "','" + xmlJobOrderReceive + "'," +
                                             " '" + LedgerID + "','" + WayBillDt + "','" + xmlSampleQty + "','" + xmlSampleQtyFileUpload + "','" + CapacityFileUpload + "','" + VendorFrom + "','" + txtTCSPercent + "','" + txtTCS + "','" + txtTCSNetAmt + "','" + txtTCSApplicable + "','" + storelocation + "','"+ txtDevationno+"','"+ txtDevationdate+"'";

                    DataTable dtDespatch = db.GetData(sqlprocDespatch);
                    if (dtDespatch.Rows.Count > 0)
                    {
                        GRNID = dtDespatch.Rows[0]["STOCKRECEIVEDID"].ToString();
                        GRNNo = dtDespatch.Rows[0]["STOCKRECEIVEDNO"].ToString();
                    }
                    else
                    {
                        GRNNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            else
            {
                flag = "U";
                try
                {
                    string sqlprocDespatch = " EXEC [SP_GRN_INSERT_UPDATE_MMPP] '" + hdnvalue + "','" + DespatchID + "','" + flag + "'," +
                                             " '" + GRNDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "','" + InvoiceNo + "'," +
                                             " '" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "'," +
                                             " '" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlRejection + "'," +
                                             " '" + xmladddetails + "','" + AddAmount + "','" + insCompID + "','" + insCompName + "'," +
                                             " '" + insuranceNumber + "','" + MenuID + "','" + Invoice_Type + "'," +
                                             " '" + ISVERIFIEDCHECKER1 + "','" + ISVERIFIEDSTOCKIN + "','" + xmlRejectionTax + "','" + TotalItemWiseFreight + "'," +
                                             " '" + TotalItemWiseAddCost + "','" + TotalItemWiseDisct + "','" + xmlJobOrderReceive + "','" + LedgerID + "'," +
                                             " '" + WayBillDt + "','" + xmlSampleQty + "','" + xmlSampleQtyFileUpload + "','" + CapacityFileUpload + "','" + VendorFrom + "','" + txtTCSPercent + "','" + txtTCS + "','" + txtTCSNetAmt + "','" + txtTCSApplicable + "','" + storelocation + "','" + txtDevationno + "','" + txtDevationdate + "'";
                    DataTable dtDespatch = db.GetData(sqlprocDespatch);

                    if (dtDespatch.Rows.Count > 0)
                    {
                        GRNID = dtDespatch.Rows[0]["STOCKRECEIVEDID"].ToString();
                        GRNNo = dtDespatch.Rows[0]["STOCKRECEIVEDNO"].ToString();
                    }
                    else
                    {
                        GRNNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            return GRNNo;
        }

        #endregion
        /*Change SP Name From SP_RECEIVED_DETAILS To SP_RECEIVED_DETAILS_MMPP (Rajeev-06-07-2017)*/
        public DataSet EditReceivedDetails(string ReceivedID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_RECEIVED_DETAILS_MMPP '" + ReceivedID + "','T'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindReceived(string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID, string TPUFLAG, string OP)
        {
            string sql = string.Empty;
            sql = "EXEC USP_GRN_SEARCH_MM '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "','" + TPUFLAG + "','" + OP + "'";
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

        public void ResetDataTables()
        {
            dtStockDespatchRecord.Clear();
        }

        public DataTable BindDespatch(string fromdate, string todate, string finyear, string TPUID)
        {
            string sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                         " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY," +
                         " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                         " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                         " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                         " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) " +
                         " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                         " WHERE CONVERT(DATE,STOCKDESPATCHDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                         " AND A.FINYEAR ='" + finyear + "'" +
                         " AND A.TPUID = '" + TPUID + "'" +
                         " ORDER BY CREATEDDATE DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDespatchWaybillFilter(string filterText, string finyear, string TPUID)
        {
            string sql = string.Empty;
            if (filterText == "1")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.WAYBILLKEY = '0'" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "2")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE (A.WAYBILLKEY <> '0' OR A.WAYBILLKEY <> '')" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "3")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.CFORMNO <> ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "4")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B WITH (NOLOCK) ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.CFORMNO = ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDespatchCformFilter(string filterText, string finyear, string TPUID)
        {
            string sql = string.Empty;
            if (filterText == "1")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.FORMREQUIRED = ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "2")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A " +
                      " INNER JOIN M_TPU_TRANSPORTER AS B ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.FORMREQUIRED <> ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetDespatchstatus(string DespatchID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKDESPATCHID='" + DespatchID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }

        public int DeleteStockDespatch(string DespatchID)
        {
            int delflag = 0;
            string sqldeleteStockDespatch = "EXEC [SP_STOCK_RECEIVED_DELETE] '" + DespatchID + "'";
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

        public int DeleteJobOrderTemp(string PoID, string BulkProductID, decimal RecevdQty)
        {
            int delflag = 0;
            string sqlJobOrderTempDelete = "EXEC SP_JOBORDER_DELETE_TEMP '" + PoID + "','" + BulkProductID + "'," + RecevdQty + "";
            int e = db.HandleData(sqlJobOrderTempDelete);
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

        public int DeleteJobOrderTemp()
        {
            int delflag = 0;
            string sqlJobOrderTempDelete = "EXEC SP_JOBORDER_DELETE_TEMP";
            int e = db.HandleData(sqlJobOrderTempDelete);
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

        public int UpdateWaybillNo(string DespatchID, string WaybillNo, string WaybillKey)
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
                string sql = " UPDATE T_STOCKDESPATCH_HEADER SET WAYBILLNO = '" + WaybillNo + "',WAYBILLKEY = '" + WaybillKey + "'" +
                             " WHERE STOCKDESPATCHID ='" + DespatchID + "'";

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

        public int UpdateCForm(string DespatchID, string CFormNo, string CFormDate)
        {
            int updateflag = 0;

            string sql = " UPDATE T_STOCKDESPATCH_HEADER SET CFORMNO = '" + CFormNo + "',CFORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
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
            string sql = "EXEC sp_DESPATCH_DETAILS '" + DespatchID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
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

        public DataTable BindWaybill(string DepotID)
        {

            string sql = " SELECT A.WAYBILLNO " +
                         " FROM M_WAYBILL AS A " +
                         " INNER JOIN M_BRANCH AS B ON A.STATEID = B.STATEID" +
                         " AND B.BRID='" + DepotID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWaybillEdit(string DepotID)
        {
            string sql = " SELECT A.WAYBILLNO " +
                         " FROM M_WAYBILL AS A " +
                         " INNER JOIN M_BRANCH AS B ON A.STATEID = B.STATEID " +
                         " AND B.BRID='" + DepotID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)" +
                         " UNION ALL" +
                         " SELECT A.WAYBILLNO  FROM M_WAYBILL AS A " +
                         " INNER JOIN M_BRANCH AS B ON A.STATEID = B.STATEID " +
                         " AND B.BRID='" + DepotID + "'" +
                         " AND A.WAYBILLNO IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal CalculateNonFGQty(string PRODUCTID, decimal QTY, string FROMPACKSIZEID)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            string TOPACKSIZE = dt.Rows[0]["PACKSIZE"].ToString();
            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        public decimal CalculateNonFGRejectionQty(string PRODUCTID, decimal QTY, string FROMPACKSIZEID)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            string TOPACKSIZE = dt.Rows[0]["UOMID"].ToString();
            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        public decimal CalculateNonFGAmount(string PRODUCTID, string FROMPACKSIZEID, string QTY, decimal Rate)
        {
            decimal RETURNVALUE = 0;
            decimal RETURNAMOUNT = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            string TOPACKSIZE = dt.Rows[0]["UOMID"].ToString();
            decimal UnitValue = Convert.ToDecimal(dt.Rows[0]["UNITVALUE"].ToString());
            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            //RETURNAMOUNT = (Rate / UnitValue) * RETURNVALUE;
            RETURNAMOUNT = (Rate * RETURNVALUE);
            return RETURNAMOUNT;
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

        public decimal ConvertionRejectionQty(string ProductID, string FromPackSize, decimal Qty)
        {
            decimal convertionqty = 0;
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + ProductID + "','" + FromPackSize + "','" + RETURNPACKSIZE + "'," + Qty + ")),0)";
            convertionqty = (decimal)db.GetSingleValue(sql);
            return convertionqty;
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

        public decimal BindDepotRate(string Productid, decimal MRP, string InvoiceDate)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_DEPOT_TRANSFER_RATESHEET] where PRODUCTID='" + Productid + "' AND CONVERT(DATE,'" + InvoiceDate + "',103) BETWEEN FROMDATE AND TODATE) BEGIN " +
                       " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));

            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_DEPOT_TRANSFER_RATESHEET" +
                            " WHERE PRODUCTID = '" + Productid + "' AND CONVERT(DATE,'" + InvoiceDate + "',103) BETWEEN FROMDATE AND TODATE ";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                string sql = "EXEC SP_CALCULATE_DEPOT_RATE " + MRP + ",'" + Productid + "'";
                price = (decimal)db.GetSingleValue(sql);
            }
            return price;
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

        #region Add by Rajeev For GST
        public decimal GetHSNTax(string TaxID, string ProductID, string VendorId, string Date)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','" + VendorId + "','" + ProductID + "','','" + Date + "')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
        #endregion

        #region Add by Rajeev For GetHSNTaxOnEdit
        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = "USP_GetHSNTaxOnEdit_KKG '" + InvoiceID + "','" + TaxID + "','" + ProductID + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
        #endregion

        #region Fetch QCID
        public DataTable FetchQCID(string GrnID)
        {
            string sql = "Select QCID from T_MM_QUALITYCONTROL_HEADER where GRNID='" + GrnID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindVendorDisc(string VendorId, string ProductId, string FromDt, string ToDt)
        {
            //decimal price = 0;
            DataTable dt = new DataTable();
            string sql = "EXEC USP_FETCH_DISCOUNT '" + VendorId + "','" + ProductId + "','" + FromDt + "','" + ToDt + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SundryDetails(string MenuID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DETAILSID,DETAILSNAME FROM M_ADDITIONALDETAILS_MAP ORDER BY DETAILSNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable TaxPercentage(string DETAILSID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT PERCENTAGE,REFERENCELEDGERID FROM M_ADDITIONALDETAILS_MAP WHERE DETAILSID = '" + DETAILSID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchGRNStatus(string fromdate, string todate, string depotid, string finyear)
        {
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_GRNSTATUS '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWorkOrderReceived(string POID, string PRODUCTID, string DEPOTID, string FINYEAR, string JobOrderRecvd)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_WORKORDER_RECEIVED '" + POID + "','" + PRODUCTID + "','" + DEPOTID + "','" + FINYEAR + "','" + JobOrderRecvd + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string InsertJobOrderReceived(string POID, string STOCKDESPATCHID, string XmlJobDetails)
        {
            string StockID = "";
            DataTable dtJobOrder = new DataTable();
            //string JobOrder = "SP_JOBORDER_RECEIVED_SAVE '" + POID + "','" + STOCKDESPATCHID + "','" + XmlJobDetails + "'";
            string JobOrder = "SP_JOBORDER_RECEIVED_TEMP '" + XmlJobDetails + "'";
            dtJobOrder = db.GetData(JobOrder);
            if (dtJobOrder.Rows.Count > 0)
            {
                StockID = dtJobOrder.Rows[0]["STOCKDESPATCHID"].ToString();
            }
            return StockID;
        }
        #region GRN Status Report modification by HPDAS on 30.01.19
        public DataTable FetchGRNStatusV2(string fromdate, string todate, string depotid, string finyear, string grnstatus, string ReportType, string Productid)
        {
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_GRNSTATUS_V2 '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "','" + grnstatus + "','" + ReportType + "','" + Productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind Issue PO Wise
        public DataTable BindIssuePOWise(string POID)
        {
            string sql = string.Empty;
            sql = " SELECT DISTINCT A.ISSUEID,B.ISSUENO FROM T_MM_STOCKDESPATCH_ISSUENO AS A" +
                  " INNER JOIN T_ISSUE_HEADER AS B ON A.ISSUEID=B.ISSUEID" +
                  " WHERE A.POID='" + POID + "'" +
                  " ORDER BY B.ISSUENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region GetChecker1 Status  (Rajeev 06-05-2019)
        public string GetChecker1Status(string StockreceivedID)
        {
            string Sql = " IF EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID='" + StockreceivedID + "' AND ISVERIFIEDCHECKER1='Y') BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN	SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        public DataTable PoCloseAuto(string vendorid, string FinYear, string DepotID) /*NEW CONDITION ADD ISVERIFIED = 'Y' approved by p.basu on 06092019*/
        {
            string sql = "EXEC USP_PURCHASE_ORDER_AUTO_CLOSING '" + vendorid + "','" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReceiveQty(string ReceivedID)
        {
            string sql = "EXEC SP_BIND_RECEIVE_QTY '" + ReceivedID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSampleQty(string ReceivedID)
        {
            string sql = "SELECT STOCKRECEIVEDID,POID,PRODUCTID,PRODUCTNAME,RECEIVEDQTY,SAMPLEQTY,OBSERVATIONQTY " +
                         "FROM T_STOCKRECEIVED_SAMPLEQTY WHERE STOCKRECEIVEDID='" + ReceivedID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetFile_SR_Capacity(string StockreceivedID)
        {
            string sql = "SELECT FILENAME FROM [T_STOCKRECEIVED_CAPACITY_UPLOAD] WHERE STOCKRECEIVEDID='" + StockreceivedID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGatePassno(string MODE)/*finyear new add by p.basu on 08-04-2021*/
        {
            string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_CHECK_STATUS '"+MODE+"','','','"+ FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int RejectGrn(string id, string userid, string mode) /*new added by p.basu on 12122019*/
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "usp_reject_grn '" + id + "','" + userid + "','" + mode + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        #region BindStoreLocation
        public DataTable BindStoreLocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION WITH(NOLOCK)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public string checkeGrnStatus(string StockreceivedID)
        {
            string Sql = "if exists(select 1 from T_MM_QUALITYCONTROL_HEADER WITH(NOLOCK) where GRNID='" + StockreceivedID + "')select 'Y' else select 'N'";
            return ((string)db.GetSingleValue(Sql));
        }

        public string checkQCQA(string StockreceivedID)
        {
            string Sql = "if ((select ISVERIFIED from T_MM_QUALITYCONTROL_HEADER WITH(NOLOCK) where GRNID='" + StockreceivedID + "')='Y') select 'Y' else select 'N'";
            return ((string)db.GetSingleValue(Sql));
        }
        public string checkQCQA(string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID, string TPUFLAG, string OP, string StockreceivedID)
        {
            string Sql = "EXEC usp_get_isverify '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "','" + TPUFLAG + "','" + OP + "','" + StockreceivedID + "'";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindGrnProductList()
        {
            string sql = "SELECT DISTINCT PRODUCTID,PRODUCTALIAS " +
                         "FROM T_STOCKRECEIVED_DETAILS AS A " +
                         "INNER JOIN M_PRODUCT AS B ON B.ID = A.PRODUCTID " +
                         "ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public int updateTableValue(string id,string DespatchNo, string WayBill, string EInvoiceNo, decimal Roundoff, decimal FinalAmt, decimal TCSApplicable,decimal TCS,decimal TCSNetAmt)
        {
            int flag = 0;
            string sql = "USP_UPDATE_STOCKRECEIVED_ITEMS '"+ id + "','" + DespatchNo + "','" + WayBill + "','" + EInvoiceNo + "','" + Roundoff + "','" + FinalAmt + "','" + TCSApplicable + "','" + TCS + "','" + TCSNetAmt + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
        }
    }
}
