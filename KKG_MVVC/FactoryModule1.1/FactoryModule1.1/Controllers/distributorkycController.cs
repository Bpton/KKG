using FactoryDatacontext;
using FactoryModel;
using FactoryModule1._1.Helpers;
using FactoryModule1.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FactoryModule1._1.Controllers
{
    public class distributorkycController : Controller
    {
        public ICacheManager _ICacheManager;
        public distributorkycController()
        {
            _ICacheManager = new CacheManager();
        }
        // GET: distributorkyc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult distributorDetails()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult distributorKycRpt()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult Distributorkyc_pan_india_rpt()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult Distributorkyc_asm_zsm_wise_rpt()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult saleinvoice_update()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult usermaster_documentupload()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult coc_document_upload()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult panindia_jd_rpt()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult distributorkycRpt_asmwise()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult attendesCorrection()
        {
            //onepointlogin();
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
        public JsonResult LoadDisttype(string USERID)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistributorType> DISTRIBUTOtype = new List<DistributorType>();
            Claimscontext Claims = new Claimscontext();
            DISTRIBUTOtype = Claims.LoadDisttype(USERID);
            return Json(DISTRIBUTOtype);
        }
        public JsonResult LoadDistributorFromType(string USERID, string TYPE,string BSID)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> DISTRIBUTOtypeWIsebind = new List<DistribuotrInfo>();
            Claimscontext Claims = new Claimscontext();
            DISTRIBUTOtypeWIsebind = Claims.DistributorFromType(USERID, TYPE,BSID);
            return Json(DISTRIBUTOtypeWIsebind);
        }

        public JsonResult bindDistributordepot(string USERID, string TYPE, string mode, string distribut)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> distibutorDepot = new List<DistribuotrInfo>();
            Claimscontext Claims = new Claimscontext();
            distibutorDepot = Claims.BindDistributorDepot(USERID, TYPE, mode, distribut);
            return Json(distibutorDepot);
        }
        public JsonResult bindDistributorstate(string USERID, string TYPE, string mode, string distribut)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> distibutorstate = new List<DistribuotrInfo>();
            Claimscontext Claims = new Claimscontext();
            distibutorstate = Claims.BindDistributorstate(USERID, TYPE, mode, distribut);
            return Json(distibutorstate);
        }

        public JsonResult bindDistributoraddress(string USERID, string TYPE, string mode, string distribut)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> distibutorstate = new List<DistribuotrInfo>();
            Claimscontext Claims = new Claimscontext();
            distibutorstate = Claims.BindDistributoraddress(USERID, TYPE, mode, distribut);
            return Json(distibutorstate);
        }
        public JsonResult bindDistributorKycDetails(string distribut, string type, string mode)
        {

            List<DistribuotrInfo> distibutorstate = new List<DistribuotrInfo>();
            Claimscontext Claims = new Claimscontext();
            distibutorstate = Claims.loadDistributorKycDetails(distribut, type, mode);
            return Json(distibutorstate);
        }

        public JsonResult loadcompanyDetails()
        {


            List<DistributorCompany> distibutorstate = new List<DistributorCompany>();
            Claimscontext Claims = new Claimscontext();
            distibutorstate = Claims.LoadcompanyDetails();
            return Json(distibutorstate);
        }

        public JsonResult InsertdistribuTorDetails(DistribuotrInfo detail, string LMBU)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext();
            //if (LMBU == null || LMBU == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    LMBU = userid.ToString();
            //}
            responseMessage = saleOrderContext.InsertdistribuTorDetails(detail, LMBU);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFileInfo(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<DistribuotrInfo> responseMessage = new List<DistribuotrInfo>();
                    Claimscontext saleOrderContext = new Claimscontext();
                    HttpFileCollectionBase files = Request.Files;

                    string DIST_ID = formCollection["distibid"];
                    for (int i = 0; i < files.Count; i++)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);


                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(fname);

                        responseMessage = saleOrderContext.UploadFileInfo(DIST_ID, path, fname).ToList();
                    }

                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

        public JsonResult binddistributorKycReport(string USERID, string fromDate, string toDate,string MTTYPE, string MTID)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BinddistributorKycReport(USERID, fromDate, toDate,MTTYPE ,MTID);
            return Json(LedgerReportModel);
        }
        /*-----asm------*/
        public JsonResult binddistributorKycAsmReport(string USERID, string fromDate, string toDate, string MTTYPE, string MTID)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}

            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BinddistributorKycAsmReport(USERID, fromDate, toDate, MTTYPE, MTID);
            return Json(LedgerReportModel);
        }
        public JsonResult binddistributorKycpanReport(string BSID)
        {
            List<DistribuotrpanindiaInfo> LedgerReportModel = new List<DistribuotrpanindiaInfo>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BinddistributorKycpanReport(BSID);
            return Json(LedgerReportModel);
        }
        public JsonResult loadcompanyDetailsForddl(string distribut)
        {


            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadcompanyDetailsForddl(distribut);
            return Json(LedgerReportModel);
        }
        public JsonResult binddistributorKyc_asm_zsmReport(string USERID, string USERTYPE)
        {
            //if (USERID == null || USERID == "")
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    USERID = userid.ToString();
            //}
            //if (USERTYPE == null || USERTYPE == "")
            //{
            //    var userid = _ICacheManager.Get<object>("USERTYPE");
            //    USERTYPE = userid.ToString();
            //}

            List<DistribuotrpanindiaInfo> DISTRIBUTOtype = new List<DistribuotrpanindiaInfo>();
            Claimscontext Claims = new Claimscontext();
            DISTRIBUTOtype = Claims.LoaddistributorKyc_asm_zsmReport(USERID, USERTYPE);
            return Json(DISTRIBUTOtype);
        }



        /*----------------updater----------------------------*/
        public JsonResult loadDistDepot(string mode)
        {

            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadDistDepot(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult loadDistributorFromDepot(string mode, string type)
        {

            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadDistributorFromDepot(mode, type);
            return Json(LedgerReportModel);
        }

        public JsonResult loadSaleinvoicenoFromParty(string mode, string type, string cusid, string startdate, string enddate)
        {

            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadSaleinvoicenoFromParty(mode, type, cusid, startdate, enddate);
            return Json(LedgerReportModel);
        }
        public JsonResult loadTsiFromParty(string mode)
        {

            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadTsiFromParty(mode);
            return Json(LedgerReportModel);
        }
        public JsonResult loadUpdateSaleinvoiceno(string mode, string saleinvoiceid, string sfid, string tsiname)
        {

            List<messageresponse> LedgerReportModel = new List<messageresponse>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadUpdateSaleinvoiceno(mode, saleinvoiceid, sfid, tsiname);
            return Json(LedgerReportModel);
        }
        public JsonResult bindsaleinvoiceGrid(string mode, string saleinvoiceid)
        {

            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BindsaleinvoiceGrid(mode, saleinvoiceid);
            return Json(LedgerReportModel);
        }
        /*--------------USERMASTERDOC---------------*/
        public JsonResult bindUser(string MODE, string USERID)
        {
            if (USERID == null || USERID == "")
            {
                var userid = _ICacheManager.Get<object>("UserID");
                USERID = userid.ToString();
            }

            List<UsermasterDocument> LedgerReportModel = new List<UsermasterDocument>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.bindUser(MODE, USERID);
            return Json(LedgerReportModel);
        }
        public ActionResult UploadFileReportInfo(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<UsermasterDocument> responseMessage = new List<UsermasterDocument>();
                    Claimscontext saleOrderContext = new Claimscontext();
                    HttpFileCollectionBase files = Request.Files;
                    string mode = "U";
                    string User_ID = formCollection["userid"];
                    for (int i = 0; i < files.Count; i++)
                    {
                        string _fname = string.Empty;
                        string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);


                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(fname);
                        //_fname = filename;
                        responseMessage = saleOrderContext.UploadFileReportInfo(mode, User_ID, path, filename).ToList();
                    }

                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }


        }
        public ActionResult UploadFileReportInfococ(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<UsermasterDocument> responseMessage = new List<UsermasterDocument>();
                    Claimscontext saleOrderContext = new Claimscontext();
                    HttpFileCollectionBase files = Request.Files;
                    string mode = "IC";
                    string User_ID = formCollection["userid"];
                    for (int i = 0; i < files.Count; i++)
                    {
                        string _fname = string.Empty;
                        string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/";
                        string filename = Path.GetFileName(Request.Files[i].FileName);


                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(fname);
                        //_fname = filename;
                        responseMessage = saleOrderContext.UploadFileReportInfo(mode, User_ID, path, filename).ToList();
                    }

                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }


        }
        public JsonResult InsertUserDetails( UsermasterDocument detail)
        {

            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext saleOrderContext = new Claimscontext();
           
            responseMessage = saleOrderContext.InsertUserDetails( detail);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }
        public JsonResult bindUserDocReport(string MODE,string USERID ,string fromDate, string toDate)
         {
            if (USERID == null || USERID == "")
            {
                var userid = _ICacheManager.Get<object>("UserID");
                USERID = userid.ToString();
            }

            List<UsermasterDocument> LedgerReportModel = new List<UsermasterDocument>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BindUserDocReport(MODE, USERID, fromDate, toDate);
            return Json(LedgerReportModel);
        }

        //public JsonResult GetFileforDownload(string userid,string fileName)/*seceondary sales*/
        //{
        //    List<messageresponse> messageresponses = new List<messageresponse>();


        //    //Hashtable hashTable = new Hashtable();
        //    //hashTable.Add("@P_USERID", userid);
        //    //DButility dbcon = new DButility();


        //    //DataSet ds = dbcon.SysFetchDataInDataSet("USP_GET_FILE", hashTable);
        //    //string dtSaleorder = ds.Tables[0].ToString();
        //    string FileName = fileName;
        //    string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadFiles\\") + FileName;
        //    messageresponses.Add(new messageresponse
        //    {
        //        response = filepath
        //    });
        //    return Json("../UploadFiles/" + FileName);
        //}


        public ActionResult Downloads(string userid)
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
            HttpContext.Session["ID"] = userid;
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/UploadFiles/"));
            FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }
        public ActionResult Download(string ImageName)
        {
            List<string> list = new List<string>();

            string USERID = "";
            var userid = _ICacheManager.Get<object>("UserID");
            USERID = userid.ToString();

            //var userid = _ICacheManager.Get<object>("UserID");
            ImageName = getFileName(USERID);

            var FileVirtualPath = "~/UploadFiles/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        public string getFileName(string USERID)
        {
            USERID = "949";
            Hashtable hashTable = new Hashtable();
            hashTable.Add("@p_userid", USERID);
            DButility dbcon = new DButility();
            DataSet ds = dbcon.SysFetchDataInDataSet("USP_GET_FILE_BYUSER", hashTable);
            string ConName = ds.Tables[0].Rows[0]["FILE_PATH"].ToString();
            return (ConName);

        }
        /*--------------USERMASTERPAN---------------*/
        public JsonResult bindUserJdReport()
        {
            List<UsermasterPanDocument> LedgerReportModel = new List<UsermasterPanDocument>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.BindUserJdReport();
            return Json(LedgerReportModel);
        }
        /*-------attendence--------*/
        public JsonResult loadUsertype()
        {
            List<UserattendenceMaster> LedgerReportModel = new List<UserattendenceMaster>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.Loadusertype();
            return Json(LedgerReportModel);
        }
        public JsonResult loadUsername( string Mode)
        {
            List<UserattendenceMaster> LedgerReportModel = new List<UserattendenceMaster>();
            Claimscontext Report = new Claimscontext();
            LedgerReportModel = Report.LoadUsername(Mode);
            return Json(LedgerReportModel);
        }
        public JsonResult updateAttendence(string userid,string uname,string date,string intime,string outtime)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext Report = new Claimscontext();
            responseMessage = Report.UpdateAttendence(userid, uname, date, intime, outtime);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);


        }

    }

}