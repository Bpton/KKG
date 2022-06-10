using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPBLL;
using System.Data;
using Obout.Grid;

public partial class VIEW_frmRptPlanningVsProduction : System.Web.UI.Page
{
    ClsStockReport objStockReport = new ClsStockReport();
    DateTime today1 = DateTime.Now;
    DataTable objDt = new DataTable();
    string date = "dd/MM/yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlDept').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlProduct').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                this.txtFromDate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtenderFromDate.EndDate = DateTime.Now;
                this.txtTodate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtenderTodate.EndDate = DateTime.Now;
                this.loadproductname("FG");
            }
        }
        catch (Exception EX)
        {

        }
    }

    public void loadproductname(string mode)
    {
        try
        {
            objDt = objStockReport.loadRequisitionIssuedept(mode);
            if (objDt.Rows.Count > 0)
            {
                this.ddlProduct.Items.Clear();
                this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                this.ddlProduct.AppendDataBoundItems = true;
                this.ddlProduct.DataSource = objDt;
                this.ddlProduct.DataTextField = "NAME";
                this.ddlProduct.DataValueField = "ID";  
                this.ddlProduct.DataBind();
            }
        }

        catch (Exception ex)
        {

        }
    }

    public void BindGrid()
    {
        try
        {
            string rDeptId = string.Empty;
            string productId = string.Empty;
            var query2 = from ListItem item in ddlProduct.Items where item.Selected select item;
            foreach (ListItem item in query2)
            {
                productId += item.Value + ',';
            }
            productId = productId.Substring(0, productId.Length - 1);

            objDt = objStockReport.Bind_planning_vs_production(this.txtFromDate.Text, this.txtTodate.Text, productId);
            if (objDt.Rows.Count > 0)
            {
                this.grdRptData.DataSource = objDt;
                this.grdRptData.DataBind();
            }
            else
            {
                this.grdRptData.DataSource = null;
                this.grdRptData.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    #region grdRptData_Exporting
    protected void grdRptData_Exporting(object sender, GridExportEventArgs e)
    {
        TableRow row1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.ColumnSpan = 16;
        cell1.BorderStyle = BorderStyle.None;
        cell1.Text = "Planning Vs Production Qty";
        cell1.Font.Name = "Helvetica";
        cell1.Font.Size = 14;
        cell1.HorizontalAlign = HorizontalAlign.Left;
        cell1.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row1.Cells.Add(cell1);
        e.Table.Rows.Add(row1);

        TableRow row3 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.ColumnSpan = 21;
        cell3.BorderStyle = BorderStyle.None;
        cell3.Text = "Details: KKG";
        cell3.Font.Name = "Calibri";
        cell3.Font.Size = 9;
        cell3.HorizontalAlign = HorizontalAlign.Left;
        cell3.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row3.Cells.Add(cell3);
        e.Table.Rows.Add(row3);

        TableRow row2 = new TableRow();
        TableCell cell2 = new TableCell();
        cell2.ColumnSpan = 21;
        cell2.BorderStyle = BorderStyle.None;
        cell2.Text = "Period:" + txtFromDate.Text + " To " + txtTodate.Text;
        cell2.Font.Name = "Calibri";
        cell2.Font.Size = 9;
        cell2.HorizontalAlign = HorizontalAlign.Left;
        cell2.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row2.Cells.Add(cell2);
        e.Table.Rows.Add(row2);
    }
    #endregion

    protected void grdRptData_Exported(object sender, GridExportEventArgs e)
    {

        int Length = grdRptData.TotalRowCount;
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

    protected void grdRptData_RowDataBound(object sender, GridRowEventArgs e)
    {

    }
}