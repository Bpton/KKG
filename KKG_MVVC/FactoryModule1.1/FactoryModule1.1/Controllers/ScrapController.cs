using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryModel;
using FactoryDatacontext;

namespace FactoryModule1._1.Controllers
{
    public class ScrapController : Controller
    {
        // GET: Scrap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Scrap()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserDepartment(string FactoryID)
        {
            List<UserDepartmentList> UserDepartment = new List<UserDepartmentList>();
            ScrapContext masterContext = new ScrapContext();
            UserDepartment = masterContext.GetUserDepartment(FactoryID);
            return Json(UserDepartment);
            //"FFC65354-AB46-4983-A67F-111486EC3D39"
        }

        [HttpPost]
        public JsonResult GetUserDepartmentWise(string USERTYPEID)
        {
            List<UserList> UserDtls = new List<UserList>();
            ScrapContext masterContext = new ScrapContext();
            UserDtls = masterContext.GetUserDepartmentWise(USERTYPEID);
            return Json(UserDtls);
        }

        [HttpPost]
        public JsonResult GetScrapSubItem(string PrimaryID)
        {
            List<ScrapSubItem> SubItemDtls = new List<ScrapSubItem>();
            ScrapContext masterContext = new ScrapContext();
            SubItemDtls = masterContext.GetScrapSubItem(PrimaryID);
            return Json(SubItemDtls);
        }

        [HttpPost]
        public JsonResult GetScrapProduct(string SubtypeID, string FactoryID)
        {
            List<ScrapProduct> ProductDtls = new List<ScrapProduct>();
            ScrapContext masterContext = new ScrapContext();
            ProductDtls = masterContext.GetScrapProduct(SubtypeID, FactoryID);
            return Json(ProductDtls);
        }

        [HttpPost]
        public JsonResult GetScrapProductUOM(string ProductID)
        {
            List<ScrapProductUOM> ProductDtls = new List<ScrapProductUOM>();
            ScrapContext masterContext = new ScrapContext();
            ProductDtls = masterContext.GetScrapProductUOM(ProductID);
            return Json(ProductDtls);
        }

        [HttpPost]
        public JsonResult ScrapHeaderSavedata(ScrapModel ScrapHeaderSave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                ScrapContext ObjContext = new ScrapContext();
                responseMessage = ObjContext.CrudScrap(ScrapHeaderSave).ToList();

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
        public JsonResult BindScrapRequestGrid(string FromDate, string ToDate, string Checker, string FinYear, string DepotID)
        {
            List<BindScrapRequest> ScrapGrid = new List<BindScrapRequest>();
            ScrapContext scrapContext = new ScrapContext();
            ScrapGrid = scrapContext.BindScrapRequest(FromDate.Trim(), ToDate.Trim(), Checker.Trim(), FinYear.Trim(), DepotID.Trim());
            return new JsonResult() { Data = ScrapGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult ScrapEdit(string ScrapID)
        {
            ScrapEdit editscrap = new ScrapEdit();
            ScrapContext scrapContext = new ScrapContext();
            editscrap = scrapContext.ScrapEdit(ScrapID);
            var alleditDataset = new
            {
                varHeader = editscrap.ScrapHeaderRequestEdit,
                varDetails = editscrap.ScrapDetailsRequestEdit,
            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult ScrapRecvdSavedata(ScrapModel ScrapHeaderSave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                ScrapContext ObjContext = new ScrapContext();
                responseMessage = ObjContext.SaveScrapRecvdData(ScrapHeaderSave).ToList();

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