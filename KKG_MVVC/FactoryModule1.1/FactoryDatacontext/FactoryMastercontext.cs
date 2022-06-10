using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masterfacmodel;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FactoryModel;

namespace FactoryDatacontext
{
    public class FactoryMastercontext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public SubItemVendorList SubItemVendor(string PrimaryItem)
        {
            SubItemVendorList subitemvendor = new SubItemVendorList();
            try
            {
                var reader = _db.QueryMultiple("USP_GET_SUBITEM_VENDOR_V2", new { P_PRIMARYID = PrimaryItem }, commandType: CommandType.StoredProcedure);
                var vsubitem = reader.Read<SubItemList>().ToList();
                var vvendor = reader.Read<VendorList>().ToList();


                subitemvendor.SubItemList = vsubitem;
                subitemvendor.VendorList = vvendor;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return subitemvendor;
        }

        public List<UomList> GetUom()
        {
            List<UomList> uom = new List<UomList>();

            try
            {
                uom = _db.Query<UomList>("USP_GET_UOM_V2 ", commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return uom;
        }

        public List<CustomerList> GetCustomer(string FactoryID)
        {
            List<CustomerList> customer = new List<CustomerList>();

            try
            {
                customer = _db.Query<CustomerList>("USP_BIND_CUSTOMER_IN_MATERIALMASTER_V2 ", new { P_FACTORYID = FactoryID }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return customer;
        }

        public List<PrimaryItemList> GetPrimaryItem()
        {
            List<PrimaryItemList> primaryItem = new List<PrimaryItemList>();

            try
            {
                primaryItem = _db.Query<PrimaryItemList>("USP_GET_PRIMARY_ITEM_V2 ", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return primaryItem;
        }

        public List<SubItemList> GetSubitem(string PrimaryItem)
        {
            List<SubItemList> subitem = new List<SubItemList>();
            try
            {
                subitem = _db.Query<SubItemList>("USP_GET_SUBITEM_V2 ", new { P_PRIMARYID = PrimaryItem }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return subitem;
        }

        public List<MessageModel> MaterialInsertUpdate(MaterialMasterModel materialmaster, int CreatedBy)
        {
            DataTable dtFactory;
            DataTable dtVendor;
            DataTable dtCustomer;
            dtFactory = FactoryTypeListDetails(materialmaster.FactoryTypeList);
            dtVendor = VendorTypeListDetails(materialmaster.VendorTypeList);
            dtCustomer = CustomerTypeListDetails(materialmaster.CustomerTypeList);
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_MATERIALMASTER_SAVE_V2 ",
                                                        new
                                                        {
                                                            P_ID = materialmaster.MaterialID,
                                                            P_CODE = materialmaster.ProductCode,
                                                            P_NAME = materialmaster.ProductName,
                                                            P_DIVID = materialmaster.PrimaryItemID,
                                                            P_DIVNAME = materialmaster.PrimaryItemText,
                                                            P_SUBTYPEID = materialmaster.SubItemID,
                                                            P_SUBTYPENAME = materialmaster.SubItemText,
                                                            P_UOMID = materialmaster.UomID,
                                                            P_UOMNAME = materialmaster.UomText,
                                                            P_UNITVALUE = materialmaster.UnitCapacity,
                                                            P_MINSTOOKLEVEL = materialmaster.ReorderLevel,
                                                            P_RETURNABLE = materialmaster.Returnable,
                                                            P_MRP = materialmaster.MRP,
                                                            P_MODE = materialmaster.FLAG,
                                                            P_ACTIVE = materialmaster.Active,
                                                            P_ASSESSABLEPERCENTAGE = materialmaster.Assesment,
                                                            P_CBU = CreatedBy,
                                                            P_DEPOTID = materialmaster.FactoryID,
                                                            P_FACTORYMAPID = materialmaster.FactoryMapID,
                                                            P_VENDORID = materialmaster.VendorMapID,
                                                            P_UNITCAPACITY = materialmaster.UnitCapacityInput,
                                                            P_FROMPACKSIZEID = materialmaster.PacksizeFrom,
                                                            P_FROMPACKSIZE = materialmaster.PacksizeFromText,
                                                            P_TOPACKSIZEID = materialmaster.PacksizeTo,
                                                            P_TOPACKSIZE = materialmaster.PacksizeToText,
                                                            P_CUSTOMERID = materialmaster.CustomerMapID,
                                                            TempTableFactory = dtFactory.AsTableValuedParameter("Type_MATERIAL_MASTER_FACTORY_MAP"),
                                                            TempTableVendor = dtVendor.AsTableValuedParameter("Type_MATERIAL_MASTER_VENDOR_MAP"),
                                                            TempTableCustomer = dtCustomer.AsTableValuedParameter("Type_MATERIAL_MASTER_CUSTOMER_MAP"),
                                                            P_PRODUCTOWNER = materialmaster.ProductOwner
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public DataTable FactoryTypeListDetails(List<FactoryTypeList> FactoryTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("VENDORID", typeof(string));
            dt.Columns.Add("VENDORNAME", typeof(string));

            int count = 1;
            foreach (var item in FactoryTypeList)
            {
                dt.Rows.Add(item.VENDORID,
                            item.VENDORNAME
                            );
                count++;
            }
            return dt;
        }

        public DataTable VendorTypeListDetails(List<VendorTypeList> VendorTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("VENDORID", typeof(string));
            dt.Columns.Add("VENDORNAME", typeof(string));

            int count = 1;
            foreach (var item in VendorTypeList)
            {
                dt.Rows.Add(item.VENDORID,
                            item.VENDORNAME
                            );
                count++;
            }
            return dt;
        }

        public DataTable CustomerTypeListDetails(List<CustomerTypeList> CustomerTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CUSTOMERID", typeof(string));
            dt.Columns.Add("CUSTOMERNAME", typeof(string));
            int count = 1;
            foreach (var item in CustomerTypeList)
            {
                dt.Rows.Add(item.CUSTOMERID,
                            item.CUSTOMERNAME
                            );
                count++;
            }
            return dt;
        }

        public List<MaterialMasterList> BindMaterialMasterGrid(string userID, string DepotID)
        {
            List<MaterialMasterList> productGrid = new List<MaterialMasterList>();
            try
            {
                productGrid = _db.Query<MaterialMasterList>("USP_BINDPRODUCT_DEPOTWISE_V2 ", new { P_USERID = userID, P_DEPOTID = DepotID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productGrid;
        }

        public List<MessageModel> IsExists(string ProductName, string Code)
        {
            List<MessageModel> exists = new List<MessageModel>();
            try
            {
                exists = _db.Query<MessageModel>("USP_CODE_NAME_CHECKING ", new { P_NAME = ProductName, P_CODE = Code }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exists;
        }

        public MaterialEditList EditMaterial(string ProductID)
        {
            MaterialEditList product = new MaterialEditList();
            try
            {
                var reader = _db.QueryMultiple("USP_EDIT_MATERIAL_V2", new { P_PRODUCTID = ProductID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<HeaderEditList>().ToList();
                var vfactory = reader.Read<FactoryEditList>().ToList();
                var vvendor = reader.Read<VendorEditList>().ToList();
                var vcustomer = reader.Read<CustomerEditList>().ToList();
                var vpacksize = reader.Read<PacksizeEditList>().ToList();


                product.HeaderEditList = vheader;
                product.FactoryEditList = vfactory;
                product.VendorEditList = vvendor;
                product.CustomerEditList = vcustomer;
                product.PacksizeEditList = vpacksize;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return product;
        }
        public List<ProductMasterModel> LoadBrand()
        {
            List<ProductMasterModel> pmaster = new List<ProductMasterModel>();
            try
            {
                pmaster = _db.Query<ProductMasterModel>("[USP_LOAD_BRAND_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return pmaster;
        }
        public List<ProductMasterModel> LOADCATEGORY()
        {
            List<ProductMasterModel> catname = new List<ProductMasterModel>();
            try
            {
                catname = _db.Query<ProductMasterModel>("[USP_LOAD_CATEGORY_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return catname;
        }

        public List<ProductMasterModel> BindBrandByCatname(string BRANDID)
        {
            List<ProductMasterModel> catname = new List<ProductMasterModel>();
            try
            {
                catname = _db.Query<ProductMasterModel>("[USP_LOAD_CATEGORY_BY_BRAND_V2]", new { P_BRANDID = BRANDID } , commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return catname;
        }
        public List<ProductMasterModel> LOADNATURE()
        {
            List<ProductMasterModel> NATURENAME = new List<ProductMasterModel>();
            try
            {
                NATURENAME = _db.Query<ProductMasterModel>("[USP_LOAD_NATURE_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return NATURENAME;
        }

        public List<ProductMasterModel> LOADUOM()
        {
            List<ProductMasterModel> UOM = new List<ProductMasterModel>();
            try
            {
                UOM = _db.Query<ProductMasterModel>("[USP_LOAD_UOM_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return UOM;
        }
        
        public List<ProductMasterModel> LOADFRAGNANCE()
        {
            List<ProductMasterModel> FRAGNANCE = new List<ProductMasterModel>();
            try
            {
                FRAGNANCE = _db.Query<ProductMasterModel>("[USP_LOAD_FRAGNANCE_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return FRAGNANCE;
        }

        public List<ProductMasterModel> LOADITEMTYPE()
        {
            List<ProductMasterModel> ITEMTYPE = new List<ProductMasterModel>();
            try
            {
                ITEMTYPE = _db.Query<ProductMasterModel>("[USP_LOAD_ITEMTYPE_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return ITEMTYPE;
        }

        public List<ProductMasterModel> LOADDEPOT()
        {
            List<ProductMasterModel> DEPOTNAME = new List<ProductMasterModel>();
            try
            {
                DEPOTNAME = _db.Query<ProductMasterModel>("[USP_LOAD_DEPOT_V2]", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return DEPOTNAME;
        }
    }
}
