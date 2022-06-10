using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FactoryModel;
namespace FactoryDatacontext
{

    public class Accountscontext
    {

        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<Region> BindRegion(string usertype, string userid)
        {
            List<Region> regionlist = new List<Region>();
            try
            {
                regionlist = _db.Query<Region>("USP_REGION_SHOW", new { USERTYPE = usertype, USERID = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return regionlist;
        }
        public List<messageresponse> isho()
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("USP_isholedger", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<Vouchertype> BindVoucherType()
        {
            List<Vouchertype> vouchertype = new List<Vouchertype>();
            try
            {
                vouchertype = _db.Query<Vouchertype>("USP_ACC_VOUCHERTYPES_GET", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return vouchertype;
        }
        public List<Accountstype> BindAccountTypedr(string vouchertype, string region)
        {
            List<Accountstype> vouchertypename = new List<Accountstype>();
            try
            {
                vouchertypename = _db.Query<Accountstype>("USP_ACC_VOUCHERTYPEACC_GET", new { VOUCHERTYPEID = vouchertype, TYPE = "D", REGIOIUNID = region }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return vouchertypename;
        }
        public List<Accountstype> BindAccountTypecr_MTClaim(string vouchertype, string region)
        {
            List<Accountstype> vouchertypename = new List<Accountstype>();
            try
            {
                vouchertypename = _db.Query<Accountstype>("USP_ACC_VOUCHERTYPEACC_GET_MTCLAIM", new { VOUCHERTYPEID = vouchertype, TYPE = "C", REGIOIUNID = region }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return vouchertypename;
        }
        public List<Accountstype> BindAccountTypedr_MTClaim(string vouchertype, string region)
        {
            List<Accountstype> vouchertypename = new List<Accountstype>();
            try
            {
                vouchertypename = _db.Query<Accountstype>("USP_ACC_VOUCHERTYPEACC_GET_MTCLAIM", new { VOUCHERTYPEID = vouchertype, TYPE = "D", REGIOIUNID = region }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return vouchertypename;
        }
        public List<bankname> BindCHEQUENOLIST(string bankid, string branchid)
        {
            List<bankname> bankname = new List<bankname>();
            try
            {
                bankname = _db.Query<bankname>("FETCH_CHEQUEBOOKLET", new { P_BANKID = bankid, BranchID = branchid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return bankname;
        }
        public List<Accountstype> BindAccountTypecr(string vouchertype, string region)
        {
            List<Accountstype> vouchertypename = new List<Accountstype>();
            try
            {
                vouchertypename = _db.Query<Accountstype>("USP_ACC_VOUCHERTYPEACC_GET", new { VOUCHERTYPEID = vouchertype, TYPE = "C", REGIOIUNID = region }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return vouchertypename;
        }
        public List<Departrment> BindDepartment()
        {
            List<Departrment> department = new List<Departrment>();
            try
            {
                department = _db.Query<Departrment>("USP_DEPARTMENT_SHOW", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return department;
        }
        public List<costcenter> BindCostCenter()
        {
            List<costcenter> costcenter = new List<costcenter>();
            try
            {
                costcenter = _db.Query<costcenter>("USP_ACC_COSTCWENTRE_GET", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return costcenter;
        }
        public List<Paymenttype> Bindpaymenttype()
        {
            List<Paymenttype> Paymenttype = new List<Paymenttype>();
            try
            {
                Paymenttype = _db.Query<Paymenttype>("USP_ACC_PAYMENT_TYPE_GET", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Paymenttype;
        }
        public List<paymentparty> Bindpaymentparty()
        {
            List<paymentparty> paymentparty = new List<paymentparty>();
            try
            {
                paymentparty = _db.Query<paymentparty>("SP_FILL_PaymentParty", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return paymentparty;
        }
        public List<Product> BindProduct()
        {
            List<Product> product = new List<Product>();
            try
            {
                product = _db.Query<Product>("usp_PRODUCTMADSTER_SHOW", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return product;
        }

        public List<thirdpartyvendor> BindDCJ_Party(string djgroup)
        {
            List<thirdpartyvendor> thirdpartyvendor = new List<thirdpartyvendor>();
            try
            {
                thirdpartyvendor = _db.Query<thirdpartyvendor>("USP_GET_THIREDPARTY_VENDOR", new { P_GroupCode = djgroup }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return thirdpartyvendor;
        }
        public List<taxpercentage> BindTaxPercentage(string partyname, string stateid, string regionid)
        {
            List<taxpercentage> taxpercentage = new List<taxpercentage>();
            try
            {
                taxpercentage = _db.Query<taxpercentage>("USP_GET_TAXPERCENTAGE", new { P_PartyID = partyname, P_StateID = stateid, P_DepoID = regionid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return taxpercentage;
        }
        public List<taxpercentage> BindTaxType(string PartyID, string stateID, string DEPOID)
        {
            List<taxpercentage> taxpercentage = new List<taxpercentage>();
            try
            {
                taxpercentage = _db.Query<taxpercentage>("USP_GET_TAXTYPE", new { P_PartyID = PartyID, P_StateID = stateID, P_DepoID = DEPOID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return taxpercentage;
        }
        public List<taxtype> BindDCJ_TaxTypeName(string TaxTypeID)
        {
            List<taxtype> taxpercentage = new List<taxtype>();
            try
            {
                taxpercentage = _db.Query<taxtype>("SP_GET_TAXTYPE_NAME", new { P_TaxTypeID = TaxTypeID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return taxpercentage;
        }
        public List<taxtype> Gettaxtype(string partyid, string stateid, string depoid)
        {
            List<taxtype> taxtype = new List<taxtype>();
            try
            {
                taxtype = _db.Query<taxtype>("USP_GET_TAXTYPE", new { P_PartyID = partyid, P_StateID = stateid, P_DepoID = depoid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return taxtype;
        }
        public List<messageresponse> CheckInvoiceNo(string PartyID, string finyr, string InvoiceNo)
        {
            List<messageresponse> msg = new List<messageresponse>();
            try
            {
                msg = _db.Query<messageresponse>("CHECK_GST_INVOICE", new { P_PartyID = PartyID, P_FINYEAR = finyr, P_InvoiceNo = InvoiceNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public List<messageresponse> CheckBANKID(string AccountID)
        {
            List<messageresponse> msg = new List<messageresponse>();
            try
            {
                msg = _db.Query<messageresponse>("CHECK_BANKIDV2", new { P_ACCENTRYID = AccountID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public List<gstpopup> CheckGSTDr(string xmlDRGST)
        {
            List<gstpopup> gstpopup = new List<gstpopup>();
            try
            {
                gstpopup = _db.Query<gstpopup>("SP_GETGSTLEDGER", new { p_InvoiceDebitInvoice = xmlDRGST }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return gstpopup;
        }
        public List<costcategort> BindCostCenterCatagory()
        {
            List<costcategort> costcategory = new List<costcategort>();
            try
            {
                costcategory = _db.Query<costcategort>("USP_GET_BUSINESSSEGMENT_LIST", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return costcategory;
        }
        public List<Brand> BindBrand()
        {
            List<Brand> brand = new List<Brand>();
            try
            {
                brand = _db.Query<Brand>("SP_DIVISION_SHOW", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return brand;
        }
        public List<groupcode> checkwxpenseledgerornot(string accdr)
        {
            List<groupcode> response = new List<groupcode>();
            try
            {
                response = _db.Query<groupcode>("SP_CHECK_EXPENSES_LEDGERGET", new { P_LedgerID = accdr }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public List<GSTGroup> BindGSTGroup()
        {
            List<GSTGroup> GSTGroup = new List<GSTGroup>();
            try
            {
                GSTGroup = _db.Query<GSTGroup>("USP_ACC_ACCOUNTGROUPTYPE_GET", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return GSTGroup;
        }
        public List<GSTNo> BindDCJ_GstNo(string party, string statename)
        {
            List<GSTNo> GSTGroup = new List<GSTNo>();
            try
            {
                GSTGroup = _db.Query<GSTNo>("USP_GET_GSTNO", new { P_PartyID = party, P_StateID = statename }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return GSTGroup;
        }
        public List<state> BindAllState()
        {
            List<state> statelist = new List<state>();
            try
            {
                statelist = _db.Query<state>("USP_STATE_SHOW", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return statelist;
        }
        public List<responseint> costapplicable(string accdr)
        {
            List<responseint> response = new List<responseint>();
            try
            {
                response = _db.Query<responseint>("USP_ACC_COSTCENTRE_APPLICABLE", new { AccountID = accdr }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        #region===invoice
        public List<invoicedetails> Outstanding(string LedgerID, string RegionID, string FromDate, string ToDate, string Finyear)
        {
            List<invoicedetails> invoicedetails = new List<invoicedetails>();
            try
            {
                invoicedetails = _db.Query<invoicedetails>("USP_ACC_LEDGER_OUTSTANDING", new { P_LEDGERID = LedgerID, P_REGIONID = RegionID, P_FRMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return invoicedetails;
        }
        public List<invoicedetails> InvoiceDetails(string VoucherID, string Leadgerid, string VouchertYpe, string Branchid)
        {
            List<invoicedetails> invoicedetails = new List<invoicedetails>();
            try
            {
                invoicedetails = _db.Query<invoicedetails>("SP_ACC_INVOICEDETAILS", new { Voucherid = VoucherID, LeadgerID = Leadgerid, VoucherType = VouchertYpe, BranchID = Branchid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return invoicedetails;
        }
        #endregion
        public List<tdsapplicable> TaxApplicable(string AccountID, string VoucherDate, string Finyear, decimal Amount, string BranchID, string VoucherType, string DrCr, decimal CalAmt)
        {
            List<tdsapplicable> tdsapplicable = new List<tdsapplicable>();
            try
            {
                tdsapplicable = _db.Query<tdsapplicable>("USP_ACC_TAXAPPLICALE_TDS", new
                {
                    P_LEDGERID = AccountID,
                    P_VOUCHERDATE = VoucherDate,
                    P_FINYEAR = Finyear,
                    P_AMOUNT = Amount,
                    P_BRANCHID = BranchID,
                    P_VOUCHERTYPEID = VoucherType,
                    P_DrCr = DrCr,
                    P_CalAmt = CalAmt
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return tdsapplicable;
        }
        public List<taxaction> TaxAction(string taxid)
        {
            List<taxaction> taxaction = new List<taxaction>();
            try
            {
                taxaction = _db.Query<taxaction>("USP_TAXACTION", new
                {
                    TaxID = taxid

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return taxaction;
        }
        public List<messageresponse> vouchersave(Accountsmodel accmodel, string finyr, string userid, string xml, string costcenter, string gstdetail)

        {
            List<messageresponse> response = new List<messageresponse>();
            response = _db.Query<messageresponse>("SP_ACC_VOUCHERV2",
                                                        new
                                                        {
                                                            p_VoucherTypeId = accmodel.vouchertypeid,
                                                            p_VoucherTypeName = accmodel.vouchertypename,
                                                            p_BranchId = accmodel.BRID,
                                                            p_BranchName = accmodel.BRNAME,
                                                            p_PayementMode = accmodel.paymentmode,
                                                            p_VoucherDate = accmodel.voucherdate,
                                                            p_Finyear = finyr,
                                                            p_Narration = accmodel.narration,
                                                            p_CREATEDBY = userid,
                                                            p_FLAG = accmodel.mode,
                                                            p_AccEntryID = accmodel.accid,
                                                            p_VoucherDetails = xml,
                                                            p_ISVOUCHERAPPROVED = accmodel.isvoucherappliocable,
                                                            p_InvoiceVoucherDetails = "",
                                                            p_CostCenterDetails = costcenter,
                                                            p_ISFROMPAGE = accmodel.isfrompage,
                                                            p_InvoiceDebitInvoice= gstdetail,
                                                            p_BILLNO = accmodel.billno,
                                                            p_BILLDATE = accmodel.billdate,
                                                            p_GRNO = accmodel.grnno,
                                                            p_GRDATE = accmodel.grdate,
                                                            p_VECHILENO = accmodel.vehicleno,
                                                            p_TRANSPORT = accmodel.transport,
                                                            p_WAYBILLNO = accmodel.waybillno,
                                                            p_WAYBILLDATE = accmodel.waybilldate,
                                                            p_OnlyBillTagFromPage = accmodel.billtargetfrompage,
                                                            p_DrCr = accmodel.drcrtds,
                                                            p_GSTVoucherType = accmodel.gstvpuchertypeid,
                                                            p_Ismtclaim = accmodel.mtclaim

                                                        }, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }
        public List<messageresponse> InsertFileUpload(string AccEntryID, string TAG, string InvoiceXML)

        {
            List<messageresponse> response = new List<messageresponse>();
            response = _db.Query<messageresponse>("USP_ACC_FILE_UPLOAD",
                                                     new
                                                     {
                                                         p_ACCENTRYID = AccEntryID,
                                                         p_FLAG = TAG,
                                                         p_XML = InvoiceXML
                                                     }, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }
        public List<voucherlist> BindVoucherDetails(string FromDate, string ToDate, string VoucherID, string DepotID, string checker, string userid, string finyr, string IsMTClaim)
        {
            List<voucherlist> voucherlist = new List<voucherlist>();
            try
            {
                voucherlist = _db.Query<voucherlist>("USP_VOUCHER_HEADERDETAILS", new
                {
                    p_FORMDATE = FromDate,
                    p_TODATE = ToDate,
                    p_USERID = userid,
                    p_FINYEAR = finyr,
                    p_VOUCHERTYPEID = VoucherID,
                    p_CHECKER = checker,
                    p_DEPOTID = DepotID,
                    p_FROMUNLOCKVOUCHER = "N",
                    p_ISMTCLAIM = IsMTClaim
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return voucherlist;
        }
        public List<Region> PaymentVoucherDetails(string AccEntryID)
        {
            List<Region> Region = new List<Region>();
            try
            {
                Region = _db.Query<Region>("USP_ACC_PAYMENT_DETAILSv2", new { p_AccEntryId = AccEntryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Region;
        }
        public List<messageresponse> deletevoucher(string voucherid, string userid)
        {
            List<messageresponse> msg = new List<messageresponse>();
            try
            {
                msg = _db.Query<messageresponse>("SP_ACC_VOUCHER_DELETE_V2", new { P_ACCENTRYID = voucherid, P_DELETEDDBY = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public List<messageresponse> CheckAutoPopup1(string voucherid)
        {
            List<messageresponse> msg = new List<messageresponse>();
            try
            {
                msg = _db.Query<messageresponse>("USP_AUTOPOPUP", new { P_ACCENTRYID = voucherid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public accvoucher editdtl(string voucherid)
        {
            accvoucher accvoucher = new accvoucher();

            var reader = _db.QueryMultiple("SP_ACC_VOUCHERDETAILS", new { p_AccEntryId = voucherid }, commandType: CommandType.StoredProcedure);
            var Acchdr = reader.Read<Acchdr>().ToList();
            var addVoucherTable = reader.Read<addVoucherTable>().ToList();
            accvoucher.Acchdr = Acchdr;
            accvoucher.addVoucherTable = addVoucherTable;
            return accvoucher;

        }
        public List<messageresponse> chkbackdateuser(string userid, string finyear, string Type)
        {
            List<messageresponse> msg = new List<messageresponse>();
            try
            {
                msg = _db.Query<messageresponse>("USP_BACKDATE_SETTING_CHEKINGv2", new { P_USERID = userid, P_FINYEAR = finyear, P_Type=Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public List<backdate> CheckDateRange(string userid, string finyear, string Type)
        {
            List<backdate> backdate = new List<backdate>();
            try
            {
                backdate = _db.Query<backdate>("USP_BACKDATE_SETTING_CHEKING_DATERANGE", new { P_USERID = userid, P_FINYEAR = finyear, P_Type = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return backdate;
        }
        public List<messageresponse> voucherapproved(string VoucherID, string Ledgerid, string LedgerName, string userid, string Finyear)
        {
            List<messageresponse> response = new List<messageresponse>();
            response = _db.Query<messageresponse>("USP_ACC_MT_CLAIM_APPROVAL",
                                    new
                                    {
                                        P_ACCENTRYID = VoucherID,
                                        P_LEDGERID = Ledgerid,
                                        P_LEDGERNAME = LedgerName,
                                        P_USERID = userid,
                                        P_FINYEAR = Finyear
                                    }, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }
    }
}
    

