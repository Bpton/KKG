using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;


namespace FactoryDatacontext
{
    public class AccountPosting
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

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

        #region Account Posting for Factory Transfer
        public int AccountPostingForFactoryTransfer(string DepotTransferID, string Finyr, string Uid)
        {
            int flag = 0;
            string Voucher = "", voucherID = "";
            string PurchaseLeadger = "", RoundOffLeadger = "", Bkg_Dmg_ShrtgLeadger = "", InsClaimLeadger = "", HOLeadger = "", Factoryid = "", Factoryname = "", VchDate = "", transporter = "";
            string invoice = "", InsuranceCompanyName = "", InsuranceNo = "", recivedepot = "";
            string REFERENCELEDGER_RECEIVEDDEPOTID = "", REFERENCELEDGER_RECEIVEDDEPONAME = "",TOTAL_DISPATCHVALUE="", ROUNDOFFVALUE="";
            decimal TAXVALUE = 0;
            DataTable dt = new DataTable();

            DataTable VoucherTable = new DataTable();
            VoucherTable = CreateVoucherTable();
            

            // DR                               CR                                  BOOKS
            //RECEIVED DEPOT                    STOCK TRANSFER LEDGER               FROM FACTORY
            //                                  OUTPUT TAX

           

            List<ParamLedger> paramLedger = new List<ParamLedger>();
            paramLedger = _db.Query<ParamLedger>("USP_GET_PARAM_TABLE_LEDGER ", new { P_MODULEID = "10"}, commandType: CommandType.StoredProcedure).ToList();
            if(paramLedger.Count>0)
            {
                foreach (var i in paramLedger)
                {
                    PurchaseLeadger = i.STKTRANSFER_ACC_LEADGER.ToString().Trim();
                    RoundOffLeadger= i.ROUNDOFF_ACC_LEADGER.ToString().Trim();
                    Bkg_Dmg_ShrtgLeadger = i.BRKG_DAMG_SHRTG_ACC_LEADGER.ToString().Trim();
                    InsClaimLeadger = i.INSURANCECLAIM_ACC_LEADGER.ToString().Trim();
                }
            }
            List<CorporateOfficeID> corporateLedger = new List<CorporateOfficeID>();
            corporateLedger = _db.Query<CorporateOfficeID>("SELECT BRID FROM [M_BRANCH] where BRANCHTAG ='O' and ISMOTHERDEPOT ='True' ").ToList();
            if (corporateLedger.Count > 0)
            {
                foreach (var i in corporateLedger)
                {
                    HOLeadger = i.BRID.ToString().Trim();
                }
            }

            List<AccountPostingDispatchDetails> acpostingdetails = new List<AccountPostingDispatchDetails>();
            acpostingdetails = _db.Query<AccountPostingDispatchDetails>(" SELECT [STOCKDESPATCHDATE],[MOTHERDEPOTID],[TPUID],[TPUNAME],[INSURANCECOMPID],"+
                                                                        " INSURANCECOMPNAME,INSURANCENO,[TOTALDESPATCHVALUE],[TRANSPORTERID] " +
                                                                        " [STOCKDESPATCHID],[INVOICENO],[MOTHERDEPOTNAME],[TAXID],[TAXVALUE],[ROUNDOFFVALUE]"+
                                                                        " FROM [Vw_FactoryStockTransfer_AccPosting] WITH  (NOLOCK)" +
                                                                        " WHERE [STOCKDESPATCHID]='" + DepotTransferID + "'").ToList();
            if (acpostingdetails.Count > 0)
            {
                foreach (var i in acpostingdetails)
                {
                    recivedepot = i.MOTHERDEPOTID.ToString().Trim();
                    Factoryid = i.TPUID.ToString().Trim();
                    Factoryname = i.TPUNAME.ToString().Trim();
                    VchDate = i.STOCKDESPATCHDATE.ToString().Trim();
                    invoice = i.INVOICENO.ToString().Trim();
                    transporter = i.TRANSPORTERID.ToString().Trim();
                    InsuranceCompanyName = i.INSURANCECOMPNAME.ToString().Trim();
                    InsuranceNo = i.INSURANCENO.ToString().Trim();
                    TOTAL_DISPATCHVALUE = i.TOTALDESPATCHVALUE.ToString().Trim();
                    ROUNDOFFVALUE = i.ROUNDOFFVALUE.ToString().Trim();
                }
                List<ReferenceLedgerID> refLedgerID = new List<ReferenceLedgerID>();
                refLedgerID = _db.Query<ReferenceLedgerID>("SELECT REFERENCELEDGERID FROM [M_BRANCH] WHERE BRID ='" + recivedepot + "'").ToList();
                if (refLedgerID.Count > 0)
                {
                    foreach (var i in refLedgerID)
                    {
                        REFERENCELEDGER_RECEIVEDDEPOTID = i.REFERENCELEDGERID.ToString().Trim();
                    }
                }
                List<ReferenceLedgerName> refLedgerName = new List<ReferenceLedgerName>();
                refLedgerName = _db.Query<ReferenceLedgerName>("SELECT BRNAME FROM [M_BRANCH] WHERE BRID ='" + REFERENCELEDGER_RECEIVEDDEPOTID + "'").ToList();
                if (refLedgerName.Count > 0)
                {
                    foreach (var i in refLedgerName)
                    {
                        REFERENCELEDGER_RECEIVEDDEPONAME = i.BRNAME.ToString().Trim();
                    }
                }

                //Factory stock transfer leadger 
                DataRow dr = VoucherTable.NewRow();
                foreach (var i in acpostingdetails)
                {
                    if (i.TAXID.ToString().Trim() != "")
                    {
                        dr["GUID"] = Guid.NewGuid();
                        dr["LedgerId"] = Convert.ToString(i.TAXID).Trim();
                        dr["LedgerName"] = "";
                        dr["TxnType"] = Convert.ToString("1");
                        dr["Amount"] = Convert.ToString(i.TAXVALUE).Trim();

                        TAXVALUE = TAXVALUE + Convert.ToDecimal(i.TAXVALUE.ToString().Trim());

                        dr["BankID"] = Convert.ToString("");
                        dr["BankName"] = "";
                        dr["ChequeNo"] = Convert.ToString("");
                        dr["ChequeDate"] = Convert.ToString("");
                        dr["IsChequeRealised"] = Convert.ToString("N");
                        dr["Remarks"] = Convert.ToString("");
                        dr["DeductableAmount"] = Convert.ToString(i.TAXVALUE).Trim();

                        VoucherTable.Rows.Add(dr);
                        VoucherTable.AcceptChanges();
                    }
                }

                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(PurchaseLeadger);
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTAL_DISPATCHVALUE) - TAXVALUE - Convert.ToDecimal(ROUNDOFFVALUE));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTAL_DISPATCHVALUE) - TAXVALUE - Convert.ToDecimal(ROUNDOFFVALUE));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                //ROUND-OFF
                
                if (Convert.ToDecimal(ROUNDOFFVALUE) > 0)
                {
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("1");
                    dr["Amount"] = Convert.ToString((Convert.ToDecimal(ROUNDOFFVALUE)));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString((Convert.ToDecimal(ROUNDOFFVALUE)));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                //received Depot
                dr = VoucherTable.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = REFERENCELEDGER_RECEIVEDDEPOTID;
                dr["LedgerName"] = "";
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(Convert.ToDecimal(TOTAL_DISPATCHVALUE));
                dr["BankID"] = Convert.ToString("");
                dr["BankName"] = "";
                dr["ChequeNo"] = Convert.ToString("");
                dr["ChequeDate"] = Convert.ToString("");
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString("");
                dr["DeductableAmount"] = Convert.ToString(Convert.ToDecimal(TOTAL_DISPATCHVALUE));

                VoucherTable.Rows.Add(dr);
                VoucherTable.AcceptChanges();

                if (Convert.ToDecimal(ROUNDOFFVALUE) < 0)
                {
                    //ROUND-OFF
                    dr = VoucherTable.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(RoundOffLeadger);
                    dr["LedgerName"] = "";
                    dr["TxnType"] = Convert.ToString("0");
                    dr["Amount"] = Convert.ToString(-1 * (Convert.ToDecimal(ROUNDOFFVALUE)));
                    dr["BankID"] = Convert.ToString("");
                    dr["BankName"] = "";
                    dr["ChequeNo"] = Convert.ToString("");
                    dr["ChequeDate"] = Convert.ToString("");
                    dr["IsChequeRealised"] = Convert.ToString("N");
                    dr["Remarks"] = Convert.ToString("");
                    dr["DeductableAmount"] = Convert.ToString(-1 * (Convert.ToDecimal(ROUNDOFFVALUE)));

                    VoucherTable.Rows.Add(dr);
                    VoucherTable.AcceptChanges();
                }

                string XML = ConvertDatatableToXML(VoucherTable);
                string Narration = "Being goods transfer from " + Factoryname + " against bill No. " + invoice + " bill date " + VchDate + ".";
                //Voucher = VchEntry.InsertVoucherDetails("4", "Stock Transfer", Factoryid, Factoryname, "", VchDate, Finyr, Narration, Uid, "A", "", XML, "Y");
            }



            //dt = db.GetData(Sql);
            //if (dt.Rows.Count > 0)
            //{

                //if (Voucher == "2")       /* LEDGER NOT FOUND FROM SYSTEM */
                //{
                //    flag = 2;
                //    return 2;
                //}
                //else if (Voucher == "3")  /* DEBIT OR CREDIT AMOUNT NOT SAME */
                //{
                //    flag = 3;
                //    return 3;
                //}
                //else if (Voucher.Contains("|"))
                //{
                //    String[] vou1 = Voucher.Split('|');
                //    voucherID = vou1[0].Trim();

                //    VchEntry.VoucherDetails(voucherID, DepotTransferID);
                //    Voucher = string.Empty;
                //    voucherID = string.Empty;

                //    flag = 1;
                //}

            //}

            return flag;
        }
        #endregion

        
    }
}
