using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FactoryModule1._1.Controllers
{
    public class MachineSpareController : Controller
    {
        // GET: MachineSpare
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MachineSpare()
        {
            //onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
    }
}