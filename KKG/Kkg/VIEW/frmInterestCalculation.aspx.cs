using BAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmInterestCalculation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                pnlDisplay.Style["display"] = "";

                Bindloanledger();
                DateLock();

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void Bindloanledger()
    {
        try
        {
            ClsStockReport ClsStockReport = new ClsStockReport();
            ddlLEDGER.Items.Clear();
            ddlLEDGER.Items.Insert(0, new ListItem("Select Ledger", "0"));
            ddlLEDGER.DataSource = ClsStockReport.Bindloanledger();
            ddlLEDGER.DataTextField = "NAME";
            ddlLEDGER.DataValueField = "ID";
            ddlLEDGER.DataBind();

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }








    protected void btnshow_click(object sender, EventArgs e)
    {


        string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string DEPOTID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
        try
        {
            DataTable dt = new DataTable();
            ClsStockReport clsStockReport = new ClsStockReport();
            dt = clsStockReport.BindInterastgrid(this.ddlLEDGER.SelectedValue, HttpContext.Current.Session["FINYEAR"].ToString().Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim(),txtdate.Text.Trim(), 
                Convert.ToDecimal(txtpercentage.Text.Trim()));
            if (dt.Rows.Count > 0)
            {
                grdinterest.DataSource = dt;
                grdinterest.DataBind();


            }
            else

            {

                grdinterest.DataSource = null;
                grdinterest.DataBind();


            }



        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }


    protected void Export_To_Excel(object sender, EventArgs e)
    {
        //Export the GridView to Excel  
        PrepareGridViewForExport(grdinterest);
        ExportGridView();
    }
    private void ExportGridView()
    {
        string attachment = "attachment; filename=Interest_calculation.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        htw.Write("<table><tr><td colspan=11><b>K.K.G. INDUSTRIES (UNIT-III)</b></td></tr>");
        htw.Write("<table><tr><td colspan=11><b>Ledger wise Interest Report : " + ddlLEDGER.SelectedItem.Text.Trim() + "</b></td></tr>");
        grdinterest.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void PrepareGridViewForExport(Control gv)
    {
        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(CheckBox))
            {
                l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            if (gv.Controls[i].HasControls())
            {
                PrepareGridViewForExport(gv.Controls[i]);
            }
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
        CalendarExtender4.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = today1;
        }
        else
        {
            this.txtdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender4.EndDate = cDate;
        }
    }
    #endregion
}




