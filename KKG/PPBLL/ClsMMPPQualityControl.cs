using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsMMPPQualityControl
    {
        DBUtils db = new DBUtils();

        public DataTable BindQualityControl(string fromdate, string todate, string UserID, string finyear, string CHECKER)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_BINDQUALITYCONTROL '" + fromdate + "','" + todate + "','" + UserID + "','" + finyear + "','" + CHECKER + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGRNNo(string VENDORID, string MODE)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_BINDGRNNO '" + VENDORID + "','" + MODE + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGRNProductDetails(string STOCKRECEIVEDID)
        {
            string sql = "EXEC [USP_BIND_GRNPRODUCTDETAILS] '" + STOCKRECEIVEDID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataSet EditQcDetails(string QCID)
        {
            string sql = "EXEC USP_MM_QC_DETAILS '" + QCID + "'";
            DataSet ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindTpuVendor()
        {
            string sql = " SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR" +
                         " WHERE SUPLIEDITEMID NOT IN ('1','4')" +
                         " AND TAG='T'" +
                         " ORDER BY VENDORNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReason(string MenuID)
        {
            string sql = " SELECT DISTINCT A.ID,A.NAME AS DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N' AND B.MENUID='" + MenuID + "' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string SAVEMMQC(string QCID, string QCDATE, string ENTRYDATE, string SAMPLEDATE, string REFNO, string GRNNUMBER, string GRNID, string GRNDATE, string VENDORID, string VENDORNAME, int CREATEDBY, string FINYEAR, string FLAG, string REMARKS, string RATING, string MODULEID, string XMLQCDetails, string XMLuploadFILE, string XMLUploadCOA, string fileuploadtagCOA, string fileuploadtag)
        {
            string qcID = string.Empty;
            string qcNo = string.Empty;
            string sqlstr;
            try
            {
                sqlstr = "EXEC USP_MM_QUALITYCONTROLHEADER '" + QCID + "','" + QCDATE + "','" + ENTRYDATE + "','" + SAMPLEDATE + "','" + REFNO + "','" + GRNNUMBER + "'," +
                         "'" + GRNID + "','" + GRNDATE + "','" + VENDORID + "','" + VENDORNAME + "'," + CREATEDBY + ",'" + FINYEAR + "','" + FLAG + "','" + REMARKS + "'," +
                         "'" + RATING + "','" + MODULEID + "','" + XMLQCDetails + "','" + XMLuploadFILE + "','" + XMLUploadCOA + "','" + fileuploadtagCOA + "','" + fileuploadtag + "'";
                DataTable dtQC = db.GetData(sqlstr);
                if (dtQC.Rows.Count > 0)
                {
                    qcID = dtQC.Rows[0]["QCID"].ToString();
                    qcNo = dtQC.Rows[0]["QCNO"].ToString();
                }
                else
                {
                    qcNo = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return qcNo;
        }
        public int DeleteQC(string QCID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC USP_MM_QC_DELETE '" + QCID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int approveQC(string QCID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE T_MM_QUALITYCONTROL_HEADER SET ISVERIFIED='Y',VERIFIEDDATETIME=GETDATE() WHERE QCID='" + QCID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int REJECTQC(string qcid, string userid) /*new added by p.basu for rejection on 11/12/2019*/
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE T_MM_QUALITYCONTROL_HEADER SET ISVERIFIED='R',REJECTEDBY ='" + userid + "',VERIFIEDDATETIME=GETDATE() WHERE QCID='" + qcid + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public DataTable GetFile(string id)
        {
            string sql = "SELECT FILENAME AS FILENAME FROM [T_MM_QUALITYCONTROL_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetFile_COA(string id)
        {
            string sql = "SELECT FILENAME AS FILENAME FROM [T_MM_QUALITYCONTROL_COA_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetPoFile(string poid)/*added on 25112019 by p.basu*/
        {
            string sql = "SELECT UPLOADFILENAME AS FILENAME FROM T_MM_POQUOTATION WHERE uploadforid='1' and POID='" + poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetPoFileCom(string poid)/*added on 25112019 by p.basu*/
        {
            string sql = "SELECT UPLOADFILENAME as FILENAME FROM T_MM_POQUOTATION WHERE uploadforid='2' and POID='" + poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FileDelete(string id)
        {

            string sql = "DELETE FROM [T_MM_QUALITYCONTROL_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FileDelete_COA(string id)
        {
            string sql = "DELETE FROM [T_MM_QUALITYCONTROL_COA_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region QC DETAILS
        public DataTable FetchQCDetails(string fromdate, string todate, string depotid, string finyear)
        {
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_QCDETAILS '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PendingQC(string DepotID, string finyear, string mode)
        {
            string sql = string.Empty;
            sql = "EXEC [USP_RPT_FACTORY_PENDING_STATUS]'" + DepotID + "','" + finyear + "','" + mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchPendingQCDetails(string DepotID)
        {
            string sql = string.Empty;
            sql = " SELECT A.STOCKRECEIVEDID AS STOCKRECEIVEDID,C.VENDORNAME,(A.STOCKRECEIVEDPREFIX + '/' + A.STOCKRECEIVEDNO + '' + A.STOCKRECEIVEDSUFFIX) AS STOCKRECEIVEDNO," +
                  " CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS STOCKRECEIVEDDATE,SUM(Z.RECEIVEDQTY) AS RECEIVEDQTY" +
                  " FROM T_STOCKRECEIVED_HEADER			 AS A WITH (NOLOCK)" +
                  " INNER JOIN T_STOCKRECEIVED_DETAILS	 AS Z ON Z.STOCKRECEIVEDID=A.STOCKRECEIVEDID" +
                  " INNER JOIN M_TPU_VENDOR				 AS C WITH (NOLOCK) ON A.TPUID=C.VENDORID" +
                  " WHERE A.MOTHERDEPOTID='" + DepotID + "'AND A.GRN='Y' AND C.SUPLIEDITEMID <> '1' AND A.ISVERIFIEDSTOCKIN <> 'Y'" +
                  " GROUP BY A.STOCKRECEIVEDID,C.VENDORNAME,A.STOCKRECEIVEDPREFIX,A.STOCKRECEIVEDNO,A.STOCKRECEIVEDSUFFIX,A.STOCKRECEIVEDDATE,A.CREATEDDATE" +
                  " ORDER BY A.CREATEDDATE DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion
        public DataTable DeleteUploadFile(string fileid)/*added on 25112019 by p.basu*/
        {
            DataTable dt = new DataTable();
            string sql = "USP_DELETE_PO_FILE '" + fileid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetPurchaseorderFile(string poid)/*added on 25112019 by p.basu*/
        {
            string sql = "SELECT FILENAME FROM T_MM_PURCHASEORDER_UPLOADFILES WHERE  POID='" + poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
