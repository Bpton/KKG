#region Namespace
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Drawing;
using System.Collections.Generic;
using Account;
#endregion


public partial class VIEW_frmRptPartyOutstandingReport : System.Web.UI.Page
{
    //ArrayList arry = new ArrayList();

    #region Page_Init
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvSummaryreport.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlgroup').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlledger').multiselect({includeSelectAllOption: true});});</script>", false);

        if (!IsPostBack)
        {
            InisiliseDate();
            this.pnlDisplay.Style["display"] = "";

            //string Customer = Request.QueryString["custid"];
            //string Depot = Request.QueryString["depot"];
            //string fromdate = Request.QueryString["fdate"];
            //string todate = Request.QueryString["todate"];
            //string BSID = Request.QueryString["bsid"];


            ddldepot.SelectedValue = Request.QueryString["DepoID"];
            //this.txtfromdate.Text = fromdate;
            //this.txttodate.Text = todate;

            this.LoadGroup();
            this.LoadDepot();
            this.LoadBusinessSegment();
            //this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());
            //this.txtfromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //this.txttodate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Session["outstanding"] = null;
            this.gvSummaryreport.DataSource = null;
            this.gvSummaryreport.DataBind();

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

            //if (Request.QueryString["custid"] != null && Request.QueryString["depot"] != null)
            //{
            //    string Customer = Request.QueryString["custid"];
            //    string Depot = Request.QueryString["depot"];
            //    string fromdate = Request.QueryString["fdate"];
            //    string todate = Request.QueryString["todate"];
            //    string BSID = Request.QueryString["bsid"];

                
            //    ddldepot.SelectedValue = Request.QueryString["DepoID"];
            //    this.txtfromdate.Text = fromdate;
            //    this.txttodate.Text = todate;

            //    this.LoadGroup();
            //    this.LoadBusinessSegment();
            //    Session["outstanding"] = null;
            //    this.gvSummaryreport.DataSource = null;
            //    this.gvSummaryreport.DataBind();
                
            //}
            //else
            //{
            //    this.LoadDepot();
            //    this.LoadGroup();
            //    LoadBusinessSegment();
                
            //    this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
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
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;

        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;

        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }

    #region LoadDepot
    public void LoadDepot()
    {

        //try
        //{
        //    ClsStockReport clsreport = new ClsStockReport();
        //    DataTable dt = clsreport.BindDepot_Primary_CUMULATIVE();
        //    //ddldepot.Items.Clear();
        //    //ddldepot.Items.Add(new ListItem("SELECT DEPOT", "0"));
        //    ddldepot.AppendDataBoundItems = true;
        //    ddldepot.DataSource = dt;
        //    ddldepot.DataValueField = "BRID";
        //    ddldepot.DataTextField = "BRNAME";
        //    ddldepot.DataBind();

        //}

        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable depot = new DataTable();
            depot = clsrpt.Region(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["IUSERID"]));

            if (depot.Rows.Count > 0)
            {
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = depot;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();

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

    #region LoadBusinessSegment
    public void LoadBusinessSegment()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindBusinessegment();
            ddlbsegment.Items.Clear();
            ddlbsegment.Items.Add(new ListItem("ALL", "0"));
            ddlbsegment.AppendDataBoundItems = true;
            ddlbsegment.DataSource = dt;
            ddlbsegment.DataValueField = "ID";
            ddlbsegment.DataTextField = "NAME";
            ddlbsegment.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    
    #region ddldepot_OnSelectedIndexChanged
    protected void ddldepot_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //ClsStockReport clsreport = new ClsStockReport();
        //DataTable dt = new DataTable();
        //dt = clsreport.BindCustomerbyDepot(ddldepot.SelectedValue);
        //if (dt.Rows.Count > 0)
        //{
        //    this.ddlledger.Items.Clear();
        //    //this.ddlledger.Items.Add(new ListItem("ALL", "0"));
        //    this.ddlledger.AppendDataBoundItems = true;
        //    this.ddlledger.DataSource = dt;
        //    this.ddlledger.DataTextField = "CUSTOMERNAME";
        //    this.ddlledger.DataValueField = "CUSTOMERID";
        //    this.ddlledger.DataBind();
        //}
        //else
        //{
        //    this.ddlledger.Items.Clear();
        //    //this.ddlledger.Items.Add(new ListItem("Select", "0"));
        //}

    }
    #endregion

    #region LoadGroup
    public void LoadGroup()
    {

        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindAccountsGroup();
            ddlgroup.AppendDataBoundItems = true;
            ddlgroup.DataSource = dt;
            ddlgroup.DataValueField = "code";
            ddlgroup.DataTextField = "grpName";
            ddlgroup.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region ddlbsegment_OnSelectedIndexChanged

    //protected void ddlbsegment_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ClsStockReport clsreport = new ClsStockReport();
    //    DataTable dt = new DataTable();
    //    dt = clsreport.BindPartyBSWise(ddlbsegment.SelectedValue);
    //    if (dt.Rows.Count > 0)
    //    {
    //        this.ddlledger.Items.Clear();
    //        this.ddlledger.Items.Add(new ListItem("ALL", "0"));
    //        this.ddlledger.AppendDataBoundItems = true;
    //        this.ddlledger.DataSource = dt;
    //        this.ddlledger.DataTextField = "CUSTOMERNAME";
    //        this.ddlledger.DataValueField = "CUSTOMERID";
    //        this.ddlledger.DataBind();
    //    }
    //    else
    //    {
    //        this.ddlledger.Items.Clear();
    //        this.ddlledger.Items.Add(new ListItem("Select", "0"));
    //    }

    //}
    #endregion

    #region ddlbsegment_OnSelectedIndexChanged
    //protected void ddlbsegment_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string BusinessSegmentID = string.Empty;
    //    string DEPOTID = string.Empty;
    //    var querysegment = from ListItem item in ddlbsegment.Items where item.Selected select item;
    //    foreach (ListItem item in querysegment)
    //    {
    //        BusinessSegmentID += item.Value + ',';
    //    }
    //    BusinessSegmentID = BusinessSegmentID.Substring(0, BusinessSegmentID.Length - 1);


    //    var querydepot = from ListItem item in ddldepot.Items where item.Selected select item;
    //    foreach (ListItem item in querydepot)
    //    {
    //        DEPOTID += item.Value + ',';
    //    }
    //    DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);

    //    this.LoadCustomersBSIDWise(BusinessSegmentID);
    //}
    #endregion

    #region LoadCustomers Business Segment Wise
    public void LoadCustomersBSIDWise(string BusinessSegmentID)
    {
        try
        {
            //ClsStockReport clsreport = new ClsStockReport();
            //DataTable dt = clsreport.BindPartyBSWise(ddlbsegment.SelectedValue);
            //ddlledger.Items.Clear();
            //ddlledger.Items.Add(new ListItem("ALL", "0"));
            //ddlledger.AppendDataBoundItems = true;
            //ddlledger.DataSource = dt;
            //ddlledger.DataValueField = "CUSTOMERID";
            //ddlledger.DataTextField = "CUSTOMERNAME";
            //ddlledger.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadLedger
    public void LoadLedger()
    {

        //try
        //{
        //    ClsStockReport clsrpt = new ClsStockReport();
        //    DataTable ledger = new DataTable();
        //    ledger = clsrpt.BindLedger(Convert.ToString(Session["IUSERID"]));

        //    if (ledger.Rows.Count > 0)
        //    {
        //        ddlledger.AppendDataBoundItems = true;
        //        ddlledger.DataSource = ledger;
        //        ddlledger.DataTextField = "LedgerName";
        //        ddlledger.DataValueField = "LedgerId";
        //        ddlledger.DataBind();

        //    }
        //    else
        //    {
        //        ddlledger.Items.Clear();
        //        ddlledger.Enabled = true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    string msg = ex.Message;
        //}

        //try
        //{
        //    ClsStockReport clsreport = new ClsStockReport();
        //    DataTable dt = clsreport.BindLedger();
        //    ddlledger.Items.Clear();
        //    //ddlledger.Items.Add(new ListItem("ALL", "0"));
        //    ddlledger.AppendDataBoundItems = true;
        //    ddlledger.DataSource = dt;
        //    ddlledger.DataValueField = "LedgerId";
        //    ddlledger.DataTextField = "LedgerName";
        //    ddlledger.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    #endregion

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindGrid
    protected void BindGrid()
    {
       
        string DepotID = string.Empty;

        var query = from ListItem item in ddldepot.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            DepotID += item.Value + ",";
        }

        DepotID = DepotID.Substring(0, DepotID.Length - 1);

        string LedgerID = string.Empty;
        int count = 0;
        var queryLedgerID = from ListItem item in ddlledger.Items where item.Selected select item;
        foreach (ListItem item in queryLedgerID)
        {
            // item ...
            LedgerID += item.Value + ',';
            count = count + 1;
        }
        LedgerID = LedgerID.Substring(0, LedgerID.Length - 1);
        

        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtDisplayGrid = new DataTable();
        Session["outstanding"] = null;

        if (ddlgroupby.SelectedValue == "1")
        {
            dtDisplayGrid = clsrpt.BindPartyOutstanding(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DepotID.Trim(), ddlgroup.SelectedValue.Trim(), LedgerID.Trim());
        
        }
        else if (ddlgroupby.SelectedValue == "2")
        {
            dtDisplayGrid = clsrpt.BindPartyOutstanding_Opening(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DepotID.Trim(), ddlgroup.SelectedValue.Trim(), LedgerID.Trim());

        }
        else if (ddlgroupby.SelectedValue == "3")
        {
            dtDisplayGrid = clsrpt.BindPartyOutstanding_OnAccReceipts(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DepotID.Trim(), ddlgroup.SelectedValue.Trim(), LedgerID.Trim());

        }

        Session["outstanding"] = dtDisplayGrid;
        this.gvSummaryreport.DataSource = dtDisplayGrid;
        
        this.gvSummaryreport.DataBind();

        //string Usertype = string.Empty;
        //Usertype = clsrpt.GetUsertype(Session["IUSERID"].ToString().Trim());

        //if (Usertype == "B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86" || Usertype == "9BF42AA9-0734-4A6A-B835-0885FBCF26F5") /*ASM & SO*/
        //{
        //    dtDisplayGrid = clsrpt.BindPrimarySalesTaxSummaryReport(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), DepotID.Trim(), "", BSID.Trim(), Convert.ToInt32(ddlamt.SelectedValue.ToString()), ddlledger.SelectedValue.Trim(), Session["IUserID"].ToString().Trim());
        //}
        //else
        //{
        //    dtDisplayGrid = clsrpt.BindPrimarySalesTaxSummaryReport(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), DepotID.Trim(), "", BSID.Trim(), Convert.ToInt32(ddlamt.SelectedValue.ToString()), ddlledger.SelectedValue.Trim(), "0");
        //}
        //if (dtDisplayGrid.Tables[0].Rows.Count > 0)
        //{
        //    this.gvSummaryreport.DataSource = dtDisplayGrid.Tables[0];
        //    double sumqty = 0;
        //    double sumgrossamt = 0;
        //    double sumscheme = 0;
        //    double sumdisc = 0;
        //    double sumsaleamt = 0;
        //    double sumtax = 0;
        //    double sumnetamt = 0;
        //    double sumAdd = 0;
        //    double sumround = 0;
        //    int COUNT = 0;

        //    foreach (DataRow dr in dtDisplayGrid.Tables[0].Rows)
        //    {
        //        sumqty += Convert.ToDouble(dr["SALEINVOICEQTY"]);
        //        sumgrossamt += Convert.ToDouble(dr["GROSSAMT"]);
        //        sumscheme += Convert.ToDouble(dr["SCHEME"]);
        //        sumdisc += Convert.ToDouble(dr["SPECIAL_DISC"]);
        //        sumsaleamt += Convert.ToDouble(dr["SALE_AMT"]);
        //        sumtax += Convert.ToDouble(dr["TOTAL_TAX"]);
        //        sumnetamt += Convert.ToDouble(dr["NETAMT"]);
        //        sumAdd += Convert.ToDouble(dr["ADDLAM"]);
        //        sumround += Convert.ToDouble(dr["RO"]);
        //        COUNT = COUNT + 1;
        //    }

        //    //for (int i = 0; i < dtDisplayGrid.Tables[0].Rows.Count; i++)
        //    //{

        //    //    sumgrossamt =Convert.ToDouble(dtDisplayGrid.Tables[0].Rows[i][8].ToString()); // here you go vr = the value of the cel
        //    //    sumgrossamt += sumgrossamt;
        //    //}
        //    gvSummaryreport.Columns[2].FooterText = "TOTAL";
        //    gvSummaryreport.Columns[5].FooterText = "";
        //    gvSummaryreport.Columns[6].FooterText = sumqty.ToString("###,###.00");
        //    gvSummaryreport.Columns[7].FooterText = sumgrossamt.ToString("###,###.00");
        //    gvSummaryreport.Columns[8].FooterText = sumscheme.ToString("###,###.00");
        //    gvSummaryreport.Columns[9].FooterText = sumdisc.ToString("###,###.00");
        //    gvSummaryreport.Columns[10].FooterText = sumsaleamt.ToString("###,###.00");
        //    gvSummaryreport.Columns[11].FooterText = sumtax.ToString("###,###.00");
        //    gvSummaryreport.Columns[12].FooterText = sumAdd.ToString("###,###.00");
        //    gvSummaryreport.Columns[13].FooterText = sumround.ToString("###,###.00");
        //    gvSummaryreport.Columns[14].FooterText = sumnetamt.ToString("###,###.00");
        //    this.gvSummaryreport.DataBind();
        //}
        //else
        //{
        //    this.gvSummaryreport.DataSource = null;
        //    this.gvSummaryreport.DataBind();
        //}
    }
    #endregion
    #region BindGridOnLoad
    protected void BindGridOnLoad(string fromdate, string todate, string depot, string scheme, string BSID, string customerid)
    {
        
        //ClsStockReport clsrpt = new ClsStockReport();
        //DataSet dtDisplayGrid = new DataSet();
        //string Customer = Request.QueryString["custid"];
        //string Depot = Request.QueryString["depot"];
        //string Usertype = string.Empty;
        //Usertype = clsrpt.GetUsertype(Session["IUSERID"].ToString().Trim());

        //if (Usertype == "B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86" || Usertype == "9BF42AA9-0734-4A6A-B835-0885FBCF26F5") /*ASM & SO*/
        //{
        //    dtDisplayGrid = clsrpt.BindPartyOnLoad(fromdate, todate, depot.Trim(), scheme, BSID.Trim(), customerid.Trim(), "0");
        //}
        //else
        //{
        //    dtDisplayGrid = clsrpt.BindPartyOnLoad(fromdate, todate, depot.Trim(), scheme, BSID.Trim(), customerid.Trim(), Session["IUserID"].ToString().Trim());
        //}
        //if (dtDisplayGrid.Tables[0].Rows.Count > 0)
        //{
        //    this.gvSummaryreport.DataSource = dtDisplayGrid.Tables[0];
        //    double sumqty = 0;
        //    double sumgrossamt = 0;
        //    double sumscheme = 0;
        //    double sumdisc = 0;
        //    double sumsaleamt = 0;
        //    double sumtax = 0;
        //    double sumnetamt = 0;
        //    double sumAdd = 0;
        //    double sumround = 0;

        //    foreach (DataRow dr in dtDisplayGrid.Tables[0].Rows)
        //    {
        //        sumqty += Convert.ToDouble(dr["SALEINVOICEQTY"]);
        //        sumgrossamt += Convert.ToDouble(dr["GROSSAMT"]);
        //        sumscheme += Convert.ToDouble(dr["SCHEME"]);
        //        sumdisc += Convert.ToDouble(dr["SPECIAL_DISC"]);
        //        sumsaleamt += Convert.ToDouble(dr["SALE_AMT"]);
        //        sumtax += Convert.ToDouble(dr["TOTAL_TAX"]);
        //        sumnetamt += Convert.ToDouble(dr["NETAMT"]);
        //        sumAdd += Convert.ToDouble(dr["ADDLAM"]);
        //        sumround += Convert.ToDouble(dr["RO"]);
        //    }
        //    //gvSummaryreport.Columns[1].FooterText = "TOTAL";
        //    //gvSummaryreport.Columns[5].FooterText = sumqty.ToString("###,###.00");
        //    //gvSummaryreport.Columns[6].FooterText = sumgrossamt.ToString("###,###.00");
        //    //gvSummaryreport.Columns[7].FooterText = sumscheme.ToString("###,###.00");
        //    //gvSummaryreport.Columns[8].FooterText = sumdisc.ToString("###,###.00");
        //    //gvSummaryreport.Columns[9].FooterText = sumsaleamt.ToString("###,###.00");
        //    //gvSummaryreport.Columns[10].FooterText = sumtax.ToString("###,###.00");
        //    //gvSummaryreport.Columns[11].FooterText = sumAdd.ToString("###,###.00");
        //    //gvSummaryreport.Columns[12].FooterText = sumround.ToString("###,###.00");
        //    //gvSummaryreport.Columns[13].FooterText = sumnetamt.ToString("###,###.00");

        //    gvSummaryreport.Columns[2].FooterText = "TOTAL";
        //    gvSummaryreport.Columns[5].FooterText = "";
        //    gvSummaryreport.Columns[6].FooterText = sumqty.ToString("###,###.00");
        //    gvSummaryreport.Columns[7].FooterText = sumgrossamt.ToString("###,###.00");
        //    gvSummaryreport.Columns[8].FooterText = sumscheme.ToString("###,###.00");
        //    gvSummaryreport.Columns[9].FooterText = sumdisc.ToString("###,###.00");
        //    gvSummaryreport.Columns[10].FooterText = sumsaleamt.ToString("###,###.00");
        //    gvSummaryreport.Columns[11].FooterText = sumtax.ToString("###,###.00");
        //    gvSummaryreport.Columns[12].FooterText = sumAdd.ToString("###,###.00");
        //    gvSummaryreport.Columns[13].FooterText = sumround.ToString("###,###.00");
        //    gvSummaryreport.Columns[14].FooterText = sumnetamt.ToString("###,###.00");
        //    this.gvSummaryreport.DataBind();
        //}
        //else
        //{
        //    this.gvSummaryreport.DataSource = null;
        //    this.gvSummaryreport.DataBind();
        //}
    }
    #endregion
    #region gvSummaryreport_OnRowDataBound
    protected void gvSummaryreport_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //decimal result;
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
            
        //    // Cell Color
        //    for (int i = 0; i <= gvSummaryreport.Rows.Count - 1; i++)
        //    {
        //        string myClassVal = gvSummaryreport.Rows[i].Cells[6].Text;
        //        if (myClassVal == "Opening")
        //        {
        //            gvSummaryreport.Rows[i].Cells[12].BackColor = Color.Yellow;
        //        }
        //        else if (myClassVal == "Party Total")
        //        {
        //            gvSummaryreport.Rows[i].Cells[12].BackColor = Color.Yellow;
        //        }
        //        else if (myClassVal == "On A/C Receipts")
        //        {
        //            gvSummaryreport.Rows[i].Cells[12].BackColor = Color.White;
        //        }
        //        else if (myClassVal == "Balance")
        //        {
        //            gvSummaryreport.Rows[i].Cells[12].BackColor = Color.Yellow;
        //        }

        //        else
        //        {
        //            gvSummaryreport.Rows[i].Cells[12].BackColor = Color.Orange;
        //        }
        //        //if (myClassVal == "DISPLAY")
        //        //{
        //        //    gvSummaryreport.Rows[i].Cells[17].Visible=false;
        //        //}
        //    }

        //    //gvSummaryreport.Rows[i].Cells[17].Visible = false;
            
        //    //gvSummaryreport.Columns[5].DataFormatString = "{0:dd.MM.yyyy}";
        //    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        //    //e.Row.Cells[8].Text = String.Format((e.Row.Cells[8].Text), "#,##");
        //    if (decimal.TryParse(e.Row.Cells[8].Text, out result))
        //    {
        //        e.Row.Cells[8].Text = result.ToString("0,0.00");
        //    }
        //    e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        //    if (decimal.TryParse(e.Row.Cells[10].Text, out result))
        //    {
        //        e.Row.Cells[10].Text = result.ToString("0,0.00");
        //    }
        //    e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        //    if (decimal.TryParse(e.Row.Cells[12].Text, out result))
        //    {
        //        e.Row.Cells[12].Text = result.ToString("0,0.00");
        //    }
        //}
        //this.gvSummaryreport.Columns[17].Visible = false;
       // if (e.Row.RowType == DataControlRowType.DataRow) 
       //// e.Row.RowType = DataControlRowType.Header)    // apply to datarow and header 
       // {
       // e.Row.Cells[e.Row.Cells.Count - 1].Visible =false; // last column
       // this.gvSummaryreport.Columns["DISPLAY"].Visible = false;
       // //e.Row.Cells(0).Visible = False  // first column
       // }
    
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
            //DataRowView drv = e.Row.DataItem as DataRowView;

            //if (drv["SALEINVOICEQTY"].ToString().ToUpper().Contains("-"))
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Black;
            //    e.Row.Font.Bold = true;
            //    LinkButton lbtn = new LinkButton();
            //    lbtn = (LinkButton)e.Row.FindControl("lnkWorkStationName");
            //    lbtn.Enabled = false;
            //  }
        //}
    }
    #endregion

    #region gvSummaryreport_RowCommand
    protected void gvSummaryreport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "selectInvoice")
        //{

        //    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        //    string str = commandArgs[0];
        //    string strdate = commandArgs[1].ToString();
        //    string upath = string.Empty;


        //    int invoicedate = Convert.ToInt32(Conver_To_ISO(strdate.Trim()));
        //    int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));


        //    if (invoicedate <= checkdate)
        //    {
        //        upath = "frmRptPrimaryInvoiceDetailsReport.aspx?invoiceid=" + str + "";
        //    }
        //    else
        //    {
        //        upath = "frmRptPrimaryInvoiceDetailsReport_GST.aspx?invoiceid=" + str + "";
        //    }

        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1200,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        //}
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["outstanding"];



        Session["outstanding"] = dt;
        
        Response.Clear();
        //Response.AddHeader("content-disposition", "inline; filename=" + "Outstanding_" + ddlgroup.SelectedItem.Text.Trim() + "_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.AddHeader("content-disposition", "inline; filename=" + "Outstanding_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        //Response.AddHeader("content-disposition", "inline; filename=" + "Bank_Reconciliation_Report_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvSummaryreport.Visible = true;
        htmlWrite.Write("<table><tr><td colspan=14><b>McNroe Consumer Products Private Limited </b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=14><b>Bills Receivables : " + ddlgroup.SelectedItem.Text.Trim() + "</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=14><b>Period : " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        gvSummaryreport.DataSource = dt;
        gvSummaryreport.DataBind();

        //grdledger_for_excel.HeaderRow.Cells[0].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[1].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[2].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[3].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[4].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[5].Style.Add("text-decoration", "Underline");
        //grdledger_for_excel.HeaderRow.Cells[6].Style.Add("text-decoration", "Underline");


        gvSummaryreport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();
        Session["itemledger"] = null;
        gvSummaryreport.Visible = false;

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #region grdledger_RowDataBound
    protected void grdledger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
        //        {
        //            e.Row.BackColor = Color.FromName("#e26b0a");
        //            e.Row.Font.Bold = true;
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 1)
        //        {
        //            e.Row.BackColor = Color.FromName("#ffc000");
        //            e.Row.Font.Bold = true;
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 2)
        //        {
        //            e.Row.BackColor = Color.FromName("#86ab3c");
        //            e.Row.Font.Bold = true;
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 3)
        //        {
        //            e.Row.BackColor = Color.FromName("#76933c");
        //            e.Row.Font.Bold = true;
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 4)
        //        {
        //            e.Row.BackColor = Color.FromName("#6fa438");
        //            e.Row.Font.Bold = true;
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -1)
        //        {
        //            e.Row.BackColor = Color.FromName("#92d050");
        //        }
        //        if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -2)
        //        {
        //            e.Row.Cells[1].Text = "Total";
        //            e.Row.Cells[0].Font.Bold = true;
        //            e.Row.Cells[0].ForeColor = Color.Black;
        //            e.Row.BackColor = Color.FromName("#c5ed84");
        //            e.Row.Font.Bold = true;
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //}
    }
    #endregion

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Sales_Summary_Report_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls"));
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvSummaryreport.AllowPaging = false;
            if (Request.QueryString["custid"] != null && Request.QueryString["depot"] != null)
            {
                this.BindGridOnLoad(Request.QueryString["fdate"], Request.QueryString["todate"], Request.QueryString["depot"], "1", Request.QueryString["bsid"], Request.QueryString["custid"]);

                foreach (GridViewRow row in gvSummaryreport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {


                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvSummaryreport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvSummaryreport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";

                        List<Control> controls = new List<Control>();
                        foreach (Control control in cell.Controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    controls.Add(control);
                                    break;
                                case "TextBox":
                                    controls.Add(control);
                                    break;
                                case "LinkButton":
                                    controls.Add(control);
                                    break;
                                case "CheckBox":
                                    controls.Add(control);
                                    break;
                                case "RadioButton":
                                    controls.Add(control);
                                    break;
                            }
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }

                hw.Write("<table><tr><td colspan=4>McNROE CONSUMERS PRODUCTS PRIVATE LIMITED</td></tr>");
                hw.Write("<table><tr><td colspan=4><b>SALES SUMMARY REPORT</b></td></tr>");
                //hw.Write("<table><tr><td colspan=4><b>DIVISION : " + bsvalue.Trim() + " </b></td></tr>");
                hw.Write("<table><tr><td colspan=4><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");

                gvSummaryreport.RenderControl(hw);

                //style to format numbers to string
                // string style = @"<style> .textmode {mso-number-format:General } </style>";

                //Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                this.BindGrid();
                gvSummaryreport.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvSummaryreport.HeaderRow.Cells)
                {
                    cell.BackColor = gvSummaryreport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvSummaryreport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {


                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvSummaryreport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvSummaryreport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";

                        List<Control> controls = new List<Control>();
                        foreach (Control control in cell.Controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    controls.Add(control);
                                    break;
                                case "TextBox":
                                    controls.Add(control);
                                    break;
                                case "LinkButton":
                                    controls.Add(control);
                                    break;
                                case "CheckBox":
                                    controls.Add(control);
                                    break;
                                case "RadioButton":
                                    controls.Add(control);
                                    break;
                            }
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }



                string bsvalue = string.Empty;
                if (ViewState["BSDTCOUNT"].ToString().Trim() == ViewState["BSCOUNTER"].ToString().Trim())
                {
                    bsvalue = "ALL";
                }
                else
                {

                    var queryBSID = from ListItem item in ddlbsegment.Items where item.Selected select item;
                    foreach (ListItem item in queryBSID)
                    {
                        // item ...
                        bsvalue += item.Text.Trim() + ',';


                    }

                    bsvalue = bsvalue.Substring(0, bsvalue.Length - 1);

                }

                hw.Write("<table><tr><td colspan=4>McNROE CONSUMERS PRODUCTS PRIVATE LIMITED</td></tr>");
                hw.Write("<table><tr><td colspan=4><b>SALES SUMMARY REPORT</b></td></tr>");
                hw.Write("<table><tr><td colspan=4><b>DIVISION : " + bsvalue.Trim() + " </b></td></tr>");
                hw.Write("<table><tr><td colspan=4><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");

                gvSummaryreport.RenderControl(hw);



                //style to format numbers to string
                // string style = @"<style> .textmode {mso-number-format:General } </style>";



                //Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }

    }
    #endregion

    #region ddlgroup_SelectedIndexChanged
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GrouptID = string.Empty;
        string LedgerID = string.Empty;
        var querygroup = from ListItem item in ddlgroup.Items where item.Selected select item;
        foreach (ListItem item in querygroup)
        {
            GrouptID += item.Value + ',';
        }
        GrouptID = GrouptID.Substring(0, GrouptID.Length - 1);


        var queryledger = from ListItem item in ddlledger.Items where item.Selected select item;
        foreach (ListItem item in queryledger)
        {
            LedgerID += item.Value + ',';
        }
        if (LedgerID.Length>0)
        {
        LedgerID = LedgerID.Substring(0, LedgerID.Length - 1);
        }

        this.LoadLedgerGroupWise(GrouptID);
    }
    #endregion

    #region LoadLedger Group Segment Wise
    public void LoadLedgerGroupWise(string GrouptID)
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindLedgerGroupWise(ddlgroup.SelectedValue);
            ddlledger.Items.Clear();
            //ddlledger.Items.Add(new ListItem("ALL", "0"));
            ddlledger.AppendDataBoundItems = true;
            ddlledger.DataSource = dt;
            ddlledger.DataValueField = "ID";
            ddlledger.DataTextField = "NAME";
            ddlledger.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}