using BAL;
using Obout.Grid;
using PPBLL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmMMPPQualityControl : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InputTable.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";

                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                txtfromdateins.Text = dtcurr.ToString(date).Replace('-', '/');
                txttodateins.Text = dtcurr.ToString(date).Replace('-', '/');
                this.BindVendor();
                this.LoadQCDetails();
                this.DateLock();
                this.Td5.Visible = false;
                this.ckhidtd.Visible = false;
                this.divshowCOA.Visible = false;

                if (Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "FALSE")
                {
                    this.divbtnsave.Style["display"] = "";
                    this.divbtncancel.Style["display"] = "";
                    this.divbtnapprove.Style["display"] = "none";
                    this.divbtnrejection.Style["display"] = "none";
                    this.divnewentry.Style["display"] = "";
                }
                else if (Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "dashboard")
                {
                    this.divnewentry.Style["display"] = "none";
                    this.InputTable.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.AutoQCNo.Visible = false;
                    this.Clearall();
                }
                else
                {
                    this.divbtnsave.Style["display"] = "none";
                    this.divbtncancel.Style["display"] = "";
                    this.divbtnapprove.Style["display"] = "";
                    this.divbtnrejection.Style["display"] = "";
                    this.divnewentry.Style["display"] = "none";
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

    public void LoadQCDetails()
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            gvQcdetails.DataSource = clsmmpp.BindQualityControl(txtfromdateins.Text.Trim(), txttodateins.Text.Trim(), Session["UserID"].ToString().Trim(),
                                                                Session["FINYEAR"].ToString().Trim(), Convert.ToString(Request.QueryString["CHECKER"]).Trim());
            if (Session["UserID"].ToString() == "1" && Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "FALSE")
            {
                this.gvQcdetails.Columns[14].Visible = true;
                gvQcdetails.DataBind();
            }
            else
            {
                this.gvQcdetails.Columns[14].Visible = false;
                gvQcdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    private void Clearall()
    {
        DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        this.ddlgrno.Enabled = true;
        this.ddlVendor.Enabled = true;
        this.ImageButton4.Enabled = true;
        this.hdnqcid.Value = "";
        this.txtqcdate.Text = "";
        txtentryDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtsampledate.Text = "";
        this.txtrefno.Text = "";
        this.txtgrndate.Text = "";
        this.ddlVendor.SelectedValue = "0";
        this.txtremarks.Text = "";
        this.txtrating.Text = "";
        this.txtfactory.Text = "";
        this.ddlgrno.Items.Clear();
        HttpContext.Current.Session["ENTERQCQTY"] = null;
        HttpContext.Current.Session["UPLOADFILENAME"] = null;
        HttpContext.Current.Session["UPLOADFILENAMECOA"] = null;
        this.gvproductdetails.DataSource = null;
        this.gvproductdetails.DataBind();
        this.ddlgrno.Items.Add(new ListItem("SELECT GRN", "0"));
    }

    protected void btnQCfind_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadQCDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private void BindGrnNo()
    {
        ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
        string MODE = string.Empty;
        if (this.hdnqcid.Value == "")
        {
            MODE = "A";
        }
        else
        {
            MODE = "U";
        }
        DataTable dt = clsmmpp.BindGRNNo(this.ddlVendor.SelectedValue.Trim(), MODE);
        if (dt.Rows.Count > 0)
        {
            this.ddlgrno.Items.Clear();
            this.ddlgrno.Items.Add(new ListItem("SELECT GRN", "0"));
            this.ddlgrno.AppendDataBoundItems = true;

            this.ddlgrno.DataSource = dt;
            this.ddlgrno.DataValueField = "STOCKRECEIVEDID";
            this.ddlgrno.DataTextField = "STOCKRECEIVEDNUM";
            this.ddlgrno.DataBind();
        }
        else
        {
            this.ddlgrno.Items.Clear();
            this.ddlgrno.Items.Add(new ListItem("SELECT GRN", "0"));
        }
    }

    private void BindGrnAllNo()
    {
        ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
        string MODE = string.Empty;
        if (this.hdnqcid.Value == "")
        {
            MODE = "Aa";
        }
        else
        {
            MODE = "Uu";
        }
        DataTable dt = clsmmpp.BindGRNNo("", MODE);
        if (dt.Rows.Count > 0)
        {
            this.ddlgrno.Items.Clear();
            this.ddlgrno.Items.Add(new ListItem("SELECT GRN", "0"));
            this.ddlgrno.AppendDataBoundItems = true;

            this.ddlgrno.DataSource = dt;
            this.ddlgrno.DataValueField = "STOCKRECEIVEDID";
            this.ddlgrno.DataTextField = "STOCKRECEIVEDNUM";
            this.ddlgrno.DataBind();

            this.ddlVendor.Items.Clear();
            this.ddlVendor.Items.Add(new ListItem("SELECT ", "0"));
            this.ddlVendor.AppendDataBoundItems = true;

            this.ddlVendor.DataSource = dt;
            this.ddlVendor.DataValueField = "VENDORID";
            this.ddlVendor.DataTextField = "VENDORNAME";
            this.ddlVendor.DataBind();
        }
        else
        {
            this.ddlgrno.Items.Clear();
            this.ddlgrno.Items.Add(new ListItem("SELECT GRN", "0"));
        }
    }

    private void BindVendor()
    {
        ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
        DataTable dt = clsmmpp.BindTpuVendor();
        if (dt.Rows.Count > 0)
        {
            ddlVendor.Items.Clear();
            ddlVendor.Items.Add(new ListItem("SELECT ", "0"));
            ddlVendor.AppendDataBoundItems = true;

            ddlVendor.DataSource = dt;
            ddlVendor.DataValueField = "VENDORID";
            ddlVendor.DataTextField = "VENDORNAME";
            ddlVendor.DataBind();
        }
        else
        {
            ddlVendor.Items.Clear();
            ddlVendor.Items.Add(new ListItem("SELECT ", "0"));
        }
    }

    //protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.BindGrnNo();
    //    }
    //    catch (Exception ex)
    //    {
    //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    }
    //}
    protected void ddlgrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            DataTable dt = new DataTable();
            BindReason();
            if (this.ddlgrno.SelectedValue.Trim() != "0")
            {
                dt = clsmmpp.BindGRNProductDetails(this.ddlgrno.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    this.txtgrndate.Text = Convert.ToString(dt.Rows[0]["STOCKRECEIVEDDATE"]).Trim();
                    this.txtfactory.Text = Convert.ToString(dt.Rows[0]["MOTHERDEPOTNAME"]).Trim();
                    this.ddlVendor.SelectedValue = Convert.ToString(dt.Rows[0]["TPUID"]).Trim();
                    this.gvproductdetails.DataSource = dt;
                    this.gvproductdetails.DataBind();
                }
                else
                {
                    this.gvproductdetails.DataSource = null;
                    this.gvproductdetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region ClickEnent
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "FALSE")
            {
                this.divbtnsave.Style["display"] = "";
                this.divbtncancel.Style["display"] = "";
                this.divbtnapprove.Style["display"] = "none";
                this.divbtnrejection.Style["display"] = "none";
                this.divnewentry.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "";
                this.InputTable.Style["display"] = "none";
            }
            else
            {
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.divnewentry.Style["display"] = "none";
                this.AutoQCNo.Visible = false;
                this.Clearall();
                this.LoadQCDetails();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.divnewentry.Style["display"] = "none";
            
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.AutoQCNo.Visible = false;
            this.Clearall();
            this.BindGrnAllNo();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            if (Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "FALSE")
            {
                this.divbtnsave.Style["display"] = "";
                this.divbtncancel.Style["display"] = "";
                this.divbtnapprove.Style["display"] = "none";
                this.divbtnrejection.Style["display"] = "none";
                this.divnewentry.Style["display"] = "none";
            }
            else
            {
                this.divbtnsave.Style["display"] = "none";
                this.divbtncancel.Style["display"] = "";
                this.divbtnapprove.Style["display"] = "";
                this.divbtnrejection.Style["display"] = "";
                this.divnewentry.Style["display"] = "none";
            }
           
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            Session["ENTERQCQTY"] = null;
            if (HttpContext.Current.Session["ENTERQCQTY"] == null)
            {
                CreateDataTable();
            }

            BindReason();
            DataSet DS = clsmmpp.EditQcDetails(hdnqcid.Value.Trim());
            if (DS.Tables[1].Rows.Count == 0)
            {
                MessageBox1.ShowWarning("No details Found,Please Contact To Support Team For further info");
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                return;
            }
            else
            {
                this.InputTable.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
            }

            if (DS.Tables[0].Rows.Count > 0)
            {
                this.txtqcno.Text = Convert.ToString(DS.Tables[0].Rows[0]["QCNO"]).Trim();
                this.txtqcdate.Text = Convert.ToString(DS.Tables[0].Rows[0]["QCDATE"]).Trim();
                this.txtentryDate.Text = Convert.ToString(DS.Tables[0].Rows[0]["ENTRYDATE"]).Trim();
                this.txtsampledate.Text = Convert.ToString(DS.Tables[0].Rows[0]["SAMPLEDATE"]).Trim();
                this.txtrefno.Text = Convert.ToString(DS.Tables[0].Rows[0]["REFNO"]).Trim();
                this.BindVendor();
                this.ddlVendor.SelectedValue = Convert.ToString(DS.Tables[0].Rows[0]["TPUID"]).Trim();
                this.BindGrnNo();
                this.ddlgrno.SelectedValue = Convert.ToString(DS.Tables[0].Rows[0]["STOCKRECEIVEDID"]).Trim();
                this.txtfactory.Text = Convert.ToString(DS.Tables[0].Rows[0]["FACTORYNAME"]).Trim();
                this.txtgrndate.Text = Convert.ToString(DS.Tables[0].Rows[0]["GRNDATE"]).Trim();
                this.txtremarks.Text = Convert.ToString(DS.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.txtrating.Text = Convert.ToString(DS.Tables[0].Rows[0]["RATING"]).Trim();
            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                DataTable DT = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = DT.NewRow();

                    dr["PRODUCTID"] = Convert.ToString(DS.Tables[1].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(DS.Tables[1].Rows[i]["PRODUCTNAME"]);
                    dr["UNITID"] = Convert.ToString(DS.Tables[1].Rows[i]["UNITID"]);
                    dr["UNITNAME"] = Convert.ToString(DS.Tables[1].Rows[i]["UNITNAME"]);
                    dr["BATCHNO"] = Convert.ToString(DS.Tables[1].Rows[i]["BATCHNO"]);
                    dr["INVOICENO"] = Convert.ToString(DS.Tables[1].Rows[i]["INVOICENO"]);
                    dr["INVOICEDATE"] = Convert.ToString(DS.Tables[1].Rows[i]["INVOICEDATE"]);
                    dr["GATEPASSNO"] = Convert.ToString(DS.Tables[1].Rows[i]["GATEPASSNO"]);
                    dr["MFDATE"] = Convert.ToString(DS.Tables[1].Rows[i]["MFDATE"]);
                    dr["EXPRDATE"] = Convert.ToString(DS.Tables[1].Rows[i]["EXPRDATE"]);
                    dr["RECEIVEDQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["RECEIVEDQTY"]);

                    dr["SAMPLEQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["SAMPLEQTY"]);

                    dr["QCQUALIFIEDQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["QCQUALIFIEDQTY"]);
                    dr["RETURNQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["RETURNQTY"]);
                    dr["HOLDQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["HOLDQTY"]);
                    dr["REJECTEDQTY"] = Convert.ToString(DS.Tables[1].Rows[i]["REJECTEDQTY"]);
                    dr["REASONID"] = Convert.ToString(DS.Tables[1].Rows[i]["REASONID"]);
                    dr["REASONNAME"] = Convert.ToString(DS.Tables[1].Rows[i]["REASONNAME"]);
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                }

                HttpContext.Current.Session["ENTERQCQTY"] = DT;
                this.gvproductdetails.DataSource = DT;
                this.gvproductdetails.DataBind();
            }
            else
            {
                this.gvproductdetails.DataSource = null;
                this.gvproductdetails.DataBind();
            }

            if (DS.Tables[0].Rows[0]["FILEUPLOADTAG"].ToString().Trim() == "Y")
            {
                chkfileupload.Checked = true;
                divshow.Style["display"] = "";
            }
            else
            {
                chkfileupload.Checked = false;
                divshow.Style["display"] = "none";
            }

            if (DS.Tables[0].Rows[0]["FILEUPLOADTAGCOA"].ToString().Trim() == "Y")
            {
                chkfileuploadCOA.Checked = true;
                btnshowCOA.Style["display"] = "";
            }
            else
            {
                chkfileuploadCOA.Checked = false;
                btnshowCOA.Style["display"] = "none";
            }
            if (DS.Tables[0].Rows[0]["ISVERIFIED"].ToString().Trim() == "Y")
            {
                this.divbtnapprove.Style["display"] = "none";
                this.divbtnrejection.Style["display"] = "none";
            }
            else if (DS.Tables[0].Rows[0]["ISVERIFIED"].ToString().Trim() == "R")
            {
                this.divbtnapprove.Style["display"] = "none";
                this.divbtnrejection.Style["display"] = "none";
            }
            this.AutoQCNo.Visible = true;
            this.ddlgrno.Enabled = false;
            this.ddlVendor.Enabled = false;
            this.ImageButton4.Enabled = false;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrdview_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.TableName = "XMLData";
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    #endregion

    #region Convert DataTable To XML COA
    public string ConvertDatatableToXMLCOA(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.TableName = "XMLData";
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    #endregion

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("UNITID", typeof(string)));

        dt.Columns.Add(new DataColumn("UNITNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICENO", typeof(string)));
        dt.Columns.Add(new DataColumn("GATEPASSNO", typeof(string)));
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("SAMPLEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("QCQUALIFIEDQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("HOLDQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("REJECTEDQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICEDATE", typeof(string)));
        HttpContext.Current.Session["ENTERQCQTY"] = dt;
    }
    #endregion

    #region Save
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            if (HttpContext.Current.Session["ENTERQCQTY"] == null)
            {
                CreateDataTable();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
            int count = 0;
            string MODE = string.Empty;
            if (this.hdnqcid.Value == "")
            {
                MODE = "A";
            }
            else
            {
                MODE = "U";
            }

            string fileuploadtag = string.Empty;
            string xmlupload = string.Empty;

            DataTable dtupload = new DataTable();
            if (chkfileupload.Checked)
            {
                dtupload = (DataTable)(Session["UPLOADFILENAME"]);
                xmlupload = ConvertDatatableToXML(dtupload);
                fileuploadtag = "Y";
            }
            else
            {
                fileuploadtag = "N";
                // clsmmpp.FileDelete(hdnqcid.Value.Trim());
            }

            string fileuploadtag_coa = string.Empty;
            string xmluploadcos = string.Empty;

            DataTable dtuploadcoa = new DataTable();
            if (chkfileuploadCOA.Checked)
            {
                dtuploadcoa = (DataTable)(Session["UPLOADFILENAMECOA"]);
                xmluploadcos = ConvertDatatableToXMLCOA(dtuploadcoa);
                fileuploadtag_coa = "Y";
            }
            else
            {
                fileuploadtag_coa = "N";
                //clsmmpp.FileDelete(hdnqcid.Value.Trim());
            }

            if (hdnqcid.Value.Trim() == "")
            {
                foreach (GridViewRow gvrow in gvproductdetails.Rows)
                {
                    Label lblPRODUCTID = (Label)gvrow.FindControl("lblPRODUCTID");
                    Label lblPRODUCTNAME = (Label)gvrow.FindControl("lblPRODUCTNAME");
                    Label lblPACKINGSIZEID = (Label)gvrow.FindControl("lblPACKINGSIZEID");
                    Label lblPACKINGSIZENAME = (Label)gvrow.FindControl("lblPACKINGSIZENAME");
                    Label lblRECEIVEDQTY = (Label)gvrow.FindControl("lblRECEIVEDQTY");
                    Label lblBATCHNO = (Label)gvrow.FindControl("lblBATCHNO");
                    Label lblINVOICENO = (Label)gvrow.FindControl("lblINVOICENO");
                    Label lblGATEPASSNO = (Label)gvrow.FindControl("lblGATEPASSNO");
                    Label lblMFDATE = (Label)gvrow.FindControl("lblMFDATE");
                    Label lblEXPRDATE = (Label)gvrow.FindControl("lblEXPRDATE");

                    TextBox txtsampleqty = (TextBox)gvrow.FindControl("txtsampleqty");
                    TextBox txtQCQUALIFIEDQty = (TextBox)gvrow.FindControl("txtQCQUALIFIEDQty");
                    TextBox txtReturnQty = (TextBox)gvrow.FindControl("txtReturnQty");
                    TextBox txtHOLDQTY = (TextBox)gvrow.FindControl("txtHOLDQTY");
                    TextBox txtREJECTEDQTY = (TextBox)gvrow.FindControl("txtREJECTEDQTY");

                    DropDownList ddlREAJECTIONREASON = (DropDownList)gvrow.FindControl("ddlREAJECTIONREASON");

                    string reasonselectvalue = ddlREAJECTIONREASON.SelectedValue.Trim();
                    string reasonselectitem = ddlREAJECTIONREASON.SelectedItem.ToString().Trim();
                    if (txtsampleqty.Text == "")
                    {
                        txtsampleqty.Text = "0";
                        txtReturnQty.Text = "0";
                        txtHOLDQTY.Text = "0";
                        txtREJECTEDQTY.Text = "0";
                    }
                    if (Convert.ToDecimal(lblRECEIVEDQTY.Text.Trim()) < Convert.ToDecimal(txtQCQUALIFIEDQty.Text.Trim()))/*new add by p.basu*/
                    {
                        MessageBox1.ShowWarning("Received Qty Cannot be less Than Qualified Qty");
                        return;
                    }
                    decimal allQty= (Convert.ToDecimal(txtQCQUALIFIEDQty.Text.Trim())+ Convert.ToDecimal(txtsampleqty.Text.Trim())+ Convert.ToDecimal(txtReturnQty.Text.Trim())+ Convert.ToDecimal(txtHOLDQTY.Text.Trim())+ Convert.ToDecimal(txtREJECTEDQTY.Text.Trim()));
                    if(allQty> Convert.ToDecimal(lblRECEIVEDQTY.Text.Trim()))
                    {
                        MessageBox1.ShowWarning("Received Qty Cannot be less Than Total Qty");
                        return;
                    }

                    DataRow dr = DT.NewRow();
                    dr["PRODUCTID"] = lblPRODUCTID.Text.Trim();
                    dr["PRODUCTNAME"] = lblPRODUCTNAME.Text.Trim();
                    dr["UNITID"] = lblPACKINGSIZEID.Text.Trim();
                    dr["UNITNAME"] = lblPACKINGSIZENAME.Text.Trim();
                    dr["BATCHNO"] = lblBATCHNO.Text.Trim();
                    dr["INVOICENO"] = lblINVOICENO.Text.Trim();
                    dr["GATEPASSNO"] = lblGATEPASSNO.Text.Trim();
                    dr["MFDATE"] = lblMFDATE.Text.Trim();
                    dr["EXPRDATE"] = lblEXPRDATE.Text.Trim();
                    dr["RECEIVEDQTY"] = lblRECEIVEDQTY.Text.Trim();
                    dr["SAMPLEQTY"] = txtsampleqty.Text.Trim();
                    dr["QCQUALIFIEDQTY"] = txtQCQUALIFIEDQty.Text.Trim();
                    dr["RETURNQTY"] = txtReturnQty.Text.Trim();
                    dr["HOLDQTY"] = txtHOLDQTY.Text.Trim();
                    dr["REJECTEDQTY"] = txtREJECTEDQTY.Text.Trim();
                    dr["REASONID"] = reasonselectvalue.Trim();
                    dr["REASONNAME"] = reasonselectitem.Trim();
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                    count = count + 1;
                }
            }
            else
            {
                if (DT.Rows.Count > 0)
                {
                    for (int k = 0; k < gvproductdetails.Rows.Count; k++)
                    {
                        //TimelineID = ((TextBox)gvTimeline.Rows[i].FindControl("txtTimeline")).Text.Trim();
                        TextBox txtsampleqty = (TextBox)gvproductdetails.Rows[k].FindControl("txtsampleqty");
                        TextBox txtQCQUALIFIEDQty = (TextBox)gvproductdetails.Rows[k].FindControl("txtQCQUALIFIEDQty");
                        TextBox txtReturnQty = (TextBox)gvproductdetails.Rows[k].FindControl("txtReturnQty");
                        TextBox txtHOLDQTY = (TextBox)gvproductdetails.Rows[k].FindControl("txtHOLDQTY");
                        TextBox txtREJECTEDQTY = (TextBox)gvproductdetails.Rows[k].FindControl("txtREJECTEDQTY");
                        Label lblRECEIVEDQTY = (Label)gvproductdetails.Rows[k].FindControl("lblRECEIVEDQTY");

                        DropDownList ddlREAJECTIONREASON = (DropDownList)gvproductdetails.Rows[k].FindControl("ddlREAJECTIONREASON");

                        string reasonselectvalue = ddlREAJECTIONREASON.SelectedValue.Trim();
                        string reasonselectitem = ddlREAJECTIONREASON.SelectedItem.ToString().Trim();
                      
                        if(txtsampleqty.Text=="")
                        {
                            txtsampleqty.Text = "0";
                            txtReturnQty.Text = "0";
                            txtHOLDQTY.Text = "0";
                            txtREJECTEDQTY.Text = "0";
                        }
                        if (Convert.ToDecimal(lblRECEIVEDQTY.Text.Trim()) < Convert.ToDecimal(txtQCQUALIFIEDQty.Text.Trim()))/*new add by p.basu*/
                        {
                            MessageBox1.ShowWarning("Received Qty Cannot be less Than Qualified Qty");
                            return;
                        }
                        decimal allQty = (Convert.ToDecimal(txtQCQUALIFIEDQty.Text.Trim()) + Convert.ToDecimal(txtsampleqty.Text.Trim()) + Convert.ToDecimal(txtReturnQty.Text.Trim()) + Convert.ToDecimal(txtHOLDQTY.Text.Trim()) + Convert.ToDecimal(txtREJECTEDQTY.Text.Trim()));
                        if (allQty > Convert.ToDecimal(lblRECEIVEDQTY.Text.Trim()))
                        {
                            MessageBox1.ShowWarning("Received Qty Cannot be less Than Total Qty");
                            return;
                        }

                        //DT.Rows[k]["RECEIVEDQTY"] = lblRECEIVEDQTY.Text.Trim();
                        DT.Rows[k]["SAMPLEQTY"] = txtsampleqty.Text.Trim();
                        DT.Rows[k]["QCQUALIFIEDQTY"] = txtQCQUALIFIEDQty.Text.Trim();
                        DT.Rows[k]["RETURNQTY"] = txtReturnQty.Text.Trim();
                        DT.Rows[k]["HOLDQTY"] = txtHOLDQTY.Text.Trim();
                        DT.Rows[k]["REJECTEDQTY"] = txtREJECTEDQTY.Text.Trim();
                        DT.Rows[k]["REASONID"] = reasonselectvalue.Trim();
                        DT.Rows[k]["REASONNAME"] = reasonselectitem.Trim();

                    }
                }
            }
            HttpContext.Current.Session["ENTERQCQTY"] = DT;
            string qcno = string.Empty;
            string FinYear = Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim();
            if (FinYear == "")
            {
                MessageBox1.ShowInfo("Please Login Again");
            }
            else
            {
                string xmldetails = string.Empty;
                xmldetails = ConvertDatatableToXML(DT);

                qcno = clsmmpp.SAVEMMQC(this.hdnqcid.Value.Trim(),
                                        this.txtqcdate.Text.Trim(),
                                        this.txtentryDate.Text.Trim(),
                                        this.txtsampledate.Text.Trim(),
                                        this.txtrefno.Text.Trim(),
                                        this.ddlgrno.SelectedItem.ToString().Trim(),
                                        this.ddlgrno.SelectedValue.Trim(),
                                        this.txtgrndate.Text.Trim(),
                                        this.ddlVendor.SelectedValue.Trim(),
                                        this.ddlVendor.SelectedItem.ToString().Trim(),
                                        Convert.ToInt32(HttpContext.Current.Session["USERID"].ToString().Trim()),
                                        FinYear,
                                        MODE.Trim(),
                                        this.txtremarks.Text.Trim(),
                                        this.txtrating.Text.Trim(), Convert.ToString(Request.QueryString["MENUID"]).Trim(), xmldetails, xmlupload, xmluploadcos, fileuploadtag_coa, fileuploadtag);

                if (qcno != "")
                {
                    if (Convert.ToString(hdnqcid.Value.Trim()) == "")
                    {
                        MessageBox1.ShowSuccess("Quality Assurance No : <b><font color='green'>  " + qcno + "</b>!</font> Saved Successfully", 60, 600);
                        DataTable dt = new DataTable();
                        ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                        dt = clsMMPo.Bind_Sms_Mobno("U", "");
                        foreach (DataRow row in dt.Rows)
                        {
                            this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                        }
                        DataTable dt1 = new DataTable();
                        dt1 = clsMMPo.Bind_Sms_Mobno("V", "");
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Quality Assurance No : <b><font color='green'>  " + qcno + "</b>!</font> Updated Successfully", 60, 600);
                    }
                    this.Clearall();
                    this.gvproductdetails.DataSource = null;
                    this.gvproductdetails.DataBind();
                    HttpContext.Current.Session["ENTERQCQTY"] = null;
                    this.InputTable.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.divnewentry.Style["display"] = "";
                    this.AutoQCNo.Visible = false;
                    this.LoadQCDetails();
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record ");
                    this.InputTable.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.AutoQCNo.Visible = false;
                    return;
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

    protected void chkfileupload_check(object sender, EventArgs e)
    {
        try
        {
            if (chkfileupload.Checked == true)
            {
                Session["UPLOADFILENAME"] = null;
                string strPopup = string.Empty;
                if (this.hdnqcid.Value.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCFileUpload_FAC.aspx?QCID=" + hdnqcid.Value.Trim() + ""
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCFileUpload_FAC.aspx?QCID= "
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                divshow.Style["display"] = "";
            }
            else
            {
                divshow.Style["display"] = "none";
                ClsMMPPQualityControl clsqc = new ClsMMPPQualityControl();
                clsqc.FileDelete(hdnqcid.Value.Trim());
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            string strPopup = string.Empty;
            if (this.hdnqcid.Value.Trim() != "")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmQCFileUpload_FAC.aspx?QCID=" + hdnqcid.Value.Trim() + "&MODE=QC"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmQCFileUpload_FAC.aspx?QCID= "
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void chkfileupload_check_COA(object sender, EventArgs e)
    {
        try
        {
            if (chkfileuploadCOA.Checked == true)
            {
                Session["UPLOADFILENAMECOA"] = null;
                string strPopup = string.Empty;
                if (this.hdnqcid.Value.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCCOAFileDownload_FAC.aspx?QCID=" + hdnqcid.Value.Trim() + ""
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCCOAFileDownload_FAC.aspx?QCID= "
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                divshowCOA.Style["display"] = "";
            }
            else
            {
                divshowCOA.Style["display"] = "none";
                ClsMMPPQualityControl clsqc = new ClsMMPPQualityControl();
                clsqc.FileDelete_COA(hdnqcid.Value.Trim());
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void btnshow_Click_COA(object sender, EventArgs e)
    {
        try
        {
            string strPopup = string.Empty;
            if (this.hdnqcid.Value.Trim() != "")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmQCCOAFileDownload_FAC.aspx?QCID=" + hdnqcid.Value.Trim() + "&MODE=QC"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmQCCOAFileDownload_FAC.aspx?QCID= "
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    public void BindReason()
    {
        ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
        DataTable dt = clsmmpp.BindReason(Request.QueryString["MENUID"].ToString());
        Session["reason"] = dt;
    }

    protected void gvproductdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropDownList1 = (e.Row.FindControl("ddlREAJECTIONREASON") as DropDownList);
            DataTable dt = (DataTable)Session["reason"];
            DropDownList1.AppendDataBoundItems = true;
            DropDownList1.DataSource = dt;
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataTextField = "DESCRIPTION";
            DropDownList1.DataBind();

            if (hdnqcid.Value != "")
            {
                string lblPRODUCTNAME = (e.Row.FindControl("lblPRODUCTNAME") as Label).Text;
                DataTable dt1 = (DataTable)Session["ENTERQCQTY"];
                string REASONID = string.Empty;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    REASONID = Convert.ToString(dt1.Rows[i]["REASONID"]);
                    DropDownList1.SelectedValue = Convert.ToString(REASONID.Trim());
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            int flag = 0;
            flag = clsmmpp.approveQC(Convert.ToString(hdnqcid.Value).Trim());

            if (flag == 1)
            {
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadQCDetails();
                MessageBox1.ShowSuccess("QC: <b><font color='green'>" + this.txtqcno.Text + "</font></b> approved successfully.", 60, 500);
                DataTable dt = new DataTable();
                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                dt = clsMMPo.Bind_Sms_Mobno("M", Convert.ToString(hdnqcid.Value).Trim());
                foreach (DataRow row in dt.Rows)
                {
                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                }
                DataTable dt1 = new DataTable();
                dt1 = clsMMPo.Bind_Sms_Mobno("N", Convert.ToString(hdnqcid.Value).Trim());


            }
            else if (flag == 0)
            {
                this.InputTable.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            int flag = 0;
            flag = clsmmpp.REJECTQC(Convert.ToString(hdnqcid.Value).Trim(), Session["UserID"].ToString().Trim());
            if (flag == 1)
            {
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadQCDetails();
                MessageBox1.ShowSuccess("QC: <b><font color='red'>" + this.txtqcno.Text + "</font></b> Reject successfully.", 60, 500);
            }
            else
            {
                MessageBox1.ShowError("QC: <b><font color='red'>" + this.txtqcno.Text + "</font></b> Reject unsuccessful.", 60, 500);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region gvQcdetails_RowDataBound
    protected void gvQcdetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
                GridDataControlFieldCell cell17 = e.Row.Cells[9] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();
                string APPROVEDtatus = cell17.Text.Trim().ToUpper();

                if (status == "N")
                {
                    cell17.ForeColor = Color.Blue;
                }
                else if (status == "R")
                {
                    cell17.ForeColor = Color.Red;
                }
                else if (status == "HOLD")
                {
                    cell17.ForeColor = Color.Black;
                }
                else
                {
                    cell17.ForeColor = Color.Green;
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

    protected void btngrdqcdelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsMMPPQualityControl clsmmpp = new ClsMMPPQualityControl();
            int flag = 0;
            flag = clsmmpp.DeleteQC(hdnqcid.Value.ToString().Trim());
            if (flag >= 1)
            {
                this.LoadQCDetails();
                MessageBox1.ShowSuccess("Record deleted Successfully!");
                HttpContext.Current.Session["ENTERQCQTY"] = null;
                HttpContext.Current.Session["UPLOADFILENAME"] = null;
            }
            else
            {
                MessageBox1.ShowError("Record deleted unsuccessful!");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region SMS
    public string SMS_Block(string numbers, string message)
    {
        string result;
        string sender = "MCNROE";
        string apiKey = "6NWhmRBnhD8-mDxNax8Q8a1R53Ouxmr7HGIu1CMKWu";
        String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&sender=" + sender + "&numbers=" + numbers + "&message=" + message;
        System.IO.StreamWriter myWriter = null;
        System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

        objRequest.Method = "POST";
        objRequest.ContentLength = System.Text.Encoding.UTF8.GetByteCount(url);
        objRequest.ContentType = "application/x-www-form-urlencoded";
        try
        {
            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(url);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }

        System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
        using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            // Close and clean up the StreamReader
            sr.Close();
        }
        return result;
    }
    #endregion

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            Calendar1.StartDate = oDate;
            CalendarExtender4.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;
            CalendarExtender5.StartDate = oDate;
            CalendarExtender2.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'); ;
                this.txtsampledate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                ///this.txtwaybilldate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                Calendar1.EndDate = today1;
                CalendarExtender4.EndDate = today1;
                CalendarExtender3.EndDate = today1;
                CalendarExtender5.EndDate = today1;
                CalendarExtender2.EndDate = today1;
            }
            else
            {
                this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtsampledate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                //this.txtwaybilldate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                Calendar1.EndDate = cDate;
                CalendarExtender4.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
                CalendarExtender5.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            /*string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);*/
        }
    }
    #endregion
}