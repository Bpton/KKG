using BAL;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmMiscellaneousProduct_FAC : System.Web.UI.Page
{
    ClsProductMaster ClsProduct = new ClsProductMaster();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () {$('#ContentPlaceHolder1_ddlVendorMap').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcustomermap').multiselect({ includeSelectAllOption: true });});</script>", false);
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                PnlDepot.Style["display"] = "none";
                LoadProductMaster();
                LoadPrimaryItem();
                LoadUOM();
                LoadUOMMapping();
                LoadType();
                LoadColor();
                LoadBrand();
                //Loadsize();
                //LoadBranch();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadProductMaster()
    {
        try
        {/*4133(Mayank Unit-2),9012(Mayank Unit-1),3873(Avik),3827(Arnab)*/
            if (Session["TPU"].ToString() == "ADMIN" || Session["UserID"].ToString() == "4133" || Session["UserID"].ToString() == "9012" || Session["UserID"].ToString() == "3873" || Session["UserID"].ToString() == "3827") // COMPANY USER
            {
                this.gvMiscellaneousProduct.DataSource = ClsProduct.BindMaterialGrid_RMPM();
                this.gvMiscellaneousProduct.DataBind();
            }
            else
            {
                this.gvMiscellaneousProduct.DataSource = ClsProduct.BindMaterialGrid_RMPM(Session["DEPOTID"].ToString());
                this.gvMiscellaneousProduct.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    public void LoadStorelocation(string id)
    {
        try
        {
            String MODE = "ProductLocation";

            if (id == "")
            {

                ClsVendor_TPU ClsProduct = new ClsVendor_TPU();
                ddlstorelocation.Items.Clear();
                ddlstorelocation.Items.Add(new ListItem("Select", "0"));
                ddlstorelocation.AppendDataBoundItems = true;
                ddlstorelocation.DataSource = ClsProduct.BindGateEntry(MODE, "");
                ddlstorelocation.DataValueField = "ID";
                ddlstorelocation.DataTextField = "NAME";
                ddlstorelocation.DataBind();
            }
            else
            {
                DataTable DT = new DataTable();
                ClsVendor_TPU ClsProduct = new ClsVendor_TPU();
                DT = ClsProduct.BindGateEntry(MODE, id);
                ddlstorelocation.Items.Clear();
                ddlstorelocation.Items.Add(new ListItem("Select", "0"));
                ddlstorelocation.AppendDataBoundItems = true;
                ddlstorelocation.DataSource = DT;
                ddlstorelocation.DataValueField = "ID";
                ddlstorelocation.DataTextField = "NAME";
                ddlstorelocation.DataBind();
                if (DT.Rows.Count > 0)
                {
                    this.ddlstorelocation.SelectedValue = DT.Rows[0]["ID"].ToString();
                }

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
            DataTable dtPrimary = new DataTable();
            dtPrimary = ClsProduct.BindPrimaryitemtype();
            if (dtPrimary.Rows.Count > 0)
            {
                ddlpritype.Items.Clear();
                ddlpritype.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlpritype.AppendDataBoundItems = true;
                ddlpritype.DataSource = dtPrimary;
                ddlpritype.DataTextField = "ITEMDESC";
                ddlpritype.DataValueField = "ID";
                ddlpritype.DataBind();
            }
            else
            {
                ddlpritype.Items.Clear();
                ddlpritype.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlpritype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadType()
    {
        try
        {
            DataTable dtPrimary = new DataTable();
            dtPrimary = ClsProduct.bindtypr();
            if (dtPrimary.Rows.Count > 0)
            {
                ddltype.Items.Clear();
                ddltype.Items.Insert(0, new ListItem("--Select Type--", "0"));
                this.ddltype.AppendDataBoundItems = true;
                ddltype.DataSource = dtPrimary;
                ddltype.DataTextField = "ITNAME";
                ddltype.DataValueField = "ITID";
                ddltype.DataBind();
            }
            else
            {
                ddltype.Items.Clear();
                ddltype.Items.Insert(0, new ListItem("--Select Type--", "0"));
                this.ddltype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadBrand()
    {
        try
        {
            DataTable dtPrimary = new DataTable();
            dtPrimary = ClsProduct.bindbrand();
            if (dtPrimary.Rows.Count > 0)
            {
                ddlbrand.Items.Clear();
                ddlbrand.Items.Insert(0, new ListItem("--Select Brand--", "0"));
                this.ddlbrand.AppendDataBoundItems = true;
                ddlbrand.DataSource = dtPrimary;
                ddlbrand.DataTextField = "DIVNAME";
                ddlbrand.DataValueField = "DIVID";
                ddlbrand.DataBind();
            }
            else
            {
                ddltype.Items.Clear();
                ddltype.Items.Insert(0, new ListItem("--Select Brand--", "0"));
                this.ddltype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    //public void Loadsize()
    //{
    //    try
    //    {
    //        DataTable dtPrimary = new DataTable();
    //        dtPrimary = ClsProduct.bindsize();
    //        if (dtPrimary.Rows.Count > 0)
    //        {
    //            ddlsw.Items.Clear();
    //            ddlsw.Items.Insert(0, new ListItem("--Select Size--", "0"));
    //            this.ddlsw.AppendDataBoundItems = true;
    //            ddlsw.DataSource = dtPrimary;
    //            ddlsw.DataTextField = "ITEM_NAME";
    //            ddlsw.DataValueField = "ID";
    //            ddlsw.DataBind();
    //        }
    //        else
    //        {
    //            ddlsw.Items.Clear();
    //            ddltype.Items.Insert(0, new ListItem("--Select Size--", "0"));
    //            this.ddltype.AppendDataBoundItems = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //}

    #region btnAddSubItem_Click
    protected void btnAddSubItem_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmSubItemType.aspx?CHACKER=" + HttpUtility.UrlEncode("true")

            + "','new window', 'top=200, left=1000, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnAddColor_Click
    protected void btnAddColor_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmFragranceMaster.aspx?CHACKER=" + HttpUtility.UrlEncode("true")

            + "','new window', 'top=200, left=1000, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion


    #region btnAddsize_Click
    protected void btnAddsize_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmsizemaster.aspx?CHACKER=" + HttpUtility.UrlEncode("true")

            + "','new window', 'top=200, left=1000, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion



    #region btnAddBrand_Click
    protected void btnAddBrand_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmDivisionMaster.aspx?CHACKER=" + HttpUtility.UrlEncode("true")

            + "','new window', 'top=200, left=1000, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    protected void btnRefreshcolor_Click(object sender, EventArgs e)
    {
        LoadColor();
    }

    #region btnAddType_Click
    protected void btnAddType_Click(object sender, EventArgs e)
    {

        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmItemTypeMaster.aspx?CHACKER=" + HttpUtility.UrlEncode("true") 

            + "','new window', 'top=200, left=1000, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion
    protected void btnRefreshTYPE_Click(object sender, EventArgs e)
    {
        LoadType();
    }

   
    protected void btnRefreshBrand_Click(object sender, EventArgs e)
    {
        LoadBrand();
    }
    
     //protected void btnRefreshsw_Click(object sender, EventArgs e)
     //{
     //   Loadsize();
     //}

    public void LoadColor()
    {
        try
        {
            DataTable dtPrimary = new DataTable();
            dtPrimary = ClsProduct.bindColor();
            if (dtPrimary.Rows.Count > 0)
            {
                ddlcolor.Items.Clear();
                ddlcolor.Items.Insert(0, new ListItem("--Select Color--", "0"));
                this.ddlcolor.AppendDataBoundItems = true;
                ddlcolor.DataSource = dtPrimary;
                ddlcolor.DataTextField = "FRGNAME";
                ddlcolor.DataValueField = "FRGID";
                ddlcolor.DataBind();
            }
            else
            {
                ddlcolor.Items.Clear();
                ddlcolor.Items.Insert(0, new ListItem("--Select Color--", "0"));
                this.ddlcolor.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlpritype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpritype.SelectedValue != "0")
        {
            this.LoadSubItem();
            this.LoadVendor();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlSbtype.ClientID + "').focus(); ", true);
            // GetProductName();
        }
        else
        {
            MessageBox1.ShowInfo("Please select Primary Item Type");
        }
    }
    protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbrand.SelectedValue != "0")
        {
            
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtSize.ClientID + "').focus(); ", true);
             GetProductName();
        }
        else
        {
            MessageBox1.ShowInfo("Please select Primary Item Type");
        }
    }

    protected void txtUValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpritype.SelectedValue != "0")
        {
           // GetProductName();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtMRP.ClientID + "').focus(); ", true);
            this.txtUnitvalue.Text= txtUValue.Text;
        }
        else
        {
            MessageBox1.ShowInfo("Please select Primary Item Type");
        }

    }

    protected void ddlcolor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcolor.SelectedValue != "0")
        {
            GetProductName();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlUOM.ClientID + "').focus(); ", true);
        }
        else
        {
          
        }
    }
    
    protected void ddlSbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSbtype.SelectedValue != "0")
        {
            GetProductName();
            GetItemDetails();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlbrand.ClientID + "').focus(); ", true);
        }
        else
        {

        }
    }

    public void GetItemDetails()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            DataTable dt = new DataTable();
            dt = ClsProduct.fetchDetailsFromSubItem(this.ddlSbtype.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                this.lblHsnCode.Text = dt.Rows[0]["HSNCODE"].ToString();
                this.lblCgstPer.Text = dt.Rows[0]["CGSTPER"].ToString();
                this.lblSgstPer.Text = dt.Rows[0]["SGSTPER"].ToString();
                this.lblIgstPer.Text = dt.Rows[0]["IGSTPER"].ToString();
            }
            else
            {
                this.lblHsnCode.Text = "";
                this.lblCgstPer.Text = "";
                this.lblSgstPer.Text = "";
                this.lblIgstPer.Text = "";
            }
        }
        
        catch(Exception ex)
        {
            MessageBox1.ShowError(Convert.ToString(ex));
        }
    }


    protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUOM.SelectedValue != "0")
        {
            //GetProductName();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtUValue.ClientID + "').focus(); ", true);
        }

    }
    
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue != "0")
        {
            GetProductName();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcolor.ClientID + "').focus(); ", true);
        }

    }

    protected void txtSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSize.Text.ToString() != "0")
        {
            GetProductName();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddltype.ClientID + "').focus(); ", true);
        }

    }



    public void GetProductName()
    {
        try
        {
            if (ddlpritype.SelectedValue.ToString() != "0" && ddlSbtype.SelectedValue.ToString() == "0" && ddltype.SelectedValue.ToString() == "0" && ddlcolor.SelectedValue.ToString() == "0" && ddlbrand.SelectedValue.ToString() == "0" && txtSize.Text.ToString() == "")
            {
               
                txtCode.Text = "";
               // this.lblPRODUCTOWNER.Text = "";
                //string CATNAME = ddlCategory.SelectedItem.ToString();
                //String[] CAT = CATNAME.Split('(');
                //string NAME = CAT[0].Trim();

                //txtName.Text = ddlDivision.SelectedItem.ToString();
                //txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.MaterialProductCode(this.ddlpritype.SelectedItem.ToString(), ddlSbtype.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), this.ddlcolor.SelectedValue.ToString(), ddlUOM.SelectedValue.ToString(), txtUValue.Text.ToString());
                txtCode.Text = ProductCode;

                //if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                //{
                //    this.lblPRODUCTOWNER.Text = "R";
                //}
                //else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                //{
                //    this.lblPRODUCTOWNER.Text = " ";
                //}
                //else
                //{
                //    this.lblPRODUCTOWNER.Text = "M";
                //}
            }
            else if (ddlpritype.SelectedValue.ToString() != "0" && ddlSbtype.SelectedValue.ToString() != "0" && ddltype.SelectedValue.ToString() == "0" && ddlbrand.SelectedValue.ToString() == "0" && ddlbrand.SelectedValue.ToString() == "0" && txtSize.Text.ToString() == "0")
            {
               
                txtCode.Text = "";
                //this.lblPRODUCTOWNER.Text = "";
                //string CATNAME = ddlCategory.SelectedItem.ToString();
                //String[] CAT = CATNAME.Split('(');
                //string NAME = CAT[0].Trim();

                //txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME);
                //txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.MaterialProductCode(this.ddlpritype.SelectedItem.ToString(), ddlSbtype.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), this.ddlcolor.SelectedValue.ToString(), ddlUOM.SelectedValue.ToString(), txtUValue.Text.ToString());
                txtCode.Text = ProductCode;
                //if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                //{
                //    this.lblPRODUCTOWNER.Text = "R";
                //}
                //else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                //{
                //    this.lblPRODUCTOWNER.Text = " ";
                //}
                //else
                //{
                //    this.lblPRODUCTOWNER.Text = "M";
                //}
            }
       else if (ddlpritype.SelectedValue.ToString() != "0" && ddlSbtype.SelectedValue.ToString() != "0" && ddltype.SelectedValue.ToString() != "0" && ddlbrand.SelectedValue.ToString() == "0" && ddlbrand.SelectedValue.ToString() == "0" && txtSize.Text.ToString() == "")
            {
               
                txtCode.Text = "";
                //this.lblPRODUCTOWNER.Text = "";
                //string CATNAME = ddlCategory.SelectedItem.ToString();
                //String[] CAT = CATNAME.Split('(');
                //string NAME = CAT[0].Trim();


                //txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString());
                //txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.MaterialProductCode(this.ddlpritype.SelectedItem.ToString(), ddlSbtype.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), this.ddlcolor.SelectedValue.ToString(), ddlUOM.SelectedValue.ToString(), txtUValue.Text.ToString());
                txtCode.Text = ProductCode;
                //if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                //{
                //    this.lblPRODUCTOWNER.Text = "R";
                //}
                //else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                //{
                //    this.lblPRODUCTOWNER.Text = " ";
                //}
                //else
                //{
                //    this.lblPRODUCTOWNER.Text = "M";
                //}
            }
       else if (ddlpritype.SelectedValue.ToString() != "0" && ddlSbtype.SelectedValue.ToString() != "0" && ddltype.SelectedValue.ToString() != "0" && ddlbrand.SelectedValue.ToString() != "0" && ddlbrand.SelectedValue.ToString() == "0" && txtSize.Text.ToString() == "")
            {
               
                txtCode.Text = "";
                //this.lblPRODUCTOWNER.Text = "";
                //string CATNAME = ddlCategory.SelectedItem.ToString();
                //String[] CAT = CATNAME.Split('(');
                //string NAME = CAT[0].Trim();

                //txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString() + " " + txtUValue.Text.ToString() + " " + ddlUOM.SelectedItem.ToString());
                //txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.MaterialProductCode(this.ddlpritype.SelectedValue, ddlSbtype.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), this.ddlcolor.SelectedValue.ToString(), ddlUOM.SelectedValue.ToString(), txtUValue.Text.ToString());
                txtCode.Text = ProductCode;
                //if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                //{
                //    this.lblPRODUCTOWNER.Text = "R";
                //}
                //else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                //{
                //    this.lblPRODUCTOWNER.Text = " ";
                //}
                //else
                //{
                //    this.lblPRODUCTOWNER.Text = "M";
                //}
            }
            else
            {
                
                txtCode.Text = "";
                //this.lblPRODUCTOWNER.Text = "";
                //string CATNAME = ddlCategory.SelectedItem.ToString();
                //String[] CAT = CATNAME.Split('(');
                //string NAME = CAT[0].Trim();

                //txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString() + " " + txtUValue.Text.ToString() + " " + ddlUOM.SelectedItem.ToString());
                //txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.MaterialProductCode(this.ddlpritype.SelectedItem.ToString(), ddlSbtype.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), this.ddlcolor.SelectedValue.ToString(), ddlbrand.SelectedValue.ToString(), txtSize.Text.ToString());
                txtCode.Text = ProductCode;
                //if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                //{
                //    this.lblPRODUCTOWNER.Text = "R";
                //}
                //else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                //{
                //    this.lblPRODUCTOWNER.Text = " ";
                //}
                //else
                //{
                //    this.lblPRODUCTOWNER.Text = "M";
                //}
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
            DataTable dt = new DataTable();
            String[] PrimaryID = this.ddlpritype.SelectedValue.Trim().Split('~');
            string ID = PrimaryID[1].Trim();
            dt = ClsProduct.BindSubitemtype(ID);
            if (dt.Rows.Count > 0)
            {
                ddlSbtype.Items.Clear();
                ddlSbtype.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlSbtype.AppendDataBoundItems = true;
                ddlSbtype.DataSource = dt;
                ddlSbtype.DataTextField = "SUBITEMDESC";
                ddlSbtype.DataValueField = "SUBTYPEID";
                ddlSbtype.DataBind();
            }
            else
            {
                ddlSbtype.Items.Clear();
                ddlSbtype.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlSbtype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadUOM()
    {
        try
        {
            ddlUOM.Items.Clear();
            ddlUOM.Items.Insert(0, new ListItem("--Select UOM--", "0"));
            ddlUOM.DataSource = ClsProduct.BindUOM();
            ddlUOM.DataTextField = "UOMDESCRIPTION";
            ddlUOM.DataValueField = "UOMID";
            ddlUOM.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblHsnCode.Text = "";
            this.lblCgstPer.Text = "";
            this.lblSgstPer.Text = "";
            this.lblIgstPer.Text = "";
            txtName.Text = "";
            txtCode.Text = "";
            txtMRP.Text = "0";
            ddlpritype.SelectedValue = "0";
            ddlSbtype.SelectedValue = "0";
            ddlUOM.SelectedValue = "0";
            txtMinstooklevel.Text = "";
            txtUValue.Text = "";
            txtSize.Text = "";
            chkRefundable.Checked = false;
            ChkActive.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            this.txtAssessablepercentage.Text = "0";
            txtUnitvalue.Text = "";
            //ddlPackigsize1.SelectedValue = "0";
            //ddlPackigsize.SelectedValue = "0";
            this.ddlfactorymap.Items.Clear();
            this.ddlVendorMap.Items.Clear();
            this.ddlcustomermap.Items.Clear();
            this.RefreshBrand.Enabled = true;
            this.btnRefreshCategory.Enabled = true;
            //this.btnRefreshsw.Enabled = true;
            this.btnRefreshTYPE.Enabled = true;
            this.btnRefreshcolor.Enabled = true;
            this.txtCode.Enabled = false;
            ddlbrand.SelectedValue = "0";
            ddlcolor.SelectedValue = "0";
            ddltype.SelectedValue = "0";
            txtName.Enabled = true;
            ddlpritype.Enabled = true;
            ddlSbtype.Enabled = true;
            ddlUOM.Enabled = true;
            txtUValue.Enabled = true;
            txtAssessablepercentage.Enabled = true;
            this.ddltype.Enabled = true;
            this.ddlcolor.Enabled = true;
            this.ddlbrand.Enabled = true;
            this.txtSize.Enabled = true;

            LoadFactoryMaster();
            LoadBranch();
            BindCustomer();
            LoadStorelocation("");
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("TPUPRODUCTMAPPING");
            Session.Remove("FACTORYPRODUCTMAPPING");
            Session.Remove("CUSTOMERPRODUCTMAPPING");
            int ID = 0;
            string Mode = "";
            string Active = string.Empty;
            string Returnable = string.Empty;

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
            }
            if (this.ChkActive.Checked == true)
            {
                Active = "T";
            }
            else
            {
                Active = "F";
            }

            if (this.txtMinstooklevel.Text == "")
            {
                this.txtMinstooklevel.Text = "0";
            }

            if (txtMRP.Text == "")
            {
                txtMRP.Text = "0";
            }

            if (chkRefundable.Checked == true)
            {
                Returnable = "Y";
            }
            else
            {
                Returnable = "N";
            }

            String[] PrimaryID = this.ddlpritype.SelectedValue.Trim().Split('~');
            string PTYPE = PrimaryID[0].Trim();
            string PID = PrimaryID[1].Trim();

            if (this.txtAssessablepercentage.Text.Trim() == "")
            {
                this.txtAssessablepercentage.Text = "0";
            }

            DataTable DtMapFactory = new DataTable();
            string XmlMapFactory = string.Empty;
            if (Session["FACTORYPRODUCTMAPPING"] == null)
            {
                CreateDataTableBranch();
            }
            DtMapFactory = (DataTable)HttpContext.Current.Session["FACTORYPRODUCTMAPPING"];

            string BRID = "", BRName = "";
            var query = from ListItem item in ddlfactorymap.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                //BRID += item.Value + ',';
                //BRName += item.Text + ',';
                BRID = item.Value;
                BRName = item.Text;

                DataRow drFAC = DtMapFactory.NewRow();
                drFAC["FACTORYID"] = BRID;
                drFAC["FACTORYNAME"] = BRName;
                DtMapFactory.Rows.Add(drFAC);
                DtMapFactory.AcceptChanges();
                HttpContext.Current.Session["FACTORYPRODUCTMAPPING"] = DtMapFactory;
                XmlMapFactory = ConvertDatatableToXML(DtMapFactory);
            }

            DataTable dtTPUProduct = new DataTable();
            string xml = string.Empty;
            if (Session["TPUPRODUCTMAPPING"] == null)
            {
                CreateDataTable();
            }
            dtTPUProduct = (DataTable)HttpContext.Current.Session["TPUPRODUCTMAPPING"];
            string VENDORID = "", VENDORNAME = "";
            var QueryVendor = from ListItem item in ddlVendorMap.Items where item.Selected select item;
            foreach (ListItem item in QueryVendor)
            {
                /*VENDORID += item.Value + ',';
                VENDORNAME += item.Text + ',';*/

                VENDORID = item.Value;
                VENDORNAME = item.Text;

                DataRow DrVendor = dtTPUProduct.NewRow();
                DrVendor["VENDORID"] = VENDORID;
                DrVendor["VENDORNAME"] = VENDORNAME;
                dtTPUProduct.Rows.Add(DrVendor);
                dtTPUProduct.AcceptChanges();
                HttpContext.Current.Session["TPUPRODUCTMAPPING"] = dtTPUProduct;
                xml = ConvertDatatableToXML(dtTPUProduct);
            }

            DataTable DtCustomerProduct = new DataTable();
            string XmlMapCustomer = string.Empty;
            if (Session["CUSTOMERPRODUCTMAPPING"] == null)
            {
                CreateDataTableCustomer();
            }
            DtCustomerProduct = (DataTable)HttpContext.Current.Session["CUSTOMERPRODUCTMAPPING"];
            string CUSTOMERID = "", CUSTOMERNAME = "";
            var QueryCustomer = from ListItem item in ddlcustomermap.Items where item.Selected select item;
            foreach (ListItem item in QueryCustomer)
            {
                /*CUSTOMERID += item.Value + ',';
                CUSTOMERNAME += item.Text + ',';*/

                CUSTOMERID = item.Value;
                CUSTOMERNAME = item.Text;
                DataRow DrCustomer = DtCustomerProduct.NewRow();
                DrCustomer["CUSTOMERID"] = CUSTOMERID;
                DrCustomer["CUSTOMERNAME"] = CUSTOMERNAME;
                DtCustomerProduct.Rows.Add(DrCustomer);
                DtCustomerProduct.AcceptChanges();
                HttpContext.Current.Session["CUSTOMERPRODUCTMAPPING"] = DtCustomerProduct;
                XmlMapCustomer = ConvertDatatableToXML(DtCustomerProduct);
            }

            ID = ClsProduct.SaveMaterialMaster_RMPM(Hdn_Fld.Value, txtCode.Text.Trim(), txtName.Text.Trim(), PID.Trim(), ddlpritype.SelectedItem.ToString().Trim(),
                                                    ddlSbtype.SelectedValue.Trim(), ddlSbtype.SelectedItem.ToString().Trim(), ddlUOM.SelectedValue.Trim(),
                                                    ddlUOM.SelectedItem.ToString().Trim(), txtUValue.Text.Trim(), Convert.ToDecimal(txtMinstooklevel.Text.Trim()),
                                                    Returnable, Convert.ToDecimal(txtMRP.Text), Mode, Active, PTYPE, Convert.ToDecimal(this.txtAssessablepercentage.Text.Trim()),
                                                    HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString(), 
                                                    ddlfactorymap.SelectedValue.ToString(),BRID, ddlVendorMap.SelectedValue.ToString(), VENDORID, txtUnitvalue.Text, 
                                                    ddlPackigsize1.SelectedValue.ToString(), ddlPackigsize1.SelectedItem.ToString(),ddlPackigsize.SelectedValue.ToString(), 
                                                    ddlPackigsize.SelectedItem.ToString(), ddlcustomermap.SelectedValue.ToString(),CUSTOMERID, XmlMapFactory, xml, XmlMapCustomer, 
                                                    ddlproductowner.SelectedValue.ToString(),ddltype.SelectedValue.Trim(),ddltype.SelectedItem.ToString(),ddlcolor.SelectedValue.Trim(),ddlcolor.SelectedItem.ToString(),
                                                    ddlbrand.SelectedValue.Trim(),txtSize.Text.Trim(), this.ddlstorelocation.SelectedValue.ToString());
            if (ID >= 3)
            {
               
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadProductMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 1)
            {                
                MessageBox1.ShowSuccess("Code already exist..");
            }
            else if (ID == 2)
            {                
                MessageBox1.ShowSuccess("Name already exist..");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadProductMaster();
            this.txtAssessablepercentage.Text = "0";
            txtName.Enabled = true;
            txtCode.Enabled = false;
            ddlpritype.Enabled = true;
            ddlSbtype.Enabled = true;
            ddlUOM.Enabled = true;
            txtUValue.Enabled = true;
            txtAssessablepercentage.Enabled = true;
            this.ddlstorelocation.SelectedValue = "0";
            LoadBranch();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = ClsProduct.DeleteProductMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadProductMaster();
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
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
            if (Hdn_Fld.Value != "")
            {
                string userid = Hdn_Fld.Value.ToString();
                DataSet ds = new DataSet();
                string ProductCheck = ClsProduct.ProducttranCheck(userid);
                if (ProductCheck == "Y")
                {
                    this.ddltype.Enabled = false;
                    this.ddlcolor.Enabled = false;
                    this.ddlbrand.Enabled = false;
                    this.txtSize.Enabled = false;
                    this.RefreshBrand.Enabled = false;
                    this.btnRefreshCategory.Enabled = false;
                    //this.btnRefreshsw.Enabled = false;
                    this.btnRefreshTYPE.Enabled = false;
                    this.btnRefreshcolor.Enabled = false;
                    /*this.txtName.Enabled = false;*//*as dicuss with mr.bakula product name should be editable before live the project dated 27/08/2021(Subhendu)*/
                    this.ddlpritype.Enabled = false;
                    this.ddlSbtype.Enabled = false;
                    this.ddlUOM.Enabled = false;
                    this.txtUValue.Enabled = false;
                    this.txtAssessablepercentage.Enabled = false;
                }
                else
                {
                    this.ddltype.Enabled = true;
                    this.ddlcolor.Enabled = true;
                    this.ddlbrand.Enabled = true;
                    this.txtSize.Enabled = true;
                    this.RefreshBrand.Enabled = true;
                    this.btnRefreshCategory.Enabled = true;
                    //this.btnRefreshsw.Enabled = true;
                    this.btnRefreshTYPE.Enabled = true;
                    this.btnRefreshcolor.Enabled = true;
                    this.txtName.Enabled = true;
                    this.ddlpritype.Enabled = true;
                    this.ddlSbtype.Enabled = true;
                    this.ddlUOM.Enabled = true;
                    this.txtUValue.Enabled = true;
                    this.txtAssessablepercentage.Enabled = true;
                }

                this.LoadPrimaryItem();
               
                ds = ClsProduct.BindMaterialMasterById_RMPM(userid);
                txtCode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();//dt.Rows[0]["CODE"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();//dt.Rows[0]["NAME"].ToString();
                ddlpritype.SelectedValue = ds.Tables[0].Rows[0]["DIVID"].ToString();/*dt.Rows[0]["TYPE"].ToString().Trim() + '~' +*/ /*dt.Rows[0]["DIVID"].ToString().Trim();*/
                if (ds.Tables[0].Rows[0]["DIVNAME"].ToString().ToUpper() == "sfg".ToUpper())
                {
                    txtCode.Enabled = true;
                }
                else
                {
                    txtCode.Enabled = false;
                }
                ddltype.SelectedValue = ds.Tables[0].Rows[0]["TYPEID"].ToString();
                ddlcolor.SelectedValue = ds.Tables[0].Rows[0]["FRGID"].ToString();
                ddlbrand.SelectedValue = ds.Tables[0].Rows[0]["BRANDID"].ToString();
                txtSize.Text = ds.Tables[0].Rows[0]["SIZEID"].ToString();
                ddlproductowner.SelectedValue = ds.Tables[0].Rows[0]["PRODUCTOWNER"].ToString();
                LoadSubItem();
                ddlSbtype.SelectedValue = ds.Tables[0].Rows[0]["CATID"].ToString();//dt.Rows[0]["CATID"].ToString();
                GetItemDetails();
                LoadUOM();
                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOMID"].ToString();//dt.Rows[0]["UOMID"].ToString();
                LoadStorelocation(userid);
                LoadBranch();
                var myList = new List<string>(ds.Tables[0].Rows[0]["BRID"].ToString().Split(','));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int i = 0; i < ddlfactorymap.Items.Count; i++)
                        {
                            if (myList[counter] == ddlfactorymap.Items[i].Value)
                            {
                                ddlfactorymap.Items[i].Selected = true;
                            }
                        }
                    }
                }
                LoadVendor();

                var VendorList = new List<string>(ds.Tables[0].Rows[0]["VENDORID"].ToString().Split(','));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int counter = 0; counter < VendorList.Count; counter++)
                    {
                        for (int i = 0; i < ddlVendorMap.Items.Count; i++)
                        {
                            if (VendorList[counter] == ddlVendorMap.Items[i].Value)
                            {
                                ddlVendorMap.Items[i].Selected = true;
                            }
                        }
                    }
                }

                BindCustomer();
                var CustomerList = new List<string>(ds.Tables[0].Rows[0]["CUSTOMERID"].ToString().Split(','));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int counter = 0; counter < CustomerList.Count; counter++)
                    {
                        for (int i = 0; i < ddlcustomermap.Items.Count; i++)
                        {
                            if (CustomerList[counter] == ddlcustomermap.Items[i].Value)
                            {
                                ddlcustomermap.Items[i].Selected = true;
                            }
                        }
                    }
                }

                if (ds.Tables[0].Rows[0]["PACKSIZEID_FROM"].ToString() != "")
                {
                    LoadUOMMapping();
                    txtUnitvalue.Text = ds.Tables[0].Rows[0]["CONVERSIONQTY"].ToString();
                    ddlPackigsize1.SelectedValue = ds.Tables[0].Rows[0]["PACKSIZEID_FROM"].ToString();//dt.Rows[0]["UOMID"].ToString();
                    ddlPackigsize.SelectedValue = ds.Tables[0].Rows[0]["PACKSIZEID_TO"].ToString();//dt.Rows[0]["UOMID"].ToString();
                }
                txtUValue.Text = ds.Tables[0].Rows[0]["UNITVALUE"].ToString();//dt.Rows[0]["UNITVALUE"].ToString();
                txtMinstooklevel.Text = ds.Tables[0].Rows[0]["MINSTOCKLEVEL"].ToString();//dt.Rows[0]["MINSTOCKLEVEL"].ToString();

                //if (dt.Rows[0]["RETURNABLE"].ToString() == "Y")
                if (ds.Tables[0].Rows[0]["RETURNABLE"].ToString() == "Y")
                {
                    chkRefundable.Checked = true;
                }
                else
                {
                    chkRefundable.Checked = false;
                }
                txtMRP.Text = ds.Tables[0].Rows[0]["MRP"].ToString();//dt.Rows[0]["MRP"].ToString();
                //if (dt.Rows[0]["ACTIVE"].ToString() == "Active")
                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString() == "Active")
                {
                    ChkActive.Checked = true;
                }
                else
                {
                    ChkActive.Checked = false;
                }
                //if (Convert.ToString(dt.Rows[0]["ASSESSABLEPERCENT"]).Trim() == "")
                if (Convert.ToString(ds.Tables[0].Rows[0]["ASSESSABLEPERCENT"]).Trim() == "")
                {
                    this.txtAssessablepercentage.Text = "0";
                }
                else
                {
                    //this.txtAssessablepercentage.Text = Convert.ToString(dt.Rows[0]["ASSESSABLEPERCENT"]).Trim();
                    this.txtAssessablepercentage.Text = Convert.ToString(ds.Tables[0].Rows[0]["ASSESSABLEPERCENT"]).Trim();
                }
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
            }

          
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        Session.Remove("TPUPRODUCTMAPPING");
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        HttpContext.Current.Session["TPUPRODUCTMAPPING"] = dt;
        return dt;
    }
    #endregion

    #region Create DataTable Factory Mapping Structure
    public DataTable CreateDataTableBranch()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        Session.Remove("FACTORYPRODUCTMAPPING");
        dt.Columns.Add(new DataColumn("FACTORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("FACTORYNAME", typeof(string)));
        HttpContext.Current.Session["FACTORYPRODUCTMAPPING"] = dt;
        return dt;
    }
    #endregion

    #region Create Customer DataTable Structure
    public DataTable CreateDataTableCustomer()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        Session.Remove("CUSTOMERPRODUCTMAPPING");
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));
        HttpContext.Current.Session["CUSTOMERPRODUCTMAPPING"] = dt;
        return dt;
    }
    #endregion
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.LoadPrimaryItem();
    }
    protected void btnRefreshCategory_Click(object sender, EventArgs e)
    {
        LoadSubItem();
    }
    protected void btnRefreshUOM_Click(object sender, EventArgs e)
    {
        this.LoadUOM();
    }
    protected void gvMiscellaneousProduct_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Active")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadVendorbyid(string id)
    {
        try
        {
            dt = ClsProduct.BindTPUMasterGridbyid(id);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadUOMMapping()
    {
        ClsProductMaster clsproduct = new ClsProductMaster();
        try
        {
            DataTable dt = new DataTable();
            dt = clsproduct.BindUOM();
            if (dt.Rows.Count > 0)
            {
                ddlPackigsize1.Items.Clear();
                //ddlPackigsize1.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                ddlPackigsize1.AppendDataBoundItems = true;
                ddlPackigsize1.DataSource = dt;
                ddlPackigsize1.DataTextField = "UOMDESCRIPTION";
                ddlPackigsize1.DataValueField = "UOMID";
                ddlPackigsize1.DataBind();

                ddlPackigsize.Items.Clear();
                //ddlPackigsize.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                ddlPackigsize1.AppendDataBoundItems = true;
                ddlPackigsize.DataSource = dt;
                ddlPackigsize.DataTextField = "UOMDESCRIPTION";
                ddlPackigsize.DataValueField = "UOMID";
                ddlPackigsize.DataBind();
            }
            else
            {
                ddlPackigsize1.Items.Clear();
               // ddlPackigsize1.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                ddlPackigsize1.AppendDataBoundItems = true;

                ddlPackigsize.Items.Clear();
                //ddlPackigsize.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                ddlPackigsize1.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region Export to Excel
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster clsproduct = new ClsProductMaster();
            DataTable dt = clsproduct.BindMaterialGrid_RMPM("EXPORTEXCEL");
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=MaterialProductDetails.xls";
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
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion       

    public void LoadFactoryid(string id)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            dt = ClsProduct.BindFactoryMasterGridbyid(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LoadFactoryMaster()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            DataTable dtfac = new DataTable();
            dtfac = ClsProduct.BindFactoryMasterGrid();
            gvFactory.DataSource = dtfac;
            gvFactory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LoadBranch()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlfactorymap.Items.Clear();
            ddlfactorymap.DataSource = ClsProduct.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
            ddlfactorymap.DataTextField = "BRANCHNAME";
            ddlfactorymap.DataValueField = "BRID";
            ddlfactorymap.DataBind();
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
            String[] PrimaryID = this.ddlpritype.SelectedValue.Trim().Split('~');
            string PTYPE = PrimaryID[0].Trim();
            string PID = PrimaryID[1].Trim();
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlVendorMap.Items.Clear();
            ddlVendorMap.DataSource = ClsProduct.BindVendorMasterGrid(PID);
            ddlVendorMap.DataTextField = "VENDORNAME";
            ddlVendorMap.DataValueField = "VENDORID";
            ddlVendorMap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindCustomer()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            this.ddlcustomermap.Items.Clear();
            //this.ddlcustomermap.Items.Add(new ListItem("Select Customer Name", "0"));
            this.ddlcustomermap.AppendDataBoundItems = true;
            this.ddlcustomermap.DataSource = ClsProduct.LoadCustomer(Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim());
            this.ddlcustomermap.DataValueField = "CUSTOMERID";
            this.ddlcustomermap.DataTextField = "CUSTOMERNAME";
            this.ddlcustomermap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}