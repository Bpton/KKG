using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmTransporterMaster : System.Web.UI.Page
{
    ClsTransporter clstrans = new ClsTransporter();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                pnlmapping.Style["display"] = "none";
                pnlStateWiseGstMapping.Style["display"] = "none";
                LoadState();
                LoadBankName();
                LoadBranchName();
                LoadTPUMaster();
                LoadDepotMaster();
                LoadAccGroup();
                ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
                ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
                this.LoadLedger();
                this.LoadCompnyType();
                if (Request.QueryString["MENU"] == "TransporterMaster")
                {
                    LoadTransporterMaster();
                    tdExcel.Style["display"] = "none";
                    btnaddhide.Style["display"] = "";
                }
                else if (Request.QueryString["MENU"] == "TransporterMaster-Inv")
                {
                    tdExcel.Style["display"] = "none";
                    pnlAdd.Style["display"] = "";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";

                }
                else
                {
                    LoadTransporterMaster();
                    tdExcel.Style["display"] = "";
                    btnaddhide.Style["display"] = "none";
                }
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

    #region LoadAccGroup
    public void LoadAccGroup()
    {
        try
        {
            ddlAccGroup.Items.Clear();
            ddlAccGroup.Items.Insert(0, new ListItem("Select Group", "0"));
            ddlAccGroup.DataSource = clstrans.BindAccGroup();
            ddlAccGroup.DataTextField = "grpName";
            ddlAccGroup.DataValueField = "Code";
            ddlAccGroup.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadState
    public void LoadState()
    {
        try
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
            ddlState.DataSource = clstrans.BindState();
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_ID";
            ddlState.DataBind();

            ddlsearchstate.Items.Clear();
            ddlsearchstate.Items.Insert(0, new ListItem("ALL", "0"));
            ddlsearchstate.DataSource = clstrans.BindState();
            ddlsearchstate.DataTextField = "State_Name";
            ddlsearchstate.DataValueField = "State_ID";
            ddlsearchstate.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadDistrict
    public void LoadDistrict(int stateid)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
            ddlDistrict.DataSource = clstrans.BindDistrict(stateid);
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

    #region LoadCity
    public void LoadCity(int cityid)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            ddlCity.DataSource = clstrans.BindCity(cityid);
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

    #region LoadBankName
    public void LoadBankName()
    {
        ddlbankname.Items.Clear();
        ddlbankname.Items.Insert(0, new ListItem("Select Bank", "0"));
        ddlbankname.DataSource = clstrans.BindBankName();
        ddlbankname.DataTextField = "BANKNAME";
        ddlbankname.DataValueField = "ID";
        ddlbankname.DataBind();
    }
    #endregion

    #region LoadBranchName
    public void LoadBranchName()
    {
        ddlbranchname.Items.Clear();
        ddlbranchname.Items.Insert(0, new ListItem("Select Branch", "0"));
        ddlbranchname.DataSource = clstrans.BindBrachName();
        ddlbranchname.DataTextField = "BRANCH";
        ddlbranchname.DataValueField = "ID";
        ddlbranchname.DataBind();
    }
    #endregion

    #region ddlsearchstate_SelectedIndexChanged
    protected void ddlsearchstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["MENU"] == "TransporterMaster")
            {
                gvTransporter.DataSource = clstrans.BindTransporterGridByState(Convert.ToInt32(ddlsearchstate.SelectedValue.Trim()));
                gvTransporter.DataBind();
            }
            else
            {
                gvTransporter.DataSource = clstrans.BindTransporterGridByState(Convert.ToInt32(ddlsearchstate.SelectedValue.Trim()));
                gvTransporter.Columns[0].Visible = true;
                gvTransporter.Columns[17].Visible = true;
                //gvTransporter.Columns[17].Visible = false;
                //gvTransporter.Columns[18].Visible = false;
                gvTransporter.Columns[20].Visible = true;
                gvTransporter.Columns[21].Visible = false;
                gvTransporter.Columns[22].Visible = false;
                gvTransporter.Columns[23].Visible = false;
                gvTransporter.Columns[24].Visible = false;
                gvTransporter.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region RbApplicable_SelectedIndexChanged
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
    #endregion

    #region LoadTPUMaster
    public void LoadTPUMaster()
    {
        try
        {
            gvTPUMap.DataSource = clstrans.BindTPUMasterGrid();
            gvTPUMap.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadDepotMaster
    public void LoadDepotMaster()
    {
        try
        {
            gvDepot.DataSource = clstrans.BinddepotMasterGrid();
            gvDepot.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadTransporterMaster
    public void LoadTransporterMaster()
    {
        try
        {
            if (Request.QueryString["MENU"] == "TransporterMaster")
            {
                gvTransporter.DataSource = clstrans.BindTransporterGrid();
                gvTransporter.DataBind();
            }
            else
            {
                gvTransporter.DataSource = clstrans.BindTransporterGrid();
                gvTransporter.Columns[0].Visible = true;
                gvTransporter.Columns[17].Visible = true;
                //gvTransporter.Columns[17].Visible = false;
                //gvTransporter.Columns[18].Visible = false;
                gvTransporter.Columns[20].Visible = true;
                gvTransporter.Columns[21].Visible = false;
                gvTransporter.Columns[22].Visible = false;
                gvTransporter.Columns[23].Visible = false;
                gvTransporter.Columns[24].Visible = false;
                gvTransporter.DataBind();
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

    #region btnAddTransporter_Click
    protected void btnAddTransporter_Click(object sender, EventArgs e)
    {
        try
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtPIN.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            txtcontactperson.Text = "";
            txtcstno.Text = "";
            txtvatno.Text = "";
            txtpanno.Text = "";
            txttinno.Text = "";
            txtbankacno.Text = "";
            ddlbankname.SelectedValue = "0";
            ddlbranchname.SelectedValue = "0";
            ddlAccGroup.SelectedValue = "0";
            chkActive.Checked = true;
            chktax.Checked = false;
            txtEmailid.Text = "";
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            txtSTNo.Text = "";
            txtIFSC.Text = "";
            this.ddlMsmeNo.Text = "";
            ddlMsmeNo.Visible = false;
            cvddlMsmeNo.Visible = false;
            ddlMsmedate.Visible = false;
            imgbtnLRGRCalendar.Visible = false;
            tdlblApplicable.Visible = false;
            tdlblApplicable2.Visible = false;
            this.ddlMsmedate.Text = "";
            chkgst.Checked = false;
            chkTDS.Checked = true;
            Hdn_Fld.Value = "";
            LoadState();
            LoadBankName();
            LoadBranchName();
            LoadAccGroup();
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            rdbaccount.SelectedValue = "N";
            rdbvechile.SelectedValue = "N";
            if (rdbledger.SelectedValue == "0")
            {
                trgroup.Style["display"] = "";
                trledger.Style["display"] = "none";
                this.ddlledger.SelectedValue = "0";
            }
            this.LoadCompnyType();
            this.ddlcompanytype.SelectedValue = "1";
            this.chkTransferToHO.Checked = false;
            RbApplicable.SelectedValue = "N";
            //this.chkgst.Checked = true;
            this.txtgstno.Text = "";
            this.txttdslimit.Text = "0";
            this.txtcreditlimit.Text = "0";
            this.txtcreditday.Text = "0";
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
            this.ddlAccGroup.SelectedValue = "0";

        }
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
                this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue));
            }

        }
        else
        {
            this.txtgstno.Text = ""; ;
            this.txtstatecode.Text = "";
            this.txtgstpanno.Text = "";
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

    #region btntransporterSubmit_Click
    protected void btntransporterSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtledger = CreateLedgerTable();
            DataTable dtTransporterCode = new DataTable();
            string GSTAPPLICABLE = string.Empty;
            string Mode = "";
            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
                dtTransporterCode = clstrans.GenerateTransporterCode();
                this.txtCode.Text = dtTransporterCode.Rows[0]["CODE"].ToString().Trim();
            }
            else
            {
                Mode = "U";
            }
            string Tag = string.Empty;
            if (chktax.Checked)
            {
                Tag = "Y";
            }
            else
            {
                Tag = "N";
            }

            string MACAddress = GetMACAddress();
            string IPAddress = Request.UserHostAddress.ToString().Trim();

            string xmlledger = string.Empty;
            int ID = 0;
            int TransporterID = 0;
            DataTable dt = clstrans.BindGridBranch();
            int i = 0;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtledger.NewRow();
                dr["ID"] = Guid.NewGuid();
                dr["OPENBALANCE"] = 0;
                dr["BALANCETYPE"] = "D";
                dr["DEBITAMOUNT"] = 0;
                dr["CREDITAMOUNT"] = 0;
                dr["REGIONID"] = dt.Rows[i]["REGIONID"].ToString();
                dr["REGIONNAME"] = dt.Rows[i]["REGIONNAME"].ToString();

                dtledger.Rows.Add(dr);
                dtledger.AcceptChanges();
            }

            xmlledger = ConvertDatatableToXML(dtledger);

            if (rdbledger.SelectedValue == "0")
            {
                if (this.ddlAccGroup.SelectedValue == "0")
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


            string ISTRANSFERTOHO = string.Empty;
            if (this.chkTransferToHO.Checked)
            {
                ISTRANSFERTOHO = "Y";
            }
            else
            {
                ISTRANSFERTOHO = "N";
            }

            //if (chkgst.Checked)
            //{
            //    if (this.txtgstno.Text == "")
            //    {
            //        MessageBox1.ShowInfo("Please Enter GST No!");
            //        return;
            //    }

            //}
            //else
            //{
            //    this.txtgstno.Text = "";
            //}

            string gstnno = string.Empty;

            if (chkgst.Checked)
            {
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
                    gstnno = string.Concat(this.txtstatecode.Text.Trim() + this.txtgstpanno.Text + this.txtgstno.Text.Trim());
                }
            }

            if (chkgst.Checked)
            {
                GSTAPPLICABLE = "Y";
            }
            else
            {
                GSTAPPLICABLE = "N";
            }
            string REVERSECHARGE = string.Empty;
            if (this.ChkReverseCharge.Checked)
            {
                REVERSECHARGE = "Y";
            }
            else
            {
                REVERSECHARGE = "N";
            }
            if (this.txttdslimit.Text == "")
            {
                this.txttdslimit.Text = "0";
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
            if (this.RbApplicable.Text == "N")
            {
                this.ddlMsmeNo.Text = "0";
                this.ddlMsmedate.Text = "";
            }


            ID = clstrans.SaveTransporter(Hdn_Fld.Value, txtCode.Text.ToString(), txtName.Text.ToString(),
                                         (Convert.ToInt32(ddlState.SelectedValue)), ddlState.SelectedValue.ToString(), (Convert.ToInt32(ddlDistrict.SelectedValue)),
                                         ddlDistrict.SelectedItem.Text.ToString(), (Convert.ToInt32(ddlCity.SelectedValue)), ddlCity.SelectedItem.Text.ToString(),
                                         txtAddress.Text.ToString(), txtMobileNo.Text.ToString(), txtPhoneNo.Text.ToString(), txtMobileNo1.Text.ToString(), txtPhoneNo1.Text.ToString(),
                                         txtPIN.Text.ToString(), txtEmailid.Text.ToString(), txtcontactperson.Text.ToString(), txtcstno.Text.ToString(), txtvatno.Text.ToString(),
                                         txttinno.Text.ToString(), txtpanno.Text.ToString(), txtSTNo.Text.ToUpper(), txtbankacno.Text.ToString(), ddlbankname.SelectedValue.ToString(),
                                         ddlbankname.SelectedItem.Text.ToString(), ddlbranchname.SelectedValue.ToString(), ddlbranchname.SelectedItem.Text.ToString(),
                                         txtIFSC.Text.ToString(), Mode, chkTDS.Checked.ToString(), chkActive.Checked.ToString(), ddlAccGroup.SelectedValue.ToString(),
                                         ddlAccGroup.SelectedItem.ToString().Trim(), txtaddress2.Text.Trim(), txtpin2.Text.Trim(), Tag.Trim(),
                                         this.rdbaccount.SelectedValue.Trim(), this.rdbvechile.SelectedValue.Trim(), this.ddlledger.SelectedValue.Trim(),
                                         this.ddlcompanytype.SelectedValue.Trim(), gstnno.Trim(), GSTAPPLICABLE.Trim(), ISTRANSFERTOHO.Trim(), REVERSECHARGE.Trim(),
                                         Convert.ToDecimal(this.txttdslimit.Text), IPAddress, MACAddress, Convert.ToDecimal(this.txtcreditlimit.Text), (Convert.ToInt32(txtcreditday.Text)),
                                         this.RbApplicable.SelectedValue.Trim(), this.ddlMsmeNo.Text.ToString(), this.ddlMsmedate.Text);





            if (ID >= 1)
            {
                if (rdbledger.SelectedValue.Trim() == "0")
                {


                    TransporterID = clstrans.BindtransporterByID(txtName.Text.Trim(), txtCode.Text.Trim(), ddlState.SelectedValue.Trim(), ddlDistrict.SelectedValue.Trim(), ddlCity.SelectedValue.Trim(), txtMobileNo.Text.Trim());
                    ID = clstrans.SaveAccInfo(this.txtName.Text.Trim(), this.ddlAccGroup.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim(),
                                                Hdn_Fld.Value.Trim(), xmlledger, Convert.ToString(TransporterID));
                }


                MessageBox1.ShowSuccess("<b><font color='green'>Record Saved Successfully!</font></b>");
                LoadTransporterMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                this.txttdslimit.Text = "0";
                this.txtcreditlimit.Text = "0";
                this.txtcreditday.Text = "0";
                this.chkgst.Checked = false;
                this.txtgstno.Text = "";
                this.txtgstpanno.Text = "";
                this.txtstatecode.Text = "";

                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string strPopup = "<script language='javascript' ID='script2'>addTab('Tax', 'frmTaxSheetMaster.aspx')</script>";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                }

                if (Request.QueryString["MENU"] == "TransporterMaster-Inv")
                {
                  
                        string strPopup = "<script language='javascript' ID='script1'>"
                    + "window.close()"
                    + "</script>";
                        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                    
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

    #region btnTransporterCancel_Click
    protected void btnTransporterCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["MENU"] == "TransporterMaster-Inv") {
                string strPopup = "<script language='javascript' ID='script1'>"
            + "window.close()"
            + "</script>";
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            this.LoadCompnyType();
            this.ddlcompanytype.SelectedValue = "1";
            rdbaccount.SelectedValue = "N";
            this.txtgstno.Text = "";
            this.txtgstpanno.Text = "";
            this.txtstatecode.Text = "";
            rdbvechile.SelectedValue = "N";
            txtCode.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtaddress2.Text = "";
            txtpin2.Text = "";
            txtPIN.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            txtcontactperson.Text = "";
            this.ddlMsmeNo.Text = "";
            ddlMsmeNo.Visible = false;
            cvddlMsmeNo.Visible = false;
            ddlMsmedate.Visible = false;
            imgbtnLRGRCalendar.Visible = false;
            tdlblApplicable.Visible = false;
            tdlblApplicable2.Visible = false;
            this.ddlMsmedate.Text = "";
            txtcstno.Text = "";
            txtvatno.Text = "";
            txtpanno.Text = "";
            txttinno.Text = "";
            txtbankacno.Text = "";
            chkActive.Checked = false;
            txtEmailid.Text = "";
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
            ddlAccGroup.SelectedValue = "0";
            txtSTNo.Text = "";
            txtIFSC.Text = "";
            chkTDS.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            this.chkTransferToHO.Checked = false;
            this.ChkReverseCharge.Checked = false;
            this.chkgst.Checked = false;
            this.txtgstno.Text = "";
            //this.txtgstno.Enabled = false;
            this.txttdslimit.Text = "0";
            this.txtcreditlimit.Text = "0";
            this.txtcreditday.Text = "0";
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
            if (clstrans.GetstatusChecking(e.Record["ID"].ToString()) == "1")
            {
                e.Record["Error"] = "Sorry, Bill's already done against this Transporter into system..";
                return;
            }
            else
            {
                if (e.Record["ID"] != "")
                {
                    int ID = 0;
                    ID = clstrans.DeleteTransporterMaster(e.Record["ID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";
                        LoadTransporterMaster();
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

    private void GetSateCode(int stateid)
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            string code = clstrans.BindStateCode(stateid);
            this.txtstatecode.Text = code.Trim();
            this.txtgstpanno.Text = this.txtpanno.Text.ToUpper();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region ddlState_SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = clstrans.BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
            this.GetSateCode(Convert.ToInt32(ddlState.SelectedValue));

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlDistrict.ClientID + "').focus(); ", true);
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
            ddlCity.DataSource = clstrans.BindCity(Convert.ToInt32(ddlDistrict.SelectedValue));
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

    #region LoadTPUbyid
    public void LoadTPUbyid(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = clstrans.BindTPUMasterGridbyid(id);
            if (dt.Rows.Count > 0)
            {
                Session["DATA"] = dt;
            }
            else
            {
                Session["DATA"] = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region gvTPUMap_RowDataBound
    protected void gvTPUMap_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataTable dt = new DataTable();

        if (hdnTid.Value != "")
        {
            dt = clstrans.BindTPUMasterGridbyid(hdnTid.Value);
        }

        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
            CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
            HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == chk.ToolTip)
                {
                    chk.Checked = true;
                }
            }
        }
    }
    #endregion

    #region LoadDepotbyid
    public void LoadDepotbyid(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = clstrans.BinddepotMasterGridbyid(id);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region gvDepot_RowDataBound
    protected void gvDepot_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataTable dt = new DataTable();
        if (hdnTid.Value != "")
        {
            //LoadDepotbyid(hdnTid.Value);
            dt = clstrans.BinddepotMasterGridbyid(hdnTid.Value);

        }

        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell Depotname = e.Row.Cells[1] as GridDataControlFieldCell;
            CheckBox chk = Depotname.FindControl("ChkIDFactory") as CheckBox;
            HiddenField hiddenField = Depotname.FindControl("hdnFactoryName") as HiddenField;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == chk.ToolTip)
                {
                    chk.Checked = true;

                }
            }
        }
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

    #region btnAddCity_Click
    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"

        // Passing intId to popup window.
        + "window.open('frmAddCity.aspx?stateid=" + HttpUtility.UrlEncode(ddlState.SelectedValue) + "&distid=" + HttpUtility.UrlEncode(ddlDistrict.SelectedValue)

        + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

        + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);


    }
    #endregion

    #region btnRefresh_Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlDistrict_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region btnBSTPUSave_Click
    protected void btnBSTPUSave_Click(object sender, EventArgs e)
    {

        try
        {
            string Mode = "A";
            string Tag = "";
            if (hdnTid.Value != "")
            {
                clstrans.DeleteTPUMapbyid(hdnTid.Value);

                for (int i = 0; i < gvTPUMap.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = gvTPUMap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                    CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                    HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;
                    Tag = "";
                    if (chk.Checked == true)
                    {

                        int ID = 0;

                        ID = clstrans.SaveTPUMap("", hdnTid.Value, txtTname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);

                        if (ID == 1)
                        {


                        }

                        else if (ID == 2)
                        {


                        }


                    }


                }
            }
            if (hdnTid.Value != "")
            {

                for (int i = 0; i < gvDepot.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell Depotname = gvDepot.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                    CheckBox chk = Depotname.FindControl("ChkIDFactory") as CheckBox;
                    HiddenField hiddenField = Depotname.FindControl("hdnFactoryName") as HiddenField;
                    Tag = "D";
                    if (chk.Checked == true)
                    {
                        int ID = 0;

                        ID = clstrans.SaveDepotMap("", hdnTid.Value, txtTname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);

                    }

                }
            }
            LoadDepotMaster();
            LoadTPUMaster();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlmapping.Style["display"] = "none";

            //string message = "alert('Record Saved Successfully..')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            ClsTransporter CLSPRO = new ClsTransporter();
            int updateid = CLSPRO.UpdateLdomTpuTransporterMaster(hdnTid.Value.Trim());
            MessageBox1.ShowSuccess("<b><font color='green>Record Saved Successfully!</font></b>");

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnBSTPUCancel_Click
    protected void btnBSTPUCancel_Click(object sender, EventArgs e)
    {
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        btnaddhide.Style["display"] = "";
        pnlmapping.Style["display"] = "none";
    }
    #endregion

    #region btngridedit_Click
    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Hdn_Fld.Value != "")
            {
                DataTable dt = new DataTable();
                string userid = Hdn_Fld.Value.ToString();
                dt = clstrans.BindTransporterById(userid);
                txtCode.Text = dt.Rows[0]["CODE"].ToString();
                txtName.Text = dt.Rows[0]["NAME"].ToString();
                txtaddress2.Text = dt.Rows[0]["ADDRESS2"].ToString();
                txtpin2.Text = dt.Rows[0]["PINZIP2"].ToString();
                ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["PHONENO"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                txtPIN.Text = dt.Rows[0]["PINZIP"].ToString();
                txtEmailid.Text = dt.Rows[0]["EMAILID"].ToString();
                txtcontactperson.Text = dt.Rows[0]["CONTACTPERSON"].ToString();
                txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
                txtvatno.Text = dt.Rows[0]["VATNO"].ToString();
                txttinno.Text = dt.Rows[0]["TINNO"].ToString();
                txtSTNo.Text = dt.Rows[0]["STNO"].ToString();
                txtpanno.Text = dt.Rows[0]["PANNO"].ToString();
                txtbankacno.Text = dt.Rows[0]["BANKACNO"].ToString();

                this.txttdslimit.Text = dt.Rows[0]["TDSLIMIT"].ToString();
                this.txtcreditlimit.Text = dt.Rows[0]["CREDIT_LIMIT"].ToString();
                this.txtcreditday.Text = dt.Rows[0]["CREDIT_DAY"].ToString();
                RbApplicable.Text = dt.Rows[0]["MSME_TAG"].ToString();
                ddlMsmeNo.Text = dt.Rows[0]["MSME_NO"].ToString();
                if (dt.Rows[0]["MSME_DATE"].ToString() == "01/01/1900")
                {
                    this.ddlMsmedate.Text = "";
                }
                else
                {
                    this.ddlMsmedate.Text = dt.Rows[0]["MSME_DATE"].ToString();
                }
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
                else
                {
                    ddlMsmeNo.Visible = true;
                    cvddlMsmeNo.Visible = true;
                    ddlMsmedate.Visible = true;
                    imgbtnLRGRCalendar.Visible = true;
                    tdlblApplicable.Visible = true;
                    tdlblApplicable2.Visible = true;
                }

                this.LoadCompnyType();
                this.ddlcompanytype.SelectedValue = Convert.ToString(dt.Rows[0]["COMPANYTYPEID"]).Trim();
                LoadBankName();
                if (dt.Rows[0]["BANKID"].ToString() != "0")
                {
                    ddlbankname.SelectedValue = dt.Rows[0]["BANKID"].ToString();
                }
                else
                {
                    ddlbankname.SelectedValue = dt.Rows[0]["BANKID"].ToString();
                    ddlbankname.SelectedItem.Text = "";
                }

                LoadBranchName();
                if (dt.Rows[0]["BRANCHID"].ToString() != "0")
                {
                    ddlbranchname.SelectedValue = dt.Rows[0]["BRANCHID"].ToString();
                }
                else
                {
                    ddlbranchname.SelectedValue = dt.Rows[0]["BRANCHID"].ToString();
                    ddlbranchname.SelectedItem.Text = "";
                }

                LoadAccGroup();
                if (dt.Rows[0]["ACCGROUPID"].ToString() == "")
                {
                    ddlAccGroup.SelectedValue = "0";
                }
                else
                {
                    ddlAccGroup.SelectedValue = dt.Rows[0]["ACCGROUPID"].ToString();
                }

                this.LoadLedger();

                if (dt.Rows[0]["LEDGER_REFERENCEID"].ToString() != "")
                {
                    //this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                    if (ddlledger.Items.FindByValue(Convert.ToString(dt.Rows[0]["LEDGER_REFERENCEID"])) != null)
                    {
                        this.ddlledger.SelectedValue = dt.Rows[0]["LEDGER_REFERENCEID"].ToString().Trim();
                    }
                }

                if (this.ddlAccGroup.SelectedValue == "0")
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


                txtIFSC.Text = dt.Rows[0]["IFSCCODE"].ToString().Trim();
                if (dt.Rows[0]["ISAPPROVED"].ToString() == "Y")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (dt.Rows[0]["ISTDSDECLARE"].ToString() == "Y")
                {
                    chkTDS.Checked = true;
                }
                else
                {
                    chkTDS.Checked = false;
                }

                if (dt.Rows[0]["SERVICETAX"].ToString() == "Y")
                {
                    chktax.Checked = true;
                }
                else
                {
                    chktax.Checked = false;
                }

                if (dt.Rows[0]["ISACCPOSTING_TOHO"].ToString() != "")
                {
                    this.rdbaccount.SelectedValue = Convert.ToString(dt.Rows[0]["ISACCPOSTING_TOHO"]).Trim();
                }
                else
                {
                    this.rdbaccount.SelectedValue = "N";
                }

                if (dt.Rows[0]["ISTRANSFERTOHO"].ToString() == "Y")
                {
                    this.chkTransferToHO.Checked = true;
                }
                else
                {
                    this.chkTransferToHO.Checked = false;
                }
                if (dt.Rows[0]["REVERSECHARGE"].ToString() == "Y")
                {
                    this.ChkReverseCharge.Checked = true;
                }
                else
                {
                    this.ChkReverseCharge.Checked = false;
                }



                if (dt.Rows[0]["ISMORE_VEHICLE"].ToString() != "")
                {
                    this.rdbvechile.SelectedValue = Convert.ToString(dt.Rows[0]["ISMORE_VEHICLE"]).Trim();
                }
                else
                {
                    this.rdbvechile.SelectedValue = "N";
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
                Hdn_Fld.Value = Convert.ToString(userid);
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
            }


            if (hdnTid.Value != "")
            {
                DataTable dt = clstrans.BindTPUMasterGridbyid(hdnTid.Value.Trim());
                DataTable dt1 = clstrans.BinddepotMasterGridbyid(hdnTid.Value.Trim());
                if (dt.Rows.Count > 0 || dt1.Rows.Count > 0)
                {
                    this.gvTPUMap.DataSource = dt;
                    this.gvTPUMap.DataBind();
                    this.gvDepot.DataSource = dt1;
                    this.gvDepot.DataBind();

                }
                else
                {
                    this.gvDepot.DataSource = null;
                    this.gvDepot.DataBind();
                    this.gvDepot.DataSource = null;
                    this.gvDepot.DataBind();
                }
                //if (Hdn_Fld.Value != "")
                //{
                //    DataTable dt1 = clstrans.BinddepotMasterGridbyid(Hdn_Fld.Value.Trim());
                //    if (dt1.Rows.Count > 0)
                //    {
                //        this.gvDepot.DataSource = dt1;
                //        this.gvDepot.DataBind();

                //    }
                //    else
                //    {
                //        this.gvDepot.DataSource = null;
                //        this.gvDepot.DataBind();
                //    }


                LoadTPUMaster();
                LoadDepotMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                pnlmapping.Style["display"] = "";
            }
        }



        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region gvTransporter_RowDataBound
    protected void gvTransporter_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[18] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Y")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
                GridDataControlFieldCell cell15 = e.Row.Cells[18] as GridDataControlFieldCell;
                string status15 = cell15.Text;

                if (status15 == "Y")
                {
                    cell15.ForeColor = Color.Green;
                }
                else
                {
                    cell15.ForeColor = Color.Red;
                }
                GridDataControlFieldCell cell16 = e.Row.Cells[17] as GridDataControlFieldCell;
                string status16 = cell16.Text;

                if (status16 == "Y")
                {
                    cell16.ForeColor = Color.Green;
                }
                else
                {
                    cell16.ForeColor = Color.Red;
                }

                /*Added By Sayan Dey On 18-01-2018*/
                GridDataControlFieldCell cell19 = e.Row.Cells[20] as GridDataControlFieldCell;
                string Active = cell19.Text;

                if (Active == "Active")
                {
                    cell19.ForeColor = Color.Green;
                }
                else
                {
                    cell19.ForeColor = Color.Red;
                }
                GridDataControlFieldCell cell18 = e.Row.Cells[19] as GridDataControlFieldCell;
                string Stax = cell18.Text;

                if (Stax == "Y")
                {
                    cell18.ForeColor = Color.Green;
                }
                else
                {
                    cell18.ForeColor = Color.Red;
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

    #region CreateLedgerTable
    public DataTable CreateLedgerTable()
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

    #region gvTransporter_Exporting
    protected void gvTransporter_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 13;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "TRANSPORTER DETAILS";
        cell.BackColor = Color.LightGray;
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

    #region gvTransporter_Exported
    protected void gvTransporter_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("TRANSPORTERID", typeof(string)));
        dt.Columns.Add(new DataColumn("TRANSPORTERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("State_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("STATENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTNO", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTADDRESS", typeof(string)));

        HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = dt;

        return dt;
    }
    #endregion

    public void BindState()
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            gvStateWiseGst.DataSource = clstrans.BindStategst();
            gvStateWiseGst.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnSateWiseGst_Click(object sender, EventArgs e)
    {
        try
        {

            ClsTransporter clstrans = new ClsTransporter();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            pnlmapping.Style["display"] = "NONE";
            pnlStateWiseGstMapping.Style["display"] = "";
            BindState();
            CreateDataTable();


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnStateWiseCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            pnlmapping.Style["display"] = "NONE";
            pnlStateWiseGstMapping.Style["display"] = "NONE";
            HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnStateWiseSave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTransporter clstrans = new ClsTransporter();
            if (HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] == null)
            {
                CreateDataTable();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"];
            int count = 0;
            string XML = string.Empty;

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
                    count = count + 1;
                    DataRow dr = DT.NewRow();
                    dr["State_ID"] = lblState_ID.Text.Trim();
                    dr["STATENAME"] = lblState_Name.Text.Trim();
                    dr["TRANSPORTERID"] = hdnTid.Value.Trim();
                    dr["TRANSPORTERNAME"] = txttrasnsporternamegst.Text.Trim();
                    dr["GSTNO"] = txtgstnostatewise.Text.Trim();
                    dr["GSTADDRESS"] = txtgstaddress.Text.Trim();

                    DT.Rows.Add(dr);
                    DT.AcceptChanges();

                }
            }
            HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = DT;

            if (count > 0)
            {
                int id = 0;
                XML = ConvertDatatableToXML(DT);
                id = clstrans.SaveSateWiseGstMapped(this.hdnTid.Value.Trim(), XML);

                if (id > 0)
                {

                    int update = clstrans.UpdateLdomTpuTransporterMaster(hdnTid.Value.Trim());

                    MessageBox1.ShowSuccess("Record saved successfully..");
                    this.gvStateWiseGst.DataSource = null;
                    this.gvStateWiseGst.DataBind();
                    HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record ");
                    HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("Plese Select atleast 1 record  ");
                HttpContext.Current.Session["STATEWISEMAPPINGDETAILS"] = null;
                return;
            }

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            pnlmapping.Style["display"] = "NONE";
            pnlStateWiseGstMapping.Style["display"] = "NONE";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void gvStateWiseGst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dt = clstrans.EditSTATEWISEGSTMAPPING(hdnTid.Value.Trim());
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

    
}