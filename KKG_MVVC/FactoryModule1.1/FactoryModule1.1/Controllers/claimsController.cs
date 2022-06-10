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
using System.Web.Mvc;

namespace FactoryModule1._1.Controllers
{
    [SessionTimeout]
    public class claimsController : Controller
    {
        public ICacheManager _ICacheManager;
        public claimsController()
        {
            _ICacheManager = new CacheManager();
        }
        public EmptyResult RemoveSession()
        {

            Session["PRODUCTCLAIMQPS"] = "";
            Session["PRODUCTCLAIMQPS"] = null;


            return new EmptyResult();

        }
        //fin yr check
        [HttpPost]
        public JsonResult finyrchk()
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            var finyr = _ICacheManager.Get<object>("FINYEAR");
            string _finyr = finyr.ToString();
            string startyear = _finyr.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = _finyr.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime datevalidation = DateTime.Today.Date;
            if (datevalidation >= new DateTime(startyear1, 04, 01) && datevalidation <= new DateTime(endyear1, 03, 31))
            {
                currentdt = datevalidation.ToString("yyyy/MM/dd");
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
            }
            else
            {
                currentdt = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");

            }
            Finyearrange.Add(new Finyearrange
            {
                currentdt = currentdt,
                frmdate = frmdate,
                todate = todate,



            });

            return Json(Finyearrange);
        }
        // GET: claims
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult qpsvps()
        {
            return View();
        }
        [HttpPost]
        public JsonResult BindDepotByUserID()
        {
            var userid = _ICacheManager.Get<object>("UserID");
            List<Depomodel> Depomodel = new List<Depomodel>();
            Claimscontext Claimscontext = new Claimscontext();
            Depomodel = Claimscontext.BindDepotByUserID(userid.ToString());
            return Json(Depomodel);
        }
        [HttpPost]
        public JsonResult BindBusinessSegment()
        {

            List<Businesssegment> Businesssegment = new List<Businesssegment>();
            Claimscontext Claimscontext = new Claimscontext();
            Businesssegment = Claimscontext.BindBusinessSegment();
            return Json(Businesssegment);
        }

