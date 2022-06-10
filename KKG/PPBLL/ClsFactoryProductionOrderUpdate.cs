using DAL;
using System;
using System.Data;
using System.Web;

namespace PPBLL
{
    public class ClsFactoryProductionOrderUpdate
    {
        DBUtils db = new DBUtils();

        #region Select    
        public DataTable BindProctuctionOrder(string DepotID, string Finyear)
        {
            string sql = "EXEC USP_BIND_PROCTUCTIONORDER '" + DepotID + "','" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindEndProctuctionOrder(string DepotID, string Finyear)
        {
            string sql = "EXEC USP_BIND_PROCTUCTIONORDER_END '" + DepotID + "','" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProcess(string PRODUCTION_ORDERID)
        {
            string sql = "EXEC USP_BIND_PROCESS_FROM_PRODUCTION '" + PRODUCTION_ORDERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string ProductionOrderID, string DepotID)
        {
            string sql = "EXEC USP_BIND_PRODUCT_FROM_PRODUCTION '" + ProductionOrderID + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductOutPut(string ProductionOrderID, string DepotID)
        {
            string sql = "EXEC USP_BIND_PRODUCTOUTPUT_FROM_PRODUCTION '" + ProductionOrderID + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreReturn(string FinYear, string DepotID)
        {
            string sql = "EXEC USP_BIND_STORE_RETURN '" + FinYear + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindWestageDtls(string ProductionID, decimal WastageQty)
        {
            string sql = "EXEC USP_STORE_RETURN_DETAILS '" + ProductionID + "'," + WastageQty + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public void StoreReturnApprove(string ProductionID)
        {
            string sql = "UPDATE T_MM_RETURN_HEADER SET STOREAPPROVE='Y' WHERE PRODUCTIONORDER_ID='" + ProductionID + "'";
            db.HandleData(sql);
        }
        #endregion

        public void InSertNotification(string productionOrderID, string ProcessID, string Action, string Remarks)
        {
            string sl = "INSERT INTO [T_PRODUCTION_NOTIFICATION] ([PRODUCTIONORDER_ID],[PEOCESSID],[PROCESSSTATE],[NOTIFICATIONDATETINE],[REMARKS]) VALUES( '" +
                productionOrderID + "','" + ProcessID + "','" + Action + "',GETDATE(),'" + Remarks + "')";
            db.HandleData(sl);
        }
        public string InsertUpdateDelete(string XML, string productionorderid, string DepotID, string FinYear)
        {
            string MRP_ID = string.Empty;
            string MRPNo = string.Empty;
            string userId = HttpContext.Current.Session["USERID"].ToString();

            try
            {
                string sqlMRP = " EXEC USP_REQUSITION_INSERT '" + XML + "','" + productionorderid + "','" + DepotID + "','" + FinYear + "','"+ userId + "'";
                DataTable dtMRP = db.GetData(sqlMRP);

                if (dtMRP.Rows.Count > 0)
                {
                    MRP_ID = dtMRP.Rows[0]["prorder_ID"].ToString();
                    MRPNo = dtMRP.Rows[0]["PRO_NO"].ToString();
                }
                else
                {
                    MRPNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }

            return MRPNo;
        }
        public string InsertUpdateDeleteRETURN(string XML, string NotificationXML, string XmlReturn, string ProductionUpdateID, string ProcessID, string Finyear, string Mode)
        {
            string MRP_ID = string.Empty;
            string MRPNo = string.Empty;
            try
            {
                string sqlMRP = " EXEC USP_RETURN_INSERT '" + XML + "','" + NotificationXML + "','" + ProductionUpdateID + "','" + ProcessID + "','" + XmlReturn + "','" + Finyear + "','" + Mode + "'";
                DataTable dtMRP = db.GetData(sqlMRP);

                if (dtMRP.Rows.Count > 0)
                {
                    MRP_ID = dtMRP.Rows[0]["prorder_ID"].ToString();
                    MRPNo = dtMRP.Rows[0]["PRO_NO"].ToString();
                }
                else
                {
                    MRPNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return MRPNo;
        }
        public DataTable BindLightBox_IssueDetails(string BATCHID, string ProcessID, decimal WastageQty)
        {
            string sql = "EXEC USP_REQISITION_ISSUE_DETAILS '" + BATCHID + "','" + ProcessID + "'," + WastageQty + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region Check BatchNo From Requestion Table (Process Start)
        public string BatchNo(string ProductionOrderID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_REQUISITION_HEADER] WHERE PRODUCTIONORDER_ID='" + ProductionOrderID + "' ) BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        #region Check ChkIssueQC From Issue_Header Table 
        public string ChkIssueQC(string ProductionOrderID)
        {
            string Sql = " IF EXISTS(SELECT * FROM T_REQUISITION_HEADER AS A " +
                         " INNER JOIN T_ISSUE_REQUISITION_MAP AS B ON B.REQUISITIONID=A.REQUISITIONID " +
                         " INNER JOIN T_ISSUE_HEADER AS C ON C.ISSUEID=B.ISSUEID " +
                         " WHERE C.ISVERIFY='Y' AND A.PRODUCTIONORDER_ID='" + ProductionOrderID + "') BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        #region fetch Batchno
        public DataTable FetchBatch(string PRODUCTION_ORDERID)
        {
            string sql = "EXEC USP_MFG_UPDATE '" + PRODUCTION_ORDERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Check SFG Pending Process
        public string SFGPending(string ProductionOrderID)
        {
            string Sql = "EXEC USP_CHECKSFG_PROCESSEND'" + ProductionOrderID + "' ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        #region Bind Production Qty
        public DataTable BindProductionQty(string ProductionOrderID, string DepotID)
        {
            DataTable dt = new DataTable();
            string Sql = "EXEC USP_BIND_PRODUCTION_QTY'" + ProductionOrderID + "','" + DepotID + "' ";
            dt = db.GetData(Sql);
            return dt;
        }
        #endregion
    }
}
