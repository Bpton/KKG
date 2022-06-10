using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
 public   class TrandepotStkContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public List<Motherdepot> BindDepotBasedOnUser(string userid)
        {
            List<Motherdepot> Depomodel = new List<Motherdepot>();
            try
            {
                Depomodel = _db.Query<Motherdepot>("USP_BIND_MOTHERDEPOT", new { P_USERID = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Motherdepot> BindToDepo(string depotid)
        {
            List<Motherdepot> Depomodel = new List<Motherdepot>();
            try
            {
                Depomodel = _db.Query<Motherdepot>("USP_BIND_DESTDEPOT", new { P_DEPOTID = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Ordertype> BindOrderType()
        {
            List<Ordertype> Ordertype = new List<Ordertype>();
            try
            {
                Ordertype = _db.Query<Ordertype>("USP_BIND_ORDERTYPEDEPOT", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Ordertype;
        }
        public List<Insurancecodepot> Bindinscomp(string menuid)
        {
            List<Insurancecodepot> Insurancecodepot = new List<Insurancecodepot>();
            try
            {
                Insurancecodepot = _db.Query<Insurancecodepot>("USP_BIND_BINDINSURANCECOMPANY", new { P_MENUID = menuid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Insurancecodepot;
        }
        public List<Insurancenodepot> BindSTNInsuranceNumber(string companyid)
        {
            List<Insurancenodepot> Insurancenodepot = new List<Insurancenodepot>();
            try
            {
                Insurancenodepot = _db.Query<Insurancenodepot>("USP_BIND_POLICYNODEPOT", new { P_COMPANYID = companyid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Insurancenodepot;
        }
        public List<Waybill> BindWayBillNo(string depotid,string transferid)
        {
            List<Waybill> Waybill = new List<Waybill>();
            try
            {
                Waybill = _db.Query<Waybill>("USP_BIND_EWAYBILLDEPOT", new { P_DEPOTID = depotid, P_TRANSFERID = transferid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Waybill;
        }
        public List<Categorydepot> BindCATEGORY()
        {
            List<Categorydepot> Categorydepot = new List<Categorydepot>();
            try
            {
                Categorydepot = _db.Query<Categorydepot>("USP_BIND_BINDCATGORYSTOCKTRANSFER", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Categorydepot;
        }
        
        public List<Categorydepot> BindCATEGORYBOS()
        {
            List<Categorydepot> Categorydepot = new List<Categorydepot>();
            try
            {
                Categorydepot = _db.Query<Categorydepot>("USP_BIND_CATEGORYBOS_DEPOT", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Categorydepot;
        }
        public List<Countrystock> BindCountry()
        {
            List<Countrystock> Countrystock = new List<Countrystock>();
            try
            {
                Countrystock = _db.Query<Countrystock>("USP_BIND_COUNTRYDEPOT", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Countrystock;
        }
        public List<Saleorder> BindSaleOrder(string CountryID)
        {
            List<Saleorder> Saleorder = new List<Saleorder>();
            try
            {
                Saleorder = _db.Query<Saleorder>("USP_EXPORT_SALEORDER_DETAILS", new { p_COUNTRYID = CountryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Saleorder;
        }

        public List<Productdepot> LoadProduct(string depotid, string saleorderid,string typeid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("USP_BIND_BINDPRODUCTDEPOT", new { P_DEPOTID = depotid, p_SALEORDERID = saleorderid, P_TYPEID=typeid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }
        public List<Productdepot> BindProduct_depotwise(string depotid,string todepotid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("Sp_rpt_stocktransfer_product_loadv2", new { P_fromdepot = depotid, P_todepot = todepotid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }
        //load product bos
        public List<Productdepot> LoadProductBOS(string depotid, string saleorderid, string typeid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("USP_BIND_BINDPRODUCTDEPOT_BOS", new { P_DEPOTID = depotid, p_SALEORDERID = saleorderid, P_TYPEID = typeid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }
        public List<Productdepot> LoadCategoryWiseProduct(string catid, string depotid, string saleorderid, string typeid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("USP_BIND_PRODUCTCATWISEDEPOT", new { P_CATID = catid,  P_DEPOTID = depotid, p_SALEORDERID = saleorderid, P_TYPEID = typeid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }
        public List<Productdepot> LoadCategoryWiseProductBOS(string catid, string depotid, string saleorderid, string typeid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("USP_BIND_PRODUCTCATWISE_BOS_DEPOT", new { P_CATID = catid, P_DEPOTID = depotid, p_SALEORDERID = saleorderid, P_TYPEID = typeid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }
        public List<Transporterdepot> BindTranspoter(string depotid)
        {
            List<Transporterdepot> Transporterdepot = new List<Transporterdepot>();
            try
            {
                Transporterdepot = _db.Query<Transporterdepot>("USP_BIND_TRANSPORTERDEPOT", new { P_DEPOTID = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterdepot;
        }
        public List<Deliveryaddress> ShippingAddress(string depotid)
        {
            List<Deliveryaddress> Deliveryaddress = new List<Deliveryaddress>();
            try
            {
                Deliveryaddress = _db.Query<Deliveryaddress>("USP_BIND_SHIPINGADDRESSDEPOT", new { P_DEPOTID = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Deliveryaddress;
        }
        public List<Transitdays> DeliveryDay(string fromdepot,string todepoid,string invoicedate)
        {
            List<Transitdays> Transitdays = new List<Transitdays>();
            try
            {
                Transitdays = _db.Query<Transitdays>("USP_CHECK_DELIVERY_DATE", new { P_FROMDEPOT = fromdepot , P_TODEPOT = todepoid , P_INVOICEDATE = invoicedate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transitdays;
        }
        public List<ProductType> ProductTypeChecking(string ProductID)
        {
            List<ProductType> ProductType = new List<ProductType>();
            try
            {
                ProductType = _db.Query<ProductType>("USP_PRODUCT_CHECKING", new { P_PRODUCTID = ProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return ProductType;
        }
        public List<Packsizedepot> BindPackSize_productwise(string productid)
        {
            List<Packsizedepot> Packsize = new List<Packsizedepot>();
            try
            {
                Packsize = _db.Query<Packsizedepot>("Sp_product_packsize_load", new { P_PRODUCTID = productid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Packsize;
        }
        public List<Batchdetail> BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            try
            {
                Batchdetail = _db.Query<Batchdetail>("SP_BATCHWISE_DEPOT_STOCK", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = PacksizeID, P_BATCHNO = BatchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Batchdetail;
        }
        public List<Batchdetail> BindRM_PM_BatchDetails(string DepotID, string ProductID, string BatchNo, string StoreLocation)
        {
            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            try
            {
                Batchdetail = _db.Query<Batchdetail>("USP_RM_PM_DEPOT_STOCK_BATCH", new { DEPOTID = DepotID, PRODUCTID = ProductID, P_BATCHNO = BatchNo, P_STORELOCATION = StoreLocation }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Batchdetail;
        }
        public List<DepotRatesheet> BindDepotRate(string Productid, decimal MRP, string Date)
        {
            List<DepotRatesheet> DepotRatesheet = new List<DepotRatesheet>();
            try
            {
                DepotRatesheet = _db.Query<DepotRatesheet>("USP_RATEBATCHWISE_DEPOT", new { P_PRODUCTID = Productid, P_MRP = MRP, P_MFGDATE = Date }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return DepotRatesheet;
        }
        public CalculateAmtInPcsFAC GetCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate,string date,decimal _qty,string taxname)
        {
            CalculateAmtInPcsFAC CalculateAmtInPcs = new CalculateAmtInPcsFAC();
            try
            {
                var reader = _db.QueryMultiple("USP_CALCULATE_AMT_IN_PCS_V3", new
                {
                    p_PRODUCTID = Productid,
                    p_FROMPACKSIZEID = PacksizeID,
                    p_QTY = Qty,
                    p_RATE = Rate,
                    P_DATE = date,
                    p_transferqty = _qty,
                    P_TAXNAME = taxname,

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

        public List<MessageModel> Depotstocktransave(Depostockmodel Depostockmodel,
                                                       int CreatedBy, string Finyear,  
                                                       string ModuleID, string EntryFrom, 
                                                       DataTable dtTaxDetails)
        {
            DataTable dtDetails;
            DataTable dtFree;
            dtFree = GTFreeDetails(Depostockmodel.Freestock);
            dtDetails = Dispatchdetails(Depostockmodel.Depotstockdtl);
            FactoryDispatchModel fgDispatchmodel = new FactoryDispatchModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_STOCKTRANSFER_INSERT_UPDATE_V2 ",
                                                        new
                                                        {
                                                            p_STOCKTRANSFERID = Depostockmodel.STOCKTRANSFERID,
                                                            p_FLAG = Depostockmodel.FLAG,
                                                            p_TRANSFERDATE = Depostockmodel.TRANSFERDATE,
                                                            p_MOTHERDEPOTID = Depostockmodel.MOTHERDEPOTID,
                                                            p_MOTHERDEPOTNAME = Depostockmodel.MOTHERDEPOTNAME,
                                                            p_TODEPOTID = Depostockmodel.TODEPOTID,
                                                            p_TODEPOTNAME = Depostockmodel.TODEPOTNAME,
                                                            p_WAYBILLKEY = Depostockmodel.WAYBILLNO,
                                                            p_WAYBILLAPPLICABLE = Depostockmodel.WAYBILLAPPLICABLE,
                                                            p_INSURANCENO = Depostockmodel.INSURANCENO,
                                                            p_TRANSPORTERID = Depostockmodel.TRANSPORTERID,
                                                            p_TRANSPORTERNAME = Depostockmodel.TRANSPORTERNAME,
                                                            p_MODEOFTRANSPORT = Depostockmodel.MODEOFTRANSPORT,
                                                            p_VEHICHLENO = Depostockmodel.VEHICLENO,
                                                            p_LRGRNO = Depostockmodel.LRGRNO,
                                                            p_LRGRDATE = Depostockmodel.LRGRDATE,
                                                            p_CHALLENNO = Depostockmodel.CHALLANNO,
                                                            p_CHALLENDATE = Depostockmodel.CHALLANDATE,
                                                            p_FFORM = "",
                                                            p_GATEPASSNO = Depostockmodel.GATEPASSNO,
                                                            p_GATEPASSDATE = Depostockmodel.GATEPASSDATE,
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_INSUARANCECOMPID = Depostockmodel.INSURANCECOMPID,
                                                            p_INSUARANCECOMPNAME = Depostockmodel.INSURANCECOMPNAME,
                                                            p_REMARKS = Depostockmodel.REMARKS,
                                                            P_MODULEID = ModuleID,
                                                            p_ORDERTYPEID = Depostockmodel.ORDERTYPE,
                                                            p_ORDERTYPENAME = Depostockmodel.ORDERTYPENAME,
                                                            p_COUNTRYID = Depostockmodel.COUNTRYID,
                                                            p_COUNTRYNAME = Depostockmodel.COUNTRYNAME,
                                                            p_SALEORDERID = Depostockmodel.SALEORDERID,
                                                            p_SALEORDERNO = Depostockmodel.SALEORDERNO,
                                                            p_TOTALCASE = Depostockmodel.TotalCase,
                                                            p_TOTALPCS = Depostockmodel.TotalPcs,
                                                            P_GROSSAMOUNT = Depostockmodel.GrossAmt,
                                                            P_NETAMOUNT = Depostockmodel.NetAmt,
                                                            P_TOTALTAXAMT = Depostockmodel.TOTALTAXAMT,
                                                            P_BASICAMT = Depostockmodel.BASICAMT,
                                                            P_ROUNDOFF = Depostockmodel.RoundOff,
                                                            P_INVOICE_TYPE = Depostockmodel.INVOICETYPE,
                                                            P_EXPORT = Depostockmodel.EXPORT,
                                                            P_SHIPPING_ADDRESS = Depostockmodel.SHIPINGADDRESS,
                                                            P_DELIVERYDATE = Depostockmodel.DELIVERYDATE,
                                                            P_TAXFLAG = Depostockmodel.TAXCOUNT,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_ENTRY_FROM = EntryFrom
                                                            
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public List<MessageModel> DepotstocktranBOSsave(Depostockmodel Depostockmodel,
                                                      int CreatedBy, string Finyear,
                                                      string ModuleID, string EntryFrom,
                                                      DataTable dtTaxDetails)
        {
            DataTable dtDetails;
            DataTable dtFree;
            dtFree = GTFreeDetails(Depostockmodel.Freestock);
            dtDetails = Dispatchdetails(Depostockmodel.Depotstockdtl);
            FactoryDispatchModel fgDispatchmodel = new FactoryDispatchModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_STOCKTRANSFER_INSERT_UPDATE_V2 ",
                                                        new
                                                        {
                                                            p_STOCKTRANSFERID = Depostockmodel.STOCKTRANSFERID,
                                                            p_FLAG = Depostockmodel.FLAG,
                                                            p_TRANSFERDATE = Depostockmodel.TRANSFERDATE,
                                                            p_MOTHERDEPOTID = Depostockmodel.MOTHERDEPOTID,
                                                            p_MOTHERDEPOTNAME = Depostockmodel.MOTHERDEPOTNAME,
                                                            p_TODEPOTID = Depostockmodel.TODEPOTID,
                                                            p_TODEPOTNAME = Depostockmodel.TODEPOTNAME,
                                                            p_WAYBILLKEY = Depostockmodel.WAYBILLNO,
                                                            p_WAYBILLAPPLICABLE = Depostockmodel.WAYBILLAPPLICABLE,
                                                            p_INSURANCENO = Depostockmodel.INSURANCENO,
                                                            p_TRANSPORTERID = Depostockmodel.TRANSPORTERID,
                                                            p_TRANSPORTERNAME = Depostockmodel.TRANSPORTERNAME,
                                                            p_MODEOFTRANSPORT = Depostockmodel.MODEOFTRANSPORT,
                                                            p_VEHICHLENO = Depostockmodel.VEHICLENO,
                                                            p_LRGRNO = Depostockmodel.LRGRNO,
                                                            p_LRGRDATE = Depostockmodel.LRGRDATE,
                                                            p_CHALLENNO = Depostockmodel.CHALLANNO,
                                                            p_CHALLENDATE = Depostockmodel.CHALLANDATE,
                                                            p_FFORM = "",
                                                            p_GATEPASSNO = Depostockmodel.GATEPASSNO,
                                                            p_GATEPASSDATE = Depostockmodel.GATEPASSDATE,
                                                            p_CREATEDBY = CreatedBy,
                                                            p_FINYEAR = Finyear,
                                                            p_INSUARANCECOMPID = Depostockmodel.INSURANCECOMPID,
                                                            p_INSUARANCECOMPNAME = Depostockmodel.INSURANCECOMPNAME,
                                                            p_REMARKS = Depostockmodel.REMARKS,
                                                            P_MODULEID = ModuleID,
                                                            p_ORDERTYPEID = Depostockmodel.ORDERTYPE,
                                                            p_ORDERTYPENAME = Depostockmodel.ORDERTYPENAME,
                                                            p_COUNTRYID = Depostockmodel.COUNTRYID,
                                                            p_COUNTRYNAME = Depostockmodel.COUNTRYNAME,
                                                            p_SALEORDERID = Depostockmodel.SALEORDERID,
                                                            p_SALEORDERNO = Depostockmodel.SALEORDERNO,
                                                            p_TOTALCASE = Depostockmodel.TotalCase,
                                                            p_TOTALPCS = Depostockmodel.TotalPcs,
                                                            P_GROSSAMOUNT = Depostockmodel.GrossAmt,
                                                            P_NETAMOUNT = Depostockmodel.NetAmt,
                                                            P_TOTALTAXAMT = Depostockmodel.TOTALTAXAMT,
                                                            P_BASICAMT = Depostockmodel.BASICAMT,
                                                            P_ROUNDOFF = Depostockmodel.RoundOff,
                                                            P_INVOICE_TYPE = Depostockmodel.INVOICETYPE,
                                                            P_EXPORT = Depostockmodel.EXPORT,
                                                            P_SHIPPING_ADDRESS = Depostockmodel.SHIPINGADDRESS,
                                                            P_DELIVERYDATE = Depostockmodel.DELIVERYDATE,
                                                            P_TAXFLAG = Depostockmodel.TAXCOUNT,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_GT_INVOICE_DETAILS"),
                                                            TempTableFree = dtFree.AsTableValuedParameter("Type_GT_FREE_DETAILS"),
                                                            TempTableTax = dtTaxDetails.AsTableValuedParameter("Type_GT_INVOICE_TAX_DETAILS"),
                                                            P_ENTRY_FROM = EntryFrom

                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public DataTable Dispatchdetails(List<Depotstockdtl> Depotstockdtl)
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
            foreach (var item in Depotstockdtl)
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

       
        public DataTable GTFreeDetails(List<Freestock> FreeDetailsGT)
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
        public List<StocktransferGrid> Bindstocktransfer(string FromDate, string ToDate, string Finyear, string CheckerFlag, string userID, string DepotID, string Type)
        {
            List<StocktransferGrid> StocktransferGrid = new List<StocktransferGrid>();
            try
            {
                StocktransferGrid = _db.Query<StocktransferGrid>("USP_STOCKTRANSFERLIST_DEPOT", new {  P_FRMDATE = FromDate, P_TODATE = ToDate, P_FINYR = Finyear, P_CHECKER = CheckerFlag, P_USERID = userID, P_DEPOTID = DepotID, P_TYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StocktransferGrid;
        }
        public List<StocktransferGrid> Bindstocktransferbos(string FromDate, string ToDate, string Finyear, string CheckerFlag, string userID, string DepotID, string Type)
        {
            List<StocktransferGrid> StocktransferGrid = new List<StocktransferGrid>();
            try
            {
                StocktransferGrid = _db.Query<StocktransferGrid>("USP_STOCKTRANSFERLISTBOS_DEPOT", new { P_FRMDATE = FromDate, P_TODATE = ToDate, P_FINYR = Finyear, P_CHECKER = CheckerFlag, P_USERID = userID, P_DEPOTID = DepotID, P_TYPE = Type }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StocktransferGrid;
        }
        public List<MessageModel> deletedispatch(string DispatchID)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("SP_STOCKTRANSFER_DELETE_V2 ", new { P_STOCKTRANSFER_ID = DispatchID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public List<MessageModel> Confirmdispatch(string DispatchID)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_STOCK_TRANSFER_APPROVED_v2", new { P_STOCKTRANSFERID = DispatchID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public Stocktransferedit Stocktranedit(string DispatchID)
        {
            Stocktransferedit Stocktransferedit = new Stocktransferedit();
            try
            {
                var reader = _db.QueryMultiple("SP_STOCK_TRANSFER_DETAILS_v2", new { P_STOCKTRANSFERID = DispatchID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<Stocktransferhdredit>().ToList();
                var vdetails = reader.Read<Stocktransferdtledit>().ToList();
                var vtaxcount = reader.Read<Stocktransfertaxcountedit>().ToList();
                var vtaxdetails = reader.Read<Stocktransfertaxedit>().ToList();
                var vfooter = reader.Read<Stocktransferfooteredit>().ToList();

                Stocktransferedit.Stocktransferhdredit = vheader;
                Stocktransferedit.Stocktransferdtledit = vdetails;
                Stocktransferedit.Stocktransfertaxcountedit = vtaxcount;
                Stocktransferedit.Stocktransfertaxedit = vtaxdetails;
                Stocktransferedit.Stocktransferfooteredit = vfooter;
            }
            catch (Exception ex)
            {

            }
            return Stocktransferedit;
        }
        public QuantityDetails CalculateQuantity(string Productid, string FromPacksizeID, decimal Qty)
        {
            QuantityDetails Quantity = new QuantityDetails();
            try
            {
                var reader = _db.QueryMultiple("USP_TOTAL_QTY_depot", new
                {
                    P_PRODUCTID = Productid,
                    P_FROM_PACKSIZE = FromPacksizeID,
                 
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
        public List<Stocktransfertax> GetTaxid(string taxname)
        {
            List<Stocktransfertax> Stocktransfertax = new List<Stocktransfertax>();
            try
            {
                Stocktransfertax = _db.Query<Stocktransfertax>("USP_Get_Taxname ", new { P_taxname = taxname }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Stocktransfertax;
        }
        public List<Stocktransfertax> GetHSNTaxOnEdit(string transferid,string taxid,string productid,string batchno)
        {
            List<Stocktransfertax> Stocktransfertax = new List<Stocktransfertax>();
            try
            {
                Stocktransfertax = _db.Query<Stocktransfertax>("USP_Get_Taxpercentage_v2 ", new { p_transferid = transferid, p_taxid = taxid, p_productod = productid, p_batchid = batchno }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Stocktransfertax;
        }
        ///inter batch transfer
        public List<Motherdepot> BindDepo(string depotid)
        {
            List<Motherdepot> Depomodel = new List<Motherdepot>();
            try
            {
                Depomodel = _db.Query<Motherdepot>("USP_BIND_DEPOT_INTERBATCH", new { P_depotid = depotid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Transporterdepot> BindStorelocation()
        {
            List<Transporterdepot> store = new List<Transporterdepot>();
            try
            {
                store = _db.Query<Transporterdepot>("USP_BIND_STORE_INTERBATCH", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return store;
        }
        public List<Transporterdepot> LoadReason(string menuid)
        {
            List<Transporterdepot> reason = new List<Transporterdepot>();
            try
            {
                reason = _db.Query<Transporterdepot>("USP_BIND_REASONS_INTERBATCH", new { p_menuid = menuid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return reason;
        }
        public List<Productdepot> BindProduct(string depotid)
        {
            List<Productdepot> Productdepot = new List<Productdepot>();
            try
            {
                Productdepot = _db.Query<Productdepot>("SP_DEPOTWISE_PRODUCT", new { P_DEPOTID = depotid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdepot;
        }

        public List<DepotRatesheet> GetToProductBCP(string Productid, decimal MRP)
        {
            List<DepotRatesheet> DepotRatesheet = new List<DepotRatesheet>();
            try
            {
                DepotRatesheet = _db.Query<DepotRatesheet>("SP_CALCULATE_DEPOT_RATE_v2", new { p_MRP = MRP, p_PRODUCTID = Productid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return DepotRatesheet;
        }
        public List<Productdtlinterbatch> GetToProductdetails(string Productid)
        {
            List<Productdtlinterbatch> Productdtlinterbatch = new List<Productdtlinterbatch>();
            try
            {
                Productdtlinterbatch = _db.Query<Productdtlinterbatch>("USP_BIND_TOPRODUCTDTL_INTERBATCH", new { p_PRODUCTID = Productid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdtlinterbatch;
        }
        public List<Productdtlinterbatch> GetfrmProductdetails(string Productid,string batchno)
        {
            List<Productdtlinterbatch> Productdtlinterbatch = new List<Productdtlinterbatch>();
            try
            {
                Productdtlinterbatch = _db.Query<Productdtlinterbatch>("USP_PRODUCT_DETAILS_INTERBATCH", new { p_PRODUCTID = Productid, p_BATCHNO = batchno }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Productdtlinterbatch;
        }
        public List<Batchdetail> BindBatchDetailsInterbatch(string DepotID, string ProductID, string PacksizeID, string BatchNo,decimal MRP,string storelocation)
        {
            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            try
            {
                Batchdetail = _db.Query<Batchdetail>("SP_BATCHWISE_DEPOT_STOCK", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = PacksizeID, P_BATCHNO = BatchNo,  P_MRP = MRP, P_STOCKLOCATION = storelocation }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Batchdetail;
        }
      
        public List<Batchdetail> LoadToBatchDetails(string ProductID, string DepotID, string BatchNo, string FromDate, string ToDate, string StoreLocation)
        {
            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            try
            {
                Batchdetail = _db.Query<Batchdetail>("USP_MFG_EXP_STOCK", new { P_DEPOTID = DepotID, P_PRODUCTID = ProductID, P_PACKSIZEID = "B9F29D12-DE94-40F1-A668-C79BF1BF4425", P_BATCHNO = BatchNo, P_FROMDATE = FromDate, P_TODATE = ToDate, P_MRP = 0, P_STOCKLOCATION = StoreLocation }, commandType: CommandType.StoredProcedure).ToList();
                }
            catch (Exception ex)
            {

            }
            return Batchdetail;
        }


    }
}
