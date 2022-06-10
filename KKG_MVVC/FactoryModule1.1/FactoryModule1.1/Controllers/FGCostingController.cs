using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using FactoryDatacontext;
using FactoryModel;

namespace FactoryModule1._1.Controllers
{
    public class FGCostingController : Controller
    {
        // GET: FGCosting
        public ICacheManager _ICacheManager;

        public FGCostingController()
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
                //HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FGRateSheet()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult FGCostingMasterReport()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public JsonResult LoadBranch()
        {
            /*var userid = _ICacheManager.Get<object>("UserID");*/
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            List<BranchList> branchlist = new List<BranchList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            branchlist = fgcostingcontext.LoadBranch(userid.ToString());
            return Json(branchlist);
        }

        public JsonResult LoadDivision()
        {
            List<DivisionList> brandlist = new List<DivisionList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            brandlist = fgcostingcontext.LoadDivision();
            return Json(brandlist);
        }

        public JsonResult LoadDivCategory(string DIVID)
        {
            List<CategoryList> categorylist = new List<CategoryList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            categorylist = fgcostingcontext.LoadCategory(DIVID);
            return Json(categorylist);
        }

        public JsonResult LoadFGItem(string DIVID, string CATID, string BRANCHID)
        {
            List<ProductList> fgitemlist = new List<ProductList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            fgitemlist = fgcostingcontext.LoadFGItem(DIVID, CATID, BRANCHID);
            return Json(fgitemlist);
        }

        public JsonResult LoadFGRateSheetList(string BRANCHID, string DIVID, string CATID, string FROMDATE, string TODATE, char FLAG)
        {
            List<FGRateSheetModel> fgratelist = new List<FGRateSheetModel>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            fgratelist = fgcostingcontext.LoadFGRateSheetList(BRANCHID, DIVID, CATID, FROMDATE, TODATE, FLAG);
            return Json(fgratelist);
        }

        [HttpPost]
        public JsonResult ratesheetsubmitdata(FGRateSheetModel ratesheetsave)
        {
            /*var userid = _ICacheManager.Get<object>("UserID");*/
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            ratesheetsave.USERID = userid.ToString();
            List<MessageModel> responseMessage = new List<MessageModel>();

            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            responseMessage = fgcostingcontext.FGRateSheetInsertUpdate(ratesheetsave);
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadFGBOMReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBOMList> fgbomlist = new List<FGBOMList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            fgbomlist = fgcostingcontext.LoadFGBOMReport(ITEMID, BRANCHID, FROMDATE, TODATE);
            return Json(fgbomlist);
        }

        public JsonResult LoadFGBulkCostReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBulkCostList> fgbulkcostlist = new List<FGBulkCostList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            fgbulkcostlist = fgcostingcontext.LoadFGBulkCostReport(ITEMID, BRANCHID,FROMDATE, TODATE);
            return Json(fgbulkcostlist);
        }

        public JsonResult LoadFGBulkBOMReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBulkBOMList> fgbulkbomlist = new List<FGBulkBOMList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            fgbulkbomlist = fgcostingcontext.LoadFGBulkBOMReport(ITEMID, BRANCHID, FROMDATE, TODATE);
            return Json(fgbulkbomlist);
        }

        public JsonResult LoadMaterialRateChartReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            var FINYEAR = _ICacheManager.Get<object>("FINYEAR");
            List<MaterialRateChartList> materiallist = new List<MaterialRateChartList>();
            FactoryFGCostingContext fgcostingcontext = new FactoryFGCostingContext();
            materiallist = fgcostingcontext.LoadMaterialRateChartReport(ITEMID, BRANCHID, FROMDATE, TODATE, FINYEAR.ToString());
            return Json(materiallist);
        }
    }
}