using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ClsUnlockInvoice
    {
        DBUtils db = new DBUtils();

        #region Unlock saleInvoice

        public DataTable BindInvoiceForUnlock(string fromdate, string todate, string finyear, string BSID, string depotid)//Sale Invoice 1
        {
            string SQL = string.Empty;
            SQL = "SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                 " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME," +
                 " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,ISNULL(A.GETPASSNO,'') AS GATEPASSNO" +
                 " FROM T_SALEINVOICE_HEADER AS A WITH(NOLOCK)	INNER JOIN T_SALEINVOICE_FOOTER AS B WITH (NOLOCK) ON  A.SALEINVOICEID=B.SALEINVOICEID" +
                 " WHERE CONVERT(DATE,SALEINVOICEDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103)" +
                 " AND A.BSID ='" + BSID + "' AND A.FINYEAR ='" + finyear + "' AND DEPOTID='" + depotid + "' AND ISVERIFIED='Y'" +
                 " ORDER BY A.SALEINVOICENO DESC";
            DataTable DT = db.GetData(SQL);
            return DT;

        }
        public DataTable BindTradingInvoiceForUnlock(string fromdate, string todate, string finyear, string BSID, string depotid)//Sale Invoice 1
        {
            string SQL = string.Empty;
            SQL = "SELECT A.SALEINVOICEID AS SALEINVOICEID, (A.SALEINVOICEPREFIX + '/' + A.SALEINVOICENO) AS SALEINVOICENO," +
                 " CONVERT(VARCHAR(10),CAST(A.SALEINVOICEDATE AS DATE),103) AS SALEINVOICEDATE,ISNULL(A.DISTRIBUTORNAME,'') AS DISTRIBUTORNAME," +
                 " ISNULL(B.TOTALSALEINVOICEVALUE,0) AS NETAMOUNT,ISNULL(A.GETPASSNO,'') AS GATEPASSNO" +
                 " FROM T_MM_SALEINVOICE_HEADER AS A WITH(NOLOCK)	INNER JOIN T_MM_SALEINVOICE_FOOTER AS B WITH (NOLOCK) ON  A.SALEINVOICEID=B.SALEINVOICEID" +
                 " WHERE CONVERT(DATE,SALEINVOICEDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103)" +
                 " AND A.BSID ='" + BSID + "' AND A.FINYEAR ='" + finyear + "' AND DEPOTID='" + depotid + "' AND ISVERIFIED='Y'" +
                 " ORDER BY A.SALEINVOICENO DESc";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public DataTable BindStockTranferForUnlock(string fromdate, string todate, string finyear, string depotid) //Stock Transfer 2
        {
            string SQL = string.Empty;
            SQL = " SELECT A.STOCKTRANSFERID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS SALEINVOICEDATE,STOCKTRANSFERNO AS SALEINVOICENO," +
                        " TODEPOTID,TODEPOTNAME AS DISTRIBUTORNAME,CHALLANNO AS GATEPASSNO,ISNULL(NETAMOUNT,0) AS NETAMOUNT" +
                        " FROM [T_STOCKTRANSFER_HEADER] A WITH (NOLOCK) INNER JOIN T_STOCKTRANSFER_FOOTER B " +
                        " ON A.STOCKTRANSFERID=B.STOCKTRANSFERID" +
                        " WHERE CONVERT(DATE,STOCKTRANSFERDATE,103) BETWEEN  " +
                       " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') " +
                       " AND FINYEAR ='" + finyear + "' AND MOTHERDEPOTID='" + depotid + "' AND ISVERIFIED='Y' " +
                       " ORDER BY STOCKTRANSFERDATE DESC";
            DataTable DT = db.GetData(SQL);
            return DT;

        }


        public DataTable BindPurchaseStockReceiptForUnlock(string fromdate, string todate, string finyear, string depotid)//Purchase Stock Receipt 3
        {
            string SQL = string.Empty;
            SQL = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE," +
                    " (A.STOCKRECEIVEDPREFIX+'/'+A.STOCKRECEIVEDNO) AS SALEINVOICENO ,A.TPUNAME AS DISTRIBUTORNAME,ISNULL(C.TOTALRECEIVEDVALUE,0) AS NETAMOUNT," +
                    " INVOICENO AS GATEPASSNO" +
                    " FROM T_STOCKRECEIVED_HEADER A INNER JOIN M_TPU_VENDOR B ON A.TPUID=B.VENDORID " +
                    " INNER JOIN T_STOCKRECEIVED_FOOTER C ON A.STOCKRECEIVEDID=C.STOCKRECEIVEDID WHERE " +
                    " CONVERT(DATE,A.STOCKRECEIVEDDATE,103) between " +
                    " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND A.MOTHERDEPOTID='" + depotid + "'" +
                    " AND ISVERIFIED='Y' " +
                    " AND B.TAG='T'" +
                    " ORDER BY A.STOCKRECEIVEDDATE ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public DataTable BindPurchaseReturnForUnlock(string fromdate, string todate, string finyear, string depotid)//Purchase Stock Receipt 3
        {
            string SQL = string.Empty;
            SQL = " SELECT A.PURCHASERETURNID AS SALEINVOICEID,CONVERT(VARCHAR(10), CAST(A.PURCHASERETURNDATE AS DATE), 103) AS SALEINVOICEDATE, " +
                    " (A.PURCHASERETURNPREFIX + '/' + A.PURCHASERETURNNO) AS SALEINVOICENO, A.VENDORNAME AS DISTRIBUTORNAME,ISNULL(C.NETAMOUNT, 0) AS NETAMOUNT," +
                    " INVOICENO AS GATEPASSNO " +
                    " FROM T_PURCHASERETURN_HEADER A INNER JOIN M_TPU_VENDOR B ON A.VENDORID = B.VENDORID " +
                    " INNER JOIN T_PURCHASERETURN_FOOTER C ON A.PURCHASERETURNID = C.PURCHASERETURNID " +
                    " WHERE CONVERT(DATE, A.PURCHASERETURNDATE,103) BETWEEN dbo.Convert_To_ISO('" + fromdate + "') AND dbo.Convert_To_ISO('" + todate + "') " +
                    " AND FINYEAR = '" + finyear + "' " +
                    " AND A.DEPOTID = '" + depotid + "' " +
                    " AND ISVERIFIED = 'Y' " +
                    " ORDER BY A.PURCHASERETURNDATE ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }


        public DataTable BindFactoryStockReceiptForUnlock(string fromdate, string todate, string finyear, string depotid)//Factory Stock Receipt
        {
            string SQL = string.Empty;
            SQL = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE," +
                    " (A.STOCKRECEIVEDPREFIX+'/'+A.STOCKRECEIVEDNO) AS SALEINVOICENO ,A.TPUNAME AS DISTRIBUTORNAME,ISNULL(C.TOTALRECEIVEDVALUE,0) AS NETAMOUNT," +
                    " INVOICENO AS GATEPASSNO" +
                    " FROM T_STOCKRECEIVED_HEADER A INNER JOIN M_TPU_VENDOR B ON A.TPUID=B.VENDORID AND B.SUPLIEDITEMID IN('1','2','3','4')" +
                    " INNER JOIN T_STOCKRECEIVED_FOOTER C ON A.STOCKRECEIVEDID=C.STOCKRECEIVEDID WHERE " +
                    " CONVERT(DATE,A.STOCKRECEIVEDDATE,103) between " +
                    " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND A.MOTHERDEPOTID='" + depotid + "' AND ISVERIFIED='Y' " +
               
                    " AND B.TAG='F'" +
                    " ORDER BY A.STOCKRECEIVEDDATE ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public DataTable BindDepoReceivedForUnlock(string fromdate, string todate, string finyear, string depotid)//Depot Stock Receipt 4
        {
            string SQL = string.Empty;
            SQL = "SELECT MOTHERDEPOTID,A.STOCKDEPORECEIVEDID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKDEPORECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE," +
                    " A.STOCKDEPORECEIVEDNO AS SALEINVOICENO ,A.RECEIVEDDEPOTNAME AS DISTRIBUTORNAME,C.NETAMOUNT AS NETAMOUNT," +
                    " ISNULL(CHALLANNO,'') AS GATEPASSNO " +
                    " FROM T_DEPORECEIVED_STOCK_HEADER A " +
                    " INNER JOIN T_DEPORECEIVED_STOCK_FOOTER C ON A.STOCKDEPORECEIVEDID=C.STOCKDEPORECEIVEDID WHERE " +
                    " CONVERT(DATE,A.STOCKDEPORECEIVEDDATE,103) between " +
                    " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND RECEIVEDDEPOTID='" + depotid + "' AND ISVERIFIED='Y' " +
                    " ORDER BY A.STOCKDEPORECEIVEDDATE   ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }
        public DataTable BindSaleReturnForUnlock(string fromdate, string todate, string finyear, string depotid, string BSID)//Sale Return 5
        {
            string SQL = string.Empty;
            SQL = " SELECT A.SALERETURNID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALEINVOICEDATE," +
                " (A.SALERETURNPREFIX+'/'+A.SALERETURNNO) AS SALEINVOICENO ,A.DISTRIBUTORNAME AS DISTRIBUTORNAME,ISNULL(C.NETAMOUNT,0) AS NETAMOUNT," +
                " ISNULL(GETPASSNO,'') AS GATEPASSNO" +
                " FROM T_SALERETURN_HEADER A " +
                " INNER JOIN T_SALERETURN_FOOTER C ON A.SALERETURNID=C.SALERETURNID WHERE " +
                " CONVERT(DATE,A.SALERETURNDATE,103) between " +
                " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND DEPOTID='" + depotid + "' AND BSID='" + BSID + "' AND ISVERIFIED='Y'" +

                "UNION ALL" +

                " SELECT A.SALERETURNID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) AS SALEINVOICEDATE," +
                " (A.SALERETURNPREFIX+'/'+A.SALERETURNNO) AS SALEINVOICENO ,A.DISTRIBUTORNAME AS DISTRIBUTORNAME,ISNULL(C.NETAMOUNT,0) AS NETAMOUNT," +
                " ISNULL(GETPASSNO,'') AS GATEPASSNO" +
                " FROM T_SALERETURN_HEADER_MM A " +
                " INNER JOIN T_SALERETURN_FOOTER_MM C ON A.SALERETURNID=C.SALERETURNID WHERE " +
                " CONVERT(DATE,A.SALERETURNDATE,103) between " +
                " dbo.Convert_To_ISO('" + fromdate + "') and  dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' AND DEPOTID='" + depotid + "' AND BSID='" + BSID + "' AND ISVERIFIED='Y'" +
                " ORDER BY CONVERT(VARCHAR(10),CAST(A.SALERETURNDATE AS DATE),103) ";
            DataTable DT = db.GetData(SQL);
            return DT;
        }


        public DataTable BindTransporterBill(string fromdate, string todate, string finyear, string depotid)//Transporter Bill Entry
        {
            string SQL = string.Empty;
            SQL = " SELECT A.TRANSPORTERBILLID AS SALEINVOICEID,TRANSPORTERBILLNO AS SALEINVOICENO," +
                  " CONVERT(VARCHAR(10),CAST( TRANSPORTERBILLDATE AS DATE),103) AS SALEINVOICEDATE," +
                  " ISNULL(TRANSPORTERID,'')AS TRANSPORTERID ,ISNULL(TRANSPORTERNAME,'') AS DISTRIBUTORNAME,ISNULL(B.TOTALBILLAMOUNT,0) AS NETAMOUNT, '' AS GATEPASSNO " +
                  " FROM T_TRANSPORTER_BILL_HEADER A INNER JOIN " +
                  " T_TRANSPORTER_BILL_FOOTER B ON A.TRANSPORTERBILLID=B.TRANSPORTERBILLID " +
                  " WHERE DEPOTID='" + depotid + "' AND CONVERT(DATE,TRANSPORTERBILLDATE  ,103) BETWEEN  " +
                  " CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103) AND FINYEAR='" + finyear + "' AND ISVERIFIED='Y' ORDER BY TRANSPORTERBILLDATE  ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public DataTable BindPurchaseBill(string fromdate, string todate, string finyear, string depotid)//Purchase Bill Factory
        {
            string SQL = string.Empty;
            SQL = " SELECT A.PUBID AS SALEINVOICEID ,PUBNO AS SALEINVOICENO,CONVERT(VARCHAR(10),CAST(A.ENTRYDATE AS DATE),103) AS SALEINVOICEDATE," +
                    " ISNULL(A.VENDORNAME,'') AS DISTRIBUTORNAME,SUM(NETAMT) AS  NETAMOUNT,'' AS GATEPASSNO " +
                    " FROM T_MMPURCHASEBILLMAKER_HEADER A INNER JOIN T_MMPURCHASEBILLMAKER_DETAILS  B ON A.PUBID=B.PUBID  " +
                    " WHERE CONVERT(DATE,A.ENTRYDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103) " +
                    " AND A.FINYEAR ='" + finyear + "' AND DEPOTID='" + depotid + "' AND ISVERIFIED='Y' " +
                    " GROUP BY A.PUBID,PUBNO,A.ENTRYDATE,A.VENDORNAME ORDER BY A.ENTRYDATE  ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }

        public DataTable BindStockDespatchForFactory(string fromdate, string todate, string finyear, string depotid)//Purchase Bill Factory
        {
            string SQL = string.Empty;
            SQL = " SELECT  A.STOCKDESPATCHID AS  SALEINVOICEID,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+ STOCKDESPATCHSUFFIX) AS SALEINVOICENO, " +
                  " CONVERT(VARCHAR(10),CAST(A.STOCKDESPATCHDATE AS DATE),103) AS SALEINVOICEDATE ," +
                  " MOTHERDEPOTNAME AS DISTRIBUTORNAME, " +
                  " B. TOTALDESPATCHVALUE AS  NETAMOUNT, INVOICENO AS GATEPASSNO  FROM " +
                  " T_STOCKDESPATCH_HEADER A INNER JOIN T_STOCKDESPATCH_FOOTER B ON A.STOCKDESPATCHID=B.STOCKDESPATCHID " +
                  " INNER  JOIN M_TPU_VENDOR C ON A.TPUID=C.VENDORID WHERE C.TAG='F' AND A.TPUID='" + depotid + "' AND A.FINYEAR='" + finyear + "' " +
                  " AND CONVERT(DATE,A.STOCKDESPATCHDATE  ,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103) AND ISVERIFIED='Y' " +
                  " ORDER BY STOCKDESPATCHDATE  ";
            DataTable DT = db.GetData(SQL);
            return DT;

        }


        public DataTable BindqcForFactory(string fromdate, string todate, string finyear, string depotid)// Factory QC
        {
            string SQL = string.Empty;
            SQL = " SELECT A.QCID AS SALEINVOICEID,QCNO AS SALEINVOICENO, CONVERT(VARCHAR(10),CAST(A.QCDATE AS DATE),103) AS SALEINVOICEDATE," +
                  " VENDORNAME AS DISTRIBUTORNAME,TOTALRECEIVEDVALUE AS NETAMOUNT,A.GRNNUMBER AS GATEPASSNO " +
                  " FROM T_MM_QUALITYCONTROL_HEADER AS A " +
                  " INNER JOIN T_STOCKRECEIVED_FOOTER AS B ON A.GRNID=B.STOCKRECEIVEDID " +
                  " WHERE CONVERT(DATE,QCDATE  ,103) BETWEEN CONVERT(DATE,'" + fromdate + "',103)" +
                  " AND CONVERT(DATE,'" + todate + "',103) AND A.FINYEAR ='" + finyear + "' " +
                  " AND FACTORYID='" + depotid + "' AND ISVERIFIED='Y' ";
            DataTable DT = db.GetData(SQL);
            return DT;
        }

        public DataTable BindForFactorychecker1(string fromdate, string todate, string finyear, string depotid)/* Factory ISVERIFIEDCHECKER1='Y'*/
        {
            string SQL = string.Empty;
            SQL = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE," +
                  " (A.STOCKRECEIVEDPREFIX+'/'+A.STOCKRECEIVEDNO+' '+A.STOCKRECEIVEDSUFFIX) AS SALEINVOICENO,A.TPUNAME" +
                  " AS DISTRIBUTORNAME,ISNULL(C.TOTALRECEIVEDVALUE,0) AS NETAMOUNT, INVOICENO AS GATEPASSNO " +
                  " FROM T_STOCKRECEIVED_HEADER AS A " +
                  " INNER JOIN T_STOCKRECEIVED_FOOTER AS C ON A.STOCKRECEIVEDID=C.STOCKRECEIVEDID " +
                  " WHERE CONVERT(DATE,A.STOCKRECEIVEDDATE,103) BETWEEN dbo.Convert_To_ISO('" + fromdate + "') " +
                  " AND dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' " +
                  " AND A.MOTHERDEPOTID='" + depotid + "' AND ISVERIFIEDCHECKER1='Y' AND ISVERIFIED='N'" +
                  " ORDER BY A.STOCKRECEIVEDDATE ";
            DataTable DT = db.GetData(SQL);
            return DT;
        }

        public DataTable BindForFactoryGrnStockIn(string fromdate, string todate, string finyear, string depotid)/* Factory ISVERIFIEDSTOCKIN='Y' */
        {
            string SQL = string.Empty;
            SQL = " SELECT A.STOCKRECEIVEDID AS SALEINVOICEID,CONVERT(VARCHAR(10),CAST(A.STOCKRECEIVEDDATE AS DATE),103) AS SALEINVOICEDATE," +
                  " (A.STOCKRECEIVEDPREFIX+'/'+A.STOCKRECEIVEDNO+' '+A.STOCKRECEIVEDSUFFIX) AS SALEINVOICENO,A.TPUNAME" +
                  " AS DISTRIBUTORNAME,ISNULL(C.TOTALRECEIVEDVALUE,0) AS NETAMOUNT, INVOICENO AS GATEPASSNO " +
                  " FROM T_STOCKRECEIVED_HEADER AS A " +
                  " INNER JOIN T_STOCKRECEIVED_FOOTER AS C ON A.STOCKRECEIVEDID=C.STOCKRECEIVEDID " +
                  " WHERE CONVERT(DATE,A.STOCKRECEIVEDDATE,103) BETWEEN dbo.Convert_To_ISO('" + fromdate + "') " +
                  " AND dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "' " +
                  " AND A.MOTHERDEPOTID='" + depotid + "' AND ISVERIFIEDSTOCKIN='Y' AND ISVERIFIEDCHECKER1='N' AND ISVERIFIED='N' " +
                  " ORDER BY A.STOCKRECEIVEDDATE    ";
            DataTable DT = db.GetData(SQL);
            return DT;
        }

        public DataTable BindBranch()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH ORDER BY BRPREFIX";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int SaveInvoice(string depoptid, string xml, string MODULEID, string IP_ADDRESS, string USERID)
        {
            int result = 0;
            string sql = "EXEC [USP_UNLOCK_INVOICE_UP] '" + depoptid + "','" + xml + "','" + MODULEID + "','" + IP_ADDRESS + "','" + USERID + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
        }

        public int unlockPo(string depoptid, string xml, string USERID,string finyear)
        {
            int result = 0;
            string sql = "EXEC [USP_UNLOCK_PURCHASE_ORDER] '" + depoptid + "','" + xml + "','" + USERID + "','"+ finyear + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
        }

        public DataTable LoadQualityControl(string fromdate, string todate, string finyear, string depotid)
        {
            string SQL = string.Empty;
            SQL = " SELECT A.QCID AS SALEINVOICEID,QCNO AS SALEINVOICENO, CONVERT(VARCHAR(10),CAST(QCDATE AS DATE),103) AS SALEINVOICEDATE,VENDORNAME AS DISTRIBUTORNAME,0 AS NETAMOUNT, A.GRNNUMBER AS GATEPASSNO " +
                  " FROM T_MM_QUALITYCONTROL_HEADER AS A " +
                  " INNER JOIN T_STOCKRECEIVED_HEADER AS B ON A.GRNID=B.STOCKRECEIVEDID" +
                  " WHERE CONVERT(DATE,QCDATE,103) BETWEEN CONVERT(DATE,'" + fromdate + "',103) AND CONVERT(DATE,'" + todate + "',103)" +
                  " AND A.FINYEAR ='" + finyear + "' " +
                  " AND FACTORYID='" + depotid + "' " +
                  " AND A.ISVERIFIED='Y' AND B.ISVERIFIEDSTOCKIN='N' AND B.ISVERIFIEDCHECKER1='N' ";
            DataTable DT = db.GetData(SQL);
            return DT;
        }

        public DataTable BindPurchaseReturnForFactory(string fromdate, string todate, string finyear, string depotid)//Purchase Return Factory
        {
            string SQL = string.Empty;
            SQL = " SELECT A.PURCHASERETURNID AS SALEINVOICEID,(PURCHASERETURNPREFIX+'/'+PURCHASERETURNNO+'/'+ PURCHASERETURNSUFFIX) AS SALEINVOICENO, " +
                  " CONVERT(VARCHAR(10),CAST(A.PURCHASERETURNDATE AS DATE),103) AS SALEINVOICEDATE," +
                  " A.VENDORNAME AS DISTRIBUTORNAME,NETAMOUNT, INVOICENO AS GATEPASSNO  " +
                  " FROM T_PURCHASERETURN_HEADER A INNER JOIN T_PURCHASERETURN_FOOTER B ON A.PURCHASERETURNID=B.PURCHASERETURNID " +
                  " INNER JOIN M_TPU_VENDOR C ON A.DEPOTID=C.VENDORID WHERE C.TAG='F' AND A.DEPOTID='" + depotid + "' AND A.FINYEAR='" + finyear + "' " +
                  " AND CONVERT(DATE,A.PURCHASERETURNDATE,103) BETWEEN  CONVERT(DATE,'" + fromdate + "',103) AND   CONVERT(DATE,'" + todate + "',103) AND ISVERIFIED='Y' " +
                  " ORDER BY PURCHASERETURNDATE  ";

            DataTable DT = db.GetData(SQL);
            return DT;
        }
        #endregion

        public string GetOfflineTag()
        {
            string Tag = string.Empty;
            string sql = "SELECT OFFLINE FROM P_APPMASTER";
            Tag = Convert.ToString(db.GetSingleValue(sql));
            return Tag;
        }
    }
}
