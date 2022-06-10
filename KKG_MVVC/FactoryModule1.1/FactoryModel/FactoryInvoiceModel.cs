using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
    public class FactoryFGInvoiceModel
    {
        public string FLAG { get; set; }
        public string FGInvoiceID { get; set; }
        public string InvoiceType { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string SaleOrderID { get; set; }
        public string SaleOrderNo { get; set; }
        public string PaymentMode { get; set; }
        public string WAYBILLNO { get; set; }
        public string WAYBILLID { get; set; }
        public string ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string INSURANCE_NO { get; set; }
        public string Tranportmode { get; set; }
        public string NO { get; set; }
        public string TransporterID { get; set; }
        public string TransporterNAME { get; set; }
        public string VehichleNo { get; set; }
        public string LrGrNo { get; set; }
        public string LrGrDate { get; set; }
        public string InvoiceDate { get; set; }
        public string GatepassNo { get; set; }
        public string GatepassDate { get; set; }
        public string hdnTransporterID { get; set; }
        public string hdnTransporterNAME { get; set; }
        public string ShippingAddress { get; set; }
        public string DeliveryDate { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string HSN { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PSID { get; set; }
        public string PSNAME { get; set; }
        public string TAXCOUNT { get; set; }
        public string TAXNAME { get; set; }
        public string TAXPERCENTAGE { get; set; }
        public string TAXRELATEDTO { get; set; }
        public string BATCHNO { get; set; }
        public string MRP { get; set; }
        public string Rate { get; set; }
        public string OrderQty { get; set; }
        public string DeliveredQty { get; set; }
        public string RemainingQty { get; set; }
        public string StockQty { get; set; }
        public string InvoiceQty { get; set; }
        public string BasicAmt { get; set; }
        public string TaxAmt { get; set; }
        public string GrossAmt1 { get; set; }
        public string GrossAmt { get; set; }
        public string RoundOff { get; set; }
        public string ProformaAmount { get; set; }
        public string AdjustmentAmount { get; set; }
        public string NetAmt { get; set; }
        public string TotalCase { get; set; }
        public string TotalPcs { get; set; }
        public string Remarks { get; set; }
        public string FGInvoiceNo { get; set; }
        public string VesselName { get; set; }
        public string ExchangeRate { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string ProformaOrderID { get; set; }
        public string ProformaOrderNo { get; set; }
        public string ExportRefNo { get; set; }
        public string OtherRefNo { get; set; }
        public string LoadingPortID { get; set; }
        public string LoadingPortName { get; set; }
        public string DischargePortID { get; set; }
        public string DischargePortName { get; set; }
        public string FinalDestination { get; set; }
        public string ShippingBill { get; set; }
        public string ShippingDate { get; set; }
        public string ContainerNo { get; set; }
        public string VoyNo { get; set; }
        public string LCNo { get; set; }
        public string LCDate { get; set; }
        public string LCBank { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string BankAddress { get; set; }
        public string IFSC { get; set; }
        public string SwiftCode { get; set; }
        public string AccNo { get; set; }
        public string Consignee { get; set; }
        public string NotifyParty { get; set; }
        public string InvoiceSeqType { get; set; }
        public string FreeTag { get; set; }
        public string PackInvoiceNo { get; set; }
        public string PackCaseQty { get; set; }
        public string AmountInWords { get; set; }
        public string DeliveryTo { get; set; }
        public string DescPackages { get; set; }
        public string NetSaleAmt { get; set; }
        public string TCSPercent { get; set; }
        public string TCSApplicable { get; set; }
        public string TCSAmt { get; set; }
        public string TCSNetAmt { get; set; }
        public string StorelocationID { get; set; }
        public string StorelocationName { get; set; }
        public List<InvoiceDetailsFG> InvoiceDetailsFG { get; set; }
        public List<FreeDetailsFG> FreeDetailsFG { get; set; }
        public List<PackingDetails> PackingDetails { get; set; }

    }

    public class TradingStorelocation
    {
        public string StorelocationID { get; set; }
        public string StorelocationName { get; set; }
    }

    public class FGSourceDepot
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }

    public class FGCustomer
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }

    public class ExportCustomer
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }

    public class Country
    {
        public string COUNTRYID { get; set; }
        public string COUNTRYNAME { get; set; }
    }

    public class LoadingPort
    {
        public string LoadingPortID { get; set; }
        public string LoadingPortName { get; set; }
    }

    public class DischargePort
    {
        public string DischargePortID { get; set; }
        public string DischargePortName { get; set; }
    }

    public class FGSaleOrder
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
    }

    public class ExportSaleOrder
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
    }

    public class Proforma
    {
        public string PROFORMAINVOICEID { get; set; }
        public string PROFORMAINVOICENO { get; set; }
    }
    public class FGShipplingAddress
    {
        public string ADDRESS { get; set; }
    }

    public class FGTaxcount
    {
        public string TAXCOUNT { get; set; }
        public string NAME { get; set; }
        public string PERCENTAGE { get; set; }
        public string RELATEDTO { get; set; }
    }
    public class FGTaxcountList
    {
        public string TAXCOUNT { get; set; }
        public string TAXNAME { get; set; }
        public string TAXPERCENTAGE { get; set; }
        public string TAXRELATEDTO { get; set; }
    }

    public class InvoiceProductList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string GROUPID { get; set; }
        public string SALEORDERDATE { get; set; }
    }

    public class ExportProductList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string SEQUENCENO { get; set; }
        public string DIVNAME { get; set; }
        public string UNITVALUE { get; set; }
    }

    public class OrderDetailsList
    {
        public string ORDERQTY { get; set; }
        public string DESPATCHQTY { get; set; }
        public string ORDERQTYPCS { get; set; }
        public string DESPATCHQTYPCS { get; set; }
        public string REMAININGQTY { get; set; }
        public string REMAININGQTYPCS { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string STOCKQTY { get; set; }
    }

    public class TradingOrderDetailsList
    {
        public string ORDERQTY { get; set; }
        public string DESPATCHQTY { get; set; }
        public string REMAININGQTY { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string STOCKQTY { get; set; }
    }

    public class ProformaDetailsList
    {
        public string EXPORTERREFNO { get; set; }
        public string OTHERREFNO { get; set; }
        public string DISCHARGEPORT { get; set; }
        public string FINALDESTINATION { get; set; }
        public string CONSIGNEE { get; set; }
        public string NOTIFYPARTIES { get; set; }
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string BRANCHNAME { get; set; }
        public string BANKADDRESS { get; set; }
        public string IFSCODE { get; set; }
        public string SWIFTCODE { get; set; }
        public string ACCOUNTNO { get; set; }
    }

    public class LCDetailsList
    {
        public string LCNO { get; set; }
        public string LCDATE { get; set; }
        public string LCBANK { get; set; }
        
    }

    public class InvoiceDetailsFG
    {

        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public decimal MRP { get; set; }
        public decimal RATE { get; set; }
        public decimal QTY { get; set; }
        public decimal QTYPCS { get; set; }
        public decimal AMOUNT { get; set; }
        public string BATCHNO { get; set; }
        public decimal ASSESMENTPERCENTAGE { get; set; }
        public decimal TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public decimal NSR { get; set; }
        public decimal RATEDISC { get; set; }
        public decimal DISCVALUE { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
        public string TAG { get; set; }
    }

    public class FreeDetailsFG
    {

        public string SCHEME_PRODUCT_ID { get; set; }
        public string SCHEME_PRODUCT_NAME { get; set; }
        public decimal QTY { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal SCHEME_QTY { get; set; }
        public decimal MRP { get; set; }
        public decimal BRATE { get; set; }
        public decimal AMOUNT { get; set; }
        public string BATCHNO { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string SCHEME_PRODUCT_BATCHNO { get; set; }
        public string STORELOCATIONID { get; set; }
    }

    public class PackingDetails
    {

        public string SALEINVOICEID { get; set; }
        public string SALEORDERID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public decimal QTY { get; set; }
        public decimal STARTPOSITION { get; set; }
        public decimal ENDPOSITION { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string NETWEIGHT { get; set; } 
    }

    public class InvoiceConvertedQty
    {
        public string CONVERTED_QTY { get; set; }
    }

    public class InvoiceAssesment
    {
        public string ASSESMENT { get; set; }
    }

    public class InvoicePcsQty
    {
        public string QTYINPCS { get; set; }
    }

    public class InvoiceNetweight
    {
        public string NETWEIGHT { get; set; }
    }

    public class InvoiceGrossweight
    {
        public string GROSSWEIGHT { get; set; }
    }

    public class InvoiceHSN
    {
        public string HSNCODE { get; set; }
    }

    public class InvoiceHSNTaxPercentage
    {
        public string HSNTAX { get; set; }
    }

    public class InvoiceHSNTaxID
    {
        public string TAXID { get; set; }
    }

    public class CGSTPercentage
    {
        public string CGSTTAX { get; set; }
    }

    public class CGST
    {
        public string CGSTID { get; set; }
    }

    public class SGSTPercentage
    {
        public string SGSTTAX { get; set; }
    }

    public class SGST
    {
        public string SGSTID { get; set; }
    }

    public class TCSPercentage
    {
        public string TCSTAX { get; set; }
    }

    public class TCS
    {
        public string TCSID { get; set; }
    }

    public class InvoiceCalculateAmtInPcsFAC
    {
        public List<InvoiceAssesment> assesments { get; set; }
        public List<InvoicePcsQty> pcsqty { get; set; }
        public List<InvoiceNetweight> netweight { get; set; }
        public List<InvoiceGrossweight> grossweight { get; set; }
        public List<InvoiceHSN> hsn { get; set; }
        public List<InvoiceHSNTaxPercentage> hsnTax { get; set; }
        public List<InvoiceHSNTaxID> hsnTaxID { get; set; }
        
    }

    public class InvoiceCalculateAmtInPcsIntraFAC
    {
        public List<InvoiceAssesment> assesments { get; set; }
        public List<InvoicePcsQty> pcsqty { get; set; }
        public List<InvoiceNetweight> netweight { get; set; }
        public List<InvoiceGrossweight> grossweight { get; set; }
        public List<InvoiceHSN> hsn { get; set; }
        public List<CGSTPercentage> cgstpercentage { get; set; }
        public List<CGST> cgst { get; set; }
        public List<SGSTPercentage> sgstpercentage { get; set; }
        public List<SGST> sgst { get; set; }

    }

    public class TradingCalculateAmtInPcsIntraFAC
    {
        public List<InvoiceAssesment> assesments { get; set; }
        public List<InvoicePcsQty> pcsqty { get; set; }
        public List<InvoiceNetweight> netweight { get; set; }
        public List<InvoiceGrossweight> grossweight { get; set; }
        public List<InvoiceHSN> hsn { get; set; }
        public List<CGSTPercentage> cgstpercentage { get; set; }
        public List<CGST> cgst { get; set; }
        public List<SGSTPercentage> sgstpercentage { get; set; }
        public List<SGST> sgst { get; set; }
        public List<TCSPercentage> tcspercentage { get; set; }
        public List<TCS> tcs { get; set; }

    }

    public class ProformaAmount
    {
        public string NETAMOUNT { get; set; }
    }

    public class AdjustmentAmount
    {
        public string ADJAMOUNT { get; set; }
    }

    public class ExportProformaAmountDetails
    {
        public List<ProformaAmount> proformaamount { get; set; }
        public List<AdjustmentAmount> adjustmentamount { get; set; }
    }

    public class FGInvoiceList
    {
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string WAYBILLNO { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string DISTRIBUTORID { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string NEXTLEVELID { get; set; }
        public string ISVERIFIED { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string INTRANSITDESC { get; set; }
        public string FORMREQUIRED { get; set; }
        public string FORMNO { get; set; }
        public string FORMDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string NETAMOUNT { get; set; }
        public string USERNAME { get; set; }
    }

    public class ExportInvoiceList
    {
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string DISTRIBUTORID { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string NEXTLEVELID { get; set; }
        public string ISVERIFIED { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string LOADINGPORTNAME { get; set; }
        public string DISCHARGEPORTNAME { get; set; }
        public string FINALDESTINATION { get; set; }
        public string TOTALCASEPACK { get; set; }
        public string TOTALPCS { get; set; }
        public string NETAMOUNT { get; set; }
        public string USERNAME { get; set; }
    }


    public class ProformaTax
    {
        public string HSNTAX { get; set; }
    }

    public class ProformaInvoiceList
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERDATE { get; set; }
        public string PROFORMAINVOICEID { get; set; }
        public string PROFORMAINVOICEDATE { get; set; }
        public string HSNCODE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string MRP { get; set; }
        public string BCP { get; set; }
        public string INR { get; set; }
        public string QTY { get; set; }
        public string QTYPCS { get; set; }
        public string BATCHNO { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string AMOUNT { get; set; }
        public string AMOUNTINR { get; set; }
        public string TOTMRP { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
    }

    public class InvoiceHeaderEditFG
    {
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string WAYBILLNO { get; set; }
        public string DISTRIBUTORID { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string TRANSPORTERID { get; set; }
        public string REMARKS { get; set; }
        public string GETPASSNO { get; set; }
        public string GETPASSDATE { get; set; }
        public string PAYMENTMODE { get; set; }
        public string GROUPID { get; set; }
        public string GROUPNAME { get; set; }
        public string DELIVERYADDRESSID { get; set; }
        public string DELIVERYADDRESS { get; set; }
        public string ICDSNO { get; set; }
        public string ICDSDATE { get; set; }
        public string FORMREQUIRED { get; set; }
        public string NOTE { get; set; }
        public string TOTALPCS { get; set; }
        public string INVOICETYPEID { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string INSURANCENUMBER { get; set; }
        public string WAYBILLNO_NEW { get; set; }
        public string WAYBILLDATE { get; set; }
    }

    public class InvoiceDetailsEditFG
    {
        public string SALEINVOICEAUTOID { get; set; }
        public string SALEORDERID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string SALEORDERDATE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string HSNCODE { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string QTY { get; set; }
        public string QTYPCS { get; set; }
        public string BATCHNO { get; set; }
        public string AMOUNT { get; set; }
        public string TOTMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string RATEDISC { get; set; }
        public string NSR { get; set; }
        public string RATEDISCVALUE { get; set; }
        public string GROSSWEIGHT { get; set; }
    }

    public class InvoiceDetailsEditTrading
    {
        public string SALEINVOICEAUTOID { get; set; }
        public string SALEORDERID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string SALEORDERDATE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string HSNCODE { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string QTY { get; set; }
        public string QTYPCS { get; set; }
        public string BATCHNO { get; set; }
        public string AMOUNT { get; set; }
        public string TOTMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string RATEDISC { get; set; }
        public string NSR { get; set; }
        public string RATEDISCVALUE { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
    }

    public class InvoiceTaxcountEditFG
    {
        public string TAXID { get; set; }
        public string NAME { get; set; }
        public string RELATEDTO { get; set; }
    }

    public class InvoiceTaxEditFG
    {
        public string SALEORDERID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string TAXID { get; set; }
        public string TAXNAME { get; set; }
        public decimal TAXPERCENTAGE { get; set; }
        public decimal TAXVALUE { get; set; }
        public string PRIMARYPRODUCTID { get; set; }
        public string PRIMARYPRODUCTBATCHNO { get; set; }
        public string TAG { get; set; }
        public decimal MRP { get; set; }
    }

    public class InvoiceFooterEditFG
    {
        public decimal TOTALSALEINVOICEVALUE { get; set; }
        public decimal OTHERCHARGESVALUE { get; set; }
        public decimal ADJUSTMENTVALUE { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
        public decimal TOTALSPECIALDISCVALUE { get; set; }
        public string REBETSCHEMEID { get; set; }
        public decimal GROSSREBATEPERCENTAGE { get; set; }
        public decimal GROSSREBATEVALUE { get; set; }
        public decimal ADDFRETCHARGEPERCENTAGE { get; set; }
        public decimal ADDFRETCHARGEVALUE { get; set; }
        public decimal ADDSSMARGINPERCENTAGE { get; set; }
        public decimal ADDSSMARGINAMT { get; set; }
        public decimal ACTUALTOTCASE { get; set; }
        public string TOTALGROSSWT { get; set; }
        public decimal TOTALTAXAMT { get; set; }
        public decimal TCSPERCENT { get; set; }
        public decimal TCSAMOUNT { get; set; }
        public decimal TCSNETAMOUNT { get; set; }
    }

    public class InvoiceOrderDetailsEditFG
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal ORDERQTY { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZEName { get; set; }
    }

    public class InvoiceOrderHeaderEditFG
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
       
    }

    public class InvoiceEditFG
    {
        public List<InvoiceHeaderEditFG> InvoiceHeaderEditFG { get; set; }
        public List<InvoiceDetailsEditFG> InvoiceDetailsEditFG { get; set; }
        public List<InvoiceTaxcountEditFG> InvoiceTaxcountEditFG { get; set; }
        public List<InvoiceTaxEditFG> InvoiceTaxEditFG { get; set; }
        public List<InvoiceFooterEditFG> InvoiceFooterEditFG { get; set; }
        public List<InvoiceOrderDetailsEditFG> InvoiceOrderDetailsEditFG { get; set; }
        public List<InvoiceOrderHeaderEditFG> InvoiceOrderHeaderEditFG { get; set; }
    }

    public class InvoiceEditTrading
    {
        public List<InvoiceHeaderEditFG> InvoiceHeaderEditFG { get; set; }
        public List<InvoiceDetailsEditTrading> InvoiceDetailsEditTrading { get; set; }
        public List<InvoiceTaxcountEditFG> InvoiceTaxcountEditFG { get; set; }
        public List<InvoiceTaxEditFG> InvoiceTaxEditFG { get; set; }
        public List<InvoiceFooterEditFG> InvoiceFooterEditFG { get; set; }
        public List<InvoiceOrderDetailsEditFG> InvoiceOrderDetailsEditFG { get; set; }
        public List<InvoiceOrderHeaderEditFG> InvoiceOrderHeaderEditFG { get; set; }
    }

    public class ExportHeaderEdit
    {
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string TOTALCASEPACK { get; set; }
        public string DISTRIBUTORID { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string GROUPID { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string REMARKS { get; set; }
        public string EXPORTERREFNO { get; set; }
        public string OTHERREFNO { get; set; }
        public string CONSIGNEE { get; set; }
        public string NOTIFYPARTIES { get; set; }
        public string LOADINGPORTID { get; set; }
        public string LOADINGPORTNAME { get; set; }
        public string DISCHARGEPORTID { get; set; }
        public string DISCHARGEPORTNAME { get; set; }
        public string FINALDESTINATION { get; set; }
        public string SHIPPINGBILL { get; set; }
        public string CONTAINERNO { get; set; }
        public string LCNO { get; set; }
        public string LCBANKNO { get; set; }
        public string LCDATE { get; set; }
        public string COUNTRYID { get; set; }
        public string COUNTRYNAME { get; set; }
        public string SHIPPINGDATE { get; set; }
        public string VOYNO { get; set; }
        public string TOTALPCS { get; set; }
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string BRANCHNAME { get; set; }
        public string BANKADDRESS { get; set; }
        public string IFSCODE { get; set; }
        public string SWIFTCODE { get; set; }
        public string ACCOUNTNO { get; set; }
        public string SELLINGRATE { get; set; }
        public string CONVERSIONRATE { get; set; }
        public string ISVERIFIED { get; set; }
        public string ERATETAG { get; set; }
        public string AMOUNTINWORD { get; set; }
        public string DELIVERYTO { get; set; }
    }

    public class ExportDetailsEdit
    {
        public string SALEINVOICEAUTOID { get; set; }
        public string SALEORDERID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string SALEORDERDATE { get; set; }
        public string PROFORMAINVOICEID { get; set; }
        public string PROFORMAINVOICEDATE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string HSNCODE { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string QTY { get; set; }
        public string QTYPCS { get; set; }
        public string BATCHNO { get; set; }
        public string AMOUNT { get; set; }
        public string TOTMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string TAG { get; set; }
        public string AMOUNTINR { get; set; }
        public string NSR { get; set; }
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
    }

    public class ExportTaxcountEdit
    {
        public string TAXID { get; set; }
        public string NAME { get; set; }
        public string RELATEDTO { get; set; }
    }

    public class ExportTaxEdit
    {
        public string SALEORDERID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string TAXID { get; set; }
        public string TAXNAME { get; set; }
        public decimal TAXPERCENTAGE { get; set; }
        public decimal TAXVALUE { get; set; }
        public string PRIMARYPRODUCTID { get; set; }
        public string PRIMARYPRODUCTBATCHNO { get; set; }
        public string TAG { get; set; }
        public decimal MRP { get; set; }
    }

    public class ExportFooterEdit
    {
        public decimal TOTALSALEINVOICEVALUE { get; set; }
        public decimal OTHERCHARGESVALUE { get; set; }
        public decimal ADJUSTMENTVALUE { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
        public decimal TOTALSPECIALDISCVALUE { get; set; }
        public string REBETSCHEMEID { get; set; }
        public decimal GROSSREBATEPERCENTAGE { get; set; }
        public decimal GROSSREBATEVALUE { get; set; }
        public decimal ADDFRETCHARGEPERCENTAGE { get; set; }
        public decimal ADDFRETCHARGEVALUE { get; set; }
        public decimal ADDSSMARGINPERCENTAGE { get; set; }
        public decimal ADDSSMARGINAMT { get; set; }
        public decimal ACTUALTOTCASE { get; set; }
        public string TOTALGROSSWT { get; set; }
        public decimal TOTALTAXAMT { get; set; }
        public decimal BASICAMOUNT { get; set; }
        public decimal GROSSAMOUNT { get; set; }
    }

    public class ExportFreeEdit
    {
        public string SALEINVOICEPRIMARYQTYSCHEMEAUTOID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string SALEORDERID { get; set; }
        public string SCHEMEID { get; set; }
        public string SCHEME_PRODUCT_ID { get; set; }
        public string SCHEME_PRODUCT_NAME { get; set; }
        public string QTY { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKSIZEID { get; set; }
        public string PACKSIZENAME { get; set; }
        public string SCHEME_QTY_PCS { get; set; }
        public string SCHEME_QTY_CASE { get; set; }
        public string MRP { get; set; }
        public string BRATE { get; set; }
        public string AMOUNT { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string BATCHNO { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string NSR { get; set; }
        public string RATEDISC { get; set; }
        public string SCHEME_PRODUCT_BATCHNO { get; set; }
        public string STORELOCATIONID { get; set; }
        public string HSNCODE { get; set; }
    }

    public class ExportOrderHeaderEdit
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }

    }

    public class ExportProformaHeaderEdit
    {
        public string PROFORMAINVOICEID { get; set; }
        public string PROFORMANO { get; set; }
        public string PROFORMAVALUE { get; set; }

    }

    public class ExportEdit
    {
        public List<ExportHeaderEdit> ExportHeaderEdit { get; set; }
        public List<ExportDetailsEdit> ExportDetailsEdit { get; set; }
        public List<ExportTaxcountEdit> ExportTaxcountEdit { get; set; }
        public List<ExportTaxEdit> ExportTaxEdit { get; set; }
        public List<ExportFooterEdit> ExportFooterEdit { get; set; }
        public List<ExportFreeEdit> ExportFreeEdit { get; set; }
        public List<ExportOrderHeaderEdit> ExportOrderHeaderEdit { get; set; }
        public List<ExportProformaHeaderEdit> ExportProformaHeaderEdit { get; set; }
    }

    public class PackingListDetails
    {
        public string SALEINVOICEID { get; set; }
        public string SALEORDERID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string QTY { get; set; }
        public string BATCHNO { get; set; }
        public string STARTPOSITION { get; set; }
        public string ENDPOSITION { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string NETWEIGHT { get; set; }
        public string CATNAME { get; set; }
        public string DESCPACKAGES { get; set; }
    }
    public class CustomerNetSaleAmt
    {
        public string NET_SALE { get; set; }

    }
}
