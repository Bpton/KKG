using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BAL;
using System.Data;
using WorkFlow;
using Account;
using Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
public partial class VIEW_frmEInvoice : System.Web.UI.Page
{
    DateTime dtcurr = DateTime.Now;
    DateTime dtPrviousDay = DateTime.Now.AddDays(-1);
    DateTime dtFirstDayofMonth = DateTime.Now.AddMonths(1).AddDays(-1);
    string date = "dd/MM/yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // readjson();
               // Decrypt("eyJhbGciOiJSUzI1NiIsImtpZCI6IjQ0NDQwNUM3ODFFNDgyNTA3MkIzNENBNEY4QkRDNjA2Qzg2QjU3MjAiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJSRVFGeDRIa2dsQnlzMHlrLUwzR0JzaHJWeUEifQ.eyJkYXRhIjoie1wiU2VsbGVyR3N0aW5cIjpcIjE5QUFCQ001Njc0SjFaMVwiLFwiQnV5ZXJHc3RpblwiOlwiMTlBRkVQSzIzMzFLMVpIXCIsXCJEb2NOb1wiOlwiSU5WLUtPTC8yNTM2XCIsXCJEb2NUeXBcIjpcIklOVlwiLFwiRG9jRHRcIjpcIjE5LzEwLzIwMjBcIixcIlRvdEludlZhbFwiOjExOTI2MS4wMCxcIkl0ZW1DbnRcIjo5LFwiTWFpbkhzbkNvZGVcIjpcIjM0MDFcIixcIklyblwiOlwiNDI2ZDA2N2JhOTE3YWRlY2RhYmU3YmIyNjk0YzU0OWRiMDM5NDRhMzdlY2E2ZGNkODgzNGM0NzcxYTNlNzJjOVwiLFwiSXJuRHRcIjpcIjIwMjAtMTAtMjAgMTI6MDY6MDBcIn0iLCJpc3MiOiJOSUMifQ.GIOWp_wZP7HAGlQEmAOiYfddlm_dqCzwB4_QbLFe0K9GT1wv6dL-kJqYlpVgcMyXpsLsOkYoRhBaeB94B2YwuQCTVx1kRR7deqdObiLiOtqNWUBOT1_7OxTjY_g18zhW9CF_t2trKn1vxdE7DMK057IlrNNr-ZIUNzAmCQ35Z6K2OVtNn0KnQTRZ_-72w_tk_LD57RdYW7R91Aiv4j6lw4jXx8MwF4OsPY0w-9aWt4S37jNZQkhu5vrvxIZypHUkOpd8GiOS_gTSY1JqzhQ8WVkflHKX2M9eofUgRkoPBklO9ofSroyTsPx1naProo0RPjC3p52Qh6gJS_PgWwGN7g");
                //GenerateCode("eyJhbGciOiJSUzI1NiIsImtpZCI6IjQ0NDQwNUM3ODFFNDgyNTA3MkIzNENBNEY4QkRDNjA2Qzg2QjU3MjAiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJSRVFGeDRIa2dsQnlzMHlrLUwzR0JzaHJWeUEifQ.eyJkYXRhIjoie1wiU2VsbGVyR3N0aW5cIjpcIjE5QUFCQ001Njc0SjFaMVwiLFwiQnV5ZXJHc3RpblwiOlwiMTlBRkVQSzIzMzFLMVpIXCIsXCJEb2NOb1wiOlwiSU5WLUtPTC8yNTM2XCIsXCJEb2NUeXBcIjpcIklOVlwiLFwiRG9jRHRcIjpcIjE5LzEwLzIwMjBcIixcIlRvdEludlZhbFwiOjExOTI2MS4wMCxcIkl0ZW1DbnRcIjo5LFwiTWFpbkhzbkNvZGVcIjpcIjM0MDFcIixcIklyblwiOlwiNDI2ZDA2N2JhOTE3YWRlY2RhYmU3YmIyNjk0YzU0OWRiMDM5NDRhMzdlY2E2ZGNkODgzNGM0NzcxYTNlNzJjOVwiLFwiSXJuRHRcIjpcIjIwMjAtMTAtMjAgMTI6MDY6MDBcIn0iLCJpc3MiOiJOSUMifQ.GIOWp_wZP7HAGlQEmAOiYfddlm_dqCzwB4_QbLFe0K9GT1wv6dL-kJqYlpVgcMyXpsLsOkYoRhBaeB94B2YwuQCTVx1kRR7deqdObiLiOtqNWUBOT1_7OxTjY_g18zhW9CF_t2trKn1vxdE7DMK057IlrNNr-ZIUNzAmCQ35Z6K2OVtNn0KnQTRZ_-72w_tk_LD57RdYW7R91Aiv4j6lw4jXx8MwF4OsPY0w-9aWt4S37jNZQkhu5vrvxIZypHUkOpd8GiOS_gTSY1JqzhQ8WVkflHKX2M9eofUgRkoPBklO9ofSroyTsPx1naProo0RPjC3p52Qh6gJS_PgWwGN7g");
                this.pnlADD.Style["display"] = "";
                //InisiliseDate();

                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                /*Calender Control Date Range*/
                // CalendarFromDate.StartDate = dtFirstDayofMonth;
                //CalendarFromDate.EndDate = dtFirstDayofMonth;
                CalendarExtenderToDate.EndDate = DateTime.Now;
                /****************************/



