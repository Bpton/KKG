using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
    public class FactoryDispatchContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<EmployeeDetails> BindEmployeeGrid()
        {
            List<EmployeeDetails> EmployeeDetails = new List<EmployeeDetails>();
            try
            {
                EmployeeDetails = _db.Query<EmployeeDetails>("USP_GET_EMPLOYE_DETAILS ", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EmployeeDetails;

        }
        public List<EmployeeDetails> BindDesignantion()
        {
            List<EmployeeDetails> EmployeeDetails = new List<EmployeeDetails>();
            try
            {
                EmployeeDetails = _db.Query<EmployeeDetails>("usp_bind_Designation ", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EmployeeDetails;

        }


        public List<MessageModel> UpdateEmployeDesignnation(string id,  string dId)
        {

            EmployeeDetails employeeDetails = new EmployeeDetails();
            List<MessageModel> response = new List<MessageModel>();

            try
            {

                response = _db.Query<MessageModel>("usp_update_employe_details ",
                                                        new
                                                        {
                                                            id = employeeDetails.EmployeId,
                                                            dId = employeeDetails.Designation
                                                            
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

            public List<SourceDepot> GetSourceDepot(string UserID)
        {
            List<SourceDepot> sourceDepot = new List<SourceDepot>();
            try
            {
                sourceDepot = _db.Query<SourceDepot>("USP_GET_DEPOT_BASE_ON_USER", new { P_USERID = UserID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return sourceDepot;
        }

        public List<DestinationDepot> GetDestinationDepot(string SourceDepotID)
        {
            List <DestinationDepot> todepot = new List<DestinationDepot>();
            List<Depot> depolist = new List<Depot>();
            try
            {
                depolist = _db.Query<Depot>("USP_BIND_TODEPOT ", new { P_DEPOID = SourceDepotID }, commandType: CommandType.StoredProcedure).ToList();
                
                foreach (var depotlst in depolist)
                {
                    todepot.Add(new DestinationDepot()
                    {
                        TODEPOTID = depotlst.BRID,
                        BRPREFIX = depotlst.BRNAME,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return todepot;
        }

        public List<FGCustomer> GetfgCustomer(string FactoryID,string BSID,string GroupID)
        {
            List<FGCustomer> fgcustomer = new List<FGCustomer>();
            
            try
            {
                fgcustomer = _db.Query<FGCustomer>("USP_BIND_CUSTOMER_MMPP_FG ", new { P_FACTORYID = FactoryID, P_BSID = BSID, P_GROUPID = GroupID }, commandType: CommandType.StoredProcedure).ToList();

                
            }
            catch (Exception ex)
            {
            }
            return fgcustomer;
        }

        public List<FGCustomer> GetTradingCustomer(string FactoryID, string BSID, string GroupID)
        {
            List<FGCustomer> fgcustomer = new List<FGCustomer>();

            try
            {
                fgcustomer = _db.Query<FGCustomer>("USP_BIND_CUSTOMER_MMPP ", new { P_FACTORYID = FactoryID, P_BSID = BSID, P_GROUPID = GroupID }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return fgcustomer;
        }

        public List<Waybill> GetWaybillno(string TransferID,string DepotID)
        {
            List<Waybill> waybill = new List<Waybill>();
            try
            {
                waybill = _db.Query<Waybill>("UPDG_BINDWAYBILL ", new { TRANSFERID = TransferID, DEPOID= DepotID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return waybill;
        }
        public List<InsuranceCompany> GetInsurancecompany(string menuID)
        {
            List<InsuranceCompany> insurancecompany = new List<InsuranceCompany>();
            try
            {
                insurancecompany = _db.Query<InsuranceCompany>("USP_BINDTRANSPORTER_FAC ", new { MenuID = menuID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return insurancecompany;
        }
        public List<PolicyNo> GetPolicyno(string companyID)
        {
            List<PolicyNo> policyNo = new List<PolicyNo>();
            try
            {
                policyNo = _db.Query<PolicyNo>("USP_BINDPOLICYNO ", new { COMPANYID = companyID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return policyNo;
        }

        public List<TransporterList> GetTransporter(string SourceDepotID,string TxnID)
        {
            List<TransporterList> transporterlist = new List<TransporterList>();
            List<Transporter> transporter = new List<Transporter>();
            try
            {
                transporter = _db.Query<Transporter>("USP_GET_TRANSPORTER_BASED_ON_DEPOT ", new { P_DEPOTID = SourceDepotID, P_TXNID= TxnID }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in transporter)
                {
                    transporterlist.Add(new TransporterList()
                    {
                        TransporterID = lst.ID,
                        TransporterNAME = lst.NAME,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return transporterlist;
        }

        public List<ShippingAddressList> GetShippingAddress(string FromDepot, string ToDepot,string Finyear,string ModuleID)
        {
            List<ShippingAddressList> shippinglist = new List<ShippingAddressList>();
            List<ShippingAddress> shipping = new List<ShippingAddress>();
            try
            {
                shipping = _db.Query<ShippingAddress>("USP_FETCH_RECENT_TXN_TRANSPORTER_FAC", new { P_FROMDEPOT = FromDepot, P_TODEPOT = ToDepot, P_FINYEAR = Finyear, P_MODULEID = ModuleID }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in shipping)
                {
                    shippinglist.Add(new ShippingAddressList()
                    {
                        hdnTransporterID = lst.ID,
                        hdnTransporterNAME = lst.NAME,
                        ShippingAddress = lst.SHIPPINGADDRESS,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return shippinglist;
        }

        public List<TransitDays> GetTransitDays(string sourcedepot,string destinationdepot,string invoicedate)
        {
            List<TransitDays> days = new List<TransitDays>();
            try
            {
                days = _db.Query<TransitDays>("USP_CHECK_DELIVERY_DATE ", new { P_FROMDEPOT = sourcedepot, P_TODEPOT = destinationdepot, P_INVOICEDATE = invoicedate}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return days;
        }

        public List<FGShipplingAddress> fgShippingAddress(string customerID)
        {
            List<FGShipplingAddress> address = new List<FGShipplingAddress>();
            try
            {
                address = _db.Query<FGShipplingAddress>("USP_BIND_SHIPPING_ADDRESS_FAC ", new { p_CUSTOMERID = customerID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return address;
        }

        public List<FGSaleOrder> fgSaleOrder(string customerID,string DepotID,string Finyear,string type)
        {
            List<FGSaleOrder> saleorder = new List<FGSaleOrder>();
            try
            {
                saleorder = _db.Query<FGSaleOrder>("USP_FG_SALE_ORDER_FAC ", new { p_CUSTOMERID = customerID, P_DEPOTID= DepotID, p_FINYEAR = Finyear, p_TYPE= type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return saleorder;
        }

        public List<Category> GetCategory(string Condition)
        {
            List<Category> category = new List<Category>();
            try
            {
                category = _db.Query<Category>("USP_BIND_CATEGORY", new { P_CONDITION = Condition}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return category;
        }
        public List<CategoryRM> GetCategoryRM(string Condition)
        {
            List<CategoryRM> category = new List<CategoryRM>();
            try
            {
                category = _db.Query<CategoryRM>("USP_BIND_CATEGORY_RMPM", new { P_CONDITION = Condition }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return category;
        }
        public List<ProductList> GetProduct(string SourceDepot,string Type)
        {
            List<ProductList> productLists = new List<ProductList>();
            try
            {
                productLists = _db.Query<ProductList>("USP_BIND_PRODUCT_FAC ", new { P_DEPOTID = SourceDepot, P_TYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return productLists;
        }

        public List<InvoiceProductList> GetInvoiceProduct(string SaleorderID)
        {
            List<InvoiceProductList> productLists = new List<InvoiceProductList>();
            try
            {
                productLists = _db.Query<InvoiceProductList>("USP_BIND_ORDER_PRODUCT_FAC ", new { P_SALEORDERID = SaleorderID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return productLists;
        }

        public List<PacksizeList> GetPacksize(string ProductID,string Type)
        {
            List<PacksizeList> packsizeLists = new List<PacksizeList>();
            try
            {
                packsizeLists = _db.Query<PacksizeList>("USP_GET_PACKSIZ_FAC_V2 ", new { P_PRODUCTID = ProductID, P_TYPE= Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return packsizeLists;
        }

        public List<TaxcountList> GetTaxcount(string MenuID, string Flag, string DepotID, string ProductID,string CustomerID,string Date)
        {
            List<TaxcountList> taxcountlist = new List<TaxcountList>();
            List<Taxcount> taxcount = new List<Taxcount>();
            try
            {
                taxcount = _db.Query<Taxcount>("USP_TAXCOUNT_V2", new { MENUID = MenuID, FLAG = Flag, DEPOTID = DepotID, PRODUCTID = ProductID, CUSTOMERID = CustomerID, DATE = Date }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in taxcount)
                {
                    taxcountlist.Add(new TaxcountList()
                    {
                        TAXCOUNT = lst.TAXCOUNT,
                        TAXNAME = lst.NAME,
                        TAXPERCENTAGE = lst.PERCENTAGE,
                        TAXRELATEDTO=lst.RELATEDTO
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return taxcountlist;
        }

        public List<FGTaxcountList> GetInvoiceTaxcount(string MenuID, string Flag, string DepotID, string ProductID, string CustomerID, string Date)
        {
            List<FGTaxcountList> taxcountlist = new List<FGTaxcountList>();
            List<FGTaxcount> taxcount = new List<FGTaxcount>();
            try
            {
                taxcount = _db.Query<FGTaxcount>("USP_INVOICE_TAXCOUNT_V2", new { MENUID = MenuID, FLAG = Flag, DEPOTID = DepotID, PRODUCTID = ProductID, CUSTOMERID = CustomerID, DATE = Date }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in taxcount)
                {
                    taxcountlist.Add(new FGTaxcountList()
                    {
                        TAXCOUNT = lst.TAXCOUNT,
                        TAXNAME = lst.NAME,
                        TAXPERCENTAGE = lst.PERCENTAGE,
                        TAXRELATEDTO = lst.RELATEDTO
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return taxcountlist;
        }

        public List<ProductList> GetCategoryProduct(string CategoryID, string DepotID,string Type)
        {
            List<ProductList> productLists = new List<ProductList>();
            try
            {
                productLists = _db.Query<ProductList>("USP_BIND_PRODUCT_CATEGORYWISE ", new { P_CATEGORY = CategoryID, P_DEPOTID = DepotID, P_TYPE= Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return productLists;
        }
        public List<BatchInfoList> GetBatchDetails(string DepotID,string ProductID,string PacksizeID,string Batch,string MRP,string Storelocation)
        {
            List<BatchInfoList> batchLists = new List<BatchInfoList>();
            List<BatchInfo> batch = new List<BatchInfo>();
            try
            {
                batch = _db.Query<BatchInfo>("USP_BATCHWISE_FACTORY_STOCK ", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = PacksizeID, P_BATCHNO = Batch, P_MRP = MRP, P_STOCKLOCATION = Storelocation }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new BatchInfoList()
                    {
                        BatchDepotID=lst.DEPOTID,
                        BatchPRODUCTID = lst.PRODUCTID,
                        BatchPRODUCTNAME = lst.PRODUCTNAME,
                        BatchASSESMENTPERCENTAGE = lst.ASSESMENTPERCENTAGE,
                        BATCHNO = lst.BATCHNO,
                        BatchMRP = lst.MRP,
                        BatchMFGDATE = lst.MFGDATE,
                        BatchEXPIRDATE = lst.EXPIRDATE,
                        BatchSTOCKLOCATION = lst.STOCKLOCATION,
                        BatchSTOCKQTY = lst.INVOICESTOCKQTY
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return batchLists;
        }

        public List<BatchInfoListRM> GetBatchDetailsRM(string DepotID, string ProductID, string PacksizeID, string Batch, string MRP, string Storelocation)
        {
            List<BatchInfoListRM> batchLists = new List<BatchInfoListRM>();
            List<BatchInfoRM> batch = new List<BatchInfoRM>();
            try
            {
                batch = _db.Query<BatchInfoRM>("USP_BATCHWISE_FACTORY_STOCK_RMPM ", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = PacksizeID, P_BATCHNO = Batch, P_MRP = MRP, P_STOCKLOCATION = Storelocation }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new BatchInfoListRM()
                    {
                        BatchDepotID = lst.DEPOTID,
                        BatchPRODUCTID = lst.PRODUCTID,
                        BatchPRODUCTNAME = lst.PRODUCTNAME,
                        BatchASSESMENTPERCENTAGE = lst.ASSESMENTPERCENTAGE,
                        BATCHNO = lst.BATCHNO,
                        BatchMRP = lst.MRP,
                        BatchMFGDATE = lst.MFGDATE,
                        BatchEXPIRDATE = lst.EXPIRDATE,
                        BatchSTOCKLOCATION = lst.STOCKLOCATION,
                        BatchSTOCKQTY = lst.INVOICESTOCKQTY
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return batchLists;
        }

        public List<StockTransferRateList> GetTransferRate(string productID, decimal mrp, string TransferDate)
        {
            List<StockTransferRateList> transferratelist = new List<StockTransferRateList>();
            List<StockTransferRate> transferrate = new List<StockTransferRate>();
            try
            {
                transferrate = _db.Query<StockTransferRate>("USP_BIND_DEPOTWISERATE", new { PRODUCTID = productID, MRP = mrp, INVOICEDATE = TransferDate }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in transferrate)
                {
                    transferratelist.Add(new StockTransferRateList()
                    {
                        hdnRATE = lst.RATE,
                        
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return transferratelist;
        }

        public List<StockTransferRateListRM> GetTransferRateRM(string productID, decimal mrp, string TransferDate)
        {
            List<StockTransferRateListRM> transferratelistRM = new List<StockTransferRateListRM>();
            List<StockTransferRateRM> transferrateRM = new List<StockTransferRateRM>();
            try
            {
                transferrateRM = _db.Query<StockTransferRateRM>("USP_BIND_DEPOTWISERATE", new { PRODUCTID = productID, MRP = mrp, INVOICEDATE = TransferDate }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in transferrateRM)
                {
                    transferratelistRM.Add(new StockTransferRateListRM()
                    {
                        hdnRATE = lst.RATE,

                    });
                }
            }
            catch (Exception ex)
            {
            }
            return transferratelistRM;
        }

        public CalculateAmtInPcsFAC GetCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment,string TaxName,string date)
        {
            CalculateAmtInPcsFAC CalculateAmtInPcs = new CalculateAmtInPcsFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_FAC_V2", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    p_ASSESMENTPERCENTAGE = Assesment,
                    P_TAXNAME = TaxName,
                    P_DATE = date,
                }, commandType: CommandType.StoredProcedure);
                var vassesments = reader.Read<Assesment>().ToList();
                var vpcsqty = reader.Read<PcsQty>().ToList();
                var vnetwght = reader.Read<Netweight>().ToList();
                var vgrosswght = reader.Read<Grossweight>().ToList();
                var vhsn = reader.Read<HSN>().ToList();
                var vhsntax = reader.Read<HSNTaxPercentage>().ToList();
                var vhsntaxid = reader.Read<HSNTaxID>().ToList();
                CalculateAmtInPcs.assesments = vassesments;
                CalculateAmtInPcs.pcsqty = vpcsqty;
                CalculateAmtInPcs.netweight = vnetwght;
                CalculateAmtInPcs.grossweight = vgrosswght;
                CalculateAmtInPcs.hsn = vhsn;
                CalculateAmtInPcs.hsnTax = vhsntax;
                CalculateAmtInPcs.hsnTaxID = vhsntaxid;
            }
            catch (Exception ex)
            {

            }
            return CalculateAmtInPcs;
        }

        public InvoiceCalculateAmtInPcsFAC GetInvoiceCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment,string TaxName, string date)
        {
            InvoiceCalculateAmtInPcsFAC CalculateAmtInPcsInvoice = new InvoiceCalculateAmtInPcsFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_FAC_V2", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    p_ASSESMENTPERCENTAGE = Assesment,
                    P_TAXNAME = TaxName,
                    P_DATE = date,
                }, commandType: CommandType.StoredProcedure);
                var vassesments = reader.Read<InvoiceAssesment>().ToList();
                var vpcsqty = reader.Read<InvoicePcsQty>().ToList();
                var vnetwght = reader.Read<InvoiceNetweight>().ToList();
                var vgrosswght = reader.Read<InvoiceGrossweight>().ToList();
                var vhsn = reader.Read<InvoiceHSN>().ToList();
                var vhsntax = reader.Read<InvoiceHSNTaxPercentage>().ToList();
                var vhsntaxid = reader.Read<InvoiceHSNTaxID>().ToList();
                CalculateAmtInPcsInvoice.assesments = vassesments;
                CalculateAmtInPcsInvoice.pcsqty = vpcsqty;
                CalculateAmtInPcsInvoice.netweight = vnetwght;
                CalculateAmtInPcsInvoice.grossweight = vgrosswght;
                CalculateAmtInPcsInvoice.hsn = vhsn;
                CalculateAmtInPcsInvoice.hsnTax = vhsntax;
                CalculateAmtInPcsInvoice.hsnTaxID = vhsntaxid;
            }
            catch (Exception ex)
            {

            }
            return CalculateAmtInPcsInvoice;
        }

        public InvoiceCalculateAmtInPcsFAC GetExportCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string TaxName, string date)
        {
            InvoiceCalculateAmtInPcsFAC CalculateAmtInPcsInvoice = new InvoiceCalculateAmtInPcsFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_EXPORT_FAC", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    p_ASSESMENTPERCENTAGE = Assesment,
                    P_TAXNAME = TaxName,
                    P_DATE = date,
                }, commandType: CommandType.StoredProcedure);
                var vassesments = reader.Read<InvoiceAssesment>().ToList();
                var vpcsqty = reader.Read<InvoicePcsQty>().ToList();
                var vnetwght = reader.Read<InvoiceNetweight>().ToList();
                var vgrosswght = reader.Read<InvoiceGrossweight>().ToList();
                var vhsn = reader.Read<InvoiceHSN>().ToList();
                var vhsntax = reader.Read<InvoiceHSNTaxPercentage>().ToList();
                var vhsntaxid = reader.Read<InvoiceHSNTaxID>().ToList();
                CalculateAmtInPcsInvoice.assesments = vassesments;
                CalculateAmtInPcsInvoice.pcsqty = vpcsqty;
                CalculateAmtInPcsInvoice.netweight = vnetwght;
                CalculateAmtInPcsInvoice.grossweight = vgrosswght;
                CalculateAmtInPcsInvoice.hsn = vhsn;
                CalculateAmtInPcsInvoice.hsnTax = vhsntax;
                CalculateAmtInPcsInvoice.hsnTaxID = vhsntaxid;
            }
            catch (Exception ex)
            {

            }
            return CalculateAmtInPcsInvoice;
        }

        public InvoiceCalculateAmtInPcsIntraFAC GetInvoiceCalculateAmtInPcsIntra(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string CgstName, string SgstName, string date)
        {
            InvoiceCalculateAmtInPcsIntraFAC CalculateIntraInvoice = new InvoiceCalculateAmtInPcsIntraFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_FAC_V3", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    p_ASSESMENTPERCENTAGE = Assesment,
                    P_CGST = CgstName,
                    P_SGST = SgstName,
                    P_DATE = date,
                }, commandType: CommandType.StoredProcedure);
                var vassesments = reader.Read<InvoiceAssesment>().ToList();
                var vpcsqty = reader.Read<InvoicePcsQty>().ToList();
                var vnetwght = reader.Read<InvoiceNetweight>().ToList();
                var vgrosswght = reader.Read<InvoiceGrossweight>().ToList();
                var vhsn = reader.Read<InvoiceHSN>().ToList();
                var vcgsttax = reader.Read<CGSTPercentage>().ToList();
                var vcgstid = reader.Read<CGST>().ToList();
                var vsgsttax = reader.Read<SGSTPercentage>().ToList();
                var vsgstid = reader.Read<SGST>().ToList();
                CalculateIntraInvoice.assesments = vassesments;
                CalculateIntraInvoice.pcsqty = vpcsqty;
                CalculateIntraInvoice.netweight = vnetwght;
                CalculateIntraInvoice.grossweight = vgrosswght;
                CalculateIntraInvoice.hsn = vhsn;
                CalculateIntraInvoice.cgstpercentage = vcgsttax;
                CalculateIntraInvoice.cgst = vcgstid;
                CalculateIntraInvoice.sgstpercentage = vsgsttax;
                CalculateIntraInvoice.sgst = vsgstid;
            }
            catch (Exception ex)
            {

            }
            return CalculateIntraInvoice;
        }


        public TradingCalculateAmtInPcsIntraFAC GetTradingCalculateAmtInPcsIntra(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string CustomerID,string CgstName, string SgstName, string TcsName, string date)
        {
            TradingCalculateAmtInPcsIntraFAC CalculateIntraInvoice = new TradingCalculateAmtInPcsIntraFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_FAC_V4", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    p_ASSESMENTPERCENTAGE = Assesment,
                    P_CUSTOMERID = CustomerID,
                    P_CGST = CgstName,
                    P_SGST = SgstName,
                    P_TCS  = TcsName,
                    P_DATE = date,
                }, commandType: CommandType.StoredProcedure);
                var vassesments = reader.Read<InvoiceAssesment>().ToList();
                var vpcsqty = reader.Read<InvoicePcsQty>().ToList();
                var vnetwght = reader.Read<InvoiceNetweight>().ToList();
                var vgrosswght = reader.Read<InvoiceGrossweight>().ToList();
                var vhsn = reader.Read<InvoiceHSN>().ToList();
                var vcgsttax = reader.Read<CGSTPercentage>().ToList();
                var vcgstid = reader.Read<CGST>().ToList();
                var vsgsttax = reader.Read<SGSTPercentage>().ToList();
                var vsgstid = reader.Read<SGST>().ToList();
                var vtcstax = reader.Read<TCSPercentage>().ToList();
                var vtcsid = reader.Read<TCS>().ToList();
                CalculateIntraInvoice.assesments = vassesments;
                CalculateIntraInvoice.pcsqty = vpcsqty;
                CalculateIntraInvoice.netweight = vnetwght;
                CalculateIntraInvoice.grossweight = vgrosswght;
                CalculateIntraInvoice.hsn = vhsn;
                CalculateIntraInvoice.cgstpercentage = vcgsttax;
                CalculateIntraInvoice.cgst = vcgstid;
                CalculateIntraInvoice.sgstpercentage = vsgsttax;
                CalculateIntraInvoice.sgst = vsgstid;
                CalculateIntraInvoice.tcspercentage = vtcstax;
                CalculateIntraInvoice.tcs = vtcsid;
            }
            catch (Exception ex)
            {

            }
            return CalculateIntraInvoice;
        }

        public QuantityDetails CalculateQuantity(string Productid, string FromPacksizeID,string CasePacksizeID, string PCSPacksizeID, decimal Qty)
        {
            QuantityDetails Quantity = new QuantityDetails();
            try
            {
                var reader = _db.QueryMultiple("USP_TOTAL_QTY", new
                {
                    P_PRODUCTID = Productid,
                    P_FROM_PACKSIZE= FromPacksizeID,
                    P_CASE_PACKSIZEID = CasePacksizeID,
                    P_PCS_PACKSIZEID= PCSPacksizeID,
                    P_QTY = Qty
                }, commandType: CommandType.StoredProcedure);
                var vcaseqty = reader.Read<TotalCase>().ToList();
                var vpcsqty = reader.Read<TotalPcs>().ToList();

                Quantity.casequantity = vcaseqty;
                Quantity.pcsquantity = vpcsqty;
            }
            catch (Exception ex)
            {

            }
            return Quantity;
        }

        public List<MessageModel> DipatchFGInsertUpdate(FactoryDispatchModel fgDispatch,string InvoiceNo,
                                                        int CreatedBy,string Finyear,string CountryID,
                                                        string CountryName,string ModuleID,string EntryFrom,string EntryType,
                                                        DataTable dtTaxDetails)
        {
            DataTable dtDetails;
            dtDetails = FGDispatchDetails(fgDispatch.DispatchDetailsFG);
            FactoryDispatchModel fgDispatchmodel = new FactoryDispatchModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_FACTORY_DESPATCH_INSERT_UPDATE_V2 ",
                                                        new
                                                        {
                                                            p_DESPATCHID = fgDispatch.DESPATCHID,
                                                            p_FLAG = fgDispatch.FLAG,
                                                            p_DESPATCHDATE = fgDispatch.DispatchDate,
                                                            p_TPUID = fgDispatch.BRID,
                                                            p_TPUNAME = fgDispatch.BRNAME,
                                                            p_WAYBILLKEY = fgDispatch.WAYBILLNO,
                                                            p_INVOICENO = InvoiceNo,
                                                            p_INVOICEDATE = fgDispatch.DispatchDate,
                                                            p_TRANSPORTERID = fgDispatch.TransporterID,
                                                            p_VEHICLENO = fgDispatch.VehichleNo,
                                                            p_MOTHERDEPOTID = fgDispatch.TODEPOTID,
                                                            p_MOTHERDEPOTNAME = fgDispatch.BRPREFIX,
                                                            p_LRGRNO = fgDispatch.LrGrNo,
                                                            p_LRGRDATE = fgDispatch.LrGrDate,
                                                            p_MODEOFTRANSPORT = fgDispatch.Tranportmode,
                                                            P_CFORMNO = "",
                                                            P_CFORMDATE = "",
                                                            P_GATEPASSNO = fgDispatch.GatepassNo,
                                                            P_GATEPASSDATE = fgDispatch.GatepassDate,
                                                            P_FORMFLAG = "N",
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_REMARKS = fgDispatch.Remarks,
                                                            p_TOTALDESPATCHVALUE = fgDispatch.NetAmt,
                                                            p_OTHERCHARGESVALUE = 0,
                                                            p_ADJUSTMENTVALUE = 0,
                                                            p_ROUNDOFFVALUE = fgDispatch.RoundOff,
                                                            p_INSURANCECOMPID = fgDispatch.ID,
                                                            p_INSURANCECOMPNAME = fgDispatch.COMPANY_NAME,
                                                            p_INSURANCENUMBER = fgDispatch.INSURANCE_NO,
                                                            p_TOTALCASE = fgDispatch.TotalCase,
                                                            p_TOTALPCS = fgDispatch.TotalPcs,
                                                            p_EXPORT = fgDispatch.DispatchMode,
                                                            p_COUNTRYID = CountryID,
                                                            p_COUNTRYNAME = CountryName,
                                                            p_ALLOCATIONID = "0",
                                                            p_ALLOCATIONNO = "NA",
                                                            P_INVOICETYPE = fgDispatch.InvoiceType,
                                                            P_SHIPPINGADDRESS = fgDispatch.ShippingAddress,
                                                            P_DELIVERYDATE = fgDispatch.DeliveryDate,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_FG_DISPATCH_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_FG_DISPATCH_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_ENTRYFROM = EntryFrom,
                                                            P_ENTRYTYPE=EntryType
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> InvoiceFGInsertUpdate(FactoryFGInvoiceModel fgInvoice,string BSID,string GroupID,
                                                        int CreatedBy, string Finyear, string SaleOrderDate,
                                                        string ModuleID, DataTable dtTaxDetails)

        {
            DataTable dtDetails;
            dtDetails = FGInvoiceDetails(fgInvoice.InvoiceDetailsFG);
            FactoryFGInvoiceModel fgInvoicehmodel = new FactoryFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_SALE_INVOICE_INSERT_UPDATE_FG_FAC_V2 ",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = fgInvoice.FGInvoiceID,
                                                            p_FLAG = fgInvoice.FLAG,
                                                            p_SALEINVOICEDATE = fgInvoice.InvoiceDate,
                                                            P_CUSTOMERID = fgInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = fgInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = fgInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = fgInvoice.TransporterID,
                                                            p_VEHICLENO = fgInvoice.VehichleNo,
                                                            p_DEPOTID = fgInvoice.BRID,
                                                            p_DEPOTNAME = fgInvoice.BRNAME,
                                                            p_BSID = BSID,
                                                            p_GROUPID = GroupID,
                                                            p_LRGRNO = fgInvoice.LrGrNo,
                                                            p_LRGRDATE = fgInvoice.LrGrDate,
                                                            p_GETPASSNO = fgInvoice.GatepassNo,
                                                            p_GETPASSDATE = fgInvoice.GatepassDate,
                                                            p_PAYMENTMODE = fgInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = fgInvoice.Tranportmode,
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_REMARKS = fgInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = fgInvoice.NetAmt,
                                                            p_OTHERCHARGESVALUE = 0,
                                                            p_ADJUSTMENTVALUE = 0,
                                                            p_ROUNDOFFVALUE = fgInvoice.RoundOff,
                                                            p_SPECIALDISC = 0,
                                                            p_TERMSID = "0",
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_FG_INVOICE_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_FG_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_DELIVERYADDRESSID = "0",
                                                            P_DELIVERYADDRESS = "",
                                                            P_ICDSNO = "",
                                                            P_ICDSDATE = "",
                                                            P_REBATESCHEMEID = "NA",
                                                            P_GROSSREBATEPERCENTAGE = 0,
                                                            P_GROSSREBATEVALUE = 0,
                                                            P_ADDFRETREBATEPERCENTAGE = 0,
                                                            P_ADDFRETREBATEVALUE = 0,
                                                            P_SALEORDERID = fgInvoice.SaleOrderID,
                                                            P_SALEORDERDATE = SaleOrderDate,
                                                            P_FORMFLAG = "0",
                                                            P_TOTALPCS = fgInvoice.TotalPcs,
                                                            P_ACTUALTOTALCASE = fgInvoice.TotalCase,
                                                            p_INVOICETYPE = fgInvoice.InvoiceType,
                                                            P_TOTALGROSSWGHT = "0",
                                                            P_ISCHALLAN = "N",
                                                            P_INSURANCECOMPID = fgInvoice.ID,
                                                            P_INSURANCECOMPNAME = fgInvoice.COMPANY_NAME,
                                                            P_INSURANCENUMBER = fgInvoice.INSURANCE_NO,
                                                            P_SHIPPINGADDRESS = fgInvoice.ShippingAddress,
                                                            P_ENTRY_FROM = "FG",
                                                            P_ENTRY_VERSION = "M",
                                                            P_TCSPERCENT = fgInvoice.TCSPercent,
                                                            P_TCSAMOUNT = fgInvoice.TCSAmt,
                                                            P_TCSNETAMOUNT = fgInvoice.TCSNetAmt,
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> InvoiceTradingInsertUpdate(FactoryFGInvoiceModel fgInvoice, string BSID, string GroupID,
                                                                int CreatedBy, string Finyear, string SaleOrderDate,
                                                                string ModuleID, DataTable dtTaxDetails)

        {
            DataTable dtDetails;
            dtDetails = FGInvoiceDetails(fgInvoice.InvoiceDetailsFG);
            FactoryFGInvoiceModel fgInvoicehmodel = new FactoryFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_TRADING_INVOICE_INSERT_UPDATE_FAC_V2 ",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = fgInvoice.FGInvoiceID,
                                                            p_FLAG = fgInvoice.FLAG,
                                                            p_SALEINVOICEDATE = fgInvoice.InvoiceDate,
                                                            P_CUSTOMERID = fgInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = fgInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = fgInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = fgInvoice.TransporterID,
                                                            p_VEHICLENO = fgInvoice.VehichleNo,
                                                            p_DEPOTID = fgInvoice.BRID,
                                                            p_DEPOTNAME = fgInvoice.BRNAME,
                                                            p_BSID = BSID,
                                                            p_GROUPID = GroupID,
                                                            p_LRGRNO = fgInvoice.LrGrNo,
                                                            p_LRGRDATE = fgInvoice.LrGrDate,
                                                            p_GETPASSNO = fgInvoice.GatepassNo,
                                                            p_GETPASSDATE = fgInvoice.GatepassDate,
                                                            p_PAYMENTMODE = fgInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = fgInvoice.Tranportmode,
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_REMARKS = fgInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = fgInvoice.NetAmt,
                                                            p_OTHERCHARGESVALUE = 0,
                                                            p_ADJUSTMENTVALUE = 0,
                                                            p_ROUNDOFFVALUE = fgInvoice.RoundOff,
                                                            p_SPECIALDISC = 0,
                                                            p_TERMSID = "0",
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_FG_INVOICE_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_FG_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_DELIVERYADDRESSID = "0",
                                                            P_DELIVERYADDRESS = "",
                                                            P_ICDSNO = "",
                                                            P_ICDSDATE = "",
                                                            P_REBATESCHEMEID = "NA",
                                                            P_GROSSREBATEPERCENTAGE = 0,
                                                            P_GROSSREBATEVALUE = 0,
                                                            P_ADDFRETREBATEPERCENTAGE = 0,
                                                            P_ADDFRETREBATEVALUE = 0,
                                                            P_SALEORDERID = fgInvoice.SaleOrderID,
                                                            P_SALEORDERDATE = SaleOrderDate,
                                                            P_FORMFLAG = "0",
                                                            P_TOTALPCS = fgInvoice.TotalPcs,
                                                            P_ACTUALTOTALCASE = fgInvoice.TotalCase,
                                                            p_INVOICETYPE = fgInvoice.InvoiceType,
                                                            P_TOTALGROSSWGHT = "0",
                                                            P_ISCHALLAN = "N",
                                                            P_INSURANCECOMPID = fgInvoice.ID,
                                                            P_INSURANCECOMPNAME = fgInvoice.COMPANY_NAME,
                                                            P_INSURANCENUMBER = fgInvoice.INSURANCE_NO,
                                                            P_SHIPPINGADDRESS = fgInvoice.ShippingAddress,
                                                            P_ENTRY_FROM = "TS",
                                                            P_ENTRY_VERSION = "M"
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> ExportGSTInsertUpdate(FactoryFGInvoiceModel fgInvoice, string BSID,
                                                                int CreatedBy, string Finyear,
                                                                string ModuleID, DataTable dtTaxDetails)

        {
            DataTable dtDetails;
            DataTable dtFree;
            dtDetails = ExportInvoiceDetails(fgInvoice.InvoiceDetailsFG);
            dtFree = FreeInvoiceDetails(fgInvoice.FreeDetailsFG);
            FactoryFGInvoiceModel fgInvoicehmodel = new FactoryFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_CRUD_EXPORT_INVOICE_GST_V2 ",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = fgInvoice.FGInvoiceID,
                                                            p_FLAG = fgInvoice.FLAG,
                                                            p_SALEINVOICEDATE = fgInvoice.InvoiceDate,
                                                            P_CUSTOMERID = fgInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = fgInvoice.CUSTOMERNAME,
                                                            p_EXPORTERSREF = fgInvoice.ExportRefNo,
                                                            p_OTHERSREF = fgInvoice.OtherRefNo,
                                                            p_VEHICLENO = fgInvoice.VesselName,
                                                            p_DEPOTID = fgInvoice.BRID,
                                                            p_DEPOTNAME = fgInvoice.BRNAME,
                                                            p_BSID = BSID,
                                                            p_LRGRNO = fgInvoice.LrGrNo,
                                                            p_LRGRDATE = fgInvoice.LrGrDate,
                                                            p_MODEOFTRANSPORT = fgInvoice.Tranportmode,
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_REMARKS = fgInvoice.Remarks, 
                                                            p_TOTALINVOICEVALUE = fgInvoice.NetAmt,
                                                            p_OTHERCHARGESVALUE = 0,
                                                            p_ADJUSTMENTVALUE = fgInvoice.AdjustmentAmount,
                                                            p_ROUNDOFFVALUE = fgInvoice.RoundOff,
                                                            P_MODULEID = ModuleID,
                                                            P_LOADINGPORTID = fgInvoice.LoadingPortID,
                                                            P_LOADINGPORTNAME = fgInvoice.LoadingPortName,
                                                            P_DISCHARGEPORTID = fgInvoice.DischargePortID,
                                                            P_DISCHARGEPORTNAME = fgInvoice.DischargePortName,
                                                            P_FINALDESTINATION = fgInvoice.FinalDestination,
                                                            P_TOTALCASE = fgInvoice.TotalCase,
                                                            P_SHIPPINGBILLNO = fgInvoice.ShippingBill,
                                                            P_CONTAINERNO = fgInvoice.ContainerNo,
                                                            P_LCNO = fgInvoice.LCNo,
                                                            P_LCDATE = fgInvoice.LCDate,
                                                            P_LCBANKNO = fgInvoice.LCBank,
                                                            P_CONSIGNEE = fgInvoice.Consignee,
                                                            P_NOTIFYPARTIES = fgInvoice.NotifyParty,
                                                            P_COUNTRYID = fgInvoice.CountryID,
                                                            P_COUNTRYNAME = fgInvoice.CountryName,
                                                            p_SHIPPINGDATE = fgInvoice.ShippingDate,
                                                            p_VOYNO = fgInvoice.VoyNo,
                                                            P_TOTALPCS = fgInvoice.TotalPcs,
                                                            P_BANKID = fgInvoice.BankID,
                                                            P_BANKNAME = fgInvoice.BankName,
                                                            P_BRANCHNAME = fgInvoice.Branch,
                                                            P_BANKADDRESS = fgInvoice.BankAddress,
                                                            P_IFSCODE = fgInvoice.IFSC,
                                                            P_SWIFTCODE = fgInvoice.SwiftCode,
                                                            P_ACCNO = fgInvoice.AccNo,
                                                            P_SALEORDERID = fgInvoice.SaleOrderID,
                                                            P_PROFORMAID = fgInvoice.ProformaOrderID,
                                                            p_INVOICETYPE = fgInvoice.InvoiceType,
                                                            P_INVOICE_SEQ_TYPE = fgInvoice.InvoiceSeqType,
                                                            P_EXCHANGE_RATE = fgInvoice.ExchangeRate,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_EXPORT_INVOICE_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_EXPORT_INVOICE_TAX_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_EXPORT_FREE_DETAILS"),
                                                            P_ENTRY_FROM = "EX",
                                                            P_ENTRY_VERSION = "M",
                                                            P_FREE_TAG = fgInvoice.FreeTag,
                                                            P_DELIVERYTO= fgInvoice.DeliveryTo,
                                                            P_AMOUNTINWORD= fgInvoice.AmountInWords
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> PackingListInsert(FactoryFGInvoiceModel fgInvoice)

        {
            DataTable dtDetails;
            dtDetails = ExportPackingDetails(fgInvoice.PackingDetails);

            FactoryFGInvoiceModel fgInvoicehmodel = new FactoryFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_PACKING_LIST_INSERT_FAC_V2 ",
                                                        new
                                                        {
                                                            P_SALEINVOICEID = fgInvoice.FGInvoiceID,
                                                            P_DESCPACKAGES= fgInvoice.DescPackages,
                                                            TempPackingDetails = dtDetails.AsTableValuedParameter("Type_EXPORT_PACKING_LIST_DETAILS")
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> DipatchRMInsertUpdate(FactoryDispatchModel fgDispatch, string InvoiceNo,
                                                        int CreatedBy, string Finyear, string ExportFlag, string CountryID,
                                                        string CountryName, string ModuleID, string EntryFrom, string EntryType,
                                                        DataTable dtTaxDetails)
        {
            DataTable dtDetails;
            dtDetails = FGDispatchDetails(fgDispatch.DispatchDetailsFG);
            FactoryDispatchModel fgDispatchmodel = new FactoryDispatchModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_FACTORY_DESPATCH_INSERT_UPDATE_RMPM_V2 ",
                                                        new
                                                        {
                                                            p_DESPATCHID = fgDispatch.DESPATCHID,
                                                            p_FLAG = fgDispatch.FLAG,
                                                            p_DESPATCHDATE = fgDispatch.DispatchDate,
                                                            p_TPUID = fgDispatch.BRID,
                                                            p_TPUNAME = fgDispatch.BRNAME,
                                                            p_WAYBILLKEY = fgDispatch.WAYBILLNO,
                                                            p_INVOICENO = InvoiceNo,
                                                            p_INVOICEDATE = fgDispatch.DispatchDate,
                                                            p_TRANSPORTERID = fgDispatch.TransporterID,
                                                            p_VEHICLENO = fgDispatch.VehichleNo,
                                                            p_MOTHERDEPOTID = fgDispatch.TODEPOTID,
                                                            p_MOTHERDEPOTNAME = fgDispatch.BRPREFIX,
                                                            p_LRGRNO = fgDispatch.LrGrNo,
                                                            p_LRGRDATE = fgDispatch.LrGrDate,
                                                            p_MODEOFTRANSPORT = fgDispatch.Tranportmode,
                                                            P_CFORMNO = "",
                                                            P_CFORMDATE = "",
                                                            P_GATEPASSNO = fgDispatch.GatepassNo,
                                                            P_GATEPASSDATE = fgDispatch.GatepassDate,
                                                            P_FORMFLAG = "N",
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_REMARKS = fgDispatch.Remarks,
                                                            p_TOTALDESPATCHVALUE = fgDispatch.NetAmt,
                                                            p_OTHERCHARGESVALUE = 0,
                                                            p_ADJUSTMENTVALUE = 0,
                                                            p_ROUNDOFFVALUE = fgDispatch.RoundOff,
                                                            p_INSURANCECOMPID = fgDispatch.ID,
                                                            p_INSURANCECOMPNAME = fgDispatch.COMPANY_NAME,
                                                            p_INSURANCENUMBER = fgDispatch.INSURANCE_NO,
                                                            p_TOTALCASE = fgDispatch.TotalCase,
                                                            p_TOTALPCS = fgDispatch.TotalPcs,
                                                            p_EXPORT = ExportFlag,
                                                            p_COUNTRYID = CountryID,
                                                            p_COUNTRYNAME = CountryName,
                                                            p_ALLOCATIONID = "0",
                                                            p_ALLOCATIONNO = "NA",
                                                            P_INVOICETYPE = fgDispatch.InvoiceType,
                                                            P_SHIPPINGADDRESS = fgDispatch.ShippingAddress,
                                                            P_DELIVERYDATE = fgDispatch.DeliveryDate,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_FG_DISPATCH_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_FG_DISPATCH_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_ENTRYFROM = EntryFrom,
                                                            P_ENTRYTYPE = EntryType
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public DataTable FGDispatchDetails(List<DispatchDetailsFG> DispatchDetailsFG)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKINGSIZEID", typeof(string));
            dt.Columns.Add("PACKINGSIZENAME", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(decimal));
            dt.Columns.Add("TOTALASSESMENTVALUE", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("GROSSWEIGHT", typeof(string));
            dt.Columns.Add("STORELOCATIONID", typeof(string));

            int count = 1;
            foreach (var item in DispatchDetailsFG)
            {
                dt.Rows.Add(item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKINGSIZEID,
                            item.PACKINGSIZENAME,
                            item.MRP,
                            item.QTY,
                            item.RATE,
                            item.BATCHNO,
                            item.AMOUNT,
                            item.ASSESMENTPERCENTAGE,
                            item.TOTALASSESMENTVALUE,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.GROSSWEIGHT,
                            item.STORELOCATIONID
                            );
                count++;
            }
            return dt;
        }

        public DataTable FGInvoiceDetails(List<InvoiceDetailsFG> InvoiceDetailsFG)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKINGSIZEID", typeof(string));
            dt.Columns.Add("PACKINGSIZENAME", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("QTYPCS", typeof(decimal));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(decimal));
            dt.Columns.Add("TOTALASSESMENTVALUE", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("GROSSWEIGHT", typeof(string));
            dt.Columns.Add("STORELOCATIONID", typeof(string));
            dt.Columns.Add("STORELOCATIONNAME", typeof(string));

            int count = 1;
            foreach (var item in InvoiceDetailsFG)
            {
                dt.Rows.Add(item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKINGSIZEID,
                            item.PACKINGSIZENAME,
                            item.MRP,
                            item.RATE,
                            item.QTY,
                            item.QTYPCS,
                            item.AMOUNT,
                            item.BATCHNO,
                            item.ASSESMENTPERCENTAGE,
                            item.TOTALASSESMENTVALUE,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.GROSSWEIGHT,
                            item.STORELOCATIONID,
                            item.STORELOCATIONNAME
                            );
                count++;
            }
            return dt;
        }

        public DataTable ExportInvoiceDetails(List<InvoiceDetailsFG> InvoiceDetailsFG)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKINGSIZEID", typeof(string));
            dt.Columns.Add("PACKINGSIZENAME", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("QTYPCS", typeof(decimal));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(decimal));
            dt.Columns.Add("TOTALASSESMENTVALUE", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("GROSSWEIGHT", typeof(string));
            dt.Columns.Add("STORELOCATIONID", typeof(string));
            dt.Columns.Add("STORELOCATIONNAME", typeof(string));
            dt.Columns.Add("NSR", typeof(decimal));
            dt.Columns.Add("RATEDISC", typeof(decimal));
            dt.Columns.Add("TAG", typeof(string));

            int count = 1;
            foreach (var item in InvoiceDetailsFG)
            {
                dt.Rows.Add(item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKINGSIZEID,
                            item.PACKINGSIZENAME,
                            item.MRP,
                            item.RATE,
                            item.QTY,
                            item.QTYPCS,
                            item.AMOUNT,
                            item.BATCHNO,
                            item.ASSESMENTPERCENTAGE,
                            item.TOTALASSESMENTVALUE,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.GROSSWEIGHT,
                            item.STORELOCATIONID,
                            item.STORELOCATIONNAME,
                            item.NSR,
                            item.RATEDISC,
                            item.TAG
                            );
                count++;
            }
            return dt;
        }

        public DataTable ExportPackingDetails(List<PackingDetails> PackingDetails)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SALEINVOICEID", typeof(string));
            dt.Columns.Add("SALEORDERID", typeof(string));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("STARTPOSITION", typeof(decimal));
            dt.Columns.Add("ENDPOSITION", typeof(decimal));
            dt.Columns.Add("GROSSWEIGHT", typeof(string));
            dt.Columns.Add("NETWEIGHT", typeof(string));

            int count = 1;
            foreach (var item in PackingDetails)
            {
                dt.Rows.Add(item.SALEINVOICEID,
                            item.SALEORDERID,
                            item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.BATCHNO,
                            item.QTY,
                            item.STARTPOSITION,
                            item.ENDPOSITION,
                            item.GROSSWEIGHT,
                            item.NETWEIGHT
                            );
                count++;
            }
            return dt;
        }

        public DataTable FreeInvoiceDetails(List<FreeDetailsFG> InvoiceDetailsFG)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SCHEME_PRODUCT_ID", typeof(string));
            dt.Columns.Add("SCHEME_PRODUCT_NAME", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("SCHEME_QTY", typeof(decimal));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("BRATE", typeof(decimal));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("SCHEME_PRODUCT_BATCHNO", typeof(string));
            dt.Columns.Add("STORELOCATIONID", typeof(string));

            int count = 1;
            foreach (var item in InvoiceDetailsFG)
            {
                dt.Rows.Add(item.SCHEME_PRODUCT_ID,
                            item.SCHEME_PRODUCT_NAME,
                            item.QTY,
                            item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.SCHEME_QTY,
                            item.MRP,
                            item.BRATE,
                            item.AMOUNT,
                            item.BATCHNO,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.SCHEME_PRODUCT_BATCHNO,
                            item.STORELOCATIONID
                            );
                count++;
            }
            return dt;
        }

        public List<DispatchedFGList> BindFgDispatchGrid(string FromDate, string ToDate,string Finyear, string CheckerFlag, string userID, string DepotID,string Type)
        {
            List<DispatchedFGList> dispatchfgGrid = new List<DispatchedFGList>();
            try
            {
                dispatchfgGrid = _db.Query<DispatchedFGList>("USP_DISPATCH_SEARCH ", new { P_FROMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear, P_CHECKERFLAG = CheckerFlag, P_USERID = userID, P_DEPOTID = DepotID, P_TYPE= Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dispatchfgGrid;
        }

        public List<FGInvoiceList> BindFgInvoiceGrid(string FromDate, string ToDate, string Finyear, string bsid, string DepotID, string CheckerFlag, string userID,string Type)
        {
            List<FGInvoiceList> invoicefgGrid = new List<FGInvoiceList>();
            try
            {
                invoicefgGrid = _db.Query<FGInvoiceList>("USP_BIND_SALEINVOICE_FG ", new { P_FROMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear, P_BSID = bsid, P_DEPOTID = DepotID, P_CHECKERFLAG = CheckerFlag, P_USERID = userID, P_ISCHALLAN=Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoicefgGrid;
        }

        public List<FGInvoiceList> BindTradingInvoiceGrid(string FromDate, string ToDate, string Finyear, string bsid, string DepotID, string CheckerFlag, string userID, string Type)
        {
            List<FGInvoiceList> invoicetradingGrid = new List<FGInvoiceList>();
            try
            {
                invoicetradingGrid = _db.Query<FGInvoiceList>("USP_BIND_TRADING_SALE_INVOICE_FACTORY", new { P_FROMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear, P_BSID = bsid, P_DEPOTID = DepotID, P_CHECKERFLAG = CheckerFlag, P_USERID = userID, P_ISCHALLAN = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoicetradingGrid;
        }

        public List<ExportInvoiceList> BindExportInvoiceGrid(string FromDate, string ToDate, string Finyear, string bsid, string DepotID, string userID, string Type)
        {
            List<ExportInvoiceList> exportGrid = new List<ExportInvoiceList>();
            try
            {
                exportGrid = _db.Query<ExportInvoiceList>("USP_BIND_EXPORT_INVOICE_FAC", new { P_FROMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear, P_BSID = bsid, P_DEPOTID = DepotID, P_USERID = userID, P_INVOICETYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exportGrid;
        }

        public List<MessageModel> GetFGDispatchStatus(string DispatchID, string ModuleID, string Type)
        {
            List<MessageModel> dispatchfgStatus = new List<MessageModel>();
            try
            {
                dispatchfgStatus = _db.Query<MessageModel>("USP_DISPATCH_STATUS_V2 ", new { P_DISPATCHID = DispatchID, P_MODULE = ModuleID, P_TYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dispatchfgStatus;
        }

        public DispatchEditFG DipatchFGEdit(string DispatchID)
        {
            DispatchEditFG EditDispatch = new DispatchEditFG();
            try
            {
                var reader = _db.QueryMultiple("USP_FACTORY_DESPATCH_DETAILS_V2", new {p_DESPATCHID = DispatchID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<DispatchHeaderEditFG>().ToList();
                var vdetails = reader.Read<DispatchDetailsEditFG>().ToList();
                var vtaxcount = reader.Read<DispatchTaxcountEditFG>().ToList();
                var vtaxdetails = reader.Read<DispatchTaxEditFG>().ToList();
                var vfooter = reader.Read<DispatchFooterEditFG>().ToList();

                EditDispatch.DispatchHeaderEditFG = vheader;
                EditDispatch.DispatchDetailsEditFG = vdetails;
                EditDispatch.DispatchTaxcountEditFG = vtaxcount;
                EditDispatch.DispatchTaxEditFG = vtaxdetails;
                EditDispatch.DispatchFooterEditFG = vfooter;
            }
            catch (Exception ex)
            {

            }
            return EditDispatch;
        }

        public List<TaxOnEdit> GetTaxOnEdit(string DispathID,string TaxID,string ProductID,string BatchNo)
        {
            List<TaxOnEdit> Tax = new List<TaxOnEdit>();
            try
            {
                Tax = _db.Query<TaxOnEdit>("USP_GET_HSN_TAX_ON_EDIT ", new { P_INVOICEID = DispathID, P_TaxID = TaxID, P_ProductID = ProductID, P_Batchno = BatchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return Tax;
        }

        public List<TaxOnEdit> GetInvoiceTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            List<TaxOnEdit> Tax = new List<TaxOnEdit>();
            try
            {
                Tax = _db.Query<TaxOnEdit>("USP_GET_HSN_TAX_ON_EDIT_INVOICE ", new { P_INVOICEID = InvoiceID, P_TaxID = TaxID, P_ProductID = ProductID, P_Batchno = BatchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return Tax;
        }

        public List<MessageModel> DipatchFGDelete(string DispatchID)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response= _db.Query<MessageModel>("USP_STOCK_FACTORY_DESPATCH_DELETE_V2 ", new{ p_DESPATCHID = DispatchID },commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<MessageModel> InvoiceFGDelete(string InvoiceID)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_SALE_INVOICE_DELETE_MM_V2 ", new { p_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<BackDateChecking> GetBackDateChecking(string MenuID)
        {
            List<BackDateChecking> response = new List<BackDateChecking>();
            try
            {
                response = _db.Query<BackDateChecking>("USP_BACKDATE_CHECKING_FAC ", new { P_MENUID = MenuID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<EntryLockChecking> GetEntryLockChecking(string EntryDate,string Finyear,string StartOfFinyear,string EndOfFinyear)
        {
            List<EntryLockChecking> response = new List<EntryLockChecking>();
            try
            {
                response = _db.Query<EntryLockChecking>("USP_ENTRY_LOCK_CHECKING_FAC ", new { P_ENTRYDATE = EntryDate, P_FINYEAR = Finyear, P_FirstFinYearDt = StartOfFinyear, P_SecondFinYearDt = EndOfFinyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<OrderDetailsList> GetOrderQtyDetails(string SaleInvoiceID,string CustomerID, string SaleOrderID, string ProductID,string DepotID,string PacksizeID,string StorelocationID)
        {
            List<OrderDetailsList> orderdetailslist = new List<OrderDetailsList>();
            try
            {
                orderdetailslist = _db.Query<OrderDetailsList>("USP_ORDER_DESPATCH_QTY_FAC_FG_V2 ", new { P_SALEINVOICEID= SaleInvoiceID, P_CUSTOMERID = CustomerID, P_SALEORDERID = SaleOrderID, P_PRODUCTID = ProductID, P_DEPOTID = DepotID, P_PACKSIZE= PacksizeID, P_STOCKLOCATION = StorelocationID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return orderdetailslist;
        }

        public List<OrderDetailsList> GetExportOrderQtyDetails(string SaleInvoiceID, string CustomerID, string SaleOrderID, string ProductID, string DepotID, string PacksizeID, string BSID,string StorelocationID)
        {
            List<OrderDetailsList> orderdetailslist = new List<OrderDetailsList>();
            try
            {
                orderdetailslist = _db.Query<OrderDetailsList>("USP_ORDER_DESPATCH_QTY_FAC_EXPORT_V2 ", new { P_SALEINVOICEID = SaleInvoiceID, P_CUSTOMERID = CustomerID, P_SALEORDERID = SaleOrderID, P_PRODUCTID = ProductID, P_DEPOTID = DepotID, P_PACKSIZE = PacksizeID, P_BSID= BSID, P_STOCKLOCATION = StorelocationID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return orderdetailslist;
        }

        public List<TradingOrderDetailsList> GetTradingOrderQtyDetails(string CustomerID, string SaleOrderID, string ProductID, string DepotID, string StorelocationID)
        {
            List<TradingOrderDetailsList> tradingorderdetailslist = new List<TradingOrderDetailsList>();
            try
            {
                tradingorderdetailslist = _db.Query<TradingOrderDetailsList>("USP_ORDER_DESPATCH_QTY_MM ", new { P_CUSTOMERID = CustomerID, P_SALEORDERID = SaleOrderID, P_PRODUCTID = ProductID, P_DEPOTID = DepotID, P_STOCKLOCATION = StorelocationID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return tradingorderdetailslist;
        }

        public List<InvoiceConvertedQty> GetConvertedQty(string ProductID, string PacksizeFrom, string PacksizeTo, decimal Qty)
        {
            List<InvoiceConvertedQty> orderdetailslist = new List<InvoiceConvertedQty>();
            try
            {
                orderdetailslist = _db.Query<InvoiceConvertedQty>("USP_CONVERSION_FAC ", new { P_PRODUCTID = ProductID, P_PACKSIZEFROM = PacksizeFrom, P_PACKSIZETO = PacksizeTo, P_QTY = Qty }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return orderdetailslist;
        }

        public InvoiceEditFG InvoiceFGEdit(string InvoiceID)
        {
            InvoiceEditFG EditInvoice = new InvoiceEditFG();
            try
            {
                var reader = _db.QueryMultiple("USP_SALE_INVOICE_DETAILS_MM_V2", new { p_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<InvoiceHeaderEditFG>().ToList();
                var vdetails = reader.Read<InvoiceDetailsEditFG>().ToList();
                var vtaxcount = reader.Read<InvoiceTaxcountEditFG>().ToList();
                var vfooter = reader.Read<InvoiceFooterEditFG>().ToList();
                var vtaxdetails = reader.Read<InvoiceTaxEditFG>().ToList();
                var vorderdetails = reader.Read<InvoiceOrderDetailsEditFG>().ToList();
                var vorderheader = reader.Read<InvoiceOrderHeaderEditFG>().ToList();


                EditInvoice.InvoiceHeaderEditFG = vheader;
                EditInvoice.InvoiceDetailsEditFG = vdetails;
                EditInvoice.InvoiceTaxcountEditFG = vtaxcount;
                EditInvoice.InvoiceFooterEditFG = vfooter;
                EditInvoice.InvoiceTaxEditFG = vtaxdetails;
                EditInvoice.InvoiceOrderDetailsEditFG = vorderdetails;
                EditInvoice.InvoiceOrderHeaderEditFG = vorderheader;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return EditInvoice;
        }

        public InvoiceEditTrading InvoiceTradingEdit(string InvoiceID)
        {
            InvoiceEditTrading EditInvoice = new InvoiceEditTrading();
            try
            {
                var reader = _db.QueryMultiple("USP_TRADING_SALE_INVOICE_DETAILS_MM_V2", new { p_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<InvoiceHeaderEditFG>().ToList();
                var vdetails = reader.Read<InvoiceDetailsEditTrading>().ToList();
                var vtaxcount = reader.Read<InvoiceTaxcountEditFG>().ToList();
                var vfooter = reader.Read<InvoiceFooterEditFG>().ToList();
                var vtaxdetails = reader.Read<InvoiceTaxEditFG>().ToList();
                var vorderdetails = reader.Read<InvoiceOrderDetailsEditFG>().ToList();
                var vorderheader = reader.Read<InvoiceOrderHeaderEditFG>().ToList();


                EditInvoice.InvoiceHeaderEditFG = vheader;
                EditInvoice.InvoiceDetailsEditTrading = vdetails;
                EditInvoice.InvoiceTaxcountEditFG = vtaxcount;
                EditInvoice.InvoiceFooterEditFG = vfooter;
                EditInvoice.InvoiceTaxEditFG = vtaxdetails;
                EditInvoice.InvoiceOrderDetailsEditFG = vorderdetails;
                EditInvoice.InvoiceOrderHeaderEditFG = vorderheader;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return EditInvoice;
        }

        public ExportEdit ExportEdit(string InvoiceID)
        {
            ExportEdit EditInvoice = new ExportEdit();
            try
            {
                var reader = _db.QueryMultiple("USP_EXPORT_INVOICE_DETAILS_MM_V2", new { p_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<ExportHeaderEdit>().ToList();
                var vdetails = reader.Read<ExportDetailsEdit>().ToList();
                var vtaxcount = reader.Read<ExportTaxcountEdit>().ToList();
                var vtaxdetails = reader.Read<ExportTaxEdit>().ToList();
                var vfooter = reader.Read<ExportFooterEdit>().ToList();
                var vfree = reader.Read<ExportFreeEdit>().ToList();
                var vorderheader = reader.Read<ExportOrderHeaderEdit>().ToList();
                var vproformaheader = reader.Read<ExportProformaHeaderEdit>().ToList();


                EditInvoice.ExportHeaderEdit = vheader;
                EditInvoice.ExportDetailsEdit = vdetails;
                EditInvoice.ExportTaxcountEdit = vtaxcount;
                EditInvoice.ExportTaxEdit = vtaxdetails;
                EditInvoice.ExportFooterEdit = vfooter;
                EditInvoice.ExportFreeEdit = vfree;
                EditInvoice.ExportOrderHeaderEdit = vorderheader;
                EditInvoice.ExportProformaHeaderEdit = vproformaheader;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return EditInvoice;
        }

        public List<Country> GetCountry()
        {
            List<Country> countryLists = new List<Country>();
            try
            {
                countryLists = _db.Query<Country>("USP_GET_COUNTRY_V2 ", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return countryLists;
        }

        public List<LoadingPort> GetLoadingPort()
        {
            List<LoadingPort> loadingportLists = new List<LoadingPort>();
            try
            {
                loadingportLists = _db.Query<LoadingPort>("USP_LOADING_PORT_V2 ", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return loadingportLists;
        }

        public List<DischargePort> GetDischargePort()
        {
            List<DischargePort> dischargeportLists = new List<DischargePort>();
            try
            {
                dischargeportLists = _db.Query<DischargePort>("USP_DISCHARGE_PORT_V2 ", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return dischargeportLists;
        }

        public List<ExportSaleOrder> GetExportSaleorder(string CountryID)
        {
            List<ExportSaleOrder> saleorderLists = new List<ExportSaleOrder>();
            try
            {
                saleorderLists = _db.Query<ExportSaleOrder>("USP_EXPORT_SALEORDER_DETAILS ", new { p_COUNTRYID = CountryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return saleorderLists;
        }

        public List<ExportCustomer> GetExportCustomer(string SaleorderID)
        {
            List<ExportCustomer> customerLists = new List<ExportCustomer>();
            try
            {
                customerLists = _db.Query<ExportCustomer>("USP_GET_EXPORT_CUSTOMER ", new { P_SALEORDERID = SaleorderID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return customerLists;
        }

        public List<Proforma> GetProforma(string SaleorderID)
        {
            List<Proforma> proformaLists = new List<Proforma>();
            try
            {
                proformaLists = _db.Query<Proforma>("USP_GET_EXPORT_PROFORMA ", new { P_SALEORDERID = SaleorderID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return proformaLists;
        }
        public List<ProformaDetailsList> GetProformaDetails(string ProformaID)
        {
            List<ProformaDetailsList> proformadetailslist = new List<ProformaDetailsList>();
            try
            {
                proformadetailslist = _db.Query<ProformaDetailsList>("USP_PROFORMA_INVOICE_RECORD ", new { p_INVOICEID = ProformaID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return proformadetailslist;
        }

        public List<LCDetailsList> GetLCDetails(string SaleorderID, string ProformaID, string CustomerID)
        {
            List<LCDetailsList> lcdetailslist = new List<LCDetailsList>();
            try
            {
                lcdetailslist = _db.Query<LCDetailsList>("USP_GET_LC_DETAILS ", new { P_SALEORDERID = SaleorderID, P_PROFORMAID = ProformaID, P_CUSTOMERID = CustomerID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return lcdetailslist;
        }

        public List<ProformaTax> GetProformaTax(string ProductID, string TaxName, string InvoiceDate)
        {
            List<ProformaTax> proformatax = new List<ProformaTax>();
            try
            {
                proformatax = _db.Query<ProformaTax>("USP_GET_EXPORT_HSN_TAX ", new { p_PRODUCTID = ProductID, P_TAXNAME = TaxName, P_DATE = InvoiceDate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return proformatax;
        }

        public List<ExportProductList> GetExportProduct(string BSID,string SaleinvoiceID)
        {
            List<ExportProductList> exportproductLists = new List<ExportProductList>();
            try
            {
                exportproductLists = _db.Query<ExportProductList>("USP_GET_EXPORT_PRODUCT ", new { P_BSID = BSID, P_SALEINVOICEID = SaleinvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return exportproductLists;
        }

        public ExportProformaAmountDetails GetProformaAmountDetails(string ProformaID)
        {
            ExportProformaAmountDetails amountdetails = new ExportProformaAmountDetails();
            try
            {
                var reader = _db.QueryMultiple("USP_GET_PROFORMA_VALUE", new{P_PROFORMAID = ProformaID}, commandType: CommandType.StoredProcedure);
                var vproformavalue = reader.Read<ProformaAmount>().ToList();
                var vadjustmentvalue = reader.Read<AdjustmentAmount>().ToList();

                amountdetails.proformaamount = vproformavalue;
                amountdetails.adjustmentamount = vadjustmentvalue;
               
            }
            catch (Exception ex)
            {

            }
            return amountdetails;
        }

        public List<ProformaInvoiceList> BindProformaList(string SaleorderID, string DepotID, string ProformaID, string CustomerID, decimal ExchangeRate)
        {
            List<ProformaInvoiceList> proformalist = new List<ProformaInvoiceList>();
            try
            {
                proformalist = _db.Query<ProformaInvoiceList>("USP_EXPORT_PRODUCT_DETAILS_FAC_V2 ", new { P_SALEORDERID = SaleorderID, P_DEPOTID = DepotID, P_PROFORMAINVOICEID = ProformaID, P_CUSTOMERID = CustomerID, P_EXCHANGERATE = ExchangeRate}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return proformalist;
        }

        public List<PackingListDetails> BindPackingList(string InvoiceID)
        {
            List<PackingListDetails> packinglist = new List<PackingListDetails>();
            try
            {
                packinglist = _db.Query<PackingListDetails>("USP_PACKINGLIST_DETAILS_EXPORT_FAC_V2 ", new { P_SALEINVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return packinglist;
        }

        public List<CustomerNetSaleAmt> GetNetSale(string CustomerID, string DepotID, string Finyear, string InvoiceID)
        {
            List<CustomerNetSaleAmt> netsale = new List<CustomerNetSaleAmt>();
            try
            {
                netsale = _db.Query<CustomerNetSaleAmt>("USP_NET_SALE_AMOUNT_FAC", new { p_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_FINYEAR = Finyear, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return netsale;
        }

        public List<TradingStorelocation> GetTradingStorelocation()
        {
            List<TradingStorelocation> storelocation = new List<TradingStorelocation>();
            try
            {
                storelocation = _db.Query<TradingStorelocation>("USP_TRADING_STORELOCATION", new {}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return storelocation;
        }

    }
}
