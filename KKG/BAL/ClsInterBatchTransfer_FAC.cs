using DAL;
using System.Data;

namespace BAL
{
    public class ClsInterBatchTransfer_FAC
    {
        DBUtils db = new DBUtils();
        public DataTable BindBatchDetails(string DepotID, string ProductID, string BatchNo, string StoreLocation)
        {
            string sql = "EXEC USP_BATCHWISE_FACTORY_STOCK '" + DepotID + "','" + ProductID + "','B9F29D12-DE94-40F1-A668-C79BF1BF4425','" + BatchNo + "','0','" + StoreLocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPRoductDetails(string DepotID, string ProductID, string PackSizeID, string BatchNo, string SalableID)
        {
            //string sql = "USP_PRODUCT_DETAILS_INTERBATCH '" + ProductID + "','" + BatchNo + "'";
            string sql = "SP_BATCHWISE_FACTORY_STOCK_PRODUCTWISE'" + DepotID + "','" + ProductID + "','" + PackSizeID + "','" + BatchNo + "','0','" + SalableID + "'";
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
        public DataTable BindProductname(string DepotID)
        {
            string sql = " SELECT A.ID AS PRODUCTID,A.PRODUCTALIAS  AS PRODUCTNAME," +
                            " C.SEQUENCENO, A.CATNAME,A.DIVNAME " +
                            " FROM M_PRODUCT AS A INNER JOIN M_CATEGORY AS C " +
                            " ON C.CATID=A.CATID INNER JOIN M_PRODUCT_TPU_MAP AS MTP " +
                            " ON A.ID = MTP.PRODUCTID " +
                            " WHERE A.ACTIVE='T'" +
                            " AND A.TYPE = 'FG' " +
                            " AND MTP.VENDORID='" + DepotID + "' " +
                            " ORDER BY C.SEQUENCENO, A.CATNAME,A.DIVNAME,PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPacksize()
        {
            string sql = "SELECT PACKSIZE,PACKSIZENAME FROM P_APPMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Binddate(string BATCHNO, string PRODUCTID)
        {
            string sql = "SELECT DISTINCT CONVERT(VARCHAR(10),MFDATE,103) AS MFDATE,CONVERT(VARCHAR(10) ,EXPRDATE,103) AS EXPRDATE " +
                       " FROM M_BATCHDETAILS WHERE  BATCHNO='" + BATCHNO + "' AND PRODUCTID= '" + PRODUCTID + "'";
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
            //string sql = "EXEC SP_DEPOTWISE_PRODUCT '" + depotid + "'";
            string sql = "EXEC SP_DEPOTWISE_PRODUCT_CATEGORY_WISE_MM '" + depotid + "','0','G'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductMRP(string Productid)
        {
            //string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM [T_BATCHWISEAVAILABLE_STOCK] WHERE BRID ='" + depotid + "' ORDER BY PRODUCTNAME";
            string sql = "SELECT MRP FROM M_PRODUCT WHERE ID='" + Productid + "'";
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

        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInterBatchTransfer(string fromdate, string todate, string depotid, string FinYear, string Checker)
        {
            string sql = "EXEC USP_INTERBATCH_TRANSFER_FAC'" + fromdate + "','" + todate + "','" + depotid + "','" + FinYear + "','" + Checker + "' ";
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

        public DataTable GetToProductdetails(string ProductId)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT MRP,ASSESSABLEPERCENT ,(UNITVALUE+' '+UOMNAME) AS WEIGHT FROM M_PRODUCT WHERE ID='" + ProductId + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetToProductBCP(decimal MRP, string ProductId)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC SP_CALCULATE_DEPOT_RATE  " + MRP + ",'" + ProductId + "'";
            decimal depotrate = (decimal)db.GetSingleValue(sql);
            return depotrate;
        }

        public string GetProductExpirydate(string prodid, string date)
        {
            string Expdate = "";
            Expdate = (string)db.GetSingleValue("exec [sp_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
            return Expdate;
        }

        public string InsertInterBatchDetails(string adjustmentdate, string depotid, string depotname, string createdby, string finyear, string hdnvalue, string xml, string Remarks, string ModuleID)
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
            string sqlproc = "EXEC [CRUD_INTERBATCH_TRANSFER] '" + hdnvalue + "','" + flag + "','" + adjustmentdate + "','" + depotid + "','" + depotname + "','" + createdby + "','" + finyear + "','" + xml + "','" + Remarks + "','" + ModuleID + "','B','innerbatchtransfer'";
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
                         " 'E' AS TAG,'0' AS BEFOREEDITEDQTY,STORELOCATIONID,STORELOCATIONNAME FROM [T_STOCKADJUSTMENT_DETAILS] WITH (NOLOCK) WHERE ADJUSTMENTID='" + adjustmentid + "'";
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
                       " SELECT '1'   END  ELSE   BEGIN	SELECT '0'  END ";
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
    }
}