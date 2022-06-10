using BAL;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptGSTReturnOutward : System.Web.UI.Page
{

    #region Page_Init
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });});</script>", false);
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      HPD       //

            ClsStockReport clsstartfy = new ClsStockReport();
            string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

            DateTime oDate = Convert.ToDateTime(STARTDATE);

            DateTime cDate = oDate.AddSeconds(31535999);

            CalendarExtender2.StartDate = oDate;
            CalendarExtender3.EndDate = cDate;

            //            ******  END LOCK CAELENDER   ***********      HPD    //

            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.LoadState();           
        }
    }
    #endregion

    #region LoadState
    public void LoadState()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (ddlstate.SelectedValue == "")
            {
                DataTable dt = clsreport.BindState();
                ddlstate.Items.Clear();
                ddlstate.Items.Add(new ListItem("Select", "0"));
                ddlstate.AppendDataBoundItems = true;
                ddlstate.DataSource = dt;
                ddlstate.DataValueField = "STATE_ID";
                ddlstate.DataTextField = "STATE_NAME";
                ddlstate.DataBind();
            }
            else
            {
                ddlstate.Items.Clear();
                ddlstate.Items.Add(new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }


    }
    #endregion

    #region ddlstateselectedindexchange
    public void ddlstateselectedindexchange(object sender, EventArgs e)
    {
        LoadDepot();
    }
    #endregion

    #region MyRegion
    public void LoadDepot()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindDepotstatewise(ddlstate.SelectedValue.Trim());
            ddldepot.Items.Clear();
            ddldepot.AppendDataBoundItems = true;
            ddldepot.DataSource = dt;
            ddldepot.DataTextField = "BRPREFIX";
            ddldepot.DataValueField = "BRID";
            ddldepot.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    #endregion

    #region BindGrid
    protected void BindGrid()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable dtDisplayGrid = new DataTable();
            string DepotID = string.Empty;
            var query = from ListItem item in ddldepot.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                DepotID += item.Value + ",";

            }
            if (DepotID.Trim() != "")
            {
                DepotID = DepotID.Substring(0, DepotID.Length - 1);
            }
            dtDisplayGrid = clsrpt.gstoutwardreturn(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DepotID.Trim());

            if (dtDisplayGrid.Rows.Count > 0)
            {
                grdinwardsupply.DataSource = dtDisplayGrid;
                grdinwardsupply.DataBind();
            }
            else
            {
                grdinwardsupply.DataSource = null;
                grdinwardsupply.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion

    #region btnShow_Click
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion
    

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {

    }
    #endregion

    #region gvStockreport_Exported
    protected void gvSummaryreport_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        try
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
            // e.Table.GridLines = GridLines.Both;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion

    #region gvSummaryreport_Exporting
    protected void gvSummaryreport_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        try
        {
            int colcount = 0;

            grdinwardsupply.ExportingSettings.ExportDetails = true;
            colcount = grdinwardsupply.Columns.Count;

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            cell.ColumnSpan = colcount;
            cell.BorderStyle = BorderStyle.None;

            cell.Text = "McNroe Consumer Products Private Limited ( " + this.ddldepot.SelectedItem.Text.Trim() + " )";
            //cell.BackColor = Color.White;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.Font.Bold = true;
            e.Table.GridLines = GridLines.None;
            row.Cells.Add(cell);
            e.Table.Rows.Add(row);


            TableRow row2 = new TableRow();
            TableCell cell2 = new TableCell();

            cell2.ColumnSpan = colcount;
            cell2.BorderStyle = BorderStyle.None;
            cell2.Text = "GST Outward Return Report";
            //cell2.BackColor = Color.Transparent;
            cell2.HorizontalAlign = HorizontalAlign.Left;
            cell2.Font.Bold = true;
            e.Table.GridLines = GridLines.None;
            row2.Cells.Add(cell2);
            e.Table.Rows.Add(row2);


            TableRow row1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.ColumnSpan = colcount;
            cell1.BorderStyle = BorderStyle.None;

            cell1.Text = "Period :" + txtfromdate.Text + " to " + txttodate.Text;
            //cell1.BackColor = Color.White;
            cell1.HorizontalAlign = HorizontalAlign.Left;
            cell1.Font.Bold = true;
            e.Table.GridLines = GridLines.None;
            row1.Cells.Add(cell1);
            e.Table.Rows.Add(row1);

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
}


