using DAL;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using Utility;

namespace BAL
{  
    public class ClsTargetForecast
    {
        DBUtils db = new DBUtils();
        private SqlCommand cmd;
        public SqlConnection cvCon = new SqlConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
        public DataSet ds = new DataSet();
        public int i;
        private SqlDataAdapter da;

        public DataTable BindSaleTarget(string UserID, string Fromdate, string Todate,string Finyear)
        {
            string sql = " SELECT SALETARGETID,  SALETARGETNUM,  JCNAME + ' - ' + CAST(DATEPART(YYYY,FROMDATE) AS VARCHAR(10)) AS JCNAME, "+
                         " DEPOTID, DEPOTNAME, BSID, BSNAME, BRANDID, BRANDNAME, CATID, CATNAME "+
                         " FROM T_SALE_TARGET_HEADER"+
                         " WHERE CREATEDBY='" + UserID + "'" +
                         " /*AND  CONVERT(DATE,FROMDATE,103) BETWEEN DBO.Convert_To_ISO('" + Fromdate + "') AND DBO.Convert_To_ISO('" + Todate + "')*/" +
                         " AND FINYEAR ='" + Finyear + "'" +
                         " ORDER BY SALETARGETNO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindMarketShare(string UserID, string Fromdate, string Todate, string Finyear)
        {
            string sql = " SELECT MARKET_SHARE_ID,  MARKET_SHARE_NUM,  JCNAME + ' - ' + CAST(DATEPART(YYYY,FROMDATE) AS VARCHAR(10)) AS JCNAME, " +
                         " STATEID, STATENAME, BSID, BSNAME, CATID, CATNAME,ISNULL(MARKET_SHARE_PERCENTAGE,0) AS PERCENTAGE " +
                         " FROM T_MARKET_SHARE_HEADER" +
                         " WHERE CREATEDBY='" + UserID + "'" +
                         " /*AND  CONVERT(DATE,FROMDATE,103) BETWEEN DBO.Convert_To_ISO('" + Fromdate + "') AND DBO.Convert_To_ISO('" + Todate + "')*/" +
                         " AND FINYEAR ='" + Finyear + "'" +
                         " ORDER BY MARKET_SHARE_NO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCategory(string brandid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE  DIVID='" + brandid + "' ORDER BY CATNAME";
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
        public DataTable BindProduct(string BSID)
        {
            string sql = " SELECT DISTINCT ID, "+
                         " CASE WHEN PRODUCTALIAS IS NULL THEN NAME WHEN PRODUCTALIAS ='' THEN NAME ELSE PRODUCTALIAS END AS NAME "+
                         " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                         " ON A.ID = B.PRODUCTID " +
                         " WHERE TYPE='FG' "+
                         " AND B.BUSNESSSEGMENTID='" + BSID + "'" +
                         " ORDER BY NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
       
        public DataTable BindProductbyBSegment(string divid, string catid, string BSID)
        {
            string STRSQL = string.Empty;
            string Sql = string.Empty;
            DataTable dt = new DataTable();
            
            STRSQL = " SELECT DISTINCT ID,NAME FROM M_PRODUCT INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP ON M_PRODUCT.ID = M_PRODUCT_BUSINESSSEGMENT_MAP.PRODUCTID";
            string STRORDERBY = "ORDER BY NAME";

            string BS_SEGMENT = "";
            if (BSID != "0")
            {

                BS_SEGMENT = " AND BUSNESSSEGMENTID='" + BSID + "'";

            }

            string DIVID = "";
            if (divid != "0")
            {

                DIVID = " AND DIVID IN('" + divid + "')";

            }

            string CATID = "";
            if (catid != "0")
            {

                CATID = " AND M_PRODUCT.CATID IN('" + catid + "')";

            }

            Sql = STRSQL + BS_SEGMENT + DIVID + CATID;
            dt = db.GetData(Sql + STRORDERBY);
            return dt;

        }

        public DataTable BindSizeInPack()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID IN('1970C78A-D062-4FE9-85C2-3E12490463AF','B9F29D12-DE94-40F1-A668-C79BF1BF4425','71B973E8-28E3-4F3A-A86E-9475AF2D14EE')";
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

        public DataTable Region()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O' UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public DataSet BindProductwisePartyOnLoad(string fromdate, string todate, string depot, string bsid, string saletype, string productid,string saleTargetID)
        {
            string sql = "EXEC [USP_SALE_TARGET] '" + fromdate + "','" + todate + "','" + depot + "','" + bsid + "','" + productid + "','" + saleTargetID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet BindSaleDetails(string fromdate, string todate, string depot, string bsid, string Brand,string CategoryID)
        {
            string sql = "EXEC [USP_SALE_DETAILS] '" + fromdate + "','" + todate + "','" + depot + "','" + bsid + "','"+ Brand+"','" + CategoryID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindDATEBYMONTH(string YEAR, string MONTH)
        {
            string sql = " select convert(varchar(10),cast(STARTDATE as date),103) as STARTDATE,convert(varchar(10),cast(ENDDATE as date),103) as ENDDATE " +
                         " from M_TIMEBREAKUP WHERE FINYEAR = '"+ YEAR + "' AND MONTHID = CAST('"+ MONTH + "' AS INT) ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
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
        public DataTable BindTimeSpan(string Tag)
        {
            string sql = "SELECT SEARCHTAG,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='" + Tag + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFREEZE()
        {
            string sql = "SELECT BRID,BRCODE,BRNAME,FULLNAME,BRDESCRIPTION,BRPREFIX,STATUS,ISAPPROVED,CITYID,STATEID,BRANCHTAG FROM M_BRANCH";
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

        public DataTable BindDepot_Primary(string StateID)
        {
            string sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' " +
                         " AND STATEID='"+StateID+"'"+
                         " AND BRID NOT IN('A2BEE84F-3F93-4B09-9BC6-023B3DE81791','A6636825-A613-497E-9E63-4DE6B985BA7F','B93E3BF9-C148-4C55-B8AB-7DF86141B3A4','93435BD2-484C-4971-9FC1-BA18D73E2B5D','C692AB5E-DD2C-44B7-BA6E-DB606C396800')" +
                         " ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindState()
        {
            string sql = " SELECT STATE_ID,STATE_NAME FROM M_REGION ORDER BY STATE_NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDeptName(string BRID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'AND BRID='" + BRID + "' order by BRNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_userwise(string userid)
        {
            string sql = "EXEC [USP_RPT_BINDDEPOT] '" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        
        public string InsertTargetDetails(  string hdnvalue,string JCID,string JCNAME,string FROMDATE,                  
                                            string TODATE,string DEPOTID,string DEPOTNAME,string BSID,string BSNAME,string BRANDID , 
                                            string BRANDNAME,string CATID ,string CATNAME,string SKUID,string SKUNAME,string SALETYPE,
	                                        string FINYEAR,string CREATEDBY,string MODIFIEDBY,string TargetDetails 
                                         )
        {

            string flag = "";
            string TargetID = string.Empty;
            string TargetNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            try
            {
                string sqlprocInvoice = " EXEC [USP_SALE_TARGET_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + JCID + "'," +
                                         " '" + JCNAME + "','" + FROMDATE + "','" + TODATE + "','" + DEPOTID + "','" + DEPOTNAME + "'," +
                                         " '" + BSID + "','" + BSNAME + "','" + BRANDID + "','" + BRANDNAME + "','" + CATID + "'," +
                                         " '" + CATNAME + "','" + SKUID + "','" + SKUNAME + "','" + SALETYPE + "','" + FINYEAR + "'," +
                                         " '" + CREATEDBY + "','" + MODIFIEDBY + "','" + TargetDetails + "'";
                DataTable dtDespatch = db.GetData(sqlprocInvoice);

                if (dtDespatch.Rows.Count > 0)
                {
                    TargetID = dtDespatch.Rows[0]["TARGETID"].ToString();
                    TargetNo = dtDespatch.Rows[0]["TARGETNO"].ToString();
                }
                else
                {
                    TargetNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }

            return TargetNo;
        }

        
        public string InsertShareDetails(   string hdnvalue, string JCID, string JCNAME,
                                            decimal Percentage, string FROMDATE, string TODATE,
                                            string STATEID, string STATENAME,string BSID, string BSNAME,
                                            string CATID,string CATNAME,
                                            string FINYEAR, string CREATEDBY, string DEPOTID,
                                            string BRAND,string XML
                                         )
        {

            string flag = "";
            string TargetID = string.Empty;
            string TargetNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            try
            {
                string sqlprocInvoice = " EXEC [USP_MARKET_SHARE_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + JCID + "'," +
                                         " '" + JCNAME + "'," + Percentage + ",'" + FROMDATE + "','" + TODATE + "'," +
                                         " '" + STATEID + "','"+STATENAME+"','" + BSID + "','"+BSNAME+"','" + CATID + "','"+CATNAME+"','" + FINYEAR + "'," +
                                         " '" + CREATEDBY + "','" + DEPOTID + "','" + BRAND + "','" + XML + "'";
                DataTable dtDespatch = db.GetData(sqlprocInvoice);

                if (dtDespatch.Rows.Count > 0)
                {
                    TargetID = dtDespatch.Rows[0]["SHAREID"].ToString();
                    TargetNo = dtDespatch.Rows[0]["SHARENO"].ToString();
                }
                else
                {
                    TargetNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }

            return TargetNo;
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
        public DataTable BindJC()
        {
            string sql = " SELECT JCID,NAME FROM M_JC ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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
        public DataTable BindBusinessegment_PrimarySales()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID <> (SELECT EXPORTBSID FROM P_APPMASTER) ORDER BY NAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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
        public DataTable BindProductALIAS()
        {
            string sql = "SELECT DISTINCT ID, PRODUCTALIAS AS NAME FROM M_PRODUCT ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotByUserid(string userid)
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID IN (SELECT TPUID FROM M_TPU_USER_MAPPING WHERE USERID='" + userid + "')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCat()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT CATID,CATNAME FROM M_CATEGORY ORDER BY SEQUENCENO";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivision()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductforPartyWiseProductSales(string DIVID, string CATID,string BSID)
        {
            string sql = string.Empty;
            if (DIVID != "")
            {
                sql = " SELECT DISTINCT ID," +
                      " CASE WHEN PRODUCTALIAS IS NULL THEN NAME WHEN PRODUCTALIAS ='' THEN NAME ELSE PRODUCTALIAS END AS NAME" +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID" +
                      " WHERE A.DIVID IN (SELECT * FROM DBO.fnSplit('" + DIVID + "',',')) " +
                      " AND A.CATID='" + CATID + "'" +
                      " AND B.BUSNESSSEGMENTID='" + BSID + "'" +
                      " ORDER BY NAME";
            }
            else
            {
                sql = " SELECT DISTINCT ID," +
                      " CASE WHEN PRODUCTALIAS IS NULL THEN NAME WHEN PRODUCTALIAS ='' THEN NAME ELSE PRODUCTALIAS END AS NAME" +
                      " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                      " ON A.ID = B.PRODUCTID" +
                      " WHERE A.CATID='" + CATID + "'" +
                      " AND B.BUSNESSSEGMENTID='" + BSID + "'" +
                      " ORDER BY NAME";
            }
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

        public DataTable BindEvent(string FromDate,string ToDate,string StateID)
        {
            DataTable dt = new DataTable();
            string sql = "  SELECT EVENTID,EVENTNAME "+
                         "  FROM M_EVENT "+
                         "  WHERE (	CONVERT(DATE,FROMDATE,103) < = DBO.Convert_To_ISO('"+ FromDate +"') AND "+
                         "          CONVERT(DATE,TODATE ,103)  > = DBO.Convert_To_ISO('" + FromDate + "')) " +
                         "  OR "+
                         "  (	CONVERT(DATE,FROMDATE,103) < = DBO.Convert_To_ISO('" + ToDate + "') AND " +
                         "      CONVERT(DATE,TODATE, 103)  > = DBO.Convert_To_ISO('" + ToDate + "')) " +
                         "  AND STATEID='" + StateID + "'" +
                         "  ORDER BY EVENTNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductforPartyWiseProductSales(string DIVID, string CATID,string SIZE,string BSID)
        {
            string sql = " SELECT DISTINCT ID,"+
                         " CASE WHEN PRODUCTALIAS IS NULL THEN NAME WHEN PRODUCTALIAS ='' THEN NAME ELSE PRODUCTALIAS END AS NAME"+
                         " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                         " ON A.ID = B.PRODUCTID "+
                         " WHERE A.DIVID='" + DIVID + "' "+
                         " AND A.CATID='" + CATID + "' "+
                         " AND A.UNITVALUE='" + SIZE + "' "+
                         " AND B.BUSNESSSEGMENTID='" + BSID + "'" +
                         " AND A.TYPE='FG'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductforPartyWiseProductSales(string SIZE, string BSID)
        {
            string sql = " SELECT DISTINCT ID," +
                         " CASE WHEN PRODUCTALIAS IS NULL THEN NAME WHEN PRODUCTALIAS ='' THEN NAME ELSE PRODUCTALIAS END AS NAME" +
                         " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                         " ON A.ID = B.PRODUCTID " +
                         " WHERE A.UNITVALUE='" + SIZE + "' " +
                         " AND B.BUSNESSSEGMENTID='" + BSID + "'" +
                         " AND A.TYPE='FG'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet EditTargetDetails(string TargetID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_SALE_TARGET_EDIT '" + TargetID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }


        public int TargetDelete(string TargetID)
        {
            string sql = string.Empty;
            int d = 0;
            try
            {
                sql = "EXEC USP_TARGET_DELETE '" + TargetID + "'";
                d = db.HandleData(sql);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return d;
        }

        public int MarketShareDelete(string MarketShareID)
        {
            string sql = string.Empty;
            int d = 0;
            try
            {
                sql = "EXEC USP_MARKET_SHARE_DELETE '" + MarketShareID + "'";
                d = db.HandleData(sql);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return d;
        }

        public DataTable BindSODepot(string UserID)
        {
            string sql = " SELECT A.BRID,A.BRPREFIX FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B " +
                         " ON A.BRID = B.TPUID " +
                         " WHERE B.USERID='" + UserID + "' " +
                         " ORDER BY A.BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllDepot()
        {
            string sql = " SELECT '-1' AS BRID ,'All' AS BRPREFIX,0 AS SEQ "+
                         " UNION ALL "+
                         " SELECT BRID, BRPREFIX,1 AS SEQ "+
                         " FROM M_BRANCH "+
                         " WHERE BRANCHTAG = 'D' "+
                         " AND ISFACTORY = 'N' "+
                         " ORDER BY SEQ,BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindReportCount(string DepotID,string MonthID,string Year,string Status)
        {
            string sql = " EXEC USP_TARGET_COUNT '"+DepotID+"','"+MonthID+"','"+Year+"','"+Status+"'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindCustomer(string UserID)
        {
            string sql =    " SELECT DISTINCT B.CUSTOMERID,(B.CUSTOMERNAME + ' ['+D.BRPREFIX+']') AS CUSTOMERNAME " +
                            " FROM M_CUSTOMER_TSI_MAPPING A WITH (NOLOCK) "+
                            " INNER JOIN M_CUSTOMER AS B WITH (NOLOCK) ON A.CUSTOMERID = B.CUSTOMERID" +
                            " INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS C WITH (NOLOCK) ON C.CUSTOMERID = B.CUSTOMERID " +
                            " INNER JOIN M_BRANCH AS D WITH (NOLOCK) ON D.BRID = C.DEPOTID" +
                            " WHERE B.ISACTIVE='True' " +
                            " /*AND TSI_ID IN(SELECT USERID FROM M_USER US  " +
                            "				WHERE REPORTINGTOID='" + UserID + "' " +
                            "				AND ISACTIVE='1')*/ " +
                            " AND A.TSI_ID = '" + UserID + "'"+
                            " ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllCustomer(string DepotID)
        {
            string sql = " SELECT DISTINCT A.CUSTOMERID,(A.CUSTOMERNAME + ' ['+D.BRPREFIX+']') AS CUSTOMERNAME " +
                         " FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B WITH (NOLOCK) " +
                         " ON A.CUSTOMERID = B.CUSTOMERID " +
                         " INNER JOIN M_BRANCH AS D WITH (NOLOCK) ON D.BRID = B.DEPOTID" +
                         " WHERE B.DEPOTID = '" + DepotID + "' " +
                         " AND A.BUSINESSSEGMENTID='7F62F951-9D1F-4B8D-803B-74EEBA468CEE' " +
                         " AND A.ISACTIVE='True' " +
                         " ORDER BY CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProdut()
        {
            ClsParam Param = new ClsParam();
            string sql =    " SELECT DISTINCT A.ID  AS PRODUCTID , " +
                            " A.PRODUCTALIAS + '~ [' + D.PSNAME + ']' AS PRODUCTNAME, " +
                            " C.SEQUENCENO,A.DIVNAME,CAST(A.UNITVALUE AS DECIMAL(18,2)) AS UNITVALUE " +
                            " FROM M_PRODUCT AS A INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP AS B " +
                            " ON A.ID = B.PRODUCTID INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN VW_SALEUNIT AS D " +
                            " ON A.ID = D.PRODUCTID " +
                            " AND D.PSID = '"+Param.CASE+"' " +
                            " AND B.BUSNESSSEGMENTID = '"+Param.GT+"' " +
                            " AND A.TYPE = 'FG' " +
                            " ORDER BY SEQUENCENO,DIVNAME,CAST(UNITVALUE AS DECIMAL(18,2)),PRODUCTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMonth()
        {
            string sql = " SELECT MONTHID,TIMESPAN FROM M_TIMEBREAKUP WHERE SEARCHTAG='M'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GenerateBulkTartgetTemplate(string DepotID)
        {
            string sql = " EXEC USP_GENERATE_BULK_TARGET_TEMPLATE '" + DepotID+"'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable GenerateTartgetTemplate()
        {
            string sql = " EXEC USP_GENERATE_TARGET_TEMPLATE ";
            DataTable dt = db.GetData(sql);
            return dt;
        }


        public string UploadFile(   string UserID, string Month, string Year, string FileName,
                                    string CustomerID,string CustomerName,string DepotID,string DepotName
                                )
        {
            string ACKNo = string.Empty;
            string sql = " USP_TARGET_UPLOAD_STATUS '" + UserID + "','" + Month + "','" + Year + "','" + FileName + "',"+
                         " '" + CustomerID + "','" + CustomerName + "','"+DepotID+"','"+DepotName+"'";
            DataTable dtACK = db.GetData(sql);
            if (dtACK.Rows.Count > 0)
            {
                ACKNo = dtACK.Rows[0]["ACKNO"].ToString();
            }
            else
            {
                ACKNo = "";
            }
            return ACKNo;
        }

        public string BulkUploadFile(   string UserID, string Month, string Year, string FileName,
                                        string DepotID, string DepotName
                                    )
        {
            string ACKNo = string.Empty;
            string sql = " USP_BULK_TARGET_UPLOAD_STATUS '" + UserID + "','" + Month + "','" + Year + "','" + FileName + "'," +
                         " '" + DepotID + "','" + DepotName + "'";
            DataTable dtACK = db.GetData(sql);
            if (dtACK.Rows.Count > 0)
            {
                ACKNo = dtACK.Rows[0]["ACKNO"].ToString();
            }
            else
            {
                ACKNo = "";
            }
            return ACKNo;
        }

        public DataTable GetSOUploadStatus(string MonthID,string Year,string UserID)
        {
            string sql =    " SELECT	A.AUTOID,A.UPLOADID,ISNULL(ACKNO,'') AS ACKNO,A.USERID,CONVERT(VARCHAR(10),CAST(A.DTOC AS DATE),103) AS UPLOADDATE, " +
                            " A.[MONTH] AS MONTHID,T.TIMESPAN AS MONTH_NAME,A.[YEAR],A.[FILENAME],STATUS, " +
                            " CASE WHEN STATUS= 'P' THEN 'PENDING' WHEN STATUS= 'D' THEN 'DONE' ELSE 'ERROR' END AS UPLOADSTATUS, " +
                            " ISNULL(REMARKS,'') AS REMARKS,ISNULL(A.CUSTOMERID,'0') AS CUSTOMERID,ISNULL(A.CUSTOMERNAME,'0') AS CUSTOMERNAME, " +
                            " ISNULL(A.DEPOTID,'0') AS DEPOTID,ISNULL(A.DEPOTNAME,'0') AS DEPOTNAME, " +
                            " ISNULL(FT.TOTALCASE,0) AS TOTALCASE,ISNULL(FT.TOTALVALUE,0) AS TOTALVALUE " +
                            " FROM T_TARGET_UPLOAD_STATUS A INNER JOIN M_TIMEBREAKUP T " +
                            " ON A.MONTH = T.MONTHID AND T.FINYEAR=A.YEAR   " +
                            " LEFT JOIN T_TARGET_UPLOAD_FOOTER AS FT WITH (NOLOCK) " +
                            " ON A.UPLOADID = FT.UPLOADID " +
                            " WHERE A.[MONTH]='" + MonthID + "' " +
                            " AND A.[YEAR] = '" + Year + "' " +
                            " AND A.USERID='" + UserID + "' ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetUploadStatus(string MonthID, string Year, string FileStatus,string DepotID)
        {
            string sql = " EXEC USP_MIS_TARGET_UPLOAD_STATUS '"+ MonthID + "','"+ Year + "','" + FileStatus + "','" + DepotID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetUploadStatusSO(string MonthID, string Year,string FileStatus,string DepotID)
        {
            string sql = " EXEC USP_MIS_TARGET_UPLOAD_STATUS '" + MonthID + "','" + Year + "','" + FileStatus + "','" + DepotID + "', '"+Convert.ToString(HttpContext.Current.Session["UserID"]).Trim()+ "', '" + Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetTSIUploadStatus(string MonthID, string Year, string DepotID)
        {
            string sql = " EXEC USP_TSI_TARGET_UPLOAD_STATUS '" + MonthID + "','" + Year + "','" + DepotID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetUserWiseTSIUploadStatus(string MonthID, string Year)
        {
            string sql = " EXEC USP_TSI_TARGET_UPLOAD_STATUS '" + MonthID + "','" + Year + "','','" + Convert.ToString(HttpContext.Current.Session["UserID"]).Trim() + "','" + Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindTarget_Details(string UploadID)
        {
            string sql = " EXEC USP_TARGET_DETAILS '" + UploadID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

       

        public DataTable BindUserType(string UserType)
        {

            string sql = " EXEC USP_BIND_USERTYPE '" + UserType + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindReport(string MonthID,string Year,string UserID,string UserType)
        {

            string sql = " EXEC USP_TARGET_HIERARCHY  '" + MonthID + "','" + Year + "','" + UserID + "','" + UserType + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindSODetailsReport( string MonthID, string Year, string UserID, string UserType,
                                            string ReportFlag,string DistFlag,string DistributorID,
                                            string ProductFlag,string ProductID, string ASMDistFlag, string ASMDistID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (ReportFlag != "3")
            {
                sql =   " EXEC USP_TARGET_REPORT_DETAILS  '" + MonthID + "','" + Year + "','" + UserID + "','" + UserType + "'," +
                        " '" + ReportFlag + "','" + DistFlag + "','" + DistributorID + "','" + ProductFlag + "','" + ProductID + "'";
            }
            else if (ReportFlag=="3")
            {
                sql =   " EXEC USP_ASM_DISTRIBUTOR_TARGET_REPORT_DETAILS  '" + MonthID + "','" + Year + "','" + UserID + "','" + UserType + "'," +
                        " '" + ASMDistFlag + "','" + ASMDistID + "'";
            }
            
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataSet PerformanceReport(   string MonthID, string Year, string UserID, string REPORTTYPE,string UserType)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;

            sql = " EXEC USP_PERFORMANCE_REPORT  '" + MonthID + "','" + Year + "','" + UserID + "','" + REPORTTYPE + "','"+ UserType +"'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataSet WeeklyPerformanceReport(string StartDate,string MonthID, string Year, string UserID,string reporttype, string UserType)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;

            sql = " EXEC USP_PERFORMANCE_REPORT_WEEKLY  '" + StartDate + "','" + MonthID + "','" + Year + "','" + UserID + "','"+ reporttype + "','" + UserType + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataSet Bind_UserInfo(string USERID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[USP_TARGET_USERINFO_ERP] '" + USERID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable CheckValidation(string Month,string Year, string CustomerID,string DepotID)
        {

            string sql = " EXEC USP_CHK_MONTH '"+ Month + "','"+ Year + "','"+ CustomerID + "','"+ DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable CheckBulkValidation(string Month, string Year, string DepotID)
        {

            string sql = " EXEC USP_BULK_CHK_MONTH '" + Month + "','" + Year + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet TargetDumpExcel(string StartDate, string EndDate, string Year, string Param)
        {
            string sql = string.Empty;
            if (Param == "ERP")
            {
                sql = " EXEC [USP_RPT_TARGET_REPORT_DUMP] '" + StartDate + "','" + EndDate + "','" + Year + "'";
            }
            else
            {
                sql = " EXEC [USP_RPT_TSPL_TARGET_REPORT_DUMP] '" + StartDate + "','" + EndDate + "','" + Year + "'";
            }
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet MonthWiseTargetDumpExcel(string Month,string Year, string Param,string ReportType,string BSID)
        {
            string sql = string.Empty;
            if (Param == "Details")
            {
                sql = " EXEC [USP_RPT_TARGET_REPORT_DUMP_V2] '" + Month + "','" + Year + "','"+ ReportType + "','"+ BSID + "'";
            }
            else
            {
                sql = " EXEC [USP_RPT_TARGET_REPORT_SUMMARY_DUMP_V2] '" + Month + "','" + Year + "','" + BSID + "'";
            }
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet PrimaryDumpExcel(string StartDate, string EndDate, string Year,string Param,string Usertype,string DepotID)
        {
            string sql = string.Empty;
            /*if (Param == "ERP")
            {
                sql = " EXEC [USP_RPT_PRIMARY_DUMP] '" + StartDate + "','" + EndDate + "'";
            }
            else
            {
                sql = " EXEC [USP_RPT_PRIMARY_DUMP_TSPL] '" + StartDate + "','" + EndDate + "'";
            }*/
            if (Usertype!= "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " EXEC [USP_RPT_PARTYWISE_INVOICEWISE_SALES_V2] '" + StartDate + "','" + EndDate + "'";
            }
            else
            {
                sql = " EXEC [USP_RPT_PARTYWISE_INVOICEWISE_SALES_DEPOT_V2] '" + StartDate + "','" + EndDate + "','"+ DepotID + "'";
            }
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataSet PrimaryDumpExcel(string StartDate, string EndDate, string Year, string Param, string UserID)
        {
            string sql = string.Empty;
            /*if (Param == "ERP")
            {
                sql = " EXEC [USP_RPT_PRIMARY_DUMP] '" + StartDate + "','" + EndDate + "'";
            }
            else
            {
                sql = " EXEC [USP_RPT_PRIMARY_DUMP_TSPL] '" + StartDate + "','" + EndDate + "'";
            }*/
            sql = " EXEC [USP_RPT_PARTYWISE_INVOICEWISE_SALES_V2] '" + StartDate + "','" + EndDate + "','" + UserID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable BindTargetDeleteDetails(string customerid) /* Add On 05/01/2019 */
        {
            string sql = "SELECT UPLOADID,ACKNO,CUSTOMERNAME,DTOC,YEAR,STATUS = CASE WHEN STATUS = 'D' then 'Done'WHEN STATUS = 'P' then 'Pending'END ," +
                        " MONTH = CASE WHEN MONTH = '01' THEN 'JANUARY'WHEN MONTH = '02' THEN 'FEBRUARY' WHEN MONTH = '03' THEN 'MARCH' WHEN MONTH = '04' THEN 'APRIL' " +
                        " WHEN MONTH = '05' THEN 'MAY' WHEN MONTH = '06' THEN 'JUNE' WHEN MONTH = '07' THEN 'JULY' WHEN MONTH = '08' THEN 'AUGUST' WHEN MONTH = '09' THEN 'SEPTEMBER' WHEN " +
                        " MONTH = '10' THEN 'OCTOBER' WHEN MONTH = '11' THEN 'NOVEMBER' WHEN MONTH = '12' THEN 'DECEMBER' END FROM  T_TARGET_UPLOAD_STATUS WHERE CUSTOMERID = '"+customerid+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;  
        }
        public DataTable TargetDeleteErp(string uploadid)  /* Add On 05/01/2019 */
        {
            string sql = "EXEC[USP_TARGET_DELETE_ERP]'" + uploadid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindDistributorDetailsReport(string MonthID, string Year, string UserID, string CustomerID, string ProductFlag, string ProductID, string RequestFrom)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_DISTRIBUTOR_TARGET_REPORT_DETAILS  '" + MonthID + "','" + Year + "','" + UserID + "','" + CustomerID + "','" + ProductFlag + "','" + ProductID + "','" + RequestFrom + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet BindTSITarget(   string MonthID, string Year, string UserID, string UserType,
                                        string ReportFlag, string DistFlag, string DistributorID,
                                        string ProductFlag, string ProductID, string ASMDistFlag,
                                        string ASMDistID,string Date,decimal tsipercent,decimal distpercent
                                     )
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (ReportFlag != "3")
            {
                sql = " EXEC USP_TSI_TARGET_SETTING_V2  '" + MonthID + "','" + Year + "','" + UserID + "','" + UserType + "'," +
                      " '" + ReportFlag + "','" + DistFlag + "','" + DistributorID + "','" + ProductFlag + "',"+
                      " '" + ProductID + "','"+ Date + "',"+ tsipercent + ","+ distpercent + "" ;
            }

            ds = db.GetDataInDataSet(sql);

            return ds;
        }

       
        public string InsertTSITargetDetails(   string MonthID, string YearID, string DesignationID, string Designation,
                                                string ASMID, string USERID, string USERNAME, string ReportType, string CustomerID, string Customer,
                                                string FINYEAR, string XML
                                            )
        {

           
            string TargetID = string.Empty;
            string TargetNo = string.Empty;
            
            try
            {
                string sqlprocInvoice = " EXEC [USP_TSI_TARGET_INSERT] '" + MonthID + "','" + YearID + "','" + DesignationID + "','" + Designation + "'," +
                                         " '" + ASMID + "','" + USERID + "','" + USERNAME + "','" + ReportType + "','" + CustomerID + "','" + Customer + "'," +
                                         " '" + FINYEAR + "','" + XML + "'";
                                         
                DataTable dtDespatch = db.GetData(sqlprocInvoice);

                if (dtDespatch.Rows.Count > 0)
                {
                    TargetID = dtDespatch.Rows[0]["TARGETID"].ToString();
                    TargetNo = dtDespatch.Rows[0]["TARGETNO"].ToString();
                }
                else
                {
                    TargetNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }

            return TargetNo;
        }



        public DataSet InsertTSITarget(string spName, Hashtable htParam)
        {
            int to = cvCon.ConnectionTimeout;
            IDictionaryEnumerator iEnum = htParam.GetEnumerator();
            cmd = new SqlCommand(spName, cvCon);
            cmd.CommandTimeout = 999999999;
            if (cvCon.State == ConnectionState.Closed)
                cvCon.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            while (iEnum.MoveNext())
            {
                cmd.Parameters.AddWithValue(iEnum.Key.ToString(), iEnum.Value);
            }
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cvCon.Close();
            return ds;
        }

        public DataTable BindBusinessSegmtnt()
        {

            string sql = " EXEC USP_BIND_BUSINESSSEGMENT ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindNonGtTarget()
        {

            string sql = " EXEC [USP_TARGET_UPLOAD_NONGT_BIND] ";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable SaveNonGtTarget(string Monthid, string Finyear, string BusinessSegmtnt, string UserId,string Xml)
        {
            DataTable result = new DataTable();
            string sql = "EXEC [USP_NONGT_TARGET_INSERT] '" + Monthid + "','" + Finyear + "','" + BusinessSegmtnt + "','"+ UserId + "','" + Xml + "'";
            result = db.GetData(sql);
            return result;
        }
        public DataTable LoadGridviewTarget(string USERID,string MONTH,string BSID ,string TAG,string FINYEAR)
        {

            string sql = "EXEC [USP_GRIDVIEW_NONGT_TARGET]'" + USERID + "','"+ MONTH + "','"+ BSID + "','"+ TAG + "','"+ FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet EditTargetupload(string TARGETID)
        {
            string sql = "EXEC [USP_RPT_EDIT_NONGT_TARGET_EDIT] '" + TARGETID + "'";
            DataSet ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable ReportType_Bind(string USERTYPE)
        {

            string sql = "EXEC [USP_BIND_REPORTTYPE]'" + USERTYPE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
