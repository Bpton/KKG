#region Namespace
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPBLL;
using System.Web;
using BAL;

#endregion

public partial class FACTORY_frmRptPoStatusReport : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeaderWorkStationSource('" + GridView1.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlstate').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlspan').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlunit').multiselect({ includeSelectAllOption: true  });});</script>", false);

        if (!IsPostBack)
        {
            DateLock();

            this.pnlDisplay.Style["display"] = "";
             this.LoadTPU();
            this.LoadDepot();
        }
    }
    #endregion

    public void LoadTPU()
    {
        try
        {
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            this.ddlvendorname.Items.Clear();
            this.ddlvendorname.Items.Add(new ListItem("All", "0"));
            this.ddlvendorname.AppendDataBoundItems = true;
            this.ddlvendorname.DataSource = clsgrnmm.BindTPU("FG","", HttpContext.Current.Session["DEPOTID"].ToString());
            this.ddlvendorname.DataValueField = "VENDORID";
            this.ddlvendorname.DataTextField = "VENDORNAME";
            this.ddlvendorname.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    public void LoadDepot()
    {
    try
    {
        if (HttpContext.Current.Session["USERTYPE"].ToString() == "4933524B-D8F5-434A-A10E-6F721841F5D6" || HttpContext.Current.Session["USERTYPE"].ToString() == "3C3D62B1-3FD4-49E1-93CB-873614BA869D")
        {
                PPBLL.ClsStockReport clsrpt = new PPBLL.ClsStockReport();
                this.ddldepot.Items.Clear();
                this.ddldepot.Items.Add(new ListItem("All", "0"));
                this.ddldepot.AppendDataBoundItems = true;
                this.ddldepot.DataSource = clsrpt.BindBranch("A", "");
                this.ddldepot.DataValueField = "BRID";
                this.ddldepot.DataTextField = "BRPREFIX";
                this.ddldepot.DataBind();
            }
        else
            {
                PPBLL.ClsStockReport clsrpt = new PPBLL.ClsStockReport();
                this.ddldepot.Items.Clear();
                this.ddldepot.AppendDataBoundItems = true;
                this.ddldepot.DataSource = clsrpt.BindBranch("F", HttpContext.Current.Session["DEPOTID"].ToString());
                this.ddldepot.DataValueField = "BRID";
                this.ddldepot.DataTextField = "BRPREFIX";
                this.ddldepot.DataBind();
            }

        }
    catch (Exception ex)
    {
        string msg = ex.Message.Replace("'", "");
    }
    }

    #region BindGrid
    protected void BindGrid()
    {
        PPBLL.ClsStockReport clsrpt = new PPBLL.ClsStockReport();
        DataTable dtDisplayGrid = new DataTable();
        string FactoryId = HttpContext.Current.Session["DEPOTID"].ToString();
        dtDisplayGrid = clsrpt.BindPoStatus(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), this.ddlvendorname.SelectedValue.Trim(), this.ddldepot.SelectedValue.Trim());
                
        if (dtDisplayGrid.Rows.Count > 0)
        {
            this.GridView1.DataSource = dtDisplayGrid;
            this.GridView1.DataBind();
        }
        else
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
        }
    }
    #endregion

    #region GridView1_RowDataBound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    #endregion

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    } 
    #endregion

    #region VerifyRenderingInServerForm
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    #endregion

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Po_Status_Report_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls"));
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataTable dt = new DataTable();
            int gvColCount = 0;
            if (ViewState["DepotwiseSale"] != null)
            {
                gvColCount = (int)ViewState["DepotwiseSale"];

            }
            GridView1.RowStyle.Font.Size = 10;
            GridView1.RowStyle.Height = 20;
            hw.Write("<table><tr><td colspan=4><b>McNROE CONSUMERS PRODUCTS PRIVATE LIMITED</b></td></tr>");
            hw.Write("<table><tr><td colspan=4><b>PO STATUS REPORT</b></td></tr>");
            hw.Write("<table><tr><td colspan=4><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            ViewState["DepotwiseSale"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

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