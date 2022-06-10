using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmRptItemLedger_Factory : System.Web.UI.Page
{
    #region Page_Init
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvItemLedger.ClientID + "', 400, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                pnlDisplay.Style["display"] = "";
                LoadDepotName();
                LoadStorelocation();
                this.LoadBrand();
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Session["INWARD"] = null;
                Session["OUTWARD"] = null;
                Session["itemledger"] = null;
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("ALL", "0"));
                this.DateLock();
                LoadSubItem();
                Loadproduct();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
    }
    #endregion

    public void LoadBrand()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlBrand.Items.Clear();
        ddlBrand.Items.Insert(0, new ListItem("ALL ", "0"));
        ddlBrand.AppendDataBoundItems = true;
        ddlBrand.DataSource = ClsCommon.BindBrand_SupliedItem();
        ddlBrand.DataTextField = "DIVNAME";
        ddlBrand.DataValueField = "DIVID";  
        ddlBrand.DataBind();
    }

    public void LoadSubItem()
    {
        ClsFactoryReport ClsLedger = new ClsFactoryReport();
        ddlCategory.Items.Clear();
        ddlCategory.Items.Insert(0, new ListItem("ALL ", "0"));
        ddlCategory.AppendDataBoundItems = true;
        ddlCategory.DataSource = ClsLedger.BindBrand_Subitem();
        ddlCategory.DataTextField = "CATNAME";
        ddlCategory.DataValueField = "CATID";
        ddlCategory.DataBind();
    }

    public void Loadproduct()
    {
        ClsFactoryReport ClsLedger = new ClsFactoryReport();
            ddlnewproduct.Items.Clear();
            ddlnewproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            ddlnewproduct.AppendDataBoundItems = true;
            ddlnewproduct.DataSource = ClsLedger.LoadProduct();
            ddlnewproduct.DataTextField = "NAME";
            ddlnewproduct.DataValueField = "ID";
            ddlnewproduct.DataBind();
 
    }



    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            ClsFactoryReport ClsLedger = new ClsFactoryReport();
            if (ddlBrand.SelectedValue != "")
            {
                if (ddlBrand.SelectedValue != "1")
                {
                    ddlCategory.Items.Clear();
                    ddlCategory.Items.Insert(0, new ListItem("ALL", "0"));
                    ddlCategory.AppendDataBoundItems = true;
                    ddlCategory.DataSource = ClsLedger.BindCategory_SupliedItem(Convert.ToString(ddlBrand.SelectedValue));
                    ddlCategory.DataTextField = "CATNAME";
                    ddlCategory.DataValueField = "CATID";
                    ddlCategory.DataBind();
                }
                else
                {
                    ddlnewproduct.Items.Clear();
                    ddlnewproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                    ddlnewproduct.AppendDataBoundItems = true;
                    ddlnewproduct.DataSource = ClsCommon.BindFGProduct();
                    ddlnewproduct.DataTextField = "NAME";
                    ddlnewproduct.DataValueField = "ID";
                    ddlnewproduct.DataBind();
                }
            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "")
            {
                ClsFactoryReport ClsLedger = new ClsFactoryReport();
                ddlnewproduct.Items.Clear();
                ddlnewproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                ddlnewproduct.AppendDataBoundItems = true;
                ddlnewproduct.DataSource = ClsLedger.BindProduct(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim(), ddldepot.SelectedValue.Trim());
                ddlnewproduct.DataTextField = "NAME";
                ddlnewproduct.DataValueField = "ID";
                ddlnewproduct.DataBind();
            }
            else
            {
                ddlnewproduct.Items.Clear();
                ddlnewproduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = ClsCommon.BindDepot_Primary();
                ddldepot.Items.Clear();
                ddldepot.Items.Add(new ListItem("Select", "0"));
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = ClsCommon.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.Items.Add(new ListItem("Select", "0"));
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else
            {
                DataTable dt = ClsCommon.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                //DataTable dt = ClsCommon.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddldepot.Items.Clear();
                //ddldepot.Items.Add(new ListItem("Select", "0"));
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

    #region LoadStorelocation
    private void LoadStorelocation()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            DataTable dt = ClsCommon.BindStoreLocation();
            ddlstorelocation.Items.Clear();
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dt;
            ddlstorelocation.DataValueField = "ID";
            ddlstorelocation.DataTextField = "NAME";
            ddlstorelocation.DataBind();
            ddlstorelocation.SelectedValue = "113BD8D6-E5DC-4164-BEE7-02A16F97ABCC";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadAllProduct
    public void LoadAllProduct()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            ddlnewproduct.Items.Clear();
            ddlnewproduct.Items.Insert(0, new ListItem("Select Product", "0"));
            ddlnewproduct.DataSource = ClsCommon.BindProductALIAS();
            ddlnewproduct.DataTextField = "NAME";
            ddlnewproduct.DataValueField = "ID";
            ddlnewproduct.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
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
        ClsFactoryReport ClsLedger = new ClsFactoryReport();
        DataTable dtDisplayGrid = new DataTable();
        dtDisplayGrid = ClsLedger.BindItemLedger(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddldepot.SelectedValue.Trim(), ddlnewproduct.SelectedValue.Trim(), ddlstorelocation.SelectedValue.Trim());

        if (dtDisplayGrid.Rows.Count > 0)
        {
            decimal sum = 0;
            decimal sum1 = 0;
            Session["INWARD"] = null;
            Session["OUTWARD"] = null;

            for (int i = 0; i < dtDisplayGrid.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(dtDisplayGrid.Rows[i]["INWARD"].ToString().Trim());
                sum1 += Convert.ToDecimal(dtDisplayGrid.Rows[i]["OUTWARD"].ToString().Trim());
            }
            Session["INWARD"] = Convert.ToString(sum).Trim();
            Session["OUTWARD"] = Convert.ToString(sum1).Trim();

            this.gvItemLedger.DataSource = dtDisplayGrid;
            this.gvItemLedger.DataBind();
            Session["itemledger"] = dtDisplayGrid;
        }
        else
        {
            this.gvItemLedger.DataSource = null;
            this.gvItemLedger.DataBind();
        }
    }
    #endregion

    #region gv_RowDataBound
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;

            if (drv["VType"].ToString().Contains("Opening Stock"))
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[5].Text = "";
                //e.Row.Cells[6].Text = "";
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                string tst = e.Row.Cells[i].Text.ToString();
                double Num;
                bool isNum = double.TryParse(tst, out Num);
                if (isNum)
                {
                    e.Row.Cells[i].Style.Add("text-Align", "right");
                }
                else
                {
                    e.Row.Cells[i].Style.Add("text-Align", "Left");
                }

                e.Row.Cells[1].Style.Add("text-Align", "Left");
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;

            e.Row.Cells[7].Text = Session["INWARD"].ToString().Trim();
            e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[7].Style.Add("text-Align", "right");

            e.Row.Cells[8].Text = Session["OUTWARD"].ToString().Trim();
            e.Row.Cells[8].ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[8].Style.Add("text-Align", "right");
        }
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
        /*DataTable dt = (DataTable)Session["itemledger"];
        int col_count = dt.Columns.Count;
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "inline; filename=" + "ItemLedger_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);        
        hw.Write("<table><tr><td colspan=3>DEPOT: " + ddldepot.SelectedItem.Text + "</td></tr>");
        hw.Write("<table><tr><td colspan=3>ITEM LEDGER REPORT</td></tr>");
        hw.Write("<table><tr><td colspan=3>ITEM: " + ddlnewproduct.SelectedItem.Text.Trim() + "</td></tr>");
        hw.Write("<table><tr><td colspan=3>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </td></tr>");
        gvItemLedger.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();*/

        DataTable dt = (DataTable)Session["itemledger"];
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "ItemLedger_" + this.txtfromdate.Text.Trim() + "_" + this.txttodate.Text.Trim() + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";

        using (StringWriter stringWrite = new StringWriter())
        {
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //To Export all pages
            gvItemLedger.AllowPaging = false;

            gvItemLedger.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvItemLedger.HeaderRow.Cells)
            {
                cell.BackColor = gvItemLedger.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvItemLedger.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvItemLedger.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvItemLedger.RowStyle.BackColor;
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

            htmlWrite.Write("<table><tr><td colspan=3>DEPOT: " + ddldepot.SelectedItem.Text + "</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=3>ITEM LEDGER REPORT</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=3>ITEM: " + ddlnewproduct.SelectedItem.Text.Trim() + "</td></tr>");
            htmlWrite.Write("<table><tr><td colspan=3><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
            for (int i = 0; i < gvItemLedger.HeaderRow.Cells.Count; i++)
            {
                //gvItemLedger.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
                gvItemLedger.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");
            }
            gvItemLedger.RowStyle.Font.Size = 8;
            gvItemLedger.RowStyle.Height = 20;
            gvItemLedger.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            gvItemLedger.Visible = false;
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

    protected void gvItemLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string VoucherID = commandArgs[0];
        string VoucherNo = commandArgs[1];
        string VoucherType = commandArgs[2];
        if (e.CommandName == "VCHID")
        {
            if (VoucherType == "Issue Qty")
            {
                string upath = "../Factory/frmMMIssue.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            }
            else if (VoucherType == "Return" || VoucherType == "Wip")
            {
                string upath = "../Factory/frmProductionOrder_WithoutMRP.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            }
            else if (VoucherType == "QA" || VoucherType == "Wip")
            {
                string upath = "../Factory/frmQualityControl_FAC.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            }
            else if (VoucherType == "Tax Invoice")
            {
                string upath = "../Factory/frmInvoiceMM_FG.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            }
            //else if (VoucherType == "Despatch")
            //{
            //    string upath = "../Factory/frmFactoryStockTransfer.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            //}
            //else if (VoucherType == "Purchase Return")
            //{
            //    string upath = "../Factory/frmPurchaseReturn_MM.aspx?VCHID=" + VoucherID + "&&Name=Itemledger";
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", String.Format("javascript:void(window.open('" + upath + "','_blank'));"), true);
            //}
        }
    }
}