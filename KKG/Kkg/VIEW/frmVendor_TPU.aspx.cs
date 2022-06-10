using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmVendor_TPU : System.Web.UI.Page
{
    ClsVendor_TPU ObjTpu = new ClsVendor_TPU();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlTpumap').multiselect({ includeSelectAllOption: true });$('#ContentPlaceHolder1_ddlgroup').multiselect({ includeSelectAllOption: true });});</script>", false);
            if (!IsPostBack)
            {
                if (Request.QueryString["MENUID"] == "VendorMaster")
                {
                    LoadTPUMaster();
                    btnaddhide.Style["display"] = "";
                    tdExcel.Style["display"] = "none";
                }
                else
                {
                    LoadTPUMaster();
                    btnaddhide.Style["display"] = "none";
                    tdExcel.Style["display"] = "";
                }
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                pnlmapping.Style["display"] = "none";
                LoadState1();
                LoadState2();
                LoadBankName();
                LoadBranchName();
                LoadSupliedItem();
                Bindgrid();
                ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
                ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
                hdnType.Visible = true;
            }
            else
            {
                if (this.txtpanno.Text != "" && Convert.ToInt32(ddlState.SelectedValue.Trim()) > 0)
                {
                    this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue.Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadState1
    public void LoadState1()
    {
        try
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));
            ddlState.DataSource = ObjTpu.BindState();
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_ID";
            ddlState.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadState2
    public void LoadState2()
    {
        try
        {
            ddlState1.Items.Clear();
            ddlState1.Items.Insert(0, new ListItem("Select", "0"));
            ddlState1.DataSource = ObjTpu.BindState();
            ddlState1.DataTextField = "State_Name";
            ddlState1.DataValueField = "State_ID";
            ddlState1.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadDistrict1
    public void LoadDistrict1(int stateid)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ObjTpu.BindDistrict(stateid);
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadDistrict2
    public void LoadDistrict2(int stateid)
    {
        try
        {
            ddlDistrict1.Items.Clear();
            ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict1.DataSource = ObjTpu.BindDistrict(stateid);
            ddlDistrict1.DataTextField = "District_Name";
            ddlDistrict1.DataValueField = "District_ID";
            ddlDistrict1.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadCity1
    public void LoadCity1(int cityid)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.DataSource = ObjTpu.BindCity(cityid);
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_ID";
            ddlCity.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadCity2
    public void LoadCity2(int cityid)
    {
        try
        {
            ddlCity1.Items.Clear();
            ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity1.DataSource = ObjTpu.BindCity(cityid);
            ddlCity1.DataTextField = "City_Name";
            ddlCity1.DataValueField = "City_ID";
            ddlCity1.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadBankName
    public void LoadBankName()
    {
        ddlbankname.Items.Clear();
        ddlbankname.Items.Insert(0, new ListItem("Select", "0"));
        ddlbankname.DataSource = ObjTpu.BindBankName();
        ddlbankname.DataTextField = "BANKNAME";
        ddlbankname.DataValueField = "ID";
        ddlbankname.DataBind();
    }
    #endregion

    #region LoadBranchName
    public void LoadBranchName()
    {
        ddlbranchname.Items.Clear();
        ddlbranchname.Items.Insert(0, new ListItem("Select", "0"));
        ddlbranchname.DataSource = ObjTpu.BindBrachName();
        ddlbranchname.DataTextField = "BRANCH";
        ddlbranchname.DataValueField = "ID";
        ddlbranchname.DataBind();
    }
    #endregion

    #region LoadSupliedItem
    public void LoadSupliedItem()
    {
        ddlSupliedItem.Items.Clear();
        ddlSupliedItem.Items.Insert(0, new ListItem("Select", "0"));
        ddlSupliedItem.DataSource = ObjTpu.BindSupliedItem();
        ddlSupliedItem.DataTextField = "ITEM_NAME";
        ddlSupliedItem.DataValueField = "ID";
        ddlSupliedItem.DataBind();

        ddltype.Items.Clear();
        ddltype.Items.Insert(0, new ListItem("ALL", "0"));
        ddltype.DataSource = ObjTpu.BindSupliedItem();
        ddltype.DataTextField = "ITEM_NAME";
        ddltype.DataValueField = "ID";
        ddltype.DataBind();
    }
    #endregion

    #region LoadTPUMaster
    public void LoadTPUMaster()
    {
        try
        {
            if (Request.QueryString["MENUID"] == "VendorMaster")
            {
                gvTPUVendor.DataSource = ObjTpu.BindTPUGrid();
                gvTPUVendor.DataBind();
            }
            else
            {
                gvTPUVendor.DataSource = ObjTpu.BindTPUGrid();

                gvTPUVendor.Columns[1].Visible = true;
                gvTPUVendor.Columns[7].Visible = true;
                gvTPUVendor.Columns[8].Visible = true;
                gvTPUVendor.Columns[14].Visible = true;
                gvTPUVendor.Columns[15].Visible = true;
                gvTPUVendor.Columns[16].Visible = true;
                gvTPUVendor.Columns[17].Visible = true;
                gvTPUVendor.Columns[18].Visible = true;
                gvTPUVendor.Columns[19].Visible = false;
                gvTPUVendor.Columns[20].Visible = false;
                gvTPUVendor.Columns[21].Visible = false;
                gvTPUVendor.Columns[22].Visible = false;
                gvTPUVendor.AllowPageSizeSelection = true;
                gvTPUVendor.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnAddTPUVendor_Click
    protected void btnAddTPUVendor_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtCode.Text = "";
            this.txtName.Text = "";
            this.txtAddress.Text = "";
            this.txtPIN.Text = "";
            this.txtPIN1.Text = "";
            this.txtPhoneNo.Text = "";
            this.txtMobileNo.Text = "";
            this.txtEmailid.Text = "";
            this.txtcontactperson.Text = "";
            this.txtcstno.Text = "";
            this.txtvatno.Text = "";
            this.txtpanno.Text = "";
            this.txttinno.Text = "";
            this.txtbankacno.Text = "";
            this.ddlSupliedItem.SelectedValue = "0";
            this.ddlState.SelectedValue = "0";
            this.ddlDistrict1.SelectedValue = "0";
            this.ddlState1.SelectedValue = "0";
            this.ddlDistrict.SelectedValue = "0";
            this.ddlbankname.SelectedValue = "0";
            this.ddlbranchname.SelectedValue = "0";
            this.ddlCity.SelectedValue = "0";
            this.ddlCity1.SelectedValue = "0";
            this.chkActive.Checked = true;
            this.txtPhoneNo1.Text = "";
            this.txtMobileNo1.Text = "";
            this.txtSTNo.Text = "";
            this.txtIFSC.Text = "";
            this.txtAddress1.Text = "";
            this.chkgst.Checked = false;
            this.Hdn_Fld.Value = "";
            this.LoadState1();
            this.LoadState2();
            this.ddlDistrict.Items.Clear();
            this.ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlDistrict1.Items.Clear();
            this.ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlCity.Items.Clear();
            this.ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlCity1.Items.Clear();
            this.ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.hdnType.Visible = true;
            this.chkgst.Checked = false;
            this.txtgstno.Text = "";
            this.trGST.Visible = false;
            btnaddgst.Visible = false;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
            /*this.rdbFactory.Checked = false;
            this.rdbTPU.Checked = true;*/
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion    

    #region CreateLedgerTable
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
    #endregion

    protected void chkgst_CheckedChanged(object sender, EventArgs e)
    {
        if (chkgst.Checked == true)
        {
            if (this.txtpanno.Text == "")
            {
                MessageBox1.ShowInfo("Please enter PAN No");
                chkgst.Checked = false;
                return;
            }
            else
            {
                this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue.Trim()));
            }
        }
        else
        {
            this.txtgstno.Text = ""; ;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
        }
    }

    private void GetSateCode(int stateid)
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            if (chkgst.Checked == true)
            {
                string code = clstrans.BindStateCode(stateid);
                this.txtstatecode.Text = code.Trim();
                this.txtgstpanno.Text = this.txtpanno.Text.Trim();
            }
            else
            {
                this.txtstatecode.Text = "";
                this.txtgstpanno.Text = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        return sMacAddress;
    }


    #region btnTPUSubmit_Click
    protected void btnTPUSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (chkgst.Checked == false)
            //{
            //    MessageBox1.ShowInfo("Please Select GST Details");
            //    return;
            //}
            string MACAddress = GetMACAddress();
            string IPAddress = Request.UserHostAddress.ToString().Trim();

            string Mode = "";
            string GSTTAG = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
            }
            string Tag = "";
            if (RbApplicable.SelectedValue == "T")
            {
                Tag = "T";
            }
            else if (RbApplicable.SelectedValue == "A")
            {
                Tag = "A";
            }
            else
            {
                MessageBox1.ShowInfo("Please select Type");
                return;
            }

            string Tagtax = string.Empty;

            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string xmlledger = string.Empty;
            string bindverndorid = string.Empty;
            DataTable dt = ObjTpu.BindGridBranch();
            string ISTRANSFERTOHO = string.Empty;

            int ID = 0;
            string gstnno = string.Empty;

            if (chkgst.Checked)
            {
                int TOTALLENGTH = 0;
                TOTALLENGTH = ((this.txtgstpanno.Text.Length) + (this.txtstatecode.Text.Length));
                if (TOTALLENGTH < 12)
                {
                    MessageBox1.ShowInfo("Please Enter Pan no!");
                    return;
                }
                if (this.txtgstpanno.Text.Length != 10)
                {
                    MessageBox1.ShowInfo("Please Enter correct pan no!");
                    return;
                }
                else if (this.txtgstno.Text.Length != 3)
                {
                    MessageBox1.ShowInfo("Please Enter GST No!");
                    return;
                }
                else
                {
                    gstnno = string.Concat(this.txtstatecode.Text.Trim() + this.txtgstpanno.Text + this.txtgstno.Text.Trim().ToUpper());
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please Select GST Details");
                return;
            }

            if (chkgst.Checked)
            {
                GSTTAG = "Y";
            }
            else
            {
                GSTTAG = "N";
            }

            #region Vendor Tpu Map
            DataTable dtTPUVendor = new DataTable();
            string xmlTPUVendor = string.Empty;
            if (Session["VENDORTPUMAPPING"] == null)
            {
                CreateTPUDataTable();
            }
            dtTPUVendor = (DataTable)HttpContext.Current.Session["VENDORTPUMAPPING"];
            string TPUID = "", TPUNAME = "";
            var QueryTPU = from ListItem item in ddlTpumap.Items where item.Selected select item;
            foreach (ListItem item in QueryTPU)
            {
                TPUID = item.Value;
                TPUNAME = item.Text;

                DataRow DrTPUVendor = dtTPUVendor.NewRow();
                DrTPUVendor["TPUID"] = TPUID;
                DrTPUVendor["TPUNAME"] = TPUNAME;
                dtTPUVendor.Rows.Add(DrTPUVendor);
                dtTPUVendor.AcceptChanges();
                HttpContext.Current.Session["VENDORTPUMAPPING"] = dtTPUVendor;
                xmlTPUVendor = ConvertDatatableToXML(dtTPUVendor);
            }
            #endregion

            #region 
            string GROUPID = "", GROUPNAME = "";
            var QueryGroup = from ListItem item in ddlgroup.Items where item.Selected select item;
            foreach (ListItem item in QueryGroup)
            {
                GROUPID = item.Value + ',';
                GROUPNAME = item.Text + ',';
            }
            GROUPID = GROUPID.Substring(0, GROUPID.Length - 1);
            GROUPNAME = GROUPNAME.Substring(0, GROUPNAME.Length - 1);
            #endregion

            #region StateWiseMapping Details
            string XMLTPUGST = string.Empty;
            if (HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] == null)
            {
                CreateTPUGSTDataTable();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"];
            foreach (GridViewRow gvrow in gvStateWiseGst.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
                TextBox txtgstamt = (TextBox)gvrow.FindControl("txtgstamt");
                TextBox txtgstnostatewise = (TextBox)gvrow.FindControl("txtgstnostatewise");
                TextBox txtgstaddress = (TextBox)gvrow.FindControl("txtgstaddress");
                Label lblState_ID = (Label)gvrow.FindControl("lblState_ID");
                Label lblState_Name = (Label)gvrow.FindControl("lblState_Name");

                if (chkBx.Checked == true)
                {
                    DataRow dr = DT.NewRow();
                    dr["VENDORID"] = hdnTid.Value.Trim();
                    dr["VENDORNAME"] = txtName.Text.ToString();
                    dr["State_ID"] = lblState_ID.Text.ToString();
                    dr["STATENAME"] = lblState_Name.Text.ToString();
                    dr["GSTNO"] = txtgstnostatewise.Text.Trim();
                    dr["GSTADDRESS"] = txtgstaddress.Text.Trim();
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                }
            }
            HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = DT;
            XMLTPUGST = ConvertDatatableToXML(DT);

            #endregion

            string ContactAddress1 = "", ContactAddress2 = "";
            ContactAddress1 = this.txtAddress.Text.Trim();
            if (ContactAddress1.Contains("'"))
            {
                ContactAddress1 = ContactAddress1.Replace("'", "''");
            }

            ContactAddress2 = this.txtAddress1.Text.Trim();
            if (ContactAddress2.Contains("'"))
            {
                ContactAddress2 = ContactAddress2.Replace("'", "''");
            }

            ID = ObjTpu.SaveThiredPartyVendorMaster(Hdn_Fld.Value, txtCode.Text.ToString(), txtName.Text.ToString(), Convert.ToInt32(ddlSupliedItem.SelectedValue.ToString()),
                                            ddlSupliedItem.SelectedItem.Text.ToString(), (Convert.ToInt32(ddlState.SelectedValue)), ddlState.SelectedValue.ToString(),
                                            (Convert.ToInt32(ddlDistrict.SelectedValue)), ddlDistrict.SelectedItem.Text.ToString(),
                                            (Convert.ToInt32(ddlCity.SelectedValue)), ddlCity.SelectedItem.Text.ToString(), ContactAddress1,
                                            (Convert.ToInt32(ddlState1.SelectedValue)), ddlState1.SelectedValue.ToString(),
                                            (Convert.ToInt32(ddlDistrict1.SelectedValue)), ddlDistrict1.SelectedItem.Text.ToString(),
                                            (Convert.ToInt32(ddlCity1.SelectedValue)), ddlCity1.SelectedItem.Text.ToString(),
                                            ContactAddress2, txtMobileNo.Text.ToString(), txtPhoneNo.Text.ToString(),
                                            txtMobileNo1.Text.ToString(), txtPhoneNo1.Text.ToString(), txtPIN.Text.ToString(),
                                            txtPIN1.Text.ToString(), txtEmailid.Text.ToString(), txtcontactperson.Text.ToString(),
                                            txtcstno.Text.ToString(), txtvatno.Text.ToString(), txttinno.Text.ToString(),
                                            txtpanno.Text.ToString(), txtSTNo.Text.ToString(), txtbankacno.Text.ToString(),
                                            ddlbankname.SelectedValue.ToString(), ddlbankname.SelectedItem.Text.ToString(),
                                            ddlbranchname.SelectedValue.ToString(), ddlbranchname.SelectedItem.Text.ToString(),
                                            txtIFSC.Text.ToString(), Mode, "", chkActive.Checked.ToString(), Tag, GROUPID, GROUPNAME, Tagtax.Trim(),
                                            "", "", gstnno.Trim(), GSTTAG.Trim(), ISTRANSFERTOHO.Trim(), 0, IPAddress, MACAddress, xmlTPUVendor, XMLTPUGST);

            if (ID >= 1)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                BindGrid();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                txtAddress1.Text = "";
                this.chkgst.Checked = false;
                this.txtgstno.Text = "";
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string strPopup = "<script language='javascript' ID='script2'>addTab('Tax', 'frmTaxSheetMaster.aspx')</script>";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                }
            }
            Session["LEDGERDETAILS"] = null;
            HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
            HttpContext.Current.Session["VENDORTPUMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnTPUCancel_Click
    protected void btnTPUCancel_Click(object sender, EventArgs e)
    {
        try
        {
            /*this.rdbFactory.Checked = false;
            this.rdbTPU.Checked = false;*/
            txtCode.Text = "";
            txtName.Text = "";
            ddlSupliedItem.SelectedIndex = 0;
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtPIN.Text = "";
            txtPIN1.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            txtcontactperson.Text = "";
            txtcstno.Text = "";
            txtvatno.Text = "";
            txtpanno.Text = "";
            txttinno.Text = "";
            txtbankacno.Text = "";
            ddlState.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            ddlState1.SelectedValue = "0";
            ddlDistrict1.SelectedValue = "0";
            ddlbankname.SelectedValue = "0";
            ddlbranchname.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
            ddlCity1.SelectedValue = "0";
            chkActive.Checked = false;
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            txtSTNo.Text = "";
            txtIFSC.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            this.chkgst.Checked = false;
            this.txtgstno.Text = "";
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
            /*RbApplicable.Items.RemoveAt(1);
            RbApplicable.Items.RemoveAt(2);*/

            RbApplicable.Items[0].Selected = false;
            //RbApplicable.Items[1].Selected = false;
            //RbApplicable.Items.Insert(0, "TPU");
            //RbApplicable.Items.Insert(1, "ACCOUNTS");
            this.LoadTpu();
            this.LoadGroup();
            HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
            HttpContext.Current.Session["VENDORTPUMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region DeleteRecord
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (ObjTpu.GetstatusChecking(e.Record["VENDORID"].ToString()) == "1")
            {
                e.Record["Error"] = "Sorry, bill's already done against this Vendor into system..";
                return;
            }
            else
            {
                if (e.Record["VENDORID"] != "")
                {
                    int ID = 0;
                    ID = ObjTpu.DeleteTPUMaster(e.Record["VENDORID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";
                        LoadTPUMaster();
                    }
                    else
                    {
                        e.Record["Error"] = "Error On Deleting. ";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlState_SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ObjTpu.BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
            this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue));
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlDistrict.ClientID + "').focus(); ", true);
    }
    #endregion

    #region ddlState1_SelectedIndexChanged
    protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict1.Items.Clear();
            ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict1.DataSource = ObjTpu.BindDistrict(Convert.ToInt32(ddlState1.SelectedValue));
            ddlDistrict1.DataTextField = "District_Name";
            ddlDistrict1.DataValueField = "District_ID";
            ddlDistrict1.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlDistrict_SelectedIndexChanged
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.DataSource = ObjTpu.BindCity(Convert.ToInt32(ddlDistrict.SelectedValue));
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
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlCity.ClientID + "').focus(); ", true);
    }
    #endregion

    #region ddlDistrict1_SelectedIndexChanged
    protected void ddlDistrict1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCity1.Items.Clear();
            ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity1.DataSource = ObjTpu.BindCity(Convert.ToInt32(ddlDistrict1.SelectedValue));
            ddlCity1.DataTextField = "City_Name";
            ddlCity1.DataValueField = "City_ID";
            ddlCity1.DataBind();
            btnAddCity1.Enabled = true;
            btnnewRefresh.Enabled = true;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnAddCity_Click
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
    #endregion

    #region btnAddCity1_Click
    protected void btnAddCity1_Click(object sender, EventArgs e)
    {
        try
        {
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('frmAddCity.aspx?stateid=" + HttpUtility.UrlEncode(ddlState1.SelectedValue) + "&distid=" + HttpUtility.UrlEncode(ddlDistrict1.SelectedValue)

            + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnRefresh_Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlDistrict_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnnewRefresh_Click
    protected void btnnewRefresh_Click(object sender, EventArgs e)
    {
        ddlDistrict1_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnAddBranch_Click
    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"

        // Passing intId to popup window.
        + "window.open('frmAddBankbrnch.aspx?brnchid=" + HttpUtility.UrlEncode(ddlbranchname.SelectedValue)

        + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

        + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
    #endregion

    #region btnRefresh1_Click
    protected void btnRefresh1_Click(object sender, EventArgs e)
    {
        LoadBranchName();
    }
    #endregion

    #region Bindgrid
    public void Bindgrid()
    {
        String[] PrimaryID = this.ddlDivision.SelectedValue.Trim().Split('~');
        if (PrimaryID.Length > 1)
        {
            string ID = PrimaryID[1].Trim();
            gvPRODUCTMap.DataSource = ObjTpu.BindProductGrid(ID, ddlCategory.SelectedValue);
            gvPRODUCTMap.DataBind();
        }
        else
        {
            gvPRODUCTMap.DataSource = ObjTpu.BindProductGrid(ddlDivision.SelectedValue.Trim(), ddlCategory.SelectedValue);
            gvPRODUCTMap.DataBind();
        }
    }
    #endregion

    #region LoadDivisionName
    public void LoadDivisionName()
    {
        ddlDivision.Items.Clear();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        ddlDivision.DataSource = ObjTpu.BindDivision();
        ddlDivision.DataTextField = "DIVNAME";
        ddlDivision.DataValueField = "DIVID";
        ddlDivision.DataBind();
    }
    #endregion

    #region LoadCategoryName
    public void LoadCategoryName()
    {
        ddlCategory.Items.Clear();
        ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        ddlCategory.DataSource = ObjTpu.BindCategory();
        ddlCategory.DataTextField = "CATNAME";
        ddlCategory.DataValueField = "CATID";
        ddlCategory.DataBind();
    }
    #endregion

    #region ddlSupliedItem_SelectedIndexChanged
    protected void ddlSupliedItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupliedItem.SelectedValue == "1")
        {
            hdnType.Visible = true;
        }
        else
        {
            hdnType.Visible = true;
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtCode.ClientID + "').focus(); ", true);
    }
    #endregion

    #region btngridedit_Click
    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnTid.Value == "E")
            {
                trGST.Visible = false;
                btnaddgst.Visible = true;
                string userid = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();
                dt = ObjTpu.BindTPUMasterById(userid);
                if (dt.Rows.Count > 0)
                {
                    txtCode.Text = dt.Rows[0]["CODE"].ToString();
                    txtName.Text = dt.Rows[0]["VENDORNAME"].ToString();
                    ddlSupliedItem.SelectedValue = dt.Rows[0]["SUPLIEDITEMID"].ToString();
                    if (dt.Rows[0]["SUPLIEDITEMID"].ToString() == "1")
                    {
                        hdnType.Visible = true;
                    }
                    else
                    {
                        hdnType.Visible = true;
                    }
                    if (dt.Rows[0]["TAG"].ToString() == "T")
                    {
                        /*rdbTPU.Checked = true;
                        rdbFactory.Checked = false;*/
                        RbApplicable.SelectedValue = "T";
                        //lblApplicable.Text = "TPU";
                        /*ddlgroup.Visible = false;
                        RbApplicable.Items.RemoveAt(1);*/
                    }
                    else
                    {
                        /*rdbFactory.Checked = true;
                        rdbTPU.Checked = false;*/
                        RbApplicable.SelectedValue = "A";
                        lblApplicable.Text = "ACCOUNTS";
                        /*ddlTpumap.Visible = false;
                        RbApplicable.Items.RemoveAt(0);*/
                    }

                    if (ddlState.Items.FindByValue(Convert.ToString(dt.Rows[0]["STATEID"])) != null)
                    {
                        ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                    }
                    LoadDistrict1(Convert.ToInt32(ddlState.SelectedValue));
                    if (ddlDistrict.Items.FindByValue(Convert.ToString(dt.Rows[0]["DISTRICTID"])) != null)
                    {
                        ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                    }
                    LoadCity1(Convert.ToInt32(ddlDistrict.SelectedValue));
                    if (ddlCity.Items.FindByValue(Convert.ToString(dt.Rows[0]["CITYID"])) != null)
                    {
                        ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                    }
                    txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();

                    //ddlState1.SelectedValue = dt.Rows[0]["STATEID1"].ToString();
                    //LoadDistrict2(Convert.ToInt32(ddlState1.SelectedValue));
                    //ddlDistrict1.SelectedValue = dt.Rows[0]["DISTRICTID1"].ToString();
                    //LoadCity2(Convert.ToInt32(ddlDistrict1.SelectedValue));
                    //if (ddlCity1.Items.Count > 1)
                    //{
                    //    ddlCity1.SelectedValue = dt.Rows[0]["CITYID1"].ToString();
                    //}
                    txtAddress1.Text = dt.Rows[0]["ADDRESS1"].ToString();
                    txtPhoneNo.Text = dt.Rows[0]["PHONENO"].ToString();
                    txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                    txtPhoneNo1.Text = dt.Rows[0]["PHONENO1"].ToString();
                    txtMobileNo1.Text = dt.Rows[0]["MOBILENO1"].ToString();
                    txtPIN.Text = dt.Rows[0]["PINZIP"].ToString();
                    txtPIN1.Text = dt.Rows[0]["PINZIP1"].ToString();
                    txtEmailid.Text = dt.Rows[0]["EMAILID"].ToString();
                    txtcontactperson.Text = dt.Rows[0]["CONTACTPERSON"].ToString();
                    txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
                    txtvatno.Text = dt.Rows[0]["VATNO"].ToString();
                    txttinno.Text = dt.Rows[0]["TINNO"].ToString();
                    txtpanno.Text = dt.Rows[0]["PANNO"].ToString();
                    txtSTNo.Text = dt.Rows[0]["STNO"].ToString();
                    txtbankacno.Text = dt.Rows[0]["BANKACNO"].ToString();
                    txtIFSC.Text = dt.Rows[0]["IFSCCODE"].ToString();
                    LoadBankName();
                    ddlbankname.SelectedValue = dt.Rows[0]["BANKID"].ToString();
                    this.LoadBranchName();
                    ddlbranchname.SelectedValue = dt.Rows[0]["BRANCHID"].ToString();

                    this.LoadTpu();
                    var myList = new List<string>(dt.Rows[0]["TPUID"].ToString().Split(','));
                    if (dt.Rows.Count > 0)
                    {
                        for (int counter = 0; counter < myList.Count; counter++)
                        {
                            for (int i = 0; i < ddlTpumap.Items.Count; i++)
                            {
                                if (myList[counter] == ddlTpumap.Items[i].Value)
                                {
                                    ddlTpumap.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    this.LoadGroup();
                    var MyListGroup = new List<string>(dt.Rows[0]["ACCCODE"].ToString().Split(','));
                    if (dt.Rows.Count > 0)
                    {
                        for (int counter = 0; counter < MyListGroup.Count; counter++)
                        {
                            for (int i = 0; i < ddlgroup.Items.Count; i++)
                            {
                                if (MyListGroup[counter] == ddlgroup.Items[i].Value)
                                {
                                    ddlgroup.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    if (dt.Rows[0]["ISAPPROVED"].ToString() == "Y")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    if (dt.Rows[0]["APPLICABLEGST"].ToString() == "Y")
                    {
                        chkgst.Checked = true;
                        string GSTNO = dt.Rows[0]["GSTNO"].ToString();

                        if (GSTNO != "")
                        {
                            if (GSTNO.Substring(0, 2).Length == 2)
                            {
                                this.txtstatecode.Text = GSTNO.Substring(0, 2);
                            }
                            else
                            {
                                this.txtstatecode.Text = "";
                            }

                            if (GSTNO.Substring(2, 10).Length == 10)
                            {
                                this.txtgstpanno.Text = GSTNO.Substring(2, 10);
                            }
                            else
                            {
                                this.txtgstpanno.Text = "";
                            }
                            if (GSTNO.Substring(12, 3).Length == 3)
                            {
                                this.txtgstno.Text = GSTNO.Substring(12, 3);
                            }
                            else
                            {
                                this.txtgstno.Text = "";
                            }
                        }
                    }
                    else
                    {
                        chkgst.Checked = false;
                    }
                }
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                pnlmapping.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                BindState();
                CreateTPUGSTDataTable();
            }
            else
            {
                string Supplied = Hdn_Supplied.Value;
                if (Supplied == "1")
                {
                    this.lblDiv.Text = "BRAND";
                    this.lblCategory.Text = "CATEGORY";
                    this.LoadDivisionName();
                    this.LoadCategoryName();
                }
                else
                {
                    this.lblDiv.Text = "PRIMARY ITEM";
                    this.lblCategory.Text = "SUB ITEM";
                    this.LoadPrimaryItem();
                    this.LoadSubItem();
                }
                this.CreateDataTable();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                pnlmapping.Style["display"] = "";
                btnaddhide.Style["display"] = "none";
                DataTable dtVendorProductMappingDetails = new DataTable();
                DataSet ds = new DataSet();
                if (Session["TPUPRODUCTMAPPING"] != null)
                {
                    dtVendorProductMappingDetails = (DataTable)Session["TPUPRODUCTMAPPING"];
                }
                ds = ObjTpu.ProductMappingDetails(Convert.ToString(this.Hdn_Fld.Value));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dtVendorProductMappingDetails.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["DIVISIONID"] = Convert.ToString(ds.Tables[0].Rows[i]["DIVISIONID"]).Trim();
                        dr["DIVISIONNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["DIVISIONNAME"]).Trim();
                        dr["CATEGORYID"] = Convert.ToString(ds.Tables[0].Rows[i]["CATEGORYID"]).Trim();
                        dr["CATEGORYNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["CATEGORYNAME"]).Trim();
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[0].Rows[i]["PRODUCTID"]).Trim();
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["PRODUCTNAME"]).Trim();
                        dr["VENDORID"] = Convert.ToString(ds.Tables[0].Rows[i]["VENDORID"]).Trim();
                        dr["VENDORNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["VENDORNAME"]).Trim();
                        dtVendorProductMappingDetails.Rows.Add(dr);
                        dtVendorProductMappingDetails.AcceptChanges();
                    }
                }
                HttpContext.Current.Session["TPUPRODUCTMAPPING"] = dtVendorProductMappingDetails;
                this.gvProduct.DataSource = dtVendorProductMappingDetails;
                this.gvProduct.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        HttpContext.Current.Session["TPUPRODUCTMAPPING"] = dt;
        return dt;
    }
    #endregion

    #region TPU-Product Mapping
    protected void btnPRODUCTSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtTPUProduct = (DataTable)HttpContext.Current.Session["TPUPRODUCTMAPPING"];
            int success = 0;
            string xml = string.Empty;
            xml = ConvertDatatableToXML(dtTPUProduct);
            success = ObjTpu.SaveProductMapping(Convert.ToString(this.Hdn_Fld.Value), this.txtTname.Text.Trim(), Convert.ToString(this.hdnTag.Value), xml);
            if (success > 0)
            {
                this.ResetControls();
                this.LoadTPUMaster();
                //string message = "alert('Record Saved Successfully')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
            }
            else
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                pnlmapping.Style["display"] = "";
                btnaddhide.Style["display"] = "none";
                //string message = "alert('Error on Saving record..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Error on Saving record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ResetControls
    protected void ResetControls()
    {
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        pnlmapping.Style["display"] = "none";
        btnaddhide.Style["display"] = "";
        this.txtTname.Text = "";
        this.ddlCategory.SelectedValue = "0";
        this.ddlDivision.SelectedValue = "0";
        Session["TPUPRODUCTMAPPING"] = null;
        this.gvPRODUCTMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        this.gvPRODUCTMap.ClearPreviousDataSource();
        this.gvPRODUCTMap.DataSource = null;
        this.gvPRODUCTMap.DataBind();
        this.gvProduct.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        this.gvProduct.ClearPreviousDataSource();
        this.gvProduct.DataSource = null;
        this.gvProduct.DataBind();
        Hdn_Fld.Value = "";
        hdnTid.Value = "";
        hdnTag.Value = "";
    }
    #endregion

    #region btnPRODUCTCancel_Click
    protected void btnPRODUCTCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    #endregion

    #region  AddProduct To TPU
    protected void btnAddGrid_Click(object sender, EventArgs e)
    {
        int CountProduct = 0;
        bool FLAG = false;
        int Counter = 0;
        int state = 0;
        string PRODUCTID = string.Empty;
        string PRODUCTNAME = string.Empty;
        DataTable dtVendorProductMappingDetails = new DataTable();
        if (Session["TPUPRODUCTMAPPING"] != null)
        {
            dtVendorProductMappingDetails = (DataTable)Session["TPUPRODUCTMAPPING"];
        }

        if (dtVendorProductMappingDetails.Rows.Count == 0)
        {
            string hdnProductID = string.Empty;
            string hdnProductName = string.Empty;
            if (gvPRODUCTMap.SelectedRecords != null)
            {
                foreach (Hashtable row in gvPRODUCTMap.SelectedRecords)
                {
                    CountProduct = CountProduct + 1;
                    DataRow dr = dtVendorProductMappingDetails.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["DIVISIONID"] = Convert.ToString(this.ddlDivision.SelectedValue);
                    dr["DIVISIONNAME"] = Convert.ToString(this.ddlDivision.SelectedItem).Trim();
                    dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue);
                    dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                    dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                    dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                    dr["VENDORID"] = Convert.ToString(this.Hdn_Fld.Value);
                    dr["VENDORNAME"] = Convert.ToString(this.txtTname.Text.Trim());
                    dtVendorProductMappingDetails.Rows.Add(dr);
                    dtVendorProductMappingDetails.AcceptChanges();
                }
            }
            if (CountProduct == 0)
            {
                string message = "alert('Please select atleast 1 Product')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                this.ddlCategory.SelectedValue = "0";
                this.ddlDivision.SelectedValue = "0";
                this.gvPRODUCTMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvPRODUCTMap.ClearPreviousDataSource();
                this.gvPRODUCTMap.DataSource = null;
                this.gvPRODUCTMap.DataBind();
            }
        }

        else if (dtVendorProductMappingDetails.Rows.Count > 0)
        {
            FLAG = false;
            PRODUCTNAME = "";
            string hdnProductID = string.Empty;
            string hdnProductName = string.Empty;
            if (gvPRODUCTMap.SelectedRecords != null)
            {
                foreach (Hashtable row in gvPRODUCTMap.SelectedRecords)
                {
                    for (int k = 0; k < gvProduct.Rows.Count; k++)
                    {
                        PRODUCTID = gvProduct.Rows[k].Cells[6].Text;
                        CountProduct = CountProduct + 1;
                        if (PRODUCTID == Convert.ToString(row["ID"]))
                        {
                            FLAG = true;
                            state = 2;
                            PRODUCTNAME = PRODUCTNAME + "," + Convert.ToString(row["NAME"]);
                            break;
                        }
                    }
                    if (FLAG == false)
                    {
                        DataRow dr = dtVendorProductMappingDetails.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["DIVISIONID"] = Convert.ToString(this.ddlDivision.SelectedValue);
                        dr["DIVISIONNAME"] = Convert.ToString(this.ddlDivision.SelectedItem).Trim();
                        dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue);
                        dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem).Trim();
                        dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                        dr["VENDORID"] = Convert.ToString(this.Hdn_Fld.Value);
                        dr["VENDORNAME"] = Convert.ToString(this.txtTname.Text.Trim());
                        dtVendorProductMappingDetails.Rows.Add(dr);
                        dtVendorProductMappingDetails.AcceptChanges();
                    }
                }
            }
            gvPRODUCTMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            gvPRODUCTMap.ClearPreviousDataSource();
            gvPRODUCTMap.DataSource = null;
            gvPRODUCTMap.DataBind();
            this.ddlDivision.SelectedValue = "0";
            this.ddlCategory.SelectedValue = "0";
            if (CountProduct == 0)
            {
                string message = "alert('Please select atleast 1 Product')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                if (FLAG == true)
                {
                    if (state == 2)
                    {
                        string message = "alert('" + PRODUCTNAME + " already exists')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }
                else
                {
                    this.ddlCategory.SelectedValue = "0";
                    this.ddlDivision.SelectedValue = "0";
                    this.gvPRODUCTMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvPRODUCTMap.ClearPreviousDataSource();
                    this.gvPRODUCTMap.DataSource = null;
                    this.gvPRODUCTMap.DataBind();
                }
            }
        }

        if (dtVendorProductMappingDetails.Rows.Count > 0)
        {
            this.gvProduct.DataSource = dtVendorProductMappingDetails;
            this.gvProduct.DataBind();
        }
    }
    #endregion

    #region btngrddelete_Click
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string ProductGUID = Convert.ToString(hdnProductDelete.Value);
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["TPUPRODUCTMAPPING"];

            DataRow[] drr = dtdeleterecord.Select("GUID='" + ProductGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleterecord.AcceptChanges();
            }
            this.gvProduct.DataSource = dtdeleterecord;
            this.gvProduct.DataBind();

            Session["dtMap"] = null;
            this.gvPRODUCTMap.ClearPreviousDataSource();
            this.gvPRODUCTMap.DataSource = null;
            this.gvPRODUCTMap.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region gvTPUVendor_RowDataBound
    protected void gvTPUVendor_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[18] as GridDataControlFieldCell;
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
    #endregion

    #region LoadPrimaryItem
    public void LoadPrimaryItem()
    {
        try
        {
            ClsTPUVendor ClsProduct = new ClsTPUVendor();
            DataTable dtPrimary = new DataTable();
            dtPrimary = ClsProduct.BindPrimaryitemtype();
            if (dtPrimary.Rows.Count > 0)
            {
                ddlDivision.Items.Clear();
                ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlDivision.AppendDataBoundItems = true;
                ddlDivision.DataSource = dtPrimary;
                ddlDivision.DataTextField = "ITEMDESC";
                ddlDivision.DataValueField = "ID";
                ddlDivision.DataBind();
            }
            else
            {
                ddlDivision.Items.Clear();
                ddlDivision.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlDivision.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlDivision_SelectedIndexChanged
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "0")
        {
            this.LoadSubItem();
        }
        else
        {
            MessageBox1.ShowInfo("Please select Primary Item Type");
        }
    }
    #endregion

    #region LoadSubItem
    public void LoadSubItem()
    {
        try
        {
            ClsTPUVendor ClsProduct = new ClsTPUVendor();
            DataTable dt = new DataTable();
            String[] PrimaryID = this.ddlDivision.SelectedValue.Trim().Split('~');
            string ID = PrimaryID[1].Trim();
            dt = ClsProduct.BindSubitemtype(ID);
            if (dt.Rows.Count > 0)
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlCategory.AppendDataBoundItems = true;
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
            string msg = ex.Message;
        }
    }
    #endregion

    #region gvTPUVendor_Exporting
    protected void gvTPUVendor_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 14;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Vendor Details";
        cell.BackColor = Color.Gray;
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.RowSpan = 1;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region gvTPUVendor_Exported
    protected void gvTPUVendor_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    #region BindGrid
    public void BindGrid()
    {
        try
        {
            if (Request.QueryString["MENUID"] == "VendorMaster")
            {
                /*gvTPUVendor.DataSource = ObjTpu.BindTPUGridByType(ddltype.SelectedValue.Trim());
                gvTPUVendor.Columns[19].Visible = true;
                gvTPUVendor.Columns[20].Visible = true;
                gvTPUVendor.Columns[21].Visible = true;
                gvTPUVendor.Columns[22].Visible = true;
                gvTPUVendor.DataBind();*/
                LoadTPUMaster();
            }
            else
            {
                /*gvTPUVendor.DataSource = ObjTpu.BindTPUGridByType(ddltype.SelectedValue.Trim());
                gvTPUVendor.Columns[1].Visible = true;
                gvTPUVendor.Columns[7].Visible = true;
                gvTPUVendor.Columns[8].Visible = true;
                gvTPUVendor.Columns[14].Visible = true;
                gvTPUVendor.Columns[15].Visible = true;
                gvTPUVendor.Columns[16].Visible = true;
                gvTPUVendor.Columns[17].Visible = true;
                gvTPUVendor.Columns[18].Visible = true;
                gvTPUVendor.Columns[19].Visible = false;
                gvTPUVendor.Columns[20].Visible = false;
                gvTPUVendor.Columns[21].Visible = false;
                gvTPUVendor.Columns[22].Visible = false;
                gvTPUVendor.AllowPageSizeSelection = true;
                gvTPUVendor.DataBind();*/
                LoadTPUMaster();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Bind Tpu
    public void LoadTpu()
    {
        try
        {
            ddlTpumap.Items.Clear();
            this.ddlTpumap.Items.Add(new ListItem("Select TPU", "0"));
            ddlTpumap.DataSource = ObjTpu.BindTpu();
            ddlTpumap.DataTextField = "VENDORNAME";
            ddlTpumap.DataValueField = "VENDORID";
            ddlTpumap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Bind Group
    public void LoadGroup()
    {
        try
        {
            ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group", "0"));
            ddlgroup.DataSource = ObjTpu.BindGroupName();
            ddlgroup.DataTextField = "GROUPNAME";
            ddlgroup.DataValueField = "GROUPID";
            ddlgroup.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void RbApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (Convert.ToString(RbApplicable.SelectedValue))
        {
            case "T":
                lblApplicable.Text = "TPU";
                ddlTpumap.Visible = true;
                cvddlTpumap.Visible = true;
                ddlgroup.Visible = false;
                CVddlgroup.Visible = false;
                this.LoadTpu();
                break;
            case "A":
                lblApplicable.Text = "ACCOUNTS";
                ddlTpumap.Visible = false;
                cvddlTpumap.Visible = false;
                ddlgroup.Visible = true;
                CVddlgroup.Visible = true;
                this.LoadGroup();
                break;
        }
    }

    #region Create TPU DataTable Structure
    public DataTable CreateTPUDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        Session.Remove("VENDORTPUMAPPING");
        dt.Columns.Add(new DataColumn("TPUID", typeof(string)));
        dt.Columns.Add(new DataColumn("TPUNAME", typeof(string)));
        HttpContext.Current.Session["VENDORTPUMAPPING"] = dt;
        return dt;
    }
    #endregion

    protected void btnaddgst_Click(object sender, EventArgs e)
    {
        try
        {
            trGST.Visible = true;
            BindState();
            CreateTPUGSTDataTable();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void BindState()
    {
        try
        {
            gvStateWiseGst.DataSource = ObjTpu.BindStategst(ddlState.SelectedValue.ToString());
            gvStateWiseGst.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void gvStateWiseGst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string userid = Hdn_Fld.Value.ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dt = ObjTpu.EditSTATEWISEGSTMAPPING(userid);
                if (dt.Rows.Count > 0)
                {
                    TextBox txtgstnostatewise = (e.Row.FindControl("txtgstnostatewise") as TextBox);
                    TextBox txtgstaddress = (e.Row.FindControl("txtgstaddress") as TextBox);
                    CheckBox chkBx = (e.Row.FindControl("chkSelect") as CheckBox);

                    string lblState_ID = (e.Row.FindControl("lblState_ID") as Label).Text;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["State_ID"].ToString().Trim() == lblState_ID.Trim())
                        {
                            txtgstnostatewise.Text = Convert.ToString(dt.Rows[i]["GSTNO"]);
                            txtgstaddress.Text = Convert.ToString(dt.Rows[i]["GSTADDRESS"]);
                            chkBx.Checked = true;
                        }
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

    #region Create DataTable Structure
    public DataTable CreateTPUGSTDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("State_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("STATENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTNO", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTADDRESS", typeof(string)));
        HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = dt;
        return dt;
    }
    #endregion
}