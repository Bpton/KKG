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

namespace FactoryModule1._1.Controllers
{
    
    public class TranDepotController : Controller
    {
       
     
        // GET: tranfac
        public ICacheManager _ICacheManager;

        public TranDepotController()
        {
           
            _ICacheManager = new CacheManager();
            
            
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

        public ActionResult InvoiceGT()
        {
            //onepointlogin();
            //HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult InvoiceMT()
        {
            return View();
        }

        public ActionResult InvoiceECOMM()
        {
            return View();
        }

        public ActionResult InvoiceINST()
        {
            return View();
        }

        public ActionResult InvoiceCPC()
        {
            return View();
        }

        public ActionResult InvoiceEXPARA()
        {
            return View();
        }

        public ActionResult InvoiceCSD()
        {
            return View();
        }

        public ActionResult InvoiceINCS()
        {
            return View();
        }

        public ActionResult Conversion()
        {
            return View();
        }

        public ActionResult StockAdjustment()
        {
            return View();
        }

        public ActionResult InterStoreLocation()
        {
            return View();
        }

        //public DataTable CreateDataTableTaxComponentGTInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_GT_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentMTInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_MT_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentECOMMInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_ECOMM_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentINSTInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_INST_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentCPCInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_CPC_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentExParaInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_EXPARA_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentCSDInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_CSD_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable CreateDataTableTaxComponentINCSInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        //    dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        //    dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
        //    HttpContext.Session["TAXDETAILS_INCS_INVOICE"] = dt;
        //    return dt;
        //}

        //public DataTable ReturnTaxTableGTInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_GT_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableMTInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_MT_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableECOMMInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_ECOMM_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableINSTInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_INST_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableCPCInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_CPC_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableExParaInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_EXPARA_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableCSDInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_CSD_INVOICE"];
        //    return dtTax;
        //}

        //public DataTable ReturnTaxTableINCSInvoice()
        //{
        //    DataTable dtTax = new DataTable();
        //    dtTax = (DataTable)Session["TAXDETAILS_INCS_INVOICE"];
        //    return dtTax;
        //}

        public string Conver_To_YMD(string dt)
        {

            string strOpenDate = dt;
            string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
            string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
            month = month.Substring(1, month.Length - 1);
            string year = month.Substring(month.IndexOf("/"));
            month = month.Substring(0, month.IndexOf("/"));
            year = year.Substring(1, year.Length - 1);
            dt = year + '-' + month + '-' + day + " 00:00:00.000";
            return dt;

        }

        [HttpPost]
        public JsonResult GetSourceDepot(string UserID)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            List<SourceDepot> sourcedepot = new List<SourceDepot>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            sourcedepot = dispatchContext.GetSourceDepot(UserID.Trim());
            return Json(sourcedepot);
        }

        [HttpPost]
        public JsonResult GetDepot(string UserID)
        {
            List<SourceDepotList> depot = new List<SourceDepotList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            depot = invoiceContext.GetDepot(UserID.Trim());
            return Json(depot);
        }

        [HttpPost]
        public JsonResult GetGTCustomer(string BSID,string GroupID, string DepotID,string InvoiceID)
        {
            List<GTCustomerList> gtCustomer = new List<GTCustomerList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            gtCustomer = invoiceContext.GetGTCustomer(BSID.Trim(), GroupID.Trim(), DepotID.Trim(), InvoiceID.Trim());
            return Json(gtCustomer);
        }

        [HttpPost]
        public JsonResult GetMTCustomer(string BSID, string DepotID, string InvoiceID)
        {
            List<GTCustomerList> mtCustomer = new List<GTCustomerList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            mtCustomer = invoiceContext.GetMTCustomer(BSID.Trim(), DepotID.Trim(), InvoiceID.Trim());
            return Json(mtCustomer);
        }

        [HttpPost]
        public JsonResult GetSaleOrder(string CustomerID)
        {
            List<SaleorderList> saleorder = new List<SaleorderList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            saleorder = invoiceContext.GetSaleOrder(CustomerID.Trim());
            return Json(saleorder);
        }

        [HttpPost]
        public JsonResult GetCPCSaleOrder(string CustomerID)
        {
            List<SaleorderList> saleorder = new List<SaleorderList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            saleorder = invoiceContext.GetCPCSaleOrder(CustomerID.Trim());
            return Json(saleorder);
        }

        [HttpPost]
        public JsonResult GetBackDateChecking(string MenuID)
        {
            List<BackDateChecking> backdate = new List<BackDateChecking>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            backdate = dispatchContext.GetBackDateChecking(MenuID.Trim());
            return Json(backdate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEntryLockChecking(string EntryDate,string Finyear)
        {
            List<EntryLockChecking> lockdate = new List<EntryLockChecking>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            //string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            string FirstFinYear = Finyear.ToString().Substring(0, 4);
            string SecondFinYear = Finyear.ToString().Substring(5, 4);
            string FirstFinYearDt = "01/04/" + FirstFinYear;
            string SecondFinYearDt = "31/03/" + SecondFinYear;
            lockdate = dispatchContext.GetEntryLockChecking(EntryDate.Trim(), Finyear.Trim(), FirstFinYearDt.Trim(), SecondFinYearDt.Trim());
            return Json(lockdate, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        public JsonResult GetTransporter(string SourceDepot, string TxnID)
        {
            List<TransporterList> transporter = new List<TransporterList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            transporter = dispatchContext.GetTransporter(SourceDepot.Trim(), TxnID.Trim());
            return Json(transporter);
        }

        [HttpPost]
        public JsonResult GetShippingAddress(string FromDepot, string ToDepot)
        {
            List<ShippingAddressList> shipping = new List<ShippingAddressList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            string ModuleID = "10";
            shipping = dispatchContext.GetShippingAddress(FromDepot.Trim(), ToDepot.Trim(), Finyear.ToString().Trim(), ModuleID.Trim());
            return Json(shipping, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult fgSaleOrder(string CustomerID,string DepotID,string Type)
        {
            List<FGSaleOrder> saleorder = new List<FGSaleOrder>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            saleorder = dispatchContext.fgSaleOrder(CustomerID.Trim(), DepotID.Trim(), Finyear.ToString().Trim(), Type.Trim());
            return Json(saleorder);
        }

        [HttpPost]
        public JsonResult GetGroup(string BSID,string CheckerFlag,string UserID)
        {
            List<GroupList> grouplist = new List<GroupList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            //var userid = _ICacheManager.Get<object>("UserID");
            //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            grouplist = invoiceContext.GetGroup(BSID, UserID.Trim(), CheckerFlag);
            return Json(grouplist);
        }

        [HttpPost]
        public JsonResult GetCategory(string CustomerID)
        {
            List<GTProductCategory> categorylist = new List<GTProductCategory>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            categorylist = invoiceContext.GetCategory(CustomerID);
            return Json(categorylist);
        }

        [HttpPost]
        public JsonResult GetEcommCategory()
        {
            List<GTProductCategory> categorylist = new List<GTProductCategory>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            categorylist = invoiceContext.GetEcommCategory();
            return Json(categorylist);
        }

        [HttpPost]
        public JsonResult GetGTProduct(string CustomerID, string DepotID, string BSID, string CategoryID, string InvoiceID)
        {
            List<GTProductList> productlist = new List<GTProductList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            productlist = invoiceContext.GetGTProduct(CustomerID.Trim(), DepotID.Trim(), BSID.Trim(), CategoryID.Trim(), InvoiceID.Trim());
            return Json(productlist);
        }

        [HttpPost]
        public JsonResult GetECommProduct(string BSID, string CategoryID, string InvoiceID)
        {
            List<GTProductList> productlist = new List<GTProductList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            productlist = invoiceContext.GetECommProduct(BSID.Trim(), CategoryID.Trim(), InvoiceID.Trim());
            return Json(productlist);
        }

        [HttpPost]
        public JsonResult GetMTProduct(string SaleorderID)
        {
            List<MTProductList> mtproductlist = new List<MTProductList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            mtproductlist = invoiceContext.GetMTProduct(SaleorderID.Trim());
            return Json(mtproductlist);
        }

        [HttpPost]
        public JsonResult GetPacksize()
        {
            List<GTPacksizeList> packlist = new List<GTPacksizeList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            packlist = invoiceContext.GetPacksize();
            return Json(packlist);
        }

        [HttpPost]
        public JsonResult GetInvoiceTaxcount(string MenuID, string Flag, string DepotID, string ProductID, string CustomerID, string Date)
        {
            List<InvoiceTaxcountList> invoicetaxcount = new List<InvoiceTaxcountList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            invoicetaxcount = invoiceContext.GetTaxcount(MenuID.Trim(), Flag.Trim(), DepotID.Trim().Trim(), ProductID.Trim(), CustomerID.Trim(), Date.Trim());
            return Json(invoicetaxcount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGTClosingStatus(string InvoiceDate, string DepotID,string Finyear)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            //string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            try
            {
                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GetGTClosingStatus(InvoiceDate.Trim(), DepotID.Trim(), Finyear.Trim());
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAccPostingStatus(string DepotID, string BSID, string InvoiceDate,string UserID)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            //var userid = _ICacheManager.Get<object>("UserID");
            //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            try
            {
                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GetAccPostingStatus(DepotID.Trim(), BSID.Trim(), UserID.Trim(), InvoiceDate.Trim());
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDayEndStatus(string DepotID, string BSID, string InvoiceDate,string UserID)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            //var userid = _ICacheManager.Get<object>("UserID");
            //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            try
            {
                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GetDayEndStatus(DepotID.Trim(), BSID.Trim(), UserID.Trim(), InvoiceDate.Trim());
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetGTInvoiceTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            List<InvoiceTaxOnEdit> invoicetaxonedit = new List<InvoiceTaxOnEdit>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            invoicetaxonedit = invoiceContext.GetGTInvoiceTaxOnEdit(InvoiceID.Trim(), TaxID.Trim(), ProductID.Trim(), BatchNo.Trim());
            return Json(invoicetaxonedit, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public JsonResult GetBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            List<DepotBatchInfoList> batchlist = new List<DepotBatchInfoList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string Storelocation = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";/*Saleable*/
            string MRP = "0";
            batchlist = invoiceContext.GetBatchDetails(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(), MRP.Trim(), Storelocation.Trim());
            return Json(batchlist);
        }

        [HttpPost]
        public JsonResult GetTransferRate(string productID, decimal mrp, string transferDate)
        {
            List<StockTransferRateList> rate = new List<StockTransferRateList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            rate = dispatchContext.GetTransferRate(productID.Trim(), mrp, transferDate.Trim());
            return Json(rate, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTaxDetails(string Productid, string FromPacksizeID, string ToPacksizeID, string QtyPcs, string CGST, string SGST, string IGST, string Date)
        {
            TaxDetailsDepot taxDetails = new TaxDetailsDepot();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            taxDetails = invoiceContext.GetTaxDetails(Productid, FromPacksizeID, ToPacksizeID, QtyPcs, CGST, SGST, IGST, Date);
            var taxDetailsDataset = new
            {
                varCaseQty = taxDetails.casequantity,
                varNetwght = taxDetails.netweight,
                varCgstTax = taxDetails.cgstpercentage,
                varCgstID = taxDetails.cgst,
                varSgstTax = taxDetails.sgstpercentage,
                varSgstID = taxDetails.sgst,
                varIgstTax = taxDetails.igstpercentage,
                varIgstID = taxDetails.igst,
            };
            return Json(new
            {
                taxDetailsDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        

        //[HttpPost]
        //public EmptyResult FillGTInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_GT_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentGTInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_GT_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_GT_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillMTInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_MT_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentMTInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_MT_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_MT_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillINSTInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_INST_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentINSTInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INST_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_INST_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillECOMMInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_ECOMM_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentECOMMInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_ECOMM_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_ECOMM_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillCPCInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_CPC_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentCPCInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_CPC_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_CPC_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillExParaInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_EXPARA_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentExParaInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_EXPARA_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_EXPARA_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillCSDInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_CSD_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentCSDInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_CSD_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_CSD_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult FillINCSInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        //{
        //    if (Session["TAXDETAILS_INCS_INVOICE"] == null)
        //    {
        //        CreateDataTableTaxComponentINCSInvoice();
        //    }

        //    DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INCS_INVOICE"];
        //    DataRow dr = dt.NewRow();
        //    if (Flag == "1")
        //    {
        //        dr["PRIMARYPRODUCTID"] = Productid;
        //        dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
        //        dr["PRODUCTID"] = Productid;
        //        dr["BATCHNO"] = BatchNo;
        //        dr["TAXID"] = TaxID;
        //        dr["TAXPERCENTAGE"] = Percentage;
        //        dr["TAXVALUE"] = TaxValue;
        //        dr["TAG"] = "P";
        //        dr["MRP"] = MRP;
        //    }
        //    else
        //    {
        //        dr["PRIMARYPRODUCTID"] = "NA";
        //        dr["PRIMARYPRODUCTBATCHNO"] = "NA";
        //        dr["PRODUCTID"] = "NA";
        //        dr["BATCHNO"] = "NA";
        //        dr["TAXID"] = "NA";
        //        dr["TAXPERCENTAGE"] = 0;
        //        dr["TAXVALUE"] = 0;
        //        dr["TAG"] = "NA";
        //        dr["MRP"] = 0;
        //    }
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    Session["TAXDETAILS_INCS_INVOICE"] = dt;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableGTInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_GT_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_GT_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableMTInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_MT_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_MT_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableINSTInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INST_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_INST_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableECOMMInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_ECOMM_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_ECOMM_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableCPCInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_CPC_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_CPC_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableExParaInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_EXPARA_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_EXPARA_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableCSDInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_CSD_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_CSD_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        //[HttpPost]
        //public EmptyResult DeleteTaxDatatableINCSInvoice(string Productid, string BatchNo)
        //{
        //    DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INCS_INVOICE"];
        //    DataRow[] drrTax = DtTax.Select("PRIMARYPRODUCTID = '" + Productid + "' AND PRIMARYPRODUCTBATCHNO = '" + BatchNo + "'");
        //    for (int t = 0; t < drrTax.Length; t++)
        //    {
        //        drrTax[t].Delete();
        //        DtTax.AcceptChanges();
        //    }
        //    HttpContext.Session["TAXDETAILS_INCS_INVOICE"] = DtTax;
        //    return new EmptyResult();
        //}

        public ActionResult GetTotalQuantity(string Productid, string FromPackSizeID, string CasePacksizeID, string PCSPacksizeID, decimal Qty)
        {
            QuantityDetails qtydetails = new QuantityDetails();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            qtydetails = dispatchContext.CalculateQuantity(Productid, FromPackSizeID, CasePacksizeID, PCSPacksizeID, Qty);
            var allquantityDataset = new
            {
                varCase = qtydetails.casequantity,
                varPcs = qtydetails.pcsquantity,
            };
            return Json(new
            {
                allquantityDataset,
                JsonRequestBehavior.AllowGet
            });
        }


        [HttpPost]
        public JsonResult gtInvoicesavedata(DepotFGInvoiceModel gtinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                //string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string ModuleID = "90";
                string BSID = "7F62F951-9D1F-4B8D-803B-74EEBA468CEE";/*GT*/
               
                //DataTable dtTax = ReturnTaxTableGTInvoice();

                if (string.IsNullOrEmpty(gtinvoicesave.Remarks))
                {
                    gtinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(gtinvoicesave.LrGrNo))
                {
                    gtinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(gtinvoicesave.VehichleNo))
                {
                    gtinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(gtinvoicesave.GatepassNo))
                {
                    gtinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(gtinvoicesave.ShippingAddress))
                {
                    gtinvoicesave.ShippingAddress = "";
                }
                if (gtinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(gtinvoicesave.Remarks).Contains("'"))
                    {
                        gtinvoicesave.Remarks = Convert.ToString(gtinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GtInvoiceInsertUpdate( gtinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult mtInvoicesavedata(DepotFGInvoiceModel mtinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                string ModuleID = "1123";
                string BSID = "0AA9353F-D350-4380-BC84-6ED5D0031E24";/*MT*/

                //DataTable dtTax = ReturnTaxTableMTInvoice();

                if (string.IsNullOrEmpty(mtinvoicesave.Remarks))
                {
                    mtinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(mtinvoicesave.LrGrNo))
                {
                    mtinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(mtinvoicesave.VehichleNo))
                {
                    mtinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(mtinvoicesave.GatepassNo))
                {
                    mtinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(mtinvoicesave.ShippingAddress))
                {
                    mtinvoicesave.ShippingAddress = "";
                }
                if (mtinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(mtinvoicesave.Remarks).Contains("'"))
                    {
                        mtinvoicesave.Remarks = Convert.ToString(mtinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.MtInvoiceInsertUpdate(mtinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult instInvoicesavedata(DepotFGInvoiceModel instinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                string ModuleID = "1461";
                string BSID = "91B9CC35-AF4F-4732-92B1-433C68104DE6";/*INST*/

                //DataTable dtTax = ReturnTaxTableINSTInvoice();

                if (string.IsNullOrEmpty(instinvoicesave.Remarks))
                {
                    instinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(instinvoicesave.LrGrNo))
                {
                    instinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(instinvoicesave.VehichleNo))
                {
                    instinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(instinvoicesave.GatepassNo))
                {
                    instinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(instinvoicesave.ShippingAddress))
                {
                    instinvoicesave.ShippingAddress = "";
                }
                if (instinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(instinvoicesave.Remarks).Contains("'"))
                    {
                        instinvoicesave.Remarks = Convert.ToString(instinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.MtInvoiceInsertUpdate(instinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ecommInvoicesavedata(DepotFGInvoiceModel ecomminvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                
                string ModuleID = "2722";
                string BSID = "A18FE04D-057C-4A88-9204-5FE1613626C9";/*E-Commerce*/

                //DataTable dtTax = ReturnTaxTableECOMMInvoice();

                if (string.IsNullOrEmpty(ecomminvoicesave.Remarks))
                {
                    ecomminvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(ecomminvoicesave.LrGrNo))
                {
                    ecomminvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(ecomminvoicesave.VehichleNo))
                {
                    ecomminvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(ecomminvoicesave.GatepassNo))
                {
                    ecomminvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(ecomminvoicesave.ShippingAddress))
                {
                    ecomminvoicesave.ShippingAddress = "";
                }
                if (ecomminvoicesave.Remarks != null)
                {
                    if (Convert.ToString(ecomminvoicesave.Remarks).Contains("'"))
                    {
                        ecomminvoicesave.Remarks = Convert.ToString(ecomminvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.EcommInvoiceInsertUpdate(ecomminvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult cpcInvoicesavedata(DepotFGInvoiceModel cpcinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                string ModuleID = "1125";
                string BSID = "A0A1E83E-1993-4FB9-AF53-9DD595D09596";/*CPC*/

                //DataTable dtTax = ReturnTaxTableCPCInvoice();

                if (string.IsNullOrEmpty(cpcinvoicesave.Remarks))
                {
                    cpcinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(cpcinvoicesave.LrGrNo))
                {
                    cpcinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(cpcinvoicesave.VehichleNo))
                {
                    cpcinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(cpcinvoicesave.GatepassNo))
                {
                    cpcinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(cpcinvoicesave.ShippingAddress))
                {
                    cpcinvoicesave.ShippingAddress = "";
                }
                if (cpcinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(cpcinvoicesave.Remarks).Contains("'"))
                    {
                        cpcinvoicesave.Remarks = Convert.ToString(cpcinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.CpcInvoiceInsertUpdate(cpcinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult exparaInvoicesavedata(DepotFGInvoiceModel exparainvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                string ModuleID = "2863";
                string BSID = "C8E2844A-B43F-4028-856B-1D8827CB9B8B";/*Ex-Paramilitary*/

                //DataTable dtTax = ReturnTaxTableExParaInvoice();
                if (string.IsNullOrEmpty(exparainvoicesave.Remarks))
                {
                    exparainvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(exparainvoicesave.LrGrNo))
                {
                    exparainvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(exparainvoicesave.VehichleNo))
                {
                    exparainvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(exparainvoicesave.GatepassNo))
                {
                    exparainvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(exparainvoicesave.ShippingAddress))
                {
                    exparainvoicesave.ShippingAddress = "";
                }
                if (exparainvoicesave.Remarks != null)
                {
                    if (Convert.ToString(exparainvoicesave.Remarks).Contains("'"))
                    {
                        exparainvoicesave.Remarks = Convert.ToString(exparainvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.CpcInvoiceInsertUpdate(exparainvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult csdInvoicesavedata(DepotFGInvoiceModel csdinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                string ModuleID = "1124";
                string BSID = "C5038911-9331-40CF-B7F9-583D50583592";/*CSD*/

                //DataTable dtTax = ReturnTaxTableCSDInvoice();
                if (string.IsNullOrEmpty(csdinvoicesave.Remarks))
                {
                    csdinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(csdinvoicesave.LrGrNo))
                {
                    csdinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(csdinvoicesave.VehichleNo))
                {
                    csdinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(csdinvoicesave.GatepassNo))
                {
                    csdinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(csdinvoicesave.ShippingAddress))
                {
                    csdinvoicesave.ShippingAddress = "";
                }
                if (csdinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(csdinvoicesave.Remarks).Contains("'"))
                    {
                        csdinvoicesave.Remarks = Convert.ToString(csdinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.CsdInvoiceInsertUpdate(csdinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult incsInvoicesavedata(DepotFGInvoiceModel incsinvoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                string ModuleID = "1498";
                string BSID = "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996";/*INCS*/

                //DataTable dtTax = ReturnTaxTableINCSInvoice();

                if (string.IsNullOrEmpty(incsinvoicesave.Remarks))
                {
                    incsinvoicesave.Remarks = "";
                }
                if (string.IsNullOrEmpty(incsinvoicesave.LrGrNo))
                {
                    incsinvoicesave.LrGrNo = "";
                }
                if (string.IsNullOrEmpty(incsinvoicesave.VehichleNo))
                {
                    incsinvoicesave.VehichleNo = "";
                }
                if (string.IsNullOrEmpty(incsinvoicesave.GatepassNo))
                {
                    incsinvoicesave.GatepassNo = "";
                }
                if (string.IsNullOrEmpty(incsinvoicesave.ShippingAddress))
                {
                    incsinvoicesave.ShippingAddress = "";
                }
                if (incsinvoicesave.Remarks != null)
                {
                    if (Convert.ToString(incsinvoicesave.Remarks).Contains("'"))
                    {
                        incsinvoicesave.Remarks = Convert.ToString(incsinvoicesave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.CsdInvoiceInsertUpdate(incsinvoicesave, BSID,
                                                                        ModuleID).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult conversionsavedata(DepotFGInvoiceModel conversionsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                

               
                if (conversionsave.Remarks != null)
                {
                    if (Convert.ToString(conversionsave.Remarks).Contains("'"))
                    {
                        conversionsave.Remarks = Convert.ToString(conversionsave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.StockAdjustmentInsertUpdate(conversionsave).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult stockjournalsavedata(DepotFGInvoiceModel stockjournalsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                if (String.IsNullOrEmpty(stockjournalsave.Remarks))
                {
                    stockjournalsave.Remarks = "";
                }

                if (stockjournalsave.Remarks != null)
                {
                    if (Convert.ToString(stockjournalsave.Remarks).Contains("'"))
                    {
                        stockjournalsave.Remarks = Convert.ToString(stockjournalsave.Remarks).Replace("'", "''");
                    }
                }

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.StockAdjustmentInsertUpdate(stockjournalsave).ToList();



                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GTdeleteInvoice(string InvoiceID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GtInvoiceDelete(InvoiceID).ToList();
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        public EmptyResult RemoveSessionGTInvoice()
        {
            Session.Remove("TAXDETAILS_GT_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionMTInvoice()
        {
            Session.Remove("TAXDETAILS_MT_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionINSTInvoice()
        {
            Session.Remove("TAXDETAILS_INST_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionECOMMInvoice()
        {
            Session.Remove("TAXDETAILS_ECOMM_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionCPCInvoice()
        {
            Session.Remove("TAXDETAILS_CPC_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionExParaInvoice()
        {
            Session.Remove("TAXDETAILS_EXPARA_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionCSDInvoice()
        {
            Session.Remove("TAXDETAILS_CSD_INVOICE");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionINCSInvoice()
        {
            Session.Remove("TAXDETAILS_INCS_INVOICE");
            return new EmptyResult();
        }

        [HttpPost]
        public JsonResult BindGtInvoiceGrid(string FromDate, string ToDate, string BSID,string CheckerFlag, string depotID, string Challan,string UserID,string FinYear)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            //string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            //string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            List<GTInvoiceList> gtinvoiceGrid = new List<GTInvoiceList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            gtinvoiceGrid = invoiceContext.BindGtInvoiceGrid(FromDate.Trim(), ToDate.Trim(), FinYear.Trim(), BSID.Trim(), depotID.Trim(), CheckerFlag.Trim(), UserID.Trim(), Challan.Trim());
            return new JsonResult() { Data = gtinvoiceGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult BindMtInvoiceGrid(string FromDate, string ToDate, string BSID, string CheckerFlag, string depotID, string Challan, string UserID, string FinYear)
        {
            List<GTInvoiceList> mtinvoiceGrid = new List<GTInvoiceList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            mtinvoiceGrid = invoiceContext.BindMtInvoiceGrid(FromDate.Trim(), ToDate.Trim(), FinYear.Trim(), BSID.Trim(), depotID.Trim(), CheckerFlag.Trim(), UserID.Trim(), Challan.Trim());
            return new JsonResult() { Data = mtinvoiceGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult GetInvoiceStatus(string InvoiceID, string ModuleID, string Type)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            try
            {
                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.GetInvoiceStatus(InvoiceID.Trim(), ModuleID.Trim(), Type.Trim());
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetFGOrderQtyDetails(string InvoiceID,string CustomerID, string SaleOrderID, string ProductID, string DepotID,string PacksizeID)
        {
            List<OrderDetailsList> orderlist = new List<OrderDetailsList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Storelocation = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";/*Saleable*/
            orderlist = dispatchContext.GetOrderQtyDetails(InvoiceID.Trim(), CustomerID.Trim(), SaleOrderID.Trim(), ProductID.Trim(), DepotID.Trim(), PacksizeID.Trim(), Storelocation.Trim());
            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceConvertedQty(string ProductID, string PacksizeFrom, string PacksizeTo, string Qty)
        {
            List<InvoiceConvertedQty> convertedqtylist = new List<InvoiceConvertedQty>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            convertedqtylist = dispatchContext.GetConvertedQty(ProductID.Trim(), PacksizeFrom.Trim(), PacksizeTo.Trim(), Convert.ToDecimal(Qty.Trim()));
            return Json(convertedqtylist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GTInvoiceEdit(string InvoiceID)
        {
            InvoiceEditGT editGTDetails = new InvoiceEditGT();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            editGTDetails = invoiceContext.GTInvoiceEdit(InvoiceID);
            var GTeditDataset = new
            {
                varHeader = editGTDetails.InvoiceHeaderEditGT,
                varDetails = editGTDetails.InvoiceDetailsEditGT,
                varTaxCount = editGTDetails.InvoiceTaxcountEditGT,
                varFooter = editGTDetails.InvoiceFooterEditGT,
                varTaxDetails = editGTDetails.InvoiceTaxEditGT,
                varPricescheme = editGTDetails.InvoicePriceSchemeEditGT,
                varQtyscheme = editGTDetails.InvoiceQuantitySchemeEditGT,
                varProductDetails = editGTDetails.InvoiceProductDetailsEditGT,
                varOrderDetails = editGTDetails.InvoiceOrderDetailsEditGT,
                varOrderHeader = editGTDetails.InvoiceOrderHeaderEditGT,

            };
            return Json(new
            {
                GTeditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult GTCustomerDetails(string CustomerID, string DepotID,string MonthID, string InvoiceID,string FinYear)
        {
            GTCustomerDetails customerdetails = new GTCustomerDetails();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            //var FinYear = _ICacheManager.Get<object>("FINYEAR");
            //string FinYear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            customerdetails = invoiceContext.GTCustomerDetails(CustomerID, DepotID, FinYear.Trim(), MonthID, InvoiceID);
            var alldetailsDataset = new
            {
                varInvoiceLimit = customerdetails.InvoiceLimit,
                varSSMargin = customerdetails.SSMargin,
                varGSTStatus = customerdetails.GstNoStatus,
            };
            return Json(new
            {
                alldetailsDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult GTCustomerOutstanding(string CustomerID, string DepotID, string InvoiceID,string FinYear)
        {
            GTCustomerOutstanding customeroutstanding = new GTCustomerOutstanding();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            //var FinYear = _ICacheManager.Get<object>("FINYEAR");
            //string FinYear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            customeroutstanding = invoiceContext.GTCustomerOutstanding(CustomerID, DepotID, FinYear.Trim(), InvoiceID);
            var allOutStandingDataset = new
            {
                varOutstanding = customeroutstanding.OutStanding,
                varCreditLimit = customeroutstanding.CreditLimit,
            };
            return Json(new
            {
                allOutStandingDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult GetCustomerTarget(string CustomerID, string DepotID, string BSID, string MonthID, string InvoiceID,string FinYear)
        {
            List<GTCustomerTarget> target = new List<GTCustomerTarget>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            //var FinYear = _ICacheManager.Get<object>("FINYEAR");
            //string FinYear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            target = invoiceContext.GetCustomerTarget(CustomerID.Trim(), DepotID.Trim(), BSID.Trim(),FinYear.Trim(),MonthID.Trim(),InvoiceID.Trim());
            return Json(target, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBCP(string Customerid, string Productid, string Invoicedate, string Mrp, string Depotid, string Menuid, string BSid, string Groupid, string Tag)
        {
            List<DepotInvoiceRate> bcp = new List<DepotInvoiceRate>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string YMD = Conver_To_YMD(Invoicedate.Trim());
            bcp = invoiceContext.GetBCP(Customerid.Trim(), Productid.Trim(), YMD.Trim(), Mrp.Trim(), Depotid.Trim(), Menuid.Trim(), BSid.Trim(), Groupid.Trim(), Tag.Trim());
            return Json(bcp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QtyInPcsGT(string Productid, string PacksizefromID, string Deliveredqty, string Stockqty, string SaleorderID, string InvoiceID)
        {
            List<QtyInPcsGT> qtypcs = new List<QtyInPcsGT>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            qtypcs = invoiceContext.QtyInPcsGT(Productid.Trim(), PacksizefromID.Trim(), Deliveredqty.Trim(), Stockqty.Trim(), SaleorderID.Trim(), InvoiceID.Trim());
            return Json(qtypcs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductType(string Productid)
        {
            List<ProductType> productType = new List<ProductType>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            productType = invoiceContext.ProductType(Productid.Trim());
            return Json(productType, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GroupStatus(string CustomerID,string BSID)
        {
            List<GroupStatus> groupstatus = new List<GroupStatus>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            groupstatus = invoiceContext.GroupStatus(CustomerID.Trim(), BSID.Trim());
            return Json(groupstatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OfflineStatus(string DepotID)
        {
            List<OfflineStatus> offlinestatus = new List<OfflineStatus>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            offlinestatus = invoiceContext.OfflineStatus(DepotID.Trim());
            return Json(offlinestatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OfflineTag()
        {
            List<OfflineTag> offlinetag = new List<OfflineTag>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            offlinetag = invoiceContext.OfflineTag();
            return Json(offlinetag, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Ratepercase(string BSID,string GroupID,string ProductID)
        {
            List<RatePerCase> ratecase = new List<RatePerCase>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            ratecase = invoiceContext.Ratepercase(BSID.Trim(), GroupID.Trim(), ProductID.Trim());
            return Json(ratecase, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FullCasePack(string ProductID, string Qty)
        {
            List<Packsize> packsize = new List<Packsize>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            packsize = invoiceContext.Packsize(ProductID.Trim(), Qty.Trim());
            return Json(packsize, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostingStatus(string InvoiceID)
        {
            List<PostingStatus> postingstatus = new List<PostingStatus>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            postingstatus = invoiceContext.Postingstatus(InvoiceID.Trim());
            return Json(postingstatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CodeStatus(string CustomerID)
        {
            List<CodeStatus> codestatus = new List<CodeStatus>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            codestatus = invoiceContext.Codestatus(CustomerID.Trim());
            return Json(codestatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPriceSchemeDiscount(string Productid, string Qty, string Packsize, string CustomerID, string SaleorderID,
                                            string Date, string BSID, string GroupID, string DepotID, string MRP
                                        )
        {
            List<PriceSchemeDiscount> priceschemediscount = new List<PriceSchemeDiscount>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string YMD = Conver_To_YMD(Date.Trim());
            priceschemediscount = invoiceContext.GetPriceSchemeDiscount(Productid.Trim(), Qty.Trim(), Packsize.Trim(), CustomerID.Trim(), SaleorderID.Trim(), YMD.Trim(), BSID.Trim(), GroupID.Trim(), DepotID.Trim(), MRP.Trim());
            return Json(priceschemediscount, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetHSNCode(string Productid)
        {
            List<HSNCode> hsncode = new List<HSNCode>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            hsncode = invoiceContext.GetHSNCode(Productid.Trim());
            return Json(hsncode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableStock(string DepotID, string Productid, string Batch, string MRP, string MfgDate, string ExpDate, string StorelocationID)
        {
            List<AvailableStock> stock = new List<AvailableStock>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            stock = invoiceContext.GetAvailableStock(DepotID.Trim(), Productid.Trim(), Batch.Trim(), MRP.Trim(), MfgDate.Trim(), ExpDate.Trim(), StorelocationID.Trim());
            return Json(stock, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetQuantityScheme(string Date, string StateID, string DistrictID, string ProductID, string GroupID, string BSID,
                                                        string Qty, string PacksizeID, string ZoneID, string TeritoryID, string CustomerID,
                                                        string DepotID, string ModuleID, string BatchNo, string MRP, string Flag, string IsDayend)
        {
            List<QuantityScheme> qtyscheme = new List<QuantityScheme>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string YMD = Conver_To_YMD(Date.Trim());
            qtyscheme = invoiceContext.GetQuantityScheme(YMD, StateID, DistrictID, ProductID, GroupID, BSID, Qty,
                                                            PacksizeID, ZoneID, TeritoryID, CustomerID, DepotID,
                                                            ModuleID, BatchNo, MRP, Flag, IsDayend);
            return Json(qtyscheme);
        }

        [HttpPost]
        public JsonResult GetFreeProductBatchDetails(   string DepotID, string ProductID, string PacksizeID, string BatchNo,
                                                        string CustomerID, string Date, string ModuleID, string BSID, string GroupID,
                                                        string Mrp, string StorelocationID)
        {
            List<FreeProductBatchInfoList> freebatchdetails = new List<FreeProductBatchInfoList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string YMD = Conver_To_YMD(Date.Trim());
            freebatchdetails = invoiceContext.GetFreeProductBatchDetails(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(),
                                                            CustomerID.Trim(), YMD, ModuleID.Trim(), BSID.Trim(), GroupID.Trim(),
                                                            Mrp.Trim(), StorelocationID.Trim());
            return Json(freebatchdetails);
        }

        [HttpPost]
        public JsonResult ConvertedQuantity(string Productid, string FromPacksize, string ToPacksize, string CaseQty, string PcsQty)
        {
            List<QuantityConversion> qtyconversion = new List<QuantityConversion>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            qtyconversion = invoiceContext.ConvertedQuantity(Productid, FromPacksize, ToPacksize, CaseQty, PcsQty);
            return Json(qtyconversion);
        }

        [HttpPost]
        public JsonResult CaseToPCSConversion(string Productid, string FromPacksize, string ToPacksize, string CaseQty, string PcsQty)
        {
            List<CaseToPCSConversion> casetopcs = new List<CaseToPCSConversion>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            casetopcs = invoiceContext.CaseToPCSConversion(Productid, FromPacksize, ToPacksize, CaseQty, PcsQty);
            return Json(casetopcs);
        }

        [HttpPost]
        public JsonResult GetExportProduct(string BSID, string SaleinvoiceID)
        {
            List<ExportProductList> exportproductlist = new List<ExportProductList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportproductlist = dispatchContext.GetExportProduct(BSID.Trim(), SaleinvoiceID.Trim());
            return Json(exportproductlist);
        }

        //fin yr check
        [HttpPost]
        public JsonResult finyrchk(string FinYear)
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            //var finyr = _ICacheManager.Get<object>("FINYEAR");
            //string finyr = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            string finyr = FinYear.Trim();
            string _finyr = finyr.ToString();
            string startyear = _finyr.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = _finyr.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime datevalidation = DateTime.Today.Date;
            if (datevalidation >= new DateTime(startyear1, 04, 01) && datevalidation <= new DateTime(endyear1, 03, 31))
            {
                currentdt = datevalidation.ToString("yyyy/MM/dd");
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
            }
            else
            {
                currentdt = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");

            }
            Finyearrange.Add(new Finyearrange
            {
                currentdt = currentdt,
                frmdate = frmdate,
                todate = todate,



            });

            return Json(Finyearrange);
        }

        [HttpPost]
        public JsonResult GetOrdervsDispatch(string OrderID, string ProductID, string PacksizeID, string InvoiceID)
        {
            List<OrdervsDispatchList> ordervsdispatch = new List<OrdervsDispatchList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            ordervsdispatch = invoiceContext.GetOrdervsDispatch(OrderID.Trim(), ProductID.Trim(), PacksizeID.Trim(), InvoiceID.Trim());
            return Json(ordervsdispatch, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetQuantityInPcs(string ProductID, string PacksizefromID, string DeliveredQty, string StockQty, string OrderID, string InvoiceID)
        {
            List<QuantityInPcs> qtyinpcs = new List<QuantityInPcs>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            qtyinpcs = invoiceContext.GetQuantityInPcs(ProductID.Trim(), PacksizefromID.Trim(), DeliveredQty.Trim(), StockQty.Trim(), OrderID.Trim(), InvoiceID.Trim());
            return Json(qtyinpcs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCSDComboProduct(string BSID)
        {
            List<CsdComboProduct> comboproductlist = new List<CsdComboProduct>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            comboproductlist = invoiceContext.BindCsdComboGrid(BSID.Trim());
            return Json(comboproductlist);
        }

        [HttpPost]
        public JsonResult GetICDSDetails(string OrderID)
        {
            List<ICDSDetails> icds = new List<ICDSDetails>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            icds = invoiceContext.BindICDSdetails(OrderID.Trim());
            return Json(icds, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetConversionComboProduct()
        {
            List<ConversionComboProduct> conversioncomboproductlist = new List<ConversionComboProduct>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            conversioncomboproductlist = invoiceContext.BindConversionComboGrid();
            return Json(conversioncomboproductlist);
        }

        [HttpPost]
        public JsonResult GetCSDComboProductDetails(string ComboProductID)
        {
            List<ConversionComboProductDetails> comboproductdetailslist = new List<ConversionComboProductDetails>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            comboproductdetailslist = invoiceContext.BindConversionComboProductDetails(ComboProductID.Trim());
            return Json(comboproductdetailslist);
        }

        [HttpPost]
        public JsonResult ComboMrp(string ProductID)
        {
            List<ComboMRP> commbomrp = new List<ComboMRP>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            commbomrp = invoiceContext.ComboMrp(ProductID.Trim());
            return Json(commbomrp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BatchExists(string ProductID, string BatchNo, string MRP)
        {
            List<BatchExistsFlag> batchexists = new List<BatchExistsFlag>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            batchexists = invoiceContext.BatchExists(ProductID.Trim(), BatchNo.Trim(),MRP.Trim());
            return Json(batchexists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BatchCountExists(string ProductID, string BatchNo, string MRP)
        {
            List<CountExists> batchcountexists = new List<CountExists>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            batchcountexists = invoiceContext.CountExists(ProductID.Trim(), BatchNo.Trim(), MRP.Trim());
            return Json(batchcountexists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveBatchMaster(DepotFGInvoiceModel batchsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {

                DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
                responseMessage = invoiceContext.BatchMasterInsert(batchsave).ToList();

                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    TempData["messageid"] = msg.MessageID;
                    TempData["messagetext"] = msg.MessageText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult BindConversionGrid(string FromDate, string ToDate, string DepotID, string FinYear)
        {
            List<ConversionList> conversionGrid = new List<ConversionList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            conversionGrid = invoiceContext.BindConversionGrid(FromDate.Trim(), ToDate.Trim(), DepotID.Trim(), FinYear.Trim());
            return new JsonResult() { Data = conversionGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult BindStockJournalGrid(string FromDate, string ToDate, string DepotID, string FinYear)
        {
            List<StockJournalList> stockjournalGrid = new List<StockJournalList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            stockjournalGrid = invoiceContext.BindStockJournalGrid(FromDate.Trim(), ToDate.Trim(), DepotID.Trim(), FinYear.Trim());
            return new JsonResult() { Data = stockjournalGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult CsdConversionEdit(string ConversionID)
        {
            ConversionEdit editConversionDetails = new ConversionEdit();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            editConversionDetails = invoiceContext.CsdConversionEdit(ConversionID);
            var ConversioneditDataset = new
            {
                varHeader = editConversionDetails.ConversionHeaderEdit,
                varDetails = editConversionDetails.ConversionDetailsEdit,

            };
            return Json(new
            {
                ConversioneditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult StockAdjustmentEdit(string StockjournalID)
        {
            StockJournalEdit editStockJournalDetails = new StockJournalEdit();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            editStockJournalDetails = invoiceContext.StockAdjustmentEdit(StockjournalID);
            var StockJournaleditDataset = new
            {
                varHeader = editStockJournalDetails.StockJournalHeaderEdit,
                varDetails = editStockJournalDetails.StockJournalDetailsEdit,

            };
            return Json(new
            {
                StockJournaleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult GetStorelocation()
        {
            List<StorelocationList> storelocation = new List<StorelocationList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            storelocation = invoiceContext.GetStorelocation();
            return Json(storelocation);
        }

        [HttpPost]
        public JsonResult GetReason(string MenuID)
        {
            List<ReasonList> reason = new List<ReasonList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            reason = invoiceContext.GetReason(MenuID);
            return Json(reason);
        }

        [HttpPost]
        public JsonResult GetJournalCategory()
        {
            List<StockJournalCategoryList> journalcategory = new List<StockJournalCategoryList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            journalcategory = invoiceContext.GetJournalCategory();
            return Json(journalcategory);
        }

        [HttpPost]
        public JsonResult GetJournalProduct(string DepotID, string CategoryID, string Mode)
        {
            List<StockJournalProductList> journalproduct = new List<StockJournalProductList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            journalproduct = invoiceContext.GetJournalProduct(DepotID, CategoryID, Mode);
            return Json(journalproduct);
        }

        [HttpPost]
        public JsonResult GetInterStoreLocationProduct(string DepotID)
        {
            List<InterStoreLocationProductList> shiftlocationproduct = new List<InterStoreLocationProductList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            shiftlocationproduct = invoiceContext.GetInterStoreLocationProduct(DepotID);
            return Json(shiftlocationproduct);
        }

        [HttpPost]
        public JsonResult GetJournalPacksize()
        {
            List<StockJournalPacksizeList> journalpacksize = new List<StockJournalPacksizeList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            journalpacksize = invoiceContext.GetJournalPacksize();
            return Json(journalpacksize);
        }

        [HttpPost]
        public JsonResult GetJournalBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo,string StorelocationID)
        {
            List<DepotBatchInfoList> batchlist = new List<DepotBatchInfoList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            string MRP = "0";
            batchlist = invoiceContext.GetBatchDetails(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(), MRP.Trim(), StorelocationID.Trim());
            return Json(batchlist);
        }

        [HttpPost]
        public JsonResult GetJournalRMPMBatchDetails(string DepotID, string ProductID, string BatchNo, string StorelocationID)
        {
            List<DepotBatchInfoList> batchlist = new List<DepotBatchInfoList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            batchlist = invoiceContext.GetRMPMBatchDetails(DepotID.Trim(), ProductID.Trim(), BatchNo.Trim(), StorelocationID.Trim());
            return Json(batchlist);
        }

        [HttpPost]
        public JsonResult GetExcessBatchDetails(string DepotID, string ProductID, string Packsize, string BatchNo, string FromDate, string ToDate, string MRP, string StorelocationID)
        {
            List<DepotExcessBatchInfoList> excessbatchlist = new List<DepotExcessBatchInfoList>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            excessbatchlist = invoiceContext.GetExcessBatchDetails(DepotID.Trim(), ProductID.Trim(), Packsize.Trim(), BatchNo.Trim(), FromDate.Trim(), ToDate.Trim(), MRP.Trim(), StorelocationID.Trim());
            return Json(excessbatchlist);
        }

        [HttpPost]
        public JsonResult JournalProductType(string ProductID)
        {
            List<StockJournalProductType> journalproductType = new List<StockJournalProductType>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            journalproductType = invoiceContext.JournalProductType(ProductID.Trim());
            return Json(journalproductType, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNetSaleAmount(string CustomerID, string DepotID, string Finyear, string InvoiceID)
        {
            List<CustomerNetSale> netsale = new List<CustomerNetSale>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            netsale = invoiceContext.GetNetSale(CustomerID.Trim(), DepotID.Trim(), Finyear.Trim(), InvoiceID.Trim());
            return Json(netsale, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTCSLimit(string CustomerID)
        {
            List<TCSPercentageLimit> tcsLimit = new List<TCSPercentageLimit>();
            DepotInvoiceContext invoiceContext = new DepotInvoiceContext();
            tcsLimit = invoiceContext.GetTCSPercentLimit(CustomerID.Trim());
            return Json(tcsLimit, JsonRequestBehavior.AllowGet);
        }

    }
}