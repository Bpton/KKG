#region Developer Info

/*
 Developer Name : Rajeev Kumar
 * Start Date   : 15/02/2020
 * End Date     :
*/

#endregion

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
#endregion

public class GRNdetails
{
    public string POID { get; set; }
    public string PONO { get; set; }
    public string PRODUCT_ID { get; set; }
    public string PRODUCTNAME { get; set; }
    public string POQTY { get; set; }
    public string DESPATCHQTY { get; set; }
    public string REMAININGQTY { get; set; }
    public string MRP { get; set; }
    public string RATE { get; set; }
    public string UNITID { get; set; }
    public string UNITNAME { get; set; }
    public string ISSUEQTY { get; set; }
}
public partial class VIEW_frmGRNMM : System.Web.UI.Page
{
    ClsGRNMM clsgrnmm = new ClsGRNMM();
    ClsCommonFunction ClsCommon = new ClsCommonFunction();
    string menuID = string.Empty;
    string Checker = string.Empty;
    string FG = string.Empty;
    string OP = string.Empty;
    decimal TotalAmount = 0;
    decimal TotalTaxValue = 0;
    DataTable dtDespatchEdit = new DataTable();
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();
    /*Added By Avishek Ghosh On 13.11.2017*/
    ArrayList RejectionArry = new ArrayList();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    decimal TotalGridAmount = 0;
    decimal AfterDiscAmt = 0;
    decimal AfterItemWiseAddCostAmt = 0, AfterItemWiseFreightAmt = 0, TtoalFreight = 0;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TYPE"] != "LGRREPORT")
            {
                #region Back Date Entry Validation
                ClsCommonFunction ClsCommon = new ClsCommonFunction();
                int Flag = ClsCommon.CheckDate(Request.QueryString["MENUID"].ToString().Trim());
                if (Flag > 0) /*backdate entry open by p.basu as per promod ji on 01-02-2021*/
                {
                    this.ImageButton1.Enabled = true;
                    this.CalendarExtenderDespatchDate.Enabled = true;
                }
                else
                {
                    this.ImageButton1.Enabled = true;
                    this.CalendarExtenderDespatchDate.Enabled = true;
                }
                #endregion

                #region QueryString
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                FG = Request.QueryString["FG"].ToString().Trim();
                OP = Request.QueryString["OP"].ToString().Trim();
                ViewState["OP"] = OP;
                #endregion

                #region Add By Rajeev
                if (ViewState["OP"].ToString() == "QC")
                {
                    divbtnapprove.Visible = false;
                    divbtnrejection.Visible = false;
                    btnaddhide.Visible = false;
                    trpodetails.Visible = false;
                    grdDespatchHeader.Columns[23].Visible = false;
                    lblpagename.Text = "GRN STOCK IN";
                    HDNISVERIFIEDCHECKER1.Value = "N";
                    HDNSTOCKIN.Value = "Y";
                    divDocuments.Visible = true;
                    Column_Print.Visible = true;
                    btnPrint.Visible = true;
                    Tdlblledger.Visible = false;
                    Tdddlledger.Visible = false;
                    TdIssueProduct.Visible = false;
                    TdddlissueProduct.Visible = false;
                    TrWayBill.Visible = false;
                    this.DateLock();
                    this.LoadGRN();
                    divGrnSample.Visible = true;
                    tdlblcapacitychk.Visible = true;
                    tdChkCapacity.Visible = true;
                    divshowcapacity.Visible = true;
                }
                else if (ViewState["OP"].ToString() == "MAKER")
                {
                    divbtnapprove.Visible = false;
                    divbtnrejection.Visible = false;
                    btnaddhide.Visible = true;
                    trpodetails.Visible = true;
                    lblpagename.Text = "GRN DETAILS";
                    HDNISVERIFIEDCHECKER1.Value = "N";
                    HDNSTOCKIN.Value = "N";
                    divDocuments.Visible = false;
                    Column_Print.Visible = true;
                    btnPrint.Visible = true;
                    Tdlblledger.Visible = false;
                    Tdddlledger.Visible = false;

                    TdIssueProduct.Visible = true;
                    TdddlissueProduct.Visible = true;

                    TrWayBill.Visible = false;
                    this.DateLock();
                    this.LoadGRN();
                    divGrnSample.Visible = false;
                    tdlblcapacitychk.Visible = false;
                    tdChkCapacity.Visible = false;
                    divshowcapacity.Visible = false;
                }

                else if (ViewState["OP"].ToString() == "Checker1")
                {
                    divbtnapprove.Visible = true;
                    divbtnrejection.Visible = true;
                    btnsubmitdiv.Visible = false;
                    btnaddhide.Visible = false;
                    trpodetails.Visible = false;
                    grdDespatchHeader.Columns[22].Visible = false;
                    lblpagename.Text = "PURCHASE STOCK RECEIPT-CHECKER 1";
                    HDNISVERIFIEDCHECKER1.Value = "Y";
                    divDocuments.Visible = true;
                    Column_Print.Visible = true;
                    btnPrint.Visible = true;
                    Tdlblledger.Visible = true;
                    Tdddlledger.Visible = true;
                    TdIssueProduct.Visible = false;
                    TdddlissueProduct.Visible = false;
                    TrWayBill.Visible = true;
                    this.DateLock();
                    this.LoadGRN();
                    divGrnSample.Visible = true;
                    tdlblcapacitychk.Visible = true;
                    tdChkCapacity.Visible = true;
                    divshowcapacity.Visible = true;
                }
                #endregion

                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                txtFromDate.Style.Add("color", "black !important");
                txtToDate.Style.Add("color", "black !important");
                menuID = Request.QueryString["MENUID"].ToString();
                Session["menuID"] = menuID;
                this.LoadMotherDepot();
                this.LoadSundry();
                this.LoadStoreLocation();
                ViewState["SumTotal"] = "0.00";
                if (chkActive.Checked)
                {
                    lbltext.Text = "Yes!";
                    lbltext.Style.Add("color", "green");
                    lbltext.Style.Add("font-weight", "bold");
                }
                else
                {
                    lbltext.Text = "No!";
                    lbltext.Style.Add("color", "red");
                    lbltext.Style.Add("font-weight", "bold");
                }

                foreach (ListItem item in ddlproduct.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        item.Attributes.CssStyle.Add("background-color", "Beige");
                    }
                }
            }

           
            else
            {
                #region Back Date Entry Validation
                ClsCommonFunction ClsCommon = new ClsCommonFunction();
                int Flag = ClsCommon.CheckDate(Request.QueryString["MENUID"].ToString().Trim());
                if (Flag > 0)
                {
                    this.ImageButton1.Enabled = true;
                    this.CalendarExtenderDespatchDate.Enabled = true;
                }
                else
                {
                    this.ImageButton1.Enabled = false;
                    this.CalendarExtenderDespatchDate.Enabled = false;
                }
                #endregion

                #region QueryString
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                FG = Request.QueryString["FG"].ToString().Trim();
                OP = Request.QueryString["OP"].ToString().Trim();
                ViewState["OP"] = OP;
                #endregion

                #region Add By Rajeev
                if (ViewState["OP"].ToString() == "QC")
                {
                    divbtnapprove.Visible = false;
                    divbtnrejection.Visible = false;
                    btnaddhide.Visible = false;
                    trpodetails.Visible = false;
                    grdDespatchHeader.Columns[22].Visible = false;
                    lblpagename.Text = "GRN QC DETAILS";
                    HDNISVERIFIEDCHECKER1.Value = "N";
                    HDNSTOCKIN.Value = "Y";
                    divDocuments.Visible = true;
                    Column_Print.Visible = true;
                    btnPrint.Visible = true;
                    Tdlblledger.Visible = false;
                    Tdddlledger.Visible = false;
                    TrWayBill.Visible = false;
                    divGrnSample.Visible = true;
                    tdlblcapacitychk.Visible = true;
                    tdChkCapacity.Visible = true;
                    divshowcapacity.Visible = true;
                }
                else if (ViewState["OP"].ToString() == "MAKER")
                {
                    divbtnapprove.Visible = false;
                    divbtnrejection.Visible = false;
                    btnaddhide.Visible = true;
                    trpodetails.Visible = true;
                    lblpagename.Text = "GRN DETAILS";
                    HDNISVERIFIEDCHECKER1.Value = "N";
                    HDNSTOCKIN.Value = "N";
                    divDocuments.Visible = false;
                    Column_Print.Visible = false;
                    btnPrint.Visible = false;
                    Tdlblledger.Visible = false;
                    Tdddlledger.Visible = false;
                    TrWayBill.Visible = false;
                    divGrnSample.Visible = false;
                    tdlblcapacitychk.Visible = false;
                    tdChkCapacity.Visible = false;
                    divshowcapacity.Visible = false;
                }
                else if (ViewState["OP"].ToString() == "Checker1")
                {
                    divbtnapprove.Visible = true;
                    divbtnrejection.Visible = true;
                    btnsubmitdiv.Visible = false;
                    btnaddhide.Visible = false;
                    trpodetails.Visible = false;
                    grdDespatchHeader.Columns[22].Visible = false;
                    lblpagename.Text = "PURCHASE STOCK RECEIPT-CHECKER 1";
                    HDNISVERIFIEDCHECKER1.Value = "Y";
                    divDocuments.Visible = true;
                    Column_Print.Visible = true;
                    btnPrint.Visible = true;
                    Tdlblledger.Visible = false;
                    Tdddlledger.Visible = false;
                    TrWayBill.Visible = true;
                    divGrnSample.Visible = true;
                    tdlblcapacitychk.Visible = true;
                    tdChkCapacity.Visible = true;
                    divshowcapacity.Visible = true;
                }
                #endregion                

                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                txtFromDate.Style.Add("color", "black !important");
                txtToDate.Style.Add("color", "black !important");
                this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                menuID = Request.QueryString["MENUID"].ToString();
                Session["menuID"] = menuID;
                this.LoadGRN();
                this.LoadMotherDepot();
                this.LoadSundry();
                ViewState["SumTotal"] = "0.00";
                if (chkActive.Checked)
                {
                    lbltext.Text = "Yes!";
                    lbltext.Style.Add("color", "green");
                    lbltext.Style.Add("font-weight", "bold");
                }
                else
                {
                    lbltext.Text = "No!";
                    lbltext.Style.Add("color", "red");
                    lbltext.Style.Add("font-weight", "bold");
                }
                foreach (ListItem item in ddlproduct.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        item.Attributes.CssStyle.Add("background-color", "Beige");
                    }
                }
                Btn_View(Request.QueryString["InvId"]);
            }
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>Tax_MakeStaticHeader('" + gvProductTax.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region BtnView GRN added by HPDAS
    public void Btn_View(string _InvoiceID)
    {
        try
        {
            hdnDespatchID.Value = _InvoiceID;
            ControlDisable();
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string BRAND = string.Empty;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal ItemWiseFreight = 0, ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;

            ViewState["SumTotal"] = 0;
            DataSet ds = new DataSet();
            DataTable dtWaybillNo = new DataTable();
            this.trAutoDespatchNo.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            div_btnPrint.Style["display"] = "";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                //this.txtRemarks.Enabled = false;
                this.lblQcRemarks.Visible = false;
                this.txtQcRemarks.Visible = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                //this.txtRemarks.Enabled = true;
                this.lblQcRemarks.Visible = true;
                this.txtQcRemarks.Visible = true;
            }
            #endregion

            this.LoadTax();
            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnDespatchID.Value);
            ds = clsgrnmm.EditReceivedDetails(receivedID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDID"]);
                this.txtDespatchNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDNO"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
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
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
                this.ddlInsuranceNumber.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();
                this.ddlGatePassNo.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtgatepassdate.Text = "";
                }
                else
                {
                    this.txtgatepassdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }

                //dtWaybillNo = clsgrnmm.BindWaybillEdit(this.ddlDepot.SelectedValue.Trim());

                this.ddlWaybill.Items.Clear();
                this.ddlWaybill.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]), Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"])));
                this.ddlWaybill.AppendDataBoundItems = true;

                //this.LoadProduct(this.ddlTPU.SelectedValue.Trim());
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                DataTable dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]);
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]);
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    dr["DESPATCHQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]);
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]);
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]);
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    dr["QCREJECTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QCREJECTQTY"]);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);
                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);

                    #region Loop For Adding Itemwise Tax Component

                    decimal excise = 0;
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = Clsdespatch.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = Clsdespatch.GetHSNTaxOnEdit(hdnDespatchID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                break;
                        }
                    }
                    #endregion

                    dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();
                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;
                ItemWiseFreight = CalculateTotalFreight(dtDespatchEdit);
                ItemWiseAddCost = CalculateTotalAddCost(dtDespatchEdit);
                ItemWiseDiscAmt = CalculateTotalDiscount(dtDespatchEdit);

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
                this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
                this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));

                #region Tax On Gross Amount
                this.LoadTax();
                this.LoadTermsConditions();
                DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionInnerGridDataTable();
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
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
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

                #region grdAddDespatch DataBind

                HttpContext.Current.Session["DESPATCHDETAILS"] = dtDespatchEdit;
                if (dtDespatchEdit.Rows.Count > 0)
                {
                    this.grdAddDespatch.DataSource = dtDespatchEdit;
                    this.grdAddDespatch.DataBind();
                    this.GridCalculation();
                }
                else
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();
                }

                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["TAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                #region grdAddDespatch DataBind
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                #endregion
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());

                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtNetAmt.Text)));
                ViewState["NETAMT"] = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtFinalAmt.Text.Trim()))));
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
                HttpContext.Current.Session["ADDN_DETAILS"] = dtAdd_DetailsEdit;
                gvadd.DataSource = dtAdd_DetailsEdit;
                gvadd.DataBind();
            }
            #endregion
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion

    #region Storelocation
    public void LoadStoreLocation()
    {
        try
        {
            string entrydate = this.txtDespatchDate.Text.ToString();
            ClsGRNMM grnmm = new ClsGRNMM();
            DataTable dt = grnmm.BindStoreLocation();
            if (dt.Rows.Count > 0)
            {
                this.ddlStorelocation.Items.Clear();
                this.ddlStorelocation.Items.Add(new ListItem("Select Storelocation", "0"));
                this.ddlStorelocation.AppendDataBoundItems = true;
                this.ddlStorelocation.DataSource = dt;
                this.ddlStorelocation.DataValueField = "ID";
                this.ddlStorelocation.DataTextField = "NAME";
                this.ddlStorelocation.DataBind();
            }
            else
            {
                this.ddlStorelocation.Items.Clear();
                this.ddlStorelocation.Items.Add(new ListItem("Select Storelocation", "0"));
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Load PO MM
    public void LoadPoMM(string TPUID)
    {
        try
        {
            if(this.ddlVendorFrom.SelectedValue=="C")
            {
                TPUID = "ConsuambleIdForPo";
            }
            string entrydate = this.txtDespatchDate.Text.ToString();
            ClsGRNMM grnmm = new ClsGRNMM();
            DataTable dt = grnmm.BindPOMMScheduleDateWise(TPUID, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(),entrydate);
            if (dt.Rows.Count > 0)
            {
                this.ddlpo.Items.Clear();
                this.ddlpo.Items.Add(new ListItem("Select PO", "0"));
                this.ddlpo.AppendDataBoundItems = true;
                this.ddlpo.DataSource = dt;
                this.ddlpo.DataValueField = "POID";
                this.ddlpo.DataTextField = "PONO";
                this.ddlpo.DataBind();
            }
            else
            {
                this.ddlpo.Items.Clear();
                this.ddlpo.Items.Add(new ListItem("Select PO", "0"));
            }
            if(TPUID == "ConsuambleIdForPo")
            {
                this.ddlpo.SelectedValue =Convert.ToString(dt.Rows[0]["POID"]);
                this.ddlpo.Enabled = false;
                LoadConsumableProduct();
                Cache["ISWORKORDER"] = "C";
            
            }
            else
            {
                this.ddlpo.Enabled = true;
            }
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
            DataTable dtTransporter = new DataTable();
            dtTransporter = clsgrnmm.BindTPU_Transporter(this.ddlTPU.SelectedValue.Trim());
            if (dtTransporter.Rows.Count > 0)
            {
                this.ddlTransporter.Items.Clear();
                this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
                this.ddlTransporter.AppendDataBoundItems = true;
                this.ddlTransporter.DataSource = dtTransporter;
                this.ddlTransporter.DataValueField = "ID";
                this.ddlTransporter.DataTextField = "NAME";
                this.ddlTransporter.DataBind();

                if (dtTransporter.Rows.Count == 1)
                {this.ddlTransporter.SelectedValue = Convert.ToString(dtTransporter.Rows[0]["ID"]);
                }
            }
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
            DataTable DT = new DataTable();
            string gatePassNo = this.ddlGatePassNo.SelectedValue;
            FG = Request.QueryString["FG"].ToString().Trim();
            this.ddlTPU.Items.Clear();
            this.ddlTPU.Items.Add(new ListItem("Select TPU/Vendor", "0"));
            this.ddlTPU.AppendDataBoundItems = true;
            DT = clsgrnmm.BindTPU(FG, gatePassNo, HttpContext.Current.Session["DEPOTID"].ToString());
            this.ddlTPU.DataSource = DT;
            this.ddlTPU.DataValueField = "VENDORID";
            this.ddlTPU.DataTextField ="VENDORNAME"; 
            this.ddlTPU.DataBind();
            this.txtgatepassdate.Text= DT.Rows[0]["ENTRYDATE"].ToString();
            this.txtInvoiceNo.Text = DT.Rows[0]["BILLNO"].ToString();
            this.txtInvoiceDate.Text = DT.Rows[0]["BILLDATE"].ToString();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadIssueProduct
    public void LoadIssueProduct()
    {
        try
        {
            this.ddlissueProduct.Items.Clear();
            this.ddlissueProduct.Items.Add(new ListItem("Select Issue Product", "0"));
            this.ddlissueProduct.AppendDataBoundItems = true;
            this.ddlissueProduct.DataSource = clsgrnmm.BindIssueProduct(HttpContext.Current.Session["DEPOTID"].ToString());
            this.ddlissueProduct.DataValueField = "PRODUCTID";
            this.ddlissueProduct.DataTextField = "PRODUCTALIAS";
            this.ddlissueProduct.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Load JobOrder Vendor
    public void LoadJobOrderVendor()
    {
        try
        {
            ClsFactoryReport Obj = new ClsFactoryReport();
            this.ddlTPU.Items.Clear();
            this.ddlTPU.Items.Add(new ListItem("Select TPU/Vendor", "0"));
            this.ddlTPU.AppendDataBoundItems = true;
            this.ddlTPU.DataSource = Obj.BindPoWiseTpu(HttpContext.Current.Session["DEPOTID"].ToString());
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

    #region LoadMotherDepot
    public void LoadMotherDepot()
    {
        try
        {
            DataTable dtDepot = new DataTable();
            dtDepot = clsgrnmm.BindFactory(Convert.ToString(Session["IUserID"]).Trim());
            this.ddlDepot.Items.Clear();
            this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
            this.ddlDepot.AppendDataBoundItems = true;
            if (dtDepot.Rows.Count > 0)
            {
                this.ddlDepot.DataSource = dtDepot;
                this.ddlDepot.DataValueField = "VENDORID";
                this.ddlDepot.DataTextField = "VENDORNAME";
                this.ddlDepot.DataBind();
                this.ddlDepot.SelectedValue = dtDepot.Rows[1]["VENDORID"].ToString();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTax
    public void LoadTax()
    {
        try
        {
            //if (hdnDespatchID.Value == "")
            //{
            //    DataTable dt = new DataTable();
            //    string flag = clsgrnmm.BindRegion(this.ddlTPU.SelectedValue.Trim(), ddlDepot.SelectedValue.ToString());

            //    if (string.IsNullOrEmpty(flag))
            //    {
            //        dt = clsgrnmm.BindTax(Session["menuID"].ToString(), "1", this.ddlTPU.SelectedValue.Trim(), this.ddlproduct.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim());
            //    }
            //    else
            //    {
            //        dt = clsgrnmm.BindTax(Session["menuID"].ToString(), "0", this.ddlTPU.SelectedValue.Trim(), this.ddlproduct.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim());
            //    }

            //    Session["GrossTotalTax"] = dt;
            //    if (dt.Rows.Count > 0)
            //    {
            //        this.grdTax.DataSource = (DataTable)Session["GrossTotalTax"];
            //        this.grdTax.DataBind();
            //    }
            //    else
            //    {
            //        this.grdTax.DataSource = null;
            //        this.grdTax.DataBind();
            //    }
            //}

            //else
            //{
            //    DataSet ds = new DataSet();
            //    string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            //    ds = clsgrnmm.EditReceivedDetails(despatchID);
            //    Session["GrossTotalTax"] = ds.Tables[4];
            //    if (ds.Tables[4].Rows.Count > 0)
            //    {
            //        this.grdTax.DataSource = (DataTable)Session["GrossTotalTax"];
            //        this.grdTax.DataBind();
            //    }
            //    else
            //    {
            //        this.grdTax.DataSource = null;
            //        this.grdTax.DataBind();
            //    }
            //}
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
            DataTable dt = new DataTable();
            dt = clsgrnmm.BindTerms(Session["menuID"].ToString());
            Session["Terms"] = dt;
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

    #region Load GRN
    public void LoadGRN()
    {
        try
        {
            ClsGRNMM GrnMM = new ClsGRNMM();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            FG = Request.QueryString["FG"].ToString().Trim();

            //this.grdDespatchHeader.DataSource = dtSource;
            //this.grdDespatchHeader.DataBind();
            DataTable dtSource = new DataTable();
               // HttpContext.Current.Cache["dtSource"] as DataTable;

            //if (dtSource.Rows.Count == 0)
            //{
                dtSource = GrnMM.BindReceived(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]), FG, ViewState["OP"].ToString());//Add one Parameter for Quality Control.
            //    HttpContext.Current.Cache["dtSource"] = dtSource;
            //}
            
            
            // Here we are converting cache into datatable
            grdDespatchHeader.DataSource = dtSource;
            grdDespatchHeader.DataBind();

        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadRejectionProductWisePacksize
    public void LoadRejectionProductWisePacksize(string ProductID, string Tag)
    {
        try
        {
            DataTable dtPackSize = new DataTable();
            dtPackSize = clsgrnmm.BindPackSize(ProductID, Tag);
            if (dtPackSize.Rows.Count > 0)
            {
                if (dtPackSize.Rows.Count == 1)
                {
                    this.ddlrejectionpacksize.Items.Clear();
                    this.ddlrejectionpacksize.Items.Add(new ListItem("Select Packsize/Unit", "0"));
                    this.ddlrejectionpacksize.AppendDataBoundItems = true;
                    this.ddlrejectionpacksize.DataSource = dtPackSize;
                    this.ddlrejectionpacksize.DataTextField = "PSNAME";
                    this.ddlrejectionpacksize.DataValueField = "PSID";
                    this.ddlrejectionpacksize.DataBind();
                    this.ddlrejectionpacksize.SelectedValue = Convert.ToString(dtPackSize.Rows[0]["PSID"]);
                }
                else if (dtPackSize.Rows.Count > 1)
                {
                    if (Tag == "TRUE")
                    {
                        this.ddlrejectionpacksize.Items.Clear();
                        this.ddlrejectionpacksize.DataSource = dtPackSize;
                        this.ddlrejectionpacksize.DataTextField = "PSNAME";
                        this.ddlrejectionpacksize.DataValueField = "PSID";
                        this.ddlrejectionpacksize.DataBind();
                    }
                    else
                    {
                        this.ddlrejectionpacksize.Items.Clear();
                        this.ddlrejectionpacksize.Items.Add(new ListItem("Select Packsize/Unit", "0"));
                        this.ddlrejectionpacksize.AppendDataBoundItems = true;
                        this.ddlrejectionpacksize.DataSource = dtPackSize;
                        this.ddlrejectionpacksize.DataTextField = "PSNAME";
                        this.ddlrejectionpacksize.DataValueField = "PSID";
                        this.ddlrejectionpacksize.DataBind();
                    }
                }
            }
            else
            {
                this.ddlrejectionpacksize.Items.Clear();
                this.ddlrejectionpacksize.Items.Add(new ListItem("Select Packsize/Unit", "0"));
                this.ddlrejectionpacksize.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region CFormRequired
    public int CFormRequired(string FromVendorID, string ToDepotID)
    {
        int req = 0;
        try
        {
            ClsGRNMM GrnMM = new ClsGRNMM();
            req = GrnMM.CFORM(FromVendorID, ToDepotID);
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return req;
    }
    #endregion

    #region ddlpo_SelectedIndexChanged
    protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpo.SelectedValue != "0")
        {
            if(this.ddlVendorFrom.SelectedValue== "C")
            {
                this.LoadConsumableProduct();
                Cache["ISWORKORDER"] = "C";
            }
            else
            {
                this.LoadProductDetails();
            }
           
        }
        else
        {
            this.txtPoDate.Text = "";
        }
    }
    #endregion

    #region TCS(%) & TCS Limit
    protected void BindTCSDetails(string VendorID)
    {
        clsDespatchStock clsDespatchStock = new clsDespatchStock();
        DataTable dtTCS = new DataTable();
        if (this.ddlDepot.SelectedValue != "0")
        {
            dtTCS = clsDespatchStock.BindTPUTCSPercent(VendorID);
            this.txtTCSPercent.Text = Convert.ToString(dtTCS.Rows[0]["TCS_PERCENT"]).Trim();
        }
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
            this.divtcs.Visible = true;
        }
        else
        {
            this.divtcs.Visible = false;
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtInner = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));                     //2
        dt.Columns.Add(new DataColumn("POID", typeof(string)));                     //3
        dt.Columns.Add(new DataColumn("PODATE", typeof(string)));                   //4
        dt.Columns.Add(new DataColumn("PONO", typeof(string)));                     //5
        dt.Columns.Add(new DataColumn("POQTY", typeof(string)));                    //6
        dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));                  //7(Add By Rajeev 01-07-2017)
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));                //8
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));              //9
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));            //10
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));          //11
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));                      //12
        dt.Columns.Add(new DataColumn("DESPATCHQTY", typeof(string)));              //13
        dt.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(string)));              //14
        dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));             //15
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));                     //16
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));                   //17
        dt.Columns.Add(new DataColumn("DISCOUNTPER", typeof(string)));              //18
        dt.Columns.Add(new DataColumn("DISCOUNTAMT", typeof(string)));              //19
        dt.Columns.Add(new DataColumn("AFTERDISCOUNTAMT", typeof(string)));         //20
        dt.Columns.Add(new DataColumn("ITEMWISEFREIGHT", typeof(string)));          //21(Add By Rajeev 01-12-2017)
        dt.Columns.Add(new DataColumn("AFTERITEMWISEFREIGHTAMT", typeof(string)));  //22(Add By Rajeev 01-12-2017)
        dt.Columns.Add(new DataColumn("ITEMWISEADDCOST", typeof(string)));          //23(Add By Rajeev 13-12-2017)
        dt.Columns.Add(new DataColumn("AFTERITEMWISEADDCOSTAMT", typeof(string)));  //24(Add By Rajeev 13-12-2017)
        dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));                   //25
        dt.Columns.Add(new DataColumn("QCREJECTQTY", typeof(decimal)));                  //26
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));      //27
        dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string)));      //28
        dt.Columns.Add(new DataColumn("DEPOTRATE", typeof(string)));                //29

        #region Loop For Adding Itemwise Tax Component
        if (hdnDespatchID.Value == "")
        {
            string flag = clsgrnmm.BindRegion(this.ddlTPU.SelectedValue.Trim(), ddlDepot.SelectedValue.ToString().Trim());

            if (string.IsNullOrEmpty(flag))
            {
                dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlTPU.SelectedValue.Trim(), this.ddlproduct.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
            }
            else
            {
                dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlTPU.SelectedValue.Trim(), this.ddlproduct.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(), this.txtInvoiceDate.Text.Trim());
            }
            Session["dtTaxCount"] = dtTaxCount;
            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsgrnmm.EditReceivedDetails(despatchID);
            Session["dtTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        #endregion

        dt.Columns.Add(new DataColumn("NETWEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEMDES", typeof(string)));
        HttpContext.Current.Session["DESPATCHDETAILS"] = dt;
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
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
            HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dt;
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
            HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dt;
        }
        return dt;
    }
    #endregion

    /*Added By Avishek Ghosh On 13.11.2017*/
    #region Create DataTable Rejection Tax Component Structure
    public DataTable CreateDataTableRejectionTaxComponent()
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
        HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"] = dt;
        return dt;
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
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));
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
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
        HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region CreateDataTable_SampleQty
    private void CreateDataTable_SampleQty()
    {
        DataTable dtSample = new DataTable();
        dtSample.Clear();
        dtSample.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("POID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dtSample.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(string)));
        dtSample.Columns.Add(new DataColumn("SAMPLEQTY", typeof(string)));
        dtSample.Columns.Add(new DataColumn("OBSERVATIONQTY", typeof(string)));
        HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"] = dtSample;
    }
    #endregion

    private void CreateDataTable_UploadCapacity()
    {
        DataTable dtUpload = new DataTable();
        dtUpload.Clear();
        dtUpload.Columns.Add("FILENAME", typeof(string));
        HttpContext.Current.Session["UPLOADCAPACITYFILE"] = dtUpload;
    }

    #region LoadPurchaseOrderDetails
    protected void LoadPurchaseOrderDetails()
    {
        DataTable DtWO = new DataTable();
        if (ddlproduct.SelectedValue == "0")
        {
            MessageBox1.ShowInfo("<b>Please select Product!</b>");
            return;
        }
        else
        {
            DataTable dtPoDetails = new DataTable();
            string packsizeID = ddlPackSize.SelectedValue;
            if (Cache["ISWORKORDER"].ToString() == "N")
            {
                if (this.hdnDespatchID.Value == "")
                {
                    dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), "", txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                }
                else
                {
                    dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), this.hdnDespatchID.Value.Trim(), txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                }
            }
            else
            {
                if (this.hdnDespatchID.Value == "")
                {
                    DataTable DtWO_Copy = new DataTable();
                    txttotalreceved.Text = "0.00";
                    txtReceive.Text = "0.00";
                    dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), "", txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                    DtWO = clsgrnmm.BindWorkOrderReceived(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnJobOrderReceived.Value == "" ? "0" : hdnJobOrderReceived.Value);
                    if (ddlDepot.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE")
                    {
                        gvJobOrderdetails.DataSource = DtWO;
                        gvJobOrderdetails.DataBind();
                        gvJobOrderdetails.Columns[7].Visible = false;
                        DtWO_Copy = DtWO.Copy();
                    }
                    else
                    {
                        gvJobOrderdetails.DataSource = DtWO;
                        gvJobOrderdetails.DataBind();
                        gvJobOrderdetails.Columns[7].Visible = true;
                    }
                    Session["WORKORDERDETAILS"] = DtWO;
                    lblJobdespatchID.Text = DtWO.Rows[0]["STOCKDESPATCHID"].ToString();
                    txtDespatchQty.Enabled = false;
                    txtpopproduct.Text = dtPoDetails.Rows[0]["PRODUCTNAME"].ToString();
                    this.WorkOrderPopup.Show();
                }
                else
                {
                    txttotalreceved.Text = "0.00";
                    txtReceive.Text = "0.00";
                    dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), this.hdnDespatchID.Value.Trim(), txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                    DtWO = clsgrnmm.BindWorkOrderReceived(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnJobOrderReceived.Value == "" ? "0" : hdnJobOrderReceived.Value);
                    if (ddlDepot.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE")
                    {
                        gvJobOrderdetails.DataSource = DtWO;
                        gvJobOrderdetails.DataBind();
                        gvJobOrderdetails.Columns[7].Visible = false;
                    }
                    else
                    {
                        gvJobOrderdetails.DataSource = DtWO;
                        gvJobOrderdetails.DataBind();
                        gvJobOrderdetails.Columns[7].Visible = true;
                    }
                    Session["WORKORDERDETAILS"] = DtWO;
                    lblJobdespatchID.Text = DtWO.Rows[0]["STOCKDESPATCHID"].ToString();
                    txtDespatchQty.Enabled = false;
                    txtpopproduct.Text = dtPoDetails.Rows[0]["PRODUCTNAME"].ToString();
                    this.WorkOrderPopup.Show();
                }
            }

            if (dtPoDetails.Rows.Count > 0 && Cache["ISWORKORDER"].ToString() == "N")
            {
                this.txtAssementPercentage.Text = Convert.ToString(dtPoDetails.Rows[0]["ASSESSABLEPERCENT"]);
                this.txtMRP.Text = Convert.ToString(dtPoDetails.Rows[0]["MRP"]);
                this.txtWeight.Text = Convert.ToString(dtPoDetails.Rows[0]["WEIGHT"]);
                this.txtRate.Text = Convert.ToString(dtPoDetails.Rows[0]["RATE"]);
                this.txtAlreadyDespatchQty.Text = Convert.ToString(dtPoDetails.Rows[0]["DEPOTWISE_DESPATCH_QTY"]);
                this.txtPoQty.Text = Convert.ToString(dtPoDetails.Rows[0]["PO_QTY"]);
                this.txtTotalAssement.Text = "0";
                this.txtPoDate.Text = Convert.ToString(dtPoDetails.Rows[0]["PODATE"]);
                this.txtQcQty.Text = ddlproduct.SelectedItem.Text.Substring(77, 14).Trim();
                ViewState["HSNCODE"] = Convert.ToString(dtPoDetails.Rows[0]["HSE"]);
                //this.Label13.Text = "Already <br>Received Qty <br> In " + this.ddlDepot.SelectedItem.ToString() + " Depot";
            }
            else if (dtPoDetails.Rows.Count > 0 && Cache["ISWORKORDER"].ToString() == "Y")
            {
                this.txtAssementPercentage.Text = Convert.ToString(dtPoDetails.Rows[0]["ASSESSABLEPERCENT"]);
                this.txtMRP.Text = Convert.ToString(dtPoDetails.Rows[0]["MRP"]);
                this.txtWeight.Text = Convert.ToString(dtPoDetails.Rows[0]["WEIGHT"]);
                this.txtRate.Text = Convert.ToString(dtPoDetails.Rows[0]["RATE"]);
                this.txtAlreadyDespatchQty.Text = Convert.ToString(dtPoDetails.Rows[0]["DEPOTWISE_DESPATCH_QTY"]);
                this.txtPoQty.Text = Convert.ToString(dtPoDetails.Rows[0]["PO_QTY"]);
                this.txtTotalAssement.Text = "0";
                this.txtPoDate.Text = Convert.ToString(dtPoDetails.Rows[0]["PODATE"]);
                this.txtQcQty.Text = "0";//ddlproduct.SelectedItem.Text.Substring(77, 14).Trim();
                ViewState["HSNCODE"] = Convert.ToString(dtPoDetails.Rows[0]["HSE"]);
            }
            else
            {
                //this.Label13.Text = "Already <br>Received Qty <br> In " + this.ddlDepot.SelectedItem.ToString() + " Depot";
                this.txtTotDespatch.Text = "";
                this.txtAssementPercentage.Text = "";
                this.txtTotalAssement.Text = "";
                this.txtMfDate.Text = "";
                this.txtExprDate.Text = "";
                this.txtMRP.Text = "";
                this.txtWeight.Text = "";
                this.txtRate.Text = "";
                this.txtitemwiseFreight.Text = "";
                this.txtitemwiseAddCost.Text = "";
                this.txtQcQty.Text = "";
                this.txtAlreadyDespatchQty.Text = "";
                this.txtPoQty.Text = "";
                this.txtAllocatedQty.Text = "";
                this.txtTotalAssement.Text = "";
            }
        }
    }
    #endregion

    #region GridQty
    protected decimal GridQty(string ProductID)
    {
        decimal qty = 0;
        string Product = string.Empty;
        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            Product = row.Cells[4].Text.Trim();
            if (Product == ProductID)
            {
                qty += Convert.ToDecimal(row.Cells[9].Text.Trim());
            }
        }
        return qty;
    }
    #endregion

    #region BatchGridQty
    protected decimal BatchGridQty(string ProductID, string BatchID)
    {
        decimal qty = 0;
        string Product = string.Empty;
        string Batch = string.Empty;
        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            Product = row.Cells[4].Text.Trim();
            Batch = row.Cells[13].Text.Trim();
            if (Product == ProductID && Batch == BatchID)
            {
                qty += Convert.ToDecimal(row.Cells[9].Text.Trim());
            }
        }
        return qty;
    }
    #endregion

    #region Add Records
    protected void btnADDGrid_Click(object sender, EventArgs e)
    {

        if(this.ddlPackSize.SelectedValue== "B9F29D12-DE94-40F1-A668-C79BF1BF4425")
        {
            bool qty= VerifyNumericValue(this.txtDespatchQty.Text);
            if(qty== false)
            {
                MessageBox1.ShowWarning("You Cannot Enter QTY into Decimal");
                return;
            }
        }
        #region Add Mode

        if (hdnDespatchID.Value == "")
        {
            ClsOpenStock clsopenstock = new ClsOpenStock();
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            DataTable dtDespatch = (DataTable)Session["DESPATCHDETAILS"];
            decimal Currentdespatchqty = Convert.ToDecimal(this.txtDespatchQty.Text);
            decimal depotwisedespatchqty = 0;
            decimal alldepotdespatchqty = 0;
            decimal TotAmt = 0;
            decimal TotalMRP = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            string BRAND = string.Empty;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal ItemWiseFreight = 0, ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;
            decimal TotAssesment = 0;
            string catid = "";

            string BATCHNO = string.Empty;
           
            this.RbApplicable.SelectedValue = "N";
            this.divtcs.Visible = false;

            this.txtTCSPercent.Text = "0";
            this.txtTCS.Text = "0";
            this.txtTCSNetAmt.Text = "0";
            this.txtTCSApplicable.Text = "0";
            this.txtTCSApplicable.Enabled = false;
            if (Currentdespatchqty > 0)
            {
                DataTable dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), this.lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), "", txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                if (dtPoDetails.Rows.Count > 0)
                {
                    depotwisedespatchqty = Convert.ToDecimal(dtPoDetails.Rows[0]["DEPOTWISE_DESPATCH_QTY"].ToString());
                    alldepotdespatchqty = Convert.ToDecimal(dtPoDetails.Rows[0]["DESPATCH_QTY"].ToString());
                    catid = (dtPoDetails.Rows[0]["CATID"].ToString());
                }
                depotwisedespatchqty = depotwisedespatchqty + Currentdespatchqty + GridQty(this.lblproductid.Text.Trim().Trim());
                alldepotdespatchqty = alldepotdespatchqty + Currentdespatchqty + BatchGridQty(this.lblproductid.Text.Trim().Trim(), this.txtBatch.Text.Trim());
               
                /**Added 5% tolerance in Received Qty (Soumitra Mondal)16/12/2019**/
                decimal Exter_percentage = 0;
                decimal Exter_QcQty = 0;
                Exter_percentage = ((Convert.ToDecimal(this.txtPoQty.Text) * 5) / 100);
                //Exter_QcQty = (Convert.ToDecimal(this.txtQcQty.Text) + Exter_percentage);
                Exter_QcQty = (Convert.ToDecimal(this.txtQcQty.Text));

                //if (Convert.ToDecimal(this.txtDespatchQty.Text) > Exter_QcQty && Cache["ISWORKORDER"].ToString() == "N")
                //{
                //    MessageBox1.ShowInfo("Received qty. should not be greater than PO qty.", 80, 500);
                //}
                //else 
                //if (Convert.ToDecimal(txtRate.Text) <= 0)
                //{
                //    MessageBox1.ShowInfo("<b><font color='red'>Rate Can not be Zero.</font></b>");
                //}
                //else
                //{
                    int numberOfRecords = 0;
                    if (txtBatch.Text != "")
                    {
                        numberOfRecords = dtDespatch.Select("POID = '" + this.ddlpo.SelectedValue.Trim() + "' AND PRODUCTID = '" + this.lblproductid.Text.Trim() + "'AND BATCHNO='" + txtBatch.Text + "'").Length;
                    }
                    else
                    {
                        numberOfRecords = dtDespatch.Select("POID = '" + this.ddlpo.SelectedValue.Trim() + "' AND PRODUCTID = '" + this.lblproductid.Text.Trim() + "'").Length;
                    }
                    if (numberOfRecords > 0)
                    {
                        MessageBox1.ShowError("<b><font color='red'>Record already exists..!</font></b>");
                        this.ResetControls();
                    }
                    else
                    {
                        string mastermfgdate = string.Empty;
                        string masterexpdate = string.Empty;
                        DataTable dtbatchcheck = new DataTable();
                        dtbatchcheck = clsopenstock.MasterBatchDetailsCheckFactory(this.lblproductid.Text.Trim(), txtBatch.Text.Trim(), Convert.ToDecimal(txtMRP.Text.Trim()), txtMfDate.Text.Trim(), txtExprDate.Text.Trim(), this.Session["FinYear"].ToString().Trim());

                        if (!string.IsNullOrEmpty(dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim()))
                        {
                            mastermfgdate = dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim();
                            masterexpdate = dtbatchcheck.Rows[0]["EXPRDATE"].ToString().Trim();

                            this.txtMfDate.Text = mastermfgdate.Trim();
                            this.txtExprDate.Text = masterexpdate.Trim();
                        }
                        decimal Amount = 0;
                        Amount = clsgrnmm.CalculateNonFGAmount(lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue.Trim(), this.txtDespatchQty.Text.Trim(), Convert.ToDecimal(this.txtRate.Text.Trim()));
                        TotAmt += Amount;

                        if (txtAssementPercentage.Text.Trim() == "")
                        {
                            txtAssementPercentage.Text = "0";
                        }
                        decimal TotMRP = clsgrnmm.CalculateAmountInPcs(lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue, Convert.ToDecimal(this.txtDespatchQty.Text), Convert.ToDecimal(this.txtMRP.Text));
                        decimal excise = 0;

                        #region Fetch Discount Details
                        DataTable disc = new DataTable();
                        decimal AfterDiscountAmt = 0;
                        disc = clsgrnmm.BindVendorDisc(this.ddlTPU.SelectedValue, this.lblproductid.Text.Trim(), txtInvoiceDate.Text, "");
                        decimal DiscountPer = Convert.ToDecimal(disc.Rows[0]["DISCOUNTPER"].ToString());
                        decimal DiscountAmt = Convert.ToDecimal(disc.Rows[0]["DISCOUNTAMT"].ToString());
                        if (DiscountPer == 0)
                        {
                            AfterDiscountAmt = Amount - DiscountAmt;
                        }
                        else
                        {
                            AfterDiscountAmt = Amount - (Amount * DiscountPer / 100);
                            DiscountAmt = (Amount * DiscountPer / 100);
                        }
                        #endregion

                        DataSet dsweight = clsgrnmm.Weight(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue.Trim(), Convert.ToDecimal(this.txtDespatchQty.Text.Trim()), Request.QueryString["FG"].ToString().Trim());
                        if (this.txtBatch.Text.Trim() == "")
                        {
                            BATCHNO = "NA";
                        }
                        else
                        {
                            BATCHNO = this.txtBatch.Text.Trim();
                        }
                        DataRow dr = dtDespatch.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["POID"] = Convert.ToString(this.ddlpo.SelectedValue.Trim());
                        dr["PONO"] = Convert.ToString(this.ddlpo.SelectedItem.Text);
                        dr["PODATE"] = Convert.ToString(this.txtPoDate.Text.Trim());
                        dr["POQTY"] = Convert.ToString(dtPoDetails.Rows[0]["PO_QTY"].ToString());
                        dr["HSNCODE"] = ViewState["HSNCODE"].ToString().Trim();
                        dr["PRODUCTID"] = Convert.ToString(lblproductid.Text.Trim());
                        dr["PRODUCTNAME"] = Convert.ToString(lblproductname.Text).Trim();
                        dr["PACKINGSIZEID"] = Convert.ToString(this.ddlPackSize.SelectedValue.Trim());
                        dr["PACKINGSIZENAME"] = Convert.ToString(this.ddlPackSize.SelectedItem).Trim();
                        dr["MRP"] = Convert.ToString(txtMRP.Text.Trim());
                        if(Cache["ISSERVICE"].ToString()=="T")
                        {
                            dr["DESPATCHQTY"] = 0;
                        }
                        else
                        {
                            dr["DESPATCHQTY"] = Convert.ToString(Convert.ToDecimal(txtDespatchQty.Text.Trim()) + Convert.ToDecimal(this.txtAlreadyDespatchQty.Text.Trim()));
                        }
                        if (Cache["ISSERVICE"].ToString() == "T")
                        {
                            dr["RECEIVEDQTY"] = 0;
                        }
                        else
                        {
                            dr["RECEIVEDQTY"] = Convert.ToString(String.Format("{0:0.00}", txtDespatchQty.Text.Trim()));
                        }

                        if (Cache["ISWORKORDER"].ToString() == "N")
                        {
                            if (Cache["ISSERVICE"].ToString() == "T")
                            {
                                dr["REMAININGQTY"] =0;
                            }
                            else
                            {
                                dr["REMAININGQTY"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtQcQty.Text.Trim()) - Convert.ToDecimal(txtDespatchQty.Text.Trim())));
                            }
                              
                        }
                       
                        else
                        {
                            dr["REMAININGQTY"] = "0";
                        }

                        dr["RATE"] = Convert.ToString(txtRate.Text.Trim());
                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", Amount));
                        dr["DISCOUNTPER"] = Convert.ToString(String.Format("{0:0.00}", DiscountPer));
                        dr["DISCOUNTAMT"] = Convert.ToString(String.Format("{0:0.00}", DiscountAmt));
                        dr["AFTERDISCOUNTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterDiscountAmt));
                        dr["ITEMWISEFREIGHT"] = Convert.ToString(String.Format("{0:0.00}", txtitemwiseFreight.Text));
                        decimal AfterItemWiseFreightAmt = (Convert.ToDecimal(txtitemwiseFreight.Text) + AfterDiscountAmt);
                        dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseFreightAmt));

                        dr["ITEMWISEADDCOST"] = Convert.ToString(String.Format("{0:0.00}", txtitemwiseAddCost.Text));
                        decimal AfterItemWiseAddCostAmt = (Convert.ToDecimal(txtitemwiseAddCost.Text) + AfterItemWiseFreightAmt);
                        dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseAddCostAmt));

                        dr["TOTMRP"] = Convert.ToString(String.Format("{0:0.00}", TotMRP));
                        dr["QCREJECTQTY"] = "0";
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(txtAssementPercentage.Text.Trim());
                        dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesment));

                        excise = 0;

                        #region Loop For Adding Itemwise Tax Component
                        DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                        ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                            {
                                case "1":
                                    TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = Clsdespatch.GetHSNTax(TAXID, Convert.ToString(this.ddlproduct.SelectedValue.ToString().Substring(0, this.ddlproduct.SelectedValue.ToString().IndexOf("~"))).Trim(), ddlTPU.SelectedValue.ToString().Trim(), txtInvoiceDate.Text.Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseAddCostAmt * ProductWiseTax / 100));
                                    break;
                            }
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());

                            CreateTaxDatatable(Convert.ToString(this.ddlpo.SelectedValue.Trim()),
                                               Convert.ToString(this.lblproductid.Text.Trim()),
                                               BATCHNO,
                                               dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                               Convert.ToString(ProductWiseTax),
                                               dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                               Convert.ToString(lblproductname.Text).Trim(),
                                               TAXID);
                        }
                        #endregion

                        if (dsweight.Tables[0].Rows.Count > 0)
                        {
                            dr["NETWEIGHT"] = Convert.ToString(dsweight.Tables[0].Rows[0]["NETWEIGHT"]);
                        }
                        else
                        {
                            dr["NETWEIGHT"] = "0";
                        }
                        dr["MFDATE"] = Convert.ToString(txtMfDate.Text.Trim());
                        dr["EXPRDATE"] = Convert.ToString(txtExprDate.Text.Trim());
                        if (dsweight.Tables[1].Rows.Count > 0)
                        {
                            dr["GROSSWEIGHT"] = Convert.ToString(dsweight.Tables[1].Rows[0]["GROSSWEIGHT"]);
                        }
                        else
                        {
                            dr["GROSSWEIGHT"] = "0";
                        }
                        decimal DepotRate = clsgrnmm.BindDepotRate(Convert.ToString(this.lblproductid.Text.Trim().Trim()), Convert.ToDecimal(txtMRP.Text.Trim()), this.txtInvoiceDate.Text.Trim());
                        dr["DEPOTRATE"] = Convert.ToString(DepotRate);
                        dr["ITEMDES"] = Convert.ToString(this.Textitemdescription.Text);

                        dtDespatch.Rows.Add(dr);
                        dtDespatch.AcceptChanges();

                        TotalMRP = CalculateTotalMRP(dtDespatch);
                        TotalAmount = CalculateGrossTotal(dtDespatch);
                        TotalTax = CalculateTaxTotal(dtDespatch);
                        GrossTotal = TotalAmount + TotalTax;
                        ItemWiseFreight = CalculateTotalFreight(dtDespatch);
                        ItemWiseAddCost = CalculateTotalAddCost(dtDespatch);
                        ItemWiseDiscAmt = CalculateTotalDiscount(dtDespatch);

                        this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                        this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                        this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
                        this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
                        this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));

                        #region Tax On Gross Amount Commented 
                        /*DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
                        if (dtGrossTax.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtGrossTax.Rows.Count; i++)
                            {
                                dtGrossTax.Rows[i]["TAXVALUE"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dtGrossTax.Rows[i]["PERCENTAGE"]) * Convert.ToDecimal(GrossTotal) / Convert.ToDecimal(100)));
                            }

                            dtGrossTax.AcceptChanges();

                        }
                        this.grdTax.DataSource = dtGrossTax;
                        this.grdTax.DataBind();*/
                        #endregion

                        //GrossFinal = CalculateFinalGrossTotal(dtGrossTax);

                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
                        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
                        this.ResetControls();
                        ViewState["NETAMT"] = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtFinalAmt.Text.Trim()))));
                    }
                //}
                DataTable dtProductTax = new DataTable();
                if (Session["TAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
                }

                if (dtProductTax.Rows.Count > 0)
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                    this.ddlGatePassNo.Enabled = false;
                    this.ddlVendorFrom.Enabled = false;
                    this.ddlTPU.Enabled = false;
                }
                else
                {
                    this.gvProductTax.DataSource = dtProductTax;
                    this.gvProductTax.DataBind();
                    this.ddlGatePassNo.Enabled = true;
                    this.ddlVendorFrom.Enabled = true;
                    this.ddlTPU.Enabled = true;
                }
            }
            else
            {
                MessageBox1.ShowInfo("Received Qty should be greater than 0'", 60, 500);
            }
            if (dtDespatch.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatch;
                this.grdAddDespatch.DataBind();
                this.GridCalculation();
                DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTaxCount"];
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }
        }
        #endregion

        #region Edit Mode
        else
        {
            ClsOpenStock clsopenstock = new ClsOpenStock();
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            DataTable dtDespatch = (DataTable)Session["DESPATCHDETAILS"];
            decimal Currentdespatchqty = Convert.ToDecimal(this.txtDespatchQty.Text);
            decimal depotwisedespatchqty = 0;
            decimal alldepotdespatchqty = 0;
            decimal TotAmt = 0;
            decimal TotalMRP = 0;
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal GrossFinal = 0;
            string BRAND = string.Empty;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal ItemWiseFreight = 0, ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;
            string BATCHNO = string.Empty;
            if (Currentdespatchqty > 0)
            {
                DataTable dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), this.lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), "", txtInvoiceDate.Text.Trim(), ddlTPU.SelectedValue.Trim());
                if (dtPoDetails.Rows.Count > 0)
                {
                    depotwisedespatchqty = Convert.ToDecimal(dtPoDetails.Rows[0]["DEPOTWISE_DESPATCH_QTY"].ToString());
                    alldepotdespatchqty = Convert.ToDecimal(dtPoDetails.Rows[0]["DESPATCH_QTY"].ToString());
                }
                depotwisedespatchqty = depotwisedespatchqty + Currentdespatchqty + GridQty(this.lblproductid.Text.Trim().Trim());
                alldepotdespatchqty = alldepotdespatchqty + Currentdespatchqty + BatchGridQty(this.lblproductid.Text.Trim().Trim(), this.txtBatch.Text.Trim());

                /**Added 5% tolerance in Received Qty (Soumitra Mondal)16/12/2019**/
                decimal Exter_percentage = 0;
                decimal Exter_QcQty = 0;
                Exter_percentage = ((Convert.ToDecimal(this.txtPoQty.Text) * 5) / 100);
                Exter_QcQty = (Convert.ToDecimal(this.txtQcQty.Text) + Exter_percentage);
                //if (Convert.ToDecimal(this.txtDespatchQty.Text) > Exter_QcQty && Cache["ISWORKORDER"].ToString() == "N")
                //{
                //    MessageBox1.ShowInfo("Received qty. should not be greater than PO qty.", 80, 500);
                //}
                //else
                //if (Convert.ToDecimal(txtRate.Text) <= 0)
                //{
                //    MessageBox1.ShowInfo("<b><font color='red'>Rate Can not be Zero.</font></b>");
                //}
                //else
                //{
                    int numberOfRecords = 0;
                    numberOfRecords = dtDespatch.Select("POID = '" + this.ddlpo.SelectedValue.Trim() + "' AND PRODUCTID = '" + this.lblproductid.Text.Trim() + "'").Length;

                    if (numberOfRecords > 0)
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Record already exists..!</font></b>");
                        this.ResetControls();
                    }
                    else
                    {
                        string mastermfgdate = string.Empty;
                        string masterexpdate = string.Empty;
                        DataTable dtbatchcheck = new DataTable();
                        dtbatchcheck = clsopenstock.MasterBatchDetailsCheckFactory(this.lblproductid.Text.Trim().Trim(), txtBatch.Text.Trim(), Convert.ToDecimal(txtMRP.Text.Trim()), txtMfDate.Text.Trim(), txtExprDate.Text.Trim(), this.Session["FinYear"].ToString().Trim());

                        if (!string.IsNullOrEmpty(dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim()))
                        {
                            mastermfgdate = dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim();
                            masterexpdate = dtbatchcheck.Rows[0]["EXPRDATE"].ToString().Trim();

                            this.txtMfDate.Text = mastermfgdate.Trim();
                            this.txtExprDate.Text = masterexpdate.Trim();
                        }
                        decimal Amount = 0;

                        Amount = clsgrnmm.CalculateNonFGAmount(this.lblproductid.Text.Trim().Trim(), this.ddlPackSize.SelectedValue.Trim(), this.txtDespatchQty.Text.Trim(), Convert.ToDecimal(this.txtRate.Text.Trim()));
                        TotAmt += Amount;

                        decimal TotAssesment = 0;

                        if (txtAssementPercentage.Text == "")
                        {
                            txtAssementPercentage.Text = "0";
                        }
                        if (Convert.ToDecimal(this.txtMRP.Text) > 0)
                        {
                            /* TotAssesment = (clsgrnmm.CalculateAmountInPcs(this.lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue,
                                                            Convert.ToDecimal(this.txtDespatchQty.Text), Convert.ToDecimal(this.txtMRP.Text))
                                                            * Convert.ToDecimal(txtAssementPercentage.Text.Trim()) / 100);*/
                        }
                        else
                        {
                            /*TotAssesment = (clsgrnmm.CalculateAmountInPcs(this.lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue,
                                                              Convert.ToDecimal(this.txtDespatchQty.Text), Convert.ToDecimal(this.txtRate.Text))
                                                              * Convert.ToDecimal(txtAssementPercentage.Text.Trim()) / 100);*/
                        }
                        decimal TotMRP = clsgrnmm.CalculateAmountInPcs(this.lblproductid.Text.Trim(), this.ddlPackSize.SelectedValue,
                                                                                Convert.ToDecimal(this.txtDespatchQty.Text), Convert.ToDecimal(this.txtMRP.Text));
                        decimal excise = 0;

                        #region Fetch Discount Details
                        DataTable disc = new DataTable();
                        decimal AfterDiscountAmt = 0;
                        disc = clsgrnmm.BindVendorDisc(this.ddlTPU.SelectedValue, this.lblproductid.Text.Trim(), txtInvoiceDate.Text, "");
                        decimal DiscountPer = Convert.ToDecimal(disc.Rows[0]["DISCOUNTPER"].ToString());
                        decimal DiscountAmt = Convert.ToDecimal(disc.Rows[0]["DISCOUNTAMT"].ToString());

                        if (DiscountPer == 0)
                        {
                            AfterDiscountAmt = Amount - DiscountAmt;
                        }
                        else
                        {
                            AfterDiscountAmt = Amount - (Amount * DiscountPer / 100);
                            DiscountAmt = (Amount * DiscountPer / 100);
                        }
                        #endregion

                        DataSet dsweight = clsgrnmm.Weight(this.ddlpo.SelectedValue.Trim(), this.lblproductid.Text.Trim().Trim(), this.ddlPackSize.SelectedValue.Trim(), Convert.ToDecimal(this.txtDespatchQty.Text.Trim()), Request.QueryString["FG"].ToString().Trim());
                        if (this.txtBatch.Text.Trim() == "")
                        {
                            BATCHNO = "NA";
                        }
                        else
                        {
                            BATCHNO = this.txtBatch.Text.Trim();
                        }
                        DataRow dr = dtDespatch.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["POID"] = Convert.ToString(this.ddlpo.SelectedValue.Trim());
                        dr["PONO"] = Convert.ToString(this.ddlpo.SelectedItem.Text);
                        dr["PODATE"] = Convert.ToString(this.txtPoDate.Text.Trim());
                        dr["POQTY"] = Convert.ToString(dtPoDetails.Rows[0]["PO_QTY"].ToString());
                        dr["HSNCODE"] = ViewState["HSNCODE"].ToString().Trim();
                        dr["PRODUCTID"] = Convert.ToString(this.lblproductid.Text.Trim().Trim());
                        dr["PRODUCTNAME"] = Convert.ToString(this.lblproductname.Text).Trim();
                        dr["PACKINGSIZEID"] = Convert.ToString(this.ddlPackSize.SelectedValue.Trim());
                        dr["PACKINGSIZENAME"] = Convert.ToString(this.ddlPackSize.SelectedItem).Trim();
                        dr["MRP"] = Convert.ToString(txtMRP.Text.Trim());
                        dr["DESPATCHQTY"] = Convert.ToString(Convert.ToDecimal(txtDespatchQty.Text.Trim()) + Convert.ToDecimal(this.txtAlreadyDespatchQty.Text.Trim()));
                        dr["RECEIVEDQTY"] = Convert.ToString(String.Format("{0:0.00}", txtDespatchQty.Text.Trim()));
                        //dr["REMAININGQTY"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtQcQty.Text.Trim()) - Convert.ToDecimal(txtDespatchQty.Text.Trim())));

                        if (Cache["ISWORKORDER"].ToString() == "N")
                        {
                            dr["REMAININGQTY"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtQcQty.Text.Trim()) - Convert.ToDecimal(txtDespatchQty.Text.Trim())));
                        }
                        else
                        {
                            dr["REMAININGQTY"] = "0";
                        }

                        dr["RATE"] = Convert.ToString(txtRate.Text.Trim());
                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", Amount));
                        dr["DISCOUNTPER"] = Convert.ToString(String.Format("{0:0.00}", DiscountPer));
                        dr["DISCOUNTAMT"] = Convert.ToString(String.Format("{0:0.00}", DiscountAmt));

                        dr["AFTERDISCOUNTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterDiscountAmt));
                        dr["ITEMWISEFREIGHT"] = Convert.ToString(String.Format("{0:0.00}", txtitemwiseFreight.Text));
                        decimal AfterItemWiseFreightAmt = (Convert.ToDecimal(txtitemwiseFreight.Text) + AfterDiscountAmt);
                        dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseFreightAmt));

                        dr["ITEMWISEADDCOST"] = Convert.ToString(String.Format("{0:0.00}", txtitemwiseAddCost.Text));
                        decimal AfterItemWiseAddCostAmt = (Convert.ToDecimal(txtitemwiseAddCost.Text) + AfterItemWiseFreightAmt);
                        dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseAddCostAmt));

                        dr["TOTMRP"] = Convert.ToString(String.Format("{0:0.00}", TotMRP));
                        dr["QCREJECTQTY"] = "0";
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(txtAssementPercentage.Text.Trim());
                        dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", TotAssesment));

                        excise = 0;

                        #region Loop For Adding Itemwise Tax Component
                        DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                            {
                                case "1":
                                    TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());



                                    ProductWiseTax = Clsdespatch.GetHSNTax(TAXID, Convert.ToString(this.ddlproduct.SelectedValue.ToString().Substring(0, this.ddlproduct.SelectedValue.ToString().IndexOf("~"))).Trim(), ddlTPU.SelectedValue.ToString().Trim(), txtInvoiceDate.Text.Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", AfterItemWiseAddCostAmt * ProductWiseTax / 100));
                                    break;
                            }
                            Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            CreateTaxDatatable(Convert.ToString(this.ddlpo.SelectedValue.Trim()),
                                               Convert.ToString(this.lblproductid.Text.Trim()),
                                               BATCHNO,
                                               dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                               Convert.ToString(ProductWiseTax),
                                               dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString(),
                                               Convert.ToString(this.lblproductname.Text).Trim(), TAXID);
                        }
                        //}
                        #endregion

                        if (dsweight.Tables[0].Rows.Count > 0)
                        {
                            dr["NETWEIGHT"] = Convert.ToString(dsweight.Tables[0].Rows[0]["NETWEIGHT"]);
                        }
                        else
                        {
                            dr["NETWEIGHT"] = "0";
                        }
                        dr["MFDATE"] = Convert.ToString(txtMfDate.Text.Trim());
                        dr["EXPRDATE"] = Convert.ToString(txtExprDate.Text.Trim());
                        if (dsweight.Tables[1].Rows.Count > 0)
                        {
                            dr["GROSSWEIGHT"] = Convert.ToString(dsweight.Tables[1].Rows[0]["GROSSWEIGHT"]);
                        }
                        else
                        {
                            dr["GROSSWEIGHT"] = "0";
                        }
                        decimal DepotRate = clsgrnmm.BindDepotRate(Convert.ToString(this.lblproductid.Text.Trim().Trim()), Convert.ToDecimal(txtMRP.Text.Trim()), this.txtInvoiceDate.Text.Trim());
                        dr["DEPOTRATE"] = Convert.ToString(DepotRate);
                        dr["ITEMDES"] = Convert.ToString(this.Textitemdescription.Text);
                        dtDespatch.Rows.Add(dr);
                        dtDespatch.AcceptChanges();

                        TotalMRP = CalculateTotalMRP(dtDespatch);
                        TotalAmount = CalculateGrossTotal(dtDespatch);
                        TotalTax = CalculateTaxTotal(dtDespatch);
                        GrossTotal = TotalAmount + TotalTax;
                        ItemWiseFreight = CalculateTotalFreight(dtDespatch);
                        ItemWiseAddCost = CalculateTotalAddCost(dtDespatch);
                        ItemWiseDiscAmt = CalculateTotalDiscount(dtDespatch);

                        this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                        this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                        this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
                        this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
                        this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));

                        #region Tax On Gross Amount
                        /*DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
                        if (dtGrossTax.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtGrossTax.Rows.Count; i++)
                            {
                                dtGrossTax.Rows[i]["TAXVALUE"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dtGrossTax.Rows[i]["PERCENTAGE"]) * Convert.ToDecimal(GrossTotal) / Convert.ToDecimal(100)));
                            }

                            dtGrossTax.AcceptChanges();

                        }
                        this.grdTax.DataSource = dtGrossTax;
                        this.grdTax.DataBind();*/
                        #endregion

                        //GrossFinal = CalculateFinalGrossTotal(dtGrossTax);

                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
                        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
                        this.ResetControls();
                    }
                //}
                DataTable dtProductTax = new DataTable();
                if (Session["TAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                MessageBox1.ShowInfo("Received Qty should be greater than 0", 80, 500);
            }
            if (dtDespatch.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtDespatch;
                this.grdAddDespatch.DataBind();
                this.GridCalculation();
                DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTaxCount"];
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
            }
        }
        #endregion
    }
    #endregion

    #region number checking
    public bool VerifyNumericValue(string ValueToCheck)
    {
        Int32 numval;
        bool rslt = false;

        rslt = Int32.TryParse(ValueToCheck, out numval);

        return rslt;
    }
    #endregion

    #region BREAKAGE/DAMAGE REJECTION

    #region LoadReason
    public void LoadReason()
    {
        ddlrejectionreason.Items.Clear();
        ddlrejectionreason.Items.Add(new ListItem("-- SELECT REJECTION REASON NAME --", "0"));
        ddlrejectionreason.AppendDataBoundItems = true;
        ddlrejectionreason.DataSource = ClsCommon.BindReason(Request.QueryString["MENUID"].ToString());
        ddlrejectionreason.DataValueField = "ID";
        ddlrejectionreason.DataTextField = "DESCRIPTION";
        ddlrejectionreason.DataBind();
    }
    #endregion

    #region btnRejectionReason
    public void btnRejectionReason(object sender, EventArgs e)
    {
        try
        {
            var RejectionLink = (Control)sender;
            GridViewRow row = (GridViewRow)RejectionLink.NamingContainer;
            decimal RemainingQty = Convert.ToDecimal(row.Cells[13].Text.Trim());
            ViewState["RemainingQty"] = Convert.ToString(RemainingQty);
            if (RemainingQty == 0)
            {
                MessageBox1.ShowInfo("Qty not available for Rejection as all Despatched qty has been Received", 60, 600);
                return;
            }

            else if (RemainingQty > 0)
            {

                if (Session["TOTALREJECTIONDETAILS"] == null)
                {
                    this.CreateRejectionTotalDataTable();
                }
                this.CreateRejectionInnerGridDataTable();

                /*Added By Avishek Ghosh On 13.11.2017*/
                if (Session["REJECTIONTAXCOMPONENTDETAILS"] == null)
                {
                    this.CreateDataTableRejectionTaxComponent();
                }

                this.hdn_guid.Value = "";
                this.hdnpoid.Value = "";
                this.hdnproductid.Value = "";
                this.hdnMfDate.Value = "";
                this.hdnExprDate.Value = "";

                dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                int rowIndex = ((sender as ImageButton).NamingContainer as GridViewRow).RowIndex;
                string guid = grdAddDespatch.DataKeys[rowIndex].Values[0].ToString().Trim();
                this.hdnGRNID.Value = grdAddDespatch.DataKeys[rowIndex].Values[0].ToString().Trim();
                string productID = grdAddDespatch.DataKeys[rowIndex].Values[1].ToString().Trim();
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
                                this.txtproductnameonrejection.Text = dtDespatchEdit.Rows[i]["PRODUCTNAME"].ToString();
                                this.txtproductbatchnoonrejection.Text = dtDespatchEdit.Rows[i]["BATCHNO"].ToString();
                                this.ddlrejectionpacksize.SelectedValue = dtDespatchEdit.Rows[i]["PACKINGSIZEID"].ToString();
                                this.hdnMfDate.Value = dtDespatchEdit.Rows[i]["MFDATE"].ToString();
                                this.hdnExprDate.Value = dtDespatchEdit.Rows[i]["EXPRDATE"].ToString();
                                //this.hdnAFTERDISCOUNTAMT.Value = dtDespatchEdit.Rows[i]["AFTERDISCOUNTAMT"].ToString();
                                //this.hdnAFTERDISCOUNTAMT.Value = dtDespatchEdit.Rows[i]["AFTERITEMWISEFREIGHTAMT"].ToString();
                                this.hdnAFTERDISCOUNTAMT.Value = dtDespatchEdit.Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString();
                                this.hdnRCVDQTY.Value = dtDespatchEdit.Rows[i]["RECEIVEDQTY"].ToString();
                                break;
                            }
                        }
                    }
                }

                this.LoadRejectionProductWisePacksize(productID, Request.QueryString["FG"].ToString().Trim());

                this.LoadReason();

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
                        if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                        {
                            if (dtrejectiontotal.Rows[i]["POID"].ToString() == hdnpoid.Value.ToString() && dtrejectiontotal.Rows[i]["PRODUCTID"].ToString() == hdnproductid.Value.ToString() && dtrejectiontotal.Rows[i]["BATCHNO"].ToString() == txtproductbatchnoonrejection.Text.Trim())
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
                        else
                        {
                            if (dtrejectiontotal.Rows[i]["POID"].ToString() == hdnpoid.Value.ToString() && dtrejectiontotal.Rows[i]["PRODUCTID"].ToString() == hdnproductid.Value.ToString())
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
                    }
                    Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;
                    this.gvrejectiondetails.DataSource = dtinnergridrejection;
                    this.gvrejectiondetails.DataBind();
                }
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
            string Rate = string.Empty;
            /*Added By Avishek Ghosh On 13.11.2017*/
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            string TaxAmount = string.Empty;
            if (Session["REJECTIONDINNERGRIDDETAILS"] == null)
            {
                this.CreateRejectionInnerGridDataTable();
            }
            int flag = 0;
            DataTable dtinnergridrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            DataTable dtinnergridTaxrejection = (DataTable)Session["REJECTIONTAXCOMPONENTDETAILS"];

            if (dtinnergridrejection.Rows.Count > 0)
            {
                for (int i = 0; i < dtinnergridrejection.Rows.Count; i++)
                {
                    if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                    {
                        int NumberofRecord = dtinnergridrejection.Select("POID='" + hdnpoid.Value.ToString() + "' AND PRODUCTID='" + hdnproductid.Value.ToString() + "' AND BATCHNO='" + txtproductbatchnoonrejection.Text.Trim() + "' AND REASONID='" + ddlrejectionreason.SelectedValue + "' AND REASONNAME='" + ddlrejectionreason.SelectedItem.Text.ToString() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            MessageBox1.ShowInfo("<b>This reason already exist with this product!</b>", 60, 400);
                            flag = 1;
                            break;
                        }
                    }
                    else
                    {
                        int NumberofRecord = dtinnergridrejection.Select("POID='" + hdnpoid.Value.ToString() + "' AND PRODUCTID='" + hdnproductid.Value.ToString() + "' AND REASONID='" + ddlrejectionreason.SelectedValue + "' AND REASONNAME='" + ddlrejectionreason.SelectedItem.Text.ToString() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            MessageBox1.ShowInfo("<b>This reason already exist with this product!</b>", 60, 400);
                            flag = 1;
                            break;
                        }
                    }
                }
            }

            if (flag == 0)
            {
                ClsGRNMM clsGRN = new ClsGRNMM();
                decimal DepotRate = 0;
                decimal DepotRate1 = 0;
                DataTable dtStoreLocation = new DataTable();
                DataTable dtProductdetails = new DataTable();

                dtStoreLocation = clsGRN.StoreLocationDetails(this.ddlrejectionreason.SelectedValue);
                dtProductdetails = clsGRN.BindPRoductDetails(hdnproductid.Value.Trim());

                if (dtProductdetails.Rows.Count > 0)
                {
                    mfDate = this.hdnMfDate.Value.Trim();
                    exprDate = this.hdnExprDate.Value.Trim();
                    assesment = Convert.ToString(dtProductdetails.Rows[0]["ASSESSABLEPERCENT"]).Trim();
                    mrp = Convert.ToString(dtProductdetails.Rows[0]["MRP"]).Trim();
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

                string DebitedTo = clsGRN.DebitedTo(this.ddlrejectionreason.SelectedValue.Trim());
                if (DebitedTo.Trim() == "0")
                {
                    DepotRate = 0;
                }
                else if (DebitedTo.Trim() == "1")
                {
                    DepotRate = clsGRN.BindDepotRate(hdnproductid.Value.Trim(), Convert.ToDecimal(mrp), this.txtInvoiceDate.Text.Trim());
                }
                else
                {
                    DepotRate = clsGRN.BindTPURate(hdnproductid.Value.Trim(), this.ddlTPU.SelectedValue.Trim(), this.txtDespatchDate.Text.Trim());
                }

                DepotRate1 = clsGRN.BindDepotRate(hdnproductid.Value.Trim(), Convert.ToDecimal(mrp), this.txtInvoiceDate.Text.Trim());
                string REJECTIONBATCHNO = string.Empty;
                if (this.txtproductbatchnoonrejection.Text.Trim() == "")
                {
                    REJECTIONBATCHNO = "NA";
                }
                else
                {
                    REJECTIONBATCHNO = this.txtproductbatchnoonrejection.Text.Trim();
                }

                DataRow dr = dtinnergridrejection.NewRow();
                dr["GUID"] = this.hdnGRNID.Value.Trim();
                dr["STOCKRECEIVEDID"] = this.hdnDespatchID.Value.ToString().Trim();
                dr["POID"] = hdnpoid.Value.ToString();
                dr["STOCKDESPATCHID"] = this.hdnReceivedID.Value.ToString().Trim();
                dr["PRODUCTID"] = this.hdnproductid.Value.ToString();
                dr["PRODUCTNAME"] = this.txtproductnameonrejection.Text.Trim();
                dr["BATCHNO"] = REJECTIONBATCHNO;
                dr["REJECTIONQTY"] = Convert.ToString(this.txtrejectionqty.Text.Trim());
                dr["PACKINGSIZEID"] = this.ddlrejectionpacksize.SelectedValue.ToString().Trim();
                dr["PACKINGSIZENAME"] = this.ddlrejectionpacksize.SelectedItem.Text;
                dr["REASONID"] = this.ddlrejectionreason.SelectedValue.ToString();
                dr["REASONNAME"] = this.ddlrejectionreason.SelectedItem.Text;
                dr["DEPOTRATE"] = Convert.ToString(DepotRate);
                dr["DEPOTRATE1"] = Convert.ToString(DepotRate1);
                decimal convertionqty = 0;

                if (this.ddlrejectionpacksize.SelectedValue.ToString().Trim() == "B9F29D12-DE94-40F1-A668-C79BF1BF4425")
                {
                    convertionqty = clsGRN.ConvertionRejectionQty(this.hdnproductid.Value.ToString(), this.ddlrejectionpacksize.SelectedValue.ToString(),
                                                                  Convert.ToDecimal(this.txtrejectionqty.Text.Trim()));
                }
                else
                {
                    convertionqty = clsgrnmm.CalculateNonFGRejectionQty(this.hdnproductid.Value.ToString().Trim(), Convert.ToDecimal(this.txtrejectionqty.Text.Trim()),
                                                                        this.ddlrejectionpacksize.SelectedValue.ToString().Trim());
                }

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
                dr["MFDATE"] = mfDate;
                dr["EXPRDATE"] = exprDate;
                dr["ASSESMENTPERCENTAGE"] = assesment;
                dr["MRP"] = mrp;
                dr["WEIGHT"] = weight;

                dtinnergridrejection.Rows.Add(dr);
                dtinnergridrejection.AcceptChanges();

                Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;

                #region Fetch Discount Details
                /*DataTable disc = new DataTable();*/
                decimal AfterDiscountAmt = 0;
                /*disc = clsgrnmm.BindVendorDisc(this.ddlTPU.SelectedValue.Trim(), Convert.ToString(this.hdnproductid.Value).Trim(), txtInvoiceDate.Text, "");
                decimal DiscountPer = Convert.ToDecimal(disc.Rows[0]["DISCOUNTPER"].ToString());
                decimal DiscountAmt = Convert.ToDecimal(disc.Rows[0]["DISCOUNTAMT"].ToString());*/
                /*if (DiscountPer == 0)
                {*/
                /*AfterDiscountAmt = (Convert.ToDecimal(this.hdnAFTERDISCOUNTAMT.Value.Trim()) / Convert.ToDecimal(this.hdnRCVDQTY.Value.Trim())) * Convert.ToDecimal(this.txtrejectionqty.Text.Trim());*/
                AfterDiscountAmt = Convert.ToDecimal(dr["AMOUNT"].ToString().Trim());
                /*}
                else
                {
                    AfterDiscountAmt = Convert.ToDecimal(this.hdnAFTERDISCOUNTAMT.Value.Trim()) - (Convert.ToDecimal(this.hdnAFTERDISCOUNTAMT.Value.Trim()) * DiscountPer / 100);
                }*/
                #endregion

                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];

                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                    {
                        case "1":
                            TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            ProductWiseTax = clsgrnmm.GetHSNTax(TAXID, Convert.ToString(this.hdnproductid.Value).Trim(), ddlTPU.SelectedValue.ToString().Trim(), txtInvoiceDate.Text.Trim());
                            TaxAmount = Convert.ToString(String.Format("{0:0.00}", AfterDiscountAmt * ProductWiseTax / 100));
                            break;
                    }
                    RejectionArry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());

                    CreateRejectionTaxDatatable(Convert.ToString(this.hdnpoid.Value.Trim()), /*POID*/
                                                Convert.ToString(this.hdnproductid.Value),/*PRODUCTID*/
                                                REJECTIONBATCHNO,/*BATCHNO*/
                                                dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                                Convert.ToString(ProductWiseTax),/*TAX(%)*/
                                                TaxAmount.Trim(),/*TAX VALUE*/
                                                Convert.ToString(this.txtproductnameonrejection.Text).Trim(),/*PRODUCTNAME*/
                                                TAXID/*TAXID*/
                                                );
                }
                #endregion

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

    #region Rejection Delete
    protected void btnRejectiongrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsGRNMM GrnMM = new ClsGRNMM();
            DataTable dtinnerrejectiongrid = (DataTable)HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"];
            DataTable dtRejectionTax = new DataTable();
            int flag = 0;
            string guid = hdn_guid.Value.ToString().Trim();
            string poid = hdnpoid.Value.ToString().Trim();
            string productid = hdnproductid.Value.ToString().Trim();
            string batch = hdnRejBatch.Value.ToString().Trim();
            flag = GrnMM.RejectionRecordsDelete(guid, dtinnerrejectiongrid);

            if (flag > 0)
            {
                this.gvrejectiondetails.DataSource = dtinnerrejectiongrid;
                this.gvrejectiondetails.DataBind();

                #region Item-wise Tax Deletion
                if (Session["REJECTIONTAXCOMPONENTDETAILS"] != null)
                {
                    dtRejectionTax = (DataTable)HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"];
                }
                DataRow[] drrTax = dtRejectionTax.Select("POID='" + poid + "' AND PRODUCTID = '" + productid + "' AND BATCHNO = '" + batch + "'");
                for (int i = 0; i < drrTax.Length; i++)
                {
                    drrTax[i].Delete();
                    dtRejectionTax.AcceptChanges();
                }
                HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"] = dtRejectionTax;
                #endregion

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

    #region RejectionQty
    protected decimal RejectionQty()
    {
        decimal RejectionQty = 0;
        for (int i = 0; i < gvrejectiondetails.RowsInViewState.Count; i++)
        {
            Hashtable dataRejectionQty = gvrejectiondetails.Rows[i].ToHashtable();
            RejectionQty += Convert.ToDecimal(dataRejectionQty["REJECTIONQTY"].ToString() == null ? "0" : dataRejectionQty["REJECTIONQTY"].ToString());
        }
        return RejectionQty;
    }
    #endregion

    #region btnrejectionsubmit_click
    public void btnrejectionsubmit_click(object sender, EventArgs e)
    {
        try
        {
            // Validation Checking
            decimal RejectionQty = 0;
            //RejectionQty = this.RejectionQty();
            if (RejectionQty > Convert.ToDecimal(ViewState["RemainingQty"].ToString().Trim()))
            {
                MessageBox1.ShowInfo("Rejection qty should not be greater than " + ViewState["RemainingQty"].ToString().Trim() + "", 60, 500);
                this.popup.Show();
            }
            else
            {
                if (Session["TOTALREJECTIONDETAILS"] == null)
                {
                    this.CreateRejectionTotalDataTable();
                }
                DataTable dtinnerrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
                int j = 0;
                int NumberofRecord = 0;

                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    for (int i = 0; i < dtinnerrejection.Rows.Count; i++)
                    {
                        while (j > -1 && j < dtrejectiontotal.Rows.Count)
                        {
                            if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                            {
                                NumberofRecord = dtrejectiontotal.Select("STOCKRECEIVEDID='" + dtinnerrejection.Rows[i]["STOCKRECEIVEDID"] + "' AND STOCKDESPATCHID='" + dtinnerrejection.Rows[i]["STOCKDESPATCHID"] + "' AND POID='" + dtinnerrejection.Rows[i]["POID"] + "' AND PRODUCTID='" + dtinnerrejection.Rows[i]["PRODUCTID"] + "' AND BATCHNO='" + dtinnerrejection.Rows[i]["BATCHNO"] + "'").Length;
                            }
                            else
                            {
                                NumberofRecord = dtrejectiontotal.Select("STOCKRECEIVEDID='" + dtinnerrejection.Rows[i]["STOCKRECEIVEDID"] + "' AND STOCKDESPATCHID='" + dtinnerrejection.Rows[i]["STOCKDESPATCHID"] + "' AND POID='" + dtinnerrejection.Rows[i]["POID"] + "' AND PRODUCTID='" + dtinnerrejection.Rows[i]["PRODUCTID"] + "'").Length;
                            }
                            if (NumberofRecord > 0)
                            {
                                string PID = dtinnerrejection.Rows[i]["PRODUCTID"].ToString();
                                string BATCHNO = dtinnerrejection.Rows[i]["BATCHNO"].ToString();
                                string POID = dtinnerrejection.Rows[i]["POID"].ToString();

                                for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                                {
                                    if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                                    {
                                        if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == POID && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == PID && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == BATCHNO)
                                        {
                                            dtrejectiontotal.Rows[k].Delete();
                                            dtrejectiontotal.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == POID && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == PID)
                                        {
                                            dtrejectiontotal.Rows[k].Delete();
                                            dtrejectiontotal.AcceptChanges();
                                        }
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
                    dr["GUID"] = dtinnerrejection.Rows[i]["GUID"].ToString();
                    dr["STOCKRECEIVEDID"] = dtinnerrejection.Rows[i]["STOCKRECEIVEDID"].ToString();
                    dr["POID"] = dtinnerrejection.Rows[i]["POID"].ToString();
                    dr["STOCKDESPATCHID"] = dtinnerrejection.Rows[i]["STOCKDESPATCHID"].ToString();
                    dr["PRODUCTID"] = dtinnerrejection.Rows[i]["PRODUCTID"].ToString();
                    dr["PRODUCTNAME"] = dtinnerrejection.Rows[i]["PRODUCTNAME"].ToString();
                    dr["BATCHNO"] = dtinnerrejection.Rows[i]["BATCHNO"].ToString();
                    dr["REJECTIONQTY"] = dtinnerrejection.Rows[i]["REJECTIONQTY"].ToString();
                    dr["PACKINGSIZEID"] = dtinnerrejection.Rows[i]["PACKINGSIZEID"].ToString();
                    dr["PACKINGSIZENAME"] = dtinnerrejection.Rows[i]["PACKINGSIZENAME"].ToString();
                    dr["REASONID"] = dtinnerrejection.Rows[i]["REASONID"].ToString();
                    dr["REASONNAME"] = dtinnerrejection.Rows[i]["REASONNAME"].ToString();
                    dr["DEPOTRATE"] = dtinnerrejection.Rows[i]["DEPOTRATE"].ToString();
                    dr["DEPOTRATE1"] = dtinnerrejection.Rows[i]["DEPOTRATE1"].ToString();
                    dr["AMOUNT"] = dtinnerrejection.Rows[i]["AMOUNT"].ToString();
                    dr["STORELOCATIONID"] = dtinnerrejection.Rows[i]["STORELOCATIONID"].ToString();
                    dr["STORELOCATIONNAME"] = dtinnerrejection.Rows[i]["STORELOCATIONNAME"].ToString();
                    dr["MFDATE"] = dtinnerrejection.Rows[i]["MFDATE"].ToString();
                    dr["EXPRDATE"] = dtinnerrejection.Rows[i]["EXPRDATE"].ToString();
                    dr["ASSESMENTPERCENTAGE"] = dtinnerrejection.Rows[i]["ASSESMENTPERCENTAGE"].ToString();
                    dr["MRP"] = dtinnerrejection.Rows[i]["MRP"].ToString();
                    dr["WEIGHT"] = dtinnerrejection.Rows[i]["WEIGHT"].ToString();


                    dtrejectiontotal.Rows.Add(dr);
                    dtrejectiontotal.AcceptChanges();
                }

                if (dtinnerrejection.Rows.Count == 0)
                {
                    if (dtrejectiontotal.Rows.Count > 0)
                    {
                        while (j > -1 && j < dtrejectiontotal.Rows.Count)
                        {
                            if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                            {
                                NumberofRecord = dtrejectiontotal.Select("POID='" + hdnpoid.Value.ToString() + "' AND PRODUCTID='" + hdnproductid.Value.ToString() + "' AND BATCHNO='" + txtproductbatchnoonrejection.Text.Trim() + "'").Length;
                            }
                            else
                            {
                                NumberofRecord = dtrejectiontotal.Select("POID='" + hdnpoid.Value.ToString() + "' AND PRODUCTID='" + hdnproductid.Value.ToString() + "'").Length;
                            }
                            if (NumberofRecord > 0)
                            {
                                for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                                {
                                    if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
                                    {
                                        if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == hdnpoid.Value.ToString() && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == hdnproductid.Value.ToString() && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == txtproductbatchnoonrejection.Text.Trim())
                                        {
                                            dtrejectiontotal.Rows[k].Delete();
                                            dtrejectiontotal.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (dtrejectiontotal.Rows[k]["POID"].ToString().Trim() == hdnpoid.Value.ToString() && dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == hdnproductid.Value.ToString())
                                        {
                                            dtrejectiontotal.Rows[k].Delete();
                                            dtrejectiontotal.AcceptChanges();
                                        }
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

                DataTable dtDespatch = (DataTable)Session["DESPATCHDETAILS"];
                if (dtDespatch.Rows.Count > 0)
                {
                    this.grdAddDespatch.DataSource = dtDespatch;
                    this.grdAddDespatch.DataBind();
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

    #endregion

    #region grdDespatchHeader_RowDataBound
    protected void grdDespatchHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[16] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();

                if (status == "PENDING")
                {
                    cell.ForeColor = Color.Blue;
                }
                else if (status == "APPROVED")
                {
                    cell.ForeColor = Color.Green;
                }
                else if (status == "REJECTED")
                {
                    cell.ForeColor = Color.DeepPink;
                }
                else if (status == "HOLD")
                {
                    cell.ForeColor = Color.Black;
                }
                else if (status == "CANCEL")
                {
                    cell.ForeColor = Color.Red;
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

    #region grdAddDespatch_OnRowDataBound
    protected void grdAddDespatch_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["OP"].ToString() == "MAKER")
            {
                e.Row.Controls[1].Visible = false;
            }
            else
            {
                e.Row.Controls[1].Visible = true;
            }

            DataTable dt = (DataTable)Session["dtTaxCount"];
            e.Row.Controls[2].Visible = false;
            e.Row.Controls[3].Visible = false;
            e.Row.Controls[6].Visible = true;
            e.Row.Controls[8].Visible = false;
            e.Row.Controls[10].Visible = false;
            e.Row.Controls[12].Visible = false;
            e.Row.Controls[13].Visible = false;
            e.Row.Controls[18].Visible = false;
            e.Row.Controls[19].Visible = false;
            e.Row.Controls[20].Visible = false;
            e.Row.Controls[25].Visible = false;
            e.Row.Controls[27].Visible = false;
            e.Row.Controls[28].Visible = false;
            e.Row.Controls[29].Visible = false;
            if (dt.Rows.Count == 2)
            {
                e.Row.Controls[34].Visible = false;
                e.Row.Controls[35].Visible = false;
                e.Row.Controls[36].Visible = false;
                e.Row.Controls[37].Visible = false;
            }
            else if(dt.Rows.Count == 1)
            {
                e.Row.Controls[32].Visible = false;
                e.Row.Controls[33].Visible = false;
                e.Row.Controls[34].Visible = false;
                e.Row.Controls[35].Visible = false;
            }
            


            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            e.Row.Cells[8].Wrap = false;
            e.Row.Cells[9].Wrap = false;
            e.Row.Cells[10].Wrap = false;
            e.Row.Cells[11].Wrap = false;
            e.Row.Cells[12].Wrap = false;
            e.Row.Cells[13].Wrap = false;
            e.Row.Cells[14].Wrap = false;
            e.Row.Cells[15].Wrap = false;
            e.Row.Cells[16].Wrap = false;
            e.Row.Cells[17].Wrap = false;
            e.Row.Cells[18].Wrap = false;
            e.Row.Cells[19].Wrap = false;
            e.Row.Cells[20].Wrap = false;
            e.Row.Cells[21].Wrap = false;
            e.Row.Cells[22].Wrap = false;
            e.Row.Cells[23].Wrap = false;
            e.Row.Cells[24].Wrap = false;
            e.Row.Cells[25].Wrap = false;
            e.Row.Cells[26].Wrap = false;
            e.Row.Cells[27].Wrap = false;

            int count = 27;
            
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                e.Row.Cells[count].Wrap = false;
            }
           
            TotalGridAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
            AfterDiscAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERDISCOUNTAMT"));
            AfterItemWiseFreightAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERITEMWISEFREIGHTAMT"));
            AfterItemWiseAddCostAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERITEMWISEADDCOSTAMT"));
            TtoalFreight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ITEMWISEFREIGHT"));
            this.txtFREIGHTAMT.Text =Convert.ToString(TtoalFreight);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            int TotalRows = grdAddDespatch.Rows.Count;
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTaxCount"];
            for (int i = 31; i <= (31 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    sum += grdAddDespatch.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdAddDespatch.Rows[j].Cells[i].Text) : 0.00;
                }
                e.Row.Cells[12].Text = "Total : ";
                e.Row.Cells[12].ForeColor = Color.Blue;
                e.Row.Cells[12].Font.Bold = true;
                e.Row.Cells[i - 1].Text = sum.ToString("#.00");
                e.Row.Cells[i - 1].Font.Bold = true;
                e.Row.Cells[i - 1].ForeColor = Color.Blue;
                e.Row.Cells[i - 1].Wrap = false;
                e.Row.Cells[i - 1].HorizontalAlign = HorizontalAlign.Left;
            }
            if (TotalGridAmount == 0)
            {
                e.Row.Cells[17].Text = "0.00";
                //e.Row.Cells[18].Text = "0.00";
                e.Row.Cells[20].Text = "0.00";
                e.Row.Cells[22].Text = "0.00";
                e.Row.Cells[25].Text = "0.00";
            }
            else
            {
                e.Row.Cells[17].Text = TotalGridAmount.ToString("#.00");
                e.Row.Cells[20].Text = AfterDiscAmt.ToString("#.00");
                e.Row.Cells[22].Text = AfterItemWiseFreightAmt.ToString("#.00");
                e.Row.Cells[25].Text = AfterItemWiseAddCostAmt.ToString("#.00");
            }
            e.Row.Cells[17].Font.Bold = true;
            e.Row.Cells[17].ForeColor = Color.Blue;
            e.Row.Cells[17].Wrap = false;
            e.Row.Cells[20].Font.Bold = true;
            e.Row.Cells[20].ForeColor = Color.Blue;
            e.Row.Cells[20].Wrap = false;
            e.Row.Cells[22].Font.Bold = true;
            e.Row.Cells[22].ForeColor = Color.Blue;
            e.Row.Cells[22].Wrap = false;
            e.Row.Cells[25].Font.Bold = true;
            e.Row.Cells[25].ForeColor = Color.Blue;
            e.Row.Cells[25].Wrap = false;

            if (ViewState["OP"].ToString() == "MAKER")
            {
                e.Row.Controls[1].Visible = false;
            }
            else
            {
                e.Row.Controls[1].Visible = true;
            }
            e.Row.Controls[2].Visible = false;
            e.Row.Controls[3].Visible = false;
            e.Row.Controls[6].Visible = false;
            e.Row.Controls[8].Visible = false;
            e.Row.Controls[10].Visible = false;
            e.Row.Controls[11].Visible = false;
            e.Row.Controls[18].Visible = false;
            e.Row.Controls[19].Visible = false;
            e.Row.Controls[20].Visible = false;
            e.Row.Controls[23].Visible = false;
            ///e.Row.Controls[16].Visible = false;
            e.Row.Controls[26].Visible = false;
            e.Row.Controls[27].Visible = false;

            if (dtTaxCountDataAddition1.Rows.Count == 2)
            {
                e.Row.Controls[34].Visible = false;
                e.Row.Controls[35].Visible = false;
                e.Row.Controls[36].Visible = false;
                e.Row.Controls[37].Visible = false;
            }
            else if (dtTaxCountDataAddition1.Rows.Count == 1)
            {
                e.Row.Controls[32].Visible = false;
                e.Row.Controls[33].Visible = false;
                e.Row.Controls[34].Visible = false;
                e.Row.Controls[35].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["TOTALREJECTIONDETAILS"] != null)
            {
                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    int NumberofRecord = 0;
                    NumberofRecord = dtrejectiontotal.Select("PRODUCTID='" + e.Row.Cells[5].Text.Trim() + "'").Length;
                    Label lblrejectionqty = (e.Row.FindControl("lblrejvalue") as Label);
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
        }
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
            e.Row.Cells[3].Text = "Total : ";
            e.Row.Cells[3].ForeColor = Color.Blue;
            e.Row.Cells[3].Font.Bold = true;

            e.Row.Cells[4].Text = TotalTaxValue.ToString("#.00");
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[4].ForeColor = Color.Blue;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
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
            decimal ItemWiseFreight = 0, ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            this.hdndtDespatchDelete.Value = gvr.Cells[2].Text.Trim();
            this.hdndtPOIDDelete.Value = gvr.Cells[3].Text.Trim();
            this.hdndtPRODUCTIDDelete.Value = gvr.Cells[8].Text.Trim();
            this.hdndtBATCHDelete.Value = gvr.Cells[26].Text.Trim();
            this.hdnRecvdQty.Value = gvr.Cells[13].Text.Trim();

            string despatchGUID = Convert.ToString(hdndtDespatchDelete.Value);
            string despatchPOID = Convert.ToString(hdndtPOIDDelete.Value);
            string despatchPRODUCTID = Convert.ToString(hdndtPRODUCTIDDelete.Value);
            string despatchBATCH = Convert.ToString(hdndtBATCHDelete.Value);
            string despatchRecvdQty = Convert.ToString(hdnRecvdQty.Value);

            this.RbApplicable.SelectedIndex = 1;
            this.RbApplicable.SelectedValue = "N";
            this.RbApplicable.Items[1].Selected = true;
            this.divtcs.Visible = false;
            this.txtTCSPercent.Text = "0";
            this.txtTCS.Text = "0";
            this.txtTCSNetAmt.Text = "0";
            this.txtTCSApplicable.Text = "0";
            this.txtTCSApplicable.Enabled = false;

            DataTable dtdeleteDespatchtrecord = new DataTable();
            dtdeleteDespatchtrecord = (DataTable)Session["DESPATCHDETAILS"];
            if (despatchBATCH == "&nbsp;")
            {
                despatchBATCH = "";
            }
            DataRow[] drr = dtdeleteDespatchtrecord.Select("GUID='" + despatchGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteDespatchtrecord.AcceptChanges();
            }
            HttpContext.Current.Session["DESPATCHDETAILS"] = dtdeleteDespatchtrecord;
            if (dtdeleteDespatchtrecord.Rows.Count > 0)
            {
                grdAddDespatch.DataSource = dtdeleteDespatchtrecord;
                grdAddDespatch.DataBind();
                this.ddlGatePassNo.Enabled = false;
                this.ddlVendorFrom.Enabled = false;
                this.ddlTPU.Enabled= false;
            }
            else
            {
                grdAddDespatch.DataSource = null;
                grdAddDespatch.DataBind();
                this.ddlGatePassNo.Enabled = true;
                this.ddlVendorFrom.Enabled = true;
                this.ddlTPU.Enabled = true;
            }

            #region Item-wise Tax Deletion
            DataTable dtdeleteDespatchtItemTax = new DataTable();
            dtdeleteDespatchtItemTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
            DataRow[] drrTax = dtdeleteDespatchtItemTax.Select("POID='" + despatchPOID + "' AND PRODUCTID = '" + despatchPRODUCTID + "' AND BATCHNO = '" + despatchBATCH + "'");
            for (int i = 0; i < drrTax.Length; i++)
            {
                drrTax[i].Delete();
                dtdeleteDespatchtItemTax.AcceptChanges();
            }
            HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dtdeleteDespatchtItemTax;
            if (dtdeleteDespatchtItemTax.Rows.Count > 0)
            {
                gvProductTax.DataSource = dtdeleteDespatchtItemTax;
                gvProductTax.DataBind();
            }
            else
            {
                gvProductTax.DataSource = null;
                gvProductTax.DataBind();
            }

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition1.Rows.Count; k++)
            {
                //if (Convert.ToDecimal(dtTaxCountDataAddition1.Rows[k]["PERCENTAGE"].ToString()) > 0)
                //{
                Arry.Add(dtTaxCountDataAddition1.Rows[k]["NAME"].ToString());
                //}
            }
            #endregion

            TotalMRP = CalculateTotalMRP(dtdeleteDespatchtrecord);
            TotalAmount = CalculateGrossTotal(dtdeleteDespatchtrecord);
            TotalTax = CalculateTaxTotal(dtdeleteDespatchtrecord);
            GrossTotal = TotalAmount + TotalTax;
            ItemWiseFreight = CalculateTotalFreight(dtdeleteDespatchtrecord);
            ItemWiseAddCost = CalculateTotalAddCost(dtdeleteDespatchtrecord);
            ItemWiseDiscAmt = CalculateTotalDiscount(dtdeleteDespatchtrecord);

            this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
            this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
            this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
            this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
            this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
            this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));
            #endregion

            #region Gross Tax

            /* DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
            if (dtGrossTax.Rows.Count > 0)
            {
                for (int i = 0; i < dtGrossTax.Rows.Count; i++)
                {
                    dtGrossTax.Rows[i]["TAXVALUE"] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dtGrossTax.Rows[i]["PERCENTAGE"]) * Convert.ToDecimal(GrossTotal) / Convert.ToDecimal(100)));
                }
                dtGrossTax.AcceptChanges();
            }
            this.grdTax.DataSource = dtGrossTax;
            this.grdTax.DataBind();

            GrossFinal = CalculateFinalGrossTotal(dtGrossTax);*/

            this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal + GrossFinal));
            this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
            this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
            #endregion

            #region Job Order Delete
            DataTable dtdeleteJobOrder = new DataTable();
            dtdeleteJobOrder = (DataTable)Session["JOBORDER_RECEIVE"];
            DataRow[] drrJobOrder = dtdeleteJobOrder.Select("POID='" + despatchPOID + "' AND WORKORDERPRODUCTID = '" + despatchPRODUCTID + "' ");
            for (int i = 0; i < drrJobOrder.Length; i++)
            {
                drrJobOrder[i].Delete();
                dtdeleteJobOrder.AcceptChanges();
            }
            HttpContext.Current.Session["JOBORDER_RECEIVE"] = dtdeleteJobOrder;
            hdnJobOrderReceived.Value = "0";
            #endregion

            int flag = 0;
            flag = clsgrnmm.DeleteJobOrderTemp(despatchPOID, despatchPRODUCTID, Convert.ToDecimal(despatchRecvdQty));

            DataTable dtDeleteInnerRejection = new DataTable();
            DataTable dtDeleteTotalRejection = new DataTable();
            //if (Session["REJECTIONDINNERGRIDDETAILS"]==null)
            dtDeleteInnerRejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            dtDeleteTotalRejection = (DataTable)Session["TOTALREJECTIONDETAILS"];
            if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
            {
                DataRow[] drrInnerRejection = dtDeleteInnerRejection.Select("PRODUCTID='" + despatchPRODUCTID + "' AND BATCHNO='" + despatchBATCH + "'");
                for (int i = 0; i < drrInnerRejection.Length; i++)
                {
                    drrInnerRejection[i].Delete();
                    dtDeleteInnerRejection.AcceptChanges();
                }
                HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dtDeleteInnerRejection;
            }
            else
            {
                DataRow[] drrInnerRejection = dtDeleteInnerRejection.Select("PRODUCTID='" + despatchPRODUCTID + "'");
                for (int i = 0; i < drrInnerRejection.Length; i++)
                {
                    drrInnerRejection[i].Delete();
                    dtDeleteInnerRejection.AcceptChanges();
                }
                HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dtDeleteInnerRejection;
            }

            if (Request.QueryString["FG"].ToString().Trim() == "TRUE")
            {
                DataRow[] drrTotalRejection = dtDeleteTotalRejection.Select("PRODUCTID='" + despatchPRODUCTID + "' AND BATCHNO='" + despatchBATCH + "'");
                for (int i = 0; i < drrTotalRejection.Length; i++)
                {
                    drrTotalRejection[i].Delete();
                    dtDeleteTotalRejection.AcceptChanges();
                }
                HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = dtDeleteTotalRejection;
            }
            else
            {
                DataRow[] drrTotalRejection = dtDeleteTotalRejection.Select("PRODUCTID='" + despatchPRODUCTID + "'");
                for (int i = 0; i < drrTotalRejection.Length; i++)
                {
                    drrTotalRejection[i].Delete();
                    dtDeleteTotalRejection.AcceptChanges();
                }
                HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = dtDeleteTotalRejection;
            }

            DataTable dtProductTax = new DataTable();
            if (Session["TAXCOMPONENTDETAILS"] != null)
            {
                dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string PRODUCTNAME, string Taxid)
    {
        //string TaxID = string.Empty;
        DataTable dt = (DataTable)Session["TAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["POID"] = POID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        //TaxID = clsgrnmm.TaxID(NAME);
        dr["TAXID"] = Taxid;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["PRODUCTNAME"] = PRODUCTNAME;
        dr["TAXNAME"] = NAME;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    /*Added By Avishek Ghosh On 13.11.2017*/
    #region Create Rejection Tax Datatable
    void CreateRejectionTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string PRODUCTNAME, string Taxid)
    {
        DataTable dt = (DataTable)Session["REJECTIONTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["POID"] = POID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = Taxid;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["PRODUCTNAME"] = PRODUCTNAME;
        dr["TAXNAME"] = NAME;
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
            //GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);
            //GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AFTERDISCOUNTAMT"]);
            //GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AFTERITEMWISEFREIGHTAMT"]);
            GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AFTERITEMWISEADDCOSTAMT"]);
        }
        return GrossTotal;
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

    #region CalculateTotalFreight
    decimal CalculateTotalFreight(DataTable dt)
    {
        decimal TotalFreight = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            TotalFreight += Convert.ToDecimal(dt.Rows[Counter]["ITEMWISEFREIGHT"]);
        }
        return TotalFreight;
    }
    #endregion

    #region CalculateTotalAddCost
    decimal CalculateTotalAddCost(DataTable dt)
    {
        decimal TotalAddCost = 0;
        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            TotalAddCost += Convert.ToDecimal(dt.Rows[Counter]["ITEMWISEADDCOST"]);
        }
        return TotalAddCost;
    }
    #endregion

    #region CalculateTotalDiscount
    decimal CalculateTotalDiscount(DataTable dt)
    {
        decimal TotalDiscount = 0;
        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            TotalDiscount += Convert.ToDecimal(dt.Rows[Counter]["DISCOUNTAMT"]);
        }
        return TotalDiscount;
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

    #region ddlWaybillFilter_SelectedIndexChanged
    protected void ddlWaybillFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWaybillFilter.SelectedValue != "0")
        {
            grdDespatchHeader.DataSource = clsgrnmm.BindDespatchWaybillFilter(this.ddlWaybillFilter.SelectedValue, Convert.ToString(HttpContext.Current.Session["FINYEAR"]), this.ddlTPU.SelectedValue.Trim());
            grdDespatchHeader.DataBind();
        }
    }
    #endregion

    #region ddlproduct_SelectedIndexChanged
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] sSplit = ddlproduct.SelectedValue.ToString().Split('~');
            string UNITID = sSplit[1];
            lblproductid.Text = sSplit[0];

            Cache["ISSERVICE"]=isProductType(lblproductid.Text);
            

            if (this.ddlVendorFrom.SelectedValue != "C")
            {
                LoadPurchaseOrderDetails();

                if (ddlpo.SelectedValue != "0" && Cache["ISWORKORDER"].ToString() != "Y")
                {
                    //this.ddlPackSize.SelectedItem.Text = ddlproduct.SelectedItem.Text.Substring(35, 14);
                    lblproductname.Text = ddlproduct.SelectedItem.Text.Substring(0, 60).Trim();
                    this.txtPoQty.Text = ddlproduct.SelectedItem.Text.Substring(74, 14).Trim();
                    this.txtAlreadyDespatchQty.Text = ddlproduct.SelectedItem.Text.Substring(88, 14).Trim();
                    //this.txtDespatchQty.Text = ddlproduct.SelectedItem.Text.Substring(88, 14).Trim();
                    this.txtQcQty.Text = ddlproduct.SelectedItem.Text.Substring(102, 14).Trim();//Remaining Qty
                    this.txtMRP.Text = ddlproduct.SelectedItem.Text.Substring(116, 14).Trim();
                    this.txtissueqty.Text = ddlproduct.SelectedItem.Text.Substring(116, 14).Trim();
                    //this.txtRate.Text = ddlproduct.SelectedItem.Text.Substring(105, 14).Trim();
                    ddlPackSize.Items.Clear();
                    ddlPackSize.Items.Add(new ListItem(ddlproduct.SelectedItem.Text.Substring(60, 14), UNITID));
                }
                else if (ddlpo.SelectedValue != "0" && Cache["ISWORKORDER"].ToString() != "N")
                {
                    lblproductname.Text = ddlproduct.SelectedItem.Text.Substring(0, 60).Trim();
                    this.txtPoQty.Text = ddlproduct.SelectedItem.Text.Substring(74, 14).Trim();
                    /*this.txtDespatchQty.Text = ddlproduct.SelectedItem.Text.Substring(88, 14).Trim();
                    this.txtQcQty.Text = ddlproduct.SelectedItem.Text.Substring(102, 14).Trim();//Remaining Qty*/
                    this.txtMRP.Text = ddlproduct.SelectedItem.Text.Substring(72, 14).Trim();
                    //this.txtissueqty.Text = ddlproduct.SelectedItem.Text.Substring(116, 14).Trim();               
                    ddlPackSize.Items.Clear();
                    ddlPackSize.Items.Add(new ListItem(ddlproduct.SelectedItem.Text.Substring(60, 14), UNITID));
                }
               
                else
                {
                    this.txtPoQty.Text = "";
                    this.txtDespatchQty.Text = "";
                    this.txtQcQty.Text = "";
                    this.txtMRP.Text = "";
                    this.txtRate.Text = "";
                }
            }
            else
            {
               

                ViewState["HSNCODE"] = Convert.ToString(gethsn("HSNFORCONPRODUCT", lblproductid.Text));
                string hsn = Convert.ToString(ViewState["HSNCODE"]);
                this.txtAlreadyDespatchQty.Text = "0";
                this.txtPoQty.Text = "0";
                this.txtDespatchQty.Text = "0";
                this.txtQcQty.Text = "0";
                this.txtMRP.Text = "0";
                this.txtRate.Text = "0";
                loadPacksizeForConsumableProduct("PacksizeForConsumable", lblproductid.Text);
                lblproductname.Text = ddlproduct.SelectedItem.Text.Trim();
            }
           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

    public void loadPacksizeForConsumableProduct(string mode,string id)
    {
        try
        {
            ClsVendor_TPU cls = new ClsVendor_TPU();
            DataTable dt = new DataTable();
            dt = cls.BindPoFromTpu(mode, id);
            if (dt.Rows.Count > 0)
            {
                this.ddlPackSize.Items.Add(new ListItem("Select Storelocation", "0"));
                this.ddlPackSize.AppendDataBoundItems = true;
                this.ddlPackSize.DataSource = dt;
                this.ddlPackSize.Items.Clear();
                this.ddlPackSize.DataValueField = "ID";
                this.ddlPackSize.DataTextField = "NAME";
                this.ddlPackSize.DataBind();
            }
            else
            {
                this.ddlPackSize.Items.Clear();
                this.ddlPackSize.Items.Add(new ListItem("Select Storelocation", "0"));
            }

        }
        catch(Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }



    #region Load LoadProductDetails
    protected void LoadProductDetails()
    {
        if (ddlDepot.SelectedValue != "0")
        {
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            DataTable dt = new DataTable();
            //dt = clsDespatch.GetPO_QtyCombo(this.ddlproduct.SelectedValue.Trim(), this.ddlTPU.SelectedValue.Trim(), this.ddlPackSize.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim(), Request.QueryString["FG"].ToString().Trim());
            dt = clsgrnmm.BindPoWiseProduct(this.ddlpo.SelectedValue.Trim(), this.ddlpo.SelectedItem.ToString().Trim(), ddlissueProduct.SelectedValue.ToString());
            List<GRNdetails> PUDetails = new List<GRNdetails>();
            if (dt.Rows.Count > 0)
            {
                Cache["ISWORKORDER"] = dt.Rows[0]["ISWORKORDER"].ToString();
                if (Cache["ISWORKORDER"].ToString() != "Y")
                {
                    txtDespatchQty.Enabled = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GRNdetails pu = new GRNdetails();
                        pu.POID = dt.Rows[i]["POID"].ToString();
                        pu.PONO = dt.Rows[i]["PONO"].ToString();
                        pu.PRODUCT_ID = dt.Rows[i]["PRODUCT_ID"].ToString();
                        if (dt.Rows[i]["PRODUCT_NAME"].ToString().Length < 60)
                        {
                            pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, dt.Rows[i]["PRODUCT_NAME"].ToString().Length);
                            //txtpopproduct.Text = pu.PRODUCTNAME;
                        }
                        else
                        {
                            pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, 60);
                            //txtpopproduct.Text = pu.PRODUCTNAME;
                        }
                        pu.POQTY = dt.Rows[i]["POQTY"].ToString();
                        pu.DESPATCHQTY = dt.Rows[i]["DESPATCHQTY"].ToString();
                        pu.REMAININGQTY = dt.Rows[i]["REMAININGQTY"].ToString();
                        pu.MRP = dt.Rows[i]["MRP"].ToString();
                        pu.RATE = dt.Rows[i]["RATE"].ToString();
                        pu.UNITID = dt.Rows[i]["UNITID"].ToString();
                        pu.UNITID = dt.Rows[i]["UNITID"].ToString();
                        pu.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                        PUDetails.Add(pu);
                    }
                    //---------------------------------------------------------------------------------------------
                    ddlproduct.Items.Clear();
                    ddlproduct.Items.Add(new ListItem("-- SELECT PRODUCT --", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    "PRODUCTNAME".PadRight(60, '\u00A0'),
                    "UNITNAME".PadRight(14, '\u00A0'),
                    "POQTY".PadRight(14, '\u00A0'),
                    "DESPATCHQTY".PadRight(14, '\u00A0'),
                    "REMAININGQTY".PadRight(14, '\u00A0'),
                    "MRP".PadRight(14, '\u00A0'),
                    "RATE".PadRight(14, '\u00A0'));

                    ddlproduct.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlproduct.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                            item.Attributes.CssStyle.Add("background-color", "Beige");
                        }
                    }
                    foreach (GRNdetails p in PUDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        p.PRODUCTNAME.PadRight(60, '\u00A0'),
                        p.UNITNAME.PadRight(14, '\u00A0'),
                        p.POQTY.PadRight(14, '\u00A0'),
                        p.DESPATCHQTY.PadRight(14, '\u00A0'),
                        p.REMAININGQTY.PadRight(14, '\u00A0'),
                        p.MRP.PadRight(14, '\u00A0'),
                        p.RATE.PadRight(14, '\u00A0'));
                        ddlproduct.Items.Add(new ListItem(text, "" + p.PRODUCT_ID + "~" + p.UNITID + ""));
                    }
                }
                else
                {
                    txtDespatchQty.Enabled = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GRNdetails pu = new GRNdetails();
                        pu.POID = dt.Rows[i]["POID"].ToString();
                        pu.PONO = dt.Rows[i]["PONO"].ToString();
                        pu.PRODUCT_ID = dt.Rows[i]["PRODUCT_ID"].ToString();
                        if (dt.Rows[i]["PRODUCT_NAME"].ToString().Length < 60)
                        {
                            pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, dt.Rows[i]["PRODUCT_NAME"].ToString().Length);
                        }
                        else
                        {
                            pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, 60);
                        }
                        /*pu.POQTY = dt.Rows[i]["POQTY"].ToString();
                        pu.DESPATCHQTY = dt.Rows[i]["DESPATCHQTY"].ToString();
                        pu.REMAININGQTY = dt.Rows[i]["REMAININGQTY"].ToString();*/
                        pu.MRP = dt.Rows[i]["MRP"].ToString();
                        pu.RATE = dt.Rows[i]["RATE"].ToString();
                        pu.UNITID = dt.Rows[i]["UNITID"].ToString();
                        pu.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                        PUDetails.Add(pu);
                    }
                    //---------------------------------------------------------------------------------------------
                    ddlproduct.Items.Clear();
                    ddlproduct.Items.Add(new ListItem("-- SELECT PRODUCT --", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}",
                    "PRODUCTNAME".PadRight(60, '\u00A0'),
                    "UNITNAME".PadRight(14, '\u00A0'),
                    /*"POQTY".PadRight(14, '\u00A0'),
                    "DESPATCHQTY".PadRight(14, '\u00A0'),
                    "REMAININGQTY".PadRight(14, '\u00A0'),*/
                    "MRP".PadRight(14, '\u00A0'),
                    "RATE".PadRight(14, '\u00A0'));

                    ddlproduct.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlproduct.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                            item.Attributes.CssStyle.Add("background-color", "Beige");
                        }
                    }
                    foreach (GRNdetails p in PUDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}",
                        p.PRODUCTNAME.PadRight(60, '\u00A0'),
                        p.UNITNAME.PadRight(14, '\u00A0'),
                        /*p.POQTY.PadRight(14, '\u00A0'),
                        p.DESPATCHQTY.PadRight(14, '\u00A0'),
                        p.REMAININGQTY.PadRight(14, '\u00A0'),*/
                        p.MRP.PadRight(14, '\u00A0'),
                        p.RATE.PadRight(14, '\u00A0'));
                        ddlproduct.Items.Add(new ListItem(text, "" + p.PRODUCT_ID + "~" + p.UNITID + ""));
                    }
                }
            }
            else
            {
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("-- SELECT Product --", "0"));
                string text1 = string.Format("{0}{1}{2}{3}",
                "PRODUCTNAME".PadRight(60, '\u00A0'),
                "UNITNAME".PadRight(14, '\u00A0'),
                /*"POQTY".PadRight(14, '\u00A0'),
                "DESPATCHQTY".PadRight(14, '\u00A0'),
                "REMAININGQTY".PadRight(14, '\u00A0'),*/
                "MRP".PadRight(14, '\u00A0'),
                "RATE".PadRight(14, '\u00A0'));
                ddlproduct.Items.Add(new ListItem(text1, "1"));

                foreach (ListItem item in ddlproduct.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        item.Attributes.CssStyle.Add("background-color", "Beige");
                    }
                }
                foreach (GRNdetails p in PUDetails)
                {
                    string text = string.Format("{0}{1}{2}{3}",
                    p.PRODUCTNAME.PadRight(60, '\u00A0'),
                    p.UNITNAME.PadRight(14, '\u00A0'),
                    /*p.POQTY.PadRight(14, '\u00A0'),
                    p.DESPATCHQTY.PadRight(14, '\u00A0'),
                    p.REMAININGQTY.PadRight(14, '\u00A0'),*/
                    p.MRP.PadRight(14, '\u00A0'),
                    p.RATE.PadRight(14, '\u00A0'));
                    ddlproduct.Items.Add(new ListItem(text, "" + p.PRODUCT_ID + "~" + p.UNITID + ""));
                }
                MessageBox1.ShowInfo("No Purchase Order is available with remaining quantity", 60, 550);
            }
        }
        else
        {
            this.ddlproduct.SelectedValue = "0";
            this.ddlPackSize.Items.Clear();
            this.ddlPackSize.Items.Add(new ListItem("Select Unit", "0"));
            this.ddlPackSize.AppendDataBoundItems = true;
            MessageBox1.ShowInfo("<b>Please select Mother Depot or Depot Name!<b>", 60, 400);
        }
    }
    #endregion

    #region Resetting Controls After Add
    public void ResetControls()
    {
        this.ddlproduct.SelectedValue = "0";
        this.txtBatch.Text = "";
        this.ddlPackSize.Items.Clear();
        this.ddlPackSize.Items.Add(new ListItem("Select Packsize", "0"));
        this.ddlPackSize.AppendDataBoundItems = true;
        //this.ddlpo.SelectedValue = "0";
        this.txtWeight.Text = "";
        this.txtRate.Text = "";
        this.txtMfDate.Text = "";
        this.txtExprDate.Text = "";
        this.txtMRP.Text = "";
        this.txtPoQty.Text = "";
        this.txtQcQty.Text = "";
        this.txtAllocatedQty.Text = "";
        this.txtAssementPercentage.Text = "";
        this.txtTotalAssement.Text = "";
        this.txtAlreadyDespatchQty.Text = "";
        this.txtTotDespatch.Text = "";
        this.txtDespatchQty.Text = "";
        this.txtitemwiseFreight.Text = "0.00";
        this.txtitemwiseAddCost.Text = "0.00";
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

    #region Convert Rejection-DataTable To XML
    public string ConvertRejectionDatatableToXML(DataTable dt)
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

    #region Convert Rejection-Tax-DataTable To XML
    public string ConvertTaxRejectionDatatableToXML(DataTable dt)
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

    #region Save GRN
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransporter.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Select Transpoter</font></b>");
                return;
            }
            if (ddlStorelocation.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Select Stroelocation</font></b>");
                return;
            }
            else
            {
                this.ControlEnable();
                int CountTerms = 0;
                string DespatchtNo = string.Empty;
                string xml = string.Empty;
                string xmlTax = string.Empty;
                string xmlGrossTax = string.Empty;
                string strTaxID = string.Empty;
                string strTaxPercentage = string.Empty;
                string strTaxValue = string.Empty;
                string strTermsID = string.Empty;
                string allocationDate = string.Empty;
                string f_formactive = string.Empty;
                string xmlRejectionDetails = string.Empty;
                string xmlRejectionTaxDetails = string.Empty;
                string xmladdnDetails = string.Empty;
                string xmlJobOrderReceive = string.Empty;
                string xmlSampleQty = string.Empty;

                DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                DataTable dtTaxRecordsCheck = (DataTable)HttpContext.Current.Session["TAXCOMPONENTDETAILS"];
                DataTable dtRejectionDetailsTotal = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];
                DataTable dtAddnDetails = (DataTable)HttpContext.Current.Session["ADDN_DETAILS"];
                DataTable dtRejectionTaxRecordsCheck = new DataTable();
                DataTable dtJobOrderReceive = (DataTable)HttpContext.Current.Session["JOBORDER_RECEIVE"];
                DataTable dtSample = (DataTable)HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"];

                if (Session["REJECTIONTAXCOMPONENTDETAILS"] != null)
                {
                    dtRejectionTaxRecordsCheck = (DataTable)HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"];
                }
                menuID = Request.QueryString["MENUID"].ToString().Trim();

                

                if (dtRecordsCheck.Rows.Count > 0 || ViewState["OP"].ToString() == "QC")
                {
                    xml = ConvertDatatableToXML(dtRecordsCheck);
                    if (dtTaxRecordsCheck != null)
                    {
                        if (dtTaxRecordsCheck.Rows.Count > 0)
                        {
                            xmlTax = ConvertDatatableToXML(dtTaxRecordsCheck);
                        }
                    }

                    if (dtRejectionDetailsTotal != null)
                    {
                        if (dtRejectionDetailsTotal.Rows.Count > 0)
                        {
                            xmlRejectionDetails = ConvertRejectionDatatableToXML(dtRejectionDetailsTotal);
                        }
                    }

                    if (dtRejectionTaxRecordsCheck != null)
                    {
                        if (dtRejectionTaxRecordsCheck.Rows.Count > 0)
                        {
                            xmlRejectionTaxDetails = ConvertTaxRejectionDatatableToXML(dtRejectionTaxRecordsCheck);
                        }
                    }

                    if (dtAddnDetails != null)
                    {
                        if (dtAddnDetails.Rows.Count > 0)
                        {
                            xmladdnDetails = ConvertDatatableToXML(dtAddnDetails);
                        }
                    }

                    if (dtJobOrderReceive != null)
                    {
                        if (dtJobOrderReceive.Rows.Count > 0)
                        {
                            xmlJobOrderReceive = ConvertDatatableToXML(dtJobOrderReceive);
                        }
                    }

                    if (dtSample != null)
                    {
                        if (dtSample.Rows.Count > 0)
                        {
                            xmlSampleQty = ConvertDatatableToXML(dtSample);
                        }
                    }

                    string CapacityFileUpload = string.Empty;
                    string xmlSampleQtyFileUpload = string.Empty;
                    DataTable dtSampleQty = new DataTable();
                    if (ChkCapacity.Checked)
                    {
                        dtSampleQty = (DataTable)(Session["UPLOADCAPACITYFILE"]);
                        xmlSampleQtyFileUpload = ConvertDatatableToXML(dtSampleQty);
                        CapacityFileUpload = "Y";
                    }
                    else
                    {
                        CapacityFileUpload = "N";
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
                    decimal ADDNAMOUNT = 0;
                    decimal Roundoff = Convert.ToDecimal(this.txtRoundoff.Text);
                    if (this.txtOtherCharge.Text == "")
                    {
                        OtherCharges = 0;
                    }
                    else
                    {
                        OtherCharges = Convert.ToDecimal(this.txtOtherCharge.Text.Trim());
                    }
                    if (txtaddnamt.Text != "")
                    {
                        ADDNAMOUNT = Convert.ToDecimal(this.txtaddnamt.Text);
                    }
                    if (this.txtAdj.Text == "")
                    {
                        Adjustment = 0;
                    }
                    else
                    {
                        Adjustment = Convert.ToDecimal(this.txtAdj.Text.Trim());
                    }
                    decimal TotalDespatch = Convert.ToDecimal(this.txtFinalAmt.Text);

                    #endregion

                    int dtdespatch = Convert.ToInt32(Conver_To_ISO(this.txtDespatchDate.Text.Trim()));
                    int dtInvoice = Convert.ToInt32(Conver_To_ISO(this.txtInvoiceDate.Text.Trim()));
                    int dtLRGR = Convert.ToInt32(Conver_To_ISO(this.txtLRGRDate.Text.Trim()));

                    //string LRGR = clsgrnmm.CheckLRGR(this.txtLRGRNo.Text.Trim(), this.hdnDespatchID.Value.Trim());
                    string INVOICE = clsgrnmm.CheckInvoiceNoGRN(this.txtInvoiceNo.Text, this.ddlTPU.SelectedValue.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnDespatchID.Value);
                    if (this.chkActive.Checked == true)
                    {
                        f_formactive = "Y";
                    }
                    else
                    {
                        f_formactive = "N";
                    }

                    string storeloaction = this.ddlStorelocation.SelectedValue;

                    /*if (LRGR == "1")
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>LR/GR No already exists..!</font></b>");
                    }*/
                    //else

                    if (ViewState["OP"].ToString() == "MAKER")
                    {
                        if (INVOICE == "1")
                        {
                            MessageBox1.ShowInfo("<b><font color='red'>Invoice No already exists..!</font></b>");
                        }
                        else               
                        {
                            DespatchtNo = clsgrnmm.InsertDespatchDetails(this.txtDespatchDate.Text.Trim(), this.ddlTPU.SelectedValue.Trim(), this.ddlTPU.SelectedItem.ToString().Trim(),
                                                                            this.ddlWaybill.SelectedValue.Trim(), this.txtInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(),
                                                                            this.ddlTransporter.SelectedValue, this.txtVehicle.Text.Trim(), this.ddlDepot.SelectedValue,
                                                                            Convert.ToString(this.ddlDepot.SelectedItem), this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                                            this.ddlTransportMode.SelectedValue, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                                                                            HttpContext.Current.Session["FINYEAR"].ToString(), this.txtRemarks.Text.Trim(), TotalDespatch, OtherCharges,
                                                                            Convert.ToDecimal(txtOtherCharge.Text), Roundoff, strTermsID, ViewState["OP"].ToString(), "",
                                                                            this.ddlGatePassNo.SelectedValue, this.txtgatepassdate.Text.Trim(), f_formactive.Trim(),
                                                                            xml, xmlTax, "", xmlRejectionDetails, xmladdnDetails, ADDNAMOUNT, Convert.ToString(hdnDespatchID.Value),
                                                                            this.ddlinsurancecompname.SelectedValue.Trim(), this.ddlinsurancecompname.SelectedItem.ToString().Trim(),
                                                                            this.ddlInsuranceNumber.SelectedValue.Trim(), menuID, Convert.ToString(this.hdnReceivedID.Value),
                                                                            Convert.ToInt16(ViewState["Invoice_Type"]), Convert.ToString(HDNISVERIFIEDCHECKER1.Value),
                                                                            Convert.ToString(HDNISVERIFIEDCHECKER1.Value), xmlRejectionTaxDetails, Convert.ToDecimal(this.lblTotalItemWiseFreight.Text),
                                                                            Convert.ToDecimal(this.lblTotalItemWiseAddCost.Text), Convert.ToDecimal(this.lblTotalItemWiseDist.Text),
                                                                            xmlJobOrderReceive, ddlledger.SelectedValue.ToString().Trim(), this.txtwaybilldate.Text, xmlSampleQty,
                                                                            xmlSampleQtyFileUpload, CapacityFileUpload, ddlVendorFrom.SelectedValue.ToString(),Convert.ToDecimal(this.txtTCSPercent.Text),
                                                                            Convert.ToDecimal(this.txtTCS.Text), Convert.ToDecimal(this.txtTCSNetAmt.Text),Convert.ToDecimal(this.txtTCSApplicable.Text), storeloaction,txtDevationno.Text,TxtDevationdate.Text);
                            if (DespatchtNo != "")
                            {
                                grdAddDespatch.DataSource = null;
                                grdAddDespatch.DataBind();

                                if (Convert.ToString(hdnDespatchID.Value) == "")
                                {
                                    MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b>  Saved Successfully", 60, 550);
                                    this.LoadGRN();
                                    pnlAdd.Style["display"] = "none";
                                    pnlDisplay.Style["display"] = "";
                                    btnaddhide.Style["display"] = "";
                                    DataTable DT = new DataTable();
                                    ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                                    DT = clsMMPo.Bind_Sms_Mobno("X", "");
                                    foreach (DataRow row in DT.Rows)
                                    {
                                        this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                                    }
                                    DataTable dt1 = new DataTable();
                                    dt1 = clsMMPo.Bind_Sms_Mobno("Y", "");
                                }
                                else
                                {
                                    MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b> Updated Successfully", 60, 550);
                                    this.LoadGRN();
                                    pnlAdd.Style["display"] = "none";
                                    pnlDisplay.Style["display"] = "";
                                    btnaddhide.Style["display"] = "";
                                }

                                this.hdnDespatchID.Value = "";
                                this.ResetSession();
                                this.ClearControls();
                            }
                            else
                            {
                                MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                            }
                        }
                    }
                    else if (ViewState["OP"].ToString() == "QC")
                    {
                        DespatchtNo = clsgrnmm.InsertDespatchDetails(this.txtDespatchDate.Text.Trim(), this.ddlTPU.SelectedValue.Trim(), this.ddlTPU.SelectedItem.ToString().Trim(),
                                                                     this.ddlWaybill.SelectedValue.Trim(), this.txtInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(),
                                                                     this.ddlTransporter.SelectedValue, this.txtVehicle.Text.Trim(), this.ddlDepot.SelectedValue,
                                                                     Convert.ToString(this.ddlDepot.SelectedItem), this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                                     this.ddlTransportMode.SelectedValue, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                                                                     HttpContext.Current.Session["FINYEAR"].ToString(), this.txtRemarks.Text.Trim(), TotalDespatch, OtherCharges,
                                                                     Convert.ToDecimal(txtOtherCharge.Text), Roundoff, strTermsID, ViewState["OP"].ToString(), "",
                                                                     this.ddlGatePassNo.SelectedValue, this.txtgatepassdate.Text.Trim(), f_formactive.Trim(),
                                                                     xml, xmlTax, "", xmlRejectionDetails, "", ADDNAMOUNT, Convert.ToString(hdnDespatchID.Value),
                                                                     this.ddlinsurancecompname.SelectedValue.Trim(), this.ddlinsurancecompname.SelectedItem.ToString().Trim(),
                                                                     this.ddlInsuranceNumber.SelectedValue.Trim(), menuID, Convert.ToString(this.hdnReceivedID.Value),
                                                                     Convert.ToInt16(ViewState["Invoice_Type"]), Convert.ToString(HDNISVERIFIEDCHECKER1.Value),
                                                                     Convert.ToString(HDNSTOCKIN.Value), xmlRejectionTaxDetails, Convert.ToDecimal(this.lblTotalItemWiseFreight.Text),
                                                                     Convert.ToDecimal(this.lblTotalItemWiseAddCost.Text), Convert.ToDecimal(this.lblTotalItemWiseDist.Text),
                                                                     xmlJobOrderReceive, ddlledger.SelectedValue.ToString().Trim(), this.txtwaybilldate.Text, xmlSampleQty,
                                                                     xmlSampleQtyFileUpload, CapacityFileUpload, ddlVendorFrom.SelectedValue.ToString(), Convert.ToDecimal(this.txtTCSPercent.Text),
                                                                     Convert.ToDecimal(this.txtTCS.Text), Convert.ToDecimal(this.txtTCSNetAmt.Text), Convert.ToDecimal(this.txtTCSApplicable.Text), storeloaction, txtDevationno.Text, TxtDevationdate.Text);
                        if (DespatchtNo != "")
                        {
                            grdAddDespatch.DataSource = null;
                            grdAddDespatch.DataBind();

                            if (Convert.ToString(hdnDespatchID.Value) == "")
                            {
                                MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b>  Saved Successfully", 60, 550);
                                this.LoadGRN();
                                pnlAdd.Style["display"] = "none";
                                pnlDisplay.Style["display"] = "";
                                btnaddhide.Style["display"] = "";
                                DataTable dt = new DataTable();
                                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                                dt = clsMMPo.Bind_Sms_Mobno("E", "");
                                foreach (DataRow row in dt.Rows)
                                {
                                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                                }
                                DataTable dt1 = new DataTable();
                                dt1 = clsMMPo.Bind_Sms_Mobno("F", "");
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b> Updated Successfully", 60, 550);
                                this.LoadGRN();
                                pnlAdd.Style["display"] = "none";
                                pnlDisplay.Style["display"] = "";
                                btnaddhide.Style["display"] = "";
                                DataTable dt = new DataTable();
                                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                                dt = clsMMPo.Bind_Sms_Mobno("E", Convert.ToString(hdnDespatchID.Value));
                                foreach (DataRow row in dt.Rows)
                                {
                                    this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                                }
                                DataTable dt1 = new DataTable();
                                dt1 = clsMMPo.Bind_Sms_Mobno("F", Convert.ToString(hdnDespatchID.Value));
                            }
                            this.hdnDespatchID.Value = "";
                            this.ResetSession();
                            this.ClearControls();
                        }
                        else
                        {
                            MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                        }
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Please add atleast 1 record</b>");
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
        Session["DESPATCHDETAILS"] = null;
        Session["GrossTotalTax"] = null;
        Session["dtTaxCount"] = null;
        Session["TAXCOMPONENTDETAILS"] = null;
        Session["Terms"] = null;
        Session["REJECTIONDINNERGRIDDETAILS"] = null;
        Session["TOTALREJECTIONDETAILS"] = null;
        Session["REJECTIONTAXCOMPONENTDETAILS"] = null;
        Session["ADDN_DETAILS"] = null;
        Session["JOBORDER_RECEIVE"] = null;
        Session["JOBORDER_RECEIVE_TEMP"] = null;
        Session["STOCKRECEIVED_SAMPLEQTY"] = null;
        Session["UPLOADCAPACITYFILE"] = null;
    }
    #endregion

    #region Delete Despatch
    protected void DeleteRecordDespatch(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string isStatus = "";
            string despatchID = Convert.ToString(e.Record["STOCKRECEIVEDID"]).Trim();
            string ISVERIFIED = Convert.ToString(e.Record["ISVERIFIEDDESC"]).Trim();
            if(ISVERIFIED=="CANCEL")
            {
                e.Record["Error"] = "It Is a Cancel Receord you can't delete it";
                return;
            }
            isStatus = clsgrnmm.checkeGrnStatus(despatchID);
            if(isStatus != "N")
            {
                e.Record["Error"] = "GRN alreday booked for qa,not allow to delete";
                return;
            }

            int flag = 0;
            flag = clsgrnmm.DeleteStockDespatch(e.Record["STOCKRECEIVEDID"].ToString());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.LoadGRN();
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

    #region Edit GRN
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.ddlTPU.Enabled = false;
            //this.ddlVendorFrom.Enabled = false;
            //this.ddlGatePassNo.Enabled = false;
            this.hdnViewMode.Value = "N";
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal additionalgross = 0;
            decimal ItemWiseFreight = 0;
            decimal ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value);
            DataSet ds = new DataSet();
            DataTable dtWaybillNo = new DataTable();
            this.trAutoDespatchNo.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            div_btnPrint.Style["display"] = "";
            ViewState["SumTotal"] = 0;
           // Session.Remove("TAXCOMPONENTDETAILS");



            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
                this.lblQcRemarks.Visible = false;
                this.txtQcRemarks.Visible = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
                this.lblQcRemarks.Visible = true;
                this.txtQcRemarks.Visible = true;
            }
            #endregion

            if (ViewState["OP"].ToString() == "QC")
            {
                divbtnapprove.Visible = false;
                divbtnrejection.Visible = false;
                btnaddhide.Visible = false;
                trpodetails.Visible = false;
                grdAddDespatch.Columns[0].Visible = false;
                this.ddlTPU.Enabled = true;
            }
            else if (ViewState["OP"].ToString() == "MAKER")
            {
                divbtnapprove.Visible = false;
                divbtnrejection.Visible = false;
                btnaddhide.Visible = true;
                trpodetails.Visible = true;
            }
            else if (ViewState["OP"].ToString() == "Checker1" && Clsdespatch.GetChecker1Status(receivedID) == "0")
            {
                divbtnapprove.Visible = true;
                divbtnrejection.Visible = true;
                btnsubmitdiv.Visible = false;
                btnaddhide.Visible = false;
                trpodetails.Visible = false;
                grdAddDespatch.Columns[0].Visible = false;
                FetchLedger();
            }
            else if (ViewState["OP"].ToString() == "Checker1" && Clsdespatch.GetChecker1Status(receivedID) == "1")
            {
                divbtnapprove.Visible = false;
                divbtnrejection.Visible = false;
                btnsubmitdiv.Visible = false;
                btnaddhide.Visible = false;
                trpodetails.Visible = false;
                grdAddDespatch.Columns[0].Visible = false;
                FetchLedger();
            }

            this.LoadTax();
            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTableRejectionTaxComponent();
            this.CreateDataTable_TAX();
            this.CreateDataTable_JOBORDER();
            this.CreateDataTable_JOBORDER_TEMP();
            this.CreateDataTable_SampleQty();
            this.CreateDataTable_UploadCapacity();
            ds = clsgrnmm.EditReceivedDetails(receivedID);
            string verifiedTag = string.Empty;
            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDID"]);
                this.txtDespatchNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDNO"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                this.txtQcRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["QCREMARKS"]);
                string VendorFrom = Convert.ToString(ds.Tables[0].Rows[0]["VENDORFROM"]);
                verifiedTag = Convert.ToString(ds.Tables[0].Rows[0]["ISVERIFIED"]);
                this.LoadGatePassNo();
                this.ddlGatePassNo.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                if (VendorFrom == "P")//From PO
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                }
                else if (VendorFrom == "C")//for Consumable
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                    Cache["ISWORKORDER"] = "C";
                }
                else if (VendorFrom == "R")//RGP/NRGP
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                }
                else//JOB ORDER
                {
                    LoadJobOrderVendor();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                    LoadIssueProduct();
                }
                this.LoadMotherDepot();
                this.LoadPoMM(this.ddlTPU.SelectedValue.Trim());
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                this.LoadTransporter();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
                this.ddlInsuranceNumber.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();
                
                this.txtitemwiseFreight.Text = "0.00";
                this.txtitemwiseAddCost.Text = "0.00";
                if (Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]) == "")
                {
                    this.ddlledger.SelectedValue = "0";
                }
                else
                {
                    this.FetchLedger();
                    this.ddlledger.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]);
                }
                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtgatepassdate.Text = "";
                }
                else
                {
                    this.txtgatepassdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
                //dtWaybillNo = clsgrnmm.BindWaybillEdit(this.ddlDepot.SelectedValue.Trim());
           
                if (Convert.ToDecimal(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]) > 0)
                {


                    this.RbApplicable.SelectedValue = "Y";
                    this.txtTCSPercent.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCS_PERCENT"]);
                    this.txtTCS.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAMOUNT"]);
                    this.txtTCSNetAmt.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]);
                    this.txtTCSApplicable.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAPPLICABLE_AMOUNT"]);
                    this.divtcs.Visible = true;


                }
                else
                {
                    this.divtcs.Visible = false;
                    this.RbApplicable.SelectedValue = "N";
                    this.txtTCSPercent.Text = "0";
                    this.txtTCS.Text = "0";
                    this.txtTCSNetAmt.Text = "0";
                    this.txtTCSApplicable.Text = "0";
                    this.txtTCSApplicable.Enabled = false;
                }



                this.ddlWaybill.Items.Clear();
                this.ddlWaybill.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]), Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"])));
                this.ddlWaybill.AppendDataBoundItems = true;

                this.txtwaybilno.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]).Trim();


                this.LoadStoreLocation();
                this.ddlStorelocation.SelectedValue=Convert.ToString(ds.Tables[0].Rows[0]["ID"]);


                if (Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]) == "01/01/1900")
                {
                    this.txtwaybilldate.Text = "";
                }
                else
                {
                    this.txtwaybilldate.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]).Trim();
                }
                //this.LoadProduct(this.ddlTPU.SelectedValue.Trim());
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);

                if (ds.Tables[0].Rows[0]["CAPACITYUPLOAD"].ToString().Trim() == "Y")
                {
                    ChkCapacity.Checked = true;
                    divshowcapacity.Style["display"] = "";
                }
                else
                {
                    ChkCapacity.Checked = false;
                    divshowcapacity.Style["display"] = "none";
                }
                string StockreceivedID = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDID"]);
                string checkRQCQA=Clsdespatch.checkQCQA(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]), FG, ViewState["OP"].ToString(),StockreceivedID);
                if (checkRQCQA == "APPROVED" || checkRQCQA == "CANCEL" || checkRQCQA == "GRN STOCK IN")
                {
                    this.btnsubmitdiv.Visible = false;
                }
                else if (checkRQCQA == "PENDING" || checkRQCQA == "REJECTED")
                {
                    this.btnsubmitdiv.Visible = true;
                }
                else
                {
                    this.btnsubmitdiv.Visible = false;
                }

                this.txtDevationno.Text= Convert.ToString(ds.Tables[0].Rows[0]["DEVATIONNO"]);
                this.TxtDevationdate.Text= Convert.ToString(ds.Tables[0].Rows[0]["DEVATIONDATE"]);
                


            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }

                HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                DataTable dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]);
                    dr["PONO"] = Convert.ToString(ds.Tables[1].Rows[i]["PONO"]);
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]);
                    dr["POQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["POQTY"]);
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    dr["DESPATCHQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]);
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]);
                    string Rate = Convert.ToString(ds.Tables[1].Rows[i]["RATE"].ToString());
                    dr["RATE"] = Rate.Remove(Rate.Length - 2);
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    dr["QCREJECTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QCREJECTQTY"]);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);

                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = Clsdespatch.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = Clsdespatch.GetHSNTaxOnEdit(hdnDespatchID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                break;

                            case "4":
                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                break;

                            case "5":
                                /*dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                                        Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                excise = Convert.ToDecimal(String.Format("{0:0.00}",
                                                                Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                                                Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));*/
                                break;
                            case "7":
                                /*dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                                     (Convert.ToDecimal(dr["AMOUNT"]) + excise) *
                                                      Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));*/

                                break;
                        }
                    }
                    #endregion

                    dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                    dr["ITEMDES"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMDES"]);
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();
                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }

                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;
                ItemWiseFreight = CalculateTotalFreight(dtDespatchEdit);
                ItemWiseAddCost = CalculateTotalAddCost(dtDespatchEdit);
                ItemWiseDiscAmt = CalculateTotalDiscount(dtDespatchEdit);

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
                this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
                this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));

                #region Tax On Gross Amount
                this.LoadTax();
                this.LoadTermsConditions();
                DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionInnerGridDataTable();
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
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
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

                #region grdAddDespatch DataBind

                HttpContext.Current.Session["DESPATCHDETAILS"] = dtDespatchEdit;
                if (dtDespatchEdit.Rows.Count > 0)
                {
                    this.grdAddDespatch.DataSource = dtDespatchEdit;
                    this.grdAddDespatch.DataBind();
                    GridCalculation();
                }
                else
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();
                }

                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["TAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                #region grdAddDespatch DataBind
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                #endregion
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtNetAmt.Text)));

                ViewState["NETAMT"] = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtFinalAmt.Text.Trim()))));
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

            #region Rejection Tax Component
            if (ds.Tables[9].Rows.Count > 0)
            {
                DataTable dtRejectionTaxComponentEdit = (DataTable)Session["REJECTIONTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    DataRow dr = dtRejectionTaxComponentEdit.NewRow();
                    dr["POID"] = Convert.ToString(ds.Tables[9].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[9].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[9].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables[9].Rows[i]["TAXID"]);
                    dr["PERCENTAGE"] = Convert.ToString(ds.Tables[9].Rows[i]["PERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables[9].Rows[i]["TAXVALUE"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[9].Rows[i]["PRODUCTNAME"]);
                    dr["TAXNAME"] = Convert.ToString(ds.Tables[9].Rows[i]["NAME"]);
                    dtRejectionTaxComponentEdit.Rows.Add(dr);
                    dtRejectionTaxComponentEdit.AcceptChanges();
                }

                HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"] = dtRejectionTaxComponentEdit;
            }
            #endregion

            #region Job Order Received
            if (ds.Tables[10].Rows.Count > 0)
            {
                DataTable dtJobOrderEdit = (DataTable)Session["JOBORDER_RECEIVE"];
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    DataRow dr = dtJobOrderEdit.NewRow();
                    dr["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[10].Rows[i]["STOCKDESPATCHID"]);
                    dr["POID"] = Convert.ToString(ds.Tables[10].Rows[i]["POID"]);
                    dr["CATID"] = Convert.ToString(ds.Tables[10].Rows[i]["CATID"]);
                    dr["CATNAME"] = Convert.ToString(ds.Tables[10].Rows[i]["CATNAME"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[10].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[10].Rows[i]["PRODUCTNAME"]);
                    dr["POQTY"] = Convert.ToString(ds.Tables[10].Rows[i]["POQTY"]);
                    dr["ISSUEQTY"] = Convert.ToString(ds.Tables[10].Rows[i]["ISSUEQTY"]);
                    dr["DISPATCHQTY"] = Convert.ToString(ds.Tables[10].Rows[i]["DISPATCHQTY"]);
                    dr["RECEIVEQTY"] = Convert.ToString(ds.Tables[10].Rows[i]["RECEIVEQTY"]);
                    dr["WORKORDERPRODUCTID"] = Convert.ToString(ds.Tables[10].Rows[i]["WORKORDERPRODUCTID"]);
                    dtJobOrderEdit.Rows.Add(dr);
                    dtJobOrderEdit.AcceptChanges();
                }
                HttpContext.Current.Session["JOBORDER_RECEIVE"] = dtJobOrderEdit;
            }
            #endregion

            #region Sample Qty
            if (ds.Tables[11].Rows.Count > 0)
            {
                DataTable DtSampleEdit = (DataTable)HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"];
                for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                {
                    DataRow dr = DtSampleEdit.NewRow();
                    dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[11].Rows[i]["STOCKRECEIVEDID"]);
                    dr["POID"] = Convert.ToString(ds.Tables[11].Rows[i]["POID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[11].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[11].Rows[i]["PRODUCTNAME"]);
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[11].Rows[i]["RECEIVEDQTY"]);
                    dr["SAMPLEQTY"] = Convert.ToString(ds.Tables[11].Rows[i]["SAMPLEQTY"]);
                    dr["OBSERVATIONQTY"] = Convert.ToString(ds.Tables[11].Rows[i]["OBSERVATIONQTY"]);
                    DtSampleEdit.Rows.Add(dr);
                    DtSampleEdit.AcceptChanges();
                }
                HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"] = DtSampleEdit;
            }
            #endregion

            #region Capacity Documents Upload
            if (ds.Tables[12].Rows.Count > 0)
            {
                DataTable DtCapacityEdit = (DataTable)HttpContext.Current.Session["UPLOADCAPACITYFILE"];
                for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                {
                    DataRow dr = DtCapacityEdit.NewRow();
                    dr["FILENAME"] = Convert.ToString(ds.Tables[12].Rows[i]["FILENAME"]);
                    DtCapacityEdit.Rows.Add(dr);
                    DtCapacityEdit.AcceptChanges();
                }
                HttpContext.Current.Session["UPLOADCAPACITYFILE"] = DtCapacityEdit;
            }

            if (verifiedTag == "Y" && ViewState["OP"].ToString()=="QC")
            {
                this.btnsubmitdiv.Visible = true;
            }
            else if(verifiedTag == "Y")
            {
                this.btnsubmitdiv.Visible = false;
            }
            #endregion
          

            
            if (ViewState["OP"].ToString() == "QC")
            {
                this.btnsubmitdiv.Style["display"] = "";
                this.btnsubmitdiv.Visible = true;
            }
            else
            {
                this.btnsubmitdiv.Style["display"] = "none";
            }

            if (verifiedTag == "DELETE")
            {
                this.btnsubmitdiv.Style["display"] = "none";
                this.btnsubmitdiv.Visible = false;
            }
           


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
        string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
        this.txtWaybillUpdate.Text = hdnWaybillNo.Value.ToString();
        this.txtWayBillKey.Text = hdnWaybillKey.Value.ToString();
        this.light.Style["display"] = "block";
        this.fade.Style["display"] = "block";

    }
    #endregion

    #region btngrdUpdateForm_Click
    protected void btngrdUpdateForm_Click(object sender, EventArgs e)
    {
        string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
        string formFlag = string.Empty;
        formFlag = clsgrnmm.CheckFormRequired(despatchID);
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
    #endregion

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        
        div_btnPrint.Style["display"] = "none";
        this.hdnDespatchID.Value = "";
        this.LoadGatePassNo();
        this.trAutoDespatchNo.Style["display"] = "none";
        pnlDisplay.Style["display"] = "none";
        pnlAdd.Style["display"] = "";
        btnaddhide.Style["display"] = "none";
        this.chkActive.Checked = false;
        lbltext.Text = "No!";
        lbltext.Style.Add("color", "red");
        this.LoadTermsConditions();
        clsgrnmm.ResetDataTables();
        this.ClearControls();
        this.ResetControls();
        this.LoadTPU();
        this.LoadReason();

        #region Enable Controls
        this.ImageButton1.Enabled = true;
        this.txtInvoiceNo.Enabled = true;
        this.imgbtnInvoiceCalendar.Enabled = true;
        this.ddlGatePassNo.Enabled = true;
        //this.ddlDepot.Enabled = true;
        this.RbApplicable.SelectedValue = "N" ;
        this.divtcs.Visible = false;
        this.txtTCSPercent.Text = "0";
        this.txtTCS.Text = "0";
        this.txtTCSNetAmt.Text = "0";
        this.txtTCSApplicable.Text = "0";
        this.txtTCSApplicable.Enabled = false;
       
        #endregion

        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.gvProductTax.DataSource = null;
        this.gvProductTax.DataBind();
        //txtDespatchDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.DateLock();
        txtLRGRDate.Text = dtcurr.ToString(date).Replace('-', '/');
        txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.BindInsurenceCompany();

        this.ddlpo.Items.Clear();
        this.ddlpo.Items.Add(new ListItem("Select PO/Indent No", "0"));
        this.ddlpo.AppendDataBoundItems = true;
        this.ddlpo.SelectedValue = "0";
        this.ddlPackSize.Items.Clear();
        this.ddlPackSize.Items.Add(new ListItem("Select Packsize/Unit", "0"));
        this.ddlPackSize.AppendDataBoundItems = true;
        this.ddlPackSize.SelectedValue = "0";

        #region QueryString

        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        if (Checker == "TRUE")
        {
            this.btnsubmitdiv.Visible = false;
            this.lblCheckerNote.Visible = false;
            this.txtCheckerNote.Visible = false;
            this.txtRemarks.Enabled = false;
            this.lblQcRemarks.Visible = false;
            this.txtQcRemarks.Visible = false;
        }
        else
        {
            this.btnsubmitdiv.Visible = true;
            this.lblCheckerNote.Visible = false;
            this.txtCheckerNote.Visible = false;
            this.txtRemarks.Enabled = true;
            this.lblQcRemarks.Visible = false;
            this.txtQcRemarks.Visible = false;
        }
        #endregion
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnDespatchID.Value = "";
        this.hdnSuppliedItem.Value = "";
        this.txtDespatchDate.Text = "";
        this.txtDespatchNo.Text = "";
        this.ddlTransporter.SelectedValue = "0";
        this.ddlTransportMode.SelectedValue = "0";
        this.ddlStorelocation.SelectedValue = "0";
        this.txtLRGRDate.Text = "";
        this.txtLRGRNo.Text = "";
        this.txtVehicle.Text = "";
        this.txtInvoiceDate.Text = "";
        this.txtInvoiceNo.Text = "";
        this.txtRemarks.Text = "";
        this.txtDevationno.Text = "";
        this.TxtDevationdate.Text = "";
 
        this.ddlWaybill.Items.Clear();
        this.ddlWaybill.Items.Add(new ListItem("-- SELECT WAYBILL KEY --", "0"));
        this.ddlWaybill.AppendDataBoundItems = true;
        this.ddlWaybill.SelectedValue = "0";

        this.ddlpo.Items.Clear();
        this.ddlpo.Items.Add(new ListItem("-- SELECT PO/Indent --", "0"));
        this.ddlpo.AppendDataBoundItems = true;
        this.ddlpo.SelectedValue = "0";

        this.ddlPackSize.Items.Clear();
        this.ddlPackSize.Items.Add(new ListItem("-- SELECT PACKSIZE/Unit --", "0"));
        this.ddlPackSize.AppendDataBoundItems = true;
        this.ddlPackSize.SelectedValue = "0";

        this.ddlTPU.Items.Clear();
        this.ddlTPU.Items.Add(new ListItem("Select TPU/Vendor", "0"));
        this.ddlTPU.AppendDataBoundItems = true;
        this.ddlTPU.SelectedValue = "0";

        this.ddlGatePassNo.SelectedValue = "0";
        this.txtgatepassdate.Text = "";
        this.txtPoDate.Text = "";
        this.txtAdj.Text = "0";
        this.txtOtherCharge.Text = "0";
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "";
        this.txtTotMRP.Text = "";
        this.txtTotTax.Text = "";
        this.txtNetAmt.Text = "";
        this.txtRoundoff.Text = "";

        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.gvProductTax.DataSource = null;
        this.gvProductTax.DataBind();

        this.ddlpo.SelectedValue = "0";
        this.txtPoDate.Text = "";
        this.ddlinsurancecompname.SelectedValue = "0";
        this.ddlInsuranceNumber.SelectedValue = "0";
        this.hdnExprDate.Value = "";
        this.hdnMfDate.Value = "";
        this.hdnGRNID.Value = "";
        this.hdnpoid.Value = "";
        this.hdnproductid.Value = "";
        this.hdnReceivedID.Value = "";
        this.hdnSuppliedItem.Value = "";
        this.hdn_Remaining.Value = "";
        this.hdn_PackSizeQC.Value = "";
        this.hdn_guid.Value = "";
        this.hdn_CurrentQty.Value = "";
        this.hdnDespatchID.Value = "";
        this.hdndtBATCHDelete.Value = "";
        this.hdndtDespatchDelete.Value = "";
        this.hdndtPOIDDelete.Value = "";
        this.hdndtPRODUCTIDDelete.Value = "";
        this.txtFinalAmt.Text = "";
        this.hdnAFTERDISCOUNTAMT.Value = "";
        this.hdnRCVDQTY.Value = "";
        this.hdnRejBatch.Value = "";
        Session["JOBORDER_RECEIVE"] = null;
        this.txtwaybilno.Text = "";
        this.txtwaybilldate.Text = "";
        Session["UPLOADCAPACITYFILE"] = null;
        this.ddlissueProduct.SelectedValue = "0";
        this.ddlVendorFrom.SelectedValue = "P";
       
    }
    #endregion

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ControlEnable();
        this.trAutoDespatchNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "";
        div_btnPrint.Style["display"] = "none";
        clsgrnmm.ResetDataTables();
        this.ClearControls();
        this.ResetControls();
        this.ResetSession();

        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.gvProductTax.DataSource = null;
        this.gvProductTax.DataBind();
        this.grdTax.ClearPreviousDataSource();
        this.grdTax.DataSource = null;
        this.grdTax.DataBind();
        this.grdTerms.ClearPreviousDataSource();
        this.grdTerms.DataSource = null;
        this.grdTerms.DataBind();
        this.hdnDespatchID.Value = "";
        this.ddlpo.SelectedValue = "0";
        this.LoadGRN();
        int flag = 0;
        flag = clsgrnmm.DeleteJobOrderTemp();
    }
    #endregion

    #region Search GRN
    protected void btnSearchDespatch_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadGRN();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnWaybillUpdate_Click
    protected void btnWaybillUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsgrnmm.UpdateWaybillNo(despatchID, this.txtWaybillUpdate.Text.Trim(), this.txtWayBillKey.Text.Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.light.Style["display"] = "none";
                this.fade.Style["display"] = "none";
                this.grdDespatchHeader.DataSource = clsgrnmm.BindDespatch(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlTPU.SelectedValue.Trim());
                this.grdDespatchHeader.DataBind();
                MessageBox1.ShowSuccess("<b><font color='green'> Waybill No. Updated Successfully</font></b>", 80, 500);
            }
            else if (flag == 0)
            {
                MessageBox1.ShowError("<b><font color='red'> Error saving record..</font></b>");
            }
            else if (flag == 2)
            {
                MessageBox1.ShowInfo("Waybill Key:<b>" + this.txtWayBillKey.Text.Trim() + " is not in waybill inventory</b>", 80, 500);
            }
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
            int flag = 0;
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsgrnmm.UpdateCForm(despatchID, this.txtCFormNo.Text.Trim(), this.txtCFormPopupDate.Text.Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.light2.Style["display"] = "none";
                this.fade.Style["display"] = "none";
                this.grdDespatchHeader.DataSource = clsgrnmm.BindDespatch(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlTPU.SelectedValue.Trim());
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

    #region btnCloseLightbox2_Click
    protected void btnCloseLightbox2_Click(object sender, EventArgs e)
    {
        this.light2.Style["display"] = "none";
        this.fade.Style["display"] = "none";
    }
    #endregion

    #region grdTax_RowDataBound
    protected void grdTax_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataSet ds = new DataSet();
        if (hdnDespatchID.Value != "")
        {

        }
        if (e.Row.RowType == GridRowType.DataRow)
        {

        }
    }
    #endregion

    #region grdTerms_RowDataBound
    protected void grdTerms_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataSet ds = new DataSet();
        if (hdnDespatchID.Value != "")
        {
            ds = clsgrnmm.EditReceivedDetails(hdnDespatchID.Value);
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

    #region ddlTPU_SelectedIndexChanged
    protected void ddlTPU_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlTPU.SelectedValue != "0")
        {
            this.ImageButton1.Enabled = false;
            this.CalendarExtenderDespatchDate.Enabled = false;
            this.LoadTransporter();
            this.PoAutoClose(this.ddlTPU.SelectedValue);
            this.LoadPoMM(this.ddlTPU.SelectedValue);
            this.DepotSelection();
            if (CFormRequired(this.ddlTPU.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim()) == 1)
            {
                this.chkActive.Checked = false;
            }
            else
            {
                this.chkActive.Checked = true;
            }
            if (chkActive.Checked)
            {
                lbltext.Text = "Yes!";
                lbltext.Style.Add("color", "green");
                lbltext.Style.Add("font-weight", "bold");
            }
            else
            {
                lbltext.Text = "No!";
                lbltext.Style.Add("color", "red");
                lbltext.Style.Add("font-weight", "bold");
            }
        }
        else
        {
            this.ImageButton1.Enabled = true;
            this.CalendarExtenderDespatchDate.Enabled = true;
        }
    }
    #endregion

    #region FetchSuppliedItem
    protected void FetchSuppliedItem(string VendorID)
    {
        ClsGRNMM GrnMM = new ClsGRNMM();
        DataTable dtSuppliedItem = new DataTable();
        dtSuppliedItem = GrnMM.FetchSuppliedItem(VendorID);
        if (dtSuppliedItem.Rows.Count > 0)
        {
            this.hdnSuppliedItem.Value = Convert.ToString(dtSuppliedItem.Rows[0]["SUPLIEDITEMID"]);
        }
        else
        {
            this.hdnSuppliedItem.Value = "";
        }
    }
    #endregion

    #region DepotSelection
    protected void DepotSelection()
    {
        this.ddlPackSize.Items.Clear();
        this.ddlPackSize.Items.Add(new ListItem("Select Unit", "0"));
        this.ddlPackSize.AppendDataBoundItems = true;
        this.ddlPackSize.SelectedValue = "0";
        this.CreateDataTable();// Creating DataTable Structure
        this.CreateDataTableTaxComponent();// Creating DataTable Structure
        this.LoadTax();
        this.CreateDataTable_TAX();//Creating DataTable Additional Detsails Structure   
        this.CreateDataTable_JOBORDER();//Creating DataTable Job Order Details Structure  
        this.CreateDataTable_JOBORDER_TEMP();//Creating DataTable Job Order Details Temp Structure  
        DataTable dtWaybillNo = new DataTable();
        if (this.hdnDespatchID.Value == "")
        {
            dtWaybillNo = clsgrnmm.BindWaybill(this.ddlDepot.SelectedValue.Trim());
        }
        else
        {
            dtWaybillNo = clsgrnmm.BindWaybillEdit(this.ddlDepot.SelectedValue.Trim());
        }

        if (dtWaybillNo.Rows.Count > 0)
        {
            this.ddlWaybill.Items.Clear();
            this.ddlWaybill.Items.Add(new ListItem("Select Waybill", "0"));
            this.ddlWaybill.AppendDataBoundItems = true;
            this.ddlWaybill.DataSource = dtWaybillNo;
            this.ddlWaybill.DataValueField = "WAYBILLNO";
            this.ddlWaybill.DataTextField = "WAYBILLNO";
            this.ddlWaybill.DataBind();
        }
        else
        {
            this.ddlWaybill.Items.Clear();
            this.ddlWaybill.Items.Add(new ListItem("Select Waybill", "0"));
            this.ddlWaybill.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region BindInsurenceCompany
    protected void BindInsurenceCompany()
    {
        DataTable dt = new DataTable();
        dt = clsgrnmm.Bindinscomp();
        if (dt.Rows.Count > 0)
        {
            this.ddlinsurancecompname.Items.Clear();
            this.ddlinsurancecompname.Items.Add(new ListItem("Select Insurance Company", "0"));
            this.ddlinsurancecompname.AppendDataBoundItems = true;
            this.ddlinsurancecompname.DataSource = dt;
            this.ddlinsurancecompname.DataValueField = "ID";
            this.ddlinsurancecompname.DataTextField = "COMPANY_NAME";
            this.ddlinsurancecompname.DataBind();
            if (dt.Rows.Count == 1)
            {
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(dt.Rows[0]["ID"]);
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
            }
        }
        else
        {
            this.ddlinsurancecompname.Items.Clear();
            this.ddlinsurancecompname.Items.Add(new ListItem("Select Insurance Company", "0"));
            this.ddlinsurancecompname.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region BindInsurenceNumber
    protected void BindInsurenceNumber(string CompID)
    {
        DataTable dt = new DataTable();
        dt = clsgrnmm.BindinsNumber(CompID);
        if (dt.Rows.Count > 0)
        {
            this.ddlInsuranceNumber.Items.Clear();
            this.ddlInsuranceNumber.Items.Add(new ListItem("Select Insurance No", "0"));
            this.ddlInsuranceNumber.AppendDataBoundItems = true;
            this.ddlInsuranceNumber.DataSource = dt;
            this.ddlInsuranceNumber.DataValueField = "INSURANCE_NO";
            this.ddlInsuranceNumber.DataTextField = "INSURANCE_NO";
            this.ddlInsuranceNumber.DataBind();
            if (dt.Rows.Count == 1)
            {
                this.ddlInsuranceNumber.SelectedValue = Convert.ToString(dt.Rows[0]["INSURANCE_NO"]);
            }
        }
    }
    #endregion

    #region ddlinsurancecompname_SelectedIndexChanged
    protected void ddlinsurancecompname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlinsurancecompname.SelectedValue != "0")
        {
            this.BindInsurenceNumber(ddlinsurancecompname.SelectedValue.Trim());
        }
    }
    #endregion

    #region txtMfDate_TextChanged
    protected void txtMfDate_TextChanged(object sender, EventArgs e)
    {
        ClsGRNMM GrnMM = new ClsGRNMM();
        this.txtExprDate.Text = GrnMM.GetProductExpirydate(this.lblproductid.Text, txtMfDate.Text.ToString().Trim());
    }
    #endregion  

    #region View GRN
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            ControlDisable();
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string BRAND = string.Empty;
            string TAXID = string.Empty;
            string verifiedTag = string.Empty;
            decimal ProductWiseTax = 0;
            decimal ItemWiseFreight = 0, ItemWiseAddCost = 0, ItemWiseDiscAmt = 0;

            ViewState["SumTotal"] = 0;
            DataSet ds = new DataSet();
            DataTable dtWaybillNo = new DataTable();
            this.trAutoDespatchNo.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            div_btnPrint.Style["display"] = "";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.btnsubmitdiv.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                //this.txtRemarks.Enabled = false;
                this.lblQcRemarks.Visible = false;
                this.txtQcRemarks.Visible = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                //this.txtRemarks.Enabled = true;
                this.lblQcRemarks.Visible = true;
                this.txtQcRemarks.Visible = true;
            }
            #endregion

            this.LoadTax();
            this.LoadReason();
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();
            this.CreateDataTable_TAX();
            string receivedID = Convert.ToString(hdnDespatchID.Value);
            ds = clsgrnmm.EditReceivedDetails(receivedID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDID"]);
                this.txtDespatchNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDNO"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                this.txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                this.txtQcRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["QCREMARKS"]);
                string VendorFrom = Convert.ToString(ds.Tables[0].Rows[0]["VENDORFROM"]);
                verifiedTag = Convert.ToString(ds.Tables[0].Rows[0]["ISVERIFIED"]);
                this.LoadGatePassNo();
                this.ddlGatePassNo.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                if (VendorFrom == "P")//From PO
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                }
                else if (VendorFrom == "C")//for Consumable
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                    Cache["ISWORKORDER"] = "C";
                }
                else//JOB ORDER
                {
                    LoadJobOrderVendor();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    this.ddlVendorFrom.SelectedValue = VendorFrom;
                    LoadIssueProduct();
                }
                this.LoadMotherDepot();
                this.LoadPoMM(this.ddlTPU.SelectedValue.Trim());
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                this.LoadTransporter();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
                this.ddlInsuranceNumber.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();

                this.txtitemwiseFreight.Text = "0.00";
                this.txtitemwiseAddCost.Text = "0.00";
                if (Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]) == "")
                {
                    this.ddlledger.SelectedValue = "0";
                }
                else
                {
                    this.FetchLedger();
                    this.ddlledger.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LEDGERID"]);
                }
                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtgatepassdate.Text = "";
                }
                else
                {
                    this.txtgatepassdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
                //dtWaybillNo = clsgrnmm.BindWaybillEdit(this.ddlDepot.SelectedValue.Trim());

                if (Convert.ToDecimal(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]) > 0)
                {


                    this.RbApplicable.SelectedValue = "Y";
                    this.txtTCSPercent.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCS_PERCENT"]);
                    this.txtTCS.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAMOUNT"]);
                    this.txtTCSNetAmt.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSNETAMOUNT"]);
                    this.txtTCSApplicable.Text = Convert.ToString(ds.Tables[3].Rows[0]["TCSAPPLICABLE_AMOUNT"]);
                    this.divtcs.Visible = true;


                }
                else
                {
                    this.divtcs.Visible = false;
                    this.RbApplicable.SelectedValue = "N";
                    this.txtTCSPercent.Text = "0";
                    this.txtTCS.Text = "0";
                    this.txtTCSNetAmt.Text = "0";
                    this.txtTCSApplicable.Text = "0";
                    this.txtTCSApplicable.Enabled = false;
                }



                this.ddlWaybill.Items.Clear();
                this.ddlWaybill.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]), Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"])));
                this.ddlWaybill.AppendDataBoundItems = true;

                this.txtwaybilno.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLNO"]).Trim();


                this.LoadStoreLocation();
                this.ddlStorelocation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);


                if (Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]) == "01/01/1900")
                {
                    this.txtwaybilldate.Text = "";
                }
                else
                {
                    this.txtwaybilldate.Text = Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLDATE"]).Trim();
                }
                //this.LoadProduct(this.ddlTPU.SelectedValue.Trim());
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);

                if (ds.Tables[0].Rows[0]["CAPACITYUPLOAD"].ToString().Trim() == "Y")
                {
                    ChkCapacity.Checked = true;
                    divshowcapacity.Style["display"] = "";
                }
                else
                {
                    ChkCapacity.Checked = false;
                    divshowcapacity.Style["display"] = "none";
                }
                string StockreceivedID = Convert.ToString(ds.Tables[0].Rows[0]["STOCKRECEIVEDID"]);
                string checkRQCQA = Clsdespatch.checkQCQA(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, Convert.ToString(Session["IUserID"]), FG, ViewState["OP"].ToString(), StockreceivedID);
                if (checkRQCQA == "APPROVED" || checkRQCQA == "CANCEL" || checkRQCQA == "GRN STOCK IN")
                {
                    this.btnsubmitdiv.Visible = false;
                }
                else if (checkRQCQA == "PENDING" || checkRQCQA == "REJECTED")
                {
                    this.btnsubmitdiv.Visible = true;
                }
                else
                {
                    this.btnsubmitdiv.Visible = false;
                }

            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }

                HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                DataTable dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]);
                    dr["PONO"] = Convert.ToString(ds.Tables[1].Rows[i]["PONO"]);
                    dr["PODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PODATE"]);
                    dr["POQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["POQTY"]);
                    dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                    dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    dr["DESPATCHQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["DESPATCHQTY"]);
                    dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                    dr["REMAININGQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REMAININGQTY"]);
                    string Rate = Convert.ToString(ds.Tables[1].Rows[i]["RATE"].ToString());
                    dr["RATE"] = Rate.Remove(Rate.Length - 2);
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    dr["QCREJECTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QCREJECTQTY"]);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESABLEVALUE"]);
                    dr["DISCOUNTPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTPER"]);
                    dr["DISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCOUNTAMT"]);
                    dr["AFTERDISCOUNTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERDISCOUNTAMT"]);
                    dr["ITEMWISEFREIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEFREIGHT"]);
                    dr["AFTERITEMWISEFREIGHTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEFREIGHTAMT"]);

                    dr["ITEMWISEADDCOST"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMWISEADDCOST"]);
                    dr["AFTERITEMWISEADDCOSTAMT"] = Convert.ToString(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"]);

                    #region Loop For Adding Itemwise Tax Component
                    decimal excise = 0;

                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString()) * Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                        switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                        {
                            case "1":
                                TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                //ProductWiseTax = Clsdespatch.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()), ddlTPU.SelectedValue.ToString().Trim(), Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]));
                                ProductWiseTax = Clsdespatch.GetHSNTaxOnEdit(hdnDespatchID.Value, TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"].ToString()));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERITEMWISEADDCOSTAMT"].ToString()) * ProductWiseTax / 100));
                                break;

                            case "4":
                                //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                break;

                            case "5":
                                /*dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                                        Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                                        Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));
                                excise = Convert.ToDecimal(String.Format("{0:0.00}",
                                                                Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMRP"].ToString()) * Convert.ToDecimal(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"].ToString()) / 100 *
                                                                Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));*/
                                break;
                            case "7":
                                /*dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}",
                                                     (Convert.ToDecimal(dr["AMOUNT"]) + excise) *
                                                      Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"].ToString()) / 100));*/

                                break;
                        }
                    }
                    #endregion

                    dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();
                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                }

                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;
                ItemWiseFreight = CalculateTotalFreight(dtDespatchEdit);
                ItemWiseAddCost = CalculateTotalAddCost(dtDespatchEdit);
                ItemWiseDiscAmt = CalculateTotalDiscount(dtDespatchEdit);

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.lblTotalItemWiseFreight.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseFreight));
                this.lblTotalItemWiseAddCost.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseAddCost));
                this.lblTotalItemWiseDist.Text = Convert.ToString(String.Format("{0:0.00}", ItemWiseDiscAmt));

                #region Tax On Gross Amount
                this.LoadTax();
                this.LoadTermsConditions();
                DataTable dtGrossTax = (DataTable)Session["GrossTotalTax"];
                #endregion

                #region Rejection Information
                if (ds.Tables[7].Rows.Count > 0)
                {
                    this.CreateRejectionInnerGridDataTable();
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
                        dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[7].Rows[i]["DEPOTRATE1"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[7].Rows[i]["AMOUNT"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[7].Rows[i]["STORELOCATIONNAME"]);
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

                #region grdAddDespatch DataBind

                HttpContext.Current.Session["DESPATCHDETAILS"] = dtDespatchEdit;
                if (dtDespatchEdit.Rows.Count > 0)
                {
                    this.grdAddDespatch.DataSource = dtDespatchEdit;
                    this.grdAddDespatch.DataBind();
                    GridCalculation();
                }
                else
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();
                }

                #endregion

                DataTable dtProductTax = new DataTable();
                if (Session["TAXCOMPONENTDETAILS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS"];
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
                #region grdAddDespatch DataBind
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                #endregion
            }

            #region Amount-Calculation
            if (ds.Tables[3].Rows.Count > 0)
            {
                //this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtothchrg.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[3].Rows[0]["OTHERCHARGESVALUE"].ToString());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()));
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtNetAmt.Text)));

                ViewState["NETAMT"] = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtFinalAmt.Text.Trim()))));
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

    # region ControlDisable
    private void ControlDisable()
    {
        this.ImageButton1.Enabled = false;
        this.ddlTPU.Enabled = false;
        this.ddlVendorFrom.Enabled = false;
        this.ddlGatePassNo.Enabled = false;
        this.ddlTransporter.Enabled = false;
        this.ddlTransportMode.Enabled = false;
        this.txtVehicle.Enabled = false;
        this.txtLRGRNo.Enabled = false;
        this.imgbtnLRGRCalendar.Enabled = false;
        this.ddlWaybill.Enabled = false;
        this.txtInvoiceNo.Enabled = false;
        this.imgbtnInvoiceCalendar.Enabled = false;
        
        this.Imgbtngatepass.Enabled = false;
        this.ddlinsurancecompname.Enabled = false;
        this.ddlInsuranceNumber.Enabled = false;
        this.chkActive.Enabled = false;
        this.trpodetails.Style["display"] = "none";
        this.grdAddDespatch.Columns[0].Visible = false;
        this.grdAddDespatch.Columns[1].Visible = false;
        this.txtRemarks.Enabled = false;
        this.btnsubmitdiv.Style["display"] = "none";
    }
    # endregion

    # region ControlEnable
    private void ControlEnable()
    {

        this.ImageButton1.Enabled = true;
        this.ddlTPU.Enabled = true;
        this.ddlVendorFrom.Enabled = true;
        this.ddlTransporter.Enabled = true;
        this.ddlTransportMode.Enabled = true;
        this.txtVehicle.Enabled = true;
        this.txtLRGRNo.Enabled = true;
        this.imgbtnLRGRCalendar.Enabled = true;
        this.ddlWaybill.Enabled = true;
        this.txtInvoiceNo.Enabled = true;
        this.imgbtnInvoiceCalendar.Enabled = true;
       
        this.Imgbtngatepass.Enabled = true;
        this.ddlinsurancecompname.Enabled = true;
        this.ddlInsuranceNumber.Enabled = true;
        this.chkActive.Enabled = true;
        this.trpodetails.Style["display"] = "";
        this.grdAddDespatch.Columns[0].Visible = true;
        this.grdAddDespatch.Columns[1].Visible = true;
        this.txtRemarks.Enabled = true;
        this.btnsubmitdiv.Style["display"] = "";
    }
    # endregion

    #region txtothchrg_TextChanged
    protected void txtothchrg_TextChanged(object sender, EventArgs e)
    {
        if (txtothchrg.Text != "" && txtothchrg.Text != "0.00")
        {
            decimal OthChrg = Convert.ToDecimal(txtothchrg.Text == "" ? "0.00" : txtothchrg.Text) + Convert.ToDecimal(ViewState["NETAMT"]);//txtFinalAmt.Text);
            txtFinalAmt.Text = Convert.ToString(OthChrg);
        }
        else if (txtothchrg.Text == "" || txtothchrg.Text == "0.00")
        {
            txtothchrg.Text = "0.00";
            txtFinalAmt.Text = ViewState["NETAMT"].ToString();
        }
    }
    #endregion

    #region Freight Charge Calculation
    protected void txtfreight_TextChanged(object sender, EventArgs e)
    {
        if (txtfreight.Text != "" && txtfreight.Text != "0.00")
        {
            decimal OthChrg = Convert.ToDecimal(txtfreight.Text == "" ? "0.00" : txtfreight.Text) + Convert.ToDecimal(ViewState["NETAMT"]);//txtFinalAmt.Text);
            txtFinalAmt.Text = Convert.ToString(OthChrg);
        }
        else if (txtfreight.Text == "" || txtfreight.Text == "0.00")
        {
            txtfreight.Text = "0.00";
            txtFinalAmt.Text = ViewState["NETAMT"].ToString();
        }
    }
    #endregion

    #region Approval Code
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransporter.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Select Transpoter</font></b>");
            }
            else if (ddlledger.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Select Ledger Name</font></b>");
            }
            else if (Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) >= 50000 && this.txtwaybilno.Text.Trim() == "" && this.txtwaybilldate.Text.Trim() == "")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Way Bill Number Required!</font></b>");
                return;
            }
            else if (Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) >= 50000 && this.txtwaybilno.Text.Trim() != "" && this.txtwaybilldate.Text.Trim() == "")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Way Bill Date Required!</font></b>");
                return;
            }
            else
            {
                this.ControlEnable();
                int CountTerms = 0;
                string DespatchtNo = string.Empty;
                string xml = string.Empty;
                string xmlTax = string.Empty;
                string xmlGrossTax = string.Empty;
                string strTaxID = string.Empty;
                string strTaxPercentage = string.Empty;
                string strTaxValue = string.Empty;
                string strTermsID = string.Empty;
                string allocationDate = string.Empty;
                string f_formactive = string.Empty;
                string xmlRejectionDetails = string.Empty;
                string xmlRejectionTaxDetails = string.Empty;
                string xmlJobOrderReceive = string.Empty;
                string xmlSampleQty = string.Empty;

                DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS"];
                DataTable dtTaxRecordsCheck = (DataTable)HttpContext.Current.Session["TAXCOMPONENTDETAILS"];
                DataTable dtRejectionDetailsTotal = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];
                //DataTable dtRejectionDetailsTotal = (DataTable)HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"];
                DataTable dtAddnDetails = (DataTable)HttpContext.Current.Session["ADDN_DETAILS"];
                DataTable dtRejectionTaxRecordsCheck = new DataTable();
                DataTable dtJobOrderReceive = (DataTable)HttpContext.Current.Session["JOBORDER_RECEIVE"];
                DataTable dtSample = (DataTable)HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"];

                if (Session["REJECTIONTAXCOMPONENTDETAILS"] != null)
                {
                    dtRejectionTaxRecordsCheck = (DataTable)HttpContext.Current.Session["REJECTIONTAXCOMPONENTDETAILS"];
                }
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
                    if (dtRejectionDetailsTotal != null)
                    {
                        if (dtRejectionDetailsTotal.Rows.Count > 0)
                        {
                            xmlRejectionDetails = ConvertRejectionDatatableToXML(dtRejectionDetailsTotal);
                        }
                    }

                    if (dtRejectionTaxRecordsCheck != null)
                    {
                        if (dtRejectionTaxRecordsCheck.Rows.Count > 0)
                        {
                            xmlRejectionTaxDetails = ConvertTaxRejectionDatatableToXML(dtRejectionTaxRecordsCheck);
                        }
                    }

                    if (dtJobOrderReceive != null)
                    {
                        if (dtJobOrderReceive.Rows.Count > 0)
                        {
                            xmlJobOrderReceive = ConvertDatatableToXML(dtJobOrderReceive);
                        }
                    }

                    if (dtSample != null)
                    {
                        if (dtSample.Rows.Count > 0)
                        {
                            xmlSampleQty = ConvertDatatableToXML(dtSample);
                        }
                    }

                    string CapacityFileUpload = string.Empty;
                    string xmlSampleQtyFileUpload = string.Empty;
                    DataTable dtSampleQty = new DataTable();
                    if (ChkCapacity.Checked)
                    {
                        dtSampleQty = (DataTable)(Session["UPLOADCAPACITYFILE"]);
                        xmlSampleQtyFileUpload = ConvertDatatableToXML(dtSampleQty);
                        CapacityFileUpload = "Y";
                    }
                    else
                    {
                        CapacityFileUpload = "N";
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
                    decimal ADDNAMOUNT = 0;
                    decimal Roundoff = Convert.ToDecimal(this.txtRoundoff.Text);
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
                    decimal TotalDespatch = Convert.ToDecimal(this.txtFinalAmt.Text);

                    #endregion

                    int dtdespatch = Convert.ToInt32(Conver_To_ISO(this.txtDespatchDate.Text.Trim()));
                    int dtInvoice = Convert.ToInt32(Conver_To_ISO(this.txtInvoiceDate.Text.Trim()));
                    int dtLRGR = Convert.ToInt32(Conver_To_ISO(this.txtLRGRDate.Text.Trim()));
                    //string LRGR = clsgrnmm.CheckLRGR(this.txtLRGRNo.Text.Trim(), this.hdnDespatchID.Value.Trim());
                    string INVOICE = clsgrnmm.CheckInvoiceNoGRN(this.txtInvoiceNo.Text, this.ddlTPU.SelectedValue.ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), hdnDespatchID.Value);
                    if (this.chkActive.Checked == true)
                    {
                        f_formactive = "Y";
                    }
                    else
                    {
                        f_formactive = "N";
                    }

                    /*if (LRGR == "1")
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>LR/GR No already exists..!</font></b>");
                    }
                    else*/
                    /*if (INVOICE == "1")
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Invoice No already exists..!</font></b>");
                    }
                    else
                    {*/
                    DespatchtNo = clsgrnmm.InsertDespatchDetails(this.txtDespatchDate.Text.Trim(), this.ddlTPU.SelectedValue.Trim(), this.ddlTPU.SelectedItem.ToString().Trim(),
                                                                 this.txtwaybilno.Text, this.txtInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(),
                                                                 this.ddlTransporter.SelectedValue, this.txtVehicle.Text.Trim(), this.ddlDepot.SelectedValue,
                                                                 Convert.ToString(this.ddlDepot.SelectedItem), this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                                                                 this.ddlTransportMode.SelectedValue, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                                                                 HttpContext.Current.Session["FINYEAR"].ToString(), this.txtRemarks.Text.Trim(), TotalDespatch, OtherCharges,
                                                                 Convert.ToDecimal(txtOtherCharge.Text), Roundoff, strTermsID, "", "",
                                                                 this.ddlGatePassNo.SelectedValue, this.txtgatepassdate.Text.Trim(), f_formactive.Trim(),
                                                                 xml, xmlTax, "", xmlRejectionDetails, "", ADDNAMOUNT, Convert.ToString(hdnDespatchID.Value),
                                                                 this.ddlinsurancecompname.SelectedValue.Trim(), this.ddlinsurancecompname.SelectedItem.ToString().Trim(),
                                                                 this.ddlInsuranceNumber.SelectedValue.Trim(), menuID, Convert.ToString(this.hdnReceivedID.Value),
                                                                 Convert.ToInt16(ViewState["Invoice_Type"]), Convert.ToString(HDNISVERIFIEDCHECKER1.Value),
                                                                 "Y", xmlRejectionTaxDetails, Convert.ToDecimal(this.lblTotalItemWiseFreight.Text),
                                                                 Convert.ToDecimal(this.lblTotalItemWiseAddCost.Text), Convert.ToDecimal(this.lblTotalItemWiseDist.Text),
                                                                 xmlJobOrderReceive, ddlledger.SelectedValue.ToString().Trim(), this.txtwaybilldate.Text, xmlSampleQty,
                                                                 xmlSampleQtyFileUpload, CapacityFileUpload, ddlVendorFrom.SelectedValue.ToString(), Convert.ToDecimal(this.txtTCSPercent.Text),
                                                                     Convert.ToDecimal(this.txtTCS.Text), Convert.ToDecimal(this.txtTCSNetAmt.Text), Convert.ToDecimal(this.txtTCSApplicable.Text), this.ddlStorelocation.SelectedValue, txtDevationno.Text, TxtDevationdate.Text);
                    if (DespatchtNo != "")
                    {
                        grdAddDespatch.DataSource = null;
                        grdAddDespatch.DataBind();

                        if (Convert.ToString(hdnDespatchID.Value) == "")
                        {
                            MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b>  Saved Successfully", 60, 550);
                            this.LoadGRN();
                            pnlAdd.Style["display"] = "none";
                            pnlDisplay.Style["display"] = "";
                            btnaddhide.Style["display"] = "";
                        }
                        else
                        {
                            MessageBox1.ShowSuccess("GRN No :  <b><font color='green'>" + DespatchtNo + "</font></b> Updated Successfully", 60, 550);
                            this.LoadGRN();
                            pnlAdd.Style["display"] = "none";
                            pnlDisplay.Style["display"] = "";
                            btnaddhide.Style["display"] = "";
                            DataTable dt = new DataTable();
                            ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                            dt = clsMMPo.Bind_Sms_Mobno("G", Convert.ToString(hdnDespatchID.Value));
                            foreach (DataRow row in dt.Rows)
                            {
                                this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                            }
                            DataTable dt1 = new DataTable();
                            dt1 = clsMMPo.Bind_Sms_Mobno("H", Convert.ToString(hdnDespatchID.Value));

                        }

                        this.hdnDespatchID.Value = "";
                        this.ResetSession();
                        this.ClearControls();
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    }
                    //}
                    // }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Please add atleast 1 record</b>");
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

    #region btnreject_Click
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            ClsGRNMM GrnMM = new ClsGRNMM();
            int flag = 0;
            flag = GrnMM.RejectGrn(Convert.ToString(hdnDespatchID.Value), HttpContext.Current.Session["UserID"].ToString(), "1"); /* 1 for reject*/
            if (flag == 2)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                MessageBox1.ShowSuccess("QC: <b><font color='green'></font></b> Reject successful.", 60, 500);
                return;
            }
            else
            {
                MessageBox1.ShowError("QC: <b><font color='red'></font></b> Reject unsuccessful.", 60, 500);
                return;
            }

        }
        catch
        {
        }
    }
    #endregion

    #region Show Document From QC Control
    protected void btnDocuments_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtQC = new DataTable();
            dtQC = clsgrnmm.FetchQCID(hdnDespatchID.Value.Trim());
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

    #region Show Document From QC Control
    protected void btnGrnSample_Click(object sender, EventArgs e)
    {
        try
        {
            string StockReceivedID = hdnDespatchID.Value;
            string strPopup = string.Empty;
            if (StockReceivedID.Trim() != "" && ViewState["OP"].ToString() == "QC")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmStockinSampleQty_FAC.aspx?RECEIVEDID=" + StockReceivedID + "&MODE=StockIn"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else if (StockReceivedID.Trim() != "" && ViewState["OP"].ToString() == "Checker1")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmStockinSampleQty_FAC.aspx?RECEIVEDID=" + StockReceivedID + "&MODE=Checker1"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmStockinSampleQty_FAC.aspx?RECEIVEDID= "
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

    #region LoadSundry
    public void LoadSundry()
    {
        try
        {
            ClsGRNMM GrnMM = new ClsGRNMM();
            this.ddlsundry.Items.Clear();
            this.ddlsundry.Items.Add(new ListItem("Select", "0"));
            this.ddlsundry.AppendDataBoundItems = true;
            this.ddlsundry.DataSource = GrnMM.SundryDetails(Session["menuID"].ToString().Trim());
            this.ddlsundry.DataValueField = "DETAILSID";
            this.ddlsundry.DataTextField = "DETAILSNAME";
            this.ddlsundry.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlsundry_SelectedIndexChanged
    protected void ddlsundry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsGRNMM GrnMM = new ClsGRNMM();
        DataTable dt = GrnMM.TaxPercentage(ddlsundry.SelectedValue.Trim());
        if (dt.Rows.Count > 0)
        {
            txtamt.Text = Convert.ToString(String.Format("{0:0.00}", (Convert.ToDecimal(dt.Rows[0]["PERCENTAGE"].ToString().Trim()) / 100) * Convert.ToDecimal(txtTotalGross.Text)));
            txtpercent.Text = Convert.ToString(String.Format("{0:0.00}", (Convert.ToDecimal(dt.Rows[0]["PERCENTAGE"].ToString().Trim()))));
            lblledgerid.Text = dt.Rows[0]["REFERENCELEDGERID"].ToString().Trim();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtamt.ClientID + "').focus(); ", true);
        }
        else
        {
            txtpercent.Text = "0";
            txtamt.Text = "0";
        }
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

    #region btntaxadd_Click
    protected void btntaxadd_Click(object sender, EventArgs e)
    {
        int chkTax = 0;
        DataTable dtadd = new DataTable();

        if (HttpContext.Current.Session["ADDN_DETAILS"] == null)
        {
            //CreateDataTable_TAX();
        }
        if (HttpContext.Current.Session["ADDN_DETAILS"] != null)
        {
            dtadd = (DataTable)HttpContext.Current.Session["ADDN_DETAILS"];
        }

        chkTax = dtadd.Select("TAXID='" + Convert.ToString(this.ddlsundry.SelectedValue.Trim()) + "'").Length;
        if (chkTax > 0)
        {
            MessageBox1.ShowInfo("Already Exists...");
            return;
        }
        else
        {
            DataRow dr = dtadd.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["TAXID"] = Convert.ToString(this.ddlsundry.SelectedValue.Trim());
            dr["TAXNAME"] = Convert.ToString(this.ddlsundry.SelectedItem).Trim();
            dr["PERCENTAGE"] = Convert.ToString(this.txtpercent.Text).Trim();
            dr["AMOUNT"] = Convert.ToString(this.txtamt.Text).Trim();
            dr["LEDGERID"] = Convert.ToString(this.lblledgerid.Text).Trim();
            dtadd.Rows.Add(dr);
            dtadd.AcceptChanges();

            ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) + double.Parse(this.txtamt.Text.ToString());
        }
        HttpContext.Current.Session["ADDN_DETAILS"] = dtadd;
        if (dtadd.Rows.Count > 0)
        {
            gvadd.DataSource = dtadd;
            gvadd.DataBind();
        }
        else
        {
            gvadd.DataSource = null;
            gvadd.DataBind();
        }
        //txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtaddnamt.Text) + Convert.ToDecimal(txtNetAmt.Text)));
        txtaddnamt.Text = String.Format("{0:0.00}", ViewState["SumTotal"].ToString().Trim());
        txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtaddnamt.Text) + Convert.ToDecimal(txtTotalGross.Text))));
        txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtFinalAmt.Text) - (Convert.ToDecimal(txtaddnamt.Text) + Convert.ToDecimal(txtTotalGross.Text))));
    }
    #endregion

    #region btn_TempDeletetax_Click
    protected void btn_TempDeletetax_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblguid = (Label)gvr.FindControl("lbl1");
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["ADDN_DETAILS"];

            DataRow[] drr = dtdeleterecord.Select("GUID='" + lblguid.Text + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                ViewState["SumTotal"] = double.Parse(ViewState["SumTotal"].ToString()) - double.Parse(drr[i]["AMOUNT"].ToString());
                txtaddnamt.Text = String.Format("{0:0.00}", ViewState["SumTotal"].ToString().Trim());
                txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtNetAmt.Text)));
                txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtaddnamt.Text) + Convert.ToDecimal(txtNetAmt.Text))));
                txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtFinalAmt.Text) - (Convert.ToDecimal(txtaddnamt.Text) + Convert.ToDecimal(txtTotalGross.Text))));

                drr[i].Delete();
                dtdeleterecord.AcceptChanges();
            }
            this.gvadd.DataSource = dtdeleterecord;
            this.gvadd.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedID = Convert.ToString(hdnDespatchID.Value);
            if (ViewState["OP"].ToString() == "Checker1")
            {
                //string upath = "frmPrintPopUp.aspx?Stnid=" + hdn_transferid.Value.Trim() + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=" + Request.QueryString["MenuId"].ToString() + " ";
                string path = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "&&MenuId=" + Request.QueryString["MenuId"].ToString() + "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
            }
            else
            {
                string path = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region GSTGridCalculation
    protected void GridCalculation()
    {
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "PO NO";
        this.grdAddDespatch.HeaderRow.Cells[4].Text = "PO DATE";
        this.grdAddDespatch.HeaderRow.Cells[6].Text = "PO QTY";
        this.grdAddDespatch.HeaderRow.Cells[7].Text = "HSN CODE";
        this.grdAddDespatch.HeaderRow.Cells[9].Text = "PRODUCT NAME";
        this.grdAddDespatch.HeaderRow.Cells[11].Text = "PACK SIZE";
        this.grdAddDespatch.HeaderRow.Cells[14].Text = "RECEIVED QTY";
        this.grdAddDespatch.HeaderRow.Cells[15].Text = "REMAINING QTY";
        this.grdAddDespatch.HeaderRow.Cells[18].Text = "DISC(%)";
        this.grdAddDespatch.HeaderRow.Cells[19].Text = "DISC RATE";
        this.grdAddDespatch.HeaderRow.Cells[20].Text = "AFTERDISCOUNT AMT";
        this.grdAddDespatch.HeaderRow.Cells[21].Text = "ITEMWISE FREIGHT";
        this.grdAddDespatch.HeaderRow.Cells[22].Text = "AFTERITEMWISE FREIGHTAMT";
        this.grdAddDespatch.HeaderRow.Cells[23].Text = "ITEMWISE ADDCOST";
        this.grdAddDespatch.HeaderRow.Cells[24].Text = "AFTERITEMWISE ADDCOSTAMT";
        this.grdAddDespatch.HeaderRow.Cells[26].Text = "QC REJECTIONQTY";
    }
    #endregion

    #region CreateDataTable_JobOrderReceive
    private void CreateDataTable_JOBORDER()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("STOCKDESPATCHID", typeof(string)));
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("POQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("ISSUEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("DISPATCHQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIVEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("WORKORDERPRODUCTID", typeof(string)));
        HttpContext.Current.Session["JOBORDER_RECEIVE"] = dt;
    }
    #endregion

    #region CreateDataTable_JobOrderReceive_Temp
    private void CreateDataTable_JOBORDER_TEMP()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("POQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("ISSUEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("DISPATCHQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIVEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("WORKORDERPRODUCTID", typeof(string)));
        HttpContext.Current.Session["JOBORDER_RECEIVE_TEMP"] = dt;
    }
    #endregion

    protected void BtnDispatchSave_click(object sender, EventArgs e)
    {
        TextBox txtReceiveQty = new TextBox();
        decimal Min = 0;
        //Session.Remove("JOBORDER_RECEIVE");       
        //if (Session["JOBORDER_RECEIVE"] == null)
        //{
        //    CreateDataTable_JOBORDER();
        //}
        DataTable DtJoborderReceive = (DataTable)HttpContext.Current.Session["JOBORDER_RECEIVE"];
        DataTable DtJoborderReceive_TEMP = (DataTable)HttpContext.Current.Session["JOBORDER_RECEIVE_TEMP"];

        int numberOfRecords = 0;
        numberOfRecords = DtJoborderReceive.Select("POID = '" + this.ddlpo.SelectedValue.Trim() + "' AND WORKORDERPRODUCTID = '" + this.lblproductid.Text.Trim() + "'").Length;
        if (numberOfRecords > 0)
        {
            MessageBox1.ShowError("<b><font color='red'>Record already exists..!</font></b>");
            this.WorkOrderPopup.Show();
            this.ResetControls();
            return;
        }
        else
        {
            foreach (GridViewRow gvRow in gvJobOrderdetails.Rows)
            {
                decimal lblissueQTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblissueQTY")).Text);
                decimal lblDisptchQty = Convert.ToDecimal(((Label)gvRow.FindControl("lblDisptchQty")).Text);
                decimal lblBalanceQty = Convert.ToDecimal(((Label)gvRow.FindControl("lblBalanceQty")).Text);
                txtReceiveQty.Text = ((TextBox)gvRow.FindControl("txtReceiveQty")).Text.ToString();
                if (lblDisptchQty != 0)
                {
                    if (lblDisptchQty >= lblBalanceQty)
                    {
                        Min = lblBalanceQty;
                        MessageBox1.ShowInfo("Min Value is " + Min + "", 60, 350);
                        //return;

                        if (Min > 0 && Min >= Convert.ToDecimal(txttotalreceved.Text))
                        {
                            DataRow dr = DtJoborderReceive.NewRow();
                            dr["STOCKDESPATCHID"] = ((Label)gvRow.FindControl("lblStockdespatchID")).Text;
                            dr["POID"] = ((Label)gvRow.FindControl("lblPoID")).Text;
                            dr["CATID"] = ((Label)gvRow.FindControl("lblCategoryID")).Text;
                            dr["CATNAME"] = ((Label)gvRow.FindControl("lblCategoryName")).Text;
                            dr["PRODUCTID"] = ((Label)gvRow.FindControl("lblMaterialID")).Text;
                            dr["PRODUCTNAME"] = ((Label)gvRow.FindControl("lblMaterialName")).Text;
                            dr["POQTY"] = ((Label)gvRow.FindControl("lblPoQty")).Text;
                            dr["ISSUEQTY"] = ((Label)gvRow.FindControl("lblissueQTY")).Text;
                            dr["DISPATCHQTY"] = ((Label)gvRow.FindControl("lblDisptchQty")).Text;
                            dr["RECEIVEQTY"] = ((TextBox)gvRow.FindControl("txtReceiveQty")).Text;
                            dr["WORKORDERPRODUCTID"] = lblproductid.Text;

                            DtJoborderReceive.Rows.Add(dr);
                            DtJoborderReceive.AcceptChanges();


                            DataRow drTemp = DtJoborderReceive_TEMP.NewRow();
                            drTemp["POID"] = ((Label)gvRow.FindControl("lblPoID")).Text;
                            drTemp["PRODUCTID"] = ((Label)gvRow.FindControl("lblMaterialID")).Text;
                            drTemp["PRODUCTNAME"] = ((Label)gvRow.FindControl("lblMaterialName")).Text;
                            drTemp["POQTY"] = ((Label)gvRow.FindControl("lblPoQty")).Text;
                            drTemp["ISSUEQTY"] = ((Label)gvRow.FindControl("lblissueQTY")).Text;
                            drTemp["DISPATCHQTY"] = ((Label)gvRow.FindControl("lblDisptchQty")).Text;
                            drTemp["RECEIVEQTY"] = ((TextBox)gvRow.FindControl("txtReceiveQty")).Text;
                            drTemp["WORKORDERPRODUCTID"] = lblproductid.Text;

                            DtJoborderReceive_TEMP.Rows.Add(drTemp);
                            DtJoborderReceive_TEMP.AcceptChanges();

                        }
                        else
                        {
                            MessageBox1.ShowInfo("Receive Quantity Can not be greater than Balance Qty.", 60, 450);
                            this.WorkOrderPopup.Show();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Receive Quantity Can not be greater than Balance Qty.", 60, 450);
                    this.WorkOrderPopup.Show();
                    return;
                }
            }
        }
        /*foreach (GridViewRow gvRow in gvJobOrderdetails.Rows)
        {
            if (Min > 0 && Min >= Convert.ToDecimal(txttotalreceved.Text))
            {
                DataRow dr = DtJoborderReceive_TEMP.NewRow();               
                dr["POID"] = ((Label)gvRow.FindControl("lblPoID")).Text;               
                dr["PRODUCTID"] = ((Label)gvRow.FindControl("lblMaterialID")).Text;
                dr["PRODUCTNAME"] = ((Label)gvRow.FindControl("lblMaterialName")).Text;
                dr["POQTY"] = ((Label)gvRow.FindControl("lblPoQty")).Text;
                dr["ISSUEQTY"] = ((Label)gvRow.FindControl("lblissueQTY")).Text;
                dr["DISPATCHQTY"] = ((Label)gvRow.FindControl("lblDisptchQty")).Text;
                dr["RECEIVEQTY"] = ((TextBox)gvRow.FindControl("txtReceiveQty")).Text;

                DtJoborderReceive_TEMP.Rows.Add(dr);
                DtJoborderReceive_TEMP.AcceptChanges();
            }
            else
            {
                MessageBox1.ShowInfo("Receive Quantity Can not be greater than Balance Qty.", 60, 450);
                this.WorkOrderPopup.Show();
                return;
            }
        }*/
        if (DtJoborderReceive.Rows.Count > 0)
        {
            string XmlJobOrderDisptch = ConvertDatatableToXML(DtJoborderReceive_TEMP);
            string JobOrder = clsgrnmm.InsertJobOrderReceived(lblJobdespatchID.Text, ddlpo.SelectedValue.ToString(), XmlJobOrderDisptch);
            if (JobOrder == "1")
            {
                HttpContext.Current.Session["JOBORDER_RECEIVE"] = DtJoborderReceive;
                MessageBox1.ShowSuccess("JOB ORDER RECEIVE : <b><font color='green'></font></b>Successfully", 60, 350);
                txtDespatchQty.Text = txttotalreceved.Text;
                txtReceive.Text = "0.00";
                hdnJobOrderReceived.Value = txtDespatchQty.Text;
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Error on Add record..</font></b>");
            }
            //ddlpo.Enabled = false;

            /*string XmlJobOrderDisptch = ConvertDatatableToXML(DtJoborderReceive);
            string JobOrder = clsgrnmm.InsertJobOrderReceived(lblJobdespatchID.Text, ddlpo.SelectedValue.ToString(), XmlJobOrderDisptch);
            if (Convert.ToString(JobOrder) != "")
            {
                MessageBox1.ShowSuccess("JOB ORDER RECEIVE : <b><font color='green'></font></b> Saved Successfully", 60, 350);
                txtDespatchQty.Text = txtReceiveQty.Text;
                txtReceiveQty.Text = "";
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
            }*/
        }
        else
        {
            MessageBox1.ShowError("<b><font color='red'>Error on Add record..</font></b>");
        }
    }
    private void TotalReceive()
    {
        foreach (GridViewRow gvRow in gvJobOrderdetails.Rows)
        {
            decimal ReceiveQty = 0;
            decimal TotalReceiveQty = 0;
            TextBox txtReceiveQty = (TextBox)gvRow.FindControl("txtReceiveQty");
            ReceiveQty = Convert.ToDecimal(txtReceiveQty.Text);
            TotalReceiveQty = Convert.ToDecimal(txttotalreceved.Text);
            TotalReceiveQty += ReceiveQty;
            txttotalreceved.Text = Convert.ToString(TotalReceiveQty);
        }
    }
    protected void txtReceive_TextChanged(object sender, EventArgs e)
    {
       
        decimal despatchQty = 0;
        string orginalReceived = Convert.ToString(txtReceive.Text);
        string productId = this.ddlproduct.SelectedValue;
        string Id = productId.Substring(0, 36);
        ClsWOOrder obj = new ClsWOOrder();
        DataTable objDt = new DataTable();
        string ProductWithQty = string.Empty;
        ProductWithQty += Id + '|' + orginalReceived + ',';

        objDt = obj.FetchBom(ProductWithQty, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserID"].ToString());
        if(objDt.Rows.Count> 0)
        {
            for (int i = 0; i < objDt.Rows.Count; i++)
            {
                TextBox txtTotal = (TextBox)gvJobOrderdetails.Rows[i].FindControl("txtReceiveQty");
                txtTotal.Text = Convert.ToString(objDt.Rows[i]["QTY"]);
            }
            foreach (GridViewRow gvRow in gvJobOrderdetails.Rows)
            {
                TextBox txtReceiveQty = (TextBox)gvRow.FindControl("txtReceiveQty");
                Label lblDisptchQty = (Label)gvRow.FindControl("lblDisptchQty");
                despatchQty = Convert.ToDecimal(lblDisptchQty.Text);
                txttotalreceved.Text = txtReceive.Text;
                this.WorkOrderPopup.Show();
            }
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
            CalendarExtenderToDate.StartDate = oDate;
            CalendarExtenderDespatchDate.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtDespatchDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
                CalendarExtenderDespatchDate.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtDespatchDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtenderDespatchDate.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Load PO MM
    public void LoadIssue()
    {
        try
        {
            /*ClsGRNMM grnmm = new ClsGRNMM();
            DataTable dt = grnmm.BindIssuePOWise(ddlpo.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                
                this.ddlissueno.Items.Clear();
                this.ddlissueno.Items.Add(new ListItem("Select Issue", "0"));
                this.ddlissueno.AppendDataBoundItems = true;
                this.ddlissueno.DataSource = dt;
                this.ddlissueno.DataValueField = "ISSUEID";
                this.ddlissueno.DataTextField = "ISSUENO";
                this.ddlissueno.DataBind();
            }
            else
            {
                this.ddlissueno.Items.Clear();
                this.ddlissueno.Items.Add(new ListItem("Select Issue", "0"));
            }*/
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
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

    #region LoadLedger
    public void FetchLedger()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlledger.Items.Clear();
        ddlledger.Items.Add(new ListItem("SELECT LEDGER NAME", "0"));
        ddlledger.AppendDataBoundItems = true;
        ddlledger.DataSource = ClsCommon.LoadLedger();
        ddlledger.DataValueField = "ID";
        ddlledger.DataTextField = "NAME";
        ddlledger.DataBind();
    }
    #endregion

    #region Close Auto PO
    public void PoAutoClose(string TPUID)
    {
        try
        {
            DataTable dt = clsgrnmm.PoCloseAuto(TPUID, HttpContext.Current.Session["FINYEAR"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString());
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void ChkCapacity_Check(object sender, EventArgs e)
    {
        try
        {
            if (ChkCapacity.Checked == true)
            {
                Session["UPLOADCAPACITYFILE"] = null;
                string strPopup = string.Empty;
                if (this.hdnDespatchID.Value.Trim() != "" && ViewState["OP"].ToString() == "QC")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmCapacityUpload_FAC.aspx?GRNID=" + hdnDespatchID.Value.Trim() + "&MODE=StockIn"
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else if (this.hdnDespatchID.Value.Trim() != "" && ViewState["OP"].ToString() == "Checker1")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmCapacityUpload_FAC.aspx?GRNID=" + hdnDespatchID.Value.Trim() + "&MODE=Checker1"
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmCapacityUpload_FAC.aspx?GRNID= "
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                divshowcapacity.Style["display"] = "";
            }
            else
            {
                /*divshowCOA.Style["display"] = "none";
                ClsMMPPQualityControl clsqc = new ClsMMPPQualityControl();
                clsqc.FileDelete(hdnqcid.Value.Trim());*/
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnshowcapacity_Click(object sender, EventArgs e)
    {
        try
        {
            string strPopup = string.Empty;
            if (this.hdnDespatchID.Value.Trim() != "" && ViewState["OP"].ToString() == "QC")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmCapacityUpload_FAC.aspx?GRNID=" + hdnDespatchID.Value.Trim() + "&MODE=StockIn"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else if (this.hdnDespatchID.Value.Trim() != "" && ViewState["OP"].ToString() == "Checker1")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmCapacityUpload_FAC.aspx?GRNID=" + hdnDespatchID.Value.Trim() + "&MODE=Checker1"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmCapacityUpload_FAC.aspx?GRNID= "
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

    protected void ddlVendorFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(this.ddlGatePassNo.SelectedValue=="0")
        {
            MessageBox1.ShowWarning("Please Select GatePassNo First");
            return;
        }
        if (ddlVendorFrom.SelectedValue == "P")
        {
            LoadTPU();
        }
        else if (ddlVendorFrom.SelectedValue == "J")
        {
            LoadJobOrderVendor();
            LoadIssueProduct();
        }
        else if (ddlVendorFrom.SelectedValue == "C")
        {
            LoadConsumableProduct();
        }
        else
        {
            this.ddlGatePassNo.SelectedValue = "0";
        }
    }

    public void LoadGatePassNo()
    {
        try
        {
            string receivedid = Convert.ToString(hdnDespatchID.Value);
            string mode = "";
            if (receivedid == "")
            {
                mode = "GatePassNo";
            }
            else
            {
                mode = "EditGatePass";
            }
            
            DataTable dt = new DataTable();
            dt = clsgrnmm.BindGatePassno(mode);
            if (dt.Rows.Count > 0)
            {
                this.ddlGatePassNo.Items.Clear();
                this.ddlGatePassNo.Items.Add(new ListItem("Select GATEPASSNO", "0"));
                this.ddlGatePassNo.AppendDataBoundItems = true;
                this.ddlGatePassNo.DataSource = dt;
                this.ddlGatePassNo.DataTextField = "GATEPASSNO";
                this.ddlGatePassNo.DataValueField = "GATEPASSID";
                this.ddlGatePassNo.DataBind();
            }
            else
            {
                this.ddlGatePassNo.Items.Clear();
                this.ddlGatePassNo.Items.Add(new ListItem("Select GATEPASSNO", "0"));
                MessageBox1.ShowInfo("No Pending GateEntry Avilable");

            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void ddlGatePassNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsVendor_TPU cls = new ClsVendor_TPU();
        string vendorfrom = string.Empty;
        vendorfrom = cls.BillStatusCheck("vendorfrom", this.ddlGatePassNo.SelectedValue.ToString(), "");
        if(vendorfrom=="C")
        {
            this.ddlVendorFrom.SelectedValue = "C";
        }
        else if(vendorfrom == "P")
        {
            this.ddlVendorFrom.SelectedValue = "P";
        }
        else if (vendorfrom == "J")
        {
            this.ddlVendorFrom.SelectedValue = "J";
        }
        else if (vendorfrom == "R")
        {
            this.ddlVendorFrom.SelectedValue = "R";
        }
        else
        {
            this.ddlVendorFrom.SelectedValue = "0";
        }
        LoadTPU();
    }

    public void LoadConsumableProduct()
    {
        try
        {
            string receivedid = Convert.ToString(hdnDespatchID.Value);
            string mode = "consumable";
            DataTable dt = new DataTable();
            dt = clsgrnmm.BindGatePassno(mode);
            if (dt.Rows.Count > 0)
            {
                this.ddlproduct.Items.Clear();
                this.ddlproduct.Items.Add(new ListItem("Select Product", "0"));
                this.ddlproduct.AppendDataBoundItems = true;
                this.ddlproduct.DataSource = dt;
                this.ddlproduct.DataTextField = "NAME";
                this.ddlproduct.DataValueField = "ID";
                this.ddlproduct.DataBind();
            }
            else
            {
                this.ddlproduct.Items.Clear();
                this.ddlproduct.Items.Add(new ListItem("Select Product", "0"));
               

            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public string gethsn(string md,string id)
    {
        string hsn = string.Empty;
        ClsVendor_TPU cls = new ClsVendor_TPU();
        hsn = cls.editGateEntryCheck(md, id);
        return hsn;

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