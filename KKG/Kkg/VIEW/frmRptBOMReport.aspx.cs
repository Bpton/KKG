#region Namespace
using BAL;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion
public partial class VIEW_frmRptBOMReport : System.Web.UI.Page
{
    #region Page_Init
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlbrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlframework').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlProcess').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlBrand').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlstorelocation').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlproduct').multiselect({ includeSelectAllOption: true  });});</script>", false);

    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlDisplay.Style["display"] = "";
            LoadFactory();
            Session["itemledger"] = null;
            this.LoadBrand();
        }
    }
    #endregion

    public void LoadBrand()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlBrand.Items.Clear();
        //ddlBrand.Items.Insert(0, new ListItem("SELECT ITEM", "0"));
        ddlBrand.AppendDataBoundItems = true;
        ddlBrand.DataSource = ClsCommon.BindFactoryTypewise_Item();
        ddlBrand.DataTextField = "NAME";
        ddlBrand.DataValueField = "ID";
        ddlBrand.DataBind();
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBrand.SelectedValue != "")
            {
                //DataTable dtframework = new DataTable();
                DataSet dtframework = new DataSet();
              
                ClsFactoryReport clsopenstock = new ClsFactoryReport();

                string brandid = string.Empty;
                var query1 = from ListItem item in ddlBrand.Items where item.Selected select item;
                int id = 1;
                foreach (ListItem item in query1)
                {
                    // item ...
                    brandid += item.Value + ',';
                }

                ddlframework.Items.Clear();
                ddlframework.AppendDataBoundItems = true;
                dtframework = clsopenstock.BindFactoryTypewise_Item_Framework1(brandid);
                ddlframework.DataSource = dtframework.Tables[0];
                ddlframework.DataTextField = "ProcessFrameworkName";
                ddlframework.DataValueField = "PROCESSFRAMEWORKID";
                ddlframework.DataBind();

                ddlProcess.Items.Clear();
               // ddlProcess.Items.Insert(0, new ListItem("SELECT ALL", "0"));
                ddlProcess.AppendDataBoundItems = true;
                ddlProcess.DataSource = dtframework.Tables[1];
                ddlProcess.DataTextField = "ProcessName";
                ddlProcess.DataValueField = "ProcessID";
                ddlProcess.DataBind();


                //if (ddlProcess.SelectedItem == null)
                //{
                //    ddlProcess.Items.RemoveAt(ddlProcess.SelectedIndex);
                //}

              
            }
            else
            {
                ddlframework.Items.Clear();
                ddlframework.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadFactory
    public void LoadFactory()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            DataTable dt = new DataTable();
            this.ddldepot.Items.Clear();
            dt = ClsCommon.BindFactory(Session["UserID"].ToString());
            this.ddldepot.DataSource = dt;
            this.ddldepot.DataValueField = "VENDORID";
            this.ddldepot.DataTextField = "VENDORNAME";
            this.ddldepot.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion    

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    #region BindGrid
    public void BindGrid()
    {
        ClsFactoryReport clsrpt = new ClsFactoryReport();
        DataTable dtBOM = new DataTable();



        string Frameworkid = string.Empty;
        var query1 = from ListItem item in ddlframework.Items where item.Selected select item;
        int id = 1;
        foreach (ListItem item in query1)
        {
            // item ...
            Frameworkid += item.Value + ',';
        }


        string processid = string.Empty;
        var process1 = from ListItem item in ddlProcess.Items where item.Selected select item;
        foreach (ListItem item in process1)
        {
            // item ...
            processid += item.Value + ',';
        }

        #region comment


        //foreach (ListItem item in query1)
        //{
        // item ...
        // Frameworkid = item.Value;

        //< HeaderStyle BackColor = "Silver" Font - Bold = "True" ForeColor = "#3399FF" />

        //GridView GridView1 = new GridView();
        //GridView1.ID = "GridView1" + id;
        //GridView1.ShowFooter = true;
        //  dtBOM = clsrpt.BindFactoryTypewise_Item_Framework_BOM_Details(Frameworkid.Trim(), ddlProcess.SelectedValue.ToString(),this.ddldepot.SelectedValue.ToString());
        #endregion

        this.grdBomReport.DataSource = clsrpt.BindFactoryTypewise_Item_Framework_BOM_Details(Frameworkid.Trim(), processid.Trim(), this.ddldepot.SelectedValue.ToString());
        this.grdBomReport.DataBind();

        #region comment
        //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        //GridView1.DataSource = dtBOM;
        //GridView1.DataBind();

        //Session["GridView" + id] = dtBOM;

        //for (int i = 0; i < dtBOM.Rows.Count; i++)
        //{
        //    GridView1.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Right;
        //    GridView1.Rows[i].Cells[5].HorizontalAlign = HorizontalAlign.Right;
        //}
        //PanelGridView.Controls.Add(GridView1);
        //id = id + 1;

        //PanelGridView.Controls.Add(new LiteralControl("<br />"));
        // }
        //Frameworkid = Frameworkid.Substring(0, Frameworkid.Length - 1);
        #endregion
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
        #region comment
        //var query1 = from ListItem item in ddlframework.Items where item.Selected select item;
        //int id = 1;
        //DataTable dt = new DataTable();
        //foreach (ListItem item in query1)
        //{
        //    dt.Merge((DataTable)Session["GridView" + id]);
        //    //dt.Rows.Add("BOM");
        //    //dt.Rows.Add("");
        //    dt.AcceptChanges();
        //    DataRow dr1 = dt.NewRow();
        //    DataRow dr2 = dt.NewRow();
        //    DataRow dr3 = dt.NewRow();
        //    DataRow dr4 = dt.NewRow();
        //    dr1[1] = "";
        //    dr2[1] = "";
        //    dr3[1] = "";
        //    dr4[1] = "";
        //    dt.Rows.Add(dr1);
        //    dt.Rows.Add(dr2);
        //    dt.Rows.Add(dr3);
        //    dt.Rows.Add(dr4);
        //    dt.AcceptChanges();
        //    id = id + 1;
        //}
        //GridView gridview2 = new GridView();
        //gridview2.DataSource = dt;
        //gridview2.DataBind();
        //int col_count = dt.Columns.Count;
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "inline; filename=" + "BOM Report.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        ////hw.Write("<table><tr><td colspan=" + col_count + ">DEPOT: " + ddldepot.SelectedItem.Text + "</td></tr>");
        //hw.Write("<table><tr><td colspan=" + col_count + ">BOM REPORT</td></tr>");
        //hw.Write("<table><tr><td colspan=" + col_count + ">ITEM: " + ddlframework.SelectedItem.Text.Trim() + "</td></tr>");
        ////hw.Write("<table><tr><td colspan=" + col_count + ">PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </td></tr>");
        //gridview2.RenderControl(hw);
        //Response.Output.Write(sw.ToString());
        //Response.Flush();
        //Response.End();
        //gridview2.Visible = false;
        //string Frameworkid = string.Empty;
        //var query1 = from ListItem item in ddlframework.Items where item.Selected select item;
        //int id = 1;
        //foreach (ListItem item in query1)
        //{
        //    dt.Merge((DataTable)Session["GridView" + id]);
        //    //dt.Rows.Add("BOM");
        //    //dt.Rows.Add("");
        //    dt.AcceptChanges();
        //    DataRow dr1 = dt.NewRow();
        //    DataRow dr2 = dt.NewRow();
        //    DataRow dr3 = dt.NewRow();
        //    DataRow dr4 = dt.NewRow();
        //    dr1[1] = "";
        //    dr2[1] = "";
        //    dr3[1] = "";
        //    dr4[1] = "";
        //    dt.Rows.Add(dr1);
        //    dt.Rows.Add(dr2);
        //    dt.Rows.Add(dr3);
        //    dt.Rows.Add(dr4);
        //    dt.AcceptChanges();
        //    id = id + 1;
        //}
        //GridView gridview2 = new GridView();
        //gridview2.DataSource = dt;
        //gridview2.DataBind();
        //int col_count = dt.Columns.Count;
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "inline; filename=" + "BOM Report.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        ////hw.Write("<table><tr><td colspan=" + col_count + ">DEPOT: " + ddldepot.SelectedItem.Text + "</td></tr>");
        //hw.Write("<table><tr><td colspan=" + col_count + ">BOM REPORT</td></tr>");
        //hw.Write("<table><tr><td colspan=" + col_count + ">ITEM: " + ddlframework.SelectedItem.Text.Trim() + "</td></tr>");
        ////hw.Write("<table><tr><td colspan=" + col_count + ">PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </td></tr>");
        //gridview2.RenderControl(hw);
        //Response.Output.Write(sw.ToString());
        //Response.Flush();
        //Response.End();
        //gridview2.Visible = false;
        #endregion

        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "BomReport" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdBomReport.GridLines = GridLines.Both;
        grdBomReport.HeaderStyle.Font.Bold = true;
        grdBomReport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    #endregion

    public static void ExportAv(string fileName, GridView[] gvs)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        foreach (GridView gv in gvs)
        {
            gv.AllowPaging = false;

            //   Create a form to contain the grid
            Table table = new Table();
            table.GridLines = gv.GridLines;
            //   add the header row to the table
            if (!(gv.Caption == null))
            {
                TableCell cell = new TableCell();
                cell.Text = gv.Caption;
                cell.ColumnSpan = 10;
                TableRow tr = new TableRow();
                tr.Controls.Add(cell);
                table.Rows.Add(tr);
            }
            if (!(gv.HeaderRow == null))
            {
                table.Rows.Add(gv.HeaderRow);
            }
            //   add each of the data rows to the table
            foreach (GridViewRow row in gv.Rows)
            {
                table.Rows.Add(row);
            }
            //   add the footer row to the table
            if (!(gv.FooterRow == null))
            {
                table.Rows.Add(gv.FooterRow);
            }
            //   render the table into the htmlwriter
            table.RenderControl(htw);
        }
        //   render the htmlwriter into the response

        string headerTable = @"<table width='100%' class='TestCssStyle'><tr><td><h4>Report </h4> </td><td></td><td><h4>" + DateTime.Now.ToString("d") + "</h4></td></tr></table>";
        HttpContext.Current.Response.Write(headerTable);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
}