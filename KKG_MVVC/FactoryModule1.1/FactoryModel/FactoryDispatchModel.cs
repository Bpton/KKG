using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
    public class FactoryDispatchModel
    {
        public string FLAG { get; set; }
        public string DESPATCHID { get; set; }
        public string InvoiceType { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string TODEPOTID { get; set; }
        public string BRPREFIX { get; set; }
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
        public string DispatchDate { get; set; }
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
        public string StockQty { get; set; }
        public string DispatchQty { get; set; }
        public string BasicAmt { get; set; }
        public string TaxAmt { get; set; }
        public string GrossAmt1 { get; set; }
        public string GrossAmt { get; set; }
        public string RoundOff { get; set; }
        public string NetAmt { get; set; }
        public string TotalCase { get; set; }
        public string TotalPcs { get; set; }
        public string Remarks { get; set; }
        public string TransferNo { get; set; }
        public string DispatchMode { get; set; }
        public List<DispatchDetailsFG> DispatchDetailsFG { get; set; }
        
    }

    public class SourceDepot
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }

    public class DestinationDepot
    {
        public string TODEPOTID { get; set; }
        public string BRPREFIX { get; set; }
    }

    public class Depot
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }


    public class Waybill
    {
        public string WAYBILLNO { get; set; }
        public string WAYBILLID { get; set; }
    }

    public class InsuranceCompany
    {
        public string ID { get; set; }
        public string COMPANY_NAME { get; set; }
    }

    public class PolicyNo
    {
        public string INSURANCE_NO { get; set; }
        public string NO { get; set; }
    }
    public class Transporter
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }

    public class TransporterList
    {
        public string TransporterID { get; set; }
        public string TransporterNAME { get; set; }
    }

    public class ShippingAddressList
    {
        public string hdnTransporterID { get; set; }
        public string hdnTransporterNAME { get; set; }
        public string ShippingAddress { get; set; }

    }

    public class ShippingAddress
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string SHIPPINGADDRESS { get; set; }
        
    }

    public class Category
    {
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string HSN { get; set; }

    }

    public class CategoryRM
    {
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string HSN { get; set; }

    }

    public class TransitDays
    {
        public string TRANSIT_DAYS { get; set; }
    }

    public class ProductList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string SEQUENCENO { get; set; }
        public string CATNAME { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
    }

    public class PacksizeList
    {
        public string PSID { get; set; }
        public string PSNAME { get; set; }
        public string RANK { get; set; }
    }

    public class Taxcount
    {
        public string TAXCOUNT { get; set; }
        public string NAME { get; set; }
        public string PERCENTAGE { get; set; }
        public string RELATEDTO { get; set; }
    }
    public class TaxcountList
    {
        public string TAXCOUNT { get; set; }
        public string TAXNAME { get; set; }
        public string TAXPERCENTAGE { get; set; }
        public string TAXRELATEDTO { get; set; }
    }

    public class BatchInfo
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
        public string STOCKLOCATION { get; set; }
    }

    public class BatchInfoList
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
        public string BatchSTOCKLOCATION { get; set; }
    }

    public class BatchInfoRM
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
        public string STOCKLOCATION { get; set; }
    }

    public class BatchInfoListRM
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
        public string BatchSTOCKLOCATION { get; set; }
    }

    public class StockTransferRate
    {
        public string RATE { get; set; }
    }
    public class StockTransferRateList
    {
        public string hdnRATE { get; set; }
    }

    public class StockTransferRateRM
    {
        public string RATE { get; set; }
    }
    public class StockTransferRateListRM
    {
        public string hdnRATE { get; set; }
    }

    public class Assesment
    {
        public string ASSESMENT { get; set; }
    }

    public class PcsQty
    {
        public string QTYINPCS { get; set; }
    }

    public class Netweight
    {
        public string NETWEIGHT { get; set; }
    }

    public class Grossweight
    {
        public string GROSSWEIGHT { get; set; }
    }

    public class HSN
    {
        public string HSNCODE { get; set; }
    }

    public class HSNTaxPercentage
    {
        public string HSNTAX { get; set; }
    }

    public class HSNTaxID
    {
        public string TAXID { get; set; }
    }

    public class CalculateAmtInPcsFAC
    {
        public List<Assesment> assesments { get; set; }
        public List<PcsQty> pcsqty { get; set; }
        public List<Netweight> netweight { get; set; }
        public List<Grossweight> grossweight { get; set; }
        public List<HSN> hsn { get; set; }
        public List<HSNTaxPercentage> hsnTax { get; set; }
        public List<HSNTaxID> hsnTaxID { get; set; }
    }

    public class TotalPcs
    {
        public string PCSQuantity { get; set; }
    }
    public class TotalCase
    {
        public string CASEQuantity { get; set; }
    }

    public class TaxOnEdit
    {
        public decimal TAXPERCENTAGE { get; set; }
    }

    public class QuantityDetails
    {
        public List<TotalPcs> pcsquantity { get; set; }
        public List<TotalCase> casequantity { get; set; }
    }

    public class DispatchDetailsFG
    {

        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public decimal MRP { get; set; }
        public decimal QTY { get; set; }
        public decimal RATE { get; set; }
        public string BATCHNO { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal TOTMRP { get; set; }
        public decimal ASSESMENTPERCENTAGE { get; set; }
        public decimal TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public decimal ALLOCATEDQTY { get; set; }
        public string REASON { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string ALLOCATIONID { get; set; }
        public string ALLOCATIONNO { get; set; }
        public string STORELOCATIONID { get; set; }


    }

    public class DispatchedFGList
    {
        public string STOCKTRANSFERID { get; set; }
        public string STOCKTRANSFERNO { get; set; }
        public string STOCKTRANSFERDATE { get; set; }
        public string WAYBILLNO { get; set; }
        public string WAYBILLKEY { get; set; }
        public string CFORMDATE { get; set; }
        public string CFORMNO { get; set; }
        public string FORMFLAG { get; set; }
        public string INVOICENO { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string MOTHERDEPOTNAME { get; set; }
        public string TODEPOTID { get; set; }
        public string TODEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string TOTALCASE { get; set; }
        public string TOTALPCS { get; set; }
        public string NEXTLEVELID { get; set; }
        public string ISVERIFIED { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string NETAMOUNT { get; set; }
        public string USERNAME { get; set; }

    }

    public class DispatchHeaderEditFG
    {
        public string STOCKDESPATCHID { get; set; }
        public string STOCKTRANSFERNO { get; set; }
        public string STOCKTRANSFERDATE { get; set; }
        public string WAYBILLNO { get; set; }
        public string WAYBILLKEY { get; set; }
        public string CHALLANNO { get; set; }
        public string CHALLANDATE { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string TODEPOTID { get; set; }
        public string TODEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string TRANSPORTERID { get; set; }
        public string REMARKS { get; set; }
        public string CFORMNO { get; set; }
        public string CFORMDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string GATEPASSDATE { get; set; }
        public string FORMFLAG { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string INSURANCE_NO { get; set; }
        public string TOTALCASE { get; set; }
        public string TOTALPCS { get; set; }
        public string COUNTRYID { get; set; }
        public string COUNTRYNAME { get; set; }
        public string NOTE { get; set; }
        public string WAYBILLDATE { get; set; }
        public string SHIPPINGADDRESS { get; set; }
        public string DELIVERYDATE { get; set; }
        public string EXPORT { get; set; }
    }

    public class DispatchDetailsEditFG
    {
        public string STOCKDESPATCHAUTOID { get; set; }
        public string POID { get; set; }
        public string PODATE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string HSNCODE { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string MRP { get; set; }
        public string TRANSFERQTY { get; set; }
        public string RATE { get; set; }
        public string BATCHNO { get; set; }
        public string AMOUNT { get; set; }
        public string ALLOCATIONID { get; set; }
        public string ALLOCATIONNO { get; set; }
        public string TOTALMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string TOTALASSESMENTVALUE { get; set; }
        public string NETWEIGHT { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string ALLOCATEDQTY { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string TAG { get; set; }
        public string BEFOREEDITEDQTY { get; set; }
    }

    public class DispatchTaxcountEditFG
    {
        public string TAXID { get; set; }
        public string NAME { get; set; }
        public string RELATEDTO { get; set; }
    }
    
    public class DispatchTaxEditFG
    {
        public string STOCKDESPATCHID { get; set; }
        public string TAXID { get; set; }
        public decimal PERCENTAGE { get; set; }
        public string SALEORDERID { get; set; }
        public string PRODUCTID { get; set; }
        public string BATCHNO { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal TAXVALUE { get; set; }
        public string NAME { get; set; }
        public decimal MRP { get; set; }
    }

    public class DispatchFooterEditFG
    {
        public decimal TOTALDESPATCHVALUE { get; set; }
        public decimal OTHERCHARGESVALUE { get; set; }
        public decimal ADJUSTMENTVALUE { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
        public decimal TOTALTAXAMT { get; set; }

    }

    public class DispatchEditFG
    {
        public List<DispatchHeaderEditFG> DispatchHeaderEditFG { get; set; }
        public List<DispatchDetailsEditFG> DispatchDetailsEditFG { get; set; }
        public List<DispatchTaxcountEditFG> DispatchTaxcountEditFG { get; set; }
        public List<DispatchTaxEditFG> DispatchTaxEditFG { get; set; }
        public List<DispatchFooterEditFG> DispatchFooterEditFG { get; set; }
    }

    public class ParamLedger
    {
        public string STKTRANSFER_ACC_LEADGER { get; set; }
        public string ROUNDOFF_ACC_LEADGER { get; set; }
        public string BRKG_DAMG_SHRTG_ACC_LEADGER { get; set; }
        public string INSURANCECLAIM_ACC_LEADGER { get; set; }
    }
    public class CorporateOfficeID
    {
        public string BRID { get; set; }
    }
    public class ReferenceLedgerID
    {
        public string REFERENCELEDGERID { get; set; }
    }
    public class ReferenceLedgerName
    {
        public string BRNAME { get; set; }
    }

    public class AccountPostingDispatchDetails
    {
        public string STOCKDESPATCHDATE { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string TPUID { get; set; }
        public string TPUNAME { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string INSURANCENO { get; set; }
        public decimal TOTALDESPATCHVALUE { get; set; }
        public string TRANSPORTERID { get; set; }
        public string STOCKDESPATCHID { get; set; }
        public string INVOICENO { get; set; }
        public string MOTHERDEPOTNAME { get; set; }
        public string TAXID { get; set; }
        public decimal TAXVALUE { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
    }
    public class BackDateChecking
    {
        public string BACKDATEFLAG { get; set; }
    }

    public class EntryLockChecking
    {
        public string LOCK_FLAG { get; set; }
    }
}
