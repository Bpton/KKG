using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class VIEW_frmStoreWiseProductMapping : System.Web.UI.Page
{

    ClsCityMaster clsobj = new ClsCityMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                LoadPrimaryItem();
                Loadstorelocation();
                loadStoreWiseProductCount();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        ClearControl();
    }

  
    public void Loadstorelocation()
    {
        try
        {
            string Mode = string.Empty;
            Mode = "S";
          
            DataTable dtobj = new DataTable();
            dtobj = clsobj.loadstorelocation(Mode);
            if (dtobj.Rows.Count > 0)
            {

                this.ddlStore.Items.Clear();
                this.ddlStore.Items.Add(new ListItem("Select StoreLocation", "0"));
                this.ddlStore.AppendDataBoundItems = true;
                this.ddlStore.DataSource = dtobj;
                this.ddlStore.DataTextField = "NAME";
                this.ddlStore.DataValueField = "ID";
                this.ddlStore.DataBind();
            }
            else
            {
                
            }

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
            string Mode = string.Empty;
            Mode = "P";

            DataTable dtobj = new DataTable();
            dtobj = clsobj.loadstorelocation(Mode);
            if (dtobj.Rows.Count > 0)
            {

                this.ddlPrimaryItem.Items.Clear();
                this.ddlPrimaryItem.Items.Add(new ListItem("Select Item", "0"));
                this.ddlPrimaryItem.AppendDataBoundItems = true;
                this.ddlPrimaryItem.DataSource = dtobj;
                this.ddlPrimaryItem.DataTextField = "DIVNAME";
                this.ddlPrimaryItem.DataValueField = "DIVID";
                this.ddlPrimaryItem.DataBind();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("STOREID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        HttpContext.Current.Session["STOREPRODUCTMAPPING"] = dt;


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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if(this.ddlStore.SelectedValue=="0")
        {
            MessageBox1.ShowWarning("Please Select Storelocation");
            return;
        }
        GridView gridView = grdStoreProdutMapping;
        string gvchkbox = "";
        string gvSTOREID = "";
        string gvSTORENAME = "";
        string gvPRODUCTID = "";
        string gvPRODUCTNAME = "";
        int count = 0;
        string XML = string.Empty;
        DataTable result = new DataTable();
        if (HttpContext.Current.Session["STOREPRODUCTMAPPING"] == null)
        {
            CreateDataTable();
        }
        DataTable DT = (DataTable)HttpContext.Current.Session["STOREPRODUCTMAPPING"];

       
        gvchkbox = "chkSelect";
        gvSTOREID = "lblSTOREID";
        gvSTORENAME = "lblSTORENAME";
        gvPRODUCTID = "lblPRODUCTID";
        gvPRODUCTNAME = "lblPRODUCTNAME";

        foreach (GridViewRow gvrow in gridView.Rows)
        {
            CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
            Label lblSTOREID = (Label)gvrow.FindControl("" + gvSTOREID + "");
            Label lblSTORENAME = (Label)gvrow.FindControl("" + gvSTORENAME + "");
            Label lblPRODUCTID = (Label)gvrow.FindControl("" + gvPRODUCTID + "");
            Label lblPRODUCTNAME = (Label)gvrow.FindControl("" + gvPRODUCTNAME + "");
            if (chkBx.Checked)
            {
                count = count + 1;
                DataRow dr = DT.NewRow();
                dr["STOREID"] = lblSTOREID.Text.Trim();
                dr["STORENAME"] = lblSTORENAME.Text.Trim();
                dr["PRODUCTID"] = lblPRODUCTID.Text.Trim();
                dr["PRODUCTNAME"] = lblPRODUCTNAME.Text.Trim();
                DT.Rows.Add(dr);
                DT.AcceptChanges();
            }
        }
        HttpContext.Current.Session["STOREPRODUCTMAPPING"] = DT;

        if (DT.Rows.Count > 0) {
            XML = ConvertDatatable(DT);
        }
        else
        {
            MessageBox1.ShowWarning("Please Check Atleast One checkbox");
            return;
        }


        
        string userId = HttpContext.Current.Session["USERID"].ToString().Trim();
        string finYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        result = clsobj.InsertUpdateStoreWiseProduct(this.ddlStore.SelectedValue,this.ddlStore.SelectedItem.ToString(),XML, userId, finYear);

        if(result.Rows[0]["MESSAGE"].ToString() == "INSERT")
        {
            MessageBox1.ShowSuccess("Product Mapped With" +' '+ this.ddlStore.SelectedItem +' '+"Insert Done");
            RemoveSession();
            ClearControl();
            clsoe();
            loadStoreWiseProductCount();
            return;
        }
        else if(result.Rows[0]["MESSAGE"].ToString() == "UPDATE")
        {
            MessageBox1.ShowSuccess("Product Mapped With" +' '+ this.ddlStore.SelectedItem +' '+ "Update Done");
            RemoveSession();
            ClearControl();
            clsoe();
            loadStoreWiseProductCount();
            return;
        }
        else
        {
            MessageBox1.ShowError("Error On Saving Record");
            RemoveSession();
            return;
        }
    }

    protected void grdStoreProdutMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSTORENAME = (Label)e.Row.FindControl("lblSTORENAME");
                string STORENAME = lblSTORENAME.Text.Trim().ToUpper();

                Label lblPRODUCTNAME = (Label)e.Row.FindControl("lblPRODUCTNAME");
                string PRODUCTNAME = lblPRODUCTNAME.Text.Trim().ToUpper();

                Label lblDIVNAME = (Label)e.Row.FindControl("lblDIVNAME");
                string DIVNAME = lblDIVNAME.Text.Trim().ToUpper();

                Label lblCATNAME = (Label)e.Row.FindControl("lblCATNAME");
                string CATNAME = lblCATNAME.Text.Trim().ToUpper();

                Label lblCODE = (Label)e.Row.FindControl("lblCODE");
                string CODE = lblCODE.Text.Trim().ToUpper();


                CheckBox chkBx = (e.Row.FindControl("chkSelect") as CheckBox);
               
                    if(STORENAME!="")
                    {
                        
                        lblSTORENAME.ForeColor = Color.Green;
                        lblPRODUCTNAME.ForeColor = Color.Green;
                        lblDIVNAME.ForeColor = Color.Green;
                        lblCATNAME.ForeColor = Color.Green;
                        lblCODE.ForeColor = Color.Green;
                    }
                    else
                    {
                       
                        lblSTORENAME.ForeColor = Color.Blue;
                        lblPRODUCTNAME.ForeColor = Color.Blue;
                        lblDIVNAME.ForeColor = Color.Blue;
                        lblCATNAME.ForeColor = Color.Blue;
                        lblCODE.ForeColor = Color.Blue;
                    }
                    
                
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            
            DataTable dtObj = new DataTable();
            dtObj = clsobj.fetchProductFromStoreLocation(this.ddlPrimaryItem.SelectedValue, this.ddlSubItem.SelectedValue, this.ddlStore.SelectedValue);
            if (dtObj.Rows.Count > 0)
            {
                grdStoreProdutMapping.DataSource = dtObj;
                grdStoreProdutMapping.DataBind();
            }

        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }


    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearControl();
        RemoveSession();
        clsoe();
    }

    public void loadStoreWiseProductCount()
    {
        try
        { 
            string mode = "F";
            DataTable objDt = new DataTable();
            objDt = clsobj.loadstorelocation(mode);
            if (objDt.Rows.Count > 0)
            {
                grdRptStoreWiseProduct.DataSource = objDt;
                grdRptStoreWiseProduct.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("No Data Found");
                return;
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }

    public void ClearControl()
    {
        this.grdStoreProdutMapping.DataSource = null;
        this.grdStoreProdutMapping.DataBind();
        this.ddlPrimaryItem.SelectedValue = "0";
        this.ddlStore.SelectedValue = "0";
        this.ddlSubItem.SelectedValue = "0";
    }
    public void RemoveSession()
    {
        this.Session["STOREPRODUCTMAPPING"] = null;
    }
    public void clsoe()
    {
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string storeId = string.Empty;
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblSTOREID = (Label)gvr.FindControl("lblSTOREID");
            storeId = lblSTOREID.Text.Trim();
            this.btnHide.Style["display"] = "";
            this.divProductStoreDetails.Style["display"] = "";
            DataTable objDt = new DataTable();
            objDt = clsobj.fetchProductStoreWise(storeId);
            if (objDt.Rows.Count > 0)
            {
                this.grdDetails.DataSource = objDt;
                this.grdDetails.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("Please Contact To Admin");
                return;
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }

    protected void btnHideGrid_Click(object sender, EventArgs e)
    {
        this.divProductStoreDetails.Style["display"] = "none";
        this.btnHide.Style["display"] = "none";
        this.grdDetails.DataSource = null;
        this.grdDetails.DataBind();
    }

    protected void ddlPrimaryItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.loadSubItemType();
    }
    public void loadSubItemType()
    {
        try
        {
            String primaryItem = this.ddlPrimaryItem.SelectedValue;
            DataTable dtobj = new DataTable();
            dtobj = clsobj.loadSubItem(primaryItem,"");
            if (dtobj.Rows.Count > 0)
            {

                this.ddlSubItem.Items.Clear();
                this.ddlSubItem.Items.Add(new ListItem("Select All", "0"));
                this.ddlSubItem.AppendDataBoundItems = true;
                this.ddlSubItem.DataSource = dtobj;
                this.ddlSubItem.DataTextField = "CATNAME";
                this.ddlSubItem.DataValueField = "CATID";
                this.ddlSubItem.DataBind();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void ddlMappedOrNot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(this.ddlMappedOrNot.SelectedValue=="N")
        {
            this.divHide.Visible = false;
            this.ddlPrimaryItem.SelectedValue = "0";
            this.ddlSubItem.SelectedValue = "0";
        }
        else if (this.ddlMappedOrNot.SelectedValue == "Y")
        {
            this.divHide.Visible = true;
        }

    }
}