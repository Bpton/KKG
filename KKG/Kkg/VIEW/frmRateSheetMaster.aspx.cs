using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRateSheetMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlCategory').multiselect({ includeSelectAllOption: true  });});</script>", false);

        if (!IsPostBack)
        {

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            LoadSearchVendor();
            //LoadRatesheetMaster();
            LoadSuppliedItem();
            ddlVendor.Enabled = true;
            ddlDivision.Enabled = true;
            ddlCategory.Enabled = true;
            hdnVendor.Visible = false;
            hdnFactory.Visible = false;
            hdnVendor2.Visible = false;
            hdnFactory2.Visible = false;
            hdnVendor1.Visible = false;
            hdnFactory1.Visible = false;
            hdnTrRdb.Visible = true;
            TrName.Visible = false;

            gvRSMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            gvRatesheet.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;

        }
    }
    public void LoadRatesheetMaster()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            string VendorID = ddlSearchVendor.SelectedValue;
            string FactoryId = ddlFactory.SelectedValue;
            gvRatesheet.DataSource = clsrs.BindRateSheetGrid(VendorID);
            gvRatesheet.DataBind();
            // gvRatesheet.Columns[3].HeaderText = @"CONVERSION COST(Rs.)";
            // gvRatesheet.Columns[2].HeaderText = @"RM/PM COST(Rs.)";           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadRatesheetMasterFactory()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            string VendorID = ddlSearchVendor.SelectedValue;
            string FactoryId = ddlsearchFactory.SelectedValue;
            gvRatesheet.DataSource = clsrs.BindRateSheetGrid1(FactoryId);
            gvRatesheet.DataBind();

            if (rdbsearchTPU.Checked)
            {
                VendorID = ddlSearchVendor.SelectedValue;
                ddlVendor.SelectedValue = VendorID;
                gvRatesheet.Columns[4].Visible = false;
                //gvRatesheet.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";

            }
            else
            {
                VendorID = ddlsearchFactory.SelectedValue;
                ddlFactory.SelectedValue = VendorID;
                gvRatesheet.Columns[2].Visible = false;
                //gvRatesheet.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void Bindgrid()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            gvRSMap.DataSource = clsrs.BindRSGrid(ddlDivision.SelectedValue, ddlCategory.SelectedValue);
            gvRSMap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Load search Vendor
    public void LoadSearchVendor()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();

            ddlSearchVendor.Items.Clear();
            ddlSearchVendor.Items.Insert(0, new ListItem("-All-", "0"));

            ddlSearchVendor.DataSource = clsrs.BindVendor();
            ddlSearchVendor.DataTextField = "VENDORNAME";
            ddlSearchVendor.DataValueField = "VENDORID";
            ddlSearchVendor.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Supplied Item
    public void LoadSuppliedItem()
    {
        try
        {
            ClsRateSheetMaster clsratesheet = new ClsRateSheetMaster();
            DataTable dt = new DataTable();
            dt = clsratesheet.BindSuppliedItem();
            if (dt.Rows.Count > 0)
            {
                ddlsupplieditem.Items.Clear();
                ddlsupplieditem.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlsupplieditem.AppendDataBoundItems = true;
                ddlsupplieditem.DataSource = dt;
                ddlsupplieditem.DataTextField = "ITEMDESC";
                ddlsupplieditem.DataValueField = "ID";
                ddlsupplieditem.DataBind();
            }
            else
            {
                ddlsupplieditem.Items.Clear();
                ddlsupplieditem.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlsupplieditem.AppendDataBoundItems = true;
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Division
    public void LoadDivision()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlDivision.DataSource = ClsProduct.BindDivision();
            ddlDivision.DataTextField = "DIVNAME";
            ddlDivision.DataValueField = "DIVID";
            ddlDivision.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load catagory
    public void LoadCategory()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlCategory.Items.Clear();
            //ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlCategory.DataSource = ClsProduct.BindCategory();
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataValueField = "CATID";
            ddlCategory.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
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
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CATAGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATAGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("RMCOST", typeof(string)));
        dt.Columns.Add(new DataColumn("TRANSFERCOST", typeof(string)));
        dt.Columns.Add(new DataColumn("PCS", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dt.Columns.Add(new DataColumn("UNITNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TODATE", typeof(string)));
        HttpContext.Current.Session["PRODUCTDETAILS"] = dt;
    }
    #endregion

    #region TPU RateSheet Save
    protected void btnPBMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            int ID = 0;
            int count = 0;
            string xml = string.Empty;

            DataTable dtadd = new DataTable();

            if (HttpContext.Current.Session["PRODUCTDETAILS"] == null)
            {
                CreateDataTable();
            }

            dtadd = (DataTable)HttpContext.Current.Session["PRODUCTDETAILS"];

            for (int i = 0; i < gvRSMap.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cellproductid = gvRSMap.RowsInViewState[i].Cells[0] as GridDataControlFieldCell;
                CheckBox chk = cellproductid.FindControl("ChkID") as CheckBox;
                HiddenField hiddenFieldproductid = cellproductid.FindControl("hdnPRODUCTID") as HiddenField;

                GridDataControlFieldCell CellCatID = gvRSMap.RowsInViewState[i].Cells[0] as GridDataControlFieldCell;
                HiddenField HdnCatID = CellCatID.FindControl("HdnCatID") as HiddenField;

                GridDataControlFieldCell cell = gvRSMap.RowsInViewState[i].Cells[5] as GridDataControlFieldCell;
                GridDataControlFieldCell cell1 = gvRSMap.RowsInViewState[i].Cells[6] as GridDataControlFieldCell;
                GridDataControlFieldCell cell6 = gvRSMap.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                HiddenField hiddenField = cell.FindControl("hdnPS") as HiddenField;
                HiddenField HdnCatName = cell.FindControl("HdnCatName") as HiddenField;
                TextBox txtrm = cell.FindControl("txtRmcost") as TextBox;
                TextBox txttrns = cell1.FindControl("txttrnscost") as TextBox;
                TextBox txtpcs = cell6.FindControl("txtpcs") as TextBox;
                Hashtable UOMID = gvRSMap.Rows[i].ToHashtable();
                string uomid = UOMID["UOMID"].ToString().Trim();
                Hashtable UNITNAME = gvRSMap.Rows[i].ToHashtable();
                string unitname = UNITNAME["UNITNAME"].ToString().Trim();

                if (rdbTPU.Checked)//&& Convert.ToDecimal(txtrm.Text.ToString()) != 0
                {
                    if (chk.Checked == true)
                    {
                        DataRow dr = dtadd.NewRow();
                        dr["VENDORID"] = Convert.ToString(this.ddlVendor.SelectedValue.Trim());
                        dr["VENDORNAME"] = Convert.ToString(this.ddlVendor.SelectedItem.ToString().Trim());
                        dr["DIVISIONID"] = Convert.ToString(this.ddlDivision.SelectedValue.Trim());
                        dr["DIVISIONNAME"] = Convert.ToString(this.ddlDivision.SelectedItem).Trim();
                        dr["CATAGORYID"] = HdnCatID.Value;//Convert.ToString(this.ddlCategory.SelectedValue.Trim());
                        dr["CATAGORYNAME"] = HdnCatName.Value;//Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                        dr["PRODUCTID"] = hiddenFieldproductid.Value;
                        dr["PRODUCTNAME"] = hiddenField.Value;
                        dr["RMCOST"] = Convert.ToDecimal(txtrm.Text.ToString());
                        dr["TRANSFERCOST"] = Convert.ToDecimal(txttrns.Text.ToString());
                        dr["PCS"] = Convert.ToDecimal(txtpcs.Text.ToString().Trim());
                        dr["UOMID"] = uomid.Trim();
                        dr["UNITNAME"] = unitname.Trim();
                        dr["FROMDATE"] = Convert.ToString(this.txtFromDate.Text).Trim();
                        dr["TODATE"] = Convert.ToString(this.txtToDate.Text).Trim();

                        dtadd.Rows.Add(dr);
                        dtadd.AcceptChanges();
                        count = count + 1;
                    }
                }
                else if (rdbFactory.Checked == true)//&& Convert.ToDecimal(txtrm.Text.ToString()) != 0
                {
                    if (chk.Checked == true)
                    {
                        DataRow dr = dtadd.NewRow();
                        dr["VENDORID"] = Convert.ToString(this.ddlFactory.SelectedValue.Trim());
                        dr["VENDORNAME"] = Convert.ToString(this.ddlFactory.SelectedItem.ToString().Trim());
                        dr["DIVISIONID"] = Convert.ToString(this.ddlDivision.SelectedValue.Trim());
                        dr["DIVISIONNAME"] = Convert.ToString(this.ddlDivision.SelectedItem).Trim();
                        dr["CATAGORYID"] = HdnCatID.Value;//Convert.ToString(this.ddlCategory.SelectedValue.Trim());
                        dr["CATAGORYNAME"] = HdnCatName.Value;//Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                        dr["PRODUCTID"] = hiddenFieldproductid.Value;
                        dr["PRODUCTNAME"] = hiddenField.Value;
                        dr["RMCOST"] = txtrm.Text.ToString();
                        dr["TRANSFERCOST"] = txttrns.Text.ToString();
                        dr["PCS"] = txtpcs.Text.ToString().Trim();
                        dr["UOMID"] = uomid.Trim();
                        dr["UNITNAME"] = unitname.Trim();
                        dr["FROMDATE"] = Convert.ToString(this.txtFromDate.Text).Trim();
                        dr["TODATE"] = Convert.ToString(this.txtToDate.Text).Trim();
                        dtadd.Rows.Add(dr);
                        dtadd.AcceptChanges();
                        count = count + 1;
                    }
                }

            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Please Select 1 record !");

                HttpContext.Current.Session["PRODUCTDETAILS"] = null;
                count = 0;
            }
            else
            {

                HttpContext.Current.Session["PRODUCTDETAILS"] = dtadd;

                xml = ConvertDatatable(dtadd);
                if (this.ddlVendor.SelectedValue != "0")
                {
                    ID = clsrs.SaveTpuRateSheet(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), Convert.ToString(this.ddlVendor.SelectedValue.Trim()), xml);
                }
                else
                {
                    ID = clsrs.SaveTpuRateSheet(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), Convert.ToString(this.ddlFactory.SelectedValue.Trim()), xml);
                }


                if (ID > 0)
                {

                    MessageBox1.ShowSuccess("Record Saved Successfully!");

                    LoadRatesheetMaster();
                    Hdn_Fld.Value = "";
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";

                    gvRSMap.DataSource = null;
                    gvRSMap.DataBind();

                    ddlVendor.Enabled = true;
                    ddlDivision.Enabled = true;
                    ddlCategory.Enabled = true;
                    HttpContext.Current.Session["PRODUCTDETAILS"] = null;
                    count = 0;
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record !");
                    HttpContext.Current.Session["PRODUCTDETAILS"] = null;
                    count = 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Delete
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = clsrs.RatesheetDelete(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
                LoadRatesheetMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Add new record
    protected void btnAddRateSheet_Click(object sender, EventArgs e)
    {
        pnlAdd.Style["display"] = "";
        pnlDisplay.Style["display"] = "none";
        btnaddhide.Style["display"] = "none";
        LoadRatesheetMaster();
        ddlVendor.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
        //ddlCategory.SelectedValue = "0";
        ddlCategory.Enabled = true;
        ddlDivision.Enabled = true;
        ddlVendor.Enabled = true;
        gvRSMap.DataSource = null;
        hdnVendor.Visible = true;
        hdnVendor2.Visible = true;
        hdnFactory.Visible = false;
        hdnFactory2.Visible = false;
        hdnTrRdb.Visible = true;
        TrName.Visible = false;
        gvRSMap.DataBind();
        rdbFactory.Enabled = true;
        rdbTPU.Enabled = true;
        //rdbTPU.Checked = true;

        gvRSMap.Columns[3].Visible = false;
        ViewState["Mode"] = "A";
        this.trSuplied.Style["display"] = "";
        this.trSuplied2.Style["display"] = "";
        this.hdnVendor.Style["display"] = "";
        this.hdnVendor2.Style["display"] = "";
        //this.LoadSuppliedItem();
        HttpContext.Current.Session["PRODUCTDETAILS"] = null;

        DateTime dtcurr = DateTime.Now;

        this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");



    }
    #endregion

    #region Cancel
    protected void btnPBMapCancel_Click(object sender, EventArgs e)
    {
        try
        {

            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            this.trSuplied.Style["display"] = "";
            this.hdnVendor.Style["display"] = "";
            this.trSuplied2.Style["display"] = "";
            this.hdnVendor2.Style["display"] = "";
            rdbFactory.Enabled = true;
            rdbTPU.Enabled = true;
            ddlVendor.Enabled = true;
            ddlDivision.Enabled = true;
            ddlCategory.Enabled = true;
            ddlCategory.Items.Clear();

            rdbFactory.Checked = false;
            rdbTPU.Checked = false;
            //ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
            //this.ddlCategory.AppendDataBoundItems = true;
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
            this.ddlDivision.AppendDataBoundItems = true;
            ddlsupplieditem.Items.Clear();
            ddlsupplieditem.Items.Insert(0, new ListItem("--Select Supplied Item--", "0"));
            this.ddlsupplieditem.AppendDataBoundItems = true;
            HttpContext.Current.Session["PRODUCTDETAILS"] = null;


        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        PoPulateGrid(ddlVendor.SelectedValue, ddlDivision.SelectedValue, ddlCategory.SelectedValue);
    }

    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            string pid = hdn_pid.Value.ToString();
            DataTable dtratesheet = new DataTable();
            DataTable dt = new DataTable();
            DataTable dtSuppliedItem = new DataTable();
            TrName.Visible = true;
            hdnTrRdb.Visible = false;
            ViewState["Mode"] = "E";


            if (rdbsearchTPU.Checked)
            {

                ViewState["VendorID"] = ddlSearchVendor.SelectedValue;
                ViewState["VendorName"] = ddlSearchVendor.SelectedItem.ToString().Trim();
                gvRSMap.Columns[4].Visible = true;
                gvRSMap.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";

            }
            else
            {

                ViewState["VendorID"] = ddlsearchFactory.SelectedValue;
                ViewState["VendorName"] = ddlSearchVendor.SelectedItem.ToString().Trim();
                gvRSMap.Columns[4].Visible = false;
                gvRSMap.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";
            }


            dtratesheet = clsrs.FetchProductDetails(pid, ViewState["VendorID"].ToString());
            dt = clsrs.BindVendorName(ViewState["VendorID"].ToString());

            this.LoadSuppliedItem();
            dtSuppliedItem = clsrs.FetchSuppliedItem(ViewState["VendorID"].ToString());
            if (dtSuppliedItem.Rows.Count > 0)
            {
                string suppliedItemID = Convert.ToString(dtSuppliedItem.Rows[0]["SUPLIEDITEMID"]);
                this.ddlsupplieditem.SelectedValue = Convert.ToString(dtSuppliedItem.Rows[0]["SUPLIEDITEMID"]);
                if (suppliedItemID.Trim() == "1")
                {
                    this.LoadDivision();
                    this.LoadCategory();
                    ddlDivision.SelectedValue = dtratesheet.Rows[0]["DIVISIONID"].ToString();
                    ddlCategory.SelectedValue = dtratesheet.Rows[0]["CATAGORYID"].ToString();
                }
                else
                {
                    this.LoadPrimaryItem();
                    ddlDivision.SelectedValue = dtratesheet.Rows[0]["DIVISIONID"].ToString();
                    this.LoadSubItem();
                    ddlCategory.SelectedValue = dtratesheet.Rows[0]["CATAGORYID"].ToString();
                }
            }


            lblName.Text = dt.Rows[0]["VENDORNAME"].ToString();

            gvRSMap.DataSource = dtratesheet;
            gvRSMap.DataBind();
            ddlVendor.Enabled = false;
            ddlDivision.Enabled = false;
            ddlCategory.Enabled = false;

            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            this.trSuplied.Style["display"] = "none";
            this.hdnVendor.Style["display"] = "none";
            this.trSuplied2.Style["display"] = "none";
            this.hdnVendor2.Style["display"] = "none";


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }

    protected void ddlSearchVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRatesheetMaster();
    }

    private void PoPulateGrid(string VendorID, string DIVISIONID, string CATAGORYID)
    {

        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();

            DataTable dtratesheet = new DataTable();
            dtratesheet = clsrs.FetchProductDetails(DIVISIONID, CATAGORYID, VendorID);

            gvRSMap.DataSource = dtratesheet;
            gvRSMap.DataBind();
            if (rdbFactory.Checked)
            {
                gvRSMap.Columns[5].Visible = false;
                // gvRSMap.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";
            }
            else
            {
                //gvRSMap.Columns[5].Visible = true;
                // gvRSMap.Columns[6].HeaderText = @"CONVERSION COST(Rs.)";
            }
            ddlVendor.Enabled = true;
            ddlDivision.Enabled = true;
            ddlCategory.Enabled = true;

            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
         BindGrid();     
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlsupplieditem.SelectedValue != "0")
        {
            if (this.ddlsupplieditem.SelectedValue == "1")
            {
                this.LoadCategory();
            }
            else
            {
                this.LoadSubItem();
            }
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            string Category = "";
            var querycat = from ListItem item in ddlCategory.Items where item.Selected select item;
            foreach (ListItem item in querycat)
            {
                Category += item.Value + ',';
            }
            Category = Category.Substring(0, Category.Length - 1);

            if (rdbTPU.Checked == false && rdbFactory.Checked == false)
            {
                gvRSMap.DataSource = null;
                gvRSMap.DataBind();
                MessageBox1.ShowInfo("Please Select Sales Location Type ");
                return;
            }
            else
            {
                if (rdbTPU.Checked)
                {
                    PoPulateGrid(ddlVendor.SelectedValue, ddlDivision.SelectedValue, Category);
                }
                else
                {
                    PoPulateGrid(ddlFactory.SelectedValue, ddlDivision.SelectedValue, Category);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadVendor()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            if (ddlsupplieditem.SelectedValue != "0")
            {
                ddlVendor.Items.Clear();
                ddlVendor.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlVendor.DataSource = clsrs.BindVendorByItem(ddlsupplieditem.SelectedValue.Trim());
                ddlVendor.DataTextField = "VENDORNAME";
                ddlVendor.DataValueField = "VENDORID";
                ddlVendor.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("Please select supplied Item");
            }

            ddlSearchVendor.Items.Clear();
            ddlSearchVendor.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlSearchVendor.DataSource = clsrs.BindVendor();
            ddlSearchVendor.DataTextField = "VENDORNAME";
            ddlSearchVendor.DataValueField = "VENDORID";
            ddlSearchVendor.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rdbTPU_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbTPU.Checked)
            {
                //LoadVendor();
                LoadSuppliedItem();
                ddlCategory.Items.Clear();
                //ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                //this.ddlCategory.AppendDataBoundItems = true;
                ddlDivision.Items.Clear();
                ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlDivision.AppendDataBoundItems = true;
                hdnVendor.Visible = true;
                hdnFactory.Visible = false;
                hdnVendor2.Visible = true;
                hdnFactory2.Visible = false;
                gvRSMap.Columns[5].Visible = true;
                gvRSMap.Columns[6].HeaderText = @"CONVERSION COST(Rs.)";
                gvRSMap.DataSource = null;
                gvRSMap.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void rdbFactory_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbFactory.Checked)
            {
                LoadSuppliedItem();
                ddlCategory.Items.Clear();
                //ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                //this.ddlCategory.AppendDataBoundItems = true;
                ddlDivision.Items.Clear();
                ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlDivision.AppendDataBoundItems = true;
                hdnVendor.Visible = false;
                hdnFactory.Visible = true;
                hdnVendor2.Visible = false;
                hdnFactory2.Visible = true;
                LoadFactoryName();
                //gvRSMap.Columns[4].Visible = false;
                //gvRSMap.Columns[5].Visible = true;
                //gvRSMap.Columns[4].HeaderText = @"RM-PM COST(Rs.)";
                //gvRSMap.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";
                //gvRSMap.Columns[2].Visible = false;
                //gvRSMap.Columns[3].Visible = false;
                gvRSMap.DataSource = null;
                gvRSMap.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void rdbsearchTPU_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbsearchTPU.Checked)
            {
                hdnVendor1.Visible = true;
                hdnFactory1.Visible = false;
                LoadSearchVendor();
                LoadRatesheetMaster();
                gvRatesheet.DataBind();
                //gvRatesheet.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
               // gvRatesheet.DataSource = null;
               // gvRatesheet.DataBind();


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region  searching TPU anf FACTORY Event

    protected void ddlsearchFactory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRatesheetMasterFactory();
    }

    protected void rdbsearchFactory_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbsearchFactory.Checked)
            {
                hdnVendor1.Visible = false;
                hdnFactory1.Visible = true;
                LoadFactoryNamesearch();
                LoadFactoryName();
                LoadRatesheetMasterFactory();
                //gvRatesheet.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                //gvRatesheet.DataSource = null;
                gvRatesheet.DataBind();
                //gvRatesheet.Columns[5].HeaderText = @"CONVERTION COST(Rs.)";
                ////gvRatesheet.Columns[4].HeaderText = @"CONVERTION COST(Rs.)";
                gvRatesheet.Columns[4].Visible = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    public void LoadFactoryName()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            ddlFactory.Items.Clear();
            ddlFactory.Items.Add(new ListItem("-- All --", "0"));
            ddlFactory.AppendDataBoundItems = true;
            ddlFactory.DataSource = clsrs.BindFactory();
            ddlFactory.DataTextField = "VENDORNAME";
            ddlFactory.DataValueField = "VENDORID";
            ddlFactory.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadFactoryNamesearch()
    {
        try
        {
            ClsRsMaster clsrs = new ClsRsMaster();
            ddlsearchFactory.Items.Clear();
            ddlsearchFactory.Items.Add(new ListItem("-- All --", "0"));
            ddlsearchFactory.AppendDataBoundItems = true;
            ddlsearchFactory.DataSource = clsrs.BindFactory();
            ddlsearchFactory.DataTextField = "VENDORNAME";
            ddlsearchFactory.DataValueField = "VENDORID";
            ddlsearchFactory.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadPrimaryItem()
    {
        try
        {
            if (ddlsupplieditem.SelectedValue != "0")
            {


                ClsRateSheetMaster ClsRS = new ClsRateSheetMaster();
                DataTable dtPrimary = new DataTable();
                dtPrimary = ClsRS.BindPrimaryitemtype();
                if (dtPrimary.Rows.Count > 0)
                {
                    ddlDivision.Items.Clear();
                    ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                    this.ddlDivision.AppendDataBoundItems = true;
                    ddlDivision.DataSource = dtPrimary;
                    ddlDivision.DataTextField = "ITEMDESC";
                    ddlDivision.DataValueField = "ID";
                    ddlDivision.DataBind();
                    this.ddlDivision.SelectedValue = this.ddlsupplieditem.SelectedValue.Trim();
                    if (this.ddlDivision.SelectedValue.Trim() == this.ddlsupplieditem.SelectedValue.Trim())
                    {
                        this.ddlDivision.Enabled = false;
                        this.LoadSubItem();
                    }
                    else
                    {
                        this.ddlDivision.Enabled = true;
                    }

                }
                else
                {
                    ddlDivision.Items.Clear();
                    ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                    this.ddlDivision.AppendDataBoundItems = true;
                }
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadSubItem()
    {
        try
        {
            ClsRateSheetMaster clsrsmaster = new ClsRateSheetMaster();
            DataTable dt = new DataTable();

            dt = clsrsmaster.BindSubitemtype(ddlDivision.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                ddlCategory.Items.Clear();
                //ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                //this.ddlCategory.AppendDataBoundItems = true;
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "SUBITEMDESC";
                ddlCategory.DataValueField = "SUBTYPEID";
                ddlCategory.DataBind();
            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlCategory.AppendDataBoundItems = true;
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlsupplieditem_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlsupplieditem.SelectedValue != "0")
        {
            LoadVendor();
            if (ddlsupplieditem.SelectedValue == "1")
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
                this.ddlCategory.AppendDataBoundItems = true;
                this.ddlDivision.Enabled = true;
                this.LoadDivision();
                gvRSMap.Columns[5].Visible = true;
                gvRSMap.Columns[6].Visible = true;
                gvRSMap.Columns[5].HeaderText = @"RM-PM COST(Rs.)";
                gvRSMap.Columns[6].HeaderText = @"CONVERSION COST(Rs.)";
                gvRSMap.Columns[3].Visible = false;
                gvRSMap.Columns[4].Visible = false;

            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlCategory.AppendDataBoundItems = true;
                this.ddlDivision.Enabled = false;
                LoadPrimaryItem();
                gvRSMap.Columns[5].Visible = true;
                gvRSMap.Columns[6].Visible = false;
                gvRSMap.Columns[5].HeaderText = @"COST(Rs.)";
                gvRSMap.Columns[6].HeaderText = @"CONVERSION COST(Rs.)";
                gvRSMap.Columns[3].Visible = false;
                //gvRSMap.Columns[3].Visible = true;
            }
        }
    }

    #region grdDespatchHeader_Exporting
    protected void gvRatesheet_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row01 = new TableRow();
        TableCell cel0l = new TableCell();
        cel0l.ColumnSpan = 8;
        cel0l.BorderStyle = BorderStyle.None;
        //cel0l.Text = "McNROE CONSUMERS PRODUCTS PRIVATE LIMITED";
        cel0l.Text = "K.K.G. INDUSTRIES (UNIT-III)";
        //cel0l.BackColor = Color.Yellow;
        cel0l.RowSpan = 1;
        cel0l.HorizontalAlign = HorizontalAlign.Left;
        cel0l.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row01.Cells.Add(cel0l);
        e.Table.Rows.Add(row01);

        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        cell.ColumnSpan = 8;
        cell.BorderStyle = BorderStyle.None;
        cell.Text = "Rate Sheet Details";
        //cell.BackColor = Color.Yellow;        
        cell.RowSpan = 1;
        cell.HorizontalAlign = HorizontalAlign.Left;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region grdDespatchHeader_Exported
    protected void gvRatesheet_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    public void BindGrid()
    {
        string divId = string.Empty;
        string catId = string.Empty;
        string VendorID = string.Empty;
        VendorID = this.ddlVendor.SelectedValue;
        ClsRsMaster clsrs = new ClsRsMaster();
        DataTable dtratesheet = new DataTable();
        dtratesheet = clsrs.FetchProductDetails(divId, catId, VendorID);
        gvRSMap.DataSource = dtratesheet;
        gvRSMap.DataBind();
        if (rdbFactory.Checked)
        {
            gvRSMap.Columns[5].Visible = false;
            // gvRSMap.Columns[5].HeaderText = @"CONVERSION COST(Rs.)";
        }
        else
        {
            //gvRSMap.Columns[5].Visible = true;
            // gvRSMap.Columns[6].HeaderText = @"CONVERSION COST(Rs.)";
        }
        ddlVendor.Enabled = true;
        ddlDivision.Enabled = true;
        ddlCategory.Enabled = true;

        pnlAdd.Style["display"] = "";
        pnlDisplay.Style["display"] = "none";
    }
}