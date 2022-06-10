using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FactoryModel;
using FactoryDatacontext;
using System.Web.Mvc;
using FactoryModule1.Helpers;
using System.IO;
using System.Collections;
using FactoryModule1._1.Helpers;

namespace FactoryModule1._1.Controllers
{
    //[SessionTimeout]
    public class TransporterController : Controller
    {
        public ICacheManager _ICacheManager;
        // GET: Transporter
        public TransporterController()
        {
            _ICacheManager = new CacheManager();
        }
        public EmptyResult RemoveSession()
        {

            Session["TRANSPORTERBILL"] = "";
            Session["TRANSPORTERBILL"] = null;


            return new EmptyResult();

        }
        public void onepointlogin()
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
            List<users> users = new List<users>();

            if (Request.Cookies["userInfo"] != null)
            {

                HttpContext.Session["UserID"] = userInfo["UserID"].ToString();
                _ICacheManager.Add("UserID", userInfo["UserID"].ToString());
                _ICacheManager.Add("USERTYPE", userInfo["USERTYPE"].ToString());
                HttpContext.Session["UTypeId"] = userInfo["UTypeId"].ToString();
                //HttpContext.Session["UTNAME"] = userInfo["UTNAME"].ToString();
                //HttpContext.Session["FNAME"] = userInfo["FNAME"].ToString();
                //HttpContext.Session["APPLICABLETO"] = userInfo["APPLICABLETO"].ToString();
                _ICacheManager.Add("TPU", userInfo["TPU"].ToString());
                HttpContext.Session["TPU"] = userInfo["TPU"].ToString();
                _ICacheManager.Add("IUserID", userInfo["IUserID"].ToString());
                HttpContext.Session["IUserID"] = userInfo["IUserID"].ToString();
                HttpContext.Session["USERTAG"] = userInfo["USERTAG"].ToString();
                HttpContext.Session["FINYEAR"] = userInfo["FINYEAR"].ToString();
                _ICacheManager.Add("FINYEAR", userInfo["FINYEAR"].ToString());





            }


