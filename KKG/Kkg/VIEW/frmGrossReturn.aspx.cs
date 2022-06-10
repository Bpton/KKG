#region Developer Info

/*
 Developer Name : Pritam Basu
 * Start Date   : 11/12/2021

*/

#endregion

#region Namespace
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using WorkFlow;
using Utility;
#endregion

public class Batchdetails
{
    public string BATCHNO { get; set; }
    public string STOCKQTY { get; set; }
    public string ID { get; set; }
    public string MRP { get; set; }
    public string ASSESMENTPERCENTAGE { get; set; }
    public string MFDATE { get; set; }
    public string EXPRDATE { get; set; }
}

public partial class VIEW_frmGrossReturn : System.Web.UI.Page
{
    string menuID = string.Empty;
    
    string Checker = string.Empty;
   
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region QueryString
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            menuID = Request.QueryString["MENUID"].ToString().Trim();
            if (Checker == "TRUE")
            {

                btnaddhide.Style["display"] = "none";
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
            }
            else
            {

                btnaddhide.Style["display"] = "";
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
            }
            #endregion
            
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.txtFromDate.Style.Add("color", "black !important");
            this.txtToDate.Style.Add("color", "black !important");
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            menuID = Request.QueryString["MENUID"].ToString().Trim();
            ViewState["menuID"] = menuID;
            

