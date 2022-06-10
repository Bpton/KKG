﻿using DAL;
using System;
using System.Data;
using System.Web;

namespace BAL
{
    public class ClsDepoReceived_FAC
    {
        DBUtils db = new DBUtils();
        DataTable dtdeporeceived = new DataTable();  // Datatable for holding depo received record

        public DataTable BindReason(string MenuID)
        {
            string sql = " SELECT A.ID,A.NAME AS DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON" +
                         " A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N' AND A.DEBITEDTOID <> '2' AND A.STORELOCATIONID <> '0' AND B.MENUID='" + MenuID + "' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotBasedOnUser(string UserID)
        {
            string sql = " SELECT BRID,BRNAME FROM M_BRANCH AS A" +
                         " INNER JOIN M_TPU_USER_MAPPING AS B ON A.BRID=B.TPUID " +
                         " AND B.USERID='" + UserID + "' " +
                         " AND A.BRANCHTAG='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepo()
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG = 'D' ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransferNo(string ReceivedDepot)
        {

            string sql = "EXEC USP_BindTransferNo '" + ReceivedDepot + "','" + Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim() + "'";

            //string sql = " SELECT STOCKTRANSFERID,STOCKTRANSFERNO AS STOCKTRANSFERNO FROM [T_STOCKTRANSFER_HEADER]" +
            //             " WHERE STOCKTRANSFERID NOT IN(SELECT STOCKTRANSFERID FROM T_DEPORECEIVED_STOCK_HEADER)" +
            //             " AND TODEPOTID ='" + ReceivedDepot + "'" +
            //             " AND FINYEAR='" + Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim() + "'" +
            //             " ORDER BY STOCKTRANSFERNO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindInsurancename()
        {
            string sql = "SELECT ID,COMPANY_NAME FROM M_INSURANCECOMPANY ORDER BY COMPANY_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindEditedTransferNo(string ReceivedDepot)
        {
            string sql = "EXEC USP_BindEditedTransferNo '" + ReceivedDepot +"','"+ Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim() + "'";
            //string sql = " SELECT STOCKTRANSFERID,STOCKTRANSFERNO AS STOCKTRANSFERNO" +
            //             " FROM [T_STOCKTRANSFER_HEADER]" +
            //             " WHERE TODEPOTID='" + ReceivedDepot + "'" +
            //             " AND FINYEAR='" + Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim() + "'" +
            //             " ORDER BY STOCKTRANSFERNO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindTransferInfo(string TransferID)
        {
            DataSet ds = new DataSet();
            string sql = "[USP_STOCKDESPATCH_RECEIPT_DETAILS] '" + TransferID + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "Header";
            ds.Tables[1].TableName = "Details";
            ds.Tables[2].TableName = "TaxCountDetails";
            ds.Tables[3].TableName = "ProductTaxDetails";
            ds.Tables[4].TableName = "FooterDetails";
            return ds;
        }
        public DataSet BindTransferReceivedInfo(string DepotReceivedID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_DEPOT_RECEIVED_DETAILS '" + DepotReceivedID + "'";
            ds = db.GetDataInDataSet(sql);
            ds.Tables[0].TableName = "Header";
            ds.Tables[1].TableName = "Details";
            ds.Tables[2].TableName = "RejectionDetails";
            ds.Tables[3].TableName = "TaxCountDetails";
            ds.Tables[4].TableName = "ProductTaxDetails";
            ds.Tables[5].TableName = "FooterDetails";
            return ds;
        }
        public DataTable BindTransferProductDetails(string transferid)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT [STOCKTRANSFERID],[PRODUCTID],[PRODUCTNAME],[PACKINGSIZEID],[PACKINGSIZENAME],[BATCHNO],[TRANSFERQTY] FROM [T_STOCKTRANSFER_DETAILS] WHERE STOCKTRANSFERID='" + transferid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSTockQTYBasedONBATCHNO(string depotid, string pid, string batchno, string TOPACKSIZEID, string transferid)
        {
            //string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            //string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            //string sql = "SELECT BS.BATCHNO,ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + RETURNPACKSIZE + "','" + TOPACKSIZEID + "',BS.STOCKQTY),0) AS STOCKQTY," +
            //    "ISNULL(DBO.GetPackingSize_OnCall('" + pid + "',SD.PACKINGSIZEID,'" + TOPACKSIZEID + "',SD.TRANSFERQTY),0) AS TRANSFERQTY," +
            //    "CAST(ABS(ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + RETURNPACKSIZE + "','" + TOPACKSIZEID + "',BS.STOCKQTY),0) " +
            //    "+ ISNULL(DBO.GetPackingSize_OnCall('" + pid + "',SD.PACKINGSIZEID,'" + TOPACKSIZEID + "',SD.TRANSFERQTY),0)) AS VARCHAR) AS EDITEDSTOCKQTY" +
            //    " FROM [T_BATCHWISEAVAILABLE_STOCK] AS BS " +
            //    "LEFT OUTER JOIN [T_STOCKTRANSFER_DETAILS] SD ON BS.PRODUCTID=SD.PRODUCTID AND BS.BATCHNO=SD.BATCHNO AND SD.STOCKTRANSFERID='" + transferid + "' WHERE BS.BRID='" + depotid + "' AND BS.PRODUCTID='" + pid + "' AND BS.BATCHNO='" + batchno + "'";

            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','" + pid + "','" + TOPACKSIZEID + "','" + batchno + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBatchno(string depotid, string pid, string transferid)
        {
            //string sql = "SELECT BATCHNO,BATCHNO + '|' + CAST(STOCKQTY AS VARCHAR) AS BATCHSTOCKQTY FROM [T_BATCHWISEAVAILABLE_STOCK] WHERE BRID='" + depotid + "' AND PRODUCTID='" + pid + "'";

            //string sql = "SELECT BS.BATCHNO,BS.STOCKQTY,ISNULL(SD.TRANSFERQTY,0) AS TRANSFERQTY,CAST(ABS(BS.STOCKQTY + ISNULL(SD.TRANSFERQTY,0)) AS VARCHAR) AS EDITEDSTOCKQTY," +
            //             "BS.BATCHNO + '#' + CAST(BS.STOCKQTY AS VARCHAR) + '#' + CAST(ISNULL(SD.TRANSFERQTY,0) AS VARCHAR) + '#' + CAST(ABS(BS.STOCKQTY + ISNULL(SD.TRANSFERQTY,0)) AS VARCHAR) AS BATCHSTOCKQTY FROM [T_BATCHWISEAVAILABLE_STOCK] AS BS " +
            //             "LEFT OUTER JOIN [T_STOCKTRANSFER_DETAILS] SD ON BS.PRODUCTID=SD.PRODUCTID AND BS.BATCHNO=SD.BATCHNO AND SD.STOCKTRANSFERID='" + transferid + "' WHERE BS.BRID='" + depotid + "' AND BS.PRODUCTID='" + pid + "'";
            //string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            //string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            //string sql = "SELECT BS.BATCHNO,BS.STOCKQTY,ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + TOPACKSIZEID + "','" + RETURNPACKSIZE + "',SD.TRANSFERQTY),0) AS TRANSFERQTY,CAST(ABS(BS.STOCKQTY + ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + TOPACKSIZEID + "','" + RETURNPACKSIZE + "',SD.TRANSFERQTY),0)) AS VARCHAR) AS EDITEDSTOCKQTY," +
            //             "BS.BATCHNO + '#' + CAST(BS.STOCKQTY AS VARCHAR) + '#' + CAST(ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + TOPACKSIZEID + "','" + RETURNPACKSIZE + "',SD.TRANSFERQTY),0) AS VARCHAR) + '#' + CAST(ABS(BS.STOCKQTY + ISNULL(DBO.GetPackingSize_OnCall('" + pid + "','" + TOPACKSIZEID + "','" + RETURNPACKSIZE + "',SD.TRANSFERQTY),0)) AS VARCHAR) AS BATCHSTOCKQTY FROM [T_BATCHWISEAVAILABLE_STOCK] AS BS " +
            //             "LEFT OUTER JOIN [T_STOCKTRANSFER_DETAILS] SD ON BS.PRODUCTID=SD.PRODUCTID AND BS.BATCHNO=SD.BATCHNO AND SD.STOCKTRANSFERID='" + transferid + "' WHERE BS.BRID='" + depotid + "' AND BS.PRODUCTID='" + pid + "'";


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

        public DataTable BindReceivedControl(string fromdate, string todate, string depotid, string CheckerFlag, string UserID, string Finyear)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            /*if (CheckerFlag == "FALSE")
            {
                sql = " SELECT A.STOCKDEPORECEIVEDID,CONVERT(VARCHAR(10),CAST(STOCKDEPORECEIVEDDATE AS DATE),103) AS STOCKDEPORECEIVEDDATE,STOCKDEPORECEIVEDNO," +
                      " STOCKTRANSFERID,MOTHERDEPOTID,MOTHERDEPOTNAME,RECEIVEDDEPOTID,RECEIVEDDEPOTNAME,CHALLANNO," +
                      " A.NEXTLEVELID AS NEXTLEVELID,(E.FNAME+' '+E.LNAME) AS APPROVAL_PERSON,A.ISVERIFIED," +
                      " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT" +
                      " FROM [T_DEPORECEIVED_STOCK_HEADER] AS A WITH (NOLOCK) INNER JOIN T_DEPORECEIVED_STOCK_FOOTER AS B WITH (NOLOCK)" +
                      " ON A.STOCKDEPORECEIVEDID = B.STOCKDEPORECEIVEDID INNER JOIN M_USER AS E ON A.NEXTLEVELID = E.USERID" +
                      " WHERE CONVERT(DATE,STOCKDEPORECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND  DBO.Convert_To_ISO('" + todate + "')" +
                      " AND FINYEAR ='" + Finyear + "'" +
                      " AND CREATEDBY = '" + UserID + "'" +
                      " ORDER BY STOCKDEPORECEIVEDNUM DESC";
            }
            else if (CheckerFlag == "TRUE")
            {
                sql = " SELECT A.STOCKDEPORECEIVEDID,CONVERT(VARCHAR(10),CAST(STOCKDEPORECEIVEDDATE AS DATE),103) AS STOCKDEPORECEIVEDDATE,STOCKDEPORECEIVEDNO," +
                      " STOCKTRANSFERID,MOTHERDEPOTID,MOTHERDEPOTNAME,RECEIVEDDEPOTID,RECEIVEDDEPOTNAME,CHALLANNO," +
                      " A.NEXTLEVELID AS NEXTLEVELID,(E.FNAME+' '+E.LNAME) AS APPROVAL_PERSON,A.ISVERIFIED," +
                      " ISNULL(TOTALCASE,0) AS TOTALCASE,ISNULL(TOTALPCS,0) AS TOTALPCS," +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(B.NETAMOUNT,0) AS NETAMOUNT" +
                      " FROM [T_DEPORECEIVED_STOCK_HEADER] AS A WITH (NOLOCK) INNER JOIN T_DEPORECEIVED_STOCK_FOOTER AS B WITH (NOLOCK)" +
                      " ON A.STOCKDEPORECEIVEDID = B.STOCKDEPORECEIVEDID INNER JOIN M_USER AS E ON A.NEXTLEVELID = E.USERID" +
                      " WHERE CONVERT(DATE,STOCKDEPORECEIVEDDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND  DBO.Convert_To_ISO('" + todate + "')" +
                      " AND FINYEAR ='" + Finyear + "'" +
                      " AND ISVERIFIED <> 'Y'" +
                      " AND NEXTLEVELID = '" + UserID + "'" +
                      " ORDER BY STOCKDEPORECEIVEDNUM DESC";
            }*/
            sql = "EXEC USP_FETCH_DEPOTRECEIVED '" + fromdate + "','" + todate + "','" + depotid + "','" + CheckerFlag + "','" + UserID + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteDepotReceived(string ReceivedID)
        {
            int delflag = 0;

            string sqldeleteStockDespatch = "EXEC [SP_DEPOT_RECEIVED_DELETE] '" + ReceivedID + "'";
            int e = db.HandleData(sqldeleteStockDespatch);

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
        public DataTable BindProductDetails(string depotid)
        {
            //string sql = "select NEWID() AS GUID,PRODUCTID,PRODUCTNAME,BATCHNO,PACKSIZEID,PACKSIZENAME,STOCKQTY FROM [T_BATCHWISEAVAILABLE_STOCK] WHERE BRID='" + depotid + "'";
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = "SP_BATCHWISE_DEPOT_STOCK '" + depotid + "','0','" + RETURNPACKSIZE + "','0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public decimal ConvertionQty(string ProductID, string FromPackSize, decimal Qty)
        {
            decimal convertionqty = 0;
            string SQLFROMPACKSIZE = "SELECT PACKSIZE FROM P_APPMASTER";
            string RETURNPACKSIZE = (string)db.GetSingleValue(SQLFROMPACKSIZE);
            string sql = string.Empty;
            sql = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + ProductID + "','" + FromPackSize + "','" + RETURNPACKSIZE + "'," + Qty + ")),0)";
            convertionqty = (decimal)db.GetSingleValue(sql);
            return convertionqty;
        }
        public decimal ConvertionQty(string ProductID, string PackSize, string ReceivedPacksize, decimal Qty)
        {
            decimal convertionqty = 0;
            string sql = string.Empty;
            sql = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + ProductID + "','" + PackSize + "','" + ReceivedPacksize + "'," + Qty + ")),0)";
            convertionqty = (decimal)db.GetSingleValue(sql);
            return convertionqty;
        }
        public int DatatableCheck(string pid, string pname, string batchno)
        {
            int flag = 0;
            dtdeporeceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            if (dtdeporeceived.Rows.Count > 0)
            {
                int NumberofRecord = dtdeporeceived.Select("PRODUCTID='" + pid + "' AND PRODUCTNAME='" + pname + "' AND BATCHNO='" + batchno + "'").Length;
                if (NumberofRecord > 0)
                {
                    flag = 1;
                }
            }
            return flag;
        }

        public int RecordCheck(string pid, string pname, string batchno)
        {
            int flag = 0;
            dtdeporeceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            if (dtdeporeceived.Rows.Count > 0)
            {
                for (int i = 0; i < dtdeporeceived.Rows.Count; i++)
                {
                    if (dtdeporeceived.Rows[i]["PRODUCTID"].ToString() == pid && dtdeporeceived.Rows[i]["PRODUCTNAME"].ToString() == pname && dtdeporeceived.Rows[i]["BATCHNO"].ToString() == batchno)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            return flag;
        }
        public int RejectionRecordsDelete(string GUID, DataTable dtinnergrid)
        {
            int delflag = 0;
            int i = dtinnergrid.Rows.Count - 1;

            while (i >= 0)
            {
                if (dtinnergrid.Rows[i]["GUID"].ToString() == GUID)
                {
                    dtinnergrid.Rows[i].Delete();
                    dtinnergrid.AcceptChanges();
                    delflag = 1;
                    break;
                }
                i--;
            }
            return delflag;
        }

        public string Getstatus(string STOCKDEPORECEIVEDID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_DEPORECEIVED_STOCK_HEADER] WHERE STOCKDEPORECEIVEDID ='" + STOCKDEPORECEIVEDID + "'  AND DAYENDTAG='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public string GetFinancestatus(string STOCKDEPORECEIVEDID)
        {
            string Sql = " IF  EXISTS(SELECT 1 FROM [T_DEPORECEIVED_STOCK_HEADER] WHERE STOCKDEPORECEIVEDID ='" + STOCKDEPORECEIVEDID + "'  AND ISVERIFIED='Y' ) BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public string InsertReceivedDetails(string receiveddate, string motherdepotid, string motherdepotname, string receiveddepotid,
                                          string receiveddepotname, string transferdate, string waybillno, string waybillKey, string insuranceno,
                                          string transpotername, string modeoftransport, string vechileno, string lrgrno, string lrgrdate,
                                          string challenno, string challendate, string createdby, string finyear, string transferno, string hdnvalue,
                                          string xml, string xmlRejectionDetails, string xmlTax, string fformno, string fformdate, string gatepassno, string gatepassdate,
                                          string Remarks, string insuranceid, string insurancename, string TransporterID, string MENUID, string SaleOrderID,
                                          string SaleOrderNo, decimal TotalCase, decimal TotalPCS, decimal GrossAmt, decimal RoundOff, decimal NetAmt,
                                          decimal TotalTaxAmt, decimal BasicAmt, int Invoice_Type, string Export,string DELIVERYDATE)
        {
            string flag = "";
            string stockreceivedid = string.Empty;
            string stockreceivedno = string.Empty;
            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }

            string sqlprocreceived = " EXEC [SP_T_DEPOSTOCKRECEIVED_HEADER_FAC] '" + hdnvalue + "','" + flag + "','" + receiveddate + "','" + motherdepotid + "'," +
                                     " '" + motherdepotname + "','" + receiveddepotid + "','" + receiveddepotname + "','" + transferdate + "','" + waybillno + "'," +
                                     " '" + waybillKey + "','" + insuranceno + "','" + transpotername + "','" + modeoftransport + "','" + vechileno + "'," +
                                     " '" + lrgrno + "','" + lrgrdate + "','" + challenno + "','" + challendate + "','" + createdby + "','" + finyear + "'," +
                                     " '" + transferno + "','" + xml + "','" + xmlRejectionDetails + "','" + xmlTax + "','" + fformno + "','" + fformdate + "','" + gatepassno + "'," +
                                     " '" + gatepassdate + "','" + Remarks + "','" + insuranceid + "','" + insurancename + "','" + TransporterID + "','" + MENUID + "'," +
                                     " '" + SaleOrderID + "','" + SaleOrderNo + "'," + TotalCase + "," + TotalPCS + "," + GrossAmt + "," + RoundOff + "," + NetAmt + "," +
                                     " " + TotalTaxAmt + "," + BasicAmt + "," + Invoice_Type + ",'" + Export + "','" + DELIVERYDATE + "'";
            DataTable dtreceiveddetails = db.GetData(sqlprocreceived);

            if (dtreceiveddetails.Rows.Count > 0)
            {
                stockreceivedid = dtreceiveddetails.Rows[0]["STOCKDEPORECEIVEDID"].ToString();
                stockreceivedno = dtreceiveddetails.Rows[0]["STOCKDEPORECEIVEDNO"].ToString();
            }
            else
            {
                stockreceivedno = "";
            }
            return stockreceivedno;
        }
        public void ResetDataTables()
        {
            dtdeporeceived.Clear();
        }
        public int ReceivedRecordsDelete(string GUID)
        {
            int delflag = 0;
            dtdeporeceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            if (dtdeporeceived.Rows.Count > 0)
            {
                DataRow[] rows;
                rows = dtdeporeceived.Select("GUID='" + GUID + "'");
                foreach (DataRow row in rows)
                {
                    dtdeporeceived.Rows.Remove(row);
                    dtdeporeceived.AcceptChanges();
                    delflag = 1;
                }
            }
            HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtdeporeceived;
            return delflag;
        }
        public DataTable UpdateStock(string receiveddepotid, string receiveddepotname, string pid, string batchno, string finyear)
        {
            string sqlprocstock = "EXEC [SP_PRODUCTWISE_DEPOTRECEIVED_STOCK_MODIFICATION] '" + receiveddepotid + "','" + receiveddepotname + "','" + pid + "','" + batchno + "','" + finyear + "'";
            DataTable dtflag = db.GetData(sqlprocstock);
            return dtflag;
        }
        public DataTable BindTransferHeader(string transferid)
        {
            string sql = " SELECT STOCKTRANSFERID,STOCKTRANSFERNO,CONVERT(VARCHAR(10),CAST(STOCKTRANSFERDATE AS DATE),103) AS STOCKTRANSFERDATE,MOTHERDEPOTID,TODEPOTID,WAYBILLNO,INSURANCENO," +
                         " TRANSPORTERID,MODEOFTRANSPORT,VEHICHLENO,LRGRNO,CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) AS LRGRDATE," +
                         " CHALLANNO,CONVERT(VARCHAR(10),CAST(CHALLANDATE AS DATE),103) AS CHALLANDATE,FINYEAR FROM [T_STOCKTRANSFER_HEADER] WHERE STOCKTRANSFERID='" + transferid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindReceivedDetails(string receivedid)
        {
            dtdeporeceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            string sql = "SELECT NEWID() AS GUID,PRODUCTID,PRODUCTNAME,BATCHNO,PACKINGSIZEID,PACKINGSIZENAME,TRANSFERQTY,RECEIVEDQTY,REMAININGQTY,REASONID,REASONNAME,'E' AS TAG,RECEIVEDQTY AS BEFOREEDITEDQTY FROM [T_DEPORECEIVED_STOCK_DETAILS] WHERE STOCKDEPORECEIVEDID='" + receivedid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            dtdeporeceived = dt;
            HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtdeporeceived;
            return dt;
        }
    }
}