using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  });});</script>", false);
        if (!IsPostBack)
        {

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

            pnlDisplay.Style["display"] = "";
            LoadVendorName();
            DateTime today1 = DateTime.Now;
            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtender3.EndDate = today1;
                CalendarExtender2.EndDate = today1;

            }
            else
            {
                this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                CalendarExtender3.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
            }
            ViewState["Opening"] = "0.00";
            ViewState["Closing"] = "0.00";
        }
    }

    public void LoadVendorName()
    {
        try
        {
            ClsVendor_TPU clsreport = new ClsVendor_TPU();
            DataTable dt = clsreport.BindAllTpuForGateEntry(); //BindOnly_Depot();
            ddldepot.Items.Clear();
            ddldepot.AppendDataBoundItems = true;
            ddldepot.DataSource = dt;
            ddldepot.DataTextField = "VENDORNAME";
            ddldepot.DataValueField = "VENDORID";
            ddldepot.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #region BindGrid
    public void BindGrid()
    {
        try
        {
            ClsVendor_TPU clsrpt = new ClsVendor_TPU();
            DataTable dtDisplayGrid = new DataTable();
            string DepotID = string.Empty;
            string DepotName = "";

            var QueryDepot = from ListItem item in ddldepot.Items where item.Selected select item;
            foreach (ListItem item in QueryDepot)
            {
                // item ...
                DepotID += item.Value + ",";
                DepotName += item.Text + ',';
            }
            DepotID = DepotID.Substring(0, DepotID.Length - 1);
            DepotName = DepotName.Substring(0, DepotName.Length - 1);
            //divDetailsData.Visible = true;

            DivbtnExportDetails.Visible = true;


            DataTable dtDisplayGrid_details = new DataTable();
            dtDisplayGrid = clsrpt.VendorProductWisePoGrnQty(txtfromdate.Text.Trim(), txttodate.Text.Trim(), DepotID);
            if (dtDisplayGrid.Rows.Count > 0)
            {
                int INITIAL_VALUE = 8;
                int COUNT_COL = dtDisplayGrid.Columns.Count;
                for (INITIAL_VALUE = 5; INITIAL_VALUE < COUNT_COL; INITIAL_VALUE++)
                {
                    ViewState["ParentGrid_FooterTotal" + INITIAL_VALUE] = null;
                    ViewState["ParentGrid_FooterTotal" + INITIAL_VALUE] = 0;
                }
                ViewState["GridRowIndex"] = 0;
                this.gvRptProgress.DataSource = dtDisplayGrid;
                this.gvRptProgress.DataBind();
                Cache["Depotname"] = DepotName.Trim();

            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #endregion
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    //private void ExportGridToExcel()
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ClearContent();
    //    Response.ClearHeaders();
    //    Response.Charset = "";
    //    string FileName = "Vithal" + DateTime.Now + ".xls";
    //    StringWriter strwritter = new StringWriter();
    //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
    //    gvRptProgress.GridLines = GridLines.Both;
    //    gvRptProgress.HeaderStyle.Font.Bold = true;
    //    gvRptProgress.RenderControl(htmltextwrtter);
    //    Response.Write(strwritter.ToString());
    //    Response.End();

    //}

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //required to avoid the run time error "  
    //    //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    //}

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    ExportGridToExcel();
    //}
}