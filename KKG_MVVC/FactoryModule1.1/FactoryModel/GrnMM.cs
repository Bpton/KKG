using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class GrnMM
    {
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public string RECEIVEDID { get; set; }
        public string DESPATCHID { get; set; }
        public string DESPATCHNO { get; set; }
        public string DESPATCHDATE { get; set; }
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string VEHICLENO { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string WAYBILLKEY { get; set; }
        public string INVOICENO { get; set; }
        public string INVOICEDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string GATEPASSDATE { get; set; }
        public string POID { get; set; }
        public string PONO { get; set; }
        public string MATERIALID { get; set; }
        public string MATERIALNAME { get; set; }
        public string BATCHNO { get; set; }
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZENAME_FROM { get; set; }
        public string MFGDATE { get; set; }
        public string EXPDATE { get; set; }
        public decimal RATE { get; set; }
        public decimal MRP { get; set; }
        public decimal POQTY { get; set; }
        public decimal RECEIVEDQTY { get; set; }
        public decimal REMAININGQTY { get; set; }
        public decimal RECEIVE_QTY { get; set; }
        public decimal FREIGHTCHARGES { get; set; }
        public decimal ADDITIONALCOST { get; set; }
        public decimal BASICVALUE { get; set; }
        public decimal MRPVALUE { get; set; }
        public decimal TAXVALUE { get; set; }
        public decimal BASICWITHTAX { get; set; }
        public decimal GROSSAMOUNT { get; set; }
        public decimal ROUNDOFF { get; set; }
        public decimal OTHCHARGEAMT { get; set; }
        public decimal NETAMT { get; set; }
        public decimal ISSUEQTY { get; set; }
        public decimal DESPATCHQTY { get; set; }
        public string REMARKS { get; set; }
        public string UNITNAME { get; set; }
        public string UNITID { get; set; }
        public string PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ISWORKORDER { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string Mode { get; set; }
        public string Createdby { get; set; }
        public string FINYEAR { get; set; }
        public string ENTRYFROM { get; set; }
        public string MODULEID { get; set; }
        public int INVOICETYPE { get; set; }
        public string ISVERIFIEDCHECKER1 { get; set; }
        public string ISVERIFIEDSTOCKIN { get; set; }
        public decimal TOTALITEMWISEFREIGHT { get; set; }
        public decimal TOTALITEMWISEADDCOST { get; set; }
        public decimal TOTALITEMWISEDISCOUNT { get; set; }
        public string LEDGERID { get; set; }
        public string WAYBILLDT { get; set; }
        public string CAPACITYUPLOAD { get; set; }
        public string VENDORFROM { get; set; }
       public string FILENAME { get; set; }
       public string STATUS { get; set; }
        public List<GrnOrderdetails> grnOrderdetails { get; set; }


    }
    public class TaxcountMM
    {
        public string TAXCOUNT { get; set; }
        public string NAME { get; set; }
        public string PERCENTAGE { get; set; }
        public string RELATEDTO { get; set; }
    }
    public class TaxcountListMM
    {
        public string TAXCOUNT { get; set; }
        public string TAXNAME { get; set; }
        public string TAXPERCENTAGE { get; set; }
        public string TAXRELATEDTO { get; set; }
    }

    public class ProductInfoGRNMM
    {
        public string PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string UNITID { get; set; }
        public string UNITNAME { get; set; }
        public decimal POQTY { get; set; }
        public decimal DESPATCHQTY { get; set; }
        public decimal REMAININGQTY { get; set; }
        public decimal MRP { get; set; }
        public decimal RATE { get; set; }
    }

    public class ProductInfo
    {
        public string PO_QTY { get; set; }
        public decimal RATE { get; set; }
        public decimal MRP { get; set; }
        public decimal ASSESSABLEPERCENT { get; set; }
        public decimal TOTAL_ASSESMENT { get; set; }
        public string WEIGHT { get; set; }
        public decimal DEPOTWISE_DESPATCH_QTY { get; set; }
        public decimal DESPATCH_QTY { get; set; }
        public string PODATE { get; set; }
        public string HSE { get; set; }
        public string PRODUCTNAME { get; set; }
        public string CATID { get; set; }
    }
    public class ProductInfoBatch
    {
        public string PO_QTY { get; set; }
        public decimal RATE { get; set; }
        public decimal MRP { get; set; }
        public decimal ASSESSABLEPERCENT { get; set; }
        public decimal TOTAL_ASSESMENT { get; set; }
        public string WEIGHT { get; set; }
        public decimal DEPOTWISE_DESPATCH_QTY { get; set; }
        public decimal DESPATCH_QTY { get; set; }
        public string PODATE { get; set; }
        public string HSE { get; set; }
        public string PRODUCTNAME { get; set; }
        public string CATID { get; set; }
    }

    public class MASTERBATCH
    {
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
    }
    public class PRODUCT_GROSSWEIGHT
    {
        public string GROSSWEIGHT { get; set; }

    }
    public class PRODUCT_RETURNAMOUNT
    {
        public decimal RETURNAMOUNT { get; set; }

    }
    public class PRODUCT_AMOUNT
    {
        public decimal AMOUNT { get; set; }
    }
    public class PRODUCT_DISCOUNT
    {
        public decimal DISCOUNTPER { get; set; }
        public decimal DISCOUNTAMT { get; set; }
    }
    public class PRODUCT_NETGROSSWEIGHT
    {
        public string NETWEIGHT { get; set; }
        public string GROSSWEIGHT { get; set; }
    }
    public class CalcualteTaxWithAmount
    {
        public List<ProductInfoBatch> productInfoBatches { get; set; }
        public List<MASTERBATCH> masterbatch { get; set; }
        public List<PRODUCT_RETURNAMOUNT> product_returnamount { get; set; }
        public List<PRODUCT_AMOUNT> product_amount { get; set; }
        public List<PRODUCT_DISCOUNT> product_discount { get; set; }
        public List<PRODUCT_NETGROSSWEIGHT> product_netgrossweight { get; set; }
        public List<PRODUCT_GROSSWEIGHT> product_grossweight { get; set; }
        public List<CGSTPercentagemm> cgstpercentagemm { get; set; }
        public List<CGSTmm> cgstmm { get; set; }
        public List<SGSTPercentagemm> sgstpercentagemm { get; set; }
        public List<SGSTmm> sgstmm { get; set; }
        public List<IGSTPercentagemm> igstpercentagemm { get; set; }
        public List<IGSTmm> igstmm { get; set; }
    }
    public class GrnOrderdetails
    {
        public string POID { get; set; }
        public string PRODUCTID { get; set; }
        public string PODATE { get; set; }
        public string PONO { get; set; }
        public decimal POQTY { get; set; }
        public string PACKSIZEID { get; set; }
        public string PACKSIZENAME { get; set; }
        public string HSNCODE { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal RECEIVEDQTY { get; set; }
        public decimal RATE { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal DISCOUNTPER { get; set; }
        public decimal DISCOUNTAMT { get; set; }
        public decimal AFTERDISCOUNTAMT { get; set; }
        public decimal ITEMWISEFREIGHT { get; set; }
        public decimal AFTERITEMWISEFREIGHTAMT { get; set; }
        public decimal ITEMWISEADDCOST { get; set; }
        public decimal AFTERITEMWISEADDCOSTAMT { get; set; }
        public string BATCHNO { get; set; }
        public string NETWEIGHT { get; set; }
        public string mastermfgdate { get; set; }
        public string masterexpdate { get; set; }
        public string GROSSWEIGHT { get; set; }
        public decimal DEPOTRATE { get; set; }
        public decimal MRP { get; set; }
        public decimal REMAININGQTY { get; set; }
        public decimal TOTMRP { get; set; }
        public decimal ASSESMENTPERCENTAGE { get; set; }
        public decimal TOTALASSESMENTVALUE { get; set; }
        public decimal DESPATCHQTY { get; set; }
        public string WEIGHT { get; set; }
        public decimal ALLOCATEDQTY { get; set; }
        public decimal BEFOREEXCHANGEAMT { get; set; }


    }
    public class CGSTPercentagemm
    {
        public decimal CGSTTAX { get; set; }
    }

    public class CGSTmm
    {
        public string CGSTID { get; set; }
    }

    public class SGSTPercentagemm
    {
        public decimal SGSTTAX { get; set; }
    }

    public class SGSTmm
    {
        public string SGSTID { get; set; }
    }
    public class IGSTPercentagemm
    {
        public decimal IGSTTAX { get; set; }
    }

    public class IGSTmm
    {
        public string IGSTID { get; set; }
    }

    public class GrnEditMM
    {
        public List<GRN_EDIT_STOCKRECEIVEDHEADER> gRN_EDIT_STOCKRECEIVEDHEADERs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDDETAILS> gRN_EDIT_STOCKRECEIVEDDETAILs { get; set; }
        public List<GRN_EDIT_TAXCOMPONENTCOUNT> gRN_EDIT_TAXCOMPONENTCOUNTs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDFOOTER> gRN_EDIT_STOCKRECEIVEDFOOTERs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDTAX> gRN_EDIT_STOCKRECEIVEDTAXes { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDTERMS> gRN_EDIT_STOCKRECEIVEDTERMs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDITEMWISETAX> gRN_EDIT_STOCKRECEIVEDITEMWISETAXes { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDREJECTIONDETAILS> gRN_EDIT_STOCKRECEIVEDREJECTIONDETAILs { get; set; }
        public List<GRN_EDIT_GRNADDITIONALDETAILS> gRN_EDIT_GRNADDITIONALDETAILs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDREJECTIONTAX> gRN_EDIT_STOCKRECEIVEDREJECTIONTAXes { get; set; }
        public List<GRN_EDIT_JOBORDERRECEIVEDDETAILS> gRN_EDIT_JOBORDERRECEIVEDDETAILs { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDSAMPLEQTY> gRN_EDIT_STOCKRECEIVEDSAMPLEQTies { get; set; }
        public List<GRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAME> gRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAMEs { get; set; }

    }

    public class GRN_EDIT_STOCKRECEIVEDHEADER
    {
        public string STOCKDESPATCHID { get; set; }
        public string STOCKRECEIVEDID { get; set; }
        public string STOCKRECEIVEDNO { get; set; }
        public string STOCKRECEIVEDDATE { get; set; }
        public string DESPATCHDATE { get; set; }
        public string TPUID { get; set; }
        public string TPUNAME { get; set; }
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
        public string WAYBILLNO { get; set; }
        public string WAYBILLKEY { get; set; }
        public string INVOICENO { get; set; }
        public string INVOICEDATE { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string MOTHERDEPOTNAME { get; set; }
        public string FINYEAR { get; set; }
        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string CFORMNO { get; set; }
        public string CFORMDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string GATEPASSDATE { get; set; }
        public string REMARKS { get; set; }
        public string EXPORT { get; set; }
        public string NOTE { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string INSURANCENUMBER { get; set; }
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
        public decimal TOTALCASE { get; set; }
        public decimal TOTALPCS { get; set; }
        public string QCREMARKS { get; set; }
        public string LEDGERID { get; set; }
        public string WAYBILLDATE { get; set; }
        public string DELIVERYDATE { get; set; }
        public string CAPACITYUPLOAD { get; set; }
        public string VENDORFROM { get; set; }



    }
    public class GRN_EDIT_STOCKRECEIVEDDETAILS
    {
        public string GUID { get; set; }
        public string STOCKRECEIVEDAUTOID { get; set; }
        public string POID { get; set; }
        public string PODATE { get; set; }
        public string PONO { get; set; }
        public decimal POQTY { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string HSNCODE { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public decimal MRP { get; set; }
        public decimal DESPATCHQTY { get; set; }
        public decimal RECEIVEDQTY { get; set; }
        public decimal REMAININGQTY { get; set; }
        public decimal REASONID { get; set; }
        public decimal REASONNAME { get; set; }
        public decimal INRRATE { get; set; }
        public decimal EXCHANGERATE { get; set; }
        public decimal RATE { get; set; }
        public decimal DEPOTRATE { get; set; }
        public string BATCHNO { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal ASSESMENTPERCENTAGE { get; set; }
        public decimal TOTALASSESABLEVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string GROSSWEIGHT { get; set; }
        public decimal ALLOCATEDQTY { get; set; }
        public decimal TOTMRP { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public decimal DISCOUNTPER { get; set; }
        public decimal DISCOUNTAMT { get; set; }
        public decimal AFTERDISCOUNTAMT { get; set; }
        public decimal ITEMWISEFREIGHT { get; set; }
        public decimal AFTERITEMWISEFREIGHTAMT { get; set; }
        public decimal ITEMWISEADDCOST { get; set; }
        public decimal AFTERITEMWISEADDCOSTAMT { get; set; }
    }
    public class GRN_EDIT_TAXCOMPONENTCOUNT
    {
            public string TAXID { get; set; }
            public string NAME { get; set; }
            public string RELATEDTO { get; set; }
    }
    public class GRN_EDIT_STOCKRECEIVEDFOOTER
    {
           
            public decimal ADJUSTMENTVALUE       {get;set;}
            public decimal ADDITIONALAMOUNT      {get;set;}
            public decimal BEFOREEXCHANGEAMT     { get; set;}
        public decimal BASICVALUE { get; set; }
        public decimal MRPVALUE { get; set; }
        public decimal TAXVALUE { get; set; }
        public decimal BASICWITHTAX { get; set; }
        public decimal GROSSAMOUNT { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
        public decimal OTHERCHARGESVALUE { get; set; }
        public decimal TOTALDESPATCHVALUE { get; set; }



    }
    public class GRN_EDIT_STOCKRECEIVEDTAX
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public decimal PERCENTAGE { get; set; }
        public decimal TAXVALUE { get; set; }
    }
    public class GRN_EDIT_STOCKRECEIVEDTERMS
    {
        public string TERMSID { get; set; }
    }
    public class GRN_EDIT_STOCKRECEIVEDITEMWISETAX
    {
        public string STOCKRECEIVEDID { get; set; }
        public string TAXID { get; set; }
        public decimal PERCENTAGE { get; set; }
        public decimal TAXVALUE { get; set; }
        public string POID { get; set; }
        public string PRODUCTID { get; set; }
        public string BATCHNO { get; set; }
        public string PRODUCTNAME { get; set; }
        public string NAME { get; set; }
        public decimal MRP { get; set; }

    }
    public class GRN_EDIT_STOCKRECEIVEDREJECTIONDETAILS
    {
            public string STOCKRECEIVEDID { get; set; }
            public string POID                { get; set; }
            public string STOCKDESPATCHID     { get; set; }
            public string PRODUCTID           { get; set; }
            public string PRODUCTNAME         { get; set; }
            public string BATCHNO             { get; set; }
            public decimal REJECTIONQTY       { get; set; }
            public string PACKINGSIZEID       { get; set; }
            public string PACKINGSIZENAME     { get; set; }
            public string REASONID            { get; set; }
            public string REASONNAME          { get; set; }
            public decimal DEPOTRATE          { get; set; }
            public decimal DEPOTRATE1         { get; set; }
            public string STORELOCATIONID     { get; set; }
            public string STORELOCATIONNAME   { get; set; }
            public string MFDATE              { get; set; }
            public string EXPRDATE            { get; set; }
            public decimal ASSESMENTPERCENTAGE{ get; set; }
            public decimal MRP                { get; set; }
            public string WEIGHT              { get; set; }
            public decimal AMOUNT              { get; set; }

    }
    public class GRN_EDIT_GRNADDITIONALDETAILS
    {
            public decimal AMOUNT          { get; set; }
            public string  TAXID           { get; set; }
            public string  TAXNAME         { get; set; }
            public decimal PERCENTAGE      { get; set; }
            public string  STOCKRECEIVEDID { get; set; }
            public string LEDGERID         { get; set; }

    }
    public class GRN_EDIT_STOCKRECEIVEDREJECTIONTAX
    {
            public string  STOCKRECEIVEDID    { get; set; }
            public string  TAXID              { get; set; }
            public decimal PERCENTAGE         { get; set; }
            public string  POID               { get; set; }
            public string  PRODUCTID          { get; set; }
            public string  BATCHNO            { get; set; }
            public string  PRODUCTNAME        { get; set; }
            public decimal TAXVALUE           { get; set; }
            public string NAME                { get; set; }
    }
    public class GRN_EDIT_JOBORDERRECEIVEDDETAILS
    {
          public string STOCKRECEIVEDID     {get;set;}
          public string STOCKDESPATCHID     {get;set;}
          public string POID                {get;set;}
          public string CATID               {get;set;}
          public string CATNAME             {get;set;}
          public string PRODUCTID           {get;set;}
          public string PRODUCTNAME         {get;set;}
          public decimal POQTY              {get;set;}
          public decimal ISSUEQTY           {get;set;}
          public decimal DISPATCHQTY        {get;set;}
          public decimal RECEIVEQTY         {get;set;}
          public string WORKORDERPRODUCTID { get; set; }
         

    }
    public class GRN_EDIT_STOCKRECEIVEDSAMPLEQTY
    {
        public string STOCKRECEIVEDID  {get;set;}
        public string POID             {get;set;}
        public string PRODUCTID        {get;set;}
        public string PRODUCTNAME      {get;set;}
        public decimal RECEIVEDQTY     {get;set;}
        public decimal SAMPLEQTY       {get;set;}
        public decimal OBSERVATIONQTY { get; set; }
       
    }
    public class GRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAME
    {
        public string FILENAME { get; set; }
    }


   public class LoadGrn
    {
        public string  STOCKDESPATCHID   {get;set;}
        public string  STOCKRECEIVEDID   {get;set;}
        public string  STOCKRECEIVEDNO   {get;set;}
        public string  DESPATCHDATE      {get;set;}
        public string  STOCKRECEIVEDDATE {get;set;}
        public string  WAYBILLNO         {get;set;}
        public string  WAYBILLKEY        {get;set;}
        public string  INVOICENO         {get;set;}
        public string  VEHICHLENO        {get;set;}
        public string  LRGRNO            {get;set;}
        public string  MODEOFTRANSPORT   {get;set;}
        public string  MOTHERDEPOTNAME   {get;set;}
        public string  MOTHERDEPOTID     {get;set;}
        public string  VENDORNAME        {get;set;}
        public string  FINYEAR           {get;set;}
        public string  TRANSPORTERID     {get;set;}
        public string  TRANSPORTERNAME   {get;set;}
        public string  NEXTLEVELID       {get;set;}
        public string  ISVERIFIEDDESC    {get;set;}
        public string TPUNAME            {get; set;}
    }


    public class GrnQc
    {
        public string STOCKRECEIVEDID { get; set; }
        public string FILENAME { get; set; }
        public string QCID { get; set; }
        public string POID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal RECEIVEDQTY { get; set; }
        public decimal SAMPLEQTY { get; set; }
        public decimal OBSERVATIONQTY { get; set; }
        public string FILEPATH { get; set; }
    }
}
