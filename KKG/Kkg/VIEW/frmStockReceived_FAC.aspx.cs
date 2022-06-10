//using Account;
using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow;

public partial class VIEW_frmStockReceived_FAC : System.Web.UI.Page
{
    decimal TotalAmount = 0;
    string menuID = string.Empty;
    string Checker = string.Empty;
    decimal TotalTaxValue = 0;
    decimal TotalGridAmount = 0;
    decimal TotalNetAmt = 0;
    DataTable dtDespatchEdit = new DataTable();
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TYPE"] != "LGRREPORT")
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
                    this.divDocuments.Visible = true;
                    this.TrExchangrate.Visible = true;
                    this.Tdlblledger.Visible = true;
                    this.Tdddlledger.Visible = true;
                    this.Tcs_div.Visible = false;
                    this.Label47.Enabled = false;


                }
                else
                {
                    btnaddhide.Style["display"] = "";
                    this.btnsubmitdiv.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.divDocuments.Visible = false;
                    this.TrExchangrate.Visible = false;
                    this.Tdlblledger.Visible = false;
                    this.Tdddlledger.Visible = false;
                }
                #endregion

                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                ViewState["count"] = null;
                /*DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";                
                this.txtFromDate.Style.Add("color", "black !important");
                this.txtToDate.Style.Add("color", "black !important");
                this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');*/
                this.LoadReceived();
                this.LoadTransporter();
                this.LoadTPU();
                this.LoadMotherDepot();
                this.LoadDespatchNo();
                //this.FetchLedger();
                this.DateLock();
                ViewState["SumTotal"] = "0.00";
                //this.grdAddDespatch.AutoGenerateColumns = false;
                this.LoadInsurance();
                #region Disable Controls

                txtRceivedNo.Enabled = false;
                txtreceiveddate.Enabled = false;
                txtDespatchDate.Enabled = false;
                txtVehicle.Enabled = false;
                txtLRGRNo.Enabled = false;
                txtLRGRDate.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtWayBill.Enabled = true;
                txtinsuranceno.Enabled = false;

                //this.imgbtnCalendar.Visible = false;
                this.ddlDepot.Enabled = false;
                this.ddlTransporter.Enabled = false;
                this.ddlTransportMode.Enabled = false;
                this.ddlTPU.Enabled = false;

                #endregion
            }
            else
            {
                btnaddhide.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.divDocuments.Visible = false;
                this.TrExchangrate.Visible = false;
                this.Tdlblledger.Visible = false;
                this.Tdddlledger.Visible = false;
                this.trBtn.Visible = false;
                this.divbtnCancel.Visible = false;
                Btn_View(Request.QueryString["InvId"]);
            }
        }

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 300, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>MakeStaticHeaderScheme('" + gvProductTax.ClientID + "', 200, '100%' , 30 ,false ); </script>", false);
    }
    #endregion

    #region LoadReason
    public void LoadReason()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlrejectionreason.Items.Clear();
        ddlrejectionreason.Items.Add(new ListItem("-- SELECT REJECTION REASON NAME --", "0"));
        ddlrejectionreason.AppendDataBoundItems = true;
        ddlrejectionreason.DataSource = ClsCommon.BindReason(Request.QueryString["MENUID"].ToString().Trim());
        ddlrejectionreason.DataValueField = "ID";
        ddlrejectionreason.DataTextField = "DESCRIPTION";
        ddlrejectionreason.DataBind();
    }
    #endregion

    #region LoadLedger
    public void FetchLedger()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlledger.Items.Clear();
        ddlledger.Items.Add(new ListItem("SELECT LEDGER NAME", "0"));
        ddlledger.AppendDataBoundItems = true;
        ddlledger.DataSource = ClsCommon.LoadGRNLedger();
        ddlledger.DataValueField = "ID";
        ddlledger.DataTextField = "NAME";
        ddlledger.DataBind();
    }
    #endregion

    #region LoadDespatchNo
    public void LoadDespatchNo()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            string DepotID = string.Empty;
            if (Checker == "FALSE")
            {
                if (Session["TPU"].ToString().Trim() == "D" || Session["TPU"].ToString().Trim() == "EXPU")
                {
                    DepotID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
                }
                else
                {
                    DataTable dtDepot = new DataTable();
                    dtDepot = clsReceivedStock.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                    DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                }
            }
            else
            {
                DepotID = this.hdnDepotID.Value.Trim();
            }
            this.ddldespatchno.Items.Clear();
            this.ddldespatchno.Items.Add(new ListItem("Invoice No", "0"));
            this.ddldespatchno.AppendDataBoundItems = true;
            this.ddldespatchno.DataSource = clsReceivedStock.BindDespatchNo(DepotID);
            this.ddldespatchno.DataValueField = "STOCKDESPATCHID";
            this.ddldespatchno.DataTextField = "INVOICENO";
            this.ddldespatchno.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadEditedDespatchNo
    public void LoadEditedDespatchNo()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            string DepotID = string.Empty;
            if (Checker == "FALSE")
            {
                if (Session["TPU"].ToString().Trim() == "D" || Session["TPU"].ToString().Trim() == "EXPU")
                {
                    DepotID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
                }
                else
                {
                    DataTable dtDepot = new DataTable();
                    dtDepot = clsReceivedStock.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                    DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                }
            }
            else
            {
                DepotID = this.hdnDepotID.Value.Trim();
            }
            this.ddldespatchno.Items.Clear();
            this.ddldespatchno.Items.Add(new ListItem("Invoice No", "0"));
            this.ddldespatchno.AppendDataBoundItems = true;
            this.ddldespatchno.DataSource = clsReceivedStock.BindEditedDespatchNo(DepotID);
            this.ddldespatchno.DataValueField = "STOCKDESPATCHID";
            this.ddldespatchno.DataTextField = "INVOICENO";
            this.ddldespatchno.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTPU
    public void LoadTPU()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.ddlTPU.Items.Clear();
            this.ddlTPU.Items.Add(new ListItem("TPU Name", "0"));
            this.ddlTPU.AppendDataBoundItems = true;
            this.ddlTPU.DataSource = clsReceivedStock.BindTPU();
            this.ddlTPU.DataValueField = "VENDORID";
            this.ddlTPU.DataTextField = "VENDORNAME";
            this.ddlTPU.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region LoadInsurance
    public void LoadInsurance()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.ddlinsurancename.Items.Clear();
            this.ddlinsurancename.Items.Add(new ListItem("Insurance Name", "0"));
            this.ddlinsurancename.AppendDataBoundItems = true;
            this.ddlinsurancename.DataSource = clsReceivedStock.BindInsurancename();
            this.ddlinsurancename.DataValueField = "ID";
            this.ddlinsurancename.DataTextField = "COMPANY_NAME";
            this.ddlinsurancename.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region LoadReceived
    public void LoadReceived()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Convert.ToString(Session["TPU"]).Trim() != "F")
            {
                this.grdReceivedHeader.DataSource = clsReceivedStock.BindReceived(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]));
                this.grdReceivedHeader.DataBind();
            }
            else
            {
                this.grdReceivedHeader.DataSource = clsReceivedStock.BindReceived(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), "", HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]));
                this.grdReceivedHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnReceivedID.Value = "";
        this.txtEInvoiceNo.Text = "";
        this.txtCFormDate.Text = "";
        this.txtCFormNo.Text = "";
        this.txtGatepassDate.Text = "";
        this.txtGatepassNo.Text = "";
        this.txtDespatchDate.Text = "";
        this.txtreceiveddate.Text = "";
        this.ddlTransporter.SelectedValue = "0";
        this.ddlTransportMode.SelectedValue = "0";
        this.ddlinsurancename.SelectedValue = "0";
        this.txtLRGRDate.Text = "";
        this.txtLRGRNo.Text = "";
        this.txtVehicle.Text = "";
        this.txtinsuranceno.Text = "";
        this.txtInvoiceDate.Text = "";
        this.txtInvoiceNo.Text = "";
        this.ddlDepot.SelectedValue = "0";
        this.txtRemarks.Text = "";
        this.txtWayBill.Text = "";
        this.ddlTPU.SelectedValue = "0";
        this.txtAdj.Text = "0";
        this.txtOtherCharge.Text = "0";
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "";
        this.txtTotTax.Text = "";
        this.txtNetAmt.Text = "";
        this.txtRoundoff.Text = "";
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.gvProductTax.DataSource = null;
        this.gvProductTax.DataBind();
        this.txtCheckerNote.Text = "";
        this.txtTotMRP.Text = "";
        this.hdnSaleOrderID.Value = "";
        this.hdnSaleOrderNo.Value = "";
        this.txtTotalCase.Text = "";
        this.txtTotalPCS.Text = "";
        txtothchrg.Text = "";
        this.DateLock();
    }
    #endregion

    #region Create Rejection InnerGrid DataTable Structure
    public DataTable CreateRejectionInnerGridDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKDESPATCHID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTRATE", typeof(string)));
        //Added By Avishek Ghosh On 30-03-2016
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));

        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));

        //Added By Avishek Ghosh On 18-03-2016
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));


        HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region Create Rejection Total DataTable Structure
    public DataTable CreateRejectionTotalDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKDESPATCHID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTRATE", typeof(string)));
        //Added By Avishek Ghosh On 30-03-2016
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));

        //Added By Avishek Ghosh On 18-03-2016
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
        HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));                     //2
        dt.Columns.Add(new DataColumn("POID", typeof(string)));                     //3
        dt.Columns.Add(new DataColumn("PODATE", typeof(string)));                   //4
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));                //5
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));              //6
        dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));                  //7
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));            //8
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));          //9
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));                      //10
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));                      //11
        dt.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(string)));              //12
        dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));             //13
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));                 //14
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));               //15
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));                     //16
        dt.Columns.Add(new DataColumn("DEPOTRATE", typeof(string)));                //17
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));                   //18
        dt.Columns.Add(new DataColumn("DISCOUNTPER", typeof(string)));              //19
        dt.Columns.Add(new DataColumn("DISCOUNTAMT", typeof(string)));              //20
        dt.Columns.Add(new DataColumn("AFTERDISCOUNTAMT", typeof(string)));         //21

        dt.Columns.Add(new DataColumn("ITEMWISEFREIGHT", typeof(string)));          //22(Add By Rajeev 01+22-2017)
        dt.Columns.Add(new DataColumn("AFTERITEMWISEFREIGHTAMT", typeof(string)));  //23(Add By Rajeev 01+22-2017)

        dt.Columns.Add(new DataColumn("ITEMWISEADDCOST", typeof(string)));          //24(Add By Rajeev 14+22-2017)
        dt.Columns.Add(new DataColumn("AFTERITEMWISEADDCOSTAMT", typeof(string)));  //25(Add By Rajeev 14+22-2017)

        dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));                   //26
        dt.Columns.Add(new DataColumn("QCREJECTQTY", typeof(string)));                  //27
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));      //28
        dt.Columns.Add(new DataColumn("TOTALASSESABLEVALUE", typeof(string)));      //29
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));                   //30
        dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));              //31
        dt.Columns.Add(new DataColumn("ALLOCATEDQTY", typeof(string)));             //32
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));                   //33
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));                 //34

        #region Loop For Adding Itemwise Tax Component
        if (hdnReceivedID.Value == "")
        {
            DataSet ds = new DataSet();
            string despatchID = Convert.ToString(ddldespatchno.SelectedValue).Trim();
            ds = clsReceivedStock.EditDespatchDetails(despatchID);
            Session["dtReceivedTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string receivedID = Convert.ToString(hdnReceivedID.Value).Trim();
            ds = clsReceivedStock.EditReceivedDetails(receivedID);
            Session["dtReceivedTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        #endregion

        dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        HttpContext.Current.Session["DESPATCHDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.hdnReceivedID.Value = "";
            this.trAutoReceivedNo.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
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
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            this.grdTax.ClearPreviousDataSource();
            this.grdTax.DataSource = null;
            this.grdTax.DataBind();
            clsReceivedStock.ResetDataTables();
            this.ClearControls();
            this.LoadMotherDepot();
            this.LoadInsurance();
            #region Enable Controls
            this.imgbtnCalendar.Visible = true;
            #endregion
            this.ddldespatchno.Enabled = true;
            this.ddldespatchno.SelectedValue = "0";
            this.txtDespatchNo.Text = "";

            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            this.LoadDespatchNo();
            this.LoadReason();
            this.txtdeliverydate.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadTax
    public void LoadTax()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            if (hdnReceivedID.Value == "")
            {
                DataSet ds = new DataSet();
                string despatchID = Convert.ToString(this.txtDespatchNo.Text);
                ds = clsReceivedStock.EditDespatchDetails(despatchID);
                Session["GrossReceivedTotalTax"] = ds.Tables[4];
                if (ds.Tables[4].Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["GrossReceivedTotalTax"];
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
                DataSet ds = new DataSet();
                string despatchID = Convert.ToString(hdnReceivedID.Value);
                ds = clsReceivedStock.EditReceivedDetails(despatchID);
                Session["GrossReceivedTotalTax"] = ds.Tables[4];
                if (ds.Tables[4].Rows.Count > 0)
                {
                    this.grdTax.DataSource = (DataTable)Session["GrossReceivedTotalTax"];
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

    #region LoadTermsConditions
    public void LoadTermsConditions()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DataTable dt = new DataTable();
            dt = clsReceivedStock.BindTerms(ddldespatchno.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                this.grdTerms.DataSource = dt;
                this.grdTerms.DataBind();
            }
            else
            {
                this.grdTerms.DataSource = dt;
                this.grdTerms.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadMotherDepot
    public void LoadMotherDepot()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.ddlDepot.Items.Clear();
            this.ddlDepot.Items.Add(new ListItem("Depot Name", "0"));
            this.ddlDepot.AppendDataBoundItems = true;
            this.ddlDepot.DataSource = clsReceivedStock.BindDepot();
            this.ddlDepot.DataValueField = "BRID";
            this.ddlDepot.DataTextField = "BRNAME";
            this.ddlDepot.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTransporter
    public void LoadTransporter()
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.ddlTransporter.Items.Clear();
            this.ddlTransporter.Items.Add(new ListItem("Transporter Name", "0"));
            this.ddlTransporter.AppendDataBoundItems = true;
            this.ddlTransporter.DataSource = clsReceivedStock.BindTPU_Transporter();
            this.ddlTransporter.DataValueField = "ID";
            this.ddlTransporter.DataTextField = "NAME";
            this.ddlTransporter.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region CreateReceivedTaxDatatable
    protected void CreateReceivedTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string TAXID, string MRP)
    {
        ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
        DataTable dt = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["POID"] = POID;
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

    #region UpdateReceivedTaxDatatable
    void UpdateReceivedTaxDatatable(string POID, string PRODUCTID, string BATCH, string TAXID, string TAXPERCENTAGE, string VALUES)
    {
        DataTable dt = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];

        for (int p = 0; p < dt.Rows.Count; p++)
        {
            if (Convert.ToString(dt.Rows[p]["POID"]) == POID && Convert.ToString(dt.Rows[p]["PRODUCTID"]) == PRODUCTID && Convert.ToString(dt.Rows[p]["BATCHNO"]) == BATCH && Convert.ToString(dt.Rows[p]["TAXID"]) == TAXID && Convert.ToString(dt.Rows[p]["PERCENTAGE"]) == TAXPERCENTAGE)
            {
                dt.Rows[p]["TAXVALUE"] = VALUES;
                break;
            }
        }
        dt.AcceptChanges();
        Session["RECEIVEDTAXCOMPONENTDETAILS"] = dt;
    }
    #endregion

    #region CalculateGrossTotal
    decimal CalculateGrossTotal(DataTable dt)
    {
        decimal GrossTotal = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            //GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AFTERITEMWISEADDCOSTAMT"]);
        }
        return GrossTotal;
    }
    #endregion

    #region CalculateGrossTotal
    decimal CalculateGrossTotal_PStockReceipt(DataTable dt)
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

    #region ddldespatchno_SelectedIndexChanged
    protected void ddldespatchno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(Request.QueryString["CHECKER"].ToString().Trim() != "TRUE")
            {
                if (ddldespatchno.SelectedValue == "0")
                {
                    this.ClearControls();
                    this.ResetSession();
                }
                else
                {
                    try
                    {
                        ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
                        clsDespatchStock clsDespatchStock = new clsDespatchStock();
                        ClsGRNMM ClsGRNMM = new ClsGRNMM();
                        ClsStockTransfer clsTrn = new ClsStockTransfer();
                        this.CreateRejectionTotalDataTable();
                        this.CreateRejectionInnerGridDataTable();// Creating Rejection Inner Grid DataTable Structure
                        this.CreateDataTable();// Creating DataTable Structure
                        this.CreateDataTableTaxComponent();// Creating DataTable Structure
                        decimal TotalMRP = 0;
                        decimal TotalAmount = 0;
                        decimal TotalTax = 0;
                        decimal GrossTotal = 0;
                        decimal DepotRate1 = 0;
                        string TAXID = string.Empty;
                        decimal ProductWiseTax = 0;

                        DataSet ds = new DataSet();
                        pnlDisplay.Style["display"] = "none";
                        pnlAdd.Style["display"] = "";
                        btnaddhide.Style["display"] = "none";

                        string despatchID = Convert.ToString(ddldespatchno.SelectedValue);
                        ds = clsReceivedStock.EditDespatchDetails(despatchID);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            #region Header Information
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESPATCHDATE"]);
                                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                                this.hdnWaybillNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]);
                                this.txtWayBill.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]);
                                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                                this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                                this.LoadTPU();
                                this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                                this.LoadMotherDepot();
                                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                                this.LoadTransporter();
                                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                                this.LoadInsurance();
                                this.ddlinsurancename.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]);
                                this.txtinsuranceno.Text = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]);
                                this.txtdeliverydate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERYDATE"]).Trim();
                                if (Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]) == "01/01/1900")
                                {
                                    this.txtCFormDate.Text = "";
                                }
                                else
                                {
                                    this.txtCFormDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]);
                                }
                                this.txtTotalCase.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALCASEPACK"]);
                                this.txtTotalPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]);
                            }
                            #endregion

                            #region Item-wise Tax Component
                            if (ds.Tables[6].Rows.Count > 0)
                            {
                                DataTable dtTaxComponentEdit = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                {
                                    DataRow dr = dtTaxComponentEdit.NewRow();
                                    dr["POID"] = Convert.ToString(ds.Tables[6].Rows[i]["POID"]);
                                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTNAME"]);
                                    dr["TAXNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["NAME"]);
                                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                                    dtTaxComponentEdit.Rows.Add(dr);
                                    dtTaxComponentEdit.AcceptChanges();
                                }
                                HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
                            }
                            #endregion

                            #region Details Information
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                decimal excise = 0;

                                #region Loop For Adding Itemwise Tax Component Into Arry
                                DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];

                                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                                {
                                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                }
                                #endregion

                                dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    DepotRate1 = clsTrn.BindDepotRate(Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]), Convert.ToDecimal(ds.Tables[1].Rows[i]["MRP"].ToString()), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]).Trim());

                                    DataRow dr = dtDespatchEdit.NewRow();
                                    dr["GUID"] = Guid.NewGuid();
                                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();
                                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]).Trim();
                                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]).Trim();
                                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]).Trim();
                                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]).Trim();
                                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]).Trim();
                                    dr["REMAININGQTY"] = Convert.ToString(0);
                                    dr["REASONID"] = Convert.ToString(0);
                                    dr["REASONNAME"] = Convert.ToString("None").Trim();
                                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();
                                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                                    dr["DEPOTRATE"] = Convert.ToString(DepotRate1).Trim();
                                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();
                                    dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                                    dr["TOTALASSESABLEVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                                    dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();
                                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();
                                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();
                                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();
                                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();
                                    decimal BillTaxAmt = 0;
                                    #region Loop For Adding Itemwise Tax Component

                                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                                    {
                                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                                        {
                                            case "1":
                                                TAXID = clsDespatchStock.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                                ProductWiseTax = ClsGRNMM.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * ProductWiseTax / 100));
                                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                                break;
                                        }
                                    }
                                    #endregion
                                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim())));
                                    dtDespatchEdit.Rows.Add(dr);
                                    dtDespatchEdit.AcceptChanges();
                                }
                                HttpContext.Current.Session["DESPATCHDETAILS"] = dtDespatchEdit;

                                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                                //TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                                TotalAmount = CalculateGrossTotal_PStockReceipt(dtDespatchEdit);
                                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                                GrossTotal = TotalAmount + TotalTax;

                                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                                this.LoadTermsConditions();

                                #region grdAddDespatch DataBind
                                this.grdAddDespatch.DataSource = dtDespatchEdit;
                                this.grdAddDespatch.DataBind();
                                if (ViewState["count"] == null)
                                {
                                    ViewState["count"] = "Y";
                                }
                                #endregion

                                DataTable dtProductTax = new DataTable();
                                if (Session["RECEIVEDTAXCOMPONENTDETAILS"] != null)
                                {
                                    dtProductTax = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                                }

                                if (dtProductTax.Rows.Count > 0)
                                {
                                    this.gvProductTax.DataSource = dtProductTax;
                                    this.gvProductTax.DataBind();
                                }
                                else
                                {
                                    this.gvProductTax.DataSource = dtProductTax;
                                    this.gvProductTax.DataBind();
                                }
                            }
                            else
                            {
                                this.grdAddDespatch.DataSource = null;
                                this.grdAddDespatch.DataBind();
                            }
                            #endregion

                            #region Amount-Calculation
                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());
                                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
                            }
                            #endregion

                            #region SaleOrder
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                this.hdnSaleOrderID.Value = ds.Tables[7].Rows[0]["SALEORDERID"].ToString();
                                this.hdnSaleOrderNo.Value = ds.Tables[7].Rows[0]["SALEORDERNO"].ToString();
                            }
                            #endregion

                            if (this.grdAddDespatch.Rows.Count > 0)
                            {
                                this.GridStyle();
                                GridCalculation_TpuDespatchReceipt();
                            }
                        }
                        else
                        {
                            MessageBox1.ShowInfo("<b><font color='red'>Waybill No</font></b> not found, please updated before stock received", 100, 500);
                        }

                    }
                    catch (Exception ex)
                    {
                        string message = "alert('" + ex.Message.Replace("'", "") + "')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

    #region TCS(%) & TCS Limit
    protected void BindTCSDetails(string VendorID)
    {
        clsDespatchStock clsDespatchStock = new clsDespatchStock();
        DataTable dtTCS = new DataTable();
        dtTCS = clsDespatchStock.BindTPUTCSPercent(VendorID);
        this.txtTCSPercent.Text = Convert.ToString(dtTCS.Rows[0]["TCS_PERCENT"]).Trim();
        //if (this.ddlDepot.SelectedValue != "0")
        //{

        //if (dtTCS.Rows.Count > 0)
        //{


        //if (this.txtTCSPercent.Text.Trim() == "0.0000")
        //{
        //    this.txtTCSPercent.Text = "0.075";
        //}


        //}
        //else
        //{
        //    this.txtTCSPercent.Text = "0.075";


        //}

        //}
    }
    #endregion

    #region TCS Calculation
    protected void RbApplicable_SelectedIndexChanged(object sender, EventArgs e)


    {
        if (RbApplicable.SelectedValue == "Y")
        {

            this.BindTCSDetails(this.ddlTPU.SelectedValue.Trim());
            this.txtTCSApplicable.Enabled = false;

            decimal TCSAmt = 0;
            decimal NetAmtWithTCS = 0;
            decimal FinalAmt = Convert.ToDecimal(this.txtFinalAmt.Text.Trim());
            decimal TCSPercent = Convert.ToDecimal(this.txtTCSPercent.Text.Trim());

            //this.txtTCSApplicable


            TCSAmt = Math.Ceiling((FinalAmt * TCSPercent) / 100);
            NetAmtWithTCS = TCSAmt + Convert.ToDecimal(txtFinalAmt.Text.Trim());

            /*TCS Related Controls*/
            this.txtTCS.Text = Convert.ToString(TCSAmt);
            this.txtTCSNetAmt.Text = Convert.ToString(NetAmtWithTCS);
            this.txtTCSApplicable.Text = this.txtFinalAmt.Text.Trim();
            /*TCS Related Controls*/
        }
        else
        {

            this.txtTCSPercent.Text = "0";
            this.txtTCS.Text = "0";
            this.txtTCSNetAmt.Text = "0";
            this.txtTCSApplicable.Text = "0";
            this.txtTCSApplicable.Enabled = false;
        }
    }
    #endregion

    #region TCS Calculation Textchanged Event
    protected void txtTCSApplicable_TextChanged(object sender, EventArgs e)


    {
        {

            this.BindTCSDetails(this.ddlTPU.SelectedValue.Trim());


            decimal TCSAmt = 0;
            decimal NetAmtWithTCS = 0;

            decimal TCSPercent = Convert.ToDecimal(this.txtTCSPercent.Text.Trim());


            TCSAmt = Math.Ceiling((Convert.ToDecimal(this.txtTCSApplicable.Text.Trim()) * TCSPercent) / 100);
            NetAmtWithTCS = TCSAmt + Convert.ToDecimal(txtFinalAmt.Text.Trim());

            /*TCS Related Controls*/
            this.txtTCS.Text = Convert.ToString(TCSAmt);
            this.txtTCSNetAmt.Text = Convert.ToString(NetAmtWithTCS);
            this.txtTCSApplicable.Text = this.txtTCSApplicable.Text.Trim();
            /*TCS Related Controls*/
        }

    }
    #endregion

    #region GridStyle
    protected void GridStyle()
    {
        this.grdAddDespatch.HeaderRow.Cells[4].Text = "PO DATE";
        this.grdAddDespatch.HeaderRow.Cells[6].Text = "PRODUCT";
        this.grdAddDespatch.HeaderRow.Cells[7].Text = "HSN CODE";
        this.grdAddDespatch.HeaderRow.Cells[9].Text = "PACKSIZE";
        this.grdAddDespatch.HeaderRow.Cells[11].Text = "DSPCH. QTY";
        this.grdAddDespatch.HeaderRow.Cells[12].Text = "RCVD. QTY";
        //this.grdAddDespatch.HeaderRow.Cells[22].Text = "TOT. MRP";
        //this.grdAddDespatch.HeaderRow.Cells[23].Text = "BATCH";
        //this.grdAddDespatch.HeaderRow.Cells[23].Text = "NET WGHT.";
        //this.grdAddDespatch.HeaderRow.Cells[27].Text = "GROSS WGHT.";
        this.grdAddDespatch.HeaderRow.Cells[29].Text = "MFG.DATE";
        this.grdAddDespatch.HeaderRow.Cells[30].Text = "EXP.DATE";
    }
    #endregion   

    #region BindDespatchGrid
    //protected void BindDespatchGrid()
    //{
    //    this.grdAddDespatch.DataSource = null;
    //    this.grdAddDespatch.DataBind();
    //    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];

    //    DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];

    //    if (ViewState["count"] == null)
    //    {
    //        ViewState["count"] = "Y";
    //    }

    //    this.grdAddDespatch.DataSource = dtDespatchEdit;
    //    this.grdAddDespatch.DataBind();
    //} 
    #endregion 

    #region BindEditedDespatchGrid
    //protected void BindEditedDespatchGrid()
    //{
    //    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];

    //    this.grdAddDespatch.DataSource = dtDespatchEdit;
    //    this.grdAddDespatch.DataBind();
    //} 
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

    #region btnSubmit_Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DateTime dtcurr = DateTime.Now;
            int flag = 0;
            string InvoiceType = string.Empty;
            dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];

            DataTable dtrejectiondetails = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];

            for (int i = 0; i < dtDespatchEdit.Rows.Count; i++)
            {
                //Label txtcurrentreceivedqty = (Label)grdAddDespatch.Rows[i].Cells[7].FindControl("txtcurrentreceivedqty");
                string ReceivedPacksizeid = dtDespatchEdit.Rows[i]["PACKINGSIZEID"].ToString().Trim();
                string BatchNO = Convert.ToString(dtDespatchEdit.Rows[i]["BATCHNO"]).Trim();
                string ProductID = Convert.ToString(dtDespatchEdit.Rows[i]["PRODUCTID"]).Trim();
                decimal despatchqty = Convert.ToDecimal(dtDespatchEdit.Rows[i]["QTY"].ToString().Trim());
                decimal receivedqty = Convert.ToDecimal(dtDespatchEdit.Rows[i]["QTY"].ToString().Trim());

                decimal totalrejectionqty = 0;
                decimal rejectionqty = 0;

                if (dtrejectiondetails != null)
                {
                    if (dtrejectiondetails.Rows.Count > 0)
                    {
                        for (int p = 0; p < dtrejectiondetails.Rows.Count; p++)
                        {
                            if (dtrejectiondetails.Rows[p]["PRODUCTID"].ToString().Trim() == ProductID && dtrejectiondetails.Rows[p]["BATCHNO"].ToString().Trim() == BatchNO)
                            {
                                rejectionqty = clsReceivedStock.ConvertionQty(ProductID, dtrejectiondetails.Rows[p]["PACKINGSIZEID"].ToString(), ReceivedPacksizeid, Convert.ToDecimal(dtrejectiondetails.Rows[p]["REJECTIONQTY"]));
                            }
                            totalrejectionqty = totalrejectionqty + rejectionqty;
                        }
                    }
                }
                if (totalrejectionqty > despatchqty)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Rejection qty should not be greater than received qty</font></b> for product <b><font color='green'>" + dtDespatchEdit.Rows[i]["PRODUCTNAME"] + "</font></b>!", 80, 750);
                    flag = 1;
                    break;
                }

            }

            if (flag == 0)
            {

                for (int i = 0; i < dtDespatchEdit.Rows.Count; i++)
                {
                    //Label txtcurrentreceivedqty = (Label)grdAddDespatch.Rows[i].Cells[7].FindControl("txtcurrentreceivedqty");

                    string ReceivedPacksizeid = Convert.ToString(dtDespatchEdit.Rows[i]["PACKINGSIZEID"]).Trim();
                    string BatchNO = Convert.ToString(dtDespatchEdit.Rows[i]["BATCHNO"]).Trim();
                    string ProductID = Convert.ToString(dtDespatchEdit.Rows[i]["PRODUCTID"]).Trim();
                    decimal despatchqty = Convert.ToDecimal(dtDespatchEdit.Rows[i]["QTY"].ToString().Trim());
                    decimal receivedqty = Convert.ToDecimal(dtDespatchEdit.Rows[i]["QTY"].ToString().Trim());

                    decimal totalrejectionqty1 = 0;
                    decimal rejectionqty1 = 0;

                    if (dtrejectiondetails != null)
                    {
                        if (dtrejectiondetails.Rows.Count > 0)
                        {
                            for (int p = 0; p < dtrejectiondetails.Rows.Count; p++)
                            {
                                if (dtrejectiondetails.Rows[p]["PRODUCTID"].ToString().Trim() == ProductID && dtrejectiondetails.Rows[p]["BATCHNO"].ToString().Trim() == BatchNO)
                                {
                                    rejectionqty1 = clsReceivedStock.ConvertionQty(ProductID, dtrejectiondetails.Rows[p]["PACKINGSIZEID"].ToString(), ReceivedPacksizeid, Convert.ToDecimal(dtrejectiondetails.Rows[p]["REJECTIONQTY"]));
                                }
                                totalrejectionqty1 = totalrejectionqty1 + rejectionqty1;
                            }
                        }
                    }

                    decimal remainingqty = despatchqty - receivedqty - totalrejectionqty1;


                    dtDespatchEdit.Rows[i]["RECEIVEDQTY"] = Convert.ToDecimal(receivedqty);
                    dtDespatchEdit.Rows[i]["REMAININGQTY"] = remainingqty;
                    dtDespatchEdit.Rows[i]["REASONID"] = "";
                    dtDespatchEdit.Rows[i]["REASONNAME"] = "";

                    dtDespatchEdit.AcceptChanges();

                    HttpContext.Current.Session["DESPATCHDETAILS"] = dtDespatchEdit;
                }
                ClsGRNMM Clsdespatch = new ClsGRNMM();
                string receivedno = Clsdespatch.CheckInvoiceNoGRN(this.txtDespatchNo.Text, this.ddlTPU.SelectedValue.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnReceivedID.Value);

                if (receivedno == null || hdnReceivedID.Value.ToString() != "")
                {
                    string ReceivedNo = string.Empty;
                    string xml = string.Empty;
                    string xmlTax = string.Empty;
                    string xmlGrossTax = string.Empty;
                    string xmlRejectionDetails = string.Empty;
                    string strTaxID = string.Empty;
                    string strTaxPercentage = string.Empty;
                    string strTaxValue = string.Empty;
                    string strTermsID = string.Empty;
                    string allocationDate = string.Empty;
                    decimal TotalCase = 0;
                    decimal TotalPCS = 0;
                    DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                    DataTable dtTaxRecordsCheck = (DataTable)HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"];
                    //DataTable dtGrossTaxRecordsCheck = (DataTable)HttpContext.Current.Session["GrossReceivedTotalTax"];
                    DataTable dtRejectionDetailsTotal = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];
                    DataTable dtTaxCount = (DataTable)Session["dtReceivedTaxCount"];
                    menuID = Request.QueryString["MENUID"].ToString().Trim();

                    if (dtRecordsCheck.Rows.Count > 0)
                    {
                        xml = ConvertDatatableToXML(dtRecordsCheck);
                        if (dtTaxRecordsCheck != null)
                        {
                            if (dtTaxRecordsCheck.Rows.Count > 0)
                            {
                                xmlTax = ConvertDatatableToXML(dtTaxRecordsCheck);
                            }
                        }
                        //if (dtGrossTaxRecordsCheck != null)
                        //{
                        //    if (dtGrossTaxRecordsCheck.Rows.Count > 0)
                        //    {
                        //        xmlGrossTax = ConvertDatatableToXML(dtGrossTaxRecordsCheck);
                        //    }
                        //}
                        if (dtRejectionDetailsTotal != null)
                        {
                            if (dtRejectionDetailsTotal.Rows.Count > 0)
                            {
                                xmlRejectionDetails = ConvertDatatableToXML(dtRejectionDetailsTotal);
                            }
                        }

                        #region Amount

                        decimal OtherCharges = 0;
                        decimal Adjustment = 0;
                        decimal Roundoff1 = Convert.ToDecimal(this.txtRoundoff.Text);
                        if (this.txtOtherCharge.Text == "")
                        {
                            OtherCharges = 0;
                        }
                        else
                        {
                            OtherCharges = Convert.ToDecimal(this.txtOtherCharge.Text.Trim());
                        }

                        if (this.txtAdj.Text == "")
                        {
                            Adjustment = 0;
                        }
                        else
                        {
                            Adjustment = Convert.ToDecimal(this.txtAdj.Text.Trim());
                        }

                        //decimal TotalDespatch = Convert.ToDecimal(this.txtTotalGross.Text) + Adjustment + Roundoff1 + OtherCharges;
                        decimal TotalDespatch = Convert.ToDecimal(this.txtFinalAmt.Text);

                        #endregion

                        int receiveddate = Convert.ToInt32(Conver_To_ISO(this.txtreceiveddate.Text.Trim()));
                        int despatchdate = Convert.ToInt32(Conver_To_ISO(this.txtDespatchDate.Text.Trim()));

                        if (despatchdate > receiveddate)
                        {
                            MessageBox1.ShowInfo("<b><font color='red'>Stock Received date can not less than Despatch date</font></b>");
                        }
                        else
                        {
                            if (this.hdnSaleOrderID.Value.Trim() == "")
                            {
                                this.hdnSaleOrderID.Value = "0";
                            }
                            if (this.hdnSaleOrderNo.Value.Trim() == "")
                            {
                                this.hdnSaleOrderNo.Value = "NA";
                            }
                            if (this.txtTotalCase.Text.Trim() != "")
                            {
                                TotalCase = Convert.ToDecimal(this.txtTotalCase.Text.Trim());
                            }
                            if (this.txtTotalPCS.Text.Trim() != "")
                            {
                                TotalPCS = Convert.ToDecimal(this.txtTotalPCS.Text.Trim());
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

                            ReceivedNo = clsReceivedStock.InsertDespatchDetails(this.txtreceiveddate.Text.Trim(), this.txtDespatchNo.Text, this.txtDespatchDate.Text.Trim(), this.ddlTPU.SelectedValue, this.ddlTPU.SelectedItem.Text, this.hdnWaybillNo.Value.ToString(), this.txtWayBill.Text.Trim(), this.txtInvoiceNo.Text.Trim(),
                                                                                    this.txtInvoiceDate.Text.Trim(),
                                                                                    this.ddlTransporter.SelectedValue, this.txtVehicle.Text.Trim(), this.ddlDepot.SelectedValue,
                                                                                    Convert.ToString(this.ddlDepot.SelectedItem), this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(), this.txtCFormNo.Text.Trim(),
                                                                                    this.txtCFormDate.Text.Trim(), this.txtGatepassNo.Text.Trim(), this.txtGatepassDate.Text.Trim(),
                                                                                    this.ddlTransportMode.SelectedValue, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                                                                                    HttpContext.Current.Session["FINYEAR"].ToString(), this.txtRemarks.Text.Trim(), TotalDespatch, OtherCharges,
                                                                                    Convert.ToDecimal(txtOtherCharge.Text), Roundoff1,
                                                                                    strTermsID, xml, xmlTax, "", xmlRejectionDetails,
                                                                                    Convert.ToString(hdnReceivedID.Value), this.ddlinsurancename.SelectedValue.Trim(),
                                                                                    this.ddlinsurancename.SelectedItem.ToString().Trim(), this.txtinsuranceno.Text.Trim(),
                                                                                    menuID, this.hdnSaleOrderID.Value.Trim(), this.hdnSaleOrderNo.Value.Trim(),
                                                                                    TotalCase, TotalPCS, InvoiceType.Trim(), "N", this.txtdeliverydate.Text.Trim());


                            if (ReceivedNo != "")
                            {

                                grdAddDespatch.DataSource = null;
                                grdAddDespatch.DataBind();

                                clsReceivedStock.ResetDataTables();   // Reset all Datatables

                                if (Convert.ToString(hdnReceivedID.Value) == "")
                                {
                                    MessageBox1.ShowSuccess("Stock Received : <b><font color='green'>" + ReceivedNo + "</font></b> saved successfully!", 60, 550);

                                }
                                else
                                {
                                    MessageBox1.ShowSuccess("Stock Received : <b><font color='green'>" + ReceivedNo + "</font></b> updated successfully!", 60, 550);

                                }
                                this.LoadReceived();
                                this.pnlAdd.Style["display"] = "none";
                                this.pnlDisplay.Style["display"] = "";
                                this.btnaddhide.Style["display"] = "";
                                this.hdnReceivedID.Value = "";
                                this.ResetSession();
                                this.ClearControls();
                                HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = null;
                                HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = null;
                                ViewState["count"] = null;
                                this.txtdeliverydate.Text = "";
                            }
                            else
                            {
                                MessageBox1.ShowError("Error on Saving record!");

                            }
                        }
                    }
                    else
                    {

                        MessageBox1.ShowInfo("Please add atleast 1 record!");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("You have already received this Despatch Stock on Received No- <b><font color='green'>" + receivedno + "</font></b>", 60, 500);
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

    #region ResetSession
    public void ResetSession()
    {
        dtDespatchEdit = null;
        grdAddDespatch.DataSource = null;
        grdAddDespatch.DataBind();
        Session["DESPATCHDETAILS"] = null;
        Session["GrossReceivedTotalTax"] = null;
        Session["dtReceivedTaxCount"] = null;
        Session["RECEIVEDTAXCOMPONENTDETAILS"] = null;
        Session["Terms"] = null;
    }
    #endregion

    #region btnApprove_Click
    // Added By Avishek Ghosh On 11-03-2016
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlledger.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>SELECT LEDGER NAME</font></b>");
            }
            else
            {

                ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
                ClsReceivedStock clsReceivedStock = new ClsReceivedStock();


                ClsGRNMM Clsdespatch = new ClsGRNMM();
                string receivedno = Clsdespatch.CheckInvoiceNoGRN(this.txtDespatchNo.Text, this.ddlTPU.SelectedValue.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnReceivedID.Value);

                if(receivedno=="1")
                {
                    MessageBox1.ShowWarning("Invoice No Already Exists");
                    return;
                }

                int flag = 0;
                int update = 0;
                string receivedID = Convert.ToString(hdnReceivedID.Value).Trim();
                if (txtRoundoff.Text == "")
                {
                    txtRoundoff.Text = "0";
                }
                if (Convert.ToDecimal(this.txtTCS.Text) == 0)
                {
                    this.txtTCSApplicable.Text = "0";
                    this.txtTCSPercent.Text = "0";
                    this.txtTCS.Text = "0";
                    this.txtTCSNetAmt.Text = "0";
                }
                update = Clsdespatch.updateTableValue(receivedID, this.txtDespatchNo.Text, this.txtWayBill.Text, this.txtEInvoiceNo.Text, Convert.ToDecimal(this.txtRoundoff.Text), Convert.ToDecimal(this.txtFinalAmt.Text)
                    , Convert.ToDecimal(this.txtTCSApplicable.Text), Convert.ToDecimal(this.txtTCS.Text), Convert.ToDecimal(this.txtTCSNetAmt.Text));
                flag = clsPurchaseStockReceipt.ApproveStockReceived(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtRceivedNo.Text.Trim(), this.ddlinsurancename.SelectedValue.Trim(), this.ddlinsurancename.SelectedItem.ToString().Trim(), this.txtInvoiceDate.Text.Trim(), this.txtinsuranceno.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), "TRUE", ddlledger.SelectedValue.ToString().Trim(), txtFinalAmt.Text, txtTotTax.Text,this.ddldespatchno.SelectedItem.Text, this.txtInvoiceDate.Text,this.txtWayBill.Text,this.txtEInvoiceNo.Text);

               

                if (flag == 1)
                {
                    pnlDisplay.Style["display"] = "";
                    pnlAdd.Style["display"] = "none";
                    LoadReceived();
                    clsReceivedStock.UpdateExchangeRate(receivedID, txtexchangrate.Text);
                    clsReceivedStock.Updateledgerinfo(receivedID,ddlledger.SelectedValue.ToString().Trim());
                    MessageBox1.ShowSuccess("Purchase Stock Received: <b><font color='green'>" + this.txtRceivedNo.Text + "</font></b> approved and accounts entry(s) passed successfull.", 60, 700);

                    //this.ClearControls();
                    //this.ResetSession();

                    //DataTable dt = new DataTable();
                    //ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                    //dt = clsMMPo.Bind_Sms_Mobno("I", receivedID);
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                    //}
                    //DataTable dt1 = new DataTable();
                    //dt1 = clsMMPo.Bind_Sms_Mobno("J", receivedID);
                }
                else if (flag == 0)
                {
                    pnlDisplay.Style["display"] = "none";
                    pnlAdd.Style["display"] = "";
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

    #region btnReject_Click
    // Added By Avishek Ghosh On 11-03-2016
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
    // Added By Avishek Ghosh On 15-03-2016
    protected void btnRejectionNoteSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdnReceivedID.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectStockReceived(receivedID, this.txtRejectionNote.Text.Trim());
            this.hdnReceivedID.Value = "";

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                LoadReceived();
                MessageBox1.ShowSuccess("Purchase Stock Received: <b><font color='green'>" + this.txtRceivedNo.Text + "</font></b> rejected successfully.", 60, 500);
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
    // Added By Avishek Ghosh On 15-03-2016
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

    #region btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            this.txtRceivedNo.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            pnlAdd.Style["display"] = "none";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                btnaddhide.Style["display"] = "none";
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                btnaddhide.Style["display"] = "";
            }
            #endregion

            clsReceivedStock.ResetDataTables();
            this.ClearControls();
            this.ResetSession();
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            this.grdTax.ClearPreviousDataSource();
            this.grdTax.DataSource = null;
            this.grdTax.DataBind();
            this.grdTerms.ClearPreviousDataSource();
            this.grdTerms.DataSource = null;
            this.grdTerms.DataBind();
            this.hdnReceivedID.Value = "";
           // this.LoadReceived();
            HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = null;
            HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = null;
            this.txtdeliverydate.Text = "";
            ViewState["count"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnSearchDespatch_Click
    protected void btnSearchDespatch_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReceived();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Edit Received
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            clsDespatchStock ClsDespatchStock = new clsDespatchStock();
            string PURCHASERETURNID = Convert.ToString(hdnReceivedID.Value).Trim();
            ClsGRNMM ClsGRNMM = new ClsGRNMM();
            DataTable dtDespatchEdit = new DataTable();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            if (Checker == "FALSE")
            {
                if (clsReceivedStock.Getstatus(this.hdnReceivedID.Value.Trim()) == "1")
                {
                    MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
            }

            DataSet ds = new DataSet();
            this.trAutoReceivedNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.txtRceivedNo.Style["display"] = "";
            this.txtTCSApplicable.Text = "0";
            this.txtTCSPercent.Text = "0";
            this.txtTCSLimit.Text = "0";
            this.txtTCS.Text = "0";
            this.txtTCSNetAmt.Text = "0";

            #region QueryString
            /*if (Checker == "TRUE")
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
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }*/

            if (clsReceivedStock.GetApproveStatus(PURCHASERETURNID) == "1" && Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
                this.divDocuments.Visible = false;
            }
            else if (clsReceivedStock.GetApproveStatus(PURCHASERETURNID) == "0" && Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = false;
            }
            else if (clsReceivedStock.GetApproveStatus(PURCHASERETURNID) == "1" && Checker == "FALSE")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnReceivedID.Value);
            ds = clsReceivedStock.EditReceivedDetails(receivedID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtRceivedNo.Enabled = false;
                if (Checker == "TRUE")
                {
                    this.ddldespatchno.Enabled = true;
                    this.txtInvoiceDate.Enabled = false;
                }
                else
                {
                    this.ddldespatchno.Enabled = false;
                    this.txtInvoiceDate.Enabled = false;
                }
               
                this.txtRceivedNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDNO"]);
                this.LoadEditedDespatchNo();
                this.ddldespatchno.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["STOCKDESPATCHID"]);
                this.txtDespatchNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                this.txtreceiveddate.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDDATE"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESPATCHDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                this.hdnWaybillNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]);
                this.txtWayBill.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]);
                this.txtEInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["EINVOICENO"]);
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                this.LoadTPU();
                this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                this.LoadMotherDepot();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                this.LoadTransporter();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                this.LoadInsurance();
                this.ddlinsurancename.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]);
                this.txtinsuranceno.Text = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]);
                this.txtexchangrate.Text = Convert.ToString(ds.Tables[1].Rows[0]["EXCHANGERATE"]);
                this.lblInrRate.Text = "INR&nbsp;RATE:- " + Convert.ToString(ds.Tables[1].Rows[0]["INRRATE"]);
                this.txtdeliverydate.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString().Trim();
                if (Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]) == "")
                {
                    this.FetchLedger();
                    this.ddlledger.Enabled = true;
                    //this.ddlledger.SelectedValue = "0";
                }
                else
                {
                    //this.FetchLedger();
                    this.ddlledger.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]);
                    this.ddlledger.Enabled = false;
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]) == "01/01/1900")
                {
                    this.txtCFormDate.Text = "";
                }
                else
                {
                    this.txtCFormDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]);
                }

                this.txtGatepassNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtGatepassDate.Text = "";
                }
                else
                {
                    this.txtGatepassDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);
                this.hdnSaleOrderID.Value = Convert.ToString(ds.Tables[0].Rows[0]["SALEORDERID"]);
                this.hdnSaleOrderNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["SALEORDERNO"]);
                this.txtTotalCase.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALCASE"]);
                this.txtTotalPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]);
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["POID"] = Convert.ToString(ds.Tables[6].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTNAME"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["NAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region TCS
            if (Convert.ToDecimal(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]) > 0)
            {

                this.txtTCSApplicable.Enabled = false;
                this.RbApplicable.SelectedValue = "Y";
                this.txtTCSPercent.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCS_PERCENT"]);
                this.txtTCS.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAMOUNT"]);
                this.txtTCSNetAmt.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]);
                this.txtTCSApplicable.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAPPLICABLE_AMOUNT"]);


            }
            else
            {
                this.RbApplicable.SelectedValue = "N";
                this.txtTCSApplicable.Text = "0";
                this.txtTCSPercent.Text = "0";
                this.txtTCSLimit.Text = "0";
                this.txtTCS.Text = "0";
                this.txtTCSNetAmt.Text = "0";

            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();  //2
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();//1+2
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]).Trim();//2+2
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();//3+2
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();//4+2
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();//5+2
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]).Trim();//6+2
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]).Trim();//7+2
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();//8+2
                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]).Trim();//9+2
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]).Trim();//10+2
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]).Trim();//11+2
                    dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]).Trim();//12+2
                    dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]).Trim();//13+2
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();//14+2
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]).Trim();//15+2
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();//16+2
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();//17+2
                    dr["QCREJECTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QCREJECTQTY"]).Trim();//18+2
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();//19+2
                    dr["TOTALASSESABLEVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);//20+2
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);//21+2
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);//22+2
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);//23+2
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);//24+2
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);//26+2
                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);//27+2
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);//28+2

                    dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();//29+2
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();//30+2
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();//31+2
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();//32+2
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();//33+2
                    decimal BillTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {

                            case "1":
                                TAXID = ClsDespatchStock.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = ClsGRNMM.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = ClsGRNMM.GetHSNTaxOnEdit(hdnReceivedID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                break;

                            case "4":
                            //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                            //break;

                            case "5":
                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                //                        Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                //                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                //excise = Convert.ToDecimal(String.Format("{0:0.00}",
                                //                                Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                //                                Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));


                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                //                                 (Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) > 0) ? (Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                //                                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100)
                                //                                       : Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * Convert.ToDecimal(Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString())) / 100 *
                                //                                Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                //excise = Convert.ToDecimal(String.Format("{0:0.00}",
                                //                                       (Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) > 0) ? (Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                //                                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100)
                                //                                       : Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                //                                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                break;
                            case "7":

                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                //                     (Convert.ToDecimal(dr["AMOUNT"]) + excise) *
                                //                      Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));


                                break;

                        }
                    }
                    #endregion

                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString().Trim())));//38+2 for cgst and 36+2
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();

                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region Tax On Gross Amount
                //this.LoadTax();
                this.LoadTermsConditions();
                //DataTable dtGrossTax = (DataTable)Session["GrossReceivedTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionTotalDataTable();
                    DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        DataRow dr = dttotalrejection.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKRECEIVEDID"]);
                        dr["POID"] = Convert.ToString(ds.Tables[7].Rows[i]["POID"]);
                        dr["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKDESPATCHID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[7].Rows[i]["BATCHNO"]);
                        dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[7].Rows[i]["REJECTIONQTY"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZENAME"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONNAME"]);
                        dr["DEPOTRATE"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE"]);
                        // Added By Avishek Ghosh On 30-03-2016
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
                        // Added By Avishek Ghosh On 18-03-2016
                        dr["MFDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[7].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[7].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[7].Rows[i]["WEIGHT"]);

                        dttotalrejection.Rows.Add(dr);
                        dttotalrejection.AcceptChanges();
                    }
                    Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                }
                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["RECEIVEDTAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                }
                if (dtProductTax.Rows.Count > 0)
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
                else
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());

                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
            }
            #endregion

            #region grdAddDespatch DataBind
            if (dtDespatchEdit.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatchEdit;
                this.grdAddDespatch.DataBind();
                if (ViewState["count"] == null)
                {
                    ViewState["count"] = "Y";
                }
                if (this.grdAddDespatch.Rows.Count > 0)
                {
                    //this.GridStyle();
                    GridCalculation();
                }
            }
            #endregion

            #endregion

            #region Addn. Details
            if (ds.Tables[8].Rows.Count > 0)
            {
                DataTable dtAdd_DetailsEdit = (DataTable)Session["ADDN_DETAILS"];
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    DataRow dr = dtAdd_DetailsEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["TAXID"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[8].Rows[i]["PERCENTAGE"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXNAME"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[8].Rows[i]["AMOUNT"]);
                    dr["LEDGERID"] = Convert.ToString(ds.Tables[8].Rows[i]["LEDGERID"]);
                    dtAdd_DetailsEdit.Rows.Add(dr);
                    dtAdd_DetailsEdit.AcceptChanges();
                    ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) + double.Parse(ds.Tables[8].Rows[i]["AMOUNT"].ToString().Trim());
                }

                //txtTotalGross.Text = additionalgross;
                //this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
                txtaddnamt.Text = ViewState["SumTotal"].ToString();
                HttpContext.Current.Session["ADDN_DETAILS"] = dtAdd_DetailsEdit;
                gvadd.DataSource = dtAdd_DetailsEdit;
                gvadd.DataBind();
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

    #region Delete Despatch
    protected void DeleteRecordReceived(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                e.Record["Error"] = "Delete not allowed..!";
            }
            else
            {
                int flag = 0;
                string DispatchID = Convert.ToString(e.Record["STOCKRECEIVEDID"]).Trim();
                if (clsReceivedStock.GetApproveStatus(DispatchID) == "1")
                {
                    e.Record["Error"] = "Finance Posting already done,not allow to delete.";
                    return;
                }
                flag = clsReceivedStock.DeleteStockReceived(e.Record["STOCKRECEIVEDID"].ToString());
                this.hdnReceivedID.Value = "";

                if (flag == 1)
                {
                    this.LoadReceived();
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

    #region delete Rejection
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DataTable dtinnerrejectiongrid = (DataTable)HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"];

            int flag = 0;
            string guid = hdn_guid.Value.ToString();
            flag = clsReceivedStock.RejectionRecordsDelete(guid, dtinnerrejectiongrid);

            if (flag > 0)
            {
                gvrejectiondetails.DataSource = dtinnerrejectiongrid;
                gvrejectiondetails.DataBind();

                HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dtinnerrejectiongrid;


                MessageBox1.ShowSuccess("Record deleted successfully!");

                this.popup.Show();
            }
            else
            {
                MessageBox1.ShowError("Record deleted unsuccessful!");
                this.popup.Show();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnRejectionReason
    public void btnRejectionReason(object sender, EventArgs e)
    {
        try
        {
            if (Session["TOTALREJECTIONDETAILS"] == null)
            {
                this.CreateRejectionTotalDataTable();
            }
            this.CreateRejectionInnerGridDataTable();

            dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            ImageButton btn_Rejection = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_Rejection.NamingContainer;
            string guid = gvr.Cells[2].Text.Trim();
            string mrp = gvr.Cells[10].Text.Trim();

            if (dtDespatchEdit.Rows.Count > 0)
            {
                for (int i = 0; i < dtDespatchEdit.Rows.Count; i++)
                {
                    if (dtDespatchEdit.Rows[i]["GUID"].ToString().Trim() == guid)
                    {
                        int NumberofRecord = dtDespatchEdit.Select("GUID='" + guid + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            this.hdn_guid.Value = dtDespatchEdit.Rows[i]["GUID"].ToString();
                            this.hdnpoid.Value = dtDespatchEdit.Rows[i]["POID"].ToString();
                            this.hdnproductid.Value = dtDespatchEdit.Rows[i]["PRODUCTID"].ToString();
                            this.hdnMFDATE.Value = dtDespatchEdit.Rows[i]["MFDATE"].ToString();
                            this.hdnEXPRDATE.Value = dtDespatchEdit.Rows[i]["EXPRDATE"].ToString();
                            this.txtproductnameonrejection.Text = dtDespatchEdit.Rows[i]["PRODUCTNAME"].ToString();
                            this.txtproductbatchnoonrejection.Text = dtDespatchEdit.Rows[i]["BATCHNO"].ToString();
                            //this.ddlrejectionpacksize.SelectedValue = dtDespatchEdit.Rows[i]["PACKINGSIZEID"].ToString();
                            this.hdnMRP.Value = mrp.Trim();
                            break;
                        }
                    }
                }
            }

            this.ddlrejectionpacksize.Items.Clear();
            this.ddlrejectionpacksize.Items.Add(new ListItem("-- SELECT PACKING SIZE --", "0"));
            this.ddlrejectionpacksize.AppendDataBoundItems = true;
            this.ddlrejectionpacksize.DataSource = clsReceivedStock.BindPackingSize(this.hdnproductid.Value.ToString());
            this.ddlrejectionpacksize.DataValueField = "PACKSIZEID_FROM";
            this.ddlrejectionpacksize.DataTextField = "PACKSIZEName_FROM";
            this.ddlrejectionpacksize.DataBind();

            this.txtrejectionqty.Text = "";
            this.ddlrejectionreason.SelectedValue = "0";
            this.gvrejectiondetails.ClearPreviousDataSource();
            this.gvrejectiondetails.DataSource = null;
            this.gvrejectiondetails.DataBind();

            DataTable dtinnergridrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
            if (dtrejectiontotal.Rows.Count > 0)
            {
                for (int i = 0; i < dtrejectiontotal.Rows.Count; i++)
                {
                    if (dtrejectiontotal.Rows[i]["POID"].ToString().Trim() == hdnpoid.Value.ToString().Trim() && dtrejectiontotal.Rows[i]["PRODUCTID"].ToString().Trim() == hdnproductid.Value.ToString().Trim() && dtrejectiontotal.Rows[i]["BATCHNO"].ToString().Trim() == txtproductbatchnoonrejection.Text.Trim())
                    {
                        DataRow dr = dtinnergridrejection.NewRow();
                        dr["GUID"] = dtrejectiontotal.Rows[i]["GUID"].ToString();
                        dr["STOCKRECEIVEDID"] = dtrejectiontotal.Rows[i]["STOCKRECEIVEDID"].ToString();
                        dr["POID"] = dtrejectiontotal.Rows[i]["POID"].ToString();
                        dr["STOCKDESPATCHID"] = dtrejectiontotal.Rows[i]["STOCKDESPATCHID"].ToString();
                        dr["PRODUCTID"] = dtrejectiontotal.Rows[i]["PRODUCTID"].ToString();
                        dr["PRODUCTNAME"] = dtrejectiontotal.Rows[i]["PRODUCTNAME"].ToString();
                        dr["BATCHNO"] = dtrejectiontotal.Rows[i]["BATCHNO"].ToString();
                        dr["REJECTIONQTY"] = dtrejectiontotal.Rows[i]["REJECTIONQTY"].ToString();
                        dr["PACKINGSIZEID"] = dtrejectiontotal.Rows[i]["PACKINGSIZEID"].ToString();
                        dr["PACKINGSIZENAME"] = dtrejectiontotal.Rows[i]["PACKINGSIZENAME"].ToString();
                        dr["REASONID"] = dtrejectiontotal.Rows[i]["REASONID"].ToString();
                        dr["REASONNAME"] = dtrejectiontotal.Rows[i]["REASONNAME"].ToString();
                        dr["DEPOTRATE"] = dtrejectiontotal.Rows[i]["DEPOTRATE"].ToString();
                        dr["AMOUNT"] = dtrejectiontotal.Rows[i]["AMOUNT"].ToString();
                        dr["DEPOTRATE1"] = dtrejectiontotal.Rows[i]["DEPOTRATE1"].ToString();
                        dr["STORELOCATIONID"] = dtrejectiontotal.Rows[i]["STORELOCATIONID"].ToString();
                        dr["STORELOCATIONNAME"] = dtrejectiontotal.Rows[i]["STORELOCATIONNAME"].ToString();
                        dr["MFDATE"] = dtrejectiontotal.Rows[i]["MFDATE"].ToString();
                        dr["EXPRDATE"] = dtrejectiontotal.Rows[i]["EXPRDATE"].ToString();
                        dr["ASSESMENTPERCENTAGE"] = dtrejectiontotal.Rows[i]["ASSESMENTPERCENTAGE"].ToString();
                        dr["MRP"] = dtrejectiontotal.Rows[i]["MRP"].ToString();
                        dr["WEIGHT"] = dtrejectiontotal.Rows[i]["WEIGHT"].ToString();


                        dtinnergridrejection.Rows.Add(dr);
                        dtinnergridrejection.AcceptChanges();
                    }
                }

                Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;
                this.gvrejectiondetails.DataSource = dtinnergridrejection;
                this.gvrejectiondetails.DataBind();
            }
            this.popup.Show();
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

    #region btnrejectionadd_click
    public void btnrejectionadd_click(object sender, EventArgs e)
    {
        try
        {
            string mfDate = string.Empty;
            string exprDate = string.Empty;
            string assesment = string.Empty;
            string mrp = string.Empty;
            string weight = string.Empty;

            if (Session["REJECTIONDINNERGRIDDETAILS"] == null)
            {
                this.CreateRejectionInnerGridDataTable();
            }
            int flag = 0;
            DataTable dtinnergridrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];

            if (dtinnergridrejection.Rows.Count > 0)
            {
                for (int i = 0; i < dtinnergridrejection.Rows.Count; i++)
                {
                    int NumberofRecord = dtinnergridrejection.Select("POID='" + hdnpoid.Value.ToString().Trim() + "' AND PRODUCTID='" + hdnproductid.Value.ToString().Trim() + "' AND BATCHNO='" + txtproductbatchnoonrejection.Text.Trim() + "' AND REASONID='" + ddlrejectionreason.SelectedValue.Trim() + "' AND REASONNAME='" + ddlrejectionreason.SelectedItem.Text.ToString().Trim() + "'").Length;
                    if (NumberofRecord > 0)
                    {
                        MessageBox1.ShowInfo("<b>This reason already exist with this product!</b>", 60, 400);
                        flag = 1;
                        break;
                    }
                }
            }

            if (flag == 0)
            {
                //ClsDepoReceived clsdeporeceived = new ClsDepoReceived();
                ClsStockAdjustment_FAC clsAdj = new ClsStockAdjustment_FAC();
                ClsStockTransfer clsTrn = new ClsStockTransfer();
                ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
                ClsGRNMM clsgrnmm = new ClsGRNMM();
                decimal DepotRate = 0;
                //Added By Avishek Ghosh On 30-03-2016
                decimal DepotRate1 = 0;

                DataTable dtStoreLocation = new DataTable();
                DataTable dtProductdetails = new DataTable();
                dtStoreLocation = clsReceivedStock.StoreLocationDetails(this.ddlrejectionreason.SelectedValue.Trim());

                // Added By Avishek Ghosh On 18-03-2016
                dtProductdetails = clsAdj.BindPRoductDetails(this.hdnproductid.Value.Trim(), this.txtproductbatchnoonrejection.Text.Trim(), Convert.ToDecimal(this.hdnMRP.Value.Trim()));
                if (dtProductdetails.Rows.Count > 0)
                {
                    mfDate = Convert.ToString(this.hdnMFDATE.Value).Trim();
                    exprDate = Convert.ToString(this.hdnEXPRDATE.Value).Trim();
                    assesment = Convert.ToString(dtProductdetails.Rows[0]["ASSESSABLEPERCENT"]).Trim();
                    mrp = this.hdnMRP.Value.Trim();
                    weight = Convert.ToString(dtProductdetails.Rows[0]["WEIGHT"]).Trim();
                }
                else
                {
                    mfDate = "";
                    exprDate = "";
                    assesment = "0";
                    mrp = "0";
                    weight = "";
                }

                string DebitedTo = clsReceivedStock.DebitedTo(this.ddlrejectionreason.SelectedValue);
                if (DebitedTo.Trim() == "0")
                {
                    DepotRate = 0;
                }
                else if (DebitedTo.Trim() == "1")
                {
                    DepotRate = clsgrnmm.BindDepotRate(hdnproductid.Value.Trim(), Convert.ToDecimal(mrp), this.txtInvoiceDate.Text.Trim());
                }
                else
                {
                    DepotRate = clsgrnmm.BindTPURate(hdnproductid.Value.Trim(), this.ddlTPU.SelectedValue.Trim(), this.txtDespatchDate.Text.Trim());
                }

                DepotRate1 = clsgrnmm.BindDepotRate(hdnproductid.Value.Trim(), Convert.ToDecimal(mrp), this.txtInvoiceDate.Text.Trim());

                DataRow dr = dtinnergridrejection.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["STOCKRECEIVEDID"] = this.hdnReceivedID.Value.ToString().Trim();
                dr["POID"] = hdnpoid.Value.ToString().Trim();
                dr["STOCKDESPATCHID"] = this.ddldespatchno.SelectedValue.ToString().Trim();
                dr["PRODUCTID"] = this.hdnproductid.Value.ToString().Trim();
                dr["PRODUCTNAME"] = this.txtproductnameonrejection.Text.Trim().Trim();
                dr["BATCHNO"] = this.txtproductbatchnoonrejection.Text.Trim();
                dr["REJECTIONQTY"] = Convert.ToString(this.txtrejectionqty.Text.Trim());
                dr["PACKINGSIZEID"] = this.ddlrejectionpacksize.SelectedValue.ToString().Trim();
                dr["PACKINGSIZENAME"] = this.ddlrejectionpacksize.SelectedItem.Text.Trim();
                dr["REASONID"] = this.ddlrejectionreason.SelectedValue.ToString().Trim();
                dr["REASONNAME"] = this.ddlrejectionreason.SelectedItem.Text.Trim();
                dr["DEPOTRATE"] = Convert.ToString(DepotRate);

                //Added By Avishek Ghosh On 30-03-2016
                dr["DEPOTRATE1"] = Convert.ToString(DepotRate1);
                decimal convertionqty = 0;

                if (this.ddlrejectionpacksize.SelectedValue.ToString().Trim() == "B9F29D12-DE94-40F1-A668-C79BF1BF4425")
                {
                    convertionqty = clsgrnmm.ConvertionRejectionQty(this.hdnproductid.Value.ToString(), this.ddlrejectionpacksize.SelectedValue.ToString(),
                                                                    Convert.ToDecimal(this.txtrejectionqty.Text.Trim()));
                }
                else
                {
                    convertionqty = clsgrnmm.CalculateNonFGRejectionQty(this.hdnproductid.Value.ToString().Trim(), Convert.ToDecimal(this.txtrejectionqty.Text.Trim()),
                                                                        this.ddlrejectionpacksize.SelectedValue.ToString().Trim());
                }
                /*convertionqty = clsdeporeceived.ConvertionQty(hdnproductid.Value.ToString().Trim(), this.ddlrejectionpacksize.SelectedValue.ToString().Trim(), Convert.ToDecimal(this.txtrejectionqty.Text.Trim()));*/
                dr["AMOUNT"] = Convert.ToString(DepotRate * convertionqty);
                if (dtStoreLocation.Rows.Count > 0)
                {
                    dr["STORELOCATIONID"] = Convert.ToString(dtStoreLocation.Rows[0]["ID"]);
                    dr["STORELOCATIONNAME"] = Convert.ToString(dtStoreLocation.Rows[0]["NAME"]);
                }
                else
                {
                    dr["STORELOCATIONID"] = "";
                    dr["STORELOCATIONNAME"] = "";
                }

                //Added By Avishek On 18-03-2016
                dr["MFDATE"] = mfDate;
                dr["EXPRDATE"] = exprDate;
                dr["ASSESMENTPERCENTAGE"] = assesment;
                dr["MRP"] = mrp;
                dr["WEIGHT"] = weight;

                dtinnergridrejection.Rows.Add(dr);
                dtinnergridrejection.AcceptChanges();

                Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;
                this.gvrejectiondetails.DataSource = dtinnergridrejection;
                this.gvrejectiondetails.DataBind();
                this.txtrejectionqty.Text = "";
                this.ddlrejectionreason.SelectedValue = "0";
            }
            this.popup.Show();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnrejectionsubmit_click
    public void btnrejectionsubmit_click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TOTALREJECTIONDETAILS"] == null)
            {
                this.CreateRejectionTotalDataTable();
            }

            DataTable dtinnerrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            int j = 0;

            //if (dtinnerrejection.Rows.Count > 0)
            //{
            DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
            if (dtrejectiontotal.Rows.Count > 0)
            {
                for (int i = 0; i < dtinnerrejection.Rows.Count; i++)
                {
                    while (j > +2 && j < dtrejectiontotal.Rows.Count)
                    {
                        int NumberofRecord = dtrejectiontotal.Select("STOCKDESPATCHID='" + Convert.ToString(dtinnerrejection.Rows[i]["STOCKDESPATCHID"]).Trim() + "' AND POID='" + Convert.ToString(dtinnerrejection.Rows[i]["POID"]).Trim() + "' AND PRODUCTID='" + Convert.ToString(dtinnerrejection.Rows[i]["PRODUCTID"]).Trim() + "' AND BATCHNO='" + Convert.ToString(dtinnerrejection.Rows[i]["BATCHNO"]).Trim() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            string PID = dtinnerrejection.Rows[i]["PRODUCTID"].ToString();
                            string BATCHNO = dtinnerrejection.Rows[i]["BATCHNO"].ToString();
                            string POID = dtinnerrejection.Rows[i]["POID"].ToString();

                            for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                            {
                                if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == POID && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == PID && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == BATCHNO)
                                {
                                    dtrejectiontotal.Rows[k].Delete();
                                    dtrejectiontotal.AcceptChanges();
                                }
                            }
                        }
                        j++;
                    }
                }
            }

            for (int i = 0; i < dtinnerrejection.Rows.Count; i++)
            {
                DataRow dr = dtrejectiontotal.NewRow();
                dr["GUID"] = dtinnerrejection.Rows[i]["GUID"].ToString().Trim();
                dr["STOCKRECEIVEDID"] = dtinnerrejection.Rows[i]["STOCKRECEIVEDID"].ToString().Trim();
                dr["POID"] = dtinnerrejection.Rows[i]["POID"].ToString().Trim();
                dr["STOCKDESPATCHID"] = dtinnerrejection.Rows[i]["STOCKDESPATCHID"].ToString().Trim();
                dr["PRODUCTID"] = dtinnerrejection.Rows[i]["PRODUCTID"].ToString().Trim();
                dr["PRODUCTNAME"] = dtinnerrejection.Rows[i]["PRODUCTNAME"].ToString().Trim();
                dr["BATCHNO"] = dtinnerrejection.Rows[i]["BATCHNO"].ToString().Trim();
                dr["REJECTIONQTY"] = dtinnerrejection.Rows[i]["REJECTIONQTY"].ToString().Trim();
                dr["PACKINGSIZEID"] = dtinnerrejection.Rows[i]["PACKINGSIZEID"].ToString().Trim();
                dr["PACKINGSIZENAME"] = dtinnerrejection.Rows[i]["PACKINGSIZENAME"].ToString().Trim();
                dr["REASONID"] = dtinnerrejection.Rows[i]["REASONID"].ToString().Trim();
                dr["REASONNAME"] = dtinnerrejection.Rows[i]["REASONNAME"].ToString().Trim();
                dr["DEPOTRATE"] = dtinnerrejection.Rows[i]["DEPOTRATE"].ToString().Trim();
                //Added By Avishek Ghosh On 30-03-2016
                dr["DEPOTRATE1"] = dtinnerrejection.Rows[i]["DEPOTRATE1"].ToString().Trim();

                dr["AMOUNT"] = dtinnerrejection.Rows[i]["AMOUNT"].ToString().Trim();
                dr["STORELOCATIONID"] = dtinnerrejection.Rows[i]["STORELOCATIONID"].ToString().Trim();
                dr["STORELOCATIONNAME"] = dtinnerrejection.Rows[i]["STORELOCATIONNAME"].ToString().Trim();

                //Added By Avishek Ghosh On 18-03-2016
                dr["MFDATE"] = dtinnerrejection.Rows[i]["MFDATE"].ToString().Trim();
                dr["EXPRDATE"] = dtinnerrejection.Rows[i]["EXPRDATE"].ToString().Trim();
                dr["ASSESMENTPERCENTAGE"] = dtinnerrejection.Rows[i]["ASSESMENTPERCENTAGE"].ToString().Trim();
                dr["MRP"] = dtinnerrejection.Rows[i]["MRP"].ToString().Trim();
                dr["WEIGHT"] = dtinnerrejection.Rows[i]["WEIGHT"].ToString().Trim();


                dtrejectiontotal.Rows.Add(dr);
                dtrejectiontotal.AcceptChanges();
            }

            if (dtinnerrejection.Rows.Count == 0)
            {
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    while (j > +2 && j < dtrejectiontotal.Rows.Count)
                    {
                        int NumberofRecord = dtrejectiontotal.Select("POID='" + hdnpoid.Value.ToString().Trim() + "' AND PRODUCTID='" + hdnproductid.Value.ToString().Trim() + "' AND BATCHNO='" + txtproductbatchnoonrejection.Text.Trim() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                            {
                                if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == hdnpoid.Value.ToString().Trim() && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == hdnproductid.Value.ToString().Trim() && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == txtproductbatchnoonrejection.Text.Trim())
                                {
                                    dtrejectiontotal.Rows[k].Delete();
                                    dtrejectiontotal.AcceptChanges();
                                }
                            }
                        }
                        j++;
                    }
                }
            }

            Session["TOTALREJECTIONDETAILS"] = dtrejectiontotal;
            Session["REJECTIONDINNERGRIDDETAILS"] = null;
            dtinnerrejection.Clear();
            this.txtrejectionqty.Text = "";
            this.ddlrejectionreason.SelectedValue = "0";
            this.gvrejectiondetails.ClearPreviousDataSource();
            this.gvrejectiondetails.DataSource = null;
            this.gvrejectiondetails.DataBind();
            MessageBox1.ShowInfo("Rejection for product <b><font color='green'>" + this.txtproductnameonrejection.Text.Trim() + "</font></b> added successfully!", 80, 750);

            #region Bind Product Grid
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
            if (ViewState["count"] == null)
            {
                ViewState["count"] = "Y";
            }
            this.grdAddDespatch.DataSource = dtDespatchEdit;
            this.grdAddDespatch.DataBind();
            if (this.grdAddDespatch.Rows.Count > 0)
            {
                this.GridStyle();
                GridCalculation();
            }
            #endregion

            //this.BindDespatchGrid();
            //}
            //else
            //{
            //    MessageBox1.ShowInfo("Please add atleast 1 rejection reason!");
            //    this.popup.Show();
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region grdReceivedHeader_RowDataBound
    protected void grdReceivedHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[17] as GridDataControlFieldCell;
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
                    cell.ForeColor = Color.Black;
                }
                else
                {
                    cell.ForeColor = Color.Green;
                }

                //if (totalParameters == 2)
                //{
                //    this.grdReceivedHeader.Columns[17].Visible = false;
                //}
                //else if (totalParameters == 1)
                //{
                //    this.grdReceivedHeader.Columns[17].Visible = true;
                //}
            }
            //else
            //{
            //    totalParameters = Request.QueryString.AllKeys.Length;

            //    if (totalParameters == 2)
            //    {
            //        this.grdReceivedHeader.Columns[17].Visible = false;
            //    }
            //    else if (totalParameters == 1)
            //    {
            //        this.grdReceivedHeader.Columns[17].Visible = true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

    #region gvProductTax_OnRowDataBound
    protected void gvProductTax_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalTaxValue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TAXVALUE"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total : ";
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[4].Font.Bold = true;

            e.Row.Cells[5].Text = TotalTaxValue.ToString("#.00");
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[5].ForeColor = Color.Blue;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    #endregion

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedID = Convert.ToString(hdnReceivedID.Value);
            // string upath = "frmRptPurchasePrint.aspx?pid=" + hdnReceivedID.Value + "";
            //string upath = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

            string path = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "&&MenuId=1636";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region txtothchrg_TextChanged
    protected void txtothchrg_TextChanged(object sender, EventArgs e)
    {
        decimal OthChrg = Convert.ToDecimal(txtothchrg.Text == "" ? "0.00" : txtothchrg.Text) + Convert.ToDecimal(txtFinalAmt.Text);
        txtFinalAmt.Text = Convert.ToString(OthChrg);
    }
    #endregion

    #region Show Document From QC Control
    protected void btnDocuments_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtQC = new DataTable();
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            dtQC = clsgrnmm.FetchQCID(hdnReceivedID.Value.Trim());
            string QCNO = Convert.ToString(dtQC.Rows[0]["QCID"].ToString());
            string strPopup = string.Empty;
            if (QCNO.Trim() != "")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmQCFileUpload_FAC.aspx?QCID=" + QCNO + "&MODE=GRN"
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

    #region GridCalculation
    protected void GridCalculation()
    {
        DataTable dtPURCHASERCVDDETAILS = new DataTable();
        decimal TotalGridAmount = 0;
        decimal TotalNetAmt = 0;
        decimal AfterDiscAmt = 0, AfterFreightAmt = 0, AfterAddCostAmt = 0, DISC=0, FREIGHT=0, ADDCOST=0,qty=0;
        if (Session["DESPATCHDETAILS"] != null)
        {
            dtPURCHASERCVDDETAILS = (DataTable)Session["DESPATCHDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtPURCHASERCVDDETAILS);

        #region Header

        this.grdAddDespatch.HeaderRow.Cells[9].Text = "UOM";
        this.grdAddDespatch.HeaderRow.Cells[19].Text = "DISC(%)";
        this.grdAddDespatch.HeaderRow.Cells[20].Text = "DISC(₹)";
        this.grdAddDespatch.HeaderRow.Cells[21].Text = "AFTERDISC(₹)";
        this.grdAddDespatch.HeaderRow.Cells[22].Text = "FREIGHT(₹)";
        this.grdAddDespatch.HeaderRow.Cells[23].Text = "AFTERFREIGHT(₹)";
        this.grdAddDespatch.HeaderRow.Cells[24].Text = "ADDCOST(₹)";
        this.grdAddDespatch.HeaderRow.Cells[25].Text = "TAXABLE(₹)";

        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[4].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[5].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[8].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[11].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[13].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[14].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[17].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[26].Visible = false;
       
        this.grdAddDespatch.HeaderRow.Cells[28].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[29].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[30].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[31].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[32].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[33].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[34].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[21].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[23].Visible = false;

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
        this.grdAddDespatch.HeaderRow.Cells[28].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[29].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[30].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[31].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[32].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[33].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[34].Wrap = false;

        #endregion

        #region Footer Style

        this.grdAddDespatch.FooterRow.Cells[2].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[3].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[4].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[5].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[8].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[11].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[13].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[14].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[15].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[17].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[26].Visible = false;
        
        this.grdAddDespatch.FooterRow.Cells[28].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[29].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[30].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[31].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[32].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[33].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[34].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[21].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[23].Visible = false;

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
        this.grdAddDespatch.FooterRow.Cells[28].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[29].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[30].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[31].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[32].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[33].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[34].Wrap = false;
      

        this.grdAddDespatch.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        this.grdAddDespatch.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        this.grdAddDespatch.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        this.grdAddDespatch.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
        this.grdAddDespatch.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;

        #endregion

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[5].Visible = false;
            row.Cells[8].Visible = false;
            row.Cells[11].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[26].Visible = false;
            
            row.Cells[28].Visible = false;
            row.Cells[29].Visible = false;
            row.Cells[30].Visible = false;
            row.Cells[31].Visible = false;
            row.Cells[32].Visible = false;
            row.Cells[33].Visible = false;
            row.Cells[34].Visible = false;
            row.Cells[21].Visible = false;
            row.Cells[23].Visible = false;

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
            row.Cells[28].Wrap = false;
            row.Cells[29].Wrap = false;
            row.Cells[30].Wrap = false;
            row.Cells[31].Wrap = false;
            row.Cells[32].Wrap = false;
            row.Cells[33].Wrap = false;
            row.Cells[34].Wrap = false;

            row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;

            TotalGridAmount += Convert.ToDecimal(row.Cells[18].Text.Trim());
            AfterDiscAmt += Convert.ToDecimal(row.Cells[21].Text.Trim());
            AfterFreightAmt += Convert.ToDecimal(row.Cells[23].Text.Trim());
            AfterAddCostAmt += Convert.ToDecimal(row.Cells[25].Text.Trim());
            qty += Convert.ToDecimal(row.Cells[12].Text.Trim());
            DISC += Convert.ToDecimal(row.Cells[20].Text.Trim());
            FREIGHT += Convert.ToDecimal(row.Cells[22].Text.Trim());
            ADDCOST += Convert.ToDecimal(row.Cells[24].Text.Trim());

            int count = 34;
            DataTable dt = (DataTable)Session["dtReceivedTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grdAddDespatch.HeaderRow.Cells[count].Wrap = false;
            }

            #region Rejection Block

            if (Session["TOTALREJECTIONDETAILS"] != null)
            {
                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    int NumberofRecord = 0;

                    NumberofRecord = dtrejectiontotal.Select("PRODUCTNAME='" + row.Cells[6].Text.Trim() + "' AND BATCHNO='" + row.Cells[23].Text.Trim() + "'").Length;
                    Label lblrejectionqty = (Label)row.FindControl("lblrejvalue");
                    if (NumberofRecord > 0)
                    {
                        lblrejectionqty.Text = Convert.ToString(NumberofRecord);
                        lblrejectionqty.CssClass = "badge green";
                    }
                    else
                    {
                        lblrejectionqty.Text = "0";
                        lblrejectionqty.CssClass = "badge red";
                    }
                }
            }
            #endregion
        }

        #region TotalTax Footer
        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtReceivedTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 36; i <= (36 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    
                    sum += grdAddDespatch.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdAddDespatch.Rows[j].Cells[i].Text) : 0.00;
                    grdAddDespatch.Rows[j].Cells[i].Wrap = false;
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
        #endregion

        #region Add Total Footer

        if (TotalGridAmount == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[18].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[18].Text = TotalGridAmount.ToString("#.00");
            this.grdAddDespatch.FooterRow.Cells[20].Text = DISC.ToString("#.00");
            this.grdAddDespatch.FooterRow.Cells[22].Text = FREIGHT.ToString("#.00");
            this.grdAddDespatch.FooterRow.Cells[24].Text = ADDCOST.ToString("#.00");
            this.grdAddDespatch.FooterRow.Cells[12].Text = qty.ToString("#.00");
        }

        if (AfterDiscAmt == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[21].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[21].Text = AfterDiscAmt.ToString("#.00");
        }

        if (AfterFreightAmt == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[23].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[23].Text = AfterFreightAmt.ToString("#.00");
        }

        if (AfterAddCostAmt == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[25].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[25].Text = AfterAddCostAmt.ToString("#.00");
        }

        this.grdAddDespatch.FooterRow.Cells[18].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[18].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[18].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[21].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[21].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[21].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[23].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[23].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[23].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[25].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[25].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[25].Wrap = false;

        this.grdAddDespatch.FooterRow.Cells[17].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[17].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[17].ForeColor = Color.Blue;

        #endregion

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[37 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[37 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[37 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[37 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[37 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[37 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[37 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[37 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[37+ count1].Wrap = false;
        }

        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[36 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[36 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[36 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[36 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[36 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[36 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[36 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[36 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[36 + count1].Wrap = false;
        }

        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[35 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[35 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[35 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[35 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[35 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[35 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[35 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[35 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[35 + count1].Wrap = false;
        }
        #endregion
    }
    #endregion    

    #region CreateDataTable_TAX
    private void CreateDataTable_TAX()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("LEDGERID", typeof(string)));
        HttpContext.Current.Session["ADDN_DETAILS"] = dt;

    }
    #endregion

    #region gvadd_OnRowDataBound
    protected void gvadd_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total (Rs.):";
                e.Row.Cells[2].ForeColor = Color.Blue;
                e.Row.Cells[4].Text = ViewState["SumTotal"].ToString();
                e.Row.Cells[4].ForeColor = Color.Blue;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region GridCalculation For Tpu Despatch Receipt
    protected void GridCalculation_TpuDespatchReceipt()
    {
        DataTable dtPURCHASERCVDDETAILS = new DataTable();
        decimal TotalGridAmount = 0;
        decimal TotalNetAmt = 0;
        decimal AfterDiscAmt = 0, AfterFreightAmt = 0, AfterAddCostAmt = 0;
        if (Session["DESPATCHDETAILS"] != null)
        {
            dtPURCHASERCVDDETAILS = (DataTable)Session["DESPATCHDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtPURCHASERCVDDETAILS);

        #region Header

        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[4].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[5].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[8].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[13].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[14].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[17].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[19].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[20].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[21].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[22].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[23].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[24].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[25].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[26].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[28].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[29].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[30].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[32].Visible = false;


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
        this.grdAddDespatch.HeaderRow.Cells[28].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[29].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[30].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[31].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[32].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[33].Wrap = false;
        this.grdAddDespatch.HeaderRow.Cells[34].Wrap = false;

        #endregion

        #region Footer Style
        this.grdAddDespatch.FooterRow.Cells[2].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[3].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[4].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[5].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[8].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[13].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[14].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[15].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[17].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[19].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[20].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[21].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[22].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[23].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[24].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[25].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[26].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[28].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[29].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[30].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[32].Visible = false;


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
        this.grdAddDespatch.FooterRow.Cells[28].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[29].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[30].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[31].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[32].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[33].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[34].Wrap = false;

        //this.grdAddDespatch.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //this.grdAddDespatch.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        //this.grdAddDespatch.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        //this.grdAddDespatch.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        //this.grdAddDespatch.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        //this.grdAddDespatch.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        //this.grdAddDespatch.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
        //this.grdAddDespatch.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;

        #endregion

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[5].Visible = false;
            row.Cells[8].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[19].Visible = false;
            row.Cells[20].Visible = false;
            row.Cells[21].Visible = false;
            row.Cells[22].Visible = false;
            row.Cells[23].Visible = false;
            row.Cells[24].Visible = false;
            row.Cells[25].Visible = false;
            row.Cells[26].Visible = false;
            row.Cells[28].Visible = false;
            row.Cells[29].Visible = false;
            row.Cells[30].Visible = false;
            row.Cells[32].Visible = false;


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
            row.Cells[28].Wrap = false;
            row.Cells[29].Wrap = false;
            row.Cells[30].Wrap = false;
            row.Cells[31].Wrap = false;
            row.Cells[32].Wrap = false;
            row.Cells[33].Wrap = false;
            row.Cells[34].Wrap = false;

            row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[18].HorizontalAlign = HorizontalAlign.Right;

            TotalGridAmount += Convert.ToDecimal(row.Cells[18].Text.Trim());
            //AfterDiscAmt += Convert.ToDecimal(row.Cells[21].Text.Trim());
            //AfterFreightAmt += Convert.ToDecimal(row.Cells[23].Text.Trim());
            //AfterAddCostAmt += Convert.ToDecimal(row.Cells[25].Text.Trim());

            int count = 34;
            DataTable dt = (DataTable)Session["dtReceivedTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grdAddDespatch.HeaderRow.Cells[count].Wrap = false;
            }

            #region Rejection Block

            if (Session["TOTALREJECTIONDETAILS"] != null)
            {
                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    int NumberofRecord = 0;

                    NumberofRecord = dtrejectiontotal.Select("PRODUCTNAME='" + row.Cells[6].Text.Trim() + "' AND BATCHNO='" + row.Cells[23].Text.Trim() + "'").Length;
                    Label lblrejectionqty = (Label)row.FindControl("lblrejvalue");
                    if (NumberofRecord > 0)
                    {
                        lblrejectionqty.Text = Convert.ToString(NumberofRecord);
                        lblrejectionqty.CssClass = "badge green";
                    }
                    else
                    {
                        lblrejectionqty.Text = "0";
                        lblrejectionqty.CssClass = "badge red";
                    }
                }
            }
            #endregion
        }

        #region TotalTax Footer
        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtReceivedTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 36; i <= (36 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    sum += grdAddDespatch.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdAddDespatch.Rows[j].Cells[i].Text) : 0.00;
                    grdAddDespatch.Rows[j].Cells[i].Wrap = false;
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
        #endregion

        #region Add Total Footer

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

        this.grdAddDespatch.FooterRow.Cells[17].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[17].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[17].ForeColor = Color.Blue;

        #endregion

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[37 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[37 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[37 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[37 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[37 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[37 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[37 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[37 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[37 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[36 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[36 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[36 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[36 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[36 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[36 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[36 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[36 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[36 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[35 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[35 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[35 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[35 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[35 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[35 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[35 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[35 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[35 + count1].Wrap = false;
        }
        #endregion
    }
    #endregion 

    #region Exchange Rate Calculation
    protected void txtexchangrate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtexchangrate.Text) > 0)
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            clsDespatchStock ClsDespatchStock = new clsDespatchStock();
            ClsGRNMM ClsGRNMM = new ClsGRNMM();
            DataTable dtDespatchEdit = new DataTable();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            if (Checker == "FALSE")
            {
                if (clsReceivedStock.Getstatus(this.hdnReceivedID.Value.Trim()) == "1")
                {
                    MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
            }

            DataSet ds = new DataSet();
            this.trAutoReceivedNo.Style["display"] = "";
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
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnReceivedID.Value);
            ds = clsReceivedStock.EditReceivedDetails(receivedID);

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["POID"] = Convert.ToString(ds.Tables[6].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTNAME"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["NAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]).Trim();
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]).Trim();
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]).Trim();
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]).Trim();
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]).Trim();
                    dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]).Trim();
                    dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]).Trim();
                    //dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();
                    dr["RATE"] = String.Format("{0:0.000000}", Convert.ToDecimal(ds.Tables[1].Rows[i]["RATE"]) * Convert.ToDecimal(txtexchangrate.Text.Trim()));
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]).Trim();
                    //dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                    dr["AMOUNT"] = String.Format("{0:0.00}", Convert.ToDecimal(dr["QTY"]) * Convert.ToDecimal(dr["RATE"]));

                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESABLEVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);

                    if (Convert.ToDecimal(dr["DISCOUNTPER"]) > 0)
                    {
                        dr["AFTERDISCOUNTAMT"] = String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"]) * Convert.ToDecimal(dr["DISCOUNTPER"]) / 100);
                    }
                    else
                    {
                        dr["AFTERDISCOUNTAMT"] = dr["AMOUNT"];
                    }
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    if (Convert.ToDecimal(dr["DISCOUNTAMT"]) > 0)
                    {
                        dr["AFTERDISCOUNTAMT"] = String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"]) - Convert.ToDecimal(dr["DISCOUNTAMT"]));
                    }
                    else
                    {
                        dr["AFTERDISCOUNTAMT"] = dr["AMOUNT"];
                    }

                    //dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    if (Convert.ToDecimal(dr["ITEMWISEFREIGHT"]) > 0)
                    {
                        dr["AFTERITEMWISEFREIGHTAMT"] = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]) + Convert.ToDecimal(dr["AFTERDISCOUNTAMT"]));
                    }
                    else
                    {
                        dr["AFTERITEMWISEFREIGHTAMT"] = dr["AMOUNT"];//Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);
                    }
                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);

                    if (Convert.ToDecimal(dr["ITEMWISEADDCOST"]) > 0)
                    {
                        dr["AFTERITEMWISEADDCOSTAMT"] = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]) + Convert.ToDecimal(dr["AFTERITEMWISEFREIGHTAMT"]));
                    }
                    else
                    {
                        dr["AFTERITEMWISEADDCOSTAMT"] = dr["AMOUNT"];//Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);
                    }
                    dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();
                    decimal BillTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = ClsDespatchStock.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = ClsGRNMM.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = ClsGRNMM.GetHSNTaxOnEdit(hdnReceivedID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                break;
                        }
                    }
                    #endregion

                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(dr["AFTERITEMWISEADDCOSTAMT"].ToString().Trim())));
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();
                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region Tax On Gross Amount
                //this.LoadTax();
                this.LoadTermsConditions();
                //DataTable dtGrossTax = (DataTable)Session["GrossReceivedTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionTotalDataTable();
                    DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        DataRow dr = dttotalrejection.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKRECEIVEDID"]);
                        dr["POID"] = Convert.ToString(ds.Tables[7].Rows[i]["POID"]);
                        dr["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKDESPATCHID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[7].Rows[i]["BATCHNO"]);
                        dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[7].Rows[i]["REJECTIONQTY"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZENAME"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONNAME"]);
                        dr["DEPOTRATE"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE"]);
                        // Added By Avishek Ghosh On 30-03-2016
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
                        // Added By Avishek Ghosh On 18-03-2016
                        dr["MFDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[7].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[7].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[7].Rows[i]["WEIGHT"]);

                        dttotalrejection.Rows.Add(dr);
                        dttotalrejection.AcceptChanges();
                    }
                    Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                }
                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["RECEIVEDTAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                }
                if (dtProductTax.Rows.Count > 0)
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
                else
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                //this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());
                //this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(GrossTotal)));
                //this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
                this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
            }
            #endregion

            #region grdAddDespatch DataBind
            if (dtDespatchEdit.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatchEdit;
                this.grdAddDespatch.DataBind();
                if (ViewState["count"] == null)
                {
                    ViewState["count"] = "Y";
                }
                if (this.grdAddDespatch.Rows.Count > 0)
                {
                    //this.GridStyle();
                    GridCalculation();
                }
            }
            #endregion

            #endregion

            #region Addn. Details
            if (ds.Tables[8].Rows.Count > 0)
            {
                DataTable dtAdd_DetailsEdit = (DataTable)Session["ADDN_DETAILS"];
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    DataRow dr = dtAdd_DetailsEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["TAXID"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[8].Rows[i]["PERCENTAGE"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXNAME"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[8].Rows[i]["AMOUNT"]);
                    dr["LEDGERID"] = Convert.ToString(ds.Tables[8].Rows[i]["LEDGERID"]);
                    dtAdd_DetailsEdit.Rows.Add(dr);
                    dtAdd_DetailsEdit.AcceptChanges();
                    ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) + double.Parse(ds.Tables[8].Rows[i]["AMOUNT"].ToString().Trim());
                }
                txtaddnamt.Text = ViewState["SumTotal"].ToString();
                HttpContext.Current.Session["ADDN_DETAILS"] = dtAdd_DetailsEdit;
                gvadd.DataSource = dtAdd_DetailsEdit;
                gvadd.DataBind();
            }
            #endregion
        }
        else
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            clsDespatchStock ClsDespatchStock = new clsDespatchStock();
            ClsGRNMM ClsGRNMM = new ClsGRNMM();
            DataTable dtDespatchEdit = new DataTable();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string TAXID = string.Empty;
            txtexchangrate.Text = "0.00";
            decimal ProductWiseTax = 0;

            if (Checker == "FALSE")
            {
                if (clsReceivedStock.Getstatus(this.hdnReceivedID.Value.Trim()) == "1")
                {
                    MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
            }

            DataSet ds = new DataSet();
            this.trAutoReceivedNo.Style["display"] = "";
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
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnReceivedID.Value);
            ds = clsReceivedStock.EditReceivedDetails(receivedID);

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["POID"] = Convert.ToString(ds.Tables[6].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTNAME"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["NAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]).Trim();
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]).Trim();
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]).Trim();
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]).Trim();
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]).Trim();
                    dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]).Trim();
                    dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]).Trim();
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]).Trim();
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESABLEVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);
                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);

                    dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();
                    decimal BillTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {

                            case "1":
                                TAXID = ClsDespatchStock.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = ClsGRNMM.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = ClsGRNMM.GetHSNTaxOnEdit(hdnReceivedID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                break;
                        }
                    }
                    #endregion

                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString().Trim())));
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();

                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region Tax On Gross Amount
                //this.LoadTax();
                this.LoadTermsConditions();
                //DataTable dtGrossTax = (DataTable)Session["GrossReceivedTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionTotalDataTable();
                    DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        DataRow dr = dttotalrejection.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKRECEIVEDID"]);
                        dr["POID"] = Convert.ToString(ds.Tables[7].Rows[i]["POID"]);
                        dr["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKDESPATCHID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[7].Rows[i]["BATCHNO"]);
                        dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[7].Rows[i]["REJECTIONQTY"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZENAME"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONNAME"]);
                        dr["DEPOTRATE"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE"]);
                        // Added By Avishek Ghosh On 30-03-2016
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
                        // Added By Avishek Ghosh On 18-03-2016
                        dr["MFDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[7].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[7].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[7].Rows[i]["WEIGHT"]);

                        dttotalrejection.Rows.Add(dr);
                        dttotalrejection.AcceptChanges();
                    }
                    Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                }
                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["RECEIVEDTAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                }
                if (dtProductTax.Rows.Count > 0)
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
                else
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());

                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
            }
            #endregion

            #region grdAddDespatch DataBind
            if (dtDespatchEdit.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatchEdit;
                this.grdAddDespatch.DataBind();
                if (ViewState["count"] == null)
                {
                    ViewState["count"] = "Y";
                }
                if (this.grdAddDespatch.Rows.Count > 0)
                {
                    //this.GridStyle();
                    GridCalculation();
                }
            }
            #endregion

            #endregion

            #region Addn. Details
            if (ds.Tables[8].Rows.Count > 0)
            {
                DataTable dtAdd_DetailsEdit = (DataTable)Session["ADDN_DETAILS"];
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    DataRow dr = dtAdd_DetailsEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["TAXID"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[8].Rows[i]["PERCENTAGE"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXNAME"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[8].Rows[i]["AMOUNT"]);
                    dr["LEDGERID"] = Convert.ToString(ds.Tables[8].Rows[i]["LEDGERID"]);
                    dtAdd_DetailsEdit.Rows.Add(dr);
                    dtAdd_DetailsEdit.AcceptChanges();
                    ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) + double.Parse(ds.Tables[8].Rows[i]["AMOUNT"].ToString().Trim());
                }

                //txtTotalGross.Text = additionalgross;
                //this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
                txtaddnamt.Text = ViewState["SumTotal"].ToString();
                HttpContext.Current.Session["ADDN_DETAILS"] = dtAdd_DetailsEdit;
                gvadd.DataSource = dtAdd_DetailsEdit;
                gvadd.DataBind();
            }
            #endregion
        }
    }
    #endregion

    public void Btn_View(string _InvoiceID)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            clsDespatchStock ClsDespatchStock = new clsDespatchStock();
            hdnReceivedID.Value = _InvoiceID;
            hdnDepotID.Value = Request.QueryString["BRANCHID"].ToString();
            //string PURCHASERETURNID = Convert.ToString(hdnReceivedID.Value).Trim();
            ClsGRNMM ClsGRNMM = new ClsGRNMM();
            DataTable dtDespatchEdit = new DataTable();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            if (Checker == "FALSE")
            {
                if (clsReceivedStock.Getstatus(this.hdnReceivedID.Value.Trim()) == "1")
                {
                    MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
            }

            DataSet ds = new DataSet();
            this.trAutoReceivedNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.txtRceivedNo.Style["display"] = "";
            this.txtTCSApplicable.Text = "0";
            this.txtTCSPercent.Text = "0";
            this.txtTCSLimit.Text = "0";
            this.txtTCS.Text = "0";
            this.txtTCSNetAmt.Text = "0";

            #region QueryString
            if (clsReceivedStock.GetApproveStatus(hdnReceivedID.Value) == "1")
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
                this.divDocuments.Visible = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnReceivedID.Value);
            ds = clsReceivedStock.EditReceivedDetails(receivedID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddldespatchno.Enabled = false;
                this.txtRceivedNo.Enabled = false;
                this.txtRceivedNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDNO"]);
                this.LoadEditedDespatchNo();
                this.ddldespatchno.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["STOCKDESPATCHID"]);
                this.txtreceiveddate.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDDATE"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESPATCHDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                this.hdnWaybillNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]);
                this.txtWayBill.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]);
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                this.LoadTPU();
                this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                this.LoadMotherDepot();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                this.LoadTransporter();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                this.LoadInsurance();
                this.ddlinsurancename.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]);
                this.txtinsuranceno.Text = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]) == "01/01/1900")
                {
                    this.txtCFormDate.Text = "";
                }
                else
                {
                    this.txtCFormDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMDATE"]);
                }
                this.txtGatepassNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtGatepassDate.Text = "";
                }
                else
                {
                    this.txtGatepassDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);
                this.hdnSaleOrderID.Value = Convert.ToString(ds.Tables[0].Rows[0]["SALEORDERID"]);
                this.hdnSaleOrderNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["SALEORDERNO"]);
                this.txtTotalCase.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALCASE"]);
                this.txtTotalPCS.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOTALPCS"]);
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["POID"] = Convert.ToString(ds.Tables[6].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[6].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[6].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[6].Rows[i]["TAXVALUE"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["PRODUCTNAME"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[6].Rows[i]["NAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[6].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RECEIVEDTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region TCS
            if (Convert.ToDecimal(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]) > 0)
            {

                this.txtTCSApplicable.Enabled = false;
                this.RbApplicable.SelectedValue = "Y";
                this.txtTCSPercent.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCS_PERCENT"]);
                this.txtTCS.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAMOUNT"]);
                this.txtTCSNetAmt.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]);
                this.txtTCSApplicable.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAPPLICABLE_AMOUNT"]);


            }
            else
            {
                this.RbApplicable.SelectedValue = "N";
                this.txtTCSApplicable.Text = "0";
                this.txtTCSPercent.Text = "0";
                this.txtTCSLimit.Text = "0";
                this.txtTCS.Text = "0";
                this.txtTCSNetAmt.Text = "0";

            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtReceivedTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]).Trim();
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]).Trim();
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]).Trim();
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]).Trim();
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]).Trim();
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]).Trim();
                    dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]).Trim();
                    dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]).Trim();
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]).Trim();
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();
                    dr["QCREJECTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QCREJECTQTY"]).Trim();
                    //dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESABLEVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);
                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);

                    dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();
                    decimal BillTaxAmt = 0;

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = ClsDespatchStock.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                ProductWiseTax = ClsGRNMM.GetHSNTaxOnEdit(hdnReceivedID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                break;
                        }
                    }
                    #endregion

                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString().Trim())));
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();

                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region Tax On Gross Amount
                //this.LoadTax();
                this.LoadTermsConditions();
                //DataTable dtGrossTax = (DataTable)Session["GrossReceivedTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionTotalDataTable();
                    DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        DataRow dr = dttotalrejection.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKRECEIVEDID"]);
                        dr["POID"] = Convert.ToString(ds.Tables[7].Rows[i]["POID"]);
                        dr["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[7].Rows[i]["STOCKDESPATCHID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[7].Rows[i]["BATCHNO"]);
                        dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[7].Rows[i]["REJECTIONQTY"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[7].Rows[i]["PACKINGSIZENAME"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["REASONNAME"]);
                        dr["DEPOTRATE"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE"]);
                        // Added By Avishek Ghosh On 30-03-2016
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
                        // Added By Avishek Ghosh On 18-03-2016
                        dr["MFDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[7].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[7].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[7].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[7].Rows[i]["WEIGHT"]);

                        dttotalrejection.Rows.Add(dr);
                        dttotalrejection.AcceptChanges();
                    }
                    Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                }
                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["RECEIVEDTAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["RECEIVEDTAXCOMPONENTDETAILS"];
                }
                if (dtProductTax.Rows.Count > 0)
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
                else
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                }
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
            }
            #endregion

            #region grdAddDespatch DataBind
            if (dtDespatchEdit.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatchEdit;
                this.grdAddDespatch.DataBind();
                if (ViewState["count"] == null)
                {
                    ViewState["count"] = "Y";
                }
                if (this.grdAddDespatch.Rows.Count > 0)
                {
                    //this.GridStyle();
                    GridCalculation();
                }
            }
            #endregion

            #endregion

            #region Addn. Details
            if (ds.Tables[8].Rows.Count > 0)
            {
                DataTable dtAdd_DetailsEdit = (DataTable)Session["ADDN_DETAILS"];
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    DataRow dr = dtAdd_DetailsEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["TAXID"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[8].Rows[i]["PERCENTAGE"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[8].Rows[i]["TAXNAME"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[8].Rows[i]["AMOUNT"]);
                    dr["LEDGERID"] = Convert.ToString(ds.Tables[8].Rows[i]["LEDGERID"]);
                    dtAdd_DetailsEdit.Rows.Add(dr);
                    dtAdd_DetailsEdit.AcceptChanges();
                    ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) + double.Parse(ds.Tables[8].Rows[i]["AMOUNT"].ToString().Trim());
                }
                txtaddnamt.Text = ViewState["SumTotal"].ToString();
                HttpContext.Current.Session["ADDN_DETAILS"] = dtAdd_DetailsEdit;
                gvadd.DataSource = dtAdd_DetailsEdit;
                gvadd.DataBind();
            }
            #endregion
        }
        catch (Exception ex)
        {
            /*string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);*/
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
            CalendarFromDate.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;
            CalendarExtender1.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtreceiveddate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = today1;
                CalendarExtender3.EndDate = today1;
                CalendarExtender1.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtreceiveddate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            /*string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);*/
        }
    }
    #endregion

    protected void txtRoundoff_TextChanged(object sender, EventArgs e)
    {
        decimal grosAmnt = 0;
        decimal roundOf = 0;
        decimal Amnt =0;
        grosAmnt = Convert.ToDecimal(this.txtTotalGross.Text);
        roundOf = Convert.ToDecimal(this.txtRoundoff.Text);
      

        if(roundOf > 0)
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
        decimal tcsPer = Convert.ToDecimal(txtTCSPercent.Text);
        if (tcsPer > 0)
        {
            this.txtTCSApplicable.Text = Convert.ToString(Amnt);
            decimal TcsAmnt = (tcsPer / Amnt) * 100;
            this.txtTCS.Text = Convert.ToString(TcsAmnt);
            this.txtTCSNetAmt.Text = Convert.ToString(Amnt + TcsAmnt);
        }
        

    }
}