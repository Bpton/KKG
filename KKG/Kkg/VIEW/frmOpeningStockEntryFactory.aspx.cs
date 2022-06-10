using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmOpeningStockEntryFactory : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.LoadFactory();
                this.LoadBrand();
                this.DateLock();
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT SUB ITEM NAME", "0"));
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                ddlPackingSize.Items.Clear();
                ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
                this.LoadStoreloacation();
                string querystring = Request.QueryString["TAG"].ToString().Trim();
                this.BindGridOpenstock(this.Session["DEPOTID"].ToString().Trim(), querystring.Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void BindGridOpenstock(string DepotID, string TAG, string FinYear)
    {
        ClsOpenStock clsopenstock = new ClsOpenStock();
        gvOpenStock.DataSource = clsopenstock.BindOpenStockGrid(DepotID, TAG, FinYear);
        gvOpenStock.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdnDepotID.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            //ddlDeptName.SelectedValue = "-1";
            ddlBrand.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
            ddlProduct.SelectedValue = "0";
            ddlPackingSize.SelectedValue = "0";
            txtBatchno.Text = "";
            txtStockqty.Text = "";
            txtmrp.Text = "";
            txtmfgdate.Text = "";
            txtexpdate.Text = "";
            Hdnassementpercentage.Value = "";
            HttpContext.Current.Session["STOCKRECORDFACTORY"] = null;
            HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"] = null;
            this.grdAddStock.ClearPreviousDataSource();
            grdAddStock.DataSource = null;
            grdAddStock.DataBind();
            ddlstorelocation.SelectedValue = "0";
            CreateOpeningTable();
            Session["EDITPRODUCTIDFACTORY"] = null;

            if (Request.QueryString["TAG"].ToString().Trim() == "O")
            {
                CreateOpeningTable();
            }
            else
            {
                CreateOpeningTableClosing();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadFactory
    public void LoadFactory()
    {
        try
        {
            ClsOpenStock clsopenstock = new ClsOpenStock();
            DataTable dt = new DataTable();
            this.ddlDeptName.Items.Clear();
            dt = clsopenstock.BindFactory(Session["UserID"].ToString());
            this.ddlDeptName.DataSource = dt;
            this.ddlDeptName.DataValueField = "VENDORID";
            this.ddlDeptName.DataTextField = "VENDORNAME";
            this.ddlDeptName.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    public void LoadBrand()
    {
        ClsOpenStock clsopenstock = new ClsOpenStock();
        ddlBrand.Items.Clear();
        ddlBrand.Items.Insert(0, new ListItem("SELECT PRIMARY ITEM", "0"));
        ddlBrand.AppendDataBoundItems = true;
        ddlBrand.DataSource = clsopenstock.BindBrand_SupliedItem();
        ddlBrand.DataTextField = "DIVNAME";
        ddlBrand.DataValueField = "DIVID";
        ddlBrand.DataBind();
    }

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBrand.SelectedValue != "")
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT SUB ITEM", "0"));
                ddlCategory.AppendDataBoundItems = true;
                ddlCategory.DataSource = clsopenstock.BindCategory_SupliedItem(Convert.ToString(ddlBrand.SelectedValue));
                ddlCategory.DataTextField = "CATNAME";
                ddlCategory.DataValueField = "CATID";
                ddlCategory.DataBind();
            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "")
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();
                if (Convert.ToString(Request.QueryString["TAG"]).Trim() == "O")
                {
                    if (hdnDepotID.Value != "")
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                        ddlProduct.AppendDataBoundItems = true;
                        ddlProduct.DataSource = clsopenstock.BindProduct(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim());
                        ddlProduct.DataTextField = "NAME";
                        ddlProduct.DataValueField = "ID";
                        ddlProduct.DataBind();
                    }
                    else
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                        ddlProduct.AppendDataBoundItems = true;
                        ddlProduct.DataSource = clsopenstock.BindWithOutSaveProductopen(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim(), this.ddlDeptName.SelectedValue.Trim(),
                                                                                     Convert.ToString(Request.QueryString["TAG"]).Trim());
                        ddlProduct.DataTextField = "NAME";
                        ddlProduct.DataValueField = "ID";
                        ddlProduct.DataBind();
                    }
                }
                else
                {
                    if (hdnDepotID.Value != "")//EDIT
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                        ddlProduct.AppendDataBoundItems = true;
                        ddlProduct.DataSource = clsopenstock.BindProduct(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim());
                        //ddlProduct.DataSource = clsopenstock.BindWithOutSaveProduct(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim(),
                        //this.ddlDeptName.SelectedValue.Trim(), "O");

                        ddlProduct.DataTextField = "NAME";
                        ddlProduct.DataValueField = "ID";
                        ddlProduct.DataBind();
                    }
                    else
                    {
                        ddlProduct.Items.Clear();
                        ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                        ddlProduct.AppendDataBoundItems = true;
                        ddlProduct.DataSource = clsopenstock.BindWithOutSaveProductopen(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim(), this.ddlDeptName.SelectedValue.Trim(),
                                                                                     Convert.ToString(Request.QueryString["TAG"]).Trim());
                        ddlProduct.DataTextField = "NAME";
                        ddlProduct.DataValueField = "ID";
                        ddlProduct.DataBind();
                    }
                }
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProduct.SelectedValue != "")
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();
                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                ddlPackingSize.Items.Clear();
                ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
                ddlPackingSize.AppendDataBoundItems = true;
                ddlPackingSize.DataSource = clsMMPo.BindPackingSize(ddlProduct.SelectedValue.ToString(), ddlBrand.SelectedValue.ToString());
                ddlPackingSize.DataValueField = "PACKSIZEID_FROM";
                ddlPackingSize.DataTextField = "PACKSIZEName_FROM";
                ddlPackingSize.DataBind();

                DataTable dt = new DataTable();
                dt = clsopenstock.BindMRPperproduct(ddlProduct.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    //txtmrp.Text = dt.Rows[0]["MRP"].ToString().Trim();
                    Hdnassementpercentage.Value = Convert.ToString(dt.Rows[0]["ASSESSABLEPERCENT"]);

                    //DataTable dtweight = new DataTable();
                    //dtweight = clsopenstock.BindMRPperproduct(ddlProduct.SelectedValue.ToString());
                }

                if (!string.IsNullOrEmpty(this.txtmfgdate.Text.Trim()))
                {
                    string expdate = expdate = clsopenstock.GetProductExpirydate(this.ddlProduct.SelectedValue.Trim(), txtmfgdate.Text.ToString());
                    this.txtexpdate.Text = expdate;
                }
            }
            else
            {
                ddlPackingSize.SelectedValue = "0";
                //txtmrp.Text = "";
                Hdnassementpercentage.Value = "";
                ddlPackingSize.Items.Clear();
                ddlPackingSize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadStoreloacation()
    {
        ClsOpenStock clsopenstock = new ClsOpenStock();
        ddlstorelocation.Items.Clear();
        ddlstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
        ddlstorelocation.AppendDataBoundItems = true;
        ddlstorelocation.DataSource = clsopenstock.BindStorelocation();
        ddlstorelocation.DataValueField = "ID";
        ddlstorelocation.DataTextField = "Name";
        ddlstorelocation.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdnDepotID.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            ddlDeptName.SelectedValue = "0";
            ddlBrand.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
            ddlProduct.SelectedValue = "0";
            ddlPackingSize.SelectedValue = "0";
            txtBatchno.Text = "";
            txtStockqty.Text = "";
            txtmrp.Text = "";
            txtmfgdate.Text = "";
            txtexpdate.Text = "";
            Hdnassementpercentage.Value = "";
            HttpContext.Current.Session["STOCKRECORDFACTORY"] = null;
            HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"] = null;
            this.grdAddStock.ClearPreviousDataSource();
            grdAddStock.DataSource = null;
            grdAddStock.DataBind();
            ddlstorelocation.SelectedValue = "0";
            ddlDeptName.Enabled = true;
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtopeningdate.Text = dtcurr.ToString(date).Replace('-', '/');
            Session["EDITPRODUCTIDFACTORY"] = null;
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

    protected void CreateOpeningTable()
    {
        DataTable dtadd = new DataTable();

        dtadd.Columns.Add("GUID", typeof(string));
        dtadd.Columns.Add("BRID", typeof(string));
        dtadd.Columns.Add("BRNAME", typeof(string));
        dtadd.Columns.Add("DIVISIONID", typeof(string));
        dtadd.Columns.Add("DIVISIONNAME", typeof(string));
        dtadd.Columns.Add("CATEGORYID", typeof(string));
        dtadd.Columns.Add("CATEGORYNAME", typeof(string));
        dtadd.Columns.Add("PRODUCTID", typeof(string));
        dtadd.Columns.Add("PRODUCTNAME", typeof(string));
        dtadd.Columns.Add("UOMID", typeof(string));
        dtadd.Columns.Add("PACKSIZENAME", typeof(string));
        dtadd.Columns.Add("BATCHNO", typeof(string));
        dtadd.Columns.Add("STOCKQTY", typeof(string));
        dtadd.Columns.Add("MRP", typeof(string));
        dtadd.Columns.Add("DEPOTRATE", typeof(string));
        dtadd.Columns.Add("MFGDATE", typeof(string));
        dtadd.Columns.Add("EXPDATE", typeof(string));
        dtadd.Columns.Add("ASSESMENTPERCENTAGE", typeof(string));
        dtadd.Columns.Add("TOTALASSESMENTVALUE", typeof(string));
        dtadd.Columns.Add("NETWEIGHT", typeof(string));
        dtadd.Columns.Add("GROSSWEIGHT", typeof(string));
        dtadd.Columns.Add("STORELOCATIONID", typeof(string));
        dtadd.Columns.Add("STORELOCATIONNAME", typeof(string));
        dtadd.Columns.Add("TAG", typeof(string));
        HttpContext.Current.Session["STOCKRECORDFACTORY"] = dtadd;
    }
    protected void CreateOpeningTableClosing()
    {
        DataTable dtadd = new DataTable();

        dtadd.Columns.Add("GUID", typeof(string));
        dtadd.Columns.Add("BRID", typeof(string));
        dtadd.Columns.Add("BRNAME", typeof(string));
        dtadd.Columns.Add("DIVISIONID", typeof(string));
        dtadd.Columns.Add("DIVISIONNAME", typeof(string));
        dtadd.Columns.Add("CATEGORYID", typeof(string));
        dtadd.Columns.Add("CATEGORYNAME", typeof(string));
        dtadd.Columns.Add("PRODUCTID", typeof(string));
        dtadd.Columns.Add("PRODUCTNAME", typeof(string));
        dtadd.Columns.Add("UOMID", typeof(string));
        dtadd.Columns.Add("PACKSIZENAME", typeof(string));
        dtadd.Columns.Add("BATCHNO", typeof(string));
        dtadd.Columns.Add("STOCKQTY", typeof(string));
        dtadd.Columns.Add("MRP", typeof(string));
        dtadd.Columns.Add("DEPOTRATE", typeof(string));
        dtadd.Columns.Add("MFGDATE", typeof(string));
        dtadd.Columns.Add("EXPDATE", typeof(string));
        dtadd.Columns.Add("ASSESMENTPERCENTAGE", typeof(string));
        dtadd.Columns.Add("TOTALASSESMENTVALUE", typeof(string));
        dtadd.Columns.Add("NETWEIGHT", typeof(string));
        dtadd.Columns.Add("GROSSWEIGHT", typeof(string));
        dtadd.Columns.Add("STORELOCATIONID", typeof(string));
        dtadd.Columns.Add("STORELOCATIONNAME", typeof(string));
        dtadd.Columns.Add("TAG", typeof(string));
        HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"] = dtadd;
    }
    protected void btnAddStock_Click(object sender, EventArgs e)
    {
        try
        {
            ClsOpenStock clsopenstock = new ClsOpenStock();
            DataTable dtadd = new DataTable();
            int numberOfRecords = 0;

            if (this.txtmfgdate.Text != "")
            {
                int mgdate = Convert.ToInt32(Conver_To_ISO(this.txtmfgdate.Text.Trim()));
                int expdate = Convert.ToInt32(Conver_To_ISO(this.txtexpdate.Text.Trim()));

                if (expdate > mgdate)
                {
                    string mastermfgdate = string.Empty;
                    string masterexpdate = string.Empty;
                    DataTable dtbatchcheck = new DataTable();

                    if (Convert.ToString(Request.QueryString["TAG"]).Trim() == "O")
                    {
                        dtbatchcheck = clsopenstock.MasterBatchDetailsCheckFactory(this.ddlProduct.SelectedValue.Trim(), txtBatchno.Text.Trim(), Convert.ToDecimal(txtmrp.Text.Trim()), txtmfgdate.Text.Trim(), txtexpdate.Text.Trim(), this.Session["FinYear"].ToString().Trim());

                        if (dtbatchcheck.Rows[0]["STATUS"].ToString().Trim()=="YES")
                        {
                            mastermfgdate = dtbatchcheck.Rows[0]["MFDATE"].ToString().Trim();
                            masterexpdate = dtbatchcheck.Rows[0]["EXPRDATE"].ToString().Trim();
                            MessageBox1.ShowInfo("<b>BatchNo already exists into system with this <font color='green'>MFG Date: " + mastermfgdate + " & EXP Date: " + masterexpdate + "</font>, if you want to add this batchno pls ensure MFG & EXP date must be same as specified</b>!", 60, 800);
                            return;
                        }
                    }
                    else
                    {
                        string check = clsopenstock.checkBatchForClosingStockFactory(this.ddlProduct.SelectedValue.Trim(), txtBatchno.Text.Trim(), Convert.ToDecimal(txtmrp.Text.Trim()));
                        if (check == "1")
                        {
                            MessageBox1.ShowInfo("Batch Already exists,Please Create New Batch", 60, 450);
                            return;
                        }
                    }
                }
            }
            if (Convert.ToString(Request.QueryString["TAG"]).Trim() == "O")
            {
                if (HttpContext.Current.Session["STOCKRECORDFACTORY"] == null)
                {
                    CreateOpeningTable();
                }
                dtadd = (DataTable)HttpContext.Current.Session["STOCKRECORDFACTORY"];
            }
            else
            {
                if (HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"] == null)
                {
                    CreateOpeningTableClosing();
                }
                dtadd = (DataTable)HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"];
            }
            numberOfRecords = dtadd.Select("PRODUCTID = '" + this.ddlProduct.SelectedValue + "' AND BRID= '" + this.ddlDeptName.SelectedValue + "' AND MRP= '" + this.txtmrp.Text.Trim() + "'  AND DEPOTRATE= '" + this.txtRate.Text.Trim() + "' AND STORELOCATIONID='" + ddlstorelocation.SelectedValue + "'").Length;

            if (numberOfRecords > 0)
            {
                //string message = "alert('Product already exists with same batchno and same depot')";
                // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Product already exists with same mrp and same rate", 80, 700);
            }
            else
            {
                if (txtStockqty.Text != "" && ddlPackingSize.SelectedValue != "0" && ddlstorelocation.SelectedValue != "0")
                {
                    decimal TotAssesment = (clsopenstock.CalculateAmountInPcs(this.ddlProduct.SelectedValue, this.ddlPackingSize.SelectedValue,
                                                    Convert.ToDecimal(this.txtStockqty.Text), Convert.ToDecimal(this.txtmrp.Text))
                                                    * Convert.ToDecimal(Hdnassementpercentage.Value) / 100);

                    DataSet dtweight = clsopenstock.GetWeight(this.ddlProduct.SelectedValue, this.ddlPackingSize.SelectedValue, Convert.ToDecimal(this.txtStockqty.Text.Trim()));
                    //DepotRate = clsopenstock.BindDepotRate(Convert.ToDecimal(this.txtmrp.Text.Trim()),this.ddlProduct.SelectedValue.Trim());

                    //if ((dtweight.Tables[0].Rows.Count > 0) )
                    //{
                    DataRow dr = dtadd.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["BRID"] = Convert.ToString(this.ddlDeptName.SelectedValue);
                    dr["BRNAME"] = Convert.ToString(this.ddlDeptName.SelectedItem).Trim();
                    dr["DIVISIONID"] = Convert.ToString(this.ddlBrand.SelectedValue);
                    dr["DIVISIONNAME"] = Convert.ToString(this.ddlBrand.SelectedItem).Trim();
                    dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue);
                    dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                    dr["PRODUCTID"] = Convert.ToString(this.ddlProduct.SelectedValue);
                    dr["PRODUCTNAME"] = Convert.ToString(this.ddlProduct.SelectedItem).Trim();
                    dr["UOMID"] = Convert.ToString(this.ddlPackingSize.SelectedValue);
                    dr["PACKSIZENAME"] = Convert.ToString(this.ddlPackingSize.SelectedItem).Trim();
                    dr["BATCHNO"] = "NA";
                    dr["STOCKQTY"] = Convert.ToString(this.txtStockqty.Text.Trim()).Trim();
                    dr["MRP"] = Convert.ToDecimal(this.txtmrp.Text).ToString().Trim();
                    dr["DEPOTRATE"] = txtRate.Text.Trim();
                    dr["MFGDATE"] = "01/01/1900";
                    dr["EXPDATE"] = "01/01/1900";
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToDecimal(Hdnassementpercentage.Value).ToString().Trim();
                    dr["TOTALASSESMENTVALUE"] = TotAssesment;
                    dr["TAG"] = Convert.ToString(Request.QueryString["TAG"]).Trim();
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
                    dr["STORELOCATIONID"] = Convert.ToString(this.ddlstorelocation.SelectedValue);
                    dr["STORELOCATIONNAME"] = Convert.ToString(this.ddlstorelocation.SelectedItem).Trim();
                    dtadd.Rows.Add(dr);
                    dtadd.AcceptChanges();
                    //}
                    //else
                    //{
                    //    MessageBox1.ShowInfo("<b>Product weight not found</b>");
                    //    return;
                    //}
                }
                ddlProduct.SelectedValue = "0";
                ddlPackingSize.SelectedValue = "0";
                txtBatchno.Text = "";
                txtStockqty.Text = "";
                txtmrp.Text = "";
                txtmfgdate.Text = "";
                txtexpdate.Text = "";
                Hdnassementpercentage.Value = "";
                ddlstorelocation.SelectedValue = "0";
            }
            if (dtadd.Rows.Count > 0)
            {
                this.grdAddStock.DataSource = dtadd;
                this.grdAddStock.DataBind();
            }
            else
            {
                this.grdAddStock.ClearPreviousDataSource();
                this.grdAddStock.DataSource = null;
                this.grdAddStock.DataBind();
            }
            //else
            //{
            //    MessageBox1.ShowInfo("<b>Expire date must be greater than Mfg date</b>");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string sGUID = Convert.ToString(hdndtAddStockDelete.Value);
            DataTable dtdeleteStock = new DataTable();
            if (Request.QueryString["TAG"].ToString().Trim() == "O")
            {
                dtdeleteStock = (DataTable)Session["STOCKRECORDFACTORY"];
            }
            else
            {
                dtdeleteStock = (DataTable)Session["STOCKRECORDCLOSINGFACTORY"];
            }
            DataRow[] drr = dtdeleteStock.Select("GUID='" + sGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteStock.AcceptChanges();
            }
            Session["STOCKRECORDFACTORY"] = dtdeleteStock;
            this.grdAddStock.DataSource = dtdeleteStock;
            this.grdAddStock.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtRecords = new DataTable();
            if (Request.QueryString["TAG"].ToString().Trim() == "O")
            {
                dtRecords = (DataTable)HttpContext.Current.Session["STOCKRECORDFACTORY"];
            }
            else
            {
                dtRecords = (DataTable)HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"];
            }
            if (dtRecords.Rows.Count > 0)
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();
                string xml = string.Empty;
                string productid = string.Empty;

                xml = ConvertDatatableToXML(dtRecords);
                string Mode = "";
                //string brid="";
                int ID = 0;
                if (hdnDepotID.Value == "")
                {
                    Mode = "A";
                }
                else
                {
                    Mode = "U";
                }

                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    productid = productid + "" + dtRecords.Rows[i]["PRODUCTID"].ToString() + "" + ",";
                }

                productid = productid.Substring(0, productid.Length - 1);
                //string TOTALPRODUCT = string.Empty;
                //if (hdnDepotID.Value == "")
                //{
                //    productid = productid.Substring(0, productid.Length - 1);
                //    TOTALPRODUCT = productid.Trim();
                //}
                //else
                //{
                //    productid = productid.Substring(0, productid.Length - 1);
                //    TOTALPRODUCT = productid + "," + Session["EDITPRODUCTIDFACTORY"];

                //}
                string TAG = Request.QueryString["TAG"].ToString().Trim();

                ID = clsopenstock.Saveopenstock(Session["FINYEAR"].ToString(), xml, Mode, hdnDepotID.Value.ToString(), hdnbrandid.Value.ToString(), hdncatagoryid.Value.ToString(), productid, this.txtopeningdate.Text.Trim(), TAG.Trim());
                if (ID > 0)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("<b><font color='green'>Opening stock Take saved successfully!</font></b>", 60, 420);
                    BindGridOpenstock(this.Session["DEPOTID"].ToString().Trim(), Request.QueryString["TAG"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    grdAddStock.ClearPreviousDataSource();
                    grdAddStock.DataSource = null;
                    grdAddStock.DataBind();
                    //ddlDeptName.SelectedValue = "0";
                    ddlBrand.SelectedValue = "0";
                    ddlCategory.SelectedValue = "0";
                    ddlProduct.SelectedValue = "0";
                    ddlPackingSize.SelectedValue = "0";
                    txtBatchno.Text = "";
                    txtStockqty.Text = "";
                    txtmrp.Text = "";
                    txtmfgdate.Text = "";
                    txtexpdate.Text = "";
                    Hdnassementpercentage.Value = "";
                    ddlstorelocation.SelectedValue = "0";
                    ddlDeptName.Enabled = true;
                    DateTime dtcurr = DateTime.Now;
                    string date = "dd/MM/yyyy";
                    this.txtopeningdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    Session["EDITPRODUCTIDFACTORY"] = null;
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful..</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please entry atleast 1 product</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            string depotid = hdnDepotID.Value.ToString();
            string brandid = hdnbrandid.Value.ToString();
            string catagoryid = hdncatagoryid.Value.ToString();
            string locationid = hdnlocationid.Value.ToString();
            string TAG = Request.QueryString["TAG"].ToString().Trim();
            ClsOpenStock clsopenstock = new ClsOpenStock();
            DataTable dtedit = new DataTable();
            dtedit = clsopenstock.Gridedit(depotid, brandid, catagoryid, locationid, TAG);
            if (dtedit.Rows.Count > 0)
            {
                DataTable dtgirdEdit = new DataTable();
                if (TAG == "O")
                {
                    CreateOpeningTable();
                    dtgirdEdit = (DataTable)HttpContext.Current.Session["STOCKRECORDFACTORY"];
                }
                else
                {
                    CreateOpeningTableClosing();
                    dtgirdEdit = (DataTable)HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"];
                }
                ddlDeptName.Enabled = false;
                ddlPackingSize.Items.Clear();
                ddlPackingSize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
                ddlPackingSize.AppendDataBoundItems = true;

                this.LoadFactory();
                this.ddlDeptName.SelectedValue = depotid;
                this.LoadBrand();
                this.ddlBrand.SelectedValue = brandid;
                this.ddlBrand_SelectedIndexChanged(sender, e);
                this.ddlCategory.SelectedValue = catagoryid;
                this.ddlCategory_SelectedIndexChanged(sender, e);
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                this.txtopeningdate.Text = dtedit.Rows[0]["OPENINGENTRYDATE"].ToString();

                for (int i = 0; i < dtedit.Rows.Count; i++)
                {
                    DataRow drt = dtgirdEdit.NewRow();
                    drt["GUID"] = Guid.NewGuid();
                    drt["BRID"] = Convert.ToString(dtedit.Rows[i]["BRID"]);
                    drt["BRNAME"] = Convert.ToString(dtedit.Rows[i]["BRNAME"]).Trim();
                    drt["DIVISIONID"] = Convert.ToString(dtedit.Rows[i]["DIVISIONID"]);
                    drt["DIVISIONNAME"] = Convert.ToString(dtedit.Rows[i]["DIVISIONNAME"]).Trim();
                    drt["CATEGORYID"] = Convert.ToString(dtedit.Rows[i]["CATEGORYID"]);
                    drt["CATEGORYNAME"] = Convert.ToString(dtedit.Rows[i]["CATEGORYNAME"]).Trim();
                    drt["PRODUCTID"] = Convert.ToString(dtedit.Rows[i]["PRODUCTID"]);
                    drt["PRODUCTNAME"] = Convert.ToString(dtedit.Rows[i]["PRODUCTNAME"]).Trim();
                    //drt["PACKSIZEID"] = Convert.ToString(dtedit.Rows[i]["PACKSIZEID"]);
                    drt["UOMID"] = Convert.ToString(dtedit.Rows[i]["PACKSIZEID"]);
                    drt["PACKSIZENAME"] = Convert.ToString(dtedit.Rows[i]["PACKSIZENAME"]).Trim();
                    drt["BATCHNO"] = Convert.ToString(dtedit.Rows[i]["BATCHNO"]).Trim();
                    drt["STOCKQTY"] = Convert.ToString(dtedit.Rows[i]["STOCKQTY"]).Trim();
                    drt["MRP"] = Convert.ToString(dtedit.Rows[i]["MRP"]).Trim();
                    drt["DEPOTRATE"] = Convert.ToString(dtedit.Rows[i]["DEPOTRATE"]).Trim();
                    drt["MFGDATE"] = Convert.ToString(dtedit.Rows[i]["MFGDATE"]).Trim();
                    drt["EXPDATE"] = Convert.ToString(dtedit.Rows[i]["EXPDATE"]).Trim();
                    drt["ASSESMENTPERCENTAGE"] = Convert.ToDecimal(dtedit.Rows[i]["ASSESMENTPERCENTAGE"]);
                    drt["TOTALASSESMENTVALUE"] = Convert.ToDecimal(dtedit.Rows[i]["TOTALASSESMENTVALUE"]);
                    drt["NETWEIGHT"] = dtedit.Rows[i]["NETWEIGHT"];
                    drt["GROSSWEIGHT"] = dtedit.Rows[i]["GROSSWEIGHT"];
                    drt["STORELOCATIONID"] = Convert.ToString(dtedit.Rows[i]["STORELOCATIONID"]);
                    drt["STORELOCATIONNAME"] = Convert.ToString(dtedit.Rows[i]["STORELOCATIONNAME"]).Trim();
                    drt["TAG"] = Convert.ToString(dtedit.Rows[i]["TAG"]).Trim();
                    dtgirdEdit.Rows.Add(drt);
                    dtgirdEdit.AcceptChanges();
                }
                //string editproductid = string.Empty;
                //for (int i = 0; i < dtgirdEdit.Rows.Count; i++)
                //{
                //    editproductid = editproductid + "" + dtgirdEdit.Rows[i]["PRODUCTID"].ToString() + "" + ",";
                //}
                //if (editproductid.Length > 0)
                //{
                //    editproductid = editproductid.Substring(0, editproductid.Length - 1);
                //    Session["EDITPRODUCTIDFACTORY"] = editproductid.Trim();
                //}

                if (TAG == "O")
                {
                    HttpContext.Current.Session["STOCKRECORDFACTORY"] = dtgirdEdit;
                }
                else
                {
                    HttpContext.Current.Session["STOCKRECORDCLOSINGFACTORY"] = dtgirdEdit;

                }
                this.grdAddStock.DataSource = dtgirdEdit;
                this.grdAddStock.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("<b>No records found!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void txtmfgdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlProduct.SelectedValue != "0")
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();

                if (!string.IsNullOrEmpty(this.txtmfgdate.Text.Trim()))
                {
                    string PRODUCT = this.ddlProduct.SelectedValue;
                    string expdate = string.Empty;
                    expdate = clsopenstock.GetProductExpirydate(PRODUCT, txtmfgdate.Text.ToString());
                    txtexpdate.Text = expdate;
                }
            }
            else
            {
                this.txtmfgdate.Text = "";
                //MessageBox1.ShowInfo("<b>Please select product name!</b>");
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
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender1.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtopeningdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarExtender1.EndDate = today1;
        }
        else
        {
            this.txtopeningdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtender1.EndDate = cDate;
        }
    }
    #endregion

  

    protected void btnOpenningDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster clsproduct = new ClsProductMaster();
            DataTable dt = clsproduct.DownloadOpenningProductList();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=Product_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";

                }
                Response.Write("\n");
            }

            Response.End();

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "Error", "<script type='text/javascript'>alert('" + ex.Message + "')</script>");
        }
    }
}