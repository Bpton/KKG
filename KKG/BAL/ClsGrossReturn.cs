
#region Developer Info

/*
 Developer Name : Avishek Ghosh
 * Start Date   : 20/07/2017
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
    public class ClsGrossReturn
    {
        DBUtils db = new DBUtils();

        public DataTable BindDepot_Transporter(string DepotID, string SALERETURNID)
        {
            string sql = string.Empty;
            if (SALERETURNID == "")
            {
                sql = " SELECT A.ID,A.NAME " +
                            " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                                 " M_TRANSPOTER_DEPOT_MAP AS B " +
                            " ON A.ID=B.TRANSPOTERID" +
                            " WHERE B.DEPOTID='" + DepotID + "'" +
                            " AND B.TAG='D' AND ISAPPROVED='Y'" +
                            " ORDER BY A.NAME";
            }
            else
            {
                sql = " SELECT A.ID,A.NAME " +
                            " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                                 " M_TRANSPOTER_DEPOT_MAP AS B " +
                            " ON A.ID=B.TRANSPOTERID" +
                            " WHERE B.DEPOTID='" + DepotID + "'" +
                            " AND B.TAG='D'" +
                            " ORDER BY A.NAME";
            }
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

        public DataTable BindBS()
        {

            string sql = " SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT "+
                         " WHERE BSID <> '33F6AC5E-1F37-4B0F-B959-D1C900BB43A5' "+
                         " ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomer(string bsID, string groupid, string DEPOTID)
        {
            string sql = string.Empty;
            if (DEPOTID == "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670" && bsID == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" && groupid == "AFF95D83-8297-40B3-A140-738F52D3F63C")
            {
                sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID=(SELECT TARGETSALESID FROM P_APPMASTER) ";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;

            }
            else
            {
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + DEPOTID + "') " +
                            "  BEGIN " +
                            "  SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER A " +
                            "  INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                            "   AND GROUPID LIKE '%" + groupid + "%' " +
                             "   AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE NOT IN('RT')) " +
                             "   AND B.DEPOTID='" + DEPOTID + "' " +
                           " END " +
                            "    ELSE " +
                             "   BEGIN " +
                              "  SELECT CUSTOMERID,CUSTOMERNAME  FROM M_CUSTOMER   " +
                               " WHERE BUSINESSSEGMENTID LIKE '%" + bsID + "%'  " +
                               " AND GROUPID LIKE '%" + groupid + "%'  " +
                               " AND CUSTYPE_ID IN (SELECT UTID FROM M_USERTYPE WHERE UTCODE NOT IN('RT')) ORDER BY CUSTOMERNAME " +
                               " END";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
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

        public DataTable BindInvoiceCustomer(string bsID, string DEPOTID)
        {
            string sql = string.Empty;
           
                sql = "  IF EXISTS(SELECT DISTINCT 1 FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (" + DEPOTID + ")) " +
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

        
        public DataTable BindInvoiceProduct(string CustomerID, string BSID, string SALERETURNID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if(SALERETURNID=="")
                {
                sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO, A.CATNAME,A.DIVNAME,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' AND A.ACTIVE = 'T'" +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                      " AND A.TYPE = 'FG'" +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE NOT IN ('FG','PM','RM','SFG') AND ACTIVE = 'T' " +
                      " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";
                }
                else
                {
                    sql = " SELECT DISTINCT A.ID  AS PRODUCTID ," +
                      " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME," +
                      " C.SEQUENCENO, A.CATNAME,A.DIVNAME,C.CATID " +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                      " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                      " ON A.ID = D.PRODUCTID " +
                      " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                      " AND B.BUSNESSSEGMENTID = '" + BSID + "'" +
                      " AND A.TYPE = 'FG'" +
                      " UNION ALL " +
                      " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                      " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                      " FROM M_PRODUCT " +
                      " WHERE TYPE NOT IN ('FG','PM','RM','SFG') " +
                      " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";
                }

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }

        public DataTable BindInvoiceCategoryProduct(string CustomerID, string BSID, string CategoryID, string SALERETURNID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (SALERETURNID == "")
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
                          " AND A.CATID='" + CategoryID + "'" +
                          " AND A.TYPE = 'FG' AND A.ACTIVE = 'T'" +
                          " UNION ALL " +
                          " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                          " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                          " FROM M_PRODUCT " +
                          " WHERE TYPE NOT IN ('FG','PM','RM','SFG')" +
                          " AND CATID='" + CategoryID + "' AND A.ACTIVE = 'T'" +
                          " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";
                }
                else
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
                         " AND A.CATID='" + CategoryID + "'" +
                         " AND A.TYPE = 'FG'" +
                         " UNION ALL " +
                         " SELECT DISTINCT ID  AS PRODUCTID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS PRODUCTNAME," +
                         " '999' AS SEQUENCENO, CATNAME,DIVNAME,CATID " +
                         " FROM M_PRODUCT " +
                         " WHERE TYPE NOT IN ('FG','PM','RM','SFG')" +
                         " AND CATID='" + CategoryID + "'" +
                         " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";
                }

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

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALEINVOICE_HEADER] WHERE SALEINVOICEID='" + SALEINVOICEID + "'  AND DAYENDTAG='Y' AND SYNCH_STATUS='Y') BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public decimal MRP(string PRODUCTID)
        {
            string Sql = " SELECT ISNULL(MRP,0) AS MRP FROM M_PRODUCT WHERE ID = '" + PRODUCTID + "'";
            decimal MRP = ((decimal)db.GetSingleValue(Sql));
            return MRP;
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

        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string CustomerID, string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            string StateID = string.Empty;
            //if (flag == "1")
            //{
            sqlState = "SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            //}
            //else
            //{
            //    if (CFormFlag == "0")
            //    {
            //        sqlState = "SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            //    }
            //    else
            //    {
            //        sqlState = "SELECT CAST (STATEID AS VARCHAR (3)) AS STATEID  FROM M_BRANCH  WHERE BRID='" + VendorID + "'";
            //    }
            //}
            StateID = (string)db.GetSingleValue(sqlState);
            if (flag == "1") //  Outer State
            {
                //sql =       " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +    
                //            " FROM ( "+
                //            " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                //            " FROM M_TAX AS A" +
                //            " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                //            " WHERE A.ACTIVE='True'" +
                //            " AND A.RELATEDTO IN (1,4,5)" +
                //            " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                //            " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 where PERCENTAGE > 0" +  
                //            " ORDER BY RELATEDTO ";

                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                      " FROM ( " +
                      " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
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
                //sql =       " SELECT TAXCOUNT ,NAME +' ('+ CAST(PERCENTAGE AS VARCHAR(25))+' %'+')' AS NAME ,PERCENTAGE,RELATEDTO " +
                //            " FROM ( " +
                //            " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + CustomerID + "','" + ProductID + "','" + StateID + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                //            " FROM M_TAX AS A" +
                //            " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                //            " WHERE A.ACTIVE='True'" +
                //            " AND A.RELATEDTO IN (1,4,5)" +
                //            " AND B.MENUID = '" + menuID + "' AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                //            " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 where PERCENTAGE > 0" +
                //            " ORDER BY RELATEDTO ";

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

        public DataSet EditGrossReturnDetails(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_GROSS_RETURN_DETAILS '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string GetProductExpirydate(string prodid, string date)
        {
            string Expdate = "";
            Expdate = (string)db.GetSingleValue("exec [sp_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
            return Expdate;
        }

        public DataTable MasterBatchDetailsCheck(string ProductID, string BatchNo, decimal Mrp, string MFDate, string EXPRDate, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_MASTERBATCH '" + ProductID + "','" + BatchNo + "','" + Mrp + "','" + MFDate + "','" + EXPRDate + "','" + Finyear + "'";
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

        public DataTable QtyInPcsGT(string productID, string packsizeFromID, decimal DeliveredQty, decimal StockQty)
        {
            DataTable dtQtyInPCS = new DataTable();
            string sql = "EXEC USP_QTYINPCS '" + productID + "','" + packsizeFromID + "'," + DeliveredQty + "," + StockQty + "";
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

        #region Insert-Update Gross Return
        public string InsertGrossReturn( string ReturnDate, string Customerid, string Customername,
                                         string DepotID, string DepotName, string userID, string finyear,
                                         string Remarks, string bsID, string groupID, decimal TotalPCS,
                                         string GetPassNo, string LRGRNo, string LRGRDate,
                                         string TransporterID, decimal TotalValue, decimal Othercharges,
                                         decimal Adjustment, decimal RoundOff, decimal SpecialDisc,
                                         decimal NetAmt, decimal GrossAmt,
                                         string xmlInvoiceDetails, string xmlTax, string GrossTax,
                                         string hdnvalue,string InvoiceType,string INVNO,string INVDATE
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
                    string sqlprocInvoice = " EXEC [USP_GROSS_RETURN_INSERT_UPDATE] '','" + flag + "','" + ReturnDate + "'," +
                                             " '" + Customerid + "','" + Customername + "'," +
                                             " '" + DepotID + "','" + DepotName + "','" + userID + "'," +
                                             " '" + finyear + "','" + Remarks + "','" + bsID + "'," +
                                             " '" + groupID + "'," + TotalPCS + ",'" + GetPassNo + "'," +
                                             " '" + LRGRNo + "','" + LRGRDate + "','" + TransporterID + "'," +
                                             " '" + TotalValue + "'," + Othercharges + "," + Adjustment + ","+
                                             " " + RoundOff + "," + SpecialDisc + "," +
                                             " " + NetAmt + "," + GrossAmt + "," +
                                             "'" + xmlInvoiceDetails + "','" + xmlTax + "','" + GrossTax + "',"+
                                             " '" + InvoiceType + "','" + INVNO + "','" + INVDATE + "'";
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
               
                flag = "U";
                try
                {

                    string sqlprocInvoice =  " EXEC [USP_GROSS_RETURN_INSERT_UPDATE] '" + hdnvalue.Trim() + "',"+
                                             " '" + flag + "','" + ReturnDate + "'," +
                                             " '" + Customerid + "','" + Customername + "'," +
                                             " '" + DepotID + "','" + DepotName + "','" + userID + "'," +
                                             " '" + finyear + "','" + Remarks + "','" + bsID + "'," +
                                             " '" + groupID + "'," + TotalPCS + ",'" + GetPassNo + "'," +
                                             " '" + LRGRNo + "','" + LRGRDate + "','" + TransporterID + "'," +
                                             " '" + TotalValue + "'," + Othercharges + "," + Adjustment + "," +
                                             " " + RoundOff + "," + SpecialDisc + "," +
                                             " " + NetAmt + "," + GrossAmt + "," +
                                             "'" + xmlInvoiceDetails + "','" + xmlTax + "','" + GrossTax + "',"+
                                             " '" + InvoiceType + "','" + INVNO + "','" + INVDATE + "'";
                    
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

            return InvoiceNo;


        }

        #endregion

        public string GetFinancestatus(string SALERETURNID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_SALERETURN_HEADER] WHERE SALERETURNID ='" + SALERETURNID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                         "  SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindGrossReturn(string fromdate, string todate, string finyear, string BSID, string depotid, string CheckerFlag, string UserID)
        {

            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                sql = " SELECT A.SALERETURNID AS SALERETURNID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALERETURNNO," +
                         " CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALERETURNDATE, " +
                         " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID," +
                         " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, ISVERIFIED,GETPASSNO," +
                         " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC ," +
                         " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT ," +
                         " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,A.BSNAME  AS BUSINESSSEGMENT " +
                         " FROM T_SALERETURN_HEADER AS A INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK)" +
                         " ON A.SALERETURNID = B.SALERETURNID " +
                         " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                         " AND A.FINYEAR ='" + finyear + "' " +
                         " AND A.DEPOTID='" + depotid + "' " +
                         " AND A.CREATEDBY = '" + UserID + "' " +
                         " AND A.GROSSRETURN = 'Y'" +
                         " AND A.TAG='FG'" +
                         " ORDER BY SALERETURNNO DESC";
            }
            else if (CheckerFlag == "TRUE")
            {
                sql = "  SELECT A.SALERETURNID AS SALERETURNID, (A.SALERETURNPREFIX + '/' + A.SALERETURNNO) AS SALERETURNNO," +
                         " CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALERETURNDATE, " +
                         " ISNULL(A.DEPOTNAME,'') AS DEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,A.TRANSPORTERID AS TRANSPORTERID,GETPASSNO," +
                         " A.DISTRIBUTORID AS DISTRIBUTORID,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME, ISVERIFIED," +
                         " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC , " +
                         " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT ," +
                         " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,A.BSNAME  AS BUSINESSSEGMENT " +
                         " FROM T_SALERETURN_HEADER AS A INNER JOIN T_SALERETURN_FOOTER AS B WITH (NOLOCK)" +
                         " ON A.SALERETURNID = B.SALERETURNID " +
                         " WHERE CONVERT(DATE,SALERETURNDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "') " +
                         " AND A.FINYEAR ='" + finyear + "' " +
                         " AND A.DEPOTID='" + depotid + "' " +
                         " AND A.ISVERIFIED <> 'Y'" +
                         " AND A.GROSSRETURN = 'Y'" +
                         " AND A.TAG='FG'" +
                         " /*AND A.NEXTLEVELID = '" + UserID + "'*/" +
                         " ORDER BY SALERETURNNO DESC";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetHSNTaxOnEdit(string ReturnID, string TaxID, string ProductID, string BatchNo)
        {
            decimal ProductWiseTax = 0;
            string sql = string.Empty;
            sql = " SELECT ISNULL(TAXPERCENTAGE,0) " +
                     " FROM T_SALERETURN_ITEMWISE_TAX " +
                     " WHERE SALERETURNID    =   '" + ReturnID + "'" +
                     " AND TAXID              =   '" + TaxID + "'" +
                     " AND PRODUCTID          =   '" + ProductID + "'" +
                     " AND BATCHNO            =   '" + BatchNo + "'";
            ProductWiseTax = (decimal)db.GetSingleValue(sql);
            return ProductWiseTax;
        }


        public int GrossReturnDelete(string InvoiceID)
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

            string sqlState = " SELECT STATEID FROM M_CUSTOMER WHERE CUSTOMERID = '" + CustomerID + "'";
            string StateID = (string)db.GetSingleValue(sqlState);

            string sqlInsert = " INSERT INTO M_WAYBILL(WAYBILLNO,STATEID,CBU,DTOC)" +
                               " VALUES('" + WaybillNo + "','" + StateID + "',0,GETDATE())";
            int e1 = db.HandleData(sqlInsert);

            string sql = " UPDATE T_SALEINVOICE_HEADER SET WAYBILLNO = '" + WaybillNo + "'" +
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
            string sql = "USP_GET_PACKSIZE '" + Po_ID + "'";
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

        public DataTable BindExportProductDetails(string SaleOrderID, string DepotID, string ProformaInvID, string CustomerID)
        {
            string sql = "EXEC USP_EXPORT_PRODUCT_DETAILS '" + SaleOrderID + "','" + DepotID + "','" + ProformaInvID + "','" + CustomerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        

        

        
    }
}
