using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsStockJournal
    {
        DBUtils db = new DBUtils();
        public DataTable BindDeptName(string BRID)
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'AND BRID='" + BRID + "' order by BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand()
        {
            string sql = " SELECT DIVID,DIVNAME from M_DIVISION" +
                         " UNION ALL" +
                         " SELECT CAST(ID AS VARCHAR(10)) AS DIVID,ITEMDESC AS DIVNAME FROM M_SUPLIEDITEM" +
                         " WHERE ITEMCODE <>'FG'" +
                         " ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory(string divid)
        {
            string sql = "SELECT DISTINCT CATID,CATNAME FROM M_PRODUCT WHERE DIVID='" + divid + "' AND ACTIVE='T' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct(string catid, string DIVID) /*INLINE TO SP CONVERT WITH NEW CONDITION BY P.BASU*/
        {
            string sql = " USP_LOAD_PRODUCT_ALL_OR_WISE '"+ catid + "','"+ DIVID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPackingSize(string PID) /*[Vw_SALEUNIT] to M_PRODUCT_UOM_MAP*/
        {
            string sql = "SELECT PACKSIZEID_FROM,PACKSIZEName_FROM from M_PRODUCT_UOM_MAP WITH(NOLOCK) where PRODUCTID='"+ PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMRPperproduct(string PRODUCTID)
        {
            string sql = "SELECT ID,NAME,MRP,ASSESSABLEPERCENT FROM M_PRODUCT WHERE ID='" + PRODUCTID + "' AND ACTIVE='T' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStorelocation()
        {
            string sql = "SELECT ID,NAME FROM M_STORELOCATION WHERE ID NOT IN('FAF6D902-EDB1-45B8-A021-7AFAFD4D0C03','8DE352DA-1862-4C41-97BF-5CEC50184CA2') --ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public decimal BindDepotRate(decimal MRP, string prodid)
        {
            string sqlCheck = string.Empty;
            decimal price = 0;

            string sql = "EXEC SP_CALCULATE_DEPOT_RATE " + MRP + ",'" + prodid + "'";
            price = (decimal)db.GetSingleValue(sql);
            return price;
        }

        public int Saveopenstock(string finyear, string xml, string mode, string depotid, string brandid, string catagoryid, string concatanetproductid, string OpeningDate, string TAG)
        {
            int result = 0;
            try
            {
                string sql = " EXEC [USP_MM_OPEN_STOCK_ENTRY_INSERT]'" + finyear + "','" + xml + "','" + mode + "','" + depotid + "','" + brandid + "','" + catagoryid + "','" + concatanetproductid + "','" + OpeningDate + "','" + TAG + "'";
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public DataTable BindAdjustmentControl(string fromdate, string todate, string depotid, string FinYear, string Checker)
        {
            string sql = "EXEC USP_RMPM_STOCK_JOUNRAL_FAC '" + fromdate + "','" + todate + "','" + depotid + "','" + FinYear + "','" + Checker + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Gridedit(string brid, string brandid, string catagoryid, string LocationID, string TAG)
        {
            string sql = " SELECT DISTINCT st.BRID, st.BRNAME,P.DIVID as DIVISIONID,P.DIVNAME as DIVISIONNAME,P.CATID as CATEGORYID,P.CATNAME as CATEGORYNAME, st.PRODUCTID," +
                         " st.PRODUCTNAME,st.PACKSIZEID, st.PACKSIZENAME,st.BATCHNO, st.STOCKQTY,ISNULL(ST.MRP,0) AS MRP,ISNULL(ST.DEPOTRATE,0) AS DEPOTRATE,ST.MFGDATE,ST.EXPDATE," +
                         " ISNULL(ST.ASSESMENTPERCENTAGE,0) AS ASSESMENTPERCENTAGE,ISNULL(ST.TOTALASSESMENTVALUE,0) AS TOTALASSESMENTVALUE,ST.NETWEIGHT,ST.GROSSWEIGHT,ST.STORELOCATIONID," +
                         " ST.STORELOCATIONNAME,CONVERT(VARCHAR(10),CAST(ST.OPENINGENTRYDATE AS DATETIME),103) AS OPENINGENTRYDATE,st.TAG AS TAG from T_MM_BATCHWISEAVAILABLE_STOCK as st inner join " +
                         " M_PRODUCT as P on st.PRODUCTID=P.ID where st.BRID='" + brid + "' AND P.DIVID='" + brandid + "' AND P.CATID='" + catagoryid + "' AND st.TAG='" + TAG + "' ";
            //AND st.STORELOCATIONID='" + LocationID + "'
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetProductExpirydate(string prodid, string date)
        {
            string Expdate = "";
            Expdate = (string)db.GetSingleValue("EXEC [SP_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
            return Expdate;
        }

        public DataSet GetWeight(string ProductId, string PackSizeid, decimal Qty)
        {
            DataSet ds = new DataSet();
            string sql = "exec [SP_STOCK_WEIGHTCALCULATION] '" + ProductId + "','" + PackSizeid + "'," + Qty + "";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public decimal CalculateAmountInPcs(string productID, string packsizeFromID, decimal Qty, decimal Rate)
        {
            decimal Amount = 0;
            decimal RETURNVALUE = 0;

            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            Amount = Rate * RETURNVALUE;
            return Amount;
        }
        public DataTable MasterBatchDetailsCheckFactory(string ProductID, string BatchNo, decimal Mrp, string MFDate, string EXPRDate, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_MM_MASTERBATCH_CHECK '" + ProductID + "','" + BatchNo + "','" + Mrp + "','" + MFDate + "','" + EXPRDate + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable MasterBatchDetailsCheck(string ProductID, string BatchNo, decimal Mrp, string MFDate, string EXPRDate, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC SP_MASTERBATCH_CHECK '" + ProductID + "','" + BatchNo + "','" + Mrp + "','" + MFDate + "','" + EXPRDate + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string checkBatchForClosingStockFactory(string productid, string batchno, decimal MRP)
        {
            string FLAG = string.Empty;
            string sql = "IF NOT EXISTS (select PRODUCTID,BATCHNO,MFDATE,EXPRDATE from M_MM_BATCHDETAILS where  PRODUCTID='" + productid + "' and " +
                                    " BATCHNO='" + batchno + "' AND MRP=" + MRP + " ) " +
                          " SELECT '0'" +
                          " ELSE       " +
                          " select '1'  " +
                          " RETURN     ";
            FLAG = (string)db.GetSingleValue(sql);
            return FLAG;
        }

        public string checkBatchForClosingStock(string productid, string batchno, decimal MRP)
        {
            string FLAG = string.Empty;
            string sql = "IF NOT EXISTS (select PRODUCTID,BATCHNO,MFDATE,EXPRDATE from M_BATCHDETAILS where  PRODUCTID='" + productid + "' and " +
                                    " BATCHNO='" + batchno + "' AND MRP=" + MRP + " ) " +
                          " SELECT '0'" +
                          " ELSE       " +
                          " select '1'  " +
                          " RETURN     ";
            FLAG = (string)db.GetSingleValue(sql);
            return FLAG;
        }
        public DataTable BindFactory(string UserID)
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE VENDORID IN (SELECT TPUID FROM M_TPU_USER_MAPPING where USERID ='" + UserID + "'  and Tag='F'  )";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPU(string UserID)
        {
            string sql = " SELECT '-1' AS VENDORID,'--SELECT TPU--' AS VENDORNAME" +
                         " union" +
                         " select VENDORID,VENDORNAME from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "' and Tag='T'  )";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrand_SupliedItem()
        {
            string sql = "EXEC USP_MM_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory_SupliedItem(string divid)
        {
            string sql = "EXEC USP_MM_LOADCATEGORY '" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet EditJournalDetails(string JournalID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_T_STOCKADJUSTMENT_EDIT_MM '" + JournalID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string InsertAdjustmentDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks, string ModuleID)
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
            string sqlproc = "EXEC [USP_T_STOCKADJUSTMENT_INSERTUPDATE_MM] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "','" + ModuleID + "'";
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

        #region Add By Rajeev (16-08-2018)
        public DataTable Bind_StockinHand(string Depotid, string Productid, string PackSizeID, string BatchNo, decimal Mrp, string Brand, string Category, string Fromdate, string Todate, string Curdate, string Size, string Storelocation)
        {
            string sql = "EXEC USP_RPT_JOURNAL_STOCK_IN_HAND_RMPM '" + Depotid + "','" + Productid + "','" + PackSizeID + "','" + BatchNo + "','" + Mrp + "','" + Brand + "','" + Category + "','" + Fromdate + "','" + Todate + "','" + Curdate + "','" + Size + "','" + Storelocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region ApproveStockJournalRMPM  (Rajeev 04-05-2019)
        public int ApproveStockJournalRMPM(string ADJUSTMENTID)
        {
            int result = 0;
            string sql = "UPDATE T_MM_STOCKADJUSTMENT_HEADER SET ISVERIFIED='Y',VERIFIEDDATETIME=GETDATE() WHERE ADJUSTMENTID='" + ADJUSTMENTID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        #region RejectStockJournalRMPM  (Rajeev 04-05-2019)
        public int RejectStockJournalRMPM(string ADJUSTMENTID)
        {
            int result = 0;
            string sql = "UPDATE T_MM_STOCKADJUSTMENT_HEADER SET ISVERIFIED='R',VERIFIEDDATETIME=GETDATE() WHERE ADJUSTMENTID='" + ADJUSTMENTID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        #region GetApproveStatus RMPM  (Rajeev 04-05-2019)
        public string GetApproveStatus(string ADJUSTMENTID)
        {
            string Sql = " IF EXISTS(SELECT 1 FROM [T_MM_STOCKADJUSTMENT_HEADER] WHERE ADJUSTMENTID='" + ADJUSTMENTID + "' AND (ISVERIFIED='Y' OR ISVERIFIED='R')) BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN	SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        #region GetApproveStatus 
        public string GetApproveStatus1(string ADJUSTMENTID)
        {
            string Sql = " IF EXISTS(SELECT 1 FROM [T_INTER_STORE_TRANSFER_HEADER] WHERE ADJUSTMENTID='" + ADJUSTMENTID + "' AND (ISVERIFIED='Y' OR ISVERIFIED='R')) BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN	SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        #endregion

        public decimal GetMrpProductWise(string productID)
        {
            decimal Mrp = 0;
            string SQL = "Select isnull(Mrp,0)MRP From M_PRODUCT WITH(NOLOCK) where id='"+ productID + "'";
            Mrp = (decimal)db.GetSingleValue(SQL);
            return Mrp;
        }


        public string InsertBulkJournalDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks)
        {
            string flag = "";
            DataTable dtadjustment = new DataTable();
            string stockadjustmentid = string.Empty;
            string stockadjustmentno = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];
            string sqlproc = "EXEC [USP_BULK_STOCKADJUSTMENT_EXCEL_UPLOAD] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "'";
            DataTable dtadjustmentdetails = db.GetData(sqlproc);

            if (dtadjustmentdetails.Rows.Count > 0)
            {
                stockadjustmentid = dtadjustmentdetails.Rows[0]["ADJUSTMENTID"].ToString();
                stockadjustmentno = dtadjustmentdetails.Rows[0]["ADJUSTMENTNO"].ToString();
            }
            else
            {
                stockadjustmentno = "";
            }

            return stockadjustmentno;
        }

        public DataTable BindbulkproductStoreWise(String USERID)
        {
            string sql = "EXEC [USP_BULK_JOURNAL_TEMPLETE] '" + USERID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public decimal GetQty(string depotid,string productid,string locationid)
        {
            decimal Mrp = 0;
            string SQL = "Select [dbo].[fn_Rpt_Stock] ('"+ depotid + "','"+ productid + "','"+ locationid + "')";
            Mrp = (decimal)db.GetSingleValue(SQL);
            return Mrp;
        }

        public string InsertInterStoreReturnDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks, string ModuleID,string FROMID,string TOID)
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
            string sqlproc = "EXEC [USP_INTER_STORE_TRANSFER_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "','" + ModuleID + "','"+ FROMID + "','"+ TOID + "'";
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
        public DataTable BindInterTransferDetails(string fromdate, string todate, string depotid, string FinYear, string Checker)
        {
            string userid= HttpContext.Current.Session["USERID"].ToString().Trim();
            string sql = "EXEC USP_INTER_STORE_DETAILS_FAC '" + fromdate + "','" + todate + "','" + depotid + "','" + FinYear + "','" + Checker + "','" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet EditReturnDetails(string JournalID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [USP_INTER_STORE_RETURN_EDIT] '" + JournalID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        #region ApproveRejectinterStore
        public int ApproveRejectinterStore(string mode,string ADJUSTMENTID,string USERID)
        {
            int result = 0;
            string sql = "EXEC USP_APPROVED_REJECT_INTER_STORE '" + mode + "','" + ADJUSTMENTID + "','" + USERID + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        /*20/04/2022 PRITAM BASU*/

        public DataTable BindProductDetailsFromProduct(string mode, string ID)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_CHECK_STATUS '" + mode + "','" + ID + "','','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable UpdateOpeningStock(string ProductId, string StoreId, decimal Qty, decimal Rate)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_UPDATE_OPENING_STOCK '" + ProductId + "','" + StoreId + "','" + Qty + "','" + Rate + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

    }
}
