using FactoryDatacontext;
using FactoryModel;
using FactoryModule1.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FactoryModule1._1.Controllers
{
    public class AutomailController : Controller
    {
        public ICacheManager _ICacheManager;
        public AutomailController()
        {
            _ICacheManager = new CacheManager();
        }
        // GET: Automail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Automailmaster()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult Automailusers()
        {
           // onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult AutomailTemplate()
        {
           // onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
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

        public JsonResult loadcustomertype()
        {


            List<CustomerinfoAutomail> distibutortype = new List<CustomerinfoAutomail>();
            Claimscontext Claims = new Claimscontext();
            distibutortype = Claims.Loadcustomertype();
            return Json(distibutortype);
        }

        public JsonResult loadDistributorFromCustomerType(string type)
        {


            List<CustomerinfoAutomail> distibutor = new List<CustomerinfoAutomail>();
            Claimscontext Claims = new Claimscontext();
            distibutor = Claims.LoadDistributorFromCustomerType(type);
            return Json(distibutor);
        }

        public JsonResult insertAutomailDetails(ServiceproviderDetails detail)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext(); 
            //if (LMBU == null || LMBU == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    LMBU = userid.ToString();
            //}
            responseMessage = saleOrderContext.InsertAutomailDetails(detail);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult bindAutomaildetails()
        {
            
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BindAutomaildetails();
            return Json(LedgerReportModel);
        }
        public JsonResult loadserviceprovider(string mode)
        {
            
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.Loadserviceprovider(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult loadservicemodule(string mode)
        {
            
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.Loadservicemodule(mode);
            return Json(LedgerReportModel);
        }

        public JsonResult Getpageurl(string moduleid)
        {

            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.Getpageurl(Int32.Parse(moduleid));
            return Json(LedgerReportModel);
        }

        public JsonResult insertAutomailSchedulerDetails(ServiceproviderDetails detail)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext();
           
            responseMessage = saleOrderContext.InsertAutomailSchedulerDetails(detail);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult bindautomailGrid(string mode)
        {

            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BindautomailGrid(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult editTemplate(string TemplateID)
        {

            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.EditTemplate(TemplateID);
            return Json(LedgerReportModel);
        }
        public JsonResult loadReportmodulename(string mode)
        {

            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadReportmodulename(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult loadserviceSubscriberEmail(string mode,string repoid)
        {

            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadserviceSubscriberEmail(mode, repoid);
            return Json(LedgerReportModel);
        }
        public JsonResult loadserviceSubscriberUsers(string mode)
        {

            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadserviceSubscriberUsers(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult getuserMail(string mode)
        {

            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.GetuserMail(mode);
            return Json(LedgerReportModel);
        }

        public JsonResult UpdateAutomailemailDetails(AutomailEmailDetails detail)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext();

            responseMessage = saleOrderContext.UpdateAutomailemailDetails(detail);
            
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult automailInsertEmaildetails(AutomailEmailDetails detail)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext();

            responseMessage = saleOrderContext.AutomailInsertEmaildetails(detail);
            
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }


      
    }
}