using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModule1.Helpers;

namespace FactoryModule1._1.Helpers
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public ICacheManager _ICacheManager=new CacheManager()   ;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            var userid = _ICacheManager.Get<object>("UserID");
            //if (HttpContext.Current.Session["ID"] == null)
            //{
            //    filterContext.Result = new RedirectResult("~/Home/Login");
            //    return;
            //}
            if (Convert.ToString(userid) == "")
            {
                filterContext.Result = new RedirectResult("~/user/Logout");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}