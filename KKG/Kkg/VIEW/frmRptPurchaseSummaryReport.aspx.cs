

#region Namespace
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.IO;
using System.Linq;
using System.Drawing;
#endregion


public partial class VIEW_frmRptPurchaseSummaryReport : System.Web.UI.Page
{
    ArrayList arry = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });});</script>", false);
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

            pnlDisplay.Style["display"] = "";
            LoadDepotName();
            ddldetails.SelectedValue = "1";
            ddlstatus.SelectedValue = "2";
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        if (ddldepot.SelectedValue != "")
        {
            if (hdn_excel.Value != "")
            {
                BindGrid();
            }

        }
    }

    public void LoadDepotName()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            //string Session = HttpContext.Current.Session["USERID"].ToString().Trim();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = clsreport.BindDepot();
                ddldepot.Items.Clear();
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn column;
        column = new DataColumn();
        column.Unique = true;
        dt.Clear();
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("QTYPCS", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));

        #region Loop For Adding Itemwise Tax Component


            ClsStockReport clsrpt = new ClsStockReport();
            DataSet ds = new DataSet();
            ds = clsrpt.BindSummaryDetails(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddldepot.SelectedValue.Trim());
            Session["dtTaxCount"] = ds.Tables[5];
            for (int k = 0; k < ds.Tables[5].Rows.Count; k++)
            {
                if (Convert.ToDecimal(ds.Tables[5].Rows[k]["PERCENTAGE"].ToString()) > 0)
                {
                    
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[5].Rows[k]["NAME"]) + "", typeof(string)));
                    
                }
            }
       
        #endregion

        HttpContext.Current.Session["RECEIVEDDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("NAME", typeof(string)));
        HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region BindGrid
    protected void BindGrid()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet dtDisplayGrid = new DataSet();
        string DEPOTID = string.Empty;
        var query1 = from ListItem item in ddldepot.Items where item.Selected select item;
        
            foreach (ListItem item in query1)
            {
                // item ...
                DEPOTID += item.Value + ',';

            }
            DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);
       
        dtDisplayGrid = clsrpt.BindSummaryDetails(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DEPOTID.Trim());
        if (dtDisplayGrid.Tables[0].Rows.Count > 0)
        {
            CreateDataTable();
            CreateDataTableTaxComponent();

            #region Item-wise Tax Component
            if (dtDisplayGrid.Tables[4].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TAXCOMPONENTDETAILS"];

                for (int i = 0; i < dtDisplayGrid.Tables[4].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["TAXID"] = Convert.ToString(dtDisplayGrid.Tables[4].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(dtDisplayGrid.Tables[4].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(dtDisplayGrid.Tables[4].Rows[i]["TAXVALUE"]);
                    dr["NAME"] = Convert.ToString(dtDisplayGrid.Tables[4].Rows[i]["NAME"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }

                HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion


            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
            }
            #endregion


            DataTable dt = (DataTable)Session["RECEIVEDDETAILS"];
            for (int i = 0; i < dtDisplayGrid.Tables[1].Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["STOCKRECEIVEDID"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["STOCKRECEIVEDID"]);
                dr["PRODUCTID"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["PRODUCTID"]);
                dr["PRODUCTNAME"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["PRODUCTNAME"]);
                dr["QTY"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["QTY"]);
                dr["QTYPCS"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["QTYPCS"]);
                dr["PACKINGSIZENAME"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                dr["RATE"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["RATE"]);
                dr["AMOUNT"] = Convert.ToString(dtDisplayGrid.Tables[1].Rows[i]["AMOUNT"]);

                for (int j = 0; j < dtTaxCountDataAddition.Rows.Count; j++)
                {
                    for (int k = 0; k < dtDisplayGrid.Tables[4].Rows.Count; k++)
                    {
                        if ((dtDisplayGrid.Tables[1].Rows[i]["PRODUCTID"].ToString() == dtDisplayGrid.Tables[4].Rows[k]["PRODUCTID"].ToString()) && (dtDisplayGrid.Tables[1].Rows[i]["STOCKRECEIVEDID"].ToString() == dtDisplayGrid.Tables[4].Rows[k]["STOCKRECEIVEDID"].ToString()) && Convert.ToString(dtTaxCountDataAddition.Rows[j]["TAXID"]).Trim() == Convert.ToString(dtDisplayGrid.Tables[4].Rows[k]["TAXID"]).Trim())
                        {

                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[j]["NAME"]) + ""] = Convert.ToString(dtDisplayGrid.Tables[4].Rows[k]["TAXVALUE"]);
                        }
                    }
                }

                dt.Rows.Add(dr);
                dt.AcceptChanges();

            }
            HttpContext.Current.Session["SALEINVOICEDETAILS"] = dt;


            //dtDisplayGrid_details = clsrpt.BindStockReceipt_Details(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddldepot.SelectedValue.Trim());

            this.gvSummaryreport.DataSource = dtDisplayGrid.Tables[0];
            this.gvSummaryreport.DataBind();

            this.childgrid.DataSource = dt;
            this.childgrid.DataBind();
            hdn_excel.Value = "1";
        }
        else
        {
            this.gvSummaryreport.DataSource = null;
            this.gvSummaryreport.DataBind();

            this.childgrid.DataSource = null;
            this.childgrid.DataBind();
        }
           
        
    }
    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {      
        BindGrid();       
    }

    #region gvSummaryreport_Exported
    protected void gvSummaryreport_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region gvSummaryreport_Exporting
    protected void gvSummaryreport_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 5;
        cell.BorderStyle = BorderStyle.None;
        
        cell.Text = "Purchase Summary Details";
        
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;      
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region childgrid_Exported
    protected void childgrid_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region btnPDFExport_Click
    protected void btnPDFExport_Click(object sender, EventArgs e)
    {
        // Export all pages
        
    }
    #endregion

    
    
}