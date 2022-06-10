using BAL;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public class QCdetails
{
    public string PUID { get; set; }
    public string PUNO { get; set; }
    public string PRODUCTIONQTY { get; set; }
    public string QCQTY { get; set; }
    public string AVAILABLEQTY { get; set; }
    public string BATCH { get; set; }
    public string SAMPLEQTY { get; set; }
}
public partial class VIEW_frmQualityControl_FAC : System.Web.UI.Page
{
    string Checker = string.Empty;
    ClsQualityAssurance clsqualitycontrol = new ClsQualityAssurance();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region QueryString                
                //Checker = Request.QueryString["CHECKER"].ToString().Trim();
                if (Convert.ToString(Request.QueryString["CHECKER"]) == "TRUE")
                {
                    this.divbtnsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divnewentry.Visible = false;
                    this.trproductdetails.Visible = false;
                    this.divqcdetails.Visible = false;
                }
                else
                {
                    this.divbtnsave.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divnewentry.Visible = true;
                    this.trproductdetails.Visible = true;
                    this.divqcdetails.Visible = true;
                }
                #endregion

                if (Convert.ToString(Request.QueryString["MENUID"]) == "dashboard")
                {
                    HttpContext.Current.Session["UPLOADFILENAME"] = null;
                    gvqcentry.EnableViewState = true;
                    divnewentry.Visible = false;
                    btnadd.Visible = true;
                    btncancel.Visible = true;
                    btnsave.Visible = true;
                    InputTable.Enabled = true;
                    this.txtremarks.Text = "";
                    fldAutoQcNumber.Style["display"] = "none";
                    fldAutoQcNumberheader.Style["display"] = "none";
                    InputTable.Style["display"] = "";
                    pnlDisplay.Style["display"] = "none";
                    clsqualitycontrol.ResetDataTables();   // Reset all Datatables
                    clsqualitycontrol.BindQCProduct();
                    this.gvqcentry.DataSource = null;
                    this.gvqcentry.DataBind();
                    hdn_qcno.Value = "";
                    hdn_guid.Value = "";
                    hdn_tpuid.Value = "";
                    txtqualitycontrolno.Text = "";
                    divshow.Style["display"] = "none";
                    chkfileupload.Checked = false;

                    this.trproductdetails.Style["display"] = "";
                    this.divbtnsave.Style["display"] = "";
                    this.divqcdetails.Style["display"] = "";
                    this.gvQCUpdate.Columns[18].Visible = true;

                    //string lastvalue = string.Empty;

                    foreach (ListItem item in ddlTPUName.Items)
                    {
                        if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "Blue");
                        }
                    }

                    this.ddlponoNew.Items.Clear();
                    this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
                    this.ddlponoNew.AppendDataBoundItems = true;
                    this.ddlponoNew.SelectedValue = "0";

                    this.ddlpono.Items.Clear();
                    this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                    this.ddlpono.AppendDataBoundItems = true;
                    this.ddlpono.SelectedValue = "0";

                    LoadTPUName();
                    this.LoadProduct(this.ddlTPUName.SelectedValue.Trim());
                    this.txtcasepack.Text = "";
                }
                else if (Convert.ToString(Request.QueryString["Name"]) == "Itemledger")
                {
                    string QAID = Convert.ToString(Request.QueryString["VCHID"]);
                    //divbtnreject.Visible = false;
                    divbtnapprove.Visible = false;
                    divbtnsave.Visible = false;
                    //divbtncancel.Visible = false;
                    ItemLedger_QA(QAID);
                }
                else
                {
                    InputTable.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    txtqualitycontrolno.Enabled = false;
                    clsqualitycontrol.BindQCProduct();
                    LoadQCDetails();
                    fldAutoQcNumber.Style["display"] = "none";
                    fldAutoQcNumberheader.Style["display"] = "none";
                    this.DateLock();
                }
            }

            foreach (ListItem item in ddlTPUName.Items)
            {
                if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "Blue");
                    //lastvalue = item.Value;
                }
            }

            foreach (ListItem item in ddlpono.Items)
            {
                if (item.Value == "1")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "blue");
                    //item.Attributes.CssStyle.Add("background-color", "Beige");
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

    protected void btnQCfind_Click(object sender, EventArgs e)
    {
        try
        {
            LoadQCDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region Close
    public void Reset()
    {
        this.ddlponoNew.Items.Clear();
        this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
        this.ddlponoNew.AppendDataBoundItems = true;
        this.ddlponoNew.SelectedValue = "0";

        this.ddlpono.Items.Clear();
        this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
        this.ddlpono.AppendDataBoundItems = true;
        this.ddlpono.SelectedValue = "0";

        this.ddlProductName.Items.Clear();
        this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
        this.ddlProductName.AppendDataBoundItems = true;
        this.ddlProductName.SelectedValue = "0";
    }

    public void Close()
    {
        hdn_qcno.Value = "";
        hdn_guid.Value = "";
        hdn_tpuid.Value = "";
        this.txtremarks.Text = "";
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        if (Checker == "TRUE")
        {
            this.divnewentry.Visible = false;
        }
        else
        {
            this.divnewentry.Visible = true;
        }
        txtQCDate.Text = "";
        lblpotime.Text = "";
        txtQCRefNo.Text = "";
        txtqualitycontrolno.Text = "";
        gvqcentry.EnableViewState = false;
        gvqcentry.ClearPreviousDataSource();
        gvqcentry.DataSource = null;
        gvqcentry.DataBind();

        gvQCUpdate.ClearPreviousDataSource();
        gvQCUpdate.DataSource = null;
        gvQCUpdate.DataBind();

        this.ddlponoNew.Items.Clear();
        this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
        this.ddlponoNew.AppendDataBoundItems = true;
        this.ddlponoNew.SelectedValue = "0";

        this.ddlpono.Items.Clear();
        this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
        this.ddlpono.AppendDataBoundItems = true;
        this.ddlpono.SelectedValue = "0";

        this.ddlProductName.Items.Clear();
        this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
        this.ddlProductName.AppendDataBoundItems = true;
        this.ddlProductName.SelectedValue = "0";

        LoadQCDetails();
        ClsQualityAssurance clsqualitycontrol = new ClsQualityAssurance();
        clsqualitycontrol.ResetDataTables();   // Reset all Datatables
        InputTable.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        fldAutoQcNumber.Style["display"] = "none";
        fldAutoQcNumberheader.Style["display"] = "none";
        ClearDataTable(); // Clear Local Datatable

        this.trproductdetails.Style["display"] = "";
        this.divbtnsave.Style["display"] = "";
        this.divqcdetails.Style["display"] = "";
        this.gvQCUpdate.Columns[18].Visible = true;
    }
    #endregion

    #region Method
    public void ClearDataTable()
    {
        HttpContext.Current.Session["PRODUCTIONQTY"] = null;
        HttpContext.Current.Session["ENTERQCQTY"] = null;
        HttpContext.Current.Session["UPLOADFILENAME"] = null;
    }
    public void LoadTPUName()
    {
        try
        {
            ddlTPUName.Items.Clear();
            ddlTPUName.Items.Add(new ListItem("SELECT TPU/FACTORY NAME", "0"));
            ddlTPUName.AppendDataBoundItems = true;
            ddlTPUName.DataSource = clsqualitycontrol.BindTPU(Session["UserID"].ToString());
            ddlTPUName.DataValueField = "VENDORID";
            ddlTPUName.DataTextField = "VENDORNAME";
            ddlTPUName.DataBind();
            ddlTPUName.SelectedValue = Session["DEPOTID"].ToString();//Session["TPUID"].ToString();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    public void LoadQCDetails()
    {
        try
        {
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.gvCurrentQcdetails.DataSource = clsqualitycontrol.BindQualityControl(txtfromdateins.Text.Trim(), txttodateins.Text.Trim(), Session["UserID"].ToString().Trim(),
                                                                                      Session["FINYEAR"].ToString().Trim(), Session["DEPOTID"].ToString().Trim(), Checker);
            this.gvCurrentQcdetails.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    protected void LoadQCGrid()
    {
        //gvqcentry.ClearPreviousDataSource();
        for (int counter = 0; gvqcentry.RowsInViewState.Count > counter; counter++)
        {
            if (gvqcentry.RowsInViewState[counter].Cells.Count > 6)
            {
                GridDataControlFieldCell cellInViewState = gvqcentry.RowsInViewState[counter].Cells[7] as GridDataControlFieldCell;
                TextBox textBoxInViewState = cellInViewState.FindControl("txtcurrentqcqty") as TextBox;
                textBoxInViewState.Text = "0";
            }
        }
        DataTable dtproductionno = clsqualitycontrol.BindQCUpdate(ddlponoNew.SelectedValue.ToString(), this.ddlpono.SelectedValue.Substring(0, ddlpono.SelectedValue.IndexOf("|")).Trim(), hdn_qcno.Value.ToString(), Session["FINYEAR"].ToString(), this.ddlProductName.SelectedValue.Trim());
        gvqcentry.DataSource = dtproductionno;
        gvqcentry.DataBind();
        txtQCDate.Text = dtproductionno.Rows[0]["PODATE"].ToString();
        lblpotime.Text = dtproductionno.Rows[0]["POTIME"].ToString();
        dtproductionno = (DataTable)HttpContext.Current.Session["PRODUCTIONQTY"];
        gvqcentry.EnableViewState = true;
    }
    #endregion

    #region ClickEnent
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
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
            HttpContext.Current.Session["UPLOADFILENAME"] = null;
            gvqcentry.EnableViewState = true;
            divnewentry.Visible = false;
            btnadd.Visible = true;
            btncancel.Visible = true;
            btnsave.Visible = true;
            InputTable.Enabled = true;
            this.txtremarks.Text = "";
            fldAutoQcNumber.Style["display"] = "none";
            fldAutoQcNumberheader.Style["display"] = "none";
            InputTable.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            clsqualitycontrol.ResetDataTables();   // Reset all Datatables
            clsqualitycontrol.BindQCProduct();
            this.gvqcentry.DataSource = null;
            this.gvqcentry.DataBind();
            hdn_qcno.Value = "";
            hdn_guid.Value = "";
            hdn_tpuid.Value = "";
            txtqualitycontrolno.Text = "";
            divshow.Style["display"] = "none";
            chkfileupload.Checked = false;
            this.trproductdetails.Style["display"] = "";
            this.divbtnsave.Style["display"] = "";
            this.divqcdetails.Style["display"] = "";
            this.gvQCUpdate.Columns[18].Visible = true;

            //string lastvalue = string.Empty;

            foreach (ListItem item in ddlTPUName.Items)
            {
                if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "Blue");
                }
            }

            this.ddlponoNew.Items.Clear();
            this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
            this.ddlponoNew.AppendDataBoundItems = true;
            this.ddlponoNew.SelectedValue = "0";

            this.ddlpono.Items.Clear();
            this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
            this.ddlpono.AppendDataBoundItems = true;
            this.ddlpono.SelectedValue = "0";

            LoadTPUName();
            this.LoadProduct(this.ddlTPUName.SelectedValue.Trim());
            this.txtcasepack.Text = "";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlTPUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTPUName.SelectedValue == "0")
            {
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
                this.ddlProductName.SelectedValue = "0";

                this.gvqcentry.DataSource = null;
                this.gvqcentry.DataBind();
            }
            else
            {
                this.LoadProduct(this.ddlTPUName.SelectedValue.Trim());
                this.gvqcentry.DataSource = null;
                this.gvqcentry.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void LoadProduct(string TPUID)
    {
        ClsQualityAssurance clsQC = new ClsQualityAssurance();
        DataTable dt = new DataTable();
        dt = clsQC.LoadProduct(TPUID);
        if (dt.Rows.Count > 0)
        {
            this.ddlProductName.Items.Clear();
            this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
            this.ddlProductName.SelectedValue = "0";

            this.ddlProductName.DataSource = dt;
            this.ddlProductName.DataValueField = "ID";
            this.ddlProductName.DataTextField = "NAME";
            this.ddlProductName.DataBind();
        }
    }

    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductName.SelectedValue == "0")
            {
                this.ddlponoNew.Items.Clear();
                this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlponoNew.AppendDataBoundItems = true;

                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
                this.ddlpono.SelectedValue = "0";
            }
            else
            {
                if (this.ddlTPUName.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select TPU/Factory");
                    this.ddlTPUName.SelectedValue = "0";
                    return;
                }

                ClsQualityAssurance clsQC = new ClsQualityAssurance();
                DataTable dt = new DataTable();
                dt = clsQC.GetPU_QtyCombo(this.ddlProductName.SelectedValue, this.ddlTPUName.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim());
                List<QCdetails> QCDetails = new List<QCdetails>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QCdetails qc = new QCdetails();
                        qc.PUID = dt.Rows[i]["PUID"].ToString();
                        qc.PUNO = dt.Rows[i]["PUNO"].ToString();
                        qc.PRODUCTIONQTY = dt.Rows[i]["PRODUCTIONQTY"].ToString();
                        qc.QCQTY = dt.Rows[i]["QCQTY"].ToString();
                        qc.AVAILABLEQTY = dt.Rows[i]["REMAININGQTY"].ToString();
                        qc.BATCH = dt.Rows[i]["BATCHNO"].ToString();
                        qc.SAMPLEQTY = dt.Rows[i]["SAMPLEQTY"].ToString();
                        QCDetails.Add(qc);
                    }

                    //---------------------------------------------------------------------------------------------
                    ddlpono.Items.Clear();
                    ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}{4}{5}",
                    "PRODUCTIONNO".PadRight(25, '\u00A0'),
                    "PRODUCTIONQTY".PadRight(16, '\u00A0'),
                    "SAMPLEQTY".PadRight(10, '\u00A0'),
                    "QCQTY".PadRight(10, '\u00A0'),
                    "REMAININGQTY".PadRight(15, '\u00A0'),
                    "BATCHNO".PadRight(10, '\u00A0')
                   );

                    ddlpono.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                            //item.Attributes.CssStyle.Add("background-color", "Beige");
                        }
                    }

                    foreach (QCdetails p in QCDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}{5}",
                        p.PUNO.PadRight(25, '\u00A0'),
                        p.PRODUCTIONQTY.PadRight(16, '\u00A0'),
                        p.SAMPLEQTY.PadRight(10, '\u00A0'),
                        p.QCQTY.PadRight(10, '\u00A0'),
                        p.AVAILABLEQTY.PadRight(15, '\u00A0'),
                        p.BATCH.PadRight(10, '\u00A0'));

                        ddlpono.Items.Add(new ListItem(text, "" + p.PUID + ""));
                    }
                }
                else
                {
                    ddlpono.Items.Clear();
                    ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}{4}{5}",
                    "PRODUCTIONNO".PadRight(25, '\u00A0'),
                    "PRODUCTIONQTY".PadRight(16, '\u00A0'),
                    "SAMPLEQTY".PadRight(10, '\u00A0'),
                    "QCQTY".PadRight(10, '\u00A0'),
                    "REMAININGQTY".PadRight(15, '\u00A0'),
                    "BATCHNO".PadRight(10, '\u00A0'));

                    ddlpono.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                            //item.Attributes.CssStyle.Add("background-color", "Beige");
                        }
                    }

                    foreach (QCdetails p in QCDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}{5}",
                        p.PUNO.PadRight(25, '\u00A0'),
                        p.PRODUCTIONQTY.PadRight(16, '\u00A0'),
                        p.SAMPLEQTY.PadRight(10, '\u00A0'),
                        p.QCQTY.PadRight(10, '\u00A0'),
                        p.AVAILABLEQTY.PadRight(15, '\u00A0'),
                        p.BATCH.PadRight(10, '\u00A0'));

                        ddlpono.Items.Add(new ListItem(text, "" + p.PUID + ""));
                    }
                    MessageBox1.ShowInfo("<B>No Purchase Order is available with remaining quantity</B>", 60, 550);
                }
            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlponoNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlponoNew.SelectedValue == "0")
            {
                this.gvqcentry.DataSource = null;
                this.gvqcentry.DataBind();
            }
            else
            {
                if (this.ddlProductName.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Product");
                    this.ddlponoNew.SelectedValue = "0";
                    return;
                }
                else if (this.ddlpono.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Production No");
                    this.ddlponoNew.SelectedValue = "0";
                    return;
                }
                this.LoadQCGrid();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrdProduct_Click(object sender, EventArgs e)
    {
        try
        {
            string QCID = hdn_qcno.Value.ToString().Trim();
            this.txtPopUPQcDate.Text = hdn_QCDate.Value.ToString().Trim();
            this.txtPopUpQCNo.Text = hdn_QANo.Value.ToString().Trim();
            DataTable dtProductDetails = new DataTable();
            dtProductDetails = clsqualitycontrol.ShowProductDetails(QCID);
            if (dtProductDetails.Rows.Count > 0)
            {
                this.grdQCProductDetails.DataSource = dtProductDetails;
                this.grdQCProductDetails.DataBind();
            }
            else
            {
                this.grdQCProductDetails.DataSource = null;
                this.grdQCProductDetails.DataBind();
            }
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCloseLightbox_Click(object sender, EventArgs e)
    {
        try
        {
            this.grdQCProductDetails.DataSource = null;
            this.grdQCProductDetails.DataBind();
            this.txtPopUPQcDate.Text = "";
            this.txtPopUpQCNo.Text = "";
            this.light.Style["display"] = "none";
            this.fade.Style["display"] = "none";
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
            DataTable dtqcdetails = new DataTable();
            this.gvqcentry.EnableViewState = false;
            this.gvqcentry.ClearPreviousDataSource();
            this.gvqcentry.DataSource = null;
            this.gvqcentry.DataBind();
            string qcID = hdn_qcno.Value.ToString();
            string Verify = "";
            Verify = clsqualitycontrol.CheckQAVerify(qcID);
            if (Verify == "Y")
            {
                divbtnsave.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                trproductdetails.Style["display"] = "none";
                divqcdetails.Style["display"] = "none";
            }
            else
            {
                divbtnsave.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                trproductdetails.Style["display"] = "";
                divqcdetails.Style["display"] = "";
            }

            #region QueryString                
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divnewentry.Visible = false;
                this.trproductdetails.Visible = false;
                this.divqcdetails.Visible = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divnewentry.Visible = true;
                this.trproductdetails.Visible = true;
                this.divqcdetails.Visible = true;
            }
            #endregion

            dtqcdetails = clsqualitycontrol.BindQCProductBasedOnQC(qcID);
            if (dtqcdetails.Rows.Count > 0)
            {
                gvQCUpdate.DataSource = dtqcdetails;
                gvQCUpdate.DataBind();

                LoadTPUName();
                this.ddlTPUName.SelectedValue = Convert.ToString(dtqcdetails.Rows[0]["TPUID"]);
                if (dtqcdetails.Rows[0]["FILEUPLOADTAG"].ToString() == "Y")
                {
                    chkfileupload.Checked = true;
                    divshow.Style["display"] = "";
                }
                else
                {
                    chkfileupload.Checked = false;
                    divshow.Style["display"] = "none";
                }
                this.ddlponoNew.Items.Clear();
                this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlponoNew.AppendDataBoundItems = true;
                this.ddlponoNew.SelectedValue = "0";

                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
                this.ddlpono.SelectedValue = "0";

                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
                this.ddlProductName.AppendDataBoundItems = true;
                this.ddlProductName.SelectedValue = "0";

                this.LoadProduct(this.ddlTPUName.SelectedValue.Trim());
                this.txtQCDate.Text = Convert.ToString(dtqcdetails.Rows[0]["QCDATE"]).Trim();
                this.txtqualitycontrolno.Text = Convert.ToString(dtqcdetails.Rows[0]["QCNO"]).Trim();
                this.txtremarks.Text = Convert.ToString(dtqcdetails.Rows[0]["REMARKS"]).Trim();
                this.txtQCRefNo.Text = Convert.ToString(dtqcdetails.Rows[0]["QCREFNO"]).Trim();
                lblpotime.Text = Convert.ToString(dtqcdetails.Rows[0]["POTIME"]).Trim();

                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                this.txtrejectedcasepack.Text = Convert.ToString(TotalRejectedCasePack());
            }
            divnewentry.Visible = false;
            ddlTPUName.Enabled = false;

            this.btnadd.Visible = true;
            this.btncancel.Visible = true;
            this.btnsave.Visible = true;
            this.InputTable.Enabled = true;

            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.fldAutoQcNumber.Style["display"] = "";
            this.fldAutoQcNumberheader.Style["display"] = "";
            this.gvQCUpdate.Columns[18].Visible = true;
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
            HttpContext.Current.Session["UPLOADFILENAME"] = null;
            this.gvqcentry.EnableViewState = false;
            this.gvqcentry.ClearPreviousDataSource();
            this.gvqcentry.DataSource = null;
            this.gvqcentry.DataBind();
            string qcID = hdn_qcno.Value.ToString();
            DataTable dtqcdetails = new DataTable();
            dtqcdetails = clsqualitycontrol.BindQCProductBasedOnQC(qcID);
            if (dtqcdetails.Rows.Count > 0)
            {
                gvQCUpdate.DataSource = dtqcdetails;
                gvQCUpdate.DataBind();

                LoadTPUName();
                this.ddlTPUName.SelectedValue = Convert.ToString(dtqcdetails.Rows[0]["TPUID"]);
                if (dtqcdetails.Rows[0]["FILEUPLOADTAG"].ToString() == "Y")
                {
                    chkfileupload.Checked = true;
                    divshow.Style["display"] = "";
                }
                else
                {
                    chkfileupload.Checked = false;
                    divshow.Style["display"] = "none";
                }
                this.txtQCDate.Text = Convert.ToString(dtqcdetails.Rows[0]["QCDATE"]).Trim();
                this.txtqualitycontrolno.Text = Convert.ToString(dtqcdetails.Rows[0]["QCNO"]).Trim();
                this.txtremarks.Text = Convert.ToString(dtqcdetails.Rows[0]["REMARKS"]).Trim();
                this.txtQCRefNo.Text = Convert.ToString(dtqcdetails.Rows[0]["QCREFNO"]).Trim();

                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                this.txtrejectedcasepack.Text = Convert.ToString(TotalRejectedCasePack());
            }
            this.divnewentry.Visible = false;
            this.ddlTPUName.Enabled = false;

            this.btnadd.Visible = true;
            this.btncancel.Visible = true;
            this.btnsave.Visible = true;
            this.InputTable.Enabled = true;

            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.fldAutoQcNumber.Style["display"] = "";
            this.fldAutoQcNumberheader.Style["display"] = "";

            this.trproductdetails.Style["display"] = "none";
            this.divbtnsave.Style["display"] = "none";
            this.divqcdetails.Style["display"] = "none";
            this.gvQCUpdate.Columns[18].Visible = false;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btngrdqcdelete_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                MessageBox1.ShowError("Delete not allowed..!");
            }
            else
            {
                string Verify = "";
                Verify = clsqualitycontrol.CheckQAVerify(hdn_qcno.Value.ToString().Trim());
                if (Verify == "Y")
                {
                    MessageBox1.ShowError("Approval is done,not allow to delete.");
                }
                else
                {
                    flag = TotalQCRecordsDelete(hdn_qcno.Value.ToString().Trim());
                    if (flag == 1)
                    {
                        clsqualitycontrol.DeleteQC(hdn_qcno.Value.ToString().Trim());
                        this.LoadQCDetails();
                        MessageBox1.ShowSuccess("Record deleted Successfully!");
                    }
                    else if (flag == 2)
                    {
                        MessageBox1.ShowInfo("<b><font color='green'>Product's Despatch partially or fully done!</font></b>", 60, 400);
                    }
                    else
                    {
                        MessageBox1.ShowError("Record deleted unsuccessful!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            int flagadd = 0;
            DataTable dtproductionno = (DataTable)HttpContext.Current.Session["PRODUCTIONQTY"];
            DataTable dtEnterQCQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];

            for (int j = 0; j < dtEnterQCQty.Rows.Count; j++)
            {
                for (int i = 0; i < dtproductionno.Rows.Count; i++)
                {
                    GridDataControlFieldCell qc = gvqcentry.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                    TextBox txtcurrentqc = qc.FindControl("txtcurrentqcqty") as TextBox;

                    GridDataControlFieldCell rejqc = gvqcentry.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                    TextBox txtrejectionqc = rejqc.FindControl("txtrejectedqcqty") as TextBox;

                    decimal currentqcqty = Convert.ToDecimal(string.IsNullOrEmpty(txtcurrentqc.Text.Trim()) ? "0" : txtcurrentqc.Text.Trim());
                    decimal rejectionqcqty = Convert.ToDecimal(string.IsNullOrEmpty(txtrejectionqc.Text.Trim()) ? "0" : txtrejectionqc.Text.Trim());

                    decimal totalqty = currentqcqty + rejectionqcqty;

                    if (totalqty != 0 && dtEnterQCQty.Rows[j]["POID"].ToString() == dtproductionno.Rows[i]["POID"].ToString() && dtEnterQCQty.Rows[j]["PUID"].ToString() == dtproductionno.Rows[i]["PUID"].ToString() && dtEnterQCQty.Rows[j]["PRODUCTID"].ToString() == dtproductionno.Rows[i]["PRODUCTID"].ToString() && dtEnterQCQty.Rows[j]["BATCHNO"].ToString() == dtproductionno.Rows[i]["BATCHNO"].ToString())
                    {
                        MessageBox1.ShowInfo("<b>Same Batch is available in <font color='green'>" + dtproductionno.Rows[i]["PRODUCTNAME"] + "</font></b> !", 60, 680);

                        flag = 1;
                        break;
                    }
                }
            }

            for (int i = 0; i < gvqcentry.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell qc = gvqcentry.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                TextBox txtcurrentqc = qc.FindControl("txtcurrentqcqty") as TextBox;

                GridDataControlFieldCell rejqc = gvqcentry.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                TextBox txtrejectionqc = rejqc.FindControl("txtrejectedqcqty") as TextBox;

                GridDataControlFieldCell qcreason = gvqcentry.RowsInViewState[i].Cells[10] as GridDataControlFieldCell;
                DropDownList ddlreason = qcreason.FindControl("ddlreason") as DropDownList;

                string BatchNO = Convert.ToString(dtproductionno.Rows[i]["BATCHNO"].ToString());
                string ProductID = Convert.ToString(dtproductionno.Rows[i]["productid"].ToString());

                decimal productionqty = Convert.ToDecimal(dtproductionno.Rows[i]["PRODUCTIONQTY"].ToString());
                decimal alreadyqcqty = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTY"].ToString());
                decimal alreadyqcqtynotin = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"].ToString());
                decimal currentqcqty1 = Convert.ToDecimal(string.IsNullOrEmpty(txtcurrentqc.Text.Trim()) ? "0" : txtcurrentqc.Text.Trim());
                decimal rejectionqcqty = Convert.ToDecimal(string.IsNullOrEmpty(txtrejectionqc.Text.Trim()) ? "0" : txtrejectionqc.Text.Trim());

                decimal totalqty = currentqcqty1 + rejectionqcqty;
                decimal remainingqty = productionqty - (alreadyqcqtynotin + currentqcqty1 + rejectionqcqty);
                string reasonid = ddlreason.SelectedValue.ToString();
                string reasonname = ddlreason.SelectedItem.Text;

                if (totalqty != 0)
                {
                    if ((totalqty) > (productionqty - alreadyqcqtynotin))
                    {
                        MessageBox1.ShowInfo("Total QA qty should not be greater than production qty for <b><font>" + dtproductionno.Rows[i]["PRODUCTNAME"] + "</font></b>!", 80, 700);
                        flag = 1;
                        break;
                    }
                    else if (rejectionqcqty > 0 && reasonid == "0")
                    {
                        MessageBox1.ShowInfo("Please select rejection qty reason for <b><font>" + dtproductionno.Rows[i]["PRODUCTNAME"] + "</font></b>!", 80, 700);
                        flag = 1;
                        break;
                    }
                    else if (rejectionqcqty == 0 && reasonid != "0")
                    {
                        MessageBox1.ShowInfo("Please select rejection reason <b>None</b> for rejection qty 0(zero)! ", 80, 500);
                        flag = 1;
                        break;
                    }
                }
            }

            if (flag == 0)
            {
                for (int i = 0; i < gvqcentry.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell qc = gvqcentry.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                    TextBox txtcurrentqc = qc.FindControl("txtcurrentqcqty") as TextBox;

                    GridDataControlFieldCell rejqc = gvqcentry.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                    TextBox txtrejectionqc = rejqc.FindControl("txtrejectedqcqty") as TextBox;

                    GridDataControlFieldCell qcreason = gvqcentry.RowsInViewState[i].Cells[10] as GridDataControlFieldCell;
                    DropDownList ddlreason = qcreason.FindControl("ddlreason") as DropDownList;

                    string BatchNO = Convert.ToString(dtproductionno.Rows[i]["BATCHNO"].ToString());
                    string ProductID = Convert.ToString(dtproductionno.Rows[i]["productid"].ToString());

                    decimal productionqty = Convert.ToDecimal(dtproductionno.Rows[i]["PRODUCTIONQTY"].ToString());
                    decimal alreadyqcqty = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTY"].ToString());
                    decimal alreadyqcqtynotin = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"].ToString());
                    decimal currentqcqty1 = Convert.ToDecimal(string.IsNullOrEmpty(txtcurrentqc.Text.Trim()) ? "0" : txtcurrentqc.Text.Trim());
                    decimal rejectionqcqty = Convert.ToDecimal(string.IsNullOrEmpty(txtrejectionqc.Text.Trim()) ? "0" : txtrejectionqc.Text.Trim());

                    decimal totalqty = currentqcqty1 + rejectionqcqty;
                    decimal remainingqty = productionqty - (alreadyqcqtynotin + currentqcqty1);
                    string reasonid = ddlreason.SelectedValue.ToString();
                    string reasonname = ddlreason.SelectedItem.Text;

                    if (totalqty != 0)
                    {
                        if ((currentqcqty1 + rejectionqcqty) > (productionqty - alreadyqcqtynotin))
                        {
                            MessageBox1.ShowInfo("QA qty should not be greater than production qty for <b><font color='green'>" + dtproductionno.Rows[i]["PRODUCTNAME"] + "</font></b> !", 8, 650);

                            flag = 1;
                            break;
                        }
                        else
                        {
                            if (flag == 0)
                            {
                                DataRow dr = dtEnterQCQty.NewRow();

                                dr["GUID"] = dtproductionno.Rows[i]["GUID"].ToString();
                                dr["POID"] = dtproductionno.Rows[i]["POID"].ToString();
                                dr["PONO"] = dtproductionno.Rows[i]["PONO"].ToString();
                                dr["PUNO"] = dtproductionno.Rows[i]["PUNO"].ToString();
                                dr["PUID"] = dtproductionno.Rows[i]["PUID"].ToString();
                                dr["PRODUCTID"] = dtproductionno.Rows[i]["PRODUCTID"].ToString();
                                dr["PRODUCTNAME"] = dtproductionno.Rows[i]["PRODUCTNAME"].ToString();
                                dr["BATCHNO"] = dtproductionno.Rows[i]["BATCHNO"].ToString();
                                dr["PACKINGSIZEID"] = dtproductionno.Rows[i]["PACKINGSIZEID"].ToString();
                                dr["PACKINGSIZENAME"] = dtproductionno.Rows[i]["PACKINGSIZENAME"].ToString();
                                dr["PRODUCTIONQTY"] = dtproductionno.Rows[i]["PRODUCTIONQTY"].ToString();
                                dr["ALREADYQCQTY"] = dtproductionno.Rows[i]["ALREADYQCQTYNOT"].ToString();
                                dr["CURRENTQCQTY"] = currentqcqty1;
                                dr["REJECTEDQCQTY"] = rejectionqcqty;
                                dr["REMAININGQCQTY"] = Convert.ToDecimal(dtproductionno.Rows[i]["PRODUCTIONQTY"]) - (Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"]) + currentqcqty1 + rejectionqcqty);
                                //dr["TOTALQCQTY"] = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"]) + Convert.ToDecimal(txtcurrentqc.Text.Trim()) + Convert.ToDecimal(txtrejectionqc.Text.Trim()) + (Convert.ToDecimal(dtproductionno.Rows[i]["PRODUCTIONQTY"]) - (Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"]) + Convert.ToDecimal(txtcurrentqc.Text.Trim() + Convert.ToDecimal(txtrejectionqc.Text.Trim()))));
                                dr["TOTALQCQTY"] = Convert.ToDecimal(dtproductionno.Rows[i]["ALREADYQCQTYNOT"]) + currentqcqty1 + rejectionqcqty;
                                dr["REASONID"] = reasonid;
                                dr["REASONNAME"] = reasonname;

                                dtEnterQCQty.Rows.Add(dr);

                                HttpContext.Current.Session["ENTERQCQTY"] = dtEnterQCQty;
                                HttpContext.Current.Session["PRODUCTIONQTY"] = dtproductionno;

                                gvQCUpdate.DataSource = dtEnterQCQty;
                                gvQCUpdate.DataBind();

                                gvqcentry.EnableViewState = false;
                                gvqcentry.ClearPreviousDataSource();
                                gvqcentry.DataSource = null;
                                gvqcentry.DataBind();

                                ddlTPUName.Enabled = false;

                                this.ddlProductName.SelectedValue = "0";
                                this.ddlponoNew.SelectedValue = "0";
                                this.ddlpono.SelectedValue = "0";

                                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                                this.txtrejectedcasepack.Text = Convert.ToString(TotalRejectedCasePack());

                                flagadd = 1;
                            }
                        }
                    }
                }

                if (flagadd == 0)
                {
                    MessageBox1.ShowInfo("<b>Please check QA details</b>");
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

    #region delete
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtEnterQCQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
            int flag = 0;
            string guid = hdn_guid.Value.ToString();
            string qcno = hdn_qcno.Value.ToString();
            flag = QCRecordsDelete(guid, qcno);

            if (flag == 1)
            {
                gvQCUpdate.DataSource = dtEnterQCQty;
                gvQCUpdate.DataBind();

                MessageBox1.ShowSuccess("Record deleted Successfully!");

                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                this.txtrejectedcasepack.Text = Convert.ToString(TotalRejectedCasePack());
            }
            else if (flag == 2)
            {
                gvQCUpdate.DataSource = dtEnterQCQty;
                gvQCUpdate.DataBind();

                MessageBox1.ShowInfo("<b><font color='green'>Product's Despatch partially or fully done!</font></b>", 60, 400);
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
    #endregion

    public int QCRecordsDelete(string GUID, string QCID)
    {
        int delflag = 0;
        DataTable dtQCRecord = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];

        int i = dtQCRecord.Rows.Count - 1;
        while (i >= 0)
        {
            if (Convert.ToString(dtQCRecord.Rows[i]["GUID"]) == GUID)
            {
                if (string.IsNullOrEmpty(QCID))
                {
                    dtQCRecord.Rows[i].Delete();
                    dtQCRecord.AcceptChanges();
                    delflag = 1;
                    break;
                }
                else
                {
                    string productid = dtQCRecord.Rows[i]["PRODUCTID"].ToString().Trim();
                    string batchno = dtQCRecord.Rows[i]["BATCHNO"].ToString().Trim();
                    string poid = dtQCRecord.Rows[i]["POID"].ToString().Trim();
                    decimal despatchqty = clsqualitycontrol.CheckRemainingDespatchQy(productid, batchno, poid, QCID);
                    if (despatchqty == 0)
                    {
                        dtQCRecord.Rows[i].Delete();
                        dtQCRecord.AcceptChanges();
                        delflag = 1;
                        break;
                    }
                    else
                    {
                        delflag = 2;
                        break;
                    }
                }
            }
            i--;
        }
        HttpContext.Current.Session["ENTERQCQTY"] = dtQCRecord;
        return delflag;
    }

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

    #region Conver_To_ISO
    public string ConvertDate(string dt)
    {
        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + month + day;
        return dt;

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
                if (this.hdn_qcno.Value.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmFACTORYQCFileUpload.aspx?QCID=" + hdn_qcno.Value.Trim() + ""
                    + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmFACTORYQCFileUpload.aspx?QCID= "
                    + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                divshow.Style["display"] = "";
            }
            else
            {
                divshow.Style["display"] = "none";
                clsqualitycontrol.FileDelete(hdn_qcno.Value.ToString());
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
            if (this.hdn_qcno.Value.Trim() != "")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmFACTORYQCFileUpload.aspx?QCID=" + hdn_qcno.Value.Trim() + ""
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmFACTORYQCFileUpload.aspx?QCID= "
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

    #region Save
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string QCNo = string.Empty;
            string puid = string.Empty;
            int flag = 0;

            DataTable dtEnterQCQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];

            if (dtEnterQCQty.Rows.Count > 0)
            {
                for (int i = 0; i < dtEnterQCQty.Rows.Count; i++)
                {
                    puid = puid + "'" + dtEnterQCQty.Rows[i]["PUID"].ToString() + "'" + ",";
                }
                puid = puid.Substring(0, puid.Length - 1);
                DataTable dtpudate = clsqualitycontrol.BindMaxProductionUpdateDate(puid);
                int pudate = Convert.ToInt32(dtpudate.Rows[0]["PUDATE"].ToString());
                int dtqcdate = Convert.ToInt32(ConvertDate(txtQCDate.Text.Trim()));

                if (pudate > dtqcdate)
                {
                    //string message = "alert('QC date can not less than Production Update date : " + dtpudate.Rows[0]["SHOWPUDATE"].ToString() + "!')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowInfo("<b><font color='red'>QA date</font></b> can not be less than <b><font color='green'>Production Update date</b></font>", 100, 500);
                }
                else
                {
                    string xml = ConvertDatatableToXML(dtEnterQCQty);
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
                        clsqualitycontrol.FileDelete(hdn_qcno.Value.ToString());
                    }

                    if (chkfileupload.Checked == true)
                    {
                        if (dtupload != null && dtupload.Rows.Count > 0)
                        {
                            flag = 0;
                        }
                        else
                        {
                            MessageBox1.ShowInfo("<b>You have checked document upload but no document found!</b>", 60, 530);
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        QCNo = clsqualitycontrol.InsertQCDetails(hdn_qcno.Value.ToString().Trim(),
                                                                    this.ddlTPUName.SelectedValue.Trim(),
                                                                    this.ddlTPUName.SelectedItem.Text.Trim(),
                                                                    HttpContext.Current.Session["USERID"].ToString().Trim(),
                                                                    HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                    this.txtQCDate.Text.Trim(), xml, fileuploadtag,
                                                                    Convert.ToDecimal(this.txtcasepack.Text.Trim()),
                                                                    Convert.ToDecimal(this.txtrejectedcasepack.Text.Trim()),
                                                                    this.txtremarks.Text.Trim(), xmlupload, this.txtQCRefNo.Text.Trim()
                                                                 );

                        if (QCNo != "")
                        {

                            if (Convert.ToString(hdn_tpuid.Value.Trim()) == "")
                            {
                                MessageBox1.ShowSuccess("Quality Assurance No : <b><font color='green'>  " + QCNo + "</b>!</font> Saved Successfully", 60, 600);
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("Quality Assurance No : <b><font color='green'>  " + QCNo + "</b>!</font> Updated Successfully", 60, 600);
                            }
                            Close();
                            txtqualitycontrolno.Text = QCNo;
                            fldAutoQcNumber.Style["display"] = "";
                            fldAutoQcNumberheader.Style["display"] = "";
                            LoadQCDetails();

                            btnadd.Visible = false;
                            btnsave.Visible = false;
                            btncancel.Visible = false;
                            InputTable.Enabled = false;
                            this.Reset();
                            ClearDataTable();
                        }
                        else
                        {
                            MessageBox1.ShowError("Error on Saving record!");
                        }
                    }
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please ensure atleast 1 accepted QA product!</b>", 60, 450);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void gvqcentry_RowDataBound(object sender, GridRowEventArgs e)
    {
        if (e.Row.RowType == GridRowType.DataRow || gvqcentry.RowsInViewState.Count > 0)
        {
            GridDataControlFieldCell cell2 = e.Row.Cells[10] as GridDataControlFieldCell;
            DropDownList ddlreason2 = cell2.FindControl("ddlreason") as DropDownList;

            ddlreason2.Items.Clear();
            ddlreason2.Items.Add(new ListItem("None", "0"));
            ddlreason2.AppendDataBoundItems = true;
            ClsQualityAssurance clsqualitycontrol = new ClsQualityAssurance();
            ddlreason2.DataSource = clsqualitycontrol.BindReason(Request.QueryString["MENUID"].ToString());
            ddlreason2.DataValueField = "ID";
            ddlreason2.DataTextField = "DESCRIPTION";
            ddlreason2.DataBind();

            if (gvqcentry.RowsInViewState.Count > e.Row.RowIndex)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
                TextBox textBox = cell.FindControl("txtcurrentqcqty") as TextBox;

                GridDataControlFieldCell cell3 = e.Row.Cells[8] as GridDataControlFieldCell;
                TextBox rejtextBox = cell3.FindControl("txtrejectedqcqty") as TextBox;

                GridDataControlFieldCell cell1 = e.Row.Cells[10] as GridDataControlFieldCell;
                DropDownList ddlreason = cell1.FindControl("ddlreason") as DropDownList;

                if (gvqcentry.RowsInViewState != null)
                {
                    if (gvqcentry.RowsInViewState[e.Row.RowIndex].Cells.Count > 6)
                    {
                        GridDataControlFieldCell cellInViewState = gvqcentry.RowsInViewState[e.Row.RowIndex].Cells[7] as GridDataControlFieldCell;
                        TextBox textBoxInViewState = cellInViewState.FindControl("txtcurrentqcqty") as TextBox;
                        textBox.Text = textBoxInViewState.Text;
                    }
                    if (gvqcentry.RowsInViewState[e.Row.RowIndex].Cells.Count > 7)
                    {
                        GridDataControlFieldCell cellInViewState = gvqcentry.RowsInViewState[e.Row.RowIndex].Cells[8] as GridDataControlFieldCell;
                        TextBox textBoxInViewState1 = cellInViewState.FindControl("txtrejectedqcqty") as TextBox;
                        rejtextBox.Text = textBoxInViewState1.Text;
                    }
                    if (gvqcentry.RowsInViewState[e.Row.RowIndex].Cells.Count > 9)
                    {
                        GridDataControlFieldCell cellInViewState = gvqcentry.RowsInViewState[e.Row.RowIndex].Cells[10] as GridDataControlFieldCell;
                        DropDownList reasonInViewState = cellInViewState.FindControl("ddlreason") as DropDownList;
                        ddlreason.SelectedValue = reasonInViewState.SelectedValue.ToString();
                    }
                }
            }
        }
    }
    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlpono.SelectedValue == "0")
            {
                this.ddlponoNew.Items.Clear();
                this.ddlponoNew.SelectedValue = "0";
                this.gvqcentry.DataSource = null;
                this.gvqcentry.DataBind();
            }
            else
            {
                if (this.ddlProductName.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Product");
                    this.ddlpono.SelectedValue = "0";
                    return;
                }
                else if (this.ddlTPUName.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select TPU/Factory No");
                    this.ddlpono.SelectedValue = "0";
                    return;
                }

                ClsQualityAssurance clsQC = new ClsQualityAssurance();
                DataTable dt = new DataTable();
                dt = clsQC.LoadPurchaseOrder(this.ddlpono.SelectedValue.Substring(0, ddlpono.SelectedValue.IndexOf("|")).Trim(), this.ddlProductName.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    this.ddlponoNew.Items.Clear();
                    this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
                    this.ddlponoNew.SelectedValue = "0";
                    this.ddlponoNew.AppendDataBoundItems = true;
                    this.ddlponoNew.DataSource = dt;
                    this.ddlponoNew.DataValueField = "POID";
                    this.ddlponoNew.DataTextField = "PONO";
                    this.ddlponoNew.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public decimal TotalCasePack()
    {
        decimal casepack = 0;
        decimal convertioncasepack = 0;
        ClsInstructionSheet_FAC clsinstruction = new ClsInstructionSheet_FAC();

        DataTable dtEnterQCQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
        foreach (DataRow dr in dtEnterQCQty.Rows)
        {
            convertioncasepack = clsinstruction.GetPackingSize_OnCall(dr["PRODUCTID"].ToString().Trim(), dr["PACKINGSIZEID"].ToString().Trim(), Convert.ToDecimal(dr["CURRENTQCQTY"].ToString().Trim()));
            casepack = casepack + convertioncasepack;
        }
        return casepack;
    }

    public decimal TotalRejectedCasePack()
    {
        decimal casepack = 0;
        decimal convertioncasepack = 0;
        ClsInstructionSheet_FAC clsinstruction = new ClsInstructionSheet_FAC();

        DataTable dtEnterQCQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
        foreach (DataRow dr in dtEnterQCQty.Rows)
        {
            convertioncasepack = clsinstruction.GetPackingSize_OnCall(dr["PRODUCTID"].ToString().Trim(), dr["PACKINGSIZEID"].ToString().Trim(), Convert.ToDecimal(dr["REJECTEDQCQTY"].ToString().Trim()));
            casepack = casepack + convertioncasepack;
        }
        return casepack;
    }

    public int TotalQCRecordsDelete(string QCID)
    {
        ClsQualityAssurance clsqualitycontrol = new ClsQualityAssurance();
        int delflag = 0;
        DataTable dtQCRecord = clsqualitycontrol.TotalQCRecords(QCID);
        if (dtQCRecord.Rows.Count > 0)
        {
            for (int i = 0; i < dtQCRecord.Rows.Count; i++)
            {
                string productid = dtQCRecord.Rows[i]["PRODUCTID"].ToString().Trim();
                string batchno = dtQCRecord.Rows[i]["BATCHNO"].ToString().Trim();
                string poid = dtQCRecord.Rows[i]["POID"].ToString().Trim();

                decimal despatchqty = clsqualitycontrol.CheckRemainingDespatchQy(productid, batchno, poid, QCID);
                if (despatchqty == 0)
                {
                    delflag = 1;
                    continue;
                }
                else
                {
                    delflag = 2;
                    break;
                }
            }
        }
        return delflag;
    }
    /*Added By Avishek On 21-08-2017*/
    #region gvCurrentQcdetails_RowDataBound
    protected void gvCurrentQcdetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[9] as GridDataControlFieldCell;
                GridDataControlFieldCell cell6 = e.Row.Cells[6] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();
                if (status == "PENDING")
                {
                    cell.ForeColor = Color.Blue;
                }
                else if (status == "REJECTED")
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Green;
                }
                cell6.ForeColor = Color.Blue;
                cell6.HorizontalAlign = HorizontalAlign.Center;
                cell6.Font.Bold = true;
                cell6.Font.Size = 9;
            }
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
        Calendar1.StartDate = oDate;
        CalendarExtender1.StartDate = oDate;
        CalendarExtender5.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Calendar1.EndDate = today1;
            CalendarExtender1.EndDate = today1;
            CalendarExtender5.EndDate = today1;
        }
        else
        {
            this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            Calendar1.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
            CalendarExtender5.EndDate = cDate;
        }
    }
    #endregion

    #region Approve Section
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        try
        {
            string QANO = hdn_qcno.Value.ToString().Trim();
            int flag = 0;
            flag = clsqualitycontrol.UpdateQA(QANO);
            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.InputTable.Style["display"] = "none";
                fldAutoQcNumber.Style["display"] = "";
                fldAutoQcNumberheader.Style["display"] = "";
                this.LoadQCDetails();
                this.hdn_qcno.Value = "";
                MessageBox1.ShowSuccess("Quality Assurance No: <b><font color='green'>" + this.txtqualitycontrolno.Text + "</font></b> Approved Successfully.", 60, 550);

                DataTable DT = new DataTable();
                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                DT = clsMMPo.Bind_Sms_Mobno("5", QANO);
                foreach (DataRow row in DT.Rows)
                {
                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                }
                DataTable dt1 = new DataTable();
                dt1 = clsMMPo.Bind_Sms_Mobno("6", QANO);
            }
            else
            {
                MessageBox1.ShowError("Error on Approve Record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

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
    public void ItemLedger_QA(string QAID)
    {
        try
        {
            DataTable dtqcdetails = new DataTable();
            this.gvqcentry.EnableViewState = false;
            this.gvqcentry.ClearPreviousDataSource();
            this.gvqcentry.DataSource = null;
            this.gvqcentry.DataBind();
            string qcID = QAID;//hdn_qcno.Value.ToString();
            string Verify = "";
            Verify = clsqualitycontrol.CheckQAVerify(qcID);
            if (Verify == "Y")
            {
                divbtnsave.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                trproductdetails.Style["display"] = "none";
                divqcdetails.Style["display"] = "none";
            }
            else
            {
                divbtnsave.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                trproductdetails.Style["display"] = "";
                divqcdetails.Style["display"] = "";
            }

            #region QueryString                
            //Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Convert.ToString(Request.QueryString["CHECKER"]) == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divnewentry.Visible = false;
                this.trproductdetails.Visible = false;
                this.divqcdetails.Visible = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divnewentry.Visible = true;
                this.trproductdetails.Visible = true;
                this.divqcdetails.Visible = true;
            }
            #endregion

            dtqcdetails = clsqualitycontrol.BindQCProductBasedOnQC(qcID);
            if (dtqcdetails.Rows.Count > 0)
            {
                gvQCUpdate.DataSource = dtqcdetails;
                gvQCUpdate.DataBind();

                LoadTPUName();
                this.ddlTPUName.SelectedValue = Convert.ToString(dtqcdetails.Rows[0]["TPUID"]);
                if (dtqcdetails.Rows[0]["FILEUPLOADTAG"].ToString() == "Y")
                {
                    chkfileupload.Checked = true;
                    divshow.Style["display"] = "";
                }
                else
                {
                    chkfileupload.Checked = false;
                    divshow.Style["display"] = "none";
                }
                this.ddlponoNew.Items.Clear();
                this.ddlponoNew.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlponoNew.AppendDataBoundItems = true;
                this.ddlponoNew.SelectedValue = "0";

                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PRODUCTION NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
                this.ddlpono.SelectedValue = "0";

                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT", "0"));
                this.ddlProductName.AppendDataBoundItems = true;
                this.ddlProductName.SelectedValue = "0";

                this.LoadProduct(this.ddlTPUName.SelectedValue.Trim());
                this.txtQCDate.Text = Convert.ToString(dtqcdetails.Rows[0]["QCDATE"]).Trim();
                this.txtqualitycontrolno.Text = Convert.ToString(dtqcdetails.Rows[0]["QCNO"]).Trim();
                this.txtremarks.Text = Convert.ToString(dtqcdetails.Rows[0]["REMARKS"]).Trim();
                this.txtQCRefNo.Text = Convert.ToString(dtqcdetails.Rows[0]["QCREFNO"]).Trim();
                lblpotime.Text = Convert.ToString(dtqcdetails.Rows[0]["POTIME"]).Trim();

                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                this.txtrejectedcasepack.Text = Convert.ToString(TotalRejectedCasePack());
            }
            divnewentry.Visible = false;
            ddlTPUName.Enabled = false;

            this.btnadd.Visible = true;
            this.btncancel.Visible = true;
            this.btnsave.Visible = true;
            this.InputTable.Enabled = true;

            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.fldAutoQcNumber.Style["display"] = "";
            this.fldAutoQcNumberheader.Style["display"] = "";
            this.gvQCUpdate.Columns[18].Visible = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnreject_Click
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtremarks.Text != "")
            {
                string QANO = hdn_qcno.Value.ToString().Trim();
                int flag = 0;
                flag = clsqualitycontrol.RejectQA(QANO, txtremarks.Text);
                if (flag == 1)
                {
                    this.pnlDisplay.Style["display"] = "";
                    this.InputTable.Style["display"] = "none";
                    fldAutoQcNumber.Style["display"] = "";
                    fldAutoQcNumberheader.Style["display"] = "";
                    this.LoadQCDetails();
                    this.hdn_qcno.Value = "";
                }
                else
                {
                    MessageBox1.ShowError("Error on Approve Record..");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Rejection Remarks is Required..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
}