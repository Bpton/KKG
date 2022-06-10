using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsClaim
    {
        DBUtils db = new DBUtils();

        /*******************FOR DISPLAY CLAIM***********************************************/

        public DataTable GETTARGETSLAEID()
        {
            string sql = "SELECT TARGETSALESID,KOLKATADEPOTID FROM P_APPMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindClaimNarration(string CLAIM_TYPE, string USERTAG, string BSID, string DEPOTID)
        {   
            string sql = "EXEC [USP_GET_NARRATION] '" + CLAIM_TYPE + "','" + USERTAG + "' ,'" + BSID + "','" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindClaimNarration_new(string CLAIM_TYPE, string USERTAG, string BSID, string DEPOTID,string FinYear)
        {
            string sql = "EXEC [USP_GET_NARRATION] '" + CLAIM_TYPE + "','" + USERTAG + "' ,'" + BSID + "','" + DEPOTID + "','"+ FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimNarration_new(string CLAIM_TYPE, string USERTAG, string BSID, string DEPOTID,string FinYear, string GRID)
        {
            string sql = "EXEC [USP_GET_NARRATION] '" + CLAIM_TYPE + "','" + USERTAG + "' ,'" + BSID + "','" + DEPOTID + "','" + FinYear + "','"+ GRID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindexportClaimNarration_new(string BSID, string FinYear)
        {
            string sql = "EXEC [USP_GET_EXPORT_NARRATION] '" + BSID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimReason(string menuid)
        {
            string sql = "SELECT ID,NAME FROM M_REASON MR INNER JOIN M_REASON_MENU_MAPPING MRMAP ON MR.ID=MRMAP.REASONID WHERE MRMAP.MENUID='" + menuid + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimPeriod(string narrationid)
        {
            string sql = "SELECT FROM_DATE,TO_DATE FROM M_CLAIM_NARRATION WHERE NARRATIONID='" + narrationid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindExportClaimPeriod(string narrationid)
        {
            string sql = "SELECT FROM_DATE,TO_DATE FROM M_EXPORT_CLAIM_NARRATION WHERE NARRATIONID ='" + narrationid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSCODE IN ('GT','MT','EC') ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindExportBusinessSegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSCODE = 'EXP'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBusinessSegment(string UserId)
        {
            //BUSINESSSEGMENTID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserId + "'),','))
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT WHERE BSID IN(select *  from dbo.fnSplit((select BUSINESSSEGMENTID from M_CUSTOMER where CUSTOMERID='" + UserId + "'),','))";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindPrincipleGroup()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT DIS_CATID,DIS_CATNAME FROM M_DISTRIBUTER_CATEGORY WHERE ACTIVE ='T' ORDER BY DIS_CATNAME";
            dt = db.GetData(sql);
            return dt;
        }


        //===================Minor Changes by Sourav Mukherjee on 16/05/2016=========================//
        public DataTable BindBrand()
        {
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            string sql = "SELECT CATID, CATNAME, CATCODE FROM M_CATEGORY ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbyBrand(string BRANDID, string CATID)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT WHERE DIVID='" + BRANDID + "' AND CATID='" + CATID + "'  ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //=================================================================================================//


        public DataTable BindProduct(string CATID)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT WHERE  CATID='" + CATID + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct()
        {
            string sql = "SELECT ID AS PRODUCTID,NAME AS PRODUCTNAME FROM  M_PRODUCT WHERE ACTIVE='T' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotAll()
        {
            string sql = " SELECT  BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRID NOT IN ('24E1EF07-0A41-4470-B745-9E4BA164C837') ORDER BY BRPREFIX ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotAll_withTarget()
        {
            string sql = "SELECT  A.BRID ,A.BRPREFIX AS BRNAME FROM M_BRANCH A UNION ALL SELECT CUSTOMERID AS BRID,CUSTOMERNAME AS BRNAME FROM M_CUSTOMER WHERE CUSTOMERID=(SELECT TARGETSALESID FROM P_APPMASTER)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_kolkta()
        {
            string sql = "SELECT CUSTOMERID AS BRID,CUSTOMERNAME AS BRNAME FROM M_CUSTOMER WHERE CUSTOMERID=(SELECT TARGETSALESID FROM P_APPMASTER)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorByDepot_kolkata(string DepotID, string BSID)
        {
            DepotID = "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670";
            string sql = " SELECT DISTINCT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID = B.CUSTOMERID " +
                         " WHERE A.DEPOTID='" + DepotID + "' AND B.BUSINESSSEGMENTID IN ('" + BSID + "') ORDER BY B.CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindDropdownPrincipleGroup(string ID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (ID == "0AA9353F-D350-4380-BC84-6ED5D0031E24")
            {
                sql = "select DIS_CATID,DIS_CATNAME from M_DISTRIBUTER_CATEGORY where BUSINESSSEGMENTID='" + ID + "' AND ISCLAIMCHAIN='Y'";
            }
            else
            {
                sql = "select DIS_CATID,DIS_CATNAME from M_DISTRIBUTER_CATEGORY where BUSINESSSEGMENTID='" + ID + "'";
            }
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorAll()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER  ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributorSingle(string customerid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + customerid + "'  ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributor(string BSID, string GRPID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID like '%" + BSID + "%' AND GROUPID like '%" + GRPID + "%' ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorUnderTSI(string BSID, string GRPID, string TSIID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE PARENT_CUST_ID='" + TSIID + "' AND  BUSINESSSEGMENTID like '%" + BSID + "%' AND GROUPID like '%" + GRPID + "%' ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorUnderTSI(string TSIID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE PARENT_CUST_ID='" + TSIID + "'  ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributor(string BSID, string GRPID, string customerid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE BUSINESSSEGMENTID like '%" + BSID + "%' AND GROUPID like '%" + GRPID + "%' AND CUSTOMERID='" + customerid + "'  ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRetailer()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER  WHERE CUSTYPE_NAME IN ('RETAILER','MT-CLIENT','CPC CLIENT','CSD CLIENT') ORDER BY CUSTYPE_NAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRetailer(string ID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER WHERE PARENT_CUST_ID =(SELECT USERID FROM M_USER WHERE REFERENCEID='" + ID + "') ORDER BY CUSTOMERNAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveDisplayClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string retrid, string retrname, string TypeID, string UserID, string FinYear, string remarks, string claimamt, string depotid, string depotname, string claimdate, string xml)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_DISPLAY_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "','" + grname + "','" + distid + "','" + distname + "','" + retrid + "','" + retrname + "'," +
                                                     "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + remarks + "'," + Convert.ToDecimal(claimamt) + ",'" + depotid + "','" + depotname + "','" + claimdate + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveExportClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string TypeID, string UserID, string FinYear, string remarks, string claimamt, string depotid, string depotname, string claimdate, string xml)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC [USP_EXPORT_CLAIM_INSERT] '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "','" + grname + "','" + distid + "','" + distname + "'," +
                                                     "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + remarks + "'," + Convert.ToDecimal(claimamt) + ",'" + depotid + "','" + depotname + "','" + claimdate + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDisplayClaim(string claimtype, string userid,string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_CLAIM_LOAD_DETAILS] '" + claimtype + "','" + userid + "','"+ FinYear + "'";

            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindExportClaim(string claimtype, string userid, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_EXPORT_CLAIM_LOAD_DETAILS] '" + claimtype + "','" + userid + "','" + FinYear + "'";

            dt = db.GetData(sql);
            return dt;

        }

        public DataSet GetDisplayCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_DISPLAY_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }
        

        public DataSet GetExportCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC [USP_EXPORT_CLAIM_DETAILS] '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public int deleteDisplayClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_DISPLAY_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }


        public int deleteExportClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC [USP_EXPORT_CLAIM_DELETE] '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable SaveApprovedClaim(string ClaimID, string Tag, string UID, string claimtypeid, string rid, string rname, string depotid, string finyear, decimal ClaimAmt, string claimdate, string remarks, string receiveddate, string USERID, string processdate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_DISPLAY_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + rid + "','" + rname + "','" + depotid + "','" + finyear + "'," + ClaimAmt + ",'" + claimdate + "','" + remarks + "','" + receiveddate + "','" + USERID + "','" + processdate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveApprovedExportClaim(string ClaimID, string Tag, string UID, string claimtypeid, string rid, string rname, string depotid, string finyear, decimal ClaimAmt, string claimdate, string remarks, string receiveddate, string USERID, string processdate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC [USP_EXPORT_CLAIM_UPDATE] '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + rid + "','" + rname + "','" + depotid + "','" + finyear + "'," + ClaimAmt + ",'" + claimdate + "','" + remarks + "','" + receiveddate + "','" + USERID + "','" + processdate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        /*******************END FOR DISPLAY CLAIM***********************************************/

        /*******************START FOR QVPS CLAIM***********************************************/
        public DataTable BindPacksize(string ProductID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT PSID,PSNAME FROM VW_SALEUNIT" +
                  " WHERE PRODUCTID='" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindQVPSClaim(string claimtype, string userid,string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_CLAIM_LOAD_DETAILS] '" + claimtype + "','" + userid + "','"+FinYear+"'";

            dt = db.GetData(sql);
            return dt;

        }

        public DataTable GetQVPSClaimScheme(string userid)
        {
            DataTable dt = new DataTable();
            DataTable dtclaimscheme = new DataTable();
            string Date = string.Empty;
            string ProductID = string.Empty;
            string GroupID = string.Empty;
            string BSID = string.Empty;
            string Quantity = string.Empty;
            string PackSizeId = string.Empty;
            string sql = string.Empty;
            //sql = " SELECT CL_H.ID,CL_D.CLAIM_PRODUCT_ID,CL_D.CLAIM_PRODUCT_NAME,CL_D.CLAIM_CAT_ID,CL_D.CLAIM_CAT_NAME," +
            //      " CONVERT(VARCHAR(10),CAST(CL_D.FROMDATE AS DATE),103) AS FROMDATE,CONVERT(VARCHAR(10),CAST(CL_D.TODATE AS DATE),103) AS TODATE,CL_D.AMOUNT " +
            //           "FROM CL_DISPLAY_CLAIM_HEADER CL_H INNER JOIN CL_DISPLAY_CLAIM_DETAILS CL_D ON CL_H.ID=CL_D.CLAIMID";

            sql = "SELECT CL_H.ID,CL_H.QV_CLM_NO,CL_H.CLAIM_BUSINESSSEGMENT_ID, CL_H.CLAIM_BUSINESSSEGMENT_NAME, CL_H.CLAIM_PRINCIPLEGROUP_ID, CL_H.CLAIM_PRINCIPLEGROUP_NAME, CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME, CL_H.CLAIM_RETAILER_ID, CL_H.CLAIM_RETAILER_NAME" +
                 ",SUM(ISNULL(CL_D.QTY,0)) AS QUANTITY FROM CL_QVPS_CLAIM_HEADER CL_H INNER JOIN CL_QVPS_CLAIM_DETAILS CL_D ON CL_H.ID=CL_D.CLAIMID AND (NEXT_LEVEL_APPROVAL_UID=" + userid + " OR CBU=" + userid + ") " +
                 " GROUP BY CL_H.ID,CL_H.QV_CLM_NO,CL_H.CLAIM_BUSINESSSEGMENT_ID, CL_H.CLAIM_BUSINESSSEGMENT_NAME, CL_H.CLAIM_PRINCIPLEGROUP_ID, CL_H.CLAIM_PRINCIPLEGROUP_NAME, CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME, CL_H.CLAIM_RETAILER_ID, CL_H.CLAIM_RETAILER_NAME ORDER BY CL_H.QV_CLM_NO";

            dtclaimscheme = db.GetData(sql);

            if (dtclaimscheme.Rows.Count > 0)
            {
                Date = Convert.ToString(dtclaimscheme.Rows[0]["FROM_DATE"]).Trim();
                ProductID = Convert.ToString(dtclaimscheme.Rows[0]["CLAIM_PRODUCT_ID"]).Trim();
                GroupID = Convert.ToString(dtclaimscheme.Rows[0]["CLAIM_PRINCIPLEGROUP_ID"]).Trim();
                BSID = Convert.ToString(dtclaimscheme.Rows[0]["CLAIM_BUSINESSSEGMENT_ID"]).Trim();
                Quantity = Convert.ToString(dtclaimscheme.Rows[0]["QUANTITY"]).Trim();
                PackSizeId = Convert.ToString(dtclaimscheme.Rows[0]["CLAIM_PACKSIZE_ID"]).Trim();
            }
            string SQLFunction = "EXEC USP_GetQPS_QTYScheme_CLAIM '" + Date + "','" + ProductID + "','" + GroupID + "','" + BSID + "'," + Quantity + ",'" + PackSizeId + "'";
            dt = db.GetData(SQLFunction);
            return dt;


        }

        public DataSet GetQVPSCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_QVPS_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataTable GetQVPSSchemeOfferDetails(string ClaimID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " EXEC USP_GETQVPS_SCHEME_OFFER_CLAIM '" + ClaimID + "'";
            dt = db.GetData(sql);

            return dt;
        }

        public DataTable SaveQVPSClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string retrid,
                                string retrname, string TypeID, string UserID, string FinYear, string remarks, string tag, decimal Amount, string depotid, string depotname, string date, string xml, string xmlShceme)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_QVPS_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "','" + grname + "','" + distid + "','" + distname + "','" + retrid + "','" + retrname + "'," +
                                                     "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + remarks + "','" + tag + "'," + Amount + ",'" + depotid + "','" + depotname + "','" + date + "','" + xml + "','" + xmlShceme + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable SaveApproved_QVPS_Claim(string ClaimID, string Tag, string UID, string claimtypeid, string REASON_ID, string REASON_NAME, string depotid, string finyear, decimal mod_amt, string claimdate, string remarks, string receiveddate, string userid, string processdate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_QVPS_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + REASON_ID + "','" + REASON_NAME + "','" + depotid + "','" + finyear + "'," + mod_amt + ",'" + claimdate + "','" + remarks + "','" + receiveddate + "','" + userid + "','" + processdate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        /*******************END FOR QVPS CLAIM***********************************************/

        /*******************FOR WORK FLOW SETTINGS***********************************************/
        public DataTable BindClaimType()
        {
            string sql = "SELECT CLAIM_ID,CLAIM_TYPE,LEDGERID FROM M_CLAIM_TYPE ORDER BY CLAIM_ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindClaimLedger()
        {
            string sql = "EXEC USP_RPT_LOAD_CLAIM_LEDGER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUser()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveWorkFlow(string Mode, string ClaimTypeID, string xml)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "EXEC USP_WORK_FLOW_CLAIM_INSERT '" + Mode + "','" + ClaimTypeID + "'," + "'" + xml + "'";
            d = db.HandleData(sql);
            return d;
        }

        public int SaveClaimLedger(string ClaimTypeID, string ledgerid)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "UPDATE  M_CLAIM_TYPE SET LEDGERID= '" + ledgerid + "' WHERE  CLAIM_ID='" + ClaimTypeID + "'";
            d = db.HandleData(sql);
            return d;
        }

        public DataTable BindUserByClaimID(string clmid)
        {
            string sql = "SELECT M_WORKFLOW.LEVEL AS SLNO, M_USERTYPE.UTID AS USERTYPEID,M_USERTYPE.UTNAME ,M_WORKFLOW.CLAIMTYPEID,'' AS CLAIMTYPENAME,M_WORKFLOW.ISEDIT FROM M_WORKFLOW INNER JOIN M_USERTYPE ON  M_WORKFLOW.USERTYPEID=M_USERTYPE.UTID WHERE M_WORKFLOW.CLAIMTYPEID='" + clmid + "' ORDER BY M_WORKFLOW.LEVEL ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*******************END FOR WORK FLOW***************************************************/

        /*******************FOR DASHBOARD WORK FLOW ***********************************************/
        public DataTable BindClaimTypeInGrid(int uid,string FinYear)
        {
            //string sql = "SELECT M_CLAIM_TYPE.CLAIM_ID, CLAIM_TYPE,COUNT(CL_D.CLAIMID) AS NO_PENDING_ITEMS,PAGE_URL FROM M_CLAIM_TYPE INNER JOIN CL_DISPLAY_CLAIM_HEADER CL_H ON M_CLAIM_TYPE.CLAIM_ID=CL_H.CLAIM_TYPEID INNER JOIN CL_DISPLAY_CLAIM_DETAILS CL_D ON CL_H.ID=CL_D.CLAIMID WHERE CL_H.CBU='" + uid + "' and ISAPPROVED='N' GROUP BY CLAIM_ID,[CLAIM_TYPE],PAGE_URL ORDER BY CLAIM_ID";

            string sql = "EXEC USP_GET_DASHBOARD_CLAIMS '" + uid + "','"+ FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*******************END FOR DASHBOARD WORK FLOW ***********************************************/

        /****************** CODE FOR TRANSPORTER CLAIM ************************************************/

        public DataTable BindReason()
        {
            string sql = "SELECT ID,NAME FROM M_REASON";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepot()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_Transporter()
        {
            string sql = " SELECT A.ID,A.NAME " +
                          " FROM M_CUSTOMER_TRANSPORTER_DMS AS A INNER JOIN" +
                               " M_TRANSPOTER_CUSTOMER_MAP_DMS AS B " +
                          " ON A.ID=B.TRANSPOTERID" +
                          " ORDER BY A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepot_Transporter(string CustomerID)
        {
            string sql = " SELECT A.ID,A.NAME " +
                         " FROM M_CUSTOMER_TRANSPORTER_DMS AS A INNER JOIN" +
                              " M_TRANSPOTER_CUSTOMER_MAP_DMS AS B " +
                         " ON A.ID=B.TRANSPOTERID" +
                         " WHERE B.CUSTOMERID='" + CustomerID + "'" +
                         " ORDER BY A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransporter(string id)
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER WHERE ID='" + id + "'ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveTRANSPORTERClaim(string ClaimID, string Mode, string transid, string transname, string uns_transname, string deoptid, string depotname, string distid, string distname, string retrid, string retrname, string TypeID, string UserID, string FinYear, string maker_tag, string tag, string Remarks, decimal CLAIM_AMT, string xml)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "EXEC USP_TRANSPORTER_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + transid + "','" + transname + "','" + uns_transname + "','" + deoptid + "','" + depotname + "','" + distid + "','" + distname + "','" + retrid + "','" + retrname + "'," +
                                                     "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + maker_tag + "','" + tag + "','" + Remarks + "','" + CLAIM_AMT + "','" + xml + "'";
            d = db.HandleData(sql);
            return d;
        }

        public int deleteTransporterClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_TRANSPORTER_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataSet GetTransporterCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_TRANSPORTER_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataTable BindTransporterClaim(string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "SELECT CONVERT(VARCHAR,CL_H.DTOC,103) AS CLAIM_DATE,CL_H.ID,CL_H.TRANS_CLM_NO,CL_H.CLAIM_TRANSPORTER_ID, CL_H.CLAIM_TRANSPORTER_NAME,  CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME, CL_H.CLAIM_RETAILER_ID, CL_H.CLAIM_RETAILER_NAME" +
                ",SUM(ISNULL(CL_D.AMOUNT,0)) AS AMOUNT,case  when CURRENTSTATUS='N' or (ISAPPROVED='N' AND CURRENTSTATUS='A') then 'Pending' when CURRENTSTATUS='R' then 'Rejected' when ISAPPROVED='Y' then  'Approved' end as CURRENTSTATUS  FROM CL_TRANSPORTER_CLAIM_HEADER CL_H INNER JOIN CL_TRANSPORTER_CLAIM_DETAILS CL_D ON CL_H.ID=CL_D.CLAIMID AND (NEXT_LEVEL_APPROVAL_UID=" + userid + " OR CBU=" + userid + ") " +
                "  GROUP BY CL_H.DTOC,CL_H.ID,CL_H.TRANS_CLM_NO,CL_H.CLAIM_TRANSPORTER_ID, CL_H.CLAIM_TRANSPORTER_NAME,  CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME, CL_H.CLAIM_RETAILER_ID, CL_H.CLAIM_RETAILER_NAME,case  when CURRENTSTATUS='N' or (ISAPPROVED='N' AND CURRENTSTATUS='A') then 'Pending' when CURRENTSTATUS='R' then 'Rejected' when ISAPPROVED='Y' then  'Approved' end  ORDER BY CL_H.TRANS_CLM_NO";

            dt = db.GetData(sql);
            return dt;

        }

        public int SaveApproved_Transporter_Claim(string ClaimID, string Tag, string UID, string claimtypeid, string REASON_ID, string REASON_NAME)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "EXEC USP_TRANSPORTER_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + REASON_ID + "','" + REASON_NAME + "'";
            d = db.HandleData(sql);
            return d;
        }


        /****************** END OF CODE FOR TRANSPORTER CLAIM ************************************************/

        /****************** CODE FOR DAMAGE CLAIM ************************************************/


        public DataSet GetDamageCliamInvoiceBaseRate(string inv_id, string prod_id, string pack_size, string batch_no, string mrp)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_DAMAGE_CLAIM_INVOICE_BASERATE '" + inv_id + "','" + prod_id + "','" + pack_size + "','" + batch_no + "','" + mrp + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable SaveDamageClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string retrid,
                                         string retrname, string TypeID, string UserID, string FinYear, string remarks, string tag, decimal CLAIM_AMT, string depotid,
                                         string depotname, string claimdate, string xml)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_DAMAGE_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "','" + grname + "','" + distid + "','" + distname + "','" + retrid + "','" + retrname + "'," +
                                                     "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + remarks + "','" + tag + "','" + CLAIM_AMT + "','" + depotid + "','" + depotname + "','" + claimdate + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveApproved_Damage_Claim(string ClaimID, string Tag, string UID, string claimtypeid, string REASON_ID, string REASON_NAME, decimal MOD_AMT, string depotid, string finyear, string claimdate, string receiveddate, string userid, string processdate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_DAMAGE_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + REASON_ID + "','" + REASON_NAME + "'," + MOD_AMT + ",'" + depotid + "','" + finyear + "','" + claimdate + "','" + receiveddate + "','" + userid + "','" + processdate + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindDamageClaim(string claimtype, string userid,string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_CLAIM_LOAD_DETAILS] '" + claimtype + "','" + userid + "','"+ FinYear + "'";

            dt = db.GetData(sql);
            return dt;

        }

        public int deleteDamageClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_DAMAGE_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataSet GetDAMAGECliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_DAMAGE_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        /****************** END OF CODE FOR DAMAGE CLAIM ************************************************/





        /********************* FOR FILE UPLOAD ********************************************************/
        public int Saveuploadfiles(string cfileid, string filename, string Mode)
        {
            int result = 0;
            string sqlstr;
            //string fileid;

            try
            {


                sqlstr = "INSERT INTO M_CLAIMFILES(Name,CLAIMID)" +
                            " values('" + filename + "','" + cfileid + "')";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        // Get a file from the database by ID
        public DataTable GetAFile(string id)
        {

            string sql = "SELECT  Name as filename  FROM M_CLAIMFILES  where CLAIMID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteFileBYID(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_CLAIMFILES WHERE CLAIMID = '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        /*************************** END FO FILE UPLOAD ***************************************************************************/



        public DataTable BindMarginClaim(string claimtypeid, string userid,string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "EXEC USP_CLAIM_LOAD_DETAILS '" + claimtypeid + "','" + userid + "','"+ FinYear + "' ";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable SaveApprovedMarginClaim(string ClaimID, string Tag, string UID, string claimtypeid, string REASON_ID, string REASON_NAME, string depotid, string finyear, decimal claim_modi_amt, string claimdate, string remarks, string receiveddate, string userid, string processdate)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_MARGIN_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + REASON_ID + "','" + REASON_NAME + "','" + depotid + "','" + finyear + "','" + claim_modi_amt + "','" + claimdate + "','" + remarks + "','" + receiveddate + "','" + userid + "','" + processdate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveMarginClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string TypeID,
                                         string UserID, string FinYear, string Remarks, decimal CLAIM_AMT, string depotid, string depotname, string claimdate, string xml)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_MARGIN_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "'," +
                                                "'" + grname + "','" + distid + "','" + distname + "'," +
                                                "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + Remarks + "','" + CLAIM_AMT + "','" + depotid + "','" + depotname + "','" + claimdate + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet GetCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_MARGIN_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataTable BindClaimProduct(string SALEINVOICEID)
        {
            string sql = "SELECT PRODUCTID,PRODUCTNAME FROM T_SALEINVOICE_DETAILS_DMS D INNER JOIN T_SALEINVOICE_HEADER_DMS H ON D.SALEINVOICEID=H.SALEINVOICEID WHERE H.SALEINVOICENO = '" + SALEINVOICEID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindClaimMRP(string SALEINVOICEID, string PRODUCTID)
        {
            string sql = "SELECT MRP FROM T_SALEINVOICE_DETAILS_DMS D INNER JOIN T_SALEINVOICE_HEADER_DMS H ON D.SALEINVOICEID=H.SALEINVOICEID WHERE H.SALEINVOICENO = '" + SALEINVOICEID + "' AND PRODUCTID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //public DataTable BindSSMClaim()
        //{
        //    DataTable dt = new DataTable();
        //    string sql = string.Empty;
        //    sql = "SELECT CL_H.ID,CL_H.SSM_CLM_NO,CL_H.CLAIM_BUSINESSSEGMENT_ID, CL_H.CLAIM_BUSINESSSEGMENT_NAME, CL_H.CLAIM_PRINCIPLEGROUP_ID, CL_H.CLAIM_PRINCIPLEGROUP_NAME, CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME," +
        //            "SUM(ISNULL(CL_D.AMOUNT,0)) AS AMOUNT FROM CL_SSM_CLAIM_HEADER CL_H INNER JOIN CL_SSM_CLAIM_DETAILS CL_D ON CL_H.ID=CL_D.CLAIMID GROUP BY CL_H.ID,CL_H.SSM_CLM_NO,CL_H.CLAIM_BUSINESSSEGMENT_ID, CL_H.CLAIM_BUSINESSSEGMENT_NAME, CL_H.CLAIM_PRINCIPLEGROUP_ID, CL_H.CLAIM_PRINCIPLEGROUP_NAME, CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME ORDER BY CL_H.SSM_CLM_NO";
        //    //"GROUP BY CL_H.ID,CL_H.SSM_CLM_NO,CL_H.CLAIM_BUSINESSSEGMENT_ID, CL_H.CLAIM_BUSINESSSEGMENT_NAME, CL_H.CLAIM_PRINCIPLEGROUP_ID, CL_H.CLAIM_PRINCIPLEGROUP_NAME, CL_H.CLAIM_DISTRIBUTOR_ID, CL_H.CLAIM_DISTRIBUTOR_NAME ORDER BY CL_H.SSM_CLM_NO";


        //    dt = db.GetData(sql);
        //    return dt;

        //}

        public DataSet GetSSMCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_SSM_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public DataSet GetMarginCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_MARGIN_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public int deletSSMClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_SSM_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public int DeleteMarginClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_MARGIN_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }
        public int SaveSSMClaim(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string TypeID, string UserID, string FinYear, string Remarks, decimal CLAIM_AMT, string depotid, string depotname, string xml)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "EXEC USP_SSM_CLAIM_INSERT '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "'," +
                                                "'" + grname + "','" + distid + "','" + distname + "'," +
                                                "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + Remarks + "','" + CLAIM_AMT + "','" + depotid + "','" + depotname + "','" + xml + "'";
            d = db.HandleData(sql);
            return d;
        }

        public DataTable BindSSMClaim(string claimtype, string userid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_CLAIM_LOAD_DETAILS] '" + claimtype + "','" + userid + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public int SaveApprovedSSMClaim(string ClaimID, string Tag, string UID, string claimtypeid, string REASON_ID, string REASON_NAME, string depotid, string finyear, decimal mod_amt)
        {
            string sql = string.Empty;
            int d = 0;
            sql = "EXEC USP_SSM_CLAIM_UPDATE '" + ClaimID + "','" + Tag + "','" + UID + "','" + claimtypeid + "','" + REASON_ID + "','" + REASON_NAME + "','" + depotid + "','" + finyear + "'," + mod_amt + "";
            d = db.HandleData(sql);
            return d;
        }


        public int deleteSSMClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_SSM_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable BindQuantityScheme()
        {
            string sql = "SELECT SCHEMEID,SCHEMENAME FROM M_PRIMARY_QTY_SCHEME_HEADER ORDER BY SCHEMEID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet PrimaryQtySchemeDetails(string SchemeID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC sp_PRIMARY_QTY_SCHEME_DETAILS '" + SchemeID + "'";
            ds = db.GetDataInDataSet(sql);

            return ds;
        }

        public string CheckClaimAccountExistence(string CLAIMID)
        {

            string Sql = "  IF  EXISTS(SELECT * FROM [Acc_Voucher_Invoice_Map] where InvoiceID='" + CLAIMID + "') BEGIN " +
                            " select '1'   END  else    begin	  select '0'   end ";

            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckClaimAccountExistence_Display(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_DISPLAY_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));

        }

        public string CheckClaimAccountExistence_Damage(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_DAMAGE_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckClaimAccountExistence_SSM(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_SSM_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));
        }

        public string CheckClaimAccountExistence_Margin(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_SUPERSTOCKIST_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));
        }
        public string CheckClaimAccountExistence_Transporter(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_TRANSPORTER_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));
        }


        public string CheckClaimAccountExistence_QVPS(string CLAIMID)
        {

            string Sql = " SELECT ISAPPROVED FROM CL_QVPS_CLAIM_HEADER WHERE ID='" + CLAIMID + "'";
            return ((string)db.GetSingleValue(Sql));
        }
        public int deleteQVPSClaim(string ClaimID)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC USP_QVPS_CLAIM_DELETE '" + ClaimID + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable BindQPSVPSScheme_Details(string SchemeID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " EXEC USP_GETQVPS_SCHEME_OFFER_DETAILS '" + SchemeID + "'";
            dt = db.GetData(sql);
            return dt;
        }




        public DataTable BindQPSVPSScheme_Name(string schemtype)
        {
            string sql = "SELECT SCHEMEID,SCHEMENAME FROM M_QPS_SCHEME_HEADER  WHERE TYPEOFSCHEMEID='" + schemtype + "' ORDER BY DTOC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindQPSVPSScheme_Details(string SchemeID, string depotid)
        {
            DataSet dt = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_GETQVPS_SCHEME_OFFER_DETAILS '" + SchemeID + "','" + depotid + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable BindQPSVPSScheme_Name(string schemtype, string BSID)
        {
            string sql = "SELECT  A.SCHEMEID,A.SCHEMENAME FROM M_QPS_SCHEME_HEADER A  WHERE A.TYPEOFSCHEMEID='" + schemtype + "'  " +
                            "AND A.SCHEME_BUSINESSSEGMENT_ID='" + BSID + "'  ORDER BY A.DTOC "; //AND  B.PRINCIPLEGROUP_ID IN (SELECT * FROM DBO.SPLIT('AFF95D83-8297-40B3-A140-738F52D3F63C',','))


            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBatch(string distributorid, string Productid)
        {
            string sql = " SELECT DISTINCT BATCHNO FROM T_SALEINVOICE_DETAILS_DMS A INNER JOIN" +
                         " T_SALEINVOICE_HEADER_DMS B ON A.SALEINVOICEID = B.SALEINVOICEID WHERE DISTRIBUTORID='" + distributorid + "' AND PRODUCTID='" + Productid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet GetDamageCliamInvoiceDetails(string prod_id, string pack_size, string batch_no, string mrp, string catid)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_DAMAGE_CLAIM_INVOICE_DETAILS '" + prod_id + "','" + pack_size + "','" + batch_no + "','" + mrp + "','" + catid + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataTable GetSuperStockistCliamInvoiceDetails(string mrp, string catid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_SUPERTOCKIST_CLAIM_INVOICE_DETAILS '" + mrp + "','" + catid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public decimal CalculateAmount(decimal Qty, string ProductID)
        {
            string Sql = "SELECT (" + Qty + "*(SELECT MRP FROM M_PRODUCT WHERE ID='" + ProductID + "')) AS AMOUNT";
            return ((decimal)db.GetSingleValue(Sql));
        }
        public DataSet GetPriceOffCliamDetails(string ClaimID)
        {
            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = " EXEC USP_PRICEOFF_CLAIM_DETAILS '" + ClaimID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindBusinessSegmentByUserID(string UserId)
        {
            string sql = "SELECT DISTINCT BSID AS ID,BSNAME AS NAME FROM M_USER_BS_MAP WHERE USERID='" + UserId + "' ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotByUserID(string UserId)
        {
            string sql = "EXEC USP_CLAIM_LOAD_DEPOT '" + UserId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorByDepot(string DepotID, string BSID)
        {
            string sql = " SELECT DISTINCT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID = B.CUSTOMERID " +
                         " WHERE A.DEPOTID='" + DepotID + "' AND B.BUSINESSSEGMENTID IN ('" + BSID + "') ORDER BY B.CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGroupByDistributor(string CustomerID, string BSID)
        {
            string sql = " SELECT DISTINCT GROUPID,GROUPNAME FROM M_CUSTOMER WHERE CUSTOMERID='" + CustomerID + "' AND BUSINESSSEGMENTID IN ('" + BSID + "') ORDER BY GROUPNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int GetUserLevel(string ClaimTypeID, string SessionID)
        {
            string sql = " SELECT LEVEL FROM M_WORKFLOW A INNER JOIN M_USER B ON A.USERTYPEID = B.USERTYPE WHERE CLAIMTYPEID=" + ClaimTypeID + " AND USERID='" + SessionID + "'";

            int level = Convert.ToInt32(db.GetSingleValue(sql));
            return level;
        }

        public DataTable BindAllProduct()
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllPackSize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID='B9F29D12-DE94-40F1-A668-C79BF1BF4425'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveNarration(string NARRATIONID, string CLAIM_NARRATION, string FROMDATE, string TODATE, string STATUS, string APPLICABLETO, string REFERENCENO, string USERTAG, string BSID, string BSNAME, string GRID, string GRNAME, string rdbtype, string FINYEAR, string XML)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = " EXEC USP_CLAIM_NARRATION_INSERT '" + NARRATIONID + "','" + CLAIM_NARRATION + "','" + FROMDATE + "'," +
                         " '" + TODATE + "','" + APPLICABLETO + "','" + REFERENCENO + "','" + USERTAG + "','" + BSID + "','" + BSNAME + "','" + GRID + "','" + GRNAME + "','" + rdbtype + "','" + FINYEAR + "','" + XML + "'";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }



        public int SaveExportNarration(string NARRATIONID, string CLAIM_NARRATION, string FROMDATE, string TODATE, string STATUS,  string USERTAG, string BSID, string BSNAME,string FINYEAR)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = " EXEC [USP_EXPORT_CLAIM_NARRATION_INSERT_UPDATE] '" + NARRATIONID + "','" + CLAIM_NARRATION + "','" + FROMDATE + "'," +
                         " '" + TODATE + "','" + USERTAG + "','" + BSID + "','" + BSNAME + "','" + FINYEAR + "'";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int DeleteNarration(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM M_CLAIM_NARRATION WHERE NARRATIONID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public int DeleteEXPORTNarration(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM M_EXPORT_CLAIM_NARRATION WHERE NARRATIONID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindNarration(string USERTAG, string BSID, string FINYEAR)
        {
            string sql = string.Empty;
            if (BSID != "0")
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION,A.BSNAME,A.REFERENCENO, CONVERT(VARCHAR(10),CAST(FROM_DATE AS DATE),103) AS FROMDATE , CONVERT(VARCHAR(10),CAST(TO_DATE AS DATE),103) AS TODATE," +
                             " B.CLAIM_TYPE FROM M_CLAIM_NARRATION A LEFT OUTER JOIN M_CLAIM_TYPE B ON A.CLAIM_TYPE=B.CLAIM_ID WHERE USERTAG='" + USERTAG + "' AND A.BSID='" + BSID + "' AND A.FINYEAR='" + FINYEAR + "' ORDER BY CLAIM_TYPE,BSNAME ";
            }
            else
            {
                sql = " SELECT NARRATIONID, CLAIM_NARRATION,A.BSNAME,A.REFERENCENO, CONVERT(VARCHAR(10),CAST(FROM_DATE AS DATE),103) AS FROMDATE , CONVERT(VARCHAR(10),CAST(TO_DATE AS DATE),103) AS TODATE," +
                                            " B.CLAIM_TYPE FROM M_CLAIM_NARRATION A LEFT OUTER JOIN M_CLAIM_TYPE B ON A.CLAIM_TYPE=B.CLAIM_ID WHERE USERTAG='" + USERTAG + "' AND A.FINYEAR='" + FINYEAR + "' ORDER BY CLAIM_TYPE,BSNAME ";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindexportNarration(string USERTAG,  string FINYEAR)
        {
            string sql = string.Empty;
           
            
                sql = " SELECT NARRATIONID, CLAIM_NARRATION,BSNAME, CONVERT(VARCHAR(10),CAST(FROM_DATE AS DATE),103) AS FROMDATE , CONVERT(VARCHAR(10),CAST(TO_DATE AS DATE),103) AS TODATE " +
                             " FROM M_EXPORT_CLAIM_NARRATION  WHERE USERTAG='" + USERTAG + "'  AND FINYEAR='" + FINYEAR + "'";
           
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet EditNarration(string ID)
        {
            string sql = "EXEC USP_EDIT_CLAIM_NARRATION '" + ID + "'";

            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }


        public DataSet EditEXPORTNarration(string ID)
        {
            string sql = "EXEC USP_EDIT_EXPORT_CLAIM_NARRATION '" + ID + "'";

            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }


        public DataTable BindDamageClaim_Filtering(string claimtype, string userid, string depotid, string status, string fromdate, string todate, string party)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_CLAIM_LOAD_DETAILS_FILTERING] '" + claimtype + "','" + userid + "','" + depotid + "','" + status + "','" + fromdate + "','" + todate + "','" + party + "'";

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindExportClaim_Filtering(string claimtype, string userid, string depotid, string status, string fromdate, string todate, string party)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            sql = "[USP_EXPORT_CLAIM_LOAD_DETAILS_FILTERING] '" + claimtype + "','" + userid + "','" + depotid + "','" + status + "','" + fromdate + "','" + todate + "','" + party + "'";

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistributorClaim(string customerid)
        {
            string sql = " SELECT DISTINCT CUSTOMERID,CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID IN (SELECT * FROM DBO.SPLIT( '" + customerid + "',',')) ORDER BY CUSTOMERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindClaimTypeforNarration()
        {
            string sql = "SELECT CLAIM_ID,CLAIM_TYPE FROM M_CLAIM_TYPE ORDER BY CLAIM_TYPE";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUserForRejection(string ClaimID)
        {
            string sql = " SELECT DISTINCT A.NEXT_LEVEL_APPROVAL_UID,B.USERNAME FROM CL_DAMAGE_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.NEXT_LEVEL_APPROVAL_UID = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " UNION ALL" +
                         " SELECT DISTINCT A.CBU,B.USERNAME FROM CL_DAMAGE_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.CBU = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " ORDER BY B.USERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUserForRejection_QVPS(string ClaimID)
        {
            string sql = " SELECT DISTINCT A.NEXT_LEVEL_APPROVAL_UID,B.USERNAME FROM CL_QVPS_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.NEXT_LEVEL_APPROVAL_UID = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " UNION ALL" +
                         " SELECT DISTINCT A.CBU,B.USERNAME FROM CL_QVPS_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.CBU = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " ORDER BY B.USERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUserForRejection_Dispaly(string ClaimID)
        {
            string sql = " SELECT DISTINCT A.NEXT_LEVEL_APPROVAL_UID,B.USERNAME FROM CL_DISPLAY_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.NEXT_LEVEL_APPROVAL_UID = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " UNION ALL" +
                         " SELECT DISTINCT A.CBU,B.USERNAME FROM CL_DISPLAY_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.CBU = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " ORDER BY B.USERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindUserForRejection_EXPORT(string ClaimID)
        {
            string sql = " SELECT DISTINCT A.NEXT_LEVEL_APPROVAL_UID,B.USERNAME FROM CL_EXPORT_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.NEXT_LEVEL_APPROVAL_UID = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " UNION ALL" +
                         " SELECT DISTINCT A.CBU,B.USERNAME FROM CL_EXPORT_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.CBU = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " ORDER BY B.USERNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUserForRejection_Margin(string ClaimID)
        {
            string sql = " SELECT DISTINCT A.NEXT_LEVEL_APPROVAL_UID,B.USERNAME FROM CL_SUPERSTOCKIST_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.NEXT_LEVEL_APPROVAL_UID = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " UNION ALL" +
                         " SELECT DISTINCT A.CBU,B.USERNAME FROM CL_SUPERSTOCKIST_CLAIM_HEADER_HISTORY A INNER JOIN M_USER B  ON A.CBU = B.USERID  WHERE A.ID ='" + ClaimID + "'" +
                         " ORDER BY B.USERNAME";

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
        public DataTable BindDATE_FROM_JC(string jcid)
        {
            string sql = " [USP_FETCH_DATERANGE_FROM_JC] '" + jcid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSuperStockistByDepot(string DepotID, string BSID)
        {
            string sql = " SELECT DISTINCT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID = B.CUSTOMERID " +
                         " WHERE A.DEPOTID='" + DepotID + "' AND B.CUSTYPE_ID=(SELECT SUPERSTOCKISTSID FROM P_APPMASTER) ORDER BY B.CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable ProcessMarginClaim(string bsid, string bsname, string grid, string grname, string distid, string distname, string TypeID,
                                       string UserID, string FinYear, string Remarks, string depotid, string depotname, string claimdate, string frmdate, string todate, string narration) /* ldtom 28/01/2019 */
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_SUPERSTOCKIST_CLAIM_PROCESS '" + bsid + "','" + bsname + "','" + grid + "'," +
                                                "'" + grname + "','" + distid + "','" + distname + "'," +
                                                "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + Remarks + "','" + depotid + "','" + depotname + "','" + claimdate + "','" + frmdate + "','" + todate + "','" + narration + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SaveMarginClaim_SuperStockist(string ClaimID, string Mode, string bsid, string bsname, string grid, string grname, string distid, string distname, string TypeID,
                                       string UserID, string FinYear, string Remarks, decimal CLAIM_AMT, string depotid, string depotname, string claimdate, string xml)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "EXEC USP_MARGIN_CLAIM_INSERT_SUPERSTOCKIST '" + ClaimID + "','" + Mode + "','" + bsid + "','" + bsname + "','" + grid + "'," +
                                                "'" + grname + "','" + distid + "','" + distname + "'," +
                                                "'" + TypeID + "','" + UserID + "','" + FinYear + "','" + Remarks + "','" + CLAIM_AMT + "','" + depotid + "','" + depotname + "','" + claimdate + "','" + xml + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string FetchLedgerID(string ClaimID)
        {
            string sql = "SELECT AccEntryID FROM Acc_Voucher_Invoice_Map WHERE InvoiceID='" + ClaimID + "'";
            string value = string.Empty;
            value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        public DataTable BindJC(string finyear)
        {
            string sql = " SELECT JCID,NAME FROM M_JC  WHERE FINYEAR='" + finyear + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetLoginBS(string USERID)
        {
            string sql = "SELECT DISTINCT BSID AS ID,BSNAME AS NAME FROM M_USER_BS_MAP where USERID='" + USERID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepot_Narration()
        {
            string sql = "SELECT BRID AS ID,BRPREFIX AS NAME FROM M_BRANCH WHERE BRANCHTAG='D' ORDER BY BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Save_ClaimBulkUpload(string uploaddate, string depotid, string depotnm, string finyear, string userid, string xmlclaimdetais)
        {
            string sql = "EXEC USP_CLAIM_BULK_UPLOAD '" + uploaddate + "','" + depotid + "','" + depotnm + "','" + finyear + "','" + userid + "','" + xmlclaimdetais + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindClaimNarration(string CLAIM_TYPE, string USERTAG, string BSID, string DEPOTID, string GRID)
        {
            string sql = "EXEC [USP_GET_NARRATION] '" + CLAIM_TYPE + "','" + USERTAG + "' ,'" + BSID + "','" + DEPOTID + "','" + GRID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchLedgerID(string ClaimID, string CallerType)
        {
            string sql = "SELECT AccEntryID, InvoiceID FROM Acc_Voucher_Invoice_Map WHERE InvoiceID IN (" + ClaimID + ")";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSuperStockiestClaim(string depotid) /* only super stockiest */
        {
            string sql = " SELECT DISTINCT A.CUSTOMERID,B.CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID " +
                         " WHERE DEPOTID IN(SELECT * FROM DBO.SPLIT('"+ depotid + "', ','))  AND B.CUSTYPE_ID = '826D656F-353F-430F-8966-4FE6BE3F67ED' ORDER BY CUSTOMERNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSettingValue() /* added on 22012019 */
        {
            string sql = "SELECT '0' AS SETTINGVALUE UNION ALL SELECT DISTINCT SETTINGVALUE FROM  M_SUPERSTOCKIST_CLAIM_PERCENTAGE_EXCEPTION ORDER BY SETTINGVALUE ASC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindSettingValueDetails(string settingvalue)  /* added on 22012019 */
        {
            string sql = "EXEC[USP_FETCH_SUPERSTOCKIST_CLAIM_PERCENTAGE_FOR_SETTINGVALUE]  '" + settingvalue + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;

        }

        public DataTable Bindmobilenoforclaim(string mode,string CnNO)
        {
            string sql = "EXEC [USP_GET_SMS_MOBILENO_CLAIM] '"+ mode + "','"+ CnNO + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SaveApprovedQtyClaim(string claimid,string xml)
        {
            string sql = "EXEC [USP_GET_GIFT_QTY_UPDATE] '"+ claimid + "','" + xml + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Giftclaimdetails(string ClaimId)  /* added on 22012019 */
        {
            string sql = "EXEC[USP_FATCH_GIFT_CLAIM_DETAILS]  '" + ClaimId + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;

        }

    }
}