            //if (userInfo != null)
            //{
            //    users.Add(new users
            //    {
            //        USERID = userInfo["UserID"].ToString(),
            //        USERTYPE = userInfo["USERTYPE"].ToString(),
            //        UTNAME = userInfo["UTNAME"].ToString(),
            //        FNAME = userInfo["FNAME"].ToString(),
            //        APPLICABLETO = userInfo["APPLICABLETO"].ToString(),
            //        TPU = userInfo["TPU"].ToString(),
            //        USERTAG = userInfo["USERTAG"].ToString(),
            //        IUSERID = userInfo["IUserID"].ToString(),
            //        FINYEAR = userInfo["FINYEAR"].ToString(),
            //    });
            //    }


        }
        public DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            dt.Columns.Add(new DataColumn("LRGRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("LRGRDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("INVID", typeof(string)));
            dt.Columns.Add(new DataColumn("INVNO", typeof(string)));
            dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLNO", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("TDSPERCENTRAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TDS", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTPERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTAX", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTPERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTAX", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTPERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTAX", typeof(string)));
            dt.Columns.Add(new DataColumn("UGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("UGSTPERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("UGSTAX", typeof(string)));
            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("TDSID", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLDATE", typeof(string)));
            dt.Columns.Add(new DataColumn("TDSDEDUCTABLEAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLINGTOSTATEID", typeof(string)));
            dt.Columns.Add(new DataColumn("BILLINGTOSTATENAME", typeof(string)));

            dt.Columns.Add(new DataColumn("TRANSPORTERID", typeof(string)));
            dt.Columns.Add(new DataColumn("TRANSPORTERNAME", typeof(string)));
            HttpContext.Session["TRANSPORTERBILL"] = dt;
            return dt;
        }
        //fin yr check
        [HttpPost]
        public JsonResult finyrchk(string finyr)
        {
            string currentdt;
            string frmdate;
            string todate;
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            //var finyr = _ICacheManager.Get<object>("FINYEAR");
            string _finyr = finyr;
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Transporterbillv2()
        {
            return View();
        }
        public ActionResult Transporterbilldepot()
        {
          //  onepointlogin();
            return View();
        }
        [HttpPost]
        public JsonResult BindDepot(string userid)
        {
            //var userid = _ICacheManager.Get<object>("UserID");
           // var usertype = _ICacheManager.Get<object>("USERTYPE");
            List<Depomodel> Depomodel = new List<Depomodel>();
            TransporterContext accountcontext = new TransporterContext();
            Depomodel = accountcontext.BindDepot(userid.ToString().Trim());
            return Json(Depomodel);
        }
        [HttpPost]
        public JsonResult BindState()
        {

            List<state> state = new List<state>();
            Accountscontext accountcontext = new Accountscontext();
            state = accountcontext.BindAllState();
            return Json(state);
        }
        [HttpPost]
        public JsonResult BindTransporter(string depoid)
        {

            List<Transportermodel> Transportermodel = new List<Transportermodel>();
            TransporterContext accountcontext = new TransporterContext();
            Transportermodel = accountcontext.BindTransporter(depoid);
            return Json(Transportermodel);
        }
        [HttpPost]
        public JsonResult BindTransporterv2(string depoid)
        {

            List<Transportermodel> Transportermodel = new List<Transportermodel>();
            TransporterContext accountcontext = new TransporterContext();
            Transportermodel = accountcontext.BindTransporterv2(depoid);
            return Json(Transportermodel);
        }
        [HttpPost]
        public JsonResult BindExportDepot()
        {

            List<Depomodel> Depomodel = new List<Depomodel>();
            TransporterContext accountcontext = new TransporterContext();
            Depomodel = accountcontext.BindExportDepot();
            return Json(Depomodel);
        }
        [HttpPost]
        public JsonResult ExportDepotChecking(string depoid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.ExportDepotChecking(depoid);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult GetOfflineStatus(string depoid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.GetOfflineStatus(depoid);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult GetTdscheck(string transporterid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.GetTdscheck(transporterid);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult GetIsTransferToHo(string transporterid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.GetIsTransferToHo(transporterid);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult GetReverseCharge(string transporterid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.GetReverseCharge(transporterid);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult BindTransporterbyInvoice(string tag, string invoiceid)
        {

            List<Transporterbyinvoice> Transporterbyinvoice = new List<Transporterbyinvoice>();
            TransporterContext accountcontext = new TransporterContext();
            Transporterbyinvoice = accountcontext.BindTransporterbyInvoice(tag, invoiceid);
            return Json(Transporterbyinvoice);
        }
        [HttpPost]
        public JsonResult BindTransporterbyInvoicev2(string tag, string invoiceid)
        {

            List<Transporterbyinvoice> Transporterbyinvoice = new List<Transporterbyinvoice>();
            TransporterContext accountcontext = new TransporterContext();
            Transporterbyinvoice = accountcontext.BindTransporterbyInvoicev2(tag, invoiceid);
            return Json(Transporterbyinvoice);
        }
        [HttpPost]
        public JsonResult BindAllInvoicenoNEWV2(string tag, string depoid, string transporterbillid,string finyr)
        {
           // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<Invoicenomodel> Invoicenomodel = new List<Invoicenomodel>();
            TransporterContext accountcontext = new TransporterContext();
            Invoicenomodel = accountcontext.BindAllInvoicenoNEWV2(tag, depoid, transporterbillid, finyr);
            return Json(Invoicenomodel);
        }
        [HttpPost]
        public JsonResult BindAllInvoicenoNEWV2depot(string tag, string depoid, string transporterbillid,string finyr)
        {
                //var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<Invoicenomodel> Invoicenomodel = new List<Invoicenomodel>();
            TransporterContext accountcontext = new TransporterContext();
            Invoicenomodel = accountcontext.BindAllInvoicenoNEWV2depot(tag, depoid, transporterbillid, finyr);
            return Json(Invoicenomodel);
        }
        [HttpPost]
        public JsonResult BindGstPercentage_New(string depoid, string tranporterid, string bvalue, string InvoiceId, string BillingType, string Fromstate, string Tostate, string RCM)
        {
                var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            TransporterContext accountcontext = new TransporterContext();
            GSTpercentage = accountcontext.BindGstPercentage_New(depoid, tranporterid, bvalue, InvoiceId, BillingType, Fromstate, Tostate, RCM);
            return Json(GSTpercentage);
        }
        [HttpPost]
        public JsonResult TDSPercentage(string tranporterid, string bvalue,string finyr,string billdate)
        {
           // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            TransporterContext accountcontext = new TransporterContext();
            GSTpercentage = accountcontext.TDSPercentage(tranporterid, bvalue,billdate);
            return Json(GSTpercentage);
        }
        [HttpPost]
        public JsonResult BindGstPercentage(string depoid, string tranporterid, string bvalue, string InvoiceId, string BillingType, string Fromstate, string Tostate,string finyr)
        {
            //var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            TransporterContext accountcontext = new TransporterContext();
            GSTpercentage = accountcontext.BindGstPercentage(depoid, tranporterid, bvalue, InvoiceId, BillingType, Fromstate, Tostate);
            return Json(GSTpercentage);
        }
        public class addtransporterdtl
        {
            public string GUID { get; set; }
            public string BILLDATE { get; set; }
            public string TRANSPORTERNAME { get; set; }
            public string LRGRNO { get; set; }
            public string INVNO { get; set; }
            public string BILLNO { get; set; }
            public string BILLAMOUNT { get; set; }
            public string CGSTPERCENTAGE { get; set; }

            public string CGSTAX { get; set; }
            public string SGSTPERCENTAGE { get; set; }
            public string SGSTAX { get; set; }


            public string IGSTPERCENTAGE { get; set; }
            public string IGSTAX { get; set; }
            public string GROSSWEIGHT { get; set; }
            public string NETAMOUNT { get; set; }

            public string _sumgrossweight { get; set; }
            public string _sumtds1 { get; set; }
            public string _sumcgst { get; set; }
            public string _sumsgst { get; set; }
            public string _sumigst { get; set; }
            public string _sumugst { get; set; }
            public string _sumbillamount { get; set; }
            public string _sumtdsdeductableamount { get; set; }
            public string _sumnetamount { get; set; }
        }

        [HttpPost]
        public JsonResult addgrid(string INVID, string INVNO,
   string LRGRNO,
   string LRGRDATE,
   string GROSSWEIGHT,
   string BILLNO,
   string BILLAMOUNT,
   string TDSPERCENTRAGE,
   string TDS,
   string CGSTID,
   string CGSTPERCENTAGE,
   string CGSTAX,
   string SGSTID,
   string SGSTPERCENTAGE,
   string SGSTAX,
   string IGSTID,
   string IGSTPERCENTAGE,
  string IGSTAX,
  string UGSTID,
  string UGSTPERCENTAGE,
  string UGSTAX,
  string NETAMOUNT,
  string TDSID,
  string BILLDATE,
  string TDSDEDUCTABLEAMOUNT,
  string TRANSPORTERID,
  string TRANSPORTERNAME
   )
        {

            decimal sumtdsdeductableamount = 0;
            decimal sumbillamount = 0;
            decimal sumnetamount = 0;
            decimal sumgrossweight = 0;
            decimal sumtds1 = 0;
            decimal sumcgst = 0;
            decimal sumsgst = 0;
            decimal sumigst = 0;
            List<addtransporterdtl> invoicedtl = new List<addtransporterdtl>();
            if (HttpContext.Session["TRANSPORTERBILL"] == null)
            {
                CreateDataTable();
            }
            DataTable dttransporterbill = (DataTable)HttpContext.Session["TRANSPORTERBILL"];
            DataRow dr = dttransporterbill.NewRow();
            if(TDS=="")
            {
                TDS = "0.00";
            }
            if (UGSTAX == "")
            {
                UGSTAX = "0.00";
            }
            try
            {


                dr["GUID"] = Guid.NewGuid();



                dr["INVID"] = INVID;
                dr["INVNO"] = INVNO;

                dr["BILLINGTOSTATEID"] = "0";
                dr["BILLINGTOSTATENAME"] = "0";

                dr["LRGRNO"] = LRGRNO;
                dr["LRGRDATE"] = LRGRDATE;
                dr["GROSSWEIGHT"] = GROSSWEIGHT;
                dr["BILLNO"] = BILLNO;
                dr["BILLAMOUNT"] = BILLAMOUNT;
                dr["TDSPERCENTRAGE"] = TDSPERCENTRAGE;
                dr["TDS"] = TDS; //tds value

                dr["CGSTID"] = CGSTID;
                dr["CGSTPERCENTAGE"] = CGSTPERCENTAGE;
                dr["CGSTAX"] = CGSTAX;

                dr["SGSTID"] = SGSTID;
                dr["SGSTPERCENTAGE"] = SGSTPERCENTAGE;
                dr["SGSTAX"] = SGSTAX;


                dr["IGSTID"] = IGSTID;
                dr["IGSTPERCENTAGE"] = IGSTPERCENTAGE;
                dr["IGSTAX"] = IGSTAX;

                dr["UGSTID"] = UGSTID;
                dr["UGSTPERCENTAGE"] = UGSTPERCENTAGE;
                dr["UGSTAX"] = UGSTAX;

                dr["NETAMOUNT"] = NETAMOUNT;
                dr["TDSID"] = TDSID;

                dr["BILLDATE"] = BILLDATE;
                dr["TDSDEDUCTABLEAMOUNT"] = TDSDEDUCTABLEAMOUNT;

                dr["TRANSPORTERID"] = TRANSPORTERID;
                dr["TRANSPORTERNAME"] = TRANSPORTERNAME;

                dttransporterbill.Rows.Add(dr);
                dttransporterbill.AcceptChanges();
                Session["TRANSPORTERBILL"] = dttransporterbill;
                decimal sumugst = 0;
                //decimal sumnetamountshow = 0;
                for (int i = 0; i < dttransporterbill.Rows.Count; i++)
                {


                    sumgrossweight = sumgrossweight + Convert.ToDecimal(dttransporterbill.Rows[i]["GROSSWEIGHT"].ToString().Trim());
                    sumtds1 = sumtds1 + Convert.ToDecimal(dttransporterbill.Rows[i]["TDS"].ToString().Trim());
                    sumcgst = sumcgst + Convert.ToDecimal(dttransporterbill.Rows[i]["CGSTAX"].ToString().Trim());
                    sumsgst = sumsgst + Convert.ToDecimal(dttransporterbill.Rows[i]["SGSTAX"].ToString().Trim());
                    sumigst = sumigst + Convert.ToDecimal(dttransporterbill.Rows[i]["IGSTAX"].ToString().Trim());
                    sumugst = sumugst + Convert.ToDecimal(dttransporterbill.Rows[i]["UGSTAX"].ToString().Trim());
                    sumbillamount = (Convert.ToDecimal(dttransporterbill.Rows[i]["BILLAMOUNT"].ToString().Trim()) + sumbillamount);
                    sumtdsdeductableamount = (Convert.ToDecimal(dttransporterbill.Rows[i]["TDSDEDUCTABLEAMOUNT"].ToString().Trim()) + sumtdsdeductableamount);
                    sumnetamount = sumnetamount + Convert.ToDecimal(dttransporterbill.Rows[i]["NETAMOUNT"].ToString().Trim());
                    //sumnetamountshow = sumnetamountshow + Convert.ToDecimal(dttransporterbill.Rows[i]["NETAMOUNT"].ToString().Trim());

                }

                foreach (DataRow row in dttransporterbill.Rows)

                    invoicedtl.Add(new addtransporterdtl
                    {
                        GUID = row["GUID"].ToString(),
                        BILLDATE = row["BILLDATE"].ToString(),
                        TRANSPORTERNAME = row["TRANSPORTERNAME"].ToString(),
                        LRGRNO = row["LRGRNO"].ToString(),
                        INVNO = row["INVNO"].ToString(),
                        BILLNO = row["BILLNO"].ToString(),
                        BILLAMOUNT = row["BILLAMOUNT"].ToString(),
                        CGSTPERCENTAGE = row["CGSTPERCENTAGE"].ToString(),

                        CGSTAX = row["CGSTAX"].ToString(),
                        SGSTPERCENTAGE = row["SGSTPERCENTAGE"].ToString(),
                        SGSTAX = row["SGSTAX"].ToString(),
                        IGSTPERCENTAGE = row["IGSTPERCENTAGE"].ToString(),
                        IGSTAX = row["IGSTAX"].ToString(),
                        GROSSWEIGHT = row["GROSSWEIGHT"].ToString(),
                        NETAMOUNT = row["NETAMOUNT"].ToString(),
                        _sumgrossweight = Convert.ToString(sumgrossweight),
                        _sumtds1 = Convert.ToString(sumtds1),
                        _sumcgst = Convert.ToString(sumcgst),
                        _sumsgst = Convert.ToString(sumsgst),

                        _sumigst = Convert.ToString(sumigst),
                        _sumugst = Convert.ToString(sumugst),
                        _sumbillamount = Convert.ToString(sumbillamount),
                        _sumtdsdeductableamount = Convert.ToString(sumtdsdeductableamount),
                        _sumnetamount = Convert.ToString(sumnetamount),

                    });
            }
            catch(Exception ex)
            {

            }
            return Json(invoicedtl);
        }
        [HttpPost]
        public JsonResult checkLRGRNOV2(string TAG, string transporterID, string LRGRNO, string TRANSPORETERBILLID, string INVID,string finyr)
        {
           // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.checkLRGRNOV2(TAG, transporterID, LRGRNO, TRANSPORETERBILLID, INVID,finyr);
            return Json(messageresponse);
        }
        [HttpPost]
        public JsonResult delgrid(string GUID)
        { 

            decimal sumtdsdeductableamount = 0;
        decimal sumbillamount = 0;
        decimal sumnetamount = 0;
        decimal sumgrossweight = 0;
        decimal sumtds1 = 0;
        decimal sumcgst = 0;
        decimal sumsgst = 0;
        decimal sumigst = 0;
        List<addtransporterdtl> invoicedtl = new List<addtransporterdtl>();
           
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["TRANSPORTERBILL"];

            DataRow[] drr = dtdeleterecord.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleterecord.AcceptChanges();
            }
            Session["TRANSPORTERBILL"] = dtdeleterecord;
            for (int i = 0; i < dtdeleterecord.Rows.Count; i++)
            {
                sumnetamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["NETAMOUNT"].ToString().Trim()) + sumnetamount);
                sumgrossweight = (Convert.ToDecimal(dtdeleterecord.Rows[i]["GROSSWEIGHT"].ToString().Trim()) + sumgrossweight);
                //sumtds1 =( Convert.ToDecimal(dtdeleterecord.Rows[i]["TDS"].ToString().Trim()) +sumtds1);
                sumcgst = sumcgst + Convert.ToDecimal(dtdeleterecord.Rows[i]["CGSTAX"].ToString().Trim());
                sumsgst = sumsgst + Convert.ToDecimal(dtdeleterecord.Rows[i]["SGSTAX"].ToString().Trim());
                sumigst = sumigst + Convert.ToDecimal(dtdeleterecord.Rows[i]["IGSTAX"].ToString().Trim());
                //sumugst = sumugst + Convert.ToDecimal(dtdeleterecord.Rows[i]["UGSTAX"].ToString().Trim());
                sumbillamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["BILLAMOUNT"].ToString().Trim()) + sumbillamount);
                sumtdsdeductableamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["TDSDEDUCTABLEAMOUNT"].ToString().Trim()) + sumtdsdeductableamount);

            }
            foreach (DataRow row in dtdeleterecord.Rows)

                invoicedtl.Add(new addtransporterdtl
                {
                    GUID = row["GUID"].ToString(),
                    BILLDATE = row["BILLDATE"].ToString(),
                    TRANSPORTERNAME = row["TRANSPORTERNAME"].ToString(),
                    LRGRNO = row["LRGRNO"].ToString(),
                    INVNO = row["INVNO"].ToString(),
                    BILLNO = row["BILLNO"].ToString(),
                    BILLAMOUNT = row["BILLAMOUNT"].ToString(),
                    CGSTPERCENTAGE = row["CGSTPERCENTAGE"].ToString(),

                    CGSTAX = row["CGSTAX"].ToString(),
                    SGSTPERCENTAGE = row["SGSTPERCENTAGE"].ToString(),
                    SGSTAX = row["SGSTAX"].ToString(),
                    IGSTPERCENTAGE = row["IGSTPERCENTAGE"].ToString(),
                    IGSTAX = row["IGSTAX"].ToString(),
                    GROSSWEIGHT = row["GROSSWEIGHT"].ToString(),
                    NETAMOUNT = row["NETAMOUNT"].ToString(),
                    _sumgrossweight = Convert.ToString(sumgrossweight),
                    _sumtds1 = "0",
                    _sumcgst = Convert.ToString(sumcgst),
                    _sumsgst = Convert.ToString(sumsgst),

                    _sumigst = Convert.ToString(sumigst),
                    _sumugst = "0",
                    _sumbillamount = Convert.ToString(sumbillamount),
                    _sumtdsdeductableamount = Convert.ToString(sumtdsdeductableamount),
                    _sumnetamount = Convert.ToString(sumnetamount),

                });
            return Json(invoicedtl);
    }
        #region===savelogic
        public JsonResult SaveTransporterbillV2fac(Transporterbillfac tran)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            var userid = _ICacheManager.Get<object>("UserID");
            var Finyear = _ICacheManager.Get<object>("FINYEAR");
            tran.UserID = userid.ToString();
            tran.finyear = Finyear.ToString();
            if (Session["TRANSPORTERBILL"] != null)
            {
                // this.CreateDataTable();


                DataTable dttransporter = (DataTable)HttpContext.Session["TRANSPORTERBILL"];
                string xml = null;
                string xmlcostcenter = "";
                string xmlDebitCreditGST = null;
                xml = ConvertDatatableToXML(dttransporter);
                TransporterContext transporter = new TransporterContext();
                responseMessage = transporter.SaveTransporterbillV2fac(tran, xml).ToList();
                //ClsVoucherentry clsvoucher = new ClsVoucherentry();

                //  string voucherno = clsvoucher.SaveTransporterbillV2(tran,xml);
                foreach (var msg in responseMessage)
                {

                    TempData["messageid"] = msg.response;

                }
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveTransporterbillV2(Transporterbillfac tran)
        {
            string messageid1 = "";
            string message = "";
            string editstatus = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
           // var userid = _ICacheManager.Get<object>("UserID");
            //var Finyear = _ICacheManager.Get<object>("FINYEAR");
           // tran.UserID = userid.ToString();
            //tran.finyear = Finyear.ToString();
            if (Session["TRANSPORTERBILL"] == null)
            {
                this.CreateDataTable();
            }
            DataTable dttransporter = (DataTable)HttpContext.Session["TRANSPORTERBILL"];
            string xml = null;
           

            
            string xmlcostcenter = "";

            int _flag = 1;

            string xmlDebitCreditGST = null;
            xml = ConvertDatatableToXML(dttransporter);
            TransporterContext transporter = new TransporterContext();
            if (tran.Mode == "U")
            {
                responseMessage = transporter.Getstatusforedit(tran.ID);

            }
            foreach (var vr in responseMessage)
            {
                editstatus = vr.response;
            }
            if (editstatus == "1")
            {
              //  message = "Transporter Bill Approved,Not Allow To Delete.";
                _flag = 0;
                responseMessage.Add(new messageresponse
                {
                    response = "Transporter Bill Approved,Not Allow To Delete."



                });
            }
            if (dttransporter.Rows.Count != 0)
            {
                if (_flag == 1)
                {
                    responseMessage = transporter.SaveTransporterbillV2(tran, xml).ToList();
                }
                //ClsVoucherentry clsvoucher = new ClsVoucherentry();

                //  string voucherno = clsvoucher.SaveTransporterbillV2(tran,xml);
                foreach (var msg in responseMessage)
                {

                    TempData["messageid"] = msg.response;

                }
            }
            else
            {
                responseMessage.Add(new messageresponse
                {
                    response = "Session has expired"



                });
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
        #endregion
        #region ==show/edit & print
        [HttpPost]
        public JsonResult BindTransporterbillall(string depotid, string fromData, string ToDate,string finyr)
        {
           // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<Transporterbillshow> Transporterbillshow = new List<Transporterbillshow>();
            TransporterContext accountcontext = new TransporterContext();
            Transporterbillshow = accountcontext.BindTransporterbillall(depotid, fromData,ToDate,finyr);
            return Json(Transporterbillshow);
        }
        [HttpPost]
        public JsonResult BindTransporterbillalldepot(string depotid, string fromData, string ToDate, string finyr)
        {
            // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<Transporterbillshow> Transporterbillshow = new List<Transporterbillshow>();
            TransporterContext accountcontext = new TransporterContext();
            Transporterbillshow = accountcontext.BindTransporterbillalldepot(depotid, fromData, ToDate, finyr);
            return Json(Transporterbillshow);
        }
        [HttpPost]
        public JsonResult DeleteTransporterbill(string TransporterbillID,string checker)
        {
            string res = "";
            string message = "";
            string editstatus = "";
            string tdschk = "0";
            int deleteflag = 0;
            int _flag = 1;
            var userid = _ICacheManager.Get<object>("UserID");
           
            List<messageresponse> msg = new List<messageresponse>();
            List<messageresponse> msg1 = new List<messageresponse>();
           // List<messageresponse> msg2 = new List<messageresponse>(); List<messageresponse> delflag = new List<messageresponse>();
            List<responseint> flag = new List<responseint>();
            TransporterContext accountcontext = new TransporterContext();

            //string ip_address = Request.UserHostAddress.ToString().Trim();
            string ip_address = "";
            msg1= accountcontext.Getstatusforedit(TransporterbillID);
            
            foreach (var vr in msg1)
            {
                editstatus = vr.response;
            }
            if(editstatus=="1")
            {
                message = "Transporter Bill Approved,Not Allow To Delete.";
                _flag = 0;
            }
            


            if (checker == "TRUE")
            {
                _flag = 0;
                message = "Not Allow To Delete.";
            }
           
            else
            if(_flag==1)
            {

                /* Add Logic : When create auto Journal from Payment then not delete journal on 18/12/2018 */
                msg = accountcontext.DeleteTransporterbill(TransporterbillID);
                foreach (var vr in msg)
                {
                    res = vr.response;
                }

                /* TDS related voucher are not deleted on 16/03/2019 */

                if (res == "1")
                {
                    message = "Record Deleted Successfully";
                }
                else if (res == "0")
                {
                    message = "Error in deleting";
                }
            }
            msg1.Add(new messageresponse
            {
                response = message



            });
            return Json(msg1);
        }
        [HttpPost]
        public JsonResult DeleteTransporterbilldepot(string TransporterbillID, string checker, string region,string userid,string finyr)
        {
            string res = "";
            string message = "";
            string editstatus = "";
            string tdschk = "0";
            int deleteflag = 0;
            int _flag = 1;
           // var userid = _ICacheManager.Get<object>("UserID");
            //var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            List<messageresponse> msg = new List<messageresponse>();
            List<messageresponse> msg1 = new List<messageresponse>();
            List<messageresponse> msg2 = new List<messageresponse>(); List<messageresponse> delflag = new List<messageresponse>();
            List<responseint> flag = new List<responseint>();
            TransporterContext accountcontext = new TransporterContext();

            //string ip_address = Request.UserHostAddress.ToString().Trim();
            string ip_address = "";
            msg1 = accountcontext.Getstatusforedit(TransporterbillID);
            //if (finyr.ToString() != "2018-2019" && (region == "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670" || region == "24E1EF07-0A41-4470-B745-9E4BA164C837"))
            if (finyr.ToString() != "2018-2019" )

            {
                msg2 = accountcontext.GetTdscheck(TransporterbillID);
                foreach (var vr in msg2)
                {
                    tdschk = vr.response;
                }
            }
            foreach (var vr in msg1)
            {
                editstatus = vr.response;
            }
            if (editstatus == "1")
            {
                message = "Transporter Bill Approved,Not Allow To Delete.";
                _flag = 0;
            }



            if (checker == "TRUE")
            {
                _flag = 0;
                message = "Not Allow To Delete.";
            }
            if (tdschk == "1")
            {
                message = "Voucher is TDS related, you can't.";
                _flag = 0;
            }
            else
            if (_flag == 1)
            {

                /* Add Logic : When create auto Journal from Payment then not delete journal on 18/12/2018 */
                msg = accountcontext.DeleteTransporterbilldepot(TransporterbillID);
                foreach (var vr in msg)
                {
                    res = vr.response;
                }

                /* TDS related voucher are not deleted on 16/03/2019 */

                if (res == "1")
                {
                    message = "Record Deleted Successfully";
                }
                else if (res == "0")
                {
                    message = "Error in deleting";
                }
            }
            msg1.Add(new messageresponse
            {
                response = message



            });
            return Json(msg1);
        }
        [HttpPost]
        public JsonResult GetAccEntryID(string transporterbillid)
        {
            List<messageresponse> msg = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            msg = accountcontext.GetAccEntryID(transporterbillid);
            return Json(msg);
        }
        [HttpPost]
        public JsonResult EditTransporterbill(string ID)
        {
            List<Transporterbillhdr> Transporterbillhdr = new List<Transporterbillhdr>();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_TRANSPORTERBILLID", ID);
            DButility dbcon = new DButility();
            DataSet ds = new DataSet();
            ds = dbcon.SysFetchDataInDataSet("[sp_TRANSPORTER_DETAILS]", hashTable);
            DataTable dtTransporterEdit = new DataTable();
            CreateDataTable();
            if (Session["TRANSPORTERBILL"] != null)
            {
                dtTransporterEdit = (DataTable)HttpContext.Session["TRANSPORTERBILL"];
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
             

                    Transporterbillhdr.Add(new Transporterbillhdr
                    {
                        TRANSPORTERBILLNO =Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERBILLNO"]),
                        TRANSPORTERBILLDATE = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERBILLDATE"]),
                        GNGNO = Convert.ToString(ds.Tables[0].Rows[0]["GNGNO"]),


                        TDS_REASONID = Convert.ToString(ds.Tables[0].Rows[0]["TDS_REASONID"]),
                        GST_REASONID = Convert.ToString(ds.Tables[0].Rows[0]["GST_REASONID"]),
                        BILLTYPEID = Convert.ToString(ds.Tables[0].Rows[0]["BILLTYPEID"]),
                        EXPORTTAG = Convert.ToString(ds.Tables[0].Rows[0]["EXPORTTAG"]),
                        DEPOTID = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]),
                        VIRTUALDEPOTID = Convert.ToString(ds.Tables[0].Rows[0]["VIRTUALDEPOTID"]),
                        TRANSPORTERID = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]),
                        BILLINGFROMSTATEID = Convert.ToString(ds.Tables[0].Rows[0]["BILLINGFROMSTATEID"]),
                        ISTRANSFERTOHO = Convert.ToString(ds.Tables[0].Rows[0]["ISTRANSFERTOHO"]),
                        REVERSECHARGE = Convert.ToString(ds.Tables[0].Rows[0]["REVERSECHARGE"]),
                        TDSAPPLICABLE = Convert.ToString(ds.Tables[0].Rows[0]["TDSAPPLICABLE"]),
                        TOTALNETAMOUNT = Convert.ToString(ds.Tables[0].Rows[0]["TOTALNETAMOUNT"]),
                        TOTALGROSSWEIGHT = Convert.ToString(ds.Tables[0].Rows[0]["TOTALGROSSWEIGHT"]),
                        TOTALTDS = Convert.ToString(ds.Tables[0].Rows[0]["TOTALTDS"]),
                        TOTALTDSDEDUCTABLE = Convert.ToString(ds.Tables[0].Rows[0]["TOTALTDSDEDUCTABLE"]),
                        TOTALBILLAMOUNT = Convert.ToString(ds.Tables[0].Rows[0]["TOTALBILLAMOUNT"]),
                        TOTALCGST = Convert.ToString(ds.Tables[0].Rows[0]["TOTALCGST"]),
                        TOTALSGST = Convert.ToString(ds.Tables[0].Rows[0]["TOTALSGST"]),
                        TOTALIGST = Convert.ToString(ds.Tables[0].Rows[0]["TOTALIGST"]),
                        TDSPECENTAGE = Convert.ToString(ds.Tables[0].Rows[0]["TDSPECENTAGE"]),
                        REMARKS = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]),
                        BILLNO = Convert.ToString(ds.Tables[0].Rows[0]["BILLNO"]),
                    });
               
                }
            if (ds.Tables[0].Rows.Count > 0)
            {
                

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtTransporterEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LRGRNO"] = Convert.ToString(ds.Tables[0].Rows[i]["LRGRNO"]).Trim();
                    dr["LRGRDATE"] = Convert.ToString(ds.Tables[0].Rows[i]["LRGRDATE"]).Trim();
                    dr["INVID"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKBILLID"]).Trim();
                    dr["INVNO"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKBILLNO"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["GROSSWEIGHT"]).ToString().Trim();
                    dr["BILLNO"] = Convert.ToString(ds.Tables[0].Rows[i]["BILLNO"]).Trim();
                    dr["BILLAMOUNT"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["BILLAMOUNT"]).ToString().Trim();
                    dr["TDSPERCENTRAGE"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["TDSPERCENTRAGE"]).ToString().Trim();
                    dr["TDS"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["TDS"]).ToString().Trim();//tds value

                    dr["CGSTID"] = Convert.ToString(ds.Tables[0].Rows[i]["CGSTID"]).Trim();
                    dr["CGSTPERCENTAGE"] = Convert.ToString(ds.Tables[0].Rows[i]["CGSTPERCENTAGE"]).Trim();
                    dr["CGSTAX"] = Convert.ToString(ds.Tables[0].Rows[i]["CGSTAX"]).Trim();

                    dr["SGSTID"] = Convert.ToString(ds.Tables[0].Rows[i]["SGSTID"]).Trim();
                    dr["SGSTPERCENTAGE"] = Convert.ToString(ds.Tables[0].Rows[i]["SGSTPERCENTAGE"]).Trim();
                    dr["SGSTAX"] = Convert.ToString(ds.Tables[0].Rows[i]["SGSTAX"]).Trim();


                    dr["IGSTID"] = Convert.ToString(ds.Tables[0].Rows[i]["IGSTID"]).Trim();
                    dr["IGSTPERCENTAGE"] = Convert.ToString(ds.Tables[0].Rows[i]["IGSTPERCENTAGE"]).Trim();
                    dr["IGSTAX"] = Convert.ToString(ds.Tables[0].Rows[i]["IGSTAX"]).Trim();

                    dr["UGSTID"] = Convert.ToString(ds.Tables[0].Rows[i]["UGSTID"]).Trim();
                    dr["UGSTPERCENTAGE"] = Convert.ToString(ds.Tables[0].Rows[i]["UGSTPERCENTAGE"]).Trim();
                    dr["UGSTAX"] = Convert.ToString(ds.Tables[0].Rows[i]["UGSTAX"]).Trim();

                    dr["NETAMOUNT"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["NETAMOUNT"]).ToString().Trim();
                    dr["TDSID"] = Convert.ToString(ds.Tables[0].Rows[i]["TDSID"]).Trim();

                    dr["BILLDATE"] = Convert.ToString(ds.Tables[0].Rows[i]["BILLDATE"]).Trim();
                    dr["TDSDEDUCTABLEAMOUNT"] = Convert.ToString(ds.Tables[0].Rows[i]["TDSDEDUCTABLEAMOUNT"]).Trim();

                    dr["BILLINGTOSTATEID"] = Convert.ToString(ds.Tables[0].Rows[i]["BILLINGTOSTATEID"]).Trim();
                    dr["BILLINGTOSTATENAME"] = Convert.ToString(ds.Tables[0].Rows[i]["BILLINGTOSTATENAME"]).Trim();

                    dr["TRANSPORTERID"] = Convert.ToString(ds.Tables[0].Rows[i]["TRANSPORTERID"]).Trim();
                    dr["TRANSPORTERNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["TRANSPORTERNAME"]).Trim();

                    dtTransporterEdit.Rows.Add(dr);
                    dtTransporterEdit.AcceptChanges();
                }
                HttpContext.Session["TRANSPORTERBILL"] = dtTransporterEdit;
            }
                    //TransporterContext accountcontext = new TransporterContext();
                    //Transporterbillhdr = accountcontext.EditTransporterbill(ID);
                    return Json(Transporterbillhdr);
        }
        [HttpPost]
        public JsonResult EditTransporterbilldtl(string ID)
        {
            List<addtransporterdtl> invoicedtl = new List<addtransporterdtl>();
            DataTable dtTransporterEdit = new DataTable();
            dtTransporterEdit = (DataTable)HttpContext.Session["TRANSPORTERBILL"];
            foreach (DataRow row in dtTransporterEdit.Rows)

                invoicedtl.Add(new addtransporterdtl
                {
                    GUID = row["GUID"].ToString(),
                    BILLDATE = row["BILLDATE"].ToString(),
                    TRANSPORTERNAME = row["TRANSPORTERNAME"].ToString(),
                    LRGRNO = row["LRGRNO"].ToString(),
                    INVNO = row["INVNO"].ToString(),
                    BILLNO = row["BILLNO"].ToString(),
                    BILLAMOUNT = row["BILLAMOUNT"].ToString(),
                    CGSTPERCENTAGE = row["CGSTPERCENTAGE"].ToString(),

                    CGSTAX = row["CGSTAX"].ToString(),
                    SGSTPERCENTAGE = row["SGSTPERCENTAGE"].ToString(),
                    SGSTAX = row["SGSTAX"].ToString(),
                    IGSTPERCENTAGE = row["IGSTPERCENTAGE"].ToString(),
                    IGSTAX = row["IGSTAX"].ToString(),
                    GROSSWEIGHT = row["GROSSWEIGHT"].ToString(),
                    NETAMOUNT = row["NETAMOUNT"].ToString(),
                  

                });
                return Json(invoicedtl);
        }
        [HttpPost]
        public JsonResult EditTransporterbill1(string ID)
        {
            List<Transporterbillhdr> Transporterbillhdr = new List<Transporterbillhdr>();
            TransporterContext accountcontext = new TransporterContext();
            Transporterbillhdr = accountcontext.EditTransporterbill(ID);
            return Json(Transporterbillhdr);
        }
        [HttpPost]
        public JsonResult Edittdsid(string transporterid)
        {

            List<messageresponse> messageresponse = new List<messageresponse>();
            TransporterContext accountcontext = new TransporterContext();
            messageresponse = accountcontext.Edittdsid(transporterid);
            return Json(messageresponse);
        }
        #endregion

    }
}