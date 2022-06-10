using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
    public class Claims
    {
    }
    public class Depo
    {

        public string BRID { get; set; }
        public string BRNAME { get; set; }
        
    }
    public class Businesssegment
    {

        public string ID { get; set; }
        public string NAME { get; set; }

    }
    public class Group
    {

        public string DIS_CATID { get; set; }
        public string DIS_CATNAME { get; set; }

    }
    public class BindClaimNarration
    {

        public string CLAIM_NARRATION { get; set; }
        public string NARRATIONID { get; set; }

        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }


    }
    public class LoadPrincipleGroup
    {

        public string GROUPID { get; set; }
        public string GROUPNAME { get; set; }

    }
    public class LoadDistributors
    {

        public string CUSTOMERID { get; set; }
        
        public string CUSTOMERNAME { get; set; }

    }
    public class LoadRetailers
    {

        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }

    }
    public class LoadBindGroupbyBS
    {

        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }

    }
    public class qpsclaimdtl
    {
        public string GUID { get; set; }
        public string INVOICE_NO { get; set; }
        public string INVOICE_DATE { get; set; }
        public string NARRATION { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }

        public string QTY { get; set; }
        public string QTYDETAILS { get; set; }
        /*NEW ADD FOR DISPLAY CLAIM BY P.BASU*/
        public decimal AMOUNT { get; set; }

        /*NEW ADD FOR GIFT CLAIM BY P.BASU*/
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }

    }
    public class qpsmodel
    {
        public string ClaimID { get; set; }
        public string Mode { get; set; }
        public string bsid { get; set; }
        public string bsname { get; set; }
        public string grid { get; set; }
        public string grname { get; set; }
        public string distid { get; set; }
        public string distname { get; set; }
        public string retrid { get; set; }
        public string retrname { get; set; }
        public string TypeID { get; set; }
        public string UserID { get; set; }
        public string FinYear { get; set; }
        public string remarks { get; set; }
        public string tag { get; set; }
        public string Amount { get; set; }

        public string Qty { get; set; }
        public string depotid { get; set; }
        public string depotname { get; set; }

        public string date { get; set; }
        public string claimno { get; set; }
        public string processno { get; set; }

    }
    public class claimstatus

    {
        public string ID { get; set; }
        public string CLAIMSTATUSNAME { get; set; }
    }
        public class qpshdr

    {
        public string ID { get; set; }
        public string QV_CLM_NO { get; set; }
        public string CLAIM_DATE { get; set; }
        public string PROCESSNUMBER { get; set; }
        public string PROCESS_DATE { get; set; }
        public string CLAIM_BUSINESSSEGMENT_ID { get; set; }
        public string CLAIM_BUSINESSSEGMENT_NAME { get; set; }
        public string DEPOTNAME { get; set; }
        public string CLAIM_PRINCIPLEGROUP_ID { get; set; }
        public string CLAIM_DISTRIBUTOR_ID { get; set; }
        public string CLAIM_DISTRIBUTOR_NAME { get; set; }
        public string CLAIM_RETAILER_ID { get; set; }
        public string AMOUNT { get; set; }
        public string CLAIM_MODI_AMT { get; set; }
        public string NARRATION { get; set; }
        public string CURRENTSTATUS { get; set; }
        public string DOCUMENTSRECEIVEDDATE { get; set; }
        public string DOCUMENTSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string DEDUCTIONDETAILS { get; set; }
        /*new add for damage claim by p.basu*/
        public string DAM_CLM_NO { get; set; }
        /*new add for display claim by p.basu*/
        public string DIS_CLM_NO { get; set; }
        /*new add for gift claim by p.basu*/
        public string STCK_CLM_NO { get; set; }
    }

        public class CategoryClaim
        {
         public string CATID { get; set; }
         public string CATNAME { get; set; }
        }

    public class ProductClaim
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }

    }

    public class ProductPacksize
    {
        public string PSID { get; set; }
        public string PSNAME { get; set; }

    }

    public class ProductBatchno
    {
        public string BATCHNO { get; set; }
        public string SALEINVOICEID { get; set; }

    }
    public class DamageReason
    {
        public string ID { get; set; }
        public string NAME { get; set; }

    }
    public class DamageClaimDetailsGrid
    {
        public string GUID { get; set; }
        public string CLAIM_PRODUCT_ID { get; set; }
        public string CLAIM_PRODUCT_NAME { get; set; }
        public string CLAIM_PACKSIZE_ID { get; set; }
        public string CLAIM_PACKSIZE_NAME { get; set; }
        public string CLAIM_CAT_ID { get; set; }
        public string CLAIM_CAT_NAME { get; set; }
        public string BATCHNO { get; set; }
        public string INVOICE_NO { get; set; }
        public string INVOICE_DATE { get; set; }
        public decimal MRP { get; set; }
        public decimal DISCOUNT_PRICE { get; set; }
        public decimal BASE_PRICE { get; set; }
        public decimal CLAIM_QTY { get; set; }
        public decimal TOTAL_INVOICE_QTY { get; set; }
        public decimal AMOUNT { get; set; }
        public string REASON_ID { get; set; }
        public string REASON { get; set; }
        public string NARRATION { get; set; }

    }

    public class Damagehdr

    {
        public string ID { get; set; }
        public string DAN_CLM_NO { get; set; }
        public string CLAIM_BUSINESSSEGMENT_ID { get; set; }
        public string CLAIM_BUSINESSSEGMENT_NAME { get; set; }
        public string CLAIM_PRINCIPLEGROUP_ID { get; set; }
        public string CLAIM_PRINCIPLEGROUP_NAME { get; set; }
        public string CLAIM_DISTRIBUTOR_ID { get; set; }
        public string CLAIM_DISTRIBUTOR_NAME { get; set; }

        public string CLAIM_RETAILER_ID { get; set; }
        public string CLAIM_RETAILER_NAME { get; set; }
        public string NEXT_LEVEL_APPROVAL_UID { get; set; }
        public string CURRENT_LEVEL_UID { get; set; }
        public string CURRENTSTATUS { get; set; }
        public string CLAIM_TYPEID { get; set; }
        public string CBU { get; set; }
        public string DTOC { get; set; }
        public string DTOM { get; set; }
        public string DOCUMENTSRECEIVEDATE { get; set; }
        public string ISAPPROVED { get; set; }
        public string CLAIM_TAG { get; set; }
        public string FINYEAR { get; set; }
        public string DEDUCTIONDETAILS { get; set; }
        public string REMARKS { get; set; }
        public string REASON_ID { get; set; }
        public string REASON_NAME { get; set; }
        public string CLAIM_AMT { get; set; }
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string PROCESSNUMBER { get; set; }
        public string CLAIM_MODI_AMT { get; set; }
        public string AccEntryID { get; set; }
    }
    public class Damagedetails

    {
        public string CL_GUID { get; set; }
        public string ID { get; set; }
        public string CLAIM_PRODUCT_ID { get; set; }
        public string CLAIM_PRODUCT_NAME { get; set; }
        public string CLAIM_PACKSIZE_ID { get; set; }
        public string CLAIM_PACKSIZE_NAME { get; set; }
        public string CLAIM_CAT_ID { get; set; }
        public string CLAIM_CAT_NAME { get; set; }
        public string BATCHNO { get; set; }
        public string INVOICE_NO { get; set; }
        public string INVOICE_DATE { get; set; }
        public string MRP { get; set; }
        public string BASE_PRICE { get; set; }
        public string DISCOUNT_PRICE { get; set; }
        public string CLAIM_QTY { get; set; }
        public string TOTAL_INVOICE_QTY { get; set; }
        public string AMOUNT { get; set; }
        public string REASON_ID { get; set; }
        public string REASON { get; set; }
        public string NARRATION { get; set; }
    }

    /*FOR DISTRIBUTOR KYC*/

    public class DistribuotrInfo
    {
        public string DISTRIBUTORID { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string DIST_CODE { get; set; }
        public string DIST_TYPE { get; set; }
        public string DIST_TYPEID { get; set; }
        public string STATE_NAME { get; set; }
        public string STATEID { get; set; }
        public string DEPOT_NAME { get; set; }
        public string DEPOTID { get; set; }
        public string DIST_ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public string CONTACTPERSON { get; set; }
        public string CONTACTPERSON2 { get; set; }
        public string EMAILID1 { get; set; }
        public string MOBILE2 { get; set; }
        public string CITYNAME { get; set; }
       // public string WHATSAPP { get; set; }
        public string OTHERS_COMPANY { get; set; }
        public string REMARKS { get; set; }
        public string LMBU { get; set; }
        public string FILE_PATH { get; set; }
        public string ALTERNATEADDRESS { get; set; }
        public string PIN { get; set; }
        public string DTOC { get; set; }
        public string OTHERCOMPANYID { get; set; }
        public string DOB { get; set; }
        public string ANVDATE { get; set; }
        public string Total_Godown_Size { get; set; }
        public string Godown_Size_McNROE { get; set; }
        public string No_Sales_Person { get; set; }
        public string No_Delivery_Person { get; set; }
        public string Total_Annual_Turnover_Lakh { get; set; }
        public string McNROE_Turnover_Lakh { get; set; }
        public string DATE { get; set; }
        public string ISKYC { get; set; }
        public string COMPANY_NAME { get; set; }
        public string COMPANY_ID { get; set; }
        public string MODE { get; set; }
        public string ASMCODE { get; set; }
        public string ASMNAME { get; set; }
        public string SOCODE { get; set; }
        public string SONAME { get; set; }
        public string KAECODE { get; set; }
        public string KAENAME { get; set; }
        public string KAMCODE { get; set; }
        public string KAMNAME { get; set; }
        public string WHATSAPPNO { get; set; }
        
       

    }

    public class DistribuotrpanindiaInfo
    {
        public string DEPOTNAME { get; set; }
        public string SOCODE { get; set; }
        public string SONAME { get; set; }
        public string ASMCODE { get; set; }
        public string ASMNAME { get; set; }
        public string CODE { get; set; }
        public string CUSTOMERTYPE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string CONTACTPERSON1 { get; set; }
        public string CONTACTPERSON2 { get; set; }
        public string ADDRESS { get; set; }
        public string CITYNAME { get; set; }
        public string PIN { get; set; }
        public string WHATSAPPNO { get; set; }
       
        public string OTHERS_COMPANY { get; set; }
        public string COMPANY_NAME { get; set; }
        public string DOB { get; set; }
        public string ANVDATE { get; set; }
        
        public string Total_Godown_Size { get; set; }
        public string Godown_Size_McNROE { get; set; }
        public string No_Sales_Person { get; set; }
        public string No_Delivery_Person { get; set; }
        public string Total_Annual_Turnover_Lakh { get; set; }
        public string McNROE_Turnover_Lakh { get; set; }
        public string ISKYC_DATE { get; set; }
        public string ZSMCODE { get; set; }
        public string ZSMNAME { get; set; }
        public string RSMNAME { get; set; }
        public string RSMCODE { get; set; }
        
        public string KAOCODE { get; set; }
        public string KAONAME { get; set; }
        public string KAMCODE { get; set; }
        public string KAMNAME { get; set; }
       
    }
    public class DistributorType
    {
        public string DIST_TYPE { get; set; }
        public string DIST_TYPEID { get; set; }

    }

    public class DistributorCompany
    {
        public string OTHERCOMPANYID { get; set; }
        public string OTHERCOMPANYNAME { get; set; }

    }
    /*FOR AUTOMAIL */
    public class CustomerinfoAutomail
    {
        public string CUSTOMERNAME { get; set; }
        public string USERID { get; set; }
        public string UTNAME { get; set; }
        public string UTID { get; set; }
      
    }


    public class ServiceproviderDetails
    {
        public string ServiceproviderID  { get; set; }
        public string Messagetype { get; set; }
        public string ServiceProviderName { get; set; }
        public string SenderID { get; set; }
        public string SmsUserid { get; set; }
        public string Smspwd { get; set; }
        public string Smtp { get; set; }
        public string Emailfrm { get; set; }
        public string Port { get; set; }
        public string Emailuserid { get; set; }
        public string Emailpwd { get; set; }
        public string Isactive { get; set; }
        public string PageName { get; set; }
        public string ID { get; set; }
        public string PageURL { get; set; }
        public string MessageContent { get; set; }
        public string SchedulerType { get; set; }
        public string Monthly { get; set; }
        public string Daily { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string USERID { get; set; }
        public string UTNAME { get; set; }
        public string UTID { get; set; }
        public string MOBILE { get; set; }
        public string TemplateID { get; set; }
        public string mode { get; set; }



    }
    public class AutomailEmailDetails
    {
        public string ReportID { get; set; }
        public string ReportName { get; set; }
        public string ReportSubscriberEmail { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string EMAIL { get; set; }
        public string mode { get; set; }
        public string SendType { get; set; }
       
        public string ReportCode { get; set; }
        public string StoreProcedure { get; set; }
        public string Sp_Parameters { get; set; }
      
    }

    public class Invoiceupdater
    {
        public string DEPOTID { get; set; }
        public string DEPOTNAME { get; set; }
        public string DISTRIBUTORNAME { get; set; }
        public string DISTRIBUTORID { get; set; }
        public string SALEINVOICEID { get; set; }
        public string SALEINVOICENO { get; set; }
        public string SALEINVOICEDATE { get; set; }
        public string code { get; set; }
        public string userid { get; set; }
        public string SFID { get; set; }
        public string SFNAME { get; set; }
    }
    /*-------USERMASTER--------------*/
    public class UsermasterDocument
    {
        public string USERNAME { get; set; }
        public int USERID { get; set; }
        public string REPORTINGTOID { get; set; }
        public string REPORTINGTONAME { get; set; }
        public string FILE_PATH { get; set; }
        public string REMARKS { get; set; }
        public string MODE { get; set; }
        public string LDTOM { get; set; }
        public string PARTY_TYPE { get; set; }
       
    }

    public class UsermasterPanDocument
    {
        public string UPLOADDATE { get; set; }
        public string UPLOADBY_USER { get; set; }
        public string PARTY_TYPE { get; set; }
        public string REPORTING_PERSON { get; set; }
        public string REPORTING_PERSON_USERTYPE { get; set; }
        public string REMARKS { get; set; }
        public string FILE_PATH { get; set; }
        public string USER_TYPE { get; set; }
        

    }
    /*-------ATTENDENSE--------------*/
    public class UserattendenceMaster
    {
        public string USERID { get; set; }
        public string USERNAME { get; set; }
      

    }
}
