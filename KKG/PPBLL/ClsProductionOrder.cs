using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsProductionOrder
    {
        DBUtils db = new DBUtils();

        #region Select
        public DataSet FetchMRPRecord(string MRPID, string ProductID, string FactoryID, string ProcessFrameworkID, string ProcessID, string FromDate, string ToDate, string Flag)
        {
            string sql = "EXEC SP_MRP_RECORD '" + MRPID + "','" + ProductID + "','" + FactoryID + "','" + ProcessFrameworkID + "','" + ProcessID + "','" + FromDate + "','" + ToDate + "','" + Flag + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        public DataSet FetchRecordWithOutMrp(string ProcessID, string ItemID, string MRP_ID, string WorkStationID)
        {
           
            string sql = "EXEC USP_BIND_RECORD_WITHOUTMRP '" + ProcessID + "','" + ItemID + "','" + MRP_ID + "','" + WorkStationID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable BindBOM(string processID, decimal qty, string Product, string FrameworkID, string FactoryID/*, string AsonDate*/)
        {
            string userid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC USP_GETBOM'" + processID + "'," + qty.ToString() + ",'" + Product + "','" + FrameworkID + "','" + FactoryID + "','" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepartMent(string processID, decimal qty, string Product, string FrameworkID, string FactoryID/*NEW ADD FOR DEPARTMENT*/)
        {
            string userid = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC USP_GETDEPARTMENT'" + processID + "'," + qty.ToString() + ",'" + Product + "','" + FrameworkID + "','" + FactoryID + "','" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatch(string MRPID)
        {
            string sql = " SELECT [BatchNO] FROM [T_PRODUCTION_ORDER_HEADER]  where MRPID='" + MRPID + "' AND ISCLOSED<>'Y' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //Add By Rajeev 23-08-2017
        public DataTable BindBatchWithOutMRP(string Productid, string DepotID)
        {           
            string sql = "EXEC USP_BIND_BATCH '" + Productid + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet FetchMRPRecord(string MRPID, string ProductID, string FactoryID, string ProcessFrameworkID, string ProcessID, string FromDate, string ToDate, string Flag, string ItemType)
        {
            string sql = "EXEC SP_MRP_RECORD '" + MRPID + "','" + ProductID + "','" + FactoryID + "','" + ProcessFrameworkID + "','" + ProcessID + "','" + FromDate + "','" + ToDate + "','" + Flag + "','" + ItemType + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet FetchWITHOUTMRPRecord(string MRPID, string ProductID, string FactoryID, string ProcessFrameworkID, string ProcessID, string FromDate, string ToDate, string Flag, string ItemType)
        {
            string sql = "EXEC SP_WITHOUTMRP_RECORD '" + MRPID + "','" + ProductID + "','" + FactoryID + "','" + ProcessFrameworkID + "','" + ProcessID + "','" + FromDate + "','" + ToDate + "','" + Flag + "','" + ItemType + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }       
        public DataSet FetchFactoryWithoutMRP()
        {
            string sql = " SELECT VENDORID AS FACTORYID,VENDORNAME AS VENDORNAME" +
                         " FROM M_TPU_VENDOR" +
                         " WHERE TAG= 'F'" +
                         " ORDER BY VENDORNAME DESC";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet FetchWorkStation(string ProductID)
        {
            string mode = "PROCESS";
            string sql = "USP_GET_PRODUCTION_ORDER_DETAILS_MODE_WISE '"+ mode + "','"+ ProductID + "' ";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public DataSet FetchWorkStation(string mode ,string ProductID)
        {
           
            string sql = "USP_GET_PRODUCTION_ORDER_DETAILS_MODE_WISE '"+ mode + "','"+ ProductID + "' ";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
       
        public DataSet FetchFrameWork(string ProductID)
        {
            string mode = "FRAMEWORK";
            string sql = "USP_GET_PRODUCTION_ORDER_DETAILS_MODE_WISE '"+ mode + "','"+ ProductID + "' ";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        /*Added By Avishek Ghosh on 19-08-2017*/
        public DataTable BindProductionOrderGridWithoutMRP(string FromDt, string ToDt, string ProductId, string DepotId)
        {
            string USERID = HttpContext.Current.Session["USERID"].ToString();
            string sql = "EXEC USP_FETCH_PRODUCTION '" + FromDt + "','" + ToDt + "','" + ProductId + "','" + DepotId + "','"+ USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string GenerateBatchNo(string Product, string FINYR, string EntryDt)
        {
            string returnvalue = "";
            string sql = "";
            string PRO_ID = "";
            string PRO_NUM = "", PRVBATCHNO = "";
            DateTime EnteredDate = DateTime.Parse(EntryDt);
            DateTime StartDt = DateTime.Parse("01-01-2018");
            string Year = EnteredDate.Year.ToString();

            if (EnteredDate < StartDt)
            {
                sql = "SELECT CODE FROM M_PRODUCT WHERE ID='" + Product + "'";
                DataTable dtPROrder = db.GetData(sql);

                if (dtPROrder.Rows.Count > 0)
                {
                    PRO_ID = dtPROrder.Rows[0]["CODE"].ToString();
                }
                //sql = "SELECT  DBO.[LPAD](ISNULL(COUNT(*),1) ,4,'0') AS CODE FROM T_PRODUCTION_ORDER_HEADER WHERE ProductID='" + Product + "'";
                sql = "EXEC GENERATE_BATCHNO'" + Product + "','" + EntryDt + "'";

                dtPROrder = db.GetData(sql);
                if (dtPROrder.Rows.Count > 0)
                {
                    PRO_NUM = dtPROrder.Rows[0]["CODE"].ToString();
                    PRVBATCHNO = dtPROrder.Rows[0]["PREVIOUSBATCH"].ToString();
                }
                returnvalue = PRO_ID + "/" + PRO_NUM + "/" + FINYR + "~" + PRO_ID + "/" + PRVBATCHNO + "/" + FINYR;
                return returnvalue;
            }
            else
            {
                sql = "SELECT CODE FROM M_PRODUCT WHERE ID='" + Product + "'";
                DataTable dtPROrder = db.GetData(sql);

                if (dtPROrder.Rows.Count > 0)
                {
                    PRO_ID = dtPROrder.Rows[0]["CODE"].ToString();
                }
                sql = "EXEC GENERATE_BATCHNO'" + Product + "','" + EntryDt + "'";
                dtPROrder = db.GetData(sql);
                if (dtPROrder.Rows.Count > 0)
                {
                    PRO_NUM = dtPROrder.Rows[0]["CODE"].ToString();
                    PRVBATCHNO = dtPROrder.Rows[0]["PREVIOUSBATCH"].ToString();
                }
                if (PRVBATCHNO == "")
                {
                    returnvalue = PRO_ID + "/" + PRO_NUM + "/" + Year + "~" + PRVBATCHNO;
                }
                else
                {
                    returnvalue = PRO_ID + "/" + PRO_NUM + "/" + Year + "~" + PRO_ID + "/" + PRVBATCHNO + "/" + Year;
                }
                return returnvalue;
            }
        }

        #region Insert,Update and Delete
        public string InsertUpdateDeleteProductionOrder(string PROID, string FINYear, string PROD_STARTIME, string PROD_ENDTIME, string FROMDATE,
                                                        string TODATE, string CreatedBy, string ModifiedBy, string BatchNO, string ProductID, 
                                                        string FactoryID, string QTY, string PRODORDERBOM, string PRODORDERRequisition, string Flag, 
                                                        string MRPID, string ProcessID, string PlantID, string FrameworkID, string IsCancelled, 
                                                        string EntryDt,string MixBatchNo,string planningNumber,decimal planningqty)
        {
            string PRO_ID = string.Empty;
            string PRONo = string.Empty;

            try
            {
                string sqlPROrder = " EXEC SP_Production_Order_INSERT_UPDATE_DELETE '" + PROID + "','" + MRPID + "','" + FINYear + "','" + PROD_STARTIME + "','" + PROD_ENDTIME + "','" + FROMDATE + "','" + TODATE + "'," +
                                    " '" + CreatedBy + "','" + ModifiedBy + "','" + BatchNO + "','" + ProductID + "','" + FactoryID + "','" + QTY + "','" + PRODORDERBOM + "'," +
                                    " '" + PRODORDERRequisition + "','" + Flag + "','" + ProcessID + "','" + PlantID + "','" + FrameworkID + "','" + IsCancelled + "','" + EntryDt + "','" + MixBatchNo + "','"+ planningNumber + "',"+ planningqty + "";
                DataTable dtPROrder = db.GetData(sqlPROrder);

                if (dtPROrder.Rows.Count > 0)
                {
                    PRO_ID = dtPROrder.Rows[0]["PRORDER_ID"].ToString();
                    PRONo = dtPROrder.Rows[0]["PRO_NO"].ToString();
                }
                else
                {
                    PRONo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }

            return PRONo;
        }
        #endregion

        #region Check Production_Order_ID From Requestion Table (Process Start)
        public string ProductionOrderID(string ProductionOrderID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_REQUISITION_HEADER] WHERE PRODUCTIONORDER_ID='" + ProductionOrderID + "' ) BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion
        public DataTable ChkBulkBalanceQty(string PRODUCTION_ORDERID, string BATCHNO, string PROCESSFRAMEWORKID, string PROCESSID, decimal QTY, string TYPE)
        {
            string sql = "EXEC BATCH_VALIDATION_QTY'" + PRODUCTION_ORDERID + "','" + BATCHNO + "','" + PROCESSFRAMEWORKID + "','" + PROCESSID + "'," + QTY + ",'" + TYPE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ChkMultiBulkBalanceQty(string PRODUCTION_ORDERID, string BATCHNO, string PROCESSFRAMEWORKID, string PROCESSID, decimal QTY, string TYPE)
        {
            string sql = "EXEC MULTIBATCH_VALIDATION_QTY'" + PRODUCTION_ORDERID + "','" + BATCHNO + "','" + PROCESSFRAMEWORKID + "','" + PROCESSID + "'," + QTY + ",'" + TYPE + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }        
        /*Added By Avishek On 19-08-2017*/
        public DataSet EditProductionOrderWithoutMRP(string ProductionOrderID, string FactoryID)
        {
            String USERID = HttpContext.Current.Session["USERID"].ToString();/*NEW ADD FOR OWN STOCK */
            string sql = "EXEC SP_EDITFETCH_PRODUCTIONORDER_WITHOUT_MRP '" + ProductionOrderID + "','" + FactoryID + "','"+USERID+"'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "HEADER";
            return ds;
        }

        #region Check Production Order From T_PRODUCTIONORDER_NOTIFICATION_ITEM Table.
        public string ProductionOrder(string ProductionOrderID) /*logic change*/
        {
            string Sql = " if exists( select 1 from T_ISSUE_REQUISITION_MAP where REQUISITIONID in ( select distinct REQUISITIONID from T_REQUISITION_HEADER where PRODUCTIONORDER_ID='"+ ProductionOrderID + "' )) BEGIN SELECT '1'   END ELSE    BEGIN SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public string StatusCheck(string mode,string ProductionOrderID,string userid,string finyear) /*new add  by p.basu for status check */
        {
            string Sql = "select dbo.Check_process_status('"+ mode + "','"+ ProductionOrderID + "')";
            return ((string)db.GetSingleValue(Sql));
        }
        public string DeleteProductionOrder(string ProductionOrderID,string userid,string finyear) /*new add  by p.basu for status check */
        {
            string Sql = "exec usp_delete_production_Order '"+ ProductionOrderID + "','"+ userid + "','"+ finyear + "'";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        #region Bind ProductName From Production.
        public DataTable BindProductName(string DepotID)
        {
            DataTable dtProduct = new DataTable();
            string Sql = " SELECT DISTINCT B.ID,B.PRODUCTALIAS FROM T_PRODUCTION_ORDER_HEADER AS A " +
                         " INNER JOIN M_PRODUCT AS B ON A.ProductID=B.ID " +
                         " INNER JOIN M_PRODUCT_TPU_MAP AS C ON C.PRODUCTID=A.PRODUCTID" +
                         " WHERE C.VENDORID= '" + DepotID + "'" +
                         " ORDER BY B.PRODUCTALIAS ASC ";

            dtProduct = db.GetData(Sql);
            return dtProduct;
        }
        #endregion

        #region Bind Bulk ProductName From Framework.
        public DataTable BindBulkProduct(string FrameWorkID)
        {
            DataTable dtProduct = new DataTable();
            string Sql = " SELECT B.ID,B.PRODUCTALIAS AS NAME FROM T_PROCESSFRAMEWORK_MATERIAL AS A " +
                         " INNER JOIN M_PRODUCT AS B ON A.ITEMID=B.ID " +
                         " WHERE A.PROCESSFRAMEWORKID= '" + FrameWorkID + "' AND B.TYPE='SFG'" +
                         " ORDER BY B.PRODUCTALIAS ASC ";
            dtProduct = db.GetData(Sql);
            return dtProduct;
        }
        #endregion

        #region Bind Batchno Bulk ProductName Wise.
        public DataTable BindBatchBulkProductWise(string BulkProductID,string FrameWorkID)
        {
            DataTable dtBatch = new DataTable();            
            string Sql = "EXEC BIND_MULTIBATCH '" + BulkProductID + "','" + FrameWorkID + "','FG'";
            dtBatch = db.GetData(Sql);
            return dtBatch;
        }
        #endregion

        #region Bind Production Order ID Through Return ID.
        public DataTable BindPoidReturntWise(string ReturnID)
        {
            DataTable dtBatch = new DataTable();
            string Sql = " SELECT PRODUCTIONORDER_ID FROM T_MM_RETURN_HEADER WHERE RETURNID='" + ReturnID + "' ";
            dtBatch = db.GetData(Sql);
            return dtBatch;
        }
        #endregion

        #region Bind Production Order Grace Entry Date.
        public DataTable BINDPRODUCTION_ENTRY_GRACEDT()
        {
            DataTable dtEntryDt = new DataTable();
            string Sql = " SELECT PRODUCTION_ENTRY_GRACEDT FROM P_APPMASTER";
            dtEntryDt = db.GetData(Sql);
            return dtEntryDt;
        }
        #endregion

        #region Bind Previous Bulk Qty.
        public DataTable BINDPREVIOUSBATCH_BULKQTY(string ProductID,string BatchNo)
        {
            DataTable dtEntryDt = new DataTable();
            string Sql = "EXEC BIND_PREVIOUSBATCH_BULKQTY '" + ProductID + "','" + BatchNo + "'";
            dtEntryDt = db.GetData(Sql);
            return dtEntryDt;
        }
        #endregion
        
        public DataTable showStockQtyStorelocationWise(string productId)
        {
            string sql = "EXEC USP_SHOW_STOCK_STORELOCATION_WISE'" + productId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region ProductionOrderEnd start Date : 09-03-2021
        public DataTable BindDataModeWise(string mode,string userId,string productId)
        {
            string finyear= HttpContext.Current.Session["FINYEAR"].ToString(); ;
            string sql = "EXEC Usp_Bind_Data_Mode_Wise'" + mode + "','" + userId + "','"+ productId + "','"+ finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchProductionEnd(string fromDate, string todate, string userid,string finyear,string id)
        {
            string sql = "EXEC usp_find_production_order_end'" + fromDate + "','" + todate + "','" + userid + "','" + finyear + "','"+ id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchRecordFromProduction(string P_ProductionOrderId)
        {
            string sql = "EXEC [Usp_Fetch_Production_Order_Details]'" + P_ProductionOrderId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindBomFromProduction(string P_ProductionId, string userId)
        {
            string sql = "EXEC [USP_EDITFETCH_PRODUCTIONORDER_WITHOUT_MRP_V2]'" + P_ProductionId + "','" + userId + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string InsertUpdateReturnDetails(string XML,  string ProductionUpdateID,string Finyear,string userid,decimal EndQty,decimal closeQty,string entrydate)
        {
            string MRP_ID = string.Empty;
            string MRPNo = string.Empty;
            try
            {
                string sqlMRP = " EXEC USP_RETURN_INSERT_V2 '" + XML + "','" + ProductionUpdateID + "','" + Finyear + "','" + userid + "','" + EndQty + "','" + closeQty + "','" + entrydate + "'";
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

        public DataSet ViewDataFromProduction(string P_ProductionId, string userId)
        {
            string sql = "EXEC [USP_VIEW_PRODUCTION_AND RETURN]'" + P_ProductionId + "','" + userId + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable fetchQty(string ProductionOrderId,decimal endQty,string UserId)/*NEW ADD BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GETQTY_FROM_PRODUCTION] '" + ProductionOrderId + "','"+ endQty + "','"+ UserId + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable CloseProductionOrder(string productionOrderId, string userId, string reason)/*NEW ADD BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_CLOSE_PRODUCTION_ORDER] '" + productionOrderId + "','" + userId + "','" + reason + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        public DataTable loadstorefromdepartment(string MODE, string deptId,string userid)/*NEW ADD BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_FETCH_DATA] '" + MODE + "','" + deptId + "','" + userid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        /*production order bom wise link with production planning*/

        public DataTable fetchPlanningNumber(string id)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GET_PLANNING_DATA] '" + id + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable fettchProductionReport(string fromdate,string todate,string finyear)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_PRODUCTION_STATUS_REPORT_KKG] '" + fromdate + "','" + todate + "','" + finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable ProductionReport(string fromdate,string todate,string userId, string finyear,string status)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PRODUCTION] '" + fromdate + "','" + todate + "','" + userId + "','" + finyear + "','" + status + "'";
            dt = db.GetData(sql);
            return dt;
        }




    }
}
