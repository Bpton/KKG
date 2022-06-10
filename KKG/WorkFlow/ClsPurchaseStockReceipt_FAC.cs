using System;
using System.Data;
using System.Globalization;
using System.IO;
using Account;
using DAL;

namespace WorkFlow
{
    public class ClsPurchaseStockReceipt_FAC
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
            dtvoucher.Columns.Add("DeductablePercentage");  /*Add for TDS*/

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
                else if (Voucher.Contains("|"))
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

        #region Account Posting for Factory Transfer
        public int AccountPostingForFactoryTransfer(string DepotTransferID, string Finyr, string Uid)
        {
            int flag = 0;
            string Voucher = "", voucherID = "";
            string PurchaseLeadger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", Factoryid = "", Factoryname = "", VchDate = "", transporter = "";
            string invoice = "", InsuranceCompanyName = "", InsuranceNo = "", recivedepot = "";
            string REFERENCELEDGER_RECEIVEDDEPOTID = "", REFERENCELEDGER_RECEIVEDDEPONAME = "";
            decimal TAXVALUE = 0;
            string REMARKS = "";
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();

            VoucherTable = CreateVoucherTable();

            // DR                               CR                                  BOOKS
            //RECEIVED DEPOT                    STOCK TRANSFER LEDGER               FROM FACTORY
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

            Sql = " SELECT [STOCKDESPATCHDATE],[MOTHERDEPOTID],[TPUID],[TPUNAME],[INSURANCECOMPID],INSURANCECOMPNAME,INSURANCENO,[TOTALDESPATCHVALUE],[TRANSPORTERID] " +
                   " ,[STOCKDESPATCHID],[INVOICENO],[MOTHERDEPOTNAME],[TAXID],[TAXVALUE],[ROUNDOFFVALUE],[REMARKS] FROM [Vw_FactoryStockTransfer_AccPosting] " +
                   " WHERE [STOCKDESPATCHID]='" + DepotTransferID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                recivedepot = Convert.ToString(dt.Rows[0]["MOTHERDEPOTID"]);
                Factoryid = Convert.ToString(dt.Rows[0]["TPUID"]);
                Factoryname = Convert.ToString(dt.Rows[0]["TPUNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["STOCKDESPATCHDATE"]);
                invoice = Convert.ToString(dt.Rows[0]["INVOICENO"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                InsuranceCompanyName = Convert.ToString(dt.Rows[0]["INSURANCECOMPNAME"]);
                InsuranceNo = Convert.ToString(dt.Rows[0]["INSURANCENO"]);
                REMARKS= Convert.ToString(dt.Rows[0]["REMARKS"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + recivedepot + "'";
                REFERENCELEDGER_RECEIVEDDEPOTID = (string)db.GetSingleValue(Sql);

                Sql = "SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_RECEIVEDDEPOTID + "'";
                REFERENCELEDGER_RECEIVEDDEPONAME = (string)db.GetSingleValue(Sql);

                //Factory stock transfer leadger 
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
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALDESPATCHVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALDESPATCHVALUE"]) - TAXVALUE - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));

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
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALDESPATCHVALUE"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALDESPATCHVALUE"]));

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

                //string Narration = "Being goods transfer from " + Factoryname + " against bill No. " + invoice + " bill date " + VchDate + ".";
                string Narration = REMARKS;
                Voucher = VchEntry.InsertVoucherDetails("4", "Stock Transfer", Factoryid, Factoryname, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

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
            string claimNo = "", PolicyNo = "", ClaimLodgeDate = "", DepotName = "";
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
                    Sql = " SELECT A.CLAIMID,A.CLAIMNO,A.POLICYNO,A.DEPOTNAME,A.CLAIMLODGEDATE,B.TPUNAME,B.INVOICENO,B.STOCKRECEIVEDID" +
                          " FROM VW_INSURANCE_ACCPOSTING AS A INNER JOIN" +
                          "     T_STOCKRECEIVED_HEADER AS B " +
                          "     ON A.VOUCHERID = B.STOCKRECEIVEDID" +
                          " WHERE A.CLAIMID = '" + ClaimID + "'";

                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        ClaimLodgeDate = dt.Rows[0]["CLAIMLODGEDATE"].ToString();
                        DepotName = dt.Rows[0]["DEPOTNAME"].ToString();
                        ClaimID = dt.Rows[0]["CLAIMID"].ToString();
                        claimNo = dt.Rows[0]["CLAIMNO"].ToString();
                        PolicyNo = dt.Rows[0]["POLICYNO"].ToString();
                        stockreceivedid = dt.Rows[0]["STOCKRECEIVEDID"].ToString();
                        invoice = dt.Rows[0]["INVOICENO"].ToString();
                        TransferFrom = dt.Rows[0]["TPUNAME"].ToString();
                    }


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
                    else if (Voucher.Contains("|"))
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, SystemVoucherID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        flag = 1;
                    }
                }
                else
                {
                    Sql = " SELECT A.CLAIMID,A.CLAIMNO,A.POLICYNO,A.DEPOTNAME,A.CLAIMLODGEDATE,B.MOTHERDEPOTNAME,C.STOCKTRANSFERNO" +
                          " FROM VW_INSURANCE_ACCPOSTING AS A INNER JOIN T_DEPORECEIVED_STOCK_HEADER AS B" +
                          "     ON A.VOUCHERID = B.STOCKDEPORECEIVEDID INNER JOIN T_STOCKTRANSFER_HEADER AS C" +
                          "     ON B.STOCKTRANSFERID = C.STOCKTRANSFERID" +
                          " WHERE A.CLAIMID = '" + ClaimID + "'";

                    dt = db.GetData(Sql);
                    if (dt.Rows.Count > 0)
                    {
                        ClaimLodgeDate = dt.Rows[0]["CLAIMLODGEDATE"].ToString();
                        DepotName = dt.Rows[0]["DEPOTNAME"].ToString();
                        ClaimID = dt.Rows[0]["CLAIMID"].ToString();
                        claimNo = dt.Rows[0]["CLAIMNO"].ToString();
                        PolicyNo = dt.Rows[0]["POLICYNO"].ToString();
                        invoice = dt.Rows[0]["STOCKTRANSFERNO"].ToString();
                        TransferFrom = dt.Rows[0]["MOTHERDEPOTNAME"].ToString();
                    }

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
                    else if (Voucher.Contains("|"))
                    {
                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, SystemVoucherID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        flag = 1;
                    }
                }
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

                String[] vou2 = Voucher.Split('|');
                voucherID = vou2[0].Trim();

                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                Voucher = string.Empty;
                voucherID = string.Empty;

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

                            Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            String[] vou3 = Voucher.Split('|');
                            voucherID = vou3[0].Trim();

                            VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;
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

                            //    Narration = "Being goods transfer from " + HOLeadgerName + " against bill No." + invoice + ".";
                            //    VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
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

                                Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGER_MOTHERDEPOTID, REFERENCELEDGER_MOTHERDEPONAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                String[] vou3 = Voucher.Split('|');
                                voucherID = vou3[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;

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

                                Narration = "Being goods transfer from " + REFERENCELEDGER_MOTHERDEPONAME + " against bill No." + invoice + " dated " + VchDate + ".";
                                Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                String[] vou4 = Voucher.Split('|');
                                voucherID = vou4[0].Trim();

                                VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }
                        }
                    }
                }
                flag = 1;
            }
            return flag;
        }
        #endregion

        #region Account Posting for Purchase Stock Received
        protected int AccountPosting(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string Remarks = "";

            string INCOMETAX_Receivable ="", StockReceiptLedger = "", PurchaseLeadger = "", POPLedger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", motherdepotid = "", motherdepotname = "", VchDate = "", TPUID = "", transporter = "", transportername = "";
            string TPUname = "", invoice = "", invoicedate = "", Insurance = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", ISFACTORY = "";
            decimal TOTALVALUE = 0;
            decimal TCSVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            // DR                           CR                  BOOKS
            //PURCHASE                      TPU                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[PURCHASE_ACC_LEADGER],[POP_ACC_LEDGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER],[INCOMETAX_Receivable] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                POPLedger = dt.Rows[0]["POP_ACC_LEDGER"].ToString();
                if (LedgerInfo == "")
                {
                    PurchaseLeadger = dt.Rows[0]["PURCHASE_ACC_LEADGER"].ToString();
                }
                else
                {
                    PurchaseLeadger = LedgerInfo;
                }
                INCOMETAX_Receivable= dt.Rows[0]["INCOMETAX_Receivable"].ToString();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
                StockReceiptLedger = dt.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='D'   and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT [TOTALRECEIVEDVALUE],[TAXID],[TAXVALUE],[STOCKRECEIVEDID],[ROUNDOFFVALUE],[TPUID],[MOTHERDEPOTID],[MOTHERDEPOTNAME],[INSURANCECOMPID]," +
                  " [STOCKRECEIVEDDATE],[TPUNAME],[INVOICENO],[INVOICEDATE],[TRANSPORTERID],[TRANSPORTERNAME],[REMARKS],[TCSAMOUNT] FROM [Vw_StockReceived_AccPosting]" +
                  " WHERE stockreceivedid='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {

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

                Remarks = Convert.ToString(dt.Rows[0]["REMARKS"]);

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

                if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                {
                    //TCS receivable
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(INCOMETAX_Receivable);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                    TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //TPU
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["TPUID"]);
                TPUID = Convert.ToString(dt.Rows[0]["TPUID"]);

                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                //dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALRECEIVEDVALUE"]); // - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) + Convert.ToDecimal(TCSVALUE));
                //TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);
                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]) + Convert.ToDecimal(TCSVALUE);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

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

                if (FGStatus == "TRUE")
                {
                    if (ISFACTORY == "Y")
                    {
                        //Commernts by Rajeev(12-10-2017)
                        //dr["LedgerId"] = Convert.ToString(StockReceiptLedger);
                        dr["LedgerId"] = Convert.ToString(LedgerInfo);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                    }
                }
                else
                {
                    dr["LedgerId"] = Convert.ToString(POPLedger);
                }
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");

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

                // string Narration = "Being goods purchased from " + TPUname + " against bill No." + invoice + " dated " + invoicedate + ".";
                string Narration = Remarks;
                if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO != "Y"))   /* if depot purchase from vendor not transfer to HO then invoice details tag with purchase voucher */
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

                        if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                        {
                            //TCS Receivable
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(INCOMETAX_Receivable);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                            TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";

                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }


                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = HOLeadger;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        //dr["Amount"] = TOTALVALUE;
                        dr["Amount"] = TOTALVALUE - Convert.ToDecimal(TCSVALUE);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        //dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);
                        dr["DeductableAmount"] = Convert.ToString((TOTALVALUE) - Convert.ToDecimal(TCSVALUE));

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "Being goods purchased from " + TPUname + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;




                        // DR                           CR                  BOOKS
                        // DEPOT                        TPU                  HO.

                        VoucherTable.Clear();

                        if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                        {
                            //TCS Receivable
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(INCOMETAX_Receivable);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                            TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";

                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }

                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = motherdepotid;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                       // dr["Amount"] = TOTALVALUE;
                        dr["Amount"] = TOTALVALUE - Convert.ToDecimal(TCSVALUE);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                       // dr["DeductableAmount"] = Convert.ToString(TOTALVALUE);
                        dr["DeductableAmount"] = Convert.ToString((TOTALVALUE) - Convert.ToDecimal(TCSVALUE));

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
                        dr["InvoiceID"] = StockReceivedID;
                        dr["InvoiceNo"] = invoice;
                        dr["InvoiceAmt"] = TOTALVALUE;
                        dr["AmtPaid"] = 0;
                        dr["VoucherType"] = "9";
                        dr["BranchID"] = HOLeadger;

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


                        Narration = "Being goods purchased from " + TPUname + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

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
                    string Debitleadger = "";
                    string Debitleadgername = "";
                    string DEBITEDTOID = "";
                    Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT],[DEBITEDTOID] FROM [Vw_DamageReceive_AccPosting_DEBITLEDGER] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                    DataTable dtledger = db.GetData(Sql);
                    if (dtledger.Rows.Count > 0)
                    {
                        Amount = 0;

                        for (int counter = 0; counter < dtledger.Rows.Count; counter++)
                        {
                            DEBITEDTOID = dtledger.Rows[counter]["DEBITEDTOID"].ToString();

                            switch (dtledger.Rows[counter]["DEBITEDTOID"].ToString())
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
                                Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT],[DEBITEDTOID],TAXID,TAXVALUE FROM [Vw_DamageReceive_AccPosting] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                                dt = db.GetData(Sql);
                                if (dt.Rows.Count > 0)
                                {
                                    VoucherTable.Clear();

                                    decimal REJECTIONTAXVALUE = 0;
                                    for (int Rejcounter = 0; Rejcounter < dt.Rows.Count; Rejcounter++)
                                    {
                                        if (Convert.ToString(dt.Rows[Rejcounter]["TAXID"]).Trim() != "" && Convert.ToDecimal(dt.Rows[Rejcounter]["TAXVALUE"]) > 0)
                                        {
                                            dr = VoucherTable.NewRow();
                                            dr["GUID"] = Guid.NewGuid();
                                            dr["LedgerId"] = Convert.ToString(dt.Rows[Rejcounter]["TAXID"]);
                                            dr["LedgerName"] = "";
                                            dr["TxnType"] = Convert.ToString("1");
                                            dr["Amount"] = Convert.ToString(dt.Rows[Rejcounter]["TAXVALUE"]);

                                            REJECTIONTAXVALUE = REJECTIONTAXVALUE + Convert.ToDecimal(dt.Rows[Rejcounter]["TAXVALUE"]);
                                            dr["BankID"] = Convert.ToString("");
                                            dr["BankName"] = "";

                                            dr["ChequeNo"] = Convert.ToString("");
                                            dr["ChequeDate"] = Convert.ToString("");
                                            dr["IsChequeRealised"] = Convert.ToString("N");
                                            dr["Remarks"] = Convert.ToString("");
                                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[Rejcounter]["TAXVALUE"]);

                                            VoucherTable.Rows.Add(dr);
                                            VoucherTable.AcceptChanges();
                                        }

                                    }


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    /*Modified By Avishek On 17.11.2017*/
                                    /*dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;*/
                                    dr["LedgerId"] = LedgerInfo;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Convert.ToString(dtledger.Rows[0]["AMOUNT"]);
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(dtledger.Rows[0]["AMOUNT"]);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    Amount = Convert.ToDecimal(dtledger.Rows[0]["AMOUNT"].ToString()) + REJECTIONTAXVALUE;

                                    decimal RoundedAmount = decimal.Round(Amount);
                                    decimal RoundOffAmount = RoundedAmount - Amount;


                                    if (RoundOffAmount > 0)
                                    {
                                        //ROUND-OFF
                                        dr = VoucherTable.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                                        dr["LedgerName"] = "";
                                        dr["TxnType"] = Convert.ToString("1");
                                        dr["Amount"] = Convert.ToString(RoundOffAmount);
                                        dr["BankID"] = Convert.ToString("");
                                        dr["BankName"] = "";
                                        dr["ChequeNo"] = Convert.ToString("");
                                        dr["ChequeDate"] = Convert.ToString("");
                                        dr["IsChequeRealised"] = Convert.ToString("N");
                                        dr["Remarks"] = Convert.ToString("");
                                        dr["DeductableAmount"] = Convert.ToString(RoundOffAmount);

                                        VoucherTable.Rows.Add(dr);
                                        VoucherTable.AcceptChanges();
                                    }


                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Convert.ToString(RoundedAmount);
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(RoundedAmount);


                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();


                                    if (RoundOffAmount < 0)
                                    {
                                        //ROUND-OFF
                                        dr = VoucherTable.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                                        dr["LedgerName"] = "";
                                        dr["TxnType"] = Convert.ToString("0");
                                        dr["Amount"] = Convert.ToString(-1 * RoundOffAmount);
                                        dr["BankID"] = Convert.ToString("");
                                        dr["BankName"] = "";
                                        dr["ChequeNo"] = Convert.ToString("");
                                        dr["ChequeDate"] = Convert.ToString("");
                                        dr["IsChequeRealised"] = Convert.ToString("N");
                                        dr["Remarks"] = Convert.ToString("");
                                        dr["DeductableAmount"] = Convert.ToString(-1 * RoundOffAmount);

                                        VoucherTable.Rows.Add(dr);
                                        VoucherTable.AcceptChanges();
                                    }

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("14", "Debit Note", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                    String[] vou3 = Voucher.Split('|');
                                    voucherID = vou3[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }
                            }
                            else
                            {
                                /*  when opened please rectify this block */

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
                                //dr["Remarks"] = Convert.ToString("");
                                //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //    VoucherTable.Rows.Add(dr);
                                //    VoucherTable.AcceptChanges();


                                //    XML = ConvertDatatableToXML(VoucherTable);

                                //    Narration = "Being goods purchased from " + TPUname + " against bill No." + invoice + ".";
                                //    VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
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

                                    Narration = "Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred to HO.";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");


                                    String[] vou4 = Voucher.Split('|');
                                    voucherID = vou4[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;

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
                                    Narration = "Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE1, "", "Y");

                                    String[] vou5 = Voucher.Split('|');
                                    voucherID = vou5[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
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

            string CarriageInwardsLedgers = "", CarriageOutwardsLedgers = "", CarriageDepotTransferLedgers = "", ServiceTaxLeadger = "", ServiceTaxLeadger_Payable = "", TDSLeadger = "", HOLeadger = "", VchDate = "", TransporterID = "", BillTypeID = "";
            string TransporterName = "", invoice = "", invoicedate = "", InvoiceID = "", ServiceTax_Paid = "", Isautoposting = "", PostingDepotID = "", PostingDepotName = "", LRGRNO = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
            decimal NETVALUE = 0;
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
                  " SERVICETAX_PAID,LAVEL,ISTRANSFERTOHO,LRGRNO FROM Vw_Transporter_AccPosting_VAT WHERE TRANSPORTERBILLID='" + TransporterBillID + "' ORDER BY  LAVEL DESC";

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
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(TransporterID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                #region As per Navin,Sukhdev MOM against 17/11/2016 email
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));
                #endregion
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]));

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
                #region As per Navin,Sukhdev MOM against 17/11/2016 email
                dr["Amount"] = Convert.ToString(dt.Rows[0]["BILLAMOUNT"]);
                #endregion
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
                dr["InvoiceAmt"] = NETVALUE;
                dr["AmtPaid"] = 0;
                dr["VoucherType"] = "9";
                dr["BranchID"] = HOLeadger;

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                string Narration = "Being goods transported by " + TransporterName + " against bill No." + invoice + " dated " + VchDate + " with LRGR No " + LRGRNO + ".";
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

                        Narration = "Being goods transported by " + TransporterName + " against bill No." + invoice + " dated + " + VchDate + " with LRGR No " + LRGRNO + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou3 = Voucher.Split('|');
                        voucherID = vou3[0].Trim();

                        VchEntry.VoucherDetails(voucherID, TransporterBillID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
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

                        Narration = "Being goods transported by " + TransporterName + " against bill No." + invoice + " dated " + VchDate + " with LRGR No " + LRGRNO + " transferred from " + REFERENCELEDGERNAME + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou4 = Voucher.Split('|');
                        voucherID = vou4[0].Trim();

                        VchEntry.VoucherDetails(voucherID, TransporterBillID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
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
                   Input_CGST_RCM_Ledger = "", Input_SGST_RCM_Ledger = "", Input_IGST_RCM_Ledger = "", GNGNo = "";
            string Output_CGST_Ledger = "", Output_SGST_Ledger = "", Output_IGST_Ledger = "", TDSLeadger = "", HOLeadger = "", VchDate = "", TransporterID = "", BillTypeID = "", ISPOSTINGTOHO = "";
            string TransporterName = "", invoice = "", invoicedate = "", InvoiceID = "", PostingDepotID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", LRGRNO = "";
            decimal NETVALUE = 0, TotalTax = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            string Sql = " SELECT [CARRIAGE_INWARDS_ACC_LEADGER],[CARRIAGE_DEPOT_TRANSFER_ACC_LEDGER],[CARRIAGE_OUTWARDS_ACC_LEADGER1],[TDS_194C_ACC_LEADGER],[INPUT_CGST],[INPUT_SGST],[INPUT_IGST]," +
                         " [OUTPUT_CGST_RCM],[OUTPUT_SGST_RCM],[OUTPUT_IGST_RCM],INPUT_SGST_RCM,INPUT_CGST_RCM,INPUT_IGST_RCM FROM [P_APPMASTER] ";

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
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TRANSPORTERBILLID,TRANSPORTERBILLNO,BILLAMOUNT,TDS,TOTALCGST,TOTALSGST,TOTALIGST,NETAMOUNT,BILLNO,TRANSPORTERID,TRANSPORTERNAME,TRANSPORTERBILLDATE,BILLTYPEID,DEPOTID," +
                  " SERVICETAX_PAID,LAVEL,ISTRANSFERTOHO,LRGRNO,REVERSECHARGE,GNGNO FROM Vw_Transporter_AccPosting WHERE TRANSPORTERBILLID='" + TransporterBillID + "' ORDER BY  LAVEL DESC";

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
                    dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax);
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
                    dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDS"]) + TotalTax);
                }

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
                dr["InvoiceAmt"] = NETVALUE;
                dr["AmtPaid"] = 0;
                dr["VoucherType"] = "9";
                dr["BranchID"] = REFERENCELEDGERID;
                dr["InvoiceDate"] = VchDate;
                dr["InvoiceBranchID"] = PostingDepotID;
                dr["InvoiceOthers"] = GNGNo;

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                string Narration = "Being goods transported by " + TransporterName + " against bill No." + invoice + " dated " + VchDate + " with LRGR No " + LRGRNO + ".";
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
                else if (Voucher.Contains("|"))
                {
                    String[] vou2 = Voucher.Split('|');
                    voucherID = vou2[0].Trim();

                    VchEntry.VoucherDetails(voucherID, TransporterBillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;


                    if (PostingDepotID != HOLeadger && ISPOSTINGTOHO == "Y")        // if entry from HO than not posting to HO
                    {
                        VoucherTable.Clear();

                        // DR                                CR                             BOOKS
                        //TRANPORTER A/C                   TO HO                            DEPOT.
                        if (IsautopostingtoHO == "Y")
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

                        if (IsautopostingtoHO == "Y")
                        {
                            //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
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
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
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


                        if (IsautopostingtoHO == "Y")
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

                        if (IsautopostingtoHO == "Y")
                        {
                            //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALCGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALSGST"]) + Convert.ToDecimal(dt.Rows[0]["TOTALIGST"]));
                            dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
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
                            dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["BILLAMOUNT"]));
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
            string Remarks = "";

            string TCSPayable = "", SaleLeadger = "", DiscountLeadger = "", RoundOffLeadger = "", HOLeadger = "", motherdepotid = "", VchDate = "", DistributorID = "", transporter = "";
            string DistributorName = "", invoice = "", invoicedate = "", InvoiceID = "", carriage_outward = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", GETPASSNO = "";
            decimal TOTALVALUE = 0;
            decimal TCSVALUE = 0;
            DataTable dt = new DataTable();
            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                  BOOKS
            //PARTY A/C                     SALE                 DEPOT.
            //DISCOUNT                      ROUND OFF
            //                              VAT TAX/CST

            string Sql = "SELECT [SALE_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER],[DISCOUNT_ACC_LEADGER],[CARRIAGE_OUTWARDS_ACC_LEADGER1],[TCS_Payable] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                TCSPayable = dt.Rows[0]["TCS_Payable"].ToString().Trim();
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString().Trim();
                DiscountLeadger = dt.Rows[0]["DISCOUNT_ACC_LEADGER"].ToString().Trim();
                carriage_outward = dt.Rows[0]["CARRIAGE_OUTWARDS_ACC_LEADGER1"].ToString().Trim();
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
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE],[GETPASSNO],[SALE_ACC_LEADGER],[Remarks] FROM [Vw_SaleInvoice_AccPosting]" +
                       " WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }
            else if (InvoiceFor == "2")  //  ----> 2 - Trading Invoice from Factory or Depot for RM/PM product
            {
                Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE],[GETPASSNO],[SALE_ACC_LEADGER],[Remarks],[TCSAMOUNT] FROM [Vw_TradingSaleInvoice_AccPosting]" +
                       " WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }
            else
            {
                Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALEINVOICEID],[ROUNDOFFVALUE],[ADJUSTMENT],[DISTRIBUTORID],[DEPOTID]," +
                       " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[TRANSPORTERID],[GROUPID],[GROUPNAME],[ISFINANCE_HO],[GROSSREBATEVALUE],[GETPASSNO],[SALE_ACC_LEADGER],[Remarks] FROM [Vw_SaleInvoice_AccPosting]" +
                       " WHERE SALEINVOICEID='" + SaleInvoiceID + "' ORDER BY  LAVEL DESC";
            }

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                SaleLeadger = dt.Rows[0]["SALE_ACC_LEADGER"].ToString().Trim();
                DistributorID = dt.Rows[0]["DISTRIBUTORID"].ToString();
                motherdepotid = dt.Rows[0]["DEPOTID"].ToString();
                VchDate = dt.Rows[0]["SALEINVOICEDATE"].ToString();
                DistributorName = dt.Rows[0]["DISTRIBUTORNAME"].ToString();
                invoice = dt.Rows[0]["SALEINVOICENO"].ToString();
                invoicedate = dt.Rows[0]["SALEINVOICEDATE"].ToString();
                transporter = dt.Rows[0]["TRANSPORTERID"].ToString();
                InvoiceID = dt.Rows[0]["SALEINVOICEID"].ToString();
                GETPASSNO = dt.Rows[0]["GETPASSNO"].ToString();

                Remarks = dt.Rows[0]["REMARKS"].ToString();

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
                if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                {
                    //TCS Payble
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TCSPayable);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                    TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

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
                //dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]));
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]));
                //TOTALVALUE = (Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                TOTALVALUE = (Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"])) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                //dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]) - Convert.ToDecimal(TAXVALUE) + Convert.ToDecimal(dt.Rows[0]["GROSSREBATEVALUE"]));
                dr["DeductableAmount"] = TOTALVALUE;
                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

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


                //PARTY A/C 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(DistributorID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                //dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALSALEINVOICEVALUE) - Convert.ToDecimal(TAXVALUE));
                dr["Amount"] = Convert.ToString(TOTALSALEINVOICEVALUE + TCSVALUE);
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALSALEINVOICEVALUE + TCSVALUE);

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

                //string Narration = "Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                string Narration = Remarks;

                if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO == "N") && (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() != "Y"))
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

                    string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                    Voucher = VchEntry.InsertVoucherDetails("11", "Sales", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");
                }
                else
                {
                    Voucher = VchEntry.InsertVoucherDetails("11", "Sales", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
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

                            Narration = "Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + " transferred to HO.";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                            String[] vou1 = Voucher.Split('|');
                            voucherID = vou1[0].Trim();

                            VchEntry.VoucherDetails(voucherID, SaleInvoiceID);
                            Voucher = string.Empty;
                            voucherID = string.Empty;




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
                            dr["LedgerId"] = DistributorID;
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
                            dr["LedgerId"] = DistributorID;
                            dr["LedgerName"] = "";
                            dr["InvoiceID"] = SaleInvoiceID;
                            dr["InvoiceNo"] = invoice;
                            dr["InvoiceAmt"] = TOTALSALEINVOICEVALUE;
                            dr["AmtPaid"] = 0;
                            dr["VoucherType"] = "10";
                            dr["BranchID"] = HOLeadger;

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


                            Narration = "Being goods sold to " + DistributorName + " against bill No." + invoice + " dated " + VchDate + ".";
                            Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

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

        #region Account Posting for Bank Receipt
        protected int AccountBankReceiptPosting(string VoucherID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;

            string HOLeadger = "", VchDate = "", Ledgerid = "", Depotid = "", Depotname = "", invoice = "", InvoiceID = "", chequeno = "", chequedate = "", payementtype = "";
            string REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
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

            Sql = "SELECT TOP 1 AccEntryID,VoucherNo,VoucherDate,[BranchID],[BranchName],[LedgerId],[LedgerName],[Amount],[ChequeNo],[ChequeDate],[PAYMENTTYPENAME] FROM [Vw_AccPosting]" +
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

                if (REFERENCELEDGERID == Depotid && ISPOSTINGTOHO == "Y")
                {
                    DataRow dr = VoucherTable.NewRow();

                    //BANK A/C
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(Ledgerid);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["Amount"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
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
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["Amount"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["Amount"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();

                    string XML = ConvertDatatableToXML(VoucherTable);

                    ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                    string Narration = "Transfered to HO " + payementtype + " no " + chequeno + " dated " + chequedate + " of Payable at " + REFERENCELEDGERNAME + ".";
                    Voucher = VchEntry.InsertVoucherDetails("15", "Advance Payment", REFERENCELEDGERID, REFERENCELEDGERNAME, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

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
                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        // DR                           CR                              BOOKS
                        // BANK A/C                     DEPOT A/C                       HO.

                        VoucherTable.Clear();

                        //TO DEPOT A/C 
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = REFERENCELEDGERID;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["Amount"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        //BANK A/C
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(Ledgerid);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(dt.Rows[0]["Amount"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString(chequeno);
                        dr["ChequeDate"] = Convert.ToString(chequedate);
                        dr["IsChequeRealised"] = Convert.ToString("Y");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["Amount"]);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "Bank Receipt No : " + invoice + " Dated : " + VchDate + " Transfered to HO " + payementtype + " No " + chequeno + " Dated " + chequedate + ".";
                        Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, InvoiceID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        flag = 1;
                    }
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

                //InvoiceTable.Rows.Add(dr);
                //InvoiceTable.AcceptChanges();

                //string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                string Narration = "Being goods sold to " + DistributorName + " against advance payment.";
                Voucher = VchEntry.InsertVoucherDetails("16", "Advance Receipt", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "N");

                String[] vou2 = Voucher.Split('|');
                voucherID = vou2[0].Trim();

                VchEntry.VoucherDetails(voucherID, requestID);
                Voucher = string.Empty;
                voucherID = string.Empty;

                flag = 1;
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
            string REMARKS = "";

            string TCS_Payable = "", PurchaseLeadger = "", RoundOffLeadger = "", HOLeadger = "", depotid = "", depotname = "", VchDate = "", DistributorID = "", DistributorName = "";
            string transporter = "", invoice = "", invoicedate = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", GETPASSNO = "";
            decimal TOTALVALUE = 0;
            decimal TCSVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();


            // DR                           CR                          BOOKS
            //PURCHASE                      DISTRIBUTOR                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT [SALERETURN_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER],[TCS_Payable] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["SALERETURN_ACC_LEADGER"].ToString()))
                {
                    flag = 0;
                    return 0;
                }

                PurchaseLeadger = Convert.ToString(dt.Rows[0]["SALERETURN_ACC_LEADGER"]);
                RoundOffLeadger = Convert.ToString(dt.Rows[0]["ROUNDOFF_ACC_LEADGER"]);
                TCS_Payable = dt.Rows[0]["TCS_Payable"].ToString().Trim();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = Convert.ToString(dt.Rows[0]["BRID"]);
            }

            Sql = " SELECT [TOTALSALEINVOICEVALUE],[TAXID],[TAXVALUE],[SALERETURNID],[ROUNDOFFVALUE],[DEPOTID],[DEPOTNAME],[DISTRIBUTORID],[DISTRIBUTORNAME],[SALERETURNDATE]," +
                  " SALERETURNNO,[TRANSPORTERID],[ISFINANCE_HO],[DISTRIBUTORID_TOHO],[GETPASSNO],[ISPARENTHOLEDGER],[SALERETURN_ACC_LEADGER],[REMARKS],[TCSAMOUNT] FROM [Vw_TradingSaleReturn_MM_AccPosting] WHERE SALERETURNID='" + DepotRecivedID + "' ORDER BY  LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                depotid = Convert.ToString(dt.Rows[0]["DEPOTID"]);
                depotname = Convert.ToString(dt.Rows[0]["DEPOTNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["SALERETURNDATE"]);
                DistributorName = Convert.ToString(dt.Rows[0]["DISTRIBUTORNAME"]);
                invoice = Convert.ToString(dt.Rows[0]["SALERETURNNO"]);
                invoicedate = Convert.ToString(dt.Rows[0]["SALERETURNDATE"]);
                transporter = Convert.ToString(dt.Rows[0]["TRANSPORTERID"]);
                StockReceivedID = Convert.ToString(dt.Rows[0]["SALERETURNID"]);
                GETPASSNO = Convert.ToString(dt.Rows[0]["GETPASSNO"]);

                REMARKS = Convert.ToString(dt.Rows[0]["REMARKS"]);

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

                if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                {
                    //TCS receivable
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(TCS_Payable);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                    TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";

                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //DISTRIBUTORID
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["DISTRIBUTORID"]);
                DistributorID = Convert.ToString(dt.Rows[0]["DISTRIBUTORID"]);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                
                dr["Amount"] = Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) + (TCSVALUE);



                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALSALEINVOICEVALUE"]) + Convert.ToDecimal(TCSVALUE); 

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
                //dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerId"] = Convert.ToString(dt.Rows[0]["SALERETURN_ACC_LEADGER"]);
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

                //string Narration = "Being goods return from " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + ".";
                string Narration = REMARKS;

                if (REFERENCELEDGERID == depotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO == "N") && (Convert.ToString(dt.Rows[0]["ISFINANCE_HO"]).Trim() != "Y"))
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

                        if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                        {
                            //TCS Receivable
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(TCS_Payable);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("1");
                            dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                            TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";

                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }


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
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALVALUE) - Convert.ToDecimal(TCSVALUE));

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "Being goods return from " + DistributorName + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", depotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        // DR                           CR                          BOOKS
                        // DEPOT                        DISTRIBUTOR                  HO.

                        VoucherTable.Clear();

                        if (Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]) > 0)
                        {
                            //TCS Receivable
                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(TCS_Payable);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString("0");
                            dr["Amount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);
                            TCSVALUE = Convert.ToDecimal(dt.Rows[0]["TCSAMOUNT"]);
                            dr["BankID"] = Convert.ToString("");
                            dr["BankName"] = "";

                            dr["ChequeNo"] = Convert.ToString("");
                            dr["ChequeDate"] = Convert.ToString("");
                            dr["IsChequeRealised"] = Convert.ToString("N");
                            dr["Remarks"] = Convert.ToString("");
                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TCSAMOUNT"]);

                            VoucherTable.Rows.Add(dr);
                            VoucherTable.AcceptChanges();
                        }

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
                        dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALVALUE) - Convert.ToDecimal(TCSVALUE));

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();


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

                        XML = ConvertDatatableToXML(VoucherTable);

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

                        InvoiceTable.Rows.Add(dr);
                        InvoiceTable.AcceptChanges();

                        string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);


                        Narration = "Being goods return from " + DistributorName + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + depotname + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Account Posting for Advertisement Bill
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

            Sql = " SELECT TOP 1 ADV_ENTRY_ID,PROCESSNUMBER,ADV_DATE,PARTYID,TYPEID,TYPENAME,POSTINGLEDGERID,TDSVALUE,BILLNO,BILLAMT," +
                  "TOTALBILLAMOUNT,NETAMOUNT,IGSTID,CGSTID,SGSTID,UGSTID,IGSTVALUE,CGSTVALUE,SGSTVALUE,UGSTVALUE,TOTALGSTTAX FROM Vw_Advertisement_AccPosting WHERE ADV_ENTRY_ID='" + BillID + "'";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0]["PROCESSNUMBER"].ToString();
                BillNo = dt.Rows[0]["BILLNO"].ToString();
                BillTypeID = dt.Rows[0]["TYPEID"].ToString();
                BillTypeName = dt.Rows[0]["TYPENAME"].ToString();
                VchDate = dt.Rows[0]["ADV_DATE"].ToString();
                invoicedate = dt.Rows[0]["ADV_DATE"].ToString();
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
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) + Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["TDSVALUE"]) + Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();


                //ADVERTISEMENT TYPE LEDGER
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["TOTALBILLAMOUNT"]) + Convert.ToDecimal(dt.Rows[0]["TOTALGSTTAX"]));

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

                    string Narration = "Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
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

            Sql = " SELECT TOP 1 AccEntryID,VoucherNo,LEDGERID,LEDGERNAME,AMOUNT,BranchID FROM Vw_Voucher_JounalDetails WHERE AccEntryID='" + AccEntryID + "'";

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

                InvoiceTable.Rows.Add(dr);
                InvoiceTable.AcceptChanges();

                string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                Voucher = VchEntry.Invoiceinsert(XMLINVOICE);

                flag = 1;
            }

            return flag;
        }
        #endregion

        #region Account Posting for Voucher
        protected int AccountVoucher_JOURNALPosting(string AccEntryID, string Finyr, string Uid, string IstransfertoHO)
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

            if (IstransfertoHO == "Y")
            {
                Sql = " SELECT AccEntryID,VoucherNo,VOUCHERDATE,LEDGERID,LEDGERNAME,TXNTYPE,AMOUNT,BranchID,BRANCHNAME FROM Vw_TotalVoucherDetails WHERE AccEntryID='" + AccEntryID + "'";

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
                        DataRow dr = VoucherTable.NewRow();

                        // DR                           CR                              BOOKS
                        // TO PARTY A/C                 HO                              DEPOT.


                        VoucherTable.Clear();

                        for (int counter = 0; counter < dt.Rows.Count; counter++)
                        {
                            if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 1)
                            {
                                dr = VoucherTable.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);
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

                                TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                                VoucherTable.Rows.Add(dr);
                                VoucherTable.AcceptChanges();
                            }
                        }

                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(HOLeadger);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString(1);
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

                        string Narration = "Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", PostingLedgerID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

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



                            // DR                           CR                              BOOKS
                            // DEPOT                           TO PARTY A/C                     HO.

                            VoucherTable.Clear();
                            TOTALVALUE = 0;

                            for (int counter = 0; counter < dt.Rows.Count; counter++)
                            {
                                if (Convert.ToDecimal(dt.Rows[counter]["TXNTYPE"]) == 1)
                                {
                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["LEDGERID"]);
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

                                    TOTALVALUE = TOTALVALUE + Convert.ToDecimal(dt.Rows[counter]["AMOUNT"]);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();
                                }
                            }

                            dr = VoucherTable.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(PostingLedgerID);
                            dr["LedgerName"] = "";
                            dr["TxnType"] = Convert.ToString(0);
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


                            XML = ConvertDatatableToXML(VoucherTable);

                            Narration = "Being Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
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
                            }

                            flag = 1;
                        }
                    }
                }
                else
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

                            string Narration = "Transfered to HO against Voucher No." + invoice + " Dated : " + VchDate + " from " + PostingLedgerName + ".";
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
                                String[] vou3 = Voucher.Split('|');
                                voucherID = vou3[0].Trim();

                                VchEntry.VoucherDetails(voucherID, AccEntryID);
                                Voucher = string.Empty;
                                voucherID = string.Empty;
                            }

                            flag = 1;
                        }
                    }


                    Sql = " SELECT TOP 1 AccEntryID,VoucherNo,LEDGERID,LEDGERNAME,AMOUNT,BranchID FROM Vw_Voucher_JounalDetails WHERE AccEntryID='" + AccEntryID + "'";

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

                        InvoiceTable.Rows.Add(dr);
                        InvoiceTable.AcceptChanges();

                        string XMLINVOICE = ConvertDatatableToXML(InvoiceTable);

                        Voucher = VchEntry.Invoiceinsert(XMLINVOICE);

                        flag = 1;
                    }
                }

            }
            return flag;
        }
        #endregion

        #region Account Posting for Trading Export Invoice Checker1
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
                  " [SALEINVOICEDATE],[DISTRIBUTORNAME],[SALEINVOICENO],[GROSSREBATEVALUE],[ISFINANCE_HO],[GETPASSNO] FROM [Vw_TradingInvoice_Export_AccPosting]" +
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

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_SALERETURN_HEADER_MM SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE SALERETURNID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region ApproveStockReceived
        public int ApproveStockReceived(string StockReceivedID, string Finyr, string uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo, string BeforeExchangeRate, string TotalTax)
        {
            #region Update BeforeExchangeRate Rajeev (04-05-2018)
            int result = 0, result1 = 0, result2 = 0;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = " UPDATE T_STOCKRECEIVED_FOOTER SET TOTALRECEIVEDVALUE = '" + BeforeExchangeRate + "'" +
                   " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";

            /*sql2 = " UPDATE T_STOCKRECEIVED_ITEMWISE_TAX SET TAXVALUE = '" + TotalTax + "'" +
                   " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";*/

            result1 = db.HandleData(sql1);
            //result2 = db.HandleData(sql2);

            #endregion

            result = Convert.ToInt32(AccountPosting(StockReceivedID, Finyr, uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName, FGStatus, LedgerInfo));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion


        #region ApproveStockReceived
        public int ApproveStockReceived(string StockReceivedID, string Finyr, string uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo, string BeforeExchangeRate,
            string TotalTax,string despatchNo,string InvoiceDate,string waybill,string einvoiceNo)
        {
            #region Update BeforeExchangeRate Rajeev (04-05-2018)
            int result = 0, result1 = 0, result2 = 0;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = " UPDATE T_STOCKRECEIVED_FOOTER SET TOTALRECEIVEDVALUE = '" + BeforeExchangeRate + "'" +
                   " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";

            sql2 = " UPDATE T_STOCKRECEIVED_HEADER SET  EINVOICENO='"+ einvoiceNo + "',INVOICEDATE= CONVERT(DATETIME,'" + InvoiceDate + "',103)" +
                   " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";

            result1 = db.HandleData(sql1);
            result2 = db.HandleData(sql2);

            #endregion

            result = Convert.ToInt32(AccountPosting(StockReceivedID, Finyr, uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName, FGStatus, LedgerInfo));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKRECEIVEDID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region ApproveStockDepotReceived
        public int ApproveStockDepotReceived(string DepotReceivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForDepotReceived(DepotReceivedID, Finyr, Uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
                string sql = string.Empty;
                sql = " UPDATE T_DEPORECEIVED_STOCK_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE STOCKDEPORECEIVEDID ='" + DepotReceivedID + "'";
                result = db.HandleData(sql);
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

        #region ApproveFactoryStockTransfer
        public int ApproveFactoryStockTransfer(string DepotTransferID, string Finyr, string Uid,string waybillno,string waybilldate)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForFactoryTransfer(DepotTransferID, Finyr, Uid));

            if (result == 1)
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKDESPATCH_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE(),WAYBILLNO='"+ waybillno + "',WAYBILLDATE= CONVERT(DATETIME,'" + waybilldate + "',103)" +
                      " WHERE STOCKDESPATCHID ='" + DepotTransferID + "'";
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

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_INSURANCE_CLAIM SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE CLAIMID ='" + ClaimID + "'";
                result = db.HandleData(sql);
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

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_STOCKADJUSTMENT_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE ADJUSTMENTID ='" + StockClosingID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region ApproveSaleInvoice
        public int ApproveSaleInvoice(string SaleInvoiceID, string Finyr, string uid, string VoucherNo, string VoucherDate, string DepotID, string DepotName, string SaleInvoiceFor,string waybillno, string waybilldate)
        {
            int result = 0;
            result = Convert.ToInt32(AccountSaleInvoicePosting(SaleInvoiceID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName, SaleInvoiceFor));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
                string sql = string.Empty;
                if (SaleInvoiceFor == "1")
                {
                    sql = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE() WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                }
                else if (SaleInvoiceFor == "2")
                {
                    sql = " UPDATE T_MM_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE(),WAYBILLNO='" + waybillno + "',WAYBILLDATE= CONVERT(DATETIME,'" + waybilldate + "',103) WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                }

                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region Approve Bank Receipt
        public int ApproveBankReceipt(string VoucherID, string Finyr, string uid, string VoucherNo, string RealishedDate, string RealishedNo, string DepotID, string DepotName)
        {
            int result = 0;
            result = Convert.ToInt32(AccountBankReceiptPosting(VoucherID, Finyr, uid, VoucherNo, RealishedDate, DepotID, DepotName));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Posting SET ChequeRealisedNo = '" + RealishedNo + "',ChequeRealisedDate = CONVERT(DATETIME,'" + RealishedDate + "',103)" +
                      " WHERE TxnType='0' AND ChequeNo<>'' AND AccEntryID ='" + VoucherID + "'";
                result = db.HandleData(sql);
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

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE M_PAYMENT_REQUEST SET ISAPPROVED = 'Y',APPROVEDDATETIME = GETDATE()" +
                      " WHERE REQUESTID ='" + RequestID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region ApproveAdvertisement
        public int ApproveAdvertisement(string BillID, string Finyr, string uid)
        {
            int result = 0;
            result = Convert.ToInt32(AccountAdvertisementPosting(BillID, Finyr, uid));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_ADV_BILLENTRY_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE ADV_ENTRY_ID ='" + BillID + "'";
                result = db.HandleData(sql);
            }

            return result;
        }
        #endregion

        #region ApproveAccountsVoucher
        public int ApproveAccountsVoucher(string AccEntryID, string Finyr, string uid)
        {
            int result = 0;
            result = Convert.ToInt32(AccountVoucherPosting(AccEntryID, Finyr, uid));

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

        #region ApproveVoucherJournal
        public int ApproveVoucherJournal(string AccEntryID, string Finyr, string uid, string IstransferHO)
        {
            int result = 0;
            result = Convert.ToInt32(AccountVoucher_JOURNALPosting(AccEntryID, Finyr, uid, IstransferHO));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE Acc_Entry SET IsVoucherApproved = 'Y',ApprovedDateTime = GETDATE()" +
                      " WHERE AccEntryID ='" + AccEntryID + "'";
                result = db.HandleData(sql);
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
            sql = " EXEC USP_NETVALUEINR_FACTORY '" + SaleInvoiceID + "'," + SellingRate + "," + NetValueINR + "," + RealisationAmount + "," +
                  " " + ConvertionRate + "," + BankCharges + "," + GainLoss + "," + RealisationAmountINR + ",'" + Checker + "'";
            result2 = db.HandleData(sql);
            if (result2 > 0)
            {
                result = Convert.ToInt32(AccountExportInvoice_Checker1_Posting(SaleInvoiceID, Finyr, uid, VoucherNo, VoucherDate, DepotID, DepotName));
            }

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sqlUpdate = string.Empty;
                sqlUpdate = " UPDATE T_MM_SALEINVOICE_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                            " WHERE SALEINVOICEID ='" + SaleInvoiceID + "'";
                result = db.HandleData(sqlUpdate);
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
            //    sqlUpdate = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'F',VERIFIEDDATETIME = GETDATE()" +
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
            if (InvoiceFor == "1")
            {
                sql = " UPDATE T_SALEINVOICE_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "' WHERE SALEINVOICEID ='" + StockReceivedID + "'";
            }
            else
            {
                sql = " UPDATE T_MM_SALEINVOICE_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "' WHERE SALEINVOICEID ='" + StockReceivedID + "'";
            }
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
            sql = "EXEC [USP_REJECT_GRN_WITH_AUTO_QC_REMOVE] '"+ StockReceivedID + "','"+ Note + "'";
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
            sql = " UPDATE T_DEPORECEIVED_STOCK_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE STOCKDEPORECEIVEDID ='" + DepotReceivedID + "'";
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
            sql = " UPDATE T_STOCKTRANSFER_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE STOCKTRANSFERID ='" + DepotReceivedID + "'";
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
            sql = " UPDATE T_SALERETURN_HEADER_MM SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE SALERETURNID ='" + StockReceivedID + "'";
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

        #region Approve Purchase Order MM Function
        public int ApprovePurchaseOrderMM(string POID,string userid,string mode,string rejectionnote)  /*inline to sp converted 19092019*/
        {
            int result = 0;
            //if (result == 0)
            //{
            //    result = 0;  // update unsuccessfull
            //}
            //else
            //{
            //    result = 1;  // update successfull

            string sql = string.Empty;
            sql = "USP_APPROVED_REJECT_PO_FAC '"+ POID + "','"+ userid + "','"+mode+"','"+ rejectionnote + "'";
            result = db.HandleData(sql);
            //}

            return result;
        }
        #endregion

        #region ApprovePurchaseBillReceivedMM
        public int ApprovePurchaseBillReceivedMM(string StockReceivedID, string Finyr, string uid, string VoucherNo, string VoucherDate, string LedgerInfo)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingForPurchaseBillingMM(StockReceivedID, Finyr, uid, VoucherNo, VoucherDate, LedgerInfo));

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                string Headersql = string.Empty;
                string Detailsql = string.Empty;
                string Foortersql = string.Empty;

                if (LedgerInfo == "9F1BDA35-8808-4618-95DC-518A71A2ADC9")/*Job Work Expenses Ledger ID*/
                {
                    sql = " UPDATE T_MMPURCHASEBILLMAKER_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                          " WHERE PUBID ='" + StockReceivedID + "'";
                    result = db.HandleData(sql);

                    Headersql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                                " WHERE STOCKRECEIVEDID " +
                                " IN(SELECT GRNID FROM T_MMPURCHASEBILLMAKER_DETAILS AS A" +
                                " INNER JOIN T_MMPURCHASEBILLMAKER_HEADER AS B ON A.PUBID=B.PUBID" +
                                " WHERE B.ISVERIFIED='Y' AND B.PUBID='" + StockReceivedID + "')";
                    result = db.HandleData(Headersql);                    
                }
                else if (LedgerInfo == "7B4AA4CB-21B3-4A80-8EB2-A898566D8AF2")/*Purchase Import Ledger ID*/
                {
                    sql = " UPDATE T_MMPURCHASEBILLMAKER_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                          " WHERE PUBID ='" + StockReceivedID + "'";
                    result = db.HandleData(sql);

                    Headersql = " UPDATE T_STOCKRECEIVED_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                                " WHERE STOCKRECEIVEDID " +
                                " IN(SELECT GRNID FROM T_MMPURCHASEBILLMAKER_DETAILS AS A" +
                                " INNER JOIN T_MMPURCHASEBILLMAKER_HEADER AS B ON A.PUBID=B.PUBID" +
                                " WHERE B.ISVERIFIED='Y' AND B.PUBID='" + StockReceivedID + "')";
                    result = db.HandleData(Headersql);

                    Detailsql = "EXEC SP_UPDATE_EXCHANGERATE '" + StockReceivedID + "'";
                    result = db.HandleData(Detailsql);

                    Foortersql = "EXEC SP_UPDATE_TOTALRECEIVEDVALUE '" + StockReceivedID + "'";
                    result = db.HandleData(Foortersql);
                }
            }
            return result;
        }
        #endregion

        #region Account Posting for Purchase Stock Received For MM
        protected int AccountPostingForPurchaseBillingMM(string BillID, string Finyr, string Uid, string VoucherNo, string VoucherDate, string LedgerAccount)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            decimal TAXVALUE = 0,PurchaseLedgerAmount=0;

            string StockReceiptLedger = "", PurchaseLeadger = "", RoundOffLeadger = "", HOLeadger = "", factoryid = "", factoryname = "", VchDate = "";
            string Vendorid = "", vendorname = "", invoice = "", invoicedate = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "";
            string Remarks = "";
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            // DR                           CR                  BOOKS
            //PURCHASE                      VENDOR              FACTORY.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[ROUNDOFF_ACC_LEADGER] FROM [P_APPMASTER]";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                PurchaseLeadger = Convert.ToString(LedgerAccount);
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                StockReceiptLedger = dt.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT TOP 1 BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            //Sql = " SELECT PURCHASEBILLID,PURCHASEBILLDATE,PURCHASEBILLNO,VENDORID,VENDORNAME,FACTORYID,FACTORYNAME,NETAMOUNT,TAXAMOUNT,VATTOTAL,CSTTOTAL,EXCISETOTAL,ROUNDOFF," +
            //      " INPUTCGST,INPUTSGST,INPUTIGST,VAT_ON_PURCHASEID,CST_ON_PURCHASEID,EXCISE_ON_PURCHASEID,INPUTCGST_ON_PURCHASEID,INPUTSGST_ON_PURCHASEID,INPUTIGST_ON_PURCHASEID" +
            //      " FROM Vw_MM_PurchaseBill_AccPosting WHERE PURCHASEBILLID='" + BillID + "'";

            Sql = " SELECT PURCHASEBILLID,PURCHASEBILLDATE,PURCHASEBILLNO,VENDORID,VENDORNAME,FACTORYID,FACTORYNAME,NETAMOUNT,TAXAMOUNT,VATTOTAL,CSTTOTAL,EXCISETOTAL,ROUNDOFF," +
                 " INPUTCGST,INPUTSGST,INPUTIGST,VAT_ON_PURCHASEID,CST_ON_PURCHASEID,EXCISE_ON_PURCHASEID,INPUTCGST_ON_PURCHASEID,INPUTSGST_ON_PURCHASEID,INPUTIGST_ON_PURCHASEID" +
                 "  ,TDSID,TDS_AMOUNT,DEDUCTABLEAMOUNT,PERCENTAGE,REMARKS FROM Vw_MM_PurchaseBill_AccPosting WHERE PURCHASEBILLID='" + BillID + "'";


            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                factoryid = Convert.ToString(dt.Rows[0]["FACTORYID"]);
                factoryname = Convert.ToString(dt.Rows[0]["FACTORYNAME"]);
                VchDate = Convert.ToString(dt.Rows[0]["PURCHASEBILLDATE"]);
                Vendorid = Convert.ToString(dt.Rows[0]["VENDORID"]);
                vendorname = Convert.ToString(dt.Rows[0]["VENDORNAME"]);
                invoice = Convert.ToString(dt.Rows[0]["PURCHASEBILLNO"]);
                invoicedate = Convert.ToString(dt.Rows[0]["PURCHASEBILLDATE"]);
                StockReceivedID = Convert.ToString(dt.Rows[0]["PURCHASEBILLID"]);

                Remarks = Convert.ToString(dt.Rows[0]["REMARKS"]);

                Sql = "SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + factoryid + "'";
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
                decimal TOTALNETAMOUNT = 0;
                TOTALNETAMOUNT = Convert.ToDecimal(dt.Rows[0]["NETAMOUNT"]);

                /*Add for TDS 31/07/2019*/
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    if (Convert.ToString(dt.Rows[counter]["TDSID"]).Trim() != "")
                    {
                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(dt.Rows[counter]["TDSID"]);
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(dt.Rows[counter]["TDS_AMOUNT"]);
                        TAXVALUE = TAXVALUE + Convert.ToDecimal(dt.Rows[counter]["TDS_AMOUNT"]);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["DEDUCTABLEAMOUNT"]);
                        dr["DeductablePercentage"] = Convert.ToString(dt.Rows[counter]["PERCENTAGE"]);


                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                //VENDOR
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(Vendorid);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");              
                //dr["Amount"] = Convert.ToString(TOTALNETAMOUNT);
                dr["Amount"] = Convert.ToString(TOTALNETAMOUNT - Convert.ToDecimal(TAXVALUE));

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(TOTALNETAMOUNT);

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"]) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"])));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"])));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["VATTOTAL"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["VAT_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["VATTOTAL"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["VATTOTAL"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["CSTTOTAL"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["CST_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["CSTTOTAL"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["CSTTOTAL"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["EXCISETOTAL"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["EXCISE_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["EXCISETOTAL"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["EXCISETOTAL"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }
                #region GST POSTING FOR PURCHASE BILL

                if (Convert.ToDecimal(dt.Rows[0]["INPUTCGST"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["INPUTCGST_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["INPUTCGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["INPUTCGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }


                if (Convert.ToDecimal(dt.Rows[0]["INPUTSGST"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["INPUTSGST_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["INPUTCGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["INPUTSGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                if (Convert.ToDecimal(dt.Rows[0]["INPUTIGST"]) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dt.Rows[0]["INPUTIGST_ON_PURCHASEID"]);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["INPUTIGST"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["INPUTIGST"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                #endregion
                //Purchase 
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTALNETAMOUNT) - Convert.ToDecimal(dt.Rows[0]["TAXAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"]));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");                
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTALNETAMOUNT) - Convert.ToDecimal(dt.Rows[0]["TAXAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"]));
                PurchaseLedgerAmount = (Convert.ToDecimal(TOTALNETAMOUNT) - Convert.ToDecimal(dt.Rows[0]["TAXAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"]));
                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(dt.Rows[0]["ROUNDOFF"]) > 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(dt.Rows[0]["ROUNDOFF"]);
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["ROUNDOFF"]);

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);
                ClsVoucherEntry VchEntry = new ClsVoucherEntry();

                //string Narration = "Being goods purchased from " + vendorname + " against bill No." + invoice + " dated " + invoicedate + ".";
                string Narration = Remarks;
                if (REFERENCELEDGERID == factoryid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO != "Y"))   /* if depot purchase from vendor not transfer to HO then invoice details tag with purchase voucher */
                {
                    InvoiceTable.Clear();

                    dr = InvoiceTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Vendorid;
                    dr["LedgerName"] = "";
                    dr["InvoiceID"] = StockReceivedID;
                    dr["InvoiceNo"] = invoice;
                    //dr["InvoiceAmt"] = Convert.ToString(TOTALNETAMOUNT); /*Instruction by Ajay 13012020*/
                    dr["InvoiceAmt"] = Convert.ToString(PurchaseLedgerAmount);
                    dr["AmtPaid"] = 0;
                    dr["VoucherType"] = "9";
                    dr["BranchID"] = REFERENCELEDGERID;
                    dr["InvoiceDate"] = VchDate;
                    dr["InvoiceBranchID"] = factoryid;
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

                    VchEntry.VoucherDetails(voucherID, BillID);
                    Voucher = string.Empty;
                    voucherID = string.Empty;

                    if (REFERENCELEDGERID == factoryid && ISPOSTINGTOHO == "Y")
                    {

                        // DR                           CR                  BOOKS
                        //VENDOR                        HO                  FACTORY.

                        VoucherTable.Clear();

                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Vendorid;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(TOTALNETAMOUNT);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALNETAMOUNT);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();


                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = HOLeadger;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(TOTALNETAMOUNT);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALNETAMOUNT);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        Narration = "Being goods purchased from " + vendorname + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", factoryid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, BillID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;


                        // DR                           CR                  BOOKS
                        // FACTORY                      VENDOR              HO.

                        VoucherTable.Clear();

                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = factoryid;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("0");
                        dr["Amount"] = Convert.ToString(TOTALNETAMOUNT);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALNETAMOUNT);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();


                        dr = VoucherTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Vendorid;
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(TOTALNETAMOUNT);
                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(TOTALNETAMOUNT);

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();

                        XML = ConvertDatatableToXML(VoucherTable);

                        InvoiceTable.Clear();

                        dr = InvoiceTable.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Vendorid;
                        dr["LedgerName"] = "";
                        dr["InvoiceID"] = StockReceivedID;
                        dr["InvoiceNo"] = invoice;
                        dr["InvoiceAmt"] = Convert.ToString(TOTALNETAMOUNT);
                        dr["AmtPaid"] = 0;
                        dr["VoucherType"] = "9";
                        dr["BranchID"] = HOLeadger;

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


                        Narration = "Being goods purchased from " + vendorname + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + factoryname + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

                        String[] vou2 = Voucher.Split('|');
                        voucherID = vou2[0].Trim();

                        VchEntry.VoucherDetails(voucherID, BillID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;
                    }

                    flag = 1;
                }
            }

            return flag;
        }
        #endregion

        #region Approve Purchase Return (MM)
        public int ApprovePurchaseReturn_MM(string StockReceivedID, string Finyr, string uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int result = 0;
            result = Convert.ToInt32(AccountPostingPurchaseReturn_MM(StockReceivedID, Finyr, uid, VoucherNo, InsuranceCompID, InsuranceCompName, VoucherDate, InsuranceNo, DepotID, DepotName, FGStatus, LedgerInfo));
            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull

                string sql = string.Empty;
                sql = " UPDATE T_PURCHASERETURN_HEADER SET ISVERIFIED = 'Y',VERIFIEDDATETIME = GETDATE()" +
                      " WHERE PURCHASERETURNID ='" + StockReceivedID + "'";
                result = db.HandleData(sql);
            }
            return result;
        }
        #endregion

        #region Account Posting for Purchase Return (MM)
        protected int AccountPostingPurchaseReturn_MM(string DepotRecivedID, string Finyr, string Uid, string VoucherNo, string InsuranceCompID, string InsuranceCompName, string VoucherDate, string InsuranceNo, string DepotID, string DepotName, string FGStatus, string LedgerInfo)
        {
            int flag = 0;
            string Voucher = string.Empty;
            string voucherID = string.Empty;
            string Remarks = ""; // 24012020

            string StockReceiptLedger = "", PurchaseLeadger = "", POPLedger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", motherdepotid = "", motherdepotname = "", VchDate = "", TPUID = "", transporter = "", transportername = "";
            string TPUname = "", invoice = "", invoicedate = "", Insurance = "", StockReceivedID = "", REFERENCELEDGERID = "", REFERENCELEDGERNAME = "", ISPOSTINGTOHO = "", ISFACTORY = "";
            decimal TOTALVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            DataTable InvoiceTable = new DataTable();

            VoucherTable = CreateVoucherTable();
            InvoiceTable = CreateInvoiceTable();

            // CR                           DR                  BOOKS
            //PURCHASE                      TPU                 DEPOT.
            //ALL TAXES                     ROUND OFF

            string Sql = "SELECT [STKRECEIPT_ACC_LEADGER],[PURCHASE_ACC_LEADGER],[POP_ACC_LEDGER],[ROUNDOFF_ACC_LEADGER],[BRKG_DAMG_SHRTG_ACC_LEADGER],[INSURANCECLAIM_ACC_LEADGER] FROM [P_APPMASTER] ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                POPLedger = dt.Rows[0]["POP_ACC_LEDGER"].ToString();
                if (LedgerInfo == "")
                {
                    PurchaseLeadger = dt.Rows[0]["PURCHASE_ACC_LEADGER"].ToString();
                }
                else
                {
                    PurchaseLeadger = LedgerInfo;
                }
                RoundOffLeadger = dt.Rows[0]["ROUNDOFF_ACC_LEADGER"].ToString();
                Bkg_Dmg_ShrtgLeadger = dt.Rows[0]["BRKG_DAMG_SHRTG_ACC_LEADGER"].ToString();
                InsClaimLeadger = dt.Rows[0]["INSURANCECLAIM_ACC_LEADGER"].ToString();
                StockReceiptLedger = dt.Rows[0]["STKRECEIPT_ACC_LEADGER"].ToString();
            }

            Sql = "SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' AND ISMOTHERDEPOT ='True' ";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {
                HOLeadger = dt.Rows[0]["BRID"].ToString();
            }

            Sql = " SELECT TOTALPURCHASERETURNVALUE AS [TOTALRECEIVEDVALUE],[TAXID],[TAXVALUE],[PURCHASERETURNID],[ROUNDOFFVALUE],[TPUID],[DEPOTID],[DEPOTNAME]," +
                  " [INSURANCECOMPID],[PURCHASERETURNDATE],[VENDORNAME],[INVOICENO],[INVOICEDATE],[TRANSPORTERID],[TRANSPORTERNAME],[REMARKS] " +
                  " FROM [VW_PURCHASERETURN_ACCPOSTING_MM]" +
                  " WHERE PURCHASERETURNID='" + DepotRecivedID + "' ORDER BY LAVEL DESC";

            dt = db.GetData(Sql);
            if (dt.Rows.Count > 0)
            {

                motherdepotid = dt.Rows[0]["DEPOTID"].ToString();
                motherdepotname = dt.Rows[0]["DEPOTNAME"].ToString();
                VchDate = dt.Rows[0]["PURCHASERETURNDATE"].ToString();
                TPUname = dt.Rows[0]["VENDORNAME"].ToString();
                invoice = dt.Rows[0]["INVOICENO"].ToString();
                invoicedate = dt.Rows[0]["INVOICEDATE"].ToString();
                transporter = dt.Rows[0]["TRANSPORTERID"].ToString();
                transportername = dt.Rows[0]["TRANSPORTERNAME"].ToString();
                Insurance = dt.Rows[0]["INSURANCECOMPID"].ToString();
                StockReceivedID = dt.Rows[0]["PURCHASERETURNID"].ToString();
                Remarks = Convert.ToString(dt.Rows[0]["REMARKS"]);

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
                dr["Amount"] = Convert.ToString(dt.Rows[0]["TOTALRECEIVEDVALUE"]); // - Convert.ToDecimal(dt.Rows[0]["ROUNDOFFVALUE"]));
                TOTALVALUE = Convert.ToDecimal(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";

                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(dt.Rows[0]["TOTALRECEIVEDVALUE"]);

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

                if (FGStatus == "TRUE")
                {
                    if (ISFACTORY == "Y")
                    {
                        //Commernts by Rajeev(12-10-2017)
                        //dr["LedgerId"] = Convert.ToString(StockReceiptLedger);
                        dr["LedgerId"] = Convert.ToString(LedgerInfo);
                    }
                    else
                    {
                        dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                    }
                }
                else
                {
                    dr["LedgerId"] = Convert.ToString(POPLedger);
                }
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

                //string Narration = "Being goods purchased return to " + TPUname + " against bill No." + invoice + " dated " + invoicedate + ".";
                string Narration = Remarks;

                if (REFERENCELEDGERID == motherdepotid && (ISPOSTINGTOHO == "Y" || ISPOSTINGTOHO != "Y"))   /* if depot purchase from vendor not transfer to HO then invoice details tag with purchase voucher */
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
                    dr["VoucherType"] = "10";
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
                        // CR                           DR                  BOOKS
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

                        Narration = "Being goods purchased return to " + TPUname + " bill No." + invoice + " dated " + invoicedate + " transferred to HO.";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                        String[] vou1 = Voucher.Split('|');
                        voucherID = vou1[0].Trim();

                        VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                        Voucher = string.Empty;
                        voucherID = string.Empty;

                        // CR                           DR                  BOOKS
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
                        dr["InvoiceID"] = StockReceivedID;
                        dr["InvoiceNo"] = invoice;
                        dr["InvoiceAmt"] = TOTALVALUE;
                        dr["AmtPaid"] = 0;
                        dr["VoucherType"] = "10";
                        dr["BranchID"] = HOLeadger;

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


                        Narration = "Being goods purchased return to " + TPUname + " against bill No." + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                        Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE, "", "Y");

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
                    string Debitleadger = "";
                    string Debitleadgername = "";
                    string DEBITEDTOID = "";
                    Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT],[DEBITEDTOID] FROM [Vw_DamageReceive_AccPosting_DEBITLEDGER] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                    DataTable dtledger = db.GetData(Sql);
                    if (dtledger.Rows.Count > 0)
                    {
                        Amount = 0;

                        for (int counter = 0; counter < dtledger.Rows.Count; counter++)
                        {
                            DEBITEDTOID = dtledger.Rows[counter]["DEBITEDTOID"].ToString();

                            switch (dtledger.Rows[counter]["DEBITEDTOID"].ToString())
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
                                Sql = "SELECT [STOCKRECEIVEDID],[AMOUNT],[DEBITEDTOID],TAXID,TAXVALUE FROM [Vw_DamageReceive_AccPosting] where STOCKRECEIVEDID='" + DepotRecivedID + "' AND DEBITEDTOID NOT IN ('0')";
                                dt = db.GetData(Sql);
                                if (dt.Rows.Count > 0)
                                {
                                    VoucherTable.Clear();

                                    decimal REJECTIONTAXVALUE = 0;
                                    for (int Rejcounter = 0; Rejcounter < dt.Rows.Count; Rejcounter++)
                                    {
                                        if (Convert.ToString(dt.Rows[Rejcounter]["TAXID"]).Trim() != "" && Convert.ToDecimal(dt.Rows[Rejcounter]["TAXVALUE"]) > 0)
                                        {
                                            dr = VoucherTable.NewRow();
                                            dr["GUID"] = Guid.NewGuid();
                                            dr["LedgerId"] = Convert.ToString(dt.Rows[Rejcounter]["TAXID"]);
                                            dr["LedgerName"] = "";
                                            dr["TxnType"] = Convert.ToString("1");
                                            dr["Amount"] = Convert.ToString(dt.Rows[Rejcounter]["TAXVALUE"]);

                                            REJECTIONTAXVALUE = REJECTIONTAXVALUE + Convert.ToDecimal(dt.Rows[Rejcounter]["TAXVALUE"]);
                                            dr["BankID"] = Convert.ToString("");
                                            dr["BankName"] = "";

                                            dr["ChequeNo"] = Convert.ToString("");
                                            dr["ChequeDate"] = Convert.ToString("");
                                            dr["IsChequeRealised"] = Convert.ToString("N");
                                            dr["Remarks"] = Convert.ToString("");
                                            dr["DeductableAmount"] = Convert.ToString(dt.Rows[Rejcounter]["TAXVALUE"]);

                                            VoucherTable.Rows.Add(dr);
                                            VoucherTable.AcceptChanges();
                                        }
                                    }

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    /*Modified By Avishek On 17.11.2017*/
                                    /*dr["LedgerId"] = Bkg_Dmg_ShrtgLeadger;*/
                                    dr["LedgerId"] = LedgerInfo;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("1");
                                    dr["Amount"] = Convert.ToString(dtledger.Rows[0]["AMOUNT"]);
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(dtledger.Rows[0]["AMOUNT"]);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    Amount = Convert.ToDecimal(dtledger.Rows[0]["AMOUNT"].ToString()) + REJECTIONTAXVALUE;

                                    decimal RoundedAmount = decimal.Round(Amount);
                                    decimal RoundOffAmount = RoundedAmount - Amount;

                                    if (RoundOffAmount > 0)
                                    {
                                        //ROUND-OFF
                                        dr = VoucherTable.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                                        dr["LedgerName"] = "";
                                        dr["TxnType"] = Convert.ToString("1");
                                        dr["Amount"] = Convert.ToString(RoundOffAmount);
                                        dr["BankID"] = Convert.ToString("");
                                        dr["BankName"] = "";
                                        dr["ChequeNo"] = Convert.ToString("");
                                        dr["ChequeDate"] = Convert.ToString("");
                                        dr["IsChequeRealised"] = Convert.ToString("N");
                                        dr["Remarks"] = Convert.ToString("");
                                        dr["DeductableAmount"] = Convert.ToString(RoundOffAmount);

                                        VoucherTable.Rows.Add(dr);
                                        VoucherTable.AcceptChanges();
                                    }

                                    dr = VoucherTable.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["LedgerId"] = Debitleadger;
                                    dr["LedgerName"] = "";
                                    dr["TxnType"] = Convert.ToString("0");
                                    dr["Amount"] = Convert.ToString(RoundedAmount);
                                    dr["BankID"] = Convert.ToString("");
                                    dr["BankName"] = "";
                                    dr["ChequeNo"] = Convert.ToString("");
                                    dr["ChequeDate"] = Convert.ToString("");
                                    dr["IsChequeRealised"] = Convert.ToString("N");
                                    dr["Remarks"] = Convert.ToString("");
                                    dr["DeductableAmount"] = Convert.ToString(RoundedAmount);

                                    VoucherTable.Rows.Add(dr);
                                    VoucherTable.AcceptChanges();

                                    if (RoundOffAmount < 0)
                                    {
                                        //ROUND-OFF
                                        dr = VoucherTable.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                                        dr["LedgerName"] = "";
                                        dr["TxnType"] = Convert.ToString("0");
                                        dr["Amount"] = Convert.ToString(-1 * RoundOffAmount);
                                        dr["BankID"] = Convert.ToString("");
                                        dr["BankName"] = "";
                                        dr["ChequeNo"] = Convert.ToString("");
                                        dr["ChequeDate"] = Convert.ToString("");
                                        dr["IsChequeRealised"] = Convert.ToString("N");
                                        dr["Remarks"] = Convert.ToString("");
                                        dr["DeductableAmount"] = Convert.ToString(-1 * RoundOffAmount);

                                        VoucherTable.Rows.Add(dr);
                                        VoucherTable.AcceptChanges();
                                    }

                                    XML = ConvertDatatableToXML(VoucherTable);

                                    Narration = "(Auto) Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("14", "Debit Note", REFERENCELEDGERID, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                    String[] vou3 = Voucher.Split('|');
                                    voucherID = vou3[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
                                }
                            }
                            else
                            {
                                /*  when opened please rectify this block */

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
                                //dr["Remarks"] = Convert.ToString("");
                                //dr["DeductableAmount"] = Convert.ToString(dt.Rows[counter]["AMOUNT"]);

                                //    VoucherTable.Rows.Add(dr);
                                //    VoucherTable.AcceptChanges();


                                //    XML = ConvertDatatableToXML(VoucherTable);

                                //    Narration = "Being goods purchased from " + TPUname + " against bill No." + invoice + ".";
                                //    VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
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

                                    Narration = "Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred to HO.";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", motherdepotid, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");

                                    String[] vou4 = Voucher.Split('|');
                                    voucherID = vou4[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;

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
                                    Narration = "Being debit note raised on " + Debitleadgername + " for shortage/damage by them in purchase bill No. " + invoice + " dated " + invoicedate + " transferred from " + motherdepotname + ".";
                                    Voucher = VchEntry.InsertVoucherDetails("2", "Journal", HOLeadger, "", "", VchDate, Finyr, Narration, Uid, "A", "", XML, XMLINVOICE1, "", "Y");

                                    String[] vou5 = Voucher.Split('|');
                                    voucherID = vou5[0].Trim();

                                    VchEntry.VoucherDetails(voucherID, DepotRecivedID);
                                    Voucher = string.Empty;
                                    voucherID = string.Empty;
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

        #region Upload File Delete
        public DataTable FileDelete(string id)
        {

            string sql = "DELETE FROM [T_MM_PURCHASEORDER_UPLOADFILES] WHERE POID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion


        #region GetFile
        public DataTable GetFile(string id)
        {
            string sql = "SELECT FILENAME AS FILENAME FROM [T_MM_PURCHASEORDER_UPLOADFILES] WHERE POID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
    }


}
