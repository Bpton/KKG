using Account;
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
using WorkFlow;

public class Purchasedetails
{
    public string INVOICEID { get; set; }
    public string INVOICENO { get; set; }
    public string INVOICEDATE { get; set; }
    public string NETAMOUNT { get; set; }
}

public partial class VIEW_frmPurchaseReturn_MM : System.Web.UI.Page
{
    string menuID = string.Empty;
    decimal TotalAmount = 0;
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";

    #region Page_Load
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
                this.CalendarExtender3.Enabled = true;
            }
            else
            {
                this.ImageButton1.Enabled = false;
                this.CalendarExtender3.Enabled = false;
            }
            #endregion

            if (Request.QueryString["TYPE"] != "LGRREPORT")
            {
                #region QueryString
                string Checker = Request.QueryString["CHECKER"].ToString().Trim();
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    this.btnaddhide.Style["display"] = "none";
                    this.divsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.tdlblledger.Visible = true;
                    this.tdddlledger.Visible = true;
                }
                else
                {
                    this.btnaddhide.Style["display"] = "";
                    this.divsave.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.tdlblledger.Visible = false;
                    this.tdddlledger.Visible = false;
                }
                #endregion

                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                ViewState["menuID"] = menuID;
                this.LoadPurchaseReturn();
                this.FetchLedger();
                this.DateLock();
                this.LoadTransporter();
            }
            else
            {
                this.btnaddhide.Style["display"] = "none";
                this.divsave.Visible = false;
                this.divbtnCancel.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.tdlblledger.Visible = false;
                this.tdddlledger.Visible = false;
                Btn_View(Request.QueryString["InvId"]);
            }
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdinvoicedetails.ClientID + "', 450, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region ddlSuppliedItem_SelectedIndexChanged
    public void ddlSuppliedItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlSuppliedItem.SelectedValue != "0")
            {
                this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Bind Vendor
    protected void BindVendor(string SuppliedITemID)
    {
        if (this.ddlSuppliedItem.SelectedValue != "0")
        {
            DataTable dtVendor = new DataTable();
            ClsPurchaseReturn_MM clsreturn = new ClsPurchaseReturn_MM();
            dtVendor = clsreturn.BindVendor(SuppliedITemID);
            if (dtVendor.Rows.Count > 0)
            {
                if (dtVendor.Rows.Count > 1)
                {
                    ddlVendor.Items.Clear();
                    ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
                    ddlVendor.AppendDataBoundItems = true;
                    ddlVendor.DataSource = dtVendor;
                    ddlVendor.DataValueField = "VENDORID";
                    ddlVendor.DataTextField = "VENDORNAME";
                    ddlVendor.DataBind();
                }
                else if (dtVendor.Rows.Count == 1)
                {
                    ddlVendor.Items.Clear();
                    ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
                    ddlVendor.DataSource = dtVendor;
                    ddlVendor.DataValueField = "VENDORID";
                    ddlVendor.DataTextField = "VENDORNAME";
                    ddlVendor.DataBind();
                    ddlVendor.SelectedValue = Convert.ToString(dtVendor.Rows[0]["VENDORID"]);
                }
            }
            else
            {
                ddlVendor.Items.Clear();
                ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
                ddlVendor.AppendDataBoundItems = true;
            }

        }
        else
        {
            ddlVendor.Items.Clear();
            ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
            ddlVendor.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region LoadSuppliedItem
    public void LoadSuppliedItem()
    {
        ClsPurchaseReturn_MM ClsRetailerSaleOrder = new ClsPurchaseReturn_MM();
        DataTable dtSuppliedItem = new DataTable();
        dtSuppliedItem = ClsRetailerSaleOrder.BindSuppliedItem();
        if (dtSuppliedItem.Rows.Count > 0)
        {
            if (dtSuppliedItem.Rows.Count > 1)
            {
                this.ddlSuppliedItem.Items.Clear();
                this.ddlSuppliedItem.Items.Add(new ListItem("Select Supplied Item", "0"));
                this.ddlSuppliedItem.AppendDataBoundItems = true;
                this.ddlSuppliedItem.DataSource = dtSuppliedItem;
                this.ddlSuppliedItem.DataValueField = "SUPPLIEDITEMID";
                this.ddlSuppliedItem.DataTextField = "SUPPLIEDITEMNAME";
                this.ddlSuppliedItem.DataBind();
            }
            else if (dtSuppliedItem.Rows.Count == 1)
            {
                this.ddlSuppliedItem.Items.Clear();
                this.ddlSuppliedItem.Items.Add(new ListItem("Select Supplied Item", "0"));
                this.ddlSuppliedItem.DataSource = dtSuppliedItem;
                this.ddlSuppliedItem.DataValueField = "SUPPLIEDITEMID";
                this.ddlSuppliedItem.DataTextField = "SUPPLIEDITEMNAME";
                this.ddlSuppliedItem.DataBind();
                this.ddlSuppliedItem.SelectedValue = Convert.ToString(dtSuppliedItem.Rows[0]["SUPPLIEDITEMID"]);
            }
            if (this.ddlSuppliedItem.SelectedValue != "0")
            {
                this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());
            }
        }
        else
        {
            this.ddlSuppliedItem.Items.Clear();
            this.ddlSuppliedItem.Items.Add(new ListItem("Select Supplied Item", "0"));
            this.ddlSuppliedItem.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region LoadMotherDepot
    public void LoadMotherDepot()
    {
        try
        {
            ClsPurchaseReturn_MM clsreport = new ClsPurchaseReturn_MM();
            DataTable dtDepot = new DataTable();
            dtDepot = clsreport.BindDepotBasedOnUser(HttpContext.Current.Session["UserID"].ToString());

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
                }
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

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.Displayonoff("A");
        this.hdnDespatchID.Value = "";
        this.ClearControls();
        this.ResetSession();
        this.trAutoInvoiceNo.Style["display"] = "none";
        pnlDisplay.Style["display"] = "none";
        pnlAdd.Style["display"] = "";
        btnaddhide.Style["display"] = "none";
        this.LoadMotherDepot();
        this.LoadSuppliedItem();

        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = false;
        this.ddlVendor.Enabled = true;
        this.ddlSuppliedItem.Enabled = true;
        this.ddlGrnRejectionNo.Enabled = true;
        this.ddlReturnType.Enabled = true;
        #endregion

        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        //this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
        divsave.Style["display"] = "";
        this.DateLock();
        this.lblGrnRejectionNo.Visible = false;
        this.ddlGrnRejectionNo.Visible = false;
        this.ddlReturnType.SelectedValue = "N";
        this.ddlGrnRejectionNo.SelectedValue = "NA";
    }
    #endregion

    #region ddlDepot_SelectedIndexChanged
    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlDepot.SelectedValue != "0")
            {
                //this.BindTransporter(this.ddlDepot.SelectedValue.Trim());
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlReturnType_SelectedIndexChanged
    protected void ddlReturnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReturnType.SelectedValue == "N")
            {
                lblGrnRejectionNo.Visible = false;
                ddlGrnRejectionNo.Visible = false;
                divsearch.Visible = true;
                trinvoiceno.Visible = true;
                this.divsearch.Style["display"] = "";
                this.trinvoiceno.Style["display"] = ""; 
                this.ddlGrnRejectionNo.Items.Clear();
            }
            else
            {
                lblGrnRejectionNo.Visible = true;
                ddlGrnRejectionNo.Visible = true;
                this.divsearch.Style["display"] = "none";
                this.trinvoiceno.Style["display"] = "none";
                LoadGrnRejection();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlGrnRejectionNo_SelectedIndexChanged
    protected void ddlGrnRejectionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /*divsearch.Visible = false;
            trinvoiceno.Visible = false;*/
            this.grdinvoicedetails.DataSource = null;
            this.grdinvoicedetails.DataBind();
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataSet dsRejection = new DataSet();
            dsRejection = clsReturn.BindPurchaseInvoiceRejectionWise(ddlGrnRejectionNo.SelectedValue.ToString());
            if (dsRejection.Tables["HEADER"].Rows.Count > 0)
            {
                this.LoadSuppliedItem();
                this.ddlSuppliedItem.SelectedValue = Convert.ToString(dsRejection.Tables["HEADER"].Rows[0]["SUPPLIEDITEMID"]).Trim();
                this.ddlSuppliedItem.Enabled = false;
                this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());
                this.ddlVendor.SelectedValue = Convert.ToString(dsRejection.Tables["HEADER"].Rows[0]["VENDORID"]).Trim();
                this.ddlVendor.Enabled = false;
                LoadVendorInvoice_RejectionWise(ddlGrnRejectionNo.SelectedValue.ToString(), ddlVendor.SelectedValue.ToString(), ddlDepot.SelectedValue.Trim());

            }
            if (this.ddlGrnRejectionNo.SelectedValue != "0")
            {
                this.RemovePurchaseInvoiceSession();
                this.CreateDataTable();
                this.CreateDataTableTaxComponent();
                LoadInvoiceProductGrid(this.ddlGrnRejectionNo.SelectedValue.Trim());
            }
            else
            {
                this.grdinvoicedetails.DataSource = null;
                this.grdinvoicedetails.DataBind();
                this.grvFreeProduct.DataSource = null;
                this.grvFreeProduct.DataBind();
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

        //if (this.grdBatchDetails.Rows.Count > 0)
        //{
        //    for (int i = 0; i < grdBatchDetails.Rows.Count; i++)
        //    {
        //        TextBox txtgrdQty = (TextBox)grdBatchDetails.Rows[i].FindControl("txtgrdQty");
        //        Label lblgrdStockQty = (Label)grdBatchDetails.Rows[i].FindControl("lblgrdStockQty");
        //        if (txtgrdQty.Text != "" && Convert.ToDecimal(txtgrdQty.Text) > 0 && Convert.ToDecimal(lblgrdStockQty.Text) > 0)
        //        {
        //            ReturnValue += Convert.ToDecimal(txtgrdQty.Text.Trim());

        //        }
        //    }
        //}
        return ReturnValue;
    }
    #endregion

    #region btn_TempDelete_Click
    protected void btn_TempDelete_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btn_views = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            string BILLGUID = gvr.Cells[1].Text.Trim();

            DataTable dtdeleteInvoicerecord = new DataTable();
            dtdeleteInvoicerecord = (DataTable)Session["ADDPURCHASERETURNDETAILS"];

            int Taxflag = 0;

            DataRow[] drr = dtdeleteInvoicerecord.Select("GUID='" + BILLGUID.Trim() + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteInvoicerecord.AcceptChanges();
            }
            HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = dtdeleteInvoicerecord;

            #region Loop For Adding Itemwise Tax
            DataTable dtTaxCount = (DataTable)Session["dtPurchaseInvoiceTaxCount"];

            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                Taxflag = 0;
                if (Arry.Count > 0)
                {
                    foreach (string row in Arry)
                    {
                        if (row.Contains(dtTaxCount.Rows[k]["NAME"].ToString().Trim()))
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
                        Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                    }
                }
                else
                {
                    Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                }

            }
            #endregion

            DataTable dtdeleteItemWiseTax = new DataTable();
            dtdeleteItemWiseTax = (DataTable)Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
            DataRow[] drrTax = dtdeleteItemWiseTax.Select("GUID='" + BILLGUID.Trim() + "'");
            for (int i = 0; i < drrTax.Length; i++)
            {
                drrTax[i].Delete();
                dtdeleteItemWiseTax.AcceptChanges();
            }
            HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"] = dtdeleteItemWiseTax;

            if (dtdeleteInvoicerecord.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtdeleteInvoicerecord;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
                this.ddlGrnRejectionNo.Enabled = false;

            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = null;
                this.txtTotMRP.Text = "0.00";
                this.txtAmount.Text = "0.00";
                this.txtNetAmt.Text = "0.00";
                this.txtTotalGross.Text = "0.00";
                this.txtRoundoff.Text = "0.00";
                this.txtFinalAmt.Text = "0.00";
                this.ddlGrnRejectionNo.Enabled = true;
            }


            if (dtdeleteInvoicerecord.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            if (dtdeleteInvoicerecord.Rows.Count == 0)
            {
                this.txtTotPCS.Text = "0";
                this.txtTotMRP.Text = "0";
                this.txtAmount.Text = "0";
                this.txtTotTax.Text = "0";
                this.txtNetAmt.Text = "0";
                this.txtTotDisc.Text = "0";
                this.txtTotalGross.Text = "0";
                this.txtRoundoff.Text = "0";
                this.txtFinalAmt.Text = "0";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ConvertDatatableToXMLReturnInvoice
    public string ConvertDatatableToXMLReturnInvoice(DataTable dt)
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

    #region ConvertDatatableToXMLTax
    public string ConvertDatatableToXMLTax(DataTable dt)
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

    #region ConvertDatatableToXMLFreeProduct
    public string ConvertDatatableToXMLFreeProduct(DataTable dt)
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

    #region Delete Retun
    protected void DeleteRecordInvoice(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            if (clsReturn.Getstatus(e.Record["SALERETURNID"].ToString().Trim()) == "1")
            {
                e.Record["Error"] = "This Purchase Return is Already Approved.";
                return;
            }

            int flag = 0;
            flag = clsReturn.InvoiceDelete(e.Record["SALERETURNID"].ToString().Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.LoadPurchaseReturn();
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

    #region Save Purchase Return
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            string RetunInvoiceNo = string.Empty;
            string xml = string.Empty;
            string xmlTax = string.Empty;
            string xmlFreeProduct = string.Empty;
            DataTable dtRecordsCheck = new DataTable();
            DataTable dtFreeProduct = new DataTable();
            DataTable dtItemWiseTax = new DataTable();

            if (Session["ADDPURCHASERETURNDETAILS"] != null)
            {
                dtRecordsCheck = (DataTable)HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"];
            }
            if (Session["PURCHASERETURNTAXCOMPONENTDETAILS"] != null)
            {
                dtItemWiseTax = (DataTable)HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
            }
            if (dtRecordsCheck != null)
            {
                xml = ConvertDatatableToXMLReturnInvoice(dtRecordsCheck);
            }
            if (dtItemWiseTax != null)
            {
                xmlTax = ConvertDatatableToXMLTax(dtItemWiseTax);
            }

            #region Amount

            decimal TotalPCS = 0;
            decimal Roundoff = 0;
            decimal OtherCharges = 0;
            decimal Adjustment = 0;
            decimal TotalReturnInvoiceValue = 0;
            decimal GrossAmt = 0;
            decimal NetAmt = 0;
            decimal SpecialDisc = 0;

            if (this.txtTotPCS.Text == "")
            {
                TotalPCS = 0;
            }
            else
            {
                TotalPCS = Convert.ToDecimal(this.txtTotPCS.Text.Trim());
            }

            if (this.txtRoundoff.Text == "")
            {
                Roundoff = 0;
            }
            else
            {
                Roundoff = Convert.ToDecimal(this.txtRoundoff.Text);
            }

            if (this.txtAmount.Text == "")
            {
                TotalReturnInvoiceValue = 0;
            }
            else
            {
                TotalReturnInvoiceValue = Convert.ToDecimal(this.txtAmount.Text.Trim());
            }

            if (this.txtTotDisc.Text == "")
            {
                SpecialDisc = 0;
            }
            else
            {
                SpecialDisc = Convert.ToDecimal(this.txtTotDisc.Text.Trim());
            }


            if (this.txtTotalGross.Text == "")
            {
                GrossAmt = 0;
            }
            else
            {
                GrossAmt = Convert.ToDecimal(this.txtTotalGross.Text.Trim());
            }

            if (this.txtFinalAmt.Text == "")
            {
                NetAmt = 0;
            }
            else
            {
                NetAmt = Convert.ToDecimal(this.txtFinalAmt.Text.Trim());
            }

            #endregion

            RetunInvoiceNo = clsReturn.InsertPurchaseReturnInvoiceDetails(this.txtInvoiceDate.Text.Trim(),
                                                                          this.ddlVendor.SelectedValue.Trim(),
                                                                          Convert.ToString(this.ddlVendor.SelectedItem).Trim(),
                                                                          this.ddlDepot.SelectedValue.Trim(),
                                                                          Convert.ToString(this.ddlDepot.SelectedItem).Trim(),
                                                                          HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                                          HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                          this.txtRemarks.Text.Trim(),
                                                                          this.ddlSuppliedItem.SelectedValue.Trim(),
                                                                          Convert.ToString(this.ddlSuppliedItem.SelectedItem).Trim(),
                                                                          TotalPCS, TotalReturnInvoiceValue, OtherCharges,
                                                                          Adjustment, Roundoff, NetAmt,
                                                                          GrossAmt, 0, xml, xmlTax,
                                                                          Convert.ToString(hdnDespatchID.Value.Trim()),
                                                                          Convert.ToInt16(ViewState["Invoice_Type"]),
                                                                          Convert.ToString(Request.QueryString["MENUID"]).Trim(),
                                                                          txtinvno.Text, txtinvdt.Text, txtlrgrno.Text, txtlrgrdt.Text,
                                                                          ddltransporter.SelectedValue.ToString(), txtvehicalno.Text,
                                                                          ddlReturnType.SelectedValue.ToString(),
                                                                          ddlGrnRejectionNo.SelectedValue.ToString());

            if (RetunInvoiceNo != "")
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();

                if (Convert.ToString(hdnDespatchID.Value) == "")
                {
                    MessageBox1.ShowSuccess("Purchase-Return Invoice No :<b><font color='green'>  " + RetunInvoiceNo + "</font></b>  Saved Successfully", 80, 600);
                    this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    this.ClearControls();
                    this.LoadPurchaseReturn();

                    this.pnlAdd.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "";
                }
                this.hdnDespatchID.Value = "";
                this.ResetSession();
                this.rdbTax.Checked = true;
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Displayonoff("A");
        this.trAutoInvoiceNo.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        pnlAdd.Style["display"] = "none";
        btnaddhide.Style["display"] = "";
        this.ClearControls();
        this.ResetSession();
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grdinvoicedetails.DataSource = null;
        this.grdinvoicedetails.DataBind();
        this.hdnDespatchID.Value = "";
        this.divsave.Style["display"] = "";
        this.LoadPurchaseReturn();

        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = false;
        this.ddlVendor.Enabled = true;
        this.ddlSuppliedItem.Enabled = true;
        this.TdlblGrnRejectionNo.Visible = true;
        this.TdddlGrnRejectionNo.Visible = true;

        #endregion
    }
    #endregion

    #region ResetSession
    public void ResetSession()
    {
        Session.Remove("ADDPURCHASERETURNDETAILS");
        Session.Remove("PURCHASERETURNTAXCOMPONENTDETAILS");
    }
    #endregion

    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadPurchaseReturn();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region View Sale Return
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataSet ds = new DataSet();
            ResetSession();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.divsave.Style["display"] = "none";
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            #region Disable Controls
            //this.ImageButton1.Enabled = false;
            this.ddlDepot.Enabled = false;
            this.ddlVendor.Enabled = false;
            this.ddlSuppliedItem.Enabled = false;
            #endregion

            this.Displayonoff("V");

            if (HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }
            if (HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }

            string PurchaseReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsReturn.EditPurchaseReturnDetails(PurchaseReturnID);
            DataTable dtRetunInvoiceEditProductDetails = (DataTable)Session["ADDPURCHASERETURNDETAILS"];
            #region Header Table Information
            if (ds.Tables["HEADER"].Rows.Count > 0)
            {
                this.LoadMotherDepot();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNNO"]).Trim();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DEPOTID"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["REMARKS"]).Trim();
                this.LoadSuppliedItem();
                this.ddlSuppliedItem.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SUPPLIEDITEMID"]).Trim();
                this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());
                this.ddlVendor.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DISTRIBUTORID"]).Trim();
                this.txtTotPCS.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["TOTALPCS"]).Trim();
                this.txtinvno.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["INVOICENO"]).Trim();
                this.txtinvdt.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["INVOICEDATE"]).Trim();
                this.txtlrgrno.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["LRGRNO"]).Trim();
                this.txtlrgrdt.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["LRGRDATE"]).Trim();
                this.LoadTransporter();
                this.ddltransporter.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["TRANSPORTERID"]).Trim();
                this.txtvehicalno.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["VEHICHLENO"]).Trim();
                if (Convert.ToString(ds.Tables["HEADER"].Rows[0]["RETURNTYPE"]).Trim() == "N")
                {
                    ddlReturnType.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["RETURNTYPE"]).Trim();
                    this.TdlblGrnRejectionNo.Visible = false;
                    this.TdddlGrnRejectionNo.Visible = false;
                    this.ddlReturnType.Enabled = false;
                }
                else
                {
                    this.TdlblGrnRejectionNo.Visible = true;
                    this.TdddlGrnRejectionNo.Visible = true;
                    this.ddlGrnRejectionNo.Enabled = false;
                    this.ddlReturnType.Enabled = false;
                    ddlReturnType.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["RETURNTYPE"]).Trim();
                    this.LoadGrnRejection();
                    ddlGrnRejectionNo.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["REJECTIONNO"]).Trim();
                }
            }
            #endregion

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            if (dtTaxCountDataAddition.Rows.Count > 0)
            {
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
            }
            #endregion

            #region Item-wise Tax
            if (ds.Tables["TAXDETAILS"].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables["TAXDETAILS"].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["SALEINVOICEID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["SALEINVOICEID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXID"]);
                    dr["TAXPERCENTAGE"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXPERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXVALUE"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["PURCHASEINVTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Bill Quantity
            if (ds.Tables["BILLDETAILS"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["BILLDETAILS"].Rows.Count; i++)
                {
                    if (Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["FREETAG"]).Trim() == "0")
                    {
                        DataRow drEditReturnInvoiceDetails = dtRetunInvoiceEditProductDetails.NewRow();
                        drEditReturnInvoiceDetails["GUID"] = Guid.NewGuid();
                        drEditReturnInvoiceDetails["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["STOCKRECEIVEDID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTNAME"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTNAME"]).Trim();
                        drEditReturnInvoiceDetails["BATCHNO"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim();
                        drEditReturnInvoiceDetails["MRP"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MRP"]).Trim();
                        drEditReturnInvoiceDetails["RATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RATE"]).Trim();
                        drEditReturnInvoiceDetails["RETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["AMOUNT"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"]).Trim();
                        drEditReturnInvoiceDetails["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ALREADYRETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["REMAININGQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["REMAININGQTY"]).Trim();
                        drEditReturnInvoiceDetails["MFDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MFDATE"]).Trim();
                        drEditReturnInvoiceDetails["EXPRDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["EXPRDATE"]).Trim();
                        drEditReturnInvoiceDetails["FREETAG"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["FREETAG"]).Trim();
                        drEditReturnInvoiceDetails["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                        drEditReturnInvoiceDetails["INVOICEQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["INVOICEQTY"]).Trim();
                        decimal BillTaxAmt = 0;
                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["RELATEDTO"]))
                            {

                                case "1":
                                    TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]), Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim());
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                        }
                        #endregion
                        drEditReturnInvoiceDetails["NETAMOUNT"] = Convert.ToString(BillTaxAmt + Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()));
                        dtRetunInvoiceEditProductDetails.Rows.Add(drEditReturnInvoiceDetails);
                        dtRetunInvoiceEditProductDetails.AcceptChanges();
                    }
                }
            }
            #endregion

            #region Footer Details
            if (ds.Tables["FOOTERDETAILS"].Rows.Count > 0)
            {
                this.txtAmount.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TOTALSALEINVOICEVALUE"]).Trim();
                this.txtTotalGross.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["GROSSAMTOUNT"]).Trim();
                this.txtRoundoff.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["ROUNDOFFVALUE"]).Trim();
                this.txtFinalAmt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["NETAMOUNT"]).Trim();
            }
            #endregion

            #region Bind Grid
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtRetunInvoiceEditProductDetails;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = null;
            }
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            #endregion

            HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = dtRetunInvoiceEditProductDetails;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlDistributor_SelectedIndexChanged
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendor.SelectedValue != "0")
        {
            this.grdinvoicedetails.DataSource = null;
            this.grdinvoicedetails.DataBind();
            this.LoadVendorInvoice(this.ddlVendor.SelectedValue.Trim(), this.txtinvoicefromdate.Text.Trim(), this.txtinvoicetodate.Text.Trim(), this.ddlDepot.SelectedValue.Trim());

        }
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnDespatchID.Value = "";
        this.txtInvoiceDate.Text = "";
        this.txtSaleInvoiceNo.Text = "";
        this.txtRemarks.Text = "";
        this.ddlVendor.SelectedValue = "0";
        this.ddlretailerinvoice.Items.Clear();
        this.ddlretailerinvoice.Items.Add(new ListItem("Select Invoice No", "0"));
        this.ddlretailerinvoice.AppendDataBoundItems = true;
        this.txtAdj.Text = "0";
        this.txtOtherCharge.Text = "0";
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "";
        this.txtTotMRP.Text = "";
        this.txtTotDisc.Text = "0.00";
        this.txtTotTax.Text = "";
        this.txtNetAmt.Text = "";
        this.txtRoundoff.Text = "";
        this.ddlSuppliedItem.SelectedValue = "0";
        this.txtFinalAmt.Text = "";
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grvFreeProduct.DataSource = null;
        this.grvFreeProduct.DataBind();
        this.grdinvoicedetails.DataSource = null;
        this.grdinvoicedetails.DataBind();
        this.txtinvoicefromdate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtinvoicetodate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtinvno.Text = "";
        this.txtinvdt.Text = "";
        this.txtlrgrno.Text = "";
        this.txtlrgrdt.Text = "";
        this.ddltransporter.SelectedValue = "0";
        this.txtvehicalno.Text = "";
        this.ddlReturnType.SelectedValue = "N";
        this.ddlGrnRejectionNo.SelectedValue = "NA";
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        if (hdnDespatchID.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));//2
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));//3
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));//4
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//5
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));//6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));//7
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));//8
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));//9
            dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));//10
            dt.Columns.Add(new DataColumn("ACTUALQTY", typeof(string)));//11
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));//12
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));//13
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));//14
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));//15
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));//16
            dt.Columns.Add(new DataColumn("FREETAG", typeof(string)));//17
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//18

            #region Loop For Adding Itemwise Tax Component
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataSet ds = new DataSet();
            string id = this.ddlretailerinvoice.SelectedValue.Trim();
            if(id=="0")
            {
                id = this.ddlGrnRejectionNo.SelectedValue.Trim();
            }
            ds = clsReturn.BindPurchaseInvoiceProductDetails(id);

            Session["dtPurchaseInvoiceTaxCount"] = ds.Tables["TaxCount"];
            for (int k = 0; k < ds.Tables["TaxCount"].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["NAME"]) + "", typeof(string)));
            }
            #endregion

            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));//2
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));//3
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));//4
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//5
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));//6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));//7
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));//8
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));//9
            dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));//10
            dt.Columns.Add(new DataColumn("ACTUALQTY", typeof(string)));//11
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));//12
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));//13
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));//14
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));//15
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));//16
            dt.Columns.Add(new DataColumn("FREETAG", typeof(string)));//17
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//18

            #region Loop For Adding Itemwise Tax Component
            DataSet ds = new DataSet();
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            ds = clsReturn.EditPurchaseReturnDetails(SaleReturnID);
            Session["dtPurchaseInvoiceTaxCount"] = ds.Tables["TAXCOUNT"];
            if (ds.Tables["TAXCOUNT"].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables["TAXCOUNT"].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            #endregion

            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        HttpContext.Current.Session["PURCHASEINVOICEDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region Create Data Table Invoice Tax Component Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        HttpContext.Current.Session["PURCHASEINVTAXCOMPONENTDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region Create Data Table Return Tax Component Structure
    public DataTable CreateDataTableReturnTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region Create Data Table Sale Return Bill Product Structure
    public DataTable CreateDataTableSaleReturnBillProduct()
    {
        DataTable dt = new DataTable();
        if (hdnDespatchID.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));//1
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));//2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));//3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//4
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));//5
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));//6
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));//7
            dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));//8
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));//9
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));//10
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));//11
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));//12
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));//13
            dt.Columns.Add(new DataColumn("FREETAG", typeof(string)));//14
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//15
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));//16
            #region Loop For Adding Itemwise Tax Component
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataSet ds = new DataSet();
            string id = this.ddlretailerinvoice.SelectedValue.Trim();
            if (id == "0")
            {
                id = this.ddlGrnRejectionNo.SelectedValue.Trim();
            }
            ds = clsReturn.BindPurchaseInvoiceProductDetails(id);

            Session["dtPurchaseInvoiceTaxCount"] = ds.Tables["TaxCount"];
            for (int k = 0; k < ds.Tables["TaxCount"].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["NAME"]) + "", typeof(string)));
            }
            #endregion
            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));//1
            dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));//2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));//3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//4
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));//5
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));//6
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));//7
            dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));//8
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));//9
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));//10
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));//11
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));//12
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));//13
            dt.Columns.Add(new DataColumn("FREETAG", typeof(string)));//14
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//15
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));//16
            #region Loop For Adding Itemwise Tax Component
            DataSet ds = new DataSet();
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            ds = clsReturn.EditPurchaseReturnDetails(SaleReturnID);
            Session["dtPurchaseInvoiceTaxCount"] = ds.Tables["TAXCOUNT"];
            if (ds.Tables["TAXCOUNT"].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables["TAXCOUNT"].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            #endregion
            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string GUID, string SALEINVOICEID, string PRODUCTID, string BATCH, string TAXID, string TAXPERCENTAGE, string TAXVALUE)
    {
        DataTable dt = (DataTable)Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["GUID"] = GUID;
        dr["SALEINVOICEID"] = SALEINVOICEID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["TAXPERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = TAXVALUE;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    #region LoadPurchaseReturn
    public void LoadPurchaseReturn()
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            string Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.grdDespatchHeader.DataSource = clsReturn.BindPurchaseReturn(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(),
                                                                                HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                                HttpContext.Current.Session["DEPOTID"].ToString().Trim(),
                                                                                Checker, HttpContext.Current.Session["UserID"].ToString().Trim()
                                                                             );
            this.grdDespatchHeader.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region btnSearchRetailerInvoice_Click
    protected void btnSearchRetailerInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadVendorInvoice(this.ddlVendor.SelectedValue.Trim(), this.txtinvoicefromdate.Text.Trim(), this.txtinvoicetodate.Text.Trim(), this.ddlDepot.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Remove SaleInvoice Session
    protected void RemovePurchaseInvoiceSession()
    {
        Session.Remove("PURCHASEINVOICEDETAILS");
        Session.Remove("PURCHASEINVTAXCOMPONENTDETAILS");
    }
    #endregion

    #region ddlretailerinvoice_SelectedIndexChanged
    protected void ddlretailerinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlretailerinvoice.SelectedValue != "0")
            {
                this.RemovePurchaseInvoiceSession();
                this.CreateDataTable();
                this.CreateDataTableTaxComponent();
                LoadInvoiceProductGrid(this.ddlretailerinvoice.SelectedValue.Trim());
            }
            else
            {
                this.grdinvoicedetails.DataSource = null;
                this.grdinvoicedetails.DataBind();
                this.grvFreeProduct.DataSource = null;
                this.grvFreeProduct.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadInvoiceProductGrid
    public void LoadInvoiceProductGrid(string InvoiceID)
    {
        ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
        string TAXID = string.Empty;
        decimal ProductWiseTax = 0;
        decimal NetAmount = 0;

        DataSet ds = new DataSet();
        ds = clsReturn.BindPurchaseInvoiceProductDetails(InvoiceID);

        #region Product Details Information

        #region Item-wise Tax Component
        if (ds.Tables["TaxDetails"].Rows.Count > 0)
        {
            DataTable dtTaxComponent = (DataTable)Session["PURCHASEINVTAXCOMPONENTDETAILS"];
            for (int i = 0; i < ds.Tables["TaxDetails"].Rows.Count; i++)
            {
                DataRow dr = dtTaxComponent.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["STOCKRECEIVEDID"]);
                dr["PRODUCTID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRODUCTID"]);
                dr["PRODUCTNAME"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRODUCTNAME"]);
                dr["BATCHNO"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["BATCHNO"]);
                dr["TAXID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAXID"]);
                dr["NAME"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["NAME"]);
                dr["TAXPERCENTAGE"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAXPERCENTAGE"]);
                dr["TAXVALUE"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAXVALUE"]);
                dtTaxComponent.Rows.Add(dr);
                dtTaxComponent.AcceptChanges();
            }
            HttpContext.Current.Session["PURCHASEINVTAXCOMPONENTDETAILS"] = dtTaxComponent;
        }
        #endregion

        #region Details Information

        #region Bill Details
        DataTable dtSaleinvoice = (DataTable)Session["PURCHASEINVOICEDETAILS"];
        if (ds.Tables["BillDetails"].Rows.Count > 0)
        {
            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
            }
            #endregion

            for (int i = 0; i < ds.Tables["BillDetails"].Rows.Count; i++)
            {
                DataRow dr = dtSaleinvoice.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["STOCKRECEIVEDID"]);
                dr["PRODUCTID"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTID"]);
                dr["PRODUCTNAME"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTNAME"]);
                dr["BATCHNO"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["BATCHNO"]);
                dr["MRP"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["MRP"]);
                dr["RATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["RATE"]);
                dr["INVOICEQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["INVOICEQTY"]);
                dr["REJECTIONQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["REJECTIONQTY"]);
                dr["ACTUALQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["ACTUALQTY"]);
                dr["AMOUNT"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["AMOUNT"]);
                dr["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["ALREADYRETURNQTY"]);
                dr["REMAININGQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["REMAININGQTY"]);
                dr["MFDATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["MFDATE"]);
                dr["EXPRDATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["EXPRDATE"]);
                dr["FREETAG"] = Convert.ToInt32(ds.Tables["BillDetails"].Rows[i]["FREETAG"]);
                dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["ASSESMENTPERCENTAGE"]);
                decimal BillTaxAmt = 0;

                #region Loop For Adding Itemwise Tax Component
                ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    switch (Convert.ToString(ds.Tables["TaxCount"].Rows[k]["RELATEDTO"]))
                    {
                        case "1":

                            TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTID"].ToString()), txtInvoiceDate.Text);
                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables["BillDetails"].Rows[i]["AMOUNT"].ToString()) * ProductWiseTax / 100));
                            BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                            break;
                    }
                }
                #endregion

                dr["NETAMOUNT"] = Convert.ToString(BillTaxAmt + Convert.ToDecimal(ds.Tables["BillDetails"].Rows[i]["AMOUNT"].ToString()));
                dtSaleinvoice.Rows.Add(dr);
                dtSaleinvoice.AcceptChanges();
            }
        }
        #endregion

        HttpContext.Current.Session["PURCHASEINVOICEDETAILS"] = dtSaleinvoice;
        this.grdinvoicedetails.DataSource = dtSaleinvoice;
        this.grdinvoicedetails.DataBind();

        #endregion

        if (dtSaleinvoice.Rows.Count == 0)
        {
            this.grdinvoicedetails.DataSource = null;
            this.grdinvoicedetails.DataBind();
        }

        #endregion

        #region Footer Details
        if (ds.Tables["Footer"].Rows.Count > 0)
        {
            NetAmount = Convert.ToDecimal(ds.Tables["Footer"].Rows[0]["TOTALINVOICEVALUE"].ToString().Trim());
        }
        #endregion

        #region Header Details
        if (ds.Tables["Header"].Rows.Count > 0)
        {
            txtinvno.Text = ds.Tables["Header"].Rows[0]["INVOICENO"].ToString().Trim();
            txtinvdt.Text = ds.Tables["Header"].Rows[0]["INVOICEDATE"].ToString().Trim();
        }
        #endregion

        if (grdinvoicedetails.Rows.Count > 0)
        {
            this.InvoiceGridCalculation(NetAmount);
        }
    }
    #endregion

    #region LoadVendorInvoice
    public void LoadVendorInvoice(string VendorID, string FromDate, string ToDate, string DepotID)
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataTable dtinvoice = new DataTable();
            dtinvoice = clsReturn.BindVendorInvoice(VendorID, FromDate, ToDate, DepotID);

            List<Purchasedetails> InvoiceDetails = new List<Purchasedetails>();
            if (dtinvoice.Rows.Count > 0)
            {
                this.ddlretailerinvoice.Items.Clear();
                this.ddlretailerinvoice.Items.Add(new ListItem("Select Invoice No", "0"));

                for (int i = 0; i < dtinvoice.Rows.Count; i++)
                {
                    Purchasedetails po = new Purchasedetails();
                    po.INVOICEID = dtinvoice.Rows[i]["INVOICEID"].ToString();
                    po.INVOICENO = dtinvoice.Rows[i]["INVOICENO"].ToString();
                    po.INVOICEDATE = dtinvoice.Rows[i]["INVOICEDATE"].ToString();
                    po.NETAMOUNT = dtinvoice.Rows[i]["NETAMOUNT"].ToString();
                    InvoiceDetails.Add(po);
                }

                //---------------------------------------------------------------------------------------------

                string text1 = string.Format("{0}{1}{2}",
                "INVOICE NO".PadRight(58, '\u00A0'),
                "INVOICE DATE".PadRight(15, '\u00A0'),
                "NET AMOUNT".PadRight(11, '\u00A0')
                );

                ddlretailerinvoice.Items.Add(new ListItem(text1, "1"));

                foreach (ListItem item in ddlretailerinvoice.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        item.Attributes.CssStyle.Add("style", "background-color:red;");
                    }
                }

                foreach (Purchasedetails p in InvoiceDetails)
                {
                    string text = string.Format("{0}{1}{2}",
                        p.INVOICENO.PadRight(58, '\u00A0'),
                        p.INVOICEDATE.PadRight(15, '\u00A0'),
                        p.NETAMOUNT.PadRight(11, '\u00A0')
                        );

                    ddlretailerinvoice.Items.Add(new ListItem(text, "" + p.INVOICEID + ""));
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region btnreturnadd_click
    public void btnreturnadd_click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }

            if (HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }

            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataTable dtReturn = (DataTable)Session["ADDPURCHASERETURNDETAILS"];
            DataTable dtItemWiseTax = (DataTable)Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
            DataTable dtTaxCount = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            DataTable dtSaleInvoice = new DataTable();
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            int numberOfRecords = 0;

            if (Session["PURCHASEINVOICEDETAILS"] != null)
            {
                dtSaleInvoice = (DataTable)Session["PURCHASEINVOICEDETAILS"];
            }

            if (dtSaleInvoice.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdinvoicedetails.Rows)
                {
                    TextBox txtreturnqty = (TextBox)row.FindControl("txtreturnqty");
                    string ReturnQty = txtreturnqty.Text.Split(',').Last();
                    decimal REMAININGQTY = Convert.ToDecimal(row.Cells[14].Text.Trim());
                    decimal rejectQty= Convert.ToDecimal(row.Cells[10].Text.Trim());
                    decimal grnRetunQty= Convert.ToDecimal(row.Cells[13].Text.Trim());

                    if(ReturnQty=="")
                    {
                        ReturnQty = "0";
                    }
                   

                    string type = this.ddlReturnType.SelectedValue;
                    if(type=="G")
                    {
                        if(Convert.ToDecimal(ReturnQty) > (rejectQty- grnRetunQty))
                        {
                            MessageBox1.ShowWarning("Return qty greater than rejected Qty");
                            return;
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(ReturnQty) > REMAININGQTY)
                        {
                            MessageBox1.ShowWarning("Return qty greater than remaining Qty");
                            return;
                        }
                    }

                    if (ReturnQty.Trim() != "")
                    {
                        if (Convert.ToDecimal(ReturnQty.Trim()) > 0 && REMAININGQTY >= Convert.ToDecimal(ReturnQty.Trim()))
                        {
                            if (rdbTax.Checked == true)
                            {
                                #region With TAX

                                decimal Amount = 0;
                                decimal ReturnAmount = 0;
                                int Taxflag = 0;
                                Amount = (Convert.ToDecimal(ReturnQty) * Convert.ToDecimal(row.Cells[8].Text.Trim()));
                                ReturnAmount = Amount;

                                if (Convert.ToString(row.Cells[17].Text.Trim()) == "0")
                                {
                                    if (this.ddlSuppliedItem.SelectedValue.Trim() == "1")
                                    {
                                        numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "'").Length;
                                    }
                                    else
                                    {
                                        //numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "'").Length;
                                        numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "'").Length;
                                    }
                                    if (numberOfRecords > 0)
                                    {
                                        MessageBox1.ShowInfo("" + Convert.ToString(row.Cells[5].Text.Trim()) + "  already exist", 60, 500);
                                        break;
                                    }
                                    else
                                    {
                                        DataRow dr = dtReturn.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        string GUID = dr["GUID"].ToString().Trim();
                                        dr["STOCKRECEIVEDID"] = Convert.ToString(row.Cells[3].Text.Trim());
                                        dr["PRODUCTID"] = Convert.ToString(row.Cells[4].Text.Trim());
                                        dr["PRODUCTNAME"] = Convert.ToString(row.Cells[5].Text.Trim());
                                        dr["BATCHNO"] = Convert.ToString(row.Cells[6].Text.Trim());
                                        dr["MRP"] = Convert.ToString(row.Cells[7].Text.Trim());
                                        dr["RATE"] = Convert.ToString(row.Cells[8].Text.Trim());
                                        dr["RETURNQTY"] = ReturnQty.Trim();
                                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ReturnAmount)).Trim();
                                        dr["ALREADYRETURNQTY"] = Convert.ToString(row.Cells[13].Text.Trim());
                                        dr["REMAININGQTY"] = Convert.ToString(row.Cells[14].Text.Trim());
                                        dr["MFDATE"] = Convert.ToString(row.Cells[15].Text.Trim());
                                        dr["EXPRDATE"] = Convert.ToString(row.Cells[16].Text.Trim());
                                        dr["FREETAG"] = Convert.ToString(row.Cells[17].Text.Trim());
                                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(row.Cells[18].Text.Trim());
                                        dr["INVOICEQTY"] = Convert.ToString(row.Cells[9].Text.Trim());
                                        decimal BillTaxAmt = 0;

                                        #region Loop For Adding Itemwise Tax Component
                                        for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                                        {
                                            TAXID = clsReturn.TaxID(dtTaxCount.Rows[k]["NAME"].ToString());
                                            ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(row.Cells[4].Text.Trim()), txtInvoiceDate.Text);
                                            dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                            dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", ReturnAmount * ProductWiseTax / 100));
                                            BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""]);

                                            if (Arry.Count > 0)
                                            {
                                                foreach (string row1 in Arry)
                                                {
                                                    if (row1.Contains(dtTaxCount.Rows[k]["NAME"].ToString().Trim()))
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
                                                    Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                                            }



                                            CreateTaxDatatable(GUID,
                                                                Convert.ToString(row.Cells[3].Text.Trim()),
                                                                Convert.ToString(row.Cells[4].Text.Trim()),
                                                                Convert.ToString(row.Cells[6].Text.Trim()),
                                                                TAXID,
                                                                Convert.ToString(ProductWiseTax).Trim(),
                                                                dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""].ToString().Trim()
                                                                );

                                        }
                                        #endregion

                                        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + ReturnAmount));
                                        dtReturn.Rows.Add(dr);
                                        dtReturn.AcceptChanges();

                                    }
                                }
                                #endregion
                            }
                            if (rdbNoTax.Checked == true)
                            {
                                #region Without TAX

                                decimal Amount = 0;
                                decimal ReturnAmount = 0;
                                int Taxflag = 0;
                                Amount = (Convert.ToDecimal(ReturnQty) * Convert.ToDecimal(row.Cells[8].Text.Trim()));
                                ReturnAmount = Amount;


                                if (Convert.ToString(row.Cells[17].Text.Trim()) == "0")
                                {
                                    if (this.ddlSuppliedItem.SelectedValue.Trim() == "1")
                                    {
                                        numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND BATCHNO= '" + Convert.ToString(row.Cells[6].Text.Trim()) + "'").Length;
                                    }
                                    else
                                    {
                                        //numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "'").Length;
                                        numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND GUID= '" + Convert.ToString(row.Cells[2].Text.Trim()) + "'").Length;
                                    }
                                    if (numberOfRecords > 0)
                                    {
                                        MessageBox1.ShowInfo("" + Convert.ToString(row.Cells[5].Text.Trim()) + "  already exist", 60, 500);
                                        break;
                                    }
                                    else
                                    {
                                        DataRow dr = dtReturn.NewRow();
                                        dr["GUID"] = Guid.NewGuid();
                                        string GUID = dr["GUID"].ToString().Trim();
                                        dr["STOCKRECEIVEDID"] = Convert.ToString(row.Cells[3].Text.Trim());
                                        dr["PRODUCTID"] = Convert.ToString(row.Cells[4].Text.Trim());
                                        dr["PRODUCTNAME"] = Convert.ToString(row.Cells[5].Text.Trim());
                                        dr["BATCHNO"] = Convert.ToString(row.Cells[6].Text.Trim());
                                        dr["MRP"] = Convert.ToString(row.Cells[7].Text.Trim());
                                        dr["RATE"] = Convert.ToString(row.Cells[8].Text.Trim());
                                        dr["RETURNQTY"] = ReturnQty.Trim();
                                        dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ReturnAmount)).Trim();
                                        dr["ALREADYRETURNQTY"] = Convert.ToString(row.Cells[13].Text.Trim());
                                        dr["REMAININGQTY"] = Convert.ToString(row.Cells[14].Text.Trim());
                                        dr["MFDATE"] = Convert.ToString(row.Cells[15].Text.Trim());
                                        dr["EXPRDATE"] = Convert.ToString(row.Cells[16].Text.Trim());
                                        dr["FREETAG"] = Convert.ToString(row.Cells[17].Text.Trim());
                                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(row.Cells[18].Text.Trim());
                                        dr["INVOICEQTY"] = Convert.ToString(row.Cells[9].Text.Trim());
                                        decimal BillTaxAmt = 0;

                                        #region Loop For Adding Itemwise Tax Component
                                        for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                                        {
                                            TAXID = "NA";
                                            ProductWiseTax = 0;
                                            dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                            dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                            BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""]);

                                            if (Arry.Count > 0)
                                            {
                                                foreach (string row1 in Arry)
                                                {
                                                    if (row1.Contains(dtTaxCount.Rows[k]["NAME"].ToString().Trim()))
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
                                                    Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                Arry.Add(dtTaxCount.Rows[k]["NAME"].ToString());
                                            }
                                        }
                                        #endregion

                                        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + ReturnAmount));
                                        dtReturn.Rows.Add(dr);
                                        dtReturn.AcceptChanges();


                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            MessageBox1.ShowInfo("Return Qty Can not be greater Than Remaining Qty.", 60, 500);
                        }
                    }

                    TextBox t = (TextBox)row.FindControl("txtreturnqty");
                    t.Text = "0";
                }


                Session["ADDPURCHASERETURNDETAILS"] = dtReturn;
            }
            if (dtReturn.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtReturn;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
                this.ddlGrnRejectionNo.Enabled = false;
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = null;
                this.ddlGrnRejectionNo.Enabled = true;
            }
            if (dtReturn.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Displayonoff
    protected void Displayonoff(string flag)
    {
        if (flag == "V")
        {
            this.divsearch.Style["display"] = "none";
            this.divproduct.Style["display"] = "none";
            this.trbtnreturnadd.Style["display"] = "none";
            this.trinvoiceno.Style["display"] = "none";
            this.grdAddDespatch.Columns[0].Visible = false;
            this.grvFreeProduct.Columns[0].Visible = false;
        }
        if (flag == "A")
        {
            this.divsearch.Style["display"] = "";
            this.divproduct.Style["display"] = "";
            this.trbtnreturnadd.Style["display"] = "";
            this.trinvoiceno.Style["display"] = "";
            this.grdAddDespatch.Columns[0].Visible = true;
            this.grvFreeProduct.Columns[0].Visible = true;
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

    #region InvoiceGridCalculation
    protected void InvoiceGridCalculation(decimal NetAmt)
    {
        decimal TotalInvoiceQuantity = 0;
        decimal TotalRejectedQuantity = 0;
        decimal TotalActualQuantity = 0;
        decimal TotalPurchaseAmt = 0;
        decimal TotalNetAmt = 0;
        TotalNetAmt = NetAmt;

        this.grdinvoicedetails.HeaderRow.Cells[5].Text = "PRODUCT";
        this.grdinvoicedetails.HeaderRow.Cells[6].Text = "BATCH";
        this.grdinvoicedetails.HeaderRow.Cells[7].Text = "MRP";
        this.grdinvoicedetails.HeaderRow.Cells[8].Text = "RATE";
        this.grdinvoicedetails.HeaderRow.Cells[9].Text = "INV.QTY(PCS)";
        this.grdinvoicedetails.HeaderRow.Cells[10].Text = "RJCT.QTY(PCS)";
        this.grdinvoicedetails.HeaderRow.Cells[11].Text = "ACT.QTY(PCS)";
        this.grdinvoicedetails.HeaderRow.Cells[12].Text = "PURCHASE AMT";
        this.grdinvoicedetails.HeaderRow.Cells[13].Text = "RETURN.QTY(PCS)";
        this.grdinvoicedetails.HeaderRow.Cells[14].Text = "REMAIN.QTY(PCS)";


        this.grdinvoicedetails.FooterRow.Cells[8].Text = "Total : ";
        this.grdinvoicedetails.FooterRow.Cells[8].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[8].ForeColor = Color.Blue;

        this.grdinvoicedetails.HeaderRow.Cells[2].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[3].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[4].Visible = false;
        //this.grdinvoicedetails.HeaderRow.Cells[13].Visible = false;
        //this.grdinvoicedetails.HeaderRow.Cells[14].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[15].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[16].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[17].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[18].Visible = false;

        this.grdinvoicedetails.HeaderRow.Cells[1].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[2].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[3].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[4].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[5].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[6].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[7].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[8].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[9].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[10].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[11].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[12].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[13].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[14].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[15].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[16].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[17].Wrap = false;
        this.grdinvoicedetails.HeaderRow.Cells[18].Wrap = false;

        this.grdinvoicedetails.FooterRow.Cells[2].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[3].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[4].Visible = false;
        //this.grdinvoicedetails.FooterRow.Cells[13].Visible = false;
        //this.grdinvoicedetails.FooterRow.Cells[14].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[15].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[16].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[17].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[18].Visible = false;

        this.grdinvoicedetails.FooterRow.Cells[1].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[2].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[3].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[4].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[5].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[6].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[7].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[8].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[9].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[10].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[11].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[12].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[13].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[14].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[15].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[16].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[17].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[18].Wrap = false;

        foreach (GridViewRow row in grdinvoicedetails.Rows)
        {
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            //row.Cells[13].Visible = false;
            //row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;

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

            TotalInvoiceQuantity += Convert.ToDecimal(row.Cells[9].Text.Trim());
            TotalRejectedQuantity += Convert.ToDecimal(row.Cells[10].Text.Trim());
            TotalActualQuantity += Convert.ToDecimal(row.Cells[11].Text.Trim());
            TotalPurchaseAmt += Convert.ToDecimal(row.Cells[12].Text.Trim());

            row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

            int count = 18;
            DataTable dt = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    count = count + 1;
                    this.grdinvoicedetails.HeaderRow.Cells[count].Wrap = true;
                }
            }
        }

        #region Invoice Qty
        if (TotalInvoiceQuantity == 0)
        {
            this.grdinvoicedetails.FooterRow.Cells[9].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[9].Text = TotalInvoiceQuantity.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[9].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[9].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[9].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Rejected Qty
        if (TotalRejectedQuantity == 0)
        {
            this.grdinvoicedetails.FooterRow.Cells[10].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[10].Text = TotalRejectedQuantity.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[10].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[10].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[10].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Actual Qty
        if (TotalActualQuantity == 0)
        {
            this.grdinvoicedetails.FooterRow.Cells[11].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[11].Text = TotalActualQuantity.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[11].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[11].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[11].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Purchase Amount
        if (TotalPurchaseAmt == 0)
        {
            this.grdinvoicedetails.FooterRow.Cells[12].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[12].Text = TotalPurchaseAmt.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[12].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[12].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[12].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Tax
        int TotalRows = grdinvoicedetails.Rows.Count;
        int count1 = 0;

        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
        for (int i = 20; i <= (20 + dtTaxCountDataAddition1.Rows.Count); i += 2)
        {
            double sum = 0.00;

            for (int j = 0; j < TotalRows; j++)
            {
                sum += grdinvoicedetails.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grdinvoicedetails.Rows[j].Cells[i].Text) : 0.00;
                grdinvoicedetails.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }

            this.grdinvoicedetails.FooterRow.Cells[i].Text = sum.ToString("#.00");
            this.grdinvoicedetails.FooterRow.Cells[i].Font.Bold = true;
            this.grdinvoicedetails.FooterRow.Cells[i].ForeColor = Color.Blue;
            this.grdinvoicedetails.FooterRow.Cells[i].Wrap = false;
            this.grdinvoicedetails.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            count1 = count1 + 1;
        }

        for (int i = 19; i <= (19 + dtTaxCountDataAddition1.Rows.Count); i += 2)
        {
            for (int j = 0; j < TotalRows; j++)
            {
                grdinvoicedetails.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        #endregion

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                this.grdinvoicedetails.FooterRow.Cells[21 + count1].Text = "0.00";
            }
            else
            {
                this.grdinvoicedetails.FooterRow.Cells[21 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            this.grdinvoicedetails.FooterRow.Cells[21 + count1].Font.Bold = true;
            this.grdinvoicedetails.FooterRow.Cells[21 + count1].ForeColor = Color.Blue;
            this.grdinvoicedetails.FooterRow.Cells[21 + count1].Wrap = false;
            this.grdinvoicedetails.FooterRow.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdinvoicedetails.Rows)
            {
                row.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdinvoicedetails.HeaderRow.Cells[21 + count1].Text = "NET AMOUNT";
            this.grdinvoicedetails.HeaderRow.Cells[21 + count1].Wrap = false;
        }
        else if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                this.grdinvoicedetails.FooterRow.Cells[20 + count1].Text = "0.00";
            }
            else
            {
                this.grdinvoicedetails.FooterRow.Cells[20 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            this.grdinvoicedetails.FooterRow.Cells[20 + count1].Font.Bold = true;
            this.grdinvoicedetails.FooterRow.Cells[20 + count1].ForeColor = Color.Blue;
            this.grdinvoicedetails.FooterRow.Cells[20 + count1].Wrap = false;
            this.grdinvoicedetails.FooterRow.Cells[20 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdinvoicedetails.Rows)
            {
                row.Cells[20 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdinvoicedetails.HeaderRow.Cells[20 + count1].Text = "NET AMOUNT";
            this.grdinvoicedetails.HeaderRow.Cells[20 + count1].Wrap = false;
        }
        else if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                this.grdinvoicedetails.FooterRow.Cells[19 + count1].Text = "0.00";
            }
            else
            {
                this.grdinvoicedetails.FooterRow.Cells[19 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            this.grdinvoicedetails.FooterRow.Cells[19 + count1].Font.Bold = true;
            this.grdinvoicedetails.FooterRow.Cells[19 + count1].ForeColor = Color.Blue;
            this.grdinvoicedetails.FooterRow.Cells[19 + count1].Wrap = false;
            this.grdinvoicedetails.FooterRow.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdinvoicedetails.Rows)
            {
                row.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdinvoicedetails.HeaderRow.Cells[19 + count1].Text = "NET AMOUNT";
            this.grdinvoicedetails.HeaderRow.Cells[19 + count1].Wrap = false;
        }
        #endregion


    }
    #endregion

    #region BillGridCalculation
    protected void BillGridCalculation()
    {
        decimal TotalReturnQuantity = 0;
        decimal TotalReturnAmt = 0;
        decimal TotalNetAmt = 0;
        decimal TotalBillTax = 0;
        DataTable dtSALERETURNDETAILS = new DataTable();
        DataTable dtRETURNFREEQTYDETAILS = new DataTable();
        if (Session["ADDPURCHASERETURNDETAILS"] != null)
        {
            dtSALERETURNDETAILS = (DataTable)Session["ADDPURCHASERETURNDETAILS"];
        }


        TotalNetAmt = CalculateTotalNetAmount(dtSALERETURNDETAILS);
        TotalBillTax = CalculateTaxTotal(dtSALERETURNDETAILS);


        this.grdAddDespatch.HeaderRow.Cells[4].Text = "PRODUCT";
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "BATCH";
        this.grdAddDespatch.HeaderRow.Cells[6].Text = "MRP";
        this.grdAddDespatch.HeaderRow.Cells[7].Text = "RATE";
        this.grdAddDespatch.HeaderRow.Cells[8].Text = "RETURN QTY(PCS)";
        this.grdAddDespatch.HeaderRow.Cells[9].Text = "RET AMT";

        this.grdAddDespatch.FooterRow.Cells[7].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[7].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[7].ForeColor = Color.Blue;

        this.grdAddDespatch.HeaderRow.Cells[1].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[10].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[11].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[12].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[13].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[14].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[15].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[16].Visible = false;

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

        this.grdAddDespatch.FooterRow.Cells[1].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[2].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[3].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[10].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[11].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[12].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[13].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[14].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[15].Visible = false;
        this.grdAddDespatch.FooterRow.Cells[16].Visible = false;

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

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[10].Visible = false;
            row.Cells[11].Visible = false;
            row.Cells[12].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;


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

            TotalReturnQuantity += Convert.ToDecimal(row.Cells[8].Text.Trim());
            TotalReturnAmt += Convert.ToDecimal(row.Cells[9].Text.Trim());

            row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;

            int count = 16;
            DataTable dt = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grdAddDespatch.HeaderRow.Cells[count].Wrap = true;
            }
        }

        #region Return Qty
        if (TotalReturnQuantity == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[8].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[8].Text = TotalReturnQuantity.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[8].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[8].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[8].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Return Amount
        if (TotalReturnAmt == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[9].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[9].Text = TotalReturnAmt.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[9].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[9].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[9].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Tax
        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;

        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
        for (int i = 18; i <= (18 + dtTaxCountDataAddition1.Rows.Count); i += 2)
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

        for (int i = 17; i <= (17 + dtTaxCountDataAddition1.Rows.Count); i += 2)
        {
            for (int j = 0; j < TotalRows; j++)
            {
                grdAddDespatch.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        #endregion

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[19 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[19 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[19 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[19 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[19 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[19 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[19 + count1].Wrap = false;
        }
        else if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[18 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[18 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[18 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[18 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[18 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[18 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[18 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[18 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[18 + count1].Wrap = false;
        }
        else if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[17 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[17 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[17 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[17 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[17 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[17 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[17 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[17 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[17 + count1].Wrap = false;
        }
        #endregion

        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalReturnAmt));
        this.txtTotMRP.Text = Convert.ToString(String.Format("{0:0.00}", 0));
        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalBillTax));




    }
    #endregion

    #region FreeGridCalculation
    protected void FreeGridCalculation()
    {
        decimal TotalReturnQuantity = 0;
        decimal TotalSchemeValue = 0;
        decimal TotalReturnAmt = 0;
        decimal TotalNetAmt = 0;
        decimal TotalBillTax = 0;
        decimal TotalFreeTax = 0;
        DataTable dtSALERETURNDETAILS = new DataTable();
        DataTable dtRETURNFREEQTYDETAILS = new DataTable();
        if (Session["ADDPURCHASERETURNDETAILS"] != null)
        {
            dtSALERETURNDETAILS = (DataTable)Session["ADDPURCHASERETURNDETAILS"];
        }
        if (Session["RETURNFREEQTYDETAILS"] != null)
        {
            dtRETURNFREEQTYDETAILS = (DataTable)Session["RETURNFREEQTYDETAILS"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtRETURNFREEQTYDETAILS);
        TotalBillTax = CalculateTaxTotal(dtSALERETURNDETAILS);
        TotalFreeTax = CalculateTaxTotal(dtRETURNFREEQTYDETAILS);

        this.grvFreeProduct.HeaderRow.Cells[4].Text = "PRODUCT";
        this.grvFreeProduct.HeaderRow.Cells[5].Text = "BATCH";
        this.grvFreeProduct.HeaderRow.Cells[6].Text = "MRP";
        this.grvFreeProduct.HeaderRow.Cells[7].Text = "RATE";
        this.grvFreeProduct.HeaderRow.Cells[8].Text = "RETURN QTY(PCS)";
        this.grvFreeProduct.HeaderRow.Cells[10].Text = "SCH(%)";
        this.grvFreeProduct.HeaderRow.Cells[11].Text = "SCH. AMT";
        this.grvFreeProduct.HeaderRow.Cells[12].Text = "SPECIAL DISC";

        this.grvFreeProduct.FooterRow.Cells[7].Text = "Total : ";
        this.grvFreeProduct.FooterRow.Cells[7].Font.Bold = true;
        this.grvFreeProduct.FooterRow.Cells[7].ForeColor = Color.Blue;

        this.grvFreeProduct.HeaderRow.Cells[1].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[2].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[3].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[9].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[13].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[14].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[15].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[16].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[17].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[18].Visible = false;
        this.grvFreeProduct.HeaderRow.Cells[19].Visible = false;

        this.grvFreeProduct.HeaderRow.Cells[1].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[2].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[3].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[4].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[5].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[6].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[7].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[8].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[9].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[10].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[11].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[12].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[13].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[14].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[15].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[16].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[17].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[18].Wrap = false;
        this.grvFreeProduct.HeaderRow.Cells[19].Wrap = false;

        this.grvFreeProduct.FooterRow.Cells[1].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[2].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[3].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[9].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[13].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[14].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[15].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[16].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[17].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[18].Visible = false;
        this.grvFreeProduct.FooterRow.Cells[19].Visible = false;

        this.grvFreeProduct.FooterRow.Cells[1].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[2].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[3].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[4].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[5].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[6].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[7].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[8].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[9].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[10].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[11].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[12].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[13].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[14].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[15].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[16].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[17].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[18].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[19].Wrap = false;

        foreach (GridViewRow row in grvFreeProduct.Rows)
        {
            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[9].Visible = false;
            row.Cells[13].Visible = false;
            row.Cells[14].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[19].Visible = false;

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

            TotalReturnQuantity += Convert.ToDecimal(row.Cells[8].Text.Trim());
            TotalSchemeValue += Convert.ToDecimal(row.Cells[11].Text.Trim());
            TotalReturnAmt += Convert.ToDecimal(row.Cells[12].Text.Trim());

            row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

            int count = 19;
            DataTable dt = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grvFreeProduct.HeaderRow.Cells[count].Wrap = true;
            }
        }

        #region Invoice Qty
        if (TotalReturnQuantity == 0)
        {
            this.grvFreeProduct.FooterRow.Cells[8].Text = "0.00";
        }
        else
        {
            this.grvFreeProduct.FooterRow.Cells[8].Text = TotalReturnQuantity.ToString("#.00");
        }
        this.grvFreeProduct.FooterRow.Cells[8].Font.Bold = true;
        this.grvFreeProduct.FooterRow.Cells[8].ForeColor = Color.Blue;
        this.grvFreeProduct.FooterRow.Cells[8].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Scheme Amount
        if (TotalSchemeValue == 0)
        {
            this.grvFreeProduct.FooterRow.Cells[11].Text = "0.00";
        }
        else
        {
            this.grvFreeProduct.FooterRow.Cells[11].Text = TotalSchemeValue.ToString("#.00");
        }
        this.grvFreeProduct.FooterRow.Cells[11].Font.Bold = true;
        this.grvFreeProduct.FooterRow.Cells[11].ForeColor = Color.Blue;
        this.grvFreeProduct.FooterRow.Cells[11].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Sale Amount
        if (TotalReturnAmt == 0)
        {
            this.grvFreeProduct.FooterRow.Cells[12].Text = "0.00";
        }
        else
        {
            this.grvFreeProduct.FooterRow.Cells[12].Text = TotalReturnAmt.ToString("#.00");
        }
        this.grvFreeProduct.FooterRow.Cells[12].Font.Bold = true;
        this.grvFreeProduct.FooterRow.Cells[12].ForeColor = Color.Blue;
        this.grvFreeProduct.FooterRow.Cells[12].Wrap = false;
        this.grvFreeProduct.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Tax
        int TotalRows = grvFreeProduct.Rows.Count;
        int count1 = 0;

        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
        for (int i = 21; i <= (21 + dtTaxCountDataAddition1.Rows.Count); i += 2)
        {
            double sum = 0.00;

            for (int j = 0; j < TotalRows; j++)
            {
                sum += grvFreeProduct.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(grvFreeProduct.Rows[j].Cells[i].Text) : 0.00;
                grvFreeProduct.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }

            this.grvFreeProduct.FooterRow.Cells[i].Text = sum.ToString("#.00");
            this.grvFreeProduct.FooterRow.Cells[i].Font.Bold = true;
            this.grvFreeProduct.FooterRow.Cells[i].ForeColor = Color.Blue;
            this.grvFreeProduct.FooterRow.Cells[i].Wrap = false;
            this.grvFreeProduct.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            count1 = count1 + 1;
        }

        for (int i = 20; i <= (20 + dtTaxCountDataAddition1.Rows.Count); i += 2)
        {
            for (int j = 0; j < TotalRows; j++)
            {
                grvFreeProduct.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        #endregion

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                grvFreeProduct.FooterRow.Cells[22 + count1].Text = "0.00";
            }
            else
            {
                grvFreeProduct.FooterRow.Cells[22 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grvFreeProduct.FooterRow.Cells[22 + count1].Font.Bold = true;
            grvFreeProduct.FooterRow.Cells[22 + count1].ForeColor = Color.Blue;
            grvFreeProduct.FooterRow.Cells[22 + count1].Wrap = false;
            grvFreeProduct.FooterRow.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grvFreeProduct.Rows)
            {
                row.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grvFreeProduct.HeaderRow.Cells[22 + count1].Text = "NET AMOUNT";
            this.grvFreeProduct.HeaderRow.Cells[22 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grvFreeProduct.FooterRow.Cells[21 + count1].Text = "0.00";
            }
            else
            {
                grvFreeProduct.FooterRow.Cells[21 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grvFreeProduct.FooterRow.Cells[21 + count1].Font.Bold = true;
            grvFreeProduct.FooterRow.Cells[21 + count1].ForeColor = Color.Blue;
            grvFreeProduct.FooterRow.Cells[21 + count1].Wrap = false;
            grvFreeProduct.FooterRow.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grvFreeProduct.Rows)
            {
                row.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grvFreeProduct.HeaderRow.Cells[21 + count1].Text = "NET AMOUNT";
            this.grvFreeProduct.HeaderRow.Cells[21 + count1].Wrap = false;
        }
        #endregion


        this.txtTotDisc.Text = Convert.ToString(String.Format("{0:0.00}", TotalReturnAmt));
        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalBillTax + TotalFreeTax));
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

    #region Gross Amount Calculation
    protected void GrossAmountCalculation()
    {
        decimal TotalNetAmt = 0;
        decimal TotalGrossAmt = 0;
        decimal TotalFinalAmt = 0;
        if (this.txtTotMRP.Text.Trim() == "")
        {
            this.txtTotMRP.Text = "0.00";
        }
        if (this.txtAmount.Text == "")
        {
            this.txtAmount.Text = "0.00";
        }
        if (txtAmount.Text.Trim() != "")
        {
            TotalNetAmt = Convert.ToDecimal(txtAmount.Text.Trim()) + Convert.ToDecimal(txtTotTax.Text.Trim());
        }
        TotalGrossAmt = TotalNetAmt;
        TotalFinalAmt = TotalNetAmt;
        this.txtNetAmt.Text = Convert.ToString(String.Format("{0:0.00}", TotalNetAmt));
        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", TotalNetAmt));
        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim()))));
        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));
    }
    #endregion

    #region TotalReturnQuantityCalculation
    protected void TotalReturnCalculation()
    {
        decimal TotalBillReturnQuantity = 0;

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            TotalBillReturnQuantity += Convert.ToDecimal(row.Cells[8].Text.Trim());
        }


        if (TotalBillReturnQuantity == 0)
        {
            this.txtTotPCS.Text = "0.00";
        }
        else
        {
            this.txtTotPCS.Text = (TotalBillReturnQuantity).ToString("#.00");
        }
    }
    #endregion

    #region rdbTax_CheckedChanged
    protected void rdbTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("ADDPURCHASERETURNDETAILS");
        Session.Remove("PURCHASERETURNTAXCOMPONENTDETAILS");
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grvFreeProduct.DataSource = null;
        this.grvFreeProduct.DataBind();
        this.txtTotMRP.Text = "0.00";
        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
        this.txtTotDisc.Text = "0.00";
    }
    #endregion

    #region rdbNoTax_CheckedChanged
    protected void rdbNoTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("ADDPURCHASERETURNDETAILS");
        Session.Remove("PURCHASERETURNTAXCOMPONENTDETAILS");
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grvFreeProduct.DataSource = null;
        this.grvFreeProduct.DataBind();
        this.txtTotMRP.Text = "0.00";
        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
        this.txtTotDisc.Text = "0.00";
    }
    #endregion

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string Tag = string.Empty;
            Tag = "PURCHASERETURN";
            string upath = "frmPrintPopUp_FAC.aspx?pid=" + hdnDespatchID.Value + "&&BSID=GTMT&&PSID=" + Tag.ToString() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnApprove_Click
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
                int flag = 0;
                string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
                //flag = clsPurchaseStockReceipt.ApprovePurchaseReturn_MM(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtSaleInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
                flag = clsPurchaseStockReceipt.ApprovePurchaseReturn_MM(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtSaleInvoiceNo.Text.Trim(), "", "", this.txtInvoiceDate.Text.Trim(), "", this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), "TRUE", ddlledger.SelectedValue.ToString().Trim());
                this.hdnDespatchID.Value = "";
                if (flag == 1)
                {
                    pnlDisplay.Style["display"] = "";
                    pnlAdd.Style["display"] = "none";
                    LoadPurchaseReturn();
                    MessageBox1.ShowSuccess("Purchase Return: <b><font color='green'>" + this.txtSaleInvoiceNo.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 700);
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
            ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
            int flag = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectSaleReturn(receivedID, this.txtRejectionNote.Text.Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                LoadPurchaseReturn();
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

    #region grdDespatchHeader_RowDataBound
    protected void grdDespatchHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
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
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadLedger
    public void FetchLedger()
    {
        ClsAccGrp clsAccGrp = new ClsAccGrp();
        ddlledger.Items.Clear();
        ddlledger.Items.Add(new ListItem("SELECT LEDGER NAME", "0"));
        ddlledger.AppendDataBoundItems = true;
        ddlledger.DataSource = clsAccGrp.LoadLedger();
        ddlledger.DataValueField = "ID";
        ddlledger.DataTextField = "NAME";
        ddlledger.DataBind();
    }
    #endregion

    #region View Purchase Return
    protected void Btn_View(string _VoucherID)
    {
        try
        {
            hdnDespatchID.Value = _VoucherID;
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataSet ds = new DataSet();
            ResetSession();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.divsave.Style["display"] = "none";
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            #region Disable Controls
            //this.ImageButton1.Enabled = false;
            this.ddlDepot.Enabled = false;
            this.ddlVendor.Enabled = false;
            this.ddlSuppliedItem.Enabled = false;
            #endregion

            this.Displayonoff("V");

            if (HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }
            if (HttpContext.Current.Session["PURCHASERETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }

            string PurchaseReturnID = Convert.ToString(_VoucherID);
            ds = clsReturn.EditPurchaseReturnDetails(PurchaseReturnID);
            DataTable dtRetunInvoiceEditProductDetails = (DataTable)Session["ADDPURCHASERETURNDETAILS"];

            #region Header Table Information
            if (ds.Tables["HEADER"].Rows.Count > 0)
            {
                this.LoadMotherDepot();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNNO"]).Trim();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DEPOTID"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["REMARKS"]).Trim();
                this.LoadSuppliedItem();
                this.ddlSuppliedItem.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SUPPLIEDITEMID"]).Trim();
                this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());
                this.ddlVendor.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DISTRIBUTORID"]).Trim();
                this.txtTotPCS.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["TOTALPCS"]).Trim();
            }
            #endregion

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtPurchaseInvoiceTaxCount"];
            if (dtTaxCountDataAddition.Rows.Count > 0)
            {
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
            }
            #endregion

            #region Item-wise Tax
            if (ds.Tables["TAXDETAILS"].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["PURCHASERETURNTAXCOMPONENTDETAILS"];
                for (int i = 0; i < ds.Tables["TAXDETAILS"].Rows.Count; i++)
                {
                    DataRow dr = dtTaxComponentEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["SALEINVOICEID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["SALEINVOICEID"]);
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["PRODUCTID"]);
                    dr["BATCHNO"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["BATCHNO"]);
                    dr["TAXID"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXID"]);
                    dr["TAXPERCENTAGE"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXPERCENTAGE"]);
                    dr["TAXVALUE"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["TAXVALUE"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["PURCHASEINVTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Bill Quantity
            if (ds.Tables["BILLDETAILS"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["BILLDETAILS"].Rows.Count; i++)
                {
                    if (Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["FREETAG"]).Trim() == "0")
                    {
                        DataRow drEditReturnInvoiceDetails = dtRetunInvoiceEditProductDetails.NewRow();
                        drEditReturnInvoiceDetails["GUID"] = Guid.NewGuid();
                        drEditReturnInvoiceDetails["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["STOCKRECEIVEDID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTNAME"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTNAME"]).Trim();
                        drEditReturnInvoiceDetails["BATCHNO"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim();
                        drEditReturnInvoiceDetails["MRP"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MRP"]).Trim();
                        drEditReturnInvoiceDetails["RATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RATE"]).Trim();
                        drEditReturnInvoiceDetails["RETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["AMOUNT"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"]).Trim();
                        drEditReturnInvoiceDetails["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ALREADYRETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["REMAININGQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["REMAININGQTY"]).Trim();
                        drEditReturnInvoiceDetails["MFDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MFDATE"]).Trim();
                        drEditReturnInvoiceDetails["EXPRDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["EXPRDATE"]).Trim();
                        drEditReturnInvoiceDetails["FREETAG"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["FREETAG"]).Trim();
                        drEditReturnInvoiceDetails["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                        drEditReturnInvoiceDetails["INVOICEQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["INVOICEQTY"]).Trim();
                        decimal BillTaxAmt = 0;
                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["RELATEDTO"]))
                            {

                                case "1":
                                    TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]), Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim());
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                        }
                        #endregion
                        drEditReturnInvoiceDetails["NETAMOUNT"] = Convert.ToString(BillTaxAmt + Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()));
                        dtRetunInvoiceEditProductDetails.Rows.Add(drEditReturnInvoiceDetails);
                        dtRetunInvoiceEditProductDetails.AcceptChanges();
                    }
                }
            }
            #endregion

            #region Footer Details
            if (ds.Tables["FOOTERDETAILS"].Rows.Count > 0)
            {
                this.txtAmount.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TOTALSALEINVOICEVALUE"]).Trim();
                this.txtTotalGross.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["GROSSAMTOUNT"]).Trim();
                this.txtRoundoff.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["ROUNDOFFVALUE"]).Trim();
                this.txtFinalAmt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["NETAMOUNT"]).Trim();
            }
            #endregion

            #region Bind Grid
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtRetunInvoiceEditProductDetails;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = null;
            }
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            #endregion

            HttpContext.Current.Session["ADDPURCHASERETURNDETAILS"] = dtRetunInvoiceEditProductDetails;
        }
        catch (Exception ex)
        {
            /*string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);*/
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
            CalendarFromDate.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
                CalendarExtender3.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtInvoiceDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region LoadTransporter
    public void LoadTransporter()
    {
        try
        {
            DataTable dtTransporter = new DataTable();
            ClsPurchaseReturn_MM clsreturn = new ClsPurchaseReturn_MM();
            dtTransporter = clsreturn.BindTPU_Transporter(Session["DEPOTID"].ToString());
            if (dtTransporter.Rows.Count > 0)
            {
                this.ddltransporter.Items.Clear();
                this.ddltransporter.Items.Add(new ListItem("Select Transporter", "0"));
                this.ddltransporter.AppendDataBoundItems = true;
                this.ddltransporter.DataSource = dtTransporter;
                this.ddltransporter.DataValueField = "ID";
                this.ddltransporter.DataTextField = "NAME";
                this.ddltransporter.DataBind();

                if (dtTransporter.Rows.Count == 1)
                {
                    this.ddltransporter.SelectedValue = Convert.ToString(dtTransporter.Rows[0]["ID"]);
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadGrn Rejection
    public void LoadGrnRejection()
    {
        try
        {
            string mode = "QCREJECT";
            DataTable dtObj = new DataTable();
            ClsPurchaseReturn_MM clsreturn = new ClsPurchaseReturn_MM();
            dtObj = clsreturn.Bind_GrnRejection(mode,Session["DEPOTID"].ToString());
            if (dtObj.Rows.Count > 0)
            {
                this.ddlGrnRejectionNo.Items.Clear();
                this.ddlGrnRejectionNo.Items.Add(new ListItem("Select Qc Rejection", "NA"));
                this.ddlGrnRejectionNo.AppendDataBoundItems = true;
                this.ddlGrnRejectionNo.DataSource = dtObj;
                this.ddlGrnRejectionNo.DataValueField = "QCID";
                this.ddlGrnRejectionNo.DataTextField = "QCNO";
                this.ddlGrnRejectionNo.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadVendorInvoice_RejectionWise
    public void LoadVendorInvoice_RejectionWise(string RejectionID, string VendorID, string DepotID)
    {
        try
        {
            ClsPurchaseReturn_MM clsReturn = new ClsPurchaseReturn_MM();
            DataTable dtinvoice = new DataTable();
            dtinvoice = clsReturn.BindVendorInvoice(RejectionID, VendorID, DepotID);

            List<Purchasedetails> InvoiceDetails = new List<Purchasedetails>();
            if (dtinvoice.Rows.Count > 0)
            {
                this.ddlretailerinvoice.Items.Clear();
                this.ddlretailerinvoice.Items.Add(new ListItem("Select Invoice No", "0"));

                for (int i = 0; i < dtinvoice.Rows.Count; i++)
                {
                    Purchasedetails po = new Purchasedetails();
                    po.INVOICEID = dtinvoice.Rows[i]["INVOICEID"].ToString();
                    po.INVOICENO = dtinvoice.Rows[i]["INVOICENO"].ToString();
                    po.INVOICEDATE = dtinvoice.Rows[i]["INVOICEDATE"].ToString();
                    po.NETAMOUNT = dtinvoice.Rows[i]["NETAMOUNT"].ToString();
                    InvoiceDetails.Add(po);
                }

                //---------------------------------------------------------------------------------------------

                string text1 = string.Format("{0}{1}{2}",
                "INVOICE NO".PadRight(58, '\u00A0'),
                "INVOICE DATE".PadRight(15, '\u00A0'),
                "NET AMOUNT".PadRight(11, '\u00A0')
                );

                ddlretailerinvoice.Items.Add(new ListItem(text1, "1"));

                foreach (ListItem item in ddlretailerinvoice.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Attributes.Add("disabled", "disabled");
                        item.Attributes.CssStyle.Add("color", "blue");
                        item.Attributes.CssStyle.Add("style", "background-color:red;");
                    }
                }

                foreach (Purchasedetails p in InvoiceDetails)
                {
                    string text = string.Format("{0}{1}{2}",
                        p.INVOICENO.PadRight(58, '\u00A0'),
                        p.INVOICEDATE.PadRight(15, '\u00A0'),
                        p.NETAMOUNT.PadRight(11, '\u00A0')
                        );

                    ddlretailerinvoice.Items.Add(new ListItem(text, "" + p.INVOICEID + ""));
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion
}
