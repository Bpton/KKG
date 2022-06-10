﻿using BAL;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptGrpSummaryTrialBalance : System.Web.UI.Page
{
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdledger.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            InisiliseDate();
            this.pnlDisplay.Style["display"] = "";
            this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());
            this.LoadAccGroup();
            //this.txtfromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //this.txttodate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Session["grpsummary_trialbalance"] = null;
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
    #region DateChange
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
    #endregion
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

        //       ******  END LOCK CAELENDER   ***********     //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;

        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }

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

    #region LoadAccGroup
    public void LoadAccGroup()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable dt = new DataTable();
            dt = clsrpt.BindAccountsGroup();

            if (dt.Rows.Count > 0)
            {
                this.ddlgroup.AppendDataBoundItems = true;
                this.ddlgroup.DataSource = dt;
                this.ddlgroup.DataTextField = "grpName";
                this.ddlgroup.DataValueField = "Code";
                this.ddlgroup.DataBind();
            }
            else
            {
                this.ddlgroup.Items.Clear();
                this.ddlgroup.Enabled = true;
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
        Session["grpsummary_trialbalance"] = null;

        dtDisplayGrid = clsrpt.BindGrpSummaryTrialbalance(txtfromdate.Text.Trim(), txttodate.Text.Trim(), Session["FINYEAR"].ToString().Trim(), ddlregion.SelectedValue.Trim(), ddlgroup.SelectedValue.Trim(), ddlgroup.SelectedItem.Text.Trim());


        Session["grpsummary_trialbalance"] = dtDisplayGrid;
        this.grdledger.DataSource = dtDisplayGrid;
        this.grdledger.DataBind();

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



    //protected void btnExport_Click(object sender, EventArgs e)
    //{

    //      DataTable dt = (DataTable)Session["grpsummary_trialbalance"];

    //      ExportToExcel(dt);

    //}
    //private void ExportToExcel(DataTable dtExcel)
    //{
    //    try
    //    {
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
    //        Response.ContentType = "application/excel";
    //        System.IO.StringWriter sw = new System.IO.StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        grdledger.RenderControl(htw);
    //        Response.Write(sw.ToString());
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "GroupSummary_Report" + ddlregion.SelectedItem.Text.Trim() + "_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdledger_for_excel.Visible = true;
        htmlWrite.Write("<table><tr><td colspan=10><b> " + ddlgroup.SelectedItem.Text.Trim() + " : " + ddlregion.SelectedItem.Text.Trim() + "</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=104><b>Period : " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        grdledger_for_excel.DataSource = Session["grpsummary_trialbalance"];
        grdledger_for_excel.DataBind();

        grdledger_for_excel.HeaderRow.Cells[0].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[1].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[2].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[3].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[4].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[5].Style.Add("text-decoration", "Underline");
        grdledger_for_excel.HeaderRow.Cells[6].Style.Add("text-decoration", "Underline");


        grdledger_for_excel.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();
        Session["itemledger"] = null;
        grdledger_for_excel.Visible = false;

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
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 2)
                {
                    e.Row.BackColor = Color.FromName("#86ab3c");
                    e.Row.Font.Bold = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 3)
                {
                    e.Row.BackColor = Color.FromName("#76933c");
                    e.Row.Font.Bold = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 4)
                {
                    e.Row.BackColor = Color.FromName("#6fa438");
                    e.Row.Font.Bold = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -1)
                {
                    e.Row.BackColor = Color.FromName("#92d050");
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -2)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#c5ed84");
                    e.Row.Font.Bold = true;
                }
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                e.Row.Attributes["onmouseover"] = "this.style.color='Black';this.style.cursor='hand';";
                e.Row.Attributes["onmouseout"] = "this.style.color='Black';";
                e.Row.Attributes["onclick"] = "addTab('Ledger Report','frmRptLedger_Reports.aspx?frmdate=" + txtfromdate.Text + "&todate=" + txttodate.Text + "&region=" + ddlregion.SelectedValue.Trim() + "&ledgerid=" + rowView["LedgerID"].ToString() + "')";

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