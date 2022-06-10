using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class ClsSaleorderMM
    {
        DBUtils db = new DBUtils();
        DataTable dtsaleorderrecord = new DataTable();  // Datatable for adding gridproduct

        public DataTable LoadSale(string fromdate, string todate, string bsid, string DepotID, string FinYear)
        {
            string sql = "EXEC USP_BIND_SALEORDER_FAC '" + fromdate + "','" + todate + "','" + bsid + "','" + DepotID + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string FetchGroup(string cusid)
        {
            string sql = string.Empty;
            string groupid = string.Empty;

            sql = "SELECT GROUPID FROM [M_CUSTOMER] WHERE CUSTOMERID='" + cusid + "'";
            groupid = (string)db.GetSingleValue(sql);

            return groupid;
        }

        public DataTable BindBUSINESSSEGMENT()
        {

            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT";
                dt  = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }
        public DataTable BindCustomer(string bsID, string groupid, string FactoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC USP_BIND_CUSTOMER_MMPP '" + FactoryID + "','" + bsID + "','" + groupid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }

            return dt;
        }
        public DataTable GetContractRate(string CustomerID, string ProductID)
        {
            string sql = " SELECT ISNULL(RATE,0) AS RATE FROM M_MM_CUSTOMER_RATESHEET WHERE CUSTOMERID='" + CustomerID + "' AND PRODUCTID='" + ProductID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetTradingSaleRate(string CustomerID, string ProductID,string date)/*new add by p.basu on 25-12-2020 for tading sale rate*/
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "EXEC USP_FETCH_RATE_FOR_TADINGSALE '"+ CustomerID + "','"+ ProductID + "','"+date+"','"+finyear+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct(string mode ,string FactoryID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "USP_LOAD_PRODUCT_TRADING_SALE_INVOCIE_KKG '"+mode+"','"+ FactoryID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }
        public DataTable BindGroup(string BSID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "SELECT DIS_CATID,DIS_CATNAME FROM dbo.M_DISTRIBUTER_CATEGORY WHERE BUSINESSSEGMENTID='" + BSID + "' ORDER BY DIS_CATNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dt;
        }
        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT UOMID,UOMNAME FROM M_UOM WHERE UOMID NOT IN('21BC00F3-2D4F-497F-BF52-5A8324DD0C37','972E37AF-2A3C-4702-988A-7693E5B26081') ORDER BY UOMNAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetPackingSize_OnCallPCS(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;

            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;
        }
        public DataTable BindSaleOrderGrid()
        {
            //---------- Create DataTable For Add Product which is show in GridView ----------- //

            dtsaleorderrecord.Clear();
            dtsaleorderrecord.Columns.Add("PRODUCTID");
            dtsaleorderrecord.Columns.Add("PRODUCTNAME");
            dtsaleorderrecord.Columns.Add("ORDERQTY");
            dtsaleorderrecord.Columns.Add("UOMID");
            dtsaleorderrecord.Columns.Add("UOMNAME");
            dtsaleorderrecord.Columns.Add("REQUIREDDATE");
            dtsaleorderrecord.Columns.Add("REQUIREDTODATE");
            dtsaleorderrecord.Columns.Add("RATE");
            dtsaleorderrecord.Columns.Add("AMOUNT");
            dtsaleorderrecord.Columns.Add("DISCPER");
            dtsaleorderrecord.Columns.Add("DISCAMNT");

            HttpContext.Current.Session["SALEORDERRECORDS"] = dtsaleorderrecord;
            return dtsaleorderrecord;
        }
        public int SaleOrderRecordsCheck(string PID, string PDATE, string PTODATE)
        {
            int flag = 0;
            dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
            foreach (DataRow dr in dtsaleorderrecord.Rows)
            {
                if (dr["PRODUCTID"].ToString() == PID && dr["REQUIREDDATE"].ToString() == PDATE && dr["REQUIREDTODATE"].ToString() == PTODATE)
                {
                    flag = 1;
                    break;
                }
            }
            return flag;
        }
        public DataTable BindSaleOrderGridRecords(string PID, string PNAME, decimal PQTY, string PPACKINGSIZE, string PPACKINGSIZENAME, string PDATE, string PTODATE, decimal RATE, decimal DISCOUNT, decimal AMOUNT)
        {
            decimal discAmnt = 0;
            discAmnt = ((AMOUNT * DISCOUNT) / 100);
            dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
            DataRow dr = dtsaleorderrecord.NewRow();
            dr["PRODUCTID"] = PID;
            dr["PRODUCTNAME"] = PNAME;
            dr["ORDERQTY"] = PQTY;
            dr["UOMID"] = PPACKINGSIZE;
            dr["UOMNAME"] = PPACKINGSIZENAME;
            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;
            dr["RATE"] = RATE;
            dr["DISCPER"] = DISCOUNT;
            dr["DISCAMNT"] = discAmnt;
            dr["AMOUNT"] = (AMOUNT- discAmnt);
            dtsaleorderrecord.Rows.Add(dr);

            return dtsaleorderrecord;
        }
        public string InsertSaleOrderDetails(string saleorderdate, string refsaleorderno, string refsaleorderdate, string bsid, string bsname, string groupid,
                                             string groupname, string customerid, string customername, string remarks, string createdby, string hdnvalue,
                                             string finyear, string xml, string active, string strTermsID, string CurrencyID, string CurrencyName, string MRPTag,
                                             string DlvTermsID, string DlvTermsName, string icds, string icdsdate, decimal TotalCase, decimal TotalPCS, string DepotId)
        {
            string flag = "";
            string saleorderno = string.Empty;

            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            try
            {
                string sqlprocqc = " EXEC [USP_T_SALEORDER_MM_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + saleorderdate + "','" + refsaleorderno + "','" + refsaleorderdate + "'," +
                                   " '" + bsid + "','" + bsname + "','" + groupid + "','" + groupname + "','" + customerid + "','" + customername + "', '" + remarks + "'," +
                                   " '" + createdby + "','" + finyear + "','" + xml + "','" + active + "','" + strTermsID + "','" + CurrencyID + "','" + CurrencyName + "'," +
                                   " '" + MRPTag + "','" + DlvTermsID + "','" + DlvTermsName + "','" + icds + "','" + icdsdate + "'," + TotalCase + "," + TotalPCS + ",'" + DepotId + "'";
                saleorderno = (string)db.GetSingleValue(sqlprocqc);
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return saleorderno;
        }
        public void ResetDataTables()
        {
            HttpContext.Current.Session["SALEORDERRECORDS"] = null;
            dtsaleorderrecord.Clear();
        }
        public DataTable FetchSaleOrderHeader(string soid)
        {
            string sql = " SELECT CONVERT(VARCHAR(10),CAST(SALEORDERDATE AS DATE),103) AS SALEORDERDATE,REFERENCESALEORDERNO,REFERENCESALEORDERDATE,BSID,GROUPID,CUSTOMERID," +
                         " CUSTOMERNAME,REMARKS,ISCANCELLED,DELIVERYTERMSID,DELIVERYTERMSNAME,ISNULL(TOTALPCS,0) AS TOTALPCS FROM T_MM_SALEORDER_HEADER WHERE SALEORDERID='" + soid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchSaleOrderDetails(string soid)
        {
            string sql = "SELECT [PRODUCTID],[PRODUCTNAME],[ORDERQTY],[UOMID],[UOMNAME] AS UOMNAME,RATE,AMOUNT,[REQUIREDFROMDATE] AS REQUIREDDATE,[REQUIREDTODATE],ISNULL(DISCPER,0) [DISCPER],ISNULL(DISCAMNT,0) [DISCAMNT] FROM [T_MM_SALEORDER_DETAILS] WHERE SALEORDERID='" + soid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteSaleOrderHeader(string saleorderid)
        {
            int delflag = 0;
            string sqleditedprocdetail = "EXEC [USP_SALEORDERDELETE_MM] '" + saleorderid + "'";
            int e = db.HandleData(sqleditedprocdetail);
            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else if (e == -1)
            {
                delflag = -1;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }
            return delflag;
        }
        public DataTable BindTerms(string menuID)
        {
            string sql = " SELECT A.ID,A.DESCRIPTION" +
                         " FROM M_TERMSANDCONDITIONS AS A " +
                         " INNER JOIN M_TERMS_MENU_MAPPING AS B ON A.ID = B.TERMSID" +
                         " WHERE A.ISAPPROVED='Y'" +
                         " AND A.ISDELETED = 'N'" +
                         " AND B.MENUID = '" + menuID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTerms()
        {

            string sql = " SELECT A.ID,A.DESCRIPTION" +
                         " FROM M_TERMSANDCONDITIONS AS A " +
                         " INNER JOIN M_TERMS_MENU_MAPPING AS B ON A.ID = B.TERMSID" +
                         " WHERE A.ISAPPROVED='Y'" +
                         " AND A.ISDELETED = 'N'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable EditTerms(string SALEORDERID)
        {
            string sql = "SELECT SALEORDERID,TERMSID FROM T_MM_SALEORDER_TERMS WHERE SALEORDERID = '" + SALEORDERID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDeliveryTerms()
        {
            string sql = "SELECT ID,NAME FROM M_DELIVERYTERMS ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
