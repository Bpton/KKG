using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Account;
public partial class VIEW_frmRptChequeBooklet : System.Web.UI.Page
{
    protected void Page_Init()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdledger1.ClientID + "', 400, '100%' , 25 ,true); </script>", false);

    }
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlspan').multiselect({ includeSelectAllOption: true  });});</script>", false);
             if (!IsPostBack)
            {
                InisiliseDate();
                this.LoadBankName();

            }
            //grdstock.ExportingSettings.FileName = "STOCKINHAND_" + txtdate.Text.Trim() + "_" + HttpContext.Current.Session["USERNAME"].ToString().Trim() + "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdMigrationCode.ClientID + "',450, '100%' , 30 ,false); </script>", false);
    }
    #endregion
    private void InisiliseDate()
    {
       
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
       
         DateTime today1 = DateTime.Now;
       
    }


    #region LoadBankName
    public void LoadBankName()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = clsreport.LoadBankName("Bank", "0");
            ddlBankName.Items.Clear();
            ddlBankName.Items.Add(new ListItem("Select All", "0"));
            ddlBankName.AppendDataBoundItems = true;
            ddlBankName.DataSource = dt;
            ddlBankName.DataValueField = "BANKID";
            ddlBankName.DataTextField = "BANKNAME";
            ddlBankName.DataBind();
            ddlBookletNo.Items.Add(new ListItem("Select All", "0"));

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region LoadBookletNo
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadBookletNo();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadBookletNo()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = clsreport.LoadBankName("Booklet", ddlBankName.SelectedValue.ToString());
            ddlBookletNo.Items.Clear();
            ddlBookletNo.Items.Add(new ListItem("Select All", "0"));
            ddlBookletNo.AppendDataBoundItems = true;
            ddlBookletNo.DataSource = dt;
            ddlBookletNo.DataValueField = "ChequeBookletID";
            ddlBookletNo.DataTextField = "BookletNo";
            ddlBookletNo.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion  

    #region btnshow_Click
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region BindGrid
    public void BindGrid()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = new DataTable();
            string str = "N";
            if (ddlReport.SelectedValue.ToString() == "Details")
            {
                str = "D";
            }
            else if (ddlReport.SelectedValue.ToString() == "Summary")
            {
                str = "S";
            }
            else if (ddlReport.SelectedValue.ToString() == "Cancel")
            {
                str = "C";
            }
            else if (ddlReport.SelectedValue.ToString() == "Disposal")
            {
                str = "L";
            }
            else if (ddlReport.SelectedValue.ToString() == "AlreadyIssue")
            {
                str = "I";
            }
            else if (ddlReport.SelectedValue.ToString() == "Balance")
            {
                str = "B";
            }


            dt = clsreport.BindChequeBooklet(ddlBankName.SelectedValue.ToString(), str, ddlBookletNo.SelectedValue.ToString());

            if (str == "D")
            {
                divDetails.Visible = true;
                divAll.Visible = false;
                this.grdledger1.DataSource = dt;
                this.grdledger1.DataBind();
                Session["BSExpRpt"] = dt;

            }
            else
            {
                divDetails.Visible = false;
                divAll.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    this.grdMigrationCode.DataSource = dt;
                    this.grdMigrationCode.DataBind();
                    Session["BSExpRpt"] = dt;

                }
                else
                {
                    this.grdMigrationCode.DataSource = null;
                    this.grdMigrationCode.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #region Export To Excel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["BSExpRpt"];
        Session["BSExpRpt"] = dt;
        Response.Clear();
        Response.AddHeader("content-disposition", "inline; filename=" + "ChequeBookletReport_" + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        htmlWrite.Write("<table><tr><td colspan=12><b>K.K.G. INDUSTRIES (UNIT-III)</b></td></tr>");
        htmlWrite.Write("<table><tr><td colspan=12><b>Cheque Booklet : </b></td></tr>");
        //htmlWrite.Write("<table><tr><td colspan=13><b>PERIOD: " + txtfromdate.Text + " TO " + txttodate.Text + " </b></td></tr>");
        //this.ClearControls(grdledger);
        grdMigrationCode.Visible = true;
        grdMigrationCode.DataSource = dt;
        grdMigrationCode.DataBind();

        for (int i = 0; i < grdMigrationCode.HeaderRow.Cells.Count; i++)
        {
            grdMigrationCode.HeaderRow.Cells[i].Style.Add("background-color", "#3AC0F2");
            grdMigrationCode.HeaderRow.Cells[i].Style.Add("Font-weight", "bold");
        }
        grdMigrationCode.RowStyle.Font.Bold = true;
        grdMigrationCode.RowStyle.Font.Size = 10;
        grdMigrationCode.RowStyle.Height = 20;
        grdMigrationCode.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

        grdMigrationCode.Visible = false;
    }
    #endregion

    #region gv_RowDataBound
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string a = e.Row.Cells[1].Text;
            e.Row.Cells[1].Width = 100;
        }

    }
    #endregion


    protected void grdledger1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        Control ctrl = e.CommandSource as Control;
        if (ctrl != null)
        {
            GridViewRow gvRow = ctrl.Parent.NamingContainer as GridViewRow;
            Label lblAccEntryid = (Label)gvRow.FindControl("lblAcc2");


            ClsVoucherEntry obj = new ClsVoucherEntry();
            DataTable dtobj = new DataTable();
            string VoucherId = "", InvoiceId = "", BRANCHID = "";

            string AccEntryID = lblAccEntryid.Text.ToString();
            dtobj = obj.FetchInvoiceId(AccEntryID);
            VoucherId = dtobj.Rows[0]["VOUCHERTYPEID"].ToString();
            InvoiceId = dtobj.Rows[0]["INVOICEID"].ToString();
            BRANCHID = dtobj.Rows[0]["BRANCHID"].ToString();

            if (e.CommandName == "VoucherNo")
            {
                if (VoucherId == "16" || VoucherId == "15" || VoucherId == "13" || VoucherId == "14")/*Advance Receipt And Advance Payment Credit Note and Debit Note Query String*/
                {
                    string upath = "frmAccAdvanceVoucher.aspx?InvId=" + AccEntryID + "&&VoucherId=" + VoucherId + "&&CHECKER=TRUE&&TYPE=LGRREPORT&&BRANCHID=" + BRANCHID + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1370,height=1000,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=0,top=0' );", true);
                }
            }
        }
    }
}