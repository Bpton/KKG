#region Developer Info

/*
 Developer Name : Avishek Ghosh
 * Start Date   : 05/02/2016
 * End Date     :
*/

#endregion


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
    public class clsSaleInvoice
    {
        DBUtils db = new DBUtils();
        DataTable dtSaleInvoiceRecord = new DataTable();

        public DataTable BindDepot_Transporter(string DepotID,string SALEINVOICEID)
        {
            string sql = string.Empty;
            if (SALEINVOICEID == "")
            {
                sql = " SELECT A.ID,A.NAME " +
                             " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                                  " M_TRANSPOTER_DEPOT_MAP AS B " +
                             " ON A.ID=B.TRANSPOTERID" +
                             " WHERE B.DEPOTID='" + DepotID + "'" +
                             " AND B.TAG='D' AND A.ISAPPROVED='Y'" +
                             " ORDER BY A.NAME";
            }
            else
            {
                sql = " SELECT A.ID,A.NAME " +
                            " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                                 " M_TRANSPOTER_DEPOT_MAP AS B " +
                            " ON A.ID=B.TRANSPOTERID" +
                            " WHERE B.DEPOTID='" + DepotID + "'" +
                            " AND B.TAG='D' " +
                            " ORDER BY A.NAME";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWaybill(string CustomeID)
        {

            string sql = " SELECT A.WAYBILLNO " +
                         " FROM M_WAYBILL AS A INNER JOIN" +
                         "      M_CUSTOMER AS B ON A.STATEID = B.STATEID" +
                         " AND B.CUSTOMERID='" + CustomeID + "'" +
                         " AND A.WAYBILLNO" +
                         " NOT IN(SELECT WAYBILLNO FROM VW_WAYBILL)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindCustomer_OutStanding(string DistributorID, string DepotID, string Finyear,string InvoiceID)
        {

            string sql = " EXEC USP_DISTRIBUTOR_OUTSTANDING '" + DistributorID + "','" + DepotID + "','" + Finyear + "','" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindCustomer_Target(string DistributorID, string DepotID, string BSID,string Finyear, string MonthID,string InvoiceID)
        {

            string sql = " EXEC USP_DISTRIBUTOR_TARGET '" + DistributorID + "','" + DepotID + "','" + BSID + "','" + Finyear + "','" + MonthID + "','" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindInvoice_Limit(string DistributorID, string DepotID, string Finyear, string MonthID, string InvoiceID)
        {

            string sql = " EXEC USP_INVOICE_LIMIT_BALANCE '" + DistributorID + "','" + DepotID + "','" + Finyear + "','" + MonthID + "','" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
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

        public DataTable BindICDSDetails(string SaleOrderID)
        {

            string sql =    " SELECT ISNULL(ICDSNO,'') AS ICDSNO, "+
	                        " CASE  WHEN ICDSDATE IS NOT NULL THEN CONVERT(VARCHAR(10),CAST(ICDSDATE AS DATE),103)  "+
                            "       ELSE '01/01/1900' END AS ICDSDATE "+
                            " FROM T_SALEORDER_HEADER "+
                            " WHERE SALEORDERID='" + SaleOrderID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomer(string bsID, string groupid, string DEPOTID, string SALEINVOICEID)
        {
            string sql = string.Empty;
            if (SALEINVOICEID == "")
            {
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + DEPOTID + "') " +
                      "  BEGIN " +
                             "  SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                             "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                             "   AND GROUPID LIKE '%" + groupid + "%' " +
                             "   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST','ECOMM')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "' "+
                             "   AND A.ISACTIVE='True'" +
                             "   ORDER BY CUSTOMERNAME " +
                        " END ";
            }
            else
            {
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + DEPOTID + "') " +
                      "  BEGIN " +
                             "  SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                             "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                             "   AND GROUPID LIKE '%" + groupid + "%' " +
                             "   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST','ECOMM')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "'" +
                             "   ORDER BY CUSTOMERNAME " +
                      " END ";
                            
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomer_GT(string bsID, string groupid, string DEPOTID, string SALEINVOICEID)
        {
            string sql = string.Empty;
            if (SALEINVOICEID == "")
            {
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + DEPOTID + "') " +
                      "  BEGIN " +
                             "  SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                             "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                             "   AND GROUPID LIKE '%" + groupid + "%' " +
                             "   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST','ECOMM')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "' " +
                             "   AND A.ISACTIVE='True'" +
                             "   AND A.BILLING_TYPE IN ('0','1')" +
                             "   ORDER BY CUSTOMERNAME " +
                        " END ";
            }
            else
            {
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + DEPOTID + "') " +
                      "  BEGIN " +
                             "  SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                             "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                             "   AND GROUPID LIKE '%" + groupid + "%' " +
                             "   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST','ECOMM')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "'" +
                             "   AND A.BILLING_TYPE IN ('0','1')" +
                             "   ORDER BY CUSTOMERNAME " +
                      " END ";

            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
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
                      " CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE IN('DI','SUDI','ST','SST','ECOMM'))" +
                      " ORDER BY CUSTOMERNAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceCustomer(string bsID, string DEPOTID)
        {

            string sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (" + DEPOTID + ")) " +
                         "  BEGIN " +
                         "      SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A " +
                         "      INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                         "      AND B.DEPOTID IN (" + DEPOTID + ")" +
                         "      AND A.ISACTIVE='True'" +
                         "      ORDER BY A.CUSTOMERNAME" +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         "      SELECT CUSTOMERID,CUSTOMERNAME  FROM M_CUSTOMER   " +
                         "      WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                         "      AND ISACTIVE='True'" +
                         "      ORDER BY CUSTOMERNAME" +
                         " END";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindExportInvoiceCustomer(string SaleOrderID)
        {


            string sql = " SELECT CUSTOMERID,CUSTOMERNAME,CONVERT(VARCHAR(10),CAST(SALEORDERDATE AS DATE),103) AS SALEORDERDATE "+  
                         " FROM T_SALEORDER_HEADER " +
                         " WHERE SALEORDERID='" + SaleOrderID + "'" +
                         " ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSaleOrder(string CustomerID)
        {
            string sql = string.Empty;
            sql = " SELECT SALEORDERID," +
                     " (SALEORDERNO + '( Ref. P.O. - ' +ISNULL(REFERENCESALEORDERNO,'') + ')') AS SALEORDERNO " +
                     " FROM T_SALEORDER_HEADER " +
                     " WHERE CUSTOMERID='" + CustomerID + "'" +
                     " AND ISCANCELLED = 'N' AND ISCLOSED='N'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSaleOrderCSD(string CustomerID)
        {
            string sql = string.Empty;
            sql =    " SELECT SALEORDERID,ISNULL(REFERENCESALEORDERNO,'') AS SALEORDERNO FROM T_SALEORDER_HEADER" +
                     " WHERE CUSTOMERID='" + CustomerID + "'" +
                     " AND ISCANCELLED = 'N' " +
                     " AND ISCLOSED='N'" +
                     " ORDER BY CREATEDDATE DESC";
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

        public DataTable BindExportSaleOrder(string CountryID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_EXPORT_SALEORDER_DETAILS '" + CountryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProformaInvoice(string SaleorderID)
        {
            string sql = string.Empty;
            sql =   " SELECT DISTINCT A.PROFORMAINVOICEID,(A.PROFORMAINVOICEPREFIX+'/'+A.PROFORMAINVOICENO+'/'+A.PROFORMAINVOICESUFFIX) AS PROFORMAINVOICENO," +
                    " CONVERT(VARCHAR(10),CAST(A.PROFORMAINVOICEDATE AS DATE),103) AS PROFORMAINVOICEDATE" +
                    " FROM T_PROFORMAINVOICE_HEADER A INNER JOIN T_PROFORMAINVOICE_DETAILS B" +
                    " ON A.PROFORMAINVOICEID = B.PROFORMAINVOICEID" +
                    " WHERE B.PURCHASEORDERID='" + SaleorderID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchDetailsFreeProduct(string DepotID, string ProductID, string PacksizeID, string BatchNo, string CustomerID, string Date, string ModuleID, string BSID, string GroupID)
        {
            string sql = "EXEC SP_FREE_PRODUCT_BCP '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','" + CustomerID + "','" + Date + "','" + ModuleID + "','" + BSID + "','" + GroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable AvailableStock(string DepotID, string ProductID, string BatchNo,decimal MRP,string MFG,string EXPR)
        {
            string sql = "EXEC USP_FINAL_STOCK_CHECKING '" + DepotID + "','" + ProductID + "','" + BatchNo + "',"+MRP+",'"+ MFG + "','"+ EXPR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PackSize(string ProductID, decimal Qty)
        {
            string sql = "EXEC USP_GET_CASEPACK '" + ProductID + "'," + Qty + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindExportBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo,decimal MRP,string StoreLocation)
        {
            string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'," + MRP + ",'" + StoreLocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindInvoiceBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo, string CustomerID, string Date, string ModuleID, string BSID, string GroupID)
        {
            string sql = "EXEC USP_BATCHWISE_DEPOT_STOCK_INVOICE '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','" + CustomerID + "','" + Date + "','" + ModuleID + "','" + BSID + "','" + GroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable Bindinvoicetype()
        {
            string sql = "SELECT INVOICETYPEID,INVOICETYPENAME FROM M_INVOICETYPE ORDER BY INVOICETYPEID ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLoadingPort()
        {
            string sql = " SELECT DISTINCT PORTID,PORTNAME FROM M_PORT WHERE PORTTYPE='S'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDischargePort()
        {
            string sql = " SELECT DISTINCT PORTID,PORTNAME FROM M_PORT WHERE PORTTYPE='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindInvoiceProduct(string CustomerID, string BSID,string SALEINVOICEID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            ClsParam Param = new ClsParam();
            try
            {
                if (SALEINVOICEID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE+ "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' AND A.ACTIVE='T'" +
                          " AND A.DIVID <>'"+ Param.DEEVA +"'" +
                          " AND A.MRP > 0 "+
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') AND ACTIVE='T'" +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG'" +
                          " AND A.DIVID <>'" + Param.DEEVA + "'" +
                          " AND A.MRP > 0 " +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') " +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceProduct_Export(string CustomerID, string BSID, string SALEINVOICEID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            ClsParam Param = new ClsParam();
            try
            {
                if (SALEINVOICEID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' AND A.ACTIVE='T'" +
                          " AND A.DIVID <>'" + Param.DEEVA + "'" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') AND ACTIVE='T'" +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG'" +
                          " AND A.DIVID <>'" + Param.DEEVA + "'" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') " +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceProduct_GT(string CustomerID, string BSID, string SALEINVOICEID,string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            ClsParam Param = new ClsParam();
            try
            {
                if (SALEINVOICEID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' "+
                          " AND A.ACTIVE='T'" +
                          " AND A.MRP > 0 " +
                          " AND A.DIVID IN (SELECT BRANDID FROM M_CUSTOMER_BRAND_MAPPING WHERE CUSTOMERID ='" + CustomerID + "')" +
                          " AND A.ID    IN (SELECT PRODUCTID FROM M_PRODUCT_DEPOT_MAPPING WHERE DEPOTID='"+ DepotID + "')" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') AND ACTIVE='T'" +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG'" +
                          " AND A.MRP > 0 " +
                          " AND A.DIVID IN (SELECT BRANDID FROM M_CUSTOMER_BRAND_MAPPING  WHERE CUSTOMERID ='" + CustomerID + "')" +
                          " AND A.ID    IN (SELECT PRODUCTID FROM M_PRODUCT_DEPOT_MAPPING WHERE DEPOTID ='" + DepotID + "')" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') " +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindGTChallanProduct(string CustomerID, string BSID, string SALEINVOICEID)
        {
            string sql = string.Empty;
            ClsParam Param = new ClsParam();
            DataTable dt = new DataTable();
            try
            {
                if (SALEINVOICEID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' "+
                          " AND A.ACTIVE='T'" +
                          " AND A.DIVID <>'" + Param.DEEVA + "'" +
                          " UNION ALL"+
                          " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.PCS + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' AND A.ACTIVE='T'" +
                          " AND A.DIVID ='" + Param.DEEVA + "'" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') AND ACTIVE='T'" +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG'" +
                          " AND A.DIVID <>'" + Param.DEEVA + "'" +
                          " UNION ALL" +
                          " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" +Param.PCS+ "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.TYPE = 'FG' AND A.ACTIVE='T'" +
                          " AND A.DIVID ='"+Param.DEEVA+"'" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME, 999 AS UNITVALUE " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG') " +
                          " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceCategoryProduct(string CustomerID, string BSID, string CategoryID,string SALEINVOICEID)
        {
            ClsParam Param = new ClsParam();
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if(SALEINVOICEID=="")
                {
                sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '"+Param.CASE+"'" +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "'"+
                      " AND A.CATID='" + CategoryID + "'" +
                      " AND A.TYPE = 'FG' " +
                      " AND A.ACTIVE='T' " +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, DIVNAME,999 AS UNITVALUE,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE NOT IN ('FG','PM','RM','SFG')"+
                      " AND CATID='" + CategoryID + "' "+
                      " AND ACTIVE='T'" +
                      " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                     sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '"+Param.CASE+"'" +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "'"+
                      " AND A.CATID='" + CategoryID + "'" +
                      " AND A.TYPE = 'FG'" +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, DIVNAME,999 AS UNITVALUE,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE NOT IN ('FG','PM','RM','SFG')"+
                      " AND CATID='" + CategoryID + "'" +
                      " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceCategoryProduct_GT(string CustomerID, string BSID, string CategoryID, string SALEINVOICEID, string DepotID)
        {
            ClsParam Param = new ClsParam();
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (SALEINVOICEID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                          " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                          " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                          " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                          " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                          " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                          " ON A.ID = D.PRODUCTID " +
                          " AND D.PSID = '" + Param.CASE + "'" +
                          " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                          " AND A.CATID='" + CategoryID + "'" +
                          " AND A.ID    IN (SELECT PRODUCTID FROM M_PRODUCT_DEPOT_MAPPING WHERE DEPOTID ='" + DepotID + "')" +
                          " AND A.TYPE = 'FG' " +
                          " AND A.ACTIVE='T' " +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, DIVNAME,999 AS UNITVALUE,CATID " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG')" +
                          " AND CATID='" + CategoryID + "' " +
                          " AND ACTIVE='T'" +
                          " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                     " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                     " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                     " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                     " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                     " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                     " ON A.ID = D.PRODUCTID " +
                     " AND D.PSID = '" + Param.CASE + "'" +
                     " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                     " AND A.CATID='" + CategoryID + "'" +
                     " AND A.ID    IN (SELECT PRODUCTID FROM M_PRODUCT_DEPOT_MAPPING WHERE DEPOTID ='" + DepotID + "')" +
                     " AND A.TYPE = 'FG'" +
                     " UNION ALL " +
                     " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                     " '999' AS SEQUENCENO, DIVNAME,999 AS UNITVALUE,CATID " +
                     " FROM M_PRODUCT " +
                     " WHERE TYPE NOT IN ('FG','PM','RM','SFG')" +
                     " AND CATID='" + CategoryID + "'" +
                     " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }




        public DataTable BindOrderProduct(string SaleOrderID, string CustomerID)
        {
            ClsParam Param = new ClsParam();
            string sql = string.Empty;
            string sqlGroup = string.Empty;
            string GroupID = string.Empty;

            sqlGroup = " SELECT GROUPID FROM T_SALEORDER_HEADER WHERE SALEORDERID='"+ SaleOrderID + "'";
            GroupID=((string)db.GetSingleValue(sqlGroup));

            sql = " SELECT  DISTINCT C.ID AS PRODUCTID,C.PRODUCTALIAS + '~ [' + E.PSNAME + ']' AS PRODUCTNAME," +
                  " ISNULL(B.REFERENCESALEORDERDATE,'') AS SALEORDERDATE," +
                  " B.GROUPID,D.SEQUENCENO, C.CATNAME,C.DIVNAME,CAST(C.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B" +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C" +
                  " ON A.PRODUCTID = C.ID INNER JOIN M_CATEGORY AS D" +
                  " ON D.CATID = C.CATID INNER JOIN VW_SALEUNIT AS E" +
                  " ON C.ID = E.PRODUCTID INNER JOIN M_CUSTOMER_PRODUCT_RATESHEET AS PR " +
                  " ON A.PRODUCTID = PR.PRODUCTID"+
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND E.PSID = '"+Param.CASE+"'" +
                  " AND C.TYPE ='FG'" +
                  " AND PR.GROUPID = '"+GroupID+"'" +
                  " AND PR.RATE > 0" +
                  " UNION ALL" +
                  " SELECT DISTINCT C.ID  AS PRODUCTID ,ISNULL(C.PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                  " CONVERT(VARCHAR(10),CAST(B.SALEORDERDATE AS DATE),103) AS SALEORDERDATE," +
                  " B.GROUPID,'999' AS SEQUENCENO, C.CATNAME,C.DIVNAME,999 AS UNITVALUE" +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B " +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C " +
                  " ON A.PRODUCTID = C.ID " +
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND C.TYPE NOT IN ('FG','PM','RM','SFG') " +
                  " ORDER BY D.SEQUENCENO,C.DIVNAME,UNITVALUE,PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMTOrderProduct(string SaleOrderID, string CustomerID)
        {
            ClsParam Param = new ClsParam();
            string sql = string.Empty;

            sql = " SELECT  DISTINCT C.ID AS PRODUCTID,C.PRODUCTALIAS + '~ [' + E.PSNAME + ']' AS PRODUCTNAME," +
                  " ISNULL(B.REFERENCESALEORDERDATE,'') AS SALEORDERDATE," +
                  " B.GROUPID,D.SEQUENCENO, C.CATNAME,C.DIVNAME,CAST(C.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B" +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C" +
                  " ON A.PRODUCTID = C.ID INNER JOIN M_CATEGORY AS D" +
                  " ON D.CATID = C.CATID INNER JOIN VW_SALEUNIT AS E" +
                  " ON C.ID = E.PRODUCTID " +
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND E.PSID = '" + Param.CASE + "'" +
                  " AND C.TYPE ='FG'" +
                  " UNION ALL" +
                  " SELECT DISTINCT C.ID  AS PRODUCTID ,ISNULL(C.PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                  " CONVERT(VARCHAR(10),CAST(B.SALEORDERDATE AS DATE),103) AS SALEORDERDATE," +
                  " B.GROUPID,'999' AS SEQUENCENO, C.CATNAME,C.DIVNAME,999 AS UNITVALUE" +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B " +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C " +
                  " ON A.PRODUCTID = C.ID " +
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND C.TYPE NOT IN ('FG','PM','RM','SFG') " +
                  " ORDER BY D.SEQUENCENO,C.DIVNAME,UNITVALUE,PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindChallanProduct(string BSID,string CustomerID,string Mode)
        {
            /*ClsParam Param = new ClsParam();*/
            string sql = string.Empty;
            sql = " EXEC USP_GET_CHALLAN_PRODUCT '"+ BSID + "','"+ CustomerID + "','"+ Mode + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        

        public DataTable BindCorporateOrderProduct(string SaleOrderID, string CustomerID)
        {
            string sql = string.Empty;
            string sqlGroup = string.Empty;
            string GroupID = string.Empty;

            sqlGroup = " SELECT GROUPID FROM T_SALEORDER_HEADER WHERE SALEORDERID='" + SaleOrderID + "'";
            GroupID = ((string)db.GetSingleValue(sqlGroup));

            sql = " SELECT  DISTINCT C.ID AS PRODUCTID,C.PRODUCTALIAS + '~ [' + E.PSNAME + ']' AS PRODUCTNAME," +
                  " ISNULL(B.REFERENCESALEORDERDATE,'') AS SALEORDERDATE," +
                  " B.GROUPID,D.SEQUENCENO, C.CATNAME,C.DIVNAME,CAST(C.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B" +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C" +
                  " ON A.PRODUCTID = C.ID INNER JOIN M_CATEGORY AS D" +
                  " ON D.CATID = C.CATID INNER JOIN VW_SALEUNIT AS E" +
                  " ON C.ID = E.PRODUCTID INNER JOIN M_CUSTOMER_PRODUCT_RATESHEET AS PR " +
                  " ON A.PRODUCTID = PR.PRODUCTID" +
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND E.UOMID IS NOT NULL" +
                  " AND C.TYPE ='FG'" +
                  " AND PR.GROUPID = '" + GroupID + "'" +
                  " AND PR.RATE > 0" +
                  " UNION ALL" +
                  " SELECT DISTINCT C.ID  AS PRODUCTID ,ISNULL(C.PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                  " CONVERT(VARCHAR(10),CAST(B.SALEORDERDATE AS DATE),103) AS SALEORDERDATE," +
                  " B.GROUPID,'999' AS SEQUENCENO, C.CATNAME,C.DIVNAME,999 AS UNITVALUE" +
                  " FROM T_SALEORDER_DETAILS AS A INNER JOIN T_SALEORDER_HEADER AS B " +
                  " ON A.SALEORDERID = B.SALEORDERID INNER JOIN M_PRODUCT AS C " +
                  " ON A.PRODUCTID = C.ID " +
                  " WHERE A.SALEORDERID = '" + SaleOrderID + "'" +
                  " AND C.TYPE NOT IN ('FG','PM','RM','SFG') " +
                  " ORDER BY D.SEQUENCENO,C.DIVNAME,UNITVALUE,PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProformaInvoiceProduct(string ProformaInvoiceID)
        {
            string sql = string.Empty;
            sql = " SELECT DISTINCT A.PRODUCTID,A.PRODUCTNAME,CONVERT(VARCHAR(10),CAST(B.PROFORMAINVOICEDATE AS DATE),103) AS PROFORMAINVOICEDATE,B.GROUPID" +
                  " FROM T_PROFORMAINVOICE_DETAILS AS A" +
                  " INNER JOIN T_PROFORMAINVOICE_HEADER AS B ON" +
                  " A.PROFORMAINVOICEID = B.PROFORMAINVOICEID" +
                  " WHERE A.PROFORMAINVOICEID = '" + ProformaInvoiceID + "'" +
                  " ORDER BY PRODUCTNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindLCDetails(string SaleOrderID, string ProformaInvoiceID, string CustomerID)
        {
            string sql = string.Empty;
            sql = "  IF EXISTS(SELECT '1' FROM [T_LC_HEADER] WHERE CUSTOMERID='" + CustomerID + "'AND PROFORMAINVOICEID='" + ProformaInvoiceID + "'AND SALEORDERID='" + SaleOrderID + "')" +
                    " BEGIN " +
                    " SELECT ISNULL(LCNO,'') AS LCNO,CONVERT(VARCHAR(10),CAST (LCDATE   AS DATE),103) AS LCDATE,ISNULL(LCBANK,'') AS LCBANK " +
                    " FROM  T_LC_HEADER " +
                    " WHERE CUSTOMERID='" + CustomerID + "' AND PROFORMAINVOICEID='" + ProformaInvoiceID + "'AND SALEORDERID='" + SaleOrderID + "'" +
                    " END " +
                    " ELSE " +
                    " BEGIN " +
                    "  SELECT '0' " +
                    " END ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string Getstatus(string SALEINVOICEID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCH_STATUS='Y') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string GetFinancestatus(string SALEINVOICEID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string GetInvoicestatus(string SALEINVOICEID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public decimal BindProformaInvoiceValue(string ProformaInvoiceID)
        {
            decimal Value = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(NETAMOUNT,0) AS NETAMOUNT FROM T_PROFORMAINVOICE_FOOTER" +
                  " WHERE PROFORMAINVOICEID='" + ProformaInvoiceID + "'";

            Value = (decimal)db.GetSingleValue(sql);
            return Value;
        }



        public decimal BindProformaAdjustmentValue(string ProformaInvoiceID)
        {
            decimal Value = 0;
            string sql = string.Empty;
            sql = " IF  EXISTS(SELECT '1' FROM [T_PROFORMAINVOICE_CREDITNOTE] WHERE PROFORMAINVOICEID='" + ProformaInvoiceID + "') " +
                  " BEGIN " +
                  "      SELECT ISNULL(ADJAMOUNT,0) AS ADJAMOUNT FROM T_PROFORMAINVOICE_FOOTER" +
                  "      WHERE PROFORMAINVOICEID='" + ProformaInvoiceID + "' " +
                  " END " +
                  " ELSE" +
                  " BEGIN" +
                  " 	SELECT CAST(0 AS DECIMAL(18,2))" +
                  " END";

            Value = (decimal)db.GetSingleValue(sql);
            return Value;
        }

        public DataTable BindProformaRecords(string ProformaID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_PROFORMA_INVOICE_RECORD '" + ProformaID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderQty(string SaleOrderID, string ProductID, string PacksizeID,string SaleInvoiceID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_ORDER_DESPATCH_QTY '" + SaleOrderID + "','" + ProductID + "','" + PacksizeID + "','"+SaleInvoiceID+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProformaInvQty(string ProformaInvID, string ProductID, string PacksizeID)
        {
            string sql = string.Empty;
            sql = " SELECT DBO.GetPackingSize_OnCall(A.PRODUCTID,'B9F29D12-DE94-40F1-A668-C79BF1BF4425',  '" + PacksizeID + "'," +
                  "        ISNULL(SUM(dbo.GetPackingSize_OnCall(A.PRODUCTID,A.PACKINGSIZEID,'B9F29D12-DE94-40F1-A668-C79BF1BF4425',A.QTY)),0)) AS PROFORMAINVQTY" +
                  " FROM [T_PROFORMAINVOICE_DETAILS] A " +
                  " WHERE A.PROFORMAINVOICEID='" + ProformaInvID + "' AND A.PRODUCTID = '" + ProductID + "'" +
                  " GROUP BY A.PRODUCTID,A.PACKINGSIZEID";
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

        public string GroupStatus(string CustomerID)
        {
            string sql = " IF EXISTS ( SELECT 1 FROM M_CUSTOMER A INNER JOIN M_DISTRIBUTER_CATEGORY B "+
			             " ON A.GROUPID = B.DIS_CATID "+
			             " AND A.BUSINESSSEGMENTID='0AA9353F-D350-4380-BC84-6ED5D0031E24' "+
			             " AND B.ISFINANCE_HO = 'N' "+
                         " AND A.CUSTOMERID = '" + CustomerID + "' " +
		                 " ) "+
                         " BEGIN "+
                         " 	SELECT '1' AS GROUP_STATUS "+
                         " END "+
                         " ELSE "+
                         " BEGIN "+
                         " 	SELECT '0' AS GROUP_STATUS "+
                         " END";
            string GroupStatus = (string)db.GetSingleValue(sql);
            return GroupStatus;
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

        public decimal GetHSNTax(string TaxID, string ProductID,string Date)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT DBO.fn_TaxEvalute('" + TaxID + "','','" + ProductID + "','','"+Date+"')";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
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

                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "','"+Date+"') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
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
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
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

        public DataSet NetWeightCaseQty(string PRODUCTID, string PACKSIZEPCS, string PACKSIZETO, decimal deliveredqty)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_NETWEIGHT_CASEQTY '" + PRODUCTID + "','" + PACKSIZEPCS + "','" + PACKSIZETO + "'," + deliveredqty + "";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet EditInvoiceDetails(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC sp_SALE_INVOICE_DETAILS '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet EditExportInvoiceDetails(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_EXPORT_SALE_INVOICE_DETAILS '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable PackingListDetails(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_EXPORT_PACKINGLIST_DETAILS '" + InvoiceID + "'";
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

        public decimal QtyInPcs(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        public DataTable QtyInPcsGT(string productID, string packsizeFromID, decimal DeliveredQty, decimal StockQty,string SaleOrderID,string SaleInvoiceID)
        {
            DataTable dtQtyInPCS = new DataTable();
            string sql = " EXEC USP_QTYINPCS '" + productID + "','" + packsizeFromID + "'," + DeliveredQty + ","+
                         " " + StockQty + ",'" + SaleOrderID + "','"+SaleInvoiceID+"'";
            dtQtyInPCS = db.GetData(sql);
            return dtQtyInPCS;
        }

        public decimal QtyInCase(string productID, string packsizeFromID, string packsizeToID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + packsizeToID + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
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

        public decimal CalculateAmountInPcsExport(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            decimal FinalAmount = 0;

            string PackSizeTo = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";

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

        public decimal CalculateINRAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal MRP, decimal Percentage, decimal Value,decimal ExchangeRate)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;
            decimal FinalAmount = 0;

            string PackSizeTo = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = MRP * ExchangeRate * RETURNVALUE;
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
            string PackSizeTo = packsizeFromID;

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
            string PackSizeTo = packsizeFromID;

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

        public decimal GetSSMargin(string CustomerID)
        {
            decimal RETURNVALUE = 0;
            string SQL = " SELECT ISNULL(ADDSSMARGINPERCENTAGE,0) AS ADDSSMARGINPERCENTAGE FROM M_CUSTOMER WHERE CUSTOMERID='" + CustomerID + "'";
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

        public string GetPrimaryPriceScheme_GT(string ProductID, decimal Qty, string Packsize, string CustomerID, string SaleOrderID, string Date, string BSID, string GroupID,string DepotID,decimal MRP)
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

            string SQLFunction = "SELECT ISNULL(DBO.fn_GetPriceScheme_GT('" + Date + "','" + StateID + "','" + DistrictID + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Qty + ",'" + Packsize + "','" + Zone + "','" + Territory + "' ,'"+ DepotID + "',"+ MRP + "),'')";
            RETURNVALUE = (string)db.GetSingleValue(SQLFunction);
            return RETURNVALUE;

        }

        public string GetCSDFretRebate(string ProductID, decimal Qty, string Packsize, string CustomerID, string SaleOrderID, string Date, string BSID, string GroupID)
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

            string SQLFunction = "SELECT ISNULL(DBO.fn_GetFretDiscount('" + Date + "','" + StateID + "','" + DistrictID + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Qty + ",'" + Packsize + "','" + Zone + "','" + Territory + "' ),'')";
            RETURNVALUE = (string)db.GetSingleValue(SQLFunction);
            return RETURNVALUE;

        }

        public string GetGrossRebate(string CustomerID, string Date, string BSID, string GroupID)
        {
            string RETURNVALUE = string.Empty;
            string StateID = string.Empty;
            string Zone = string.Empty;
            string Territory = string.Empty;
            DataTable dtCustomer = new DataTable();


            string sql = " SELECT A.BUSINESSSEGMENTID,A.GROUPID,A.STATEID,B.Zone,ISNULL(C.TERRITORYID,'') AS TERRITORYID" +
                         " FROM M_CUSTOMER AS A " +
                         " INNER JOIN M_REGION AS B ON A.STATEID = B.State_ID" +
                         " LEFT JOIN  M_TERRITORYDETAILS AS C ON A.STATEID = C.STATEID AND A.DISTRICTID = C.DISTRICTID" +
                         " WHERE A.CUSTOMERID='" + CustomerID + "'";
            dtCustomer = db.GetData(sql);
            if (dtCustomer.Rows.Count > 0)
            {
                StateID = Convert.ToString(dtCustomer.Rows[0]["STATEID"]).Trim();
                Zone = Convert.ToString(dtCustomer.Rows[0]["Zone"]).Trim();
                Territory = Convert.ToString(dtCustomer.Rows[0]["TERRITORYID"]).Trim();
            }

            string SQLFunction = "SELECT ISNULL(DBO.fn_GetGrossRebate('" + Date + "','" + StateID + "','" + GroupID + "','" + BSID + "','" + Zone + "','" + Territory + "' ),'')";
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

        public DataTable GetFretRebateParam(string Param)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT * FROM dbo.fnSplit('" + Param + "','~')";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetPrimaryQtyScheme(string ProductID, decimal Qty, string Packsize, string CustomerID, string SaleOrderID, string Date, string BSID, string GroupID, string DepotID, string ModuleID, string BatchNo, decimal MRP)
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
            string SQLFunction = "EXEC SP_GetQTYScheme '" + Date + "','" + StateID + "','" + DistrictID + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Qty + ",'" + Packsize + "','" + Zone + "','" + Territory + "','" + CustomerID + "','" + DepotID + "','" + ModuleID + "','" + BatchNo + "'," + MRP + "";
            dt = db.GetData(SQLFunction);
            return dt;
        }



        public DataTable BindSaleOrderDate(string saleorderID)
        {

            string sql = " SELECT CONVERT(VARCHAR(8),SALEORDERDATE,112) AS SALEORDERDATE FROM T_SALEORDER_HEADER WHERE SALEORDERID='" + saleorderID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Insert-Update MT/Institutional Invoice
        public string InsertInvoiceDetails( string InvoiceDate, string Customerid, string Customername, string wayBillNo,
                                            string TransporterID, string Vehicle, string DepotID, string DepotName, string BSID, string GroupID,
                                            string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate, string PaymentMode,
                                            string TransportMode, string userID, string finyear, string Remarks,
                                            decimal TotalValue, decimal Othercharges, decimal Adjustment, 
                                            decimal RoundOff, decimal Disc, string strTermsID,
                                            string xmlInvoiceDetails, string xmlTax, string xmlGrossTax, 
                                            string xmlQtyScheme, string xmlOfferPack,string xmlProductDetails,
                                            string hdnvalue, string ModuleID, string DeliveryAddressID,
                                            string DeliveryAddress, string ICDSNo, string ICDSDate,
                                            string RebateSchemeID, decimal RebatePercentage, decimal RebateValue, 
                                            decimal AddRebatePercentage, decimal AddRebateValue,
                                            string SaleOrderID, string CFormFlag, decimal TotalPCS, 
                                            decimal TotActualCase,string InvoiceType,string GrossWght,
                                            string IsChallan,decimal TaxableAmt,string MonthID)
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
                    string sqlprocInvoice = " EXEC [SP_SALE_INVOICE_INSERT_UPDATE] '','" + flag + "','" + InvoiceDate + "',"+
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "',"+
                                             " '" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + ","+
                                             " " + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "','" + xmlTax + "',"+
                                             " '" + xmlGrossTax + "','" + xmlQtyScheme + "','"+ xmlOfferPack +"'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "',"+
                                             " '" + DeliveryAddressID + "','" + DeliveryAddress + "'," +
                                             " '" + ICDSNo + "','" + ICDSDate + "','" + RebateSchemeID + "',"+
                                             " " + RebatePercentage + "," + RebateValue + "," +
                                             " " + AddRebatePercentage + "," + AddRebateValue + "," +
                                             " '" + SaleOrderID + "','" + CFormFlag + "'," + TotalPCS + ","+
                                             " " + TotActualCase + ",'" + InvoiceType + "',"+
                                             " '" + GrossWght + "','" + IsChallan + "'," + TaxableAmt + ",'"+MonthID+"'";
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
                }
            }
            else
            {
                dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "U";
                try
                {

                    string sqlprocInvoice = " EXEC [SP_SALE_INVOICE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "',"+
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "',"+
                                             " '" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + ","+
                                             " "  + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "',"+
                                             " '" + xmlTax + "','" + xmlGrossTax + "',"+
                                             " '" + xmlQtyScheme + "','"+xmlOfferPack+"'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "',"+
                                             " '" + DeliveryAddressID + "','" + DeliveryAddress + "'," +
                                             " '" + ICDSNo + "','" + ICDSDate + "','" + RebateSchemeID + "',"+
                                             " "  + RebatePercentage + "," + RebateValue + "," +
                                             " "  + AddRebatePercentage + "," + AddRebateValue + "," +
                                             " '" + SaleOrderID + "','" + CFormFlag + "'," + TotalPCS + ","+
                                             " "  + TotActualCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "'," + TaxableAmt + ",'" + MonthID + "'";
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
                }
            }

            return InvoiceNo;


        }

        #endregion

        #region Insert-Update CSD/CPC/INCS/Corporate/Ex-Para Invoice
        public string InsertCSDInvoice(     string InvoiceDate, string Customerid, string Customername, string wayBillNo,
                                            string TransporterID, string Vehicle, string DepotID, string DepotName, string BSID, string GroupID,
                                            string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate, string PaymentMode,
                                            string TransportMode, string userID, string finyear, string Remarks,
                                            decimal TotalValue, decimal Othercharges, decimal Adjustment,
                                            decimal RoundOff, decimal Disc, string strTermsID,
                                            string xmlInvoiceDetails, string xmlTax, string xmlGrossTax,
                                            string xmlQtyScheme, string xmlOfferPack, string xmlProductDetails,
                                            string hdnvalue, string ModuleID, string DeliveryAddressID,
                                            string DeliveryAddress, string ICDSNo, string ICDSDate,
                                            string RebateSchemeID, decimal RebatePercentage, decimal RebateValue,
                                            decimal AddRebatePercentage, decimal AddRebateValue,
                                            string SaleOrderID, string CFormFlag, decimal TotalPCS,
                                            decimal TotActualCase, string InvoiceType, string GrossWght,
                                            string IsChallan, decimal TaxableAmt, string MonthID)
        {

            string flag = "";
            string InvoiceID = string.Empty;
            string InvoiceNo = string.Empty;
            if (hdnvalue == "")
            {
                //dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "A";
                try
                {
                    string sqlprocInvoice = " EXEC [USP_CSD_INVOICE_INSERT_UPDATE] '','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "'," +
                                             " '" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," +
                                             " " + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "','" + xmlTax + "'," +
                                             " '" + xmlGrossTax + "','" + xmlQtyScheme + "','" + xmlOfferPack + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "'," +
                                             " '" + DeliveryAddressID + "','" + DeliveryAddress + "'," +
                                             " '" + ICDSNo + "','" + ICDSDate + "','" + RebateSchemeID + "'," +
                                             " " + RebatePercentage + "," + RebateValue + "," +
                                             " " + AddRebatePercentage + "," + AddRebateValue + "," +
                                             " '" + SaleOrderID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + TotActualCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "'," + TaxableAmt + ",'" + MonthID + "'";
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
                }
            }
            else
            {
                //dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "U";
                try
                {

                    string sqlprocInvoice = " EXEC [USP_CSD_INVOICE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "'," +
                                             " '" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," +
                                             " " + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "'," +
                                             " '" + xmlTax + "','" + xmlGrossTax + "'," +
                                             " '" + xmlQtyScheme + "','" + xmlOfferPack + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "'," +
                                             " '" + DeliveryAddressID + "','" + DeliveryAddress + "'," +
                                             " '" + ICDSNo + "','" + ICDSDate + "','" + RebateSchemeID + "'," +
                                             " " + RebatePercentage + "," + RebateValue + "," +
                                             " " + AddRebatePercentage + "," + AddRebateValue + "," +
                                             " '" + SaleOrderID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + TotActualCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "'," + TaxableAmt + ",'" + MonthID + "'";
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
                }
            }

            return InvoiceNo;


        }

        #endregion

        #region Insert-Update Export Invoice
        public string InsertExportInvoiceDetails(string InvoiceDate, string Customerid, string Customername, string ExportersRefNo,
                                                  string OthersRef, string Vehicle, string DepotID, string DepotName,
                                                  string BSID, string GroupID,
                                                  string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate,
                                                  string TransportMode, string userID, string finyear,
                                                  string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment,
                                                  decimal RoundOff, decimal Disc,
                                                  string strTermsID, string xmlInvoiceDetails, string xmlQtyScheme,string xmlTaxDetails,
                                                  string hdnvalue, string ModuleID, string LoadingPortID, string LoadingPortName,
                                                  string DischargePortID, string DischargePortName,
                                                  string FinalDestination,
                                                  decimal TotalCase, string ShippingBill, string ContainerNo,
                                                  string LCNo, string LCDate, string LCBankNo,
                                                  string Consignee, string NotifyParties, string CountryID, string CountryName,
                                                  string ShippingDate, string VOYNo, decimal TotalPCS, string BankID, string BankName,
                                                  string Branch, string BankAddress, string IFSCode, string SwiftCode, string AccNo,
                                                  string SaleOrderID,string InvoiceType, string InvoiceSeqType,decimal ExchangeRate)
        {

            string flag = "";
            string InvoiceID = string.Empty;
            string InvoiceNo = string.Empty;
            if (hdnvalue == "")
            {
                
                flag = "A";
                try
                {
                    string sqlprocInvoice = " EXEC [USP_EXPORT_INVOICE_INSERT_UPDATE] '','" + flag + "','" + InvoiceDate + "','" + Customerid + "','" + Customername + "'," +
                                             " '" + ExportersRefNo + "'," +
                                             " '" + OthersRef + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "','" + xmlQtyScheme + "','" + xmlTaxDetails + "'," +
                                             " '" + ModuleID + "','" + LoadingPortID + "','" + LoadingPortName + "','" + DischargePortID + "','" + DischargePortName + "'," +
                                             " '" + FinalDestination + "'," + TotalCase + ",'" + ShippingBill + "'," +
                                             " '" + ContainerNo + "','" + LCNo + "','" + LCDate + "','" + LCBankNo + "'," +
                                             " '" + Consignee + "','" + NotifyParties + "','" + CountryID + "','" + CountryName + "','" + ShippingDate + "','" + VOYNo + "'," +
                                             " " + TotalPCS + ",'" + BankID + "','" + BankName + "','" + Branch + "','" + BankAddress + "','" + IFSCode + "'," +
                                             " '" + SwiftCode + "','" + AccNo + "','" + SaleOrderID + "','" + InvoiceType + "','"+ InvoiceSeqType + "',"+ ExchangeRate + "";
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
                }
            }
            else
            {
                
                flag = "U";
                try
                {

                    string sqlprocInvoice = " EXEC [USP_EXPORT_INVOICE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "','" + Customerid + "','" + Customername + "'," +
                                             " '" + ExportersRefNo + "'," +
                                             " '" + OthersRef + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + BSID + "','" + GroupID + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," + Disc + "," +
                                             " '" + strTermsID + "','" + xmlInvoiceDetails + "','" + xmlQtyScheme + "','" + xmlTaxDetails + "'," +
                                             " '" + ModuleID + "','" + LoadingPortID + "','" + LoadingPortName + "','" + DischargePortID + "','" + DischargePortName + "'," +
                                             " '" + FinalDestination + "'," + TotalCase + ",'" + ShippingBill + "'," +
                                             " '" + ContainerNo + "','" + LCNo + "','" + LCDate + "','" + LCBankNo + "'," +
                                             " '" + Consignee + "','" + NotifyParties + "','" + CountryID + "','" + CountryName + "','" + ShippingDate + "','" + VOYNo + "'," +
                                             " " + TotalPCS + ",'" + BankID + "','" + BankName + "','" + Branch + "','" + BankAddress + "','" + IFSCode + "'," +
                                             " '" + SwiftCode + "','" + AccNo + "','" + SaleOrderID + "','" + InvoiceType + "','" + InvoiceSeqType + "'," + ExchangeRate + "";
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
                }
            }

            return InvoiceNo;


        }

        #endregion

        public int InsertPackingList(string SaleInvoiceID, string xml)
        {
            int e = 0;
            string sql = "EXEC USP_EXPORT_PACKINGLIST_INSERT '" + SaleInvoiceID + "','" + xml + "'";
            e = db.HandleData(sql);
            return e;
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

        #region Insert-Update GT Challan
        public string InsertOrderInvoiceDetails_Challan(string InvoiceDate, string Customerid, string Customername, string wayBillNo,
                                                string TransporterID, string Vehicle, string DepotID, string DepotName,
                                                string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate, string PaymentMode,
                                                string TransportMode, string userID, string finyear,
                                                string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment, decimal RoundOff, decimal SpecialDisc,
                                                string strTermsID, string bsID, string bsName, string groupID, string groupName, string OrderDate,
                                                string xmlInvoiceDetails, string xmlTax, string xmlGrossTax, string xmlQtyScheme,
                                                string xmlOfferPack, string xmlOrderInvoice,
                                                string xmlProductDetails, string hdnvalue, string ModuleID, string CFormFlag, decimal TotalPCS,
                                                decimal SSMarginPercentage, decimal SSMarginAmt, decimal ActualTotCase, string InvoiceType,
                                                string GrossWght, string IsChallan, string RefPONo,decimal TaxableAmt,string MonthID,string DeliveryAddress,string Distributorname,
                                                string retailername)
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
                    string sqlprocInvoice = " EXEC [SP_SALE_ORDERINVOICE_INSERT_UPDATE] '','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," +
                                             " " + RoundOff + "," + SpecialDisc + ",'" + strTermsID + "'," +
                                             " '" + bsID + "','" + bsName + "','" + groupID + "','" + groupName + "','" + OrderDate + "'," +
                                             " '" + xmlInvoiceDetails + "','" + xmlTax + "','" + xmlGrossTax + "'," +
                                             " '" + xmlQtyScheme + "','" + xmlOfferPack + "','" + xmlOrderInvoice + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + SSMarginPercentage + "," + SSMarginAmt + "," + ActualTotCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "','" + RefPONo + "'," + TaxableAmt + ",'" + MonthID + "',"+
                                             " '" + DeliveryAddress + "','"+ Distributorname + "','"+ retailername + "'";
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
                }
            }
            else
            {
                dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "U";
                try
                {

                    string sqlprocInvoice = " EXEC [SP_SALE_ORDERINVOICE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," +
                                             " " + Adjustment + "," + RoundOff + "," + SpecialDisc + ",'" + strTermsID + "'," +
                                             " '" + bsID + "','" + bsName + "','" + groupID + "','" + groupName + "','" + OrderDate + "'," +
                                             " '" + xmlInvoiceDetails + "','" + xmlTax + "','" + xmlGrossTax + "'," +
                                             " '" + xmlQtyScheme + "','" + xmlOfferPack + "','" + xmlOrderInvoice + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + SSMarginPercentage + "," + SSMarginAmt + "," + ActualTotCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "','" + RefPONo + "'," + TaxableAmt + ",'" + MonthID + "',"+
                                             " '"+DeliveryAddress+ "','" + Distributorname + "','" + retailername + "'";
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
                }
            }

            return InvoiceNo;


        }

        #endregion

        #region Insert-Update GT/E-Commerce/Bill Of Supply Invoice
        public string InsertOrderInvoiceDetails(string InvoiceDate, string Customerid, string Customername, string wayBillNo,
                                                string TransporterID, string Vehicle, string DepotID, string DepotName,
                                                string LRGRNo, string LRGRDate, string GetPassNo, string GetPassDate, string PaymentMode,
                                                string TransportMode, string userID, string finyear,
                                                string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment, decimal RoundOff, decimal SpecialDisc,
                                                string strTermsID, string bsID, string bsName, string groupID, string groupName, string OrderDate,
                                                string xmlInvoiceDetails, string xmlTax, string xmlGrossTax, string xmlQtyScheme,
                                                string xmlOfferPack, string xmlOrderInvoice,
                                                string xmlProductDetails, string hdnvalue, string ModuleID, string CFormFlag, decimal TotalPCS,
                                                decimal SSMarginPercentage, decimal SSMarginAmt, decimal ActualTotCase, string InvoiceType,
                                                string GrossWght, string IsChallan, string RefPONo, decimal TaxableAmt, string MonthID, string DeliveryAddress
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
                    string sqlprocInvoice = " EXEC [SP_SALE_ORDERINVOICE_INSERT_UPDATE] '','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," +
                                             " " + RoundOff + "," + SpecialDisc + ",'" + strTermsID + "'," +
                                             " '" + bsID + "','" + bsName + "','" + groupID + "','" + groupName + "','" + OrderDate + "'," +
                                             " '" + xmlInvoiceDetails + "','" + xmlTax + "','" + xmlGrossTax + "'," +
                                             " '" + xmlQtyScheme + "','" + xmlOfferPack + "','" + xmlOrderInvoice + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + SSMarginPercentage + "," + SSMarginAmt + "," + ActualTotCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "','" + RefPONo + "'," + TaxableAmt + ",'" + MonthID + "'," +
                                             " '" + DeliveryAddress + "'";
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
                }
            }
            else
            {
                dtSaleInvoiceRecord = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                flag = "U";
                try
                {

                    string sqlprocInvoice = " EXEC [SP_SALE_ORDERINVOICE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + InvoiceDate + "'," +
                                             " '" + Customerid + "','" + Customername + "','" + wayBillNo + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + GetPassNo + "','" + GetPassDate + "','" + PaymentMode + "'," +
                                             " '" + TransportMode + "','" + userID + "','" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," +
                                             " " + Adjustment + "," + RoundOff + "," + SpecialDisc + ",'" + strTermsID + "'," +
                                             " '" + bsID + "','" + bsName + "','" + groupID + "','" + groupName + "','" + OrderDate + "'," +
                                             " '" + xmlInvoiceDetails + "','" + xmlTax + "','" + xmlGrossTax + "'," +
                                             " '" + xmlQtyScheme + "','" + xmlOfferPack + "','" + xmlOrderInvoice + "'," +
                                             " '" + xmlProductDetails + "','" + ModuleID + "','" + CFormFlag + "'," + TotalPCS + "," +
                                             " " + SSMarginPercentage + "," + SSMarginAmt + "," + ActualTotCase + ",'" + InvoiceType + "'," +
                                             " '" + GrossWght + "','" + IsChallan + "','" + RefPONo + "'," + TaxableAmt + ",'" + MonthID + "'," +
                                             " '" + DeliveryAddress + "'";
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
                }
            }

            return InvoiceNo;


        }

        #endregion

        #region Bind CPC/CSD/INCS/MT/Corporate Invoice
        public DataTable BindInvoice(   string fromdate, string todate, string finyear, string BSID,
                                        string depotid, string CheckerFlag, string UserID,
                                        string IsChallan)
        {
            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                if (UserID == "160")
                {
                    if (IsChallan == "FALSE")
                    {
                        sql =   " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                                " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                                " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                                " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                                " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                                " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                                " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                                " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                                " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                                " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                                " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG ,(US.FNAME + ' ' + US.LNAME) AS USERNAME " +
                                " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                                " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                                " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                                " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                                " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                                " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                                " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                                " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " AND A.FINYEAR ='" + finyear + "'" +
                                " AND A.BSID = '" + BSID + "'" +
                                " AND A.WITHSALEORDER = 'Y'" +
                                " AND A.ISCHALLAN = 'N'" +
                                " AND A.DEPOTID='" + depotid + "'" +
                                " ORDER BY A.SALEINVOICENO DESC";
                    }
                    else
                    {
                        sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                                " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                                " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                                " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                                " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                                " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                                " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                                " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                                " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                                " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                                " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG ,(US.FNAME + ' ' + US.LNAME) AS USERNAME" +
                                " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                                " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                                " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                                " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                                " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                                " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                                " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                                " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " AND A.FINYEAR ='" + finyear + "'" +
                                " AND A.BSID = '" + BSID + "'" +
                                " AND A.WITHSALEORDER = 'Y'" +
                                " AND A.ISCHALLAN = 'Y'" +
                                " AND A.DEPOTID='" + depotid + "'" +
                                " ORDER BY A.SALEINVOICENO DESC";
                    }
                }
                else
                {
                    if (IsChallan == "FALSE")
                    {
                        sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                                " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                                " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                                " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                                " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                                " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                                " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                                " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                                " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                                " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                                " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG ,(US.FNAME + ' ' + US.LNAME) AS USERNAME" +
                                " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                                " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                                " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                                " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                                " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                                " INNER JOIN M_USER AS D ON A.NEXTLEVELID= D.USERID" +
                                " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                                " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " AND A.FINYEAR ='" + finyear + "'" +
                                " AND A.BSID = '" + BSID + "'" +
                                " AND A.WITHSALEORDER = 'Y'" +
                                " AND A.DEPOTID = '" + depotid + "'" +
                                " AND A.ISCHALLAN = 'N'" +
                                " ORDER BY A.SALEINVOICENO DESC";
                    }
                    else
                    {
                        sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                                " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                                " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                                " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                                " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                                " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                                " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                                " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                                " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                                " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                                " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG ,(US.FNAME + ' ' + US.LNAME) AS USERNAME" +
                                " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                                " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                                " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                                " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                                " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                                " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                                " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                                " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " AND A.FINYEAR ='" + finyear + "'" +
                                " AND A.BSID = '" + BSID + "'" +
                                " AND A.WITHSALEORDER = 'Y'" +
                                " AND A.DEPOTID = '" + depotid + "'" +
                                " AND A.ISCHALLAN = 'Y'" +
                                " ORDER BY A.SALEINVOICENO DESC";
                    }
                }
            }
            else if (CheckerFlag == "TRUE")
            {
                if (IsChallan == "FALSE")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.ISCHALLAN = 'N'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) LEFT JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.ISCHALLAN = 'Y'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Bind GT/E-Commerce Invoice
        public DataTable BindInvoiceWithoutSaleOrder(   string fromdate, string todate, string finyear, string BSID,
                                                        string depotid, string CheckerFlag, string UserID, 
                                                        string IsChallan)
        {

            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                if (IsChallan == "FALSE")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,(US.FNAME + ' ' + US.LNAME) AS USERNAME " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID = D.USERID" +
                            " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'N'" +
                            " AND A.DEPOTID = '" + depotid + "'" +
                            " AND A.ISCHALLAN = 'N'" +
                            " AND A.INVOICETYPEID <> '9'"+
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,(US.FNAME + ' ' + US.LNAME) AS USERNAME " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID " +
                            " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'N'" +
                            " AND A.DEPOTID = '" + depotid + "'" +
                            " AND A.ISCHALLAN = 'Y'" +
                            " AND A.INVOICETYPEID <> '9'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            else if (CheckerFlag == "TRUE")
            {
                if (IsChallan == "FALSE")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'N'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.ISCHALLAN = 'N'" +
                            " AND A.INVOICETYPEID <> '9'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.WAYBILLKEY,'') AS WAYBILLNO," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                            " CASE WHEN INTRANSIT='Y' THEN 'IN TRANSIT' ELSE 'RECEIVED' END AS INTRANSITDESC," +
                            " ISNULL(A.FORMREQUIRED,'') AS FORMREQUIRED,ISNULL(A.FORMNO,'') AS FORMNO,CONVERT(VARCHAR(10),CAST(A.FORMDATE AS DATE),103) AS FORMDATE," +
                            " ISNULL(A.GETPASSNO,'') AS GATEPASSNO,ISNULL(C.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON," +
                            " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                            " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                            " ON A.TRANSPORTERID = B.ID INNER JOIN" +
                            " T_SALEINVOICE_FOOTER AS C WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = C.SALEINVOICEID " +
                            " INNER JOIN M_USER AS D ON A.NEXTLEVELID=D.USERID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'N'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.ISCHALLAN = 'Y'" +
                            " AND A.INVOICETYPEID <> '9'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        
        public DataTable BindeExportInvoice(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID, string Currency)
        {
            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " /*AND A.ISVERIFIED <> 'Y'*/" +
                            " AND A.CREATEDBY = '" + UserID + "'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " /*AND A.ISVERIFIED <> 'Y'*/" +
                            " AND A.CREATEDBY = '" + UserID + "'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            else if (CheckerFlag == "1")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            else if (CheckerFlag == "2")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED = 'Y'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED = 'Y'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID = '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindeExportInvoiceGST(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID, string Currency)
        {
            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " /*AND A.ISVERIFIED <> 'Y'*/" +
                            " AND A.CREATEDBY = '" + UserID + "'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " /*AND A.ISVERIFIED <> 'Y'*/" +
                            " AND A.CREATEDBY = '" + UserID + "'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            else if (CheckerFlag == "1")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.NEXTLEVELID = '" + UserID + "'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED <> 'Y'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
            }
            else if (CheckerFlag == "2")
            {
                if (Currency == "0")
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED = 'Y'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
                else
                {
                    sql = " SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                            " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE," +
                            " ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                            " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR," +
                            " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, " +
                            " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                            " A.LOADINGPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.LOADINGPORTID = '0' THEN ''" +
                            "     ELSE A.LOADINGPORTNAME" +
                            " END AS LOADINGPORTNAME," +
                            " A.DISCHARGEPORTID," +
                            " CASE " +
                            "     WHEN " +
                            "          A.DISCHARGEPORTID = '0' THEN ''" +
                            "     ELSE A.DISCHARGEPORTNAME" +
                            " END AS DISCHARGEPORTNAME," +
                            " A.FINALDESTINATION,ISNULL(A.TOTALCASEPACK,0) AS TOTALCASEPACK,ISNULL(A.TOTALPCS,0) AS TOTALPCS," +
                            " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT," +
                            " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC" +
                            " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK)" +
                            " ON A.SALEINVOICEID = B.SALEINVOICEID INNER JOIN M_CUSTOMER AS C WITH (NOLOCK)" +
                            " ON C.CUSTOMERID = A.DISTRIBUTORID" +
                            " WHERE CONVERT(DATE,SALEINVOICEDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                            " AND A.FINYEAR ='" + finyear + "'" +
                            " AND A.BSID = '" + BSID + "'" +
                            " AND A.WITHSALEORDER = 'Y'" +
                            " AND A.ISVERIFIED = 'Y'" +
                            " AND C.CURRENCYID = '" + Currency + "'" +
                            " AND A.INVOICETYPEID <> '0'" +
                            " ORDER BY A.SALEINVOICENO DESC";
                }
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

        public int InvoiceDelete(string InvoiceID)
        {
            int delflag = 0;

            string sqldeleteInvoice = "EXEC [SP_SALE_INVOICE_DELETE] '" + InvoiceID + "'";

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

        public int ExportInvoiceDelete(string InvoiceID)
        {
            int delflag = 0;

            string sqldeleteInvoice = "EXEC [USP_EXPORT_INVOICE_DELETE] '" + InvoiceID + "'";

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
                      " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
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
                      " FROM T_SALEINVOICE_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
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

            string sql = " UPDATE T_SALEINVOICE_HEADER SET WAYBILLKEY = '" + WaybillNo + "'" +
                         " WHERE SALEINVOICEID ='" + InvoiceID + "'";

            int e = db.HandleData(sql);
            if (e == 0)
            {
                updateflag = 0;  // update unsuccessfull
            }
            else if (e > 0)
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
            string sql = " SELECT PSID,PSNAME FROM  Vw_PACKSIZE" +
                         " WHERE SEQUENCENO NOT IN ('2','4')" +
                         " ORDER BY SEQUENCENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChallanPackSize(string Po_ID)
        {
            string sql = " SELECT PSID,PSNAME FROM  Vw_PACKSIZE" +
                         " WHERE SEQUENCENO NOT IN ('2','4')" +
                         " ORDER BY SEQUENCENO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCorporatePackSize(string Po_ID)
        {
            string sql = " SELECT PSID,PSNAME FROM  Vw_PACKSIZE" +
                         " WHERE SEQUENCENO IN ('2','3')" +
                         " ORDER BY SEQUENCENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindInvoiceComboProduct(string CustomerID, string BSID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {

                sql = " IF  EXISTS( SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM M_CUSTOMER_PRODUCT_MAPPING" +
                      "             WHERE  CUSTOMERID = '" + CustomerID + "')" +
                      "     BEGIN " +
                      "        SELECT DISTINCT M_PRODUCT.ID AS PRIMARYPRODUCTID,M_PRODUCT.NAME AS PRIMARYPRODUCTNAME," +
                      "                        CASE WHEN M_PRODUCT.ISSINGLECARTOON IS NULL THEN '0' ELSE M_PRODUCT.ISSINGLECARTOON END AS ISSINGLECARTOON," +
                      "                        M_COMBO_PRODUCT.PRODUCTID AS SECONDARYPRODUCTID,M_COMBO_PRODUCT.PRODUCTNAME AS SECONDARYPRODUCTNAME,M_COMBO_PRODUCT.QTY" +
                      "        FROM M_CUSTOMER_PRODUCT_MAPPING INNER JOIN" +
                      "                        M_PRODUCT ON M_PRODUCT.ID=M_CUSTOMER_PRODUCT_MAPPING.PRODUCTID INNER JOIN" +
                      "                        M_COMBO_PRODUCT ON M_PRODUCT.ID = M_COMBO_PRODUCT.COMBOPRODUCTID" +
                      "        WHERE  CUSTOMERID = '" + CustomerID + "'" +
                      "        AND M_PRODUCT.SETFLAG = 'C'" +
                      "        ORDER BY PRIMARYPRODUCTNAME" +
                      "     END" +
                      " ELSE" +
                      "     BEGIN" +
                      "        SELECT DISTINCT A.ID  AS PRIMARYPRODUCTID ,A.NAME AS PRIMARYPRODUCTNAME," +
                      "                        CASE WHEN A.ISSINGLECARTOON IS NULL THEN '0' ELSE A.ISSINGLECARTOON END AS ISSINGLECARTOON," +
                      "                        C.PRODUCTID AS SECONDARYPRODUCTID,C.PRODUCTNAME AS SECONDARYPRODUCTNAME,C.QTY" +
                      "        FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B ON A.ID = B.PRODUCTID INNER JOIN" +
                      "                       M_COMBO_PRODUCT AS C ON A.ID = C.COMBOPRODUCTID" +
                      "        AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                      "        AND A.SETFLAG = 'C'" +
                      "        ORDER BY PRIMARYPRODUCTNAME" +
                      " END";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return ds;
        }

        public string getBrand(string ProductID)
        {
            string Brand = string.Empty;
            string sql = string.Empty;
            sql = "SELECT DIVID FROM M_PRODUCT WHERE ID = '" + ProductID + "'";
            Brand = (string)db.GetSingleValue(sql);
            return Brand;
        }

        public string getCurrencySymbol(string SaleOrderID)
        {
            string Symbol = string.Empty;
            string sql = string.Empty;
            sql = " SELECT A.SYMBOL FROM M_CURRENCY AS A INNER JOIN T_SALEORDER_HEADER AS B " +
                  " ON A.CURRENCYID = B.CURRENCYID" +
                  " AND B.SALEORDERID='" + SaleOrderID + "'";
            Symbol = (string)db.GetSingleValue(sql);
            return Symbol;
        }

        public string getCategory(string ProductID)
        {
            string Brand = string.Empty;
            string sql = string.Empty;
            sql = "SELECT CATID FROM M_PRODUCT WHERE ID = '" + ProductID + "'";
            Brand = (string)db.GetSingleValue(sql);
            return Brand;
        }

        public string getProductTaxStatus(string ProductID, string CustomerID, string TaxID)
        {
            string ProductWiseTax = string.Empty;
            string sql = string.Empty;
            sql = " IF  EXISTS(SELECT 1 FROM M_TAX_PRODUCT_MAPPING WHERE PRODUCT_ID='" + ProductID + "' AND VENDORID='" + CustomerID + "' AND TAXID='" + TaxID + "')" +
                    " BEGIN " +
                       " SELECT '1' " +
                    " END " +
                    " ELSE BEGIN SELECT 'NA'   END";
            ProductWiseTax = (string)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        public decimal SoapTaxPercentage(string ProductID, string CustomerID, string TaxID)
        {
            decimal SoapTaxPercentage = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(PERCENTAGE,0) FROM M_TAX_PRODUCT_MAPPING " +
                  " WHERE PRODUCT_ID='" + ProductID + "' AND VENDORID='" + CustomerID + "' AND TAXID='" + TaxID + "'";
            SoapTaxPercentage = (decimal)db.GetSingleValue(sql);
            return SoapTaxPercentage;
        }

        public string CheckFormRequired(string DespatchID)
        {
            string result = string.Empty;

            string sql = " IF  EXISTS(SELECT * FROM [T_SALEINVOICE_HEADER] WHERE FORMREQUIRED='Y' AND SALEINVOICEID = '" + DespatchID + "') BEGIN" +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END";

            result = (string)db.GetSingleValue(sql);
            return result;
        }

        public int UpdateCForm(string DespatchID, string CFormNo, string CFormDate)
        {
            int updateflag = 0;

            string sql = " UPDATE T_SALEINVOICE_HEADER SET FORMNO = '" + CFormNo + "',FORMDATE = CONVERT(DATETIME,'" + CFormDate + "',103)" +
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

        public DataTable BindExportProductDetails(string SaleOrderID, string DepotID, string ProformaInvID, string CustomerID,decimal ExchangeRate)
        {
            string sql = "EXEC USP_EXPORT_PRODUCT_DETAILS '" + SaleOrderID + "','" + DepotID + "','" + ProformaInvID + "','" + CustomerID + "',"+ ExchangeRate + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ExportCurrency(string Checker)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            if (Checker == "2")
            {
                sql = "SELECT CURRENCYID,CURRENCYNAME FROM ( SELECT DISTINCT A.CURRENCYID,A.CURRENCYNAME FROM M_CURRENCY AS A " +
                             " INNER JOIN M_CUSTOMER AS B " +
                             " ON A.CURRENCYID = B.CURRENCYID " +
                             " INNER JOIN T_SALEINVOICE_HEADER AS C " +
                             " ON B.CUSTOMERID = C.DISTRIBUTORID )f6" +
                             " WHERE  CURRENCYID NOT IN(SELECT CURRENCYID FROM P_APPMASTER)" +
                             " ORDER BY CURRENCYNAME";
            }
            else
            {
                sql = " SELECT DISTINCT A.CURRENCYID,A.CURRENCYNAME FROM M_CURRENCY AS A " +
                             " INNER JOIN M_CUSTOMER AS B " +
                             " ON A.CURRENCYID = B.CURRENCYID " +
                             " INNER JOIN T_SALEINVOICE_HEADER AS C " +
                             " ON B.CUSTOMERID = C.DISTRIBUTORID " +
                             " ORDER BY A.CURRENCYNAME";
            }

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

        public decimal RatePerCase(string BSID,string GRPID,string ProductID)
        {
            decimal RatePerCase = 0;
            string sql = string.Empty;
            sql = " EXEC USP_RatePerCase '"+ BSID + "','"+ GRPID + "','"+ ProductID + "'";
            RatePerCase = (decimal)db.GetSingleValue(sql);
            return RatePerCase;
        }

        public decimal GetADDFRETCHARGE()
        {
            decimal ADDFRETCHARGE = 0;
            string Sql = "SELECT ISNULL(ADDFRETCHARGE,0) AS ADDFRETCHARGE FROM P_APPMASTER ";
            ADDFRETCHARGE = (decimal)db.GetSingleValue(Sql);
            return ADDFRETCHARGE;
        }

        #region Update LR/GR No

        public DataTable BindInvoiceBlankLrGrNo(string fromdate, string todate, string finyear, string BSID, string depotid, string module)
        {
            string SQL = string.Empty;

            string SQLLRNO =    "  DECLARE @Names VARCHAR(MAX)" +
                                "  SELECT @Names = COALESCE(@Names + ', ', '') + LRGRNO" +
                                "  FROM T_TRANSPORTER_BILL_DETAILS INNER JOIN [T_TRANSPORTER_BILL_HEADER] " +
                                "  ON T_TRANSPORTER_BILL_HEADER.TRANSPORTERBILLID=T_TRANSPORTER_BILL_DETAILS.TRANSPORTERBILLID WHERE DEPOTID='" + depotid + "' " +
                                "  SELECT @Names";


            DataTable dt1 = new DataTable();
            dt1 = db.GetData(SQLLRNO);
            string LRGR = "";

            if (dt1.Rows.Count > 0)
            {
                LRGR = dt1.Rows[0][0].ToString();


            }

            if (module == "1")
            {


                SQL = "SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO + '/' + A.SALEINVOICESUFFIX) AS SALEINVOICENO," +
                     " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME," +
                     " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,ISNULL(A.GETPASSNO,'') AS GATEPASSNO," +
                     "ISNULL(LRGRNO,'') AS LRGRNO," +
                     " CASE WHEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) ELSE '01/01/1900' END AS LRGRDATE," +
                     " ISNULL(ACTUALTOTCASE,0) AS TOTALCASE ,ISNULL(VEHICHLENO,'') AS VEHICHLENO" +
                     " FROM T_SALEINVOICE_HEADER AS A WITH(NOLOCK)	INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK) ON  A.SALEINVOICEID=B.SALEINVOICEID" +
                     " WHERE CONVERT(DATE,SALEINVOICEDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103)" +
                     " AND A.BSID ='" + BSID + "' AND A.FINYEAR ='" + finyear + "' " +
                     "  AND DEPOTID='" + depotid + "' AND LRGRNO NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) " +
                     " ORDER BY A.SALEINVOICEDATE DESC";
            }
            else if (module == "2") /*STOCK TRANSFER*/
            {
                SQL = " select A.STOCKTRANSFERID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKTRANSFERDATE AS DATE),103) AS SALEINVOICEDATE," +
                    " STOCKTRANSFERNO AS SALEINVOICENO," +
                    " TODEPOTNAME AS DISTRIBUTORNAME,SUM(B.AMOUNT) AS NETAMOUNT,CHALLANNO AS GATEPASSNO," +
                    " ISNULL(LRGRNO,'') AS LRGRNO," +
                    " CASE WHEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) ELSE '01/01/1900' END AS LRGRDATE," +
                    " ISNULL(TOTALCASE,0)AS TOTALCASE ,ISNULL(VEHICHLENO,'') AS VEHICHLENO" +
                    " from [T_STOCKTRANSFER_HEADER] AS A INNER JOIN T_STOCKTRANSFER_DETAILS AS B ON A.STOCKTRANSFERID=B.STOCKTRANSFERID" +
                    " WHERE  MOTHERDEPOTID='" + depotid + "' AND CONVERT(DATE,A.STOCKTRANSFERDATE,103) between " +
                    " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                    " AND FINYEAR  ='" + finyear + "' AND LRGRNO NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) " +
                    " group by A.STOCKTRANSFERID,A.STOCKTRANSFERDATE,STOCKTRANSFERNO,TODEPOTNAME,CHALLANNO,LRGRNO,LRGRDATE,TOTALCASE, VEHICHLENO ORDER BY A.STOCKTRANSFERDATE";


                //SQL = " select STOCKTRANSFERID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS SALEINVOICEDATE,STOCKTRANSFERNO AS SALEINVOICENO," +
                //       " TODEPOTID,TODEPOTNAME AS DISTRIBUTORNAME,SUM(B.AMOUNT) AS NETAMOUNT,CHALLANNO AS GATEPASSNO," +
                //       "ISNULL(LRGRNO,'') AS LRGRNO," +
                //        " CASE WHEN (CONVERT(VARCHAR(10),CAST(CHALLANDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(CHALLANDATE AS DATE),103)) ELSE '01/01/1900' END AS LRGRDATE" +
                //       " from [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between " +
                //       " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                //       " AND FINYEAR ='" + finyear + "'  AND MOTHERDEPOTID='" + depotid + "' AND LRGRNO NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) " +
                //       " ORDER BY STOCKTRANSFERDATE DESC";
            }
            else if (module == "3") /*PURCHASE STOCK RECEIPT*/
            {
                SQL = "SELECT A.STOCKRECEIVEDID AS SALEINVOICEID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO + '/' + A.STOCKRECEIVEDSUFFIX) AS SALEINVOICENO," +
                    " CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.TPUNAME,'') AS DISTRIBUTORNAME," +
                    " ISNULL(B.TOTALRECEIVEDVALUE,0) AS NETAMOUNT,ISNULL(A.INVOICENO,'') AS GATEPASSNO," +
                    " ISNULL(LRGRNO,'') AS LRGRNO," +
                    " CASE WHEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) ELSE '01/01/1900' END AS LRGRDATE," +
                    " 0 AS TOTALCASE ,ISNULL(VEHICHLENO,'') AS VEHICHLENO " +
                    " FROM T_STOCKRECEIVED_HEADER AS A WITH(NOLOCK)	INNER JOIN T_STOCKRECEIVED_FOOTER AS B WITH (NOLOCK) ON  A.STOCKRECEIVEDID=B.STOCKRECEIVEDID" +
                    " WHERE CONVERT(DATE,A.STOCKRECEIVEDDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103)" +
                    " AND A.FINYEAR ='" + finyear + "'" +
                    " AND MOTHERDEPOTID='" + depotid + "'AND LRGRNO NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) " +
                    " ORDER BY A.STOCKRECEIVEDDATE DESC";
            }

            else if (module == "4") /*FACTORY DESPATCH*/
            {
                SQL = "SELECT A.STOCKDESPATCHID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS SALEINVOICEDATE," +
                        " (STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO +'/'+STOCKDESPATCHSUFFIX) AS SALEINVOICENO," +
                        " MOTHERDEPOTNAME AS DISTRIBUTORNAME,SUM(B.TOTALDESPATCHVALUE) AS NETAMOUNT,INVOICENO AS GATEPASSNO," +
                        " ISNULL(LRGRNO,'') AS LRGRNO," +
                        " CASE WHEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) " +
                        " ELSE '01/01/1900' END AS LRGRDATE," +
                        " 0 AS TOTALCASE ,ISNULL(VEHICHLENO,'') AS VEHICHLENO" +
                        " FROM T_STOCKDESPATCH_HEADER AS A INNER JOIN T_STOCKDESPATCH_FOOTER AS B ON A.STOCKDESPATCHID=B.STOCKDESPATCHID INNER JOIN M_TPU_VENDOR C " +
                        " ON A.TPUID = C.VENDORID " +
                        " WHERE C.TAG = 'F' AND  TPUID='" + depotid + "'  AND CONVERT(DATE,A.STOCKDESPATCHDATE,103) BETWEEN " +
                        " DBO.CONVERT_TO_ISO('" + fromdate + "') AND  DBO.CONVERT_TO_ISO('" + todate + "') " +
                        " AND FINYEAR  ='" + finyear + "' " +
                        " GROUP BY A.STOCKDESPATCHID,A.STOCKDESPATCHDATE,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO +'/'+STOCKDESPATCHSUFFIX)," +
                        " MOTHERDEPOTNAME,INVOICENO,LRGRNO,LRGRDATE,TOTALCASEPACK, VEHICHLENO,TOTALPCS ORDER BY A.STOCKDESPATCHDATE";
            }
            else if (module == "5")/*Sale Return*/
            {
                SQL = "SELECT A.SALERETURNID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALEINVOICEDATE," +
                        "(A.SALERETURNPREFIX+'/'+A.SALERETURNNO+'/'+A.SALERETURNSUFFIX) AS SALEINVOICENO ,A.DISTRIBUTORNAME AS DISTRIBUTORNAME,ISNULL(C.NETAMOUNT,0) AS NETAMOUNT," +
                        "ISNULL(GETPASSNO,'') AS GATEPASSNO," +
                        "ISNULL(LRGRNO,'') AS LRGRNO," +
                        "CASE WHEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103)) IS NOT NULL THEN (CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103))" +
                        "ELSE '01/01/1900' END AS LRGRDATE," +
                        "ISNULL(TOTALPCS,0)AS TOTALCASE ,ISNULL(VEHICHLENO,'') AS VEHICHLENO " +
                        "FROM T_SALERETURN_HEADER A " +
                        "INNER JOIN T_SALERETURN_FOOTER C ON A.SALERETURNID=C.SALERETURNID WHERE " +
                        "CONVERT(DATE,A.SALERETURNDATE,103) between " +
                        "dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND DEPOTID='" + depotid + "' AND BSID='" + BSID + "'" +
                        "ORDER BY A.SALERETURNDATE";

            }

            DataTable DT = db.GetData(SQL);
            return DT;

        }


        public DataTable BindStockTranferForUnlock(string fromdate, string todate, string finyear, string depotid)
        {
            string SQL = string.Empty;
            SQL = " select STOCKTRANSFERID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS SALEINVOICEDATE,STOCKTRANSFERNO AS SALEINVOICENO," +
                       " TODEPOTID,TODEPOTNAME AS DISTRIBUTORNAME,'' AS NETAMOUNT,CHALLANNO AS GATEPASSNO" +
                       " from [T_STOCKTRANSFER_HEADER] WITH (NOLOCK) where CONVERT(DATE,STOCKTRANSFERDATE,103) between " +
                       " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                       " AND FINYEAR ='" + finyear + "' AND MOTHERDEPOTID='" + depotid + "' " +
                       " ORDER BY STOCKTRANSFERDATE DESC";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public string BindTransporter(string SALEINVOICEID, string module)
        {
            string SQL = string.Empty;
            if (module == "1")
            {
                SQL = "SELECT TRANSPORTERID FROM T_SALEINVOICE_HEADER WHERE SALEINVOICEID='" + SALEINVOICEID + "'";
            }
            else if (module == "2")
            {
                SQL = " select  TRANSPORTERID  from [T_STOCKTRANSFER_HEADER] WHERE STOCKTRANSFERID='" + SALEINVOICEID + "'";
            }
            else if (module == "3")
            {
                SQL = " select  TRANSPORTERID  from [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID='" + SALEINVOICEID + "'";
            }
            else if (module == "4")
            {
                SQL = " select  TRANSPORTERID  from [T_STOCKDESPATCH_HEADER] WHERE STOCKDESPATCHID='" + SALEINVOICEID + "'";
            }
            else if (module == "5")
            {
                SQL = " select  TRANSPORTERID  from [T_SALERETURN_HEADER] WHERE SALERETURNID='" + SALEINVOICEID + "'";
            }
            string id = (string)db.GetSingleValue(SQL);
            return id;

        }


        public int UpdateLrGrNo(string depoptid, string xml, string MODULEID)
        {
            int result = 0;
            string sql = "EXEC [USP_UPDATE_LRGRNO] '" + depoptid + "','" + xml + "','" + MODULEID + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
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

        public DataTable BindExportDepot()
        {
            string sql = " SELECT BRID,BRNAME FROM M_BRANCH WHERE ISUSEDFOREXPORT='Y' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql =    " SELECT ISNULL(TAXPERCENTAGE,0) " +
                     " FROM T_SALEINVOICE_ITEMWISE_TAX " +
                     " WHERE SALEINVOICEID    =   '" + InvoiceID + "'" +
                     " AND TAXID              =   '" + TaxID + "'" +
                     " AND PRODUCTID          =   '" + ProductID + "'" +
                     " AND BATCHNO            =   '" + BatchNo + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }

        public string getofflinetag()
        {
            string OFFLINE = string.Empty;
            string sql = string.Empty;
            sql = "SELECT OFFLINE FROM P_APPMASTER";
            OFFLINE = (string)db.GetSingleValue(sql);
            return OFFLINE;
        }

        public DataTable GstNoStatus(string CustomerID)
        {

            string sql = " SELECT CASE WHEN GSTNO IS NULL THEN '1' WHEN GSTNO = '' THEN '1' ELSE GSTNO END AS GSTSTATUS " +
                            " FROM M_CUSTOMER" +
                            " WHERE CUSTOMERID = '" + CustomerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory()
        {

            string sql = " SELECT DISTINCT CT.CATID," +
                            " CASE WHEN CT.HSN IS NULL THEN CT.CATNAME WHEN CT.HSN='' THEN CT.CATNAME ELSE CT.CATNAME+'  ('+CT.HSN+')' END AS CATNAME " +
                            " FROM M_PRODUCT PR INNER JOIN M_CATEGORY CT " +
                            " ON PR.CATID = CT.CATID " +
                            " AND PR.DIVID <> '6B869907-14E0-44F6-9BA5-57F4DA726686' " +
                            " AND CT.ACTIVE='True' " +
                            " ORDER BY CATNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory_GT(string CustomerID)
        {

            string sql =    " SELECT DISTINCT CT.CATID, "+
                            " CASE WHEN CT.HSN IS NULL THEN CT.CATNAME WHEN CT.HSN = '' THEN CT.CATNAME ELSE CT.CATNAME + '  (' + CT.HSN + ')' END AS CATNAME "+
                            " FROM M_PRODUCT PR INNER JOIN M_CATEGORY CT "+
                            " ON PR.CATID = CT.CATID INNER JOIN M_CUSTOMER_BRAND_MAPPING BM "+
                            " ON PR.DIVID = BM.BRANDID "+
                            " /*AND PR.DIVID <> '6B869907-14E0-44F6-9BA5-57F4DA726686'*/ "+
                            " AND PR.DIVID IN(SELECT BRANDID FROM M_CUSTOMER_BRAND_MAPPING WHERE CUSTOMERID = '"+ CustomerID + "') "+
                            " AND CT.ACTIVE = 'True' "+
                            " ORDER BY CATNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string  WaybillStatus(string InvoiceID)
        {
            string Status = string.Empty;
            string sql = " SELECT CASE WHEN WAYBILLKEY IS NULL THEN '1' "+
                         " WHEN WAYBILLKEY = '' THEN '1' "+
                         " ELSE WAYBILLKEY END AS WAYBILLKEY "+
                         " FROM T_SALEINVOICE_HEADER WITH (NOLOCK) "+
                         " WHERE SALEINVOICEID = '" + InvoiceID + "'";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }
        public string GTMTECommPostingStatus(string DepotID,string BSID,string FinYear,string UserID,string CurrentDate)
        {
            string Status = string.Empty;
            string sql = "IF EXISTS (	SELECT 1 FROM VW_SALEINVOICE_APPROVAL " +
                         "              WHERE DEPOTID = '" + DepotID + "' " +
			             "              AND BSID='"+BSID+"' "+
			             "              AND ISVERIFIED <> 'Y' "+
                         "              /*AND FINYEAR = '" + FinYear + "' */" +
                         "              AND NEXTLEVELID = '" + UserID + "' " +
                         "              AND CONVERT(DATE,SALEINVOICEDATE,103) < DBO.Convert_To_ISO('" + CurrentDate + "') " +
		                 "          ) "+
                         " BEGIN "+
                         " 	  SELECT '1' "+
                         " END "+
                         " ELSE "+
                         " BEGIN "+
                         " 	  SELECT '0' "+
                         " END";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

        public string LocalDayEndStatus(string DepotID, string BSID, string FinYear, string UserID, string CurrentDate)
        {
            string Status = string.Empty;
            string sql = "IF EXISTS (	SELECT 1 FROM T_SALEINVOICE_HEADER " +
                         "              WHERE DEPOTID = '" + DepotID + "' " +
                         "              AND BSID='" + BSID + "' " +
                         "              AND DAYENDTAG <> 'Y' " +
                         "              /*AND FINYEAR = '" + FinYear + "' */" +
                         "              AND NEXTLEVELID = '" + UserID + "' " +
                         "              AND CONVERT(DATE,SALEINVOICEDATE,103) < DBO.Convert_To_ISO('" + CurrentDate + "') " +
                         "          ) " +
                         " BEGIN " +
                         " 	  SELECT '1' " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " 	  SELECT '0' " +
                         " END";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

        public DataTable BindTradingInvoiceApproval(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID)
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql =   " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
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
                sql =   " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
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

        public string FreeSampleCSD(string CustomerID)
        {
            string Status = string.Empty;
            string sql = " IF EXISTS (SELECT 1 FROM M_CUSTOMER WHERE CUSTOMERID='"+CustomerID+"' AND GROUPID IN (SELECT CSDSAMPLE FROM P_APPMASTER)) "+
                         " BEGIN"+
                         "	SELECT '1' "+
                         " END "+
                         " ELSE "+
                         " BEGIN "+
                         " 	SELECT '0' "+
                         " END";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }
        public string FreeSampleCPC(string CustomerID)
        {
            string Status = string.Empty;
            string sql = " IF EXISTS (SELECT 1 FROM M_CUSTOMER WHERE CUSTOMERID='" + CustomerID + "' AND GROUPID IN (SELECT CPCSAMPLE FROM P_APPMASTER)) " +
                         " BEGIN" +
                         "	SELECT '1' " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " 	SELECT '0' " +
                         " END";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

        public string FreeSampleMT(string CustomerID)
        {
            string Status = string.Empty;
            string sql = " IF EXISTS (SELECT 1 FROM M_CUSTOMER WHERE CUSTOMERID='" + CustomerID + "' AND GROUPID IN (SELECT MTSAMPLE FROM P_APPMASTER)) " +
                         " BEGIN" +
                         "	SELECT '1' " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " 	SELECT '0' " +
                         " END";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

        public string FreeSampleGT(string CustomerID)
        {
            string Status = string.Empty;
            string sql = "EXEC [free_sample_gt] '"+ CustomerID + "'";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

        public DataTable BindGTChallanCategory()
        {

            string sql = " SELECT DISTINCT CT.CATID," +
                            " CASE WHEN CT.HSN IS NULL THEN CT.CATNAME WHEN CT.HSN='' THEN CT.CATNAME ELSE CT.CATNAME+'  ('+CT.HSN+')' END AS CATNAME " +
                            " FROM M_PRODUCT PR INNER JOIN M_CATEGORY CT " +
                            " ON PR.CATID = CT.CATID " +
                            " AND CT.ACTIVE='True' " +
                            " ORDER BY CATNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessSegmentOnlineOffline(string module, string DEPOTID)
        {
            string sql = "EXEC USP_BIND_BUSINESSSEGMENT_ONLINE_OFFLINE '" + module + "','" + DEPOTID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        /* Get Delete or unlock data by D.Mondal on 28/11/2018 */
        public DataTable BindInvoiceCancel(string Status, string DepotID, string MODULEID, string GROUPID, string STARTDATE, string ENDDATE)
        {
            string sql = string.Empty;
            sql = " EXEC SP_RPT_CANCEL_VOUCHER_STATUS '" + Status + "','" + DepotID + "','" + MODULEID + "','" + GROUPID + "','" + STARTDATE + "','" + ENDDATE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMasterApproval(string fromdate, string todate, string ModuleID)
        {
            string sql = string.Empty;
            sql = " EXEC USP_MASTER_APPROVE '" + ModuleID + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /* Get Closing Date Checking Added By Sayan Dey on 28/05/2019*/
        public string Bindclosingstatus(string saleinvoicedate,string depotid,string finyear)
        {
            string Status = string.Empty;
            string sql = " EXEC [usp_businesssegmentwise_closing_date_checking] '" + saleinvoicedate + "','" + depotid + "','" + finyear + "' ";
            Status = (string)db.GetSingleValue(sql);
            return Status;
        }

    }
}
