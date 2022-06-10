#region NameSpace
using BAL;
using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

public partial class VIEW_frmRequisitionMaster : System.Web.UI.Page
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
                string userid = HttpContext.Current.Session["USERID"].ToString().Trim();
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
                    this.txtrequisiondate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    CalendarExtender1.EndDate = today1;
                    CalendarExtender2.EndDate = today1;
                    CalendarExtender6.EndDate = today1;
                }
                else
                {
                    this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtrequisiondate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
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
                this.LoadAllRequisition();
                this.loadStoreLocation();
                hdOwnLocationID.Value = getStoreId(userid);
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
        dt.Columns.Add(new DataColumn("MATERIALID", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dt.Columns.Add(new DataColumn("StockQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("BUFFERQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("NETQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("OWN_STOCKQTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DESTINATION_STOCKQTY", typeof(decimal)));
        HttpContext.Current.Session["REQUISITIONDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region LoadSuppliedItem
    public void LoadSuppliedItem()
    {
        try
        {
            string mode = "Requ";
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataTable dt = clsRequisition.BindSuppliedItemmodewise(mode);

            this.ddlsupplieditem.Items.Clear();
            this.ddlsupplieditem.Items.Insert(0, new ListItem("Select Category", "0"));
            this.ddlsupplieditem.Items.Insert(1, new ListItem("Select All", "10"));
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
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindRequistionDepartment();

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
            DataTable dt = clsRequisition.LoadMaterial(this.ddlsupplieditem.SelectedValue);

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
            DataTable dt = clsRequisition.LoadUnit_Productwise(mode,productId);

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
            if (this.ddlsupplieditem.SelectedValue != "0")
            {
                this.LoadMaterial();
            }
            else
            {
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Insert(0, new ListItem("Select Material", "0"));
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

    #region Requisition Check
    public int RecordsCheck(string PID, string FROMDATE, string TODATE, decimal QTY)
    {
        int flag = 0;

        DataTable dt = (DataTable)HttpContext.Current.Session["REQUISITIONDETAILS"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["MATERIALID"].ToString() == PID)
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

        DataTable dt = (DataTable)HttpContext.Current.Session["REQUISITIONDETAILS"];

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
            HttpContext.Current.Session["REQUISITIONDETAILS"] = dt;
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
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlProductName.ClientID + "').focus(); ", true);
            if (this.ddlpackingsize.SelectedValue==""|| this.ddlpackingsize.SelectedValue == "0")
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
            if (this.ddlsupplieditem.SelectedValue == "1" || this.ddlsupplieditem.SelectedValue == "10")
            {
                categoryId=getSuplliedItemId(this.ddlProductName.SelectedValue);
                CategoryName=getSuplliedItemName(this.ddlProductName.SelectedValue);
            }
            else
            {
                categoryId = this.ddlsupplieditem.SelectedValue.Trim();
                CategoryName = this.ddlsupplieditem.SelectedItem.Text.Trim();
            }

            DataTable dtRequisition = (DataTable)Session["REQUISITIONDETAILS"];

            DataRow dr = dtRequisition.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["CATEGORYID"] = Convert.ToString(categoryId);
            dr["CATEGORYNAME"] = Convert.ToString(CategoryName);
            dr["MATERIALID"] = Convert.ToString(this.ddlProductName.SelectedValue.Trim());
            dr["MATERIALNAME"] = Convert.ToString(this.ddlProductName.SelectedItem.Text.Trim());
            dr["StockQTY"] = Convert.ToString(this.hdn_ProductStockQty.Value.ToString());
            dr["OWN_STOCKQTY"] = getStockQty(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue.Trim(),this.hdOwnLocationID.Value.ToString());
            dr["DESTINATION_STOCKQTY"] = getStockQty(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue.Trim(), this.ddlStorelocation.SelectedValue.ToString());
            dr["QTY"] = Convert.ToString(this.txtqty.Text.Trim());
            dr["UOMID"] = Convert.ToString(this.ddlpackingsize.SelectedValue.Trim());
            dr["UOMNAME"] = Convert.ToString(this.ddlpackingsize.SelectedItem.Text.Trim());
            dr["REQUIREDFROMDATE"] = Convert.ToString(this.txtrequiredfromdate.Text.Trim());
            dr["REQUIREDTODATE"] = Convert.ToString(this.txtrequiredfromdate.Text.Trim());

            dtRequisition.Rows.Add(dr);
            dtRequisition.AcceptChanges();

            this.LoadRequisitionGrid(ref dtRequisition);
          

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
        cateId = clsRequisition.getSuppliedItem(mode,productid);
        return (cateId);
    }
    public string getSuplliedItemName(string productid)
    {
        string cateName = "";
        string mode = "Name";
        ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
        cateName = clsRequisition.getSuppliedItem(mode,productid);
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
            this.ddlProductName.SelectedValue = "0";
            this.ddlpackingsize.SelectedValue = "0";
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

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlfactory.ClientID + "').focus(); ", true);
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
            DataTable dtrequisitionrecord = (DataTable)HttpContext.Current.Session["REQUISITIONDETAILS"];
            string requisionno = string.Empty;
            string xml = string.Empty;

            if (dtrequisitionrecord.Rows.Count > 0)
            {
                xml = ConvertDatatableToXML(dtrequisitionrecord);

                ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
                requisionno = clsRequisition.InsertRequisitionDetails(this.Hdn_Fld.Value.Trim(), this.txtrequisiondate.Text.Trim(), this.txtremarks.Text,
                              HttpContext.Current.Session["IUserID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), xml,
                              this.ddldepartment.SelectedValue.Trim(), this.ddldepartment.SelectedItem.ToString().Trim(), this.ddlfactory.SelectedValue.Trim(),this.ddlStorelocation.SelectedValue.ToString());
                if (!string.IsNullOrEmpty(requisionno))
                {
                    if (Convert.ToString(Hdn_Fld.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + requisionno + "</font></b> saved successfully,", 40, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + requisionno + "</font></b> updated successfully", 40, 550);
                    }

                    this.InputTable.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.divsaleorderno.Style["display"] = "none";
                    this.divsaleorderno1.Style["display"] = "none";

                    this.imgPopuppodate.Style["display"] = "";
                    this.gvRequisition.Columns[8].Visible = true;
                    this.ResetAll();
                    this.LoadAllRequisition();
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
            this.txtOwnQty.Text = "0";
            this.txtremarks.Text = "";
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtrequiredfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txttorequireddate.Text = dtcurr.ToString(date).Replace('-', '/');
            Session["REQUISITIONDETAILS"] = null;
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
    public void LoadAllRequisition()
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            this.gvRequisitiondetails.DataSource = clsRequisition.LoadRequisition(this.txtfromdate.Text.Trim(), this.txttodate.Text.Trim(), HttpContext.Current.Session["IUSERID"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
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
            DataSet ds = clsRequisition.EditRequisition(this.Hdn_Fld.Value.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtrequisitionno.Text = ds.Tables[0].Rows[0]["REQUISITIONNO"].ToString();
                this.txtrequisiondate.Text = ds.Tables[0].Rows[0]["REQUISITIONDATE"].ToString();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
               // this.ddldepartment.Enabled = false;
                this.ddldepartment.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                this.ddlfactory.Enabled = false;
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.CreateDataTable();
                    DataTable dtRequisitionEdit = (DataTable)Session["REQUISITIONDETAILS"];
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtRequisitionEdit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["MATERIALID"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALID"]);
                        drEdit["MATERIALNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALNAME"]);
                        drEdit["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEdit["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEdit["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]);
                        drEdit["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDFROMDATE"]);
                        drEdit["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                        drEdit["StockQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKQTY"]);
                        drEdit["OWN_STOCKQTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["OWN_STOCKQTY"]);
                        drEdit["DESTINATION_STOCKQTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["DESTINATION_STOCKQTY"]);
                        drEdit["BUFFERQTY"] = 0;
                        //Convert.ToString(ds.Tables[1].Rows[i]["BUFFERQTY"]);
                        drEdit["NETQTY"] = 0;
                        //Convert.ToString(ds.Tables[1].Rows[i]["NETQTY"]);
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
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsRequisitionMaster clsRequisition = new ClsRequisitionMaster();
            DataSet ds = clsRequisition.EditRequisition(this.Hdn_Fld.Value.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtrequisitionno.Text = ds.Tables[0].Rows[0]["REQUISITIONNO"].ToString();
                this.txtrequisiondate.Text = ds.Tables[0].Rows[0]["REQUISITIONDATE"].ToString();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                LoadDepartment();
                
               // this.ddldepartment.Enabled = false;
                this.ddldepartment.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                loadStoreLocation();

                this.ddlfactory.Enabled = false;
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.CreateDataTable();
                    DataTable dtRequisitionEdit = (DataTable)Session["REQUISITIONDETAILS"];
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtRequisitionEdit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["MATERIALID"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALID"]);
                        drEdit["MATERIALNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALNAME"]);
                        drEdit["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEdit["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEdit["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]);
                        drEdit["REQUIREDFROMDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDFROMDATE"]);
                        drEdit["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                        drEdit["StockQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKQTY"]);
                        drEdit["OWN_STOCKQTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["OWN_STOCKQTY"]);
                        drEdit["DESTINATION_STOCKQTY"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["DESTINATION_STOCKQTY"]);
                        drEdit["BUFFERQTY"] = 0;
                        //Convert.ToString(ds.Tables[1].Rows[i]["BUFFERQTY"]);
                        drEdit["NETQTY"] = 0;
                        //Convert.ToString(ds.Tables[1].Rows[i]["NETQTY"]);
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

            string requisitionno = this.txtrequisitionno.Text;
            string number = requisitionno.Substring(0, 4);
            if(number=="AUTO")
            {
                this.divBtnSave.Style["display"] = "none";
            }
            else
            {
                this.divBtnSave.Style["display"] = "";
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
                this.LoadAllRequisition();
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
            this.LoadAllRequisition();
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
        this.ddlfactory.Items.Insert(0, new ListItem("-Select Factory -", "0"));
        ds = ClsBindFactory.BindFactory();
        this.ddlfactory.DataSource = ds.Tables[0];
        this.ddlfactory.DataTextField = "VENDORNAME";
        this.ddlfactory.DataValueField = "FACTORYID";
        this.ddlfactory.DataBind();
        this.ddlfactory.SelectedValue = ds.Tables[0].Rows[0]["FACTORYID"].ToString();
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
        if(this.ddlfactory.SelectedValue=="0")
        {
            MessageBox1.ShowInfo("Please Select Factory for stock qty!!!");
            return;
        }
        this.hdn_ProductStockQty.Value = "";
        this.LoadUnit(this.ddlProductName.SelectedValue);
        decimal stockQty = getStockQtyProduct(this.ddlfactory.SelectedValue,this.ddlProductName.SelectedValue);
        this.hdn_ProductStockQty.Value =Convert.ToDecimal(stockQty).ToString();
        this.txtStockQty.Text = Convert.ToString(
            getStockQty(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue.Trim(), this.ddlStorelocation.SelectedValue.ToString()));
        this.txtOwnQty.Text = Convert.ToString(
            getStockQty(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue.Trim(), this.hdOwnLocationID.Value.ToString()));

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtqty.ClientID + "').focus(); ", true);
    }

    public decimal getStockQtyProduct(string depotid,string productid)
    {
        decimal qty = 0;
        ClsRequisitionMaster stockQty = new ClsRequisitionMaster();
        DataTable dt = new DataTable();
        try
        {

            if(this.ddlStorelocation.SelectedValue=="0")
            {
                MessageBox1.ShowInfo("Please Select Storelocation");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlStorelocation.ClientID + "').focus(); ", true);
            }
            dt = stockQty.getStockQtyProductWise(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue, this.ddlStorelocation.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                qty = Convert.ToDecimal(dt.Rows[0]["Column1"].ToString());
                
            }
        }
        catch(Exception ex)
        {
            throw ex;

        }
        
        return qty;
    }


    public decimal getStockQty(string depotid, string productid,string locationid)
    {
        decimal qty = 0;
        ClsRequisitionMaster stockQty = new ClsRequisitionMaster();
        DataTable dt = new DataTable();
        try
        {
            dt = stockQty.getStockQtyProductWise(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue, locationid);

            if (dt.Rows.Count > 0)
            {
                qty = Convert.ToDecimal(dt.Rows[0]["Column1"].ToString());

            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

        return qty;
    }

    protected void ddlfactory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlsupplieditem.SelectedValue = "0";
        this.ddlProductName.Items.Clear();
        this.ddlProductName.Items.Add(new ListItem("Select Item", "0"));
        this.ddlProductName.AppendDataBoundItems = true;

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlsupplieditem.ClientID + "').focus(); ", true);
    }

    public void loadStoreLocation()
    {
        string Mode = string.Empty;
        Mode = "S";
        ClsProductionOrder clsobj = new ClsProductionOrder();
        DataTable dtobj = new DataTable();
        dtobj = clsobj.loadstorefromdepartment("",this.ddldepartment.SelectedValue,"");

        this.ddlStorelocation.Items.Clear();
        this.ddlStorelocation.Items.Insert(0, new ListItem("Select Storelocation", "0"));
        this.ddlStorelocation.AppendDataBoundItems = true;
        if (dtobj.Rows.Count > 0)
        {
            this.ddlStorelocation.DataSource = dtobj;
            this.ddlStorelocation.DataTextField = "NAME";
            this.ddlStorelocation.DataValueField = "ID";
            this.ddlStorelocation.DataBind();
            if (dtobj.Rows.Count == 1)
            {
                this.ddlStorelocation.SelectedValue = Convert.ToString(dtobj.Rows[0]["ID"].ToString());
            }
        }
        
    }

    protected void ddlStorelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal qty = 0;
        qty = this.getStockQtyProduct(this.ddlfactory.SelectedValue, this.ddlProductName.SelectedValue);
        this.txtStockQty.Text = Convert.ToString(qty);
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStoreLocation();
    }

    public string getStoreId(string userid)
    {
        string Id = string.Empty;
        ClsRequisitionMaster stockQty = new ClsRequisitionMaster();
        DataTable dt = new DataTable();
        try
        {
            dt = stockQty.getstoreid(userid);

            if (dt.Rows.Count > 0)
            {
                Id = Convert.ToString(dt.Rows[0]["ID"].ToString());

            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

        return Id;
    }
}