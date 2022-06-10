using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Obout.Grid;
using System.IO;
public partial class VIEW_frmSalesLocation : System.Web.UI.Page
{
    ClsBranchMaster ClsBrMaster = new ClsBranchMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldistrictcustmap').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlstatetransporter').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                pnlCustomerMapping.Style["display"] = "none";
                PnlTransporter.Style["display"] = "none";
                btnAddBranch.Style["display"] = "";
                LoadBranchMaster();
                LoadState();
                Hdn_Tag.Value = "O";
                LoadParentBranch();
                rdbOffice.Checked = true;
                ChkCopyAddressPIN.Checked = false;
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
                LoadAccountGroup();
                Bindheadoffice();
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void Bindheadoffice()
    {
        try
        {

            string HeadOffice = string.Empty;
            HeadOffice = ClsBrMaster.CheckHeadOffice();
            if (HeadOffice == "1")
            {
                chkMotherDepo.Visible = false;
                chkMotherDepo.Checked = true;
                chkMotherDepo.Text = "HO";
            }
            else
            {
                chkMotherDepo.Checked = true;
                chkMotherDepo.Text = "HO";
                chkMotherDepo.Visible = true;
            }

        }

        catch (Exception Ex)
        {
            string msg = Ex.Message;
        }
    }

    public void LoadParentBranch()
    {
        try
        {
            ddlParentBranch.Items.Clear();
            ddlParentBranch.Items.Insert(0, new ListItem("McNROE", "0"));
            ddlParentBranch.DataSource = ClsBrMaster.BindParentBranch(Hdn_Tag.Value);
            ddlParentBranch.DataTextField = "BRNAME";
            ddlParentBranch.DataValueField = "PARENTID";
            ddlParentBranch.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadState()
    {
        try
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));
            ddlState.DataSource = ClsBrMaster.BindState();
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_ID";
            ddlState.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadDistrict(int stateid)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ClsBrMaster.BindDistrict(stateid);
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCity(int cityid)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.DataSource = ClsBrMaster.BindCity(cityid);
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_ID";
            ddlCity.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadBranchMaster()
    {
        try
        {
            gvBranch.DataSource = ClsBrMaster.BindBranchMasterGrid();
            gvBranch.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        try
        {
            Bindheadoffice();
            LoadAccountGroup();
            txtBRCode.Text = "";
            txtBranchName.Text = "";
            txtBRPrefix.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            LoadState();
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            txtPIN.Text = "";
            txttinno.Text = "";
            txtcinno.Text = "";
            txtcstno.Text = "";
            ChkCopyAddressPIN.Checked = false;
            txtDeliveryaddress.Text = "";
            txtDeliveryPIN.Text = "";
            this.txtfullname.Text = "";
            rdbOffice.Checked = true;
            rdbDepo.Checked = false;
            //chkMotherDepo.Checked = false;
            ddlaccountgroup.SelectedValue = "0";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            this.chkisexportranspertoho.Checked = false;
            this.Chkisusedforexport.Checked = false;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadAccountGroup()
    {
        ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
        ddlaccountgroup.Items.Clear();
        ddlaccountgroup.Items.Insert(0, new ListItem("-Select Account Group-", "0"));
        ddlaccountgroup.DataSource = clstxmaster.BindAccountgrp();
        ddlaccountgroup.DataTextField = "grpName";
        ddlaccountgroup.DataValueField = "Code";
        ddlaccountgroup.DataBind();

    }

    protected DataTable CreateLedgerTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEBITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CREDITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("OPENBALANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCETYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONNAME", typeof(string)));

        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
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
    protected void btnBRSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
            }
            else
            {
                ViewState["mode"] = "U";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;
            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string xmlledger = string.Empty;
            string bindverndorid = string.Empty;
            int ledger = 0;
            DataTable dt = ClsBrMaster.BindGridBranch();

            string isusedforexport = string.Empty;
            if (Chkisusedforexport.Checked)
            {
                isusedforexport = "Y";
            }
            else
            {
                isusedforexport = "N";
            }
            string isexportranspertoho = string.Empty;
            if (chkisexportranspertoho.Checked)
            {
                isexportranspertoho = "Y";
            }
            else
            {
                isexportranspertoho = "N";
            }


            int parentID = ddlParentBranch.SelectedIndex;
            int maxValue = ddlParentBranch.Items.Cast<ListItem>().Select(item => int.Parse(item.Value)).Max();
            int childID = maxValue + 1;
            if (txtMobileNo.Text == "" && txtPhoneNo.Text == "" && txtMobileNo.Text == "" && txtPhoneNo.Text == "")
            {
                txtMobileNo.Focus();
                //string message = "alert('Please insert atleast 1 contact info')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Please insert atleast 1 contact info");
            }
            else
            {
                ID = ClsBrMaster.SaveBranchMaster(Hdn_Fld.Value, parentID, childID, txtBRCode.Text.ToString(), txtBranchName.Text.ToString(), txtDescription.Text.ToString(),
                txtBRPrefix.Text.ToString(), Mode, Hdn_Tag.Value, chkMotherDepo.Checked.ToString(), txtAddress.Text.ToString(), txtDeliveryaddress.Text.ToString(), txtPhoneNo.Text.ToString(),
                txtMobileNo.Text.ToString(), (Convert.ToInt32(ddlState.SelectedValue)), (Convert.ToInt32(ddlDistrict.SelectedValue)), (Convert.ToInt32(ddlCity.SelectedValue)), txtPIN.Text.ToString(), txtDeliveryPIN.Text.ToString(),
                txtEmail.Text.ToString(), txtPhoneNo1.Text.ToString(), txtMobileNo1.Text.ToString(), this.txtfullname.Text.Trim(),
                ddlaccountgroup.SelectedValue, ddlaccountgroup.SelectedItem.Text.Trim(), txttinno.Text.Trim(), txtcinno.Text.Trim(), txtcstno.Text.Trim(),
               isusedforexport.Trim(), isexportranspertoho.Trim());

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr = dtledger.NewRow();
                    dr["ID"] = Guid.NewGuid();
                    dr["OPENBALANCE"] = 0;
                    dr["BALANCETYPE"] = "D";
                    dr["DEBITAMOUNT"] = 0;
                    dr["CREDITAMOUNT"] = 0;
                    dr["REGIONID"] = dt.Rows[j]["REGIONID"].ToString();
                    dr["REGIONNAME"] = dt.Rows[j]["REGIONNAME"].ToString();

                    dtledger.Rows.Add(dr);
                    dtledger.AcceptChanges();
                }
                bindverndorid = ClsBrMaster.Bindbranchid(txtBranchName.Text.Trim());

                xmlledger = ConvertDatatableToXML(dtledger);


                if (Hdn_Fld.Value == "")
                {
                    ViewState["mode"] = "A";
                }
                else
                {
                    ViewState["mode"] = "U";
                }
                string Modedelete = ViewState["mode"].ToString();

                ledger = ClsBrMaster.SaveAccInfo(txtBranchName.Text.Trim(), ddlaccountgroup.SelectedValue.Trim(), Session["FINYEAR"].ToString(), Hdn_Fld.Value.Trim(), xmlledger, bindverndorid.Trim(), Modedelete);

            }
            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");

                LoadBranchMaster();
                LoadParentBranch();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                ddlaccountgroup.SelectedValue = "0";
                Session["LEDGERDETAILS"] = null;
                Hdn_Tag.Value = "";
                this.chkisexportranspertoho.Checked = false;
                this.Chkisusedforexport.Checked = false;
            }
            else if (ID == 2)
            {
                //string message = "alert('Code already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Code already exist..");
            }
            else if (ID == 3)
            {
                //string message = "alert('Name already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Code already exist..");
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnBRCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtBRCode.Text = "";
            txtBranchName.Text = "";
            txtBRPrefix.Text = "";
            txtDescription.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            txttinno.Text = "";
            txtcinno.Text = "";
            txtcstno.Text = "";
            txtPIN.Text = "";
            Hdn_Fld.Value = "";
            Hdn_Tag.Value = "";
            rdbOffice.Checked = false;
            rdbDepo.Checked = false;
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            this.txtfullname.Text = "";
            ddlaccountgroup.SelectedValue = "0";
            this.chkisexportranspertoho.Checked = false;
            this.Chkisusedforexport.Checked = false;
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
            if (e.Record["BRID"] != "")
            {
                int ID = 0;
                ID = ClsBrMaster.DeleteBranchMaster(e.Record["BRID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";

                    LoadBranchMaster();
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void rdbOffice_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbOffice.Checked)
            {
                chkMotherDepo.Visible = true;
                chkMotherDepo.Checked = true;
                lihide.Visible = false;
                chkMotherDepo.Text = "HO";
                Hdn_Tag.Value = "O";
                LoadParentBranch();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void rdbDepo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbDepo.Checked)
            {
                chkMotherDepo.Visible = true;
                chkMotherDepo.Checked = true;
                lihide.Visible = false;
                chkMotherDepo.Text = "MOTHER DEPOT";
                Hdn_Tag.Value = "D";
                LoadParentBranch();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void chkMotherDepo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkMotherDepo.Checked)
            {

                lihide.Visible = false;
                Hdn_Tag.Value = "D";
                LoadParentBranch();
            }
            else
            {
                lihide.Visible = true;

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ClsBrMaster.BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.DataSource = ClsBrMaster.BindCity(Convert.ToInt32(ddlDistrict.SelectedValue));
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_ID";
            ddlCity.DataBind();
            btnAddCity.Enabled = true;
            btnRefresh.Enabled = true;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btngridsave_Click(object sender, EventArgs e)
    {

        try
        {
            string userid = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsBrMaster.BindBranchMasterById(userid);
            if (dt.Rows.Count > 0)
            {
                txtBRCode.Text = dt.Rows[0]["BRCODE"].ToString();

                txtBranchName.Text = dt.Rows[0]["BRNAME"].ToString();
                this.txtfullname.Text = dt.Rows[0]["FULLNAME"].ToString().Trim();
                txtDescription.Text = dt.Rows[0]["BRDESCRIPTION"].ToString();
                txtBRPrefix.Text = dt.Rows[0]["BRPREFIX"].ToString();
                txttinno.Text = dt.Rows[0]["TIN"].ToString();
                txtcinno.Text = dt.Rows[0]["CIN"].ToString();
                txtcstno.Text = dt.Rows[0]["CST"].ToString();
                ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                if (ddlCity.Items.Count > 1)
                {
                    ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                }
                btnAddCity.Enabled = true;
                btnRefresh.Enabled = true;

                Hdn_Tag.Value = dt.Rows[0]["BRANCHTAG"].ToString();
                //chkMotherDepo.Checked = Convert.ToBoolean(dt.Rows[0]["ISMOTHERDEPOT"].ToString());

                if (dt.Rows[0]["PARENTID"].ToString() != "0")
                {
                    this.LoadParentBranch();
                    ddlParentBranch.SelectedValue = dt.Rows[0]["PARENTID"].ToString();
                    lihide.Visible = true;
                }
                else
                {
                    ddlParentBranch.SelectedValue = "0";
                    lihide.Visible = false;
                }
                if (Hdn_Tag.Value == "O")
                {
                    rdbOffice.Checked = true;
                    rdbDepo.Checked = false;
                    chkMotherDepo.Visible = true;
                    chkMotherDepo.Checked = true;
                    chkMotherDepo.Text = "HO";
                    lihide.Visible = true;
                    if (dt.Rows[0]["ISMOTHERDEPOT"].ToString().Trim() == "True")
                    {
                        chkMotherDepo.Checked = true;

                    }
                    else
                    {
                        chkMotherDepo.Checked = false;
                    }
                }
                else if (Hdn_Tag.Value == "D")
                {
                    rdbOffice.Checked = false;
                    rdbDepo.Checked = true;
                    chkMotherDepo.Visible = true;
                    chkMotherDepo.Text = "MOTHER DEPOT";
                    lihide.Visible = false;

                    if (dt.Rows[0]["ISMOTHERDEPOT"].ToString().Trim() == "True")
                    {
                        chkMotherDepo.Checked = true;

                    }
                    else
                    {
                        chkMotherDepo.Checked = false;
                    }
                }
                txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                txtDeliveryaddress.Text = Convert.ToString(dt.Rows[0]["DELIVERYADDRESS"]).Trim();
                txtPhoneNo.Text = dt.Rows[0]["PHONENO"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                txtEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                txtPhoneNo1.Text = dt.Rows[0]["PHONENO1"].ToString();
                txtMobileNo1.Text = dt.Rows[0]["MOBILENO1"].ToString();
                txtPIN.Text = dt.Rows[0]["PINZIP"].ToString();
                txtDeliveryPIN.Text = Convert.ToString(dt.Rows[0]["DELIVERYPIN"]).Trim();
                Hdn_Fld.Value = Convert.ToString(userid);
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                if (txtDeliveryPIN.Text.Trim() == txtPIN.Text.Trim() && this.txtDeliveryaddress.Text.Trim() == this.txtAddress.Text.Trim())
                {
                    ChkCopyAddressPIN.Checked = true;
                    txtDeliveryaddress.Enabled = false;
                    txtDeliveryPIN.Enabled = false;
                }
                else
                {
                    ChkCopyAddressPIN.Checked = false;
                    txtDeliveryaddress.Enabled = true;
                    txtDeliveryPIN.Enabled = true;
                }
                if (dt.Rows[0]["ACCCODE"].ToString() != "")
                {
                    ddlaccountgroup.SelectedValue = dt.Rows[0]["ACCCODE"].ToString();
                }

                if (dt.Rows[0]["ISUSEDFOREXPORT"].ToString() == "Y")
                {
                    Chkisusedforexport.Checked = true;
                }
                else
                {
                    Chkisusedforexport.Checked = false;
                }
                if (dt.Rows[0]["ISEXPORTTRANSFERTOHO"].ToString() == "Y")
                {
                    chkisexportranspertoho.Checked = true;
                }
                else
                {
                    chkisexportranspertoho.Checked = false;
                }

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddCity_Click(object sender, EventArgs e)
    {

        try
        {
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmAddCity.aspx?stateid=" + HttpUtility.UrlEncode(ddlState.SelectedValue) + "&distid=" + HttpUtility.UrlEncode(ddlDistrict.SelectedValue)

            + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);



        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlDistrict_SelectedIndexChanged(sender, e);
    }

    protected void ChkCopyAddressPIN_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkCopyAddressPIN.Checked)
        {
            txtDeliveryaddress.Enabled = false;
            txtDeliveryPIN.Enabled = false;
            txtDeliveryaddress.Text = txtAddress.Text;
            txtDeliveryPIN.Text = txtPIN.Text;
            txtEmail.Focus();
        }
        else
        {
            txtDeliveryaddress.Enabled = true;
            txtDeliveryPIN.Enabled = true;
            txtDeliveryaddress.Text = "";
            txtDeliveryPIN.Text = "";
            txtDeliveryaddress.Focus();
        }

    }


    //==============ADD BY SUBHODIP DE ON 16.09.2016=========//


    # region DEPOT CUSTOMER MAPPING


    # region Customer mapping click
    protected void btnDepotCustMapping_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlCustomerMapping.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            LoadStateCustmap();
            CreateDataTable();
            LoadOrderType();
            if (ddlordertype.SelectedValue.Trim() == "9A555D40-5E12-4F5C-8EE0-E085B5BAB169")
            {
                trcountry.Visible = false;
                trstate.Visible = true;
            }
            else
            {
                trstate.Visible = false;
                trcountry.Visible = true;
            }

            DataTable dtedit = new DataTable();
            if (Session["CUSTOMERMAPPINGDETAILS"] != null)
            {
                dtedit = (DataTable)HttpContext.Current.Session["CUSTOMERMAPPINGDETAILS"];
            }
            ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
            DataTable dt = ClsBrMaster.Bindsaveddepotbycustid(Hdn_Fld.Value);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtedit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["DEPOTID"] = Convert.ToString(dt.Rows[i]["DEPOTID"]).Trim();
                    dr["DEPOTNAME"] = Convert.ToString(dt.Rows[i]["DEPOTNAME"]).Trim();
                    dr["CUSTOMERTYPE"] = Convert.ToString(dt.Rows[i]["CUSTOMERTYPE"]).Trim();
                    dr["CUSTOMERID"] = Convert.ToString(dt.Rows[i]["CUSTOMERID"]).Trim();
                    dr["CUSTOMERNAME"] = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Trim();
                    dtedit.Rows.Add(dr);
                    dtedit.AcceptChanges();
                }

                HttpContext.Current.Session["CUSTOMERMAPPINGDETAILS"] = dtedit;
                gvcustomerdepotmapping.DataSource = dtedit;
                gvcustomerdepotmapping.DataBind();
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
    public string ConvertDatatableToXMLCUSTMAP(DataTable dt)
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

    private void Clearall()
    {
        Hdn_Fld.Value = "";
        txtDepotname.Text = "";
        ddldistrictcustmap.Items.Clear();

    }

    public void LoadOrderType()
    {
        try
        {
            ClsBranchMaster clstax = new ClsBranchMaster();
            DataTable dt = new DataTable();
            dt = clstax.BindOrderType();
            if (dt.Rows.Count > 0)
            {

                this.ddlordertype.DataSource = dt;
                this.ddlordertype.DataValueField = "OrderTYPEID";
                this.ddlordertype.DataTextField = "ORDERTYPENAME";
                this.ddlordertype.DataBind();
            }
            else
            {
                this.ddlordertype.Items.Clear();
                this.ddlordertype.Items.Add(new ListItem("-- SELECT ORDER TYPE --", "0"));
                this.ddlordertype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadCountry()
    {
        try
        {
            ClsBranchMaster clstax = new ClsBranchMaster();
            DataTable dt = new DataTable();
            dt = clstax.BindCountry();
            if (dt.Rows.Count > 0)
            {
                this.ddlcountry.Items.Clear();
                this.ddlcountry.Items.Add(new ListItem("-- SELECT COUNTRY --", "0"));
                this.ddlcountry.AppendDataBoundItems = true;
                this.ddlcountry.DataSource = dt;
                this.ddlcountry.DataValueField = "COUNTRYID";
                this.ddlcountry.DataTextField = "COUNTRYNAME";
                this.ddlcountry.DataBind();
            }
            else
            {
                this.ddlcountry.Items.Clear();
                this.ddlcountry.Items.Add(new ListItem("-- SELECT ORDER TYPE --", "0"));
                this.ddlcountry.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    # region cancel
    protected void btnDepotCustMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clearall();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            pnlCustomerMapping.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
            this.gvCustomer.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            this.gvCustomer.ClearPreviousDataSource();
            this.gvCustomer.DataSource = null;
            this.gvCustomer.DataBind();
            this.gvcustomerdepotmapping.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            this.gvcustomerdepotmapping.ClearPreviousDataSource();
            this.gvcustomerdepotmapping.DataSource = null;
            this.gvcustomerdepotmapping.DataBind();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    # region LoadStateCustmap
    public void LoadStateCustmap()
    {
        try
        {
            ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
            DataTable dt = ClsBrMaster.BindState();
            if (dt.Rows.Count > 0)
            {
                ddlstateCustmap.Items.Clear();
                ddlstateCustmap.Items.Insert(0, new ListItem("Select", "0"));
                ddlstateCustmap.DataSource = dt;
                ddlstateCustmap.DataTextField = "State_Name";
                ddlstateCustmap.DataValueField = "State_ID";
                ddlstateCustmap.DataBind();
            }
            else
            {
                ddlstateCustmap.Items.Clear();
                ddlstateCustmap.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    # region LoadDistrictCustmap
    public void LoadDistrictCustmap(int stateid)
    {
        try
        {
            ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
            DataTable dt = ClsBrMaster.BindDistrict(stateid);
            if (dt.Rows.Count > 0)
            {
                ddldistrictcustmap.Items.Clear();
                // ddldistrictcustmap.Items.Insert(0, new ListItem("Select", "0"));
                ddldistrictcustmap.DataSource = ClsBrMaster.BindDistrict(stateid);
                ddldistrictcustmap.DataTextField = "District_Name";
                ddldistrictcustmap.DataValueField = "District_ID";
                ddldistrictcustmap.DataBind();
            }
            else
            {
                ddldistrictcustmap.Items.Clear();
                ddldistrictcustmap.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #endregion

    # region ddlstateCustmap_SelectedIndexChanged
    protected void ddlstateCustmap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlstateCustmap.SelectedValue != "0")
            {
                LoadDistrictCustmap(Convert.ToInt32(ddlstateCustmap.SelectedValue));
            }
            else
            {
                ddldistrictcustmap.Items.Clear();
                ddldistrictcustmap.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion


    # region ddlordertype_SelectedIndexChanged
    protected void ddlordertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlordertype.SelectedValue.Trim() == "9A555D40-5E12-4F5C-8EE0-E085B5BAB169")
            {
                trcountry.Visible = false;
                trstate.Visible = true;
                LoadStateCustmap();

            }
            else
            {
                trstate.Visible = false;
                trcountry.Visible = true;
                LoadCountry();
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    protected void LoadCustomergrid()
    {
        try
        {
            string districtid = "";
            if (this.ddldistrictcustmap.SelectedValue != "0")
            {
                var query1 = from ListItem item in ddldistrictcustmap.Items where item.Selected select item;

                foreach (ListItem item in query1)
                {
                    // item ...
                    districtid += item.Value + ',';

                }

                if (districtid.Length > 0)
                {
                    districtid = districtid.Substring(0, districtid.Length - 1);
                }
            }
            else
            {
                gvCustomer.DataSource = null;
                gvCustomer.DataBind();
            }


            ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
            DataTable dt = ClsBrMaster.Bindcutomergrid(Convert.ToInt32(ddlstateCustmap.SelectedValue.Trim()), districtid.Trim());
            if (dt.Rows.Count > 0)
            {
                gvCustomer.DataSource = dt;
                gvCustomer.DataBind();
            }
            else
            {
                gvCustomer.DataSource = null;
                gvCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void LoadCustomergridCountry()
    {
        try
        {

            if (this.ddlcountry.SelectedValue != "0")
            {
                ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
                DataTable dt = ClsBrMaster.BindcutomergridCOUNTRY(this.ddlcountry.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    gvCustomer.DataSource = dt;
                    gvCustomer.DataBind();
                }
                else
                {
                    gvCustomer.DataSource = null;
                    gvCustomer.DataBind();
                }
            }
            else
            {
                gvCustomer.DataSource = null;
                gvCustomer.DataBind();
            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void ddldistrictcustmap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCustomergrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCustomergridCountry();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add("GUID", typeof(string));
        dt.Columns.Add(new DataColumn("DEPOTID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERTYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));


        HttpContext.Current.Session["CUSTOMERMAPPINGDETAILS"] = dt;

        return dt;
    }
    #endregion

    # region   btnAddGrid_Click
    protected void btnAddGrid_Click(object sender, EventArgs e)
    {
        try
        {
            int countcustomer = 0;
            bool FLAG = false;
            int state = 0;
            string CUSTOMERNAME = string.Empty;
            string CUSTOMERID = string.Empty;
            DataTable dtadd = new DataTable();
            if (Session["CUSTOMERMAPPINGDETAILS"] != null)
            {
                dtadd = (DataTable)HttpContext.Current.Session["CUSTOMERMAPPINGDETAILS"];
            }

            if (dtadd.Rows.Count == 0)
            {
                if (gvCustomer.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvCustomer.SelectedRecords)
                    {
                        countcustomer = countcustomer + 1;
                        DataRow dr = dtadd.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["DEPOTID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                        dr["DEPOTNAME"] = Convert.ToString(this.txtDepotname.Text);
                        dr["CUSTOMERTYPE"] = Convert.ToString(row["CUSTYPE_NAME"]);
                        dr["CUSTOMERID"] = Convert.ToString(row["CUSTOMERID"]);
                        dr["CUSTOMERNAME"] = Convert.ToString(row["CUSTOMERNAME"]);
                        dtadd.Rows.Add(dr);
                        dtadd.AcceptChanges();
                    }
                }
                if (countcustomer == 0)
                {
                    //string message = "alert('Please select atleast 1 City')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Please select atleast 1 Cutomer'");
                }
                else
                {

                    this.ddlstateCustmap.SelectedValue = "0";
                    this.gvCustomer.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvCustomer.ClearPreviousDataSource();
                    this.gvCustomer.DataSource = null;
                    this.gvCustomer.DataBind();
                    ddldistrictcustmap.Items.Clear();
                }
            }
            else if (dtadd.Rows.Count > 0)
            {
                FLAG = false;
                CUSTOMERNAME = "";
                if (gvCustomer.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvCustomer.SelectedRecords)
                    {
                        for (int k = 0; k < gvcustomerdepotmapping.Rows.Count; k++)
                        {
                            CUSTOMERID = gvcustomerdepotmapping.Rows[k].Cells[4].Text.ToString();
                            countcustomer = countcustomer + 1;
                            if (CUSTOMERID == Convert.ToString(row["CUSTOMERID"]))
                            {
                                FLAG = true;
                                state = 2;
                                CUSTOMERNAME = CUSTOMERNAME + "," + Convert.ToString(row["CUSTOMERNAME"]);
                                break;
                            }
                        }

                        if (FLAG == false)
                        {
                            DataRow dr = dtadd.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["DEPOTID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                            dr["DEPOTNAME"] = Convert.ToString(this.txtDepotname.Text);
                            dr["CUSTOMERTYPE"] = Convert.ToString(row["CUSTYPE_NAME"]);
                            dr["CUSTOMERID"] = Convert.ToString(row["CUSTOMERID"]);
                            dr["CUSTOMERNAME"] = Convert.ToString(row["CUSTOMERNAME"]);
                            dtadd.Rows.Add(dr);
                            dtadd.AcceptChanges();
                        }
                    }
                }

                this.gvCustomer.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvCustomer.ClearPreviousDataSource();
                this.gvCustomer.DataSource = null;
                this.gvCustomer.DataBind();
                ddldistrictcustmap.Items.Clear();
                this.ddlstateCustmap.SelectedValue = "0";


                if (countcustomer == 0)
                {
                    //string message = "alert('Please select atleast 1 city')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Please select atleast 1 Customer");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            //string message = "alert('" + CITYNAME + " already exists')";
                            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            MessageBox1.ShowSuccess("" + CUSTOMERNAME + " already exists");
                        }
                    }
                    else
                    {


                        this.gvCustomer.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                        this.gvCustomer.ClearPreviousDataSource();
                        this.gvCustomer.DataSource = null;
                        this.gvCustomer.DataBind();
                        ddldistrictcustmap.Items.Clear();
                        this.ddlstateCustmap.SelectedValue = "0";
                    }


                }


            }
            if (dtadd.Rows.Count > 0)
            {
                gvcustomerdepotmapping.DataSource = dtadd;
                gvcustomerdepotmapping.DataBind();
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #endregion

    #region   Delete
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(hdndcustomerDelete.Value);
            DataTable dtdeleteLocationrecord = new DataTable();
            dtdeleteLocationrecord = (DataTable)Session["CUSTOMERMAPPINGDETAILS"];

            DataRow[] drr = dtdeleteLocationrecord.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteLocationrecord.AcceptChanges();
            }
            this.gvcustomerdepotmapping.DataSource = dtdeleteLocationrecord;
            this.gvcustomerdepotmapping.DataBind();


            this.gvCustomer.ClearPreviousDataSource();
            this.gvCustomer.DataSource = null;
            this.gvCustomer.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region   Submit
    protected void btnDepotCustMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsBranchMaster ClsBrMaster = new ClsBranchMaster();
            string xml = string.Empty;

            int ID = 0;
            DataTable dt = new DataTable();
            if (Session["CUSTOMERMAPPINGDETAILS"] != null)
            {
                dt = (DataTable)HttpContext.Current.Session["CUSTOMERMAPPINGDETAILS"];
            }
            //if(dt.Rows.Count>0)
            //{
            xml = ConvertDatatableToXML(dt);
            ID = ClsBrMaster.SaveDepotCustMapping(Hdn_Fld.Value, xml);
            if (ID > 0)
            {
                MessageBox1.ShowSuccess("Record saved successfully..");
                Clearall();
                pnlCustomerMapping.Style["display"] = "none";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
            }
            //}

            //else
            //{
            //    MessageBox1.ShowError("Please Save at least one record");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #endregion
    //============================//


    //*****************************SOURAV MUKHERJEE ON 30/09/2016****************************************//

    #region CreateTransporterDataTable
    public DataTable CreateTransporterDataTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("GUID", typeof(string));
        dt.Columns.Add("DEPOTID", typeof(string));
        dt.Columns.Add("DEPOTNAME", typeof(string));
        dt.Columns.Add("TRANSPOTERID", typeof(string));
        dt.Columns.Add("TRANSPOTERNAME", typeof(string));
        dt.Columns.Add("TAG", typeof(string));
        HttpContext.Current.Session["TRANSPORTERMAPPING"] = dt;
        return dt;
    }
    #endregion

    # region OpenTranporterMapping
    protected void btnTransporter_Click(object sender, EventArgs e)
    {
        ClsBranchMaster clsbranch = new ClsBranchMaster();
        string userid = Hdn_Fld.Value.ToString();
        DataTable dtedit = new DataTable();
        this.Loadstatetransporter();
        dtedit = clsbranch.BindBranchMasterById(userid);
        //if (dtedit.Rows[0]["BRANCHTAG"].ToString() != "")
        //{

        this.CreateTransporterDataTable();
        this.PnlTransporter.Style["display"] = "";
        this.pnlCustomerMapping.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "none";
        DataTable dttrns = new DataTable();


        dttrns = clsbranch.BindTransporter();
        if (dttrns.Rows.Count > 0)
        {
            grdFirstGrid.DataSource = dttrns;
            grdFirstGrid.DataBind();
        }
        this.grdFinalGrid.ClearPreviousDataSource();
        this.grdFinalGrid.DataSource = null;
        this.grdFinalGrid.DataBind();
        DataTable dtTransporterEdit = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];
        DataTable dt = new DataTable();
        //string CustomerID = Convert.ToString(Hdn_Fld.Value).Trim();
        dt = clsbranch.EditTransporter(Hdn_Fld.Value);
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtTransporterEdit.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["TAG"] = Convert.ToString(Hdn_Tag.Value).Trim();
                dr["TRANSPOTERID"] = Convert.ToString(dt.Rows[i]["TRANSPOTERID"]).Trim();
                dr["TRANSPOTERNAME"] = Convert.ToString(dt.Rows[i]["TRANSPOTERNAME"]).Trim();
                dr["DEPOTID"] = Convert.ToString(dt.Rows[i]["DEPOTID"]).Trim();
                dr["DEPOTNAME"] = Convert.ToString(dt.Rows[i]["DEPOTNAME"]).Trim();
                dtTransporterEdit.Rows.Add(dr);
                dtTransporterEdit.AcceptChanges();
            }

            HttpContext.Current.Session["TRANSPORTERGROUP"] = dtTransporterEdit;
            grdFinalGrid.DataSource = dtTransporterEdit;
            grdFinalGrid.DataBind();
        }
        else
        {
            grdFinalGrid.DataSource = null;
            grdFinalGrid.DataBind();
        }
        //}
        //else
        //{
        //    MessageBox1.ShowInfo("Not Allowed.....");
        //    pnlAdd.Style["display"] = "none";
        //    pnlCustomerMapping.Style["display"] = "none";
        //    pnlDisplay.Style["display"] = "";
        //    this.PnlTransporter.Style["display"] = "none";
        //}
        grdFirstGrid.SelectedRecords = null;
    }
    #endregion

    # region btnTransCancel_Click
    protected void btnTransCancel_Click(object sender, EventArgs e)
    {
        this.pnlCustomerMapping.Style["display"] = "none";
        this.PnlTransporter.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.btnaddhide.Style["display"] = "";

    }
    #endregion

    #region btnTrnsporteradd_Click
    protected void btnTrnsporteradd_Click(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            bool FLAG = false;
            string TransporterID = string.Empty;
            int state = 0;
            string TransporterName = string.Empty;
            DataTable dt = new DataTable();

            if (HttpContext.Current.Session["TRANSPORTERMAPPING"] == null)
            {

                dt.Columns.Add("GUID", typeof(string));
                dt.Columns.Add("DEPOTID", typeof(string));
                dt.Columns.Add("DEPOTNAME", typeof(string));
                dt.Columns.Add("TRANSPOTERID", typeof(string));
                dt.Columns.Add("TRANSPOTERNAME", typeof(string));
                dt.Columns.Add("TAG", typeof(string));
                HttpContext.Current.Session["TRANSPORTERMAPPING"] = dt;
            }
            else
            {

                dt = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];

            }
            if (dt.Rows.Count == 0)
            {
                if (grdFirstGrid.SelectedRecords != null)
                {
                    foreach (Hashtable row in grdFirstGrid.SelectedRecords)
                    {
                        Count = Count + 1;
                        DataRow dr = dt.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["DEPOTID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                        dr["DEPOTNAME"] = Convert.ToString(this.txtDepot.Text);
                        dr["TRANSPOTERID"] = Convert.ToString(row["ID"]);
                        dr["TRANSPOTERNAME"] = Convert.ToString(row["NAME"]);
                        dr["TAG"] = Convert.ToString(Hdn_Tag.Value).Trim();
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }


                if (Count == 0)
                {

                    MessageBox1.ShowInfo("Please select atleast 1 Records");
                }
                else
                {


                }
            }

            else if (dt.Rows.Count > 0)
            {
                FLAG = false;
                TransporterName = "";
                if (grdFirstGrid.SelectedRecords != null)
                {
                    foreach (Hashtable row in grdFirstGrid.SelectedRecords)
                    {
                        for (int k = 0; k < grdFinalGrid.Rows.Count; k++)
                        {
                            TransporterID = grdFinalGrid.Rows[k].Cells[4].Text.ToString();
                            Count = Count + 1;
                            if (TransporterID == Convert.ToString(row["ID"]))
                            {
                                FLAG = true;
                                state = 2;
                                TransporterName = TransporterName + "," + Convert.ToString(row["NAME"]);
                                break;
                            }
                        }
                        if (FLAG == false)
                        {
                            DataRow dr = dt.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["DEPOTID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                            dr["DEPOTNAME"] = Convert.ToString(this.txtDepot.Text);
                            dr["TAG"] = Convert.ToString(Hdn_Tag.Value).Trim();
                            dr["TRANSPOTERID"] = Convert.ToString(row["ID"]);
                            dr["TRANSPOTERNAME"] = Convert.ToString(row["NAME"]);
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                }

                grdFirstGrid.SelectedRecords = null;

                if (Count == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast Record");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            TransporterName = TransporterName.Substring(0, TransporterName.Length - 1);
                            MessageBox1.ShowSuccess("" + TransporterName + " already exists", 60, 700);
                        }

                    }
                    else
                    {

                    }
                }

            }

            if (dt.Rows.Count > 0)
            {
                grdFinalGrid.DataSource = dt;
                grdFinalGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region btnTransSubmit_Click
    protected void btnTransSubmit_Click(object sender, EventArgs e)
    {
        ClsBranchMaster clsbranch = new ClsBranchMaster();

        try
        {
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["TRANSPORTERMAPPING"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;
            if (grdFinalGrid.TotalRowCount > 0)
            {
                ID = clsbranch.SaveDepotTransporterMapping(Hdn_Fld.Value, XML);
                if (dt.Rows.Count > 0)
                {
                    if (ID > 0)
                    {

                        MessageBox1.ShowSuccess("Record Saved Successfully..");
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
                        pnlCustomerMapping.Style["display"] = "none";
                        PnlTransporter.Style["display"] = "none";


                        this.grdFirstGrid.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                        this.grdFirstGrid.ClearPreviousDataSource();
                        this.grdFirstGrid.DataSource = null;
                        this.grdFirstGrid.DataBind();

                    }

                    else
                    {
                        MessageBox1.ShowError("Error on saving");
                    }
                }
            }

            else
            {
                MessageBox1.ShowError("Please select atleast 1 record");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btntransporterdelete_Click
    protected void btntransporterdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(Hdn_Fld_Delete.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["TRANSPORTERMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
            }
            this.grdFinalGrid.DataSource = dt;
            this.grdFinalGrid.DataBind();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    private void Loadstatetransporter()
    {
        try
        {

            ClsBranchMaster clsbranch = new ClsBranchMaster();
            ddlstatetransporter.Items.Clear();
            ddlstatetransporter.DataSource = clsbranch.BindState();
            ddlstatetransporter.DataTextField = "State_Name";
            ddlstatetransporter.DataValueField = "State_ID";
            ddlstatetransporter.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlstatetransporter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsBranchMaster clsbranch = new ClsBranchMaster();
            string stateid = "";
            var query = from ListItem item in ddlstatetransporter.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                stateid += item.Value + "','";

            }

            if (stateid.Length > 0)
            {
                stateid = stateid.Substring(0, stateid.Length - 3);
                DataTable dt = clsbranch.BindStateTransporter(stateid.Trim());
                if (dt.Rows.Count > 0)
                {
                    grdFirstGrid.DataSource = dt;
                    grdFirstGrid.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}