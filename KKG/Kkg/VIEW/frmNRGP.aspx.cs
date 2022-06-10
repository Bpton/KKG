using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmNRGP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
                this.divsaleorderno.Style["display"] = "none";
                this.divsaleorderno1.Style["display"] = "none";
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                ViewState["BSID"] = Convert.ToString("33F6AC5E-1F37-4B0F-B959-D1C900BB43A5").Trim();        /* BS - OTHERTS */
                this.LoadBUSINESSSEGMENT(Convert.ToString(ViewState["BSID"]).Trim());
                //this.LoadDeleveryTerms();
                this.LoadSaleOrder();
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                this.txtsalenrgodate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtgatepassdate.Text = dtcurr.ToString(date).Replace('-', '/');
                //this.txtgatepassdate.Text = dtcurr.ToString(date).Replace('-', '/');
                //this.LoadPrincipleGroup(Convert.ToString(ViewState["BSID"]).Trim());
                ViewState["SumTotalAmount"] = "0.00";
                ViewState["cgstamnt"] = "0.00";
                ViewState["sgstamnt"] = "0.00";
                ViewState["igstamnt"] = "0.00";
                ViewState["totalamnt"] = "0.00";
                //this.ddlgroup.Enabled = true;
                //this.ddlcustomer.Enabled = true;
                this.LoadSale();
                this.tdQty.Visible = false;
                // this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
                //this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
                this.loadvendor();
                this.LoadSale();
                this.LoadSuppliedItem();
                this.DateLock();
            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadBUSINESSSEGMENT(string bsid)
    {
        ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
        string bsname = string.Empty;
        bsname = ClsSaleorderFG.BindBUSINESSSEGMENT(bsid);
        hdn_bsid.Value = bsid;
        hdn_bsname.Value = bsname;
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
            CalendarExtender1.StartDate = oDate;
            CalendarExtender2.StartDate = oDate;
            CalendarExtender6.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtsalenrgodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = today1;
                CalendarExtender2.EndDate = today1;
                CalendarExtender6.EndDate = today1;
            }
            else
            {
                this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtsalenrgodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
                CalendarExtender6.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    #region LoadSale
    public void LoadSale()
    {
        try
        {
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            this.gvsaleorderdetailsdetails.DataSource = ClsSaleorderFG.LoadNRGP(txtfromdate.Text.Trim(), txttodate.Text.Trim(), Convert.ToString(Session["DEPOTID"]).Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(),"N");
            this.gvsaleorderdetailsdetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    public void LoadSaleOrder()
    {
        try
        {
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            ViewState["SumTotalAmount"] = 0;
            ViewState["cgstamnt"] = 0;
            ViewState["sgstamnt"] = 0;
            ViewState["igstamnt"] =0;
            ViewState["totalamnt"] = 0;
            gvSaleOrder.DataSource = ClsSaleorderFG.BindNRGPGrid();
            gvSaleOrder.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadSaleOrderGrid()
    {
        try
        {
            DataTable dtOrderGrid = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];
            if (dtOrderGrid.Rows.Count > 0)
            {
                ViewState["SumTotalAmount"] = 0;
                ViewState["cgstamnt"] = 0;
                ViewState["sgstamnt"] = 0;
                ViewState["igstamnt"] = 0;
                ViewState["totalamnt"] = 0;
                gvSaleOrder.DataSource = dtOrderGrid;
                gvSaleOrder.DataBind();
            }
            else
            {
                gvSaleOrder.DataSource = null;
                gvSaleOrder.DataBind();
                ViewState["SumTotalAmount"] = "0.00";
                ViewState["cgstamnt"] = "0.00";
                ViewState["sgstamnt"] = "0.00";
                ViewState["igstamnt"] = "0.00";
                ViewState["totalamnt"] = "0.00";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.ResetHiddenField();
            this.imgPopuppodate.Focus();
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            this.ddlTransportMode.Enabled = true;
            imgPopuppodate.Visible = true;
            // this.ddldeliveryterms.Enabled = true;
            ClsSaleorderFG.ResetDataTables();   // Reset all Datatables
            this.divnew.Visible = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divsaleorderno.Style["display"] = "none";
            this.divsaleorderno1.Style["display"] = "none";
            this.divBtnSave.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "";
            //this.gvSaleOrder.Columns[10].Visible = true;
            ClsSaleorderFG.BindNRGPGrid();
            this.LoadTermsConditions();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtsalenrgodate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtgatepassdate.Text = dtcurr.ToString(date).Replace('-', '/');
            //this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtsaleorderno.Text = "";
            this.txtremarks.Text = "";
            this.txtrate.Text = "";
            this.txtdiscount.Text = "0";
            this.lblgatepassno.Text = "";
            this.txtremarks.Text = "";
            this.txtgstin.Text = "";
            this.txtplascedelivery.Text = "";
            this.txtlrgrno.Text = "";
            this.txttransport.Text = "";
            this.txtVehicleno.Text = "";
            this.gvSaleOrder.DataSource = null;
            this.gvSaleOrder.DataBind();
            this.DateLock();

            if (Session["TPU"].ToString() == "DI" || Session["TPU"].ToString() == "SUDI" || Session["TPU"].ToString() == "ST" || Session["TPU"].ToString() == "SST")
            {
                string groupid = ClsSaleorderFG.FetchGroup(Session["UserID"].ToString());

                if (!string.IsNullOrEmpty(groupid))
                {
                    //this.ddlgroup.SelectedValue = groupid;
                    //this.ddlgroup.Enabled = false;
                    // this.ddlcustomer.Items.Clear();
                    // this.ddlcustomer.Items.Add(new ListItem(Session["UTNAME"].ToString(), Session["USERID"].ToString()));
                    // this.ddlcustomer.SelectedValue = Session["USERID"].ToString();
                    // this.ddlcustomer.Enabled = false;
                }
            }
            else
            {
                //this.ddlgroup.Enabled = true;
                // this.ddlcustomer.Enabled = true;
            }

            //ddlgroup_SelectedIndexChanged(sender, e);

            this.tdQty.Visible = false;
            // this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            // this.gvsaleorderdetailsdetails.Columns[7].Visible = false;

            // ==== CSD BS Checking

            this.txtTotalCase.Text = "0";
            this.txtTotalPCS.Text = "0";
            ViewState["SumTotalAmount"] = 0;
            ViewState["cgstamnt"] = 0;
            ViewState["sgstamnt"] = 0;
            ViewState["igstamnt"] = 0;
            ViewState["totalamnt"] = 0;
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtsalenrgodate.ClientID + "').focus(); ", true); 
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region CalculateTotalPCS
    decimal CalculateTotalPCS(DataTable dt)
    {
        decimal TotalCase = 0;
        try
        {
            ClsSaleorderMM ClsSaleorderMM = new ClsSaleorderMM();
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                TotalCase += ClsSaleorderMM.GetPackingSize_OnCallPCS(Convert.ToString(dt.Rows[Counter]["PRODUCTID"]), Convert.ToString(dt.Rows[Counter]["PRODUCTPACKINGSIZEID"]), Convert.ToDecimal(dt.Rows[Counter]["ORDERQTY"]));

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return TotalCase;
    }
    #endregion

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
            this.hdn_saleorderno.Value = "";
            this.hdn_saleorderdelete.Value = "";
            //this.ddlgroup.SelectedValue = "0";
            this.ddlProductName.SelectedValue = "0";

            this.imgPopuppodate.Visible = true;
            //this.ddldeliveryterms.Enabled = true;
            this.txtremarks.Text = "";
            this.txtqty.Text = "";
            // this.txtrequireddate.Text = "";
            this.txtgatepassdate.Text = "";
            this.txtname.Text = "";
            this.gvSaleOrder.DataSource = null;
            this.gvSaleOrder.DataBind();
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            ClsSaleorderFG.ResetDataTables();   // Reset all Datatables
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "";
            this.divBtnSave.Style["display"] = "";
            //this.gvSaleOrder.Columns[10].Visible = true;
            this.LoadSale();
            this.divnew.Visible = true;
            this.ResetHiddenField();
            this.lblgatepassno.Text = "";
            this.txtremarks.Text = "";
            this.txtgstin.Text = "";
            this.txtplascedelivery.Text = "";
            this.txtlrgrno.Text = "";
            this.txttransport.Text = "";
            this.txtVehicleno.Text = "";

            // ==== CSD BS Checking


            //==== Export Checking ======== //
            if (Convert.ToString(ViewState["BSID"]).Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
            {
                this.tdQty.Visible = true;
                // this.gvsaleorderdetailsdetails.Columns[6].Visible = true;
                // this.gvsaleorderdetailsdetails.Columns[7].Visible = true;
            }
            else
            {
                this.tdQty.Visible = false;
                //this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
                // this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            }
            //=============================//

            this.txtTotalCase.Text = "0";
            this.txtTotalPCS.Text = "0";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

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

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsSaleorderFG ClsSaleorderMM = new ClsSaleorderFG();
            if (ClsSaleorderMM.NRGPRecordsCheck(ddlProductName.SelectedValue.ToString()) == 0)
            {
                decimal covverqy = Convert.ToDecimal(txtqty.Text.Trim());
                decimal rate = Convert.ToDecimal(txtrate.Text.Trim());
                if (rate == 0)
                {
                    MessageBox1.ShowWarning("Rate cannot be zero");
                    return;
                }
                decimal stockqty = Convert.ToDecimal(txtStockqty.Text.Trim());
                decimal addqty = Convert.ToDecimal(txtqty.Text.Trim());
                if (stockqty >= addqty)
                {
                    if (string.IsNullOrEmpty(txtdiscount.Text.Trim()))
                    {
                        txtdiscount.Text = "0";
                    }
                    decimal toalammount = covverqy * rate;                    
                    DataTable dtrecords = ClsSaleorderMM.BindNRGPGridRecords(this.ddlProductName.SelectedValue.ToString(), this.ddlProductName.SelectedItem.ToString(),
                                                                             Convert.ToDecimal(txtqty.Text), ddlpackingsize.SelectedValue.ToString(),
                                                                             ddlpackingsize.SelectedItem.Text.Trim(), Convert.ToDecimal(txtrate.Text.Trim()),
                                                                             toalammount, ddlstorelocation.SelectedValue.ToString(), ddlstorelocation.SelectedItem.ToString(), "",this.hdn_igstId.Value.ToString()
                                                                             ,Convert.ToDecimal(this.hdn_IgstPer.Value.ToString()),this.hdn_cgstId.Value.ToString(), Convert.ToDecimal(this.hdn_CgstPer.Value.ToString()),
                                                                             this.hdn_sgstId.Value.ToString(), Convert.ToDecimal(this.hdn_SgstPer.Value.ToString()));
                    if (dtrecords.Rows.Count > 0)
                    {
                        ViewState["SumTotalAmount"] = 0;
                        ViewState["cgstamnt"] = 0;
                        ViewState["sgstamnt"] = 0;
                        ViewState["igstamnt"] = 0;
                        ViewState["totalamnt"] = 0;
                        gvSaleOrder.DataSource = dtrecords;
                        gvSaleOrder.DataBind();
                        this.ddlTransportMode.Enabled = false;
                    }
                    else
                    {
                        gvSaleOrder.DataSource = null;
                        gvSaleOrder.DataBind();
                        ViewState["SumTotalAmount"] = "0.00";
                        ViewState["cgstamnt"] = "0.00";
                        ViewState["sgstamnt"] = "0.00";
                        ViewState["igstamnt"] = "0.00";
                        ViewState["totalamnt"] = "0.00";
                        this.ddlTransportMode.Enabled = true;
                    }
                    this.txtTotalCase.Text = "0";
                    this.txtTotalPCS.Text = "0";
                    this.txtrate.Text = "";
                    this.ddlProductName.SelectedValue = "0";
                    this.ddlpackingsize.SelectedValue = "0";
                    this.ddlstorelocation.SelectedValue = "0";
                    this.txtqty.Text = "";
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Delivered Qty should not be greater than Stock Qty</font></b>!");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color=red>Product name already exists !</font></b>", 80, 400);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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

    #region ddlProductName_SelectedIndexChanged
    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductName.SelectedValue == "0")
            {
                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                ddlpackingsize.DataBind();
            }
            else
            {
                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
                DataTable dtPackSize = new DataTable();
                dtPackSize = ClsSaleorderFG.BindPackingSize(ddlProductName.SelectedValue); ;
                ddlpackingsize.DataSource = dtPackSize;
                if (dtPackSize.Rows.Count > 0)
                {
                    ddlpackingsize.DataValueField = "PACKSIZEID_FROM";
                    ddlpackingsize.DataTextField = "PACKSIZEName_FROM";
                    ddlpackingsize.DataBind();
                    ddlpackingsize.SelectedValue = Convert.ToString(dtPackSize.Rows[0]["PACKSIZEID_FROM"]).Trim();
                }

                string YMD = Conver_To_YMD(this.txtsalenrgodate.Text.Trim());
                //txtrate.Text = "0.00";
                this.LoadStoreLocation();
                this.txtStockqty.Text = Convert.ToString(getStockQty(this.ddlstorelocation.SelectedValue.ToString()));
                LoadTaxProductWise(HttpContext.Current.Session["DEPOTID"].ToString(), this.ddlTransportMode.SelectedValue.ToString(),this.ddlProductName.SelectedValue.ToString());
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlpackingsize.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlstorelocation_SelectedIndexChanged
    protected void ddlstorelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsSaleorderFG obj = new ClsSaleorderFG();
            if (ddlProductName.SelectedValue != "0")
            {
              this.txtStockqty.Text=Convert.ToString(getStockQty(this.ddlstorelocation.SelectedValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void LoadTaxProductWise(string deptid,string vendorid,string productid)
    {
        string date = this.txtfromdate.Text;
        DataTable dtTax = new DataTable();
        ClsSaleorderFG clsTax = new ClsSaleorderFG();
        dtTax = clsTax.bindTax(date,deptid, vendorid, productid);
        if(dtTax.Rows.Count > 0)
        {
            this.hdn_igstId.Value = dtTax.Rows[0]["IGSTID"].ToString();
            this.hdn_cgstId.Value = dtTax.Rows[0]["CGSTID"].ToString();
            this.hdn_sgstId.Value = dtTax.Rows[0]["SGSTID"].ToString();

            if(this.hdn_igstId.Value != "")
            {
                this.hdn_IgstPer.Value= dtTax.Rows[0]["IGSTPER"].ToString();
                this.hdn_CgstPer.Value = Convert.ToString("0");
                this.hdn_SgstPer.Value = Convert.ToString("0");
            }
            else if(this.hdn_igstId.Value == "")
            {
                this.hdn_IgstPer.Value = Convert.ToString("0");
                this.hdn_CgstPer.Value = dtTax.Rows[0]["CGSTPER"].ToString();
                this.hdn_SgstPer.Value = dtTax.Rows[0]["SGSTPER"].ToString();
            }
        }
        else
        {
            MessageBox1.ShowError("Tax not mapped for this : " + this.ddlProductName.SelectedItem.ToString());
            return;
        }

    }


    public decimal getStockQty(string id)
    {

        decimal stockQty = 0;
        PPBLL.ClsStockJournal ClsStockJournal = new PPBLL.ClsStockJournal();
        stockQty = ClsStockJournal.GetQty(HttpContext.Current.Session["DEPOTID"].ToString(), this.ddlProductName.SelectedValue, id);
        return stockQty;
    }

    protected void DeleteRecordSaleNRGP(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            int flag = 0;
            flag = ClsSaleorderFG.DeleteNRGP(e.Record["SALENRGPID"].ToString());

            if (flag == 1)
            {
                LoadSale();
                e.Record["Error"] = "Record deleted successfully!";
            }
            else if (flag == -1)
            {
                e.Record["Error"] = "Sale Order is Allready Exists in Sale Invoice.";
            }
            else
            {
                e.Record["Error"] = "Error On Deleting!";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region View Sale Order
    protected void btngrdview_Click(object sender, EventArgs e)
    {
        try
        {
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();

            this.tdQty.Visible = false;
            this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            this.LoadTermsConditions();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            // txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            // txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];

            ClsSaleorderFG.BindNRGPGrid();
            // ddlgroup.Enabled = false;
            //ddldeliveryterms.Style.Add("color", "black !important");
            // ddlgroup.Style.Add("color", "black !important");
            // ddlcustomer.Style.Add("color", "black !important");
            string salenrgpno = hdn_saleorderno.Value.ToString();
            txtsaleorderno.Text = salenrgpno;
            txtsaleorderno.Enabled = false;
            divsaleorderno.Style["display"] = "";
            divsaleorderno1.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            DataTable dtheader = new DataTable();
            dtheader = ClsSaleorderFG.FetchSaleOrderHeader(hdn_saleorderid.Value);

            if (dtheader.Rows.Count > 0)
            {
                // this.ddlgroup.SelectedValue = dtheader.Rows[0]["GROUPID"].ToString();
                // this.ddlgroup_SelectedIndexChanged(sender, e);
                // this.ddlcustomer.SelectedValue = dtheader.Rows[0]["CUSTOMERID"].ToString();
                this.txtsalenrgodate.Text = dtheader.Rows[0]["SALEORDERDATE"].ToString();
                this.txtremarks.Text = dtheader.Rows[0]["REMARKS"].ToString();
                //if (dtheader.Rows[0]["ISCANCELLED"].ToString() == "Y")
                //{
                //    this.chkactive.Checked = true;
                //}
                //else
                //{
                //    this.chkactive.Checked = false;
                //}
                this.txtname.Text = dtheader.Rows[0]["REFERENCESALEORDERNO"].ToString();
                this.txtgatepassdate.Text = dtheader.Rows[0]["REFERENCESALEORDERDATE"].ToString();
                // this.LoadDeleveryTerms();
                // this.ddldeliveryterms.SelectedValue = dtheader.Rows[0]["DELIVERYTERMSID"].ToString();
                this.txtTotalPCS.Text = dtheader.Rows[0]["TOTALPCS"].ToString().Trim();
            }
            this.imgPopuppodate.Visible = false;
            // this.ddldeliveryterms.Enabled = false;
            //this.ddlgroup.Enabled = false;
            //this.ddlcustomer.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.divnew.Visible = false;
            this.divBtnSave.Style["display"] = "none";
            this.gvSaleOrder.Columns[10].Visible = false;
            ViewState["SumTotalAmount"] = 0;
            ViewState["cgstamnt"] = 0;
            ViewState["sgstamnt"] = 0;
            ViewState["igstamnt"] = 0;
            ViewState["totalamnt"] = 0;
            dtsaleorderrecord = ClsSaleorderFG.FetchSaleOrderDetails(hdn_saleorderid.Value);
            gvSaleOrder.DataSource = dtsaleorderrecord;
            gvSaleOrder.DataBind();
            HttpContext.Current.Session["NRGPRECORDS"] = dtsaleorderrecord;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Edit Sale Order
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            //string EntryDatecheck;

            this.LoadSuppliedItem();
            this.divBtnSave.Style["display"] = "";
            //this.gvSaleOrder.Columns[10].Visible = true;
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            this.divcancelorder.Style["display"] = "true";
            this.tdQty.Visible = false;
            //this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            // this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            string salenrgpno = hdn_saleorderno.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsSaleorderFG.EntryDatecheck(hdn_saleorderid.Value);
            //DateTime dtcurr1 = DateTime.Now;
            //if (dt.Rows.Count > 0)
            //{
                this.LoadTermsConditions();
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];

                ClsSaleorderFG.BindNRGPGrid();

                DataTable dtheader = new DataTable();
                dtheader = ClsSaleorderFG.Editnrgp(hdn_saleorderid.Value, "H","N");
                if (dtheader.Rows.Count > 0)
                {
                    loadvendor();
                    this.ddlTransportMode.SelectedValue = dtheader.Rows[0]["VENDORID"].ToString();
                    this.ddlTransportMode.Enabled = false;
                    this.txtsalenrgodate.Text = dtheader.Rows[0]["SALENRGPDATE"].ToString();
                    //this.txtremarks.Text = dtheader.Rows[0]["REMARKS"].ToString();
                    this.txtname.Text = dtheader.Rows[0]["NAME"].ToString();
                    this.lblgatepassno.Text = dtheader.Rows[0]["GATEPASSNO"].ToString();
                    this.txtgatepassdate.Text = dtheader.Rows[0]["GATEPASSDATE"].ToString();
                    this.txtinvoiceno.Text = dtheader.Rows[0]["SALENRGPNO"].ToString();
                    this.txtremarks.Text = dtheader.Rows[0]["REMARKS"].ToString();
                    this.txtlrgrno.Text = dtheader.Rows[0]["LRGRNO"].ToString();
                    this.txtVehicleno.Text = dtheader.Rows[0]["VEHICLENO"].ToString();
                    this.txttransport.Text = dtheader.Rows[0]["TRANSPORT"].ToString();
                    this.txtplascedelivery.Text = dtheader.Rows[0]["PLACEOFDELIVERY"].ToString();
                    this.txtgstin.Text = dtheader.Rows[0]["GSTIN"].ToString();
                }
                this.imgPopuppodate.Visible = true;
                // this.ddlgroup.Enabled = false;
                // this.ddlcustomer.Enabled = false;
                // this.ddldeliveryterms.Enabled = false;
                this.InputTable.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.trAddProduct.Style["display"] = "";
                this.btnadd.Visible = true;
                this.divnew.Visible = false;
                this.txtinvoiceno.Visible = true;
                ViewState["SumTotalAmount"] = 0;
                ViewState["cgstamnt"] = 0;
                ViewState["sgstamnt"] = 0;
                ViewState["igstamnt"] = 0;
                ViewState["totalamnt"] = 0;
                dtsaleorderrecord = ClsSaleorderFG.Editnrgp(hdn_saleorderid.Value, "D","N");
                gvSaleOrder.DataSource = dtsaleorderrecord;
                gvSaleOrder.DataBind();
                HttpContext.Current.Session["NRGPRECORDS"] = dtsaleorderrecord;
                this.txtStockqty.Text = "";
            //}
            //else
            //{
            //    MessageBox1.ShowInfo("Not authorized to access this invoice !.");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];
            int i = dtsaleorderrecord.Rows.Count - 1;
            while (i >= 0)
            {
                if (Convert.ToString(dtsaleorderrecord.Rows[i]["PRODUCTID"]) == Convert.ToString(hdn_saleorderdelete.Value))
                {
                    dtsaleorderrecord.Rows[i].Delete();
                    dtsaleorderrecord.AcceptChanges();
                    flag = 1;
                    break;
                }
                i--;
            }
            if (flag == 1)
            {
                HttpContext.Current.Session["NRGPRECORDS"] = dtsaleorderrecord;
                this.txtTotalCase.Text = "0";
                this.txtTotalPCS.Text = "0";
                LoadSaleOrderGrid();
                MessageBox1.ShowSuccess("<b><font color='green'>Record deleted successfully!</font></b>");
                if (dtsaleorderrecord.Rows.Count > 0)
                {
                    this.ddlTransportMode.Enabled = false;
                }
                else
                {
                    this.ddlTransportMode.Enabled = true;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Record deleted unsuccessful!</font></b>");
                if (dtsaleorderrecord.Rows.Count > 0)
                {
                    this.ddlTransportMode.Enabled = false;
                }
                else
                {
                    this.ddlTransportMode.Enabled = true;
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

    #region CalculateTotalAmount
    decimal CalculateTotalAmount(DataTable dt)
    {
        decimal TotalAmount = 0;
        if (dt.Rows.Count > 0)
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                TotalAmount += Convert.ToDecimal(dt.Rows[Counter]["DISCOUNTAMOUNT"]);
            }
        }
        return TotalAmount;
    }
    #endregion

    #region Save NRGP
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["NRGPRECORDS"];
            string getflag = string.Empty;
            string salenrgpno = string.Empty;
            int CountTerms = 0;
            string strTermsID = string.Empty;
            string ProformaID = string.Empty;
            decimal TotalCase = 0;
            decimal TotalPCS = 0;

            // Logic to be implemented for Proforma Inv Value
            if (dtsaleorderrecord.Rows.Count > 0)
            {
                ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
                string xml = ConvertDatatableToXML(dtsaleorderrecord);
                string status = string.Empty;


                #region grdTerms Loop
                DataTable dtTerms = (DataTable)Session["TermsSaleOrder"];
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

                salenrgpno = ClsSaleorderFG.InsertNRGPDetails(this.txtsalenrgodate.Text.Trim(), this.ddlTransportMode.SelectedValue.ToString() , this.txtname.Text.Trim(),
                                                              this.lblgatepassno.Text.Trim(), this.txtgatepassdate.Text.Trim(), this.txtremarks.Text,
                                                              HttpContext.Current.Session["UserID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                              xml, this.hdn_saleorderid.Value.ToString(), TotalCase, TotalPCS, HttpContext.Current.Session["DEPOTID"].ToString(),
                                                              this.ddlSuppliedItem.SelectedItem.Text.Trim(),this.txtlrgrno.Text.Trim(), this.txtVehicleno.Text.Trim(), 
                                                              this.txttransport.Text.Trim(), this.txtplascedelivery.Text.Trim(), this.txtgstin.Text.Trim());
                if (!string.IsNullOrEmpty(salenrgpno))
                {
                    if (Convert.ToString(hdn_saleorderno.Value) == "")
                    {
                        MessageBox1.ShowSuccess("SaleOrder No : <b><font color='green'>" + salenrgpno + "</font></b> saved successfully,", 40, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("SaleOrder No : <b><font color='green'>" + salenrgpno + "</font></b> updated successfully", 40, 550);
                    }
                    this.divcancelorder.Style["display"] = "none";
                    InputTable.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    divsaleorderno.Style["display"] = "none";
                    divsaleorderno1.Style["display"] = "none";
                    ddlProductName.SelectedValue = "0";
                    ddlpackingsize.SelectedValue = "0";
                    txtdiscount.Text = "0";

                    txtrate.Text = "";
                    txtqty.Text = "";
                    //txtrequireddate.Text = "";
                    // txttorequireddate.Text = "";
                    txtgatepassdate.Text = "";
                    txtname.Text = "";
                    hdn_saleorderno.Value = "";
                    hdn_saleorderdelete.Value = "";
                    Hdn_Fld.Value = "";
                    dtsaleorderrecord.Clear();
                    gvSaleOrder.DataSource = null;
                    gvSaleOrder.DataBind();
                    LoadSale();
                    ResetHiddenField();
                    divnew.Visible = true;
                    imgPopuppodate.Visible = true;

                    grdTerms.ClearPreviousDataSource();
                    grdTerms.DataSource = null;
                    grdTerms.DataBind();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color=red>Error on Saving record!</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please add atleast 1 product");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void clear()
    {
        // ddlgroup.SelectedValue = "0";
        // ddlgroup.Enabled = true;
        ddlTransportMode.SelectedValue = "0";
        ddlProductName.SelectedValue = "0";
        txtremarks.Text = "";
        txtqty.Text = "";
        // txtrequireddate.Text = "";
        // txttorequireddate.Text = "";
        divnew.Visible = true;
        txtdiscount.Text = "0";
        txtrate.Text = "";
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
        ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
        ClsSaleorderFG.ResetDataTables();   // Reset all Datatables
        LoadSale();
        this.txtTotalCase.Text = "0";
        this.txtremarks.Text = "0";
        this.txtTotalPCS.Text = "0";
        this.txtname.Text = "";
        this.lblgatepassno.Text = "";
        this.txtgstin.Text = "";
        this.txtplascedelivery.Text = "";
        this.txtlrgrno.Text = "";
        this.txttransport.Text = "";
        this.txtVehicleno.Text = "";

    }

    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {
            LoadSale();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void ResetHiddenField()
    {
        Hdn_Fld.Value = "";
        hdn_saleorderdelete.Value = "";
        hdn_saleorderid.Value = "";
        hdn_saleorderno.Value = "";
    }

    public void LoadTermsConditions()
    {
        try
        {
            ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
            DataTable dtTerms = new DataTable();
            //dtTerms = ClsSaleorderMM.BindTerms(Convert.ToString(ViewState["menuID"]).Trim());
            Session["TermsSaleOrder"] = dtTerms;

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
            throw ex;
        }
    }

    #region grdTerms_RowDataBound
    protected void grdTerms_RowDataBound(object sender, GridRowEventArgs e)
    {
        ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
        DataTable dt = new DataTable();
        if (hdn_saleorderid.Value != "")
        {
            dt = ClsSaleorderFG.EditTerms(hdn_saleorderid.Value);
        }
        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell chkcell = e.Row.Cells[2] as GridDataControlFieldCell;
            CheckBox chk = chkcell.FindControl("ChkIDTERMS") as CheckBox;
            HiddenField hiddenField = chkcell.FindControl("hdnTERMSName") as HiddenField;
            if (hdn_saleorderid.Value != "")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TERMSID"].ToString() == chk.ToolTip)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region gvSaleOrder_RowDataBound
    protected void gvSaleOrder_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.Header)
            {
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                ViewState["SumTotalAmount"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["SumTotalAmount"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[9].Text.Trim()) ? "0" : e.Row.Cells[9].Text.Trim())));

                ViewState["cgstamnt"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["cgstamnt"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[12].Text.Trim()) ? "0" : e.Row.Cells[12].Text.Trim())));
                ViewState["sgstamnt"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["sgstamnt"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[15].Text.Trim()) ? "0" : e.Row.Cells[15].Text.Trim())));
                ViewState["igstamnt"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["igstamnt"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[18].Text.Trim()) ? "0" : e.Row.Cells[18].Text.Trim())));
                ViewState["totalamnt"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["totalamnt"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[19].Text.Trim()) ? "0" : e.Row.Cells[19].Text.Trim())));
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                e.Row.Cells[5].Text = "Total (Rs.):";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;

                e.Row.Cells[9].Text = ViewState["SumTotalAmount"].ToString();
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[12].Text = ViewState["cgstamnt"].ToString();
                e.Row.Cells[12].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[15].Text = ViewState["sgstamnt"].ToString();
                e.Row.Cells[15].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[18].Text = ViewState["igstamnt"].ToString();
                e.Row.Cells[18].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[19].Text = ViewState["totalamnt"].ToString();
                e.Row.Cells[19].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;

                ViewState["SumTotalAmount"] = 0;
                ViewState["cgstamnt"] = 0;
                ViewState["sgstamnt"] = 0;
                ViewState["igstamnt"] = 0;
                ViewState["totalamnt"] = 0;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlSuppliedItem_SelectedIndexChanged
    public void ddlSuppliedItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlSuppliedItem.SelectedValue != "0")
            {
                this.BindItem(this.ddlSuppliedItem.SelectedValue.Trim());

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadSuppliedItem
    public void LoadSuppliedItem()
    {
        ClsPurchaseReturn_MM ClsRetailerSaleOrder = new ClsPurchaseReturn_MM();
        DataTable dtSuppliedItem = new DataTable();
        dtSuppliedItem = ClsRetailerSaleOrder.BindSuppliedItem_NRGP();
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
            //if (this.ddlSuppliedItem.SelectedValue != "0")
            //{
            //    this.BindVendor(this.ddlSuppliedItem.SelectedValue.Trim());
            //}
        }
        else
        {
            this.ddlSuppliedItem.Items.Clear();
            this.ddlSuppliedItem.Items.Add(new ListItem("Select Supplied Item", "0"));
            this.ddlSuppliedItem.AppendDataBoundItems = true;
        }
    }
    #endregion

    #region btnPrint_Click
    protected void btnNRGPPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?NRGPIvoicveid=" + hdn_saleorderid.Value.Trim() + "&&TAG=NRGP&&MenuId=144";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Bind Item
    protected void BindItem(string SuppliedITemID)
    {
        try
        {
            if (ddlSuppliedItem.SelectedValue != "0")
            {
                ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
                //  this.LoadCustomer();
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("Select Product Name", "0"));
                this.ddlProductName.AppendDataBoundItems = true;
                this.ddlProductName.DataSource = ClsSaleorderFG.BindProductbySItemWise(this.ddlSuppliedItem.SelectedValue.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                this.ddlProductName.DataValueField = "PRODUCTID";
                this.ddlProductName.DataTextField = "PRODUCTNAME";
                this.ddlProductName.DataBind();

                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;
            }
            else
            {
                // this.ddlcustomer.Items.Clear();
                // this.ddlcustomer.Items.Add(new ListItem("Select Customer Name", "0"));
                // this.ddlcustomer.AppendDataBoundItems = true;

                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("Select Product Name", "0"));
                this.ddlProductName.AppendDataBoundItems = true;

                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;


            }
            //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcustomer.ClientID + "').focus(); ", true);

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadStoreLocation()
    public void LoadStoreLocation()
    {
        ClsSaleorderFG ClsSaleorderFG = new ClsSaleorderFG();
        DataTable dtlocation = new DataTable();
        dtlocation = ClsSaleorderFG.BindStoreLocation(ddlProductName.SelectedValue.ToString());
        if (dtlocation.Rows.Count > 0)
        {
            this.ddlstorelocation.Items.Clear();
            //this.ddlstorelocation.Items.Add(new ListItem("Select Store Location", "0"));
            this.ddlstorelocation.AppendDataBoundItems = true;
            this.ddlstorelocation.DataValueField = "ID";
            this.ddlstorelocation.DataTextField = "NAME";
            this.ddlstorelocation.DataSource = dtlocation;
            this.ddlstorelocation.DataBind();
            this.ddlstorelocation.SelectedValue = Convert.ToString(dtlocation.Rows[0]["ID"]);
        }
    }
    #endregion
    #region vendor
    public void loadvendor()
    {
        DataTable dt = new DataTable();
        ClsSaleorderFG obj = new ClsSaleorderFG();
        dt = obj.bindparty();
        if(dt.Rows.Count > 0)
        {
            this.ddlTransportMode.Items.Clear();
            this.ddlTransportMode.Items.Add(new ListItem("Select Venodr Name", "0"));
            this.ddlTransportMode.AppendDataBoundItems = true;
            this.ddlTransportMode.DataValueField = "VENDORID";
            this.ddlTransportMode.DataTextField = "VENDORNAME";
            this.ddlTransportMode.DataSource = dt;
            this.ddlTransportMode.DataBind();
        }
    }
    #endregion

    protected void ddlTransportMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string mode = "gstnumber";
            ClsVendor_TPU objtpuvendor =new ClsVendor_TPU();
            DataTable objDt = new DataTable();
            objDt = objtpuvendor.BindPoFromTpu(mode,this.ddlTransportMode.SelectedValue.ToString());
            if(objDt.Rows.Count > 0)
            {
                if(objDt.Rows[0]["GSTNO"].ToString()!="")
                {
                    this.txtgstin.Text = objDt.Rows[0]["GSTNO"].ToString();
                    this.txtgstin.Enabled = false;
                }
                else
                {
                    MessageBox1.ShowInfo("Gst Number Not Set for this vendor");
                    this.txtgstin.Enabled = true;
                    this.txtgstin.Text = "";
                }
            }
            else
            {
                MessageBox1.ShowInfo("Gst Number Not Set for this vendor");
                this.txtgstin.Enabled = true;
                this.txtgstin.Text = "";
                return;
            }
            

        }
        catch(Exception ex)
        {

        }
    }
}