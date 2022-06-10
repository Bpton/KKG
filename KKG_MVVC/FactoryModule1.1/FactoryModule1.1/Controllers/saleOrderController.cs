using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FactoryModel;
using FactoryDatacontext;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using FactoryModule1._1.Helpers;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;

namespace FactoryModule1._1.Controllers
{
    public class saleOrderController : Controller
    {
        // GET: saleOrder
        public ICacheManager _ICacheManager;
        public saleOrderController()
        {

            _ICacheManager = new CacheManager();


        }
        public ActionResult distributorSaleOrder()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            //var userid = _ICacheManager.Get<object>("IUserID");
            //string id = userid.ToString();
            //Binddepot(id);
            return View();
        }
        public void onepointlogin()
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
            if (userInfo != null)
            {
                HttpContext.Session["UserID"] = userInfo["UserID"].ToString();
                _ICacheManager.Add("UserID", userInfo["UserID"].ToString());
                HttpContext.Session["USERTYPE"] = userInfo["USERTYPE"].ToString();
                HttpContext.Session["UTypeId"] = userInfo["UTypeId"].ToString();
                HttpContext.Session["UTNAME"] = userInfo["UTNAME"].ToString();
                HttpContext.Session["FNAME"] = userInfo["FNAME"].ToString();
                HttpContext.Session["APPLICABLETO"] = userInfo["APPLICABLETO"].ToString();
                HttpContext.Session["TPU"] = userInfo["TPU"].ToString();
                _ICacheManager.Add("TPU", userInfo["TPU"].ToString());
                HttpContext.Session["IUserID"] = userInfo["IUserID"].ToString();
                HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());
              


            }


        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult BindDepotBasedOnUserCacheAdd(string id)
        {
            var userid = _ICacheManager.Get<object>("UserID");
            saleOrderContext saleOrder = new saleOrderContext();
            id = userid.ToString();
            Customer user = saleOrder.LoadDepotFromUserForCAcheAdd(id);
            _ICacheManager.Add("DEPOTID", user.BRID);
            return Json(user);
        }

        public JsonResult BindDepotBasedOnUser(string id)
        {
            BindDepotBasedOnUserCacheAdd(id);
            var userid = _ICacheManager.Get<object>("UserID");
            id = userid.ToString();
            saleOrderContext saleOrder = new saleOrderContext();
            List<Customer> BRID = new List<Customer>();
            BRID = saleOrder.LoadDepotFromUser(id);
            return Json(BRID);
        }
        public JsonResult BindBUSINESSSEGMENT(string bstype)
        {
            var mode = "bstype";
            List<bstype> bstypes = new List<bstype>();
            saleOrderContext saleOrder = new saleOrderContext();
            bstypes = saleOrder.loadBusinesssegment(bstype, mode);
            return Json(bstypes);
        }

        public JsonResult LoadDeleveryTerms(string bstype)
        {
            var mode = "terms";
            saleOrderContext saleOrder = new saleOrderContext();
            List<DeliveryTerms> deliveryTerms =new List<DeliveryTerms>();
            DataTable dtDeliveryTerms = (DataTable)HttpContext.Session["DeliveryTerms"];
            if (dtDeliveryTerms == null)
            {
                deliveryTerms = saleOrder.BindDeliveryTerms(mode);
                //if (dtDeliveryTerms.Rows.Count > 0)
                //{
                //    HttpContext.Session["DeliveryTerms"] = dtDeliveryTerms;
                //}
            }
            return Json(deliveryTerms);
        }

        [HttpPost]
        public DataTable LoadSaleOrder()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("ORDERQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("AMENDMENTQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTPACKINGSIZEID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTPACKINGSIZE", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUIREDDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DISCOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DISCOUNTAMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("REASON", typeof(string)));
            dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
            HttpContext.Session["SALEORDERRECORDS"] = dt;
            return dt;
        }

        public JsonResult LoadPrincipleGroup(string bstype)
        {
            string mode = "";
            List<LoadSaleOrderGroup> bstypes = new List<LoadSaleOrderGroup>();
            saleOrderContext saleOrder = new saleOrderContext();
            bstypes = saleOrder.BindGroup(bstype, HttpContext.Session["IUserID"].ToString(),"F", mode);
            return Json(bstypes);
        }

        [HttpPost]
        public JsonResult getTpuFromSession()
        {
            var tpuid = HttpContext.Session["TPU"];
            string TPUID = tpuid.ToString();
            return Json(TPUID);
        }
        [HttpPost]
        public JsonResult getGroupFromSession()
        {
            var mode = "grop";
            List<LoadSaleOrderGroup> groupid = new List<LoadSaleOrderGroup>();
            saleOrderContext saleOrder = new saleOrderContext();
            groupid = saleOrder.BindGroup("", HttpContext.Session["USERID"].ToString(), "", mode);
            return Json(groupid);
        }
        [HttpPost]
        public JsonResult Getstatus(string saleOrderId)
        {
            var mode = "status";
            List<Proform_status> statusCheck = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            statusCheck = saleOrder.Getstatus(saleOrderId, mode);
            return Json(statusCheck);
        }
        [HttpPost]
        public JsonResult GetProformastatus(string saleOrderId)
        {
            var mode = "proforma";
            List<Proform_status> Proformastatus = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            Proformastatus = saleOrder.Getstatus(saleOrderId, mode);
            return Json(Proformastatus);
        }
        [HttpPost]
        public JsonResult BindProformaInvoice(string saleOrderId)
        {
            var mode = "proformaInv";
            List<Proform_status> ProformaInvoice = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            ProformaInvoice = saleOrder.Getstatus(saleOrderId, mode);
            return Json(ProformaInvoice);
        }

        [HttpPost]
        public JsonResult ProformaAmount(string ProformaID)
        {
            
            List<Proform_status> ProformaAmount = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            ProformaAmount = saleOrder.ProformaAmount(ProformaID);
            return Json(ProformaAmount);
        }

        [HttpPost]
        public JsonResult getEXPORTBSID()
        {
            var mode = "export";
            List<LoadSaleOrderGroup> exportbsid = new List<LoadSaleOrderGroup>();
            saleOrderContext saleOrder = new saleOrderContext();
            exportbsid = saleOrder.BindGroup("", "", "", mode);
            return Json(exportbsid);
        }
        [HttpPost]
        public JsonResult getCustomerFromSession()
        {
            var cusid = _ICacheManager.Get<object>("USERID");
            string CUSTOMERID = cusid.ToString();
            return Json(CUSTOMERID);
        }

        public JsonResult BindCurrency(string groupid)
        {

            List<Currencey> currenceys = new List<Currencey>();
            saleOrderContext saleOrder = new saleOrderContext();
            currenceys = saleOrder.BindCurrency(groupid);
            return Json(currenceys);
        }
        [HttpPost]
        public JsonResult getDepotFromSession()
        {
            var depotid = _ICacheManager.Get<object>("DEPOTID");
            
            //var depotid = "F9D5E0A4-CD51-4CFF-B2C8-99D4A2A13441";
            string DEPOTID = depotid.ToString();
            return Json(DEPOTID);
        }
        public JsonResult BindCustomer(string bstype,string groupid,string sessionDepotid,string saleorderid)
        {

            List<Customer> customers = new List<Customer>();
            saleOrderContext saleOrder = new saleOrderContext();
            customers = saleOrder.BindCustomers(bstype, groupid, sessionDepotid, saleorderid);
            return Json(customers);
        }
        public JsonResult BindProduct(string bstype, string groupid, string saleorderid)
        {

            List<ProductSaleOrder> customers = new List<ProductSaleOrder>();
            saleOrderContext saleOrder = new saleOrderContext();
            customers = saleOrder.BindProduct(bstype, groupid, saleorderid);
            return Json(customers);
        }
        public JsonResult BindPackingSize(string productId)
        {
            var mode = "Pack";
            List<PACKSIZEName> pACKSIZENames = new List<PACKSIZEName>();
            saleOrderContext saleOrder = new saleOrderContext();
            pACKSIZENames = saleOrder.BindPackingSize(productId,mode);
            return Json(pACKSIZENames);
        }
        public JsonResult BindBatchDetails(string depotid, string productId, string packSizeId,string batchNo)
        {

            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext saleOrder = new saleOrderContext();
            saleOrderModels = saleOrder.BindBatchDetails(depotid, productId, packSizeId, batchNo);
            return Json(saleOrderModels);
        }
        public JsonResult BindExportBatchDetails(string depotid, string productId, string packSizeId, string batchNo,string mrp,string storeLocationId)
        {

            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext saleOrder = new saleOrderContext();
            saleOrderModels = saleOrder.BindExportBatchDetails(depotid, productId, packSizeId, batchNo, mrp, storeLocationId);
            return Json(saleOrderModels);
        }
        public JsonResult BindPackingSizeconversionqty(string productId, string Packsize, decimal qty)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext saleOrder = new saleOrderContext();
            saleOrderModels = saleOrder.BindPackingSizeconversionqty(productId, Packsize, qty);
            return Json(saleOrderModels);
        }
        public JsonResult GetBaseCostPrice(string customerId, string productId, string YMD, string mrp, string depotid, string menuID, string BSType, string groupId)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext saleOrder = new saleOrderContext();
            saleOrderModels = saleOrder.GetBaseCostPrice(customerId, productId, YMD, mrp, depotid, menuID, BSType, groupId);
            return Json(saleOrderModels);
        }

        [HttpPost]
        public JsonResult AlreadyDeliveredQty(string productId, string Packsize, string saleOrderid)
        {

            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext Claimscontext = new saleOrderContext();
            saleOrderModels = Claimscontext.AlreadyDeliveredQty(productId, Packsize, saleOrderid);
            return Json(saleOrderModels);
        }
        [HttpPost]
        public JsonResult bindReason(string menuID)
        {

            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext Claimscontext = new saleOrderContext();
            saleOrderModels = Claimscontext.bindReason(menuID);
            return Json(saleOrderModels);
        }

        [HttpPost]
        public JsonResult SaleOrderRecordsCheck(string productId, string scheduleOrderFromDate,string scheduleOrderToDate,string BSType,string ratecheck)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            if (HttpContext.Session["SALEORDERRECORDS"] == null)
            {
                createSaleOrderRecordsDataTable();
            }
            return Json(saleOrderModels);
        }

        public DataTable createSaleOrderRecordsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("ORDERQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMENDMENTQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PRODUCTPACKINGSIZEID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTPACKINGSIZE", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUIREDDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DISCOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DISCOUNTAMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("REASON", typeof(string)));
            dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
            HttpContext.Session["SALEORDERRECORDS"] = dt;
            return dt;
        }

        [HttpPost]
        public JsonResult BindSaleOrderGridRecords(string productId, string productName, string productName1, string PQTY, string Packsize, string packsizeName,
            string requiredDate, string toRequiredDate,decimal rate, decimal dicount, decimal toalammount, decimal QTY, string reson, string resonId, decimal AmendmentQty)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            DataTable dtsaleOrder = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataRow dr = dtsaleOrder.NewRow();
            dr["PRODUCTID"] = productId;
            dr["PRODUCTNAME"] = productName;
            dr["ORDERQTY"] = PQTY;
            dr["AMENDMENTQTY"] = AmendmentQty;
            dr["PRODUCTPACKINGSIZEID"] = Packsize;
            dr["PRODUCTPACKINGSIZE"] = packsizeName;
            dr["REQUIREDDATE"] = requiredDate;
            dr["REQUIREDTODATE"] = toRequiredDate;
            dr["RATE"] = rate;
            dr["DISCOUNT"] = dicount;
            dr["DISCOUNTAMOUNT"] = toalammount;
            dr["QTY"] = QTY;
            dr["REASON"] = reson;
            dr["REASONID"] = resonId;
            dtsaleOrder.Rows.Add(dr);
            dtsaleOrder.AcceptChanges();
            Session["SALEORDERRECORDS"] = dtsaleOrder;


            foreach (DataRow row in dtsaleOrder.Rows)

                saleOrderModels.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            return Json(saleOrderModels);
        }

        public JsonResult CalculateCasePcs(string mode, string Productid, string PacksizeID, decimal Qty)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            saleOrderContext saleOrder = new saleOrderContext();
            saleOrderModels = saleOrder.CalculateCasePcs(mode, Productid, PacksizeID, Qty);
            return Json(saleOrderModels);
        }

        public JsonResult InsertSaleOrderDetails(sale_order sale)
        {
           
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var depotid = _ICacheManager.Get<object>("DEPOTID");
            sale.UserID = userid.ToString();
            sale.FinYear = Finyear.ToString();
            sale.depotid = depotid.ToString();
            if (Session["SALEORDERRECORDS"] == null)
            {
                this.createSaleOrderRecordsDataTable();
            }
            DataTable dtsaleOrder = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            string xml = null;
            xml = ConvertDatatableToXML(dtsaleOrder);
            saleOrderContext saleOrderContext = new saleOrderContext();
            responseMessage = saleOrderContext.InsertSaleOrderDetails(sale, xml).ToList();
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaleOrderRecordsDelete(string PRODUCTID,decimal rate,string PRODUCTNAME)
        {
            List<saleOrderModel> sale_Orders = new List<saleOrderModel>();
            List<messageresponse> responseMessage = new List<messageresponse>();
            DataTable dtrecordsdelete1 = new DataTable();
            dtrecordsdelete1 = (DataTable)Session["SALEORDERRECORDS"];
            int response = 0;
            DataRow[] drr = dtrecordsdelete1.Select("PRODUCTID='" + PRODUCTID + "' AND RATE ='" + rate + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtrecordsdelete1.AcceptChanges();
                response = 1;
            }
            Session["MARGINCLAIMDISPLAY"] = dtrecordsdelete1;
            foreach (DataRow row in dtrecordsdelete1.Rows)

                sale_Orders.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });

            if (response == 1)
            {
                ShowAlertAfterDeleteProduct(PRODUCTNAME);

            }
            else
            {
                ShowAlertErrorDeleteProduct(PRODUCTNAME);
            }
            return Json(sale_Orders);
        }

        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.TableName = "XMLData";
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }


        public EmptyResult RemovesaleOrderSession()
        {

            Session["SALEORDERRECORDS"] = "";
            Session["SALEORDERRECORDS"] = null;


            return new EmptyResult();

        }

        [HttpPost]
        public JsonResult LoadSale(string fromdate,string todate,string BStype,string _depotid)
        {
           
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            List<Load_saleOrder> load_SaleOrders = new List<Load_saleOrder>();
            saleOrderContext saleOrderContext = new saleOrderContext();
            load_SaleOrders = saleOrderContext.LoadSale(fromdate, todate, BStype, _depotid, Finyear);
            return Json(load_SaleOrders, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FetchSaleOrderDetails(string saleorderId)
        {
            List<sale_order> sale_Orders = new List<sale_order>();
            Hashtable hashTable = new Hashtable();
            try
            {
                hashTable.Add("@p_saleOrderId", saleorderId);
                DButility dbcon = new DButility();
                DataSet ds = new DataSet();
                ds = dbcon.SysFetchDataInDataSet("[usp_edit_saleOrder_details]", hashTable);
                DataTable dtSaleorder = new DataTable();

                if (Session["SALEORDERRECORDS"] != null)
                {
                    dtSaleorder = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sale_Orders.Add(new sale_order
                    {
                        saleOrderDate= Convert.ToString(ds.Tables[0].Rows[0]["SALEORDERDATE"]),
                        saleOrderNo = Convert.ToString(ds.Tables[0].Rows[0]["REFERENCESALEORDERNO"]),
                        refSaleOrderDate = Convert.ToString(ds.Tables[0].Rows[0]["REFERENCESALEORDERDATE"]),
                        bsId = Convert.ToString(ds.Tables[0].Rows[0]["BSID"]),
                        groupId = Convert.ToString(ds.Tables[0].Rows[0]["GROUPID"]),
                        customerId = Convert.ToString(ds.Tables[0].Rows[0]["CUSTOMERID"]),
                        customerName = Convert.ToString(ds.Tables[0].Rows[0]["CUSTOMERNAME"]),
                        remarks = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        isCancelled = Convert.ToString(ds.Tables[0].Rows[0]["ISCANCELLED"]),
                        currenceyId = Convert.ToString(ds.Tables[0].Rows[0]["CURRENCYID"]),
                        currenceyName = Convert.ToString(ds.Tables[0].Rows[0]["CURRENCYNAME"]),
                        MRPTag = Convert.ToString(ds.Tables[0].Rows[0]["MRPTAG"]),
                        deliverytermsId = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYTERMSID"]),
                        deliverytermsName = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYTERMSNAME"]),
                        icds = Convert.ToString(ds.Tables[0].Rows[0]["ICDSNO"]),
                        TotalCase = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTALCASE"]),
                        TotalPCS = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTALPCS"]),
                        Paymentterms = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENTTERMS"]),
                        Usanceperiod = Convert.ToString(ds.Tables[0].Rows[0]["USANCEPERIOD"]),
                        icdsDate = Convert.ToString(ds.Tables[0].Rows[0]["ICDSDATE"]),
                       
                    });
                    
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtSaleorder.NewRow();
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                        dr["ORDERQTY"] = Convert.ToDecimal (ds.Tables[1].Rows[i]["ORDERQTY"]);
                        dr["AMENDMENTQTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMENDMENTQTY"]);
                        dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZEID"]).Trim();
                        dr["PRODUCTPACKINGSIZE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZE"]).Trim();
                        dr["RATE"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["RATE"]);
                        dr["DISCOUNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["DISCOUNT"]);
                        dr["DISCOUNTAMOUNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["DISCOUNTAMOUNT"]);
                        dr["REQUIREDDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDDATE"]).Trim(); 
                        dr["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]).Trim();
                        dr["QTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["QTY"]);
                        dr["REASON"] = Convert.ToString(ds.Tables[1].Rows[i]["REASON"]).Trim();
                        dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]).Trim();
                        dtSaleorder.Rows.Add(dr);
                        dtSaleorder.AcceptChanges();
                    }
                    HttpContext.Session["SALEORDERRECORDS"] = dtSaleorder;

                }
            }
            catch (Exception ex)
            {

            }
            return Json(sale_Orders);
        }


        [HttpPost]
        public JsonResult saleOrderDetailsTable()
        {
            List<saleOrderModel> sale_Orders = new List<saleOrderModel>();
            DataTable dtSaleorder = new DataTable();
            dtSaleorder = (DataTable)Session["SALEORDERRECORDS"];
            foreach (DataRow row in dtSaleorder.Rows)

                sale_Orders.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            return Json(sale_Orders);

        }


        public ActionResult BindTerms(string menuId)
        {
            List<DeliveryTerms> deliveryTerms = new List<DeliveryTerms>();
            saleOrderContext saleOrderContext = new saleOrderContext();
            deliveryTerms = saleOrderContext.BindTerms(menuId);
            return Json(deliveryTerms);
        }
        [HttpPost]
        public JsonResult updateScheduleDate(string dtpofromdate,string dtpotodate)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            DataTable dtUpdate = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataTable dtUpdatecopy = (DataTable)HttpContext.Session["SALEORDERRECORDS"];

            foreach (DataRow row in dtUpdate.Rows)
            {
                row["REQUIREDDATE"] = dtpofromdate;
                row["REQUIREDTODATE"] = dtpotodate;
            }

            foreach (DataRow row in dtUpdatecopy.Rows)

                saleOrderModels.Add(new saleOrderModel
                {
                    
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                });

            dtUpdatecopy = dtUpdate.Copy();
            Session["SALEORDERRECORDS"] = dtUpdatecopy;
            return Json(saleOrderModels);
        }

        [HttpPost]
        public JsonResult updateReason(string _PRODUCT, string _REASONID, string _REASON)
        {
            List<saleOrderModel> saleOrderModels = new List<saleOrderModel>();
            DataTable dtUpdate = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataTable dtUpdatecopy = (DataTable)HttpContext.Session["SALEORDERRECORDS"];

            DataRow[] drr = dtUpdate.Select("PRODUCTID='" + _PRODUCT + "'");
            for (int i = 0; i < drr.Length; i++)
            {

            foreach (DataRow row in dtUpdate.Rows)
            {
                row["REASONID"] = _REASONID;
                row["REASON"] = _REASON;
            }

            foreach (DataRow row in dtUpdatecopy.Rows)

                saleOrderModels.Add(new saleOrderModel
                {

                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            }
            dtUpdatecopy = dtUpdate.Copy();
            Session["SALEORDERRECORDS"] = dtUpdatecopy;
            return Json(saleOrderModels);
        }


        [HttpPost]
        public JsonResult updateQty(string _PRODUCT, string rate, string _DESPATCHQTY, string amount)
        {
            List<saleOrderModel> sale_Orders = new List<saleOrderModel>();
            DataTable dtUpdate = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataRow[] drr = dtUpdate.Select("PRODUCTID='" + _PRODUCT + "' AND RATE ='" + rate + "'");

            if (drr.Length > 0)
            {
                foreach (DataRow dr in drr)
                {
                    dr["ORDERQTY"] = _DESPATCHQTY;
                    dr["DISCOUNTAMOUNT"] = amount;
                    dtUpdate.AcceptChanges();
                    dr.SetModified();
                }
                HttpContext.Session["SALEORDERRECORDS"] = dtUpdate;

            }


            foreach (DataRow row in dtUpdate.Rows)

            {
                sale_Orders.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            }

            return Json(sale_Orders);
        }

        [HttpPost]
        public JsonResult updateAmdQty(string _PRODUCT, string rate, string _DESPATCHQTY)
        {
            List<saleOrderModel> sale_Orders = new List<saleOrderModel>();
            DataTable dtUpdate = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataRow[] drr = dtUpdate.Select("PRODUCTID='" + _PRODUCT + "' AND RATE ='" + rate + "'");

            if (drr.Length > 0)
            {
                foreach (DataRow dr in drr)
                {
                    dr["AMENDMENTQTY"] = _DESPATCHQTY;
                    dtUpdate.AcceptChanges();
                    dr.SetModified();
                }   
                HttpContext.Session["SALEORDERRECORDS"] = dtUpdate;
            }
            foreach (DataRow row in dtUpdate.Rows)
            {
                sale_Orders.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            }

            return Json(sale_Orders);
        }

        [HttpPost]
        public JsonResult upadateReason(string _PRODUCT, string _REASONID, string _REASON)
        {
            List<saleOrderModel> sale_Orders = new List<saleOrderModel>();
            DataTable dtUpdate = (DataTable)HttpContext.Session["SALEORDERRECORDS"];
            DataRow[] drr = dtUpdate.Select("PRODUCTID='" + _PRODUCT + "'");

            if (drr.Length > 0)
            {
                foreach (DataRow dr in drr)
                {
                    dr["REASON"] = _REASON;
                    dr["REASONID"] = _REASONID;
                    dtUpdate.AcceptChanges();
                    dr.SetModified();
                }
                HttpContext.Session["SALEORDERRECORDS"] = dtUpdate;

            }


            foreach (DataRow row in dtUpdate.Rows)

            {
                sale_Orders.Add(new saleOrderModel
                {
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    ORDERQTY = Convert.ToDecimal(row["ORDERQTY"]),
                    AMENDMENTQTY = Convert.ToDecimal(row["AMENDMENTQTY"]),
                    PRODUCTPACKINGSIZEID = row["PRODUCTPACKINGSIZEID"].ToString(),
                    PRODUCTPACKINGSIZE = row["PRODUCTPACKINGSIZE"].ToString(),
                    REQUIREDDATE = row["REQUIREDDATE"].ToString(),
                    REQUIREDTODATE = row["REQUIREDTODATE"].ToString(),
                    RATE = Convert.ToDecimal(row["RATE"]),
                    DISCOUNT = Convert.ToDecimal(row["DISCOUNT"]),
                    DISCOUNTAMOUNT = Convert.ToDecimal(row["DISCOUNTAMOUNT"]),
                    QTY = Convert.ToDecimal(row["QTY"]),
                    REASON = row["REASON"].ToString(),
                    REASONID = row["REASONID"].ToString(),
                });
            }

            return Json(sale_Orders);
        }

        [HttpPost]
        public ActionResult ShowAlertAfterDeleteProduct(string productname)
        {
            ViewBag.Message = String.Format("<b><font color='green'><b><font color='red'>" + productname + "</font></b> deleted successfully.</font></b>");
            return View();
        }

        [HttpPost]
        public ActionResult ShowAlertErrorDeleteProduct(string productname)
        {
            ViewBag.Message = String.Format("<b><font color='red'><b><font color='red'>" + productname + "</font></b> deleted unsuccessfully.</font></b>");
            return View();
        }
        
        [HttpPost]
        public JsonResult get_Delete_Status(string saleOrderId)
        {
           
            List<Proform_status> statusCheck = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            statusCheck = saleOrder.get_Delete_Status(saleOrderId);
            return Json(statusCheck);
        }

        [HttpPost]
        public JsonResult DeleteSaleOrderHeader(string saleOrderId)
        {

            List<Proform_status> statusCheck = new List<Proform_status>();
            saleOrderContext saleOrder = new saleOrderContext();
            statusCheck = saleOrder.DeleteSaleOrderHeader(saleOrderId);
            return Json(statusCheck);
        }
    }
}