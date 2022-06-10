using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;

namespace BAL
{
    public class clsGRN
    {
        DBUtils db = new DBUtils();
        DataTable dtStockDespatchRecord = new DataTable();

        public DataTable LoadCategoryWiseProduct(string TPUID, string CategoryID, string STOCKRECEIVEDID)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                if (STOCKRECEIVEDID == "")
                {
                    sql =   " SELECT	A.ID,A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS NAME, " +
                            " A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,A.CATID " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.CATID='" + CategoryID + "' " +
                            " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' "+
                            " AND A.ACTIVE = 'T'" +
                            " UNION ALL " +
                            " SELECT DISTINCT ID  AS ID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS NAME, " +
                            " 				'ZZZ' AS DIVNAME,999 AS UNITVALUE,CATID " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.CATID='" + CategoryID + "' " +
                            " AND A.TYPE IN ('GIFT','POP') "+
                            " AND A.ACTIVE = 'T' " +
                            " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),NAME ";
                }
                else
                {
                    sql = " SELECT	A.ID,A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS NAME, " +
                            " A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE,A.CATID " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.CATID='" + CategoryID + "' " +
                            " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " UNION ALL " +
                            " SELECT DISTINCT ID  AS ID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS NAME, " +
                            " 				'ZZZ' AS DIVNAME,999 AS UNITVALUE,CATID " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.CATID='" + CategoryID + "' " +
                            " AND A.TYPE IN ('GIFT','POP') " +
                            " ORDER BY DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),NAME ";
                }

                
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
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

        public string NonFGHSNCode(string ProductID)
        {
            string HSNCODE = string.Empty;
            string sql = string.Empty;
            sql = " SELECT ISNULL(B.HSE,'') AS HSNCODE FROM M_PRODUCT A INNER JOIN M_PRIMARY_SUB_ITEM_TYPE B" +
                  " ON A.CATID=B.SUBTYPEID " +
                  " AND A.ID='" + ProductID + "'";
            HSNCODE = (string)db.GetSingleValue(sql);
            return HSNCODE;
        }

        public DataTable LoadProduct(string TPUID, string STOCKRECEIVEDID)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                if (STOCKRECEIVEDID == "")
                {
                    sql = " SELECT A.ID,A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS NAME, " +
                            " CT.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_CATEGORY AS CT " +
                            " ON A.CATID = CT.CATID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' "+
                            " AND A.ACTIVE = 'T' " +
                            " UNION ALL " +
                            " SELECT DISTINCT ID  AS ID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS NAME, " +
                            " 				999 AS SEQUENCENO,'ZZZ' AS DIVNAME,999 AS UNITVALUE " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.TYPE IN ('GIFT','POP') "+
                            " AND A.ACTIVE = 'T' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),NAME ";
                }
                else
                {
                    sql = " SELECT A.ID,A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS NAME, " +
                            " CT.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID INNER JOIN M_CATEGORY AS CT " +
                            " ON A.CATID = CT.CATID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND D.PSID = '1970C78A-D062-4FE9-85C2-3E12490463AF' " +
                            " UNION ALL " +
                            " SELECT DISTINCT ID  AS ID ,ISNULL(PRODUCTALIAS,'')+'~ [PCS]' AS NAME, " +
                            " 				999 AS SEQUENCENO,'ZZZ' AS DIVNAME,999 AS UNITVALUE " +
                            " FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B " +
                            " ON A.ID=B.PRODUCTID " +
                            " WHERE B.VENDORID='" + TPUID + "' " +
                            " AND A.TYPE IN ('GIFT','POP') " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),NAME ";
                }
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable BindDepotBasedOnUser(string UserID)
        {
            string sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH INNER JOIN M_TPU_USER_MAPPING " +
                         " ON M_BRANCH.BRID=M_TPU_USER_MAPPING.TPUID " +
                         " AND M_TPU_USER_MAPPING.USERID='" + UserID + "' " +
                         " AND M_BRANCH.BRANCHTAG='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetPO_QtyCombo(string ProductID, string VendorID, string PacksizeID, string Finyear,string Tag)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_GRN_DETAILS '" + ProductID + "','" + VendorID + "','" + PacksizeID + "','" + Finyear + "','"+Tag+"'";
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
            string sql = " SELECT CASE WHEN A.STATEID = B.STATEID THEN 1 ELSE 0 END SAMESTATE FROM M_TPU_VENDOR A,M_BRANCH B "+
                         " WHERE A.VENDORID='" + FromVendorID + "' AND B.BRID='" + ToDepotID + "'";
            flag = (int)db.GetSingleValue(sql);
            return flag;
        }


