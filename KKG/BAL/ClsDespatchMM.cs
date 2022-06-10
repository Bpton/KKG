using DAL;
using System.Data;

namespace BAL
{
    public class ClsDespatchMM
    {
        DBUtils db = new DBUtils();

        public DataTable BindTPU_Transporter(string FactoryID)
        {
            string sql = " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                         " M_TRANSPOTER_TPU_MAP AS B ON" +
                         " A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         " AND  B.TPUID='" + FactoryID + "'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindVendor()
        {
            string sql = "SELECT VENDORID, VENDORNAME ,SUPLIEDITEMID FROM M_TPU_VENDOR WHERE SUPLIEDITEMID<>1 AND TAG='T'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindVendorBYentrywise(string DepotId)
        {
            string sql = "SELECT DISTINCT VENDORID, VENDORNAME  FROM T_MM_STOCKDESPATCH_HEADER WHERE FACTORYID='" + DepotId + "' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDespatch(string fromdate, string todate, string finyear, string FactoryID, string Checker)
        {
            string sql = string.Empty;
            sql = "EXEC BIND_JOBWORK_OUTWARD '" + fromdate + "','" + todate + "','" + finyear + "','" + FactoryID + "','" + Checker + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindWorkOrder(string VendorID, string DespatchID)
        {
            string sql = "EXEC USP_ORDERNO_MM '" + VendorID + "','" + DespatchID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindIssueDetails(string OrderId, string IssueID)
        {
            string sql = "EXEC USP_WORKORDER_DETAILS '" + OrderId + "','" + IssueID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public int DeleteDespatch(string DespatchID)
        {
            int delflag = 0;

            string sqldeleteStockDespatch = "EXEC [USP_DESPATCH_DELETE_MM] '" + DespatchID + "'";
            int e = db.HandleData(sqldeleteStockDespatch);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else if (e == -1)
            {
                delflag =-1;  // issue done
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }
        public DataSet EditDespatchDetails(string DespatchID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_DESPATCH_DETAILS_MM '" + DespatchID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public string InsertDespatchDetails(string despatchDate, string Vedorid, string VendorName, string wayBillNo, string TransporterID, string Vehicle,
                                            string FactoryId, string FactoryName, string LRGRNo, string LRGRDate, string TransportMode, string userID,
                                            string finyear, string Remarks, string Gatepassno, string gatepassdate, string xml, string hdnvalue,
                                            decimal TotalDespatchValue, decimal Roundoff, string IssueId, string IssueDtls, string InvoiceNo, string InvoiceDate, string PartyId,string taxDetails,decimal taxamnt,decimal finalamnt)
        {
            string flag = "";
            string DespatchNo = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            string sqlprocDespatch = " EXEC [USP_DESPATCH_INSERT_UPDATE_MM] '" + hdnvalue + "','" + flag + "','" + despatchDate + "','" + FactoryId + "','" + FactoryName + "','" + wayBillNo + "'," +
                                     " '" + TransporterID + "','" + Vehicle + "','" + Vedorid + "','" + VendorName + "','" + LRGRNo + "','" + LRGRDate + "'," +
                                     " '" + TransportMode + "','" + Gatepassno + "','" + gatepassdate + "','" + userID + "','" + finyear + "','" + Remarks + "'," +
                                     " " + TotalDespatchValue + "," + Roundoff + ",'" + xml + "','" + IssueId + "','" + IssueDtls + "','" + InvoiceNo + "','" + InvoiceDate + "','" + PartyId + "','"+taxDetails+"','"+ taxamnt + "','"+ finalamnt + "'";
            DataTable dtDespatch = db.GetData(sqlprocDespatch);

            if (dtDespatch.Rows.Count > 0)
            {
                DespatchNo = dtDespatch.Rows[0]["DESPATCHNO"].ToString();
            }
            else
            {
                DespatchNo = "";
            }
            return DespatchNo;
        }
        public int JobWorkApprove(string DESPATCHID)
        {
            int Approveflag = 0;
            string sql = " UPDATE T_MM_STOCKDESPATCH_HEADER SET ISVERIFIED = 'Y' Where STOCKDESPATCHID='" + DESPATCHID + "'";
            int e = db.HandleData(sql);

            if (e == 0)
            {
                Approveflag = 0;  // delete unsuccessfull
            }
            else
            {
                Approveflag = 1;  // delete successfull
            }
            return Approveflag;
        }
        public string GetPOStatus(string POID)
        {
            string Sql = " IF EXISTS(SELECT * FROM [T_STOCKRECEIVED_DETAILS] WHERE POID='" + POID + "') BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN  SELECT '0' END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public DataTable BindIssueNo(string WorkOrderID)
        {
            string sql = "EXEC USP_BINDISSUE_WORKORDER_WISE '" + WorkOrderID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        
        public DataTable bindTaxCount(string vendorid,string deptid)
        {
            string sql = "EXEC USP_BINDISSUE_WORKORDER_WISE '" + vendorid + "','" + deptid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetTaxPercentage(string TaxID, string CustomerID, string ProductID, string Date)
        {
            decimal Taxpercent = 0;

            string SQLFunction = "EXEC USP_GETTAXPERCENTAGE '" + TaxID + "','" + CustomerID + "','" + ProductID + "','" + Date + "'";
            Taxpercent = (decimal)db.GetSingleValue(SQLFunction);
            return Taxpercent;
        }

        /*NEW ADD 30-04-2021*/
        public DataTable bindTaxMenuWise(string MODE,string MENUID,string date)
        {
            string sql = "EXEC USP_GET_TAX_INFO '" + MODE + "','"+ MENUID + "','"+ date + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
