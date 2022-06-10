using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;
using System.Data.SqlClient;
using Account;
using System.IO;

namespace WorkFlow
{
    public class ClsPurchaseStockReceipt
    {
        DBUtils db = new DBUtils();

        #region Convert DataTable To XML
        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.TableName = "XMLData";
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }
        #endregion

        #region Create Voucher DataTable Structure
        public DataTable CreateVoucherTable()
        {
            DataTable dtvoucher = new DataTable();
            dtvoucher.Clear();
            dtvoucher.Columns.Add("GUID");
            dtvoucher.Columns.Add("LedgerId");
            dtvoucher.Columns.Add("LedgerName");
            dtvoucher.Columns.Add("TxnType");
            dtvoucher.Columns.Add("Amount", typeof(decimal));
            dtvoucher.Columns.Add("BankID");
            dtvoucher.Columns.Add("BankName");
            dtvoucher.Columns.Add("ChequeNo");
            dtvoucher.Columns.Add("ChequeDate");
            dtvoucher.Columns.Add("IsChequeRealised");
            dtvoucher.Columns.Add("Remarks");
            dtvoucher.Columns.Add("DeductableAmount");
            dtvoucher.Columns.Add("IsCostCenter");
            dtvoucher.Columns["IsCostCenter"].DefaultValue = "N";
            return dtvoucher;
        }
        #endregion

        #region Create Invoice DataTable Structure
        public DataTable CreateInvoiceTable()
        {
            DataTable dtInvoice = new DataTable();
            dtInvoice.Clear();
            dtInvoice.Columns.Add("GUID");
            dtInvoice.Columns.Add("LedgerId");
            dtInvoice.Columns.Add("LedgerName");
            dtInvoice.Columns.Add("InvoiceID");
            dtInvoice.Columns.Add("InvoiceNo");
            dtInvoice.Columns.Add("InvoiceAmt", typeof(decimal));
            dtInvoice.Columns.Add("AmtPaid", typeof(decimal));
            dtInvoice.Columns.Add("VoucherType");
            dtInvoice.Columns.Add("BranchID");
            dtInvoice.Columns.Add("InvoiceDate");
            dtInvoice.Columns.Add("InvoiceBranchID");
            dtInvoice.Columns.Add("InvoiceOthers");
            dtInvoice.Columns.Add("BILLDATE");

            return dtInvoice;
        }
        #endregion

