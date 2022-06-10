using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;
using System.Web.UI;


namespace FactoryDatacontext
{
    public class saleOrderContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public List<bstype> loadBusinesssegment(string bstype,string mode)
        {
            List<bstype> saleOrders = new List<bstype>();
            try
            {
                saleOrders = _db.Query<bstype>("USP_BIND_LOAD_V2", new { P_bsid = bstype, P_mode= mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }

        public List<DeliveryTerms> BindDeliveryTerms(string mode)
        {
            List<DeliveryTerms> deliveryTerms = new List<DeliveryTerms>();
            try
            {
                deliveryTerms = _db.Query<DeliveryTerms>("USP_BIND_LOAD_V2", new { P_mode = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return deliveryTerms;
        }

        public List<LoadSaleOrderGroup> BindGroup(string BSID,string USERID,string FLAG,string mode)
        {
            List<LoadSaleOrderGroup> loadPrincipleGroups = new List<LoadSaleOrderGroup>();
            try
            {
                loadPrincipleGroups = _db.Query<LoadSaleOrderGroup>("usp_bind_principle_group_saleorder_V2", new { p_bsid = BSID, p_UserID= USERID, p_checkerflag = FLAG,p_mode=mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return loadPrincipleGroups;
        }

        public List<LoadSaleOrderGroup> BindDeliveryTerms(string mode,string userid)
        {
            List<LoadSaleOrderGroup> GROUPID = new List<LoadSaleOrderGroup>();
            try
            {
                GROUPID = _db.Query<LoadSaleOrderGroup>("USP_BIND_LOAD_V2", new { P_mode = mode,p_userid= userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return GROUPID;
        }

        public List<Currencey> BindCurrency(string groupid)
        {
            List<Currencey> currenceys = new List<Currencey>();
            try
            {
                currenceys = _db.Query<Currencey>("usp_bind_currencey_saleorder_v2", new {p_groupid= groupid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return currenceys;
        }

        public List<Customer> BindCustomers(string bstype, string groupid, string sessionDepotid, string saleorderid)
        {
            List<Customer> currenceys = new List<Customer>();
            try
            {
                currenceys = _db.Query<Customer>("usp_bind_customer_saleorder_v2", new { p_bsID = bstype, p_groupid = groupid , p_DepotID = sessionDepotid , p_SALEORDERID = saleorderid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return currenceys;
        }

        public List<ProductSaleOrder> BindProduct(string bstype, string groupid,string saleorderid)
        {
            List<ProductSaleOrder> productSaleOrders = new List<ProductSaleOrder>();
            try
            {
                productSaleOrders = _db.Query<ProductSaleOrder>("usp_bind_product_saleorder_add_edit", new { p_bstype = bstype, p_groupid = groupid,p_SALEORDERID = saleorderid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return productSaleOrders;
        }
        public List<saleOrderModel> BindBatchDetails(string depotid, string productId, string packSizeId, string batchNo)
        {
            List<saleOrderModel> INVOICESTOCKQTY = new List<saleOrderModel>();
            try
            {
                INVOICESTOCKQTY = _db.Query<saleOrderModel>("SP_BATCHWISE_DEPOT_STOCK_v2", new { P_DEPOTID = depotid, P_PRODUCTID = productId, P_PACKSIZEID = packSizeId, P_BATCHNO= batchNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return INVOICESTOCKQTY;
        }

        public List<saleOrderModel> BindPackingSizeconversionqty(string ProductID, string packidfrom, decimal qty)
        {
            List<saleOrderModel> conversionqty = new List<saleOrderModel>();
            try
            {
                conversionqty = _db.Query<saleOrderModel>("usp_get_convertion_qty_v2", new { P_ProductID = ProductID, P_packidfrom = packidfrom, P_qty = qty}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return conversionqty;
        }
        public List<saleOrderModel> BindExportBatchDetails(string depotid, string productId, string packSizeId, string batchNo,string mrp,string storeLocationId)
        {
            List<saleOrderModel> INVOICESTOCKQTY = new List<saleOrderModel>();
            try
            {
                INVOICESTOCKQTY = _db.Query<saleOrderModel>("SP_BATCHWISE_DEPOT_STOCK_v2", new { P_DEPOTID = depotid, P_PRODUCTID = productId, P_PACKSIZEID = packSizeId, P_BATCHNO = batchNo, P_MRP= mrp, P_STOCKLOCATION= storeLocationId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return INVOICESTOCKQTY;
        }

        public List<PACKSIZEName> BindPackingSize(string productId, string mode)
        {
            List<PACKSIZEName> pACKSIZENames = new List<PACKSIZEName>();
            try
            {
                pACKSIZENames = _db.Query<PACKSIZEName>("usp_bind_packsize_with_mode_v2", new { p_productId = productId,p_mode= mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return pACKSIZENames;
        }
        public List<saleOrderModel> GetBaseCostPrice(string customerId, string productId, string YMD, string mrp, string depotid, string menuID, string BSType, string groupId)
        {
            List<saleOrderModel> BASECOSTPRICE = new List<saleOrderModel>();
            try
            {
                BASECOSTPRICE = _db.Query<saleOrderModel>("USP_GETBCP_V2", new { CustomerID = customerId, ProductID = productId, DATE = YMD, MRP = mrp, DepoID = depotid, ModuleID = menuID, BusinessSegmentID = BSType, GroupID = groupId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return BASECOSTPRICE;
        }

        public List<saleOrderModel> AlreadyDeliveredQty(string productId, string Packsize, string saleOrderid)
        {
            List<saleOrderModel> DeliveredQty = new List<saleOrderModel>();
            try
            {
                DeliveredQty = _db.Query<saleOrderModel>("USP_QTYINPCS_ORDER", new { P_PRODUCTID = productId, P_PACKSIZEFROMID = Packsize, P_SALEORDERID = saleOrderid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DeliveredQty;
        }

        public List<Proform_status> ProformaAmount(string ProformaID)
        {
            List<Proform_status> ProformaAmount = new List<Proform_status>();
            try
            {
                ProformaAmount = _db.Query<Proform_status>("usp_ProformaAmount_saleOrder", new { P_ProformaID = ProformaID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return ProformaAmount;
        }

        public List<saleOrderModel> bindReason(string menuId)
        {
            List<saleOrderModel> reason = new List<saleOrderModel>();
            try
            {
                reason = _db.Query<saleOrderModel>("usp_bindClaim_reason_v2", new { p_menuID = menuId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return reason;
        }

        public List<saleOrderModel> CalculateCasePcs(string mode, string Productid, string PacksizeID, decimal Qty)
        {
            List<saleOrderModel> RETURNVALUE = new List<saleOrderModel>();
            try
            {
                RETURNVALUE = _db.Query<saleOrderModel>("usp_get_TotalCasePack_TotalPCS_saleOrder_v2", new { P_mode = mode, p_Productid = Productid, p_PackSizeId = PacksizeID, p_Qty = Qty }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return RETURNVALUE;
        }

        public List<messageresponse> InsertSaleOrderDetails(sale_order sale_, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_T_SALEORDER_v2",
                                                           new
                                                           {
                                                               p_SALEORDERID=sale_.saleOrderId,
                                                               p_FLAG = sale_.flag,
                                                               P_SALEORDERDATE = sale_.saleOrderDate,
                                                               P_REFSALEORDERNO = sale_.saleOrderNo,
                                                               P_REFSALEORDERDATE = sale_.refSaleOrderDate,
                                                               P_BSID = sale_.bsId,
                                                               P_BSNAME = sale_.bsName,
                                                               P_GROUPID = sale_.groupId,
                                                               P_GROUPNAME = sale_.groupName,
                                                               P_CUSTOMERID = sale_.customerId,
                                                               P_CUSTOMERNAME = sale_.customerName,
                                                               P_REMARKS = sale_.remarks,
                                                               p_CREATEDBY = sale_.UserID,
                                                               p_FINYEAR = sale_.FinYear,
                                                               p_SALEORDERDETAILS = xml,
                                                               p_ISCANCELLED = sale_.isCancelled,
                                                               p_TERMSID = sale_.strTermsID,
                                                               p_CURRENCYID = sale_.currenceyId,
                                                               p_CURRENCYNAME = sale_.currenceyName,
                                                               p_MRPTAG = sale_.MRPTag,
                                                               p_DELIVERYTERMSID = sale_.deliverytermsId,
                                                               p_DELIVERYTERMSNAME = sale_.deliverytermsName,
                                                               p_ICDS = sale_.icds,
                                                               p_ICDSDATE = sale_.icdsDate,
                                                               P_TOTALCASE = sale_.TotalCase,
                                                               P_TOTALPCS = sale_.TotalPCS,
                                                               P_PAYMENTTERMS = sale_.Paymentterms,
                                                               P_USANCEPERIOD = sale_.Usanceperiod,
                                                               P_DEPOTID = sale_.depotid

                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }

        public List<Load_saleOrder> LoadSale(string fromdate, string todate, string BStype, string _depotid,string finyear)
        {
            List<Load_saleOrder> load_SaleOrders = new List<Load_saleOrder>();
            try
            {
                load_SaleOrders = _db.Query<Load_saleOrder>("usp_load_sale_order_v2", new { p_fromdate = fromdate, p_todate = todate, p_bsid = BStype, p_depotid= _depotid, p_Finyear= finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return load_SaleOrders;
        }

        public List<Proform_status> Getstatus(string saleOrderId,string mode)
        {
            List<Proform_status> statusCheck = new List<Proform_status>();
            try
            {
                statusCheck = _db.Query<Proform_status>("usp_getStatus_saleOrder_v2", new { p_saleorderId = saleOrderId,p_mode=mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return statusCheck;
        }
        public List<DeliveryTerms> BindTerms(string menuId)
        {
            List<DeliveryTerms> deliveryTerms = new List<DeliveryTerms>();
            try
            {
                deliveryTerms = _db.Query<DeliveryTerms>("usp_load_deliveryTerms_v2", new { p_menuId = menuId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return deliveryTerms;
        }

        public List<Proform_status> get_Delete_Status(string saleOrderId)
        {
            List<Proform_status> statusCheck = new List<Proform_status>();
            try
            {
                statusCheck = _db.Query<Proform_status>("usp_get_Delete_Status_saleorder_v2", new { p_saleorderid = saleOrderId }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return statusCheck;
        }
        public List<Proform_status> DeleteSaleOrderHeader(string saleOrderId)
        {
            List<Proform_status> statusCheck = new List<Proform_status>();
            try
            {
                statusCheck = _db.Query<Proform_status>("Usp_T_TotalSALEORDERDELETE_v2", new { p_saleorderid = saleOrderId}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return statusCheck;
        }

        public Customer LoadDepotFromUserForCAcheAdd(string userid)
        {
            Customer user = new Customer();
            try
            {
                user = _db.QueryFirstOrDefault<Customer>("Usp_Bind_Depot_User_Wise_saleorder", new { P_userid = userid }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public List<Customer> LoadDepotFromUser(string userid)
        {
            List<Customer> BRID = new List<Customer>();
            try
            {
                BRID = _db.Query<Customer>("Usp_Bind_Depot_User_Wise_saleorder", new { P_userid = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return BRID;
        }

    }
}
