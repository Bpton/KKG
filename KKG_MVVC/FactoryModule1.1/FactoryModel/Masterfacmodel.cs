using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Masterfacmodel
{
    //public ICacheManager _ICacheManager;
    public class Masterfacmodel
    {

    }

    public class MaterialMasterModel
    {
        public string FLAG { get; set; }
        public string MaterialID { get; set; }
        public string ProductOwner { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string PrimaryItemID { get; set; }
        public string PrimaryItemText { get; set; }
        public string SubItemID { get; set; }
        public string SubItemText { get; set; }
        public string UomID { get; set; }
        public string UomText { get; set; }
        public string UnitCapacity { get; set; }
        public string MRP { get; set; }
        public string ReorderLevel { get; set; }
        public string FactoryID { get; set; }
        public string FactoryText { get; set; }
        public string CustomerID { get; set; }
        public string VendorID { get; set; }
        public string UnitCapacityInput { get; set; }
        public string PacksizeFrom { get; set; }
        public string PacksizeFromText { get; set; }
        public string PacksizeTo { get; set; }
        public string PacksizeToText { get; set; }
        public string Assesment { get; set; }
        public string Returnable { get; set; }
        public string Active { get; set; }
        public string FactoryMapID { get; set; }
        public string VendorMapID { get; set; }
        public string CustomerMapID { get; set; }
        public List<FactoryTypeList> FactoryTypeList { get; set; }
        public List<VendorTypeList> VendorTypeList { get; set; }
        public List<CustomerTypeList> CustomerTypeList { get; set; }
    }

    public class PrimaryItemList
    {
        public string ID { get; set; }
        public string ITEMDESC { get; set; }
    }

    public class SubItemList
    {
        public string SUBTYPEID { get; set; }
        public string SUBITEMDESC { get; set; }
    }

    public class VendorList
    { 
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }

    public class SubItemVendorList
    {
        public List<SubItemList> SubItemList { get; set; }
        public List<VendorList> VendorList { get; set; }
    }

    public class UomList
    {
        public string UOMID { get; set; }
        public string UOMDESCRIPTION { get; set; }
    }

    public class CustomerList
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }

    public class FactoryTypeList
    {
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }

    public class VendorTypeList
    {
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }

    public class CustomerTypeList
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }

    public class MaterialMasterList
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string PRODUCTALIAS { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNITVALUE { get; set; }
        public string MRP { get; set; }
        public string DTOC { get; set; }
        public string STATUS { get; set; }
        public string ACTIVE { get; set; }
        public string TYPE { get; set; }
        public string ASSESSABLEPERCENT { get; set; }
        public string BRANCHNAME { get; set; }
        public string VENDORNAME { get; set; }
        public string UNITMAP { get; set; }
        public string CONVERSIONQTY { get; set; }
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string PRODUCTOWNER { get; set; }
    }

    public class HeaderEditList
    {
        public string PRODUCTOWNER { get; set; }
        public string PRODUCTALIAS { get; set; }
        public string CODE { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNITVALUE { get; set; }
        public string MRP { get; set; }
        public string ASSESSABLEPERCENT { get; set; }
        public string REORDERLEVEL { get; set; }
        public string RETURNABLE { get; set; }
        public string ACTIVE { get; set; }
        public string TYPE { get; set; }
    }

    public class FactoryEditList
    {
        public string FACTORYID { get; set; }
        public string FACTORYNAME { get; set; }
    }

    public class VendorEditList
    {
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
    }

    public class CustomerEditList
    {
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
    }

    public class PacksizeEditList
    {
        public string PACKSIZEID_FROM { get; set; }
        public string PACKSIZEID_TO { get; set; }
        public string CONVERSIONQTY { get; set; }
    }

    public class MaterialEditList
    {
        public List<HeaderEditList> HeaderEditList { get; set; }
        public List<FactoryEditList> FactoryEditList { get; set; }
        public List<VendorEditList> VendorEditList { get; set; }
        public List<CustomerEditList> CustomerEditList { get; set; }
        public List<PacksizeEditList> PacksizeEditList { get; set; }
    }
}