        #region Account Posting for Depot Transfer
        public int AccountPostingForDepotTransfer(string DepotTransferID, string Finyr, string Uid)
        {
            int flag = 0;
            string Voucher = "", voucherID = "";
            string PurchaseLeadger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", motherdepotid = "", VchDate = "", transporter = "";
            string invoice = "", InsuranceCompanyName = "", InsuranceNo = "", recivedepot = "", motherdepotName = "";
            string REFERENCELEDGER_MOTHERDEPOTID = "", REFERENCELEDGER_MOTHERDEPONAME = "", REFERENCELEDGER_RECEIVEDDEPOTID = "", REFERENCELEDGER_RECEIVEDDEPONAME = "";
            decimal TAXVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();

            VoucherTable = CreateVoucherTable();

            // DR                               CR                                  BOOKS
            //RECEIVED DEPOT                    STOCK TRANSFER LEDGER               FROM DEPOT
            //                                  OUTPUT TAX

            string Sql = "SELECT  [STKTRANSFER_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PurchaseLeadger = dt.Rows[0]["STKTRANSFER_ACC_LEADGER"].ToString();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT [STOCKTRANSFERDATE],[MOTHERDEPOTID],[TODEPOTID],[INSURANCECOMPID],INSURANCECOMPNAME,INSURANCENO,[TOTALRECEIVEDVALUE],[TRANSPORTERID] " +
                   " ,[STOCKTRANSFERID],[INVOICENO],[MOTHERDEPOTNAME],[TAXID],[TAXVALUE],[ROUNDOFFVALUE] FROM [Vw_DepoStockTransfer_AccPosting] " +
                   " WHERE [STOCKTRANSFERID]='" + DepotTransferID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                motherdepotid = Convert.ToString(dt.Rows[0]["MOTHERDEPOTID"]);
                recivedepot = Convert.ToString(dt.Rows[0]["TODEPOTID"]);
                motherdepotName = Convert.ToString(dt.Rows[0]["MOTHERDEPOTNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["STOCKTRANSFERDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["INVOICENO"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                InsuranceCompanyName = Convert.ToString(dt.Rows[0]["INSURANCECOMPNAME"]);
                InsuranceNo = Convert.ToString(dt.Rows[0]["INSURANCENO"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGER_MOTHERDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_MOTHERDEPOTID + "'";
                REFERENCELEDGER_MOTHERDEPONAME = (string)db.GetSingleValue(Sql);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + recivedepot + "'";
                REFERENCELEDGER_RECEIVEDDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_RECEIVEDDEPOTID + "'";
                REFERENCELEDGER_RECEIVEDDEPONAME = (string)db.GetSingleValue(Sql);

                //stock transfer leadger 
                DataRow dr = VoucherTable.NewRow();

                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString((Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString((Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //received Depot
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = REFERENCELEDGER_RECEIVEDDEPOTID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No. " + invoice + " bill date " + VchDate + " with insurance no. " + InsuranceNo + " on " + InsuranceCompanyName + ".";
                Voucher = VchEntry.InsertVoucherDetails("4", "Stock Transfer", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou1 = Voucher.Split('|');
                    voucherID = vou1[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotTransferID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Account Posting for Insurance
        public int AccountPostingForInsurance(string SystemVoucherID, string Finyr, string Uid, string ClaimID)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string claimNo = "", PolicyNo = "", ClaimLodgeDate = "", DepotName = "", DepotID = "", REFERENCELEDGERID = "";
            string InsClaimLeadger = "", HOLeadger = "", HOLeadgerName = "", VchDate = "", Insurance = "", Bkg_Dmg_ShrtgLeadger = "", Narration = "", SettlementAmount = "";
            string invoice = "", stockreceivedid = "", TransferFrom = "", Tag = "";
            decimal SETTLEMENTAMOUNT = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                  BOOKS
            //INS CO                      BRK/DAMG                 HO.

            string Sql = "SELECT  [INSURANCECLAIM_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                //Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
            }


            Sql = "SELECT BRID,BRNAME FROM [M_BRANCH] where BRANCHTAG ='O'   and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
                HOLeadgerName = dt.Rows[0]["BRNAME"].ToString();
            }

            Sql = "SELECT VOUCHERID,SETTLEMENTAMOUNT,VOUCHERDATE,INSURANCECOMPANYID,TAG FROM [Vw_Insurance_AccPosting] WHERE [CLAIMID]='" + ClaimID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {

                VchDate = dt.Rows[0]["VOUCHERDATE"].ToString();
                Insurance = dt.Rows[0]["INSURANCECOMPANYID"].ToString();
                Tag = dt.Rows[0]["TAG"].ToString().Trim();
                //Insurance leadger 
                DataRow dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(Insurance);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["SETTLEMENTAMOUNT"]);

                SETTLEMENTAMOUNT = Convert.ToDecimal(dt.Rows[0]["SETTLEMENTAMOUNT"]);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SETTLEMENTAMOUNT"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                //received Depot
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = InsClaimLeadger;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["SETTLEMENTAMOUNT"]);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SETTLEMENTAMOUNT"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();
                if (Tag == "SR")
                {
                    Sql = " SELECT A.CLAIMID,A.CLAIMNO,A.POLICYNO,A.DEPOTNAME,A.CLAIMLODGEDATE,B.TPUNAME,B.INVOICENO,B.STOCKRECEIVEDID,B.MOTHERDEPOTID AS DEPOTID " +
                          " FROM VW_INSURANCE_ACCPOSTING AS A INNER JOIN" +
                          "     T_STOCKRECEIVED_HEADER AS B " +
                          "     ON A.VOUCHERID = B.STOCKRECEIVEDID" +
                          " WHERE A.CLAIMID = '" + ClaimID + "'";

                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        ClaimLodgeDate = dt.Rows[0]["CLAIMLODGEDATE"].ToString();
                        DepotName = dt.Rows[0]["DEPOTNAME"].ToString();
                        DepotID = dt.Rows[0]["DEPOTID"].ToString();
                        ClaimID = dt.Rows[0]["CLAIMID"].ToString();
                        claimNo = dt.Rows[0]["CLAIMNO"].ToString();
                        PolicyNo = dt.Rows[0]["POLICYNO"].ToString();
                        stockreceivedid = dt.Rows[0]["STOCKRECEIVEDID"].ToString();
                        invoice = dt.Rows[0]["INVOICENO"].ToString();
                        TransferFrom = dt.Rows[0]["TPUNAME"].ToString();
                    }

                    Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
                    REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = InsClaimLeadger;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = ClaimID;
                    dr["InvoiceNo"] = claimNo;
                    dr["InvoiceAmt"] = SETTLEMENTAMOUNT;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = ClaimLodgeDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = claimNo;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Narration = "Being claim No. " + claimNo + " lodged with insurance No. " + PolicyNo + " for damage/breakage/shortage " +
                                "dated. " + ClaimLodgeDate + " during transmit to depot " + DepotName + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, SystemVoucherID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }
                }
                else
                {
                    Sql = " SELECT A.CLAIMID,A.CLAIMNO,A.POLICYNO,A.DEPOTNAME,A.CLAIMLODGEDATE,B.MOTHERDEPOTNAME,B.MOTHERDEPOTID AS DEPOTID,C.STOCKTRANSFERNO" +
                          " FROM VW_INSURANCE_ACCPOSTING AS A INNER JOIN T_DEPORECEIVED_STOCK_HEADER AS B" +
                          "     ON A.VOUCHERID = B.STOCKDEPORECEIVEDID INNER JOIN T_STOCKTRANSFER_HEADER AS C" +
                          "     ON B.STOCKTRANSFERID = C.STOCKTRANSFERID" +
                          " WHERE A.CLAIMID = '" + ClaimID + "'";

                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        ClaimLodgeDate = dt.Rows[0]["CLAIMLODGEDATE"].ToString();
                        DepotName = dt.Rows[0]["DEPOTNAME"].ToString();
                        DepotID = dt.Rows[0]["DEPOTID"].ToString();
                        ClaimID = dt.Rows[0]["CLAIMID"].ToString();
                        claimNo = dt.Rows[0]["CLAIMNO"].ToString();
                        PolicyNo = dt.Rows[0]["POLICYNO"].ToString();
                        invoice = dt.Rows[0]["STOCKTRANSFERNO"].ToString();
                        TransferFrom = dt.Rows[0]["MOTHERDEPOTNAME"].ToString();
                    }

                    Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
                    REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = InsClaimLeadger;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = ClaimID;
                    dr["InvoiceNo"] = claimNo;
                    dr["InvoiceAmt"] = SETTLEMENTAMOUNT;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = ClaimLodgeDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = claimNo;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Narration = "Being claim No. " + claimNo + " lodged with insurance No. " + PolicyNo + " for damage/breakage/shortage " +
                                "dated. " + ClaimLodgeDate + " during transmit to depot " + DepotName + ".";
                    VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, SystemVoucherID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }
                }

                flag = 1;
            }

            return flag;
        }
        #endregion

        #region Account Posting for Depot Received stock transfer
        protected int AccountPostingForDepotReceived(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string PurchaseLeadger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", HOLeadgerName = "", motherdepotid = "", VchDate = "", TPUID = "", transporter = "";
            string invoice = "", Insurance = "", recivedepot = "", motherdepotName = "", REFERENCELEDGER_MOTHERDEPOTID = "", REFERENCELEDGER_MOTHERDEPONAME = "", REFERENCELEDGER_RECEIVEDDEPOTID = "", REFERENCELEDGER_RECEIVEDDEPONAME = "";
            decimal TAXVALUE = 0;
            DataTable dt = new DataTable();
            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            // DR                               CR                                  BOOKS
            //STOCK RECEIPT                     FROM DEPOT                          RECEIVED DEPOT
            //INPUT TAX

            string Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PurchaseLeadger = dt.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT BRID,BRNAME FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
                HOLeadgerName = dt.Rows[0]["BRNAME"].ToString();
            }

            Sql = " SELECT [TOTALRECEIVEDVALUE],[STOCKDEPORECEIVEDID] as [STOCKRECEIVEDID],[RECEIVEDDEPOTID], [MOTHERDEPOTID],[INSURANCECOMPID],[ROUNDOFFVALUE]," +
                  " [STOCKDEPORECEIVEDDATE] as [STOCKRECEIVEDDATE],[INVOICENO],[TRANSPORTERID],[MOTHERDEPOTNAME],[TAXID],[TAXVALUE] FROM [Vw_DepoStockReceived_AccPosting] " +
                  " WHERE STOCKDEPORECEIVEDID='" + DepotRecivedID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                motherdepotid = Convert.ToString(dt.Rows[0]["MOTHERDEPOTID"]);
                recivedepot = Convert.ToString(dt.Rows[0]["RECEIVEDDEPOTID"]);
                motherdepotName = Convert.ToString(dt.Rows[0]["MOTHERDEPOTNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["STOCKRECEIVEDDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["INVOICENO"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                Insurance = Convert.ToString(dt.Rows[0]["INSURANCECOMPID"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGER_MOTHERDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_MOTHERDEPOTID + "'";
                REFERENCELEDGER_MOTHERDEPONAME = (string)db.GetSingleValue(Sql);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + recivedepot + "'";
                REFERENCELEDGER_RECEIVEDDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_RECEIVEDDEPOTID + "'";
                REFERENCELEDGER_RECEIVEDDEPONAME = (string)db.GetSingleValue(Sql);

                //stock transfer receive leadger 
                DataRow dr = VoucherTable.NewRow();

                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                //Issue Depot
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = REFERENCELEDGER_MOTHERDEPOTID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                Voucher = VchEntry.InsertVoucherDetails("3", "Stock Received", REFERENCELEDGER_RECEIVEDDEPOTID, REFERENCELEDGER_RECEIVEDDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;
                }

                // Damage / Breakage / shortage
                //dr                                cr                              books
                //trnsporter/insurance/tpu          damage/shortage/btreakage       depot

                decimal Amount = 0;
                string DEBITEDTOID = "";
                string Debitleadger = "";
                Sql = "SELECT [DEPOTRECEIVEDID] AS  [STOCKRECEIVEDID],[AMOUNT] ,[DEBITEDTOID] FROM [Vw_DepoDamageReceive_AccPosting] where [DEPOTRECEIVEDID]='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                dt = db.GetData(Sql);
                if (dt.Rows.Count > 0)
                {
                    Amount = 0;
                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        DEBITEDTOID = dt.Rows[counter]["DEBITEDTOID"].ToString();
                        switch (dt.Rows[counter]["DEBITEDTOID"].ToString())
                        {
                            case "1":   // Transporter

                                Debitleadger = transporter;
                                break;

                            case "2":   // TPU

                                Debitleadger = TPUID;
                                break;

                                #region Commented By Avishek Ghosh on 31-03-2016 (Insurance)
                                //case "3":   // Insurance

                                //    Debitleadger = Insurance;
                                //    Bkg_Dmg_ShrtgLeadger = InsClaimLeadger;
                                //    break;
                                #endregion

                        }

                        if (DEBITEDTOID != "3")
                        {
                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Debitleadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();

                            Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou3 = Voucher.Split('|');
                                voucherID = vou3[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }


                        else
                        {

                            #region Commented By Avishek Ghosh on 31-03-2016 (Insurance Auto Posting)
                            //    VoucherTable.Clear();

                            //    dr = VoucherTable.NewRow();
                            //    dr["GUID"] = Guid.NewGuid();
                            //    dr["LedgerId"] = Debitleadger;
                            //    dr["LedgerName"] = "";
                            //    dr["TxnType"] = Convert.ToString("0");
                            //    dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();

                            //    Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                            //    dr["BankID"] = Convert.ToString("");
                            //    dr["BankName"] = "";
                            //    dr["ChequeNo"] = Convert.ToString("");
                            //    dr["ChequeDate"] = Convert.ToString("");
                            //    dr["IsChequeRealised"] = Convert.ToString("N");
                            //dr["Remarks"] = Convert.ToString("");
                            //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            //    VoucherTable.Rows.Add(dr);
                            //    VoucherTable.AcceptChanges();



                            //    dr = VoucherTable.NewRow();
                            //    dr["GUID"] = Guid.NewGuid();
                            //    dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                            //    dr["LedgerName"] = "";
                            //    dr["TxnType"] = Convert.ToString("1");
                            //    dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();



                            //    dr["BankID"] = Convert.ToString("");
                            //    dr["BankName"] = "";
                            //    dr["ChequeNo"] = Convert.ToString("");
                            //    dr["ChequeDate"] = Convert.ToString("");
                            //    dr["IsChequeRealised"] = Convert.ToString("N");
                            //dr["Remarks"] = Convert.ToString("");
                            //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            //    VoucherTable.Rows.Add(dr);
                            //    VoucherTable.AcceptChanges();


                            //    XML = ConvertDatatableToXML(VoucherTable);

                            //    Narration = "(Auto) Being goods transfer from " + HOLeadgerName + " against bill No." + invoice + ".";
                            //    Voucher =  VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            //String[] vou4 = Voucher.Split('|');
                            //voucherID = vou4[0].Trim();

                            //VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                            //Voucher = string.Empty;
                            //voucherID = string.Empty;

                            #endregion

                            // Added By Avishek Ghosh On 31-03-2016
                            string sqlInsurance = string.Empty;
                            sqlInsurance = "EXEC SP_INSURANCE_CLAIM_INSERT 'A','" + DepotRecivedID + "','" + VoucherNo + "'," + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString()) + ",'" + Uid + "','" + Finyr + "','" + InsuranceCompID + "','" + InsuranceCompName + "','DR','" + VoucherDate + "','" + InsuranceNo + "','" + DepotID + "','" + DepotName + "'";
                            db.HandleData(sqlInsurance);
                        }


                        if (DEBITEDTOID != "3")
                        {
                            if (REFERENCELEDGER_MOTHERDEPOTID == motherdepotid)
                            {

                                //dr                                cr                              books
                                //HO                               trnsporter/insurance/tpu         depot

                                VoucherTable.Clear();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = HOLeadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("0");
                                dr["Amount"] = Amount;
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(Amount);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();


                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Debitleadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Amount;
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(Amount);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                XML = ConvertDatatableToXML(VoucherTable);

                                Narration = "(Auto) Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                {
                                    flag = 2;
                                    return 2;
                                }
                                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                {
                                    flag = 3;
                                    return 3;
                                }
                                else
                                {
                                    String[] vou3 = Voucher.Split('|');
                                    voucherID = vou3[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }

                                // DR                                               CR                    BOOKS
                                // Trnsporter/insurance/tpu                         Depot                 HO.

                                VoucherTable.Clear();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Debitleadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("0");
                                dr["Amount"] = Amount;
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(Amount);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();


                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = REFERENCELEDGER_MOTHERDEPOTID;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Amount;
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(Amount);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                XML = ConvertDatatableToXML(VoucherTable);

                                Narration = "(Auto) Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                {
                                    flag = 2;
                                    return 2;
                                }
                                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                {
                                    flag = 3;
                                    return 3;
                                }
                                else
                                {
                                    String[] vou4 = Voucher.Split('|');
                                    voucherID = vou4[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }
                            }
                        }
                    }
                }
                flag = 1;
            }
            return flag;
        }
        #endregion

        #region Account Posting for Depot Received from Factory stock transfer
        protected int AccountPostingForDepotReceived_FactoryStockTransfer(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string PurchaseLeadger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", HOLeadgerName = "", motherdepotid = "", VchDate = "", TPUID = "", transporter = "";
            string invoice = "", Insurance = "", recivedepot = "", motherdepotName = "", REFERENCELEDGER_MOTHERDEPOTID = "", REFERENCELEDGER_MOTHERDEPONAME = "", REFERENCELEDGER_RECEIVEDDEPOTID = "", REFERENCELEDGER_RECEIVEDDEPONAME = "";
            decimal TAXVALUE = 0;
            DataTable dt = new DataTable();
            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            // DR                               CR                                  BOOKS
            //STOCK RECEIPT                     FROM DEPOT                          RECEIVED DEPOT
            //INPUT TAX

            string Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PurchaseLeadger = dt.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT BRID,BRNAME FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
                HOLeadgerName = dt.Rows[0]["BRNAME"].ToString();
            }

            Sql = " SELECT [TOTALRECEIVEDVALUE],[TAXID],[TAXVALUE],[STOCKRECEIVEDID],[ROUNDOFFVALUE],[TPUID],[MOTHERDEPOTID],[MOTHERDEPOTNAME],[INSURANCECOMPID]," +
                  " [STOCKRECEIVEDDATE],[TPUNAME],[INVOICENO],[INVOICEDATE],[TRANSPORTERID],[TRANSPORTERNAME],[ISTRANSFERTOHO] FROM [Vw_StockReceived_AccPosting]" +
                  " WHERE stockreceivedid='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                motherdepotid = Convert.ToString(dt.Rows[0]["TPUID"]);
                recivedepot = Convert.ToString(dt.Rows[0]["MOTHERDEPOTID"]);
                motherdepotName = Convert.ToString(dt.Rows[0]["TPUNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["STOCKRECEIVEDDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["INVOICENO"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                Insurance = Convert.ToString(dt.Rows[0]["INSURANCECOMPID"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGER_MOTHERDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_MOTHERDEPOTID + "'";
                REFERENCELEDGER_MOTHERDEPONAME = (string)db.GetSingleValue(Sql);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + recivedepot + "'";
                REFERENCELEDGER_RECEIVEDDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_RECEIVEDDEPOTID + "'";
                REFERENCELEDGER_RECEIVEDDEPONAME = (string)db.GetSingleValue(Sql);

                //stock transfer receive leadger 
                DataRow dr = VoucherTable.NewRow();

                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                //Issue Depot
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = REFERENCELEDGER_MOTHERDEPOTID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                Voucher = VchEntry.InsertVoucherDetails("3", "Stock Received", REFERENCELEDGER_RECEIVEDDEPOTID, REFERENCELEDGER_RECEIVEDDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;
                }

                // Damage / Breakage / shortage
                //dr                                cr                              books
                //trnsporter/insurance/tpu          damage/shortage/btreakage       depot

                decimal Amount = 0;
                string DEBITEDTOID = "";
                string Debitleadger = "";
                Sql = "SELECT [STOCKRECEIVEDID] AS  [STOCKRECEIVEDID],[AMOUNT] ,[DEBITEDTOID] FROM [Vw_DepotDamageReceive_FatcoryTransfer] where [STOCKRECEIVEDID]='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                dt = db.GetData(Sql);
                if (dt.Rows.Count > 0)
                {
                    Amount = 0;
                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        DEBITEDTOID = dt.Rows[counter]["DEBITEDTOID"].ToString();
                        switch (dt.Rows[counter]["DEBITEDTOID"].ToString())
                        {
                            case "1":   // Transporter

                                Debitleadger = transporter;
                                break;

                            case "2":   // TPU

                                Debitleadger = TPUID;
                                break;

                                #region Commented By Avishek Ghosh on 31-03-2016 (Insurance)
                                //case "3":   // Insurance

                                //    Debitleadger = Insurance;
                                //    Bkg_Dmg_ShrtgLeadger = InsClaimLeadger;
                                //    break;
                                #endregion

                        }

                        if (DEBITEDTOID != "3")
                        {
                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Debitleadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();

                            Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou3 = Voucher.Split('|');
                                voucherID = vou3[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }


                        else
                        {

                            #region Commented By Avishek Ghosh on 31-03-2016 (Insurance Auto Posting)
                            //    VoucherTable.Clear();

                            //    dr = VoucherTable.NewRow();
                            //    dr["GUID"] = Guid.NewGuid();
                            //    dr["LedgerId"] = Debitleadger;
                            //    dr["LedgerName"] = "";
                            //    dr["TxnType"] = Convert.ToString("0");
                            //    dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();

                            //    Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                            //    dr["BankID"] = Convert.ToString("");
                            //    dr["BankName"] = "";
                            //    dr["ChequeNo"] = Convert.ToString("");
                            //    dr["ChequeDate"] = Convert.ToString("");
                            //    dr["IsChequeRealised"] = Convert.ToString("N");
                            //dr["Remarks"] = Convert.ToString("");
                            //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            //    VoucherTable.Rows.Add(dr);
                            //    VoucherTable.AcceptChanges();



                            //    dr = VoucherTable.NewRow();
                            //    dr["GUID"] = Guid.NewGuid();
                            //    dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                            //    dr["LedgerName"] = "";
                            //    dr["TxnType"] = Convert.ToString("1");
                            //    dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();



                            //    dr["BankID"] = Convert.ToString("");
                            //    dr["BankName"] = "";
                            //    dr["ChequeNo"] = Convert.ToString("");
                            //    dr["ChequeDate"] = Convert.ToString("");
                            //    dr["IsChequeRealised"] = Convert.ToString("N");
                            //dr["Remarks"] = Convert.ToString("");
                            //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"].ToString());

                            //    VoucherTable.Rows.Add(dr);
                            //    VoucherTable.AcceptChanges();


                            //    XML = ConvertDatatableToXML(VoucherTable);

                            //    Narration = "(Auto) Being goods transfer from " + HOLeadgerName + " against bill No." + invoice + ".";
                            //    Voucher =  VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            //String[] vou4 = Voucher.Split('|');
                            //voucherID = vou4[0].Trim();

                            //VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                            //Voucher = string.Empty;
                            //voucherID = string.Empty;

                            #endregion

                            // Added By Avishek Ghosh On 31-03-2016
                            string sqlInsurance = string.Empty;
                            sqlInsurance = "EXEC SP_INSURANCE_CLAIM_INSERT 'A','" + DepotRecivedID + "','" + VoucherNo + "'," + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString()) + ",'" + Uid + "','" + Finyr + "','" + InsuranceCompID + "','" + InsuranceCompName + "','DR','" + VoucherDate + "','" + InsuranceNo + "','" + DepotID + "','" + DepotName + "'";
                            db.HandleData(sqlInsurance);
                        }
                    }
                }
                flag = 1;
            }
            return flag;
        }
        #endregion

        #region Account Posting for Purchase Stock Received
        protected int AccountPosting(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID,
            string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo, string TxnTag = "N")
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string StockReceiptLedger = "", PurchaseLeadger = "", POPLedger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", motherdepotid = "", motherdepotname = "", VchDate = "", TPUID = "", transporter = "", transportername = "";
            string TPUname = "", invoice = "", invoicedate = "", Insurance = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", ISFACTORY = "", Isautoposting = "N", kolkataDepotID = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            // DR                           CR                  BOOKS
            //PURCHASE                      TPU                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O'   and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT [TOTALRECEIVEDVALUE],[TAXID],[TAXVALUE],[STOCKRECEIVEDID],[ROUNDOFFVALUE],[TPUID],[MOTHERDEPOTID],[MOTHERDEPOTNAME],[INSURANCECOMPID]," +
                  " [STOCKRECEIVEDDATE],[TPUNAME],[INVOICENO],[INVOICEDATE],[TRANSPORTERID],[TRANSPORTERNAME],[ISTRANSFERTOHO],[POSTING_ACC_LEDGERID]," +
                  " [ADDITIONAL_LEDGERID],[ADDITIONAL_AMOUNT] FROM [Vw_StockReceived_AccPosting] WHERE stockreceivedid='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[PURCHASE_ACC_LEADGER],[PURCHASE_EXPORT_ACC_LEADGER],[POP_ACC_LEDGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER],[KOLKATADEPOTID] FROM [P_APPMASTER] ";

                DataTable dtledger = db.GetData(Sql);
                if (dtledger.Rows.Count > 0)
                {
                    //POPLedger = dt.Rows[0]["POP_ACC_LEDGER"].ToString();
                    if (LedgerInfo == "")       /* Based on Single Page Parameter */
                    {
                        if (TxnTag == "E")
                        {
                            PurchaseLeadger = dtledger.Rows[0]["PURCHASE_EXPORT_ACC_LEADGER"].ToString();
                        }
                        else
                        {
                            PurchaseLeadger = dt.Rows[0]["POSTING_ACC_LEDGERID"].ToString();
                        }
                    }
                    else
                    {
                        PurchaseLeadger = LedgerInfo;
                    }
                    RoundOffLeadger = dtledger.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                    Bkg_Dmg_ShrtgLeadger = dtledger.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                    InsClaimLeadger = dtledger.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
                    StockReceiptLedger = dtledger.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
                    kolkataDepotID = dtledger.Rows[0]["KOLKATADEPOTID"].ToString().Trim();
                }


                motherdepotid = dt.Rows[0]["MOTHERDEPOTID"].ToString();
                motherdepotname = dt.Rows[0]["MOTHERDEPOTNAME"].ToString();
                VchDate = dt.Rows[0]["STOCKRECEIVEDDATE"].ToString();
                TPUname = dt.Rows[0]["TPUNAME"].ToString();
                invoice = dt.Rows[0]["INVOICENO"].ToString();
                invoicedate = dt.Rows[0]["INVOICEDATE"].ToString();
                transporter = dt.Rows[0]["TRANSPORTERID"].ToString();
                transportername = dt.Rows[0]["TRANSPORTERNAME"].ToString();
                Insurance = dt.Rows[0]["INSURANCECOMPID"].ToString();
                StockReceivedID = dt.Rows[0]["STOCKRECEIVEDID"].ToString();
                Isautoposting = dt.Rows[0]["ISTRANSFERTOHO"].ToString();

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO,ISNULL(ISFACTORY,'N') AS ISFACTORY FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                    ISFACTORY = Convert.ToString(dtbranch.Rows[0]["ISFACTORY"]);
                }

                DataRow dr = VoucherTable.NewRow();

                decimal TOTALRECEIVEDVALUE = 0;

                TOTALRECEIVEDVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

                //TPU
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["TPUID"]);
                TPUID = Convert.ToString(dt.Rows[0]["TPUID"]);

                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));

                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                if (Convert.ToDecimal(dt.Rows[0]["ADDITIONAL_AMOUNT"]) > 0)
                {
                    //ADDITIONAL AMOUNT
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["ADDITIONAL_LEDGERID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ADDITIONAL_AMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ADDITIONAL_AMOUNT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                decimal TAXVALUE = 0;
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "" && Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]) > 0)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";

                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                }


                //Purchase 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALRECEIVEDVALUE) - Convert.ToDecimal(dt.Rows[0]["ADDITIONAL_AMOUNT"]) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALRECEIVEDVALUE) - Convert.ToDecimal(dt.Rows[0]["ADDITIONAL_AMOUNT"]) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();
                string Narration = "Being goods purchased from " + TPUname + " against bill No." + invoice + " dated " + invoicedate + ".";

                if (kolkataDepotID == motherdepotid)    /* if depot id kolkata then invoice details tag with purchase voucher */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = TPUID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = DepotRecivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = VchDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = invoice;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("12", "Purchase", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO != "Y") && (Isautoposting != "Y"))   /* if depot purchase from vendor not transfer to HO then invoice details tag with purchase voucher */
                {
                    /* FROM VENDOR MASTER not transfer to HO */
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = TPUID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = DepotRecivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = VchDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = invoice;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("12", "Purchase", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails("12", "Purchase", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
                }

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else if (Voucher.Contains("|"))
                {
                    String[] vou = Voucher.Split('|');
                    voucherID = vou[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (REFERENCELEDGERID == motherdepotid && ISPOSTINGTOHO == "Y")
                    {
                        if (Isautoposting == "Y")           /* FROM VENDOR MASTER */
                        {

                            // DR                           CR                  BOOKS
                            //TPU                           HO                   DEPOT.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = HOLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods purchased from " + TPUname + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou1 = Voucher.Split('|');
                                voucherID = vou1[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }


                            // DR                           CR                  BOOKS
                            // DEPOT                        TPU                  HO.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = motherdepotid;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = DepotRecivedID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "9";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = VchDate;
                            dr["InvoiceBranchID"] = REFERENCELEDGERID;
                            dr["InvoiceOthers"] = invoice;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                            //Here invoiceiwse posting need to be passed here 
                            //Leadger ID : TPU Leadger ID
                            //Invoice ID: Tpu received ID
                            //Invoice No: Tpu Invoice No.
                            //Amt :  TOTALVALUE
                            //VoucherType: Payment
                            //BranchID  : HO Leadger


                            Narration = "(Auto) Being goods purchased from " + TPUname + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }


                    // Damage / Breakage / shortage
                    //dr                                cr                              books
                    //trnsporter/insurance/tpu          damage/shortage/btreakage       depot

                    decimal Amount = 0;
                    string Debitleadger = "";
                    string Debitleadgername = "";
                    string DEBITEDTOID = "";
                    Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT] ,[DEBITEDTOID] FROM [Vw_DamageReceive_AccPosting] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        Amount = 0;
                        for (int counter = 0; counter < dt.Rows.Count; counter++)
                        {
                            DEBITEDTOID = dt.Rows[counter]["DEBITEDTOID"].ToString();

                            switch (dt.Rows[counter]["DEBITEDTOID"].ToString())
                            {
                                case "1":   // Transporter

                                    Debitleadger = transporter;
                                    Debitleadgername = transportername;

                                    break;

                                case "2":   // TPU

                                    Debitleadger = TPUID;
                                    Debitleadgername = TPUname;
                                    break;

                                    #region Commented By Avishek Ghosh on 31-03-2016 (Insurance)
                                    //case "3":   // Insurance

                                    //    Debitleadger = Insurance;
                                    //    Bkg_Dmg_ShrtgLeadger = InsClaimLeadger;
                                    //    break;
                                    #endregion

                            }

                            if (DEBITEDTOID != "3")
                            {
                                VoucherTable.Clear();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Debitleadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("0");
                                dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());
                                Amount = Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                XML = ConvertDatatableToXML(VoucherTable);

                                Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                {
                                    flag = 2;
                                    return 2;
                                }
                                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                {
                                    flag = 3;
                                    return 3;
                                }
                                else
                                {
                                    String[] vou3 = Voucher.Split('|');
                                    voucherID = vou3[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }
                            }


                            else
                            {
                                #region Commented By Avishek Ghosh on 31-03-2016 (Insurance Auto Posting)

                                //    VoucherTable.Clear();

                                //    dr = VoucherTable.NewRow();
                                //    dr["GUID"] = Guid.NewGuid();
                                //    dr["LedgerId"] = Debitleadger;
                                //    dr["LedgerName"] = "";
                                //    dr["TxnType"] = Convert.ToString("0");
                                //    dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //    Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                                //    dr["BankID"] = Convert.ToString("");
                                //    dr["BankName"] = "";
                                //    dr["ChequeNo"] = Convert.ToString("");
                                //    dr["ChequeDate"] = Convert.ToString("");
                                //    dr["IsChequeRealised"] = Convert.ToString("N");
                                //dr["Remarks"] = Convert.ToString("");
                                //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //    VoucherTable.Rows.Add(dr);
                                //    VoucherTable.AcceptChanges();



                                //    dr = VoucherTable.NewRow();
                                //    dr["GUID"] = Guid.NewGuid();
                                //    dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                                //    dr["LedgerName"] = "";
                                //    dr["TxnType"] = Convert.ToString("1");
                                //    dr["Amount"] = dt.Rows[counter]["AMOUNT"].ToString();



                                //    dr["BankID"] = Convert.ToString("");
                                //    dr["BankName"] = "";
                                //    dr["ChequeNo"] = Convert.ToString("");
                                //    dr["ChequeDate"] = Convert.ToString("");
                                //    dr["IsChequeRealised"] = Convert.ToString("N");
                                //    dr["Remarks"] = Convert.ToString("");
                                //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //    VoucherTable.Rows.Add(dr);
                                //    VoucherTable.AcceptChanges();


                                //    XML = ConvertDatatableToXML(VoucherTable);

                                //    Narration = "(Auto) Being goods purchased from " + TPUname + " against bill No." + invoice + ".";
                                //    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                //String[] vou3 = Voucher.Split('|');
                                //voucherID = vou3[0].Trim();

                                //VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                //Voucher = string.Empty;
                                //voucherID = string.Empty;
                                #endregion

                                // Added By Avishek Ghosh On 31-03-2016
                                string sqlInsurance = string.Empty;
                                sqlInsurance = "EXEC SP_INSURANCE_CLAIM_INSERT 'A','" + DepotRecivedID + "','" + VoucherNo + "'," + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString()) + ",'" + Uid + "','" + Finyr + "','" + InsuranceCompID + "','" + InsuranceCompName + "','SR','" + VoucherDate + "','" + InsuranceNo + "','" + DepotID + "','" + DepotName + "'";
                                db.HandleData(sqlInsurance);

                            }


                            if (DEBITEDTOID != "3" && ISPOSTINGTOHO == "Y")
                            {
                                if (REFERENCELEDGERID == motherdepotid)
                                {

                                    //dr                                cr                              books
                                    //HO                               trnsporter/insurance/tpu         depot

                                    VoucherTable.Clear();

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = HOLeadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred to HO.";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                    {
                                        flag = 2;
                                        return 2;
                                    }
                                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                    {
                                        flag = 3;
                                        return 3;
                                    }
                                    else
                                    {

                                        String[] vou4 = Voucher.Split('|');
                                        voucherID = vou4[0].Trim();

                                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                        Voucher = string.Empty;
                                        voucherID = string.Empty;
                                    }

                                    // DR                                               CR                    BOOKS
                                    //  trnsporter/insurance/tpu                        depot                  HO.

                                    VoucherTable.Clear();

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = motherdepotid;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    InvoiceTable.Clear();

                                    dr = InvoiceTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["InvoiceID"] = StockReceivedID;
                                    dr["InvoiceNo"] = invoice;
                                    dr["InvoiceAmt"] = -1 * Amount;
                                    dr["AmtPaid"] = 0;
                                    dr["VoucherType"] = "9";
                                    dr["BranchID"] = HOLeadger;
                                    dr["InvoiceDate"] = VchDate;
                                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                                    dr["InvoiceOthers"] = invoice;

                                    InvoiceTable.Rows.Add(dr);
                                    InvoiceTable.AcceptChanges();

                                    string XMLINVOICE1 = ConvertDatatableToXML(InvoiceTable);

                                    //Here invoiceiwse posting need to be passed here 
                                    //Leadger ID : TPU/Transporter Leadger ID (Debitleadger)
                                    //Invoice ID: Tpu received ID
                                    //Invoice No: Tpu Invoice No.
                                    //Amt :  Amount
                                    //VoucherType: Receipt
                                    //BranchID  : HO Leadger
                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE1, "", "Y");

                                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                    {
                                        flag = 2;
                                        return 2;
                                    }
                                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                    {
                                        flag = 3;
                                        return 3;
                                    }
                                    else
                                    {
                                        String[] vou5 = Voucher.Split('|');
                                        voucherID = vou5[0].Trim();

                                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                        Voucher = string.Empty;
                                        voucherID = string.Empty;
                                    }
                                }
                            }
                        }
                    }

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Account Posting for Transporter Bill for VAT
        protected int AccountTransporterBillPostingForVAT(string TransporterBillID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string IsautopostingtoHO, string IsREVERSECHARGE)
        {
            int flag = 0;
            int postingflag = 1;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string Remarks = ""; // 24012020

            string CarriageInwardsLedgers = "", CarriageOutwardsLedgers = "", CarriageDepotTransferLedgers = "", ServiceTaxLeadger = "", ServiceTaxLeadger_Payable = "", TDSLeadger = "", HOLeadger = "", VchDate = "", TransporterID = "", BillTypeID = "";
            string TransporterName = "", invoice = "", invoicedate = "", InvoiceID = "", ServiceTax_Paid = "", Isautoposting = "", PostingDepotID = "", PostingDepotName = "", LRGRNO = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", GNGNo = "";
            decimal NETVALUE = 0, LedgerPostingValue = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                                CR                             BOOKS
            //TRANPORATION A/C                   TO TDS                         HO.
            //SERVICE TAX P&L                    TO TRANPORTER A/C 
            //                                   TO SERVICE TAX RECEIVABLE

            string Sql = "SELECT [CARRIAGE_INWARDS_ACC_LEADGER],[CARRIAGE_DEPOT_TRANSFER_ACC_LEDGER],[CARRIAGE_OUTWARDS_ACC_LEADGER1],[TDS_194C_ACC_LEADGER],[SERVICETAX_PAYABLE_ACC_LEADGER],[SERVICETAX_TRANSPORT_ACC_LEDGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                CarriageInwardsLedgers = dt.Rows[0]["CARRIAGE_INWARDS_ACC_LEADGER"].ToString().Trim();
                CarriageOutwardsLedgers = dt.Rows[0]["CARRIAGE_OUTWARDS_ACC_LEADGER1"].ToString().Trim();
                CarriageDepotTransferLedgers = dt.Rows[0]["CARRIAGE_DEPOT_TRANSFER_ACC_LEDGER"].ToString().Trim();
                TDSLeadger = dt.Rows[0]["TDS_194C_ACC_LEADGER"].ToString().Trim();
                ServiceTaxLeadger = dt.Rows[0]["SERVICETAX_TRANSPORT_ACC_LEDGER"].ToString().Trim();
                ServiceTaxLeadger_Payable = dt.Rows[0]["SERVICETAX_PAYABLE_ACC_LEADGER"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TRANSPORTERBILLID,TRANSPORTERBILLNO,BILLAMOUNT,TDS,SERVICETAX,NETAMOUNT,BILLNO,TRANSPORTERID,TRANSPORTERNAME,TRANSPORTERBILLDATE,BILLTYPEID,DEPOTID," +
                  " SERVICETAX_PAID,LAVEL,ISTRANSFERTOHO,LRGRNO,GNGNO,REMARKS FROM Vw_Transporter_AccPosting_VAT WHERE TRANSPORTERBILLID='" + TransporterBillID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                ServiceTax_Paid = dt.Rows[0]["SERVICETAX_PAID"].ToString().Trim();
                invoice = dt.Rows[0]["BILLNO"].ToString();
                BillTypeID = dt.Rows[0]["BILLTYPEID"].ToString();
                VchDate = dt.Rows[0]["TRANSPORTERBILLDATE"].ToString();
                invoicedate = dt.Rows[0]["TRANSPORTERBILLDATE"].ToString();
                TransporterID = dt.Rows[0]["TRANSPORTERID"].ToString();
                TransporterName = dt.Rows[0]["TRANSPORTERNAME"].ToString();
                InvoiceID = dt.Rows[0]["TRANSPORTERBILLID"].ToString();
                NETVALUE = Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"].ToString().Trim());
                PostingDepotID = dt.Rows[0]["DEPOTID"].ToString();
                //IsautopostingtoHO = dt.Rows[0]["ISTRANSFERTOHO"].ToString();
                LRGRNO = dt.Rows[0]["LRGRNO"].ToString();
                GNGNo = dt.Rows[0]["GNGNO"].ToString();
                Remarks = Convert.ToString(dt.Rows[0]["REMARKS"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + PostingDepotID + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                }

                //Sql = "SELECT ISACCPOSTING_TOHO FROM M_TPU_TRANSPORTER WHERE ID='" + TransporterID + "'";
                //Isautoposting = (string)db.GetSingleValue(Sql);

                DataRow dr = VoucherTable.NewRow();

                if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                {

                    //TDS
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TDSLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //TRANPORTER A/C

                LedgerPostingValue = Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]);

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(TransporterID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(LedgerPostingValue);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(LedgerPostingValue);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]) > 0)
                {

                    //TO SERVICE TAX RECEIVABLE
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(ServiceTaxLeadger_Payable);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                //TRANPORATION A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();

                if (BillTypeID.IndexOf("(") > 0)
                {
                    BillTypeID = BillTypeID.Substring(BillTypeID.IndexOf("(") + 1, 6);
                }

                if (BillTypeID == "STKINV")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageOutwardsLedgers);
                }
                else if (BillTypeID == "STKDES")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageInwardsLedgers);
                }
                else if (BillTypeID == "STKTRN")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageDepotTransferLedgers);
                }
                else
                {
                    dr["LedgerId"] = Convert.ToString(CarriageInwardsLedgers);
                }

                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["BILLAMOUNT"]);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["BILLAMOUNT"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]) > 0)
                {
                    //SERVICE TAX
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(ServiceTaxLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                XML = ConvertDatatableToXML(VoucherTable);

                InvoiceTable.Clear();

                dr = InvoiceTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = TransporterID;
                dr["LedgerName"] = "";
                dr["InvoiceID"] = TransporterBillID;
                dr["InvoiceNo"] = invoice;
                dr["InvoiceAmt"] = LedgerPostingValue;
                dr["AmtPaid"] = 0;
                dr["VoucherType"] = "9";
                dr["BranchID"] = HOLeadger;
                dr["InvoiceDate"] = VchDate;
                dr["InvoiceBranchID"] = REFERENCELEDGERID;
                dr["InvoiceOthers"] = GNGNo;

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                //string Narration = "Being goods transported by " + TransporterName + " against bill No." + invoice + " dated " + VchDate + " with LRGR No " + LRGRNO + ".";
                string Narration = Remarks;
                Voucher = VchEntry.InsertVoucherDetails("5", "Purchases Bill Transport", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");


                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, TransporterBillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;
                }



                if (PostingDepotID != HOLeadger && ISPOSTINGTOHO == "Y")        // if entry from HO than not posting to HO
                {

                    VoucherTable.Clear();

                    // DR                                CR                             BOOKS
                    //TRANPORTER A/C                   TO HO                            DEPOT.
                    if (Isautoposting == "Y")
                    {
                        //TRANPORTER A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TransporterID);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                    {

                        //TDS
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TDSLeadger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]) > 0)
                    {

                        //TO SERVICE TAX RECEIVABLE
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(ServiceTaxLeadger_Payable);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }


                    //TO HO  A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");

                    if (Isautoposting == "Y")
                    {
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }
                    else
                    {
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));

                        decimal sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]);

                        if (sumvalue == 0)
                        {
                            postingflag = 0;
                        }
                    }

                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    if (Isautoposting == "Y")
                    {
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }
                    else
                    {
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    if (postingflag == 1)
                    {
                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "(Auto) Being goods transported by " + TransporterName + " against bill No." + invoice + " dated + " + VchDate + " with LRGR No " + LRGRNO + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou3 = Voucher.Split('|');
                            voucherID = vou3[0].Trim();

                            VchEntry.VoucherDetails(voucherID, TransporterBillID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }
                    }

                    postingflag = 1;

                    VoucherTable.Clear();

                    // DR                           CR                              BOOKS
                    // DEPOT                       TRANPORTER A/C                   HO.


                    if (Isautoposting == "Y")
                    {


                        //TRANPORTER A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TransporterID);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                    {

                        //TDS
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TDSLeadger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]) > 0)
                    {

                        //TO SERVICE TAX RECEIVABLE
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(ServiceTaxLeadger_Payable);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["SERVICETAX"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }


                    //TO HO  A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(REFERENCELEDGERID);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");

                    if (Isautoposting == "Y")
                    {
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }
                    else
                    {
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));

                        decimal sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]);

                        if (sumvalue == 0)
                        {
                            postingflag = 0;
                        }
                    }

                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    if (Isautoposting == "Y")
                    {
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }
                    else
                    {
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAX"]));
                    }

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    if (postingflag == 1)
                    {

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "(Auto) Being goods transported by " + TransporterName + " against bill No." + invoice + " dated " + VchDate + " with LRGR No " + LRGRNO + " transferred from " + REFERENCELEDGERNAME + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou4 = Voucher.Split('|');
                            voucherID = vou4[0].Trim();

                            VchEntry.VoucherDetails(voucherID, TransporterBillID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }
                    }

                }

                flag = 1;
            }
            return flag;
        }
        #endregion

        #region Account Posting for Transporter Bill for GST
        protected int AccountTransporterBillPosting(string TransporterBillID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string IsautopostingtoHO, string IsREVERSECHARGE)
        {
            int flag = 0;
            int postingflag = 1;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            
            string CarriageInwardsLedgers = "", CarriageOutwardsLedgers = "", CarriageDepotTransferLedgers = "", Input_CGST_Ledger = "", Input_SGST_Ledger = "", Input_IGST_Ledger = "",
                   Input_CGST_RCM_Ledger = "", Input_SGST_RCM_Ledger = "", Input_IGST_RCM_Ledger = "", GNGNo = "", kolkataDepotID = "", NARRATION = "", BILLDATE = "";
            string Output_CGST_Ledger = "", Output_SGST_Ledger = "", Output_IGST_Ledger = "", TDSLeadger = "", HOLeadger = "", VchDate = "", TransporterID = "", BillTypeID = "", ISPOSTINGTOHO = "";
            string TransporterName = "", invoice = "", invoicedate = "", InvoiceID = "", PostingDepotID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", LRGRNO = "";
            decimal NETVALUE = 0, LedgerPostingValue = 0, TotalTax = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            string Sql = " SELECT [CARRIAGE_INWARDS_ACC_LEADGER],[CARRIAGE_DEPOT_TRANSFER_ACC_LEDGER],[CARRIAGE_OUTWARDS_ACC_LEADGER1],[TDS_194C_ACC_LEADGER],[INPUT_CGST],[INPUT_SGST],[INPUT_IGST]," +
                         " [OUTPUT_CGST_RCM],[OUTPUT_SGST_RCM],[OUTPUT_IGST_RCM],INPUT_SGST_RCM,INPUT_CGST_RCM,INPUT_IGST_RCM,KOLKATADEPOTID FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                CarriageInwardsLedgers = Convert.ToString(dt.Rows[0]["CARRIAGE_INWARDS_ACC_LEADGER"]);
                CarriageOutwardsLedgers = Convert.ToString(dt.Rows[0]["CARRIAGE_OUTWARDS_ACC_LEADGER1"]);
                CarriageDepotTransferLedgers = Convert.ToString(dt.Rows[0]["CARRIAGE_DEPOT_TRANSFER_ACC_LEDGER"]);
                TDSLeadger = Convert.ToString(dt.Rows[0]["TDS_194C_ACC_LEADGER"]);
                Input_CGST_Ledger = Convert.ToString(dt.Rows[0]["INPUT_CGST"]);
                Input_SGST_Ledger = Convert.ToString(dt.Rows[0]["INPUT_SGST"]);
                Input_IGST_Ledger = Convert.ToString(dt.Rows[0]["INPUT_IGST"]);
                Output_CGST_Ledger = Convert.ToString(dt.Rows[0]["OUTPUT_CGST_RCM"]);
                Output_SGST_Ledger = Convert.ToString(dt.Rows[0]["OUTPUT_SGST_RCM"]);
                Output_IGST_Ledger = Convert.ToString(dt.Rows[0]["OUTPUT_IGST_RCM"]);
                Input_CGST_RCM_Ledger = Convert.ToString(dt.Rows[0]["INPUT_CGST_RCM"]);
                Input_SGST_RCM_Ledger = Convert.ToString(dt.Rows[0]["INPUT_SGST_RCM"]);
                Input_IGST_RCM_Ledger = Convert.ToString(dt.Rows[0]["INPUT_IGST_RCM"]);
                kolkataDepotID = Convert.ToString(dt.Rows[0]["KOLKATADEPOTID"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TRANSPORTERBILLID,TRANSPORTERBILLNO,BILLAMOUNT,TDS,TOTALCGST,TOTALSGST,TOTALIGST,NETAMOUNT,BILLNO,TRANSPORTERID,TRANSPORTERNAME,TRANSPORTERBILLDATE,BILLTYPEID,DEPOTID," +
                  " SERVICETAX_PAID,LAVEL,ISTRANSFERTOHO,LRGRNO,REVERSECHARGE,GNGNO,NARRATION,BILLDATE FROM Vw_Transporter_AccPosting WHERE TRANSPORTERBILLID='" + TransporterBillID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0]["BILLNO"].ToString();
                BillTypeID = dt.Rows[0]["BILLTYPEID"].ToString();
                VchDate = dt.Rows[0]["TRANSPORTERBILLDATE"].ToString();
                invoicedate = dt.Rows[0]["TRANSPORTERBILLDATE"].ToString();
                TransporterID = dt.Rows[0]["TRANSPORTERID"].ToString();
                TransporterName = dt.Rows[0]["TRANSPORTERNAME"].ToString();
                InvoiceID = dt.Rows[0]["TRANSPORTERBILLID"].ToString();
                NETVALUE = Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"].ToString().Trim());
                PostingDepotID = dt.Rows[0]["DEPOTID"].ToString();
                //IsautopostingtoHO = dt.Rows[0]["ISTRANSFERTOHO"].ToString();
                LRGRNO = dt.Rows[0]["LRGRNO"].ToString();
                GNGNo = dt.Rows[0]["GNGNO"].ToString();
                NARRATION = dt.Rows[0]["NARRATION"].ToString();
                BILLDATE = dt.Rows[0]["BILLDATE"].ToString();

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + PostingDepotID + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                }

                //Sql = "SELECT ISACCPOSTING_TOHO FROM M_TPU_TRANSPORTER WHERE ID='" + TransporterID + "'";
                //Isautoposting = (string)db.GetSingleValue(Sql);

                DataRow dr = VoucherTable.NewRow();

                //TRANPORATION A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();

                if (BillTypeID.IndexOf("(") > 0)
                {
                    BillTypeID = BillTypeID.Substring(BillTypeID.IndexOf("(") + 1, 6);
                }

                if (BillTypeID == "STKINV")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageOutwardsLedgers);
                }
                else if (BillTypeID == "STKSR")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageInwardsLedgers);
                }
                else if (BillTypeID == "STKDES")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageInwardsLedgers);
                }
                else if (BillTypeID == "STKTRN")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageDepotTransferLedgers);
                }
                else if (BillTypeID == "DPTRCVD")
                {
                    dr["LedgerId"] = Convert.ToString(CarriageDepotTransferLedgers);
                }
                else
                {
                    dr["LedgerId"] = Convert.ToString(CarriageInwardsLedgers);
                }


                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["BILLAMOUNT"]);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["BILLAMOUNT"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) > 0)
                {
                    //TO INPUT CGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();

                    if (IsREVERSECHARGE == "Y")
                    {
                        dr["LedgerId"] = Convert.ToString(Input_CGST_RCM_Ledger);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(Input_CGST_Ledger);
                    }

                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    TotalTax = TotalTax + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]);
                }

                if (Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) > 0)
                {
                    //TO INPUT SGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();

                    if (IsREVERSECHARGE == "Y")
                    {
                        dr["LedgerId"] = Convert.ToString(Input_SGST_RCM_Ledger);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(Input_SGST_Ledger);
                    }

                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    TotalTax = TotalTax + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]);
                }

                if (Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]) > 0)
                {
                    //TO INPUT IGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();

                    if (IsREVERSECHARGE == "Y")
                    {
                        dr["LedgerId"] = Convert.ToString(Input_IGST_RCM_Ledger);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(Input_IGST_Ledger);
                    }

                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    TotalTax = TotalTax + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]);
                }


                if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                {
                    //TDS
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TDSLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //TRANPORTER A/C

                if (IsREVERSECHARGE == "Y")
                {
                    LedgerPostingValue = Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]);
                }
                else
                {
                    LedgerPostingValue = Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax;
                }

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(TransporterID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(LedgerPostingValue);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(LedgerPostingValue);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (IsREVERSECHARGE == "Y")
                {
                    if (Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) > 0)
                    {
                        //TO OUTPUT CGST
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(Output_CGST_Ledger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) > 0)
                    {
                        //TO OUTPUT SGST
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(Output_SGST_Ledger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]) > 0)
                    {
                        //TO OUTPUT IGST
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(Output_IGST_Ledger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                InvoiceTable.Clear();

                dr = InvoiceTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = TransporterID;
                dr["LedgerName"] = "";
                dr["InvoiceID"] = TransporterBillID;
                dr["InvoiceNo"] = invoice;
                dr["InvoiceAmt"] = LedgerPostingValue;
                dr["AmtPaid"] = 0;
                dr["VoucherType"] = "9";
                if (kolkataDepotID == PostingDepotID)    /* if depot id id kolkata then invoice details tag with HO LEDGER */
                {
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceBranchID"] = HOLeadger;
                }
                else
                {
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceBranchID"] = PostingDepotID;
                }
                dr["InvoiceDate"] = VchDate;
                dr["InvoiceOthers"] = GNGNo;
                dr["BILLDATE"] = BILLDATE;

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                //string Narration = NARRATION + Environment.NewLine + " against bill No." + invoice + " dated " + BILLDATE + " with LRGR No " + LRGRNO + ".";
                string Narration = NARRATION;
                Voucher = VchEntry.InsertVoucherDetails("5", "Purchases Bill Transport", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, TransporterBillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;
                }


                if (PostingDepotID != HOLeadger && ISPOSTINGTOHO == "Y")        // if entry from HO than not posting to HO
                {
                    VoucherTable.Clear();

                    // DR                                CR                             BOOKS
                    //TRANPORTER A/C                   TO HO                            DEPOT.
                    if (IsautopostingtoHO == "Y")               /* IsautopostingtoHO flag comes from approval page checkbox */
                    {
                        //TRANPORTER A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TransporterID);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        // 24/12/2018 
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));
                        }
                        else
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax);

                        }

                       // dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                         dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");

                        // 24/12/2018 
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"])); 
                        }
                        else
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax); // ***

                        }
                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                    {

                        //TDS
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TDSLeadger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) > 0)
                    //{
                    //    //TO OUTPUT CGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Output_CGST_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("0");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) > 0)
                    //{
                    //    //TO OUTPUT SGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Output_SGST_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("0");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]) > 0)
                    //{

                    //    //TO OUTPUT IGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Output_IGST_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("0");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}


                    //TO HO  A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");

                    if (IsautopostingtoHO == "Y")   /* IsautopostingtoHO flag comes from approval page checkbox */
                    {
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        // 24/12/2018 
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                        }
                        else
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + TotalTax);

                        }
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                    }
                    else
                    {
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]));

                        decimal sumvalue = 0;
                        //sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]);
                        sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]);

                        if (sumvalue == 0)
                        {
                            postingflag = 0;
                        }
                    }

                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    if (IsautopostingtoHO == "Y")
                    {
                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        // 24/12/2018 
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                        }
                        else
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + TotalTax);

                        }
                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                    }
                    else
                    {
                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]));
                    }

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    if (postingflag == 1)
                    {
                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "(Auto)" + Environment.NewLine + NARRATION + Environment.NewLine + " against bill No." + invoice + " dated + " + BILLDATE + " with LRGR No " + LRGRNO + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou3 = Voucher.Split('|');
                            voucherID = vou3[0].Trim();

                            VchEntry.VoucherDetails(voucherID, TransporterBillID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }
                    }

                    postingflag = 1;

                    VoucherTable.Clear();

                    // DR                           CR                              BOOKS
                    // DEPOT                       TRANPORTER A/C                   HO.


                    if (IsautopostingtoHO == "Y")   /* IsautopostingtoHO flag comes from approval page checkbox */
                    {
                        //TRANPORTER A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TransporterID);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");

                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"])); 
                        }
                        else
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax);  // ***

                        }
                        
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"])); 
                        }
                        else
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax);  // ***

                        }
                        

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TDS"]) > 0)
                    {
                        //TDS
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(TDSLeadger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["TDS"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDS"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) > 0)
                    //{
                    //    //TO INPUT CGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Input_CGST_RCM_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("1");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALCGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) > 0)
                    //{
                    //    //TO INPUT SGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Input_SGST_RCM_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("1");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALSGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}

                    //if (Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]) > 0)
                    //{
                    //    //TO INPUT IGST
                    //    dr = VoucherTable.NewRow();
                    //    dr["GUID"] = Guid.NewGuid();
                    //    dr["LedgerId"] = Convert.ToString(Input_IGST_RCM_Ledger);
                    //    dr["LedgerName"] = "";
                    //    dr["TxnType"] = Convert.ToString("1");
                    //    dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);
                    //    dr["BankID"] = Convert.ToString("");
                    //    dr["BankName"] = "";
                    //    dr["ChequeNo"] = Convert.ToString("");
                    //    dr["ChequeDate"] = Convert.ToString("");
                    //    dr["IsChequeRealised"] = Convert.ToString("N");
                    //    dr["Remarks"] = Convert.ToString("");
                    //    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALIGST"]);

                    //    VoucherTable.Rows.Add(dr);
                    //    VoucherTable.AcceptChanges();
                    //}


                    //TO HO  A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(REFERENCELEDGERID);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");

                    if (IsautopostingtoHO == "Y")       /* IsautopostingtoHO flag comes from approval page checkbox */
                    {
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        // 24/12/2018
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                        }
                        else
                        {
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + TotalTax);

                        }
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                    }
                    else
                    {
                        //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]));

                        decimal sumvalue = 0;
                        //sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]);
                        sumvalue = Convert.ToDecimal(dt.Rows[0]["TDS"]);

                        if (sumvalue == 0)
                        {
                            postingflag = 0;
                        }
                    }

                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");

                    if (IsautopostingtoHO == "Y")           /* IsautopostingtoHO flag comes from approval page checkbox */
                    {
                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        // 24/12/2018
                        if (IsREVERSECHARGE == "Y")
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                        }
                        else
                        {
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + TotalTax);

                        }

                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
                    }
                    else
                    {
                        //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TDS"]));
                    }

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    if (postingflag == 1)
                    {
                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "(Auto)" + Environment.NewLine + NARRATION + Environment.NewLine + "against bill No." + invoice + " dated " + BILLDATE + " with LRGR No " + LRGRNO + " transferred from " + REFERENCELEDGERNAME + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou4 = Voucher.Split('|');
                            voucherID = vou4[0].Trim();

                            VchEntry.VoucherDetails(voucherID, TransporterBillID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }
                    }
                }
                flag = 1;
            }
            return flag;
        }
        #endregion

        #region Account Posting for Sale Invoice
        protected int AccountSaleInvoicePosting(string SaleInvoiceID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName, string InvoiceFor)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string SaleLeadger = "", DiscountLeadger = "", RoundOffLeadger = "", HOLeadger = "", motherdepotid = "", VchDate = "", DistributorID = "", transporter = "", kolkataDepotID = "", IsParentHoLedger = "N";
            string DistributorName = "", invoice = "", invoicedate = "", InvoiceID = "", carriage_outward = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", DISTRIBUTORID_TOHO = "", GETPASSNO = "", DELIVERY_CHALLAN_ACC_ID = "", ISCHALLAN = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                  BOOKS
            //PARTY A/C                     SALE                 DEPOT.
            //DISCOUNT                      ROUND OFF
            //                              VAT TAX/CST

            string Sql = "SELECT [ROUNDOFF_ACC_LEADGER],[DISCOUNT_ACC_LEADGER],[CARRIAGE_OUTWARDS_ACC_LEADGER1],[KOLKATADEPOTID],[DELIVERY_CHALLAN_ACC_ID] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString().Trim();
                DiscountLeadger = dt.Rows[0]["DISCOUNT_ACC_LEADGER"].ToString().Trim();
                carriage_outward = dt.Rows[0]["CARRIAGE_OUTWARDS_ACC_LEADGER1"].ToString().Trim();
                kolkataDepotID = dt.Rows[0]["KOLKATADEPOTID"].ToString().Trim();
                DELIVERY_CHALLAN_ACC_ID = dt.Rows[0]["DELIVERY_CHALLAN_ACC_ID"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            if (InvoiceFor == "1")      //  ----> 1 - Normal Depot Invoice to distributor/retailer/CSD/CPC etc for FG/POP product
            {
                Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE], " +
                       " [DISTRIBUTORID_TOHO],[ISPARENTHOLEDGER],[GETPASSNO],[SALE_ACC_LEADGER],[ISCHALLAN] FROM [Vw_SaleInvoice_AccPosting] WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }
            else if (InvoiceFor == "2")  //  ----> 2 - Trading Invoice from Factory or Depot for RM/PM product
            {
                Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE], " +
                       " [DISTRIBUTORID_TOHO],[ISPARENTHOLEDGER],[GETPASSNO],[SALE_ACC_LEADGER],[ISCHALLAN] FROM [Vw_TradingSaleInvoice_AccPosting] WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }
            else
            {
                Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE], " +
                       " [DISTRIBUTORID_TOHO],[ISPARENTHOLEDGER],[GETPASSNO],[SALE_ACC_LEADGER],[ISCHALLAN] FROM [Vw_SaleInvoice_AccPosting] WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                SaleLeadger = dt.Rows[0]["SALE_ACC_LEADGER"].ToString().Trim();
                DistributorID = dt.Rows[0]["DISTRIBUTORID"].ToString().Trim();
                motherdepotid = dt.Rows[0]["DEPOTID"].ToString().Trim();
                VchDate = dt.Rows[0]["SALEINVOICEDATE"].ToString().Trim();
                DistributorName = dt.Rows[0]["DISTRIBUTORNAME"].ToString().Trim();
                invoice = dt.Rows[0]["SALEINVOICENO"].ToString().Trim();
                invoicedate = dt.Rows[0]["SALEINVOICEDATE"].ToString().Trim();
                transporter = dt.Rows[0]["TRANSPORTERID"].ToString().Trim();
                InvoiceID = dt.Rows[0]["SALEINVOICEID"].ToString().Trim();
                DISTRIBUTORID_TOHO = dt.Rows[0]["DISTRIBUTORID_TOHO"].ToString().Trim();
                IsParentHoLedger = dt.Rows[0]["ISPARENTHOLEDGER"].ToString().Trim();
                GETPASSNO = dt.Rows[0]["GETPASSNO"].ToString().Trim();
                ISCHALLAN = dt.Rows[0]["ISCHALLAN"].ToString().Trim();
                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                }

                DataRow dr = VoucherTable.NewRow();

                decimal TAXVALUE = 0;
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        //VAT TAX/CST
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";

                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                }


                decimal TOTALSALEINVOICEVALUE = 0;

                TOTALSALEINVOICEVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]);
                //SALE
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(SaleLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]));
                TOTALVALUE = (Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) != 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //PARTY A/C 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(DistributorID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                //dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALSALEINVOICEVALUE) - Convert.ToDecimal(TAXVALUE));
                dr["Amount"] = Convert.ToString(TOTALSALEINVOICEVALUE);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]) > 0)
                {

                    //CARRIAGE OUTWARD A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(carriage_outward);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["GROSSREBATEVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["GROSSREBATEVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //DISCOUNT
                if (DiscountLeadger != "" && Convert.ToDecimal(dt.Rows[0]["ADJUSTMENT"]) != 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(DistributorID);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ADJUSTMENT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ADJUSTMENT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                string XMLINVOICE = "";

                string VoucherID = string.Empty;
                string VoucherType = string.Empty;
                if (ISCHALLAN == "Y")
                {
                    VoucherID = "19";
                    VoucherType = "Challan";
                }
                else
                {
                    VoucherID = "11";
                    VoucherType = "Sales";
                }

                if (REFERENCELEDGERID == HOLeadger && (IsParentHoLedger == "Y" || DISTRIBUTORID_TOHO == ""))    /* This posting only for Kolkata Depot only */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = SaleInvoiceID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALSALEINVOICEVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = HOLeadger;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails(VoucherID, VoucherType, REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO == "N") && (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() != "Y"))
                {
                    /* IF POSTING NOT TRANSFER TO HO THEN INVOICE DETAILS TAGING WITH SAME DEPOT */
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = SaleInvoiceID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALSALEINVOICEVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = motherdepotid;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails(VoucherID, VoucherType, REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails(VoucherID, VoucherType, REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
                }


                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else if (Voucher.Contains("|"))
                {
                    String[] vou = Voucher.Split('|');
                    voucherID = vou[0].Trim();

                    VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (REFERENCELEDGERID == motherdepotid && ISPOSTINGTOHO == "Y")
                    {
                        if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                        {

                            // DR                           CR                              BOOKS
                            // HO                           TO PARTY A/C                     DEPOT.

                            VoucherTable.Clear();

                            //TO PART A/C 
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DistributorID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            //HO
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = HOLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + " transferred to HO.";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou1 = Voucher.Split('|');
                                voucherID = vou1[0].Trim();

                                VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }



                            // DR                               CR                      BOOKS
                            // PARTY A/C                        DEPOT                   HO.

                            VoucherTable.Clear();

                            //DEPOT
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = REFERENCELEDGERID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            //dr["Amount"] = TOTALVALUE;
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            //PARTY A/C
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();

                            if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                            {
                                dr["LedgerId"] = DISTRIBUTORID_TOHO;
                            }
                            else
                            {
                                dr["LedgerId"] = DistributorID;
                            }
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DISTRIBUTORID_TOHO;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = SaleInvoiceID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALSALEINVOICEVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "10";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = invoicedate;
                            dr["InvoiceBranchID"] = motherdepotid;
                            dr["InvoiceOthers"] = GETPASSNO;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                            //Here invoiceiwse posting need to be passed here 
                            //Leadger ID : TPU Leadger ID
                            //Invoice ID: Tpu received ID
                            //Invoice No: Tpu Invoice No.
                            //Amt :  TOTALVALUE
                            //VoucherType: Payment
                            //BranchID  : HO Leadger


                            Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }
                    else if (REFERENCELEDGERID == HOLeadger && IsParentHoLedger == "N")    /*This posting only for Kolkata Depot only (Posting Only HO Books)*/
                    {
                        if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                        {
                            // DR                               CR                      BOOKS
                            // PARTY A/C                        DEPOT                   HO.

                            VoucherTable.Clear();

                            //PARTY A/C
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DistributorID;                         /* FOR NULLIFY */
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            //PARTY HO A/C
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DISTRIBUTORID_TOHO;                    /* FOR NULLIFY */
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALSALEINVOICEVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DISTRIBUTORID_TOHO;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = SaleInvoiceID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALSALEINVOICEVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "10";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = invoicedate;
                            dr["InvoiceBranchID"] = HOLeadger;
                            dr["InvoiceOthers"] = GETPASSNO;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                            //Here invoiceiwse posting need to be passed here 
                            //Leadger ID : TPU Leadger ID
                            //Invoice ID: Tpu received ID
                            //Invoice No: Tpu Invoice No.
                            //Amt :  TOTALVALUE
                            //VoucherType: Payment
                            //BranchID  : HO Leadger


                            Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }
                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Bank Receipt
        protected int AccountBankReceiptPosting(string VoucherID, string Finyr, string Uid, string VoucherNo, string RealishedDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", Ledgerid = "", Depotid = "", Depotname = "", invoice = "", InvoiceID = "", chequeno = "", chequedate = "", payementtype = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
            decimal TotalAmount = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            DataTable VouParticularsTable = new DataTable();

            // DR                           CR                  BOOKS
            //HO A/C                   BANK A/C                 DEPOT.

            string Sql = "";

            Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
            REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

            DataTable dtbranch = new DataTable();
            Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
            dtbranch = db.GetData(Sql);

            if (dtbranch.Rows.Count > 0)
            {
                ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";
            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME] FROM [Vw_VoucherReceipt_Partial_AccPosting]" +
                  " WHERE AccEntryID='" + VoucherID + "'";

            dt = db.GetData(Sql);

            if (dt.Rows.Count > 0)
            {
                /*CONTINUE ONLY FOR PARIAL TRANSFER BANK LEDGER AMOUNT*/
            }
            else
            {
                Sql = " SELECT AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME] FROM [Vw_AccPosting]" +
                      " WHERE AccEntryID='" + VoucherID + "'";

                dt = db.GetData(Sql);
            }
            if (dt.Rows.Count > 0)
            {
                Ledgerid = dt.Rows[0]["LedgerId"].ToString();
                Depotid = dt.Rows[0]["BranchID"].ToString();
                Depotname = dt.Rows[0]["BranchName"].ToString();
                invoice = dt.Rows[0]["VoucherNo"].ToString();
                VchDate = dt.Rows[0]["VoucherDate"].ToString();
                InvoiceID = dt.Rows[0]["AccEntryID"].ToString();
                chequeno = dt.Rows[0]["ChequeNo"].ToString();
                chequedate = dt.Rows[0]["ChequeDate"].ToString();
                payementtype = dt.Rows[0]["PAYMENTTYPENAME"].ToString();

                if (REFERENCELEDGERID == Depotid && ISPOSTINGTOHO == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();
                    DataRow drPar = VouParticularsTable.NewRow();

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LedgerId"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    //HO A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                    string Narration = "(Auto) Transfered to HO " + payementtype + " no " + chequeno + " dated " + chequedate + " of Payable at " + REFERENCELEDGERNAME + ".";
                    Voucher = VchEntry.InsertVoucherDetails("15", "Advance Payment", REFERENCELEDGERID, REFERENCELEDGERNAME, "B", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        VchEntry.ParticulrasInsert(voucherID, InvoiceID, "16");

                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    // DR                           CR                              BOOKS
                    // BANK A/C                     DEPOT A/C                       HO.

                    VoucherTable.Clear();

                    //TO DEPOT A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = REFERENCELEDGERID;
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();


                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LedgerId"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString(dt.Rows[counter]["ChequeNo"]);
                        dr["ChequeDate"] = Convert.ToString(dt.Rows[counter]["ChequeDate"]);
                        dr["IsChequeRealised"] = Convert.ToString("Y");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    XML = ConvertDatatableToXML(VoucherTable);

                    Narration = "(Auto) Bank Receipt No : " + invoice + " Dated : " + VchDate + " Transfered to HO " + payementtype + " No " + chequeno + " Dated " + chequedate + ".";
                    Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", HOLeadger, "", "B", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        VchEntry.ParticulrasInsert(voucherID, InvoiceID, "16");

                        string sql = " UPDATE Acc_Posting SET ChequeRealisedDate = CONVERT(DATETIME,'" + RealishedDate + "',103) WHERE TxnType='0' AND AccEntryID ='" + voucherID + "'";
                        db.HandleData(sql);

                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    flag = 1;
                }
                else
                {
                    flag = 1; // only for kolkata depot & Factory
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Bank Payment
        protected int AccountBankPaymentPosting(string VoucherID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName, string ISPOSTINGTOHO_PAGE)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", Ledgerid = "", Depotid = "", Depotname = "", invoice = "", InvoiceID = "", chequeno = "", chequedate = "", payementtype = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "";
            string Costcenterledgerid = string.Empty;
            string ISPOSTINGTOHO_MASTER = "N";
            decimal TotalAmount = 0;
            DataTable dt = new DataTable();
            DataTable dtcostcenter = new DataTable();
            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            DataTable VouParticularsTable = new DataTable();

            // CR                           DR                  BOOKS
            //HO A/C                   BANK A/C                 DEPOT.

            string Sql = "";

            Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
            REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

            DataTable dtbranch = new DataTable();
            Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
            dtbranch = db.GetData(Sql);

            if (dtbranch.Rows.Count > 0)
            {
                ISPOSTINGTOHO_MASTER = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";
            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }
            ClsVoucherEntry VchEntry = new ClsVoucherEntry();

            Sql = " SELECT AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME],[IsCostCenter] FROM [Vw_BankPayment_AccPosting]" +
                    " WHERE AccEntryID='" + VoucherID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                Ledgerid = dt.Rows[0]["LedgerId"].ToString();
                Depotid = dt.Rows[0]["BranchID"].ToString();
                Depotname = dt.Rows[0]["BranchName"].ToString();
                invoice = dt.Rows[0]["VoucherNo"].ToString();
                VchDate = dt.Rows[0]["VoucherDate"].ToString();
                InvoiceID = dt.Rows[0]["AccEntryID"].ToString();
                chequeno = dt.Rows[0]["ChequeNo"].ToString();
                chequedate = dt.Rows[0]["ChequeDate"].ToString();
                payementtype = dt.Rows[0]["PAYMENTTYPENAME"].ToString();

                if (REFERENCELEDGERID == Depotid && ISPOSTINGTOHO_PAGE == "Y" && ISPOSTINGTOHO_MASTER == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();
                    DataRow drPar = VouParticularsTable.NewRow();

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LedgerId"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    //HO A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    string Narration = "(Auto) Transfered to HO " + payementtype + " no " + chequeno + " dated " + chequedate + " of Payable at " + REFERENCELEDGERNAME + ".";
                    Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", REFERENCELEDGERID, REFERENCELEDGERNAME, "B", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        /*VchEntry.ParticulrasInsert(voucherID, InvoiceID, "15");*/

                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    // CR                           DR                              BOOKS
                    // BANK A/C                     DEPOT A/C                       HO.

                    VoucherTable.Clear();

                    //TO DEPOT A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = REFERENCELEDGERID;
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();


                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LedgerId"]);

                        Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString(dt.Rows[counter]["ChequeNo"]);
                        dr["ChequeDate"] = Convert.ToString(dt.Rows[counter]["ChequeDate"]);
                        dr["IsChequeRealised"] = Convert.ToString("Y");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                        dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    XML = ConvertDatatableToXML(VoucherTable);

                    Sql = " SELECT AccEntryID,LedgerId,LedgerName,CostCatagoryID,CostCatagoryName,CostCenterID,CostCenterName,BranchID,BranchName,amount,BrandID,BrandName, " +
                             " ProductID,ProductName,DepartmentID,DepartmentName,FromDate,ToDate,NARRATION,TxnType,SYNCTAG FROM Vw_CostCenterDetails WHERE AccEntryID='" + InvoiceID + "' " +
                             " AND LEDGERID IN(select *  from dbo.fnSplit(('" + Costcenterledgerid + "'),','))";
                    dtcostcenter = db.GetData(Sql);           /* VoucherTypeID = 2 AND BranchID <> '24E1EF07-0A41-4470-B745-9E4BA164C837' */

                    string XMLCOSTCENTER = "";
                    if (dtcostcenter.Rows.Count > 0)
                    {
                        XMLCOSTCENTER = ConvertDatatableToXML(dtcostcenter);
                    }

                    Narration = "(Auto) Bank Receipt No : " + invoice + " Dated : " + VchDate + " Transfered to HO " + payementtype + " No " + chequeno + " Dated " + chequedate + ".";
                    Voucher = VchEntry.InsertVoucherDetails("15", "Advance Payment", HOLeadger, "", "B", VchDate, Finyr, Narration, Uid, "A", "", XML, null, XMLCOSTCENTER, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        /*VchEntry.ParticulrasInsert(voucherID, InvoiceID, "15");*/

                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    flag = 1;
                }
                else
                {
                    flag = 1; // only for kolkata depot & Factory
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Payment Request
        protected int AccountPaymentRequestPosting(string RequestID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", DistributorID = "", DistributorName = "", paymentdate = "", requestID = "", bankid = "", bankname = "", instrumentalcode = "";
            decimal AMOUNT = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            //DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            //InvoiceTable = CreateInvoiceTable();

            string Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT REQUESTID,REQUESTDATE,CUSTOMERID,CUSTOMERNAME,[MODEOFTXNID],[MODEOFTXNNAME],[BANKID],[BANKNAME],[INSTRUMENTALCODE],[TXNID],PAYMENTDATE,[AMOUNT] " +
                  " FROM Vw_PaymentRequest_AccPosting WHERE REQUESTID='" + RequestID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                VchDate = dt.Rows[0]["PAYMENTDATE"].ToString();
                paymentdate = dt.Rows[0]["PAYMENTDATE"].ToString();
                DistributorID = dt.Rows[0]["CUSTOMERID"].ToString();
                DistributorName = dt.Rows[0]["CUSTOMERNAME"].ToString();
                requestID = dt.Rows[0]["REQUESTID"].ToString();
                AMOUNT = Convert.ToDecimal(dt.Rows[0]["AMOUNT"].ToString().Trim());

                DataRow dr = VoucherTable.NewRow();

                //DISTRIBUTOR A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(DistributorID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["AMOUNT"]);
                dr["BankID"] = Convert.ToString(bankid);
                dr["BankName"] = Convert.ToString(bankname);
                dr["ChequeNo"] = Convert.ToString(instrumentalcode);
                dr["ChequeDate"] = Convert.ToString(paymentdate);
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["Amount"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                //HO A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(HOLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["AMOUNT"]);
                dr["BankID"] = Convert.ToString(bankid);
                dr["BankName"] = Convert.ToString(bankname);
                dr["ChequeNo"] = Convert.ToString(instrumentalcode);
                dr["ChequeDate"] = Convert.ToString(paymentdate);
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["Amount"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                XML = ConvertDatatableToXML(VoucherTable);

                //InvoiceTable.Clear();

                //dr = InvoiceTable.NewRow();
                //dr["GUID"] = Guid.NewGuid();
                //dr["LedgerId"] = DistributorID;
                //dr["LedgerName"] = "";
                //dr["InvoiceID"] = requestID;
                //dr["InvoiceNo"] = invoice;
                //dr["InvoiceAmt"] = AMOUNT;
                //dr["AmtPaid"] = 0;
                //dr["VoucherType"] = "16";
                //dr["BranchID"] = HOLeadger;
                //dr["InvoiceDate"] = invoicedate;
                //dr["InvoiceBranchID"] = REFERENCELEDGERID;
                //dr["InvoiceOthers"] = GETPASSNO;

                //InvoiceTable.Rows.Add(dr);
                //InvoiceTable.AcceptChanges();

                //string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                string Narration = "(Auto) Being goods sold to " + DistributorName + " against advance payment.";
                Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "N");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, requestID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;

                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Sale Return
        protected int AccountPostingFoeSaleReturn(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string ISPOSTING_TOHO_REFERENCELEDGERID = string.Empty;
            string REFERENCEDISTRIBUTORID = string.Empty;

            string PurchaseLeadger = "", RoundOffLeadger = "", HOLeadger = "", depotid = "", depotname = "", VchDate = "", DistributorID = "", DistributorName = "", IsParentHoLedger = "N";
            string transporter = "", invoice = "", invoicedate = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", GETPASSNO = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                          BOOKS
            //PURCHASE                      DISTRIBUTOR                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT [ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                RoundOffLeadger = Convert.ToString(dt.Rows[0]["ROUNDOFF_ACC_LEADGER"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = Convert.ToString(dt.Rows[0]["BRID"]);
            }

            Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALERETURNID],[ROUNDOFFVALUE],[DEPOTID],[DEPOTNAME],[DISTRIBUTORID],[DISTRIBUTORNAME],[SALERETURNDATE]," +
                  " SALERETURNNO,[TRANSPORTERID],[ISFINANCE_HO],[DISTRIBUTORID_TOHO],[GETPASSNO],[ISPARENTHOLEDGER],[SALERETURN_ACC_LEADGER] FROM [Vw_SaleReturn_AccPosting] WHERE SALERETURNID='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PurchaseLeadger = Convert.ToString(dt.Rows[0]["SALERETURN_ACC_LEADGER"]);
                depotid = Convert.ToString(dt.Rows[0]["DEPOTID"]);
                depotname = Convert.ToString(dt.Rows[0]["DEPOTNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["SALERETURNDATE"]);
                DistributorName = Convert.ToString(dt.Rows[0]["DISTRIBUTORNAME"]);
                invoice = Convert.ToString(dt.Rows[0]["SALERETURNNO"]);
                invoicedate = Convert.ToString(dt.Rows[0]["SALERETURNDATE"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                StockReceivedID = Convert.ToString(dt.Rows[0]["SALERETURNID"]);
                REFERENCEDISTRIBUTORID = Convert.ToString(dt.Rows[0]["DISTRIBUTORID_TOHO"]);
                GETPASSNO = Convert.ToString(dt.Rows[0]["GETPASSNO"]);
                IsParentHoLedger = Convert.ToString(dt.Rows[0]["ISPARENTHOLEDGER"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + depotid + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                }

                DataRow dr = VoucherTable.NewRow();

                decimal TOTALSALEINVOICEVALUE = 0;

                TOTALSALEINVOICEVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]);

                //DISTRIBUTORID
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["DISTRIBUTORID"]);
                DistributorID = Convert.ToString(dt.Rows[0]["DISTRIBUTORID"]);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALSALEINVOICEVALUE"]);

                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALSALEINVOICEVALUE"]);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                decimal TAXVALUE = 0;
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }


                //Purchase 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALSALEINVOICEVALUE) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALSALEINVOICEVALUE) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods return from " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + ".";

                if (REFERENCELEDGERID == HOLeadger && IsParentHoLedger == "Y")    /* This posting only for Kolkata Depot only */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = StockReceivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("8", "Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else if (REFERENCELEDGERID == depotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO == "N") && (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() != "Y"))
                {
                    /* IF POSTING NOT TRANSFER TO HO THEN INVOICE DETAILS TAGING WITH SAME DEPOT */
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = StockReceivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = depotid;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("8", "Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails("8", "Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
                }

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else if (Voucher.Contains("|"))
                {
                    String[] vou = Voucher.Split('|');
                    voucherID = vou[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (REFERENCELEDGERID == depotid && ISPOSTINGTOHO == "Y")
                    {
                        if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                        {

                            // DR                                   CR                   BOOKS
                            //DISTRIBUTOR                           HO                   DEPOT.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DistributorID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = HOLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods return from " + DistributorName + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", depotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou1 = Voucher.Split('|');
                                voucherID = vou1[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }

                            // DR                           CR                          BOOKS
                            // DEPOT                        DISTRIBUTOR                  HO.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = depotid;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                            {
                                dr["LedgerId"] = REFERENCEDISTRIBUTORID;
                            }
                            else
                            {
                                dr["LedgerId"] = DistributorID;
                            }
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = REFERENCEDISTRIBUTORID;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = StockReceivedID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "9";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = invoicedate;
                            dr["InvoiceBranchID"] = REFERENCELEDGERID;
                            dr["InvoiceOthers"] = GETPASSNO;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);


                            Narration = "(Auto) Being goods return from " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + depotname + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }
                    else if (REFERENCELEDGERID == HOLeadger && IsParentHoLedger == "N")    /*This posting only for Kolkata Depot only (Posting Only HO Books)*/
                    {
                        if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                        {
                            // DR                               CR                      BOOKS
                            // PARTY A/C                        DEPOT                   HO.

                            VoucherTable.Clear();

                            //PARTY HO A/C
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = REFERENCEDISTRIBUTORID;                         /* FOR NULLIFY */
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            //PARTY A/C
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = DistributorID;                    /* FOR NULLIFY */
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = REFERENCEDISTRIBUTORID;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = StockReceivedID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "9";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = invoicedate;
                            dr["InvoiceBranchID"] = REFERENCELEDGERID;
                            dr["InvoiceOthers"] = GETPASSNO;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                            //Here invoiceiwse posting need to be passed here 
                            //Leadger ID : TPU Leadger ID
                            //Invoice ID: Tpu received ID
                            //Invoice No: Tpu Invoice No.
                            //Amt :  TOTALVALUE
                            //VoucherType: Payment
                            //BranchID  : HO Leadger


                            Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Account Posting for Purchase Return
        protected int AccountPostingForPurchseReturn(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, 
             string VoucherDate,  string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string StockReceiptLedger = "", PurchaseLeadger = "", POPLedger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", motherdepotid = "", motherdepotname = "", VchDate = "", TPUID = "", transporter = "", transportername = "";
            string TPUname = "", invoice = "", invoicedate = "", Insurance = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", ISFACTORY = "", Isautoposting = "N", kolkataDepotID = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            // DR                           CR                  BOOKS
            //PURCHASE                      TPU                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O'   and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT [TOTALRECEIVEDVALUE],[TAXID],[TAXVALUE],[STOCKRECEIVEDID],[ROUNDOFFVALUE],[TPUID],[MOTHERDEPOTID],[MOTHERDEPOTNAME]," +
                  " [STOCKRECEIVEDDATE],[TPUNAME],[INVOICENO],[INVOICEDATE],[TRANSPORTERID],[TRANSPORTERNAME],[ISTRANSFERTOHO],[POSTING_ACC_LEDGERID]" +
                  " FROM [Vw_PurchaseReturn_AccPosting] WHERE stockreceivedid='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[PURCHASE_ACC_LEADGER],[PURCHASE_EXPORT_ACC_LEADGER],[POP_ACC_LEDGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER],[KOLKATADEPOTID] FROM [P_APPMASTER] ";

                DataTable dtledger = db.GetData(Sql);
                if (dtledger.Rows.Count > 0)
                {
                    //POPLedger = dt.Rows[0]["POP_ACC_LEDGER"].ToString();
                    if (LedgerInfo == "")       /* Based on Single Page Parameter */
                    {
                        
                        PurchaseLeadger = dt.Rows[0]["POSTING_ACC_LEDGERID"].ToString();
                        
                    }
                    else
                    {
                        PurchaseLeadger = LedgerInfo;
                    }
                    RoundOffLeadger = dtledger.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                    Bkg_Dmg_ShrtgLeadger = dtledger.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                    InsClaimLeadger = dtledger.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
                    StockReceiptLedger = dtledger.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
                    kolkataDepotID = dtledger.Rows[0]["KOLKATADEPOTID"].ToString().Trim();
                }


                motherdepotid = dt.Rows[0]["MOTHERDEPOTID"].ToString();
                motherdepotname = dt.Rows[0]["MOTHERDEPOTNAME"].ToString();
                VchDate = dt.Rows[0]["STOCKRECEIVEDDATE"].ToString();
                TPUname = dt.Rows[0]["TPUNAME"].ToString();
                invoice = dt.Rows[0]["INVOICENO"].ToString();
                invoicedate = dt.Rows[0]["INVOICEDATE"].ToString();
                transporter = dt.Rows[0]["TRANSPORTERID"].ToString();
                transportername = dt.Rows[0]["TRANSPORTERNAME"].ToString();
                StockReceivedID = dt.Rows[0]["STOCKRECEIVEDID"].ToString();
                Isautoposting = dt.Rows[0]["ISTRANSFERTOHO"].ToString();

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + motherdepotid + "'";
                REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO,ISNULL(ISFACTORY,'N') AS ISFACTORY FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                    ISFACTORY = Convert.ToString(dtbranch.Rows[0]["ISFACTORY"]);
                }

                DataRow dr = VoucherTable.NewRow();

                decimal TOTALRECEIVEDVALUE = 0;

                TOTALRECEIVEDVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

                //TPU
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["TPUID"]);
                TPUID = Convert.ToString(dt.Rows[0]["TPUID"]);

                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]));

                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                decimal TAXVALUE = 0;
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "" && Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]) > 0)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";

                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                }


                //Purchase 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALRECEIVEDVALUE) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALRECEIVEDVALUE) - Convert.ToDecimal(TAXVALUE) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();
                string Narration = "Being goods purchased retun to " + TPUname + " against bill No." + invoice + " dated " + invoicedate + ".";

                if (kolkataDepotID == motherdepotid)    /* if depot id kolkata then invoice details tag with purchase voucher */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = TPUID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = DepotRecivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = VchDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = invoice;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("20", "Purchase Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO != "Y") && (Isautoposting != "Y"))   /* if depot purchase from vendor not transfer to HO then invoice details tag with purchase voucher */
                {
                    /* FROM VENDOR MASTER not transfer to HO */
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = TPUID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = DepotRecivedID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = VchDate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = invoice;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("20", "Purchase Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails("20", "Purchase Return", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
                }

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else if (Voucher.Contains("|"))
                {
                    String[] vou = Voucher.Split('|');
                    voucherID = vou[0].Trim();

                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (REFERENCELEDGERID == motherdepotid && ISPOSTINGTOHO == "Y")
                    {
                        if (Isautoposting == "Y")           /* FROM VENDOR MASTER */
                        {

                            // DR                           CR                  BOOKS
                            //TPU                           HO                   DEPOT.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = HOLeadger;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "(Auto) Being goods purchased return to " + TPUname + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou1 = Voucher.Split('|');
                                voucherID = vou1[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }


                            // DR                           CR                  BOOKS
                            // DEPOT                        TPU                  HO.

                            VoucherTable.Clear();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = motherdepotid;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();


                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = TOTALVALUE;
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            InvoiceTable.Clear();

                            dr = InvoiceTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = TPUID;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = DepotRecivedID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "10";
                            dr["BranchID"] = HOLeadger;
                            dr["InvoiceDate"] = VchDate;
                            dr["InvoiceBranchID"] = REFERENCELEDGERID;
                            dr["InvoiceOthers"] = invoice;

                            InvoiceTable.Rows.Add(dr);
                            InvoiceTable.AcceptChanges();

                            string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                            //Here invoiceiwse posting need to be passed here 
                            //Leadger ID : TPU Leadger ID
                            //Invoice ID: Tpu received ID
                            //Invoice No: Tpu Invoice No.
                            //Amt :  TOTALVALUE
                            //VoucherType: Payment
                            //BranchID  : HO Leadger


                            Narration = "(Auto) Being goods purchased return to " + TPUname + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }


                    // Damage / Breakage / shortage
                    //dr                                cr                              books
                    //trnsporter/insurance/tpu          damage/shortage/btreakage       depot

                    decimal Amount = 0;
                    string Debitleadger = "";
                    string Debitleadgername = "";
                    string DEBITEDTOID = "";
                    Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT] ,[DEBITEDTOID] FROM [Vw_DamageReceive_AccPosting] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        Amount = 0;
                        for (int counter = 0; counter < dt.Rows.Count; counter++)
                        {
                            DEBITEDTOID = dt.Rows[counter]["DEBITEDTOID"].ToString();

                            switch (dt.Rows[counter]["DEBITEDTOID"].ToString())
                            {
                                case "1":   // Transporter

                                    Debitleadger = transporter;
                                    Debitleadgername = transportername;

                                    break;

                                case "2":   // TPU

                                    Debitleadger = TPUID;
                                    Debitleadgername = TPUname;
                                    break;

                                    

                            }

                            if (DEBITEDTOID != "3")
                            {
                                VoucherTable.Clear();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Debitleadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //Amount = Amount + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());
                                Amount = Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString());

                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;
                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();

                                XML = ConvertDatatableToXML(VoucherTable);

                                Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                {
                                    flag = 2;
                                    return 2;
                                }
                                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                {
                                    flag = 3;
                                    return 3;
                                }
                                else
                                {
                                    String[] vou3 = Voucher.Split('|');
                                    voucherID = vou3[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }
                            }
                            else
                            {
                                // Added By Avishek Ghosh On 31-03-2016
                                string sqlInsurance = string.Empty;
                                sqlInsurance = "EXEC SP_INSURANCE_CLAIM_INSERT 'A','" + DepotRecivedID + "','" + VoucherNo + "'," + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"].ToString()) + ",'" + Uid + "','" + Finyr + "','','','PR','" + VoucherDate + "','','" + DepotID + "','" + DepotName + "'";
                                db.HandleData(sqlInsurance);

                            }

                            if (DEBITEDTOID != "3" && ISPOSTINGTOHO == "Y")
                            {
                                if (REFERENCELEDGERID == motherdepotid)
                                {

                                    //dr                                cr                              books
                                    //HO                               trnsporter/insurance/tpu         depot

                                    VoucherTable.Clear();

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = HOLeadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred to HO.";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                    {
                                        flag = 2;
                                        return 2;
                                    }
                                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                    {
                                        flag = 3;
                                        return 3;
                                    }
                                    else
                                    {

                                        String[] vou4 = Voucher.Split('|');
                                        voucherID = vou4[0].Trim();

                                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                        Voucher = string.Empty;
                                        voucherID = string.Empty;
                                    }

                                    // DR                                               CR                    BOOKS
                                    //  trnsporter/insurance/tpu                        depot                  HO.

                                    VoucherTable.Clear();

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = motherdepotid;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Amount;
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(Amount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    InvoiceTable.Clear();

                                    dr = InvoiceTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["InvoiceID"] = StockReceivedID;
                                    dr["InvoiceNo"] = invoice;
                                    dr["InvoiceAmt"] = -1 * Amount;
                                    dr["AmtPaid"] = 0;
                                    dr["VoucherType"] = "10";
                                    dr["BranchID"] = HOLeadger;
                                    dr["InvoiceDate"] = VchDate;
                                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                                    dr["InvoiceOthers"] = invoice;

                                    InvoiceTable.Rows.Add(dr);
                                    InvoiceTable.AcceptChanges();

                                    string XMLINVOICE1 = ConvertDatatableToXML(InvoiceTable);

                                    //Here invoiceiwse posting need to be passed here 
                                    //Leadger ID : TPU/Transporter Leadger ID (Debitleadger)
                                    //Invoice ID: Tpu received ID
                                    //Invoice No: Tpu Invoice No.
                                    //Amt :  Amount
                                    //VoucherType: Receipt
                                    //BranchID  : HO Leadger
                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE1, "", "Y");

                                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                                    {
                                        flag = 2;
                                        return 2;
                                    }
                                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                                    {
                                        flag = 3;
                                        return 3;
                                    }
                                    else
                                    {
                                        String[] vou5 = Voucher.Split('|');
                                        voucherID = vou5[0].Trim();

                                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                        Voucher = string.Empty;
                                        voucherID = string.Empty;
                                    }
                                }
                            }
                        }
                    }

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Account Posting for Advertisement Bill for VAT
        protected int AccountAdvertisementPostingforVAT(string BillID, string Finyr, string Uid)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string TDSLeadger = "", servicetax = "", HOLeadger = "", VchDate = "", BillNo = "", BillTypeID = "", BillTypeName = "", PartyID = "";
            string invoice = "", invoicedate = "", InvoiceID = "", PostingLedgerID = "";
            decimal NETVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();

            VoucherTable = CreateVoucherTable();

            // DR                                CR                             BOOKS
            //M_ADV TYPE                    PARTY A/C                               HO.
            //TDS

            string Sql = "SELECT [TDS_194C_ACC_LEADGER],[SERVICETAX_ADV_LEDGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                TDSLeadger = dt.Rows[0]["TDS_194C_ACC_LEADGER"].ToString().Trim();
                servicetax = dt.Rows[0]["SERVICETAX_ADV_LEDGER"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TOP 1 ADV_ENTRY_ID,PROCESSNUMBER,ADV_DATE,BILLDATE,PARTYID,TYPEID,TYPENAME,POSTINGLEDGERID,TDSVALUE,SERVICETAXVALUE,BILLNO,BILLAMT,TOTALBILLAMOUNT,NETAMOUNT FROM Vw_Advertisement_AccPosting_VAT WHERE ADV_ENTRY_ID='" + BillID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0]["PROCESSNUMBER"].ToString();
                BillNo = dt.Rows[0]["BILLNO"].ToString();
                BillTypeID = dt.Rows[0]["TYPEID"].ToString();
                BillTypeName = dt.Rows[0]["TYPENAME"].ToString();
                VchDate = dt.Rows[0]["ADV_DATE"].ToString();
                invoicedate = dt.Rows[0]["BILLDATE"].ToString();
                InvoiceID = dt.Rows[0]["ADV_ENTRY_ID"].ToString();
                NETVALUE = Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"].ToString().Trim());
                PostingLedgerID = dt.Rows[0]["POSTINGLEDGERID"].ToString();
                PartyID = dt.Rows[0]["PARTYID"].ToString();

                DataRow dr = VoucherTable.NewRow();

                if (Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) > 0)
                {
                    //TDS
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TDSLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TDSVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDSVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //PARTY A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PartyID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAXVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAXVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                //ADVERTISEMENT TYPE LEDGER
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAXVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["SERVICETAXVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();
                string Narration = "Being " + BillTypeName + " Advertisement Exp for Bill No " + BillNo + " Dated " + VchDate + ".";
                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, BillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;

                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Advertisement Bill for GST
        protected int AccountAdvertisementPosting(string BillID, string Finyr, string Uid)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string TDSLeadger = "", servicetax = "", HOLeadger = "", VchDate = "", BillNo = "", BillTypeID = "", BillTypeName = "", PartyID = "";
            string invoice = "", invoicedate = "", InvoiceID = "", PostingLedgerID = "";
            decimal NETVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();

            VoucherTable = CreateVoucherTable();

            // DR                                CR                             BOOKS
            //M_ADV TYPE                    PARTY A/C                               HO.
            //TDS

            string Sql = "SELECT [TDS_194C_ACC_LEADGER],[SERVICETAX_ADV_LEDGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                TDSLeadger = dt.Rows[0]["TDS_194C_ACC_LEADGER"].ToString().Trim();
                servicetax = dt.Rows[0]["SERVICETAX_ADV_LEDGER"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TOP 1 ADV_ENTRY_ID,PROCESSNUMBER,ADV_DATE,BILLDATE,PARTYID,TYPEID,TYPENAME,POSTINGLEDGERID,TDSVALUE,BILLNO,BILLAMT," +
                  "TOTALBILLAMOUNT,NETAMOUNT,IGSTID AS [ISD-IGSTID] ,CGSTID AS [ISD-CGSTID],SGSTID AS [ISD-SGSTID],UGSTID,IGSTVALUE AS [ISD-IGSTVALUE], CGSTVALUE AS [ISD-CGSTVALUE] ,SGSTVALUE AS [ISD-SGSTVALUE], UGSTVALUE, TOTALGSTTAX FROM Vw_Advertisement_AccPosting WHERE ADV_ENTRY_ID='" + BillID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0]["PROCESSNUMBER"].ToString();
                BillNo = dt.Rows[0]["BILLNO"].ToString();
                BillTypeID = dt.Rows[0]["TYPEID"].ToString();
                BillTypeName = dt.Rows[0]["TYPENAME"].ToString();
                VchDate = dt.Rows[0]["ADV_DATE"].ToString();
                invoicedate = dt.Rows[0]["BILLDATE"].ToString();
                InvoiceID = dt.Rows[0]["ADV_ENTRY_ID"].ToString();
                NETVALUE = Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"].ToString().Trim());
                PostingLedgerID = dt.Rows[0]["POSTINGLEDGERID"].ToString();
                PartyID = dt.Rows[0]["PARTYID"].ToString();

                DataRow dr = VoucherTable.NewRow();

                if (Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) > 0)
                {
                    //TDS
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TDSLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TDSVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TDSVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //PARTY A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PartyID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                //ADVERTISEMENT TYPE LEDGER
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ISD-CGSTVALUE"]) > 0)
                {
                    //CGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["ISD-CGSTID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ISD-CGSTVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ISD-CGSTVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["ISD-SGSTVALUE"]) > 0)
                {
                    //CGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["ISD-SGSTID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ISD-SGSTVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ISD-SGSTVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["ISD-IGSTVALUE"]) > 0)
                {
                    //CGST
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["ISD-IGSTID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ISD-IGSTVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ISD-IGSTVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();
                string Narration = "Being " + BillTypeName + " Advertisement Exp for Bill No " + BillNo + " Dated " + VchDate + ".";
                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, BillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;
                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Voucher
        protected int AccountVoucherPosting(string AccEntryID, string Finyr, string Uid)
        {
            ClsVoucherEntry VchEntry = new ClsVoucherEntry();
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "";
            string invoice = "", PostingLedgerID = "", PostingLedgerName = "", ISPOSTINGTOHO = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            string Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT AccEntryID,VoucherNo,VOUCHERDATE,LEDGERID,LEDGERNAME,AMOUNT,BranchID,BRANCHNAME FROM Vw_VoucherDetails WHERE AccEntryID='" + AccEntryID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PostingLedgerID = Convert.ToString(dt.Rows[0]["BranchID"]);
                PostingLedgerName = Convert.ToString(dt.Rows[0]["BRANCHNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["VOUCHERDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["VoucherNo"]);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + PostingLedgerID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                }

                if (ISPOSTINGTOHO == "Y")
                {
                    // DR                           CR                              BOOKS
                    // DEPOT                       TAX A/C                          HO.

                    DataRow dr = VoucherTable.NewRow();

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        if (Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]) > 0)
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                            TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                    }


                    //TO DEPOT  A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(TOTALVALUE);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    string Narration = "(Auto) Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, AccEntryID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                        flag = 1;
                    }
                }
            }

            Sql = " SELECT TOP 1 AccEntryID,VoucherNo,LEDGERID,LEDGERNAME,AMOUNT,BranchID,VOUCHERDATE FROM Vw_Voucher_JounalDetails WHERE AccEntryID='" + AccEntryID + "'";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(Sql);
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = InvoiceTable.NewRow();

                dr = InvoiceTable.NewRow();
                dr["GUID"] = Convert.ToString(dt1.Rows[0]["AccEntryID"]);
                dr["LedgerId"] = Convert.ToString(dt1.Rows[0]["LEDGERID"]);
                dr["LedgerName"] = "";
                dr["InvoiceID"] = Convert.ToString(dt1.Rows[0]["AccEntryID"]);
                dr["InvoiceNo"] = Convert.ToString(dt1.Rows[0]["VoucherNo"]);
                dr["InvoiceAmt"] = Convert.ToString(dt1.Rows[0]["AMOUNT"]);
                dr["AmtPaid"] = 0;
                dr["VoucherType"] = "9";
                dr["BranchID"] = Convert.ToString(dt1.Rows[0]["BranchID"]);
                dr["InvoiceDate"] = Convert.ToString(dt1.Rows[0]["VOUCHERDATE"]);
                dr["InvoiceBranchID"] = Convert.ToString(dt1.Rows[0]["BranchID"]);
                dr["InvoiceOthers"] = Convert.ToString(dt1.Rows[0]["VoucherNo"]);

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                Voucher = VchEntry.Invoiceinsert(XMLINVOICE);

                flag = 1;
            }

            return flag;
        }
        #endregion

        #region Account Posting for Voucher Journal
        protected int AccountVoucher_JOURNALPosting(string AccEntryID, string Finyr, string Uid, string IstransfertoHO)
        {
            ClsVoucherEntry VchEntry = new ClsVoucherEntry();
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string Costcenterledgerid = string.Empty;

            string HOLeadger = "", VchDate = "", TxnType = "0";
            string invoice = "", PostingLedgerID = "", PostingLedgerName = "", ISPOSTINGTOHO = "";
            decimal TOTALVALUE = 0;
            decimal NewTOTALVALUE = 0;
            int Count = 0;
            DataTable dt = new DataTable();
            DataTable dtcostcenter = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            string Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            if (IstransfertoHO == "YF" || IstransfertoHO == "YP")   /* Transfer to HO Full or Partial */
            {
                if (IstransfertoHO == "YF")             /* Transfer to HO Full */
                {
                    Sql = " SELECT AccEntryID,VoucherNo,VOUCHERDATE,LEDGERID,LEDGERNAME,TXNTYPE,AMOUNT,BranchID,BRANCHNAME,IsCostCenter FROM Vw_TotalVoucherDetails WHERE AccEntryID='" + AccEntryID + "' ORDER BY AMOUNT DESC";
                }
                else if (IstransfertoHO == "YP")        /* Transfer to HO Partial */
                {
                    Sql = " SELECT AccEntryID,VoucherNo,VOUCHERDATE,LEDGERID,LEDGERNAME,TXNTYPE,AMOUNT,BranchID,BRANCHNAME,IsCostCenter FROM Vw_Partial_Exception_VoucherDetails WHERE AccEntryID='" + AccEntryID + "' ORDER BY AMOUNT DESC";
                }
            }
            else
            {
                Sql = " SELECT Distinct ACCENTRYID,VOUCHERNO,VOUCHERDATE,BRANCHID,BRANCHNAME,DEPOTLEDGERID,HO_REF_LEDGERID AS LEDGERID,LEDGERNAME,AMOUNT,CHEQUENO,CHEQUEDATE,PAYMENTTYPENAME,VOUCHERTYPEID,TXNTYPE,ISCOSTCENTER FROM Vw_AccPosting_JOURNAL WHERE AccEntryID='" + AccEntryID + "' ORDER BY AMOUNT DESC";
            }
            dt = db.GetData(Sql);           /* VoucherTypeID = 2 AND BranchID <> '24E1EF07-0A41-4470-B745-9E4BA164C837' */
            if (dt.Rows.Count > 0)
            {
                PostingLedgerID = Convert.ToString(dt.Rows[0]["BranchID"]);
                PostingLedgerName = Convert.ToString(dt.Rows[0]["BRANCHNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["VOUCHERDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["VoucherNo"]);
                TxnType = Convert.ToString(dt.Rows[0]["TXNTYPE"]);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + PostingLedgerID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                }

                if (ISPOSTINGTOHO == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();

                    // DR                           CR                              BOOKS
                    // TO PARTY A/C                 HO                              DEPOT.


                    VoucherTable.Clear();

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 0)
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);  /* 2018*/

                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString(1);
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                            TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 1)
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);  /* 2018*/

                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString(0);
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                            /*TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);*/   /* 03-11-2018 */
                            if (Count == 0)
                            {
                                NewTOTALVALUE = TOTALVALUE;
                                Count = 1;
                            }

                            if (NewTOTALVALUE > 0)
                            {
                                TOTALVALUE = TOTALVALUE - Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);
                            }
                            else
                            {
                                TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);
                            }

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                    }
                    Count = 0;
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";
                    if (Convert.ToDecimal(TxnType) == 0)
                    {
                        dr["TxnType"] = Convert.ToString(0);
                    }
                    else
                    {
                        dr["TxnType"] = Convert.ToString(1);
                    }
                    dr["Amount"] = Convert.ToString(TOTALVALUE);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    /*2018*/
                    Sql = " SELECT AccEntryID,LedgerId,LedgerName,CostCatagoryID,CostCatagoryName,CostCenterID,CostCenterName,BranchID,BranchName,amount,BrandID,BrandName, " +
                        " ProductID,ProductName,DepartmentID,DepartmentName,FromDate,ToDate,NARRATION,TxnType,SYNCTAG FROM Vw_CostCenterDetails WHERE AccEntryID='" + AccEntryID + "' " +
                        " AND LEDGERID IN(select *  from dbo.fnSplit(('" + Costcenterledgerid + "'),','))";
                    dtcostcenter = db.GetData(Sql);           /* VoucherTypeID = 2 AND BranchID <> '24E1EF07-0A41-4470-B745-9E4BA164C837' */

                    string XMLCOSTCENTER = "";
                    if (dtcostcenter.Rows.Count > 0)
                    {
                        XMLCOSTCENTER = ConvertDatatableToXML(dtcostcenter);
                    }

                    dtcostcenter = null;
                    Costcenterledgerid = null;

                    string XML = ConvertDatatableToXML(VoucherTable);

                    string Narration = "(Auto) Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", PostingLedgerID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, null, XMLCOSTCENTER, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou3 = Voucher.Split('|');
                        voucherID = vou3[0].Trim();

                        VchEntry.VoucherDetails(voucherID, AccEntryID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    // DR                           CR                              BOOKS
                    // DEPOT                           TO PARTY A/C                     HO.

                    VoucherTable.Clear();
                    TOTALVALUE = 0;

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 0)
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString(0);
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                            TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 1)
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString(1);
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                            dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                            /*TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);*/   /* 03-11-2018 */

                            if (Count == 0)
                            {
                                NewTOTALVALUE = TOTALVALUE;
                                Count = 1;
                            }

                            if (NewTOTALVALUE > 0)
                            {
                                TOTALVALUE = TOTALVALUE - Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);
                            }
                            else
                            {
                                TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);
                            }

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                    }
                    Count = 0;
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                    dr["LedgerName"] = "";
                    if (Convert.ToDecimal(TxnType) == 0)
                    {
                        dr["TxnType"] = Convert.ToString(1);
                    }
                    else
                    {
                        dr["TxnType"] = Convert.ToString(0);
                    }
                    dr["Amount"] = Convert.ToString(TOTALVALUE);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);
                    dr["IsCostCenter"] = Convert.ToString("N");

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    Sql = " SELECT AccEntryID,LedgerId,LedgerName,CostCatagoryID,CostCatagoryName,CostCenterID,CostCenterName,BranchID,BranchName,amount,BrandID,BrandName, " +
                          " ProductID,ProductName,DepartmentID,DepartmentName,FromDate,ToDate,NARRATION,TxnType,SYNCTAG FROM Vw_CostCenterDetails WHERE AccEntryID='" + AccEntryID + "' " +
                          " AND LEDGERID IN(select *  from dbo.fnSplit(('" + Costcenterledgerid + "'),','))";
                    dtcostcenter = db.GetData(Sql);           /* VoucherTypeID = 2 AND BranchID <> '24E1EF07-0A41-4470-B745-9E4BA164C837' */

                    XMLCOSTCENTER = "";
                    if (dtcostcenter.Rows.Count > 0)
                    {
                        XMLCOSTCENTER = ConvertDatatableToXML(dtcostcenter);
                    }

                    XML = ConvertDatatableToXML(VoucherTable);

                    Narration = "(Auto) Being Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, null, XMLCOSTCENTER, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, AccEntryID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }
                    flag = 1;
                }
            }
            else if (IstransfertoHO != "N")
            {
                Sql = " SELECT AccEntryID,VoucherNo,VOUCHERDATE,LEDGERID,LEDGERNAME,AMOUNT,BranchID,BRANCHNAME FROM Vw_VoucherDetails WHERE AccEntryID='" + AccEntryID + "'";

                dt = db.GetData(Sql);
                if (dt.Rows.Count > 0)
                {
                    PostingLedgerID = Convert.ToString(dt.Rows[0]["BranchID"]);
                    PostingLedgerName = Convert.ToString(dt.Rows[0]["BRANCHNAME"]);
                    VchDate = Convert.ToString(dt.Rows[0]["VOUCHERDATE"]);
                    invoice = Convert.ToString(dt.Rows[0]["VoucherNo"]);

                    DataTable dtbranch = new DataTable();
                    Sql = "SELECT ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + PostingLedgerID + "'";
                    dtbranch = db.GetData(Sql);

                    if (dtbranch.Rows.Count > 0)
                    {
                        ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                    }

                    if (ISPOSTINGTOHO == "Y")
                    {

                        // DR                           CR                              BOOKS
                        // DEPOT                        TAX A/C                         HO.

                        DataRow dr = VoucherTable.NewRow();

                        for (int counter = 0; counter < dt.Rows.Count; counter++)
                        {
                            if (Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]) > 0)
                            {
                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                                Costcenterledgerid = Costcenterledgerid + "," + Convert.ToString(dt.Rows[counter]["LEDGERID"]);

                                dr["LedgerName"] = "";
                                dr["TxnType"] = Convert.ToString("1");
                                dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                                dr["BankID"] = Convert.ToString("");
                                dr["BankName"] = "";
                                dr["ChequeNo"] = Convert.ToString("");
                                dr["ChequeDate"] = Convert.ToString("");
                                dr["IsChequeRealised"] = Convert.ToString("N");
                                dr["Remarks"] = Convert.ToString("");
                                dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                                dr["IsCostCenter"] = Convert.ToString(dt.Rows[counter]["IsCostCenter"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();
                            }
                        }


                        //TO DEPOT  A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(TOTALVALUE);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);
                        dr["IsCostCenter"] = Convert.ToString("N");

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        string XML = ConvertDatatableToXML(VoucherTable);

                        Sql = " SELECT AccEntryID,LedgerId,LedgerName,CostCatagoryID,CostCatagoryName,CostCenterID,CostCenterName,BranchID,BranchName,amount,BrandID,BrandName, " +
                          " ProductID,ProductName,DepartmentID,DepartmentName,FromDate,ToDate,NARRATION,TxnType,SYNCTAG FROM Vw_CostCenterDetails WHERE AccEntryID='" + AccEntryID + "' " +
                          " AND LEDGERID IN(select *  from dbo.fnSplit(('" + Costcenterledgerid + "'),','))";
                        dtcostcenter = db.GetData(Sql);           /* VoucherTypeID = 2 AND BranchID <> '24E1EF07-0A41-4470-B745-9E4BA164C837' */

                        string XMLCOSTCENTER = "";
                        if (dtcostcenter.Rows.Count > 0)
                        {
                            XMLCOSTCENTER = ConvertDatatableToXML(dtcostcenter);
                        }


                        string Narration = "(Auto) Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, null, XMLCOSTCENTER, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou3 = Voucher.Split('|');
                            voucherID = vou3[0].Trim();

                            VchEntry.VoucherDetails(voucherID, AccEntryID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }

                        flag = 1;
                    }
                }


                Sql = " SELECT TOP 1 AccEntryID,VoucherNo,LEDGERID,LEDGERNAME,AMOUNT,BranchID,VOUCHERDATE FROM Vw_Voucher_JounalDetails WHERE AccEntryID='" + AccEntryID + "'";

                DataTable dt1 = new DataTable();
                dt1 = db.GetData(Sql);
                if (dt1.Rows.Count > 0)
                {
                    DataRow dr = InvoiceTable.NewRow();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Convert.ToString(dt1.Rows[0]["AccEntryID"]);
                    dr["LedgerId"] = Convert.ToString(dt1.Rows[0]["LEDGERID"]);
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = Convert.ToString(dt1.Rows[0]["AccEntryID"]);
                    dr["InvoiceNo"] = Convert.ToString(dt1.Rows[0]["VoucherNo"]);
                    dr["InvoiceAmt"] = Convert.ToString(dt1.Rows[0]["AMOUNT"]);
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = Convert.ToString(dt1.Rows[0]["BranchID"]);
                    dr["InvoiceDate"] = Convert.ToString(dt1.Rows[0]["VOUCHERDATE"]);
                    dr["InvoiceBranchID"] = Convert.ToString(dt1.Rows[0]["BranchID"]);
                    dr["InvoiceOthers"] = Convert.ToString(dt1.Rows[0]["VoucherNo"]);

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.Invoiceinsert(XMLINVOICE);

                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Export Invoice Checker1
        protected int AccountExportInvoice_Checker1_Posting(string SaleInvoiceID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string SaleLeadger = "", HOLeadger = "", RoundOffLeadger = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", VchDate = "", DistributorID = "", DistributorName = "";
            string invoice = "", invoicedate = "", InvoiceID = "", ISPOSTINGTOHO = "N", DISTRIBUTORID_TOHO = "", GETPASSNO = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                          BOOKS
            //PARTY A/C                     EXPORT SALE                 HO.


            string Sql = "SELECT [EXPORT_SALE_ACC_LEDGER],[ROUNDOFF_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                SaleLeadger = dt.Rows[0]["EXPORT_SALE_ACC_LEDGER"].ToString().Trim();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString().Trim();
            }

            Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
            REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

            DataTable dtbranch = new DataTable();
            Sql = "SELECT BRNAME,ISNULL(ISEXPORTTRANSFERTOHO,'N') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
            dtbranch = db.GetData(Sql);

            if (dtbranch.Rows.Count > 0)
            {
                ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = Convert.ToString(dt.Rows[0]["BRID"]);
            }

            Sql = " SELECT [CURRENTDATE],[NETVALUEINR],[REALISATIONAMOUNT],[TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DISTRIBUTORID_TOHO],[DEPOTID]," +
                  " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[GROSSREBATEVALUE],[ISFINANCE_HO],[GETPASSNO] FROM [Vw_SaleInvoice_Export_AccPosting]" +
                  " WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                DistributorID = dt.Rows[0]["DISTRIBUTORID"].ToString();
                DISTRIBUTORID_TOHO = dt.Rows[0]["DISTRIBUTORID_TOHO"].ToString();
                //VchDate = dt.Rows[0]["CURRENTDATE"].ToString();   
                VchDate = Convert.ToString(VoucherDate);            /* Instructed by Mr.Sukhdev(which was passed from approval time) on 20-10-2017 */
                DistributorName = dt.Rows[0]["DISTRIBUTORNAME"].ToString();
                invoice = dt.Rows[0]["SALEINVOICENO"].ToString();
                invoicedate = dt.Rows[0]["SALEINVOICEDATE"].ToString();
                InvoiceID = dt.Rows[0]["SALEINVOICEID"].ToString();
                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["NETVALUEINR"].ToString());
                GETPASSNO = Convert.ToString(dt.Rows[0]["GETPASSNO"]);

                DataRow dr = VoucherTable.NewRow();

                decimal TAXVALUE = 0;
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TAXID"]).Trim() != "")
                    {
                        //TAX
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TAXID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);
                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TAXVALUE"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["TAXVALUE"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                }


                //EXPORT SALE
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = SaleLeadger;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(TOTALVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) != 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFFVALUE"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                //PARTY A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = DistributorID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = TOTALVALUE;
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + ".";

                if (REFERENCELEDGERID == HOLeadger)   /* FROM M_BRANCH TABLE */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = SaleInvoiceID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = HOLeadger;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = REFERENCELEDGERID;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("11", "Sales", DepotID, DepotName, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else if (REFERENCELEDGERID != HOLeadger && ISPOSTINGTOHO != "Y")   /* NOT POSTING TO HO */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = DistributorID;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = SaleInvoiceID;
                    dr["InvoiceNo"] = invoice;
                    dr["InvoiceAmt"] = TOTALVALUE;
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "10";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = invoicedate;
                    dr["InvoiceBranchID"] = DepotID;
                    dr["InvoiceOthers"] = GETPASSNO;

                    InvoiceTable.Rows.Add(dr);
                    InvoiceTable.AcceptChanges();

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("11", "Sales", DepotID, DepotName, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails("11", "Sales", DepotID, DepotName, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
                }

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou3 = Voucher.Split('|');
                    voucherID = vou3[0].Trim();

                    VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (REFERENCELEDGERID != HOLeadger && ISPOSTINGTOHO == "Y")   /* FROM M_BRANCH TABLE */
                    {

                        // DR                           CR                              BOOKS
                        // HO                           TO PARTY A/C                    DEPOT.

                        VoucherTable.Clear();

                        //TO PART A/C 
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = DistributorID;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = TOTALVALUE;
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        //HO
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = HOLeadger;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = TOTALVALUE;
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou1 = Voucher.Split('|');
                            voucherID = vou1[0].Trim();

                            VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }

                        // DR                               CR                      BOOKS
                        // PARTY A/C                        DEPOT                   HO.

                        VoucherTable.Clear();

                        //DEPOT
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = REFERENCELEDGERID;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = TOTALVALUE;
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        //PARTY A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        if (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() == "Y")
                        {
                            dr["LedgerId"] = DISTRIBUTORID_TOHO;
                        }
                        else
                        {
                            dr["LedgerId"] = DistributorID;
                        }
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = TOTALVALUE;
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        InvoiceTable.Clear();

                        dr = InvoiceTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = DistributorID;
                        dr["LedgerName"] = "";
                        dr["InvoiceID"] = SaleInvoiceID;
                        dr["InvoiceNo"] = invoice;
                        dr["InvoiceAmt"] = TOTALVALUE;
                        dr["AmtPaid"] = 0;
                        dr["VoucherType"] = "10";
                        dr["BranchID"] = HOLeadger;
                        dr["InvoiceDate"] = invoicedate;
                        dr["InvoiceBranchID"] = REFERENCELEDGERID;
                        dr["InvoiceOthers"] = GETPASSNO;

                        InvoiceTable.Rows.Add(dr);
                        InvoiceTable.AcceptChanges();

                        string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                        //Here invoiceiwse posting need to be passed here 
                        //Leadger ID : TPU Leadger ID
                        //Invoice ID: Tpu received ID
                        //Invoice No: Tpu Invoice No.
                        //Amt :  TOTALVALUE
                        //VoucherType: Payment
                        //BranchID  : HO Leadger


                        Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                        if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                        {
                            flag = 2;
                            return 2;
                        }
                        else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                        {
                            flag = 3;
                            return 3;
                        }
                        else
                        {
                            String[] vou2 = Voucher.Split('|');
                            voucherID = vou2[0].Trim();

                            VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
                        }
                    }

                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region Account Posting for Export Invoice Checker2
        protected int AccountExportInvoice_Checker2_Posting(string SaleInvoiceID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string SaleLeadger = "", BankLeadger = "", HOLeadger = "", VchDate = "", DistributorID = "", DistributorName = "", invoice = "", invoicedate = "", InvoiceID = "";
            decimal TOTALVALUE = 0, REALISATIONVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();


            // DR                           CR                          BOOKS
            //BANK A/C                     PARTY SALE                    HO.

            string Sql = "SELECT [FEF_ACC_LEDGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                SaleLeadger = dt.Rows[0]["FEF_ACC_LEDGER"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT [CURRENTDATE],[NETVALUEINR],[REALISATIONAMOUNT],[TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                  " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[GROSSREBATEVALUE],[BANKID] FROM [Vw_SaleInvoice_Export_AccPosting]" +
                  " WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                DistributorID = Convert.ToString(dt.Rows[0]["DISTRIBUTORID"]);
                //VchDate = Convert.ToString(dt.Rows[0]["CURRENTDATE"]);
                VchDate = Convert.ToString(VoucherDate);
                DistributorName = Convert.ToString(dt.Rows[0]["DISTRIBUTORNAME"]);
                invoice = Convert.ToString(dt.Rows[0]["SALEINVOICENO"]);
                invoicedate = Convert.ToString(dt.Rows[0]["SALEINVOICEDATE"]);
                InvoiceID = Convert.ToString(dt.Rows[0]["SALEINVOICEID"]);
                BankLeadger = Convert.ToString(dt.Rows[0]["BANKID"]);
                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["NETVALUEINR"].ToString());
                REALISATIONVALUE = Convert.ToDecimal(dt.Rows[0]["REALISATIONAMOUNT"].ToString());

                DataRow dr = VoucherTable.NewRow();

                VoucherTable.Clear();

                //PARTY A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = DistributorID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(TOTALVALUE);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if ((REALISATIONVALUE - TOTALVALUE) != 0)
                {
                    //FEF GAIN A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = SaleLeadger;
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(REALISATIONVALUE - TOTALVALUE);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(REALISATIONVALUE - TOTALVALUE);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //BANK A/C
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = BankLeadger;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = REALISATIONVALUE;
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(REALISATIONVALUE);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                string XML = ConvertDatatableToXML(VoucherTable);

                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                string Narration = "(Auto) Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + ".";
                Voucher = VchEntry.InsertVoucherDetails("10", "Receipt", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                {
                    flag = 2;
                    return 2;
                }
                else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                {
                    flag = 3;
                    return 3;
                }
                else
                {
                    String[] vou3 = Voucher.Split('|');
                    voucherID = vou3[0].Trim();

                    VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;

                    flag = 1;
                }
            }
            return flag;
        }
        #endregion

        #region ApproveSaleReturn
        public int ApproveSaleReturn(string StockReceivedID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingFoeSaleReturn(StockReceivedID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_SALERETURN_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE SALERETURNID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApprovePurchaseReturn
        public int ApprovePurchaseReturn(string StockReceivedID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForPurchseReturn(StockReceivedID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName, FGStatus, LedgerInfo));
            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_PURCHASERETURN_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE PURCHASERETURNID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveStockReceived
        public int ApproveStockReceived(string StockReceivedID, string Finyr, string uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int result = 0;
            string sql = string.Empty;
            string PurchaseTypeTag = "N";
            sql = "SELECT CASE WHEN ISNULL(B.EXPORT,'N')='Y' THEN 'E' ELSE 'N' END AS ISEXPORT FROM T_STOCKRECEIVED_HEADER A INNER JOIN M_BRANCH B ON A.MOTHERDEPOTID=B.BRID  WHERE STOCKRECEIVEDID='" + StockReceivedID + "'";
            PurchaseTypeTag = (string)db.GetSingleValue(sql);
            result = Convert.ToInt32(AccountPosting(StockReceivedID, Finyr, uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName, FGStatus, LedgerInfo, PurchaseTypeTag));

            if (result == 1)
            {
                result = 1;  // update successfull

                sql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveStockDepotReceived
        public int ApproveStockDepotReceived(string DepotReceivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForDepotReceived(DepotReceivedID, Finyr, Uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_DEPORECEIVED_STOCK_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKDEPORECEIVEDID ='" + DepotReceivedID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveFactoryStockDepotReceived
        public int ApproveFactoryStockDepotReceived(string DepotReceivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForDepotReceived_FactoryStockTransfer(DepotReceivedID, Finyr, Uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKRECEIVEDID ='" + DepotReceivedID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveStockTransfer
        public int ApproveStockTransfer(string DepotTransferID, string Finyr, string Uid)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForDepotTransfer(DepotTransferID, Finyr, Uid));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKTRANSFER_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKTRANSFERID ='" + DepotTransferID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveInsuranceClaim
        public int ApproveInsuranceClaim(string ClaimID, string Finyr, string Uid, string ClaimNo, string VoucherID)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForInsurance(VoucherID, Finyr, Uid, ClaimID));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_INSURANCE_CLAIM SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE CLAIMID ='" + ClaimID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveClosingStock
        public int ApproveClosingStock(string StockClosingID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            //int result = 0;
            int result = 1;
            //result = Convert.ToInt32(AccountPosting(StockClosingID, Finyr, uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKADJUSTMENT_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE ADJUSTMENTID ='" + StockClosingID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveSaleInvoice
        public int ApproveSaleInvoice(string SaleInvoiceID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName, string SaleInvoiceFor)
        {
            int result = 0;
            result = Convert.ToInt32(AccountSaleInvoicePosting(SaleInvoiceID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName, SaleInvoiceFor));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                if (SaleInvoiceFor == "1")
                {
                    sql = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE() WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                }
                else if (SaleInvoiceFor == "2")
                {
                    sql = " UPDATE T_MM_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE() WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                }

                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region Approve Bank Receipt
        public int ApproveBankReceipt(string VoucherID, string Finyr, string uid, string VoucherNo, string RealishedDate, string RealishedNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountBankReceiptPosting(VoucherID, Finyr, uid, VoucherNo, RealishedDate, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Posting SET ChequeRealisedNo = '" + RealishedNo + "',ChequeRealisedDate = CONVERT(DATETIME,'" + RealishedDate + "',103)" +
                      " WHERE TxnType='0' AND ChequeNo<>'' AND AccEntryID ='" + VoucherID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region Approve Bank Payment
        public int ApproveBankPayment(string VoucherID, string Finyr, string uid, string VoucherNo, string RealishedDate, string RealishedNo, string DepotID, string DepotName, string IsPostingToHO)
        {
            int result = 0;
            result = Convert.ToInt32(AccountBankPaymentPosting(VoucherID, Finyr, uid, VoucherNo, RealishedDate, DepotID, DepotName, IsPostingToHO));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Posting SET ChequeRealisedNo = '" + RealishedNo + "',ChequeRealisedDate = CONVERT(DATETIME,'" + RealishedDate + "',103)" +
                      " WHERE TxnType='0' AND ChequeNo<>'' AND AccEntryID ='" + VoucherID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveTransporterBill
        public int ApproveTransporterBill(string TransporterBillID, string Finyr, string uid, string VoucherNo, string VoucherDate, string IsautopostingtoHO, string IsREVERSECHARGE)
        {
            int result = 0;

            var parameterDate = DateTime.ParseExact("" + VoucherDate + "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var todaysDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (parameterDate < todaysDate)
            {
                result = Convert.ToInt32(AccountTransporterBillPostingForVAT(TransporterBillID, Finyr, uid, VoucherNo, VoucherDate, IsautopostingtoHO, IsREVERSECHARGE));
            }
            else
            {
                result = Convert.ToInt32(AccountTransporterBillPosting(TransporterBillID, Finyr, uid, VoucherNo, VoucherDate, IsautopostingtoHO, IsREVERSECHARGE));
            }

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_TRANSPORTER_BILL_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()," +
                      " ISTRANSFERTOHO='" + IsautopostingtoHO + "',REVERSECHARGE = '" + IsREVERSECHARGE + "' " +
                      " WHERE TRANSPORTERBILLID ='" + TransporterBillID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApprovePaymentRequset
        public int ApprovePaymentRequset(string RequestID, string Finyr, string uid, string VoucherNo, string VoucherDate, string TransporterID, string TransporterName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPaymentRequestPosting(RequestID, Finyr, uid, VoucherNo, VoucherDate, TransporterID, TransporterName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE M_PAYMENT_REQUEST SET ISAPPROVED = 'Y',APPROVEDDATETIME = GETDATE()" +
                      " WHERE REQUESTID ='" + RequestID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveAdvertisement
        public int ApproveAdvertisement(string BillID, string Finyr, string uid, string VoucherDate)
        {
            int result = 0;

            var parameterDate = DateTime.ParseExact("" + VoucherDate + "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var todaysDate = DateTime.ParseExact("01/07/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (parameterDate < todaysDate)
            {
                result = Convert.ToInt32(AccountAdvertisementPostingforVAT(BillID, Finyr, uid));
            }
            else
            {
                result = Convert.ToInt32(AccountAdvertisementPosting(BillID, Finyr, uid));
            }

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_ADV_BILLENTRY_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE ADV_ENTRY_ID ='" + BillID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveAccountsVoucher
        public int ApproveAccountsVoucher(string AccEntryID, string Finyr, string uid)
        {
            int result = 0;
            result = Convert.ToInt32(AccountVoucherPosting(AccEntryID, Finyr, uid));

            if (result == 1)
            {
                result = 1;  // update successfull
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveVoucherJournal
        public int ApproveVoucherJournal(string AccEntryID, string Finyr, string uid, string IstransferHO)
        {
            int result = 0;
            //if (IstransferHO == "N")
            //{
            //    result = 1;
            //}
            //else
            //{
            //    //result = Convert.ToInt32(AccountVoucher_JOURNALPosting(AccEntryID, Finyr, uid, IstransferHO));
            //    /* As instructed by Sandip Biswas talk with Sukhdev on 10-10-2017 */
            //    /* As instructed by Sandip Biswas to open on 16-10-2017 */

            //    result = 1;  
            //}

            result = Convert.ToInt32(AccountVoucher_JOURNALPosting(AccEntryID, Finyr, uid, IstransferHO));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Entry SET IsVoucherApproved = 'Y',ApprovedDateTime = GETDATE()" +
                      " WHERE AccEntryID ='" + AccEntryID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveExportInvoice_Checker1
        public int ApproveExportInvoice_Checker1(string SaleInvoiceID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName,
                                                    decimal SellingRate, decimal NetValueINR, decimal RealisationAmount, string Checker, decimal ConvertionRate,
                                                    decimal BankCharges, decimal GainLoss, decimal RealisationAmountINR)
        {
            int result = 0;
            int result2 = 0;

            string sql = string.Empty;
            sql = " EXEC USP_NETVALUEINR '" + SaleInvoiceID + "'," + SellingRate + "," + NetValueINR + "," + RealisationAmount + "," +
                  " " + ConvertionRate + "," + BankCharges + "," + GainLoss + "," + RealisationAmountINR + ",'" + Checker + "'";
            result2 = db.HandleData(sql);
            if (result2 > 0)
            {
                result = Convert.ToInt32(AccountExportInvoice_Checker1_Posting(SaleInvoiceID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName));
            }

            if (result == 1)
            {
                result = 1;  // update successfull

                string sqlUpdate = string.Empty;
                sqlUpdate = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = CONVERT(DATETIME,'" + VoucherDate + "',103)" +
                            " WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                result = db.HandleData(sqlUpdate);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region ApproveExportInvoice_Checker2
        public int ApproveExportInvoice_Checker2(string SaleInvoiceID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName,
                                                    decimal SellingRate, decimal NetValueINR, decimal RealisationAmount, string Checker, decimal ConvertionRate,
                                                    decimal BankCharges, decimal GainLoss, decimal RealisationAmountINR)
        {

            int result = 0;

            string sql = string.Empty;
            sql = " EXEC USP_NETVALUEINR '" + SaleInvoiceID + "'," + SellingRate + "," + NetValueINR + "," + RealisationAmount + "," +
                  " " + ConvertionRate + "," + BankCharges + "," + GainLoss + "," + RealisationAmountINR + ",'" + Checker + "'";
            result = db.HandleData(sql);
            result = 1;  // update successfull
            //if (result2 > 0)
            //{
            //    result = Convert.ToInt32(AccountExportInvoice_Checker2_Posting(SaleInvoiceID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName));
            //}

            //if (result == 0)
            //{
            //    result = 0;  // update unsuccessfull
            //}
            //else
            //{
            //    result = 1;  // update successfull

            //    string sqlUpdate = string.Empty;
            //    sqlUpdate = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'F',VERIFIEDDATETIME = CONVERT(DATETIME,'" +  VoucherDate + "',103)" +
            //                " WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
            //    result = db.HandleData(sqlUpdate);

            //}

            return result;
        }
        #endregion

        #region RejectSaleInvoice
        public int RejectSaleInvoice(string StockReceivedID, string Note, string InvoiceFor)
        {
            int result = 0;
            string sql = string.Empty;
            /*if (InvoiceFor == "1")
            {
                sql = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "' WHERE SALEINVOICEID ='" + StockReceivedID + "'";
            }
            else
            {
                sql = " UPDATE T_MM_SALEINVOICE_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "' WHERE SALEINVOICEID ='" + StockReceivedID + "'";
            }*/

            sql = "EXEC USP_TRANSACTION_REJECTION '1','" + StockReceivedID + "','" + Note + "'";  /* MODULE = 1 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectStockReceived
        public int RejectStockReceived(string StockReceivedID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '28','" + StockReceivedID + "','" + Note + "'";  /* MODULE = 28 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectDepotReceived
        public int RejectDepotReceived(string DepotReceivedID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE T_DEPORECEIVED_STOCK_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE STOCKDEPORECEIVEDID ='" + DepotReceivedID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '30','" + DepotReceivedID + "','" + Note + "'";  /* MODULE = 30 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectStockTransfer
        public int RejectStockTransfer(string DepotReceivedID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE T_STOCKTRANSFER_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE STOCKTRANSFERID ='" + DepotReceivedID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '29','" + DepotReceivedID + "','" + Note + "'";  /* MODULE = 29 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectInsuranceClaim
        public int RejectInsuranceClaim(string ClaimID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            sql = " UPDATE T_INSURANCE_CLAIM SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE CLAIMID ='" + ClaimID + "'";
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectSaleReturn
        public int RejectSaleReturn(string StockReceivedID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE T_SALERETURN_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE SALERETURNID ='" + StockReceivedID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '26','" + StockReceivedID + "','" + Note + "'";  /* MODULE = 26 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectClosingStock
        public int RejectClosingStock(string StockClosingID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            sql = " UPDATE T_STOCKADJUSTMENT_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE ADJUSTMENTID ='" + StockClosingID + "'";
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectTransporterBill
        public int RejectTransporterBill(string StockReceivedID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE T_TRANSPORTER_BILL_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE TRANSPORTERBILLID ='" + StockReceivedID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '25','" + StockReceivedID + "','" + Note + "'";  /* MODULE = 25 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region RejectAccounts
        public int RejectAccounts(string AccentryID, string Note, string moduleID)
        {
            int result = 0;
            string sql = string.Empty;
            /*sql = " UPDATE ACC_ENTRY SET IsVoucherApproved = 'R',ApprovedDateTime = GETDATE(),SYNCTAG='N',DAYENDTAG='N',RejectionNote = '" + Note + "' WHERE AccEntryID ='" + AccentryID + "'";*/
            sql = "EXEC USP_TRANSACTION_REJECTION '" + moduleID + "','" + AccentryID + "','" + Note + "'";  /* MODULE = 30 */
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        #region PendingDepotStockReceiptVoucher
        public DataTable PendingDepotStockReceiptVoucher(string userID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT NEXTLEVELID ,COUNT(*) AS PENDINGNUMBER FROM T_DEPORECEIVED_STOCK_HEADER" +
                  " WHERE NEXTLEVELID = '" + userID + "' AND ISVERIFIED <> 'Y'" +
                  " GROUP BY NEXTLEVELID";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region PendingTransferVoucher
        public DataTable PendingTransferVoucher(string userID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT NEXTLEVELID ,COUNT(*) AS PENDINGNUMBER FROM T_STOCKTRANSFER_HEADER" +
                  " WHERE NEXTLEVELID = '" + userID + "' AND ISVERIFIED <> 'Y'" +
                  " GROUP BY NEXTLEVELID";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region PendingInsuranceClaimVoucher
        public DataTable PendingInsuranceClaimVoucher(string userID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT NEXTLEVELID ,COUNT(*) AS PENDINGNUMBER FROM T_INSURANCE_CLAIM" +
                  " WHERE NEXTLEVELID = '" + userID + "' AND ISVERIFIED <> 'Y' AND STATUS = '4'" +
                  " GROUP BY NEXTLEVELID";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region PendingPurchaseStockReceiptVoucher
        public DataTable PendingPurchaseStockReceiptVoucher(string userID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = " SELECT NEXTLEVELID ,COUNT(*) AS PENDINGNUMBER FROM T_STOCKRECEIVED_HEADER" +
                  " WHERE NEXTLEVELID = '" + userID + "' AND ISVERIFIED <> 'Y'" +
                  " GROUP BY NEXTLEVELID";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Approve Bank Receipt Only For Transfer HO
        public int ApproveBankReceipt_OnlyForTransferHO(string VoucherID, string Finyr, string uid, string VoucherNo, string RealishedDate, string RealishedNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountBankReceiptPosting_OnlyForTransferHO(VoucherID, Finyr, uid, VoucherNo, RealishedDate, DepotID, DepotName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Posting SET ChequeRealisedNo = '" + RealishedNo + "',ChequeRealisedDate = CONVERT(DATETIME,'" + RealishedDate + "',103)" +
                      " WHERE TxnType='0' AND ChequeNo<>'' AND AccEntryID ='" + VoucherID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region Account Posting for Bank Receipt Only For Transfer HO
        protected int AccountBankReceiptPosting_OnlyForTransferHO(string VoucherID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", Ledgerid = "", Depotid = "", Depotname = "", invoice = "", InvoiceID = "", chequeno = "", chequedate = "", payementtype = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
            decimal TotalAmount = 0;

            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                  BOOKS
            //HO A/C                   BANK A/C                 DEPOT.

            string Sql = "";

            Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
            REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

            DataTable dtbranch = new DataTable();
            Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
            dtbranch = db.GetData(Sql);

            if (dtbranch.Rows.Count > 0)
            {
                ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";
            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME] FROM [Vw_VoucherReceipt_Partial_AccPosting]" +
                  " WHERE AccEntryID='" + VoucherID + "'";

            dt = db.GetData(Sql);

            if (dt.Rows.Count > 0)
            {
                /*CONTINUE ONLY FOR PARIAL TRANSFER BANK LEDGER AMOUNT*/
            }
            else
            {
                Sql = " SELECT AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME] FROM [Vw_AccPosting]" +
                      " WHERE AccEntryID='" + VoucherID + "'";

                dt = db.GetData(Sql);
            }

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                Ledgerid = dt.Rows[0]["LedgerId"].ToString();
                Depotid = dt.Rows[0]["BranchID"].ToString();
                Depotname = dt.Rows[0]["BranchName"].ToString();
                invoice = dt.Rows[0]["VoucherNo"].ToString();
                VchDate = dt.Rows[0]["VoucherDate"].ToString();
                InvoiceID = dt.Rows[0]["AccEntryID"].ToString();
                chequeno = dt.Rows[0]["ChequeNo"].ToString();
                chequedate = dt.Rows[0]["ChequeDate"].ToString();
                payementtype = dt.Rows[0]["PAYMENTTYPENAME"].ToString();

                if (REFERENCELEDGERID == Depotid && ISPOSTINGTOHO == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();

                    // DR                           CR                              BOOKS
                    // BANK A/C                     DEPOT A/C                       HO.


                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LedgerId"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString(dt.Rows[counter]["ChequeNo"]);
                        dr["ChequeDate"] = Convert.ToString(dt.Rows[counter]["ChequeDate"]);
                        dr["IsChequeRealised"] = Convert.ToString("Y");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }

                    //TO DEPOT A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = REFERENCELEDGERID;
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();


                    string XML = ConvertDatatableToXML(VoucherTable);

                    ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                    string Narration = "(Auto) Bank Receipt No : " + invoice + " Dated : " + VchDate + " Transfered to HO " + payementtype + " No " + chequeno + " Dated " + chequedate + ".";
                    Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", HOLeadger, "", "B", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    flag = 1;
                }
                else
                {
                    flag = 1; // only for kolkata depot & Factory
                }
            }
            return flag;
        }
        #endregion

        public string InsertVoucherDetailsUpload(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string InvoiceDebitInvoice, string BillNo, string BillDate)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string VoucherID = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "','" + InvoiceDebitInvoice + "','" + BillNo + "','" + BillDate + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            //if (Voucherdetails != "")
            //{
            //    String[] vou1 = Voucherdetails.Split('|');
            //    VoucherID = vou1[0].Trim();
            //    VoucherNo = vou1[1].Trim();

            //    if (VoucherID != "")
            //    {
            //        this.ApproveVoucherJournal(VoucherID, FinYear, UserID, "Y");
            //    }
            //}

            return Voucherdetails;
        }
        public string GetLedgerID(string LedgerName)
        {
            string Sql = string.Empty;
            string LedgerID = string.Empty;
            Sql = "SELECT Id FROM Acc_AccountsInfo WHERE name='" + LedgerName + "'";
            LedgerID = Convert.ToString(db.GetSingleValue(Sql));
            return LedgerID;
        }

        public string GetCostCentreID(string CostCentreName)
        {
            string Sql = string.Empty;
            string CostCentreID = string.Empty;
            Sql = "SELECT COSTCENTREID FROM M_COST_CENTRE WHERE COSTCENTRENAME='" + CostCentreName + "'";
            CostCentreID = Convert.ToString(db.GetSingleValue(Sql));
            return CostCentreID;
        }
        public decimal GetTaxPercentage(string LedgerID, string VendorID)
        {
            string Sql = string.Empty;
            decimal TaxPercentage = 0;
            Sql = "SELECT PERCENTAGE FROM M_TAX_EXCEPTION WHERE TAXID='" + LedgerID + "' AND VENDORID='" + VendorID + "'";
            TaxPercentage = Convert.ToDecimal(db.GetSingleValue(Sql));
            if (TaxPercentage == 0)
            {
                Sql = "SELECT PERCENTAGE FROM M_TAX WHERE ID='" + LedgerID + "'";
                TaxPercentage = Convert.ToDecimal(db.GetSingleValue(Sql));
            }
            return TaxPercentage;
        }
        public string GetBSID(string BSNAME)
        {
            string Sql = string.Empty;
            string BSID = string.Empty;
            Sql = "SELECT BSID FROM M_BUSINESSSEGMENT WHERE BSNAME='" + BSNAME + "'";
            BSID = Convert.ToString(db.GetSingleValue(Sql));
            return BSID;
        }
        public string GetLedgerRegionMapping(string LedgerID, string BranchID)
        {
            string Sql = string.Empty;
            string BranchName = string.Empty;
            Sql = "select BranchName from Acc_Branch_Mapping where LedgerID='" + LedgerID + "' and BranchID='" + BranchID + "'";
            BranchName = Convert.ToString(db.GetSingleValue(Sql));
            return BranchName;
        }
        public string GetCostCentreTag(string LedgerID)
        {
            string Sql = string.Empty;
            string Tag = string.Empty;
            Sql = "select isnull(costcenter,'') as costcenter from Acc_AccountsInfo where Id ='" + LedgerID + "'";
            Tag = Convert.ToString(db.GetSingleValue(Sql));
            return Tag;
        }

        #region Approve Bank Payment PaymentAdvisory
        public int ApproveBankPayment_PaymentAdvisory(string VoucherID, string Finyr, string uid, string VoucherNo, string VoucherDate, string FolderPath, string PrefixFileName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountBankPayment_PaymentAdvisoryPosting(VoucherID, Finyr, uid, VoucherNo, VoucherDate, FolderPath, PrefixFileName));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE ACC_PAYMENT_ADVISERY_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME=GETDATE() WHERE PAYMENTADVISERYID='" + VoucherID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region Account Posting for Payment Advisory
        protected int AccountBankPayment_PaymentAdvisoryPosting(string VoucherID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string FolderPath, string PrefixFileName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string BankNotepad = string.Empty;

            string HOLeadgerID = "", HOLeadgerName = "", VchDate = "", VchDate_dd_mm_yyyy = "", Ledgerid = "", LedgerName = "", Depotid = "", Depotname = "", adviseryno = "", InvoiceID = "", chequeno = "", chequedate = "", payementtype = "", BANKNARRATION = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", PostingBankID = "", DDMMYYYY_TXNDATE = "", SERVICE_BANK="", CLIENT_ACCOUNTNO="", CLIENT_CODE ="", CLIENT_NAME="", PAYMENT_TYPE="", PartyLedgerID="";
            string BeneficiaryCode = "", BeneficiaryAccountNumber = "", BeneficiaryIFSCCode = "", BeneBankName = "", BeneBankBranchName = "", TxnType = "N", DDMM_TXNDATE = "", SLTXNNO = "00001", TRANSACTION_REMARKS="";
            decimal Amount = 0;
            string InvoiceDebitInvoicexml = null;
            string BILLNO = "";
            string BILLDATE = "";
            string GRNO = "";
            string GRDATE = "";
            string VECHILENO = "";
            string TRANSPORT = "";
            string WAYBILLNO = "";
            string WAYBILLDATE = "";
            string ISCLOSED = "N";
            string ISFORAUTOBANK = "Y";
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            DataTable VouParticularsTable = new DataTable();

            //DR                           CR                      BOOKS
            //party A/C                    BANK A/C                HO.

            string Sql = "";

            Sql = "SELECT TOP 1 BRID,BRNAME FROM [M_BRANCH] WITH(NOLOCK) WHERE BRANCHTAG ='O' and ISMOTHERDEPOT ='True'";
            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadgerID = Convert.ToString(dt.Rows[0]["BRID"]);
                HOLeadgerName = Convert.ToString(dt.Rows[0]["BRNAME"]);
            }

            Sql = " SELECT TXNTYPE,SLTXNNO,DDMM_TXNDATE,DDMMYYYY_TXNDATE,PAYMENTADVISERYID,PAYMENTADVISERYPREFIX,PAYMENTADVISERYNUM,PAYMENTADVISERYSUFFIX," +
                  " CONVERT(VARCHAR(10),PAYMENTADVISERYDATE,103) AS PAYMENTADVISERYDATE,PAYMENTADVISERYDATE_dd_mm_yyyy,PAYMENTADVISERYNO,VENDORID,VENDORNAME,AMOUNT,TRANSACTION_REMARKS,SERVICE_BANK," +
                  " CLIENT_ACCOUNTNO,CLIENT_CODE,CLIENT_NAME,PAYMENT_TYPE,TRANSACTION_REMARKS,BANKNARRATION,PAYMENTBRANCHID FROM Vw_ACC_PAYMENT_ADVISERY " +
                  " WHERE PAYMENTADVISERYID='" + VoucherID + "'";
            dt = db.GetData(Sql);

            if (dt.Rows.Count > 0)
            {
                string FileNametxt = "";
                InvoiceID = Convert.ToString(dt.Rows[0]["PAYMENTADVISERYID"]);
                TxnType = Convert.ToString(dt.Rows[0]["TXNTYPE"]);
                Ledgerid = Convert.ToString(dt.Rows[0]["VENDORID"]);
                VchDate = Convert.ToString(dt.Rows[0]["PAYMENTADVISERYDATE"]);
                VchDate_dd_mm_yyyy = Convert.ToString(dt.Rows[0]["PAYMENTADVISERYDATE_dd_mm_yyyy"]);
                SLTXNNO = Convert.ToString(dt.Rows[0]["SLTXNNO"]);
                DDMM_TXNDATE = Convert.ToString(dt.Rows[0]["DDMM_TXNDATE"]);
                DDMMYYYY_TXNDATE = Convert.ToString(dt.Rows[0]["DDMMYYYY_TXNDATE"]);
                SERVICE_BANK = Convert.ToString(dt.Rows[0]["SERVICE_BANK"]);
                CLIENT_ACCOUNTNO = Convert.ToString(dt.Rows[0]["CLIENT_ACCOUNTNO"]);
                CLIENT_CODE = Convert.ToString(dt.Rows[0]["CLIENT_CODE"]);
                CLIENT_NAME = Convert.ToString(dt.Rows[0]["CLIENT_NAME"]);
                PAYMENT_TYPE = Convert.ToString(dt.Rows[0]["PAYMENT_TYPE"]);
                TRANSACTION_REMARKS = Convert.ToString(dt.Rows[0]["TRANSACTION_REMARKS"]);
                BANKNARRATION = Convert.ToString(dt.Rows[0]["BANKNARRATION"]);
                REFERENCELEDGERID = Convert.ToString(dt.Rows[0]["PAYMENTBRANCHID"]);

                DataTable dtbranch = new DataTable();
                Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
                dtbranch = db.GetData(Sql);

                if (dtbranch.Rows.Count > 0)
                {
                    REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
                }

                if (SERVICE_BANK == "ICICI")
                {
                    Sql = "SELECT ISNULL(ICICIBANK_CC_ACID,'') AS HDFCBANKID FROM P_APPMASTER";
                    PostingBankID = (string)db.GetSingleValue(Sql);
                     
                    FileNametxt = PrefixFileName + "_" + DDMMYYYY_TXNDATE + "_" + SLTXNNO + ".txt";
                }
                else if(SERVICE_BANK == "HDFC")
                {
                    Sql = "SELECT ISNULL(HDFCBANKID,'') AS HDFCBANKID FROM P_APPMASTER";
                    PostingBankID = (string)db.GetSingleValue(Sql);

                    FileNametxt = PrefixFileName + DDMM_TXNDATE + "." + SLTXNNO;
                }

                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    Sql = "EXEC USP_ACC_LEDGER_INFO '" + Ledgerid + "'";
                    DataTable dtinfo = db.GetData(Sql);

                    if (dtinfo.Rows.Count > 0)
                    {
                        BeneficiaryCode = Convert.ToString(dtinfo.Rows[0]["BeneficiaryCode"]);
                        BeneficiaryAccountNumber = Convert.ToString(dtinfo.Rows[0]["BeneficiaryAccountNumber"]);
                        BeneficiaryIFSCCode = Convert.ToString(dtinfo.Rows[0]["IFSCCode"]);
                        BeneBankName = Convert.ToString(dtinfo.Rows[0]["BeneBankName"]);
                        BeneBankBranchName = Convert.ToString(dtinfo.Rows[0]["BeneBankBranchName"]);
                    }
                    else
                    {
                        BeneficiaryCode = "";
                        BeneficiaryAccountNumber = "";
                        BeneficiaryIFSCCode = "";
                        BeneBankName = "";
                        BeneBankBranchName = "";
                    }

                    VoucherTable.Clear();
                    DataRow dr = VoucherTable.NewRow();

                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    if(HOLeadgerID == REFERENCELEDGERID)
                    {
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["VENDORID"]);
                        PartyLedgerID = Convert.ToString(dt.Rows[counter]["VENDORID"]);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(REFERENCELEDGERID);
                    }
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                    LedgerName = Convert.ToString(dt.Rows[counter]["VENDORNAME"]);
                    Amount = Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);
                    adviseryno = Convert.ToString(dt.Rows[counter]["PAYMENTADVISERYNO"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    //BANK A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(PostingBankID);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                    string Narration = "";
                    /*"" + SERVICE_BANK + " Bank Paid to " + LedgerName + " against reference no " + adviseryno + ".";*/
                    Narration = BANKNARRATION + " against reference no " + adviseryno + ".";

                    Voucher = VchEntry.InsertVoucherDetails("15", "Advance Payment", HOLeadgerID, HOLeadgerName, "B", VchDate, Finyr, Narration, Uid, "A", "", XML, "N", "NULL", "NULL", "N",
                                                        InvoiceDebitInvoicexml, BILLNO, BILLDATE, GRNO, GRDATE, VECHILENO, TRANSPORT, WAYBILLNO, WAYBILLDATE, ISCLOSED, ISFORAUTOBANK, SERVICE_BANK);

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        if (HOLeadgerID != REFERENCELEDGERID)
                        {
                            // CR                           DR                              BOOKS
                            // HO.                          PARTY A/C                       DEPOT.

                            VoucherTable.Clear();

                            //TO PARTY A/C 
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = PartyLedgerID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(Amount);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(Amount);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = HOLeadgerID;
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(Amount);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(Amount);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();

                            XML = ConvertDatatableToXML(VoucherTable);

                            string AutoNarration = "(Auto) " + Narration;
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, AutoNarration, Uid, "A", "", XML, "N");

                            if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                            {
                                flag = 2;
                                return 2;
                            }
                            else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                            {
                                flag = 3;
                                return 3;
                            }
                            else
                            {
                                String[] vou2 = Voucher.Split('|');
                                voucherID = vou2[0].Trim();

                                VchEntry.VoucherDetails(voucherID, InvoiceID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }


                        if (SERVICE_BANK == "ICICI")
                        {
                            if (counter == 0)
                            {
                                BankNotepad = "Posting date|Account No|Client code|Client name|Reference No.|Amount|Remitter Account|Remitter Name|Remitter Bank|Remitter IFSC|Product|Transaction Remarks" + Environment.NewLine;
                            }

                            BankNotepad = BankNotepad + VchDate_dd_mm_yyyy + "|" + CLIENT_ACCOUNTNO + "|" + CLIENT_CODE + "|" + CLIENT_NAME + "|" + adviseryno + "|" + Amount + "|" + BeneficiaryAccountNumber + "|" + LedgerName + "|" + BeneBankName + "|" +
                                    "" + BeneficiaryIFSCCode + "|" + PAYMENT_TYPE + "|" + TRANSACTION_REMARKS + "";
                        }
                        else if (SERVICE_BANK == "HDFC")
                        {
                            BankNotepad = TxnType + "," + BeneficiaryCode + "," + BeneficiaryAccountNumber + "," + Amount + "," + LedgerName + "" +
                                    ",,,,,,,," + adviseryno.Substring(0, 20).Trim() + "," + BeneficiaryCode + "," + adviseryno.Substring(0, 20).Trim() + ",,,,,,,," + VchDate + ",," + BeneficiaryIFSCCode + "," + BeneBankName + "," + BeneBankBranchName + ",,";
                        }

                        ClsPaymentAdvisory clspayadv = new ClsPaymentAdvisory();
                        string _status = clspayadv.InsertPaymentAdvisoryFileContent(InvoiceID, BankNotepad, FileNametxt);
                    }

                    flag = 1;   
                }
            }

            return flag;
        }
        #endregion

        public string VoucherBranchID(string VoucherId)
        {
            string Sql = string.Empty;
            string VoucherBranchID = string.Empty;
            Sql = "SELECT BranchID FROM ACC_ENTRY WITH (NOLOCK) WHERE ACCENTRYID ='" + VoucherId + "'";
            VoucherBranchID = (string)db.GetSingleValue(Sql);
            return VoucherBranchID;
        }

        #region CHEQUEREALISEDDATE
        public string CHEQUEREALISEDDATE(string VoucherId, string VoucherTypeID)
        {
            string Sql = "EXEC USP_ACC_CHEQUEREALISEDDATE '" + VoucherId + "','" + VoucherTypeID + "'";
            string CHEQUEREALISEDDATE = (string)db.GetSingleValue(Sql);
            return CHEQUEREALISEDDATE;
        }
        #endregion

        /* Start Added by D.Mondal 20/09/2018*/
        #region ApproveVoucherDebit_CreditNote

        public int ApproveVoucherDebit_CreditNote(string AccEntryID, string Finyr, string uid, string DepotID, string ChkFlag, string VoucherTypeID)
        {
            int result = 0;
            result = Convert.ToInt32(AccountVoucher_Debit_CreditNotePosting(AccEntryID, Finyr, uid, DepotID, ChkFlag, VoucherTypeID));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Entry SET IsVoucherApproved = 'Y',ApprovedDateTime = GETDATE()" +
                      " WHERE AccEntryID ='" + AccEntryID + "'";
                result = db.HandleData(sql);
            }
            else if (result == 2)
            {
                result = 2;  /* LEDGER NOT FOUND FROM SYSTEM */
            }
            else if (result == 3)
            {
                result = 3;  /* DEBIT OR CREDIT AMOUNT NOT SAME */
            }
            else
            {
                result = 0;  // update unsuccessfull
            }

            return result;
        }
        #endregion

        #region Account Posting for Voucher Debit_Credit Note
        protected int AccountVoucher_Debit_CreditNotePosting(string AccEntryID, string Finyr, string Uid, string DepotID, string ChkFlag, string VoucherTypeID)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", DepotLedgerID = "", HO_REF_LedgerID = "", Depotid = "", Depotname = "", invoice = "", InvoiceID = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
            decimal TotalAmount = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();

            DataTable VouParticularsTable = new DataTable();

            // DR                           CR                  BOOKS
            //PARTY                         HO                  DEPOT.

            string Sql = "";

            Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + DepotID + "'";
            REFERENCELEDGERID = (string)db.GetSingleValue(Sql);

            DataTable dtbranch = new DataTable();
            Sql = "SELECT BRNAME,ISNULL(ISPOSTINGTOHO,'Y') AS ISPOSTINGTOHO FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGERID + "'";
            dtbranch = db.GetData(Sql);

            if (dtbranch.Rows.Count > 0)
            {
                ISPOSTINGTOHO = Convert.ToString(dtbranch.Rows[0]["ISPOSTINGTOHO"]);
                REFERENCELEDGERNAME = Convert.ToString(dtbranch.Rows[0]["BRNAME"]);
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";
            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            if (VoucherTypeID == "13")      /* Credit Note */
            {
                if (ChkFlag == "Y")
                {
                    Sql = " SELECT V.AccEntryID,V.VoucherNo,V.VoucherDate,V.[BranchID],V.[BranchName],V.[DepotLedgerID],V.[HO_REF_LedgerID],V.[LedgerName],V.[Amount],V.[ChequeNo],V.[ChequeDate],V.[PAYMENTTYPENAME],V.[VoucherTypeID],V.[TxnType] FROM [Vw_AccPosting_CreditNote] AS V " +
                          " WHERE V.AccEntryID='" + AccEntryID + "'";
                }
                else
                {
                    Sql = " SELECT V.AccEntryID,V.VoucherNo,V.VoucherDate,V.[BranchID],V.[BranchName],V.[DepotLedgerID],V.[HO_REF_LedgerID],V.[LedgerName],V.[Amount],V.[ChequeNo],V.[ChequeDate],V.[PAYMENTTYPENAME],V.[VoucherTypeID],V.[TxnType] FROM [Vw_AccPosting_CreditNote] AS V " +
                          " INNER JOIN M_TAX T ON V.DepotLedgerID=T.ID WHERE V.AccEntryID='" + AccEntryID + "'";
                }
            }
            else if (VoucherTypeID == "14")      /* Debit Note */
            {
                if (ChkFlag == "Y")
                {
                    Sql = " SELECT V.AccEntryID,V.VoucherNo,V.VoucherDate,V.[BranchID],V.[BranchName],V.[DepotLedgerID],V.[HO_REF_LedgerID],V.[LedgerName],V.[Amount],V.[ChequeNo],V.[ChequeDate],V.[PAYMENTTYPENAME],V.[VoucherTypeID],V.[TxnType] FROM [Vw_AccPosting_DebitNote] AS V " +
                          " WHERE V.AccEntryID='" + AccEntryID + "'";
                }
                else
                {
                    Sql = " SELECT V.AccEntryID,V.VoucherNo,V.VoucherDate,V.[BranchID],V.[BranchName],V.[DepotLedgerID],V.[HO_REF_LedgerID],V.[LedgerName],V.[Amount],V.[ChequeNo],V.[ChequeDate],V.[PAYMENTTYPENAME],V.[VoucherTypeID],V.[TxnType] FROM [Vw_AccPosting_DebitNote] AS V " +
                          " INNER JOIN M_TAX T ON V.DepotLedgerID=T.ID WHERE V.AccEntryID='" + AccEntryID + "'";
                }
            }

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                DepotLedgerID = dt.Rows[0]["DepotLedgerID"].ToString();
                HO_REF_LedgerID = dt.Rows[0]["HO_REF_LedgerID"].ToString();
                Depotid = dt.Rows[0]["BranchID"].ToString();
                Depotname = dt.Rows[0]["BranchName"].ToString();
                invoice = dt.Rows[0]["VoucherNo"].ToString();
                VchDate = dt.Rows[0]["VoucherDate"].ToString();
                InvoiceID = dt.Rows[0]["AccEntryID"].ToString();

                if (REFERENCELEDGERID == Depotid && ISPOSTINGTOHO == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();
                    DataRow drPar = VouParticularsTable.NewRow();

                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                    {
                        if (VoucherTypeID == "13" && Convert.ToString(dt.Rows[counter]["TxnType"]) == "1")      /*Credit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["DepotLedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                            TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (VoucherTypeID == "13" && Convert.ToString(dt.Rows[counter]["TxnType"]) == "0")      /*Credit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["DepotLedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                            TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }


                        if (VoucherTypeID == "14" && Convert.ToString(dt.Rows[counter]["TxnType"]) == "0")     /*Debit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["DepotLedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);
                            TotalAmount = TotalAmount + Convert.ToDecimal(dt.Rows[counter]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (VoucherTypeID == "14" && Convert.ToString(dt.Rows[counter]["TxnType"]) == "1")      /*Debit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["DepotLedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                            if (ChkFlag == "Y")             /* Party with TDS */
                            {
                                TotalAmount = TotalAmount - Convert.ToDecimal(dt.Rows[counter]["Amount"]);
                            }
                            else                            /* Only TDS transfer to HO */
                            {
                                TotalAmount = Convert.ToDecimal(dt.Rows[counter]["Amount"]);
                            }

                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                    }

                    //HO A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(HOLeadger);
                    dr["LedgerName"] = "";

                    if (VoucherTypeID == "13") /*Credit Note*/
                    {
                        dr["TxnType"] = Convert.ToString("1");
                    }
                    else if (VoucherTypeID == "14") /*Debit Note*/
                    {
                        if (ChkFlag == "Y")             /* Party with TDS */
                        {
                            dr["TxnType"] = Convert.ToString("0");
                        }
                        else
                        {
                            dr["TxnType"] = Convert.ToString("1");              /* cause TDS ledger on debit side so HOLedger posting to credit side(Exception when TDS) */
                        }
                    }

                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                    string Narration = "(Auto) Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + REFERENCELEDGERNAME + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        VchEntry.ParticulrasInsert(voucherID, InvoiceID, "2");

                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    // DR                               CR                              BOOKS
                    // DEPOT A/C                        PARTY A/C                       HO.

                    VoucherTable.Clear();

                    //TO DEPOT A/C 
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = REFERENCELEDGERID;
                    dr["LedgerName"] = "";

                    if (VoucherTypeID == "13")          /*Credit Note*/
                    {
                        dr["TxnType"] = Convert.ToString("0");
                    }
                    else if (VoucherTypeID == "14")     /*Debit Note*/
                    {
                        if (ChkFlag == "Y")             /* Party with TDS */
                        {
                            dr["TxnType"] = Convert.ToString("1");
                        }
                        else
                        {
                            dr["TxnType"] = Convert.ToString("0");              /* cause TDS ledger on credit side so depot posting to debit side(Exception when only TDS) */
                        }
                    }

                    dr["Amount"] = Convert.ToString(TotalAmount);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(TotalAmount);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    for (int counter1 = 0; counter1 < dt.Rows.Count; counter1++)
                    {
                        if (VoucherTypeID == "13" && Convert.ToString(dt.Rows[counter1]["TxnType"]) == "1")      /*Credit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter1]["HO_REF_LedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = "";
                            dr["ChequeDate"] = "";
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (VoucherTypeID == "13" && Convert.ToString(dt.Rows[counter1]["TxnType"]) == "0")      /*Credit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter1]["HO_REF_LedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = "";
                            dr["ChequeDate"] = "";
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }

                        if (VoucherTypeID == "14" && Convert.ToString(dt.Rows[counter1]["TxnType"]) == "0")      /*Debit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter1]["HO_REF_LedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = "";
                            dr["ChequeDate"] = "";
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                        else if (VoucherTypeID == "14" && Convert.ToString(dt.Rows[counter1]["TxnType"]) == "1")      /*Debit Note*/
                        {
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(dt.Rows[counter1]["HO_REF_LedgerID"]);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";
                            dr["ChequeNo"] = "";
                            dr["ChequeDate"] = "";
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter1]["Amount"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }
                    }

                    XML = ConvertDatatableToXML(VoucherTable);

                    Narration = "(Auto) Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + REFERENCELEDGERNAME + ".";
                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                    if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                    {
                        flag = 2;
                        return 2;
                    }
                    else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                    {
                        flag = 3;
                        return 3;
                    }
                    else
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        VchEntry.ParticulrasInsert(voucherID, InvoiceID, "2");
                    }

                    flag = 1;
                }
                else
                {
                    flag = 1; // only for kolkata depot & Factory
                }
            }
            return flag;
        }
        #endregion
        /* End Added by D.Mondal 20/09/2018*/
        /* Add For update Acc_Entry IsTranferToHO_System and IsTranferToHO_User by D.Mondal on 05/11/2018 */
        #region UpdateAccEntryIsTranferToHO
        public int UpdateAccEntryIsTranferToHO(string AccEntryID, string IsTranferToHO_System, string IsTranferToHO_User)
        {
            int result = 0;

            string sql = string.Empty;
            sql = " UPDATE Acc_Entry SET IsTranferToHO_System = '" + IsTranferToHO_System + "' ,IsTranferToHO_User = '" + IsTranferToHO_User + "' WHERE AccEntryID ='" + AccEntryID + "'";
            result = db.HandleData(sql);

            return result;
        }
        public string GET_ISTRANSFERTOHO(string TRANSPORTERBILLID)
        {
            string Sql = "EXEC USP_GET_ISTRANSFERTOHO '" + TRANSPORTERBILLID + "'";
            string ISTRANSFERTOHO = (string)db.GetSingleValue(Sql);
            return ISTRANSFERTOHO;
        }
        #endregion

        /* ONLY FOR ACCOUNTS AND SALEINVOICE (Sometimes twice approved restriction) */
        public string TRANSACTION_APPROVED_CHECKING(string TransactionID)
        {
            string status = "0";
            string sql = string.Empty;
            sql = "EXEC USP_APPROVED_TRANSACTION_CHECKING '" + TransactionID + "'";
            status = (string)db.GetSingleValue(sql);
            return status;
        }
    }
}

