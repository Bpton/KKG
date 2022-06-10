using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FactoryModel;
using FactoryDatacontext;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using FactoryModule1._1.Helpers;

namespace FactoryModule1._1.Controllers
{
   
    public class ProcessController : Controller
    {
       
     
        // GET: tranfac
        public ICacheManager _ICacheManager;

        public ProcessController()
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

        public ActionResult ProcessMaster()
        {
           // onepointlogin();
           // HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        public ActionResult BOM()
        {
           // onepointlogin();
           // HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }
        public ActionResult Employee()
        {
           // onepointlogin();
           // HttpCookie userInfo = Request.Cookies["userInfo"];
            return View();
        }

        /*for employe project ***********Start*/

        [HttpPost]
        public JsonResult BindEmployeeDetials()
        {

            List<EmployeeDetails> fgdispatchGrid = new List<EmployeeDetails>();
            FactoryDispatchContext dispatchContext = new FactoryDispatchContext();
            fgdispatchGrid = dispatchContext.BindEmployeeGrid();
            return new JsonResult() { Data = fgdispatchGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        [HttpPost]
        public JsonResult LoadDesignantion()
        {
            List<EmployeeDetails> type = new List<EmployeeDetails>();
            FactoryDispatchContext processContext = new FactoryDispatchContext();
            type = processContext.BindDesignantion();
            return Json(type);
        }

        [HttpPost]
        public JsonResult UpdateEmployeDesignnation(string id, string dId)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryDispatchContext processContext = new FactoryDispatchContext();
                responseMessage = processContext.UpdateEmployeDesignnation(id, dId);
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


        /*for employe project ***********end*/


        [HttpPost]
        public JsonResult saveProcessMaster(FactoryProcessModel processsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                //var Finyear = _ICacheManager.Get<object>("FINYEAR");
                string Finyear = Convert.ToString(HttpContext.Session["FINYEAR"]).Trim();
                FactoryProcessContext processContext = new FactoryProcessContext();
                responseMessage = processContext.ProcessMasterInsertUpdate(processsave,Convert.ToInt32(userid.ToString().Trim())).ToList();
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
        public JsonResult DeleteProcess(string ProcessID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryProcessContext processContext = new FactoryProcessContext();
                responseMessage = processContext.DeleteProcess(ProcessID.Trim()).ToList();
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
        public JsonResult BindProcessMasterGrid(string processid)
        {
            List<ProcessMasterList> processGrid = new List<ProcessMasterList>();
            FactoryProcessContext processContext = new FactoryProcessContext();
            processGrid = processContext.BindProcessMasterGrid(processid);
            return Json(processGrid);
        }

        [HttpPost]
        public JsonResult EditProcess(string Processid)
        {
            List<ProcessEdit> editlist = new List<ProcessEdit>();
            FactoryProcessContext processContext = new FactoryProcessContext();
            editlist = processContext.EditProcess(Processid.Trim());
            return Json(editlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBomProduct()
        {
            List<BomProductList> bomProduct = new List<BomProductList>();
            FactoryProcessContext processContext = new FactoryProcessContext();
            bomProduct = processContext.GetBomProduct();
            return Json(bomProduct);
        }
        [HttpPost]
        public JsonResult BindFrameworkMasterGrid()
        {
            List<BomMasterList> bomGrid = new List<BomMasterList>();
            FactoryProcessContext processContext = new FactoryProcessContext();
            bomGrid = processContext.BindFrameworkGrid();
            return new JsonResult() { Data = bomGrid, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult ProcessSequence()


        {
            ProcessSequenceList loadProcessSequence = new ProcessSequenceList();
            FactoryProcessContext processContext = new FactoryProcessContext();
            //var userid = _ICacheManager.Get<object>("UserID");
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
           // userid = "3";
            loadProcessSequence = processContext.ProcessSequence(userid.ToString().Trim());
            var allsequenceDataset = new
            {
                varInputMaterial = loadProcessSequence.MaterialInputSourceList,
                varOutputMaterial = loadProcessSequence.MaterialOutputSourceList,
                varWorkstation = loadProcessSequence.WorkstationSourceList,
                varResource = loadProcessSequence.ResourceSourceList,
                varQC = loadProcessSequence.QCSourceList,
                varInputUom = loadProcessSequence.InputUomList,
                varOutputUom = loadProcessSequence.OutputUomList,

            };
            return Json(new
            {
                allsequenceDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult BOMsavedata(FactoryProcessModel bomsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                //var userid = _ICacheManager.Get<object>("UserID");
                string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
                userid = "3";
                FactoryProcessContext processContext = new FactoryProcessContext();
                responseMessage = processContext.BomInsertUpdate(bomsave, Convert.ToInt32(userid.ToString().Trim())).ToList();
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

        public ActionResult EditProcessFramework(string FrameworkID)
        {
            FrameworkEditList editFramework = new FrameworkEditList();
            FactoryProcessContext processContext = new FactoryProcessContext();
            editFramework = processContext.EditFramework(FrameworkID.Trim());
            var allEditDataset = new
            {
                varEditHeader = editFramework.FrameworkHeaderList,
                varEditSequence = editFramework.FrameworkSequenceList,
                varEditInputMaterial = editFramework.FrameworkInputMaterialList,
                varEditOutputMaterial = editFramework.FrameworkOutputMaterialList,
                varEditFgMaterial = editFramework.FrameworkFGMaterialList,
                varEditWorkstation = editFramework.FrameworkWorkstationList,
                varEditResource = editFramework.FrameworkResourceList,
                varEditQC = editFramework.FrameworkQCList,
                varEditProcess = editFramework.FrameworkProcessList,

            };
            return Json(new
            {
                allEditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult DeleteFramework(string FrameworkID)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryProcessContext processContext = new FactoryProcessContext();
                responseMessage = processContext.DeleteFramework(FrameworkID.Trim()).ToList();
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

        /*new add by p.basu*/
        [HttpPost]
        public JsonResult LoadType(string MODE)
        {
            List<TYPES> type = new List<TYPES>();
            FactoryProcessContext processContext = new FactoryProcessContext();
            type = processContext.GetInputOutPutTypes(MODE);
            return Json(type);
        }

        public ActionResult ProductTypeWise(string TYPE)
        {
            ProcessSequenceList loadProcessSequence = new ProcessSequenceList();
            FactoryProcessContext processContext = new FactoryProcessContext();
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            loadProcessSequence = processContext.LoadTypeWise(TYPE, userid);
            var allsequenceDataset = new
            {
                varInputMaterial = loadProcessSequence.MaterialInputSourceList,
                varOutputMaterial = loadProcessSequence.MaterialOutputSourceList,
                varInputUom = loadProcessSequence.InputUomList,
                varOutputUom = loadProcessSequence.OutputUomList,
            };
            return Json(new
            {
                allsequenceDataset,
                JsonRequestBehavior.AllowGet
            });
        }
        public ActionResult ProductIdWise(string PRODUCTID)
        {
            ProcessSequenceList loadProcessSequence = new ProcessSequenceList();
            FactoryProcessContext processContext = new FactoryProcessContext();
            string userid = Convert.ToString(HttpContext.Session["UserID"]).Trim();
            loadProcessSequence = processContext.LoadIdWise(PRODUCTID, userid);
            var allsequenceDataset = new
            {
                varInputMaterial = loadProcessSequence.MaterialInputSourceList,
                varOutputMaterial = loadProcessSequence.MaterialOutputSourceList,
                varInputUom = loadProcessSequence.InputUomList,
                varOutputUom = loadProcessSequence.OutputUomList,
            };
            return Json(new
            {
                allsequenceDataset,
                JsonRequestBehavior.AllowGet
            });
        }



        [HttpPost]
        public JsonResult BomNameCheck(string MODE,string NAME)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                FactoryProcessContext processContext = new FactoryProcessContext();
                responseMessage = processContext.NameCheck(MODE, NAME);
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