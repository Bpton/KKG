#region Namespace
using BAL;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class VIEW_frmRptDepotWisePurchase : System.Web.UI.Page
{
    ArrayList arry = new ArrayList();

    #region Page_Init
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvDepotwisePurchase.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true });$('#ContentPlaceHolder1_ddlspan').multiselect({includeSelectAllOption: true}); $('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlproduct').multiselect({ includeSelectAllOption:true}); $('#ContentPlaceHolder1_ddlgroupby').multiselect({ includeSelectAllOption:true}); $('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true });});</script>", false);

        if (!IsPostBack)
        {
            //*********  LOCK CALENDAR BEYOND FINANCIAL YEAR  ********** SOUMITRA MONDAL //

            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            CalendarExtender2.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;
            // ******  END LOCK CAELENDER   ***********  SOUMITRA MONDAL //

            this.pnlDisplay.Style["display"] = "";
            this.LoadDepot();
            this.LoadBrand();
            ddlcategory.Items.Clear();
            ddlcategory.Items.Insert(0, new ListItem("SELECT SUB ITEM NAME", "0"));
           
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime today1 = DateTime.Now;
            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtender3.EndDate = today1;
                CalendarExtender2.EndDate = today1;

            }
            else
            {
                this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                CalendarExtender3.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
            }
        }
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
                lblcalender.Text = "PERIOD";
                Tag = "P";
            }
            else
            {
                Tag = "P";
                txtfromdate.Enabled = true;
                txttodate.Enabled = true;
                Imgfrom.Enabled = true;
                ImgToDate.Enabled = true;
            }
            dt = clsreport.BindTimeSpanFY(Tag, Session["FINYEAR"].ToString());
            if (dt.Rows.Count > 0)
            {
                ViewState["Searchtag"] = Tag;
                ddlspan.Items.Clear();
                ddlspan.AppendDataBoundItems = true;
                ddlspan.DataSource = dt;
                ddlspan.DataValueField = "TIMESPAN";
                ddlspan.DataTextField = "TIMESPAN";
                ddlspan.DataBind();
            }
            else
            {
                ddlspan.Items.Clear();
                ddlspan.SelectedIndex = -1;
                ViewState["Searchtag"] = Tag;
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

    #region LoadDepot
    public void LoadDepot()
    {
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

    #region LoadBrand
    public void LoadBrand()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (ddlbrand.SelectedValue == "")
            {
                ddlbrand.Items.Clear();
                ddlbrand.AppendDataBoundItems = true;
                ddlbrand.DataSource = clsreport.BindBrand_Purchase();
                ddlbrand.DataTextField = "DIVNAME";
                ddlbrand.DataValueField = "DIVID";
                ddlbrand.DataBind();
                
            }
            else
            {
                ddlbrand.Items.Clear();
                ddlbrand.Items.Add(new ListItem("Select", "0"));
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlbrand_SelectedIndexChanged
    protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsStockReport ClsCommon = new ClsStockReport();
            ClsStockReport ClsLedger = new ClsStockReport();
            ClsStockReport clsreport = new ClsStockReport();



            if (ddlbrand.SelectedValue != "")
            {
                if (ddlbrand.SelectedValue != "1")
                {
                    string BRANDID = "";
                    var query = from ListItem item in ddlbrand.Items where item.Selected select item;
                    foreach (ListItem item in query)
                    {
                        // item ...
                        BRANDID += item.Value + ',';
                    }
                    if (BRANDID.Trim() != "")
                    {
                        BRANDID = BRANDID.Substring(0, BRANDID.Length - 1);
                    }

                    ddlcategory.Items.Clear();
                    //ddlcategory.Items.Insert(0, new ListItem("SELECT SUB ITEM", "0"));
                    ddlcategory.AppendDataBoundItems = true;
                    ddlcategory.DataSource = ClsLedger.BindCategory_Brandwise(BRANDID.ToString().Trim());
                    ddlcategory.DataTextField = "CATNAME";
                    ddlcategory.DataValueField = "CATID";
                    ddlcategory.DataBind();

                }
                else
                {
                    ddlproduct.Items.Clear();
                    ddlproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                    ddlproduct.AppendDataBoundItems = true;
                    ddlproduct.DataSource = ClsCommon.BindFGProduct();
                    ddlproduct.DataTextField = "NAME";
                    ddlproduct.DataValueField = "ID";
                    ddlproduct.DataBind();
                }
            }
            else
            {
                ddlcategory.Items.Clear();
                ddlcategory.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    #endregion

    #region ddlcategory_SelectedIndexChanged
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcategory.SelectedValue != "")
            {
                ClsStockReport ClsLedger = new ClsStockReport();

                string BRANDID = "";
                var query = from ListItem item in ddlbrand.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    // item ...
                    BRANDID += item.Value + ',';
                }
                if (BRANDID.Trim() != "")
                {
                    BRANDID = BRANDID.Substring(0, BRANDID.Length - 1);
                }
                string CATID = "";
                var query1 = from ListItem item in ddlcategory.Items where item.Selected select item;
                foreach (ListItem item in query1)
                {
                    // item ...
                    CATID += item.Value + ',';
                }
                if (CATID.Trim() != "")
                {
                    CATID = CATID.Substring(0, CATID.Length - 1);
                }


                ddlproduct.Items.Clear();
                //ddlproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                ddlproduct.AppendDataBoundItems = true;
                ddlproduct.DataSource = ClsLedger.BindProductMultiple(CATID.ToString().Trim(), BRANDID.ToString().Trim());
                ddlproduct.DataTextField = "NAME";
                ddlproduct.DataValueField = "ID";
                ddlproduct.DataBind();
            }
            else
            {
                ddlproduct.Items.Clear();
                ddlproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region LoadAllProduct
    public void LoadAllProduct()
    {
        try
        {
            ClsStockReport ClsCommon = new ClsStockReport();
            ddlproduct.Items.Clear();
            //ddlproduct.Items.Insert(0, new ListItem("Select Product", "0"));
            ddlproduct.DataSource = ClsCommon.BindProductALIAS();
            ddlproduct.DataTextField = "NAME";
            ddlproduct.DataValueField = "ID";
            ddlproduct.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion 

    #region ddldepot_OnSelectedIndexChanged
    protected void ddldepot_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataTable dt = new DataTable();
        string DepotID = string.Empty;
        var query = from ListItem item in ddldepot.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            DepotID += item.Value + ",";
        }
        if (DepotID.Length > 0)
        {
            DepotID = DepotID.Substring(0, DepotID.Length - 1);
        }
        else
        {
            DepotID = "0";
        }
    }
    #endregion        

    #region BindGrid
    protected void BindGrid()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtDisplayGrid = new DataTable();
        string span = string.Empty;
        string Value = string.Empty;

        string SearchTag = string.Empty;
        //SearchTag = ddlcalender.SelectedValue.Trim();

        SearchTag = Convert.ToString(ViewState["Searchtag"]).Trim();

        string TSID = "";
        var query8 = from ListItem item in ddlspan.Items where item.Selected select item;
        var itemcount = (from ListItem item in ddlspan.Items where item.Selected select item).Count();
        if (itemcount == 0)
        {
            ViewState["spancount"] = 1;
        }
        else
        {
            ViewState["spancount"] = itemcount;
        }
        foreach (ListItem item in query8)
        {
            TSID += item.Value + ',';

        }
        if (TSID.Length >= 1)
        {
            TSID = TSID.Substring(0, TSID.Length - 1);
        }
        else
        {
            TSID = "Period";
        }
        ViewState["TSID"] = TSID;

        string DepotID = string.Empty; 
        var QueryDepot = from ListItem item in ddldepot.Items where item.Selected select item;
        foreach (ListItem item in QueryDepot)
        {
            // item ...
            DepotID += item.Value + ",";
        }
        if (DepotID.Length > 0)
        {
            DepotID = DepotID.Substring(0, DepotID.Length - 1);
        }
        else
        {
            DepotID = "0";
        }
        ViewState["DEPOTID"] = DepotID;

        string Brand = "";
        var querybrand = from ListItem item in ddlbrand.Items where item.Selected select item;
        foreach (ListItem item in querybrand)
        {
            // item ...
            Brand += item.Value + ',';
        }
        Brand = Brand.Substring(0, Brand.Length - 1);
        ViewState["Brand"] = Brand;

        string CATID = "";
        var Querycat = from ListItem item in ddlcategory.Items where item.Selected select item;
        foreach (ListItem item in Querycat)
        {
            // item ...
            CATID += item.Value + ',';
        }
        if (CATID.Length>0)
        { 
        CATID = CATID.Substring(0, CATID.Length - 1);
        }
        else
        { CATID = "0"; }
        ViewState["Category"] = CATID;

        string PRODUCTID = "";
        var QueryProduct = from ListItem item in ddlproduct.Items where item.Selected select item;
        foreach (ListItem item in QueryProduct)
        {
            // item ...
            PRODUCTID += item.Value + ',';
        }
        if (PRODUCTID.Length>0)
        { 
        PRODUCTID = PRODUCTID.Substring(0, PRODUCTID.Length - 1);
        }
        else
        { PRODUCTID = "0"; }
        ViewState["Product"] = PRODUCTID;

        string TYPEID = "";
        var QueryType = from ListItem item in ddlgroupby.Items where item.Selected select item;
        foreach (ListItem item in QueryType)
        {
            // item ...
            TYPEID += item.Value + ',';
        }
        if (TYPEID.Length>0)
        { 
        TYPEID = TYPEID.Substring(0, TYPEID.Length - 1);
        }
        else
        { TYPEID = "0"; }
        ViewState["TYPEID"] = TYPEID;

        gvDepotwisePurchase.Columns.Clear();

        dtDisplayGrid = clsrpt.BIND_DEPOTWISE_PURCHASE(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), DepotID.Trim(), CATID, PRODUCTID, TYPEID, SearchTag, TSID, Session["FINYEAR"].ToString());
        Session["ds1"] = dtDisplayGrid;

        if (dtDisplayGrid.Rows.Count > 0)
        {
            this.gvDepotwisePurchase.DataSource = dtDisplayGrid;
            this.gvDepotwisePurchase.DataBind();
        }
        else
        {
            this.gvDepotwisePurchase.DataSource = null;
            this.gvDepotwisePurchase.DataBind();
        }
    }
    #endregion

    protected void OnDataBound(object sender, EventArgs e)
    {
        
    }

    protected void gvDepotwisePurchase_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            var spancount = Convert.ToInt64(ViewState["spancount"]);
            DataTable dt = (DataTable)Session["ds1"];
            
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hp = new HyperLink();
                hp.Text = e.Row.Cells[6].Text;
                string brandid = e.Row.Cells[0].Text;
                string catid = e.Row.Cells[1].Text;
                string productid = e.Row.Cells[2].Text;
                string PartyID = "0";
                string FinYear;

                string pageurl = "frmRptPartywisePurchase.aspx?name=" + hp.Text + "&SearchTag=" + ViewState["Searchtag"].ToString() + "&TSID=" + ViewState["TSID"].ToString() + "&Depot=" + ViewState["DEPOTID"] + "&Spancount=" + ViewState["spancount"].ToString() + "&&fromdate=" + txtfromdate.Text + "&&todate=" + txttodate.Text + "&TYPEID=" + ViewState["TYPEID"].ToString() + "&Category=" + catid.ToString() + "&Brand=" + brandid.ToString() + "&Product=" + productid.ToString() + "&FinYear=" + Session["FINYEAR"].ToString() + "&PartyID=" + PartyID;
                //"&Category=" + ViewState["Category"].ToString() + "&Brand=" + ViewState["Brand"].ToString();
                hp.NavigateUrl = String.Format("javascript:void(window.open('" + pageurl + "','_blank'));");
                e.Row.Cells[6].Controls.Add(hp);

                DataRowView drv = e.Row.DataItem as DataRowView;

                e.Row.Cells[1].CssClass = "locked";
                e.Row.Cells[2].CssClass = "locked";
                
                gvDepotwisePurchase.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Left;

                for (int i = 4; i <= gvDepotwisePurchase.Columns.Count; i++)
                {
                    gvDepotwisePurchase.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
            }


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        
    }
    protected void gvDepotwisePurchase_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "selectState")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string str = commandArgs[0];
            //string depot = commandArgs[1];
            string upath = "frmRptPartywisePurchase.aspx?stateid=" + str + "&&fromdate=" + txtfromdate.Text + "&&todate=" + txttodate.Text + "&BSID=" + ViewState["BSID"].ToString() + "&Brand=" + ViewState["Brand"].ToString() + "&Category=" + ViewState["Category"].ToString() + "&Product=" + ViewState["Product"].ToString() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1200,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
    }

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }
    #endregion

    #region VerifyRenderingInServerForm
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    #endregion

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Productwise Purchase.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvDepotwisePurchase.HeaderStyle.ForeColor = Color.Black;
            gvDepotwisePurchase.RowStyle.Font.Bold = false;
            gvDepotwisePurchase.RowStyle.Font.Size = 10;
            gvDepotwisePurchase.RowStyle.Height = 20;
            gvDepotwisePurchase.FooterStyle.Font.Bold = true;
            gvDepotwisePurchase.FooterStyle.Font.Size = 10;
            gvDepotwisePurchase.FooterStyle.Height = 20;
            hw.Write("<table><tr><td colspan='3'>"+ Session["DEPOTNAME"] +"</td></tr>");
            hw.Write("<table><tr><td colspan='3'>Productwise Purchase Report</ td></tr>");
            hw.Write("<table><tr><td colspan='3'>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </td></tr>");
            gvDepotwisePurchase.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
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

}