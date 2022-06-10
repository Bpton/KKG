using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FactoryModel;
using FactoryDatacontext;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using System.IO;
using System.Collections;
using FactoryModule1._1.Helpers;

namespace FactoryModule1._1.Controllers
{
    public class TranDepotStockController : Controller
    {
        public ICacheManager _ICacheManager;
        // GET: Transporter
        public TranDepotStockController()
        {
            _ICacheManager = new CacheManager();
        }
        // GET: TranDepotStock
        public void onepointlogin()
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
            List<users> users = new List<users>();

            if (Request.Cookies["userInfo"] != null)
            {

                HttpContext.Session["UserID"] = userInfo["UserID"].ToString();
                _ICacheManager.Add("UserID", userInfo["UserID"].ToString());
                _ICacheManager.Add("USERTYPE", userInfo["USERTYPE"].ToString());
                HttpContext.Session["UTypeId"] = userInfo["UTypeId"].ToString();
                //HttpContext.Session["UTNAME"] = userInfo["UTNAME"].ToString();
                //HttpContext.Session["FNAME"] = userInfo["FNAME"].ToString();
                //HttpContext.Session["APPLICABLETO"] = userInfo["APPLICABLETO"].ToString();
                _ICacheManager.Add("TPU", userInfo["TPU"].ToString());
                HttpContext.Session["TPU"] = userInfo["TPU"].ToString();
                _ICacheManager.Add("IUserID", userInfo["IUserID"].ToString());
                HttpContext.Session["IUserID"] = userInfo["IUserID"].ToString();
                HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());





            }
        }
        public DataTable CreateDataTableTaxComponent()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAG", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_DISPATCH_FG"] = dt;
            return dt;
        }
        public DataTable CreateDataTableTaxComponentbos()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAG", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_DISPATCHbos"] = dt;
            return dt;
        }
        //fin yr check
        [HttpPost]
        public JsonResult finyrchk(string finyr)
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            // var finyr = _ICacheManager.Get<object>("FINYEAR");
            string _finyr = finyr;
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

        public ActionResult Index()
        {
           // onepointlogin();

            return View();
        }
        public ActionResult DepotStocktransfer()
        {
            //onepointlogin();
            return View();
        }
        public ActionResult depostktransferBOS()
        {
            return View();
        }
        public ActionResult Interbatchtransfer()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Bindsourcedepot(string user)
        {
            string userid = user;
            List<Motherdepot> Motherdepot = new List<Motherdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Motherdepot = TrandepotStkContext.BindDepotBasedOnUser(userid);
            return Json(Motherdepot);
        }
        [HttpPost]
        public JsonResult BindToDepo(string depotid)
        {
            
            List<Motherdepot> Motherdepot = new List<Motherdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Motherdepot = TrandepotStkContext.BindToDepo(depotid);
            return Json(Motherdepot);
        }
        [HttpPost]
        public JsonResult BindOrderType()
        {
        
            List<Ordertype> Ordertype = new List<Ordertype>();
             TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Ordertype = TrandepotStkContext.BindOrderType();
            return Json(Ordertype);
        }
        [HttpPost]
        public JsonResult BindCountry()
        {

            List<Countrystock> Countrystock = new List<Countrystock>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Countrystock = TrandepotStkContext.BindCountry();
            return Json(Countrystock);
        }
        [HttpPost]
        public JsonResult BindSaleOrder(string countryid)
        {

            List<Saleorder> Saleorder = new List<Saleorder>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Saleorder = TrandepotStkContext.BindSaleOrder(countryid);
            return Json(Saleorder);
        }
        [HttpPost]
        public JsonResult Bindinscomp(string menuid)
        {

            List<Insurancecodepot> Insurancecodepot = new List<Insurancecodepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Insurancecodepot = TrandepotStkContext.Bindinscomp(menuid);
            return Json(Insurancecodepot);
        }
        [HttpPost]
        public JsonResult BindSTNInsuranceNumber(string companyid)
        {

            List<Insurancenodepot> Insurancenodepot = new List<Insurancenodepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Insurancenodepot = TrandepotStkContext.BindSTNInsuranceNumber(companyid);
            return Json(Insurancenodepot);
        }
        [HttpPost]
        public JsonResult BindWayBillNo(string depotid, string transferid)
        {

            List<Waybill> Waybill = new List<Waybill>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Waybill = TrandepotStkContext.BindWayBillNo(depotid,transferid);
            return Json(Waybill);
        }
        [HttpPost]
        public JsonResult BindCATEGORY()
        {

            List<Categorydepot> Categorydepot = new List<Categorydepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Categorydepot = TrandepotStkContext.BindCATEGORY();
            return Json(Categorydepot);
        }
        [HttpPost]
        public JsonResult BindCATEGORYBOS()
        {


            List<Categorydepot> Categorydepot = new List<Categorydepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Categorydepot = TrandepotStkContext.BindCATEGORYBOS();
            return Json(Categorydepot);
        }
        [HttpPost]
        public JsonResult LoadProduct(string depotid, string saleorderid, string typeid)
        {

            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdepot = TrandepotStkContext.LoadProduct(depotid, saleorderid,typeid);
            return Json(Productdepot);
        }
        [HttpPost]
        public JsonResult BindProduct_depotwise(string depotid, string todepotid)
        {

            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdepot = TrandepotStkContext.BindProduct_depotwise(depotid,todepotid);
            return Json(Productdepot);
        }
        [HttpPost]
        public JsonResult LoadProductBOS(string depotid, string saleorderid, string typeid)
        {
            

            
            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            if (typeid == "9A555D40-5E12-4F5C-8EE0-E085B5BAB169")
            {
                Productdepot = TrandepotStkContext.LoadProductBOS(depotid, saleorderid, typeid);
            }
            return Json(Productdepot);
        }
        [HttpPost]
        public JsonResult LoadCategoryWiseProduct(string catid, string depotid, string saleorderid, string typeid)
        {

            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdepot = TrandepotStkContext.LoadCategoryWiseProduct(catid, depotid, saleorderid,typeid);
            return Json(Productdepot);
        }

        [HttpPost]
        public JsonResult LoadCategoryWiseProductBOS(string catid, string depotid, string saleorderid, string typeid)
        {

            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdepot = TrandepotStkContext.LoadCategoryWiseProductBOS(catid, depotid, saleorderid, typeid);
            return Json(Productdepot);
        }
        [HttpPost]
        public JsonResult BindTranspoter(string depotid)
        {

            List<Transporterdepot> Transporterdepot = new List<Transporterdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Transporterdepot = TrandepotStkContext.BindTranspoter(depotid);
            return Json(Transporterdepot);
        }
        [HttpPost]
        public JsonResult ShippingAddress(string depotid)
        {

            List<Deliveryaddress> Deliveryaddress = new List<Deliveryaddress>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Deliveryaddress = TrandepotStkContext.ShippingAddress(depotid);
            return Json(Deliveryaddress);
        }
        [HttpPost]
        public JsonResult DeliveryDay(string fromdepot, string todepoid, string invoicedate)
        {

            List<Transitdays> Transitdays = new List<Transitdays>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Transitdays = TrandepotStkContext.DeliveryDay(fromdepot,todepoid,invoicedate);
            return Json(Transitdays);
        }
        [HttpPost]
        public JsonResult ProductTypeChecking(string ProductID)
        {

            List<ProductType> ProductType = new List<ProductType>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            ProductType = TrandepotStkContext.ProductTypeChecking(ProductID);
            return Json(ProductType);
        }
        [HttpPost]
        public JsonResult BindPackSize_productwise(string ProductID)
        {

            List<Packsizedepot> Packsize = new List<Packsizedepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Packsize = TrandepotStkContext.BindPackSize_productwise(ProductID);
            return Json(Packsize);
        }
        [HttpPost]
        public JsonResult BindBatchDetails(string DepotID, string ProductID, string PacksizeID, string BatchNo)
        {

            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Batchdetail = TrandepotStkContext.BindBatchDetails(DepotID,ProductID,PacksizeID,BatchNo);
            return Json(Batchdetail);
        }
        [HttpPost]
        public JsonResult BindRM_PM_BatchDetails(string DepotID, string ProductID, string BatchNo, string StoreLocation)
        {

            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Batchdetail = TrandepotStkContext.BindRM_PM_BatchDetails(DepotID, ProductID, BatchNo, StoreLocation);
            return Json(Batchdetail);
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
        public JsonResult BindDepotRate(string Productid, string MRP, string Date)
        {
            decimal _mrp = 0;
            _mrp = decimal.Parse(MRP);
            List<DepotRatesheet> DepotRatesheet = new List<DepotRatesheet>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            DepotRatesheet = TrandepotStkContext.BindDepotRate(Productid,_mrp,Date);
            return Json(DepotRatesheet);
           
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
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["TAG"] = "N";
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["TAG"] = "N";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCH_FG"] = dt;
            return new EmptyResult();
        }
        [HttpPost]
        public EmptyResult FillTaxDatatablebos(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_DISPATCHbos"] == null)
            {
                CreateDataTableTaxComponentbos();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCHbos"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["TAG"] = "N";
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["TAG"] = "N";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCHbos"] = dt;
            return new EmptyResult();
        }
        //for edit
        [HttpPost]
        public EmptyResult FillTaxDatatableEdit(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_DISPATCH_FG"] == null)
            {
                CreateDataTableTaxComponent();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCH_FG"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["TAG"] = "N";
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["TAG"] = "N";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCH_FG"] = dt;
            return new EmptyResult();
        }
        [HttpPost]
        public EmptyResult FillTaxDatatableEditbos(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag)
        {
            if (Session["TAXDETAILS_DISPATCHbos"] == null)
            {
                CreateDataTableTaxComponentbos();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCHbos"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["TAG"] = "N";
                dr["MRP"] = MRP;
            }
            else
            {
                dr["PRIMARYPRODUCTID"] = "NA";
                dr["PRIMARYPRODUCTBATCHNO"] = "NA";
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["TAG"] = "N";
                dr["MRP"] = 0;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_DISPATCHbos"] = dt;
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
        public EmptyResult DeleteTaxDatatablebos(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_DISPATCHbos"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_DISPATCHbos"] = DtTax;
            return new EmptyResult();
        }

        public ActionResult GetCalculateAmtInPcs(string Productid, string PacksizeID, decimal Qty, decimal Rate, string date, decimal _qty,string taxname)
        {
            CalculateAmtInPcsFAC calculateamtpcs = new CalculateAmtInPcsFAC();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            calculateamtpcs = TrandepotStkContext.GetCalculateAmtInPcs(Productid, PacksizeID, Qty, Rate,date,_qty,taxname);
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
        public ActionResult GetTotalQuantity(string Productid, string FromPackSizeID, decimal Qty)
        {
            QuantityDetails qtydetails = new QuantityDetails();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            qtydetails = TrandepotStkContext.CalculateQuantity(Productid, FromPackSizeID, Qty);
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
        public JsonResult Depotstocktransave(Depostockmodel Depostockmodel)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Depostockmodel.userid;
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Depostockmodel.Finyear;
               
                string ModuleID = "46";
                string EntryFrom = "MVC";

                DataTable dtTax = ReturnTaxTable();

                if (string.IsNullOrEmpty(Depostockmodel.LRGRNO))
                {
                    Depostockmodel.LRGRNO = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.SHIPINGADDRESS))
                {
                    Depostockmodel.SHIPINGADDRESS = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.DELIVERYDATE))
                {
                    Depostockmodel.DELIVERYDATE = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.REMARKS))
                {
                    Depostockmodel.REMARKS = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.VEHICLENO))
                {
                    Depostockmodel.VEHICLENO = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.CHALLANNO))
                {
                    Depostockmodel.CHALLANNO = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.GATEPASSNO))
                {
                    Depostockmodel.GATEPASSNO = "";
                }

                if (Depostockmodel.REMARKS != null)
                {
                    if (Convert.ToString(Depostockmodel.REMARKS).Contains("'"))
                    {
                        Depostockmodel.REMARKS = Convert.ToString(Depostockmodel.REMARKS).Replace("'", "''");
                    }
                }



                TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
                responseMessage = TrandepotStkContext.Depotstocktransave(Depostockmodel,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        ModuleID, EntryFrom,
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
        public JsonResult DepotstocktranBOSsave(Depostockmodel Depostockmodel)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Depostockmodel.userid;
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Depostockmodel.Finyear;

                string ModuleID = "2778";
                string EntryFrom = "MVC";

                DataTable dtTax = ReturnTaxTablebos();

                if (string.IsNullOrEmpty(Depostockmodel.LRGRNO))
                {
                    Depostockmodel.LRGRNO = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.SHIPINGADDRESS))
                {
                    Depostockmodel.SHIPINGADDRESS = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.DELIVERYDATE))
                {
                    Depostockmodel.DELIVERYDATE = "";
                }

                if (string.IsNullOrEmpty(Depostockmodel.REMARKS))
                {
                    Depostockmodel.REMARKS = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.VEHICLENO))
                {
                    Depostockmodel.VEHICLENO = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.CHALLANNO))
                {
                    Depostockmodel.CHALLANNO = "";
                }
                if (string.IsNullOrEmpty(Depostockmodel.GATEPASSNO))
                {
                    Depostockmodel.GATEPASSNO = "";
                }

                if (Depostockmodel.REMARKS != null)
                {
                    if (Convert.ToString(Depostockmodel.REMARKS).Contains("'"))
                    {
                        Depostockmodel.REMARKS = Convert.ToString(Depostockmodel.REMARKS).Replace("'", "''");
                    }
                }



                TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
                responseMessage = TrandepotStkContext.DepotstocktranBOSsave(Depostockmodel,
                                                                        Convert.ToInt32(userid.ToString().Trim()),
                                                                        Finyear.ToString().Trim(),
                                                                        ModuleID, EntryFrom,
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

        public DataTable ReturnTaxTable()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_DISPATCH_FG"];
            return dtTax;
        }
        public DataTable ReturnTaxTablebos()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_DISPATCHbos"];
            return dtTax;
        }
        public EmptyResult RemoveSession()
        {
            Session.Remove("TAXDETAILS_DISPATCH_FG");
            return new EmptyResult();
        }
        public EmptyResult RemoveSessionbos()
        {
            Session.Remove("TAXDETAILS_DISPATCHbos");
            return new EmptyResult();
        }
        [HttpPost]
        public JsonResult Bindstocktransfer(string FromDate, string ToDate, string CheckerFlag, string depotID,string finyr)
        {
             string type = "9A555D40-5E12-4F5C-8EE0-E085B5BAB169";
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();

            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = finyr;
            List<StocktransferGrid> StocktransferGrid = new List<StocktransferGrid>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            StocktransferGrid = TrandepotStkContext.Bindstocktransfer(FromDate, ToDate, Finyear.ToString().Trim(), CheckerFlag, userid.ToString().Trim(), depotID, type);
            return new JsonResult() { Data = StocktransferGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult Bindstocktransferbos(string FromDate, string ToDate, string CheckerFlag, string depotID, string finyr,string userid)
        {
            string type = "9A555D40-5E12-4F5C-8EE0-E085B5BAB169";
            //var userid = _ICacheManager.Get<object>("UserID");
            
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
            string Finyear = finyr;
            List<StocktransferGrid> StocktransferGrid = new List<StocktransferGrid>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            StocktransferGrid = TrandepotStkContext.Bindstocktransferbos(FromDate, ToDate, Finyear.ToString().Trim(), CheckerFlag, userid.ToString().Trim(), depotID, type);
            return new JsonResult() { Data = StocktransferGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        [HttpPost]
        public JsonResult deletedispatch(string DispatchID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
                responseMessage = TrandepotStkContext.deletedispatch(DispatchID).ToList();
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
        public JsonResult Confirmdispatch(string DispatchID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
                responseMessage = TrandepotStkContext.Confirmdispatch(DispatchID).ToList();
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
        public ActionResult Stocktransferedit(string DispatchID)
        {
            Stocktransferedit Stocktransferedit = new Stocktransferedit();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Stocktransferedit = TrandepotStkContext.Stocktranedit(DispatchID);
            var alleditDataset = new
            {
                varHeader = Stocktransferedit.Stocktransferhdredit,
                varDetails = Stocktransferedit.Stocktransferdtledit,
                varTaxCount = Stocktransferedit.Stocktransfertaxcountedit,
                varTax = Stocktransferedit.Stocktransfertaxedit,
                varFooter = Stocktransferedit.Stocktransferfooteredit,
            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }
        [HttpPost]
        public JsonResult GetTaxid(string taxname)
        {

            List<Stocktransfertax> Stocktransfertax = new List<Stocktransfertax>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Stocktransfertax = TrandepotStkContext.GetTaxid(taxname);
            return Json(Stocktransfertax);
        }
        [HttpPost]
        public JsonResult GetHSNTaxOnEdit(string transferid, string taxid, string productid, string batchno)
        {

            List<Stocktransfertax> Stocktransfertax = new List<Stocktransfertax>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Stocktransfertax = TrandepotStkContext.GetHSNTaxOnEdit(transferid,taxid,productid,batchno);
            return Json(Stocktransfertax);
        }
        /// <summary>
        /// Inter batch transfer
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindDepo(string depotid)
        {
            
            List<Motherdepot> Motherdepot = new List<Motherdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Motherdepot = TrandepotStkContext.BindDepo(depotid);
            return Json(Motherdepot);
        }
        [HttpPost]
        public JsonResult BindStorelocation()
        {

            List<Transporterdepot> store = new List<Transporterdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            store = TrandepotStkContext.BindStorelocation();
            return Json(store);
        }
        [HttpPost]
        public JsonResult LoadReason(string menuid)
        {

            List<Transporterdepot> store = new List<Transporterdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            store = TrandepotStkContext.LoadReason(menuid);
            return Json(store);
        }
        [HttpPost]
        public JsonResult BindProduct(string depotid)
        {

            List<Productdepot> Productdepot = new List<Productdepot>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdepot = TrandepotStkContext.BindProduct(depotid);
            return Json(Productdepot);
        }
        [HttpPost]
        public JsonResult GetToProductBCP(string Productid, string MRP)
        {
            decimal _mrp = decimal.Parse(MRP);
            List<DepotRatesheet> DepotRatesheet = new List<DepotRatesheet>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            DepotRatesheet = TrandepotStkContext.GetToProductBCP(Productid,_mrp    );
            return Json(DepotRatesheet);
        }
        [HttpPost]
        public JsonResult GetToProductdetails(string Productid)
        {

            List<Productdtlinterbatch> Productdtlinterbatch = new List<Productdtlinterbatch>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdtlinterbatch = TrandepotStkContext.GetToProductdetails(Productid);
            return Json(Productdtlinterbatch);
        }
        [HttpPost]
        public JsonResult GetfrmProductdetails(string Productid,string batchno)
        {

            List<Productdtlinterbatch> Productdtlinterbatch = new List<Productdtlinterbatch>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Productdtlinterbatch = TrandepotStkContext.GetfrmProductdetails(Productid, batchno);
            return Json(Productdtlinterbatch);
        }
        [HttpPost]
        public JsonResult BindBatchDetailsInterbatch(string DepotID, string ProductID, string PacksizeID, string BatchNo,  string storelocation)
        {
            decimal _mrp = 0;
            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Batchdetail = TrandepotStkContext.BindBatchDetailsInterbatch(DepotID,  ProductID,  PacksizeID,  BatchNo, _mrp,  storelocation);
            return Json(Batchdetail);
        }
        [HttpPost]
        public JsonResult LoadToBatchDetails(string DepotID,string ProductID,  string BatchNo, string FromDate, string ToDate, string StoreLocation)
        {

            List<Batchdetail> Batchdetail = new List<Batchdetail>();
            TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
            Batchdetail = TrandepotStkContext.LoadToBatchDetails(ProductID, DepotID, BatchNo,  FromDate,  ToDate,  StoreLocation);
            return Json(Batchdetail);
        }
        //[HttpPost]
        //public JsonResult BindBatchDetailsInterbatch(string DepotID, string ProductID, string PacksizeID, string BatchNo, string MRP, string storelocation)
        //{
        //    decimal _mrp = decimal.Parse(MRP);
        //    List<Batchdetail> Batchdetail = new List<Batchdetail>();
        //    TrandepotStkContext TrandepotStkContext = new TrandepotStkContext();
        //    Batchdetail = TrandepotStkContext.BindBatchDetailsInterbatch(DepotID, ProductID, PacksizeID, BatchNo, _mrp, storelocation);
        //    return Json(Batchdetail);
        //}



    }
}