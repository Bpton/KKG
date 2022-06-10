using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRrtPoStatus : System.Web.UI.Page
{
    #region Page_Init
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key3", "<script>MakeStaticHeader('" + gvDepotwisePurchase.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });});</script>", false);
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      HPD       //

            ClsStockReport clsstartfy = new ClsStockReport();
            string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

            DateTime oDate = Convert.ToDateTime(STARTDATE);

            DateTime cDate = oDate.AddSeconds(31535999);

            CalendarExtender2.StartDate = oDate;
            CalendarExtender3.EndDate = cDate;

            //            ******  END LOCK CAELENDER   ***********      HPD    //

            pnlDisplay.Style["display"] = "";
            this.LoadDepotName();
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
        {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtDisplayGrid = new DataTable();
        string Depot = "";
        string DepotName = "";
        var query = from ListItem item in ddldepot.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            Depot += item.Value + ',';
            DepotName += item.Text + ',';
        }
        Depot = Depot.Substring(0, Depot.Length - 1);
        DepotName = DepotName.Substring(0, DepotName.Length - 1);
        dtDisplayGrid = clsrpt.BindPoStatus(this.txtfromdate.Text,this.txttodate.Text, Session["FINYEAR"].ToString(), Depot);
        Session["ds1"] = dtDisplayGrid;

        if (dtDisplayGrid.Rows.Count > 0)
        {
            this.gvDepotwisePurchase.DataSource = dtDisplayGrid;
            this.gvDepotwisePurchase.DataBind();
        }
        else
        {
            this.gvDepotwisePurchase.DataSource = null;
            this.gvDepotwisePurchase.DataBind();
        }

    }
    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = clsreport.BindDepot_Primary();
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
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
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accountsforfactory(HttpContext.Current.Session["IUSERID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
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
    #endregion
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Po Status.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvDepotwisePurchase.HeaderStyle.ForeColor = Color.Black;
            gvDepotwisePurchase.RowStyle.Font.Bold = false;
            gvDepotwisePurchase.RowStyle.Font.Size = 10;
            gvDepotwisePurchase.RowStyle.Height = 20;
            gvDepotwisePurchase.FooterStyle.Font.Bold = true;
            gvDepotwisePurchase.FooterStyle.Font.Size = 10;
            gvDepotwisePurchase.FooterStyle.Height = 20;
            hw.Write("<table><tr><td colspan='3'>CategoryWise Purchase Report</ td></tr>");
            hw.Write("<table><tr><td colspan='3'>MONTH: " + ddldepot.SelectedItem.Text + "</td></tr>");
            gvDepotwisePurchase.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void gvDepotwisePurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPOSTATUS = (Label)e.Row.FindControl("lblPOSTATUS");
                Label lblPENDINGFOR = (Label)e.Row.FindControl("lblPENDINGFOR");
                string postatus = lblPOSTATUS.Text.Trim().ToUpper();
                string pendingfor = lblPENDINGFOR.Text.Trim().ToUpper();


                if (postatus == "APPROVED")
                {
                    lblPOSTATUS.ForeColor = Color.Green;
                }
                else if (postatus == "REJECTED")
                {
                    lblPOSTATUS.ForeColor = Color.Red;
                }
                else if (postatus == "HOLD")
                {
                    lblPOSTATUS.ForeColor = Color.Blue;
                }
                else if (postatus == "CONFIRM")
                {
                    lblPOSTATUS.ForeColor = Color.Yellow;
                }
                else if (postatus == "WAIT FOR APPROVED")
                {
                    lblPOSTATUS.ForeColor = Color.YellowGreen;
                }
                else if (postatus == "PENDING")
                {
                    lblPOSTATUS.ForeColor = Color.Black;
                }
                if(pendingfor== "ALL DONE")
                {
                    lblPENDINGFOR.ForeColor = Color.Green;
                }
                else
                {
                    lblPENDINGFOR.ForeColor = Color.DeepPink;
                }
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}