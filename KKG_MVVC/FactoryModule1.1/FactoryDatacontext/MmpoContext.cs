using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
    public class MmpoContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public Povendorcurrencey Loadvendorcurrencey()
        {
            Povendorcurrencey poorder = new Povendorcurrencey();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_VENDOR_AND_CURRENCY", commandType: CommandType.StoredProcedure);

                var vendor = reader.Read<Povendor>().ToList();
                var currencey = reader.Read<Pocurrncey>().ToList();

                poorder.Povendors = vendor;
                poorder.Pocurrnceys = currencey;
               
            }
            catch (Exception ex)
            {

            }
             return poorder; ;
        }

        public List<PurchaseOrder> Loadproduct(string Vendorid)
        {
            List<PurchaseOrder> poorder = new List<PurchaseOrder>();
            try
            {
                poorder = _db.Query<PurchaseOrder>("USP_BIND_PRDODUCT_VENDORWISE", new { p_vendorid = Vendorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return poorder;
        }
        public PurchaseOrder LoadDepotFromUser(string userid)
        {
            PurchaseOrder user = new PurchaseOrder();
            try
            {
                user = _db.QueryFirstOrDefault<PurchaseOrder>("Usp_Bind_Depot_User_Wise ", new { P_userid = userid }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public List<PurchaseOrder> Loadtax(string vendorid,string productid,string depotid)
        {
            List<PurchaseOrder> poorder = new List<PurchaseOrder>();
            try
            {
                poorder = _db.Query<PurchaseOrder>("usp_tax_from_category_new", new { p_vaendorid = vendorid, p_productid= productid, p_depotid= depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return poorder;
        }

        public List<PurchaseOrder> Loadpacsizemrprate(string Productid,string Vendorid,string Podate)
        {
            List<PurchaseOrder> poorder = new List<PurchaseOrder>();
            try
            {
                poorder = _db.Query<PurchaseOrder>("USP_BIND_PACKSIZE_MRP_AND_RATE", new { P_PRODUCTID = Productid,
                    P_TPUID= Vendorid,
                    P_FROMDATE= Podate
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return poorder;
        }

        public Pomrprateunit Loadunitmrprate(string Productid, string Vendorid, string Podate)
        {
            Pomrprateunit mrprateunit = new Pomrprateunit();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_PACKSIZE_MRP_AND_RATE", new
                {
                    P_PRODUCTID = Productid,
                    P_TPUID = Vendorid,
                    P_FROMDATE = Podate,
                }, commandType: CommandType.StoredProcedure);
                var unit = reader.Read<Pounit>().ToList();
                var mrp = reader.Read<Pomrp>().ToList();
                var rate = reader.Read<Porate>().ToList();
                mrprateunit.Pounit = unit;
                mrprateunit.Pomrp = mrp;
                mrprateunit.Porate = rate;
            }
            catch(Exception ex)
            {

            }
            return mrprateunit;
        }

        
        public Pomaxminlastrate LoadmaxminlastRate(string Productid, string Vendorid, string Podate)
        {
            Pomaxminlastrate maxminlast = new Pomaxminlastrate();
            try
            {
                var finyear = "2019-2020";
                var reader = _db.QueryMultiple("USP_BIND_PURCHASERATE_MAX_MIN_AVG", new
                {
                    P_PRODUCTID = Productid,
                    P_TPUID = Vendorid,
                    P_ENTRYDATE = Podate,
                    p_FINYEAR= finyear,
                }, commandType: CommandType.StoredProcedure);
                var last = reader.Read<Polasrate>().ToList();
                var max = reader.Read<Pomaxrate>().ToList();
                var min = reader.Read<Pominrate>().ToList();
                var avg = reader.Read<Poavgrate>().ToList();
                maxminlast.Polasrate = last;
                maxminlast.Pomaxrate = max;
                maxminlast.Pominrate = min;
                maxminlast.Poavgrate = avg;
            }
            catch(Exception ex)
            {

            }
            return maxminlast;
        }
        public List<PurchaseOrder> chckproduct(string productid, string vendorid)
        {
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            try
            {
                purchaseOrders = _db.Query<PurchaseOrder>("usp_product_with_vendor_check", new
                {
                    p_productid = productid,
                    p_vendorid = vendorid,
                    p_mode = "1",
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return purchaseOrders;
        }

        public List<PurchaseOrder> BindTerms()
        {
            List<PurchaseOrder> terms = new List<PurchaseOrder>();
            try
            {
                terms = _db.Query<PurchaseOrder>("USP_BIND_PODETAILS_BASED_ONPONO", new
                {
                    P_PONO = "",
                    P_FINYEAR = "",
                    P_MODE = "3",
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return terms;
        }

        public List<MessageModel> PoInsertUpdate(PurchaseOrder po)
        {
            DataTable dt;
            dt = Poformdtl(po.PurchaseOrderdetails);
            PurchaseOrder poModel = new PurchaseOrder();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("Usp_Po_Insert_Update_Factory", new
                {
                    flag = "",
                    editpoid = po.Poid,
                    podate = po.Podate,
                    tpuid=po.Vendorid,
                    tpuname=po.Vendorname,
                    remarks=po.Remarks,
                    createdby=po.Createdby,
                    grosstotal=po.Totalgross,
                    adjustment=po.Totaladjusment,
                    discountpercent=po.Discper,
                    discount=po.Discamnt,
                    packingpercent=po.PACKINGPERCENTAGE,
                    packing=po.PACKING,
                    exercisepercent=po.EXERCISEPERCENTAGE,
                    exercise=po.EXERCISE,
                    saletaxpercent=po.SALETAXPERCENTAGE,
                    saletax=po.SALETAX,
                    othercharges=po.OTHERCHARGES,
                    totalamount=po.TOTALAMOUNT,
                    nettotal=po.NETTOTAL,
                    p_FINYEAR=po.FINYEAR,
                    P_MRPTOTAL=po.Totalmrp,
                    P_QSTAG=po.QSTAG,
                    P_MODULEID=po.MODULEID,
                    P_SHIPINGADDRESS=po.Shippingadress,
                    p_CURRENCYTYPE=po.CURRENCYTYPE,
                    p_CURRENCYID=po.Currencyid,
                    p_FACTORYID=po.Depotid,
                    P_REFERENCEPOID=po.REFERENCEPOID,
                    P_INDENTID=po.INDENTID,   
                    P_REFRENCENO =po.REFRENCENO,
                    p_TERMSID   =po.TERMSID,
                    P_QUOTDATE   =po.QUOTDATE,
                    p_rejectionnote =po.REJECTIONNOTE,
                    p_termsandcondition=po.Termscondition,
                    p_entryfrom  =po.ENTRYFROM,
                    p_ASSESMENT=po.ASSESSABLEPERCENT,
                    p_EXCISE  =po.EXERCISEPERCENTAGE,
                    p_CST   =po.EXERCISE,
                    P_nccformfillup = dt.AsTableValuedParameter("TYPEPoformdtl")

                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public DataTable Poformdtl(List<PurchaseOrderdetails> PurchaseOrderdetails)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("POID", typeof(string));
            dt.Columns.Add("CATEGORYID", typeof(string));
            dt.Columns.Add("CATEGORYName", typeof(string));
            dt.Columns.Add("DIVISIONID", typeof(string));
            dt.Columns.Add("DIVISIONName", typeof(string));
            dt.Columns.Add("NATUREOFPRODUCTID", typeof(string));
            dt.Columns.Add("NATUREOFPRODUCTNAME", typeof(string));
            dt.Columns.Add("UOMID", typeof(string));
            dt.Columns.Add("UOMName", typeof(string));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTName", typeof(string));
            dt.Columns.Add("QTY",typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("PRODUCTAMOUNT", typeof(decimal)); 
            dt.Columns.Add("REQUIREDDate", typeof(string)); 
            dt.Columns.Add("REQUIREDTODATE", typeof(string));  
            dt.Columns.Add("MRP", typeof(decimal)); 
            dt.Columns.Add("TOTMRP", typeof(decimal));
            dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(string));
            dt.Columns.Add("EXCISEPERCENTAGE", typeof(string));
            dt.Columns.Add("CSTPERCENTAGE", typeof(string));
            dt.Columns.Add("LASTRATE", typeof(decimal));
            dt.Columns.Add("MAXRATE", typeof(decimal));
            dt.Columns.Add("MINRATE", typeof(decimal));
            dt.Columns.Add("AVGRATE", typeof(decimal));
            dt.Columns.Add("CGSTTAXID", typeof(string));
            dt.Columns.Add("SGSTTAXID", typeof(string));
            dt.Columns.Add("IGSTTAXID", typeof(string));
            dt.Columns.Add("Cgst", typeof(decimal));
            dt.Columns.Add("Sgst", typeof(decimal));
            dt.Columns.Add("Igst", typeof(decimal));

            int count = 1;
            foreach (var item in PurchaseOrderdetails)
            {
                dt.Rows.Add(item.POID,
                            item.CATEGORYID,
                            item.CATEGORYName,
                            item.DIVISIONID,
                            item.DIVISIONName,
                            item.NATUREOFPRODUCTID,
                            item.NATUREOFPRODUCTNAME,
                            item.UOMID,
                            item.UOMName,
                            item.PRODUCTID,
                            item.PRODUCTName,
                            item.QTY,
                            item.RATE,
                            item.PRODUCTAMOUNT,
                            item.REQUIREDDate,
                            item.REQUIREDTODATE,
                            item.MRP,
                            item.TOTMRP,
                            item.ASSESMENTPERCENTAGE,
                            item.EXCISEPERCENTAGE,
                            item.CSTPERCENTAGE,
                            item.LASTRATE,
                            item.MAXRATE,
                            item.MINRATE,
                            item.AVGRATE,
                            item.CGSTTAXID,
                            item.SGSTTAXID,
                            item.IGSTTAXID,
                            item.Cgst,
                            item.Sgst,
                            item.Igst
                            );
                count++;
            }
            return dt;
        }
        public List<PodetailsGrid> BindComplainGrid(string FromDate, string ToDate, string Finyear, string QSTAG,string checker,string factoryid,string Potype,string userid)
        {
            List<PodetailsGrid> podetails = new List<PodetailsGrid>();
            try
            {
                podetails = _db.Query<PodetailsGrid>("USP_BIND_PURCHASE_ORDER", new { P_FROMDATE = FromDate, P_TODATE = ToDate, P_FINYEAR = Finyear, P_QSTAG = QSTAG, P_CHECKER= checker, P_DEPOTID= factoryid, P_POTYPE= Potype, P_USERID= userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return podetails;
        }

        public PodetailsEdit Editpurchaseorderdetails(string poid, string finyear)
        {
            PodetailsEdit poorder = new PodetailsEdit();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_PODDETAILS_EDIT_V2", new { P_POID = poid, P_FINYEAR = finyear }, commandType: CommandType.StoredProcedure);

                var pogrid = reader.Read<PurchaseOrderdetails>().ToList();
                var poedit = reader.Read<EditPo>().ToList();

                poorder.Podetails = pogrid;
                poorder.EditPos = poedit;

            }
            catch (Exception ex)
            {

            }
            return poorder; ;
        }

        public PurchaseOrder ApprovePurchaseOrder(string poid,string userid,string mode,string rejectionote)
        {
            PurchaseOrder approve = new PurchaseOrder();
            try
            {
                approve = _db.QueryFirstOrDefault<PurchaseOrder>("USP_APPROVED_REJECT_PO_FAC", new
                {
                    P_POID = poid,
                    p_userid = userid,
                    P_MODE = mode,
                    p_rejectionote= rejectionote,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return approve;
        }

        public PurchaseOrder DeletePurchaseOrder(string poid, string Finyear)
        {
            PurchaseOrder approve = new PurchaseOrder();
            try
            {
                approve = _db.QueryFirstOrDefault<PurchaseOrder>("SP_MM_TOTALPODELETE_V2", new
                {
                    p_poid = poid,
                    p_finyear = Finyear,

                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return approve;
        }

        
       public List<PurchaseOrder> CONVERTIONQTY(string productid, string unitid, string qty, string purchaserate,string mrp,string mode)
        {
            List<PurchaseOrder> RETURNAMOUNT = new List<PurchaseOrder>();
            try
            {
                RETURNAMOUNT = _db.Query<PurchaseOrder>("usp_convertion_qty_mrp_totoal", new
                {
                    p_productid = productid,
                    p_packsizeid = unitid,
                    p_qty = qty,
                    p_rate= purchaserate,
                    p_mrp= mrp,
                    p_mode= mode,
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return RETURNAMOUNT;
        }
        public List<PurchaseOrder> CONVERTIONQTYmrp(string productid, string unitid, string qty, string purchaserate, string mrp, string mode)
        {
            List<PurchaseOrder> RETURNAMOUNTmrp = new List<PurchaseOrder>();
            try
            {
                RETURNAMOUNTmrp = _db.Query<PurchaseOrder>("usp_convertion_qty_mrp_totoal", new
                {
                    p_productid = productid,
                    p_packsizeid = unitid,
                    p_qty = qty,
                    p_rate = purchaserate,
                    p_mrp = mrp,
                    p_mode = mode,
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return RETURNAMOUNTmrp;
        }

        public List<PurchaseOrder> Quotationupload_fileSaveUpdate(string poid, string filename,string filepath,string mode)
        {
            List<PurchaseOrder> upload = new List<PurchaseOrder>();
            try
            {
                upload = _db.Query<PurchaseOrder>("USP_UPLOAD_POQuotation_Comparative_Upload", new
                {
                    p_poid = poid,
                    p_filename = filename,
                    p_filepath= filepath,
                    P_MODE = mode,
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return upload;
        }

        public List<PurchaseOrder> FetchinfoComparative(string poid, string mode)
        {
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            try
            {
                purchaseOrders = _db.Query<PurchaseOrder>("usp_bind_uploadinfo_po ", new { p_poid = poid, p_mode = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchaseOrders;

        }

        public List<PurchaseOrder> DeleteUploadfile(string uploadid)
        {
            List<PurchaseOrder> delete = new List<PurchaseOrder>();
            try
            {
                delete = _db.Query<PurchaseOrder>("DELETE FROM T_MM_POQUOTATION WHERE UPLOADID='"+ uploadid + "'").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return delete;
        }


        /*GRN MM GST START*/
        public List<GrnMM> BindTPU(string FG,string depotid)
        {
            List<GrnMM> grnmm = new List<GrnMM>();
            try
            {
                grnmm = _db.Query<GrnMM>("USP_BIND_VENDOR_V2", new { P_Tag= FG, P_Depotid = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return grnmm;
        }

        public List<GrnMM> BindTpu_Transporter(string Vendorid)
        {
            List<GrnMM> grnmm = new List<GrnMM>();
            try
            {
                grnmm = _db.Query<GrnMM>("USP_BIND_TPU_TRANSPORTER_V2", new { P_Vendorid= Vendorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return grnmm;
        }

        public List<GrnMM> BindPoCloseAuto(string Vendorid,string finyear,string depotid)
        {
            List<GrnMM> grnmm = new List<GrnMM>();
            try
            {
                grnmm = _db.Query<GrnMM>("USP_PURCHASE_ORDER_AUTO_CLOSING", new { P_TPUID = Vendorid, P_FINYEAR = finyear, P_DEPOTID = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return grnmm;
        }

        public List<GrnMM> BindPoMM(string Vendorid, string depotid, string finyear)
        {
            List<GrnMM> grnmm = new List<GrnMM>();
            try
            {
                grnmm = _db.Query<GrnMM>("USP_BIND_PO_VENDORWISE_V2", new { P_TPUID = Vendorid,P_DEPOTID = depotid, P_FINYEAR = finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return grnmm;
        }
        public List<GrnMM> BindProductDetails(string poid, string pono,string isseproduct)
        {
            List<GrnMM> batchLists = new List<GrnMM>();
            List<ProductInfoGRNMM> batch = new List<ProductInfoGRNMM>();
            try
            {
                batch = _db.Query<ProductInfoGRNMM>("sp_COMBO_GRN_DETAILS_MM", new { PO_ID = poid, PO_NO = pono, ISSUEPRODUCT= isseproduct }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new GrnMM()
                    {
                        PRODUCT_ID=lst.PRODUCT_ID,
                        PRODUCT_NAME = lst.PRODUCT_NAME,
                        UNITID=lst.UNITID,
                        UNITNAME = lst.UNITNAME,
                        POQTY = lst.POQTY,
                        DESPATCHQTY = lst.DESPATCHQTY,
                        REMAININGQTY = lst.REMAININGQTY,
                        MRP = lst.MRP,
                        RATE = lst.RATE,
                    });
                }
            }

            catch (Exception ex)
            {

            }
            return batchLists;
        }

        public List<ProductInfo> Bindproductdtl(string POID, string productID, string packSizeID, string depotid, string TAG, string DespatchID, string Fromdt, string VendorId)
        {
            List<ProductInfo> batchLists = new List<ProductInfo>();
            List<ProductInfoBatch> batch = new List<ProductInfoBatch>();
            try
            {
                batch = _db.Query<ProductInfoBatch>("SP_PO_GRN_DETAILS_MMPP", new { p_POID = POID, p_PRODUCTID = productID, p_PACKSIZETO = packSizeID , p_DEPOTID = depotid , P_TAG = TAG , P_DESPATCHID = DespatchID , p_FROMDATE = Fromdt , p_VENDORID= VendorId }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var lst in batch)
                {
                    batchLists.Add(new ProductInfo()
                    {
                        PO_QTY=lst.PO_QTY,
                        RATE=lst.RATE,
                        MRP = lst.MRP,
                        ASSESSABLEPERCENT= lst.ASSESSABLEPERCENT,
                        TOTAL_ASSESMENT=lst.TOTAL_ASSESMENT,
                        WEIGHT=lst.WEIGHT,
                        DEPOTWISE_DESPATCH_QTY=lst.DEPOTWISE_DESPATCH_QTY,
                        DESPATCH_QTY=lst.DESPATCH_QTY,
                        PODATE=lst.PODATE,
                        HSE=lst.HSE,
                        PRODUCTNAME=lst.PRODUCTNAME,
                        CATID=lst.CATID,
                    });
                }
            }

            catch (Exception ex)
            {

            }
            return batchLists;
        }
        public List<TaxcountListMM> GetTaxcount(string MenuID, string Flag, string DepotID, string ProductID, string CustomerID, string Date)
        {
            List<TaxcountListMM> taxcountlist = new List<TaxcountListMM>();
            List<TaxcountMM> taxcount = new List<TaxcountMM>();
            try
            {
                taxcount = _db.Query<TaxcountMM>("USP_TAXCOUNT_MM_V2", new { MENUID = MenuID, FLAG = Flag, DEPOTID = DepotID, PRODUCTID = ProductID, CUSTOMERID = CustomerID, DATE = Date }, commandType: CommandType.StoredProcedure).ToList();

                foreach (var lst in taxcount)
                {
                    taxcountlist.Add(new TaxcountListMM()
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

        public DataTable Grnformdtl(List<GrnOrderdetails> grnOrderdetails)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("POID", typeof(string));
            dt.Columns.Add("PODATE", typeof(string));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("PACKSIZEID", typeof(string));
            dt.Columns.Add("PACKSIZENAME", typeof(string));
            dt.Columns.Add("MRP", typeof(decimal));
            dt.Columns.Add("DESPATCHQTY", typeof(decimal));
            dt.Columns.Add("RECEIVEDQTY", typeof(decimal));
            dt.Columns.Add("REMAININGQTY", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("TOTMRP", typeof(decimal));
            dt.Columns.Add("BATCHNO", typeof(string));
            dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(decimal));
            dt.Columns.Add("TOTALASSESMENTVALUE", typeof(decimal));
            dt.Columns.Add("WEIGHT", typeof(string));
            dt.Columns.Add("mastermfgdate", typeof(string));
            dt.Columns.Add("masterexpdate", typeof(string));
            dt.Columns.Add("GROSSWEIGHT", typeof(string));
            dt.Columns.Add("ALLOCATEDQTY", typeof(decimal));
            dt.Columns.Add("DEPOTRATE", typeof(decimal));
            dt.Columns.Add("DISCOUNTPER", typeof(decimal));
            dt.Columns.Add("DISCOUNTAMT", typeof(decimal));
            dt.Columns.Add("AFTERDISCOUNTAMT", typeof(decimal));
            dt.Columns.Add("ITEMWISEFREIGHT", typeof(decimal));
            dt.Columns.Add("AFTERITEMWISEFREIGHTAMT", typeof(decimal));
            dt.Columns.Add("ITEMWISEADDCOST", typeof(decimal));
            dt.Columns.Add("AFTERITEMWISEADDCOSTAMT", typeof(decimal));
            dt.Columns.Add("BEFOREEXCHANGEAMT", typeof(decimal));


        int count = 1;
            foreach (var item in grnOrderdetails)
            {
                dt.Rows.Add(item.POID,
                            item.PODATE,
                            item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.PACKSIZEID,
                            item.PACKSIZENAME,
                            item.MRP,
                            item.POQTY,
                            item.RECEIVEDQTY,
                            item.REMAININGQTY,
                            item.RATE,
                            item.AMOUNT,
                            item.TOTMRP,
                            item.BATCHNO,
                            item.ASSESMENTPERCENTAGE,
                            item.TOTALASSESMENTVALUE,
                            item.WEIGHT,
                            item.mastermfgdate,
                            item.masterexpdate,
                            item.GROSSWEIGHT,
                            item.ALLOCATEDQTY,
                            item.DEPOTRATE,
                            item.DISCOUNTPER,
                            item.DISCOUNTAMT,
                            item.AFTERDISCOUNTAMT,
                            item.ITEMWISEFREIGHT,
                            item.AFTERITEMWISEFREIGHTAMT,
                            item.ITEMWISEADDCOST,
                            item.AFTERITEMWISEADDCOSTAMT,
                            item.BEFOREEXCHANGEAMT
            );
                count++;
            }
            return dt;
        }

        public List<MessageModel> GrnMMInsertUpdate(GrnMM grnMM, 
                                                       DataTable dtTaxDetails,DataTable dtTaxRejection,DataTable dtRejTax,DataTable dtSample,DataTable dtSampleQty)
        {
            DataTable dtDetails;
            dtDetails = Grnformdtl(grnMM.grnOrderdetails);
            GrnMM grnmodel = new GrnMM();
            List<MessageModel> response = new List<MessageModel>();
            try
            {

                response = _db.Query<MessageModel>("SP_GRN_INSERT_UPDATE_MMPP_V2",
                                                        new
                                                        {
                                                            p_RECEIVEDID           = grnMM.RECEIVEDID,
                                                            p_DESPATCHID           = grnMM.DESPATCHID,
                                                            p_FLAG                 = "A",
                                                            p_RECEIVEDDATE         =grnMM.DESPATCHDATE,
                                                            p_TPUID                =grnMM.VENDORID,
                                                            p_TPUNAME              =grnMM.VENDORNAME,
                                                            p_WAYBILLNO            =grnMM.WAYBILLKEY,
                                                            p_INVOICENO            =grnMM.INVOICENO,
                                                            p_INVOICEDATE          =grnMM.INVOICEDATE,
                                                            p_TRANSPORTERID        =grnMM.TRANSPORTERID,
                                                            p_VEHICLENO            =grnMM.VEHICLENO,
                                                            p_MOTHERDEPOTID        =grnMM.DEPOTID,
                                                            p_MOTHERDEPOTNAME      =grnMM.DEPOTNAME,
                                                            p_LRGRNO               =grnMM.LRGRNO,
                                                            p_LRGRDATE             =grnMM.LRGRDATE,
                                                            p_MODEOFTRANSPORT      = grnMM.MODEOFTRANSPORT,
                                                            p_CFORMNO              ="",
                                                            p_CFORMDATE            ="",
                                                            p_GATEPASSNO           = grnMM.GATEPASSNO,
                                                            p_GATEPASSDATE         = grnMM.GATEPASSDATE,
                                                            p_FORMFLAG             ="N",
                                                            p_CREATEDBY            = grnMM.Createdby,
                                                            p_FINYEAR              = grnMM.FINYEAR,
                                                            p_REMARKS              = grnMM.REMARKS,
                                                            p_TOTALDESPATCHVALUE   = grnMM.DESPATCHQTY,
                                                            p_OTHERCHARGESVALUE    = grnMM.OTHCHARGEAMT,
                                                            p_ADJUSTMENTVALUE      = grnMM.ADDITIONALCOST,
                                                            p_ROUNDOFFVALUE        = grnMM.ROUNDOFF,
                                                            p_TERMSID              ="",
                                                            P_DespatchDetails      =dtDetails.AsTableValuedParameter("TYPEGrnformdtl"),
                                                            P_ItemWiseTaxDetails   =dtTaxDetails.AsTableValuedParameter("TAXDETAILS_GRN_MM"),
                                                            P_RejectionDetails     =dtTaxRejection.AsTableValuedParameter("REJECTIONDETAILS__MM"),
                                                            p_ADDNAMOUNT           =grnMM.ADDITIONALCOST,
                                                            p_INSURANCECOMPID      ="",
                                                            p_INSURANCECOMPNAME    ="",
                                                            p_INSURANCENUMBER      ="",
                                                            P_MODULEID             = grnMM.MODULEID,
                                                            P_INVOICETYPE          = grnMM.INVOICETYPE,
                                                            P_ISVERIFIEDCHECKER1   = grnMM.ISVERIFIEDCHECKER1,
                                                            P_ISVERIFIEDSTOCKIN    = grnMM.ISVERIFIEDSTOCKIN,
                                                            p_RejectionTaxDetails  = dtRejTax.AsTableValuedParameter("REJTAXDETAILS_GRN_MM"),
                                                            P_TOTALITEMWISEFREIGHT = grnMM.TOTALITEMWISEFREIGHT,
                                                            P_TOTALITEMWISEADDCOST = grnMM.TOTALITEMWISEADDCOST,
                                                            P_TOTALITEMWISEDISCOUNT= grnMM.TOTALITEMWISEDISCOUNT,
                                                            P_LEDGERID             = grnMM.LEDGERID,
                                                            p_WAYBILLDT            = grnMM.WAYBILLDT,
                                                            P_CAPACITYUPLOAD       = grnMM.CAPACITYUPLOAD,
                                                            P_VENDORFROM           = grnMM.VENDORFROM,
                                                            P_GRN                  = "Y",
                                                            @P_SampleQty           = dtSample.AsTableValuedParameter("STOCKRECEIVED_SAMPLEQTY"),
                                                            @P_SampleQtyFileUpload = dtSampleQty.AsTableValuedParameter("UPLOADCAPACITYFILE")
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


        public CalcualteTaxWithAmount GetCalculateAmtInPcs(string depoid, string poid, string pono, string podate, string hsncode, 
            string productid, string productname, string packsizeid, string packsizename, decimal mrp, decimal despatchqty, 
            decimal qcqty, decimal rate, decimal itemwisefreight, decimal itemwiseaddcost, string batchno, string FG,
            string invoicedate, string tpu, string mfgdt, string expdt, decimal alreadydespatchqty, decimal assessmentpercent,string finyear,string taxnamecgst, string taxnamesgst, string taxnameigst)
        {
            CalcualteTaxWithAmount CalculateTaxWith = new CalcualteTaxWithAmount();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_PRODUCT_WITH_GRN_MM_V2", new
                {
                    P_depoid = depoid,
                    P_poid = poid,
                    P_pono = pono,
                    P_podate = podate,
                    P_hsncode = hsncode,
                    P_productid = productid,
                    P_productname = productname,
                    P_packsizeid = packsizeid,
                    P_packsizename = packsizename,
                    P_mrp = mrp,
                    P_despatchqty = despatchqty,
                    P_qcqty = qcqty,
                    P_rate = rate,
                    P_itemwisefreight = itemwisefreight,
                    P_itemwiseaddcost = itemwiseaddcost,
                    P_batchno = batchno,
                    P_Tag = "FG",
                    P_invoicedate = invoicedate,
                    P_tpu = tpu,
                    P_mfgdt = mfgdt,
                    P_expdt = expdt,
                    P_alreadydespatchqty = alreadydespatchqty,
                    P_assessmentpercent = assessmentpercent,
                    P_despatchid="",
                    P_finyear = finyear,
                    P_TAXNAMECGST=taxnamecgst,
                    P_TAXNAMESGST = taxnamesgst,
                    P_TAXNAMEIGST = taxnameigst,

                }, commandType: CommandType.StoredProcedure);

                var v_ProductInfoBatch = reader.Read<ProductInfoBatch>().ToList();
                var v_masterbatch = reader.Read<MASTERBATCH>().ToList();
                var v_product_returnamount = reader.Read<PRODUCT_RETURNAMOUNT>().ToList();
                var v_product_amount = reader.Read<PRODUCT_AMOUNT>().ToList();
                var v_product_discount = reader.Read<PRODUCT_DISCOUNT>().ToList();
                var v_product_netgrossweight = reader.Read<PRODUCT_NETGROSSWEIGHT>().ToList();
                var v_product_grossweight = reader.Read<PRODUCT_GROSSWEIGHT>().ToList();
                var vcgsttax = reader.Read<CGSTPercentagemm>().ToList();
                var vsgsttax = reader.Read<SGSTPercentagemm>().ToList();
                var vigsttax = reader.Read<IGSTPercentagemm>().ToList();
                var vcgstid = reader.Read<CGSTmm>().ToList();
                var vsgstid = reader.Read<SGSTmm>().ToList();
                var vigstid = reader.Read<IGSTmm>().ToList();


                CalculateTaxWith.productInfoBatches = v_ProductInfoBatch;
                CalculateTaxWith.masterbatch = v_masterbatch;
                CalculateTaxWith.product_returnamount = v_product_returnamount;
                CalculateTaxWith.product_amount = v_product_amount;
                CalculateTaxWith.product_discount = v_product_discount;
                CalculateTaxWith.product_netgrossweight = v_product_netgrossweight;
                CalculateTaxWith.product_grossweight = v_product_grossweight;
                CalculateTaxWith.cgstpercentagemm = vcgsttax;
                CalculateTaxWith.cgstmm = vcgstid;
                CalculateTaxWith.sgstpercentagemm = vsgsttax;
                CalculateTaxWith.sgstmm = vsgstid;
                CalculateTaxWith.igstpercentagemm = vigsttax;
                CalculateTaxWith.igstmm = vigstid;

            }
            catch (Exception ex)
            {

            }
            return CalculateTaxWith;
        }
        public List<LoadGrn> BindReceived(string fromdate, string todate, string Depotid, string Finyear, string CheckerFlag, string userid, string TPUFLAG, string OP)
        {
            List<LoadGrn> grndetails = new List<LoadGrn>();
            try
            {
                grndetails = _db.Query<LoadGrn>("USP_GRN_SEARCH_MM ", new { P_FROMDATE = fromdate, P_TODATE = todate, P_DEPOTID = Depotid, P_FINYEAR = Finyear, P_CHECKERFLAG = CheckerFlag, P_USERID = userid, P_TPUFLAG = TPUFLAG, P_OP = OP }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grndetails;
        }

        public GrnEditMM EditReceivedDetails(string grnid,string mode)
        {
            GrnEditMM EditGrn = new GrnEditMM();
            try
            {
                var reader = _db.QueryMultiple("SP_RECEIVED_DETAILS_MMPP_V2", new { P_RECEIVEDID = grnid, P_MODE = "1" }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<GRN_EDIT_STOCKRECEIVEDHEADER>().ToList();
                var vdetails = reader.Read<GRN_EDIT_STOCKRECEIVEDDETAILS>().ToList();
                var vtaxcount = reader.Read<GRN_EDIT_TAXCOMPONENTCOUNT>().ToList();
                var vfooter = reader.Read<GRN_EDIT_STOCKRECEIVEDFOOTER>().ToList();
                var vRetax = reader.Read<GRN_EDIT_STOCKRECEIVEDTAX>().ToList();
                var vRecTerms = reader.Read<GRN_EDIT_STOCKRECEIVEDTERMS>().ToList();
                var vItemWiseTax = reader.Read<GRN_EDIT_STOCKRECEIVEDITEMWISETAX>().ToList();
                var vRejDetails = reader.Read<GRN_EDIT_STOCKRECEIVEDREJECTIONDETAILS>().ToList();
                var vAddDetails = reader.Read<GRN_EDIT_GRNADDITIONALDETAILS>().ToList();
                var vRejTax = reader.Read<GRN_EDIT_STOCKRECEIVEDREJECTIONTAX>().ToList();
                var vJobRecDetails = reader.Read<GRN_EDIT_JOBORDERRECEIVEDDETAILS>().ToList();
                var vRecSamplQty = reader.Read<GRN_EDIT_STOCKRECEIVEDSAMPLEQTY>().ToList();
                var vRecSampleName = reader.Read<GRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAME>().ToList();



                EditGrn.gRN_EDIT_STOCKRECEIVEDHEADERs = vheader;
                EditGrn.gRN_EDIT_STOCKRECEIVEDDETAILs = vdetails;
                EditGrn.gRN_EDIT_TAXCOMPONENTCOUNTs = vtaxcount;
                EditGrn.gRN_EDIT_STOCKRECEIVEDFOOTERs = vfooter;
                EditGrn.gRN_EDIT_STOCKRECEIVEDTAXes = vRetax;
                EditGrn.gRN_EDIT_STOCKRECEIVEDTERMs = vRecTerms;
                EditGrn.gRN_EDIT_STOCKRECEIVEDITEMWISETAXes = vItemWiseTax;
                EditGrn.gRN_EDIT_STOCKRECEIVEDREJECTIONDETAILs = vRejDetails;
                EditGrn.gRN_EDIT_GRNADDITIONALDETAILs = vAddDetails;
                EditGrn.gRN_EDIT_STOCKRECEIVEDREJECTIONTAXes = vRejTax;
                EditGrn.gRN_EDIT_JOBORDERRECEIVEDDETAILs = vJobRecDetails;
                EditGrn.gRN_EDIT_STOCKRECEIVEDSAMPLEQTies = vRecSamplQty;
                EditGrn.gRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAMEs = vRecSampleName;
                

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return EditGrn;
        }
        public List<TaxOnEdit> GetGrnTaxOnEdit(string grnid, string TaxID, string ProductID, string BatchNo)
        {
            List<TaxOnEdit> Tax = new List<TaxOnEdit>();
            try
            {
                Tax = _db.Query<TaxOnEdit>("USP_GET_TAX_ON_EDIT_GRN ", new { P_STOCKRECEVID = grnid, P_TaxID = TaxID, P_ProductID = ProductID, P_Batchno = BatchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return Tax;
        }

        public List<GrnMM> CheckInvoiceNo(string invoiceno,string vendorid,string finyear)
        {
            List<GrnMM> INVOICENO = new List<GrnMM>();
            try
            {
                INVOICENO = _db.Query<GrnMM>("usp_invoiceno_checking", new
                {
                    p_invoiceno = invoiceno,
                    p_vendorid = vendorid,
                    p_finyear = finyear,
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return INVOICENO;
        }

        public GrnMM DeleteStockDespatch(string stockreceivedid)
        {
            GrnMM grnMM = new GrnMM();
            try
            {
                grnMM = _db.QueryFirstOrDefault<GrnMM>("USP_STOCK_RECEIVED_DELETE_V2", new
                {
                    p_RECEIVEDID = stockreceivedid,
                    

                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return grnMM;
        }

        public List<GrnQc> GetFile_SR_Capacity(string grnid, string mode)
        {
            List<GrnQc> grnMMs = new List<GrnQc>();
            try
            {
                grnMMs = _db.Query<GrnQc>("USP_GetFile_SR_Capacity_INFO_V2 ", new { P_GRNID = grnid, P_MODE = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grnMMs;

        }

        /*approve*/
        public List<GrnMM> onlyBindNewFactory()
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            try
            {
                grnMMs = _db.Query<GrnMM>("usp_get_new_factory", new {}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grnMMs;

        }
        public List<GrnMM> getPendingPo(string FromDate, string ToDate, string Depotid, string FinYear)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            try
            {
                grnMMs = _db.Query<GrnMM>("usp_bind_pending_po_v2", new {p_fromdate= FromDate ,p_todate= ToDate ,p_depotid=Depotid,p_finyear= FinYear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grnMMs;

        }

        public List<messageresponse> updateApprove(string POID, string USERID)
        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_APPROVE_PO",
                                                           new
                                                           { p_poid = POID, p_userid= USERID }, commandType: CommandType.StoredProcedure).ToList();
            }
             catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }
    }
}
