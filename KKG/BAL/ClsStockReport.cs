using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsStockReport
    {
        DBUtils db = new DBUtils();

        public DataTable BindDepot_Primary()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindOnly_Depot()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID NOT IN('14857CFC-2450-4D52-B93A-486D9507A1BE','FFC65354-AB46-4983-A67F-111486EC3D39') ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOnly_Factory()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID IN('14857CFC-2450-4D52-B93A-486D9507A1BE','FFC65354-AB46-4983-A67F-111486EC3D39') ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_Primary1()
        {
            string sql = " SELECT '' as COMMISIONID,BRID,BRPREFIX AS BRNAME, " +
                         " '' as GTMTCSDCPC0TO5,'' as EXPORT0TO5, " +
                         " '' as CORPORATESALES0TO5,'' as STOCKTRANSFER0TO5, " +
                         " '' as TRADINGSALES0TO5,'' as OTHERS0TO5,'' as GTMTCSDCPC5TO10, " +
                         " '' as EXPORT5TO10,'' as CORPORATESALES5TO10,'' as STOCKTRANSFER5TO10, " +
                         " '' as TRADINGSALES5TO10,'' as OTHERS5TO10,'' as [GTMTCSDCPC10TO12.5]," +
                         " '' as [EXPORT10TO12.5], " +
                         " '' as [CORPORATESALES10TO12.5],'' as [STOCKTRANSFER10TO12.5],'' as [TRADINGSALES10TO12.5]," +
                         " '' as [OTHERS10TO12.5], " +
                         " '' as [GTMTCSDCPC12.5TOABOBE],'' as [EXPORT12.5TOABOBE],'' as [CORPORATESALES12.5TOABOBE]," +
                         " '' as [STOCKTRANSFER12.5TOABOBE], " +
                         " '' as [TRADINGSALES12.5TOABOBE],'' as [OTHERS12.5TOABOBE] " +
                         " FROM M_BRANCH " +
                         " WHERE BRANCHTAG = 'D' and " +
                         " brid not in('14857CFC-2450-4D52-B93A-486D9507A1BE', 'FFC65354-AB46-4983-A67F-111486EC3D39') " +
                         " ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindDepotIncludeExport()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH  where  BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributor(String CUSTOMERID)
        {
            if (CUSTOMERID == "8")
            {
                string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID='5E24E686-C9F4-4477-B84A-E4639D025135'  ORDER BY CUSTOMERNAME";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            else
            {
                string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + CUSTOMERID + "' ORDER BY CUSTOMERNAME";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
        }

        public DataTable Region(string UserType, string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'O'  UNION " +
                       "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX ASBRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE'  ORDER BY SEQUENCE,BRNAME";
                }
                else
                {
                    sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "'  ORDER BY BRNAME";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable Region_foraccounts(string UserType, string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    //sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                    //   " WHERE  BRANCHTAG = 'O' AND ISSHOWACCOUNTS='Y' UNION " +
                    //   "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                    //   " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' AND ISSHOWACCOUNTS='Y' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX ASBRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                    //   " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
                    sql = "SELECT BRID, BRPREFIX AS BRNAME FROM M_BRANCH ORDER BY BRNAME";
                }
                else
                {
                    sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "' AND ISSHOWACCOUNTS='Y'  ORDER BY BRNAME";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindDepot()
        {
            //string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE  BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID, BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'O' AND ISSHOWACCOUNTS='Y' UNION " +
               "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindDepot(string BRID)
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH  WHERE BRID='" + BRID + "' AND BRANCHTAG='D'  ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string BindBranchName(string BRID)
        {
            string sql = "SELECT BRNAME FROM M_BRANCH  WHERE BRID='" + BRID + "'  ";
            string name = "";
            name = (string)db.GetSingleValue(sql);
            return name;
        }

        public DataTable BindPackSize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreLocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION WITH(NOLOCK) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        

        public DataTable BindInvoiceNo()
        {
            string sql = "SELECT SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER_DMS with(nolock) ORDER BY SALEINVOICENO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindGrid(string DistID, string PackSizeID, string StoreID)
        {
            string sql = "EXEC [USP_RPT_DEPOT_STOCK_DMS] '" + DistID + "','" + PackSizeID + "','" + StoreID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGridForDepot(string DistID, string PackSizeID, string StoreID)
        {
            string sql = "EXEC [USP_RPT_DEPOT_STOCK] '" + DistID + "','" + PackSizeID + "','" + StoreID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStkExpiryGrid(string DistID, string PackSizeID, string StoreID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_DEPOT_EXPIRY_STOCK_ERP] '" + DistID + "','" + PackSizeID + "','" + StoreID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable Bindloanledger()
        {
            string sql = "EXEC [USP_LOAD_LOAN_LEDGER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindInterastgrid(string LEDGERID, string FINYEAR, string REGIONID, string DATE, decimal PERCENTAGE)
        {
            string sql = "EXEC [USP_RPT_INTEREST_CALCULATION_FOR_UNSECURED_LOAN] '" + LEDGERID + "','" + FINYEAR + "','" + REGIONID + "','" + DATE + "' ,'" + PERCENTAGE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessegment(string BSID)
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + BSID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME DESC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer(string ID)
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + ID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroup(string ID)
        {
            string sql = "SELECT DISTINCT GROUPID,GROUPNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID='" + ID + "' ORDER BY GROUPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindType()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE DISTRIBUTIONCHANNEL='Y' " +
                         "UNION ALL " +
                         "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN('9BF42AA9-0734-4A6A-B835-0885FBCF26F5','DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB') ORDER BY UTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTypeDetails(string ID)
        {
            string sql = "SELECT CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END USERID,USERNAME FROM M_USER WHERE USERTYPE='" + ID + "' ORDER BY USERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION " +//Add by Rajeev (24-03-2018)
                         " UNION ALL" +
                         " SELECT CONVERT (VARCHAR(5), ID)AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ID NOT IN(1,9,10)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBrand_type(string typeid)
        {
            string sql = "";
            if (typeid == "0")
            {
                 sql = " SELECT DIVID,DIVNAME FROM M_DIVISION " +
                       " UNION ALL" +
                       " SELECT CONVERT (VARCHAR(5), ID)AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ID NOT IN(1,9,10)";
            }
            if (typeid == "FG")
            {
                sql = " SELECT DIVID,DIVNAME FROM M_DIVISION ";
            }
            if (typeid == "NON-FG")
            {
               sql = " SELECT CONVERT (VARCHAR(5), ID)AS DIVID,ITEMDESC AS DIVNAME FROM M_SUPLIEDITEM WHERE ID NOT IN(1,9,10)";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindBrand(string typeid)
        {
            string sql = "SELECT DISTINCT DIVID,DIVNAME FROM M_PRODUCT  WHERE TYPEID='" + typeid + "' ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindItemtype()
        {
            string sql = "SELECT DISTINCT TYPEID,TYPENAME FROM M_PRODUCT ORDER BY TYPENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            //string sql = "SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY UNION ALL SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE ";
            /*string sql = " SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY  WHERE CATCODE NOT IN('201','190','20','230','210','60')" +//Add by Rajeev (24-03-2018)
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2')";*/
            string sql =    " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY A INNER JOIN  M_PRODUCT B " +
                            " ON A.CATID = B.CATID " +
                            " UNION ALL  " +
                            " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                            " FROM M_PRIMARY_SUB_ITEM_TYPE ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllCategory()
        {            
            string sql = " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY AS A " +
                         " INNER JOIN  M_PRODUCT AS B ON A.CATID = B.CATID " +
                         " UNION ALL SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE  DIVID in ('" + brandid + "') ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNature()
        {
            string sql = "SELECT DISTINCT NATUREID,NATURENAME FROM M_PRODUCT ORDER BY NATURENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFragrance()
        {
            string sql = "SELECT DISTINCT FRGID,FRGNAME FROM M_PRODUCT ORDER BY FRGNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFragrance(string catid)
        {
            string sql = "SELECT DISTINCT FRGID,FRGNAME FROM M_PRODUCT WHERE CATID='" + catid + "' ORDER BY FRGNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPacksize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct()
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS  NAME FROM M_PRODUCT ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductByFiltration(string itemtypeid, string brndid, string catid, string fragranceid)
        {
            string Sql = string.Empty;
            string STRSQL = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE ACTIVE='T'";
            string STRORDERBY = "ORDER BY NAME";
            DataTable dt = new DataTable();

            string TYPEID = "";

            if (itemtypeid != "")
            {

                TYPEID = " AND TYPEID='" + itemtypeid + "'";

            }

            string DIVID = "";

            if (brndid != "")
            {

                DIVID = " AND DIVID='" + brndid + "'";

            }

            string CATID = "";

            if (catid != "")
            {

                CATID = " AND CATID='" + catid + "'";

            }

            string FRGID = "";

            if (fragranceid != "")
            {

                FRGID = " AND FRGID='" + fragranceid + "'";

            }

            Sql = STRSQL + TYPEID + DIVID + CATID + FRGID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;
        }

        public DataTable BindSaleDetailsgrid(string packid, string Fromdate, string Todate, string bsid,
            string grpid, string typeid, string typedetails, string itemtype, string brandid, string catid, string frgid, string natureid, string productid, string chkdetails)
        {
            DataTable dtsalesdetails = new DataTable();
            DataTable pcksize = new DataTable();
            string Sql = string.Empty;
            string strpacksize_pcs = string.Empty;
            string packsize_pcs = string.Empty;
            strpacksize_pcs = "SELECT PACKSIZE FROM P_APPMASTER";
            pcksize = db.GetData(strpacksize_pcs);
            packsize_pcs = pcksize.Rows[0]["PACKSIZE"].ToString();

            string beatcode = "";
            string beatname = "";

            beatcode = "(SELECT M_BEAT.CODE FROM M_BEAT INNER JOIN M_BEAT_RETAILER_MAPPING ON M_BEAT.BEAT_ID=M_BEAT_RETAILER_MAPPING.BEAT_ID WHERE CUSTOMERID LIKE   '%' + DISTRIBUTORID + '%' )";
            beatname = "(SELECT M_BEAT.NAME FROM M_BEAT INNER JOIN M_BEAT_RETAILER_MAPPING ON M_BEAT.BEAT_ID=M_BEAT_RETAILER_MAPPING.BEAT_ID WHERE CUSTOMERID LIKE   '%' + DISTRIBUTORID + '%' )";



            string Fieldstr = "";


            Fieldstr = "SELECT SFID AS USRID , UCODE AS USRCODE, SFNAME AS USRNAME, PRODUCTID, PCODE, PRODUCTNAME,'BITCODE' AS BEATCODE,'BITNAME' AS BEATNAME," +
                                 "SUM((CAST(CONVERT(VARCHAR,CONVERT(INT,QTY)) + '.' + dbo.RPAD('0', 3,CONVERT(VARCHAR,CONVERT(INT,QTYPCS))) AS DECIMAL(18,2)))) AS CASES, " +
                                 "CAST(SUM(ISNULL(((ISNULL(QTYPCS,0)+ ISNULL(dbo.GetPackingSize_OnCall(PRODUCTID,PACKINGSIZEID,'" + packsize_pcs + "',QTY),0))),0)) AS INT) AS QTY ," +
                                 "CAST(SUM(RATE * ISNULL(((ISNULL(QTYPCS,0)+ ISNULL(dbo.GetPackingSize_OnCall(PRODUCTID,PACKINGSIZEID,'" + packsize_pcs + "',QTY),0))),0)) AS DECIMAL(18,2)) AS VALUE  FROM Vw_SaleDetailsReportForDMS";

            string GROUPBY = " GROUP BY SFID,UCODE,SFNAME,PRODUCTID,PCODE,PRODUCTNAME";

            string DateRange = "";

            DateRange = " WHERE SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + Fromdate + "',103) AND CONVERT(DATETIME,'" + Todate + "',103) ";

            string STRBSID = "";

            if (bsid != "")
            {

                STRBSID = " AND BSID='" + bsid + "'";

            }

            string STRGRPID = "";

            if (grpid != "")
            {

                STRGRPID = " AND GROUPID='" + grpid + "'";

            }


            string STRTYPEID = "";

            if (typeid != "")
            {



                switch (typeid)
                {


                    // DISTRIBUTOR
                    case "5E24E686-C9F4-4477-B84A-E4639D025135":

                        Fieldstr = Fieldstr.Replace("SFID", "DEPOTID");
                        Fieldstr = Fieldstr.Replace("SFNAME", "DEPOTNAME");
                        GROUPBY = GROUPBY.Replace("SFID", "DEPOTID");
                        GROUPBY = GROUPBY.Replace("SFNAME", "DEPOTNAME");
                        STRTYPEID = " AND USERTYPE='" + typeid + "'";

                        break;



                    // RETAILER
                    case "B9343E49-D86B-49EA-9ACA-4F4F7315EC96":


                        Fieldstr = Fieldstr.Replace("SFID", "DISTRIBUTORID");
                        Fieldstr = Fieldstr.Replace("SFNAME", "DISTRIBUTORNAME");

                        Fieldstr = Fieldstr.Replace("'BITCODE'", " " + beatcode + " ");
                        Fieldstr = Fieldstr.Replace("'BITNAME'", " " + beatname + " ");

                        GROUPBY = GROUPBY.Replace("SFID", "DISTRIBUTORID");
                        GROUPBY = GROUPBY.Replace("SFNAME", "DISTRIBUTORNAME");
                        STRTYPEID = " AND USERTYPE='" + typeid + "'";

                        break;


                    // SALESFORCE
                    default:

                        STRTYPEID = " AND USERTYPE='" + typeid + "'";

                        break;


                }



            }


            string STRTYPEDETAILSID = "";

            if (typedetails != "")
            {

                //
                //

                switch (typeid)
                {


                    // DISTRIBUTOR
                    case "5E24E686-C9F4-4477-B84A-E4639D025135":

                        STRTYPEDETAILSID = " AND DEPOTID='" + typedetails + "'";

                        break;



                    // RETAILER
                    case "B9343E49-D86B-49EA-9ACA-4F4F7315EC96":

                        STRTYPEDETAILSID = " AND DISTRIBUTORID='" + typedetails + "'";

                        break;


                    // SALESFORCE
                    default:

                        STRTYPEDETAILSID = " AND SFID='" + typedetails + "'";

                        break;


                }

            }

            string STRITEMTYPE = "";

            if (itemtype != "")
            {
                STRITEMTYPE = " AND TYPEID='" + itemtype + "'";

            }

            string BRANDID = "";

            if (brandid != "")
            {
                BRANDID = " AND DIVID='" + brandid + "'";

            }
            string CATID = "";

            if (catid != "")
            {
                CATID = " AND CATID='" + catid + "'";

            }

            string NATUREID = "";

            if (natureid != "")
            {
                NATUREID = " AND NATUREID='" + natureid + "'";

            }

            string FRGID = "";

            if (frgid != "")
            {
                FRGID = " AND FRGID='" + frgid + "'";

            }

            string PRODUCTID = "";

            if (productid != "")
            {
                PRODUCTID = " AND PRODUCTID='" + productid + "'";

            }

            string PACKINGSIZEID = "";

            if (packid != "")
            {
                if (packid != packsize_pcs)
                {
                    PACKINGSIZEID = " AND PACKINGSIZEID='" + packid + "'";
                }

            }



            if (chkdetails != "")
            {
                Fieldstr = Fieldstr.Replace("PRODUCTNAME", "PRODUCTNAME,BSNAME,GROUPNAME,TYPENAME,DIVNAME,CATNAME,NATURENAME,FRGNAME");
                GROUPBY = GROUPBY.Replace("PRODUCTNAME", "PRODUCTNAME,BSNAME,GROUPNAME,TYPENAME,DIVNAME,CATNAME,NATURENAME,FRGNAME");

            }





            Sql = Fieldstr + DateRange + STRBSID + STRGRPID + STRTYPEID + STRTYPEDETAILSID + STRITEMTYPE + BRANDID + CATID + NATUREID + FRGID + PRODUCTID + PACKINGSIZEID;


            dtsalesdetails = db.GetData(Sql + GROUPBY);
            return dtsalesdetails;
        }

        public DataTable BindInvoiceNoByDateRange(string frmdate, string todate)
        {
            string sql = "SELECT  (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER_DMS " +
                         "WHERE SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindInvoiceNoByDateRangeERP(string frmdate, string todate, string depotid, string bsid, string isvarified)
        {
            string sql = "";
            if (depotid != "0")
            {
                sql = "SELECT  (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                            "WHERE DEPOTID='" + depotid + "'  AND ISVERIFIED='" + isvarified + "' AND BSID='" + bsid + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";

            }
            else
            {
                sql = "SELECT  (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                          "WHERE BSID='" + bsid + "' AND ISVERIFIED='" + isvarified + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";


            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataSet InvoiceDetailsReport(string FromDate, string ToDate, string DistID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC sp_RPT_SALE_INVOICE_DETAILS_ERP '" + FromDate + "','" + ToDate + "','" + DistID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindSaleDetailS(string FromDate, string ToDate, string DistID)
        {
            string sql = "EXEC SP_SALEINVOICE_REPORT_DETAILS_ERP '" + FromDate + "','" + ToDate + "','" + DistID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet PurchaseReport(string FromDate, string ToDate, string DistID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC sp_RPT_PURCHASE_INVOICE_DETAILS_DMS '" + FromDate + "','" + ToDate + "','" + DistID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindPurchaseDetailS(string FromDate, string ToDate, string DistID)
        {
            string sql = "EXEC SP_RPT_PURCHASE_REPORT_DETAILS_DMS '" + FromDate + "','" + ToDate + "','" + DistID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet TransitDetailsReport(string FromDate, string ToDate, string DistID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC sp_RPT_TRANSIT_INVOICE_DETAILS '" + FromDate + "','" + ToDate + "','" + DistID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable DepotTransitDetailsReport(string FromDate, string ToDate, string DepotID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC SP_RPT_TRANSIT_DEPOT_DETAILS '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet PurchaseReportERP(string FromDate, string ToDate, string DepotID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC sp_RPT_PURCHASE_INVOICE_DETAILS_ERP '" + FromDate + "','" + ToDate + "','" + DepotID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindprogressReportDetails(string fromdate, string todate, string brandid, string catid, string depot, string StoreLocation)
        {
            string sql = "EXEC [USP_RPT_STOCK_PROGRESS] '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "','" + StoreLocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategorybybrand(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE  DIVID  IN('" + brandid + "') ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockReceipt(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_RECEIPT_REGISTER '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockReceipt_Details(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_RECEIPT_REGISTER_DETAILS '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGroupbyid(string BSID)
        {
            string sql = " SELECT DIS_CATID,DIS_CATNAME,BUSINESSSEGMENTID FROM M_DISTRIBUTER_CATEGORY " +
                         " WHERE BUSINESSSEGMENTID in ('" + BSID + "')" +
                         " ORDER BY DIS_CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindState()
        {
            string sql = "SELECT STATE_ID, STATE_NAME FROM M_REGION  WHERE ZONE<>'F' ORDER BY STATE_NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbybrand(string divid, string catid)
        {
            string sql = "SELECT DISTINCT ID, PRODUCTALIAS NAME FROM M_PRODUCT with(nolock) WHERE DIVID IN('" + divid + "') AND CATID IN('" + catid + "') ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindType(string BSID, string GRPID)
        {
            string sql = " SELECT DISTINCT CHANNELID,CHANNELNAME FROM VW_MARGIN WHERE BSID='" + BSID + "' AND GROUPID='" + GRPID + "' ORDER BY CHANNELNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPriceGrid(string stateid, string depotid, string bsid, string grpid, string product, string tag, string module, string type)
        {
            string sql = "EXEC USP_RPT_BASE_COST_PRICE_LIST '" + stateid + "','" + depotid + "','" + bsid + "','" + grpid + "','" + product + "','" + tag + "','" + module + "','" + type + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindDeptName(string BRID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID='" + BRID + "' order by BRNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockTransfer(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_TRANSFER_REGISTER '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockTransferDetails(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_STOCK_TRANSFER_REGISTER_DETAILS '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSummaryDetails(string fromdate, string todate, string depotid)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_REGISTER '" + fromdate + "','" + todate + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindVatStatutory(string fromdate, string todate, string depotid, string bsid, string groupid)
        {
            string sql = "EXEC USP_RPT_VAT_STATUTORY_REPORT '" + fromdate + "','" + todate + "','" + depotid + "','" + bsid + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindStockinHandDetails(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string storelocation)
        {
            string sql = "EXEC USP_RPT_DEPOTWISE_STOCK_IN_HAND '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + storelocation + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /*Add by Rajeev (26-03-2018)*/
        public DataSet BindStockinHand_AccountWise(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string storelocation)
        {
            string sql = "EXEC USP_RPT_ACCOUNTWISE_STOCK_IN_HAND '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + storelocation + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable GetBatch(string PRODUCTID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT DISTINCT BATCHNO,PRODUCTID FROM T_TPU_PRODUCTION_DETAILS WHERE PRODUCTID='" + PRODUCTID + "'";

            dt = db.GetData(sql);
            return dt;

        }


        public DataSet BindDespatchcfrom(string fromdate, string todate, string depotid, string status)
        {
            string sql = "EXEC USP_RPT_DESPATCH_C_FORM '" + fromdate + "','" + todate + "','" + depotid + "','" + status + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindStockReceiptFfrom(string fromdate, string todate, string depotid, string STATUS)
        {
            string sql = "EXEC [USP_RPT_STOCK_RECEIPT_F_FORM] '" + fromdate + "','" + todate + "','" + depotid + "','" + STATUS + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindProductbyBSegment(string divid, string catid, string BSID)
        {
            string STRSQL = string.Empty;
            string STRSQL_CHECK = string.Empty;
            string Sql = string.Empty;
            string str = string.Empty;
            DataTable dt = new DataTable();
            //if (BSID != "0")
            //{
            //    sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID" +
            //                " WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "')" +
            //                " AND BUSNESSSEGMENTID='" + BSID + "'";
            //}
            //else
            //{
            //    sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID" +
            //              " WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "')";
            //}

            //DataTable dt = new DataTable();
            //dt = db.GetData(sql);
            //return dt;
            string BS_SEGMENT = "";
            if (BSID == "0")
            {
                STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT ";
            }
            else
            {

                STRSQL_CHECK = "SELECT TOP 1 NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "') ";

                str = (string)db.GetSingleValue(STRSQL_CHECK);

                if (str == "" || str == null)
                {
                    STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE 1=1  ";
                }
                else
                {

                    STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE 1=1 ";


                    if (BSID != "0")
                    {

                        BS_SEGMENT = " AND BUSNESSSEGMENTID IN ('" + BSID + "')";

                    }
                }
            }
            string STRORDERBY = "ORDER BY PRODUCTALIAS";




            string DIVID = "";
            if (divid != "0")
            {
                if (BSID != "0")
                {

                    DIVID = " AND DIVID IN('" + divid + "')";
                }


            }

            string CATID = "";
            if (catid != "0")
            {
                if (BSID != "0")
                {

                    CATID = " AND M_PRODUCT.CATID IN('" + catid + "')";
                }



            }

            Sql = STRSQL + BS_SEGMENT + DIVID + CATID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;

        }

        public DataSet BindSalesTaxSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_TAX_SUMMARY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindVoucherTypes()
        {
            string sql = " SELECT Id,VoucherName FROM Acc_VoucherTypes  ORDER BY VoucherName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSizeInPack()
        {
            string sql = " SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID IN('1970C78A-D062-4FE9-85C2-3E12490463AF','B9F29D12-DE94-40F1-A668-C79BF1BF4425')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindClosingStock(string month, string year, string depot)
        {
            string sql = "EXEC [USP_RPT_CLOSING_STOCK_TAKE] '" + month + "','" + year + "','" + depot + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerforAdmin()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID='5E24E686-C9F4-4477-B84A-E4639D025135' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerClosingStock(string customerid)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + customerid + "'" +
                         " UNION ALL" +
                         " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + customerid + "' ORDER BY CUSTOMERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVoucherNo(string VoucherTypeID, string BranchID, string fromdate, string todate)
        {
            //string sql = "SELECT AccEntryID,VoucherNo FROM Acc_Entry WHERE VoucherTypeID='" + VoucherTypeID + "' AND BranchID='" + BranchID + "' AND Date between CONVERT(date,'" + fromdate + "',103) and CONVERT(date,'" + todate + "',103)";
            string sql = " SELECT A.AccEntryID,A.VoucherNo +' ( ' +'Dr - ' + B.name + ' / Cr - ' +C.name +' )' AS VoucherNo " +
                            " FROM Acc_Entry A LEFT JOIN Acc_AccountsInfo B " +
                            " ON A.DebitAccId = B.Id  LEFT JOIN Acc_AccountsInfo C " +
                            " ON A.CreditAccId = C.Id " +
                            " WHERE VoucherTypeID = '" + VoucherTypeID + "' " +
                            " AND A.BranchID = '" + BranchID + "' " +
                            " AND A.Date between CONVERT(date, '" + fromdate + "', 103) and CONVERT(date,'" + todate + "',103)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotOffice()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE  BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLedger(String SessionID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            if (SessionID == "8")
            {
                sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' ORDER BY LedgerName";
            }
            else
            {
                sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' ORDER BY LedgerName";
            }

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindForLedgerReport(String SessionID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            if (SessionID == "8")
            {
                sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                      " LEFT JOIN Acc_AccountGroup B ON A.actGrpCode=B.CODE WHERE name <> '' ORDER BY LedgerName";
            }
            else
            {
                sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                      " LEFT JOIN Acc_AccountGroup B ON A.actGrpCode=B.CODE WHERE name <> '' ORDER BY LedgerName";
            }

            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindLedgerReportGrid(string fromdate, string todate, string legderid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS] '" + fromdate + "','" + todate + "','" + legderid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCountry()
        {
            string sql = "SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY WHERE COUNTRYID <> '8F35D2B4-417C-406A-AD7F-7CB34F4D0B35' ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInvoice(string countryid, string fromdate, string todate)
        {
            string sql = " SELECT SALEORDERID,SALEORDERNO,COUNTRYNAME,SALEORDERDATE FROM T_SALEORDER_HEADER A INNER JOIN M_CUSTOMER B" +
                         " ON A.CUSTOMERID = B.CUSTOMERID WHERE COUNTRYID='" + countryid + "'" +
                         " AND SALEORDERDATE BETWEEN CONVERT(DATE,'" + fromdate + "',103) AND CONVERT(DATE,'" + todate + "',103) ORDER BY SALEORDERNO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindExportOrder(string SaleOrderID)
        {
            string sql = "EXEC [USP_RPT_EXPORT_ORDER_INVOICE_RRPORT] '" + SaleOrderID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable VoucherType()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select Id ,VoucherName from Acc_VoucherTypes Order by VoucherName";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable Region()
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql = "Select [BRID],[BRNAME] from [M_BRANCH] order by BRNAME";
                string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O'  UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE'  ORDER BY SEQUENCE,BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindAccVoucher(string fromdate, string todate, string VoucherTypeID, string Mode, string region)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART1] '" + fromdate + "','" + todate + "','" + VoucherTypeID + "','" + Mode + "', '" + region + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAccVoucher_Ledger(string AccEntryId)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART2] '" + AccEntryId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindAccVoucher_Ledger_Voucher(string LedgerID, string AccEntryID)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART3] '" + LedgerID + "','" + AccEntryID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindCategory_Type()
        {
            string sql = "SELECT ADV_CATEGORYID,ADV_CATEGORYNAME FROM M_ADV_CATEGORY ORDER BY ADV_CATEGORYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAdvertisementDetails(string fromdate, string todate, string CategoryID)
        {
            string sql = "EXEC [USP_RPT_ADVERISMENT_DETAILS] '" + CategoryID + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyDepot(string DepotID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCollectionGrid(string DistributorID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_SALE_INVOICE_OUTSTANDING_TOTAL] '" + DistributorID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerbyId(string ID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID = '" + ID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDailyReportGrid(string date)
        {
            string sql = "EXEC [USP_RPT_GET_DAILY_DMS_INVOICE_COUNT] '" + date + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDailyReportGrid_Depot()
        {
            string sql = "EXEC [USP_RPT_GET_DAILY_DEPOT_INVOICE_COUNT]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDailyReportGrid_TPU()
        {
            string sql = "EXEC [USP_RPT_GET_DAILY_FACTORY_TPU_TRANSACTION_COUNT]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStatewiseSale(string fromdate, string todate, string BSID, int amt,string target)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_STATEWISE] '" + fromdate + "','" + todate + "','" + BSID + "'," + amt + ",'"+ target +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPartywiseSale(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotwiseSale(string fromdate, string todate, string stateid, string brand, string categoryid, string packsizeid, string bsid, int amt, string unitvalue,string target)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_DEPOTWISE] '" + fromdate + "','" + todate + "','" + stateid + "','" + brand + "','" + categoryid + "','" + packsizeid + "','" + bsid + "'," + amt + ",'" + unitvalue + "','"+ target +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindProductWiseSale(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string ProductType)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + ProductType + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductWiseSale_motherreport(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string ProductType,string rpttype)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + ProductType + "','"+ rpttype +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCitybyHO(string DepotID)
        {
            string sql = " SELECT DISTINCT A.CITYID AS CITYID,City_Name AS CITYNAME FROM M_HEADQUATER A INNER JOIN M_CITY B ON A.CITYID=B.City_ID " +
                         " INNER JOIN M_BRANCH C ON A.HOID=C.CITYID " +
                         " WHERE BRID='" + DepotID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCity()
        {
            string sql = " SELECT DISTINCT City_ID,City_Name FROM M_CITY ORDER BY City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAllCustomers()
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID <> (SELECT RETAILERID FROM P_APPMASTER) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotSales(string Month, string Year, string DepotID, string DistributorID)
        {
            string sql = "EXEC USP_RPT_DISTRIBUTORSALESREPORT '" + DepotID + "','" + DistributorID + "','" + Month + "', '" + Year + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPartyOnLoad(string fromdate, string todate, string depot, string Scheme, string bsid, string CustomerID, string UserID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT] '" + fromdate + "','" + todate + "','" + depot + "','" + Scheme + "','" + bsid + "','1','" + CustomerID + "','" + UserID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindProductwisePartyOnLoad(string fromdate, string todate, string depot, string bsid, string saletype, string productid, int amt, string USERID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISEPARTY] '" + fromdate + "','" + todate + "','" + depot + "','" + bsid + "','" + saletype + "','" + productid + "'," + amt + ",'" + USERID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindProductwithBS(string BSID)
        {
            string sql = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID " +
                         " WHERE BUSNESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BSID + "',','))  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindUserType()
        {
            string sql = " SELECT A.ROLEID,B.UTNAME FROM M_ROLE_HEIRARCHY A INNER JOIN M_USERTYPE B ON A.ROLEID = B.UTID " +
                         " WHERE A.ROLEID NOT IN((SELECT DISTRIBUTORID FROM P_APPMASTER),'DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB') ORDER BY AUTOID DESC";

            //string sql = "select utid as ROLEID,utname from M_USERTYPE where applicableto='C' and utname like '%Sales%'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUserTypeNEW()
        {
            string sql = " SELECT utid AS ROLEID,UTNAME FROM M_USERTYPE WHERE  applicableto='C' and utname like '%Sales%' " +
                         " AND UTID NOT IN ('DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB','E71105C2-64B7-40E9-AEE5-A08F055C6CDC') ORDER BY UTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindEmployeebyType(string typeid)
        {
            string sql = "SELECT USERID,(FNAME+' '+LNAME) AS FULLNAME FROM M_USER WHERE USERTYPE='" + typeid + "' ORDER BY FULLNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTimeSpan(string Tag)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /* Modified by Soumitra Mondal on 05.04.19 for new FY */
        public DataTable FetchDateRange(string span, string Tag, string FinYear)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
            }
            else
            {
                sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable FetchDateRange(string span, string Tag)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                sql = " [USP_FETCH_DATERANGE] '" + span + "','" + Tag + "'";
            }
            else
            {
                sql = " [USP_FETCH_DATERANGE] '" + span + "','" + Tag + "'";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        /* End of Modification */
        public DataTable BindJC()
        {
            string sql = " SELECT JCID,NAME FROM M_JC ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSalesGrid1(string usertype, string userid, string calender, string material, string calender_details, string header_based, string header_based_details, string fromdate, string todate, string finyear)
        {
            string sql = " EXEC [USP_RPT_SECONDARY_STOCK_SALES_MAIN] '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + finyear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindSalesGrid2(string usertype, string userid, string calender, string material, string calender_details, string header_based, string header_based_details, string fromdate, string todate, string brand_cat)
        {
            string sql = " EXEC [USP_RPT_SECONDARY_STOCK_SALES_MAIN_UNIT_VALUE] '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + brand_cat + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindSalesGrid3(string usertype, string userid, string calender, string material, string calender_details, string header_based, string header_based_details, string fromdate, string todate, string brand_cat, string unitvalue, string uom)
        {
            string sql = " EXEC [USP_RPT_SECONDARY_STOCK_SALES_MAIN_PRODUCTWISE] '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + brand_cat + "','" + unitvalue + "','" + uom + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindASM()
        {
            string sql = " SELECT USERID,(FNAME+' '+LNAME) + ' ('+ HQNAME +')' AS USERNAME FROM M_USER WHERE USERTYPE IN ('B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86','5B192861-B248-4F3A-8DBF-2CD8446F6628') ORDER BY USERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindTrendAnalysisGrid1(string usertype, string Hoid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string bsid)
        {
            //string sql = " EXEC [USP_RPT_SECONDARY_TRENDANALYSIS_CAT_BRAND]  '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + sale_type + "'";
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY  '" + usertype + "','" + Hoid + "','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + bsid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindTrendAnalysisGrid2(string usertype, string Hoid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string bsid, string CatID, string DivId)
        {
            //string sql = " EXEC [USP_RPT_SECONDARY_TRENDANALYSIS_CAT_BRAND_UNIT_VALUE] '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + analysis + "','" + brand_catid + "'";
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY_UNIT_VALUE  '" + usertype + "','" + Hoid + "','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + CatID + "','" + DivId + "','" + bsid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindTrendAnalysisGrid3(string usertype, string Hoid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string bsid, string CatID, string DivId, string UnitValue)
        {
            //string sql = " EXEC [USP_RPT_SECONDARY_TRENDANALYSIS] '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + analysis + "','" + brand_catid + "','" + unitvalue + "','" + uom + "'";
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY_PRODUCTLIST  '" + usertype + "','" + Hoid + "','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + CatID + "','" + DivId + "','" + UnitValue + "','" + bsid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindTrendAnalysisDepotwiseGrid1(string bsid, string depotid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type)
        {
            //string sql = " EXEC [USP_RPT_SECONDARY_TRENDANALYSIS_CAT_BRAND]  '" + usertype + "','" + userid + "','" + calender + "','" + material + "','" + calender_details + "','" + header_based + "','" + header_based_details + "','" + fromdate + "','" + todate + "','" + sale_type + "'";
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY  '','','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + bsid + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindTrendAnalysisDepotwiseGrid2(string bsid, string depotid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string CatID, string DivId)
        {
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY_UNIT_VALUE  '','','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + CatID + "','" + DivId + "','" + bsid + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindTrendAnalysisDepotwiseGrid3(string bsid, string depotid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string CatID, string DivId, string UnitValue)
        {
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_PRIMARY_PRODUCTLIST  '','','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + CatID + "','" + DivId + "','" + UnitValue + "','" + bsid + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindTrendAnalysisDistributorwiseGrid(string usertype, string Hoid, string material, string calender_details, string JcDetails, string header_based, string fromdate, string todate, string amt_qty, string sale_type, string CatID, string DivId, string bsid, string packsize)
        {
            string sql = " EXEC USP_RPT_SECONDARY_TRENDANALYSIS_DISTRIBUTORWISE  '" + usertype + "','" + Hoid + "','" + material + "','" + amt_qty + "','" + JcDetails + "','" + calender_details + "','" + fromdate + "','" + todate + "','" + header_based + "','" + sale_type + "','" + CatID + "','" + DivId + "','" + bsid + "','" + packsize + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindASMwiseGrid(string usertype, string userid, string caldel, string year, string matdel, string analon, string frmdate, string todate, string finyear)
        {
            string sql = " EXEC [USP_RPT_SECONDARY_ASM_ASE_SALES_MAIN] '" + usertype + "','" + userid + "','" + caldel + "','" + year + "','" + matdel + "','" + analon + "','" + frmdate + "','" + todate + "','" + finyear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindPackSizeName()
        {
            string sql = "SELECT DISTINCT UNITVALUE, (UNITVALUE+' '+UOMNAME) AS PACKSIZE FROM M_PRODUCT ORDER BY UNITVALUE ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackSizeUOMName()
        {
            string sql = "SELECT DISTINCT UNITVALUE + '~' + UOMID AS PACKSIZEID, (UNITVALUE+' '+UOMNAME) AS PACKSIZENAME FROM M_PRODUCT WHERE UNITVALUE IS NOT NULL ORDER BY UNITVALUE + '~' + UOMID ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindHeadQuarder(string UserTypeID)
        {
            //  string sql = "SELECT DISTINCT HQID,HQNAME FROM VW_HEADQUARTER WHERE USERTYPE='" + UserTypeID + "' ORDER BY HQNAME";
            string sql = " SELECT DISTINCT  HQNAME ,HQID  from m_user where isnull(hqname,'') <>'' AND usertype='" + UserTypeID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAssignto(string ID)
        {
            string sql = "EXEC [USP_RPT_PARTYWISE_ASSIGNTO] '" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //public DataTable BindBusinessegment_PrimarySales()
        //{
        //    string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID <> (SELECT EXPORTBSID FROM P_APPMASTER) ORDER BY NAME ASC ";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}

        public DataTable BindBusinessegment_PrimarySales()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment_PrimarySales_new(string usertype)
        {
            string sql = "EXEC [usp_BindBusinessegment_bind] '" + usertype + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment_BySalesDept(string UserId)
        {
            string sql = "EXEC [USER_ROLE_WISE_BSLOAD] '" + UserId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCitybyHO()
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindLedgerReport(string fromdate, string todate, string ledgerid, string depot, string Sessionid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + depot + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public string GetFinYearID(string Session)
        {
            string sql = "SELECT id FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        public DataTable BindVoucher_credit_debit(string AccEntry_ID)
        {
            string sql = "EXEC [USP_RPT_DEBIT_CREDIT_LEDGER] '" + AccEntry_ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindTrialBalance(string FromDate, string ToDate, string RegionID, string FinYear,string Type)
        {
            string sql = "EXEC [USP_RPT_TRAILBALANCE] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + FinYear + "','"+ Type +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindBalanceSheet(string FromDate, string ToDate, string RegionID, string FinYear)
        {
            string sql = "EXEC [USP_RPT_BLSHEET] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + FinYear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindProfitLoss(string FromDate, string ToDate, string RegionID, string FinYear)
        {
            string sql = "EXEC [USP_RPT_PLSHEET] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + FinYear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        protected string Convert_To_YYYYMMDD(string pDate)
        {

            string sDate = "";

            string dd = "";
            string mm = "";
            string yy = "";


            dd = pDate.Substring(0, 2);
            mm = pDate.Substring(3, 2);
            yy = pDate.Substring(6, 4);




            sDate = yy + "-" + mm + "-" + dd;




            return sDate;
        }

        public DataTable Bind_PO_PRODUCT_SUMMARY(string POID, string TPUID, string fromdate, string todate)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PO_SUMMARY] '" + POID + "','" + TPUID + "','" + fromdate + "','" + todate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_PO_ORDER_SUMMARY(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_PO_SUMMARY] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_CompanyInfo()
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_COMPANY_INFO]";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_CompanyInfofactoryid(string poid) /*NEW SP ADD FOR FACTORY WISE PRINT OUT*/
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_COMPANY_INFO_FOR_FACTORYWISE] '"+ poid + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bind_TPUInfo(string VendorID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_TPU_INFO]'" + VendorID + "' ";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInvoice_Depot(string ID, string FROMDATE, string TODATE)
        {
            string sql = "SELECT SALEINVOICEID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX) AS SALEINVOICENO FROM T_SALEINVOICE_HEADER " +
                         " WHERE DEPOTID='" + ID + "' AND SALEINVOICEDATE  BETWEEN CONVERT(DATETIME,'" + FROMDATE + "',103) AND CONVERT(DATETIME,'" + TODATE + "',103) " +
                         " ORDER BY SALEINVOICENO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDetails(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDetails_CPC(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_CSD] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceFooter(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_FOOTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceHeader(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTax(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTerms(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TERMS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceItemWiseTax(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_ITEMWISE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoicePriceScheme(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_PRICE_SCHEME] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceQtyScheme(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_QTY_SCHEME_DMS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDistributor(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_DISTRIBUTORINFO] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bind_TaxComponents(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_TAX_COMPONENT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_OutstadingAmount(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOUCE_OUTSTANDING] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceFromDepot(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALEINVOICE_FROMDEPOT_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_Combo(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALEINVOICE_COMBO_PRODUCT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_ExportInvoice(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_EXPORT_INVOICE] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_ExportInvoice_GST(string InvoiceID,string original,string duplicate,string triplicate)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_EXPORT_INVOICE_GST] '" + InvoiceID + "','"+ original + "','"+ duplicate + "','"+ triplicate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Bind_ClaimReport(string fromdate, string todate, string Depotid, string BSID, string status, string partyid)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_CLAIM_PARTYWISE] '" + fromdate + "','" + todate + "','" + Depotid + "','" + BSID + "','" + status + "','" + partyid + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindClaimStatus()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT ID,CLAIMSTATUSNAME FROM M_CLAIM_STATUS ORDER BY CLAIMSTATUSNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProforma(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PROFORMA_CREDITNOTE] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTerms_Proforma(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PROFORMA_INVOICE_TERMS_CONDITIONS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTerms_PaclingList(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PACKINGLIST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindST_Header(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_HEADER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_ToDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_TRANSFER_TODEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_FromDepot_Details(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_TRANSFER_FROMDEPOT_DETAILS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_QC_PRODUCT_SUMMARY(string Product, string TPUID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_PU_SUMMARY] '" + Product + "','" + TPUID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductForReport()
        {
            string sql = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE SETFLAG='P' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDebitNote(string Vouchertype, string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE] '" + Vouchertype + "','" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Depot_Accounts(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "'  ORDER BY BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }


        public DataTable BindInvoiceNoByUser(string frmdate, string todate, string depotid, string bsid, string isvarified)
        {
            string sql = "";
            DataTable dt1 = new DataTable();

            if (depotid != "0")
            {
                sql = "SELECT  (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                            "WHERE DEPOTID IN(SELECT BRID FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + depotid + "')  AND ISVERIFIED='" + isvarified + "' AND BSID='" + bsid + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";

            }
            else
            {
                sql = "SELECT  (SALEINVOICEPREFIX + '/' + SALEINVOICENO + '/' + SALEINVOICESUFFIX) AS  SALEINVOICENO,SALEINVOICEID FROM T_SALEINVOICE_HEADER " +
                          "WHERE BSID='" + bsid + "' AND ISVERIFIED='" + isvarified + "' AND SALEINVOICEDATE BETWEEN CONVERT(DATETIME,'" + frmdate + "',103) AND  CONVERT(DATETIME,'" + todate + "',103) ORDER BY SALEINVOICENO DESC";


            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindChallan_Details(string ChallanID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKTRANSFER_CHALLAN_DETAILS] '" + ChallanID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChallan_Header(string ChallanID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKTRANSFER_CHALLAN_HEADER] '" + ChallanID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChaalan_ToDepot(string ChallanID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_TRANSFER_TODEPOT_CHALLAN_DETAILS] '" + ChallanID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChaalan_FromDepot(string ChallanID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_TRANSFER_FROMDEPOT_CHALLAN_DETAILS] '" + ChallanID + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindStockJournalReport(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_JOURNAL] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDespatchReport(string fromdate, string todate, string TPUID, string depotid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_DESPATCH_DETAILS] '" + fromdate + "','" + todate + "','" + TPUID + "','" + depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindItemLedger(string fromdate, string todate, string depotid, string productid, string storelocation)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_Ledger] '" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + storelocation + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductALIAS()
        {
            //string sql = "SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT ORDER BY NAME";
            string sql = " SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT" +//Add by Rajeev (24-03-2018)
                         " WHERE DIVID NOT IN('11','6','13','7')" +
                         " ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPOHeader(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [sp_rpt_PURCHASE_ORDER_HEADER] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPODetails(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [sp_rpt_PURCHASE_ORDER_DETAILS] '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindJournal(string VoucherTypeId, string VoucherId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACC_JOURNAL]  '" + VoucherTypeId + "','" + VoucherId + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindInvoiceTax(string INVOICEID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_SALE_INVOICE_TAX] '" + INVOICEID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurchaseHeader(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_HEADER] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseDetails(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_DETAILS] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseFooter(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_FOOTER] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPurchaseTax(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_TAX] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_SendDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_SENDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSR_DetailsDS(string STNO)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT] '" + STNO + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "SR_TRANSFER";
            ds.Tables[1].TableName = "SR_ITEMS";
            ds.Tables[2].TableName = "SR_RECEVIEDDEPO";
            ds.Tables[3].TableName = "SR_SENDDEPO";
            ds.Tables[4].TableName = "SR_COMPANYINFO";
            return ds;
        }

        public DataTable BindSR_Items(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_ITEMS] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_RecvdDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_RECVDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_Transfer(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_TRANSFER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductForTPU(string TPUID)
        {
            string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM M_PRODUCT_TPU_MAP WHERE VENDORID='" + TPUID + "' ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_Claim(string Vouchertype, string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM] '" + Vouchertype + "','" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetMultipulClaimCreditNote(string vouchertype, string voucherids, string UserID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_MULTIPUL_ACCOUNTDEBITNOTE_CLAIM] '" + vouchertype + "','" + voucherids + "','" + UserID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_HeaderClaim(string voucherid, string UserID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM_HEADER] '" + voucherid + "','" + UserID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCreditNote_PartyHeaderClaim(string voucherid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACCOUNTDEBITNOTE_CLAIM__PARTY_HEADER] '" + voucherid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindClaimHistoryReports(string fromdate, string todate, string depotid, string status, string businesssegment)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_CLAIM_PARTYWISE_HISTORY] '" + fromdate + "','" + todate + "','" + depotid + "','" + status + "','" + businesssegment + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindClaimSummary(string fromdate, string todate, string depotid)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_DEPOTWISE_CLAIM_SUMMARY] '" + fromdate + "','" + todate + "','" + depotid + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        
        public DataTable BindTopSheet_ClaimHeader(string Depotid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT__CLAIM_TOPSHEET_HEADER] '" + Depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTopSheet_ClaimDetails(string claimtypeid, string claimid, string userid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_CLAIM_TOPSHEET_PRINT] '" + claimtypeid + "','" + claimid + "','" + userid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTopSheet_ClaimDetailsPage(string fromdate, string todate, string depotid, string claimtype)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_CLAIM_TOPSHEET] '" + fromdate + "','" + todate + "','" + depotid + "','" + claimtype + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataSet BindSalesTaxSummary_Details(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSummaryDetails(string fromdate, string todate, string depotid, string partyname)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY '" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindDistributorbyAdmin()
        {
            string retailerid = "SELECT RETAILERID FROM P_APPMASTER";
            string retailer = Convert.ToString(db.GetSingleValue(retailerid));

            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN('" + retailer + "') ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Get Tpu Party Name
        public DataTable BindTPUPatyName()
        {
            string Sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE TAG='T' AND SUPLIEDITEMID NOT IN('8','9')";
            DataTable dt = new DataTable();
            dt = db.GetData(Sql);
            return dt;
        }
        #endregion

        public DataTable BindDepotwiseSales(string caldetails, string spamid, string fromdate, string todate, string groupid, string productid, string BSID)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALE_REPORT] '" + caldetails + "','" + spamid + "','" + fromdate + "','" + todate + "','" + groupid + "','" + productid + "','" + BSID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroup()
        {
            string sql = "SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY ORDER BY DIS_CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Retrive CategoryWise_Sales
        public DataTable BindCategoryWise_Sales(string fromdate, string todate, string zone)
        {
            string sql = "EXEC [USP_RPT_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Retrive ZoneWise_Sales
        public DataTable BindZoneWise_Sales(string fromdate, string todate, string zone)
        {
            string sql = "EXEC [USP_RPT_CATEGORYWISE_ZONEWISE] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Get Zone
        public DataTable BindZone()
        {
            string sql = "SELECT DISTINCT ZONE AS ZONE_ID,CASE WHEN ZONE='N' THEN 'North'WHEN ZONE='C' THEN 'Central'WHEN ZONE='E' THEN 'East'WHEN ZONE='S' THEN 'South'WHEN ZONE='W' THEN 'West'WHEN ZONE='NE' THEN 'North East'Else 'Central'END AS Zone FROM M_REGION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindProductbybrandandcatid(string divid, string catid)
        {
            string sql = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE DIVID IN(SELECT *  FROM DBO.FNSPLIT('" + divid + "',',')) AND CATID IN(SELECT *  FROM DBO.FNSPLIT('" + catid + "',',')) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindDepotwiseProductwiseSales(string fromdate, string todate, string groupid, string productid, string BSID)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_PRODUCTWISE_SALE_REPORT] '" + fromdate + "','" + todate + "','" + groupid + "','" + productid + "','" + BSID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindCustomerwiseReport(string fromdate, string todate, string groupid)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + groupid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerConfirmedInvoice(string depotid, string destid, string userid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_CONFIRMED_INVOICES] '" + depotid + "','" + destid + "','" + userid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindCustomerConfirmedInvoiceDetails(string invoiceid, string finyr)
        {
            string sql = "EXEC [USP_RPT_DEPOTWISE_PRODUCTWISE_INVOICE_DETAILS] '" + invoiceid + "','" + finyr + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindCustomerPendingInvoice(string depotid, string destid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_PENDING_INVOICES] '" + depotid + "','" + destid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotByUserid(string userid)
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE   BRID IN (SELECT TPUID FROM M_TPU_USER_MAPPING WHERE USERID='" + userid + "')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCustomerByUserid(string userid)
        {
            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID IN (SELECT * FROM DBO.FNSPLIT((SELECT DISTRIBUTORLIST FROM M_USER WHERE USERID='" + userid + "'),','))ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerInvoiceCount(string userid, string finyr)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_INVOICES_COUNT] '" + userid + "','" + finyr + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotFreeProductSale(string format, string span, string fromdate, string todate, string depotid, string productid, string showby, string bsid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOT_FREE_PRODUCT_SALE] '" + format + "','" + span + "','" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + showby + "','" + bsid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTrialBalance_tax_payable(string FromDate, string ToDate, string RegionID, string AccgroupID)
        {

            string sql = "EXEC [USP_RPT_TRAILBALANCE_TAX_PAYABLE] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + AccgroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHQbyDepot(string DepotID)
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY HQNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindPrimarySalesTaxSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn, string CustomerID, string USERID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "','" + CustomerID + "','" + USERID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindInvoiceDetialsReport(string InvoiceID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_DETAILS] '" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindHQSalesReport(string DepotID, string Bsid, string fromdate, string todate)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_HEADQUARTER_SALES] '" + DepotID + "','" + Bsid + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDailyReportGrid(string date, string todate, string depotid)
        {
            string sql = "EXEC [USP_RPT_GET_DAILY_DMS_INVOICE_COUNT] '" + date + "','" + todate + "','" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDetails_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceHeader_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTax_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_ITEMWISE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceFooter_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_FOOTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoicePriceScheme_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_RETURN_PRICE_SCHEME] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDistributor_SaleReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_SALE_RETURN_DISTRIBUTORINFO] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Fetch Party For a Business Segment Wise
        public DataTable BindPartyBSWise(string BusinessSegmentID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BusinessSegmentID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPartyBSWise(string BusinessSegmentID, string DEPOTID)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID = B.CUSTOMERID WHERE BUSINESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BusinessSegmentID + "',',')) AND B.DEPOTID='" + DEPOTID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        #region Fetch Party For a HQ Wise
        public DataTable BindPartyHQWise(string HQID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_HQ_MAPPING WHERE HQID IN (SELECT *  FROM DBO.FNSPLIT('" + HQID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable Bind_DepotWise_Statewise(string FROMDATE, string TODATE, string STATEID, string BSID, decimal AMTIN)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PRIMARY_SALE_DEPOT_STATEWISE] '" + FROMDATE + "','" + TODATE + "','" + STATEID + "','" + BSID + "','" + AMTIN + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillEntry(string fromdate, string todate, string depot, string Type)
        {
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_ENTRY] '" + fromdate + "','" + todate + "','" + depot + "','" + Type + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDetails_GST(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_GST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSalesTaxSummary_Details_GST(string fromdate, string todate, string depotid, string
                SchemeStatus, string BSID, string type, string taxtypeid, string partyid, string InvoiceType, string DetailsType)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_GST] '" + fromdate + "','" + todate + "','" +

            depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "','" + InvoiceType + "','" + DetailsType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSummaryDetails_GST(string fromdate, string todate, string depotid, string partyname)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST'" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindSummaryDetails_GST_FACTORY_ALL(string fromdate, string todate, string depotid, string partyname, string InvoiceType, string vouchertype)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST_FACTORYNEW'" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "','" + InvoiceType + "','" + vouchertype + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindSummaryDetails_GST_FACTORY(string fromdate, string todate, string depotid, string partyname, string InvoiceType, string vouchertype)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST_CANCELLED_FACTORY '" + fromdate + "','" + todate + "','" + depotid + "','" + partyname + "','" + InvoiceType + "','" + vouchertype + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindPurchaseDetails_GST(string STCKRID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIVED_DETAILS_GST] '" + STCKRID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_Items_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_ITEMS_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSR_FOOTER_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_FOOTER_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindST_Details_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_DETAILS_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindST_FOOTER_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_FOOTER_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_InvoiceDetails_CPC_GST(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_CSD_GST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindstock(string fromdate, string todate, string finyear, string depotid, string module)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_STOCK_TRANSFER] '" + fromdate + "','" + todate + "','" + finyear + "','" + depotid + "','" + module + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCollectionSummary(string fromdate, string todate, string depotid, string finyear)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_CLLECTION_SUMMARY] '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCat()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_BIND_BRAND_CATEGORY 'C'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivision()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_BIND_BRAND_CATEGORY 'B'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductforPartyWiseProductSales(string DIVID, string CATID)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) AND CATID  IN (SELECT * FROM DBO.fnSplit('" + CATID + "',','))  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindInvoiceDetialsReport_GST(string InvoiceID)
        {
            string sql = "EXEC [USP_RPT_SALE_INVOICE_DETAILS_GST_PRIMARY_INVOICE_DETAILS] '" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindDepot_userwise(string userid)
        {
            string sql = "EXEC [USP_RPT_BINDDEPOT] '" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUserDepot(string userid)
        {
            string sql = "EXEC [USP_BIND_DEPOT] '" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindSKUSize()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT CAST(UNITVALUE AS FLOAT) AS UNITVALUE FROM M_PRODUCT ORDER BY CAST(UNITVALUE AS FLOAT)  ASC";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductforPartyWiseProductSales(string DIVID, string CATID, string SIZE)
        {
            string sql = string.Empty;

            if (SIZE == "-1")
            {
                sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) AND CATID  IN (SELECT * FROM DBO.fnSplit('" + CATID + "',','))  ORDER BY NAME";
            }
            else
            {
                sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) AND CATID  IN (SELECT * FROM DBO.fnSplit('" + CATID + "',',')) AND UNITVALUE='" + SIZE + "'  ORDER BY NAME";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductforPartyWiseProductSales(string SIZE)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE  UNITVALUE='" + SIZE + "'  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_Allocation(string tpuid, string fromdate, string todate, string depotid)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_Allocation_Sheet] '" + tpuid + "','" + depotid + "','" + fromdate + "','" + todate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_TargetSaleStock(string depotid, string productid, string fromdate, string todate, string packsizeid, string storelocation)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_TARGET_VS_SALE] '" + depotid + "','" + productid + "','" + fromdate + "','" + packsizeid + "','" + storelocation + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_TargetSaleStock_json(string depotid, string productid, string BSID,string fromdate, string packsizeid, string storelocation,string mrpType, string rptType)
        {
            DataSet dt = new DataSet();
            string sql = "[dbo].[USP_RPT_TARGET_VS_SALE_JSON] '" + depotid + "','" + productid + "','"+BSID+"','" + fromdate + "','" + packsizeid + "','" + storelocation + "','"+ mrpType + "','"+ rptType + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }


        public DataTable BindPendingVouchers(string fromdate, string todate, string depotid, string vouchertype)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PENDING_VOUCHER_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + vouchertype + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGoodsDespatchReport(string fromdate, string todate, string depotid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_GOODS_DESPATCH_DETAILS] '" + fromdate + "','" + todate + "','" + depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindCSDReport(string fromdate, string todate, string depot, string productid)
        {
            string sql = "EXEC [USP_RPT_CSD_DETAILS] '" + fromdate + "','" + todate + "','" + depot + "','" + productid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindProductwithBSCSD()
        {
            string sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID " +
                         " WHERE BUSNESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT((select BS_CSD from P_APPMASTER),','))  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMonthWiseSales(string fromdate, string todate, string depotid, string productid, string bsid)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_DEPOTWSIE_PRODUCTWISE_MONTHLY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + bsid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindFreightDetails(string fromdate, string todate, string depotid, string Type, string finyear)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_FREIGHT_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + Type + "','" + finyear + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindInvoiceBillDetails(string fromdate, string todate, string depotid, string Distributorid)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_DAYEND_BILL_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + Distributorid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSaleReturnSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_TAX_SUMMARY_REPORT_RETURN] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSalesReturnTaxSummary_Details(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_RETURN] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSalesReturnTaxSummary_DetailsBoth(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_BOTH] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindAccTransporterVoucher(string fromdate, string todate, string VoucherTypeID, string Mode, string region)
        {
            string sql = "EXEC [USP_RPT_ACC_TRANSPORTER_VOUCHERDETAILS_PART1] '" + fromdate + "','" + todate + "','" + VoucherTypeID + "','" + Mode + "', '" + region + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable TransporterBillVoucherType()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select Id ,VoucherName from Acc_VoucherTypes where Id=5 Order by VoucherName";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }
        public DataTable BindDepotWithoutDepot(string BRID)
        {
            string sql = "SELECT  DISTINCT BRID AS CUSTOMERID,BRPREFIX AS CUSTOMERNAME FROM M_BRANCH  WHERE  BRID NOT IN ('" + BRID + "') AND BRANCHTAG='D'  ORDER BY BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorbyAdminWithDepot()
        {
            string retailerid = "SELECT RETAILERID FROM P_APPMASTER";
            string retailer = Convert.ToString(db.GetSingleValue(retailerid));
            string sql = "SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN('" + retailer + "')" +
                        " union all" +
                        " SELECT BRID AS ID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'   AND " +
                        " BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER)," +
                        " (SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorbyAdminWithDepot(string depotid)
        {
            string sql = "SELECT DISTINCT B.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + depotid + "',','))" +
                        " union all" +
                        " SELECT BRID AS ID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  AND " +
                        " BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER)," +
                        " (SELECT EXPORTPAKISTAN FROM P_APPMASTER),'" + depotid + "') ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTpu_Depot()
        {
            string sql = " SELECT VENDORID AS ID, VENDORNAME AS BRNAME FROM M_TPU_VENDOR WHERE TAG='T' AND SUPLIEDITEMID NOT IN('8','9')" +
                        " union all" +
                        " SELECT BRID AS ID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  AND " +
                        " BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER)," +
                        " (SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAccVoucher_NotMap(string fromdate, string todate, string VoucherTypeID, string Mode, string region)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART1_NOTMAP] '" + fromdate + "','" + todate + "','" + VoucherTypeID + "','" + Mode + "', '" + region + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTSIWiseBillReport(string fromdate, string todate, string depotid, string Distributorid)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_DAYEND_BILL_REPORT_TSIWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + Distributorid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_CSD_PO_report(string fromdate, string todate, string BSID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PO_CSD] '" + fromdate + "','" + todate + "','" + BSID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_OpeningbalancesReport(string Date)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_DEPOT_BALANCES] '" + Date + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimStatus_Approval()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT ID,CLAIMSTATUSNAME FROM M_CLAIM_STATUS WHERE ID IN (5,2) ORDER BY CLAIMSTATUSNAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet InvoiceDetails_ForPrint(string InvoiceID)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC USP_RPT_LEDGER_REPORT_PRINT '" + InvoiceID + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet Bindsale_mainprint(string AccentryID)
        {
            DataSet dt = new DataSet();
            string sql = " select SALEINVOICEID,BSID,EXPORT from T_SALEINVOICE_HEADER a inner join Acc_Voucher_Invoice_Map b on a.SALEINVOICEID=b.InvoiceID " +
                         " where b.AccEntryID = '" + AccentryID + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet InvoiceDetails_ForPrintAcc(string InvoiceID)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC USP_RPT_LEDGER_REPORT_ACCPRINT '" + InvoiceID + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindForLedgerReport_DepotWise(String DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                  "LEFT JOIN Acc_AccountGroup B ON A.actGrpCode = B.CODE " +
                  "INNER JOIN Acc_Branch_Mapping M  ON A.Id = M.LedgerID WHERE BranchID IN(SELECT * FROM DBO.fnSplit('" + DepotID + "', ','))" +
                  " AND name <> '' ORDER BY LedgerName";

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLedger_DepotLedgerNameWise(string DepotID, string LedgerName)/*Add By Rajeev(04-03-2021)*/
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC BIND_LEDGER_NAMEWISE '" + DepotID + "','" + LedgerName + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransporterBillNo(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT TRANSPORTERBILLID,TRANSPORTERBILLNO FROM T_TRANSPORTER_BILL_HEADER WHERE TRANSPORTERBILLDATE BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103)";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillHeader(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_HEADER] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillDetails(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_DETAILS] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillFooter(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_FOOTER] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRetailerWiseSaleReport(string fromdate, string todate, string Party, string Product)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RETAILERWISE_PRODUCTWISE_SALE_REPORT] '" + fromdate + "','" + todate + "','" + Party + "','" + Product + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBeatByDistributor(string DistributorID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT BEAT_ID,BEAT_NAME FROM M_BEAT_DISTRIBUTOR_MAPPING WHERE DISTRIBUTORID IN (SELECT * FROM DBO.fnSplit('" + DistributorID + "',',')) ORDER BY BEAT_NAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductforRetailerWiseProductSales(string CATID, string UNITVALUE)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE CATID IN (SELECT * FROM DBO.fnSplit('" + CATID + "',',')) AND UNITVALUE IN (SELECT * FROM DBO.fnSplit('" + UNITVALUE + "',','))  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRouteWiseSaleReport(string fromdate, string todate, string Party, string RouteID, string CatID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ROUTEWISE_SALE_REPORT] '" + fromdate + "','" + todate + "','" + Party + "','" + RouteID + "','" + CatID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindEffectiveCoverageReport(string fromdate, string todate, string Party, string RouteID, string HQID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_EFFECTIVE_COVERAGE_REPORT] '" + fromdate + "','" + todate + "','" + Party + "','" + RouteID + "','" + HQID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFortnightRouteWiseSalesReport(string Month, string DepotID, string Party, string Tag)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_FORTNIGHT_ROUTE_SALES] '" + Month + "','" + DepotID + "','" + Party + "','" + Tag + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFortnightRouteWiseSalesReport_Qtywise(string Month, string DepotID, string Party, string Tag)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_FORTNIGHT_ROUTE_SALES_QTYWISE] '" + Month + "','" + DepotID + "','" + Party + "','" + Tag + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindConsolidateTrialbalance(string Fromdate, string Todate, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRAILBALANCE_CONSOLIDATE] '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGrpSummaryTrialbalance(string Fromdate, string Todate, string Finyear, string RegionID, string GroupID, string GroupName)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRAILBALANCE_GRP_SUMMARY] '" + RegionID + "','" + GroupID + "','" + GroupName + "', '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGrpSummaryTrialbalance_multi(string Fromdate, string Todate, string Finyear, string RegionID, string GroupID, string GroupName)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRAILBALANCE_GRP_SUMMARY_MULTI] '" + RegionID + "','" + GroupID + "','" + GroupName + "', '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAccountsGroup()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Code,grpName FROM Acc_AccountGroup ORDER BY grpName";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindTransitReport(string Fromdate, string ToDate, string ToDepot, string FromDepot, string TPU, string ProductID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_DEPOTWISE_TRANSIT_REPORT] '" + Fromdate + "','" + ToDate + "','" + ToDepot + "','" + FromDepot + "','" + TPU + "','" + ProductID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindProductbyCategory(string CATID)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE CATID IN (SELECT * FROM DBO.fnSplit('" + CATID + "',',')) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindASMNAME()
        {
            string sql = "SELECT USERID,(FNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86' AND ISACTIVE='1' ORDER BY FNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRSM()
        {
            string sql = "SELECT USERID,(FNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25' AND ISACTIVE='1' ORDER BY FNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFinalDSRReport(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype)
        {
            string sql = "EXEC [USP_RPT_FINAL_DSR_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + Amount + "','" + AmtType + "','" + EntryType + "','" + usertype + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindFinalDSRReport_JSON(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype)
        {
            string sql = "EXEC [USP_RPT_FINAL_DSR_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + Amount + "','" + AmtType + "','" + EntryType + "','" + usertype + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet Bindtsiwisesales_JSON(string DateFrom, string DateTo, string ASMID, string SOID, string DISTRIBUTORID)
        {
            string sql = "EXEC [Usp_sales_report_with_eco] '" + DateFrom + "','" + DateTo + "','" + ASMID + "','" + SOID + "','" + DISTRIBUTORID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }


        public DataSet Bindtsiwisesalespricewise_JSON(string DateFrom, string DateTo, string ASMID, string SOID, string DISTRIBUTORID)
        {
            string sql = "EXEC [Usp_sales_report_with_eco_price] '" + DateFrom + "','" + DateTo + "','" + ASMID + "','" + SOID + "','" + DISTRIBUTORID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindSO()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT USERID,(FNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE USERTYPE='9BF42AA9-0734-4A6A-B835-0885FBCF26F5' AND ISACTIVE='1' ORDER BY FNAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSODailyReport(string FromDate, string ToDate, string SOID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_SO_DAILY_REPORT] '" + FromDate + "','" + ToDate + "','" + SOID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDMSDailyTracker(string fromdate, string todate, string depotid, string distributorid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string finyear)
        {
            string sql = "EXEC [USP_RPT_DMS_DAILY_TRACKER_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + distributorid + "','" + headqurtr + "','" + bsid + "','" + saletype + "'," + AmountIn + ",'" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMonth()
        {
            string sql = "SELECT TIMESPAN,MONTHID FROM M_TIMEBREAKUP WHERE FINYEAR ='" + HttpContext.Current.Session["FINYEAR"].ToString() + "' AND SEARCHTAG='M' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductionPlan(string DATE, string TPUID, string MONTHID, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TPU_PRODUCTION_PLAN] '" + DATE + "','" + TPUID + "','" + MONTHID + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCostCenter()
        {
            string sql = "SELECT COSTCENTREID,COSTCENTRENAME FROM M_COST_CENTRE ORDER BY COSTCENTRENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCostCenterVoucherDetails(string FromDate, string ToDate, string LedgerID, string RegionID, string FinYear, string CostCenterID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_COSTCENTRE_DETAILS] '" + FromDate + "','" + ToDate + "','" + LedgerID + "','" + RegionID + "','" + FinYear + "','" + CostCenterID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLedgerWiseCostCenter(string FromDate, string ToDate, string RegionID, string AccGroupID, string BSID, string LedgerId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_LEDGERWISE_COSTCENTRE] '" + FromDate + "','" + ToDate + "','" + RegionID + "','" + AccGroupID + "','" + BSID + "','" + LedgerId + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCostCenterwiseDetails(string LedgerID, string AccEntryID, string CostCentreID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_COSTCENTREWISE] '" + LedgerID + "','" + AccEntryID + "','" + CostCentreID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable PortalDetails(string FROMDATE, string TODATE, string CUSTOMERID, string ddlshow_by)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC[USP_RPT_PORTAL_DETAILS] '" + FROMDATE + "','" + TODATE + "','" + CUSTOMERID + "','" + ddlshow_by + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BindProductbybrandid(string divid)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE DIVID IN (SELECT *  FROM DBO.FNSPLIT('" + divid + "',',')) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductbycatid(string catid)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE CATID IN (SELECT *  FROM DBO.FNSPLIT('" + catid + "',',')) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindOutwardSupply(string Fromdate, string Todate, string Party, string ProductID, string Type, string Depot)
        {
            string sql = string.Empty;

            if (Type == "1")
            {
                sql = "EXEC [USP_RPT_SUPPLY_OUTWARD] '" + Fromdate + "','" + Todate + "','" + Party + "','" + ProductID + "'";
            }
            else
            {
                sql = "EXEC [USP_RPT_SUPPLY_INWARD] '" + Fromdate + "','" + Todate + "','" + Depot + "','" + Party + "','" + ProductID + "'";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindHSNCode()
        {
            string sql = "SELECT HSNCODE FROM M_HSNCODE WHERE HSNCODE IS NOT NULL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHSNWiseReport(string Fromdate, string Todate, string HSN, string Type, string DepotID)
        {
            string sql = string.Empty;

            if (Type == "1")
            {
                sql = "EXEC [USP_RPT_HSNWISE_OUTWARD] '" + Fromdate + "','" + Todate + "','" + HSN + "','" + DepotID + "'";
            }
            else
            {
                sql = "EXEC [USP_RPT_HSNWISE_INWARD] '" + Fromdate + "','" + Todate + "','" + HSN + "','" + DepotID + "'";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotstatewise(string STATEID)
        {

            DataTable dt = new DataTable();
            string sql = "select BRID,BRPREFIX from m_branch where stateid='" + STATEID + "' ORDER BY BRNAME";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable inwardsupplyservices(string FROMDATE, string TODATE, string depotid, string type_of_service, string GRP_EXP_HEAD, string ISRCM)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC[usp_rpt_inward_supply_services] '" + FROMDATE + "','" + TODATE + "','" + depotid + "','" + type_of_service + "','" + GRP_EXP_HEAD + "','" + ISRCM + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable gstoutwardreturn(string FROMDATE, string TODATE, string depotid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC[USP_RPT_GST_OUTWARD_RETURN_REPORT] '" + FROMDATE + "','" + TODATE + "','" + depotid + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BindCustomerList()
        {

            DataTable dt = new DataTable();
            string sql = "EXEC USP_RPT_CUSTOMERlIST";
            dt = db.GetData(sql);
            return dt;

        }

        #region ALLFORDSRREPORTS_JSON
        public DataTable BindRSMAll()
        {
            string sql = "SELECT DISTINCT A.USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER A INNER JOIN  M_TPU_USER_MAPPING B ON A.USERID=B.USERID AND" +
                            " A.USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BINDASMBYRSMALL()
        {
            string sql = " SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN(SELECT A.USERID FROM M_USER A INNER JOIN  M_TPU_USER_MAPPING B ON A.USERID=B.USERID AND A.USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25') " +
                         " AND USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BINDSOBYASMALL()
        {
            string sql = " SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE " +
                         " REPORTINGTOID IN (SELECT USERID FROM M_USER WHERE USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86' AND REPORTINGTOID IN (SELECT USERID FROM M_USER WHERE USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25')) " +
                         " AND USERTYPE='9BF42AA9-0734-4A6A-B835-0885FBCF26F5'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion


        public DataTable LedgerOutstandiing(string FROMDATE, string TODATE, string customerid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_LEDGER_OUTSTANDING] '" + FROMDATE + "','" + TODATE + "','" + customerid + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable LedgerOutstandiing_Child(string InvID)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_LEDGER_OUTSTANDING_CHILD] '" + InvID + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable LedgerInvoiceVsCollection(string FROMDATE, string TODATE, string customerid, string regionid, string finyrid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_LEDGER_INVOICE_VS_COLLECTION] '" + FROMDATE + "','" + TODATE + "','" + customerid + "','" + regionid + "','" + finyrid + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable LoadStateBasedonDepotPartyBS(string depotid, string bsid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PARTYWISE_STATE] '" + depotid + "','" + bsid + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable LoadPartyBasedonState(string depotid, string bsid, string stateid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STATEWISE_PARTY] '" + depotid + "','" + bsid + "','" + stateid + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BindItemLedgerExport(string fromdate, string todate, string depotid, string productid, string storelocation)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_Ledger_Export] '" + fromdate + "','" + todate + "','" + depotid + "','" + productid + "','" + storelocation + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRetailerDisplayReport(string RouteID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RETAILER_DISPLAY_REPORT] '" + RouteID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet GSTBusinessReport(string FROMDATE, string TODATE, string depotid, string type)
        {

            DataSet ds = new DataSet();
            string sql = "EXEC[USP_RPT_GST_B2B] '" + FROMDATE + "','" + TODATE + "','" + depotid + "','" + type + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;

        }

        /*ADDED AND MODIFIED BY SUBHODIP DE ON 09.03.2018 */
        public DataSet BindBRSNonReconciliation(string FRMDATE, string TODATE, string LEDGERID, string REGIONID, string Finyr, string RECONCILEDTAG)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS_BRS] '" + FRMDATE + "','" + TODATE + "','" + LEDGERID + "','" + REGIONID + "','" + Finyr + "','" + RECONCILEDTAG + "' ";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable binddmsdailytracker(string DistributorID, string Fromdate, string Todate)
        {
            string sql = "EXEC [USP_RPT_DAILYTRACKER_DMS] '" + Fromdate + "','" + Todate + "','" + DistributorID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSuperbyDepot(string DepotID)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING AS A INNER JOIN M_CUSTOMER AS B ON A.CUSTOMERID=B.CUSTOMERID INNER JOIN M_USER AS C ON C.REFERENCEID=B.CUSTOMERID WHERE C.ISACTIVE='1' AND CUSTYPE_ID='826D656F-353F-430F-8966-4FE6BE3F67ED' " +
                         " AND DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindsupersoasm(string SUPERID)
        {

            string sql = "EXEC[USP_RPT_ASM_SO_SUPER]'" + SUPERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BinddistributorbyDepot(string DepotID)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING AS A INNER JOIN M_CUSTOMER AS B ON A.CUSTOMERID=B.CUSTOMERID INNER JOIN M_USER AS C ON C.REFERENCEID=B.CUSTOMERID WHERE C.ISACTIVE='1' AND CUSTYPE_ID='5E24E686-C9F4-4477-B84A-E4639D025135' " +
                         " AND DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Binddistributorsosasm(string DISTRIBUTORID)
        {

            string sql = "EXEC[USP_RPT_DISTRIBUTOR_HQ_DETAILS]'" + DISTRIBUTORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BindSKUSizebasedonProduct(string BrandID, string CatID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT (UNITVALUE+' '+UOMNAME) AS UNITVALUE,UNITVALUE AS SIZE FROM M_PRODUCT WHERE DIVID IN (SELECT *  FROM DBO.FNSPLIT('" + BrandID + "',',')) AND CATID IN (SELECT *  FROM DBO.FNSPLIT('" + CatID + "',','))";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSKUSizebasedonProduct_new(string BrandID, string CatID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT (UNITVALUE+' '+UOMNAME) AS UNITVALUE,(UNITVALUE+' '+UOMNAME) AS SIZE FROM M_PRODUCT WHERE DIVID IN (SELECT *  FROM DBO.FNSPLIT('" + BrandID + "',',')) AND CATID IN (SELECT *  FROM DBO.FNSPLIT('" + CatID + "',','))";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSKUSize(string BrandID, string CatID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT (UNITVALUE + ' ' + UOMNAME) AS UNITVALUE,(UNITVALUE + ' ' + UOMNAME) AS SIZE FROM M_PRODUCT WITH(NOLOCK) WHERE DIVID IN (SELECT *  FROM DBO.FNSPLIT('" + BrandID + "',',')) AND CATID IN (SELECT *  FROM DBO.FNSPLIT('" + CatID + "',',')) ORDER BY (UNITVALUE + ' ' + UOMNAME)";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotExcludeExport()
        {
            string sql = "SELECT BRID,BRPREFIX FROM M_BRANCH WHERE BRANCHTAG='D' AND EXPORT='N' ORDER BY BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomersowise(string soid)
        {

            string sql = "select customername,customerid from m_customer_tsi_mapping where TSI_ID='" + soid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable BindProduct_FG()
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT WHERE TYPE='FG' ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetSOType(string soid)
        {
            string GetType = string.Empty;
            string sql = "SELECT USERTYPE FROM M_USER WHERE USERID='" + soid + "'";
            GetType = Convert.ToString(db.GetSingleValue(sql));
            return GetType;
        }
        public string GetPurchaseBillType(string TPUID)
        {
            string GetType = string.Empty;
            string sql = "SELECT 1 FROM M_BRANCH WHERE BRID='" + TPUID + "'";
            GetType = Convert.ToString(db.GetSingleValue(sql));
            return GetType;
        }
        public DataTable Bind_InvoiceType(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_CHEAK_INVOICE_TYPE] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_InvoiceHeader_Print(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [dbo].[USP_RPT_SALE_INVOICE_HEADER_GST_PRINT] '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable Bind_InvoiceDetails_GST_Print(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_GST_PRINT] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Bind_StockTransferHeader_Print(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[SP_RPT_STOCKTRANSFER_HEADER_PRINT] '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable Bind_InvoiceDetails_CPC_GST_Print(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_CSD_GST_PRINT] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        /*Added By Sayan Dey On 18/06/2018*/
        public DataTable Bind_InvoiceDetails_CPC_GST_Print_BATCH(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_CSD_GST_PRINT_BATCH] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindST_Details_GST(string STNO, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKTRANSFER_DETAILS_GST] '" + STNO + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Bind_UserInfo(string USERID)  //** SP CHANGE ** IF ANY ISSUE OCCURE PLESE USE OLD SP.SP NAME [USP_GET_USERINFO_ERP]//
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_GET_USERINFO_ERP_WITH_KOLKATA] '" + USERID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable Bind_UserInfo_json()  
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_LOAD_TSI_ALL] ";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_UserInfo_tsisale(string USERID)
        {
            DataTable ds = new DataTable();
            string sql = "[dbo].[USP_GET_USERINFO_ASM] '" + USERID + "'";
            ds = db.GetData(sql);
            return ds;
        }

        public string GetUsertype(string USERID)
        {
            string Usertype = string.Empty;
            string sql = "SELECT USERTYPE FROM M_USER WHERE USERID='" + USERID + "'";
            Usertype = Convert.ToString(db.GetSingleValue(sql));
            return Usertype;
        }

        public DataTable BindPartywiseSale(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string UserID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE_ASMWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotwiseSale_asm(string fromdate, string todate, string stateid, string brand, string categoryid, string packsizeid, string bsid, int amt, string unitvalue, string UserID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_DEPOTWISE_ASMWISE] '" + fromdate + "','" + todate + "','" + stateid + "','" + brand + "','" + categoryid + "','" + packsizeid + "','" + bsid + "'," + amt + ",'" + unitvalue + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyDepotAndASM(string DepotID, string UserID) /*SUNNY*/
        {
            string sql = "  SELECT DISTINCT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID " +
                         "  WHERE A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + UserID + "'),','))" +
                         "  AND B.DEPOTID IN(SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductWiseSale(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string UserID, string ProductType)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE_ASMWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + UserID + "','" + ProductType + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHQSalesReport(string DepotID, string Bsid, string fromdate, string todate, string UserID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_HEADQUARTER_SALES_ASMWISE] '" + DepotID + "','" + Bsid + "','" + fromdate + "','" + todate + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStatebyUser(string USERID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT DISTINCT State_ID,State_Name FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID " +
                         " INNER JOIN M_REGION C ON A.STATEID=C.State_ID WHERE USERID='" + USERID + "' ORDER BY State_Name ";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegmentGTMT()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT  WHERE BSID IN ('7F62F951-9D1F-4B8D-803B-74EEBA468CEE','0AA9353F-D350-4380-BC84-6ED5D0031E24') ORDER BY NAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAddressBook(string USERID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_ADDRESS_BOOK] '" + USERID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHSNInvoice(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_HSNWISE_INVOICE_PRINT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockjournalheader(string ADJUSTMENTID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKJOURNAL_HEADER]'" + ADJUSTMENTID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockjournaldetails(string ADJUSTMENTID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKJOURNAL_DETAILS]'" + ADJUSTMENTID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindHSNStkTransfer(string TransferID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_HSNWISE_INVOICE_PRINT_TRANSFER] '" + TransferID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockPositionReport(string fromdate, string todate, string brandid, string catid, string depot, string pacsizeid)
        {
            string sql = "EXEC USP_RPT_StockPositionReport '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "','" + pacsizeid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindHQbyUSER(string USERID)
        {
            string sql = " EXEC usp_rpt_bind_HQbyUser '" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindGT()
        {
            string sql = "SELECT BSID as id,BSNAME as NAME FROM M_BUSINESSSEGMENT WHERE BSID='7F62F951-9D1F-4B8D-803B-74EEBA468CEE'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPartywiseSale(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string USERTYPE, string USERID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "','" + USERTYPE + "','" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindPartywiseSale_motherreport(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string USERTYPE, string USERID,string rpttype)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "','" + USERTYPE + "','" + USERID + "','"+ rpttype +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindForLedgerReport_DepotWise_SO(String DepotID, String userid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                  "LEFT JOIN Acc_AccountGroup B ON A.actGrpCode = B.CODE " +
                  "INNER JOIN Acc_Branch_Mapping M  ON A.Id = M.LedgerID WHERE BranchID IN(SELECT * FROM DBO.fnSplit('" + DepotID + "', ','))" +
                  " AND name <> ''  AND A.Id IN (SELECT CUSTOMERID FROM M_CUSTOMER_TSI_MAPPING WHERE TSI_ID='" + userid + "') ORDER BY LedgerName";

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMonth(string FinYear)
        {
            string sql = "SELECT DISTINCT MONTHID,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='M' AND FINYEAR='" + FinYear + "' ORDER BY MONTHID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotSales(string BSID, string GroupID, string ProductID, string MonthID, string Session)
        {
            string sql = "EXEC [USP_RPT_SALES_DEPOTWISE] '" + BSID + "','" + GroupID + "','" + ProductID + "','" + MonthID + "','" + Session + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bindwaybill(string fromdate, string todate, string depotid, string typeid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_BULKWAYBILL] '" + fromdate + "','" + todate + "','" + depotid + "','" + typeid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindCumulativeStockinHandDetails(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string StockDtls, string storelocation)
        {
            string sql = "EXEC USP_RPT_CUMULATIVE_DEPOTWISE_STOCK_IN_HAND '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + StockDtls + "','" + storelocation + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindForLedgerReport_DepotWise_ASM(String DepotID, String userid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                  "LEFT JOIN Acc_AccountGroup B ON A.actGrpCode = B.CODE " +
                  "INNER JOIN Acc_Branch_Mapping M  ON A.Id = M.LedgerID WHERE BranchID IN(SELECT * FROM DBO.fnSplit('" + DepotID + "', ','))" +
                  " AND name <> ''  AND A.Id IN (SELECT CUSTOMERID FROM M_CUSTOMER_TSI_MAPPING WHERE TSI_ID IN (SELECT USERID FROM M_USER WHERE REPORTINGTOID ='" + userid + "')) ORDER BY LedgerName";

            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSalesTaxSummary_Details_GST_CANCELLED(string fromdate, string todate, string depotid, string
                                                        SchemeStatus, string BSID, string type, string taxtypeid, string partyid, string InvoiceType, string DetailsType)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_GST_CANCELLED] '" + fromdate + "','" + todate + "','" +
                        depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "','" + InvoiceType + "','" + DetailsType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindSummaryDetails_GST(string fromdate, string todate, string depotid, string partyname, string InvoiceType,string VoucherType)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST_CANCELLED '" + fromdate + "','" + todate + "','" + depotid
                        + "','" + partyname + "','" + InvoiceType + "','" + VoucherType + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /*Added By Rajesh On 07.5.2018*/
        public DataTable BindGoodsInTranist(string asondt, string packsize)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_GOODS_IN_TRANSIT]'" + asondt + "','" + packsize + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet DebtorsOutstanding(string fromdate, string depotid, string finyr, string tag, string type, string showby, string BasedON)
        {
            string sql = "EXEC USP_RPT_DEBTORS_CNF_Ageing '" + fromdate + "','" + depotid + "','" + finyr + "','" + tag + "','" + type + "','" + showby + "','" + BasedON + "'";

            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindDepotForAgingReport()
        {
            string sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH ORDER BY BRNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_Primary_CUMULATIVE()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRPREFIX NOT IN ('MCPPL/HRD1','MCPPL/HRD2') ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Depot_Accounts_CUMULATIVE(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "' AND BRPREFIX NOT IN ('MCPPL/HRD1','MCPPL/HRD2')  ORDER BY BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindDepoWiseStockAndTranistQTY(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string storelocation)
        {
            string sql = "EXEC SP_DEPO_WISE_STOCK_AND_TRANSIT_QTY '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + storelocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerbyDepotAndASMHQ(string DepotID, string UserID) /*SUNNY*/
        {
            string sql = "  SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID INNER JOIN M_CUSTOMER_HQ_MAPPING HQ ON HQ.CUSTOMERID=A.CUSTOMERID " +
                         "  WHERE A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + UserID + "'),','))" +
                         "  AND B.DEPOTID IN(SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',','))  AND HQ.HQID IN( select *  from dbo.fnSplit((select HQLIST from M_USER WHERE USERID='" + UserID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransitInsurance(string fromdate, string todate)
        {
            string sql = "EXEC [SP_TRANSIT_INSURANCE] '" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*========Added By Sayan Dey On 24.05.2018==============*/
        public DataTable BindCategoryForPriceList()
        {
            string sql = " SELECT CATID,CATNAME FROM M_CATEGORY WHERE CATID NOT IN('085272A7-0522-4BBB-A672-7D9BFF8539D6','14D71FE5-FFB0-4405-BA7D-D7E6ADE45952'," +
                         " '6E9A0A94-98E7-41DE-9FCD-4E76C2C05690','9F8433DE-517E-459B-BF78-236A1487EB45','F469DD79-0DD7-412D-A533-860E42831BE1','FD677590-50FA-4985-999B-0EB19344EF17')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbyBSegmentForPriceList(string divid, string catid, string BSID)
        {
            string STRSQL = string.Empty;
            string STRSQL_CHECK = string.Empty;
            string Sql = string.Empty;
            string str = string.Empty;
            DataTable dt = new DataTable();

            string BS_SEGMENT = "";
            if (BSID == "0")
            {
                STRSQL = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT ";
            }
            else
            {

                STRSQL_CHECK = "SELECT TOP 1  PRODUCTALIAS AS NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE DIVID IN('" + divid + "') AND M_PRODUCT.CATID IN('" + catid + "') ";

                str = (string)db.GetSingleValue(STRSQL_CHECK);

                if (str == "" || str == null)
                {
                    STRSQL = " SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT  	WHERE 1=1  ";
                }
                else
                {

                    STRSQL = " SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID WHERE 1=1 ";


                    if (BSID != "0")
                    {

                        BS_SEGMENT = " AND BUSNESSSEGMENTID IN ('" + BSID + "')";

                    }
                }
            }
            string STRORDERBY = "ORDER BY PRODUCTALIAS";




            string DIVID = "";
            if (divid != "0")
            {
                if (BSID != "0")
                {

                    DIVID = " AND DIVID IN('" + divid + "')";
                }


            }

            string CATID = "";
            if (catid != "0")
            {
                if (BSID != "0")
                {

                    CATID = " AND M_PRODUCT.CATID IN('" + catid + "')";
                }



            }

            Sql = STRSQL + BS_SEGMENT + DIVID + CATID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;

        }

        public DataTable BindBrandForPriceList()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION WHERE DIVID NOT IN ('E6B2F631-7237-4B97-BC33-B6FCF0B9075B') ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBrandForFGRequirement()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION WHERE DIVID IN ('12E3F880-C465-4C0E-80B8-42E8DD05E51D','1865C411-3F93-4F03-9FE5-154C93A4CB7C','1AC1960B-5C21-4C67-93D4-D38D1B3FAEB2')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGroupbyidPriceList(string BSID, string DepotID)
        {
            string sql = " SELECT DISTINCT A.GROUPID AS DIS_CATID,A.GROUPNAME AS DIS_CATNAME,A.BUSINESSSEGMENTID " +
                         " FROM M_CUSTOMER AS A " +
                         " INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                         " ON A.CUSTOMERID = B.CUSTOMERID " +
                         " WHERE A.BUSINESSSEGMENTID='" + BSID + "' " +
                         " AND B.DEPOTID='" + DepotID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*======================================================*/

        public DataTable BindSSDistName(string userid)
        {
            string sql = "SELECT DISTINCT A.CUSTOMERID,A.CUSTOMERNAME FROM  M_CUSTOMER A WHERE PARENT_CUST_ID IN (SELECT CAST(USERID AS VARCHAR(20)) FROM M_USER WHERE REFERENCEID=(SELECT TARGETSALESID FROM P_APPMASTER)) AND A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + userid + "'),',')) ORDER BY A.CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSSDistASM_SO(string userid)
        {
            string sql = "SELECT DISTINCT A.CUSTOMERID,A.CUSTOMERNAME FROM  M_CUSTOMER A WHERE A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + userid + "'),',')) and custype_id in('826D656F-353F-430F-8966-4FE6BE3F67ED') ORDER BY A.CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSSDist(string userid)
        {
            string sql = "SELECT DISTINCT USERID,DISTRIBUTORLIST FROM M_USER WHERE USERID='" + userid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerSSDist(string refid)
        {
            string sql = "SELECT DISTINCT A.CUSTOMERID,A.CUSTOMERNAME FROM  M_CUSTOMER A WHERE PARENT_CUST_ID IN (select userid from m_user where referenceid in ('" + refid.Replace(",", "','") + "')) ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindSalesTaxSummary_Details_GST_TSALES(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_DMS_GST_TSALES] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindSummaryDetails_GST_TSALES(string fromdate, string todate, string depotid, string partyname)
        {
            string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_DMS_GST_TSALES'" + fromdate + "','" + todate + "','" + depotid
             + "','" + partyname + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }


        public DataSet BindSalesTaxSummary_Details_GST_SO(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, string type, string taxtypeid, string partyid)
        {
            string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_GST_SO] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindCostCategory()
        {
            string sql = "SELECT COSTCATID, COSTCATNAME  FROM M_COST_CATEGORY ORDER BY COSTCATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindForLedger_ACCGROUPWise(String CODE)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "SELECT ID AS LEDGERID,NAME AS LEDGERNAME FROM ACC_ACCOUNTSINFO AS A " +
                  "INNER JOIN ACC_ACCOUNTGROUP AS B ON A.ACTGRPCODE=B.CODE WHERE B.CODE IN (SELECT * from dbo.fnSplit('" + CODE + "',',')) ORDER BY LEDGERNAME";

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepartment()
        {
            string sql = "SELECT DEPTID AS ID, DEPTNAME  FROM M_DEPARTMENT ORDER BY DEPTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivison()
        {
            string sql = "SELECT DIVID , DIVNAME  FROM M_DIVISION ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductBrandWise(string DIVID)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS FROM M_PRODUCT WHERE DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCostCenterBalance(string FromDate, string ToDate, string RegionID, string COSTCATEGORYID, string ACCGROUPID, string LEDGERID, string COSTCENTERID, string DEPARMENTID, string SEGMENTID, string PRODUCTID)
        {
            string sql = "EXEC [USP_COSTCENTERSUMMARY] '" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + RegionID + "','" + COSTCATEGORYID + "','" + ACCGROUPID + "','" + LEDGERID + "','" + COSTCENTERID + "','" + DEPARMENTID + "','" + SEGMENTID + "','" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*Modified By Sunny Samanta On 01.06.2018*/
        public DataTable BindCostCenterVoucherDetailsV2(string FromDate, string ToDate, string RegionID, string CostCenterID, string LedgerId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_COSTCENTERDETAILS] '" + FromDate + "','" + ToDate + "','" + RegionID + "','" + CostCenterID + "','" + LedgerId + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPartywiseInvoicewiseSales(string fromdate, string todate)
        {
            string sql = "EXEC [USP_RPT_PARTYWISE_INVOICEWISE_SALES] '" + fromdate + "','" + todate + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindDepotwiseSales_withTax(string fromdate, string todate, string depotid, string bsid, string brandid, string catid, string productid)
        {

            string sql = "EXEC [sp_DepotwiseSales_withTax]'" + fromdate + "','" + todate + "','" + depotid + "','" + bsid + "','" + brandid + "','" + catid + "','" + productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindDepotwiseSales_withTax_json(string fromdate, string todate, string depotid, string bsid, string brandid, string catid, string productid, string type,string RptType)
        {

            string sql = "EXEC [sp_DepotwiseSales_withTax]'" + fromdate + "','" + todate + "','" + depotid + "','" + bsid + "','" + brandid + "','" + catid + "','" + productid + "','" + type + "','"+ RptType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable LoadPartyBasedonTSI(string depotid, string bsid, string stateid, string tsiid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STATEWISE_PARTY_VER2] '" + depotid + "','" + bsid + "','" + stateid + "','" + tsiid + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataSet getvoucherid(string vtype)
        {

            DataSet dt = new DataSet();
            string sql = "select id from Acc_VoucherTypes where VoucherName ='" + vtype + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;

        }

        public string GetStartEndDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));

            return STARTDATE;
        }

        public string GetStartDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));

            return STARTDATE;
        }

        /*Modified By Sunny Samanta On 05.06.2018*/
        public DataTable BindLedgerForCostCenterVoucher()
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' AND costcenter ='Y' ORDER BY LedgerName";
            dt = db.GetData(sql);
            return dt;
        }
        /*Added By Sunny Samanta On 05.06.2018*/
        public DataTable BindForLedger_ACCGROUPWise_CostCenter_Summary(String CODE)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "SELECT ID AS LEDGERID,NAME AS LEDGERNAME FROM ACC_ACCOUNTSINFO AS A " +
                  "INNER JOIN ACC_ACCOUNTGROUP AS B ON A.ACTGRPCODE=B.CODE WHERE B.CODE IN (SELECT * from dbo.fnSplit('" + CODE + "',',')) AND costcenter ='Y' ORDER BY LEDGERNAME";

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLedgerReportForASM(string ledgerid, string fromdate, string todate, string Sessionid)
        {

            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRAILBALANCE_SO] '" + ledgerid + "', '" + fromdate + "','" + todate + "','" + Sessionid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Trial Balance ASM

        public DataTable BINDSOFORASM(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                sql = "SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN (SELECT * FROM DBO.FNSPLIT('" + UserID + "',',')) AND USERTYPE='9BF42AA9-0734-4A6A-B835-0885FBCF26F5'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataTable BindDistributorso(string soid)
        {
            string sql = " SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_TSI_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID " +
                         " WHERE B.ISACTIVE='True' AND A.TSI_ID IN (SELECT * FROM DBO.FNSPLIT('" + soid + "',','))  ORDER BY B.CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion


        #region NCC report

        public DataTable BindASM(string depotid)
        {
            string sql = "SELECT A.USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER A INNER JOIN  M_TPU_USER_MAPPING B ON A.USERID=B.USERID AND" +
                            " TPUID IN (SELECT * FROM DBO.FNSPLIT('" + depotid + "',','))" +
                            " AND A.USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindSO(string asmid)
        {
            string sql = "EXEC USP_SOLIST_ASMWISE '" + asmid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTsi(string soid)
        {
            string sql = "EXEC USP_TSILIST_SOWISE '" + soid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTsiBasedonSO(string soid)
        {
            string sql = "EXEC USP_TSILIST_SOWISE '" + soid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSO_TSISALE(string asmid, String USERID)
        {
            string sql = "[dbo].[USP_GET_USERINFO_SO]'" + asmid + "','" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorncc(string soid)
        {
            string sql = " SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_TSI_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID " +
                         " WHERE A.TSI_ID IN (SELECT * FROM DBO.FNSPLIT('" + soid + "',',')) ORDER BY B.CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindASMNCC(string depotid, string bsid)
        {
            string sql = "SELECT DISTINCT A.USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER A INNER JOIN  M_TPU_USER_MAPPING B ON A.USERID=B.USERID" +
                         " INNER JOIN M_USER_BS_MAP AS C ON A.USERID=C.USERID WHERE C.BSID IN (SELECT * FROM DBO.FNSPLIT('" + bsid + "',','))    " +
                            " AND TPUID IN (SELECT * FROM DBO.FNSPLIT('" + depotid + "',','))" +
                            " AND A.USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindClaimType()
        {
            string sql = "SELECT CLAIM_ID,CLAIM_TYPE FROM M_CLAIM_TYPE ORDER BY CLAIM_TYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindNccReport(string FRMDATE, string TODATE, string BUSINESSSEGMENTID, string DEPOTID, string ASMID, string SOID, string PARTYID, string CLAIMID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_NCCSTATUS]'" + FRMDATE + "','" + TODATE + "','" + BUSINESSSEGMENTID + "','" + DEPOTID + "','" + ASMID + "','" + SOID + "','" + PARTYID + "','" + CLAIMID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion



        public DataTable BindUserTypASMWISE(string UserID)
        {

            string sql = " EXEC USP_RPT_LOAD_USERTYPE '" + UserID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindHeadQuarderASM(string UserID)
        {
            string sql = " EXEC USP_RPT_LOAD_HQ '" + UserID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*=========Added By Avishek On 10-06-2016==============*/
        public DataTable Bind_InvoiceDetails_Corporate_GST_Print(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_CORPORATE_GST_PRINT] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        /*====================================================*/
        /*Add By Rajeev (14-06-2018)*/
        public DataSet BindFreightWithoutDetails(string fromdate, string todate, string depotid, string Type)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_FREIGHT_REPORT_WITHOUTDETAILS] '" + fromdate + "','" + todate + "','" + depotid + "','" + Type + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /*Add by Subhodip on 19.06.2018*/
        public DataTable BindMigrationCodeReport(string fromdate, string todate, string DISTRIBUTORid, string PRODUCTID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_MIGRATION] '" + fromdate + "','" + todate + "','" + DISTRIBUTORid + "','" + PRODUCTID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepoForCostcenterReport()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID <>'0EEDDA49-C3AB-416A-8A44-0B9DFECD6670'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindBalanceSheetV2(string ToDate, string RegionID, string FinYear, string Type,string Reporttype)
        {
            string sql = "EXEC [USP_RPT_BLSHEET] '" + RegionID + "','" + ToDate.ToString() + "','" + FinYear + "','" + Type + "',"+ 0 +",'"+ Reporttype + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindProfitLossV2(string ToDate, string RegionID, string FinYear, string Type, string Reporttype)
        {
            string sql = "EXEC [USP_RPT_PLSHEET] '" + RegionID + "','" + ToDate.ToString() + "','" + FinYear + "','" + Type + "'," + 0 + ",'" + Reporttype + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindClaimSummaryV2(string fromdate, string todate, string ID, string TAG)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_DEPOTWISE_CLAIM_SUMMARYV2] '" + fromdate + "','" + todate + "','" + ID + "','" + TAG + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindBusinessegmentForClaim()
        {
            string sql = " SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN ('0AA9353F-D350-4380-BC84-6ED5D0031E24','7F62F951-9D1F-4B8D-803B-74EEBA468CEE','A18FE04D-057C-4A88-9204-5FE1613626C9') " +
                         " ORDER BY NAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNarrtionForClaim(string CLAIM_TYPE, string FinYear)
        {
            string sql = string.Empty;

            if (CLAIM_TYPE == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE   FINYEAR='" + FinYear + "'  ORDER BY CLAIM_NARRATION ASC ";
            }
            else
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',',')) AND  FINYEAR='" + FinYear + "'  						ORDER BY CLAIM_NARRATION ASC ";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindNarrtionForChainandclaim(string CHAIN, string CLAIM_TYPE, string FinYear)
        {
            string sql = string.Empty;
            if (CHAIN == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',','))  AND FINYEAR='" + FinYear + "' ORDER BY CLAIM_NARRATION ASC ";
            }
            else
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',',')) AND GROUPID IN (SELECT * FROM DBO.FNSPLIT('" + CHAIN + "',',')) AND FINYEAR='" + FinYear + "' ORDER BY CLAIM_NARRATION ASC ";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindChainTypeClaim(string BUSINESSSEGMENTID)
        {
            string sql = string.Empty;
            if (BUSINESSSEGMENTID == "0AA9353F-D350-4380-BC84-6ED5D0031E24")
            {
                sql = " SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY WHERE  BUSINESSSEGMENTID ='" + BUSINESSSEGMENTID + "' " +
                            " ORDER BY DIS_CATNAME ASC ";
            }
            else if (BUSINESSSEGMENTID == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE")
            {
                sql = " SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY WHERE  BUSINESSSEGMENTID ='" + BUSINESSSEGMENTID + "' " +
                            " ORDER BY DIS_CATNAME ASC ";
            }
            else
            {
                sql = "select '' AS DIS_CATID ,'' AS DIS_CATNAME";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimStatusForSchemewise()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT ID,CLAIMSTATUSNAME FROM M_CLAIM_STATUS WHERE ID='5' ORDER BY CLAIMSTATUSNAME ";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindClaimSchemewisereport(string fromdate, string todate, string depotid, string status, string businesssegment, string claimnarration, string claimtype, string chain, string NARRATIONNAME)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_RPT_CLAIM_PARTYWISE_sCHEMEWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + status + "','" + businesssegment + "','" + claimnarration + "','" + claimtype + "','" + chain + "','" + NARRATIONNAME + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindTSIWiseSale(string FromDate, string ToDate, string SOID)
        {
            string sql = "EXEC [USP_RPT_SALE_REPORT_TSIWISE] '" + FromDate + "','" + ToDate + "','" + SOID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //  Added by HPDAS on 28.06.18
        #region Fetch Ledger Group Wise
        public DataTable BindLedgerGroupWise(string GroupID)
        {
            string sql = " SELECT ID,NAME FROM ACC_ACCOUNTSINFO WHERE ACTGRPCODE IN (SELECT *  FROM DBO.FNSPLIT('" + GroupID + "',',')) ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindLedger()
        {
            string sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' ORDER BY LedgerName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPartyOutstanding(string FromDate, string ToDate, string DepotID, string GroupID, string LedgerID)
        {
            string sql = "EXEC [SP_ACC_INVOICE_OUTSTANDING_DETAILS] '" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + DepotID + "','" + GroupID + "','" + LedgerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // End Adding 28.06.18


        /*Added by sayan dey 04.07.2018*/
        public DataTable BindSalereturnDetails(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROSS_RETURN_DETAILS_GST_PRINT] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindSalereturnHeader(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROSS_RETURN_HEADER_GST_PRINT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindSalereturnFooter(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROOS_RETURN_FOOTER_PRINT_GST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDistributorReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_DISTRIBUTORINFO_SALERETURN] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_ReturnFromDepot(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALERETURN_FROMDEPOT_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_Returntype(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_CHEAK_RETURN_TYPE] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNarrtionForClaimBS(string CLAIM_TYPE, string FinYear, string BUSINESSEGMENTID, string CHAINID)
        {
            string sql = string.Empty;

            if (CLAIM_TYPE == "" && BUSINESSEGMENTID == "" && CHAINID == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE   FINYEAR='" + FinYear + "'  ORDER BY CLAIM_NARRATION ASC ";
            }
            else if (BUSINESSEGMENTID == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',',')) AND  FINYEAR='" + FinYear + "'  ORDER BY CLAIM_NARRATION ASC ";
            }
            else if (CLAIM_TYPE == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE   FINYEAR='" + FinYear + "' AND BSID IN (SELECT * FROM DBO.FNSPLIT('" + BUSINESSEGMENTID + "',','))  ORDER BY CLAIM_NARRATION ASC ";
            }
            else if (CHAINID == "")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',',')) AND  FINYEAR='" + FinYear + "' AND BSID IN (SELECT * FROM DBO.FNSPLIT('" + BUSINESSEGMENTID + "',',')) ORDER BY CLAIM_NARRATION ASC ";
            }
            else
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION FROM M_CLAIM_NARRATION WHERE CLAIM_TYPE IN (SELECT * FROM DBO.FNSPLIT('" + CLAIM_TYPE + "',',')) AND  FINYEAR='" + FinYear + "' AND BSID IN (SELECT * FROM DBO.FNSPLIT('" + BUSINESSEGMENTID + "',','))" +
                       " AND GROUPID IN (SELECT * FROM DBO.FNSPLIT('" + CHAINID + "',',')) ORDER BY CLAIM_NARRATION ASC ";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Add Rajeev (10-07-2018)
        public DataTable BSWISE_SALEREPORT(string fromdate, string todate, string depot)
        {
            string sql = "EXEC [USP_RPT_BSWISE_SALEREPORT] '" + fromdate + "','" + todate + "','" + depot + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindBS()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN ('7F62F951-9D1F-4B8D-803B-74EEBA468CEE')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataSet Bind_UserInfoFORNCC(string USERID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_GET_USERINFOFORNCC] '" + USERID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BINDASMBYRSM(string rsmid)
        {
            string sql = "SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN (SELECT * FROM DBO.FNSPLIT('" + rsmid + "',',')) AND USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRSMBYDEPOT(string depotid)
        {
            string sql =    " SELECT DISTINCT A.USERID,LTRIM(RTRIM(RTRIM(RTRIM(A.FNAME) + ' ' + RTRIM(A.MNAME)) + ' ' + A.LNAME)) AS USERNAME FROM M_USER A WITH(NOLOCK) " +
                            " INNER JOIN M_TPU_USER_MAPPING B WITH(NOLOCK) ON A.USERID=B.USERID AND" +
                            " TPUID IN (SELECT ITEM FROM DBO.FNSPLIT('" + depotid + "',','))" +
                            " AND A.USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindZSMBYDEPOT(string depotid)
        {
            string sql =    " SELECT DISTINCT A.USERID,LTRIM(RTRIM(RTRIM(RTRIM(A.FNAME) + ' ' + RTRIM(A.MNAME)) + ' ' + A.LNAME)) AS USERNAME FROM M_USER A WITH(NOLOCK) " +
                            " INNER JOIN M_TPU_USER_MAPPING B WITH(NOLOCK) ON A.USERID=B.USERID AND" +
                            " TPUID IN (SELECT ITEM FROM DBO.FNSPLIT('" + depotid + "',','))" +
                            " AND A.USERTYPE='FE89F22F-CC62-4E7D-B68A-2FE460DC2470'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Add Rajeev (17-07-2018)
        public DataTable BindFGBrand()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION WHERE DIVID NOT IN ('E6B2F631-7237-4B97-BC33-B6FCF0B9075B','6B869907-14E0-44F6-9BA5-57F4DA726686')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategoryBrandWise(string BrandID)
        {
            string sql = " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY AS A " +
                         " INNER JOIN M_PRODUCT AS B ON A.CATID = B.CATID " +
                         // " WHERE B.DIVID IN (SELECT * FROM DBO.fnSplit('" + BrandID + "',',')";
                         " WHERE B.DIVID IN ('1865C411-3F93-4F03-9FE5-154C93A4CB7C','1AC1960B-5C21-4C67-93D4-D38D1B3FAEB2','12E3F880-C465-4C0E-80B8-42E8DD05E51D')";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbyBSegment(string divid, string catid)
        {
            string STRSQL = string.Empty, sql = string.Empty;
            string STRSQL_CHECK = string.Empty;
            string Sql = string.Empty;
            string str = string.Empty;
            DataTable dt = new DataTable();

            sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B ON A.ID = B.PRODUCTID" +
                  " WHERE A.DIVID IN('" + divid + "') AND A.CATID IN('" + catid + "')";

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepoWiseAgeingStock(string AsonDate, string Depotid, string Brand, string Category, string Productid)
        {
            string sql = "EXEC USP_RPT_AGEINGWISE_STOCK '" + AsonDate + "','" + Depotid + "','" + Brand + "','" + Category + "','" + Productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFGProduct()
        {
            string sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT " +
                         " WHERE TYPE='FG' AND DIVID NOT IN('6B869907-14E0-44F6-9BA5-57F4DA726686','E6B2F631-7237-4B97-BC33-B6FCF0B9075B')" +
                         " ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        public DataSet BindBalanceSheetV2_Consolidated(string ToDate, string FinYear, string Details, string Type)
        {
            string sql = "EXEC [USP_RPT_BLSHEET_CONSOLIDATED] '" + ToDate.ToString() + "','" + FinYear + "','" + Details + "','" + Type + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /******************************************************************************/

        public DataTable BindCustomerbyDepotForPortalReport(string DepotID)
        {
            string sql = String.Empty;
            if (DepotID == "0")
            {
                sql = "  SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_BRANCH B ON A.DEPOTID=B.BRID WHERE B.BRANCHTAG='D' AND A.DEPOTID IS NOT NULL ORDER BY CUSTOMERNAME";
            }
            else
            {
                sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindProfitLossV2_Consolidated(string ToDate, string FinYear, string Details, string Type)
        {
            string sql = "EXEC [USP_RPT_PLSHEET_CONSOLIDATED] '" + ToDate.ToString() + "','" + FinYear + "','" + Details + "','" + Type + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindDepotAll(string UserType, string UserID)
        {

            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    sql = " SELECT DISTINCT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE  BRID NOT IN ('24E1EF07-0A41-4470-B745-9E4BA164C837','0EEDDA49-C3AB-416A-8A44-0B9DFECD6670') ORDER BY BRPREFIX ";
                }
                else
                {
                    sql = " SELECT  BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "' AND BRID NOT IN ('24E1EF07-0A41-4470-B745-9E4BA164C837','0EEDDA49-C3AB-416A-8A44-0B9DFECD6670') ORDER BY BRPREFIX ";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataTable BindCostCenterReport(string FromDate, string ToDate, string SearchTag, string TSID, string AccGroupID, string BRID, string CCID, string LedgerId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_COSTCENTER_REPORT] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + AccGroupID + "','" + BRID + "','" + CCID + "','" + LedgerId + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Add By Rajeev(27-07-2018)
        public DataTable UserWiseMappingBS(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT A.BSID AS ID,A.BSNAME AS NAME FROM M_BUSINESSSEGMENT AS A " +
                      " INNER JOIN M_USER_BS_MAP AS B ON B.BSID=A.BSID" +
                      " WHERE B.USERID='" + userid + "' ORDER BY NAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        #endregion
        public DataTable BindCostCenterReportV2(string FromDate, string ToDate, string SearchTag, string TSID, string AccGroupID, string BRID, string CCID, string LedgerId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_COSTCENTER_REPORT_V2] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + AccGroupID + "','" + BRID + "','" + CCID + "','" + LedgerId + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFinalDSRSummaryReport(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype, string TSID)
        {
            string sql = "EXEC [USP_RPT_FINAL_DSR_SUMMARY_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + Amount + "','" + AmtType + "','" + EntryType + "','" + usertype + "','" + TSID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindFinalDSRSummaryReport2(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype, string TSID)
        {
            string sql = "EXEC [USP_RPT_FINAL_DSR_SUMMARY_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + Amount + "','" + AmtType + "','" + EntryType + "','" + usertype + "','" + TSID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindTSI(string asmid)
        {
            string sql = "SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN (SELECT * FROM DBO.FNSPLIT('" + asmid + "',',')) AND USERTYPE='DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_UserInfoFForAll(string USERID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_GET_USERINFO] '" + USERID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        #region Add By Rajeev (09-08-2018)
        public DataTable Bind_distributorReport(string FromDate, string ToDate, string DepotID, string BSID, string ASMID)
        {
            string sql = "EXEC [USP_GET_DISTIBUTOR_DETAILS] '" + FromDate + "','" + ToDate + "','" + BSID + "','" + DepotID + "','" + ASMID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindASMDepotWise(string depotid)
        {
            string sql = " SELECT DISTINCT A.USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER A INNER JOIN  M_TPU_USER_MAPPING B ON A.USERID=B.USERID" +
                         //" INNER JOIN M_USER_BS_MAP AS C ON A.USERID=C.USERID WHERE C.BSID IN (SELECT * FROM DBO.FNSPLIT('" + bsid + "',','))    " +
                         " INNER JOIN M_USER_BS_MAP AS C ON A.USERID=C.USERID " +
                         " AND TPUID IN (SELECT * FROM DBO.FNSPLIT('" + depotid + "',','))" +
                         " AND A.USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Added By HPDAS(09-08-2018)
        public DataTable BindPartyOutstanding_Opening(string FromDate, string ToDate, string DepotID, string GroupID, string LedgerID)
        {
            string sql = "EXEC [SP_ACC_INVOICE_OUTSTANDING_DETAILS_OPENING] '" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + DepotID + "','" + GroupID + "','" + LedgerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Add By HPDAS(11-08-2018)
        public DataTable BindPartyOutstanding_OnAccReceipts(string FromDate, string ToDate, string DepotID, string GroupID, string LedgerID)
        {
            string sql = "EXEC [SP_ACC_INVOICE_OUTSTANDING_DETAILS_ON_AC_RECEIPTS] '" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + DepotID + "','" + GroupID + "','" + LedgerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable ReceiptDetailsAgainstBill(string RegionID, string GroupID, string LedgerID, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RECEIPTDETAILS_AGAINST_BILL] '" + GroupID + "','" + RegionID + "','" + LedgerID + "','" + FromDate + "','" + ToDate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        /*ADDED BY DHANANJOY MONDAL ON 23.08.2018*/
        public DataTable BindLedgerName(string GroupID, string DeportID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT AI.ID,AI.name FROM Acc_Branch_Mapping BM INNER JOIN Acc_AccountsInfo AI ON BM.LedgerID=AI.Id INNER JOIN Acc_AccountGroup AG ON AI.actGrpCode=AG.Code WHERE BM.BranchID='" + DeportID + "' and AG.Code='" + GroupID + "' ORDER BY AI.name ";
            dt = db.GetData(sql);
            return dt;
        }
        #region Add By Rajeev (17-08-2018)
        public DataTable Bind_InvoiceData(string Fromdt, string Todt, string AsmID, string SoID, string DistibutorID, string TsiID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_GET_DISTIBUTOR_INVOICE] '" + Fromdt + "','" + Todt + "','" + AsmID + "','" + SoID + "','" + DistibutorID + "','" + TsiID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_OrderData(string Fromdt, string Todt, string AsmID, string SoID, string DistibutorID, string TsiID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_GET_DISTIBUTOR_ORDER] '" + Fromdt + "','" + Todt + "','" + AsmID + "','" + SoID + "','" + DistibutorID + "','" + TsiID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_UserReport(string FromDate, string ToDate, string Department)
        {
            string sql = "EXEC [USP_GET_USER_DETAILS] '" + FromDate + "','" + ToDate + "','" + Department + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataSet BindPrimarySalesTaxSummary_InvoiceReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn, string CustomerID, string USERID, string InvoiceType)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT_INVOICE] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "','" + CustomerID + "','" + USERID + "','" + InvoiceType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataSet BindInvoiceDetialsReport_GST_Invoice(string InvoiceID)
        {
            string sql = "EXEC [USP_RPT_SALE_INVOICE_DETAILS_GST_PRIMARY_INVOICE_DETAILS_INVOICE] '" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        /*ADDED BY DHANANJOY MONDAL ON 23.08.2018*/
        public DataTable BindAccountsGroupName(string DeportID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DISTINCT AG.CODE,AG.grpName FROM Acc_Branch_Mapping BM INNER JOIN Acc_AccountsInfo AI ON BM.LedgerID=AI.Id " +
                " INNER JOIN Acc_AccountGroup AG ON AI.actGrpCode=AG.Code WHERE BM.BranchID='" + DeportID + "' ORDER BY AG.grpName";
            dt = db.GetData(sql);
            return dt;
        }

        /*ADDED BY SAYAN DEY ON 24.08.2018*/
        public DataTable Bind_Headerexport(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_EXPORT_INVOICE_HEADER_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        /*Added By Arti Agarwal On on 21.08.2018*/
        public string OfflineNewRecord(string BRID)
        {
            string qury = " SELECT ISNULL(OFFLINE,'') AS OFFLINE FROM M_BRANCH WHERE BRID='" + BRID + "'";
            string OFFLINE = (string)db.GetSingleValue(qury);
            return OFFLINE;

        }
        public string BindAppMasterStatus()
        {
            string sql = "select offline from P_appmaster ";
            string OFFLINE1 = (string)db.GetSingleValue(sql);
            return OFFLINE1;

        }

        public DataTable BindForcastVsSalesStock(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string storelocation)
        {
            string sql = "EXEC USP_RPT_FORCAST_VS_SALE_N_STOCK '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + storelocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindForcastVsSalesStock_json(string depotid, string packsize, string brand, string category, string curdate, string storelocation,string MrpType,string RptType)
        {
            string sql = "EXEC [USP_RPT_FORCAST_VS_SALE_N_STOCK_JSON] '" + depotid + "','" + packsize + "','" + brand + "','" + category + "','" + curdate + "','" + storelocation + "','"+ MrpType + "','"+ RptType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        /*ADDED BY SAYAN DEY ON 15.11.2018*/
        public DataSet Bind_secondarydump_JSON(string fromdate, string todate, string depotid)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC [USP_RPT_SECONDARY_SALES_DUMP_DMS_DATEWISE_DEPOTWISE] '" + fromdate + "','" + todate + "','" + depotid + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        /************************************/
        /*****************************************/

        /*ADDED BY SAYAN DEY ON 28.08.2018*/

        #region Tax_Report

        public DataTable BindTax()
        {
            string sql = "SELECT ID,NAME FROM M_TAX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindcategorywisetax(string tax)
        {
            string sql = "EXEC [SP_RPT_CATEGORY_TAX] '" + tax + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindExceptionwisetax(string tax)
        {
            string sql = "EXEC [SP_RPT_EXCEPTION_TAX] '" + tax + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDefaulttax(string tax)
        {
            string sql = "EXEC [SP_RPT_CATEGORY_TAX_DEFAULT] '" + tax + "'";
            DataTable dt2 = new DataTable();
            dt2 = db.GetData(sql);
            return dt2;
        }

        public DataTable Bindtransven(string str)
        {
            string sql = " SELECT DISTINCT NAME,TYPE,PERCENTAGE,STATUS,ID FROM ( " +
                         " SELECT A.NAME AS NAME,'TRANSPORTER' AS TYPE, B.PERCENTAGE AS PERCENTAGE," +
                         " CASE WHEN A.ISAPPROVED IS NULL THEN 'In-Active' " +
                         " WHEN A.ISAPPROVED =''  THEN 'In-Active' " +
                         " WHEN A.ISAPPROVED ='N' THEN 'In-Active' " +
                         " ELSE  'Active' END AS STATUS,CAST(ID AS VARCHAR(50)) AS ID  " +
                         " FROM M_TPU_TRANSPORTER A INNER JOIN M_TAX_GROUP_MAPPING B ON A.COMPANYTYPEID=B.GROUPID WHERE A.COMPANYTYPEID='" + str + "' " +
                         " UNION ALL" +
                         " SELECT A.VENDORNAME AS NAME,'VENDOR' AS TYPE, B.PERCENTAGE AS PERCENTAGE," +
                         " CASE WHEN A.ISAPPROVED IS NULL THEN 'In-Active' " +
                         " WHEN A.ISAPPROVED =''  THEN 'In-Active' " +
                         " WHEN A.ISAPPROVED ='N' THEN 'In-Active' " +
                         " ELSE  'Active' END AS STATUS,CAST(VENDORID AS VARCHAR(50)) AS ID " +
                         " FROM M_TPU_VENDOR A INNER JOIN M_TAX_GROUP_MAPPING B ON A.COMPANYTYPEID=B.GROUPID WHERE A.COMPANYTYPEID='" + str + "')TEMP " +
                         " ORDER BY TYPE,NAME ASC";
            DataTable dt2 = new DataTable();
            dt2 = db.GetData(sql);
            return dt2;
        }

        public DataTable Bindtransven1(string str)
        {
            string sql = " SELECT A.TRANSPORTERBILLNO,TOTALBILLAMOUNT,B.TOTALTDS, " +
                          " CASE WHEN B.TDSPECENTAGE IS NULL " +
                          " THEN CONVERT(DECIMAL(10,2),100*B.TOTALTDS/TOTALBILLAMOUNT)" +
                          " ELSE B.TDSPECENTAGE END AS TDSPECENTAGE " +
                          " FROM T_TRANSPORTER_BILL_HEADER A INNER JOIN T_TRANSPORTER_BILL_FOOTER B ON A.TRANSPORTERBILLID=B.TRANSPORTERBILLID " +
                          " INNER JOIN M_TPU_TRANSPORTER C ON A.TRANSPORTERID = C.ID " +
                          " WHERE A.TRANSPORTERID='" + str + "'";
            DataTable dt2 = new DataTable();
            dt2 = db.GetData(sql);
            return dt2;
        }

        #endregion

        /***********************************************/
        #region Add By HPDAS(30-08-2018)
        public DataTable BindCustomerbyDepotV2(string DepotID)
        {
            string sql = "SELECT DISTINCT B.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID AND B.CUSTYPE_ID NOT IN ('B9343E49-D86B-49EA-9ACA-4F4F7315EC96') WHERE A.DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY A.CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerbyIdV2(string ID)
        {
            string sql = " SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN ('B9343E49-D86B-49EA-9ACA-4F4F7315EC96') AND CUSTOMERID = '" + ID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerV2()
        {
            string sql = "SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN ('B9343E49-D86B-49EA-9ACA-4F4F7315EC96') ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorSalesHierarchy(string userid)
        {
            string sql = " SELECT B.CUSTOMERID,B.CUSTOMERNAME FROM M_USER A INNER JOIN M_CUSTOMER B ON B.CUSTOMERID IN (SELECT * FROM DBO.FNSPLIT(A.DISTRIBUTORLIST,',')) " +
                         " WHERE USERID='" + userid + "' AND B.ISACTIVE='True' ORDER BY B.CUSTOMERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorForOtherUser(string depotid)
        {
            string sql = " SELECT DISTINCT B.BRID,B.BRNAME,D.CUSTOMERID,D.CUSTOMERNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON B.BRID=A.TPUID INNER JOIN " +
                        " M_CUSTOMER_DEPOT_MAPPING C ON C.DEPOTID=B.BRID INNER JOIN M_CUSTOMER D ON D.CUSTOMERID=C.CUSTOMERID INNER JOIN M_USER E ON E.USERTYPE=D.CUSTYPE_ID " +
                        " WHERE D.ISACTIVE='True' AND B.BRID='" + depotid + "' ORDER BY D.CUSTOMERNAME ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorCollection(string Fromdate, string Todate, string Finyear, string RegionID, string DISTRIBUTORID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DISTRIBUTOR_COLLECTION_REPORT] '" + Fromdate + "','" + Todate + "','" + RegionID + "','" + DISTRIBUTORID + "', '" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Add by Rajeev(03-09-2018)
        public DataTable BindFGCategory()
        {
            string sql = " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY AS A" +
                         " INNER JOIN M_PRODUCT AS B ON A.CATID = B.CATID and B.TYPE='FG' AND DIVID NOT IN('6B869907-14E0-44F6-9BA5-57F4DA726686')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerbyDepotAndSo(string DepotID, string SoName)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING AS A" +
                         " INNER JOIN M_CUSTOMER_TSI_MAPPING AS B ON A.CUSTOMERID=B.CUSTOMERID" +
                         " INNER JOIN M_CUSTOMER AS C ON C.CUSTOMERID=A.CUSTOMERID" +
                         " WHERE A.DEPOTID IN (SELECT * FROM DBO.FNSPLIT('" + DepotID + "',',')) " +
                         " AND B.TSI_ID='" + SoName + "'" +
                         " AND C.ISACTIVE='True'" +
                         " ORDER BY A.CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        /*Added By Rajeev Kumar On 28.09.2018*/
        public DataTable BindPO(string BSID)
        {
            string sql = " SELECT REFERENCESALEORDERNO FROM T_SALEORDER_HEADER WHERE BSID='" + BSID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCpcCsdReport(string Fromdate, string Todate, string Bsid, string Depotid, string Poid)
        {
            string sql = "EXEC USP_RPT_CPCCSD_POREPORT '" + Fromdate + "','" + Todate + "','" + Bsid + "','" + Depotid + "','" + Poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*Added By H.P.Das On 28.09.2018*/
        public DataTable BindDepotwiseSalesReport(string FromDate, string ToDate, string SearchTag, string TSID, string BSId, string depotid, string brand, string category, string productid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALES_REPORT] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + BSId + "','" + depotid + "','" + brand + "','" + category + "','" + productid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Poallocation
        public DataTable Bindpo1(string POID)
        {
            string sql = " SELECT T.PRODUCTID,T.PRODUCTNAME, " +
                         " T.QTY AS TOTALQTY, ISNULL(T1.QTY, '0') AS TOTALALLOCATEQTY, " +
                         "      ISNULL((T.QTY - T1.QTY), T.QTY) AS " +
                         "         QTY, UOMNAME FROM" +
                         "(SELECT  PRODUCTID, PRODUCTNAME," +
                         " SUM(CAST(QTY AS DECIMAL(10, 3))) AS QTY, UOMNAME, POID FROM T_MM_PODETAILS GROUP BY PRODUCTID, PRODUCTNAME, UOMNAME, POID) AS T " +
                         " LEFT OUTER JOIN " +
                         " (SELECT B.PRODUCTID, B.POID, B.PRODUCTNAME, " +
                         " SUM(CAST(B.ALLOCATIONQTY AS DECIMAL(10, 3)))AS QTY FROM " +
                         " POP_PO_ALLOCATION B   GROUP BY B.PRODUCTID, B.POID, B.PRODUCTNAME) " +
                         " AS T1 " +
                         "  ON T.PRODUCTID = T1.PRODUCTID AND T.POID = T1.POID " +
                         " WHERE " +
                         " T.POID='" + POID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindallocatepo(string POID)
        {
            string sql = " SELECT CASE WHEN DTOC IS NULL AND DEPOTID IS NOT NULL  THEN 'SUB TOTAL' " +
                         " WHEN PRODUCTID IS NULL THEN 'GRAND TOTAL' " +
                         " ELSE DTOC END AS DTOC, " +
                         " POID,PRODUCTID,PRODUCTNAME,UOMNAME,DEPOTID,DEPOTNAME,SUM(QTY)QTY,ALLOCATIONID FROM " +
                         " (SELECT DISTINCT CONVERT(VARCHAR(10),B.DTOC,103) AS DTOC, " +
                         " B.POID,B.PRODUCTID,B.PRODUCTNAME,A.UOMNAME,B.DEPOTID,C.BRPREFIX AS DEPOTNAME, " +
                         " CAST(B.ALLOCATIONQTY AS DECIMAL(10,3))AS QTY,B.ALLOCATIONID FROM T_MM_PODETAILS  A  " +
                         "  INNER JOIN POP_PO_ALLOCATION B ON A.POID=B.POID  INNER JOIN M_BRANCH C ON B.DEPOTID=C.BRID " +
                         "   WHERE B.POID='" + POID + "')A " +
                         "   Group BY " +
                         "       GROUPING SETS " +
                         "       ( " +
                         " 			(DTOC,POID,PRODUCTID,PRODUCTNAME,UOMNAME,DEPOTID,DEPOTNAME,ALLOCATIONID), " +
                         "             (DEPOTID), " +
                         "                         () " +
                         "       )";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindpoforpop(string vendorid)
        {
            string sql = "SELECT POID,PONO FROM T_MM_POHEADER WHERE ISCLOSED='N' AND VENDORID='" + vendorid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVENDORFORALLOCATION()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEMID <> '1'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string setallocationpop(string poid, string pono, string productid, string productname, string depotid, string allocateqty, string dtoc)
        {
            string sql = "EXEC [SP_SET_ALLOCATION_POP] '" + poid + "','" + pono + "','" + productid + "','" + productname + "','" + depotid + "','" + allocateqty + "','" + dtoc + "'";
            string setallocation = (string)db.GetSingleValue(sql);
            return setallocation;
        }

        public string resetallocationpop(string autoid, String POID, String PRODUCTID, String DEPOTID)
        {
            string sql = "EXEC [SP_SET_ALLOCATION_POP_DELETE] '" + autoid + "','" + POID + "','" + PRODUCTID + "','" + DEPOTID + "'";
            string setallocation = (string)db.GetSingleValue(sql);
            return setallocation;
        }

        public DataTable Totalallocateqtypowise(string PRODUCTID, string POID, string DEPOTID)
        {
            string sql = " SELECT T.QTY,ISNULL((T.QTY - T1.QTY), T.QTY)AS REMAININGQTY, " +
                         " ISNULL(T1.QTY, '0') " +
                         " AS ALLOCATEQTY FROM " +
                         " (SELECT ISNULL(SUM(CAST(ALLOCATIONQTY AS DECIMAL(10, 3))), '0') " +
                         " AS QTY, PRODUCTID, DEPOTID, POID FROM POP_PO_ALLOCATION  GROUP BY PRODUCTID, DEPOTID, POID)T " +
                         " LEFT OUTER JOIN " +
                         " (SELECT ISNULL(SUM(A.RECEIVEDQTY), '0') AS QTY, A.POID, A.PRODUCTID, B.MOTHERDEPOTID " +
                         " FROM T_STOCKRECEIVED_DETAILS A  INNER JOIN T_STOCKRECEIVED_HEADER B ON A.STOCKRECEIVEDID = B.STOCKRECEIVEDID " +
                         " GROUP BY A.POID, A.PRODUCTID, B.MOTHERDEPOTID)T1 " +
                         " ON T.PRODUCTID = T1.PRODUCTID AND T.POID = T1.POID " +
                         " AND T.DEPOTID = T1.MOTHERDEPOTID " +
                         " WHERE T.PRODUCTID = '" + PRODUCTID + "' AND " +
                         " T.POID = '" + POID + "' AND T.DEPOTID = '" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindASMBYDEPOT(string depotid, string BSID, string MODE)
        {
            string sql = " USP_ASM_SO_DAILY_SYNC '" + depotid + "','" + BSID + "','" + MODE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSOBYASM(string asmid, string BSID, string mode)
        {
            string sql = "EXEC [USP_SO_BIND_SYNC]'" + asmid + "','" + BSID + "','" + mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTSIDailyStatus(string depotid, string BSCode, string ASMID, string SOID, string CustomerID, string Finyear, string MonthID)
        {
            string sql = "EXEC [USP_RPT_TSIWISE_DAILY_SYNC] '" + depotid + "','" + BSCode + "','" + ASMID + "','" + SOID + "','" + CustomerID + "','" + Finyear + "','" + MonthID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchInvoiceId(string ACCENTRYID)
        {
            string sql = " EXEC USP_FETCHINVOICE '" + ACCENTRYID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /* Added By H P das */
        public DataTable Bind_DepotWise_BSwise(string FromDate, string ToDate, string SearchTag, string TSID, string depotid, string BSId, string Brand, string Category)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALES_REPORT_DETAILS] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + depotid + "','" + BSId + "','" + Brand + "','" + Category + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Added by Arti(26-09-2018)

        public DataTable BindBusinessegment_GT()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN (SELECT BS_GT FROM P_APPMASTER) ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindASMBYDEPOT(string depotid)
        {
            string sql = "EXEC USP_ASMLIST_DEPOTWISE '" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindASMBYDEPOT(string depotid, string UserID)
        {
            string sql = "EXEC USP_ASMLIST_DEPOTWISE '" + depotid + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDISTRIBUTORBYSO(string Soid)
        {

            string sql = "SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_TSI_MAPPING B  " +
                           " ON A.CUSTOMERID = B.CUSTOMERID " +
                           " WHERE TSI_ID IN(SELECT * FROM DBO.FNSPLIT('" + Soid + "', ',')) AND CUSTYPE_ID = '5E24E686-C9F4-4477-B84A-E4639D025135'OR CUSTYPE_ID='826D656F-353F-430F-8966-4FE6BE3F67ED'";


            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        /*Modified by soumitra mondal*/
        public DataTable BindDistributorDailyStatus(string depotid, string ASMID, string SOID, string CustomerID, string Finyear, string MonthID, string Monthname)
        {
            string sql = "EXEC [USP_RPT_DISTRIBUTOR_DAILY_SYNC] '" + depotid + "','" + ASMID + "','" + SOID + "','" + CustomerID + "','" + Finyear + "','" + MonthID + "','" + Monthname + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
        #endregion

        #region Get ProductName Name
        public DataTable BindProductName()
        {
            string Sql = "SELECT ID,PRODUCTALIAS FROM M_PRODUCT WHERE TYPE NOT IN ('FG','GIFT') ORDER BY PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(Sql);
            return dt;
        }
        #endregion

        public DataTable BIND_RATEWISE_PURCHASE_REPORT(string FromDate, string ToDate, string VendorID, string ProductID, string DepotId, string ReportType)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_RATEWISE_PURCHASE_REPORT] '" + FromDate + "','" + ToDate + "','" + VendorID + "','" + ProductID + "','" + DepotId + "','" + ReportType + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindStockinHand_Revised(string depotid, string productid, string packsize, string batchno, decimal mrp, string brand, string category, string fromdate, string todate, string curdate, string size, string StockDtls, string storelocation)
        {
            string sql = "EXEC USP_RPT_DEPOTWISE_STOCK_IN_HAND_REVISED '" + depotid + "','" + productid + "','" + packsize + "','" + batchno + "','" + mrp + "','" + brand + "','" + category + "','" + fromdate + "','" + todate + "','" + curdate + "','" + size + "','" + StockDtls + "','" + storelocation + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindProductWithOutBSegment(string divid, string catid, string BSID)
        {
            string STRSQL = string.Empty;
            string STRSQL_CHECK = string.Empty;
            string sql = string.Empty;
            string str = string.Empty;
            DataTable dt = new DataTable();
            if (BSID != "0")
            {
                sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT AS A" +
                      " INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B ON A.ID = B.PRODUCTID" +
                      " WHERE A.DIVID IN('" + divid + "') " +
                      " AND A..CATID IN('" + catid + "')" +
                      " AND BUSNESSSEGMENTID='" + BSID + "'";
            }
            else
            {
                sql = " SELECT DISTINCT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT AS A " +
                      //" INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B ON A.ID = B.PRODUCTID" +
                      " WHERE DIVID IN('" + divid + "') " +
                      " AND A.CATID IN('" + catid + "')";
            }
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindDistributorChallanReport(string fromdate, string todate,  string depot, string viewmode, string Sessionid)
        {
            string sql = "EXEC [USP_RPT_DISTRIBUTOR_CHALLAN_REPORT] '" + fromdate + "','" + todate + "','" + depot + "','" + viewmode + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindLedgerReport_JSON(string fromdate, string todate, string ledgerid, string depot, string viewmode, string Sessionid)
        {
            string sql = "EXEC [USP_RPT_LEDGER_DETAILS_JSON] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + depot + "','" + viewmode + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        /*Added By Arti Agarwal On 05.11.2018*/
        public DataTable BindDistributorCreditAmount(string depotid, string bsid, string op, string Amount, string date, string condition)
        {
            string sql = "EXEC [USP_DISTRIBUTOR_AMOUNT] '" + depotid + "','" + bsid + "','" + op + "','" + Amount + "','" + date + "','" + condition + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }


        public DataTable BindExpesesLeader()
        {
            string sql = "EXEC [USP_LOAD_EXPENSES_LEDGER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataSet BindExpensesLedgerReport_JSON(string fromdate, string todate, string ledgerid, string depot, string type, string Sessionid)
        {
            string sql = "EXEC [USP_RPT_PLSHEET_EXPENSES_CONSOLIDATED] '" + fromdate + "','" + todate + "','" + depot + "','" + ledgerid + "','" + Sessionid + "','" + type + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #region BindBrandWithRawMaterials by HPDAS
        public DataTable BindBrandWithRawMaterials()
        {
            //string sql = "SELECT DISTINCT DIVID,DIVNAME FROM M_PRODUCT ORDER BY DIVNAME";
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION WHERE DIVID NOT IN ('E6B2F631-7237-4B97-BC33-B6FCF0B9075B')" +//Add by HPDAS (20-11-2018)
                         " UNION ALL" +
                         " SELECT CONVERT (VARCHAR(5), ID)AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ID IN('2','3','4')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegmentNew(string BSID)
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit(('" + BSID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataSet BindProductwisePartyOnLoad_CSD(string fromdate, string todate, string depot, string bsid, string saletype, string productid, int amt, string USERID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISEPARTY_CSD] '" + fromdate + "','" + todate + "','" + depot + "','" + bsid + "','" + saletype + "','" + productid + "'," + amt + ",'" + USERID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindBusinessegment_CSD()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN ('C5038911-9331-40CF-B7F9-583D50583592','A0A1E83E-1993-4FB9-AF53-9DD595D09596','8AE0E8C9-F4F7-4382-B8AB-870A64ABF996') ORDER BY NAME   ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindprogressReport_SummeryDetails(string fromdate, string todate, string brandid, string catid, string depot, string StoreLocationID)
        {
            string sql = "EXEC USP_RPT_STOCK_MOVEMENT_SUMMERY '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "','" + StoreLocationID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Secondary Sales Dump  add free qty new sp checkin by arnab 16-10-2019
        public DataTable BindSecondarySalesDMSHierarchywise(string fromdate, string todate, string userid)
        {
            string sql = "EXEC [USP_RPT_SECONDARY_SALES_DUMP_DMS_HIERARCHYWISE_WITH_FREE_QTY] '" + fromdate + "','" + todate + "','" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BIND_DEPOTWISE_PURCHASE(string FromDate, string ToDate, string DepotId, string CatId, string ProductId, string Type, string SearchTag, string TSID, string FinYear)
        {
            string sql = "EXEC USP_RPT_DEPOTWISE_PURCHASE '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + CatId + "','" + ProductId + "','" + Type + "','" + SearchTag + "','" + TSID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BIND_PARTYWISE_PURCHASE(string FromDate, string ToDate, string DepotId, string CatId, string ProductId, string Type, string SearchTag, string TSID, string FinYear)
        {
            string sql = "EXEC USP_RPT_PARTYWISE_PURCHASE_FAC '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + CatId + "','" + ProductId + "','" + Type + "','" + SearchTag + "','" + TSID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Secondary Sales Under SO by HPDAS on 03.12.2018
        public DataSet BindSalesReportUnderSO_json(string fromdate, string todate, string userid, string choise, string brandid, string catid, string sku, string productid)
        {

            string sql = "EXEC [USP_RPT_SECONDARY_SALES_DMS_UNDER_SO]'" + fromdate + "','" + todate + "','" + userid + "','" + choise + "','" + brandid + "','" + catid + "','" + sku + "','" + productid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion

        public DataTable BindBusinessegment_Defance()//Only Defence Businessegment
        {
            string sql = " SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT " +
                         " WHERE BSID IN('A0A1E83E-1993-4FB9-AF53-9DD595D09596','C5038911-9331-40CF-B7F9-583D50583592','8AE0E8C9-F4F7-4382-B8AB-870A64ABF996','C8E2844A-B43F-4028-856B-1D8827CB9B8B') " +
                         " ORDER BY NAME DESC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory_ForPurchase()
        {
            string sql = " SELECT DISTINCT A.CATID,A.CATNAME FROM M_CATEGORY AS A " +
                         " INNER JOIN  M_PRODUCT AS B ON A.CATID = B.CATID" +
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessegment_GT_MT()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN ('7F62F951-9D1F-4B8D-803B-74EEBA468CEE','0AA9353F-D350-4380-BC84-6ED5D0031E24') ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory_ForPurchase(string Brand)
        {
            string sql = " SELECT DISTINCT A.CATID,A.CATNAME FROM M_CATEGORY AS A " +
                         " INNER JOIN  M_PRODUCT AS B ON A.CATID = B.CATID" +
                         " WHERE B.DIVID IN('" + Brand + "')" +
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                         " FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2','12')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #region Partywise Balance Report by HPDAS on 15.12.18
        public DataTable BindPartywiseBalance(string Fromdate, string Todate, string userid, string DISTRIBUTORID, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PARTYWISE_BALANCE_REPORT] '" + Fromdate + "','" + Todate + "','" + userid + "','" + DISTRIBUTORID + "', '" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable Bind_PurchaseReturnType(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_CHEAK_PURCHASE_RETURN_TYPE_ERP] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSalereturnDetailsPurchase(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_PURCHASE_RETURN_DETAILS_GST_PRINT]'" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindPendingApproval(string fromdate, string todate, string finyear)/* added on 08/01/2019 */
        {
            string sql = "EXEC [USP_PENDING_VOUCHER] '" + fromdate + "','" + todate + "','" + finyear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #region Budget Components by HPDAS on 27.12.18
        public DataTable BindBudgetComponent()
        {
            string sql = "SELECT * FROM M_BUDGET_COMPONENT WHERE COMPONENTID<>'11'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public int SaveBudgetComponents(string FromDate, string ToDate, string XML)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC [USP_BUDGET_COMPONENT_INSERT] '" + FromDate + "','" + ToDate + "','" + XML + "'";
            result = db.HandleData(sql);
            return result;

        }
        public DataTable BindComponentDetails(string FromDate, string ToDate)
        {
            string sql = "SELECT * FROM T_BUDGET_COMPONENT_RATE WHERE COMPONENTID<>'11' AND FromDate = CONVERT(DATE,'" + FromDate + "',103) and ToDate = CONVERT(DATE,'" + ToDate + "',103)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Budget Components for Advertise by HPDAS on 29.12.18
        public DataTable BindBudgetComponentAdvertise()
        {
            string sql = "SELECT * FROM M_ADV_BUDGET";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public int SaveBudgetComponentsAdvertise(string FromDate, string ToDate, string XML)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC [USP_BUDGET_COMPONENT_ADV_INSERT] '" + FromDate + "','" + ToDate + "','" + XML + "'";
            result = db.HandleData(sql);
            return result;

        }
        public DataTable BindComponentDetailsAdvertise(string FromDate, string ToDate)
        {
            string sql = "SELECT * FROM T_BUDGET_COMPONENT_ADV_RATE WHERE FromDate = CONVERT(DATE,'" + FromDate + "',103) and ToDate = CONVERT(DATE,'" + ToDate + "',103)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Budget Report by Rabindra and HPDAS on 31.12.18
        public DataTable BindBudgetReport(string Fromdate, string Todate, string DEPOTID, string BSID, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_BUDGET_SUMMARY] '" + Fromdate + "','" + Todate + "','" + DEPOTID + "','" + BSID + "', '" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Purchase Dump by HPDAS on 09.01.2019
        public DataTable BindPurchaseDump(string fromdate, string todate,string finyear)
        {
            string sql = "EXEC [USP_PURCHASE_DUMP] '" + fromdate + "','" + todate + "','"+ finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Adv rpt by Rabindra on 18.01.2019
        public DataTable BindAdvertisementTaxDetails(string fromdate, string todate, string CategoryID)
        {
            string sql = "EXEC [USP_RPT_ADV_TAX_REPORT] '" + CategoryID + "','" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Primary Vs Closing Report by HPDAS on 21.01.2019
        public DataTable BindPrimaryVsClosing(string fromdate, string todate, string SearchTag, string TSID, string FINYEAR, string UserID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_VS_CLOSING] '" + fromdate + "','" + todate + "','" + SearchTag + "','" + TSID + "','" + FINYEAR + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Primary Vs Closing Report for PAN India by HPDAS on 24.01.2019
        public DataTable BindPrimaryVsClosingPanIndia(string fromdate, string todate, string SearchTag, string TSID, string FINYEAR)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_VS_CLOSING_PANINDIA] '" + fromdate + "','" + todate + "','" + SearchTag + "','" + TSID + "','" + FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Job Order Report by HPDAS on 29.01.2019
        public DataSet BindJobOrder(string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_RPT_JOB_ORDER_FACTORY] '" + fromdate + "','" + todate + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion
        #region Added by HPDAS
        public DataTable BindStockReceiptReport(string FromDate, string ToDate, string DepotId, string PartyID, string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCK_RECEIPT_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + PartyID + "','" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Added by Rabindra
        public DataTable BindPartywisePrimarySecondaryClosing(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string USERTYPE, string USERID)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE_WEEKLY] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Secondary Sales MT for DMS Hierarchywise by SOUMITRA MONDAL on 06.02.2019
        public DataSet BindSecondarySalesDMSHierarchywiseMT(string fromdate, string todate, string userid)
        {
            string sql = "EXEC [USP_RPT_SECONDARY_SALES_DUMP_DMS_HIERARCHYWISE_MT] '" + fromdate + "','" + todate + "','" + userid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion

        #region fac_report
        /*Added by sayan dey on 11/02/2019*/
        public DataSet Bind_ledger_cr_dr_factory(string DateFrom, string DateTo, string DepotID, string LedgerID, string finyear)
        {
            string sql = "EXEC [USP_RPT_LEDGER_FAC_DETAILS] '" + DateFrom + "','" + DateTo + "','" + DepotID + "','" + LedgerID + "','" + finyear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable Bind_productfor_production_report(string depotid)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct b.id,b.PRODUCTALIAS from " +
            " T_TPU_QUALITYCONTROL_DETAILS a " +
            " inner join T_TPU_QUALITYCONTROL_HEADER c on a.QCID = c.QCID " +
            " inner join m_product b " +
            " on a.PRODUCTID = b.id " +
            " where c.tpuid = '" + depotid + "' and b.type = 'FG'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_Production_report_fac(string DateFrom, string DateTo, string finyear, string productid, string quater, string depotid, string packsize)
        {
            string sql = "EXEC [usp_rpt_production_qc_details] '" + DateFrom + "','" + DateTo + "','" + finyear + "','" + productid + "','" + quater + "','" + depotid + "','" + packsize + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        #endregion

        public DataTable BindDailySecondaryTSV_TSIDetails(string UserID, string FROMDATE, string TODATE)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DAILYSECONDARY_TSV_TSIDETAILS] '" + UserID + "','" + FROMDATE + "','" + TODATE + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDailySecondaryTSV_ASMDetails(string UserID, string FROMDATE, string TODATE)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DAILYSECONDARY_TSV_ASMDETAILS] '" + UserID + "','" + FROMDATE + "','" + TODATE + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_TSITracking_Details(string USERDETAILSID, string FROMDATE, string TODATE, string USERTYPE, string REPORTTYPE, string USERID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_APP_USER_TRACKING_DETAILS] '" + USERDETAILSID + "','" + FROMDATE + "','" + TODATE + "','N','" + USERID + "','" + USERTYPE + "','" + REPORTTYPE + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_TSITracking_Summary(string TSIID, string TODATE)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_APP_USER_TRACKING_SUMMARY] '" + TSIID + "','" + TODATE + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_UserInfo_Details(string USERID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_GET_USERINFO_DETAILS] '" + USERID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable Bind_ItemStatus_Primary_Secondary(string FROMDATE, string TODATE, string PRODUCTLIST)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_ITEM_RETAIL_DETAILS] '" + FROMDATE + "','" + TODATE + "','" + PRODUCTLIST + "'";
            dt = db.GetData(sql);
            return dt;
        }

        /*Added By Sayan Dey On 31/07/2019 For Gstr 2A report*/
        public DataSet Bind_Gstr_2A(string fromdate, string todate)
        {
            string sql = "EXEC [usp_gstr_report] '" + fromdate + "','" + todate + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
                return ds;
        }
        public DataSet Bind_Gstr_2A_ISD(string fromdate, string todate)
        {
            string sql = "EXEC [usp_gstr_report_ISD] '" + fromdate + "','" + todate + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        /*Added By Sayan Dey 02/08/2019 Creditors*/
        public DataSet BindSundryCreditors(string Asondate, string Groupid, string Ledgerid, string Mode,string FinYear)
        {
            string sql = "EXEC [USP_RPT_CREDITORS_BALANCE] '" + Asondate + "','" + Groupid + "','" + Ledgerid + "','" + Mode + "','"+ FinYear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        /*Added By Sayan Dey 14/10/2019 Creditors*/
        public DataSet BindForcastVsProductionVsSalereport(string fromdate, string todate,string FinYear)
        {
            string sql = "EXEC [Usp_Rpt_Forcast_Vs_Production_Vs_Sale_Report] '" + fromdate + "','" + todate + "','" + FinYear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindDailyProductionVsQaVsReq(string fromdate, string todate, string depotid,string mode) /*Added By P.Basu on 19/12/2019*/
        {
            string sql = "EXEC [USP_RPT_DAILY_PRODUCTION_VS_QA] '" + fromdate + "','" + todate + "','" + depotid + "','"+mode+"'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }


        public DataTable BindSundryCreditors_bind(string Asondate, string Groupid, string Ledgerid, string Mode)
        {
            string sql = "EXEC [USP_RPT_CREDITORS_BALANCE] '" + Asondate + "','" + Groupid + "','" + Ledgerid + "','" + Mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSundryGroupList()
        {
            string sql = "EXEC USP_ACC_SUNDRY_GROUPLIST";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /**********************************************************/

        public DataSet BindFillRate_json(string depotid, string packsize, string brand, string category, string curdate, string storelocation)
        {
            string sql = "EXEC [USP_RPT_FORCAST_VS_SALE_N_STOCK_FILL_RATE_JSON] '" + depotid + "','" + packsize + "','" + brand + "','" + category + "','" + curdate + "','" + storelocation + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindProductionRequirement_json(string depotid, string packsize, string brand, string category, string curdate, string storelocation)
        {
            string sql = "EXEC [USP_PRODUCTION_REQUIREMET_JSON] '" + depotid + "','" + packsize + "','" + brand + "','" + category + "','" + curdate + "','" + storelocation + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindStoreLocation_json(string Mode)
        {
            string sql = "EXEC USP_STORE_LOCATION_JSON '"+ Mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataSet BindFillRateSummary_json(string depotid, string packsize, string brand, string category, string curdate, string storelocation)
        {
            string sql = "EXEC [USP_RPT_FILL_RATE_JSON] '" + depotid + "','" + packsize + "','" + brand + "','" + category + "','" + curdate + "','" + storelocation + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }


        public string Bind_Scheme_Coin(string invoiceid)
        {
            string sql = "EXEC [usp_scheme_challan_print] '"+ invoiceid + "' ";
            string region = (string)db.GetSingleValue(sql);
            return region;
        }

        public DataTable BindProduct_RMBrandWise(string BrandID, string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME " +
                  "FROM T_STOCKRECEIVED_DETAILS	        AS A " +
                  "INNER JOIN T_STOCKRECEIVED_HEADER	AS B ON B.STOCKRECEIVEDID=A.STOCKRECEIVEDID AND B.MOTHERDEPOTID='" + DepotID + "'" +
                  "INNER JOIN M_PRODUCT					AS C ON C.ID=A.PRODUCTID AND C.TYPE='RM' AND DIVID='" + BrandID + "'" +
                  "WHERE MFDATE!='1900-01-01 00:00:00.000' AND	EXPRDATE!='1900-01-01 00:00:00.000'" +
                  "ORDER BY PRODUCTALIAS";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategoryRM(string BrandID)
        {
            string sql = " SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='3' ORDER BY CATNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory_RMBrand(string brandid, string DepotID)
        {
            string sql = "SELECT DISTINCT C.CATID,C.CATNAME " +
                         "FROM T_STOCKRECEIVED_DETAILS      AS A " +
                         "INNER JOIN T_STOCKRECEIVED_HEADER	AS B ON B.STOCKRECEIVEDID=A.STOCKRECEIVEDID AND B.MOTHERDEPOTID='" + DepotID + "'" +
                         "INNER JOIN M_PRODUCT				AS C ON C.ID=A.PRODUCTID AND C.TYPE='RM' AND DIVID='" + brandid + "'" +
                         "WHERE A.MFDATE!='1900-01-01 00:00:00.000' AND A.EXPRDATE!='1900-01-01 00:00:00.000'" +
                         "ORDER BY C.CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct_RMBrandWise(string BrandID, string CatID, string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = "SELECT DISTINCT ID,PRODUCTALIAS AS NAME " +
                  "FROM T_STOCKRECEIVED_DETAILS	        AS A " +
                  "INNER JOIN T_STOCKRECEIVED_HEADER	AS B ON B.STOCKRECEIVEDID=A.STOCKRECEIVEDID AND B.MOTHERDEPOTID='" + DepotID + "'" +
                  "INNER JOIN M_PRODUCT					AS C ON C.ID=A.PRODUCTID AND C.TYPE='RM' AND DIVID='" + BrandID + "'" +
                  "WHERE MFDATE!='1900-01-01 00:00:00.000' AND	EXPRDATE!='1900-01-01 00:00:00.000'" +
                  "AND C.CATID IN('" + CatID + "')" +
                  "ORDER BY PRODUCTALIAS";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRMWiseAgeingStock(string AsonDate, string Depotid, string Brand, string Category, string Productid, string FinYear)
        {
            // string sql = "EXEC USP_RPT_RM_AGEING_STOCK '" + AsonDate + "','" + Depotid + "','" + Brand + "','" + Category + "','" + Productid + "','" + FinYear + "'";
            string sql = "EXEC USP_RPT_AGEINGWISE_STOCK '" + AsonDate + "','" + Depotid + "','" + Brand + "','" + Category + "','" + Productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        #region get_customer_dump
        public DataSet get_customer_dump(string depotid)
        {
            string sql = "EXEC [USP_RPT_CUSTOMER_DUMP] '" + depotid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion

        /*Added  by Sayan Dey on 28/09/2019 for Power SKU Flighting ASM Wise*/
        #region Power_SKU_Flighting_ASM_Wise
        public DataSet get_powersku_asmwise (string userid, string finyear,string Month)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_FLASH_WITHTARGET_ASMWISE] '" + userid + "','"+ finyear +"','"+ Month +"'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        /*Added  by Sayan Dey on 30/09/2019 for Distributor_sales_efficiency report*/
        #region Distributor_sales_efficiency
        public DataSet get_distributor_sales_efficiency(string depotid, string monthid,string asmid, string soid, string finyear)
        {
            string sql = "EXEC [usp_rpt_distributor_sales_efficiency] '" + depotid + "','" + monthid + "','"+ asmid + "','"+ soid + "','"+ finyear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        public DataSet BindGSTOutward(string fromdate, string todate, string depot)
        {
            string sql = "EXEC [USP_RPT_OUTWARD_GST] '" + fromdate + "','" + todate + "','" + depot + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindGSTInward(string fromdate, string todate, string depot)
        {
            string sql = "EXEC [USP_RPT_INWARD_GST] '" + fromdate + "','" + todate + "','" + depot + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindSegment()
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            sql = " SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT "+
                  " WHERE BSID NOT IN ('33F6AC5E-1F37-4B0F-B959-D1C900BB43A5','97547CB6-F40B-4B43-923D-B63F61A910C2') "+
                  " ORDER BY BSNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreLocation_json_V2()
        {
            string sql = " SELECT ID,NAME FROM M_STORELOCATION "+
                         " WHERE ID IN('5FA9501B-E8D0-4A0F-B917-17C636655514', 'D2D8505C-CA12-4E9A-8B9C-DD4D051D7EA3', '113BD8D6-E5DC-4164-BEE7-02A16F97ABCC') "+
                         " ORDER BY NAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindASMwisePerformanceGrid(string monthfromdate) /*added by p.basu on 07/01/2020*/
        {
            string sql = " EXEC [usp_asm_wise_daily_performance_report]'" + monthfromdate + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindPODetailsExcel(string POID)
        {
            DataTable ds = new DataTable();
            string sql = "EXEC [USP_PURCHASE_ORDER_EXPORT_EXCEL] '" + POID + "'";
            ds = db.GetData(sql);
            return ds;
        }
        public DataTable BindPoInfoExcel(string fromdate,string todate, string FinYear)
        {
            DataTable ds = new DataTable();
            string sql = "EXEC [USP_PURCHASE_ORDER_INFO_KKG] '" + fromdate + "','" + todate + "','" + FinYear + "' ";
            ds = db.GetData(sql);
            return ds;
        }

        /*new add for stock in hand*/
        public DataTable FetchStockInHand(string DEPOTID,string PRODUCTID,string STORELOATIONID,string DIVID,string CATID,string FROMDATE)
        {
            string sql = "[USP_RPT_STOCK_IN_HAND_KKG] '"+ DEPOTID + "','"+ PRODUCTID + "','"+ STORELOATIONID + "','"+ DIVID + "','"+ CATID + "','" + FROMDATE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }


        public DataTable FecthData()
        {
            string sql = "EXEC [USP_RPT_STOCK_IN_HAND_KKG_TEST]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        #region arnab chatterjee ON 19/07/2019   =====dsr report====
        public DataTable Bind_DEPO_SO(string DEPOTID, string mode, string soid, string DSRxml)  //** SP CHANGE ** IF ANY ISSUE OCCURE PLESE USE OLD SP.SP NAME [USP_GET_USERINFO_ERP]//
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_GET_USERINFO_ERP] '" + DEPOTID + "','" + mode + "','" + soid + "','" + DSRxml + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region arnab chatterjee ON 19/07/2019   =====bankbind report====
        public DataTable Bind_Bank()
        {
            DataTable dt = new DataTable();
            string sql = "[USP_BANK_DETAILS]";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region arnab chatterjee ON 19/07/2019   =====bankbind report====
        public DataTable Bind_Bank_Booklet(char MODE, string USERID, string BANKID, string BOOKLETNO, string BRANCH)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_BANK_BOOKLET_DETAILS] '" + MODE + "','" + USERID + "','" + BANKID + "','" + BOOKLETNO + "','" + BRANCH + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string Acc_Booklet_Insert(string Date, string BankID, string userid, string xml, string ch)
        {
            string sql = "EXEC USP_AccBooklet_Insert_Update '" + Date + "','" + BankID + "','" + userid + "','" + xml + "','" + ch + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        #endregion
        #region arnab chatterjee ON 06/08/2019   =====bankbind report====
        public string Acc_Booklet_Insert_vouter(string BankID, string userid, string xml)
        {
            string sql = "EXEC USP_AccBooklet_vouter_Insert_Update '" + BankID + "','" + userid + "','" + xml + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        #endregion
        #region arnab chatterjee ON 14/08/2019   =====bankbind report====
        public DataTable Acc_Bank_Booklet_Bind(char MODE, string BANKID, string chequeno, string BOOKLETNO, string xml)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_BANK_BOOKLET_DETAILS_checker'" + MODE + "','" + BANKID + "','" + chequeno + "','" + BOOKLETNO + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion



        #region SOUMITRA MONDAL ON 09/02/2019   =====Primary Sale Factory====
        public DataSet BindPrimarySalesTaxSummary_InvoiceReport_Factory(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn, string CustomerID, string USERID, string InvoiceType)
        {
            string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT_INVOICE_FACTORY] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "','" + CustomerID + "','" + USERID + "','" + InvoiceType + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion
        #region SOUMITRA MONDAL ON 09/02/2019   =====Primary Sale Factory With Invoice Link===========
        public DataSet BindInvoiceDetialsReport_GST_Invoice_Factory(string InvoiceID)
        {
            string sql = "EXEC [USP_RPT_SALE_INVOICE_DETAILS_GST_PRIMARY_INVOICE_DETAILS_INVOICE_FACTORY] '" + InvoiceID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable BindDepotwiseSalesReport_Factory(string FromDate, string ToDate, string SearchTag, string TSID, string BSId, string depotid, string brand, string category, string productid, string invtype)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALES_REPORT_FACTORY] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + BSId + "','" + depotid + "','" + brand + "','" + category + "','" + productid + "','" + invtype + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_DepotWise_BSwise_Factory(string FromDate, string ToDate, string SearchTag, string TSID, string depotid, string BSId, string Brand, string Category, string catname)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_DEPOTWISE_SALES_REPORT_DETAILS_FACTORY] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + depotid + "','" + BSId + "','" + Brand + "','" + Category + "','" + catname + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #region FACTORY PROGRESS REPORT ON 13.02.2019
        public DataSet BindprogressReportDetails_Factory(string fromdate, string todate, string brandid, string catid, string depot)
        {
            string sql = "EXEC USP_RPT_STOCK_MOVEMENT_FACTORY '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion

        #region FACTORY PROGRESS REPORT DETAILS ON 13.02.2019
        public DataSet BindprogressReportDetails_Factory_PART1(string fromdate, string todate, string brandid, string catid, string depot)
        {
            string sql = "EXEC USP_RPT_STOCK_MOVEMENT_FACTORY_PART1 '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #endregion

        public DataSet BindprogressReportDetails_Factory_PART2(string fromdate, string todate, string brandid, string catid, string depot, string productid)
        {
            string sql = "EXEC USP_RPT_STOCK_MOVEMENT_FACTORY_PART2 '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "','" + productid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #region Bind Brand_SupliedItem
        public DataTable BindBrand_SupliedItem()
        {
            string sql = "EXEC USP_MM_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataSet BindNonMovementItems(string fromdate, string todate, string brandid, string catid, string depot, string productid, string tag, int noofdays)
        {
            string sql = "EXEC USP_RPT_STOCK_NON_MOVEMENT_ITEM '" + fromdate + "','" + todate + "','" + brandid + "','" + catid + "','" + depot + "','" + productid + "','" + tag + "'," + noofdays + "";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        #region Bind Customer for Distributor Payment Collection
        public DataTable BindCustomerForReport()
        {
            string sql = "EXEC USP_ALL_CUSTOMER_FOR_REPORT";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region cls added for productwise purchase
        public DataTable BindCategory_Brandwise(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY_MULTI '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductMultiple(string catid, string DIVID)
        {
            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO,A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A LEFT JOIN M_CATEGORY AS B " +
                         " ON A.CATID= B.CATID" +
                         " WHERE A.CATID IN (SELECT * FROM DBO.fnSplit('" + catid + "',','))" +
                         " AND A.DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',','))" +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Bind All Brand
        public DataTable BindBrand_Purchase()
        {
            string sql = "EXEC USP_ALL_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Bind All PARTY and Bind Partywise Purchase
        public DataTable BindParty_Purchase()
        {
            string sql = "EXEC USP_ALL_PARTY";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BIND_PARTYWISE_PURCHASE(string FromDate, string ToDate, string DepotId, string CatId, string ProductId, string Type, string SearchTag, string TSID, string FinYear, string PartyID)
        {
            string sql = "EXEC USP_RPT_PARTYWISE_PURCHASE '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + CatId + "','" + ProductId + "','" + Type + "','" + SearchTag + "','" + TSID + "','" + FinYear + "','" + PartyID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // ADDED FOR DEPOTWISE STOCK-IN-HAND WITH PURCHASE on 27.03.19
        public DataTable BIND_DEPOTWISE_STOCKINHAND_PURCHASE(string FromDate, string ToDate, string DepotId, string BrandId, string PackSizeId, string StockLocation, string ReportOn, string FinYear)
        {
            string sql = "EXEC USP_RPT_DEPOTWISE_STOCK_IN_HAND_PURCHASE '" + FromDate + "','" + ToDate + "','" + DepotId + "','" + BrandId + "','" + PackSizeId + "','" + StockLocation + "','" + ReportOn + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // added for new FY
        public DataTable BindTimeSpanFY(string Tag, string FinYear)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "' AND FINYEAR='" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Closing_Stock_Hirerchy_Wise
        /*******Hirerchy Wise Closing Stock******/
        public DataSet BindClosing_Stock_Hirerchy_Wise(string fromdate, string todate, string userid,string mode)
        {
            string sql = "EXEC [usp_rpt_sale_stock_report_asm] '" + fromdate + "','" + todate + "','" + userid + "','"+ mode +"'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }


        /***************************************/
        #endregion

        #region pJP
        /******************************************added by sayan dey********************************************************/

        public DataTable Bind_Pgp(string Monthid, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_SALES_PGP] '" + Monthid + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributor_pgp(string Userid)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_SALES_PGP_WORK_HIERARCHY_BINDDISTRIBUTOR] '" + Userid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public string Savepgp(string Monthid, string Finyear, String Userid, String Xml)
        {
            string result = string.Empty;
            string sql = "EXEC [USP_SALES_PGP_INSERT] '" + Monthid + "','" + Finyear + "','" + Userid + "','" + Xml + "'";
            result = (string)db.GetSingleValue(sql);
            return result;
        }

        public DataTable Bindworkingfor_pgp(string Userid,string mode)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_SALES_PGP_WORK_HIERARCHY] '" + Userid + "',"+ mode +"";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bindpjpsummary_JSON(string Userid, string Monthid, string Finyear)
        {
            string sql = "EXEC [usp_pjp_submission_report_summary] '" + Userid + "','" + Monthid + "','" + Finyear + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet Bindpjpdetails_JSON(string USERID, string MONTHID, string FINYEAR)
        {
            string sql = "EXEC [usp_pjp_submission_report_details] '" + USERID + "','" + MONTHID + "','" + FINYEAR + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        /**********************************************************/
        #endregion

        public DataTable BeatAllocationSFA(string Soid, string TSIid, string Customerid, string Type, string Userid, string Allocationxml)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_BEAT_ALLOCATION_PLANNING_SFA] '" + Soid + "','" + TSIid + "','" + Customerid + "','" + Type + "','" + Userid + "','" + Allocationxml + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Binddepotwisesales_JSON(string fromdate, string todate, string stateid,string bsid,int amtin)
        {
            string sql = "EXEC [usp_rpt_primary_sale_depotwise_new] '" + fromdate + "','" + todate + "','" + stateid + "','"+ bsid + "','"+ amtin + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet Bindfreesalereport(string fromdate, string todate, string type, string producttype)
        {
            string sql = "EXEC [USP_RPT_FREE_SALE_REPORT] '" + fromdate + "','" + todate + "','" + type + "','" + producttype + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindProductionReport(string selectdate,string depotid) /*new add by p.basu on 17/12/2019*/
        {
            string sql = "EXEC [usp_productionreport_asondate_factory] '" + selectdate + "','" + depotid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindFinYear(string FinYear)  /* new add by soumitra 19042019*/
        {
            string sql = "SELECT TOP 1 * FROM M_TIMEBREAKUP WHERE  FINYEAR='" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindpjpstatusdetails(string USERDETAILSID, string FROMDATE, string TODATE, string USERID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_APP_SO_ATTENDANCE_DETAILS] '" + USERDETAILSID + "','" + FROMDATE + "','" + TODATE + "','N','" + USERID + "','N','Y'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPrintHeaderForAccStatement(string FromDate, string ToDate, string depotid,string ledgerid, string FinYear)
        {
            string sql = "EXEC [USP_CUSTOMER_DETAILS] '" + FromDate + "','"+ ToDate + "','"+ depotid + "','"+ ledgerid + "','"+ FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerBySOID(string userid)
        {
            string sql = "EXEC USP_SOWISE_PARTYLIST '" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPartywiseTargetvsAchev(string CustomerID, string MonthID, string Finyear, string BrandID, string CategoryID, string UNITVALUE)
        {
            string sql = "EXEC USP_RPT_DISTRIBUTORWISE_TARGET_VS_ACHV '" + BrandID + "','" + CategoryID + "','" + UNITVALUE + "','" + MonthID + "','" + Finyear + "','" + CustomerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTSIwiseTargetvsAchev(string TSIID, string MonthID, string Finyear, string BrandID, string CategoryID, string UNITVALUE)
        {
            string sql = "EXEC USP_RPT_TSIWISE_TARGET_VS_ACHV '" + BrandID + "','" + CategoryID + "','" + UNITVALUE + "','" + MonthID + "','" + Finyear + "','" + TSIID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTSIPerformanceCalculator(string TSIID, string MonthID, string Finyear)
        {
            string sql = "EXEC USP_RPT_TSI_DESIRED_PERFORMANCE_CALCULATOR '" + MonthID + "','" + Finyear + "','" + TSIID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindWeeklyTargetvsAchevreport(string Month, string Finyear, string ASMID, string CatID)
        {
            string sql = "EXEC [USP_RPT_WEEKWISE_TARGET_VS_ACHV] '" + Month + "','" + Finyear + "','" + ASMID + "','" + CatID + "'";
            DataSet dsweek = new DataSet();
            dsweek = db.GetDataInDataSet(sql);
            return dsweek;
        }

        public DataTable BindSale_Stk_AvailableDays(string Finyear)
        {
            string sql = "EXEC USP_RPT_PRODUCT_DEPOTWISE_SALES_STOCK '" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region added by Soumitra Mondal on 05.04.19 for New FY
        public DataTable BindTimeSpan_New(string Tag, string FinYear)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'AND FINYEAR='" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Transporter bill entry report  by SOUMITRA MONDAL on 12.06.2019
        public DataTable Bindtransporterbillentryreport(string fromdate, string todate)
        {
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILLENTRY_DUMP] '" + fromdate + "','" + todate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Distributor daily sync report add by arnab 15-10-2019
        public DataTable BindDepot_Primary_new(string Usertype, string Userid)
        {
            string sql = "USP_DistributorDaily_Dropdown_Bind'" + Usertype + "','" + Userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable BindZoneDepotwiseASM(string ZoneID,string DepotID)
        {
            string sql = "EXEC USP_ZONEWISE_DEPOTWISE_USERLIST '" + ZoneID + "','" + DepotID + "'";
            DataTable dtASM = new DataTable();
            dtASM = db.GetData(sql);
            return dtASM;
        }

        public DataTable BindNextLevelUserList(string SuperiorID, string SubOrdinateType)
        {
            string sql = "EXEC USP_USERLIST '" + SuperiorID + "','" + SubOrdinateType + "'";
            DataTable dtASM = new DataTable();
            dtASM = db.GetData(sql);
            return dtASM;
        }

        public DataTable BindPartyMonthwiseOpenigClosing(string DistributorID, string Finyear, string UserID)
        {
            string sql = "EXEC USP_RPT_PARTYWISE_MONTHLY_OPEN_PRIM_SECOND_CLS '" + Finyear + "','" + DistributorID + "','" + UserID + "'";
            DataTable dtOpening = new DataTable();
            dtOpening = db.GetData(sql);
            return dtOpening;
        }

        public DataSet TSIPERFORMENCE(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtQtyInPCS = new DataSet();
            string sql = " EXEC USP_RPT_TSI_PERFORMENCE '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            dtQtyInPCS = db.GetDataInDataSet(sql);
            return dtQtyInPCS;
        }

        public DataSet TSITRENDLINEANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            string sql = " EXEC USP_RPT_DAILYTSV_TSI_PERFORMENCE '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            dtPerformance = db.GetDataInDataSet(sql);
            return dtPerformance;
        }

        public DataSet TSIORDER_VS_INVOICE_ANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            string sql = " EXEC USP_RPT_ORDER_VS_INVOICE_ANALYSIS '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            dtPerformance = db.GetDataInDataSet(sql);
            return dtPerformance;
        }

        public DataSet TSIORDER_BRANDSKU_ANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            string sql = " EXEC USP_RPT_BRAND_CAT_ACHEV_ORDER '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            dtPerformance = db.GetDataInDataSet(sql);
            return dtPerformance;
        }

        public DataTable BindPoStatus(string fromdtae,string todate, string Finyear, string depotid) /*new added by p.basu for po status report on 19112019*/
        {
            string sql = "EXEC USP_PROC_PO_SATUS  '"+ fromdtae + "','"+todate+"','" + Finyear + "','" + depotid + "'";
            DataTable dtpo = new DataTable();
            dtpo = db.GetData(sql);
            return dtpo;
        }

        public DataTable Depot_Accountsforfactory(string userid) /*new added by p.basu for po status report on 28112019*/
        {
            string SQL = "usp_bind_ho_fac_userwise '"+userid+"'";
            DataTable DT = new DataTable();
            DT = db.GetData(SQL);
            return DT;
        }

        public DataSet PRIMARY_SECONDARY_MISANALYSIS(string USERID, string MonthID, string Finyear, string DepotID, string RSMID, string ASMID, string SOID, string SHOWNFOR)
        {
            DataSet dtPerformance = new DataSet();
            string sql = " EXEC USP_RPT_PRIMARY_SECONDARY_MIS_SUMMARY '" + USERID + "','" + MonthID + "','" + Finyear + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + SHOWNFOR + "'";
            dtPerformance = db.GetDataInDataSet(sql);
            return dtPerformance;
        }

        public DataTable BindMonthName(string FinYear)
        {
            string sql = " EXEC USP_MONTH '"+ FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendor()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductbyVendore(string vendore)
        {
            string sql = "SELECT PRODUCTID,PRODUCTNAME FROM M_PRODUCT_TPU_MAP WHERE VENDORID='"+ vendore + "' ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindMaterialRejectoins(string fromdate, string todate, string depotid, string vendore,string product) 
        {
            string sql = "EXEC [USP_RPT_MATERIAL_REJECTION_DETAILS] '" + fromdate + "','" + todate + "','" + depotid + "','" + vendore + "','"+ product + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet MONTHWISE_ORDER_VS_INVOICE_ANALYSIS(string ASMID, string USERID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            string sql = " EXEC USP_RPT_MONTHLY_PARTYWISE_ORDERVSINVOICE '" + ASMID + "','" + USERID + "','" + Finyear + "'";
            dtPerformance = db.GetDataInDataSet(sql);
            return dtPerformance;
        }

        public DataSet bindConSumptionReport_JSON(string productid, string fromdate, string todate)
        {
            string sql = "EXEC [usp_rpt_raw_material_wise_counsmption] '" + productid + "','" + fromdate + "','" + todate + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable Bind_product()
        {
            string sql = "EXEC [USP_BIND_PRODUCT_WITH_OUT_FG]";
            DataTable ds = new DataTable();
            ds = db.GetData(sql);
            return ds;
        }
    }

    #endregion


   

}


