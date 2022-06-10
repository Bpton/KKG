using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPBLL;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

public partial class VIEW_frmRptLedgerWiseInvoiceDetails : System.Web.UI.Page
{
    DateTime today1 = DateTime.Now;
    string date = "dd/MM/yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlLedger').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                this.DateLock();
                date = Convert.ToString(today1);
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                loadLedgerName();
            }
        }
        catch(Exception x)
        {   

        }
        

    }


    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            CalendarExtender1.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
        


            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    public void loadLedgerName()
    {
        String depotId = Session["DEPOTID"].ToString();
        BAL.ClsStockReport clsrpt = new BAL.ClsStockReport();
        DataTable dtledger = new DataTable();
        dtledger = clsrpt.BindForLedgerReport_DepotWise(depotId);
        if(dtledger.Rows.Count > 0)
        {
            ddlLedger.Items.Clear();
            ddlLedger.DataSource = dtledger;
            ddlLedger.DataTextField = "LedgerName";
            ddlLedger.DataValueField = "LedgerId";
            ddlLedger.DataBind();
        }
        else
        {
            ddlLedger.Items.Clear();
        }

    }

    protected void grvParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dt = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            Label lblVOUCHERID = (Label)e.Row.FindControl("lblVOUCHERID");
            Label lblAPPROVED = (Label)e.Row.FindControl("lblAPPROVED");
            string txtvoucherId = lblVOUCHERID.Text.Trim();
            string txtApproved = lblAPPROVED.Text.Trim();


            if(txtApproved == "Approved")
            {
                lblAPPROVED.ForeColor = Color.Green;
            }
            else
            {
                lblAPPROVED.ForeColor = Color.Red;
            }


            if (txtvoucherId != "")
            {
                dt = clsrpt.BindInvoicedetails(txtvoucherId);
                GridView grdChild = (GridView)e.Row.FindControl("grdChild");
                if (dt.Rows.Count > 0)
                {
                    grdChild.DataSource = dt;
                    grdChild.DataBind();
                }
                else
                {
                    grdChild.DataSource = null;
                    grdChild.DataBind();
                }

            }

            else
            {

            }

        }
    }   

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch(Exception ex)
        {

        }
    }

    public void BindGrid()
    {
        try
        {
            string lLedgerId = string.Empty;
            string lLedgerNo = string.Empty;
            DataTable DataTable = new DataTable();
            ClsStockReport ObjclsStockReport = new ClsStockReport();

            var query = from ListItem item in ddlLedger.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                lLedgerId += item.Value + ',';
                lLedgerNo += item.Text + ',';
            }
            lLedgerId = lLedgerId.Substring(0, lLedgerId.Length - 1);
            lLedgerNo = lLedgerNo.Substring(0, lLedgerNo.Length - 1);


            if (lLedgerId == "0")
            {
                MessageBox1.ShowInfo("Please Select Ledger Name");
                return;
            }
            

            DataTable = ObjclsStockReport.BindLedgerWiseInvoiceReport(this.txtFromDate.Text,this.txtToDate.Text,lLedgerId);
            if (DataTable.Rows.Count > 0)
            {
                grdParent.DataSource = DataTable;
                grdParent.DataBind();
                Session["grdParent"] = DataTable;
            }
        }
        catch(Exception e)
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = (DataTable)Session["grdParent"];
        if (dt.Columns.Count > 1 )
        {
        }
        Session["grdParent"] = dt;
        Response.AddHeader("content-disposition", "inline; filename=" + "LedgerReport_" + this.txtFromDate.Text.Trim() + "_" + this.txtToDate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        ImageButton img = new ImageButton();
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        htmlWrite.Write("<table><tr><td colspan=10>Kkg Industry</td></tr>");
        htmlWrite.Write("<table><tr><td colspan=10><b>Ledger Report : " + ddlLedger.SelectedItem.Text.Trim() + "</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=104><b>PERIOD: " + txtFromDate.Text + " TO " + txtToDate.Text + " </b></td></tr>");
        
        grdParent.DataSource = dt;
        grdParent.DataBind();
        grdParent.Columns[0].Visible = false;
       

        for (int i = 0; i < grdParent.Rows.Count; i++)
        {
            
            GridView grdChild = (GridView)grdParent.Rows[i].Cells[10].FindControl("grdChild");
            grdChild.Columns[0].Visible = false;
            grdChild.Columns[1].Visible = false;
            
        }



        //grdParent.Columns[13].Visible = false;
        //grdParent.Columns[14].Visible = false;
        for (int i = 0; i < grdParent.HeaderRow.Cells.Count; i++)
        {
            grdParent.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
            grdParent.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");

        }
        grdParent.RowStyle.Font.Bold = true;
        grdParent.RowStyle.Font.Size = 10;
        grdParent.RowStyle.Height = 20;
        grdParent.RowStyle.Wrap = false;
        grdParent.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // return;
    }

}