using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
   public class Trandepotstock
    {

    }
    public class Motherdepot
    {

        public string BRID { get; set; }
        public string BRNAME { get; set; }
      
    }
    public class Ordertype
    {

        public string OrderTYPEID { get; set; }
        public string ORDERTYPENAME { get; set; }

    }
    public class Insurancecodepot
    {

        public string ID { get; set; }
        public string COMPANY_NAME { get; set; }

    }
    public class Insurancenodepot
    {

        public string INSURANCE_NO { get; set; }
        

    }

    public class Waybillno
    {

        public string WAYBILLNO { get; set; }


    }
    public class Categorydepot
    {

        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string HSN { get; set; }


    }
    public class Productdepot
    {

        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        


    }

    public class Transporterdepot
    {

        public string ID { get; set; }
        public string NAME { get; set; }



    }
    public class Deliveryaddress
    {

        public string DELIVERYADDRESS { get; set; }
        



    }
    public class Transitdays
    {

        public string TRANSIT_DAYS { get; set; }




    }
    public class Producttype
    {

        public string ptype { get; set; }




    }

     public class Packsizedepot
    {

        public string psid { get; set; }

        public string psname { get; set; }
        public string sequenceno { get; set; }


    }
    public class Batchdetail
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
        public string STORELOCATIONID  { get; set; }
    }
    public class Countrystock
    {
        public string COUNTRYID { get; set; }
        public string COUNTRYNAME { get; set; }
    }
    public class Saleorder
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
    }
    public class DepotRatesheet
    {
        public string RATE { get; set; }
       
    }
    public class Productdtlinterbatch
    {
        public string MRP { get; set; }
        public string ASSESSABLEPERCENT { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public string Price { get; set; }

    }
    public class Depostockmodel
    {
        public string STOCKTRANSFERID { get; set; }
        public string FLAG { get; set; }
        public string TRANSFERDATE { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string MOTHERDEPOTNAME { get; set; }
        public string TODEPOTID { get; set; }
        public string TODEPOTNAME { get; set; }
        public string WAYBILLNO { get; set; }
        public string WAYBILLAPPLICABLE { get; set; }
        public string INSURANCENO { get; set; }
        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string VEHICLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string CHALLANNO { get; set; }
        public string CHALLANDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string GATEPASSDATE { get; set; }
        public string FFORM { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string REMARKS { get; set; }
        public string MODULEID { get; set; }
        public string ORDERTYPE { get; set; }
        public string ORDERTYPENAME { get; set; }
        public string COUNTRYID { get; set; }
        public string COUNTRYNAME { get; set; }
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
        public decimal TotalCase { get; set; }
        public decimal TotalPcs { get; set; }
        public decimal GrossAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal TOTALTAXAMT { get; set; }
        public decimal BASICAMT { get; set; }
        public decimal RoundOff { get; set; }

        public int INVOICETYPE { get; set; }
        public string EXPORT { get; set; }
        public string SHIPINGADDRESS { get; set; }
        public string DELIVERYDATE { get; set; }
       
        public string TAXCOUNT { get; set; }
        public string userid { get; set; }
        public string Finyear { get; set; }

        public List<Depotstockdtl> Depotstockdtl { get; set; }
        public List<Freestock> Freestock { get; set; }

        
    }
    public class Interbatchmodel
    {
        public string ADJUSTMENTID { get; set; }
        public string ADJUSTMENTDATE { get; set; }
        public string FLAG { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public int CREATEDBY { get; set; }
        public string FINYEAR { get; set; }
        public string REMARKS { get; set; }
        public string TYPE { get; set; }
        public string TSALEADJUSTMENTID { get; set; }
        public List<Interbatchdtl> Depotstockdtl { get; set; }
    }
    public class Interbatchdtl
    {

        public string PRODUCTID { get; set; }

        
        public string PRODUCTNAME { get; set; }
        public string BATCHNO { get; set; }
        public decimal PRICE { get; set; }
        public decimal ADJUSTMENTQTY { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string REASONID { get; set; }

        public string REASONNAME { get; set; }
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public decimal MRP { get; set; }
        public string WEIGHT { get; set; }

        public decimal AMOUNT { get; set; }
        public decimal BUFFERQTY { get; set; }
        public string APPROVED { get; set; }

    }
        public class Depotstockdtl
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


    public class Depotstockdtl1
    {

        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKINGSIZEID { get; set; }
        public string PACKINGSIZENAME { get; set; }
        public string BATCHNO { get; set; }
        public decimal QTY { get; set; }
        public decimal MRP { get; set; }
       
        public decimal RATE { get; set; }
       
        public decimal AMOUNT { get; set; }
        public decimal TOTMRP { get; set; }
        public decimal ASSESMENTPERCENTAGE { get; set; }
        public decimal TOTALASSESMENTVALUE { get; set; }
        public string WEIGHT { get; set; }
        public string GROSSWEIGHT { get; set; }
       
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
       

    }
    public class Freestock
    {
        public string SCHEMEID { get; set; }
        public string SCHEME_PRODUCT_ID { get; set; }
        public string SCHEME_PRODUCT_NAME { get; set; }
        public decimal QTY { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PACKSIZEID { get; set; }
        public string PACKSIZENAME { get; set; }
        public decimal SCHEME_QTY { get; set; }
        public decimal MRP { get; set; }
        public decimal BRATE { get; set; }
        public decimal AMOUNT { get; set; }
        public string BATCHNO { get; set; }
        public string WEIGHT { get; set; }
        public string MFDATE { get; set; }
        public string EXPRDATE { get; set; }
        public decimal NSR { get; set; }
        public string SCHEME_PRODUCT_BATCHNO { get; set; }
        public string QSGUID { get; set; }
    }
    public class StocktransferGrid
    {
        public string STOCKTRANSFERID { get; set; }
        public string STOCKTRANSFERDATE { get; set; }
       
        public string STOCKTRANSFERNO { get; set; }
        public string TODEPOTNAME { get; set; }
        public string WAYBILLNO { get; set; }
        public string FFORMDATE { get; set; }
        public string FFORMNO { get; set; }
        public string FORMREQUIRED { get; set; }
        public string WAYBILLKEY { get; set; }
        public string ISVERIFIED { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string DAYENDTAG { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string TODEPOTID { get; set; }
        public string TOTALCASE { get; set; }
        public string TOTALPCS { get; set; }
        public string NETAMOUNT { get; set; }
        public string USERNAME { get; set; }
        public string APPROVAL_PERSON { get; set; }
        public string EXPORT { get; set; }
        

    }
    public class Stocktransferhdredit
    {
        public string STOCKTRANSFERID { get; set; }
        public string STOCKTRANSFERNO { get; set; }
        public string STOCKTRANSFERDATE { get; set; }
        public string MOTHERDEPOTID { get; set; }
        public string WAYBILLNOAPPLICABLE { get; set; }
        public string TODEPOTID { get; set; }
        public string WAYBILLKEY { get; set; }
        public string TRANSPORTERID { get; set; }
        public string MODEOFTRANSPORT { get; set; }
        public string VEHICHLENO { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
        public string CHALLANNO { get; set; }
        public string CHALLANDATE { get; set; }
        public string REMARKS { get; set; }
        public string NOTE { get; set; }
        public string GATEPASSDATE { get; set; }
        public string GATEPASSNO { get; set; }
        public string INSURANCECOMPID { get; set; }
        public string TOTALCASE { get; set; }
        public string TOTALPCS { get; set; }
        public string SHIPPINGADDRESS { get; set; }
        public string ORDERTYPEID { get; set; }
        public string COUNTRYID { get; set; }
        public string INSURANCECOMPNAME { get; set; }
        public string INSURANCENO  { get; set; }
       
        public string DELIVERYDATE { get; set; }
        public string INVOICE_TYPE { get; set; }
        
    }

    public class Stocktransferdtledit
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
    public class Stocktransfertax
    {
        public string ID { get; set; }
        public string TAXPERCENTAGE { get; set; }
        
    }


    public class Stocktransfertaxcountedit
    {
        public string TAXID { get; set; }
        public string NAME { get; set; }
        public string RELATEDTO { get; set; }
    }

    public class Stocktransfertaxedit
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

    public class Stocktransferfooteredit
    {
        public decimal GROSSAMOUNT { get; set; }
        public decimal OTHERCHARGESVALUE { get; set; }
        public decimal ADJUSTMENT { get; set; }
        public decimal ROUNDOFFVALUE { get; set; }
        public decimal NETAMOUNT { get; set; }
        public decimal TOTALTAXAMT { get; set; }
        public decimal BASICAMT { get; set; }
        
    }

    public class Stocktransferedit
    {
        public List<Stocktransferhdredit> Stocktransferhdredit { get; set; }
        public List<Stocktransferdtledit> Stocktransferdtledit { get; set; }
        public List<Stocktransfertaxcountedit> Stocktransfertaxcountedit { get; set; }
        public List<Stocktransfertaxedit> Stocktransfertaxedit { get; set; }
        public List<Stocktransferfooteredit> Stocktransferfooteredit { get; set; }
    }




}
