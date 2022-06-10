using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class VIEW_frmCustomerMaster : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlsearchbs').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddldepots').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlaccountsgroup').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcustype').multiselect({ includeSelectAllOption: true  }); });</script>", false);
            if (!IsPostBack)
            {
                ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
                pnlAdd.Style["display"] = "none";
                pnlDepotMapping.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                pnltsimapping.Style["display"] = "none";
                pnlPlotMapping.Style["display"] = "none";
                this.PnlAddress.Style["display"] = "none";
                this.PnlTransporter.Style["display"] = "none";
                /*Added By Arti Agarwal on 09.10.2018*/
                trshop.Style["display"] = "none";
                trRetDiv.Style["display"] = "none";
                trcategory.Style["display"] = "none";
                trMarginPer.Style["display"] = "none";
                this.trSO.Visible = false;
                this.trBrandMap.Visible = false;
                this.trinvoice.Visible = false;
                this.txtInactiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                /**********************************/
                this.LoadState();
                this.LoadBrandType();
                /*Added By Sayan Dey On 01/01/2018*/
                this.LoadRetDiv();
                this.LoadFilterCustomerType();
                LoadFilterAccGroup();
                if (Request.QueryString["MENU"].ToString().Trim() == "Y")
                {
                    gvCustomer.Columns[17].Visible = true;
                    gvCustomer.Columns[18].Visible = true;
                    gvCustomer.Columns[19].Visible = true;
                    gvCustomer.Columns[20].Visible = true;
                    gvCustomer.Columns[21].Visible = true;
                    gvCustomer.Columns[22].Visible = true;
                    gvCustomer.Columns[23].Visible = true;
                    gvCustomer.Columns[24].Visible = true;

                    btnaddhide.Style["display"] = "";
                    divbtnCustomerSubmit.Style["display"] = "";
                }
                else
                {
                    btnaddhide.Style["display"] = "NONE";
                    divbtnCustomerSubmit.Style["display"] = "none";
                }
                DataTable dtlevel = new DataTable();
                if (Request.QueryString["MENU"] == "Y")
                {
                    dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                    if (dtlevel.Rows.Count > 0)
                    {
                        LoadBusinessSegment(Session["USERID"].ToString().Trim());
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim());
                    }
                    else
                    {
                        LoadBusinessSegment();
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim(), Session["TPU"].ToString().Trim());
                    }
                    tdExcel.Style["display"] = "none";

                }
                else
                {
                    dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                    if (dtlevel.Rows.Count > 0)
                    {
                        LoadBusinessSegment(Session["USERID"].ToString().Trim());
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim());
                    }
                    else
                    {
                        LoadBusinessSegment();
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim(), Session["TPU"].ToString().Trim());
                    }
                    tdExcel.Style["display"] = "";
                    btnaddhide.Style["display"] = "none";
                }

                pnlProductMapping.Style["display"] = "none";
                txtDistrict.Text = "";
                txtCity.Text = "";           
                ddlGroupType.Items.Insert(0, new ListItem("Select Group Type", "0"));
                BindProduct();
                LoadBrand();
                LoadShopName();
                LoadAccGroup();
                LoadLedger();
                HttpContext.Current.Session["PRODUCTMAPPING"] = null;
                LoadSearchBSGroup();
                this.LoadCountry();
                this.LoadCompnyType();
                this.LoadPurchaseType();
                this.LoadDepo();

                if (Request.QueryString["Mode"] != null)
                {
                    string qst = Request.QueryString["Custid"].ToString().Trim();
                    string qst1 = Request.QueryString["HiddenEnabled"].ToString().Trim();
                    if (Request.QueryString["Mode"].ToString().Trim() == "D" & Request.QueryString["HiddenEnabled"].ToString().Trim() == "E")
                    {
                        EditCustomerbyCreditAmount(qst, qst1);
                    }
                }
            }
            else
            {
                if (this.txtPanNO.Text != "" && Convert.ToInt32(ddlState.SelectedValue.Trim()) > 0)
                {
                    this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue.Trim()));
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

    private void AllControlEnabDisb(string tag)
    {
        if (this.Hdn_controltagE.Value == "E")
        {
            this.txtName.Enabled = true;
            this.txtcode.Enabled = true;
            this.txtshortname.Enabled = true;
            this.ddlBusniessSegment.Attributes.Remove("disabled");
            this.ddlGroupType.Attributes.Remove("disabled");
            this.LoadBusinessSegment();
            this.ddlCustomer1.Enabled = true;
            this.txtContactPerson1.Enabled = true;
            this.txtContactPerson2.Enabled = true;
            this.txtMobileNo.Enabled = true;
            this.txtPhoneNo.Enabled = true;
            this.txtMobileNo1.Enabled = true;
            this.txtPhoneNo1.Enabled = true;
            this.txtEmailid1.Enabled = true;
            this.txtEmailid2.Enabled = true;
            this.ddlState.Enabled = true;
            this.txtDistrict.Enabled = true;
            this.txtCity.Enabled = true;
            tdaddCR.Style["display"] = "";
            this.ddlcountry.Enabled = true;
            this.txtAddress.Enabled = true;
            this.txtPIN.Enabled = true;
            this.txtDeliveryaddress.Enabled = true;
            this.ddlAccGroup.Enabled = true;
            this.ddlReportToRole.Enabled = true;
            this.ddlUnder.Enabled = true;
            this.txtpercentage.Enabled = true;
            this.txtamount.Enabled = true;
            this.ddlcurrency.Enabled = true;
            this.txtcstno.Enabled = true;
            this.txtvatno.Enabled = true;
            this.txttinno.Enabled = true;
            this.txtPanNO.Enabled = true;
            this.ddlShop.Enabled = true;
            this.ddlretailcat.Enabled = true;
            this.chkActive.Enabled = true;


        }
        else
        {
            this.txtName.Enabled = false;
            this.txtcode.Enabled = false;
            this.txtshortname.Enabled = false;
            ddlBusniessSegment.Attributes.Add("disabled", "disabled");
            ddlGroupType.Attributes.Add("disabled", "disabled");
            this.ddlCustomer1.Enabled = false;
            this.txtContactPerson1.Enabled = false;
            this.txtContactPerson2.Enabled = false;
            this.txtMobileNo.Enabled = false;
            this.txtPhoneNo.Enabled = false;
            this.txtMobileNo1.Enabled = false;
            this.txtPhoneNo1.Enabled = false;
            this.txtEmailid1.Enabled = false;
            this.txtEmailid2.Enabled = false;
            this.ddlState.Enabled = false;
            this.txtDistrict.Enabled = false;
            this.txtCity.Enabled = false;
            tdaddCR.Style["display"] = "none";
            this.ddlcountry.Enabled = false;
            this.txtAddress.Enabled = false;
            this.txtPIN.Enabled = false;
            this.txtDeliveryaddress.Enabled = false;
            this.ddlAccGroup.Enabled = false;
            this.ddlReportToRole.Enabled = false;
            this.ddlUnder.Enabled = false;
            this.txtpercentage.Enabled = false;
            this.txtamount.Enabled = false;
            this.ddlcurrency.Enabled = false;
            this.txtcstno.Enabled = false;
            this.txtvatno.Enabled = false;
            this.txttinno.Enabled = false;
            this.txtPanNO.Enabled = false;
            this.ddlShop.Enabled = false;
            this.ddlretailcat.Enabled = false;
            this.chkActive.Enabled = false;
            divbtnCustomerSubmit.Style["display"] = "none";

        }
    }

    #region LoadShopName
    public void LoadShopName()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlShop.Items.Clear();
            ddlShop.Items.Insert(0, new ListItem("Select Shop Type", "0"));
            ddlShop.DataSource = ClsCustomer.BindShop();
            ddlShop.DataTextField = "SHOPNAME";
            ddlShop.DataValueField = "SHOPID";
            ddlShop.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Compny
    public void LoadCompnyType()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlcompanytype.Items.Clear();
            ddlcompanytype.DataSource = ClsCustomer.BindCompanyType();
            ddlcompanytype.DataTextField = "COMPANYTYPENAME";
            ddlcompanytype.DataValueField = "COMPANYTYPEID";
            ddlcompanytype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load PurchaseType
    public void LoadPurchaseType()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            //DataTable dt = ClsCustomer.BindPurchaseType();
            //if (dt.Rows.Count>0)
            this.lstpurchasetype.Items.Clear();
            lstpurchasetype.Items.Insert(0, new ListItem("Select Purchase Type", "0"));
            this.lstpurchasetype.DataSource = ClsCustomer.BindPurchaseType();
            this.lstpurchasetype.DataTextField = "ITEM_NAME";
            this.lstpurchasetype.DataValueField = "ID";
            this.lstpurchasetype.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    public void LoadDepo()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.BinddepotMasterGrid(Session["USERID"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.ddldepots.Items.Clear();
                this.ddldepots.DataSource = dt;
                this.ddldepots.DataTextField = "BRNAME";
                this.ddldepots.DataValueField = "BRID";
                this.ddldepots.DataBind();
            }
            else
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region LoadMotherDepot
    /* In Customer Add and Edit*/
    public void LoadMotherDepot()
    {
        try
        {
            ClsCustomerMaster clsrpt = new ClsCustomerMaster();
            DataTable dtDepot = new DataTable();
            dtDepot = clsrpt.BinddepotMasterGrid(Session["USERID"].ToString());

            if (dtDepot.Rows.Count > 0)
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                this.ddlDepot.AppendDataBoundItems = true;
                this.ddlDepot.DataSource = dtDepot;
                this.ddlDepot.DataValueField = "BRID";
                this.ddlDepot.DataTextField = "BRNAME";
                this.ddlDepot.DataBind();
                /*if (dtDepot.Rows.Count == 1)
                {
                    this.ddlDepot.SelectedValue = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                }*/
            }
            else
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                this.ddlDepot.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region BindSo Against Depot
    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster clsreport = new ClsCustomerMaster();
            DataTable dt = new DataTable();
            dt = clsreport.BindSOBYDEPOT(ddlDepot.SelectedValue);
            this.ddlSO.Items.Clear();
            this.ddlSO.Items.Add(new ListItem("Select", "0"));
            this.ddlSO.AppendDataBoundItems = true;
            this.ddlSO.DataSource = dt;
            this.ddlSO.DataTextField = "USERNAME";
            this.ddlSO.DataValueField = "USERID";
            this.ddlSO.DataBind();
            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    public void LoadSearchBSGroup()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlsearchbs.Items.Clear();
            ddlsearchbs.DataSource = ClsCustomer.BindCustomerType_V1(Session["USERID"].ToString());
            ddlsearchbs.DataTextField = "BSNAME";
            ddlsearchbs.DataValueField = "BSID";
            ddlsearchbs.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadAccGroup()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlAccGroup.Items.Clear();
            ddlAccGroup.Items.Insert(0, new ListItem("Select Group", "0"));
            //ddlAccGroup.DataSource = ClsCustomer.BindAccGroup();
            ddlAccGroup.DataSource = ClsCustomer.BindAccGroupKKG();
            ddlAccGroup.DataTextField = "grpName";
            ddlAccGroup.DataValueField = "Code";
            ddlAccGroup.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    //public void LoadAccGroup1()
    //{
    //    try
    //    {
    //        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
    //        ddlAccGroup.Items.Clear();
    //        ddlAccGroup.Items.Insert(0, new ListItem("Select Group", "0"));
    //        ddlAccGroup.DataSource = ClsCustomer.BindAccGroup();
    //       // ddlAccGroup.DataSource = ClsCustomer.BindAccGroupKKG();
    //        ddlAccGroup.DataTextField = "grpName";
    //        ddlAccGroup.DataValueField = "Code";
    //        ddlAccGroup.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //}


    public void LoadLedger()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.BindLedger();

            if (dt.Rows.Count > 0)
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
                this.ddlledger.DataSource = ClsCustomer.BindLedger();
                this.ddlledger.DataTextField = "NAME";
                this.ddlledger.DataValueField = "ID";
                this.ddlledger.DataBind();
            }
            else
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    protected void rdbledger_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbledger.SelectedValue == "0")
        {
            trgroup.Style["display"] = "";
            trledger.Style["display"] = "none";
            this.ddlledger.SelectedValue = "0";
        }
        else
        {

            trgroup.Style["display"] = "none";
            trledger.Style["display"] = "";
            this.ddlAccGroup.SelectedValue = "0";

        }
        /*----inactive date---*/
        if (chkActive.Checked == true)
        {
            trinactivedate.Style["display"] = "none";
        }
        else
        {
            trinactivedate.Style["display"] = "";
        }
    }

    //===========ADD BY SUBHODIP DE ON23.05.2016=====================//

    #region LoadRetailercategory
    public void LoadRetailercategory(string tag)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.Bindreatilercategory(tag);
            if (dt.Rows.Count > 0)
            {
                ddlretailcat.Items.Clear();
                ddlretailcat.Items.Insert(0, new ListItem("Select Retailer Category", "0"));
                ddlretailcat.AppendDataBoundItems = true;
                ddlretailcat.DataSource = dt;
                ddlretailcat.DataTextField = "NAME";
                ddlretailcat.DataValueField = "ID";
                ddlretailcat.DataBind();

            }
            else
            {
                ddlretailcat.Items.Clear();
                ddlretailcat.Items.Insert(0, new ListItem("Select Retailer category", "0"));
            }



        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    #endregion

    //==========================================================//

    #region LoadCurrency
    public void LoadCurrency(string GroupID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dtCurrency = new DataTable();
            dtCurrency = ClsCustomer.BindCurrency(GroupID);
            if (dtCurrency.Rows.Count > 0)
            {
                this.ddlcurrency.Items.Clear();
                this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
                this.ddlcurrency.DataSource = dtCurrency;
                this.ddlcurrency.DataTextField = "CURRENCYNAME";
                this.ddlcurrency.DataValueField = "CURRENCYID";
                this.ddlcurrency.DataBind();

                if (dtCurrency.Rows.Count == 1)
                {
                    this.ddlcurrency.SelectedValue = Convert.ToString(dtCurrency.Rows[0]["CURRENCYID"]).Trim();
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCustomerType1
    public void LoadCustomerType1(string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlCustomer1.Items.Clear();
            ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
            ddlCustomer1.DataSource = ClsCustomer.BindCustomerType1(BSID);
            ddlCustomer1.DataTextField = "UTNAME";
            ddlCustomer1.DataValueField = "UTID";
            ddlCustomer1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCustomerType1
    public void LoadCustomerType1(string level, string UserId)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlCustomer1.Items.Clear();
            ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
            ddlCustomer1.DataSource = ClsCustomer.BindCustomerType1(level, UserId);
            ddlCustomer1.DataTextField = "UTNAME";
            ddlCustomer1.DataValueField = "UTID";
            ddlCustomer1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCustomerType1
    public void LoadCustomerType1(string level, string UserId, string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

            ddlCustomer1.Items.Clear();
            ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
            ddlCustomer1.DataSource = ClsCustomer.BindCustomerType1(level, UserId, BSID);
            ddlCustomer1.DataTextField = "UTNAME";
            ddlCustomer1.DataValueField = "UTID";
            ddlCustomer1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadState
    public void LoadState()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
            ddlState.DataSource = ClsCustomer.BindState();
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_ID";
            ddlState.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion
    
    #region LoadCustomerGrird
    public void LoadCustomerGrird(string UserID, string UserType)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (Request.QueryString["MENU"] == "Y")
            {
                DataTable dt = ClsCustomer.BindCustomerGrid(UserID, UserType);
                gvCustomer.DataSource = dt;
                //gvCustomer.Columns[14].Visible = false;
                //gvCustomer.Columns[14].Wrap = false;
                gvCustomer.DataBind();

            }
            else
            {
                gvCustomer.DataSource = ClsCustomer.BindCustomerGrid(UserID, UserType);
                gvCustomer.Columns[1].Visible = true;
                gvCustomer.Columns[6].Visible = true;
                gvCustomer.Columns[9].Visible = true;
                gvCustomer.Columns[8].Visible = true;
                gvCustomer.Columns[10].Visible = true;
                gvCustomer.Columns[11].Visible = true;
                gvCustomer.Columns[15].Visible = true;
                gvCustomer.Columns[16].Visible = true;
                gvCustomer.Columns[17].Visible = true;
                gvCustomer.Columns[18].Visible = true;
                gvCustomer.Columns[19].Visible = true;
                gvCustomer.Columns[20].Visible = true;
                gvCustomer.Columns[21].Visible = true;
                gvCustomer.Columns[22].Visible = false;
                gvCustomer.Columns[23].Visible = false;
                gvCustomer.Columns[24].Visible = false;
                gvCustomer.Columns[25].Visible = false;
                gvCustomer.Columns[26].Visible = false;
                gvCustomer.Columns[27].Visible = false;
                gvCustomer.Columns[28].Visible = false;
                gvCustomer.Columns[29].Visible = false;
                gvCustomer.Columns[30].Visible = false;
                gvCustomer.Columns[14].Visible = false;
              
                gvCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCustomerGrird
    public void LoadCustomerGrird(string UserId)// call after cancel 
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.BindCustomerGrid(UserId);

            if (dt.Rows.Count > 0)
            {
                Session["CUSTYPE_ID"] = dt.Rows[0]["CUSTYPE_ID"].ToString().Trim();
                gvCustomer.DataSource = dt;
                gvCustomer.DataBind();
                gvCustomer.Columns[48].Visible = false;
                gvCustomer.Columns[48].Wrap = false;
            }
            else
            {
                gvCustomer.DataSource = null;
                gvCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    private void ViewandHideCategory()
    {
        if (ddlCustomer1.SelectedValue == "B9343E49-D86B-49EA-9ACA-4F4F7315EC96" || ddlCustomer1.SelectedValue == "5E24E686-C9F4-4477-B84A-E4639D025135")
        {
            trcategory.Style["display"] = "";

        }
        else
        {
            trcategory.Style["display"] = "none";

        }
    }

    #region ddlCustomer1_SelectedIndexChanged
    protected void ddlCustomer1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (ddlCustomer1.SelectedValue == "B9343E49-D86B-49EA-9ACA-4F4F7315EC96")/*Retailer*/
            {
                trshop.Style["display"] = "";
                trRetDiv.Style["display"] = "";
                trcategory.Style["display"] = "";
            }
            else
            {
                trshop.Style["display"] = "none";
                trRetDiv.Style["display"] = "none";
                trcategory.Style["display"] = "none";
            }

            //this.ViewandHideCategory();
            if (ddlCustomer1.SelectedValue != "0")
            {
                if (ddlBusniessSegment.SelectedValue != "0")
                {
                    string BSID = "";
                    var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                    foreach (ListItem item in query)
                    {
                        BSID += item.Value + ",";
                    }
                    BSID = BSID.Substring(0, BSID.Length - 1);

                    /*----inactive date---*/
                    if (chkActive.Checked == true)
                    {
                        trinactivedate.Style["display"] = "none";
                    }
                    else
                    {
                        trinactivedate.Style["display"] = "";
                    }



                    DataTable dtlevel = new DataTable();
                    dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                    if (dtlevel.Rows.Count > 0)
                    {
                        this.BindReportingTo();
                        this.ddlReportToRole.SelectedValue = Session["USERTYPE"].ToString().Trim();
                        this.ddlReportToRole.Enabled = false;

                        this.BindReportingToUser(BSID);
                        this.ddlUnder.SelectedValue = Session["IUSERID"].ToString().Trim();
                        this.ddlUnder.Enabled = false;
                    }
                    else
                    {
                        this.BindReportingTo();
                        this.BindReportingToUser(BSID);
                    }

                    this.LoadRetailercategory(this.ddlCustomer1.SelectedValue);

                    if (ddlCustomer1.SelectedValue == "5E24E686-C9F4-4477-B84A-E4639D025135")  //For Distributor and
                    {
                        lstpurchasetype.SelectedValue = "1";
                        ddlAccGroup.SelectedValue = "f8db0bd3-5fe8-43cf-aadd-7855612bf421";
                    }
                    if (ddlCustomer1.SelectedValue == "826D656F-353F-430F-8966-4FE6BE3F67ED") // for Super Stockist
                    {
                        lstpurchasetype.SelectedValue = "1";
                        ddlAccGroup.SelectedValue = "e9fa4c79-85c2-4ad5-a703-42f039c7f194";
                    }

                }
                else
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Add(new ListItem("Select", "0"));
                    ddlReportToRole.Items.Clear();
                    ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
                }
            }
            else
            {
                ddlUnder.Items.Clear();
                ddlUnder.Items.Add(new ListItem("Select", "0"));
                ddlReportToRole.Items.Clear();
                ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + lstpurchasetype.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlReportToRole_SelectedIndexChanged
    protected void ddlReportToRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string BSID = "";
            var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                BSID += item.Value + ",";
            }
            if (BSID.Length > 0)
            {
                BSID = BSID.Substring(0, BSID.Length - 1);

                BindReportingToUser(BSID);
            }
            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    
    #region EDIT_Click
    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            divbtnCustomerSubmit.Style["display"] = "";
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string userid = Hdn_Fld.Value.ToString();
            this.AllControlEnabDisb(this.Hdn_controltagE.Value);
            DataTable dt = new DataTable();
            DataTable dtpurchasetype = new DataTable();
            dt = ClsCustomer.BindCustomerEditById(userid);
            dtpurchasetype = ClsCustomer.BindPurchaseEditById(userid);
            this.LoadMotherDepot();
            DataTable dtdepot = new DataTable();
            dtdepot = ClsCustomer.BindDepotByCustomer(userid);
            //if (this.ddlDepot.SelectedValue == "0")
            //{
            //    MessageBox1.ShowWarning("Dpot Not Map With This User");
            //    return;
            //}
            //else
            //{
            ddlDepot.SelectedValue = dtdepot.Rows[0]["DEPOTID"].ToString();
            //}

            if (dt.Rows.Count > 0)
            {
                this.txtName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                this.txtcode.Text = dt.Rows[0]["CODE"].ToString().Trim();
                this.lblCode.Visible = true;
                this.txtcode.Visible = true;
                this.txtcode.Enabled = false;
                this.txtshortname.Text = dt.Rows[0]["SHORTNAME"].ToString().Trim();
                var myList = new List<string>(dt.Rows[0]["BUSINESSSEGMENTID"].ToString().Split(','));
                ddlBusniessSegment.Items.Clear();
                DataTable dtlevel = new DataTable();
                dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                if (dtlevel.Rows.Count > 0)
                {
                    LoadBusinessSegment(Session["USERID"].ToString().Trim());
                    this.ddlReportToRole.Enabled = false;
                    this.ddlUnder.Enabled = false;
                }
                else
                {
                    if (Session["TPU"].ToString() == "TSI")
                    {
                        LoadBusinessSegment(dt.Rows[0]["CUSTOMERID"].ToString());
                    }
                    else
                    {
                        LoadBusinessSegment();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlBusniessSegment.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlBusniessSegment.Items[innercounter].Value)
                            {
                                ddlBusniessSegment.Items[innercounter].Selected = true;

                            }
                        }
                    }
                }

                var myList1 = new List<string>(dt.Rows[0]["GROUPID"].ToString().Split(','));
                ddlGroupType.Items.Clear();
                string BSID = "";
                var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BSID += item.Value + ",";

                }
                BSID = BSID.Substring(0, BSID.Length - 1);

                this.LoadGroup(BSID);
                if (dtlevel.Rows.Count > 0)
                {
                    LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);
                }
                else
                {
                    LoadCustomerType1(BSID);
                }

                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList1.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlGroupType.Items.Count; innercounter++)
                        {
                            if (myList1[counter] == ddlGroupType.Items[innercounter].Value)
                            {
                                ddlGroupType.Items[innercounter].Selected = true;

                            }
                        }
                    }
                }

                ClsParam Param = new ClsParam();
                if (ddlBusniessSegment.SelectedValue == Param.GT)
                {
                    DataTable dtso = ClsCustomer.BindSOBYDEPOT(ddlDepot.SelectedValue);
                    this.ddlSO.Items.Clear();
                    this.ddlSO.Items.Add(new ListItem("Select", "0"));
                    this.ddlSO.AppendDataBoundItems = true;
                    this.ddlSO.DataSource = dtso;
                    this.ddlSO.DataTextField = "USERNAME";
                    this.ddlSO.DataValueField = "USERID";
                    this.ddlSO.DataBind();
                    DataTable dtsoEdit = ClsCustomer.BindSObyCustomer(userid, ddlDepot.SelectedValue);
                    if (dtsoEdit.Rows.Count > 0)
                    {
                        //Flag=0 for not exist and deactive SO and Flag=1 for active SO
                        if (dtsoEdit.Rows[0]["FLAG"].ToString() == "1")
                        {

                            ddlSO.SelectedValue = dtsoEdit.Rows[0]["USERID"].ToString().Trim();
                        }
                        if (dtsoEdit.Rows[0]["FLAG"].ToString() == "0")
                        {
                            ddlSO.SelectedValue = "0";
                        }
                    }
                    this.trSO.Visible = true;
                    this.trBrandMap.Visible = true;
                    /*Customer Brand Mapping Logic*/
                    this.LoadBrandType();
                    this.ddlBrandType.SelectedValue = dt.Rows[0]["BILLING_TYPE"].ToString().Trim();
                    if (this.ddlBrandType.SelectedValue.Trim() == "1")
                    {
                        this.trinvoice.Visible = true;
                        this.LoadBrandGrid(userid, this.ddlDepot.SelectedValue.Trim(), "U");
                    }
                    else
                    {
                        this.trinvoice.Visible = false;
                        this.gvBrandTypeMap.DataSource = null;
                        this.gvBrandTypeMap.DataBind();
                    }
                }
                else
                {
                    this.trSO.Visible = false;
                    this.trBrandMap.Visible = false;
                    this.trinvoice.Visible = false;
                }

                LoadCustomerType1(ddlBusniessSegment.SelectedValue.Trim());
                if (dt.Rows[0]["CUSTYPE_ID"].ToString() != "")
                {
                    ddlCustomer1.SelectedValue = dt.Rows[0]["CUSTYPE_ID"].ToString();
                }
                else
                {
                    ddlCustomer1.SelectedValue = "0";
                }
                if (Convert.ToString(dt.Rows[0]["BUSINESSSEGMENTID"]).Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")//EXPORT
                {
                    this.divcountry.Visible = true;
                    this.divstate.Visible = false;
                }
                else
                {
                    this.divcountry.Visible = false;
                    this.divstate.Visible = true;
                    ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                    //LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                    //ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                    //LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                    //if (ddlCity.Items.Count > 1)
                    //{
                    //    ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                    //}

                }
                this.LoadCountry();
                this.ddlcountry.SelectedValue = Convert.ToString(dt.Rows[0]["COUNTRYID"]).Trim();


                this.LoadCompnyType();
                if (Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim() == null)
                {

                }
                else
                {
                    this.ddlcompanytype.SelectedValue = Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim();
                }


                //=== Added By Avishek Ghosh On 08-04-2016====//

                this.BindReportingTo();

                if (dt.Rows[0]["REPORTTOROLEID"].ToString() != "")
                {
                    this.ddlReportToRole.SelectedValue = dt.Rows[0]["REPORTTOROLEID"].ToString().Trim();
                }
                this.BindReportingToUser(BSID);

                if (dt.Rows[0]["PARENT_CUST_ID"].ToString() != "")
                {
                    ddlUnder.SelectedValue = dt.Rows[0]["PARENT_CUST_ID"].ToString().Trim();
                }

                string GRPID = "";
                var queryGrp = from ListItem item in ddlGroupType.Items where item.Selected select item;
                foreach (ListItem item in queryGrp)
                {
                    GRPID += item.Value + ",";

                }
                GRPID = GRPID.Substring(0, GRPID.Length - 1);

                this.LoadCurrency(GRPID.Trim());
                if (dt.Rows[0]["CURRENCYID"].ToString() != "")
                {
                    ddlcurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString().Trim();
                }
                //LoadAccGroup1();
                LoadAccGroup();
                this.LoadLedger();

                if (Convert.ToString(dt.Rows[0]["CUSTOMERID"]).Trim() != Convert.ToString(dt.Rows[0]["LEDGER_REFERENCEID"]).Trim())
                {
                    this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                    rdbledger.SelectedValue = "1";
                }

                if (this.ddlAccGroup.SelectedValue == "0")
                {
                    if (dt.Rows[0]["LEDGER_REFERENCEID"].ToString() != "")
                    {
                       this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                    }
                }
                if (dt.Rows[0]["ACCGROUPID"].ToString() != "")
                {
                    this.ddlAccGroup.SelectedValue = dt.Rows[0]["ACCGROUPID"].ToString().Trim();
                }
                if (this.ddlAccGroup.SelectedValue == "0")
                {
                    trledger.Style["display"] = "";
                    rdbledger.SelectedValue = "1";

                    trgroup.Style["display"] = "none";
                }
                else
                {
                    trledger.Style["display"] = "none";
                    rdbledger.SelectedValue = "0";
                    trgroup.Style["display"] = "";
                }

                //string Inactivedate = string.Empty;

                txtInactiveDate.Text = dt.Rows[0]["INACTIVEDATE"].ToString();

                txtPanNO.Text = dt.Rows[0]["PANCARDNO"].ToString();
                txtvatno.Text = dt.Rows[0]["VATNO"].ToString();
                txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
                txttinno.Text = dt.Rows[0]["TINNO"].ToString();
                txtContactPerson1.Text = dt.Rows[0]["CONTACTPERSON1"].ToString();
                txtContactPerson2.Text = dt.Rows[0]["CONTACTPERSON2"].ToString();
                txtEmailid1.Text = dt.Rows[0]["EMAILID1"].ToString();
                txtEmailid2.Text = dt.Rows[0]["EMAILID2"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MOBILE1"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["TELEPHONE1"].ToString();
                txtMobileNo1.Text = dt.Rows[0]["MOBILE2"].ToString();
                txtPhoneNo1.Text = dt.Rows[0]["TELEPHONE2"].ToString();
                txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                txtDeliveryaddress.Text = dt.Rows[0]["ALTERNATEADDRESS"].ToString();
                txtDistrict.Text= dt.Rows[0]["DISTRICTNAME"].ToString();
                txtCity.Text = dt.Rows[0]["CITYNAME"].ToString();
                txtDeliveryPIN.Text = dt.Rows[0]["ALTERNATEPIN"].ToString();
                this.txtprintname.Text = dt.Rows[0]["PRINTNAME"].ToString();
                this.LoadShopName();
                ddlShop.SelectedValue = dt.Rows[0]["SHOPID"].ToString().Trim();

                /*Added By Sayan Dey On 01/01/2018*/
                this.LoadRetDiv();

                this.LoadRetailercategory(this.ddlCustomer1.SelectedValue);
                this.ViewandHideCategory();
                if (Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim() != "")
                {
                    ddlretailcat.SelectedValue = Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim();
                }
                else
                {
                    ddlretailcat.SelectedValue = "0";
                }


                /*Added By Sayan Dey On 04/01/2018*/
                if (Convert.ToString(dt.Rows[0]["DOB"]).Trim() == "01/01/1900")
                {
                    this.txtDOB.Text = "";
                }
                else
                {
                    this.txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]).Trim();
                }

                if (Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim() == "01/01/1900")
                {
                    this.txtAnvDate.Text = "";
                }
                else
                {
                    this.txtAnvDate.Text = Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim();
                }

                txtamount.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString().Trim();


                txtPIN.Text = dt.Rows[0]["PIN"].ToString();

                if (dt.Rows[0]["ISACTIVE"].ToString() == "Active")
                {
                    chkActive.Checked = true;
                    trinactivedate.Style["display"] = "none";

                }
                else
                {
                    chkActive.Checked = false;
                    trinactivedate.Style["display"] = "";

                }

                if (dt.Rows[0]["ISTRANSFERTOHO"].ToString() == "Y")
                {
                    chktransfertoho.Checked = true;
                }
                else
                {
                    chktransfertoho.Checked = false;
                }

                /*Added By Sayan Dey On 01/01/2018*/
                if (dt.Rows[0]["CUSTYPE_ID"].ToString().Trim() == "B9343E49-D86B-49EA-9ACA-4F4F7315EC96")
                {
                    trRetDiv.Style["display"] = "";
                    this.ddlRetDiv.SelectedValue = dt.Rows[0]["RETDIVID"].ToString().Trim();
                }
                else
                {
                    trRetDiv.Style["display"] = "none";
                    this.ddlRetDiv.SelectedValue = "0";
                }


                if (dt.Rows[0]["APPLICABLEGST"].ToString() == "Y")
                {
                    chkgst.Checked = true;
                    string GSTNO = dt.Rows[0]["GSTNO"].ToString();

                    if (GSTNO != "")
                    {

                        if (GSTNO.Substring(0, 2).Length == 2)
                        {
                            this.txtstatecode.Text = GSTNO.Substring(0, 2);
                        }
                        else
                        {
                            this.txtstatecode.Text = "";
                        }

                        if (GSTNO.Substring(2, 10).Length == 10)
                        {
                            this.txtgstpanno.Text = GSTNO.Substring(2, 10);
                        }
                        else
                        {
                            this.txtgstpanno.Text = "";
                        }

                        if (GSTNO.Substring(12, 3).Length == 3)
                        {
                            this.txtgstno.Text = GSTNO.Substring(12, 3);
                        }
                        else
                        {
                            this.txtgstno.Text = "";
                        }
                    }
                }
                else
                {
                    chkgst.Checked = false;
                }
                txtpercentage.Text = dt.Rows[0]["PERCENTAGE"].ToString();
                txttcspercentage.Text = Convert.ToDecimal(dt.Rows[0]["TCSPERCENT"]).ToString().Trim();
                if (dt.Rows[0]["ADDSSMARGIN"].ToString() == "Y")
                {
                    this.chkaddssmagine.Checked = true;
                }
                else
                {
                    this.chkaddssmagine.Checked = false;
                }

                this.txtaddmarginpercentage.Text = dt.Rows[0]["ADDSSMARGINPERCENTAGE"].ToString().Trim();

                string Upload = dt.Rows[0]["FILENAME"].ToString();

                Hdn_Fld.Value = Convert.ToString(userid);
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                pnlDepotMapping.Style["display"] = "none";
                btnAddCity.Enabled = true;
                this.ddlcountry.Enabled = false;
                this.ddlcurrency.Enabled = false;
            }
            this.LoadPurchaseType();
            if (dtpurchasetype.Rows.Count > 0)
            {

                var myListTYPE = new List<string>(dtpurchasetype.Rows[0]["PURCHASETYPE"].ToString().Split(','));


                for (int counter = 0; counter < myListTYPE.Count; counter++)
                {
                    for (int innercounter = 0; innercounter < lstpurchasetype.Items.Count; innercounter++)
                    {
                        if (myListTYPE[counter] == lstpurchasetype.Items[innercounter].ToString().Trim())
                        {
                            lstpurchasetype.Items[innercounter].Selected = true;

                        }
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
    #endregion

    #region btnview_Click
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            this.AllControlEnabDisb(this.Hdn_controltagD.Value.Trim());

            string userid = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsCustomer.BindCustomerEditById(userid);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                txtcode.Text = dt.Rows[0]["CODE"].ToString().Trim();
                txtshortname.Text = dt.Rows[0]["SHORTNAME"].ToString().Trim();
                var myList = new List<string>(dt.Rows[0]["BUSINESSSEGMENTID"].ToString().Split(','));
                ddlBusniessSegment.Items.Clear();
                DataTable dtlevel = new DataTable();
                dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                if (dtlevel.Rows.Count > 0)
                {
                    LoadBusinessSegment(Session["USERID"].ToString().Trim());
                    this.ddlReportToRole.Enabled = false;
                    this.ddlUnder.Enabled = false;
                }
                else
                {
                    if (Session["TPU"].ToString() == "TSI")
                    {
                        LoadBusinessSegment(dt.Rows[0]["CUSTOMERID"].ToString());
                    }
                    else
                    {
                        LoadBusinessSegment();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlBusniessSegment.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlBusniessSegment.Items[innercounter].Value)
                            {
                                ddlBusniessSegment.Items[innercounter].Selected = true;

                            }
                        }
                    }
                }
                var myList1 = new List<string>(dt.Rows[0]["GROUPID"].ToString().Split(','));
                ddlGroupType.Items.Clear();
                string BSID = "";
                var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BSID += item.Value + ",";
                }
                BSID = BSID.Substring(0, BSID.Length - 1);

                this.LoadGroup(BSID);
                if (dtlevel.Rows.Count > 0)
                {
                    LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);
                }
                else
                {
                    LoadCustomerType1(BSID);
                }

                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList1.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlGroupType.Items.Count; innercounter++)
                        {
                            if (myList1[counter] == ddlGroupType.Items[innercounter].Value)
                            {
                                ddlGroupType.Items[innercounter].Selected = true;

                            }
                        }
                    }
                }

                ddlCustomer1.SelectedValue = dt.Rows[0]["CUSTYPE_ID"].ToString();

                /*SO Binding Logic*/
                ClsParam Param = new ClsParam();
                if (ddlBusniessSegment.SelectedValue == Param.GT)
                {
                    DataTable dtso = ClsCustomer.BindSOBYDEPOT(ddlDepot.SelectedValue);
                    this.ddlSO.Items.Clear();
                    this.ddlSO.Items.Add(new ListItem("Select", "0"));
                    this.ddlSO.AppendDataBoundItems = true;
                    this.ddlSO.DataSource = dtso;
                    this.ddlSO.DataTextField = "USERNAME";
                    this.ddlSO.DataValueField = "USERID";
                    this.ddlSO.DataBind();
                    DataTable dtsoEdit = ClsCustomer.BindSObyCustomer(userid, ddlDepot.SelectedValue);
                    if (dtsoEdit.Rows.Count > 0)
                    {
                        //Flag=0 for not exist and deactive SO and Flag=1 for active SO
                        if (dtsoEdit.Rows[0]["FLAG"].ToString() == "1")
                        {

                            ddlSO.SelectedValue = dtsoEdit.Rows[0]["USERID"].ToString().Trim();
                        }
                        if (dtsoEdit.Rows[0]["FLAG"].ToString() == "0")
                        {
                            ddlSO.SelectedValue = "0";
                        }
                    }
                    this.trSO.Visible = true;
                    this.trBrandMap.Visible = true;
                    /*Customer Brand Mapping Logic*/
                    this.LoadBrandType();
                    this.ddlBrandType.SelectedValue = dt.Rows[0]["BILLING_TYPE"].ToString().Trim();
                    if (this.ddlBrandType.SelectedValue.Trim() == "1")
                    {
                        this.trinvoice.Visible = true;
                        this.LoadBrandGrid(userid, this.ddlDepot.SelectedValue.Trim(), "U");
                    }
                    else
                    {
                        this.trinvoice.Visible = false;
                        this.gvBrandTypeMap.DataSource = null;
                        this.gvBrandTypeMap.DataBind();
                    }
                }
                else
                {
                    this.trSO.Visible = false;
                    this.trBrandMap.Visible = false;
                    this.trinvoice.Visible = false;
                }

                if (Convert.ToString(dt.Rows[0]["BUSINESSSEGMENTID"]).Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")//EXPORT
                {
                    this.divcountry.Visible = true;
                    this.divstate.Visible = false;
                }
                else
                {
                    this.divcountry.Visible = false;
                    this.divstate.Visible = true;
                    ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                    //LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                    //ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                    //LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                    //if (ddlCity.Items.Count > 1)
                    //{
                    //    ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                    //}

                }
                this.LoadCountry();
                this.ddlcountry.SelectedValue = Convert.ToString(dt.Rows[0]["COUNTRYID"]).Trim();

                //=== Added By Avishek Ghosh On 08-04-2016====//

                this.BindReportingTo();

                if (dt.Rows[0]["REPORTTOROLEID"].ToString() != "")
                {
                    this.ddlReportToRole.SelectedValue = dt.Rows[0]["REPORTTOROLEID"].ToString().Trim();
                }
                this.BindReportingToUser(BSID);

                if (dt.Rows[0]["PARENT_CUST_ID"].ToString() != "")
                {
                    ddlUnder.SelectedValue = dt.Rows[0]["PARENT_CUST_ID"].ToString().Trim();
                }

                string GRPID = "";
                var queryGrp = from ListItem item in ddlGroupType.Items where item.Selected select item;
                foreach (ListItem item in queryGrp)
                {
                    GRPID += item.Value + ",";

                }
                GRPID = GRPID.Substring(0, GRPID.Length - 1);

                this.LoadCurrency(GRPID.Trim());
                if (dt.Rows[0]["CURRENCYID"].ToString() != "")
                {
                    ddlcurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString().Trim();
                }
                //LoadAccGroup1();
                LoadAccGroup();
                if (dt.Rows[0]["ACCGROUPID"].ToString() != "")
                {
                    this.ddlAccGroup.SelectedValue = dt.Rows[0]["ACCGROUPID"].ToString().Trim();
                }

                txtPanNO.Text = dt.Rows[0]["PANCARDNO"].ToString();
                txtvatno.Text = dt.Rows[0]["VATNO"].ToString();
                txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
                txttinno.Text = dt.Rows[0]["TINNO"].ToString();
                txtContactPerson1.Text = dt.Rows[0]["CONTACTPERSON1"].ToString();
                txtContactPerson2.Text = dt.Rows[0]["CONTACTPERSON2"].ToString();
                txtEmailid1.Text = dt.Rows[0]["EMAILID1"].ToString();
                txtEmailid2.Text = dt.Rows[0]["EMAILID2"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MOBILE1"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["TELEPHONE1"].ToString();
                txtMobileNo1.Text = dt.Rows[0]["MOBILE2"].ToString();
                txtPhoneNo1.Text = dt.Rows[0]["TELEPHONE2"].ToString();
                txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                txtDeliveryaddress.Text = dt.Rows[0]["ALTERNATEADDRESS"].ToString();
                txtDeliveryPIN.Text = dt.Rows[0]["ALTERNATEPIN"].ToString();

                LoadShopName();
                ddlShop.SelectedValue = dt.Rows[0]["SHOPID"].ToString().Trim();

                /*Added By Sayan Dey On 01/01/2018*/
                this.LoadRetDiv();

                this.LoadRetailercategory(this.ddlCustomer1.SelectedValue);
                if (Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim() != "")
                {
                    ddlretailcat.SelectedValue = Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim();
                }
                else
                {
                    ddlretailcat.SelectedValue = "0";
                }

                /*Added By Sayan Dey On 04/01/2018*/
                if (Convert.ToString(dt.Rows[0]["DOB"]).Trim() == "01/01/1900")
                {
                    this.txtDOB.Text = "";
                }
                else
                {
                    this.txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]).Trim();
                }

                if (Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim() == "01/01/1900")
                {
                    this.txtAnvDate.Text = "";
                }
                else
                {
                    this.txtAnvDate.Text = Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim();
                }

                txtamount.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString().Trim();


                txtPIN.Text = dt.Rows[0]["PIN"].ToString();

                if (dt.Rows[0]["ISACTIVE"].ToString() == "Active")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (dt.Rows[0]["ISTRANSFERTOHO"].ToString() == "Y")
                {
                    chktransfertoho.Checked = true;
                }
                else
                {
                    chktransfertoho.Checked = false;
                }

                /*Added By Sayan Dey On 01/01/2018*/
                if (dt.Rows[0]["CUSTYPE_ID"].ToString().Trim() == "B9343E49-D86B-49EA-9ACA-4F4F7315EC96")
                {
                    trRetDiv.Style["display"] = "";
                    this.ddlRetDiv.SelectedValue = dt.Rows[0]["RETDIVID"].ToString().Trim();
                }
                else
                {
                    trRetDiv.Style["display"] = "none";
                    this.ddlRetDiv.SelectedValue = "0";
                }
                txtpercentage.Text = dt.Rows[0]["PERCENTAGE"].ToString();

                string Upload = dt.Rows[0]["FILENAME"].ToString();

                Hdn_Fld.Value = Convert.ToString(userid);
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                pnlDepotMapping.Style["display"] = "none";
                btnAddCity.Enabled = true;
                this.ddlcountry.Enabled = false;
                this.ddlcurrency.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnAddCity_Click
    protected void btnAddCity_Click(object sender, EventArgs e)
    {

        try
        {
            //ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            //string strPopup = "<script language='javascript' ID='script1'>"

            //// Passing intId to popup window.
            //+ "window.open('frmAddCity.aspx?stateid=" + HttpUtility.UrlEncode(ddlState.SelectedValue) + "&distid=" + HttpUtility.UrlEncode(ddlDistrict.SelectedValue)

            //+ "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            //+ "</script>";

            //ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnRefresh_Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //ddlDistrict_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnAddCustomer_Click
    protected void btnAddCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            this.Hdn_controltagE.Value = "E";
            this.AllControlEnabDisb(this.Hdn_controltagE.Value.Trim());
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            trshop.Style["display"] = "none";
            this.txtamount.Text = "500";
            Session["rowupload"] = null;
            ddlCustomer1.SelectedValue = "0";
            ddlUnder.SelectedValue = "0";
            ddlcurrency.SelectedValue = "0";
            txtName.Text = "";
            ddlAccGroup.SelectedValue = "0";
            this.lblCode.Style["display"] = "none";
            this.txtcode.Style["display"] = "none";
            txtPanNO.Text = "";
            txtContactPerson1.Text = "";
            txtContactPerson2.Text = "";
            txtMobileNo.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo1.Text = "";
            txtPhoneNo1.Text = "";
            txtEmailid1.Text = "";
            txtEmailid2.Text = "";
            txtAddress.Text = "";
            txtPIN.Text = "";
            ddlState.SelectedValue = "0";
            txtDistrict.Text = "";
            txtCity.Text = "";
            this.ddlretailcat.SelectedValue = "0";
            ddlShop.SelectedValue = "0";
            txtcstno.Text = "";
            txtvatno.Text = "";
            txttinno.Text = "";
            txtgstno.Text = "";
            txtDeliveryPIN.Text = "";
            txtDeliveryaddress.Text = "";
            chkActive.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            pnlProductMapping.Style["display"] = "none";
            txtpercentage.Text = "0";
            divstate.Visible = true;
            divcountry.Visible = false;
            this.ddlcurrency.Items.Clear();
            this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
            this.ddlcurrency.AppendDataBoundItems = true;
            this.ddlcountry.Enabled = true;
            this.ddlcurrency.Enabled = true;
            this.txtshortname.Text = "";
            this.txtDOB.Text = "";
            this.txtAnvDate.Text = "";
            LoadMotherDepot();

            divbtnCustomerSubmit.Style["display"] = "";
            if (rdbledger.SelectedValue == "0")
            {
                trgroup.Style["display"] = "";
                trledger.Style["display"] = "none";
            }
            this.LoadCompnyType();
            this.ddlcompanytype.SelectedValue = "1";
            this.chkaddssmagine.Checked = false;
            this.chkgst.Checked = false;
            this.txtgstno.Text = ""; ;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
            this.lstpurchasetype.Items.Clear();
            this.LoadPurchaseType();
            this.chktransfertoho.Checked = false;
            this.txtprintname.Text = "";


            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcompanytype.ClientID + "').focus(); ", true);
            ddlBusniessSegment.Focus();

            this.ddlretailcat.Items.Clear();
            this.ddlretailcat.Items.Insert(0, new ListItem("Select  Category", "0"));

            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected DataTable CreateLedgerTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEBITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CREDITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("OPENBALANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCETYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONNAME", typeof(string)));

        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
    }

    private void GetSateCode(int stateid)
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            if (chkgst.Checked == true)
            {
                string code = clstrans.BindStateCode(stateid);
                this.txtstatecode.Text = code.Trim();
                this.txtgstpanno.Text = this.txtPanNO.Text.Trim();
            }
            else
            {
                this.txtstatecode.Text = "";
                this.txtgstpanno.Text = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void chkgst_CheckedChanged(object sender, EventArgs e)
    {
        if (chkgst.Checked)
        {
            if (this.txtPanNO.Text == "")
            {
                MessageBox1.ShowInfo("Please enter PAN No");
                chkgst.Checked = false;
                return;
            }
            else
            {
                this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue.Trim()));
            }
        }
        else
        {
            this.txtgstno.Text = ""; ;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";

        }
    }



    #region btnCustomerSubmit_Click
    protected void btnCustomerSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ClsParam Param = new ClsParam();
            string Mode = "";
            string Upload = string.Empty;
            string CustomerID = string.Empty;
            string FileId = string.Empty;
            string StateID = string.Empty;
            string StateName = string.Empty;
            string DistrictID = string.Empty;
            string DistrictName = string.Empty;
            string CityID = string.Empty;
            string CityName = string.Empty;
            string CountryID = string.Empty;
            string CountryName = string.Empty;
            string RETDIVID = string.Empty;
            string RETDIVNAME = string.Empty;
            string ip_address = Request.UserHostAddress.ToString().Trim();
            string BrandType = string.Empty;
            string XML = string.Empty;

            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
                CustomerID = Guid.NewGuid().ToString().ToUpper();
                FileId = Guid.NewGuid().ToString().ToUpper();
            }
            else
            {
                Mode = "U";
                CustomerID = Hdn_Fld.Value.Trim();
                FileId = Hdn_Vatexemp.Value.Trim();
            }
            string tag = string.Empty;


            int ID = 0;

            if (ddlBusniessSegment.SelectedValue.Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")//Export
            {
                if (this.ddlcountry.SelectedValue.Trim() == "0")
                {
                    MessageBox1.ShowInfo("Please select Country");
                    return;
                }

                // SET State,District,City,Country
                StateID = this.ddlState.SelectedValue.Trim();
                StateName = "0";
                //DistrictID = this.ddlDistrict.SelectedValue.Trim();
                //DistrictName = "0";
                //CityID = this.ddlCity.SelectedValue.Trim();
                CityName = "0";
                CountryID = this.ddlcountry.SelectedValue.Trim();
                CountryName = this.ddlcountry.SelectedItem.ToString().Trim();
            }
            else
            {
                if (this.ddlState.SelectedValue.Trim() == "0" || this.txtDistrict.Text.Trim() == "" || this.txtCity.Text.Trim() == "")
                {
                    MessageBox1.ShowInfo("Please select State,District and City");
                    return;
                }

                // SET State,District,City,Country
                StateID = this.ddlState.SelectedValue.Trim();
                StateName = this.ddlState.SelectedItem.ToString().Trim();
                //DistrictID = this.ddlDistrict.SelectedValue.Trim();
                DistrictID = "0";
                DistrictName = this.txtDistrict.Text.ToString().Trim();
                //CityID = this.ddlCity.SelectedValue.Trim();
                CityID = "0";
                CityName = this.txtCity.Text.ToString().Trim();
                CountryID = "8F35D2B4-417C-406A-AD7F-7CB34F4D0B35";
                CountryName = "INDIA";
            }

            if (ddlBusniessSegment.SelectedValue.Trim() == Param.GT)
            {
                if (ddlSO.SelectedValue.Trim() == "0" && chkActive.Checked == true)
                {
                    MessageBox1.ShowWarning("Please select SO");
                    return;
                }

            }

            string BSID = "";
            string BSName = "";
            var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BSID += item.Value + ",";
                BSName += item.Text + ",";
            }

            if (BSID.Length > 1)
            {

                BSID = BSID.Substring(0, BSID.Length - 1);
                BSName = BSName.Substring(0, BSName.Length - 1);
            }


            string GRID = "";
            string GRName = "";
            var query1 = from ListItem item1 in ddlGroupType.Items where item1.Selected select item1;
            foreach (ListItem item1 in query1)
            {
                // item ...
                GRID += item1.Value + ",";
                GRName += item1.Text + ",";
            }
            if (GRID.Length > 1)
            {
                GRID = GRID.Substring(0, GRID.Length - 1);
                GRName = GRName.Substring(0, GRName.Length - 1);
            }



            string Purchasetypename = "";
            var queryPurchasetypename = from ListItem item1 in lstpurchasetype.Items where item1.Selected select item1;
            foreach (ListItem item1 in queryPurchasetypename)
            {
                Purchasetypename += item1.Text + ",";
            }

            if (Purchasetypename.Length > 0)
            {
                Purchasetypename = Purchasetypename.Substring(0, Purchasetypename.Length - 1);
            }

            string strPassword = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));

            if (rdbledger.SelectedValue == "0")
            {
                if (this.ddlAccGroup.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Account Group");
                    return;
                }
            }
            else
            {
                if (this.ddlledger.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Ledger");
                    return;
                }
            }
            string additionalmargine = string.Empty;
            decimal addmarginpercentage = 0;
            if (chkaddssmagine.Checked)
            {
                additionalmargine = "Y";
                if (this.txtaddmarginpercentage.Text.Trim() != "")
                {
                    addmarginpercentage = Convert.ToDecimal(this.txtaddmarginpercentage.Text.Trim());
                }

            }
            else
            {
                additionalmargine = "N";
            }



            string gstnno = string.Empty;
            if (chkgst.Checked)
            {
                int TOTALLENGTH = 0;
                TOTALLENGTH = ((this.txtgstpanno.Text.Length) + (this.txtstatecode.Text.Length));
                if (this.txtgstpanno.Text.Length != 10)
                {
                    MessageBox1.ShowInfo("Please Enter correct pan no!");
                    return;
                }
                if (TOTALLENGTH < 12)
                {
                    MessageBox1.ShowInfo("Please Enter Pan no!");
                    return;
                }

                if (this.txtgstno.Text.Length != 3)
                {
                    MessageBox1.ShowInfo("Please Enter 3 Digits GST No!");
                    return;
                }
                else
                {
                    gstnno = string.Concat(this.txtstatecode.Text.Trim() + this.txtgstpanno.Text + this.txtgstno.Text.Trim().ToUpper());
                }
            }

            string GSTAPPLICABLE = string.Empty;
            if (chkgst.Checked)
            {
                GSTAPPLICABLE = "Y";
            }
            else
            {
                GSTAPPLICABLE = "N";
            }

            string ISTRANSFERTOHO = string.Empty;
            if (chktransfertoho.Checked)
            {
                ISTRANSFERTOHO = "Y";
            }
            else
            {
                ISTRANSFERTOHO = "N";
            }
            if (ddlRetDiv.SelectedValue != "0")
            {
                RETDIVID = ddlRetDiv.SelectedValue.Trim();
                RETDIVNAME = ddlRetDiv.SelectedItem.ToString().Trim();
            }
            else
            {
                RETDIVID = "0";
                RETDIVNAME = "Select Retailer Division";
            }



            string Inactivedate = string.Empty;
            if (chkActive.Checked == true)
            {

                Inactivedate = "";
            }
            else
            {

                Inactivedate = txtInactiveDate.Text.Trim();

            }
            if (chkActive.Checked == false && txtInactiveDate.Text.Trim() == "")
            {
                MessageBox1.ShowInfo("Please provide inactive date ");


                return;
            }

            //if(chkgst.Checked==false)
            //{
            //    MessageBox1.ShowInfo("Please Click The GST Applicable CheckBox");
            //    return;
            //}

            if(Convert.ToDecimal(txtamount.Text.Trim()) <1 || Convert.ToDecimal(txtpercentage.Text.Trim())<1)
            {
                MessageBox1.ShowInfo("You Can Not Enter Credit Limit Days/ Credit Limit Ammount Less Than 1");
                return;
            }

            if (this.ddlBrandType.SelectedValue.Trim() == "1")
            {
                GridView gvunlock = gvBrandTypeMap;
                string gvchkbox = "";
                string gvbrandid = "";
                string gvbrandname = "";
                int count = 0;
                gvchkbox = "chkSelect";
                gvbrandid = "lblBRANDID";
                gvbrandname = "lblBRANDNAME";
                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblBRANDID = (Label)gvrow.FindControl("" + gvbrandid + "");
                    Label lblBRANDNAME = (Label)gvrow.FindControl("" + gvbrandname + "");
                    if (chkBx.Checked)
                    {
                        count = count + 1;

                    }
                }
                if (count == 0)
                {
                    MessageBox1.ShowInfo("Plese Select atleast 1 Brand... ");
                    return;
                }
            }

            if (ddlBrandType.SelectedValue.Trim() != "-1")
            {
                BrandType = ddlBrandType.SelectedValue.Trim();
            }
            else if (ddlBrandType.SelectedValue.Trim() == "-1")
            {
                BrandType = "0";
            }
            ID = ClsCustomer.SaveCustomerMaster(Hdn_Fld.Value.Trim(), CustomerID, strPassword, txtName.Text.ToString(), ddlCustomer1.SelectedValue.ToString(), ddlCustomer1.SelectedItem.Text.ToString(), BSID, BSName,
                                                     GRID, GRName, txtPanNO.Text.ToString(),
                                                     txtvatno.Text.ToString(), txtcstno.Text.ToString().Trim(), txttinno.Text.ToString(), txtContactPerson1.Text.ToString(), txtContactPerson2.Text.ToString(),
                                                     txtEmailid1.Text.ToString(), txtEmailid2.Text.ToString(), txtMobileNo.Text.ToString(), txtPhoneNo.Text.ToString(),
                                                     txtMobileNo1.Text.ToString(), txtPhoneNo1.Text.ToString(), txtAddress.Text.ToString(),
                                                     StateID.Trim(), StateName.Trim(),
                                                     DistrictID.Trim(), DistrictName.Trim(),
                                                     CityID.Trim(), CityName.Trim(), txtPIN.Text.ToString(), txtDeliveryaddress.Text, txtDeliveryPIN.Text, Mode,
                                                      chkActive.Checked.ToString(), txtcode.Text.Trim(), ddlUnder.SelectedValue.ToString(),
                                                      ddlUnder.SelectedItem.Text.ToString().Trim(), this.ddlReportToRole.SelectedValue.Trim(),
                                                      HttpContext.Current.Session["IUserID"].ToString(), Convert.ToDecimal(this.txtpercentage.Text.Trim()),
                                                      ddlShop.SelectedValue.Trim(), ddlShop.SelectedItem.ToString().Trim(),
                                                      Convert.ToString(ddlretailcat.SelectedValue).Trim(), Convert.ToString(ddlretailcat.SelectedItem).Trim(),
                                                      Convert.ToDecimal(txtamount.Text.Trim()), this.ddlcurrency.SelectedValue.ToString().Trim(),
                                                      this.ddlcurrency.SelectedItem.ToString().Trim(), ddlAccGroup.SelectedValue.ToString().Trim(),
                                                      ddlAccGroup.SelectedItem.ToString().Trim(), FileId, Upload.Trim(), tag,
                                                      CountryID.Trim(), CountryName.Trim(), this.txtshortname.Text.Trim(), ddlledger.SelectedValue.Trim()
                                                      , this.ddlcompanytype.SelectedValue.Trim(), additionalmargine.Trim(),
                                                      addmarginpercentage, gstnno.Trim(), GSTAPPLICABLE.Trim(), Purchasetypename.Trim(),
                                                      ISTRANSFERTOHO.Trim(), RETDIVID, RETDIVNAME, this.txtDOB.Text.Trim(), txtAnvDate.Text.Trim(),
                                                      this.txtprintname.Text.Trim(), ip_address, Convert.ToString(this.ddlDepot.SelectedValue),
                                                      Convert.ToString(this.ddlDepot.SelectedItem), Convert.ToString(this.ddlSO.SelectedValue),
                                                      Convert.ToString(this.ddlSO.SelectedItem).Trim(), Inactivedate.Trim(), BrandType, 
                                                      Convert.ToDecimal(txttcspercentage.Text.Trim()));
            if (ID == 2)
            {
                if (rdbledger.SelectedValue.Trim() == "0")
                {
                    if (Session["LEDGERDETAILS"] == null)
                    {
                        this.CreateLedgerTable();
                    }

                    DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
                    string xmlledger = string.Empty;
                    int ledger = 0;
                    DataTable dt = ClsCustomer.Depot();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow dr = dtledger.NewRow();
                        dr["ID"] = Guid.NewGuid();
                        dr["OPENBALANCE"] = 0;
                        dr["BALANCETYPE"] = "D";
                        dr["DEBITAMOUNT"] = 0;
                        dr["CREDITAMOUNT"] = 0;
                        dr["REGIONID"] = dt.Rows[j]["BRID"].ToString();
                        dr["REGIONNAME"] = dt.Rows[j]["BRNAME"].ToString();

                        dtledger.Rows.Add(dr);
                        dtledger.AcceptChanges();
                    }

                    xmlledger = ConvertDatatableToXML(dtledger);

                    DataTable deletedt = new DataTable();
                    if (Hdn_Fld.Value == "")
                    {
                        ViewState["mode"] = "A";
                    }
                    else
                    {
                        ViewState["mode"] = "U";
                    }
                    string Modedelete = ViewState["mode"].ToString();

                    ledger = ClsCustomer.SaveAccInfo(txtName.Text.Trim(), this.ddlAccGroup.SelectedValue.Trim(), Session["FINYEAR"].ToString(), Hdn_Fld.Value.Trim(), xmlledger, CustomerID.Trim(), Modedelete);
                }
                if (ddlBusniessSegment.SelectedValue.Trim() == Param.GT)
                {
                    if (this.ddlBrandType.SelectedValue.Trim() == "1")
                    {
                        int BrandMap = 0;
                        if (HttpContext.Current.Session["BRNDMAPPING"] == null)
                        {
                            CreateBrandMappingDataTable();
                        }
                        DataTable DT = (DataTable)HttpContext.Current.Session["BRNDMAPPING"];
                        GridView gvunlock = gvBrandTypeMap;
                        string gvchkbox = "";
                        string gvbrandid = "";
                        string gvbrandname = "";
                        int count = 0;
                        gvchkbox = "chkSelect";
                        gvbrandid = "lblBRANDID";
                        gvbrandname = "lblBRANDNAME";
                        foreach (GridViewRow gvrow in gvunlock.Rows)
                        {
                            CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                            Label lblBRANDID = (Label)gvrow.FindControl("" + gvbrandid + "");
                            Label lblBRANDNAME = (Label)gvrow.FindControl("" + gvbrandname + "");
                            if (chkBx.Checked)
                            {
                                DataRow dr = DT.NewRow();
                                dr["BRANDID"] = lblBRANDID.Text.Trim();
                                dr["BRANDNAME"] = lblBRANDNAME.Text.Trim();
                                dr["CUSTOMERID"] = CustomerID;
                                dr["CUSTOMERNAME"] = txtName.Text.Trim();
                                dr["CUSTOMERTYPEID"] = ddlCustomer1.SelectedValue.Trim();
                                dr["CUSTOMERTYPENAME"] = ddlCustomer1.SelectedItem.ToString().Trim();
                                dr["DEPOTID"] = ddlDepot.SelectedValue.Trim();
                                dr["DEPOTNAME"] = ddlDepot.SelectedItem.ToString().Trim();

                                DT.Rows.Add(dr);
                                DT.AcceptChanges();
                            }
                        }
                        HttpContext.Current.Session["BRNDMAPPING"] = DT;
                        if (DT != null)
                        {
                            if (DT.Rows.Count > 0)
                            {
                                XML = ConvertDatatable(DT);
                            }
                        }
                        BrandMap = ClsCustomer.SaveBrandMapping(CustomerID, XML);
                    }
                }

                if (ddlBusniessSegment.SelectedValue.Trim() == Param.GT)
                {
                    /*For All Brand Type*/
                    if (this.ddlBrandType.SelectedValue.Trim() == "0")
                    {
                        int BrandMap = 0;
                        int count = 0;
                        if (HttpContext.Current.Session["BRNDMAPPING"] == null)
                        {
                            CreateBrandMappingDataTable();
                            CreateCoreBrandDataTable();
                        }
                        DataTable DT = (DataTable)HttpContext.Current.Session["BRNDMAPPING"];
                        for (int i = 0; i <= 3; i++)
                        {
                            if (count == 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr["BRANDID"] = "1AC1960B-5C21-4C67-93D4-D38D1B3FAEB2";
                                dr["BRANDNAME"] = "HEAVENS GARDEN";
                                dr["CUSTOMERID"] = CustomerID;
                                dr["CUSTOMERNAME"] = txtName.Text.Trim();
                                dr["CUSTOMERTYPEID"] = ddlCustomer1.SelectedValue.Trim();
                                dr["CUSTOMERTYPENAME"] = ddlCustomer1.SelectedItem.ToString().Trim();
                                dr["DEPOTID"] = ddlDepot.SelectedValue.Trim();
                                dr["DEPOTNAME"] = ddlDepot.SelectedItem.ToString().Trim();
                                DT.Rows.Add(dr);
                                DT.AcceptChanges();
                            }
                            if (count == 1)
                            {
                                DataRow dr = DT.NewRow();
                                dr["BRANDID"] = "12E3F880-C465-4C0E-80B8-42E8DD05E51D";
                                dr["BRANDNAME"] = "SECRET TEMPTATION";
                                dr["CUSTOMERID"] = CustomerID;
                                dr["CUSTOMERNAME"] = txtName.Text.Trim();
                                dr["CUSTOMERTYPEID"] = ddlCustomer1.SelectedValue.Trim();
                                dr["CUSTOMERTYPENAME"] = ddlCustomer1.SelectedItem.ToString().Trim();
                                dr["DEPOTID"] = ddlDepot.SelectedValue.Trim();
                                dr["DEPOTNAME"] = ddlDepot.SelectedItem.ToString().Trim();
                                DT.Rows.Add(dr);
                                DT.AcceptChanges();
                            }
                            if (count == 2)
                            {
                                DataRow dr = DT.NewRow();
                                dr["BRANDID"] = "1865C411-3F93-4F03-9FE5-154C93A4CB7C";
                                dr["BRANDNAME"] = "WILD STONE";
                                dr["CUSTOMERID"] = CustomerID;
                                dr["CUSTOMERNAME"] = txtName.Text.Trim();
                                dr["CUSTOMERTYPEID"] = ddlCustomer1.SelectedValue.Trim();
                                dr["CUSTOMERTYPENAME"] = ddlCustomer1.SelectedItem.ToString().Trim();
                                dr["DEPOTID"] = ddlDepot.SelectedValue.Trim();
                                dr["DEPOTNAME"] = ddlDepot.SelectedItem.ToString().Trim();
                                DT.Rows.Add(dr);
                                DT.AcceptChanges();
                            }

                            count = count + 1;
                        }
                        HttpContext.Current.Session["BRNDMAPPING"] = DT;
                        if (DT != null)
                        {
                            if (DT.Rows.Count > 0)
                            {
                                XML = ConvertDatatable(DT);
                            }
                        }
                        BrandMap = ClsCustomer.SaveBrandMapping(CustomerID, XML);
                    }
                }

                MessageBox1.ShowSuccess("<b><font color='green'>Record saved successfully!</font></b>");
                //Clssendsms sendsms = new Clssendsms();
                //if(Convert.ToDecimal(txtamount.Text) >500)
                //{

                //string sms = string.Empty;
                //sms = sendsms.sendSMS("Dear Sir"+"%n %n"+" "+ txtName.Text.Trim()+" "+ Convert.ToString(this.ddlDepot.SelectedItem)+" " + "has increased credit limit of Rs"+" "+ txtamount.Text.Trim() +" "+Convert.ToString(DateTime.Now)+ "%n %n"+ "TEAM ERP");


                //}

                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
                this.pnlDepotMapping.Style["display"] = "none";
                this.Hdn_Fld.Value = "";
                this.ddlBusniessSegment.SelectedIndex = -1;
                this.ddlGroupType.SelectedIndex = -1;
                this.ddlUnder.Items.Clear();
                this.ddlUnder.Items.Add(new ListItem("Select", "0"));
                this.ddlReportToRole.Items.Clear();
                this.ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
                this.ddlReportToRole.Enabled = true;
                this.ddlUnder.Enabled = true;
                this.txtpercentage.Text = "";
                this.ddlCustomer1.Items.Clear();
                this.ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
                this.ddlShop.SelectedValue = "0";
                this.ddlretailcat.SelectedValue = "0";
                this.ddlcountry.SelectedValue = "0";
                this.ddlledger.SelectedValue = "0";
                this.txtamount.Text = "";
                DataTable dtlevel = new DataTable();
                dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                if (dtlevel.Rows.Count > 0)
                {
                    LoadCustomerGrird(Session["IUserID"].ToString().Trim());
                }
                else
                {
                    LoadCustomerGrird(Session["IUserID"].ToString().Trim(), Session["TPU"].ToString().Trim());
                }
                this.chkaddssmagine.Checked = false;
                this.txtaddmarginpercentage.Text = "";
                this.chkgst.Checked = false;
                this.txtgstno.Text = ""; ;
                this.txtstatecode.Text = "";
                this.txtgstpanno.Text = "";
                this.txtDOB.Text = "";
                this.txtAnvDate.Text = "";
                this.txtprintname.Text = "";
                Session.Remove("BRNDMAPPING");
                /*Added By Sayan Dey On 01/01/2018*/
                this.ddlRetDiv.SelectedValue = "0";

                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string strPopup = "<script language='javascript' ID='script2'>addTab('Tax', 'frmTaxSheetMaster.aspx')</script>";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                }


            }
            else if (ID == 5)
            {
                MessageBox1.ShowInfo("Name alredy exist!");
            }
            else if (ID == 4)
            {
                MessageBox1.ShowInfo("Pan No. alredy exist!");
            }
            else if (ID == 7)
            {
                MessageBox1.ShowInfo("Short Name alredy exist!");
                return;
            }
            else if (ID == 6)
            {
                MessageBox1.ShowInfo("User Name alredy exist in User Master!");
                return;
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful!</font></b>");
            }

            Session["LEDGERDETAILS"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }
    #endregion

    #region btnTPUCancel_Click
    protected void btnTPUCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            this.txtamount.Text = "";
            this.ddlretailcat.SelectedValue = "0";
            ddlCustomer1.SelectedValue = "0";
            txtName.Text = "";
            ddlBusniessSegment.SelectedIndex = -1;
            ddlGroupType.SelectedIndex = -1;
            txtPanNO.Text = "";
            txtContactPerson1.Text = "";
            txtContactPerson2.Text = "";
            txtMobileNo.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo1.Text = "";
            txtPhoneNo1.Text = "";
            txtEmailid1.Text = "";
            txtEmailid2.Text = "";
            txtAddress.Text = "";
            txtPIN.Text = "";
            ddlState.SelectedValue = "0";
            txtDistrict.Text = "";
            txtCity.Text = "";
            ddlShop.SelectedValue = "0";
            txtcstno.Text = "";
            txtvatno.Text = "";
            txttinno.Text = "";
            txtcode.Text = "";
            this.txtDOB.Text = "";
            this.txtAnvDate.Text = "";
            txtDeliveryPIN.Text = "";
            txtDeliveryaddress.Text = "";
            chkActive.Checked = true;
            txtInactiveDate.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlDepotMapping.Style["display"] = "none";
            pnlProductMapping.Style["display"] = "none";
            ddlUnder.Items.Clear();
            ddlUnder.Items.Add(new ListItem("Select", "0"));
            ddlReportToRole.Items.Clear();
            ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
            this.ddlReportToRole.Enabled = true;
            this.ddlUnder.Enabled = true;
            this.txtAddress.Text = "";
            ddlCustomer1.Items.Clear();
            ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
            this.ddlShop.SelectedValue = "0";
            this.ddlcountry.Enabled = true;
            this.ddlcurrency.Enabled = true;
            this.txtshortname.Text = "";
            LoadBusinessSegment(Session["USERID"].ToString().Trim());
            //LoadCustomerGrird(Session["IUserID"].ToString().Trim());
            LoadCustomerGrird(Session["IUserID"].ToString().Trim(), Session["TPU"].ToString().Trim());
            this.Hdn_controltagD.Value = "";
            this.Hdn_controltagE.Value = "";
            this.ddlledger.SelectedValue = "0";
            this.chkaddssmagine.Checked = false;
            this.txtaddmarginpercentage.Text = "";
            this.chkgst.Checked = false;
            this.txtgstno.Text = ""; ;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
            this.lstpurchasetype.Items.Clear();
            this.chktransfertoho.Checked = false;
            //BindGrid();
            //LoadCustomerGrird(Session["IUserID"].ToString().Trim());
            this.txtprintname.Text = "";
            this.ddlretailcat.Items.Clear();
            this.ddlretailcat.Items.Insert(0, new ListItem("Select  Category", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    #endregion

    #region DeleteRecord
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (e.Record["CUSTOMERID"] != "")
            {
                int ID = 0;
                ID = ClsCustomer.DeleteCustomerMaster(e.Record["CUSTOMERID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    DataTable dtlevel = new DataTable();
                    dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                    if (dtlevel.Rows.Count > 0)
                    {
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim());
                    }
                    else
                    {
                        LoadCustomerGrird(Session["IUserID"].ToString().Trim(), Session["TPU"].ToString().Trim());
                    }
                }
                else
                {
                    e.Record["Error"] = "Sorry, some invoice's already into system.";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadBusinessSegment
    public void LoadBusinessSegment(string UserId)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlBusniessSegment.Items.Clear();
            DataTable dtbs = ClsCustomer.BindCustomerType(UserId);
            ddlBusniessSegment.DataSource = dtbs;
            ddlBusniessSegment.DataTextField = "BSNAME";
            ddlBusniessSegment.DataValueField = "BSID";
            ddlBusniessSegment.DataBind();

            if (dtbs.Rows.Count == 1)
            {
                this.ddlBusniessSegment.SelectedValue = dtbs.Rows[0]["BSID"].ToString();
                this.LoadGroup(dtbs.Rows[0]["BSID"].ToString());

                string BSID = "";
                var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BSID += item.Value + "','";
                }
                BSID = BSID.Substring(0, BSID.Length - 3);

                DataTable dtlevel = new DataTable();
                dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                if (dtlevel.Rows.Count > 0)
                {
                    LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);
                }
                else
                {
                    LoadCustomerType1(BSID);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadBusinessSegment
    public void LoadBusinessSegment()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

            DataTable dtbs = ClsCustomer.BindCustomerType_V1(Session["USERID"].ToString());
            if (dtbs.Rows.Count > 0)
            {
                this.ddlBusniessSegment.Items.Clear();
                this.ddlBusniessSegment.Items.Insert(0, new ListItem("Select Business Segment", "0"));
                this.ddlBusniessSegment.DataSource = dtbs;
                this.ddlBusniessSegment.DataTextField = "BSNAME";
                this.ddlBusniessSegment.DataValueField = "BSID";
                this.ddlBusniessSegment.DataBind();

                /*if (dtbs.Rows.Count == 1)
                {
                    this.ddlBusniessSegment.SelectedValue = dtbs.Rows[0]["BSID"].ToString();
                    this.LoadGroup(dtbs.Rows[0]["BSID"].ToString());
                }*/
            }

            DataTable dtlevel = new DataTable();
            dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
            if (dtlevel.Rows.Count > 0)
            {
                string BSID = "";
                var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BSID += item.Value + "','";
                }
                BSID = BSID.Substring(0, BSID.Length - 3);
                LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadGroup
    public void LoadGroup(string id)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (ddlGroupType.SelectedValue == "")
            {
                ddlGroupType.Items.Clear();

            }

            this.ddlGroupType.Items.Clear();
            this.ddlGroupType.Items.Add(new ListItem("Select Group Type", "0"));
            this.ddlGroupType.AppendDataBoundItems = true;
            DataTable dtgroup = ClsCustomer.BindGroupType(id);
            ddlGroupType.DataSource = dtgroup;
            ddlGroupType.DataTextField = "DIS_CATNAME";
            ddlGroupType.DataValueField = "DIS_CATID";
            ddlGroupType.DataBind();

            if (dtgroup.Rows.Count == 1)
            {
                this.ddlGroupType.SelectedValue = dtgroup.Rows[0]["DIS_CATID"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadGroupALL
    public void LoadGroupALL()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlGroupType.Items.Clear();
            ddlGroupType.Items.Insert(0, new ListItem("Select Group Type", "0"));
            DataTable dtgroup = ClsCustomer.BindGroupTypeAll();
            ddlGroupType.DataSource = dtgroup;

            ddlGroupType.DataTextField = "DIS_CATNAME";
            ddlGroupType.DataValueField = "DIS_CATID";
            ddlGroupType.DataBind();

            if (dtgroup.Rows.Count == 1)
            {
                this.ddlGroupType.SelectedValue = dtgroup.Rows[0]["DIS_CATID"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCountry
    public void LoadCountry()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlcountry.Items.Clear();
            ddlcountry.Items.Insert(0, new ListItem("Select Country", "0"));
            ddlcountry.DataSource = ClsCustomer.BindCountry();
            ddlcountry.DataTextField = "COUNTRYNAME";
            ddlcountry.DataValueField = "COUNTRYID";
            ddlcountry.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    #region ddlBusniessSegment_SelectedIndexChanged
    protected void ddlBusniessSegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsParam Param = new ClsParam();

            if (ddlBusniessSegment.SelectedValue.Trim() == Param.GT)
            {
                this.trSO.Visible = true;
                this.ddlBrandType.SelectedValue = "1";

                if (this.ddlBrandType.SelectedValue.Trim() == "1")
                {
                    this.trBrandMap.Visible = true;
                    this.trinvoice.Visible = true;
                    if (Hdn_Fld.Value == "")
                    {
                        this.LoadBrandGrid("NA", "NA", "A");

                        GridView gvunlock = gvBrandTypeMap;
                        string gvchkbox = "";
                        gvchkbox = "chkSelect";
                        foreach (GridViewRow gvrow in gvunlock.Rows)
                        {
                            CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                            chkBx.Checked = true;
                        }
                    }
                    else
                    {
                        this.LoadBrandGrid(Hdn_Fld.Value.Trim(), this.ddlDepot.SelectedValue.Trim(), "U");
                    }
                }
                else
                {
                    this.trBrandMap.Visible = false;
                    this.trinvoice.Visible = false;
                    this.gvBrandTypeMap.DataSource = null;
                    this.gvBrandTypeMap.DataBind();
                }

            }
            else
            {
                this.trSO.Visible = false;
                this.trBrandMap.Visible = false;
                this.trinvoice.Visible = false;
                this.ddlBrandType.SelectedValue = "-1";
            }

            if (ddlBusniessSegment.SelectedValue.Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")/*EXPORT*/
            {
                divcountry.Visible = true;
                divstate.Visible = false;
            }
            else
            {
                divcountry.Visible = false;
                divstate.Visible = true;
            }

            /******inactive date******/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }

            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (ddlBusniessSegment.SelectedValue != "0")
            {
                string BSID = "";
                var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    BSID += item.Value + ",";
                }
                BSID = BSID.Substring(0, BSID.Length - 1);

                this.LoadGroup(BSID);

                if (Session["APPLICABLETO"].ToString() == "C")
                {
                    LoadCustomerType1(BSID);
                }
                else
                {
                    DataTable dtlevel = new DataTable();
                    dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
                    if (dtlevel.Rows.Count > 0)
                    {
                        LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);

                        this.BindReportingTo();
                        this.ddlReportToRole.SelectedValue = Session["USERTYPE"].ToString().Trim();
                        this.ddlReportToRole.Enabled = false;

                        this.BindReportingToUser(BSID);
                        this.ddlUnder.SelectedValue = Session["IUSERID"].ToString().Trim();
                        this.ddlUnder.Enabled = false;
                    }
                    else
                    {
                        this.BindReportingTo();
                    }
                }

                this.LoadCurrency(this.ddlGroupType.SelectedValue.Trim());
            }
            else
            {
                ddlCustomer1.Items.Clear();
                ddlCustomer1.Items.Insert(0, new ListItem("Select Customer Type", "0"));
                ddlGroupType.Items.Clear();
                ddlGroupType.Items.Insert(0, new ListItem("Select Group Type", "0"));
                ddlUnder.Items.Clear();
                ddlUnder.Items.Add(new ListItem("Select", "0"));
                ddlReportToRole.Items.Clear();
                ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
                this.trSO.Visible = false;
                this.trBrandMap.Visible = false;
                this.trinvoice.Visible = false;
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlGroupType.ClientID + "').focus(); ", true);
    }
    #endregion

    #region ddlGroupType_SelectedIndexChanged
    protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBusniessSegment.SelectedValue.Trim() != "0")
            {


                if (this.ddlGroupType.SelectedValue != "0")
                {

                    this.LoadCurrency(this.ddlGroupType.SelectedValue.Trim());

                }
            }
            if (this.ddlGroupType.SelectedValue == "")
            {
                this.ddlcurrency.SelectedValue = "0";
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlCustomer1.ClientID + "').focus(); ", true);
            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindReportingTo
    protected void BindReportingTo()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (ddlCustomer1.SelectedValue != "0")
            {
                ClsCustomerMaster ClsCustomerMaster = new ClsCustomerMaster();
                DataTable dt = new DataTable();
                dt = ClsCustomerMaster.BindReporintToRole(this.ddlCustomer1.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    this.ddlReportToRole.DataSource = dt;
                    this.ddlReportToRole.DataValueField = "UTID";
                    this.ddlReportToRole.DataTextField = "UTNAME";
                    this.ddlReportToRole.DataBind();

                    if (dt.Rows.Count == 1)
                    {
                        this.ddlReportToRole.SelectedValue = dt.Rows[0]["UTID"].ToString();
                    }
                }
            }
            else
            {
                ddlReportToRole.Items.Clear();
                ddlReportToRole.Items.Add(new ListItem("Select Report To Role", "0"));
                ddlReportToRole.AppendDataBoundItems = true;

                ddlUnder.Items.Clear();
                ddlUnder.Items.Add(new ListItem("Select", "0"));
                ddlUnder.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetReportingTo
    public void GetReportingTo(string utid, string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (utid != "")
            {
                ClsCustomerMaster ClsCustomerMaster = new ClsCustomerMaster();
                DataTable dt = new DataTable();
                dt = ClsCustomerMaster.BindReporintTo(utid, BSID);
                if (dt.Rows.Count > 0)
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Add(new ListItem("Select", "0"));
                    ddlUnder.AppendDataBoundItems = true;
                    ddlUnder.DataSource = dt;
                    ddlUnder.DataValueField = "USERID";
                    ddlUnder.DataTextField = "USERNAME";
                    ddlUnder.DataBind();

                    if (dt.Rows.Count == 1)
                    {
                        this.ddlUnder.SelectedValue = dt.Rows[0]["USERID"].ToString();
                    }
                }
                else
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Add(new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region GetReportingTo
    public void GetReportingTo(string utid, string IUserID, string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (utid != "")
            {
                ClsCustomerMaster ClsCustomerMaster = new ClsCustomerMaster();
                DataTable dt = new DataTable();
                dt = ClsCustomerMaster.BindReporintTo(utid, IUserID, BSID);
                if (dt.Rows.Count > 0)
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Add(new ListItem("Select", "0"));
                    ddlUnder.AppendDataBoundItems = true;
                    ddlUnder.DataSource = dt;
                    ddlUnder.DataValueField = "USERID";
                    ddlUnder.DataTextField = "USERNAME";
                    ddlUnder.DataBind();

                    if (dt.Rows.Count == 1)
                    {
                        this.ddlUnder.SelectedValue = dt.Rows[0]["USERID"].ToString();
                    }
                }
                else
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Add(new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region BindReportingToUser
    protected void BindReportingToUser(string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (Session["TPU"].ToString() == "TSI")
            {
                string ctype = ClsCustomer.CheckLastLevel(this.ddlCustomer1.SelectedValue.Trim());
                if (!string.IsNullOrEmpty(ctype))
                {
                    GetReportingTo(ddlReportToRole.SelectedValue.Trim(), Session["IUserID"].ToString().Trim(), BSID);
                }
                else
                {
                    GetReportingTo(ddlReportToRole.SelectedValue.Trim(), BSID);
                }
            }
            else
            {
                GetReportingTo(ddlReportToRole.SelectedValue.Trim(), BSID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region BindUnderCustomer
    public void BindUnderCustomer()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            int level = 0;
            DataTable dtlevel = new DataTable();
            dtlevel = ClsCustomer.GETLEVEL(ddlBusniessSegment.SelectedValue, ddlCustomer1.SelectedValue);
            if (dtlevel.Rows.Count > 0)
            {
                level = (Convert.ToInt32(dtlevel.Rows[0][0].ToString()) - 1);
                if (level <= 0)
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Insert(0, new ListItem("McNROE ", "-1"));
                }
                else
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Insert(0, new ListItem("Select", "0"));
                    ddlUnder.DataSource = ClsCustomer.GetUnderCustomer(ddlBusniessSegment.SelectedValue, level, ddlState.SelectedValue);
                    ddlUnder.DataTextField = "CUSTOMERNAME";
                    ddlUnder.DataValueField = "CUSTOMERID";
                    ddlUnder.DataBind();
                }

            }
            else
            {
                ddlUnder.Items.Clear();
                ddlUnder.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnder.DataSource = ClsCustomer.GetUnderUser();
                ddlUnder.DataTextField = "USERNAME";
                ddlUnder.DataValueField = "USERID";
                ddlUnder.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region BindUnderCustomerByParentID
    public void BindUnderCustomerByParentID(string bsid, string parentid)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            int level = 0;
            DataTable dtlevel = new DataTable();
            dtlevel = ClsCustomer.GETLEVELBYPARENTID(bsid, parentid);
            if (dtlevel.Rows.Count > 0)
            {
                level = Convert.ToInt32(dtlevel.Rows[0][0].ToString());
                if (level <= 0)
                {
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Insert(0, new ListItem("Select", "0"));
                    ddlUnder.DataSource = ClsCustomer.GetUnderUser();
                    ddlUnder.DataTextField = "USERNAME";
                    ddlUnder.DataValueField = "USERID";
                    ddlUnder.DataBind();
                }
                else
                {
                    level = (Convert.ToInt32(dtlevel.Rows[0][0].ToString()) - 1);
                    ddlUnder.Items.Clear();
                    ddlUnder.Items.Insert(0, new ListItem("Select", "0"));
                    ddlUnder.DataSource = ClsCustomer.GetUnderCustomer(ddlBusniessSegment.SelectedValue, level, ddlState.SelectedValue);
                    ddlUnder.DataTextField = "USERNAME";
                    ddlUnder.DataValueField = "USERID";
                    ddlUnder.DataBind();
                }
            }
            else
            {
                ddlUnder.Items.Clear();
                ddlUnder.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnder.DataSource = ClsCustomer.GetUnderUser();
                ddlUnder.DataTextField = "USERNAME";
                ddlUnder.DataValueField = "USERID";
                ddlUnder.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region gvCustomer_RowDataBound
    protected void gvCustomer_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[20] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Active")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Depot Mapping

    #region BindDepot
    protected void BindDepot()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.Depot();
            if (dt.Rows.Count > 0)
            {
                gvCustomerDepotMapping.DataSource = dt;
                gvCustomerDepotMapping.DataBind();
            }
            else
            {

                gvCustomerDepotMapping.DataSource = null;
                gvCustomerDepotMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("CUSTOMERTYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTNAME", typeof(string)));

        HttpContext.Current.Session["DEPOTMAPPINGDETAILS"] = dt;

        return dt;
    }
    #endregion

    # region DepotMappingButton
    protected void btncustDepotMapping_Click(object sender, EventArgs e)
    {
        try
        {
            BindDepot();
            CreateDataTable();
            pnlDepotMapping.Style["display"] = "";
            pnlProductMapping.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.Bindsaveddepotbycustid(Hdn_Fld.Value);

            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvCustomerDepotMapping.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelectvou");
                    Label lbllblBRID = (Label)gvrow.FindControl("lblBRID");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == chkBx.ToolTip)
                        {
                            chkBx.Checked = true;

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    # region DepotMappingCancel
    protected void btnCustDepotMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlDepotMapping.Style["display"] = "none";
            pnlProductMapping.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
        }
        catch (Exception ex)
        {
            throw ex;
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

    #region CustDepotMapSubmit
    protected void btnCustDepotMapSubmit_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

        try
        {
            string xml = string.Empty;
            int Depotcount = 0;
            int ID = 0;

            DataTable dtDepotRecordsCheck = (DataTable)HttpContext.Current.Session["DEPOTMAPPINGDETAILS"];

            if (gvCustomerDepotMapping.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvCustomerDepotMapping.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelectvou");
                    Label lbllblBRID = (Label)gvrow.FindControl("lblBRID");
                    Label lbllblBRNAME = (Label)gvrow.FindControl("lblBRNAME");
                    if (chkBx.Checked)
                    {
                        Depotcount = Depotcount + 1;
                        DataRow dr = dtDepotRecordsCheck.NewRow();
                        dr["CUSTOMERTYPE"] = Convert.ToString(hdnCustCategory.Value).Trim();
                        dr["CUSTOMERID"] = Convert.ToString(Hdn_Fld.Value);
                        dr["CUSTOMERNAME"] = Convert.ToString(this.txtCustomername.Text).Trim().Replace("&amp;", "&");
                        dr["DEPOTID"] = lbllblBRID.Text.Trim();
                        dr["DEPOTNAME"] = lbllblBRNAME.Text.Trim();
                        dtDepotRecordsCheck.Rows.Add(dr);
                        dtDepotRecordsCheck.AcceptChanges();
                    }
                }
            }
            if (Depotcount == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Depot / Mother Depot", 60, 500);
            }
            else
            {
                if (dtDepotRecordsCheck.Rows.Count > 0)
                {

                    xml = ConvertDatatableToXML(dtDepotRecordsCheck);
                    ID = ClsCustomer.SaveCustDepotMapping(Hdn_Fld.Value, xml);
                    if (ID > 0)
                    {
                        int updateldom = ClsCustomer.UpdateLdomCustomerMaster(Hdn_Fld.Value.Trim());
                        MessageBox1.ShowSuccess("Record saved successfully..");
                        hdnCustCategory.Value = "";
                        Hdn_Fld.Value = "";
                        txtCustomername.Text = "";
                        pnlDepotMapping.Style["display"] = "none";
                        pnlProductMapping.Style["display"] = "none";
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
                        Session.Remove("DEPOTMAPPINGDETAILS");
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
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
    #endregion

    #region   gvCustomerDepotMapping_RowDataBound
    protected void gvCustomerDepotMapping_RowDataBound(object sender, GridRowEventArgs e)
    {

        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            if (e.Row.RowType == GridRowType.DataRow && gvCustomerDepotMapping.Rows.Count > 0)
            {
                DataTable dt = ClsCustomer.Bindsaveddepotbycustid(Hdn_Fld.Value);
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkID") as CheckBox;
                HiddenField hiddenField = cell.FindControl("BRName") as HiddenField;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

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
    #endregion

    #endregion

    #region btnshow_Click
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {

            string strPopup = "<script language='javascript' ID='script2'>"

            // Passing intId to popup window.
            + "window.open('frmVatExpense.aspx?customerid=" + Hdn_Fld.Value.Trim() + ""

            + "','new window', 'top=200, left=1000, width=600, height=250, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    //=================Added By Sourav Mukherjee on 13/04/2016=================//

    #region OpenProductMapping
    protected void btnProductMapping_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
        this.CreateProductDataTable();
        this.pnlDepotMapping.Style["display"] = "none";
        this.pnlProductMapping.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "none";
        this.PnlAddress.Style["display"] = "none";
        this.PnlTransporter.Style["display"] = "none";
        this.gvProductadd.ClearPreviousDataSource();
        this.gvProductadd.DataSource = null;
        this.gvProductadd.DataBind();
        this.gvproductselect.ClearPreviousDataSource();
        this.gvproductselect.DataSource = null;
        this.gvproductselect.DataBind();
        this.ddlBrand.SelectedValue = "0";
        this.ddlCategory.SelectedValue = "0";
        DataTable dtProductEdit = (DataTable)HttpContext.Current.Session["PRODUCTMAPPING"];
        DataTable dtproduct = new DataTable();
        //string CustomerID = Convert.ToString(Hdn_Fld.Value).Trim();
        dtproduct = ClsCustomer.EditProduct(Hdn_Fld.Value);
        if (dtproduct.Rows.Count > 0)
        {

            for (int i = 0; i < dtproduct.Rows.Count; i++)
            {
                DataRow dr = dtProductEdit.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["PRODUCTID"] = Convert.ToString(dtproduct.Rows[i]["PRODUCTID"]).Trim();
                dr["PRODUCTNAME"] = Convert.ToString(dtproduct.Rows[i]["PRODUCTNAME"]).Trim();
                dr["BRANDID"] = Convert.ToString(dtproduct.Rows[i]["BRANDID"]).Trim();
                dr["BRANDNAME"] = Convert.ToString(dtproduct.Rows[i]["BRANDNAME"]).Trim();
                dr["CATEGORYID"] = Convert.ToString(dtproduct.Rows[i]["CATEGORYID"]).Trim();
                dr["CATEGORYNAME"] = Convert.ToString(dtproduct.Rows[i]["CATEGORYNAME"]).Trim();
                dr["CUSTOMERID"] = Convert.ToString(dtproduct.Rows[i]["CUSTOMERID"]).Trim();
                dr["CUSTOMERNAME"] = Convert.ToString(dtproduct.Rows[i]["CUSTOMERNAME"]).Trim();
                dtProductEdit.Rows.Add(dr);
                dtProductEdit.AcceptChanges();
            }

            HttpContext.Current.Session["PRODUCTGROUP"] = dtProductEdit;
            gvProductadd.DataSource = dtProductEdit;
            gvProductadd.DataBind();
        }
    }
    #endregion

    # region ProductMappingCancel
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        pnlDepotMapping.Style["display"] = "none";
        pnlProductMapping.Style["display"] = "none";
        this.PnlAddress.Style["display"] = "none";
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        btnaddhide.Style["display"] = "";

    }
    #endregion

    #region LoadBrand
    public void LoadBrand()
    {
        ClsCustomerMaster ClsProduct = new ClsCustomerMaster();
        try
        {
            ddlBrand.Items.Clear();
            ddlBrand.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlBrand.DataSource = ClsProduct.BindDivision();
            ddlBrand.DataTextField = "DIVNAME";
            ddlBrand.DataValueField = "DIVID";
            ddlBrand.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlBrand_SelectedIndexChanged
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
            ddlCategory.DataSource = ClsCustomer.BindCategory(ddlBrand.SelectedValue);
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataValueField = "CATID";
            ddlCategory.DataBind();

            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindProduct();

            /*----inactive date---*/
            if (chkActive.Checked == true)
            {
                trinactivedate.Style["display"] = "none";
            }
            else
            {
                trinactivedate.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindProduct
    protected void BindProduct()
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
        DataTable dt = ClsCustomer.BindProduct(ddlBrand.SelectedValue, ddlCategory.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            gvproductselect.DataSource = dt;
            gvproductselect.DataBind();
        }
        else
        {
            gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            gvproductselect.ClearPreviousDataSource();
            gvproductselect.DataSource = null;
            gvproductselect.DataBind();
        }
    }
    #endregion

    #region CreateProductDataTable
    public DataTable CreateProductDataTable()
    {
        DataTable dtProduct = new DataTable();

        dtProduct.Columns.Add("GUID", typeof(string));
        dtProduct.Columns.Add("CUSTOMERID", typeof(string));
        dtProduct.Columns.Add("CUSTOMERNAME", typeof(string));
        dtProduct.Columns.Add("BRANDID", typeof(string));
        dtProduct.Columns.Add("BRANDNAME", typeof(string));
        dtProduct.Columns.Add("CATEGORYID", typeof(string));
        dtProduct.Columns.Add("CATEGORYNAME", typeof(string));
        dtProduct.Columns.Add("PRODUCTID", typeof(string));
        dtProduct.Columns.Add("PRODUCTNAME", typeof(string));
        HttpContext.Current.Session["PRODUCTMAPPING"] = dtProduct;

        return dtProduct;
    }
    #endregion

    #region Add
    protected void btnproductadd_Click(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            bool FLAG = false;
            string ProductID = string.Empty;
            int state = 0;
            string ProductName = string.Empty;
            DataTable dtProduct = new DataTable();

            if (HttpContext.Current.Session["PRODUCTMAPPING"] == null)
            {

                dtProduct.Columns.Add("GUID", typeof(string));
                dtProduct.Columns.Add("CUSTOMERID", typeof(string));
                dtProduct.Columns.Add("CUSTOMERNAME", typeof(string));
                dtProduct.Columns.Add("BRANDID", typeof(string));
                dtProduct.Columns.Add("BRANDNAME", typeof(string));
                dtProduct.Columns.Add("CATEGORYID", typeof(string));
                dtProduct.Columns.Add("CATEGORYNAME", typeof(string));
                dtProduct.Columns.Add("PRODUCTID", typeof(string));
                dtProduct.Columns.Add("PRODUCTNAME", typeof(string));
                HttpContext.Current.Session["PRODUCTMAPPING"] = dtProduct;
            }
            else
            {

                dtProduct = (DataTable)HttpContext.Current.Session["PRODUCTMAPPING"];

            }
            if (dtProduct.Rows.Count == 0)
            {
                if (gvproductselect.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvproductselect.SelectedRecords)
                    {
                        Count = Count + 1;
                        DataRow dr = dtProduct.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["CUSTOMERID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                        dr["CUSTOMERNAME"] = Convert.ToString(this.txtCustomerproductname.Text);
                        dr["BRANDNAME"] = Convert.ToString(this.ddlBrand.SelectedItem).Trim();
                        dr["BRANDID"] = Convert.ToString(this.ddlBrand.SelectedValue);
                        dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                        dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue);
                        dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                        dtProduct.Rows.Add(dr);
                        dtProduct.AcceptChanges();
                    }
                }


                if (Count == 0)
                {

                    MessageBox1.ShowInfo("Please select atleast 1 Product");
                }
                else
                {
                    this.ddlBrand.SelectedValue = "0";
                    this.ddlCategory.SelectedValue = "0";
                    this.gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvproductselect.ClearPreviousDataSource();
                    this.gvproductselect.DataSource = null;
                    this.gvproductselect.DataBind();
                }
            }

            else if (dtProduct.Rows.Count > 0)
            {
                FLAG = false;
                ProductName = "";
                if (gvproductselect.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvproductselect.SelectedRecords)
                    {
                        for (int k = 0; k < gvProductadd.Rows.Count; k++)
                        {
                            ProductID = gvProductadd.Rows[k].Cells[8].Text.ToString();
                            Count = Count + 1;
                            if (ProductID == Convert.ToString(row["ID"]))
                            {
                                FLAG = true;
                                state = 2;
                                ProductName = ProductName + "," + Convert.ToString(row["NAME"]);
                                break;
                            }
                        }
                        if (FLAG == false)
                        {
                            DataRow dr = dtProduct.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["CUSTOMERID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                            dr["CUSTOMERNAME"] = Convert.ToString(this.txtCustomerproductname.Text);
                            dr["BRANDNAME"] = Convert.ToString(this.ddlBrand.SelectedItem).Trim();
                            dr["BRANDID"] = Convert.ToString(this.ddlBrand.SelectedValue);
                            dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                            dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue);
                            dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                            dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                            dtProduct.Rows.Add(dr);
                            dtProduct.AcceptChanges();
                        }
                    }
                }

                gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                gvproductselect.ClearPreviousDataSource();
                gvproductselect.DataSource = null;
                gvproductselect.DataBind();
                this.ddlBrand.SelectedValue = "0";
                this.ddlCategory.SelectedValue = "0";
                if (Count == 0)
                {
                    //MessageBox1.ShowInfo("Please select atleast 1 Product");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            ProductName = ProductName.Substring(0, ProductName.Length - 1);
                            MessageBox1.ShowSuccess("" + ProductName + " already exists", 60, 700);
                        }

                    }
                    else
                    {
                        this.ddlBrand.SelectedValue = "0";
                        this.ddlCategory.SelectedValue = "0";
                        this.gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                        this.gvproductselect.ClearPreviousDataSource();
                        this.gvproductselect.DataSource = null;
                        this.gvproductselect.DataBind();
                    }
                }

            }

            if (dtProduct.Rows.Count > 0)
            {
                gvProductadd.DataSource = dtProduct;

                gvProductadd.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region btnproductdelete_Click
    protected void btnproductdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(hdnproductdelete.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["PRODUCTMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
                //MessageBox1.ShowSuccess("Deleted Successfully");
            }
            this.gvProductadd.DataSource = dt;
            this.gvProductadd.DataBind();

            //Session["dtCity"] = null;
            //this.gvproductselect.ClearPreviousDataSource();
            //this.gvproductselect.DataSource = null;
            //this.gvproductselect.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Product Submit
    protected void btnProductSubmit_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

        try
        {
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["PRODUCTMAPPING"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;

            ID = ClsCustomer.SaveCustomerProductMapping(Hdn_Fld.Value, XML);
            if (dt.Rows.Count > 0)
            {
                if (ID > 0)
                {
                    int updateldom = ClsCustomer.UpdateLdomCustomerMaster(Hdn_Fld.Value.Trim());
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlProductMapping.Style["display"] = "none";
                    pnlDepotMapping.Style["display"] = "none";

                    gvProductadd.DataSource = null;
                    gvProductadd.DataBind();

                    this.ddlBrand.SelectedValue = "0";
                    this.ddlCategory.SelectedValue = "0";
                    this.gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvproductselect.ClearPreviousDataSource();
                    this.gvproductselect.DataSource = null;
                    this.gvproductselect.DataBind();

                }

                else
                {
                    MessageBox1.ShowError("Error on saving");
                }
            }


            MessageBox1.ShowSuccess("Record Saved Successfully..");
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlProductMapping.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";

            gvProductadd.DataSource = null;
            gvProductadd.DataBind();

            this.ddlBrand.SelectedValue = "0";
            this.ddlCategory.SelectedValue = "0";
            this.gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            this.gvproductselect.ClearPreviousDataSource();
            this.gvproductselect.DataSource = null;
            this.gvproductselect.DataBind();
            //else
            //{
            //    MessageBox1.ShowError("Please select atleast 1 record");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    //=========================================================================//

    //==================================ADD BY SUBHODIP DE ON 11/05/2016=============//
    # region Btn TSI Mappinf
    public void btntsimapping_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            pnltsimapping.Style["display"] = "";
            pnlAdd.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            this.PnlAddress.Style["display"] = "none";
            this.PnlTransporter.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Visible = false;
            LoadTSI();
            DataTable dt = ClsCustomer.EditTsimapping(Hdn_Fld.Value);
            if (dt.Rows.Count > 0)
            {
                txttsicustomername.Text = Convert.ToString(dt.Rows[0]["CUSTOMERNAME"]).Trim();
                //LoadTSI();
                var myList = new List<string>(dt.Rows[0]["TSI_ID"].ToString().Split(','));
                //lbtsi.Items.Clear();

                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < lbtsi.Items.Count; innercounter++)
                        {
                            if (myList[counter] == lbtsi.Items[innercounter].Value)
                            {
                                lbtsi.Items[innercounter].Selected = true;

                            }
                        }
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
    #endregion

    # region Btn PLOT Mappinf
    public void btnplotmapping_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            //pnlPlotMapping.Style["display"] = "";
            //pnlAdd.Style["display"] = "none";
            //pnlDepotMapping.Style["display"] = "none";
            //pnlDisplay.Style["display"] = "none";
            //btnaddhide.Visible = false;
            //this.city.Text = hdn_city.Value;

            DataTable dtmap = ClsCustomer.FetchMap(Hdn_Fld.Value);
            string latitude = dtmap.Rows[0]["LATITUDE"].ToString().Trim();
            string longitude = dtmap.Rows[0]["LONGITUDE"].ToString().Trim();

            string strPopup = "<script language='javascript' ID='script1'>"

                // Passing intId to popup window.
                + "window.open('frmGooglePlotMapping.aspx?customerid=" + HttpUtility.UrlEncode(Hdn_Fld.Value.ToString()) + "&customername=" + HttpUtility.UrlEncode(hdncustomername.Value.ToString()) + "&cityname=" + HttpUtility.UrlEncode(hdn_city.Value.ToString()) + "&latitude=" + HttpUtility.UrlEncode(latitude) + "&longitude=" + HttpUtility.UrlEncode(longitude)

                + "','popUpWindow', 'top=200, left=550, width=495, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=0, scrollbars=n, titlebar=no, toolbar=yes, status=no, center=yes')"

                + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    # region LoadTSI Name
    private void LoadTSI()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.BindTSI();
            lbtsi.Items.Clear();
            lbtsi.DataSource = dt;
            lbtsi.DataValueField = "USERID";
            lbtsi.DataTextField = "USERNAME";
            lbtsi.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    # endregion

    # region Btn TSI Submit
    public void btntsisubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

            int ID = 0;

            string TSIid = "";
            string TSIname = "";
            // LINQ over Items collection (must cast Items)
            var query = from ListItem item in lbtsi.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                TSIid += item.Value + ",";
                TSIname += item.Text + ",";
            }
            if (TSIid.Length > 0)
            {
                TSIid = TSIid.Substring(0, TSIid.Length - 1);
                TSIname = TSIname.Substring(0, TSIname.Length - 1);



                ID = ClsCustomer.SaveTSIMapping(Hdn_Fld.Value, txttsicustomername.Text.Trim(), TSIid.Trim(), TSIname.Trim());
                if (ID > 0)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    int updateldom = ClsCustomer.UpdateLdomCustomerMaster(Hdn_Fld.Value.Trim());
                    MessageBox1.ShowSuccess("<b><font color='green'>Record saved successfully!</font></b>");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlDepotMapping.Style["display"] = "none";
                    pnltsimapping.Style["display"] = "none";
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful!</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please select  atleast one TSI name!</font></b>");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    # endregion 

    # region Btn TSI Cancel
    public void btntsicancel_Click(object sender, EventArgs e)
    {
        try
        {

            pnltsimapping.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Visible = true;
            Hdn_Fld.Value = "";
            lbtsi.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    # endregion 

    # region Btn PLOT Submit
    public void btnplotsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int ID = 0;

            //ID = ClsCustomer.SaveTSIMapping(Hdn_Fld.Value, txttsicustomername.Text.Trim(), TSIid.Trim(), TSIname.Trim());
            if (ID > 0)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("<b><font color='green'>Record saved successfully!</font></b>");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                pnlDepotMapping.Style["display"] = "none";
                pnltsimapping.Style["display"] = "none";
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful!</font></b>");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    # endregion 

    # region Btn TSI Cancel
    public void btnplotcancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPlotMapping.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Visible = true;
            Hdn_Fld.Value = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    # endregion 
    //=====================================================//

    #region gvCustomer_Exporting
    protected void gvCustomer_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 18;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Customer Details";
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        cell.BackColor = Color.Gray;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region gvCustomer_Exported
    protected void gvCustomer_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        foreach (TableRow item in e.Table.Rows)
        {
            int cellcount = item.Cells.Count;
            for (int i = 0; i < cellcount; i++)
            {
                item.Cells[i].Style.Add("border", "thin solid black");

            }

            //not using css.
            e.Table.GridLines = GridLines.None;

        }
    }
    #endregion


    //****************************Addded by Sourav Mukherjee on 08/08/2016********************************//


    #region CreateDataTableStructure
    public DataTable CreateDataTableStructure()
    {
        DataTable dtAddress = new DataTable();
        dtAddress.Columns.Add("GUID", typeof(string));
        dtAddress.Columns.Add("CUSTOMERID", typeof(string));
        dtAddress.Columns.Add("CUSTOMERNAME", typeof(string));
        dtAddress.Columns.Add("ADDRESS", typeof(string));
        dtAddress.Columns.Add("ADDRESSID", typeof(string));
        dtAddress.Columns.Add("PINCODE", typeof(string));
        HttpContext.Current.Session["ADDRESSMAPPING"] = dtAddress;
        return dtAddress;
    }
    #endregion

    # region OpenAddressMapping
    protected void btnaddressmapping_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
        this.CreateDataTableStructure();
        this.pnlDepotMapping.Style["display"] = "none";
        this.pnlProductMapping.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.PnlAddress.Style["display"] = "";
        this.btnaddhide.Style["display"] = "none";
        this.PnlTransporter.Style["display"] = "none";
        this.grdaddress.ClearPreviousDataSource();
        this.grdaddress.DataSource = null;
        this.grdaddress.DataBind();
        this.txtmapaddress.Text = "";
        this.txtmappin.Text = "";

        DataTable dtaddressEdit = (DataTable)HttpContext.Current.Session["ADDRESSMAPPING"];
        DataTable dtadd = new DataTable();
        dtadd = ClsCustomer.EditAddressMapping(Hdn_Fld.Value);
        if (dtadd.Rows.Count > 0)
        {

            for (int i = 0; i < dtadd.Rows.Count; i++)
            {
                DataRow dr = dtaddressEdit.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["ADDRESS"] = Convert.ToString(dtadd.Rows[i]["ADDRESS"]).Trim();
                dr["ADDRESSID"] = Convert.ToString(dtadd.Rows[i]["ADDRESSID"]).Trim();
                dr["PINCODE"] = Convert.ToString(dtadd.Rows[i]["PINCODE"]).Trim();
                dr["CUSTOMERID"] = Convert.ToString(dtadd.Rows[i]["CUSTOMERID"]).Trim();
                dr["CUSTOMERNAME"] = Convert.ToString(dtadd.Rows[i]["CUSTOMERNAME"]).Trim();
                dtaddressEdit.Rows.Add(dr);
                dtaddressEdit.AcceptChanges();
            }

            HttpContext.Current.Session["ADDRESS"] = dtaddressEdit;
            grdaddress.DataSource = dtaddressEdit;
            grdaddress.DataBind();
        }
    }
    #endregion

    # region AddressMappingCancel
    protected void btnAddressCancel_Click(object sender, EventArgs e)
    {
        pnlDepotMapping.Style["display"] = "none";
        pnlProductMapping.Style["display"] = "none";
        this.PnlAddress.Style["display"] = "none";
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        btnaddhide.Style["display"] = "";

    }
    #endregion

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtAddress = new DataTable();
            if (HttpContext.Current.Session["ADDRESSMAPPING"] == null)
            {

                dtAddress.Columns.Add("GUID", typeof(string));
                dtAddress.Columns.Add("CUSTOMERID", typeof(string));
                dtAddress.Columns.Add("CUSTOMERNAME", typeof(string));
                dtAddress.Columns.Add("ADDRESS", typeof(string));
                dtAddress.Columns.Add("ADDRESSID", typeof(string));
                dtAddress.Columns.Add("PINCODE", typeof(string));
                HttpContext.Current.Session["ADDRESSMAPPING"] = dtAddress;
            }
            else
            {

                dtAddress = (DataTable)HttpContext.Current.Session["ADDRESSMAPPING"];

            }
            DataRow dr = dtAddress.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["CUSTOMERID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
            dr["CUSTOMERNAME"] = Convert.ToString(this.txtcustomer.Text.Trim());
            dr["PINCODE"] = Convert.ToString(this.txtmappin.Text.Trim());
            dr["ADDRESS"] = Convert.ToString(this.txtmapaddress.Text.Trim());
            dr["ADDRESSID"] = Convert.ToString(this.txtmapaddress.Text.Trim());
            dtAddress.Rows.Add(dr);
            dtAddress.AcceptChanges();


            if (dtAddress.Rows.Count > 0)
            {
                grdaddress.DataSource = dtAddress;
                grdaddress.DataBind();
            }

            txtmapaddress.Text = "";
            txtmappin.Text = "";
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region btnAddressSubmit_Click
    protected void btnAddressSubmit_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

        try
        {
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["ADDRESSMAPPING"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;

            ID = ClsCustomer.SaveCustomerAddressMapping(Hdn_Fld.Value, XML);
            if (dt.Rows.Count > 0)
            {
                if (ID > 0)
                {
                    int updateldom = ClsCustomer.UpdateLdomCustomerMaster(Hdn_Fld.Value.Trim());
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlProductMapping.Style["display"] = "none";
                    pnlDepotMapping.Style["display"] = "none";
                    PnlAddress.Style["display"] = "none";
                    grdaddress.DataSource = null;
                    grdaddress.DataBind();

                }

                else
                {
                    MessageBox1.ShowError("Error on saving");
                }
            }

            else
            {
                MessageBox1.ShowError("Please select atleast 1 record");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btndelete_Click
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(HdnGrdDel.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["ADDRESSMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
            }
            this.grdaddress.DataSource = dt;
            this.grdaddress.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    //****************************************************************************************************//


    #region CreateTransporterDataTable
    public DataTable CreateTransporterDataTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("GUID", typeof(string));
        dt.Columns.Add("CUSTOMERID", typeof(string));
        dt.Columns.Add("CUSTOMERNAME", typeof(string));
        dt.Columns.Add("TRANSPORTERID", typeof(string));
        dt.Columns.Add("TRANSPORTERNAME", typeof(string));
        dt.Columns.Add("CUSTYPE_ID", typeof(string));
        HttpContext.Current.Session["TRANSPORTERMAPPING"] = dt;
        return dt;
    }
    #endregion

    # region OpenProductMapping
    protected void btnTransporter_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
        string userid = Hdn_Fld.Value.ToString();
        DataTable dtedit = new DataTable();
        dtedit = ClsCustomer.BindCustomerEditById(userid);
        if (dtedit.Rows[0]["CUSTYPE_ID"].ToString() == "5E24E686-C9F4-4477-B84A-E4639D025135")
        {

            this.CreateTransporterDataTable();
            this.PnlTransporter.Style["display"] = "";
            this.pnlDepotMapping.Style["display"] = "none";
            this.pnlProductMapping.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.PnlAddress.Style["display"] = "none";
            DataTable dttrns = new DataTable();
            dttrns = ClsCustomer.BindTransporter();
            if (dttrns.Rows.Count > 0)
            {
                grdFirstGrid.DataSource = dttrns;
                grdFirstGrid.DataBind();
            }
            this.grdFinalGrid.ClearPreviousDataSource();
            this.grdFinalGrid.DataSource = null;
            this.grdFinalGrid.DataBind();
            DataTable dtTransporterEdit = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];
            DataTable dt = new DataTable();
            //string CustomerID = Convert.ToString(Hdn_Fld.Value).Trim();
            dt = ClsCustomer.EditTransporter(Hdn_Fld.Value);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtTransporterEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["CUSTYPE_ID"] = Convert.ToString(hdnCustCategory.Value).Trim();
                    dr["TRANSPORTERID"] = Convert.ToString(dt.Rows[i]["TRANSPORTERID"]).Trim();
                    dr["TRANSPORTERNAME"] = Convert.ToString(dt.Rows[i]["TRANSPORTERNAME"]).Trim();
                    dr["CUSTOMERID"] = Convert.ToString(dt.Rows[i]["CUSTOMERID"]).Trim();
                    dr["CUSTOMERNAME"] = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Trim();
                    dtTransporterEdit.Rows.Add(dr);
                    dtTransporterEdit.AcceptChanges();
                }

                HttpContext.Current.Session["TRANSPORTERGROUP"] = dtTransporterEdit;
                grdFinalGrid.DataSource = dtTransporterEdit;
                grdFinalGrid.DataBind();
            }
            else
            {
                grdFinalGrid.DataSource = null;
                grdFinalGrid.DataBind();
            }
        }
        else
        {
            MessageBox1.ShowInfo("Not Allowed");
            pnlAdd.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            pnltsimapping.Style["display"] = "none";
            pnlPlotMapping.Style["display"] = "none";
            this.PnlAddress.Style["display"] = "none";
            this.PnlTransporter.Style["display"] = "none";
        }
    }
    #endregion

    # region btnTransCancel_Click
    protected void btnTransCancel_Click(object sender, EventArgs e)
    {
        this.pnlDepotMapping.Style["display"] = "none";
        this.PnlTransporter.Style["display"] = "none";
        this.pnlProductMapping.Style["display"] = "none";
        this.PnlAddress.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.btnaddhide.Style["display"] = "";

    }
    #endregion

    #region btnTrnsporteradd_Click
    protected void btnTrnsporteradd_Click(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            bool FLAG = false;
            string TransporterID = string.Empty;
            int state = 0;
            string TransporterName = string.Empty;
            DataTable dt = new DataTable();

            if (HttpContext.Current.Session["TRANSPORTERMAPPING"] == null)
            {

                dt.Columns.Add("GUID", typeof(string));
                dt.Columns.Add("CUSTOMERID", typeof(string));
                dt.Columns.Add("CUSTOMERNAME", typeof(string));
                dt.Columns.Add("TRANSPORTERID", typeof(string));
                dt.Columns.Add("TRANSPORTERNAME", typeof(string));
                dt.Columns.Add("CUSTYPE_ID", typeof(string));
                HttpContext.Current.Session["TRANSPORTERMAPPING"] = dt;
            }
            else
            {

                dt = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];

            }
            if (dt.Rows.Count == 0)
            {
                if (grdFirstGrid.SelectedRecords != null)
                {
                    foreach (Hashtable row in grdFirstGrid.SelectedRecords)
                    {
                        Count = Count + 1;
                        DataRow dr = dt.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["CUSTOMERID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                        dr["CUSTOMERNAME"] = Convert.ToString(this.txtDistrbutor.Text);
                        dr["TRANSPORTERID"] = Convert.ToString(row["ID"]);
                        dr["TRANSPORTERNAME"] = Convert.ToString(row["NAME"]);
                        dr["CUSTYPE_ID"] = Convert.ToString(Hdncustid.Value).Trim();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }


                if (Count == 0)
                {

                    MessageBox1.ShowInfo("Please select atleast 1 Records");
                }
                else
                {


                }
            }

            else if (dt.Rows.Count > 0)
            {
                FLAG = false;
                TransporterName = "";
                if (grdFirstGrid.SelectedRecords != null)
                {
                    foreach (Hashtable row in grdFirstGrid.SelectedRecords)
                    {
                        for (int k = 0; k < grdFinalGrid.Rows.Count; k++)
                        {
                            TransporterID = grdFinalGrid.Rows[k].Cells[4].Text.ToString();
                            Count = Count + 1;
                            if (TransporterID == Convert.ToString(row["ID"]))
                            {
                                FLAG = true;
                                state = 2;
                                TransporterName = TransporterName + "," + Convert.ToString(row["NAME"]);
                                break;
                            }
                        }
                        if (FLAG == false)
                        {
                            DataRow dr = dt.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["CUSTOMERID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                            dr["CUSTOMERNAME"] = Convert.ToString(this.txtDistrbutor.Text);
                            dr["CUSTYPE_ID"] = Convert.ToString(Hdncustid.Value).Trim();
                            dr["TRANSPORTERID"] = Convert.ToString(row["ID"]);
                            dr["TRANSPORTERNAME"] = Convert.ToString(row["NAME"]);
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                }

                grdFirstGrid.SelectedRecords = null;

                if (Count == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast Record");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            TransporterName = TransporterName.Substring(0, TransporterName.Length - 1);
                            MessageBox1.ShowSuccess("" + TransporterName + " already exists", 60, 700);
                        }

                    }
                    else
                    {

                    }
                }

            }

            if (dt.Rows.Count > 0)
            {
                grdFinalGrid.DataSource = dt;
                grdFinalGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region btnTransSubmit_Click
    protected void btnTransSubmit_Click(object sender, EventArgs e)
    {
        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

        try
        {
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;

            ID = ClsCustomer.SaveCustomerTransporterMapping(Hdn_Fld.Value, XML);
            if (dt.Rows.Count > 0)
            {
                if (ID > 0)
                {
                    int updateldom = ClsCustomer.UpdateLdomCustomerMaster(Hdn_Fld.Value.Trim());
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlProductMapping.Style["display"] = "none";
                    pnlDepotMapping.Style["display"] = "none";
                    PnlTransporter.Style["display"] = "none";


                    this.grdFirstGrid.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.grdFirstGrid.ClearPreviousDataSource();
                    this.grdFirstGrid.DataSource = null;
                    this.grdFirstGrid.DataBind();

                }

                else
                {
                    MessageBox1.ShowError("Error on saving");
                }
            }


            //MessageBox1.ShowSuccess("Record Saved Successfully..");
            //pnlAdd.Style["display"] = "none";
            //pnlDisplay.Style["display"] = "";
            //btnaddhide.Style["display"] = "";
            //pnlProductMapping.Style["display"] = "none";
            //pnlDepotMapping.Style["display"] = "none";

            //gvProductadd.DataSource = null;
            //gvProductadd.DataBind();

            //this.ddlBrand.SelectedValue = "0";
            //this.ddlCategory.SelectedValue = "0";
            //this.gvproductselect.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            //this.gvproductselect.ClearPreviousDataSource();
            //this.gvproductselect.DataSource = null;
            //this.gvproductselect.DataBind();


            //else
            //{
            //    MessageBox1.ShowError("Please select atleast 1 record");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btntransporterdelete_Click
    protected void btntransporterdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(Hdn_Fld_Delete.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["TRANSPORTERMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
                //MessageBox1.ShowSuccess("Deleted Successfully");
            }
            this.grdFinalGrid.DataSource = dt;
            this.grdFinalGrid.DataBind();

            //Session["dtCity"] = null;
            //this.gvproductselect.ClearPreviousDataSource();
            //this.gvproductselect.DataSource = null;
            //this.gvproductselect.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btngeneraltemp_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster clscust = new ClsCustomerMaster();
            DataTable dt = clscust.Bindtemplete();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=CustomerDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            //int i;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    tab = "";
            //    for (i = 0; i < dt.Columns.Count; i++)
            //    {
            //        Response.Write(tab + dr[i].ToString());
            //        tab = "\t";
            //    }
            //    Response.Write("\n");
            //}
            Response.End();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    /*Added By Sayan Dey On 01/01/2018*/
    #region LoadRetDiv
    public void LoadRetDiv()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlRetDiv.Items.Clear();
            ddlRetDiv.Items.Insert(0, new ListItem("Select Retailer Division", "0"));
            ddlRetDiv.DataSource = ClsCustomer.BindRetailerDivision();
            ddlRetDiv.DataTextField = "RETDIVNAME";
            ddlRetDiv.DataValueField = "RETDIVID";
            ddlRetDiv.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadFilterCustomerType
    public void LoadFilterCustomerType()
    {
        try
        {
            ClsStockReport ClsCustomer = new ClsStockReport();
            ddlcustype.Items.Clear();
            ddlcustype.DataSource = ClsCustomer.BindType();
            ddlcustype.DataTextField = "UTNAME";
            ddlcustype.DataValueField = "UTID";
            ddlcustype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    public void LoadFilterAccGroup()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlaccountsgroup.Items.Clear();
            ddlaccountsgroup.DataSource = ClsCustomer.BindAccGroup();
            //ddlaccountsgroup.DataSource = ClsCustomer.BindAccGroupKKG();
            ddlaccountsgroup.DataTextField = "grpName";
            ddlaccountsgroup.DataValueField = "Code";
            ddlaccountsgroup.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    public void BindGrid()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string BSID = "";
            var query = from ListItem item in ddlsearchbs.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                BSID += item.Value + ',';

            }
            string DEPOTID = "";
            var querydepot = from ListItem item in ddldepots.Items where item.Selected select item;
            foreach (ListItem item in querydepot)
            {
                // item ...
                DEPOTID += item.Value + ',';

            }
            string ACCOUNTS = "";
            var queryacc = from ListItem item in ddlaccountsgroup.Items where item.Selected select item;
            foreach (ListItem item in queryacc)
            {
                // item ...
                ACCOUNTS += item.Value + ',';

            }
            string TYPE = "";
            var querycustype = from ListItem item in ddlcustype.Items where item.Selected select item;
            foreach (ListItem item in querycustype)
            {
                // item ...
                TYPE += item.Value + ',';

            }

            if (BSID.Length > 0)
            {
                BSID = BSID.Substring(0, BSID.Length - 1);
                DEPOTID = DEPOTID.Substring(0, DEPOTID.Length - 1);
                ACCOUNTS = ACCOUNTS.Substring(0, ACCOUNTS.Length - 1);
                TYPE = TYPE.Substring(0, TYPE.Length - 1);

                DataTable dt = ClsCustomer.BindCustomerGridbyBSGroup(HttpContext.Current.Session["USERID"].ToString().Trim(), BSID.Trim(), DEPOTID, TYPE, ACCOUNTS);
                if (Request.QueryString["MENU"] == "Y")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvCustomer.DataSource = dt;
                        gvCustomer.Columns[17].Visible = true;
                        gvCustomer.Columns[18].Visible = true;
                        gvCustomer.Columns[19].Visible = true;
                        gvCustomer.Columns[20].Visible = true;
                        gvCustomer.Columns[21].Visible = true;
                        gvCustomer.Columns[22].Visible = true;
                        gvCustomer.Columns[23].Visible = true;
                        gvCustomer.Columns[24].Visible = true;
                        gvCustomer.DataBind();
                    }
                    else
                    {
                        gvCustomer.DataSource = null;
                        gvCustomer.DataBind();
                    }
                }

                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvCustomer.DataSource = dt;

                        gvCustomer.Columns[7].Visible = true;
                        gvCustomer.Columns[12].Visible = true;
                        gvCustomer.Columns[20].Visible = true;
                        gvCustomer.Columns[21].Visible = true;
                        gvCustomer.Columns[29].Visible = false;
                        gvCustomer.AllowPageSizeSelection = true;
                        gvCustomer.DataBind();
                    }
                    else
                    {
                        gvCustomer.DataSource = null;
                        gvCustomer.DataBind();
                    }
                }
            }
            else
            {
                gvCustomer.DataSource = null;
                gvCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /*Method Created By Arti Agarwal On 05.11.2018*/
    public void EditCustomerbyCreditAmount(string CustomerId, string ControlMode)
    {

        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
        string userid = CustomerId;
        this.Hdn_controltagE.Value = ControlMode;

        this.AllControlEnabDisb(Hdn_controltagE.Value.Trim());
        DataTable dt = new DataTable();
        DataTable dtpurchasetype = new DataTable();
        dt = ClsCustomer.BindCustomerEditById(userid);
        dtpurchasetype = ClsCustomer.BindPurchaseEditById(userid);
        this.LoadMotherDepot();
        DataTable dtdepot = new DataTable();
        dtdepot = ClsCustomer.BindDepotByCustomer(userid);

        ddlDepot.SelectedValue = dtdepot.Rows[0]["DEPOTID"].ToString();


        if (dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
            txtcode.Text = dt.Rows[0]["CODE"].ToString().Trim();
            this.lblCode.Visible = true;
            this.txtcode.Visible = true;
            this.txtcode.Enabled = false;
            txtshortname.Text = dt.Rows[0]["SHORTNAME"].ToString().Trim();
            var myList = new List<string>(dt.Rows[0]["BUSINESSSEGMENTID"].ToString().Split(','));
            ddlBusniessSegment.Items.Clear();
            DataTable dtlevel = new DataTable();
            dtlevel = ClsCustomer.FetchLevel(Session["USERID"].ToString(), Session["USERTYPE"].ToString());
            if (dtlevel.Rows.Count > 0)
            {
                LoadBusinessSegment(Session["USERID"].ToString().Trim());
                this.ddlReportToRole.Enabled = false;
                this.ddlUnder.Enabled = false;
            }
            else
            {
                if (Session["TPU"].ToString() == "TSI")
                {
                    LoadBusinessSegment(dt.Rows[0]["CUSTOMERID"].ToString());
                }
                else
                {
                    LoadBusinessSegment();
                }
            }

            if (dt.Rows.Count > 0)
            {
                for (int counter = 0; counter < myList.Count; counter++)
                {
                    for (int innercounter = 0; innercounter < ddlBusniessSegment.Items.Count; innercounter++)
                    {
                        if (myList[counter] == ddlBusniessSegment.Items[innercounter].Value)
                        {
                            ddlBusniessSegment.Items[innercounter].Selected = true;

                        }
                    }
                }
            }

            var myList1 = new List<string>(dt.Rows[0]["GROUPID"].ToString().Split(','));
            ddlGroupType.Items.Clear();
            string BSID = "";
            var query = from ListItem item in ddlBusniessSegment.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                BSID += item.Value + ",";

            }
            BSID = BSID.Substring(0, BSID.Length - 1);



            this.LoadGroup(BSID);
            if (dtlevel.Rows.Count > 0)
            {
                LoadCustomerType1(dtlevel.Rows[0][0].ToString(), Session["USERID"].ToString().Trim(), BSID);
            }
            else
            {
                LoadCustomerType1(BSID);
            }

            if (dt.Rows.Count > 0)
            {
                for (int counter = 0; counter < myList1.Count; counter++)
                {
                    for (int innercounter = 0; innercounter < ddlGroupType.Items.Count; innercounter++)
                    {
                        if (myList1[counter] == ddlGroupType.Items[innercounter].Value)
                        {
                            ddlGroupType.Items[innercounter].Selected = true;

                        }
                    }
                }
            }

            ClsParam Param = new ClsParam();
            if (ddlBusniessSegment.SelectedValue == Param.GT)
            {
                DataTable dtso = ClsCustomer.BindSOBYDEPOT(ddlDepot.SelectedValue);
                this.ddlSO.Items.Clear();
                this.ddlSO.Items.Add(new ListItem("Select", "0"));
                this.ddlSO.AppendDataBoundItems = true;
                this.ddlSO.DataSource = dtso;
                this.ddlSO.DataTextField = "USERNAME";
                this.ddlSO.DataValueField = "USERID";
                this.ddlSO.DataBind();
                DataTable dtsoEdit = ClsCustomer.BindSObyCustomer(userid, ddlDepot.SelectedValue);
                if (dtsoEdit.Rows.Count > 0)
                {
                    //Flag=0 for not exist and deactive SO and Flag=1 for active SO
                    if (dtsoEdit.Rows[0]["FLAG"].ToString() == "1")
                    {

                        ddlSO.SelectedValue = dtsoEdit.Rows[0]["USERID"].ToString().Trim();
                    }
                    if (dtsoEdit.Rows[0]["FLAG"].ToString() == "0")
                    {
                        ddlSO.SelectedValue = "0";
                    }


                }
                this.trSO.Visible = true;
                this.trBrandMap.Visible = true;
                /*Customer Brand Mapping Logic*/
                this.LoadBrandType();
                this.ddlBrandType.SelectedValue = dt.Rows[0]["BILLING_TYPE"].ToString().Trim();
                if (this.ddlBrandType.SelectedValue.Trim() == "1")
                {
                    this.trinvoice.Visible = true;
                    this.LoadBrandGrid(userid, this.ddlDepot.SelectedValue.Trim(), "U");
                }
                else
                {
                    this.trinvoice.Visible = false;
                    this.gvBrandTypeMap.DataSource = null;
                    this.gvBrandTypeMap.DataBind();
                }
            }
            else
            {
                this.trSO.Visible = false;
                this.trBrandMap.Visible = false;
                this.trinvoice.Visible = false;
            }

            LoadCustomerType1(ddlBusniessSegment.SelectedValue.Trim());
            if (dt.Rows[0]["CUSTYPE_ID"].ToString() != "")
            {
                ddlCustomer1.SelectedValue = dt.Rows[0]["CUSTYPE_ID"].ToString();
            }
            else
            {
                ddlCustomer1.SelectedValue = "0";
            }
            if (Convert.ToString(dt.Rows[0]["BUSINESSSEGMENTID"]).Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")//EXPORT
            {
                this.divcountry.Visible = true;
                this.divstate.Visible = false;
            }
            else
            {
                this.divcountry.Visible = false;
                this.divstate.Visible = true;
                ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                //LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                //ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                //LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                //if (ddlCity.Items.Count > 1)
                //{
                //    ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                //}

            }
            this.LoadCountry();
            this.ddlcountry.SelectedValue = Convert.ToString(dt.Rows[0]["COUNTRYID"]).Trim();


            this.LoadCompnyType();
            if (Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim() == null)
            {

            }
            else
            {
                this.ddlcompanytype.SelectedValue = Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim();
            }


            //=== Added By Avishek Ghosh On 08-04-2016====//
            this.BindReportingTo();

            if (dt.Rows[0]["REPORTTOROLEID"].ToString() != "")
            {
                this.ddlReportToRole.SelectedValue = dt.Rows[0]["REPORTTOROLEID"].ToString().Trim();
            }
            this.BindReportingToUser(BSID);

            if (dt.Rows[0]["PARENT_CUST_ID"].ToString() != "")
            {
                ddlUnder.SelectedValue = dt.Rows[0]["PARENT_CUST_ID"].ToString().Trim();
            }

            string GRPID = "";
            var queryGrp = from ListItem item in ddlGroupType.Items where item.Selected select item;
            foreach (ListItem item in queryGrp)
            {
                GRPID += item.Value + ",";

            }
            GRPID = GRPID.Substring(0, GRPID.Length - 1);

            this.LoadCurrency(GRPID.Trim());
            if (dt.Rows[0]["CURRENCYID"].ToString() != "")
            {
                ddlcurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString().Trim();
            }
            LoadAccGroup();
            this.LoadLedger();

            if (Convert.ToString(dt.Rows[0]["CUSTOMERID"]).Trim() != Convert.ToString(dt.Rows[0]["LEDGER_REFERENCEID"]).Trim())
            {
                this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                rdbledger.SelectedValue = "1";
            }

            if (this.ddlAccGroup.SelectedValue == "0")
            {
                if (dt.Rows[0]["LEDGER_REFERENCEID"].ToString() != "")
                {
                    this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                }
            }
            if (dt.Rows[0]["ACCGROUPID"].ToString() != "")
            {
                this.ddlAccGroup.SelectedValue = dt.Rows[0]["ACCGROUPID"].ToString().Trim();
            }

            if (this.ddlAccGroup.SelectedValue == "0")
            {
                trledger.Style["display"] = "";
                rdbledger.SelectedValue = "1";

                trgroup.Style["display"] = "none";
            }
            else
            {
                trledger.Style["display"] = "none";
                rdbledger.SelectedValue = "0";
                trgroup.Style["display"] = "";
            }
            txtInactiveDate.Text = dt.Rows[0]["INACTIVEDATE"].ToString();

            txtPanNO.Text = dt.Rows[0]["PANCARDNO"].ToString();
            txtvatno.Text = dt.Rows[0]["VATNO"].ToString();
            txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
            txttinno.Text = dt.Rows[0]["TINNO"].ToString();
            txtContactPerson1.Text = dt.Rows[0]["CONTACTPERSON1"].ToString();
            txtContactPerson2.Text = dt.Rows[0]["CONTACTPERSON2"].ToString();
            txtEmailid1.Text = dt.Rows[0]["EMAILID1"].ToString();
            txtEmailid2.Text = dt.Rows[0]["EMAILID2"].ToString();
            txtMobileNo.Text = dt.Rows[0]["MOBILE1"].ToString();
            txtPhoneNo.Text = dt.Rows[0]["TELEPHONE1"].ToString();
            txtMobileNo1.Text = dt.Rows[0]["MOBILE2"].ToString();
            txtPhoneNo1.Text = dt.Rows[0]["TELEPHONE2"].ToString();
            txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            txtDeliveryaddress.Text = dt.Rows[0]["ALTERNATEADDRESS"].ToString();
            txtDeliveryPIN.Text = dt.Rows[0]["ALTERNATEPIN"].ToString();
            this.txtprintname.Text = dt.Rows[0]["PRINTNAME"].ToString();
            this.LoadShopName();
            ddlShop.SelectedValue = dt.Rows[0]["SHOPID"].ToString().Trim();

            /*Added By Sayan Dey On 01/01/2018*/
            this.LoadRetDiv();

            this.LoadRetailercategory(this.ddlCustomer1.SelectedValue);
            this.ViewandHideCategory();
            if (Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim() != "")
            {
                ddlretailcat.SelectedValue = Convert.ToString(dt.Rows[0]["RETAILER_CAT_ID"]).Trim();
            }
            else
            {
                ddlretailcat.SelectedValue = "0";
            }


            /*Added By Sayan Dey On 04/01/2018*/
            if (Convert.ToString(dt.Rows[0]["DOB"]).Trim() == "01/01/1900")
            {
                this.txtDOB.Text = "";
            }
            else
            {
                this.txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]).Trim();
            }

            if (Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim() == "01/01/1900")
            {
                this.txtAnvDate.Text = "";
            }
            else
            {
                this.txtAnvDate.Text = Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim();
            }

            txtamount.Text = Convert.ToDecimal(dt.Rows[0]["AMOUNT"]).ToString().Trim();
            txtamount.Focus();
            txtamount.Enabled = true;
            txtPIN.Text = dt.Rows[0]["PIN"].ToString();

            if (dt.Rows[0]["ISACTIVE"].ToString() == "Active")
            {
                chkActive.Checked = true;
                trinactivedate.Style["display"] = "none";

            }
            else
            {
                chkActive.Checked = false;
                trinactivedate.Style["display"] = "";

            }

            if (dt.Rows[0]["ISTRANSFERTOHO"].ToString() == "Y")
            {
                chktransfertoho.Checked = true;
            }
            else
            {
                chktransfertoho.Checked = false;
            }

            /*Added By Sayan Dey On 01/01/2018*/
            if (dt.Rows[0]["CUSTYPE_ID"].ToString().Trim() == "B9343E49-D86B-49EA-9ACA-4F4F7315EC96")
            {
                trRetDiv.Style["display"] = "";
                this.ddlRetDiv.SelectedValue = dt.Rows[0]["RETDIVID"].ToString().Trim();
            }
            else
            {
                trRetDiv.Style["display"] = "none";
                this.ddlRetDiv.SelectedValue = "0";
            }


            if (dt.Rows[0]["APPLICABLEGST"].ToString() == "Y")
            {
                chkgst.Checked = true;
                string GSTNO = dt.Rows[0]["GSTNO"].ToString();

                if (GSTNO != "")
                {

                    if (GSTNO.Substring(0, 2).Length == 2)
                    {
                        this.txtstatecode.Text = GSTNO.Substring(0, 2);
                    }
                    else
                    {
                        this.txtstatecode.Text = "";
                    }

                    if (GSTNO.Substring(2, 10).Length == 10)
                    {
                        this.txtgstpanno.Text = GSTNO.Substring(2, 10);
                    }
                    else
                    {
                        this.txtgstpanno.Text = "";
                    }


                    if (GSTNO.Substring(12, 3).Length == 3)
                    {
                        this.txtgstno.Text = GSTNO.Substring(12, 3);
                    }
                    else
                    {
                        this.txtgstno.Text = "";
                    }
                }
            }
            else
            {
                chkgst.Checked = false;

            }
            txtpercentage.Text = dt.Rows[0]["PERCENTAGE"].ToString();
            if (dt.Rows[0]["ADDSSMARGIN"].ToString() == "Y")
            {
                this.chkaddssmagine.Checked = true;
            }
            else
            {
                this.chkaddssmagine.Checked = false;
            }

            this.txtaddmarginpercentage.Text = dt.Rows[0]["ADDSSMARGINPERCENTAGE"].ToString().Trim();

            string Upload = dt.Rows[0]["FILENAME"].ToString();

            Hdn_Fld.Value = Convert.ToString(userid);
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            pnlDepotMapping.Style["display"] = "none";
            btnAddCity.Enabled = true;
            this.ddlcountry.Enabled = false;
            this.ddlcurrency.Enabled = false;
        }
        this.LoadPurchaseType();
        if (dtpurchasetype.Rows.Count > 0)
        {

            var myListTYPE = new List<string>(dtpurchasetype.Rows[0]["PURCHASETYPE"].ToString().Split(','));


            for (int counter = 0; counter < myListTYPE.Count; counter++)
            {
                for (int innercounter = 0; innercounter < lstpurchasetype.Items.Count; innercounter++)
                {
                    if (myListTYPE[counter] == lstpurchasetype.Items[innercounter].ToString().Trim())
                    {
                        lstpurchasetype.Items[innercounter].Selected = true;

                    }
                }
            }
        }
        divbtnCustomerSubmit.Style["display"] = "";
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTAG = (e.Row.FindControl("lblTAG") as Label);
            CheckBox chkSelect = (e.Row.FindControl("chkSelect") as CheckBox);

            if (lblTAG.Text.Trim() == "0")
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    chkSelect.Checked = false;
                }
            }
            else
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    chkSelect.Checked = true;
                }
            }

        }
    }

    #region LoadBrandType
    public void LoadBrandType()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlBrandType.Items.Clear();
            ddlBrandType.Items.Insert(0, new ListItem("Select Selling Brand", "-1"));
            ddlBrandType.DataSource = ClsCustomer.BindBrandType();
            ddlBrandType.DataTextField = "BRANDTYPE";
            ddlBrandType.DataValueField = "BRANDVALUE";
            ddlBrandType.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region ddlBrandType_SelectedIndexChanged
    protected void ddlBrandType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlBrandType.SelectedValue.Trim() == "1")
            {
                this.trBrandMap.Visible = true;
                this.trinvoice.Visible = true;
                if (Hdn_Fld.Value == "")
                {
                    this.LoadBrandGrid("NA", "NA", "A");
                }
                else
                {
                    this.LoadBrandGrid(Hdn_Fld.Value.Trim(), this.ddlDepot.SelectedValue.Trim(), "U");
                }
            }
            else
            {
                this.trBrandMap.Visible = false;
                this.trinvoice.Visible = false;
                this.gvBrandTypeMap.DataSource = null;
                this.gvBrandTypeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadBrand
    public void LoadBrandGrid(string CustomerID, string DepotID, string Mode)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dtBrand = new DataTable();
            dtBrand = ClsCustomer.BindBrand(CustomerID, DepotID, Mode);
            if (dtBrand.Rows.Count > 0)
            {
                this.gvBrandTypeMap.DataSource = dtBrand;
                this.gvBrandTypeMap.DataBind();
            }
            else
            {
                this.gvBrandTypeMap.DataSource = null;
                this.gvBrandTypeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Create DataTable Structure
    public void CreateBrandMappingDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("BRANDID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERTYPEID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERTYPENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTNAME", typeof(string)));
        HttpContext.Current.Session["BRNDMAPPING"] = dt;
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatable(DataTable dt)
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

    #region Create Core Brand DataTable Structure
    public void CreateCoreBrandDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("BRANDID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDNAME", typeof(string)));

        dt.Rows.Add("1AC1960B-5C21-4C67-93D4-D38D1B3FAEB2", "HEAVENS GARDEN");
        dt.Rows.Add("12E3F880-C465-4C0E-80B8-42E8DD05E51D", "SECRET TEMPTATION");
        dt.Rows.Add("1865C411-3F93-4F03-9FE5-154C93A4CB7C", "WILD STONE");
        ViewState["COREBRAND"] = dt;
    }
    #endregion
}