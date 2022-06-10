using PPBLL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmRptStockInHand_FAC : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });});</script>", false);

            if (!IsPostBack)
            {
                this.LoadDepotName();
                this.LoadBrand();
                LoadPackSize();
                LoadCategory();
                LoadBusinessSegment();
                LoadStorelocation();
                //txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                LoadAllProduct();
                DateLock();
            }
            grdstock.ExportingSettings.FileName = "STOCKINHAND_" + txtdate.Text.Trim() + "_" + HttpContext.Current.Session["USERNAME"].ToString().Trim() + "";
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
        ClsStockReport clsreport = new ClsStockReport();
        try
        {
            DataTable dt = clsreport.BindProduct(HttpContext.Current.Session["DEPOTID"].ToString());
            ddlproduct.Items.Clear();
            ddlproduct.Items.Insert(0, new ListItem("ALL", "0"));
            ddlproduct.AppendDataBoundItems = true;
            ddlproduct.DataSource = dt;
            ddlproduct.DataValueField = "ID";
            ddlproduct.DataTextField = "NAME";
            ddlproduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
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
            //ddlbsegment.Items.Add(new ListItem("ALL", "0"));
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

    #region LoadPackSize
    public void LoadPackSize()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindSizeInPack();
            ddlpacksize.Items.Clear();
            ddlpacksize.Items.Add(new ListItem("Select", "0"));
            ddlpacksize.AppendDataBoundItems = true;
            ddlpacksize.DataSource = dt;
            ddlpacksize.DataValueField = "PSID";
            ddlpacksize.DataTextField = "PSNAME";
            ddlpacksize.DataBind();
            ddlpacksize.SelectedValue = "1970C78A-D062-4FE9-85C2-3E12490463AF";//for case

        }

        catch (Exception ex)
        {
            throw ex;
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
                ddlsupplingdepot.Items.Clear();
                ddlsupplingdepot.AppendDataBoundItems = true;
                ddlsupplingdepot.DataSource = dt;
                ddlsupplingdepot.DataTextField = "BRNAME";
                ddlsupplingdepot.DataValueField = "BRID";
                ddlsupplingdepot.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = clsreport.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddlsupplingdepot.Items.Clear();
                ddlsupplingdepot.AppendDataBoundItems = true;
                ddlsupplingdepot.DataSource = dt;
                ddlsupplingdepot.DataTextField = "BRNAME";
                ddlsupplingdepot.DataValueField = "BRID";
                ddlsupplingdepot.DataBind();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddlsupplingdepot.Items.Clear();
                ddlsupplingdepot.AppendDataBoundItems = true;
                ddlsupplingdepot.DataSource = dt;
                ddlsupplingdepot.DataTextField = "BRNAME";
                ddlsupplingdepot.DataValueField = "BRID";
                ddlsupplingdepot.DataBind();
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadStorelocation
    private void LoadStorelocation()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindStoreLocation();
            ddlstorelocation.Items.Clear();
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dt;
            ddlstorelocation.DataValueField = "ID";
            ddlstorelocation.DataTextField = "NAME";
            ddlstorelocation.DataBind();
            ddlstorelocation.SelectedValue = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";
        }
        catch (Exception ex)
        {
            throw ex;
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
                DataTable dt = clsreport.BindBrand();
                ddlbrand.Items.Clear();
                ddlbrand.AppendDataBoundItems = true;
                ddlbrand.DataSource = dt;
                ddlbrand.DataValueField = "DIVID";
                ddlbrand.DataTextField = "DIVNAME";
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

    #region LoadCategory
    public void LoadCategory()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindCategory();
            ddlcategory.Items.Clear();
            ddlcategory.AppendDataBoundItems = true;
            ddlcategory.DataSource = dt;
            ddlcategory.DataValueField = "CATID";
            ddlcategory.DataTextField = "CATNAME";
            ddlcategory.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadCategory
    public void LoadCategoryByBrand()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();

            string BRANDID = "";
            var query = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BRANDID += item.Value + "','";
            }
            if (BRANDID.Trim() != "")
            {
                BRANDID = BRANDID.Substring(0, BRANDID.Length - 3);
            }

            ddlcategory.Items.Clear();
            DataTable dt = clsreport.BindCategorybybrand(BRANDID);

            ddlcategory.AppendDataBoundItems = true;
            ddlcategory.DataSource = dt;
            ddlcategory.DataValueField = "CATID";
            ddlcategory.DataTextField = "CATNAME";
            ddlcategory.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
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
                LoadProduct();
                ddlbatch.Items.Clear();
                ddlbatch.Items.Add(new ListItem("ALL", "0"));
            }
            else
            {
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("ALL", "0"));
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
            if (ddlbrand.SelectedValue != "")
            {
                string prod = ddlproduct.SelectedValue.Trim();
                LoadCategoryByBrand();
                LoadProduct();
                ddlproduct.SelectedValue = prod;

                ddlbatch.Items.Clear();
                ddlbatch.Items.Add(new ListItem("ALL", "0"));
            }
            else
            {
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlproduct_SelectedIndexChanged
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (ddlproduct.SelectedValue == "0")
            {
                ddlbatch.Items.Clear();
                ddlbatch.Items.Add(new ListItem("ALL", "0"));
                ddlbatch.Enabled = false;
            }
            else
            {
                DataTable dt = clsreport.GetBatch(ddlproduct.SelectedValue.Trim());

                if (dt.Rows.Count == 1)
                {
                    ddlbatch.SelectedItem.Text = dt.Rows[0]["BATCHNO"].ToString().Trim();
                    ddlbatch.Enabled = true;
                }
                else
                {

                    ddlbatch.Items.Clear();
                    ddlbatch.Items.Add(new ListItem("ALL", "0"));
                    ddlbatch.DataSource = dt;
                    ddlbatch.DataValueField = "BATCHNO";
                    ddlbatch.DataTextField = "BATCHNO";
                    ddlbatch.DataBind();
                    ddlbatch.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadProduct
    public void LoadProduct()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();

            string BSID = "";
            var queryddlbsegment = from ListItem item in ddlbsegment.Items where item.Selected select item;
            foreach (ListItem item in queryddlbsegment)
            {
                // item ...
                BSID += item.Value + "','";
            }
            BSID = BSID.Substring(0, BSID.Length - 3);

            string BRANDID = "";
            var query = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                BRANDID += item.Value + ",";
            }
            BRANDID = BRANDID.Substring(0, BRANDID.Length - 1);

            string CATID = "";
            var query1 = from ListItem item in ddlcategory.Items where item.Selected select item;
            foreach (ListItem item in query1)
            {
                CATID += item.Value + ",";
            }
            CATID = CATID.Substring(0, CATID.Length - 1);
            if (ddlcategory.SelectedValue != "")
            {
                //DataTable dt = clsreport.BindProductbyBSegment(BRANDID.Trim(), CATID.Trim(), BSID.Trim());
                DataTable dt = clsreport.BindProductbybrandandcatid(BRANDID.Trim(), CATID.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("ALL", "0"));
                ddlproduct.AppendDataBoundItems = true;
                ddlproduct.DataSource = dt;
                ddlproduct.DataValueField = "ID";
                ddlproduct.DataTextField = "NAME";
                ddlproduct.DataBind();
            }
            else
            {
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
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
            string Depot = "";
            string DepotName = "";
            var query = from ListItem item in ddlsupplingdepot.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                Depot += item.Value + ',';
                DepotName += item.Text + ',';
            }
            Depot = Depot.Substring(0, Depot.Length - 1);
            DepotName = DepotName.Substring(0, DepotName.Length - 1);

            string Brand = "";
            var querybrand = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in querybrand)
            {
                // item ...
                Brand += item.Value + ',';
            }
            Brand = Brand.Substring(0, Brand.Length - 1);

            string Category = "";
            var querycat = from ListItem item in ddlcategory.Items where item.Selected select item;
            foreach (ListItem item in querycat)
            {
                // item ...
                Category += item.Value + ',';
            }
            Category = Category.Substring(0, Category.Length - 1);
            string Product = ddlproduct.SelectedValue.Trim();
            DataSet ds = new DataSet();
            DataTable dtbatchwise = new DataTable();

            ds = clsreport.BindStockinHandDetails(Depot, Product, ddlpacksize.SelectedValue.Trim(), ddlbatch.SelectedValue.Trim(), Convert.ToDecimal(txtmrp.Text.Trim()),
                 Brand.Trim(), Category.Trim(), txtfromdate.Text.Trim(), txtxtodate.Text.Trim(), txtdate.Text.Trim(), txtsize.Text.Trim(), ddlstockdtls.SelectedValue.ToString(),
                 ddlmrpdtls.SelectedValue.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), ddlProductOwner.SelectedValue.ToString(), ddlstorelocation.SelectedValue.ToString().Trim());

            Session["Depotname"] = DepotName.Trim();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "1")
                {
                    if (ddldetails.SelectedValue == "1")
                    {
                        this.grdstock.DataSource = ds.Tables[0];
                        this.grdstock.DataBind();
                        this.grdstock.Columns[1].AllowFilter = true;
                        this.grdstock.Columns[0].Width = "125px";
                        this.grdstock.Columns[1].Width = "150px";
                        this.grdstock.Columns[2].Width = "90px";
                        this.grdstock.Columns[5].Width = "80px";
                        this.grdstock.Columns[6].Width = "100px";
                        this.grdstock.Columns[7].Width = "100px";
                        this.grdstock.Columns[8].Width = "100px";
                        this.grdstock.Columns[9].Width = "110px";
                        this.grdstock.Width = Unit.Percentage(100);
                        this.grdstock.ScrollingSettings.NumberOfFixedColumns = 5;
                    }
                    else
                    {
                        this.grdstock.DataSource = ds.Tables[1];
                        this.grdstock.DataBind();
                        this.grdstock.Columns[1].AllowFilter = true;
                        this.grdstock.Width = Unit.Percentage(100);
                        this.grdstock.ScrollingSettings.NumberOfFixedColumns = 5;
                        this.grdstock.Columns[0].Width = "125px";
                        this.grdstock.Columns[1].Width = "150px";
                        this.grdstock.Columns[2].Width = "90px";
                        this.grdstock.Columns[5].Width = "80px";
                        this.grdstock.Columns[6].Width = "115px";
                        this.grdstock.Columns[7].Width = "100px";
                        this.grdstock.Columns[8].Width = "100px";
                        this.grdstock.Columns[9].Width = "100px";
                        this.grdstock.Columns[10].Width = "100px";
                    }
                }
                else
                {
                    this.grdstock.DataSource = null;
                    this.grdstock.DataBind();
                }
            }
            else
            {
                this.grdstock.DataSource = null;
                this.grdstock.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region grdstock_Exported
    protected void grdstock_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        foreach (TableRow item in e.Table.Rows)
        {
            int cellcount = item.Cells.Count;
            for (int i = 0; i < cellcount; i++)
            {
                item.Cells[i].Style.Add("border", "thin solid black");
            }
            //not using css.
            e.Table.GridLines = GridLines.None;
        }
    }
    #endregion

    #region grdstock_Exporting
    protected void grdstock_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {

        TableRow row1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.ColumnSpan = 1;
        cell1.BorderStyle = BorderStyle.None;
        cell1.Text = "Stock In Hand Report";
        cell1.Font.Name = "Calibri";
        cell1.Font.Size = 9;
        cell1.HorizontalAlign = HorizontalAlign.Left;
        cell1.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row1.Cells.Add(cell1);
        e.Table.Rows.Add(row1);

        TableRow row3 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.ColumnSpan = 1;
        cell3.BorderStyle = BorderStyle.None;
        cell3.Text = "Depot:" + Session["Depotname"].ToString();
        cell3.Font.Name = "Calibri";
        cell3.Font.Size = 9;
        cell3.HorizontalAlign = HorizontalAlign.Left;
        cell3.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row3.Cells.Add(cell3);
        e.Table.Rows.Add(row3);

        TableRow row2 = new TableRow();
        TableCell cell2 = new TableCell();
        cell2.ColumnSpan = 1;
        cell2.BorderStyle = BorderStyle.None;
        cell2.Text = "Stock As On:" + txtdate.Text;
        cell2.Font.Name = "Calibri";
        cell2.Font.Size = 9;
        cell2.HorizontalAlign = HorizontalAlign.Left;
        cell2.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row2.Cells.Add(cell2);
        e.Table.Rows.Add(row2);
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

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = today1;
        }
        else
        {
            this.txtdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = cDate;
        }
    }
    #endregion
}