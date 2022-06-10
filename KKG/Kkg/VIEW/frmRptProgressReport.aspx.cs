#region Namespace
using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class VIEW_frmRptProgressReport : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlType').multiselect({ includeSelectAllOption: true  });});</script>", false);
        if (!IsPostBack)
        {

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

            pnlDisplay.Style["display"] = "";
            BindProductType();
            //BindBrand();
            //BINDCATEGORY();
            LoadDepotName();
            LoadStorelocation();
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
            ViewState["Opening"] = "0.00";
            ViewState["Closing"] = "0.00";
            divSummeryData.Visible = false;
            DivbtnExportSummery.Visible = false;
        }
        //gvRptProgress.ExportingSettings.FileName = "ProgressReport_" + DateTime.Now.ToString("dd/MM/yyyy") + "_" + ddldepot.SelectedItem.Text.Trim() + "";
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
                DataTable dt = clsreport.BindDepot_Primary(); //BindOnly_Depot();
                ddldepot.Items.Clear();
                //ddldepot.Items.Add(new ListItem("Select", "0"));
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = clsreport.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddldepot.Items.Clear();
                //ddldepot.Items.Add(new ListItem("Select", "0"));
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
                ddldepot.SelectedValue = HttpContext.Current.Session["DEPOTID"].ToString();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddldepot.Items.Clear();
                //ddldepot.Items.Add(new ListItem("Select", "0"));
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

    public void BindProductType()
    {
        ClsVendor_TPU obj = new ClsVendor_TPU();
        DataTable dt = obj.BindPoFromTpu("P-TYPE", "");
        ddlType.Items.Clear();
        ddlType.DataSource = dt;
        ddlType.DataTextField = "NAME";
        ddlType.DataValueField = "ID";
        ddlType.DataBind();
    }

    #region BindBrand
    public void BindBrand()
    {
        ClsVendor_TPU clsreport = new ClsVendor_TPU();
       
        string BRANDID = "";
        string CATID = "";
        var query = from ListItem item in ddlType.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            BRANDID += item.Value + ",";
        }
        
        DataTable dt = clsreport.BindPoFromTpu("P-DIV", BRANDID);
        ddlbrand.Items.Clear();
        ddlcategory.Items.Clear();
        ddlbrand.DataSource = dt;
        ddlbrand.DataTextField = "DIVNAME";
        ddlbrand.DataValueField = "DIVID";
        ddlbrand.DataBind();

        //var query1 = from ListItem item in ddlbrand.Items select item;
        //foreach (ListItem item in query1)
        //{
        //    // item ...
        //    CATID += item.Value + ",";
        //}

        //string[] values1 = CATID.Split(',');

        //if ((ddlbrand.Items.Count > 0) && (values1.Length > 0))
        //{
        //    for (int i2 = 0; i2 < ddlbrand.Items.Count; i2++)
        //    {
        //        for (int i = 0; i < values1.Length; i++)
        //        {
        //            if (ddlbrand.Items[i2].Value.ToString().Trim() == values1[i].Trim())
        //            {
        //                ddlbrand.Items[i2].Selected = true;
        //            }
        //        }
        //    }
        //}
        //var QueryDepot = from ListItem item in ddldepot.Items where item.Selected select item;
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
            ddlstorelocation.Items.Insert(0, new ListItem("All", "0"));
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dt;
            ddlstorelocation.DataValueField = "ID";
            ddlstorelocation.DataTextField = "NAME";
            ddlstorelocation.DataBind();
            //ddlstorelocation.SelectedValue = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";
        }
        catch (Exception ex)
        {
            throw ex;
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
                LoadCategoryByBrand();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Load Category Brand Wise
    public void LoadCategoryByBrand()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            string BRANDID = "";
            string CATID = "";
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

            var query1 = from ListItem item in ddlcategory.Items select item;
            foreach (ListItem item in query1)
            {
                // item ...
                CATID += item.Value + ",";
            }

            string[] values1 = CATID.Split(',');

            if ((ddlcategory.Items.Count > 0) && (values1.Length > 0))
            {
                for (int i2 = 0; i2 < ddlcategory.Items.Count; i2++)
                {
                    for (int i = 0; i < values1.Length; i++)
                    {
                        if (ddlcategory.Items[i2].Value.ToString().Trim() == values1[i].Trim())
                        {
                            ddlcategory.Items[i2].Selected = true;
                        }
                    }
                }
            }
            var QueryDepot = from ListItem item in ddldepot.Items where item.Selected select item;
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region BINDCATEGORY
    public void BINDCATEGORY()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            ddlcategory.Items.Clear();
            ddlcategory.DataSource = clsrpt.BindCategory();
            ddlcategory.DataTextField = "CATNAME";
            ddlcategory.DataValueField = "CATID";
            ddlcategory.DataBind();
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
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable dtDisplayGrid = new DataTable();
            string BRANDID = "";
            string CATID = "";
            string DepotID = string.Empty;
            string DepotName = "";
            var query = from ListItem item in ddlbrand.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BRANDID += item.Value + ",";

            }
            var query1 = from ListItem item in ddlcategory.Items where item.Selected select item;
            foreach (ListItem item in query1)
            {
                // item ...
                CATID += item.Value + ",";
            }
            var QueryDepot = from ListItem item in ddldepot.Items where item.Selected select item;
            foreach (ListItem item in QueryDepot)
            {
                // item ...
                DepotID += item.Value + ",";
                DepotName += item.Text + ',';
            }
            DepotID = DepotID.Substring(0, DepotID.Length - 1);
            DepotName = DepotName.Substring(0, DepotName.Length - 1);
            BRANDID = BRANDID.Substring(0, BRANDID.Length - 1);
            CATID = CATID.Substring(0, CATID.Length - 1);

            if (ddldetails.SelectedValue == "0")
            {
                divDetailsData.Visible = true;
                divSummeryData.Visible = false;
                DivbtnExportDetails.Visible = true;
                DivbtnExportSummery.Visible = false;

                DataTable dtDisplayGrid_details = new DataTable();
                dtDisplayGrid = clsrpt.BindprogressReportDetails(txtfromdate.Text.Trim(), txttodate.Text.Trim(), BRANDID, CATID, DepotID, ddlstorelocation.SelectedValue.ToString());
                if (dtDisplayGrid.Rows.Count > 0)
                {
                    int INITIAL_VALUE = 8;
                    int COUNT_COL = dtDisplayGrid.Columns.Count;
                    for (INITIAL_VALUE = 5; INITIAL_VALUE < COUNT_COL; INITIAL_VALUE++)
                    {
                        ViewState["ParentGrid_FooterTotal" + INITIAL_VALUE] = null;
                        ViewState["ParentGrid_FooterTotal" + INITIAL_VALUE] = 0;
                    }
                    ViewState["GridRowIndex"] = 0;
                    this.gvRptProgress.DataSource = dtDisplayGrid;
                    this.gvRptProgress.DataBind();
                    Cache["Depotname"] = DepotName.Trim();
                }
                else
                {
                    this.gvRptProgress.DataSource = null;
                    this.gvRptProgress.DataBind();
                }
            }
            else
            {
                divDetailsData.Visible = false;
                divSummeryData.Visible = true;

                DivbtnExportDetails.Visible = false;
                DivbtnExportSummery.Visible = true;

                DataTable dtDisplayGrid_details = new DataTable();
                dtDisplayGrid = clsrpt.BindprogressReport_SummeryDetails(txtfromdate.Text.Trim(), txttodate.Text.Trim(), BRANDID, CATID, DepotID, ddlstorelocation.SelectedValue.ToString());
                if (dtDisplayGrid.Rows.Count > 0)
                {
                    int INITIAL_VALUE = 8;
                    int COUNT_COL = dtDisplayGrid.Columns.Count;
                    for (INITIAL_VALUE = 3; INITIAL_VALUE < COUNT_COL; INITIAL_VALUE++)
                    {
                        ViewState["ParentGrid_FooterTotal_Summery" + INITIAL_VALUE] = null;
                        ViewState["ParentGrid_FooterTotal_Summery" + INITIAL_VALUE] = 0;
                    }
                    ViewState["GridRowIndex_Summery"] = 0;
                    this.gvRptProgressSummeryData.DataSource = dtDisplayGrid;
                    this.gvRptProgressSummeryData.DataBind();
                    Cache["Depotname"] = DepotName.Trim();
                }
                else
                {
                    this.gvRptProgressSummeryData.DataSource = null;
                    this.gvRptProgressSummeryData.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #endregion

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    #region gvRptProgress_Exported
    protected void gvRptProgress_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {

        int Length = gvRptProgress.TotalRowCount;
        if (Length > 0)
        {
            TableRow row = new TableRow();
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
    }
    #endregion

    #region gvRptProgress_RowDataBound
    protected void gvRptProgress_RowDataBound(object sender, GridRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == GridRowType.DataRow)
        //    {
        //        for (int cellno = 5; cellno < gvRptProgress.Columns.Count; cellno++)
        //        {
        //            ViewState["ParentGrid_FooterTotal" + cellno] = Convert.ToString(double.Parse(ViewState["ParentGrid_FooterTotal" + cellno].ToString()) + double.Parse(e.Row.Cells[cellno].Text));
        //            ViewState["GridRowIndex"] = e.Row.RowIndex;
        //        }
        //    }

        //    if (e.Row.RowType == GridRowType.ColumnFooter)
        //    {
        //        for (int cellno = 5; cellno < gvRptProgress.Columns.Count; cellno++)
        //        {
        //            e.Row.Cells[2].Text = "Grand Total";
        //            gvRptProgress.Columns[cellno].Align = "Right";
        //            if (ViewState["ParentGrid_FooterTotal" + cellno] != null && ViewState["ParentGrid_FooterTotal" + cellno].ToString() != "")
        //            {
        //                decimal Grand_Total = Convert.ToDecimal(ViewState["ParentGrid_FooterTotal" + cellno].ToString());
        //                e.Row.Cells[cellno].Text = Grand_Total.ToString("n2");
        //            }

        //            e.Row.Cells[cellno].ForeColor = System.Drawing.Color.Blue;
        //            e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
        //        }
        //        for (int cellno = 5; cellno < gvRptProgress.Columns.Count; cellno++)
        //        {
        //            ViewState["ParentGrid_FooterTotal" + cellno] = null;
        //            ViewState["ParentGrid_FooterTotal" + cellno] = 0;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    #endregion

    #region gvRptProgress_Exporting
    protected void gvRptProgress_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.ColumnSpan = 16;
        cell1.BorderStyle = BorderStyle.None;
        cell1.Text = "Progress Report";
        cell1.Font.Name = "Calibri";
        cell1.Font.Size = 9;
        cell1.HorizontalAlign = HorizontalAlign.Left;
        cell1.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row1.Cells.Add(cell1);
        e.Table.Rows.Add(row1);

        TableRow row3 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.ColumnSpan = 16;
        cell3.BorderStyle = BorderStyle.None;
        cell3.Text = "Depot:" + ddldepot.SelectedItem.Text;
        cell3.Font.Name = "Calibri";
        cell3.Font.Size = 9;
        cell3.HorizontalAlign = HorizontalAlign.Left;
        cell3.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row3.Cells.Add(cell3);
        e.Table.Rows.Add(row3);

        TableRow row2 = new TableRow();
        TableCell cell2 = new TableCell();
        cell2.ColumnSpan = 16;
        cell2.BorderStyle = BorderStyle.None;
        cell2.Text = "Period:" + txtfromdate.Text + " To " + txttodate.Text;
        cell2.Font.Name = "Calibri";
        cell2.Font.Size = 9;
        cell2.HorizontalAlign = HorizontalAlign.Left;
        cell2.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row2.Cells.Add(cell2);
        e.Table.Rows.Add(row2);
    }
    #endregion

    #region gvRptProgressSummeryData_Exported
    protected void gvRptProgressSummeryData_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        int Length = gvRptProgress.TotalRowCount;
        if (Length > 0)
        {
            TableRow row = new TableRow();
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
    }
    #endregion

    #region gvRptProgressSummeryData_RowDataBound
    protected void gvRptProgressSummeryData_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                for (int cellno = 4; cellno < gvRptProgressSummeryData.Columns.Count; cellno++)
                {
                    ViewState["ParentGrid_FooterTotal_Summery" + cellno] = Convert.ToString(double.Parse(ViewState["ParentGrid_FooterTotal_Summery" + cellno].ToString()) + double.Parse(e.Row.Cells[cellno].Text));
                    ViewState["GridRowIndex_Summery"] = e.Row.RowIndex;
                }
            }

            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                for (int cellno = 4; cellno < gvRptProgressSummeryData.Columns.Count; cellno++)
                {
                    e.Row.Cells[2].Text = "Grand Total";
                    gvRptProgressSummeryData.Columns[cellno].Align = "Right";
                    if (ViewState["ParentGrid_FooterTotal_Summery" + cellno] != null && ViewState["ParentGrid_FooterTotal_Summery" + cellno].ToString() != "")
                    {
                        decimal Grand_Total = Convert.ToDecimal(ViewState["ParentGrid_FooterTotal_Summery" + cellno].ToString());
                        e.Row.Cells[cellno].Text = Grand_Total.ToString("n2");
                    }
                    e.Row.Cells[cellno].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
                }
                for (int cellno = 4; cellno < gvRptProgressSummeryData.Columns.Count; cellno++)
                {
                    ViewState["ParentGrid_FooterTotal_Summery" + cellno] = null;
                    ViewState["ParentGrid_FooterTotal_Summery" + cellno] = 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region gvRptProgressSummeryData_Exporting
    protected void gvRptProgressSummeryData_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.ColumnSpan = 16;
        cell1.BorderStyle = BorderStyle.None;
        cell1.Text = "Progress Report";
        cell1.Font.Name = "Calibri";
        cell1.Font.Size = 9;
        cell1.HorizontalAlign = HorizontalAlign.Left;
        cell1.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row1.Cells.Add(cell1);
        e.Table.Rows.Add(row1);

        TableRow row3 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.ColumnSpan = 6;
        cell3.BorderStyle = BorderStyle.None;
        cell3.Text = "Depot:" + Cache["Depotname"].ToString();
        cell3.Font.Name = "Calibri";
        cell3.Font.Size = 9;
        cell3.HorizontalAlign = HorizontalAlign.Left;

        cell3.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row3.Cells.Add(cell3);
        e.Table.Rows.Add(row3);

        TableRow row2 = new TableRow();
        TableCell cell2 = new TableCell();
        cell2.ColumnSpan = 16;
        cell2.BorderStyle = BorderStyle.None;
        cell2.Text = "Period:" + txtfromdate.Text + " To " + txttodate.Text;
        cell2.Font.Name = "Calibri";
        cell2.Font.Size = 9;
        cell2.HorizontalAlign = HorizontalAlign.Left;
        cell2.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row2.Cells.Add(cell2);
        e.Table.Rows.Add(row2);
    }
    #endregion

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBrand();    
    }
}