using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;

public partial class VIEW_frmRptRequisitionVsIssueQty : System.Web.UI.Page
{
    ClsStockReport objStockReport = new ClsStockReport();
    DateTime today1 = DateTime.Now;
    DataTable objDt = new DataTable();
    string date = "dd/MM/yyyy";
    string deptid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlRequisitionDept').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlIssueDept').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlProduct').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                deptid = HttpContext.Current.Session["DEPARTMENTID"].ToString().Trim();
                this.txtFromDate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtenderFromDate.EndDate = DateTime.Now;
                this.txtTodate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtenderTodate.EndDate = DateTime.Now;
               
                this.loadRequisitionDept("REQ");
                this.loadIssueDept("ISS");
                this.loadproductname("PRO");    
            }
        }
        catch(Exception ex)
        {

        }

    }


    public void loadRequisitionDept(string mode)
    {
        try
        {
            objDt = objStockReport.loadRequisitionIssuedept(mode);
            if (objDt.Rows.Count > 0)
            {
                this.ddlRequisitionDept.Items.Clear();
                this.ddlRequisitionDept.AppendDataBoundItems = true;
                this.ddlRequisitionDept.DataSource = objDt;
                this.ddlRequisitionDept.DataTextField = "NAME";
                this.ddlRequisitionDept.DataValueField = "ID";
                this.ddlRequisitionDept.DataBind();
                if(deptid != "4DF535B2-AC83-4314-B953-F1CE5FC57E4A")
                {
                   for(int i=1; i < objDt.Rows.Count; i++)
                   {
                       string value = objDt.Rows[i]["ID"].ToString();
                       if (deptid == value)
                       {
                           int value_row_index =i;
                           ddlRequisitionDept.Items[value_row_index].Selected = true;
                               
                       }
                   }
                }
            }
        }
       
        catch (Exception ex)
        {

        }

    }

    public void loadIssueDept(string mode)
    {
       
        try
        {
            objDt = objStockReport.loadRequisitionIssuedept(mode);
            if (objDt.Rows.Count > 0)
            {
                this.ddlIssueDept.Items.Clear();
                this.ddlIssueDept.Items.Add(new ListItem("Select dept", "0"));
                this.ddlIssueDept.AppendDataBoundItems = true;
                this.ddlIssueDept.DataSource = objDt;
                this.ddlIssueDept.DataTextField = "NAME";
                this.ddlIssueDept.DataValueField = "ID";
                this.ddlIssueDept.DataBind();
            }
        }

        catch (Exception ex)
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
            string iDeptId = string.Empty;
            string productId = string.Empty;

            var query = from ListItem item in ddlRequisitionDept.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                rDeptId += item.Value + ',';
            }
            rDeptId = rDeptId.Substring(0, rDeptId.Length - 1);

           

            var query1 = from ListItem item in ddlIssueDept.Items where item.Selected select item;
            foreach (ListItem item in query1)
            {
                iDeptId += item.Value + ',';
            }
            iDeptId = iDeptId.Substring(0, iDeptId.Length - 1);

            var query2 = from ListItem item in ddlProduct.Items where item.Selected select item;
            foreach (ListItem item in query2)
            {
                productId += item.Value + ',';
            }
            productId = productId.Substring(0, productId.Length - 1);

            objDt = objStockReport.BindRequisitionVsIssueQty(this.txtFromDate.Text, this.txtTodate.Text, rDeptId, iDeptId, productId,this.ddlIssueType.SelectedValue);
            if(objDt.Rows.Count>0)
            {
                divDetailsData.Visible = true;
                this.grdRptData.DataSource = objDt;
                this.grdRptData.DataBind();
            }
            else
            {
                this.grdRptData.DataSource = null;
                this.grdRptData.DataBind();
            }
        }
        catch(Exception ex)
        {

        }
    }

    #region grdRptData_Exporting
    protected void grdRptData_Exporting(object sender, GridExportEventArgs e)
    {
        TableRow row1 = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.ColumnSpan = 16;
        cell1.BorderStyle = BorderStyle.None;
        cell1.Text = "Requisition Vs Issue Qty";
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
        cell3.Text = "Factory:KKG";
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


   
    protected void grdRptData_RowDataBound(object sender, GridRowEventArgs e)
    {

    }
}