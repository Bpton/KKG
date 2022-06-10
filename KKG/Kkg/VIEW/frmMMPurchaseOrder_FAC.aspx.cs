using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow;
public partial class VIEW_frmMMPurchaseOrder_FAC : System.Web.UI.Page
{
    string Checker = string.Empty;
    string VENDORID = string.Empty;
    string PRODUCTID = string.Empty;
    string mode = string.Empty;
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    decimal CONVERTIONQTY = 0;
    DataTable dteditedporecord = new DataTable();
    DataSet dstax = new DataSet();
    ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
    ArrayList Arry = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region Checker Work
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                ViewState["Checker"] = Checker;

                if (Request.QueryString["MODE"] == "RQ")
                {
                    try
                    {
                        VENDORID = Request.QueryString["VendorID"].ToString().Trim();
                        DataTable DT = new DataTable();
                        DT = clsMMPo.BINDVENDORNAME(VENDORID);
                        if (DT.Rows.Count > 0)
                        {
                            LoadTPUNameAndCurrency();
                            this.ddlTPUName.SelectedValue = DT.Rows[0]["VENDORID"].ToString();
                            this.ddlTPUName.SelectedItem.Text = DT.Rows[0]["VENDORNAME"].ToString();
                            ddlProductName.Items.Clear();
                            ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
                            ddlProductName.AppendDataBoundItems = true;
                            ddlProductName.DataSource = clsMMPo.BindProduct(VENDORID);
                            ddlProductName.DataValueField = "ID";
                            ddlProductName.DataTextField = "NAME";
                            ddlProductName.DataBind();
                            ddlProductName.Enabled = true;
                        }
                        else
                        {
                            this.ddlTPUName.Enabled = true;
                            this.ddlTPUName.SelectedValue = "0";

                        }

                        PRODUCTID = Request.QueryString["ProductId"].ToString().Trim();
                        DataTable ProductDt = new DataTable();
                        ProductDt = clsMMPo.bindproductfromid(PRODUCTID);
                        if (ProductDt.Rows.Count > 0)
                        {

                            ddlProductName.Items.Clear();
                            ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
                            ddlProductName.AppendDataBoundItems = true;
                            ddlProductName.DataSource = clsMMPo.bindproductfromid(PRODUCTID);
                            ddlProductName.DataValueField = "ID";
                            ddlProductName.DataTextField = "PRODUCTALIAS";
                            ddlProductName.DataBind();
                            ddlProductName.Enabled = true;
                        }
                        else
                        {
                            this.ddlTPUName.Enabled = true;
                            this.ddlTPUName.SelectedValue = "0";

                        }
                        this.hdn_pofield.Value = "";
                        this.hdn_podelete.Value = "";
                        this.hdnCST.Value = "";
                        this.hdnAssesment.Value = "";
                        this.hdn_convertionqty.Value = "";
                        this.Hdn_Fld.Value = "";
                        this.hdnExcise.Value = "";
                        this.hdnMRP.Value = "";
                        ddlpackingsize.Enabled = true;
                        ddlProductName.Enabled = true;

                        clsMMPo.ResetDataTables();   // Reset all Datatables
                        clearTable();
                        InputTable.Style["display"] = "";
                        pnlDisplay.Style["display"] = "none";
                        divpono.Style["display"] = "none";
                        divadd.Style["display"] = "none";
                        tdlblrejection.Visible = false;
                        tdtxtrejection.Visible = false;
                        InputTable.Enabled = true;
                        imgPopuppodate.Visible = true;
                        clsMMPo.BindPurchaseOrderGrid();
                        this.TaxPercentage();
                        //this.txtrequireddate.Text = "";
                        //this.txttorequireddate.Text = "";
                        this.gvPurchaseOrder.ClearPreviousDataSource();
                        this.gvPurchaseOrder.DataSource = null;
                        this.gvPurchaseOrder.DataBind();

                        //this.gvindentdetails.ClearPreviousDataSource();
                        this.gvindentdetails.DataSource = null;
                        this.gvindentdetails.DataBind();

                        this.txtprice.Text = "";
                        this.txtqty.Text = "";
                        this.txtremarks.Text = "";
                        this.txtsaletax.Text = "0";
                        this.txtexercise.Text = "0";
                        this.txttotalamount.Text = "0";
                        this.txtTotalMRP.Text = "0";

                        this.txtnettotal.Text = "0";
                        this.txtgrosstotal.Text = "0";

                        txtQuotdt.Text = "";
                        txtrefno.Text = "";
                        this.btnsave.Enabled = true;
                        this.trdate.Visible = false;
                        this.trgvindentdetails.Visible = false;
                        this.divbtnapprove.Visible = false;
                        this.divbtnreject.Visible = false;
                        this.btnHold.Visible = false;
                        this.btnConfi.Visible = false;
                        this.divbtncancel.Visible = false;
                        this.LoadTermsConditions();
                        DataTable dt = new DataTable();
                        dt = clsMMPo.BindPODetailsBasedOnPONO("", "", "3");
                        txtTermsCondition.Text = dt.Rows[0]["TERMS"].ToString();
                    }
                    catch (Exception ex)
                    {
                        string message = "alert('" + ex.Message.Replace("'", "") + "')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }

                else if (Request.QueryString["MODE"] == "NOTIFY")
                {
                    this.contentarea.Style["display"] = "none";
                    string upath = string.Empty;
                    string tag = Request.QueryString["TAG"];
                    string POID = Request.QueryString["POID"].ToString().Trim();

                    upath = "frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + POID + "&&TAG=PO&&MenuId=29";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                }
                else
                {
                    if (Checker == "TRUE")
                    {
                        divadd.Style["display"] = "none";
                        tdlblrejection.Visible = true;
                        tdtxtrejection.Visible = true;
                        this.btnsubmitdiv.Visible = false;
                        this.divbtnapprove.Visible = true;
                        this.divbtnreject.Visible = true;
                        this.lblCheckerNote.Visible = false;
                        this.txtCheckerNote.Visible = false;
                    }
                    else
                    {
                        divadd.Style["display"] = "";
                        tdlblrejection.Visible = false;
                        tdtxtrejection.Visible = false;
                        this.btnsubmitdiv.Visible = true;
                        this.divbtnapprove.Visible = false;
                        this.divbtnreject.Visible = false;
                        this.lblCheckerNote.Visible = false;
                        this.txtCheckerNote.Visible = false;
                        this.btnConfi.Visible = false;
                        this.btnHold.Visible = false;
                    }
                    #endregion

                    //if (HttpContext.Current.Session["UserID"].ToString() == "4133" || HttpContext.Current.Session["UserID"].ToString() == "4137" || HttpContext.Current.Session["UserID"].ToString() == "37284") /*mayank and p.k verma*/
                    //{
                    //    this.ddlPoType.SelectedValue = "H";
                    //    divadd.Style["display"] = "none";
                    //    tdlblrejection.Visible = true;
                    //    tdtxtrejection.Visible = true;
                    //    this.btnsubmitdiv.Visible = false;
                    //    this.divbtnapprove.Visible = true;
                    //    this.divbtnreject.Visible = true;
                    //    this.btnHold.Visible = true;
                    //    this.lblCheckerNote.Visible = false;
                    //    this.txtCheckerNote.Visible = false;
                    //    this.btnConfi.Visible = false;
                    //}
                    //else if (HttpContext.Current.Session["UserID"].ToString() == "4135" || HttpContext.Current.Session["UserID"].ToString() == "3882" || HttpContext.Current.Session["UserID"].ToString() == "37241") /*for gla,vksarda and rksurana*/
                    //{
                    //    divadd.Style["display"] = "none";
                    //    tdlblrejection.Visible = true;
                    //    tdtxtrejection.Visible = true;
                    //    this.ddlPoType.SelectedValue = "C";
                    //    this.btnsubmitdiv.Visible = false;
                    //    this.divbtnapprove.Visible = true;
                    //    this.divbtnreject.Visible = true;
                    //    this.lblCheckerNote.Visible = false;
                    //    this.txtCheckerNote.Visible = false;
                    //    this.btnHold.Visible = false;
                    //    this.btnConfi.Visible = true;
                    //}
                    //else if (HttpContext.Current.Session["UserID"].ToString() == "3894" || HttpContext.Current.Session["UserID"].ToString() == "9038") /*for sktapariya and sunil Agarwal */
                    //{
                    //    divadd.Style["display"] = "none";
                    //    tdlblrejection.Visible = true;
                    //    tdtxtrejection.Visible = true;
                    //    this.ddlPoType.SelectedValue = "A";
                    //    this.btnsubmitdiv.Visible = false;
                    //    this.divbtnapprove.Visible = true;
                    //    this.divbtnreject.Visible = true;
                    //    this.lblCheckerNote.Visible = false;
                    //    this.txtCheckerNote.Visible = false;
                    //    this.btnHold.Visible = false;
                    //    this.btnConfi.Visible = false;
                    //}
                    //divpono.Style["display"] = "none";
                    InputTable.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    string menuID = Request.QueryString["MENUID"].ToString().Trim();
                    ViewState["menuID"] = menuID;
                    this.DateLock();

                    if (Request.QueryString["INDENT"].ToString().Trim() == "Y")
                    {
                        trproduct.Style["display"] = "none";
                        trdate.Style["display"] = "";
                        trgvindentdetails.Style["display"] = "";
                        tradd.Style["display"] = "none";
                        txtreqfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                        txtreqtodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        
                    }
                    else
                    {
                        this.DateLock();
                        trproduct.Style["display"] = "";
                        trdate.Style["display"] = "none";
                        trgvindentdetails.Style["display"] = "none";
                        tradd.Style["display"] = "";
                    }
                    this.txtlastrate.Text = "";
                    this.txtmaxrate.Text = "";
                    this.txtminrate.Text = "";
                    this.txtavgrate.Text = "";
                    ddlProductName.Enabled = false;
                    ddlpackingsize.Enabled = false;
                    LoadPO();
                    LoadPurchaseOrder();
                    LoadTPUNameAndCurrency();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadPO()
    {
        try
        {
            string QSTAG = string.Empty;
            if (Request.QueryString["INDENT"].ToString().Trim() == "Y")
            {
                QSTAG = "Y";
            }
            else
            {
                QSTAG = "N";
            }
            gvpodetails.DataSource = clsMMPo.BindPO(txtfromdate.Text, txttodate.Text, Session["FINYEAR"].ToString(), QSTAG, Checker, Session["DEPOTID"].ToString(), this.ddlPoType.SelectedValue.ToString(), HttpContext.Current.Session["UserID"].ToString());
            gvpodetails.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    public void LoadPurchaseOrder()
    {
        try
        {
            gvPurchaseOrder.DataSource = clsMMPo.BindPurchaseOrderGrid();
            gvPurchaseOrder.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadPOGrid
    public void LoadPOGrid()
    {
        try
        {
            gvPurchaseOrder.DataSource = clsMMPo.BindPOGrid(hdn_pofield.Value.ToString());
            gvPurchaseOrder.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTPUName
    public void LoadTPUNameAndCurrency()
    {
        try
        {
            VENDOR_AND_CURRENCY Obj = new VENDOR_AND_CURRENCY();
            Obj = clsMMPo.VendorWihtCurrency();

            ddlTPUName.Items.Clear();
            ddlTPUName.Items.Add(new ListItem("-- SELECT VENDOR NAME --", "0"));
            ddlTPUName.AppendDataBoundItems = true;
            ddlTPUName.DataSource = Obj.Vendorid;//clsMMPo.BindVendor();
            ddlTPUName.DataValueField = "VENDORID";
            ddlTPUName.DataTextField = "VENDORNAME";
            ddlTPUName.DataBind();

            ddlcurrencyname.Items.Clear();
            ddlcurrencyname.Items.Add(new ListItem("INR", "396C1D95-EF9A-4689-9FE0-9856714BA02E"));
            ddlcurrencyname.AppendDataBoundItems = true;
            ddlcurrencyname.DataSource = Obj.CURRENCY;//clsMMPo.LoadCurrencyName();
            ddlcurrencyname.DataValueField = "CURRENCYID";
            ddlcurrencyname.DataTextField = "CURRENCYTYPE";
            ddlcurrencyname.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlTPUName_SelectedIndexChanged
    protected void ddlTPUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTPUName.SelectedValue == "0")
            {
                ddlProductName.SelectedValue = "0";
                ddlpackingsize.SelectedValue = "0";
                txtqty.Text = "";
                txtprice.Text = "";
                txtSgstPer.Text = "";
                txtCgstPer.Text = "";
                txtIgstPer.Text = "";
                txtmaxrate.Text = "";
                txtlastrate.Text = "";
                txtavgrate.Text = "";
                txtminrate.Text = "";
                txtCgstId.Text = "";
                txtSgstId.Text = "";
                txtIgstId.Text = "";

                //txtrequireddate.Text = "";
                ddlProductName.Enabled = false;
                ddlpackingsize.Enabled = false;
            }
            else
            {
                ddlProductName.Items.Clear();
                ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
                ddlProductName.AppendDataBoundItems = true;
                ddlProductName.DataSource = clsMMPo.BindProduct(ddlTPUName.SelectedValue.ToString());
                ddlProductName.DataValueField = "ID";
                ddlProductName.DataTextField = "NAME";
                ddlProductName.DataBind();
                ddlProductName.Enabled = true;
                txtSgstPer.Text = "";
                txtCgstPer.Text = "";
                txtIgstPer.Text = "";
                txtmaxrate.Text = "";
                txtlastrate.Text = "";
                txtavgrate.Text = "";
                txtminrate.Text = "";
                txtCgstId.Text = "";
                txtSgstId.Text = "";
                txtIgstId.Text = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnnewentry_Click
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {

            if (Request.QueryString["MODE"] == "RQ")
            {
                VENDORID = Request.QueryString["VendorID"].ToString().Trim();
                DataTable DT = new DataTable();
                DT = clsMMPo.BINDVENDORNAME(VENDORID);
                if (DT.Rows.Count > 0)
                {
                    LoadTPUNameAndCurrency();
                    this.ddlTPUName.SelectedValue = DT.Rows[0]["VENDORID"].ToString();
                    this.ddlTPUName.SelectedItem.Text = DT.Rows[0]["VENDORNAME"].ToString();
                    ddlProductName.Items.Clear();
                    ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
                    ddlProductName.AppendDataBoundItems = true;
                    ddlProductName.DataSource = clsMMPo.BindProduct(VENDORID);
                    ddlProductName.DataValueField = "ID";
                    ddlProductName.DataTextField = "NAME";
                    ddlProductName.DataBind();
                    ddlProductName.Enabled = true;
                }
                else
                {
                    this.ddlTPUName.Enabled = true;
                    this.ddlTPUName.SelectedValue = "0";

                }

                PRODUCTID = Request.QueryString["ProductId"].ToString().Trim();
                DataTable ProductDt = new DataTable();
                ProductDt = clsMMPo.bindproductfromid(PRODUCTID);
                if (ProductDt.Rows.Count > 0)
                {

                    ddlProductName.Items.Clear();
                    ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
                    ddlProductName.AppendDataBoundItems = true;
                    ddlProductName.DataSource = clsMMPo.bindproductfromid(PRODUCTID);
                    ddlProductName.DataValueField = "ID";
                    ddlProductName.DataTextField = "PRODUCTALIAS";
                    ddlProductName.DataBind();
                    ddlProductName.Enabled = true;
                }
                else
                {
                    this.ddlTPUName.Enabled = true;
                    this.ddlTPUName.SelectedValue = "0";

                }
            }


            txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";
            this.hdnAssesment.Value = "";
            this.hdn_convertionqty.Value = "";
            this.Hdn_Fld.Value = "";
            this.hdnExcise.Value = "";
            this.hdnMRP.Value = "";
            ddlpackingsize.Enabled = true;
            ddlProductName.Enabled = true;

            clsMMPo.ResetDataTables();   // Reset all Datatables
            clearTable();
            InputTable.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            divpono.Style["display"] = "none";
            divadd.Style["display"] = "none";
            tdlblrejection.Visible = false;
            tdtxtrejection.Visible = false;
            InputTable.Enabled = true;
            imgPopuppodate.Visible = true;
            clsMMPo.BindPurchaseOrderGrid();
            this.TaxPercentage();
            //this.txtrequireddate.Text = "";
            //this.txttorequireddate.Text = "";
            this.gvPurchaseOrder.ClearPreviousDataSource();
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();

            //this.gvindentdetails.ClearPreviousDataSource();
            this.gvindentdetails.DataSource = null;
            this.gvindentdetails.DataBind();

            this.txtprice.Text = "";
            this.txtqty.Text = "";
            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";

            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";

            txtQuotdt.Text = "";
            txtrefno.Text = "";
            this.btnsave.Enabled = true;
            this.btnsubmitdiv.Style["display"] = "";
            this.LoadTermsConditions();
            DataTable dt = new DataTable();
            dt = clsMMPo.BindPODetailsBasedOnPONO("", "", "3");
            txtTermsCondition.Text = dt.Rows[0]["TERMS"].ToString();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region TaxPercentage
    protected void TaxPercentage()
    {
        DataTable dt = new DataTable();
        dt = clsMMPo.TaxPercentage();
        if (dt.Rows.Count > 0)
        {
            this.hdnExcise.Value = dt.Rows[0]["PERCENTAGE"].ToString();
            this.hdnCST.Value = dt.Rows[1]["PERCENTAGE"].ToString();
        }
    }
    #endregion

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnsave.Enabled = true;
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";
            this.hdnAssesment.Value = "";
            this.hdn_convertionqty.Value = "";
            this.Hdn_Fld.Value = "";
            this.hdnExcise.Value = "";
            this.hdnMRP.Value = "";
            ddlTPUName.SelectedValue = "0";
            ddlTPUName.Enabled = true;
            ddlProductName.SelectedValue = "0";
            txtremarks.Text = "";
            txtqty.Text = "";
            txtprice.Text = "";
           // txtrequireddate.Text = "";
            txtgrosstotal.Text = "0";
            txtadjustment.Text = "0";
            txtTotalMRP.Text = "0";

            txtpacking.Text = "0";
            txtsaletax.Text = "0";
            txtexercise.Text = "0";
            txtDiscAmnt.Text = "0";
            txtDiscPer.Text = "0";

            txttotalamount.Text = "0";
            txtnettotal.Text = "0";

            this.gvPurchaseOrder.ClearPreviousDataSource();
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            this.txtprice.Text = "";
            this.txtqty.Text = "";
            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";

            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";
            this.ddlTPUName.SelectedValue = "0";

            clsMMPo.ResetDataTables();   // Reset all Datatables
            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            if (ViewState["Checker"].ToString().Trim() == "TRUE")
            {
                divadd.Style["display"] = "none";
                tdlblrejection.Visible = true;
                tdtxtrejection.Visible = true;
            }
            else
            {
                divadd.Style["display"] = "";
                tdlblrejection.Visible = false;
                tdtxtrejection.Visible = false;
            }
            if (HttpContext.Current.Session["UserID"].ToString() == "4133" || HttpContext.Current.Session["UserID"].ToString() == "3894" || HttpContext.Current.Session["UserID"].ToString() == "9038" || HttpContext.Current.Session["UserID"].ToString() == "37284")
            {
                divadd.Style["display"] = "none";
                tdlblrejection.Visible = true;
                tdtxtrejection.Visible = true;
            }
            divpono.Style["display"] = "none";
            LoadPO();
            clearTable();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Conver_To_ISO
    public string Conver_To_ISO(string dt)
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

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDecimal(txtqty.Text.Trim()) != 0)
            {
                int dtpofromdate = Convert.ToInt32(Conver_To_ISO(this.txtrequireddate.Text.Trim()));
                int dtpotodate = Convert.ToInt32(Conver_To_ISO(this.txttorequireddate.Text.Trim()));
                int dtpoEntrydate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));
                string MRPValue = string.Empty;
                if (dtpofromdate < dtpoEntrydate)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Delivery from date</font></b> can not be less than <b><font color='green'>PO Entry Date</font></b>", 60, 480);
                }
                else if (dtpofromdate > dtpotodate)
                {

                    MessageBox1.ShowInfo("<b><font color='red'>Delivery to date</font></b> can not be less than <b><font color='green'>delivery from Date</font></b>", 60, 480);
                }
                else
                {
                    int flag = clsMMPo.PORecordsCheck(ddlProductName.SelectedValue.ToString(), ddlpackingsize.SelectedValue, txtrequireddate.Text, txttorequireddate.Text, hdn_pofield.Value.ToString());
                    if (flag == 0)
                    {
                        if (Convert.ToDecimal(this.txtprice.Text) == 0)
                        {
                            MessageBox1.ShowWarning("Cannot Add Product With Zero Rate");
                            return;
                        }

                        string pid = ddlProductName.SelectedValue.ToString().Trim();
                        //------------------ Convert to in PCS ----------------------
                        hdn_convertionqty.Value = Convert.ToString(clsMMPo.ConvertionQty(pid, ddlpackingsize.SelectedValue, txtqty.Text.Trim(), Convert.ToDecimal(this.txtprice.Text.Trim())));
                        MRPValue = Convert.ToString(clsMMPo.ProductMRPTotal(pid, ddlpackingsize.SelectedValue, txtqty.Text.Trim(), Convert.ToDecimal(this.hdnMRP.Value)));
                        if (Convert.ToString(txtlastrate.Text) == "")
                        {
                            this.txtlastrate.Text = "0";
                        }
                        if (Convert.ToString(txtmaxrate.Text) == "")
                        {
                            this.txtmaxrate.Text = "0";
                        }
                        if (Convert.ToString(txtminrate.Text) == "")
                        {
                            this.txtminrate.Text = "0";
                        }
                        if (Convert.ToString(txtavgrate.Text) == "")
                        {
                            this.txtavgrate.Text = "0";
                        }
                        if (Convert.ToString(txtCgstPer.Text) == "")
                        {
                            this.txtCgstPer.Text = "0";
                        }
                        if (Convert.ToString(txtSgstPer.Text) == "")
                        {
                            this.txtSgstPer.Text = "0";
                        }
                        if (Convert.ToString(txtIgstPer.Text) == "")
                        {
                            this.txtIgstPer.Text = "0";
                        }
                        decimal cgstvalue = 0;
                        decimal sgstvalue = 0;
                        decimal igstvalue = 0;

                        if (Convert.ToDecimal(txtCgstPer.Text) == 0)
                        {
                            igstvalue = ((Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtprice.Text.Trim())) * Convert.ToDecimal(txtIgstPer.Text)) / 100;
                        }
                        else
                        {
                            cgstvalue = ((Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtprice.Text.Trim())) * Convert.ToDecimal(txtCgstPer.Text)) / 100;
                            sgstvalue = ((Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtprice.Text.Trim())) * Convert.ToDecimal(txtSgstPer.Text)) / 100;
                        }
                        if (hdn_pofield.Value.ToString() == "")
                        {
                            gvPurchaseOrder.DataSource = clsMMPo.BindPurchaseOrderGridRecords(ddlProductName.SelectedValue.ToString(), ddlProductName.SelectedItem.Text, Convert.ToDecimal(txtqty.Text), this.ddlpackingsize.SelectedValue.Trim(), this.ddlpackingsize.SelectedItem.ToString(), hdn_convertionqty.Value.ToString(), txtrequireddate.Text, txttorequireddate.Text, Convert.ToDecimal(txtprice.Text.Trim()), Convert.ToDecimal(hdnMRP.Value), Convert.ToDecimal(MRPValue), 0, Convert.ToDecimal(this.hdnExcise.Value), Convert.ToDecimal(this.hdnCST.Value), Convert.ToDecimal(txtlastrate.Text), Convert.ToDecimal(txtmaxrate.Text), Convert.ToDecimal(txtavgrate.Text), Convert.ToDecimal(txtminrate.Text), Convert.ToDecimal(txtCgstPer.Text),cgstvalue, this.txtCgstId.Text, Convert.ToDecimal(txtSgstPer.Text),sgstvalue, this.txtSgstId.Text, Convert.ToDecimal(txtIgstPer.Text),igstvalue, this.txtIgstId.Text);
                            gvPurchaseOrder.DataBind();

                        }
                        else
                        {
                            gvPurchaseOrder.DataSource = clsMMPo.BindPOEditedGridRecords(hdn_pofield.Value.ToString(), ddlProductName.SelectedValue.ToString(), ddlProductName.SelectedItem.Text, Convert.ToDecimal(txtqty.Text), hdn_convertionqty.Value.ToString(), txtrequireddate.Text, txttorequireddate.Text, Convert.ToDecimal(txtprice.Text.Trim()), Convert.ToDecimal(this.hdnMRP.Value), Convert.ToDecimal(MRPValue), 0, Convert.ToDecimal(this.hdnExcise.Value), Convert.ToDecimal(this.hdnCST.Value), this.ddlpackingsize.SelectedValue.Trim(), this.ddlpackingsize.SelectedItem.ToString().Trim(), Convert.ToDecimal(txtlastrate.Text), Convert.ToDecimal(txtmaxrate.Text), Convert.ToDecimal(txtavgrate.Text), Convert.ToDecimal(txtminrate.Text), Convert.ToDecimal(txtCgstPer.Text), cgstvalue,this.txtCgstId.Text, Convert.ToDecimal(txtSgstPer.Text), sgstvalue, this.txtSgstId.Text, Convert.ToDecimal(txtIgstPer.Text), igstvalue, this.txtIgstId.Text);
                            gvPurchaseOrder.DataBind();
                        }

                        DataTable dtPOValue = clsMMPo.BindPOValues(hdn_pofield.Value.ToString());

                        decimal grossvalue = Convert.ToDecimal(dtPOValue.Rows[0]["TOTAL"]);
                        txtgrosstotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["GROSSTOTAL"]), 2));
                        txtTotalMRP.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["MRPTOTAL"]), 2));
                        txtsaletax.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["CSTTOTAL"]), 2));
                        txtexercise.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["EXCISETOTAL"]), 2));
                        //txttotalamount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["TOTAL"]), 2));
                        txtCgstValue.Text= Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["CGSTVALUE"]), 2));
                        decimal cgst = Convert.ToDecimal(dtPOValue.Rows[0]["CGSTVALUE"]);/*new add for taxamnt cal*/
                        txtSgstValue.Text= Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["SGSTVALUE"]), 2));
                        decimal sgst = Convert.ToDecimal(dtPOValue.Rows[0]["SGSTVALUE"]);/*new add for taxamnt cal*/
                        txtIgstValue.Text= Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["IGSTVALUE"]), 2));
                        decimal igst = Convert.ToDecimal(dtPOValue.Rows[0]["IGSTVALUE"]);/*new add for taxamnt cal*/

                        if (txtIgstValue.Text == "0.00")
                        {
                            txttotalamount.Text = Convert.ToString(grossvalue + cgst + sgst);
                        }
                        else
                        {
                            txttotalamount.Text = Convert.ToString(grossvalue + igst);
                        }
                        

                        ddlProductName.SelectedValue = "0";
                        ddlpackingsize.SelectedValue = "0";
                        txtqty.Text = "";
                        //txtrequireddate.Text = "";
                        //txttorequireddate.Text = "";
                        txtprice.Text = "";
                        this.txtlastrate.Text = "";
                        this.txtmaxrate.Text = "";
                        this.txtminrate.Text = "";
                        this.txtavgrate.Text = "";
                        this.txtCgstPer.Text = "";
                        this.txtSgstPer.Text = "";
                        this.txtIgstPer.Text = "";
                        this.txtCgstId.Text = "";
                        this.txtSgstId.Text = "";
                        this.txtIgstId.Text = "";

                        ddlProductName.Focus();
                        DataTable dt = new DataTable();
                        if (hdn_pofield.Value.ToString() == "")
                        {
                            dt = (DataTable)HttpContext.Current.Session["PORECORDS"];
                        }
                        else
                        {
                            dt = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                        }
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                this.ddlTPUName.Enabled = false;
                            }
                            else
                            {
                                this.ddlTPUName.Enabled = true;
                            }
                        }
                    }
                    else if (flag == 1)
                    {
                        MessageBox1.ShowInfo("<b>This Product added with <font color='red'>same delivery date!</font></b>", 60, 450);
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b>You have already added this product, if you want to added again with different delivery date, <font color='green'>please select existing packsize!</font></b>", 60, 800);
                    }
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Order qty must be non zero(0) !</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlProductName_SelectedIndexChanged
    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductName.SelectedValue == "0")
            {
                txtprice.Text = "";
                this.hdnAssesment.Value = "0";
                this.hdnMRP.Value = "0";
                ddlpackingsize.Enabled = false;
                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("-- SELECT UNIT --", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                ddlpackingsize.DataBind();
            }
            else
            {
                BindPackSize_MrpAndRate();
                this.producttpumapcheck();
            }
            txtqty.Text = "0.00";
           // txtrequireddate.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void BindPackSize_MrpAndRate()
    {
        PACKSIZE_MRP_AND_RATE Obj = new PACKSIZE_MRP_AND_RATE();
        Obj = clsMMPo.PackSize_MrpWihtRate(ddlProductName.SelectedValue.ToString(), ddlTPUName.SelectedValue.ToString(), txtpodate.Text);
        List<MRP> Mrp = new List<MRP>();
        List<RATE> Rate = new List<RATE>();
        Mrp = Obj.Mrp;
        Rate = Obj.Rate;
        string _Mrp = Convert.ToString(Mrp[0].Mrp);
        string _Rate = Convert.ToString(Rate[0].Rate);

        if (ddlProductName.SelectedValue == "0")
        {
            txtprice.Text = "";
            this.hdnAssesment.Value = "0";
            this.hdnMRP.Value = "0";
            ddlpackingsize.Enabled = false;
            ddlpackingsize.Items.Clear();
            //ddlpackingsize.Items.Add(new ListItem("SELECT UNIT", "0"));
            ddlpackingsize.AppendDataBoundItems = true;
            ddlpackingsize.DataBind();
        }
        else
        {
            ddlpackingsize.Enabled = true;
            ddlpackingsize.Items.Clear();
            //ddlpackingsize.Items.Add(new ListItem("SELECT UNIT", "0"));
            ddlpackingsize.AppendDataBoundItems = true;
            ddlpackingsize.DataSource = Obj.PackSize;//clsMMPo.BindPackingSize(this.ddlProductName.SelectedValue.Trim());
            ddlpackingsize.DataValueField = "PACKSIZEID_FROM";
            ddlpackingsize.DataTextField = "PACKSIZEName_FROM";
            ddlpackingsize.DataBind();

            this.hdnMRP.Value = _Mrp;
            this.txtprice.Text = _Rate;
        }
    }
    public void producttpumapcheck()
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = clsMMPo.checkproduct(ddlProductName.SelectedValue.ToString(), Session["DEPOTID"].ToString(), "1");
            dt1 = clsMMPo.checkproduct(ddlProductName.SelectedValue.ToString(), Session["DEPOTID"].ToString(), "2");
            if (dt.Rows.Count > 0)
            {
                this.MaxMinAvgLastRate();
                this.TaxPercentageForFactory();
                this.txtqty.Enabled = true;
            }
            else
            {
                MessageBox1.ShowWarning("<b><font color='red'> Product Not Mapped With " + " : " + dt1.Rows[0]["VENDORNAME"].ToString() + " Cannot Add this Prodcut</font></b> ", 40, 550);
                this.txtqty.Enabled = false;
                txtSgstPer.Text = "";
                txtCgstPer.Text = "";
                txtIgstPer.Text = "";
                txtmaxrate.Text = "";
                txtlastrate.Text = "";
                txtavgrate.Text = "";
                txtminrate.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }

    }


    public void MaxMinAvgLastRate()  /*max avg min and last rate*/
    {
        DataSet ds = new DataSet();
        ds = clsMMPo.BindAllRate(ddlProductName.SelectedValue.ToString(), ddlTPUName.SelectedValue.ToString(), this.txtpodate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.txtlastrate.Text = String.Format("{0:0.00}", ds.Tables[0].Rows[0]["LASTRATE"].ToString());
        }
        else
        {
            this.txtlastrate.Text = "";
            this.txtmaxrate.Text = "";
            this.txtminrate.Text = "";
            this.txtavgrate.Text = "";
            //MessageBox1.ShowInfo("There is no details found in Previous Purchase");
            

        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            this.txtmaxrate.Text = String.Format("{0:0.00}", ds.Tables[1].Rows[0]["MAXRATE"].ToString());
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            this.txtminrate.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["MINRATE"].ToString());
        }
        if (ds.Tables[3].Rows.Count > 0)
        {
            this.txtavgrate.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["AVGRATE"].ToString());
        }
    }


    public void TaxPercentageForFactory()  /*tax category wises*/
    {
        string depotid = Session["DEPOTID"].ToString();
        DataTable dttax = new DataTable();
        dttax = clsMMPo.BindTaxPercentage(ddlTPUName.SelectedValue.ToString(), ddlProductName.SelectedValue.ToString(), depotid);
        if (dttax.Rows.Count > 1)
        {
            txtCgstPer.Text = dttax.Rows[0]["PERCENTAGE"].ToString();
            txtCgstId.Text = dttax.Rows[0]["TAXID"].ToString();
            txtSgstPer.Text = dttax.Rows[1]["PERCENTAGE"].ToString();
            txtSgstId.Text = dttax.Rows[1]["TAXID"].ToString();
            this.txtIgstPer.Text = String.Format("{0:0.00}", 0.00);
            this.txtIgstId.Text = "";


        }
        else
        {
            txtIgstPer.Text = dttax.Rows[0]["PERCENTAGE"].ToString();
            txtIgstId.Text = dttax.Rows[0]["TAXID"].ToString();
            this.txtCgstPer.Text = String.Format("{0:0.00}", 0.00);
            this.txtSgstPer.Text = String.Format("{0:0.00}", 0.00);
            this.txtCgstId.Text = "";
            this.txtSgstId.Text = "";
        }


    }

    #region DeleteRecordPoDetails
    protected void DeleteRecordPoDetails(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string pono = e.Record["PONO"].ToString();
            string POID = e.Record["POID"].ToString();
            string Verify = "";
            int flag = 0;

            Verify = clsMMPo.CheckVerify(POID);
            if (Verify == "Y")
            {
                e.Record["Error"] = "Approve PO Can not be Deleted.";
            }
            else if (Verify == "H")
            {
                e.Record["Error"] = "Hold PO Can not be Deleted.";
            }
            else
            {
                flag = clsMMPo.POEditedRecordDELETE(e.Record["PONO"].ToString(), Session["FINYEAR"].ToString());
                if (flag == 1)
                {
                    LoadPO();
                    e.Record["Error"] = "Record Deleted Successfully!";
                }
                else if (flag == -1)
                {
                    e.Record["Error"] = "Delete unsuccessfull Because Pono is allready exists in GRN";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting!";
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

    #region Selected Indent
    public string SelectedIndent(ref DataTable dt)
    {
        string requisitionid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvindentdetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                Label lblINDENTID = (Label)gvr.FindControl("lblINDENTID");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lblINDENTID.Text.Trim() == dt.Rows[i]["INDENTID"].ToString().Trim())
                    {
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return requisitionid;
    }
    #endregion

    #region btngrdedit_Click
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            string Verify = "";
            string POID = hdn_pofield.Value.ToString();
            string PONO = hdn_pono.Value.ToString();
            DataTable depotid = new DataTable();

            depotid = clsMMPo.DepotidPoid(POID);
            if (depotid.Rows[0]["FACTORYID"].ToString() == "")
            {
                MessageBox1.ShowWarning("You Can't Edit This PO,It's Raise From HO Please Go to POP Purchase Order Menu For Approved");
                return;
            }

            Verify = clsMMPo.CheckVerify(POID);
            if (Verify == "N") {
                divbtnapprove.Style["display"] = "";
                divbtnreject.Style["display"] = "";
                btnsubmitdiv.Style["display"] = "";
            }
            else if (Verify == "Y")
            {
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
                btnsubmitdiv.Style["display"] = "none";
            }
            else if (Verify == "R")
            {
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
                btnsubmitdiv.Style["display"] = "";
            }

            //if (Verify == "N" && (HttpContext.Current.Session["UserID"].ToString() == "4133" || HttpContext.Current.Session["UserID"].ToString() == "4137" || HttpContext.Current.Session["UserID"].ToString() == "37284")) /*for MAYANK and PKVERMA*/
            //{
            //    divbtnapprove.Style["display"] = "";
            //    divbtnreject.Style["display"] = "";
            //    this.btnHold.Visible = false;

            //}
            //else if (Verify == "H" && (HttpContext.Current.Session["UserID"].ToString() == "4133" || HttpContext.Current.Session["UserID"].ToString() == "4137" || HttpContext.Current.Session["UserID"].ToString() == "37284")) /*for MAYANK and PKVERMA*/
            //{
            //    this.btnHold.Visible = true;
            //    divbtnreject.Style["display"] = "";
            //    divbtnapprove.Style["display"] = "none";
            //    tdlblrejection.Visible = true;
            //    tdtxtrejection.Visible = true;
            //}
            //else if (Verify == "C" && (HttpContext.Current.Session["UserID"].ToString() == "3882" || HttpContext.Current.Session["UserID"].ToString() == "4135" || HttpContext.Current.Session["UserID"].ToString() == "37241")) /*for VKSARDA and GLA*/
            //{
            //    this.btnHold.Visible = false;
            //    this.btnConfi.Visible = true;
            //    divbtnreject.Style["display"] = "";
            //    divbtnapprove.Style["display"] = "none";
            //    tdlblrejection.Visible = true;
            //    tdtxtrejection.Visible = true;
            //}
            //else if (Verify == "A" && (HttpContext.Current.Session["UserID"].ToString() == "3894" || HttpContext.Current.Session["UserID"].ToString() == "9038")) /*for SKTAPARIA and SAGAWRAL*/
            //{
            //    this.btnHold.Visible = false;
            //    this.btnConfi.Visible = false;
            //    divbtnreject.Style["display"] = "";
            //    divbtnapprove.Style["display"] = "";
            //    tdlblrejection.Visible = true;
            //    tdtxtrejection.Visible = true;
            //}
            //else if (Verify == "N" && (HttpContext.Current.Session["UserID"].ToString() == "3894" || HttpContext.Current.Session["UserID"].ToString() == "9038" || HttpContext.Current.Session["UserID"].ToString() == "3882" || HttpContext.Current.Session["UserID"].ToString() == "4135" || HttpContext.Current.Session["UserID"].ToString() == "37241")) /*for SKTAPARIA and SAGAWRAL*/
            //{
            //    this.btnHold.Visible = false;
            //    this.btnConfi.Visible = false;
            //    divbtnreject.Style["display"] = "none";
            //    divbtnapprove.Style["display"] = "none";
            //}
            //else if (Verify == "H" || Verify == "C" || Verify == "A" || Verify == "Y")
            //{
            //    btnsubmitdiv.Style["display"] = "none";
            //    divbtnapprove.Style["display"] = "none";
            //    divbtnreject.Style["display"] = "none";
            //    btnConfi.Style["display"] = "none";
            //    this.btnHold.Visible = false;
            //}
            //else
            //{
            //    btnsubmitdiv.Style["display"] = "";
            //    divbtnapprove.Style["display"] = "";
            //    divbtnreject.Style["display"] = "";
            //    tdlblrejection.Visible = false;
            //    tdtxtrejection.Visible = false;
            //}


            this.TaxPercentage();
            clsMMPo.BindPurchaseOrderGrid();
            ddlTPUName.Enabled = false;
            txtpodate.Enabled = false;
            txtpono.Text = PONO;
            ViewState["pono"] = PONO;
            divpono.Style["display"] = "";
            this.LoadTermsConditions();
            this.btnsave.Enabled = true;

            dteditedporecord = clsMMPo.BindPODetailsBasedOnPONO(POID, Session["FINYEAR"].ToString());
            txtTermsCondition.Text = dteditedporecord.Rows[0]["TERMSCONDITION"].ToString();
            txtrefno.Text = dteditedporecord.Rows[0]["REFRENCENO"].ToString();
            txtQuotdt.Text = dteditedporecord.Rows[0]["QUOTDATE"].ToString();
            txtpodate.Text = dteditedporecord.Rows[0]["PODATE"].ToString();
            txtshippingaddr.Text = dteditedporecord.Rows[0]["SHIPINGADDRESS"].ToString();
            ddlcurrencyname.SelectedValue = dteditedporecord.Rows[0]["CURRENCYID"].ToString();
            this.txtRejection.Text = dteditedporecord.Rows[0]["REJECTIONNOTE"].ToString();
            gvPurchaseOrder.DataSource = dteditedporecord;
            gvPurchaseOrder.DataBind();

            if (Request.QueryString["INDENT"].ToString().Trim() == "Y")
            {
                trgvindentdetails.Style["display"] = "";
                DataTable dtindent = clsMMPo.EditIndent(POID.Trim());
                this.gvindentdetails.DataSource = dtindent;
                this.gvindentdetails.DataBind();
                SelectedIndent(ref dtindent);
            }

            DataTable dt = new DataTable();
            dt = clsMMPo.BindPOFooterBasedOnPONO(PONO, Session["FINYEAR"].ToString());
            txtgrosstotal.Text = dt.Rows[0]["GROSSTOTAL"].ToString();
            this.txtTotalMRP.Text = dt.Rows[0]["MRPTOTAL"].ToString();
            this.txtadjustment.Text = dt.Rows[0]["ADJUSTMENT"].ToString();
            this.txtDiscPer.Text = dt.Rows[0]["DISCOUNTPERCENTAGE"].ToString();
            this.txtDiscAmnt.Text = dt.Rows[0]["DISCOUNT"].ToString();
            this.txtIgstValue.Text = dt.Rows[0]["IGSTVALUE"].ToString();
            this.txtCgstValue.Text = dt.Rows[0]["CGSTVALUE"].ToString();
            this.txtSgstValue.Text = dt.Rows[0]["SGSTVALUE"].ToString();
            txtexercise.Text = dt.Rows[0]["EXERCISE"].ToString();
            txtsaletax.Text = dt.Rows[0]["SALETAX"].ToString();
            txtrefno.Text = dt.Rows[0]["REFRENCENO"].ToString();
            txtQuotdt.Text = dt.Rows[0]["QUOTDATE"].ToString();
            txttotalamount.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
            ddlTPUName.SelectedValue = dt.Rows[0]["VENDORID"].ToString();
            txtremarks.Text = dt.Rows[0]["REMARKS"].ToString();
            //txtshippingaddr.Text = dt.Rows[0]["SHIPINGADDRESS"].ToString();

            ddlTPUName.Enabled = false;
            ddlProductName.Items.Clear();
            ddlProductName.Items.Add(new ListItem("-- SELECT PRODUCT NAME--", "0"));
            ddlProductName.AppendDataBoundItems = true;
            ddlProductName.DataSource = clsMMPo.BindProduct(ddlTPUName.SelectedValue.ToString());
            ddlProductName.DataValueField = "ID";
            ddlProductName.DataTextField = "NAME";
            ddlProductName.DataBind();
            ddlProductName.Enabled = true;
            InputTable.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            divadd.Style["display"] = "none";
            //tdlblrejection.Visible = false;
            //tdtxtrejection.Visible = false;
            btnadd.Visible = true;
            //btnsave.Visible = true;
            btncancel.Visible = true;
            //imgPopuppodate.Visible = false;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngrddelete_Click
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string Verify = "";
            decimal cgst = 0;
            decimal sgst = 0;
            decimal igst = 0;
            int flag = 0;
            string productid = hdn_podelete.Value.ToString();
            string poID = hdn_pofield.Value.ToString();
            string FromDate = hdn_FromDate.Value;
            string ToDate = hdn_ToDate.Value;
            Verify = clsMMPo.CheckVerify(poID);
            if (Verify == "Y")
            {
                MessageBox1.ShowInfo("Approve PO Can not be Delete");
            }
            else if (Verify == "H")
            {
                MessageBox1.ShowInfo("Hold PO Can not be Delete");
            }
            else if (Verify == "C")
            {
                MessageBox1.ShowInfo("Confirmed PO Can not be Delete");
            }
            else if (Verify == "A")
            {
                MessageBox1.ShowInfo("Before Approved PO Can not be Delete");
            }
            else
            {
                flag = clsMMPo.PORecordsDelete(productid, poID, FromDate, ToDate);
                if (flag == 1)
                {
                    DataTable dtDeleteValue = clsMMPo.DeletePOValues();
                    decimal tempgrosstotal = Convert.ToDecimal(txtgrosstotal.Text);
                    decimal tempTotalMRP = Convert.ToDecimal(txtTotalMRP.Text);
                    decimal tempsaletax = Convert.ToDecimal(txtsaletax.Text);
                    decimal tempexercise = Convert.ToDecimal(txtexercise.Text);
                    decimal temptotalamount = Convert.ToDecimal(txttotalamount.Text);

                    this.txtgrosstotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["GROSSTOTAL"]), 2));
                    this.txtTotalMRP.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["MRPTOTAL"]), 2));
                    this.txtsaletax.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["CSTTOTAL"]), 2));
                    this.txtexercise.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["EXCISETOTAL"]), 2));
                   // this.txttotalamount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["TOTAL"]) + Convert.ToDecimal(this.txtadjustment.Text.Trim()), 2));

                   // this.txtCgstValue.Text
                        cgst = Convert.ToDecimal(dtDeleteValue.Rows[0]["CGSTVALUE"]);
                    //this.txtSgstValue.Text 
                    sgst = Convert.ToDecimal(dtDeleteValue.Rows[0]["SGSTVALUE"]);
                    //this.txtIgstValue.Text
                    igst = Convert.ToDecimal(dtDeleteValue.Rows[0]["IGSTVALUE"]);
                    this.txtCgstValue.Text = Convert.ToString (cgst);
                    this.txtSgstValue.Text = Convert.ToString(sgst);
                    this.txtIgstValue.Text = Convert.ToString(igst);


                    this.txttotalamount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtDeleteValue.Rows[0]["TOTAL"]) + cgst + sgst + igst, 2));

                    if (Convert.ToDecimal(txtgrosstotal.Text) == 0)
                    {
                        this.txtnettotal.Text = "0";
                        this.txtgrosstotal.Text = "0";
                        this.txtTotalMRP.Text = "0";
                        this.txtsaletax.Text = "0";
                        this.txtexercise.Text = "0";
                        this.txttotalamount.Text = "0";
                        this.txtadjustment.Text = "0";
                        this.txtCgstValue.Text = "0";
                        this.txtSgstValue.Text = "0";
                        this.txtIgstValue.Text = "0";
                    }
                    LoadPOGrid();
                    DataTable dt = new DataTable();
                    if (hdn_pofield.Value.ToString() == "")
                    {
                        dt = (DataTable)HttpContext.Current.Session["PORECORDS"];
                    }
                    else
                    {
                        dt = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            this.ddlTPUName.Enabled = false;
                        }
                        else
                        {
                            this.ddlTPUName.Enabled = true;
                        }
                    }
                    MessageBox1.ShowSuccess("Record Deleted Successfully!");
                }
                else
                {
                    MessageBox1.ShowError("Record Deleted UnSuccessful!");
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

    #region Selected Indent
    public string SelectedIndent()
    {
        string indentid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvindentdetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                if (chk.Checked == true)
                {
                    Label lblINDENTID = (Label)gvr.FindControl("lblINDENTID");
                    indentid += lblINDENTID.Text.Trim() + ",";
                }
            }

            if (!string.IsNullOrEmpty(indentid))
            {
                indentid = indentid.Substring(0, indentid.Length - 1);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return indentid;
    }
    #endregion

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string getflag = string.Empty;
            DataTable dtpono = new DataTable();
            string pono = string.Empty;
            string INDENTNO = string.Empty;
            string QSTAG = string.Empty;
            if (Request.QueryString["INDENT"].ToString().Trim() == "Y")
            {
                QSTAG = "Y";
            }
            else
            {
                QSTAG = "N";
            }
            if (gvPurchaseOrder.Rows.Count > 0)
            {
                if (Convert.ToDecimal(txtgrosstotal.Text) != 0)
                {
                    if (Request.QueryString["INDENT"].ToString().Trim() == "Y")
                    {
                        INDENTNO = SelectedIndent();
                    }
                    else
                    {
                        INDENTNO = "";
                    }

                    #region grdTerms Loop
                    //int CountTerms = 0;
                    string strTermsID = string.Empty;
                    //DataTable dtTerms = (DataTable)Session["TermsMMPOOrder"];
                    //if (dtTerms.Rows.Count > 0)
                    //{
                    //    for (int j = 0; j < grdTerms.Rows.Count; j++)
                    //    {
                    //        GridDataControlFieldCell chkcell = grdTerms.RowsInViewState[j].Cells[2] as GridDataControlFieldCell;
                    //        CheckBox chk = chkcell.FindControl("ChkIDTERMS") as CheckBox;
                    //        HiddenField hiddenField = chkcell.FindControl("hdnTERMSName") as HiddenField;

                    //        if (chk.Checked == true)
                    //        {
                    //            CountTerms = CountTerms + 1;
                    //            strTermsID = strTermsID + hiddenField.Value.ToString().ToString() + ",";
                    //        }
                    //    }
                    //    if (CountTerms > 0)
                    //    {
                    //        strTermsID = strTermsID.Substring(0, strTermsID.Length - 1);
                    //    }
                    //}
                    decimal discpercent = 0;
                    decimal discamount = 0;
                    if (txtDiscPer.Text.Trim() == "")
                    {
                        discpercent = 0;
                    }
                    else
                    {
                        discpercent = Convert.ToDecimal(txtDiscPer.Text.Trim());
                    }
                    if (txtDiscAmnt.Text.Trim() == "")
                    {
                        discamount = 0;
                    }
                    else
                    {
                        discamount = Convert.ToDecimal(txtDiscAmnt.Text.Trim());
                    }



                    #endregion

                    dtpono = clsMMPo.InsertPODetails(txtpodate.Text.Trim(), ddlTPUName.SelectedValue.ToString(), ddlTPUName.SelectedItem.Text, txtremarks.Text, HttpContext.Current.Session["UserID"].ToString(),
                             Convert.ToDecimal(txtgrosstotal.Text), Convert.ToDecimal(txtadjustment.Text), discpercent, discamount, 0, Convert.ToDecimal(txtpacking.Text), 0, Convert.ToDecimal(txtexercise.Text), 0,
                             Convert.ToDecimal(txtsaletax.Text), Convert.ToDecimal(txtnettotal.Text), 0, Convert.ToDecimal(txttotalamount.Text), hdn_pofield.Value.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(),
                             Convert.ToDecimal(this.txtTotalMRP.Text.Trim()), "", "N", QSTAG.Trim(), Convert.ToString(Request.QueryString["MENUID"]).Trim(), INDENTNO.Trim(), txtrefno.Text.Trim(), strTermsID, txtQuotdt.Text,
                             txtshippingaddr.Text, ddlcurrencyname.SelectedItem.Text, ddlcurrencyname.SelectedValue.ToString(), HttpContext.Current.Session["DEPOTID"].ToString(), this.txtRejection.Text, this.txtTermsCondition.Text, "O"); /*NEW PARAMETER txtTermsCondition ADDED BY PRITMA BASU*/



                    if (dtpono.Rows.Count > 0)
                    {
                        pono = dtpono.Rows[0]["PONO"].ToString();
                        ViewState["pono"] = pono;
                        if (hdn_pofield.Value == "")
                        {
                            if (Convert.ToDecimal(this.txttotalamount.Text) >= 50000)
                            {
                                MessageBox1.ShowSuccess("PurchaseOrder NO:<b><font color='blue'> " + pono + "</font></b> Saved Successfully!</br></b><b><font color='blue'> This PO Status Hold</b>", 60, 600);
                                LoadPO();
                                txtpono.Text = pono;

                                //div_btnPrint.Style["display"] = "";
                                if (Request.QueryString["MODE"] == "RQ")
                                {
                                    pnlDisplay.Style["display"] = "";
                                    InputTable.Style["display"] = "none";
                                    divpono.Style["display"] = "none";
                                    this.divadd.Visible = false;
                                    tdlblrejection.Visible = true;
                                    tdtxtrejection.Visible = true;
                                }
                                else
                                {
                                    this.btnsave.Enabled = false;
                                    pnlDisplay.Style["display"] = "";
                                    InputTable.Style["display"] = "none";
                                    divpono.Style["display"] = "none";
                                    divadd.Style["display"] = "none";
                                    tdlblrejection.Visible = true;
                                    tdtxtrejection.Visible = true;
                                    LoadPO();
                                    txtshippingaddr.Text = "";

                                    DataTable DT = new DataTable();
                                    DT = clsMMPo.Bind_Sms_Mobno("A", "");
                                    foreach (DataRow row in DT.Rows)
                                    {
                                        this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                                    }
                                    string DELETE = string.Empty;
                                    DT = clsMMPo.Bind_Sms_Mobno("D", "");
                                }
                            }
                            else
                            {

                                if (Request.QueryString["MODE"] == "RQ")
                                {
                                    MessageBox1.ShowSuccess("PurchaseOrder NO:<b><font color='green'> " + pono + "</font>Saved Successfully!", 60, 600);
                                    LoadPO();
                                    pnlDisplay.Style["display"] = "";
                                    InputTable.Style["display"] = "none";
                                    divpono.Style["display"] = "none";
                                    this.divadd.Visible = false;
                                }
                                else
                                {
                                    MessageBox1.ShowSuccess("PurchaseOrder NO:<b><font color='green'> " + pono + "</font>Saved Successfully!", 60, 600);
                                    LoadPO();
                                    txtpono.Text = pono;
                                    //div_btnPrint.Style["display"] = "";
                                    this.btnsave.Enabled = false;
                                    pnlDisplay.Style["display"] = "";
                                    InputTable.Style["display"] = "none";
                                    divpono.Style["display"] = "none";
                                    divadd.Style["display"] = "none";
                                    tdlblrejection.Visible = true;
                                    tdtxtrejection.Visible = true;
                                    LoadPO();
                                    txtshippingaddr.Text = "";
                                    DataTable DT = new DataTable();
                                    DT = clsMMPo.Bind_Sms_Mobno("A", "");
                                    foreach (DataRow row in DT.Rows)
                                    {
                                        this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                                    }
                                    string DELETE = string.Empty;
                                    DT = clsMMPo.Bind_Sms_Mobno("D", "");
                                }

                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(this.txttotalamount.Text) >= 50000)
                            {
                                MessageBox1.ShowSuccess("PurchaseOrder NO:<b><font color='blue'> " + pono + "</font></b> Updated Successfully!</br><b><font color='blue'>This PO Status Hold</b>", 60, 600);
                                LoadPO();
                                txtpono.Text = hdn_pofield.Value.ToString();
                                this.btnsave.Enabled = false;
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("PurchaseOrder NO:<b><font color='green'> " + pono + "</font></b> Updated Successfully!", 60, 600);
                                LoadPO();
                                txtpono.Text = hdn_pofield.Value.ToString();
                                this.btnsave.Enabled = false;
                            }
                        }
                        //hdn_pofield.Value = "";
                        //hdn_pono.Value = "";
                        //hdn_podelete.Value = "";
                        //Hdn_Fld.Value = "";
                        //hdn_convertionqty.Value = "";
                        //txtshippingaddr.Text = "";
                        divpono.Style["display"] = "none";
                        InputTable.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        string menuID = Request.QueryString["MENUID"].ToString().Trim();
                        ViewState["menuID"] = menuID;
                        txtpodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        divadd.Style["display"] = "";
                        tdlblrejection.Visible = false;
                        tdtxtrejection.Visible = false;
                    }


                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error on Saving record!</font></b>");
                        this.btnsave.Enabled = true;
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Entry atleast one Product or Gross total amount not zero(0)!</b>", 60, 550);
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please Add Purchase Order</b>", 60, 550);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region clear
    public void clear()
    {
        ddlTPUName.SelectedValue = "0";
        ddlTPUName.Enabled = true;
        ddlProductName.SelectedValue = "0";
        txtremarks.Text = "";
        txtqty.Text = "";
        txtprice.Text = "";
       // txtrequireddate.Text = "";
       // txttorequireddate.Text = "";
        txtgrosstotal.Text = "0";
        txtadjustment.Text = "0";
        txtTotalMRP.Text = "0";
        txtpacking.Text = "0";
        txtsaletax.Text = "0";
        txtexercise.Text = "0";
        txttotalamount.Text = "0";
        txtnettotal.Text = "0";
        btnnewentry.Visible = true;
        gvPurchaseOrder.DataSource = null;
        gvPurchaseOrder.DataBind();
        clsMMPo.ResetDataTables();   // Reset all Datatables
        DateLock();
        LoadPO();
        btnsave.Enabled = true;
        txtrefno.Text = "";
        txtQuotdt.Text = "";
        txtpodate.Text = "";
    }
    #endregion

    #region btngvfill_Click
    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {
            LoadPO();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string upath = "frmRptPurchaseOrder.aspx?pid=" + ViewState["pono"];
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

    }
    #endregion

    #region clearTable
    public void clearTable()
    {
        dteditedporecord.Clear();
    }
    #endregion

    #region Load Indent Details
    public void LoadIndentDetails()
    {
        try
        {
            ClsMMPoOrder clsmmpo = new ClsMMPoOrder();
            DataTable dt = clsmmpo.BindIndent(this.ddlTPUName.SelectedValue.Trim(), this.txtreqfromdate.Text.Trim(), this.txtreqtodate.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvindentdetails.DataSource = dt;
                this.gvindentdetails.DataBind();
            }
            else
            {
                this.gvindentdetails.DataSource = null;
                this.gvindentdetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Indent Search
    protected void btnindentseach_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlTPUName.SelectedValue != "0")
            {
                this.LoadIndentDetails();
            }
            else
            {
                MessageBox1.ShowWarning("Please Select Vendor ");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnindentadd_Click(object sender, EventArgs e)
    {
        string MRPValue = string.Empty;
        foreach (GridViewRow row in gvindentdetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkreq") as CheckBox);
                if (chkRow.Checked)
                {
                    string INDENTID = (row.Cells[1].FindControl("lblINDENTID") as Label).Text;
                    DataTable dt = clsMMPo.BindProductMM(INDENTID.Trim());

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string MATERIALID = dt.Rows[i]["MATERIALID"].ToString().Trim();
                        string NAME = dt.Rows[i]["NAME"].ToString().Trim();
                        string UOMID = dt.Rows[i]["UOMID"].ToString().Trim();
                        string UOMNAME = dt.Rows[i]["UOMNAME"].ToString().Trim();
                        string QTY = dt.Rows[i]["QTY"].ToString().Trim();
                        string REQUIREDFROMDATE = dt.Rows[i]["REQUIREDFROMDATE"].ToString().Trim();
                        string REQUIREDTODATE = dt.Rows[i]["REQUIREDTODATE"].ToString().Trim();
                        decimal mrp = clsMMPo.BindMrp(MATERIALID.Trim());
                        string Rate = clsMMPo.BindRate(MATERIALID.Trim(), this.ddlTPUName.SelectedValue.Trim());

                        hdn_convertionqty.Value = Convert.ToString(clsMMPo.ConvertionQty(MATERIALID, UOMID, QTY,
                            Convert.ToDecimal(Rate.Trim())));
                        MRPValue = Convert.ToString(clsMMPo.ProductMRPTotal(MATERIALID, UOMID, QTY.Trim(), mrp));
                        gvPurchaseOrder.DataSource = clsMMPo.BindPurchaseOrderGridRecords(MATERIALID, NAME, Convert.ToDecimal(QTY), UOMID.Trim(),
                                                                                   UOMNAME.Trim(), hdn_convertionqty.Value.ToString(), REQUIREDFROMDATE.Trim(),
                                                                                   REQUIREDTODATE, Convert.ToDecimal(Rate.Trim()),
                                                                                   Convert.ToDecimal(mrp), Convert.ToDecimal(MRPValue),
                                                                                   0, Convert.ToDecimal(this.hdnExcise.Value), Convert.ToDecimal(this.hdnCST.Value), 0, 0, 0, 0, 0,0,"", 0,0,"", 0,0,"");
                        gvPurchaseOrder.DataBind();
                    }
                    DataTable dtPOValue = clsMMPo.BindPOValues(hdn_pofield.Value.ToString());
                    txtgrosstotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["GROSSTOTAL"]), 2));
                    txtTotalMRP.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["MRPTOTAL"]), 2));
                    txtsaletax.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["CSTTOTAL"]), 2));
                    txtexercise.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["EXCISETOTAL"]), 2));
                    txttotalamount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPOValue.Rows[0]["TOTAL"]), 2));
                }
            }
        }
    }

    #region grdTerms_RowDataBound
    protected void grdTerms_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataTable dt = new DataTable();
        if (hdn_pono.Value != "")
        {
            dt = clsMMPo.EditTerms(hdn_pono.Value);
        }
        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell chkcell = e.Row.Cells[2] as GridDataControlFieldCell;
            CheckBox chk = chkcell.FindControl("ChkIDTERMS") as CheckBox;
            HiddenField hiddenField = chkcell.FindControl("hdnTERMSName") as HiddenField;
            if (hdn_pono.Value != "")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TERMSID"].ToString() == chk.ToolTip)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Add By Rajeev
    public void LoadTermsConditions()
    {
        try
        {
            DataTable dtTerms = new DataTable();
            dtTerms = clsMMPo.BindTerms(Convert.ToString(ViewState["menuID"]).Trim());
            Session["TermsMMPOOrder"] = dtTerms;

            if (dtTerms != null)
            {
                if (dtTerms.Rows.Count > 0)
                {
                    this.grdTerms.DataSource = dtTerms;
                    this.grdTerms.DataBind();
                }
                else
                {
                    this.grdTerms.DataSource = null;
                    this.grdTerms.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btnApprove_Click
    // Added By Rajeev Kumar On 15-07-2017
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "1", "");
            this.hdn_pofield.Value = "";
            if (flag == 1)
            {
                MessageBox1.ShowSuccess("Purchase Order: <b><font color='green'>" + this.txtpono.Text + "</font></b> approved.", 60, 700);
                pnlDisplay.Style["display"] = "";
                InputTable.Style["display"] = "none";
                divpono.Style["display"] = "none";
                divadd.Style["display"] = "none";
                tdlblrejection.Visible = true;
                tdtxtrejection.Visible = true;
                LoadPO();
                DataTable dtc = new DataTable();
                dtc = clsMMPo.CREATESMS(PurchaseID, "2");

                DataTable DT = new DataTable();
                DT = clsMMPo.Bind_Sms_Mobno("P", PurchaseID);
                foreach (DataRow row in DT.Rows)
                {
                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                }
                string DELETE = string.Empty;
                DT = clsMMPo.Bind_Sms_Mobno("Q", PurchaseID);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                //hdn_pofield.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnReject_Click
    // Added By Rajeev On 15-07-2017
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtRejection.Text == "")
            {
                MessageBox1.ShowWarning("Please Give rejection details");
                this.txtRejection.BackColor = Color.LightPink;
                return;
            }
            else
            {
                ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
                int flag = 0;
                string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
                flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "2", this.txtRejection.Text);
                this.hdn_pofield.Value = "";
                if (flag == 1)
                {
                    MessageBox1.ShowSuccess("Purchase Order: <b><font color='red'>" + this.txtpono.Text + "</font></b> Rejected.", 60, 700);
                    pnlDisplay.Style["display"] = "";
                    InputTable.Style["display"] = "none";
                    divpono.Style["display"] = "none";
                    divadd.Style["display"] = "none";
                    tdlblrejection.Visible = true;
                    tdtxtrejection.Visible = true;
                    LoadPO();
                }
                else if (flag == 0)
                {
                    pnlDisplay.Style["display"] = "none";
                    //hdn_pofield.Style["display"] = "";
                    MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
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

    #region grdDespatchHeader_RowDataBound
    protected void gvpodetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();

                if (status == "PENDING")
                {
                    cell.ForeColor = Color.Blue;
                }
                else if (status == "REJECTED")
                {
                    cell.ForeColor = Color.Red;
                }
                else if (status == "HOLD")
                {
                    cell.ForeColor = Color.YellowGreen;
                }
                else if (status == "CONFIRMED")
                {
                    cell.ForeColor = Color.Violet;
                }
                else
                {
                    cell.ForeColor = Color.Green;
                }

                GridDataControlFieldCell cell5 = e.Row.Cells[5] as GridDataControlFieldCell;
                string createdfrom = cell5.Text.Trim().ToUpper();

                if (createdfrom == "C.O")
                {
                    cell5.ForeColor = Color.Blue;
                }
                else if (createdfrom == "HARIDWAR FACTORY UNIT-1")
                {
                    cell5.ForeColor = Color.Red;
                }
                else if (createdfrom == "HARIDWAR FACTORY UNIT-2")
                {
                    cell5.ForeColor = Color.Red;
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

    #region btnPrint_Click
    protected void btnPOPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + hdn_pofield.Value.Trim() + "&&TAG=PO&&MenuId=29";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
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
            CalendarExtender1.StartDate = oDate;
            CalendarExtender2.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;
            CalendarExtender8.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtpodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtQuotdt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

                CalendarExtender1.EndDate = today1;
                CalendarExtender2.EndDate = today1;
                CalendarExtender3.EndDate = today1;
                CalendarExtender8.EndDate = today1;
            }
            else
            {
                this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtpodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtQuotdt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
                CalendarExtender8.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void btnHoldApproved_Click(object sender, EventArgs e)
    {
        /*try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "4", ""); 
            this.hdn_pofield.Value = "";
            if (flag == 1)
            {
                MessageBox1.ShowSuccess("Purchase Order: <b><font color='green'>" + this.txtpono.Text + "</font></b> Wait Approved.", 60, 700);
                pnlDisplay.Style["display"] = "";
                InputTable.Style["display"] = "none";
                divpono.Style["display"] = "none";
                divadd.Style["display"] = "none";
                tdlblrejection.Visible = true;
                tdtxtrejection.Visible = true;
                LoadPO();

                DataTable DT = new DataTable();
                DT = clsMMPo.Bind_Sms_Mobno("Z", PurchaseID);
                foreach (DataRow row in DT.Rows)
                {
                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                }
                string DELETE = string.Empty;
                DT = clsMMPo.Bind_Sms_Mobno("Z1", PurchaseID);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                //hdn_pofield.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }*/
    }

    protected void btnConfirmed_Click(object sender, EventArgs e)
    {
        /*try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "3", ""); 
            this.hdn_pofield.Value = "";
            if (flag == 1)
            {
                MessageBox1.ShowSuccess("Purchase Order: <b><font color='green'>" + this.txtpono.Text + "</font></b> Wait Approved.", 60, 700);
                pnlDisplay.Style["display"] = "";
                InputTable.Style["display"] = "none";
                divpono.Style["display"] = "none";
                divadd.Style["display"] = "none";
                tdlblrejection.Visible = true;
                tdtxtrejection.Visible = true;
                LoadPO();

                DataTable DT = new DataTable();
                DT = clsMMPo.Bind_Sms_Mobno("SK", PurchaseID);
                foreach (DataRow row in DT.Rows)
                {
                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                }
                string DELETE = string.Empty;
                DT = clsMMPo.Bind_Sms_Mobno("K1", PurchaseID);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                //hdn_pofield.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }*/
    }

    #region CreateTaxDatatable
    void CreateTaxDatatable(string POID, string PRODUCTID, string TAXPERCENTAGE, string MRP, string PRODUCTNAME, string Taxid)
    {
        DataTable dt = (DataTable)Session["TAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["POID"] = POID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["TAXID"] = Taxid;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["MRP"] = MRP;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    protected void btnPoQuotationUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string POID = hdn_pofield.Value.ToString();
            if (POID == "")
            {
                MessageBox1.ShowError("First Save The Po Then Upload File");
                return;
            }
            else
            {
                if (FileUpload2.HasFile)
                {
                    string FileName = FileUpload2.PostedFile.FileName.ToString().Trim();
                    string FileNameWithoutExt = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
                    string Extension = Path.GetExtension(FileUpload2.FileName);

                    string currentPath = Server.MapPath("~/FileUpload/") + Path.GetFileName(FileUpload2.FileName);
                    FileNameWithoutExt = FileNameWithoutExt + '_' + DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    ClsMMPoOrder upload = new ClsMMPoOrder();
                    string uploadno = string.Empty;
                    DataTable IsExists = new DataTable();
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/FileUpload/" + FileNameWithoutExt + Extension));
                    string FinalFileName = FileNameWithoutExt + Extension;
                    uploadno = upload.UploadFileForPo(POID, FinalFileName, "1");
                    if (uploadno != "")
                    {
                        MessageBox1.ShowSuccess("<font color='green'>File uploaded successfully.</font><br /><br /> <b> Uploadno No is - " + uploadno + " <b> <br /><br /> <font color='blue'>Please Click on the Document Status Button<br /> to check the uploaded file</font>", 60, 450);
                    }
                    else
                    {
                        MessageBox1.ShowWarning("<font color='red'> Error On Uploading </font><br /><br /> <b> ", 60, 450);
                    }
                }
                else
                {
                    MessageBox1.ShowWarning("Please select File..!", 40, 250);
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnPoComprativeUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string POID = hdn_pofield.Value.ToString();
            if (POID == "")
            {
                MessageBox1.ShowError("First Save The Po Then Upload File");
                return;
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = FileUpload1.PostedFile.FileName.ToString().Trim();
                    string FileNameWithoutExt = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                    string Extension = Path.GetExtension(FileUpload1.FileName);

                    string currentPath = Server.MapPath("~/FileUpload/") + Path.GetFileName(FileUpload1.FileName);
                    FileNameWithoutExt = FileNameWithoutExt + '_' + DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    ClsMMPoOrder upload = new ClsMMPoOrder();
                    string uploadno = string.Empty;
                    DataTable IsExists = new DataTable();
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/FileUpload/" + FileNameWithoutExt + Extension));
                    string FinalFileName = FileNameWithoutExt + Extension;
                    uploadno = upload.UploadFileForPo(POID, FinalFileName, "2");
                    if (uploadno != "")
                    {
                        MessageBox1.ShowSuccess("<font color='green'>File uploaded successfully.</font><br /><br /> <b> Uploadno No is - " + uploadno + " <b> <br /><br /> <font color='blue'>Please Click on the Document Status Button<br /> to check the uploaded file</font>", 60, 450);
                    }
                    else
                    {
                        MessageBox1.ShowWarning("<font color='red'> Error On Uploading </font><br /><br /> <b> ", 60, 450);
                    }
                }
                else
                {
                    MessageBox1.ShowWarning("Please select File..!", 40, 250);
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region
    protected void btnDocuments_Click(object sender, EventArgs e)/*quation*/
    {
        try
        {
            ClsMMPoOrder upload = new ClsMMPoOrder();
            string POID = hdn_pofield.Value.ToString();
            DataTable dtpo = new DataTable();
            dtpo = upload.FetchPoDocument(POID);
            if (dtpo.Rows.Count > 0)
            {
                string UPLOADPOID = Convert.ToString(dtpo.Rows[0]["POID"].ToString());
                string fileid = Convert.ToString(dtpo.Rows[0]["UPLOADID"].ToString());
                string strPopup = string.Empty;
                if (UPLOADPOID.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCFileUpload_FAC.aspx?&QCID=" + UPLOADPOID + "&FID=" + fileid + "&MODE=PO"
                    + "','new window', 'top=200, left=1000, width=1000, height=550, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
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
            else
            {
                MessageBox1.ShowWarning("There Is no file");
                return;
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnComDocuments_Click(object sender, EventArgs e) /*FetchPoDocumentComparative*/
    {
        try
        {
            ClsMMPoOrder upload = new ClsMMPoOrder();
            string POID = hdn_pofield.Value.ToString();
            DataTable dtpo = new DataTable();
            dtpo = upload.FetchPoDocumentComparative(POID);
            if (dtpo.Rows.Count > 0)
            {
                string UPLOADPOID = Convert.ToString(dtpo.Rows[0]["POID"].ToString());
                string fileid = Convert.ToString(dtpo.Rows[0]["UPLOADID"].ToString());
                string strPopup = string.Empty;
                if (UPLOADPOID.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmQCFileUpload_FAC.aspx?QCID=" + UPLOADPOID + "&FID=" + fileid + "&MODE=POCom"
                    + "','new window', 'top=200, left=1000, width=1000, height=550, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
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
            else
            {
                MessageBox1.ShowWarning("There Is no file");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}