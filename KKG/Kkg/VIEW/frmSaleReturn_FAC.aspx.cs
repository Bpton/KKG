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
using WorkFlow;
public class Invoicedetails
{
    public string INVOICEID { get; set; }
    public string INVOICENO { get; set; }
    public string INVOICEDATE { get; set; }
    public string NETAMOUNT { get; set; }
}
public partial class FACTORY_frmSaleReturn_FAC : System.Web.UI.Page
{
    string menuID = string.Empty;
    string bsID = string.Empty;
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
                    btnaddhide.Style["display"] = "none";
                    this.divsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                }
                else
                {
                    btnaddhide.Style["display"] = "";
                    this.divsave.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                }
                #endregion

                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                txtFromDate.Style.Add("color", "black !important");
                txtToDate.Style.Add("color", "black !important");
                /*txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtinvoicefromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtinvoicetodate.Text = dtcurr.ToString(date).Replace('-', '/');*/
                /*Calender Control Date Range*/
                CalendarExtender3.EndDate = DateTime.Now;
                /****************************/
                ViewState["menuID"] = menuID;
                ViewState["BSID"] = Convert.ToString("33F6AC5E-1F37-4B0F-B959-D1C900BB43A5").Trim();        /* BS - OTHERTS */
                this.LoadSaleReturnInvoice();
                this.DateLock();
            }
            else
            {
                ViewState["BSID"] = Convert.ToString("33F6AC5E-1F37-4B0F-B959-D1C900BB43A5").Trim();        /* BS - OTHERTS */
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "";
                this.divsave.Visible = false;
                this.divbtnCancel.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.trBtn.Visible = false;
                Btn_View(Request.QueryString["InvId"]);
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
        

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdinvoicedetails.ClientID + "', 450, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region LoadBUSINESSSEGMENT
    public void LoadBUSINESSSEGMENT(string bstype)
    {
        ClsSaleReturn_FAC clsreturn = new ClsSaleReturn_FAC();
        string bsid = string.Empty;
        bsid = clsreturn.BindBUSINESSSEGMENT(bstype);
        hdn_bsid.Value = bsid;
        hdn_bsname.Value = bstype;
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
        if (this.ddlgroup.SelectedValue != "0")
        {
            DataTable dtCustomer = new DataTable();
            ClsSaleReturn_FAC clsreturn = new ClsSaleReturn_FAC();
            dtCustomer = clsreturn.BindDepotCustomer(Convert.ToString(Session["BSID"]).Trim(), ddlgroup.SelectedValue, Convert.ToString(Session["DEPOTID"]).Trim(), Convert.ToString(hdnDespatchID.Value).Trim());
            if (dtCustomer.Rows.Count > 0)
            {
                if (dtCustomer.Rows.Count > 1)
                {
                    ddlDistributor.Items.Clear();
                    ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    ddlDistributor.AppendDataBoundItems = true;
                    ddlDistributor.DataSource = dtCustomer;
                    ddlDistributor.DataValueField = "CUSTOMERID";
                    ddlDistributor.DataTextField = "CUSTOMERNAME";
                    ddlDistributor.DataBind();
                }
                else if (dtCustomer.Rows.Count == 1)
                {
                    ddlDistributor.Items.Clear();
                    ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    ddlDistributor.DataSource = dtCustomer;
                    ddlDistributor.DataValueField = "CUSTOMERID";
                    ddlDistributor.DataTextField = "CUSTOMERNAME";
                    ddlDistributor.DataBind();
                    ddlDistributor.SelectedValue = Convert.ToString(dtCustomer.Rows[0]["CUSTOMERID"]);
                    //this.CreateStructure();
                }
            }
            else
            {
                ddlDistributor.Items.Clear();
                ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                ddlDistributor.AppendDataBoundItems = true;
            }
        }
        else
        {
            ddlDistributor.Items.Clear();
            ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
            ddlDistributor.AppendDataBoundItems = true;
        }
    }
    #endregion


    public void LoadBusinessSegment()
    {
        try
        {
            ClsSaleReturn_FAC ClsRetailerSaleOrder = new ClsSaleReturn_FAC();
            DataTable dt = ClsRetailerSaleOrder.BindBusinessegment();
            this.ddlbssegment.Items.Clear();
            this.ddlbssegment.Items.Add(new ListItem("Select bs Name", "0"));
            ddlbssegment.AppendDataBoundItems = true;
            ddlbssegment.DataSource = dt;
            ddlbssegment.DataValueField = "BSID";
            ddlbssegment.DataTextField = "BSNAME";
            ddlbssegment.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

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


    public void LoadPrincipleGroup1()
    {
        ClsSaleReturn_FAC ClsRetailerSaleOrder = new ClsSaleReturn_FAC();
        DataTable dtGroup = new DataTable();
        dtGroup = ClsRetailerSaleOrder.BindGroup(ddlbssegment.SelectedValue);
        //dtGroup = ClsRetailerSaleOrder.BindGroup(bstype);
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
            }
        }
        else
        {
            this.ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
            this.ddlgroup.AppendDataBoundItems = true;
        }
    }




    #region LoadPrincipleGroup
    public void LoadPrincipleGroup(string bstype)
    {
        ClsSaleReturn_FAC ClsRetailerSaleOrder = new ClsSaleReturn_FAC();
        DataTable dtGroup = new DataTable();
        dtGroup = ClsRetailerSaleOrder.BindGroup(bstype, HttpContext.Current.Session["IUserID"].ToString(), "F");
        //dtGroup = ClsRetailerSaleOrder.BindGroup(bstype);
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
            ClsSaleReturn_FAC clsreport = new ClsSaleReturn_FAC();
            DataTable dtDepot = new DataTable();
            dtDepot = clsreport.BindDepo(HttpContext.Current.Session["DEPOTID"].ToString());
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
        loadadd();
    }
    #endregion

    #region BindDistributor
    protected void BindDistributor()
    {
        if (ViewState["BSID"].ToString() != "")
        {
            DataTable dtCustomer = new DataTable();
            ClsSaleReturn_FAC clsreturn = new ClsSaleReturn_FAC();
            dtCustomer = clsreturn.BindCustomer(ViewState["BSID"].ToString());
            if (this.hdnDespatchID.Value == "")
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    if (dtCustomer.Rows.Count > 1)
                    {
                        ddlDepot.Items.Clear();
                        ddlDepot.Items.Add(new ListItem("Select Customer", "0"));
                        ddlDepot.AppendDataBoundItems = true;
                        ddlDepot.DataSource = dtCustomer;
                        ddlDepot.DataValueField = "CUSTOMERID";
                        ddlDepot.DataTextField = "CUSTOMERNAME";
                        ddlDepot.DataBind();
                    }
                    else if (dtCustomer.Rows.Count == 1)
                    {
                        ddlDepot.Items.Clear();
                        ddlDepot.Items.Add(new ListItem("Select Customer", "0"));
                        ddlDepot.DataSource = dtCustomer;
                        ddlDepot.DataValueField = "CUSTOMERID";
                        ddlDepot.DataTextField = "CUSTOMERNAME";
                        ddlDepot.DataBind();
                        ddlDepot.SelectedValue = Convert.ToString(dtCustomer.Rows[0]["CUSTOMERID"]);
                        //this.CreateStructure();
                    }
                }
                else
                {
                    ddlDepot.Items.Clear();
                    ddlDepot.Items.Add(new ListItem("Select Customer", "0"));
                    ddlDepot.AppendDataBoundItems = true;
                }

            }
            else if (this.hdnDespatchID.Value != "")
            {
                ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
                string saleInvoiceID = Convert.ToString(hdnDespatchID.Value).Trim();
                DataSet ds = new DataSet();
                ds = clsReturn.EditInvoiceDetails(saleInvoiceID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistributor.Items.Clear();
                    ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
                    ddlDistributor.AppendDataBoundItems = true;
                    ddlDistributor.DataSource = dtCustomer;
                    ddlDistributor.DataValueField = "CUSTOMERID";
                    ddlDistributor.DataTextField = "CUSTOMERNAME";
                    ddlDistributor.DataBind();
                    this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DISTRIBUTORID"]).Trim();
                }
            }
        }
        else
        {
            ddlDistributor.Items.Clear();
            ddlDistributor.Items.Add(new ListItem("Select Customer", "0"));
            ddlDistributor.AppendDataBoundItems = true;
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
            

            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            string BILLGUID = gvr.Cells[1].Text.Trim();
            DataTable dtdeleteInvoicerecord = new DataTable();
            dtdeleteInvoicerecord = (DataTable)Session["SALERETURNDETAILS"];
            int Taxflag = 0;

            DataRow[] drr = dtdeleteInvoicerecord.Select("GUID='" + BILLGUID.Trim() + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteInvoicerecord.AcceptChanges();
            }
            HttpContext.Current.Session["SALERETURNDETAILS"] = dtdeleteInvoicerecord;
            

            #region Loop For Adding Itemwise Tax
            DataTable dtTaxCount = (DataTable)Session["dtInvoiceTaxCount"];

            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                Taxflag = 0;
                if (Arry.Count > 0)
                {
                    foreach (string row in Arry)
                    {
                        if (row.Contains(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString().Trim()))
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
                        Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                    }
                }
                else
                {
                    Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                }
            }
            #endregion

            DataTable dtdeleteItemWiseTax = new DataTable();
            dtdeleteItemWiseTax = (DataTable)Session["RETURNTAXCOMPONENTDETAILS"];
            DataRow[] drrTax = dtdeleteItemWiseTax.Select("GUID='" + BILLGUID.Trim() + "'");
            for (int i = 0; i < drrTax.Length; i++)
            {
                drrTax[i].Delete();
                dtdeleteItemWiseTax.AcceptChanges();
            }
            HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] = dtdeleteItemWiseTax;

            this.GrossAmountCalculation();

            if (dtdeleteInvoicerecord.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtdeleteInvoicerecord;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
            }
            else
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["SALERETURNDETAILS"] = null;
                this.txtAmount.Text = "0.00";
                this.txtNetAmt.Text = "0.00";
                this.txtTotalGross.Text = "0.00";
                this.txtRoundoff.Text = "0.00";
                this.txtFinalAmt.Text = "0.00";
                this.txttcsamt.Text = "0.00";
                this.txttcspercent.Text = "0.00";
                this.txttcs.Text = "0.00";
                this.txttcsnet.Text = "0.00";
            }

            if (dtdeleteInvoicerecord.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            if (dtdeleteInvoicerecord.Rows.Count == 0)
            {
                this.txtTotPCS.Text = "0";
                this.txtAmount.Text = "0";
                this.txtTotTax.Text = "0";
                this.txtNetAmt.Text = "0";
                this.txtTotalGross.Text = "0";
                this.txtRoundoff.Text = "0";
                this.txtFinalAmt.Text = "0";
                this.txttcsamt.Text = "0.00";
                this.txttcspercent.Text = "0.00";
                this.txttcs.Text = "0.00";
                this.txttcsnet.Text = "0.00";
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

    #region Delete Sale Retun
    protected void DeleteRecordInvoice(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            string ReturnStatus = string.Empty;
            if (clsReturn.GetFinancestatus(e.Record["SALERETURNID"].ToString().Trim()) == "1")
            {
                MessageBox1.ShowInfo("Finance posting already done,not allow to delete.", 50, 450);
                return;
            }
            /*int flag = 0;
            flag = clsReturn.InvoiceDelete(e.Record["SALERETURNID"].ToString().Trim());*/
            ReturnStatus = clsReturn.InvoiceDelete(e.Record["SALERETURNID"].ToString().Trim());
            this.hdnDespatchID.Value = "";

            /*if (flag == 1)
            {
                this.LoadSaleReturnInvoice();
                e.Record["Error"] = "Record Deleted Successfully. ";
            }
            else
            {
                e.Record["Error"] = "Error On Deleting. ";
            }*/

            if (ReturnStatus == "1")
            {
                this.LoadSaleReturnInvoice();
                e.Record["Error"] = "Record Deleted Successfully. ";
            }
            else if (ReturnStatus == "0")
            {
                e.Record["Error"] = "Error On Deleting... ";
            }
            else
            {
                String[] Value = ReturnStatus.Split('|');
                string Product = Value[1].Trim();
                string Batch = Value[2].Trim();
                e.Record["Error"] = "Not allow to delete,Stock will be Negative for  " + Product + " of  " + Batch + "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Save Sale Return
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtInvoiceDate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
                string RetunInvoiceNo = string.Empty;
                string xml = string.Empty;
                string xmlTax = string.Empty;
                string xmlFreeProduct = string.Empty;
                string InvoiceType = string.Empty;
                DataTable dtRecordsCheck = new DataTable();
                DataTable dtItemWiseTax = new DataTable();
                DataTable dtTaxCount = new DataTable();

                if (Session["dtInvoiceTaxCount"] != null)
                {
                    dtTaxCount = (DataTable)Session["dtInvoiceTaxCount"]; //dtInvoiceTaxCount
                }

                if (Session["SALERETURNDETAILS"] != null)
                {
                    dtRecordsCheck = (DataTable)HttpContext.Current.Session["SALERETURNDETAILS"];
                }
                if (Session["RETURNTAXCOMPONENTDETAILS"] != null)
                {
                    dtItemWiseTax = (DataTable)HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"];
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

                RetunInvoiceNo = clsReturn.InsertReturnInvoiceDetails(this.txtInvoiceDate.Text.Trim(), this.ddlDistributor.SelectedValue.Trim(),
                                                                      Convert.ToString(this.ddlDistributor.SelectedItem).Trim(),
                                                                      this.ddlDepot.SelectedValue, Convert.ToString(this.ddlDepot.SelectedItem).Trim(),
                                                                      HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(),
                                                                      this.txtRemarks.Text.Trim(), Convert.ToString(ViewState["BSID"]).Trim(),
                                                                      Convert.ToString(this.ddlgroup.SelectedValue).Trim(), TotalPCS, TotalReturnInvoiceValue, OtherCharges,
                                                                      Adjustment, Roundoff, SpecialDisc, NetAmt, GrossAmt, xml, xmlTax,
                                                                      Convert.ToString(hdnDespatchID.Value), InvoiceType,
                                                                      Convert.ToDecimal(txttcsamt.Text.Trim()), Convert.ToDecimal(this.txttcspercent.Text.Trim()), Convert.ToDecimal(this.txttcs.Text.Trim()),
                                                                      Convert.ToDecimal(this.txttcsnet.Text.Trim()), this.ddlbssegment.SelectedValue.Trim(), Convert.ToString(this.ddlbssegment.SelectedItem).Trim(),this.ddlstorelocation.SelectedValue.Trim(),
                                                                      Request.QueryString["MENUID"].ToString().Trim());

                if (RetunInvoiceNo != "")
                {
                    this.grdAddDespatch.DataSource = null;
                    this.grdAddDespatch.DataBind();

                    if (Convert.ToString(hdnDespatchID.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Return Invoice No :<b><font color='green'>  " + RetunInvoiceNo + "</font></b>  Saved Successfully", 80, 600);


                        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        this.ClearControls();
                        this.LoadSaleReturnInvoice();

                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
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
            else
            {
                MessageBox1.ShowInfo("Entry Date is Locked, Please Contact to Admin", 60, 500);
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
        ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
        clsReturn.ResetDataTables();
        this.ClearControls();
        this.ResetSession();
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.grdinvoicedetails.DataSource = null;
        this.grdinvoicedetails.DataBind();
        this.hdnDespatchID.Value = "";
        this.divsave.Style["display"] = "";
        this.LoadSaleReturnInvoice();

        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = false;
        this.ddlDistributor.Enabled = true;
        this.ddlgroup.Enabled = true;

        #endregion
    }
    #endregion

    #region ResetSession
    public void ResetSession()
    {
        Session.Remove("SALERETURNDETAILS");
        Session.Remove("RETURNFREEQTYDETAILS");
        Session.Remove("RETURNTAXCOMPONENTDETAILS");
    }
    #endregion

    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadSaleReturnInvoice();
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
            ResetSession();
            clsDespatchStock clsDespatchStck = new clsDespatchStock();
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataSet ds = new DataSet();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.divsave.Style["display"] = "none";
            this.LoadStore1();
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            #region Disable Controls
            //this.ImageButton1.Enabled = false;
            this.ddlDepot.Enabled = false;
            this.ddlDistributor.Enabled = false;
            this.ddlgroup.Enabled = false;
            this.ddlbssegment.Enabled = false;
            this.ddlstorelocation.Enabled = false;
            #endregion

            this.Displayonoff("V");

            if (HttpContext.Current.Session["SALERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }
            if (HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }
            this.LoadPrincipleGroup(ViewState["BSID"].ToString());
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsReturn.EditInvoiceDetails(SaleReturnID);
            DataTable dtRetunInvoiceEditProductDetails = (DataTable)Session["SALERETURNDETAILS"];

            this.ddlstorelocation.SelectedValue = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[0]["STORELOCATIONID"]).Trim();
            #region Header Table Information
            if (ds.Tables["HEADER"].Rows.Count > 0)
            {
                this.LoadMotherDepot();
                this.LoadBusinessSegment();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNNO"]).Trim();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DEPOTID"]).Trim();
                this.ddlbssegment.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["BUSINESSSEGMENTID"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["REMARKS"]).Trim();
                this.ddlgroup.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["GROUPID"]).Trim();
                this.BindCustomer();
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DISTRIBUTORID"]).Trim();
                
                this.txtTotPCS.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["TOTALPCS"]).Trim();
            }
            #endregion

            #region Item-wise Tax 
            if (ds.Tables["TAXDETAILS"].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RETURNTAXCOMPONENTDETAILS"];
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
                    dr["MRP"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"].ToString());
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
                        drEditReturnInvoiceDetails["SALEINVOICEID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["SALEINVOICEID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTNAME"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTNAME"]).Trim();
                        drEditReturnInvoiceDetails["HSNCODE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["HSNCODE"]).Trim();
                        drEditReturnInvoiceDetails["BATCHNO"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim();
                        drEditReturnInvoiceDetails["MRP"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MRP"]).Trim();
                        drEditReturnInvoiceDetails["RATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RATE"]).Trim();
                        drEditReturnInvoiceDetails["RETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["AMOUNT"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"]).Trim();
                        drEditReturnInvoiceDetails["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ALREADYRETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["REMAININGQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["REMAININGQTY"]).Trim();
                        drEditReturnInvoiceDetails["MFDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MFDATE"]).Trim();
                        drEditReturnInvoiceDetails["EXPRDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["EXPRDATE"]).Trim();
                        drEditReturnInvoiceDetails["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                        drEditReturnInvoiceDetails["INVOICEQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["INVOICEQTY"]).Trim();
                        decimal BillTaxAmt = 0;
                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["RELATEDTO"]))
                            {
                                case "1":
                                    TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"].ToString());
                                    //ProductWiseTax = clsReturn.GetHSNTaxOnEdit(SaleReturnID, TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim());
                                    ProductWiseTax = clsReturn.GetHSNTaxOnEdit_Return(SaleReturnID, TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim());
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    double TaxValue = 0;
                                    TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()) * ProductWiseTax) / 100) * 100) / 100);
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                    BillTaxAmt += Convert.ToDecimal(drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""]);
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
                this.txtRoundoff.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["ROUNDOFFVALUE"]).Trim();
                this.txtFinalAmt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["NETAMOUNT"]).Trim();
                this.txttcspercent.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TCSPERCENT"]).Trim();
                this.txttcs.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TCSAMOUNT"]).Trim();
                this.txttcsnet.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TCSNETAMOUNT"]).Trim();
                this.txttcsamt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["TCSAPPLICABLE_AMOUNT"]).Trim();
                this.txtNetAmt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["GROSSAMTOUNT"]).Trim();


                if (txtAmount.Text.Trim() != "")
                {
                    // added by Subhodip De on 30.01.2018 for txtTotTax.Text was empty
                    if (string.IsNullOrEmpty(this.txtTotTax.Text.Trim()))
                    {
                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim())));
                    }
                    else
                    {
                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim()) + Convert.ToDecimal(txtTotTax.Text.Trim())));
                    }
                }
            }
            #endregion

            #region Invoice No
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.txtSaleNo.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEINVOICENO"]).Trim();
                this.txtSaleDate.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEINVOICEDATE"]).Trim();
                this.txtSaleAmt.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEAMT"]).Trim();
            }
            else
            {
                this.txtSaleNo.Text = "";
                this.txtSaleDate.Text = "";
                this.txtSaleAmt.Text = "";
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
                HttpContext.Current.Session["SALERETURNDETAILS"] = null;
            }
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                //this.GrossAmountCalculation();
            }
            #endregion

            HttpContext.Current.Session["SALERETURNDETAILS"] = dtRetunInvoiceEditProductDetails;
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
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataTable dtCustomer = new DataTable();
            dtCustomer = clsReturn.BindCustomer();
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
            this.grdinvoicedetails.DataSource = null;
            this.grdinvoicedetails.DataBind();
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();

            Session.Remove("SALEINVOICEDETAILS");
            Session.Remove("INVOICETAXCOMPONENTDETAILS");
            Session.Remove("RETURNTAXCOMPONENTDETAILS");
            Session.Remove("dtInvoiceTaxCount");
            Session.Remove("SALEINVOICEDETAILSRET");
            Session.Remove("SALERETURNDETAILS");
            this.txtAmount.Text = "";
            this.txtTotTax.Text = "";
            this.txtNetAmt.Text = "";
            this.txtRoundoff.Text = "";
            this.txtTotPCS.Text = "";
            this.txtTotalGross.Text = "";
            this.txtFinalAmt.Text = "";
            //this.ClearControls();
            this.LoadRetailerInvoice(this.ddlDistributor.SelectedValue.Trim(), this.txtinvoicefromdate.Text.Trim(), this.txtinvoicetodate.Text.Trim(), Session["DEPOTID"].ToString());
        }
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdnDespatchID.Value = "";
        this.hdnSaleInvoiceDate.Value = "";
        this.Hdn_EntryFrom.Value = "";
        this.txtInvoiceDate.Text = "";
        this.txtSaleInvoiceNo.Text = "";
        this.ddlDepot.SelectedValue = "0";
        this.txtRemarks.Text = "";
        this.ddlDistributor.SelectedValue = "0";
        this.ddlretailerinvoice.Items.Clear();
        this.ddlretailerinvoice.Items.Add(new ListItem("Select SaleInvoice No", "0"));
        this.ddlretailerinvoice.AppendDataBoundItems = true;
        this.txtAdj.Text = "0";
        this.txtOtherCharge.Text = "0";
        this.txtTotalGross.Text = "";
        this.txtAmount.Text = "";
        this.txtTotTax.Text = "";
        this.txtNetAmt.Text = "";
        this.txtRoundoff.Text = "";
        this.txtTotPCS.Text = "";
        this.ddlgroup.SelectedValue = "0";
        this.txtFinalAmt.Text = "";
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();

        this.grdinvoicedetails.DataSource = null;
        this.grdinvoicedetails.DataBind();
        this.txtinvoicefromdate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtinvoicetodate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.txtSaleNo.Text = "";
        this.txtSaleDate.Text = "";
        this.txtSaleAmt.Text = "";
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        if (hdnDespatchID.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //2
            dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));        //3
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //4
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //5
            dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));              //6
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //7
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //8
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                 //9
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));           //10
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //11
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));     //12
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));         //13
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //14
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //15
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //16

            #region Loop For Adding Itemwise Tax Component
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataSet ds = new DataSet();
            ds = clsReturn.BindInvoiceProductDetails(this.ddlretailerinvoice.SelectedValue.Trim());

            Session["dtInvoiceTaxCount"] = ds.Tables["TaxCount"];
            for (int k = 0; k < ds.Tables["TaxCount"].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["INPUT_TAXNAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["INPUT_TAXNAME"]) + "", typeof(string)));
            }
            #endregion

            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //2
            dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));        //3
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //4
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //5
            dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));              //6
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //7
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //8
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                 //9
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));           //10            
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //11
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));     //12
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));         //13
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //14
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //15            
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //16

            #region Loop For Adding Itemwise Tax Component
            DataSet ds = new DataSet();
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            ds = clsReturn.EditInvoiceDetails(SaleReturnID);
            Session["dtInvoiceTaxCount"] = ds.Tables["TAXCOUNT"];
            if (ds.Tables["TAXCOUNT"].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables["TAXCOUNT"].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["INPUT_TAXNAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["INPUT_TAXNAME"]) + "", typeof(string)));
                }
            }
            #endregion

            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        HttpContext.Current.Session["SALEINVOICEDETAILSRET"] = dt;
        return dt;
    }
    #endregion

    #region Create Data Table Invoice Tax Component Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("PRIMARYPRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRIMARYPRODUCTBATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("TAG", typeof(string)));
        HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dt;
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
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] = dt;
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
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //1
            dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));        //2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //4
            dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));              //5
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //7
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                 //8
            dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));            //9 
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //10
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));     //11
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));         //12
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //13
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //14
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //15
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));           //16
            #region Loop For Adding Itemwise Tax Component
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataSet ds = new DataSet();
            ds = clsReturn.BindInvoiceProductDetails(this.ddlretailerinvoice.SelectedValue.Trim());

            Session["dtInvoiceTaxCount"] = ds.Tables["TaxCount"];
            for (int k = 0; k < ds.Tables["TaxCount"].Rows.Count; k++)
            {
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["INPUT_TAXNAME"]) + "" + "(%)", typeof(string)));
                dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCount"].Rows[k]["INPUT_TAXNAME"]) + "", typeof(string)));
            }
            #endregion
            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //1
            dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));        //2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //4
            dt.Columns.Add(new DataColumn("HSNCODE", typeof(string)));              //5
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //7
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                 //8
            dt.Columns.Add(new DataColumn("RETURNQTY", typeof(string)));            //9 
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //10
            dt.Columns.Add(new DataColumn("ALREADYRETURNQTY", typeof(string)));     //11
            dt.Columns.Add(new DataColumn("REMAININGQTY", typeof(string)));         //12
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //13
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //14
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //15
            dt.Columns.Add(new DataColumn("INVOICEQTY", typeof(string)));           //16
            #region Loop For Adding Itemwise Tax Component
            DataSet ds = new DataSet();
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            ds = clsReturn.EditInvoiceDetails(SaleReturnID);
            Session["dtInvoiceTaxCount"] = ds.Tables["TAXCOUNT"];
            if (ds.Tables["TAXCOUNT"].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables["TAXCOUNT"].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["INPUT_TAXNAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["INPUT_TAXNAME"]) + "", typeof(string)));
                }
            }
            #endregion
            dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        }
        HttpContext.Current.Session["SALERETURNDETAILS"] = dt;
        return dt;
    }
    #endregion    

    #region CreateTaxDatatable
    void CreateTaxDatatable(string GUID, string SALEINVOICEID, string PRODUCTID, string BATCH, string TAXID, string TAXPERCENTAGE, string TAXVALUE, string MRP)
    {
        DataTable dt = (DataTable)Session["RETURNTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["GUID"] = GUID;
        dr["SALEINVOICEID"] = SALEINVOICEID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["TAXPERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = TAXVALUE;
        dr["MRP"] = MRP;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    #region LoadSaleReturnInvoice
    public void LoadSaleReturnInvoice()
    {
        try
        {
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            string Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.grdDespatchHeader.DataSource = clsReturn.BindSaleReturn(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), ViewState["BSID"].ToString().Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim(), Checker, HttpContext.Current.Session["UserID"].ToString().Trim());
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
            this.LoadRetailerInvoice(this.ddlDistributor.SelectedValue.Trim(), this.txtinvoicefromdate.Text.Trim(), this.txtinvoicetodate.Text.Trim(), Session["DEPOTID"].ToString());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Remove SaleInvoice Session
    protected void RemoveSaleInvoiceSession()
    {
        Session.Remove("SALEINVOICEDETAILS");
        Session.Remove("INVOICETAXCOMPONENTDETAILS");
    }
    #endregion

    #region ddlretailerinvoice_SelectedIndexChanged
    protected void ddlretailerinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlretailerinvoice.SelectedValue != "0")
            {
                this.RemoveSaleInvoiceSession();
                this.CreateDataTable();
                this.CreateDataTableTaxComponent();
                LoadInvoiceProductGrid(this.ddlretailerinvoice.SelectedValue.Trim());
            }
            else
            {
                this.grdinvoicedetails.DataSource = null;
                this.grdinvoicedetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    //public void LoadtcsGrid(string InvoiceID)
    //{
    //    ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
    //    DataTable dt = new DataTable();
    //    dt = clsReturn.BindTcsDetails(InvoiceID);
    //    //if(dt.[].rows.count)
    //}


        #region LoadInvoiceProductGrid
        public void LoadInvoiceProductGrid(string InvoiceID)
    {
        ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
        string TAXID = string.Empty;
        decimal ProductWiseTax = 0;
        decimal NetAmount = 0;

        DataSet ds = new DataSet();
        ds = clsReturn.BindInvoiceProductDetails(InvoiceID);

        #region Invoice Header Information
        if (ds.Tables["Header"].Rows.Count > 0)
        {
            this.hdnSaleInvoiceDate.Value = Convert.ToString(ds.Tables["Header"].Rows[0]["SALEINVOICEDATE"]).Trim();
            //this.Hdn_EntryFrom.Value = Convert.ToString(ds.Tables["Header"].Rows[0]["ENTRY_FROM"]).Trim();
        }
        #endregion

        #region Product Details Information

        #region Item-wise Tax Component
        if (ds.Tables["TaxDetails"].Rows.Count > 0)
        {
            DataTable dtTaxComponent = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];
            for (int i = 0; i < ds.Tables["TaxDetails"].Rows.Count; i++)
            {
                DataRow dr = dtTaxComponent.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["SALEINVOICEID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["SALEINVOICEID"]);
                dr["PRODUCTID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRODUCTID"]);
                dr["PRODUCTNAME"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRODUCTNAME"]);
                dr["BATCHNO"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["BATCHNO"]);
                dr["TAXID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAXID"]);
                dr["NAME"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["NAME"]);
                dr["TAXPERCENTAGE"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PERCENTAGE"]);
                dr["TAXVALUE"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAXVALUE"]);
                dr["PRIMARYPRODUCTID"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRIMARYPRODUCTID"]);
                dr["PRIMARYPRODUCTBATCHNO"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["PRIMARYPRODUCTBATCHNO"]);
                dr["TAG"] = Convert.ToString(ds.Tables["TaxDetails"].Rows[i]["TAG"]);
                dtTaxComponent.Rows.Add(dr);
                dtTaxComponent.AcceptChanges();
            }

            HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtTaxComponent;
        }
        #endregion

        #region Details Information

        #region Bill Details 
        DataTable dtSaleinvoice = (DataTable)Session["SALEINVOICEDETAILSRET"];
        if (ds.Tables["BillDetails"].Rows.Count > 0)
        {
            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"].ToString());
            }
            #endregion
            for (int i = 0; i < ds.Tables["BillDetails"].Rows.Count; i++)
            {
                DataRow dr = dtSaleinvoice.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["SALEINVOICEID"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["SALEINVOICEID"]);
                dr["PRODUCTID"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTID"]);
                dr["PRODUCTNAME"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTNAME"]);
                dr["HSNCODE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["HSNCODE"]);
                dr["BATCHNO"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["BATCHNO"]);
                dr["MRP"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["MRP"]);
                dr["RATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["RATE"]);
                dr["INVOICEQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["INVOICEQTY"]);
                dr["AMOUNT"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["AMOUNT"]);
                dr["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["ALREADYRETURNQTY"]);
                dr["REMAININGQTY"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["REMAININGQTY"]);
                dr["MFDATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["MFDATE"]);
                dr["EXPRDATE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["EXPRDATE"]);
                dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BillDetails"].Rows[i]["ASSESMENTPERCENTAGE"]);
                decimal BillTaxAmt = 0;

                #region Loop For Adding Itemwise Tax Component
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    switch (Convert.ToString(ds.Tables["TaxCount"].Rows[k]["RELATEDTO"]))
                    {
                        case "1":

                            TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                            /*ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTID"].ToString()).Trim(),
                                                                    Convert.ToString(ds.Tables["Header"].Rows[0]["SALEINVOICEDATE"]).Trim());*/

                            ProductWiseTax = clsReturn.GetHSNTaxOnEdit(InvoiceID, TAXID, Convert.ToString(ds.Tables["BillDetails"].Rows[i]["PRODUCTID"].ToString()).Trim(), "");

                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                            double TaxValue = 0;
                            TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(ds.Tables["BillDetails"].Rows[i]["AMOUNT"].ToString()) * ProductWiseTax) / 100) * 100) / 100);
                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                            BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""]);
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

        HttpContext.Current.Session["SALEINVOICEDETAILSRET"] = dtSaleinvoice;
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
            NetAmount = Convert.ToDecimal(ds.Tables["Footer"].Rows[0]["TOTALSALEINVOICEVALUE"].ToString().Trim());
        }
        #endregion

        if (grdinvoicedetails.Rows.Count > 0)
        {
            this.InvoiceGridCalculation(NetAmount);
        }
    }
    #endregion

    #region LoadRetailerInvoice
    public void LoadRetailerInvoice(string RetailerID, string FromDate, string ToDate, string DepotID)
    {
        try
        {
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataTable dtinvoice = new DataTable();
            dtinvoice = clsReturn.BindDistributorInvoice(RetailerID, FromDate, ToDate, DepotID);

            List<Invoicedetails> InvoiceDetails = new List<Invoicedetails>();
            if (dtinvoice.Rows.Count > 0)
            {
                this.ddlretailerinvoice.Items.Clear();
                this.ddlretailerinvoice.Items.Add(new ListItem("Select SaleInvoice No", "0"));

                for (int i = 0; i < dtinvoice.Rows.Count; i++)
                {
                    Invoicedetails po = new Invoicedetails();
                    po.INVOICEID = dtinvoice.Rows[i]["INVOICEID"].ToString();
                    po.INVOICENO = dtinvoice.Rows[i]["INVOICENO"].ToString();
                    po.INVOICEDATE = dtinvoice.Rows[i]["INVOICEDATE"].ToString();
                    po.NETAMOUNT = dtinvoice.Rows[i]["NETAMOUNT"].ToString();
                    InvoiceDetails.Add(po);
                }

                //---------------------------------------------------------------------------------------------

                string text1 = string.Format("{0}{1}{2}",
                "INVOICENO".PadRight(38, '\u00A0'),
                "INVOICEDATE".PadRight(15, '\u00A0'),
                "NETAMOUNT".PadRight(11, '\u00A0')
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

                foreach (Invoicedetails p in InvoiceDetails)
                {
                    string text = string.Format("{0}{1}{2}",
                        p.INVOICENO.PadRight(38, '\u00A0'),
                        p.INVOICEDATE.PadRight(15, '\u00A0'),
                        p.NETAMOUNT.PadRight(11, '\u00A0')
                        );

                    ddlretailerinvoice.Items.Add(new ListItem(text, "" + p.INVOICEID + ""));
                }
            }
            else
            {
                this.ddlretailerinvoice.Items.Clear();
                this.ddlretailerinvoice.Items.Add(new ListItem("Select SaleInvoice No", "0"));
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
            if (HttpContext.Current.Session["SALERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }
            if (HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataTable dtReturn = (DataTable)Session["SALERETURNDETAILS"];

            DataTable dtItemWiseTax = (DataTable)Session["RETURNTAXCOMPONENTDETAILS"];
            DataTable dtTaxCount = (DataTable)Session["dtInvoiceTaxCount"];
            DataTable dtSaleInvoice = new DataTable();
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            int numberOfRecords = 0;

            if (Session["SALEINVOICEDETAILSRET"] != null)
            {
                dtSaleInvoice = (DataTable)Session["SALEINVOICEDETAILSRET"];
            }
            if (dtSaleInvoice.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdinvoicedetails.Rows)
                {
                    TextBox txtreturnqty = (TextBox)row.FindControl("txtreturnqty");
                    string ReturnQty = txtreturnqty.Text.Split(',').Last();
                    decimal REMAININGQTY = Convert.ToDecimal(row.Cells[13].Text.Trim());
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
                                Amount = (Convert.ToDecimal(ReturnQty) * Convert.ToDecimal(row.Cells[9].Text.Trim()));
                                ReturnAmount = Amount;

                                #region Bill Quantity
                                /*if (Convert.ToString(row.Cells[19].Text.Trim()) == "0")
                                {*/
                                //numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND BATCHNO= '" + Convert.ToString(row.Cells[7].Text.Trim()) + "' AND FREETAG= '" + Convert.ToString(row.Cells[19].Text.Trim()) + "'").Length;
                                numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND BATCHNO= '" + Convert.ToString(row.Cells[7].Text.Trim()) + "'").Length;
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
                                    dr["SALEINVOICEID"] = Convert.ToString(row.Cells[3].Text.Trim());
                                    dr["PRODUCTID"] = Convert.ToString(row.Cells[4].Text.Trim());
                                    dr["PRODUCTNAME"] = Convert.ToString(row.Cells[5].Text.Trim());
                                    dr["HSNCODE"] = Convert.ToString(row.Cells[6].Text.Trim());
                                    dr["BATCHNO"] = Convert.ToString(row.Cells[7].Text.Trim());
                                    dr["MRP"] = Convert.ToString(row.Cells[8].Text.Trim());
                                    dr["RATE"] = Convert.ToString(row.Cells[9].Text.Trim());
                                    dr["RETURNQTY"] = ReturnQty.Trim();
                                    dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ReturnAmount)).Trim();
                                    dr["ALREADYRETURNQTY"] = Convert.ToString(row.Cells[12].Text.Trim());
                                    dr["REMAININGQTY"] = Convert.ToString(row.Cells[13].Text.Trim());
                                    dr["MFDATE"] = Convert.ToString(row.Cells[14].Text.Trim());
                                    dr["EXPRDATE"] = Convert.ToString(row.Cells[15].Text.Trim());
                                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(row.Cells[16].Text.Trim());
                                    dr["INVOICEQTY"] = Convert.ToString(row.Cells[10].Text.Trim());
                                    decimal BillTaxAmt = 0;

                                    #region Loop For Adding Itemwise Tax Component
                                    for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                                    {
                                        TAXID = clsReturn.TaxID(dtTaxCount.Rows[k]["NAME"].ToString());

                                        string NEWTAXID = clsReturn.TaxID(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                                        //ProductWiseTax = clsReturn.GetHSNTax(TAXID, Convert.ToString(row.Cells[4].Text.Trim()), this.hdnSaleInvoiceDate.Value.Trim());
                                        ProductWiseTax = clsReturn.GetHSNTaxOnEdit(Convert.ToString(row.Cells[3].Text.Trim()), TAXID, Convert.ToString(row.Cells[4].Text.Trim()), "");
                                        dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                        double TaxValue = 0;
                                        TaxValue = Convert.ToDouble(Math.Floor(((ReturnAmount * ProductWiseTax) / 100) * 100) / 100);
                                        dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                        BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + ""]);

                                        if (Arry.Count > 0)
                                        {
                                            foreach (string row1 in Arry)
                                            {
                                                if (row1.Contains(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString().Trim()))
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
                                                Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                                        }
                                        CreateTaxDatatable(GUID,
                                                            Convert.ToString(row.Cells[3].Text.Trim()),
                                                            Convert.ToString(row.Cells[4].Text.Trim()),
                                                            Convert.ToString(row.Cells[7].Text.Trim()),
                                                            NEWTAXID,
                                                            Convert.ToString(ProductWiseTax),
                                                            dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + ""].ToString().Trim(),
                                                            Convert.ToString(row.Cells[8].Text.Trim())
                                                            );
                                    }
                                    #endregion

                                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + ReturnAmount));
                                    dtReturn.Rows.Add(dr);
                                    dtReturn.AcceptChanges();
                                }
                                //}

                                #endregion                               

                                #endregion
                            }
                            if (rdbNoTax.Checked == true)
                            {
                                #region Without TAX

                                decimal Amount = 0;
                                decimal ReturnAmount = 0;

                                int Taxflag = 0;
                                Amount = (Convert.ToDecimal(ReturnQty) * Convert.ToDecimal(row.Cells[9].Text.Trim()));
                                ReturnAmount = Amount;

                                #region Bill Quantity
                                /*if (Convert.ToString(row.Cells[19].Text.Trim()) == "0")
                                {*/
                                //numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND BATCHNO= '" + Convert.ToString(row.Cells[7].Text.Trim()) + "' AND FREETAG= '" + Convert.ToString(row.Cells[19].Text.Trim()) + "'").Length;
                                numberOfRecords = dtReturn.Select("PRODUCTID = '" + Convert.ToString(row.Cells[4].Text.Trim()) + "' AND BATCHNO= '" + Convert.ToString(row.Cells[7].Text.Trim()) + "'").Length;
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
                                    dr["SALEINVOICEID"] = Convert.ToString(row.Cells[3].Text.Trim());
                                    dr["PRODUCTID"] = Convert.ToString(row.Cells[4].Text.Trim());
                                    dr["PRODUCTNAME"] = Convert.ToString(row.Cells[5].Text.Trim());
                                    dr["HSNCODE"] = Convert.ToString(row.Cells[6].Text.Trim());
                                    dr["BATCHNO"] = Convert.ToString(row.Cells[7].Text.Trim());
                                    dr["MRP"] = Convert.ToString(row.Cells[8].Text.Trim());
                                    dr["RATE"] = Convert.ToString(row.Cells[9].Text.Trim());
                                    dr["RETURNQTY"] = ReturnQty.Trim();
                                    dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", ReturnAmount)).Trim();
                                    dr["ALREADYRETURNQTY"] = Convert.ToString(row.Cells[12].Text.Trim());
                                    dr["REMAININGQTY"] = Convert.ToString(row.Cells[13].Text.Trim());
                                    dr["MFDATE"] = Convert.ToString(row.Cells[14].Text.Trim());
                                    dr["EXPRDATE"] = Convert.ToString(row.Cells[15].Text.Trim());
                                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(row.Cells[16].Text.Trim());
                                    dr["INVOICEQTY"] = Convert.ToString(row.Cells[10].Text.Trim());
                                    decimal BillTaxAmt = 0;

                                    #region Loop For Adding Itemwise Tax Component
                                    for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                                    {
                                        TAXID = "NA";
                                        ProductWiseTax = 0;
                                        dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                        dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                        BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCount.Rows[k]["INPUT_TAXNAME"]) + ""]);

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
                                                Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                                        }
                                        //CreateTaxDatatable(GUID,
                                        //                    Convert.ToString(row.Cells[3].Text.Trim()),
                                        //                    Convert.ToString(row.Cells[4].Text.Trim()),
                                        //                    Convert.ToString(row.Cells[6].Text.Trim()),
                                        //                    TAXID,
                                        //                    Convert.ToString(ProductWiseTax),
                                        //                    dr["" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + ""].ToString().Trim()
                                        //                    );

                                    }
                                    #endregion

                                    dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + ReturnAmount));
                                    dtReturn.Rows.Add(dr);
                                    dtReturn.AcceptChanges();
                                }
                                //}

                                #endregion
                                #endregion
                            }
                        }
                    }
                }
                Session["SALERETURNDETAILS"] = dtReturn;
            }
            if (dtReturn.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = dtReturn;
                this.grdAddDespatch.DataBind();
                this.BillGridCalculation();
            }
            else
            {
                
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                HttpContext.Current.Session["SALERETURNDETAILS"] = null;
                MessageBox1.ShowError("<b><font color='red'>Return Qty should be less than Invoice qty..!</font></b>");
            }

            if (dtReturn.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            if (this.ddlretailerinvoice.SelectedValue != "0")
            {
                this.RemoveSaleInvoiceSession();
                this.CreateDataTable();
                this.CreateDataTableTaxComponent();
                LoadInvoiceProductGrid(this.ddlretailerinvoice.SelectedValue.Trim());
            }
            else
            {
                this.grdinvoicedetails.DataSource = null;
                this.grdinvoicedetails.DataBind();
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
            this.trinvoicenoView.Style["display"] = "";
            this.grdAddDespatch.Columns[0].Visible = false;
        }
        if (flag == "A")
        {
            this.divsearch.Style["display"] = "";
            this.divproduct.Style["display"] = "";
            this.trbtnreturnadd.Style["display"] = "";
            this.trinvoiceno.Style["display"] = "";
            this.trinvoicenoView.Style["display"] = "none";
            this.grdAddDespatch.Columns[0].Visible = true;
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

        decimal TotalSaleAmt = 0;
        decimal TotalNetAmt = 0;
        TotalNetAmt = NetAmt;

        this.grdinvoicedetails.HeaderRow.Cells[5].Text = "PRODUCT";
        this.grdinvoicedetails.HeaderRow.Cells[6].Text = "HSN CODE";
        //this.grdinvoicedetails.HeaderRow.Cells[7].Text = "BATCH";
        this.grdinvoicedetails.HeaderRow.Cells[8].Text = "MRP";
        this.grdinvoicedetails.HeaderRow.Cells[9].Text = "RATE";
        this.grdinvoicedetails.HeaderRow.Cells[10].Text = "INV QTY(PCS)";
        this.grdinvoicedetails.HeaderRow.Cells[11].Text = "SALE AMT";
        this.grdinvoicedetails.HeaderRow.Cells[12].Text = "ALREADY RETURN QTY";

        this.grdinvoicedetails.FooterRow.Cells[9].Text = "Total : ";
        this.grdinvoicedetails.FooterRow.Cells[9].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[9].ForeColor = Color.Blue;

        this.grdinvoicedetails.HeaderRow.Cells[2].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[3].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[4].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[7].Visible = false;
        //this.grdinvoicedetails.HeaderRow.Cells[12].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[13].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[14].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[15].Visible = false;
        this.grdinvoicedetails.HeaderRow.Cells[16].Visible = false;

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

        this.grdinvoicedetails.FooterRow.Cells[2].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[3].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[4].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[7].Visible = false;
        //this.grdinvoicedetails.FooterRow.Cells[12].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[13].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[14].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[15].Visible = false;
        this.grdinvoicedetails.FooterRow.Cells[16].Visible = false;

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
        //this.grdinvoicedetails.FooterRow.Cells[12].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[13].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[14].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[15].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[16].Wrap = false;

        foreach (GridViewRow row in grdinvoicedetails.Rows)
        {
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[7].Visible = false;
            //row.Cells[12].Visible = false;
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

            TotalInvoiceQuantity += Convert.ToDecimal(row.Cells[10].Text.Trim());
            TotalSaleAmt += Convert.ToDecimal(row.Cells[11].Text.Trim());
            row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            //row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            //row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

            int count = 16;
            DataTable dt = (DataTable)Session["dtInvoiceTaxCount"];
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
            this.grdinvoicedetails.FooterRow.Cells[10].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[10].Text = TotalInvoiceQuantity.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[10].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[10].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[10].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        #endregion        

        #region Sale Amount
        if (TotalSaleAmt == 0)
        {
            this.grdinvoicedetails.FooterRow.Cells[11].Text = "0.00";
        }
        else
        {
            this.grdinvoicedetails.FooterRow.Cells[11].Text = TotalSaleAmt.ToString("#.00");
        }
        this.grdinvoicedetails.FooterRow.Cells[11].Font.Bold = true;
        this.grdinvoicedetails.FooterRow.Cells[11].ForeColor = Color.Blue;
        this.grdinvoicedetails.FooterRow.Cells[11].Wrap = false;
        this.grdinvoicedetails.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Tax
        int TotalRows = grdinvoicedetails.Rows.Count;
        int count1 = 0;

        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
        for (int i = 18; i <= (18 + dtTaxCountDataAddition1.Rows.Count); i += 2)
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

        for (int i = 17; i <= (17 + dtTaxCountDataAddition1.Rows.Count); i += 2)
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
                grdinvoicedetails.FooterRow.Cells[19 + count1].Text = "0.00";
            }
            else
            {
                grdinvoicedetails.FooterRow.Cells[19 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdinvoicedetails.FooterRow.Cells[19 + count1].Font.Bold = true;
            grdinvoicedetails.FooterRow.Cells[19 + count1].ForeColor = Color.Blue;
            grdinvoicedetails.FooterRow.Cells[19 + count1].Wrap = false;
            grdinvoicedetails.FooterRow.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdinvoicedetails.Rows)
            {
                row.Cells[19 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdinvoicedetails.HeaderRow.Cells[19 + count1].Text = "NET AMOUNT";
            this.grdinvoicedetails.HeaderRow.Cells[19 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                grdinvoicedetails.FooterRow.Cells[18 + count1].Text = "0.00";
            }
            else
            {
                grdinvoicedetails.FooterRow.Cells[18 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdinvoicedetails.FooterRow.Cells[18 + count1].Font.Bold = true;
            grdinvoicedetails.FooterRow.Cells[18 + count1].ForeColor = Color.Blue;
            grdinvoicedetails.FooterRow.Cells[18 + count1].Wrap = false;
            grdinvoicedetails.FooterRow.Cells[18 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdinvoicedetails.Rows)
            {
                row.Cells[18 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdinvoicedetails.HeaderRow.Cells[18 + count1].Text = "NET AMOUNT";
            this.grdinvoicedetails.HeaderRow.Cells[18 + count1].Wrap = false;
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
        int Taxflag = 0;
        DataTable dtSALERETURNDETAILS = new DataTable();
        DataTable dtRETURNFREEQTYDETAILS = new DataTable();
        DataTable dtTaxCount = (DataTable)Session["dtInvoiceTaxCount"];
        if (Session["SALERETURNDETAILS"] != null)
        {
            dtSALERETURNDETAILS = (DataTable)Session["SALERETURNDETAILS"];
        }
        
            TotalNetAmt = CalculateTotalNetAmount(dtSALERETURNDETAILS);

        for (int k = 0; k < dtTaxCount.Rows.Count; k++)
        {
            Taxflag = 0;
            if (Arry.Count > 0)
            {
                foreach (string row in Arry)
                {
                    if (row.Contains(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString().Trim()))
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
                    Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                    TotalBillTax = CalculateTaxTotal(dtSALERETURNDETAILS);
                }
                else
                {
                    TotalBillTax = CalculateTaxTotal(dtSALERETURNDETAILS);
                }
            }
            else
            {
                Arry.Add(dtTaxCount.Rows[k]["INPUT_TAXNAME"].ToString());
                TotalBillTax = CalculateTaxTotal(dtSALERETURNDETAILS);
            }
        }

       

        this.grdAddDespatch.HeaderRow.Cells[4].Text = "PRODUCT";
        this.grdAddDespatch.HeaderRow.Cells[5].Text = "HSN CODE";
        //this.grdAddDespatch.HeaderRow.Cells[6].Text = "BATCH";
        this.grdAddDespatch.HeaderRow.Cells[7].Text = "MRP";
        this.grdAddDespatch.HeaderRow.Cells[8].Text = "RATE";
        this.grdAddDespatch.HeaderRow.Cells[9].Text = "RETURN QTY(PCS)";
        this.grdAddDespatch.HeaderRow.Cells[10].Text = "RET AMT";

        this.grdAddDespatch.FooterRow.Cells[8].Text = "Total : ";
        this.grdAddDespatch.FooterRow.Cells[8].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[8].ForeColor = Color.Blue;

        this.grdAddDespatch.HeaderRow.Cells[1].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[2].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[3].Visible = false;
        this.grdAddDespatch.HeaderRow.Cells[6].Visible = false;
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
        this.grdAddDespatch.FooterRow.Cells[6].Visible = false;
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
            row.Cells[6].Visible = false;
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

            TotalReturnQuantity += Convert.ToDecimal(row.Cells[9].Text.Trim());
            TotalReturnAmt += Convert.ToDecimal(row.Cells[10].Text.Trim());

            row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

            int count = 16;
            DataTable dt = (DataTable)Session["dtInvoiceTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.grdAddDespatch.HeaderRow.Cells[count].Wrap = true;
            }
        }

        #region Invoice Qty
        if (TotalReturnQuantity == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[9].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[9].Text = TotalReturnQuantity.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[9].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[9].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[9].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Sale Amount
        if (TotalReturnAmt == 0)
        {
            this.grdAddDespatch.FooterRow.Cells[10].Text = "0.00";
        }
        else
        {
            this.grdAddDespatch.FooterRow.Cells[10].Text = TotalReturnAmt.ToString("#.00");
        }
        this.grdAddDespatch.FooterRow.Cells[10].Font.Bold = true;
        this.grdAddDespatch.FooterRow.Cells[10].ForeColor = Color.Blue;
        this.grdAddDespatch.FooterRow.Cells[10].Wrap = false;
        this.grdAddDespatch.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        #endregion

        #region Tax
        int TotalRows = grdAddDespatch.Rows.Count;
        int count1 = 0;

        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtInvoiceTaxCount"];
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

        if (dtTaxCountDataAddition1.Rows.Count == 3)
        {
            if (TotalNetAmt == 0)
            {
                grdAddDespatch.FooterRow.Cells[21 + count1].Text = "0.00";
            }
            else
            {
                grdAddDespatch.FooterRow.Cells[21 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            grdAddDespatch.FooterRow.Cells[21 + count1].Font.Bold = true;
            grdAddDespatch.FooterRow.Cells[21 + count1].ForeColor = Color.Blue;
            grdAddDespatch.FooterRow.Cells[21 + count1].Wrap = false;
            grdAddDespatch.FooterRow.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in grdAddDespatch.Rows)
            {
                row.Cells[21 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.grdAddDespatch.HeaderRow.Cells[21 + count1].Text = "NET AMOUNT";
            this.grdAddDespatch.HeaderRow.Cells[21 + count1].Wrap = false;
        }

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
        if (dtTaxCountDataAddition1.Rows.Count == 1)
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
        #endregion


       

        this.txtAmount.Text = Convert.ToString(String.Format("{0:0.00}", TotalReturnAmt));
        this.txtTotTax.Text = Convert.ToString(String.Format("{0:0.00}", TotalBillTax));
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
        ClsSaleReturn_FAC objReturn = new ClsSaleReturn_FAC();
        DataTable dtTcs = new DataTable();
        dtTcs = objReturn.BindTcsDetails(this.ddlretailerinvoice.SelectedValue.ToString());
        string tcspercent = string.Empty;
        string tcs = string.Empty;
        if (dtTcs.Rows.Count > 0)
        {
            tcs = dtTcs.Rows[0]["tcs"].ToString();
            tcspercent= dtTcs.Rows[0]["TCSPERCENT"].ToString();
        }
        if (tcs == "Y")
        {

            this.txttcsamt.Text = this.txtFinalAmt.Text;
            this.txttcspercent.Text = tcspercent;
            //this.txttcs.Text = (Convert.ToDecimal(txttcsamt.Text) * Convert.ToDecimal(txttcspercent.Text)).ToString();
            this.txttcs.Text = Math.Ceiling(Convert.ToDecimal(txttcsamt.Text) * Convert.ToDecimal(txttcspercent.Text)/100).ToString();
            //this.txttcsnet.Text = (this.txttcsamt.Text + this.txttcs.Text);
            this.txttcsnet.Text = (Convert.ToDecimal(txttcsamt.Text) + Convert.ToDecimal(txttcs.Text)).ToString();

            
        }
        else
        {
            this.txttcsamt.Text = "0.000";
            this.txttcspercent.Text = "0.000";
            this.txttcs.Text = "0.000";
            this.txttcsnet.Text = "0.000";
        }
    }
    #endregion

    #region TotalReturnQuantityCalculation
    protected void TotalReturnCalculation()
    {
        decimal TotalBillReturnQuantity = 0;
        decimal TotalFreeReturnQuantity = 0;

        foreach (GridViewRow row in grdAddDespatch.Rows)
        {
            TotalBillReturnQuantity += Convert.ToDecimal(row.Cells[9].Text.Trim());
        }

        if (TotalBillReturnQuantity == 0 & TotalFreeReturnQuantity == 0)
        {
            this.txtTotPCS.Text = "0.00";
        }
        else
        {
            this.txtTotPCS.Text = (TotalBillReturnQuantity + TotalFreeReturnQuantity).ToString("#.00");
        }
    }
    #endregion

    #region rdbTax_CheckedChanged
    protected void rdbTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("SALERETURNDETAILS");
        Session.Remove("RETURNFREEQTYDETAILS");
        Session.Remove("RETURNTAXCOMPONENTDETAILS");
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
    }
    #endregion

    #region rdbNoTax_CheckedChanged
    protected void rdbNoTax_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("SALERETURNDETAILS");
        Session.Remove("RETURNFREEQTYDETAILS");
        Session.Remove("RETURNTAXCOMPONENTDETAILS");
        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();

        this.txtAmount.Text = "0.00";
        this.txtTotTax.Text = "0.00";
        this.txtNetAmt.Text = "0.00";
        this.txtTotalGross.Text = "0.00";
        this.txtRoundoff.Text = "0.00";
        this.txtFinalAmt.Text = "0.00";
        this.txtTotPCS.Text = "0";
    }
    #endregion

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = "frmPrintPopUp_FAC.aspx?pid=" + hdnDespatchID.Value + "&&BSID=" + " " + "";
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
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdnDespatchID.Value).Trim();
            flag = clsPurchaseStockReceipt.ApproveSaleReturn(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtSaleInvoiceNo.Text.Trim(), this.txtInvoiceDate.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                LoadSaleReturnInvoice();
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
                LoadSaleReturnInvoice();
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
                GridDataControlFieldCell cell8 = e.Row.Cells[8] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper().Trim();
                string DAYEND = cell8.Text.Trim().ToUpper().Trim();

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
                    cell8.ForeColor = Color.Blue;
                }
                else
                {
                    cell8.ForeColor = Color.Green;
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

    public void loadadd()
    {
        this.Displayonoff("A");
        this.hdnDespatchID.Value = "";
        this.ClearControls();
        this.ResetSession();
        this.trAutoInvoiceNo.Style["display"] = "none";
        pnlDisplay.Style["display"] = "none";
        pnlAdd.Style["display"] = "";
        btnaddhide.Style["display"] = "none";
        this.txttcsamt.Text = "0.00";
        this.txttcspercent.Text = "0.00";
        this.txttcs.Text = "0.00";
        this.txttcsnet.Text = "0.00";
        this.LoadMotherDepot();
        this.LoadBusinessSegment();
        this.LoadStore();
        this.LoadPrincipleGroup1();
        //this.LoadPrincipleGroup(ViewState["BSID"].ToString());

        #region Enable Controls
        //this.ImageButton1.Enabled = true;
        this.ddlDepot.Enabled = false;
        this.ddlDistributor.Enabled = true;
        this.ddlgroup.Enabled = true;
        this.ddlbssegment.Enabled = true;
        #endregion

        this.grdAddDespatch.DataSource = null;
        this.grdAddDespatch.DataBind();
        //this.txtInvoiceDate.Text = dtcurr.ToString(date).Replace('-', '/');
        divsave.Style["display"] = "";
        this.DateLock();
    }

    public void Btn_View(string _InvoiceID)
    {
        try
        {
            hdnDespatchID.Value = _InvoiceID;
            ResetSession();
            clsDespatchStock clsDespatchStck = new clsDespatchStock();
            ClsSaleReturn_FAC clsReturn = new ClsSaleReturn_FAC();
            DataSet ds = new DataSet();
            this.trAutoInvoiceNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.divsave.Style["display"] = "none";
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;

            #region Disable Controls
            this.ImageButton1.Enabled = false;
            this.ddlDepot.Enabled = false;
            this.ddlDistributor.Enabled = false;
            this.ddlgroup.Enabled = false;
            #endregion

            this.Displayonoff("V");

            if (HttpContext.Current.Session["SALERETURNDETAILS"] == null)
            {
                CreateDataTableSaleReturnBillProduct();
            }
            if (HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] == null)
            {
                CreateDataTableReturnTaxComponent();
            }
            this.LoadPrincipleGroup(ViewState["BSID"].ToString());
            string SaleReturnID = Convert.ToString(hdnDespatchID.Value).Trim();
            ds = clsReturn.EditInvoiceDetails(SaleReturnID);
            DataTable dtRetunInvoiceEditProductDetails = (DataTable)Session["SALERETURNDETAILS"];

            #region Header Table Information
            if (ds.Tables["HEADER"].Rows.Count > 0)
            {
                this.LoadMotherDepot();
                this.txtSaleInvoiceNo.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNNO"]).Trim();
                this.ddlDepot.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DEPOTID"]).Trim();
                this.txtInvoiceDate.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["SALERETURNDATE"]).Trim();
                this.txtRemarks.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["REMARKS"]).Trim();
                this.ddlgroup.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["GROUPID"]).Trim();
                this.BindCustomer();
                this.ddlDistributor.SelectedValue = Convert.ToString(ds.Tables["HEADER"].Rows[0]["DISTRIBUTORID"]).Trim();
                this.txtTotPCS.Text = Convert.ToString(ds.Tables["HEADER"].Rows[0]["TOTALPCS"]).Trim();
            }
            #endregion

            #region Item-wise Tax 
            if (ds.Tables["TAXDETAILS"].Rows.Count > 0)
            {
                DataTable dtTaxComponentEdit = (DataTable)Session["RETURNTAXCOMPONENTDETAILS"];
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
                    dr["MRP"] = Convert.ToString(ds.Tables["TAXDETAILS"].Rows[i]["MRP"]);
                    dtTaxComponentEdit.Rows.Add(dr);
                    dtTaxComponentEdit.AcceptChanges();
                }
                HttpContext.Current.Session["RETURNTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
            }
            #endregion

            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"].ToString());
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
                        drEditReturnInvoiceDetails["SALEINVOICEID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["SALEINVOICEID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTID"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim();
                        drEditReturnInvoiceDetails["PRODUCTNAME"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTNAME"]).Trim();
                        drEditReturnInvoiceDetails["HSNCODE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["HSNCODE"]).Trim();
                        drEditReturnInvoiceDetails["BATCHNO"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim();
                        drEditReturnInvoiceDetails["MRP"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MRP"]).Trim();
                        drEditReturnInvoiceDetails["RATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RATE"]).Trim();
                        drEditReturnInvoiceDetails["RETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["RETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["AMOUNT"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"]).Trim();
                        drEditReturnInvoiceDetails["ALREADYRETURNQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ALREADYRETURNQTY"]).Trim();
                        drEditReturnInvoiceDetails["REMAININGQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["REMAININGQTY"]).Trim();
                        drEditReturnInvoiceDetails["MFDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["MFDATE"]).Trim();
                        drEditReturnInvoiceDetails["EXPRDATE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["EXPRDATE"]).Trim();
                        drEditReturnInvoiceDetails["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                        drEditReturnInvoiceDetails["INVOICEQTY"] = Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["INVOICEQTY"]).Trim();
                        decimal BillTaxAmt = 0;
                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables["TAXCOUNT"].Rows[k]["RELATEDTO"]))
                            {
                                case "1":
                                    TAXID = clsReturn.TaxID(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"].ToString());
                                    //ProductWiseTax = clsReturn.GetHSNTaxOnEdit(SaleReturnID, TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim());
                                    ProductWiseTax = clsReturn.GetHSNTaxOnEdit_Return(SaleReturnID, TAXID, Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["PRODUCTID"]).Trim(), Convert.ToString(ds.Tables["BILLDETAILS"].Rows[i]["BATCHNO"]).Trim());
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    double TaxValue = 0;
                                    TaxValue = Convert.ToDouble(Math.Floor(((Convert.ToDecimal(ds.Tables["BILLDETAILS"].Rows[i]["AMOUNT"].ToString().Trim()) * ProductWiseTax) / 100) * 100) / 100);
                                    drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", TaxValue));
                                    BillTaxAmt += Convert.ToDecimal(drEditReturnInvoiceDetails["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["INPUT_TAXNAME"]) + ""]);
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
                this.txtRoundoff.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["ROUNDOFFVALUE"]).Trim();
                this.txtFinalAmt.Text = Convert.ToString(ds.Tables["FOOTERDETAILS"].Rows[0]["NETAMOUNT"]).Trim();

                if (txtAmount.Text.Trim() != "")
                {
                    // added by Subhodip De on 30.01.2018 for txtTotTax.Text was empty
                    if (string.IsNullOrEmpty(this.txtTotTax.Text.Trim()))
                    {
                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim())));
                    }
                    else
                    {
                        this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(txtAmount.Text.Trim()) + Convert.ToDecimal(txtTotTax.Text.Trim())));
                    }
                }
            }
            #endregion

            #region Invoice No
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.txtSaleNo.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEINVOICENO"]).Trim();
                this.txtSaleDate.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEINVOICEDATE"]).Trim();
                this.txtSaleAmt.Text = Convert.ToString(ds.Tables[5].Rows[0]["SALEAMT"]).Trim();
            }
            else
            {
                this.txtSaleNo.Text = "";
                this.txtSaleDate.Text = "";
                this.txtSaleAmt.Text = "";
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
                HttpContext.Current.Session["SALERETURNDETAILS"] = null;
            }
            if (dtRetunInvoiceEditProductDetails.Rows.Count > 0)
            {
                this.TotalReturnCalculation();
                this.GrossAmountCalculation();
            }
            #endregion

            HttpContext.Current.Session["SALERETURNDETAILS"] = dtRetunInvoiceEditProductDetails;
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
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarFromDate.EndDate = today1;
            CalendarExtenderToDate.EndDate = today1;
            CalendarExtender3.EndDate = today1;
        }
        else
        {
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtInvoiceDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarFromDate.EndDate = cDate;
            CalendarExtenderToDate.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
    }
    #endregion

    protected void ddlbssegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadPrincipleGroup1();
    }
}