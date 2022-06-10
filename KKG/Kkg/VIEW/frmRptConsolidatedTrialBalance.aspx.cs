using BAL;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptConsolidatedTrialBalance : System.Web.UI.Page
{
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdledger.ClientID + "', 400, '100%' , 88 ,false); </script>", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InisiliseDate();
            this.pnlDisplay.Style["display"] = "";
            this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());

            Session["trialbalance"] = null;
        }

        foreach (ListItem item in ddlregion.Items)
        {
            if (item.Text == "---- OFFICE ----" || item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "Blue");
            }
        }
    }
    private void InisiliseDate()
    {
        /* New code for date on 02/04/2019 */
        //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********       //

        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;

        //       ******  END LOCK CAELENDER   ***********    //
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
    //#region DateChange
    //protected void txtfromdate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txtfromdate.Text);
    //    CalendarExtender3.StartDate = oDate1;
    //}
    //protected void txttodate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txttodate.Text);
    //    CalendarExtender2.EndDate = oDate1;
    //}
    //#endregion

    #region LoadRegion
    public void LoadRegion(string UserTypeid, string UserID)
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable depot = new DataTable();
            depot = clsrpt.Region_foraccounts(UserTypeid, UserID);

            if (depot.Rows.Count > 0)
            {
                this.ddlregion.AppendDataBoundItems = true;
                this.ddlregion.DataSource = depot;
                this.ddlregion.DataTextField = "BRNAME";
                this.ddlregion.DataValueField = "BRID";
                this.ddlregion.DataBind();
            }
            else
            {
                this.ddlregion.Items.Clear();
                this.ddlregion.Enabled = true;
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
        Session["CONSOLIDATED"] = null;

        dtDisplayGrid = clsrpt.BindConsolidateTrialbalance(txtfromdate.Text.Trim(), txttodate.Text.Trim(), Session["FINYEAR"].ToString().Trim());
        Session["CONSOLIDATED"] = dtDisplayGrid;

        if (dtDisplayGrid.Rows.Count > 0)
        {
            //DYNAMIC GRID

            grdledger.Columns.Clear();

            BoundField bfield24 = new BoundField();
            bfield24.HeaderText = "Stage";
            bfield24.DataField = dtDisplayGrid.Columns[24].ToString();
            grdledger.Columns.Add(bfield24);
            grdledger.Columns[0].Visible = false;

            BoundField bfield0 = new BoundField();
            bfield0.HeaderText = "Ledger Name (Group Wise)";
            bfield0.DataField = dtDisplayGrid.Columns[0].ToString();
            bfield0.HtmlEncode = false;
            grdledger.Columns.Add(bfield0);
            bfield0.ItemStyle.Width = Unit.Pixel(400);

            BoundField bfield23 = new BoundField();
            bfield23.HeaderText = "Ledger Name (Group Wise)";
            bfield23.DataField = dtDisplayGrid.Columns[23].ToString();
            grdledger.Columns.Add(bfield23);
            grdledger.Columns[2].Visible = false;


            for (int i = 4, j = 1; i < dtDisplayGrid.Columns.Count - 3; i = i + 2)
            {
                BoundField bfield1 = new BoundField();
                bfield1.HeaderText = "Debit";
                bfield1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                bfield1.DataField = dtDisplayGrid.Columns[i].ToString();
                grdledger.Columns.Add(bfield1);

                BoundField bfield5 = new BoundField();
                bfield5.HeaderText = "Credit";
                bfield1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                bfield5.DataField = dtDisplayGrid.Columns[i + 1].ToString();
                grdledger.Columns.Add(bfield5);

                j = j + 2;
            }
        }

        grdledger.DataSource = dtDisplayGrid;
        grdledger.DataBind();
    }

    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void grdledger_OnDataBound(object sender, EventArgs e)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        GridViewRow row1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        TableHeaderCell cell1 = new TableHeaderCell();

        DataTable dt = new DataTable();

        if (Session["CONSOLIDATED"] != null)
        {
            dt = (DataTable)Session["CONSOLIDATED"];

            int columncount = dt.Columns.Count;
            int headercount = dt.Columns.Count;

            cell.Text = "Consolidated Trial Balance";
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.ColumnSpan = 1;
            cell.Width = Unit.Pixel(500);
            row.Controls.Add(cell);

            for (int cellint = 0; cellint < 1; cellint++)
            {
                for (int i = 0; i < 1; i = i + 1)
                {
                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 6;
                    cell.Text = "Corporate Trial Balance";
                    cell.Width = Unit.Pixel(150);
                    row.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 6;
                    cell.Text = "All others Depot (Excluding Factory)";
                    cell.Width = Unit.Pixel(150);
                    row.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 6;
                    cell.Text = "Factory";
                    cell.Width = Unit.Pixel(150);
                    row.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 6;
                    cell.Text = "Total";
                    cell.Width = Unit.Pixel(150);
                    row.Controls.Add(cell);
                }
            }
            for (int cellint = 0; cellint < 1; cellint++)
            {
                cell = new TableHeaderCell();
                cell.ColumnSpan = 1;
                cell.Text = "";
                cell.Width = Unit.Pixel(150);
                row1.Controls.Add(cell);

                for (int i = 0; i < 4; i = i + 1)
                {
                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 2;
                    cell.Text = "Opening";
                    cell.Width = Unit.Pixel(150);
                    row1.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 2;
                    cell.Text = "During the Period";
                    cell.Width = Unit.Pixel(150);
                    row1.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.ColumnSpan = 2;
                    cell.Text = "Closing";
                    cell.Width = Unit.Pixel(150);
                    row1.Controls.Add(cell);
                }
            }
        }

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        row.BorderColor = Color.White;
        row.BorderWidth = 1;
        row.ForeColor = Color.Black;
        grdledger.HeaderRow.Parent.Controls.AddAt(0, row);

        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        row1.BorderColor = Color.White;
        row1.BorderWidth = 1;
        row1.ForeColor = Color.Black;
        grdledger.HeaderRow.Parent.Controls.AddAt(1, row1);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "Consolidated_TrialBalance" + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdledger.DataSource = Session["CONSOLIDATED"];
        grdledger.Columns[1].Visible = true;
        //grdledger.Columns[2].Visible = true;
        grdledger.DataBind();
        grdledger.RowStyle.Font.Bold = false;
        grdledger.RowStyle.Font.Size = 10;
        grdledger.RowStyle.Height = 20;
        grdledger.FooterStyle.Font.Bold = true;
        grdledger.FooterStyle.Font.Size = 10;
        grdledger.FooterStyle.Height = 20;
        htmlWrite.Write("<table><tr><td colspan=4><b>KKG Industries</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=4><b>CONSOLIDATED TRIAL BALANCE</b></td></tr>");
        grdledger.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }

    #region grdledger_RowDataBound
    protected void grdledger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
                {
                    e.Row.BackColor = Color.FromName("#e26b0a");
                    e.Row.Font.Bold = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 1)
                {
                    e.Row.BackColor = Color.FromName("#ffc000");
                    e.Row.Font.Bold = true;
                    //grdledger.CssClass = "trialbalancegrid";
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 2)
                {
                    e.Row.BackColor = Color.FromName("#86ab3c");
                    e.Row.Font.Bold = true;
                    //grdledger.CssClass = "trialbalancegrid";
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 3)
                {
                    e.Row.BackColor = Color.FromName("#76933c");
                    e.Row.Font.Bold = true;
                    //grdledger.CssClass = "trialbalancegrid";
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 4)
                {
                    e.Row.BackColor = Color.FromName("#6fa438");
                    e.Row.Font.Bold = true;
                    //grdledger.CssClass = "trialbalancegrid";
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -1)
                {
                    e.Row.BackColor = Color.FromName("#92d050");
                    //grdledger.CssClass = "trialbalancegrid";
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -2)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#c5ed84");
                    e.Row.Font.Bold = true;
                    //grdledger.CssClass = "trialbalancegrid";
                }
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
}