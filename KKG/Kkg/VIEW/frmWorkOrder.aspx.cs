using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmWorkOrder : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                divpono.Style["display"] = "none";
                divponoheader.Style["display"] = "none";
                InputTable.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";

                /*DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                txtpodate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                txttodate.Text = dtcurr.ToString(date).Replace('-', '/');*/

                this.DateLock();
                this.LoadPacksize();
                this.LoadPO();
                LoadTPUName();
                //ClsWOOrder clspurchaseorder = new ClsWOOrder();
                //clspurchaseorder.BindPackingSize(); // Datatable fill PackingSize Conversation from [M_PRODUCT_UOM_MAP] TABLE
            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadPO
    public void LoadPO()
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            gvpodetails.DataSource = clspurchaseorder.BindPO(Convert.ToString(this.txtfromdate.Text), Convert.ToString(this.txttodate.Text), Convert.ToString(Session["FINYEAR"]), Convert.ToString(Session["DEPOTID"]));
            gvpodetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadPOGrid
    public void LoadPOGrid()
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            gvPurchaseOrder.DataSource = clspurchaseorder.BindPOGrid(hdn_pofield.Value.ToString());
            gvPurchaseOrder.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadTPU
    public void LoadTPUName()
    {
        try
        {
            ddlTPUName.Items.Clear();
            ddlTPUName.Items.Add(new ListItem("SELECT TPU NAME", "0"));
            ddlTPUName.AppendDataBoundItems = true;
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            ddlTPUName.DataSource = clspurchaseorder.BindTPU();
            ddlTPUName.DataValueField = "VENDORID";
            ddlTPUName.DataTextField = "VENDORNAME";
            ddlTPUName.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadOrderType
    public void LoadOrderType()
    {
        try
        {
            ddlorderfor.Items.Clear();
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            ddlorderfor.DataSource = clspurchaseorder.BindOrderType();
            ddlorderfor.DataValueField = "BSID";
            ddlorderfor.DataTextField = "BSNAME";
            ddlorderfor.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadPacksize
    public void LoadPacksize()
    {
        try
        {
            ddlpacksize.Items.Clear();
            //ddlpacksize.Items.Add(new ListItem("SELECT", "0"));
            ddlpacksize.AppendDataBoundItems = true;
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            ddlpacksize.DataSource = clspurchaseorder.BindPacksize();
            ddlpacksize.DataValueField = "UOMID";
            ddlpacksize.DataTextField = "UOMNAME";
            ddlpacksize.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ddlTPUName_SelectedIndexChanged
    protected void ddlTPUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTPUName.SelectedValue == "0")
            {

                this.grdpodetailsadd.DataSource = null;
                this.grdpodetailsadd.DataBind();
            }
            else
            {
                this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim(), this.ddlpacksize.SelectedValue.Trim());
                Session["BOMDATA"] = null; /*subhodip*/
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    # region btn Schedule date
    protected void btnscheduledate_Click(object sender, EventArgs e)
    {
        try
        {
            int delfromdate = Convert.ToInt32(Conver_To_ISO(this.txtdelfromdate.Text.Trim()));
            int deltodate = Convert.ToInt32(Conver_To_ISO(this.txtdeltodate.Text.Trim()));

            int podate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));
            if (this.ddlTPUName.SelectedValue.Trim() == "9BCE2A2E-18A2-4EBD-AA3B-69471965B1E8" && this.ddlpacksize.SelectedValue.Trim() != "71B973E8-28E3-4F3A-A86E-9475AF2D14EE")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Purchase unit should be BOX for Bindi</font></b>", 60, 500);
                return;
            }

            if (deltodate < delfromdate)
            {
                MessageBox1.ShowInfo("<b><font color='red'>DELIVERY TO DATE MUST BE GREATER THAN DELIVERY  FROM DATE</font></b>", 60, 550);
                return;
            }
            if (deltodate >= delfromdate)
            {
                if (grdpodetailsadd.Rows.Count > 0)
                {
                    this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim(), this.ddlpacksize.SelectedValue.Trim());
                    for (int grddate = 0; grddate < grdpodetailsadd.Rows.Count; grddate++)
                    {
                        TextBox grdtxtdeliverydatefrom = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydatefrom");
                        TextBox grdtxtdeliverydateto = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydateto");
                        grdtxtdeliverydatefrom.Text = this.txtdelfromdate.Text.Trim();
                        grdtxtdeliverydateto.Text = this.txtdeltodate.Text.Trim();
                        Label grdlblpacksizeid = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizeid");
                        grdlblpacksizeid.Text = this.ddlpacksize.SelectedValue.Trim();
                        Label grdlblpacksizename = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizename");
                        grdlblpacksizename.Text = this.ddlpacksize.SelectedItem.ToString().Trim();

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
    # endregion

    #region grdpodetailsadd_RowDataBound
    protected void grdpodetailsadd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblpcsqty = (Label)e.Row.FindControl("grdlblpcsqty");
                TextBox lblpurchasecost = (TextBox)e.Row.FindControl("grdlblpurchasecost");
                TextBox lblproductamt = (TextBox)e.Row.FindControl("grdlblbasiccostvalue");
                TextBox poqty = (TextBox)e.Row.FindControl("grdtxtpoqty");
                TextBox lbltotalmrp = (TextBox)e.Row.FindControl("grdlblmrpvalue");
                Label lbmrp = (Label)e.Row.FindControl("grdlblmrp");
                //TextBox grdtxtdeliverydatefrom = (TextBox)e.Row.FindControl("grdtxtdeliverydatefrom");
                Double pcsqty = Convert.ToDouble(lblpcsqty.Text);
                decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text);
                Double mrp = Convert.ToDouble(lbmrp.Text);
                poqty.Attributes.Add("onkeyup", "Calpurchaseorderdetails( " + pcsqty + "," + purchasecost + ",'" + poqty.ClientID + "'," + lblproductamt.ClientID + "," +
                                                                          " " + lbltotalmrp.ClientID + "," + mrp + ");");

                poqty.Attributes.Add("onkeypress", "return isNumberKey('" + poqty.ClientID + "')");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Add New Record
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.div_btnPrint.Style["display"] = "none";
            this.divProductDetails.Style["display"] = "";
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";

            this.hdn_convertionqty.Value = "";
            this.Hdn_Fld.Value = "";
            this.hdn_podelete.Value = "";
            this.hdn_pofield.Value = "";
            this.hdnExcise.Value = "";

            this.ddlTPUName.Enabled = true;
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables

            InputTable.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            divpono.Style["display"] = "none";
            divponoheader.Style["display"] = "none";
            divadd.Style["display"] = "none";
            InputTable.Enabled = true;
            imgPopuppodate.Visible = true;
            clspurchaseorder.BindPurchaseOrderGrid();
            this.TaxPercentage();

            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";

            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";
            this.ddlTPUName.SelectedValue = "0";
            this.btnsave.Enabled = true;
            this.ddlorderfor.Enabled = true;
            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            this.divbtnsave.Style["display"] = "";
            this.tblADD.Style["display"] = "";
            this.LoadOrderType();
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            Session["PORECORDSV2"] = null;
            Session["BOMDATA"] = null;
            this.txtdelfromdate.Text = "";
            this.txtdeltodate.Text = "";
            trscheduledate.Style["display"] = "";
            this.GvBomDtls.DataSource = null;
            this.GvBomDtls.DataBind();
            BindFactoryName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region TaxPercentage
    protected void TaxPercentage()
    {
        try
        {
            DataTable dt = new DataTable();
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            dt = clspurchaseorder.TaxPercentage();
            if (dt.Rows.Count > 0)
            {
                this.hdnExcise.Value = dt.Rows[0]["PERCENTAGE"].ToString();
                this.hdnCST.Value = dt.Rows[1]["PERCENTAGE"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ResetAll
    public void ResetAll()
    {
        try
        {
            this.txtcasepack.Text = "";
            this.btnsave.Enabled = true;
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";
            // this.hdnAssesment.Value = "";
            this.hdn_convertionqty.Value = "";
            // this.Hdn_Fld.Value = "";
            this.hdn_podelete.Value = "";
            this.hdn_pofield.Value = "";
            this.hdnExcise.Value = "";
            // this.hdnMRP.Value = "";
            ddlTPUName.SelectedValue = "0";
            ddlTPUName.Enabled = true;


            //this.txtforecastedqty.Text = "";
            //this.txtnxtmnthforecast.Text = "";
            txtremarks.Text = "";
            //txtqty.Text = "";
            //txtprice.Text = "";
            //txtrequireddate.Text = "";
            txtgrosstotal.Text = "0";
            txtadjustment.Text = "0";
            txtTotalMRP.Text = "0";

            txtpacking.Text = "0";
            txtsaletax.Text = "0";
            txtexercise.Text = "0";

            txttotalamount.Text = "0";
            txtnettotal.Text = "0";

            //this.gvPurchaseOrder.ClearPreviousDataSource();
            //this.gvPurchaseOrder.DataSource = null;
            //this.gvPurchaseOrder.DataBind();
            //this.txtprice.Text = "";
            //this.txtqty.Text = "";
            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";
            //this.txtnxtmnthforecast.Text = "0";
            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";
            this.ddlTPUName.SelectedValue = "0";
            //this.ddlpacksize.SelectedValue = "0";
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables
            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            divadd.Style["display"] = "";
            divpono.Style["display"] = "none";
            divponoheader.Style["display"] = "none";
            LoadPO();

            //clearTable();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.ResetAll();
            this.divProductDetails.Style["display"] = "";
            this.divbtnsave.Style["display"] = "";
            this.tblADD.Style["display"] = "";
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            this.LoadPO();
            this.ddlorderfor.Enabled = true;
            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            Session["PORECORDSV2"] = null;
            this.txtdelfromdate.Text = "";
            this.txtdeltodate.Text = "";
            trscheduledate.Style["display"] = "";
            Session["BOMDATA"] = null;
            GvBomDtls.DataSource = null;
            GvBomDtls.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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

    #region CountPOQty
    protected decimal CountPOQty()
    {
        decimal ReturnValue = 0;

        if (grdpodetailsadd.Rows.Count > 0)
        {
            for (int i = 0; i < grdpodetailsadd.Rows.Count; i++)
            {
                TextBox grdtxtpoqty = (TextBox)grdpodetailsadd.Rows[i].FindControl("grdtxtpoqty");

                if (grdtxtpoqty.Text != "" && Convert.ToDecimal(grdtxtpoqty.Text) > 0)
                {
                    ReturnValue += Convert.ToDecimal(grdtxtpoqty.Text.Trim());

                }
            }
        }
        return ReturnValue;
    }
    #endregion

    #region BindPurchaseOrderGridStructure
    public DataTable BindPurchaseOrderGridStructure()
    {
        DataTable dtPORecord = new DataTable();
        dtPORecord.Clear();
        dtPORecord.Columns.Add(new DataColumn("DIVISIONID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("DIVISIONNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("NATUREOFPRODUCTID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("NATUREOFPRODUCTNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTQTY", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPACKINGSIZEID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPACKINGSIZE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTCONVERSIONQTY", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPRICE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTAMOUNT", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("MRP", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("MRPVALUE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("EXCISE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("CST", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("REQUIREDDATE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("MODE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("STOCKQTY", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTRATE", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        HttpContext.Current.Session["PORECORDSV2"] = dtPORecord;
        return dtPORecord;
    }
    #endregion

    #region CalculateTotalAmount
    decimal CalculateTotalAmount(DataTable dt)
    {
        decimal GrossTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return GrossTotal;
    }
    #endregion

    #region CalculateTotalMRP
    decimal CalculateTotalMRP(DataTable dt)
    {
        decimal MRPTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                MRPTotal += Convert.ToDecimal(dt.Rows[Counter]["MRPVALUE"]);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return MRPTotal;
    }
    #endregion

    #region CalculateTotalExcise
    decimal CalculateTotalExcise(DataTable dt)
    {
        decimal ExciseTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                ExciseTotal += Convert.ToDecimal(dt.Rows[Counter]["MRPVALUE"]) * (Convert.ToDecimal(dt.Rows[Counter]["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dt.Rows[Counter]["EXCISE"]) / 100);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return ExciseTotal;
    }
    #endregion

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            DataTable dtBomDtls = new DataTable();

            if (Session["BOMDATA"] == null)
            {
                CreateBomData();
            }
            dtBomDtls = (DataTable)Session["BOMDATA"];

            if (HttpContext.Current.Session["PORECORDSV2"] == null)
            {
                this.BindPurchaseOrderGridStructure();
            }
            DataTable dtPurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];
            DataTable dtFetchBOM = new DataTable();
            int dtpoEntrydate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));
            int flag = 0;
            decimal TotPOQty = 0;
            int STATUS = 0;
            string ProductName = string.Empty;
            decimal GrossTotal = 0;
            decimal NetTotal = 0;
            decimal TotalMRP = 0;
            decimal TotalExcise = 0;
            decimal TotalCST = 0;

            TotPOQty = CountPOQty();
            if (TotPOQty == 0)
            {
                MessageBox1.ShowInfo("<b><font color='red'>Please Enter PO Quantity..!</font></b>", 50, 500);
                return;
            }
            if (grdpodetailsadd.Rows.Count > 0)
            {
                for (int grdloop = 0; grdloop < grdpodetailsadd.Rows.Count; grdloop++)
                {
                    Label grdlblproductid = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblproductid");
                    Label grdlblproductname = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblproductname");
                    Label grdlblpacksizeid = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpacksizeid");
                    Label grdlblpacksizename = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpacksizename");
                    Label grdlblpcsqty = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpcsqty");
                    TextBox grdlblpurchasecost = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpurchasecost");
                    TextBox grdtxtpoqty = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtpoqty");
                    TextBox grdlblbasiccostvalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblbasiccostvalue");
                    Label grdlblmrp = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblmrp");
                    TextBox grdlblmrpvalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblmrpvalue");
                    Label grdlblassesmentpercent = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblassesmentpercent");
                    Label grdlblexcise = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblexcise");
                    Label grdlblcst = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblcst");
                    TextBox grdlblexcisevalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblexcisevalue");
                    TextBox grdtxtdeliverydatefrom = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtdeliverydatefrom");
                    TextBox grdtxtdeliverydateto = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtdeliverydateto");

                    Label grdlblDIVISIONID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblDIVISIONID");
                    Label grdlblDIVISIONNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblDIVISIONNAME");
                    Label grdlblCATEGORYID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblCATEGORYID");
                    Label grdlblCATEGORYNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblCATEGORYNAME");
                    Label grdlblNATUREOFPRODUCTID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblNATUREOFPRODUCTID");
                    Label grdlblNATUREOFPRODUCTNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblNATUREOFPRODUCTNAME");
                    Label grdlblUOMID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblUOMID");
                    Label grdlblUOMNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblUOMNAME");
                    Label grdlblStockQty = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblStockQty");

                    if (Convert.ToDecimal(grdtxtpoqty.Text.Trim()) != 0 && grdtxtpoqty.Text.Trim() != "")
                    {
                       
                        flag = clspurchaseorder.PORecordsCheckV2(grdlblproductid.Text.Trim(), grdlblpacksizeid.Text.Trim(), grdtxtdeliverydatefrom.Text.Trim(),
                                                                  grdtxtdeliverydateto.Text.Trim(), hdn_pofield.Value.ToString());

                        if (flag == 0)
                        {
                            hdn_convertionqty.Value = Convert.ToString(Convert.ToDecimal(grdtxtpoqty.Text.Trim()) * Convert.ToDecimal(grdlblpcsqty.Text.Trim()));

                            #region ADD MODE
                            if (this.hdn_pofield.Value.ToString() == "")
                            {
                                if (STATUS == 0)
                                {                                    
                                  //  DataTable DtmaxRate = new DataTable();
                                    //DtmaxRate = clspurchaseorder.FetchMaxRate(grdlblproductid.Text);
                                   // string MaxRate = DtmaxRate.Rows[0]["MAXRATE"].ToString();

                                    DataRow dr = dtPurchaseOrderDetails.NewRow();
                                    dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                    dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                    dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                    dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                    dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                    dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                    dr["UOMID"] = Convert.ToString(grdlblUOMID.Text.Trim());
                                    dr["UOMNAME"] = Convert.ToString(grdlblUOMNAME.Text.Trim());
                                    dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                    dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                    dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                    dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(grdlblpacksizeid.Text.Trim());
                                    dr["PRODUCTPACKINGSIZE"] = Convert.ToString(grdlblpacksizename.Text.Trim());
                                    dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                    dr["PRODUCTPRICE"] = Convert.ToDecimal(grdlblpurchasecost.Text);
                                    dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                    dr["MRP"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));
                                    dr["MRPVALUE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                    dr["EXCISE"] = Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                    dr["CST"] = Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                    dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                    dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                    dr["MODE"] = "A";
                                    dr["STOCKQTY"] = Convert.ToString(grdlblStockQty.Text.Trim());
                                    dr["PRODUCTRATE"] = Convert.ToDecimal(grdlblpurchasecost.Text);
                                    dr["AMOUNT"] = Convert.ToDecimal(grdtxtpoqty.Text) * Convert.ToDecimal(grdlblpurchasecost.Text);                                    

                                    #region Generate BOM

                                    string ProductWithQty = string.Empty;
                                    ProductWithQty += grdlblproductid.Text + '|' + grdtxtpoqty.Text + ',';

                                    dtFetchBOM = clspurchaseorder.FetchBom(ProductWithQty, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserID"].ToString());
                                    if (dtFetchBOM.Rows.Count > 0)
                                    {
                                        dtPurchaseOrderDetails.Rows.Add(dr);
                                        dtPurchaseOrderDetails.AcceptChanges();

                                        for (int i = 0; i < dtFetchBOM.Rows.Count; i++)
                                        {
                                            DataRow drBom = dtBomDtls.NewRow();
                                            drBom["REQUISITIONID"] = "";
                                            drBom["CATEGORYID"] = dtFetchBOM.Rows[i]["CATEGORYID"].ToString().Trim();
                                            drBom["CATEGORYNAME"] = dtFetchBOM.Rows[i]["CATEGORYNAME"].ToString().Trim();
                                            drBom["PRODUCTID"] = dtFetchBOM.Rows[i]["PRODUCTID"].ToString().Trim();
                                            drBom["PRODUCTNAME"] = dtFetchBOM.Rows[i]["PRODUCTNAME"].ToString().Trim();
                                            drBom["UOMID"] = dtFetchBOM.Rows[i]["UOMID"].ToString().Trim();
                                            drBom["UOMNAME"] = dtFetchBOM.Rows[i]["UOMNAME"].ToString().Trim();
                                            drBom["QTY"] = dtFetchBOM.Rows[i]["QTY"].ToString().Trim();
                                            drBom["REQUIREDFROMDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                            drBom["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                            drBom["WORKORDERPRODUCTID"] = grdlblproductid.Text;
                                            drBom["STOCKQTY"] = dtFetchBOM.Rows[i]["STOCKQTY"].ToString().Trim();
                                            drBom["STORELOCATIONID"] = dtFetchBOM.Rows[i]["STORELOCATIONID"].ToString().Trim();
                                            drBom["STORELOCATIONAME"] = dtFetchBOM.Rows[i]["STORELOCATIONAME"].ToString().Trim();

                                            dtBomDtls.Rows.Add(drBom);
                                            dtBomDtls.AcceptChanges();
                                        }
                                        /*GvBomDtls.DataSource = dtFetchBOM;
                                        GvBomDtls.DataBind();
                                        Session["BOMDATA"] = dtFetchBOM;*/

                                        GvBomDtls.DataSource = dtBomDtls;
                                        GvBomDtls.DataBind();
                                        Session["BOMDATA"] = dtBomDtls;
                                    }
                                    else
                                    {                                        
                                        MessageBox1.ShowInfo("<b><font color='red'>Bom is not create of this product</font></br></br><font color='green'> " + grdlblproductname.Text + "</font></b>", 60, 500);
                                        //gvPurchaseOrder.DataSource = null;
                                        //gvPurchaseOrder.DataBind();
                                        //GvBomDtls.DataSource = null;
                                        //GvBomDtls.DataBind();
                                    }
                                    #endregion
                                    //}
                                }
                            }
                            #endregion

                            #region EDIT MODE
                            else
                            {
                                if (Convert.ToDecimal(grdtxtpoqty.Text.Trim()) != 0 && grdtxtpoqty.Text.Trim() != "")
                                {
                                    //if (grdtxtdeliverydatefrom.Text.Trim() == "")
                                    //{
                                    //    ProductName = grdlblproductname.Text.Trim();
                                    //    STATUS = 1;

                                    //    DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                    //    dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                    //    DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                    //    for (int i = 0; i < drr.Length; i++)
                                    //    {
                                    //        drr[i].Delete();
                                    //        dtdeletePurchaseOrderDetails.AcceptChanges();
                                    //    }
                                    //    HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                    //    break;
                                    //}

                                    //if (grdtxtdeliverydateto.Text.Trim() == "")
                                    //{
                                    //    ProductName = grdlblproductname.Text.Trim();
                                    //    STATUS = 2;
                                    //    DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                    //    dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                    //    DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                    //    for (int i = 0; i < drr.Length; i++)
                                    //    {
                                    //        drr[i].Delete();
                                    //        dtdeletePurchaseOrderDetails.AcceptChanges();
                                    //    }
                                    //    HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                    //    break;
                                    //}

                                    //if (dtpofromdate < dtpoEntrydate)
                                    //{
                                    //    ProductName = grdlblproductname.Text.Trim();
                                    //    STATUS = 3;
                                    //    DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                    //    dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                    //    DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                    //    for (int i = 0; i < drr.Length; i++)
                                    //    {
                                    //        drr[i].Delete();
                                    //        dtdeletePurchaseOrderDetails.AcceptChanges();
                                    //    }
                                    //    HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                    //    break;
                                    //}

                                    //if (dtpotodate < dtpofromdate)
                                    //{
                                    //    ProductName = grdlblproductname.Text.Trim();
                                    //    STATUS = 4;
                                    //    DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                    //    dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                    //    DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                    //    for (int i = 0; i < drr.Length; i++)
                                    //    {
                                    //        drr[i].Delete();
                                    //        dtdeletePurchaseOrderDetails.AcceptChanges();
                                    //    }
                                    //    HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                    //    break;
                                    //}

                                    if (STATUS == 0)
                                    {
                                        if (grdtxtdeliverydatefrom.Text.Trim() != "" && grdtxtdeliverydateto.Text.Trim() != "")
                                        {
                                            DataTable DtmaxRate = new DataTable();
                                            DtmaxRate = clspurchaseorder.FetchMaxRate(grdlblproductid.Text);
                                            string MaxRate = DtmaxRate.Rows[0]["MAXRATE"].ToString();

                                            DataRow dr = dtPurchaseOrderDetails.NewRow();
                                            dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                            dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                            dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                            dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                            dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                            dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                            dr["UOMID"] = Convert.ToString(grdlblUOMID.Text.Trim());
                                            dr["UOMNAME"] = Convert.ToString(grdlblUOMNAME.Text.Trim());
                                            dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                            dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                            dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                            dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(grdlblpacksizeid.Text.Trim());
                                            dr["PRODUCTPACKINGSIZE"] = Convert.ToString(grdlblpacksizename.Text.Trim());
                                            dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                            dr["PRODUCTPRICE"] = Convert.ToString(grdlblpurchasecost.Text.Trim());
                                            dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                            dr["MRP"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));
                                            dr["MRPVALUE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                            dr["ASSESMENTPERCENTAGE"] = Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                            dr["EXCISE"] = Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                            dr["CST"] = Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                            dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                            dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                            dr["MODE"] = "E";
                                            dr["STOCKQTY"] = Convert.ToString(grdlblStockQty.Text.Trim());
                                            dr["PRODUCTRATE"] = Convert.ToDecimal(MaxRate);
                                            dr["AMOUNT"] = Convert.ToDecimal(grdtxtpoqty.Text) * Convert.ToDecimal(MaxRate);

                                            dtPurchaseOrderDetails.Rows.Add(dr);
                                            dtPurchaseOrderDetails.AcceptChanges();

                                            #region Generate BOM

                                            string ProductWithQty = string.Empty;
                                            ProductWithQty += grdlblproductid.Text + '|' + grdtxtpoqty.Text + ',';
                                            // CreateBomData();
                                            //  DataTable dtBomDtls = (DataTable)Session["BOMDATA"];
                                            dtFetchBOM = clspurchaseorder.FetchBom(ProductWithQty, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserID"].ToString());
                                            if (dtFetchBOM.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dtFetchBOM.Rows.Count; i++)
                                                {
                                                    DataRow drBom = dtBomDtls.NewRow();
                                                    drBom["REQUISITIONID"] = "";
                                                    drBom["CATEGORYID"] = dtFetchBOM.Rows[i]["CATEGORYID"].ToString().Trim();
                                                    drBom["CATEGORYNAME"] = dtFetchBOM.Rows[i]["CATEGORYNAME"].ToString().Trim();
                                                    drBom["PRODUCTID"] = dtFetchBOM.Rows[i]["PRODUCTID"].ToString().Trim();
                                                    drBom["PRODUCTNAME"] = dtFetchBOM.Rows[i]["PRODUCTNAME"].ToString().Trim();
                                                    drBom["UOMID"] = dtFetchBOM.Rows[i]["UOMID"].ToString().Trim();
                                                    drBom["UOMNAME"] = dtFetchBOM.Rows[i]["UOMNAME"].ToString().Trim();
                                                    drBom["QTY"] = dtFetchBOM.Rows[i]["QTY"].ToString().Trim();
                                                    drBom["REQUIREDFROMDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                                    drBom["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                                    drBom["WORKORDERPRODUCTID"] = grdlblproductid.Text;
                                                    drBom["STOCKQTY"] = dtFetchBOM.Rows[i]["STOCKQTY"].ToString().Trim();

                                                    dtBomDtls.Rows.Add(drBom);
                                                    dtBomDtls.AcceptChanges();
                                                }
                                                GvBomDtls.DataSource = dtBomDtls;
                                                GvBomDtls.DataBind();
                                                Session["BOMDATA"] = dtBomDtls;
                                            }
                                            else
                                            {
                                                GvBomDtls.DataSource = null;
                                                GvBomDtls.DataBind();
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Amount Calculation
                            if (dtPurchaseOrderDetails.Rows.Count > 0)
                            {
                                GrossTotal = CalculateTotalAmount(dtPurchaseOrderDetails);
                                NetTotal = GrossTotal;
                                TotalMRP = CalculateTotalMRP(dtPurchaseOrderDetails);
                                TotalExcise = CalculateTotalExcise(dtPurchaseOrderDetails);
                                TotalCST = ((GrossTotal + TotalExcise) * (Convert.ToDecimal(dtPurchaseOrderDetails.Rows[0]["CST"]) / 100));

                                txtgrosstotal.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                                txtTotalMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                                txtexercise.Text = Convert.ToString(String.Format("{0:0.00}", TotalExcise));
                                txtsaletax.Text = Convert.ToString(String.Format("{0:0.00}", TotalCST));
                                txttotalamount.Text = Convert.ToString(String.Format("{0:0.00}", NetTotal));
                            }

                            #endregion

                            #region Total Case
                            txtcasepack.Text = Convert.ToString(TotalCasePack(hdn_pofield.Value.ToString()));
                            #endregion

                        }
                        else
                        {
                            ProductName = grdlblproductname.Text.Trim();
                            STATUS = 5;
                            break;
                        }
                    }
                }
                if (STATUS == 5)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Record already exists</font> for </br></br><font color='green'> " + ProductName + "</font><font color='red'> with same schedule Date</font></b>", 60, 650);
                    //if (dtPurchaseOrderDetails.Rows.Count > 0)
                    if (GvBomDtls.Rows.Count > 0)
                    {
                        this.gvPurchaseOrder.DataSource = dtPurchaseOrderDetails;
                        this.gvPurchaseOrder.DataBind();
                    }
                    else
                    {
                        this.gvPurchaseOrder.DataSource = null;
                        this.gvPurchaseOrder.DataBind();
                    }
                    return;
                }
                //if (dtPurchaseOrderDetails.Rows.Count > 0)
                if (GvBomDtls.Rows.Count > 0)
                {
                    this.gvPurchaseOrder.DataSource = dtPurchaseOrderDetails;
                    this.gvPurchaseOrder.DataBind();
                }
                else
                {
                    this.gvPurchaseOrder.DataSource = null;
                    this.gvPurchaseOrder.DataBind();
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

    #region TotalCasePack
    public decimal TotalCasePack(string hdnvalue)
    {
        decimal casepack = 0;
        decimal convertioncasepack = 0;
        ClsWOOrder clspurchaseorder = new ClsWOOrder();


        DataTable dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDSV2"];
        foreach (DataRow dr in dtPORecord.Rows)
        {
            String[] productid = dr["PRODUCTID"].ToString().Trim().Split('#');
            string pid = productid[0].Trim();
            if (this.ddlpacksize.SelectedValue.Trim() != "B1D2969B-78D3-43A6-A26F-70E702203EF0")
            {
                convertioncasepack = clspurchaseorder.GetPackingSize_OnCall(pid, dr["PRODUCTPACKINGSIZEID"].ToString().Trim(), Convert.ToDecimal(dr["PRODUCTQTY"].ToString().Trim()));
            }
            else
            {
                convertioncasepack = Convert.ToDecimal(dr["PRODUCTQTY"].ToString().Trim());
            }
            casepack = casepack + convertioncasepack;
        }


        return casepack;
    }
    #endregion

    #region DeleteRecordPoDetails
    protected void DeleteRecordPoDetails(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string POID = Convert.ToString(e.Record["POID"]).Trim();
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            if (clspurchaseorder.GetPUstatus(POID) == "1")
            {
                //e.Record["Error"] = "Producion Update already done,not allow to delete record!";
                e.Record["Error"] = "Job Order(OutWard) already done,not allow to delete record!";
                return;
            }

            int flag = 0;
            //flag = clspurchaseorder.POEditedRecordDELETE(e.Record["POID"].ToString(), Session["FINYEAR"].ToString(), "N");
            flag = clspurchaseorder.DeleteWorkOrder(e.Record["POID"].ToString(), Session["FINYEAR"].ToString(), "N");
            if (flag == 1)
            {
                LoadPO();
                e.Record["Error"] = "Record Deleted Successfully!";
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
    #endregion

    #region Edit Purchase Order
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            DataSet ds = new DataSet();
            string POID = hdn_pofield.Value.ToString().Trim();

            if (clspurchaseorder.GetPUstatusV2(POID) == "1")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Producion Update already done,not allow to edit!.</font></b>", 50, 450);
                return;
            }
            else
            {
                ds = clspurchaseorder.BindPurchaseOrderDetails(POID, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserID"].ToString());

                #region Header Information
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.LoadOrderType();
                    this.LoadTPUName();
                    this.LoadPacksize();
                    this.BindFactoryName();
                    this.ddlTPUName.SelectedValue = ds.Tables[0].Rows[0]["TPUID"].ToString().Trim();
                    this.ddlfactory.SelectedValue = ds.Tables[0].Rows[0]["FACTORYID"].ToString().Trim();
                    this.ddlorderfor.SelectedValue = ds.Tables[0].Rows[0]["ORDERTYPEID"].ToString().Trim();
                    this.txtpodate.Text = ds.Tables[0].Rows[0]["PODATE"].ToString().Trim();
                    this.txtcasepack.Text = ds.Tables[0].Rows[0]["TOTALCASEPACK"].ToString().Trim();
                    this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString().Trim();
                    this.txtpono.Text = ds.Tables[0].Rows[0]["PONO"].ToString().Trim();
                    this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim(), this.ddlpacksize.SelectedValue.Trim());
                }
                #endregion

                #region Details Information

                this.BindPurchaseOrderGridStructure();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtPOEdit = (DataTable)Session["PORECORDSV2"];
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEditPO = dtPOEdit.NewRow();
                        drEditPO["DIVISIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONID"]);
                        drEditPO["DIVISIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONNAME"]);
                        drEditPO["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEditPO["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEditPO["NATUREOFPRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTID"]);
                        drEditPO["NATUREOFPRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTNAME"]);
                        drEditPO["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEditPO["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEditPO["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                        drEditPO["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                        drEditPO["PRODUCTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTQTY"]);
                        drEditPO["PRODUCTCONVERSIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCONVERSIONQTY"]);
                        drEditPO["PRODUCTPRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPRICE"]);
                        drEditPO["PRODUCTAMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTAMOUNT"]);
                        drEditPO["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                        drEditPO["MRPVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["MRPVALUE"]);
                        drEditPO["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                        drEditPO["EXCISE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXCISE"]);
                        drEditPO["CST"] = Convert.ToString(ds.Tables[1].Rows[i]["CST"]);
                        drEditPO["REQUIREDDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDDATE"]);
                        drEditPO["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                        drEditPO["MODE"] = "V";
                        drEditPO["STOCKQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKQTY"]);
                        drEditPO["PRODUCTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTRATE"]);
                        drEditPO["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);

                        dtPOEdit.Rows.Add(drEditPO);
                        dtPOEdit.AcceptChanges();
                    }
                    #region grdAddDespatch DataBind
                    HttpContext.Current.Session["PORECORDSV2"] = dtPOEdit;
                    this.gvPurchaseOrder.DataSource = dtPOEdit;
                    this.gvPurchaseOrder.DataBind();
                    #endregion
                }
                else
                {
                    this.gvPurchaseOrder.DataSource = null;
                    this.gvPurchaseOrder.DataBind();
                }
                #endregion

                #region Footer Information
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.txtgrosstotal.Text = ds.Tables[2].Rows[0]["GROSSTOTAL"].ToString();
                    this.txtTotalMRP.Text = ds.Tables[2].Rows[0]["MRPTOTAL"].ToString();
                    this.txtadjustment.Text = ds.Tables[2].Rows[0]["ADJUSTMENT"].ToString();
                    this.txtexercise.Text = ds.Tables[2].Rows[0]["EXERCISE"].ToString();
                    this.txtsaletax.Text = ds.Tables[2].Rows[0]["CST"].ToString();
                    this.txttotalamount.Text = ds.Tables[2].Rows[0]["TOTALAMOUNT"].ToString();
                }
                #endregion

                #region BOM DETAILS
                CreateBomData();
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtRequestionEdit = new DataTable();
                    dtRequestionEdit = (DataTable)Session["BOMDATA"];

                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        DataRow drEditREQ = dtRequestionEdit.NewRow();
                        //drEditREQ["REQUISITIONID"] = "";
                        drEditREQ["CATEGORYID"] = Convert.ToString(ds.Tables[3].Rows[i]["CATEGORYID"]);
                        drEditREQ["CATEGORYNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["CATEGORYNAME"]);
                        drEditREQ["PRODUCTID"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTID"]);
                        drEditREQ["PRODUCTNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTNAME"]);
                        drEditREQ["UOMID"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMID"]);
                        drEditREQ["UOMNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMNAME"]);
                        drEditREQ["QTY"] = Convert.ToString(ds.Tables[3].Rows[i]["QTY"]);
                        drEditREQ["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[3].Rows[i]["REQUIREDFROMDATE"]);
                        drEditREQ["REQUIREDTODATE"] = Convert.ToString(ds.Tables[3].Rows[i]["REQUIREDTODATE"]);
                        drEditREQ["WORKORDERPRODUCTID"] = Convert.ToString(ds.Tables[3].Rows[i]["WORKORDERPRODUCTID"]);
                        drEditREQ["STOCKQTY"] = Convert.ToString(ds.Tables[3].Rows[i]["STOCKQTY"]);
                        drEditREQ["STORELOCATIONID"] = Convert.ToString(ds.Tables[3].Rows[i]["STORELOCATIONID"]);
                        drEditREQ["STORELOCATIONAME"] = Convert.ToString(ds.Tables[3].Rows[i]["STORELOCATIONAME"]);

                        dtRequestionEdit.Rows.Add(drEditREQ);
                        dtRequestionEdit.AcceptChanges();
                    }
                    HttpContext.Current.Session["BOMDATA"] = dtRequestionEdit;
                    GvBomDtls.DataSource = dtRequestionEdit;
                    GvBomDtls.DataBind();
                }
                else
                {
                    GvBomDtls.DataSource = null;
                    GvBomDtls.DataBind();
                }
                #endregion
            }
            this.trscheduledate.Style["display"] = "";
            this.ddlTPUName.Enabled = false;
            this.txtpodate.Enabled = false;
            //this.div_btnPrint.Style["display"] = "";
            this.divProductDetails.Style["display"] = "";
            this.divpono.Style["display"] = "";
            this.divponoheader.Style["display"] = "";
            this.btnsave.Enabled = true;
            this.ddlTPUName.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.btnsave.Visible = true;
            this.btncancel.Visible = true;
            this.divbtnsave.Style["display"] = "";
            this.tblADD.Style["display"] = "";
            this.ddlorderfor.Enabled = true;
            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            this.imgPopuppodate.Visible = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region View Purchase Order
    protected void btngrdview_Click(object sender, EventArgs e)
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            DataSet ds = new DataSet();
            string POID = hdn_pofield.Value.ToString().Trim();
            ds = clspurchaseorder.BindPurchaseOrderDetails(POID, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserID"].ToString());

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.LoadOrderType();
                this.LoadTPUName();
                this.ddlTPUName.SelectedValue = ds.Tables[0].Rows[0]["TPUID"].ToString().Trim();
                this.ddlorderfor.SelectedValue = ds.Tables[0].Rows[0]["ORDERTYPEID"].ToString().Trim();
                this.txtpodate.Text = ds.Tables[0].Rows[0]["PODATE"].ToString().Trim();
                this.txtcasepack.Text = ds.Tables[0].Rows[0]["TOTALCASEPACK"].ToString().Trim();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString().Trim();
                this.txtpono.Text = ds.Tables[0].Rows[0]["PONO"].ToString().Trim();
            }
            #endregion

            #region Details Information

            this.BindPurchaseOrderGridStructure();
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtPOEdit = (DataTable)Session["PORECORDSV2"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow drEditPO = dtPOEdit.NewRow();
                    drEditPO["DIVISIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONID"]);
                    drEditPO["DIVISIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONNAME"]);
                    drEditPO["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                    drEditPO["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                    drEditPO["NATUREOFPRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTID"]);
                    drEditPO["NATUREOFPRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTNAME"]);
                    drEditPO["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                    drEditPO["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                    drEditPO["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    drEditPO["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    drEditPO["PRODUCTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTQTY"]);
                    //drEditPO["PRODUCTPACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZEID"]);
                    //drEditPO["PRODUCTPACKINGSIZE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZE"]);
                    drEditPO["PRODUCTCONVERSIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCONVERSIONQTY"]);
                    drEditPO["PRODUCTPRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPRICE"]);
                    drEditPO["PRODUCTAMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTAMOUNT"]);
                    drEditPO["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    drEditPO["MRPVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["MRPVALUE"]);
                    drEditPO["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    drEditPO["EXCISE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXCISE"]);
                    drEditPO["CST"] = Convert.ToString(ds.Tables[1].Rows[i]["CST"]);
                    drEditPO["REQUIREDDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDDATE"]);
                    drEditPO["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                    drEditPO["MODE"] = "V";
                    drEditPO["STOCKQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKQTY"]);
                    drEditPO["PRODUCTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTRATE"]);
                    drEditPO["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);

                    dtPOEdit.Rows.Add(drEditPO);
                    dtPOEdit.AcceptChanges();
                }
                #region grdAddDespatch DataBind
                HttpContext.Current.Session["PORECORDSV2"] = dtPOEdit;
                this.gvPurchaseOrder.DataSource = dtPOEdit;
                this.gvPurchaseOrder.DataBind();
                #endregion
            }
            else
            {
                this.gvPurchaseOrder.DataSource = null;
                this.gvPurchaseOrder.DataBind();
            }
            #endregion

            #region Footer Information
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.txtgrosstotal.Text = ds.Tables[2].Rows[0]["GROSSTOTAL"].ToString();
                this.txtTotalMRP.Text = ds.Tables[2].Rows[0]["MRPTOTAL"].ToString();
                this.txtadjustment.Text = ds.Tables[2].Rows[0]["ADJUSTMENT"].ToString();
                this.txtexercise.Text = ds.Tables[2].Rows[0]["EXERCISE"].ToString();
                this.txtsaletax.Text = ds.Tables[2].Rows[0]["CST"].ToString();
                this.txttotalamount.Text = ds.Tables[2].Rows[0]["TOTALAMOUNT"].ToString();

            }
            #endregion

            #region BOM DETAILS
            CreateBomData();
            if (ds.Tables[3].Rows.Count > 0)
            {
                DataTable dtRequestionEdit = new DataTable();
                dtRequestionEdit = (DataTable)Session["BOMDATA"];
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    DataRow drEditREQ = dtRequestionEdit.NewRow();
                    //drEditREQ["REQUISITIONID"] = "";
                    drEditREQ["CATEGORYID"] = Convert.ToString(ds.Tables[3].Rows[i]["CATEGORYID"]);
                    drEditREQ["CATEGORYNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["CATEGORYNAME"]);
                    drEditREQ["PRODUCTID"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTID"]);
                    drEditREQ["PRODUCTNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTNAME"]);
                    drEditREQ["UOMID"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMID"]);
                    drEditREQ["UOMNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMNAME"]);
                    drEditREQ["QTY"] = Convert.ToString(ds.Tables[3].Rows[i]["QTY"]);
                    drEditREQ["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[3].Rows[i]["REQUIREDFROMDATE"]);
                    drEditREQ["REQUIREDTODATE"] = Convert.ToString(ds.Tables[3].Rows[i]["REQUIREDTODATE"]);
                    drEditREQ["WORKORDERPRODUCTID"] = Convert.ToString(ds.Tables[3].Rows[i]["WORKORDERPRODUCTID"]);
                    drEditREQ["STOCKQTY"] = Convert.ToString(ds.Tables[3].Rows[i]["STOCKQTY"]);
                    drEditREQ["STORELOCATIONID"] = Convert.ToString(ds.Tables[3].Rows[i]["STORELOCATIONID"]);
                    drEditREQ["STORELOCATIONAME"] = Convert.ToString(ds.Tables[3].Rows[i]["STORELOCATIONAME"]);

                    dtRequestionEdit.Rows.Add(drEditREQ);
                    dtRequestionEdit.AcceptChanges();
                }
                HttpContext.Current.Session["BOMDATA"] = dtRequestionEdit;
                GvBomDtls.DataSource = dtRequestionEdit;
                GvBomDtls.DataBind();
            }
            else
            {
                GvBomDtls.DataSource = null;
                GvBomDtls.DataBind();
            }
            #endregion

            this.trscheduledate.Style["display"] = "none";
            this.divbtnsave.Style["display"] = "none";
            //this.div_btnPrint.Style["display"] = "";
            this.divProductDetails.Style["display"] = "none";
            this.divpono.Style["display"] = "";
            this.divponoheader.Style["display"] = "";
            this.ddlTPUName.Enabled = false;
            this.txtpodate.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.tblADD.Style["display"] = "none";
            this.btnsave.Visible = true;
            this.btncancel.Visible = true;
            this.imgPopuppodate.Visible = false;
            this.ddlorderfor.Enabled = false;
            this.txtremarks.Enabled = false;
            this.gvPurchaseOrder.Columns[22].Visible = false;

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btn_TempDelete_Click
    protected void btn_TempDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblproductid1 = gvr.Cells[1].Controls[0].FindControl("grdlblproductid1") as Label;
            Label grdlblFromDate1 = gvr.Cells[12].Controls[0].FindControl("grdlblFromDate1") as Label;
            Label grdlblToDate1 = gvr.Cells[13].Controls[0].FindControl("grdlblToDate1") as Label;

            this.hdn_podelete.Value = grdlblproductid1.Text.Trim();
            this.hdn_FromDate.Value = grdlblFromDate1.Text.Trim();
            this.hdn_ToDate.Value = grdlblToDate1.Text.Trim();

            string productid = hdn_podelete.Value.ToString().Trim();
            string FromDate = hdn_FromDate.Value.Trim();
            string ToDate = hdn_ToDate.Value.Trim();

            decimal GrossTotal = 0;
            decimal NetTotal = 0;
            decimal TotalMRP = 0;
            decimal TotalExcise = 0;
            decimal TotalCST = 0;
            DataTable dtdeletePurchaseOrderDetails = new DataTable();
            dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];
            DataTable dtdeleteBOMDetails = new DataTable();
            dtdeleteBOMDetails = (DataTable)Session["BOMDATA"];

            DataRow[] drr = dtdeletePurchaseOrderDetails.Select("PRODUCTID='" + productid + "' AND REQUIREDDATE = '" + FromDate + "' AND REQUIREDTODATE = '" + ToDate + "'");

            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeletePurchaseOrderDetails.AcceptChanges();
            }
            HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
            DataRow[] drBOM = dtdeleteBOMDetails.Select("WORKORDERPRODUCTID='" + productid + "'");
            for (int i = 0; i < drBOM.Length; i++)
            {
                drBOM[i].Delete();
                dtdeleteBOMDetails.AcceptChanges();
            }
            HttpContext.Current.Session["BOMDATA"] = dtdeleteBOMDetails;

            #region Amount Calculation
            if (dtdeletePurchaseOrderDetails.Rows.Count > 0)
            {
                GrossTotal = CalculateTotalAmount(dtdeletePurchaseOrderDetails);
                NetTotal = GrossTotal;
                TotalMRP = CalculateTotalMRP(dtdeletePurchaseOrderDetails);
                TotalExcise = CalculateTotalExcise(dtdeletePurchaseOrderDetails);
                TotalCST = ((GrossTotal + TotalExcise) * (Convert.ToDecimal(dtdeletePurchaseOrderDetails.Rows[0]["CST"]) / 100));

                this.txtgrosstotal.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.txtTotalMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtexercise.Text = Convert.ToString(String.Format("{0:0.00}", TotalExcise));
                this.txtsaletax.Text = Convert.ToString(String.Format("{0:0.00}", TotalCST));
                this.txttotalamount.Text = Convert.ToString(String.Format("{0:0.00}", NetTotal));

                #region Total Case
                this.txtcasepack.Text = Convert.ToString(TotalCasePack(hdn_pofield.Value.ToString()));
                #endregion
            }
            else
            {
                this.txtgrosstotal.Text = "0";
                this.txtTotalMRP.Text = "0";
                this.txtexercise.Text = "0";
                this.txtsaletax.Text = "0";
                this.txttotalamount.Text = "0";
                this.txtcasepack.Text = "0";
            }

            #endregion

            if (dtdeletePurchaseOrderDetails.Rows.Count > 0)
            {
                gvPurchaseOrder.DataSource = dtdeletePurchaseOrderDetails;
                gvPurchaseOrder.DataBind();
                GvBomDtls.DataSource = dtdeleteBOMDetails;
                GvBomDtls.DataBind();
            }
            else
            {
                gvPurchaseOrder.DataSource = null;
                gvPurchaseOrder.DataBind();
                GvBomDtls.DataSource = null;
                GvBomDtls.DataBind();
                Session["BOMDATA"] = null;
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

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string delivaryFromdate = string.Empty;
            string delivaryTodate = string.Empty;
            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            string PONo = string.Empty;
            string xml = string.Empty;
            string xmlrequestion = string.Empty;
            DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["PORECORDSV2"];
            DataTable dtRequestion = (DataTable)HttpContext.Current.Session["BOMDATA"];
            delivaryFromdate = this.txtdelfromdate.Text;
            delivaryTodate = this.txtdeltodate.Text;
            if (delivaryFromdate == "" || delivaryTodate == "")
            {
                MessageBox1.ShowWarning("Delivary date Cannot be blank!!");
                return;
            }
            if (dtRecordsCheck.Rows.Count > 0)
            {
                if (Session["PORECORDSV2"] != null)
                {
                    xml = ConvertDatatableToXML(dtRecordsCheck);
                }
                if (Session["BOMDATA"] != null)
                {
                    xmlrequestion = ConvertDatatableToXML(dtRequestion);
                }
                foreach (GridViewRow row in GvBomDtls.Rows)
                {
                    Label lblstorelocationid = (Label)row.FindControl("gvlblSTORELOCATIONID");
                    Label lblproductname = (Label)row.FindControl("gvlblproductname");

                    if (lblstorelocationid.Text == "")
                    {
                        MessageBox1.ShowInfo("Please map storelocation for this product:- <font color='red'><b>" + lblproductname.Text + "</font><b>", 60, 550);
                        return;
                    }                    
                }
                if (gvPurchaseOrder.Rows.Count > 0)
                {
                    PONo = clspurchaseorder.InsertJobOrderDetails(this.hdn_pofield.Value.ToString().Trim(), this.txtpodate.Text.Trim().Trim(),
                                                                  this.ddlTPUName.SelectedValue.ToString().Trim(), this.ddlTPUName.SelectedItem.ToString().Trim(),
                                                                  this.txtremarks.Text.Trim(), Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString().Trim()),
                                                                  Convert.ToDecimal(this.txtgrosstotal.Text.Trim()), Convert.ToDecimal(this.txtexercise.Text.Trim()),
                                                                  Convert.ToDecimal(this.txtsaletax.Text.Trim()), Convert.ToDecimal(this.txttotalamount.Text.Trim()),
                                                                  HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Convert.ToDecimal(this.txtTotalMRP.Text.Trim()),
                                                                  Convert.ToDecimal(this.txtcasepack.Text.Trim()), this.ddlorderfor.SelectedValue.ToString().Trim(),
                                                                  this.ddlorderfor.SelectedItem.ToString().Trim(), xml, xmlrequestion,
                                                                  this.ddlfactory.SelectedValue.ToString().Trim(), delivaryFromdate, delivaryTodate);
                    if (PONo != "")
                    {
                        if (Convert.ToString(hdn_pofield.Value) == "")
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> saved Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            //this.div_btnPrint.Style["display"] = "";
                            this.tblADD.Style["display"] = "none";
                            this.btnsave.Enabled = false;
                            trscheduledate.Style["display"] = "none";
                            Session["BOMDATA"] = null;

                            divpono.Style["display"] = "none";
                            divponoheader.Style["display"] = "none";
                            InputTable.Style["display"] = "none";
                            pnlDisplay.Style["display"] = "";
                            divadd.Style["display"] = "";
                            gvpodetails.DataSource = null;
                            gvpodetails.DataBind();

                        }
                        else
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> updated Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            this.btnsave.Enabled = false;
                            trscheduledate.Style["display"] = "none";
                            this.tblADD.Style["display"] = "none";

                            divpono.Style["display"] = "none";
                            divponoheader.Style["display"] = "none";
                            InputTable.Style["display"] = "none";
                            pnlDisplay.Style["display"] = "";
                            divadd.Style["display"] = "";
                            Session["BOMDATA"] = null;
                            gvpodetails.DataSource = null;
                            gvpodetails.DataBind();

                        }
                        LoadPO();
                        this.grdpodetailsadd.DataSource = null;
                        this.grdpodetailsadd.DataBind();
                        //this.gvpodetails.DataSource = null;
                        //this.gvpodetails.DataBind();
                        this.divProductDetails.Style["display"] = "none";
                    }
                    else
                    {
                        this.divProductDetails.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please enter Purchase Order details.", 60, 450);
                    return;
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

    #region Clear Control
    public void clear()
    {
        try
        {
            this.ddlTPUName.SelectedValue = "0";
            this.ddlTPUName.Enabled = true;

            this.txtremarks.Text = "";
            this.txtgrosstotal.Text = "0";
            this.txtadjustment.Text = "0";
            this.txtTotalMRP.Text = "0";
            this.txtpacking.Text = "0";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtnettotal.Text = "0";
            this.btnnewentry.Visible = true;

            ClsWOOrder clspurchaseorder = new ClsWOOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
            LoadPO();
            btnsave.Enabled = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region BindProductDetails
    protected void BindProductDetails(string TPUID, string PacksizeID)
    {
        ClsWOOrder clspurchaseorder = new ClsWOOrder();
        DataTable dt = clspurchaseorder.FetchProductdetails(TPUID, PacksizeID, HttpContext.Current.Session["DEPOTID"].ToString());
        if (dt.Rows.Count > 0)
        {
            this.grdpodetailsadd.DataSource = dt;
            this.grdpodetailsadd.DataBind();
        }
        else
        {
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
        }
    }
    #endregion

    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {
            LoadPO();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = "frmRptPurchaseOrder.aspx?pid=" + hdn_pofield.Value;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public DataTable CreateBomData()
    {
        DataTable dtBOMRecord = new DataTable();
        dtBOMRecord.Clear();
        dtBOMRecord.Columns.Add(new DataColumn("REQUISITIONID", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("QTY", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("WORKORDERPRODUCTID", typeof(string)));
        //dtBOMRecord.Columns.Add(new DataColumn("REFQTY", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("STOCKQTY", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dtBOMRecord.Columns.Add(new DataColumn("STORELOCATIONAME", typeof(string)));
        HttpContext.Current.Session["BOMDATA"] = dtBOMRecord;
        return dtBOMRecord;
    }
    
    #region LoadDepotName
    public void BindFactoryName()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = ClsCommon.BindDepot_Primary();
                ddlfactory.Items.Clear();
                ddlfactory.Items.Add(new ListItem("Select", "0"));
                ddlfactory.AppendDataBoundItems = true;
                ddlfactory.DataSource = dt;
                ddlfactory.DataTextField = "BRNAME";
                ddlfactory.DataValueField = "BRID";
                ddlfactory.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = ClsCommon.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddlfactory.Items.Clear();
                ddlfactory.Items.Add(new ListItem("Select", "0"));
                ddlfactory.AppendDataBoundItems = true;
                ddlfactory.DataSource = dt;
                ddlfactory.DataTextField = "BRNAME";
                ddlfactory.DataValueField = "BRID";
                ddlfactory.DataBind();
            }
            else
            {
                DataTable dt = ClsCommon.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                //DataTable dt = ClsCommon.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddlfactory.Items.Clear();
                //ddlfactory.Items.Add(new ListItem("Select", "0"));
                ddlfactory.AppendDataBoundItems = true;
                ddlfactory.DataSource = dt;
                ddlfactory.DataTextField = "BRNAME";
                ddlfactory.DataValueField = "BRID";
                ddlfactory.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        CalendarExtender1.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;
        CalendarExtender4.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtpodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtdelfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender1.EndDate = today1;
            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;
            CalendarExtender4.EndDate = today1;
        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtpodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtdelfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender1.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
            CalendarExtender4.EndDate = cDate;
        }
    }
    #endregion
}