using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class PurchaseOrder
    {
        public string PONO { get; set; }
        public string EditPoid { get; set; }
        public string Depotid { get; set; }
        public string Podate { get; set; }
        public string Vendorid { get; set; }
        public string Vendorname { get; set; }
        public string Qutrefno { get; set; }
        public string Qutrefdate { get; set; }
        public string Productid { get; set; }
        public string Productname { get; set; }
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZENAME_FROM { get; set; }
        public string Qty { get; set; }
        public string Currencyid { get; set; }
        public string CURRENCYTYPE { get; set; }
        public string RATE { get; set; }
        public string LASTRATE { get; set; }
        public string MAXRATE { get; set; }
        public string MINRATE { get; set; }
        public string AVGRATE { get; set; }
        public string Cgst { get; set; }
        public string Sgst { get; set; }
        public string Igst { get; set; }
        public string REQUIREDDate { get; set; }
        public string REQUIREDTODATE { get; set; }
        public string Poid { get; set; }
        public string ASSESSABLEPERCENT { get; set; }
        public string MRP { get; set; }
        public string TAXID { get; set; }
        public string CGSTTAXID { get; set; }
        public string SGSTTAXID { get; set; }
        public string IGSTTAXID { get; set; }
        public string NAME { get; set; }
        public decimal PERCENTAGE { get; set; }
        public decimal Totalbasicvalue { get; set; }
        public decimal Totalmrp { get; set; }
        public decimal Totaladjusment { get; set; }
        public decimal Discper { get; set; }
        public decimal Discamnt { get; set; }
        public decimal Totalgross { get; set; }
        public string Termscondition { get; set; }
        public string Shippingadress { get; set; }
        public string Remarks { get; set; }
        public string Fromdate { get;set; }
        public string Todate { get; set; }
        public string Mode { get; set; }
        public string Createdby { get; set; }
        public string PACKINGPERCENTAGE { get; set; }
        public string PACKING { get; set; }
        public string EXERCISEPERCENTAGE { get; set; }
        public string EXERCISE { get; set; }
        public string SALETAXPERCENTAGE { get; set; }
        public string SALETAX { get; set; }
        public string OTHERCHARGES { get; set; }
        public string TOTALAMOUNT { get; set; }
        public string NETTOTAL { get; set; }
        public string FINYEAR { get; set; }
        public string QSTAG { get; set; }
        public string MODULEID { get; set; }
       /*FACTORYID TO DEPOTID FOR SESSION*/
        public string FACTORYID { get; set; }
        public string REFERENCEPOID { get; set; }
        public string INDENTID { get; set; }
        public string REFRENCENO { get; set; }
        public string TERMSID { get; set; }
        public string QUOTDATE { get; set; }
        public string REJECTIONNOTE { get; set; }
        public string ENTRYFROM { get; set; }

        public string UPLOADID { get; set; }
        public string UPLOADFILEPATH { get; set; }
        public string UPLOADDATE { get; set; }
        public string UPLOADFILENAME { get; set; }
        public string UPLOADFOR { get; set; }

        public List<PurchaseOrderdetails> PurchaseOrderdetails { get; set; }
        
    }
    public class PurchaseOrderdetails
    {
        public string slno { get; set; }
        public string POID { get; set; }
        public string CATEGORYID { get; set; }
        public string CATEGORYName { get; set; }
        public string DIVISIONID { get; set; }
        public string DIVISIONName { get; set; }
        public string NATUREOFPRODUCTID { get; set; }
        public string NATUREOFPRODUCTNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMName { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTName { get; set; }
        public decimal QTY { get; set; }
        public decimal RATE { get; set; }
        public decimal PRODUCTAMOUNT { get; set; }
        public string REQUIREDDate { get; set; }
        public string REQUIREDTODATE { get; set; }
        public decimal MRP { get; set; }
        public decimal TOTMRP { get; set; }
        public string ASSESMENTPERCENTAGE { get; set; }
        public string EXCISEPERCENTAGE { get; set; }
        public string CSTPERCENTAGE { get; set; }
        public decimal LASTRATE { get; set; }
        public decimal MAXRATE { get; set; }
        public decimal MINRATE { get; set; }
        public decimal AVGRATE { get; set; }
        public decimal PRODUCTPRICE { get; set; }
        public string IGSTTAXID { get; set; }
        public string CGSTTAXID { get; set; }
        public string SGSTTAXID { get; set; }
        public decimal Igst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string PONO { get; set; }
        public string PODATE { get; set; }
        public string CREATEDFROM { get; set; }
        //public decimal PACKING { get; set; }
        //public decimal EXERCISEPERCENT { get; set; }
        //public decimal  EXERCISE { get; set; }
        //public decimal SALETAXPERCENT { get; set; }
        //public decimal SALETAX { get; set; }
        //public decimal OTHERCHARGES  { get; set; }
        //public decimal TOTALAMOUNT { get; set; }
        //public decimal NETTOTAL { get; set; }
        //public decimal MRPTOTAL { get; set; }
        //public string QSTAG { get; set; }
        //public string MODULEID { get; set; }

    }

    //public class PurchaseOrderfooter
    //{
    //    public string POID { get; set; }
    //    public string GROSSTOTAL { get; set; }
    //    public string ADJUSTMENT { get; set; }
    //    public string DISCOUNTPERCENTAGE { get; set; }
    //    public string DISCOUNT { get; set; }
    //    public string PACKINGPERCENTAGE { get; set; }
    //    public string PACKING { get; set; }
    //    public string EXERCISEPERCENTAGE { get; set; }
    //    public string EXERCISE { get; set; }
    //    public string SALETAXPERCENTAGE { get; set; }
    //    public string SALETAX { get; set; }
    //    public string OTHERCHARGES { get; set; }
    //    public string TOTALAMOUNT { get; set; }
    //    public string NETTOTAL { get; set; }
    //    public string MRPTOTAL { get; set; }
    //}

    public class Polasrate
    {
        public decimal LASTRATE { get; set; }
    }
    public class Pomaxrate
    {
        public decimal MAXRATE { get; set; }
    }
    public class Pominrate
    {
        public decimal MINRATE { get; set; }
    }
    public class Poavgrate
    {
        public decimal AVGRATE { get; set; }
    }

    public class Povendor
    {
        public string Vendorid { get; set; }
        public string Vendorname { get; set; }
    }

    public class Pocurrncey
    {
        public string Currencyid { get; set; }
        public string CURRENCYTYPE { get; set; }
    }

    public class Pounit
    {
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZENAME_FROM { get; set; }
    }
    public class Pomrp
    {
        public decimal ASSESSABLEPERCENT { get; set; }
        public decimal MRP { get; set; }
    }

    public class Porate
    {
        public decimal RATE { get; set; }
    }

    public class Pomrprateunit
    {
        public List<Porate> Porate { get; set; }
        public List<Pomrp> Pomrp { get; set; }
        public List<Pounit> Pounit { get; set; }

    }
    public class Pomaxminlastrate
    {
        public List<Polasrate> Polasrate { get; set; }
        public List<Pomaxrate> Pomaxrate { get; set; }
        public List<Pominrate> Pominrate { get; set; }
        public List<Poavgrate> Poavgrate { get; set; }

    }

    public class Povendorcurrencey
    {
        public List<Povendor> Povendors { get; set; }
        public List<Pocurrncey> Pocurrnceys { get; set; }

    }

    public class Potax
    {
        public string CGSTTAXID { get; set; }
        public string CGSTNAME { get; set; }
        public decimal CGSTPERCENTAGE { get; set; }
        public string SGSTTAXID { get; set; }
        public string SGSTNAME { get; set; }
        public decimal SGSTPERCENTAGE { get; set; }
        public string IGSTTAXID { get; set; }
        public string IGSTNAME { get; set; }
        public decimal IGSTPERCENTAGE { get; set; }

    }
    public class PodetailsEdit
    {
        public List<PurchaseOrderdetails> Podetails { get; set; }
        public List<EditPo> EditPos { get; set; }
    }

    public class PodetailsGrid
    {
        public string Sl { get; set; }
        public string PODATE { get; set; }
        public string PONO { get; set; }
        public string VENDORNAME { get; set; }
        public string ISVERIFIEDDESC { get; set; }
        public string CREATEDFROM { get; set; }
        public string POID { get; set; }
    }

    public class EditPo
    {
        public decimal GROSSTOTAL { get; set; }
        public decimal ADJUSTMENT { get; set; }
        public decimal DISCOUNTPERCENTAGE { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal PACKINGPERCENTAGE { get; set; }
        public decimal PACKING { get; set; }
        public decimal EXERCISEPERCENTAGE { get; set; }
        public decimal EXERCISE { get; set; }
        public decimal SALETAXPERCENTAGE { get; set; }
        public decimal SALETAX { get; set; }
        public decimal OTHERCHARGES { get; set; }
        public decimal TOTALAMOUNT { get; set; }
        public decimal NETTOTAL { get; set; }
        public decimal MRPTOTAL { get; set; }
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
        public string REFERENCEPOID { get; set; }
        public string REMARKS { get; set; }
        public string REFRENCENO { get; set; }
        public string QUOTDATE { get; set; }
        public string SHIPPING_ADRESS { get; set; }
        public string TERMS_CONDITION { get; set; }
        public string CURRENCYID { get; set; }
        public string CURRENCYTYPE { get; set; }
        public string Poid { get; set; }
        public string PONO { get; set; }
        public string PODATE { get; set; }
    }

    //public class EditPoDetails
    //{
    //    public string PONO { get; set; }
    //    public string PODATE { get; set; }
    //    public string CATEGORYID { get; set; }
    //    public string CATEGORYName { get; set; }
    //    public string DIVISIONID { get; set; }
    //    public string DIVISIONName { get; set; }
    //    public string NATUREOFPRODUCTID { get; set; }
    //    public string NATUREOFPRODUCTNAME { get; set; }
    //    public decimal MRP { get; set; }
    //    public decimal MRPVALUE { get; set; }
    //    public decimal ASSESMENTPERCENTAGE { get; set; }
    //    public decimal EXCISE { get; set; }
    //    public decimal CST { get; set; }
    //    public string UOMID { get; set; }
    //    public string UOMNAME { get; set; }
    //    public string PRODUCTID { get; set; }
    //    public string PRODUCTName { get; set; }
    //    public decimal PRODUCTQTY { get; set; }
    //    public decimal PRODUCTPRICE { get; set; }
    //    public decimal PRODUCTAMOUNT { get; set; }
    //    public string REQUIREDDate { get; set; }
    //    public string REQUIREDTODate { get; set; }
    //    public string REFRENCENO { get; set; }
    //    public string QUOTDATE { get; set; }
    //    public string SHIPINGADDRESS { get; set;}
    //    public string CURRENCYTYPE { get; set; }
    //    public string CURRENCYID { get; set; }
    //    public string REJECTIONNOTE { get; set; }
    //    public decimal LASTRATE { get; set; }
    //    public decimal MAXRATE { get; set; }
    //    public decimal MINRATE { get; set; }
    //    public decimal AVGRATE { get; set; }
    //    public string TERMSCONDITION { get; set; }
    //    public decimal CGST { get; set; }
    //    public string CGSTTAXID { get; set; }
    //    public decimal SGST { get; set; }
    //    public string SGSTTAXID { get; set; }
    //    public decimal IGST { get; set; }
    //    public string IGSTTAXID { get; set; }
    //}
}