            /*Calender Control Date Range*/
            CalendarExtenderInvoiceDate.EndDate = DateTime.Now;
            CalendarExtenderLRGRDate.EndDate = DateTime.Now;
            CalendarExtenderGetPassDate.EndDate = DateTime.Now;
            CalendarExtender111.EndDate = DateTime.Now;
            /****************************/
            this.LoadBS();
            this.LoadMotherDepot();
            this.LoadGrossReturn();
            this.LoadProductWisePacksize("0");
            this.LoadCategory();
            this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());
            this.LoadTermsConditions();
        }

        foreach (ListItem item in ddlDepot.Items)
        {
            if (item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "Blue");
            }
        }

        

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 300, '100%' , 30 ,false); </script>", false);
        
    }
    #endregion

    #region LoadTax
    public void LoadTax()
    {
        try
        {
            if (hdnDespatchID.Value == "")
            {
                ClsGrossReturn clsInvc = new ClsGrossReturn();
                DataTable dt = new DataTable();
                string flag = clsInvc.BindRegion(this.ddlDistributor.SelectedValue.Trim(), ddlDepot.SelectedValue.Trim());

                if (string.IsNullOrEmpty(flag))
                {

                    dt = clsInvc.BindTax(ViewState["menuID"].ToString().Trim(), "1", Session["DEPOTID"].ToString().Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                    Session["GrossReturnCST"] = dt;
                    
                }
                else
                {
                    dt = clsInvc.BindTax(ViewState["menuID"].ToString().Trim(), "0", Session["DEPOTID"].ToString().Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                    Session["GrossReturnCST"] = dt;
                }

                if (dt.Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["GrossReturnCST"];
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
                ClsGrossReturn clsInvc = new ClsGrossReturn();
                DataSet ds = new DataSet();
                string ReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
                ds = clsInvc.EditGrossReturnDetails(ReturnID);
                Session["GrossReturnCST"] = ds.Tables[5];
                if (ds.Tables[5].Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["GrossReturnCST"];
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

    #region LoadBUSINESSSEGMENT
    public void LoadBUSINESSSEGMENT(string bstype)
    {
        ClsSaleorderFG clssaleorder = new ClsSaleorderFG();
        string bsid = string.Empty;
        bsid = clssaleorder.BindBUSINESSSEGMENT(bstype);
        hdn_bsid.Value = bsid;
        hdn_bsname.Value = bstype;
    }
    #endregion

    #region ddlBS_SelectedIndexChanged
    public void ddlBS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlBS.SelectedValue != "0")
            {
                this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());                
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlgroup.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlgroup_SelectedIndexChanged
    public void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlgroup.SelectedValue != "0")
            {
                this.BindCustomer();
                
                if (this.ddlDistributor.SelectedValue != "0")
                {
                    this.LoadProduct();
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlDistributor.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindCustomer
    protected void BindCustomer()
    {
        if (ddlgroup.SelectedValue != "0")
        {
            DataTable dtCustomer = new DataTable();
            ClsGrossReturn clssaleorder = new ClsGrossReturn();
            dtCustomer = clssaleorder.BindCustomer(this.ddlBS.SelectedValue.Trim(), ddlgroup.SelectedValue, Session["DEPOTID"].ToString().Trim());
            if (this.hdnDespatchID.Value == "")
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    if (dtCustomer.Rows.Count > 1)
                    {
                        this.ddlDistributor.Items.Clear();
                        this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                        this.ddlDistributor.AppendDataBoundItems = true;
                        this.ddlDistributor.DataSource = dtCustomer;
                        this.ddlDistributor.DataValueField = "CUSTOMERID";
                        this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                        this.ddlDistributor.DataBind();
                    }
                    else if (dtCustomer.Rows.Count == 1)
                    {
                        this.ddlDistributor.Items.Clear();
                        this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                        this.ddlDistributor.DataSource = dtCustomer;
                        this.ddlDistributor.DataValueField = "CUSTOMERID";
                        this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                        this.ddlDistributor.DataBind();
                        this.ddlDistributor.SelectedValue = Convert.ToString(dtCustomer.Rows[0]["CUSTOMERID"]);
                        
                        /* ADDITIONAL SS MARGIN */
                        this.CreateStructure();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlCategory.ClientID + "').focus(); ", true);
                        
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
                ClsGrossReturn clsInvc = new ClsGrossReturn();
                string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
                DataSet ds = new DataSet();
                ds = clsInvc.EditGrossReturnDetails(saleInvoiceID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.ddlDistributor.Items.Clear();
                    this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    this.ddlDistributor.DataSource = dtCustomer;
                    this.ddlDistributor.DataValueField = "CUSTOMERID";
                    this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                    this.ddlDistributor.DataBind();
                    this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
                    
                }
            }
        }
        else
        {
            this.ddlDistributor.Items.Clear();
            this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
            this.ddlDistributor.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region LoadPrincipleGroup
    public void LoadPrincipleGroup(string bstype)
    {
        ClsSaleorderFG clssaleorder = new ClsSaleorderFG();
        DataTable dtGroup = new DataTable();
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        if (Checker == "TRUE")
        {
            dtGroup = clssaleorder.BindGroup(this.ddlBS.SelectedValue.ToString());
        }
        else
        {
            dtGroup = clssaleorder.BindGroup(this.ddlBS.SelectedValue.ToString());
        }

        
        if (dtGroup.Rows.Count > 0)
        {
            if (dtGroup.Rows.Count > 1)
            {
                this.ddlgroup.Items.Clear();
                this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
                this.ddlgroup.AppendDataBoundItems = true;
                this.ddlgroup.DataSource = dtGroup;
                this.ddlgroup.DataValueField = "DIS_CATID";
                this.ddlgroup.DataTextField = "DIS_CATNAME";
                this.ddlgroup.DataBind();
            }
            else if (dtGroup.Rows.Count == 1)
            {
                this.ddlgroup.Items.Clear();
                this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
                this.ddlgroup.DataSource = dtGroup;
                this.ddlgroup.DataValueField = "DIS_CATID";
                this.ddlgroup.DataTextField = "DIS_CATNAME";
                this.ddlgroup.DataBind();
                this.ddlgroup.SelectedValue = Convert.ToString(dtGroup.Rows[0]["DIS_CATID"]);
            }


            if (this.ddlgroup.SelectedValue != "0")
            {
                this.BindCustomer();
                if (this.ddlDistributor.SelectedValue != "0")
                {
                    this.LoadProduct();
                    
                }
            }
        }
        else
        {
            this.ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
            this.ddlgroup.AppendDataBoundItems = true;
        }
        
        
        
    }
    #endregion
    
    #region LoadMotherDepot
    public void LoadMotherDepot()
    {
        try
        {
            ClsStockTransfer clsstocktransfer = new ClsStockTransfer();
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DataTable dtDepot = new DataTable ();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "FALSE")
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

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClsStockReport clsofflineinvoice = new ClsStockReport();
        string offline = clsofflineinvoice.OfflineNewRecord(Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim());
        string tag = clsofflineinvoice.BindAppMasterStatus();
        loadadd();
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

    #region Populate Transporter
    protected void BindTransporter(string DepotID)
    {
        clsDespatchStock clsDespatchStck = new clsDespatchStock();
        ClsGrossReturn clsInvc = new ClsGrossReturn();
        DataTable dtTransporter = clsInvc.BindDepot_Transporter(DepotID, Convert.ToString(hdnDespatchID.Value).Trim());


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
        if (ddlProduct.SelectedValue != "0")
        {
           
                this.LoadProductWisePacksize(this.ddlProduct.SelectedValue.ToString());
                this.ProductDetails();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlPacksize.ClientID + "').focus(); ", true);
           
        }
    }
    #endregion

    #region ProductDetails
    protected void ProductDetails()
    {
        ClsGrossReturn clsInvoice = new ClsGrossReturn();
        DataTable dtRate = new DataTable();
        string YMD = string.Empty;
        decimal MRP = 0;
        MRP = clsInvoice.MRP(this.ddlProduct.SelectedValue.Trim());
        this.txtMRP.Text = Convert.ToString(MRP);

        YMD = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());

        if (this.txtMRP.Text.Trim() == "")
        {
            MRP = 0;
        }
        dtRate = clsInvoice.GetBaseCostPrice(   this.ddlDistributor.SelectedValue.Trim(), 
                                                this.ddlProduct.SelectedValue.Trim(), YMD,
                                                MRP, 
                                                this.ddlDepot.SelectedValue.Trim(),
                                                Convert.ToString(ViewState["menuID"]).Trim(),
                                                this.ddlBS.SelectedValue.Trim(),
                                                this.ddlgroup.SelectedValue.Trim());
        if (dtRate.Rows.Count > 0)
        {
            this.txtBaseCost.Text = dtRate.Rows[0]["BASECOSTPRICE"].ToString();
        }
        else
        {
            this.txtBaseCost.Text = "0.00";
        }

    }
    #endregion

    #region rdbTax_CheckedChanged
    protected void rdbTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("GROSSRETURNDETAILS");
        Session.Remove("dtGrossReturnTaxCount");
        Session.Remove("GROSSRETTAXCOMPONENTDETAILS");
        
        
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        
        this.txtTotMRP.Text = "0.00";
        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
        this.txtTotDisc.Text = "0.00";

        this.ddlgroup.Enabled = true;
        this.ddlDistributor.Enabled = true;
        this.ddlDistributor.SelectedValue = "0";
        this.ddlProduct.Items.Clear();
    }
    #endregion

    #region rdbNoTax_CheckedChanged
    protected void rdbNoTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("GROSSRETURNDETAILS");
        Session.Remove("dtGrossReturnTaxCount");
        Session.Remove("GROSSRETTAXCOMPONENTDETAILS");
        
        
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
       
        this.txtTotMRP.Text = "0.00";
        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
        this.txtTotDisc.Text = "0.00";
        this.ddlgroup.Enabled = true;
        this.ddlDistributor.Enabled = true;
        this.ddlDistributor.SelectedValue = "0";
        this.ddlProduct.Items.Clear();
    }
    #endregion

    #region LoadProductWisePacksize
    public void LoadProductWisePacksize(string ProductID)
    {
        try
        {
            ClsGrossReturn clsInvoice = new ClsGrossReturn();
            DataTable dtPackSize = new DataTable();
                //HttpContext.Current.Cache["GrossPackSize"] as DataTable;
            dtPackSize = clsInvoice.BindPackSize(ProductID);
            //if (dtPackSize == null)
            //{
            //   
            //    if (dtPackSize.Rows.Count > 0)
            //    {
            //        HttpContext.Current.Cache["GrossPackSize"] = dtPackSize;
            //        HttpContext.Current.Cache.Insert("GrossPackSize", dtPackSize, null, DateTime.Now.AddDays(90), System.Web.Caching.Cache.NoSlidingExpiration);
            //    }
            //}
            //if (dtPackSize != null)
            //{
            if (dtPackSize.Rows.Count > 0)
                {
                    this.ddlPacksize.DataSource = dtPackSize;
                    this.ddlPacksize.DataTextField = "PSNAME";
                    this.ddlPacksize.DataValueField = "PSID";
                    this.ddlPacksize.DataBind();

                }
                else
                {
                    this.ddlPacksize.Items.Clear();
                    this.ddlPacksize.Items.Add(new ListItem("SELECT PACKSIZE", "0"));
                    this.ddlPacksize.AppendDataBoundItems = true;
                }
            //}
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region Add Records
    protected void btnADDGrid_Click(object sender, EventArgs e)
    {
        #region Add Mode

        if (hdnDespatchID.Value == "")
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            DataTable dtInvoice = (DataTable)Session["GROSSRETURNDETAILS"];
            DataTable dtQtyScheme = new DataTable();
            DataTable dtFreeProductDetails = new DataTable();
            DataTable dtQtyInPCS = new DataTable();
            string Packsize = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";
            string Brand = string.Empty;
            string BatchNo = string.Empty;
            
            decimal deliveredqty = Convert.ToDecimal(this.txtDeliveredQtyPCS.Text.Trim());
            decimal orderedqty = deliveredqty;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            decimal TotalMRP = 0;
            string ymd = string.Empty;
            decimal AddSSPercentage = 0;
            string PrimaryPriceScheme = string.Empty;
            if (deliveredqty > 0)
            {
                if (this.txtBatchNo.Text.Trim() == "")
                {
                    BatchNo = "";
                }
                else
                {
                    BatchNo = this.txtBatchNo.Text.Trim();
                }
                int numberOfRecords = dtInvoice.Select("PRODUCTID = '" + this.ddlProduct.SelectedValue.Trim() + "' AND BATCHNO = '" + BatchNo + "'").Length;

                #region Bill Issue

                if (numberOfRecords > 0)
                {
                    MessageBox1.ShowInfo("<b>Record already Exists..</b>");
                    this.ResetControls();
                    return;
                }
                else
                {
                    /*DataTable dtbatchcheck = new DataTable();
                    dtbatchcheck = clsInvc.MasterBatchDetailsCheck(this.ddlProduct.SelectedValue.Trim(), txtBatchNo.Text.Trim(), Convert.ToDecimal(txtMRP.Text.Trim()), txtMFDate.Text.Trim(), txtEXPRDate.Text.Trim(), Session["FinYear"].ToString().Trim());
                    string mastermfgdate = string.Empty;
                    string masterexpdate = string.Empty;
                    if (!string.IsNullOrEmpty(dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim()))
                    {
                        mastermfgdate = dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim();
                        masterexpdate = dtbatchcheck.Rows[0]["EXPRDATE"].ToString().Trim();
                        MessageBox1.ShowInfo("<b>BatchNo already exists into system with this <font color='green'>MFG Date: " + mastermfgdate + " & EXP Date: " + masterexpdate + "</font>, please enter the same as specified</b>!", 60, 700);
                        return;
                    }*/

                    ymd = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());
                    /*string PrimaryPriceScheme = clsInvc.GetPrimaryPriceScheme(this.ddlProduct.SelectedValue.Trim(), deliveredqty, Packsize, this.ddlDistributor.SelectedValue.Trim(),
                                                                                "this.ddlBrand.SelectedValue.Trim()", ymd,
                                                                                Convert.ToString(ViewState["BSID"]).Trim(),
                                                                                this.ddlgroup.SelectedValue.Trim());*/
                    Brand = clsInvc.getBrand(this.ddlProduct.SelectedValue.Trim());
                    AddSSPercentage = 0;
                    
                    #region Primary Price Scheme

                    this.FillPrimaryGridView(   PrimaryPriceScheme.Trim(), 
                                                AddSSPercentage, Brand.Trim(), 
                                                deliveredqty, Packsize.Trim(), 
                                                this.ddlPacksize.SelectedValue.Trim(),
                                                Convert.ToString(this.ddlPacksize.SelectedItem).Trim(), 
                                                this.txtOrderDate.Text.Trim(),
                                                this.ddlProduct.SelectedValue.Trim(), 
                                                Convert.ToString(this.ddlProduct.SelectedItem).Trim(),
                                                this.txtBatchNo.Text.Trim(), this.txtBatchNo.Text.Trim(),
                                                Convert.ToDecimal(this.txtBaseCost.Text.Trim()), 
                                                Convert.ToDecimal(this.txtMRP.Text.Trim()),
                                                0, TotalAmount, TotalTax, GrossTotal, 
                                                GrossFinal, TotalMRP);

                    #endregion


                }

                #endregion

            }
            else
            {
                MessageBox1.ShowInfo("<b>Returned Qty should be greater than 0</b>", 80, 550);

            }
        }
        #endregion

        #region Edit Mode
        else
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            DataTable dtInvoice = (DataTable)Session["GROSSRETURNDETAILS"];
            DataTable dtQtyScheme = new DataTable();
            DataTable dtFreeProductDetails = new DataTable();
            DataTable dtQtyInPCS = new DataTable();
            string Packsize = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";
            string Brand = string.Empty;
            string PrimaryPriceScheme = string.Empty;
            string BatchNo = string.Empty;
            decimal deliveredqty =  Convert.ToDecimal(this.txtDeliveredQtyPCS.Text.Trim());
            decimal orderedqty = deliveredqty;


            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            decimal TotalMRP = 0;
            string ymd = string.Empty;
            decimal AddSSPercentage = 0;

            if (deliveredqty > 0)
            {
                if (this.txtBatchNo.Text.Trim() == "")
                {
                    BatchNo = "";
                }
                else
                {
                    BatchNo = this.txtBatchNo.Text.Trim();
                }

                int numberOfRecords = dtInvoice.Select(" PRODUCTID = '" + this.ddlProduct.SelectedValue + "' AND BATCHNO = '" + BatchNo + "'").Length;


                #region Bill Issue

                if (numberOfRecords > 0)
                {
                    MessageBox1.ShowInfo("<b>Record already Exists..</b>");

                    this.ResetControls();
                    return;
                }
                else
                {
                    /*DataTable dtbatchcheck = new DataTable();
                    dtbatchcheck = clsInvc.MasterBatchDetailsCheck(this.ddlProduct.SelectedValue.Trim(), txtBatchNo.Text.Trim(), Convert.ToDecimal(txtMRP.Text.Trim()), txtMFDate.Text.Trim(), txtEXPRDate.Text.Trim(), Session["FinYear"].ToString().Trim());
                    string mastermfgdate = string.Empty;
                    string masterexpdate = string.Empty;
                    if (!string.IsNullOrEmpty(dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim()))
                    {
                        mastermfgdate = dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim();
                        masterexpdate = dtbatchcheck.Rows[0]["EXPRDATE"].ToString().Trim();
                        MessageBox1.ShowInfo("<b>BatchNo already exists into system with this <font color='green'>MFG Date: " + mastermfgdate + " & EXP Date: " + masterexpdate + "</font>, please enter the same as specified</b>!", 60, 700);
                        return;
                    }*/
                    ymd = Conver_To_YMD(this.txtInvoiceDate.Text.Trim());
                    /*PrimaryPriceScheme = clsInvc.GetPrimaryPriceScheme(this.ddlProduct.SelectedValue.Trim(),
                                                                        deliveredqty, Packsize, this.ddlDistributor.SelectedValue.Trim(),
                                                                        "this.ddlBrand.SelectedValue.Trim()", ymd, ViewState["BSID"].ToString().Trim(),
                                                                        this.ddlgroup.SelectedValue.Trim());*/
                    Brand = clsInvc.getBrand(this.ddlProduct.SelectedValue.Trim());
                    AddSSPercentage = 0;
                    
                    /*ViewState["PrimaryPriceScheme"] = PrimaryPriceScheme;
                    ViewState["SSMarginInv"] = AddSSValue;
                    ViewState["SSMarginPercentageInv"] = AddSSPercentage;
                    ViewState["Brand"] = Brand;
                    ViewState["Packsize"] = Packsize;
                    ViewState["deliveredqty"] = deliveredqty;*/

                    #region Primary Price Scheme

                    this.FillPrimaryGridView(PrimaryPriceScheme.Trim(),
                                                AddSSPercentage, Brand.Trim(),
                                                deliveredqty, Packsize.Trim(),
                                                this.ddlPacksize.SelectedValue.Trim(),
                                                Convert.ToString(this.ddlPacksize.SelectedItem).Trim(),
                                                this.txtOrderDate.Text.Trim(),
                                                this.ddlProduct.SelectedValue.Trim(),
                                                Convert.ToString(this.ddlProduct.SelectedItem).Trim(),
                                                this.txtBatchNo.Text.Trim(), this.txtBatchNo.Text.Trim(),
                                                Convert.ToDecimal(this.txtBaseCost.Text.Trim()),
                                                Convert.ToDecimal(this.txtMRP.Text.Trim()),
                                                0, TotalAmount, TotalTax, GrossTotal,
                                                GrossFinal, TotalMRP);
                    #endregion
                }
                #endregion

            }
            else
            {
                MessageBox1.ShowInfo("<b>Returned Qty should be greater than 0</b>", 80, 550);

            }
        }
        #endregion

        #region Grid Calculation
        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.GridAddDespatchStyle();
            this.BillingGridCalculation();
        }
       
        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
        }
        #endregion

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlProduct.ClientID + "').focus(); ", true);
    }
    #endregion

    #region FillPrimaryGridView
    protected void FillPrimaryGridView( string PRIMARYPRICESCHEME, decimal ADDSSPERCENTAGE, string BRAND, decimal deliveredqty,
                                        string PACKSIZEPCS, string DDLPACKSIZEID, string DDLPACKSIZENAME,  
                                        string ORDERDATE,string PRODUCTID,string PRODUCTNAME,string BATCHID,string BATCHNO,
                                        decimal BCP, decimal MRP, decimal ASSESMENTPERCENTAGE, decimal TotalAmount, decimal TotalTax,
                                        decimal GrossTotal, decimal GrossFinal, decimal TotalMRP) 
                                        
    {
        ClsGrossReturn clsInvc = new ClsGrossReturn();
        clsSaleInvoice Inv = new clsSaleInvoice();
        DataTable dtInvoice = (DataTable)Session["GROSSRETURNDETAILS"];
        DataSet dsNtWghtCaseQty = new DataSet();
        decimal TotAmt = 0;
        decimal AfterRebateAmount = 0;
        string Weight = string.Empty;
        decimal QtyInCase = 0;
        decimal AddSSPercentage = ADDSSPERCENTAGE;
        decimal AddSSValue = 0;
        string BatchNo = string.Empty;
        string MFDATE = string.Empty;
        string EXPDATE = string.Empty;
        string TAXID = string.Empty;
        decimal ProductWiseTax = 0;
        int flag = 0;
        string HSNCODE = string.Empty;

        if (this.txtBatchNo.Text == "")
        {
            BatchNo = "";
        }
        else
        {
            BatchNo = this.txtBatchNo.Text.Trim();
        }

        if (this.txtMFDate.Text == "")
        {
            MFDATE = "01/09/2017";
        }
        else
        {
            MFDATE = this.txtMFDate.Text.Trim();
        }

        if (this.txtEXPRDate.Text == "")
        {
            EXPDATE = "30/08/2020";
        }
        else
        {
            EXPDATE = this.txtEXPRDate.Text.Trim();
        }
        

        #region IF Primary Price Scheme Not Exists
        
        decimal Amount = clsInvc.CalculateAmountInPcs(PRODUCTID, PACKSIZEPCS, deliveredqty, BCP, 0, 0);
        TotAmt += Amount;

        /*====== ADD SS MARGIN LOGIC =================*/
        if (AddSSPercentage > 0)
        {
            AfterRebateAmount = (Amount - (((Amount * AddSSPercentage) / 100)));
        }
        else
        {
            AfterRebateAmount = Amount;
        }
        /*=========================================*/


        decimal TotAssesment = clsInvc.CalculateAmountInPcs(PRODUCTID, PACKSIZEPCS,deliveredqty, MRP, 0, 0)* ASSESMENTPERCENTAGE / 100;

        decimal TotMRP = clsInvc.CalculateTotalMRPInPcs(PRODUCTID, PACKSIZEPCS,deliveredqty, MRP, 0, 0);

        dsNtWghtCaseQty = clsInvc.NetWeightCaseQty(PRODUCTID, PACKSIZEPCS, DDLPACKSIZEID, deliveredqty);
        if (dsNtWghtCaseQty.Tables[0].Rows.Count > 0)
        {
            Weight = Convert.ToString(dsNtWghtCaseQty.Tables[0].Rows[0]["NETWEIGHT"]).Trim();
        }
        if (dsNtWghtCaseQty.Tables[1].Rows.Count > 0)
        {
            QtyInCase = Convert.ToDecimal(dsNtWghtCaseQty.Tables[1].Rows[0]["CASEQTY"].ToString().Trim());
        }

        DataRow dr = dtInvoice.NewRow();
        dr["GUID"] = Guid.NewGuid();
        dr["SALEORDERID"] = "0";
        dr["SALEORDERDATE"] = Convert.ToString(ORDERDATE);
        dr["PRODUCTID"] = Convert.ToString(PRODUCTID);
        dr["PRODUCTNAME"] = Convert.ToString(PRODUCTNAME.Substring(0, PRODUCTNAME.IndexOf("~"))).Trim();
        dr["PACKINGSIZEID"] = Convert.ToString(DDLPACKSIZEID);
        dr["PACKINGSIZENAME"] = Convert.ToString(DDLPACKSIZENAME).Trim();
        dr["MRP"] = Convert.ToString(MRP);
        dr["BCP"] = Convert.ToString(BCP);

        if (DDLPACKSIZEID != "B9F29D12-DE94-40F1-A668-C79BF1BF4425")
        {
            if (QtyInCase.ToString().IndexOf(".") > 0)
            {
                dr["QTY"] = Convert.ToInt16(QtyInCase.ToString().Substring(0, QtyInCase.ToString().IndexOf("."))).ToString();
                dr["RETURNQTY"] = Convert.ToInt16(QtyInCase.ToString().Substring(QtyInCase.ToString().IndexOf(".") + 1, 3)).ToString();
            }
            else
            {
                dr["QTY"] = Convert.ToString(QtyInCase);
                dr["RETURNQTY"] = Convert.ToString("0");
            }
        }
        else if (DDLPACKSIZEID == "B9F29D12-DE94-40F1-A668-C79BF1BF4425")
        {
            dr["QTY"] = Convert.ToString("0");
            dr["RETURNQTY"] = Convert.ToString(deliveredqty);
        }

        dr["BATCHNO"] = BatchNo;
        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ASSESMENTPERCENTAGE);
        dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesment));
        dr["PRIMARYPRICESCHEMEID"] = "";
        dr["PERCENTAGE"] = Convert.ToString("0.00");
        dr["VALUE"] = Convert.ToString(String.Format("{0:0.00}", "0.00"));
        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", Amount));
        dr["TOTMRP"] = Convert.ToString(String.Format("{0:0.00}", TotMRP));
        if (!string.IsNullOrEmpty(Weight))
        {
            dr["WEIGHT"] = Weight;
        }
        else
        {
            dr["WEIGHT"] = "0";
        }
        dr["MFDATE"] = MFDATE;
        dr["EXPRDATE"] = EXPDATE;
        decimal BillTaxAmt = 0;
        #region Loop For Adding Itemwise Tax Component
        DataTable dtTaxCountDataAddition = (DataTable)Session["dtGrossReturnTaxCount"];
        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
        {
            switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
            {
                case "1":
                    if (BRAND.Trim() == "6B869907-14E0-44F6-9BA5-57F4DA726686")
                    {
                        ProductWiseTax = 0;
                        dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                        dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", AfterRebateAmount * ProductWiseTax / 100));
                        BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                    }
                    else
                    {
                        TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                        ProductWiseTax = Inv.GetHSNTax(TAXID, Convert.ToString(this.ddlProduct.SelectedValue).Trim(), this.txtInvoiceDate.Text.Trim());
                        dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                        double TaxValue = 0;
                        TaxValue = Convert.ToDouble(Math.Floor(((AfterRebateAmount * ProductWiseTax) / 100) * 100) / 100);
                        dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                        BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                    }
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
                
            

            CreateTaxDatatable( "0",
                                Convert.ToString(PRODUCTID).Trim(),
                                Convert.ToString(BatchNo).Trim(),
                                dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                Convert.ToString(ProductWiseTax),
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                TAXID, Convert.ToString(MRP)
                                );
            
        }
        #endregion
        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Amount));

        dtInvoice.Rows.Add(dr);
        dtInvoice.AcceptChanges();
        
        #endregion

        

        #region Amount Calculation
        TotalMRP = CalculateTotalMRP(dtInvoice);
        TotalAmount = CalculateGrossTotal(dtInvoice);

        if (AddSSPercentage == 0)
        {
            TotalTax = CalculateTaxTotal(dtInvoice);
            GrossTotal = TotalAmount + TotalTax;
            this.txtSSMarginAmt.Text = Convert.ToString(String.Format("{0:0.00}", 0));
        }
        else
        {
            if (this.txtSSMarginAmt.Text.Trim() == "")
            {
                this.txtSSMarginAmt.Text = "0.00";
            }
            AddSSValue = (TotalAmount * AddSSPercentage) / 100;
            this.txtSSMarginAmt.Text = Convert.ToString(String.Format("{0:0.00}", AddSSValue));
            TotalTax = CalculateTaxTotal(dtInvoice);
            GrossTotal = (TotalAmount - Convert.ToDecimal(this.txtSSMarginAmt.Text.Trim())) + TotalTax;
        }
        

        this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
        this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

       
        GrossFinal = 0;
        

        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Ceiling(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
        #endregion

        #region Grid Calculation
        if (dtInvoice.Rows.Count > 0)
        {
            
            this.grdAddDespatch.DataSource = dtInvoice;
            this.grdAddDespatch.DataBind();
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtGrossReturnTaxCount"];
            this.GridAddDespatchStyle();
            this.BillingGridCalculation();
        }
        else
        {
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
        }


        if (this.grdAddDespatch.Rows.Count > 0)
        {
            this.TotalPcsCalculation();
            this.ddlgroup.Enabled = false;
            this.ddlDistributor.Enabled = false;
            this.ddlBS.Enabled = false;
        }
        else
        {
            this.ddlgroup.Enabled = true;
            this.ddlDistributor.Enabled = true;
            this.ddlBS.Enabled = true;
        }

        
        #endregion

        this.ResetControls();

    }

    #endregion

    #region txtMFDate_TextChanged
    protected void txtMFDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlProduct.SelectedValue != "0")
            {
                ClsGrossReturn clsInvc = new ClsGrossReturn();

                if (!string.IsNullOrEmpty(this.txtMFDate.Text.Trim()))
                {
                    string PRODUCT = this.ddlProduct.SelectedValue;
                    string expdate = string.Empty;
                    expdate = clsInvc.GetProductExpirydate(PRODUCT, txtMFDate.Text.ToString());
                    txtEXPRDate.Text = expdate;
                }
            }
            else
            {
                this.txtMFDate.Text = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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
            decimal TotalDisc = 0;
            decimal AddSSMarginPercentage = 0;
            decimal AddSSMarginValue = 0;
            int Taxflag = 0;
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            this.hdndtDespatchDelete.Value = gvr.Cells[1].Text.Trim();
            this.hdndtPOIDDelete.Value = gvr.Cells[2].Text.Trim();
            this.hdndtPRODUCTIDDelete.Value = gvr.Cells[4].Text.Trim();
            this.hdndtBATCHDelete.Value = gvr.Cells[12].Text.Trim();
            string invoiceGUID = Convert.ToString(hdndtDespatchDelete.Value);
            string invoiceSALEORDERID = Convert.ToString(hdndtPOIDDelete.Value);
            string invoicePRODUCTID = Convert.ToString(hdndtPRODUCTIDDelete.Value);
            string invoiceBATCH = Convert.ToString(hdndtBATCHDelete.Value);

            DataTable dtdeleteInvoicerecord = new DataTable();
            dtdeleteInvoicerecord = (DataTable)Session["GROSSRETURNDETAILS"];
            DataRow[] drr = dtdeleteInvoicerecord.Select("GUID='" + invoiceGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteInvoicerecord.AcceptChanges();
            }
            HttpContext.Current.Session["GROSSRETURNDETAILS"] = dtdeleteInvoicerecord;
            grdAddDespatch.DataSource = dtdeleteInvoicerecord;
            grdAddDespatch.DataBind();

            //================Itemwise Tax Deletion ==============================//

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtGrossReturnTaxCount"];

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
            dtdeleteInvoiceItemTax = (DataTable)Session["GROSSRETTAXCOMPONENTDETAILS"];

            DataRow[] drrTax = dtdeleteInvoiceItemTax.Select("PRODUCTID = '" + invoicePRODUCTID + "' AND BATCHNO = '" + invoiceBATCH + "'");
            for (int i = 0; i < drrTax.Length; i++)
            {
                drrTax[i].Delete();
                dtdeleteInvoiceItemTax.AcceptChanges();
            }

            HttpContext.Current.Session["GROSSRETTAXCOMPONENTDETAILS"] = dtdeleteInvoiceItemTax;

            
            
            
            TotalMRP = 0;
            TotalAmount = CalculateGrossTotal(dtdeleteInvoicerecord);

            if (AddSSMarginPercentage == 0)
            {
                TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord);
                GrossTotal = TotalAmount + TotalTax;
            }
            else
            {
                if (this.txtSSMarginAmt.Text.Trim() == "")
                {
                    this.txtSSMarginAmt.Text = "0";
                }
                AddSSMarginValue = (TotalAmount * AddSSMarginPercentage) / 100;
                this.txtSSMarginAmt.Text = Convert.ToString(String.Format("{0:0.00}", AddSSMarginValue));
                TotalTax = CalculateTaxTotal(dtdeleteInvoicerecord);
                GrossTotal = TotalAmount - (Convert.ToDecimal(this.txtSSMarginAmt.Text.Trim())) + TotalTax;
            }
            


            this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
            this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
            this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            //==================================================================//

            // ================ Gross Tax Deletion ============================== //

            

            GrossFinal = 0; 
            
            #region Update Round off Calculation by Avishek Ghosh

            this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
            this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Ceiling(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
            this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));

            #endregion

            //====================================================================//

            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridAddDespatchStyle();
                this.BillingGridCalculation();
                this.TotalPcsCalculation();
                this.ddlgroup.Enabled = false;
                this.ddlDistributor.Enabled = false;
                this.ddlBS.Enabled = false;
            }
            else
            {
                this.ddlgroup.Enabled = true;
                this.ddlDistributor.Enabled = true;
                this.ddlBS.Enabled = true;
            }
            if (grdAddDespatch.Rows.Count == 0 )
            {
                this.txtTotPCS.Text = "0";
            }
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
        ClsGrossReturn clsInvc = new ClsGrossReturn();
        DataSet ds = new DataSet();
        if (hdnDespatchID.Value != "")
        {
            ds = clsInvc.EditGrossReturnDetails(hdnDespatchID.Value);
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

    #region ConvertDatatableToXMLOrderInvoice
    public string ConvertDatatableToXMLOrderInvoice(DataTable dt)
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

    #region Delete Gross Return
    protected void DeleteRecordInvoice(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();

            if (clsInvc.GetFinancestatus(e.Record["SALERETURNID"].ToString().Trim()) == "1")
            {
                e.Record["Error"] = "Finance posting already done,not allow to delete.";
                return;
            }
            if (clsInvc.Getstatus(e.Record["SALERETURNID"].ToString().Trim()) == "1")
            {
                e.Record["Error"] = "Day End already done,not allow to delete.";
                return;
            }

            int flag = 0;
            flag = clsInvc.GrossReturnDelete(e.Record["SALERETURNID"].ToString().Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.LoadGrossReturn();
                e.Record["Error"] = "Record Deleted Successfully. ";
            }
            else
            {
                e.Record["Error"] = "Error On Deleting. ";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Save Gross Return
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockReport clsofflineinvoice = new ClsStockReport();
            string offline = clsofflineinvoice.OfflineNewRecord(Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim());
            string tag = clsofflineinvoice.BindAppMasterStatus();
            loadsave();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Close
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.trAutoInvoiceNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "";
        this.ClearControls();
        this.ResetControls();
        this.ResetSession();
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
       
        this.grdTax.ClearPreviousDataSource();
        this.grdTax.DataSource = null;
        this.grdTax.DataBind();
        this.grdTerms.ClearPreviousDataSource();
        this.grdTerms.DataSource = null;
        this.grdTerms.DataBind();
        this.hdnDespatchID.Value = "";
        this.ddlCategory.SelectedValue = "0";
        this.LoadGrossReturn();

        #region Enable Controls
        this.ImageButton1.Enabled = true;
        this.ddlDistributor.Enabled = true;
        this.ddlgroup.Enabled = true;
        this.ddlBS.Enabled = true;

        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        if (Checker == "TRUE")
        {

            btnaddhide.Style["display"] = "none";
            this.btnsubmitdiv.Visible = false;
            this.divbtnapprove.Visible = true;
            this.divbtnreject.Visible = true;
            this.lblCheckerNote.Visible = false;
            this.txtCheckerNote.Visible = false;
        }
        else
        {

            btnaddhide.Style["display"] = "";
            this.btnsubmitdiv.Visible = true;
            this.divbtnapprove.Visible = false;
            this.divbtnreject.Visible = false;
            this.lblCheckerNote.Visible = false;
            this.txtCheckerNote.Visible = false;
        }

        #endregion
    }
    #endregion

    #region ResetSession
    public void ResetSession()
    {
        Session.Remove("GROSSRETURNDETAILS");
        Session.Remove("dtGrossReturnTaxCount");
        Session.Remove("GROSSRETTAXCOMPONENTDETAILS");
    }
    #endregion

    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadGrossReturn();
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
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            grdDespatchHeader.DataSource = clsInvc.BindInvoiceWaybillFilter(this.ddlWaybillFilter.SelectedValue, Convert.ToString(HttpContext.Current.Session["FINYEAR"]), this.ddlBS.SelectedValue.Trim(), HttpContext.Current.Session["UserID"].ToString());
            grdDespatchHeader.DataBind();
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

    #region CalculateTotalQty
    decimal CalculateTotalQty(DataTable dt,string ProductID)
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
    decimal CalculateTotalFreeQty(DataTable dt,string FreeProductID,string ProductID)
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

    #region Edit Gross Return
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            clsDespatchStock clsDespatchStck = new clsDespatchStock();
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            clsSaleInvoice INV = new clsSaleInvoice();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            
            this.txtDeliveredQty.Text = "0";
            this.txtOrderDate.Text = "01/01/1900";
            decimal AfterRebateAmount = 0;
            decimal AddSSMarginPercentage = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string Brand = string.Empty;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal GrossFinal = 0;

            DataSet ds = new DataSet();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";

            #region QueryString
            
            if (Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                if (clsInvc.GetFinancestatus(this.hdnDespatchID.Value.Trim()) == "1" && clsInvc.Getstatus(this.hdnDespatchID.Value.Trim()) == "1")
                {
                    this.btnsubmitdiv.Visible = false;
                    this.grdAddDespatch.Columns[0].Visible = false;
                }
                else if (clsInvc.GetFinancestatus(this.hdnDespatchID.Value.Trim()) == "0" && clsInvc.Getstatus(this.hdnDespatchID.Value.Trim()) == "0")
                {
                    this.btnsubmitdiv.Visible = true;
                    this.grdAddDespatch.Columns[0].Visible = true;
                }
                else
                {
                    this.btnsubmitdiv.Visible = false;
                    this.grdAddDespatch.Columns[0].Visible = false;
                }
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            #region Disable Controls
            this.ImageButton1.Enabled = true;
            this.ddlDistributor.Enabled = false;
            this.ddlgroup.Enabled = false;
            this.ddlBS.Enabled = false;
            #endregion

            this.ResetSession();
            this.LoadCategory();
            string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsInvc.EditGrossReturnDetails(saleInvoiceID);

            this.CreateDataTable();
            this.CreateDataTableTaxComponent();


           

            #region Header Table Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.LoadBS();
                this.ddlBS.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["BSID"]).Trim();

                this.ddlgroup.Items.Clear();
                this.ddlgroup.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["GROUPNAME"]).Trim(), Convert.ToString(ds.Tables[0].Rows[0]["GROUPID"]).Trim()));
                this.ddlgroup.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GROUPID"]).Trim();
               
                if (this.ddlgroup.SelectedValue != "0")
                {
                    
                    this.BindCustomer();
                    this.LoadCategory();
                    
                }
                this.LoadMotherDepot();

                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALERETURNNO"]).Trim();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPOTID"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SALERETURNDATE"]).Trim();
                
                //=== Waybill And Transporter Bind Based On Depot====//
                DataTable dtTransporter = clsInvc.BindDepot_Transporter(this.ddlDepot.SelectedValue.Trim(), Convert.ToString(hdnDespatchID.Value).Trim());
                if (dtTransporter.Rows.Count > 0)
                {
                    this.ddlTransporter.Items.Clear();
                    this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
                    this.ddlTransporter.AppendDataBoundItems = true;
                    this.ddlTransporter.DataSource = dtTransporter;
                    this.ddlTransporter.DataValueField = "ID";
                    this.ddlTransporter.DataTextField = "NAME";
                    this.ddlTransporter.DataBind();
                }
                else
                {
                    this.ddlTransporter.Items.Clear();
                    this.ddlTransporter.SelectedValue = "0";
                }
                //===========================================================//

                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]).Trim();
                
                this.txtTotPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]).Trim();

                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]).Trim();
                this.txtInvNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]).Trim();
                this.txtInvDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]).Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim() == "01/01/1900")
                {
                    this.txtLRGRDate.Text = "";
                }
                else
                {
                    this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]).Trim();
                }
                this.txtGetpass.Text = Convert.ToString(ds.Tables[0].Rows[0]["GETPASSNO"]).Trim();
                
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
               
                if (this.ddlDistributor.SelectedValue != "0")
                {
                    this.LoadProduct();
                }
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[4].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["GROSSRETTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["SALEORDERID"] = "NA";
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[4].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[4].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[4].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[4].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[4].Rows[i]["TAXVALUE"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[4].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }

                HttpContext.Current.Session["GROSSRETTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtGrossReturnTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                DataTable dtInvoiceEdit = (DataTable)Session["GROSSRETURNDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow drEditInvoice = dtInvoiceEdit.NewRow();
                    drEditInvoice["GUID"] = Guid.NewGuid();
                    drEditInvoice["SALEORDERID"] = "NA";
                    drEditInvoice["SALEORDERDATE"] = "01/01/1900";
                    drEditInvoice["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    drEditInvoice["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    drEditInvoice["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    drEditInvoice["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    drEditInvoice["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    drEditInvoice["BCP"] = Convert.ToString(ds.Tables[1].Rows[i]["BCP"]);
                    drEditInvoice["QTY"] = "0";
                    drEditInvoice["RETURNQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RETURNQTY"]);
                    drEditInvoice["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                    drEditInvoice["ASSESMENTPERCENTAGE"] = "0";
                    drEditInvoice["TOTALASSESMENTVALUE"] = "0";
                    drEditInvoice["PRIMARYPRICESCHEMEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRIMARYPRICESCHEMEID"]);
                    drEditInvoice["PERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["PERCENTAGE"]);
                    drEditInvoice["VALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["VALUE"]);
                    drEditInvoice["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    drEditInvoice["TOTMRP"] = "0";
                    drEditInvoice["WEIGHT"] = "0";
                    drEditInvoice["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    drEditInvoice["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);

                    Brand = clsInvc.getBrand(Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]));

                    if (AddSSMarginPercentage == 0)
                    {
                        AfterRebateAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString());
                    }
                    else if (AddSSMarginPercentage > 0 )
                    {
                        AfterRebateAmount = (Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) - ((Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * AddSSMarginPercentage) / 100));
                    }
                    decimal BillTaxAmt = 0;
                     
                    #region Loop For Adding Itemwise Tax Component
                    if (dtTaxCountDataAddition.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables[3].Rows[k]["RELATEDTO"]))
                            {

                                case "1":
                                    if (Brand.Trim() == "6B869907-14E0-44F6-9BA5-57F4DA726686")
                                    {
                                        ProductWiseTax = 0;
                                        drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                        drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", AfterRebateAmount * ProductWiseTax / 100));
                                        BillTaxAmt += Convert.ToDecimal(drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    }
                                    else
                                    {
                                        TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                        ProductWiseTax = clsInvc.GetHSNTaxOnEdit(saleInvoiceID, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim());
                                        drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                        double TaxValue = 0;
                                        TaxValue = Convert.ToDouble(Math.Floor(((AfterRebateAmount * ProductWiseTax) / 100) * 100) / 100);
                                        drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                        BillTaxAmt += Convert.ToDecimal(drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    }
                                    break;
                            }
                        }
                    }
                    #endregion

                    drEditInvoice["NETAMOUNT"] = Convert.ToString(BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()));

                    dtInvoiceEdit.Rows.Add(drEditInvoice);
                    dtInvoiceEdit.AcceptChanges();
                }
                
                TotalMRP = 0;
                TotalAmount = CalculateGrossTotal(dtInvoiceEdit);
                TotalTax = CalculateTaxTotal(dtInvoiceEdit);
                GrossTotal = TotalAmount + TotalTax;
                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region grdAddDespatch DataBind
                HttpContext.Current.Session["GROSSRETURNDETAILS"] = dtInvoiceEdit;
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

            #region Tax On Gross Amount
            this.LoadTermsConditions();
            #endregion

            GrossFinal = 0; 

            #region Amount-Calculation
            if (ds.Tables[2].Rows.Count > 0)
            {
                
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[2].Rows[0]["NETAMOUNT"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[2].Rows[0]["TOTALSALEINVOICEVALUE"].ToString()));
            }
            #endregion

            #region Grid Calculation
            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridAddDespatchStyle();
                this.BillingGridCalculation();
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

    #region LoadCustomer
    public void LoadCustomer()
    {
        try
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            DataTable dtCustomer = new DataTable();
            DataTable dtDepot = new DataTable();
            String DepotID = string.Empty;
            if (Session["TPU"].ToString().Trim() == "D" || Session["TPU"].ToString().Trim() == "EXPU")
            {
                DepotID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
                dtCustomer = clsInvc.BindInvoiceCustomer(this.ddlBS.SelectedValue.Trim(), DepotID);
            }
            else
            {
                ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
                dtDepot = clsReceivedStock.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                if (dtDepot.Rows.Count > 0)
                {
                    if (dtDepot.Rows.Count == 1)
                    {
                        DepotID = dtDepot.Rows[0]["BRID"].ToString().Trim();

                    }
                    else
                    {
                        DepotID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
                    }
                }

                
                dtCustomer = clsInvc.BindInvoiceCustomer(this.ddlBS.SelectedValue.Trim(), DepotID);
            }
            if (dtCustomer.Rows.Count > 0)
            {
                if (dtCustomer.Rows.Count > 1)
                {
                    this.ddlDistributor.Items.Clear();
                    this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    this.ddlDistributor.AppendDataBoundItems = true;
                    this.ddlDistributor.DataSource = dtCustomer;
                    this.ddlDistributor.DataValueField = "CUSTOMERID";
                    this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                    this.ddlDistributor.DataBind();
                }
                else if (dtCustomer.Rows.Count == 1)
                {
                    this.ddlDistributor.Items.Clear();
                    this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    this.ddlDistributor.DataSource = dtCustomer;
                    this.ddlDistributor.DataValueField = "CUSTOMERID";
                    this.ddlDistributor.DataTextField = "CUSTOMERNAME";
                    this.ddlDistributor.DataBind();
                    this.ddlDistributor.SelectedValue = Convert.ToString(dtCustomer.Rows[0]["CUSTOMERID"]);

                }
            }
            else
            {
                this.ddlDistributor.Items.Clear();
                this.ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlDistributor.AppendDataBoundItems = true;
            }
            
            
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region ddlDistributor_SelectedIndexChanged
    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistributor.SelectedValue != "0")
        {
            
            this.LoadProduct();
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            
            this.CreateStructure();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlCategory.ClientID + "').focus(); ", true);
            

            
        }
        else
        {
            this.txtSSMarginPercentage.Text = "0.00";
            this.txtSSMarginAmt.Text = "0.00";
        }
    }
    #endregion

    #region  DataTable Structure Method
    protected void CreateStructure()
    {
        this.CreateDataTable();// Creating DataTable Structure
        this.CreateDataTableTaxComponent();// Creating DataTable Structure
        this.LoadTax();
        
    }
    #endregion

    #region Resetting Controls After Add
    public void ResetControls()
    {
        this.txtMRP.Text = "";
        this.txtBaseCost.Text = "";
        this.txtAssementPercentage.Text = "";
        this.txtDeliveredQty.Text = "";
        this.txtDeliveredQtyPCS.Text = "";
        this.txtStockQty.Text = "";
        this.txtBatchNo.Text = "";
        this.txtMFDate.Text = "";
        this.txtEXPRDate.Text = "";
        
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnDespatchID.Value = "";
        this.txtSaleInvoiceNo.Text = "";
        this.ddlTransporter.SelectedValue = "0";
        this.ddlTransportMode.SelectedValue = "By Road";
        this.txtVehicle.Text = "";
        this.ddlWaybill.Items.Clear();
        this.ddlWaybill.Items.Add(new ListItem("Select Waybill", "0"));
        this.ddlWaybill.AppendDataBoundItems = true;
        this.txtLRGRDate.Text = "";
        this.txtLRGRNo.Text = "";
        this.txtGetPassDate.Text = "";
        this.txtGetpass.Text = "";
        this.txtRemarks.Text = "";
        this.txtDeliveredQtyPCS.Text = "";
        this.ddlBS.SelectedValue = "0";
        this.ddlDistributor.SelectedValue = "0";
        this.ddlProduct.Items.Clear();
        this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
        this.ddlProduct.AppendDataBoundItems = true;
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "";
        this.txtTotMRP.Text = "";
        this.txtTotDisc.Text = "0.00";
        this.txtTotTax.Text = "";
        this.txtNetAmt.Text = "";
        this.txtRoundoff.Text = "";
        this.txtDeliveredQty.Text = "";
        this.txtStockQty.Text = "";
        this.txtMRP.Text = "";
        this.txtBaseCost.Text = "";
        this.txtTotCase.Text = "";
        this.ddlPaymentMode.SelectedValue = "Cash";
        this.txtAssementPercentage.Text = "";
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.txtFinalAmt.Text = "";
        this.txtSSMarginPercentage.Text = "0.00";
        this.txtSSMarginAmt.Text = "0.00";
        this.txtSchemeAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
        this.txtInvNo.Text ="";
        this.txtInvDate.Text = "";
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtInner = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));//1
        dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));//2
        dt.Columns.Add(new DataColumn("SALEORDERDATE", typeof(string)));//3
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));//4
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//5
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));//6
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));//7
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));//8
        dt.Columns.Add(new DataColumn("BCP", typeof(string)));//9
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));//10
        dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));//11
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));//12
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//13
        dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string)));//14
        dt.Columns.Add(new DataColumn("PRIMARYPRICESCHEMEID", typeof(string)));//15
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));//16
        dt.Columns.Add(new DataColumn("VALUE", typeof(string)));//17
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));//18
        dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));//19
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));//20
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));//21
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));//22
        #region Loop For Adding Itemwise Tax Component
        if (hdnDespatchID.Value == "")
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            string flag = clsInvc.BindRegion(this.ddlDistributor.SelectedValue.Trim(), ddlDepot.SelectedValue.ToString().Trim());

            if (string.IsNullOrEmpty(flag))
            {

                dtTaxCount = clsInvc.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlDistributor.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                
            }
            else
            {
                dtTaxCount = clsInvc.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlDistributor.SelectedValue.Trim(), this.ddlProduct.SelectedValue.Trim(), this.ddlDistributor.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
                
            }
            
            Session["dtGrossReturnTaxCount"] = dtTaxCount;
            
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
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            ds = clsInvc.EditGrossReturnDetails(InvoiceID);
            Session["dtGrossReturnTaxCount"] = ds.Tables[3];
            if (ds.Tables[3].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables[3].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[3].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[3].Rows[k]["NAME"]) + "", typeof(string)));
                    
                }
            }
        }
        #endregion
        dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        HttpContext.Current.Session["GROSSRETURNDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        if (hdnDespatchID.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            HttpContext.Current.Session["GROSSRETTAXCOMPONENTDETAILS"] = dt;
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            HttpContext.Current.Session["GROSSRETTAXCOMPONENTDETAILS"] = dt;
        }
        return dt;
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string SALEORDERID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES,string TAXID,string MRP)
    {
        ClsGrossReturn clsInvc = new ClsGrossReturn();
        string TaxID = string.Empty;
        DataTable dt = (DataTable)Session["GROSSRETTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["SALEORDERID"] = SALEORDERID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["MRP"] = MRP;
        dt.Rows.Add(dr);
        dt.AcceptChanges();


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
        ClsGrossReturn clsInvoice = new ClsGrossReturn();
        decimal TotalBillPCS = 0;
        decimal TotalFreePCS = 0;
        
        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            TotalBillPCS += clsInvoice.QtyInCase(row.Cells[4].Text.Trim(), row.Cells[6].Text.Trim(), "B9F29D12-DE94-40F1-A668-C79BF1BF4425", Convert.ToDecimal(row.Cells[10].Text.Trim())) + Convert.ToDecimal(row.Cells[11].Text.Trim());
        }
        

        if (TotalBillPCS == 0 & TotalFreePCS == 0)
        {
            this.txtTotPCS.Text = "0.00";
        }
        else
        {
            this.txtTotPCS.Text = (TotalBillPCS + TotalFreePCS).ToString("#.00");
        }

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

    #region BillingGridCalculation
    protected void BillingGridCalculation()
    {
        decimal TotalGridMRP = 0;
        decimal TotalGridVALUE = 0;
        decimal TotalGridAmount = 0;
        decimal TotalNetAmt = 0;
        DataTable dtGROSSRETDETAILS = new DataTable();
        if (Session["GROSSRETURNDETAILS"] != null)
        {
            dtGROSSRETDETAILS = (DataTable)Session["GROSSRETURNDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtGROSSRETDETAILS);
        
        this.grdAddDespatch.HeaderRow.Cells[1].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[4].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[6].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[7].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[10].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[13].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[14].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[16].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[17].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[19].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[20].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[21].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[22].Visible = false;
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


        this.grdAddDespatch.FooterRow.Cells[1].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[2].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[3].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[4].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[6].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[7].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[10].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[13].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[14].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[15].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[16].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[17].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[19].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[20].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[21].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[22].Visible = false;
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

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {

            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[6].Visible = false;
            row.Cells[7].Visible = false;
            row.Cells[10].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[19].Visible = false;
            row.Cells[20].Visible = false;
            row.Cells[21].Visible = false;
            row.Cells[22].Visible = false;
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

            
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;

            TotalGridAmount += Convert.ToDecimal(row.Cells[18].Text.Trim());
            TotalGridMRP += Convert.ToDecimal(row.Cells[19].Text.Trim());
            TotalGridVALUE += Convert.ToDecimal(row.Cells[17].Text.Trim());

            int count = 22;
            DataTable dt = (DataTable)Session["dtGrossReturnTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grdAddDespatch.HeaderRow.Cells[count].Wrap = false;
            }
        }

        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtGrossReturnTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 24; i <= (24 + dtTaxCountDataAddition1.Rows.Count); i += 2)
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
            this.grdAddDespatch.FooterRow.Cells[17].Text = "0.00";
            this.txtSchemeAmt.Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[17].Text = TotalGridVALUE.ToString("#.00");
            this.txtSchemeAmt.Text = TotalGridVALUE.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[17].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[17].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[17].Wrap = false;
        #endregion
        
        if (TotalGridAmount == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[18].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[18].Text = TotalGridAmount.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[18].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[18].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[18].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;

        if (TotalGridMRP == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[19].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[19].Text = TotalGridMRP.ToString("#.00");
        }

        this.grdAddDespatch.FooterRow.Cells[16].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[16].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[16].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[19].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[19].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[19].Wrap = false;


        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[25 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[25 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[25 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[25 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[25 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[25 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[25 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[25 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[24 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[24 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[24 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[24 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[24 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[24 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[24 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[24 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[24 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[23 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[23 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[23 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[23 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[23 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[23 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[23 + count1].Wrap = false;
        }
        #endregion
    }
    #endregion

    #region LoadInvoice
    public void LoadGrossReturn()
    {
        try
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.grdDespatchHeader.DataSource = clsInvc.BindGrossReturn(txtFromDate.Text.Trim(), 
                                                                            txtToDate.Text.Trim(), 
                                                                            HttpContext.Current.Session["FINYEAR"].ToString().Trim(), 
                                                                            this.ddlBS.SelectedValue.Trim(),
                                                                            "15E687A6-CD85-412A-ABD4-B52AB91CADE0", 
                                                                            Checker, 
                                                                            HttpContext.Current.Session["UserID"].ToString().Trim());
            this.grdDespatchHeader.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
            this.LoadProduct();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlProduct.ClientID + "').focus(); ", true);
       
    }
    #endregion

    #region Load Business Segment
    protected void LoadBS()
    {

        
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            DataTable dtBS = new DataTable();
            
            dtBS = clsInvc.BindBS();
            if (dtBS.Rows.Count > 0)
            {
                this.ddlBS.Items.Clear();
                this.ddlBS.Items.Add(new ListItem("Select Business Segment", "0"));
                this.ddlBS.AppendDataBoundItems = true;
                this.ddlBS.DataSource = dtBS;
                this.ddlBS.DataValueField = "BSID";
                this.ddlBS.DataTextField = "BSNAME";
                this.ddlBS.DataBind();
            }
    }
    #endregion

    #region Load Product
    protected void LoadProduct()
    {
        
        if (this.ddlDistributor.SelectedValue != "0")
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            
            DataTable dtProductCache = new DataTable();
            if (dtProductCache.Rows.Count == 0)
            {
                if (this.ddlCategory.SelectedValue == "0")
                {
                    dtProductCache = clsInvc.BindInvoiceProduct(this.ddlDistributor.SelectedValue.Trim(), this.ddlBS.SelectedValue.Trim(), Convert.ToString(hdnDespatchID.Value).Trim());
                }
                else if (this.ddlCategory.SelectedValue != "0")
                {
                    dtProductCache = clsInvc.BindInvoiceCategoryProduct(this.ddlDistributor.SelectedValue.Trim(), this.ddlBS.SelectedValue.Trim(), this.ddlCategory.SelectedValue.ToString().Trim(), Convert.ToString(hdnDespatchID.Value).Trim());
                }
                
            }
            
                if (dtProductCache.Rows.Count > 0)
                {
                    if (this.ddlCategory.SelectedValue.Trim() == "0")
                    {
                        if (dtProductCache.Rows.Count > 1)
                        {
                            this.ddlProduct.Items.Clear();
                            this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                            this.ddlProduct.AppendDataBoundItems = true;
                            this.ddlProduct.DataSource = dtProductCache;
                            this.ddlProduct.DataValueField = "PRODUCTID";
                            this.ddlProduct.DataTextField = "PRODUCTNAME";
                            this.ddlProduct.DataBind();
                        }
                        else if (dtProductCache.Rows.Count == 1)
                        {
                            this.ddlProduct.Items.Clear();
                            this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                            this.ddlProduct.AppendDataBoundItems = true;
                            this.ddlProduct.DataSource = dtProductCache;
                            this.ddlProduct.DataValueField = "PRODUCTID";
                            this.ddlProduct.DataTextField = "PRODUCTNAME";
                            this.ddlProduct.DataBind();
                            this.ddlProduct.SelectedValue = Convert.ToString(dtProductCache.Rows[0]["PRODUCTID"]);
                            this.ProductDetails();

                        }
                    }
                    if (this.ddlCategory.SelectedValue.Trim() != "0")
                    {

                        DataTable dtProductCacheClone = new DataTable();
                        DataView dvProductCacheClone = new DataView(dtProductCache);
                        dvProductCacheClone.RowFilter = "CATID = '" + this.ddlCategory.SelectedValue.Trim() + "'";
                        
                        
                        if (dtProductCache.Rows.Count > 0)
                        {
                            if (dtProductCache.Rows.Count > 1)
                            {
                                this.ddlProduct.Items.Clear();
                                this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                                this.ddlProduct.AppendDataBoundItems = true;
                                this.ddlProduct.DataSource = dvProductCacheClone;
                                this.ddlProduct.DataValueField = "PRODUCTID";
                                this.ddlProduct.DataTextField = "PRODUCTNAME";
                                this.ddlProduct.DataBind();
                            }
                            else if (dtProductCacheClone.Rows.Count == 1)
                            {
                                this.ddlProduct.Items.Clear();
                                this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                                this.ddlProduct.AppendDataBoundItems = true;
                                this.ddlProduct.DataSource = dvProductCacheClone;
                                this.ddlProduct.DataValueField = "PRODUCTID";
                                this.ddlProduct.DataTextField = "PRODUCTNAME";
                                this.ddlProduct.DataBind();
                                this.ProductDetails();

                            }
                        }
                       
                    }
                }
                else
                {
                    this.ddlProduct.Items.Clear();
                    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
                    this.ddlProduct.AppendDataBoundItems = true;
                }
            
            //=== Combo Product =========//
            DataSet dsComboProduct = new DataSet();
            dsComboProduct = clsInvc.BindInvoiceComboProduct(this.ddlDistributor.SelectedValue.Trim(), this.ddlBS.SelectedValue.Trim());
            dsComboProduct.Tables[0].TableName = "ComboProduct";
            if (dsComboProduct.Tables["ComboProduct"].Rows.Count > 0)
            {
                Session["ComboProduct"] = dsComboProduct.Tables["ComboProduct"];
                
            }
            //========================//
        }
    }
    #endregion

    #region btnApprove_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
            int flag = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsPurchaseStockReceipt.ApproveSaleReturn(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtSaleInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                LoadGrossReturn();
                MessageBox1.ShowSuccess("Sale Invoice: <b><font color='green'>" + this.txtSaleInvoiceNo.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 700);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
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

    #region grdDespatchHeader_RowDataBound
    protected void grdDespatchHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[9] as GridDataControlFieldCell;
                GridDataControlFieldCell cell10 = e.Row.Cells[10] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper().Trim();
                string DAYEND = cell10.Text.Trim().ToUpper().Trim();

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

                if (DAYEND == "PENDING")
                {
                    cell10.ForeColor = Color.Blue;
                }
                else
                {
                    cell10.ForeColor = Color.Green;
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

    #region GridAddDespatchStyle
    public void GridAddDespatchStyle()
    {
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "PRODUCT";
        this.grdAddDespatch.HeaderRow.Cells[7].Text = "PACKSIZE";
        this.grdAddDespatch.HeaderRow.Cells[9].Text = "RATE";
        this.grdAddDespatch.HeaderRow.Cells[10].Text = "CASE";
        this.grdAddDespatch.HeaderRow.Cells[11].Text = "PCS";
        this.grdAddDespatch.HeaderRow.Cells[16].Text = "SCH%";
        this.grdAddDespatch.HeaderRow.Cells[17].Text = "SCH AMT";
        this.grdAddDespatch.HeaderRow.Cells[18].Text = "AMOUNT";
        this.grdAddDespatch.HeaderRow.Cells[19].Text = "TOT MRP";
        
        this.grdAddDespatch.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Left;
        this.grdAddDespatch.FooterRow.Cells[17].HorizontalAlign = HorizontalAlign.Left;
        this.grdAddDespatch.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Left;
        this.grdAddDespatch.FooterRow.Cells[19].HorizontalAlign = HorizontalAlign.Left;

        this.grdAddDespatch.HeaderRow.Cells[12].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[12].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[12].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[12].Wrap = false;

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            row.Cells[12].Visible = false;
        }

    }
    #endregion
   
    #region LoadCategory
    public DataTable LoadCategory()
    {
        DataTable dtCategoryCache = HttpContext.Current.Cache["Category"] as DataTable;
        ClsProductMaster clsProduct = new ClsProductMaster();
        if (dtCategoryCache == null)
        {
            dtCategoryCache = clsProduct.BindCategory();
            if (dtCategoryCache.Rows.Count > 0)
            {
                HttpContext.Current.Cache["Category"] = dtCategoryCache;
                HttpContext.Current.Cache.Insert("Category", dtCategoryCache, null, DateTime.Now.AddDays(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
        if (dtCategoryCache != null)
        {
            if (dtCategoryCache.Rows.Count > 0)
            {
                this.ddlCategory.Items.Clear();
                this.ddlCategory.Items.Add(new ListItem("--- ALL ---", "0"));
                this.ddlCategory.AppendDataBoundItems = true;
                this.ddlCategory.DataSource = dtCategoryCache;
                this.ddlCategory.DataValueField = "CATID";
                this.ddlCategory.DataTextField = "CATNAME";
                this.ddlCategory.DataBind();
            }
        }
        return dtCategoryCache;
    }
    #endregion



    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = "frmPrintPopUp.aspx?pid=" + hdnDespatchID.Value + "&&BSID=" + Request.QueryString["BSID"].ToString() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    public void loadadd()
    {
        this.ImageButton1.Focus();
        this.hdnDespatchID.Value = "";
        this.ClearControls();
        this.ResetControls();
        this.ResetSession();
        this.trAutoInvoiceNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.btnaddhide.Style["display"] = "none";
        #region Enable Controls
        this.ImageButton1.Enabled = true;
        this.ddlBS.Enabled = true;
        this.ddlDistributor.Enabled = true;
        this.ddlgroup.Enabled = true;
        #endregion
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();

        this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtOrderDate.Text = dtcurr.ToString(date).Replace('-', '/');

        this.btnsubmitdiv.Visible = true;
        this.grdAddDespatch.Columns[0].Visible = true;

        /*Received Date Must Be bettween last two date and today*/
        /*Added By Sayan Dey On 08.03.2019*/
        DateTime datevalidation = DateTime.Today.Date;
        DateTime beforedays = datevalidation.AddDays(-7);
        CalendarExtenderInvoiceDate.EndDate = datevalidation;
        CalendarExtenderInvoiceDate.StartDate = beforedays;

        foreach (ListItem item in ddlDepot.Items)
        {
            if (item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "Blue");
            }
        }
        if (Request.QueryString["CHECKER"].ToString().Trim() == "TRUE")
        {
            this.btnsubmitdiv.Visible = false;
            this.grdAddDespatch.Columns[0].Visible = false;
        }
        else
        {
            this.btnsubmitdiv.Visible = true;
            this.grdAddDespatch.Columns[0].Visible = true;
        }
    }

    public void loadsave()
    {
        ClsEntryLock objLock = new ClsEntryLock();
        bool ObjDate = objLock.EntryLock(this.txtInvoiceDate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
        if (ObjDate == true)
        {
            ClsGrossReturn clsInvc = new ClsGrossReturn();
            int CountTerms = 0;
            string InvoiceType = string.Empty;
            string InvoiceNo = string.Empty;
            string xml = string.Empty;
            string xmlTax = string.Empty;
            string xmlGrossTax = string.Empty;
            string xmlQtyScheme = string.Empty;
            string xmlOrder = string.Empty;

            string strTaxID = string.Empty;
            string strTaxPercentage = string.Empty;
            string strTaxValue = string.Empty;
            string strTermsID = string.Empty;
            string saleOrderDate = string.Empty;
            decimal Disc = 0;
            string Remarks = string.Empty;

            DataTable dtTaxCount = new DataTable();
            DataTable dtRecordsCheck = new DataTable();
            DataTable dtTaxRecordsCheck = new DataTable();


            if (Session["dtGrossReturnTaxCount"] != null)
            {
                dtTaxCount = (DataTable)Session["dtGrossReturnTaxCount"];
            }
            if (Session["GROSSRETURNDETAILS"] != null)
            {
                dtRecordsCheck = (DataTable)Session["GROSSRETURNDETAILS"];
            }
            if (Session["GROSSRETTAXCOMPONENTDETAILS"] != null)
            {
                dtTaxRecordsCheck = (DataTable)Session["GROSSRETTAXCOMPONENTDETAILS"];
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
            decimal SSMarginPercentage = 0;
            decimal SSMarginAmt = 0;
            decimal ActualTotalCase = 0;
            decimal TotalInvoiceValue = 0;
            decimal NetAmount = 0;
            decimal GrossAmount = 0;

            if (this.txtSSMarginPercentage.Text == "")
            {
                SSMarginPercentage = 0;
            }
            else
            {
                SSMarginPercentage = Convert.ToDecimal(this.txtSSMarginPercentage.Text.Trim());
            }

            if (this.txtSSMarginAmt.Text == "")
            {
                SSMarginAmt = 0;
            }
            else
            {
                SSMarginAmt = Convert.ToDecimal(this.txtSSMarginAmt.Text.Trim());
            }

            if (this.txtTotCase.Text == "")
            {
                ActualTotalCase = 0;
            }
            else
            {
                ActualTotalCase = Convert.ToDecimal(this.txtTotCase.Text.Trim());
            }
            if (this.txtAmount.Text == "")
            {
                TotalInvoiceValue = 0;
            }
            else
            {
                TotalInvoiceValue = Convert.ToDecimal(this.txtAmount.Text.Trim());
            }
            if (this.txtTotalGross.Text == "")
            {
                GrossAmount = 0;
            }
            else
            {
                GrossAmount = Convert.ToDecimal(this.txtTotalGross.Text.Trim());
            }
            if (this.txtFinalAmt.Text == "")
            {
                NetAmount = 0;
            }
            else
            {
                NetAmount = Convert.ToDecimal(this.txtFinalAmt.Text.Trim());
            }
            #endregion


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

            Remarks = this.txtRemarks.Text.Trim();
            if (Remarks.Contains("'"))
            {
                Remarks = Remarks.Replace("'", "''");
            }

            if (this.txtLRGRDate.Text.Trim() != "" || this.txtGetPassDate.Text.Trim() != "")
            {
                if (this.txtTotDisc.Text == "")
                {
                    Disc = 0;
                }
                else
                {
                    Disc = Convert.ToDecimal(this.txtTotDisc.Text.Trim());
                }
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                InvoiceNo = clsInvc.InsertGrossReturn(this.txtInvoiceDate.Text.Trim(), this.ddlDistributor.SelectedValue.Trim(),
                                                        Convert.ToString(this.ddlDistributor.SelectedItem).Trim(), this.ddlDepot.SelectedValue.Trim(),
                                                        Convert.ToString(this.ddlDepot.SelectedItem).Trim(),
                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                        Remarks,
                                                        this.ddlBS.SelectedValue.Trim(),
                                                        Convert.ToString(this.ddlgroup.SelectedValue).Trim(),
                                                        Convert.ToDecimal(this.txtTotPCS.Text.Trim()),
                                                        this.txtGetpass.Text.Trim(),
                                                        this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                        this.ddlTransporter.SelectedValue.Trim(),
                                                        TotalInvoiceValue, OtherCharges,
                                                        Adjustment, Roundoff, Disc,
                                                        NetAmount, GrossAmount,
                                                        xml, xmlTax, xmlGrossTax,
                                                        Convert.ToString(hdnDespatchID.Value.Trim()), InvoiceType,
                                                        this.txtInvNo.Text.Trim(), this.txtInvDate.Text.Trim());
                if (InvoiceNo != "")
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();
                    if (Convert.ToString(hdnDespatchID.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Sale Return :<b><font color='green'>  " + InvoiceNo + "</font></b>  Saved Successfully", 80, 550);

                        this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.ClearControls();
                        this.ddlCategory.SelectedValue = "0";
                        this.ImageButton1.Focus();
                        this.trAutoInvoiceNo.Style["display"] = "none";
                        this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Sale Return : <b><font color='green'> " + InvoiceNo + " </font></b> Updated Successfully", 80, 550);

                        this.trAutoInvoiceNo.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "";
                        this.ClearControls();
                        this.ResetControls();
                        this.grdAddDespatch.DataSource = null;
                        this.grdAddDespatch.DataBind();
                        this.grdTax.ClearPreviousDataSource();
                        this.grdTax.DataSource = null;
                        this.grdTax.DataBind();
                        this.grdTerms.ClearPreviousDataSource();
                        this.grdTerms.DataSource = null;
                        this.grdTerms.DataBind();
                        this.hdnDespatchID.Value = "";
                        this.ddlCategory.SelectedValue = "0";
                        this.LoadGrossReturn();

                        #region Enable Controls
                        this.ImageButton1.Enabled = true;
                        this.ddlDistributor.Enabled = true;
                        this.ddlgroup.Enabled = true;
                        this.ddlBS.Enabled = true;
                        #endregion

                        this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());

                    }

                    this.hdnDespatchID.Value = "";
                    this.ResetSession();
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Error on Saving record..</font></b>");

                }

            }
            else
            {
                if (this.txtTotDisc.Text == "")
                {
                    Disc = 0;
                }
                else
                {
                    Disc = Convert.ToDecimal(this.txtTotDisc.Text.Trim());
                }
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                InvoiceNo = clsInvc.InsertGrossReturn(this.txtInvoiceDate.Text.Trim(), this.ddlDistributor.SelectedValue.Trim(),
                                                       Convert.ToString(this.ddlDistributor.SelectedItem).Trim(), this.ddlDepot.SelectedValue.Trim(),
                                                       Convert.ToString(this.ddlDepot.SelectedItem).Trim(),
                                                       HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                       HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                       Remarks,
                                                       this.ddlBS.SelectedValue.Trim(),
                                                       Convert.ToString(this.ddlgroup.SelectedValue).Trim(),
                                                       Convert.ToDecimal(this.txtTotPCS.Text.Trim()),
                                                       this.txtGetpass.Text.Trim(),
                                                       this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                       this.ddlTransporter.SelectedValue.Trim(),
                                                       TotalInvoiceValue, OtherCharges,
                                                       Adjustment, Roundoff, Disc,
                                                       NetAmount, GrossAmount,
                                                       xml, xmlTax, xmlGrossTax,
                                                       Convert.ToString(hdnDespatchID.Value.Trim()), InvoiceType,
                                                       this.txtInvNo.Text.Trim(), this.txtInvDate.Text.Trim());



                if (InvoiceNo != "")
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();
                    if (Convert.ToString(hdnDespatchID.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Sale Return :<b><font color='green'>  " + InvoiceNo + "</font></b>  Saved Successfully", 80, 550);
                        this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.ClearControls();
                        this.ddlCategory.SelectedValue = "0";
                        this.ImageButton1.Focus();
                        this.trAutoInvoiceNo.Style["display"] = "none";
                        this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Sale Return : <b><font color='green'> " + InvoiceNo + " </font></b> Updated Successfully", 80, 550);

                        this.trAutoInvoiceNo.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "";
                        this.ClearControls();
                        this.ResetControls();
                        this.grdAddDespatch.DataSource = null;
                        this.grdAddDespatch.DataBind();

                        this.grdTax.ClearPreviousDataSource();
                        this.grdTax.DataSource = null;
                        this.grdTax.DataBind();
                        this.grdTerms.ClearPreviousDataSource();
                        this.grdTerms.DataSource = null;
                        this.grdTerms.DataBind();
                        this.hdnDespatchID.Value = "";
                        this.ddlCategory.SelectedValue = "0";
                        this.LoadGrossReturn();

                        #region Enable Controls
                        this.ImageButton1.Enabled = true;
                        this.ddlDistributor.Enabled = true;
                        this.ddlgroup.Enabled = true;
                        #endregion

                        this.LoadPrincipleGroup(this.ddlBS.SelectedValue.Trim());
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

}