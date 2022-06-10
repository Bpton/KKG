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

public partial class VIEW_frmrptproductlist : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeaderWorkStationSource('" + GridView1.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlstate').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlspan').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlunit').multiselect({ includeSelectAllOption: true  });});</script>", false);

        if (!IsPostBack)
        {
           
            //BindGrid();
            this.pnlDisplay.Style["display"] = "";
            
        }
    }
    #endregion

    #region BindGrid
    protected void BindGrid()
    {
        ClsProductMaster ClsProduct = new ClsProductMaster();
        DataTable dtBindGrid = new DataTable();
        dtBindGrid = ClsProduct.BindProductList(this.ddltype.SelectedValue.Trim());
                
        if (dtBindGrid.Rows.Count > 0)
        {
            this.GridView1.DataSource = dtBindGrid;
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
           Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ProductList_"  + ".xls"));
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataTable dt = new DataTable();
            int gvColCount = 0;
            if (ViewState["ProductList"] != null)
            {
                gvColCount = (int)ViewState["ProductList"];

            }
            GridView1.RowStyle.Font.Size = 10;
            GridView1.RowStyle.Height = 20;
            hw.Write("<table><tr><td colspan=4><b>KKG INDUSTRY</b></td></tr>");
            hw.Write("<table><tr><td colspan=4><b>Product List</b></td></tr>");
           // hw.Write("<table><tr><td colspan=4><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            ViewState["ProductList"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

}