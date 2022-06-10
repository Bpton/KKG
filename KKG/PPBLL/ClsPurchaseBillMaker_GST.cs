using System;
using System.Data;
using System.Web;
using DAL;
using Utility;

namespace PPBLL
{
    public class ClsPurchaseBillMaker_GST
    {
        DBUtils db = new DBUtils();
        public DataTable BindVendor()
        {
            string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEM <>'FG' AND TAG='T' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurBill(string VendorID, string Fromdt, string Todt, string Checker, string Depotid, string FinYear)
        {
            string sql = "";
            /*if (Checker == "FALSE")
            {
                sql = " Select A.PUBID,A.PUBNO,CONVERT(VARCHAR(10),CAST(A.ENTRYDATE AS DATE),103) AS ENTRYDATE,SUM(B.NETAMT)AS NETAMT,SUM(B.BILLAMT)AS BILLAMT,0.00 AS ALREADYAMT, " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING' WHEN ISVERIFIED='R' THEN 'REJECT' WHEN ISVERIFIED='Y' THEN 'APPROVED' ELSE 'PENDING' END AS APPROVAL  FROM T_MMPURCHASEBILLMAKER_HEADER AS A" +
                      " INNER JOIN T_MMPURCHASEBILLMAKER_DETAILS AS B ON B.PUBID=A.PUBID" +
                      " WHERE A.VENDORID='" + VendorID + "' AND CONVERT(DATE,A.ENTRYDATE,103) BETWEEN DBO.Convert_To_ISO('" + Fromdt + "') AND DBO.Convert_To_ISO('" + Todt + "')" +
                    //" AND ISVERIFIED='N'" +
                      " GROUP BY A.PUBID,A.PUBNO,A.ENTRYDATE,A.ISVERIFIED";
            }
            else
            {
                sql = " Select A.PUBID,A.PUBNO,CONVERT(VARCHAR(10),CAST(A.ENTRYDATE AS DATE),103) AS ENTRYDATE,SUM(B.NETAMT)AS NETAMT,SUM(B.BILLAMT)AS BILLAMT,0.00 AS ALREADYAMT, " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING' WHEN ISVERIFIED='R' THEN 'REJECT' WHEN ISVERIFIED='Y' THEN 'APPROVED' ELSE 'PENDING' END AS APPROVAL FROM T_MMPURCHASEBILLMAKER_HEADER AS A" +
                      " INNER JOIN T_MMPURCHASEBILLMAKER_DETAILS AS B ON B.PUBID=A.PUBID" +
                      " WHERE A.VENDORID='" + VendorID + "' AND CONVERT(DATE,A.ENTRYDATE,103) BETWEEN DBO.Convert_To_ISO('" + Fromdt + "') AND DBO.Convert_To_ISO('" + Todt + "')  " +
                    //" AND ISVERIFIED<>'Y' " +
                      " GROUP BY A.PUBID,A.PUBNO,A.ENTRYDATE,A.ISVERIFIED";
            }*/
            sql = "EXEC USP_BIND_PURCHASEBILL '" + VendorID + "','" + Fromdt + "','" + Todt + "','" + Checker + "','" + Depotid + "','" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #region LoadGRN
        public DataTable BindReceived(string fromdate, string todate, string depotid, string finyear, string CheckerFlag, string UserID, string TPUID)
        {
            DataTable dtPURGRN = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_PURCHASEBILL_GRNDTLS_GST '" + fromdate + "','" + todate + "','" + depotid + "','" + finyear + "','" + CheckerFlag + "','" + UserID + "','" + TPUID + "'";
            dtPURGRN = db.GetData(sql);
            return dtPURGRN;
        }
        #endregion

        #region Save Purchase Bill Maker
        public string SAVEPURCHASEBILLMAKER(string PUBID, string ENTRYDATE, string REFNO, string REFDATE, string GRNNO, string GRNID, string GRNDATE,
                                            string VENDORID, string VENDORNAME, int CREATEDBY, string FINYEAR, string REMARKS,
                                            string DepotID, string DepotNAME, string XMLPUBDetails/*, string XmlPUBTax*/,
                                            string TDSID, decimal DeductableAmount, decimal Percentage, decimal DeductionAmount,
                                            decimal NetAmount,decimal TDS_DefaultAmt,decimal BasicAmt)                                                  
        {
            string sqlstr;
            string FLAG = "";
            string Pubid = string.Empty;
            string PubNo = string.Empty;
            DataTable dtPurchaseBillNO = new DataTable();
            if (PUBID == "")
            {
                FLAG = "A";
                try
                {
                    sqlstr = "EXEC USP_MM_PURCHASEBILLMAKER_GST '" + PUBID + "','" + ENTRYDATE + "','" + REFNO + "','" + REFDATE + "','" + GRNNO + "'," +
                             "'" + GRNID + "','" + GRNDATE + "','" + VENDORID + "','" + VENDORNAME + "'," + CREATEDBY + ",'" + FINYEAR + "','" + FLAG + "','" + REMARKS + "','" + DepotID + "','" + DepotNAME + "',''," +
                             "'" + XMLPUBDetails + "','" + TDSID + "','" + DeductableAmount + "','" + Percentage + "','" + DeductionAmount + "','" + NetAmount + "','" + TDS_DefaultAmt +"','" + BasicAmt + "' ";
                    dtPurchaseBillNO = db.GetData(sqlstr);

                    if (dtPurchaseBillNO.Rows.Count > 0)
                    {
                        Pubid = dtPurchaseBillNO.Rows[0]["PUBID"].ToString();
                        PubNo = dtPurchaseBillNO.Rows[0]["PUBNO"].ToString();
                    }
                    else
                    {
                        PubNo = "";
                    }
                }
                catch (Exception ex)
                {
                    CreateLogFiles Errlog = new CreateLogFiles();
                    Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
                }
            }
            else
            {
                FLAG = "U";
                try
                {
                    sqlstr = "EXEC USP_MM_PURCHASEBILLMAKER_GST '" + PUBID + "','" + ENTRYDATE + "','" + REFNO + "','" + REFDATE + "','" + GRNNO + "'," +
                             "'" + GRNID + "','" + GRNDATE + "','" + VENDORID + "','" + VENDORNAME + "'," + CREATEDBY + ",'" + FINYEAR + "','" + FLAG + "','" + REMARKS + "','" + DepotID + "','" + DepotNAME + "',''," +
                             "'" + XMLPUBDetails + "'";
                    dtPurchaseBillNO = db.GetData(sqlstr);

                    if (dtPurchaseBillNO.Rows.Count > 0)
                    {
                        Pubid = dtPurchaseBillNO.Rows[0]["PUBID"].ToString();
                        PubNo = dtPurchaseBillNO.Rows[0]["PUBNO"].ToString();
                    }
                    else
                    {
                        PubNo = "";
                    }
                }
                catch (Exception ex)
                {
                    CreateLogFiles Errlog = new CreateLogFiles();
                    Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
                }

            }
            return PubNo;
        }
        #endregion

        #region
        public DataSet EditPUBILL(string PUBID, string FINYEAR, decimal ExchangeRate)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_MM_PURCHASEBILLMAKER_GST_EDIT '" + PUBID + "','" + FINYEAR + "'," + ExchangeRate + "";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        #endregion

        #region 
        public int DeletePUBILL(string PUBID, string FINYEAR)
        {
            int delflag = 0;
            string sqleditedprocdetail = "exec [USP_MM_PURCHASEBILLMAKER_DELETE] '" + PUBID + "','" + FINYEAR + "'";
            int e = db.HandleData(sqleditedprocdetail);

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

        #endregion

        #region CheckVerify
        public string CheckVerify(string MMPUBID)
        {
            string Flag = "";
            string sql = "SELECT ISVERIFIED FROM T_MMPURCHASEBILLMAKER_HEADER WHERE PUBID = '" + MMPUBID + "' ";
            Flag = (string)db.GetSingleValue(sql);
            return Flag;
        }
        #endregion

        #region LoadGRN
        public DataSet BindGrnTax(string StockReceivedID)
        {
            DataSet DsPURTAX = new DataSet();
            string sql = string.Empty;
            sql = "EXEC USP_PURCHASEBILL_GRN_TAX '" + StockReceivedID + "'";
            DsPURTAX = db.GetDataInDataSet(sql);
            return DsPURTAX;
        }
        #endregion
        #region TDS
        public DataTable TaxApplicable(string AccountID, string VoucherDate, string Finyear, decimal Amount, string BranchID, string VoucherType, string DrCr, decimal CalAmt)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "EXEC USP_ACC_TAXAPPLICALE_TDS '" + AccountID + "','" + VoucherDate + "','" + Finyear + "'," + Amount + ",'" + BranchID + "','" + VoucherType + "','" + DrCr + "','" + CalAmt + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :QUERY Error");
            }
            return dt1;
        }
        #endregion
        #region CheckVerify
        public int TDSVerify(string MMPUBID)
        {
            int Flag = 0;
            string sql = "EXEC USP_CHECK_TDS '" + MMPUBID + "' ";
            Flag = (int)db.GetSingleValue(sql);
            return Flag;
        }
        #endregion
    }
}
