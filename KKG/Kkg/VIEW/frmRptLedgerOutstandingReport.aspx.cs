using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Obout.Grid;
using System.Web;
using Account;
public partial class VIEW_frmRptLedgerOutstandingReport : System.Web.UI.Page
{

    #region Page_Init
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdledger.ClientID + "', 400, '100%' , 25 ,true); </script>", false);
      
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            InisiliseDate();

            this.LoadDepot();
            //this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["GridRowIndex"] = null;
        }
    }
    #endregion
    private void InisiliseDate()
    {
        /* New code for date on 02/04/2019 */
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
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;

        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }
   
    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            try
            {
                ClsStockReport clsreport = new ClsStockReport();
                if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
                {
                    DataTable dt = clsreport.BindDepot_Primary();
                    ddldepot.Items.Clear();
                    ddldepot.AppendDataBoundItems = true;
                    ddldepot.Items.Add(new ListItem("Select", "0"));
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
                    ddldepot.Items.Add(new ListItem("Select", "0"));
                    ddldepot.DataSource = dt;
                    ddldepot.DataTextField = "BRNAME";
                    ddldepot.DataValueField = "BRID";
                    ddldepot.DataBind();
                }
                else
                {
                    DataTable dt = clsreport.BindDepot_userwise(HttpContext.Current.Session["IUSERID"].ToString());
                    ddldepot.Items.Clear();
                    ddldepot.AppendDataBoundItems = true;
                    ddldepot.Items.Add(new ListItem("Select", "0"));
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
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           

            ClsClaim clmclaim = new ClsClaim();
            DataTable dt = clmclaim.BindDistributorClaim(ddldepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                ddlCustomer.Items.Clear();
                ddlCustomer.AppendDataBoundItems = true;
                ddlCustomer.Items.Add(new ListItem("Select", "0"));
                ddlCustomer.DataSource = dt;
                ddlCustomer.DataValueField = "CUSTOMERID";
                ddlCustomer.DataTextField = "CUSTOMERNAME";
                ddlCustomer.DataBind();
            }
            else
            {
                ddlCustomer.Items.Clear();

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion



    #region BindGrid
    protected void BindGrid()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtDisplayGrid = new DataTable();
       
        dtDisplayGrid = clsrpt.LedgerOutstandiing(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddlCustomer.SelectedValue.Trim());

        if (dtDisplayGrid.Rows.Count > 0)
        {


            decimal sum = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            ViewState["InvoiceAmt"] = null;
            ViewState["AlreadyAmtPaid"] = null;
            ViewState["RemainingAmt"] = null;
            foreach (DataRow row in dtDisplayGrid.Rows)
            {
                sum += Convert.ToDecimal(row["InvoiceAmt"].ToString().Trim());
                sum1 += Convert.ToDecimal(row["AlreadyAmtPaid"].ToString().Trim());
                sum2 += Convert.ToDecimal(row["RemainingAmt"].ToString().Trim());
            }
            ViewState["InvoiceAmt"] = Convert.ToString(sum).Trim();
            ViewState["AlreadyAmtPaid"] = Convert.ToString(sum1).Trim();
            ViewState["RemainingAmt"] = Convert.ToString(sum2).Trim();


            grdledger.DataSource = dtDisplayGrid;
            grdledger.DataBind();
            ViewState["LedgerReport_forexcel"] = dtDisplayGrid;

        }
        else
        {
            grdledger.DataSource = null;
            grdledger.DataBind();
        }
    }
    #endregion

    #region btnShow_Click
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion


    #region maingrid_OnRowDataBound
    protected void grdledger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();
                ClsStockReport clsstock = new ClsStockReport();
                Label lblAccEntryID = (Label)e.Row.FindControl("lblAcc");
                string txtAccEntryID = lblAccEntryID.Text.Trim();
                if (txtAccEntryID != "")
                {
                    dt = clsstock.LedgerOutstandiing_Child(txtAccEntryID);
                    GridView GV_LEDGER = (GridView)e.Row.FindControl("GV_LEDGER");
                    if (dt.Rows.Count > 0)
                    {
                        GV_LEDGER.DataSource = dt;
                        GV_LEDGER.DataBind();
                    }
                    else
                    {
                        GV_LEDGER.DataSource = null;
                        GV_LEDGER.DataBind();
                    }

                }
            }



            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;

                e.Row.Cells[5].Text = ViewState["InvoiceAmt"].ToString().Trim();
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[6].Text = ViewState["AlreadyAmtPaid"].ToString().Trim();
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[7].Text = ViewState["RemainingAmt"].ToString().Trim();
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
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
        // return;
    }

    #region Export To Excel
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = (DataTable)Session["LedgerReport_forexcel"];

        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "LedgerOutstanding_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        htmlWrite.Write("<table><tr><td colspan=10><b>McNROE CONSUMERS PRODUCTS PRIVATE LIMITED</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=10><b>Outstanding Report : " + ddldepot.SelectedItem.Text.Trim() + "</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=104><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        
        grdledger.Visible = true;
        grdledger.DataSource = dt;
        grdledger.DataBind();

       
        grdledger.RowStyle.Font.Bold = true;
        grdledger.RowStyle.Font.Size = 10;
        grdledger.RowStyle.Height = 20;
        grdledger.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }

    private void ClearControls(Control control)
    {
        try
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }
            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else
                    if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
        }
        catch (Exception e)
        {

        }
        return;
    }
    #endregion
}


