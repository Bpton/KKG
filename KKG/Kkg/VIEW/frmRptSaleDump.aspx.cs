﻿#region Namespace
using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion
public partial class VIEW_frmRptSaleDump : System.Web.UI.Page
{
    ClsFactoryReport clsrpt = new ClsFactoryReport();
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvsaledump.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.DateLock();                
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }    

    #region BindGrid
    protected void BindGrid()
    {
        DataTable dtDisplayGrid = new DataTable();
        dtDisplayGrid = clsrpt.Bind_SaleDump(txtfromdate.Text.Trim(), txttodate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());

        if (dtDisplayGrid.Rows.Count > 0)
        {
            this.gvsaledump.DataSource = dtDisplayGrid;
            this.gvsaledump.DataBind();
            Cache["SALEDTLSREPORT"] = dtDisplayGrid;
        }
        else
        {
            this.gvsaledump.DataSource = null;
            this.gvsaledump.DataBind();
        }
    }
    #endregion    

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Cache["SALEDTLSREPORT"];
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "SaleDump_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";

        using (StringWriter stringWrite = new StringWriter())
        {
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //To Export all pages
            gvsaledump.AllowPaging = false;

            gvsaledump.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvsaledump.HeaderRow.Cells)
            {
                cell.BackColor = gvsaledump.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvsaledump.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvsaledump.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvsaledump.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                    List<Control> controls = new List<Control>();
                    foreach (Control control in cell.Controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "HyperLink":
                                controls.Add(control);
                                break;
                            case "TextBox":
                                controls.Add(control);
                                break;
                            case "LinkButton":
                                controls.Add(control);
                                break;
                            case "CheckBox":
                                controls.Add(control);
                                break;
                            case "RadioButton":
                                controls.Add(control);
                                break;
                        }
                    }
                    foreach (Control control in controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "HyperLink":
                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                break;
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                break;
                            case "LinkButton":
                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                break;
                            case "CheckBox":
                                cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                break;
                            case "RadioButton":
                                cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            htmlWrite.Write("<table><tr><td colspan=3<b>" + Session["Depotname"].ToString() + "</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=3<b>Sale Dump</b></td></tr>");
            htmlWrite.Write("<table><tr><td colspan=3><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            for (int i = 0; i < gvsaledump.HeaderRow.Cells.Count; i++)
            {
                gvsaledump.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
                gvsaledump.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");
            }
            gvsaledump.RowStyle.Font.Size = 8;
            gvsaledump.RowStyle.Height = 20;
            gvsaledump.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            gvsaledump.Visible = false;
        }
    }

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;
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
    }
    #endregion
}