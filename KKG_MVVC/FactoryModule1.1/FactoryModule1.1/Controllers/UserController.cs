using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using FactoryModel;
using FactoryDatacontext;
using System.Web.Mvc;
using FactoryModule1.Helpers;

namespace FactoryModule1.Controllers
{
   
    public class usercontroller : Controller
    {
        public ICacheManager _ICacheManager;

        Usercontext usercontext = new Usercontext();
        // GET: User
        public usercontroller()
        {
            _ICacheManager = new CacheManager();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)

        {
            
         
            if (!string.IsNullOrEmpty(loginViewModel.Username) && !string.IsNullOrEmpty(loginViewModel.Password))
            {
                var username = loginViewModel.Username;
                var password = loginViewModel.Password;
                users user = usercontext.ValidateUser(username, password);
                if (user != null)
                {
                    _ICacheManager.Add("UserID", user.USERID );
                    _ICacheManager.Add("USERTYPE", user.USERTYPE);
                    _ICacheManager.Add("UserName", user.USERNAME);
                    _ICacheManager.Add("FNAME", user.FNAME);
                    _ICacheManager.Add("MNAME", user.MNAME);
                    _ICacheManager.Add("LNAME", user.LNAME);
                    _ICacheManager.Add("USERTAG", user.USERTAG);
                    _ICacheManager.Add("TPU", user.TPU);
                  
                    Session["UserID"] = user.USERID.Trim();
                    Session["USERTYPE"] = user.USERTYPE.Trim();
                    Session["UTypeId"] = user.USERTYPE.Trim();
                    Session["UTNAME"] = user.UTNAME.Trim();
                    Session["FNAME"] = user.FNAME.Trim();
                    Session["UserName"] = user.USERNAME.Trim();
                    Session["APPLICABLETO"] = user.APPLICABLETO.Trim();
                    Session["TPU"] = user.TPU.Trim();
                    Session["IUserID"] = user.IUSERID.Trim();
                    Session["USERTAG"] = user.USERTAG.Trim();

                    ViewBag.errormessage = "successful";
                    ViewBag.finyr = Bindfinyr();
                } 
                else
                {
                    ViewBag.errormessage = "Entered Invalid Username and Password";
                    return View(loginViewModel);
                }
            }
            else
            {
                
            }
            return View(loginViewModel);
        }
        public List<Finyear> Bindfinyr()
        {
            List<Finyear> finyr = new List<Finyear>();
            finyr = usercontext.Bindfinyear();
            return finyr;
        }

        [HttpPost]
        public JsonResult Getfinyr(string finyr)
        {
            Session["FINYEAR"] = finyr.Trim();
            _ICacheManager.Add("FINYEAR", finyr.Trim());
            return Json(finyr, JsonRequestBehavior.AllowGet);

        }

        }
}