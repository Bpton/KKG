using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsCustomerMaster
    {
        DBUtils db = new DBUtils();

        public string CheckLastLevel(string ParamTypeID)
        {
            string sql = "select ISNULL(LASTLEVEL_DISTRIBUION_CHANNEL,0) from P_APPMASTER where LASTLEVEL_DISTRIBUION_CHANNEL LIKE '%" + ParamTypeID + "%'";
            string TYPE = (string)db.GetSingleValue(sql);
            return TYPE;
        }

        public DataTable FetchMap(string CustomerID)
        {
            string sql = "SELECT ISNULL(LATITUDE,0) AS LATITUDE,ISNULL(LONGITUDE,0) AS LONGITUDE FROM M_CUSTOMER where CUSTOMERID='" + CustomerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchLevel(string UserId, string UserType)
        {
            string sql = "Select CAST(LEVEL AS VARCHAR(20)) AS LEVEL from M_SAELSPERSONHIERARCHY where USERTYPEID='" + UserType + "' AND BUSINESSSEGMENTID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserId + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchLevel(string UserId, string UserType, string BSID)
        {
            string sql = "Select CAST(LEVEL AS VARCHAR(20)) AS LEVEL from M_SAELSPERSONHIERARCHY where USERTYPEID='" + UserType + "' AND BUSINESSSEGMENTID IN(select *  from dbo.fnSplit(('" + BSID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string FetchBSID(string UserId)
        {
            string sql = "select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserId + "'";
            string BSID = (string)db.GetSingleValue(sql);
            return BSID;
        }

        public DataTable BindPurchaseType()
        {
            string sql = "SELECT ID,ITEM_NAME FROM M_SUPLIEDITEM ORDER BY ITEM_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindShop()
        {
            string sql = "SELECT SHOPID,SHOPNAME FROM M_CUSTOMER_SHOPTYPE WHERE ISACTIVE='Y' ORDER BY SHOPNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCompanyType()
        {
            string sql = "SELECT COMPANYTYPEID,COMPANYTYPENAME FROM M_COMPANYTYPE ORDER BY COMPANYTYPEID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
       
        public DataTable BindLedger()
        {
            string sql = "SELECT ID,NAME FROM ACC_ACCOUNTSINFO ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        // change SQL query on 04.08.2016//
        public DataTable BindCurrency(string GrpID)
        {
            //string sql = " SELECT TOP 1 CURRENCYID,CURRENCYNAME FROM M_DISTRIBUTER_CATEGORY" +
            //             " WHERE DIS_CATID='" + GrpID + "'";

            string sql = " SELECT TOP 1 CURRENCYID,CURRENCYNAME FROM M_DISTRIBUTER_CATEGORY" +
                        " WHERE DIS_CATID IN(select *  from dbo.fnSplit(('" + GrpID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //=======================//

        public DataTable BindCountry()
        {
            string sql = "SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindreatilercategory(string TAG)
        {
            string sql = string.Empty;
            sql = "SELECT ID,NAME  FROM M_RETAILER_CATEGORY WHERE TAG='" + TAG + "' ORDER BY NAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID, DIVNAME, DIVCODE FROM M_DIVISION  WHERE ACTIVE='True' ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            string sql = "SELECT CATID, CATNAME, CATCODE FROM M_CATEGORY  WHERE ACTIVE='True' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCustomerGrid(string UserId, string UserType)
        {
            string sql = string.Empty;
            string TPUTAG = HttpContext.Current.Session["TPU"].ToString();
            string DEPOTID = HttpContext.Current.Session["DEPOTID"].ToString();

            //if (UserType == "TSI")
            //{
            //    sql = " SELECT ROW_NUMBER() OVER(ORDER BY CUSTOMERNAME) AS SLNO,CUSTOMERID, CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME,SHORTNAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID, GROUPNAME, PANCARDNO, VATNO, CSTNO," +
            //          " TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, STATEID," +
            //          " CASE WHEN STATENAME='0' THEN 'NA' ELSE STATENAME END AS STATENAME, DISTRICTID, " +
            //          " CASE WHEN DISTRICTNAME='0' THEN 'NA' ELSE DISTRICTNAME END AS DISTRICTNAME, " +
            //          " CITYID, CASE WHEN CITYNAME='0' THEN 'NA' ELSE CITYNAME END AS CITYNAME, PIN,ALTERNATEADDRESS,ALTERNATEPIN, CBU, DTOC, LMBU, LDTOM, " +
            //          " STATUS, ISAPPROVED,  CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE," +
            //          " CODE,ISNULL(LONGITUDE,0) AS LONGITUDE,ISNULL(LONGITUDE,0) AS LONGITUDE,GSTNO FROM M_CUSTOMER " +
            //          " WHERE CUSTOMERID IN (SELECT REFERENCEID FROM M_USER WHERE REPORTINGTOID IN(SELECT USERID FROM M_USER WHERE REPORTINGTOID='" + UserId + "') UNION ALL " +
            //          " SELECT REFERENCEID FROM M_USER WHERE REPORTINGTOID='" + UserId + "') AND PARENT_CUST_ID<>'158'";
            //}
            //else
            //{
            //    if (HttpContext.Current.Session["TPU"].ToString() == "D")
            //    {

            //        sql = " SELECT ROW_NUMBER() OVER(ORDER BY A.CUSTOMERNAME) AS SLNO, A.CUSTOMERID,BRPREFIX AS DEPOTNAME, A.CUSTOMERNAME, A.CUSTYPE_ID,A.CUSTYPE_NAME,A.BUSINESSSEGMENTID, A.BUSINESSSEGMENTNAME, A.GROUPID, A.GROUPNAME, A.PANCARDNO, " +
            //             " A.VATNO, A.CSTNO,A.TINNO, A.CONTACTPERSON1, A.CONTACTPERSON2, A.EMAILID1, A.EMAILID2, A.MOBILE1, A.TELEPHONE1, A.MOBILE2, A.TELEPHONE2, A.ADDRESS," +
            //             " A.STATEID, CASE WHEN A.STATENAME='0' THEN 'NA' ELSE A.STATENAME END AS STATENAME, A.DISTRICTID, " +
            //             " CASE WHEN A.DISTRICTNAME='Select District' THEN '' ELSE A.DISTRICTNAME END AS DISTRICTNAME, A.CITYID," +
            //             " CASE WHEN A.CITYNAME='Select City' THEN '' ELSE A.CITYNAME END AS CITYNAME, A.PIN,A.ALTERNATEADDRESS,A.ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM," +
            //             " A.STATUS, A.ISAPPROVED," +
            //             " CASE WHEN A.ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE," +
            //             " ISNULL(A.LONGITUDE,0) AS LONGITUDE,ISNULL(A.LONGITUDE,0) AS LONGITUDE,GSTNO " +
            //             " FROM M_CUSTOMER AS A LEFT OUTER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
            //             " ON A.CUSTOMERID = B.CUSTOMERID LEFT OUTER JOIN M_BRANCH D ON B.DEPOTID=D.BRID " +
            //             " AND AND PARENT_CUST_ID<>'158' AND B.DEPOTID = '" + Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim() + "'"; 


            //    }
            //    else
            //    {
            //        sql = " SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY A.CUSTOMERNAME) AS SLNO, A.CUSTOMERID, A.CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME, BUSINESSSEGMENTID,GENCODE,"+
            //              " BUSINESSSEGMENTNAME,  GROUPID, GROUPNAME, PANCARDNO, VATNO, CSTNO,TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, MOBILE1, TELEPHONE1,"+
            //              " MOBILE2, TELEPHONE2, A.ADDRESS, A.STATEID,GRPNAME AS ACCGROUPNAME , CASE WHEN STATENAME='0' THEN 'NA' ELSE STATENAME END AS STATENAME, A.DISTRICTID,"+
            //              " CASE WHEN DISTRICTNAME='Select District' THEN '' ELSE DISTRICTNAME END AS DISTRICTNAME, A.CITYID, CASE WHEN CITYNAME='Select City' "+
            //              " THEN '' ELSE CITYNAME END AS CITYNAME, PIN,ALTERNATEADDRESS,ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM, A.STATUS, A.ISAPPROVED,"+
            //              " CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE, A.CODE,ISNULL(LONGITUDE,0) AS LONGITUDE,  ISNULL(LONGITUDE,0) AS LONGITUDE,"+
            //              " A.GSTNO,"+
            //              " DEPOTNAME=STUFF"+
            //              " ( "+
            //              " ( "+
            //              " SELECT DISTINCT ', '+ CAST(g2.BRPREFIX AS VARCHAR(MAX)) "+
            //              " FROM M_CUSTOMER_DEPOT_MAPPING g INNER JOIN M_BRANCH G2 ON G.DEPOTID=G2.BRID,M_CUSTOMER e "+
            //              " WHERE g.CUSTOMERID=e.CUSTOMERID and e.CUSTOMERID=A.CUSTOMERID  "+
            //              " FOR XMl PATH('') "+
            //              " ),1,1,'' "+
            //              " ) "+
            //              " FROM M_CUSTOMER A LEFT OUTER JOIN ACC_ACCOUNTSINFO ON CUSTOMERID=ID LEFT OUTER JOIN ACC_ACCOUNTGROUP  "+
            //              " ON ACC_ACCOUNTSINFO.ACTGRPCODE=ACC_ACCOUNTGROUP.CODE "+
            //              " WHERE PARENT_CUST_ID<>'158' AND CUSTYPE_ID<>'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' AND REPORTTOROLEID<>'826D656F-353F-430F-8966-4FE6BE3F67ED'" +
            //              " ORDER BY CUSTOMERNAME";
            //    }
            //}

            sql = "exec [USP_CUSTOMER_MASTER_PAGELOAD] '" + UserId + "','" + UserType + "','" + TPUTAG + "','" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BinddepotMasterGrid()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BinddepotMasterGrid(string USERID)
        {
            string sql = "EXEC [USP_USER_WISE_DEPO]'" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerGridbyBSGroup(string UserId, string BSID, string depotid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (UserId == "8")
            {
                if (BSID == "0")
                {
                    sql = " SELECT    CUSTOMERID, CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID," +
                          "           GROUPNAME, PANCARDNO, VATNO, CSTNO,TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, MOBILE1, TELEPHONE1," +
                          "           MOBILE2, TELEPHONE2, ADDRESS, STATEID, CASE WHEN STATENAME='0' THEN 'NA' ELSE STATENAME END AS STATENAME," +
                          "           DISTRICTID, CASE WHEN DISTRICTNAME='0' THEN 'NA' ELSE DISTRICTNAME END AS DISTRICTNAME, CITYID," +
                          "           CASE WHEN CITYNAME='0' THEN 'NA' ELSE CITYNAME END AS CITYNAME, PIN,ALTERNATEADDRESS,ALTERNATEPIN," +
                          "           CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED," +
                          "           CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,CODE," +
                          "           ISNULL(LONGITUDE,0) AS LONGITUDE,ISNULL(LONGITUDE,0) AS LONGITUDE" +
                          " FROM M_CUSTOMER WHERE CUSTYPE_ID <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96'" +
                          " ORDER BY CUSTOMERNAME";
                }
                else
                {
                    if (depotid == "0")
                    {
                        sql = " SELECT ROW_NUMBER() OVER(ORDER BY A.CUSTOMERNAME) AS SLNO, A.CUSTOMERID AS CUSTOMERID,A.CUSTOMERNAME AS CUSTOMERNAME, CUSTYPE_ID," +
                              " CUSTYPE_NAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID, GROUPNAME, PANCARDNO, VATNO, CSTNO,TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1," +
                              " EMAILID2, MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, STATEID, CASE WHEN STATENAME='0' THEN 'NA' ELSE STATENAME END AS STATENAME, DISTRICTID," +
                              " CASE WHEN DISTRICTNAME='Select District' THEN '' ELSE DISTRICTNAME END AS DISTRICTNAME, CITYID, CASE WHEN CITYNAME='Select City' THEN '' ELSE CITYNAME END AS CITYNAME," +
                              " PIN,ALTERNATEADDRESS,ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM, STATUS, ISAPPROVED,GSTNO," +
                              " CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE,ISNULL(LONGITUDE,0) AS LONGITUDE,GRPNAME AS ACCGROUPNAME ,ISNULL(LONGITUDE,0) AS LONGITUDE  FROM M_CUSTOMER A LEFT OUTER JOIN ACC_ACCOUNTSINFO ACC1 ON A.CUSTOMERID=ACC1.ID LEFT OUTER JOIN ACC_ACCOUNTGROUP ACC2 ON ACC1.ACTGRPCODE=ACC2.CODE INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID  WHERE CUSTYPE_ID <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' AND BUSINESSSEGMENTID IN('" + BSID + "')";
                    }
                    else
                    {
                        sql = " SELECT ROW_NUMBER() OVER(ORDER BY A.CUSTOMERNAME) AS SLNO, A.CUSTOMERID AS CUSTOMERID,A.CUSTOMERNAME AS CUSTOMERNAME, CUSTYPE_ID," +
                             " CUSTYPE_NAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID, GROUPNAME, PANCARDNO, VATNO, CSTNO,TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1," +
                             " EMAILID2, MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, STATEID, CASE WHEN STATENAME='0' THEN 'NA' ELSE STATENAME END AS STATENAME, DISTRICTID," +
                             " CASE WHEN DISTRICTNAME='Select District' THEN '' ELSE DISTRICTNAME END AS DISTRICTNAME, CITYID, CASE WHEN CITYNAME='Select City' THEN '' ELSE CITYNAME END AS CITYNAME," +
                             " PIN,ALTERNATEADDRESS,ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM, STATUS, ISAPPROVED,GSTNO," +
                             " CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE,ISNULL(LONGITUDE,0) AS LONGITUDE,GRPNAME AS ACCGROUPNAME ,ISNULL(LONGITUDE,0) AS LONGITUDE  FROM M_CUSTOMER A LEFT OUTER JOIN ACC_ACCOUNTSINFO ACC1 ON A.CUSTOMERID=ACC1.ID LEFT OUTER JOIN ACC_ACCOUNTGROUP ACC2 ON ACC1.ACTGRPCODE=ACC2.CODE INNER JOIN M_CUSTOMER_DEPOT_MAPPING B ON A.CUSTOMERID=B.CUSTOMERID  WHERE CUSTYPE_ID <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' AND BUSINESSSEGMENTID IN('" + BSID + "') AND DEPOTID= '" + depotid + "'";
                    }

                }
            }

            else
            {
                if (BSID == "0")
                {
                    sql = " SELECT A.CUSTOMERID, A.CUSTOMERNAME, A.CUSTYPE_ID,A.CUSTYPE_NAME,A.BUSINESSSEGMENTID, A.BUSINESSSEGMENTNAME, A.GROUPID, A.GROUPNAME, A.PANCARDNO, " +
                          " A.VATNO, A.CSTNO,A.TINNO, A.CONTACTPERSON1, A.CONTACTPERSON2, A.EMAILID1, A.EMAILID2, A.MOBILE1, A.TELEPHONE1, A.MOBILE2, A.TELEPHONE2, A.ADDRESS," +
                          " A.STATEID, CASE WHEN A.STATENAME='0' THEN 'NA' ELSE A.STATENAME END AS STATENAME, A.DISTRICTID, " +
                          " CASE WHEN A.DISTRICTNAME='Select District' THEN '' ELSE A.DISTRICTNAME END AS DISTRICTNAME, A.CITYID," +
                          " CASE WHEN A.CITYNAME='Select City' THEN '' ELSE A.CITYNAME END AS CITYNAME, A.PIN,A.ALTERNATEADDRESS,A.ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM," +
                          " A.STATUS, A.ISAPPROVED," +
                          " CASE WHEN A.ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE," +
                          " ISNULL(A.LONGITUDE,0) AS LONGITUDE,ISNULL(A.LONGITUDE,0) AS LONGITUDE " +
                          " FROM M_CUSTOMER AS A INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                          " ON A.CUSTOMERID = B.CUSTOMERID " +
                          " AND B.DEPOTID = '" + Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim() + "'" +
                          " ORDER BY CUSTOMERNAME";
                }
                else
                {
                    sql = " SELECT A.CUSTOMERID, A.CUSTOMERNAME, A.CUSTYPE_ID,A.CUSTYPE_NAME,A.BUSINESSSEGMENTID, A.BUSINESSSEGMENTNAME, A.GROUPID, A.GROUPNAME, A.PANCARDNO, " +
                          " A.VATNO, A.CSTNO,A.TINNO, A.CONTACTPERSON1, A.CONTACTPERSON2, A.EMAILID1, A.EMAILID2, A.MOBILE1, A.TELEPHONE1, A.MOBILE2, A.TELEPHONE2, A.ADDRESS," +
                          " A.STATEID, CASE WHEN A.STATENAME='0' THEN 'NA' ELSE A.STATENAME END AS STATENAME, A.DISTRICTID, " +
                          " CASE WHEN A.DISTRICTNAME='0' THEN 'NA' ELSE A.DISTRICTNAME END AS DISTRICTNAME, A.CITYID," +
                          " CASE WHEN A.CITYNAME='0' THEN 'NA' ELSE A.CITYNAME END AS CITYNAME, A.PIN,A.ALTERNATEADDRESS,A.ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM," +
                          " A.STATUS, A.ISAPPROVED," +
                          " CASE WHEN A.ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE," +
                          " ISNULL(A.LONGITUDE,0) AS LONGITUDE,ISNULL(A.LONGITUDE,0) AS LONGITUDE " +
                          " FROM M_CUSTOMER AS A INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                          " ON A.CUSTOMERID = B.CUSTOMERID " +
                          " AND A.BUSINESSSEGMENTID IN('" + BSID + "')" +
                          " AND B.DEPOTID = '" + Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim() + "'" +
                          " ORDER BY CUSTOMERNAME";
                }
            }

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerGrid(string UserId)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            if (UserId == "8")
            {
                sql = " SELECT CUSTOMERID, CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID, GROUPNAME, PANCARDNO, VATNO, CSTNO," +
                       " TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, STATEID, STATENAME, DISTRICTID," +
                       " DISTRICTNAME, CITYID, CITYNAME, PIN,ALTERNATEADDRESS,ALTERNATEPIN, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED," +
                       " CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,CODE,ISNULL(LONGITUDE,0) AS LONGITUDE,ISNULL(LONGITUDE,0) AS LONGITUDE" +
                       " FROM M_CUSTOMER " +
                       " WHERE CUSTYPE_ID <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96'" +
                       " ORDER BY CUSTOMERNAME";
            }
            else
            {
                sql = " SELECT A.CUSTOMERID, A.CUSTOMERNAME, A.CUSTYPE_ID,A.CUSTYPE_NAME,A.BUSINESSSEGMENTID, A.BUSINESSSEGMENTNAME, A.GROUPID, A.GROUPNAME, A.PANCARDNO, " +
                      " A.VATNO, A.CSTNO,A.TINNO, A.CONTACTPERSON1, A.CONTACTPERSON2, A.EMAILID1, A.EMAILID2, A.MOBILE1, A.TELEPHONE1, A.MOBILE2, A.TELEPHONE2, A.ADDRESS," +
                      " A.STATEID, CASE WHEN A.STATENAME='0' THEN 'NA' ELSE A.STATENAME END AS STATENAME, A.DISTRICTID, " +
                      " CASE WHEN A.DISTRICTNAME='0' THEN 'NA' ELSE A.DISTRICTNAME END AS DISTRICTNAME, A.CITYID," +
                      " CASE WHEN A.CITYNAME='0' THEN 'NA' ELSE A.CITYNAME END AS CITYNAME, A.PIN,A.ALTERNATEADDRESS,A.ALTERNATEPIN, A.CBU, A.DTOC, A.LMBU, A.LDTOM," +
                      " A.STATUS, A.ISAPPROVED," +
                      " CASE WHEN A.ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE,A.CODE," +
                      " ISNULL(A.LONGITUDE,0) AS LONGITUDE,ISNULL(A.LONGITUDE,0) AS LONGITUDE " +
                      " FROM M_CUSTOMER AS A INNER JOIN M_CUSTOMER_DEPOT_MAPPING AS B " +
                      " ON A.CUSTOMERID = B.CUSTOMERID " +
                      " AND B.DEPOTID = '" + Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim() + "'" +
                      " ORDER BY CUSTOMERNAME";
            }

            dt = db.GetData(sql);
            return dt;
        }

        /*Modified By Sayan Dey on 01/01/2018*/
        public DataTable BindCustomerEditById(string ID)
        {
            //string sql = " SELECT CUSTOMERID, CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME,ISNULL(SHORTNAME,'') AS SHORTNAME,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME," +
            //             " GROUPID, GROUPNAME, PANCARDNO, " +
            //             " VATNO, CSTNO,TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, " +
            //             " STATEID, STATENAME, DISTRICTID, DISTRICTNAME, CITYID, CITYNAME, PIN,ALTERNATEADDRESS,ALTERNATEPIN, CBU, DTOC, LMBU, LDTOM," +
            //             " STATUS, ISAPPROVED,  CASE WHEN ISACTIVE='True' Then 'Active' else 'InActive' END AS ISACTIVE ,CODE,PARENT_CUST_ID, " +
            //             " PARENT_CUST_NAME,REPORTTOROLEID,PERCENTAGE,ISNULL(SHOPID,0) AS SHOPID,ISNULL(SHOPNAME,'') AS SHOPNAME," +
            //             " ISNULL( RETAILER_CAT_ID,'') AS RETAILER_CAT_ID,ISNULL(RETAILER_CAT_NAME,'') AS RETAILER_CAT_NAME,ISNULL(AMOUNT,0) AS AMOUNT," +
            //             " CURRENCYID,CURRENCYNAME,ACCGROUPID,ACCGROUPNAME,FILEEXIST,FILENAME,FILEID,COUNTRYID,ISNULL(LEDGER_REFERENCEID,'0') AS LEDGER_REFERENCEID," +
            //             " M_CUSTOMER.COMPANYTYPEID AS COMPANYTYPEID ,B.COMPANYTYPENAME AS COMPANYTYPENAME,ADDSSMARGIN ," +
            //             " ISNULL(ADDSSMARGINPERCENTAGE ,0) AS ADDSSMARGINPERCENTAGE,GSTNO,ISNULL(APPLICABLEGST,'N') AS APPLICABLEGST," +
            //             " ISNULL(ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO,ISNULL(RETDIVID,'0') AS RETDIVID,ISNULL(RETDIVNAME,'Select Retailer Division') AS RETDIVNAME,  " +
            //             " CONVERT(VARCHAR(10),CAST(DOB AS DATE),103) AS DOB,CONVERT(VARCHAR(10),CAST(ANVDATE AS DATE),103) AS ANVDATE,ISNULL(PRINTNAME,'') AS PRINTNAME" +
            //             " FROM M_CUSTOMER  LEFT OUTER JOIN M_COMPANYTYPE B ON M_CUSTOMER.COMPANYTYPEID=B.COMPANYTYPEID" +
            //             " where CUSTOMERID='" + ID + "'";

            //dt = db.GetData(sql);
            //return dt;

            DataTable dt = new DataTable();
            string sql =    " SELECT M_CUSTOMER.CUSTOMERID, M_CUSTOMER.CUSTOMERNAME, CUSTYPE_ID,CUSTYPE_NAME,ISNULL(SHORTNAME, '') AS SHORTNAME, " +
                            " BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, GROUPID, GROUPNAME, PANCARDNO,VATNO, CSTNO, TINNO, CONTACTPERSON1, CONTACTPERSON2, EMAILID1, EMAILID2, " +
                            " MOBILE1, TELEPHONE1, MOBILE2, TELEPHONE2, ADDRESS, STATEID, STATENAME,DISTRICTID, DISTRICTNAME, CITYID, CITYNAME, PIN, ALTERNATEADDRESS, ALTERNATEPIN, " +
                            " CBU, DTOC, LMBU, LDTOM, STATUS,ISAPPROVED,CASE WHEN ISACTIVE = 'True' Then 'Active' else 'InActive' END AS ISACTIVE , CODE,PARENT_CUST_ID, " +
                            " PARENT_CUST_NAME,REPORTTOROLEID,PERCENTAGE,ISNULL(SHOPID, 0) AS SHOPID,  ISNULL(SHOPNAME, '') AS SHOPNAME, ISNULL( RETAILER_CAT_ID, '') AS RETAILER_CAT_ID, " +
                            " ISNULL(RETAILER_CAT_NAME, '') AS RETAILER_CAT_NAME, ISNULL(AMOUNT, 0) AS AMOUNT, CURRENCYID,CURRENCYNAME, ACCGROUPID, ACCGROUPNAME, FILEEXIST,  " +
                            " FILENAME, FILEID, COUNTRYID, ISNULL(LEDGER_REFERENCEID, '0') AS LEDGER_REFERENCEID, M_CUSTOMER.COMPANYTYPEID AS COMPANYTYPEID , " +
                            " B.COMPANYTYPENAME AS COMPANYTYPENAME,ADDSSMARGIN ,  ISNULL(ADDSSMARGINPERCENTAGE, 0) AS ADDSSMARGINPERCENTAGE, GSTNO, " +
                            " ISNULL(APPLICABLEGST, 'N') AS APPLICABLEGST, ISNULL(ISTRANSFERTOHO, 'N') AS ISTRANSFERTOHO, ISNULL(RETDIVID, '0') AS RETDIVID, " +
                            " ISNULL(RETDIVNAME, 'Select Retailer Division') AS RETDIVNAME, ISNULL(INACTIVEDATE,'') AS INACTIVEDATE, D.DEPOTNAME," +
                            " CONVERT(VARCHAR(10), CAST(DOB AS DATE), 103) AS DOB, CONVERT(VARCHAR(10), CAST(ANVDATE AS DATE), 103) AS ANVDATE, " +
                            " ISNULL(PRINTNAME, '') AS PRINTNAME,ISNULL(BILLING_TYPE,'0') AS BILLING_TYPE,ISNULL(TCSPERCENT, 0) AS TCSPERCENT " +
                            " FROM M_CUSTOMER INNER JOIN M_CUSTOMER_DEPOT_MAPPING D ON M_CUSTOMER.CUSTOMERID = D.CUSTOMERID " +
                            " LEFT OUTER JOIN M_COMPANYTYPE B ON M_CUSTOMER.COMPANYTYPEID = B.COMPANYTYPEID where M_CUSTOMER.CUSTOMERID = '" + ID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        
        
        public DataTable BindPurchaseEditById(string ID)
        {
            string sql = " SELECT A.CUSTOMERID, B.PURCHASETYPE AS PURCHASETYPE  " +
                         " FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_PURCHASETYPE B ON A.CUSTOMERID=B.CUSTOMERID" +
                         " where A.CUSTOMERID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerType1()
        {
            string sql = " SELECT UTID,UTNAME" +
                         " FROM M_USERTYPE" +
                         " WHERE DISTRIBUTIONCHANNEL='Y'" +
                         " ORDER BY DISTRIBUTIONLEVEL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType1(string BSID)
        {
            string sql = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE DISTRIBUTIONCHANNEL='Y' " +
                         "AND UTID IN(SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE BUSINESSSEGMENTID  " +
                         "IN(select *  from dbo.fnSplit(('" + BSID + "'),','))) ORDER BY DISTRIBUTIONLEVEL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType1(string level, string UserID)
        {
            string sql = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE DISTRIBUTIONCHANNEL='Y' " +
                         "AND UTID IN(SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE LEVEL>'" + level + "' AND BUSINESSSEGMENTID IN(select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserID + "')) " +
                          "ORDER BY DISTRIBUTIONLEVEL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType1(string level, string UserID, string BSID)
        {
            string sql = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE DISTRIBUTIONCHANNEL='Y' " +
                         "AND UTID IN(SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE LEVEL>'" + level + "' AND BUSINESSSEGMENTID='" + BSID + "') " +
                          "ORDER BY DISTRIBUTIONLEVEL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType()
        {
            string sql = "select BSID,BSNAME from M_BUSINESSSEGMENT order by BSNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType_V1(string userid)
        {
            string sql = "EXEC[USP_USER_WISE_BS]'"+ userid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCustomerType(string UserId)
        {
            string sql = "select BSID,BSNAME from M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserId + "'),',')) order by BSNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReporintTo(string utid, string BSID)
        {
            string sqltype = "SELECT USERID,FNAME + '' + MNAME + '' + LNAME AS USERNAME FROM M_USER WHERE USERTYPE='" + utid + "' ORDER BY USERNAME";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReporintTo(string utid, string IUerID, string BSID)
        {
            string sqltype = "SELECT USERID,USERNAME FROM M_USER WHERE REFERENCEID IN (SELECT CUSTOMERID FROM M_CUSTOMER WHERE CUSTOMERID IN(SELECT REFERENCEID FROM M_USER WHERE USERTYPE='" + utid + "' " +
                             " AND USERID IN(SELECT USERID FROM M_USER WHERE REPORTINGTOID='" + IUerID + "')) AND BUSINESSSEGMENTID like '%" + BSID + "%')";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReporintToRole(string utid)
        {
            string sqltype = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN(select *  from dbo.fnSplit((SELECT PARENTID FROM M_USERTYPE WHERE UTID='" + utid + "'),','))";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindGroupTypeAll()
        {
            string sql = "select DIS_CATID,DIS_CATNAME from M_DISTRIBUTER_CATEGORY order by DIS_CATNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBSGroup()
        {
            string sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroupType(string ID)
        {
            string sql = "select DIS_CATID,DIS_CATNAME from M_DISTRIBUTER_CATEGORY WHERE BUSINESSSEGMENTID  " +
                         "IN(select *  from dbo.fnSplit(('" + ID + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroupTypeMultiple(string MultipleID)
        {
            string sql = "select DIS_CATID,DIS_CATNAME from M_DISTRIBUTER_CATEGORY WHERE BUSINESSSEGMENTID in ('" + MultipleID + "') ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GETLEVEL(string BSID, string UTYPE)
        {
            string sql = "SELECT ISNULL(LEVEL,0) AS LEVEL FROM M_SAELSPERSONHIERARCHY WHERE  BUSINESSSEGMENTID='" + BSID + "' AND USERTYPEID='" + UTYPE + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetUnderUser()
        {
            string sql = "SELECT USERID,USERNAME FROM M_USER WHERE APPLICABLETO='C'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GETLEVELBYPARENTID(string BSID, string PARRENTCUSTID)
        {
            string sql = "SELECT LEVEL FROM M_SAELSPERSONHIERARCHY WHERE  BUSINESSSEGMENTID='" + BSID + "' AND USERTYPEID in (SELECT CUSTYPE_ID FROM M_CUSTOMER WHERE PARENT_CUST_ID='" + PARRENTCUSTID + "') ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetUnderCustomer(string BSID, int level, string Stateid)
        {
            //string sql = "SELECT CASE WHEN REFERENCEID IS NULL THEN CAST(USERID AS VARCHAR(50)) ELSE REFERENCEID END USERID,USERNAME FROM M_USER WHERE USERTYPE IN (SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE LEVEL='" + level + "' AND BUSINESSSEGMENTID='" + BSID + "') AND STATEID='" + Stateid + "'";
            //string sql = "SELECT USERID,USERNAME FROM M_USER WHERE USERTYPE IN (SELECT PARENTID FROM M_USERTYPE WHERE UTID IN(SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE LEVEL='" + level + "' AND BUSINESSSEGMENTID='" + BSID + "'))";
            string sql = "SELECT USERID,USERNAME FROM M_USER WHERE USERTYPE IN (SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY WHERE LEVEL='" + level + "' AND BUSINESSSEGMENTID='" + BSID + "')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindState()
        {
            string sql = "select State_ID, State_Name from M_REGION order by State_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrandType()
        {
            string sql = " SELECT  BRANDVALUE,BRANDTYPE FROM M_BRAND_TYPE "+
                         " WHERE ACTIVE='Y' "+
                         " ORDER BY BRANDVALUE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand(string CustomerID,string DepotID,string Mode)
        {
            string sql = "EXEC USP_BRAND_TYPE '"+ CustomerID + "','"+ DepotID + "','"+Mode+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistrict(int stateid)
        {
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "' order by District_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCity(int distid)
        {
            string sql = "select City_ID, City_Name from M_CITY where District_ID='" + distid + "' order by City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        /*Modified By Sayan Dey on 01/01/2018*/
        public int SaveCustomerMaster(string ID, string NewCustomerID, string Password, string Customername, string Custypeid, string Custypename, string Businesssegmentid, string Businesssegmentname, string Groupid, string Groupname,
                                            string Panno, string Vatno, string Cstno, string Tinno, string Contractperson1, string Contractperson2, string Emailid1, string Emailid2,
                                            string Mobile1, string Telephone1, string Mobile2, string Telephone2, string Address, string Stateid, string Statename,
                                             string Districtid, string Districtname, string Cityid, string Cityname, string Pin, string Address2, string Pin2, string Mode, string Isactive, string Code, string parentcustid, string parentcustname,
                                            string ReportToRole, string UserId, decimal Percentage, string shopid, string shopname, string RETAILER_CAT_ID,
                                            string RETAILER_CAT_NAME, decimal AMOUNT, string currencyid, string currencyname, string ledgergroupid,
                                            string ledgergroupname, string fileid, string filename, string tag, string CountryID, string CountryName, string SHORTNAME,
                                            string LEDGER_REFERENCEID, string CompanyTypeID, string margin, decimal marginpercentage, string gstno, string APPLICABLEGST,
                                            string Purchasetypename, string istransfertoho, string RETDIVID, string RETDIVNAME, string DOB, string ANVDATE, string printname, 
                                            string IP_ADDRESS, string Depotid, string DepotName, string SOId, string SOName, string Inactivedate,string BrandType,
                                            decimal Tcspercent)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            string currentdate = dtcurr.ToString(date).Replace('-', '/');
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (SHORTNAME != "")
                    {
                        strcheck = "select top 1 * from M_CUSTOMER where CUSTOMERNAME='" + Customername + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 5;
                            return result;
                        }
                    }
                }
                else
                {
                    if (SHORTNAME != "")
                    {
                        //Short name not exist in Retailer
                        strcheck = "select top 1 * from M_CUSTOMER where  CUSTYPE_ID <>'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' AND CUSTOMERNAME='" + Customername + "' and CUSTOMERID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 5;
                            return result;
                        }
                    }
                }
                if (ID == "")
                {
                    if (SHORTNAME != "")
                    {
                        strcheck = "select top 1 * from M_USER where USERNAME='" + SHORTNAME + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 7;
                            return result;
                        }
                    }
                }
                else
                {
                    if (SHORTNAME != "")
                    {
                        //Short name not exist in Retailer
                        strcheck = "select top 1 * from M_CUSTOMER where  CUSTYPE_ID <>'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' AND SHORTNAME='" + SHORTNAME + "' and CUSTOMERID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 7;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    sqlstr = "EXEC [SP_CUSTOMER_INSERT] '" + NewCustomerID + "','" + ID + "','" + Customername + "','" + Custypeid + "','" + Custypename + "'," +
                             " '" + Businesssegmentid + "','" + Businesssegmentname + "','" + Groupid + "','" + Groupname + "'," +
                             " '" + Panno + "','" + Vatno + "','" + Cstno + "','" + Tinno + "','" + Contractperson1 + "','" + Contractperson2 + "'," +
                             " '" + Emailid1 + "', '" + Emailid2 + "','" + Mobile1 + "','" + Telephone1 + "','" + Mobile2 + "','" + Telephone2 + "'," +
                             " '" + Address + "','" + Stateid + "','" + Statename + "','" + Districtid + "','" + Districtname + "','" + Cityid + "','" + Cityname + "'," +
                             " '" + Pin + "','" + Address2 + "','" + Pin2 + "','" + UserId + "','" + currentdate + "','" + Mode + "','N'," +
                             " '" + Isactive + "','" + Code + "','" + parentcustid + "','" + parentcustname + "','" + Password + "','" + ReportToRole + "'," +
                             " '" + Percentage + "','" + shopid + "','" + shopname + "','" + RETAILER_CAT_ID + "','" + RETAILER_CAT_NAME + "','" + AMOUNT + "'," +
                             " '" + currencyid + "','" + currencyname + "','" + ledgergroupid + "','" + ledgergroupname + "','" + fileid + "','" + filename + "'," +
                             " '" + tag + "','" + CountryID + "','" + CountryName + "','" + SHORTNAME + "','','','" + LEDGER_REFERENCEID + "','" + CompanyTypeID + "'," +
                             " '" + margin + "'," + marginpercentage + ",'" + gstno + "','" + APPLICABLEGST + "','" + Purchasetypename + "','" + istransfertoho + "'," +
                             "'" + RETDIVID + "','" + RETDIVNAME + "','" + DOB + "','" + ANVDATE + "','" + printname + "','" + IP_ADDRESS + "','','','" + Depotid + "'," +
                             " '" + DepotName + "','" + SOId + "','" + SOName + "','" + Inactivedate + "','" + BrandType + "'," + Tcspercent + "";
                    result = db.HandleData(sqlstr);
                    result = 2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }




        public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string vendorid, string mode)
        {
            int flag = 0;
            string sql = string.Empty;
            try
            {
                if (accid == "")
                {
                    mode = "A";
                }
                else
                {
                    mode = "U";
                }
                sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','" + vendorid + "','T'";
                flag = (int)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }

        public int SaveBrandMapping(string CustomerID, string xml)
        {
            int flag = 0;
            string sql = string.Empty;
            try
            {
                sql = "exec [USP_BRAND_MAPPING_INSERT] '" + CustomerID + "','" + xml + "'";
                flag = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }


        public int DeleteCustomerMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "[SP_CUSTOMER_DELETE] '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public DataTable Depot() // Don't Change because this is used by Auto Posting Leadger creation in account table
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCustDepotMapping(string Customerid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_CUSTOMER_DEPOT_MAPPING] '" + Customerid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable BindProduct(string BRANDID, string CATID)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT  WHERE DIVID='" + BRANDID + "' AND CATID='" + CATID + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory(string ID)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='" + ID + "' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCustomerProductMapping(string Customerid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_CUSTOMER_PRODUCT_MAPPING] '" + Customerid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }
        public int SaveCustomerAddressMapping(string Customerid,string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_CUSTOMER_ADDRESS_MAPPING] '" + Customerid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }
        public DataTable EditProduct(string ID)
        {
            string sql = "EXEC SP_CUSTOMER_PRODUCT_EDIT '" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditAddressMapping(string CustomerID)
        {
            string sql = "EXEC SP_CUSTOMER_ADDRESS_EDIT '" + CustomerID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTSI()
        {
            string sql = "SELECT U.USERID,U.FNAME + ' ' + U.LNAME AS USERNAME FROM M_USER U INNER JOIN P_APPMASTER P ON U.USERTYPE=P.TSI_TYPEID OR U.USERTYPE=P.SO_TYPEID  ORDER BY USERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveTSIMapping(string CUSTOMERID,string CUSTOMERNAME,string TSI_ID,string TSI_NAME)
        {
            string sql = string.Empty;
            int result=0;
            sql = "[SP_CUSTOMER_TSI_INSERT] '" + CUSTOMERID + "','" + CUSTOMERNAME + "','" + TSI_ID + "','" + TSI_NAME + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable EditTsimapping(string CUSTOMERID)
        {
            string sql = "SELECT CUSTOMERNAME, TSI_ID, TSI_NAME FROM M_CUSTOMER_TSI_MAPPING  WHERE CUSTOMERID='" + CUSTOMERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SavePLOTMapping(string CUSTOMERID, string CUSTOMERNAME, string Latitude, string Longitude)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "UPDATE M_CUSTOMER SET LATITUDE='" + Latitude + "',LONGITUDE='" + Longitude + "' WHERE CUSTOMERID='" + CUSTOMERID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable BindAccGroup()
        {
            string sql = "SELECT Code,grpName FROM Acc_AccountGroup ORDER BY grpName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAccGroupKKG()
        {
            //string sql = "SELECT Code,grpName FROM Acc_AccountGroup where parentId='8a11fa2b-ab74-44f4-85e1-c9a1a055961e' ORDER BY grpName";
            string sql = "exec [USP_ACC_FILL_SUNDRY_Debtors_GROUP]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetFile(string id)
        {

            string sql = "SELECT FILEID,FILENAME FROM M_CUSTOMER WHERE CUSTOMERID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveuploadVatfiles(string fileid,string filename)
        {
            int result = 0;
            string sqlstr;
            try
            {
                
                   // string sql = "SELECT ID FROM M_INSURANCEDOCUMENT WHERE INSURANCE_NO='" + name + "' ";
                   // DataTable dt = new DataTable();
                    //dt = db.GetData(sql);
                    //fileid = dt.Rows[0]["FILEID"].ToString();

                    sqlstr = "INSERT INTO M_CUSTOMER(FILENAME,FILEID)" +
                      " values('" + filename + "',NEWID())";
               
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindTransporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCustomerTransporterMapping(string Customerid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_CUSTOMER_TRANSPORTER_MAPPING] '" + Customerid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable EditTransporter(string ID)
        {
            string sql = " SELECT ISNULL(TRANSPORTERID,'') AS TRANSPORTERID,ISNULL(TRANSPORTERNAME,'') AS TRANSPORTERNAME,ISNULL(CUSTYPE_ID,'') AS CUSTYPE_ID," +
                         " ISNULL(CUSTOMERID,'') AS CUSTOMERID,ISNULL(CUSTOMERNAME,'') AS CUSTOMERNAME FROM M_CUSTOMER_TRANSPORTER WHERE CUSTOMERID = '" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        
        public int SaveDistributor( string UserID, string BSID, string BSNAME, string GRPID, string GRPNAME, string CountryID, string CountryName,
                                    string CurrencyID, string CurrencyName, string StateID, string StateName, string AcccGroupID, string AcccGroupName,
                                    string finyear, string depotid, string xmlDistributor, string xmlAccountInfo, string xmlOpeningBalance)
        {
            int result = 0;
            string sqlstr;
            
            try
            {
                {
                    sqlstr = " EXEC [USP_DISTRIBUTOR_INSERT] '" + UserID + "','" + BSID + "','" + BSNAME + "','" + GRPID + "','" + GRPNAME + "','" + CountryID + "'," +
                             " '" + CountryName + "','" + CurrencyID + "','" + CurrencyName + "','" + StateID + "','" + StateName + "','" + AcccGroupID + "'," +
                             " '" + AcccGroupName + "','" + finyear + "','" + depotid + "','" + xmlDistributor + "','" + xmlAccountInfo + "','" + xmlOpeningBalance + "'";
                    result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public DataTable GetGroupID(string ID)
        {
            string sql = " SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY WHERE BUSINESSSEGMENTID='" + ID + "' ORDER BY DIS_CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable  customercheck(string Customer)
        {
            string sql = " SELECT CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERNAME='"+ Customer + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetLedgerbyBSID(string BSID)
        {
            string sql = " EXEC [USP_GET_LEDGERGROUP] '" + BSID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bindtemplete()
        {
            string sql = " SELECT ISNULL('','')AS CODE	,ISNULL('','')AS CUSTOMERNAME,ISNULL('','')AS	PANCARDNO,ISNULL('','')AS	GSTNO," +
                        " ISNULL('','')AS	CSTNO,ISNULL('','')AS	TINNO,ISNULL('','')AS	CONTACTPERSON1,ISNULL('','')AS	CONTACTPERSON2, " +
                        " ISNULL('','')AS	EMAILID1,ISNULL('','')AS	EMAILID2," +
                        " ISNULL('','')AS MOBILE1,ISNULL('','')AS	TELEPHONE1,ISNULL('','')AS	MOBILE2,ISNULL('','')AS	TELEPHONE2,ISNULL('','')AS	ADDRESS," +
                        " ISNULL('','')AS	PIN,ISNULL('','')AS	SHORTNAME FROM M_CUSTOMER ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessSegmentForUploadCustomer()
        {
            string sql = "SELECT * FROM M_BUSINESSSEGMENT WHERE ACTIVE='True'  ORDER BY BSID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int UpdateLdomCustomerMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE M_CUSTOMER SET  LDTOM=GETDATE() WHERE CUSTOMERID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        /*Added By Sayan Dey On 01/01/2018*/
        public DataTable BindRetailerDivision()
        {
            string sql = "SELECT RETDIVID,RETDIVNAME FROM M_RETAILER_DIVISION ORDER BY RETDIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerGridbyBSGroup(string UserId, string BSID, string depotid, string Custype, string AccGroup)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC [USP_RPT_CUSTOMER_LIST] '" + BSID + "','" + depotid + "','" + Custype + "','" + AccGroup + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #region Bind Customer Grid SO Wise (Add By Rajeev 04-09-2018)
        public DataTable BindCustomerGridSO_Wise(string AsonDt, string Depot, string Distibutor,string Status)
        {
            string sql = " EXEC USP_RETRIVE_RETAILOR '" + AsonDt + "','" + Depot + "','" + Distibutor + "','" + Status + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Add by Arti on 08-10-2018
        public DataTable BindSOBYDEPOT(string depotid)
        {

            string sql = " SELECT A.USERID,(FNAME + ' ' + MNAME + ' ' + LNAME + ' ' +'('+ ISNULL(HQNAME,'NOT AVAILABLE') + ')') AS USERNAME FROM M_USER AS A" +
                         " INNER JOIN M_TPU_USER_MAPPING AS B ON A.USERID = B.USERID AND  TPUID = '" + depotid + "'" +
                         " AND A.USERTYPE = '9BF42AA9-0734-4A6A-B835-0885FBCF26F5' AND ISACTIVE='1' ORDER BY (FNAME + ' ' + MNAME + ' ' + LNAME) ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }




        public DataTable BindDepotByCustomer(string userid)
        {

            string sql = " SELECT TOP(1) DEPOTID  FROM M_CUSTOMER_DEPOT_MAPPING " +
                         " WHERE DEPOTID<>'24E1EF07-0A41-4470-B745-9E4BA164C837' AND CUSTOMERID='" + userid + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSObyCustomer(string userid, string depotid)
        {

            string sql = " EXEC USP_SELECT_SO_FOR_CUSTOMER '" + userid + "','" + depotid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable Bindsaveddepotbycustid(string customerid)
        {
            /*Bind All depot except HeadOffice Kolkata(CO)*/
            string sql = "SELECT DEPOTID,DEPOTNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID<>'24E1EF07-0A41-4470-B745-9E4BA164C837' AND CUSTOMERID='" + customerid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        public DataTable BindDistributorDepotWise(string depotid) /* ADDED ON 05/01/2019 */
        {
            string sql = "SELECT DISTINCT A.CUSTOMERID,A.CUSTOMERNAME FROM M_CUSTOMER A INNER JOIN M_CUSTOMER_DEPOT_MAPPING B " +
                        " ON A.CUSTOMERID = B.CUSTOMERID WHERE B.DEPOTID = '" + depotid + "' ORDER BY A.CUSTOMERNAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BranchDepot() /* ADDED ON 05/01/2019 */
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #region BindComplaintDetailsfordashbord
        public DataTable BindComplaintDetailsfordashbord(string userid)
        {
            string sql = "sp_complaint_details_count'"+ userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        public DataTable loadsotsidistributor(string userid,string mode) /* ADDED ON 29/03/2019 */
        {
            string sql = "exec[so_tsi_customer_asmwise] '"+ userid + "','"+ mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int Asmexceluploadfortsidistributor(string ASMID,string xml,string soid,string tsiid,string distributorid,string date) /* added on 31/03/2019 */
        {
            string sql = string.Empty;
            int result = 0;
            sql = "exec[USP_ASM_TSI_DISTRIBUTOR_EXCEL_UPLOAD] '" + ASMID + "','" + xml + "','"+ soid + "','"+ tsiid + "','"+ distributorid + "','"+date+"'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable loadasmgrid(string asmid)
        {
            string sql = "SELECT CONVERT(VARCHAR,DTOC,103) AS ENTRYDATE,TOWNNAME,BEATNAME,OUTLATENAME,PLANEDSALEFORTHEDAY,STORECHECK,MERCHANDISING,RANGESELLING,LBPC,FOCUSPRODUCTDEMO,ISSUERESOLUTION,SUPPLYVISITDATE,RELATIONSHIPBUILDING, " +
                         " TOTALUNITCOUNT,TOTALVALUEACHIVE,SONAME,TSINAME,DISTRIBUTORNAME FROM M_ASM_DOCUMENTUPLOAD where ASMID = '"+ asmid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable bindexcel()
        {
            string sql = " SELECT '' AS[TOWNNAME],'' AS[BEATNAME],'' AS[OUTLATENAME],''AS[PLANEDSALEFORTHEDAY_RS],'' AS[STORECHECK_YORN],'' AS[MERCHANDISING_YORN],'' AS[RANGESELLING_YORN],'' AS[LBPCCOUNT], "+
                         "'' AS[FOCUSPRODUCTDEMO_YORN],''AS[ISSUERESOLUTION_YORN],'' AS[SUPPLYVISITDATE_YORN],''AS[RELATIONSHIPBUILDING_YORN],'' AS[TOTALUNITCOUNT],'' AS[TOTALVALUEACHIVE] ";
                       
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SaveUploadCustomer(string xml, string UserID, string IPAddress)
        {

            DataTable flag = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "EXEC [USP_Insert_Upload_Customer] '" + xml + "','" + UserID + "','" + IPAddress + "'";
                flag = db.GetData(sql);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }

    }


}