        [HttpPost]
        public JsonResult BindBusinessSegmentByUserID()
        {
            var userid = _ICacheManager.Get<object>("UserID");
            List<Businesssegment> Businesssegment = new List<Businesssegment>();
            Claimscontext Claimscontext = new Claimscontext();
            Businesssegment = Claimscontext.BindBusinessSegmentByUserID(userid.ToString());
            return Json(Businesssegment);
        }
        public JsonResult BindPrincipleGroup()
        {

            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadPrincipleGroup = Claimscontext.BindPrincipleGroup();
            return Json(LoadPrincipleGroup);
        }
        [HttpPost]
        public JsonResult BindDropdownPrincipleGroup(string ID)
        {

            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadPrincipleGroup = Claimscontext.BindDropdownPrincipleGroup(ID);
            return Json(LoadPrincipleGroup);
        }
        [HttpPost]
        public JsonResult BindClaimNarration(string CLAIM_TYPE, string BSID, string DEPOTID)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var USERTAG = _ICacheManager.Get<object>("USERTAG");
            List<BindClaimNarration> BindClaimNarration = new List<BindClaimNarration>();
            Claimscontext Claimscontext = new Claimscontext();
            BindClaimNarration = Claimscontext.BindClaimNarration(CLAIM_TYPE, USERTAG.ToString(), BSID, DEPOTID, Finyear.ToString());
            return Json(BindClaimNarration);
        }
        [HttpPost]
        public JsonResult BindGroupByDistributor(string DistributorID, string BSID)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var USERTAG = _ICacheManager.Get<object>("USERTAG");
            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadPrincipleGroup = Claimscontext.BindGroupByDistributor(DistributorID, BSID);
            return Json(LoadPrincipleGroup);
        }
        [HttpPost]
        public JsonResult BindDistributorByDepot(string DistributorID, string BSID)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var USERTAG = _ICacheManager.Get<object>("USERTAG");
            List<LoadDistributors> LoadDistributors = new List<LoadDistributors>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadDistributors = Claimscontext.BindDistributorByDepot(DistributorID, BSID);
            return Json(LoadDistributors);
        }

        [HttpPost]
        public JsonResult BindDistributorClaim(string DistributorID)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var USERTAG = _ICacheManager.Get<object>("USERTAG");
            List<LoadDistributors> LoadDistributors = new List<LoadDistributors>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadDistributors = Claimscontext.BindDistributorClaim(DistributorID);
            return Json(LoadDistributors);
        }
        [HttpPost]
        public JsonResult BindClaimStatus()
        {
            
            List<claimstatus> claimstatus = new List<claimstatus>();
            Claimscontext Claimscontext = new Claimscontext();
            claimstatus = Claimscontext.BindClaimStatus();
            return Json(claimstatus);
        }
        [HttpPost]
        public JsonResult BindRetailer(string DistributorID)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var USERTAG = _ICacheManager.Get<object>("USERTAG");
            List<LoadRetailers> LoadRetailers = new List<LoadRetailers>();
            Claimscontext Claimscontext = new Claimscontext();
            LoadRetailers = Claimscontext.BindRetailer(DistributorID);
            return Json(LoadRetailers);
        }
        [HttpPost]
        public JsonResult BindClaimPeriod(string narrationid)
        {

            List<BindClaimNarration> BindClaimNarration = new List<BindClaimNarration>();
            Claimscontext Claimscontext = new Claimscontext();
            BindClaimNarration = Claimscontext.BindClaimPeriod(narrationid);
            return Json(BindClaimNarration);
        }
        [HttpPost]
        public JsonResult addgrid(string INVOICENO, string INVOICEDT,
            string FROMDT,
            string TODT,
            string QTY,
            string QTYDTL,
            string NARRATIONID,
            string NARRATION)
            {
            if (HttpContext.Session["PRODUCTCLAIMQPS"] == null)
            {
                CreateDataTable();
            }
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = (DataTable)HttpContext.Session["PRODUCTCLAIMQPS"];
            DataRow dr = dtproductclaim.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["CLAIM_PRODUCT_ID"] = NARRATIONID;
            dr["CLAIM_PRODUCT_NAME"] =NARRATION;
            dr["CLAIM_CAT_ID"] = "0";
            dr["CLAIM_CAT_NAME"] = "";
            dr["CLAIM_PACKSIZE_ID"] = "0";
            dr["CLAIM_PACKSIZE_NAME"] = "";
            dr["INVOICE_NO"] =INVOICENO;
            dr["INVOICE_DATE"] = INVOICEDT;
            dr["FROM_DATE"] = FROMDT;
            dr["TO_DATE"] = TODT;
            dr["QTY"] = QTY;
            dr["QTYDETAILS"] = QTYDTL;
            dr["NARRATION"] = NARRATION;
            dtproductclaim.Rows.Add(dr);
            dtproductclaim.AcceptChanges();
            Session["PRODUCTCLAIMDISPLAY"] = dtproductclaim;

          
                foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    INVOICE_NO = row["INVOICE_NO"].ToString(),
                    INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),

                    TO_DATE = row["TO_DATE"].ToString(),
                    QTY = row["QTY"].ToString(),
                    QTYDETAILS = row["QTYDETAILS"].ToString(),
                });
            return Json(qpsclaimdtl);
        }
        [HttpPost]
        public JsonResult delgrid(string GUID)
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIMDISPLAY"];

            DataRow[] drr = dtproductclaim.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtproductclaim.AcceptChanges();
            }
            Session["PRODUCTCLAIMDISPLAY"] = dtproductclaim;

            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    INVOICE_NO = row["INVOICE_NO"].ToString(),
                    INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    FROM_DATE = row["NARRATION"].ToString(),

                    TO_DATE = row["TO_DATE"].ToString(),
                    QTY = row["QTY"].ToString(),
                    QTYDETAILS = row["QTYDETAILS"].ToString(),
                });
            return Json(qpsclaimdtl);
        }
        
        public JsonResult Saveqps(qpsmodel tran)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            tran.UserID = userid.ToString();
            tran.FinYear = Finyear.ToString();
            if (Session["PRODUCTCLAIMDISPLAY"] == null)
            {
                this.CreateDataTable();
            }
            DataTable dttransporter = (DataTable)HttpContext.Session["PRODUCTCLAIMDISPLAY"];
            string xml = null;
            string xmlcostcenter = "";
            string xmlDebitCreditGST = null;
            xml = ConvertDatatableToXML(dttransporter);
            Claimscontext Claimscontext = new Claimscontext();
            responseMessage = Claimscontext.Saveqps(tran, xml).ToList();
            //ClsVoucherentry clsvoucher = new ClsVoucherentry();

            //  string voucherno = clsvoucher.SaveTransporterbillV2(tran,xml);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.TableName = "XMLData";
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }

        public DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PRODUCT_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PRODUCT_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PACKSIZE_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PACKSIZE_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_CAT_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_CAT_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("FROM_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("TO_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("INVOICE_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));
            dt.Columns.Add(new DataColumn("QTYDETAILS", typeof(string)));
            HttpContext.Session["PRODUCTCLAIMQPS"] = dt;
            return dt;
        }
        #region ===display & save
        [HttpPost]
        public JsonResult BindDamageClaim_Filtering(string claimtype, string depotid, string status, string fromdate, string todate, string party)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var userid = _ICacheManager.Get<object>("UserID");
            List<qpshdr> qpshdr = new List<qpshdr>();
            Claimscontext Claimscontext = new Claimscontext();
            qpshdr = Claimscontext.BindDamageClaim_Filtering(claimtype, userid.ToString(), depotid,status,fromdate,todate,party);
            return Json(qpshdr);
        }
        [HttpPost]
        public JsonResult BindQVPSClaim(string claimtype)
        {
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var userid = _ICacheManager.Get<object>("UserID");
            List<qpshdr> qpshdr = new List<qpshdr>();
            Claimscontext Claimscontext = new Claimscontext();
            qpshdr = Claimscontext.BindQVPSClaim(claimtype, userid.ToString(), Finyear.ToString());
            return Json(qpshdr);
        }
        [HttpPost]
        public JsonResult Editqps(string ClaimID)
        {
            List<qpsmodel> qpsmodel = new List<qpsmodel>();
            Hashtable hashTable = new Hashtable();
            try
            {
                hashTable.Add("C_CLAIMID", ClaimID);
                DButility dbcon = new DButility();
                DataSet ds = new DataSet();
                ds = dbcon.SysFetchDataInDataSet("[USP_QVPS_CLAIM_DETAILS]", hashTable);
                DataTable dtqpsbill = new DataTable();
                CreateDataTable();
                if (Session["PRODUCTCLAIMQPS"] != null)
                {
                    dtqpsbill = (DataTable)HttpContext.Session["PRODUCTCLAIMQPS"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qpsmodel.Add(new qpsmodel
                    {
                        depotid = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]),
                        bsid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_BUSINESSSEGMENT_ID"]),
                        grid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_PRINCIPLEGROUP_ID"]),
                        distid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_DISTRIBUTOR_ID"]),
                        retrid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_RETAILER_ID"]),
                        remarks = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        date = Convert.ToString(ds.Tables[0].Rows[0]["DTOC"]),
                        tag = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_TAG"]),
                        Amount = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_AMT"]),
                        processno = Convert.ToString(ds.Tables[0].Rows[0]["PROCESSNUMBER"]),
                        claimno = Convert.ToString(ds.Tables[0].Rows[0]["QV_CLM_NO"]),
                      
                    });
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtqpsbill.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["CLAIM_PRODUCT_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PRODUCT_ID"]).Trim();
                        dr["CLAIM_PRODUCT_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PRODUCT_NAME"]).Trim();
                        dr["CLAIM_PACKSIZE_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PACKSIZE_ID"]).Trim();
                        dr["CLAIM_PACKSIZE_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PACKSIZE_NAME"]).Trim();
                        dr["CLAIM_CAT_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_CAT_ID"]).Trim();
                        dr["CLAIM_CAT_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_CAT_NAME"]).Trim();
                        dr["FROM_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["FROM_DATE"]).Trim();
                        dr["TO_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["TO_DATE"]).Trim();
                        dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]).Trim();
                        dr["INVOICE_NO"] = Convert.ToString(ds.Tables[1].Rows[i]["INVOICE_NO"]).Trim();
                        dr["QTYDETAILS"] = Convert.ToString(ds.Tables[1].Rows[i]["QUANTITYDETAILS"]).Trim();
                        if (Convert.ToString(ds.Tables[1].Rows[i]["INVOICE_DATE"]).Trim() == "01/01/1900")
                        {
                            dr["INVOICE_DATE"] = "";
                        }
                        else
                        {
                            dr["INVOICE_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["INVOICE_DATE"]).Trim();
                        }

                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                        dr["NARRATION"] = Convert.ToString(ds.Tables[1].Rows[i]["NARRATION"]).Trim();
                        dtqpsbill.Rows.Add(dr);
                        dtqpsbill.AcceptChanges();
                    }
                    HttpContext.Session["PRODUCTCLAIMQPS"] = dtqpsbill;

                }
            }
            catch(Exception ex)
            {

            }
                     return Json(qpsmodel);
        }
       [HttpPost]
        public JsonResult Editqpsdtl()
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIMQPS"];
            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    INVOICE_NO = row["INVOICE_NO"].ToString(),
                    INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),

                    TO_DATE = row["TO_DATE"].ToString(),
                    QTY = row["QTY"].ToString(),
                    QTYDETAILS = row["QTYDETAILS"].ToString(),
                });
            return Json(qpsclaimdtl);
        
        }

        #endregion



        /*Damage Claim Start*/
        public ActionResult Damageclaim()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BindDamageClaim(string claimtype) /*make complain grid*/
        {
            var userid = _ICacheManager.Get<object>("UserID");
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            List<qpshdr> qpshdr = new List<qpshdr>();
            Claimscontext Claimscontext = new Claimscontext();
            qpshdr = Claimscontext.BindComplainGrid(claimtype,userid.ToString(), Finyear);
            return Json(qpshdr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCategory(string mode,string catid,string distributorid)
        {
         
            List<CategoryClaim> categories = new List<CategoryClaim>();
            Claimscontext Claimscontext = new Claimscontext();
            categories = Claimscontext.BindCategory(mode, catid, distributorid);
            return Json(categories);
        }

        public JsonResult BindProductCatwise(string mode, string catid, string distributorid)
        {

            List<ProductClaim> productClaims = new List<ProductClaim>();
            Claimscontext Claimscontext = new Claimscontext();
            productClaims = Claimscontext.BindProductCatwise(mode, catid,distributorid);
            return Json(productClaims);
        }
        public JsonResult BindPacksize(string mode,string productid, string distributorid)
        {

            List<ProductPacksize> packsizes = new List<ProductPacksize>();
            Claimscontext Claimscontext = new Claimscontext();
            packsizes = Claimscontext.BindPacksize(mode, productid, distributorid);
            return Json(packsizes);
        }

        public JsonResult BindBatchno(string mode, string productid,string distributorid)
        {

            List<ProductBatchno> batchnos = new List<ProductBatchno>();
            Claimscontext Claimscontext = new Claimscontext();
            batchnos = Claimscontext.BindBatchno(mode, productid, distributorid);
            return Json(batchnos);
        }
        public JsonResult BindReason(string mode, string productid, string distributorid)
        {

            List<DamageReason> damageReasons = new List<DamageReason>();
            Claimscontext Claimscontext = new Claimscontext();
            damageReasons = Claimscontext.BindReason(mode, productid, distributorid);
            return Json(damageReasons);
        }

        public DataTable CreateDamageClaimDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PRODUCT_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PRODUCT_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PACKSIZE_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_PACKSIZE_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_CAT_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("CLAIM_CAT_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("INVOICE_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("BASE_PRICE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DISCOUNT_PRICE", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CLAIM_QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TOTAL_INVOICE_QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("REASON_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("REASON", typeof(string)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));
            HttpContext.Session["PRODUCTCLAIM"] = dt;
            return dt;
        }

        public EmptyResult RemoveDamageClaimeSession()
        {

            Session["PRODUCTCLAIM"] = "";
            Session["PRODUCTCLAIM"] = null;


            return new EmptyResult();

        }

        [HttpPost]
        public JsonResult addDamgeClaim(string PRODUCTID, string PRODUCTNAME,string CLAIM_PACKSIZE_ID,string CLAIM_PACKSIZE_NAME,string CLAIM_CAT_ID, 
            string CLAIM_CAT_NAME,string BATCHNO,string INVOICE_NO,string INVOICE_DATE, decimal MRP, decimal BASE_PRICE, decimal DISCOUNT_PRICE,
            decimal CLAIM_QTY, decimal TOTAL_INVOICE_QTY, decimal AMOUNT,string NARRATION,string REASON_ID,string REASON
        )
        {
            if (HttpContext.Session["PRODUCTCLAIM"] == null)
            {
                CreateDamageClaimDataTable();
            }
            List<DamageClaimDetailsGrid> qpsclaimdtl = new List<DamageClaimDetailsGrid>();
            DataTable dtproductclaim = (DataTable)HttpContext.Session["PRODUCTCLAIM"];
            DataRow dr = dtproductclaim.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["CLAIM_PRODUCT_ID"] = PRODUCTID;
            dr["CLAIM_PRODUCT_NAME"] = PRODUCTNAME;
            dr["CLAIM_PACKSIZE_ID"] = CLAIM_PACKSIZE_ID;
            dr["CLAIM_PACKSIZE_NAME"] = CLAIM_PACKSIZE_NAME;
            dr["CLAIM_CAT_ID"] = CLAIM_CAT_ID;
            dr["CLAIM_CAT_NAME"] = CLAIM_CAT_NAME;
            dr["BATCHNO"] = BATCHNO;
            dr["INVOICE_NO"] = INVOICE_NO;
            dr["INVOICE_DATE"] = INVOICE_DATE;
            dr["MRP"] = MRP;
            dr["BASE_PRICE"] = BASE_PRICE;
            dr["DISCOUNT_PRICE"] = DISCOUNT_PRICE;
            dr["CLAIM_QTY"] = CLAIM_QTY;
            dr["TOTAL_INVOICE_QTY"] = TOTAL_INVOICE_QTY;
            dr["AMOUNT"] = AMOUNT;
            dr["NARRATION"] = NARRATION;
            dr["REASON_ID"] = REASON_ID;
            dr["REASON"] = REASON;
            dtproductclaim.Rows.Add(dr);
            dtproductclaim.AcceptChanges();
            Session["PRODUCTCLAIM"] = dtproductclaim;


            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new DamageClaimDetailsGrid
                {
                    GUID = row["GUID"].ToString(),
                    CLAIM_PRODUCT_ID = row["CLAIM_PRODUCT_ID"].ToString(),
                    CLAIM_PRODUCT_NAME = row["CLAIM_PRODUCT_NAME"].ToString(),
                    CLAIM_PACKSIZE_ID = row["CLAIM_PACKSIZE_ID"].ToString(),
                    CLAIM_PACKSIZE_NAME = row["CLAIM_PACKSIZE_NAME"].ToString(),
                    CLAIM_CAT_ID = row["CLAIM_CAT_ID"].ToString(),
                    CLAIM_CAT_NAME = row["CLAIM_CAT_NAME"].ToString(),
                    BATCHNO = row["BATCHNO"].ToString(),
                    INVOICE_NO = row["INVOICE_NO"].ToString(),
                    INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                    MRP = Convert.ToDecimal(row["MRP"]),
                    DISCOUNT_PRICE = Convert.ToDecimal(row["DISCOUNT_PRICE"]),
                    BASE_PRICE = Convert.ToDecimal(row["BASE_PRICE"]),
                    CLAIM_QTY = Convert.ToDecimal(row["CLAIM_QTY"]),
                    TOTAL_INVOICE_QTY = Convert.ToDecimal(row["TOTAL_INVOICE_QTY"]),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                    REASON_ID = row["REASON_ID"].ToString(),
                    REASON = row["REASON"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                });
            return Json(qpsclaimdtl);
        }

        public JsonResult SaveDamageClaim(qpsmodel tran)
        {
           
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            tran.UserID = userid.ToString();
            tran.FinYear = Finyear.ToString();
            if (Session["PRODUCTCLAIM"] == null)
            {
                this.CreateDamageClaimDataTable();
            }
            DataTable dttransporter = (DataTable)HttpContext.Session["PRODUCTCLAIM"];
            string xml = null;
            xml = ConvertDatatableToXML(dttransporter);
            Claimscontext Claimscontext = new Claimscontext();
            responseMessage = Claimscontext.SaveDamageClaim(tran, xml).ToList();
            foreach (var msg in responseMessage)
            {

                TempData["messagetext"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDamage(string ClaimID)
        {
            List<qpsmodel> qpsmodel = new List<qpsmodel>();
            Hashtable hashTable = new Hashtable();
            try
            {
                hashTable.Add("C_CLAIMID", ClaimID);
                DButility dbcon = new DButility();
                DataSet ds = new DataSet();
                ds = dbcon.SysFetchDataInDataSet("[USP_DAMAGE_CLAIM_DETAILS]", hashTable);
                DataTable dtqpsbill = new DataTable();
                CreateDamageClaimDataTable();
                if (Session["PRODUCTCLAIM"] != null)
                {
                    dtqpsbill = (DataTable)HttpContext.Session["PRODUCTCLAIM"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qpsmodel.Add(new qpsmodel
                    {
                        depotid = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]),
                        bsid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_BUSINESSSEGMENT_ID"]),
                        grid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_PRINCIPLEGROUP_ID"]),
                        distid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_DISTRIBUTOR_ID"]),
                        retrid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_RETAILER_ID"]),
                        remarks = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        date = Convert.ToString(ds.Tables[0].Rows[0]["DTOC"]),
                        tag = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_TAG"]),
                        Amount = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_AMT"]),
                        processno = Convert.ToString(ds.Tables[0].Rows[0]["PROCESSNUMBER"]),
                        claimno = Convert.ToString(ds.Tables[0].Rows[0]["DAM_CLM_NO"]),

                    });
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtqpsbill.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["CLAIM_PRODUCT_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PRODUCT_ID"]).Trim();
                        dr["CLAIM_PRODUCT_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PRODUCT_NAME"]).Trim();
                        dr["CLAIM_PACKSIZE_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PACKSIZE_ID"]).Trim();
                        dr["CLAIM_PACKSIZE_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_PACKSIZE_NAME"]).Trim();
                        dr["CLAIM_CAT_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_CAT_ID"]).Trim();
                        dr["CLAIM_CAT_NAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_CAT_NAME"]).Trim();
                        dr["INVOICE_NO"] = Convert.ToString(ds.Tables[1].Rows[i]["INVOICE_NO"]).Trim();
                        dr["INVOICE_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["INVOICE_DATE"]).Trim();
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                        dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                        dr["BASE_PRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["BASE_PRICE"]).Trim();
                        dr["DISCOUNT_PRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNT_PRICE"]).Trim();
                        dr["CLAIM_QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["CLAIM_QTY"]).Trim();
                        dr["TOTAL_INVOICE_QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTAL_INVOICE_QTY"]).Trim();
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                        dr["REASON_ID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASON_ID"]).Trim();
                        dr["REASON"] = Convert.ToString(ds.Tables[1].Rows[i]["REASON"]).Trim();
                        dr["NARRATION"] = Convert.ToString(ds.Tables[1].Rows[i]["NARRATION"]).Trim();
                        dtqpsbill.Rows.Add(dr);
                        dtqpsbill.AcceptChanges();
                    }
                    HttpContext.Session["PRODUCTCLAIM"] = dtqpsbill;

                }
            }
            catch (Exception ex)
            {

            }
            return Json(qpsmodel);
        }



        [HttpPost]
        public JsonResult EditDamgaedtl()
        {
            List<DamageClaimDetailsGrid> qpsclaimdtl = new List<DamageClaimDetailsGrid>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIM"];
            foreach (DataRow row in dtproductclaim.Rows)
                    qpsclaimdtl.Add(new DamageClaimDetailsGrid
                    {
                        GUID = row["GUID"].ToString(),
                        CLAIM_PRODUCT_ID = row["CLAIM_PRODUCT_ID"].ToString(),
                        CLAIM_PRODUCT_NAME = row["CLAIM_PRODUCT_NAME"].ToString(),
                        CLAIM_PACKSIZE_ID = row["CLAIM_PACKSIZE_ID"].ToString(),
                        CLAIM_PACKSIZE_NAME = row["CLAIM_PACKSIZE_NAME"].ToString(),
                        CLAIM_CAT_ID = row["CLAIM_CAT_ID"].ToString(),
                        CLAIM_CAT_NAME = row["CLAIM_CAT_NAME"].ToString(),
                        BATCHNO = row["BATCHNO"].ToString(),
                        INVOICE_NO = row["INVOICE_NO"].ToString(),
                        INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                        MRP = Convert.ToDecimal(row["MRP"]),
                        DISCOUNT_PRICE = Convert.ToDecimal(row["DISCOUNT_PRICE"]),
                        BASE_PRICE = Convert.ToDecimal(row["BASE_PRICE"]),
                        CLAIM_QTY = Convert.ToDecimal(row["CLAIM_QTY"]),
                        TOTAL_INVOICE_QTY = Convert.ToDecimal(row["TOTAL_INVOICE_QTY"]),
                        AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                        REASON_ID = row["REASON_ID"].ToString(),
                        REASON = row["REASON"].ToString(),
                        NARRATION = row["NARRATION"].ToString(),
                    });
            return Json(qpsclaimdtl);

        }

        [HttpPost]
        public JsonResult deleTempDamageGrid(string GUID)
        {
            List<DamageClaimDetailsGrid> qpsclaimdtl = new List<DamageClaimDetailsGrid>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIM"];

            DataRow[] drr = dtproductclaim.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtproductclaim.AcceptChanges();
            }
            Session["PRODUCTCLAIM"] = dtproductclaim;

            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new DamageClaimDetailsGrid
                {
                    GUID = row["GUID"].ToString(),
                    CLAIM_PRODUCT_ID = row["CLAIM_PRODUCT_ID"].ToString(),
                    CLAIM_PRODUCT_NAME = row["CLAIM_PRODUCT_NAME"].ToString(),
                    CLAIM_PACKSIZE_ID = row["CLAIM_PACKSIZE_ID"].ToString(),
                    CLAIM_PACKSIZE_NAME = row["CLAIM_PACKSIZE_NAME"].ToString(),
                    CLAIM_CAT_ID = row["CLAIM_CAT_ID"].ToString(),
                    CLAIM_CAT_NAME = row["CLAIM_CAT_NAME"].ToString(),
                    BATCHNO = row["BATCHNO"].ToString(),
                    INVOICE_NO = row["INVOICE_NO"].ToString(),
                    INVOICE_DATE = row["INVOICE_DATE"].ToString(),
                    MRP = Convert.ToDecimal(row["MRP"]),
                    DISCOUNT_PRICE = Convert.ToDecimal(row["DISCOUNT_PRICE"]),
                    BASE_PRICE = Convert.ToDecimal(row["BASE_PRICE"]),
                    CLAIM_QTY = Convert.ToDecimal(row["CLAIM_QTY"]),
                    TOTAL_INVOICE_QTY = Convert.ToDecimal(row["TOTAL_INVOICE_QTY"]),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                    REASON_ID = row["REASON_ID"].ToString(),
                    REASON = row["REASON"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                });
            return Json(qpsclaimdtl);
        }

        public JsonResult DamgeDeleteClaim(string claimid)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext claimscontext = new Claimscontext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            responseMessage = claimscontext.DamgeDeleteClaim(claimid, Finyear);
            foreach (var msg in responseMessage)
            {

                TempData["messagetext"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }


        /*Display Claim Start 13.05.2020*/
        public ActionResult DisplayClaim()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BindDisplayClaim(string claimtype) 
        {
            var userid = _ICacheManager.Get<object>("UserID");
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            List<qpshdr> qpshdr = new List<qpshdr>();
            Claimscontext Claimscontext = new Claimscontext();
            qpshdr = Claimscontext.BindDisplayClaim(claimtype, userid.ToString(), Finyear);
            return Json(qpshdr, JsonRequestBehavior.AllowGet);
        }

        public DataTable CreateDisplayClaimDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("FROM_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("TO_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));
            HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"] = dt;
            return dt;
        }

        [HttpPost]
        public JsonResult addDisplaygrid(
            string FROMDT,
            string TODT,
            decimal AMOUNT,
            string NARRATION)
        {
            if (HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"] == null)
            {
                CreateDisplayClaimDataTable();
            }
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = (DataTable)HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"];
            DataRow dr = dtproductclaim.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["FROM_DATE"] = FROMDT;
            dr["TO_DATE"] = TODT;
            dr["AMOUNT"] = AMOUNT;
            dr["NARRATION"] = NARRATION;
            dtproductclaim.Rows.Add(dr);
            dtproductclaim.AcceptChanges();
            Session["PRODUCTCLAIMDISPLAY_V2"] = dtproductclaim;


            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                });
            return Json(qpsclaimdtl);
        }


        [HttpPost]
        public JsonResult deleTempDisplayGrid(string GUID)
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIMDISPLAY_V2"];

            DataRow[] drr = dtproductclaim.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtproductclaim.AcceptChanges();
            }
            Session["PRODUCTCLAIMDISPLAY_V2"] = dtproductclaim;

            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    AMOUNT = Convert.ToDecimal( row["AMOUNT"]),
                    NARRATION = row["NARRATION"].ToString(),
                });
            return Json(qpsclaimdtl);
        }

        public EmptyResult RemoveDisplayClaimeSession()
        {

            Session["PRODUCTCLAIMDISPLAY_V2"] = "";
            Session["PRODUCTCLAIMDISPLAY_V2"] = null;


            return new EmptyResult();

        }

        public JsonResult SaveDisplayClaim(qpsmodel tran)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            tran.UserID = userid.ToString();
            tran.FinYear = Finyear.ToString();
            if (Session["PRODUCTCLAIMDISPLAY_V2"] == null)
            {
                this.CreateDisplayClaimDataTable();
            }
            DataTable dttransporter = (DataTable)HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"];
            string xml = null;
            string xmlcostcenter = "";
            string xmlDebitCreditGST = null;
            xml = ConvertDatatableToXML(dttransporter);
            Claimscontext Claimscontext = new Claimscontext();
            responseMessage = Claimscontext.SaveDisplayClaim(tran, xml).ToList();
            //ClsVoucherentry clsvoucher = new ClsVoucherentry();

            //  string voucherno = clsvoucher.SaveTransporterbillV2(tran,xml);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDisplay(string ClaimID)
        {
            List<qpsmodel> qpsmodel = new List<qpsmodel>();
            Hashtable hashTable = new Hashtable();
            try
            {
                hashTable.Add("C_CLAIMID", ClaimID);
                DButility dbcon = new DButility();
                DataSet ds = new DataSet();
                ds = dbcon.SysFetchDataInDataSet("[USP_DISPLAY_CLAIM_DETAILS_V2]", hashTable);
                DataTable dtqpsbill = new DataTable();
                CreateDisplayClaimDataTable();
                if (Session["PRODUCTCLAIMDISPLAY_V2"] != null)
                {
                    dtqpsbill = (DataTable)HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qpsmodel.Add(new qpsmodel
                    {
                        depotid = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]),
                        bsid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_BUSINESSSEGMENT_ID"]),
                        grid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_PRINCIPLEGROUP_ID"]),
                        distid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_DISTRIBUTOR_ID"]),
                        retrid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_RETAILER_ID"]),
                        remarks = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        date = Convert.ToString(ds.Tables[0].Rows[0]["DTOC"]),
                        Amount = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_AMT"]),
                        processno = Convert.ToString(ds.Tables[0].Rows[0]["PROCESSNUMBER"]),
                        claimno = Convert.ToString(ds.Tables[0].Rows[0]["DISPLAY_CLM_NO"]),

                    });
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtqpsbill.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["FROM_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["FROM_DATE"]).Trim();
                        dr["TO_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["TO_DATE"]).Trim();
                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ds.Tables[1].Rows[i]["AMOUNT"]).Trim());
                        dr["NARRATION"] = Convert.ToString(ds.Tables[1].Rows[i]["NARRATION"]).Trim();
                        dtqpsbill.Rows.Add(dr);
                        dtqpsbill.AcceptChanges();
                    }
                    HttpContext.Session["PRODUCTCLAIMDISPLAY_V2"] = dtqpsbill;

                }
            }
            catch (Exception ex)
            {

            }
            return Json(qpsmodel);
        }
        [HttpPost]
        public JsonResult EditDisplaydtl()
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["PRODUCTCLAIMDISPLAY_V2"];
            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                });
            return Json(qpsclaimdtl);

        }

        public JsonResult DisplayDeleteClaim(string claimid)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext claimscontext = new Claimscontext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            responseMessage = claimscontext.DisplayDeleteClaim(claimid, Finyear);
            foreach (var msg in responseMessage)
            {

                TempData["messagetext"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }


        /*giftClaim Start 15.05.2020 15:09*/
        public ActionResult giftClaim()
        {
            return View();
        }

        public DataTable CreateGiftClaimDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("FROM_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("TO_DATE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CLAIM_PASSQTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));
            HttpContext.Session["MARGINCLAIMDISPLAY"] = dt;
            return dt;
        }

        [HttpPost]
        public JsonResult addGiftGrid(
            string FROMDT,
            string TODT,
             string PRODUCTID,
            string PRODUCTNAME,
            decimal AMOUNT,
            decimal QTY,
            string NARRATION
           )
        {
            if (HttpContext.Session["MARGINCLAIMDISPLAY"] == null)
            {
                CreateGiftClaimDataTable();
            }
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = (DataTable)HttpContext.Session["MARGINCLAIMDISPLAY"];
            DataRow dr = dtproductclaim.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["FROM_DATE"] = FROMDT;
            dr["TO_DATE"] = TODT;
            dr["PRODUCTID"] = PRODUCTID;
            dr["PRODUCTNAME"] = PRODUCTNAME;
            dr["AMOUNT"] = AMOUNT;
            dr["CLAIM_PASSQTY"] = QTY;
            dr["NARRATION"] = NARRATION;
            dtproductclaim.Rows.Add(dr);
            dtproductclaim.AcceptChanges();
            Session["MARGINCLAIMDISPLAY"] = dtproductclaim;


            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                    QTY = row["CLAIM_PASSQTY"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                });
            return Json(qpsclaimdtl);
        }


        [HttpPost]
        public JsonResult deleTempGiftyGrid(string GUID)
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["MARGINCLAIMDISPLAY"];

            DataRow[] drr = dtproductclaim.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtproductclaim.AcceptChanges();
            }
            Session["MARGINCLAIMDISPLAY"] = dtproductclaim;

            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    QTY = row["CLAIM_PASSQTY"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                });
            return Json(qpsclaimdtl);
        }

        public EmptyResult RemoveGiftClaimeSession()
        {

            Session["MARGINCLAIMDISPLAY"] = "";
            Session["MARGINCLAIMDISPLAY"] = null;


            return new EmptyResult();

        }

        public JsonResult SaveGiftClaim(qpsmodel tran)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            tran.UserID = userid.ToString();
            tran.FinYear = Finyear.ToString();
            if (Session["MARGINCLAIMDISPLAY"] == null)
            {
                this.CreateGiftClaimDataTable();
            }
            DataTable dttransporter = (DataTable)HttpContext.Session["MARGINCLAIMDISPLAY"];
            string xml = null;
            string xmlcostcenter = "";
            string xmlDebitCreditGST = null;
            xml = ConvertDatatableToXML(dttransporter);
            Claimscontext Claimscontext = new Claimscontext();
            responseMessage = Claimscontext.SaveGiftClaim(tran, xml).ToList(); 
            //ClsVoucherentry clsvoucher = new ClsVoucherentry();

            //  string voucherno = clsvoucher.SaveTransporterbillV2(tran,xml);
            foreach (var msg in responseMessage)
            {

                TempData["messageid"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditGift(string ClaimID)
        {
            List<qpsmodel> qpsmodel = new List<qpsmodel>();
            Hashtable hashTable = new Hashtable();
            try
            {
                hashTable.Add("C_MARGIN_CLAIMID", ClaimID);
                DButility dbcon = new DButility();
                DataSet ds = new DataSet();
                ds = dbcon.SysFetchDataInDataSet("[USP_MARGIN_CLAIM_DETAILS_V2]", hashTable);
                DataTable dtqpsbill = new DataTable();
                CreateGiftClaimDataTable();
                if (Session["MARGINCLAIMDISPLAY"] != null)
                {
                    dtqpsbill = (DataTable)HttpContext.Session["MARGINCLAIMDISPLAY"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qpsmodel.Add(new qpsmodel
                    {
                        depotid = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]),
                        bsid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_BUSINESSSEGMENT_ID"]),
                        grid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_PRINCIPLEGROUP_ID"]),
                        distid = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_DISTRIBUTOR_ID"]),
                        remarks = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        date = Convert.ToString(ds.Tables[0].Rows[0]["DTOC"]),
                        Amount = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_AMT"]),
                        //Qty = Convert.ToString(ds.Tables[0].Rows[0]["CLAIM_PASSQTY"]),
                        processno = Convert.ToString(ds.Tables[0].Rows[0]["PROCESSNUMBER"]),
                        claimno = Convert.ToString(ds.Tables[0].Rows[0]["GIFT_CLM_NO"]),

                    });
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtqpsbill.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["FROM_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["FROM_DATE"]).Trim();
                        dr["TO_DATE"] = Convert.ToString(ds.Tables[1].Rows[i]["TO_DATE"]).Trim();
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ds.Tables[1].Rows[i]["AMOUNT"]).Trim());
                        dr["CLAIM_PASSQTY"] = Convert.ToString(String.Format("{0:0.00}", ds.Tables[1].Rows[i]["CLAIM_PASSQTY"]).Trim());
                        dr["NARRATION"] = Convert.ToString(ds.Tables[1].Rows[i]["NARRATION"]).Trim();
                        dtqpsbill.Rows.Add(dr);
                        dtqpsbill.AcceptChanges();
                    }
                    HttpContext.Session["MARGINCLAIMDISPLAY"] = dtqpsbill;

                }
            }
            catch (Exception ex)
            {

            }
            return Json(qpsmodel);
        }
        [HttpPost]
        public JsonResult EditGiftdtl()
        {
            List<qpsclaimdtl> qpsclaimdtl = new List<qpsclaimdtl>();
            DataTable dtproductclaim = new DataTable();
            dtproductclaim = (DataTable)Session["MARGINCLAIMDISPLAY"];
            foreach (DataRow row in dtproductclaim.Rows)

                qpsclaimdtl.Add(new qpsclaimdtl
                {
                    GUID = row["GUID"].ToString(),
                    NARRATION = row["NARRATION"].ToString(),
                    PRODUCTID = row["PRODUCTID"].ToString(),
                    PRODUCTNAME = row["PRODUCTNAME"].ToString(),
                    FROM_DATE = row["FROM_DATE"].ToString(),
                    TO_DATE = row["TO_DATE"].ToString(),
                    AMOUNT = Convert.ToDecimal(row["AMOUNT"]),
                    QTY = row["CLAIM_PASSQTY"].ToString(),
                });
            return Json(qpsclaimdtl);

        }

        public JsonResult GiftDeleteClaim(string claimid)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            Claimscontext claimscontext = new Claimscontext();
            var finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = finyear.ToString();
            responseMessage = claimscontext.GiftDeleteClaim(claimid, Finyear);
            foreach (var msg in responseMessage)
            {

                TempData["messagetext"] = msg.response;

            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}