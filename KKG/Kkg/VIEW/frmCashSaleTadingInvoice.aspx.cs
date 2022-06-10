#region Namespace
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
using Utility;
using WorkFlow;
#endregion

public partial class VIEW_frmCashSaleTadingInvoice : System.Web.UI.Page
{
    string mode = "trading";/*mode for sp by p.basu*/
    string billtype = "";/*mode for sp by p.basu*/
    ArrayList Arry = new ArrayList();
    ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Back Date Entry Validation
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            int Flag = ClsCommon.CheckDate(Request.QueryString["MENUID"].ToString().Trim());
            if (Flag > 0)
            {
                this.ImageButton1.Enabled = true;
                this.CalendarExtenderInvoiceDate.Enabled = true;
            }
            else
            {
                this.ImageButton1.Enabled = true;
                this.CalendarExtenderInvoiceDate.Enabled = true;
            }
            #endregion

            #region QueryString
            ViewState["menuID"] = Request.QueryString["MENUID"].ToString();
            ViewState["BSID"] = Request.QueryString["BSID"].ToString();
            ViewState["Checker"] = Request.QueryString["CHECKER"].ToString().Trim();

            if (Convert.ToString(ViewState["Checker"]).Trim() == "TRUE")
            {
                this.btnaddhide.Style["display"] = "none";
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.trproductadd.Visible = false;
            }
            else
            {
                this.btnaddhide.Style["display"] = "";
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.trproductadd.Visible = true;
            }
            if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
            {
                this.hdInvoice.InnerText = "INVOICE DETAILS";
                this.lgndInvoice.InnerText = "INVOICE DETAILS";
                //this.lgndProductDetails.InnerText = "Product & Scheme Details";
                this.lgndProductDetails.InnerText = "Product Details";
                this.lblStockRcvdNo.Text = "Invoice No.";
                this.Label2.Text = "Invoice Date";
                this.Label10.Text = "GATEPASS NO";
                this.txtGetpass.Attributes.Add("placeholder", "GETPASS NO");
                this.grdDespatchHeader.Columns[2].HeaderText = "INVOICE NO.";
            }
            else
            {
                this.hdInvoice.InnerText = "CHALLAN DETAILS";
                this.lgndInvoice.InnerText = "CHALLAN DETAILS";
                this.lgndProductDetails.InnerText = "Product Details";
                this.lblStockRcvdNo.Text = "Challan No.";
                this.Label2.Text = "Challan Date";
                this.Label10.Text = "GNG CHALLAN NO";
                this.txtGetpass.Attributes.Add("placeholder", "GNG CHALLAN NO");
                this.grdDespatchHeader.Columns[2].HeaderText = "CHALLAN NO.";
            }
            #endregion

