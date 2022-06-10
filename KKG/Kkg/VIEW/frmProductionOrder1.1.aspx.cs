#region NameSpace
using BAL;
using PPBLL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

public partial class VIEW_frmProductionOrder1_1 : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region Add FinYear Wise Date Lock
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
                    CalendarExtender1.EndDate = today1;
                    CalendarExtender2.EndDate = today1;
                    CalendarExtender6.EndDate = today1;
                }
                else
                {
                    this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    CalendarExtender1.EndDate = cDate;
                    CalendarExtender2.EndDate = cDate;
                    CalendarExtender6.EndDate = cDate;
                }
                #endregion

                this.divsaleorderno.Style["display"] = "none";
                this.divsaleorderno1.Style["display"] = "none";
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadSuppliedItem();

                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                this.txtrequisiondate.Text = dtcurr.ToString(date).Replace('-', '/');
                /*this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');*/
                this.txtrequiredfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.LoadDepartment();
                this.BindFactoryName();
                this.LoadProductionOrder();
                //this.loadStoreLocation();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEMID", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("BUFFERQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("NETQTY", typeof(string)));
        HttpContext.Current.Session["PODETAILS"] = dt;
        return dt;
    }
    #endregion

    #region LoadSuppliedItem
    public void LoadSuppliedItem()
    {
        try
        {
            string mode = "po";
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataTable dt = clsRequisition.BindSuppliedItemmodewise(mode);

            this.ddlsupplieditem.Items.Clear();
            this.ddlsupplieditem.Items.Insert(0, new ListItem("Select Category", "0"));
            //this.ddlsupplieditem.Items.Insert(1, new ListItem("Select All", "1"));
            this.ddlsupplieditem.AppendDataBoundItems = true;

            if (dt.Rows.Count > 0)
            {
                this.ddlsupplieditem.DataSource = dt;
                this.ddlsupplieditem.DataTextField = "ITEMNAME";
                this.ddlsupplieditem.DataValueField = "ITEMID";
                this.ddlsupplieditem.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Department
    public void LoadDepartment()
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataTable dt = clsRequisition.BindDepartment();

            this.ddldepartment.Items.Clear();
            this.ddldepartment.Items.Insert(0, new ListItem("Select Department", "0"));
            this.ddldepartment.AppendDataBoundItems = true;

            if (dt.Rows.Count > 0)
            {
                this.ddldepartment.DataSource = dt;
                this.ddldepartment.DataTextField = "DEPTNAME";
                this.ddldepartment.DataValueField = "DEPTID";
                this.ddldepartment.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadMaterial
    public void LoadMaterial()
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataTable dt = clsRequisition.LoadMaterial(this.ddlsupplieditem.SelectedValue, this.ddlStorelocation.SelectedValue.ToString());

            this.ddlProductName.Items.Clear();
            this.ddlProductName.Items.Add(new ListItem("Select Material", "0"));
            this.ddlProductName.AppendDataBoundItems = true;

            if (dt.Rows.Count > 0)
            {
                this.ddlProductName.DataSource = dt;
                this.ddlProductName.DataTextField = "PRODUCTNAME";
                this.ddlProductName.DataValueField = "PRODUCTID";
                this.ddlProductName.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadUnit
    public void LoadUnit(string productId)
    {
        try
        {
            string mode = "PRODUCT_UOM";
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataTable dt = clsRequisition.LoadUnit_Productwise(mode, productId);

            this.ddlpackingsize.Items.Clear();
            // this.ddlpackingsize.Items.Add(new ListItem("Select Unit", "0"));
            this.ddlpackingsize.AppendDataBoundItems = true;

            if (dt.Rows.Count > 0)
            {
                this.ddlpackingsize.DataSource = dt;
                this.ddlpackingsize.DataTextField = "UOMNAME";
                this.ddlpackingsize.DataValueField = "UOMID";
                this.ddlpackingsize.DataBind();
            }
            else
            {
                MessageBox1.ShowWarning("NO UOM FOUND FOR THIS PRODUCT  : " + this.ddlProductName.SelectedItem.Text + "");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ddlsupplieditem_SelectedIndexChanged
    protected void ddlsupplieditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Requisition Check
    public int RecordsCheck(string PID, string FROMDATE, string TODATE, decimal QTY)
    {
        int flag = 0;

        DataTable dt = (DataTable)HttpContext.Current.Session["PODETAILS"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["ITEMID"].ToString() == PID)
            {
                flag = 1;
                break;
            }
        }

        return flag;
    }
    #endregion

    #region Delete Requisition from Grid
    public int RecordsDelete(string GUID)
    {
        int delflag = 0;

        DataTable dt = (DataTable)HttpContext.Current.Session["PODETAILS"];

        int i = dt.Rows.Count - 1;
        while (i >= 0)
        {
            if (dt.Rows[i]["GUID"].ToString().Trim() == GUID)
            {
                dt.Rows[i].Delete();
                dt.AcceptChanges();

                delflag = 1;
                break;
            }
            i--;
        }

        if (delflag > 0)
        {
            HttpContext.Current.Session["PODETAILS"] = dt;
            LoadRequisitionGrid(ref dt);
        }
        return delflag;
    }
    #endregion

    #region Add into Requisition Grid
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlpackingsize.SelectedValue == "" || this.ddlpackingsize.SelectedValue == "0")
            {
                MessageBox1.ShowWarning("You cannot add any product with out packsize!!");
                return;
            }

            int flag = RecordsCheck(this.ddlProductName.SelectedValue.ToString(), this.txtrequiredfromdate.Text.Trim(), this.txttorequireddate.Text.Trim(), Convert.ToDecimal(this.txtqty.Text.Trim()));

            if (flag != 0)
            {
                MessageBox1.ShowInfo("<b><font color=green>Material already exists.</font></b>");
                return;
            }
            else if (string.IsNullOrEmpty(this.txtqty.Text.Trim()) || Convert.ToDecimal(this.txtqty.Text.Trim()) == 0)
            {
                MessageBox1.ShowInfo("<b><font color=green>Please check material qty.</font></b>");
                return;
            }
            string categoryId = "";
            string CategoryName = "";
            if (this.ddlsupplieditem.SelectedValue == "1")
            {
                categoryId = getSuplliedItemId(this.ddlProductName.SelectedValue);
                CategoryName = getSuplliedItemName(this.ddlProductName.SelectedValue);
            }
            else
            {
                categoryId = this.ddlsupplieditem.SelectedValue.Trim();
                CategoryName = this.ddlsupplieditem.SelectedItem.Text.Trim();
            }

            DataTable dtRequisition = (DataTable)Session["PODETAILS"];

            DataRow dr = dtRequisition.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["CATEGORYID"] = Convert.ToString(categoryId);
            dr["CATEGORYNAME"] = Convert.ToString(CategoryName);
            dr["ITEMID"] = Convert.ToString(this.ddlProductName.SelectedValue.Trim());
            dr["ITEMNAME"] = Convert.ToString(this.ddlProductName.SelectedItem.Text.Trim());
            dr["PRODUCTIONQTY"] = Convert.ToString(this.txtqty.Text.Trim());
            dr["PACKSIZEID"] = Convert.ToString(this.ddlpackingsize.SelectedValue.Trim());
            dr["PACKSIZENAME"] = Convert.ToString(this.ddlpackingsize.SelectedItem.Text.Trim());
            dr["REQUIREDFROMDATE"] = Convert.ToString(this.txtrequiredfromdate.Text.Trim());
            dr["REQUIREDTODATE"] = Convert.ToString(this.txtrequiredfromdate.Text.Trim());

            dtRequisition.Rows.Add(dr);
            dtRequisition.AcceptChanges();

            this.LoadRequisitionGrid(ref dtRequisition);

            this.ddlProductName.SelectedValue = "0";
            this.ddlpackingsize.SelectedValue = "0";
            this.txtqty.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete Reqisition Grid Details
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {            
            int flag = 0;
            string guid = hdn_requisitionguiddelete.Value.ToString();
            flag = RecordsDelete(guid);

            if (flag == 1)
            {
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

    #region getSuppliedItem
    public string getSuplliedItemId(string productid)
    {
        string cateId = "";
        string mode = "Id";
        ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
        cateId = clsRequisition.getSuppliedItem(mode, productid);
        return (cateId);
    }
    public string getSuplliedItemName(string productid)
    {
        string cateName = "";
        string mode = "Name";
        ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
        cateName = clsRequisition.getSuppliedItem(mode, productid);
        return (cateName);
    }
    #endregion

    #region Load Requisition Grid Details
    public void LoadRequisitionGrid(ref DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvRequisition.DataSource = dt;
                gvRequisition.DataBind();
            }
            else
            {
                gvRequisition.DataSource = null;
                gvRequisition.DataBind();
            }

            LoadMaterial();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (ListItem item in ddlProductName.Items)
                {
                    if (dt.Rows[i]["ITEMID"].ToString() == item.Value)
                    {
                        item.Attributes.CssStyle.Add("color", "Red");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region New Requisition
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.CreateDataTable();
            this.imgPopuppodate.Style["display"] = "";
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divsaleorderno.Style["display"] = "none";
            this.divsaleorderno1.Style["display"] = "none";
            this.divBtnSave.Style["display"] = "";
            this.trAddProduct.Style["display"] = "";
            this.divnew.Style["display"] = "none";
            this.gvRequisition.Columns[8].Visible = true;
            this.ddldepartment.Enabled = true;
            this.ddlfactory.Enabled = true;
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtrequisiondate.Text= dtcurr.ToString(date).Replace('-', '/');
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Save Requisition
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if(this.ddlStorelocation.SelectedValue=="0" || this.ddlStorelocation.SelectedValue == "")
            {
                MessageBox1.ShowWarning("Please Select Storelocation");
                return;
            }
     
            DataTable dtrequisitionrecord = (DataTable)HttpContext.Current.Session["PODETAILS"];
            string PRODUCTIONNO = string.Empty;
            string xml = string.Empty;

            if (dtrequisitionrecord.Rows.Count > 0)
            {
                xml = ConvertDatatableToXML(dtrequisitionrecord);

                ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
                PRODUCTIONNO = clsRequisition.InsertUpdateProDetails(this.Hdn_Fld.Value.Trim(), this.txtrequisiondate.Text.Trim(), this.txtremarks.Text,
                              HttpContext.Current.Session["IUserID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), xml,
                              this.ddldepartment.SelectedValue.Trim(), this.ddldepartment.SelectedItem.ToString().Trim(), this.ddlfactory.SelectedValue.Trim(),this.ddlStorelocation.SelectedValue.ToString());
                if (!string.IsNullOrEmpty(PRODUCTIONNO))
                {
                    if (Convert.ToString(Hdn_Fld.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + PRODUCTIONNO + "</font></b> saved successfully,", 40, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + PRODUCTIONNO + "</font></b> updated successfully", 40, 550);
                    }

                    this.InputTable.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.divsaleorderno.Style["display"] = "none";
                    this.divsaleorderno1.Style["display"] = "none";

                    this.imgPopuppodate.Style["display"] = "";
                    this.gvRequisition.Columns[8].Visible = true;
                    this.ResetAll();
                    this.LoadProductionOrder();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color=red>Error on Saving record!</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please add atleast 1 Material");
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

    #region Reset Requisition
    public void ResetAll()
    {
        try
        {
            this.Hdn_Fld.Value = "";
            this.hdn_requisitionguiddelete.Value = "";
            this.ddlsupplieditem.SelectedValue = "0";
            this.ddlProductName.Items.Clear();
            this.ddlpackingsize.Items.Clear();
            this.ddlProductName.Items.Add(new ListItem("Select Material", "0"));
            this.ddlProductName.AppendDataBoundItems = true;
            this.ddlpackingsize.SelectedValue = "0";
            this.txtqty.Text = "0";
            this.txtremarks.Text = "";
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtrequiredfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            Session["PODETAILS"] = null;
            this.gvRequisition.DataSource = null;
            this.gvRequisition.DataBind();
            this.ddldepartment.SelectedValue = "0";
            this.txtStockQty.Text = "0";


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Requisition
    public void LoadProductionOrder()
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            this.gvRequisitiondetails.DataSource = clsRequisition.LoadProductionOrder(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), HttpContext.Current.Session["IUSERID"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
            this.gvRequisitiondetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Cancel Requisition
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divsaleorderno.Style["display"] = "none";
            this.divsaleorderno1.Style["display"] = "none";
            this.imgPopuppodate.Style["display"] = "";
            this.divnew.Style["display"] = "";
            this.gvRequisition.Columns[8].Visible = true;
            this.ResetAll();
            this.ddlStorelocation.Enabled = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region View Requisition
    protected void btngrdview_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataSet ds = clsRequisition.EditProductionOrder(this.Hdn_Fld.Value.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtrequisitionno.Text = ds.Tables[0].Rows[0]["PRODUCTIONNO"].ToString();
                this.txtrequisiondate.Text = ds.Tables[0].Rows[0]["PRODUCTIONDATE"].ToString();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                this.ddldepartment.Enabled = false;
                this.ddldepartment.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();

                this.ddlfactory.Enabled = false;
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();

                loadStoreLocation();
                this.ddlStorelocation.Enabled = false;
                this.ddlStorelocation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TOSTORELOCATIONID"]).Trim();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.CreateDataTable();
                    DataTable dtRequisitionEdit = (DataTable)Session["PODETAILS"];
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtRequisitionEdit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["ITEMID"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMID"]);
                        drEdit["ITEMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMNAME"]);
                        drEdit["PACKSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKSIZEID"]);
                        drEdit["PACKSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKSIZENAME"]);
                        drEdit["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDFROMDATE"]);
                        drEdit["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                        drEdit["PRODUCTIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTIONQTY"]);
                        drEdit["BUFFERQTY"] = 0;
                        drEdit["NETQTY"] = 0;
                        dtRequisitionEdit.Rows.Add(drEdit);
                        dtRequisitionEdit.AcceptChanges();
                    }

                    this.LoadRequisitionGrid(ref dtRequisitionEdit);
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Requisition details not found</b>");
                    return;
                }
                this.divsaleorderno.Style["display"] = "";
                this.divsaleorderno1.Style["display"] = "";
                this.imgPopuppodate.Style["display"] = "none";
                this.InputTable.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.trAddProduct.Style["display"] = "none";
                this.btnadd.Style["display"] = "";

                this.divBtnSave.Style["display"] = "none";
                this.gvRequisition.Columns[10].Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion btnsave_Click

    #region Edit Requisition
    protected void  btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataSet ds = clsRequisition.EditProductionOrder(this.Hdn_Fld.Value.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtrequisitionno.Text = ds.Tables[0].Rows[0]["PRODUCTIONNO"].ToString();
                this.txtrequisiondate.Text = ds.Tables[0].Rows[0]["PRODUCTIONDATE"].ToString();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                this.ddldepartment.Enabled = false;
                this.ddldepartment.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                loadStoreLocation();
                this.ddlStorelocation.Enabled = false;
                this.ddlStorelocation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TOSTORELOCATIONID"]).Trim();



                this.ddlfactory.Enabled = false;
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.CreateDataTable();
                    DataTable dtRequisitionEdit = (DataTable)Session["PODETAILS"];
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtRequisitionEdit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["ITEMID"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMID"]);
                        drEdit["ITEMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["ITEMNAME"]);
                        drEdit["PACKSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKSIZEID"]);
                        drEdit["PACKSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKSIZENAME"]);
                        drEdit["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDFROMDATE"]);
                        drEdit["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                        drEdit["PRODUCTIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTIONQTY"]);
                        drEdit["BUFFERQTY"] = 0;
                        drEdit["NETQTY"] = 0;
                        dtRequisitionEdit.Rows.Add(drEdit);
                        dtRequisitionEdit.AcceptChanges();
                    }
                    this.LoadRequisitionGrid(ref dtRequisitionEdit);
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Requisition details not found</b>");
                    return;
                }
                this.divsaleorderno.Style["display"] = "";
                this.divsaleorderno1.Style["display"] = "";
                this.divBtnSave.Style["display"] = "";
                this.imgPopuppodate.Style["display"] = "none";
                this.InputTable.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.trAddProduct.Style["display"] = "";
                this.btnadd.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete Requisition
    protected void btnfinalgrdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            int flag = 0;
            flag = clsRequisition.DeleteRequisition(Hdn_Fld.Value.Trim());
            if (flag > 0)
            {
                this.LoadProductionOrder();
                this.Hdn_Fld.Value = "";
                MessageBox1.ShowSuccess("<b><font color=green>Record deleted successfully.</font></b>");
            }
            else
            {
                MessageBox1.ShowError("<b><font color=red>Error on deleteing.</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Search Requisition
    protected void btnseach_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadProductionOrder();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void BindFactoryName()
    {
        DataSet ds = new DataSet();
        BAL.ClsCommonFunction ClsBindFactory = new BAL.ClsCommonFunction();
        this.ddlfactory.Items.Clear();
        ds = ClsBindFactory.BindFactory();
        this.ddlfactory.DataSource = ds.Tables[0];
        this.ddlfactory.DataTextField = "VENDORNAME";
        this.ddlfactory.DataValueField = "FACTORYID";
        this.ddlfactory.DataBind();
    }

    #region btnPrint_Click
    protected void btnNRGPPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?REQSIvoicveid=" + Hdn_Fld.Value.Trim() + "&&TAG=REQS&&MenuId=518";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlfactory.SelectedValue == "0")
        {
            MessageBox1.ShowInfo("Please select factory before add product!!!");
            return;
        }
        this.hdn_ProductStockQty.Value = "";
        this.LoadUnit(this.ddlProductName.SelectedValue);
        decimal stockQty = getStockQtyProduct(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue);
        this.hdn_ProductStockQty.Value = Convert.ToDecimal(stockQty).ToString();
        this.txtStockQty.Text = this.hdn_ProductStockQty.Value;
        this.lblFgStockQty.Text = this.hdn_ProductStockQty.Value;
    }

    public decimal getStockQtyProduct(string depotid, string productid)
    {
        decimal qty = 0;
        ClsRequisitionMaster stockQty = new ClsRequisitionMaster();
        DataTable dt = new DataTable();
        dt = stockQty.getStockQtyProductWise(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue,this.ddlStorelocation.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            qty = Convert.ToDecimal(dt.Rows[0]["Column1"].ToString());
        }

        return qty;
    }

    public void loadStoreLocation()
    {
        string deptId = "";
        deptId = this.ddldepartment.SelectedValue.ToString();
        string deptName = "";
        deptName = this.ddldepartment.SelectedItem.ToString();
        string mode = "STOREFRMDEPT";
        string userid = HttpContext.Current.Session["USERID"].ToString().Trim();
        ClsProductionOrder clsobj = new ClsProductionOrder();
        DataTable dtobj = new DataTable();
        dtobj = clsobj.loadstorefromdepartment(mode,deptId, userid);

        this.ddlStorelocation.Items.Clear();
        this.ddlStorelocation.Items.Insert(0, new ListItem("Select Storelocation", "0"));
        this.ddlStorelocation.AppendDataBoundItems = true;
        if (dtobj.Rows.Count > 0)
        {
            this.ddlStorelocation.Enabled = true;
            this.ddlStorelocation.DataSource = dtobj;
            this.ddlStorelocation.DataTextField = "NAME";
            this.ddlStorelocation.DataValueField = "ID";
            this.ddlStorelocation.DataBind();
            this.btnadd.Visible = true;
        }
        else
        {
            MessageBox1.ShowWarning("No Storelocation Found For "+ deptName +" Please Mappeed Storelocation ");
            this.btnadd.Visible = false;
            return;
        }
    }

    protected void ddlStorelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlStorelocation.SelectedValue != "0")
        {
            this.LoadMaterial();
        }
        else
        {
            this.ddlProductName.Items.Clear();
            this.ddlProductName.Items.Insert(0, new ListItem("Select Material", "0"));
        }
        decimal qty = 0;
        qty = this.getStockQtyProduct(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue);
        this.txtStockQty.Text = Convert.ToString(qty);
    }

    protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlsupplieditem.SelectedValue = "0";
        this.ddlProductName.Items.Clear();
        this.ddlProductName.Items.Add(new ListItem("Select Material", "0"));
        this.ddlProductName.AppendDataBoundItems = true;
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStoreLocation();
    }

    protected void Btnshow_Click(object sender, EventArgs e)
    {
        ClsRequisitionMaster ClsRequisitionMaster = new ClsRequisitionMaster();
        if (this.ddlProductName.SelectedValue == "0")
        {
            MessageBox1.ShowWarning("Please Slect Product First");
            return;
        }
        else
        {
            DataTable dtsaleorderrecord = ClsRequisitionMaster.FetchBomDetailsForView(this.ddlProductName.SelectedValue.ToString(), this.ddlStorelocation.SelectedValue,0);
            if (dtsaleorderrecord.Rows.Count > 0)
            {
                mp1.Show();
                gvBomDetails.DataSource = dtsaleorderrecord;
                gvBomDetails.DataBind();
            }
            else
            {
                MessageBox1.ShowWarning("No bom details found");
                return;
            }
        }

    }

    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        ClsRequisitionMaster ClsRequisitionMaster = new ClsRequisitionMaster();
        if (this.ddlProductName.SelectedValue == "0")
        {
            MessageBox1.ShowWarning("Please Slect Product First");
            return;
        }
        else
        {
            if (this.txtqty.Text == "")
            {
                this.txtqty.Text = "0";
            }
            DataTable dtsaleorderrecord = ClsRequisitionMaster.FetchBomDetailsForView(this.ddlProductName.SelectedValue.ToString(), this.ddlStorelocation.SelectedValue,Convert.ToDecimal(this.txtqty.Text));
            if (dtsaleorderrecord.Rows.Count > 0)
            {
                mp1.Show();
                gvBomDetails.DataSource = dtsaleorderrecord;
                gvBomDetails.DataBind();
            }
            else
            {
                MessageBox1.ShowWarning("No bom details found");
                return;
            }
        }
    }

    protected void gvBomDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Check your condition here
            Label grdLblNetQty = (Label)e.Row.FindControl("grdLblNetQty");
            Label grdLblStockQty = (Label)e.Row.FindControl("grdLblStockQty");

            decimal netQty = Convert.ToDecimal(grdLblNetQty.Text);
            decimal stockQty = Convert.ToDecimal(grdLblStockQty.Text);
            if(stockQty <= 0)
            {
                Label T = (Label)e.Row.FindControl("grdLblNetQty");
                T.ForeColor = Color.IndianRed;
                T.Font.Bold = true;
                Label T1 = (Label)e.Row.FindControl("grdLblStockQty");
                T1.ForeColor = Color.IndianRed;
                T1.Font.Bold = true;
                /*e.Row.BackColor = System.Drawing.Color.PaleVioletRed;*/// This will make row back color red
            }
            else if(stockQty < netQty)
            {
                Label T = (Label)e.Row.FindControl("grdLblNetQty");
                T.ForeColor = Color.OrangeRed;
                T.Font.Bold = true;
                Label T1 = (Label)e.Row.FindControl("grdLblStockQty");
                T1.ForeColor = Color.OrangeRed;// This will make row back color red
                T1.Font.Bold = true;
            }
            else
            {
                Label T = (Label)e.Row.FindControl("grdLblNetQty");
                T.ForeColor = Color.Green;
                T.Font.Bold = true;
                Label T1 = (Label)e.Row.FindControl("grdLblStockQty");
                T1.ForeColor = Color.Green;// This will make row back color red
                T1.Font.Bold = true;
            }

          


        }
    }
}