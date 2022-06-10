using BAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptBalanceSheet_ProfitLoss : System.Web.UI.Page
{
    decimal gridtotal1 = 0;
    decimal gridtotal2 = 0;

    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdpl1.ClientID + "', 300, '100%' , 30 ,true); </script>", false);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>MakeStaticHeaderScheme('" + grdpl2.ClientID + "', 300, '100%' , 30 ,true); </script>", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InisiliseDate();

            this.pnlDisplay.Style["display"] = "";
            this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());

            Session["grd1"] = null;
            Session["grd2"] = null;

            this.grdpl1.DataSource = null;
            this.grdpl1.DataBind();

            this.grdpl2.DataSource = null;
            this.grdpl2.DataBind();

            string Type = Convert.ToString(Request.QueryString["TYPE"]).Trim();
            if (Type == "BL")
            {
                lbltype.Text = "Balance Sheet";
                lblgrd1.Text = "Assets";
                lblgrd2.Text = "Liabilities";
                //ddlreporttype.Visible = true;
                //lblreporttype.Visible = true;
            }
            else
            {
                lbltype.Text = "Profit & Loss";
                lblgrd1.Text = "Income";
                lblgrd2.Text = "Expence";
                //ddlreporttype.Visible = false;
                //lblreporttype.Visible = false;
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

        //       ******  END LOCK CAELENDER   ***********          //
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

    #region BindGrid
    protected void BindGrid(string Type)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet ds = new DataSet();
        Session["grd1"] = null;
        Session["totalexpence"] = null;
        Session["totalincome"] = null;
        if (Type == "BL")
        {
            ds = clsrpt.BindBalanceSheetV2(txttodate.Text.Trim(), ddlregion.SelectedValue.Trim(), Session["FINYEAR"].ToString(), "0", ddlreporttype.SelectedValue);
        }
        else
        {
            ds = clsrpt.BindProfitLossV2(txttodate.Text.Trim(), ddlregion.SelectedValue.Trim(), Session["FINYEAR"].ToString(), "0", ddlreporttype.SelectedValue);
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["totalexpence"] = ds.Tables[2].Rows[0]["TOTALEXPENSE"];
            Session["totalincome"] = ds.Tables[2].Rows[0]["TOTALINCOME"];
            Session["grd1"] = ds.Tables[0];
            ViewState["grd1"] = ds.Tables[0];
            DataTable dt = ds.Tables[0];
            DataTable dt2 = new DataTable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt2.Columns.Add(dt.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt2.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString().Replace("a", ""), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString());
            }
            this.grdpl1.DataSource = dt2;
            this.grdpl1.DataBind();

        }
        else
        {
            Session["grd2"] = null;
            this.grdpl1.DataSource = null;
            this.grdpl1.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            Session["grd1"] = ds.Tables[1];
            ViewState["grd2"] = ds.Tables[1];
            DataTable dt3 = ds.Tables[1];
            DataTable dt4 = new DataTable();
            for (int i = 0; i < dt3.Columns.Count; i++)
            {
                dt4.Columns.Add(dt3.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                dt4.Rows.Add(dt3.Rows[i][0].ToString(), dt3.Rows[i][1].ToString(), dt3.Rows[i][2].ToString(), dt3.Rows[i][3].ToString(), dt3.Rows[i][4].ToString(), dt3.Rows[i][5].ToString().Replace("a", ""), dt3.Rows[i][6].ToString(), dt3.Rows[i][7].ToString(), dt3.Rows[i][8].ToString());
            }

            this.grdpl2.DataSource = dt4;
            this.grdpl2.DataBind();
        }
        else
        {
            Session["grd2"] = null;
            this.grdpl2.DataSource = null;
            this.grdpl2.DataBind();
        }


        //if (ds.Tables[2].Rows.Count > 0)
        //{


        //}
        //else
        //{
        //    Session["totalexpence"] = "0";
        //    Session["totalincome"] = "0";

        //}
    }

    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindGrid(Request.QueryString["TYPE"].ToString());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }




    protected void PrepareForExport(GridView Gridview)
    {
        //Change the Header Row back to white color
        Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //Apply style to Individual Cells
        for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
        {
            Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
        }

        for (int i = 0; i < Gridview.Rows.Count; i++)
        {
            GridViewRow row = Gridview.Rows[i];

            //Change Color back to white
            row.BackColor = System.Drawing.Color.White;

            //Apply text style to each Row
            row.Attributes.Add("class", "textmode");

            //Apply style to Individual Cells of Alternating Row
            if (i % 2 != 0)
            {
                for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                {
                    row.Cells[j].Style.Add("background-color", "#C2D69B");
                }
            }
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlregion.SelectedValue != "0")
            {

                ClsStockReport clsrpt = new ClsStockReport();
                string BranchName = clsrpt.BindBranchName(this.ddlregion.SelectedValue);

                string Type = Convert.ToString(Request.QueryString["TYPE"]).Trim();
                string name = "";
                if (Type == "BL")
                {
                    name = "Balance Sheet a/c";
                }
                else
                {
                    name = "Profit & Loss a/c";
                }

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "inline; filename=" + "" + name + "_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                Table tb = new Table();
                TableRow tr1 = new TableRow();

                TableCell cell1 = new TableCell();
                cell1.Controls.Add(grdpl2);
                tr1.Cells.Add(cell1);

                TableCell cell3 = new TableCell();
                cell3.Controls.Add(grdpl1);


                TableCell cell2 = new TableCell();
                cell2.Text = "&nbsp;";

                int totalincomecount = grdpl1.Rows.Count + 1;
                int totalexpencecount = grdpl2.Rows.Count + 1;

                grdpl2.FooterRow.Visible = false;
                grdpl1.FooterRow.Visible = false;

                TableCell cell21 = new TableCell();
                TableCell cell22 = new TableCell();
                TableCell cell23 = new TableCell();

                hw.Write("<table><tr><td colspan=4><b>" + BranchName + "</b></td></tr>");
                hw.Write("<table><tr><td colspan=4><b>" + name + "</b></td></tr>");
                hw.Write("<table><tr><td colspan=4><b>AS ON DATE: " + txttodate.Text + "  </b></td></tr>");
                string totalexpenceAMT = string.Empty;
                if (grdpl1.Rows.Count > 0)
                {
                    totalexpenceAMT = Session["totalexpence"].ToString();
                }


                string totalincomeamt = string.Empty;
                if (grdpl2.Rows.Count > 0)
                {
                    totalincomeamt = Session["totalincome"].ToString();
                }

                if (totalincomecount > totalexpencecount)
                {
                    //cell22.Text = "Total Expense";
                    cell21.Text = totalincomeamt;
                }
                else
                {
                    //cell22.Text = "Total Income";
                    cell21.Text = totalexpenceAMT;
                }

                tr1.Cells.Add(cell2);
                tr1.Cells.Add(cell3);
                tb.Rows.Add(tr1);

                tb.RenderControl(hw);
                if (Type == "BL")
                {

                    hw.Write("<table border='1'><tr><td ><b>Total Liabilities  </b></td><td><b>" + totalexpenceAMT.Trim() + "</b></td><td></td><td><b>Total Assets   </b></td><td><b>" + totalincomeamt.Trim() + "</b></td></tr><table>");
                }
                else
                {
                    hw.Write("<table border='1'><tr><td ><b>Total Expenses   </b></td><td><b>" + totalexpenceAMT.Trim() + "</b></td><td></td><td><b>Total  Income  </b></td><td><b>" + totalincomeamt.Trim() + "</b></td></tr><table>");

                }
                //style to format numbers to string

                string style = @"<style> .textmode { mso-number-format:\@; } </style>";


                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region grdpl1_RowDataBound
    protected void grdpl1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltotal = (Label)e.Row.FindControl("lblamount1");

                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
                {
                    e.Row.BackColor = Color.FromName("#e26b0a");
                    e.Row.Font.Bold = true;
                    gridtotal1 += Convert.ToDecimal(lbltotal.Text.Trim());
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
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                string Type = Convert.ToString(Request.QueryString["TYPE"]).Trim();
                if (Type == "BL")
                {
                    e.Row.Cells[1].Text = "Total Assets";
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#91c5d4");
                    e.Row.Font.Bold = true;
                    e.Row.Cells[2].Text = Convert.ToString(Session["totalincome"]).Trim();
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].Width = new Unit("75.5%");
                    e.Row.Cells[2].Width = new Unit("24.5%");
                }
                else
                {
                    e.Row.Cells[1].Text = "Total Income";
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#91c5d4");
                    e.Row.Font.Bold = true;
                    e.Row.Cells[2].Text = Convert.ToString(Session["totalincome"]).Trim();
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].Width = new Unit("75.5%");
                    e.Row.Cells[2].Width = new Unit("24.5%");

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

    #region grdpl2_RowDataBound
    protected void grdpl2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltotal = (Label)e.Row.FindControl("lblamount2");
                Label lblgrpname = (Label)e.Row.FindControl("lblgrpname");
                LinkButton lnkgrpname = (LinkButton)e.Row.FindControl("lnkgrpname");

                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Stage")) == 0)
                {
                    e.Row.BackColor = Color.FromName("#e26b0a");
                    e.Row.Font.Bold = true;
                    gridtotal2 += Convert.ToDecimal(lbltotal.Text.Trim());
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
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                string Type = Convert.ToString(Request.QueryString["TYPE"]).Trim();
                if (Type == "BL")
                {
                    e.Row.Cells[1].Text = "Total Liabilities";
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#91c5d4");
                    e.Row.Font.Bold = true;
                    e.Row.Cells[2].Text = Convert.ToString(Session["totalexpence"]);
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].Width = new Unit("77.5%");
                    e.Row.Cells[2].Width = new Unit("22.5%");
                }
                else
                {
                    e.Row.Cells[1].Text = "Total Expenses";
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].ForeColor = Color.Black;
                    e.Row.BackColor = Color.FromName("#91c5d4");
                    e.Row.Font.Bold = true;
                    e.Row.Cells[2].Text = Convert.ToString(Session["totalexpence"]);
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].Width = new Unit("77.5%");
                    e.Row.Cells[2].Width = new Unit("22.5%");
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

    #region grdpl2_RowCommand
    protected void grdpl2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtledger = new DataTable();
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string LedgerName = commandArgs[0];
        dtledger = clsrpt.BindLedger_DepotLedgerNameWise(ddlregion.SelectedValue.ToString(), LedgerName);
        string Ledgerid = dtledger.Rows[0]["LEDGERID"].ToString();
        if (e.CommandName == "grpname")
        {
            string upath = "frmRptLedger_Reports.aspx?LedgerID=" + Ledgerid + "&&Asondt=" + txtfromdate.Text + "&&Region=" + ddlregion.SelectedValue.ToString() + "&&FromPage=ProfitLoss";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
        }
    }
    #endregion
}