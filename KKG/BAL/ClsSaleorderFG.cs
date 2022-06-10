using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class ClsSaleorderFG
    {
        DBUtils db = new DBUtils();
        DataTable dtsaleorderrecord = new DataTable();  // Datatable for adding gridproduct

        public DataTable LoadSale(string fromdate, string todate, string bsid, string DepotID, string FinYear)
        {
            string sql = "EXEC [USP_BIND_SALEORDER_FG_FAC] '" + fromdate + "','" + todate + "','" + bsid + "','" + DepotID + "','" + FinYear + "'";
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

        public string BindBUSINESSSEGMENT(string bsid)
        {
            string sql = string.Empty;
            string bsname = string.Empty;
            try
            {
                sql = "SELECT BSNAME FROM M_BUSINESSSEGMENT where BSID='" + bsid + "'";
                bsname = (string)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return bsname;
        }

        public DataTable BindCustomer(string bsID, string groupid, string FactoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC USP_BIND_CUSTOMER_MMPP_FG '" + FactoryID + "','" + bsID + "','" + groupid + "'";
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

        public DataTable BindProduct(string FactoryID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = " SELECT A.ID AS PRODUCTID,A.PRODUCTALIAS AS PRODUCTNAME" +
                      " FROM M_PRODUCT AS A" +
                      " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.ID=B.PRODUCTID" +
                      " WHERE A.[TYPE] IN ('FG') " +
                      " AND B.VENDORID='" + FactoryID + "'" +
                      " ORDER BY NAME";
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
            string sql = "SELECT UOMID AS PACKSIZEID_FROM,UOMNAME AS PACKSIZEName_FROM FROM [M_PRODUCT] WHERE ID='" + PID + "'";
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
            dr["AMOUNT"] = AMOUNT;
            dtsaleorderrecord.Rows.Add(dr);

            return dtsaleorderrecord;
        }
        public string InsertSaleOrderDetails(string saleorderdate, string refsaleorderno, string refsaleorderdate, string bsid, string bsname, string groupid,
                                             string groupname, string customerid, string customername, string remarks, string createdby, string hdnvalue,
                                             string finyear, string xml, string active, string strTermsID, string CurrencyID, string CurrencyName, string MRPTag,
                                             string DlvTermsID, string DlvTermsName, string icds, string icdsdate, decimal TotalCase, decimal TotalPCS, string DepotId, string Entry_From)
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
                string sqlprocqc = " EXEC [USP_T_SALEORDER_FG_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + saleorderdate + "','" + refsaleorderno + "','" + refsaleorderdate + "'," +
                                   " '" + bsid + "','" + bsname + "','" + groupid + "','" + groupname + "','" + customerid + "','" + customername + "', '" + remarks + "'," +
                                   " '" + createdby + "','" + finyear + "','" + xml + "','" + active + "','" + strTermsID + "','" + CurrencyID + "','" + CurrencyName + "'," +
                                   " '" + MRPTag + "','" + DlvTermsID + "','" + DlvTermsName + "','" + icds + "','" + icdsdate + "'," + TotalCase + "," + TotalPCS + ",'" + DepotId + "','" + Entry_From + "'";
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
            string sql = "SELECT [PRODUCTID],[PRODUCTNAME],[ORDERQTY],[UOMID],[UOMNAME] AS UOMNAME,RATE,AMOUNT,[REQUIREDFROMDATE] AS REQUIREDDATE,[REQUIREDTODATE] FROM [T_MM_SALEORDER_DETAILS] WHERE SALEORDERID='" + soid + "'";
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
            string sql = "SELECT SALEORDERID,TERMSID FROM  T_MM_SALEORDER_TERMS  WHERE SALEORDERID = '" + SALEORDERID + "' ";
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
        public DataTable BindNRGPGrid()
        {
            //---------- Create DataTable For Add Product which is show in GridView ----------- //
            dtsaleorderrecord.Clear();
            dtsaleorderrecord.Columns.Add("PRODUCTID");
            dtsaleorderrecord.Columns.Add("PRODUCTNAME");
            dtsaleorderrecord.Columns.Add("QTY");
            dtsaleorderrecord.Columns.Add("UOMID");
            dtsaleorderrecord.Columns.Add("UOMNAME");
            dtsaleorderrecord.Columns.Add("RATE");
            dtsaleorderrecord.Columns.Add("AMOUNT");
            dtsaleorderrecord.Columns.Add("STORELOCATIONID");
            dtsaleorderrecord.Columns.Add("STORELOCATIONNAME");
            dtsaleorderrecord.Columns.Add("TOSTORELOCATIONID");

            dtsaleorderrecord.Columns.Add("IGSTID");
            dtsaleorderrecord.Columns.Add("IGSTPER");
            dtsaleorderrecord.Columns.Add("IGSTAMNT");

            dtsaleorderrecord.Columns.Add("CGSTID");
            dtsaleorderrecord.Columns.Add("CGSTPER");
            dtsaleorderrecord.Columns.Add("CGSTAMNT");

            dtsaleorderrecord.Columns.Add("SGSTID");
            dtsaleorderrecord.Columns.Add("SGSTPER");
            dtsaleorderrecord.Columns.Add("SGSTAMNT");

            dtsaleorderrecord.Columns.Add("TOTALAMNT");

            HttpContext.Current.Session["NRGPRECORDS"] = dtsaleorderrecord;
            return dtsaleorderrecord;
        }

        public DataTable BindNRGPGridRecords(string PID, string PNAME, decimal PQTY, string PPACKINGSIZE, string PPACKINGSIZENAME, decimal RATE, decimal AMOUNT, 
                                             string STORELOCATIONID, string STORELOCATIONNAME, string TOSTORELOCATIONID,string IGSTID,decimal IGSTPER,string CGSTID,
                                             decimal CGSTPER,string SGSTID,decimal SGSTPER)
        {
            decimal IGSTAMNT = ((AMOUNT * IGSTPER)/100);
            decimal CGSTAMNT = ((AMOUNT * CGSTPER) / 100);
            decimal SGSTAMNT = ((AMOUNT * SGSTPER) / 100);
                
            dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];
            DataRow dr = dtsaleorderrecord.NewRow();
            dr["PRODUCTID"] = PID;
            dr["PRODUCTNAME"] = PNAME;
            dr["QTY"] = PQTY;
            dr["UOMID"] = PPACKINGSIZE;
            dr["UOMNAME"] = PPACKINGSIZENAME;
            dr["RATE"] = RATE;
            dr["AMOUNT"] = AMOUNT;
            dr["STORELOCATIONID"] = STORELOCATIONID;
            dr["STORELOCATIONNAME"] = STORELOCATIONNAME;
            dr["TOSTORELOCATIONID"] = TOSTORELOCATIONID;

            dr["IGSTID"] = IGSTID;
            dr["IGSTPER"] = IGSTPER;
            dr["IGSTAMNT"] = IGSTAMNT;

            dr["CGSTID"] = CGSTID;
            dr["CGSTPER"] = CGSTPER;
            dr["CGSTAMNT"] = CGSTAMNT;

            dr["SGSTID"] = SGSTID;
            dr["SGSTPER"] = SGSTPER;
            dr["SGSTAMNT"] = SGSTAMNT;

            dr["TOTALAMNT"] =(SGSTAMNT+ CGSTAMNT+ IGSTAMNT+ AMOUNT);

            dtsaleorderrecord.Rows.Add(dr);

            return dtsaleorderrecord;
        }
        public int NRGPRecordsCheck(string PID)
        {
            int flag = 0;
            dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];
            foreach (DataRow dr in dtsaleorderrecord.Rows)
            {
                if (dr["PRODUCTID"].ToString() == PID)
                {
                    flag = 1;
                    break;
                }
            }
            return flag;
        }
        public DataTable LoadNRGP(string fromdate, string todate, string DepotID, string FinYear,string mode)
        {
            string sql = "EXEC [USP_BIND_SEARCH_NRGP] '" + fromdate + "','" + todate + "','" + DepotID + "','" + FinYear + "','"+ mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Editnrgp(string NRGPID, string MODE, string ISRGP)
        {
            string sql = "EXEC [USP_BIND_NRGP_EDIT] '" + NRGPID + "','" + MODE + "','" + ISRGP + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable EntryDatecheck(string NRGPID)
        {
            string sql = "EXEC [USP_CHECK_DATE] '" + NRGPID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable CheckStockQty(string PRODUCTID, string DEPOTID, string STORELOCATIONID)
        {
            string sql = "EXEC [USP_RPT_STOCT_CHECKING] '" + PRODUCTID + "','" + DEPOTID + "','" + STORELOCATIONID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteNRGP(string saleorderid)
        {
            int delflag = 0;
            string sqleditedprocdetail = "EXEC [USP_NRGPDELETE] '" + saleorderid + "'";
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
        public string InsertNRGPDetails(string nrgpdate, string transportermode, string name, string gatepassno, string gatepassdate, string remarks,
                                         string createdby, string finyear, string xml, string hdnvalue,
                                        decimal TotalCase, decimal TotalPCS, string DepotId, string EntryFrom, string lrgrno, string vechicleno, string transport, string PLACEOFDELIVERY, string gstni)
        {
            string flag = "";
            string salenrgpno = string.Empty;

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
                string sqlprocqc = " EXEC [USP_NRGP_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + nrgpdate + "','" + transportermode + "','" + name + "'," +
                                   " '" + gatepassno + "','" + gatepassdate + "','" + remarks + "','" + createdby + "','" + finyear + "'," +
                                   " '" + xml + "','" + TotalCase + "','" + TotalPCS + "','" + DepotId + "','" + EntryFrom + "','" + lrgrno + "','" + vechicleno + "','" + transport + "','" + PLACEOFDELIVERY + "','" + gstni + "'";
                salenrgpno = (string)db.GetSingleValue(sqlprocqc);
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return salenrgpno;
        }

        public string InsertRGPDetails(string nrgpdate, string transportermode, string name, string gatepassno, string gatepassdate, string remarks,
                                        string createdby, string finyear, string xml, string hdnvalue,
                                       decimal TotalCase, decimal TotalPCS, string DepotId, string EntryFrom, string lrgrno, string vechicleno, string transport, 
                                       string PLACEOFDELIVERY, string gstni,string partyid,string partytag )
        {
            string flag = "";
            string salenrgpno = string.Empty;

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
                string sqlprocqc = " EXEC [USP_RGP_INSERT_UPDATE] '" + hdnvalue + "','" + flag + "','" + nrgpdate + "','" + transportermode + "','" + name + "'," +
                                   " '" + gatepassno + "','" + gatepassdate + "','" + remarks + "','" + createdby + "','" + finyear + "'," +
                                   " '" + xml + "','" + TotalCase + "','" + TotalPCS + "','" + DepotId + "','" + EntryFrom + "','" + lrgrno + "','" + vechicleno + "','" + transport + "'," +
                                   "'" + PLACEOFDELIVERY + "','" + gstni + "','"+ partyid + "','"+ partytag + "'";
                salenrgpno = (string)db.GetSingleValue(sqlprocqc);
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return salenrgpno;
        }
        public DataTable BindNRGPDetails(string NRGPID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_NRGP_DETAILS] '" + NRGPID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNRGPHeader(string NRGPID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_NRGP_HEADER] '" + NRGPID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_CompanyInfo()
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_COMPANY_INFO]";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_CompanyInfofactoryid(string poid) /*NEW SP ADD FOR FACTORY WISE PRINT OUT*/
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_COMPANY_INFO_FOR_FACTORYWISE] '" + poid + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindREQSDetails(string REQSID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_REQUISITION_DETAILS] '" + REQSID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindREQSHeader(string REQSID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_REQUISITION_HEADER] '" + REQSID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductbySItemWise(string SuppliedItem, string FactoryId)
        {
            string sql = "EXEC [BIND_PRODUCT_SUPPLIEDITEM_WISE]  '" + SuppliedItem + "','" + FactoryId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable bindparty()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable bindtransporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStoreLocation(string ProductID)
        {
            string sql = "EXEC BIND_STORELOCATION_PRODUCTWISE '" + ProductID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*new add for tax mapping by p.basu on 19-10-2021*/
        public DataTable bindTax(string date,string deptid,string vendorid,string productid)
        {
            string sql = "EXEC USP_GET_TAX_DETAILS '" + date + "','" + deptid + "','" + vendorid + "','" + productid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

    }
}