                this.Loaddepot();
                this.trinvoice.Style["display"] = "";
                this.DateLock();

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            CalendarFromDate.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
            //CalendarExtender6.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                //this.txtsalenrgodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
               // CalendarExtender6.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                //this.txtsalenrgodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
               // CalendarExtender6.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion
    private void InisiliseDate()
    {
        /* New code for date on 02/04/2019 */
        //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      SOUMITRA MONDAL       //

        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        CalendarFromDate.StartDate = oDate;
        CalendarExtenderToDate.StartDate = oDate;

        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            CalendarFromDate.EndDate = today1;
            CalendarExtenderToDate.EndDate = today1;

        }
        else
        {
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarFromDate.EndDate = cDate;
            CalendarExtenderToDate.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }

    private void Loaddepot()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = clsVoucher.BindDepot(Convert.ToString(Session["IUserID"]).Trim());
            if (dt.Rows.Count > 0)
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select", "0"));
                this.ddlDepot.AppendDataBoundItems = true;
                this.ddlDepot.DataSource = dt;
                this.ddlDepot.DataValueField = "BRID";
                this.ddlDepot.DataTextField = "BRNAME";
                this.ddlDepot.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlDepot.SelectedValue = Convert.ToString(dt.Rows[0]["BRID"]);
                }
            }
            else
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadInvoice
    public void LoadInvoice()
    {
        try
        {
            ClsEinvoice clsInvc = new ClsEinvoice();
            DataTable dt = clsInvc.BindApprovedInvoice(this.ddlDepot.SelectedValue.Trim(), this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Search
    public void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            LoadInvoice();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void ResetGrid()
    {
        try
        {
            this.gvUnlockInvoice.DataSource = null;
            this.gvUnlockInvoice.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsEinvoice clsInvc = new ClsEinvoice();


            int totalcount = 0;


            GridView gvunlock = gvUnlockInvoice;
            string gvchkbox = "";

            string gvinvoiceid = "";
            string gvinvoiceno = "";
            string gvinvoicedate = "";



            gvunlock = gvUnlockInvoice;
            gvchkbox = "chkSelect";

            gvinvoiceid = "lblSALEINVOICEID";
            //gvinvoiceno = "lblSALEINVOICENO";
            //gvinvoicedate = "lblSALEINVOICEDATE";            

            foreach (GridViewRow gvrow in gvunlock.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                if (chkBx.Checked)
                {
                    totalcount += 1;
                }
            }

            if (totalcount <= 300)
            {
                int totalapproval = 0;
                string InvoiceID = string.Empty;

                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblSALEINVOICEID = (Label)gvrow.FindControl("" + gvinvoiceid + "");

                    if (chkBx.Checked)
                    {
                        totalapproval += 1;
                        InvoiceID += lblSALEINVOICEID.Text.Trim() + ',';
                    }
                }
                InvoiceID = InvoiceID.Substring(0, InvoiceID.Length - 1);
                DataTable dt = clsInvc.CreateJson(InvoiceID);

                /*Hard code invoice id for testing */
                //DataTable dt = clsInvc.CreateJson("78B7E83E-9084-4412-94D7-255BCE70C2CD,F4B365A7-604B-4DA3-BA6D-BF74DC0B4529");

                Converttojson(dt);

                //string strFilePath = @"D:\E_Invoice\KOL\E_Invoice_Kol.csv";

                //this.ToCSV(dt, strFilePath);
                this.LoadInvoice();
                MessageBox1.ShowInfo("Json File generated Successfully.<b>Path:E:" + @"\E_Invoice" + @"\EINVOIVCE" + "</b>", 40, 500);
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='maroon'>Note :</font> <font color='gray'>You can approved maximum 50 invoices at a time.</font></b>", 40, 510);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void btnupdate_Click(object sender, EventArgs e)
    {
        readjson();
    }

        public void Converttojson(DataTable dtinvoice)
    {
        ClsEinvoice clsInvc = new ClsEinvoice();
        string response = string.Empty;
        //List<TranDtls> TranDtlstemp = new List<TranDtls>();
        //TranDtls TranDtlstemp = new TranDtls();
        //DocDtls DocDtlstemp = new DocDtls();
        //SellerDtls SellerDtlstemp = new SellerDtls();
        //BuyerDtls BuyerDtlstemp = new BuyerDtls();
        //DispDtls DispDtlstemp = new DispDtls();
        //ValDtls ValDtlstemp = new ValDtls();
        //AddlDocDtl AddlDocDtltemp = new AddlDocDtl();
        List<GSTList> GSTList = new List<GSTList>();
        Root root = new Root();
        string version = "1.1";

        string prevno = string.Empty;
        string sl = string.Empty;
        if (dtinvoice.Rows.Count > 0)
        {
            //  GSTList.Version = version;
            foreach (DataRow dr in dtinvoice.Rows)

            {
                List<ItemList> ItemList = new List<ItemList>();
                TranDtls TranDtlstemp = new TranDtls();
                DocDtls DocDtlstemp = new DocDtls();
                SellerDtls SellerDtlstemp = new SellerDtls();
                BuyerDtls BuyerDtlstemp = new BuyerDtls();
                DispDtls DispDtlstemp = new DispDtls();
                ValDtls ValDtlstemp = new ValDtls();

                List<AddlDocDtl> AddlDocDtl = new List<AddlDocDtl>();
                // GSTList GSTListtemp = new GSTList();
                sl = Convert.ToString(dr["Sl"]);
                if (prevno != Convert.ToString(dr["Sl"]))
                {

                    //TranDtlstemp.Add(new TranDtls()
                    //{
                    TranDtlstemp.TaxSch = "GST";
                    TranDtlstemp.SupTyp = Convert.ToString(dr["Supply Type Code"]);
                    TranDtlstemp.IgstOnIntra = Convert.ToString(dr["Igst On Intra"]);
                    TranDtlstemp.RegRev = Convert.ToString(dr["Reverse Charge"]);
                    if (Convert.ToString(dr["e-Comm GSTIN"]) == "")
                    {
                        TranDtlstemp.EcmGstin = null;
                    }
                    else
                    {
                        TranDtlstemp.EcmGstin = Convert.ToString(dr["e-Comm GSTIN"]);
                    }
                    /*})*/;
                }
                if (prevno != Convert.ToString(dr["Sl"]))
                {

                    if (Convert.ToString(dr["Document Type"]) == "Tax Invoice")
                    {
                        DocDtlstemp.Typ = "INV";
                    }
                    if (Convert.ToString(dr["Document Type"]) == "Credit Note")
                    {
                        DocDtlstemp.Typ = "CRN";
                    }
                    if (Convert.ToString(dr["Document Type"]) == "Debit Note")
                    {
                        DocDtlstemp.Typ = "DBN";
                    }
                    DocDtlstemp.No = Convert.ToString(dr["Document Number"]);
                    DocDtlstemp.Dt = Convert.ToString(dr["Document Date (DD/MM/YYYY)"]);

                }
                if (prevno != Convert.ToString(dr["Sl"]))
                {

                    SellerDtlstemp.Gstin = Convert.ToString(dr["Dispatch GSTIN"]);
                    SellerDtlstemp.LglNm = Convert.ToString(dr["Dispatch Name"]);
                    if (Convert.ToString(dr["Dispatch Trade Name"]) == "")
                    {
                        SellerDtlstemp.TrdNm = null;
                    }
                    else
                    {
                        SellerDtlstemp.TrdNm = Convert.ToString(dr["Dispatch Trade Name"]);
                    }
                    SellerDtlstemp.Addr1 = Convert.ToString(dr["Dispatch Addr1"]);
                    if (Convert.ToString(dr["Dispatch Addr2"]) == "")
                    {
                        SellerDtlstemp.Addr2 = null;
                    }
                    else
                    {
                        SellerDtlstemp.Addr2 = Convert.ToString(dr["Dispatch Addr2"]);
                    }
                    SellerDtlstemp.Loc = Convert.ToString(dr["Dispatch Location"]);
                    SellerDtlstemp.Pin = Int32.Parse(Convert.ToString(dr["Dispatch Pin Code"]));
                    SellerDtlstemp.Stcd = Convert.ToString(dr["Dispatch State"]);
                    if (Convert.ToString(dr["Dispatch Phone Number"]) == "")
                    {
                        SellerDtlstemp.Ph = null;
                    }
                    else
                    {
                        SellerDtlstemp.Ph = Convert.ToString(dr["Dispatch Phone Number"]);
                    }
                    if (Convert.ToString(dr["Dispatch Email Id"]) == "")
                    {
                        SellerDtlstemp.Em = null;
                    }
                    else
                    {
                        SellerDtlstemp.Em = Convert.ToString(dr["Dispatch Email Id"]);
                    }


                }
                if (prevno != Convert.ToString(dr["Sl"]))
                {

                    BuyerDtlstemp.Gstin = Convert.ToString(dr["Buyer GSTIN"]);
                    BuyerDtlstemp.LglNm = Convert.ToString(dr["Buyer Legal Name"]);
                    BuyerDtlstemp.TrdNm = Convert.ToString(dr["Buyer Trade Name"]);
                    BuyerDtlstemp.Pos = Convert.ToString(dr["Buyer POS"]);
                    BuyerDtlstemp.Addr1 = Convert.ToString(dr["Buyer Addr1"]);
                    if (Convert.ToString(dr["Buyer Addr2"]) == "")
                    {
                        BuyerDtlstemp.Addr2 = null;
                    }
                    else
                    {
                        BuyerDtlstemp.Addr2 = Convert.ToString(dr["Buyer Addr2"]);
                    }
                    BuyerDtlstemp.Loc = Convert.ToString(dr["Buyer Location"]);
                    BuyerDtlstemp.Pin = Int32.Parse(Convert.ToString(dr["Buyer Pin Code"]));
                    BuyerDtlstemp.Stcd = Convert.ToString(dr["Buyer POS"]);
                    if (Convert.ToString(dr["Buyer Phone Number"]) == "")
                    {
                        BuyerDtlstemp.Ph = null;
                    }
                    else
                    {
                        BuyerDtlstemp.Ph = Convert.ToString(dr["Buyer Phone Number"]);
                    }
                    if (Convert.ToString(dr["Buyer Email Id"]) == "")
                    {
                        BuyerDtlstemp.Em = null;
                    }
                    else
                    {
                        BuyerDtlstemp.Em = Convert.ToString(dr["Buyer Email Id"]);
                    }

                }
                if (prevno != Convert.ToString(dr["Sl"]))
                {


                    DispDtlstemp.Nm = Convert.ToString(dr["Dispatch Name"]);
                    DispDtlstemp.Addr1 = Convert.ToString(dr["Dispatch Addr1"]);
                    if (Convert.ToString(dr["Dispatch Addr2"]) == "")
                    {
                        DispDtlstemp.Addr2 = null;
                    }
                    else
                    {
                        DispDtlstemp.Addr2 = Convert.ToString(dr["Dispatch Addr2"]);
                    }
                    DispDtlstemp.Loc = Convert.ToString(dr["Dispatch Location"]);
                    DispDtlstemp.Pin = Int32.Parse(Convert.ToString(dr["Dispatch Pin Code"]));
                    DispDtlstemp.Stcd = Convert.ToString(dr["Dispatch State"]);
                }
                if (prevno != Convert.ToString(dr["Sl"]))
                {


                    ValDtlstemp.AssVal = Convert.ToDecimal(Convert.ToString(dr["Total Taxable Value"]));
                    ValDtlstemp.IgstVal = Convert.ToDecimal(Convert.ToString(dr["Igst Amt"]));
                    ValDtlstemp.CgstVal = Convert.ToDecimal(Convert.ToString(dr["Cgst Amt"]));
                    ValDtlstemp.SgstVal = Convert.ToDecimal(Convert.ToString(dr["Sgst Amt"]));
                    ValDtlstemp.CesVal = Int32.Parse(Convert.ToString(dr["Cess Amt"]));
                    ValDtlstemp.StCesVal = Int32.Parse(Convert.ToString(dr["State Cess Amt"]));
                    ValDtlstemp.Discount = Int32.Parse(Convert.ToString(dr["Total Discount"]));
                    ValDtlstemp.OthChrg = Int32.Parse(Convert.ToString(dr["Total Other Charges"]));
                    ValDtlstemp.RndOffAmt = Convert.ToDecimal(Convert.ToString(dr["Round off"]));
                    ValDtlstemp.TotInvVal = Convert.ToDecimal(Convert.ToString(dr["Total Invoice value"]));
                    ValDtlstemp.TotInvValFc = Int32.Parse(Convert.ToString(dr["Total Invoice value in Additional Currency"]));
                }

                if (prevno != Convert.ToString(dr["Sl"]))
                {

                    //foreach (DataRow intemrow in rowitem)
                    //{
                    AddlDocDtl AddlDocDtltemp = new AddlDocDtl();
                    AddlDocDtltemp.Url = null;
                    AddlDocDtltemp.Docs = null;
                    AddlDocDtltemp.Info = null;
                    AddlDocDtl.Add(AddlDocDtltemp);
                    //}
                }


                if (prevno != Convert.ToString(dr["Sl"]))
                {
                    DataRow[] rowitem;

                    rowitem = dtinvoice.Select("Sl  = '" + sl + "'");
                    foreach (DataRow intemrow in rowitem)
                    {
                        ItemList ItemListtemp = new ItemList();
                        ItemListtemp.SlNo = Convert.ToString(intemrow["Sl.No."]);
                        ItemListtemp.PrdDesc = Convert.ToString(intemrow["Product Description"]).Trim();
                        ItemListtemp.IsServc = Convert.ToString(intemrow["Is_Service"]);
                        ItemListtemp.HsnCd = Convert.ToString(intemrow["HSN code"]);
                        ItemListtemp.Qty = Int32.Parse(Convert.ToString(intemrow["Quantity"]));
                        ItemListtemp.FreeQty = Int32.Parse(Convert.ToString(intemrow["Free Quantity"]));
                        ItemListtemp.Unit = Convert.ToString(intemrow["Unit"]);
                        ItemListtemp.UnitPrice = Convert.ToDecimal(Convert.ToString(intemrow["Unit Price"]));
                        ItemListtemp.TotAmt = Convert.ToDecimal(Convert.ToString(intemrow["Gross Amount"]));
                        ItemListtemp.Discount = Convert.ToDecimal(Convert.ToString(intemrow["Discount"]));
                        ItemListtemp.PreTaxVal = 0;
                        ItemListtemp.AssAmt = Convert.ToDecimal(Convert.ToString(intemrow["Taxable value"]));
                        ItemListtemp.GstRt = Int32.Parse(Convert.ToString(intemrow["GST Rate (%)"]));
                        ItemListtemp.IgstAmt = Convert.ToDecimal(Convert.ToString(intemrow["Igst Amt(Rs)"]));
                        ItemListtemp.CgstAmt = Convert.ToDecimal(Convert.ToString(intemrow["Cgst Amt(Rs)"]));
                        ItemListtemp.SgstAmt = Convert.ToDecimal(Convert.ToString(intemrow["Sgst Amt(Rs)"]));
                        ItemListtemp.CesRt = Int32.Parse(Convert.ToString(intemrow["Cess Rate (%)"]));
                        ItemListtemp.CesAmt = Int32.Parse(Convert.ToString(intemrow["Cess Amt Adval (Rs)"]));
                        ItemListtemp.CesNonAdvlAmt = Int32.Parse(Convert.ToString(intemrow["Cess Non Adval Amt (Rs)"]));
                        ItemListtemp.StateCesRt = Int32.Parse(Convert.ToString(intemrow["State Cess Rate (%)"]));
                        ItemListtemp.StateCesAmt = Int32.Parse(Convert.ToString(intemrow["State Cess Amt Adval (Rs)"]));
                        ItemListtemp.StateCesNonAdvlAmt = Int32.Parse(Convert.ToString(intemrow["State Cess Non-Adval Amt (Rs)"]));
                        ItemListtemp.OthChrg = Int32.Parse(Convert.ToString(intemrow["Other Charges"]));
                        ItemListtemp.TotItemVal = Convert.ToDecimal(Convert.ToString(intemrow["Item Total"]));
                        ItemListtemp.BchDtls = null;
                        ItemList.Add(ItemListtemp);
                    }




                }//item if
                if (prevno != Convert.ToString(dr["Sl"]))
                {
                    GSTList.Add(new GSTList()
                    {
                        Version = "1.1",
                        TranDtls = TranDtlstemp,
                        DocDtls = DocDtlstemp,
                        SellerDtls = SellerDtlstemp,
                        BuyerDtls = BuyerDtlstemp,
                        DispDtls = DispDtlstemp,
                        ValDtls = ValDtlstemp,
                        AddlDocDtls = AddlDocDtl,
                        ItemList = ItemList
                        //GSTList.Add(GSTListtemp);
                    });

                }
                prevno = Convert.ToString(dr["Sl"]);

            }//main for loop

            root.GSTList = GSTList;
            response = JsonConvert.SerializeObject(GSTList);

            //string drive = @"E:\";
            //string folders = @"E_Invoice\"+ this.ddlDepot.SelectedItem.ToString().Trim()+@"\";
            string fileName = @"E-INVOICE" + this.ddlDepot.SelectedItem.ToString() + "_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".json";
            //string[] paths = { drive, folders, fileName };

            //string fullPath = Path.Combine(paths);
            string filepath = Server.MapPath("../") + "FileUpload\\" + fileName;
            System.IO.File.WriteAllText(filepath, response);


            Downloadfile(fileName);

            //response = response.Replace("[", " ").Replace("]", " ");
            //var jobject = JObject.Parse(response);
            //System.IO.File.WriteAllText(@"D:\E_Invoice\IND\E-INVOICE_V4_JSON.json", JsonConvert.SerializeObject(GSTList));

        }

    }

    public void Downloadfile(string fileName)
    {
        //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://mcnroeerp.com:2122/Upload/speye.txt");
        //HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        //int bufferSize = 1;
        //Response.Clear();
        //Response.ClearHeaders();
        //Response.ClearContent();
        //Response.AppendHeader("Content-Disposition:", "attachment; filename=Export_Thane_0004.json");
        //Response.AppendHeader("Content-Length", objResponse.ContentLength.ToString());
        //Response.ContentType = "application/download";
        //byte[] byteBuffer = new byte[bufferSize + 1];
        //MemoryStream memStrm = new MemoryStream(byteBuffer, true);
        //Stream strm = objRequest.GetResponse().GetResponseStream();
        //byte[] bytes = new byte[bufferSize + 1];
        //while (strm.Read(byteBuffer, 0, byteBuffer.Length) > 0)
        //{
        //    Response.BinaryWrite(memStrm.ToArray());
        //    Response.Flush();
        //}
        //Response.Close();

        //memStrm.Close();
        //memStrm.Dispose();
        //strm.Dispose();
        //Response.End();
        //secondcode
        //  System.Net.WebClient webClient = new System.Net.WebClient();
        //  string url = "http://mcnroeerp.com:2122/Upload/speye.txt";
        //  byte[] bytes = webClient.DownloadData(url);
        //  string fileName = (url.Split('/')[url.Split('/').Length - 1]).Split('.')[0];
        //  string fname = "speye.txt";
        // // Response.ContentType = "image/png";
        //  Response.ContentType = GetContentType("json");
        //  Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        ////  Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
        //  Response.BinaryWrite(bytes);
        //  Response.End();

        //Clsstockreportnew clsrptnew = new Clsstockreportnew();
        //string[] param = clsrptnew.fileparameters();
        //string uploadpath = param[0];
        //string downloadpath = param[1];
        //string ftphost = param[3];
        //string ftpuserid = param[4];
        //string ftppasseprd = param[5];
        //string uploadfolder = param[6];
        //ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        //ftpClient.download("Upload/Export_Thane_0003.json", "Export_Thane_0003.json");
        // string fileName = "Export_Thane_0004.json";
        string fileExtension = ".json";

        // Set Response.ContentType
        Response.ContentType = GetContentType(fileExtension);

        // Append header
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

        // Write the file to the Response
        Response.TransmitFile(Server.MapPath("~/FileUpload/" + fileName));

        Response.End();

    }
    struct decryptresult
        {
        public string invoiceno;
        public string invoicedt;
        }

    private decryptresult Decrypt(string qrcoxde)
    {
        decryptresult values = new decryptresult();
        string inv = "";
        string result = "";
        StringBuilder sb = new StringBuilder();

        try
        {
           // string value = "eyJkYXRhIjoie1wiU2VsbGVyR3N0aW5cIjpcIjE5QUFCQ001Njc0SjFaMVwiLFwiQnV5ZXJHc3RpblwiOlwiMTlBTFpQTTQ3MDRIMVpKXCIsXCJEb2NOb1wiOlwiSU5WLUtPTC8xOTIxXCIsXCJEb2NUeXBcIjpcIklOVlwiLFwiRG9jRHRcIjpcIjE3LzA5LzIwMjBcIixcIlRvdEludlZhbFwiOjc4Mzk3LjAwLFwiSXRlbUNudFwiOjE0LFwiTWFpbkhzbkNvZGVcIjpcIjMzMDdcIixcIklyblwiOlwiNWIxMjM0NDQzY2E2MWY4N2FjY2ZjNDk4NDlmZTRjYjAyNmE0YzNlYjg5OThiNDhkMTEzNmUxNzNiZmM3NjkwZVwiLFwiSXJuRHRcIjpcIjIwMjAtMTAtMDEgMTE6MTM6MDBcIn0iLCJpc3MiOiJOSUMifQ";
            char[] data = qrcoxde.ToCharArray();
            Base64Decoder myDecoder = new Base64Decoder(data);
           

            byte[] temp = myDecoder.GetDecoded();
            sb.Append(System.Text.UTF8Encoding.UTF8.GetChars(temp));
            result = sb.ToString();
            
         // string  decrypt    = result.Substring(7, length);
            string decrypt = result.Remove(0,8);
            string[] splitstr = result.Split(',', '\"');
            //string ss = "{\"SellerGstin\":\"19AABCM5674J1Z1\",\"BuyerGstin\":\"19ALZPM4704H1ZJ\",\"DocNo\":\"INV-KOL/1921\",\"DocTyp\":\"INV\",\"DocDt\":\"17/09/2020\",\"TotInvVal\":78397.00,\"ItemCnt\":14,\"MainHsnCode\":\"3307\",\"Irn\":\"5b1234443ca61f87accfc49849fe4cb026a4c3eb8998b48d1136e173bfc7690e\",\"IrnDt\":\"2020-10-01 11:13:00\"}";
            //Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(decrypt);
            //inv = htmlAttributes["DocNo"];
            values.invoiceno = splitstr[16];
            values.invoicedt = splitstr[26];

        }
        catch (Exception ae)
        { 

                }

        return values;
    }

    private void readjson()
    {
        try
        {
            string _ackDt ;
            string _ackno ;
            string irn  ;
            string qrcode ;
            string[] splitqr ;
            string finalqrcode ;
            // string filename = "~/FileUpload/eInv_182010009715336.json";
            // string filename = Server.MapPath(uploadfile.FileName);
            string filename = System.IO.Path.GetFileName(uploadfile.PostedFile.FileName);
            //using (StreamReader file = File.OpenText(Server.MapPath("~/FileUpload/eInv_182010009715336.json")))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Jsonmodel Jsonmodel = (Jsonmodel)serializer.Deserialize(file, typeof(Jsonmodel));
            //}

            // using (StreamReader file = File.OpenText(Server.MapPath(uploadfile.PostedFile.FileName)))

            //{
            StringBuilder displayString = new StringBuilder();
            decryptresult values = new decryptresult();
                string invoiceno = string.Empty;
                string invoicedt = string.Empty;
                string doctype = string.Empty;
                string invno = string.Empty;
            Int32 fileLen;
            Jsonmodel Jsonmodel = new Jsonmodel();
            System.IO.Stream myStream;
            myStream= uploadfile.FileContent;
            fileLen = uploadfile.PostedFile.ContentLength;
            Byte[] Input;
            Input = uploadfile.FileBytes;
            //just now
            //myStream.Read(Input, 0, fileLen);
            //for (int loop1 = 0; loop1 < fileLen; loop1++)
            //{
            //    displayString.Append(Input[loop1].ToString());
            //}
            // string stream = displayString.ToString();
            string stream = System.Text.Encoding.UTF8.GetString(Input);
            // string stream = File.ReadAllText(Server.MapPath("~/FileUpload/eInv_182010009715336.json"));
            //var stream1 = File.ReadAllBytes(uploadfile.PostedFile.FileName);
                string[] splitstr = stream.Split(',', '\"');

                 _ackDt = splitstr[7];
            if(_ackDt=="")
            {
                _ackDt = splitstr[6];
            }
                 _ackno = splitstr[3];
            if (_ackno == "")
            {
                _ackno = splitstr[2];
            }
                irn = splitstr[12];
            if (irn == "")
            {
                irn = splitstr[11];
            }

                qrcode = splitstr[22];
            if (qrcode == "")
            {
                qrcode = splitstr[21];
            }
                splitqr = qrcode.Split('.');
                 finalqrcode = splitqr[1];
                values = Decrypt(finalqrcode);
                invoiceno = values.invoiceno;
                invoicedt = values.invoicedt;
                invoiceno= invoiceno.TrimEnd(new[] { '/', '\\' });
            _ackno = _ackno.TrimStart(new[] { ':' });
            invoicedt = invoicedt.TrimEnd(new[] { '/', '\\' });
            string ackDt = _ackDt.Substring(8, 2) + "/" + _ackDt.Substring(5, 2) + "/" + _ackDt.Substring(0, 4);
            string[] splitqr1 = invoiceno.Split('-');
            //doctype = invoiceno.Substring(0, 3);
            doctype = splitqr1[0];
            DataTable invoicetable = new DataTable();
                invoicetable = updateeinvoice(doctype, invoiceno, invoicedt, _ackno, ackDt, irn, qrcode);
            MessageBox1.ShowInfo("Signed Json uploaded Successfully with Acknowledgement No."+ _ackno, 40, 500);
            //GenerateCode(qrcode);
            //not required
            //    dynamic stuff = JsonConvert.DeserializeObject(stream);
            //    for (int i = 0; i < stuff.elements[0].items.Count; i++)
            //    {

            //        string acno= stuff.elements[0].items[i].AckNo;
            //        string acdate = stuff.elements[0].items[i].AckDt;

            //    }
            //}
            //}
        }
        catch(Exception ex)
        {
            MessageBox1.ShowInfo(ex.Message.ToString(), 40, 500);
        }
       

    }
    private DataTable updateeinvoice(string doctype,string invoiceno,string invoicedt, string ackno,string ackdate,string irn, string qrcode)
    {
        ClsEinvoice einvoice = new ClsEinvoice();
        DataTable dtinvoice = new DataTable();
        dtinvoice = einvoice.updateeinvoice(doctype, invoiceno, invoicedt, ackno, ackdate, irn, qrcode);
        return dtinvoice;
    }
    private void GenerateCode(string name)
    {
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = new ZXing.Common.EncodingOptions
        {
            Width = 930,
            Height = 930
        };
        var result = writer.Write(name);
        string path = Server.MapPath("~/FileUpload/QRImage.jpg");
        var barcodeBitmap = new Bitmap(result);
        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }


        private string GetContentType(string fileExtension)
    {
        if (string.IsNullOrEmpty(fileExtension))
            return string.Empty;

        string contentType = string.Empty;
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
                contentType = "text/HTML";
                break;

            case ".txt":
                contentType = "text/plain";
                break;

            case ".doc":
            case ".rtf":
            case ".docx":
                contentType = "Application/msword";
                break;

            case ".xls":
            case ".xlsx":
                contentType = "Application/x-msexcel";
                break;

            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;

            case ".gif":
                contentType = "image/GIF";
                break;

            case ".pdf":
                contentType = "application/pdf";
                break;
        }

        return contentType;
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class TranDtls
    {
        public string TaxSch { get; set; }
        public string SupTyp { get; set; }
        public string IgstOnIntra { get; set; }
        public string RegRev { get; set; }
        public string EcmGstin { get; set; }
    }

    public class DocDtls
    {
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }
    }

    public class SellerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }

    public class BuyerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Pos { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }

    public class DispDtls
    {
        public string Nm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
    }

    public class ValDtls
    {
        public decimal AssVal { get; set; }
        public decimal IgstVal { get; set; }
        public decimal CgstVal { get; set; }
        public decimal SgstVal { get; set; }
        public int CesVal { get; set; }
        public int StCesVal { get; set; }
        public int Discount { get; set; }
        public int OthChrg { get; set; }
        public decimal RndOffAmt { get; set; }
        public decimal TotInvVal { get; set; }
        public int TotInvValFc { get; set; }
    }

    public class AddlDocDtl
    {
        public string Url { get; set; }
        public string Docs { get; set; }
        public string Info { get; set; }
    }


    public class ItemList
    {
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HsnCd { get; set; }
        public int Qty { get; set; }
        public int FreeQty { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotAmt { get; set; }
        public decimal Discount { get; set; }
        public int PreTaxVal { get; set; }
        public decimal AssAmt { get; set; }
        public int GstRt { get; set; }
        public decimal IgstAmt { get; set; }
        public decimal CgstAmt { get; set; }
        public decimal SgstAmt { get; set; }
        public int CesRt { get; set; }
        public int CesAmt { get; set; }
        public int CesNonAdvlAmt { get; set; }
        public int StateCesRt { get; set; }
        public int StateCesAmt { get; set; }
        public int StateCesNonAdvlAmt { get; set; }
        public int OthChrg { get; set; }
        public decimal TotItemVal { get; set; }
        public object BchDtls { get; set; }
    }
    public class ItemListDump
    {
        public ItemList ItemListdump { get; set; }
    }


    public class GSTList
    {
        public string Version { get; set; }
        public TranDtls TranDtls { get; set; }
        public DocDtls DocDtls { get; set; }
        public SellerDtls SellerDtls { get; set; }
        public BuyerDtls BuyerDtls { get; set; }
        public DispDtls DispDtls { get; set; }
        public ValDtls ValDtls { get; set; }
        public object PayDtls { get; set; }
        public object RefDtls { get; set; }
        public List<AddlDocDtl> AddlDocDtls { get; set; }
        public List<ItemList> ItemList { get; set; }
    }

    public class Root
    {

        public List<GSTList> GSTList { get; set; }
    }


    #region Conver_To_ISO
    public string Conver_To_ISO(string dt)
    {
        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + month + day;
        return dt;
    }
    #endregion
   

    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    //for out put json model
    public class Jsonmodel
    {
        public string AckNo { get; set; }
        public string AckDt { get; set; }
    }

        public void ToCSV(DataTable dtDataTable, string strFilePath)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);
        //headers  
        for (int i = 0; i < dtDataTable.Columns.Count; i++)
        {
            sw.Write(dtDataTable.Columns[i]);
            if (i < dtDataTable.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(","))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }


}