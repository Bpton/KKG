
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
using System.Linq;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using Account;
#endregion


public partial class VIEW_frmRptVoucherReport : System.Web.UI.Page
{

    Clsstockreportnew clsrpt1 = new Clsstockreportnew();

    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GV_VOUCHER.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlregion').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlvouchertype').multiselect({ includeSelectAllOption: true  });});</script>", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InisiliseDate();

                /// this.txtfromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
                this.trpaymentmodelbl.Style["display"] = "none";
                this.trddlpaymentmode.Style["display"] = "none";
                this.td_lblmode.Visible = true;
                this.td_ddlmode.Visible = true;
                this.ddlMode.SelectedValue = "S";
                //this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.BindVoucherType();
                this.LoadDepot();

                string lastvalue = string.Empty;
                foreach (ListItem item in ddlregion.Items)
                {
                    if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        lastvalue = item.Value;
                    }
                }

                if (lastvalue == "-1" || lastvalue == "-2" || lastvalue == "-4" || lastvalue == "-3" || lastvalue == "-8")
                {
                    ddlregion.Items.Remove(ddlregion.Items.FindByValue(lastvalue));
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

    protected void BindVoucherType()
    {
        try
        {
            ClsStockReport clsVoucher = new ClsStockReport();
            this.ddlvouchertype.Items.Clear();
            //this.ddlvouchertype.Items.Add(new ListItem("Select Voucher Type", "0"));
            this.ddlvouchertype.AppendDataBoundItems = true;
            this.ddlvouchertype.DataSource = clsVoucher.VoucherType();
           // this.ddlvouchertype.DataSource = clsrpt1.VoucherType();
            this.ddlvouchertype.DataTextField = "VoucherName";
            this.ddlvouchertype.DataValueField = "Id";
            this.ddlvouchertype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void BindRegion()
    //{
    //    try
    //    {
    //        ClsStockReport clsVoucher = new ClsStockReport();
    //        DataTable dt = new DataTable();
    //        this.ddlregion.Items.Clear();
    //        this.ddlregion.AppendDataBoundItems = true;
    //        dt = clsVoucher.Region_foraccounts(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["USERID"]));
    //        this.ddlregion.DataSource = dt;
    //        this.ddlregion.DataTextField = "BRNAME";
    //        this.ddlregion.DataValueField = "BRID";
    //        this.ddlregion.DataBind();

    //        if (dt.Rows.Count ==1)
    //        {
    //            this.ddlregion.SelectedValue = Convert.ToString(dt.Rows[0]["BRID"]);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable depot = new DataTable();
            depot = clsrpt.Region_foraccounts(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["USERID"]));
           // depot = clsrpt1.Region_foraccounts(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["USERID"]));

            if (depot.Rows.Count > 0)
            {
                ddlregion.AppendDataBoundItems = true;
                ddlregion.DataSource = depot;
                ddlregion.DataTextField = "BRNAME";
                ddlregion.DataValueField = "BRID";
                ddlregion.DataBind();

            }
            else
            {
                ddlregion.Items.Clear();
                ddlregion.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindOrderGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region ddlvouchertype_SelectedIndexChanged
    protected void ddlvouchertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.VoucherTypeIndexChanged();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    } 
    #endregion

    #region VoucherTypeIndexChanged
    public void VoucherTypeIndexChanged()
    {
        try
        {
            // This is for Payment=9, Receipt=10, Advance Payment=15, Advance Receipt=16
            if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16")
            {
                this.trpaymentmodelbl.Style["display"] = "";
                this.trddlpaymentmode.Style["display"] = "";
                this.ddlMode.SelectedValue = "B";
            }
            else
            {
                this.trpaymentmodelbl.Style["display"] = "none";
                this.trddlpaymentmode.Style["display"] = "none";
                this.td_lblmode.Style["display"] = "";
                this.td_ddlmode.Style["display"] = "";
                this.ddlMode.SelectedValue = "S";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    } 
    #endregion

    public void BindOrderGrid()
    {
        string Mode = "";
        string Region = string.Empty;
        var Depot = from ListItem item in ddlregion.Items where item.Selected select item;
        foreach (ListItem item in Depot)
        {
            Region += item.Value + ",";
        }
        Region = Region.Substring(0, Region.Length - 1);

        string Voucher = string.Empty;
        var vouchertype = from ListItem item in ddlvouchertype.Items where item.Selected select item;
        foreach (ListItem item in vouchertype)
        {
            Voucher += item.Value + ',';
        }
        Voucher = Voucher.Substring(0, Voucher.Length - 1);

        if (ddlMode.SelectedValue.Trim() == "S")
        {
            Mode = "";
        }
        else
        {
            Mode = ddlMode.SelectedValue.Trim();
        }
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtVoucher = new DataTable();
        dtVoucher = clsrpt.BindAccVoucher(txtfromdate.Text.Trim(), txttodate.Text.Trim(), Voucher.Trim(), Mode, Region.Trim());
        //dtVoucher = clsrpt1.BindAccVoucher(txtfromdate.Text.Trim(), txttodate.Text.Trim(), Voucher.Trim(), Mode, Region.Trim());
        Session["LedgerReport_forexcel"] = dtVoucher;
        if (dtVoucher.Rows.Count > 0)
        {
            this.GV_VOUCHER.DataSource = dtVoucher;
            this.GV_VOUCHER.DataBind();
           
            GV_VOUCHER.Columns[12].ItemStyle.Width = 400;
            GV_VOUCHER.Columns[12].ItemStyle.Wrap = true;

        }
        else
        {
            this.GV_VOUCHER.DataSource = null;
            this.GV_VOUCHER.DataBind();
        }
    }

    protected void GV_VOUCHER_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ddlviewmode.SelectedValue == "B" || ddlviewmode.SelectedValue == "C")
            {
                ClsStockReport clsstock = new ClsStockReport();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    DataRowView drv = e.Row.DataItem as DataRowView;

                    Label lblAccEntryID = (Label)e.Row.FindControl("lblAccEntryID");
                    Label LedgerName = (Label)e.Row.FindControl("LedgerName");
                    Label Type = (Label)e.Row.FindControl("lbltype");
                    GridView GV_LEDGER = (GridView)e.Row.FindControl("GV_LEDGER");

                    decimal debittotal = 0;
                    decimal credittotal = 0;
                    string txtAccEntryID = lblAccEntryID.Text.Trim();
                    string VOUCHERTYPE = Type.Text.Trim();
                    DataTable dt = new DataTable();
                   dt = clsstock.BindAccVoucher_Ledger(txtAccEntryID);
                  //  dt = clsrpt1.BindAccVoucher_Ledger(txtAccEntryID);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt.Rows)
                        {
                            //string currentCellValue = row1["TxnType"].ToString();

                            //if (currentCellValue == "DR.")
                            //{
                            debittotal += (decimal)row1["DebitAmount"];  // For Specific Row
                            credittotal += (decimal)row1["CreditAmount"];  // For Specific Row
                            HttpContext.Current.Session["DebitTotalAmt"] = debittotal.ToString();
                            HttpContext.Current.Session["CreditTotalAmt"] = debittotal.ToString();
                            GV_LEDGER.DataSource = dt;
                            GV_LEDGER.DataBind();
                            //}
                        }
                    }
                    else
                    {
                        GV_LEDGER.DataSource = null;
                        GV_LEDGER.DataBind();
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    //e.Row.Cells[4].Text = "Total";
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GV_LEDGER_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ddlviewmode.SelectedValue == "C" || ddlviewmode.SelectedValue == "B")
            {
                ClsStockReport clsstock = new ClsStockReport();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label LblLedgerId = (Label)e.Row.FindControl("LblLedgerId");
                    Label LblentryId = (Label)e.Row.FindControl("lblEntryID");
                    GridView GV_INVOICE = (GridView)e.Row.FindControl("GV_INVOICE");
                    GridView GV_COSTCENTRE = (GridView)e.Row.FindControl("GV_COSTCENTRE");
                    string txtLedgerId = LblLedgerId.Text.Trim();
                    string AccEntryID = LblentryId.Text.Trim();
                    DataSet ds = new DataSet();
                    ds = clsstock.BindAccVoucher_Ledger_Voucher(txtLedgerId, AccEntryID);
                  //  ds = clsrpt1.BindAccVoucher_Ledger_Voucher(txtLedgerId, AccEntryID);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            decimal InvoiceAmt = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("InvoiceAmt"));
                            HttpContext.Current.Session["InvoiceAmt"] = InvoiceAmt.ToString();
                            decimal AmtReceived = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("AmtReceived"));
                            HttpContext.Current.Session["AmtReceived"] = AmtReceived.ToString();
                            decimal OutStanding = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("OutStanding"));
                            HttpContext.Current.Session["OutStanding"] = OutStanding.ToString();
                            GV_INVOICE.DataSource = ds.Tables[1];
                            GV_INVOICE.DataBind();
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GV_COSTCENTRE.DataSource = ds.Tables[0];
                            GV_COSTCENTRE.DataBind();
                        }
                    }
                    else
                    {
                        GV_INVOICE.DataSource = null;
                        GV_INVOICE.DataBind();

                        GV_COSTCENTRE.DataSource = null;
                        GV_COSTCENTRE.DataBind();
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[4].Text = "Total";
                    e.Row.Cells[4].ForeColor = Color.Black;
                    e.Row.Cells[4].Font.Name = "Arial";
                    e.Row.Cells[4].Height = 20;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].ForeColor = Color.Black;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].Text = HttpContext.Current.Session["DebitTotalAmt"].ToString().Trim();
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].ForeColor = Color.Black;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].Text = HttpContext.Current.Session["CreditTotalAmt"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region VerifyRenderingInServerForm
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    #endregion

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (ddlviewmode.SelectedValue == "A")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["LedgerReport_forexcel"];
            dt.Columns.RemoveAt(1);
            dt.AcceptChanges();
            dt.Columns.RemoveAt(1);
            dt.AcceptChanges();

            //dt.Columns.RemoveAt(3);
            //dt.AcceptChanges();

            Session["LedgerReport_forexcel"] = dt;

            Response.Clear();
            Response.AddHeader("content-disposition", "inline; filename=" + "VoucherReport_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            htmlWrite.Write("<table><tr><td colspan=10>K.K.G. INDUSTRIES (UNIT-III)</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=10><b>Ledger Report : " + ddlvouchertype.SelectedItem.Text.Trim() + "</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=104><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            //this.ClearControls(grdledger);
            gv_for_excel.Visible = true;
            gv_for_excel.DataSource = Session["LedgerReport_forexcel"];
            gv_for_excel.DataBind();
            gv_for_excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            Session["LedgerReport_forexcel"] = null;
            gv_for_excel.Visible = false;

            //dt.Clear();
        }
        else
        {
           // Response.Clear();
            Response.AddHeader("content-disposition", "inline; filename=" + "VoucherReport_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            ImageButton img=new ImageButton();            
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            htmlWrite.Write("<table><tr><td colspan=10>K.K.G. INDUSTRIES (UNIT-III)</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=10><b>Ledger Report : " + ddlvouchertype.SelectedItem.Text.Trim() + "</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=104><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            //GV_VOUCHER.RenderControl(
            GV_VOUCHER.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        
    }
    #endregion      
}