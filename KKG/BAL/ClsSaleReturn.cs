using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using System.Data;
using Utility;
using System.Globalization;

namespace BAL
{
    public class ClsSaleReturn
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

        public string BindBUSINESSSEGMENT(string bsid)
        {
            string sql = string.Empty;
            string bsname = string.Empty;
            try
            {

                sql = "SELECT BSNAME FROM M_BUSINESSSEGMENT where BSID='" + bsid + "'";
                bsname = (string)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return bsname;
        }

        public DataTable BindGroup(string BSID, string UserID, string CheckerFlag)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                //sql = " SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY" +
                //      " WHERE BUSINESSSEGMENTID = '" + BSID + "' ORDER BY DIS_CATNAME";
                if (CheckerFlag == "F")
                {
                    sql = " SELECT DISTINCT C.GROUPID AS DIS_CATID,C.GROUPNAME AS DIS_CATNAME FROM M_TPU_USER_MAPPING AS A" +
                          " INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                          " ON A.TPUID = B.DEPOTID" +
                          " INNER JOIN M_CUSTOMER AS C" +
                          " ON B.CUSTOMERID = C.CUSTOMERID" +
                          " WHERE A.USERID = '" + UserID + "'" +
                          " AND C.BUSINESSSEGMENTID='" + BSID + "'";
                }
                else
                {
                    sql = " SELECT DISTINCT C.GROUPID AS DIS_CATID,C.GROUPNAME AS DIS_CATNAME FROM M_TPU_USER_MAPPING AS A" +
                          " INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                          " ON A.TPUID = B.DEPOTID" +
                          " INNER JOIN M_CUSTOMER AS C" +
                          " ON B.CUSTOMERID = C.CUSTOMERID" +
                          " WHERE C.BUSINESSSEGMENTID='" + BSID + "'";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }
        
        public DataTable BindCustomer()
        {

            string sql = " SELECT CUSTOMERID,CUSTOMERNAME " +
                         " FROM M_CUSTOMER " +
                         " WHERE CUSTYPE_ID " +
                         " IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST'))" +
                         " ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer(string BSID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID LIKE '%" + BSID + "%' AND CUSTYPE_ID IN(SELECT SUPERDISTRIBUTORID FROM P_APPMASTER UNION SELECT DISTRIBUTORID FROM P_APPMASTER UNION SELECT STOCKISTSID FROM P_APPMASTER UNION SELECT SUPERSTOCKISTSID FROM P_APPMASTER) order by CUSTOMERNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindCustomer(string bsID, string groupid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT CUSTOMERID,CUSTOMERNAME " +
                      " FROM M_CUSTOMER " +
                      " WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%' AND" +
                      " GROUPID LIKE '%" + groupid + "%' AND" +
                      " CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST'))" +
                      " ORDER BY CUSTOMERNAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }


        public DataTable BindDepotCustomer(string bsID, string groupid, string DEPOTID, string SALERETURNID)
        {
            string sql = string.Empty;
            if (SALERETURNID == "")
            {
                 sql = " SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                             "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                             "   AND GROUPID LIKE '%" + groupid + "%' " +
                    //"   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST')) " +
                              "   AND B.DEPOTID='" + DEPOTID + "' AND A.ISACTIVE='True' ";
            }
            else
            {
                 sql = " SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                            "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                            "   AND GROUPID LIKE '%" + groupid + "%' " +
                    //"   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "' ";
            }
                      
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCustomer(string bsID, string groupid, string customerid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT CUSTOMERID,CUSTOMERNAME " +
                      " FROM M_CUSTOMER " +
                      " WHERE BUSINESSSEGMENTID LIKE   '%" + bsID + "%' AND" +
                      " GROUPID = '" + groupid + "' AND PARENT_CUST_ID='" + customerid + "' AND " +
                      " CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('RT'))" +
                      " ORDER BY CUSTOMERNAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindPackSize(string Po_ID)
        {

            //string sql = " SELECT PSID,PSNAME,RANK FROM  VW_PCS" +
            //             " WHERE PRODUCTID = '" + Po_ID + "'" +
            //             " UNION ALL" +
            //             " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
            //             " WHERE PSID NOT IN (SELECT PSID FROM  VW_PCS)" +
            //             " AND PRODUCTID = '" + Po_ID + "'" +
            //             " ORDER BY RANK";
            string sql = " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                         " WHERE PSID NOT IN (SELECT PSID FROM  VW_PCS)" +
                         " AND PRODUCTID = '" + Po_ID + "'" +
                         " ORDER BY RANK";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public void ResetDataTables()
        {
            dtSaleInvoiceRecord.Clear();
        }


        public DataTable BindInvoice(string fromdate, string todate,string CustID,string FinYear)
        {

            string sql = " SELECT SALEINVOICEID AS SALEINVOICEID, (SALEINVOICEPREFIX + '/' + SALEINVOICENO) AS SALEINVOICENO"+
                         " FROM T_SALEINVOICE_HEADER"+
                         " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')"+
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

        public string GetFinancestatus(string SALERETURNID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALERETURN_HEADER] WHERE SALERETURNID ='" + SALERETURNID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                         "  SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindSaleReturn(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID)
        {

            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                sql =    " SELECT A.SALERETURNID AS SALERETURNID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALERETURNNO," +
                         " CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALERETURNDATE, " +
                         " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID," +
                         " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                         " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC , " +
                         " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT, " +
                         " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                         " FROM T_SALERETURN_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK) " +
                         " ON A.SALERETURNID = B.SALERETURNID "+
                         " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                         " AND A.FINYEAR ='" + finyear + "' " +
                         " AND A.BSID = '" + BSID + "'  AND A.DEPOTID='" + depotid + "' " +
                         " AND A.CREATEDBY = '" + UserID + "' " +
                         " AND A.GROSSRETURN = 'N'"+
                         " ORDER BY SALERETURNNO DESC";
            }
            else if (CheckerFlag == "TRUE")
            {
                sql =    " SELECT A.SALERETURNID AS SALERETURNID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALERETURNNO," +
                         " CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALERETURNDATE, " +
                         " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID," +
                         " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                         " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC , " +
                         " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT, " +
                         " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                         " FROM T_SALERETURN_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK) " +
                         " ON A.SALERETURNID = B.SALERETURNID " +
                         " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                         " AND A.FINYEAR ='" + finyear + "' " +
                         " AND A.BSID = '" + BSID + "'  AND A.DEPOTID='" + depotid + "' " +
                         " AND A.ISVERIFIED <> 'Y'" +
                         " AND A.NEXTLEVELID = '" + UserID + "'" +
                         " AND A.GROSSRETURN = 'N'" +
                         " ORDER BY SALERETURNNO DESC";
            }

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

        public decimal GetHSNTax(string TaxID, string ProductID,string Date)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','','" + ProductID + "','','" + Date + "')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        public DataSet BindInvoiceProductDetails(string InvoiceID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                //sql = " EXEC USP_SALEINVOICEQTY_BASEDONPRODUCT_ERP '" + InvoiceID + "'";
                sql = " EXEC USP_INVOICE_DETAILS '" + InvoiceID + "'";
                ds = db.GetDataInDataSet(sql);
                ds.Tables[0].TableName = "BillDetails";
                ds.Tables[1].TableName = "FreeDetails";
                ds.Tables[2].TableName = "TaxCount";
                ds.Tables[3].TableName = "TaxDetails";
                ds.Tables[4].TableName = "Footer";
                ds.Tables[5].TableName = "Header";
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return ds;
        }

        public DataTable BindDistributorInvoice(string DistID, string FromDate, string ToDate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {

                sql = " SELECT A.SALEINVOICEID AS INVOICEID,SALEINVOICEPREFIX + '/' + SALEINVOICENO AS INVOICENO,"+
                      " CONVERT(VARCHAR(10),CAST(SALEINVOICEDATE AS DATE),103) AS INVOICEDATE,B.TOTALSALEINVOICEVALUE AS NETAMOUNT FROM T_SALEINVOICE_HEADER A " +
                      " INNER JOIN T_SALEINVOICE_FOOTER B ON A.SALEINVOICEID=B.SALEINVOICEID "+
                      " WHERE DISTRIBUTORID='" + DistID + "'"+
                      " AND CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN dbo.Convert_To_ISO('" + FromDate + "') AND  dbo.Convert_To_ISO('" + ToDate + "') "+
                      " ORDER BY SALEINVOICEDATE,INVOICENO DESC";
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
        public DataTable InvoiceDetails(string InvoiceID, string ProductID, string BatchNo)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_SALEINVOICE_DETAILS_BASEDONBATCHNO '" + InvoiceID + "','" + ProductID + "','" + BatchNo + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet EditInvoiceDetails(string SALERETURNID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_SALE_RETURN_DETAILS '" + SALERETURNID + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "HEADER";
            ds.Tables[1].TableName = "BILLDETAILS";
            ds.Tables[2].TableName = "FREEDETAILS";
            ds.Tables[3].TableName = "FOOTERDETAILS";
            ds.Tables[4].TableName = "TAXCOUNT";
            ds.Tables[5].TableName = "TAXDETAILS";
            return ds;
        }

        public string Getstatus(string SALEINVOICEID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALERETURN_HEADER] WHERE SALERETURNID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCTAG='Y') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

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
            //string Date = string.Empty;
            string StateID = string.Empty;
            string DistrictID = string.Empty;
            //string GroupID = string.Empty;
            //string BSID = string.Empty;
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
                //GroupID = Convert.ToString(dtCustomer.Rows[0]["GROUPID"]).Trim();
                //BSID = Convert.ToString(dtCustomer.Rows[0]["BUSINESSSEGMENTID"]).Trim();
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
        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string CustomerID,string Date)
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
        public DataTable BindTax(string menuID, string flag, string VendorID, string ProductID, string CustomerID,string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            string StateID = string.Empty;
            sqlState = "SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            StateID = (string)db.GetSingleValue(sqlState);


            if (flag == "1") //  Outer State
            {
                sql = " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + StateID + "','"+ Date +"') AS PERCENTAGE,0.00 AS TAXVALUE" +
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
        public string InsertReturnInvoiceDetails(   string ReturnDate, string Customerid, string Customername,
                                                    string DepotID, string DepotName, string userID, string finyear,
                                                    string Remarks, string bsID, string groupID, decimal TotalPcs,
                                                    decimal TotalInvoiceValue, decimal Othercharges, decimal Adjustment,
                                                    decimal RoundOff, decimal SpecialDisc, decimal NetAmount, decimal GrossAmount,
                                                    string xmlReturnInvoiceDetails, string xmlTax, string xmlFreeProductDetails,
                                                    string hdnvalue, string InvoiceType
                                                 )
        {

            string flag = "";
            string InvoiceID = string.Empty;
            string InvoiceNo = string.Empty;
            if (hdnvalue == "")
            {
                dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "A";
                try
                {
                    string sqlprocInvoice =  " EXEC [USP_SALE_RETURNINVOICE_INSERT_UPDATE] '','" + flag + "','" + ReturnDate + "',"+
                                             " '" + Customerid + "','" + Customername + "'," +
                                             " '" + DepotID + "','" + DepotName + "','" + userID + "',"+
                                             " '" + finyear + "','" + Remarks + "','" + bsID + "',"+
                                             " '" + groupID + "'," + TotalPcs + "," +
                                             " " + TotalInvoiceValue + "," + Othercharges + "," + Adjustment + ","+
                                             " " + RoundOff + "," + SpecialDisc + "," + NetAmount + "," + GrossAmount + "," +
                                             " '" + xmlReturnInvoiceDetails + "','" + xmlTax + "','" + xmlFreeProductDetails + "','" + InvoiceType + "'";
                    DataTable dtDespatch = db.GetData(sqlprocInvoice);

                    if (dtDespatch.Rows.Count > 0)
                    {
                        InvoiceID = dtDespatch.Rows[0]["RETURNINVOICEID"].ToString();
                        InvoiceNo = dtDespatch.Rows[0]["RETURNINVOICENO"].ToString();
                    }
                    else
                    {
                        InvoiceNo = "";
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

            return InvoiceNo;


        }

        #endregion

        /*public int InvoiceDelete(string InvoiceID)
        {
            int delflag = 0;

            string sqldeleteInvoice = "EXEC [USP_SALE_RETURN_DELETE] '" + InvoiceID + "'";

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
        }*/

        public string InvoiceDelete(string InvoiceID)
        {
            string Status = string.Empty;

            string sqldeleteStockDespatch = "EXEC [USP_SALE_RETURN_DELETE] '" + InvoiceID + "'";

            DataTable dtDeleteReceived = db.GetData(sqldeleteStockDespatch);

            if (dtDeleteReceived.Rows.Count > 0)
            {
                if (Convert.ToString(dtDeleteReceived.Rows[0]["STATUS"]).Trim() == "1")
                {
                    Status = "1";
                }
                else if (Convert.ToString(dtDeleteReceived.Rows[0]["STATUS"]).Trim() == "99")
                {
                    Status = Convert.ToString(dtDeleteReceived.Rows[0]["STATUS"]).Trim() + "|" + Convert.ToString(dtDeleteReceived.Rows[0]["PRODUCT"]).Trim() + "|" + Convert.ToString(dtDeleteReceived.Rows[0]["BATCHNO"]).Trim();
                }
            }
            else
            {
                Status = "0";
            }

            return Status;
        }

        public DataTable BindSaleReturnApproval(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID, string IsGrossReturn)
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT A.SALERETURNID AS SALEINVOICEID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALEINVOICENO,CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALEINVOICEDATE, " +
                        " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID,ADJUSTMENT, " +
                        " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, 'Return' AS BILLTYPE," +
                        " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC , " +
                        " ISNULL(B.NETAMOUNT,0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE, 0 AS TAXVALUE, ROUNDOFFVALUE,ISNULL(GETPASSNO,'') AS GETPASSNO,0 AS GROSSREBATEVALUE,0 AS TOTALTDS " +
                        " FROM T_SALERETURN_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK) " +
                        " ON A.SALERETURNID = B.SALERETURNID  " +
                        " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                        " AND FINYEAR ='" + finyear + "'" +
                        " AND BSID = '" + BSID + "'" +
                        " AND DEPOTID = '" + depotid + "'" +
                        " AND ISVERIFIED <> 'Y'" +
                        " AND GROSSRETURN ='" + IsGrossReturn + "'" +
                        " ORDER BY CONVERT(DATE,SALERETURNDATE,103) ASC";
            }
            else
            {
                sql = " SELECT A.SALERETURNID AS SALEINVOICEID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALEINVOICENO,CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALEINVOICEDATE, " +
                        " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID,ADJUSTMENT, " +
                        " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, 'Return' AS BILLTYPE," +
                        " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC , " +
                        " ISNULL(B.NETAMOUNT,0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE, 0 AS TAXVALUE, ROUNDOFFVALUE,ISNULL(GETPASSNO,'') AS GETPASSNO,0 AS GROSSREBATEVALUE,0 AS TOTALTDS " +
                        " FROM T_SALERETURN_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK) " +
                        " ON A.SALERETURNID = B.SALERETURNID  " +
                        " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                        " AND FINYEAR ='" + finyear + "'" +
                        " AND BSID = '" + BSID + "'" +
                        " AND DEPOTID = '" + depotid + "'" +
                        " AND ISVERIFIED <> 'Y'" +
                        " AND GROSSRETURN ='" + IsGrossReturn + "'" +
                        " ORDER BY CONVERT(DATE,SALERETURNDATE,103) ASC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql =    " SELECT ISNULL(TAXPERCENTAGE,0) " +
                     " FROM T_SALERETURN_ITEMWISE_TAX " +
                     " WHERE SALERETURNID     =   '" + InvoiceID + "'" +
                     " AND TAXID              =   '" + TaxID + "'" +
                     " AND PRODUCTID          =   '" + ProductID + "'" +
                     " AND BATCHNO            =   '" + BatchNo + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }
    }
}
