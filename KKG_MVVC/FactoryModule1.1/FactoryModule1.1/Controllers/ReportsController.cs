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
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult itemLedger_ERP()
        {
            return View();
        }
        public ActionResult ledgerReport_erp()
        {
            onepointlogin();
            return View();
        }
        public ActionResult einvoice_report()
        {
            return View();
        }

        public ICacheManager _ICacheManager;
        public ReportsController()
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
                _ICacheManager.Add("TPU", userInfo["TPU"].ToString());
                HttpContext.Session["IUserID"] = userInfo["IUserID"].ToString();
                HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());

            }

        }

        [HttpPost]
        public JsonResult Reportfinyrchk(string _finyr)
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            if(_finyr==null ||_finyr == "" )
            {
                var FINYEAR = _ICacheManager.Get<object>("FINYEAR");
                _finyr = FINYEAR.ToString();
            }
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
        public JsonResult GetSourceDepotFromUser(string userid)
        {
       
            List<SourceDepot> sourcedepot = new List<SourceDepot>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            sourcedepot = dispatchContext.GetSourceDepot(userid.ToString().Trim());
            return Json(sourcedepot);
        }
        [HttpPost]
        public JsonResult BindDepot_Primary()
        {
            
            string mode = "Depot_Primary";
            List<ReportBranch> branches = new List<ReportBranch>();
            ReportContext Report = new ReportContext();
            branches = Report.BindDepot_Primary(mode,"");
            return Json(branches);
        }
        [HttpPost]
        public JsonResult BindDeptName(string depotid)
        {
            string mode = "DeptName";
            List<ReportBranch> reportBranches = new List<ReportBranch>();
            ReportContext Report = new ReportContext();
            reportBranches = Report.BindDepot_Primary(mode, depotid);
            return Json(reportBranches);
        }
        [HttpPost]
        public JsonResult Depot_Accounts(string IUSEID)
        {
            string mode = "Depot_Acc";
            //string IUSEID = "";
            //var iuserid = _ICacheManager.Get<object>("IUserID");
            //IUSEID = iuserid.ToString();
            List<ReportBranch> reportBranches = new List<ReportBranch>();
            ReportContext Report = new ReportContext();
            reportBranches = Report.BindDepot_Primary(mode, IUSEID);
            return Json(reportBranches);
        }
        [HttpPost]
        public JsonResult BindProductALIAS()
        {
            List<ReportProductlias> reportProductlias = new List<ReportProductlias>();
            ReportContext Report = new ReportContext();
            reportProductlias = Report.LoadProductALIAS();
            return Json(reportProductlias);
        }
        [HttpPost]
        public JsonResult LoadStorelocation()
        {
            List<ReportStorelocation> reportStorelocations = new List<ReportStorelocation>();
            ReportContext Report = new ReportContext();
            reportStorelocations = Report.LoadStorelocation();
            return Json(reportStorelocations);
        }
        [HttpPost]
        public JsonResult LoadItemLedgerExport(string fromDate, string toDate, string depot, string product, string storeLocation)
        {
            List<ReportModel> response = new List<ReportModel>();
            ReportContext Report = new ReportContext();
            response = Report.BindItemLedgerExport(fromDate, toDate, depot, product, storeLocation);
            return Json(response);
        }
        [HttpPost]
        public JsonResult LoadItemLedger(string fromDate, string toDate, string depot, string product, string storeLocation)
        {
            List<ReportModel> response = new List<ReportModel>();
            ReportContext Report = new ReportContext();
            response = Report.BindItemLedger(fromDate, toDate, depot, product, storeLocation);
            return Json(response);
        }

        /*ledger report*/
        [HttpPost]
        public JsonResult Region_foraccounts(string UTNAME,string userid)
        {
            if(UTNAME==null || UTNAME=="")
            {
                var uname = _ICacheManager.Get<object>("UTNAME");
                UTNAME = uname.ToString();
            }
            if (userid == null || userid == "")
            {
                var user = _ICacheManager.Get<object>("UTNAME");
                userid = user.ToString();
            }

            List<ReportBranch> reportStorelocations = new List<ReportBranch>();
            ReportContext Report = new ReportContext();
            reportStorelocations = Report.LoadRegion_foraccounts(userid, "");
            return Json(reportStorelocations);
        }
        public JsonResult BindForLedgerReport_DepotWise(string UTNAME,string userid)
        {
            if(UTNAME==null || UTNAME=="")
            {
                var uname = _ICacheManager.Get<object>("UTNAME");
                UTNAME = uname.ToString();
            }
            if (userid == null || userid == "")
            {
                var user = _ICacheManager.Get<object>("UTNAME");
                userid = user.ToString();
            }

            List<ReportBranch> reportStorelocations = new List<ReportBranch>();
            ReportContext Report = new ReportContext();
            reportStorelocations = Report.LoadRegion_foraccounts(userid, "");
            return Json(reportStorelocations);
        }
        public JsonResult LoadJc(string mode)
        {
            List<ReportJcMaster> ReportJcMaster = new List<ReportJcMaster>();
            ReportContext Report = new ReportContext();
            ReportJcMaster = Report.LoadJc(mode);
            return Json(ReportJcMaster);
        }
        public JsonResult LoadTimeSpan(string mode,string Tag, string Finyear)
        {
            List<ReportTimeSpan> ReportJcMaster = new List<ReportTimeSpan>();
            ReportContext Report = new ReportContext();
            ReportJcMaster = Report.LoadTimeSpan(mode, Tag, Finyear);
            return Json(ReportJcMaster);
        }
        public JsonResult FetchDateRange(string span, string Tag, string _FinYear)
        {
            List<ReportTimeSpan> ReportTimeSpan = new List<ReportTimeSpan>();
            ReportContext Report = new ReportContext();
            ReportTimeSpan = Report.FetchDateRange(span, Tag, _FinYear);
            return Json(ReportTimeSpan);
        }
        public JsonResult BindLedgerReport(string fromDate, string toDate, string ledgerId,string depotId,string FINYEAR)
        {
            List<LedgerReportModel> LedgerReportModel = new List<LedgerReportModel>();
            ReportContext Report = new ReportContext();
            LedgerReportModel = Report.BindLedgerReport(fromDate, toDate, ledgerId, depotId, FINYEAR);
            return Json(LedgerReportModel);
        }


        //Added By Aisha Pain for E-invoice report(04-12-2020)
        [HttpPost]
        public JsonResult BindPendingEinvoiceGrid(string FromDate, string ToDate,string depotID)
        {
            List<EinvoicePendingReportModel> pendingeinvoice = new List<EinvoicePendingReportModel>();
            ReportContext einvoiceContext = new ReportContext();
            pendingeinvoice = einvoiceContext.BindPendingEinvoiceGrid(FromDate.Trim(), ToDate.Trim(), depotID.Trim());
            return new JsonResult() { Data = pendingeinvoice, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

    }
}