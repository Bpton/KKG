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
    
    public class tranfacController : Controller
    {
       
     
        // GET: tranfac
        public ICacheManager _ICacheManager;

        public tranfacController()
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
        public ActionResult FactoryDispatchFG()
        {

            /*For Production*/
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            //if ((Session["UserID"].ToString() != "" || Session["UserID"].ToString() != null))
            //{
            //    return View();
            //}
            //else
            //{
            //    return Redirect("http://mcnroeerp.com/Factory/View/frmAdminLogin.aspx");
            //}

            /*For Developement*/
            return View();
        }

        public ActionResult FactoryDispatchRMPM()
        {

            /*For Production*/
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            //if ((Session["UserID"].ToString() != "" || Session["UserID"].ToString() != null))
            //{
            //    return View();

            //}
            //else
            //{
            //    return Redirect("http://mcnroeerp.com/Factory/View/frmAdminLogin.aspx");
            //}

            /*For Developement*/
            return View();
        }

        public ActionResult FactoryInvoiceFG()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult FactoryInvoiceTrading()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult FactoryInvoiceExportGST()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult FactoryInvoiceExport()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public DataTable CreateDataTableTaxComponent()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_DISPATCH_FG"] = dt;
            return dt;
        }

        public DataTable CreateDataTableTaxComponentRM()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_DISPATCH_RM"] = dt;
            return dt;
        }

        public DataTable CreateDataTableTaxComponentFGInvoice()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_INVOICE_FG"] = dt;
            return dt;
        }

        public DataTable CreateDataTableTaxComponentTradingInvoice()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_INVOICE_TRADING"] = dt;
            return dt;
        }

        public DataTable CreateDataTableTaxComponentExportInvoice()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_INVOICE_EXPORT"] = dt;
            return dt;
        }

        public DataTable CreateDataTableTaxComponentExportNormal()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"] = dt;
            return dt;
        }

        public DataTable ReturnTaxTable()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_DISPATCH_FG"];
            return dtTax;
        }

        public DataTable ReturnTaxTableRM()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_DISPATCH_RM"];
            return dtTax;
        }

        public DataTable ReturnTaxTableInvoice()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_INVOICE_FG"];
            return dtTax;
        }

        public DataTable ReturnTaxTableTrading()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_INVOICE_TRADING"];
            return dtTax;
        }

        public DataTable ReturnTaxTableExport()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_INVOICE_EXPORT"];
            return dtTax;
        }

        public DataTable ReturnTaxTableExportNormal()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"];
            return dtTax;
        }

        [HttpPost]
        public JsonResult GetSourceDepot()
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            List<SourceDepot> sourcedepot = new List<SourceDepot>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            sourcedepot = dispatchContext.GetSourceDepot(userid.ToString().Trim());
            return Json(sourcedepot);
        }

        [HttpPost]
        public JsonResult GetDestinationDepot(string SourceDepot)
        {
            List<DestinationDepot> destinationedepot = new List<DestinationDepot>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            destinationedepot = dispatchContext.GetDestinationDepot(SourceDepot.Trim());
            return Json(destinationedepot);
        }

        [HttpPost]
        public JsonResult GetFGInvoiceCustomer(string FactoryID,string BSID,string GroupID)
        {
            List<FGCustomer> fgCustomer = new List<FGCustomer>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            fgCustomer = dispatchContext.GetfgCustomer(FactoryID.Trim(), BSID.Trim(), GroupID.Trim());
            return Json(fgCustomer);
        }

        [HttpPost]
        public JsonResult GetTradingInvoiceCustomer(string FactoryID, string BSID, string GroupID)
        {
            List<FGCustomer> tradingCustomer = new List<FGCustomer>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            tradingCustomer = dispatchContext.GetTradingCustomer(FactoryID.Trim(), BSID.Trim(), GroupID.Trim());
            return Json(tradingCustomer);
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
        public JsonResult GetEntryLockChecking(string EntryDate)
        {
            List<EntryLockChecking> lockdate = new List<EntryLockChecking>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            string FirstFinYear = Finyear.ToString().Substring(0, 4);
            string SecondFinYear = Finyear.ToString().Substring(5, 4);
            string FirstFinYearDt = "01/04/" + FirstFinYear;
            string SecondFinYearDt = "31/03/" + SecondFinYear;
            lockdate = dispatchContext.GetEntryLockChecking(EntryDate.Trim(), Finyear.ToString().Trim(), FirstFinYearDt.Trim(), SecondFinYearDt.Trim());
            return Json(lockdate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetWaybillno(string DepotID)
        {
            List<Waybill> Waybill = new List<Waybill>();
            string TransferID = "";
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            Waybill = dispatchContext.GetWaybillno(TransferID, DepotID);
            return Json(Waybill);
        }

        [HttpPost]
        public JsonResult GetInsurancecompany()
        {
            List<InsuranceCompany> incCompany = new List<InsuranceCompany>();
            string menuID = "60";
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            incCompany = dispatchContext.GetInsurancecompany(menuID);
            return Json(incCompany);
        }

        [HttpPost]
        public JsonResult GetPolicyno(string companyID)
        {
            List<PolicyNo> Policy = new List<PolicyNo>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            Policy = dispatchContext.GetPolicyno(companyID);
            return Json(Policy);
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
        public JsonResult fgShippingAddress(string CustomerID)
        {
            List<FGShipplingAddress> shipping = new List<FGShipplingAddress>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            shipping = dispatchContext.fgShippingAddress(CustomerID.Trim());
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
        public JsonResult GetTransitDays(string FromDepot, string ToDepot,string InvoiceDate)
        {
            List<TransitDays> days = new List<TransitDays>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            days = dispatchContext.GetTransitDays(FromDepot.Trim(), ToDepot.Trim(), InvoiceDate.Trim());
            return Json(days, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCategory()
        {
            List<Category> categorylist = new List<Category>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Condition = "HSN";
            categorylist = dispatchContext.GetCategory(Condition.Trim());
            return Json(categorylist);
        }

        [HttpPost]
        public JsonResult GetCategoryRM()
        {
            List<CategoryRM> categorylist = new List<CategoryRM>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Condition = "HSN";
            categorylist = dispatchContext.GetCategoryRM(Condition.Trim());
            return Json(categorylist);
        }

        [HttpPost]
        public JsonResult GetProduct(string SourceDepot,string Type)
        {
            List<ProductList> productlist = new List<ProductList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            productlist = dispatchContext.GetProduct(SourceDepot.Trim(), Type.Trim());
            return Json(productlist);
        }

        [HttpPost]
        public JsonResult GetInvoiceProduct(string SaleOrder)
        {
            List<InvoiceProductList> productlist = new List<InvoiceProductList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            productlist = dispatchContext.GetInvoiceProduct(SaleOrder.Trim());
            return Json(productlist);
        }

        [HttpPost]
        public JsonResult GetPacksize(string ProductID,string Type)
        {
            List<PacksizeList> packlist = new List<PacksizeList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            packlist = dispatchContext.GetPacksize(ProductID, Type);
            return Json(packlist);
        }

        [HttpPost]
        public JsonResult GetTaxcount(string MenuID, string Flag, string DepotID, string ProductID, string CustomerID, string Date)
        {
            List<TaxcountList> taxcount = new List<TaxcountList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            taxcount = dispatchContext.GetTaxcount(MenuID.Trim(), Flag.Trim(), DepotID.Trim().Trim(), ProductID.Trim(), CustomerID.Trim(), Date.Trim());
            return Json(taxcount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceTaxcount(string MenuID, string Flag, string DepotID, string ProductID, string CustomerID, string Date)
        {
            List<FGTaxcountList> fgtaxcount = new List<FGTaxcountList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            fgtaxcount = dispatchContext.GetInvoiceTaxcount(MenuID.Trim(), Flag.Trim(), DepotID.Trim().Trim(), ProductID.Trim(), CustomerID.Trim(), Date.Trim());
            return Json(fgtaxcount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTaxOnEdit(string DispatchID, string TaxID, string ProductID, string BatchNo)
        {
            List<TaxOnEdit> taxonedit = new List<TaxOnEdit>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            taxonedit = dispatchContext.GetTaxOnEdit(DispatchID.Trim(), TaxID.Trim(), ProductID.Trim(), BatchNo.Trim());
            return Json(taxonedit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceTaxOnEdit(string InvoiceID, string TaxID, string ProductID, string BatchNo)
        {
            List<TaxOnEdit> invoicetaxonedit = new List<TaxOnEdit>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            invoicetaxonedit = dispatchContext.GetInvoiceTaxOnEdit(InvoiceID.Trim(), TaxID.Trim(), ProductID.Trim(), BatchNo.Trim());
            return Json(invoicetaxonedit, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetCategoryProduct(string CategoryID, string DepotID,string Type)
        {
            List<ProductList> productlist = new List<ProductList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            productlist = dispatchContext.GetCategoryProduct(CategoryID.Trim(), DepotID.Trim(), Type.Trim());
            return Json(productlist);
        }

        [HttpPost]
        public JsonResult GetBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            List<BatchInfoList> batchlist = new List<BatchInfoList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Storelocation = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";/*Saleable*/
            string MRP = "0";
            batchlist = dispatchContext.GetBatchDetails(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(), MRP.Trim(), Storelocation.Trim());
            return Json(batchlist);
        }

        [HttpPost]
        public JsonResult GetExportBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            List<BatchInfoList> batchlist = new List<BatchInfoList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Storelocation = "5FA9501B-E8D0-4A0F-B917-17C636655514";/*Export*/
            string MRP = "0";
            batchlist = dispatchContext.GetBatchDetails(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(), MRP.Trim(), Storelocation.Trim());
            return Json(batchlist);
        }

        [HttpPost]
        public JsonResult GetBatchDetailsRM(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {
            List<BatchInfoListRM> batchlist = new List<BatchInfoListRM>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Storelocation = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";/*Saleable*/
            string MRP = "0";
            batchlist = dispatchContext.GetBatchDetailsRM(DepotID.Trim(), ProductID.Trim(), PacksizeID.Trim(), BatchNo.Trim(), MRP.Trim(), Storelocation.Trim());
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

        [HttpPost]
        public JsonResult GetTransferRateRM(string productID, decimal mrp, string transferDate)
        {
            List<StockTransferRateListRM> rate = new List<StockTransferRateListRM>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            rate = dispatchContext.GetTransferRateRM(productID.Trim(), mrp, transferDate.Trim());
            return Json(rate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string TaxName, string date)
        {
            CalculateAmtInPcsFAC calculateamtpcs = new CalculateAmtInPcsFAC();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            calculateamtpcs = dispatchContext.GetCalculateAmtInPcs(Productid, PacksizeID, Qty, Rate, Assesment, TaxName, date);
            var allcalculateDataset = new
            {
                varAssesment = calculateamtpcs.assesments,
                varQty = calculateamtpcs.pcsqty,
                varNetwght = calculateamtpcs.netweight,
                varGrosswght = calculateamtpcs.grossweight,
                varHSN = calculateamtpcs.hsn,
                varHSNTax = calculateamtpcs.hsnTax,
                varHSNTaxID = calculateamtpcs.hsnTaxID,

            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public ActionResult GetInvoiceCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string TaxName, string date)
        {
            InvoiceCalculateAmtInPcsFAC calculateinvoiceamtpcs = new InvoiceCalculateAmtInPcsFAC();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            calculateinvoiceamtpcs = dispatchContext.GetInvoiceCalculateAmtInPcs(Productid, PacksizeID, Qty, Rate, Assesment, TaxName, date);
            var allcalculateDataset = new
            {
                varAssesment = calculateinvoiceamtpcs.assesments,
                varQty = calculateinvoiceamtpcs.pcsqty,
                varNetwght = calculateinvoiceamtpcs.netweight,
                varGrosswght = calculateinvoiceamtpcs.grossweight,
                varHSN = calculateinvoiceamtpcs.hsn,
                varHSNTax = calculateinvoiceamtpcs.hsnTax,
                varHSNTaxID = calculateinvoiceamtpcs.hsnTaxID,

            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public ActionResult GetExportCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string TaxName, string date)
        {
            InvoiceCalculateAmtInPcsFAC calculateinvoiceamtpcs = new InvoiceCalculateAmtInPcsFAC();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            calculateinvoiceamtpcs = dispatchContext.GetExportCalculateAmtInPcs(Productid, PacksizeID, Qty, Rate, Assesment, TaxName, date);
            var allcalculateDataset = new
            {
                varAssesment = calculateinvoiceamtpcs.assesments,
                varQty = calculateinvoiceamtpcs.pcsqty,
                varNetwght = calculateinvoiceamtpcs.netweight,
                varGrosswght = calculateinvoiceamtpcs.grossweight,
                varHSN = calculateinvoiceamtpcs.hsn,
                varHSNTax = calculateinvoiceamtpcs.hsnTax,
                varHSNTaxID = calculateinvoiceamtpcs.hsnTaxID,

            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public ActionResult GetInvoiceCalculateAmtInPcsIntra(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string CGST,string SGST,string date)
        {
            InvoiceCalculateAmtInPcsIntraFAC calculateinvoiceamtpcs = new InvoiceCalculateAmtInPcsIntraFAC();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            calculateinvoiceamtpcs = dispatchContext.GetInvoiceCalculateAmtInPcsIntra(Productid, PacksizeID, Qty, Rate, Assesment, CGST, SGST,date);
            var allcalculateDataset = new
            {
                varAssesment = calculateinvoiceamtpcs.assesments,
                varQty = calculateinvoiceamtpcs.pcsqty,
                varNetwght = calculateinvoiceamtpcs.netweight,
                varGrosswght = calculateinvoiceamtpcs.grossweight,
                varHSN = calculateinvoiceamtpcs.hsn,
                varCgstTax = calculateinvoiceamtpcs.cgstpercentage,
                varCgstID = calculateinvoiceamtpcs.cgst,
                varSgstTax = calculateinvoiceamtpcs.sgstpercentage,
                varSgstID = calculateinvoiceamtpcs.sgst,
            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public ActionResult GetTradingCalculateAmtInPcsIntra(string Productid, string PacksizeID, decimal Qty, decimal Rate, decimal Assesment, string CustomerID,string CGST, string SGST, string TCS, string date)
        {
            TradingCalculateAmtInPcsIntraFAC calculateinvoiceamtpcs = new TradingCalculateAmtInPcsIntraFAC();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            calculateinvoiceamtpcs = dispatchContext.GetTradingCalculateAmtInPcsIntra(Productid, PacksizeID, Qty, Rate, Assesment, CustomerID, CGST, SGST, TCS, date);
            var allcalculateDataset = new
            {
                varAssesment = calculateinvoiceamtpcs.assesments,
                varQty = calculateinvoiceamtpcs.pcsqty,
                varNetwght = calculateinvoiceamtpcs.netweight,
                varGrosswght = calculateinvoiceamtpcs.grossweight,
                varHSN = calculateinvoiceamtpcs.hsn,
                varCgstTax = calculateinvoiceamtpcs.cgstpercentage,
                varCgstID = calculateinvoiceamtpcs.cgst,
                varSgstTax = calculateinvoiceamtpcs.sgstpercentage,
                varSgstID = calculateinvoiceamtpcs.sgst,
                varTcsTax = calculateinvoiceamtpcs.tcspercentage,
                varTcsID = calculateinvoiceamtpcs.tcs,

            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        [HttpPost]
        public EmptyResult FillTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_DISPATCH_FG"] == null)
            {
                CreateDataTableTaxComponent();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCH_FG"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCH_FG"] = dt;
            return new EmptyResult();
        }


        [HttpPost]
        public EmptyResult FillTaxDatatableRM(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_DISPATCH_RM"] == null)
            {
                CreateDataTableTaxComponentRM();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCH_RM"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCH_RM"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult FillFgInvoiceTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_INVOICE_FG"] == null)
            {
                CreateDataTableTaxComponentFGInvoice();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_FG"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["PRIMARYPRODUCTID"] = Productid;
                dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_INVOICE_FG"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult FillTradingTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_INVOICE_TRADING"] == null)
            {
                CreateDataTableTaxComponentTradingInvoice();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_TRADING"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["PRIMARYPRODUCTID"] = Productid;
                dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_INVOICE_TRADING"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult FillExportTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_INVOICE_EXPORT"] == null)
            {
                CreateDataTableTaxComponentExportInvoice();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_EXPORT"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["PRIMARYPRODUCTID"] = Productid;
                dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_INVOICE_EXPORT"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult FillExportTaxDatatableNormal(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"] == null)
            {
                CreateDataTableTaxComponentExportNormal();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["PRIMARYPRODUCTID"] = Productid;
                dr["PRIMARYPRODUCTBATCHNO"] = BatchNo;
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatable(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCH_FG"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_DISPATCH_FG"] = DtTax;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatableRM(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCH_RM"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_DISPATCH_RM"] = DtTax;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatableInvoice(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_FG"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_INVOICE_FG"] = DtTax;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatableTrading(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_TRADING"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_INVOICE_TRADING"] = DtTax;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatableExport(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_EXPORT"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_INVOICE_EXPORT"] = DtTax;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatableExportNormal(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_INVOICE_EXPORT_NORMAL"] = DtTax;
            return new EmptyResult();
        }

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
        public JsonResult fgDispatchsavedata(FactoryDispatchModel dispatchsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string InvoiceNo = "";
                string ExportFlag = "N";
                string CountryID = "8F35D2B4-417C-406A-AD7F-7CB34F4D0B35";
                string CountryName = "INDIA";
                string ModuleID = "60";
                string EntryFrom = "M";
                string EntryType = "FG";
                DataTable dtTax = ReturnTaxTable();
                if (dispatchsave.Remarks != null)
                {
                    if (Convert.ToString(dispatchsave.Remarks).Contains("'"))
                    {
                        dispatchsave.Remarks = Convert.ToString(dispatchsave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.DipatchFGInsertUpdate(dispatchsave, InvoiceNo,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        CountryID, CountryName,
                                                                        ModuleID, EntryFrom, EntryType,
                                                                        dtTax).ToList();


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
        public JsonResult rmDispatchsavedata(FactoryDispatchModel dispatchsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string InvoiceNo = "";
                string ExportFlag = "N";
                string CountryID = "8F35D2B4-417C-406A-AD7F-7CB34F4D0B35";
                string CountryName = "INDIA";
                string ModuleID = "2700";
                string EntryFrom = "M";
                string EntryType = "RM";
                
                DataTable dtTax = ReturnTaxTableRM();
                if (dispatchsave.Remarks != null)
                {
                    if (Convert.ToString(dispatchsave.Remarks).Contains("'"))
                    {
                        dispatchsave.Remarks = Convert.ToString(dispatchsave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.DipatchRMInsertUpdate(dispatchsave, InvoiceNo,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        ExportFlag, CountryID, CountryName,
                                                                        ModuleID, EntryFrom, EntryType,
                                                                        dtTax).ToList();


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
        public JsonResult fgInvoicesavedata(FactoryFGInvoiceModel invoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string ModuleID = "2792";
                string BSID = "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5";
                string GroupID = "";
                string SaleOrderDate = "";
                DataTable dtTax = ReturnTaxTableInvoice();
                if (invoicesave.Remarks != null)
                {
                    if (Convert.ToString(invoicesave.Remarks).Contains("'"))
                    {
                        invoicesave.Remarks = Convert.ToString(invoicesave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.InvoiceFGInsertUpdate(invoicesave, BSID, GroupID,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(), SaleOrderDate.Trim(),
                                                                        ModuleID, dtTax).ToList();



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
        public JsonResult tradingInvoicesavedata(FactoryFGInvoiceModel invoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string ModuleID = "2782";
                string BSID = "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5";
                string GroupID = "";
                string SaleOrderDate = "";
                DataTable dtTax = ReturnTaxTableTrading();
                if (invoicesave.Remarks != null)
                {
                    if (Convert.ToString(invoicesave.Remarks).Contains("'"))
                    {
                        invoicesave.Remarks = Convert.ToString(invoicesave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.InvoiceTradingInsertUpdate(invoicesave, BSID, GroupID,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(), SaleOrderDate.Trim(),
                                                                        ModuleID, dtTax).ToList();



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
        public JsonResult exportGSTsavedata(FactoryFGInvoiceModel invoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string ModuleID = "3161";
                string BSID = "2E96A0A4-6256-472C-BE4F-C59599C948B0";
                DataTable dtTax = ReturnTaxTableExport();
                if (invoicesave.Remarks != null)
                {
                    if (Convert.ToString(invoicesave.Remarks).Contains("'"))
                    {
                        invoicesave.Remarks = Convert.ToString(invoicesave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.ExportGSTInsertUpdate(invoicesave, BSID,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        ModuleID, dtTax).ToList();



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
        public JsonResult exportsavedata(FactoryFGInvoiceModel invoicesave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                string ModuleID = "3038";
                string BSID = "2E96A0A4-6256-472C-BE4F-C59599C948B0";
                DataTable dtTax = ReturnTaxTableExportNormal();
                if (invoicesave.Remarks != null)
                {
                    if (Convert.ToString(invoicesave.Remarks).Contains("'"))
                    {
                        invoicesave.Remarks = Convert.ToString(invoicesave.Remarks).Replace("'", "''");
                    }
                }

                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.ExportGSTInsertUpdate(invoicesave, BSID,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        ModuleID, dtTax).ToList();



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
        public JsonResult PackingListInsert(FactoryFGInvoiceModel packingsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.PackingListInsert(packingsave).ToList();
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
        public JsonResult fgdeleteDispatch(string DispatchID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.DipatchFGDelete(DispatchID).ToList();
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
        public JsonResult fgdeleteInvoice(string InvoiceID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.InvoiceFGDelete(InvoiceID).ToList();
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

        public EmptyResult RemoveSession()
        {
            Session.Remove("TAXDETAILS_DISPATCH_FG");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionRM()
        {
            Session.Remove("TAXDETAILS_DISPATCH_RM");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionFGInvoice()
        {
            Session.Remove("TAXDETAILS_INVOICE_FG");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionTradingInvoice()
        {
            Session.Remove("TAXDETAILS_INVOICE_TRADING");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionExportInvoice()
        {
            Session.Remove("TAXDETAILS_INVOICE_EXPORT");
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionExportNormal()
        {
            Session.Remove("TAXDETAILS_INVOICE_EXPORT_NORMAL");
            return new EmptyResult();
        }

        [HttpPost]
        public JsonResult BindFgDispatchGrid(string FromDate, string ToDate, string CheckerFlag, string depotID,string type)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            List<DispatchedFGList> fgdispatchGrid = new List<DispatchedFGList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            fgdispatchGrid = dispatchContext.BindFgDispatchGrid(FromDate, ToDate, Finyear.ToString().Trim(), CheckerFlag, userid.ToString().Trim(), depotID, type);
            return new JsonResult() { Data = fgdispatchGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult BindFgInvoiceGrid(string FromDate, string ToDate, string BSID,string CheckerFlag, string depotID, string type)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            List<FGInvoiceList> fginvoiceGrid = new List<FGInvoiceList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            fginvoiceGrid = dispatchContext.BindFgInvoiceGrid(FromDate, ToDate, Finyear.ToString().Trim(), BSID, depotID, CheckerFlag, userid.ToString().Trim(),type);
            return new JsonResult() { Data = fginvoiceGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
            //return Json(fginvoiceGrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindTradingInvoiceGrid(string FromDate, string ToDate, string BSID, string CheckerFlag, string depotID, string type)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            List<FGInvoiceList> tradinginvoiceGrid = new List<FGInvoiceList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            tradinginvoiceGrid = dispatchContext.BindTradingInvoiceGrid(FromDate, ToDate, Finyear.ToString().Trim(), BSID, depotID, CheckerFlag, userid.ToString().Trim(), type);
            return new JsonResult() { Data = tradinginvoiceGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult BindExportInvoiceGrid(string FromDate, string ToDate, string BSID, string depotID, string type)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            List<ExportInvoiceList> exportinvoiceGrid = new List<ExportInvoiceList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportinvoiceGrid = dispatchContext.BindExportInvoiceGrid(FromDate.Trim(), ToDate.Trim(), Finyear.ToString().Trim(), BSID.Trim(), depotID.Trim(), userid.ToString().Trim(), type);
            return new JsonResult() { Data = exportinvoiceGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult GetFGDispatchStatus(string DispatchID,string ModuleID,string Type)
        {
            
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            try
            {
                FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
                responseMessage = dispatchContext.GetFGDispatchStatus(DispatchID.Trim(), ModuleID.Trim(), Type.Trim());
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

        public ActionResult DipatchFGEdit(string DispatchID)
        {
            DispatchEditFG editFGDetails = new DispatchEditFG();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            editFGDetails = dispatchContext.DipatchFGEdit(DispatchID);
            var alleditDataset = new
            {
                varHeader = editFGDetails.DispatchHeaderEditFG,
                varDetails = editFGDetails.DispatchDetailsEditFG,
                varTaxCount = editFGDetails.DispatchTaxcountEditFG,
                varTax = editFGDetails.DispatchTaxEditFG,
                varFooter = editFGDetails.DispatchFooterEditFG,
            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
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
        public JsonResult GetExportOrderQtyDetails(string InvoiceID, string CustomerID, string SaleOrderID, string ProductID, string DepotID, string PacksizeID,string BSID)
        {
            List<OrderDetailsList> orderlist = new List<OrderDetailsList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Storelocation = "5FA9501B-E8D0-4A0F-B917-17C636655514";/*Export*/
            orderlist = dispatchContext.GetExportOrderQtyDetails(InvoiceID.Trim(), CustomerID.Trim(), SaleOrderID.Trim(), ProductID.Trim(), DepotID.Trim(), PacksizeID.Trim(), BSID.Trim(), Storelocation.Trim());
            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTradingOrderQtyDetails(string CustomerID, string SaleOrderID, string ProductID, string DepotID,string StorelocationID)
        {
            List<TradingOrderDetailsList> tradingorderlist = new List<TradingOrderDetailsList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            //string Storelocation = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";/*Saleable*/
            tradingorderlist = dispatchContext.GetTradingOrderQtyDetails(CustomerID.Trim(), SaleOrderID.Trim(), ProductID.Trim(), DepotID.Trim(), StorelocationID.Trim());
            return Json(tradingorderlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInvoiceConvertedQty(string ProductID, string PacksizeFrom, string PacksizeTo, string Qty)
        {
            List<InvoiceConvertedQty> convertedqtylist = new List<InvoiceConvertedQty>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            convertedqtylist = dispatchContext.GetConvertedQty(ProductID.Trim(), PacksizeFrom.Trim(), PacksizeTo.Trim(), Convert.ToDecimal(Qty.Trim()));
            return Json(convertedqtylist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvoiceFGEdit(string InvoiceID)
        {
            InvoiceEditFG editFGDetails = new InvoiceEditFG();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            editFGDetails = dispatchContext.InvoiceFGEdit(InvoiceID);
            var alleditDataset = new
            {
                varHeader = editFGDetails.InvoiceHeaderEditFG,
                varDetails = editFGDetails.InvoiceDetailsEditFG,
                varTaxCount = editFGDetails.InvoiceTaxcountEditFG,
                varFooter = editFGDetails.InvoiceFooterEditFG,
                varTax = editFGDetails.InvoiceTaxEditFG,
                varOrderDetails = editFGDetails.InvoiceOrderDetailsEditFG,
                varOrderHeader = editFGDetails.InvoiceOrderHeaderEditFG,

            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult InvoiceTradingEdit(string InvoiceID)
        {
            InvoiceEditTrading editFGDetails = new InvoiceEditTrading();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            editFGDetails = dispatchContext.InvoiceTradingEdit(InvoiceID);
            var alleditDataset = new
            {
                varHeader = editFGDetails.InvoiceHeaderEditFG,
                varDetails = editFGDetails.InvoiceDetailsEditTrading,
                varTaxCount = editFGDetails.InvoiceTaxcountEditFG,
                varFooter = editFGDetails.InvoiceFooterEditFG,
                varTax = editFGDetails.InvoiceTaxEditFG,
                varOrderDetails = editFGDetails.InvoiceOrderDetailsEditFG,
                varOrderHeader = editFGDetails.InvoiceOrderHeaderEditFG,

            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult ExportEdit(string InvoiceID)
        {
            ExportEdit editFGDetails = new ExportEdit();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            editFGDetails = dispatchContext.ExportEdit(InvoiceID);
            var alleditDataset = new
            {
                varHeader = editFGDetails.ExportHeaderEdit,
                varDetails = editFGDetails.ExportDetailsEdit,
                varTaxCount = editFGDetails.ExportTaxcountEdit,
                varTax = editFGDetails.ExportTaxEdit,
                varFooter = editFGDetails.ExportFooterEdit,
                varFree = editFGDetails.ExportFreeEdit,
                varOrderHeader = editFGDetails.ExportOrderHeaderEdit,
                varProformaHeader = editFGDetails.ExportProformaHeaderEdit,

            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult GetCountry()
        {
            List<Country> country = new List<Country>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            country = dispatchContext.GetCountry();
            return Json(country);
        }

        [HttpPost]
        public JsonResult GetLoadingPort()
        {
            List<LoadingPort> loadingport = new List<LoadingPort>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            loadingport = dispatchContext.GetLoadingPort();
            return Json(loadingport);
        }

        [HttpPost]
        public JsonResult GetDischargePort()
        {
            List<DischargePort> dischargegport = new List<DischargePort>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            dischargegport = dispatchContext.GetDischargePort();
            return Json(dischargegport);
        }

        [HttpPost]
        public JsonResult ExportSaleOrder(string CountryID)
        {
            List<ExportSaleOrder> exportsaleorder = new List<ExportSaleOrder>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportsaleorder = dispatchContext.GetExportSaleorder(CountryID.Trim());
            return Json(exportsaleorder);
        }

        [HttpPost]
        public JsonResult ExportCustomer(string SaleorderID)
        {
            List<ExportCustomer> exportcustomer = new List<ExportCustomer>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportcustomer = dispatchContext.GetExportCustomer(SaleorderID.Trim());
            return Json(exportcustomer);
        }

        [HttpPost]
        public JsonResult ExportProforma(string SaleorderID)
        {
            List<Proforma> exportproforma = new List<Proforma>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportproforma = dispatchContext.GetProforma(SaleorderID.Trim());
            return Json(exportproforma);
        }

        [HttpPost]
        public JsonResult GetProformaDetails(string ProformaID)
        {
            List<ProformaDetailsList> proformalist = new List<ProformaDetailsList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            proformalist = dispatchContext.GetProformaDetails(ProformaID.Trim());
            return Json(proformalist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLCDetails(string SaleorderID,string ProformaID, string CustomerID)
        {
            List<LCDetailsList> lclist = new List<LCDetailsList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            lclist = dispatchContext.GetLCDetails(SaleorderID.Trim(), ProformaID.Trim(), CustomerID.Trim());
            return Json(lclist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProformaTax(string ProductID, string TaxName, string InvoiceDate)
        {
            List<ProformaTax> proformatax = new List<ProformaTax>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            proformatax = dispatchContext.GetProformaTax(ProductID.Trim(), TaxName.Trim(), InvoiceDate.Trim());
            return Json(proformatax, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetExportProduct(string BSID, string SaleinvoiceID)
        {
            List<ExportProductList> exportproductlist = new List<ExportProductList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            exportproductlist = dispatchContext.GetExportProduct(BSID.Trim(), SaleinvoiceID.Trim());
            return Json(exportproductlist);
        }

        public ActionResult GetProformaAmountDetails(string ProformaID)
        {
            ExportProformaAmountDetails proformavalue = new ExportProformaAmountDetails();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            proformavalue = dispatchContext.GetProformaAmountDetails(ProformaID.Trim());
            var ProformaValueDataset = new
            {
                varProformaAmount = proformavalue.proformaamount,
                varAdjustmentAmount = proformavalue.adjustmentamount,
               
            };
            return Json(new
            {
                ProformaValueDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        [HttpPost]
        public JsonResult BindProformaList(string SaleorderID, string DepotID, string ProformaID, string CustomerID, string ExchangeRate)
        { 
            List<ProformaInvoiceList> proformaGrid = new List<ProformaInvoiceList>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            proformaGrid = dispatchContext.BindProformaList(SaleorderID.Trim(), DepotID.Trim(), ProformaID.Trim(), CustomerID.Trim(), Convert.ToDecimal(ExchangeRate.Trim()));
            return Json(proformaGrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindPackingList(string InvoiceID)
        {
            List<PackingListDetails> packinglistGrid = new List<PackingListDetails>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            packinglistGrid = dispatchContext.BindPackingList(InvoiceID.Trim());
            return Json(packinglistGrid, JsonRequestBehavior.AllowGet);
        }

        //fin yr check
        [HttpPost]
        public JsonResult finyrchk()
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            //var finyr = _ICacheManager.Get<object>("FINYEAR");
            string finyr = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
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
        public JsonResult GetNetSaleAmount(string CustomerID, string DepotID,string InvoiceID)
        {
            List<CustomerNetSaleAmt> netsale = new List<CustomerNetSaleAmt>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
            netsale = dispatchContext.GetNetSale(CustomerID.Trim(), DepotID.Trim(), Finyear.Trim(), InvoiceID.Trim());
            return Json(netsale, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTradingStorelocation()
        {
            List<TradingStorelocation> storelocation = new List<TradingStorelocation>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            storelocation = dispatchContext.GetTradingStorelocation();
            return Json(storelocation);
        }
    }
}