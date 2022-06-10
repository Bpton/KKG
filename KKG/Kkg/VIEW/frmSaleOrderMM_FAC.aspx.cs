using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmSaleOrderMM_FAC : System.Web.UI.Page
{
    ClsSaleorderMM ClsSaleorderMM = new ClsSaleorderMM();
    string mode = "order";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.divsaleorderno.Style["display"] = "none";
                this.divsaleorderno1.Style["display"] = "none";
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                //ViewState["BSID"] = Convert.ToString("33F6AC5E-1F37-4B0F-B959-D1C900BB43A5").Trim();        /* BS - OTHERTS */
                this.LoadBUSINESSSEGMENT();
                this.LoadDeleveryTerms();
                this.LoadSaleOrder();
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                this.txtsaleorderdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
                //this.LoadPrincipleGroup(Convert.ToString(ViewState["BSID"]).Trim());
                ViewState["SumTotalAmount"] = "0.00";
                this.ddlgroup.Enabled = true;
                this.ddlcustomer.Enabled = true;
                this.tdQty.Visible = false;
                this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
                this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
                this.LoadSale();
                this.DateLock();

            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
            CalendarExtender6.StartDate = oDate;
            CalendarExtender2.StartDate = oDate;
            CalendarExtender1.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtsaleorderdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender6.EndDate = today1;
                CalendarExtender2.EndDate = today1;
                CalendarExtender1.EndDate = today1;
            }
            else
            {
                this.txtsaleorderdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender6.EndDate = cDate;
                CalendarExtender2.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion
    public void LoadBUSINESSSEGMENT()
    {
        DataTable dtBS = new DataTable();
        dtBS = ClsSaleorderMM.BindBUSINESSSEGMENT();
        if (dtBS.Rows.Count > 0)
        {
            this.ddlbsegment.Items.Clear();
            this.ddlbsegment.Items.Add(new ListItem("Select BS Name", "0"));
            this.ddlbsegment.AppendDataBoundItems = true;
            this.ddlbsegment.DataSource = dtBS;
            this.ddlbsegment.DataValueField = "BSID";
            this.ddlbsegment.DataTextField = "BSNAME";
            this.ddlbsegment.DataBind();
        }
    }

    public void LoadPrincipleGroup(string bsid)
    {
        DataTable dtGroup = new DataTable();
        dtGroup = ClsSaleorderMM.BindGroup(bsid);
        if (dtGroup.Rows.Count > 0)
        {
            this.ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));

            if (dtGroup.Rows.Count == 1)
            {
                this.ddlgroup.Items.Clear();
                this.ddlgroup.AppendDataBoundItems = true;
                this.ddlgroup.DataSource = dtGroup;
                this.ddlgroup.DataValueField = "DIS_CATID";
                this.ddlgroup.DataTextField = "DIS_CATNAME";
                this.ddlgroup.DataBind();


                //this.ddlgroup.SelectedValue = dtGroup.Rows[0]["DIS_CATID"].ToString();
                this.ddlcustomer.Items.Clear();
                this.ddlcustomer.Items.Add(new ListItem("Select Customer Name", "0"));
                this.ddlcustomer.AppendDataBoundItems = true;
                this.ddlcustomer.DataSource = ClsSaleorderMM.BindCustomer(ddlbsegment.SelectedValue.ToString(), this.ddlgroup.SelectedValue.Trim(), Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim());
                this.ddlcustomer.DataValueField = "CUSTOMERID";
                this.ddlcustomer.DataTextField = "CUSTOMERNAME";
                this.ddlcustomer.DataBind();

                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("Select Product Name", "0"));
                this.ddlProductName.AppendDataBoundItems = true;
                this.ddlProductName.DataSource = ClsSaleorderMM.BindProduct(mode, this.ddlbsegment.SelectedValue.ToString());
                this.ddlProductName.DataValueField = "PRODUCTID";
                this.ddlProductName.DataTextField = "PRODUCTNAME";
                this.ddlProductName.DataBind();

                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;
            }
        }
        else
        {
            this.ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
            this.ddlgroup.AppendDataBoundItems = true;
        }
    }

    #region LoadSale
    public void LoadSale()
    {
        try
        {
            this.gvsaleorderdetailsdetails.DataSource = ClsSaleorderMM.LoadSale(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddlbsegment.SelectedValue.ToString(), Convert.ToString(Session["DEPOTID"]).Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
            this.gvsaleorderdetailsdetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadCustomer
    public void LoadCustomer()
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtDepot = new DataTable();
            string DepotID = string.Empty;

            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            dtDepot = clsReceivedStock.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());

            if (Session["TPU"].ToString().Trim() == "D" || Session["TPU"].ToString().Trim() == "EXPU")
            {
                dt = ClsSaleorderMM.BindCustomer(ddlbsegment.SelectedValue.ToString(), this.ddlgroup.SelectedValue.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim());
            }
            else
            {
                dt = ClsSaleorderMM.BindCustomer(ddlbsegment.SelectedValue.ToString(), this.ddlgroup.SelectedValue.Trim(), dtDepot.Rows[0]["BRID"].ToString().Trim());
            }
            this.ddlcustomer.Items.Clear();
            this.ddlcustomer.Items.Add(new ListItem("Select Customer Name", "0"));
            this.ddlcustomer.AppendDataBoundItems = true;
            this.ddlcustomer.DataSource = dt;
            this.ddlcustomer.DataValueField = "CUSTOMERID";
            this.ddlcustomer.DataTextField = "CUSTOMERNAME";
            this.ddlcustomer.DataBind();
            if (dt.Rows.Count == 1)
            {
                this.ddlcustomer.SelectedValue = Convert.ToString(dt.Rows[0]["CUSTOMERID"]).Trim();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion

    #region ddlgroup_SelectedIndexChanged
    public void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlgroup.SelectedValue != "0")
            {
                this.LoadCustomer();
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("Select Product Name", "0"));
                this.ddlProductName.AppendDataBoundItems = true;
                this.ddlProductName.DataSource = ClsSaleorderMM.BindProduct(mode, this.ddlbsegment.SelectedValue.ToString());
                this.ddlProductName.DataValueField = "PRODUCTID";
                this.ddlProductName.DataTextField = "PRODUCTNAME";
                this.ddlProductName.DataBind();

                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;
            }
            else
            {
                this.ddlcustomer.Items.Clear();
                this.ddlcustomer.Items.Add(new ListItem("Select Customer Name", "0"));
                this.ddlcustomer.AppendDataBoundItems = true;

                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("Select Product Name", "0"));
                this.ddlProductName.AppendDataBoundItems = true;

                this.ddlpackingsize.Items.Clear();
                this.ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                this.ddlpackingsize.AppendDataBoundItems = true;

                this.ddlCurrency.Items.Clear();
                this.ddlCurrency.Items.Add(new ListItem("Select Currency", "0"));
                this.ddlCurrency.AppendDataBoundItems = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcustomer.ClientID + "').focus(); ", true);

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void LoadSaleOrder()
    {
        try
        {
            ViewState["SumTotalAmount"] = 0;
            gvSaleOrder.DataSource = ClsSaleorderMM.BindSaleOrderGrid();
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
            DataTable dtOrderGrid = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
            if (dtOrderGrid.Rows.Count > 0)
            {
                ViewState["SumTotalAmount"] = 0;
                gvSaleOrder.DataSource = dtOrderGrid;
                gvSaleOrder.DataBind();
            }
            else
            {
                gvSaleOrder.DataSource = null;
                gvSaleOrder.DataBind();
                ViewState["SumTotalAmount"] = "0.00";
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
            chkactive.Checked = false;
            imgPopuppodate.Visible = true;
            this.ddldeliveryterms.Enabled = true;
            ClsSaleorderMM.ResetDataTables();   // Reset all Datatables
            this.divnew.Visible = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divsaleorderno.Style["display"] = "none";
            this.divsaleorderno1.Style["display"] = "none";
            this.divBtnSave.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "";
            this.gvSaleOrder.Columns[10].Visible = true;
            this.LoadBUSINESSSEGMENT();
            ClsSaleorderMM.BindSaleOrderGrid();
            this.LoadTermsConditions();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtsaleorderdate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtsaleorderno.Text = "";
            this.txtremarks.Text = "";
            this.txtrate.Text = "";
            this.txtdiscount.Text = "0";
            this.LoadDeleveryTerms();
            this.gvSaleOrder.DataSource = null;
            this.gvSaleOrder.DataBind();

            if (Session["TPU"].ToString() == "DI" || Session["TPU"].ToString() == "SUDI" || Session["TPU"].ToString() == "ST" || Session["TPU"].ToString() == "SST")
            {
                string groupid = ClsSaleorderMM.FetchGroup(Session["UserID"].ToString());

                if (!string.IsNullOrEmpty(groupid))
                {
                    this.ddlgroup.SelectedValue = groupid;
                    this.ddlgroup.Enabled = false;
                    this.ddlcustomer.Items.Clear();
                    this.ddlcustomer.Items.Add(new ListItem(Session["UTNAME"].ToString(), Session["USERID"].ToString()));
                    this.ddlcustomer.SelectedValue = Session["USERID"].ToString();
                    this.ddlcustomer.Enabled = false;
                }
            }
            else
            {
                this.ddlgroup.Enabled = true;
                this.ddlcustomer.Enabled = true;
                this.ddlbsegment.Enabled = true;
            }

            ddlgroup_SelectedIndexChanged(sender, e);
            this.ddlCurrency.Items.Clear();
            this.ddlCurrency.Items.Add(new ListItem("Select Currency", "0"));
            this.ddlCurrency.AppendDataBoundItems = true;
            this.chkMRPTag.Checked = false;
            this.trCurrency.Visible = false;
            this.tdQty.Visible = false;
            this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            this.gvsaleorderdetailsdetails.Columns[7].Visible = false;

            // ==== CSD BS Checking
            //if (Convert.ToString(ViewState["BSID"]).Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
            //{
            //    this.trICDS.Visible = true;
            //}
            //else
            //{
            //    this.trICDS.Visible = false;
            //}

            this.trICDS.Visible = false;

            this.txtICDS.Text = "";
            this.txtICDSDate.Text = "";
            this.txtTotalCase.Text = "0";
            this.txtTotalPCS.Text = "0";
            ViewState["SumTotalAmount"] = 0;
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtsaleorderdate.ClientID + "').focus(); ", true); 
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
            this.hdn_saleorderno.Value = "";
            this.hdn_saleorderdelete.Value = "";
            //this.ddlgroup.Items.Clear();
            this.ddlbsegment.SelectedValue = "0";
            this.ddlgroup.Items.Add(new ListItem("Select Group Name", "0"));
            this.ddlgroup.SelectedValue = "0";
            this.ddlProductName.SelectedValue = "0";
            this.chkactive.Checked = false;
            this.imgPopuppodate.Visible = true;
            this.ddldeliveryterms.Enabled = true;
            this.txtremarks.Text = "";
            this.txtqty.Text = "";
            this.txtrequireddate.Text = "";
            this.txtrefsaleorderdate.Text = "";
            this.txtrefsaleorderno.Text = "";
            this.gvSaleOrder.DataSource = null;
            this.gvSaleOrder.DataBind();
            ClsSaleorderMM.ResetDataTables();   // Reset all Datatables
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "";
            this.divBtnSave.Style["display"] = "";
            this.gvSaleOrder.Columns[10].Visible = true;
            this.LoadSale();
            this.divnew.Visible = true;
            this.ResetHiddenField();
            this.ddlCurrency.Items.Clear();
            this.ddlCurrency.Items.Add(new ListItem("Select Currency", "0"));
            this.ddlCurrency.AppendDataBoundItems = true;
            this.chkMRPTag.Checked = false;
            // ==== CSD BS Checking
            if (Convert.ToString(ViewState["BSID"]).Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
            {
                this.trICDS.Visible = true;
            }
            else
            {
                this.trICDS.Visible = false;
            }

            //==== Export Checking ======== //
            if (Convert.ToString(ViewState["BSID"]).Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
            {
                this.tdQty.Visible = true;
                this.gvsaleorderdetailsdetails.Columns[6].Visible = true;
                this.gvsaleorderdetailsdetails.Columns[7].Visible = true;
            }
            else
            {
                this.tdQty.Visible = false;
                this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
                this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            }
            //=============================//
            this.txtICDS.Text = "";
            this.txtICDSDate.Text = "";
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
            if (this.txtqty.Text.Trim() == "0.00" || txtqty.Text.Trim() == "")
            {

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtqty.ClientID + "').focus(); ", true);
                MessageBox1.ShowWarning("Please Enter Qty");
                return;
            }
            int dtpofromdate = Convert.ToInt32(Conver_To_ISO(this.txtrequireddate.Text.Trim()));
            int dtpotodate = Convert.ToInt32(Conver_To_ISO(this.txttorequireddate.Text.Trim()));

            if (dtpofromdate > dtpotodate)
            {
                MessageBox1.ShowInfo("Order to date can not less than from date:<b><font color='red'> " + txtrequireddate.Text + "!</font><b>", 80, 500);
            }
            else
            {
                if (ClsSaleorderMM.SaleOrderRecordsCheck(ddlProductName.SelectedValue.ToString(), txtrequireddate.Text, txttorequireddate.Text) == 0)
                {
                    decimal covverqy = Convert.ToDecimal(txtqty.Text.Trim());
                    decimal rate = Convert.ToDecimal(txtrate.Text.Trim());
                    if (string.IsNullOrEmpty(txtdiscount.Text.Trim()))
                    {
                        txtdiscount.Text = "0";
                    }
                    decimal toalammount = covverqy * rate;
                    if (this.Hdn_productType.Value == "T")
                    {
                        this.txtqty.Text = "0";
                    }


                    DataTable dtrecords = ClsSaleorderMM.BindSaleOrderGridRecords(this.ddlProductName.SelectedValue.ToString(),
                                                                                 Convert.ToString(this.ddlProductName.SelectedItem.Text),
                                                                                 Convert.ToDecimal(txtqty.Text), ddlpackingsize.SelectedValue.ToString(),
                                                                                 ddlpackingsize.SelectedItem.Text.Trim(), txtrequireddate.Text.Trim(),
                                                                                 txttorequireddate.Text, Convert.ToDecimal(txtrate.Text.Trim()),
                                                                                 Convert.ToDecimal(txtdiscount.Text.Trim()), toalammount);

                    if (dtrecords.Rows.Count > 0)
                    {
                        ViewState["SumTotalAmount"] = 0;
                        gvSaleOrder.DataSource = dtrecords;
                        gvSaleOrder.DataBind();

                    }
                    else
                    {
                        gvSaleOrder.DataSource = null;
                        gvSaleOrder.DataBind();
                        ViewState["SumTotalAmount"] = "0.00";
                    }

                    this.txtTotalCase.Text = "0";
                    this.txtTotalPCS.Text = "0";
                    this.txtrate.Text = "";
                    this.ddlProductName.SelectedValue = "0";
                    this.ddlpackingsize.SelectedValue = "0";
                    this.txtqty.Text = "0.00";
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color=red>Product exists with same scheduled date!</font></b>", 80, 400);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlProductName.ClientID + "').focus(); ", true);
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
                DataTable dtPackSize = new DataTable();
                dtPackSize = ClsSaleorderMM.BindPackingSize(ddlProductName.SelectedValue); ;
                ddlpackingsize.DataSource = dtPackSize;
                if (dtPackSize.Rows.Count > 0)
                {
                    ddlpackingsize.DataValueField = "UOMID";
                    ddlpackingsize.DataTextField = "UOMNAME";
                    ddlpackingsize.DataBind();
                    ddlpackingsize.SelectedValue = Convert.ToString(dtPackSize.Rows[0]["UOMID"]).Trim();
                }

                string YMD = Conver_To_YMD(this.txtsaleorderdate.Text.Trim());

                //txtrate.Text = "10";

                DataTable dt = new DataTable();
                dt = ClsSaleorderMM.GetTradingSaleRate(this.ddlcustomer.SelectedValue.Trim(), this.ddlProductName.SelectedValue.Trim(), this.txtsaleorderdate.Text);

                if (dt.Rows.Count > 0)
                {
                    txtrate.Text = dt.Rows[0]["RATE"].ToString().Trim();
                    if (this.txtrate.Text == "T")
                    {
                        this.txtrate.Enabled = true;
                        this.txtrate.Text = "0";
                        this.Hdn_productType.Value = "T";
                    }
                    else
                    {
                        this.txtrate.Enabled = false;
                        this.Hdn_productType.Value = "F";
                    }
                    txtqty.Text = "0.00";
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Product rate not avilable for above customer</font></b>");
                    txtrate.Text = "";
                    txtdiscount.Text = "0";
                    ddlpackingsize.Items.Clear();
                    ddlpackingsize.Items.Add(new ListItem("UOM", "0"));
                    ddlpackingsize.AppendDataBoundItems = true;
                }
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

    protected void DeleteRecordSaleOrderDetails(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            int flag = 0;
            flag = ClsSaleorderMM.DeleteSaleOrderHeader(e.Record["SALEORDERID"].ToString());

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
            this.trCurrency.Visible = false;
            this.tdQty.Visible = false;
            this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            this.trICDS.Visible = false;
            this.LoadTermsConditions();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];

            ClsSaleorderMM.BindSaleOrderGrid();
            ddlgroup.Enabled = false;
            ddldeliveryterms.Style.Add("color", "black !important");
            ddlgroup.Style.Add("color", "black !important");
            ddlcustomer.Style.Add("color", "black !important");
            string saleorderno = hdn_saleorderno.Value.ToString();
            txtsaleorderno.Text = saleorderno;
            txtsaleorderno.Enabled = false;
            divsaleorderno.Style["display"] = "";
            divsaleorderno1.Style["display"] = "";
            this.divcancelorder.Style["display"] = "none";
            DataTable dtheader = new DataTable();
            dtheader = ClsSaleorderMM.FetchSaleOrderHeader(hdn_saleorderid.Value);

            if (dtheader.Rows.Count > 0)
            {
                this.ddlbsegment.SelectedValue = dtheader.Rows[0]["BSID"].ToString();
                this.ddlbsegment_SelectedIndexChanged(sender, e);

                this.ddlgroup.SelectedValue = dtheader.Rows[0]["GROUPID"].ToString();
                this.ddlcustomer.SelectedValue = dtheader.Rows[0]["CUSTOMERID"].ToString();
                this.txtsaleorderdate.Text = dtheader.Rows[0]["SALEORDERDATE"].ToString();
                this.txtremarks.Text = dtheader.Rows[0]["REMARKS"].ToString();
                if (dtheader.Rows[0]["ISCANCELLED"].ToString() == "Y")
                {
                    this.chkactive.Checked = true;
                }
                else
                {
                    this.chkactive.Checked = false;
                }
                this.txtrefsaleorderno.Text = dtheader.Rows[0]["REFERENCESALEORDERNO"].ToString();
                this.txtrefsaleorderdate.Text = dtheader.Rows[0]["REFERENCESALEORDERDATE"].ToString();
                this.LoadDeleveryTerms();
                this.ddldeliveryterms.SelectedValue = dtheader.Rows[0]["DELIVERYTERMSID"].ToString();
                this.txtTotalPCS.Text = dtheader.Rows[0]["TOTALPCS"].ToString().Trim();
            }
            this.imgPopuppodate.Visible = false;
            this.ddldeliveryterms.Enabled = false;
            this.ddlgroup.Enabled = false;
            this.ddlcustomer.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.divnew.Visible = false;
            this.divBtnSave.Style["display"] = "none";
            this.gvSaleOrder.Columns[10].Visible = false;
            ViewState["SumTotalAmount"] = 0;
            dtsaleorderrecord = ClsSaleorderMM.FetchSaleOrderDetails(hdn_saleorderid.Value);
            gvSaleOrder.DataSource = dtsaleorderrecord;
            gvSaleOrder.DataBind();
            HttpContext.Current.Session["SALEORDERRECORDS"] = dtsaleorderrecord;
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
            ClsVendor_TPU clsGetEntry = new ClsVendor_TPU();
            string ID = "";
            string mode = "SALE_FAC";
            ID = clsGetEntry.editGateEntryCheck(mode, hdn_saleorderid.Value);
            if (ID == "Y")
            {
                this.divBtnSave.Style["display"] = "none";
            }
            else
            {
                this.divBtnSave.Style["display"] = "";
            }


            this.gvSaleOrder.Columns[10].Visible = true;
            this.trCurrency.Visible = false;
            this.tdQty.Visible = false;
            this.gvsaleorderdetailsdetails.Columns[6].Visible = false;
            this.gvsaleorderdetailsdetails.Columns[7].Visible = false;
            this.trICDS.Visible = false;

            this.LoadTermsConditions();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            txtrequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];

            ClsSaleorderMM.BindSaleOrderGrid();
            ddlbsegment.Enabled = false;
            ddlbsegment.Style.Add("color", "black !important");
            ddlgroup.Enabled = false;
            ddlgroup.Style.Add("color", "black !important");
            ddldeliveryterms.Style.Add("color", "black !important");
            ddlcustomer.Style.Add("color", "black !important");
            string saleorderno = hdn_saleorderno.Value.ToString();
            txtsaleorderno.Text = saleorderno;
            txtsaleorderno.Enabled = false;
            divsaleorderno.Style["display"] = "";
            divsaleorderno1.Style["display"] = "";
            this.divcancelorder.Style["display"] = "";
            DataTable dtheader = new DataTable();
            dtheader = ClsSaleorderMM.FetchSaleOrderHeader(hdn_saleorderid.Value);
            if (dtheader.Rows.Count > 0)
            {
                this.LoadBUSINESSSEGMENT();
                this.ddlbsegment.SelectedValue = dtheader.Rows[0]["BSID"].ToString();
                //sthis.ddlbsegment_SelectedIndexChanged(sender, e);
                LoadPrincipleGroup(this.ddlbsegment.SelectedValue);
                this.ddlgroup.SelectedValue = dtheader.Rows[0]["GROUPID"].ToString();
                //this.ddlgroup_SelectedIndexChanged(sender, e);
                this.ddlcustomer.SelectedValue = dtheader.Rows[0]["CUSTOMERID"].ToString();
                this.txtsaleorderdate.Text = dtheader.Rows[0]["SALEORDERDATE"].ToString();
                this.txtremarks.Text = dtheader.Rows[0]["REMARKS"].ToString();
                if (dtheader.Rows[0]["ISCANCELLED"].ToString() == "Y")
                {
                    this.chkactive.Checked = true;
                }
                else
                {
                    this.chkactive.Checked = false;
                }
                this.txtrefsaleorderno.Text = dtheader.Rows[0]["REFERENCESALEORDERNO"].ToString();
                this.txtrefsaleorderdate.Text = dtheader.Rows[0]["REFERENCESALEORDERDATE"].ToString();
                this.LoadDeleveryTerms();
                this.ddldeliveryterms.SelectedValue = dtheader.Rows[0]["DELIVERYTERMSID"].ToString();
                this.txtTotalPCS.Text = dtheader.Rows[0]["TOTALPCS"].ToString().Trim();
            }
            this.imgPopuppodate.Visible = true;
            this.ddlgroup.Enabled = false;
            this.ddlcustomer.Enabled = false;
            this.ddldeliveryterms.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.trAddProduct.Style["display"] = "";
            this.btnadd.Visible = true;
            this.divnew.Visible = false;
            ViewState["SumTotalAmount"] = 0;
            dtsaleorderrecord = ClsSaleorderMM.FetchSaleOrderDetails(hdn_saleorderid.Value);
            gvSaleOrder.DataSource = dtsaleorderrecord;
            gvSaleOrder.DataBind();
            HttpContext.Current.Session["SALEORDERRECORDS"] = dtsaleorderrecord;
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
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
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
                HttpContext.Current.Session["SALEORDERRECORDS"] = dtsaleorderrecord;
                this.txtTotalCase.Text = "0";
                this.txtTotalPCS.Text = "0";
                LoadSaleOrderGrid();
                MessageBox1.ShowSuccess("<b><font color='green'>Record deleted successfully!</font></b>");
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Record deleted unsuccessful!</font></b>");
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

    #region Save Sale Order
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
            string getflag = string.Empty;
            string saleorderno = string.Empty;
            int CountTerms = 0;
            string strTermsID = string.Empty;
            string ProformaID = string.Empty;
            decimal TotalCase = 0;
            decimal TotalPCS = 0;

            // Logic to be implemented for Proforma Inv Value
            if (dtsaleorderrecord.Rows.Count > 0)
            {
                string xml = ConvertDatatableToXML(dtsaleorderrecord);
                string status = string.Empty;
                string MRPTag = "N";

                if (chkactive.Checked == true)
                {
                    status = "Y";
                }
                else
                {
                    status = "N";
                }

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

                saleorderno = ClsSaleorderMM.InsertSaleOrderDetails(this.txtsaleorderdate.Text.Trim(), this.txtrefsaleorderno.Text.Trim(), this.txtrefsaleorderdate.Text.Trim(),
                                                                     ddlbsegment.SelectedValue.ToString(), ddlbsegment.SelectedItem.ToString(), this.ddlgroup.SelectedValue.ToString(),
                                                                     this.ddlgroup.SelectedItem.Text, this.ddlcustomer.SelectedValue, this.ddlcustomer.SelectedItem.Text,
                                                                     this.txtremarks.Text, HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                                     this.hdn_saleorderid.Value.ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), xml,
                                                                     status, strTermsID, "0", "INR",
                                                                     MRPTag, Convert.ToString(this.ddldeliveryterms.SelectedValue.Trim()), this.ddldeliveryterms.SelectedItem.ToString().Trim(),
                                                                     this.txtICDS.Text.Trim(), this.txtICDSDate.Text.Trim(), TotalCase, TotalPCS, Convert.ToString(HttpContext.Current.Session["DEPOTID"]));
                if (!string.IsNullOrEmpty(saleorderno))
                {
                    if (Convert.ToString(hdn_saleorderno.Value) == "")
                    {
                        MessageBox1.ShowSuccess("SaleOrder No : <b><font color='green'>" + saleorderno + "</font></b> saved successfully,", 40, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("SaleOrder No : <b><font color='green'>" + saleorderno + "</font></b> updated successfully", 40, 550);
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
                    txtrequireddate.Text = "";
                    txttorequireddate.Text = "";
                    txtrefsaleorderdate.Text = "";
                    txtrefsaleorderno.Text = "";
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
        ddlgroup.SelectedValue = "0";
        ddlgroup.Enabled = true;
        ddlProductName.SelectedValue = "0";
        txtremarks.Text = "";
        txtqty.Text = "";
        txtrequireddate.Text = "";
        txttorequireddate.Text = "";
        divnew.Visible = true;
        txtdiscount.Text = "0";
        txtrate.Text = "";
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
        ClsSaleorderMM.ResetDataTables();   // Reset all Datatables
        LoadSale();
        this.txtTotalCase.Text = "0";
        this.txtTotalPCS.Text = "0";
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
        DataTable dt = new DataTable();
        if (hdn_saleorderid.Value != "")
        {
            dt = ClsSaleorderMM.EditTerms(hdn_saleorderid.Value);
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
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                ViewState["SumTotalAmount"] = Convert.ToString(String.Format("{0:0.00}", double.Parse(ViewState["SumTotalAmount"].ToString()) + double.Parse(string.IsNullOrEmpty(e.Row.Cells[7].Text.Trim()) ? "0" : e.Row.Cells[7].Text.Trim())));
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                e.Row.Cells[5].Text = "Total (Rs.):";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[7].Text = ViewState["SumTotalAmount"].ToString();
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                ViewState["SumTotalAmount"] = 0;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
    //==================Added by Sourav Mukherjee on 30/08/2016====================//

    #region LoadDeleveryTerms
    public void LoadDeleveryTerms()
    {
        DataTable dtDeliveryTerms = HttpContext.Current.Cache["DeliveryTerms"] as DataTable;
        if (dtDeliveryTerms == null)
        {
            dtDeliveryTerms = ClsSaleorderMM.BindDeliveryTerms();
            if (dtDeliveryTerms.Rows.Count > 0)
            {
                HttpContext.Current.Cache["DeliveryTerms"] = dtDeliveryTerms;
                HttpContext.Current.Cache.Insert("DeliveryTerms", dtDeliveryTerms, null, DateTime.Now.AddDays(90), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
        if (dtDeliveryTerms != null)
        {
            ddldeliveryterms.Items.Clear();
            ddldeliveryterms.AppendDataBoundItems = true;
            ddldeliveryterms.DataSource = dtDeliveryTerms;
            ddldeliveryterms.SelectedValue = "1";
            ddldeliveryterms.DataValueField = "ID";
            ddldeliveryterms.DataTextField = "NAME";
            ddldeliveryterms.DataBind();
        }
    }
    #endregion

    #region ddlbsegment_SelectedIndexChanged
    public void ddlbsegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlbsegment.SelectedValue != "0")
            {
                LoadPrincipleGroup(ddlbsegment.SelectedValue.ToString());
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void Btnshow_Click(object sender, EventArgs e)
    {

        DataTable dtsaleorderrecord = (DataTable)HttpContext.Current.Session["SALEORDERRECORDS"];
        if (dtsaleorderrecord.Rows.Count > 0)
        {
            mp1.Show();
            lblOrderDate.Text = txtsaleorderdate.Text;
            lblCustomer.Text = ddlcustomer.SelectedItem.ToString();
            grvSaleOrderCopy.DataSource = dtsaleorderrecord;
            grvSaleOrderCopy.DataBind();
            CalculateFooterAmount(dtsaleorderrecord);
        }
        else
        {
            MessageBox1.ShowWarning("Add Record First");
            return;
        }
    }

    #region CalculateTotalAmount
    public void CalculateFooterAmount(DataTable dt)
    {
        decimal TotalPcs = 0;
        decimal TotalAmount = 0;
        if (dt.Rows.Count > 0)
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                TotalPcs += Convert.ToDecimal(dt.Rows[Counter]["ORDERQTY"]);
                TotalAmount += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);
            }
        }

        lblTotalPcs.Text = Convert.ToString(TotalPcs);
        lblTotalAmnt.Text = Convert.ToString(TotalAmount);


    }
    #endregion
}