        public DataTable BindTPU_Transporter(string VendorID,string STOCKRECEIVEDID)
        {
            string sql = string.Empty;

            if (STOCKRECEIVEDID == "")
            {
                sql =   " SELECT ID,NAME" +
                        " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                             " M_TRANSPOTER_TPU_MAP AS B ON" +
                             " A.ID=B.TRANSPOTERID" +
                        " WHERE A.ISDELETED = 'N'" +
                        " AND  B.TPUID='" + VendorID + "' AND ISAPPROVED='Y'" +
                        " ORDER BY NAME";
            }
            else
            {
                sql =   " SELECT ID,NAME" +
                        " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                             " M_TRANSPOTER_TPU_MAP AS B ON" +
                             " A.ID=B.TRANSPOTERID" +
                        " WHERE A.ISDELETED = 'N'" +
                        " AND  B.TPUID='" + VendorID + "'" +
                        " ORDER BY NAME";
            }
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

        public DataTable FetchSuppliedItem(string VendorID)
        {
            string sql = " SELECT SUPLIEDITEM,SUPLIEDITEMID FROM M_TPU_VENDOR WHERE VENDORID='" + VendorID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPU(string TAG,string STOCKRECEIVEDID)
        {
            string sql = string.Empty;
            if (TAG == "TRUE")
            {
                if (STOCKRECEIVEDID=="")
                {
                    sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEM ='FG' AND ISAPPROVED='Y' ORDER BY VENDORNAME";
                }
                else
                {
                 sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEM ='FG' ORDER BY VENDORNAME";
                }
            }
            else
            {
                if (STOCKRECEIVEDID == "")
                {
                    sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEMID IN ('4','12') AND TAG <> 'F' AND ISAPPROVED='Y' ORDER BY VENDORNAME";
                }
                else
                {
                    sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEMID IN ('4','12') AND TAG <> 'F' ORDER BY VENDORNAME";
                }
            }
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
                         " UNION" +
                         " SELECT VENDORID, VENDORNAME   FROM   M_TPU_VENDOR  WHERE VENDORID IN (SELECT TPUID FROM M_TPU_USER_MAPPING WHERE USERID ='" + UserID + "'  AND TAG='F'  )";
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
                         " ORDER BY A.PONO DESC";
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
                         " UNION" +
                         " SELECT DISTINCT CONVERT(VARCHAR(10),CAST(A.PODATE AS DATE),103) AS PODATE," +
                         " B.PRODUCTID,B.PRODUCTNAME" +
                         " FROM T_Factory_POHEADER A INNER JOIN T_Factory_PODETAILS B" +
                         " ON A.POID=B.POID" +
                         " WHERE B.POID='" + PO_ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSize(string ProductID,string Tag)
        {
            string sql = string.Empty;
            if (Tag == "TRUE")
            {
                sql = " SELECT PSID,PSNAME,RANK FROM  VW_PCS" +
                      " WHERE PRODUCTID = '" + ProductID + "'" +
                      " UNION ALL" +
                      " SELECT PSID,PSNAME,'1' AS RANK FROM Vw_SALEUNIT" +
                      " WHERE PSID ='1970C78A-D062-4FE9-85C2-3E12490463AF'" +
                      " AND PRODUCTID = '" + ProductID + "'" +
                      " ORDER BY RANK ";
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

        public decimal BindTPURate(string pid, string VendorID, string Date)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(    SELECT 1 FROM [M_TPU__PRODUCT_RATESHEET] WHERE PRODUCTID='" + pid + "' " +
                       "                AND VENDORID='" + VendorID + "' " +
                       "                AND CONVERT(DATE,'" + Date + "',103) " +
                       "                BETWEEN FROMDATE AND TODATE ) " +
                       " BEGIN " +
                       " SELECT '1'   END  " +
                       " ELSE    " +
                       " BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));


            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RMCOST+TRANSFERCOST,0) AS RATE FROM M_TPU__PRODUCT_RATESHEET" +
                             " WHERE PRODUCTID = '" + pid + "' AND VENDORID='" + VendorID + "'" +
                             " AND CONVERT(DATE,'" + Date + "',103) BETWEEN FROMDATE AND TODATE";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                price = 0;
            }
            return price;
        }

        /*Add DespatchDt (Rajeev)*/
        public DataTable BindPoDetails(string POID, string productID, string packSizeID, string depotid, string TAG, string DespatchID, string DespatchDt,string VendorID)
        {
            string sql = "EXEC sp_PO_GRN_DETAILS '" + POID + "','" + productID + "','" + packSizeID + "','" + depotid + "','" + TAG + "','" + DespatchID + "','" + DespatchDt + "','" + VendorID + "'";
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

        public DataTable BindTax(string menuID, string flag, string VendorID, string ProductID, string DepotID,string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "SELECT STATEID FROM M_BRANCH WHERE BRID = '" + DepotID + "'";
            StateID = (Int32)db.GetSingleValue(sqlState);


            if (flag == "1") //  Outer State
            {
                sql =    " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "','"+ Date +"') AS PERCENTAGE,0.00 AS TAXVALUE" +
                         " FROM M_TAX AS A " +
                         " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                         " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                         " WHERE A.ACTIVE='True'" +
                         " AND A.RELATEDTO = 3" +
                         " AND B.MENUID = '" + menuID + "' "+
                         " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')";

            }
            else  // Inner State
            {
                sql =    " SELECT DISTINCT A.ID,A.NAME,[dbo].fn_TaxEvalute(A.ID,'" + VendorID + "','" + ProductID + "','" + Convert.ToString(StateID) + "','" + Date + "') AS PERCENTAGE,0.00 AS TAXVALUE" +
                         " FROM M_TAX AS A " +
                         " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                         " INNER JOIN M_TAX_RELATEDTO AS C ON A.RELATEDTO = C.ID" +
                         " WHERE A.ACTIVE='True'" +
                         " AND A.RELATEDTO = 3" +
                         " AND B.MENUID = '" + menuID + "' "+
                         " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')";
            }
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ItemWiseTaxCount(string menuID, string flag, string VendorID, string ProductID, string DepotID,string Date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string sqlState = string.Empty;
            int StateID = 0;
            sqlState = "select STATEID from M_BRANCH WHERE BRID='" + DepotID + "'";
            StateID = (Int32)db.GetSingleValue(sqlState);
            if (flag == "1") //  Outer State
            {
                sql = " SELECT TAXCOUNT,NAME,PERCENTAGE,RELATEDTO " +
                     " FROM ( " +
                     " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME ,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                     " FROM M_TAX AS A" +
                     " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                     " WHERE A.ACTIVE='True'" +
                     " AND A.RELATEDTO IN (1,4,5,7)" +
                     " AND B.MENUID = '" + menuID + "' "+
                     " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','25FB9B16-1B36-458C-A330-8DCE538E9219')" +
                     " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                     " ORDER BY RELATEDTO ";
            }
            else // Inner State
            {
                sql = " SELECT TAXCOUNT ,NAME ,PERCENTAGE,RELATEDTO " +
                     " FROM ( " +
                     " SELECT COUNT(A.RELATEDTO) AS TAXCOUNT,(A.NAME) AS NAME,[dbo].fn_TaxEvalute(B.TAXID,'" + VendorID + "','" + ProductID + "','" + StateID + "','" + Date + "') AS PERCENTAGE,B.TAXID,A.RELATEDTO   " +
                     " FROM M_TAX AS A" +
                     " INNER JOIN M_TAX_MENU_MAPPING AS B ON A.ID = B.TAXID" +
                     " WHERE A.ACTIVE='True'" +
                     " AND A.RELATEDTO IN (1,4,5,7)" +
                     " AND B.MENUID = '" + menuID + "' "+
                     " AND A.APPLICABLETO IN('B97F27F8-87E7-4F03-8BFF-E65FE4A0402E','4D46CA01-CEDA-4DD1-A0A8-61776A03E5C0')" +
                     " GROUP BY A.NAME,A.PERCENTAGE,B.TAXID,A.RELATEDTO ) F6 " +
                     " ORDER BY RELATEDTO ";
            }

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
                         " FROM  M_STORELOCATION AS A INNER JOIN" +
                         "       M_REASON AS B ON A.ID=B.STORELOCATIONID" +
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
        

        public DataSet Weight(string POID, string productID, string PacksizeID, decimal DespatchQty,string TAG)
        {
            DataSet dsWeight = new DataSet();
            string sql = string.Empty;
            sql = "EXEC SP_GRN_WEIGHTCALCULATION '" + POID + "','" + productID + "','" + PacksizeID + "'," + DespatchQty + ",'" + TAG + "'";
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
        public string InsertDespatchDetails( string GRNDate, string tpuid, string tpuname, string wayBillNo, string InvoiceNo, string InvoiceDate,
                                             string TransporterID, string Vehicle, string DepotID, string DepotName,
                                             string LRGRNo, string LRGRDate, string TransportMode, int userID, string finyear,
                                             string Remarks, decimal TotalValue, decimal Othercharges, decimal Adjustment, decimal RoundOff,
                                             string strTermsID, string CFormNo, string CFormDate, string Gatepassno, string gatepassdate, string FormFlag,
                                             string xml, string xmlTax, string xmlGrossTax, string xmlRejection,string hdnvalue, string insCompID, 
                                             string insCompName, string insuranceNumber,string MenuID,string DespatchID,
                                             string xmladddetails,decimal AddnAmt,string InvoiceType,decimal TotAddCost,decimal TotPCS)
        {

            string flag = "";
            string GRNID = string.Empty;
            string GRNNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
                try
                {
                    string sqlprocDespatch = " EXEC [SP_GRN_INSERT_UPDATE] '','','" + flag + "','" + GRNDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "','" + InvoiceNo + "'," +
                                             " '" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "','" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlRejection + "',"+
                                             " '" + xmladddetails + "'," + AddnAmt + ",'" + insCompID + "','" + insCompName + "','" + insuranceNumber + "',"+
                                             " '" + MenuID + "','" + InvoiceType + "'," + TotAddCost + "," + TotPCS + "";
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

                    string sqlprocDespatch = " EXEC [SP_GRN_INSERT_UPDATE] '"+hdnvalue+"','"+DespatchID+"','" + flag + "','" + GRNDate + "','" + tpuid + "','" + tpuname + "','" + wayBillNo + "','" + InvoiceNo + "'," +
                                             " '" + InvoiceDate + "'," +
                                             " '" + TransporterID + "','" + Vehicle + "','" + DepotID + "'," +
                                             " '" + DepotName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                             " '" + TransportMode + "','" + CFormNo + "','" + CFormDate + "','" + Gatepassno + "','" + gatepassdate + "',"+
                                             " '" + FormFlag + "'," + userID + ",'" + finyear + "'," +
                                             " '" + Remarks + "'," + TotalValue + "," + Othercharges + "," + Adjustment + "," + RoundOff + "," +
                                             " '" + strTermsID + "','" + xml + "','" + xmlTax + "','" + xmlGrossTax + "','" + xmlRejection + "',"+
                                             " '" + xmladddetails + "'," + AddnAmt + ",'" + insCompID + "','" + insCompName + "','" + insuranceNumber + "',"+
                                             " '" + MenuID + "','" + InvoiceType + "'," + TotAddCost + "," + TotPCS + "";
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

        public string GetFinancestatus(string STOCKRECEIVEDID)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM [T_STOCKRECEIVED_HEADER] WHERE STOCKRECEIVEDID ='" + STOCKRECEIVEDID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                            " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public DataSet EditReceivedDetails(string ReceivedID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_RECEIVED_DETAILS '" + ReceivedID + "','T'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindReceived(string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID, string TPUFLAG)
        {
            string sql = string.Empty;
            if (CheckerFlag == "FALSE")
            {
                if (TPUFLAG == "TRUE")
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID,A.STOCKRECEIVEDID AS STOCKRECEIVEDID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO) AS STOCKRECEIVEDNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS STOCKRECEIVEDDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'') AS WAYBILLKEY," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,MOTHERDEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " A.TPUNAME,ISNULL(D.TOTALRECEIVEDVALUE,0) AS NETAMOUNT,(U.FNAME + ' '+U.LNAME) AS APPROVAL_PERSON," +
                          " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,(US.FNAME + ' ' + US.LNAME)  AS USERNAME " +
                          " FROM T_STOCKRECEIVED_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                          " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                          " M_TPU_VENDOR AS C WITH (NOLOCK)" +
                          " ON A.TPUID=C.VENDORID INNER JOIN" +
                          " T_STOCKRECEIVED_FOOTER AS D WITH (NOLOCK)" +
                          " ON A.STOCKRECEIVEDID = D.STOCKRECEIVEDID " +
                          " INNER JOIN M_USER AS U WITH (NOLOCK)  ON A.NEXTLEVELID = U.USERID" +
                          " INNER JOIN M_USER AS US WITH (NOLOCK) ON A.CREATEDBY = US.USERID " +
                          " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.GRN='Y'" +
                          " AND C.SUPLIEDITEMID = '1'" +
                          " AND A.MOTHERDEPOTID='" + depotid + "'" +
                          " ORDER BY STOCKRECEIVEDNO DESC";
                }
                else
                {
                    sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID,A.STOCKRECEIVEDID AS STOCKRECEIVEDID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO) AS STOCKRECEIVEDNO," +
                          " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS STOCKRECEIVEDDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'') AS WAYBILLKEY," +
                          " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                          " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,MOTHERDEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                          " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                          " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                          " A.TPUNAME,ISNULL(D.TOTALRECEIVEDVALUE,0) AS NETAMOUNT,(U.FNAME + ' '+U.LNAME) AS APPROVAL_PERSON," +
                          " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG,(US.FNAME + ' ' + US.LNAME)  AS USERNAME " +
                          " FROM T_STOCKRECEIVED_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                          " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                          " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                          " M_TPU_VENDOR AS C WITH (NOLOCK)" +
                          " ON A.TPUID=C.VENDORID INNER JOIN" +
                          " T_STOCKRECEIVED_FOOTER AS D WITH (NOLOCK)" +
                          " ON A.STOCKRECEIVEDID = D.STOCKRECEIVEDID " +
                          " INNER JOIN M_USER AS U WITH (NOLOCK)  ON A.NEXTLEVELID = U.USERID" +
                          " INNER JOIN M_USER AS US WITH (NOLOCK) ON A.CREATEDBY = US.USERID " +
                          " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                          " AND A.FINYEAR ='" + finyear + "'" +
                          " AND A.GRN='Y'" +
                          " AND C.SUPLIEDITEMID IN ('4','12')" +
                          " AND A.MOTHERDEPOTID='" + depotid + "'" +
                          " ORDER BY STOCKRECEIVEDNO DESC";
                }
            }
            else
            {
                if (TPUFLAG == "TRUE")
                {
                    sql =     " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID,A.STOCKRECEIVEDID AS STOCKRECEIVEDID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO) AS STOCKRECEIVEDNO," +
                              " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS STOCKRECEIVEDDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'') AS WAYBILLKEY," +
                              " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                              " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,MOTHERDEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                              " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                              " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                              " A.TPUNAME,ISNULL(D.TOTALRECEIVEDVALUE,0) AS NETAMOUNT,(U.FNAME + ' '+U.LNAME) AS APPROVAL_PERSON," +
                              " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                              " FROM T_STOCKRECEIVED_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                              " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                              " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                              " M_TPU_VENDOR AS C WITH (NOLOCK)" +
                              " ON A.TPUID=C.VENDORID INNER JOIN " +
                              " T_STOCKRECEIVED_FOOTER AS D WITH (NOLOCK)" +
                              " ON A.STOCKRECEIVEDID = D.STOCKRECEIVEDID INNER JOIN M_USER AS U WITH (NOLOCK)" +
                              " ON A.NEXTLEVELID = U.USERID" +
                              " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                              " AND A.FINYEAR ='" + finyear + "'" +
                              " AND A.ISVERIFIED <> 'Y'" +
                              " AND A.NEXTLEVELID = '" + UserID + "'"+
                              " AND A.GRN='Y'" +
                              " AND C.SUPLIEDITEMID = '1'" +
                              " ORDER BY STOCKRECEIVEDNO DESC";
                }
                else
                {
                    sql =     " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID,A.STOCKRECEIVEDID AS STOCKRECEIVEDID, (A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO) AS STOCKRECEIVEDNO," +
                              " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS STOCKRECEIVEDDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'') AS WAYBILLKEY," +
                              " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                              " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,MOTHERDEPOTID,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME," +
                              " A.NEXTLEVELID AS NEXTLEVELID,A.ISVERIFIED," +
                              " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                              " A.TPUNAME,ISNULL(D.TOTALRECEIVEDVALUE,0) AS NETAMOUNT,(U.FNAME + ' '+U.LNAME) AS APPROVAL_PERSON," +
                              " CASE WHEN A.DAYENDTAG='N' THEN 'PENDING' ELSE 'DONE' END AS DAYENDTAG " +
                              " FROM T_STOCKRECEIVED_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                              " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                              " ON A.TRANSPORTERID = B.ID INNER JOIN " +
                              " M_TPU_VENDOR AS C WITH (NOLOCK)" +
                              " ON A.TPUID=C.VENDORID INNER JOIN" +
                              " T_STOCKRECEIVED_FOOTER AS D WITH (NOLOCK)" +
                              " ON A.STOCKRECEIVEDID = D.STOCKRECEIVEDID INNER JOIN M_USER AS U WITH (NOLOCK)" +
                              " ON A.NEXTLEVELID = U.USERID" +
                              " WHERE CONVERT(DATE,STOCKRECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                              " AND A.FINYEAR ='" + finyear + "'" +
                              " AND A.ISVERIFIED <> 'Y'" +
                              " AND A.NEXTLEVELID = '" + UserID + "'"+
                              " AND A.GRN='Y'" +
                              " AND C.SUPLIEDITEMID = ('4','12')" +
                              " ORDER BY STOCKRECEIVEDNO DESC";
                }

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

        public void ResetDataTables()
        {
            dtStockDespatchRecord.Clear();
        }

        public DataTable BindDespatch(string fromdate, string todate, string finyear, string TPUID)
        {

            string sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                         " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY," +
                         " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                         " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                         " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                         " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN " +
                         " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                         " ON A.TRANSPORTERID = B.ID" +
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
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.WAYBILLKEY = '0'" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "2")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE (A.WAYBILLKEY <> '0' OR A.WAYBILLKEY <> '')" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "3")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
                      " WHERE A.CFORMNO <> ''" +
                      " AND A.FINYEAR ='" + finyear + "'" +
                      " AND A.TPUID = '" + TPUID + "'" +
                      " ORDER BY CREATEDDATE DESC";
            }
            else if (filterText == "4")
            {
                sql = " SELECT A.STOCKDESPATCHID AS STOCKDESPATCHID, (A.STOCKDESPATCHPREFIX + '/' + A.STOCKDESPATCHNO + '/' + A.STOCKDESPATCHSUFFIX) AS STOCKDESPATCHNO," +
                      " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS DESPATCHDATE,ISNULL(A.WAYBILLKEY,'0') AS WAYBILLKEY,ISNULL(A.WAYBILLNO,'') AS WAYBILLNO," +
                      " CONVERT(VARCHAR(10),CAST(A.CFORMDATE AS DATE),103) AS CFORMDATE,ISNULL(A.CFORMNO,'') AS CFORMNO,A.FORMFLAG," +
                      " ISNULL(A.INVOICENO,'') AS INVOICENO,ISNULL(A.VEHICHLENO,'') AS VEHICHLENO,ISNULL(A.LRGRNO,'') AS LRGRNO,ISNULL(A.MODEOFTRANSPORT,'') AS MODEOFTRANSPORT," +
                      " ISNULL(A.MOTHERDEPOTNAME,'') AS MOTHERDEPOTNAME,ISNULL(A.FINYEAR,'') AS FINYEAR,B.ID AS TRANSPORTERID,ISNULL(B.NAME,'') AS TRANSPORTERNAME" +
                      " FROM T_STOCKDESPATCH_HEADER AS A WITH (NOLOCK) INNER JOIN" +
                      " M_TPU_TRANSPORTER AS B WITH (NOLOCK)" +
                      " ON A.TRANSPORTERID = B.ID" +
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

        public int UpdateWaybillNo(string DespatchID, string WaybillNo, string WaybillKey)
        {
            int updateflag = 0;
            string sqlCheck = string.Empty;
            int exists = 0;
            int e = 0;

            sqlCheck = " IF  EXISTS(SELECT * FROM [M_WAYBILL] WHERE WAYBILLNO='" + WaybillKey + "') BEGIN " +
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

            RETURNAMOUNT = (Rate / UnitValue) * RETURNVALUE;

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

        public decimal BindDepotRate(string Productid, decimal MRP, string Date)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS (   SELECT 1 FROM M_DEPOT_TRANSFER_RATESHEET where PRODUCTID='" + Productid + "'" +
                       "                AND CONVERT(DATE,'" + Date + "',103) BETWEEN FROMDATE AND TODATE )" +
                       " BEGIN " +
                       "    SELECT '1' " +
                       " END " +
                       " ELSE " +
                       " BEGIN	" +
                       "    SELECT '0'  " +
                       " END ";
            exists = ((string)db.GetSingleValue(sqlCheck));


            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_DEPOT_TRANSFER_RATESHEET" +
                             " WHERE PRODUCTID = '" + Productid + "'" +
                             " AND CONVERT(DATE,'" + Date + "',103) BETWEEN FROMDATE AND TODATE";

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

        #region BindLedger
        public DataTable BindLedger()
        {
            string sql = "SELECT ID,NAME FROM ACC_ACCOUNTSINFO ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region BindMainGrid
        public DataTable BindMainGrid()
        {
            string sql = "SELECT DETAILSID,DETAILSNAME,PERCENTAGE,REFERENCELEDGERID,B.NAME AS LEDGERNAME FROM M_ADDITIONALDETAILS_MAP A INNER JOIN Acc_AccountsInfo B ON A.REFERENCELEDGERID = B.Id ORDER BY DETAILSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        } 
        #endregion

        #region BindGridEdit
        public DataTable BindGridEdit(string ID)
        {
            string sql = "SELECT DETAILSID,DETAILSNAME,PERCENTAGE,REFERENCELEDGERID FROM M_ADDITIONALDETAILS_MAP A INNER JOIN Acc_AccountsInfo B ON A.REFERENCELEDGERID = B.Id WHERE DETAILSID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        } 
        #endregion

        #region Save
        public int Save(string ID, string Name, string Mode, decimal Percentage, string LedgerID,string CBU,string LMBU,string DTOC,string DTOM)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_ADDITIONALDETAILS_MAP WHERE DETAILSNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }

                    }
                }
                else
                {
                    if (Name != "")
                    {
                        strcheck = "SELECT TOP 1 * FROM M_ADDITIONALDETAILS_MAP WHERE DETAILSNAME='" + Name + "' AND  DETAILSID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = " INSERT INTO M_ADDITIONALDETAILS_MAP(DETAILSID,DETAILSNAME,PERCENTAGE,REFERENCELEDGERID,CBU,LMBU,DTOC,DTOM)" +
                                 " VALUES(NEWID(),'" + Name + "',  '" + Percentage + "','" + LedgerID + "','" + CBU + "',  '" + LMBU + "',GETDATE(),'')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "UPDATE M_ADDITIONALDETAILS_MAP SET DETAILSNAME='" + Name + "',PERCENTAGE='" + Percentage + "',REFERENCELEDGERID='" + LedgerID + "',LMBU='" + LMBU + "',DTOM=GETDATE() WHERE DETAILSID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    //result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        } 
        #endregion

        #region Delete
        public int Delete(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "DELETE FROM M_ADDITIONALDETAILS_MAP where DETAILSID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        } 
        #endregion
    }
}
