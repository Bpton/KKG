using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using Masterfacmodel;
using FactoryDatacontext;
using FactoryModel;
using FactoryModule1._1.Helpers;

namespace FactoryModule1._1.Controllers
{
  
    public class MasterfacController : Controller
    {
        // GET: Masterfac
        public ICacheManager _ICacheManager;

        public MasterfacController()
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
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult PrimaryItem()
        {
            return View();
        }

        public ActionResult MaterialMaster()
        {
            onepointlogin();
            HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        [HttpPost]
        public JsonResult GetPrimaryItem()
        {
            List<PrimaryItemList> primaryitem = new List<PrimaryItemList>();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            primaryitem = masterContext.GetPrimaryItem();
            return Json(primaryitem);
        }

        [HttpPost]
        public JsonResult GetSubItem(string PrimaryItemID)
        {
            List<SubItemList> subitem = new List<SubItemList>();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            subitem = masterContext.GetSubitem(PrimaryItemID);
            return Json(subitem);
        }

        public ActionResult SubItemVendor(string PrimaryItemID)
        {
            SubItemVendorList subitemvendor = new SubItemVendorList();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            subitemvendor = masterContext.SubItemVendor(PrimaryItemID.Trim());
            var allDataset = new
            {
                varSubItem = subitemvendor.SubItemList,
                varVendor = subitemvendor.VendorList,
            };
            return Json(new
            {
                allDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult GetUom()
        {
            List<UomList> uom = new List<UomList>();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            uom = masterContext.GetUom();
            return Json(uom);
        }

        [HttpPost]
        public JsonResult GetCustomer(string FactoryID)
        {
            List<CustomerList> customer = new List<CustomerList>();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            customer = masterContext.GetCustomer(FactoryID);
            return Json(customer);
        }

        [HttpPost]
        public JsonResult MaterialMasterSaveData(MaterialMasterModel materilamastersave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                var userid = _ICacheManager.Get<object>("UserID");
                FactoryMastercontext masterContext = new FactoryMastercontext();
                responseMessage = masterContext.MaterialInsertUpdate(materilamastersave, Convert.ToInt32(userid.ToString().Trim())).ToList();
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
        public JsonResult BindMaterialMasterGrid()
        {
            List<MaterialMasterList> productGrid = new List<MaterialMasterList>();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            var userid = _ICacheManager.Get<object>("UserID");
            string depotid = "";
            if (Convert.ToString(userid).Trim() == "8" || Convert.ToString(userid).Trim() == "4133" || Convert.ToString(userid).Trim() == "9012" || Convert.ToString(userid).Trim() == "3873" || Convert.ToString(userid).Trim() == "3827")
            {
                productGrid = masterContext.BindMaterialMasterGrid(Convert.ToString(userid).Trim(), depotid);
            }
            else
            {
                depotid = "NA";
                productGrid = masterContext.BindMaterialMasterGrid(Convert.ToString(userid).Trim(), depotid);
            }

            return new JsonResult() { Data = productGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult IsExists(string ProductName, string Code)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            try
            {
                FactoryMastercontext masterContext = new FactoryMastercontext();
                responseMessage = masterContext.IsExists(ProductName.Trim(), Code.Trim());
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

        public ActionResult EditMaterial(string ProductID)
        {
            MaterialEditList material = new MaterialEditList();
            FactoryMastercontext masterContext = new FactoryMastercontext();
            material = masterContext.EditMaterial(ProductID.Trim());
            var allDataset = new
            {
                varHeader = material.HeaderEditList,
                varFactory = material.FactoryEditList,
                varVendor = material.VendorEditList,
                varCustomer = material.CustomerEditList,
                varPacksize = material.PacksizeEditList,
            };
            return Json(new
            {
                allDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult ProductMaster()
        {
            //onepointlogin();
            //HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public JsonResult LOADBRAND()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext pmaster = new FactoryMastercontext();
            Productmaster = pmaster.LoadBrand();
            return Json(Productmaster);
        }
        public JsonResult LOADCATEGORY()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext catname = new FactoryMastercontext();
            Productmaster = catname.LOADCATEGORY();
            return Json(Productmaster);
        }
        public JsonResult BindBrandByCatname(string BRANDID)
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext catname = new FactoryMastercontext();
            Productmaster = catname.BindBrandByCatname(BRANDID);
            return Json(Productmaster);
        }
        public JsonResult LOADNATURE()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext NATURENAME = new FactoryMastercontext();
            Productmaster = NATURENAME.LOADNATURE();
            return Json(Productmaster);
        }

        public JsonResult LOADUOM()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext UOM = new FactoryMastercontext();
            Productmaster = UOM.LOADUOM();
            return Json(Productmaster);
        }

        public JsonResult LOADFRAGNANCE()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext FRAGNANCE = new FactoryMastercontext();
            Productmaster = FRAGNANCE.LOADFRAGNANCE();
            return Json(Productmaster);
        }

        public JsonResult LOADITEMTYPE()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext LOADITEMTYPE = new FactoryMastercontext();
            Productmaster = LOADITEMTYPE.LOADITEMTYPE();
            return Json(Productmaster);
        }
        
         public JsonResult LOADDEPOT()
        {
            List<ProductMasterModel> Productmaster = new List<ProductMasterModel>();
            FactoryMastercontext DEPOTNAME = new FactoryMastercontext();
            Productmaster = DEPOTNAME.LOADDEPOT();
            return Json(Productmaster);
        }
    }
}