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
    public class AccountstranController : Controller
    {
        // GET: Accountstran
        public ICacheManager _ICacheManager;
        public AccountstranController()
        {
            _ICacheManager = new CacheManager();
        }
        public void onepointlogin1()
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
            List<users> users = new List<users>();

            if (Request.Cookies["userInfo"] != null)
            {
                HttpContext.Session["UserID"] = userInfo["UserID"].ToString();
                _ICacheManager.Add("UserID", userInfo["UserID"].ToString());
                _ICacheManager.Add("USERTYPE", userInfo["USERTYPE"].ToString());
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
        public DataTable CreateVoucherTable()
        {
            DataTable dtvoucher = new DataTable();
            dtvoucher.Clear();
            dtvoucher.Columns.Add("GUID");
            dtvoucher.Columns.Add("LedgerId");
            dtvoucher.Columns.Add("LedgerName");
            dtvoucher.Columns.Add("TxnType");
            dtvoucher.Columns.Add("Amount", typeof(decimal));
            dtvoucher.Columns.Add("BankID");
            dtvoucher.Columns.Add("BankName");
            dtvoucher.Columns.Add("PAYMENTTYPEID");
            dtvoucher.Columns.Add("PAYMENTTYPENAME");
            dtvoucher.Columns.Add("ChequeNo");
            dtvoucher.Columns.Add("ChequeDate");
            dtvoucher.Columns.Add("IsChequeRealised");
            dtvoucher.Columns.Add("Remarks");
            dtvoucher.Columns.Add("ChequeRealisedNo");
            dtvoucher.Columns.Add("ChequeRealisedDate");
            dtvoucher.Columns.Add("DeductableAmount");
            dtvoucher.Columns.Add("DeductablePercentage");
            dtvoucher.Columns.Add("DeductableLedgerId");
            dtvoucher.Columns.Add("IsCostCenter");
            dtvoucher.Columns.Add("IsTagInvoice");
            dtvoucher.Columns.Add("NonTaxableAmount"); /*Add 06/12/2018 */
            dtvoucher.Columns.Add("BYDEFAULTAMOUNT"); /*Add 06/03/2019 */
            dtvoucher.Columns.Add("TransactionAmount"); /*Add 06/03/2019 */

            dtvoucher.Columns.Add("DepartmentID"); /*Add 24/06/2019 */
            dtvoucher.Columns.Add("DepartmentName"); /*Add 24/06/2019 */
            dtvoucher.Columns.Add("BusinessSegId"); /*Add 23/04/2020 */
            dtvoucher.Columns.Add("BusinessSegName"); /*Add 23/04/2020 */
            Session["ACC_VOUCHERDETAILS"] = dtvoucher;
            return dtvoucher;
        }
        public DataTable CreateCostCenterTable()
        {
            DataTable dtcostcenter = new DataTable();
            dtcostcenter.Clear();
            dtcostcenter.Columns.Add("GUID");
            dtcostcenter.Columns.Add("LedgerId");
            dtcostcenter.Columns.Add("LedgerName");
            dtcostcenter.Columns.Add("CostCatagoryID");
            dtcostcenter.Columns.Add("CostCatagoryName");
            dtcostcenter.Columns.Add("CostCenterID");
            dtcostcenter.Columns.Add("CostCenterName");
            dtcostcenter.Columns.Add("BranchID");
            dtcostcenter.Columns.Add("BranchName");
            dtcostcenter.Columns.Add("amount", typeof(decimal));
            dtcostcenter.Columns.Add("BrandID");
            dtcostcenter.Columns.Add("BrandName");
            dtcostcenter.Columns.Add("ProductID");
            dtcostcenter.Columns.Add("ProductName");
            dtcostcenter.Columns.Add("DepartmentID");
            dtcostcenter.Columns.Add("DepartmentName");
            dtcostcenter.Columns.Add("FromDate");
            dtcostcenter.Columns.Add("ToDate");
            dtcostcenter.Columns.Add("Narration");
            dtcostcenter.Columns.Add("TxnType");
            /*Add for Budget 07/02/2019 */
            dtcostcenter.Columns.Add("BudgetApplicableSubComponentDeptID");
            dtcostcenter.Columns.Add("DepartmentalComponenetID");
            dtcostcenter.Columns.Add("DepartmentalComponenetName");
            dtcostcenter.Columns.Add("SubComponenetID");
            dtcostcenter.Columns.Add("SubComponenetName");
            //dtcostcenter.Columns.Add("QUATERSPAN");

            Session["ACC_COSTCENTERDETAILS"] = dtcostcenter;
            return dtcostcenter;
        }
        public DataTable CreateBudgetTable()
        {
            DataTable dtBudget = new DataTable();
            dtBudget.Clear();
            dtBudget.Columns.Add("LedgerId");
            dtBudget.Columns.Add("LedgerName");
            dtBudget.Columns.Add("DepartmentId");
            dtBudget.Columns.Add("DepartmentName");

            Session["BudgetDetails"] = dtBudget;
            return dtBudget;
        }
        public DataTable CreateGSTTable()
        {
            DataTable dtvoucher = new DataTable();
            dtvoucher.Clear();
            dtvoucher.Columns.Add("GUID");
            dtvoucher.Columns.Add("GroupId");
            dtvoucher.Columns.Add("PartyID");
            dtvoucher.Columns.Add("InvoiceNo");
            dtvoucher.Columns.Add("InvoiceDate");
            dtvoucher.Columns.Add("Taxable");
            dtvoucher.Columns.Add("TaxableValue");
            dtvoucher.Columns.Add("HSNCode");
            dtvoucher.Columns.Add("PartyTrade");
            dtvoucher.Columns.Add("StateID");
            dtvoucher.Columns.Add("GSTNo");
            dtvoucher.Columns.Add("TaxTypeID");
            dtvoucher.Columns.Add("TaxType");
            dtvoucher.Columns.Add("TaxAmount");
            dtvoucher.Columns.Add("NetAmount");
            dtvoucher.Columns.Add("TanNo");
            dtvoucher.Columns.Add("OldNew");
            dtvoucher.Columns.Add("Taxable1");
            dtvoucher.Columns.Add("TaxAmount1");
            dtvoucher.Columns.Add("TaxTypeID1");
            dtvoucher.Columns.Add("TaxType1");
            dtvoucher.Columns.Add("ISIGST");
            dtvoucher.Columns.Add("RoundOff");
            dtvoucher.Columns.Add("NonTaxableAmountGST");
            dtvoucher.Columns.Add("PlaceOfSupply");
            Session["GSTDETAILS"] = dtvoucher;
            return dtvoucher;
        }
        public DataTable CreateInvoiceTable()
        {
            DataTable dtinvoice = new DataTable();
            dtinvoice.Clear();
            dtinvoice.Columns.Add("GUID");
            dtinvoice.Columns.Add("LedgerId");
            dtinvoice.Columns.Add("LedgerName");
            dtinvoice.Columns.Add("InvoiceID");
            dtinvoice.Columns.Add("InvoiceNo");
            dtinvoice.Columns.Add("InvoiceDate");
            dtinvoice.Columns.Add("InvoiceOthers");
            dtinvoice.Columns.Add("InvoiceBranchID");
            dtinvoice.Columns.Add("InvoiceBranchName");
            dtinvoice.Columns.Add("VoucherType");
            dtinvoice.Columns.Add("BranchID");
            dtinvoice.Columns.Add("InvoiceAmt", typeof(decimal));
            dtinvoice.Columns.Add("AlreadyAmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("ReturnAmt", typeof(decimal));        /* Add new column ReturnAmt by D.Mondal on 11/10/2018 */
            dtinvoice.Columns.Add("RemainingAmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("AmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("Type");

            HttpContext.Session["ACC_INVOICEDETAILS"] = dtinvoice;
            return dtinvoice;
        }
        public DataTable CreateInvoiceTableCR()
        {
            DataTable dtinvoice = new DataTable();
            dtinvoice.Clear();
            dtinvoice.Columns.Add("GUID");
            dtinvoice.Columns.Add("LedgerId");
            dtinvoice.Columns.Add("LedgerName");
            dtinvoice.Columns.Add("InvoiceID");
            dtinvoice.Columns.Add("InvoiceNo");
            dtinvoice.Columns.Add("InvoiceDate");
            dtinvoice.Columns.Add("InvoiceOthers");
            dtinvoice.Columns.Add("InvoiceBranchID");
            dtinvoice.Columns.Add("InvoiceBranchName");
            dtinvoice.Columns.Add("VoucherType");
            dtinvoice.Columns.Add("BranchID");
            dtinvoice.Columns.Add("InvoiceAmt", typeof(decimal));
            dtinvoice.Columns.Add("AlreadyAmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("ReturnAmt", typeof(decimal));        /* Add new column ReturnAmt by D.Mondal on 11/10/2018 */
            dtinvoice.Columns.Add("RemainingAmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("AmtPaid", typeof(decimal));
            dtinvoice.Columns.Add("Type");

            HttpContext.Session["ACC_INVOICEDETAILS_CR"] = dtinvoice;
            return dtinvoice;
        }

        public class addcostcenter
        {
            public string GUID { get; set; }
            public string LedgerId { get; set; }
            public string LedgerName { get; set; }
            public string CostCatagoryID { get; set; }
            public string CostCatagoryName { get; set; }
            public string CostCenterID { get; set; }
            public string CostCenterName { get; set; }
            public string BranchID { get; set; }
            public string BranchName { get; set; }
            public string amount { get; set; }
            public string BrandID { get; set; }
            public string BrandName { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Narration { get; set; }
            public string TxnType { get; set; }
        }
        public class addVoucherTable
        {
            public string GUID { get; set; }
            public string LedgerId { get; set; }
            public string LedgerName { get; set; }
            public string TxnType { get; set; }
            public string Amount { get; set; }
            public string BankID { get; set; }
            public string BankName { get; set; }
            public string PAYMENTTYPEID { get; set; }
            public string PAYMENTTYPENAME { get; set; }
            public string ChequeNo { get; set; }
            public string ChequeDate { get; set; }
            public string IsChequeRealised { get; set; }
            public string Remarks { get; set; }
            public string ChequeRealisedNo { get; set; }
            public string ChequeRealisedDate { get; set; }
            public string DeductableAmount { get; set; }
            public string DeductablePercentage { get; set; }
            public string DeductableLedgerId { get; set; }
            public string IsCostCenter { get; set; }
            public string IsTagInvoice { get; set; }
            public string NonTaxableAmount { get; set; }
            public string BYDEFAULTAMOUNT { get; set; }
            public string TransactionAmount { get; set; }
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string taxapplicable { get; set; }
            public string BusinessSegId { get; set; }
            public string BusinessSegName { get; set; }

        }
        public class addgsttable
        {
            public string GUID { get; set; }
            public string GroupId { get; set; }
            public string PartyID { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public string Taxable { get; set; }
            public string TaxableValue { get; set; }
            public string HSNCode { get; set; }
            public string PartyTrade { get; set; }
            public string StateID { get; set; }
            public string GSTNo { get; set; }
            public string TaxTypeID { get; set; }
            public string TaxType { get; set; }
            public string TaxAmount { get; set; }
            public string NetAmount { get; set; }
            public string OldNew { get; set; }
            public string Taxable1 { get; set; }
            public string TaxAmount1 { get; set; }
            public string TaxTypeID1 { get; set; }
            public string TaxType1 { get; set; }

            public string ISIGST { get; set; }
            public string NonTaxableAmountGST { get; set; }
            public string RoundOff { get; set; }
            public string PlaceOfSupply { get; set; }

            
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Accvoucher()
        {
          //  onepointlogin();
            return View();
        }
        public ActionResult Accvoucherdepot()
        {
            return View();
        }
        public ActionResult Accadvancevoucher()
        {
            onepointlogin();

            return View();
        }
        public ActionResult Accadvancevoucherdepot()
        {
            return View();
        }
        [HttpPost]
        public JsonResult finyrchk(string finyr,string userid,string TPU)
        {
            HttpCookie userInfo = Request.Cookies["userInfo"];
          int flag = 0;
            string currentdt = "";
            string currentdt1 = "";
            string frmdate = "";
            string todate = "";
            string today;
            string startyear;
            int startyear1;
            string endyear;
            int endyear1;
            string isho = "";
            DateTime datevalidation = DateTime.Today.Date;
            flag = chkbackdateuser(userid,finyr,TPU);
            List<Finyearrange> Finyearrange = new List<Finyearrange>();
            List<backdate> backdate = new List<backdate>();
            List<messageresponse> messageresponse = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            messageresponse = accountcontext.isho();

            foreach (var ho in messageresponse)
            {
                isho = ho.response.ToString();
            }
            var userid1 = Convert.ToString(userid);
            // var userid = _ICacheManager.Get<object>("UserID");
            //  var finyr = _ICacheManager.Get<object>("FINYEAR");
            var finyr1 = Convert.ToString(finyr);
            //var finyr = userInfo["FINYEAR"].ToString();
            var TPU1 = Convert.ToString(TPU);
            //var TPU = _ICacheManager.Get<object>("TPU");

            string _finyr = finyr1.ToString();
            startyear = _finyr.Substring(0, 4);
            startyear1 = Convert.ToInt32(startyear);
            endyear = _finyr.Substring(5);
            endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);

            if (datevalidation >= new DateTime(startyear1, 04, 01) && datevalidation <= new DateTime(endyear1, 03, 31))
            {
                currentdt = datevalidation.ToString("yyyy/MM/dd");
                currentdt1 = datevalidation.ToString("yyyy/MM/dd"); ;
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                today = datevalidation.ToString("yyyy/MM/dd");
            }
            else
            {
                currentdt = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                currentdt1 = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                frmdate = new DateTime(startyear1, 04, 01).ToString("yyyy/MM/dd");
                todate = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
                today = new DateTime(endyear1, 03, 31).ToString("yyyy/MM/dd");
            }

            if (flag == 1)
            {

                backdate = accountcontext.CheckDateRange(userid1.ToString(), finyr1.ToString(), "D");
                foreach (var vr in backdate)
                {
                    string StartDate = Convert.ToString(vr.FromDate);
                    string sDay = StartDate.Substring(0, 2);
                    string sMonth = StartDate.Substring(3, 2);
                    string sYear = StartDate.Substring(6, 4);

                    string EndDate = Convert.ToString(vr.ToDate);
                    string EDay = EndDate.Substring(0, 2);
                    string EMonth = EndDate.Substring(3, 2);
                    string EYear = EndDate.Substring(6, 4);
                    int sDay1 = Convert.ToInt32(sDay);
                    int sMonth1 = Convert.ToInt32(sMonth);
                    int sYear1 = Convert.ToInt32(sYear);

                    int EDay1 = Convert.ToInt32(EDay);
                    int EMonth1 = Convert.ToInt32(EMonth);
                    int EYear1 = Convert.ToInt32(EYear);
                    currentdt = new DateTime(sYear1, sMonth1, sDay1).ToString("yyyy/MM/dd");
                    frmdate = new DateTime(EYear1, EMonth1, EDay1).ToString("yyyy/MM/dd");
                    todate = new DateTime(EYear1, EMonth1, EDay1).ToString("yyyy/MM/dd");
                    today = new DateTime(EYear1, EMonth1, EDay1).ToString("yyyy/MM/dd");
                    currentdt1 = new DateTime(EYear1, EMonth1, EDay1).ToString("yyyy/MM/dd");
                }
            }

            Finyearrange.Add(new Finyearrange
            {
                currentdt = currentdt,
                currentdt1 = currentdt1,
                frmdate = frmdate,
                todate = todate,
                today = today,
                TPU = TPU1.ToString(),
                HO = isho,


            });

            return Json(Finyearrange);
        }

        private int chkbackdateuser(string userid,string finyr,string TPU)
        {
            int flag = 0;
            // var finyr = _ICacheManager.Get<object>("FINYEAR");
            var userid1 = Convert.ToString(userid);
            // var userid = _ICacheManager.Get<object>("UserID");
            //  var finyr = _ICacheManager.Get<object>("FINYEAR");
            var finyr1 = Convert.ToString(finyr);
            //var finyr = userInfo["FINYEAR"].ToString();
            var TPU1 = Convert.ToString(TPU);
            //   var userid = _ICacheManager.Get<object>("UserID");
            List<messageresponse> messageresponse = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            messageresponse = accountcontext.chkbackdateuser(userid.ToString(), finyr.ToString(), "D");
            foreach (var vr in messageresponse)
            {
                flag = Int32.Parse(Convert.ToString(vr.response));
            }


            return flag;
        }


        [HttpPost]
        public JsonResult BindRegion(string userid, string usertype)
        {
          //  var userid = Convert.ToString(Session["UserID"]);
            //      var userid = _ICacheManager.Get<object>("UserID");
            //var usertype = _ICacheManager.Get<object>("USERTYPE");
           // var usertype = Convert.ToString(Session["USERTYPE"]);
            List<Region> region = new List<Region>();
            Accountscontext accountcontext = new Accountscontext();
            region = accountcontext.BindRegion(usertype.ToString(), userid.ToString().Trim());
            return Json(region);
        }
        [HttpPost]
        public JsonResult Bindpaymenttype()
        {

            List<Paymenttype> Paymenttype = new List<Paymenttype>();
            Accountscontext accountcontext = new Accountscontext();
            Paymenttype = accountcontext.Bindpaymenttype();
            return Json(Paymenttype);
        }
        [HttpPost]
        public JsonResult Bindpaymentparty()
        {

            List<paymentparty> paymentparty = new List<paymentparty>();
            Accountscontext accountcontext = new Accountscontext();
            paymentparty = accountcontext.Bindpaymentparty();
            return Json(paymentparty);
        }
        [HttpPost]
        public JsonResult BindVoucherType()
        {

            List<Vouchertype> Vouchertype = new List<Vouchertype>();
            Accountscontext accountcontext = new Accountscontext();
            Vouchertype = accountcontext.BindVoucherType();
            return Json(Vouchertype);
        }
        [HttpPost]
        public JsonResult BindAllState()
        {

            List<state> state = new List<state>();
            Accountscontext accountcontext = new Accountscontext();
            state = accountcontext.BindAllState();
            return Json(state);
        }
        [HttpPost]
        public JsonResult BindAccountTypedr(string VoucherType, string RegionId)
        {

            List<Accountstype> Accountstype = new List<Accountstype>();
            Accountscontext accountcontext = new Accountscontext();
            Accountstype = accountcontext.BindAccountTypedr(VoucherType, RegionId);
            return Json(Accountstype);
        }
        [HttpPost]
        public JsonResult BindCHEQUENOLIST(string bankid, string branchid)
        {

            List<bankname> bankname = new List<bankname>();
            Accountscontext accountcontext = new Accountscontext();
            bankname = accountcontext.BindCHEQUENOLIST(bankid, branchid);
            return Json(bankname);
        }
        [HttpPost]
        public JsonResult BindAccountTypecr(string VoucherType, string RegionId)
        {

            List<Accountstype> Accountstype = new List<Accountstype>();
            Accountscontext accountcontext = new Accountscontext();
            Accountstype = accountcontext.BindAccountTypedr(VoucherType, RegionId);
            return Json(Accountstype);
        }

        [HttpPost]
        public JsonResult BindAccountTypecr_MTClaim(string VoucherType, string RegionId)
        {

            List<Accountstype> Accountstype = new List<Accountstype>();
            Accountscontext accountcontext = new Accountscontext();
            Accountstype = accountcontext.BindAccountTypecr_MTClaim(VoucherType, RegionId);
            return Json(Accountstype);
        }

        [HttpPost]
        public JsonResult BindAccountTypedr_MTClaim(string VoucherType, string RegionId)
        {

            List<Accountstype> Accountstype = new List<Accountstype>();
            Accountscontext accountcontext = new Accountscontext();
            Accountstype = accountcontext.BindAccountTypedr_MTClaim(VoucherType, RegionId);
            return Json(Accountstype);
        }

        public EmptyResult Releasecookie()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                Response.Cookies["userInfo"].Expires = DateTime.Now.AddHours(-1);

            }
            return new EmptyResult();
        }

        public EmptyResult RemoveSession()
        {

            Session["ACC_VOUCHERDETAILS"] = "";
            Session["ACC_VOUCHERDETAILS"] = null;
            HttpContext.Session["ACC_COSTCENTERDETAILS"] = "";
            HttpContext.Session["ACC_COSTCENTERDETAILS"] = null;
            HttpContext.Session["BudgetDetails"] = "";
            HttpContext.Session["BudgetDetails"] = null;
            HttpContext.Session["GSTDETAILS"] = "";
            HttpContext.Session["GSTDETAILS"] = null;

            HttpContext.Session["UPLOADACCOUNTSFILENAME"] = "";
            HttpContext.Session["UPLOADACCOUNTSFILENAME"] = null;
            HttpContext.Session["ACC_INVOICEDETAILS"] = "";
            HttpContext.Session["ACC_INVOICEDETAILS"] = null;
            HttpContext.Session["ACC_INVOICEDETAILS_CR"] = "";
            HttpContext.Session["ACC_INVOICEDETAILS_CR"] = null;

            return new EmptyResult();

        }
        [HttpPost]
        public JsonResult addvoucheradvdrcr(string AccountID, string AccountName,
       string DebitAmount,
       string DeductableAmount,
       string DeductablePercent,
       string DeductableLedgerID,
       string IsCostCenter,
       string IsTagInvoice,
       string Isautoposting,
       string NonTaxableAmount,
       string ByDefaultAmount,
       string TransactionAmount,

       string remarks,
       string departmentid,
       string departmentname,
     string PAYMENTTYPEID,
     string PAYMENTTYPENAME,
     string ChequeNo,
     string ChequeDate,

       string BusinessSegId,
     string BusinessSegName)
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            int flag = 0;
            if (Session["ACC_VOUCHERDETAILS"] == null)
            {
                CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableCheck(AccountID, AccountName, "", "0");
            }
            if (flag == 0)
            {

                DataRow dr = dtvoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = AccountID;
                dr["LedgerName"] = AccountName;
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(DebitAmount);
                dr["BankID"] = "0";
                dr["BankName"] = "";
                dr["PAYMENTTYPEID"] = PAYMENTTYPEID;
                dr["PAYMENTTYPENAME"] = PAYMENTTYPENAME;
                dr["ChequeNo"] = ChequeNo;
                dr["ChequeDate"] = ChequeDate;
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = remarks;
                dr["DeductableAmount"] = Convert.ToString(DeductableAmount);
                dr["DeductablePercentage"] = Convert.ToString(DeductablePercent);
                dr["DeductableLedgerId"] = Convert.ToString(DeductableLedgerID);
                dr["IsCostCenter"] = Convert.ToString(IsCostCenter);
                dr["IsTagInvoice"] = Convert.ToString(IsTagInvoice);
                dr["NonTaxableAmount"] = Convert.ToString(NonTaxableAmount);

                dr["BYDEFAULTAMOUNT"] = ByDefaultAmount;




                dr["TransactionAmount"] = TransactionAmount;


                if (departmentid == "0")  /* 24/06/2019 */
                {
                    dr["DepartmentID"] = "";
                    dr["DepartmentName"] = "";
                }
                else
                {
                    dr["DepartmentID"] = departmentid;
                    dr["DepartmentName"] = departmentname;
                }
                if (departmentname == "")  /* 24/06/2019 */
                {
                    dr["DepartmentName"] = "";
                }

                if (BusinessSegId == "0")  /* 24/06/2019 */
                {
                    dr["BusinessSegId"] = "";
                    dr["BusinessSegName"] = "";
                }
                else
                {
                    dr["BusinessSegId"] = BusinessSegId;
                    dr["BusinessSegName"] = BusinessSegName;
                }
                if (BusinessSegName == "")  /* 24/06/2019 */
                {
                    dr["BusinessSegName"] = "";
                }

                dtvoucher.Rows.Add(dr);
                dtvoucher.AcceptChanges();
                Session["ACC_VOUCHERDETAILS"] = dtvoucher;
                var result = from r in dtvoucher.AsEnumerable()
                             where r.Field<string>("TxnType") == "0"
                             select r;
                DataTable debit = result.CopyToDataTable();
                foreach (DataRow row in debit.Rows)

                    addbudgetlist.Add(new addVoucherTable
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        TxnType = row["TxnType"].ToString(),
                        Amount = row["Amount"].ToString(),
                        BankID = row["BankID"].ToString(),
                        BankName = row["BankName"].ToString(),
                        PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                        PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                        ChequeNo = row["ChequeNo"].ToString(),
                        ChequeDate = row["ChequeDate"].ToString(),
                        IsChequeRealised = row["IsChequeRealised"].ToString(),
                        Remarks = row["Remarks"].ToString(),


                        DeductableAmount = row["DeductableAmount"].ToString(),
                        DeductablePercentage = row["DeductablePercentage"].ToString(),
                        DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                        IsCostCenter = row["IsCostCenter"].ToString(),
                        IsTagInvoice = row["IsTagInvoice"].ToString(),
                        NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                        BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                        TransactionAmount = row["TransactionAmount"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                        BusinessSegId = row["BusinessSegId"].ToString(),
                        BusinessSegName = row["BusinessSegName"].ToString(),
                    });

            }
            return Json(addbudgetlist);
        }
        [HttpPost]
        public JsonResult addvoucherdrcr(string AccountID, string AccountName,
        string DebitAmount,
        string DeductableAmount,
        string DeductablePercent,
        string DeductableLedgerID,
        string IsCostCenter,
        string IsTagInvoice,
        string Isautoposting,
        string NonTaxableAmount,
        string ByDefaultAmount,
        string TransactionAmount,

        string remarks,
        string departmentid,
        string departmentname,
         string BusinessSegId,
     string BusinessSegName)
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            int flag = 0;
            if (Session["ACC_VOUCHERDETAILS"] == null)
            {
                CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableCheck(AccountID, AccountName, "", "0");
            }
            if (flag == 0)
            {

                DataRow dr = dtvoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = AccountID;
                dr["LedgerName"] = AccountName;
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(DebitAmount);
                dr["BankID"] = "0";
                dr["BankName"] = "";
                dr["PAYMENTTYPEID"] = "2";
                dr["PAYMENTTYPENAME"] = "NEFT";
                dr["ChequeNo"] = "";
                dr["ChequeDate"] = "";
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = remarks;
                dr["DeductableAmount"] = Convert.ToString(DeductableAmount);
                dr["DeductablePercentage"] = Convert.ToString(DeductablePercent);
                dr["DeductableLedgerId"] = Convert.ToString(DeductableLedgerID);
                dr["IsCostCenter"] = Convert.ToString(IsCostCenter);
                dr["IsTagInvoice"] = Convert.ToString(IsTagInvoice);
                dr["NonTaxableAmount"] = Convert.ToString(NonTaxableAmount);

                dr["BYDEFAULTAMOUNT"] = ByDefaultAmount;




                dr["TransactionAmount"] = TransactionAmount;


                if (departmentid == "0")  /* 24/06/2019 */
                {
                    dr["DepartmentID"] = "";
                    dr["DepartmentName"] = "";
                }
                else
                {
                    dr["DepartmentID"] = departmentid;
                    dr["DepartmentName"] = departmentname;
                }
                if (departmentname == "")  /* 24/06/2019 */
                {
                    dr["DepartmentName"] = "";
                }

                if (BusinessSegId == "0")  /* 24/06/2019 */
                {
                    dr["BusinessSegId"] = "";
                    dr["BusinessSegName"] = "";
                }
                else
                {
                    dr["BusinessSegId"] = BusinessSegId;
                    dr["BusinessSegName"] = BusinessSegName;
                }
                if (BusinessSegName == "")  /* 24/06/2019 */
                {
                    dr["BusinessSegName"] = "";
                }

                dtvoucher.Rows.Add(dr);
                dtvoucher.AcceptChanges();
                Session["ACC_VOUCHERDETAILS"] = dtvoucher;
                var result = from r in dtvoucher.AsEnumerable()
                             where r.Field<string>("TxnType") == "0"
                             select r;
                DataTable debit = result.CopyToDataTable();
                foreach (DataRow row in debit.Rows)

                    addbudgetlist.Add(new addVoucherTable
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        TxnType = row["TxnType"].ToString(),
                        Amount = row["Amount"].ToString(),
                        BankID = row["BankID"].ToString(),
                        BankName = row["BankName"].ToString(),
                        PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                        PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                        ChequeNo = row["ChequeNo"].ToString(),
                        ChequeDate = row["ChequeDate"].ToString(),
                        IsChequeRealised = row["IsChequeRealised"].ToString(),
                        Remarks = row["Remarks"].ToString(),


                        DeductableAmount = row["DeductableAmount"].ToString(),
                        DeductablePercentage = row["DeductablePercentage"].ToString(),
                        DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                        IsCostCenter = row["IsCostCenter"].ToString(),
                        IsTagInvoice = row["IsTagInvoice"].ToString(),
                        NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                        BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                        TransactionAmount = row["TransactionAmount"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                        BusinessSegId = row["BusinessSegId"].ToString(),
                        BusinessSegName = row["BusinessSegName"].ToString(),
                    });

            }
            return Json(addbudgetlist);
        }
        [HttpPost]
        public JsonResult Bindautovoucher(string autovoucherid)
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_AccEntryId", autovoucherid);
            DButility dbcon = new DButility();
            DataSet dtvoucherdetails = new DataSet();
            dtvoucherdetails = dbcon.SysFetchDataInDataSet("[USP_ACC_PAYMENT_DETAILS]", hashTable);
            if (dtvoucherdetails.Tables[1].Rows.Count > 0)
            {
                this.CreateVoucherTable();
                this.CreateInvoiceTable();
                DataTable dtVoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
                DataTable dtinvoice = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS"];

                DataTable dtinvoice_CR = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS_CR"];

                for (int i = 0; i < dtvoucherdetails.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtVoucher.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["LedgerId"]).Trim();
                    dr["LedgerName"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["LedgerName"]).Trim();
                    dr["TxnType"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["TxnType"]).Trim();
                    dr["Amount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["Amount"]).Trim();
                    dr["BankID"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["BankID"]);
                    dr["BankName"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["BankName"]);
                    dr["PAYMENTTYPEID"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["PAYMENTTYPEID"]);
                    dr["PAYMENTTYPENAME"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["PAYMENTTYPENAME"]);
                    dr["ChequeNo"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["ChequeNo"]);
                    dr["ChequeDate"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["ChequeDate"]);
                    dr["IsChequeRealised"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["IsChequeRealised"]);
                    dr["Remarks"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["Remarks"]);
                    dr["DeductableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableAmount"]);
                    dr["DeductablePercentage"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductablePercentage"]);
                    dr["DeductableLedgerId"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableLedgerId"]);
                    dr["IsCostCenter"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["IsCostCenter"]);
                    dr["IsTagInvoice"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["IsTagInvoice"]);
                    dr["NonTaxableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["NonTaxableAmount"]);
                    dr["BYDEFAULTAMOUNT"] = "0";
                    dr["TransactionAmount"] = "0";


                    dtVoucher.Rows.Add(dr);
                    dtVoucher.AcceptChanges();
                }

                HttpContext.Session["ACC_VOUCHERDETAILS"] = dtVoucher;
                foreach (DataRow row in dtVoucher.Rows)

                    addbudgetlist.Add(new addVoucherTable
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        TxnType = row["TxnType"].ToString(),
                        Amount = row["Amount"].ToString(),
                        BankID = row["BankID"].ToString(),
                        BankName = row["BankName"].ToString(),
                        PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                        PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                        ChequeNo = row["ChequeNo"].ToString(),
                        ChequeDate = row["ChequeDate"].ToString(),
                        IsChequeRealised = row["IsChequeRealised"].ToString(),
                        Remarks = row["Remarks"].ToString(),


                        DeductableAmount = row["DeductableAmount"].ToString(),
                        DeductablePercentage = row["DeductablePercentage"].ToString(),
                        DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                        IsCostCenter = row["IsCostCenter"].ToString(),
                        IsTagInvoice = row["IsTagInvoice"].ToString(),
                        NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                        BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                        TransactionAmount = row["TransactionAmount"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                        BusinessSegId = row["BusinessSegId"].ToString(),
                        BusinessSegName = row["BusinessSegName"].ToString(),
                    });
            }
            return Json(addbudgetlist);

        }
        [HttpPost]

        public JsonResult BindvoucherDr()
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            DataTable dtvoucher = new DataTable();
            dtvoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            var result = from r in dtvoucher.AsEnumerable()
                         where r.Field<string>("TxnType") == "0"
                         select r;
            DataTable cr = result.CopyToDataTable();
            foreach (DataRow row in cr.Rows)

                addbudgetlist.Add(new addVoucherTable
                {
                    GUID = row["GUID"].ToString(),
                    LedgerId = row["LedgerId"].ToString(),
                    LedgerName = row["LedgerName"].ToString(),
                    TxnType = row["TxnType"].ToString(),
                    Amount = row["Amount"].ToString(),
                    BankID = row["BankID"].ToString(),
                    BankName = row["BankName"].ToString(),
                    PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                    PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                    ChequeNo = row["ChequeNo"].ToString(),
                    ChequeDate = row["ChequeDate"].ToString(),
                    IsChequeRealised = row["IsChequeRealised"].ToString(),
                    Remarks = row["Remarks"].ToString(),


                    DeductableAmount = row["DeductableAmount"].ToString(),
                    DeductablePercentage = row["DeductablePercentage"].ToString(),
                    DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                    IsCostCenter = row["IsCostCenter"].ToString(),
                    IsTagInvoice = row["IsTagInvoice"].ToString(),
                    NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                    BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                    TransactionAmount = row["TransactionAmount"].ToString(),
                    DepartmentID = row["DepartmentID"].ToString(),
                    DepartmentName = row["DepartmentName"].ToString(),
                    BusinessSegId = row["BusinessSegId"].ToString(),
                    BusinessSegName = row["BusinessSegName"].ToString(),
                });
            return Json(addbudgetlist);
        }

        [HttpPost]
        public JsonResult addvouchercr1(string AccountID, string AccountName,
      string DebitAmount,
      string DeductableAmount,
      string DeductablePercent,
      string DeductableLedgerID,
      string IsCostCenter,
      string IsTagInvoice,
      string Isautoposting,
      string NonTaxableAmount,
      string ByDefaultAmount,
      string TransactionAmount,

      string remarks,
      string departmentid,
      string departmentname,
      string paymenttypeid,
      string paymenttypename,
      string chequeno,
      string chequedate,
      string taxapplicable,
       string BusinessSegId,
     string BusinessSegName
      )
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            int flag = 0;
            if (Session["ACC_VOUCHERDETAILS"] == null)
            {
                CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableCheck(AccountID, AccountName, "", "0");
            }
            if (flag == 0)
            {

                DataRow dr = dtvoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = AccountID;
                dr["LedgerName"] = AccountName;
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(DebitAmount);
                dr["BankID"] = "0";
                dr["BankName"] = "";
                dr["PAYMENTTYPEID"] = paymenttypeid;
                dr["PAYMENTTYPENAME"] = paymenttypename;
                if (chequeno != "0")
                {
                    dr["ChequeNo"] = chequeno;
                }
                else
                {
                    dr["ChequeNo"] = "";
                }
                dr["ChequeDate"] = chequedate;
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = remarks;
                dr["DeductableAmount"] = Convert.ToString(DeductableAmount);
                dr["DeductablePercentage"] = Convert.ToString(DeductablePercent);
                dr["DeductableLedgerId"] = Convert.ToString(DeductableLedgerID);
                dr["IsCostCenter"] = Convert.ToString(IsCostCenter);
                dr["IsTagInvoice"] = Convert.ToString(IsTagInvoice);
                dr["NonTaxableAmount"] = Convert.ToString(NonTaxableAmount);

                dr["BYDEFAULTAMOUNT"] = ByDefaultAmount;




                dr["TransactionAmount"] = TransactionAmount;


                if (departmentid == "0")  /* 24/06/2019 */
                {
                    dr["DepartmentID"] = "";
                    dr["DepartmentName"] = "";
                }
                else
                {
                    dr["DepartmentID"] = departmentid;
                    dr["DepartmentName"] = departmentname;
                }
                if (departmentname == "")  /* 24/06/2019 */
                {
                    dr["DepartmentName"] = "";
                }

                if (BusinessSegId == "0")  /* 24/06/2019 */
                {
                    dr["BusinessSegId"] = "";
                    dr["BusinessSegName"] = "";
                }
                else
                {
                    dr["BusinessSegId"] = BusinessSegId;
                    dr["BusinessSegName"] = BusinessSegName;
                }
                if (BusinessSegName == "")  /* 24/06/2019 */
                {
                    dr["BusinessSegName"] = "";
                }

                dtvoucher.Rows.Add(dr);
                dtvoucher.AcceptChanges();
                Session["ACC_VOUCHERDETAILS"] = dtvoucher;
                var result = from r in dtvoucher.AsEnumerable()
                             where r.Field<string>("TxnType") == "1"
                             select r;
                DataTable cr = result.CopyToDataTable();
                foreach (DataRow row in cr.Rows)

                    addbudgetlist.Add(new addVoucherTable
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        TxnType = row["TxnType"].ToString(),
                        Amount = row["Amount"].ToString(),
                        BankID = row["BankID"].ToString(),
                        BankName = row["BankName"].ToString(),
                        PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                        PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                        ChequeNo = row["ChequeNo"].ToString(),
                        ChequeDate = row["ChequeDate"].ToString(),
                        IsChequeRealised = row["IsChequeRealised"].ToString(),
                        Remarks = row["Remarks"].ToString(),


                        DeductableAmount = row["DeductableAmount"].ToString(),
                        DeductablePercentage = row["DeductablePercentage"].ToString(),
                        DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                        IsCostCenter = row["IsCostCenter"].ToString(),
                        IsTagInvoice = row["IsTagInvoice"].ToString(),
                        NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                        BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                        TransactionAmount = row["TransactionAmount"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                        taxapplicable = taxapplicable,
                        BusinessSegId = row["BusinessSegId"].ToString(),
                        BusinessSegName = row["BusinessSegName"].ToString(),
                    });

            }
            return Json(addbudgetlist);
        }
        //delete debit voucher
        [HttpPost]
        public JsonResult Bindvouchercr()
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            DataTable dtvoucher = new DataTable();
            dtvoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            var result = from r in dtvoucher.AsEnumerable()
                         where r.Field<string>("TxnType") == "1"
                         select r;
            DataTable cr = result.CopyToDataTable();
            foreach (DataRow row in cr.Rows)

                addbudgetlist.Add(new addVoucherTable
                {
                    GUID = row["GUID"].ToString(),
                    LedgerId = row["LedgerId"].ToString(),
                    LedgerName = row["LedgerName"].ToString(),
                    TxnType = row["TxnType"].ToString(),
                    Amount = row["Amount"].ToString(),
                    BankID = row["BankID"].ToString(),
                    BankName = row["BankName"].ToString(),
                    PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                    PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                    ChequeNo = row["ChequeNo"].ToString(),
                    ChequeDate = row["ChequeDate"].ToString(),
                    IsChequeRealised = row["IsChequeRealised"].ToString(),
                    Remarks = row["Remarks"].ToString(),


                    DeductableAmount = row["DeductableAmount"].ToString(),
                    DeductablePercentage = row["DeductablePercentage"].ToString(),
                    DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                    IsCostCenter = row["IsCostCenter"].ToString(),
                    IsTagInvoice = row["IsTagInvoice"].ToString(),
                    NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                    BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                    TransactionAmount = row["TransactionAmount"].ToString(),
                    DepartmentID = row["DepartmentID"].ToString(),
                    DepartmentName = row["DepartmentName"].ToString(),
                    BusinessSegId = row["BusinessSegId"].ToString(),
                    BusinessSegName = row["BusinessSegName"].ToString(),
                });
            return Json(addbudgetlist);
        }

        [HttpPost]
        public JsonResult deletedr(string ledgerid)
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            //delete budget
            if (Session["BudgetDetails"] != null)
            {
                DataTable dtBudget = (DataTable)Session["BudgetDetails"];
                DataRow[] rows;

                rows = dtBudget.Select("LedgerId = '" + ledgerid + "'"); // UserName is Column Name

                foreach (DataRow r in rows)
                    r.Delete();
                dtBudget.AcceptChanges();
                Session["BudgetDetails"] = dtBudget;
            }
            //delete costcenter
            if (Session["ACC_COSTCENTERDETAILS"] != null)
            {
                DataTable dtcost = (DataTable)Session["ACC_COSTCENTERDETAILS"];
                DataRow[] rowscost;

                rowscost = dtcost.Select("LedgerId = '" + ledgerid + "'"); // UserName is Column Name
                foreach (DataRow r in rowscost)
                    r.Delete();
                dtcost.AcceptChanges();
                Session["ACC_COSTCENTERDETAILS"] = dtcost;
            }
            DataTable dttds = (DataTable)Session["ACC_VOUCHERDETAILS"];
            DataRow[] rowstds;

            rowstds = dttds.Select("DeductableLedgerId = '" + ledgerid + "'"); // UserName is Column Name
            foreach (DataRow r in rowstds)
                r.Delete();
            dttds.AcceptChanges();
            Session["ACC_VOUCHERDETAILS"] = dttds;

            //delete main voucher
            DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];
            DataRow[] rowsvoucher;

            rowsvoucher = dtvoucher.Select("LedgerId  = '" + ledgerid + "'"); // UserName is Column Name
            foreach (DataRow r in rowsvoucher)
                r.Delete();
            dtvoucher.AcceptChanges();
            rowsvoucher = dtvoucher.Select("LedgerId  = '" + ledgerid + "'"); // UserName is Column Name

            if (dtvoucher.Rows.Count > 0)
            {
                Session["ACC_VOUCHERDETAILS"] = dtvoucher;
                var result = from r in dtvoucher.AsEnumerable()
                             where r.Field<string>("TxnType") == "0"
                             select r;
                DataTable debit = result.CopyToDataTable();
                foreach (DataRow row in debit.Rows)

                    addbudgetlist.Add(new addVoucherTable
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        TxnType = row["TxnType"].ToString(),
                        Amount = row["Amount"].ToString(),
                        BankID = row["BankID"].ToString(),
                        BankName = row["BankName"].ToString(),
                        PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                        PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                        ChequeNo = row["ChequeNo"].ToString(),
                        ChequeDate = row["ChequeDate"].ToString(),
                        IsChequeRealised = row["IsChequeRealised"].ToString(),
                        Remarks = row["Remarks"].ToString(),


                        DeductableAmount = row["DeductableAmount"].ToString(),
                        DeductablePercentage = row["DeductablePercentage"].ToString(),
                        DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                        IsCostCenter = row["IsCostCenter"].ToString(),
                        IsTagInvoice = row["IsTagInvoice"].ToString(),
                        NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                        BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                        TransactionAmount = row["TransactionAmount"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                    });
            }

            return Json(addbudgetlist);
        }

        //delete credit voucher
        [HttpPost]
        public JsonResult deletecr(string ledgerid, string tdsapplicable)
        {
            List<addVoucherTable> addbudgetlist = new List<addVoucherTable>();
            //delete budget
            try
            {
                if (Session["BudgetDetails"] != null)
                {
                    DataTable dtBudget = (DataTable)Session["BudgetDetails"];
                    DataRow[] rows;

                    rows = dtBudget.Select("LedgerId = '" + ledgerid + "'"); // UserName is Column Name

                    foreach (DataRow r in rows)
                        r.Delete();
                    dtBudget.AcceptChanges();
                    Session["BudgetDetails"] = dtBudget;
                }
                //delete costcenter
                if (Session["ACC_COSTCENTERDETAILS"] != null)
                {
                    DataTable dtcost = (DataTable)Session["ACC_COSTCENTERDETAILS"];
                    DataRow[] rowscost;

                    rowscost = dtcost.Select("LedgerId = '" + ledgerid + "'"); // UserName is Column Name
                    foreach (DataRow r in rowscost)
                        r.Delete();
                    dtcost.AcceptChanges();
                    Session["ACC_COSTCENTERDETAILS"] = dtcost;
                }

                DataTable dttds = (DataTable)Session["ACC_VOUCHERDETAILS"];
                DataRow[] rowstds;

                rowstds = dttds.Select("DeductableLedgerId = '" + ledgerid + "'"); // UserName is Column Name
                foreach (DataRow r in rowstds)
                    r.Delete();
                dttds.AcceptChanges();
                Session["ACC_VOUCHERDETAILS"] = dttds;

                //delete main voucher
                DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];
                DataRow[] rowsvoucher;

                rowsvoucher = dtvoucher.Select("LedgerId  = '" + ledgerid + "'"); // UserName is Column Name
                foreach (DataRow r in rowsvoucher)
                    r.Delete();
                dtvoucher.AcceptChanges();
                Session["ACC_VOUCHERDETAILS"] = dtvoucher;
                if (dtvoucher.Rows.Count > 0)
                {
                    var result = from r in dtvoucher.AsEnumerable()
                                 where r.Field<string>("TxnType") == "1"
                                 select r;
                    DataTable cr = result.CopyToDataTable();
                    foreach (DataRow row in cr.Rows)

                        addbudgetlist.Add(new addVoucherTable
                        {
                            GUID = row["GUID"].ToString(),
                            LedgerId = row["LedgerId"].ToString(),
                            LedgerName = row["LedgerName"].ToString(),
                            TxnType = row["TxnType"].ToString(),
                            Amount = row["Amount"].ToString(),
                            BankID = row["BankID"].ToString(),
                            BankName = row["BankName"].ToString(),
                            PAYMENTTYPEID = row["PAYMENTTYPEID"].ToString(),
                            PAYMENTTYPENAME = row["PAYMENTTYPENAME"].ToString(),
                            ChequeNo = row["ChequeNo"].ToString(),
                            ChequeDate = row["ChequeDate"].ToString(),
                            IsChequeRealised = row["IsChequeRealised"].ToString(),
                            Remarks = row["Remarks"].ToString(),


                            DeductableAmount = row["DeductableAmount"].ToString(),
                            DeductablePercentage = row["DeductablePercentage"].ToString(),
                            DeductableLedgerId = row["DeductableLedgerId"].ToString(),
                            IsCostCenter = row["IsCostCenter"].ToString(),
                            IsTagInvoice = row["IsTagInvoice"].ToString(),
                            NonTaxableAmount = row["NonTaxableAmount"].ToString(),
                            BYDEFAULTAMOUNT = row["BYDEFAULTAMOUNT"].ToString(),
                            TransactionAmount = row["TransactionAmount"].ToString(),
                            DepartmentID = row["DepartmentID"].ToString(),
                            DepartmentName = row["DepartmentName"].ToString(),
                            taxapplicable = tdsapplicable,
                        });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(addbudgetlist);
        }

        public int DatatableCheck(string Ledgerid, string BankId, string ChequeNo, string TxnType)
        {
            int flag = 0;
            DataTable dtvoucher = (DataTable)Session["ACC_VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                int NumberofRecord = dtvoucher.Select("LedgerId='" + Ledgerid + "' AND ChequeNo='" + ChequeNo + "' AND TxnType='" + TxnType + "'").Length;
                if (NumberofRecord > 0)
                {
                    flag = 1;
                }

            }
            return flag;
        }
        //save data
        public DataTable CreateCheckBankLedgerTable()
        {
            DataTable CheckBankLedgerTable = new DataTable();
            CheckBankLedgerTable.Clear();
            CheckBankLedgerTable.Columns.Add("LedgerId");
            CheckBankLedgerTable.Columns.Add("TxnType");
            CheckBankLedgerTable.Columns.Add("Amount");

            HttpContext.Session["CheckBankLedgerTable"] = CheckBankLedgerTable;
            return CheckBankLedgerTable;
        }
        [HttpPost]
        public JsonResult chkbankdtl(string vouchertypeiod, string cashbank)
        {
            List<messageresponse> msg = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            string xmlDRGST = null;
            string chkbank = "n";
            DataTable dtbank = new DataTable();
            DataTable dt = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            DataRow[] drresult = new DataRow[0];
            int trantype = 0;
            if (vouchertypeiod == "15")
            {
                drresult = dt.Select("TxnType = '1'");
                trantype = 15;
            }
            else

            if (vouchertypeiod == "16")
            {
                drresult = dt.Select("TxnType = '0'");
                trantype = 16;

            }
            CreateCheckBankLedgerTable();
            DataTable CheckBankLedgerTable = (DataTable)HttpContext.Session["CheckBankLedgerTable"];
            foreach (DataRow row in drresult)
            {
                DataRow dr = CheckBankLedgerTable.NewRow();

                dr["LedgerId"] = row["LedgerId"].ToString();
                dr["TxnType"] = row["TxnType"].ToString();
                dr["Amount"] = row["Amount"].ToString();


                CheckBankLedgerTable.Rows.Add(dr);
                CheckBankLedgerTable.AcceptChanges();
            }
            if (CheckBankLedgerTable.Rows.Count > 0)
            {
                xmlDRGST = ConvertDatatableToXML(CheckBankLedgerTable);
                Hashtable hashTable = new Hashtable();
                hashTable.Add("p_InvoiceDebitInvoice", xmlDRGST);
                hashTable.Add("chkbank", cashbank);
                hashTable.Add("vouchertypeid", trantype);
                DButility dbcon = new DButility();
                //  DataTable dtbank = new DataTable();
                dtbank = dbcon.getDataSet("SP_GETBANKLEDGER", hashTable);
                //dtbank = ds.Tables[0];
                if (dtbank.Rows.Count > 0)
                {
                    if (dtbank.Rows[0]["cnt"].ToString() != "0")
                        chkbank = "y";
                }
                msg.Add(new messageresponse
                {
                    response = chkbank



                });
            }
            return Json(msg);
        }
        [HttpPost]
        public JsonResult chkbank(string voucherid)
        {
            List<messageresponse> msg = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            msg = accountcontext.CheckBANKID(voucherid);


            return Json(msg);
        }
        //fileupload
        [HttpPost]
        public JsonResult fileupload(FormCollection formCollection)
        {

            List<fileupload> fileupload = new List<fileupload>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<PurchaseOrder> responseMessage = new List<PurchaseOrder>();
                    HttpFileCollectionBase files = Request.Files;
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();


                    string mode = "1";
                    dt.Columns.Add("filename");

                    DataRow _uploadfile = dt.NewRow();
                    for (int i = 0; i < files.Count; i++)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "AccountsFileUpload/";
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
                        if (Session["UPLOADACCOUNTSFILENAME"] != null)
                        {
                            dt1 = (DataTable)Session["UPLOADACCOUNTSFILENAME"];
                        }
                        _uploadfile["filename"] = fname;

                        dt.Rows.Add(_uploadfile);
                        dt1.Merge(dt);
                        Session["UPLOADACCOUNTSFILENAME"] = dt1;
                        fname = Path.Combine(Server.MapPath("~/AccountsFileUpload/"), fname);
                        file.SaveAs(fname);


                    }
                    foreach (DataRow row in dt1.Rows)

                        fileupload.Add(new fileupload
                        {
                            UPLOADACCOUNTSFILENAME = row["filename"].ToString(),
                            filepath = " ~/AccountsFileUpload /" + row["filename"].ToString(),

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

            if (HttpContext.Session["UPLOADACCOUNTSFILENAME"] != null)
            {
                DataTable dtcost = (DataTable)HttpContext.Session["UPLOADACCOUNTSFILENAME"];
                DataRow[] rowscost;
                rowscost = dtcost.Select("filename = '" + filename + "'"); // UserName is Column Name
                foreach (DataRow r in rowscost)
                    r.Delete();
                dtcost.AcceptChanges();
                HttpContext.Session["UPLOADACCOUNTSFILENAME"] = dtcost;

            }

            return Json("File deleted Successfully!");
        }

        [HttpPost]
        public JsonResult savevoucher(Accountsmodel accountssavesave)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            List<Accresponse> Accresponse = new List<Accresponse>();
            List<Region> Region = new List<Region>();
            //  var userid = _ICacheManager.Get<object>("UserID");
            var userid = accountssavesave.userid;
            //  var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = accountssavesave.finyear;
            if (Session["ACC_VOUCHERDETAILS"] != null || Session["ACC_VOUCHERDETAILS"] !="")
            {
                // this.CreateVoucherTable();

                DataTable dtvoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
                string xml = null;
                string xmlcostcenter = "";
                string xmlDebitCreditGST = null;
                xml = ConvertDatatableToXML(dtvoucher);
                if (HttpContext.Session["ACC_COSTCENTERDETAILS"] != null)
                {
                    DataTable dtcostcenter = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
                    if (dtcostcenter.Rows.Count > 0)
                    {
                        xmlcostcenter = ConvertDatatableToXML(dtcostcenter);
                    }
                }
                if (HttpContext.Session["GSTDETAILS"] != null)
                {
                    DataTable dtdebitcredit_GST = (DataTable)HttpContext.Session["GSTDETAILS"];
                    if (dtdebitcredit_GST.Rows.Count > 0)
                    {
                        xmlDebitCreditGST = ConvertDatatableToXML(dtdebitcredit_GST);

                    }
                }
                if (Session["ACC_INVOICEDETAILS"] == null)
                {
                    this.CreateInvoiceTable();
                }
                DataTable dtinvoice = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS"];

                if (Session["ACC_INVOICEDETAILS_CR"] == null)
                {
                    this.CreateInvoiceTableCR();
                }
                DataTable dtinvoiceCR = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS_CR"];

                string Invoicexml = string.Empty;
                if (dtinvoiceCR.Rows.Count > 0)
                {

                    DataRow[] drrInvoice = dtinvoiceCR.Select("AmtPaid = 0");
                    for (int i = 0; i < drrInvoice.Length; i++)
                    {
                        drrInvoice[i].Delete();
                        dtinvoiceCR.AcceptChanges();
                    }

                    //Invoicexml = ConvertDatatableToXML(dtinvoiceCR);

                    //clsVoucher.VoucherDetailsInsert(voucherid, mode, Invoicexml);
                }
                if (dtinvoice.Rows.Count > 0 && dtinvoiceCR.Rows.Count > 0)
                {
                    dtinvoiceCR.Merge(dtinvoice);
                    dtinvoiceCR.AcceptChanges();
                    Invoicexml = ConvertDatatableToXML(dtinvoiceCR);
                }
                else if (dtinvoice.Rows.Count > 0 && dtinvoiceCR.Rows.Count <= 0)
                {

                    Invoicexml = ConvertDatatableToXML(dtinvoice);
                }
                else if (dtinvoice.Rows.Count <= 0 && dtinvoiceCR.Rows.Count > 0)
                {

                    Invoicexml = ConvertDatatableToXML(dtinvoiceCR);
                }
                Accountscontext accountcontext = new Accountscontext();
                ClsVoucherentry clsvoucher = new ClsVoucherentry();
                responseMessage = accountcontext.vouchersave(accountssavesave, Finyear.ToString(), userid.ToString(), xml, xmlcostcenter, xmlDebitCreditGST).ToList();
                //  string voucherno1    = clsvoucher.InsertBillTagingJournalVoucherDetails(accountssavesave, Finyear.ToString(), userid.ToString(), xml, xmlcostcenter, xmlDebitCreditGST);

                string fileuploadtag = string.Empty;
                string xmlupload = string.Empty;
                DataTable dtupload = new DataTable();
                if (Session["UPLOADACCOUNTSFILENAME"] != null)
                {
                    dtupload = (DataTable)(Session["UPLOADACCOUNTSFILENAME"]);

                    xmlupload = ConvertDatatableToXML(dtupload);
                }
                fileuploadtag = "Y";
                string voucherid = "";
                string voucherno = "";
                int autovoucher = 0;
                foreach (var msg in responseMessage)
                {
                    string VoucherNo = msg.response;
                    TempData["messageid"] = msg.response;
                    String[] voucher = VoucherNo.Split('|');
                    voucherid = voucher[0].Trim();
                    voucherno = voucher[1].Trim();

                }
                Region = accountcontext.PaymentVoucherDetails(voucherid);
                autovoucher = Region.Count;
                Accresponse.Add(new Accresponse
                {
                    voucherid = voucherid,
                    voucherno = voucherno,
                    isautovoucher = autovoucher.ToString()

                });
            }
            else
            {
                Accresponse.Add(new Accresponse
                {
                    voucherid = "0",
                    voucherno = "0",
                    isautovoucher = "0"

                });

            }
            return Json(Accresponse, JsonRequestBehavior.AllowGet);
        }
        // voucher save for depot
        [HttpPost]
        public JsonResult savevoucherdepot(Accountsmodel accountssavesave)
        {
            string messageid1 = "";
            string messagetext1 = "";
            List<messageresponse> responseMessage = new List<messageresponse>();
            List<Accresponse> Accresponse = new List<Accresponse>();
            List<Region> Region = new List<Region>();
            //   var userid = _ICacheManager.Get<object>("UserID");
            var userid = Convert.ToString(Session["UserID"]);
            //  var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = Convert.ToString(Session["FINYEAR"]);
            if (Session["ACC_VOUCHERDETAILS"] == null)
            {
                this.CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            string xml = null;
            string xmlcostcenter = "";
            string xmlDebitCreditGST = null;
            xml = ConvertDatatableToXML(dtvoucher);
            if (HttpContext.Session["ACC_COSTCENTERDETAILS"] != null)
            {
                DataTable dtcostcenter = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
                if (dtcostcenter.Rows.Count > 0)
                {
                    xmlcostcenter = ConvertDatatableToXML(dtcostcenter);
                }
            }
            if (HttpContext.Session["GSTDETAILS"] != null)
            {
                DataTable dtdebitcredit_GST = (DataTable)HttpContext.Session["GSTDETAILS"];
                if (dtdebitcredit_GST.Rows.Count > 0)
                {
                    xmlDebitCreditGST = ConvertDatatableToXML(dtdebitcredit_GST);

                }
            }
            if (Session["ACC_INVOICEDETAILS"] == null)
            {
                this.CreateInvoiceTable();
            }
            DataTable dtinvoice = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS"];

            if (Session["ACC_INVOICEDETAILS_CR"] == null)
            {
                this.CreateInvoiceTableCR();
            }
            DataTable dtinvoiceCR = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS_CR"];

            string Invoicexml = string.Empty;
            if (dtinvoiceCR.Rows.Count > 0)
            {

                DataRow[] drrInvoice = dtinvoiceCR.Select("AmtPaid = 0");
                for (int i = 0; i < drrInvoice.Length; i++)
                {
                    drrInvoice[i].Delete();
                    dtinvoiceCR.AcceptChanges();
                }

                //Invoicexml = ConvertDatatableToXML(dtinvoiceCR);

                //clsVoucher.VoucherDetailsInsert(voucherid, mode, Invoicexml);
            }
            if (dtinvoice.Rows.Count > 0 && dtinvoiceCR.Rows.Count > 0)
            {
                dtinvoiceCR.Merge(dtinvoice);
                dtinvoiceCR.AcceptChanges();
                Invoicexml = ConvertDatatableToXML(dtinvoiceCR);
            }
            else if (dtinvoice.Rows.Count > 0 && dtinvoiceCR.Rows.Count <= 0)
            {

                Invoicexml = ConvertDatatableToXML(dtinvoice);
            }
            else if (dtinvoice.Rows.Count <= 0 && dtinvoiceCR.Rows.Count > 0)
            {

                Invoicexml = ConvertDatatableToXML(dtinvoiceCR);
            }
            Accountscontext accountcontext = new Accountscontext();
            ClsVoucherentry clsvoucher = new ClsVoucherentry();
            responseMessage = accountcontext.vouchersave(accountssavesave, Finyear.ToString(), userid.ToString(), xml, xmlcostcenter, xmlDebitCreditGST).ToList();
            //string voucherno = clsvoucher.InsertBillTagingJournalVoucherDetails(accountssavesave, Finyear.ToString(), userid.ToString(), xml, xmlcostcenter, xmlDebitCreditGST);

            //string fileuploadtag = string.Empty;
            //string xmlupload = string.Empty;
            //DataTable dtupload = new DataTable();
            //if (Session["UPLOADACCOUNTSFILENAME"] != null)
            //{
            //    dtupload = (DataTable)(Session["UPLOADACCOUNTSFILENAME"]);

            //    xmlupload = ConvertDatatableToXML(dtupload);
            //}
            //fileuploadtag = "Y";
            string voucherid = "";
            string voucherno = "";
            //int autovoucher = 0;
            foreach (var msg in responseMessage)
            {
                string VoucherNo = msg.response;
                TempData["messageid"] = msg.response;
                String[] voucher = VoucherNo.Split('|');
                voucherid = voucher[0].Trim();
                voucherno = voucher[1].Trim();

            }
            //Region = accountcontext.PaymentVoucherDetails(voucherid);
            //autovoucher = Region.Count;
            Accresponse.Add(new Accresponse
            {
                voucherid = voucherid,
                voucherno = voucherno,
                isautovoucher = ""

            });
            return Json(Accresponse, JsonRequestBehavior.AllowGet);
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
        #region gst related
        [HttpPost]
        public JsonResult gstpopup()
        {
            string popup = "n";
            string xmlDRGST = null;
            DataTable dtGST = new DataTable();
            DataTable dtGST1 = new DataTable();
            int FlagGST = 0;
            int FlagGSTOLDPOPUP = 0;
            DataTable dt = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            DataRow[] drresult = dt.Select("TxnType = '0'");
            CreateCheckGSTLedgerTable();
            DataTable dtCheckGSTLedgerTable = (DataTable)HttpContext.Session["CheckGSTLedgerTable"];
            foreach (DataRow row in drresult)
            {
                DataRow dr = dtCheckGSTLedgerTable.NewRow();

                dr["LedgerId"] = row["LedgerId"].ToString();
                dr["DrCR"] = "Dr";

                dtCheckGSTLedgerTable.Rows.Add(dr);
                dtCheckGSTLedgerTable.AcceptChanges();
            }

            List<gstpopup> gstpopup = new List<gstpopup>();
            List<messageresponse> msg = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            HttpContext.Session["CheckGSTLedgerTable"] = dtCheckGSTLedgerTable;
            if (dtCheckGSTLedgerTable.Rows.Count > 0)
            {
                xmlDRGST = ConvertDatatableToXML(dtCheckGSTLedgerTable);

                gstpopup = accountcontext.CheckGSTDr(xmlDRGST);
                foreach (var gstdt in gstpopup)
                {
                    int count = Int32.Parse(Convert.ToString(gstdt.gstcount));
                    if (count > 0)
                    {
                        FlagGST = FlagGST + 1;
                        FlagGSTOLDPOPUP = Convert.ToInt32(Convert.ToString(gstdt.oldpopup));
                    }
                }


            }

            DataRow[] crresult = dt.Select("TxnType = '1'");
            CreateCheckGSTLedgerTable();
            DataTable dtCheckGSTLedgerTableCr = (DataTable)HttpContext.Session["CheckGSTLedgerTable"];
            foreach (DataRow row in crresult)
            {
                DataRow dr = dtCheckGSTLedgerTableCr.NewRow();

                dr["LedgerId"] = row["LedgerId"].ToString();
                dr["DrCR"] = "Dr";

                dtCheckGSTLedgerTableCr.Rows.Add(dr);
                dtCheckGSTLedgerTableCr.AcceptChanges();
            }
            HttpContext.Session["CheckGSTLedgerTable"] = dtCheckGSTLedgerTableCr;

            if (dtCheckGSTLedgerTableCr.Rows.Count > 0)
            {
                xmlDRGST = ConvertDatatableToXML(dtCheckGSTLedgerTableCr);
                gstpopup = accountcontext.CheckGSTDr(xmlDRGST);
                // dtGST1 = clsVoucherGST.CheckGSTDr(xmlDRGST);

                foreach (var gstdt1 in gstpopup)
                {
                    int count = Int32.Parse(Convert.ToString(gstdt1.gstcount));
                    if (count > 0)
                    {
                        FlagGST = FlagGST + 1;
                        FlagGSTOLDPOPUP = Int32.Parse(Convert.ToString(gstdt1.oldpopup));
                    }
                }
            }
            if (FlagGST == 1)
            {
                if (FlagGSTOLDPOPUP > 0)
                {
                    popup = "o";
                    HttpContext.Session["GSTPopup"] = "Old";

                }
                else
                {
                    popup = "n";
                    HttpContext.Session["GSTPopup"] = "New";

                }
            }
            msg.Add(new messageresponse
            {
                response = popup



            });

            return Json(msg);
        }
        [HttpPost]
        public JsonResult gstchk()
        {
            string popup = "n";
            string xmlDRGST = null;
            DataTable dtGST = new DataTable();
            DataTable dtGST1 = new DataTable();
            int FlagGST = 0;
            int FlagGSTOLDPOPUP = 0;
            DataTable dt = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            DataRow[] drresult = dt.Select("TxnType = '0'");
            CreateCheckGSTLedgerTable();
            DataTable dtCheckGSTLedgerTable = (DataTable)HttpContext.Session["CheckGSTLedgerTable"];
            foreach (DataRow row in drresult)
            {
                DataRow dr = dtCheckGSTLedgerTable.NewRow();

                dr["LedgerId"] = row["LedgerId"].ToString();
                dr["DrCR"] = "Dr";

                dtCheckGSTLedgerTable.Rows.Add(dr);
                dtCheckGSTLedgerTable.AcceptChanges();
            }

            List<gstpopup> gstpopup = new List<gstpopup>();
            List<messageresponse> msg = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            HttpContext.Session["CheckGSTLedgerTable"] = dtCheckGSTLedgerTable;
            if (dtCheckGSTLedgerTable.Rows.Count > 0)
            {
                xmlDRGST = ConvertDatatableToXML(dtCheckGSTLedgerTable);

                gstpopup = accountcontext.CheckGSTDr(xmlDRGST);
                foreach (var gstdt in gstpopup)
                {
                    int count = Int32.Parse(Convert.ToString(gstdt.gstcount));
                    if (count > 0)
                    {
                        FlagGST = FlagGST + 1;
                        FlagGSTOLDPOPUP = Convert.ToInt32(Convert.ToString(gstdt.oldpopup));
                    }
                }


            }

            DataRow[] crresult = dt.Select("TxnType = '1'");
            CreateCheckGSTLedgerTable();
            DataTable dtCheckGSTLedgerTableCr = (DataTable)HttpContext.Session["CheckGSTLedgerTable"];
            foreach (DataRow row in crresult)
            {
                DataRow dr = dtCheckGSTLedgerTableCr.NewRow();

                dr["LedgerId"] = row["LedgerId"].ToString();
                dr["DrCR"] = "Dr";

                dtCheckGSTLedgerTableCr.Rows.Add(dr);
                dtCheckGSTLedgerTableCr.AcceptChanges();
            }
            HttpContext.Session["CheckGSTLedgerTable"] = dtCheckGSTLedgerTableCr;

            if (dtCheckGSTLedgerTableCr.Rows.Count > 0)
            {
                xmlDRGST = ConvertDatatableToXML(dtCheckGSTLedgerTableCr);
                gstpopup = accountcontext.CheckGSTDr(xmlDRGST);
                // dtGST1 = clsVoucherGST.CheckGSTDr(xmlDRGST);

                foreach (var gstdt1 in gstpopup)
                {
                    int count = Int32.Parse(Convert.ToString(gstdt1.gstcount));
                    if (count > 0)
                    {
                        FlagGST = FlagGST + 1;
                        FlagGSTOLDPOPUP = Convert.ToInt32(Convert.ToString(gstdt1.oldpopup));
                    }
                }
            }

            msg.Add(new messageresponse
            {
                response = FlagGST.ToString()



            });

            return Json(msg);
        }
        [HttpPost]


        public JsonResult chkgstdetail()
        {
            string res = "0";
            List<messageresponse> msg = new List<messageresponse>();
            //  DataTable dt = (DataTable)HttpContext.Session["GSTDETAILS"];
            if (HttpContext.Session["GSTDETAILS"] != null)


            {
                res = "1";
            }
            else
            {
                res = "0";
            }
            msg.Add(new messageresponse
            {
                response = res



            });
            return Json(msg);
        }
        [HttpPost]
        public JsonResult BindGSTGroup()
        {

            List<GSTGroup> GSTGroup = new List<GSTGroup>();
            Accountscontext accountcontext = new Accountscontext();
            GSTGroup = accountcontext.BindGSTGroup();
            return Json(GSTGroup);
        }
        [HttpPost]
        public JsonResult BindDCJ_Party(string djgroup)
        {

            List<thirdpartyvendor> thirdpartyvendor = new List<thirdpartyvendor>();
            Accountscontext accountcontext = new Accountscontext();
            thirdpartyvendor = accountcontext.BindDCJ_Party(djgroup);
            return Json(thirdpartyvendor);
        }
        [HttpPost]
        public JsonResult BindDCJ_GstNo(string party, string statename)
        {

            List<GSTNo> gstn = new List<GSTNo>();
            Accountscontext accountcontext = new Accountscontext();
            gstn = accountcontext.BindDCJ_GstNo(party, statename);
            return Json(gstn);
        }
        [HttpPost]
        public JsonResult BindTaxType(string partyname, string stateid, string regionid)
        {

            List<taxpercentage> taxpercentage = new List<taxpercentage>();
            Accountscontext accountcontext = new Accountscontext();
            taxpercentage = accountcontext.BindTaxType(partyname, stateid, regionid);
            return Json(taxpercentage);
        }
        [HttpPost]
        public JsonResult BindDCJ_TaxTypeName(string TaxTypeID)
        {

            List<taxtype> taxpercentage = new List<taxtype>();
            Accountscontext accountcontext = new Accountscontext();
            taxpercentage = accountcontext.BindDCJ_TaxTypeName(TaxTypeID);
            return Json(taxpercentage);
        }
        [HttpPost]
        public JsonResult BindTaxPercentage(string partyname, string stateid, string regionid)
        {

            List<taxpercentage> taxpercentage = new List<taxpercentage>();
            Accountscontext accountcontext = new Accountscontext();
            taxpercentage = accountcontext.BindTaxPercentage(partyname, stateid, regionid);
            return Json(taxpercentage);
        }
        [HttpPost]
        public JsonResult Gettaxtype(string partyid, string stateid, string depoid)
        {

             List<taxtype> taxtype = new List<taxtype>();
            Accountscontext accountcontext = new Accountscontext();
            taxtype = accountcontext.Gettaxtype(partyid, stateid, depoid);
            return Json(taxtype);
        }
        [HttpPost]
        public JsonResult CheckInvoiceNo(string PartyID, string InvoiceNo)
        {

            //   var Finyear = _ICacheManager.Get<object>("FINYEAR");
            var Finyear = Convert.ToString(Session["FINYEAR"]);
            List<messageresponse> messageresponse = new List<messageresponse>();
            Accountscontext accountcontext = new Accountscontext();
            messageresponse = accountcontext.CheckInvoiceNo(PartyID, Finyear.ToString(), InvoiceNo);
            return Json(messageresponse);
        }
        public DataTable CreateCheckGSTLedgerTable()
        {
            DataTable CheckGSTLedgerTable = new DataTable();
            CheckGSTLedgerTable.Clear();
            CheckGSTLedgerTable.Columns.Add("LedgerId");
            CheckGSTLedgerTable.Columns.Add("DrCR");

            HttpContext.Session["CheckGSTLedgerTable"] = CheckGSTLedgerTable;
            return CheckGSTLedgerTable;
        }
        [HttpPost]
        public JsonResult addgstnew(string GroupId, string PartyID, string InvoiceNo,
      string InvoiceDate,
      string Taxable,
      string TaxableValue,
      string HSNCode,
      string PartyTrade,
      string StateID,
      string GSTNo,
      string TaxTypeID,
      string TaxType,
      string TaxAmount,
      string NetAmount,
      string Taxable1,
      string TaxAmount1,
      string TaxTypeID1,
      string Taxtype1,
      string igst,
      string roundoff,
     string txtDCJ_NonTaxableAmount,
     string PlaceOfSupply
  )
        {
            List<addgsttable> taxabletable = new List<addgsttable>();
            string res = "";
            if (HttpContext.Session["GSTDETAILS"] == null)
            {
                //HttpContext.Current.Session["GSTDETAILS"] = null;
                CreateGSTTable();
            }
            DataTable dtvoucherGST = (DataTable)HttpContext.Session["GSTDETAILS"];
            try
            {

                DataRow dr = dtvoucherGST.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["GroupId"] = GroupId;
                dr["PartyID"] = PartyID;
                dr["InvoiceNo"] = InvoiceNo;
                dr["InvoiceDate"] = InvoiceDate;
                dr["Taxable"] = Taxable;
                dr["TaxableValue"] = TaxableValue;
                dr["HSNCode"] = HSNCode;
                dr["PartyTrade"] = PartyTrade;
                dr["StateID"] = StateID;
                dr["GSTNo"] = GSTNo;
                dr["TaxTypeID"] = TaxTypeID;
                dr["TaxType"] = TaxType;
                dr["TaxAmount"] = TaxAmount;
                dr["NetAmount"] = NetAmount;
                dr["OldNew"] = "New";
                if (Taxable1 == "")
                {
                    dr["Taxable1"] = "0";
                }
                else
                {
                    dr["Taxable1"] = Taxable1;
                }
                if (TaxAmount1 == "")
                {
                    dr["TaxAmount1"] = "0";
                }
                else
                {
                    dr["TaxAmount1"] = TaxAmount1;
                }
                dr["TaxTypeID1"] = TaxTypeID1;
                dr["TaxType1"] = Taxtype1;
                dr["ISIGST"] = igst;
                dr["RoundOff"] = roundoff;
                if (txtDCJ_NonTaxableAmount == "")
                {
                    dr["NonTaxableAmountGST"] = "0";
                }
                else
                {
                    dr["NonTaxableAmountGST"] = txtDCJ_NonTaxableAmount;
                }

                dr["PlaceOfSupply"] = PlaceOfSupply;
                dtvoucherGST.Rows.Add(dr);
                dtvoucherGST.AcceptChanges();

                HttpContext.Session["GSTDETAILS"] = dtvoucherGST;
                foreach (DataRow row in dtvoucherGST.Rows)

                    taxabletable.Add(new addgsttable
                    {
                        GUID = Convert.ToString(row["GUID"]),
                        GroupId = Convert.ToString(row["GroupId"]),
                        PartyID = Convert.ToString(row["PartyID"]),
                        InvoiceNo = Convert.ToString(row["InvoiceNo"]),
                        InvoiceDate = Convert.ToString(row["InvoiceDate"]),
                        Taxable = Convert.ToString(row["Taxable"]),
                        TaxableValue = Convert.ToString(row["TaxableValue"]),
                        HSNCode = Convert.ToString(row["HSNCode"]),
                        PartyTrade = Convert.ToString(row["PartyTrade"]),
                        StateID = Convert.ToString(row["StateID"]),
                        GSTNo = Convert.ToString(row["GSTNo"]),
                        TaxTypeID = Convert.ToString(row["TaxTypeID"]),
                        TaxType = Convert.ToString(row["TaxType"]),
                        TaxAmount = Convert.ToString(row["TaxAmount"]),
                        NetAmount = Convert.ToString(row["NetAmount"]),
                        Taxable1 = Convert.ToString(row["Taxable1"]),
                        TaxAmount1 = Convert.ToString(row["TaxAmount1"]),
                        TaxTypeID1 = Convert.ToString(row["TaxTypeID1"]),
                        TaxType1 = Convert.ToString(row["TaxType1"]),
                        ISIGST = Convert.ToString(row["ISIGST"]),




                    });
            }
            catch (Exception ex)
            {

            }
            return Json(taxabletable);
        }
        [HttpPost]
        public JsonResult delnewgst(string guid)
        {
            List<addgsttable> taxabletable = new List<addgsttable>();
            DataTable dtvoucher = (DataTable)HttpContext.Session["GSTDETAILS"];
            DataRow[] rowsvoucher;

            rowsvoucher = dtvoucher.Select("GUID  = '" + guid + "'"); // UserName is Column Name
            foreach (DataRow r in rowsvoucher)
                r.Delete();
            dtvoucher.AcceptChanges();
            HttpContext.Session["GSTDETAILS"] = dtvoucher;
            foreach (DataRow dr in dtvoucher.Rows)

                taxabletable.Add(new addgsttable
                {
                    GUID = Convert.ToString(dr["GUID"]),
                    GroupId = Convert.ToString(dr["GroupId"]),
                    PartyID = Convert.ToString(dr["PartyID"]),
                    InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                    InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                    Taxable = Convert.ToString(dr["Taxable"]),
                    TaxableValue = Convert.ToString(dr["TaxableValue"]),
                    HSNCode = Convert.ToString(dr["HSNCode"]),
                    PartyTrade = Convert.ToString(dr["PartyTrade"]),
                    StateID = Convert.ToString(dr["StateID"]),
                    GSTNo = Convert.ToString(dr["GSTNo"]),
                    TaxTypeID = Convert.ToString(dr["TaxTypeID"]),
                    TaxType = Convert.ToString(dr["TaxType"]),
                    TaxAmount = Convert.ToString(dr["TaxAmount"]),
                    NetAmount = Convert.ToString(dr["NetAmount"]),
                    Taxable1 = Convert.ToString(dr["Taxable1"]),
                    TaxAmount1 = Convert.ToString(dr["TaxAmount1"]),
                    TaxTypeID1 = Convert.ToString(dr["TaxTypeID1"]),
                    TaxType1 = Convert.ToString(dr["TaxType1"]),
                    ISIGST = Convert.ToString(dr["ISIGST"]),




                });
            return Json(taxabletable);
        }

        #endregion
        //tds
        [HttpPost]
        public JsonResult TaxApplicable(string AccountID, string AccountName, string AccountType, string Amount, string BranchID, string voucherdate, string voucherid, string drcr,string finyr)
        {
            //  var finyr= _ICacheManager.Get<object>("FINYEAR"); ;
            //var finyr = Convert.ToString(Session["FINYEAR"]);
               decimal billamt = decimal.Parse(Amount);
            decimal amt = decimal.Parse(AccountType);
            List<tdsapplicable> tdsapplicable = new List<tdsapplicable>();
            Accountscontext accountcontext = new Accountscontext();
            tdsapplicable = accountcontext.TaxApplicable(AccountID, voucherdate, finyr.ToString(), billamt, BranchID, voucherid, drcr, amt);
            return Json(tdsapplicable);
        }
        public JsonResult TaxAction(string taxid)
        {


            List<taxaction> taxaction = new List<taxaction>();
            Accountscontext accountcontext = new Accountscontext();
            taxaction = accountcontext.TaxAction(taxid);
            return Json(taxaction);
        }

        #region  cost center
        [HttpPost]
        public JsonResult costapplicable(string AccountID,string finyr)
        {
            //  var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            //var finyr = Convert.ToString(Session["FINYEAR"]);
            List<responseint> responseint = new List<responseint>();
            Accountscontext accountcontext = new Accountscontext();
            responseint = accountcontext.costapplicable(AccountID);
            return Json(responseint);
        }
        [HttpPost]
        public JsonResult BindCostCenterCatagory()
        {


            List<costcategort> costcategort = new List<costcategort>();
            Accountscontext accountcontext = new Accountscontext();
            costcategort = accountcontext.BindCostCenterCatagory();
            return Json(costcategort);
        }
        [HttpPost]
        public JsonResult BindBrand()
        {


            List<Brand> Brand = new List<Brand>();
            Accountscontext accountcontext = new Accountscontext();
            Brand = accountcontext.BindBrand();
            return Json(Brand);
        }
        [HttpPost]
        public JsonResult BindProduct()
        {


            List<Product> Product = new List<Product>();
            Accountscontext accountcontext = new Accountscontext();
            Product = accountcontext.BindProduct();
            return Json(Product);
        }
        [HttpPost]
        public JsonResult BindDepartment()
        {


            List<Departrment> Departrment = new List<Departrment>();
            Accountscontext accountcontext = new Accountscontext();
            Departrment = accountcontext.BindDepartment();
            return Json(Departrment);
        }
        [HttpPost]
        public JsonResult BindCostCenter()
        {


            List<costcenter> costcenter = new List<costcenter>();
            Accountscontext accountcontext = new Accountscontext();
            costcenter = accountcontext.BindCostCenter();
            return Json(costcenter);
        }
        [HttpPost]
        public JsonResult addcostcenterdtl(string ledgerid, string CostCatagoryID,
        string BrandId,
        string ProductId, string DepartmentId,
        string BranchID,
        string CostCenterID,
        string CostCenterName,
        string CostCatagoryName,
        string BrandName,
        string ProductName,
        string DepartmentName,
        string FromDate, string ToDate,
        string BranchName,
        string Amount,
        string txntype, string ledgercostcentername)
        {
            List<addcostcenter> addbudgetlist = new List<addcostcenter>();
            if (HttpContext.Session["ACC_COSTCENTERDETAILS"] == null)
            {
                CreateCostCenterTable();
            }
            DataTable dtcost = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
            DataRow dr = dtcost.NewRow();
            try
            {
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = ledgerid;
                dr["LedgerName"] = ledgercostcentername;
                dr["CostCatagoryID"] = CostCatagoryID;
                dr["CostCatagoryName"] = CostCatagoryName;
                dr["CostCenterID"] = CostCenterID;
                dr["CostCenterName"] = CostCenterName;
                dr["BranchID"] = BranchID;
                dr["BranchName"] = BranchName;
                dr["amount"] = Amount;
                dr["BrandID"] = BrandId;
                dr["BrandName"] = BrandName;
                dr["ProductID"] = ProductId;
                dr["ProductName"] = ProductName;
                dr["DepartmentID"] = DepartmentId;
                dr["DepartmentName"] = DepartmentName;
                dr["FromDate"] = FromDate;
                dr["ToDate"] = ToDate;
                dr["Narration"] = "";
                dr["TxnType"] = txntype;
                dr["BudgetApplicableSubComponentDeptID"] = "0";
                dr["DepartmentalComponenetID"] = "0";
                dr["DepartmentalComponenetName"] = "0";
                dr["SubComponenetID"] = "0";
                dr["SubComponenetName"] = "0";
                dtcost.Rows.Add(dr);
                dtcost.AcceptChanges();
                HttpContext.Session["ACC_COSTCENTERDETAILS"] = dtcost;
                var result = from r in dtcost.AsEnumerable()
                             where r.Field<string>("LedgerId") == ledgerid
                             select r;
                DataTable dtResult = result.CopyToDataTable();
                foreach (DataRow row in dtResult.Rows)

                    addbudgetlist.Add(new addcostcenter
                    {
                        GUID = row["GUID"].ToString(),
                        LedgerId = row["LedgerId"].ToString(),
                        LedgerName = row["LedgerName"].ToString(),
                        CostCatagoryID = row["CostCatagoryID"].ToString(),
                        CostCatagoryName = row["CostCatagoryName"].ToString(),
                        CostCenterID = row["CostCenterID"].ToString(),
                        CostCenterName = row["CostCenterName"].ToString(),
                        BranchID = row["BranchID"].ToString(),
                        BranchName = row["BranchName"].ToString(),
                        amount = row["amount"].ToString(),
                        BrandID = row["BrandID"].ToString(),
                        BrandName = row["BrandName"].ToString(),
                        ProductID = row["ProductID"].ToString(),
                        ProductName = row["ProductName"].ToString(),
                        DepartmentID = row["DepartmentID"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString(),
                        FromDate = row["FromDate"].ToString(),
                        ToDate = row["ToDate"].ToString(),
                        Narration = row["Narration"].ToString(),
                        TxnType = row["TxnType"].ToString(),

                    });
            }
            catch (Exception ex)
            {

            }
            return Json(addbudgetlist);
        }
        [HttpPost]
        public JsonResult viewcostcenter(string ledgerid)
        {
            List<addcostcenter> addbudgetlist = new List<addcostcenter>();
            DataTable dtcost = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
            var result = from r in dtcost.AsEnumerable()
                         where r.Field<string>("LedgerId") == ledgerid
                         select r;
            DataTable dtResult = result.CopyToDataTable();
            foreach (DataRow row in dtResult.Rows)

                addbudgetlist.Add(new addcostcenter
                {
                    GUID = row["GUID"].ToString(),
                    LedgerId = row["LedgerId"].ToString(),
                    LedgerName = row["LedgerName"].ToString(),
                    CostCatagoryID = row["CostCatagoryID"].ToString(),
                    CostCatagoryName = row["CostCatagoryName"].ToString(),
                    CostCenterID = row["CostCenterID"].ToString(),
                    CostCenterName = row["CostCenterName"].ToString(),
                    BranchID = row["BranchID"].ToString(),
                    BranchName = row["BranchName"].ToString(),
                    amount = row["amount"].ToString(),
                    BrandID = row["BrandID"].ToString(),
                    BrandName = row["BrandName"].ToString(),
                    ProductID = row["ProductID"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    DepartmentID = row["DepartmentID"].ToString(),
                    DepartmentName = row["DepartmentName"].ToString(),
                    FromDate = row["FromDate"].ToString(),
                    ToDate = row["ToDate"].ToString(),
                    Narration = row["Narration"].ToString(),
                    TxnType = row["TxnType"].ToString(),

                });
            //DataRow[] rows;

            //rows = dtcost.Select("LedgerId = '" + ledgerid + "'"); // UserName is Column Name

            //foreach (DataRow r in rows)
            //    r.Delete();
            //dtcost.AcceptChanges();
            //HttpContext.Current.Session["ACC_COSTCENTERDETAILS"] = dtcost;

            return Json(addbudgetlist);
        }
        [HttpPost]
        public JsonResult deletecostcenter(string guid, string ledgerid)
        {
            List<addcostcenter> addbudgetlist = new List<addcostcenter>();
            if (HttpContext.Session["ACC_COSTCENTERDETAILS"] != null)
            {
                DataTable dtcost = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
                DataRow[] rowscost;
                rowscost = dtcost.Select("GUID = '" + guid + "'"); // UserName is Column Name
                foreach (DataRow r in rowscost)
                    r.Delete();
                dtcost.AcceptChanges();
                HttpContext.Session["ACC_COSTCENTERDETAILS"] = dtcost;
                if (dtcost.Rows.Count > 0)
                {
                    var result = from r in dtcost.AsEnumerable()
                                 where r.Field<string>("LedgerId") == ledgerid
                                 select r;
                    DataTable dtResult = result.CopyToDataTable();
                    foreach (DataRow row in dtResult.Rows)

                        addbudgetlist.Add(new addcostcenter
                        {
                            GUID = row["GUID"].ToString(),
                            LedgerId = row["LedgerId"].ToString(),
                            LedgerName = row["LedgerName"].ToString(),
                            CostCatagoryID = row["CostCatagoryID"].ToString(),
                            CostCatagoryName = row["CostCatagoryName"].ToString(),
                            CostCenterID = row["CostCenterID"].ToString(),
                            CostCenterName = row["CostCenterName"].ToString(),
                            BranchID = row["BranchID"].ToString(),
                            BranchName = row["BranchName"].ToString(),
                            amount = row["amount"].ToString(),
                            BrandID = row["BrandID"].ToString(),
                            BrandName = row["BrandName"].ToString(),
                            ProductID = row["ProductID"].ToString(),
                            ProductName = row["ProductName"].ToString(),
                            DepartmentID = row["DepartmentID"].ToString(),
                            DepartmentName = row["DepartmentName"].ToString(),
                            FromDate = row["FromDate"].ToString(),
                            ToDate = row["ToDate"].ToString(),
                            Narration = row["Narration"].ToString(),
                            TxnType = row["TxnType"].ToString(),

                        });
                }
            }

            return Json(addbudgetlist);
        }
        #endregion
        #region=== budget
        [HttpPost]
        public JsonResult checkwxpenseledgerornot(string acctypedr)
        {
            string res = "0";
            // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            var finyr = Convert.ToString(Session["FINYEAR"]);
            List<groupcode> responseint = new List<groupcode>();
            Accountscontext accountcontext = new Accountscontext();
            responseint = accountcontext.checkwxpenseledgerornot(acctypedr);
            List<messageresponse> msg = new List<messageresponse>();
            if (responseint.Count == 0)
            {
                res = "0";
            }
            else
            {
                res = "1";
            }
            msg.Add(new messageresponse
            {
                response = res



            });
            return Json(msg);
        }
        [HttpPost]
        public JsonResult addbudgetdtl(string acctypedr, string acnamedr, string deptid, string deptname)
        {
            List<addbudget> addbudgetlist = new List<addbudget>();
            string flag = "0";
            if (HttpContext.Session["BudgetDetails"] == null)
            {
                CreateBudgetTable();
            }

            DataTable dtBudget = (DataTable)HttpContext.Session["BudgetDetails"];
            if (dtBudget.Rows.Count > 0)
            {
                int NumberofRecord = dtBudget.Select("LedgerId='" + acctypedr + "'").Length;
                if (NumberofRecord > 0)
                {
                    flag = "1";
                }
            }

            DataRow dr = dtBudget.NewRow();

            dr["LedgerId"] = acctypedr;
            dr["LedgerName"] = acnamedr;
            dr["DepartmentId"] = deptid;
            dr["DepartmentName"] = deptname;
            dtBudget.Rows.Add(dr);
            dtBudget.AcceptChanges();
            HttpContext.Session["BudgetDetails"] = dtBudget;

            foreach (DataRow row in dtBudget.Rows)

                addbudgetlist.Add(new addbudget
                {
                    LedgerId = row["LedgerId"].ToString(),
                    LedgerName = row["LedgerName"].ToString(),
                    DepartmentId = row["DepartmentId"].ToString(),
                    DepartmentName = row["DepartmentName"].ToString(),

                });




            return Json(addbudgetlist);
        }
        [HttpPost]
        public JsonResult checkbudgetledgerexists(string accdr)
        {
            // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            var finyr = Convert.ToString(Session["FINYEAR"]);
            List<messageresponse> msg = new List<messageresponse>();
            string res = "";
            DataTable dtBudget = (DataTable)HttpContext.Session["BudgetDetails"];
            if (dtBudget.Rows.Count > 0)
            {
                int NumberofRecord = dtBudget.Select("LedgerId='" + accdr + "'").Length;
                if (NumberofRecord > 0)
                {
                    res = "This account already exists!";


                }
            }
            msg.Add(new messageresponse
            {
                response = res



            });
            return Json(msg);
        }
        public class addbudget
        {
            public string LedgerId { get; set; }
            public string LedgerName { get; set; }
            public string DepartmentId { get; set; }
            public string DepartmentName { get; set; }
        }
        #endregion
        #region===edit and list
        [HttpPost]
        public JsonResult BindVoucherDetails(string FromDate, string ToDate, string VoucherID, string DepotID, string checker, string IsMTClaim,string finyr,string userid)
        {
            // var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
            //var finyr = Convert.ToString(Session["FINYEAR"]);
            //   var userid = _ICacheManager.Get<object>("UserID");
         //   var userid = Convert.ToString(Session["UserID"]);
            List<voucherlist> voucherlist = new List<voucherlist>();
            Accountscontext accountcontext = new Accountscontext();
            voucherlist = accountcontext.BindVoucherDetails(FromDate, ToDate, VoucherID, DepotID, checker, userid.ToString(), finyr.ToString(), IsMTClaim);
           // return Json(voucherlist);
            return new JsonResult() { Data = voucherlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        [HttpPost]
        public JsonResult deletevoucher(string voucherid, string voucherapproived, string dayend, string tdsrelated)
        {
            string res = "";
            int deleteflag = 0;
            //    var userid = _ICacheManager.Get<object>("UserID");
            var userid = Convert.ToString(Session["UserID"]);
            List<messageresponse> msg = new List<messageresponse>();
            List<messageresponse> msg1 = new List<messageresponse>();
            List<messageresponse> delflag = new List<messageresponse>();
            List<responseint> flag = new List<responseint>();
            Accountscontext accountcontext = new Accountscontext();

            //string ip_address = Request.UserHostAddress.ToString().Trim();
            string ip_address = "";
            int Flag = 0;
            /* Add Logic : When create auto Journal from Payment then not delete journal on 18/12/2018 */
            msg = accountcontext.CheckAutoPopup1(voucherid);
            foreach (var vr in msg)
            {
                Flag = Int32.Parse(Convert.ToString(vr.response));
            }
            /* TDS related voucher are not deleted on 16/03/2019 */
            //   var finyear = _ICacheManager.Get<object>("FINYEAR");
            var finyear = Convert.ToString(Session["FINYEAR"]);

            if (tdsrelated == "Y" && finyear.ToString() == "2019-2020")
            {
                res = "Voucher is TDS related, you can't delete</font> this!";
            }
            else if (Flag > 0)
            {
                res = "Its auto generate Journal , If you want to delete this, Please go to payment voucher.";
            }
            else
            {

                if (dayend.Trim() == "Yes")
                {
                    res = "Voucher status dayend, you can't delete this";
                }
                else if (voucherapproived.Trim() == "No" || (voucherapproived.Trim() == "Reject"))
                {
                    /* Add logic : when delete the record then insert in backup history  By D.Mondal on 26/11/2018 */
                    delflag = accountcontext.deletevoucher(voucherid.Trim(), User.ToString());

                    foreach (var del in delflag)
                    {
                        deleteflag = Int32.Parse(Convert.ToString(del.response));
                    }
                    if (deleteflag > 0)
                    {
                        res = "Voucher deleted successfully!";

                    }
                    else
                    {
                        res = "Voucher deleted unsuccessful!";
                    }
                }
                else
                {
                    res = "Voucher already apporved, you can't delete this!";
                }

            }
            msg1.Add(new messageresponse
            {
                response = res



            });
            return Json(msg1);
        }
        public JsonResult deletevoucher(string voucherid)
        {
            List<messageresponse> msg = new List<messageresponse>();
            List<messageresponse> msg1 = new List<messageresponse>();
            List<messageresponse> delflag = new List<messageresponse>();
            msg1.Add(new messageresponse
            {
                response = ""



            });
            return Json(msg1);
        }
        [HttpPost]
        public JsonResult editdtl(string voucherid)
        {
            List<Acchdr> header = new List<Acchdr>();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("p_AccEntryId", voucherid);
            DButility dbcon = new DButility();
            DataSet ds = new DataSet();
            ds = dbcon.SysFetchDataInDataSet("[SP_ACC_VOUCHERDETAILS]", hashTable);
            //  ds = clsVoucher.VoucherDetails(voucherid);
            string IsGSTVoucher = "";
            string VoucherTypeID = "";
            string BranchID = "";
            string Date = "";
            string Mode = "";
            string Narration = "";
            string RejectionNote = "";
            string VoucherNo1 = "";
            string IsPaymentType = "";
            string PaymentParty = "";
            string MTClaimLedgerID = "";
            string MTClaimLedgerName = "";
            string MTClaimChecker1 = "";
            string MTClaimChecker2 = "";
            string MTClaimChecker3 = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGSTVoucher = Convert.ToString(ds.Tables[0].Rows[0]["IsGSTVoucher"]);
                VoucherTypeID = Convert.ToString(ds.Tables[0].Rows[0]["VoucherTypeID"]);
                BranchID = Convert.ToString(ds.Tables[0].Rows[0]["BranchID"]);
                Date = Convert.ToString(ds.Tables[0].Rows[0]["Date"]);
                Mode = Convert.ToString(ds.Tables[0].Rows[0]["Mode"]);
                Narration = Convert.ToString(ds.Tables[0].Rows[0]["Narration"]);
                RejectionNote = Convert.ToString(ds.Tables[0].Rows[0]["RejectionNote"]);
                VoucherNo1 = Convert.ToString(ds.Tables[0].Rows[0]["VoucherNo"]);
                PaymentParty = Convert.ToString(ds.Tables[0].Rows[0]["PaymentParty"]);
                IsPaymentType = Convert.ToString(ds.Tables[0].Rows[0]["IsPaymentType"]);
                MTClaimLedgerID = Convert.ToString(ds.Tables[0].Rows[0]["MTClaimLedgerID"]);
                MTClaimLedgerName = Convert.ToString(ds.Tables[0].Rows[0]["MTClaimLedgerName"]);
                MTClaimChecker1 = Convert.ToString(ds.Tables[0].Rows[0]["MTClaimChecker1"]);
                MTClaimChecker2 = Convert.ToString(ds.Tables[0].Rows[0]["MTClaimChecker2"]);
                MTClaimChecker3 = Convert.ToString(ds.Tables[0].Rows[0]["MTClaimChecker3"]);
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                IsGSTVoucher = "G";
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[3].Rows)
                {
                    header.Add(new Acchdr
                    {
                        IsGSTVoucher = IsGSTVoucher,
                        VoucherTypeID = VoucherTypeID,
                        BranchID = BranchID,
                        Date = Date,
                        Mode = Mode,
                        Narration = Narration,
                        RejectionNote = RejectionNote,
                        VoucherNo = VoucherNo1,
                        IsPaymentType = IsPaymentType,
                        PaymentParty = PaymentParty,
                        BillNo = Convert.ToString(ds.Tables[3].Rows[0]["BillNo"]),
                        BillDate = Convert.ToString(ds.Tables[3].Rows[0]["BillDate"]),
                        GRNo = Convert.ToString(ds.Tables[3].Rows[0]["GRNo"]),
                        GRDate = Convert.ToString(ds.Tables[3].Rows[0]["GRDate"]),
                        VehicleNo = Convert.ToString(ds.Tables[3].Rows[0]["VehicleNo"]),
                        Transport = Convert.ToString(ds.Tables[3].Rows[0]["Transport"]),
                        WayBillNo = Convert.ToString(ds.Tables[3].Rows[0]["WayBillNo"]),
                        WayBillDate = Convert.ToString(ds.Tables[3].Rows[0]["WayBillDate"]),
                        MTClaimLedgerID = MTClaimLedgerID,
                        MTClaimLedgerName = MTClaimLedgerName,
                        MTClaimChecker1 = MTClaimChecker1,
                        MTClaimChecker2 = MTClaimChecker2,
                        MTClaimChecker3 = MTClaimChecker3
                    });
                }
            }
            else

        if (ds.Tables[3].Rows.Count == 0)
            {
                header.Add(new Acchdr
                {
                    IsGSTVoucher = IsGSTVoucher,
                    VoucherTypeID = VoucherTypeID,
                    BranchID = BranchID,
                    Date = Date,
                    Mode = Mode,
                    Narration = Narration,
                    RejectionNote = RejectionNote,
                    VoucherNo = VoucherNo1,
                    IsPaymentType = IsPaymentType,
                    PaymentParty = PaymentParty,
                    BillNo = "",
                    BillDate = "",
                    GRNo = "",
                    GRDate = "",
                    VehicleNo = "",
                    Transport = "",
                    WayBillNo = "",
                    WayBillDate = "",
                });
            }
            CreateVoucherTable();
            //CreateInvoiceTable();
            //Create_DebitCreditInvoiceTable();
            CreateGSTTable();
            CreateBudgetTable(); /*Budget on 24/06/2019 */
            DataTable dtVoucher = (DataTable)HttpContext.Session["ACC_VOUCHERDETAILS"];
            DataTable dtinvoice = (DataTable)HttpContext.Session["ACC_INVOICEDETAILS"];
            DataTable dtdebitcredit = (DataTable)HttpContext.Session["INVOICE_CREDITDEBITDETAILS"];
            DataTable dtGST = (DataTable)HttpContext.Session["GSTDETAILS"];
            DataTable dtBudget = (DataTable)HttpContext.Session["BudgetDetails"];/*Budget on 24/06/2019 */
            HttpContext.Session["BudgetDetailsTemp"] = null;
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtVoucher.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                    dr["LedgerName"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                    dr["TxnType"] = Convert.ToString(ds.Tables[1].Rows[i]["TxnType"]).Trim();
                    dr["Amount"] = Convert.ToString(ds.Tables[1].Rows[i]["Amount"]).Trim();
                    dr["BankID"] = Convert.ToString(ds.Tables[1].Rows[i]["BankID"]);
                    dr["BankName"] = Convert.ToString(ds.Tables[1].Rows[i]["BankName"]);
                    dr["PAYMENTTYPEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPEID"]);
                    dr["PAYMENTTYPENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPENAME"]);
                    dr["ChequeNo"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeNo"]);
                    dr["ChequeDate"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeDate"]);
                    dr["IsChequeRealised"] = Convert.ToString(ds.Tables[1].Rows[i]["IsChequeRealised"]);
                    dr["Remarks"] = Convert.ToString(ds.Tables[1].Rows[i]["Remarks"]);
                    dr["DeductableAmount"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableAmount"]);
                    dr["DeductablePercentage"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductablePercentage"]);
                    dr["DeductableLedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableLedgerId"]);
                    dr["IsCostCenter"] = Convert.ToString(ds.Tables[1].Rows[i]["IsCostCenter"]);
                    dr["IsTagInvoice"] = Convert.ToString(ds.Tables[1].Rows[i]["IsTagInvoice"]);
                    dr["NonTaxableAmount"] = Convert.ToString(ds.Tables[1].Rows[i]["NonTaxableAmount"]);
                    dr["BYDEFAULTAMOUNT"] = "0";
                    dr["TransactionAmount"] = "0";
                    dr["DepartmentID"] = Convert.ToString(ds.Tables[1].Rows[i]["DepartmentID"]); /* Add Budget on 24/06/2019 */
                    dr["DepartmentName"] = Convert.ToString(ds.Tables[1].Rows[i]["DepartmentName"]); /* Add Budget on 24/06/2019 */

                    dr["BusinessSegId"] = Convert.ToString(ds.Tables[1].Rows[i]["BusinessSegId"]); /* Add  23/04/2019 */
                    dr["BusinessSegName"] = Convert.ToString(ds.Tables[1].Rows[i]["BusinessSegName"]); /* Add  23/04/2019 */
                    dtVoucher.Rows.Add(dr);
                    dtVoucher.AcceptChanges();
                    HttpContext.Session["ACC_VOUCHERDETAILS"] = dtVoucher;
                    //for budget
                    if (Convert.ToString(ds.Tables[1].Rows[i]["TxnType"]).Trim() == "0" && Convert.ToString(ds.Tables[1].Rows[i]["DepartmentID"]).Trim() != "")
                    {
                        DataRow drBud = dtBudget.NewRow();
                        drBud["LedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                        drBud["LedgerName"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                        drBud["DepartmentID"] = Convert.ToString(ds.Tables[1].Rows[i]["DepartmentID"]).Trim();
                        drBud["DepartmentName"] = Convert.ToString(ds.Tables[1].Rows[i]["DepartmentName"]).Trim();

                        dtBudget.Rows.Add(drBud);
                        dtBudget.AcceptChanges();

                        HttpContext.Session["BudgetDetails"] = dtBudget;
                    }

                }

            }
            //cost center
            if (ds.Tables[2].Rows.Count > 0)
            {
                CreateCostCenterTable();
                DataTable dtcost = (DataTable)HttpContext.Session["ACC_COSTCENTERDETAILS"];
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    DataRow dr = dtcost.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LedgerId"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerId"]);
                    dr["LedgerName"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerName"]);
                    dr["CostCatagoryID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryID"]);
                    dr["CostCatagoryName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryName"]);
                    dr["CostCenterID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterID"]);
                    dr["CostCenterName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterName"]);
                    dr["BranchID"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchID"]);
                    dr["BranchName"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchName"]);
                    dr["amount"] = Convert.ToString(ds.Tables[2].Rows[i]["amount"]);
                    dr["BrandID"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandID"]);
                    dr["BrandName"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandName"]);
                    dr["ProductID"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductID"]);
                    dr["ProductName"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductName"]);
                    dr["DepartmentID"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentID"]);
                    dr["DepartmentName"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentName"]);
                    dr["FromDate"] = Convert.ToString(ds.Tables[2].Rows[i]["FromDate"]);
                    dr["ToDate"] = Convert.ToString(ds.Tables[2].Rows[i]["ToDate"]);
                    dr["Narration"] = Convert.ToString(ds.Tables[2].Rows[i]["NARRATION"]);
                    dr["TxnType"] = Convert.ToString(ds.Tables[2].Rows[i]["TxnType"]);
                    /*Add for budget */


                    dtcost.Rows.Add(dr);
                    dtcost.AcceptChanges();
                }
                HttpContext.Session["COSTCENTERDETAILS"] = dtcost;
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                CreateGSTTable();
                DataTable dtvoucherGST = (DataTable)HttpContext.Session["GSTDETAILS"];
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    DataRow dr = dtvoucherGST.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["GroupId"] = ds.Tables[4].Rows[i]["GroupID"].ToString().Trim();
                    dr["PartyID"] = ds.Tables[4].Rows[i]["LedgerId"].ToString().Trim();
                    dr["InvoiceNo"] = ds.Tables[4].Rows[i]["InvoiceNo"].ToString().Trim();
                    dr["InvoiceDate"] = ds.Tables[4].Rows[i]["InvoiceDate"].ToString().Trim();
                    dr["Taxable"] = ds.Tables[4].Rows[i]["Taxable"].ToString().Trim();
                    dr["TaxableValue"] = ds.Tables[4].Rows[i]["TaxableValue"].ToString().Trim();
                    dr["HSNCode"] = ds.Tables[4].Rows[i]["HSNCode"].ToString().Trim();
                    dr["PartyTrade"] = ds.Tables[4].Rows[i]["PratyTrade"].ToString().Trim();
                    dr["StateID"] = ds.Tables[4].Rows[i]["StateID"].ToString().Trim();
                    dr["GSTNo"] = ds.Tables[4].Rows[i]["GSTNo"].ToString().Trim();
                    dr["TaxTypeID"] = ds.Tables[4].Rows[i]["TaxTypeID"].ToString().Trim();
                    dr["TaxType"] = ds.Tables[4].Rows[i]["TaxType"].ToString().Trim();
                    dr["TaxAmount"] = ds.Tables[4].Rows[i]["TaxAmount"].ToString().Trim();
                    dr["NetAmount"] = ds.Tables[4].Rows[i]["NetAmount"].ToString().Trim();
                    dr["OldNew"] = "New";
                    if (string.IsNullOrWhiteSpace(ds.Tables[4].Rows[i]["Taxable1"].ToString()))
                    {
                        dr["Taxable1"] = "0";
                    }
                    else
                    {
                        dr["Taxable1"] = ds.Tables[4].Rows[i]["Taxable1"].ToString();
                    }
                    if (string.IsNullOrWhiteSpace(ds.Tables[4].Rows[i]["TaxAmount1"].ToString()))
                    {
                        dr["TaxAmount1"] = "0";
                    }
                    else
                    {
                        dr["TaxAmount1"] = ds.Tables[4].Rows[i]["TaxAmount1"].ToString();
                    }
                    dr["TaxTypeID1"] = ds.Tables[4].Rows[i]["TaxTypeID1"].ToString(); ;
                    dr["TaxType1"] = ds.Tables[4].Rows[i]["TaxType1"].ToString();
                    dr["ISIGST"] = ds.Tables[4].Rows[i]["ISIGST"].ToString();
                    dr["RoundOff"] = ds.Tables[4].Rows[i]["RoundOff"].ToString();
                    if (string.IsNullOrWhiteSpace(ds.Tables[4].Rows[i]["NonTaxableAmountGST"].ToString()))
                    {
                        dr["NonTaxableAmountGST"] = "0";
                    }
                    else
                    {
                        dr["NonTaxableAmountGST"] = ds.Tables[4].Rows[i]["NonTaxableAmountGST"].ToString(); ;
                    }

                    dr["PlaceOfSupply"] = ds.Tables[4].Rows[i]["PlaceOfSupply"].ToString(); ; ;
                    dtvoucherGST.Rows.Add(dr);
                    dtvoucherGST.AcceptChanges();

                }
                HttpContext.Session["GSTDETAILS"] = dtvoucherGST;

            }
            return Json(header);
        }
        #endregion
        [HttpPost]
        public JsonResult Viewgst()
        {
            List<addgsttable> taxabletable = new List<addgsttable>();
            DataTable dtvoucherGST = (DataTable)HttpContext.Session["GSTDETAILS"];
            foreach (DataRow row in dtvoucherGST.Rows)

                taxabletable.Add(new addgsttable
                {
                    GUID = Convert.ToString(row["GUID"]),
                    GroupId = Convert.ToString(row["GroupId"]),
                    PartyID = Convert.ToString(row["PartyID"]),
                    InvoiceNo = Convert.ToString(row["InvoiceNo"]),
                    InvoiceDate = Convert.ToString(row["InvoiceDate"]),
                    Taxable = Convert.ToString(row["Taxable"]),
                    TaxableValue = Convert.ToString(row["TaxableValue"]),
                    HSNCode = Convert.ToString(row["HSNCode"]),
                    PartyTrade = Convert.ToString(row["PartyTrade"]),
                    StateID = Convert.ToString(row["StateID"]),
                    GSTNo = Convert.ToString(row["GSTNo"]),
                    TaxTypeID = Convert.ToString(row["TaxTypeID"]),
                    TaxType = Convert.ToString(row["TaxType"]),
                    TaxAmount = Convert.ToString(row["TaxAmount"]),
                    NetAmount = Convert.ToString(row["NetAmount"]),
                    Taxable1 = Convert.ToString(row["Taxable1"]),
                    TaxAmount1 = Convert.ToString(row["TaxAmount1"]),
                    TaxTypeID1 = Convert.ToString(row["TaxTypeID1"]),
                    TaxType1 = Convert.ToString(row["TaxType1"]),
                    ISIGST = Convert.ToString(row["ISIGST"]),
                    NonTaxableAmountGST = Convert.ToString(row["NonTaxableAmountGST"]),
                    RoundOff = Convert.ToString(row["RoundOff"]),
                    PlaceOfSupply = Convert.ToString(row["PlaceOfSupply"]),
                    


                });
            return Json(taxabletable);


        }
        #region ===invoice details
        #endregion
        [HttpPost]
        public JsonResult Outstanding(string LedgerID, string RegionID, string FromDate, string ToDate,string finyr)
        {
            //  var finyr = _ICacheManager.Get<object>("FINYEAR"); ;
           // var finyr = Convert.ToString(Session["FINYEAR"]);
            List<invoicedetails> invoicedetails = new List<invoicedetails>();
            Accountscontext accountcontext = new Accountscontext();
            invoicedetails = accountcontext.Outstanding(LedgerID, RegionID, FromDate, ToDate, finyr.ToString());
            return Json(invoicedetails);
        }
        [HttpPost]
        public JsonResult InvoiceDetails(string VoucherID, string Leadgerid, string VouchertYpe, string Branchid,string finyr)

        {
            //  var finyr = _ICacheManager.Get<object>("FINYEAR");
          //  var finyr = Convert.ToString(Session["FINYEAR"]); ;
            List<invoicedetails> invoicedetails = new List<invoicedetails>();
            Accountscontext accountcontext = new Accountscontext();
            invoicedetails = accountcontext.InvoiceDetails(VoucherID, Leadgerid, VouchertYpe, Branchid);
            int count = invoicedetails.Count;
            return Json(invoicedetails);
        }
        [HttpPost]
        public JsonResult Approvedvoucher(string VoucherID, string Ledgerid, string LedgerName,string Finyear)
        {
            List<messageresponse> responseMessage = new List<messageresponse>();
            List<Accresponse> Accresponse = new List<Accresponse>();
            //  var userid = _ICacheManager.Get<object>("UserID");
            var userid = Convert.ToString(Session["UserID"]);
            //  var Finyear = _ICacheManager.Get<object>("FINYEAR");
          //  var Finyear = Convert.ToString(Session["FINYEAR"]);

            Accountscontext accountcontext = new Accountscontext();
            ClsVoucherentry clsvoucher = new ClsVoucherentry();
            responseMessage = accountcontext.voucherapproved(VoucherID, Ledgerid, LedgerName, userid.ToString(), Finyear.ToString()).ToList();

            string voucherid = "";
            string voucherno = "";
            foreach (var msg in responseMessage)
            {
                string VoucherNo = msg.response;
                TempData["messageid"] = msg.response;
                String[] voucher = VoucherNo.Split('|');
                voucherid = voucher[0].Trim();
                voucherno = voucher[1].Trim();
            }

            Accresponse.Add(new Accresponse
            {
                voucherid = voucherid,
                voucherno = voucherno
            });
            return Json(Accresponse, JsonRequestBehavior.AllowGet);
        }
    }
}