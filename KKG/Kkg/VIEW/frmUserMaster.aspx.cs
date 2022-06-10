using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Web.Services;

public partial class VIEW_frmUserMaster : System.Web.UI.Page
{
    ClsUserMaster ClsUserMaster = new ClsUserMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldistributor').multiselect({ includeSelectAllOption: true  });});</script>", false);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key3", "<script>MakeStaticHeader_voucher('" + gvUser.ClientID + "', 400, '100%' , 42 ,false); </script>", false);

            if (!IsPostBack)
            {
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.pnlBrand.Style["display"] = "none";
                this.plnMenuMapping.Style["display"] = "none";
                this.pnlDepotMapping.Style["display"] = "none";
                this.pnlBSmap.Style["display"] = "none";
                this.pnlbrandMAPPING.Style["display"] = "none";
                this.pnldistributor.Style["display"] = "none";
                // this.divTsiType.Style["display"] = "none";
                //this.LoadUserTypeMaster();
                //this.ddltpu.Visible = false;
                this.LoadStoreLocation();
                loadMenuid();
                this.ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                this.pnlBeatMap.Style["display"] = "none";
                //this.LoadBeatGrid();
                this.ddlreportingtomap.Items.Clear();
                this.ddlreportingtomap.Items.Add(new ListItem("Select Role", "0"));
                this.ddlreportingtomap.AppendDataBoundItems = true;
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.LoadHeadQuater();
                this.Loadtype();
                this.Loaddepot();
                /*this.ddlusertypesearch.SelectedValue = "DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB"; /*TSI*/
                this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
            }
            this.txtpassword.Attributes.Add("value", txtpassword.Text);
            this.ddlUserStatus.Items[0].Attributes.Add("style", "color:green;");
            this.ddlUserStatus.Items[1].Attributes.Add("style", "color:red;");
            //HttpContext.Current.Session["MENUMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region Load Type
    public void Loadtype()
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        DataTable dt = ClsUserMaster.BindType();
        this.ddlusertypesearch.Items.Clear();
        this.ddlusertypesearch.Items.Insert(0, new ListItem("ALL", "0"));
        if (dt.Rows.Count > 0)
        {
            this.ddlusertypesearch.DataSource = dt;
            this.ddlusertypesearch.DataTextField = "UTNAME";
            this.ddlusertypesearch.DataValueField = "UTID";
            this.ddlusertypesearch.DataBind();
        }
        else
        {
            this.ddlusertypesearch.Items.Clear();
            this.ddlusertypesearch.Items.Insert(0, new ListItem("ALL", "0"));
        }
    }
    #endregion

    #region Load Depot
    public void Loaddepot()
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        DataTable dt = ClsUserMaster.BindSearchDepot();
        this.ddldepotsearch.Items.Clear();
        this.ddldepotsearch.Items.Insert(0, new ListItem("ALL", "0"));
        if (dt.Rows.Count > 0)
        {
            this.ddldepotsearch.DataSource = dt;
            this.ddldepotsearch.DataTextField = "BRNAME";
            this.ddldepotsearch.DataValueField = "BRID";
            this.ddldepotsearch.DataBind();
        }
        else
        {
            this.ddldepotsearch.Items.Clear();
            this.ddldepotsearch.Items.Insert(0, new ListItem("ALL", "0"));
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    [WebMethod]
    public static int CheckUserNameAvailability(string username)
    {
        try
        {
            ClsUserMaster ClsUserMaster = new ClsUserMaster();
            int flag = ClsUserMaster.CheckUserName(username);
            return flag;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadUserMaster(string UserType, string DepotID, string SearchName)
    {
        try
        {
            this.gvUser.DataSource = ClsUserMaster.BindUserGrid(UserType, DepotID, SearchName);
            this.gvUser.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadVendor(string tputag)
    {
        this.gvPTPUMap.DataSource = ClsUserMaster.BindVendor(tputag);
        this.gvPTPUMap.DataBind();
    }

    public void LoadDepot(string depottag)
    {
        this.gvPTPUMap.DataSource = ClsUserMaster.BindDepot(depottag);
        this.gvPTPUMap.DataBind();
    }

    public void LoadUserTypeMaster(string applicableto)
    {
        this.ddlUserType.Items.Clear();
        this.ddlUserType.Items.Add(new ListItem("Select", "0"));
        this.ddlUserType.AppendDataBoundItems = true;
        this.ddlUserType.DataSource = ClsUserMaster.BindUserType(applicableto);
        this.ddlUserType.DataValueField = "UTID";
        this.ddlUserType.DataTextField = "UTNAME";
        this.ddlUserType.DataBind();
    }

    public void LoadDepartment()
    {
        this.ddldept.Items.Clear();
        this.ddldept.Items.Add(new ListItem("Select", "0"));
        this.ddldept.AppendDataBoundItems = true;
        this.ddldept.DataSource = ClsUserMaster.BindDepartment();
        this.ddldept.DataValueField = "DEPTID";
        this.ddldept.DataTextField = "DEPTNAME";
        this.ddldept.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //this.LoadUserTypeMaster();
            this.LoadDepartment();
            this.txtusername.Text = "";
            this.txtpassword.Text = "";
            this.txtusername.Enabled = true;
            this.txtpassword.Enabled = true;
            this.txtpassword.Visible = true;
            this.lblpassword.Visible = true;
            this.txtFName.Text = "";
            this.txtMName.Text = "";
            this.txtLName.Text = "";
            this.txtEmail.Text = "";
            this.txtMobile.Text = "";
            this.txtDOB.Text = "";
            this.txtAnvDate.Text = "";
            this.txtpassword.Attributes["value"] = "";
            this.PhoneTextBox1.Text = "";
            this.rdoGender.ClearSelection();
            this.txtAddress.Text = "";
            this.txtpin.Text = "";
            this.ddlUserType.SelectedValue = "0";
            this.ddlReportingTo.SelectedValue = "0";
            this.ddlReportToRole.SelectedValue = "0";
            this.ddlUserStatus.Items[0].Attributes.Add("style", "color:green;");
            this.ddlUserStatus.Items[1].Attributes.Add("style", "color:red;");
            this.Hdn_Fld.Value = "";
            this.Hdn_Fld.Value = "";
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
            this.ddlReportToRole.SelectedValue = "0";
            this.divtxtpasswordedit.Visible = false;
            this.divtxtpassword.Visible = true;
            this.txtpasswordedit.Text = "";
            this.divaddbtn.Visible = false;
            this.ddlheadquater.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlReportToRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindReportingToUserEdit();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void BindReportingToUser()
    {

        if (this.ddlReportToRole.SelectedValue != "0")
        {
            this.GetReportingTo(this.ddlReportToRole.SelectedValue);
        }
    }

    protected void BindReportingToUserEdit()
    {
        if (this.ddlReportToRole.SelectedValue != "0")
        {
            //GetReportingToEdit();
            this.GetReportingTo(this.ddlReportToRole.SelectedValue);//ADDED BY PRITAM BASU FOR SO NAME //
        }
    }

    protected void ddlReportingTo_SelectedIndexChanged(object sender, EventArgs e)//ADDED BY PRITAM BASU FOR HQ NAME  USERWISE//
    {
        try
        {
            this.LoadHeadQuaterUserWise();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private void LoadHeadQuaterUserWise() //ADDED BY PRITAM BASU FOR HQ NAME  USERWISE//
    {
        ClsUserMaster clsUserMaster = new ClsUserMaster();
        DataTable dt = new DataTable();
        dt = clsUserMaster.BindHeadquaterUserWise(this.ddlReportingTo.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            this.ddlheadquater.Items.Clear();
            this.ddlheadquater.Items.Add(new ListItem("Select", "0"));
            this.ddlheadquater.AppendDataBoundItems = true;
            this.ddlheadquater.DataSource = dt;
            this.ddlheadquater.DataValueField = "HQID";
            this.ddlheadquater.DataTextField = "HQNAME";
            this.ddlheadquater.DataBind();
        }
        else
        {
            this.ddlheadquater.Items.Clear();
            this.ddlheadquater.Items.Add(new ListItem("Select", "0"));
        }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindReportingTo();
            // this.divTsiType.Style["display"] = "none"; // ADDED BY PRITAM BASU FOR HIDE DIV OF TSI ON 19022019 //

            DataTable dt = ClsUserMaster.BindParamUserType();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.ddlUserType.SelectedValue.ToString().Trim() == dt.Rows[i]["USERTYPEID"].ToString().Trim())
                    {
                        if (dt.Rows[i]["TPU"].ToString() == "T" || dt.Rows[i]["TPU"].ToString() == "QC" || dt.Rows[i]["TPU"].ToString() == "F" || dt.Rows[i]["TPU"].ToString() == "FQA")
                        {
                            // this.divTsiType.Style["display"] = "none";
                            divddl.Style["display"] = "";
                            LoadVendor(dt.Rows[i]["TPU"].ToString().Trim());
                            if (dt.Rows[i]["TPU"].ToString().Trim() == "F" || dt.Rows[i]["TPU"].ToString().Trim() == "FQA")
                            {
                                this.gvPTPUMap.Columns[1].HeaderText = "FACTORY MAPPING";
                            }
                            else
                            {
                                this.gvPTPUMap.Columns[1].HeaderText = "TPU MAPPING";
                            }
                            return;
                        }
                        else if (dt.Rows[i]["TPU"].ToString().Trim() == "D")
                        {
                            // this.divTsiType.Style["display"] = "none";
                            this.divddl.Style["display"] = "";
                            this.LoadDepot(dt.Rows[i]["TPU"].ToString());
                            this.gvPTPUMap.Columns[1].HeaderText = "DEPOT MAPPING";
                            return;
                        }
                        else if (dt.Rows[i]["TPU"].ToString().Trim() == "EXPU")
                        {
                            // this.divTsiType.Style["display"] = "none";
                            this.divddl.Style["display"] = "";
                            this.LoadDepot("D");
                            //vendorheadername.HeaderText = "MOTHERDEPOT/DEPOT MAPPING";
                            this.gvPTPUMap.Columns[1].HeaderText = "MOTHER DEPOT/DEPOT MAPPING";
                            return;
                        }
                    }
                    else
                    {
                        if (this.rdbapplicableto.SelectedValue == "C" || this.rdbapplicableto.SelectedValue == "T")
                        {
                            if (this.ddlUserType.SelectedValue != "DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB")
                            {
                                this.divddl.Style["display"] = "";
                                //this.divTsiType.Style["display"] = "none";
                                this.LoadDepot("D");
                                //vendorheadername.HeaderText = "MOTHERDEPOT/DEPOT MAPPING";
                                this.gvPTPUMap.Columns[1].HeaderText = "MOTHER DEPOT/DEPOT MAPPING";
                                return;
                            }
                            else
                            {
                                this.divddl.Style["display"] = "none";
                                // this.divTsiType.Style["display"] = "";

                            }
                        }
                        else
                        {
                            this.divddl.Style["display"] = "none";
                            // this.divTsiType.Style["display"] = "none";

                        }
                    }
                }
            }
            else
            {
                this.divddl.Style["display"] = "none";
                // this.divTsiType.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void BindReportingTo()
    {
        if (this.ddlUserType.SelectedValue != "0")
        {
            ClsUserMaster clsUserMaster = new ClsUserMaster();
            DataTable dt = new DataTable();
            dt = clsUserMaster.BindReporintToRole(this.ddlUserType.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.ddlReportToRole.Items.Clear();
                this.ddlReportToRole.Items.Add(new ListItem("Select", "0"));
                this.ddlReportToRole.AppendDataBoundItems = true;
                this.ddlReportToRole.DataSource = dt;
                this.ddlReportToRole.DataValueField = "UTID";
                this.ddlReportToRole.DataTextField = "UTNAME";
                this.ddlReportToRole.DataBind();
            }
        }
    }

    public void GetReportingTo(string utid)
    {
        if (utid != "")
        {
            this.ddlReportingTo.Items.Clear();
            this.ddlReportingTo.Items.Add(new ListItem("Select", "0"));
            this.ddlReportingTo.AppendDataBoundItems = true;
            this.ddlReportingTo.DataSource = ClsUserMaster.BindReporintTo(utid);
            this.ddlReportingTo.DataValueField = "USERID";
            this.ddlReportingTo.DataTextField = "USERNAME";
            this.ddlReportingTo.DataBind();
        }
    }
    public void GetReportingToEdit()
    {
        ClsUserMaster clsUserMaster = new ClsUserMaster();
        this.ddlReportingTo.Items.Clear();
        this.ddlReportingTo.Items.Add(new ListItem("Select", "0"));
        this.ddlReportingTo.AppendDataBoundItems = true;
        this.ddlReportingTo.DataSource = clsUserMaster.BindReporintToEdit();
        this.ddlReportingTo.DataValueField = "USERID";
        this.ddlReportingTo.DataTextField = "USERNAME";
        this.ddlReportingTo.DataBind();
    }

    private void LoadHeadQuater()
    {

        ClsUserMaster clsUserMaster = new ClsUserMaster();
        DataTable dt = new DataTable();
        dt = clsUserMaster.BindHeadquater();
        if (dt.Rows.Count > 0)
        {
            this.ddlheadquater.Items.Clear();
            this.ddlheadquater.Items.Add(new ListItem("Select", "0"));
            this.ddlheadquater.AppendDataBoundItems = true;
            this.ddlheadquater.DataSource = dt;
            this.ddlheadquater.DataValueField = "HQID";
            this.ddlheadquater.DataTextField = "HQNAME";
            this.ddlheadquater.DataBind();
        }
        else
        {
            this.ddlheadquater.Items.Clear();
            this.ddlheadquater.Items.Add(new ListItem("Select", "0"));
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string status = "";
            int MappingID = 0;
            string Mode = "A";
            string ip_address = Request.UserHostAddress.ToString().Trim();
            string DOB = string.Empty;
            string AnvDate = string.Empty;

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
            }

            if (this.txtDOB.Text.Trim() == "")
            {
                DOB = "01/01/1900";
            }
            else
            {
                DOB = this.txtDOB.Text.Trim();
            }

            if (this.txtAnvDate.Text.Trim() == "")
            {
                AnvDate = "01/01/1900";
            }
            else
            {
                AnvDate = this.txtAnvDate.Text.Trim();
            }

            if (this.gvPTPUMap.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvPTPUMap.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkIDTPU");
                    Label lblVENDORID = (Label)gvrow.FindControl("lblVENDORID");
                    Label lblVENDORNAME = (Label)gvrow.FindControl("lblVENDORNAME");

                    if (chk.Checked == true)
                    {
                        count = count + 1;
                    }
                }
            }

            if (count == 0 && this.gvPTPUMap.Rows.Count > 0 && this.ddlUserType.SelectedValue != "DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB")
            {
                MessageBox1.ShowInfo("Please Select atleast One Depot..");
                return;
            }

            status = ClsUserMaster.SaveUserMaster(this.txtcode.Text.Trim(), this.Hdn_Fld.Value.Trim(), this.txtusername.Text.ToString(), this.txtpassword.Text.ToString(),
                                                this.txtFName.Text.ToString(), this.txtMName.Text.ToString(), this.txtLName.Text.ToString(), this.txtEmail.Text.ToString(),
                                                this.txtMobile.Text.ToString(), this.PhoneTextBox1.Text.ToString(), this.rdoGender.SelectedValue, this.txtAddress.Text.ToString(),
                                                this.txtpin.Text.ToString(), this.ddlUserType.SelectedValue.ToString(), Convert.ToInt32(this.ddlUserStatus.SelectedValue), Mode,
                                                Convert.ToString(this.ddldept.SelectedValue), Convert.ToString(this.ddldept.SelectedItem), this.ddlReportingTo.SelectedValue.ToString(),
                                                this.ddlReportingTo.SelectedItem.Text, this.rdbapplicableto.SelectedValue.Trim(),
                                                this.ddlReportToRole.SelectedValue.Trim(), this.ddlheadquater.SelectedValue.Trim(),
                                                this.ddlheadquater.SelectedItem.ToString().Trim(), DOB, AnvDate, ip_address, this.txtempcode.Text.ToString(), this.txtthirdpartyname.Text.ToString()
                                                /*this.ddlTsiType.SelectedValue*/);


            if (status == "-1")
            {
                MessageBox1.ShowInfo("<b>UserName already exist into system, Please try another!</b>", 50, 450);
                return;
            }
            else if (status == "1")
            {
                if (this.gvPTPUMap.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in gvPTPUMap.Rows)
                    {
                        CheckBox chk = (CheckBox)gvrow.FindControl("ChkIDTPU");
                        Label lblVENDORID = (Label)gvrow.FindControl("lblVENDORID");
                        Label lblVENDORNAME = (Label)gvrow.FindControl("lblVENDORNAME");

                        if (chk.Checked == true)
                        {
                            count = count + 1;

                            if (count > 0)
                            {
                                MappingID = ClsUserMaster.SaveUserTPUMapping(lblVENDORID.Text.Trim(), this.txtusername.Text.ToString(), this.txtpassword.Text.ToString(), Mode, this.Hdn_Fld.Value.Trim());
                            }
                        }
                    }
                }

                MessageBox1.ShowSuccess("<b><font color='green'>Record Saved Successfully..</font></b>");
                this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
                Session["dt"] = null;
                this.rdoGender.ClearSelection();
                this.txtcode.Text = "";
                this.txtempcode.Text = "";
                this.rdbapplicableto.ClearSelection();
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.divddl.Style["display"] = "none";
                // this.divTsiType.Style["display"] = "none";
                this.Hdn_Fld.Value = "";
                this.Hdn_Fld.Value = "";
                this.divaddbtn.Style["display"] = "";
                this.divaddbtn.Visible = true;
                this.ddlheadquater.SelectedValue = "0";
                this.txtDOB.Text = "";
                this.txtAnvDate.Text = "";
                this.txtthirdpartyname.Text = "";

            }
            else
            {
                MessageBox1.ShowInfo(status, 60, 600);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtpassword.Attributes["value"] = "";
            this.txtpasswordedit.Attributes["value"] = "";
            this.ddldept.SelectedValue = "0";
            this.txtcode.Text = "";
            this.txtempcode.Text = "";
            this.txtusername.Text = "";
            this.txtpassword.Text = "";
            this.txtpasswordedit.Text = "";
            this.txtFName.Text = "";
            this.txtMName.Text = "";
            this.txtLName.Text = "";
            this.txtEmail.Text = "";
            this.txtMobile.Text = "";
            this.PhoneTextBox1.Text = "";
            this.rdoGender.ClearSelection();
            this.rdbapplicableto.ClearSelection();
            this.txtAddress.Text = "";
            this.txtpin.Text = "";
            this.ddlUserType.SelectedValue = "0";
            this.Hdn_Fld.Value = "";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divddl.Style["display"] = "none";
            // this.divTsiType.Style["display"] = "none";
            this.divtxtpasswordedit.Visible = false;
            this.divtxtpassword.Visible = true;
            this.divaddbtn.Visible = true;
            this.divaddbtn.Style["display"] = "";
            this.ddlheadquater.SelectedValue = "0";
            this.txtDOB.Text = "";
            this.txtAnvDate.Text = "";
            this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
            Session["dt"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            int ID = 0;
            ID = ClsUserMaster.DeleteUserMaster(lblUserID.Text);
            if (ID > 0)
            {
                MessageBox1.ShowSuccess("<b><font color='green'>User deleted successfully!</font></b>");
                this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
            }
            else
            {
                MessageBox1.ShowError("<b><font color='green'>User deleted unsuccessful!</font></b>");
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 600);
        }
    }

    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            this.Hdn_Fld.Value = lblUserID.Text;
            DataTable dt = new DataTable();
            this.LoadDepartment();
            dt = ClsUserMaster.BindUserinformation(this.Hdn_Fld.Value.Trim());
            if (dt.Rows.Count > 0)
            {
                ViewState["rdbapplicableto"] = Convert.ToString(dt.Rows[0]["TPU"]);
                this.rdbapplicableto.SelectedValue = Convert.ToString(dt.Rows[0]["APPLICABLETO"]).Trim();

                // LoadUserTypeMaster(rdbapplicableto.SelectedValue);
                this.BindRadioButton();
                ListItem item = ddlUserType.Items.FindByValue(Convert.ToString(dt.Rows[0]["USERTYPE"]));
                if (item == null)
                {
                    //ddlUserType.Items.FindByText("2").Selected = true;
                    MessageBox1.ShowInfo("<font color='red'>This role can't be edited!</font>");  // Because usertype is -1
                }
                else
                {
                    //this.BindReportingTo();
                    this.ddlUserType.SelectedValue = Convert.ToString(dt.Rows[0]["USERTYPE"]);
                    this.BindReportingTo();
                    if (Convert.ToString(dt.Rows[0]["REPORTTOROLEID"]) != "")
                    {
                        this.ddlReportToRole.SelectedValue = Convert.ToString(dt.Rows[0]["REPORTTOROLEID"]);
                        this.BindReportingToUserEdit();
                        this.ddlReportingTo.SelectedValue = Convert.ToString(dt.Rows[0]["REPORTINGTOID"]);
                    }

                    this.ddldept.SelectedValue = Convert.ToString(dt.Rows[0]["DEPARTMENTID"]);
                    this.txtcode.Text = Convert.ToString(dt.Rows[0]["CODE"]);
                    this.txtempcode.Text = Convert.ToString(dt.Rows[0]["EMPLOYEECODE"]);
                    this.txtthirdpartyname.Text = Convert.ToString(dt.Rows[0]["THIRDPARTYNAME"]);
                    this.txtusername.Text = Convert.ToString(dt.Rows[0]["USERNAME"]);
                    //this.txtusername.Enabled = false;
                    this.divtxtpassword.Visible = false;
                    this.txtpassword.Text = Convert.ToString(dt.Rows[0]["PASSWORD"]);
                    this.txtpassword.Enabled = false;
                    this.txtpassword.Visible = false;
                    this.lblpassword.Visible = false;
                    this.divaddbtn.Visible = false;
                    this.divtxtpasswordedit.Visible = true;
                    this.txtpasswordedit.Text = Convert.ToString(dt.Rows[0]["PASSWORD"]);

                    this.txtFName.Text = Convert.ToString(dt.Rows[0]["FNAME"]);
                    this.txtMName.Text = Convert.ToString(dt.Rows[0]["MNAME"]);
                    this.txtLName.Text = Convert.ToString(dt.Rows[0]["LNAME"]);
                    this.txtEmail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                    this.txtMobile.Text = Convert.ToString(dt.Rows[0]["MOBILE"]);
                    this.PhoneTextBox1.Text = Convert.ToString(dt.Rows[0]["TELEPHONE"]);
                    this.rdoGender.SelectedValue = Convert.ToString(dt.Rows[0]["GENDER"]);
                    this.txtAddress.Text = Convert.ToString(dt.Rows[0]["ADDRESS"]);
                    this.txtpin.Text = Convert.ToString(dt.Rows[0]["PIN"]);
                    this.LoadHeadQuater();

                    if (Convert.ToString(dt.Rows[0]["HQID"]) == "")
                    {
                        this.ddlheadquater.SelectedValue = "0";
                    }
                    else
                    {
                        this.ddlheadquater.SelectedValue = Convert.ToString(dt.Rows[0]["HQID"]).Trim();
                    }
                    /* ADDED BY SAYAN DEY ON 05-01-2018*/
                    if (Convert.ToString(dt.Rows[0]["DOB"]).Trim() == "01/01/1900")
                    {
                        this.txtDOB.Text = "";
                    }
                    else
                    {
                        this.txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]).Trim();
                    }
                    if (Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim() == "01/01/1900")
                    {
                        this.txtAnvDate.Text = "";
                    }
                    else
                    {
                        this.txtAnvDate.Text = Convert.ToString(dt.Rows[0]["ANVDATE"]).Trim();
                    }

                    this.ddlUserStatus.SelectedValue = Convert.ToString(dt.Rows[0]["ISACTIVE"]);

                    if (Convert.ToString(dt.Rows[0]["TPU"]) == "T" || Convert.ToString(dt.Rows[0]["TPU"]) == "QC" || Convert.ToString(dt.Rows[0]["TPU"]) == "F" || Convert.ToString(dt.Rows[0]["TPU"]) == "FQA")
                    {
                        this.divddl.Style["display"] = "";
                        this.LoadVendor(Convert.ToString(dt.Rows[0]["TPU"]));
                        if (Convert.ToString(dt.Rows[0]["TPU"]) == "F" || Convert.ToString(dt.Rows[0]["TPU"]).Trim() == "FQA")
                        {
                            //this.vendorheadername.HeaderText = "FACTORY MAPPING";
                            this.gvPTPUMap.Columns[1].HeaderText = "FACTORY MAPPING";
                        }
                        else
                        {
                            //this.vendorheadername.HeaderText = "TPU MAPPING";
                            this.gvPTPUMap.Columns[1].HeaderText = "TPU MAPPING";
                        }
                    }
                    else if (Convert.ToString(dt.Rows[0]["TPU"]) == "D")
                    {
                        this.divddl.Style["display"] = "";
                        this.LoadDepot(Convert.ToString(dt.Rows[0]["TPU"]));
                        //this.vendorheadername.HeaderText = "DEPOT MAPPING";
                        this.gvPTPUMap.Columns[1].HeaderText = "DEPOT MAPPING";
                    }
                    else
                    {
                        if (rdbapplicableto.SelectedValue != "D")
                        {

                            if (ddlUserType.SelectedValue != "DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB")
                            {
                                divddl.Style["display"] = "";
                                LoadDepot("D");
                                //vendorheadername.HeaderText = "MOTHERDEPOT/DEPOT MAPPING";
                                this.gvPTPUMap.Columns[1].HeaderText = "MOTHER DEPOT/DEPOT MAPPING";
                            }
                            else
                            {
                                divddl.Style["display"] = "none";

                            }

                        }
                        else
                        {
                            divddl.Style["display"] = "none";

                        }
                    }
                    //ViewState["HEADER"] = this.gvPTPUMap.Columns[1].HeaderText;
                    this.LoadTPUbyid();
                    this.InputTable.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                }
            }
            else
            {
                this.divaddbtn.Style["display"] = "";
                MessageBox1.ShowInfo("<b>You can not view/edit this user, </br></br><font color='green'>please goto Customer Master for any modification</font>!</b>", 60, 450);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowError(ex.Message, 50, 500);
            this.divaddbtn.Style["display"] = "";
        }
    }

    public void LoadTPUbyid()
    {
        DataTable dt = new DataTable();
        dt = ClsUserMaster.FetchTPUMappping(Convert.ToString(Hdn_Fld.Value));
        Session["dt"] = dt;
    }

    protected void gvPTPUMap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtchk = new DataTable();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.Hdn_Fld.Value != "")
                {

                    dtchk = ClsUserMaster.FetchTPUMappping(Convert.ToString(Hdn_Fld.Value));
                    CheckBox chk = (e.Row.FindControl("ChkIDTPU") as CheckBox);
                    Label lblVENDORID = (e.Row.FindControl("lblVENDORID") as Label);
                    Label lblVENDORNAME = (e.Row.FindControl("lblVENDORNAME") as Label);

                    for (int i = 0; i < dtchk.Rows.Count; i++)
                    {
                        if (dtchk.Rows[i][0].ToString() == lblVENDORID.Text.Trim())
                        {
                            chk.Checked = true;
                            break;
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

    public void LoadStoreLocation()
    {
        string Mode = string.Empty;
        Mode = "S";
        ClsCityMaster clsobj = new ClsCityMaster();
        DataTable dtobj = new DataTable();
        this.ddlStoreLocation.Items.Clear();
        this.ddlStoreLocation.Items.Add(new ListItem("Select", "0"));
        this.ddlStoreLocation.DataSource = clsobj.loadstorelocation(Mode);
        this.ddlStoreLocation.DataValueField = "ID";
        this.ddlStoreLocation.DataTextField = "NAME";
        this.ddlStoreLocation.DataBind();
    }

    public void LoadDistrict(int stateid)
    {
        this.ddlDistrict.Items.Clear();
        this.ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));

        this.ddlDistrict.DataSource = ClsUserMaster.BindDistrict(stateid);
        this.ddlDistrict.DataValueField = "District_ID";
        this.ddlDistrict.DataTextField = "District_Name";
        this.ddlDistrict.DataBind();
    }

    public void LoadBeatGrid(int districtid)
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        this.gvBeatMap.DataSource = ClsUserMaster.BindBeatGridMap(districtid);
        this.gvBeatMap.DataBind();
    }

    public void loadMenuid()
    {
        ClsCityMaster clsobj = new ClsCityMaster();
        string Mode = string.Empty;
        Mode = "M";
        DataTable dt = new DataTable();
        dt = clsobj.loadstorelocation(Mode);
        if (dt.Rows.Count>0)
        {
            this.ddlMenuId.Items.Clear();
            this.ddlMenuId.Items.Add(new ListItem("Select", "0"));
            this.ddlMenuId.DataSource = dt;
            this.ddlMenuId.DataValueField = "id";
            this.ddlMenuId.DataTextField = "PageName";
            this.ddlMenuId.DataBind();
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                this.LoadBeatGrid(Convert.ToInt32(ddlDistrict.SelectedValue));
            }
            else
            {
                this.gvBeatMap.DataSource = null;
                this.gvBeatMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    //protected void ddlSate_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.lbltotalbeatcount.Text = "";
    //        if (ddlState.SelectedValue != "0")
    //        {
    //            int count = 0;
    //            DataTable dt = ClsUserMaster.BindBeatMapInfo(Convert.ToString(this.Hdn_Fld.Value));
    //            if (dt.Rows.Count > 0)
    //            {
    //                this.LoadBeatGrid(0, Convert.ToString(this.Hdn_Fld.Value), Convert.ToInt16(1));

    //                foreach (GridViewRow gvrow in gvBeatMap.Rows)
    //                {
    //                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkIDBEAT");
    //                    Label lblbeatid = (Label)gvrow.FindControl("lblBEAT_ID");
    //                    Label lbltsiid = (Label)gvrow.FindControl("lblTSIID");
    //                    for (int i = 0; i < dt.Rows.Count; i++)
    //                    {
    //                        if (Convert.ToString(dt.Rows[i]["BEAT_ID"]) == Convert.ToString(lblbeatid.Text.Trim()) && Convert.ToString(lbltsiid.Text.Trim()) == Convert.ToString(this.Hdn_Fld.Value))
    //                        {
    //                            chk.Checked = true;
    //                            chk.BackColor = System.Drawing.Color.GreenYellow;
    //                            chk.BorderColor = System.Drawing.Color.GreenYellow;
    //                            gvrow.BackColor = System.Drawing.Color.LightGreen;
    //                            count = count + 1;
    //                        }
    //                    }
    //                }

    //                if (count > 0)
    //                {
    //                    this.lbltotalbeatcount.Text = "Total : " + count + " beat selected.";
    //                }
    //                else
    //                {
    //                    this.lbltotalbeatcount.Text = "";
    //                }
    //            }
    //            else
    //            {
    //                this.LoadBeatGrid(0, Convert.ToString(this.Hdn_Fld.Value), Convert.ToInt16(1));
    //            }
    //        }
    //        else
    //        {
    //            this.ddlDistrict.Items.Clear();
    //            this.ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
    //            this.ddlDistrict.AppendDataBoundItems = true;

    //            this.gvBeatMap.DataSource = null;
    //            this.gvBeatMap.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    }
    //}

    protected void rdbapplicableto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindRadioButton();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void BindRadioButton()
    {
        if (rdbapplicableto.SelectedValue.Trim() == "C" || rdbapplicableto.SelectedValue.Trim() == "D")
        {
            this.LoadUserTypeMaster(rdbapplicableto.SelectedValue);
            this.divddl.Style["display"] = "none";
            this.gvPTPUMap.Columns[1].HeaderText = "MOTHER DEPOT/DEPOT MAPPING";
            this.gvPTPUMap.DataSource = null;
            this.gvPTPUMap.DataBind();
        }
        else
        {
            this.LoadUserTypeMaster(rdbapplicableto.SelectedValue);
            this.divddl.Style["display"] = "none";
            this.gvPTPUMap.Columns[1].HeaderText = "TPU MAPPING";
            this.gvPTPUMap.DataSource = null;
            this.gvPTPUMap.DataBind();
        }

    }

    protected void btnbeatMapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtUsernameMap.Text = lblName.Text;
            this.lbltotalbeatcount.Text = "";

            ClsUserMaster ClsUserMaster = new ClsUserMaster();

            if (Convert.ToString(this.Hdn_Fld.Value) != "")
            {
                this.pnlBeatMap.Style["display"] = "";
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "none";
                this.divaddbtn.Style["display"] = "none";
                LoadStoreLocation();
                loadMenuid();
                DataTable dt = ClsUserMaster.BindStoreLocationInfo(Convert.ToString(this.Hdn_Fld.Value));
                if(dt.Rows.Count>0)
                {
                    this.ddlStoreLocation.SelectedValue = Convert.ToString(dt.Rows[0]["STOREID"]);
                    this.ddlStoreLocation.SelectedItem.Text = Convert.ToString(dt.Rows[0]["STORENAME"]);
                    this.ddlMenuId.SelectedValue = Convert.ToString(dt.Rows[0]["id"]);
                    this.ddlMenuId.SelectedItem.Text = Convert.ToString(dt.Rows[0]["PageName"]);
                }
                else
                {
                    MessageBox1.ShowInfo("No Mapping found");
                }
                
            }
            else
            {
                MessageBox1.ShowInfo("<b>User identity id not found.</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public DataTable CreateDataTableBeatTSIMapping()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("BEATID", typeof(string)));
        dt.Columns.Add(new DataColumn("BEATNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("USERID", typeof(string)));
        dt.Columns.Add(new DataColumn("USERNAME", typeof(string)));

        HttpContext.Current.Session["BEATTSIMAPPING"] = dt;
        return dt;
    }

    protected void btnBeatMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            string Status ="";
            string storeId = this.ddlStoreLocation.SelectedValue.ToString();
            string menuId = this.ddlStoreLocation.SelectedValue.ToString();
            string stroeName = this.ddlStoreLocation.SelectedItem.ToString();
            Status = clsuser.savestorelocation(this.Hdn_Fld.Value.Trim(), this.txtUsernameMap.Text.Trim(), storeId, stroeName,this.ddlMenuId.SelectedValue);
            string result = "";
            result = Status.Substring(0,4); 
            if (result == "Done")
            {
                //int updateid = clsuser.UpdateLdomUserMaster(this.Hdn_Fld.Value.Trim());
                MessageBox1.ShowInfo("<b>" + Status + "</b>", 60, 550);
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.divaddbtn.Style["display"] = "";
                this.pnlBeatMap.Style["display"] = "none";
                this.Hdn_Fld.Value = "";
                this.gvBeatMap.DataSource = null;
                this.gvBeatMap.DataBind();
                this.lbltotalbeatcount.Text = "";
            }
            else
            {
                MessageBox1.ShowError(Status);
                /*ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Beat", "totalbeatmapped(this); ", true);*/
            }
        }
        catch (Exception ex)
        {
            this.divaddbtn.Style["display"] = "";
            HttpContext.Current.Session["BEATTSIMAPPING"] = null;
            this.lbltotalbeatcount.Text = "";
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnBeatMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.Hdn_Fld.Value = "";
            this.ddlDistrict.SelectedValue = "0";
            //this.ddlState.SelectedValue = "0";
            this.InputTable.Style["display"] = "none";
            this.pnlBeatMap.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divaddbtn.Style["display"] = "";
            this.lbltotalbeatcount.Text = "";
           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region Brand Mappig

    //======================ADD BY SUBHODIP DE ON 20.06.2016======================//////////
    # region Btn Brand Grid
    protected void btnItemmapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtBrandusername.Text = lblName.Text;

            this.LoadBrand();
            this.CreateDataTable();

            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dt = clsuser.EditBrand(Convert.ToString(this.Hdn_Fld.Value));
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in grdbrand.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblDIVID = (Label)gvrow.FindControl("lblDIVID");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["BRANDID"]) == Convert.ToString(lblDIVID.Text))
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }

            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlBrand.Style["display"] = "";
            this.pnlBeatMap.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    # region LoadBrand
    public void LoadBrand()
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        DataTable dt = ClsUserMaster.BindBrand();
        if (dt.Rows.Count > 0)
        {
            this.grdbrand.DataSource = dt;
            this.grdbrand.DataBind();
        }
        else
        {
            this.grdbrand.DataSource = null;
            this.grdbrand.DataBind();
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("TSIID", typeof(string)));
        dt.Columns.Add(new DataColumn("TSINAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDNAME", typeof(string)));


        HttpContext.Current.Session["BRANDMAPPING"] = dt;

        return dt;
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

    #region Btn Brand submit
    protected void btnbrandSubmit_Click(object sender, EventArgs e)
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        try
        {
            string xml = string.Empty;
            int Brandcount = 0;
            int ID = 0;
            DataTable dtbrandchk = (DataTable)HttpContext.Current.Session["BRANDMAPPING"];

            if (grdbrand.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in grdbrand.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblDIVID = (Label)gvrow.FindControl("lblDIVID");

                    if (chk != null & chk.Checked)
                    {
                        Brandcount = Brandcount + 1;
                        DataRow dr = dtbrandchk.NewRow();

                        dr["TSIID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                        dr["TSINAME"] = Convert.ToString(txtBrandusername.Text);
                        dr["BRANDID"] = Convert.ToString(lblDIVID.Text);
                        dr["BRANDNAME"] = Convert.ToString(gvrow.Cells[2].Text);

                        dtbrandchk.Rows.Add(dr);
                        dtbrandchk.AcceptChanges();
                    }
                }
                Session["BRANDMAPPING"] = dtbrandchk;
            }

            if (Brandcount == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Brand", 40, 400);
            }
            else
            {
                if (dtbrandchk.Rows.Count > 0)
                {
                    xml = ConvertDatatableToXML(dtbrandchk);
                    ID = ClsUserMaster.SaveBrandMapping(this.Hdn_Fld.Value, xml);
                    if (ID > 0)
                    {
                        int updateid = ClsUserMaster.UpdateLdomUserMaster(Hdn_Fld.Value.Trim());
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        this.Hdn_Fld.Value = "";
                        this.txtBrandusername.Text = "";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlBrand.Style["display"] = "none";
                        this.pnlBeatMap.Style["display"] = "none";
                        this.divaddbtn.Style["display"] = "";
                        this.grdbrand.DataSource = null;
                        this.grdbrand.DataBind();
                        Session["BRANDMAPPING"] = null;
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["BRANDMAPPING"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Brand Cancel
    protected void btnbrandCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.Hdn_Fld.Value = "";
            this.txtBrandusername.Text = "";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.pnlBrand.Style["display"] = "none";
            this.pnlBeatMap.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "";
            this.grdbrand.DataSource = null;
            this.grdbrand.DataBind();
            Session["BRANDMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    //============================================================//
    #endregion

    #region EXPITION MAPPING
    //========================ADD BY SUBHODIO DE ON 08062016====//


    #region Load Menu Name
    public void LoadMenuName()
    {
        ddlmenu.Items.Clear();
        ddlmenu.Items.Add(new ListItem("Select Menu", "0"));
        ddlmenu.AppendDataBoundItems = true;
        ddlmenu.DataSource = ClsUserMaster.BindMenuName();
        ddlmenu.DataValueField = "ID";
        ddlmenu.DataTextField = "PAGENAME";
        ddlmenu.DataBind();
    }
    #endregion

    protected void BindReportingRoleExp()
    {
        ClsUserMaster clsUserMaster = new ClsUserMaster();
        DataTable dt = new DataTable();
        dt = clsUserMaster.BindReportRoleExpmap();
        if (dt.Rows.Count > 0)
        {
            this.ddlreportingrolemap.Items.Clear();
            this.ddlreportingrolemap.Items.Add(new ListItem("--select--", "0"));
            this.ddlreportingrolemap.AppendDataBoundItems = true;
            this.ddlreportingrolemap.DataSource = dt;
            this.ddlreportingrolemap.DataValueField = "UTID";
            this.ddlreportingrolemap.DataTextField = "UTNAME";
            this.ddlreportingrolemap.DataBind();

        }
        else
        {
            this.ddlreportingrolemap.Items.Clear();
            this.ddlreportingrolemap.Items.Add(new ListItem("--select--", "0"));
        }

    }


    protected void ddlreportingrolemap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            this.BindReportingToUserMap();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region BindReportingToUserMap
    protected void BindReportingToUserMap()
    {

        if (this.ddlreportingrolemap.SelectedValue != "0")
        {
            GetReportingTomap(this.ddlreportingrolemap.SelectedValue);


        }

    }
    #endregion

    #region Get_Reporting_To_Map
    public void GetReportingTomap(string utid)
    {
        if (utid != "")
        {
            DataTable dt = ClsUserMaster.BindReporintTo(utid);
            if (dt.Rows.Count > 0)
            {
                this.ddlreportingtomap.Items.Clear();
                this.ddlreportingtomap.Items.Add(new ListItem("Select", "0"));
                this.ddlreportingtomap.AppendDataBoundItems = true;

                this.ddlreportingtomap.DataSource = dt;
                this.ddlreportingtomap.DataValueField = "USERID";
                this.ddlreportingtomap.DataTextField = "USERNAME";
                this.ddlreportingtomap.DataBind();
            }
            else
            {
                this.ddlreportingtomap.Items.Clear();
                this.ddlreportingtomap.Items.Add(new ListItem("Select", "0"));

            }

        }

    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTablemenu()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("MENUID", typeof(string)));
        dt.Columns.Add(new DataColumn("MENUNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REPORTINGROLEID", typeof(string)));
        dt.Columns.Add(new DataColumn("REPOTINGROLENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REPORTINGTOID", typeof(string)));
        dt.Columns.Add(new DataColumn("REPORTINGTONAME", typeof(string)));

        HttpContext.Current.Session["MENUMAPPING"] = dt;
        return dt;

    }
    #endregion


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

    #region btn add
    protected void btnAddmenu_Click(object sender, EventArgs e)
    {
        DataTable dtaddmenu = new DataTable();
        int numberOfRecords = 0;
        if (HttpContext.Current.Session["MENUMAPPING"] != null)
        {
            dtaddmenu = (DataTable)HttpContext.Current.Session["MENUMAPPING"];
        }
        else
        {
            CreateDataTablemenu();
        }

        numberOfRecords = dtaddmenu.Select("MENUID = '" + this.ddlmenu.SelectedValue + "'").Length;
        if (numberOfRecords > 0)
        {
            MessageBox1.ShowInfo("<b><font color=red>" + this.ddlmenu.SelectedItem.ToString() + "</font></b> already exit", 40, 450);
        }
        else
        {
            DataRow dr = dtaddmenu.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["MENUID"] = Convert.ToString(this.ddlmenu.SelectedValue);
            dr["MENUNAME"] = Convert.ToString(this.ddlmenu.SelectedItem.Text).Trim();
            dr["REPORTINGROLEID"] = Convert.ToString(this.ddlreportingrolemap.SelectedValue);
            dr["REPOTINGROLENAME"] = Convert.ToString(this.ddlreportingrolemap.SelectedItem).Trim();
            dr["REPORTINGTOID"] = Convert.ToString(this.ddlreportingtomap.SelectedValue);
            dr["REPORTINGTONAME"] = Convert.ToString(this.ddlreportingtomap.SelectedItem.Text).Trim();
            dtaddmenu.Rows.Add(dr);
            dtaddmenu.AcceptChanges();

            this.ddlreportingrolemap.SelectedValue = "0";
            this.ddlreportingtomap.SelectedValue = "0";
            Session["MENUMAPPING"] = dtaddmenu;
            if (dtaddmenu.Rows.Count > 0)
            {
                this.grdAddmenu.DataSource = dtaddmenu;
                this.grdAddmenu.DataBind();
            }
            else
            {
                this.grdAddmenu.DataSource = null;
                this.grdAddmenu.DataBind();
            }
        }
    }
    #endregion

    #region delete
    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblGUID = (Label)gvr.FindControl("lblGUID");
            DataTable dtdelete = new DataTable();
            dtdelete = (DataTable)Session["MENUMAPPING"];

            DataRow[] drr = dtdelete.Select("GUID='" + Convert.ToString(lblGUID.Text) + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdelete.AcceptChanges();
            }
            Session["MENUMAPPING"] = dtdelete;
            this.grdAddmenu.DataSource = dtdelete;
            this.grdAddmenu.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btn exception mapping
    protected void btnmenumapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtusernamemapping.Text = lblName.Text;
            this.txtuserrolemap.Text = gvr.Cells[5].Text;

            this.CreateDataTablemenu();
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlBrand.Style["display"] = "none";
            this.plnMenuMapping.Style["display"] = "";
            this.divaddbtn.Style["display"] = "NONE";
            this.LoadMenuName();
            //this.BindReportingToMap();
            this.BindReportingRoleExp();

            DataTable dtgirdEdit = (DataTable)HttpContext.Current.Session["MENUMAPPING"];
            DataTable dt = ClsUserMaster.exceptionedit(Hdn_Fld.Value);

            if (dt.Rows.Count > 0)
            {
                this.txtfromdate.Text = Convert.ToString(dt.Rows[0]["FROMDATE"]).Trim();
                this.txttodate.Text = Convert.ToString(dt.Rows[0]["TODATE"]).Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtgirdEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["MENUID"] = Convert.ToString(dt.Rows[i]["MENUID"]).Trim();
                    dr["MENUNAME"] = Convert.ToString(dt.Rows[i]["MENUNAME"]).Trim();
                    dr["REPORTINGROLEID"] = Convert.ToString(dt.Rows[i]["REPORTINGROLEID"]);
                    dr["REPOTINGROLENAME"] = Convert.ToString(dt.Rows[i]["REPOTINGROLENAME"]).Trim();
                    dr["REPORTINGTOID"] = Convert.ToString(dt.Rows[i]["REPORTINGTOID"]);
                    dr["REPORTINGTONAME"] = Convert.ToString(dt.Rows[i]["REPORTINGTONAME"]).Trim();
                    dtgirdEdit.Rows.Add(dr);
                    dtgirdEdit.AcceptChanges();
                }

                HttpContext.Current.Session["STOCKRECORD"] = dtgirdEdit;
                this.grdAddmenu.DataSource = dtgirdEdit;
                this.grdAddmenu.DataBind();
            }
            else
            {
                //MessageBox1.ShowInfo("<b>No records found!</b>");
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
    public string ConvertDatatableToXMLmenu(DataTable dt)
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

    #region btn exception save
    protected void btnusermappingsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtComboRecordsCheck = (DataTable)HttpContext.Current.Session["MENUMAPPING"];
            int id = 0;
            string xml = string.Empty;
            string Mode = string.Empty;
            xml = ConvertDatatableToXMLmenu(dtComboRecordsCheck);
            int fromdate = Convert.ToInt32(Conver_To_ISO(this.txtfromdate.Text.Trim()));
            int todate = Convert.ToInt32(Conver_To_ISO(this.txttodate.Text.Trim()));
            if (dtComboRecordsCheck.Rows.Count > 0)
            {
                if (todate > fromdate)
                {
                    id = ClsUserMaster.SaveMenuMapping(Convert.ToString(this.Hdn_Fld.Value), Convert.ToString(this.txtusernamemapping.Text.Trim()), Convert.ToString(this.txtfromdate.Text.Trim()), Convert.ToString(this.txttodate.Text.Trim()), xml);
                    if (id > 0)
                    {
                        int updateid = ClsUserMaster.UpdateLdomUserMaster(Convert.ToString(this.Hdn_Fld.Value));
                        MessageBox1.ShowSuccess("Record Saved Successfully.."); ;
                        this.grdAddmenu.DataSource = null;
                        this.grdAddmenu.DataBind();
                        this.txtfromdate.Text = "";
                        this.txttodate.Text = "";
                        this.Hdn_Fld.Value = "";
                        this.ddlreportingtomap.SelectedValue = "0";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlBrand.Style["display"] = "none";
                        this.plnMenuMapping.Style["display"] = "none";
                        this.divaddbtn.Style["display"] = "";
                        this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
                        Session["MENUMAPPING"] = null;
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Todate must be greater than Fromdate");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please entry atleast 1 </b>");
            }
        }
        catch (Exception ex)
        {
            Session["MENUMAPPING"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btncancel
    protected void btnusermappingcancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.pnlBrand.Style["display"] = "none";
            this.plnMenuMapping.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "";
            this.grdAddmenu.DataSource = null;
            this.grdAddmenu.DataBind();
            this.txtfromdate.Text = "";
            this.txttodate.Text = "";
            this.Hdn_Fld.Value = "";
            this.ddlreportingtomap.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #endregion


    #region Depot Mapping

    #region BindDepot
    protected void BindDepot()
    {
        try
        {
            ClsUserMaster ClsUserMaster = new ClsUserMaster();
            DataTable dt = ClsUserMaster.BindDepot();
            if (dt.Rows.Count > 0)
            {
                this.gvDepotMapping.DataSource = dt;
                this.gvDepotMapping.DataBind();
            }
            else
            {
                this.gvDepotMapping.DataSource = null;
                this.gvDepotMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTableDepot()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("TPUID", typeof(string)));
        dt.Columns.Add(new DataColumn("USERID", typeof(string)));

        HttpContext.Current.Session["DEPOTMAPPINGDETAILS"] = dt;

        return dt;
    }
    #endregion

    # region DepotMappingButtom
    protected void btnDepotMapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtCustomername.Text = lblName.Text;

            ClsUserTypeMaster clsutmaster = new ClsUserTypeMaster();
            if (clsutmaster.DepotMappingChking(Convert.ToString(this.Hdn_Fld.Value)) == "0")
            {
                this.BindDepot();
                this.CreateDataTableDepot();

                DataTable dt = clsutmaster.Bindsaveddepotbyuserid(Convert.ToString(this.Hdn_Fld.Value));
                if (dt.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in gvDepotMapping.Rows)
                    {
                        CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                        Label lblBRID = (Label)gvrow.FindControl("lblBRID");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[i]["TPUID"]) == Convert.ToString(lblBRID.Text))
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }

                this.pnlDepotMapping.Style["display"] = "";
                this.InputTable.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "none";
                this.divaddbtn.Style["display"] = "none";
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please Go To Customer Master For Depot Mapping</b>", 40, 450);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    # region DepotMappingCancel
    protected void btnDepotMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlDepotMapping.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divaddbtn.Style["display"] = "";
            this.gvDepotMapping.DataSource = null;
            this.gvDepotMapping.DataBind();
            Session["DEPOTMAPPINGDETAILS"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatableToXMLdepot(DataTable dt)
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

    #region DepotMapSubmit
    protected void btnDepotMapSubmit_Click(object sender, EventArgs e)
    {
        ClsUserTypeMaster clsutmaster = new ClsUserTypeMaster();

        try
        {
            string xml = string.Empty;
            int Depotcount = 0;
            int ID = 0;
            DataTable dtDepotRecordsCheck = (DataTable)HttpContext.Current.Session["DEPOTMAPPINGDETAILS"];
            if (gvDepotMapping.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvDepotMapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblBRID = (Label)gvrow.FindControl("lblBRID");

                    if (chk != null & chk.Checked)
                    {
                        Depotcount = Depotcount + 1;
                        DataRow dr = dtDepotRecordsCheck.NewRow();

                        dr["TPUID"] = Convert.ToString(lblBRID.Text);
                        dr["USERID"] = Convert.ToString(this.Hdn_Fld.Value.Trim());

                        dtDepotRecordsCheck.Rows.Add(dr);
                        dtDepotRecordsCheck.AcceptChanges();
                    }
                }
                Session["DEPOTMAPPINGDETAILS"] = dtDepotRecordsCheck;
            }

            if (Depotcount == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Depot / Mother Depot", 60, 500);
            }
            else
            {
                if (dtDepotRecordsCheck.Rows.Count > 0)
                {
                    xml = ConvertDatatableToXMLdepot(dtDepotRecordsCheck);
                    ID = clsutmaster.SaveDepotMapping(this.Hdn_Fld.Value, xml);
                    if (ID > 0)
                    {
                        int updateid = ClsUserMaster.UpdateLdomUserMaster(Convert.ToString(this.Hdn_Fld.Value));
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        this.Hdn_Fld.Value = "";
                        this.txtCustomername.Text = "";
                        this.pnlDepotMapping.Style["display"] = "none";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.divaddbtn.Style["display"] = "";
                        this.gvDepotMapping.DataSource = null;
                        this.gvDepotMapping.DataBind();
                        Session["DEPOTMAPPINGDETAILS"] = null;
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["DEPOTMAPPINGDETAILS"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #endregion

    #region BS MAPPING

    private void LoadBusinessSegment()
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dt = clsuser.BindBusinessSegment();
            if (dt.Rows.Count > 0)
            {
                this.gvbsmapping.DataSource = dt;
                this.gvbsmapping.DataBind();
            }
            else
            {
                this.gvbsmapping.DataSource = null;
                this.gvbsmapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    #region Create DataTable Structure
    public DataTable CreateDataTableBSMapping()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("USERID", typeof(string)));
        dt.Columns.Add(new DataColumn("USERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BSID", typeof(string)));
        dt.Columns.Add(new DataColumn("BSNAME", typeof(string)));

        HttpContext.Current.Session["BUSINESSSEGMENTMAPPING"] = dt;

        return dt;
    }
    #endregion
    protected void btnBSMapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtusernamebsmap.Text = lblName.Text;

            this.LoadBusinessSegment();
            this.CreateDataTableBSMapping();

            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dt = clsuser.BindsavedBusinessSegmentbyuserid(Convert.ToString(this.Hdn_Fld.Value));
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvbsmapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblBSID = (Label)gvrow.FindControl("lblBSID");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["BSID"]) == Convert.ToString(lblBSID.Text))
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }

            this.pnlBSmap.Style["display"] = "";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #region Convert DataTable To XML
    public string ConvertDatatableToXMLBusinessSegment(DataTable dt)
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
    protected void btnBSMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            int bscount = 0;
            int ID = 0;
            string xml = string.Empty;
            DataTable dtcheck = (DataTable)HttpContext.Current.Session["BUSINESSSEGMENTMAPPING"];

            if (gvbsmapping.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvbsmapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblBSID = (Label)gvrow.FindControl("lblBSID");

                    if (chk != null & chk.Checked)
                    {
                        bscount = bscount + 1;
                        DataRow dr = dtcheck.NewRow();

                        dr["USERID"] = Convert.ToString(this.Hdn_Fld.Value.Trim());
                        dr["USERNAME"] = Convert.ToString(this.txtusernamebrandmap.Text.Trim());
                        dr["BSID"] = Convert.ToString(lblBSID.Text.Trim());
                        dr["BSNAME"] = Convert.ToString(gvrow.Cells[2].Text);

                        dtcheck.Rows.Add(dr);
                        dtcheck.AcceptChanges();
                    }
                }
            }
            if (bscount == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Business Segment", 40, 400);
            }
            else
            {
                if (dtcheck.Rows.Count > 0)
                {
                    xml = ConvertDatatableToXMLBusinessSegment(dtcheck);
                    ID = clsuser.SaveBusinessSegmentMapping(Convert.ToString(this.Hdn_Fld.Value), xml);
                    if (ID > 0)
                    {
                        int updateid = ClsUserMaster.UpdateLdomUserMaster(Convert.ToString(this.Hdn_Fld.Value));
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        this.Hdn_Fld.Value = "";
                        this.txtusernamebsmap.Text = "";
                        this.pnlBSmap.Style["display"] = "none";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.divaddbtn.Style["display"] = "";
                        this.gvbsmapping.DataSource = null;
                        this.gvbsmapping.DataBind();
                        Session["BUSINESSSEGMENTMAPPING"] = null;
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["BUSINESSSEGMENTMAPPING"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnBSMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlBSmap.Style["display"] = "none";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divaddbtn.Style["display"] = "";
            this.gvbsmapping.DataSource = null;
            this.gvbsmapping.DataBind();
            Session["BUSINESSSEGMENTMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #endregion

    #region Brand MAPPING

    private void LoadBrandMAPPING()
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dt = clsuser.BindBrandMapping();
            if (dt.Rows.Count > 0)
            {
                this.gvbrandmapping.DataSource = dt;
                this.gvbrandmapping.DataBind();
            }
            else
            {
                this.gvbrandmapping.DataSource = null;
                this.gvbrandmapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    #region Create DataTable Structure
    public DataTable CreateDataTableBRANDMapping()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("USERID", typeof(string)));
        dt.Columns.Add(new DataColumn("USERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDNAME", typeof(string)));

        HttpContext.Current.Session["BRANDMAPPING"] = dt;

        return dt;
    }
    #endregion
    protected void btnbrandmapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtusernamebrandmap.Text = lblName.Text;
            this.LoadBrandMAPPING();
            this.CreateDataTableBRANDMapping();

            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dt = clsuser.BindRowdataboundbrandbyuserid(Convert.ToString(this.Hdn_Fld.Value));
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvbrandmapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblDIVID = (Label)gvrow.FindControl("lblDIVID");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["BRANDID"]) == Convert.ToString(lblDIVID.Text))
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }

            this.pnlbrandMAPPING.Style["display"] = "";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #region Convert DataTable To XML
    public string ConvertDatatableToXMLBrandmapping(DataTable dt)
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
    protected void btnbrandmapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            int bscount = 0;
            int ID = 0;
            string xml = string.Empty;
            DataTable dtcheck = (DataTable)HttpContext.Current.Session["BRANDMAPPING"];

            if (gvbrandmapping.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvbrandmapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("ChkID");
                    Label lblDIVID = (Label)gvrow.FindControl("lblDIVID");

                    if (chk != null & chk.Checked)
                    {
                        bscount = bscount + 1;
                        DataRow dr = dtcheck.NewRow();

                        dr["USERID"] = Convert.ToString(this.Hdn_Fld.Value.Trim());
                        dr["USERNAME"] = Convert.ToString(this.txtusernamebrandmap.Text.Trim());
                        dr["BRANDID"] = Convert.ToString(lblDIVID.Text.Trim());
                        dr["BRANDNAME"] = Convert.ToString(gvrow.Cells[2].Text);

                        dtcheck.Rows.Add(dr);
                        dtcheck.AcceptChanges();
                    }
                }
            }
            if (bscount == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Brand", 40, 400);
                return;
            }
            else
            {
                if (dtcheck.Rows.Count > 0)
                {
                    xml = ConvertDatatableToXMLBrandmapping(dtcheck);
                    ID = clsuser.SaveUserBrandMapping(Convert.ToString(this.Hdn_Fld.Value), xml);
                    if (ID > 0)
                    {
                        int updateid = ClsUserMaster.UpdateLdomUserMaster(Convert.ToString(this.Hdn_Fld.Value));
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        Session["BRANDMAPPING"] = null;
                        this.gvbrandmapping.DataSource = null;
                        this.gvbrandmapping.DataBind();
                        this.Hdn_Fld.Value = "";
                        this.txtusernamebrandmap.Text = "";
                        this.pnlbrandMAPPING.Style["display"] = "none";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.divaddbtn.Style["display"] = "";
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["BRANDMAPPING"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    protected void btnbrandmapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlbrandMAPPING.Style["display"] = "none";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divaddbtn.Style["display"] = "";
            this.gvbrandmapping.DataSource = null;
            this.gvbrandmapping.DataBind();
            Session["BRANDMAPPING"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Distributor Mapping

    private void LoadDistrbutor(string depot)
    {
        DataTable dt = new DataTable();
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            dt = clsuser.BindDistributor(depot);
            if (dt.Rows.Count > 0)
            {
                this.gvdistributor.DataSource = dt;
                this.gvdistributor.DataBind();
            }
            else
            {
                this.gvdistributor.DataSource = null;
                this.gvdistributor.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
        finally
        {
            dt.Dispose();
        }
    }

    public void LoadDepot()
    {
        try
        {
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Add(new ListItem("Select", "0"));
            this.ddldepot.AppendDataBoundItems = true;
            this.ddldepot.DataSource = ClsUserMaster.BindDepot();
            this.ddldepot.DataValueField = "BRID";
            this.ddldepot.DataTextField = "BRNAME";
            this.ddldepot.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadDistrbutor(this.ddldepot.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region Create DataTable Structure
    public DataTable CreateDataTableDistributorMapping()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("USERID", typeof(string)));
        dt.Columns.Add(new DataColumn("USERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));

        HttpContext.Current.Session["DISTROBUTORMAPPING"] = dt;
        return dt;
    }
    #endregion

    protected void btndistributorMapping_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblUserID = (Label)gvr.FindControl("lblUserID");
            Label lblName = (Label)gvr.FindControl("lblName");
            this.Hdn_Fld.Value = lblUserID.Text;
            this.txtuserdistributor.Text = lblName.Text;
            this.lbltotaldistributorcount.Text = "";

            this.gvdistributor.DataSource = null;
            this.gvdistributor.DataBind();
            this.LoadDepot();
            this.CreateDataTableDistributorMapping();
            int count = 0;

            ClsUserMaster clsuser = new ClsUserMaster();
            DataTable dtdepot = clsuser.EidtDistributorDepot(this.Hdn_Fld.Value);
            if (dtdepot.Rows.Count > 0)
            {
                this.ddldepot.SelectedValue = Convert.ToString(dtdepot.Rows[0]["DEPOTID"].ToString());
                this.LoadDistrbutor(this.ddldepot.SelectedValue.Trim());

                DataTable dt = clsuser.EidtDistributor(this.Hdn_Fld.Value);

                foreach (GridViewRow gvrow in gvdistributor.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    Label customerid = (Label)gvrow.FindControl("lblCUSTOMERID");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == customerid.Text)
                        {
                            chk.Checked = true;
                            chk.BackColor = System.Drawing.Color.GreenYellow;
                            chk.BorderColor = System.Drawing.Color.GreenYellow;
                            gvrow.BackColor = System.Drawing.Color.LightGreen;
                            count = count + 1;
                        }
                    }
                }

                if (count > 0)
                {
                    this.lbltotaldistributorcount.Text = "Total : " + count + " party selected.";
                }
                else
                {
                    this.lbltotaldistributorcount.Text = "";
                }
            }
            this.pnldistributor.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.InputTable.Style["display"] = "none";
            this.divaddbtn.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnDistributorCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnldistributor.Style["display"] = "none";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divaddbtn.Style["display"] = "";
            this.gvdistributor.DataSource = null;
            this.gvdistributor.DataBind();
            HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
            this.lbltotaldistributorcount.Text = "";
        }
        catch (Exception ex)
        {
            HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
            this.lbltotaldistributorcount.Text = "";
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnDistributorSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUserMaster clsuser = new ClsUserMaster();
            string Status = "0";

            if (HttpContext.Current.Session["DISTROBUTORMAPPING"] == null)
            {
                this.CreateDataTableDistributorMapping();
            }

            DataTable dt = (DataTable)HttpContext.Current.Session["DISTROBUTORMAPPING"];

            int count = 0;
            foreach (GridViewRow gvrow in gvdistributor.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                Label customerid = (Label)gvrow.FindControl("lblCUSTOMERID");

                if (chk != null & chk.Checked)
                {
                    count = count + 1;
                    DataRow dr = dt.NewRow();

                    dr["USERID"] = Convert.ToString(this.Hdn_Fld.Value.Trim());
                    dr["USERNAME"] = Convert.ToString(this.txtuserdistributor.Text.Trim());
                    dr["CUSTOMERID"] = Convert.ToString(customerid.Text);
                    dr["CUSTOMERNAME"] = Convert.ToString(gvrow.Cells[2].Text);
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
            }

            string xml = ConvertDatatableToXML(dt);
            Status = clsuser.SaveDistributorNew(this.Hdn_Fld.Value.Trim(), this.txtuserdistributor.Text.Trim(), xml);

            if (Status == "1")
            {
                int updateid = clsuser.UpdateLdomUserMaster(this.Hdn_Fld.Value.Trim());
                MessageBox1.ShowSuccess("Customer mapping saved successfully..");

                this.Hdn_Fld.Value = "";
                this.txtuserdistributor.Text = "";
                this.pnldistributor.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
                this.gvdistributor.DataSource = null;
                this.gvdistributor.DataBind();
                this.lbltotaldistributorcount.Text = "";

                if (count == 0)
                {
                    MessageBox1.ShowWarning("You have not mapped any customer..");
                    HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
                    this.lbltotaldistributorcount.Text = "";
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>" + Status + "</b>", 60, 620);
                HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
                //this.lbltotaldistributorcount.Text = "";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Dis", "totaldistributormapped(this); ", true);
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Session["DISTROBUTORMAPPING"] = null;
            this.lbltotaldistributorcount.Text = "";
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    public void LoadBeatGrid(int Districtid, string UserID, int StateID)
    {
        ClsUserMaster ClsUserMaster = new ClsUserMaster();
        this.gvBeatMap.DataSource = ClsUserMaster.BindBeatGridMap(Districtid, UserID, StateID);
        this.gvBeatMap.DataBind();
    }

    

     protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gvUser.PageIndex = e.NewPageIndex;
            this.LoadUserMaster(this.ddlusertypesearch.SelectedValue.Trim(), this.ddldepotsearch.SelectedValue.Trim(), this.txtnamesearch.Text.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ClsUserMaster clsuser = new ClsUserMaster();
        DataTable table = clsuser.userInfoExcelDownload();
        Response.ClearContent();
        Response.Buffer = true;
        string attachment = "attachment; filename=userinfo.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in table.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in table.Rows)
        {
            tab = "";
            for (i = 0; i < table.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";

            }
            Response.Write("\n");
        }

        Response.End();
    }


    protected void btnInactiveStoreLocation_Click(object sender, EventArgs e)
    {
        ClsUserMaster clsuser = new ClsUserMaster();
        string Status = "";
        Status = clsuser.InactiveUserStorelocation(this.Hdn_Fld.Value.Trim());
        if(Status=="Y")
        {
            MessageBox1.ShowSuccess("Inactive Done");
            return;
        }
        else if (Status == "UNF")
        {
            MessageBox1.ShowError("USER NOT FOUND");
            return;
        }
        else
        {
            MessageBox1.ShowError("Please Contact To Support Team");
            return;
        }
    }
}