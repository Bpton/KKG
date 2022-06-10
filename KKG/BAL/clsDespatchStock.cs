#region Developer Info

/*
 Developer Name : Rajeev Kumar
 * Start Date   : 21/02/2020
 * End Date     :
*/

#endregion

using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class clsDespatchStock
    {
        DBUtils db = new DBUtils();
        DataTable dtStockDespatchRecord = new DataTable();

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

        public DataTable GetPO_QtyCombo(string ProductID, string VendorID, string PacksizeID, string Finyear, string type, string DespatchID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_DESPATCH_QTY_DETAILS '" + ProductID + "','" + VendorID + "','" + PacksizeID + "','" + Finyear + "','" + DespatchID + "','" + type + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetPO_QtyCombo_Export(string ProductID, string VendorID, string PacksizeID, string Finyear, string type, string DespatchID, string AllocationID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_DESPATCH_QTY_DETAILS_EXPORT '" + ProductID + "','" + VendorID + "','" + PacksizeID + "'," +
                     " '" + Finyear + "','" + DespatchID + "','" + AllocationID + "','" + type + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public int CFORM(string FromVendorID, string ToDepotID)
        {
            int flag = 0;
            string sql = " SELECT CASE WHEN A.STATEID = B.STATEID THEN 1 ELSE 0 END SAMESTATE FROM M_TPU_VENDOR A,M_BRANCH B " +
                         " WHERE A.VENDORID='" + FromVendorID + "' AND B.BRID='" + ToDepotID + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
        }

        public DataTable BindTPU_Transporter()
        {
            string sql = " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                              " M_TRANSPOTER_TPU_MAP AS B ON" +
                              " A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         " AND  B.TPUID='" + Convert.ToString(HttpContext.Current.Session["TPUID"]) + "'" +
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

        public string CheckInvoiceNo(string LRGR, string DespatchID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_STOCKDESPATCH_HEADER] where INVOICENO='" + LRGR + "' AND STOCKDESPATCHID <> '" + DespatchID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";


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

        public DataTable BindTPU(string UserID)
        {
            string sql = " SELECT '-1' AS VENDORID,'--SELECT TPU--' AS VENDORNAME" +
                         " union" +
                         " select VENDORID,VENDORNAME from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "' and Tag='T'  )";
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
                         " select VENDORID, VENDORNAME   from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "'  and Tag='F'  )";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPO(string tpuid, string Finyear)
        {
            string sql = " SELECT DISTINCT A.PONO,A.POID,(A.POPREFIX+'/'+A.PONUM+'/'+A.POSUFFIX) AS PONUMBER" +
                         " FROM T_TPU_POHEADER AS A INNER JOIN T_TPU_INSTRUCTION_DETAILS AS B" +
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

        public DataTable BindPackSize(string Po_ID)
        {
            //string sql = "SELECT PSID,PSNAME FROM Vw_SALEUNIT WHERE PRODUCTID = '" + Po_ID + "'";
            string sql = " SELECT PSID,PSNAME,RANK FROM  VW_PCS" +
                          " WHERE PRODUCTID = '" + Po_ID + "'" +
                          " UNION ALL" +
                          " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                          " WHERE PSID NOT IN (SELECT PSID FROM  VW_PCS)" +
                          " AND PRODUCTID = '" + Po_ID + "'" +
                          " ORDER BY RANK DESC";
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

        public DataTable BindPoDetails(string POID, string productID, string batchno, string packSizeID, string depotid, string VENDORID, string DespatchID, string DespatchDt)
        {
            string TPUTAG = (string)db.GetSingleValue("SELECT  [TAG] FROM [M_TPU_VENDOR] WHERE  VENDORID='" + VENDORID + "'");
            string sql = "EXEC sp_PO_QC_ALLOCATED_DESPATCH_DETAILS '" + POID + "','" + productID + "','" + batchno + "','" + packSizeID + "','" + depotid + "','" + TPUTAG + "','" + DespatchID + "','" + VENDORID + "','" + DespatchDt + "'";
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

        public DataTable BindTax(string menuID, string flag, string VendorID, string ProductID, string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "SELECT STATEID FROM M_BRANCH WHERE BRID = '" + DepotID + "'";
            StateID = (Int32)db.GetSingleValue(sqlState);

            if (flag == "1") //  Outer State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                         " FROM M_TAX AS A " +
                         " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                         " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                         " WHERE A.ACTIVE='True'" +
                         " AND A.RELATEDTO = 3" +
                         " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')";

            }
            else  // Inner State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
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

        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "select STATEID from M_BRANCH WHERE BRID='" + DepotID + "'";
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
                //      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 where PERCENTAGE > 0" +
                //      " ORDER BY RELATEDTO ";
                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
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
                //      " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 where PERCENTAGE > 0" +
                //      " ORDER BY RELATEDTO ";

                sql = " SELECT TAXCOUNT ,NAME ,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
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
                         " FROM M_TERMSANDCONDITIONS AS A INNER JOIN M_TERMS_MENU_MAPPING AS B" +
                         " ON A.ID = B.TERMSID" +
                         " WHERE A.ISAPPROVED='Y'" +
                         " AND A.ISDELETED = 'N'" +
                         " AND B.MENUID = '" + menuID + "'";
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

        public decimal GetPackingSize_OnCall(string productID, string packsizeFromID, decimal Qty)
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

        public DataSet Weight(string POID, string productID, string PacksizeID, decimal DespatchQty)
        {
            DataSet dsWeight = new DataSet();
            string sql = string.Empty;
            sql = "EXEC SP_WEIGHTCALCULATION '" + POID + "','" + productID + "','" + PacksizeID + "'," + DespatchQty + "";
            dsWeight = db.GetDataInDataSet(sql);
            return dsWeight;

        }

        public DataTable BindDepot()
        {
            string sql = "SELECT '-1' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'0' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-3' as BRID,'---- DEPOT ----' as BRNAME ,'3' AS SEQUENCE UNION SELECT BRID,BRNAME,'5' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Insert-Update Despatch
        public string InsertDespatchDetails(string despatchDate, string tpuid, string tpuname, string wayBillNo, string InvoiceNo, string InvoiceDate,
                                             string TransporterID, string Vehicle, string DepotID, string DepotName,
                                             string LRGRNo, string LRGRDate, string TransportMode, int userID, string finyear,
                                             string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment, decimal RoundOff,
                                             string strTermsID, string CFormNo, string CFormDate, string Gatepassno, string gatepassdate, string FormFlag,
                                             string xml, string xmlTax, string xmlGrossTax, string hdnvalue, string insCompID, string insCompName, string insuranceNumber,
                                             decimal TotalCase, decimal TotalPCS, string Export, string CountryID, string CountryName,
                                             string AllocationID, string AllocationNo, string InvoiceType)
        {

            string flag = "";
            string DespatchID = string.Empty;
            string DespatchNo = string.Empty;
            if (hdnvalue == "")
            {
                dtStockDespatchRecord = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                flag = "A";
                try
                {
                    string sqlprocDespatch = " EXEC [SP_STOCK_DESPATCH_INSERT_UPDATE] '','" + flag + "','" + despatchDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "'," +
                                             " '" + InvoiceNo + "','" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "','" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + insCompID + "','" + insCompName + "'," +
                                             " '" + insuranceNumber + "'," + TotalCase + "," + TotalPCS + ",'" + Export + "','" + CountryID + "','" + CountryName + "'," +
                                             " '" + AllocationID + "','" + AllocationNo + "','" + InvoiceType + "'";
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
            }
            else
            {
                dtStockDespatchRecord = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                flag = "U";
                try
                {

                    string sqlprocDespatch = " EXEC [SP_STOCK_DESPATCH_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + despatchDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "'," +
                                             " '" + InvoiceNo + "','" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "','" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + insCompID + "','" + insCompName + "'," +
                                             " '" + insuranceNumber + "'," + TotalCase + "," + TotalPCS + ",'" + Export + "','" + CountryID + "','" + CountryName + "'," +
                                             " '" + AllocationID + "','" + AllocationNo + "','" + InvoiceType + "'";
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
            }

            return DespatchNo;
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
            dtStockDespatchRecord.Clear();
        }

        public DataTable BindDespatch(string fromdate, string todate, string finyear, string TPUID, string Export)
        {
            string sql = string.Empty;
            if (Export.Trim() == "N")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                      " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                      " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE CONVERT(DATE,STOCKDESPATCHDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " AND A.EXPORT='N'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (Export.Trim() == "Y")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                      " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                      " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE CONVERT(DATE,STOCKDESPATCHDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " AND A.EXPORT='Y'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDespatchWaybillFilter(string filterText, string finyear, string TPUID, string Export)
        {
            string sql = string.Empty;
            if (filterText == "1")
            {
                if (Export.Trim() == "Y")
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.WAYBILLKEY = '0'" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='Y'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.WAYBILLKEY = '0'" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='N'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "2")
            {
                if (Export.Trim() == "Y")
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE (A.WAYBILLKEY <> '0' OR A.WAYBILLKEY <> '')" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='Y'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE (A.WAYBILLKEY <> '0' OR A.WAYBILLKEY <> '')" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='N'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "3")
            {
                if (Export.Trim() == "Y")
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.CFORMNO <> ''" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='Y'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.CFORMNO <> ''" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='N'" +
                          " ORDER BY CREATEDDATE DESC";
                }
            }
            else if (filterText == "4")
            {
                if (Export.Trim() == "Y")
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.CFORMNO = ''" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='Y'" +
                          " ORDER BY CREATEDDATE DESC";
                }
                else
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                          " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK," +
                          " ISNULL(A.TOTALPCS,0) AS TOTALPCS" +
                          " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                          " ON A.TRANSPORTERID = B.ID" +
                          " WHERE A.CFORMNO = ''" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.TPUID = '" + TPUID + "'" +
                          " AND A.EXPORT='N'" +
                          " ORDER BY CREATEDDATE DESC";
                }
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
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B" +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.FORMREQUIRED = ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "2")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B" +
                      " ON A.TRANSPORTERID = B.ID" +
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
            string Sql = "  IF  EXISTS(SELECT * FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKDESPATCHID='" + DespatchID + "') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindMaxAllocationDate(string Despatchdate)
        {
            string sql = " SELECT CONVERT(VARCHAR(8),MAX(INSTRUCTIONDATE),112) AS ALLOCATIONDATE,CONVERT(VARCHAR(10),CAST(MAX(INSTRUCTIONDATE) AS DATE),103) AS SHOWALLOCATIONDATE FROM T_TPU_INSTRUCTION_HEADER AS A" +
                         " INNER JOIN T_TPU_INSTRUCTION_DETAILS AS B" +
                         " ON A.INSTRUCTIONID = B.INSTRUCTIONID" +
                         " WHERE B.POID IN(" + Despatchdate + ")";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteStockDespatch(string DespatchID)
        {
            int delflag = 0;

            string sqldeleteStockDespatch = "EXEC [SP_STOCK_DESPATCH_DELETE] '" + DespatchID + "'";

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
            string sqlformflag = string.Empty;
            string FLAG = string.Empty;
            sqlformflag = "SELECT FORMFLAG FROM T_STOCKDESPATCH_HEADER WHERE STOCKDESPATCHID ='" + DespatchID + "'";
            FLAG = (string)db.GetSingleValue(sqlformflag);

            string sql = " UPDATE T_STOCKDESPATCH_HEADER SET CFORMNO = '" + CFormNo + "',CFORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
                         " WHERE STOCKDESPATCHID ='" + DespatchID + "'";

            int e = db.HandleData(sql);

            if (e == 0)
            {
                updateflag = 0;  // delete unsuccessfull
            }
            else
            {
                sql = " UPDATE T_STOCKRECEIVED_HEADER SET CFORMNO = '" + CFormNo + "',CFORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103),FORMREQUIRED='" + FLAG + "'" +
                      " WHERE STOCKDESPATCHID ='" + DespatchID + "'";

                e = db.HandleData(sql);
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

        public DataTable PackingListDetails(string DespatchID, string TPUID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_DESPATCH_PACKINGLIST_DETAILS '" + DespatchID + "','" + TPUID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public int InsertPackingList(string DespatchID, string TPUID, string xml)
        {
            int e = 0;
            string sql = " EXEC USP_DESPATCH_PACKINGLIST_INSERT '" + DespatchID + "','" + TPUID + "','" + xml + "'";
            e = db.HandleData(sql);
            return e;
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
                         " FROM M_WAYBILL AS A INNER JOIN" +
                         "      M_BRANCH AS B ON A.STATEID = B.STATEID" +
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
                         " FROM M_WAYBILL AS A INNER JOIN" +
                         "      M_BRANCH AS B ON A.STATEID = B.STATEID" +
                         " AND B.BRID='" + DepotID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)" +
                         " UNION ALL" +
                         " SELECT A.WAYBILLNO  FROM M_WAYBILL AS A INNER JOIN" +
                         " M_BRANCH AS B ON A.STATEID = B.STATEID" +
                         " AND B.BRID='" + DepotID + "'" +
                         " AND A.WAYBILLNO IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
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

        public DataTable BindAllocation(string CountryID, string TPUID)
        {
            string sql = "EXEC USP_EXPORT_ALLOCATION_DETAILS '" + CountryID + "','" + TPUID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderProduct(string AllocationID, string VendorID)
        {
            string sql = " SELECT DISTINCT A.PRODUCTID AS ID,A.PRODUCTNAME AS NAME FROM T_TPU_INSTRUCTION_DETAILS AS A " +
                         " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.PRODUCTID=B.PRODUCTID " +
                         " WHERE A.INSTRUCTIONID='" + AllocationID + "' AND B.VENDORID='" + VendorID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Add by Rajeev For GST
        public decimal GetHSNTax_MMPP(string TaxID, string ProductID)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','','" + ProductID + "','')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        public string HSNCode(string ProductID)
        {
            string HSNCODE = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ISNULL(B.HSN,'') AS HSNCODE FROM M_PRODUCT A INNER JOIN M_CATEGORY B " +
                  " ON A.CATID=B.CATID " +
                  " AND A.ID='" + ProductID + "'";
            HSNCODE = (string)db.GetSingleValue(sql);
            return HSNCODE;
        }

        #endregion

        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(TAXPERCENTAGE,0) " +
                  " FROM T_STOCKDESPATCH_ITEMWISE_TAX " +
                  " WHERE STOCKDESPATCHID  =   '" + InvoiceID + "'" +
                  " AND TAXID              =   '" + TaxID + "'" +
                  " AND PRODUCTID          =   '" + ProductID + "'" +
                  " AND BATCHNO            =   '" + BatchNo + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        public DataTable BindTPUTCSPercent(string VendorID)
        {

            string sql = " EXEC USP_GET_VENDOR_TCS_PERCENT '" + VendorID + "'";
            DataTable ds = new DataTable();
            ds = db.GetData(sql);
            return ds;
        }
    }
}
