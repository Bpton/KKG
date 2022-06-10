#region NameSpace
using BAL;
using Entitymodel;
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
#endregion

public class StockBatchdetails
{
    public string BATCHNO { get; set; }
    public string STOCKQTY { get; set; }
    public string ID { get; set; }
    public string MRP { get; set; }
    public string ASSESMENTPERCENTAGE { get; set; }
    public string MFDATE { get; set; }
    public string EXPRDATE { get; set; }
}

public partial class FACTORY_frmFactoryStockTransfer_RMPM : System.Web.UI.Page
{
    string Checker = string.Empty;
    string MENUID = string.Empty;
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Back Date Entry Validation
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            int Flag = ClsCommon.CheckDate(Request.QueryString["MENUID"].ToString().Trim());
            string userid = HttpContext.Current.Session["USERID"].ToString();
            if (Flag > 0)
            {
                this.imgchallandate.Enabled = true;
                this.CalendarExtender2.Enabled = true;
            }
            else
            {
                this.imgchallandate.Enabled = false;
                this.CalendarExtender2.Enabled = false;
            }
            #endregion

            #region QueryString
            MENUID = Request.QueryString["MENUID"].ToString().Trim();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                btnaddhide.Style["display"] = "none";
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.btnReject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.trproductdetails.Visible = false;
                
            }
            else
            {
                btnaddhide.Style["display"] = "";
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.trproductdetails.Visible = true;
            }
            #endregion

            this.DateLock();           
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.ddlfromdepot.Enabled = false;            
            this.LoadDispatchDetails();
            //this.LoadProductWisePacksize("0");
            this.LoadCategory();
            this.LoadDepo();   // Fill Mother Depot
            this.FillDepot(); // Fill To Transfer DEPOT
            this.MotherDepoChange();  // Fill Product List of MotherDepot
            this.BindInsurenceCompany();// Fill To Insuarance company
            this.LoadTranspoter();
            this.LoadModeofTransport();
            this.btnback.Visible = false;            
        }
        if (chkActive.Checked)
        {
            this.lbltext.Text = "Yes!";
            this.lbltext.Style.Add("color", "green");
            this.lbltext.Style.Add("font-weight", "bold");
        }
        else
        {
            this.lbltext.Text = "No!";
            this.lbltext.Style.Add("color", "red");
            this.lbltext.Style.Add("font-weight", "bold");
        }
        foreach (ListItem item in ddlbatchno.Items)
        {
            if (item.Value == "1")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "blue");
            }
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvtransfer.ClientID + "', 300, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region btnApprove_Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            ClsStockTransfer clsTransfer = new ClsStockTransfer();
            int flag = 0;
            string receivedID = Convert.ToString(hdn_transferid.Value).Trim();
            DataTable dtWayBill = new DataTable();
            DataTable dtCheckForm = new DataTable();
            dtWayBill = clsTransfer.CheckWaybillNo(receivedID);
            if (dtWayBill.Rows.Count > 0)
            {
                dtCheckForm = clsTransfer.CheckForm(receivedID);
                if (dtCheckForm.Rows.Count > 0)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>F Form Required</font></b>, please update form no. before approve", 60, 550);
                }
                else
                {
                    flag = clsPurchaseStockReceipt.ApproveFactoryStockTransfer(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(),"","");
                    if (flag == 1)
                    {
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.LoadDispatchDetails();
                        this.hdn_transferid.Value = "";
                        MessageBox1.ShowSuccess("Stock Transfer: <b><font color='green'>" + this.txttransferno.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 750);
                    }
                    else if (flag == 2)
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>Unable to process cause ledger not found from system....<br><br><font color='green'>Please inform to support team or refresh this page and try again!</font></font></b>", 60, 540);
                    }
                    else if (flag == 3) 
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>Unable to process cause debit and credit amount not same.... <br><br><font color='green'>Please inform to support team or refresh this page and try again!</font></font></b>", 60, 540);
                    }
                    else if (flag == 0)
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>System unable to process your approval right now.... <br><br><font color='green'>Please inform to support team or refresh this page and try again!</font></font></b>", 60, 540);
                    }
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Waybill No</font></b> not found, please update before approve", 60, 500);
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
    protected void btnRejectionNoteSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /*ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
            int flag = 0;
            string receivedID = Convert.ToString(hdn_transferid.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectStockTransfer(receivedID, this.txtRejectionNote.Text.Trim());
            this.hdn_transferid.Value = "";

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                LoadDispatchDetails();
                MessageBox1.ShowSuccess("Stock Transfer: <b><font color='green'>" + this.txttransferno.Text + "</font></b> rejected successfully.", 60, 500);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "block";
                this.fadeRejectionNote.Style["display"] = "block";

                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }*/
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnRejectionCloseLightbox_Click
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

    #region gvStockTransferDetails_RowDataBound
    protected void gvStockTransferDetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[13] as GridDataControlFieldCell;
                string status = cell.Text;

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
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Creating Session DataTable Structure
    public DataTable BindTransferGrid()
    {
        DataTable dttransfer = new DataTable();
        dttransfer.Columns.Add("GUID");                 //1
        dttransfer.Columns.Add("PRODUCTID");            //2
        dttransfer.Columns.Add("HSNCODE");              //3
        dttransfer.Columns.Add("PRODUCTNAME");          //4
        dttransfer.Columns.Add("BATCHNO");              //5
        dttransfer.Columns.Add("PACKINGSIZEID");        //6
        dttransfer.Columns.Add("PACKINGSIZENAME");      //7
        dttransfer.Columns.Add("TRANSFERQTY");          //8
        dttransfer.Columns.Add("MRP");                  //9
        dttransfer.Columns.Add("RATE");                 //10
        dttransfer.Columns.Add("AMOUNT");               //11
        dttransfer.Columns.Add("TOTALMRP");             //12
        dttransfer.Columns.Add("ASSESMENTPERCENTAGE");  //13
        dttransfer.Columns.Add("TOTALASSESMENTVALUE");  //14
        dttransfer.Columns.Add("NETWEIGHT");            //15
        dttransfer.Columns.Add("GROSSWEIGHT");          //16
        dttransfer.Columns.Add("MFDATE");               //17
        dttransfer.Columns.Add("EXPRDATE");             //18
        dttransfer.Columns.Add("TAG");                  //19
        dttransfer.Columns.Add("BEFOREEDITEDQTY");      //20

        #region Loop For Adding Itemwise Tax Component (Add By Rajeev)

        if (hdn_transferid.Value == "")
        {
            ClsFactoryStockTransfer_RMPM clsTrans = new ClsFactoryStockTransfer_RMPM();
            string flag = clsTrans.BindRegion_StockTransfer(this.ddltodepot.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.ToString().Trim());

            if (string.IsNullOrEmpty(flag))
            {
                dtTaxCount = clsTrans.ItemWiseTaxCount_StockTransfer(Request.QueryString["MENUID"].ToString(), "1", this.ddltodepot.SelectedValue.Trim(), this.ddlproductname.SelectedValue.Trim(), this.ddltodepot.SelectedValue.Trim(), this.txttransferdate.Text.Trim());
            }
            else
            {
                dtTaxCount = clsTrans.ItemWiseTaxCount_StockTransfer(Request.QueryString["MENUID"].ToString(), "0", this.ddltodepot.SelectedValue.Trim(), this.ddlproductname.SelectedValue.Trim(), this.ddltodepot.SelectedValue.Trim(), this.txttransferdate.Text.Trim());
            }
            Session["dtTransferTaxCount"] = dtTaxCount;

            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                dttransfer.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));//19
                dttransfer.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));        //20     
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string TransferID = Convert.ToString(hdn_transferid.Value).Trim();
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            ds = clsstocktransfer.EditStockTransferDetails(TransferID);
            Session["dtTransferTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dttransfer.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dttransfer.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        #endregion

        dttransfer.Columns.Add("NETAMOUNT");
        HttpContext.Current.Session["STOCKTRANSFERQTY"] = dttransfer;
        return dttransfer;
    }
    #endregion

    #region BindTransferDetailsGrid
    public void BindTransferDetailsGrid()
    {
        DataTable dttransfer = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
        if (dttransfer.Rows.Count > 0)
        {
            this.gvtransfer.DataSource = dttransfer;
            this.gvtransfer.DataBind();
        }
        else
        {
            this.gvtransfer.DataSource = null;
            this.gvtransfer.DataBind();
        }

    }
    #endregion

    #region LoadModeofTransport
    public void LoadModeofTransport()
    {
        this.ddlmodetransport.Items.Clear();
        this.ddlmodetransport.Items.Add(new ListItem("Select Transport Mode", "0"));
        this.ddlmodetransport.AppendDataBoundItems = true;
        this.ddlmodetransport.Items.Add(new ListItem("By Air", "By Air"));
        this.ddlmodetransport.Items.Add(new ListItem("By Rail", "By Rail"));
        this.ddlmodetransport.Items.Add(new ListItem("By Road", "By Road"));
        this.ddlmodetransport.Items.Add(new ListItem("By Ship", "By Ship"));
        this.ddlmodetransport.DataBind();
    }
    #endregion

    #region LoadSourceDepo
    public void LoadDepo()
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        string DepotID = string.Empty;
        DataTable dtSourceDepot = new DataTable();

        //if (Checker == "FALSE")
        //{
        dtSourceDepot = clsdeporeceived.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
        //}
        //else
        //{
        //    dtSourceDepot = clsdeporeceived.BindDepo();
        //    DepotID = this.hdnDepotID.Value.Trim();
        //}

        if (dtSourceDepot != null)
        {
            if (dtSourceDepot.Rows.Count > 0)
            {
                //if (Checker == "FALSE")
                //{
                this.ddlfromdepot.Items.Clear();
                this.ddlfromdepot.Items.Add(new ListItem("SELECT DEPOT NAME", "0"));
                this.ddlfromdepot.DataSource = dtSourceDepot;
                this.ddlfromdepot.DataValueField = "BRID";
                this.ddlfromdepot.DataTextField = "BRNAME";
                this.ddlfromdepot.DataBind();

                if (dtSourceDepot.Rows.Count == 1)
                {
                    this.ddlfromdepot.SelectedValue = Convert.ToString(dtSourceDepot.Rows[0]["BRID"]);
                }
                //}
                //else
                //{
                //    this.ddlfromdepot.Items.Clear();
                //    this.ddlfromdepot.Items.Add(new ListItem("SELECT DEPOT NAME", "0"));
                //    this.ddlfromdepot.DataSource = dtSourceDepot;
                //    this.ddlfromdepot.DataValueField = "BRID";
                //    this.ddlfromdepot.DataTextField = "BRNAME";
                //    this.ddlfromdepot.DataBind();
                //    //this.ddlfromdepot.SelectedValue = DepotID;
                //}
            }
        }
    }
    #endregion

    #region LoadTranspoter
    public void LoadTranspoter()
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        ddltranspoter.Items.Clear();
        ddltranspoter.Items.Add(new ListItem("SELECT TRANSPOTER NAME", "0"));
        ddltranspoter.AppendDataBoundItems = true;
        List<USP_GET_TRANSPORTER_BASED_ON_DEPOT_Result> ObjTransporter = new List<USP_GET_TRANSPORTER_BASED_ON_DEPOT_Result>();
        ddltranspoter.DataSource = ObjTransporter = clsstocktransfer.BindFactoryTransporter_edmx(this.ddlfromdepot.SelectedValue.Trim(), this.ddltranspoter.SelectedValue.Trim());
        ddltranspoter.DataValueField = "ID";
        ddltranspoter.DataTextField = "NAME";
        ddltranspoter.DataBind();
    }
    #endregion

    #region Load Recent Txn Transporter
    public void LoadRecentTxnTransporter(string FromDepotID, string ToDepotID, string FinYear, string ModuleID)
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        /*DataTable dtTxnTransporter = new DataTable();
        dtTxnTransporter = clsstocktransfer.BindRecentTxnTransporter(FromDepotID, ToDepotID, FinYear, ModuleID);*/
        List<USP_FETCH_RECENT_TXN_TRANSPORTER_FAC_Result> Obj = new List<USP_FETCH_RECENT_TXN_TRANSPORTER_FAC_Result>();
        Obj = clsstocktransfer.BindRecentTxnTransporter_edmx(FromDepotID, ToDepotID, FinYear, ModuleID);
        if (Obj.Count > 0)
        {
            foreach (var Item in Obj)
            {
                //this.ddltranspoter.SelectedValue = Convert.ToString(dtTxnTransporter.Rows[0]["ID"]).Trim();
                //this.txtShippingAddress.Text = Convert.ToString(dtTxnTransporter.Rows[0]["SHIPPINGADDRESS"]).Trim();
                this.ddltranspoter.SelectedValue = Convert.ToString(Item.ID).Trim();
                this.txtShippingAddress.Text = Convert.ToString(Item.SHIPPINGADDRESS).Trim();
            }
        }
    }
    #endregion

    #region LoadTranspoterChecker
    public void LoadTranspoterChecker()
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        ddltranspoter.Items.Clear();
        ddltranspoter.Items.Add(new ListItem("SELECT TRANSPOTER NAME", "0"));
        ddltranspoter.AppendDataBoundItems = true;
        ddltranspoter.DataSource = clsstocktransfer.BindTranspoterChecker();
        ddltranspoter.DataValueField = "ID";
        ddltranspoter.DataTextField = "NAME";
        ddltranspoter.DataBind();
    }
    #endregion

    #region Load Destination Depot
    public void FillDepot()
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        List<USP_BIND_TODEPOT_Result> ObjToDepot = new List<USP_BIND_TODEPOT_Result>();
        //DataTable dtToDepot = new DataTable();
        //dtToDepot = clsstocktransfer.BindToDepo(ddlfromdepot.SelectedValue.Trim());
        ObjToDepot = clsstocktransfer.BindToDepo_edmx(ddlfromdepot.SelectedValue.Trim());
        if (ObjToDepot.Count > 0)
        {
            this.ddltodepot.Items.Clear();
            this.ddltodepot.Items.Add(new ListItem("SELECT DEPOT NAME", "0"));
            this.ddltodepot.AppendDataBoundItems = true;
            this.ddltodepot.DataSource = ObjToDepot;
            this.ddltodepot.DataValueField = "BRID";
            this.ddltodepot.DataTextField = "BRNAME";
            this.ddltodepot.DataBind();
        }
    }
    #endregion

    #region FFormRequired
    public int FFormRequired(string FromDepotID, string ToDepotID)
    {
        int req = 0;
        try
        {
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            req = clsstocktransfer.FFORM(FromDepotID, ToDepotID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return req;
    }
    #endregion

    #region LoadCategory
    private void LoadCategory()
    {
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
        DataTable dtCategoryCache = new DataTable();

        dtCategoryCache = clsstocktransfer.BindCATEGORY();

        if (dtCategoryCache.Rows.Count > 0)
        {
            this.ddlcategory.Items.Clear();
            this.ddlcategory.Items.Add(new ListItem("--- ALL ---", "0"));
            this.ddlcategory.AppendDataBoundItems = true;
            this.ddlcategory.DataSource = dtCategoryCache;
            this.ddlcategory.DataValueField = "CATID";
            this.ddlcategory.DataTextField = "CATNAME";
            this.ddlcategory.DataBind();
        }
    }
    #endregion

    #region LoadProduct
    private void LoadProduct()
    {
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
        DataTable dtproduct = new DataTable();
        dtproduct = clsstocktransfer.BindProduct(ddlfromdepot.SelectedValue.ToString().Trim());
        if (dtproduct.Rows.Count > 0)
        {
            this.ddlproductname.Items.Clear();
            this.ddlproductname.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
            this.ddlproductname.AppendDataBoundItems = true;
            this.ddlproductname.DataSource = dtproduct;
            this.ddlproductname.DataValueField = "PRODUCTID";
            this.ddlproductname.DataTextField = "PRODUCTNAME";
            this.ddlproductname.DataBind();
        }
        HttpContext.Current.Session["PRODUCTDETAILS"] = dtproduct;
    }
    #endregion

    #region LoadCategoryWiseProduct
    private void LoadCategoryWiseProduct()
    {
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
        DataTable dtproduct = new DataTable();
        this.ddlproductname.Items.Clear();
        dtproduct = clsstocktransfer.BindCategoryProduct(this.ddlcategory.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim());
        if (dtproduct != null)
        {
            DataTable dtProductClone = new DataTable();
            DataView dvProductClone = new DataView(dtproduct);
            dvProductClone.RowFilter = "CATID = '" + this.ddlcategory.SelectedValue.Trim() + "'";
            if (dtproduct.Rows.Count > 0)
            {
                this.ddlproductname.Items.Clear();
                this.ddlproductname.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
                this.ddlproductname.AppendDataBoundItems = true;
                this.ddlproductname.DataSource = dvProductClone;
                this.ddlproductname.DataValueField = "PRODUCTID";
                this.ddlproductname.DataTextField = "PRODUCTNAME";
                this.ddlproductname.DataBind();
            }
        }
        HttpContext.Current.Session["PRODUCTDETAILS"] = dtproduct;
    }
    #endregion

    #region MotherDepoChange
    public void MotherDepoChange()
    {
        try
        {
            if (this.ddlcategory.SelectedValue == "0")
            {
                LoadProduct();
            }
            else if (this.ddlcategory.SelectedValue != "0")
            {
                LoadCategoryWiseProduct();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btngrdUpdateWaybill_Click
    protected void btngrdUpdateWaybill_Click(object sender, EventArgs e)
    {
        string despatchID = Convert.ToString(hdn_stocktransferid.Value).Trim();
        this.txtWaybillUpdate.Text = hdnWaybillNo.Value.ToString();
        this.txtWaybillkey.Text = this.hdnWaybillKey.Value.Trim();
        this.light.Style["display"] = "block";
        this.fade.Style["display"] = "block";

    }
    #endregion

    #region btnCloseLightbox_Click
    protected void btnCloseLightbox_Click(object sender, EventArgs e)
    {
        this.light.Style["display"] = "none";
        this.fade.Style["display"] = "none";
    }
    #endregion

    #region btnWaybillUpdate_Click
    protected void btnWaybillUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            int flag = 0;
            string transferid = Convert.ToString(hdn_transferid.Value).Trim();
            flag = clsstocktransfer.UpdateWaybillNo(transferid, this.txtWaybillUpdate.Text.Trim(), this.txtWaybillkey.Text.Trim());
            this.hdn_transferid.Value = "";

            if (flag == 1)
            {
                this.light.Style["display"] = "none";
                this.fade.Style["display"] = "none";
                this.LoadDispatchDetails();
                MessageBox1.ShowSuccess("<b><font color='green'> Waybill No. Updated Successfully</font></b>", 80, 500);
            }
            else if (flag == 0)
            {
                MessageBox1.ShowError("<b><font color='red'> Error saving record..</font></b>");
            }
            else if (flag == 2)
            {
                MessageBox1.ShowInfo("Waybill Key:<b>" + this.txtWaybillkey.Text.Trim() + " is not in waybill inventory</b>", 80, 500);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region ddltodepot_SelectedIndexChanged
    protected void ddltodepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddltodepot.SelectedValue.Trim() == "0")
            {
                if (this.ddlwaybillapplicale.SelectedValue == "0")
                {
                    this.ddlwaybill.Items.Clear();
                    this.ddlwaybill.Items.Add(new ListItem("NOT APPLICABLE", "0"));
                    this.ddlwaybill.AppendDataBoundItems = true;
                }
                else
                {
                    this.ddlwaybill.Items.Clear();
                    this.ddlwaybill.Items.Add(new ListItem("SELECT EWAYBILL KEY", "0"));
                    this.ddlwaybill.AppendDataBoundItems = true;
                }
            }
            else
            {
                this.LoadRecentTxnTransporter(this.ddlfromdepot.SelectedValue.Trim(),
                                                this.ddltodepot.SelectedValue.Trim(),
                                                Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim(),
                                                "10"
                                             );
                if (this.ddlwaybillapplicale.SelectedValue.Trim() == "0")
                {
                    this.ddlwaybill.Items.Clear();
                    this.ddlwaybill.Items.Add(new ListItem("NOT APPLICABLE", "0"));
                    this.ddlwaybill.AppendDataBoundItems = true;
                }
                else
                {
                    this.FillWayBillNo(hdn_transferid.Value);
                    this.BindTransferGrid();
                    this.CreateDataTableTaxComponent();//Fro Tax Calculation Method
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlwaybillapplicale.ClientID + "').focus(); ", true);
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

    #region FillWayBillNo
    public void FillWayBillNo(string transferid)
    {
        ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
        if (this.hdn_transferid.Value == "")
        {
            ddlwaybill.Items.Clear();
            ddlwaybill.Items.Add(new ListItem("SELECT EWAYBILL KEY", "0"));
            ddlwaybill.AppendDataBoundItems = true;
            ddlwaybill.DataSource = clsstocktransfer.BindWayBillNo(ddltodepot.SelectedValue.ToString(), transferid);
            ddlwaybill.DataValueField = "WAYBILLNO";
            ddlwaybill.DataTextField = "WAYBILLNO";
            ddlwaybill.DataBind();
        }
        else
        {
            ddlwaybill.Items.Clear();
            ddlwaybill.Items.Add(new ListItem("SELECT EWAYBILL KEY", "0"));
            ddlwaybill.AppendDataBoundItems = true;
            ddlwaybill.DataSource = clsstocktransfer.BindWayBillNoEdit(ddltodepot.SelectedValue.ToString(), transferid);
            ddlwaybill.DataValueField = "WAYBILLNO";
            ddlwaybill.DataTextField = "WAYBILLNO";
            ddlwaybill.DataBind();
        }
    }
    #endregion

    #region ddlwaybillapplicale_SelectedIndexChanged
    protected void ddlwaybillapplicale_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlwaybillapplicale.SelectedValue == "0")
            {
                this.ddlwaybill.Items.Clear();
                this.ddlwaybill.Items.Add(new ListItem("NOT APPLICABLE", "0"));
                this.ddlwaybill.Enabled = false;
            }
            else
            {
                this.ddlwaybill.Enabled = true;
                FillWayBillNo(hdn_transferid.Value);
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlinsurancecompname.ClientID + "').focus(); ", true);

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadProductWisePacksize
    public void LoadProductWisePacksize(string ProductID)
    {
        try
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            DataTable dtPackSize = new DataTable();
            this.ddlpackingsize.Items.Clear();
            dtPackSize = clsstocktransfer.BindPackSize(ProductID);
            if (dtPackSize.Rows.Count > 0)
            {
                this.ddlpackingsize.DataSource = dtPackSize;
                this.ddlpackingsize.DataTextField = "PSNAME";
                this.ddlpackingsize.DataValueField = "PSID";
                this.ddlpackingsize.DataBind();
            }
            else
            {
                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("SELECT PACKSIZE", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadBatchDetails
    public void LoadBatchDetails(string ProductID, string PacksizeID, string DepotID, string BatchNo)
    {
        try
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            DataTable dtBatch = new DataTable();
            DataTable dtBaseCost = new DataTable();
            dtBatch = clsstocktransfer.BindBatchDetails(DepotID, ProductID, PacksizeID, BatchNo);

            List<StockBatchdetails> BatchDetails = new List<StockBatchdetails>();
            if (dtBatch.Rows.Count > 0)
            {
                if (dtBatch.Rows.Count > 1)
                {
                    this.ddlbatchno.Items.Clear();
                    this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));

                    for (int i = 0; i < dtBatch.Rows.Count; i++)
                    {
                        StockBatchdetails po = new StockBatchdetails();
                        //po.BATCHNO = dtBatch.Rows[i]["BATCHNO"].ToString();
                        po.STOCKQTY = dtBatch.Rows[i]["INVOICESTOCKQTY"].ToString();
                        //po.ID = dtBatch.Rows[i]["BATCHNO"].ToString() + '|' + dtBatch.Rows[i]["MRP"].ToString() + dtBatch.Rows[i]["MFGDATE"].ToString() + dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        po.ID = dtBatch.Rows[i]["MRP"].ToString().ToString();
                        po.MRP = dtBatch.Rows[i]["MRP"].ToString();
                        //po.ASSESMENTPERCENTAGE = dtBatch.Rows[i]["ASSESMENTPERCENTAGE"].ToString();
                        //po.MFDATE = dtBatch.Rows[i]["MFGDATE"].ToString();
                        //po.EXPRDATE = dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        BatchDetails.Add(po);
                    }
                    //string text1 = string.Format("{0}{1}{2}{3}{4}{5}",
                    string text1 = string.Format("{0}{1}",
                    "STOCKQTY".PadRight(15, '\u00A0'),
                    "MRP".PadRight(11, '\u00A0')
                    /*"ASSESMENT(%)".PadRight(14, '\u00A0'),
                    "MFG DATE".PadRight(14, '\u00A0'),
                    "EXP DATE".PadRight(14, '\u00A0'),
                    "BATCHNO".PadRight(10, '\u00A0')*/
                    );

                    ddlbatchno.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlbatchno.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                        }
                    }

                    foreach (StockBatchdetails p in BatchDetails)
                    {
                        //string text = string.Format("{0}{1}{2}{3}{4}{5}",
                        string text = string.Format("{0}{1}",
                            p.STOCKQTY.PadRight(15, '\u00A0'),
                            p.MRP.PadRight(11, '\u00A0')
                            /*p.ASSESMENTPERCENTAGE.PadRight(14, '\u00A0'),
                            p.MFDATE.PadRight(14, '\u00A0'),
                            p.EXPRDATE.PadRight(14, '\u00A0'),
                            p.BATCHNO.PadRight(10, '\u00A0')*/
                            );
                        ddlbatchno.Items.Add(new ListItem(text, "" + p.ID + ""));
                    }
                }
                else if (dtBatch.Rows.Count == 1)
                {
                    this.ddlbatchno.Items.Clear();

                    for (int i = 0; i < dtBatch.Rows.Count; i++)
                    {
                        StockBatchdetails po = new StockBatchdetails();
                        //po.BATCHNO = dtBatch.Rows[i]["BATCHNO"].ToString();
                        po.STOCKQTY = dtBatch.Rows[i]["INVOICESTOCKQTY"].ToString();
                        //po.ID = dtBatch.Rows[i]["BATCHNO"].ToString() + '|' + dtBatch.Rows[i]["MRP"].ToString() + dtBatch.Rows[i]["MFGDATE"].ToString() + dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        po.ID = dtBatch.Rows[i]["MRP"].ToString();
                        po.MRP = dtBatch.Rows[i]["MRP"].ToString();
                        //po.ASSESMENTPERCENTAGE = dtBatch.Rows[i]["ASSESMENTPERCENTAGE"].ToString();
                        //po.MFDATE = dtBatch.Rows[i]["MFGDATE"].ToString();
                        //po.EXPRDATE = dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        BatchDetails.Add(po);
                    }
                    //string text1 = string.Format("{0}{1}{2}{3}{4}{5}",
                    string text1 = string.Format("{0}{1}",
                    "STOCKQTY".PadRight(15, '\u00A0'),
                    "MRP".PadRight(11, '\u00A0')
                    /*"ASSESMENT(%)".PadRight(14, '\u00A0'),
                    "MFG DATE".PadRight(14, '\u00A0'),
                    "EXP DATE".PadRight(14, '\u00A0'),
                    "BATCHNO".PadRight(10, '\u00A0')*/
                    );

                    ddlbatchno.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlbatchno.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                        }
                    }

                    foreach (StockBatchdetails p in BatchDetails)
                    {
                        //string text = string.Format("{0}{1}{2}{3}{4}{5}",
                           string text = string.Format("{0}{1}",
                            p.STOCKQTY.PadRight(15, '\u00A0'),
                            p.MRP.PadRight(11, '\u00A0')
                            /*p.ASSESMENTPERCENTAGE.PadRight(14, '\u00A0'),
                            p.MFDATE.PadRight(14, '\u00A0'),
                            p.EXPRDATE.PadRight(14, '\u00A0'),
                            p.BATCHNO.PadRight(10, '\u00A0')*/
                            );
                        ddlbatchno.Items.Add(new ListItem(text, "" + p.ID + ""));
                    }
                    //this.ddlbatchno.SelectedValue = Convert.ToString(dtBatch.Rows[0]["BATCHNO"].ToString() + '|' + dtBatch.Rows[0]["MRP"].ToString() + dtBatch.Rows[0]["MFGDATE"].ToString() + dtBatch.Rows[0]["EXPIRDATE"].ToString()).Trim();
                    this.ddlbatchno.SelectedValue = Convert.ToString(dtBatch.Rows[0]["MRP"].ToString());
                    if (this.ddlbatchno.SelectedValue.Trim() != "0")
                    {
                        this.BatchDetails();
                    }
                }
            }
            else
            {
                this.ddlbatchno.Items.Clear();
                this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                MessageBox1.ShowInfo("<b>Stock not available!</b>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ddlcategory_SelectedChanged
    protected void ddlcategory_SelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlcategory.SelectedValue != "0")
            {
                LoadCategoryWiseProduct();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlproductname.ClientID + "').focus(); ", true);
            }
            else if (this.ddlcategory.SelectedValue == "0")
            {
                LoadProduct();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlproductname.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlproductname_SelectedChanged
    protected void ddlproductname_SelectedChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable hscode = new DataTable();
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            if (ddlproductname.SelectedValue == "0")
            {
                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                ddlbatchno.AppendDataBoundItems = true;
                txtstockqty.Text = "";
                txttransferqty.Text = "";
            }
            else
            {
                if (this.ddlproductname.SelectedValue != "0")
                {
                    #region Retrive HSN Code After Product Selection(Rajeev)
                    /*ViewState["HSNCODE"] = null;
                    hscode = clsstocktransfer.GETHSNCODE(ddlproductname.SelectedValue.Trim());
                    ViewState["HSNCODE"] = hscode.Rows[0]["HSN"].ToString().Trim();*/
                    #endregion
                    LoadProductWisePacksize(this.ddlproductname.SelectedValue.Trim());
                    this.LoadBatchDetails(this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim(), "0");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlpackingsize.ClientID + "').focus(); ", true);
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

    #region ddlpackingsize_SelectedIndexChanged
    protected void ddlpackingsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            if (ddlpackingsize.SelectedValue == "0")
            {
                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                ddlbatchno.AppendDataBoundItems = true;
                txtstockqty.Text = "";
            }
            else
            {
                this.LoadBatchDetails(this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim(), "0");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlbatchno.ClientID + "').focus(); ", true);

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BatchDetails
    protected void BatchDetails()
    {
        decimal Rate = 0;
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
        if (this.ddlproductname.SelectedValue != "0" && this.ddlpackingsize.SelectedValue != "0")
        {
            if (this.ddlbatchno.SelectedValue != "0")
            {
                this.txtstockqty.Text = ddlbatchno.SelectedItem.Text.Substring(0, 15).Trim();
                this.hdnmrp.Value = ddlbatchno.SelectedItem.Text.Substring(15, 11).Trim();
                this.txtmrp.Text = ddlbatchno.SelectedItem.Text.Substring(15, 11).Trim();
                //this.hdn_ASSESMENTPERCENTAGE.Value = ddlbatchno.SelectedItem.Text.Substring(26, 14).Trim();
                Rate = clsstocktransfer.BindDepotRate(this.ddlproductname.SelectedValue.Trim(), Convert.ToDecimal(this.txtmrp.Text.Trim()), this.txtchallandate.Text.Trim());
                this.hdnrate.Value = Convert.ToString(Rate);
                /*this.hdn_mfgdate.Value = ddlbatchno.SelectedItem.Text.Substring(40, 14).Trim();
                this.hdn_exprdate.Value = ddlbatchno.SelectedItem.Text.Substring(54, 14).Trim();*/
            }
            else
            {
                this.hdnmrp.Value = "";
                this.txtmrp.Text = "";
                this.txtstockqty.Text = "0";
                this.hdn_ASSESMENTPERCENTAGE.Value = "";
                this.hdnrate.Value = "";
                this.hdn_mfgdate.Value = "";
                this.hdn_exprdate.Value = "";
            }
        }
        else
        {
            MessageBox1.ShowInfo("Please select Product and Packsize");
        }
    }
    #endregion

    #region ddlbatchno_SelectedIndexChanged
    protected void ddlbatchno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            if (ddlbatchno.SelectedValue == "0")
            {
                txtstockqty.Text = "";
            }
            else
            {
                this.BatchDetails();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txttransferqty.ClientID + "').focus(); ", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btnadd.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CalculateTotalCasePack
    decimal CalculateTotalCasePack(DataTable dt)
    {
        decimal TotalCase = 0;
        try
        {
            ClsFactoryStockTransfer clsTransfer = new ClsFactoryStockTransfer();
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                TotalCase += clsTransfer.GetPackingSize_OnCallCase(Convert.ToString(dt.Rows[Counter]["PRODUCTID"]),
                                                                    Convert.ToString(dt.Rows[Counter]["PACKINGSIZEID"]),
                                                                    Convert.ToDecimal(dt.Rows[Counter]["TRANSFERQTY"]));

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return TotalCase;
    }
    #endregion

    #region CalculateTotalPCS
    decimal CalculateTotalPCS(DataTable dt)
    {
        decimal TotalPCS = 0;
        try
        {
            ClsFactoryStockTransfer clsTransfer = new ClsFactoryStockTransfer();
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                TotalPCS += clsTransfer.GetPackingSize_OnCallPCS(Convert.ToString(dt.Rows[Counter]["PRODUCTID"]),
                                                                  Convert.ToString(dt.Rows[Counter]["PACKINGSIZEID"]),
                                                                  Convert.ToDecimal(dt.Rows[Counter]["TRANSFERQTY"]));

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return TotalPCS;
    }
    #endregion

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadStore();
            if (ddltodepot.SelectedValue.Trim() == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Please Select Depot</font></b>!");
            }
            else
            {
                ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
                //clsSaleInvoice clsInvc = new clsSaleInvoice();
                DataTable dttransfer = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
                         
                int flag = 0;
                decimal remainingqty = 0;
                decimal TotalAmount = 0;
                string TAXID = string.Empty;
                decimal ProductWiseTax = 0;
                decimal TotalTax = 0;
                string HSNCODE = string.Empty;

                string pid = ddlproductname.SelectedValue.ToString().Trim();
                //string pname = Convert.ToString(this.ddlproductname.SelectedItem.ToString().Substring(0, this.ddlproductname.SelectedItem.ToString().IndexOf("~"))).Trim();
                string pname = this.ddlproductname.SelectedItem.ToString().Trim();
                String[] batch = ddlbatchno.SelectedValue.ToString().Split('|');
                string batchno = batch[0].Trim();
                string packsizeid = ddlpackingsize.SelectedValue.ToString();
                string packname = ddlpackingsize.SelectedItem.Text;
                decimal transferqty = Convert.ToDecimal(txttransferqty.Text.Trim());
                decimal stockqty = Convert.ToDecimal(txtstockqty.Text.Trim());

                if (dttransfer.Rows.Count > 0)
                {
                    int NumberofRecord = dttransfer.Select("PRODUCTID='" + pid + "' AND PRODUCTNAME='" + pname + "' AND BATCHNO='" + batchno + "'").Length;
                    if (NumberofRecord > 0)
                    {
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    remainingqty = (stockqty - transferqty);
                    if (remainingqty >= 0 && transferqty != 0)
                    {
                        //decimal TotAssesment = (clsstocktransfer.CalculateAmountInPcs(pid, packsizeid, transferqty, Convert.ToDecimal(hdnmrp.Value)) * Convert.ToDecimal(hdn_ASSESMENTPERCENTAGE.Value) / 100);
                        decimal PcsQty = Convert.ToDecimal(txttransferqty.Text);//clsstocktransfer.FetchQtyInPcs(pid, packsizeid, transferqty);
                        DataSet dtweight = clsstocktransfer.GetWeight(this.ddlproductname.SelectedValue, this.ddlpackingsize.SelectedValue, Convert.ToDecimal(transferqty));
                        HSNCODE = clsstocktransfer.HSNCode(pid);

                        DataRow dr = dttransfer.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["PRODUCTID"] = pid;
                        dr["HSNCODE"] = HSNCODE.Trim();
                        dr["PRODUCTNAME"] = pname;
                        dr["BATCHNO"] = batchno;
                        dr["PACKINGSIZEID"] = packsizeid;
                        dr["PACKINGSIZENAME"] = packname;
                        dr["TRANSFERQTY"] = transferqty;
                        dr["MRP"] = hdnmrp.Value.ToString();
                        dr["RATE"] = hdnrate.Value.Trim();
                        string amount = string.Empty;
                        amount = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(hdnrate.Value.Trim()) * PcsQty));
                        dr["AMOUNT"] = Convert.ToDecimal(amount);
                        dr["TOTALMRP"] = Convert.ToDecimal(hdnmrp.Value) * PcsQty;
                        dr["ASSESMENTPERCENTAGE"] = hdn_ASSESMENTPERCENTAGE.Value.ToString() == "" ? "0" : hdn_ASSESMENTPERCENTAGE.Value.ToString();
                        dr["TOTALASSESMENTVALUE"] = 0;

                        if (dtweight.Tables[0].Rows.Count > 0)
                        {
                            dr["NETWEIGHT"] = dtweight.Tables[0].Rows[0]["NETWEIGHT"].ToString();
                        }
                        else
                        {
                            dr["NETWEIGHT"] = "0";
                        }

                        if (dtweight.Tables[1].Rows.Count > 0)
                        {
                            dr["GROSSWEIGHT"] = dtweight.Tables[1].Rows[0]["GROSSWEIGHT"].ToString();
                        }
                        else
                        {
                            dr["GROSSWEIGHT"] = "0";
                        }
                        dr["MFDATE"] = hdn_mfgdate.Value.Trim();
                        dr["EXPRDATE"] = hdn_exprdate.Value.Trim();

                        if (hdn_transferid.Value.Trim() == "")
                        {
                            dr["TAG"] = "A";
                        }
                        else
                        {
                            dr["TAG"] = "U";
                        }
                        dr["BEFOREEDITEDQTY"] = 0;

                        decimal BillTaxAmt = 0;

                        #region Loop For Adding Itemwise Tax Component
                        DataTable dtTaxCountDataAddition = (DataTable)Session["dtTransferTaxCount"];
                        ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                            {
                                case "1":

                                    TAXID = clsstocktransfer.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = clsstocktransfer.GetHSNTax(TAXID, Convert.ToString(this.ddlproductname.SelectedValue).Trim(), this.txttransferdate.Text.Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            CreateTaxDatatable("0",
                                               Convert.ToString(pid).Trim(),
                                               Convert.ToString(batchno).Trim(),
                                               dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                               Convert.ToString(ProductWiseTax),
                                               dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                               TAXID);
                        }
                        #endregion

                        dr["NETAMOUNT"] = BillTaxAmt + Convert.ToDecimal(Convert.ToString(String.Format("{0:0.00}", amount)));

                        dttransfer.Rows.Add(dr);
                        BindTransferDetailsGrid();

                        if (dttransfer.Rows.Count > 0)
                        {
                            this.txtTotalCase.Text = Convert.ToString(String.Format("{0:0.00}", CalculateTotalCasePack(dttransfer)));
                            this.txtTotalPCS.Text = Convert.ToString(String.Format("{0:0.00}", CalculateTotalPCS(dttransfer)));
                        }
                        else
                        {
                            this.txtTotalCase.Text = "0";
                            this.txtTotalPCS.Text = "0";
                        }

                        for (int i = 0; i < dttransfer.Rows.Count; i++)
                        {
                            TotalAmount = Convert.ToDecimal(dttransfer.Rows[i]["AMOUNT"].ToString().Trim()) + TotalAmount;
                        }
                        TotalTax = CalculateTaxTotal(dttransfer);
                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount + TotalTax));
                        this.txttaxamt.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                        this.txtnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtnetamt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim())));

                        if (this.gvtransfer.Rows.Count > 0)
                        {
                            this.BillingGridCalculation();
                        }
                        this.txttransferqty.Text = "0.00";
                        this.txtmrp.Text = "";
                        this.txtstockqty.Text = "";
                        this.ddlproductname.SelectedValue = "0";
                        this.ddlbatchno.Items.Clear();
                        this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                        this.hdn_ASSESMENTPERCENTAGE.Value = "";
                        this.hdn_mfgdate.Value = "";
                        this.hdn_exprdate.Value = "";
                        
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Stock not available, please check</font></b>!");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Product <b><font color='green'>" + pname + "</font></b> exists with same <b><font color='red'>BatchNo</font></b>!", 80, 650);
                }

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlproductname.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();

        if (hdn_transferid.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"] = dt;
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
            HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"] = dt;
        }
        return dt;
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string SALEORDERID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string TAXID)
    {
        DataTable dt = (DataTable)Session["TRANSFERTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["SALEORDERID"] = SALEORDERID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dt.Rows.Add(dr);
        dt.AcceptChanges();


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

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            string stocktransferno = string.Empty;
            string mode = string.Empty;
            string f_formactive = string.Empty;
            /*string OrderTypeID = "9A555D40-5E12-4F5C-8EE0-E085B5BAB169";
            string OrderTypeName = "GENERAL";*/
            string CountryID = "8F35D2B4-417C-406A-AD7F-7CB34F4D0B35";
            string CountryName = "INDIA";
            string AllocationID = "0";
            string AllocationNo = "NA";
            decimal TotalCase = 0;
            decimal TotalPCS = 0;
            string xmlTax = string.Empty;

            DataTable dttransfer = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
            DataTable dtTaxRecordsCheck = (DataTable)HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"];
            if (dttransfer.Rows.Count > 0)
            {
                if (this.chkActive.Checked == true)
                {
                    f_formactive = "Y";
                }
                else
                {
                    f_formactive = "N";
                }

                if (this.txtTotalCase.Text.Trim() != "")
                {
                    TotalCase = Convert.ToDecimal(this.txtTotalCase.Text.Trim());
                }
                if (this.txtTotalPCS.Text.Trim() != "")
                {
                    TotalPCS = Convert.ToDecimal(this.txtTotalPCS.Text.Trim());
                }

                int dtdespatch = Convert.ToInt32(Conver_To_ISO(this.txttransferdate.Text.Trim()));
                int dtInvoice = Convert.ToInt32(Conver_To_ISO(this.txtchallandate.Text.Trim()));
                int dtLRGR = Convert.ToInt32(Conver_To_ISO(this.txtlrgrdate.Text.Trim()));

                string INVOICE = clsstocktransfer.CheckInvoiceNo(this.txtchallanno.Text.Trim(), this.hdn_transferid.Value.Trim(), this.ddlfromdepot.SelectedValue.ToString().Trim());

                if (INVOICE == "1")
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Challan No already exists..!</font></b>");
                    return;
                }
                else
                {
                    xmlTax = ConvertDatatableToXMLItemWiseTaxDetails(dtTaxRecordsCheck);
                    string xml = ConvertDatatableToXML(dttransfer);
                    MENUID = Request.QueryString["MENUID"].ToString().Trim();

                    stocktransferno = clsstocktransfer.InsertFactoryDespatch(Convert.ToString(hdn_transferid.Value).Trim(), this.txtchallandate.Text.Trim(),
                                                                                this.ddltodepot.SelectedValue.Trim(), this.ddltodepot.SelectedItem.Text.Trim(),
                                                                                this.ddlwaybill.SelectedValue.ToString().Trim(), this.txtchallanno.Text.Trim(),
                                                                                this.txttransferdate.Text.Trim(), this.ddltranspoter.SelectedValue.Trim(),
                                                                                this.txtvehicleno.Text.Trim(), this.ddlfromdepot.SelectedValue.Trim(),
                                                                                this.ddlfromdepot.SelectedItem.Text.Trim(), this.txtlrgrno.Text.Trim(),
                                                                                this.txtlrgrdate.Text.Trim(), this.ddlmodetransport.SelectedValue.Trim(),
                                                                                Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString().Trim()),
                                                                                Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim(),
                                                                                this.txtRemarks.Text.Trim(), Convert.ToDecimal(this.txtnetamt.Text.Trim()), 0, 0,
                                                                                Convert.ToDecimal(txtRoundoff.Text.Trim()), "", "", "01/01/1900",
                                                                                this.txtgatepassno.Text.Trim(), this.txtgatepassdate.Text.Trim(),
                                                                                f_formactive, xml, xmlTax, "", this.ddlinsurancecompname.SelectedValue.Trim(),
                                                                                this.ddlinsurancecompname.SelectedItem.ToString().Trim(),
                                                                                this.ddlinsuranceno.SelectedValue.Trim(), TotalCase, TotalPCS,
                                                                                "N", CountryID, CountryName, AllocationID, AllocationNo,
                                                                                Convert.ToString(ViewState["Invoice_Type"]).Trim(),
                                                                                txtShippingAddress.Text, MENUID,this.ddlstorelocation.SelectedValue.Trim()
                                                                                );
                }

                if (stocktransferno != "")
                {
                    string finyear = Session["FINYEAR"].ToString();
                    string motherdepotid = ddlfromdepot.SelectedValue.ToString();
                    string motherdepotname = ddlfromdepot.SelectedItem.Text;

                    if (hdn_transferid.Value == "")
                    {
                        mode = "A";
                    }
                    else
                    {
                        mode = "E";
                    }
                    if (Convert.ToString(hdn_transferid.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Dispatch No: <b><font color='green'>" + stocktransferno + "</font></b> Saved Successfully!", 60, 550);
                        txttransferno.Text = stocktransferno;
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Dispatch No: <b><font color='green'>" + stocktransferno + "</font></b> Updated Successfully!", 60, 550);

                        txttransferno.Text = stocktransferno;
                    }

                    this.btnnewentry.Visible = true;
                    this.btnback.Visible = false;
                    this.gvtransfer.DataSource = null;
                    this.gvtransfer.DataBind();
                    this.LoadDispatchDetails();
                    dttransfer.Clear();
                    this.ResetTable();
                    this.pnlAdd.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.ClearControls();
                    this.ddlinsurancecompname.SelectedValue = "0";
                    this.ddlinsuranceno.SelectedValue = "0";
                    this.btnadd.Visible = false;
                    this.divbtnsave.Visible = false;
                    this.btncancel.Visible = false;
                    this.pnlAdd.Enabled = false;
                    this.hdn_transferid.Value = "";
                    this.hdn_stocktransferid.Value = "";
                    this.hdn_guid.Value = "";
                    this.btnaddhide.Style["display"] = "";
                    this.txtTotalCase.Text = "";
                    this.txtTotalPCS.Text = "";
                    this.ddlcategory.SelectedValue = "0";
                }
                else
                {
                    MessageBox1.ShowError("Error on Saving record..");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please add atleast 1 record!");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete StockTransfer
    protected void DeleteStockTransfer(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsFactoryStockTransfer clsstocktransfer = new ClsFactoryStockTransfer();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                e.Record["Error"] = "Delete not allowed..!";
            }
            else
            {
                string DispatchID = Convert.ToString(e.Record["STOCKTRANSFERID"]).Trim();
                if (clsstocktransfer.GetFinancestatus(DispatchID) == "1")
                {
                    e.Record["Error"] = "Finance Posting already done,not allow to delete.";
                    return;
                }
                if (clsstocktransfer.GetDespatchstatus(DispatchID) == "1")
                {
                    e.Record["Error"] = "Stock Received already done,not allow to delete.";
                    return;
                }

                int flag = 0;
                flag = clsstocktransfer.DeleteFactoryDispatch(e.Record["STOCKTRANSFERID"].ToString());
                this.hdn_transferid.Value = "";

                if (flag == 1)
                {
                    this.LoadDispatchDetails();
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

    #region ResetTable
    public void ResetTable()
    {
        Session.Remove("STOCKTRANSFERQTY");
        Session.Remove("TRANSFERTAXCOMPONENTDETAILS");
    }
    #endregion

    #region ClearControls
    public void ClearControls()
    {
        this.ddlwaybill.SelectedValue = "0";
        this.ddltranspoter.SelectedValue = "0";
        this.ddlmodetransport.SelectedValue = "By Road";
        this.txtvehicleno.Text = "";
        this.txtlrgrno.Text = "";
        this.txtchallanno.Text = "";
        this.txtgatepassno.Text = "";
        DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        //this.txttransferdate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtlrgrdate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtchallandate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtgatepassdate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.ddltodepot.SelectedValue = "0";
        this.hdn_ASSESMENTPERCENTAGE.Value = "";
        this.hdn_mfgdate.Value = "";
        this.hdn_exprdate.Value = "";
        this.txtRemarks.Text = "";
        this.txtmrp.Text = "";
        this.txtbasicamt.Text = "";
        this.txttaxamt.Text = "";
        this.txttotal.Text = "";
        this.txtTotalGross.Text = "";
        this.txtRoundoff.Text = "";
        this.txtnetamt.Text = "";
        this.txtTotalCase.Text = "";
        this.txtTotalPCS.Text = "";
        this.ddlbatchno.Items.Clear();
        this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
        this.BindInsurenceCompany();
        this.txtShippingAddress.Text = "";
    }
    #endregion

    #region btnSTfind_Click
    protected void btnSTfind_Click(object sender, EventArgs e)
    {
        try
        {
            LoadDispatchDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadDispatchDetails
    public void LoadDispatchDetails()
    {
        try
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.gvStockTransferDetails.DataSource = clsstocktransfer.BindFactoryDispatch(this.txtfromdateins.Text.Trim(), this.txttodateins.Text.Trim(),
                                                                                           Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim(),
                                                                                           Checker, Convert.ToString(HttpContext.Current.Session["IUserID"]).Trim(),
                                                                                           Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim());


            this.gvStockTransferDetails.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdn_transferid.Value = "";
            this.btnnewentry.Visible = true;
            this.btnback.Visible = false;
            this.gvtransfer.DataSource = null;
            this.gvtransfer.DataBind();
            LoadDispatchDetails();
            ResetTable();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            this.ClearControls();
            this.ddlinsuranceno.SelectedValue = "0";
            this.ddlinsurancecompname.SelectedValue = "0";
            this.txtTotalGross.Text = "";
            this.txtTotalCase.Text = "";
            this.txtTotalPCS.Text = "";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = true;
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

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdn_transferid.Value = "";
            this.btnaddhide.Style["display"] = "";
            this.btnnewentry.Visible = true;
            this.btnback.Visible = false;
            this.gvtransfer.DataSource = null;
            this.gvtransfer.DataBind();
            this.LoadDispatchDetails();
            this.ResetTable();
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.ClearControls();
            this.ddlinsuranceno.SelectedValue = "0";
            this.ddlinsurancecompname.SelectedValue = "0";
            this.txtTotalGross.Text = "";
            this.txtTotalCase.Text = "";
            this.txtTotalPCS.Text = "";
            this.ddlcategory.SelectedValue = "0";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = true;
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

    #region btnnewentry_Click
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            LoadStore();
            this.hdn_transferid.Value = "";
            this.ddlwaybillapplicale.SelectedValue = "1";
            this.trtransferid.Style["display"] = "none";
            this.imgPopuppodate.Visible = true;
            this.btnnewentry.Visible = false;
            this.btnback.Visible = true;
            this.pnlAdd.Enabled = true;
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.divbtnsave.Visible = true;
            this.btncancel.Visible = true;
            this.ResetTable();
            this.ClearControls();

            this.ddltodepot.Enabled = true;
            this.ddlbatchno.SelectedValue = "0";

            this.txtstockqty.Text = "";
            this.txttransferqty.Text = "0.00";
            this.btnaddhide.Style["display"] = "none";

            this.gvtransfer.DataSource = null;
            this.gvtransfer.DataBind();
            //ViewState["SumTotalAmount"] = 0.00;
            this.DateLock();
            

            #region QueryString
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = true;
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

    #region btngrddelete_Click
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dttransfer = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
            DataTable DtTax = (DataTable)HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"];
            int delflag = 0;
            string pid = string.Empty;
            string batchno = string.Empty;
            decimal tqty = 0;
            decimal TotalTax = 0;

            string Taxpid = string.Empty;
            string Taxbatchno = string.Empty;

            decimal TotalAmount = 0;
            ImageButton btn_Del = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_Del.NamingContainer;
            this.hdn_guid.Value = gvr.Cells[1].Text.Trim();
            string guid = hdn_guid.Value.ToString();

            int i = dttransfer.Rows.Count - 1;

            while (i >= 0)
            {
                if (Convert.ToString(dttransfer.Rows[i]["GUID"]).Trim() == guid)
                {
                    pid = dttransfer.Rows[i]["PRODUCTID"].ToString().Trim();
                    batchno = dttransfer.Rows[i]["BATCHNO"].ToString().Trim();
                    Taxpid = dttransfer.Rows[i]["PRODUCTID"].ToString().Trim();
                    Taxbatchno = dttransfer.Rows[i]["BATCHNO"].ToString().Trim();
                    tqty = Convert.ToDecimal(dttransfer.Rows[i]["TRANSFERQTY"].ToString());
                    dttransfer.Rows[i].Delete();
                    dttransfer.AcceptChanges();
                    delflag = 1;
                    break;
                }
                i--;
            }

            if (delflag == 1)
            {
                HttpContext.Current.Session["STOCKTRANSFERQTY"] = dttransfer;
                gvtransfer.DataSource = dttransfer;
                gvtransfer.DataBind();

                for (int j = 0; j < dttransfer.Rows.Count; j++)
                {
                    TotalAmount = Convert.ToDecimal(dttransfer.Rows[j]["AMOUNT"].ToString().Trim()) + TotalAmount;
                }

                #region Item Wise Tax
                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTransferTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition1.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                }
                #endregion
                DtTax = (DataTable)Session["TRANSFERTAXCOMPONENTDETAILS"];
                //DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Taxpid + "' AND BATCHNO = '" + Taxbatchno + "' AND TAXGUID='" + guid + "'");
                DataRow[] drrTax = DtTax.Select("PRODUCTID = '" + Taxpid + "' AND BATCHNO = '" + Taxbatchno + "'");
                for (int t = 0; t < drrTax.Length; t++)
                {
                    drrTax[t].Delete();
                    DtTax.AcceptChanges();
                }
                HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"] = DtTax;
                #endregion
                TotalTax = CalculateTaxTotal(dttransfer);
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount + TotalTax));
                this.txttaxamt.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim()))));
                this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtnetamt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim())));


                if (dttransfer.Rows.Count > 0)
                {
                    this.BillingGridCalculation();
                    this.txtTotalCase.Text = Convert.ToString(String.Format("{0:0.00}", CalculateTotalCasePack(dttransfer)));
                    this.txtTotalPCS.Text = Convert.ToString(String.Format("{0:0.00}", CalculateTotalPCS(dttransfer)));
                }
                else
                {
                    this.txtTotalCase.Text = "0";
                    this.txtTotalPCS.Text = "0";
                    this.txttaxamt.Text = "0";
                    this.txtbasicamt.Text = "0";
                    this.txttotal.Text = "0";
                }

                this.ddlproductname.SelectedValue = "0";
                this.ddlbatchno.SelectedValue = "0";
                this.txttransferqty.Text = "0.00";
                this.hdn_guid.Value = "";

                MessageBox1.ShowSuccess("Record Deleted Successfully!");
            }
            else
            {
                MessageBox1.ShowSuccess("Record Deleted UnSuccessful!");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngrdedit_Click
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadStore1();
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            string transferid = hdn_transferid.Value.ToString().Trim();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalTax = 0;
            DataTable dttransferEdit = new DataTable();
           

            if (clsstocktransfer.GetDespatchstatus(transferid) == "1" && Checker == "FALSE")
            {
                MessageBox1.ShowInfo("<b>Stock Received already done,not allow to edit.</b>", 50, 530);
                return;
            }
            else
            {
                this.LoadStore1();
                this.LoadCategory();
                this.LoadDepo();   // Fill Source Depot
                this.FillDepot(); // Fill Destination DEPOT
                this.MotherDepoChange();  // Fill Product List 
                this.BindInsurenceCompany();// Fill To Insuarance company
                if (Checker == "FALSE")
                {
                    this.LoadTranspoter();
                }
                else
                {
                    this.LoadTranspoterChecker();
                }
                this.LoadModeofTransport();
                decimal TotalAmount = 0;
                this.ResetTable();
                this.ddlbatchno.Items.Clear();
                this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO ", "0"));
                this.ddlbatchno.AppendDataBoundItems = true;
                this.txttransferqty.Text = "";
                this.txtstockqty.Text = "";
                this.btnaddhide.Style["display"] = "none";
                this.trtransferid.Style["display"] = "";

                DataSet dsTransferDtls = new DataSet();
                dsTransferDtls = clsstocktransfer.EditStockTransferDetails(hdn_transferid.Value.Trim());

                if (Session["STOCKTRANSFERQTY"] == null)
                {
                    BindTransferGrid();
                }
                if (Session["TRANSFERTAXCOMPONENTDETAILS"] == null)
                {
                    CreateDataTableTaxComponent();//For Tax Calculation Method
                }
                //this.ddlstorelocation.SelectedValue = Convert.ToString(dsTransferDtls.Tables["BILLDETAILS"].Rows[0]["STORELOCATIONID"]).Trim();

                #region Stocktransfer Header
                if (dsTransferDtls.Tables[0].Rows.Count > 0)
                {
                    this.txttransferno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["STOCKTRANSFERNO"]).Trim();
                    this.txttransferdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANDATE"]).Trim();
                    this.ddlfromdepot.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["MOTHERDEPOTID"]).Trim();
                    this.FillDepot();
                    this.ddlwaybillapplicale.SelectedValue = "1";
                    this.ddltodepot.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TODEPOTID"]).Trim();
                    this.ddltodepot_SelectedIndexChanged(sender, e);
                    this.ddlwaybill.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["WAYBILLKEY"]).Trim();
                    this.ddltranspoter.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TRANSPORTERID"]).Trim();
                    this.ddlmodetransport.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["MODEOFTRANSPORT"]).Trim();
                    this.txtvehicleno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["VEHICHLENO"]).Trim();
                    this.txtlrgrno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["LRGRNO"]).Trim();
                    this.txtlrgrdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["LRGRDATE"]).Trim();
                    this.txtchallanno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANNO"]).Trim();
                    this.txtchallandate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["STOCKTRANSFERDATE"]).Trim();
                    this.txtRemarks.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["REMARKS"]).Trim();
                    this.txtCheckerNote.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["NOTE"]).Trim();

                    /*if (dteditedtransferrecord.Rows[0]["FORMREQUIRED"].ToString().Trim() == "Y")
                    {
                        chkActive.Checked = true;
                        lbltext.Text = "Yes!";
                        lbltext.Style.Add("color", "green");
                        lbltext.Style.Add("font-weight", "bold");
                    }
                    else
                    {
                        chkActive.Checked = false;
                        lbltext.Text = "No!";
                        lbltext.Style.Add("color", "red");
                        lbltext.Style.Add("font-weight", "bold");
                    }*/
                    this.txtgatepassno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["GATEPASSNO"]).Trim();
                    this.txtgatepassdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["GATEPASSDATE"]).Trim();
                    this.MotherDepoChange();
                    this.BindInsurenceCompany();
                    this.ddlinsurancecompname.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                    this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue);
                    this.ddlinsuranceno.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["INSURANCENO"]).Trim();
                    this.txtTotalCase.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TOTALCASE"]).Trim();
                    this.txtTotalPCS.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TOTALPCS"]).Trim();
                }
                else
                {
                    MessageBox1.ShowInfo("Transfered records not found.");
                    return;
                }
                #endregion

                #region Stock Transfer Details

                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTransferTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                if (dsTransferDtls.Tables[1].Rows.Count > 0)
                {
                    this.ddlstorelocation.SelectedValue = Convert.ToString(dsTransferDtls.Tables[1].Rows[0]["STORELOCATIONID"]).Trim();
                    dttransferEdit = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
                    for (int p = 0; p < dsTransferDtls.Tables[1].Rows.Count; p++)
                    {
                        DataRow dr = dttransferEdit.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["PRODUCTID"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim();
                        dr["HSNCODE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["HSNCODE"]).Trim();
                        dr["PRODUCTNAME"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTNAME"]).Trim();
                        dr["BATCHNO"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BATCHNO"]).Trim();
                        dr["PACKINGSIZEID"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PACKINGSIZEID"]).Trim();
                        dr["PACKINGSIZENAME"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PACKINGSIZENAME"]).Trim();
                        dr["TRANSFERQTY"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["TRANSFERQTY"]).Trim());
                        dr["MRP"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["MRP"]).Trim());
                        dr["RATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["RATE"]).Trim();
                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["AMOUNT"]).Trim());
                        dr["TOTALMRP"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TOTALMRP"]).Trim();
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["ASSESMENTPERCENTAGE"]).Trim();
                        dr["TOTALASSESMENTVALUE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TOTALASSESMENTVALUE"]).Trim();
                        dr["MFDATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["MFDATE"]).Trim();
                        dr["EXPRDATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["EXPRDATE"]).Trim();
                        dr["NETWEIGHT"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["NETWEIGHT"]).Trim();
                        dr["GROSSWEIGHT"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["GROSSWEIGHT"]).Trim();
                        dr["TAG"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TAG"]).Trim();
                        dr["BEFOREEDITEDQTY"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BEFOREEDITEDQTY"]).Trim();

                        TotalAmount = Convert.ToDecimal(dsTransferDtls.Tables[1].Rows[p]["AMOUNT"].ToString().Trim()) + TotalAmount;

                        decimal BillTaxAmt = 0;

                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(dsTransferDtls.Tables[2].Rows[k]["RELATEDTO"]))
                            {
                                case "1":
                                    TAXID = clsstocktransfer.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    //ProductWiseTax = clsstocktransfer.GetHSNTax(TAXID, Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim(), Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANDATE"]).Trim());
                                    ProductWiseTax = clsstocktransfer.GetHSNTaxOnEdit(hdn_transferid.Value.Trim(), TAXID, Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim(), Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BATCHNO"]).Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                        }
                        #endregion

                        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(dsTransferDtls.Tables[1].Rows[p]["AMOUNT"].ToString().Trim())));
                        dttransferEdit.Rows.Add(dr);
                        dttransferEdit.AcceptChanges();
                    }
                }
                #endregion

                #region Product Wise Tax Details
                if (dsTransferDtls.Tables[3].Rows.Count > 0)
                {
                    DataTable dtTaxComponentEdit = (DataTable)Session["TRANSFERTAXCOMPONENTDETAILS"];
                    for (int T = 0; T < dsTransferDtls.Tables[3].Rows.Count; T++)
                    {
                        DataRow drTax = dtTaxComponentEdit.NewRow();
                        drTax["SALEORDERID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["SALEORDERID"]);
                        drTax["PRODUCTID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["PRODUCTID"]);
                        drTax["BATCHNO"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["BATCHNO"]);
                        drTax["TAXID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["TAXID"]);
                        drTax["PERCENTAGE"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["PERCENTAGE"]);
                        drTax["TAXVALUE"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["TAXVALUE"]);
                        dtTaxComponentEdit.Rows.Add(drTax);
                        dtTaxComponentEdit.AcceptChanges();
                    }
                    HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
                }
                #endregion

                TotalTax = CalculateTaxTotal(dttransferEdit);
                /*for (int i = 0; i < dttransferEdit.Rows.Count; i++)
                {
                    TotalAmount = Convert.ToDecimal(dttransferEdit.Rows[i]["AMOUNT"].ToString().Trim()) + TotalAmount;
                }
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount + TotalTax));*/

                #region Amount-Calculation Footer Details
                if (dsTransferDtls.Tables[4].Rows.Count > 0)
                {
                    this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["ROUNDOFFVALUE"].ToString())));
                    this.txtnetamt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                    this.txtRoundoff.Text = String.Format("{0:0.00}", dsTransferDtls.Tables[4].Rows[0]["ROUNDOFFVALUE"].ToString());
                    this.txttaxamt.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                }
                #endregion

                #region gvtransfer DataBind
                HttpContext.Current.Session["STOCKTRANSFERQTY"] = dttransferEdit;
                this.gvtransfer.DataSource = dttransferEdit;
                this.gvtransfer.DataBind();
                this.BillingGridCalculation();
                #endregion

                this.ddlfromdepot.Enabled = false;
                this.ddltodepot.Enabled = false;
                this.pnlAdd.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.btnadd.Visible = true;
                this.divbtnsave.Visible = true;
                this.btncancel.Visible = true;
                this.btnback.Visible = false;
                this.pnlAdd.Enabled = true;

                #region QueryString

                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    this.divbtnsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.txtRemarks.Enabled = false;
                }
                else
                {
                    if (clsstocktransfer.GetFinancestatus(transferid) == "1")
                    {
                        this.divbtnsave.Visible = false;
                        this.gvtransfer.Columns[0].Visible = false;
                    }
                    else
                    {
                        this.divbtnsave.Visible = true;
                        this.gvtransfer.Columns[0].Visible = true;
                    }
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = true;
                    this.txtCheckerNote.Visible = true;
                    this.txtRemarks.Enabled = true;
                }
                #endregion
            }
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
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
        int flag = 0;
        string transferid = Convert.ToString(hdn_transferid.Value).Trim();
        flag = clsstocktransfer.CheckFormRequired(transferid);
        if (flag == 1)
        {
            this.txtCFormNo.Text = hdnCFormNo.Value.ToString();
            this.txtCFormPopupDate.Text = hdnCFormDate.Value.ToString();
            this.light2.Style["display"] = "block";
            this.fade.Style["display"] = "block";
        }
        else
        {
            MessageBox1.ShowInfo("<b><font color='red'>F Form not required!</font></b>");
        }
    }
    #endregion

    #region btnCFormUpdate_Click
    protected void btnCFormUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtCFormNo.Text.Trim() != "")
            {
                ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
                int flag = 0;
                string transferid = Convert.ToString(hdn_transferid.Value).Trim();
                flag = clsstocktransfer.UpdateCForm(transferid, this.txtCFormNo.Text.Trim(), this.txtCFormPopupDate.Text.Trim());
                this.hdn_transferid.Value = "";

                if (flag == 1)
                {
                    this.light2.Style["display"] = "none";
                    this.fade.Style["display"] = "none";
                    this.LoadDispatchDetails();
                    MessageBox1.ShowSuccess("F Form <b><font color='green'>updated successfully</font></b>!");
                }
                else
                {
                    MessageBox1.ShowError("F Form <b><font color='red'>updated unsuccessful</font></b>!");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please enter <b><font color='green'>F Form No</font></b>!");
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
        this.light2.Style["display"] = "none";
        this.fade.Style["display"] = "none";
    }
    #endregion

    #region ddlCFormFilter_SelectedIndexChanged
    protected void ddlCFormFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCFormFilter.SelectedValue != "0")
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            gvStockTransferDetails.DataSource = clsstocktransfer.BindDespatchCformFilter(this.ddlCFormFilter.SelectedValue, txtfromdateins.Text.Trim(), txttodateins.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]), "9A555D40-5E12-4F5C-8EE0-E085B5BAB169");
            gvStockTransferDetails.DataBind();
        }
    }
    #endregion

    #region BindInsurenceCompany
    protected void BindInsurenceCompany()
    {
        try
        {
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            DataTable dtInsuranceCompany = new DataTable();
            dtInsuranceCompany = clsstocktransfer.BindinscompIns(Convert.ToString(Request.QueryString["MENUID"]).Trim());
            if (dtInsuranceCompany.Rows.Count > 0)
            {
                this.ddlinsurancecompname.Items.Clear();
                this.ddlinsurancecompname.Items.Add(new ListItem("Select Inurance Company", "0"));
                ddlinsurancecompname.AppendDataBoundItems = true;
                ddlinsurancecompname.DataSource = dtInsuranceCompany;
                ddlinsurancecompname.DataTextField = "COMPANY_NAME";
                ddlinsurancecompname.DataValueField = "ID";
                ddlinsurancecompname.DataBind();
                if (dtInsuranceCompany.Rows.Count == 1)
                {
                    ddlinsurancecompname.SelectedValue = Convert.ToString(dtInsuranceCompany.Rows[0]["ID"]).Trim();
                    this.BindInsurenceNumber(ddlinsurancecompname.SelectedValue.Trim());
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlinsuranceno.ClientID + "').focus(); ", true);
                }
            }
            else
            {
                this.ddlinsurancecompname.Items.Clear();
                this.ddlinsurancecompname.Items.Add(new ListItem("Select Inurance Company", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region BindInsurenceNumber
    protected void BindInsurenceNumber(string CompID)
    {
        ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = "frmPrintPopUp_FAC.aspx?Stnid=" + hdn_transferid.Value.Trim() + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=" + Request.QueryString["MenuId"].ToString() + " ";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
        DataTable dtSTOCKTRANSFERDETAILS = new DataTable();
        decimal TotalGridAmount = 0;
        decimal TotalTaxAmount = 0;
        decimal TotalAmount = 0;
        decimal TotalNetAmt = 0;

        if (Session["STOCKTRANSFERQTY"] != null)
        {
            dtSTOCKTRANSFERDETAILS = (DataTable)Session["STOCKTRANSFERQTY"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtSTOCKTRANSFERDETAILS);

        #region Header Style

        this.gvtransfer.HeaderRow.Cells[4].Text = "PRODUCT";
        //this.gvtransfer.HeaderRow.Cells[5].Text = "BATCH";
        this.gvtransfer.HeaderRow.Cells[7].Text = "PACK SIZE";
        this.gvtransfer.HeaderRow.Cells[8].Text = "QTY";
        //this.gvtransfer.HeaderRow.Cells[17].Text = "MFG DATE";
        //this.gvtransfer.HeaderRow.Cells[18].Text = "EXPR DATE";

        this.gvtransfer.HeaderRow.Cells[1].Visible = false;
        this.gvtransfer.HeaderRow.Cells[2].Visible = false;
        this.gvtransfer.HeaderRow.Cells[5].Visible = false;
        this.gvtransfer.HeaderRow.Cells[6].Visible = false;
        this.gvtransfer.HeaderRow.Cells[12].Visible = false;
        this.gvtransfer.HeaderRow.Cells[13].Visible = false;
        this.gvtransfer.HeaderRow.Cells[14].Visible = false;
        this.gvtransfer.HeaderRow.Cells[15].Visible = false;
        this.gvtransfer.HeaderRow.Cells[16].Visible = false;
        this.gvtransfer.HeaderRow.Cells[17].Visible = false;
        this.gvtransfer.HeaderRow.Cells[18].Visible = false;
        this.gvtransfer.HeaderRow.Cells[19].Visible = false;
        this.gvtransfer.HeaderRow.Cells[20].Visible = false;

        this.gvtransfer.HeaderRow.Cells[1].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[2].Wrap = false;        
        this.gvtransfer.HeaderRow.Cells[3].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[4].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[5].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[6].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[7].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[8].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[9].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[10].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[11].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[12].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[13].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[14].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[15].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[16].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[17].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[18].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[19].Wrap = false;
        this.gvtransfer.HeaderRow.Cells[20].Wrap = false;
        #endregion

        #region Footer Style
        this.gvtransfer.FooterRow.Cells[1].Visible = false;
        this.gvtransfer.FooterRow.Cells[2].Visible = false;
        this.gvtransfer.FooterRow.Cells[5].Visible = false;
        this.gvtransfer.FooterRow.Cells[6].Visible = false;
        this.gvtransfer.FooterRow.Cells[12].Visible = false;
        this.gvtransfer.FooterRow.Cells[13].Visible = false;
        this.gvtransfer.FooterRow.Cells[14].Visible = false;
        this.gvtransfer.FooterRow.Cells[15].Visible = false;
        this.gvtransfer.FooterRow.Cells[16].Visible = false;
        this.gvtransfer.FooterRow.Cells[17].Visible = false;
        this.gvtransfer.FooterRow.Cells[18].Visible = false;
        this.gvtransfer.FooterRow.Cells[19].Visible = false;
        this.gvtransfer.FooterRow.Cells[20].Visible = false;

        this.gvtransfer.FooterRow.Cells[1].Wrap = false;
        this.gvtransfer.FooterRow.Cells[2].Wrap = false;
        this.gvtransfer.FooterRow.Cells[3].Wrap = false;
        this.gvtransfer.FooterRow.Cells[4].Wrap = false;
        this.gvtransfer.FooterRow.Cells[5].Wrap = false;
        this.gvtransfer.FooterRow.Cells[6].Wrap = false;
        this.gvtransfer.FooterRow.Cells[7].Wrap = false;
        this.gvtransfer.FooterRow.Cells[8].Wrap = false;
        this.gvtransfer.FooterRow.Cells[9].Wrap = false;
        this.gvtransfer.FooterRow.Cells[10].Wrap = false;
        this.gvtransfer.FooterRow.Cells[11].Wrap = false;
        this.gvtransfer.FooterRow.Cells[12].Wrap = false;
        this.gvtransfer.FooterRow.Cells[13].Wrap = false;
        this.gvtransfer.FooterRow.Cells[14].Wrap = false;
        this.gvtransfer.FooterRow.Cells[15].Wrap = false;
        this.gvtransfer.FooterRow.Cells[16].Wrap = false;
        this.gvtransfer.FooterRow.Cells[17].Wrap = false;
        this.gvtransfer.FooterRow.Cells[18].Wrap = false;
        this.gvtransfer.FooterRow.Cells[19].Wrap = false;
        this.gvtransfer.FooterRow.Cells[20].Wrap = false;
        this.gvtransfer.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;

        #endregion

        foreach (GridViewRow row in gvtransfer.Rows)
        {
            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[5].Visible = false;
            row.Cells[6].Visible = false;
            row.Cells[12].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[19].Visible = false;
            row.Cells[20].Visible = false;

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
            row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            /*row.Cells[17].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Center;*/

            TotalGridAmount += Convert.ToDecimal(row.Cells[11].Text.Trim());
            txtbasicamt.Text = TotalGridAmount.ToString("#.00");

            int count = 20;
            DataTable dt = (DataTable)Session["dtTransferTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.gvtransfer.HeaderRow.Cells[count].Wrap = false;
            }
        }

        #region TotalTax Footer
        int TotalRows = gvtransfer.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTransferTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 22; i <= (22 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    sum += gvtransfer.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(gvtransfer.Rows[j].Cells[i].Text) : 0.00;
                    gvtransfer.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                this.gvtransfer.FooterRow.Cells[i].Text = sum.ToString("#.00");
                this.gvtransfer.FooterRow.Cells[i].Font.Bold = true;
                this.gvtransfer.FooterRow.Cells[i].ForeColor = Color.Blue;
                this.gvtransfer.FooterRow.Cells[i].Wrap = false;
                this.gvtransfer.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                count1 = count1 + 1;
            }
        }
        #endregion

        if (TotalGridAmount == 0)
        {
            this.gvtransfer.FooterRow.Cells[11].Text = "0.00";
        }
        else
        {
            this.gvtransfer.FooterRow.Cells[11].Text = TotalGridAmount.ToString("#.00");
        }
        this.gvtransfer.FooterRow.Cells[11].Font.Bold = true;
        this.gvtransfer.FooterRow.Cells[11].ForeColor = Color.Blue;
        this.gvtransfer.FooterRow.Cells[11].Wrap = false;

        this.gvtransfer.FooterRow.Cells[10].Text = "Total : ";
        this.gvtransfer.FooterRow.Cells[10].Font.Bold = true;
        this.gvtransfer.FooterRow.Cells[10].ForeColor = Color.Blue;

        TotalTaxAmount = Convert.ToDecimal(this.txttaxamt.Text.Trim());
        TotalAmount = (TotalGridAmount + TotalTaxAmount);
        this.txttotal.Text = TotalAmount.ToString("#.00");

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                gvtransfer.FooterRow.Cells[23 + count1].Text = "0.00";
            }
            else
            {
                gvtransfer.FooterRow.Cells[23 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvtransfer.FooterRow.Cells[23 + count1].Font.Bold = true;
            gvtransfer.FooterRow.Cells[23 + count1].ForeColor = Color.Blue;
            gvtransfer.FooterRow.Cells[23 + count1].Wrap = false;
            gvtransfer.FooterRow.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvtransfer.Rows)
            {
                row.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvtransfer.HeaderRow.Cells[23 + count1].Text = "NET AMOUNT";
            this.gvtransfer.HeaderRow.Cells[23 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                gvtransfer.FooterRow.Cells[22 + count1].Text = "0.00";
            }
            else
            {
                gvtransfer.FooterRow.Cells[22 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvtransfer.FooterRow.Cells[22 + count1].Font.Bold = true;
            gvtransfer.FooterRow.Cells[22 + count1].ForeColor = Color.Blue;
            gvtransfer.FooterRow.Cells[22 + count1].Wrap = false;
            gvtransfer.FooterRow.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvtransfer.Rows)
            {
                row.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvtransfer.HeaderRow.Cells[22 + count1].Text = "NET AMOUNT";
            this.gvtransfer.HeaderRow.Cells[22 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                gvtransfer.FooterRow.Cells[21 + count1].Text = "0.00";
            }
            else
            {
                gvtransfer.FooterRow.Cells[21 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvtransfer.FooterRow.Cells[21 + count1].Font.Bold = true;
            gvtransfer.FooterRow.Cells[21 + count1].ForeColor = Color.Blue;
            gvtransfer.FooterRow.Cells[21 + count1].Wrap = false;
            gvtransfer.FooterRow.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvtransfer.Rows)
            {
                row.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvtransfer.HeaderRow.Cells[21 + count1].Text = "NET AMOUNT";
            this.gvtransfer.HeaderRow.Cells[21 + count1].Wrap = false;
        }
        #endregion
    }
    #endregion

    #region View_Click
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            ClsFactoryStockTransfer_RMPM clsstocktransfer = new ClsFactoryStockTransfer_RMPM();
            string transferid = hdn_transferid.Value.ToString().Trim();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalTax = 0;
            DataTable dttransferEdit = new DataTable();
            this.LoadStore1();

            /*if (clsstocktransfer.GetDespatchstatus(transferid) == "1" && Checker == "FALSE")
            {
                MessageBox1.ShowInfo("<b>Stock Received already done,not allow to edit.</b>", 50, 530);
                return;
            }
            else
            {*/
            this.LoadCategory();
            this.LoadDepo();   // Fill Source Depot
            this.FillDepot(); // Fill Destination DEPOT
            this.MotherDepoChange();  // Fill Product List 
            this.BindInsurenceCompany();// Fill To Insuarance company
            if (Checker == "FALSE")
            {
                this.LoadTranspoter();
            }
            else
            {
                this.LoadTranspoterChecker();
            }
            this.LoadModeofTransport();
            decimal TotalAmount = 0;
            this.ResetTable();
            this.ddlbatchno.Items.Clear();
            this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO ", "0"));
            this.ddlbatchno.AppendDataBoundItems = true;
            this.txttransferqty.Text = "";
            this.txtstockqty.Text = "";
            this.btnaddhide.Style["display"] = "none";
            this.trtransferid.Style["display"] = "";

            DataSet dsTransferDtls = new DataSet();
            dsTransferDtls = clsstocktransfer.EditStockTransferDetails(hdn_transferid.Value.Trim());

            if (Session["STOCKTRANSFERQTY"] == null)
            {
                BindTransferGrid();
            }
            if (Session["TRANSFERTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableTaxComponent();//For Tax Calculation Method
            }

            #region Stocktransfer Header
            if (dsTransferDtls.Tables[0].Rows.Count > 0)
            {
                this.txttransferno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["STOCKTRANSFERNO"]).Trim();
                this.txttransferdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANDATE"]).Trim();
                this.ddlfromdepot.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["MOTHERDEPOTID"]).Trim();
                this.FillDepot();
                this.ddlwaybillapplicale.SelectedValue = "1";
                this.ddltodepot.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TODEPOTID"]).Trim();
                this.ddltodepot_SelectedIndexChanged(sender, e);
                this.ddlwaybill.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["WAYBILLKEY"]).Trim();
                this.ddltranspoter.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TRANSPORTERID"]).Trim();
                this.ddlmodetransport.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["MODEOFTRANSPORT"]).Trim();
                this.txtvehicleno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["VEHICHLENO"]).Trim();
                this.txtlrgrno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["LRGRNO"]).Trim();
                this.txtlrgrdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["LRGRDATE"]).Trim();
                this.txtchallanno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANNO"]).Trim();
                this.txtchallandate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["STOCKTRANSFERDATE"]).Trim();
                this.txtRemarks.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.txtCheckerNote.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["NOTE"]).Trim();
                this.txtgatepassno.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["GATEPASSNO"]).Trim();
                this.txtgatepassdate.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["GATEPASSDATE"]).Trim();
                this.MotherDepoChange();
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue);
                this.ddlinsuranceno.SelectedValue = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["INSURANCENO"]).Trim();
                this.txtTotalCase.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TOTALCASE"]).Trim();
                this.txtTotalPCS.Text = Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["TOTALPCS"]).Trim();
            }
            else
            {
                MessageBox1.ShowInfo("Transfered records not found.");
                return;
            }
            #endregion

            #region Stock Transfer Details

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtTransferTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
            }
            #endregion

            if (dsTransferDtls.Tables[1].Rows.Count > 0)
            {
                dttransferEdit = (DataTable)HttpContext.Current.Session["STOCKTRANSFERQTY"];
                this.ddlstorelocation.SelectedValue = Convert.ToString(dsTransferDtls.Tables[1].Rows[0]["STORELOCATIONID"]).Trim();
                for (int p = 0; p < dsTransferDtls.Tables[1].Rows.Count; p++)
                {
                    DataRow dr = dttransferEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["PRODUCTID"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim();
                    dr["HSNCODE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["HSNCODE"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTNAME"]).Trim();
                    dr["BATCHNO"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BATCHNO"]).Trim();
                    dr["PACKINGSIZEID"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PACKINGSIZEID"]).Trim();
                    dr["PACKINGSIZENAME"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PACKINGSIZENAME"]).Trim();
                    dr["TRANSFERQTY"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["TRANSFERQTY"]).Trim());
                    dr["MRP"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["MRP"]).Trim());
                    dr["RATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["RATE"]).Trim();
                    dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", dsTransferDtls.Tables[1].Rows[p]["AMOUNT"]).Trim());
                    dr["TOTALMRP"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TOTALMRP"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESMENTVALUE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TOTALASSESMENTVALUE"]).Trim();
                    dr["MFDATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["EXPRDATE"]).Trim();
                    dr["NETWEIGHT"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["NETWEIGHT"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["GROSSWEIGHT"]).Trim();
                    dr["TAG"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["TAG"]).Trim();
                    dr["BEFOREEDITEDQTY"] = Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BEFOREEDITEDQTY"]).Trim();
                    TotalAmount = Convert.ToDecimal(dsTransferDtls.Tables[1].Rows[p]["AMOUNT"].ToString().Trim()) + TotalAmount;
                    decimal BillTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dsTransferDtls.Tables[2].Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = clsstocktransfer.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = clsstocktransfer.GetHSNTax(TAXID, Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim(), Convert.ToString(dsTransferDtls.Tables[0].Rows[0]["CHALLANDATE"]).Trim());
                                ProductWiseTax = clsstocktransfer.GetHSNTaxOnEdit(hdn_transferid.Value.Trim(), TAXID, Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["PRODUCTID"]).Trim(), Convert.ToString(dsTransferDtls.Tables[1].Rows[p]["BATCHNO"]).Trim());
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                break;
                        }
                    }
                    #endregion

                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(dsTransferDtls.Tables[1].Rows[p]["AMOUNT"].ToString().Trim())));
                    dttransferEdit.Rows.Add(dr);
                    dttransferEdit.AcceptChanges();
                }
            }
            #endregion

            #region Product Wise Tax Details
            if (dsTransferDtls.Tables[3].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TRANSFERTAXCOMPONENTDETAILS"];
                for (int T = 0; T < dsTransferDtls.Tables[3].Rows.Count; T++)
                {
                    DataRow drTax = dtTaxComponentEdit.NewRow();
                    drTax["SALEORDERID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["SALEORDERID"]);
                    drTax["PRODUCTID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["PRODUCTID"]);
                    drTax["BATCHNO"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["BATCHNO"]);
                    drTax["TAXID"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["TAXID"]);
                    drTax["PERCENTAGE"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["PERCENTAGE"]);
                    drTax["TAXVALUE"] = Convert.ToString(dsTransferDtls.Tables[3].Rows[T]["TAXVALUE"]);
                    dtTaxComponentEdit.Rows.Add(drTax);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["TRANSFERTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            TotalTax = CalculateTaxTotal(dttransferEdit);
            #region Amount-Calculation Footer Details
            if (dsTransferDtls.Tables[4].Rows.Count > 0)
            {
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["ROUNDOFFVALUE"].ToString())));
                this.txtnetamt.Text = String.Format("{0:0.00}", Convert.ToDecimal(dsTransferDtls.Tables[4].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtRoundoff.Text = String.Format("{0:0.00}", dsTransferDtls.Tables[4].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txttaxamt.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            }
            #endregion

            #region gvtransfer DataBind
            HttpContext.Current.Session["STOCKTRANSFERQTY"] = dttransferEdit;
            this.gvtransfer.DataSource = dttransferEdit;
            this.gvtransfer.DataBind();
            this.BillingGridCalculation();
            #endregion

            this.ddlfromdepot.Enabled = false;
            this.ddltodepot.Enabled = false;
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.divbtnsave.Visible = false;
            this.divbtnapprove.Visible = false;
            this.divbtnreject.Visible = false;
            this.btncancel.Visible = true;
            this.btnback.Visible = false;
            this.pnlAdd.Enabled = true;

            #region QueryString

            //Checker = Request.QueryString["CHECKER"].ToString().Trim();
            //if (Checker == "TRUE")
            //{
            //    this.divbtnapprove.Visible = true;
            //    this.divbtnreject.Visible = true;
            //    this.lblCheckerNote.Visible = false;
            //    this.txtCheckerNote.Visible = false;
            //    this.txtRemarks.Enabled = false;
            //}
            //else
            //{
            //    if (clsstocktransfer.GetFinancestatus(transferid) == "1")
            //    {                    
            //        this.gvtransfer.Columns[0].Visible = false;
            //    }
            //    else
            //    {                    
            //        this.gvtransfer.Columns[0].Visible = true;
            //    }
            //    this.divbtnapprove.Visible = false;
            //    this.divbtnreject.Visible = false;
            //    this.lblCheckerNote.Visible = true;
            //    this.txtCheckerNote.Visible = true;
            //    this.txtRemarks.Enabled = true;
            //}
            #endregion
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void LoadStore()
    {
        try
        {
            ClsSaleReturn_FAC objReturn = new ClsSaleReturn_FAC();
            DataTable dtstore1 = new DataTable();
            dtstore1 = objReturn.BindStoreDetails1(HttpContext.Current.Session["USERID"].ToString().Trim());
            this.ddlstorelocation.Items.Clear();
            //this.ddlstorelocation.Items.Add(new ListItem("Select store location", "0"));
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dtstore1;
            ddlstorelocation.DataValueField = "STORELOCATIONID";
            ddlstorelocation.DataTextField = "STORELOCATIONNAME";
            ddlstorelocation.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadStore1()
    {
        try
        {
            ClsSaleReturn_FAC objReturn = new ClsSaleReturn_FAC();
            DataTable dtstore1 = new DataTable();
            dtstore1 = objReturn.BindStoreLOCATION();
            this.ddlstorelocation.Items.Clear();
            //this.ddlstorelocation.Items.Add(new ListItem("Select store location", "0"));
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dtstore1;
            ddlstorelocation.DataValueField = "ID";
            ddlstorelocation.DataTextField = "NAME";
            ddlstorelocation.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


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

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'); ;
                this.txttransferdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                Calendar1.EndDate = today1;
                CalendarExtender4.EndDate = today1;
                CalendarExtender3.EndDate = today1;
                CalendarExtender5.EndDate = today1;
            }
            else
            {
                this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttransferdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                Calendar1.EndDate = cDate;
                CalendarExtender4.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
                CalendarExtender5.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
}
