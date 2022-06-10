using DAL;
using System.Data;

namespace BAL
{
    public class ClsStockAdjustment_FAC
    {
        DBUtils db = new DBUtils();
        public string Getstatus(string STOCKDEPORECEIVEDID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_STOCKADJUSTMENT_HEADER] WHERE ADJUSTMENTID='" + STOCKDEPORECEIVEDID + "'  AND DAYENDTAG='Y')" +
                         " BEGIN SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

            return ((string)db.GetSingleValue(Sql));
        }

        public DataTable BindBusinessegment()
        {
            string sql = "SELECT BSID AS ID,BSNAME AS NAME FROM M_BUSINESSSEGMENT ORDER BY NAME DESC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo, string StoreLocation)
        {
            //string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','0','" + StoreLocation + "'";
            string sql = "EXEC USP_BATCHWISE_FACTORY_STOCK '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','0','" + StoreLocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindExceptionBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo, string StoreLocation)
        {
            string sql = "EXEC SP_BATCHWISE_DEPOT_STOCK_EXCESS '" + DepotID + "','" + ProductID + "','" + PacksizeID + "','" + BatchNo + "','0','" + StoreLocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPRoductDetails(string ProductID, string BatchNo, decimal MRP)
        {
            string sql = "EXEC SP_PRODUCT_DETAILS '" + ProductID + "','" + BatchNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepo(string DEPOTID)
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' AND BRID='" + DEPOTID + "' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReason(string MenuID)
        {
            string sql = " SELECT A.ID,A.NAME AS DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON" +
                         " A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N' AND B.MENUID='" + MenuID + "' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStorelocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string depotid)
        {
            //string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM [T_BATCHWISEAVAILABLE_STOCK] WHERE BRID ='" + depotid + "' ORDER BY PRODUCTNAME";
            string sql = "EXEC SP_DEPOTWISE_PRODUCT '" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductCategoryWise(string Depotid, string CATEGORYID, string Mode)
        {
            string sql = "EXEC SP_DEPOTWISE_PRODUCT_CATEGORY_WISE_MM '" + Depotid + "','" + CATEGORYID + "','" + Mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal BindPrice(string pid, string batchno)
        {
            //string sql = "SELECT DISTINCT ISNULL(SUM(RATE),0) FROM [T_STOCKRECEIVED_DETAILS] WHERE PRODUCTID='" + pid + "' AND BATCHNO='" + batchno + "'";
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_DEPOT_TRANSFER_RATESHEET] where PRODUCTID='" + pid + "') BEGIN " +
                       " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));


            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_DEPOT_TRANSFER_RATESHEET" +
                             " WHERE PRODUCTID = '" + pid + "'";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                price = 0;
            }
            return price;
        }

        public DataTable BindSTockQTYBasedONBATCHNO(string depotid, string pid, string batchno, string TOPACKSIZEID, string adjustmentid)
        {

            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','" + pid + "','" + TOPACKSIZEID + "','" + batchno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchno(string depotid, string pid, string transferid)
        {
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','" + pid + "','" + RETURNPACKSIZE + "','0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackingSizeJournal()
        {
            string sql = " SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM " +
                         " FROM  Vw_PACKSIZE " +
                         " WHERE SEQUENCENO ='3' " +
                         " UNION ALL " +
                         " SELECT PSID,PSNAME + ' -(BINDI)' AS PACKSIZEName_FROM " +
                         " FROM  Vw_PACKSIZE " +
                         " WHERE SEQUENCENO ='2'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAdjustmentControl(string fromdate, string todate, string depotid, string FinYear, string Checker)
        {            
            string sql = "EXEC [USP_FG_STOCK_JOUNRAL_FAC] '" + fromdate + "','" + todate + "','" + depotid + "','" + FinYear + "','"+ Checker + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet GetWeight(string ProductId, string PackSizeid, decimal Qty)
        {
            DataSet ds = new DataSet();
            string sql = "exec [SP_STOCK_WEIGHTCALCULATION] '" + ProductId + "','" + PackSizeid + "'," + Qty + "";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string InsertAdjustmentDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks,string MenuID)
        {
            string flag = "";
            string stockadjustmentno = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            string sqlproc = "EXEC [SP_T_STOCKADJUSTMENT_FG_FAC] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "','" + MenuID + "'";
            DataTable dtadjustmentdetails = db.GetData(sqlproc);

            if (dtadjustmentdetails.Rows.Count > 0)
            {
                stockadjustmentno = dtadjustmentdetails.Rows[0]["ADJUSTMENTNO"].ToString();
            }
            else
            {
                stockadjustmentno = "";
            }
            return stockadjustmentno;
        }
        public DataTable UpdateStock(string mode, string finyear, string depotid, string depotname, string pid, string pname, string batchno, decimal qty, decimal beforeeditedqty, string frompacksizeid)
        {
            string sqlprocstock = "EXEC [sp_STOCKUPDATE] '" + mode + "','" + finyear + "','" + depotid + "','" + depotname + "','" + pid + "','" + pname + "','" + batchno + "','" + qty + "','" + beforeeditedqty + "','" + frompacksizeid + "'";
            DataTable dtflag = db.GetData(sqlprocstock);
            return dtflag;
        }

        public DataTable BindAdjustmentHeader(string adjustmentid)
        {
            string sql = "SELECT ADJUSTMENTID,ADJUSTMENTNO,CONVERT(VARCHAR(10),CAST(ADJUSTMENTDATE AS DATE),103) AS ADJUSTMENTDATE,DEPOTID,FINYEAR,REMARKS,ISVERIFIED FROM [T_STOCKADJUSTMENT_HEADER] WITH (NOLOCK) WHERE ADJUSTMENTID='" + adjustmentid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAdjustmentDetails(string adjustmentid)
        {
            string sql = "  SELECT NEWID() AS GUID,PRODUCTID,PRODUCTNAME,BATCHNO,PACKINGSIZEID,PACKINGSIZENAME,REASONID,REASONNAME,ADJUSTMENTQTY,PRICE," +
                         "  CONVERT(VARCHAR(10),CAST(MFDATE AS DATE),103) AS MFDATE,CONVERT(VARCHAR(10),CAST(EXPRDATE AS DATE),103) AS EXPRDATE," +
                         "  ISNULL(ASSESMENTPERCENTAGE,0) AS ASSESMENTPERCENTAGE,ISNULL(MRP,0) AS MRP,WEIGHT,ISNULL(AMOUNT,0) AS AMOUNT," +
                         " 'E' AS TAG,'0' AS BEFOREEDITEDQTY,STORELOCATIONID,STORELOCATIONNAME FROM [T_STOCKADJUSTMENT_DETAILS] WITH (NOLOCK)" +
                         "  WHERE ADJUSTMENTID='" + adjustmentid + "' " +
                         " ORDER BY SLNO ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            return dt;
        }

        public decimal BindDepotRate(string Productid, decimal MRP)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;
            string exists = string.Empty;
            sqlCheck = " IF  EXISTS(SELECT * FROM [M_DEPOT_TRANSFER_RATESHEET] where PRODUCTID='" + Productid + "') BEGIN " +
                       " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            exists = ((string)db.GetSingleValue(sqlCheck));


            if (exists == "1")
            {
                string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_DEPOT_TRANSFER_RATESHEET" +
                             " WHERE PRODUCTID = '" + Productid + "'";

                price = (decimal)db.GetSingleValue(sql);
            }
            else
            {
                string sql = "EXEC SP_CALCULATE_DEPOT_RATE " + MRP + ",'" + Productid + "'";
                price = (decimal)db.GetSingleValue(sql);
            }

            return price;
        }

        public string TReserve(string ReserveDate)
        {
            string status = string.Empty;
            string sql = "";

            sql = "EXEC SP_TOTAL_BATCHEISE_STOCKTRANSFER '" + ReserveDate + "'";
            status = (string)db.GetSingleValue(sql);

            return status;
        }

        public DataTable BatchDetails(string ReserveDate, string BSID)
        {
            string sql = "";
            sql = "EXEC USP_BATCHDETAILS_BUSINESSSEGMENT '" + ReserveDate + "','" + BSID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            return dt;
        }

        public decimal CalculateQtyPcs(string pid, string packsizeid, decimal qty)
        {
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(sql);
            sql = "SELECT ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + packsizeid + "','" + RETURNPACKSIZE + "'," + qty + "),0)";
            decimal adjqtypcs = (decimal)db.GetSingleValue(sql);

            return adjqtypcs;
        }

        public string InsertShiftStoreLocationDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks)
        {
            string flag = "";
            string stockadjustmentno = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            string sqlproc = "EXEC [CRUD_SHIFTSTORELOCATION_FAC] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "'";
            DataTable dtadjustmentdetails = db.GetData(sqlproc);

            if (dtadjustmentdetails.Rows.Count > 0)
            {
                stockadjustmentno = dtadjustmentdetails.Rows[0]["ADJUSTMENTNO"].ToString();
            }
            else
            {
                stockadjustmentno = "";
            }
            return stockadjustmentno;
        }

        #region ApproveStockJournalFG  (Rajeev 06-05-2019)
        public int ApproveStockJournalFG(string ADJUSTMENTID)
        {
            int result = 0;
            string sql = "UPDATE T_STOCKADJUSTMENT_HEADER SET ISVERIFIED='Y',VERIFIEDDATETIME=GETDATE() WHERE ADJUSTMENTID='" + ADJUSTMENTID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion


        #region RejectStockJournalFG  (Rajeev 06-05-2019)
        public int RejectStockJournalFG(string ADJUSTMENTID)
        {
            int result = 0;
            string sql = "UPDATE T_STOCKADJUSTMENT_HEADER SET ISVERIFIED='R',VERIFIEDDATETIME=GETDATE() WHERE ADJUSTMENTID='" + ADJUSTMENTID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        #region GetApproveStatus FG (Rajeev 06-05-2019)
        public string GetApproveStatus(string ADJUSTMENTID)
        {
            string Sql = " IF EXISTS(SELECT 1 FROM [T_STOCKADJUSTMENT_HEADER] WHERE ADJUSTMENTID='" + ADJUSTMENTID + "' AND ISVERIFIED='Y' OR ISVERIFIED='R' ) BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN	SELECT '0' END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion
    }
}