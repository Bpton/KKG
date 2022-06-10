using BAL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmRptRateWise_PurchaseReport : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true});$('#ContentPlaceHolder1_ddlBrand').multiselect({ includeSelectAllOption: true});$('#ContentPlaceHolder1_ddlCategory').multiselect({ includeSelectAllOption: true});$('#ContentPlaceHolder1_ddlProduct').multiselect({ includeSelectAllOption: true});});</script>", false);
            if (!IsPostBack)
            {
                this.LoadVendorName();
                this.DateLock();
                this.LoadDepotName();
                this.LoadBrand();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = clsreport.BindDepot_Primary();
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadVendorName
    public void LoadVendorName()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindTPUPatyName();
            ddlVendor.Items.Clear();
            ddlVendor.Items.Add(new ListItem("All", "0"));
            ddlVendor.AppendDataBoundItems = true;
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "VENDORNAME";
            ddlVendor.DataValueField = "VENDORID";
            ddlVendor.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Brand
    public void LoadBrand()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlBrand.Items.Clear();
        ddlBrand.AppendDataBoundItems = true;
        ddlBrand.DataSource = ClsCommon.BindBrand_NotinFG();
        ddlBrand.DataTextField = "DIVNAME";
        ddlBrand.DataValueField = "DIVID";
        ddlBrand.DataBind();
    }
    #endregion

    #region ddlBrand_SelectedIndexChanged
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            ClsStockReport ClsLedger = new ClsStockReport();
            if (ddlBrand.SelectedValue != "")
            {
                if (ddlBrand.SelectedValue != "1")
                {
                    string BRANDID = "";
                    var query = from ListItem item in ddlBrand.Items where item.Selected select item;
                    foreach (ListItem item in query)
                    {
                        // item ...
                        BRANDID += item.Value + "','";
                    }
                    if (BRANDID.Trim() != "")
                    {
                        BRANDID = BRANDID.Substring(0, BRANDID.Length - 3);
                    }
                    ddlCategory.Items.Clear();
                    //ddlCategory.Items.Insert(0, new ListItem("SELECT SUB ITEM", "0"));
                    ddlCategory.AppendDataBoundItems = true;
                    ddlCategory.DataSource = ClsLedger.BindCategorybybrand(BRANDID);
                    ddlCategory.DataTextField = "CATNAME";
                    ddlCategory.DataValueField = "CATID";
                    ddlCategory.DataBind();
                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                    ddlProduct.AppendDataBoundItems = true;
                    ddlProduct.DataSource = ClsCommon.BindFGProduct();
                    ddlProduct.DataTextField = "NAME";
                    ddlProduct.DataValueField = "ID";
                    ddlProduct.DataBind();
                }
            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "")
            {
                string DEPOTID = "";
                var Depotquery = from ListItem item in ddldepot.Items where item.Selected select item;
                foreach (ListItem item in Depotquery)
                {
                    DEPOTID += item.Value + ",";
                }
                DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);

                string BRANDID = "";
                var query = from ListItem item in ddlBrand.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BRANDID += item.Value + ",";
                }
                BRANDID = BRANDID.Substring(0, BRANDID.Length - 1);

                string CATID = "";
                var query1 = from ListItem item in ddlCategory.Items where item.Selected select item;
                foreach (ListItem item in query1)
                {
                    CATID += item.Value + ",";
                }
                CATID = CATID.Substring(0, CATID.Length - 1);
                ClsFactoryReport ClsLedger = new ClsFactoryReport();
                ddlProduct.Items.Clear();
                ddlProduct.AppendDataBoundItems = true;
                ddlProduct.DataSource = ClsLedger.BindProduct(Convert.ToString(CATID).Trim(), Convert.ToString(BRANDID).Trim(), DEPOTID);
                ddlProduct.DataTextField = "NAME";
                ddlProduct.DataValueField = "ID";
                ddlProduct.DataBind();
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion    

    #region btnshow_Click
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region BindGrid
    public void BindGrid()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = new DataTable();
            string Depot = "", ProductID = "";
            string DepotName = "";
            var query = from ListItem item in ddldepot.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                Depot += item.Value + ',';
                DepotName += item.Text + ',';
            }
            Depot = Depot.Substring(0, Depot.Length - 1);
            DepotName = DepotName.Substring(0, DepotName.Length - 1);

            var Productquery = from ListItem item in ddlProduct.Items where item.Selected select item;
            foreach (ListItem item in Productquery)
            {
                ProductID += item.Value + ',';
            }
            ProductID = ProductID.Substring(0, ProductID.Length - 1);

            dt = clsreport.BIND_RATEWISE_PURCHASE_REPORT(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), ddlVendor.SelectedValue.ToString(), ProductID, Depot, ddlReportType.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                this.grdRatewisePurchase.DataSource = dt;
                this.grdRatewisePurchase.DataBind();
                Cache["EXPOTCPCCSD"] = dt;
            }
            else
            {
                this.grdRatewisePurchase.DataSource = null;
                this.grdRatewisePurchase.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #region Export To Excel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Cache["EXPOTCPCCSD"];
        Cache["EXPOTCPCCSD"] = dt;
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "RateWise_PurchaseReport_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        htmlWrite.Write("<table><tr><td colspan=4><b>" + Session["DEPOTNAME"] + " </b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan='4'><b>Ratewise Purchase Report</b></ td></tr>");
        htmlWrite.Write("<table><tr><td colspan=4><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        //this.ClearControls(grdledger);
        grdRatewisePurchase.Visible = true;
        grdRatewisePurchase.DataSource = dt;
        grdRatewisePurchase.DataBind();

        for (int i = 0; i < grdRatewisePurchase.HeaderRow.Cells.Count; i++)
        {
            grdRatewisePurchase.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
            grdRatewisePurchase.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");
        }
        //grdRatewisePurchase.RowStyle.Font.Bold = true;
        grdRatewisePurchase.RowStyle.Font.Size = 10;
        grdRatewisePurchase.RowStyle.Height = 20;
        grdRatewisePurchase.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        grdRatewisePurchase.Visible = false;
    }
    #endregion

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender4.StartDate = oDate;
        CalendarExtender1.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = today1;
            CalendarExtender1.EndDate = today1;
        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
        }
    }
    #endregion

    protected void grdRatewisePurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlReportType.SelectedValue == "0" || ddlReportType.SelectedValue == "1")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[11].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[11].Visible = false;
            }
        }
        else
        {

        }
    }
}