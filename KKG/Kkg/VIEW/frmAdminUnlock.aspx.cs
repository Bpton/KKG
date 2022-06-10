using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using Account;

public partial class VIEW_frmAdminUnlock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
                this.pnlADD.Style["display"] = "";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.Loaddepot();
                this.LoadBusinessSegment();
                this.trinvoice.Style["display"] = "";
                this.trvoucher.Style["display"] = "none";
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
                this.gvUnlockVoucher.DataSource = null;
                this.gvUnlockVoucher.DataBind();
                this.Bindmodule();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    private void Bindmodule()
    {
        clsSaleInvoice clsinvoice = new clsSaleInvoice();
        string offline = clsinvoice.getofflinetag();
        if (offline == "Y")
        {
            this.ddlModule.Items.Clear();
            /*this.ddlModule.Items.Add(new ListItem("Sale Invoice", "1"));
            this.ddlModule.Items.Add(new ListItem("Credit Note", "13"));
            this.ddlModule.Items.Add(new ListItem("Debit Note", "14"));
            this.ddlModule.Items.Add(new ListItem("Advance Receipt", "16"));
            this.ddlModule.Items.Add(new ListItem("Advance Payment", "15"));
            this.ddlModule.Items.Add(new ListItem("Voucher Journal", "18"));*/
            
        }
        else
        {
            this.ddlModule.Items.Clear();
            this.ddlModule.Items.Add(new ListItem("Sale Invoice", "1"));
            this.ddlModule.Items.Add(new ListItem("Stock Transfer", "2"));
            this.ddlModule.Items.Add(new ListItem("Purchase Stock Receipt", "3"));
            this.ddlModule.Items.Add(new ListItem("Depot Stock Receipt", "4"));
            this.ddlModule.Items.Add(new ListItem("Sale Return", "5"));
            this.ddlModule.Items.Add(new ListItem("Advance Receipt", "16"));
            this.ddlModule.Items.Add(new ListItem("Advance Payment", "15"));
            this.ddlModule.Items.Add(new ListItem("Voucher Journal", "18"));
            this.ddlModule.Items.Add(new ListItem("Transporter Bill", "17"));
            this.ddlModule.Items.Add(new ListItem("Purchase Bill(Factory)", "19"));
            this.ddlModule.Items.Add(new ListItem("Trading Invoice", "20"));
            this.ddlModule.Items.Add(new ListItem("Stock Despatch(Factory)", "21"));
            this.ddlModule.Items.Add(new ListItem("Factory Stock Receipt", "22"));
            this.ddlModule.Items.Add(new ListItem("Factory Quality Control", "23"));
            this.ddlModule.Items.Add(new ListItem("Account Checker-1(Factory)", "24"));
            this.ddlModule.Items.Add(new ListItem("Factory GRN Stock IN", "25"));
            this.ddlModule.Items.Add(new ListItem("Credit Note", "13"));
            this.ddlModule.Items.Add(new ListItem("Debit Note", "14"));
            this.ddlModule.Items.Add(new ListItem("Contra", "26"));
            this.ddlModule.Items.Add(new ListItem("Quality Control", "27"));
            this.ddlModule.Items.Add(new ListItem("Factory Purchase Return", "28"));
            this.ddlModule.Items.Add(new ListItem("Purchase Return", "35"));
            this.ddlModule.Items.Add(new ListItem("Purchase Order", "36"));
        }
    }


    private void Loaddepot()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            ClsStockReport clsrpt = new ClsStockReport();
            string OfflineTag = string.Empty;

            OfflineTag = clsInvc.GetOfflineTag();


            if (OfflineTag == "N")
            {
                DataTable dt = clsInvc.BindBranch();
                if (dt.Rows.Count > 0)
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                    this.ddlDepot.AppendDataBoundItems = true;
                    this.ddlDepot.DataSource = dt;
                    this.ddlDepot.DataValueField = "BRID";
                    this.ddlDepot.DataTextField = "BRNAME";
                    this.ddlDepot.DataBind();

                }
                else
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                }
            }
            else
            {
                DataTable dt = clsrpt.Region(Session["UTNAME"].ToString().Trim(), Session["IUSERID"].ToString().Trim());
                if (dt.Rows.Count > 0)
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.AppendDataBoundItems = true;
                    this.ddlDepot.DataSource = dt;
                    this.ddlDepot.DataValueField = "BRID";
                    this.ddlDepot.DataTextField = "BRNAME";
                    this.ddlDepot.DataBind();

                }
                else
                {
                    this.ddlDepot.Items.Clear();
                    this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModule.SelectedValue == "1" || ddlModule.SelectedValue == "5" || ddlModule.SelectedValue == "20")
        {
             this.tdBusinessSegment.Visible = true;
            this.tdlblBusinessSegment.Visible = true;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
            this.ddlModule.Style["display"] = "none";
        }
        else if (ddlModule.SelectedValue == "15" || ddlModule.SelectedValue == "16" || ddlModule.SelectedValue == "18" || ddlModule.SelectedValue == "13" || ddlModule.SelectedValue == "14" || ddlModule.SelectedValue == "26")    //Payment & Receipt
        {
            this.tdBusinessSegment.Visible = false;
            this.tdlblBusinessSegment.Visible = false;
            this.trinvoice.Style["display"] = "none";
            this.trvoucher.Style["display"] = "";
        }
        else
        {
            this.tdBusinessSegment.Visible = false;
            this.tdlblBusinessSegment.Visible = false;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
        }

        this.gvUnlockInvoice.DataSource = null;
        this.gvUnlockInvoice.DataBind();
        this.gvUnlockVoucher.DataSource = null;
        this.gvUnlockVoucher.DataBind();
    }

    private void LoadBusinessSegment()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            ClsStockReport clsrpt = new ClsStockReport();
            string OfflineTag = string.Empty;
            OfflineTag = clsInvc.GetOfflineTag();

            if (OfflineTag == "N")
            {
                DataTable dt = clsInvc.BindBusinessSegment();
                if (dt.Rows.Count > 0)
                {
                    this.ddlBusinessSegment.Items.Clear();
                    this.ddlBusinessSegment.Items.Add(new ListItem("Select", "0"));
                    this.ddlBusinessSegment.AppendDataBoundItems = true;
                    this.ddlBusinessSegment.DataSource = dt;
                    this.ddlBusinessSegment.DataValueField = "BSID";
                    this.ddlBusinessSegment.DataTextField = "BSNAME";
                    this.ddlBusinessSegment.DataBind();
                }
                else
                {
                    this.ddlBusinessSegment.Items.Clear();
                    this.ddlBusinessSegment.Items.Add(new ListItem("Select", "0"));
                }
            }
            else
            {
                DataTable dt = clsrpt.BindBusinessegmentGTMT();
                if (dt.Rows.Count > 0)
                {
                    this.ddlBusinessSegment.Items.Clear();
                    this.ddlBusinessSegment.Items.Add(new ListItem("Select", "0"));
                    this.ddlBusinessSegment.AppendDataBoundItems = true;
                    this.ddlBusinessSegment.DataSource = dt;
                    this.ddlBusinessSegment.DataValueField = "ID";
                    this.ddlBusinessSegment.DataTextField = "NAME";
                    this.ddlBusinessSegment.DataBind();
                }
                else
                {
                    this.ddlBusinessSegment.Items.Clear();
                    this.ddlBusinessSegment.Items.Add(new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadTransfer
    public void LoadTransfer()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindStockTranferForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTransferBill
    public void LoadTransferBill()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindTransporterBill(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadInvoice
    public void LoadInvoice()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindInvoiceForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTradingInvoice
    public void LoadTradingInvoice()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindTradingInvoiceForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadPurchaseStcokReceipt
    public void LoadPurchaseStcokReceipt()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindPurchaseStockReceiptForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                     this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadPurchaseReturn
    public void LoadPurchaseReturn()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindPurchaseReturnForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                     this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadFactoryStockReceipt
    public void LoadFactoryStockReceipt()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindFactoryStockReceiptForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                     this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadDepoReceived
    public void LoadDepoReceived()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindDepoReceivedForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                     this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadSaleReturn
    public void LoadSaleReturn()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindSaleReturnForUnlock(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                     this.ddlDepot.SelectedValue.Trim(), this.ddlBusinessSegment.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadVoucher
    public void LoadVoucher(string VoucherID)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = new DataTable();

            if (VoucherID == "13" || VoucherID == "14")
            {
                dt = clsVoucher.UnlockCreditNote(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), VoucherID, "TRUE", this.ddlDepot.SelectedValue.Trim(), "Y");
            }
            else
            {
                dt = clsVoucher.VoucherHeaderDetails(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), VoucherID, "TRUE", this.ddlDepot.SelectedValue.Trim(), "Y","");
            }
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockVoucher.DataSource = dt;
                this.gvUnlockVoucher.DataBind();
            }
            else
            {
                this.gvUnlockVoucher.DataSource = null;
                this.gvUnlockVoucher.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    #region LoadPurchaseBillFactory
    public void LoadPurchaseBillFactory()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindPurchaseBill(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadStockDespatchFactory
    public void LoadStockDespatchFactory()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindStockDespatchForFactory(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    #region LoadQCFactory
    public void LoadQCFactory()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindqcForFactory(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }


            gvUnlockInvoice.HeaderRow.Cells[7].Text = "GRN NO";
            gvUnlockInvoice.HeaderRow.Cells[6].Text = "GRN NET AMOUNT";
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadFactoryChecker1
    public void LoadFactoryChecker1()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindForFactorychecker1(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }



        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    #region LoadFactorygrnStockIn
    public void LoadFactorygrnStockIn()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindForFactoryGrnStockIn(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }



        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    /*sunny*/
    #region quality control
    public void LoadQualityControl()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.LoadQualityControl(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();

            }
            else
            {
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Factory Purchase Return
    public void LoadFactoryPurchaseReturn()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindPurchaseReturnForFactory(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    private void LoadPurchaseOrder()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = new DataTable();
            dt = clsVoucher.LoadPurchaseOrder(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }


    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            string module = "1";
            this.CreateDataTable();

            if (ddlModule.SelectedValue == "1")//invoice
            {
                this.LoadInvoice();
            }
            else if (ddlModule.SelectedValue == "2")//Stock Transfer
            {
                this.LoadTransfer();
            }
            else if (ddlModule.SelectedValue == "3")//Purchase Stock receipt
            {
                this.LoadPurchaseStcokReceipt();
            }
            else if (ddlModule.SelectedValue == "4")//Depot Stock Receipt
            {
                this.LoadDepoReceived();
            }
            else if (ddlModule.SelectedValue == "5")//SALE RETURN
            {
                this.LoadSaleReturn();
            }
            else if (ddlModule.SelectedValue == "15" || ddlModule.SelectedValue == "16" || ddlModule.SelectedValue == "18" || ddlModule.SelectedValue == "13" || ddlModule.SelectedValue == "14" || ddlModule.SelectedValue == "26")    //Advance Payment * Receipt
            {
                this.gvUnlockVoucher.Columns[10].Visible = false;
                if (ddlModule.SelectedValue == "18")
                {
                    module = "2";                   // VOUCHER JOURNAL
                }
                else if (ddlModule.SelectedValue == "26")
                {
                    module = "1";                   // CONTRA
                }
                else
                {
                    module = Convert.ToString(this.ddlModule.SelectedValue);
                }

                this.LoadVoucher(module);
            }
            else if (this.ddlModule.SelectedValue == "17")// Transporter Bill
            {
                this.LoadTransferBill();
            }
            else if (this.ddlModule.SelectedValue == "19") /*Purchase Bill(Factory)*/
            {
                this.LoadPurchaseBillFactory();
            }
            else if (this.ddlModule.SelectedValue == "20")/*Trading Invoice*/
            {
                this.LoadTradingInvoice();
            }
            else if (this.ddlModule.SelectedValue == "21")/*Stock Despatch(Factory)*/
            {
                this.LoadStockDespatchFactory();
            }
            else if (this.ddlModule.SelectedValue == "22")/*Factory Stock Receipt*/
            {
                this.LoadFactoryStockReceipt();
            }
            else if (this.ddlModule.SelectedValue == "23")/*Factory Quality Control*/
            {
                this.LoadQCFactory();
            }
            else if (this.ddlModule.SelectedValue == "24")/*Account Checker-1(Factory) */
            {
                this.LoadFactoryChecker1();
            }
            else if (this.ddlModule.SelectedValue == "25")/*Factory GRN Stock IN*/
            {
                this.LoadFactorygrnStockIn();
            }
            else if (this.ddlModule.SelectedValue == "27")/* Quality Control*/
            {
                this.LoadQualityControl();
            }
            else if (this.ddlModule.SelectedValue == "28")/*Factory Purchase Return*/
            {
                this.LoadFactoryPurchaseReturn();
            }
            else if (this.ddlModule.SelectedValue == "35")/*Factory Purchase Return*/
            {
                this.LoadPurchaseReturn();
            }

            else if (this.ddlModule.SelectedValue == "36")/*Purchase Order*/
            {
                this.LoadPurchaseOrder();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));


        HttpContext.Current.Session["SALEINVOICE"] = dt;


    }
    #endregion

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            int id = 0;
            string module = Convert.ToString(this.ddlModule.SelectedValue.Trim());

            GridView gvunlock = gvUnlockInvoice;
            string gvchkbox = "";
            string gvinvoiceid = "";
            int count = 0;
            string XML = string.Empty;
            string SALEINVOICEID = string.Empty;
            string ip_address = Request.UserHostAddress.ToString().Trim();
            string USERID = HttpContext.Current.Session["UserID"].ToString().Trim();


            if (HttpContext.Current.Session["SALEINVOICE"] == null)
            {
                CreateDataTable();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["SALEINVOICE"];

            if (module == "15" || module == "16" || module == "18" || module == "13" || module == "14" || module == "26")         //Advance Payment * Receipt * Voucher Journal  CreditNote  DebitNote
            {
                gvunlock = gvUnlockVoucher;
                gvchkbox = "chkSelectvou";
                gvinvoiceid = "lblAccEntryID";
            }
            else
            {
                gvunlock = gvUnlockInvoice;
                gvchkbox = "chkSelect";
                gvinvoiceid = "lblSALEINVOICEID";
            }

            foreach (GridViewRow gvrow in gvunlock.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                Label lblSALEINVOICEID = (Label)gvrow.FindControl("" + gvinvoiceid + "");
                if (chkBx.Checked)
                {
                    count = count + 1;
                    DataRow dr = DT.NewRow();
                    dr["SALEINVOICEID"] = lblSALEINVOICEID.Text.Trim();
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                }
            }
            HttpContext.Current.Session["SALEINVOICE"] = DT;

            if (count == 0)
            {
                MessageBox1.ShowInfo("Plese Select atleast 1 record... ");
                return;
            }
            else
            {
                XML = ConvertDatatable(DT);
                string Module = string.Empty;
                if (ddlModule.SelectedValue == "26")
                {
                    Module = "1";
                    id = clsInvc.SaveInvoice(this.ddlDepot.SelectedValue.Trim(), XML, Module.Trim(), ip_address, USERID);
                }
                else if (ddlModule.SelectedValue == "36")
                {
                    id = clsInvc.unlockPo(this.ddlDepot.SelectedValue.Trim(), XML, HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
                }
                else 
                {
                    id = clsInvc.SaveInvoice(this.ddlDepot.SelectedValue.Trim(), XML, this.ddlModule.SelectedValue.Trim(), ip_address, USERID);
                }

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
                this.gvUnlockVoucher.DataSource = null;
                this.gvUnlockVoucher.DataBind();

                if (id > 0)
                {
                    MessageBox1.ShowSuccess("Invoices unlocked successfully..");
                    this.gvUnlockInvoice.DataSource = null;
                    this.gvUnlockInvoice.DataBind();
                    this.gvUnlockVoucher.DataSource = null;
                    this.gvUnlockVoucher.DataBind();
                    HttpContext.Current.Session["SALEINVOICE"] = null;
                }
                else
                {
                    MessageBox1.ShowInfo("Error on unlocked..");
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnvoucherPrint_Click
    protected void btnvoucherPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn_views = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblid = gvr.Cells[1].Controls[0].FindControl("lblAccEntryID") as Label;

            string upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + grdlblid.Text.Trim() + "&Voucherid=" + this.ddlModule.SelectedValue.Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
}