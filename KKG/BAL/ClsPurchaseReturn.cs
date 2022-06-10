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
    public class ClsPurchaseReturn
    {
        DBUtils db = new DBUtils();
        DataTable dtSaleInvoiceRecord = new DataTable();

        public DataTable BindDepot_Transporter(string SALEINVOICEID)
        {
            string sql = string.Empty;
            if (SALEINVOICEID == "")
            {
                sql = " SELECT A.ID,A.NAME " +
                      " FROM M_TPU_TRANSPORTER AS A "+
                      " WHERE A.ISAPPROVED='Y'" +
                      " ORDER BY A.NAME";
            }
            else
            {
                sql = " SELECT A.ID,A.NAME " +
                      " FROM M_TPU_TRANSPORTER AS A " +
                      " ORDER BY A.NAME";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSuppliedItem()
        {
            string sql = string.Empty;

            sql = " SELECT ID AS DIS_CATID,ITEMDESC AS DIS_CATNAME FROM M_SUPLIEDITEM WHERE ID IN ('1','2','3','4')";
            
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
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

        

        public DataTable BindVendor(string SupldItem, string TxnID)
        {
            string sql = string.Empty;
            if (TxnID == "")
            {
                sql = " SELECT VENDORID AS CUSTOMERID,VENDORNAME AS CUSTOMERNAME" +
                      " FROM M_TPU_VENDOR " +
                      " WHERE SUPLIEDITEMID='" + SupldItem + "'" +
                      " AND TAG='T'" +
                      " AND ISAPPROVED='Y'" +
                      " ORDER BY VENDORNAME";
            }
            else
            {
                sql = " SELECT VENDORID AS CUSTOMERID,VENDORNAME AS CUSTOMERNAME" +
                      " FROM M_TPU_VENDOR " +
                      " WHERE SUPLIEDITEMID='" + SupldItem + "'" +
                      " AND TAG='T'" +
                      " ORDER BY VENDORNAME";
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

        public DataTable BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_RM_PM_BatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            string sql = "EXEC USP_P_RETURN_DEPOT '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "'";
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

        public DataTable BindPReturnProduct(string VendorID, string Type,string ReturnID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (ReturnID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B " +
                            " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_TPU_VENDOR AS VN " +
                            " ON B.VENDORID         =  VN.VENDORID " +
                            " AND D.PSID            = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " AND A.ACTIVE          = 'T' " +
                            " AND VN.SUPLIEDITEMID  ='" + Type + "' " +
                            " UNION ALL " +
                            " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " '0' AS SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID " +
                            " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.ID = B.PRODUCTID " +
                            " AND A.ACTIVE          = 'T' " +
                            " AND A.DIVID ='" + Type + "' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME ";
                           
                }
                else
                {
                    sql =   " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B " +
                            " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_TPU_VENDOR AS VN " +
                            " ON B.VENDORID         =  VN.VENDORID " +
                            " AND D.PSID            = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " AND VN.SUPLIEDITEMID  ='" + Type + "' " +
                            " UNION ALL " +
                            " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " '0' AS SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID " +
                            " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.ID = B.PRODUCTID " +
                            " AND A.DIVID  ='" + Type + "' " +
                            " AND A.ACTIVE          = 'T' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME ";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindPReturnCategoryProduct(string VendorID, string Type, string CategoryID, string ReturnID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (ReturnID == "")
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B " +
                            " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_TPU_VENDOR AS VN " +
                            " ON B.VENDORID         =  VN.VENDORID " +
                            " AND A.CATID           =  '" + CategoryID + "'" +
                            " AND D.PSID            = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " AND A.ACTIVE          = 'T' " +
                            " AND VN.SUPLIEDITEMID  ='" + Type + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME ";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,C.CATID " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B " +
                            " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_TPU_VENDOR AS VN " +
                            " ON B.VENDORID         =  VN.VENDORID " +
                            " AND A.CATID           =  '" + CategoryID + "'" +
                            " AND D.PSID            = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " AND VN.SUPLIEDITEMID  ='" + Type + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME ";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindNonFGProduct(string VendorID, string Type, string ReturnID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (ReturnID == "")
                {
                    sql =   " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS AS PRODUCTNAME, " +
                            " '0' AS SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B" +
                            " ON A.ID = B.PRODUCTID " +
                            " AND A.ACTIVE          = 'T' " +
                            " AND A.DIVID ='" + Type + "' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,PRODUCTNAME ";

                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS AS PRODUCTNAME, " +
                            " '0' AS SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_TPU_MAP AS B" +
                            " ON A.ID = B.PRODUCTID " +
                            " AND A.DIVID ='" + Type + "' " +
                            " AND B.VENDORID        = '" + VendorID + "' " +
                            " ORDER BY SEQUENCENO,DIVNAME,PRODUCTNAME ";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }


        public string Getstatus(string PRETURNID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_PURCHASERETURN_HEADER] WHERE PURCHASERETURNID='" + PRETURNID + "'  AND DAYENDTAG='Y' AND SYNCTAG='Y') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string GetFinancestatus(string PRETURNID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_PURCHASERETURN_HEADER] WHERE PURCHASERETURNID='" + PRETURNID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string BindRegion(string VendorID, string DepotID)
        {
            string sql =    " SELECT CAST(STATEID AS VARCHAR) AS STATEID FROM M_TPU_VENDOR " +
                            " WHERE STATEID IN(SELECT STATEID FROM dbo.M_BRANCH WHERE BRID='" + DepotID + "') " +
                            " AND VENDORID='" + VendorID + "' ";
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

            sqlState = " SELECT CAST(STATEID AS VARCHAR(2)) AS STATEID  FROM M_TPU_VENDOR WHERE VENDORID = '" + CustomerID + "'";
            
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
            string sql = "EXEC USP_PRETURN_DETAILS '" + InvoiceID + "'";
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
            string sql = " EXEC USP_QTYINPCS_PRETURN '" + productID + "','" + packsizeFromID + "'," + DeliveredQty + "," +
                         " " + StockQty + "";
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

        #region Insert-Update Purchase Return
        public string PReturnInsertUpdate(      string hdnvalue, string PURCHASERETURNDATE, string VENDORID, 
                                                string VENDORNAME, string TRANSPORTERID,
                                                string VEHICLENO, string DEPOTID, string DEPOTNAME, string TYPEID,
                                                string TYPENAME, string CREATEDBY, string FINYEAR, string REMARKS,
                                                decimal TOTALBASICAMT, decimal OTHERCHARGESAMT, decimal ADJUSTMENTAMT, 
                                                decimal ROUNDOFFAMT, decimal NETAMT,decimal GROSSAMT,
                                                string xmlPRETURNDETAILS, string xmlITEMWISETAXDETAILS,
                                                decimal TOTALPCS, decimal TOTALCASE, string INVOICETYPE, string GROSSWGHT,
                                                string INVOICENO, string INVOICEDATE, string LRNO, string LRDATE

                                           )
        {

            string flag = "";
            string InvoiceID = string.Empty;
            string InvoiceNo = string.Empty;
            if (hdnvalue == "")
            {
                
                flag = "A";
                try
                {

                    string sqlproc = " EXEC [USP_PURCHASE_RETURN_INSERT_UPDATE] '','" + flag + "','" + PURCHASERETURNDATE + "'," +
                                             " '" + VENDORID + "','" + VENDORNAME + "','" + TRANSPORTERID + "'," +
                                             " '" + VEHICLENO + "','" + DEPOTID + "','" + DEPOTNAME + "'," +
                                             " '" + TYPEID + "','" + TYPENAME + "','" + CREATEDBY + "'," +
                                             " '" + FINYEAR + "','" + REMARKS + "'," + TOTALBASICAMT + "," +
                                             " " + OTHERCHARGESAMT + "," + ADJUSTMENTAMT + "," + ROUNDOFFAMT + "," + NETAMT + "," +
                                             " " + GROSSAMT + ",'" + xmlPRETURNDETAILS + "','" + xmlITEMWISETAXDETAILS + "'," +
                                             " " + TOTALPCS + "," + TOTALCASE + ",'" + INVOICETYPE + "','" + GROSSWGHT + "'," +
                                             "'"+ INVOICENO + "','"+ INVOICEDATE + "','"+ LRNO + "','"+ LRDATE + "'";

                    DataTable dtDespatch = db.GetData(sqlproc);

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

                    string sqlproc = " EXEC [USP_PURCHASE_RETURN_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + PURCHASERETURNDATE + "'," +
                                             " '" + VENDORID + "','" + VENDORNAME + "','" + TRANSPORTERID + "'," +
                                             " '" + VEHICLENO + "','" + DEPOTID + "','" + DEPOTNAME + "'," +
                                             " '" + TYPEID + "','" + TYPENAME + "','" + CREATEDBY + "'," +
                                             " '" + FINYEAR + "','" + REMARKS + "'," + TOTALBASICAMT + "," +
                                             " " + OTHERCHARGESAMT + "," + ADJUSTMENTAMT + "," + ROUNDOFFAMT + "," + NETAMT + "," +
                                             " " + GROSSAMT + ",'" + xmlPRETURNDETAILS + "','" + xmlITEMWISETAXDETAILS + "'," +
                                             " " + TOTALPCS + "," + TOTALCASE + ",'" + INVOICETYPE + "','" + GROSSWGHT + "',"+
                                             "'" + INVOICENO + "','" + INVOICEDATE + "','" + LRNO + "','" + LRDATE + "'";

                    DataTable dtDespatch = db.GetData(sqlproc);

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

        #region Bind PurchaseReturn
        public DataTable BindPReturn(   string fromdate, string todate, string finyear, 
                                                        string depotid, string UserID 
                                                    )
        {

            string sql = string.Empty;

            sql =   " SELECT A.PURCHASERETURNID AS SALEINVOICEID, (A.PURCHASERETURNPREFIX + '/' + A.PURCHASERETURNNO) AS SALEINVOICENO, " +
                    " CONVERT(VARCHAR(10),CAST(A.PURCHASERETURNDATE AS DATE),103) AS SALEINVOICEDATE, " +
                    " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME, " +
                    " A.VENDORID AS DISTRIBUTORID,ISNULL(A.VENDORNAME,'') AS DISTRIBUTORNAME, " +
                    " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED, " +
                    " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC, " +
                    " ISNULL(C.NETAMOUNT,0) AS NETAMOUNT,(D.FNAME+' '+D.LNAME) AS APPROVAL_PERSON, " +
                    " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,(US.FNAME + ' ' + US.LNAME) AS USERNAME " +
                    " FROM T_PURCHASERETURN_HEADER AS A WITH (NOLOCK) INNER JOIN " +
                    " M_TPU_TRANSPORTER AS B WITH (NOLOCK) " +
                    " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                    " T_PURCHASERETURN_FOOTER AS C WITH (NOLOCK) " +
                    " ON A.PURCHASERETURNID = C.PURCHASERETURNID " +
                    " INNER JOIN M_USER AS D ON A.NEXTLEVELID = D.USERID " +
                    " INNER JOIN M_USER AS US ON A.CREATEDBY = US.USERID " +
                    " WHERE CONVERT(DATE,PURCHASERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                    " AND A.FINYEAR ='" + finyear + "' " +
                    " AND A.DEPOTID = '" + depotid + "' " +
                    " ORDER BY A.PURCHASERETURNNO DESC ";
               
            
            
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        

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
                sql =   " SELECT TOTALSALEINVOICEVALUE,TOTALSPECIALDISCVALUE,TAXVALUE,SALEINVOICEID,ROUNDOFFVALUE,ADJUSTMENT,GROSSREBATEVALUE,DISTRIBUTORID,DISTRIBUTORNAME," +
                        " DEPOTID,SALEINVOICENO,SALEINVOICEDATE,GETPASSNO,FINYEAR,BSID,ISVERIFIED,NEXTLEVELID,ISVERIFIEDDESC,BILLTYPE FROM VW_SALEINVOICE_APPROVAL " +
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

        public int InvoiceDelete(string InvoiceID)
        {
            int delflag = 0;

            string sqldeleteInvoice = "EXEC [USP_PRETURN_DELETE] '" + InvoiceID + "'";

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

        public DataTable BindPackSize(string Po_ID)
        {
            string sql = " SELECT PSID,PSNAME FROM  Vw_PACKSIZE" +
                         " WHERE SEQUENCENO NOT IN ('2','4')" +
                         " ORDER BY SEQUENCENO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMMPackSize(string ProductID)
        {
            string sql = string.Empty;

            sql = " SELECT PSID ,PSNAME,'1' AS RANK FROM [Vw_MMUNIT] " +
                    " WHERE PRODUCTID='" + ProductID + "'" +
                    " ORDER BY RANK";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPOPPackSize(string ProductID)
        {
            string sql = string.Empty;
           
            sql = " SELECT PSID,PSNAME,RANK FROM  VW_PCS" +
                  " WHERE PRODUCTID = '" + ProductID + "'" +
                  " UNION ALL" +
                  " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                  " WHERE PSID ='1970C78A-D062-4FE9-85C2-3E12490463AF'" +
                  " AND PRODUCTID = '" + ProductID + "'" +
                  " ORDER BY RANK ";
       
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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
            sql = " SELECT ISNULL(B.HSNCODE,'') AS HSNCODE FROM M_PRODUCT A INNER JOIN M_HSNCODE B " +
                  " ON A.CATID=B.CATID " +
                  " AND A.ID='" + ProductID + "'";
            HSNCODE = (string)db.GetSingleValue(sql);
            return HSNCODE;
        }

        public decimal GetHSNTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql =    " SELECT ISNULL(TAXPERCENTAGE,0) " +
                     " FROM T_PURCHASERETURN_ITEMWISE_TAX " +
                     " WHERE PURCHASERETURNID       =   '" + InvoiceID + "'" +
                     " AND TAXID                    =   '" + TaxID + "'" +
                     " AND PRODUCTID                =   '" + ProductID + "'" +
                     " AND BATCHNO                  =   '" + BatchNo + "'";
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
        
        public string GTMTECommPostingStatus(string DepotID,string BSID,string FinYear,string UserID,string CurrentDate)
        {
            string Status = string.Empty;
            string sql = "IF EXISTS (	SELECT 1 FROM VW_SALEINVOICE_APPROVAL " +
                         "              WHERE DEPOTID = '" + DepotID + "' " +
			             "              AND BSID='"+BSID+"' "+
			             "              AND ISVERIFIED <> 'Y' "+
                         "              AND FINYEAR = '" + FinYear + "' " +
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
            string sql = "IF EXISTS (	SELECT 1 FROM T_PURCHASERETURN_HEADER " +
                         "              WHERE DEPOTID = '" + DepotID + "' " +
                         "              AND DAYENDTAG <> 'Y' " +
                         "              AND FINYEAR = '" + FinYear + "' " +
                         "              AND CONVERT(DATE,PURCHASERETURNDATE,103) < DBO.Convert_To_ISO('" + CurrentDate + "') " +
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

        public DataTable BindPurchaseReturnApproval(string fromdate, string todate, string finyear, string depotid, string CheckerFlag, string UserID)    /*Tag (T) = TPU , (F) = Factory */
        {
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql =   " SELECT A.PURCHASERETURNID AS SALEINVOICEID, (A.PURCHASERETURNPREFIX + '/' + A.PURCHASERETURNNO) AS SALEINVOICENO, "+
                        " A.VENDORNAME AS DISTRIBUTORNAME,A.VENDORID AS DISTRIBUTORID, " +
                        " CONVERT(VARCHAR(10), CAST(A.PURCHASERETURNDATE AS DATE), 103) AS SALEINVOICEDATE,(ISNULL(A.WAYBILLNO, '') + ' ( ' + A.WAYBILLKEY + ' )') AS GETPASSNO,"+
                        " DEPOTID AS DEPOTID, ISNULL(A.FINYEAR, '') AS FINYEAR," +
                        " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED,0 AS ADJUSTMENT,0 AS GROSSREBATEVALUE," +
                        " CASE WHEN V.SUPLIEDITEMID = '1' THEN 'FG' WHEN V.SUPLIEDITEMID = '2' THEN 'PM' WHEN V.SUPLIEDITEMID = '3' THEN 'RM'  ELSE 'POP' END BILLTYPE," +
                        " CASE WHEN ISVERIFIED = 'N' THEN 'PENDING'  WHEN ISVERIFIED = 'R' THEN 'REJECTED' WHEN ISVERIFIED = 'H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                        " ISNULL(C.NETAMOUNT, 0) AS TOTALSALEINVOICEVALUE,0 AS TOTALSPECIALDISCVALUE,0 AS TAXVALUE, ISNULL(C.ROUNDOFFVALUE, 0) AS ROUNDOFFVALUE,0 AS TOTALTDS " +
                        " FROM T_PURCHASERETURN_HEADER AS A " +
                        " INNER JOIN T_PURCHASERETURN_FOOTER AS C ON A.PURCHASERETURNID = C.PURCHASERETURNID INNER JOIN M_TPU_VENDOR V ON A.VENDORID = V.VENDORID " +
                        " WHERE CONVERT(DATE, PURCHASERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('"+ fromdate + "') AND DBO.Convert_To_ISO('"+ todate + "') " +
                        " AND FINYEAR = '"+ finyear + "' " +
                        " AND DEPOTID = '"+ depotid + "' " +
                        " AND ISVERIFIED<> 'Y'" +
                        " AND NEXTLEVELID = '"+ UserID + "' " +
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
