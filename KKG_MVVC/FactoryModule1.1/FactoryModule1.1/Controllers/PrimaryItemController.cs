using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using PrimaryItemModel;
using FactoryModel;
using FactoryDatacontext;

namespace FactoryModule1._1.Controllers
{
    public class PrimaryItemController : Controller
    {
        // GET: PrimaryItem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrimaryItem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BindPrimaryItem(string FromDate, string ToDate)
        {
            //var CustomerID = _ICacheManager.Get<object>("CUSTOMERID");
            List<BindPrimaryItemGrid> nccGrid = new List<BindPrimaryItemGrid>();
            PrimaryItemContext ObjMasterContext = new PrimaryItemContext();
            nccGrid = ObjMasterContext.BindPrimaryItem();
            return Json(nccGrid, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult PrimaryItemSave(CRUDPrimaryItem PrimaryItemSave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //PrimaryItemSave.MODE = 'A';
                PrimaryItemContext ObjContext = new PrimaryItemContext();
                responseMessage = ObjContext.PrimaryItemInsertUpdate(PrimaryItemSave).ToList();
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
        public JsonResult IsExists(string Name, string ID)
        {
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            try
            {
                PrimaryItemContext masterContext = new PrimaryItemContext();
                responseMessage = masterContext.IsExists(Name.Trim(), ID.Trim(), "1");
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
        public ActionResult EditMaterial(string PrimaryID)
        {
            PrimaryEditList ObjPrimary = new PrimaryEditList();
            PrimaryItemContext ObjContext = new PrimaryItemContext();
            ObjPrimary = ObjContext.EditPrimary(PrimaryID.Trim());
            var allDataset = new
            {
                varHeader = ObjPrimary.HeaderEditList,
            };
            return Json(new
            {
                allDataset,
                JsonRequestBehavior.AllowGet
            });
        }
    }
}