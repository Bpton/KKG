using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Drawing;
using Account;

public partial class VIEW_frmRptTrialBalance : System.Web.UI.Page
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

            //////this.txtfromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //////this.txttodate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  ********    //

            //////ClsStockReport clsstartfy = new ClsStockReport();
            //////string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

            //////DateTime oDate = Convert.ToDateTime(STARTDATE);

            //////ClsRptAccount clsVoucherEntry = new ClsRptAccount();
            //////string EndDate = clsVoucherEntry.GetEndDateOfFinYear(Session["FINYEAR"].ToString());
            //////DateTime cDate = Convert.ToDateTime(EndDate);

            //////CalendarExtender2.StartDate = oDate;
            //////CalendarExtender2.EndDate = cDate;

            //////CalendarExtender3.StartDate = oDate;
            //////CalendarExtender3.EndDate = cDate;

            /* Get Last date for non cuurent financial year : on 27/03/2019 */

            //////string LastDate = clsVoucherEntry.GetStartDateOfFinYear(Session["FINYEAR"].ToString());
            //////DateTime dtcurr = DateTime.Now;
            //////if ((LastDate) != "")
            //////{
            //////    dtcurr = Convert.ToDateTime(LastDate);
            //////}
            //////else
            //////{
            //////    this.CalendarExtender3.EndDate = DateTime.Now;
            //////    CalendarExtender2.EndDate = DateTime.Now;

            //////}

            //////string date = "dd/MM/yyyy";
            //////this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            //////this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');

            /* End */
            /*LOCK CALENDAR*/
            //////DateTime oDate1 = Convert.ToDateTime(txtfromdate.Text);
            //////CalendarExtender3.StartDate = oDate1;

            //////DateTime oDate2 = Convert.ToDateTime(txttodate.Text);
            //////CalendarExtender2.EndDate = oDate2;
            /****************************/

            this.pnlDisplay.Style["display"] = "";
            this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());
            //this.txtfromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //this.txttodate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Session["trialbalance"] = null;
            this.grdledger.DataSource = null;
            this.grdledger.DataBind();
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
        Session["trialbalance"] = null;

        dtDisplayGrid = clsrpt.BindTrialBalance(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddlregion.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim(), ddlType.SelectedValue.Trim());


        Session["trialbalance"] = dtDisplayGrid;
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

    //      DataTable dt = (DataTable)Session["trialbalance"];

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
        Response.AddHeader("content-disposition", "inline; filename=" + "TrialBalance_" + ddlregion.SelectedItem.Text.Trim() + "_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdledger_for_excel.Visible = true;
        htmlWrite.Write("<table><tr><td colspan=10><b>Trial Balance Report : " + ddlregion.SelectedItem.Text.Trim() + "</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=104><b>Period : " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        grdledger_for_excel.DataSource = Session["trialbalance"];
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

                Label lblgrpname = (Label)e.Row.FindControl("lblgrpname");
                LinkButton lnkgrpname = (LinkButton)e.Row.FindControl("lnkgrpname");

                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
                {
                    e.Row.BackColor = Color.FromName("#e26b0a");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 1)
                {
                    e.Row.BackColor = Color.FromName("#ffc000");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 2)
                {
                    e.Row.BackColor = Color.FromName("#86ab3c");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 3)
                {
                    e.Row.BackColor = Color.FromName("#76933c");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 4)
                {
                    e.Row.BackColor = Color.FromName("#6fa438");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -1)
                {
                    e.Row.BackColor = Color.FromName("#92d050");
                    lblgrpname.Visible = false;
                    lnkgrpname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -2)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#c5ed84");
                    e.Row.Font.Bold = true;
                    lblgrpname.Visible = true;
                    lnkgrpname.Visible = false;
                }

                if (rbdReport.SelectedValue == "All")
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = true;
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }

                if (rbdReport.SelectedValue == "Opening")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
                
                if (rbdReport.SelectedValue == "During")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }

                if (rbdReport.SelectedValue == "Closing")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (rbdReport.SelectedValue == "Opening")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;

                }

                if (rbdReport.SelectedValue == "During")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;

                }

                if (rbdReport.SelectedValue == "Closing")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;

                }

                if (rbdReport.SelectedValue == "All")
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = true;
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
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

    #region grdledgerexport_RowDataBound
    protected void grdledgerexport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblname = (Label)e.Row.FindControl("lblname");
              
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
                {
                    e.Row.BackColor = Color.FromName("#e26b0a");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 1)
                {
                    e.Row.BackColor = Color.FromName("#ffc000");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 2)
                {
                    e.Row.BackColor = Color.FromName("#86ab3c");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 3)
                {
                    e.Row.BackColor = Color.FromName("#76933c");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 4)
                {
                    e.Row.BackColor = Color.FromName("#6fa438");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -1)
                {
                    e.Row.BackColor = Color.FromName("#92d050");
                    lblname.Visible = true;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == -2)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#c5ed84");
                    e.Row.Font.Bold = true;
                    lblname.Visible = true;
                }
                
                if (rbdReport.SelectedValue == "All")
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = true;
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }

                if (rbdReport.SelectedValue == "Opening")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }

                if (rbdReport.SelectedValue == "During")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }

                if (rbdReport.SelectedValue == "Closing")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (rbdReport.SelectedValue == "Opening")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;

                }

                if (rbdReport.SelectedValue == "During")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;

                }

                if (rbdReport.SelectedValue == "Closing")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;

                }

                if (rbdReport.SelectedValue == "All")
                {
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = true;
                    e.Row.Cells[2].Visible = true;
                    e.Row.Cells[3].Visible = true;
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
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
    

    #region grdledger_RowCommand
    protected void grdledger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtledger = new DataTable();
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string LedgerName = commandArgs[0];
        dtledger = clsrpt.BindLedger_DepotLedgerNameWise(ddlregion.SelectedValue.ToString(), LedgerName);
        string Ledgerid = dtledger.Rows[0]["LEDGERID"].ToString();
        if (e.CommandName == "grpname")
        {
            string upath = "frmRptLedger_Reports.aspx?LedgerID=" + Ledgerid + "&&Fromdt=" + txtfromdate.Text + "&&Todt=" + txttodate.Text + "&&Region=" + ddlregion.SelectedValue.ToString() + "&&FromPage=TrialBalance";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
        }

       
        this.grdledger.DataSource = Session["trialbalance"];
        this.grdledger.DataBind();
    }
    #endregion
}