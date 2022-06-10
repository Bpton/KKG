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
    public class DepotInvoiceContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

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

        public List<GTCustomerList> GetGTCustomer(string BSID, string GroupID,string DepotID,string InvoiceID)
        {
            List<GTCustomerList> gtcustomer = new List<GTCustomerList>();
            
            try
            {
                gtcustomer = _db.Query<GTCustomerList>("USP_GT_CUSTOMER_V2 ", new { P_BSID = BSID, P_GROUPID = GroupID, P_DEPOTID = DepotID, P_INVOICEID= InvoiceID }, commandType: CommandType.StoredProcedure).ToList();

                
            }
            catch (Exception ex)
            {
            }
            return gtcustomer;
        }

        public List<GTCustomerList> GetMTCustomer(string BSID, string DepotID, string InvoiceID)
        {
            List<GTCustomerList> gtcustomer = new List<GTCustomerList>();

            try
            {
                gtcustomer = _db.Query<GTCustomerList>("USP_MT_CUSTOMER_V2 ", new { P_BSID = BSID, P_DEPOTID = DepotID, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return gtcustomer;
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

        public List<GTProductCategory> GetCategory(string CustomerID)
        {
            List<GTProductCategory> category = new List<GTProductCategory>();
            try
            {
                category = _db.Query<GTProductCategory>("USP_CATAGORY_GT", new { P_CUSTOMERID = CustomerID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return category;
        }

        public List<GTProductCategory> GetEcommCategory()
        {
            List<GTProductCategory> category = new List<GTProductCategory>();
            try
            {
                category = _db.Query<GTProductCategory>("USP_CATAGORY_ECOMMERCE", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return category;
        }

        public List<GTProductList> GetGTProduct(string CustomerID,string DepotID, string BSID, string CategoryID, string InvoiceID)
        {
            List<GTProductList> productLists = new List<GTProductList>();
            try
            {
                productLists = _db.Query<GTProductList>("USP_GT_PRODUCT ", new { P_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_BSID = BSID, P_CATEGORYID = CategoryID, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return productLists;
        }

        public List<GTProductList> GetECommProduct(string BSID,string CategoryID,string InvoiceID )
        {
            List<GTProductList> productLists = new List<GTProductList>();
            try
            {
                productLists = _db.Query<GTProductList>("USP_ECOMM_PRODUCT ", new { P_BSID = BSID, P_CATEGORYID = CategoryID, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return productLists;
        }

        public List<MTProductList> GetMTProduct(string SaleorderID)
        {
            List<MTProductList> MTproductLists = new List<MTProductList>();
            try
            {
                MTproductLists = _db.Query<MTProductList>("USP_GET_MT_PRODUCT_V2 ", new { P_SALEORDERID = SaleorderID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return MTproductLists;
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

        public List<GTPacksizeList> GetPacksize()
        {
            List<GTPacksizeList> packsizeLists = new List<GTPacksizeList>();
            try
            {
                packsizeLists = _db.Query<GTPacksizeList>("USP_GT_PACKSIZE_V2 ",commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return packsizeLists;
        }

        public List<SaleorderList> GetSaleOrder(string CustomerID)
        {
            List<SaleorderList> saleorderLists = new List<SaleorderList>();
            try
            {
                saleorderLists = _db.Query<SaleorderList>("USP_GET_SALEORDER_NO_V2 ",  new { P_CUSTOMERID = CustomerID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return saleorderLists;
        }

        public List<SaleorderList> GetCPCSaleOrder(string CustomerID)
        {
            List<SaleorderList> saleorderLists = new List<SaleorderList>();
            try
            {
                saleorderLists = _db.Query<SaleorderList>("USP_GET_CPC_SALEORDER_NO_V2 ", new { P_CUSTOMERID = CustomerID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return saleorderLists;
        }

        public List<GroupList> GetGroup(string BSID,string UserID,string CheckerFlag)
        {
            List<GroupList> groupLists = new List<GroupList>();
            try
            {
                groupLists = _db.Query<GroupList>("USP_GROUP_V2 ", new { P_BSID = BSID, P_USERID = UserID, P_CHECKER_FLAG = CheckerFlag }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return groupLists;
        }

        public List<SourceDepotList> GetDepot(string UserID)
        {
            List<SourceDepotList> depotLists = new List<SourceDepotList>();
            try
            {
                depotLists = _db.Query<SourceDepotList>("USP_GET_DEPOT_BASED_ON_USER_V2 ", new { P_USERID = UserID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return depotLists;
        }

        public List<InvoiceTaxcountList> GetTaxcount(string MenuID, string Flag, string DepotID, string ProductID,string CustomerID,string Date)
        {
            List<InvoiceTaxcountList> taxcountlist = new List<InvoiceTaxcountList>();
            List<InvoiceTaxcount> taxcount = new List<InvoiceTaxcount>();
            try
            {
                taxcount = _db.Query<InvoiceTaxcount>("USP_INVOICE_TAXCOUNT_V2", new { MENUID = MenuID, FLAG = Flag, DEPOTID = DepotID, PRODUCTID = ProductID, CUSTOMERID = CustomerID, DATE = Date }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in taxcount)
                {
                    taxcountlist.Add(new InvoiceTaxcountList()
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

        public List<DepotBatchInfoList> GetBatchDetails(string DepotID,string ProductID,string PacksizeID,string Batch,string MRP,string Storelocation)
        {
            List<DepotBatchInfoList> batchLists = new List<DepotBatchInfoList>();
            List<DepotBatchInfo> batch = new List<DepotBatchInfo>();
            try
            {
                batch = _db.Query<DepotBatchInfo>("SP_BATCHWISE_DEPOT_STOCK ", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = PacksizeID, P_BATCHNO = Batch, P_MRP = MRP, P_STOCKLOCATION = Storelocation }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new DepotBatchInfoList()
                    {
                        BatchDepotID=lst.DEPOTID,
                        BatchPRODUCTID = lst.PRODUCTID,
                        BatchPRODUCTNAME = lst.PRODUCTNAME,
                        BatchASSESMENTPERCENTAGE = lst.ASSESMENTPERCENTAGE,
                        BATCHNO = lst.BATCHNO,
                        BatchMRP = lst.MRP,
                        BatchMFGDATE = lst.MFGDATE,
                        BatchEXPIRDATE = lst.EXPIRDATE,
                        BatchSTORELOCATION = lst.STORELOCATIONID,
                        BatchSTOCKQTY = lst.INVOICESTOCKQTY
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return batchLists;
        }

        public List<DepotBatchInfoList> GetRMPMBatchDetails(string DepotID, string ProductID, string Batch, string Storelocation)
        {
            List<DepotBatchInfoList> batchLists = new List<DepotBatchInfoList>();
            List<DepotBatchInfo> batch = new List<DepotBatchInfo>();
            try
            {
                batch = _db.Query<DepotBatchInfo>("USP_RM_PM_DEPOT_STOCK_BATCH ", new { DEPOTID = DepotID, PRODUCTID = ProductID, P_BATCHNO = Batch, P_STORELOCATION = Storelocation }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new DepotBatchInfoList()
                    {
                        BatchDepotID = lst.DEPOTID,
                        BatchPRODUCTID = lst.PRODUCTID,
                        BatchPRODUCTNAME = lst.PRODUCTNAME,
                        BatchASSESMENTPERCENTAGE = lst.ASSESMENTPERCENTAGE,
                        BATCHNO = lst.BATCHNO,
                        BatchMRP = lst.MRP,
                        BatchMFGDATE = lst.MFGDATE,
                        BatchEXPIRDATE = lst.EXPIRDATE,
                        BatchSTORELOCATION = lst.STORELOCATIONID,
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

        public TaxDetailsDepot GetTaxDetails(string Productid, string FromPacksizeID, string ToPacksizeID,string QtyPcs, string CGST, string SGST, string IGST, string Date)
        {
            TaxDetailsDepot taxdetails = new TaxDetailsDepot();
            try
            {
                var reader = _db.QueryMultiple("USP_GET_TAX_DETAILS_DEPOT_V2", new
                {
                    P_PRODUCTID = Productid,
                    P_FROMPACKSIZE = FromPacksizeID,
                    P_TOPACKSIZE= ToPacksizeID,
                    P_QTY_PCS = Convert.ToDecimal(QtyPcs),
                    P_CGST = CGST,
                    P_SGST = SGST,
                    P_IGST = IGST,
                    P_DATE = Date,
                }, commandType: CommandType.StoredProcedure);
                var vcaseqty = reader.Read<GTInvoiceCaseQuantity>().ToList();
                var vnetwght = reader.Read<GTInvoiceNetweight>().ToList();
                var vcgst = reader.Read<GTInoiceCGSTPercentage>().ToList();
                var vcgstid = reader.Read<GTInvoiceCGST>().ToList();
                var vsgst = reader.Read<GTInvoiceSGSTPercentage>().ToList();
                var vsgstid = reader.Read<GTInvoiceSGST>().ToList();
                var vigst = reader.Read<GTInvoiceIGSTPercentage>().ToList();
                var vigstid = reader.Read<GTInvoiceIGST>().ToList();
                taxdetails.casequantity = vcaseqty;
                taxdetails.netweight = vnetwght;
                taxdetails.cgstpercentage = vcgst;
                taxdetails.cgst = vcgstid;
                taxdetails.sgstpercentage = vsgst;
                taxdetails.sgst = vsgstid;
                taxdetails.igstpercentage = vigst;
                taxdetails.igst = vigstid;
            }
            catch (Exception ex)
            {

            }
            return taxdetails;
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

        
        public List<MessageModel> GtInvoiceInsertUpdate(DepotFGInvoiceModel gtInvoice,string BSID,
                                                        string ModuleID)

        {
            DataTable dtDetails;
            DataTable dtFree;
            DataTable dtTax;
            dtDetails = GTInvoiceDetails(gtInvoice.InvoiceDetailsGT);
            dtFree = GTFreeDetails(gtInvoice.FreeDetailsGT);
            dtTax = GTTaxDetails(gtInvoice.TaxDetailsGT);
            DepotFGInvoiceModel gtInvoicehmodel = new DepotFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_GT_INVOICE_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = gtInvoice.FGInvoiceID,
                                                            p_FLAG = gtInvoice.FLAG,
                                                            p_SALEINVOICEDATE = gtInvoice.InvoiceDate,
                                                            P_CUSTOMERID = gtInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = gtInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = gtInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = gtInvoice.TransporterID,
                                                            p_VEHICLENO = gtInvoice.VehichleNo,
                                                            p_DEPOTID = gtInvoice.BRID,
                                                            p_DEPOTNAME = gtInvoice.BRNAME,
                                                            p_LRGRNO = gtInvoice.LrGrNo,
                                                            p_LRGRDATE = gtInvoice.LrGrDate,
                                                            p_GETPASSNO = gtInvoice.GatepassNo,
                                                            p_GETPASSDATE = gtInvoice.GatepassDate,
                                                            p_PAYMENTMODE = gtInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = gtInvoice.Tranportmode,
                                                            p_CREATEDBY = gtInvoice.UserID,
                                                            p_FINYEAR = gtInvoice.FinYear,
                                                            p_REMARKS = gtInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = Convert.ToDecimal(gtInvoice.NetAmt),
                                                            p_OTHERCHARGESVALUE = Convert.ToDecimal("0"),
                                                            p_ADJUSTMENTVALUE = Convert.ToDecimal("0"),
                                                            p_ROUNDOFFVALUE = Convert.ToDecimal(gtInvoice.RoundOff),
                                                            p_SPECIALDISC = Convert.ToDecimal(gtInvoice.TotalFreeAmt),
                                                            p_TERMSID = "0",
                                                            P_BSID = BSID,
                                                            P_BSNAME = "",
                                                            P_GROUPID = gtInvoice.GroupID,
                                                            P_GROUPNAME = gtInvoice.GroupName,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTax.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            p_TOTALPCS = Convert.ToDecimal(gtInvoice.TotalPcs),
                                                            P_ADDSSMARGINPERCENTAGE = Convert.ToDecimal(gtInvoice.SSMargin),
                                                            P_ADDSSMARGINAMT = Convert.ToDecimal(gtInvoice.SSMarginAmt),
                                                            P_ACTUALTOTCASE = Convert.ToDecimal(gtInvoice.TotalCase),
                                                            p_INVOICETYPE = gtInvoice.InvoiceType,
                                                            P_ISCHALLAN = "N",
                                                            P_REFPONO = gtInvoice.ReferencePO,
                                                            P_INVOICEAMT = Convert.ToDecimal(gtInvoice.BasicAmt),
                                                            P_MONTHID = gtInvoice.MonthID,
                                                            P_FREETAG = gtInvoice.GTFreeTag,
                                                            P_GROSSWGHT = gtInvoice.TotalGrossWght,
                                                            P_ISCLAIM = "N",
                                                            P_CLAIMNO = "0",
                                                            P_DELIVERYADDRESS = "",
                                                            P_DISNAME = "",
                                                            P_RETNAME = "",
                                                            P_ORDERTAG="N",
                                                            P_ENTRY_VERSION = "MVC",
                                                            P_TCS_PERCENT = Convert.ToDecimal(gtInvoice.TCSPercent),
                                                            P_TCS_AMOUNT = Convert.ToDecimal(gtInvoice.TCSAmt),
                                                            P_TCS_NET_AMOUNT = Convert.ToDecimal(gtInvoice.TCSNetAmt)
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                dtFree = null;
                dtTax = null;
            }
            return response;
        }

        public List<MessageModel> EcommInvoiceInsertUpdate(DepotFGInvoiceModel ecommInvoice, string BSID,
                                                        string ModuleID)

        {
            DataTable dtDetails;
            DataTable dtFree;
            DataTable dtTax;
            dtDetails = GTInvoiceDetails(ecommInvoice.InvoiceDetailsGT);
            dtFree = GTFreeDetails(ecommInvoice.FreeDetailsGT);
            dtTax = GTTaxDetails(ecommInvoice.TaxDetailsGT);
            DepotFGInvoiceModel gtInvoicehmodel = new DepotFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_GT_INVOICE_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = ecommInvoice.FGInvoiceID,
                                                            p_FLAG = ecommInvoice.FLAG,
                                                            p_SALEINVOICEDATE = ecommInvoice.InvoiceDate,
                                                            P_CUSTOMERID = ecommInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = ecommInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = ecommInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = ecommInvoice.TransporterID,
                                                            p_VEHICLENO = ecommInvoice.VehichleNo,
                                                            p_DEPOTID = ecommInvoice.BRID,
                                                            p_DEPOTNAME = ecommInvoice.BRNAME,
                                                            p_LRGRNO = ecommInvoice.LrGrNo,
                                                            p_LRGRDATE = ecommInvoice.LrGrDate,
                                                            p_GETPASSNO = ecommInvoice.GatepassNo,
                                                            p_GETPASSDATE = ecommInvoice.GatepassDate,
                                                            p_PAYMENTMODE = ecommInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = ecommInvoice.Tranportmode,
                                                            p_CREATEDBY = ecommInvoice.UserID,
                                                            p_FINYEAR = ecommInvoice.FinYear,
                                                            p_REMARKS = ecommInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = Convert.ToDecimal(ecommInvoice.NetAmt),
                                                            p_OTHERCHARGESVALUE = Convert.ToDecimal("0"),
                                                            p_ADJUSTMENTVALUE = Convert.ToDecimal("0"),
                                                            p_ROUNDOFFVALUE = Convert.ToDecimal(ecommInvoice.RoundOff),
                                                            p_SPECIALDISC = Convert.ToDecimal(ecommInvoice.TotalFreeAmt),
                                                            p_TERMSID = "0",
                                                            P_BSID = BSID,
                                                            P_BSNAME = "",
                                                            P_GROUPID = ecommInvoice.GroupID,
                                                            P_GROUPNAME = ecommInvoice.GroupName,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTax.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            p_TOTALPCS = Convert.ToDecimal(ecommInvoice.TotalPcs),
                                                            P_ADDSSMARGINPERCENTAGE = Convert.ToDecimal(ecommInvoice.SSMargin),
                                                            P_ADDSSMARGINAMT = Convert.ToDecimal(ecommInvoice.SSMarginAmt),
                                                            P_ACTUALTOTCASE = Convert.ToDecimal(ecommInvoice.TotalCase),
                                                            p_INVOICETYPE = ecommInvoice.InvoiceType,
                                                            P_ISCHALLAN = "N",
                                                            P_REFPONO = ecommInvoice.ReferencePO,
                                                            P_INVOICEAMT = Convert.ToDecimal(ecommInvoice.BasicAmt),
                                                            P_MONTHID = ecommInvoice.MonthID,
                                                            P_FREETAG = ecommInvoice.GTFreeTag,
                                                            P_GROSSWGHT = ecommInvoice.TotalGrossWght,
                                                            P_ISCLAIM = "N",
                                                            P_CLAIMNO = "0",
                                                            P_DELIVERYADDRESS = ecommInvoice.ShippingAddress,
                                                            P_DISNAME = "",
                                                            P_RETNAME = "",
                                                            P_ORDERTAG = "N",
                                                            P_ENTRY_VERSION = "MVC",
                                                            P_TCS_PERCENT = Convert.ToDecimal(ecommInvoice.TCSPercent),
                                                            P_TCS_AMOUNT = Convert.ToDecimal(ecommInvoice.TCSAmt),
                                                            P_TCS_NET_AMOUNT = Convert.ToDecimal(ecommInvoice.TCSNetAmt)
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                dtFree = null;
                dtTax = null;
            }
            return response;
        }

        public List<MessageModel> MtInvoiceInsertUpdate(DepotFGInvoiceModel mtInvoice, string BSID,
                                                        string ModuleID)

        {
            DataTable dtDetails;
            DataTable dtFree;
            DataTable dtTax;
            dtDetails = GTInvoiceDetails(mtInvoice.InvoiceDetailsGT);
            dtFree = GTFreeDetails(mtInvoice.FreeDetailsGT);
            dtTax = GTTaxDetails(mtInvoice.TaxDetailsGT);
            DepotFGInvoiceModel gtInvoicehmodel = new DepotFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_MT_INVOICE_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = mtInvoice.FGInvoiceID,
                                                            p_FLAG = mtInvoice.FLAG,
                                                            p_SALEINVOICEDATE = mtInvoice.InvoiceDate,
                                                            P_CUSTOMERID = mtInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = mtInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = mtInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = mtInvoice.TransporterID,
                                                            p_VEHICLENO = mtInvoice.VehichleNo,
                                                            p_DEPOTID = mtInvoice.BRID,
                                                            p_DEPOTNAME = mtInvoice.BRNAME,
                                                            P_BSID = BSID,
                                                            P_GROUPID = mtInvoice.GroupID,
                                                            p_LRGRNO = mtInvoice.LrGrNo,
                                                            p_LRGRDATE = mtInvoice.LrGrDate,
                                                            p_GETPASSNO = mtInvoice.GatepassNo,
                                                            p_GETPASSDATE = mtInvoice.GatepassDate,
                                                            p_PAYMENTMODE = mtInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = mtInvoice.Tranportmode,
                                                            p_CREATEDBY = mtInvoice.UserID,
                                                            p_FINYEAR = mtInvoice.FinYear,
                                                            p_REMARKS = mtInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = Convert.ToDecimal(mtInvoice.NetAmt),
                                                            p_OTHERCHARGESVALUE = Convert.ToDecimal("0"),
                                                            p_ADJUSTMENTVALUE = Convert.ToDecimal("0"),
                                                            p_ROUNDOFFVALUE = Convert.ToDecimal(mtInvoice.RoundOff),
                                                            p_SPECIALDISC = Convert.ToDecimal(mtInvoice.TotalFreeAmt), 
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTax.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_DELIVERYADDRESS= mtInvoice.ShippingAddress,
                                                            P_SALEORDERID=mtInvoice.SaleOrderID,
                                                            P_TOTALPCS = Convert.ToDecimal(mtInvoice.TotalPcs),
                                                            P_ACTUALTOTALCASE = Convert.ToDecimal(mtInvoice.TotalCase),
                                                            p_INVOICETYPE = mtInvoice.InvoiceType,
                                                            P_GROSSWGHT = mtInvoice.TotalGrossWght,
                                                            P_ISCHALLAN = "N",
                                                            P_INVOICEAMT = Convert.ToDecimal(mtInvoice.BasicAmt),
                                                            P_MONTHID = mtInvoice.MonthID,                                                            
                                                            P_FREETAG = mtInvoice.GTFreeTag,
                                                            P_ENTRY_VERSION = "MVC",
                                                            P_TCS_PERCENT = Convert.ToDecimal(mtInvoice.TCSPercent),
                                                            P_TCS_AMOUNT = Convert.ToDecimal(mtInvoice.TCSAmt),
                                                            P_TCS_NET_AMOUNT = Convert.ToDecimal(mtInvoice.TCSNetAmt)
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                dtFree = null;
                dtTax = null;
            }
            return response;
        }

        public List<MessageModel> CpcInvoiceInsertUpdate(DepotFGInvoiceModel cpcInvoice, string BSID,
                                                        string ModuleID)

        {
            DataTable dtDetails;
            DataTable dtFree;
            DataTable dtTax;
            dtDetails = GTInvoiceDetails(cpcInvoice.InvoiceDetailsGT);
            dtFree = GTFreeDetails(cpcInvoice.FreeDetailsGT);
            dtTax = GTTaxDetails(cpcInvoice.TaxDetailsGT);
            DepotFGInvoiceModel gtInvoicehmodel = new DepotFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_CPC_INVOICE_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = cpcInvoice.FGInvoiceID,
                                                            p_FLAG = cpcInvoice.FLAG,
                                                            p_SALEINVOICEDATE = cpcInvoice.InvoiceDate,
                                                            P_CUSTOMERID = cpcInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = cpcInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = cpcInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = cpcInvoice.TransporterID,
                                                            p_VEHICLENO = cpcInvoice.VehichleNo,
                                                            p_DEPOTID = cpcInvoice.BRID,
                                                            p_DEPOTNAME = cpcInvoice.BRNAME,
                                                            P_BSID = BSID,
                                                            P_GROUPID = cpcInvoice.GroupID,
                                                            p_LRGRNO = cpcInvoice.LrGrNo,
                                                            p_LRGRDATE = cpcInvoice.LrGrDate,
                                                            p_PAYMENTMODE = cpcInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = cpcInvoice.Tranportmode,
                                                            p_CREATEDBY = cpcInvoice.UserID,
                                                            p_FINYEAR = cpcInvoice.FinYear,
                                                            p_REMARKS = cpcInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = Convert.ToDecimal(cpcInvoice.NetAmt),
                                                            p_OTHERCHARGESVALUE = Convert.ToDecimal("0"),
                                                            p_ADJUSTMENTVALUE = Convert.ToDecimal("0"),
                                                            p_ROUNDOFFVALUE = Convert.ToDecimal(cpcInvoice.RoundOff),
                                                            p_SPECIALDISC = Convert.ToDecimal(cpcInvoice.TotalFreeAmt),
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTax.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_DELIVERYADDRESS = cpcInvoice.ShippingAddress,
                                                            P_SALEORDERID = cpcInvoice.SaleOrderID,
                                                            P_TOTALPCS = Convert.ToDecimal(cpcInvoice.TotalPcs),
                                                            P_ACTUALTOTALCASE = Convert.ToDecimal(cpcInvoice.TotalCase),
                                                            p_INVOICETYPE = cpcInvoice.InvoiceType,
                                                            P_GROSSWGHT = cpcInvoice.TotalGrossWght,
                                                            P_ISCHALLAN = "N",
                                                            P_INVOICEAMT = Convert.ToDecimal(cpcInvoice.BasicAmt),
                                                            P_MONTHID = cpcInvoice.MonthID,
                                                            P_FREETAG = cpcInvoice.GTFreeTag,
                                                            P_ENTRY_VERSION = "MVC"
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                dtFree = null;
                dtTax = null;
            }
            return response;
        }

        public List<MessageModel> CsdInvoiceInsertUpdate(DepotFGInvoiceModel csdInvoice, string BSID,
                                                        string ModuleID)

        {
            DataTable dtDetails;
            DataTable dtFree;
            DataTable dtComboCase;
            DataTable dtTax;
            dtDetails = GTInvoiceDetails(csdInvoice.InvoiceDetailsGT);
            dtFree = GTFreeDetails(csdInvoice.FreeDetailsGT);
            dtComboCase = CSDComboDetails(csdInvoice.ComboDetailsCSD);
            dtTax = GTTaxDetails(csdInvoice.TaxDetailsGT);
            DepotFGInvoiceModel gtInvoicehmodel = new DepotFGInvoiceModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_CSD_INVOICE_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_SALEINVOICEID = csdInvoice.FGInvoiceID,
                                                            p_FLAG = csdInvoice.FLAG,
                                                            p_SALEINVOICEDATE = csdInvoice.InvoiceDate,
                                                            P_CUSTOMERID = csdInvoice.CUSTOMERID,
                                                            p_CUSTOMERNAME = csdInvoice.CUSTOMERNAME,
                                                            p_WAYBILLNO = csdInvoice.WAYBILLNO,
                                                            p_TRANSPORTERID = csdInvoice.TransporterID,
                                                            p_VEHICLENO = csdInvoice.VehichleNo,
                                                            p_DEPOTID = csdInvoice.BRID,
                                                            p_DEPOTNAME = csdInvoice.BRNAME,
                                                            P_BSID = BSID,
                                                            P_GROUPID = csdInvoice.GroupID,
                                                            p_LRGRNO = csdInvoice.LrGrNo,
                                                            p_LRGRDATE = csdInvoice.LrGrDate,
                                                            p_ICDSNO = csdInvoice.ICDSNo,
                                                            p_ICDSDATE = csdInvoice.ICDSDate,
                                                            p_PAYMENTMODE = csdInvoice.PaymentMode,
                                                            p_MODEOFTRANSPORT = csdInvoice.Tranportmode,
                                                            p_CREATEDBY = csdInvoice.UserID,
                                                            p_FINYEAR = csdInvoice.FinYear,
                                                            p_REMARKS = csdInvoice.Remarks,
                                                            p_TOTALINVOICEVALUE = Convert.ToDecimal(csdInvoice.NetAmt),
                                                            p_OTHERCHARGESVALUE = Convert.ToDecimal("0"),
                                                            p_ADJUSTMENTVALUE = Convert.ToDecimal("0"),
                                                            p_ROUNDOFFVALUE = Convert.ToDecimal(csdInvoice.RoundOff),
                                                            p_SPECIALDISC = Convert.ToDecimal(csdInvoice.TotalFreeAmt),
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTax.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            TempTableCombo = dtComboCase.AsTableValuedParameter("Type_CSD_COMBO_PRODUCT_DETAILS"),
                                                            P_MODULEID = ModuleID,
                                                            P_DELIVERYADDRESS = csdInvoice.ShippingAddress,
                                                            P_SALEORDERID = csdInvoice.SaleOrderID,
                                                            P_TOTALPCS = Convert.ToDecimal(csdInvoice.TotalPcs),
                                                            P_ACTUALTOTALCASE = Convert.ToDecimal(csdInvoice.TotalCase),
                                                            p_INVOICETYPE = csdInvoice.InvoiceType,
                                                            P_GROSSWGHT = csdInvoice.TotalGrossWght,
                                                            P_ISCHALLAN = "N",
                                                            P_INVOICEAMT = Convert.ToDecimal(csdInvoice.BasicAmt),
                                                            P_MONTHID = csdInvoice.MonthID,
                                                            P_FREETAG = csdInvoice.GTFreeTag,
                                                            P_COMBOTAG = csdInvoice.CSDComboTag,
                                                            P_ENTRY_VERSION = "MVC"
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
                dtFree = null;
                dtComboCase = null;
                dtTax = null;
            }
            return response;
        }

        public List<MessageModel> StockAdjustmentInsertUpdate(DepotFGInvoiceModel adjustment)

        {
            DataTable dtDetails;
            
            dtDetails = StockAdjustmentDetails(adjustment.StockAdjustmentDetails);
            List<MessageModel> response = new List<MessageModel>();
            try
            {

                response = _db.Query<MessageModel>("USP_STOCKADJUSTMENT_INSERT_UPDATE_V2",
                                                        new
                                                        {
                                                            p_ADJUSTMENTID = adjustment.FGInvoiceID,
                                                            p_FLAG = adjustment.FLAG,
                                                            p_ADJUSTMENTDATE = adjustment.InvoiceDate,
                                                            p_DEPOTID = adjustment.BRID,
                                                            p_DEPOTNAME = adjustment.BRNAME,
                                                            p_CREATEDBY = Convert.ToInt16(adjustment.UserID),
                                                            p_FINYEAR = adjustment.FinYear,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_STOCK_ADJUSTMENT_DETAILS"),
                                                            p_REMARKS = adjustment.Remarks,
                                                            p_TYPE = adjustment.AdjustmentType,
                                                            p_FROMMENU = adjustment.AdjustmentMenu,
                                                            P_ENTRY_FROM = "MVC"
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
            }
            return response;
        }

        public DataTable GTInvoiceDetails(List<InvoiceDetailsGT> InvoiceDetailsGT)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKINGSIZEID", typeof(string));
            dt.Columns.Add("PACKINGSIZENAME", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("QTYPCS", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("NSR", typeof(decimal));
            dt.Columns.Add("RATEDISC", typeof(decimal));
            dt.Columns.Add("DISCVALUE", typeof(decimal));
            dt.Columns.Add("QSH", typeof(string));
            dt.Columns.Add("QSGUID", typeof(string));
            dt.Columns.Add("DISCPER", typeof(decimal));
            dt.Columns.Add("DISCAMT", typeof(decimal));
            dt.Columns.Add("PRICESCHEMEID", typeof(string));
            dt.Columns.Add("PERCENTAGE", typeof(decimal));
            dt.Columns.Add("VALUE", typeof(decimal));

            int count = 1;
            foreach (var item in InvoiceDetailsGT)
            {
                dt.Rows.Add(item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKINGSIZEID,
                            item.PACKINGSIZENAME,
                            item.MRP,
                            item.QTY,
                            item.QTYPCS,
                            item.RATE,
                            item.BATCHNO,
                            item.AMOUNT,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.NSR,
                            item.RATEDISC,
                            item.DISCVALUE,
                            item.QSH,
                            item.QSGUID,
                            item.DISCPER,
                            item.DISCAMT,
                            item.PRICESCHEMEID,
                            item.PERCENTAGE,
                            item.VALUE
                            );
                count++;
            }
            return dt;
        }

        public DataTable GTFreeDetails(List<FreeDetailsGT> FreeDetailsGT)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SCHEMEID", typeof(string));
            dt.Columns.Add("SCHEME_PRODUCT_ID", typeof(string));
            dt.Columns.Add("SCHEME_PRODUCT_NAME", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKSIZEID", typeof(string));
            dt.Columns.Add("PACKSIZENAME", typeof(string));
            dt.Columns.Add("SCHEME_QTY", typeof(decimal));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("BRATE", typeof(decimal));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("NSR", typeof(decimal));
            dt.Columns.Add("SCHEME_PRODUCT_BATCHNO", typeof(string));
            dt.Columns.Add("QSGUID", typeof(string));


            int count = 1;
            foreach (var item in FreeDetailsGT)
            {
                dt.Rows.Add(item.SCHEMEID,
                            item.SCHEME_PRODUCT_ID,
                            item.SCHEME_PRODUCT_NAME,
                            item.QTY,
                            item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKSIZEID,
                            item.PACKSIZENAME,
                            item.SCHEME_QTY,
                            item.MRP,
                            item.BRATE,
                            item.AMOUNT,
                            item.BATCHNO,
                            item.WEIGHT,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.NSR,
                            item.SCHEME_PRODUCT_BATCHNO,
                            item.QSGUID
                            );
                count++;
            }
            return dt;
        }

        public DataTable GTTaxDetails(List<TaxDetailsGT> TaxDetailsGT)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRIMARYPRODUCTID", typeof(string));
            dt.Columns.Add("PRIMARYPRODUCTBATCHNO", typeof(string));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("TAXID", typeof(string));
            dt.Columns.Add("TAXPERCENTAGE", typeof(decimal));
            dt.Columns.Add("TAXVALUE", typeof(decimal));
            dt.Columns.Add("TAG", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));


            int count = 1;
            foreach (var item in TaxDetailsGT)
            {
                dt.Rows.Add(item.PRIMARYPRODUCTID,
                            item.PRIMARYPRODUCTBATCHNO,
                            item.PRODUCTID,
                            item.BATCHNO,
                            item.TAXID,
                            item.TAXPERCENTAGE,
                            item.TAXVALUE,
                            item.TAG,
                            item.MRP
                            );
                count++;
            }
            return dt;
        }

        public DataTable CSDComboDetails(List<ComboDetailsCSD> ComboDetailsCSD)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRIMARYPRODUCTID", typeof(string));
            dt.Columns.Add("SECONDARYPRODUCTID", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            int count = 1;
            foreach (var item in ComboDetailsCSD)
            {
                dt.Rows.Add(item.PRIMARYPRODUCTID,
                            item.SECONDARYPRODUCTID,
                            item.QTY
                            );
                count++;
            }
            return dt;
        }

        public DataTable StockAdjustmentDetails(List<StockAdjustmentDetails> StockAdjustmentDetails)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("PRICE", typeof(decimal));
            dt.Columns.Add("ADJUSTMENTQTY", typeof(decimal));
            dt.Columns.Add("PACKINGSIZEID", typeof(string));
            dt.Columns.Add("PACKINGSIZENAME", typeof(string));
            dt.Columns.Add("REASONID", typeof(string));
            dt.Columns.Add("REASONNAME", typeof(string));
            dt.Columns.Add("STORELOCATIONID", typeof(string));
            dt.Columns.Add("STORELOCATIONNAME", typeof(string));
            dt.Columns.Add("MFDATE", typeof(string));
            dt.Columns.Add("EXPRDATE", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("BUFFERQTY", typeof(decimal));
            dt.Columns.Add("APPROVED", typeof(string));

            int count = 1;
            foreach (var item in StockAdjustmentDetails)
            {
                dt.Rows.Add(item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.BATCHNO,
                            item.PRICE,
                            item.ADJUSTMENTQTY,
                            item.PACKINGSIZEID,
                            item.PACKINGSIZENAME,
                            item.REASONID,
                            item.REASONNAME,
                            item.STORELOCATIONID,
                            item.STORELOCATIONNAME,
                            item.MFDATE,
                            item.EXPRDATE,
                            item.MRP,
                            item.WEIGHT,
                            item.AMOUNT,
                            item.BUFFERQTY,
                            item.APPROVED
                            );
                count++;
            }
            return dt;
        }

        public List<GTInvoiceList> BindGtInvoiceGrid(string FromDate, string ToDate, string Finyear, string bsid, string DepotID, string CheckerFlag, string userID,string Challan)
        {
            List<GTInvoiceList> gtinvoicefgGrid = new List<GTInvoiceList>();
            try
            {
                gtinvoicefgGrid = _db.Query<GTInvoiceList>("USP_VERBAL_INVOICE_LIST_V2", 
                    new {
                        P_FROMDATE = FromDate,
                        P_TODATE = ToDate,
                        P_FINYEAR = Finyear,
                        P_BSID = bsid,
                        P_DEPOTID = DepotID,
                        P_CHECKERFLAG = CheckerFlag,
                        P_USERID = userID,
                        P_CHALLAN = Challan
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gtinvoicefgGrid;
        }

        public List<GTInvoiceList> BindMtInvoiceGrid(string FromDate, string ToDate, string Finyear, string bsid, string DepotID, string CheckerFlag, string userID, string Challan)
        {
            List<GTInvoiceList> mtinvoicefgGrid = new List<GTInvoiceList>();
            try
            {
                mtinvoicefgGrid = _db.Query<GTInvoiceList>("USP_ORDER_INVOICE_LIST_V2",
                    new
                    {
                        P_FROMDATE = FromDate,
                        P_TODATE = ToDate,
                        P_FINYEAR = Finyear,
                        P_BSID = bsid,
                        P_DEPOTID = DepotID,
                        P_CHECKERFLAG = CheckerFlag,
                        P_USERID = userID,
                        P_CHALLAN = Challan
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mtinvoicefgGrid;
        }

        public List<MessageModel> GetInvoiceStatus(string InvoiceID, string ModuleID, string Type)
        {
            List<MessageModel> invoiceStatus = new List<MessageModel>();
            try
            {
                invoiceStatus = _db.Query<MessageModel>("USP_TRANSACTION_STATUS_V2 ", new { P_INVOICEID = InvoiceID, P_MODULE = ModuleID, P_TYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return invoiceStatus;
        }

        public List<MessageModel> GetGTClosingStatus(string InvoiceDate, string DepotID, string Finyear)
        {
            List<MessageModel> gtclosingstatus = new List<MessageModel>();
            try
            {
                gtclosingstatus = _db.Query<MessageModel>("USP_BUSINESSSEGMENTWISE_CLOSING_DATE_CHECKING_V2 ", new { p_saleinvoicedate = InvoiceDate, p_depotid = DepotID, p_finyear = Finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gtclosingstatus;
        }

        public List<MessageModel> GetAccPostingStatus(string DepotID, string BSID, string UserID, string InvoiceDate)
        {
            List<MessageModel> accpostingstatus = new List<MessageModel>();
            try
            {
                accpostingstatus = _db.Query<MessageModel>("USP_ACCOUNT_POSTING_STATUS ", new { P_DEPOTID = DepotID, P_BSID = BSID, P_USERID = UserID, P_DATE = InvoiceDate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return accpostingstatus;
        }

        public List<MessageModel> GetDayEndStatus(string DepotID, string BSID, string UserID, string InvoiceDate)
        {
            List<MessageModel> dayendstatus = new List<MessageModel>();
            try
            {
                dayendstatus = _db.Query<MessageModel>("USP_DAY_END_STATUS_V2 ", new { P_DEPOTID = DepotID, P_BSID = BSID, P_USERID = UserID, P_DATE = InvoiceDate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dayendstatus;
        }

        public List<InvoiceTaxOnEdit> GetGTInvoiceTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            List<InvoiceTaxOnEdit> Tax = new List<InvoiceTaxOnEdit>();
            try
            {
                Tax = _db.Query<InvoiceTaxOnEdit>("USP_GET_CNF_INVOICE_TAX_ON_EDIT ", new { P_INVOICEID = InvoiceID, P_TaxID = TaxID, P_ProductID = ProductID, P_Batchno = BatchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return Tax;
        }

        public List<MessageModel> GtInvoiceDelete(string InvoiceID)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_SALE_INVOICE_DELETE_V2 ", new { p_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
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

        public InvoiceEditGT GTInvoiceEdit(string InvoiceID)
        {
            InvoiceEditGT EditInvoice = new InvoiceEditGT();
            var reader = _db.QueryMultiple("USP_SALE_INVOICE_DETAILS_V2", new { P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
            try
            {
                
                var vheader = reader.Read<InvoiceHeaderEditGT>().ToList();
                var vdetails = reader.Read<InvoiceDetailsEditGT>().ToList();
                var vtaxcount = reader.Read<InvoiceTaxcountEditGT>().ToList();
                var vfooter = reader.Read<InvoiceFooterEditGT>().ToList();
                var vtaxdetails = reader.Read<InvoiceTaxEditGT>().ToList();
                var vpriceschemedetails = reader.Read<InvoicePriceSchemeEditGT>().ToList();
                var vqtyschemedetails = reader.Read<InvoiceQuantitySchemeEditGT>().ToList();
                var vproductdetails = reader.Read<InvoiceProductDetailsEditGT>().ToList();
                var vorderdetails = reader.Read<InvoiceOrderDetailsEditGT>().ToList();
                var vorderheader = reader.Read<InvoiceOrderHeaderEditGT>().ToList();


                EditInvoice.InvoiceHeaderEditGT = vheader;
                EditInvoice.InvoiceDetailsEditGT = vdetails;
                EditInvoice.InvoiceTaxcountEditGT = vtaxcount;
                EditInvoice.InvoiceFooterEditGT = vfooter;
                EditInvoice.InvoiceTaxEditGT = vtaxdetails;
                EditInvoice.InvoicePriceSchemeEditGT = vpriceschemedetails;
                EditInvoice.InvoiceQuantitySchemeEditGT = vqtyschemedetails;
                EditInvoice.InvoiceProductDetailsEditGT = vproductdetails;
                EditInvoice.InvoiceOrderDetailsEditGT = vorderdetails;
                EditInvoice.InvoiceOrderHeaderEditGT = vorderheader;

                reader.Dispose();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                reader.Dispose();
            }
            return EditInvoice;
        }

        public ConversionEdit CsdConversionEdit(string ConversionID)
        {
            ConversionEdit EditConversion = new ConversionEdit();
            var reader = _db.QueryMultiple("USP_CONVERSION_DETAILS", new { P_CONVERSIONID = ConversionID }, commandType: CommandType.StoredProcedure);
            try
            {

                var vheader = reader.Read<ConversionHeaderEdit>().ToList();
                var vdetails = reader.Read<ConversionDetailsEdit>().ToList();
                


                EditConversion.ConversionHeaderEdit = vheader;
                EditConversion.ConversionDetailsEdit = vdetails;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                reader.Dispose();
            }
            return EditConversion;
        }

        public StockJournalEdit StockAdjustmentEdit(string StockjournalID)
        {
            StockJournalEdit EditJournal = new StockJournalEdit();
            var reader = _db.QueryMultiple("USP_STOCK_JOURNAL_DETAILS", new { P_JOURNALID = StockjournalID }, commandType: CommandType.StoredProcedure);
            try
            {

                var vheader = reader.Read<StockJournalHeaderEdit>().ToList();
                var vdetails = reader.Read<StockJournalDetailsEdit>().ToList();



                EditJournal.StockJournalHeaderEdit = vheader;
                EditJournal.StockJournalDetailsEdit = vdetails;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                reader.Dispose();
            }
            return EditJournal;
        }

        public GTCustomerDetails GTCustomerDetails(string CustomerID, string DepotID, string Finyear, string MonthID, string InvoiceID)
        {
            GTCustomerDetails CustomerDetails = new GTCustomerDetails();
            try
            {
                var reader = _db.QueryMultiple("USP_GET_GT_CUSTOMER_DETAILS", new { P_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_FINYEAR = Finyear, P_MONTHID = MonthID, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
                var vinvoicelimit = reader.Read<InvoiceLimit>().ToList();
                var vssmargin = reader.Read<SSMargin>().ToList();
                var vgststatus = reader.Read<GstNoStatus>().ToList();
                CustomerDetails.InvoiceLimit = vinvoicelimit;
                CustomerDetails.SSMargin = vssmargin;
                CustomerDetails.GstNoStatus = vgststatus;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return CustomerDetails;
        }

        public GTCustomerOutstanding GTCustomerOutstanding(string CustomerID, string DepotID, string Finyear, string InvoiceID)
        {
            GTCustomerOutstanding CustomerOutstanding = new GTCustomerOutstanding();
            try
            {
                var reader = _db.QueryMultiple("USP_DISTRIBUTOR_OUTSTANDING_V2", new { p_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_FINYEAR = Finyear, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure);
                var voutstanding = reader.Read<OutStanding>().ToList();
                var vcreditlimit = reader.Read<CreditLimit>().ToList();
                CustomerOutstanding.OutStanding = voutstanding;
                CustomerOutstanding.CreditLimit = vcreditlimit;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return CustomerOutstanding;
        }



        public List<GTCustomerTarget> GetCustomerTarget(string CustomerID, string DepotID, string BSID,string Finyear, string MonthID, string InvoiceID)
        {
            List<GTCustomerTarget> target = new List<GTCustomerTarget>();
            try
            {
                target = _db.Query<GTCustomerTarget>("USP_DISTRIBUTOR_TARGET_V2 ", new { p_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_BSID = BSID, P_FINYEAR = Finyear, P_MONTH = MonthID, P_INVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return target;
        }

        public List<CustomerNetSale> GetNetSale(string CustomerID, string DepotID, string Finyear, string InvoiceID)
        {
            List<CustomerNetSale> netsale = new List<CustomerNetSale>();
            try
            {
                netsale = _db.Query<CustomerNetSale>("USP_NET_SALE_AMOUNT", new { p_CUSTOMERID = CustomerID, P_DEPOTID = DepotID, P_FINYEAR = Finyear, P_INVOICEID = InvoiceID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return netsale;
        }

        public List<TCSPercentageLimit> GetTCSPercentLimit(string CustomerID)
        {
            List<TCSPercentageLimit> tcsLimit = new List<TCSPercentageLimit>();
            try
            {
                tcsLimit = _db.Query<TCSPercentageLimit>("USP_GET_TCS_PERCENT", new { P_CUSTOMERID = CustomerID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return tcsLimit;
        }


        public List<DepotInvoiceRate> GetBCP(string Customerid, string Productid, string Invoicedate, string Mrp, string Depotid, string Menuid,string BSid,string Groupid,string Tag)
        {
            List<DepotInvoiceRate> bcp = new List<DepotInvoiceRate>();
            try
            {
                bcp = _db.Query<DepotInvoiceRate>("sp_GetBCP ", new { CustomerID = Customerid, ProductID = Productid, DATE = Invoicedate, MRP = Convert.ToDecimal(Mrp), DepoID = Depotid, ModuleID = Menuid, BusinessSegmentID= BSid, GroupID= Groupid, TAG= Tag }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return bcp;
        }
        public List<QtyInPcsGT> QtyInPcsGT(string Productid, string PacksizefromID, string Deliveredqty, string Stockqty, string SaleorderID, string InvoiceID)
        {
            List<QtyInPcsGT> qtypcs = new List<QtyInPcsGT>();
            try
            {
                qtypcs = _db.Query<QtyInPcsGT>("USP_QTYINPCS ", new { P_PRODUCTID = Productid, P_PACKSIZEFROMID = PacksizefromID, P_DELIVEREDQTY = Convert.ToDecimal(Deliveredqty), P_STOCKQTY = Convert.ToDecimal(Stockqty), P_SALEORDERID = SaleorderID, P_SALEINVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return qtypcs;
        }

        public List<ProductType> ProductType(string Productid)
        {
            List<ProductType> producttype = new List<ProductType>();
            try
            {
                producttype = _db.Query<ProductType>("USP_PRODUCT_TYPE_V2 ", new { P_PRODUCTID = Productid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return producttype;
        }

        public List<GroupStatus> GroupStatus(string CustomerID,string BSID)
        {
            List<GroupStatus> groupstatus = new List<GroupStatus>();
            try
            {
                groupstatus = _db.Query<GroupStatus>("USP_GROUP_STATUS ", new { P_CUSTOMERID = CustomerID, P_BSID = BSID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return groupstatus;
        }

        public List<OfflineStatus> OfflineStatus(string DepotID)
        {
            List<OfflineStatus> offlinestatus = new List<OfflineStatus>();
            try
            {
                offlinestatus = _db.Query<OfflineStatus>("USP_GET_OFFLINE_STATUS ", new { P_DEPOTID = DepotID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return offlinestatus;
        }

        public List<OfflineTag> OfflineTag()
        {
            List<OfflineTag> offlinetag = new List<OfflineTag>();
            try
            {
                offlinetag = _db.Query<OfflineTag>("USP_GET_NEW_ENTRY_TAG ",  commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return offlinetag;
        }

        public List<RatePerCase> Ratepercase(string BSID,string GroupID,string ProductID)
        {
            List<RatePerCase> ratepercase = new List<RatePerCase>();
            try
            {
                ratepercase = _db.Query<RatePerCase>("USP_RatePerCase ", new { P_BSID = BSID, P_GRPID= GroupID, P_PRODUCTID= ProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return ratepercase;
        }

        public List<Packsize> Packsize(string ProductID, string Qty)
        {
            List<Packsize> packsize = new List<Packsize>();
            try
            {
                packsize = _db.Query<Packsize>("USP_GET_CASEPACK ", new { P_PRODUCTID = ProductID, P_QTY = Convert.ToDecimal(Qty)}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return packsize;
        }

        public List<PostingStatus> Postingstatus(string InvoiceID)
        {
            List<PostingStatus> postingstatus = new List<PostingStatus>();
            try
            {
                postingstatus = _db.Query<PostingStatus>("USP_CHECK_ACCOUNT_POSTING_V2", new { P_INVOICEID = InvoiceID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return postingstatus;
        }

        public List<CodeStatus> Codestatus(string CustomerID)
        {
            List<CodeStatus> codestatus = new List<CodeStatus>();
            try
            {
                codestatus = _db.Query<CodeStatus>("USP_CHECK_CPC_CUSTOMER_CODE_V2", new { P_CUSTOMERID = CustomerID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return codestatus;
        }

        public List<PriceSchemeDiscount> GetPriceSchemeDiscount(string Productid, string Qty, string Packsize, string CustomerID, string SaleorderID,
                                                    string Date, string BSID, string GroupID, string DepotID, string MRP
                                                )
        {
            List<PriceSchemeDiscount> priceSchemediscount = new List<PriceSchemeDiscount>();
            try
            {
                priceSchemediscount = _db.Query<PriceSchemeDiscount>("USP_PRICE_SCHEME_DISCOUNT_GT_V2 ", new { P_ProductID = Productid, P_Qty = Convert.ToDecimal(Qty), P_Packsize = Packsize, P_CustomerID = CustomerID, P_SaleOrderID = SaleorderID, P_Date = Date, P_BSID = BSID, P_GroupID = GroupID, P_DepotID = DepotID, P_MRP = Convert.ToDecimal(MRP) }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return priceSchemediscount;
        }

        public List<HSNCode> GetHSNCode(string Productid)
        {
            List<HSNCode> hsn = new List<HSNCode>();
            try
            {
                hsn = _db.Query<HSNCode>("USP_GET_FREE_PRODUCT_HSN ", new { P_PRODUCTID = Productid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return hsn;
        }

        public List<QuantityScheme> GetQuantityScheme(  string date, string stateid, string disctrictid, string productid, string groupid, string bsid, 
                                                        string qty, string packsizeid, string zoneid, string teritoryid,string customerid,
                                                        string depotid, string moduleid, string batchno, string mrp, string flag, string isdayend)
        {
            List<QuantityScheme> qtyscheme = new List<QuantityScheme>();
            try
            {
                qtyscheme = _db.Query<QuantityScheme>("SP_GetQTYScheme ", new {   DATE = date,
                                                                                    STATEID = stateid,
                                                                                    DISTRICTID = disctrictid,
                                                                                    PRODUCTID = productid,
                                                                                    PRINCIPALGROUPID = groupid,
                                                                                    BUSINESSSEGMENTID = bsid,
                                                                                    QTY = Convert.ToDecimal(qty),
                                                                                    PACKSIZEID = packsizeid,
                                                                                    Zone = zoneid,
                                                                                    Territory = teritoryid,
                                                                                    CustomerID = customerid,
                                                                                    DepoID = depotid,
                                                                                    ModuleID = moduleid,
                                                                                    BATCHNO = batchno,
                                                                                    MRP = Convert.ToDecimal(mrp),
                                                                                    FLAG = flag,
                                                                                    ISDAYEND = Convert.ToInt32(isdayend)
                                                                                  }, 
                                                                             commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return qtyscheme;
        }

        public List<QuantityConversion> ConvertedQuantity(string Productid,string FromPacksize,string ToPacksize,string CaseQty,string PcsQty)
        {
            List<QuantityConversion> convertedqty = new List<QuantityConversion>();
            try
            {
                convertedqty = _db.Query<QuantityConversion>("USP_PCS_CASE_CONVERSION ", new { P_PRODUCTID = Productid, P_FROMPACKSIZE = FromPacksize, P_TOPACKSIZE = ToPacksize, P_QTY_CASE = CaseQty, P_QTY_PCS = PcsQty }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return convertedqty;
        }

        public List<CaseToPCSConversion> CaseToPCSConversion(string Productid, string FromPacksize, string ToPacksize, string CaseQty, string PcsQty)
        {
            List<CaseToPCSConversion> casetopcs = new List<CaseToPCSConversion>();
            try
            {
                casetopcs = _db.Query<CaseToPCSConversion>("USP_GET_CASE_TO_PCS_CONVERSION_V2 ", new { P_PRODUCTID = Productid, P_FROMPACKSIZE = FromPacksize, P_TOPACKSIZE = ToPacksize, P_CASE_QTY = Convert.ToDecimal(CaseQty), P_PCS_QTY = Convert.ToDecimal(PcsQty) }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return casetopcs;
        }

        public List<FreeProductBatchInfoList> GetFreeProductBatchDetails(   string depotid, string productid, string packsizeid, string batchno,   
                                                                            string customerid, string date, string moduleid, string bsid, string groupid,
                                                                            string mrp, string storelocationid
                                                                        )
        {
            List<FreeProductBatchInfoList> freeProductBatchDetails = new List<FreeProductBatchInfoList>();
            try
            {
                freeProductBatchDetails = _db.Query<FreeProductBatchInfoList>("USP_FREE_PRODUCT_BCP_V2 ", new
                {
                    P_DEPOTID = depotid,
                    P_PRODUCTID = productid,
                    P_PACKSIZEID = packsizeid,
                    P_BATCHNO = batchno,
                    P_CUSTOMERID = customerid,
                    P_DATE = date,
                    P_MODULEID = moduleid,
                    P_BUSINESSSEGMENTID = bsid,
                    P_PRINCIPALGROUPID = groupid,
                    P_MRP = Convert.ToDecimal(mrp),
                    P_STOCKLOCATION = storelocationid
                },
                commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return freeProductBatchDetails;
        }

        public List<AvailableStock> GetAvailableStock(string DepotID,string Productid,string Batch,string MRP,string MfgDate,string ExpDate,string StorelocationID)
        {
            List<AvailableStock> stock = new List<AvailableStock>();
            try
            {
                stock = _db.Query<AvailableStock>("USP_FINAL_STOCK_CHECKING ", new { P_DEPOTID = DepotID, P_PRODUCTID = Productid, P_BATCHNO = Batch, P_MRP = Convert.ToDecimal(MRP), P_MFGDATE = MfgDate, P_EXPDATE = ExpDate, P_STOCKLOCATION = StorelocationID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return stock;
        }

        public List<OrdervsDispatchList> GetOrdervsDispatch(string OrderID, string ProductID, string PacksizeID,string InvoiceID)
        {
            List<OrdervsDispatchList> ordervsdispatch = new List<OrdervsDispatchList>();

            try
            {
                ordervsdispatch = _db.Query<OrdervsDispatchList>("USP_ORDER_DESPATCH_QTY_V2 ", new { P_SALEORDERID = OrderID, P_PRODUCTID = ProductID, P_PACKSIZE = PacksizeID, P_SALEINVOICEID = InvoiceID }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return ordervsdispatch;
        }

        public List<QuantityInPcs> GetQuantityInPcs(string ProductID, string PacksizefromID, string DeliveredQty, string StockQty, string OrderID, string InvoiceID)
        {
            List<QuantityInPcs> qtypcs = new List<QuantityInPcs>();

            try
            {
                qtypcs = _db.Query<QuantityInPcs>("USP_QTYINPCS ", 
                                                new {   P_PRODUCTID = ProductID,
                                                        P_PACKSIZEFROMID = PacksizefromID,
                                                        P_DELIVEREDQTY = Convert.ToDecimal(DeliveredQty),
                                                        P_STOCKQTY = Convert.ToDecimal(StockQty),
                                                        P_SALEORDERID = OrderID,
                                                        P_SALEINVOICEID = InvoiceID,
                                                }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return qtypcs;
        }

        public List<CsdComboProduct> BindCsdComboGrid(string BSID)
        {
            List<CsdComboProduct> csdcomboGrid = new List<CsdComboProduct>();
            try
            {
                csdcomboGrid = _db.Query<CsdComboProduct>("USP_CSD_COMBO_PRODUCT", new{P_BSID = BSID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return csdcomboGrid;
        }

        public List<ICDSDetails> BindICDSdetails(string SaleorderID)
        {
            List<ICDSDetails> icdsdetails = new List<ICDSDetails>();
            try
            {
                icdsdetails = _db.Query<ICDSDetails>("USP_GET_ICDS_DETAILS_V2", new { P_SALEORDERID = SaleorderID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return icdsdetails;
        }

        public List<ConversionComboProduct> BindConversionComboGrid()
        {
            List<ConversionComboProduct> conversioncomboGrid = new List<ConversionComboProduct>();
            try
            {
                conversioncomboGrid = _db.Query<ConversionComboProduct>("USP_CSD_COMBO_PRODUCT_V2", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conversioncomboGrid;
        }

        public List<ConversionComboProductDetails> BindConversionComboProductDetails(string ComboProductID)
        {
            List<ConversionComboProductDetails> conversioncomboproductdetails = new List<ConversionComboProductDetails>();
            try
            {
                conversioncomboproductdetails = _db.Query<ConversionComboProductDetails>("USP_CONVERSION_COMBO_PRODUCT_DETAILS", new { P_COMBOPRODUCTID = ComboProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conversioncomboproductdetails;
        }

        public List<ComboMRP> ComboMrp(string ProductID)
        {
            List<ComboMRP> combomrp = new List<ComboMRP>();
            try
            {
                combomrp = _db.Query<ComboMRP>("USP_COMBO_PRODUCT_MRP", new { P_PRODUCTID = ProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return combomrp;
        }

        public List<StockJournalProductType> JournalProductType(string ProductID)
        {
            List<StockJournalProductType> journalproducttype = new List<StockJournalProductType>();
            try
            {
                journalproducttype = _db.Query<StockJournalProductType>("USP_PRODUCT_CHECKING", new { P_PRODUCTID = ProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return journalproducttype;
        }

        public List<BatchExistsFlag> BatchExists(string ProductID,string BatchNo,string MRP)
        {
            List<BatchExistsFlag> batchexists = new List<BatchExistsFlag>();
            try
            {
                batchexists = _db.Query<BatchExistsFlag>("USP_CHECK_BATCH_V2", new { P_PRODUCTID = ProductID, P_BATCHNO = BatchNo, P_MRP = Convert.ToDecimal(MRP) }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return batchexists;
        }

        public List<CountExists> CountExists(string ProductID, string BatchNo, string MRP)
        {
            List<CountExists> countexists = new List<CountExists>();
            try
            {
                countexists = _db.Query<CountExists>("USP_COUNT_EXISTS_BATCH_V2", new { P_PRODUCTID = ProductID, P_BATCHNO = BatchNo, P_MRP = Convert.ToDecimal(MRP) }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return countexists;
        }

        public List<MessageModel> BatchMasterInsert(DepotFGInvoiceModel batch)
        {
            
            List<MessageModel> response = new List<MessageModel>();
            try
            {

                response = _db.Query<MessageModel>("USP_BATCH_MASTER_INSERT_V2",
                                                        new
                                                        {
                                                            P_PRODUCTID = batch.ComboProduct,
                                                            P_BATCHNO = batch.ComboBatch,
                                                            P_MFDATE = batch.ComboMfg,
                                                            P_EXPDATE = batch.ComboExpr,
                                                            P_MRP = Convert.ToDecimal(batch.ComboMrp),
                                                            P_FINYEAR = batch.FinYear
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return response;
        }

        public List<ConversionList> BindConversionGrid(string FromDate, string ToDate, string DepotID, string Finyear)
        {
            List<ConversionList> conversionGrid = new List<ConversionList>();
            try
            {
                conversionGrid = _db.Query<ConversionList>("USP_CSD_CONVERSION_LIST",
                    new
                    {
                        P_FROMDATE = FromDate,
                        P_TODATE = ToDate,
                        P_DEPOTID = DepotID,
                        P_FINYEAR = Finyear
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conversionGrid;
        }

        public List<StockJournalList> BindStockJournalGrid(string FromDate, string ToDate, string DepotID, string Finyear)
        {
            List<StockJournalList> stockjournalGrid = new List<StockJournalList>();
            try
            {
                stockjournalGrid = _db.Query<StockJournalList>("USP_GET_STOCK_JOURNAL_LIST_V2",
                    new
                    {
                        P_FROMDATE = FromDate,
                        P_TODATE = ToDate,
                        P_DEPOTID = DepotID,
                        P_FINYEAR = Finyear
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stockjournalGrid;
        }

        public List<StorelocationList> GetStorelocation()
        {
            List<StorelocationList> storelocation = new List<StorelocationList>();

            try
            {
                storelocation = _db.Query<StorelocationList>("USP_GET_STORELOCATION ", commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return storelocation;
        }

        public List<ReasonList> GetReason(string MenuID)
        {
            List<ReasonList> reason = new List<ReasonList>();

            try
            {
                reason = _db.Query<ReasonList>("USP_GET_REASON_V2 ", new { P_MENUID = MenuID }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return reason;
        }

        public List<StockJournalCategoryList> GetJournalCategory()
        {
            List<StockJournalCategoryList> journalcategory = new List<StockJournalCategoryList>();

            try
            {
                journalcategory = _db.Query<StockJournalCategoryList>("USP_GET_STOCK_JOURNAL_PRODUCT_CATEGORY_V2 ", commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return journalcategory;
        }

        public List<StockJournalProductList> GetJournalProduct(string DepotID,string CategoryID,string Mode)
        {
            List<StockJournalProductList> journalproduct = new List<StockJournalProductList>();

            try
            {
                journalproduct = _db.Query<StockJournalProductList>("SP_DEPOTWISE_PRODUCT_CATEGORY_WISE ", new { P_DEPOTID = DepotID, P_CATEGORYID = CategoryID, P_MODE = Mode}, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return journalproduct;
        }

        public List<InterStoreLocationProductList> GetInterStoreLocationProduct(string DepotID)
        {
            List<InterStoreLocationProductList> shiftstorelocationproduct = new List<InterStoreLocationProductList>();

            try
            {
                shiftstorelocationproduct = _db.Query<InterStoreLocationProductList>("SP_DEPOTWISE_PRODUCT ", new { P_DEPOTID = DepotID}, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return shiftstorelocationproduct;
        }

        public List<StockJournalPacksizeList> GetJournalPacksize()
        {
            List<StockJournalPacksizeList> journalpacksize = new List<StockJournalPacksizeList>();

            try
            {
                journalpacksize = _db.Query<StockJournalPacksizeList>("USP_GET_STOCK_JOURNAL_PACKSIZE_V2 ", commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return journalpacksize;
        }

        public List<DepotExcessBatchInfoList> GetExcessBatchDetails(string DepotID, string ProductID, string Packsize,string Batch, string FromDate,string ToDate,string MRP,string Storelocation)
        {
            List<DepotExcessBatchInfoList> excessbatchLists = new List<DepotExcessBatchInfoList>();
            List<DepotExcessBatchInfo> batch = new List<DepotExcessBatchInfo>();
            try
            {
                batch = _db.Query<DepotExcessBatchInfo>("USP_MFG_EXP_STOCK ", 
                                                    new {   P_DEPOTID = DepotID,
                                                        P_PRODUCTID = ProductID,
                                                        P_PACKSIZEID = Packsize,
                                                        P_BATCHNO = Batch,
                                                        P_FROMDATE = FromDate,
                                                        P_TODATE = ToDate,
                                                        P_MRP = Convert.ToDecimal(MRP),
                                                        P_STOCKLOCATION = Storelocation,
                                                    }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    excessbatchLists.Add(new DepotExcessBatchInfoList()
                    {
                        BatchDepotID = lst.DEPOTID,
                        BatchPRODUCTID = lst.PRODUCTID,
                        BatchPRODUCTNAME = lst.PRODUCTNAME,
                        BatchASSESMENTPERCENTAGE = lst.ASSESMENTPERCENTAGE,
                        BATCHNO = lst.BATCHNO,
                        BatchMRP = lst.MRP,
                        BatchMFGDATE = lst.MFGDATE,
                        BatchEXPIRDATE = lst.EXPIRDATE,
                        BatchSTORELOCATION = lst.STORELOCATIONID
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return excessbatchLists;
        }
    }
}
