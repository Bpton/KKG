using System;
using System.Collections;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class Clsstockreportnew
    {
        DButility dbconn = new DButility();
        private object dt1;

        public DataSet Bind_UserInfo(string USERID)  //** SP CHANGE ** IF ANY ISSUE OCCURE PLESE USE OLD SP.SP NAME [USP_GET_USERINFO_ERP]//
        {
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_USERID", USERID);
            ds = dbconn.SysFetchDataInDataSetcube("[USP_GET_USERINFO_ERP_WITH_KOLKATA]", hashTable);

            return ds;
        }
        public DataTable BINDASMBYRSM(string rsmid)
        {
            string sql = "SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN (SELECT * FROM DBO.FNSPLIT('" + rsmid + "',',')) AND USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86'";
            DataSet dt = new DataSet();


            dt = dbconn.GetDataSet(sql);
            //dt = db.GetData(sql);
            return dt.Tables[0];
        }
        public DataTable BindDepotAll(string UserType, string UserID)
        {

            DataSet dt = new DataSet();
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
                dt = dbconn.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt.Tables[0];
        }

        public DataTable BindFinalDSRSummaryReport(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype, string TSID)
        {

            DataTable dt = new DataTable();

            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", FromDate);
            hashTable.Add("ToDate", ToDate);
            hashTable.Add("DepotID", DepotID);
            hashTable.Add("RSMID", RSMID);
            hashTable.Add("ASMID", ASMID);
            hashTable.Add("SOID", SOID);
            hashTable.Add("AMOUNT", Amount);
            hashTable.Add("AMT_TYPE", AmtType);
            hashTable.Add("ENTRY_TYPE", EntryType);
            hashTable.Add("USER_TYPE", usertype);
            hashTable.Add("TSID", TSID);

            dt = dbconn.getDataSetcube("[USP_RPT_FINAL_DSR_SUMMARY_REPORT]", hashTable);
            return dt;
        }
        public DataTable BindSO(string asmid)
        {

            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_ASMID", asmid);
            dt = dbconn.getDataSetcube("USP_SOLIST_ASMWISE", hashTable);
            return dt;
        }
        public DataTable BindRSMBYDEPOT(string depotid)
        {
            string sql = " SELECT DISTINCT A.USERID,LTRIM(RTRIM(RTRIM(RTRIM(A.FNAME) + ' ' + RTRIM(A.MNAME)) + ' ' + A.LNAME)) AS USERNAME FROM M_USER A WITH(NOLOCK) " +
                            " INNER JOIN M_TPU_USER_MAPPING B WITH(NOLOCK) ON A.USERID=B.USERID AND" +
                            " TPUID IN (SELECT ITEM FROM DBO.FNSPLIT('" + depotid + "',','))" +
                            " AND A.USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSetcube(sql);

            return dt.Tables[0];

        }
        public DataTable BindTSI(string asmid)
        {
            string sql = "SELECT USERID,(FNAME+' '+MNAME+' '+LNAME) AS USERNAME FROM M_USER WHERE REPORTINGTOID IN (SELECT * FROM DBO.FNSPLIT('" + asmid + "',',')) AND USERTYPE='DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindFinalDSRReport(string FromDate, string ToDate, string DepotID, string RSMID, string ASMID, string SOID, decimal Amount, string AmtType, string EntryType, string usertype)
        {
            //string sql = "EXEC [USP_RPT_FINAL_DSR_REPORT] '" + FromDate + "','" + ToDate + "','" + DepotID + "','" + RSMID + "','" + ASMID + "','" + SOID + "','" + Amount + "','" + AmtType + "','" + EntryType + "','" + usertype + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", FromDate);
            hashTable.Add("TODATE", ToDate);
            hashTable.Add("DEPOTID", DepotID);
            hashTable.Add("RSMID", RSMID);
            hashTable.Add("ASMID", ASMID);
            hashTable.Add("SOID", SOID);
            hashTable.Add("AMOUNT", Amount);
            hashTable.Add("AMT_TYPE", AmtType);
            hashTable.Add("ENTRY_TYPE", EntryType);
            hashTable.Add("USER_TYPE", usertype);
            //dt = dbconn.GetData(sql);
            dt = dbconn.getDataSetcube("USP_RPT_FINAL_DSR_REPORT", hashTable);
            return dt;
        }
        public DataSet BindSecondarySalesDMSHierarchywise(string fromdate, string todate, string userid)
        {
            //string sql = "EXEC [USP_RPT_SECONDARY_SALES_DUMP_DMS_HIERARCHYWISE_WITH_FREE_QTY] '" + fromdate + "','" + todate + "','" + userid + "'";
            DataSet dt1 = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FROMDATE", fromdate);
            hashTable.Add("TODATE", todate);
            //hashTable.Add("p_USERID", userid);
            //dt = db.GetData(sql);
            //dt = dbconn.getDataSet("USP_RPT_SECONDARY_SALES_DUMP_DMS_HIERARCHYWISE_WITH_FREE_QTY", hashTable);
            dt1 = dbconn.SysFetchDataInDataSetcube("USP_secondaryDump", hashTable);
            return dt1;
        }


        public DataTable BindDepot_Primary()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            // DataTable dt = new DataTable();
            DataSet dt = new DataSet();
            //dt = db.GetData(sql);
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];

        }
        public DataTable BindDeptName(string BRID)
        {
            //DataTable dt = new DataTable();
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' AND BRID='" + BRID + "' order by BRNAME";
            //dt = db.GetData(sql);
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
            //return dt;

        }
        public DataTable BindDepot_userwise(string userid)
        {
            // string sql = "EXEC [USP_RPT_BINDDEPOT] '" + userid + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_USERID", userid);
            dt = dbconn.getDataSet("USP_RPT_BINDDEPOT", hashTable);
            return dt;
        }
        public DataTable BindBusinessegment(string BSID)
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + BSID + "'),','))";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindBusinessegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME DESC ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindSizeInPack()
        {
            string sql = " SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID IN('1970C78A-D062-4FE9-85C2-3E12490463AF','B9F29D12-DE94-40F1-A668-C79BF1BF4425')";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindStoreLocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION ORDER BY NAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];

        }
        public DataTable BindBrandForPriceList()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION WHERE DIVID NOT IN ('E6B2F631-7237-4B97-BC33-B6FCF0B9075B') ORDER BY DIVNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindGT()
        {
            string sql = "SELECT BSID as id,BSNAME as NAME FROM M_BUSINESSSEGMENT WHERE BSID='7F62F951-9D1F-4B8D-803B-74EEBA468CEE'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindBusinessegment_PrimarySales_new(string usertype)
        {
            string sql = "EXEC [usp_BindBusinessegment_bind] '" + usertype + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("usrtype", usertype);
            //dt = dbconn.GetDataSet(sql);
            dt = dbconn.getDataSet("usp_BindBusinessegment_bind", hashTable);
            return dt;
        }
        public DataTable BindPartywiseSale(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string USERTYPE, string USERID)
        {
            //string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("DEPOT", depotid);
            hashTable.Add("HEADQUARTER", headqurtr);
            hashTable.Add("BSID", bsid);
            hashTable.Add("SALETYPE", saletype);
            hashTable.Add("AMTIN", AmountIn);
            //dt = db.GetData(sql);
            dt = dbconn.getDataSet("USP_RPT_PRIMARY_SALE_PARTYWISE", hashTable);

            return dt;
        }
        public DataTable BindHQbyUSER(string USERID)
        {
            //string sql = " EXEC usp_rpt_bind_HQbyUser '" + USERID + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_userid", USERID);
            //dt = db.GetData(sql);
            dt = dbconn.getDataSet("usp_rpt_bind_HQbyUser", hashTable);
            return dt;
        }
        public DataTable BindHQbyDepot(string DepotID)
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY HQNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCity()
        {
            string sql = " SELECT DISTINCT City_ID,City_Name FROM M_CITY ORDER BY City_Name";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindPartywiseSale_motherreport(string fromdate, string todate, string depotid, string headqurtr, string bsid, string saletype, Int32 AmountIn, string USERTYPE, string USERID, string rpttype)
        {
            //string sql = "EXEC [USP_RPT_PRIMARY_SALE_PARTYWISE] '" + fromdate + "','" + todate + "','" + depotid + "','" + headqurtr + "','" + bsid + "','" + saletype + "','" + AmountIn + "','" + USERTYPE + "','" + USERID + "','" + rpttype + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("DEPOT", depotid);
            hashTable.Add("HEADQUARTER", headqurtr);
            hashTable.Add("BSID", bsid);
            hashTable.Add("SALETYPE", saletype);
            hashTable.Add("AMTIN", AmountIn);
            hashTable.Add("usertype", USERTYPE);
            hashTable.Add("userid", USERID);
            hashTable.Add("target", rpttype);
            //dt = db.GetData(sql);
            dt = dbconn.getDataSet("USP_RPT_PRIMARY_SALE_PARTYWISE", hashTable);
            return dt;
        }
        public DataTable BindAssignto(string ID)
        {
            // string sql = "EXEC [USP_RPT_PARTYWISE_ASSIGNTO] '" + ID + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_DISTRIBUTORRID", ID);
            //dt = db.GetData(sql);
            dt = dbconn.getDataSet("USP_RPT_PARTYWISE_ASSIGNTO", hashTable);
            return dt;
        }
        public DataTable BindJC()
        {
            string sql = " SELECT JCID,NAME FROM M_JC ORDER BY NAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindTimeSpan_New(string Tag, string FinYear)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'AND FINYEAR='" + FinYear + "'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataSet BindSalesTaxSummary_Details_GST(string fromdate, string todate, string depotid, string
                SchemeStatus, string BSID, string type, string taxtypeid, string partyid, string InvoiceType, string DetailsType)
        {
            //string sql = "EXEC [USP_RPT_TAX_SUMMARY_REPORT_GST] '" + fromdate + "','" + todate + "','" +

            //depotid + "','" + SchemeStatus + "','" + BSID + "','" + type + "','" + taxtypeid + "','" + partyid + "','" + InvoiceType + "','" + DetailsType + "'";
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", fromdate);
            //hashTable.Add(",", ',');
            hashTable.Add("TODATE", todate);
            // hashTable.Add(",", ',');
            hashTable.Add("DEPOTID", depotid);
            // hashTable.Add(",", ',');
            hashTable.Add("SCHEME_STATUS", SchemeStatus);
            //hashTable.Add(",", ',');
            hashTable.Add("BusinessSegment", BSID);
            // hashTable.Add(",", ',');
            hashTable.Add("VoucherType", type);
            //hashTable.Add(",", ',');
            hashTable.Add("TaxType", taxtypeid);
            // hashTable.Add(",", ',');
            hashTable.Add("Party", partyid);
            //hashTable.Add(",", ',');
            hashTable.Add("InvoiceType", InvoiceType);
            //hashTable.Add(",", ',');
            hashTable.Add("DeatilsType", DetailsType);
            ds = dbconn.SysFetchDataInDataSet("[USP_RPT_TAX_SUMMARY_REPORT_GST]", hashTable);

            return ds;
        }
        public DataTable BindDepot()
        {
            //string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE  BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER),(SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID, BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'O' AND ISSHOWACCOUNTS='Y' UNION " +
               "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindDistributorbyAdmin()
        {
            DataSet dt_retailer = new DataSet();
            string retailerid = "SELECT RETAILERID FROM P_APPMASTER";
            dt_retailer = dbconn.GetDataSet(retailerid);
            string retailer = Convert.ToString(dt_retailer.Tables[0].Rows[0]["retailerid"]);
            //string retailer = Convert.ToString(dbconn.GetSingleValue(retailerid));

            string sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTYPE_ID NOT IN('" + retailer + "') ORDER BY CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);

            return dt.Tables[0];
        }
        public DataTable BindDistributorbyAdminWithDepot(string depotid)
        {
            string sql = "SELECT DISTINCT B.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + depotid + "',','))" +
                        " union all" +
                        " SELECT BRID AS ID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  AND " +
                        " BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER)," +
                        " (SELECT EXPORTPAKISTAN FROM P_APPMASTER),'" + depotid + "') ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCustomerbyDepot(string DepotID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',',')) ORDER BY CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindDepotWithoutDepot(string BRID)
        {
            string sql = "SELECT  DISTINCT BRID AS CUSTOMERID,BRPREFIX AS CUSTOMERNAME FROM M_BRANCH  WHERE  BRID NOT IN ('" + BRID + "') AND BRANCHTAG='D'  ORDER BY BRPREFIX";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataSet BindSummaryDetails_GST(string fromdate, string todate, string depotid, string partyname, string InvoiceType, string VoucherType)
        {
            //string sql = "EXEC USP_RPT_PURCHASE_TAX_SUMMARY_GST_CANCELLED '" + fromdate + "','" + todate + "','" + depotid
            // + "','" + partyname + "','" + InvoiceType + "','" + VoucherType + "'";
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", fromdate);
            hashTable.Add("TODATE", todate);
            hashTable.Add("DEPOTID", depotid);
            hashTable.Add("PARTYID", partyname);
            hashTable.Add("InvoiceType", InvoiceType);
            hashTable.Add("VoucherType", VoucherType);
            ds = dbconn.SysFetchDataInDataSet("[USP_RPT_PURCHASE_TAX_SUMMARY_GST_CANCELLED]", hashTable);

            return ds;
        }
        public DataTable Depot_Accounts(string userid)
        {
            //DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID" +
                      " WHERE B.USERID='" + userid + "'  ORDER BY BRNAME";
                //DataTable dt = new DataTable();
                DataSet dt1 = new DataSet();
                dt1 = dbconn.GetDataSet(sql);
                return dt1.Tables[0];
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            DataSet dt = new DataSet();
            return dt.Tables[0];
        }
        public DataTable BindTpu_Depot()
        {
            string sql = " SELECT VENDORID AS ID, VENDORNAME AS BRNAME FROM M_TPU_VENDOR WHERE TAG='T' AND SUPLIEDITEMID NOT IN('8','9')" +
                        " union all" +
                        " SELECT BRID AS ID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  AND " +
                        " BRID NOT IN((SELECT EXPORTBANGLADESH FROM P_APPMASTER),(SELECT EXPORTNEPAL FROM P_APPMASTER)," +
                        " (SELECT EXPORTPAKISTAN FROM P_APPMASTER)) ORDER BY BRNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindTPUPatyName()
        {
            string Sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE TAG='T' AND SUPLIEDITEMID NOT IN('8','9')";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(Sql);
            return dt.Tables[0];
        }
        public DataSet TSITRENDLINEANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            //string sql = " EXEC USP_RPT_DAILYTSV_TSI_PERFORMENCE '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            //dtPerformance = db.GetDataInDataSet(sql);
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TSIID", TSIID);
            hashTable.Add("P_USERID", USERID);
            hashTable.Add("P_MONTH", MonthID);
            hashTable.Add("P_FINYEAR", Finyear);
            dtPerformance = dbconn.SysFetchDataInDataSet("[USP_RPT_DAILYTSV_TSI_PERFORMENCE]", hashTable);

            //return ds;
            return dtPerformance;
        }
        public DataSet TSIPERFORMENCE(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtQtyInPCS = new DataSet();
            //string sql = " EXEC USP_RPT_TSI_PERFORMENCE '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            // dtQtyInPCS = db.GetDataInDataSet(sql);
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TSIID", TSIID);
            hashTable.Add("P_TSIID", USERID);
            hashTable.Add("P_TSIID", MonthID);
            hashTable.Add("P_TSIID", Finyear);
            dtQtyInPCS = dbconn.SysFetchDataInDataSet("[USP_RPT_DAILYTSV_TSI_PERFORMENCE]", hashTable);
            return dtQtyInPCS;
        }
        public DataSet TSIORDER_VS_INVOICE_ANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            //string sql = " EXEC USP_RPT_ORDER_VS_INVOICE_ANALYSIS '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            //dtPerformance = db.GetDataInDataSet(sql);
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TSIID", TSIID);
            hashTable.Add("P_USERID", USERID);
            hashTable.Add("P_MONTH", MonthID);
            hashTable.Add("P_FINYEAR", Finyear);
            dtPerformance = dbconn.SysFetchDataInDataSet("[USP_RPT_DAILYTSV_TSI_PERFORMENCE]", hashTable);
            return dtPerformance;
        }
        public DataSet MONTHWISE_ORDER_VS_INVOICE_ANALYSIS(string ASMID, string USERID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_ASMID", ASMID);
            hashTable.Add("P_USERID", USERID);
            hashTable.Add("P_FINYEAR", Finyear);

            dtPerformance = dbconn.SysFetchDataInDataSet("[USP_RPT_MONTHLY_PARTYWISE_ORDERVSINVOICE]", hashTable);
            return dtPerformance;
        }
        public DataSet TSIORDER_BRANDSKU_ANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            //string sql = " EXEC USP_RPT_BRAND_CAT_ACHEV_ORDER '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            //dtPerformance = db.GetDataInDataSet(sql);
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TSIID", TSIID);
            hashTable.Add("P_USERID", USERID);
            hashTable.Add("P_MONTH", MonthID);
            hashTable.Add("P_FINYEAR", Finyear);
            dtPerformance = dbconn.SysFetchDataInDataSet("[USP_RPT_BRAND_CAT_ACHEV_ORDER]", hashTable);
            return dtPerformance;
        }
        public DataSet TSI_INCENTIVE_ANALYSIS(string TSIID, string USERID, string MonthID, string Finyear)
        {
            DataSet dtPerformance = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TSIID", TSIID);
            hashTable.Add("P_USERID", USERID);
            hashTable.Add("P_MONTH", MonthID);
            hashTable.Add("P_FINYEAR", Finyear);
            //string sql = " EXEC USP_RPT_TSI_INCENTIVE_CALCULATION '" + TSIID + "','" + USERID + "','" + MonthID + "','" + Finyear + "'";
            dtPerformance = dbconn.SysFetchDataInDataSet("[USP_RPT_TSI_INCENTIVE_CALCULATION]", hashTable);
            return dtPerformance;
        }
        public DataTable Region_foraccounts(string UserType, string UserID)
        {
            //DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'O' AND ISSHOWACCOUNTS='Y' UNION " +
                       "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' AND ISSHOWACCOUNTS='Y' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX ASBRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                       " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
                    DataSet dt1 = new DataSet();
                    dt1 = dbconn.GetDataSet(sql);
                    return dt1.Tables[0];
                }
                else
                {
                    sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "' AND ISSHOWACCOUNTS='Y'  ORDER BY BRNAME";
                    DataSet dt1 = new DataSet();
                    dt1 = dbconn.GetDataSet(sql);
                    return dt1.Tables[0];
                }

                //DataSet dt1 = new DataSet();
                //dt1 = dbconn.GetDataSet(sql);
                ////return dt1.Tables[0];
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            DataSet dt = new DataSet();
            return dt.Tables[0];
        }
        public DataSet BindBalanceSheetV2(string ToDate, string RegionID, string FinYear, string Type, string Reporttype)
        {
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TODATE", ToDate);
            hashTable.Add("P_REGION", RegionID);
            hashTable.Add("P_FINYEAR", FinYear);
            hashTable.Add("P_ZERO", Type);
            hashTable.Add("P_TYPE", Reporttype);
            //string sql = "EXEC [USP_RPT_BLSHEET] '" + RegionID + "','" + ToDate.ToString() + "','" + FinYear + "','" + Type + "'," + 0 + ",'" + Reporttype + "'";
            //DataSet ds = new DataSet();
            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_BLSHEET]", hashTable);
            return dt;
        }
        public DataSet BindProfitLossV2(string ToDate, string RegionID, string FinYear, string Type)
        {
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_TODATE", ToDate);
            hashTable.Add("P_REGION", RegionID);
            hashTable.Add("P_FINYEAR", FinYear);
            hashTable.Add("P_ZERO", Type);
            //string sql = "EXEC [USP_RPT_PLSHEET] '" + RegionID + "','" + ToDate.ToString() + "','" + FinYear + "','" + Type + "'";
            //DataSet ds = new DataSet();
            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_PLSHEET]", hashTable);
            return dt;
        }
        public string BindBranchName(string BRID)
        {
            string sql = "SELECT BRNAME FROM M_BRANCH  WHERE BRID='" + BRID + "'  ";

            // string name = "";
            DataSet name = new DataSet();
            // DataSet name  = new DataSet();
            name = dbconn.GetDataSet(sql);
            return name.Tables[0].ToString();
        }
        public DataTable BindConsolidateTrialbalance(string Fromdate, string Todate, string Finyear)
        {
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_FROMDATE", Fromdate);
            hashTable.Add("p_TODATE", Todate);
            hashTable.Add("p_Finyear", Finyear);
            //string sql = "EXEC [USP_RPT_TRAILBALANCE_CONSOLIDATE] '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            dt = dbconn.getDataSet("USP_RPT_TRAILBALANCE_CONSOLIDATE", hashTable);
            return dt;
        }
        public DataTable BindAccountsGroup()
        {
            //DataTable dt = new DataTable();
            string sql = "SELECT Code,grpName FROM Acc_AccountGroup ORDER BY grpName";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCostCenter()
        {
            string sql = "SELECT COSTCENTREID,COSTCENTRENAME FROM M_COST_CENTRE ORDER BY COSTCENTRENAME";
            // DataTable dt = new DataTable();
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindDivison()
        {
            string sql = "SELECT DIVID , DIVNAME  FROM M_DIVISION ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindDepartment()
        {
            string sql = "SELECT DEPTID AS ID, DEPTNAME  FROM M_DEPARTMENT ORDER BY DEPTNAME ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindLedgerForCostCenterVoucher()
        {
            string sql = string.Empty;
            // DataTable dt = new DataTable();
            sql = "SELECT DISTINCT Id AS LedgerId,name AS LedgerName from ACC_ACCOUNTSINFO WHERE name <> '' AND costcenter ='Y' ORDER BY LedgerName";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCostCenterBalance(string FromDate, string ToDate, string RegionID, string COSTCATEGORYID, string ACCGROUPID, string LEDGERID, string COSTCENTERID, string DEPARMENTID, string SEGMENTID, string PRODUCTID)
        {
            //string sql = "EXEC [USP_COSTCENTERSUMMARY] '" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + RegionID + "','" + COSTCATEGORYID + "','" + ACCGROUPID + "','" + LEDGERID + "','" + COSTCENTERID + "','" + DEPARMENTID + "','" + SEGMENTID + "','" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FROMDATE", FromDate);
            hashTable.Add("TODATE", ToDate);
            hashTable.Add("REGION", RegionID);
            hashTable.Add("COSTCATEGORY", COSTCATEGORYID);
            hashTable.Add("ACCGROUPID", ACCGROUPID);
            hashTable.Add("LEDGERS", LEDGERID);
            hashTable.Add("COSTCENTER", COSTCENTERID);
            hashTable.Add("DEPARMENT", DEPARMENTID);
            hashTable.Add("SEGMENT", SEGMENTID);
            hashTable.Add("PRODUCT", PRODUCTID);
            dt = dbconn.getDataSet("USP_COSTCENTERSUMMARY", hashTable);
            return dt;
        }
        public DataTable BindCostCategory()
        {
            string sql = "SELECT COSTCATID, COSTCATNAME  FROM M_COST_CATEGORY ORDER BY COSTCATNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindTimeSpan(string Tag)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable FetchDateRange(string span, string Tag)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                //sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
                DataTable dt1 = new DataTable();
                Hashtable hashTable = new Hashtable();
                hashTable.Add("p_SPANDATE", span);
                hashTable.Add("p_TAG", Tag);
                //hashTable.Add("P_FINYEAR", FinYear);
                dt1 = dbconn.getDataSet("USP_FETCH_DATERANGE", hashTable);
                return dt1;
            }
            else
            {
                //sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
                DataTable dt = new DataTable();
                Hashtable hashTable = new Hashtable();
                hashTable.Add("p_SPANDATE", span);
                hashTable.Add("p_TAG", Tag);
                // hashTable.Add("P_FINYEAR", FinYear);
                dt = dbconn.getDataSet("USP_FETCH_DATERANGE", hashTable);
                return dt;
            }




        }
        public DataTable BindForLedger_ACCGROUPWise_CostCenter_Summary(String CODE)
        {
            string sql = string.Empty;
            //DataTable dt = new DataTable();
            sql = "SELECT ID AS LEDGERID,NAME AS LEDGERNAME FROM ACC_ACCOUNTSINFO AS A " +
                  "INNER JOIN ACC_ACCOUNTGROUP AS B ON A.ACTGRPCODE=B.CODE WHERE B.CODE IN (SELECT * from dbo.fnSplit('" + CODE + "',',')) AND costcenter ='Y' ORDER BY LEDGERNAME";

            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindProductBrandWise(string DIVID)
        {
            string sql = "SELECT DISTINCT ID,PRODUCTALIAS FROM M_PRODUCT WHERE DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) ORDER BY PRODUCTALIAS";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCostCenterVoucherDetailsV2(string FromDate, string ToDate, string RegionID, string CostCenterID, string LedgerId)
        {
            //DataTable dt = new DataTable();
            //string sql = "EXEC [USP_COSTCENTERDETAILS] '" + FromDate + "','" + ToDate + "','" + RegionID + "','" + CostCenterID + "','" + LedgerId + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FROMDATE", FromDate);
            hashTable.Add("TODATE", ToDate);
            hashTable.Add("REGION", RegionID);
            hashTable.Add("COSTCENTER", CostCenterID);
            hashTable.Add("LEDGER", LedgerId);
            // hashTable.Add("P_FINYEAR", FinYear);
            dt1 = dbconn.getDataSet("USP_COSTCENTERDETAILS", hashTable);
            return dt1;
        }
        public DataTable BindForLedgerReport_DepotWise(String DepotID)
        {
            string sql = string.Empty;
            //DataTable dt = new DataTable();

            sql = " SELECT DISTINCT Id AS LedgerId,name + ' ( ' + ISNULL(B.grpName,'') + ' ) ' AS LedgerName from ACC_ACCOUNTSINFO A " +
                  "LEFT JOIN Acc_AccountGroup B ON A.actGrpCode = B.CODE " +
                  "INNER JOIN Acc_Branch_Mapping M  ON A.Id = M.LedgerID WHERE BranchID IN(SELECT * FROM DBO.fnSplit('" + DepotID + "', ','))" +
                  " AND name <> '' ORDER BY LedgerName";

            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable FetchDateRange(string span, string Tag, string FinYear)
        {
            string sql = string.Empty;
            if (Tag == "5")
            {
                //sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
                DataTable dt1 = new DataTable();
                Hashtable hashTable = new Hashtable();
                hashTable.Add("p_SPANDATE", span);
                hashTable.Add("p_TAG", Tag);
                hashTable.Add("P_FINYEAR", FinYear);
                dt1 = dbconn.getDataSet("USP_FETCH_DATERANGE_NEW", hashTable);
                return dt1;
            }
            else
            {
                //sql = " [USP_FETCH_DATERANGE_NEW] '" + span + "','" + Tag + "','" + FinYear + "'";
                DataTable dt1 = new DataTable();
                Hashtable hashTable = new Hashtable();
                hashTable.Add("p_SPANDATE", span);
                hashTable.Add("p_TAG", Tag);
                hashTable.Add("P_FINYEAR", FinYear);
                dt1 = dbconn.getDataSet("USP_FETCH_DATERANGE_NEW", hashTable);
                return dt1;
            }



        }
        public DataSet BindLedgerReport(string fromdate, string todate, string ledgerid, string depot, string Sessionid)
        {
            //string sql = "EXEC [USP_RPT_LEDGER_DETAILS] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + depot + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FRMDATE", fromdate);
            hashTable.Add("P_TODATE", todate);
            hashTable.Add("P_LEDGERID", ledgerid);
            hashTable.Add("P_REGIONID", depot);
            hashTable.Add("Finyr", Sessionid);
            ds = dbconn.SysFetchDataInDataSet("[USP_RPT_LEDGER_DETAILS]", hashTable);
            return ds;
        }
        public string GetFinYearID(string Session)
        {
            string sql = "SELECT id FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            //string value = Convert.ToString(db.GetSingleValue(sql));
            //return value;
            DataSet value = new DataSet();
            // DataSet name  = new DataSet();
            value = dbconn.GetDataSet(sql);
            return value.Tables[0].ToString();
        }
        public DataTable BindVoucher_credit_debit(string AccEntry_ID)
        {
            //string sql = "EXEC [USP_RPT_DEBIT_CREDIT_LEDGER] '" + AccEntry_ID + "'";

            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_ACC_ENTRYID", AccEntry_ID);

            dt1 = dbconn.getDataSet("USP_RPT_DEBIT_CREDIT_LEDGER", hashTable);
            return dt1;
        }
        public DataSet BindAccVoucher_Ledger_Voucher(string LedgerID, string AccEntryID)
        {
            //string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART3] '" + LedgerID + "','" + AccEntryID + "'";
            DataSet dt1 = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_LedgerID", LedgerID);
            hashTable.Add("p_AccEntryID", AccEntryID);

            dt1 = dbconn.SysFetchDataInDataSet("[USP_RPT_ACC_VOUCHERDETAILS_PART3]", hashTable);
            return dt1;
        }
        public DataSet InvoiceDetails_ForPrint(string InvoiceID)
        {
            // DataSet dt = new DataSet();
            //string sql = "EXEC USP_RPT_LEDGER_REPORT_PRINT '" + InvoiceID + "'";
            DataSet dt1 = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_INVOICEID", InvoiceID);
            dt1 = dbconn.SysFetchDataInDataSet("[USP_RPT_LEDGER_REPORT_PRINT]", hashTable);
            return dt1;
        }
        public DataSet InvoiceDetails_ForPrintAcc(string InvoiceID)
        {
            // DataSet dt = new DataSet();
            //string sql = "EXEC USP_RPT_LEDGER_REPORT_ACCPRINT '" + InvoiceID + "'";
            DataSet dt1 = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_INVOICEID", InvoiceID);
            dt1 = dbconn.SysFetchDataInDataSet("[USP_RPT_LEDGER_REPORT_ACCPRINT]", hashTable);
            return dt1;
        }
        public DataTable BindDepoForCostcenterReport()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID <>'0EEDDA49-C3AB-416A-8A44-0B9DFECD6670'";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindLedgerWiseCostCenter(string FromDate, string ToDate, string RegionID, string AccGroupID, string BSID, string LedgerId, string ddltype) /*new param add by s.basu on 19032020*/
        {
            // DataTable dt = new DataTable();
            //string sql = "EXEC [USP_RPT_LEDGERWISE_COSTCENTRE] '" + FromDate + "','" + ToDate + "','" + RegionID + "','" + AccGroupID + "','" + BSID + "','" + LedgerId + "','" + ddltype + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_FROMDATE", FromDate);
            hashTable.Add("p_TODATE", ToDate);
            hashTable.Add("p_BRANCHID", RegionID);
            hashTable.Add("p_ACCOUNTSGROUPID", AccGroupID);
            hashTable.Add("p_BSID", BSID);
            hashTable.Add("p_LEDGER", LedgerId);
            hashTable.Add("p_type", ddltype);

            dt1 = dbconn.getDataSet("USP_RPT_LEDGERWISE_COSTCENTRE", hashTable);
            return dt1;
        }
        public string GetStartDateOfFinYear(string Session)
        {
            //string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            //STARTDATE = Convert.ToString(db.GetSingleValue(sql1));
            // string STARTDATE = string.Empty;

            //return STARTDATE;
            DataSet STARTDATE = new DataSet();
            // DataSet name  = new DataSet();
            STARTDATE = dbconn.GetDataSet(sql1);
            return STARTDATE.Tables[0].ToString();
        }
        public DataTable BindTrialBalance(string FromDate, string ToDate, string RegionID, string FinYear, string Type)
        {
            //string sql = "EXEC [USP_RPT_TRAILBALANCE] '" + RegionID + "','" + FromDate.ToString() + "','" + ToDate.ToString() + "','" + FinYear + "','" + Type + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_FROMDATE", FromDate);
            hashTable.Add("p_TODATE", ToDate);
            hashTable.Add("p_REGION", RegionID);
            hashTable.Add("p_Finyear", FinYear);
            hashTable.Add("p_mode", Type);


            dt1 = dbconn.getDataSet("USP_RPT_TRAILBALANCE", hashTable);
            return dt1;
        }
        public DataTable BindCostCenterReportV2(string FromDate, string ToDate, string SearchTag, string TSID, string AccGroupID, string BRID, string CCID, string LedgerId, string ddltype)
        {
            //DataTable dt = new DataTable();
            //string sql = "EXEC [USP_RPT_COSTCENTER_REPORT_V2] '" + FromDate + "','" + ToDate + "','" + SearchTag + "','" + TSID + "','" + AccGroupID + "','" + BRID + "','" + CCID + "','" + LedgerId + "' ,'" + ddltype + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_FROMDATE", FromDate);
            hashTable.Add("p_TODATE", ToDate);
            hashTable.Add("SEARCHTAG", SearchTag);
            hashTable.Add("TSID", TSID);
            hashTable.Add("GROUP", AccGroupID);
            hashTable.Add("BRANCHID", BRID);
            hashTable.Add("COSTCENTERID", CCID);
            hashTable.Add("LEDGERID", LedgerId);
            hashTable.Add("P_TYPE", ddltype);


            dt1 = dbconn.getDataSet("USP_RPT_COSTCENTER_REPORT_V2", hashTable);
            return dt1;
        }
        public DataTable BindGrpSummaryTrialbalance(string Fromdate, string Todate, string Finyear, string RegionID, string GroupID, string ShowType)
        {
            //DataTable dt = new DataTable();
            // string sql = "EXEC [USP_RPT_TRAILBALANCE_GRP_SUMMARY] '" + RegionID + "','" + GroupID + "','" + ShowType + "', '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_REGION", RegionID);
            hashTable.Add("p_GroupID", GroupID);
            hashTable.Add("p_ShowTYpe", ShowType);
            hashTable.Add("p_FROMDATE", Fromdate);
            hashTable.Add("p_TODATE", Todate);
            hashTable.Add("p_Finyear", Finyear);
            dt1 = dbconn.getDataSet("USP_RPT_TRAILBALANCE_GRP_SUMMARY", hashTable);
            return dt1;
        }
        public DataTable BindGrpSummaryTrialbalance_multi(string Fromdate, string Todate, string Finyear, string RegionID, string GroupID, string GroupName)
        {
            // DataTable dt = new DataTable();
            //string sql = "EXEC [USP_RPT_TRAILBALANCE_GRP_SUMMARY_MULTI] '" + RegionID + "','" + GroupID + "','" + GroupName + "', '" + Fromdate + "','" + Todate + "','" + Finyear + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_REGION", RegionID);
            hashTable.Add("p_GroupID", GroupID);
            hashTable.Add("p_GroupName", GroupName);
            hashTable.Add("p_FROMDATE", Fromdate);
            hashTable.Add("p_TODATE", Todate);
            hashTable.Add("p_Finyear", Finyear);
            dt1 = dbconn.getDataSet("USP_RPT_TRAILBALANCE_GRP_SUMMARY_MULTI", hashTable);
            return dt1;
        }
        public DataTable VoucherType()
        {
            string Sql = "";
            // DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select Id ,VoucherName from Acc_VoucherTypes Order by VoucherName";
                DataSet dt1 = new DataSet();
                dt1 = dbconn.GetDataSet(Sql);
                return dt1.Tables[0];
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            DataSet dt = new DataSet();
            return dt.Tables[0];
        }
        public DataTable BindAccVoucher(string fromdate, string todate, string VoucherTypeID, string Mode, string region)
        {
            // string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART1] '" + fromdate + "','" + todate + "','" + VoucherTypeID + "','" + Mode + "', '" + region + "' ";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FRMDATE", fromdate);
            hashTable.Add("P_TODATE", todate);
            hashTable.Add("P_VoucherTypeID", VoucherTypeID);
            hashTable.Add("P_Mode", Mode);
            hashTable.Add("P_RegionID", region);
            dt1 = dbconn.getDataSet("USP_RPT_ACC_VOUCHERDETAILS_PART1", hashTable);
            return dt1;
        }
        public DataTable BindAccVoucher_Ledger(string AccEntryId)
        {
            string sql = "EXEC [USP_RPT_ACC_VOUCHERDETAILS_PART2] '" + AccEntryId + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_AccEntryId", AccEntryId);
            dt1 = dbconn.getDataSet("USP_RPT_ACC_VOUCHERDETAILS_PART2", hashTable);
            return dt1;
        }
        public DataTable BindBrand()
        {
            string sql = " SELECT DIVID,DIVNAME FROM M_DIVISION " +//Add by Rajeev (24-03-2018)
                         " UNION ALL" +
                         " SELECT CONVERT (VARCHAR(5), ID)AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ID NOT IN(1,9,10)";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCategory()
        {
            //string sql = "SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY UNION ALL SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE ";
            /*string sql = " SELECT DISTINCT CATID,CATNAME FROM M_CATEGORY  WHERE CATCODE NOT IN('201','190','20','230','210','60')" +//Add by Rajeev (24-03-2018)
                         " UNION ALL " +
                         " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID IN('4','2')";*/
            string sql = " SELECT DISTINCT A.CATID,B.CATNAME FROM M_CATEGORY A INNER JOIN  M_PRODUCT B " +
                            " ON A.CATID = B.CATID " +
                            " UNION ALL  " +
                            " SELECT DISTINCT CONVERT(VARCHAR(50),SUBTYPEID),SUBITEMNAME " +
                            " FROM M_PRIMARY_SUB_ITEM_TYPE ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];

        }
        public DataTable BindGroup()
        {
            string sql = "SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY ORDER BY DIS_CATNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCategorybybrand(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE  DIVID  IN('" + brandid + "') ORDER BY CATNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindProductbybrandandcatid(string divid, string catid)
        {
            string sql = "SELECT DISTINCT ID,NAME FROM M_PRODUCT WHERE DIVID IN(SELECT *  FROM DBO.FNSPLIT('" + divid + "',',')) AND CATID IN(SELECT *  FROM DBO.FNSPLIT('" + catid + "',',')) ORDER BY NAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindDepotwiseSales(string caldetails, string spamid, string fromdate, string todate, string groupid, string productid, string BSID)
        {
            //string sql = "EXEC [USP_RPT_DEPOTWISE_SALE_REPORT] '" + caldetails + "','" + spamid + "','" + fromdate + "','" + todate + "','" + groupid + "','" + productid + "','" + BSID + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FORMATETYPE", caldetails);
            hashTable.Add("SPAM", spamid);
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("GROUPID", groupid);
            hashTable.Add("PRODUCT", productid);
            hashTable.Add("BSID", BSID);
            dt1 = dbconn.getDataSet("USP_RPT_DEPOTWISE_SALE_REPORT", hashTable);
            return dt1;
        }
        public DataTable LedgerOutstandiing(string FROMDATE, string TODATE, string customerid)
        {

            //DataTable dt = new DataTable();
            //string sql = "EXEC [USP_RPT_LEDGER_OUTSTANDING] '" + FROMDATE + "','" + TODATE + "','" + customerid + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FRMDATE", FROMDATE);
            hashTable.Add("P_TODAET", TODATE);
            hashTable.Add("P_CUSTOMERID", customerid);

            dt1 = dbconn.getDataSet("USP_RPT_LEDGER_OUTSTANDING", hashTable);
            return dt1;

        }
        public DataTable LedgerOutstandiing_Child(string InvID)
        {

            //DataTable dt = new DataTable();
            //string sql = "EXEC [USP_RPT_LEDGER_OUTSTANDING_CHILD] '" + InvID + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_INVOICEID", InvID);
          
            dt1 = dbconn.getDataSet("USP_RPT_LEDGER_OUTSTANDING_CHILD", hashTable);
            return dt1;


        }
        public DataSet BindLedgerReport_JSON(string fromdate, string todate, string ledgerid, string depot, string viewmode, string Sessionid)
        {
           // string sql = "EXEC [USP_RPT_LEDGER_DETAILS_JSON] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + depot + "','" + viewmode + "','" + Sessionid + "'";
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FRMDATE", fromdate);
            hashTable.Add("P_TODATE", todate);
            hashTable.Add("P_LEDGERID", ledgerid);
            hashTable.Add("P_REGIONID", depot);
            hashTable.Add("P_ViewMode", viewmode);
            hashTable.Add("Finyr", Sessionid);
            ds = dbconn.SysFetchDataInDataSet("[USP_RPT_LEDGER_DETAILS_JSON]", hashTable);
            return ds;
        }
        public DataTable Bind_UserInfo_tsisale(string USERID)
        {
           // DataTable ds = new DataTable();
           // string sql = "[dbo].[USP_GET_USERINFO_ASM] '" + USERID + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_USERID", USERID);

            dt1 = dbconn.getDataSet("USP_GET_USERINFO_ASM", hashTable);
            return dt1;
        }
        public DataTable BindTsi(string soid)
        {
           // string sql = "EXEC USP_TSILIST_SOWISE '" + soid + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_SOID", soid);

            dt1 = dbconn.getDataSet("USP_TSILIST_SOWISE", hashTable);
            return dt1;
        }
        public string GetUsertype(string USERID)
        {
           // string Usertype = string.Empty;
            string sql = "SELECT USERTYPE FROM M_USER WHERE USERID='" + USERID + "'";
            DataSet Usertype = new DataSet();
            //Usertype = Convert.ToString(db.GetSingleValue(sql));
            //return Usertype;
            Usertype = dbconn.GetDataSet(sql);
            return Usertype.Tables[0].ToString();
        }
        public DataTable BindCustomerbyDepotAndASMHQ(string DepotID, string UserID) /*SUNNY*/
        {
            string sql = "  SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID INNER JOIN M_CUSTOMER_HQ_MAPPING HQ ON HQ.CUSTOMERID=A.CUSTOMERID " +
                         "  WHERE A.CUSTOMERID IN( select *  from dbo.fnSplit((select DISTRIBUTORLIST from M_USER WHERE USERID='" + UserID + "'),','))" +
                         "  AND B.DEPOTID IN(SELECT *  FROM DBO.FNSPLIT('" + DepotID + "',','))  AND HQ.HQID IN( select *  from dbo.fnSplit((select HQLIST from M_USER WHERE USERID='" + UserID + "'),','))";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindProductWiseSale(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string UserID, string ProductType)
        {
           // string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE_ASMWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + UserID + "','" + ProductType + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("DEPOT", depot);
            hashTable.Add("HEADQUARTER", HeadQuarter);
            hashTable.Add("BSID", bsid);
            hashTable.Add("SALETYPE", saletype);
            hashTable.Add("PARTY", party);
            hashTable.Add("AMTIN", AmtIn);
            hashTable.Add("ASMID", UserID);
            hashTable.Add("ProductType", ProductType);
            dt1 = dbconn.getDataSet("USP_RPT_PRIMARY_SALE_PRODUCTWISE_ASMWISE", hashTable);
            return dt1;
        }
        public DataTable BindProductWiseSale(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string ProductType)
        {
           // string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + ProductType + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("DEPOT", depot);
            hashTable.Add("HEADQUARTER", HeadQuarter);
            hashTable.Add("BSID", bsid);
            hashTable.Add("SALETYPE", saletype);
            hashTable.Add("PARTY", party);
            hashTable.Add("AMTIN", AmtIn);
            hashTable.Add("ProductType", ProductType);
            dt1 = dbconn.getDataSet("USP_RPT_PRIMARY_SALE_PRODUCTWISE", hashTable);
            return dt1;
        }
        //public DataTable Region(string UserType, string UserID)
        //{
        //    //DataTable dt = new DataTable();
        //    try
        //    {
        //        string sql = "";
        //        if (UserType == "admin")
        //        {
        //            sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
        //               " WHERE  BRANCHTAG = 'O'  UNION " +
        //               "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
        //               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'  UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX ASBRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
        //               " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE'  ORDER BY SEQUENCE,BRNAME";
        //            //DataSet dt = new DataSet();
        //            //dt = dbconn.GetDataSet(sql);
        //            //return dt.Tables[0];
        //        }
        //        else
        //        {
        //            sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "'  ORDER BY BRNAME";
        //        }
        //        DataSet dt1 = new DataSet();
        //        dt1 = dbconn.GetDataSet(sql);
        //      return dt1.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogFiles Errlog = new CreateLogFiles();
        //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
        //    }
        //    //return dt1.Tables[0];
        //}
        public DataTable BindCitybyHO()
        {
            string sql = " SELECT DISTINCT HQID,HQNAME FROM M_HEADQUARTER ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindProductWiseSale_motherreport(string fromdate, string todate, string depot, string HeadQuarter, string bsid, string saletype, string party, Int32 AmtIn, string ProductType, string rpttype)
        {
            //string sql = "EXEC [USP_RPT_PRIMARY_SALE_PRODUCTWISE] '" + fromdate + "','" + todate + "','" + depot + "','" + HeadQuarter + "','" + bsid + "','" + saletype + "','" + party + "','" + AmtIn + "','" + ProductType + "','" + rpttype + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRM_DATE", fromdate);
            hashTable.Add("TO_DATE", todate);
            hashTable.Add("DEPOT", depot);
            hashTable.Add("HEADQUARTER", HeadQuarter);
            hashTable.Add("BSID", bsid);
            hashTable.Add("SALETYPE", saletype);
            hashTable.Add("PARTY", party);
            hashTable.Add("AMTIN", AmtIn);
            hashTable.Add("ProductType", ProductType);
            hashTable.Add("target", rpttype);
            dt1 = dbconn.getDataSet("USP_RPT_PRIMARY_SALE_PRODUCTWISE", hashTable);
            return dt1;
        }
        public DataTable BindPartyBSWise(string BusinessSegmentID, string DEPOTID)
        {
            string sql = " SELECT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID = B.CUSTOMERID WHERE BUSINESSSEGMENTID IN (SELECT *  FROM DBO.FNSPLIT('" + BusinessSegmentID + "',',')) AND B.DEPOTID='" + DEPOTID + "' ORDER BY CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindPartyHQWise(string HQID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_HQ_MAPPING WHERE HQID IN (SELECT *  FROM DBO.FNSPLIT('" + HQID + "',',')) ORDER BY CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCustomerbyId(string ID)
        {
            string sql = " SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID = '" + ID + "' ORDER BY CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCollectionGrid(string DistributorID, string Fromdate, string Todate)
        {
            // string sql = "EXEC [USP_RPT_SALE_INVOICE_OUTSTANDING_TOTAL] '" + DistributorID + "','" + Fromdate + "','" + Todate + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_DistributorId", DistributorID);
            hashTable.Add("p_FROMDATE", Fromdate);
            hashTable.Add("p_TODATE", Todate);
        
            dt1 = dbconn.getDataSet("USP_RPT_SALE_INVOICE_OUTSTANDING_TOTAL", hashTable);
            return dt1;

        }
        public DataTable BindCustomerForReport()
        {
           // string sql = "EXEC USP_ALL_CUSTOMER_FOR_REPORT";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
        
            dt1 = dbconn.getDataSet("USP_ALL_CUSTOMER_FOR_REPORT", hashTable);
            return dt1;
        }
        public DataTable BindBusinessegment_PrimarySales()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME ASC ";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataSet BindPartyOnLoad(string fromdate, string todate, string depot, string Scheme, string bsid, string CustomerID, string UserID)
        {
          //string sql = "EXEC [USP_RPT_PRIMARY_INVOICE_REPORT] '" + fromdate + "','" + todate + "','" + depot + "','" + Scheme + "','" + bsid + "','1','" + CustomerID + "','" + UserID + "'";
            DataSet  dt1 = new DataSet ();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", fromdate);
            hashTable.Add("TODATE", todate);
            hashTable.Add("DEPOTID", depot);
            hashTable.Add("SCHEME_STATUS", Scheme);
            hashTable.Add("BusinessSegment", bsid);
            hashTable.Add("PARTYID", CustomerID);
            hashTable.Add("ASMID", UserID);
            dt1 = dbconn.SysFetchDataInDataSet("[USP_RPT_PRIMARY_INVOICE_REPORT]", hashTable);
            return dt1;
        }
        public DataSet BindSalesTaxSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn)
        {
          //  string sql = "EXEC [USP_RPT_PRIMARY_TAX_SUMMARY_REPORT] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "'";
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", fromdate);
            hashTable.Add("TODATE", todate);
            hashTable.Add("DEPOTID", depotid);
            hashTable.Add("SCHEME_STATUS", SchemeStatus);
            hashTable.Add("BusinessSegment", BSID);
            hashTable.Add("AMTIN", AmtIn);

            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_PRIMARY_TAX_SUMMARY_REPORT]", hashTable);
            return dt;
        }
        public DataSet BindSaleReturnSummaryReport(string fromdate, string todate, string depotid, string SchemeStatus, string BSID, Int32 AmtIn)
        {
           // string sql = "EXEC [USP_RPT_PRIMARY_TAX_SUMMARY_REPORT_RETURN] '" + fromdate + "','" + todate + "','" + depotid + "','" + SchemeStatus + "','" + BSID + "','" + AmtIn + "'";
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("FRMDATE", fromdate);
            hashTable.Add("TODATE", todate);
            hashTable.Add("DEPOTID", depotid);
            hashTable.Add("SCHEME_STATUS", SchemeStatus);
            hashTable.Add("BusinessSegment", BSID);
            hashTable.Add("AMTIN", AmtIn);

            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_PRIMARY_TAX_SUMMARY_REPORT_RETURN]", hashTable);
            return dt;
        }
        public DataTable BindDepot(string BRID)
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH  WHERE BRID='" + BRID + "' AND BRANCHTAG='D'  ORDER BY BRNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindZone()
        {
            string sql = "SELECT DISTINCT ZONE AS ZONE_ID,CASE WHEN ZONE='N' THEN 'North'WHEN ZONE='C' THEN 'Central'WHEN ZONE='E' THEN 'East'WHEN ZONE='S' THEN 'South'WHEN ZONE='W' THEN 'West'WHEN ZONE='NE' THEN 'North East'Else 'Central'END AS Zone FROM M_REGION";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable BindCategoryWise_Sales(string fromdate, string todate, string zone)
        {
           // string sql = "EXEC [USP_RPT_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FORMDT", fromdate);
            hashTable.Add("P_TODT", todate);
            hashTable.Add("P_ZONE", zone);
           
            dt1 = dbconn.getDataSet("USP_RPT_CATEGORYWISE_SALES", hashTable);
            return dt1;
        }
        public DataTable BindZoneWise_Sales(string fromdate, string todate, string zone)
        {
           // string sql = "EXEC [USP_RPT_CATEGORYWISE_ZONEWISE] '" + fromdate + "','" + todate + "','" + zone + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FORMDT", fromdate);
            hashTable.Add("P_TODT", todate);
            hashTable.Add("P_ZONE", zone);

            dt1 = dbconn.getDataSet("USP_RPT_CATEGORYWISE_ZONEWISE", hashTable);
            return dt1;
        }
        public DataTable BindCustomerwiseReport(string fromdate, string todate, string groupid)
        {
            //string sql = "EXEC [USP_RPT_CUSTOMER_CATEGORYWISE_SALES] '" + fromdate + "','" + todate + "','" + groupid + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_FORMDATE", fromdate);
            hashTable.Add("P_TODATE", todate);
            hashTable.Add("P_GROUP", groupid);
      
            dt1 = dbconn.getDataSet("USP_RPT_CUSTOMER_CATEGORYWISE_SALES", hashTable);
            return dt1;
        }
        public DataTable BindDistributorso(string soid)
        {
            string sql = " SELECT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_TSI_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID " +
                         " WHERE B.ISACTIVE='True' AND A.TSI_ID IN (SELECT * FROM DBO.FNSPLIT('" + soid + "',','))  ORDER BY B.CUSTOMERNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }
        public DataTable Bindworkingfor_pgp(string Userid, string mode)
        {
            //DataTable dt = new DataTable();
            // string sql = "EXEC [USP_SALES_PGP_WORK_HIERARCHY] '" + Userid + "'," + mode + "";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_userid", Userid);
            hashTable.Add("p_mode ", mode);
         
            dt1 = dbconn.getDataSet("USP_SALES_PGP_WORK_HIERARCHY", hashTable);
            return dt1;
        }
        public DataTable BindLedgerReportForASM(string ledgerid, string fromdate, string todate, string Sessionid)
        {

            //DataTable dt = new DataTable();
           // string sql = "EXEC [USP_RPT_TRAILBALANCE_SO] '" + ledgerid + "', '" + fromdate + "','" + todate + "','" + Sessionid + "'";
            DataTable dt1 = new DataTable();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_LEDGER", ledgerid);
            hashTable.Add("p_FROMDATE", fromdate);
            hashTable.Add("p_TODATE", todate);
            hashTable.Add("p_Finyear", Sessionid);
         
            dt1 = dbconn.getDataSet("USP_RPT_TRAILBALANCE_SO", hashTable);
            return dt1;
        }
        public DataSet Bind_TargetSaleStock_json(string depotid, string productid, string BSID, string fromdate, string packsizeid, string storelocation, string mrpType, string rptType)
        {
           // DataSet dt = new DataSet();
            //string sql = "[dbo].[USP_RPT_TARGET_VS_SALE_JSON] '" + depotid + "','" + productid + "','" + BSID + "','" + fromdate + "','" + packsizeid + "','" + storelocation + "','" + mrpType + "','" + rptType + "'";
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_DEPOTID", depotid);
            hashTable.Add("P_PRODUCTID", productid);
            hashTable.Add("P_BSID", BSID);
            hashTable.Add("P_AS_ON_DATE", fromdate);
            hashTable.Add("P_PACKSIZEID", packsizeid);
            hashTable.Add("P_STOCKLOCATION", storelocation);
            hashTable.Add("P_MRPTYPE", mrpType);
            hashTable.Add("P_RPTTYPE", rptType);
          
            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_TARGET_VS_SALE_JSON]", hashTable); 
            return dt;

        }
        public DataSet BindForcastVsSalesStock_json(string depotid, string packsize, string brand, string category, string curdate, string storelocation, string MrpType, string RptType)
        {
            //string sql = "EXEC [USP_RPT_FORCAST_VS_SALE_N_STOCK_JSON] '" + depotid + "','" + packsize + "','" + brand + "','" + category + "','" + curdate + "','" + storelocation + "','" + MrpType + "','" + RptType + "'";
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_DEPOTID", depotid);
            hashTable.Add("P_PACKSIZEID", packsize);
            hashTable.Add("P_DIVID", brand);
            hashTable.Add("P_CATID", category);
            hashTable.Add("P_AS_ON_DATE", curdate);
            hashTable.Add("P_STOCKLOCATION", storelocation);
            hashTable.Add("P_MRPTYPE", MrpType);
            hashTable.Add("P_RPTTYPE", RptType);

            dt = dbconn.SysFetchDataInDataSet("[USP_RPT_FORCAST_VS_SALE_N_STOCK_JSON]", hashTable);
            return dt;
        }
        public DataSet BindForcastVsProductionVsSalereport(string fromdate, string todate, string FinYear)
        {
           // string sql = "EXEC [Usp_Rpt_Forcast_Vs_Production_Vs_Sale_Report] '" + fromdate + "','" + todate + "','" + FinYear + "'";
            DataSet dt = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_frmdate", fromdate);
            hashTable.Add("p_todate", todate);
            hashTable.Add("p_finyear", FinYear);
          

            dt = dbconn.SysFetchDataInDataSet("[Usp_Rpt_Forcast_Vs_Production_Vs_Sale_Report]", hashTable);
            return dt;
        }
        public DataTable BindSegment()
        {
            string sql = string.Empty;
           // DataTable dt = new DataTable();

            sql = " SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT " +
                  " WHERE BSID NOT IN ('33F6AC5E-1F37-4B0F-B959-D1C900BB43A5','97547CB6-F40B-4B43-923D-B63F61A910C2') " +
                  " ORDER BY BSNAME";
            DataSet dt = new DataSet();
            dt = dbconn.GetDataSet(sql);
            return dt.Tables[0];
        }

        public string [] fileparameters()
        {
            string[] parameter=new string[9];
            parameter[0] = "10.200.1.14\\dump_excel\\FileUpload\\";
            parameter[1] = "http://mcnroeerp.com:2122/FileUpload/";
           // parameter[1] = "http://mcnroeerp.com:8082//FileUpload/";
            parameter[2] = "10.200.1.14\\dump_excel\\TargetUpload\\";
            parameter[3] = "ftp://10.200.1.14:2121/";
            parameter[4] = "excel";

            parameter[5] = "mcn@1234";
            parameter[6] = "FileUpload/";
            parameter[7] = "TargetUpload/";
            parameter[8] = "Schemecircular/";
            return parameter;
        }
        /*new add by p.basu on 29-10-2020 for so wise summary report*/
        public DataSet bindSoSummary(string userid,string month,string finyear)
        {
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_USERID", userid);
            hashTable.Add("P_MONTH", month);
            hashTable.Add("P_FINYEAR", finyear);
            ds = dbconn.SysFetchDataInDataSetcube("[USP_RPT_SOWISE_SUMMARY]", hashTable);
            return ds;
        }
        public DataSet bindASMSummary(string userid, string month, string finyear)
        {
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_USERID", userid);
            hashTable.Add("P_MONTH", month);
            hashTable.Add("P_FINYEAR", finyear);
            ds = dbconn.SysFetchDataInDataSetcube("[USP_RPT_ASMWISE_SUMMARY_ALL]", hashTable);
            return ds;
        }
        public DataSet bindRSMSummary(string userid, string month, string finyear)
        {
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_USERID", userid);
            hashTable.Add("P_MONTH", month);
            hashTable.Add("P_FINYEAR", finyear);
            ds = dbconn.SysFetchDataInDataSetcube("[USP_RPT_RSMWISE_SUMMARY_ALL]", hashTable);
            return ds;
        }
        public DataSet bindPartySummary(string userid,string month,string finyear)
        {
            DataSet ds = new DataSet();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("P_USERID", userid);
            hashTable.Add("P_MONTH", month);
            hashTable.Add("P_FINYEAR", finyear);
            ds = dbconn.SysFetchDataInDataSetcube("[USP_RPT_PARTYWISE_SUMMARY_ALL]", hashTable);
            return ds;
        }
    }
}



 
