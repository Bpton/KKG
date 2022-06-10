using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
    public class TpuFGInvoiceModel
    {
        public string FLAG { get; set; }
        public string FGInvoiceID { get; set; }
        public string InvoiceType { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string SearchDepotID { get; set; }
        public string SearchDepotName { get; set; }
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
        public string ICDSNo { get; set; }
        public string ICDSDate { get; set; }
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
        public string OrderDate { get; set; }
        public string OrderQty { get; set; }
        public string DeliveredQty { get; set; }
        public string RemainingQty { get; set; }
        public string StockQty { get; set; }
        public string InvoiceQty { get; set; }
        public string InvoicePcs { get; set; }
        public string ConversionQty { get; set; }
        public string BasicAmt { get; set; }
        public string TaxAmt { get; set; }
        public string GrossAmt1 { get; set; }
        public string GrossAmt { get; set; }
        public string RoundOff { get; set; }
        public string ProformaAmount { get; set; }
        public string AdjustmentAmount { get; set; }
        public string NetAmt { get; set; }
        public string NetAmt2 { get; set; }
        public string TotalCase { get; set; }
        public string TotalPcs { get; set; }
        public string Remarks { get; set; }
        public string CheckerRemarks { get; set; }
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
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string ClosingBalance { get; set; }
        public string CreditLimit { get; set; }
        public string ReferencePO { get; set; }
        public string Target { get; set; }
        public string Balance { get; set; }
        public string AchPercentage { get; set; }
        public string InvoiceLimit { get; set; }
        public string InvoiceDone { get; set; }
        public string InvoiceBalance { get; set; }
        public string TotalSchemeAmt { get; set; }
        public string TotalDiscountAmt { get; set; }
        public string TotalFreeAmt { get; set; }
        public string TotalGrossWght { get; set; }
        public string SSMargin { get; set; }
        public string SSMarginAmt { get; set; }
        public string FREEPRODUCTID { get; set; }
        public string FREEPRODUCTNAME { get; set; }
        public string SchemeQuantity { get; set; }
        public string MonthID { get; set; }
        public string GTFreeTag { get; set; }
        public string CSDComboTag { get; set; }
        public string UserID { get; set; }
        public string FinYear { get; set; }
        public string ComboProductID { get; set; }
        public string ComboProductName { get; set; }
        public string ConversionTypeID { get; set; }
        public string ConversionType { get; set; }
        public string SingleProductName { get; set; }
        public string TotalStockQty { get; set; }
        public string TotalConversionQty { get; set; }
        public string AdjustmentType { get; set; }
        public string AdjustmentMenu { get; set; }
        public string ComboProduct { get; set; }
        public string ComboBatch { get; set; }
        public string ComboMfg { get; set; }
        public string ComboExpr { get; set; }
        public string ComboMrp { get; set; }
        public string StorelocationID { get; set; }
        public string StorelocationName { get; set; }
        public string JournalQuantity { get; set; }
        public string ReasonID { get; set; }
        public string ReasonName { get; set; }
        public string BatchFromDate { get; set; }
        public string BatchToDate { get; set; }

        public string ToStorelocationID { get; set; }

        public string ToStorelocationName { get; set; }

        public string NetSaleAmt { get; set; }
        public string TCSPercent { get; set; }
        public string TCSApplicable { get; set; }
        public string TCSAmt { get; set; }
        public string TCSNetAmt { get; set; }
        public List<InvoiceDetailsGT> InvoiceDetailsGT { get; set; }
        public List<FreeDetailsGT> FreeDetailsGT { get; set; }
        public List<ComboDetailsCSD> ComboDetailsCSD { get; set; }
        public List<StockAdjustmentDetails> StockAdjustmentDetails { get; set; }

    }

    public class TpuStorelocationList
    {
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
    }

    public class TpuReasonList
    {
        public string REASONID { get; set; }
        public string REASONNAME { get; set; }
    }

    public class TpuStockJournalPacksizeList
    {
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZENAME_FROM { get; set; }
    }

    public class TpuStockJournalCategoryList
    {
        public string CATID { get; set; }
        public string CATCODE { get; set; }
        public string SEQUENCENO { get; set; }
        public string CATNAME { get; set; }
    }

    public class TpuStockJournalProductList
    {
        public string MOTHERDEPOTID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string SEQUENCENO { get; set; }
        public string CATNAME { get; set; }
        public string DIVNAME { get; set; }
    }

    public class TpuInterStoreLocationProductList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string SEQUENCENO { get; set; }
        public string CATNAME { get; set; }
        public string DIVNAME { get; set; }
        public string PRODUCTALIAS { get; set; }
    }

    public class TpuStockJournalProductType
    {
        public string TYPE { get; set; }
    }

    public class TpuGTCustomerList
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }
    
    public class TpuShipplingAddress
    {
        public string ADDRESS { get; set; }
    }

    public class TpuInvoiceTaxcount
    {
        public string TAXCOUNT { get; set; }
        public string NAME { get; set; }
        public string PERCENTAGE { get; set; }
        public string RELATEDTO { get; set; }
    }
    public class TpuInvoiceTaxcountList
    {
        public string TAXCOUNT { get; set; }
        public string TAXNAME { get; set; }
        public string TAXPERCENTAGE { get; set; }
        public string TAXRELATEDTO { get; set; }
    }

    public class TpuOrderDetailsList
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

    public class TpuInvoiceDetails
    {

        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public decimal MRP { get; set; }
        public decimal QTY { get; set; }
        public decimal QTYPCS { get; set; }
        public decimal RATE { get; set; }
        public string BATCHNO { get; set; }
        public decimal AMOUNT { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public decimal NSR { get; set; }
        public decimal RATEDISC { get; set; }
        public decimal DISCVALUE { get; set; }
        public string QSH { get; set; }
        public string QSGUID { get; set; }
        public decimal DISCPER { get; set; }
        public decimal DISCAMT { get; set; }
        public string PRICESCHEMEID { get; set; }
        public decimal PERCENTAGE { get; set; }
        public decimal VALUE { get; set; }
    }

   

    

    

    public class TpuInvoiceConvertedQty
    {
        public string CONVERTED_QTY { get; set; }
    }

    public class TpuInvoiceCaseQuantity
    {
        public string QTYINCASE { get; set; }
    }

    public class TpuInvoiceNetweight
    {
        public string NETWEIGHT { get; set; }
    }

    public class TpuInoiceCGSTPercentage
    {
        public string CGSTTAX { get; set; }
    }

    public class TpuInvoiceCGST
    {
        public string CGSTID { get; set; }
    }

    public class TpuInvoiceSGSTPercentage
    {
        public string SGSTTAX { get; set; }
    }

    public class TpuInvoiceSGST
    {
        public string SGSTID { get; set; }
    }

    public class TpuInvoiceIGSTPercentage
    {
        public string IGSTTAX { get; set; }
    }

    public class TpuInvoiceIGST
    {
        public string IGSTID { get; set; }
    }

    public class TpuTaxDetailsDepot
    {
        public List<GTInvoiceCaseQuantity> casequantity { get; set; }
        public List<GTInvoiceNetweight> netweight { get; set; }
        public List<GTInoiceCGSTPercentage> cgstpercentage { get; set; }
        public List<GTInvoiceCGST> cgst { get; set; }
        public List<GTInvoiceSGSTPercentage> sgstpercentage { get; set; }
        public List<GTInvoiceSGST> sgst { get; set; }
        public List<GTInvoiceIGSTPercentage> igstpercentage { get; set; }
        public List<GTInvoiceIGST> igst { get; set; }

    }

   

    public class TpuInvoiceList
    {
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string WAYBILLNO { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string MODEOFTRANSPORT { get; set; }
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
        public string APPROVAL_PERSON { get; set; }
        public string DAYENDTAG { get; set; }
        public string USERNAME { get; set; }
    }

    

    

    public class TpuInvoiceHeaderEdit
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
        public string FORMFLAG { get; set; }
        public string INVOICETYPEID { get; set; }
        public string FORMREQUIRED { get; set; }
        public string NOTE { get; set; }
        public string TOTALPCS { get; set; }
        public string ISCHALLAN { get; set; }
        public string OTHERREFNO { get; set; }
        public string DISTNAME { get; set; }
        public string RETNAME { get; set; }
        public string ISCLAIM { get; set; }
        public string CLAIMNO { get; set; }
    }

    public class TpuInvoiceDetailsEdit
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
        public string BCP { get; set; }
        public string QTY { get; set; }
        public string QTYPCS { get; set; }
        public string BATCHNO { get; set; }
        public string AMOUNT { get; set; }
        public string TOTMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string PRIMARYPRICESCHEMEID { get; set; }
        public string PERCENTAGE { get; set; }
        public string VALUE { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string RATEDISC { get; set; }
        public string NSR { get; set; }
        public string RATEDISCVALUE { get; set; }
        public string RATEPERCASE { get; set; }
        public string DISCPER { get; set; }
        public string DISCAMT { get; set; }
        public string QSH { get; set; }
        public string QSGUID { get; set; }
    }

    public class TpuInvoiceTaxcountEdit
    {
        public string TAXID { get; set; }
        public string NAME { get; set; }
        public string RELATEDTO { get; set; }
    }

    public class TpuInvoiceFooterEdit
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

    public class TpuInvoiceTaxEdit
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

    public class TpuInvoiceOrderDetailsEdit
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string ORDERQTY { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZEName { get; set; }
    }

    public class TpuInvoiceOrderHeaderEdit
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
        public string REFERENCESALEORDERNO { get; set; }
        public string REFSALEORDERNO { get; set; }

    }


    public class TpuInvoiceEdit
    {
        public List<InvoiceHeaderEditGT> InvoiceHeaderEditGT { get; set; }
        public List<InvoiceDetailsEditGT> InvoiceDetailsEditGT { get; set; }
        public List<InvoiceTaxcountEditGT> InvoiceTaxcountEditGT { get; set; }
        public List<InvoiceFooterEditGT> InvoiceFooterEditGT { get; set; }
        public List<InvoiceTaxEditGT> InvoiceTaxEditGT { get; set; }
        public List<InvoicePriceSchemeEditGT> InvoicePriceSchemeEditGT { get; set; }
        public List<InvoiceQuantitySchemeEditGT> InvoiceQuantitySchemeEditGT { get; set; }
        public List<InvoiceProductDetailsEditGT> InvoiceProductDetailsEditGT { get; set; }
        public List<InvoiceOrderDetailsEditGT> InvoiceOrderDetailsEditGT { get; set; }
        public List<InvoiceOrderHeaderEditGT> InvoiceOrderHeaderEditGT { get; set; }
    }

    
    

   

   
    

    

    public class TpuProductList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string SEQUENCENO { get; set; }
        public string DIVNAME { get; set; }
        public string UNITVALUE { get; set; }
        public string CATID { get; set; }
    }

    public class TpuPacksizeList
    {
        public string PSID { get; set; }
        public string PSNAME { get; set; }
    }

    

    public class TpuBatchInfoList
    {
        public string BatchDepotID { get; set; }
        public string BatchPRODUCTID { get; set; }
        public string BatchPRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string BatchMRP { get; set; }
        public string BatchSTOCKQTY { get; set; }
        public string BatchASSESMENTPERCENTAGE { get; set; }
        public string BatchMFGDATE { get; set; }
        public string BatchEXPIRDATE { get; set; }
        public string BatchSTORELOCATION { get; set; }
    }

    public class TpuBatchInfo
    {
        public string DEPOTID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public string MRP { get; set; }
        public string INVOICESTOCKQTY { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string MFGDATE { get; set; }
        public string EXPIRDATE { get; set; }
        public string STORELOCATIONID { get; set; }
    }

    public class TpuInvoiceRate
    {
        public string BASECOSTPRICE { get; set; }
    }

    public class TpuQtyInPcs
    {
        public string FINAL_DELIVEREDQTY { get; set; }
        public string DELIVEREDQTY { get; set; }
        public string STOCKQTY { get; set; }
    }

    public class TpuCaseToPCSConversion
    {
        public string PCS_QTY { get; set; }
    }


    public class TpuProductType
    {
        public string TYPE { get; set; }
    }

    public class TpuHSNCode
    {
        public string HSNCODE { get; set; }
    }
    

    public class TpuInvoiceTaxOnEdit
    {
        public decimal TAXPERCENTAGE { get; set; }
    }

    public class TpuSaleorderList
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
    }

    public class TpuOrdervsDispatchList
    {
        public string ORDERQTY { get; set; }
        public string DESPATCHQTY { get; set; }
        public string DELIVERYQTY { get; set; }
        public string ORDERQTYPCS { get; set; }
        public string DESPATCHQTYPCS { get; set; }
        public string DELIVERYQTYPCS { get; set; }
    }

    public class TpuQuantityInPcs
    {
        public string FINAL_DELIVEREDQTY { get; set; }
        public string DELIVEREDQTY { get; set; }
        public string STOCKQTY { get; set; }
    }

    public class TpuList
    { 
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }






















}