            string Type = Request.QueryString["TYPE"];
            if (Type != null)
            {
                if (Convert.ToString(Request.QueryString["TYPE"]).Trim() == "LGRREPORT")
                {
                    this.pnlDisplay.Style["display"] = "none";
                    this.pnlAdd.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "none";
                    this.btnsubmitdiv.Visible = false;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.btnCanceldiv.Visible = false;
                    Btn_View(Request.QueryString["InvId"]);
                    this.trBtn.Style["display"] = "none";
                }
            }
            else
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.txtFromDate.Style.Add("color", "black !important");
                this.txtToDate.Style.Add("color", "black !important");
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtLRGRDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtGetPassDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.LoadMotherDepot();
                this.BindInvoiceCustomer();
                this.LoadInvoice();
                this.LoadTermsConditions();
                this.BindInsurenceCompany();// Fill To Insuarance company
                this.BindShippingCustomer();
                /*Calender Control Date Range*/
                CalendarExtenderInvoiceDate.EndDate = DateTime.Now;
                CalendarExtenderLRGRDate.EndDate = DateTime.Now;
                CalendarExtenderGetPassDate.EndDate = DateTime.Now;
                CalendarExtenderICDS.EndDate = DateTime.Now;
                /****************************/
            }

            if (Convert.ToString(ViewState["BSID"]).Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
            {
                this.txtICDS.Visible = true;
                this.txtICDSDate.Visible = true;
                this.ImageButtonICDS.Visible = true;
                this.LabelICDS.Visible = true;
                this.LabelICDSDate.Visible = true;
            }
            else
            {
                this.txtICDS.Visible = false;
                this.txtICDSDate.Visible = false;
                this.ImageButtonICDS.Visible = false;
                this.LabelICDS.Visible = false;
                this.LabelICDSDate.Visible = false;
            }
        }

        foreach (ListItem item in ddlDepot.Items)
        {
            if (item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "Blue");
            }
        }

        foreach (ListItem item in ddlBatch.Items)
        {
            if (item.Value == "1")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "blue");
                item.Attributes.CssStyle.Add("background-color", "Beige");
            }
        }


        string userid = HttpContext.Current.Session["USERID"].ToString().Trim();
        if (userid != null && Convert.ToString(Request.QueryString["CHECKER"]).Trim() == "FALSE")
        {
            clsSaleInvoiceMM objReturn = new clsSaleInvoiceMM();
            DataTable dtstore = new DataTable();
            dtstore = objReturn.BindStoreDetails(HttpContext.Current.Session["USERID"].ToString().Trim());
            string store = string.Empty;
            if (dtstore.Rows.Count > 0)
            {
                store = dtstore.Rows[0]["userst"].ToString();
            }
            if (store == "2")
            {
                MessageBox1.ShowInfo("Customer is not Mapped With StoreLocation please map storelocation");
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "none";
                this.btnaddhide.Style["display"] = "none";
                return;



            }

        }

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 300, '100%' , 30 ,false); </script>", false);
    }

    #region Populate Transporter
    protected void BindTransporter(string DepotID)
    {
        clsDespatchStock clsDespatchStck = new clsDespatchStock();
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataTable dtTransporter = clsInvc.BindDepot_Transporter(DepotID);
        if (dtTransporter.Rows.Count > 0)
        {
            if (dtTransporter.Rows.Count > 1)
            {
                this.ddlTransporter.Items.Clear();
                this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
                this.ddlTransporter.AppendDataBoundItems = true;
                this.ddlTransporter.DataSource = dtTransporter;
                this.ddlTransporter.DataValueField = "ID";
                this.ddlTransporter.DataTextField = "NAME";
                this.ddlTransporter.DataBind();
            }
            else if (dtTransporter.Rows.Count == 1)
            {
                this.ddlTransporter.Items.Clear();
                this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
                this.ddlTransporter.AppendDataBoundItems = true;
                this.ddlTransporter.DataSource = dtTransporter;
                this.ddlTransporter.DataValueField = "ID";
                this.ddlTransporter.DataTextField = "NAME";
                this.ddlTransporter.DataBind();
                this.ddlTransporter.SelectedValue = Convert.ToString(dtTransporter.Rows[0]["ID"]);
            }
        }
        else
        {
            this.ddlTransporter.Items.Clear();
            this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
            this.ddlTransporter.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region LoadMotherDepot
    public void LoadMotherDepot()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DataTable dtDepot = new DataTable();

            if (Convert.ToString(ViewState["Checker"]) == "FALSE")
            {
                dtDepot = clsReceivedStock.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
            }
            else
            {
                dtDepot = clsstocktransfer.BindDepo("");
            }
            if (dtDepot != null)
            {
                if (dtDepot.Rows.Count > 0)
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                    this.ddlDepot.AppendDataBoundItems = true;
                    this.ddlDepot.DataSource = dtDepot;
                    this.ddlDepot.DataValueField = "BRID";
                    this.ddlDepot.DataTextField = "BRNAME";
                    this.ddlDepot.DataBind();

                    if (dtDepot.Rows.Count == 1)
                    {
                        this.ddlDepot.SelectedValue = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                        this.BindTransporter(this.ddlDepot.SelectedValue.Trim());
                    }
                }
                else
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                    this.ddlDepot.AppendDataBoundItems = true;
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region  DataTable Structure Method
    protected void CreateStructure()
    {
        this.CreateDataTable();
        this.CreateDataTableTaxComponent();
    }
    #endregion

    #region BindInvoiceCustomer
    protected void BindInvoiceCustomer()
    {
        DataTable dtCustomer = new DataTable();
        DataTable dtDepot = new DataTable();
        clsSaleInvoiceMM clsSaleInvoiceMM = new clsSaleInvoiceMM();
        dtCustomer = clsSaleInvoiceMM.BindCustomer(ViewState["BSID"].ToString().Trim(), "", this.ddlDepot.SelectedValue.Trim());

        if (this.hdnDespatchID.Value == "")
        {
            if (dtCustomer.Rows.Count > 0)
            {
                this.ddlDistributor.Items.Clear();
                this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlDistributor.AppendDataBoundItems = true;
                this.ddlDistributor.DataSource = dtCustomer;
                this.ddlDistributor.DataValueField = "CUSTOMERID";
                this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                this.ddlDistributor.DataBind();

                if (dtCustomer.Rows.Count == 1)
                {
                    this.ddlDistributor.SelectedValue = Convert.ToString(dtCustomer.Rows[0]["CUSTOMERID"]);
                    this.BindDeliveryAddress(this.ddlDistributor.SelectedValue.Trim());
                    this.CreateStructure();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlWaybill.ClientID + "').focus(); ", true);
                    this.txtRebate.Text = "0";
                }
            }
            else
            {
                this.ddlDistributor.Items.Clear();
                this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlDistributor.AppendDataBoundItems = true;
            }
        }
        else if (this.hdnDespatchID.Value != "")
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            DataSet ds = new DataSet();
            ds = clsInvc.EditInvoiceDetails(saleInvoiceID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlDistributor.Items.Clear();
                this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlDistributor.AppendDataBoundItems = true;
                this.ddlDistributor.DataSource = dtCustomer;
                this.ddlDistributor.DataValueField = "CUSTOMERID";
                this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                this.ddlDistributor.DataBind();
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
                this.BindDeliveryAddress(this.ddlDistributor.SelectedValue.Trim());
            }
        }

    }
    #endregion

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtBillingAdress.Attributes.Add("maxlength", "350");
        this.hdnbilltype.Value = "";
        this.hdnDespatchID.Value = "";
        this.ClearControls();
        this.ResetControls();
        this.ResetSession();
        this.trAutoInvoiceNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.btnaddhide.Style["display"] = "none";
        this.btnsubmitdiv.Style["display"] = "";
        this.chkFree.Checked = false;
        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = true;
        this.ddlDistributor.Enabled = true;
        this.lblCheckerNote.Visible = false;
        this.txtCheckerNote.Visible = false;
        #endregion
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grdQtySchemeDetails.DataSource = null;
        this.grdQtySchemeDetails.DataBind();
        this.txtwaybilno.Text = "";
        this.txtwaybilldate.Text = "";
        this.txtwaybilno.Visible = false;
        this.txtwaybilldate.Visible = false;
        this.imgPopupwaybilldate.Visible = false;
        this.lblwaybildate.Visible = false;
        this.txtwaybilno.Visible = false;
        this.lblwaybillno.Visible = false;
        foreach (ListItem item in ddlDepot.Items)
        {
            if (item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "Blue");
            }
        }

        DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        CalendarExtenderInvoiceDate.StartDate = oDate;
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarExtenderInvoiceDate.EndDate = today1;
            CalendarExtenderInvoiceDate.StartDate = oDate;

        }
        else
        {
            CalendarExtenderInvoiceDate.StartDate = new DateTime(endyear1, 03, 01);
            this.txtInvoiceDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtenderInvoiceDate.EndDate = cDate;

        }
      
        this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
    }
    #endregion

    #region LoadTermsConditions
    public void LoadTermsConditions()
    {
        try
        {
            clsDespatchStock clsDespatchStck = new clsDespatchStock();
            DataTable dtTerms = new DataTable();

            dtTerms = clsDespatchStck.BindTerms(Convert.ToString(ViewState["menuID"]).Trim());
            Session["Terms"] = dtTerms;

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
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region ddlDepot_SelectedIndexChanged
    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlDepot.SelectedValue != "0")
            {
                this.BindTransporter(this.ddlDepot.SelectedValue.Trim());
            }

            foreach (ListItem item in ddlDepot.Items)
            {
                if (item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "Blue");
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

    #region ddlProduct_SelectedIndexChanged
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtDeliveredQtyPCS.Text = "0";
        if (ddlProduct.SelectedValue != "0")
        {
            if (this.ddlPacksize.SelectedValue != "0")
            {
                this.hdnProductType.Value = isProductType(this.ddlProduct.SelectedValue);
                this.BindOrderQty(this.ddlDistributor.SelectedValue.Trim(), "", this.ddlProduct.SelectedValue.Trim(), Convert.ToString(Session["DEPOTID"]));
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlPacksize.ClientID + "').focus(); ", true);

            }
        }
    }
    #endregion

    public string isProductType(string id)
    {
        string type = string.Empty;
        ClsVendor_TPU obj = new ClsVendor_TPU();
        type = obj.editGateEntryCheck("SERVICE", id);
        return type;
    }



    #region BindOrderQty
    protected void BindOrderQty(string CustomerID, string SaleOrderID, string ProductID, string FactoryID)
    {
        clsSaleInvoiceMM Inv = new clsSaleInvoiceMM();
        DataTable dtOrderQty = new DataTable();
        dtOrderQty = Inv.BindOrderQty(CustomerID, SaleOrderID, ProductID, FactoryID);
        if (dtOrderQty.Rows.Count > 0)
        {
            this.ddlPacksize.Items.Clear();
            this.ddlPacksize.Items.Add(new ListItem(Convert.ToString(dtOrderQty.Rows[0]["UOMNAME"]), Convert.ToString(dtOrderQty.Rows[0]["UOMID"])));
            this.ddlPacksize.AppendDataBoundItems = true;
            this.ddlPacksize.SelectedValue = Convert.ToString(dtOrderQty.Rows[0]["UOMID"]);
            //this.txtOrderQty.Text = Convert.ToString(dtOrderQty.Rows[0]["ORDERQTY"]);
            //this.txtDespatchQty.Text = Convert.ToString(dtOrderQty.Rows[0]["DESPATCHQTY"]);
            this.txtRemainingQty.Text = Convert.ToString(dtOrderQty.Rows[0]["REMAININGQTY"]);

            this.txtMRP.Text = Convert.ToString(dtOrderQty.Rows[0]["MRP"]);
            this.txtStockQty.Text = Convert.ToString(dtOrderQty.Rows[0]["STOCKQTY"]);
        }
        else
        {
            //this.txtOrderQty.Text = "0";
            //this.txtDespatchQty.Text = "0";
            this.txtRemainingQty.Text = "0";
            this.txtBaseCost.Text = "0";
            this.txtMRP.Text = "0";
        }


        DataTable dt = new DataTable();
        ClsSaleorderMM cls = new ClsSaleorderMM();
        dt = cls.GetTradingSaleRate(CustomerID, ProductID, this.txtInvoiceDate.Text);/*same method call in sale order for rate made by p.basu on 25-12-2020*/

        if (this.hdnProductType.Value == "F")
        {
            if (dt.Rows.Count > 0)
            {
                this.txtBaseCost.Text = Convert.ToString(dtOrderQty.Rows[0]["RATE"]);

            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Product rate not avilable for above customer</font></b>");
            }
        }

    }
    #endregion

  

   

    #region ddlDistributor_SelectedIndexChanged
    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlDistributor.SelectedValue != "0")
        {
            string mode = "shiffingAdress";
            ClsVendor_TPU clsTpu = new ClsVendor_TPU();
            DataTable dt = new DataTable();
            dt = clsTpu.BindGateEntry(mode, this.ddlDistributor.SelectedValue.Trim());
            this.txtBillingAdress.Text = dt.Rows[0]["ADDRESS"].ToString();
            this.LoadOrderProduct();
            this.CreateStructure();
            this.txtRebate.Text = "0";

        }
    }
    #endregion

    #region ddlForm_SelectedIndexChanged
    protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.CreateStructure();
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlSaleOrder.ClientID + "').focus(); ", true);
    }
    #endregion

    #region BatchDetails
    protected void BatchDetails()
    {
        if (this.ddlProduct.SelectedValue != "0" && this.ddlPacksize.SelectedValue != "0")
        {
            if (this.ddlBatch.SelectedValue != "0")
            {
                if (this.hdnGroupID.Value != "0")
                {
                    DataTable dtBaseCost = new DataTable();
                    clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
                    string YMD = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());

                    this.txtStockQty.Text = ddlBatch.SelectedItem.Text.Substring(0, 15).Trim();
                    this.txtMRP.Text = ddlBatch.SelectedItem.Text.Substring(15, 11).Trim();
                    this.txtAssementPercentage.Text = ddlBatch.SelectedItem.Text.Substring(26, 14).Trim();

                    dtBaseCost = clsInvc.GetBaseCostPrice(this.ddlDistributor.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), YMD,
                                                          Convert.ToDecimal(this.txtMRP.Text.Trim()), this.ddlDepot.SelectedValue.Trim(),
                                                          Convert.ToString(ViewState["menuID"]).Trim(), Convert.ToString(ViewState["BSID"]).Trim(),
                                                          this.hdnGroupID.Value.Trim());

                    if (dtBaseCost.Rows.Count > 0)
                    {
                        this.txtBaseCost.Text = dtBaseCost.Rows[0]["BASECOSTPRICE"].ToString();
                    }
                    else
                    {
                        this.txtBaseCost.Text = "0.00";
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please select Group", 60, 450);
                }
            }
            else
            {
                this.txtMRP.Text = "0";
                this.txtStockQty.Text = "0";
                this.txtAssementPercentage.Text = "0";
                this.txtBaseCost.Text = "0";
            }
        }
        else
        {
            MessageBox1.ShowWarning("Please select Product and Packsize");
        }
    }
    #endregion

    #region BindDeliveryAddress
    protected void BindDeliveryAddress(string CustomerID)
    {
        clsSaleInvoiceMM ClsInv = new clsSaleInvoiceMM();
        DataTable dtAddress = new DataTable();
        dtAddress = ClsInv.DeliveryAddress(CustomerID);
        if (dtAddress.Rows.Count > 0)
        {
            if (dtAddress.Rows.Count > 1)
            {
                this.ddlDeliveryAddress.Items.Clear();
                this.ddlDeliveryAddress.Items.Add(new ListItem("Select Delivery Address", "0"));
                this.ddlDeliveryAddress.AppendDataBoundItems = true;
                this.ddlDeliveryAddress.DataSource = dtAddress;
                this.ddlDeliveryAddress.DataValueField = "ADDRESSID";
                this.ddlDeliveryAddress.DataTextField = "ADDRESS";
                this.ddlDeliveryAddress.DataBind();
            }
            else if (dtAddress.Rows.Count == 1)
            {
                this.ddlDeliveryAddress.Items.Clear();
                this.ddlDeliveryAddress.Items.Add(new ListItem("Select Delivery Address", "0"));
                this.ddlDeliveryAddress.AppendDataBoundItems = true;
                this.ddlDeliveryAddress.DataSource = dtAddress;
                this.ddlDeliveryAddress.DataValueField = "ADDRESSID";
                this.ddlDeliveryAddress.DataTextField = "ADDRESS";
                this.ddlDeliveryAddress.DataBind();
                this.ddlDeliveryAddress.SelectedValue = Convert.ToString(dtAddress.Rows[0]["ADDRESSID"]);
            }
        }
        else
        {
            this.ddlDeliveryAddress.Items.Clear();
            this.ddlDeliveryAddress.Items.Add(new ListItem("Select Delivery Address", "0"));
            this.ddlDeliveryAddress.AppendDataBoundItems = true;
        }
    }
    #endregion

  

    #region LoadOrderProduct
    protected void LoadOrderProduct()
    {

        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataTable dtOrderDetails = new DataTable();
        if (hdnDespatchID.Value == "")
        {
           
                mode = "AllProduct";
                dtOrderDetails = clsInvc.BindOrderProduct(mode, this.ddlDistributor.SelectedValue.Trim());
            
        }
        else
        {
            if (this.hdnbilltype.Value == "N")
            {
                mode = "AllProduct";
                dtOrderDetails = clsInvc.BindOrderProduct(mode, this.ddlDistributor.SelectedValue.Trim());
            }
        }


        if (dtOrderDetails.Rows.Count > 0)
        {
            if (hdnDespatchID.Value == "")
            {
                
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                    this.ddlProduct.AppendDataBoundItems = true;
                    this.ddlProduct.DataSource = dtOrderDetails;
                    this.ddlProduct.DataValueField = "PRODUCTID";
                    this.ddlProduct.DataTextField = "PRODUCTNAME";
                    this.ddlProduct.DataBind();
            }
            else
            {
                if (this.hdnbilltype.Value == "N")
                {
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                    this.ddlProduct.AppendDataBoundItems = true;
                    this.ddlProduct.DataSource = dtOrderDetails;
                    this.ddlProduct.DataValueField = "PRODUCTID";
                    this.ddlProduct.DataTextField = "PRODUCTNAME";
                    this.ddlProduct.DataBind();
                }
                else
                {
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                    this.ddlProduct.AppendDataBoundItems = true;
                    this.ddlProduct.DataSource = dtOrderDetails;
                    this.ddlProduct.DataValueField = "PRODUCTID";
                    this.ddlProduct.DataTextField = "PRODUCTNAME";
                    this.ddlProduct.DataBind();
                    this.txtOrderDate.Text = Convert.ToString(dtOrderDetails.Rows[0]["SALEORDERDATE"]);
                    this.hdnGroupID.Value = Convert.ToString(dtOrderDetails.Rows[0]["GROUPID"]);
                }
            }

        }
        else
        {
            this.ddlProduct.Items.Clear();
            this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
            this.ddlProduct.AppendDataBoundItems = true;
            this.txtOrderDate.Text = "";
        }
    }
    #endregion

    #region CalculateTotalMRP
    decimal CalculateTotalMRP(DataTable dt)
    {
        decimal GrossTotal = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["TOTMRP"]);


        }


        return GrossTotal;
    }
    #endregion

    #region CalculateTotalSpecialDisc
    decimal CalculateTotalSpecialDisc(DataTable dt)
    {
        decimal GrossTotal = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["Amount"]);
        }
        return GrossTotal;
    }
    #endregion

    #region Add Records
    protected void btnADDGrid_Click(object sender, EventArgs e)
    {


        string BATCHNO = "";
        if (this.hdnDespatchID.Value == "")
        {
           
            if (this.hdnbilltype.Value == "N")
            {
               // this.txtOrderQty.Text = "0";
                this.txtRemainingQty.Text = "0";
            }
        }

        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataTable dtInvoice = (DataTable)Session["SALEINVOICEDETAILS"];
        string Brand = string.Empty;
        string Category = string.Empty;
        string HSNCODE = string.Empty;
        string PrimaryPriceScheme = string.Empty;
        decimal deliveredqty = Convert.ToDecimal(this.txtDeliveredQtyPCS.Text.Trim());
        decimal orderedqty = 0;
            //Convert.ToDecimal(this.txtOrderQty.Text.Trim());
        decimal stockqty = Convert.ToDecimal(this.txtStockQty.Text.Trim());
        decimal RemainingQty = 0;
            //Convert.ToDecimal(this.txtRemainingQty.Text.Trim());
        decimal TotalAmount = 0;
        decimal TotalTax = 0;
        decimal GrossTotal = 0;
        decimal GrossFinal = 0;
        decimal TotalMRP = 0;
        string ymd = string.Empty;
        decimal RateDisc = 0;
        decimal NSR = 0;
        decimal MRP = Convert.ToDecimal(this.txtMRP.Text.Trim());
        decimal AssementPercentage = 0;
        TotalMRP = Convert.ToDecimal(this.txtMRP.Text.Trim()) * deliveredqty;

        string isService = this.hdnProductType.Value;

        if (deliveredqty > 0 && isService == "F")
        {
            if (Convert.ToDecimal(this.txtBaseCost.Text.Trim()) == 0)
            {
                MessageBox1.ShowInfo("<b>Please check product rate</b>");
                return;
            }
            

            if (deliveredqty > stockqty)
            {

                MessageBox1.ShowInfo("<b>Delivered Qty should not be greater than Stock Qty</b>", 40, 550);
                return;


            }
            int numberOfRecords = dtInvoice.Select("PRODUCTID = '" + this.ddlProduct.SelectedValue + "'").Length;
            if (this.chkFree.Checked == false)
            {
                if (numberOfRecords > 0)
                {
                    MessageBox1.ShowInfo("<b>Record already Exists..</b>");
                    this.ResetControls();
                    return;
                }
                else
                {
                    ymd = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());
                    NSR = Convert.ToDecimal(this.txtBaseCost.Text.Trim());

                    this.FillPrimaryGridView(deliveredqty, "0",
                                                     this.ddlPacksize.SelectedValue.Trim(),
                                                     Convert.ToString(this.ddlPacksize.SelectedItem).Trim(),
                                                     this.txtOrderDate.Text.Trim(), this.ddlProduct.SelectedValue.Trim(),
                                                     Convert.ToString(this.ddlProduct.SelectedItem).Trim(), BATCHNO,
                                                     Convert.ToDecimal(this.txtBaseCost.Text.Trim()), MRP, AssementPercentage,
                                                     TotalAmount, TotalTax, GrossTotal, GrossFinal, TotalMRP, RateDisc, NSR);
                }
            }

        }
        else if (isService == "T")
        {
            this.FillPrimaryGridView(deliveredqty, "0",
                                                     this.ddlPacksize.SelectedValue.Trim(),
                                                     Convert.ToString(this.ddlPacksize.SelectedItem).Trim(),
                                                     this.txtOrderDate.Text.Trim(), this.ddlProduct.SelectedValue.Trim(),
                                                     Convert.ToString(this.ddlProduct.SelectedItem).Trim(), BATCHNO,
                                                     Convert.ToDecimal(this.txtBaseCost.Text.Trim()), MRP, AssementPercentage,
                                                     TotalAmount, TotalTax, GrossTotal, GrossFinal, TotalMRP, RateDisc, NSR);
        }
        else
        {
            MessageBox1.ShowInfo("<b>Delivered Qty should be greater than 0</b>", 40, 550);
        }


        #region Grid Calculation
        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.GridAddDespatchStyle();
            this.BillingGridCalculation();
        }
        if (this.grdGST.Rows.Count > 0)
        {
            this.GSTGridCalculation();
        }

        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
            this.TotalCaseCalculation();
        }
        if (this.grdGST.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
            this.TotalCaseCalculation();
        }
        if (this.grdAddDespatch.Rows.Count == 0 && this.grdGST.Rows.Count == 0)
        {
            this.txtTotDisc.Text = "0";
            this.txtTotPCS.Text = "0";
            this.txtTotCase.Text = "0";
        }
        #endregion

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlProduct.ClientID + "').focus(); ", true);
    }
    #endregion

    #region CreateDisplayFreeDataTable Structure
    public DataTable CreateDisplayFreeDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEMEID", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEME_PRODUCT_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEME_PRODUCT_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEME_PRODUCT_BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEME_QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTAL_QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("DISC", typeof(string)));
        dt.Columns.Add(new DataColumn("BRate", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));
        dt.Columns.Add(new DataColumn("NSR", typeof(string)));
        dt.Columns.Add(new DataColumn("RATEDISCVALUE", typeof(string)));

        HttpContext.Current.Session["DISPLAYSCHEMEDETAILS"] = dt;
        return dt;
    }
    #endregion    

    #region Free Product/Special Discount
    protected void SpecialDiscount(DataTable dt1, DataTable dt2, decimal TotalDisc)
    {
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        string TAXID = string.Empty;
        /*decimal ProductWiseTax = 0;*/
        int flag = 0;
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim() != "-NA")
                {

                    //decimal TotAssesmentFreeQty = clsInvc.CalculateAmountInPcs(Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(dt1.Rows[i]["PACKSIZEID"]).Trim(),
                    //                                       Convert.ToDecimal(Convert.ToString(dt1.Rows[i]["SCHEME_QTY"]).Trim()), Convert.ToDecimal(Convert.ToString(dt1.Rows[i]["MRP"]).Trim()), 0, 0)
                    //                                       * Convert.ToDecimal(Convert.ToString(dt1.Rows[i]["ASSESMENTPERCENTAGE"]).Trim()) / 100;

                    decimal TotAssesmentFreeQty = 0;

                    DataRow drQty = dt2.NewRow();
                    drQty["GUID"] = Guid.NewGuid();
                    drQty["SCHEMEID"] = Convert.ToString(dt1.Rows[i]["SCHEMEID"]).Trim();
                    drQty["SCHEME_PRODUCT_ID"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_ID"]).Trim();
                    drQty["SCHEME_PRODUCT_NAME"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_NAME"]).Trim();
                    drQty["SCHEME_PRODUCT_BATCHNO"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_BATCHNO"]).Trim();
                    drQty["QTY"] = Convert.ToString(dt1.Rows[i]["QTY"]).Trim();
                    drQty["PRODUCTID"] = Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim();
                    drQty["PRODUCTNAME"] = Convert.ToString(dt1.Rows[i]["PRODUCTNAME"]).Trim();
                    drQty["HSNCODE"] = clsInvc.HSNCode(Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim());
                    drQty["PACKSIZEID"] = Convert.ToString(dt1.Rows[i]["PACKSIZEID"]).Trim();
                    drQty["PACKSIZENAME"] = Convert.ToString(dt1.Rows[i]["PACKSIZENAME"]).Trim();
                    drQty["SCHEME_QTY"] = Convert.ToString(dt1.Rows[i]["SCHEME_QTY"]).Trim();
                    drQty["MRP"] = Convert.ToString(dt1.Rows[i]["MRP"]).Trim();
                    drQty["RATEDISC"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["DISC"])).Trim();
                    drQty["BRate"] = Convert.ToString(dt1.Rows[i]["BRate"]).Trim();
                    drQty["Amount"] = Convert.ToString(dt1.Rows[i]["Amount"]).Trim();
                    drQty["BATCHNO"] = Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim();
                    drQty["ASSESMENTPERCENTAGE"] = Convert.ToString(dt1.Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    drQty["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesmentFreeQty));//TOTALASSESMENTVALUE
                    drQty["MFDATE"] = Convert.ToString(dt1.Rows[i]["MFDATE"]).Trim();
                    drQty["EXPRDATE"] = Convert.ToString(dt1.Rows[i]["EXPRDATE"]).Trim();
                    drQty["WEIGHT"] = Convert.ToString(dt1.Rows[i]["WEIGHT"]).Trim();
                    drQty["NSR"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["NSR"])).Trim();
                    drQty["RATEDISCVALUE"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["Amount"])).Trim();

                    decimal FreeTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {

                            case "1":
                                /*TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                ProductWiseTax = clsInvc.GetHSNTax(TAXID, Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim());
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim()) * ProductWiseTax / 100));
                                FreeTaxAmt += Convert.ToDecimal(drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);*/
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", 0));
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim()) * 0 / 100));
                                break;
                        }

                        flag = 0;
                        if (Arry.Count > 0)
                        {
                            foreach (string row in Arry)
                            {
                                if (row.Contains(dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim()))
                                {
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 0;
                                }
                            }
                            if (flag == 0)
                            {
                                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            }
                        }
                        else
                        {
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                        }

                        /*CreateTaxDatatable( this.ddlSaleOrder.SelectedValue.Trim(),
                                            Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_ID"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_BATCHNO"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim(),
                                            dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                            Convert.ToString(ProductWiseTax),
                                            drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                            TAXID,
                                            "F");*/
                    }
                    #endregion

                    drQty["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", FreeTaxAmt + Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim())));

                    dt2.Rows.Add(drQty);
                    dt2.AcceptChanges();
                }
            }

            Session["INVOICEQTYSCHEMEDETAILS"] = dt2;
            this.grdQtySchemeDetails.DataSource = dt2;
            this.grdQtySchemeDetails.DataBind();

            if (Session["OFFERPACKSCHEMEDETAILS"] != null)
            {
                DataTable dt3 = (DataTable)Session["OFFERPACKSCHEMEDETAILS"];
                if (dt3.Rows.Count > 0)
                {
                    this.grdGST.DataSource = dt3;
                    this.grdGST.DataBind();
                }
                else
                {
                    this.grdGST.DataSource = null;
                    this.grdGST.DataBind();
                }
            }
        }
        else
        {
            if (dt2.Rows.Count == 0)
            {
                this.grdQtySchemeDetails.DataSource = null;
                this.grdQtySchemeDetails.DataBind();
            }

        }

    }
    #endregion

    #region Special Discount OfferPack
    protected void OfferPackSpecialDiscount(DataTable dt1, DataTable dt2, decimal TotalDisc)
    {
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        string TAXID = string.Empty;
        /*decimal ProductWiseTax = 0;*/
        int flag = 0;
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim() != "-NA")
                {
                    decimal TotAssesmentFreeQty = 0;

                    DataRow drQty = dt2.NewRow();
                    drQty["GUID"] = Guid.NewGuid();
                    drQty["SCHEMEID"] = Convert.ToString(dt1.Rows[i]["SCHEMEID"]).Trim();
                    drQty["SCHEME_PRODUCT_ID"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_ID"]).Trim();
                    drQty["SCHEME_PRODUCT_NAME"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_NAME"]).Trim();
                    drQty["SCHEME_PRODUCT_BATCHNO"] = Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_BATCHNO"]).Trim();
                    drQty["QTY"] = Convert.ToString(dt1.Rows[i]["QTY"]).Trim();
                    drQty["PRODUCTID"] = Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim();
                    drQty["PRODUCTNAME"] = Convert.ToString(dt1.Rows[i]["PRODUCTNAME"]).Trim();
                    drQty["HSNCODE"] = clsInvc.HSNCode(Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim());
                    drQty["PACKSIZEID"] = Convert.ToString(dt1.Rows[i]["PACKSIZEID"]).Trim();
                    drQty["PACKSIZENAME"] = Convert.ToString(dt1.Rows[i]["PACKSIZENAME"]).Trim();
                    drQty["SCHEME_QTY"] = Convert.ToString(dt1.Rows[i]["SCHEME_QTY"]).Trim();
                    drQty["MRP"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["MRP"])).Trim();
                    drQty["RATEDISC"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["DISC"])).Trim();
                    drQty["BRate"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["BRate"])).Trim();
                    drQty["Amount"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["Amount"])).Trim();
                    drQty["BATCHNO"] = Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim();
                    drQty["ASSESMENTPERCENTAGE"] = Convert.ToString(dt1.Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    drQty["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesmentFreeQty));
                    drQty["MFDATE"] = Convert.ToString(dt1.Rows[i]["MFDATE"]).Trim();
                    drQty["EXPRDATE"] = Convert.ToString(dt1.Rows[i]["EXPRDATE"]).Trim();
                    drQty["WEIGHT"] = Convert.ToString(dt1.Rows[i]["WEIGHT"]).Trim();
                    drQty["NSR"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["NSR"])).Trim();
                    drQty["RATEDISCVALUE"] = Convert.ToString(String.Format("{0:0.00}", dt1.Rows[i]["Amount"])).Trim();

                    decimal FreeTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                /*TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                ProductWiseTax = clsInvc.GetHSNTax(TAXID, Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim());
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim()) * ProductWiseTax / 100));
                                FreeTaxAmt += Convert.ToDecimal(drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);*/
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", 0));
                                drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim()) * 0 / 100));

                                break;
                        }

                        flag = 0;
                        if (Arry.Count > 0)
                        {
                            foreach (string row in Arry)
                            {
                                if (row.Contains(dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim()))
                                {
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 0;
                                }
                            }
                            if (flag == 0)
                            {
                                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            }
                        }
                        else
                        {
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                        }

                        /*CreateTaxDatatable( "0", 
                                            Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_ID"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["SCHEME_PRODUCT_BATCHNO"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["PRODUCTID"]).Trim(),
                                            Convert.ToString(dt1.Rows[i]["BATCHNO"]).Trim(),
                                            dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                            Convert.ToString(ProductWiseTax),
                                            drQty["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                            TAXID, 
                                            "F");*/
                    }
                    #endregion

                    drQty["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", FreeTaxAmt + Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString().Trim())));
                    dt2.Rows.Add(drQty);
                    dt2.AcceptChanges();
                }
            }

            Session["OFFERPACKSCHEMEDETAILS"] = dt2;

            this.grdGST.DataSource = dt2;
            this.grdGST.DataBind();
        }
        else
        {
            if (dt2.Rows.Count == 0)
            {
                this.grdGST.DataSource = null;
                this.grdGST.DataBind();
            }

        }

    }
    #endregion

    #region ReturnBatchFlag
    private bool ReturnBatchFlag(DataTable dt)
    {
        bool flg = true;
        int i = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["BATCHNO"].ToString().Trim() == "-NA")
            {
                flg = false;
                break;
            }
        }
        return flg;
    }
    #endregion

    #region FillPrimaryGridView
    protected void FillPrimaryGridView(decimal deliveredqty, string SALEORDERID, string DDLPACKSIZEID, string DDLPACKSIZENAME,
                                        string ORDERDATE, string PRODUCTID, string PRODUCTNAME, string BATCHID,
                                        decimal BCP, decimal MRP, decimal ASSESMENTPERCENTAGE, decimal TotalAmount, decimal TotalTax,
                                        decimal GrossTotal, decimal GrossFinal, decimal TotalMRP,
                                        decimal RateDisc, decimal NSR)
    {
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataTable dtInvoice = (DataTable)Session["SALEINVOICEDETAILS"];
        string PrimaryGUID = Convert.ToString(Guid.NewGuid()).ToUpper();
        decimal Taxpercent = 0;
        decimal Amount = 0;
        decimal TotAssesment = 0;
        decimal SpecialDisc = 0;
        decimal TotMRP = 0;
        decimal Totaltax = 0;
        int flag = 0;
        string HSNCODE = clsInvc.HSNCode(PRODUCTID);

        //DispatchAmtPcs_Calculation Obj = new DispatchAmtPcs_Calculation();
        //Obj = clsstocktransfer.DispatchAmtPcs(PRODUCTID, DDLPACKSIZEID, deliveredqty, Convert.ToDecimal(MRP), Convert.ToDecimal(ASSESMENTPERCENTAGE));

        //List<GrossWeight> GrossWt = new List<GrossWeight>();
        //GrossWt = Obj.GROSSWEIGHT;
        //string _GrossWt = Convert.ToString(GrossWt[0].GROSSWEIGHT);
        if (this.txtRateDiscAmnt.Text == "")
        {
            this.txtRateDiscAmnt.Text = "0";
        }
        decimal DiscAmnt = 0;
        decimal ttAmnt = 0;
        decimal discPer = Convert.ToDecimal(this.txtRateDiscAmnt.Text);
        if (discPer > 0)
        {
            ttAmnt = (deliveredqty * NSR);
            DiscAmnt = (ttAmnt * discPer) / 100;
            Amount = (deliveredqty * NSR) - DiscAmnt;
        }
        else
        {
            Amount = (deliveredqty * NSR);
        }
        if (this.hdnProductType.Value == "T")
        {
            BCP = Convert.ToDecimal(this.txtBaseCost.Text);
            NSR = BCP;
            Amount = NSR * deliveredqty;
        }


        DataRow dr = dtInvoice.NewRow();
        dr["GUID"] = PrimaryGUID;
        dr["SALEORDERID"] = Convert.ToString(SALEORDERID);
        dr["SALEORDERDATE"] = Convert.ToString(ORDERDATE);
        dr["PRODUCTID"] = Convert.ToString(PRODUCTID);
        dr["PRODUCTNAME"] = Convert.ToString(PRODUCTNAME).Trim();
        dr["HSNCODE"] = HSNCODE.Trim();
        dr["PACKINGSIZEID"] = Convert.ToString(DDLPACKSIZEID);
        dr["PACKINGSIZENAME"] = Convert.ToString(DDLPACKSIZENAME).Trim();
        dr["MRP"] = Convert.ToString(MRP);
        dr["RATEDISC"] = Convert.ToString(RateDisc);
        dr["RATEDISCVALUE"] = Convert.ToString(String.Format("{0:0.00}", SpecialDisc));
        dr["BCP"] = Convert.ToString(BCP);
        dr["QTY"] = Convert.ToString("0");

        if (this.hdnProductType.Value == "T")
        {
            dr["QTYPCS"] = Convert.ToString(0);
        }
        else
        {
            dr["QTYPCS"] = Convert.ToString(deliveredqty);
        }

        dr["DISCPER"] = Convert.ToString(discPer);
        dr["DISCAMNT"] = Convert.ToString(DiscAmnt);
        dr["BATCHNO"] = Convert.ToString("NA");
        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ASSESMENTPERCENTAGE);
        dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesment));
        dr["PRIMARYPRICESCHEMEID"] = "";
        dr["PERCENTAGE"] = Convert.ToString("0.00");
        dr["VALUE"] = Convert.ToString(String.Format("{0:0.00}", "0.00"));
        if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
        {
            dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", Amount));
        }
        else
        {
            dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", 0));
        }
        dr["TOTMRP"] = Convert.ToString(String.Format("{0:0.00}", TotMRP));
        dr["WEIGHT"] = "0";

        //if (Obj.GROSSWEIGHT.Count > 0)
        //{
        //    dr["GROSSWEIGHT"] = _GrossWt;
        //}
        //else
        //{
        dr["GROSSWEIGHT"] = "0";
        //}

        dr["MFDATE"] = Convert.ToString(null);
        dr["EXPRDATE"] = Convert.ToString(null);
        dr["NSR"] = Convert.ToString(NSR);

        #region Loop For Adding Itemwise Tax Component
        DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];

        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
        {
            if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
            {
                Taxpercent = clsInvc.GetTaxPercentage(dtTaxCountDataAddition.Rows[k]["TAXID"].ToString(), this.ddlDistributor.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
            }
            else
            {
                Taxpercent = 0;
            }
            switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
            {
                case "1":

                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", Taxpercent));
                    double TaxValue = 0;
                    TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(Amount) * Taxpercent) / 100) * 100) / 100);
                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                    Totaltax += Convert.ToDecimal(String.Format("{0:0.00}", TaxValue));
                    break;
            }
            flag = 0;
            if (Arry.Count > 0)
            {
                foreach (string row in Arry)
                {
                    if (row.Contains(dtTaxCountDataAddition.Rows[k]["NAME"].ToString()))
                    {
                        flag = 1;
                        break;
                    }
                    else
                    {
                        flag = 0;
                    }
                }

                if (flag == 0)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
            }
            else
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
            }

            CreateTaxDatatable(SALEORDERID, Convert.ToString(PRODUCTID), Convert.ToString(BATCHID), Convert.ToString(PRODUCTID), Convert.ToString("NA"), dtTaxCountDataAddition.Rows[k]["TAXID"].ToString(), Convert.ToString(Taxpercent),
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString(), "P", Convert.ToString(MRP));
        }
        #endregion

        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", Totaltax + Amount));

        if (this.txtCboxQty.Text == "")
        {
            this.txtCboxQty.Text = "0";
        }

        dr["CBOXQTY"] = Convert.ToString(this.txtCboxQty.Text);

        dtInvoice.Rows.Add(dr);
        dtInvoice.AcceptChanges();

        #region Amount Calculation
        TotalMRP = CalculateTotalMRP(dtInvoice);
        TotalAmount = CalculateGrossTotal(dtInvoice);
        TotalTax = CalculateTaxTotal(dtInvoice);
        GrossTotal = TotalAmount - (Convert.ToDecimal(this.txtRebateValue.Text.Trim()) + Convert.ToDecimal(this.txtAddRebateValue.Text.Trim())) + TotalTax;
        GrossFinal = 0;

        this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
        this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
        #endregion

        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));

        #region Grid Calculation
        if (dtInvoice.Rows.Count > 0)
        {
            this.grdAddDespatch.DataSource = dtInvoice;
            this.grdAddDespatch.DataBind();
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
            this.GridAddDespatchStyle();
            this.BillingGridCalculation();
        }
        else
        {
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
        }
        if (this.grdGST.Rows.Count > 0)
        {
            this.GSTGridCalculation();
        }
        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
            this.TotalCaseCalculation();
        }
        if (this.grdGST.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
            this.TotalCaseCalculation();
        }
        if (this.grdAddDespatch.Rows.Count == 0 && this.grdGST.Rows.Count == 0)
        {
            this.txtTotDisc.Text = "0";
            this.txtTotPCS.Text = "0";
            this.txtTotCase.Text = "0";
        }
        #endregion

        this.ResetControls();
    }
    #endregion

    #region btn_TempDeleteFree_Click
    protected void btn_TempDeleteFree_Click(object sender, EventArgs e)
    {
        try
        {
            decimal TotalDisc = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            decimal TotalMRP = 0;
            int Taxflag = 0;
            decimal FretRebatePercentage = Convert.ToDecimal(this.txtRebate.Text.Trim());
            decimal FretRebateValue = 0;
            decimal AddFretRebatePercentage = Convert.ToDecimal(this.txtAddRebate.Text.Trim());
            decimal AddFretRebateValue = 0;

            //ImageButton btn_views = (ImageButton)sender;
            //GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            //Label lblFreeGridSchemeID = (Label)gvr.FindControl("lblFreeGridSchemeID");
            //Label lblFreeGridSCHEME_PRODUCT_ID = (Label)gvr.FindControl("lblFreeGridSCHEME_PRODUCT_ID");
            //string FreeGUID = lblFreeGridSchemeID.Text.Trim();
            //string SCHEME_PRODUCT_ID = lblFreeGridSCHEME_PRODUCT_ID.Text.Trim();

            ImageButton btn_views = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            string FreeGUID = gvr.Cells[2].Text.Trim();
            string SCHEME_PRODUCT_ID = gvr.Cells[3].Text.Trim();
            string BATCHNO = gvr.Cells[19].Text.Trim();

            if (FreeGUID == "0")
            {
                #region Free Issue
                DataTable dtdeleteInvoicerecord = new DataTable();
                dtdeleteInvoicerecord = (DataTable)Session["SALEINVOICEDETAILS"];

                DataTable dtdeleteQtyScheme = new DataTable();
                dtdeleteQtyScheme = (DataTable)Session["INVOICEQTYSCHEMEDETAILS"];
                DataRow[] drrQtyScheme = dtdeleteQtyScheme.Select("SCHEME_PRODUCT_ID='" + SCHEME_PRODUCT_ID + "' AND SCHEMEID='" + FreeGUID + "'");
                for (int j = 0; j < drrQtyScheme.Length; j++)
                {
                    drrQtyScheme[j].Delete();
                    dtdeleteQtyScheme.AcceptChanges();
                }
                HttpContext.Current.Session["INVOICEQTYSCHEMEDETAILS"] = dtdeleteQtyScheme;
                #endregion

                #region Item Wise Tax

                #region Loop For Adding Itemwise Tax
                DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];

                for (int k = 0; k < dtTaxCountDataAddition1.Rows.Count; k++)
                {
                    Taxflag = 0;
                    if (Arry.Count > 0)
                    {
                        foreach (string row in Arry)
                        {
                            if (row.Contains(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString().Trim()))
                            {
                                Taxflag = 1;
                                break;
                            }
                            else
                            {
                                Taxflag = 0;
                            }
                        }
                        if (Taxflag == 0)
                        {
                            Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                        }
                    }
                    else
                    {
                        Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                    }
                }

                #endregion

                DataTable dtdeleteInvoiceItemTax = new DataTable();
                dtdeleteInvoiceItemTax = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];

                DataRow[] drrTax = dtdeleteInvoiceItemTax.Select("PRIMARYPRODUCTID = '" + SCHEME_PRODUCT_ID + "' AND PRIMARYPRODUCTBATCHNO = '" + BATCHNO + "' AND TAG = 'F'");
                for (int i = 0; i < drrTax.Length; i++)
                {
                    drrTax[i].Delete();
                    dtdeleteInvoiceItemTax.AcceptChanges();
                }
                HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtdeleteInvoiceItemTax;
                #endregion

                #region Amount Calculation
                TotalMRP = CalculateTotalMRP(dtdeleteInvoicerecord);
                TotalAmount = CalculateGrossTotal(dtdeleteInvoicerecord);

                if (FretRebatePercentage == 0 && AddFretRebatePercentage == 0)
                {
                    TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord);
                    GrossTotal = TotalAmount + TotalTax;
                }
                else
                {
                    if (this.txtRebate.Text.Trim() == "")
                    {
                        this.txtRebateValue.Text = "0";
                    }
                    FretRebateValue = (TotalAmount * FretRebatePercentage) / 100;
                    AddFretRebateValue = (TotalAmount * AddFretRebatePercentage) / 100;
                    this.txtRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", FretRebateValue));
                    this.txtAddRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", AddFretRebateValue));
                    TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord) + CalculateTaxTotal(dtdeleteQtyScheme);
                    GrossTotal = TotalAmount - (Convert.ToDecimal(this.txtRebateValue.Text.Trim()) + Convert.ToDecimal(this.txtAddRebateValue.Text.Trim())) + TotalTax;
                }



                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                #endregion

                #region Tax On Gross Amount
                //DataTable dtGrossTax = (DataTable)Session["SaleInvoiceGrossTotalTax"];
                //if (dtGrossTax.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtGrossTax.Rows.Count; i++)
                //    {
                //        dtGrossTax.Rows[i]["TAXVALUE"] = (Convert.ToDecimal(dtGrossTax.Rows[i]["PERCENTAGE"]) * GrossTotal / 100);
                //    }

                //    dtGrossTax.AcceptChanges();

                //}
                //this.grdTax.DataSource = dtGrossTax;
                //this.grdTax.DataBind();
                //GrossFinal = CalculateFinalGrossTotal(dtGrossTax);
                GrossFinal = 0;
                #endregion

                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
                this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
                this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));

                #region Bind Grid
                if (dtdeleteQtyScheme.Rows.Count > 0)
                {
                    grdQtySchemeDetails.DataSource = dtdeleteQtyScheme;
                    grdQtySchemeDetails.DataBind();
                }
                else
                {
                    grdQtySchemeDetails.DataSource = null;
                    grdQtySchemeDetails.DataBind();
                }

                /*if (this.grdQtySchemeDetails.Rows.Count > 0)
                {
                    this.SchemeGridCalculation();
                }*/

                if (this.grdGST.Rows.Count > 0)
                {
                    this.GSTGridCalculation();
                }

                if (grdAddDespatch.Rows.Count > 0)
                {
                    this.TotalPcsCalculation();
                    this.TotalCaseCalculation();
                }

                if (grdAddDespatch.Rows.Count == 0)
                {
                    this.txtTotPCS.Text = "0";
                    this.txtTotCase.Text = "0";
                }
                #endregion

                MessageBox1.ShowSuccess("Record Deleted Successfully..");
            }
            else
            {
                MessageBox1.ShowInfo("Not allow to delete.");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CountTotalQty
    protected decimal CountTotalQty()
    {
        decimal ReturnValue = 0;

        if (this.grdBatchDetails.Rows.Count > 0)
        {
            for (int i = 0; i < grdBatchDetails.Rows.Count; i++)
            {
                TextBox txtgrdQty = (TextBox)grdBatchDetails.Rows[i].FindControl("txtgrdQty");
                Label lblgrdStockQty = (Label)grdBatchDetails.Rows[i].FindControl("lblgrdStockQty");
                if (txtgrdQty.Text != "" && Convert.ToDecimal(txtgrdQty.Text) > 0 && Convert.ToDecimal(lblgrdStockQty.Text) > 0)
                {
                    ReturnValue += Convert.ToDecimal(txtgrdQty.Text.Trim());

                }
            }
        }
        return ReturnValue;
    }
    #endregion

    #region grdAddDespatch Delete
    protected void btn_TempDelete_Click(object sender, EventArgs e)
    {
        try
        {
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            decimal TotalMRP = 0;
            /*decimal TotalDisc = 0;*/
            decimal FretRebatePercentage = Convert.ToDecimal(this.txtRebate.Text.Trim());
            decimal FretRebateValue = 0;
            decimal AddFretRebatePercentage = Convert.ToDecimal(this.txtAddRebate.Text.Trim());
            decimal AddFretRebateValue = 0;
            int Taxflag = 0;

            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            this.hdndtDespatchDelete.Value = gvr.Cells[1].Text.Trim();
            this.hdndtPOIDDelete.Value = gvr.Cells[2].Text.Trim();
            this.hdndtPRODUCTIDDelete.Value = gvr.Cells[4].Text.Trim();
            this.hdndtBATCHDelete.Value = gvr.Cells[15].Text.Trim();
            string invoiceGUID = Convert.ToString(hdndtDespatchDelete.Value);
            string invoiceSALEORDERID = Convert.ToString(hdndtPOIDDelete.Value);
            string invoicePRODUCTID = Convert.ToString(hdndtPRODUCTIDDelete.Value);
            string invoiceBATCH = Convert.ToString(hdndtBATCHDelete.Value);
            DataTable dtdeleteQtyScheme = new DataTable();
            DataTable dtdeleteOfferPack = new DataTable();

            #region Product Details
            DataTable dtdeleteInvoicerecord = new DataTable();
            dtdeleteInvoicerecord = (DataTable)Session["SALEINVOICEDETAILS"];
            DataRow[] drr = dtdeleteInvoicerecord.Select("GUID='" + invoiceGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteInvoicerecord.AcceptChanges();
            }
            HttpContext.Current.Session["SALEINVOICEDETAILS"] = dtdeleteInvoicerecord;
            grdAddDespatch.DataSource = dtdeleteInvoicerecord;
            grdAddDespatch.DataBind();
            #endregion

            #region Item Wise Tax

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];

            if (dtTaxCountDataAddition1.Rows.Count > 0)
            {
                for (int k = 0; k < dtTaxCountDataAddition1.Rows.Count; k++)
                {
                    Taxflag = 0;
                    if (Arry.Count > 0)
                    {
                        foreach (string row in Arry)
                        {
                            if (row.Contains(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString().Trim()))
                            {
                                Taxflag = 1;
                                break;
                            }
                            else
                            {
                                Taxflag = 0;
                            }
                        }
                        if (Taxflag == 0)
                        {
                            Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                        }
                    }
                    else
                    {
                        Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                    }
                }
            }

            #endregion

            DataTable dtdeleteInvoiceItemTax = new DataTable();
            dtdeleteInvoiceItemTax = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];

            DataRow[] drrTax = dtdeleteInvoiceItemTax.Select("PRIMARYPRODUCTID = '" + invoicePRODUCTID + "'");
            for (int i = 0; i < drrTax.Length; i++)
            {
                drrTax[i].Delete();
                dtdeleteInvoiceItemTax.AcceptChanges();
            }
            HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtdeleteInvoiceItemTax;
            #endregion

            #region Amount Calculation
            TotalMRP = CalculateTotalMRP(dtdeleteInvoicerecord);
            TotalAmount = CalculateGrossTotal(dtdeleteInvoicerecord);

            if (FretRebatePercentage == 0 && AddFretRebatePercentage == 0)
            {
                TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord);
                GrossTotal = TotalAmount + TotalTax;
                this.txtRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", 0));
                this.txtAddRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", 0));
            }
            else if (FretRebatePercentage > 0 && AddFretRebatePercentage > 0)
            {

                if (this.txtRebateValue.Text.Trim() == "")
                {
                    this.txtRebateValue.Text = "0";
                }
                if (this.txtAddRebateValue.Text.Trim() == "")
                {
                    this.txtAddRebateValue.Text = "0";
                }
                FretRebateValue = (TotalAmount * FretRebatePercentage) / 100;
                AddFretRebateValue = (TotalAmount * AddFretRebatePercentage) / 100;
                this.txtRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", FretRebateValue));
                this.txtAddRebateValue.Text = Convert.ToString(String.Format("{0:0.00}", AddFretRebateValue));
                TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord);
                GrossTotal = TotalAmount - (Convert.ToDecimal(this.txtRebateValue.Text.Trim()) + Convert.ToDecimal(this.txtAddRebateValue.Text.Trim())) + TotalTax;
            }

            this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
            this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
            this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            #endregion

            #region Tax On Gross Amount
            GrossFinal = 0;
            #endregion

            this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
            this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
            this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));

            this.rdbtcs.SelectedValue = "N";
            this.txttcspercent.Text = "0";
            this.txttcsamt.Text = "0";
            this.txtnetwithtcsamt.Text = "0";

            #region Grid Bind
            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridAddDespatchStyle();
                this.BillingGridCalculation();

            }
            else
            {
                this.txtRebateValue.Text = "0";
            }
            if (this.grdGST.Rows.Count > 0)
            {
                this.GSTGridCalculation();
            }

            if (grdAddDespatch.Rows.Count > 0)
            {

                this.TotalPcsCalculation();
                this.TotalCaseCalculation();

            }
            if (this.grdGST.Rows.Count > 0)
            {
                this.TotalPcsCalculation();
                this.TotalCaseCalculation();

            }

            if (this.grdAddDespatch.Rows.Count == 0 && this.grdGST.Rows.Count == 0)
            {
                this.txtTotDisc.Text = "0";
                this.txtTotPCS.Text = "0";
                this.txtTotCase.Text = "0";
            }
            #endregion


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region grdTerms_RowDataBound
    protected void grdTerms_RowDataBound(object sender, GridRowEventArgs e)
    {
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataSet ds = new DataSet();
        if (hdnDespatchID.Value != "")
        {
            ds = clsInvc.EditInvoiceDetails(hdnDespatchID.Value);
        }

        if (e.Row.RowType == GridRowType.DataRow)
        {

            GridDataControlFieldCell chkcell = e.Row.Cells[2] as GridDataControlFieldCell;
            CheckBox chk = chkcell.FindControl("ChkIDTERMS") as CheckBox;
            HiddenField hiddenField = chkcell.FindControl("hdnTERMSName") as HiddenField;
            if (hdnDespatchID.Value != "")
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                    {
                        if (ds.Tables[5].Rows[i]["TERMSID"].ToString() == chk.ToolTip)
                        {
                            chk.Checked = true;

                        }
                    }
                }
            }
        }
    }
    #endregion

    #region ConvertDatatableToXMLInvoiceDetails
    public string ConvertDatatableToXMLInvoiceDetails(DataTable dt)
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

    #region ConvertDatatableToXMLItemWiseTaxDetails
    public string ConvertDatatableToXMLItemWiseTaxDetails(DataTable dt)
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

    #region ConvertDatatableToXMLGrossTaxDetails
    public string ConvertDatatableToXMLGrossTaxDetails(DataTable dt)
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

    #region ConvertDatatableToXMLProductDetails
    public string ConvertDatatableToXMLProductDetails(DataTable dt)
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

    #region ConvertDatatableToXMLPrimaryQtySchemeDetails
    public string ConvertDatatableToXMLPrimaryQtySchemeDetails(DataTable dt)
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

    #region Conver_To_YMD
    public string Conver_To_YMD(string dt)
    {

        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + '-' + month + '-' + day + " 00:00:00.000";
        return dt;

    }
    #endregion

    #region Delete Invoice
    protected void DeleteRecordInvoice(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            int flag = 0;
            string Verify = "";
            Verify = clsInvc.CheckVerify(e.Record["SALEINVOICEID"].ToString().Trim());
            if (Verify == "Y")
            {
                e.Record["Error"] = "Approve SaleInvoice Can not be Delete.";
            }
            else
            {
                flag = clsInvc.InvoiceDelete(e.Record["SALEINVOICEID"].ToString().Trim());
                this.hdnDespatchID.Value = "";
                if (flag == 1)
                {
                    this.LoadInvoice();
                    e.Record["Error"] = "Record Deleted Successfully. ";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
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

    #region Save Invoice
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtInvoiceDate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {

                if (txtTotPCS.Text == "0")
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Please add atleast 1 product.</font></b>");
                }
                else if (txtTotTax.Text == "0")
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Please Map Tax.</font></b>");
                }
                else
                {
                    clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
                    int CountTerms = 0;
                    string InvoiceNo = string.Empty;
                    string xml = string.Empty;
                    string xmlTax = string.Empty;
                    string xmlGrossTax = string.Empty;
                    string xmlQtyScheme = string.Empty;
                    string xmlOfferPack = string.Empty;
                    string xmlProductDetails = string.Empty;
                    string strTaxID = string.Empty;
                    string strTaxPercentage = string.Empty;
                    string strTaxValue = string.Empty;
                    string strTermsID = string.Empty;
                    string saleOrderDate = string.Empty;
                    decimal Disc = 0;
                    string RebateSchemeID = string.Empty;
                    decimal RebatePercentage = 0;
                    decimal RebateValue = 0;
                    decimal AddRebatePercentage = 0;
                    decimal AddRebateValue = 0;
                    string InvoiceType = string.Empty;
                    string GrossWght = string.Empty;
                    string IsChallan = string.Empty;

                    DataTable dtOfferPack = new DataTable();
                    DataTable dtTaxCount = (DataTable)Session["dtInvoiceTaxCount"];
                    DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["SALEINVOICEDETAILS"];
                    DataTable dtTaxRecordsCheck = (DataTable)HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"];

                    if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
                    {
                        IsChallan = "N";
                    }
                    else
                    {
                        IsChallan = "Y";
                    }

                    if (dtTaxCount.Rows.Count == 0)
                    {
                        InvoiceType = "0";
                    }
                    else if (dtTaxCount.Rows.Count == 1)
                    {
                        InvoiceType = "1";
                    }
                    else if (dtTaxCount.Rows.Count == 2)
                    {
                        InvoiceType = "2";
                    }
                    else if (dtTaxCount.Rows.Count == 3)
                    {
                        InvoiceType = "3";
                    }
                    if (dtRecordsCheck.Rows.Count > 0)
                    {
                        xml = ConvertDatatableToXMLInvoiceDetails(dtRecordsCheck);
                    }
                    if (dtTaxRecordsCheck.Rows.Count > 0)
                    {
                        xmlTax = ConvertDatatableToXMLItemWiseTaxDetails(dtTaxRecordsCheck);
                    }

                    #region grdTerms Loop
                    DataTable dtTerms = (DataTable)Session["Terms"];
                    if (dtTerms.Rows.Count > 0)
                    {
                        for (int j = 0; j < grdTerms.Rows.Count; j++)
                        {
                            GridDataControlFieldCell chkcell = grdTerms.RowsInViewState[j].Cells[2] as GridDataControlFieldCell;
                            CheckBox chk = chkcell.FindControl("ChkIDTERMS") as CheckBox;
                            HiddenField hiddenField = chkcell.FindControl("hdnTERMSName") as HiddenField;

                            if (chk.Checked == true)
                            {
                                CountTerms = CountTerms + 1;
                                strTermsID = strTermsID + hiddenField.Value.ToString().ToString() + ",";
                            }

                        }
                        if (CountTerms > 0)
                        {
                            strTermsID = strTermsID.Substring(0, strTermsID.Length - 1);
                        }
                    }

                    #endregion

                    #region Amount

                    decimal OtherCharges = 0;
                    decimal Adjustment = 0;
                    decimal Roundoff = Convert.ToDecimal(this.txtRoundoff.Text);
                    decimal TotalInvoiceValue = Convert.ToDecimal(this.txtFinalAmt.Text);
                    decimal ActualTotalCase = 0;
                    if (this.txtTotCase.Text == "")
                    {
                        ActualTotalCase = 0;
                    }
                    else
                    {
                        ActualTotalCase = Convert.ToDecimal(this.txtTotCase.Text.Trim());
                    }
                    GrossWght = "0";
                    #endregion

                    if (this.hdnGrossSchemeID.Value == "")
                    {
                        RebateSchemeID = "NA";
                    }
                    else
                    {
                        RebateSchemeID = this.hdnGrossSchemeID.Value.Trim();
                    }
                    if (this.txtRebate.Text == "")
                    {
                        RebatePercentage = 0;
                    }
                    else
                    {
                        RebatePercentage = Convert.ToDecimal(this.txtRebate.Text.Trim());
                    }
                    if (this.txtRebateValue.Text == "")
                    {
                        RebateValue = 0;
                    }
                    else
                    {
                        RebateValue = Convert.ToDecimal(this.txtRebateValue.Text.Trim());
                    }

                    if (this.txtAddRebate.Text == "")
                    {
                        AddRebatePercentage = 0;
                    }
                    else
                    {
                        AddRebatePercentage = Convert.ToDecimal(this.txtAddRebate.Text.Trim());
                    }
                    if (this.txtAddRebateValue.Text == "")
                    {
                        AddRebateValue = 0;
                    }
                    else
                    {
                        AddRebateValue = Convert.ToDecimal(this.txtAddRebateValue.Text.Trim());
                    }

                    if (this.txtTotDisc.Text == "")
                    {
                        Disc = 0;
                    }
                    else
                    {
                        Disc = Convert.ToDecimal(this.txtTotDisc.Text.Trim());
                    }
                    string billingType = string.Empty;
                    

                    InvoiceNo = clsInvc.InsertInvoiceDetails(this.txtInvoiceDate.Text.Trim(), this.ddlDistributor.SelectedValue.Trim(),
                                                             Convert.ToString(this.ddlDistributor.SelectedItem).Trim(), this.ddlWaybill.Text,
                                                             this.ddlTransporter.SelectedValue, this.txtVehicle.Text.Trim(), this.ddlDepot.SelectedValue,
                                                             Convert.ToString(this.ddlDepot.SelectedItem).Trim(),
                                                             Convert.ToString(ViewState["BSID"]),
                                                             this.hdnGroupID.Value.Trim(), this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                             this.txtGetpass.Text.Trim(), this.txtGetPassDate.Text.Trim(), this.ddlPaymentMode.SelectedValue.Trim(),
                                                             this.ddlTransportMode.SelectedValue, HttpContext.Current.Session["UserID"].ToString(),
                                                             HttpContext.Current.Session["FINYEAR"].ToString(), this.txtRemarks.Text.Trim(), TotalInvoiceValue, OtherCharges,
                                                             Adjustment, Roundoff, Disc,
                                                             strTermsID,
                                                             xml, xmlTax,
                                                             Convert.ToString(hdnDespatchID.Value),
                                                             Convert.ToString(ViewState["menuID"]),
                                                             "0",
                                                             "",
                                                             this.txtICDS.Text.Trim(), this.txtICDSDate.Text.Trim(),
                                                             RebateSchemeID, RebatePercentage, RebateValue, AddRebatePercentage, AddRebateValue,
                                                             "", this.ddlForm.SelectedValue.Trim(),
                                                             Convert.ToDecimal(this.txtTotPCS.Text.Trim()),
                                                             ActualTotalCase, InvoiceType.Trim(),
                                                             GrossWght.Trim(), IsChallan.Trim(),
                                                             this.ddlinsurancecompname.SelectedValue.Trim(),
                                                             this.ddlinsurancecompname.SelectedItem.ToString().Trim(),
                                                             this.ddlinsuranceno.SelectedValue.Trim(), billingType, rdbtcs.SelectedValue.ToString(),
                                                             Convert.ToDecimal(txttcspercent.Text),
                                                             Convert.ToDecimal(txttcsamt.Text),
                                                             Convert.ToDecimal(txtnetwithtcsamt.Text),
                                                             Convert.ToDecimal(txtFinalAmt.Text),
                                                             Convert.ToDecimal(txtUserInputFreight.Text),
                                                             Convert.ToString(txtBillingAdress.Text),
                                                             Convert.ToString(this.ddlShippingAdress.SelectedValue)
                                                             );

                    if (InvoiceNo != "")
                    {
                        this.grdAddDespatch.DataSource = null;
                        this.grdAddDespatch.DataBind();

                        if (Convert.ToString(hdnDespatchID.Value) == "")
                        {
                            if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
                            {
                                MessageBox1.ShowSuccess("Sale Invoice :<b><font color='green'>  " + InvoiceNo + "</font></b>  Saved Successfully", 40, 550);
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("Challan :<b><font color='green'>  " + InvoiceNo + "</font></b>  Saved Successfully", 40, 550);
                            }
                            this.trAutoInvoiceNo.Style["display"] = "none";
                            this.pnlDisplay.Style["display"] = "";
                            this.pnlAdd.Style["display"] = "none";
                            this.btnaddhide.Style["display"] = "";
                            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            this.ClearControls();
                            this.LoadInvoice();
                        }
                        else
                        {
                            if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
                            {
                                MessageBox1.ShowSuccess("Sale Invoice : <b><font color='green'> " + InvoiceNo + " </font></b> Updated Successfully", 40, 550);
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("Challan : <b><font color='green'> " + InvoiceNo + " </font></b> Updated Successfully", 40, 550);
                            }
                            this.trAutoInvoiceNo.Style["display"] = "none";
                            this.pnlDisplay.Style["display"] = "";
                            this.pnlAdd.Style["display"] = "none";
                            this.btnaddhide.Style["display"] = "";
                            this.ClearControls();
                            this.ResetControls();
                            this.grdAddDespatch.DataSource = null;
                            this.grdAddDespatch.DataBind();
                            this.grdQtySchemeDetails.DataSource = null;
                            this.grdQtySchemeDetails.DataBind();
                            this.grdTax.ClearPreviousDataSource();
                            this.grdTax.DataSource = null;
                            this.grdTax.DataBind();
                            this.grdTerms.ClearPreviousDataSource();
                            this.grdTerms.DataSource = null;
                            this.grdTerms.DataBind();
                            this.hdnDespatchID.Value = "";
                            this.LoadInvoice();

                            #region Enable Controls
                            //this.ImageButton1.Enabled = true;
                            this.ddlDepot.Enabled = true;
                            this.ddlDistributor.Enabled = true;
                            #endregion
                        }

                        this.hdnDespatchID.Value = "";
                        this.ResetSession();
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Error on Saving record..</font></b>");
                    }
                }
            }
            else
            {
                MessageBox1.ShowInfo("Entry Date is Locked, Please Contact to Admin", 60, 500);
            }
        }

        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }
    #endregion

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.hdnDespatchID.Value = "";
        this.trAutoInvoiceNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.ClearControls();
        this.ResetControls();
        this.ResetSession();
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grdQtySchemeDetails.DataSource = null;
        this.grdQtySchemeDetails.DataBind();
        this.grdGST.DataSource = null;
        this.grdGST.DataBind();
        this.grdTax.ClearPreviousDataSource();
        this.grdTax.DataSource = null;
        this.grdTax.DataBind();
        this.grdTerms.ClearPreviousDataSource();
        this.grdTerms.DataSource = null;
        this.grdTerms.DataBind();
        // this.LoadInvoice();
        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = true;
        
        this.ddlDistributor.Enabled = true;
        #endregion

        if (Convert.ToString(ViewState["Checker"]).Trim() == "TRUE")
        {
            this.btnaddhide.Style["display"] = "none";
        }
        else
        {
            this.btnaddhide.Style["display"] = "";
        }
    }
    #endregion

    #region ResetSession
    public void ResetSession()
    {
        Session.Remove("SALEINVOICEDETAILS");
        Session.Remove("dtInvoiceTaxCount");
        Session.Remove("INVOICETAXCOMPONENTDETAILS");
    }
    #endregion

    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadInvoice();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlWaybillFilter_SelectedIndexChanged
    protected void ddlWaybillFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWaybillFilter.SelectedValue != "0")
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            grdDespatchHeader.DataSource = clsInvc.BindInvoiceWaybillFilter(this.ddlWaybillFilter.SelectedValue, Convert.ToString(HttpContext.Current.Session["FINYEAR"]), ViewState["BSID"].ToString().Trim(), HttpContext.Current.Session["UserID"].ToString());
            grdDespatchHeader.DataBind();
        }
    }
    #endregion

    #region Edit Sale Invoice
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            txtBillingAdress.Attributes.Add("maxlength", "350");
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            decimal AfterRebateAmount = 0;
            decimal GrossRebatePercentage = 0;
            decimal AddFretRebatePercentage = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            /*decimal GrossFinal = 0;*/
            string TAXID = string.Empty;
            decimal TotalMRP = 0;
            string Verify = "";
            DataSet ds = new DataSet();
            DataTable dtQtyschemeEdit = new DataTable();
            DataTable dtInvoiceEdit = new DataTable();
            DataTable dtOfferPackEdit = new DataTable();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            Verify = clsInvc.CheckVerify(saleInvoiceID);
            if (Verify == "Y")
            {
                btnsubmitdiv.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
                this.txtwaybilno.Visible = false;
                this.txtwaybilldate.Visible = false;
                this.imgPopupwaybilldate.Visible = false;
                this.lblwaybildate.Visible = false;
                this.txtwaybilno.Visible = false;
                this.lblwaybillno.Visible = false;
                rdbtcs.Enabled = false;
            }
            else
            {
                btnsubmitdiv.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                divbtnreject.Style["display"] = "";
                this.txtwaybilno.Visible = false;
                this.txtwaybilldate.Visible = false;
                this.imgPopupwaybilldate.Visible = false;
                this.lblwaybildate.Visible = false;
                this.txtwaybilno.Visible = false;
                this.lblwaybillno.Visible = false;
            }

            #region QueryString
            if (Convert.ToString(ViewState["Checker"]) == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
                this.trproductadd.Visible = false;
                this.txtwaybilno.Visible = false;
                this.txtwaybilldate.Visible = false;
                this.imgPopupwaybilldate.Visible = false;
                this.lblwaybildate.Visible = false;
                this.txtwaybilno.Visible = false;
                this.lblwaybillno.Visible = false;
                rdbtcs.Enabled = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
                this.trproductadd.Visible = true;
            }
            #endregion

            #region Disable Controls
            //this.ImageButton1.Enabled = true;
            this.ddlDepot.Enabled = false;
            
            this.ddlDistributor.Enabled = false;
            #endregion

            ds = clsInvc.EditInvoiceDetails(saleInvoiceID);
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();

            #region Header Table Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hdnGroupID.Value = Convert.ToString(ds.Tables[0].Rows[0]["GROUPID"]).Trim();
                this.LoadMotherDepot();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]).Trim();
                this.BindInvoiceCustomer();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICENO"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICEDATE"]).Trim();
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]).Trim();
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]).Trim();
                this.txtTotPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]).Trim();
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]).Trim();
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue);
                this.ddlinsuranceno.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim() == "01/01/1900")
                {
                    this.txtLRGRDate.Text = "";
                }
                else
                {
                    this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim();
                }
                this.txtGetpass.Text = Convert.ToString(ds.Tables[0].Rows[0]["GETPASSNO"]).Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["GETPASSDATE"]).Trim() == "01/01/1900")
                {
                    this.txtGetPassDate.Text = "";
                }
                else
                {
                    this.txtGetPassDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GETPASSDATE"]).Trim();
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["ICDSDATE"]).Trim() == "01/01/1900")
                {
                    this.txtICDSDate.Text = "";
                }
                else
                {
                    this.txtICDSDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ICDSDATE"]).Trim();
                }

                this.txtICDS.Text = Convert.ToString(ds.Tables[0].Rows[0]["ICDSNO"]).Trim();
                this.ddlPaymentMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENTMODE"]).Trim();
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
                this.BindTransporter(this.ddlDepot.SelectedValue);
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]).Trim();
                BindShippingCustomer();
                BindDeliveryAddress(this.ddlDistributor.SelectedValue.ToString());
                this.ddlDeliveryAddress.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYADDRESSID"]).Trim();
                //  this.txtwaybilno.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]).Trim();
                this.ddlWaybill.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]).Trim();
                this.txtwaybilldate.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]).Trim();
                //  this.ddlWaybill.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO_NEW"]).Trim();
                this.rdbtcs.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TCS"]).Trim();
                this.ddlShippingAdress.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYADDRESSID"]).Trim();
                loadGstNumber(this.ddlShippingAdress.SelectedValue);
                this.txtBillingAdress.Text = Convert.ToString(ds.Tables[0].Rows[0]["BillToShiftToAdress"]).Trim();

                if (Convert.ToString(ds.Tables[0].Rows[0]["BILLINGTYPE"]).Trim() == "Y")
                {
                    //if (ds.Tables[11].Rows.Count > 0)
                    //{
                    //    this.ddlSaleOrder.Items.Add(new ListItem(Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERNO"]).Trim(), Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERID"]).Trim()));
                    //    this.ddlSaleOrder.SelectedValue = Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERID"]).Trim();
                    //}
                    //this.lblSaleOrder.Style["display"] = "";
                    //this.tdSaleOrder.Style["display"] = "";
                    this.hdnbilltype.Value = "Y";
                    // ==== Load Product based on Sale Order ===== //
                    DataTable dtEditOrderDetails = new DataTable();
                    dtEditOrderDetails = clsInvc.BindOrderProduct(mode, "");
                    if (dtEditOrderDetails.Rows.Count > 0)
                    {
                        this.ddlProduct.Items.Clear();
                        this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                        this.ddlProduct.AppendDataBoundItems = true;
                        this.ddlProduct.DataSource = dtEditOrderDetails;
                        this.ddlProduct.DataValueField = "PRODUCTID";
                        this.ddlProduct.DataTextField = "PRODUCTNAME";
                        this.ddlProduct.DataBind();
                        this.txtOrderDate.Text = Convert.ToString(dtEditOrderDetails.Rows[0]["SALEORDERDATE"]);
                    }
                    else
                    {
                        this.ddlProduct.Items.Clear();
                        this.ddlProduct.SelectedValue = "0";
                        this.txtOrderDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICEDATE"]).Trim();
                    }
                }

                else
                {
                    
                    this.hdnbilltype.Value = "N";
                    this.LoadOrderProduct();
                }
                // =========================================== //
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["SALEORDERID"] = Convert.ToString(ds.Tables[6].Rows[i]["SALEORDERID"]);
                    dr["PRIMARYPRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRIMARYPRODUCTID"]);
                    dr["PRIMARYPRODUCTBATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["PRIMARYPRODUCTBATCHNO"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["TAG"] = Convert.ToString(ds.Tables[6].Rows[i]["TAG"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                dtInvoiceEdit = (DataTable)Session["SALEINVOICEDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow drEditInvoice = dtInvoiceEdit.NewRow();
                    drEditInvoice["GUID"] = Guid.NewGuid();
                    drEditInvoice["SALEORDERID"] = Convert.ToString(ds.Tables[1].Rows[i]["SALEORDERID"]);
                    drEditInvoice["SALEORDERDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["SALEORDERDATE"]);
                    drEditInvoice["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    drEditInvoice["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    drEditInvoice["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                    drEditInvoice["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    drEditInvoice["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    drEditInvoice["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    drEditInvoice["RATEDISC"] = Convert.ToString(ds.Tables[1].Rows[i]["RATEDISC"]).Trim();
                    drEditInvoice["BCP"] = Convert.ToString(ds.Tables[1].Rows[i]["BCP"]);
                    drEditInvoice["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]);
                    drEditInvoice["QTYPCS"] = Convert.ToString(ds.Tables[1].Rows[i]["QTYPCS"]);
                    drEditInvoice["DISCPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCPER"]);
                    drEditInvoice["DISCAMNT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCAMNT"]);
                    drEditInvoice["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                    drEditInvoice["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    drEditInvoice["TOTALASSESMENTVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                    drEditInvoice["PRIMARYPRICESCHEMEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRIMARYPRICESCHEMEID"]);
                    drEditInvoice["PERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["PERCENTAGE"]);
                    drEditInvoice["VALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["VALUE"]);
                    drEditInvoice["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    drEditInvoice["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    drEditInvoice["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                    drEditInvoice["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                    drEditInvoice["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    drEditInvoice["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                    drEditInvoice["NSR"] = Convert.ToString(ds.Tables[1].Rows[i]["NSR"]).Trim();
                    drEditInvoice["RATEDISCVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATEDISCVALUE"]).Trim();
                    if (GrossRebatePercentage == 0 && AddFretRebatePercentage == 0)
                    {
                        AfterRebateAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString());
                    }
                    else if (GrossRebatePercentage > 0 && AddFretRebatePercentage > 0)
                    {
                        AfterRebateAmount = (Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) - (((Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * GrossRebatePercentage) / 100) + ((Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * AddFretRebatePercentage) / 100)));
                    }
                    decimal Totaltax = 0;
                    decimal Taxpercent = 0;
                    int flag = 0;

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
                        {
                            //Taxpercent = clsInvc.GetTaxPercentage(dtTaxCountDataAddition.Rows[k]["TAXID"].ToString(), this.ddlDistributor.SelectedValue.Trim(), Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]), Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICEDATE"]).Trim());
                            Taxpercent = clsInvc.GetHSNTaxOnEdit(hdnDespatchID.Value, dtTaxCountDataAddition.Rows[k]["TAXID"].ToString(), Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]));
                        }
                        else
                        {
                            Taxpercent = 0;
                        }
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":

                                drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", Taxpercent));
                                double TaxValue = 0;
                                TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(AfterRebateAmount) * Taxpercent) / 100) * 100) / 100);
                                drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                Totaltax += Convert.ToDecimal(String.Format("{0:0.00}", TaxValue));
                                break;
                        }
                        flag = 0;
                        if (Arry.Count > 0)
                        {
                            foreach (string row in Arry)
                            {
                                if (row.Contains(dtTaxCountDataAddition.Rows[k]["NAME"].ToString()))
                                {
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 0;
                                }
                            }
                            if (flag == 0)
                            {
                                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            }
                        }
                        else
                        {
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                        }
                    }
                    #endregion

                    drEditInvoice["NETAMOUNT"] = Convert.ToString(Totaltax + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()));
                    drEditInvoice["CBOXQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["CBOXQTY"]).Trim();
                    dtInvoiceEdit.Rows.Add(drEditInvoice);
                    dtInvoiceEdit.AcceptChanges();
                }

                #region grdAddDespatch DataBind
                HttpContext.Current.Session["SALEINVOICEDETAILS"] = dtInvoiceEdit;
                this.grdAddDespatch.DataSource = dtInvoiceEdit;
                this.grdAddDespatch.DataBind();
                #endregion
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }
            #endregion

            #region Amount-Calculation

            TotalMRP = CalculateTotalMRP(dtInvoiceEdit);
            TotalAmount = CalculateGrossTotal(dtInvoiceEdit);
            TotalTax = CalculateTaxTotal(dtInvoiceEdit);
            GrossTotal = TotalAmount + TotalTax;
            this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
            this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
            this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.txtTotGrossWght.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TOTALGROSSWT"].ToString());
                this.txtTotCase.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ACTUALTOTCASE"].ToString().Trim());
                this.txtRebate.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["GROSSREBATEPERCENTAGE"].ToString().Trim());
                this.txtRebateValue.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["GROSSREBATEVALUE"].ToString().Trim());
                this.txtAddRebate.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGEPERCENTAGE"].ToString().Trim());
                this.txtAddRebateValue.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGEVALUE"].ToString().Trim());
                this.hdnGrossSchemeID.Value = ds.Tables[3].Rows[0]["REBETSCHEMEID"].ToString().Trim();
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString().Trim());
                this.txtTotDisc.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TOTALSPECIALDISCVALUE"].ToString().Trim());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALSALEINVOICEVALUE"].ToString().Trim()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALSALEINVOICEVALUE"].ToString().Trim()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString().Trim())));
                this.txttcspercent.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TCSPERCENT"].ToString());
                this.txttcsamt.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TCSAMOUNT"].ToString());
                this.txtnetwithtcsamt.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TCSNETAMOUNT"].ToString());
                this.txtFreightAmnt.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGE"].ToString());
                this.txtUserInputFreight.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGE"].ToString());
            }
            #endregion

            #region Grid Calculation
            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridAddDespatchStyle();
                this.BillingGridCalculation();
            }
            if (this.grdGST.Rows.Count > 0)
            {
                this.GSTGridCalculation();
            }
            #endregion
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngrdUpdateWaybill_Click
    protected void btngrdUpdateWaybill_Click(object sender, EventArgs e)
    {
        string invoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
        this.txtWaybillUpdate.Text = hdnWaybillNo.Value.ToString();
        this.light.Style["display"] = "block";
        this.fade.Style["display"] = "block";
    }
    #endregion

    #region btnWaybillUpdate_Click
    protected void btnWaybillUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            string invoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            string stateID = Convert.ToString(this.hdnCustomerID.Value).Trim();
            flag = clsInvc.UpdateWaybillNo(invoiceID, this.txtWaybillUpdate.Text.Trim(), stateID);
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.light.Style["display"] = "none";
                this.fade.Style["display"] = "none";
                this.grdDespatchHeader.DataSource = clsInvc.BindInvoice(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(),
                                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                        ViewState["BSID"].ToString().Trim(),
                                                                        HttpContext.Current.Session["DEPOTID"].ToString().Trim(),
                                                                        Convert.ToString(ViewState["Checker"]),
                                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                                        Convert.ToString(Request.QueryString["CHALLAN"]).Trim());
                this.grdDespatchHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnCloseLightbox_Click
    protected void btnCloseLightbox_Click(object sender, EventArgs e)
    {
        this.light.Style["display"] = "none";
        this.fade.Style["display"] = "none";
    }
    #endregion

    #region Resetting Controls After Add
    public void ResetControls()
    {
        this.txtMRP.Text = "0";
        this.txtBaseCost.Text = "0";
        this.txtAssementPercentage.Text = "0";
        this.txtDeliveredQty.Text = "0.00";
        this.txtDeliveredQtyPCS.Text = "0.00";
        this.txtStockQty.Text = "0";
        //this.txtOrderQty.Text = "0";
        //this.txtDespatchQty.Text = "0";
        this.txtRemainingQty.Text = "0";
        this.ddlProduct.SelectedValue = "0";
        //this.ddlPacksize.SelectedValue = "0";
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnDespatchID.Value = "";
        this.hdnGrossSchemeID.Value = "";
        this.txtSaleInvoiceNo.Text = "";
        this.ddlTransporter.SelectedValue = "0";
        this.ddlTransportMode.SelectedValue = "By Road";
        this.txtVehicle.Text = "";
        this.txtRateDiscAmnt.Text = "";

        this.ddlWaybill.Text = "";

        this.txtLRGRDate.Text = "";
        this.txtLRGRNo.Text = "";
        this.txtGetPassDate.Text = "";
        this.txtGetpass.Text = "";
        this.ddlPaymentMode.SelectedValue = "Cash";
        this.txtRemarks.Text = "";
        
        this.txtOrderDate.Text = "";
        this.ddlDistributor.SelectedValue = "0";
        this.ddlProduct.SelectedValue = "0";

        this.ddlBatch.Items.Clear();
        this.ddlBatch.Items.Add(new ListItem("Select Batch", "0"));
        this.ddlBatch.AppendDataBoundItems = true;
        this.txtTotCase.Text = "";
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "0";
        this.txtTotMRP.Text = "0";
        this.txtRebateValue.Text = "0";
        this.txtAddRebateValue.Text = "0";
        this.txtRebate.Text = "0";
        this.txtAddRebate.Text = "0";
        this.txtTotDisc.Text = "0";
        this.txtTotTax.Text = "0";
        this.txtNetAmt.Text = "0";
        this.txtRoundoff.Text = "0";
        this.txtDeliveredQty.Text = "0.00";
        this.txtDeliveredQtyPCS.Text = "0.00";
        this.txtCboxQty.Text = "0.00";
        this.txtFreightAmnt.Text = "0.00";
        this.txtUserInputFreight.Text = "0.00";

        this.txttcspercent.Text = "0";
        this.txttcsamt.Text = "0";
        this.txtnetwithtcsamt.Text = "0";

        this.txtStockQty.Text = "";
        //this.txtOrderQty.Text = "";
        this.txtOrderDate.Text = "";
        this.txtMRP.Text = "";
        this.txtBaseCost.Text = "";
        this.txtBillingAdress.Text = "";
        this.txtAssementPercentage.Text = "";
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grdQtySchemeDetails.DataSource = null;
        this.grdQtySchemeDetails.DataBind();
        this.grdGST.DataSource = null;
        this.grdGST.DataBind();
        //this.txtOrderQty.Text = "";
        this.txtRemainingQty.Text = "";
        //this.txtDespatchQty.Text = "";
        this.txtFinalAmt.Text = "";
        //this.tdFormLabel.Style["display"] = "none";
        //this.tdFormControl.Style["display"] = "none";
        //this.ddlForm.Enabled = true;
        this.ddlForm.SelectedValue = "0";
        this.txtTotPCS.Text = "0";
        this.txtTotGrossWght.Text = "0";
        this.txtTotCase.Text = "0";
        DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        this.txtLRGRDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtGetPassDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.ddlShippingAdress.SelectedValue = "0";
        this.lblShippingAdress.Text = "";
    }
    #endregion

    #region ddlBatch_SelectedIndexChanged
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlBatch.SelectedValue != "0")
        {
            this.BatchDetails();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtDeliveredQty.ClientID + "').focus(); ", true);
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtInner = new DataTable();
        DataTable dtTaxCount = new DataTable();// for Tax Count
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //1
        dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));          //2
        dt.Columns.Add(new DataColumn("SALEORDERDATE", typeof(string)));        //3
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //4
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //5
        dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));              //6       
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));        //7
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));      //8
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //9
        dt.Columns.Add(new DataColumn("NSR", typeof(string)));                  //10
        dt.Columns.Add(new DataColumn("RATEDISC", typeof(string)));             //11
        dt.Columns.Add(new DataColumn("BCP", typeof(string)));                  //12
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));                  //13
        dt.Columns.Add(new DataColumn("QTYPCS", typeof(string)));               //14
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //15
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //16
        dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string)));  //17
        dt.Columns.Add(new DataColumn("PRIMARYPRICESCHEMEID", typeof(string))); //18
        dt.Columns.Add(new DataColumn("RATEDISCVALUE", typeof(string)));        //19
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));           //20
        dt.Columns.Add(new DataColumn("VALUE", typeof(string)));                //21
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //22
        dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));               //23
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));               //24
        dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));          //25
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //26     
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //27
        #region Loop For Adding Itemwise Tax Component
        if (hdnDespatchID.Value == "")
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            string flag = clsInvc.BindRegion(this.ddlDistributor.SelectedValue.Trim(), ddlDepot.SelectedValue.ToString().Trim());

            if (string.IsNullOrEmpty(flag))
            {
                dtTaxCount = clsInvc.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlDepot.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
            }
            else
            {
                dtTaxCount = clsInvc.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlDepot.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
            }
            Session["dtInvoiceTaxCount"] = dtTaxCount;
            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string InvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            ds = clsInvc.EditInvoiceDetails(InvoiceID);
            Session["dtInvoiceTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        #endregion
        dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CBOXQTY", typeof(string)));//33 FOR CGSS AND GST 31 FOR IGST
        dt.Columns.Add(new DataColumn("DISCPER", typeof(string)));//34 FOR CGST AND 32 FOR IGST
        dt.Columns.Add(new DataColumn("DISCAMNT", typeof(string)));//35 FOR CGST AND 33 FOR IGST

        HttpContext.Current.Session["SALEINVOICEDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string SALEORDERID, string PRIMARYPRODUCTID, string PRIMARYPRODUCTBATCH,
                            string PRODUCTID, string BATCH, string TAXID, string TAXPERCENTAGE, string VALUES, string TAG, string MRP)
    {
        DataTable dt = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["SALEORDERID"] = SALEORDERID;
        dr["PRIMARYPRODUCTID"] = PRIMARYPRODUCTID;
        dr["PRIMARYPRODUCTBATCHNO"] = PRIMARYPRODUCTBATCH;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["TAG"] = TAG;
        dr["MRP"] = MRP;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    #region LoadTax
    public void LoadTax(string CFormFlag)
    {
        try
        {
            if (hdnDespatchID.Value == "")
            {
                clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
                DataTable dt = new DataTable();
                string flag = clsInvc.BindRegion(this.ddlDistributor.SelectedValue.Trim(), ddlDepot.SelectedValue.ToString());

                if (string.IsNullOrEmpty(flag))
                {
                    if (CFormFlag == "0")
                    {
                        dt = clsInvc.BindTax(ViewState["menuID"].ToString().Trim(), "1", Session["DEPOTID"].ToString(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                        Session["SaleInvoiceGrossTotalTax"] = dt;
                    }
                    else
                    {
                        dt = clsInvc.BindTax(ViewState["menuID"].ToString().Trim(), "0", Session["DEPOTID"].ToString(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                        Session["SaleInvoiceGrossTotalTax"] = dt;
                    }
                }
                else
                {
                    dt = clsInvc.BindTax(ViewState["menuID"].ToString().Trim(), "0", Session["DEPOTID"].ToString(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                    Session["SaleInvoiceGrossTotalTax"] = dt;
                }
                if (dt.Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["SaleInvoiceGrossTotalTax"];
                    this.grdTax.DataBind();
                }
                else
                {
                    this.grdTax.DataSource = null;
                    this.grdTax.DataBind();
                }
            }
            else
            {
                clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
                DataSet ds = new DataSet();
                string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
                ds = clsInvc.EditInvoiceDetails(despatchID);
                Session["SaleInvoiceGrossTotalTax"] = ds.Tables[4];
                if (ds.Tables[4].Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["SaleInvoiceGrossTotalTax"];
                    this.grdTax.DataBind();
                }
                else
                {
                    this.grdTax.DataSource = null;
                    this.grdTax.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region CalculateGrossTotal
    decimal CalculateGrossTotal(DataTable dt)
    {
        decimal GrossTotal = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);
        }
        return GrossTotal;
    }
    #endregion

    #region CalculateTaxTotal
    decimal CalculateTaxTotal(DataTable dt)
    {
        decimal TotalTax = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            foreach (string name in Arry)
            {
                TotalTax += Convert.ToDecimal(dt.Rows[Counter][name]);
            }
        }
        return TotalTax;
    }
    #endregion

    #region CalculateFinalGrossTotal
    decimal CalculateFinalGrossTotal(DataTable dt)
    {
        decimal GrossTotal = 0;
        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["TAXVALUE"]);
        }
        return GrossTotal;
    }
    #endregion

    #region TotalPCS Calculation
    protected void TotalPcsCalculation()
    {
        clsSaleInvoiceMM clsInvoice = new clsSaleInvoiceMM();
        decimal TotalBillPCS = 0;
        decimal TotalFreePCS = 0;

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            TotalBillPCS += clsInvoice.QtyInCase(row.Cells[4].Text.Trim(), row.Cells[7].Text.Trim(), "B9F29D12-DE94-40F1-A668-C79BF1BF4425", Convert.ToDecimal(row.Cells[13].Text.Trim())) + Convert.ToDecimal(row.Cells[14].Text.Trim());
        }
        foreach (GridViewRow row in grdGST.Rows)
        {
            TotalFreePCS += Convert.ToDecimal(row.Cells[12].Text.Trim());
        }

        if (TotalBillPCS == 0 & TotalFreePCS == 0)
        {
            this.txtTotPCS.Text = "0";
        }
        else
        {
            this.txtTotPCS.Text = (TotalBillPCS + TotalFreePCS).ToString("#.00");

        }
        //if (TotalBillPCS == 0)
        //{
        //    this.txtTotPCS.Text = "0.00";
        //}
        //else
        //{
        //    this.txtTotPCS.Text = (TotalBillPCS).ToString("#.00");
        //}

    }
    #endregion

    #region TotalCase Calculation
    protected void TotalCaseCalculation()
    {

        decimal TotalBillCase = 0;
        decimal TotalBillPCS = 0;
        decimal TotalFreePCS = 0;
        decimal TotalPCS = 0;
        DataTable dtTax = (DataTable)Session["dtInvoiceTaxCount"];

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            if (dtTax.Rows.Count == 1)
            {
                TotalBillCase += Convert.ToDecimal(row.Cells[31].Text.Trim());//13
            }
            else if (dtTax.Rows.Count == 2)
            {
                TotalBillCase += Convert.ToDecimal(row.Cells[33].Text.Trim());//13
            }
            else
            {
                TotalBillCase += Convert.ToDecimal(row.Cells[31].Text.Trim());//13
            }
            TotalBillPCS += Convert.ToDecimal(row.Cells[14].Text.Trim());
        }
        foreach (GridViewRow row in grdGST.Rows)
        {
            TotalFreePCS += Convert.ToDecimal(row.Cells[12].Text.Trim());
        }

        TotalPCS = TotalFreePCS + TotalBillPCS;

        if (TotalBillCase == 0 & TotalPCS == 0)
        {
            this.txtTotCase.Text = "0";
        }
        else
        {
            this.txtTotCase.Text = Convert.ToString(String.Format("{0:0}", TotalBillCase)).Trim();
        }

        //if (TotalBillCase == 0 & TotalBillPCS == 0)
        //{
        //    this.txtTotCase.Text = "0";
        //}
        //else
        //{
        //    this.txtTotCase.Text = Convert.ToString(String.Format("{0:0}", TotalBillCase)).Trim() + '.' + Convert.ToString(String.Format("{0:0}", TotalBillPCS)).Trim();
        //}

    }
    #endregion

    #region CalculateTotalQty
    decimal CalculateTotalQty(DataTable dt, string ProductID)
    {
        decimal TotalQty = 0;
        if (dt.Rows.Count > 0)
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                if (dt.Rows[Counter]["SECONDARYPRODUCTID"].ToString() == ProductID)
                {
                    TotalQty += Convert.ToDecimal(dt.Rows[Counter]["QTY"]);
                }
            }
        }
        return TotalQty;
    }
    #endregion

    #region CalculateTotalFreeQty
    decimal CalculateTotalFreeQty(DataTable dt, string FreeProductID, string ProductID)
    {
        decimal TotalFreeQty = 0;
        if (dt.Rows.Count > 0)
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                if (dt.Rows[Counter]["PRODUCTID"].ToString() == FreeProductID && dt.Rows[Counter]["SCHEME_PRODUCT_ID"].ToString() == ProductID)
                {
                    TotalFreeQty += Convert.ToDecimal(dt.Rows[Counter]["SCHEME_QTY"]);
                }
            }
        }
        return TotalFreeQty;
    }
    #endregion

    #region LoadInvoice
    public void LoadInvoice()
    {
        try
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            this.grdDespatchHeader.DataSource = clsInvc.BindInvoice(txtFromDate.Text.Trim(),
                                                                    txtToDate.Text.Trim(),
                                                                    HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                    //ViewState["BSID"].ToString().Trim(),
                                                                    "",
                                                                    HttpContext.Current.Session["DEPOTID"].ToString().Trim(),
                                                                    Convert.ToString(ViewState["Checker"]), HttpContext.Current.Session["UserID"].ToString(),
                                                                    Convert.ToString(Request.QueryString["CHALLAN"]).Trim());
            this.grdDespatchHeader.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region FreeProductBatchDetails
    protected void FreeProductBatchDetails()
    {
        clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
        DataTable dtBatch = new DataTable();
        string ymd = string.Empty;
        ymd = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());
        String[] batchno = ddlBatch.SelectedValue.Trim().ToString().Split('|');
        string BATCHNO = batchno[0].Trim();
        string Packsize = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";
        dtBatch = clsInvc.BindBatchDetailsFreeProduct(this.ddlDepot.SelectedValue.Trim(), Convert.ToString(this.ddlFreeProduct.SelectedValue.Trim()),
                                           Packsize.Trim(), "0", this.ddlDistributor.SelectedValue.Trim(), ymd,
                                           ViewState["menuID"].ToString().Trim(), ViewState["BSID"].ToString().Trim(), this.hdnGroupID.Value.Trim());
        if (dtBatch.Rows.Count > 0)
        {
            DataRow[] drr = dtBatch.Select("PRODUCTID='" + this.ddlProduct.SelectedValue.Trim() + "' AND BATCHNO='" + BATCHNO + "' ");
            for (int i = 0; i < drr.Length; i++)
                drr[i].Delete();
            dtBatch.AcceptChanges();
        }

        if (dtBatch.Rows.Count > 0)
        {
            this.grdBatchDetails.DataSource = dtBatch;
            this.grdBatchDetails.DataBind();
        }
        else
        {
            this.grdBatchDetails.DataSource = null;
            this.grdBatchDetails.DataBind();
        }
    }
    #endregion

    #region ddlFreeProduct_SelectedIndexChanged
    protected void ddlFreeProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlFreeProduct.SelectedValue != "0")
        {
            this.txtSchemeQty.Text = ddlFreeProduct.SelectedItem.Text.Substring(ddlFreeProduct.SelectedItem.Text.LastIndexOf("(") + 1,
                ddlFreeProduct.SelectedItem.Text.LastIndexOf(")") - ddlFreeProduct.SelectedItem.Text.LastIndexOf("(") - 1);
            this.FreeProductBatchDetails();
            popup.Show();
        }
        else
        {
            popup.Show();
        }
    }
    #endregion

    #region OfferPack Pop-UP Close
    protected void imgclosebtn_click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAddEdit.Style["display"] = "none";
            Session.Remove("BATCHQTYDETAILS");
            this.txtSchemeQty.Text = "";
            this.grdBatchDetails.DataSource = null;
            this.grdBatchDetails.DataBind();
            this.grdBatchDetailsNew.DataSource = null;
            this.grdBatchDetailsNew.DataBind();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnBatchAdd_click
    public void btnBatchAdd_click(object sender, EventArgs e)
    {

    }
    #endregion

    #region btnBatchSubmit_click
    public void btnBatchSubmit_click(object sender, EventArgs e)
    {

    }
    #endregion

    #region btnApprove_Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
            string SaleInvoiceFor = "2";    // --- TRADING

            //if (Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) >= 50000 && this.txtwaybilno.Text.Trim() == "" && this.txtwaybilldate.Text.Trim() == "")
            //{
            //    MessageBox1.ShowInfo("<b><font color='red'>Way Bill No Required!</font></b>");
            //    return;
            //}
            //else if (Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) >= 50000 && this.txtwaybilno.Text.Trim() != "" && this.txtwaybilldate.Text.Trim() == "")
            //{
            //    MessageBox1.ShowInfo("<b><font color='red'>Way Bill Date Required!</font></b>");
            //    return;
            //}
            //else
            //{

                flag = clsPurchaseStockReceipt.ApproveSaleInvoice(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtSaleInvoiceNo.Text.Trim(),
                                                                    this.txtInvoiceDate.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(),
                                                                    SaleInvoiceFor, this.txtwaybilno.Text.Trim(), this.txtwaybilldate.Text.Trim());

                if (flag == 1)
                {
                    this.pnlDisplay.Style["display"] = "";
                    this.pnlAdd.Style["display"] = "none";
                    this.LoadInvoice();
                    MessageBox1.ShowSuccess("Sale Invoice: <b><font color='green'>" + this.txtSaleInvoiceNo.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 700);
                    this.hdnDespatchID.Value = "";
                }
                else if (flag == 0)
                {
                    this.pnlDisplay.Style["display"] = "none";
                    this.pnlAdd.Style["display"] = "";
                    MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
                }
           // }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnReject_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "block";
            this.fadeRejectionNote.Style["display"] = "block";
            this.pnlAdd.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnRejectionNoteSubmit_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnRejectionNoteSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectSaleInvoice(receivedID, this.txtRejectionNote.Text.Trim(), "2");
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                LoadInvoice();
                MessageBox1.ShowSuccess("Sale Invoice: <b><font color='green'>" + this.txtSaleInvoiceNo.Text + "</font></b> rejected successfully.", 60, 500);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "block";
                this.fadeRejectionNote.Style["display"] = "block";

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

    #region btnRejectionCloseLightbox_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnRejectionCloseLightbox_Click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "none";
            this.fadeRejectionNote.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.txtRejectionNote.Text = "";
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region imgrejectionbtn_click
    // Added By Ajoy Rana On 28-06-2016
    protected void imgrejectionbtn_click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "none";
            this.fadeRejectionNote.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.txtRejectionNote.Text = "";
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngrdUpdateForm_Click
    protected void btngrdUpdateForm_Click(object sender, EventArgs e)
    {
        try
        {
            clsSaleInvoiceMM clsSaleInvoiceMM = new clsSaleInvoiceMM();
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            string formFlag = string.Empty;
            formFlag = clsSaleInvoiceMM.CheckFormRequired(despatchID);
            if (formFlag == "1")
            {
                this.txtCFormNo.Text = hdnCFormNo.Value.ToString();
                if (Convert.ToString(hdnCFormDate.Value.Trim()) == "01/01/1900")
                {
                    this.txtCFormPopupDate.Text = "";
                }
                else
                {
                    this.txtCFormPopupDate.Text = hdnCFormDate.Value.ToString();
                }
                this.light2.Style["display"] = "block";
                this.fade.Style["display"] = "block";
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>C Form not required!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnCloseLightbox2_Click
    protected void btnCloseLightbox2_Click(object sender, EventArgs e)
    {
        try
        {
            this.light2.Style["display"] = "none";
            this.fade.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnCFormUpdate_Click
    protected void btnCFormUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            clsSaleInvoiceMM clsSaleInvoiceMM = new clsSaleInvoiceMM();
            int flag = 0;
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsSaleInvoiceMM.UpdateCForm(despatchID, this.txtCFormNo.Text.Trim(), this.txtCFormPopupDate.Text.Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.light2.Style["display"] = "none";
                this.fade.Style["display"] = "none";
                this.LoadInvoice();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region grdDespatchHeader_RowDataBound
    protected void grdDespatchHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[16] as GridDataControlFieldCell;
                GridDataControlFieldCell cell17 = e.Row.Cells[17] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();
                string Stockstatus = cell17.Text.Trim().ToUpper();

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
                    cell.ForeColor = Color.Black;
                }
                else
                {
                    cell.ForeColor = Color.Green;
                }

                if (Stockstatus == "IN TRANSIT")
                {
                    cell17.ForeColor = Color.Blue;
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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string upath = "frmPrintPopUp_FAC.aspx?pid=" + hdnDespatchID.Value + "&&BSID=" + Request.QueryString["BSID"].ToString() + "";
                //string upath = "frmPrintPopUp.aspx?Stnid=" + hdnDespatchID.Value.Trim() + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=" + Request.QueryString["MenuId"].ToString() + " ";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region GridAddDespatchStyle
    public void GridAddDespatchStyle()
    {
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "PRODUCT";
        this.grdAddDespatch.HeaderRow.Cells[6].Text = "HSN CODE";
        this.grdAddDespatch.HeaderRow.Cells[8].Text = "PACKSIZE";
        this.grdAddDespatch.HeaderRow.Cells[10].Text = "RATE";
        this.grdAddDespatch.HeaderRow.Cells[11].Text = "RATE DISC(%)";
        this.grdAddDespatch.HeaderRow.Cells[12].Text = "DISC RATE";
        this.grdAddDespatch.HeaderRow.Cells[13].Text = "CASE";
        this.grdAddDespatch.HeaderRow.Cells[14].Text = "PCS";
        this.grdAddDespatch.HeaderRow.Cells[19].Text = "DISC";
        this.grdAddDespatch.HeaderRow.Cells[20].Text = "DISC%";
        this.grdAddDespatch.HeaderRow.Cells[21].Text = "DISC AMT";
        this.grdAddDespatch.HeaderRow.Cells[22].Text = "AMOUNT";
        this.grdAddDespatch.HeaderRow.Cells[23].Text = "TOT MRP";

    }
    #endregion

    #region BillingGridCalculation
    protected void BillingGridCalculation()
    {
        DataTable dtSALEINVOICEDETAILS = new DataTable();
        DataTable dtOfferPackDetails = new DataTable();
        decimal TotalGridMRP = 0;
        decimal TotalGridVALUE = 0;
        decimal TotalGridAmount = 0;
        decimal TotalNetAmt = 0;
        decimal TotalSpecialDisc = 0;
        decimal TotalOfferpackDisc = 0;
        if (Session["SALEINVOICEDETAILS"] != null)
        {
            dtSALEINVOICEDETAILS = (DataTable)Session["SALEINVOICEDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtSALEINVOICEDETAILS);

        if (Session["OFFERPACKSCHEMEDETAILS"] != null)
        {
            dtOfferPackDetails = (DataTable)Session["OFFERPACKSCHEMEDETAILS"];
            TotalOfferpackDisc = CalculateTotalSpecialDisc(dtOfferPackDetails);
        }

        this.grdAddDespatch.HeaderRow.Cells[1].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[4].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[7].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[8].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[11].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[12].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[16].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[17].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[18].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[19].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[23].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[24].Visible = false;
        //this.grdAddDespatch.HeaderRow.Cells[25].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[26].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[27].Visible = false;

        this.grdAddDespatch.HeaderRow.Cells[1].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[2].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[4].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[5].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[6].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[7].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[8].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[9].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[10].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[11].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[12].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[13].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[14].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[16].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[17].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[18].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[19].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[20].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[21].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[22].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[23].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[24].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[25].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[26].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[27].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[1].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[2].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[3].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[4].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[7].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[8].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[11].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[12].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[15].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[16].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[17].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[18].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[19].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[23].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[24].Visible = false;
        //this.grdAddDespatch.FooterRow.Cells[25].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[26].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[27].Visible = false;

        this.grdAddDespatch.FooterRow.Cells[1].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[2].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[3].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[4].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[5].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[6].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[7].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[8].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[9].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[10].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[11].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[12].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[13].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[14].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[15].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[16].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[17].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[18].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[19].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[20].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[21].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[22].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[23].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[24].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[25].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[26].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[27].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[19].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[20].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[21].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[22].HorizontalAlign = HorizontalAlign.Right;

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[7].Visible = false;
            row.Cells[8].Visible = false;
            row.Cells[11].Visible = false;
            row.Cells[12].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[19].Visible = false;
            row.Cells[23].Visible = false;
            row.Cells[24].Visible = false;
            //row.Cells[25].Visible = false;
            row.Cells[26].Visible = false;
            row.Cells[27].Visible = false;

            row.Cells[1].Wrap = false;
            row.Cells[2].Wrap = false;
            row.Cells[3].Wrap = false;
            row.Cells[4].Wrap = false;
            row.Cells[5].Wrap = false;
            row.Cells[6].Wrap = false;
            row.Cells[7].Wrap = false;
            row.Cells[8].Wrap = false;
            row.Cells[9].Wrap = false;
            row.Cells[10].Wrap = false;
            row.Cells[11].Wrap = false;
            row.Cells[12].Wrap = false;
            row.Cells[13].Wrap = false;
            row.Cells[14].Wrap = false;
            row.Cells[15].Wrap = false;
            row.Cells[16].Wrap = false;
            row.Cells[17].Wrap = false;
            row.Cells[18].Wrap = false;
            row.Cells[19].Wrap = false;
            row.Cells[20].Wrap = false;
            row.Cells[21].Wrap = false;
            row.Cells[22].Wrap = false;
            row.Cells[23].Wrap = false;
            row.Cells[24].Wrap = false;
            row.Cells[25].Wrap = false;
            row.Cells[26].Wrap = false;
            row.Cells[27].Wrap = false;

            row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[15].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[26].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[27].HorizontalAlign = HorizontalAlign.Right;

            TotalSpecialDisc += Convert.ToDecimal(row.Cells[19].Text.Trim());
            TotalGridVALUE += Convert.ToDecimal(row.Cells[21].Text.Trim());
            TotalGridAmount += Convert.ToDecimal(row.Cells[22].Text.Trim());
            TotalGridMRP += Convert.ToDecimal(row.Cells[23].Text.Trim());

            int count = 27;
            DataTable dt = (DataTable)Session["dtInvoiceTaxCount"];
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    count = count + 1;
                    this.grdAddDespatch.HeaderRow.Cells[count].Wrap = true;
                }
            }
        }
        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 29; i <= (29 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    sum += grdAddDespatch.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdAddDespatch.Rows[j].Cells[i].Text) : 0.00;
                    grdAddDespatch.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                this.grdAddDespatch.FooterRow.Cells[i].Text = sum.ToString("#.00");
                this.grdAddDespatch.FooterRow.Cells[i].Font.Bold = true;
                this.grdAddDespatch.FooterRow.Cells[i].ForeColor = Color.Blue;
                this.grdAddDespatch.FooterRow.Cells[i].Wrap = false;
                this.grdAddDespatch.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                count1 = count1 + 1;
            }
        }

        #region Scheme
        if (TotalGridVALUE == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[21].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[21].Text = TotalGridVALUE.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[21].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[21].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[21].Wrap = false;
        #endregion

        #region TotalAmount
        if (TotalGridAmount == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[22].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[22].Text = TotalGridAmount.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[22].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[22].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[22].Wrap = false;
        #endregion

        if (TotalGridMRP == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[23].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[23].Text = TotalGridMRP.ToString("#.00");
        }

        #region Special Discount
        if (TotalSpecialDisc == 0 && TotalOfferpackDisc == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[19].Text = "0.00";
            this.txtTotDisc.Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[19].Text = TotalSpecialDisc.ToString("#.00");
            this.txtTotDisc.Text = Convert.ToString(String.Format("{0:0.00}", TotalSpecialDisc + TotalOfferpackDisc));
        }
        this.grdAddDespatch.FooterRow.Cells[19].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[19].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[19].Wrap = false;
        #endregion

        this.grdAddDespatch.FooterRow.Cells[15].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[15].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[15].ForeColor = Color.Blue;

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 3)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[32 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[32 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[32 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[32 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[32 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[32 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[32 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[32 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[32 + count1].Wrap = false;
        }

        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[30 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[30 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[30 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[30 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[30 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[30 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[30 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[30 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[30 + count1].Wrap = false;
        }

        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[29 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[29 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[29 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[29 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[29 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[29 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[29 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[29 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[29 + count1].Wrap = false;
        }

        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[28 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[28 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[28 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[28 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[28 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[28 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[28 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[28 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[28 + count1].Wrap = false;
        }
        #endregion
    }
    #endregion

    #region SchemeGridCalculation
    protected void SchemeGridCalculation()
    {
        DataTable dtFREEINVOICEDETAILS = new DataTable();
        decimal TotalSpecialDisc = 0;
        decimal TotalNetAmt = 0;
        if (Session["INVOICEQTYSCHEMEDETAILS"] != null)
        {
            dtFREEINVOICEDETAILS = (DataTable)Session["INVOICEQTYSCHEMEDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtFREEINVOICEDETAILS);

        this.grdQtySchemeDetails.HeaderRow.Cells[4].Text = "PRIMARY PRODUCT";
        this.grdQtySchemeDetails.HeaderRow.Cells[8].Text = "SPECIAL PRODUCT";
        this.grdQtySchemeDetails.HeaderRow.Cells[9].Text = "HSN CODE";
        this.grdQtySchemeDetails.HeaderRow.Cells[11].Text = "PACKSIZE";
        this.grdQtySchemeDetails.HeaderRow.Cells[12].Text = "SCHEME QTY(PCS)";
        this.grdQtySchemeDetails.HeaderRow.Cells[14].Text = "RATE";
        this.grdQtySchemeDetails.HeaderRow.Cells[15].Text = "RATE DISC(%)";
        this.grdQtySchemeDetails.HeaderRow.Cells[17].Text = "DISC RATE";
        this.grdQtySchemeDetails.HeaderRow.Cells[18].Text = "SPECIAL DISC";
        this.grdQtySchemeDetails.HeaderRow.Cells[19].Text = "BATCH";



        this.grdQtySchemeDetails.HeaderRow.Cells[1].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[2].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[3].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[4].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[5].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[6].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[7].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[10].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[11].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[15].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[16].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[17].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[18].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[20].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[21].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[22].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[23].Visible = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[24].Visible = false;

        this.grdQtySchemeDetails.HeaderRow.Cells[1].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[2].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[3].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[4].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[5].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[6].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[7].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[8].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[9].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[10].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[11].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[12].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[13].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[14].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[15].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[16].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[17].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[18].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[19].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[20].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[21].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[22].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[23].Wrap = false;
        this.grdQtySchemeDetails.HeaderRow.Cells[24].Wrap = false;

        this.grdQtySchemeDetails.FooterRow.Cells[1].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[2].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[3].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[4].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[5].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[6].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[7].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[10].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[11].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[15].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[16].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[17].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[18].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[20].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[21].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[22].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[23].Visible = false;
        this.grdQtySchemeDetails.FooterRow.Cells[24].Visible = false;

        this.grdQtySchemeDetails.FooterRow.Cells[1].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[2].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[3].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[4].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[5].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[6].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[7].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[8].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[9].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[10].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[11].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[12].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[13].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[14].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[15].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[16].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[17].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[18].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[19].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[20].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[21].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[22].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[23].Wrap = false;
        this.grdQtySchemeDetails.FooterRow.Cells[24].Wrap = false;

        this.grdQtySchemeDetails.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
        this.grdQtySchemeDetails.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;

        foreach (GridViewRow row in grdQtySchemeDetails.Rows)
        {

            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[5].Visible = false;
            row.Cells[6].Visible = false;
            row.Cells[7].Visible = false;
            row.Cells[10].Visible = false;
            row.Cells[11].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[20].Visible = false;
            row.Cells[21].Visible = false;
            row.Cells[22].Visible = false;
            row.Cells[23].Visible = false;
            row.Cells[24].Visible = false;

            row.Cells[1].Wrap = false;
            row.Cells[2].Wrap = false;
            row.Cells[3].Wrap = false;
            row.Cells[4].Wrap = false;
            row.Cells[5].Wrap = false;
            row.Cells[6].Wrap = false;
            row.Cells[7].Wrap = false;
            row.Cells[8].Wrap = false;
            row.Cells[9].Wrap = false;
            row.Cells[10].Wrap = false;
            row.Cells[11].Wrap = false;
            row.Cells[12].Wrap = false;
            row.Cells[13].Wrap = false;
            row.Cells[14].Wrap = false;
            row.Cells[15].Wrap = false;
            row.Cells[16].Wrap = false;
            row.Cells[17].Wrap = false;
            row.Cells[18].Wrap = false;
            row.Cells[19].Wrap = false;
            row.Cells[20].Wrap = false;
            row.Cells[21].Wrap = false;
            row.Cells[22].Wrap = false;
            row.Cells[23].Wrap = false;
            row.Cells[24].Wrap = false;

            row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[19].HorizontalAlign = HorizontalAlign.Center;


            int count = 24;
            DataTable dt = (DataTable)Session["dtInvoiceTaxCount"];
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    count = count + 1;
                    this.grdQtySchemeDetails.HeaderRow.Cells[count].Wrap = true;
                }
            }
        }

        int TotalRows = grdQtySchemeDetails.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 26; i <= (26 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;

                for (int j = 0; j < TotalRows; j++)
                {
                    sum += grdQtySchemeDetails.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdQtySchemeDetails.Rows[j].Cells[i].Text) : 0.00;
                    grdQtySchemeDetails.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                this.grdQtySchemeDetails.FooterRow.Cells[i].Text = sum.ToString("#.00");
                this.grdQtySchemeDetails.FooterRow.Cells[i].Font.Bold = true;
                this.grdQtySchemeDetails.FooterRow.Cells[i].ForeColor = Color.Blue;
                this.grdQtySchemeDetails.FooterRow.Cells[i].Wrap = false;
                this.grdQtySchemeDetails.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                count1 = count1 + 1;

            }
        }


        this.grdQtySchemeDetails.FooterRow.Cells[13].Text = "Total : ";
        this.grdQtySchemeDetails.FooterRow.Cells[13].Font.Bold = true;
        this.grdQtySchemeDetails.FooterRow.Cells[13].ForeColor = Color.Blue;

        foreach (GridViewRow row in grdQtySchemeDetails.Rows)
        {
            TotalSpecialDisc += Convert.ToDecimal(row.Cells[18].Text.Trim());
        }

        if (TotalSpecialDisc == 0)
        {
            this.grdQtySchemeDetails.FooterRow.Cells[18].Text = "0.00";
        }
        else
        {
            this.grdQtySchemeDetails.FooterRow.Cells[18].Text = TotalSpecialDisc.ToString("#.00");
        }

        this.grdQtySchemeDetails.FooterRow.Cells[18].Font.Bold = true;
        this.grdQtySchemeDetails.FooterRow.Cells[18].ForeColor = Color.Blue;
        this.grdQtySchemeDetails.FooterRow.Cells[18].Wrap = false;

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdQtySchemeDetails.FooterRow.Cells[27 + count1].Text = "0.00";
            }
            else
            {
                grdQtySchemeDetails.FooterRow.Cells[27 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdQtySchemeDetails.FooterRow.Cells[27 + count1].Font.Bold = true;
            grdQtySchemeDetails.FooterRow.Cells[27 + count1].ForeColor = Color.Blue;
            grdQtySchemeDetails.FooterRow.Cells[27 + count1].Wrap = false;
            grdQtySchemeDetails.FooterRow.Cells[27 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdQtySchemeDetails.Rows)
            {
                row.Cells[27 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdQtySchemeDetails.HeaderRow.Cells[27 + count1].Text = "NET AMOUNT";
            this.grdQtySchemeDetails.HeaderRow.Cells[27 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdQtySchemeDetails.FooterRow.Cells[26 + count1].Text = "0.00";
            }
            else
            {
                grdQtySchemeDetails.FooterRow.Cells[26 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdQtySchemeDetails.FooterRow.Cells[26 + count1].Font.Bold = true;
            grdQtySchemeDetails.FooterRow.Cells[26 + count1].ForeColor = Color.Blue;
            grdQtySchemeDetails.FooterRow.Cells[26 + count1].Wrap = false;
            grdQtySchemeDetails.FooterRow.Cells[26 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdQtySchemeDetails.Rows)
            {
                row.Cells[26 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdQtySchemeDetails.HeaderRow.Cells[26 + count1].Text = "NET AMOUNT";
            this.grdQtySchemeDetails.HeaderRow.Cells[26 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdGST.FooterRow.Cells[25 + count1].Text = "0.00";
            }
            else
            {
                grdGST.FooterRow.Cells[25 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdGST.FooterRow.Cells[25 + count1].Font.Bold = true;
            grdGST.FooterRow.Cells[25 + count1].ForeColor = Color.Blue;
            grdGST.FooterRow.Cells[25 + count1].Wrap = false;
            grdGST.FooterRow.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdGST.Rows)
            {
                row.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdGST.HeaderRow.Cells[25 + count1].Text = "NET AMOUNT";
            this.grdGST.HeaderRow.Cells[25 + count1].Wrap = false;
        }
        #endregion

        this.grdAddDespatch.HeaderRow.Cells[19].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[20].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[21].Visible = false;
    }
    #endregion

    #region GSTGridCalculation
    protected void GSTGridCalculation()
    {
        DataTable dtFREEINVOICEDETAILS = new DataTable();
        decimal TotalSpecialDisc = 0;
        decimal TotalNetAmt = 0;
        if (Session["OFFERPACKSCHEMEDETAILS"] != null)
        {
            dtFREEINVOICEDETAILS = (DataTable)Session["OFFERPACKSCHEMEDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtFREEINVOICEDETAILS);

        this.grdGST.HeaderRow.Cells[4].Text = "PRIMARY PRODUCT";
        this.grdGST.HeaderRow.Cells[8].Text = "SPECIAL PRODUCT";
        this.grdGST.HeaderRow.Cells[9].Text = "HSN CODE";
        this.grdGST.HeaderRow.Cells[11].Text = "PACKSIZE";
        this.grdGST.HeaderRow.Cells[12].Text = "SCHEME QTY(PCS)";
        this.grdGST.HeaderRow.Cells[14].Text = "RATE";
        this.grdGST.HeaderRow.Cells[15].Text = "RATE DISC(%)";
        this.grdGST.HeaderRow.Cells[17].Text = "DISC RATE";
        this.grdGST.HeaderRow.Cells[18].Text = "SPECIAL DISC";
        this.grdGST.HeaderRow.Cells[19].Text = "BATCH";



        this.grdGST.HeaderRow.Cells[1].Visible = false;
        this.grdGST.HeaderRow.Cells[2].Visible = false;
        this.grdGST.HeaderRow.Cells[3].Visible = false;
        this.grdGST.HeaderRow.Cells[4].Visible = false;
        this.grdGST.HeaderRow.Cells[5].Visible = false;
        this.grdGST.HeaderRow.Cells[6].Visible = false;
        this.grdGST.HeaderRow.Cells[7].Visible = false;
        this.grdGST.HeaderRow.Cells[10].Visible = false;
        this.grdGST.HeaderRow.Cells[11].Visible = false;
        this.grdGST.HeaderRow.Cells[15].Visible = false;
        this.grdGST.HeaderRow.Cells[16].Visible = false;
        this.grdGST.HeaderRow.Cells[17].Visible = false;
        this.grdGST.HeaderRow.Cells[18].Visible = false;
        this.grdGST.HeaderRow.Cells[20].Visible = false;
        this.grdGST.HeaderRow.Cells[21].Visible = false;
        this.grdGST.HeaderRow.Cells[22].Visible = false;
        this.grdGST.HeaderRow.Cells[23].Visible = false;
        this.grdGST.HeaderRow.Cells[24].Visible = false;

        this.grdGST.HeaderRow.Cells[1].Wrap = false;
        this.grdGST.HeaderRow.Cells[2].Wrap = false;
        this.grdGST.HeaderRow.Cells[3].Wrap = false;
        this.grdGST.HeaderRow.Cells[4].Wrap = false;
        this.grdGST.HeaderRow.Cells[5].Wrap = false;
        this.grdGST.HeaderRow.Cells[6].Wrap = false;
        this.grdGST.HeaderRow.Cells[7].Wrap = false;
        this.grdGST.HeaderRow.Cells[8].Wrap = false;
        this.grdGST.HeaderRow.Cells[9].Wrap = false;
        this.grdGST.HeaderRow.Cells[10].Wrap = false;
        this.grdGST.HeaderRow.Cells[11].Wrap = false;
        this.grdGST.HeaderRow.Cells[12].Wrap = false;
        this.grdGST.HeaderRow.Cells[13].Wrap = false;
        this.grdGST.HeaderRow.Cells[14].Wrap = false;
        this.grdGST.HeaderRow.Cells[15].Wrap = false;
        this.grdGST.HeaderRow.Cells[16].Wrap = false;
        this.grdGST.HeaderRow.Cells[17].Wrap = false;
        this.grdGST.HeaderRow.Cells[18].Wrap = false;
        this.grdGST.HeaderRow.Cells[19].Wrap = false;
        this.grdGST.HeaderRow.Cells[20].Wrap = false;
        this.grdGST.HeaderRow.Cells[21].Wrap = false;
        this.grdGST.HeaderRow.Cells[22].Wrap = false;
        this.grdGST.HeaderRow.Cells[23].Wrap = false;
        this.grdGST.HeaderRow.Cells[24].Wrap = false;

        this.grdGST.FooterRow.Cells[1].Visible = false;
        this.grdGST.FooterRow.Cells[2].Visible = false;
        this.grdGST.FooterRow.Cells[3].Visible = false;
        this.grdGST.FooterRow.Cells[4].Visible = false;
        this.grdGST.FooterRow.Cells[5].Visible = false;
        this.grdGST.FooterRow.Cells[6].Visible = false;
        this.grdGST.FooterRow.Cells[7].Visible = false;
        this.grdGST.FooterRow.Cells[10].Visible = false;
        this.grdGST.FooterRow.Cells[11].Visible = false;
        this.grdGST.FooterRow.Cells[15].Visible = false;
        this.grdGST.FooterRow.Cells[16].Visible = false;
        this.grdGST.FooterRow.Cells[17].Visible = false;
        this.grdGST.FooterRow.Cells[18].Visible = false;
        this.grdGST.FooterRow.Cells[20].Visible = false;
        this.grdGST.FooterRow.Cells[21].Visible = false;
        this.grdGST.FooterRow.Cells[22].Visible = false;
        this.grdGST.FooterRow.Cells[23].Visible = false;
        this.grdGST.FooterRow.Cells[24].Visible = false;

        this.grdGST.FooterRow.Cells[1].Wrap = false;
        this.grdGST.FooterRow.Cells[2].Wrap = false;
        this.grdGST.FooterRow.Cells[3].Wrap = false;
        this.grdGST.FooterRow.Cells[4].Wrap = false;
        this.grdGST.FooterRow.Cells[5].Wrap = false;
        this.grdGST.FooterRow.Cells[6].Wrap = false;
        this.grdGST.FooterRow.Cells[7].Wrap = false;
        this.grdGST.FooterRow.Cells[8].Wrap = false;
        this.grdGST.FooterRow.Cells[9].Wrap = false;
        this.grdGST.FooterRow.Cells[10].Wrap = false;
        this.grdGST.FooterRow.Cells[11].Wrap = false;
        this.grdGST.FooterRow.Cells[12].Wrap = false;
        this.grdGST.FooterRow.Cells[13].Wrap = false;
        this.grdGST.FooterRow.Cells[14].Wrap = false;
        this.grdGST.FooterRow.Cells[15].Wrap = false;
        this.grdGST.FooterRow.Cells[16].Wrap = false;
        this.grdGST.FooterRow.Cells[17].Wrap = false;
        this.grdGST.FooterRow.Cells[18].Wrap = false;
        this.grdGST.FooterRow.Cells[19].Wrap = false;
        this.grdGST.FooterRow.Cells[20].Wrap = false;
        this.grdGST.FooterRow.Cells[21].Wrap = false;
        this.grdGST.FooterRow.Cells[22].Wrap = false;
        this.grdGST.FooterRow.Cells[23].Wrap = false;
        this.grdGST.FooterRow.Cells[24].Wrap = false;

        this.grdGST.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
        this.grdGST.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;

        foreach (GridViewRow row in grdGST.Rows)
        {

            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[5].Visible = false;
            row.Cells[6].Visible = false;
            row.Cells[7].Visible = false;
            row.Cells[10].Visible = false;
            row.Cells[11].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[20].Visible = false;
            row.Cells[21].Visible = false;
            row.Cells[22].Visible = false;
            row.Cells[23].Visible = false;
            row.Cells[24].Visible = false;

            row.Cells[1].Wrap = false;
            row.Cells[2].Wrap = false;
            row.Cells[3].Wrap = false;
            row.Cells[4].Wrap = false;
            row.Cells[5].Wrap = false;
            row.Cells[6].Wrap = false;
            row.Cells[7].Wrap = false;
            row.Cells[8].Wrap = false;
            row.Cells[9].Wrap = false;
            row.Cells[10].Wrap = false;
            row.Cells[11].Wrap = false;
            row.Cells[12].Wrap = false;
            row.Cells[13].Wrap = false;
            row.Cells[14].Wrap = false;
            row.Cells[15].Wrap = false;
            row.Cells[16].Wrap = false;
            row.Cells[17].Wrap = false;
            row.Cells[18].Wrap = false;
            row.Cells[19].Wrap = false;
            row.Cells[20].Wrap = false;
            row.Cells[21].Wrap = false;
            row.Cells[22].Wrap = false;
            row.Cells[23].Wrap = false;
            row.Cells[24].Wrap = false;

            row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[19].HorizontalAlign = HorizontalAlign.Center;


            int count = 24;
            DataTable dt = (DataTable)Session["dtInvoiceTaxCount"];
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    count = count + 1;
                    this.grdGST.HeaderRow.Cells[count].Wrap = true;
                }
            }
        }

        int TotalRows = grdGST.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 26; i <= (26 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;

                for (int j = 0; j < TotalRows; j++)
                {
                    sum += grdQtySchemeDetails.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdGST.Rows[j].Cells[i].Text) : 0.00;
                    grdGST.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                this.grdGST.FooterRow.Cells[i].Text = sum.ToString("#.00");
                this.grdGST.FooterRow.Cells[i].Font.Bold = true;
                this.grdGST.FooterRow.Cells[i].ForeColor = Color.Blue;
                this.grdGST.FooterRow.Cells[i].Wrap = false;
                this.grdGST.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                count1 = count1 + 1;

            }
        }


        this.grdGST.FooterRow.Cells[13].Text = "Total : ";
        this.grdGST.FooterRow.Cells[13].Font.Bold = true;
        this.grdGST.FooterRow.Cells[13].ForeColor = Color.Blue;

        foreach (GridViewRow row in grdGST.Rows)
        {
            TotalSpecialDisc += Convert.ToDecimal(row.Cells[18].Text.Trim());
        }

        if (TotalSpecialDisc == 0)
        {
            this.grdGST.FooterRow.Cells[18].Text = "0.00";
        }
        else
        {
            this.grdGST.FooterRow.Cells[18].Text = TotalSpecialDisc.ToString("#.00");
        }

        this.grdGST.FooterRow.Cells[18].Font.Bold = true;
        this.grdGST.FooterRow.Cells[18].ForeColor = Color.Blue;
        this.grdGST.FooterRow.Cells[18].Wrap = false;

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdGST.FooterRow.Cells[27 + count1].Text = "0.00";
            }
            else
            {
                grdGST.FooterRow.Cells[27 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdGST.FooterRow.Cells[27 + count1].Font.Bold = true;
            grdGST.FooterRow.Cells[27 + count1].ForeColor = Color.Blue;
            grdGST.FooterRow.Cells[27 + count1].Wrap = false;
            grdGST.FooterRow.Cells[27 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdGST.Rows)
            {
                row.Cells[27 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdGST.HeaderRow.Cells[27 + count1].Text = "NET AMOUNT";
            this.grdGST.HeaderRow.Cells[27 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdGST.FooterRow.Cells[26 + count1].Text = "0.00";
            }
            else
            {
                grdGST.FooterRow.Cells[26 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdGST.FooterRow.Cells[26 + count1].Font.Bold = true;
            grdGST.FooterRow.Cells[26 + count1].ForeColor = Color.Blue;
            grdGST.FooterRow.Cells[26 + count1].Wrap = false;
            grdGST.FooterRow.Cells[26 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdGST.Rows)
            {
                row.Cells[26 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdGST.HeaderRow.Cells[26 + count1].Text = "NET AMOUNT";
            this.grdGST.HeaderRow.Cells[26 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdGST.FooterRow.Cells[25 + count1].Text = "0.00";
            }
            else
            {
                grdGST.FooterRow.Cells[25 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdGST.FooterRow.Cells[25 + count1].Font.Bold = true;
            grdGST.FooterRow.Cells[25 + count1].ForeColor = Color.Blue;
            grdGST.FooterRow.Cells[25 + count1].Wrap = false;
            grdGST.FooterRow.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdGST.Rows)
            {
                row.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdGST.HeaderRow.Cells[25 + count1].Text = "NET AMOUNT";
            this.grdGST.HeaderRow.Cells[25 + count1].Wrap = false;
        }
        #endregion

    }
    #endregion

    #region CalculateTotalNetAmount
    decimal CalculateTotalNetAmount(DataTable dt)
    {
        decimal NetAmount = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            NetAmount += Convert.ToDecimal(dt.Rows[Counter]["NETAMOUNT"]);
        }
        return NetAmount;
    }
    #endregion

    #region BindInsurenceCompany
    protected void BindInsurenceCompany()
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        DataTable dtInsuranceCompany = HttpContext.Current.Cache["InsuranceCompany"] as DataTable;

        if (dtInsuranceCompany == null)
        {
            dtInsuranceCompany = clsstocktransfer.Bindinscomp();
            if (dtInsuranceCompany.Rows.Count > 0)
            {
                HttpContext.Current.Cache["InsuranceCompany"] = dtInsuranceCompany;
                HttpContext.Current.Cache.Insert("InsuranceCompany", dtInsuranceCompany, null, DateTime.Now.AddDays(90), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
        if (dtInsuranceCompany != null)
        {
            if (dtInsuranceCompany.Rows.Count > 0)
            {
                this.ddlinsurancecompname.Items.Clear();
                this.ddlinsurancecompname.Items.Add(new ListItem("SELECT INSURANCE COMPANY", "0"));
                this.ddlinsurancecompname.AppendDataBoundItems = true;
                this.ddlinsurancecompname.DataSource = dtInsuranceCompany;
                this.ddlinsurancecompname.DataValueField = "ID";
                this.ddlinsurancecompname.DataTextField = "COMPANY_NAME";
                this.ddlinsurancecompname.DataBind();

                if (dtInsuranceCompany.Rows.Count == 1)
                {
                    this.ddlinsurancecompname.SelectedValue = Convert.ToString(dtInsuranceCompany.Rows[0]["ID"]).Trim();
                    BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue);
                }
            }
            else
            {
                this.ddlinsurancecompname.Items.Clear();
                this.ddlinsurancecompname.Items.Add(new ListItem("SELECT INSURANCE COMPANY", "0"));
                this.ddlinsurancecompname.AppendDataBoundItems = true;
            }
        }
    }
    #endregion

    #region BindInsurenceNumber
    protected void BindInsurenceNumber(string CompID)
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        DataTable dtInsuranceNumber = new DataTable();
        dtInsuranceNumber = clsstocktransfer.BindinsNumber(CompID);
        if (dtInsuranceNumber.Rows.Count > 0)
        {
            this.ddlinsuranceno.Items.Clear();
            this.ddlinsuranceno.Items.Add(new ListItem("SELECT POLICY NO", "0"));
            this.ddlinsuranceno.AppendDataBoundItems = true;
            this.ddlinsuranceno.DataSource = dtInsuranceNumber;
            this.ddlinsuranceno.DataValueField = "INSURANCE_NO";
            this.ddlinsuranceno.DataTextField = "INSURANCE_NO";
            this.ddlinsuranceno.DataBind();
            if (dtInsuranceNumber.Rows.Count == 1)
            {
                this.ddlinsuranceno.SelectedValue = Convert.ToString(dtInsuranceNumber.Rows[0]["INSURANCE_NO"]);
            }
        }
    }
    #endregion

    #region ddlinsurancecompname_SelectedIndexChanged
    protected void ddlinsurancecompname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlinsurancecompname.SelectedValue != "0")
        {
            this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlinsuranceno.ClientID + "').focus(); ", true);
        }
    }
    #endregion

    public void Btn_View(string InvoiceID)
    {
        try
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            decimal AfterRebateAmount = 0;
            decimal GrossRebatePercentage = 0;
            decimal AddFretRebatePercentage = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            /*decimal GrossFinal = 0;*/
            string TAXID = string.Empty;
            decimal TotalMRP = 0;
            string Verify = "";
            DataSet ds = new DataSet();
            DataTable dtQtyschemeEdit = new DataTable();
            DataTable dtInvoiceEdit = new DataTable();
            DataTable dtOfferPackEdit = new DataTable();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            hdnDespatchID.Value = InvoiceID;
            string saleInvoiceID = Convert.ToString(hdnDespatchID.Value.Trim());
            Verify = clsInvc.CheckVerify(saleInvoiceID);
            if (Verify == "Y")
            {
                btnsubmitdiv.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
            }
            else
            {
                btnsubmitdiv.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                divbtnreject.Style["display"] = "";
            }

            #region QueryString
            if (Convert.ToString(ViewState["Checker"]) == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
                this.trproductadd.Visible = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
                this.trproductadd.Visible = true;
            }
            #endregion

            #region Disable Controls
            //this.ImageButton1.Enabled = true;
            this.ddlDepot.Enabled = false;
            
            this.ddlDistributor.Enabled = false;
            #endregion

            ds = clsInvc.EditInvoiceDetails(saleInvoiceID);
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();

            #region Header Table Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hdnGroupID.Value = Convert.ToString(ds.Tables[0].Rows[0]["GROUPID"]).Trim();
                this.LoadMotherDepot();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]).Trim();
                this.BindInvoiceCustomer();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICENO"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICEDATE"]).Trim();
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]).Trim();
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]).Trim();
                this.txtTotPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]).Trim();
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]).Trim();
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue);
                this.ddlinsuranceno.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim() == "01/01/1900")
                {
                    this.txtLRGRDate.Text = "";
                }
                else
                {
                    this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim();
                }
                this.txtGetpass.Text = Convert.ToString(ds.Tables[0].Rows[0]["GETPASSNO"]).Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["GETPASSDATE"]).Trim() == "01/01/1900")
                {
                    this.txtGetPassDate.Text = "";
                }
                else
                {
                    this.txtGetPassDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GETPASSDATE"]).Trim();
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["ICDSDATE"]).Trim() == "01/01/1900")
                {
                    this.txtICDSDate.Text = "";
                }
                else
                {
                    this.txtICDSDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ICDSDATE"]).Trim();
                }

                this.txtICDS.Text = Convert.ToString(ds.Tables[0].Rows[0]["ICDSNO"]).Trim();
                this.ddlPaymentMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENTMODE"]).Trim();
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
                this.BindTransporter(this.ddlDepot.SelectedValue);
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]).Trim();
                this.ddlDeliveryAddress.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYADDRESSID"]).Trim();
                this.txtwaybilno.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO_NEW"]).Trim();
                this.txtwaybilldate.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]).Trim();

                //if (ds.Tables[11].Rows.Count > 0)
                //{
                //    this.ddlSaleOrder.Items.Add(new ListItem(Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERNO"]).Trim(), Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERID"]).Trim()));
                //    this.ddlSaleOrder.SelectedValue = Convert.ToString(ds.Tables[11].Rows[0]["SALEORDERID"]).Trim();
                //}

                // ==== Load Product based on Sale Order ===== //
                DataTable dtEditOrderDetails = new DataTable();
                dtEditOrderDetails = clsInvc.BindOrderProduct(mode, "");
                if (dtEditOrderDetails.Rows.Count > 0)
                {
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                    this.ddlProduct.AppendDataBoundItems = true;
                    this.ddlProduct.DataSource = dtEditOrderDetails;
                    this.ddlProduct.DataValueField = "PRODUCTID";
                    this.ddlProduct.DataTextField = "PRODUCTNAME";
                    this.ddlProduct.DataBind();
                    this.txtOrderDate.Text = Convert.ToString(dtEditOrderDetails.Rows[0]["SALEORDERDATE"]);
                }
                else
                {
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.SelectedValue = "0";
                    this.txtOrderDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALEINVOICEDATE"]).Trim();
                }
                // =========================================== //
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["SALEORDERID"] = Convert.ToString(ds.Tables[6].Rows[i]["SALEORDERID"]);
                    dr["PRIMARYPRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRIMARYPRODUCTID"]);
                    dr["PRIMARYPRODUCTBATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["PRIMARYPRODUCTBATCHNO"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["TAG"] = Convert.ToString(ds.Tables[6].Rows[i]["TAG"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                dtInvoiceEdit = (DataTable)Session["SALEINVOICEDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow drEditInvoice = dtInvoiceEdit.NewRow();
                    drEditInvoice["GUID"] = Guid.NewGuid();
                    drEditInvoice["SALEORDERID"] = Convert.ToString(ds.Tables[1].Rows[i]["SALEORDERID"]);
                    drEditInvoice["SALEORDERDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["SALEORDERDATE"]);
                    drEditInvoice["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    drEditInvoice["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    drEditInvoice["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                    drEditInvoice["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    drEditInvoice["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    drEditInvoice["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    drEditInvoice["RATEDISC"] = Convert.ToString(ds.Tables[1].Rows[i]["RATEDISC"]).Trim();
                    drEditInvoice["BCP"] = Convert.ToString(ds.Tables[1].Rows[i]["BCP"]);
                    drEditInvoice["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]);
                    drEditInvoice["QTYPCS"] = Convert.ToString(ds.Tables[1].Rows[i]["QTYPCS"]);
                    drEditInvoice["DISCPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCPER"]);
                    drEditInvoice["DISCAMNT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCAMNT"]);
                    drEditInvoice["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                    drEditInvoice["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    drEditInvoice["TOTALASSESMENTVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                    drEditInvoice["PRIMARYPRICESCHEMEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRIMARYPRICESCHEMEID"]);
                    drEditInvoice["PERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["PERCENTAGE"]);
                    drEditInvoice["VALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["VALUE"]);
                    drEditInvoice["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    drEditInvoice["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    drEditInvoice["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                    drEditInvoice["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    drEditInvoice["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                    drEditInvoice["NSR"] = Convert.ToString(ds.Tables[1].Rows[i]["NSR"]).Trim();
                    drEditInvoice["RATEDISCVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATEDISCVALUE"]).Trim();
                    if (GrossRebatePercentage == 0 && AddFretRebatePercentage == 0)
                    {
                        AfterRebateAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString());
                    }
                    else if (GrossRebatePercentage > 0 && AddFretRebatePercentage > 0)
                    {
                        AfterRebateAmount = (Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) - (((Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * GrossRebatePercentage) / 100) + ((Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * AddFretRebatePercentage) / 100)));
                    }
                    decimal Totaltax = 0;
                    decimal Taxpercent = 0;
                    int flag = 0;

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        if (Convert.ToString(Request.QueryString["CHALLAN"]).Trim() == "FALSE")
                        {
                            Taxpercent = clsInvc.GetHSNTaxOnEdit(saleInvoiceID, dtTaxCountDataAddition.Rows[k]["TAXID"].ToString(), Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]));
                        }
                        else
                        {
                            Taxpercent = 0;
                        }
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":

                                drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", Taxpercent));
                                double TaxValue = 0;
                                TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(AfterRebateAmount) * Taxpercent) / 100) * 100) / 100);
                                drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                Totaltax += Convert.ToDecimal(String.Format("{0:0.00}", TaxValue));
                                break;
                        }
                        flag = 0;
                        if (Arry.Count > 0)
                        {
                            foreach (string row in Arry)
                            {
                                if (row.Contains(dtTaxCountDataAddition.Rows[k]["NAME"].ToString()))
                                {
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 0;
                                }
                            }
                            if (flag == 0)
                            {
                                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            }
                        }
                        else
                        {
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                        }
                    }
                    #endregion

                    drEditInvoice["NETAMOUNT"] = Convert.ToString(Totaltax + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()));
                    dtInvoiceEdit.Rows.Add(drEditInvoice);
                    dtInvoiceEdit.AcceptChanges();
                }

                #region grdAddDespatch DataBind
                HttpContext.Current.Session["SALEINVOICEDETAILS"] = dtInvoiceEdit;
                this.grdAddDespatch.DataSource = dtInvoiceEdit;
                this.grdAddDespatch.DataBind();
                #endregion
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }
            #endregion

            #region Amount-Calculation

            TotalMRP = CalculateTotalMRP(dtInvoiceEdit);
            TotalAmount = CalculateGrossTotal(dtInvoiceEdit);
            TotalTax = CalculateTaxTotal(dtInvoiceEdit);
            GrossTotal = TotalAmount + TotalTax;
            this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
            this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
            this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.txtTotGrossWght.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TOTALGROSSWT"].ToString());
                this.txtTotCase.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ACTUALTOTCASE"].ToString().Trim());
                this.txtRebate.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["GROSSREBATEPERCENTAGE"].ToString().Trim());
                this.txtRebateValue.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["GROSSREBATEVALUE"].ToString().Trim());
                this.txtAddRebate.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGEPERCENTAGE"].ToString().Trim());
                this.txtAddRebateValue.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADDFRETCHARGEVALUE"].ToString().Trim());
                this.hdnGrossSchemeID.Value = ds.Tables[3].Rows[0]["REBETSCHEMEID"].ToString().Trim();
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString().Trim());
                this.txtTotDisc.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["TOTALSPECIALDISCVALUE"].ToString().Trim());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALSALEINVOICEVALUE"].ToString().Trim()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALSALEINVOICEVALUE"].ToString().Trim()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString().Trim())));
            }
            #endregion

            #region Grid Calculation
            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridAddDespatchStyle();
                this.BillingGridCalculation();
            }
            if (this.grdGST.Rows.Count > 0)
            {
                this.GSTGridCalculation();
            }
            #endregion
        }
        catch (Exception ex)
        {
            /*string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);*/
        }
    }

    protected void btnTransporter_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('transporterFromSaleOrder.aspx?MENU=" + HttpUtility.UrlEncode("TransporterMaster-Inv")

             + "','new window', 'top=200, left=600, width=400, height=150, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnRefreshCategory_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindTransporter(this.ddlDepot.SelectedValue);
        }
        catch (Exception ex)
        {
            string msg = "alert('" + ex.Message.Replace("'", "") + "')";
            MessageBox1.ShowError(msg);
        }
    }

    protected void rblInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            this.ddlDistributor.SelectedValue = "0";
            this.ddlProduct.SelectedValue = "0";
        
    }

    protected void rdbtcs_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (rdbtcs.SelectedValue == "Y")
        {
            if (this.ddlDistributor.SelectedValue != "" && (txtFinalAmt.Text != "" && txtFinalAmt.Text != "0.00"))
            {
                decimal TcsAmt = 0;
                clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
                DataTable dttcspercent = clsInvc.BindTcsPercent(ddlDistributor.SelectedValue.ToString());

                txttcspercent.Text = dttcspercent.Rows[0]["TCSPERCENT"].ToString();
                if (Convert.ToDecimal(txttcspercent.Text) > 0)
                {
                    TcsAmt = Math.Ceiling((Convert.ToDecimal(txtFinalAmt.Text) * Convert.ToDecimal(txttcspercent.Text)) / 100);
                    txttcsamt.Text = Convert.ToString(TcsAmt);
                    txtnetwithtcsamt.Text = Convert.ToString((Convert.ToDecimal(txtFinalAmt.Text) + TcsAmt));
                }

                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Tcs Percentage Zero.</font></b>");
                    return;
                }

            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Net Amt Can not be Blank.</font></b>");
                rdbtcs.SelectedValue = "N";
                txttcspercent.Text = "0";
            }
        }
        else
        {
            rdbtcs.SelectedValue = "N";
            txttcspercent.Text = "0";
            txttcsamt.Text = "0";
            txtnetwithtcsamt.Text = "0";
        }


    }

    protected void txtUserInputFreight_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtUserInputFreight.Text) > 0)
        {
            if (rdbtcs.SelectedValue == "Y")
            {
                MessageBox1.ShowWarning("make tcs no then use freight");
                txtUserInputFreight.Text = "0";
                return;
            }
            this.txtFreightAmnt.Text = this.txtUserInputFreight.Text;
            decimal amnt = Convert.ToDecimal(txtNetAmt.Text) + Convert.ToDecimal(this.txtUserInputFreight.Text);
            txtTotalGross.Text = Convert.ToString(amnt);
            txtFinalAmt.Text = Convert.ToString(Math.Round(amnt));
            this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtFinalAmt.Text) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
        }
        else
        {
            this.txtFreightAmnt.Text = "0";
            txtTotalGross.Text = txtNetAmt.Text;
            txtFinalAmt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtTotalGross.Text)));
            this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
        }

    }

    protected void ddlShippingAdress_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGstNumber(this.ddlShippingAdress.SelectedValue);
    }

    public void loadGstNumber(string ShippingId)
    {
        try
        {
            string mode1 = "SHIPPINGGST";
            ClsVendor_TPU clsGetEntry = new ClsVendor_TPU();
            DataTable dt = new DataTable();
            dt = clsGetEntry.BindPoFromTpu(mode1, ShippingId);
            if (dt.Rows.Count > 0)
            {
                this.lblShippingAdress.Text = Convert.ToString(dt.Rows[0]["GSTNUMBER"]);
            }
            else
            {
                this.lblShippingAdress.Text = "";
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    #region BindShippingCustomer
    protected void BindShippingCustomer()
    {
        DataTable dtCustomer = new DataTable();
        DataTable dtDepot = new DataTable();
        clsSaleInvoiceMM clsSaleInvoiceMM = new clsSaleInvoiceMM();
        dtCustomer = clsSaleInvoiceMM.BindCustomer(""/*ViewState["BSID"].ToString().Trim()*/, "", this.ddlDepot.SelectedValue.Trim());

        if (this.hdnDespatchID.Value == "")
        {
            if (dtCustomer.Rows.Count > 0)
            {
                this.ddlShippingAdress.Items.Clear();
                this.ddlShippingAdress.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlShippingAdress.AppendDataBoundItems = true;
                this.ddlShippingAdress.DataSource = dtCustomer;
                this.ddlShippingAdress.DataValueField = "CUSTOMERID";
                this.ddlShippingAdress.DataTextField = "CUSTOMERNAME";
                this.ddlShippingAdress.DataBind();
            }
            else
            {
                this.ddlShippingAdress.Items.Clear();
                this.ddlShippingAdress.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlShippingAdress.AppendDataBoundItems = true;
            }
        }
        else if (this.hdnDespatchID.Value != "")
        {
            clsSaleInvoiceMM clsInvc = new clsSaleInvoiceMM();
            string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            DataTable ds = new DataTable();
            ds = clsInvc.BindCustomer(""/*ViewState["BSID"].ToString().Trim()*/, "", this.ddlDepot.SelectedValue.Trim());
            if (ds.Rows.Count > 0)
            {
                this.ddlShippingAdress.Items.Clear();
                this.ddlShippingAdress.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlShippingAdress.AppendDataBoundItems = true;
                this.ddlShippingAdress.DataSource = dtCustomer;
                this.ddlShippingAdress.DataValueField = "CUSTOMERID";
                this.ddlShippingAdress.DataTextField = "CUSTOMERNAME";
                this.ddlShippingAdress.DataBind();
            }
        }

    }
    #endregion

    protected void txtDeliveredQtyPCS_TextChanged(object sender, EventArgs e)
    {

        if (this.ddlPacksize.SelectedItem.Text.Trim() == "PCS")
        {
            decimal d = Convert.ToDecimal(txtDeliveredQtyPCS.Text);
            if ((d % 1) > 0)
            {
                MessageBox1.ShowWarning("Decimal value cannot be accept for this product");
                txtDeliveredQtyPCS.Text = "0";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtCboxQty.ClientID + "').focus(); ", true);
                return;
            }
            else
            {
                txtDeliveredQtyPCS.Text = txtDeliveredQtyPCS.Text;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtCboxQty.ClientID + "').focus(); ", true);
            }
        }

    }

    protected void txtRoundoff_TextChanged(object sender, EventArgs e)
    {
        decimal grosAmnt = 0;
        decimal roundOf = 0;
        decimal Amnt = 0;
        grosAmnt = Convert.ToDecimal(this.txtTotalGross.Text);
        roundOf = Convert.ToDecimal(this.txtRoundoff.Text);


        if (roundOf > 0)
        {
            Amnt = grosAmnt + roundOf;
        }
        else if (roundOf < 0)
        {
            Amnt = grosAmnt + roundOf;
        }
        else
        {
            Amnt = grosAmnt;
        }

        this.txtFinalAmt.Text = Convert.ToString(Amnt);
        decimal tcsPer = Convert.ToDecimal(txttcspercent.Text);
        if (tcsPer > 0)
        {
            
            decimal TcsAmnt = (tcsPer / Amnt) * 100;
            this.txttcsamt.Text = Convert.ToString(TcsAmnt);
            this.txtnetwithtcsamt.Text = Convert.ToString(Amnt + TcsAmnt);
        }
    }
}