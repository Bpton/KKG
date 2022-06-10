using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
}

public partial class VIEW_frmRptGRNStatus : System.Web.UI.Page
{
    ClsGRNMM clsgrnmm = new ClsGRNMM();
    string menuID = string.Empty;
    string Checker = string.Empty;
    string FG = string.Empty;
    string OP = string.Empty;
    decimal TotalAmount = 0;
    decimal TotalTaxValue = 0;
    DataTable dtDespatchEdit = new DataTable();
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    decimal TotalGridAmount = 0;
    decimal AfterDiscAmt = 0;
    decimal AfterItemWiseFreightAmt = 0, AfterItemWiseAddCostAmt = 0;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlinvtype').multiselect({includeSelectAllOption: true});});</script>", false);
        if (!IsPostBack)
        {
            pnlDisplay.Style["display"] = "";
            pnlAdd.Style["display"] = "none";
            txtFromDate.Style.Add("color", "black !important");
            txtToDate.Style.Add("color", "black !important");
            /*this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');*/
            this.LoadGRN();
            this.LoadMotherDepot();
            this.DateLock();
            this.LoadGrnProductList();
            lblpagename.Text = "GRN STATUS";
            Session["GRNSTATUS"] = null;
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

        foreach (ListItem item in ddlproduct.Items)
        {
            if (item.Value == "1")
            {
                item.Attributes.Add("disabled", "disabled");
                item.Attributes.CssStyle.Add("color", "blue");
                item.Attributes.CssStyle.Add("background-color", "Beige");
            }
        }

        OnPageLoadGRN();

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>Tax_MakeStaticHeader('" + gvProductTax.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region Load PO MM
    public void LoadPoMM(string TPUID)
    {
        try
        {
            ClsGRNMM grnmm = new ClsGRNMM();
            DataTable dt = grnmm.BindPOMM(TPUID, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
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
                {
                    this.ddlTransporter.SelectedValue = Convert.ToString(dtTransporter.Rows[0]["ID"]);
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
            FG = "FALSE";
            this.ddlTPU.Items.Clear();
            this.ddlTPU.Items.Add(new ListItem("Select TPU/Vendor", "0"));
            this.ddlTPU.AppendDataBoundItems = true;
            this.ddlTPU.DataSource = clsgrnmm.BindTPU(FG, HttpContext.Current.Session["DEPOTID"].ToString());
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
                this.ddlDepot.SelectedValue = HttpContext.Current.Session["DEPOTID"].ToString();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region OnPageLoadGRN
    public void OnPageLoadGRN()
    {
        try
        {
            ClsGRNMM clsGRN = new ClsGRNMM();
            DataTable dtGrn = new DataTable();

            // added by HPDAS for filtering GRN Status
            string INVTYPEID = string.Empty;
            INVTYPEID = "1,2,3,4,5,6,";

            if (INVTYPEID.Length > 0)
            {
                INVTYPEID = INVTYPEID.Substring(0, INVTYPEID.Length - 1);
            }
            else
            {
                INVTYPEID = "0";
            }
            dtGrn = clsGRN.FetchGRNStatusV2(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(),
                                            HttpContext.Current.Session["FINYEAR"].ToString(), INVTYPEID.ToString().Trim(),
                                            ddlreportype.SelectedValue.ToString(), ddlSearchproduct.SelectedValue.ToString());
            if (dtGrn.Rows.Count > 0)
            {
                this.grdDespatchHeader.DataSource = dtGrn;
                this.grdDespatchHeader.DataBind();
                Session["GRNSTATUS"] = dtGrn;
            }
            else
            {
                this.grdDespatchHeader.DataSource = null;
                this.grdDespatchHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadGRN
    public void LoadGRN()
    {
        try
        {
            ClsGRNMM clsGRN = new ClsGRNMM();
            DataTable dtGrn = new DataTable();

            // added by HPDAS for filtering GRN Status
            string INVTYPEID = string.Empty;
            var QueryInvType = from ListItem item in ddlinvtype.Items where item.Selected select item;
            foreach (ListItem item in QueryInvType)
            {
                // item ...
                INVTYPEID += item.Value + ",";
            }
            if (INVTYPEID.Length > 0)
            {
                INVTYPEID = INVTYPEID.Substring(0, INVTYPEID.Length - 1);
            }
            else
            {
                INVTYPEID = "0";
            }            
            dtGrn = clsGRN.FetchGRNStatusV2(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), 
                                            HttpContext.Current.Session["FINYEAR"].ToString(), INVTYPEID.ToString().Trim(),ddlreportype.SelectedValue.ToString(),
                                            ddlSearchproduct.SelectedValue.ToString());
            if (dtGrn.Rows.Count > 0)
            {
                this.grdDespatchHeader.DataSource = dtGrn;
                this.grdDespatchHeader.DataBind();
                Session["GRNSTATUS"] = dtGrn;
            }
            else
            {
                this.grdDespatchHeader.DataSource = null;
                this.grdDespatchHeader.DataBind();
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
            ClsGRNMM ClsGRN = new ClsGRNMM();
            req = ClsGRN.CFORM(FromVendorID, ToDepotID);
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
        //this.txtWeight.Text = "";
        //this.txtRate.Text = "";
        //this.txtMfDate.Text = "";
        //this.txtExprDate.Text = "";
        //this.txtMRP.Text = "";
        //this.txtPoQty.Text = "";
        //this.txtQcQty.Text = "";
        //this.txtAllocatedQty.Text = "";
        //this.txtAssementPercentage.Text = "";
        //this.txtTotalAssement.Text = "";
        //this.txtAlreadyDespatchQty.Text = "";
        //this.txtTotDespatch.Text = "";
        //this.txtDespatchQty.Text = "";
        //this.Label13.Text = "";
        if (ddlpo.SelectedValue != "0")
        {
            //this.LoadPurchaseOrderDetails();
            this.LoadProductDetails();
        }
        else
        {
            this.txtPoDate.Text = "";
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
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));                  //26
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
            Session["dtTaxCount_Status"] = dtTaxCount;
            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                //if (Convert.ToDecimal(dtTaxCount.Rows[k]["PERCENTAGE"].ToString()) > 0)
                //{
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
                //}
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsgrnmm.EditReceivedDetails(despatchID);
            Session["dtTaxCount_Status"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                //if (Convert.ToDecimal(ds.Tables[2].Rows[k]["PERCENTAGE"].ToString()) > 0)
                //{
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
                //}
            }
        }
        #endregion

        dt.Columns.Add(new DataColumn("NETWEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));
        HttpContext.Current.Session["DESPATCHDETAILS_STATUS"] = dt;
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
            HttpContext.Current.Session["TAXCOMPONENTDETAILS_STATUS"] = dt;
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
            HttpContext.Current.Session["TAXCOMPONENTDETAILS_STATUS"] = dt;
        }
        return dt;
    }
    #endregion

    #region LoadPurchaseOrderDetails
    protected void LoadPurchaseOrderDetails()
    {
        if (ddlproduct.SelectedValue == "0")
        {
            MessageBox1.ShowInfo("<b>Please select Product!</b>");
            return;
        }
        else
        {
            DataTable dtPoDetails = new DataTable();
            string packsizeID = ddlPackSize.SelectedValue;

            if (this.hdnDespatchID.Value == "")
            {
                dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), "", txtDespatchDate.Text, ddlTPU.SelectedValue.Trim());
            }
            else
            {
                dtPoDetails = clsgrnmm.BindPoDetails(this.ddlpo.SelectedValue.Trim(), lblproductid.Text.Trim(), packsizeID, this.ddlDepot.SelectedValue, Request.QueryString["FG"].ToString().Trim(), this.hdnDespatchID.Value.Trim(), txtDespatchDate.Text, ddlTPU.SelectedValue.Trim());
            }

            if (dtPoDetails.Rows.Count > 0)
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
                this.txtQcQty.Text = "";
                this.txtAlreadyDespatchQty.Text = "";
                this.txtPoQty.Text = "";
                this.txtAllocatedQty.Text = "";
                this.txtTotalAssement.Text = "";
            }
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
                GridDataControlFieldCell cell = e.Row.Cells[15] as GridDataControlFieldCell;
                GridDataControlFieldCell Nxtcell = e.Row.Cells[18] as GridDataControlFieldCell;
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
                    Nxtcell.ForeColor = Color.Green;
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
            e.Row.Controls[2].Visible = false;
            e.Row.Controls[3].Visible = false;
            e.Row.Controls[6].Visible = true;
            e.Row.Controls[8].Visible = false;
            e.Row.Controls[10].Visible = false;
            e.Row.Controls[12].Visible = false;
            e.Row.Controls[13].Visible = false;
            e.Row.Controls[25].Visible = false;
            e.Row.Controls[27].Visible = false;
            e.Row.Controls[28].Visible = false;
            e.Row.Controls[29].Visible = false;

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
            DataTable dt = (DataTable)Session["dtTaxCount_Status"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                e.Row.Cells[count].Wrap = false;
            }
            TotalGridAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
            AfterDiscAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERDISCOUNTAMT"));
            AfterItemWiseFreightAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERITEMWISEFREIGHTAMT"));
            AfterItemWiseAddCostAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AFTERITEMWISEADDCOSTAMT"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            int TotalRows = grdAddDespatch.Rows.Count;
            DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtTaxCount_Status"];
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
                e.Row.Cells[i - 1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (TotalGridAmount == 0)
            {
                e.Row.Cells[17].Text = "0.00";
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

            e.Row.Controls[2].Visible = false;
            e.Row.Controls[3].Visible = false;
            e.Row.Controls[6].Visible = false;
            e.Row.Controls[8].Visible = false;
            e.Row.Controls[10].Visible = false;
            e.Row.Controls[11].Visible = false;
            e.Row.Controls[23].Visible = false;
            //e.Row.Controls[25].Visible = false;
            e.Row.Controls[26].Visible = false;
            e.Row.Controls[27].Visible = false;
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

    #region CreateTaxDatatable
    void CreateTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string PRODUCTNAME, string Taxid)
    {
        DataTable dt = (DataTable)Session["TAXCOMPONENTDETAILS_STATUS"];
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
        //if (ddlproduct.SelectedValue != "0")
        //{
        //    this.LoadProductWisePacksize(this.ddlproduct.SelectedValue.Trim(), Request.QueryString["FG"].ToString().Trim());
        //}

        try
        {
            string[] sSplit = ddlproduct.SelectedValue.ToString().Split('~');
            string UNITID = sSplit[1];
            lblproductid.Text = sSplit[0];

            LoadPurchaseOrderDetails();

            if (ddlpo.SelectedValue != "0")
            {
                //this.ddlPackSize.SelectedItem.Text = ddlproduct.SelectedItem.Text.Substring(35, 14);
                lblproductname.Text = ddlproduct.SelectedItem.Text.Substring(0, 35);
                this.txtPoQty.Text = ddlproduct.SelectedItem.Text.Substring(49, 14);
                this.txtDespatchQty.Text = ddlproduct.SelectedItem.Text.Substring(63, 14).Trim();
                this.txtQcQty.Text = ddlproduct.SelectedItem.Text.Substring(77, 14).Trim();//Remaining Qty
                this.txtMRP.Text = ddlproduct.SelectedItem.Text.Substring(91, 14).Trim();
                //this.txtRate.Text = ddlproduct.SelectedItem.Text.Substring(105, 14).Trim();

                ddlPackSize.Items.Clear();
                ddlPackSize.Items.Add(new ListItem(ddlproduct.SelectedItem.Text.Substring(35, 14), UNITID));
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
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Load LoadProductDetails
    protected void LoadProductDetails()
    {
        if (ddlDepot.SelectedValue != "0")
        {
            //if (ddlPackSize.SelectedValue != "0")
            //{               
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            DataTable dt = new DataTable();
            //dt = clsDespatch.GetPO_QtyCombo(this.ddlproduct.SelectedValue.Trim(), this.ddlTPU.SelectedValue.Trim(), this.ddlPackSize.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim(), Request.QueryString["FG"].ToString().Trim());
            dt = clsgrnmm.BindPoWiseProduct(this.ddlpo.SelectedValue.Trim(), this.ddlpo.SelectedItem.ToString().Trim(), "");
            List<GRNdetails> PUDetails = new List<GRNdetails>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GRNdetails pu = new GRNdetails();
                    pu.POID = dt.Rows[i]["POID"].ToString();
                    pu.PONO = dt.Rows[i]["PONO"].ToString();
                    pu.PRODUCT_ID = dt.Rows[i]["PRODUCT_ID"].ToString();
                    if (dt.Rows[i]["PRODUCT_NAME"].ToString().Length < 35)
                    {
                        pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, dt.Rows[i]["PRODUCT_NAME"].ToString().Length);
                    }
                    else
                    {
                        pu.PRODUCTNAME = dt.Rows[i]["PRODUCT_NAME"].ToString().Substring(0, 35);
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
                "PRODUCTNAME".PadRight(35, '\u00A0'),
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
                    p.PRODUCTNAME.PadRight(35, '\u00A0'),
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
                ddlproduct.Items.Clear();
                ddlproduct.Items.Add(new ListItem("-- SELECT Product --", "0"));
                string text1 = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                "PRODUCTNAME".PadRight(35, '\u00A0'),
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
                    p.PRODUCTNAME.PadRight(35, '\u00A0'),
                    p.UNITNAME.PadRight(14, '\u00A0'),
                    p.POQTY.PadRight(14, '\u00A0'),
                    p.DESPATCHQTY.PadRight(14, '\u00A0'),
                    p.REMAININGQTY.PadRight(14, '\u00A0'),
                    p.MRP.PadRight(14, '\u00A0'),
                    p.RATE.PadRight(14, '\u00A0'));

                    ddlproduct.Items.Add(new ListItem(text, "" + p.PRODUCT_ID + "~" + p.UNITID + ""));
                }
                MessageBox1.ShowInfo("No Purchase Order is available with remaining quantity", 60, 550);
            }
            //}
            //else
            //{
            //    ddlproduct.Items.Clear();
            //    ddlproduct.Items.Add(new ListItem("Select PO/Indent No --", "0"));
            //    ddlproduct.AppendDataBoundItems = true;
            //}
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
        this.ddlpo.SelectedValue = "0";
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
        //this.Label13.Text = "";
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

    #region ResetSession
    public void ResetSession()
    {
        Session["DESPATCHDETAILS_STATUS"] = null;
        Session["GrossTotalTax_Status"] = null;
        Session["dtTaxCount_Status"] = null;
        Session["TAXCOMPONENTDETAILS_STATUS"] = null;
        Session["Terms"] = null;
        Session["REJECTIONDINNERGRIDDETAILS"] = null;
        Session["TOTALREJECTIONDETAILS"] = null;
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
        this.txtLRGRDate.Text = "";
        this.txtLRGRNo.Text = "";
        this.txtVehicle.Text = "";
        this.txtInvoiceDate.Text = "";
        this.txtInvoiceNo.Text = "";
        this.txtRemarks.Text = "";
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

        this.txtgatepassno.Text = "";
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
    }
    #endregion

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ControlEnable();
        this.trAutoDespatchNo.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
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
        this.LoadGRN();
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
            this.LoadTransporter();
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
    }
    #endregion

    #region FetchSuppliedItem
    protected void FetchSuppliedItem(string VendorID)
    {
        ClsGRNMM clsGRN = new ClsGRNMM();
        DataTable dtSuppliedItem = new DataTable();
        dtSuppliedItem = clsGRN.FetchSuppliedItem(VendorID);
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

        ClsGRNMM clsgrnmm = new ClsGRNMM();
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
        clsDespatchStock clsgrnmm = new clsDespatchStock();
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
        clsDespatchStock clsgrnmm = new clsDespatchStock();
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

    #region View GRN
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            ControlDisable();
            ClsGRNMM clsGRN = new ClsGRNMM();
            ClsGRNMM Clsdespatch = new ClsGRNMM();
            decimal TotalAmount = 0;
            decimal TotalTax = 0;
            decimal GrossTotal = 0;
            decimal TotalMRP = 0;
            string BRAND = string.Empty;
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            decimal ItemWiseFreight = 0;
            txtothchrg.Text = "0.00";

            DataSet ds = new DataSet();
            DataTable dtWaybillNo = new DataTable();
            this.trAutoDespatchNo.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            this.CreateDataTable();
            this.CreateDataTableTaxComponent();

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
                //this.LoadTPU();
                //this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);

                string VendorFrom = Convert.ToString(ds.Tables[0].Rows[0]["VENDORFROM"]);
                if (VendorFrom == "P")//From PO
                {
                    this.LoadTPU();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    //this.ddlTPU.SelectedValue = VendorFrom;
                }
                else//JOB ORDER
                {
                    LoadJobOrderVendor();
                    this.ddlTPU.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TPUID"]);
                    //this.ddlVendorFrom.SelectedValue = VendorFrom;                    
                }
                this.LoadMotherDepot();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MOTHERDEPOTID"]);
                this.LoadTransporter();
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtCFormNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFORMNO"]);
                this.BindInsurenceCompany();
                this.ddlinsurancecompname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCECOMPID"]).Trim();
                this.BindInsurenceNumber(this.ddlinsurancecompname.SelectedValue.Trim());
                this.ddlInsuranceNumber.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCENUMBER"]).Trim();
                lblInrRate.Text = "INR&nbsp;&nbsp;RATE:- " + Convert.ToString(ds.Tables[1].Rows[0]["INRRATE"]);
                lblExchangeRate.Text = "EXCHANGE&nbsp;RATE:- " + Convert.ToString(ds.Tables[1].Rows[0]["EXCHANGERATE"]);

                this.txtgatepassno.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtgatepassdate.Text = "";
                }
                else
                {
                    this.txtgatepassdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
                this.ddlWaybill.Items.Clear();
                this.ddlWaybill.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"]), Convert.ToString(ds.Tables[0].Rows[0]["WAYBILLKEY"])));
                this.ddlWaybill.AppendDataBoundItems = true;
                this.txtCheckerNote.Text = Convert.ToString(ds.Tables[0].Rows[0]["NOTE"]);
            }
            #endregion

            #region Item-wise Tax Component
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["TAXCOMPONENTDETAILS_STATUS"];
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

                HttpContext.Current.Session["TAXCOMPONENTDETAILS_STATUS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                #region Loop For Adding Itemwise Tax Component into Arry
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount_Status"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                DataTable dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS_STATUS"];
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
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]);
                    dr["DEPOTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["DEPOTRATE"]);
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                    dr["BATCHNO"] = "";//Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
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

                    dtDespatchEdit = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_STATUS"];
                }
                TotalMRP = CalculateTotalMRP(dtDespatchEdit);
                TotalAmount = CalculateGrossTotal(dtDespatchEdit);
                TotalTax = CalculateTaxTotal(dtDespatchEdit);
                GrossTotal = TotalAmount + TotalTax;
                ItemWiseFreight = CalculateTotalFreight(dtDespatchEdit);

                this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalTax));
                this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));

                #region Tax On Gross Amount
                DataTable dtGrossTax = (DataTable)Session["GrossTotalTax_Status"];
                #endregion

                #region grdAddDespatch DataBind

                HttpContext.Current.Session["DESPATCHDETAILS_STATUS"] = dtDespatchEdit;
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
                if (Session["TAXCOMPONENTDETAILS_STATUS"] != null)
                {
                    dtProductTax = (DataTable)Session["TAXCOMPONENTDETAILS_STATUS"];
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
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[3].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[3].Rows[0]["ROUNDOFFVALUE"].ToString())));
                ViewState["NETAMT"] = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtFinalAmt.Text.Trim()))));
            }
            #endregion

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
        this.ddlTransporter.Enabled = false;
        this.ddlTransportMode.Enabled = false;
        this.txtVehicle.Enabled = false;
        this.txtLRGRNo.Enabled = false;
        this.imgbtnLRGRCalendar.Enabled = false;
        this.ddlWaybill.Enabled = false;
        this.txtInvoiceNo.Enabled = false;
        this.imgbtnInvoiceCalendar.Enabled = false;
        this.txtgatepassno.Enabled = false;
        this.Imgbtngatepass.Enabled = false;
        this.ddlinsurancecompname.Enabled = false;
        this.ddlInsuranceNumber.Enabled = false;
        this.chkActive.Enabled = false;
        this.trpodetails.Style["display"] = "none";
        this.grdAddDespatch.Columns[0].Visible = false;
        this.grdAddDespatch.Columns[1].Visible = false;
        this.txtRemarks.Enabled = false;
    }
    # endregion

    #region ControlEnable
    private void ControlEnable()
    {
        this.ImageButton1.Enabled = true;
        this.ddlTPU.Enabled = true;
        this.ddlTransporter.Enabled = true;
        this.ddlTransportMode.Enabled = true;
        this.txtVehicle.Enabled = true;
        this.txtLRGRNo.Enabled = true;
        this.imgbtnLRGRCalendar.Enabled = true;
        this.ddlWaybill.Enabled = true;
        this.txtInvoiceNo.Enabled = true;
        this.imgbtnInvoiceCalendar.Enabled = true;
        this.txtgatepassno.Enabled = true;
        this.Imgbtngatepass.Enabled = true;
        this.ddlinsurancecompname.Enabled = true;
        this.ddlInsuranceNumber.Enabled = true;
        this.chkActive.Enabled = true;
        this.trpodetails.Style["display"] = "";
        this.grdAddDespatch.Columns[0].Visible = true;
        this.grdAddDespatch.Columns[1].Visible = true;
        this.txtRemarks.Enabled = true;
    }
    #endregion    

    #region grdDespatchHeader_Exporting
    protected void grdDespatchHeader_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        cell.ColumnSpan = 12;
        cell.BorderStyle = BorderStyle.None;
        cell.Text = "Grn Status Details";
        cell.BackColor = Color.Gray;
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.RowSpan = 1;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region grdDespatchHeader_Exported
    protected void grdDespatchHeader_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedID = Convert.ToString(hdnDespatchID.Value);
            string path = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "";//"&&MenuId=1636";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
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
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "PONO";
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
        this.grdAddDespatch.HeaderRow.Cells[26].Text = "BATCH NO";
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
        CalendarFromDate.StartDate = oDate;
        CalendarExtenderToDate.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarFromDate.EndDate = today1;
            CalendarExtenderToDate.EndDate = today1;
        }
        else
        {
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarFromDate.EndDate = cDate;
            CalendarExtenderToDate.EndDate = cDate;
        }
    }
    #endregion

    #region LoadGrnProductList
    public void LoadGrnProductList()
    {
        try
        {
            DataTable dtproduct = new DataTable();
            dtproduct = clsgrnmm.BindGrnProductList();
            this.ddlSearchproduct.Items.Clear();
            this.ddlSearchproduct.Items.Add(new ListItem("All", "0"));
            this.ddlSearchproduct.AppendDataBoundItems = true;
            if (dtproduct.Rows.Count > 0)
            {
                this.ddlSearchproduct.DataSource = dtproduct;
                this.ddlSearchproduct.DataValueField = "PRODUCTID";
                this.ddlSearchproduct.DataTextField = "PRODUCTALIAS";
                this.ddlSearchproduct.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion
}