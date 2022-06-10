using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using SubItemModel;
using FactoryModel;
using FactoryDatacontext;

namespace FactoryModule1._1.Controllers
{
    public class SubItemController : Controller
    {
        // GET: SubItem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubItem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BindSubItem()
        {            
            List<BindSubItemGrid> SubItemGrid = new List<BindSubItemGrid>();
            SubItemContext ObjMasterContext = new SubItemContext();
            SubItemGrid = ObjMasterContext.BindSubItem();
            return Json(SubItemGrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubItemSave(CRUDSubItem SubItemSave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                SubItemContext ObjContext = new SubItemContext();
                responseMessage = ObjContext.SubItemInsertUpdate(SubItemSave).ToList();
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
        public ActionResult EditSubItem(string SubItemID)
        {
            SubItemEditList ObjSubItem = new SubItemEditList();
            SubItemContext ObjContext = new SubItemContext();
            ObjSubItem = ObjContext.EditSubItem(SubItemID);
            var allDataset = new
            {
                varHeader = ObjSubItem.EditList,
            };
            return Json(new
            {
                allDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult IsExists(string Name, string ID)
        {
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            string messageid1 = "";
            try
            {
                SubItemContext masterContext = new SubItemContext();
                responseMessage = masterContext.IsExists(Name.Trim(), ID.Trim(), "2");
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
    }
}