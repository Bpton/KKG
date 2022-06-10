using FactoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryDatacontext;
using FactoryModule1.Helpers;
using System.Data;
using System.IO;

namespace FactoryModule1._1.Controllers
{
    public class mmpoController : Controller
    {
        // GET: mmpo
        public ActionResult factorypurchaseorder()
        {
            // onepointlogin();
            //  HttpCookie userInfo = Request.Cookies["userInfo"];
            //if (Convert.ToString(Session["UserID"]) != "" )
            //{
            //    var userid = _ICacheManager.Get<object>("UserID");
            //    string id = userid.ToString();
            //    Binddepot(id);
            //    //Response.Cookies["userInfo"].Expires = DateTime.Now.AddHours(-1);
            //    //  Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(30);
            //    return View();

            //}
            //else
            //{
            //    return Redirect("http://mcnroeerp.com:8085/View/frmAdminLogin.aspx");
            //}
            ////development
            var userid = _ICacheManager.Get<object>("UserID");
            string id = userid.ToString();
            Binddepot(id);
            //Response.Cookies["userInfo"].Expires = DateTime.Now.AddHours(-1);
            //  Response.Cookies["userInfo"].Expires = DateTime.Now.AddMinutes(30);
            return View();



        }

        public ActionResult approvePurchaseOrder()
        {
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
                HttpContext.Session["IUserID"] = userInfo["IUserID"].ToString();
                HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());


            }


        }
        [HttpPost]
        public JsonResult getuserid()
        {
            var userid = _ICacheManager.Get<object>("UserID");
            string USERID = userid.ToString();
            return Json(USERID);
        }
        [HttpPost]
        public JsonResult getfinyear()
        {
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            string FINYEAR = finyear.ToString();
            return Json(FINYEAR);
        }

        public ICacheManager _ICacheManager;
        public mmpoController()
        {
            _ICacheManager = new CacheManager();
        }
        [HttpPost]
        public JsonResult Binddepot(string id)
        {
            var userid = _ICacheManager.Get<object>("UserID");
            MmpoContext mmpo = new MmpoContext();
            id = userid.ToString();
            PurchaseOrder user = mmpo.LoadDepotFromUser(id);
            _ICacheManager.Add("DEPOTID", user.Depotid);
            return Json(user);
        }
        [HttpPost]
        public JsonResult Bindproduct(string Vendorid)
        {
            List<PurchaseOrder> product = new List<PurchaseOrder>();
            MmpoContext mmpo = new MmpoContext();
            product = mmpo.Loadproduct(Vendorid);
            var Depotid = _ICacheManager.Get<object>("DEPOTID");
            return Json(product);
        }

        public ActionResult PackSize_MrpTaxWihtRate(string Productid, string Vendorid, string Podate)
        {
            Pomrprateunit rateunitmrp = new Pomrprateunit();
            MmpoContext nccContext = new MmpoContext();
            rateunitmrp = nccContext.Loadunitmrprate(Productid, Vendorid, Podate);
            var packsizemrpunitDataset = new
            {
                pounit = rateunitmrp.Pounit,
                pomrp = rateunitmrp.Pomrp,
                porate = rateunitmrp.Porate
            };
            return Json(new
            {
                packsizemrpunitDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult Bindmaxminlastrate(string Productid, string Vendorid, string Podate)
        {
            Pomaxminlastrate maxminlast = new Pomaxminlastrate();
            MmpoContext nccContext = new MmpoContext();
            maxminlast = nccContext.LoadmaxminlastRate(Productid, Vendorid, Podate);
            var allrateDataset = new
            {
                polasrate = maxminlast.Polasrate,
                pomaxrate = maxminlast.Pomaxrate,
                pominrate = maxminlast.Pominrate,
                poavgrate = maxminlast.Poavgrate,

            };
            return Json(new
            {
                allrateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public ActionResult Bindvendorcurrencey()
        {
            Povendorcurrencey povendorcurrencey = new Povendorcurrencey();
            MmpoContext nccContext = new MmpoContext();
            povendorcurrencey = nccContext.Loadvendorcurrencey();
            var vendorcurrenceyDataset = new
            {
                povendor = povendorcurrencey.Povendors,
                pocurrecncy = povendorcurrencey.Pocurrnceys,
            };
            return Json(new
            {
                vendorcurrenceyDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        [HttpPost]
        public JsonResult Bindtax(string Vendorid, string productid)
        {
            string cgstid, sgstid, igstid;
            decimal cgstper, sgstper, igstper;
            List<PurchaseOrder> tax = new List<PurchaseOrder>();
            List<Potax> taxpercentage = new List<Potax>();
            var Depotid = _ICacheManager.Get<object>("DEPOTID");
            var depotid = Depotid.ToString();
            MmpoContext mmpo = new MmpoContext();
            tax = mmpo.Loadtax(Vendorid, productid, depotid);

            var taxper = tax.Where(r => r.TAXID == "8C60D11D-9524-4DC4-AA9B-AF956C52E41F").ToList().FirstOrDefault();
            var taxpercgst = tax.Where(r => r.TAXID == "DC552F34-827B-4FA7-95C2-20A2B6211B11").ToList().FirstOrDefault();
            var taxpersgst = tax.Where(r => r.TAXID == "F660840C-00DE-4E6F-B1CE-16F53063A3A1").ToList().FirstOrDefault();

            if (taxpercgst == null)
            {
                cgstid = "DC552F34-827B-4FA7-95C2-20A2B6211B11";
                cgstper = 0;
            }
            else
            {
                cgstid = taxpercgst.TAXID;
                cgstper = taxpercgst.PERCENTAGE;
            }
            if (taxpersgst == null)
            {
                sgstid = "F660840C-00DE-4E6F-B1CE-16F53063A3A1";
                sgstper = 0;
            }
            else
            {
                sgstid = taxpersgst.TAXID;
                sgstper = taxpersgst.PERCENTAGE;
            }
            if (taxper == null)
            {
                igstid = "8C60D11D-9524-4DC4-AA9B-AF956C52E41F";
                igstper = 0;
            }
            else
            {
                igstid = taxper.TAXID;
                igstper = taxper.PERCENTAGE;
            }

            taxpercentage.Add(new Potax()
            {
                CGSTTAXID = cgstid,
                CGSTPERCENTAGE = cgstper,
                SGSTTAXID = sgstid,
                SGSTPERCENTAGE = sgstper,
                IGSTTAXID = igstid,
                IGSTPERCENTAGE = igstper,
            });

            return Json(taxpercentage);
        }

        [HttpPost]
        public JsonResult producttpumapcheck(string Productid, string Vendorid)
        {
            List<PurchaseOrder> check = new List<PurchaseOrder>();
            MmpoContext mmpo = new MmpoContext();
            var Depotid = _ICacheManager.Get<object>("DEPOTID");
            Vendorid = Depotid.ToString();
            check = mmpo.chckproduct(Productid, Vendorid);
            return Json(check);
        }
        [HttpPost]
        public JsonResult BindTermsCondition()
        {
            List<PurchaseOrder> Terms = new List<PurchaseOrder>();
            MmpoContext mmpo = new MmpoContext();
            Terms = mmpo.BindTerms();
            return Json(Terms);
        }
        [HttpPost]
        public JsonResult posavedata(PurchaseOrder posave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                var userid = _ICacheManager.Get<object>("UserID");
                var Finyear = _ICacheManager.Get<object>("FINYEAR");
                var factoryid = _ICacheManager.Get<object>("DEPOTID");
                var entryfrom = "M";
                posave.Mode = "A";
                posave.Createdby = userid.ToString();
                posave.FINYEAR = Finyear.ToString();
                posave.Depotid = factoryid.ToString();
                posave.ENTRYFROM = entryfrom.ToString();
                //posave.SMSText = "";
                // nccsave.Remarks = "";

                MmpoContext mmpoContext = new MmpoContext();
                responseMessage = mmpoContext.PoInsertUpdate(posave).ToList();

                // }
                foreach (var msg in responseMessage)
                {
                    messageid1 = msg.MessageID;
                    messagetext1 = msg.MessageText;
                    //ViewBag.messageid = msg.MessageID;
                    //ViewBag.messagetext = msg.MessageText;
                    TempData["messageid1"] = msg.MessageID;
                    TempData["messagetext1"] = msg.MessageText;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Quotationupload_file(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<PurchaseOrder> responseMessage = new List<PurchaseOrder>();
                    HttpFileCollectionBase files = Request.Files;
                    string poid = formCollection["POID"];
                    if (poid == "")
                    {
                        return Json("FirstFirst Save Po then upload file");
                    }
                    else
                    {
                        string mode = "1";
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
                            MmpoContext mmpoContext = new MmpoContext();
                            responseMessage = mmpoContext.Quotationupload_fileSaveUpdate(poid, filename, fname, mode).ToList();
                        }

                        return Json("File Uploaded Successfully!");
                    }

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


        [HttpPost]
        public ActionResult Comparative_file(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<PurchaseOrder> responseMessage = new List<PurchaseOrder>();
                    HttpFileCollectionBase files = Request.Files;
                    string poid = formCollection["POID"];
                    if (poid == "")
                    {
                        return Json("FirstFirst Save Po then upload file");
                    }
                    else
                    {
                        string mode = "2";
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
                            MmpoContext mmpoContext = new MmpoContext();
                            responseMessage = mmpoContext.Quotationupload_fileSaveUpdate(poid, filename, fname, mode).ToList();
                        }

                        return Json("File Uploaded Successfully!");
                    }

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



        [HttpPost]
        public JsonResult BindPoGrid(string FromDate, string ToDate, string QSTAG, string checker, string Potype) /*make complain grid*/
        {
            var userid = _ICacheManager.Get<object>("UserID");
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            var Factoryid = _ICacheManager.Get<object>("DEPOTID");
            var factoryid = Factoryid.ToString();
            List<PodetailsGrid> podetails = new List<PodetailsGrid>();
            MmpoContext mmpoContext = new MmpoContext();
            podetails = mmpoContext.BindComplainGrid(FromDate, ToDate, Finyear, QSTAG, checker, factoryid, Potype, userid.ToString());
            return Json(podetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPurchaseOrder(string Poid)
        {
            PodetailsEdit podetailsall = new PodetailsEdit();
            MmpoContext mmpoContext = new MmpoContext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            podetailsall = mmpoContext.Editpurchaseorderdetails(Poid, Finyear);
            var podetailsDataset = new
            {
                podetails = podetailsall.Podetails,
                editpos = podetailsall.EditPos,

            };
            return Json(new
            {
                podetailsDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        public JsonResult ApprovedPo(string poid, string mode, string rejrectionnote)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            MmpoContext mmpoContext = new MmpoContext();
            var userid = _ICacheManager.Get<object>("UserID");
            purchaseOrder = mmpoContext.ApprovePurchaseOrder(poid, userid.ToString(), mode, rejrectionnote);
            return Json(purchaseOrder);
        }

        public JsonResult DeletePo(string poid)
        {

            List<MessageModel> responseMessage = new List<MessageModel>();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            MmpoContext mmpoContext = new MmpoContext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            purchaseOrder = mmpoContext.DeletePurchaseOrder(poid, Finyear);
            return Json(purchaseOrder);
        }
        [HttpPost]
        public JsonResult ConvertionQty(string Productid, string unitid, string qty, string purchaserate, string mrp)
        {
            List<PurchaseOrder> convertionqty = new List<PurchaseOrder>();
            MmpoContext mmpo = new MmpoContext();
            var mode = "1";
            convertionqty = mmpo.CONVERTIONQTY(Productid, unitid, qty, purchaserate, mrp, mode);
            return Json(convertionqty);
        }
        [HttpPost]
        public JsonResult ConvertionQtymrp(string Productid, string unitid, string qty, string purchaserate, string mrp)
        {
            List<PurchaseOrder> convertionqtymrp = new List<PurchaseOrder>();
            MmpoContext mmpo = new MmpoContext();
            var mode = "2";
            convertionqtymrp = mmpo.CONVERTIONQTYmrp(Productid, unitid, qty, purchaserate, mrp, mode);
            return Json(convertionqtymrp);
        }

        [HttpPost]
        public JsonResult FetchPoDocumentComparative(string POID, string mode) /*make complain grid*/
        {
            List<PurchaseOrder> uploadgrid = new List<PurchaseOrder>();
            MmpoContext mmpoContext = new MmpoContext();
            uploadgrid = mmpoContext.FetchinfoComparative(POID, mode);
            return Json(uploadgrid, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteUploadfile(string uploadid)
        {
            List<PurchaseOrder> deletecomp = new List<PurchaseOrder>();
            MmpoContext mmpoContext = new MmpoContext();
            deletecomp = mmpoContext.DeleteUploadfile(uploadid.ToString());
            return Json(deletecomp, JsonRequestBehavior.AllowGet);
        }

        /*GRN MM*/

       

        public ActionResult factorygrnMM()
        {
            var userid = _ICacheManager.Get<object>("UserID");
            string id = userid.ToString();
            Binddepot(id);
            return View();
        }

        public JsonResult LoadTpu(string FG)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            var DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            var id = DEPOTID.ToString();
            grnMMs = mmpo.BindTPU(FG, id);
            return Json(grnMMs);
        }

        public JsonResult LoadTpu_Transporter(string Vendorid)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            grnMMs = mmpo.BindTpu_Transporter(Vendorid);
            return Json(grnMMs);
        }
        public JsonResult PoCloseAuto(string Vendorid)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            var DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            var id = DEPOTID.ToString();
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var FINYEAR = Finyear.ToString();
            grnMMs = mmpo.BindPoCloseAuto(Vendorid, FINYEAR, id);
            return Json(grnMMs);
        }
        public JsonResult LoadPoMM(string Vendorid)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            var DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            var id = DEPOTID.ToString();
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var FINYEAR = Finyear.ToString();
            grnMMs = mmpo.BindPoMM(Vendorid,id, FINYEAR);
            return Json(grnMMs);
        }
        public JsonResult LoadProductDetails(string poid)
        {
            List<GrnMM> grnMMs = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            grnMMs = mmpo.BindProductDetails(poid, "","");
            return Json(grnMMs);
        }
        public JsonResult Loadproductdtl(string poid, string productid, string unitid, string FG, string invoicedt, string tpu)
        {
            List<ProductInfo> grnMMs = new List<ProductInfo>();
            MmpoContext mmpo = new MmpoContext();
            var DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            var id = DEPOTID.ToString();
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var FINYEAR = Finyear.ToString();
            grnMMs = mmpo.Bindproductdtl(poid, productid, unitid, id, FG,"", invoicedt, tpu);
            return Json(grnMMs);
        }
        [HttpPost]
        public JsonResult GetTaxcount(string MenuID, string Flag, string ProductID, string CustomerID, string Date)
        {
            List<TaxcountListMM> taxcount = new List<TaxcountListMM>();
            MmpoContext dispatchContext = new MmpoContext();
            var _DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            var DepotID = _DEPOTID.ToString();
            taxcount = dispatchContext.GetTaxcount(MenuID.Trim(), Flag.Trim(), DepotID.Trim().Trim(), ProductID.Trim(), CustomerID.Trim(), Date.Trim());
            return Json(taxcount, JsonRequestBehavior.AllowGet);
        }
        public ActionResult addproductdtl1(string depoid, string poid, string pono, string podate, string hsncode, string productid, string productname, string packsizeid, string packsizename, decimal mrp, decimal despatchqty, decimal qcqty, decimal rate, decimal itemwisefreight, decimal itemwiseaddcost, string batchno, string FG, string invoicedate, string tpu, string mfgdt, string expdt, decimal alreadydespatchqty, decimal assessmentpercent,string taxnamecgst, string taxnamesgst, string taxnameigst)
        {
            CalcualteTaxWithAmount calculateamtpcs = new CalcualteTaxWithAmount();
            MmpoContext mmpo = new MmpoContext();
            var DEPOTID = _ICacheManager.Get<object>("DEPOTID");
            depoid = DEPOTID.ToString();
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var FINYEAR = Finyear.ToString();
            calculateamtpcs = mmpo.GetCalculateAmtInPcs(depoid, poid, pono, podate, hsncode, productid, productname, packsizeid, packsizename, mrp, despatchqty, qcqty, rate, itemwisefreight, itemwiseaddcost, batchno, FG, invoicedate, tpu, mfgdt, expdt, alreadydespatchqty, assessmentpercent, FINYEAR, taxnamecgst, taxnamesgst, taxnameigst);
            var allcalculateDataset = new
            {
                varproductInfoBatches = calculateamtpcs.productInfoBatches,
                varmasterbatch = calculateamtpcs.masterbatch,
                varproduct_returnamount = calculateamtpcs.product_returnamount,
                varproduct_amount = calculateamtpcs.product_amount,
                varproduct_discount = calculateamtpcs.product_discount,
                varproduct_netgrossweight = calculateamtpcs.product_netgrossweight,
                varproduct_grossweight = calculateamtpcs.product_grossweight,
                varCgstTax = calculateamtpcs.cgstpercentagemm,
                varSgstTax = calculateamtpcs.sgstpercentagemm,
                varIgstTax = calculateamtpcs.igstpercentagemm,
                varSgstID = calculateamtpcs.sgstmm,
                varCgstID = calculateamtpcs.cgstmm,
                varIgstID = calculateamtpcs.igstmm,
            };
            return Json(new
            {
                allcalculateDataset,
                JsonRequestBehavior.AllowGet
            });

        }

        public DataTable CreateDataTableTaxComponent()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(decimal)));
            HttpContext.Session["TAXDETAILS_GRN_MM"] = dt;
            return dt;
        }

        public DataTable CraeatetableInTaxRejection()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
            HttpContext.Session["REJTAXDETAILS_GRN_MM"] = dt;
            return dt;

           
        }

        public DataTable CraeatetableInRejection()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("STOCKDESPATCHID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));
            dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
            dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
            dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("DEPOTRATE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
            dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
            HttpContext.Session["REJECTIONDETAILS__MM"] = dt;
            return dt;

        }
        public DataTable ReturnTaxTable()
        {
            DataTable dtTax = new DataTable();
            dtTax = (DataTable)Session["TAXDETAILS_GRN_MM"];
            return dtTax;
        }
        public DataTable ReturnRejTaxTable()
        {
            DataTable dtRejTax = new DataTable();
            dtRejTax = (DataTable)Session["REJTAXDETAILS_GRN_MM"];
            return dtRejTax;
        }
        public DataTable ReturnRejectionTable()
        {
            DataTable dtRejc = new DataTable();
            dtRejc = (DataTable)Session["REJECTIONDETAILS__MM"];
            return dtRejc;
        }

        [HttpPost]
        public EmptyResult FillTaxDatatable(string Productid, string BatchNo, string TaxID, decimal Percentage, decimal TaxValue, decimal MRP, string Flag,string POID)
        {
            if (Session["TAXDETAILS_GRN_MM"] == null)
            {
                CreateDataTableTaxComponent();
            }

            DataTable dt = (DataTable)HttpContext.Session["TAXDETAILS_GRN_MM"];
            DataRow dr = dt.NewRow();
            if (Flag == "1")
            {
                dr["PRODUCTID"] = Productid;
                dr["BATCHNO"] = BatchNo;
                dr["TAXID"] = TaxID;
                dr["TAXPERCENTAGE"] = Percentage;
                dr["TAXVALUE"] = TaxValue;
                dr["MRP"] = MRP;
                dr["POID"] = POID;
            }
            else
            {
                dr["PRODUCTID"] = "NA";
                dr["BATCHNO"] = "NA";
                dr["TAXID"] = "NA";
                dr["TAXPERCENTAGE"] = 0;
                dr["TAXVALUE"] = 0;
                dr["MRP"] = 0;
                dr["POID"] = "NA";
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["TAXDETAILS_GRN_MM"] = dt;
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult FillRejectionTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string PRODUCTNAME, string Taxid)
        {
            if (Session["REJTAXDETAILS_GRN_MM"] == null)
            {
                CraeatetableInTaxRejection();
            }

            DataTable dt = (DataTable)HttpContext.Session["REJTAXDETAILS_GRN_MM"];
            DataRow dr = dt.NewRow();
            
                dr["POID"] = POID;
                dr["PRODUCTID"] = PRODUCTID;
                dr["BATCHNO"] = BATCH;
                dr["TAXID"] = Taxid;
                dr["PERCENTAGE"] = TAXPERCENTAGE;
                dr["TAXVALUE"] = VALUES;
                dr["PRODUCTNAME"] = PRODUCTNAME;
                dr["TAXNAME"] = NAME;
            
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["REJTAXDETAILS_GRN_MM"] = dt;
            return new EmptyResult();
        }

        public EmptyResult RemoveSessionGrnMM()
        {
            Session.Remove("TAXDETAILS_GRN_MM");
            Session.Remove("REJTAXDETAILS_GRN_MM");
            Session.Remove("REJECTIONDETAILS__MM");
            Session.Remove("UPLOADCAPACITYFILE");
            Session.Remove("STOCKRECEIVED_SAMPLEQTY");
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult DeleteTaxDatatable(string Productid, string BatchNo)
        {
            DataTable DtTax = (DataTable)HttpContext.Session["TAXDETAILS_GRN_MM"];
            DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Productid + "' AND BATCHNO = '" + BatchNo + "'");
            for (int t = 0; t < drrTax.Length; t++)
            {
                drrTax[t].Delete();
                DtTax.AcceptChanges();
            }
            HttpContext.Session["TAXDETAILS_GRN_MM"] = DtTax;
            return new EmptyResult();
        }



        public JsonResult grnsavedata(GrnMM grnsave)
        {
            string messageid1 = "";
            List<MessageModel> responseMessage = new List<MessageModel>();
            string messagetext1 = "";
            try
            {
                
                if (Session["REJECTIONDETAILS__MM"] == null)
                {
                    CraeatetableInRejection();
                }
                if (Session["REJTAXDETAILS_GRN_MM"] == null)
                {
                    CraeatetableInTaxRejection();
                }
                if (Session["STOCKRECEIVED_SAMPLEQTY"] == null)
                {
                    CreateDataTable_SampleQty();
                }
                if (Session["UPLOADCAPACITYFILE"] == null)
                {
                    CreateDataTable_UploadCapacity();
                }
                DataTable dtTax = ReturnTaxTable();
                DataTable dtRej = ReturnRejectionTable();
                DataTable dtRejTax = (DataTable)HttpContext.Session["REJTAXDETAILS_GRN_MM"];
                DataTable dtSample = (DataTable)HttpContext.Session["STOCKRECEIVED_SAMPLEQTY"];
                DataTable dtSampleQty = (DataTable)(Session["UPLOADCAPACITYFILE"]);


                var userid = _ICacheManager.Get<object>("UserID");
                var Finyear = _ICacheManager.Get<object>("FINYEAR");
                var factoryid = _ICacheManager.Get<object>("DEPOTID");
                var entryfrom = "M";
                grnsave.Mode = "A";
                grnsave.Createdby = userid.ToString();
                grnsave.FINYEAR = Finyear.ToString();
                grnsave.DEPOTID = factoryid.ToString();
                grnsave.ENTRYFROM = entryfrom.ToString();

                MmpoContext dispatchContext = new MmpoContext();
                responseMessage = dispatchContext.GrnMMInsertUpdate(grnsave,
                                                                        dtTax, dtRej, dtRejTax, dtSample, dtSampleQty).ToList();


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
        public JsonResult BindgrnGrid(string fromdate, string todate, string CheckerFlag,string TPUFLAG, string OP) /*make grn grid*/
        {
            var userid = _ICacheManager.Get<object>("UserID");
            var UID= userid.ToString();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            var depotid = _ICacheManager.Get<object>("DEPOTID");
            var Depotid = depotid.ToString();
            List<LoadGrn> grndetails = new List<LoadGrn>(); 
             MmpoContext mmpoContext = new MmpoContext();
            grndetails = mmpoContext.BindReceived(fromdate, todate, Depotid, Finyear, CheckerFlag, UID, TPUFLAG, OP);
            return Json(grndetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditReceivedDetails(string grnid,string mode)
        {
            GrnEditMM grnEditMM = new GrnEditMM();
            MmpoContext dispatchContext = new MmpoContext();
            grnEditMM = dispatchContext.EditReceivedDetails(grnid,mode);
            var alleditDataset = new
            {
                varSTOCKRECEIVEDHEADERs = grnEditMM.gRN_EDIT_STOCKRECEIVEDHEADERs,
                varSTOCKRECEIVEDDETAILs = grnEditMM.gRN_EDIT_STOCKRECEIVEDDETAILs,
                varTAXCOMPONENTCOUNTs = grnEditMM.gRN_EDIT_TAXCOMPONENTCOUNTs,
                varSTOCKRECEIVEDFOOTERs = grnEditMM.gRN_EDIT_STOCKRECEIVEDFOOTERs,
                varSTOCKRECEIVEDTAXes = grnEditMM.gRN_EDIT_STOCKRECEIVEDTAXes,
                varSTOCKRECEIVEDTERMs =grnEditMM.gRN_EDIT_STOCKRECEIVEDTERMs,
                varSTOCKRECEIVEDITEMWISETAXes = grnEditMM.gRN_EDIT_STOCKRECEIVEDITEMWISETAXes,
                varSTOCKRECEIVEDREJECTIONDETAILs =grnEditMM.gRN_EDIT_STOCKRECEIVEDREJECTIONDETAILs,
                varGRNADDITIONALDETAILs=grnEditMM.gRN_EDIT_GRNADDITIONALDETAILs,
                varSTOCKRECEIVEDREJECTIONTAXes=grnEditMM.gRN_EDIT_STOCKRECEIVEDREJECTIONTAXes,
                varJOBORDERRECEIVEDDETAILs=grnEditMM.gRN_EDIT_JOBORDERRECEIVEDDETAILs,
                varSTOCKRECEIVEDSAMPLEQTies=grnEditMM.gRN_EDIT_STOCKRECEIVEDSAMPLEQTies,
                varSTOCKRECEIVEDSAMPLEQTYNAMEs=grnEditMM.gRN_EDIT_STOCKRECEIVEDSAMPLEQTYNAMEs,

            };
            return Json(new
            {
                alleditDataset,
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public JsonResult GetGrnTaxOnEdit(string grnid, string TaxID, string ProductID, string BatchNo)
        {
            List<TaxOnEdit> grntaxonedit = new List<TaxOnEdit>();
            MmpoContext dispatchContext = new MmpoContext();
            grntaxonedit = dispatchContext.GetGrnTaxOnEdit(grnid.Trim(), TaxID.Trim(), ProductID.Trim(), BatchNo.Trim());
            return Json(grntaxonedit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckInvoiceNo(string INVOICENO, string VENDORID)
        {
            List<GrnMM> INVOICE = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            INVOICE = mmpo.CheckInvoiceNo(INVOICENO, VENDORID, Finyear);
            return Json(INVOICE);
        }

        public JsonResult DeleteStockDespatch(string stockreceivedid)
        {

            GrnMM grnMM = new GrnMM();
            MmpoContext mmpoContext = new MmpoContext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            grnMM = mmpoContext.DeleteStockDespatch(stockreceivedid);
            return Json(grnMM);
        }

        [HttpPost]
        public JsonResult GetFile_SR_Capacity(string grnid,string mode) /*make complain grid*/
        {
          
            List<GrnQc> grnMMs = new List<GrnQc>();
            MmpoContext mmpoContext = new MmpoContext();
            grnMMs = mmpoContext.GetFile_SR_Capacity(grnid, mode);
            return Json(grnMMs, JsonRequestBehavior.AllowGet);
        }

        public DataTable CreateDataTable_SampleQty()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SAMPLEQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("OBSERVATIONQTY", typeof(decimal)));
            
            HttpContext.Session["STOCKRECEIVED_SAMPLEQTY"] = dt;
            return dt;


        }

        public DataTable CreateDataTable_UploadCapacity()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("FILENAME", typeof(string)));
            HttpContext.Session["UPLOADCAPACITYFILE"] = dt;
            return dt;
        }

        [HttpPost]
        public EmptyResult FillSampleQtyDatatable(string STOCKRECEIVEDID, string POID, string PRODUCTID, string PRODUCTNAME, decimal RECEIVEDQTY, decimal SAMPLEQTY, decimal OBSERVATIONQTY)
        {
            if (Session["STOCKRECEIVED_SAMPLEQTY"] == null)
            {
                CreateDataTable_SampleQty();
            }

            DataTable dt = (DataTable)HttpContext.Session["STOCKRECEIVED_SAMPLEQTY"];
            DataRow dr = dt.NewRow();
           
                dr["STOCKRECEIVEDID"] = STOCKRECEIVEDID;
                dr["POID"] = POID;
                dr["PRODUCTID"] = PRODUCTID;
                dr["PRODUCTNAME"] = PRODUCTNAME;
                dr["RECEIVEDQTY"] = RECEIVEDQTY;
                dr["SAMPLEQTY"] = SAMPLEQTY;
                dr["OBSERVATIONQTY"] = OBSERVATIONQTY;
           
            
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["STOCKRECEIVED_SAMPLEQTY"] = dt;
            return new EmptyResult();

        }

        [HttpPost]
        public EmptyResult FillSampleQtyDatatablefilename(string FILENAME)
        {
           
            if (Session["UPLOADCAPACITYFILE"] == null)
            {
                CreateDataTable_UploadCapacity();
            }

            DataTable dt = (DataTable)HttpContext.Session["UPLOADCAPACITYFILE"];
            DataRow dr = dt.NewRow();

            dr["FILENAME"] = FILENAME;

            dt.Rows.Add(dr);
            dt.AcceptChanges();
            Session["UPLOADCAPACITYFILE"] = dt;
            return new EmptyResult();
        }



        [HttpPost]
        public JsonResult Chkupload_file(FormCollection formCollection)
        {
            List<GrnQc> fileupload = new List<GrnQc>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<GrnQc> responseMessage = new List<GrnQc>();
                    HttpFileCollectionBase files = Request.Files;
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();


                    dt.Columns.Add("FILENAME");

                    DataRow _uploadfile = dt.NewRow();
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
                        if (Session["UPLOADCAPACITYFILE"] != null)
                        {
                            dt1 = (DataTable)Session["UPLOADCAPACITYFILE"];
                        }
                        _uploadfile["FILENAME"] = fname;

                        dt.Rows.Add(_uploadfile);
                        dt1.Merge(dt);
                        Session["UPLOADCAPACITYFILE"] = dt1;
                        fname = Path.Combine(Server.MapPath("UploadFiles/"), fname);
                        file.SaveAs(fname);
                    }
                    foreach (DataRow row in dt1.Rows)

                        fileupload.Add(new GrnQc
                        {
                            FILENAME = row["FILENAME"].ToString(),
                            FILEPATH = " ~/UploadFiles/" + row["FILENAME"].ToString(),

                        });

                    return Json(fileupload);


                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(fileupload);
            }

        }

        [HttpPost]
        public JsonResult deletefile(string filename)
        {

            if (HttpContext.Session["UPLOADCAPACITYFILE"] != null)
            {
                DataTable dtcost = (DataTable)HttpContext.Session["UPLOADCAPACITYFILE"];
                DataRow[] rowscost;
                rowscost = dtcost.Select("FILENAME = '" + filename + "'"); 
                foreach (DataRow r in rowscost)
                    r.Delete();
                dtcost.AcceptChanges();
                HttpContext.Session["UPLOADCAPACITYFILE"] = dtcost;

            }

            return Json("File deleted Successfully!");
        }

        /*po approve*/
        [HttpPost]
        public JsonResult getFactory()
        {
            List<GrnMM> factory = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            factory = mmpo.onlyBindNewFactory();
            return Json(factory);
        }
        public JsonResult bindPendingPo(string FromDate,string ToDate,string Depotid,string FinYear)
        {
            List<GrnMM> factory = new List<GrnMM>();
            MmpoContext mmpo = new MmpoContext();
            factory = mmpo.getPendingPo(FromDate, ToDate, Depotid, FinYear);
            return Json(factory);
        }

        public JsonResult updatePo(string POID,string USERID)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            try {
                MmpoContext dispatchContext = new MmpoContext();
                responseMessage = dispatchContext.updateApprove(POID, USERID).ToList();
                foreach (var msg in responseMessage)
                {
                    TempData["messageid"] = msg.response;

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