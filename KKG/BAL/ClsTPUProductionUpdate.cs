using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using Utility;
using System.Globalization;
using DAL;

namespace BAL
{

    public class ClsTPUProductionUpdate
    {
        DBUtils db = new DBUtils();

        public DataTable BindPO()
        {
            string sql = "SELECT POID,PONO FROM T_TPU_POHEADER ORDER BY PODATE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetQCstatus(string POUPDATEID)
        {

            string Sql = "  IF  EXISTS(SELECT * FROM [T_TPU_QUALITYCONTROL_DETAILS] where puid='" + POUPDATEID + "') BEGIN " +
                            " select '1'   END  else    begin	  select '0'   end ";


            return ((string)db.GetSingleValue(Sql));

        }

        public decimal GetPOItemWiseRemainingQty(string POID, string ProductID, string PacksizeID, string productionid, string TPU)
        {
            decimal Qty = 0;

            if (TPU == "T")
            {

                Qty = (decimal)db.GetSingleValue("select dbo.GetPOItemWiseRemainingQty('" + POID + "','" + ProductID + "','" + PacksizeID + "','" + productionid + "') ");
            }

            if (TPU == "F")
            {

                Qty = (decimal)db.GetSingleValue("select dbo.[GetIndentItemWiseRemainingQty]('" + POID + "','" + ProductID + "','" + PacksizeID + "','" + productionid + "') ");
            }
            else/*NEW ADD FOR KKG BY P.BASU ON 27112020 FOR REMANING QTY*/
            {

                Qty = (decimal)db.GetSingleValue("select dbo.[GetIndentItemWiseRemainingQty_KKG]('" + POID + "','" + ProductID + "','" + PacksizeID + "','" + productionid + "') ");
            }

            return Qty;
        }

        public DataTable FetchPODate(string poid)
        {
            string sql = "SELECT CONVERT(VARCHAR(8),PODATE,112) AS PODATE FROM T_TPU_POHEADER WITH (NOLOCK) WHERE POID='" + poid + "'";
            DataTable PODATE = db.GetData(sql);
            return PODATE;
        }

        public DataTable FetchFACTORYDate(string poid)/* T_Factory_POHEADER TO TBL_PRODUCTION_ORDER_HEADER CHANGE FOR KKG BY  P.BASU ON 27-11-2020*/
        {
            string sql = "SELECT CONVERT(VARCHAR(8),ENTRYDATE,112) AS PODATE FROM TBL_PRODUCTION_ORDER_HEADER WITH (NOLOCK) WHERE PRODUCTIONID='" + poid + "'";
            DataTable PODATE = db.GetData(sql);
            return PODATE;
        }

        public DataTable BindMaxPODate(string poid)
        {
            string sql = "SELECT CONVERT(VARCHAR(8),MAX(PODATE),112) AS PODATE,CONVERT(VARCHAR(10),CAST(MAX(PODATE) AS DATE),103) AS SHOWPODATE FROM T_TPU_POHEADER WITH (NOLOCK) WHERE POID IN(" + poid + ")";
            DataTable PoDATE = db.GetData(sql);
            return PoDATE;
        }

        public DataTable BindMaxFactoryDate(string poid)
        {
            string sql = "SELECT CONVERT(VARCHAR(8),MAX(PODATE),112) AS PODATE,CONVERT(VARCHAR(10),CAST(MAX(PODATE) AS DATE),103) AS SHOWPODATE FROM T_Factory_POHEADER WITH (NOLOCK) WHERE POID IN(" + poid + ")";
            DataTable PoDATE = db.GetData(sql);
            return PoDATE;
        }

        //public DataTable BindProductExpiry(string prodid, string date)
        //{
        //    string sql = "exec [sp_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public string GetProductExpirydate(string prodid, string date)
        {
            string Expdate = "";
            Expdate = (string)db.GetSingleValue("exec [sp_TPU_GETEXPIRYDATE] '" + prodid + "','" + date + "'");
            return Expdate;
        }

        public DataTable BindPUHeader(string puno)
        {
            string sql = "select  PUNO,BATCHNO,CONVERT(VARCHAR(10),CAST(PUDATE AS DATE),103) AS PUDATE,CONVERT(VARCHAR(10),CAST(ManfDate AS DATE),103) AS ManfDate from T_TPU_PRODUCTION_HEADER WITH (NOLOCK) WHERE  ID='" + puno + "'";
            //PONO,PRODUCTNAME, POTOTALQTY AS TOTALQTY, POREMAININGQTY AS REMAININGQTY,POUPDATEQTY AS ACTLQTY,UOM,UOMID,PUID, POID,  PRODUCTID,    POUPDATEDATE, TPUID, TPUNAME, REMARKS,  ISCLOSED, CLOSEDDATE, PUDATE from T_TPU_PRODUCTION_UPDATE WHERE  PUID='" + puno + "'";
            //string sql = "exec [sp_TPU_EDITPRODUCTIONUPDATE] '" + puno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //public DataTable BindFactoryHeader(string puno)
        //{
        //    string sql = "select  PUNO,BATCHNO,CONVERT(VARCHAR(10),CAST(PUDATE AS DATE),103) AS PUDATE,CONVERT(VARCHAR(10),CAST(ManfDate AS DATE),103) AS ManfDate from T_TPU_PRODUCTION_HEADER WHERE  ID='" + puno + "'";//PONO,PRODUCTNAME, POTOTALQTY AS TOTALQTY, POREMAININGQTY AS REMAININGQTY,POUPDATEQTY AS ACTLQTY,UOM,UOMID,PUID, POID,  PRODUCTID,    POUPDATEDATE, TPUID, TPUNAME, REMARKS,  ISCLOSED, CLOSEDDATE, PUDATE from T_TPU_PRODUCTION_UPDATE WHERE  PUID='" + puno + "'";
        //    //string sql = "exec [sp_TPU_EDITPRODUCTIONUPDATE] '" + puno + "'";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}

        public DataTable BindPU(string puno)
        {
            //string sql = "select PONO,PRODUCTNAME, POTOTALQTY AS TOTALQTY, POREMAININGQTY AS REMAININGQTY,POUPDATEQTY AS ACTLQTY,UOM,UOMID,PUID, POID,  PRODUCTID,    POUPDATEDATE, TPUID, TPUNAME, REMARKS,  ISCLOSED, CLOSEDDATE, PUDATE from T_TPU_PRODUCTION_UPDATE WHERE  PUID='" + puno + "'";
            string sql = "exec [sp_TPU_EDITPRODUCTIONUPDATE] '" + puno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFACTORY(string puno)
        {
            //string sql = "select PONO,PRODUCTNAME, POTOTALQTY AS TOTALQTY, POREMAININGQTY AS REMAININGQTY,POUPDATEQTY AS ACTLQTY,UOM,UOMID,PUID, POID,  PRODUCTID,    POUPDATEDATE, TPUID, TPUNAME, REMARKS,  ISCLOSED, CLOSEDDATE, PUDATE from T_TPU_PRODUCTION_UPDATE WHERE  PUID='" + puno + "'";
            string sql = "exec [sp_Factory_EDITPRODUCTIONUPDATE] '" + puno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPUNO(string pono)
        {
            string sql = "select distinct PUID ,PUID from T_TPU_PRODUCTION_UPDATE WITH (NOLOCK) where pono='" + pono + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPUPDATENO(string TpuID, string finyear, string FromDate, string ToDate)
        {
            try
            {
                string sql = "select ID,PUNO,CONVERT(VARCHAR(10),CAST(A.PUDATE AS DATE),103) AS PUDATE,ISNULL(TOTALCASEPACK,0) AS TOTALCASEPACK,COUNT(DISTINCT PRODUCTID) AS TOTALPRODUCT from T_TPU_PRODUCTION_HEADER A INNER JOIN T_TPU_PRODUCTION_DETAILS B ON A.ID=B.PUID " +
                             " WHERE CONVERT(date,A.PUDATE,103) between dbo.Convert_To_ISO('" + FromDate + "') and dbo.Convert_To_ISO('" + ToDate + "') AND FINYEAR='" + finyear + "' " +
                             " AND TPUID='" + TpuID + "' GROUP BY ID,PUNO,A.PUDATE,TOTALCASEPACK  order by A.PUDATE desc";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindPOProctuct(string PONO, string PUID, string MFGDATE, string ProductID)
        {
            string sql = "exec [sp_TPU_GETPRODUCTIONUPDATE] '" + PONO + "','" + PUID + "','" + MFGDATE + "','" + ProductID + "'";
            //string sql = "SELECT PONO,PRODUCTNAME,QTY AS TOTALQTY,CAST(QTY - (SELECT ISNULL(SUM(POUPDATEQTY),0) FROM T_TPU_PRODUCTION_UPDATE WHERE PONO='" + PONO + "') AS INT) AS REMAININGQTY,'0' as ACTLQTY FROM T_TPU_PODETAILS WHERE PONO='" + PONO + "'"; 
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

       

        public string ExistsBatchno(string pid, string batchno)
        {
            string sql = "SELECT BATCHNO FROM dbo.T_TPU_PRODUCTION_DETAILS WITH (NOLOCK) WHERE PRODUCTID='" + pid + "' AND BATCHNO='" + batchno + "'";
            string GetBatchno = (string)db.GetSingleValue(sql);
            return GetBatchno;
        }

        public DataTable BindPO(string fromdate, string todate, string tpuid)
        {
            string sql = "select POID,PONO from [T_TPU_POHEADER] WITH (NOLOCK) where TPUID='" + tpuid + "' and CONVERT(date,PODATE) between  dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') and ISCLOSED='N' order by PONO desc";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactory(string fromdate, string todate, string tpuid)
        {
            string sql = "select POID,PONO from [T_Factory_POHEADER] WITH (NOLOCK) where TPUID='" + tpuid + "' and CONVERT(date,PODATE) between  dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') order by PODATE desc";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPUNO(DateTime fromdate, DateTime todate)
        {
            string sql = "select POID,PONO from [T_TPU_POHEADER] WITH (NOLOCK) where CONVERT(date,PODATE,103) between CONVERT(date,'" + fromdate + "',103) and CONVERT(date,'" + todate + "',103) AND CLOSEDDATE IS NULL order by PODATE desc";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackingSize(string pid)
        {
            string sql = "SELECT DISTINCT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + pid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SavePUSheetHeader(string puid, string poid, string pono, string prodid, string prodname, decimal orderqty, decimal rmianingqty, decimal actualqty, string tpuid, string tpuname, string remarks, string pudate, string uom, string uomid, string batchno, string manfdate, string expdate, string Tag, decimal TotalCasePack)
        {
            int saveflag = 0;
            string flag = "";
            if (puid == "")
            { flag = "A"; }
            else
            { flag = "U"; }

            DataTable dt = new DataTable();
            string sqlprocpo = "exec [sp_TPU_INSERTPRODUCTIONUPDATE_HEADER] '" + flag + "', '" + puid + "','" + poid + "','" + pono + "','" + prodid + "','" + prodname + "'," + orderqty + "," + rmianingqty + "," + actualqty + ",'" + tpuid + "','" + tpuname + "','" + remarks + "'," + HttpContext.Current.Session["UserID"].ToString() + ",'" + pudate + "','" + uom + "','" + uomid + "','" + batchno + "','" + HttpContext.Current.Session["FINYEAR"].ToString() + "','" + manfdate + "','" + expdate + "','" + Tag + "'," + TotalCasePack + "";
            //int p = db.HandleData(sqlprocpo);
            dt = db.GetData(sqlprocpo);

            if (dt.Rows.Count > 0)
            {
                //saveflag = 1;
            }
            return dt;
        }

        public DataTable SavePUSheetDetails(string hdnfld, string puid, string poid, string pono, string prodid, string prodname, decimal orderqty, decimal rmianingqty, decimal actualqty, string tpuid, string tpuname, string remarks, string pudate, string uom, string uomid, string batchno, string manfdate, string expdate)
        {
            int saveflag = 0;
            string flag = "";
            if (hdnfld == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }

            DataTable dt = new DataTable();
            string sqlprocpo = "exec [sp_TPU_INSERTPRODUCTIONUPDATE_DETAILS] '" + flag + "', '" + puid + "','" + poid + "','" + pono + "','" + prodid + "','" + prodname + "'," + orderqty + "," + rmianingqty + "," + actualqty + ",'" + tpuid + "','" + tpuname + "','" + remarks + "'," + HttpContext.Current.Session["UserID"].ToString() + ",'" + pudate.Trim() + "','" + uom + "','" + uomid + "','" + batchno + "','" + HttpContext.Current.Session["FINYEAR"].ToString() + "','" + manfdate.Trim() + "','" + expdate.Trim() + "'";
            //int p = db.HandleData(sqlprocpo);
            dt = db.GetData(sqlprocpo);

            if (dt.Rows.Count > 0)
            {
                //saveflag = 1;
            }
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

        public int DeleteProduction(string ProductionId)
        {
            int delflag = 0;

            string sql = "EXEC [SP_PRODUCTION_DELETE] '" + ProductionId + "'";

            int e = db.HandleData(sql);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }

        public decimal GetPackingSize_OnCall(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;

            string sql = "SELECT PACKSIZE FROM P_APPMASTER";/*CASEPACKSIZEID TO PACKSIZE CHANGE BY P.BASU FOR KKG BECAUSE ALL PRODUCTS ARE IN PCS*/
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;
        }





        //--------------------------------------------------------------- COMBO FUNCTION ------------------------------------------------------------------------//


        public string Check_IsCombo_Item(string Product_Id)
        {

            string Sql = "  IF  EXISTS(SELECT 1 FROM dbo.M_PRODUCT WHERE SETFLAG='C' AND ID='" + Product_Id + "') BEGIN " +
                            " select '1'   END  else    begin	  select '0'   end ";


            return ((string)db.GetSingleValue(Sql));

        }



        public DataTable BindPOProctuct_New(string PONO, string PUID, string MFGDATE, string ProductID)
        {
            string sql = "exec [sp_TPU_GETPRODUCTIONUPDATE_COMBO] '" + PONO + "','" + PUID + "','" + MFGDATE + "','" + ProductID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindPOProctuct_For_Combo(string PONO, string PUID, string MFGDATE, string ProductID, decimal PU_QTY)
        {
            string sql = "exec [sp_TPU_GETPRODUCTIONUPDATE_COMBO_PUQTY] '" + PONO + "','" + PUID + "','" + MFGDATE + "','" + ProductID + "'," + PU_QTY + "";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SavePUSheetHeader_Combo(string puid, string poid, string pono, string prodid, string prodname, decimal orderqty, decimal rmianingqty, decimal actualqty, string tpuid, string tpuname, string remarks, string pudate, string uom, string uomid, string batchno, string manfdate, string expdate, string Tag, decimal TotalCasePack, string combo_id, string combo_name, decimal combo_order, string combo_packsize)
        {
            int saveflag = 0;
            string flag = "";
            if (puid == "")
            { flag = "A"; }
            else
            { flag = "U"; }

            DataTable dt = new DataTable();
            string sqlprocpo = "exec [sp_TPU_INSERTPRODUCTIONUPDATE_HEADER_FOR_COMBO] '" + flag + "', '" + puid + "','" + poid + "','" + pono + "','" + prodid + "','" + prodname + "'," + orderqty + "," + rmianingqty + "," + actualqty + ",'" + tpuid + "','" + tpuname + "','" + remarks + "'," + HttpContext.Current.Session["UserID"].ToString() + ",'" + pudate + "','" + uom + "','" + uomid + "','" + batchno + "','" + HttpContext.Current.Session["FINYEAR"].ToString() + "','" + manfdate + "','" + expdate + "','" + Tag + "'," + TotalCasePack + ",'" + combo_id + "','" + combo_name + "'," + combo_order + ",'" + combo_packsize + "'";
            //int p = db.HandleData(sqlprocpo);
            dt = db.GetData(sqlprocpo);

            if (dt.Rows.Count > 0)
            {
                //saveflag = 1;
            }
            return dt;
        }

        public DataTable SavePUSheetDetails_Combo(string hdnfld, string puid, string poid, string pono, string prodid, string prodname, decimal orderqty, decimal rmianingqty, decimal actualqty, string tpuid, string tpuname, string remarks, string pudate, string uom, string uomid, string batchno, string manfdate, string expdate, string combo_id, string combo_name, decimal combo_order, string combo_packsize)
        {
            int saveflag = 0;
            string flag = "";
            if (hdnfld == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }

            DataTable dt = new DataTable();
            string sqlprocpo = "exec [sp_TPU_INSERTPRODUCTIONUPDATE_DETAILS_FOR_COMBO] '" + flag + "', '" + puid + "','" + poid + "','" + pono + "','" + prodid + "','" + prodname + "'," + orderqty + "," + rmianingqty + "," + actualqty + ",'" + tpuid + "','" + tpuname + "','" + remarks + "'," + HttpContext.Current.Session["UserID"].ToString() + ",'" + pudate.Trim() + "','" + uom + "','" + uomid + "','" + batchno + "','" + HttpContext.Current.Session["FINYEAR"].ToString() + "','" + manfdate.Trim() + "','" + expdate.Trim() + "','" + combo_id + "','" + combo_name + "'," + combo_order + ",'" + combo_packsize + "'";
            //int p = db.HandleData(sqlprocpo);
            dt = db.GetData(sqlprocpo);

            if (dt.Rows.Count > 0)
            {
                //saveflag = 1;
            }
            return dt;
        }

        public int DeleteProduction_Combo(string ProductionId)
        {
            int delflag = 0;

            string sql = "EXEC [SP_PRODUCTION_DELETE_COMBO] '" + ProductionId + "'";

            int e = db.HandleData(sql);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }
 public DataTable BindPU_COMBO(string puno)
        {
            //string sql = "select PONO,PRODUCTNAME, POTOTALQTY AS TOTALQTY, POREMAININGQTY AS REMAININGQTY,POUPDATEQTY AS ACTLQTY,UOM,UOMID,PUID, POID,  PRODUCTID,    POUPDATEDATE, TPUID, TPUNAME, REMARKS,  ISCLOSED, CLOSEDDATE, PUDATE from T_TPU_PRODUCTION_UPDATE WHERE  PUID='" + puno + "'";
            string sql = "exec [sp_TPU_EDITPRODUCTIONUPDATE_COMBO] '" + puno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

public DataTable BindPackingSize_ForCombo(string pid)
        {
            string sql_pack_pcs = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSize_pcs = (string)db.GetSingleValue(sql_pack_pcs);

            string sql = "SELECT DISTINCT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + pid + "' AND PSID='" + PackSize_pcs + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

 public decimal GetPOItemWiseRemainingQty_FOR_COMBO(string POID, string ProductID, string PacksizeID, string productionid, string TPU,decimal poqty)
        {
            decimal Qty = 0;

            if (TPU == "T")
            {

                Qty = (decimal)db.GetSingleValue("select dbo.GetPOItemWiseRemainingQty_FOR_COMBO('" + POID + "','" + ProductID + "','" + PacksizeID + "','" + productionid + "'," + poqty + ") ");
            }

            if (TPU == "F")
            {

                Qty = (decimal)db.GetSingleValue("select dbo.[GetPOItemWiseRemainingQty_FOR_COMBO]('" + POID + "','" + ProductID + "','" + PacksizeID + "','" + productionid + "'," + poqty + ") ");
            }

            return Qty;
        }
         public string Check_IsCombo_Item_by_puno(string puid)
         {

             string Sql = "  IF  EXISTS(SELECT 1 FROM dbo.T_TPU_PRODUCTION_DETAILS_BREAKUP WHERE  PUID='" + puid + "') BEGIN " +
                             " select '1'   END  else    begin	  select '0'   end ";


             return ((string)db.GetSingleValue(Sql));

         }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------//
        /*PRODUCTION FOR KKG START BY P.BASU ON 27-11-2020*/
        public DataTable LoadProduct(string TPUID)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "USP_LOAD_FG_PRODUCT_PRODUCTION";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable GetPO_QtyCombo(string ProductID, string VendorID, string Finyear, string Type)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_PRODUCTION_ORDER_DETAILS '" + ProductID + "','" + VendorID + "','" + Finyear + "','" + Type + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable GetProduction_QtyCombo(string ProductID, string VendorID, string Finyear, string Type)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_PRODUCTION_ORDER_DETAILS_v2 '" + ProductID + "','" + VendorID + "','" + Finyear + "','" + Type + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindFactoryProctuct(string PONO, string PUID, string MFGDATE, string ProductID)
        {
            string sql = "exec [sp_Factory_GETPRODUCTIONUPDATE_KKG] '" + PONO + "','" + PUID + "','" + MFGDATE + "','" + ProductID + "'";
            //string sql = "SELECT PONO,PRODUCTNAME,QTY AS TOTALQTY,CAST(QTY - (SELECT ISNULL(SUM(POUPDATEQTY),0) FROM T_TPU_PRODUCTION_UPDATE WHERE PONO='" + PONO + "') AS INT) AS REMAININGQTY,'0' as ACTLQTY FROM T_TPU_PODETAILS WHERE PONO='" + PONO + "'"; 
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMaxFactoryDatekkg(string poid) /*add by p.basu for kkg*/
        {
            string sql = "SELECT CONVERT(VARCHAR(8),MAX(ENTRYDATE),112) AS PODATE,CONVERT(VARCHAR(10),CAST(MAX(ENTRYDATE) AS DATE),103) AS SHOWPODATE FROM TBL_PRODUCTION_ORDER_HEADER WITH (NOLOCK) WHERE PRODUCTIONID IN(" + poid + ")";
            DataTable PoDATE = db.GetData(sql);
            return PoDATE;
        }


    }
}
