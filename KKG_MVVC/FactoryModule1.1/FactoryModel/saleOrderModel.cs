using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class saleOrderModel
    {
       public decimal INVOICESTOCKQTY { get; set; }
       public decimal BASECOSTPRICE { get; set; }
       public string PRODUCTID { get; set; }
       public string PRODUCTNAME { get; set; }
       public decimal ORDERQTY { get; set; }
       public decimal AMENDMENTQTY { get; set; }
       public string PRODUCTPACKINGSIZEID { get; set; }
       public string PRODUCTPACKINGSIZE { get; set; }
       public string REQUIREDDATE { get; set; }
       public string REQUIREDTODATE { get; set; }
       public decimal RATE { get; set; }
       public decimal DISCOUNT { get; set; }
       public decimal DISCOUNTAMOUNT { get; set; }
       public decimal QTY { get; set; }
       public string REASON { get; set; }
       public string REASONID { get; set; }
       public decimal ALREADY_DELIVEREDQTY { get; set; }
       public decimal converqty { get; set; }
       public decimal RETURNVALUE { get; set; }

    }
    public class bstype
    {
        public string BSID { get; set; }
        public string BSNAME { get; set; }
    }
    public class DeliveryTerms
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class LoadSaleOrderGroup
    {
        public string DIS_CATID { get; set; }
        public string DIS_CATNAME { get; set; }
        public string GROUPID { get; set; }
        public string EXPORTBSID { get; set; }
    }
    public class Currencey
    {
        public string CURRENCYID { get; set; }
        public string CURRENCYNAME { get; set; }
    }
    public class Customer
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }
    public class ProductSaleOrder
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
    }
    public class PACKSIZEName
    {
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZEName_FROM { get; set; }
    }

    public class sale_order
    {
        public string saleOrderDate { get; set; }
        public string saleOrderNo { get; set; }
        public string refSaleOrderDate { get; set; }
        public string bsId { get; set; }
        public string bsName { get; set; }
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string remarks { get; set; }
        public string saleOrderId { get; set; }
        public string status { get; set; }
        public string MRPTag { get; set; }
        public string strTermsID { get; set; }
        public string currenceyId { get; set; }
        public string currenceyName { get; set; }
        public string deliverytermsId { get; set; }
        public string deliverytermsName { get; set; }
        public string icds { get; set; }
        public string icdsDate { get; set; }
        public decimal TotalCase { get; set; }
        public decimal TotalPCS { get; set; }
        public string Paymentterms { get; set; }
        public string Usanceperiod { get; set; }
        public string depotid { get; set; }
        public string UserID { get; set; }
        public string FinYear { get; set; }
        public string flag { get; set; }
        public string isCancelled { get; set; }
        public string reasonId { get; set; }
        public string reason { get; set; }
        
    }

    public class Load_saleOrder
    {
        public string SALEORDERID { get; set; }
        public string SALEORDERNO { get; set; }
        public string REFERENCESALEORDERNO { get; set; }
        public string SALEORDERDATE { get; set; }
        public string REFERENCESALEORDERDATE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string ISCANCELLED { get; set; }
        public decimal TOTALVALUE { get; set; }
        public decimal TOTALCASE { get; set; }
        public decimal TOTALPCS { get; set; }
    }

    public class Proform_status
    {
        public string PROFORMAINVOICEID { get; set; }
        public string ISVERIFIED { get; set; }
        public string APPROVEDTAG { get; set; }
        public decimal GROSSAMOUNT { get; set; }
    }
}
