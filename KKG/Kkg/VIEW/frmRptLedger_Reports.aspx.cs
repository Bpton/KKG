#region Namespace
using BAL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class VIEW_frmRptLedger_Reports : System.Web.UI.Page
{
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdledger.ClientID + "', 400, '100%' , 25 ,true); </script>", false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlspan').multiselect({ includeSelectAllOption: true  });});</script>", false);
        if (!IsPostBack)
        {
            InisiliseDate();
            pnlDisplay.Style["display"] = "";
            LoadDepot();
            DateTime date1 = DateTime.Now;
            var firstDay = new DateTime(date1.Year, date1.Month, 1);
            this.txtfromdate.Text = Convert.ToString(firstDay);

            if (Convert.ToString(Request.QueryString["FromPage"]) == "TrialBalance")
            {
                DateTime date = new DateTime();
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                string Fromdt = Convert.ToString(Request.QueryString["Fromdt"]);
                string Todt = Convert.ToString(Request.QueryString["Todt"]);
                string Regionid = Convert.ToString(Request.QueryString["Region"]);
                string LedgerID = Convert.ToString(Request.QueryString["LedgerID"]);
                txtfromdate.Text = Convert.ToString(firstDayOfMonth);
                txttodate.Text = Todt;
                ddldepot.SelectedValue = Regionid;
                LoadGroup(Regionid);
                ddlledger.SelectedValue = LedgerID;
                BindGridFromQueryString(Convert.ToString(firstDayOfMonth), Todt, Regionid, LedgerID);
            }
            else if (Convert.ToString(Request.QueryString["FromPage"]) == "ProfitLoss")
            {               
                string Asondt = Convert.ToString(Request.QueryString["Asondt"]);
                string Regionid = Convert.ToString(Request.QueryString["Region"]);
                string LedgerID = Convert.ToString(Request.QueryString["LedgerID"]);
                txtfromdate.Text = "01-04-2020";
                txttodate.Text = Asondt;
                ddldepot.SelectedValue = Regionid;
                LoadGroup(Regionid);
                ddlledger.SelectedValue = LedgerID;
                BindGridFromQueryString(txtfromdate.Text, Asondt, Regionid, LedgerID);
            }

            else if (Request.QueryString["frmdate"] != null)
            {
                DateTime date = new DateTime();
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                string frmdate = Request.QueryString["frmdate"].ToString();
                string todate = Request.QueryString["todate"].ToString();
                string region = Request.QueryString["region"].ToString();
                string ledgerid = Request.QueryString["ledgerid"].ToString();
                ddlviewmode.SelectedValue = "B";

                this.txtfromdate.Text = Convert.ToString(firstDayOfMonth);
                this.txttodate.Text = todate;
                ddldepot.SelectedValue = region.Trim();
                LoadGroup(region);
                ddlledger.SelectedValue = ledgerid.Trim();
                BindGridFromQueryString(Convert.ToString(firstDayOfMonth), todate, region, ledgerid.Trim());
            }
        }

        string lastvalue = string.Empty;
        foreach (ListItem item in ddldepot.Items)
        {
            if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "blue");
                lastvalue = item.Value;
            }
        }
        if (lastvalue == "-1" || lastvalue == "-2" || lastvalue == "-4" || lastvalue == "-3" || lastvalue == "-8")
        {
            //  ddldepot.Items.Remove(ddldepot.Items.FindByValue(lastvalue));
        }
    }
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
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;

        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;

        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }
    public void LedgerNameBind()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable dtledger = new DataTable();
            string DEPOTID = "";

            var query1 = from ListItem item in ddldepot.Items where item.Selected select item;

            foreach (ListItem item in query1)
            {
                // item ...
                DEPOTID += item.Value + ',';

            }
            if (DEPOTID.Length > 1)
            {
                DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);
            }

            dtledger = clsrpt.BindForLedgerReport_DepotWise(DEPOTID.Trim());
            if (dtledger.Rows.Count > 0)
            {
                ddlledger.Items.Clear();
                ddlledger.Items.Add(new ListItem("Select", "0"));
                ddlledger.DataSource = dtledger;
                ddlledger.DataTextField = "LedgerName";
                ddlledger.DataValueField = "LedgerId";
                ddlledger.DataBind();
                dtledger.Clear();
            }
            else
            {
                ddlledger.Items.Clear();
                ddlledger.Items.Add(new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //protected void txtfromdate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txtfromdate.Text);
    //    CalendarExtender3.StartDate = oDate1;
    //}
    //protected void txttodate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txttodate.Text);
    //    CalendarExtender2.EndDate = oDate1;
    //}

    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable depot = new DataTable();
            depot = clsrpt.Region_foraccounts(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["USERID"]));

            if (depot.Rows.Count > 0)
            {

                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = depot;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
                
                if (depot.Rows.Count == 1)
                {
                    this.ddldepot.SelectedValue = depot.Rows[0]["BRID"].ToString().Trim();
                    LedgerNameBind();
                }
                depot.Clear();
            }
           
            else
            {
                ddldepot.Items.Clear();
                ddldepot.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    public void LoadGroup(String Deptid)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtledger = new DataTable();
        dtledger = clsrpt.BindForLedgerReport_DepotWise(Deptid.Trim());
        if (dtledger.Rows.Count > 0)
        {
            ddlledger.Items.Clear();
            ddlledger.Items.Add(new ListItem("Select", "0"));
            ddlledger.DataSource = dtledger;
            ddlledger.DataTextField = "LedgerName";
            ddlledger.DataValueField = "LedgerId";
            ddlledger.DataBind();
            dtledger.Clear();
        }
        else
        {
            ddlledger.Items.Clear();
            ddlledger.Items.Add(new ListItem("Select", "0"));
        }
    }    

    #region ddldepot_SelectedIndexChanged
    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LedgerNameBind();
    }
    #endregion

    #region ddlcalender_SelectedIndexChanged
    protected void ddlcalender_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataTable dt = new DataTable();
        string Tag = string.Empty;

        if (ddlcalender.SelectedValue == "5")
        {
            dt = clsreport.BindJC();
            lblcalender.Text = "JC";

            if (dt.Rows.Count > 0)
            {
                ddlspan.Items.Clear();
                //ddlspan.Items.Add(new ListItem("Select", "0"));
                ddlspan.AppendDataBoundItems = true;
                ddlspan.DataSource = dt;
                ddlspan.DataValueField = "JCID";
                ddlspan.DataTextField = "NAME";
                ddlspan.DataBind();
            }
        }
        else
        {
            if (ddlcalender.SelectedValue == "1")
            {
                Tag = "Y";
                lblcalender.Text = "YEAR";
            }
            else if (ddlcalender.SelectedValue == "2")
            {
                lblcalender.Text = "QUARTER";
                Tag = "Q";
            }
            else if (ddlcalender.SelectedValue == "3")
            {
                lblcalender.Text = "MONTH";
                Tag = "M";
            }
            else if (ddlcalender.SelectedValue == "4")
            {
                lblcalender.Text = "WEEK";
                Tag = "W";
            }
            else
            {
                txtfromdate.Enabled = true;
                txttodate.Enabled = true;
                Imgfrom.Enabled = true;
                ImgToDate.Enabled = true;
            }

            dt = clsreport.BindTimeSpan_New(Tag, Session["FINYEAR"].ToString());

            if (dt.Rows.Count > 0)
            {
                ddlspan.Items.Clear();
                //ddlspan.Items.Add(new ListItem("Select", "0"));
                ddlspan.AppendDataBoundItems = true;
                ddlspan.DataSource = dt;
                ddlspan.DataValueField = "TIMESPAN";
                ddlspan.DataTextField = "TIMESPAN";
                ddlspan.DataBind();
            }
            else
            {

            }
        }
    }
    #endregion

    #region ddlspan_SelectedIndexChanged
    protected void ddlspan_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataTable dt = new DataTable();
        string Span = string.Empty;
        var query = from ListItem item in ddlspan.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            Span += item.Value + ',';

        }
        Span = Span.Substring(0, Span.Length - 1);

        string Tag = string.Empty;
        Tag = ddlcalender.SelectedValue.Trim();
        dt = clsreport.FetchDateRange(Span.Trim(), Tag, Session["FINYEAR"].ToString());

        if (dt.Rows.Count > 0)
        {
            txtfromdate.Text = dt.Rows[0]["STARTDATE"].ToString().Trim();
            txttodate.Text = dt.Rows[0]["ENDDATE"].ToString().Trim();
            txtfromdate.Enabled = false;
            txttodate.Enabled = false;
            Imgfrom.Enabled = false;
            ImgToDate.Enabled = false;
        }
        else
        {

        }
    }
    #endregion

    #region BindGrid
    protected void BindGrid()
    {

        ClsStockReport clsrpt = new ClsStockReport();
        DataSet dtDisplayGrid = new DataSet();
        DataTable dtDisplayGrid_details = new DataTable();
        string DEPOTID = "";

        //if (ddldepot.SelectedValue != "")
        //{
        var query1 = from ListItem item in ddldepot.Items where item.Selected select item;

        foreach (ListItem item in query1)
        {
            // item ...
            DEPOTID += item.Value + ',';

        }
        if (DEPOTID.Length > 1)
        {
            DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);
        }
        string finyearid = clsrpt.GetFinYearID(HttpContext.Current.Session["FINYEAR"].ToString().Trim());

        Session["LedgerReport_forexcel"] = null;

        dtDisplayGrid = clsrpt.BindLedgerReport(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddlledger.SelectedValue.Trim(), DEPOTID, finyearid);

        if (dtDisplayGrid.Tables[0].Rows.Count > 0)
        {
            this.grdledger.DataSource = dtDisplayGrid.Tables[0];
            this.grdledger.DataBind();
            Session["LedgerReport"] = dtDisplayGrid;
            Session["LedgerReport_forexcel"] = dtDisplayGrid.Tables[0]; ;
            //this.childgrid.DataSource = dtDisplayGrid_details;
            //this.childgrid.DataBind();
            //hdn_excel.Value = "1";
        }
        else
        {
            this.grdledger.DataSource = null;
            this.grdledger.DataBind();

            //this.childgrid.DataSource = null;
            //this.childgrid.DataBind();
        }
    }

    #endregion

    public void BindGridFromQueryString(string txtfrmdt, string todt, string region, string ledger)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet dtDisplayGrid = new DataSet();
        DataTable dtDisplayGrid_details = new DataTable();

        string finyearid = clsrpt.GetFinYearID(HttpContext.Current.Session["FINYEAR"].ToString().Trim());

        Session["LedgerReport_forexcel"] = null;

        dtDisplayGrid = clsrpt.BindLedgerReport(txtfrmdt.Trim(), todt.Trim(), ledger.Trim(), region.Trim(), finyearid);

        if (dtDisplayGrid.Tables[0].Rows.Count > 0)
        {
            this.grdledger.DataSource = dtDisplayGrid.Tables[0];
            this.grdledger.DataBind();
            Session["LedgerReport"] = dtDisplayGrid;
            Session["LedgerReport_forexcel"] = dtDisplayGrid.Tables[0]; ;
            //this.childgrid.DataSource = dtDisplayGrid_details;
            //this.childgrid.DataBind();
            //hdn_excel.Value = "1";
        }
        else
        {
            this.grdledger.DataSource = null;
            this.grdledger.DataBind();

            //this.childgrid.DataSource = null;
            //this.childgrid.DataBind();
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void grdledger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dt = new DataTable();
        if (ddlviewmode.SelectedValue == "B" || ddlviewmode.SelectedValue == "C")
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;

                if (drv["LedgerName"].ToString().Contains("Opening Balance"))
                {
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    e.Row.Font.Bold = true;

                    e.Row.CssClass = "subtotal";

                }

                if (drv["LedgerName"].ToString().Contains("Closing Balance"))
                {
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    e.Row.Font.Bold = true;
                    e.Row.CssClass = "zone";

                }
                if (drv["LedgerName"].ToString().Contains("TOTAL"))
                {
                    e.Row.CssClass = "grandtotal";

                }

                //e.Row.Font.Bold = true;
                e.Row.ForeColor = Color.Black;

                Label lblAccEntryID = (Label)e.Row.FindControl("lblAcc");
                string txtAccEntryID = lblAccEntryID.Text.Trim();
                if (txtAccEntryID != "")
                {
                    dt = clsrpt.BindVoucher_credit_debit(txtAccEntryID);
                    GridView GV_LEDGER = (GridView)e.Row.FindControl("GV_LEDGER");
                    if (dt.Rows.Count > 0)
                    {
                        GV_LEDGER.DataSource = dt;
                        GV_LEDGER.DataBind();
                    }
                    else
                    {
                        GV_LEDGER.DataSource = null;
                        GV_LEDGER.DataBind();
                    }

                }

                else
                {

                }

            }
        }
    }

    protected void GV_LEDGER_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ddlviewmode.SelectedValue == "B")
            {
                e.Row.Font.Bold = true;
                e.Row.ForeColor = Color.Black;
            }
            if (ddlviewmode.SelectedValue == "C")
            {
                ClsStockReport clsstock = new ClsStockReport();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = Color.Black;

                    Label LblLedgerId = (Label)e.Row.FindControl("LblLedgerId");
                    Label LblentryId = (Label)e.Row.FindControl("lblEntryID");
                    //GridView GV_INVOICE = (GridView)e.Row.FindControl("GV_INVOICE");
                    GridView GV_COSTCENTRE = (GridView)e.Row.FindControl("GV_COSTCENTRE");
                    string txtLedgerId = LblLedgerId.Text.Trim();
                    string AccEntryID = LblentryId.Text.Trim();
                    DataSet ds = new DataSet();
                    ds = clsstock.BindAccVoucher_Ledger_Voucher(txtLedgerId, AccEntryID);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            decimal InvoiceAmt = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("InvoiceAmt"));
                            HttpContext.Current.Session["InvoiceAmt"] = InvoiceAmt.ToString();
                            decimal AmtReceived = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("AmtReceived"));
                            HttpContext.Current.Session["AmtReceived"] = AmtReceived.ToString();
                            decimal OutStanding = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("OutStanding"));
                            HttpContext.Current.Session["OutStanding"] = OutStanding.ToString();
                            //GV_INVOICE.DataSource = ds.Tables[1];
                            //GV_INVOICE.DataBind();
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GV_COSTCENTRE.DataSource = ds.Tables[0];
                            GV_COSTCENTRE.DataBind();
                        }
                    }
                    else
                    {
                        //GV_INVOICE.DataSource = null;
                        //GV_INVOICE.DataBind();

                        GV_COSTCENTRE.DataSource = null;
                        GV_COSTCENTRE.DataBind();
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {

                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                //Session["BSID"] = Request.QueryString["BSID"].ToString();
                ClsStockReport clsInvc = new ClsStockReport();
                DataSet dt = new DataSet();
                string Vouchertype = string.Empty;

                foreach (GridViewRow gvrow in grdledger.Rows)
                {
                    //vouchertype
                    Label lblAcc = (Label)gvrow.FindControl("lblAcc");
                    if (lblAcc.Text == Hdn_Print.Value.Trim())
                    {
                        Label lbl3 = (Label)gvrow.FindControl("lbl3");
                        Vouchertype = lbl3.Text.Trim();

                    }
                }

                dt = clsInvc.InvoiceDetails_ForPrint(Hdn_Print.Value.Trim());
                dt = clsInvc.InvoiceDetails_ForPrint(Hdn_Print.Value.Trim());
                dt = clsInvc.InvoiceDetails_ForPrint(Hdn_Print.Value.Trim());
                if (dt.Tables.Count == 1)
                {


                    string upath = "";

                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SI")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate)
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "&&PSID=" + dt.Tables[0].Rows[0]["EXPORTTAG"].ToString().Trim() + "";
                        }
                        else
                        {
                            upath = "frmPrintPopUp.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "&&PSID=" + dt.Tables[0].Rows[0]["EXPORTTAG"].ToString().Trim() + "";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }

                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SLR")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate) /**** queary string change SLRID TO pid by P.basu on 16012020 for print issue****/
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                        }
                        else   /**** queary string change SLRID TO pid by P.basu on 16012020 for print issue****/
                        {
                            upath = "frmPrintPopUp.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }

                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "ST")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate)
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=46";
                        }
                        else
                        {
                            upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=46";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SR")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate)
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=47";
                        }
                        else
                        {
                            //upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=47";
                            upath = "frmRptPurchasePrint.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + " ";
                            //string upath = "frmRptInvoicePrint.aspx?Stnid=" + hdn_transferid.Value.Trim() + "&MenuId=" + this.Request.QueryString["MENUID"].ToString().Trim() + "";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SRD")
                    {
                        upath = "frmRptInvoicePrint.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=65";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SRD1")
                    {
                        upath = "frmRptInvoicePrint.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=2778";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SD")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate)
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                        }
                        else
                        {
                            upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "CL")
                    {
                        upath = "frmRptClaimPrint.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&CLID=" + dt.Tables[0].Rows[0]["CLAIM_TYPEID"].ToString().Trim() + "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }
                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "ACC")
                    {
                        upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }

                    if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "TB")
                    {
                        int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                        int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                        if (invoicedate <= checkdate)
                        {
                            upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&TAG=" + dt.Tables[0].Rows[0]["TAG"].ToString().Trim() + "&&MenuId=45";
                        }
                        else
                        {
                            upath = "frmRptInvoicePrint.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&TAG=" + dt.Tables[0].Rows[0]["TAG"].ToString().Trim() + "&&MenuId=45";
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                    }


                }
                else if (dt.Tables.Count > 1)
                {
                    string upath = "";

                    if (Vouchertype == "Journal")
                    {
                        Session["Journal"] = dt.Tables[1];
                        if (dt.Tables[1].Rows[0]["TAG"].ToString().Trim() == "ACC")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[1].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?AdvRecpt_ID=" + dt.Tables[1].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[1].Rows[0]["BSID"].ToString().Trim() + "";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                            }
                            else
                            {
                                upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + dt.Tables[1].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[1].Rows[0]["BSID"].ToString().Trim() + "";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                            }
                        }
                    }

                    else if (Vouchertype == "Advance Receipt")
                    {
                        Session["Journal"] = dt.Tables[1];
                        if (dt.Tables[1].Rows[0]["TAG"].ToString().Trim() == "ACC")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[1].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?AdvRecpt_ID=" + dt.Tables[1].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[1].Rows[0]["BSID"].ToString().Trim() + "";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                            }
                            else
                            {
                                upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + dt.Tables[1].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[1].Rows[0]["BSID"].ToString().Trim() + "";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                            }
                        }
                    }
                    else
                    {

                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SI")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                            }
                            else
                            {
                                upath = "frmPrintPopUp.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }

                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SLR")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?SLRID=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                            }
                            else
                            {
                                upath = "frmPrintPopUp.aspx?SLRID=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&BSID=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "";
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }

                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "ST")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=46";
                            }
                            else
                            {
                                upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=46";
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }
                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SR")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                            }
                            else
                            {
                                upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }
                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "SD")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                            }
                            else
                            {
                                upath = "frmPrintPopUp.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&MenuId=45";
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }
                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "CL")
                        {
                            upath = "frmRptClaimPrint.aspx?pid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&CLID=" + dt.Tables[0].Rows[0]["CLAIM_TYPEID"].ToString().Trim() + "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }


                        if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "TB")
                        {
                            int invoicedate = Convert.ToInt32(Conver_To_ISO(dt.Tables[0].Rows[0]["SALEINVOICEDATE"].ToString().Trim()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));
                            if (invoicedate <= checkdate)
                            {
                                upath = "frmRptInvoicePrint_VAT.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&TAG=" + dt.Tables[0].Rows[0]["TAG"].ToString().Trim() + "&&MenuId=45";
                            }
                            else
                            {
                                upath = "frmRptInvoicePrint.aspx?Stnid=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&TAG=" + dt.Tables[0].Rows[0]["TAG"].ToString().Trim() + "&&MenuId=45";
                            }

                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                        }
                    }


                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnaccPrint_Click
    protected void btnaccPrint_Click(object sender, EventArgs e)
    {
        try
        {

            //Session["BSID"] = Request.QueryString["BSID"].ToString();
            ClsStockReport clsInvc = new ClsStockReport();
            DataSet dt = new DataSet();
            string Vouchertype = string.Empty;


            dt = clsInvc.InvoiceDetails_ForPrintAcc(Hdn_Print.Value.Trim());
            if (dt.Tables.Count == 1)
            {
                string upath = "";


                if (dt.Tables[0].Rows[0]["TAG"].ToString().Trim() == "ACC")
                {
                    upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + dt.Tables[0].Rows[0]["SALEINVOICEID"].ToString().Trim() + "&&Voucherid=" + dt.Tables[0].Rows[0]["BSID"].ToString().Trim() + "&&Isautovoucher=" + dt.Tables[0].Rows[0]["Isautovoucher"].ToString().Trim() + "&&AUTOACCENTRYID=" + dt.Tables[0].Rows[0]["AUTOACCENTRYID"].ToString().Trim() + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                }
            }
            dt.Clear();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // return;
    }

    #region Export To Excel
    protected void ExportToExcel(object sender, EventArgs e)
    {

        if (ddlviewmode.SelectedValue == "A")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["LedgerReport_forexcel"];

            //if (dt.Columns.Count > 11)
            //{
            //    dt.Columns.RemoveAt(11);
            //    dt.AcceptChanges();
            //    dt.Columns.RemoveAt(11);
            //    dt.AcceptChanges();
            //}

            Session["LedgerReport_forexcel"] = dt;

            Response.Clear();
            Response.AddHeader("content-disposition", "inline; filename=" + "LedgerReport_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            htmlWrite.Write("<table><tr><td colspan=11><b>Kkg Industry</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=11><b>Ledger Report : " + ddlledger.SelectedItem.Text.Trim() + "</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=11><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            //this.ClearControls(grdledger);
            gv_for_excel.Visible = true;
            gv_for_excel.DataSource = dt;
            gv_for_excel.DataBind();

            for (int i = 0; i < gv_for_excel.HeaderRow.Cells.Count; i++)
            {
                gv_for_excel.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
                gv_for_excel.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");
            }

            gv_for_excel.RowStyle.Font.Bold = true;
            gv_for_excel.RowStyle.Font.Size = 10;
            gv_for_excel.RowStyle.Height = 20;
            gv_for_excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            gv_for_excel.Visible = false;

        }
        else
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["LedgerReport_forexcel"];
            if (dt.Columns.Count > 11)
            {
                //dt.Columns.RemoveAt(10);
                //dt.AcceptChanges();
                //dt.Columns.RemoveAt(11);
                //dt.AcceptChanges();
            }
            Session["LedgerReport_forexcel"] = dt;
            Response.AddHeader("content-disposition", "inline; filename=" + "LedgerReport_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            ImageButton img = new ImageButton();
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            htmlWrite.Write("<table><tr><td colspan=10>Kkg Industry</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=10><b>Ledger Report : " + ddlledger.SelectedItem.Text.Trim() + "</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=104><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            //grdledger.Visible = true;
            grdledger.DataSource = dt;
            grdledger.DataBind();
            grdledger.Columns[0].Visible = false;
            //grdledger.Columns[1].Visible = false;

            for (int i = 0; i < grdledger.Rows.Count; i++)
            {
                // string t1 = grdledger.Rows[i].Cells[1].Text;
                GridView GV_LEDGER = (GridView)grdledger.Rows[i].Cells[15].FindControl("GV_LEDGER");
                //GridView txtQtyval2 = grdledger.Rows[i].FindControl("txtRMAPartQuantity") as TextBox;
                GV_LEDGER.Columns[0].Visible = false;

                //Label lbl15 = (Label)GV_LEDGER.Rows[i].Cells[2].FindControl("lbl15");

                //if (lbl15.Text.ToString()=="")
                //{
                //    GV_LEDGER.Rows[i].BackColor = Color.PeachPuff;
                //    GV_LEDGER.Rows[i].BackColor = Color.Gray;
                //}
            }



            grdledger.Columns[13].Visible = false;
            grdledger.Columns[14].Visible = false;
            for (int i = 0; i < grdledger.HeaderRow.Cells.Count; i++)
            {
                grdledger.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
                grdledger.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");

            }
            grdledger.RowStyle.Font.Bold = true;
            grdledger.RowStyle.Font.Size = 10;
            grdledger.RowStyle.Height = 20;
            grdledger.RowStyle.Wrap = false;
            grdledger.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
    }

   
    private void ClearControls(Control control)
    {
        try
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }
            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else
                    if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
        }
        catch (Exception e)
        {
            /*lblError.Visible = true;
            lblError.Text = ee.Message;*/
        }
        return;
    }
    #endregion

    protected void grdledger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //LinkButton btn_Link = (LinkButton)sender;
        //GridViewRow gvr = (GridViewRow)btn_Link.NamingContainer;
        //Label lblLedgerName = (Label)gvr.FindControl("lbl2");

        Control ctrl = e.CommandSource as Control;
        if (ctrl != null)
        {
            GridViewRow gvRow = ctrl.Parent.NamingContainer as GridViewRow;
            Label lblLedgerName = (Label)gvRow.FindControl("lbl3");  // Find Your Control here  
            Label lblParticulars = (Label)gvRow.FindControl("lbl2");

            ClsStockReport obj = new ClsStockReport();
            DataTable dtobj = new DataTable();
            string VoucherId = "", InvoiceId = "", BRANCHID = "";
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string AccEntryID = commandArgs[0];
            string Docno = commandArgs[1];
            dtobj = obj.FetchInvoiceId(AccEntryID);
            VoucherId = dtobj.Rows[0]["VOUCHERTYPEID"].ToString();
            InvoiceId = dtobj.Rows[0]["INVOICEID"].ToString();
            BRANCHID = dtobj.Rows[0]["BRANCHID"].ToString();
            // VoucherType= dtobj.Rows[0]["VOUCHERTYPE"].ToString();
            if (e.CommandName == "VoucherNo")
            {
                if (VoucherId == "11" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Factory TradingSale And Sale Invoice FG Query String*/
                {
                    string upath = "frmInvoiceMM_FAC.aspx?InvId=" + InvoiceId + "&&MENUID=2782&&BSID=33F6AC5E-1F37-4B0F-B959-D1C900BB43A5&&CHECKER=TRUE&&CHALLAN=FALSE&&TYPE=LGRREPORT";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
                else if (VoucherId == "16" || VoucherId == "15" || VoucherId == "13" || VoucherId == "14")/*Advance Receipt And Advance Payment Credit Note and Debit Note Query String*/
                {
                    string upath = "frmAccAdvanceVoucher.aspx?InvId=" + AccEntryID + "&&VoucherId=" + VoucherId + "&&CHECKER=TRUE&&TYPE=LGRREPORT&&BRANCHID=" + ddldepot.SelectedValue.ToString() + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
                else if (VoucherId == "4" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Factory Despatch Query String*/
                {
                    string upath = "frmFactoryStockTransfer.aspx?InvId=" + InvoiceId + "&&CHECKER=TRUE&&TYPE=LGRREPORT&&MENUID=1660";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
                else if (VoucherId == "12" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Grn AND Purchase Received Query String*/
                {
                    if (lblParticulars.Text == "Job Work Expenses")
                    {
                        string upath = "frmPurchaseBillMaker_GST.aspx?InvId=" + InvoiceId + "&&MENUID=2738&&CHECKER=TRUE&&TYPE=LGRREPORT&&BRANCHID=" + ddldepot.SelectedValue.ToString() + "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                    }
                    else
                    {
                        string upath = "frmStockReceived_FAC.aspx?InvId=" + InvoiceId + "&&MENUID=2809&&CHECKER=TRUE&&TYPE=LGRREPORT&&BRANCHID=" + ddldepot.SelectedValue.ToString() + "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                    }
                }
                else if (VoucherId == "5" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Transporter Query String*/
                {
                    string upath = "frmTransporterBillV2_FAC.aspx?InvId=" + InvoiceId + "&&MENUID=2861&&CHECKER=TRUE&&TYPE=LGRREPORT";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }

                else if ((VoucherId == "1" || VoucherId == "2" || VoucherId == "9" || VoucherId == "10" || VoucherId == "11" || VoucherId == "12") && (lblLedgerName.Text != "Sales" && lblLedgerName.Text != "Purchase" && lblLedgerName.Text != "Challan"))/*All Journal Query String*/
                {
                    string upath = "frmAccVoucher.aspx?InvId=" + AccEntryID + "&&CHECKER=TRUE&&AUTOOPEN=FALSE&&AUTOVOUCHERID=0&&AUTOVOUCHERDATE=0&&TYPE=LGRREPORT&&BRANCHID=" + ddldepot.SelectedValue.ToString() + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
                else if (VoucherId == "8" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Sale Query String*/
                {
                    string upath = "frmSaleReturn_FAC.aspx?InvId=" + InvoiceId + "&&MENUID=2883&&CHECKER=TRUE&&TYPE=LGRREPORT";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }

                else if (VoucherId == "20" && (BRANCHID == "15E687A6-CD85-412A-ABD4-B52AB91CADE0" || BRANCHID == "14857CFC-2450-4D52-B93A-486D9507A1BE"))/*Purchase Return Query String*/
                {
                    string upath = "frmPurchaseReturn_MM.aspx?InvId=" + InvoiceId + "&&MENUID=2821&&CHECKER=TRUE&&TYPE=LGRREPORT";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
            }
        }
    }
}