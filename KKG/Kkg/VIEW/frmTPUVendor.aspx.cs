using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmTPUVendor : System.Web.UI.Page
{
    ClsTPUVendor ClsTPUMaster = new ClsTPUVendor();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                pnlDepotWiseGstMapping.Style["display"] = "none";
                LoadState1();
                LoadState2();
                LoadBankName();               
                LoadSupliedItem();
                Bindgrid();
                ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
                ddlCity1.Items.Insert(0, new ListItem("Select", "0"));              
                txtDistrict.Text = "";
                txtCity.Text = "";
                txtbranchname.Text = "";
                hdnType.Visible = true;
                this.LoadAccountGroup();
                this.LoadFilterAccountGroup();
                this.LoadCompnyType();
                this.LoadLedger();
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
            ddlState.DataSource = ClsTPUMaster.BindState();
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
            ddlState1.DataSource = ClsTPUMaster.BindState();
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

   

    #region LoadDistrict2
    public void LoadDistrict2(int stateid)
    {
        try
        {
            ddlDistrict1.Items.Clear();
            ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict1.DataSource = ClsTPUMaster.BindDistrict(stateid);
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

   

    #region LoadCity2
    public void LoadCity2(int cityid)
    {
        try
        {
            ddlCity1.Items.Clear();
            ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity1.DataSource = ClsTPUMaster.BindCity(cityid);
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
        ddlbankname.DataSource = ClsTPUMaster.BindBankName();
        ddlbankname.DataTextField = "BANKNAME";
        ddlbankname.DataValueField = "ID";
        ddlbankname.DataBind();
    }
    #endregion

   

    #region LoadSupliedItem
    public void LoadSupliedItem()
    {
        ddlSupliedItem.Items.Clear();
        ddlSupliedItem.Items.Insert(0, new ListItem("Select", "0"));
        ddlSupliedItem.DataSource = ClsTPUMaster.BindSupliedItem();
        ddlSupliedItem.DataTextField = "ITEM_NAME";
        ddlSupliedItem.DataValueField = "ID";
        ddlSupliedItem.DataBind();

        ddltype.Items.Clear();
        ddltype.Items.Insert(0, new ListItem("ALL", "0"));
        ddltype.DataSource = ClsTPUMaster.BindSupliedItem();
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
                gvTPUVendor.DataSource = ClsTPUMaster.BindTPUGrid();
                gvTPUVendor.DataBind();

            }
            else
            {
                gvTPUVendor.DataSource = ClsTPUMaster.BindTPUGrid();

                gvTPUVendor.Columns[1].Visible = false;
                gvTPUVendor.Columns[4].Visible = false;
                gvTPUVendor.Columns[7].Visible = true;
                gvTPUVendor.Columns[8].Visible = false;
                gvTPUVendor.Columns[14].Visible = true;
                gvTPUVendor.Columns[15].Visible = false;
                gvTPUVendor.Columns[16].Visible = true;
                gvTPUVendor.Columns[17].Visible = true;
                gvTPUVendor.Columns[18].Visible = true;
                gvTPUVendor.Columns[19].Visible = false;
                gvTPUVendor.Columns[20].Visible = false;
                gvTPUVendor.Columns[25].Visible = false;
                gvTPUVendor.Columns[26].Visible = false;
                gvTPUVendor.Columns[27].Visible = false;
                gvTPUVendor.Columns[28].Visible = false;
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

    #region Load Compny
    public void LoadCompnyType()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlcompanytype.Items.Clear();
            ddlcompanytype.DataSource = ClsCustomer.BindCompanyType();
            ddlcompanytype.DataTextField = "COMPANYTYPENAME";
            ddlcompanytype.DataValueField = "COMPANYTYPEID";
            ddlcompanytype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btnAddTPUVendor_Click
    protected void btnAddTPUVendor_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadAccountGroup();
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
            this.txtDistrict.Text = "";
            this.ddlbankname.SelectedValue = "0";
            this.txtbranchname.Text = "";
            this.txtCity.Text = "";
            this.ddlCity1.SelectedValue = "0";
            this.chkActive.Checked = true;
            this.txtPhoneNo1.Text = "";
            this.txtMobileNo1.Text = "";
            this.txtSTNo.Text = "";
            this.txtIFSC.Text = "";
            this.txtAddress1.Text = "";
            this.chkTDS.Checked = true;
            this.chktax.Checked = false;
            this.chkgst.Checked = false;
            this.Hdn_Fld.Value = "";
            this.LoadState1();
            this.LoadState2();
         
            this.ddlDistrict1.Items.Clear();
            this.ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));          
            this.ddlCity1.Items.Clear();
            this.ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.hdnType.Visible = true;
            this.ddlaccountgroup.SelectedValue = "0";
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
            this.txtgstno.Text = "";
            tcsPerCentageAndLimit();
            this.LoadCompnyType();
            this.ddlcompanytype.SelectedValue = "1";
            this.ddlvendorowner.SelectedValue = "0";
            this.rdbledger.SelectedValue = "0";
            if (rdbledger.SelectedValue == "0")
            {
                this.trgroup.Style["display"] = "";
                this.trledger.Style["display"] = "none";
                this.ddlledger.SelectedValue = "0";
            }
            this.chkTransferToHO.Checked = false;
            this.chkgst.Checked = false;
            //this.txtgstno.Text = "";
            this.rdbFactory.Checked = false;
            this.rdbTPU.Checked = true;
            //this.RbApplicable.Text = "";
            this.ddlMsmeNo.Text = "";
            this.ddlMsmedate.Text = "";
            this.txtcreditlimit.Text = "0";
            this.txtcreditday.Text = "0";
            lblApplicable.Text = "";
            lblApplicable2.Text = "";
            ddlMsmeNo.Visible = false;
            cvddlMsmeNo.Visible = false;
            ddlMsmedate.Visible = false;
            imgbtnLRGRCalendar.Visible = false;
            tdlblApplicable.Visible = false;
            tdlblApplicable2.Visible = false;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    public void tcsPerCentageAndLimit()
    {
        ClsVendor_TPU clsGetEntry = new ClsVendor_TPU();
        string mode = "TCS";
        DataTable dataTable = new DataTable();
        dataTable = clsGetEntry.BindGateEntry(mode, "");
        if(dataTable.Rows.Count >0 )
        {
            this.txttcspercentage.Text = dataTable.Rows[0]["PER"].ToString();
            this.txttcslimit.Text = dataTable.Rows[0]["AMNT"].ToString();
        }
        else
        {
            this.txttcspercentage.Text = "0";
            this.txttcslimit.Text = "0";
        }
        
    }

    #region LoadAccountGroup
    public void LoadAccountGroup()
    {
        ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
        ddlaccountgroup.Items.Clear();
        ddlaccountgroup.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlaccountgroup.DataSource = clstxmaster.BindAccountSundryCreditorsGroup();
        ddlaccountgroup.DataTextField = "grpName";
        ddlaccountgroup.DataValueField = "Code";
        ddlaccountgroup.DataBind();

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

    #region LoadLedger
    public void LoadLedger()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.BindLedger();

            if (dt.Rows.Count > 0)
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
                this.ddlledger.DataSource = ClsCustomer.BindLedger();
                this.ddlledger.DataTextField = "NAME";
                this.ddlledger.DataValueField = "ID";
                this.ddlledger.DataBind();
            }
            else
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region rdbledger_SelectedIndexChanged
    protected void rdbledger_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbledger.SelectedValue == "0")
        {
            trgroup.Style["display"] = "";
            trledger.Style["display"] = "none";
            this.ddlledger.SelectedValue = "0";
        }
        else
        {

            trgroup.Style["display"] = "none";
            trledger.Style["display"] = "";
            this.ddlaccountgroup.SelectedValue = "0";

        }
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


    protected void RbApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbApplicable.SelectedValue == "Y")
        {
            lblApplicable2.Text = "MSME CERTIFICATE DATE";
            ddlMsmeNo.Visible = true;
            cvddlMsmeNo.Visible = true;
            lblApplicable.Text = "MSME CERTIFICATE NO";
            ddlMsmedate.Visible = true;
            imgbtnLRGRCalendar.Visible = true;
            tdlblApplicable.Visible = true;
            tdlblApplicable2.Visible = true;
        }
        else
        {
            lblApplicable.Text = "";
            lblApplicable2.Text = "";
            ddlMsmeNo.Visible = false;
            cvddlMsmeNo.Visible = false;
            ddlMsmedate.Visible = false;
            imgbtnLRGRCalendar.Visible = false;
            tdlblApplicable.Visible = false;
            tdlblApplicable2.Visible = false;

        }
    }
    #region btnTPUSubmit_Click
    protected void btnTPUSubmit_Click(object sender, EventArgs e)
    {
        try
        {
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
            if (rdbFactory.Checked)
            {
                Tag = "F";
            }
            else if (rdbTPU.Checked)
            {
                Tag = "T";
            }
            else
            {
                MessageBox1.ShowInfo("Please select Type");
                return;
            }

            string Tagtax = string.Empty;
            if (chktax.Checked)
            {
                Tagtax = "Y";
            }
            else
            {
                Tagtax = "N";
            }
            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string xmlledger = string.Empty;
            string bindverndorid = string.Empty;
            int ledger = 0;
            DataTable dt = ClsTPUMaster.BindGridBranch();
            string ISTRANSFERTOHO = string.Empty;
            if (this.chkTransferToHO.Checked)
            {
                ISTRANSFERTOHO = "Y";
            }
            else
            {
                ISTRANSFERTOHO = "N";
            }
            int ID = 0;
            //if (txtPhoneNo.Text.ToString() != "" || txtMobileNo.Text.ToString() != "" || txtMobileNo1.Text.ToString() != "" || txtPhoneNo1.Text.ToString() != "")
            //{

            if (rdbledger.SelectedValue == "0")
            {
                if (this.ddlaccountgroup.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Account Group");
                    return;
                }
            }
            else
            {
                if (this.ddlledger.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Ledger");
                    return;
                }
            }

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

            if (chkgst.Checked)
            {
                GSTTAG = "Y";
            }
            else
            {
                GSTTAG = "N";
            }
            decimal TdsLimit = 0;
            if (txttdslimit.Text != "")
            {
                TdsLimit = Convert.ToDecimal(txttdslimit.Text);
            }
            else
            {
                TdsLimit = 0;
            }
            if (this.txtcreditlimit.Text == "")
            {
                this.txtcreditlimit.Text = "0";
            }
            if (this.txtcreditday.Text == "")
            {
                this.txtcreditday.Text = "0";
            }
            if (this.RbApplicable.Text == "")
            {
                this.RbApplicable.Text = "N";
            }
            if (this.ddlvendorowner.Text == "")
            {
                this.ddlvendorowner.Text = "B";
            }

            //if (Convert.ToDecimal(txtcreditday.Text.Trim()) < 1 )
            //{
            //    MessageBox1.ShowInfo("You Can Not Enter Credit Limit Days Less Than 1");
            //    return;
            //}
            //if (chkgst.Checked == false)
            //{
            //    MessageBox1.ShowInfo("Please Click The GST Applicable CheckBox");
            //    return;
            //}


            ID = ClsTPUMaster.SaveTPUMaster(Hdn_Fld.Value, txtCode.Text.ToString(), txtName.Text.ToString(), Convert.ToInt32(ddlSupliedItem.SelectedValue.ToString()),
                                            ddlSupliedItem.SelectedItem.Text.ToString(), (Convert.ToInt32(ddlState.SelectedValue)), ddlState.SelectedValue.ToString(),
                                            0, txtDistrict.Text.ToString(),
                                            0, txtCity.Text.ToString(), txtAddress.Text.ToString(),
                                            (Convert.ToInt32(ddlState1.SelectedValue)), ddlState1.SelectedValue.ToString(),
                                            (Convert.ToInt32(ddlDistrict1.SelectedValue)), ddlDistrict1.SelectedItem.Text.ToString(),
                                            (Convert.ToInt32(ddlCity1.SelectedValue)), ddlCity1.SelectedItem.Text.ToString(),
                                            txtAddress1.Text.ToString(), txtMobileNo.Text.ToString(), txtPhoneNo.Text.ToString(),
                                            txtMobileNo1.Text.ToString(), txtPhoneNo1.Text.ToString(), txtPIN.Text.ToString(),
                                            txtPIN1.Text.ToString(), txtEmailid.Text.ToString(), txtcontactperson.Text.ToString(),
                                            txtcstno.Text.ToString(), txtvatno.Text.ToString(), txttinno.Text.ToString(),
                                            txtpanno.Text.ToString(), txtSTNo.Text.ToString(), txtbankacno.Text.ToString(),
                                            ddlbankname.SelectedValue.ToString(), ddlbankname.SelectedItem.Text.ToString(),
                                            "0", txtbranchname.Text.ToString(),
                                            txtIFSC.Text.ToString(), Mode, chkTDS.Checked.ToString(), chkActive.Checked.ToString(),
                                            Tag, ddlaccountgroup.SelectedValue, ddlaccountgroup.SelectedItem.Text.Trim(), Tagtax.Trim(),
                                            this.ddlcompanytype.SelectedValue.Trim(), this.ddlledger.SelectedValue.Trim(), gstnno.Trim(), GSTTAG.Trim(), ISTRANSFERTOHO.Trim(),
                                            TdsLimit, IPAddress, MACAddress, Convert.ToDecimal(this.txtcreditlimit.Text), (Convert.ToInt32(txtcreditday.Text)),
                                            this.RbApplicable.SelectedValue.Trim(), this.ddlMsmeNo.Text.ToString(), this.ddlMsmedate.Text, this.ddlvendorowner.SelectedValue.Trim(), Convert.ToDecimal(txttcspercentage.Text), Convert.ToDecimal(txttcslimit.Text)); // txttcspercentage & txttcslimit added

            if (ID == -2)
            {
                MessageBox1.ShowInfo("Vendor Name alredy exist!");
                return;
            }
            else if (ID >= 1)
            {
                if (rdbledger.SelectedValue.Trim() == "0")
                {
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
                    bindverndorid = ClsTPUMaster.Bindvendorid(txtName.Text.Trim());

                    xmlledger = ConvertDatatableToXML(dtledger);

                    DataTable deletedt = new DataTable();
                    if (Hdn_Fld.Value == "")
                    {
                        ViewState["mode"] = "A";
                    }
                    else
                    {
                        ViewState["mode"] = "U";
                    }
                    string Modedelete = ViewState["mode"].ToString();

                    #region  Fetch TaxId and CostCenter(Rajeev 25-06-2018)
                    DataTable Taxdt = new DataTable();
                    string CostCenter, TaxId;
                    Taxdt = ClsTPUMaster.FetchTaxId(Hdn_Fld.Value);
                    if (Taxdt.Rows.Count > 0)
                    {
                        CostCenter = Taxdt.Rows[0]["COSTCENTER"].ToString();
                        TaxId = Taxdt.Rows[0]["TAXID"].ToString();
                    }
                    else
                    {
                        CostCenter = "N";
                        TaxId = "";
                    }
                    #endregion

                    ledger = ClsTPUMaster.SaveAccInfo(txtName.Text.Trim(), ddlaccountgroup.SelectedValue.Trim(), Session["FINYEAR"].ToString(), Hdn_Fld.Value.Trim(), xmlledger, bindverndorid.Trim(), Modedelete, CostCenter, TaxId);

                }
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                //LoadTPUMaster();
                BindGrid();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                txtAddress1.Text = "";
                txttdslimit.Text = "";
                this.txtcreditlimit.Text = "";
                this.txtcreditday.Text = "";
                this.ddlMsmeNo.Text = "";
                this.ddlMsmedate.Text = "";
                //this.RbApplicable.SelectedValue = "";
                ddlaccountgroup.SelectedValue = "0";

                this.chkgst.Checked = false;
                this.txtgstno.Text = "";
                this.txtstatecode.Text = "";
                this.txtgstpanno.Text = "";
                this.rdbFactory.Checked = false;
                this.rdbTPU.Checked = false;
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string strPopup = "<script language='javascript' ID='script2'>addTab('Tax', 'frmTaxSheetMaster.aspx')</script>";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                }
            }
            


            Session["LEDGERDETAILS"] = null;
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
            this.rdbFactory.Checked = false;
            this.rdbTPU.Checked = false;
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
            txtDistrict.Text = "";
            ddlState1.SelectedValue = "0";
            ddlDistrict1.SelectedValue = "0";
            ddlbankname.SelectedValue = "0";
            txtbranchname.Text = "";
            txtCity.Text = "";
            ddlCity1.SelectedValue = "0";
            chkActive.Checked = false;
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            txtSTNo.Text = "";
            txtIFSC.Text = "";
            chkTDS.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            ddlaccountgroup.SelectedValue = "0";
            this.LoadCompnyType();
            this.ddlcompanytype.SelectedValue = "1";
            this.rdbledger.SelectedValue = "0";
            this.RbApplicable.Text = null;
            if (rdbledger.SelectedValue == "0")
            {
                trgroup.Style["display"] = "";
                trledger.Style["display"] = "none";
                this.ddlledger.SelectedValue = "0";
            }
            this.chkTransferToHO.Checked = false;
            this.chkgst.Checked = false;
            this.txtgstno.Text = "";

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
            if (ClsTPUMaster.GetstatusChecking(e.Record["VENDORID"].ToString()) == "1")
            {
                e.Record["Error"] = "Sorry, bill's already done against this Vendor into system..";
                return;
            }
            else
            {
                if (e.Record["VENDORID"] != "")
                {
                    int ID = 0;
                    ID = ClsTPUMaster.DeleteTPUMaster(e.Record["VENDORID"].ToString());
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

   
    #region ddlState1_SelectedIndexChanged
    protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict1.Items.Clear();
            ddlDistrict1.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict1.DataSource = ClsTPUMaster.BindDistrict(Convert.ToInt32(ddlState1.SelectedValue));
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

    

    #region ddlDistrict1_SelectedIndexChanged
    protected void ddlDistrict1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCity1.Items.Clear();
            ddlCity1.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity1.DataSource = ClsTPUMaster.BindCity(Convert.ToInt32(ddlDistrict1.SelectedValue));
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
    //protected void btnAddCity_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        string strPopup = "<script language='javascript' ID='script1'>"

    //        // Passing intId to popup window.
    //        + "window.open('frmAddCity.aspx?stateid=" + HttpUtility.UrlEncode(ddlState.SelectedValue) + "&distid=" + HttpUtility.UrlEncode(ddlDistrict.SelectedValue)

    //        + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

    //        + "</script>";

    //        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }

    //}
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
        //ddlDistrict_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnnewRefresh_Click
    protected void btnnewRefresh_Click(object sender, EventArgs e)
    {
        ddlDistrict1_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnAddBranch_Click
    //protected void btnAddBranch_Click(object sender, EventArgs e)
    //{


    //    string strPopup = "<script language='javascript' ID='script1'>"

    //    // Passing intId to popup window.
    //    + "window.open('frmAddBankbrnch.aspx?brnchid=" + HttpUtility.UrlEncode(ddlbranchname.SelectedValue)

    //    + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

    //    + "</script>";

    //    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);


    //}
    #endregion

    #region btnRefresh1_Click
    protected void btnRefresh1_Click(object sender, EventArgs e)
    {
        //LoadBranchName();
    }
    #endregion

    #region Bindgrid
    public void Bindgrid()
    {
        String[] PrimaryID = this.ddlDivision.SelectedValue.Trim().Split('~');
        if (PrimaryID.Length > 1)
        {
            string ID = PrimaryID[1].Trim();
            gvPRODUCTMap.DataSource = ClsTPUMaster.BindProductGrid(ID, ddlCategory.SelectedValue);
            gvPRODUCTMap.DataBind();
        }
        else
        {
            gvPRODUCTMap.DataSource = ClsTPUMaster.BindProductGrid(ddlDivision.SelectedValue.Trim(), ddlCategory.SelectedValue);
            gvPRODUCTMap.DataBind();
        }

    }
    #endregion

    #region LoadDivisionName
    public void LoadDivisionName()
    {
        ddlDivision.Items.Clear();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        ddlDivision.DataSource = ClsTPUMaster.BindDivision();
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
        ddlCategory.DataSource = ClsTPUMaster.BindCategory();
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

    #region btngridedit_Click1
    protected void btngridedit_Click1(object sender, EventArgs e)
    {

        try
        {
            if (hdnTid.Value == "E")
            {
                ClsTPUVendor ClsTPUMaster = new ClsTPUVendor();
                string userid = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();
                dt = ClsTPUMaster.BindTPUMasterById(userid);
              
                if (dt.Rows.Count > 0)
                {
                    txtCode.Text = dt.Rows[0]["CODE"].ToString();
                    txtName.Text = dt.Rows[0]["VENDORNAME"].ToString();
                    txttcspercentage.Text = dt.Rows[0]["TCS_PERCENT"].ToString();
                    txttcslimit.Text = dt.Rows[0]["TCS_LIMIT"].ToString();

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
                        rdbTPU.Checked = true;
                        rdbFactory.Checked = false;
                    }
                    else
                    {
                        rdbFactory.Checked = true;
                        rdbTPU.Checked = false;
                    }
                    if (dt.Rows[0]["SERVICETAX"].ToString() == "Y")
                    {
                        chktax.Checked = true;
                    }
                    else
                    {
                        chktax.Checked = false;
                    }
                    //ddlSupliedItem.SelectedItem.Text = dt.Rows[0]["SUPLIEDITEM"].ToString();

                    if (ddlState.Items.FindByValue(Convert.ToString(dt.Rows[0]["STATEID"])) != null)
                    {
                        ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                    }
                    txtDistrict.Text = Convert.ToString(dt.Rows[0]["DISTRICTNAME"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CITYNAME"]);
                    txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();


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
                    txttdslimit.Text = dt.Rows[0]["TDSLIMIT"].ToString();
                    txtcreditlimit.Text = dt.Rows[0]["CREDIT_LIMIT"].ToString();
                    txtcreditday.Text = dt.Rows[0]["CREDIT_DAY"].ToString();
                    RbApplicable.Text = dt.Rows[0]["MSME_TAG"].ToString();
                    ddlMsmeNo.Text = dt.Rows[0]["MSME_NO"].ToString();
                    ddlvendorowner.Text = dt.Rows[0]["VENDOROWNER"].ToString();
                    ddlMsmedate.Text = dt.Rows[0]["MSME_DATE"].ToString();
                    if (dt.Rows[0]["MSME_TAG"].ToString() == "N")
                    {
                        lblApplicable.Text = "";
                        lblApplicable2.Text = "";
                        ddlMsmeNo.Visible = false;
                        cvddlMsmeNo.Visible = false;
                        ddlMsmedate.Visible = false;
                        imgbtnLRGRCalendar.Visible = false;
                        tdlblApplicable.Visible = false;
                        tdlblApplicable2.Visible = false;
                    }
                    //if (dt.Rows[0]["MSME_TAG"].ToString() == "Y")
                    //   {
                    //   lblApplicable.Text = "";
                    //   ddlMsmedate.Text = "";
                    //   }
                    LoadBankName();
                    ddlbankname.SelectedValue = dt.Rows[0]["BANKID"].ToString();
                    
                    txtbranchname.Text = dt.Rows[0]["BRANCHNAME"].ToString();
                    this.LoadCompnyType();
                    this.ddlcompanytype.SelectedValue = Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim();
                    this.ddlvendorowner.SelectedValue = Convert.ToString(dt.Rows[0]["VENDOROWNER"]).Trim();
                    if (dt.Rows[0]["ISTDSDECLARE"].ToString() == "Y")
                    {
                        chkTDS.Checked = true;
                    }
                    else
                    {
                        chkTDS.Checked = false;
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
                        this.txtgstno.Text = "";
                        this.txtstatecode.Text = "";
                        this.txtgstpanno.Text = "";

                    }
                    LoadAccountGroup();
                    if (dt.Rows[0]["ACCCODE"].ToString() != "")
                    {
                        ddlaccountgroup.SelectedValue = dt.Rows[0]["ACCCODE"].ToString();
                    }
                    this.LoadLedger();

                    string a = "qwrqwr";

                    if (dt.Rows[0]["LEDGER_REFERENCEID"].ToString() != "")
                    {
                        if (ddlledger.Items.FindByValue(Convert.ToString(dt.Rows[0]["LEDGER_REFERENCEID"])) != null)
                        {
                            this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                        }
                    }

                    if (this.ddlaccountgroup.SelectedValue == "0")
                    {
                        trledger.Style["display"] = "";
                        rdbledger.SelectedValue = "1";

                        trgroup.Style["display"] = "none";
                        //ddlAccGroup.SelectedValue = "0";
                    }
                    else
                    {
                        trledger.Style["display"] = "none";
                        rdbledger.SelectedValue = "0";
                        trgroup.Style["display"] = "";
                    }

                    if (dt.Rows[0]["ISTRANSFERTOHO"].ToString() == "Y")
                    {
                        this.chkTransferToHO.Checked = true;
                    }
                    else
                    {
                        this.chkTransferToHO.Checked = false;
                    }
                }

                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                pnlmapping.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";


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

                ds = ClsTPUMaster.ProductMappingDetails(Convert.ToString(this.Hdn_Fld.Value));
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
            ClsTPUVendor ClsTPUMaster = new ClsTPUVendor();
            DataTable dtTPUProduct = (DataTable)HttpContext.Current.Session["TPUPRODUCTMAPPING"];
            int success = 0;
            string xml = string.Empty;
            xml = ConvertDatatableToXML(dtTPUProduct);
            success = ClsTPUMaster.SaveProductMapping(Convert.ToString(this.Hdn_Fld.Value), this.txtTname.Text.Trim(), Convert.ToString(this.hdnTag.Value), xml);
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
                GridDataControlFieldCell cell = e.Row.Cells[19] as GridDataControlFieldCell;
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

    #region LoadFilterAccountGroup
    public void LoadFilterAccountGroup()
    {
        ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
        ddlaccgroup.Items.Clear();
        ddlaccgroup.Items.Insert(0, new ListItem("ALL", "0"));
        ddlaccgroup.DataSource = clstxmaster.BindAccountgrp();
        ddlaccgroup.DataTextField = "grpName";
        ddlaccgroup.DataValueField = "Code";
        ddlaccgroup.DataBind();

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
                gvTPUVendor.DataSource = ClsTPUMaster.BindTPUGridByType(ddltype.SelectedValue.Trim(), ddlaccgroup.SelectedValue);
                gvTPUVendor.Columns[19].Visible = true;
                gvTPUVendor.Columns[20].Visible = false;
                gvTPUVendor.Columns[25].Visible = true;
                gvTPUVendor.Columns[26].Visible = true;
                gvTPUVendor.DataBind();
            }
            else
            {
                gvTPUVendor.DataSource = ClsTPUMaster.BindTPUGridByType(ddltype.SelectedValue.Trim(), ddlaccgroup.SelectedValue);
                gvTPUVendor.Columns[1].Visible = false;
                gvTPUVendor.Columns[4].Visible = false;
                gvTPUVendor.Columns[7].Visible = true;
                gvTPUVendor.Columns[8].Visible = false;
                gvTPUVendor.Columns[14].Visible = true;
                gvTPUVendor.Columns[15].Visible = false;
                gvTPUVendor.Columns[16].Visible = true;
                gvTPUVendor.Columns[17].Visible = true;
                gvTPUVendor.Columns[18].Visible = true;
                gvTPUVendor.Columns[19].Visible = false;
                gvTPUVendor.Columns[20].Visible = false;
                gvTPUVendor.Columns[25].Visible = false;
                gvTPUVendor.Columns[26].Visible = false;
                gvTPUVendor.AllowPageSizeSelection = true;
                gvTPUVendor.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    #region btnDepotWiseSave_Click

    protected void btnDepotWiseSave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTPUVendor clstrans = new ClsTPUVendor();
            if (HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] == null)
            {
                CreateDataTableDepot();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"];
            int count = 0;
            string XML = string.Empty;

            foreach (GridViewRow gvrow in gvDepotWiseGst.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
                Label lblDepot_ID = (Label)gvrow.FindControl("lblDepot_ID");
                Label lblDepot_Name = (Label)gvrow.FindControl("lblDepot_Name");

                if (chkBx.Checked == true)
                {
                    count = count + 1;
                    DataRow dr = DT.NewRow();
                    dr["VENDORID"] = Hdn_Fld.Value.Trim();
                    dr["VENDORNAME"] = txtTname.Text.Trim();
                    dr["BRID"] = lblDepot_ID.Text.Trim();
                    dr["BRNAME"] = lblDepot_Name.Text.Trim();
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();

                }
            }
            HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = DT;

            if (count > 0)
            {
                int id = 0;
                XML = ConvertDatatableToXML(DT);
                id = clstrans.SaveDepotWiseGstMapped(this.Hdn_Fld.Value.Trim(), XML);

                if (id > 0)
                {

                    // int update = clstrans.UpdateLdomTpuTransporterMaster(Hdn_Fld.Value.Trim());

                    MessageBox1.ShowSuccess("Record saved successfully..");
                    this.gvDepotWiseGst.DataSource = null;
                    this.gvDepotWiseGst.DataBind();
                    HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = null;
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record ");
                    HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = null;
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("Plese Select atleast 1 record  ");
                HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = null;
                return;
            }

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            pnlmapping.Style["display"] = "NONE";
            pnlDepotWiseGstMapping.Style["display"] = "NONE";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnDepotWiseCancel_Click
    protected void btnDepotWiseCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            pnlmapping.Style["display"] = "NONE";
            pnlDepotWiseGstMapping.Style["display"] = "NONE";
            HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    protected void gvdepotWiseGst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            ClsTPUVendor clsvendor = new ClsTPUVendor();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dt = clsvendor.EditDEPOTWISEGSTMAPPING(Hdn_Fld.Value.Trim());
                if (dt.Rows.Count > 0)
                {



                    string lblDepot_ID = (e.Row.FindControl("lblDepot_ID") as Label).Text;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label lblDepot_Name = (e.Row.FindControl("lblDepot_Name") as Label);
                        CheckBox chkBx = (e.Row.FindControl("chkSelect") as CheckBox);
                        if (dt.Rows[i]["BRID"].ToString().Trim() == lblDepot_ID.Trim())
                        {

                            lblDepot_Name.Text = dt.Rows[i]["BRNAME"].ToString();

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
    public DataTable CreateDataTableDepot()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRNAME", typeof(string)));

        HttpContext.Current.Session["DEPOTWISEMAPPINGDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region btndepotWiseGst_Click
    protected void btndepotWiseGst_Click(object sender, EventArgs e)
    {
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "none";
        btnaddhide.Style["display"] = "none";
        pnlmapping.Style["display"] = "NONE";
        pnlDepotWiseGstMapping.Style["display"] = "";
        LoadDepotMaster();
        CreateDataTableDepot();
    }
    #endregion
    #region LoadDepotMaster
    public void LoadDepotMaster()
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            gvDepotWiseGst.DataSource = clstrans.BinddepotMasterGrid();
            gvDepotWiseGst.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion
}