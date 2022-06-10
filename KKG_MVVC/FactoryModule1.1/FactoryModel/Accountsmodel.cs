using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
namespace FactoryModel
{
    public class Accountsmodel
    {

        public string vouchertypeid { get; set; }
        public string vouchertypename { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string paymentmode { get; set; }
        public string voucherdate { get; set; }
        public string finyear { get; set; }
        public string narration { get; set; }
        public string mode { get; set; }
        public string accid { get; set; }
        public string isvoucherappliocable { get; set; }
        public string isfrompage { get; set; }
        public string billno { get; set; }
        public string billdate { get; set; }
        public string grnno { get; set; }
        public string grdate { get; set; }
        public string vehicleno { get; set; }
        public string transport { get; set; }
        public string waybillno { get; set; }
        public string waybilldate { get; set; }
        public string billtargetfrompage { get; set; }
        public string drcrtds { get; set; }
        public string gstvpuchertypeid { get; set; }
        public string PaymentParty { get; set; }
        public string mtclaim { get; set; }
        public string userid { get; set; }



    }
    public class Region
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }
    public class Vouchertype
    {
        public string ID { get; set; }
        public string VoucherName { get; set; }
    }
    public class Accountstype
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class Departrment
    {
        public string DEPTID { get; set; }
        public string DEPTNAME { get; set; }
    }
    public class costcenter
    {
        public string COSTCENTREID { get; set; }
        public string COSTCENTRENAME { get; set; }
    }

    public class Product
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
    }

    public class costcategort
    {
        public string BSID { get; set; }
        public string BSNAME { get; set; }
    }


    public class Brand
    {
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string DIVCODE { get; set; }

    }
    public class GSTGroup
    {
        public string GROUP_TYPEID { get; set; }
        public string GROUP_TYPENAME { get; set; }
    }
    public class Dcjparty
    {
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }
    public class GSTNo
    {
        public string GSTNO { get; set; }
        public string STATEID { get; set; }
    }
    public class state
    {
        public int State_ID { get; set; }
        public string State_Name { get; set; }
    }
    public class messageresponse
    {

        public string response { get; set; }
    }
    public class responseint
    {
        public int response { get; set; }
    }

    public class Paymenttype
    {
        public string PAYMENTTYPEID { get; set; }
        public string PAYMENTTYPENAME { get; set; }
    }
    public class tdsapplicable
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public decimal? BYDEFAULTAMOUNT { get; set; }
        public decimal? TransactionAmount { get; set; }
        public decimal? DEDUCTABLEPERCENT { get; set; }
        public decimal? DEDUCTABLEAMOUNT { get; set; }
        public decimal? TDSAMOUNT { get; set; }
        public decimal? NonTaxableAmount { get; set; }
    }
    public class taxaction
    {
        public string isnegative { get; set; }
        public string isdrcr { get; set; }
    }
    public class gstgroup
    {
        public string GROUP_TYPEID { get; set; }
        public string GROUP_TYPENAME { get; set; }
    }
    public class thirdpartyvendor
    {
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }
    public class taxpercentage
    {
        public decimal ID { get; set; }
        public decimal NAME { get; set; }
        public decimal ISIGST { get; set; }

    }
    public class taxtype
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string ISIGST { get; set; }
    }
    public class fileupload
    {
        public string UPLOADACCOUNTSFILENAME { get; set; }
        public string filepath { get; set; }
    }
    public class gstpopup
    {
        public string gstcount { get; set; }
        public string oldpopup { get; set; }
    }
    public class groupcode
    {
        public string GROUPCODE { get; set; }

    }
    public class voucherlist
    {
        public string NARRATION { get; set; }
        public string DAYENDTAG { get; set; }
        public string LedgerName { get; set; }
        public decimal? AMOUNT { get; set; }
        public string VoucherApproved { get; set; }
        public string BranchName { get; set; }
        public string ISTRANSFERTOHO { get; set; }
        public string BranchID { get; set; }
        public string VoucherNo { get; set; }
        public string Date { get; set; }
        public string Mode { get; set; }
        public string VoucherTypeName { get; set; }
        public string VoucherTypeID { get; set; }
        public string AccEntryID { get; set; }
        public string PeriodID { get; set; }
        public string TDSRelated { get; set; }
    }
    public class Acchdr
    {
        public string IsGSTVoucher { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherTypeID { get; set; }
        public string BranchID { get; set; }
        public string Date { get; set; }
        public string Mode { get; set; }
        public string Narration { get; set; }
        public string RejectionNote { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string GRNo { get; set; }
        public string GRDate { get; set; }
        public string VehicleNo { get; set; }
        public string Transport { get; set; }
        public string WayBillNo { get; set; }
        public string WayBillDate { get; set; }
        public string IsPaymentType { get; set; }
        public string PaymentParty { get; set; }
        public string MTClaimLedgerID { get; set; }
        public string MTClaimLedgerName { get; set; }
        public string MTClaimChecker1 { get; set; }
        public string MTClaimChecker2 { get; set; }
        public string MTClaimChecker3 { get; set; }
    }
    public class addVoucherTable
    {
        public string GUID { get; set; }
        public string LedgerId { get; set; }
        public string LedgerName { get; set; }
        public string TxnType { get; set; }
        public string Amount { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public string PAYMENTTYPEID { get; set; }
        public string PAYMENTTYPENAME { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string IsChequeRealised { get; set; }
        public string Remarks { get; set; }
        public string ChequeRealisedNo { get; set; }
        public string ChequeRealisedDate { get; set; }
        public string DeductableAmount { get; set; }
        public string DeductablePercentage { get; set; }
        public string DeductableLedgerId { get; set; }
        public string IsCostCenter { get; set; }
        public string IsTagInvoice { get; set; }
        public string NonTaxableAmount { get; set; }
        public string BYDEFAULTAMOUNT { get; set; }
        public string TransactionAmount { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string taxapplicable { get; set; }
    }
    public class accvoucher
    {
        public List<Acchdr> Acchdr { get; set; }
        public List<addVoucherTable> addVoucherTable { get; set; }
    }
    public class paymentparty
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class bankname
    {
        public string CHEQUENO { get; set; }
        public string CHEQUENOWITHBANK { get; set; }
    }

    public class backdate
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class invoicedetails
    {
        public string OUTSTANDING { get; set; }
        public string GUID { get; set; }
        public string LedgerId { get; set; }
        public string LedgerName { get; set; }
        public string InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceOthers { get; set; }
        public string InvoiceBranchID { get; set; }
        public string InvoiceBranchName { get; set; }
        public string VoucherType { get; set; }
        public string BranchID { get; set; }
        public string InvoiceAmt { get; set; }
        public string AlreadyAmtPaid { get; set; }
        public string ReturnAmt { get; set; }
        public string RemainingAmt { get; set; }
        public string AmtPaid { get; set; }
        public string Type { get; set; }
    }
    public class Accresponse
    {
        public string voucherid { get; set; }
        public string voucherno { get; set; }
        public string isautovoucher { get; set; }
    }
}
    







