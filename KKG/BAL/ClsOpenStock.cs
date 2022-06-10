using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsOpenStock
    {
        DBUtils db = new DBUtils();
        public DataTable BindDeptName(string BRID)
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'AND BRID='" + BRID + "' ORDER BY BRNAME";
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
            string sql = "SELECT DISTINCT CATID,CATNAME from M_PRODUCT where DIVID='" + divid + "' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string catid, string DIVID)/*inline to sp convert by p.basu on 16-02-2021*/
        {
            string sql = "USP_LOAD_PRODUCT_FOR_OPENING '"+ catid + "','"+ DIVID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWithOutSaveProduct(string catid, string DIVID, string DEPOTID, string TAG)
        {
            string SQLWITHOUTPRODUCT = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + PRODUCTID " +
                                       " FROM T_MM_BATCHWISEAVAILABLE_STOCK " +
                                       " WHERE BRID='" + DEPOTID + "'AND TAG='" + TAG + "' " +
                                       " select @Names";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(SQLWITHOUTPRODUCT);
            string PRODUCTID = "";

            if (dt1.Rows.Count > 0)
            {
                PRODUCTID = dt1.Rows[0][0].ToString();
            }

            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A " +
                         " LEFT JOIN M_CATEGORY AS B ON A.CATID= B.CATID" +
                         " WHERE A.CATID='" + catid + "' AND A.DIVID='" + DIVID + "' AND A.ID NOT IN (SELECT * from dbo.fnSplit('" + PRODUCTID + "',',')) " +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWithOutSaveProductaddnew(string catid, string DIVID, string DEPOTID, string TAG, string tag1)
        {
            string SQLWITHOUTPRODUCT = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + PRODUCTID " +
                                       " FROM T_MM_BATCHWISEAVAILABLE_STOCK " +
                                       " WHERE BRID='" + DEPOTID + "'AND TAG in ('" + TAG + "','" + tag1 + "') " +
                                       " select @Names";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(SQLWITHOUTPRODUCT);
            string PRODUCTID = "";

            if (dt1.Rows.Count > 0)
            {
                PRODUCTID = dt1.Rows[0][0].ToString();
            }
            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A " +
                         " LEFT JOIN M_CATEGORY AS B ON A.CATID= B.CATID" +
                         " WHERE A.CATID='" + catid + "' AND A.DIVID='" + DIVID + "' AND A.ID NOT IN (SELECT * from dbo.fnSplit('" + PRODUCTID + "',',')) " +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductWithOutStock(string catid, string DIVID, string DEPOTID, string TAG)
        {
            string SQLWITHOUTPRODUCT = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + PRODUCTID " +
                                       " FROM T_MM_BATCHWISEAVAILABLE_STOCK " +
                                       " WHERE BRID='" + DEPOTID + "' " +
                                       " select @Names";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(SQLWITHOUTPRODUCT);
            string PRODUCTID = "";

            if (dt1.Rows.Count > 0)
            {
                PRODUCTID = dt1.Rows[0][0].ToString();
            }
            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A " +
                         " LEFT JOIN M_CATEGORY AS B ON A.CATID= B.CATID" +
                         " WHERE A.CATID='" + catid + "' AND A.DIVID='" + DIVID + "' AND A.ID NOT IN (SELECT * from dbo.fnSplit('" + PRODUCTID + "',',')) " +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT DISTINCT PSID AS PACKSIZEID_FROM,UOMNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "' AND UOMNAME IS NOT NULL";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMRPperproduct(string PRODUCTID)
        {
            string sql = "SELECT ID,PRODUCTALIAS AS NAME,MRP,ASSESSABLEPERCENT FROM M_PRODUCT WHERE ID='" + PRODUCTID + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGenerateTempForTaxCategory()
        {
            string sql = "EXEC [USP_BIND_TAX_CATEGORY_FOR_TEMPLATE]";
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

        public int UploadTaxCategory(string xml)
        {
            int result = 0;
            try
            {
                string sql = " EXEC [USP_UPLOAD_TAX_CATEGORY_INSERT]'" + xml + "'";
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindOpenStockGrid(string DepotID, string TAG, string FinYear)
        {
            string sql = " EXEC [USP_BIND_OPENINGSTOCK_FAC]'" + DepotID + "','" + TAG + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Gridedit(string brid, string brandid, string catagoryid, string LocationID, string TAG)
        {
            string sql = " SELECT DISTINCT  st.BRID, st.BRNAME,P.DIVID as DIVISIONID,P.DIVNAME as DIVISIONNAME,P.CATID as CATEGORYID,P.CATNAME as CATEGORYNAME, st.PRODUCTID," +
                         " st.PRODUCTNAME, st.UOMID AS PACKSIZEID, st.PACKSIZENAME,st.BATCHNO, st.STOCKQTY,ISNULL(ST.MRP,0) AS MRP,ISNULL(ST.DEPOTRATE,0) AS DEPOTRATE,ST.MFGDATE,ST.EXPDATE," +
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
            Expdate = (string)db.GetSingleValue("exec [SP_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
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


        #region ForUpload Opening Stock

        public int SaveUploadOpeningstock(string Finyear, string depotid, string xml, string Openingdate, string TOTALPRODUCTID)
        {
            int RESULT = 0;
            string sql = string.Empty;
            sql = "EXEC USP_MM_OPENING_STOCK_UPLOAD '" + Finyear + "','" + depotid + "','" + xml + "','" + Openingdate + "','" + TOTALPRODUCTID + "'";
            RESULT = db.HandleData(sql);
            return RESULT;
        }

        public string GETUOMID(string id)
        {
            string _UOMNAME = string.Empty;
            string sql = "SELECT PACKSIZEID_FROM UOMID FROM M_PRODUCT_UOM_MAP WITH(NOLOCK) WHERE PRODUCTID='" + id + "'";
            _UOMNAME = (string)db.GetSingleValue(sql);
            return _UOMNAME;
        }

        public string CHKUOMNAME(string PRODUCTID)
        {
            string _UOMNAMECHK = string.Empty;
            string sql = " SELECT UOMNAME FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            _UOMNAMECHK = (string)db.GetSingleValue(sql);
            return _UOMNAMECHK;
        }

        public decimal GETASSESMNETVALUE(string PRODUCTID)
        {
            string SQL = "SELECT ASSESSABLEPERCENT FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            decimal ASSEMNT = (decimal)db.GetSingleValue(SQL);
            return ASSEMNT;
        }
        public DataTable BindUploadStorelocation()
        {
            string userid = Convert.ToString(HttpContext.Current.Session["USERID"]).Trim();
            string sql = "EXEC [USP_BIND_STORELOCATION_USERWISE] '"+ userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string checkBatch(string productid, string batchno, string MRP)
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

        public DataTable GetDateBatchDetails(string PRODUCTID, string BATCHNO, string MRP)
        {

            string SQL = string.Empty;
            SQL = "SELECT CONVERT(VARCHAR(10),CAST(MFDATE AS DATE),103)  AS MFDATE,CONVERT(VARCHAR(10),CAST(EXPRDATE AS DATE),103) AS EXPRDATE  FROM  M_MM_BATCHDETAILS WHERE " +
                     " PRODUCTID='" + PRODUCTID + "' AND BATCHNO='" + BATCHNO + "'" +
                     "  AND MRP=" + MRP + "";
            DataTable dt = db.GetData(SQL);
            return dt;
        }
        #endregion


        public DataTable BindFactory(string UserID)
        {
            string sql = "select VENDORID, VENDORNAME  from   M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "'  and Tag='F'  )";
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

        public DataTable BindWithOutSaveProductopen(string catid, string DIVID, string DEPOTID, string TAG)
        {

            string SQLWITHOUTPRODUCT = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + PRODUCTID " +
                                       " FROM T_MM_BATCHWISEAVAILABLE_STOCK " +
                                       " WHERE BRID='" + DEPOTID + "'AND TAG='" + TAG + "' " +
                                       " select @Names";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(SQLWITHOUTPRODUCT);
            string PRODUCTID = "";

            if (dt1.Rows.Count > 0)
            {
                PRODUCTID = dt1.Rows[0][0].ToString();
            }
            /*productname to PRODUCTALIAS change for kkg by p.basu on 16-12-2020*/
            string sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME,B.SEQUENCENO, A.CATNAME,A.DIVNAME" +
                         " FROM M_PRODUCT AS A  " +
                         " LEFT JOIN M_CATEGORY AS B ON A.CATID= B.CATID" +
                         " WHERE A.CATID='" + catid + "' AND A.DIVID='" + DIVID + "'  " +
                         " AND A.ID NOT IN (SELECT * from dbo.fnSplit('" + PRODUCTID + "',','))" +
                         " ORDER BY B.SEQUENCENO, A.CATNAME,A.DIVNAME, A.NAME ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
