using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Drawing;


public partial class VIEW_frmRptSupplyOutward : System.Web.UI.Page
{

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlparty').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlproduct').multiselect({ includeSelectAllOption: true  });});</script>", false);

            if (!IsPostBack)
            {

                //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      HPD       //

                ClsStockReport clsstartfy = new ClsStockReport();
                string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

                DateTime oDate = Convert.ToDateTime(STARTDATE);

                DateTime cDate = oDate.AddSeconds(31535999);

                CalendarExtender4.StartDate = oDate;
                CalendarExtender1.EndDate = cDate;

                //            ******  END LOCK CAELENDER   ***********      HPD    //

                this.LoadDepotName();
                this.LoadBrand();
                LoadCategory();
                txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                LoadAllProduct();
            }
            grdstock.ExportingSettings.FileName = "Supply_"+ddltype.SelectedItem.Text+"_" + txtdate.Text.Trim() + "_" + HttpContext.Current.Session["USERNAME"].ToString().Trim() + "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadAllProduct
    public void LoadAllProduct()
    {
        ClsStockReport clsreport = new ClsStockReport();
        try
        {
            DataTable dt = clsreport.BindProduct();
            ddlproduct.Items.Clear();
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

            string BRANDID = "";
            var query = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BRANDID += item.Value + "','";
            }
            if (BRANDID.Trim()!="")
            {
            BRANDID = BRANDID.Substring(0, BRANDID.Length - 3);
            }

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

    #region ddlcategory_SelectedIndexChanged
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcategory.SelectedValue != "")
            {
                LoadProduct();
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
                LoadProduct();
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

    #region LoadParty
    public void LoadParty()
    {
        try
        {
            DataTable dtdist = new DataTable();
            ClsStockReport clsrpt = new ClsStockReport();

            //if (Session["TPU"].ToString() == "ADMIN")
            //{
                string DepotID = string.Empty;
                var query = from ListItem item in ddlsupplingdepot.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    // item ...
                    DepotID += item.Value + ",";

                }

                if (DepotID.Length > 1)
                {
                    DepotID = DepotID.Substring(0, DepotID.Length - 1);
                }
                if (DepotID.ToString() == "")
                {
                    dtdist = clsrpt.BindDistributorbyAdmin();
                }
                else
                {
                    if (ddltype.SelectedValue == "1")
                    {
                        dtdist = clsrpt.BindDistributorbyAdminWithDepot(DepotID.Trim());
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                    else if (ddltype.SelectedValue == "2")
                    {
                        dtdist = clsrpt.BindTpu_Depot();
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "BRNAME";
                        this.ddlparty.DataValueField = "ID";
                        this.ddlparty.DataBind();
                    }                    
                }                
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ddlsupplingdepot_OnSelectedIndexChanged
    protected void ddlsupplingdepot_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlsupplingdepot.SelectedValue.Trim() != "")
            {
                LoadParty();
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
            DataTable dt = new DataTable();

            string BRANDID = "";
            var query = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BRANDID += item.Value + ',';
            }
            if (BRANDID.Length > 1)
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
            if (CATID.Length > 1)
            {
                CATID = CATID.Substring(0, CATID.Length - 1);
            }

            if (BRANDID != "" && CATID == "")
            {

                dt = clsreport.BindProductbybrandid(BRANDID);
                ddlproduct.Items.Clear();
                ddlproduct.AppendDataBoundItems = true;
                ddlproduct.DataSource = dt;
                ddlproduct.DataValueField = "ID";
                ddlproduct.DataTextField = "NAME";
                ddlproduct.DataBind();
            }
            else if (BRANDID == "" && CATID != "")
            {

                dt = clsreport.BindProductbycatid(CATID);
                ddlproduct.Items.Clear();
                ddlproduct.AppendDataBoundItems = true;
                ddlproduct.DataSource = dt;
                ddlproduct.DataValueField = "ID";
                ddlproduct.DataTextField = "NAME";
                ddlproduct.DataBind();
            }
            else if (BRANDID != "" && CATID != "")
            {

                dt = clsreport.BindProductbybrandandcatid(BRANDID, CATID);
                ddlproduct.Items.Clear();
                ddlproduct.AppendDataBoundItems = true;
                ddlproduct.DataSource = dt;
                ddlproduct.DataValueField = "ID";
                ddlproduct.DataTextField = "NAME";
                ddlproduct.DataBind();
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
            string Party = "";
            var queryparty = from ListItem item in ddlparty.Items where item.Selected select item;
            foreach (ListItem item in queryparty)
            {
                // item ...
                Party += item.Value + ',';

            }
            Party = Party.Substring(0, Party.Length - 1);

            string Depot = "";
            var querydepot = from ListItem item in ddlsupplingdepot.Items where item.Selected select item;
            foreach (ListItem item in querydepot)
            {
                // item ...
                Depot += item.Value + ',';

            }
            Depot = Depot.Substring(0, Depot.Length - 1);

            string ProductID = "";
            var queryproduct = from ListItem item in ddlproduct.Items where item.Selected select item;
            foreach (ListItem item in queryproduct)
            {
                // item ...
                ProductID += item.Value + ',';

            }
            ProductID = ProductID.Substring(0, ProductID.Length - 1);


            DataTable dt = new DataTable();
            dt = clsreport.BindOutwardSupply(txtdate.Text.Trim(), txttodate.Text.Trim(), Party.Trim(), ProductID.Trim(),ddltype.SelectedValue.Trim(),Depot.Trim());

            if (dt.Rows.Count > 0)
            {

                this.grdstock.DataSource = dt;
                this.grdstock.DataBind();
                this.grdstock.ScrollingSettings.NumberOfFixedColumns = 6;
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

    protected void grdstock_OnRowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[5] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "TOTAL :")
                {
                    e.Row.ForeColor = Color.Blue;
                    e.Row.Font.Bold = true;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

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
        cell1.ColumnSpan = 4;
        cell1.BorderStyle = BorderStyle.None;
        if (ddltype.SelectedValue == "1")
        {
            cell1.Text = "Supply Outward";
        }
        else
        {
            cell1.Text = "Supply Inward";
        }
        
        cell1.Font.Name = "Calibri";
        cell1.Font.Size = 9;
        cell1.HorizontalAlign = HorizontalAlign.Left;
        cell1.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row1.Cells.Add(cell1);
        e.Table.Rows.Add(row1);
       
    }
    #endregion

}