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
using System.Text;
using Obout.Interface;
using System.Drawing;
using System.Configuration;



public partial class VIEW_frmInsuranceCompanyMaster : System.Web.UI.Page
{
    ClsInsuranceCompanyMaster clsinsurance = new ClsInsuranceCompanyMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";

                LoadInsuranceMaster();
                LoadState();
               
                LoadBankName();
                LoadBranchName();
               
               
                
                //Hdn_Tag.Value = "O";
               
                ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
                ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
              
                this.LoadAccountGroup();
            }
        }
        catch (Exception ex)
        {
           string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadState()
    {
        try
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
            ddlState.DataSource = clsinsurance.BindState();
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_ID";
            ddlState.DataBind();
        }
        catch (Exception ex)
        {
           string message = "alert('" + ex.Message.Replace("'", "") + "')";
           
        }
    }
   
    public void LoadDistrict(int stateid)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
            ddlDistrict.DataSource = clsinsurance.BindDistrict(stateid);
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
           string message = "alert('" + ex.Message.Replace("'", "") + "')";
            
        }
    }

    public void LoadCity(int cityid)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
            ddlCity.DataSource = clsinsurance.BindCity(cityid);
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_ID";
            ddlCity.DataBind();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            
        }
    }

   
    public void LoadBankName()
    {
        ddlbankname.Items.Clear();
        ddlbankname.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        ddlbankname.DataSource = clsinsurance.BindBankName();
        ddlbankname.DataTextField = "BANKNAME";
        ddlbankname.DataValueField = "ID";
        ddlbankname.DataBind();
    }
    public void LoadBranchName()
    {
        ddlbranchname.Items.Clear();
        ddlbranchname.Items.Insert(0, new ListItem("--Select Branch--", "0"));
        ddlbranchname.DataSource = clsinsurance.BindBrachName();
        ddlbranchname.DataTextField = "BRANCH";
        ddlbranchname.DataValueField = "ID";
        ddlbranchname.DataBind();
    }
   
    public void LoadInsuranceMaster()
    {
            gvInsurancecompany.DataSource = clsinsurance.BindinsuranceGrid();
            gvInsurancecompany.DataBind();
       
    }

    public void Resetfilled()
    {
        this.LoadAccountGroup();
        txtCode.Text = "";
        txtName.Text = "";
        txtAddress.Text = "";
        txtPIN.Text = "";
        txtPIN1.Text = "";
        txtPhoneNo.Text = "";
        txtMobileNo.Text = "";
        txtEmailid.Text = "";
        txtcontactperson.Text = "";
        txtcstno.Text = "";

        txtpanno.Text = "";
        txttinno.Text = "";
        txtbankacno.Text = "";

        ddlState.SelectedValue = "0";

        ddlDistrict.SelectedValue = "0";
        ddlbankname.SelectedValue = "0";
        ddlbranchname.SelectedValue = "0";
        ddlCity.SelectedValue = "0";

        chkActive.Checked = true;
        txtPhoneNo1.Text = "";
        txtMobileNo1.Text = "";

        txtIFSC.Text = "";
        txtAddress1.Text = "";
        chkTDS.Checked = true;
        Hdn_Fld.Value = "";
        LoadState();

        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));

        ddlCity.Items.Clear();
        ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));

        ddlaccountgroup.SelectedValue = "0";
    }
    protected void btnAddTPUVendor_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadAccountGroup();
            txtCode.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtPIN.Text = "";
            txtPIN1.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            txtcontactperson.Text = "";
            txtcstno.Text = "";
           
            txtpanno.Text = "";
            txttinno.Text = "";
            txtbankacno.Text = "";
          
            ddlState.SelectedValue = "0";
           
            ddlDistrict.SelectedValue = "0";
            ddlbankname.SelectedValue = "0";
            ddlbranchname.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
           
            chkActive.Checked = true;           
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
          
            txtIFSC.Text = "";
            txtAddress1.Text = "";
            chkTDS.Checked = true;
            Hdn_Fld.Value = "";
            LoadState();
            
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
           
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
           
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            
            ddlaccountgroup.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadAccountGroup()
    {
        
        ddlaccountgroup.Items.Clear();
        ddlaccountgroup.Items.Insert(0, new ListItem("-Select Account Group-", "0"));
        ddlaccountgroup.DataSource = clsinsurance.BindAccountgrp();
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

    protected void btnInsuranceSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Mode = "";
            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
            }


            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string xmlledger = string.Empty;
            string bindinsuranceid = string.Empty;
            int ledger = 0;
            DataTable dt = clsinsurance.BindGridBranch();


            int ID = 0;
            if (txtPhoneNo.Text.Trim() != "" || txtMobileNo.Text.Trim() != "" || txtMobileNo1.Text.Trim() != "" || txtPhoneNo1.Text.Trim() != "")
            {

                ID = clsinsurance.SaveinsuranceMaster(Hdn_Fld.Value, txtCode.Text.Trim(), txtName.Text.Trim(), (Convert.ToInt32(ddlCity.SelectedValue)), ddlCity.SelectedItem.Text.Trim(),
                   (Convert.ToInt32(ddlDistrict.SelectedValue)), ddlDistrict.SelectedItem.Text.Trim(), (Convert.ToInt32(ddlState.SelectedValue)), ddlState.SelectedItem.Text.Trim(),
                    txtAddress.Text.Trim(), txtAddress1.Text.Trim(), txtMobileNo.Text.Trim(), txtPhoneNo.Text.Trim(), txtMobileNo1.Text.Trim(),
                    txtPhoneNo1.Text.Trim(), txtPIN.Text.Trim(), txtPIN1.Text.Trim(),
                    txtEmailid.Text.Trim(), txtcontactperson.Text.ToString(), txtcstno.Text.ToString(), txttinno.Text.Trim(), txtpanno.Text.Trim(),
                    txtbankacno.Text.Trim(), txtIFSC.Text.Trim(),
                    ddlbankname.SelectedValue.Trim(), ddlbankname.SelectedItem.Text.ToString(), ddlbranchname.SelectedValue.Trim(),
                    ddlbranchname.SelectedItem.Text.Trim(), Mode, chkTDS.Checked.ToString(), chkActive.Checked.ToString(), 
                    ddlaccountgroup.SelectedValue, ddlaccountgroup.SelectedItem.Text.Trim());


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
                bindinsuranceid = clsinsurance.Bindinsuranceid(txtName.Text.Trim());

                xmlledger = ConvertDatatableToXML(dtledger);


                if (Hdn_Fld.Value == "")
                {
                    ViewState["modeinsurance"] = "A";
                }
                else
                {
                    ViewState["modeinsurance"] = "U";
                }
                string Modedelete = ViewState["modeinsurance"].ToString();

                ledger = clsinsurance.SaveAccInfo(txtName.Text.Trim(), ddlaccountgroup.SelectedValue.Trim(), Session["FINYEAR"].ToString(), Hdn_Fld.Value.Trim(), xmlledger, bindinsuranceid.Trim(), Modedelete);
                
                if (ID == 1)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully!");
                    LoadInsuranceMaster();
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    Hdn_Fld.Value = "";
                    txtAddress1.Text = "";
                    //Hdn_Tag.Value = "";
                    ddlaccountgroup.SelectedValue = "0";
                    Session["LEDGERDETAILS"] = null;
                    Resetfilled();
                   

                }
                else if (ID == 2)
                {
                    //string message = "alert('Code already exist..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowInfo("</b> Code already exist..");
                }
                else if (ID == 3)
                {
                    //string message = "alert('Name already exist..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowInfo("</b> Name already exist..");
                }
            }
            else
            {
                this.txtMobileNo.Focus();
                //string message = "alert('Please enter at least 1 contact number..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("</b>Please enter at least 1 contact number..");
            }
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnInsuranceCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtCode.Text = "";
            txtName.Text = "";
           
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtPIN.Text = "";
            txtPIN1.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            txtcontactperson.Text = "";
            txtcstno.Text = "";
           
            txtpanno.Text = "";
            txttinno.Text = "";
            txtbankacno.Text = "";         
            ddlState.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
           
            ddlbankname.SelectedValue = "0";
            ddlbranchname.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
            
            chkActive.Checked = false;           
            txtPhoneNo1.Text = "";
            txtMobileNo1.Text = "";
           
            txtIFSC.Text = "";
            chkTDS.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            ddlaccountgroup.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = clsinsurance.DeleteinsuranceMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadInsuranceMaster();
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
            ddlDistrict.DataSource = clsinsurance.BindDistrict(Convert.ToInt32(ddlState.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

   
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
            ddlCity.DataSource = clsinsurance.BindCity(Convert.ToInt32(ddlDistrict.SelectedValue));
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_ID";
            ddlCity.DataBind();
            btnAddCity.Enabled = true;
            btnRefresh.Enabled = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
           string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
      
    }

    
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
         ddlDistrict_SelectedIndexChanged(sender, e);
    }

  
     protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        

        string strPopup = "<script language='javascript' ID='script1'>"

        // Passing intId to popup window.
        + "window.open('frmAddBankbrnch.aspx?brnchid=" + HttpUtility.UrlEncode(ddlbranchname.SelectedValue)

        + "','new window', 'top=200, left=1000, width=300, height=100, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

        + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

      
    }
     protected void btnRefresh1_Click(object sender, EventArgs e)
     {
         LoadBranchName();
     }

    

     protected void btngridedit_Click(object sender, EventArgs e)
     {

         try
         {
             if (Hdn_Fld.Value !="")
             {
                 string userid = Hdn_Fld.Value.ToString();
                 DataTable dt = new DataTable();
                 dt = clsinsurance.BindInsuranceById(userid);
                 if (dt.Rows.Count > 0)
                 {
                     txtCode.Text = dt.Rows[0]["CODE"].ToString();
                     txtName.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                
                 
                    
                    
                     ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                     LoadDistrict(Convert.ToInt32(ddlState.SelectedValue));
                     ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                     LoadCity(Convert.ToInt32(ddlDistrict.SelectedValue));
                     if (ddlCity.Items.Count > 1)
                     {
                         ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                     }
                     txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                     if (dt.Rows[0]["ADDRESS1"].ToString() != "")
                     {

                         txtAddress1.Text = dt.Rows[0]["ADDRESS1"].ToString();
                     }
                     else
                     {
                         txtAddress1.Text = "";
                     }
                     txtPhoneNo.Text = dt.Rows[0]["PHONENO"].ToString();
                     txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                     txtPhoneNo1.Text = dt.Rows[0]["PHONENO1"].ToString();
                     txtMobileNo1.Text = dt.Rows[0]["MOBILENO1"].ToString();
                     txtPIN.Text = dt.Rows[0]["PINZIP"].ToString();
                     txtPIN1.Text = dt.Rows[0]["PINZIP1"].ToString();
                     txtEmailid.Text = dt.Rows[0]["EMAILID"].ToString();
                     txtcontactperson.Text = dt.Rows[0]["CONTACTPERSON"].ToString();
                     txtcstno.Text = dt.Rows[0]["CSTNO"].ToString();
                 
                     txttinno.Text = dt.Rows[0]["TINNO"].ToString();
                     txtpanno.Text = dt.Rows[0]["PANNO"].ToString();
                     
                     txtbankacno.Text = dt.Rows[0]["BANKACNO"].ToString();
                     txtIFSC.Text = dt.Rows[0]["IFSCCODE"].ToString();
                     LoadBankName();
                     ddlbankname.SelectedValue = dt.Rows[0]["BANKID"].ToString();
                     this.LoadBranchName();
                     ddlbranchname.SelectedValue = dt.Rows[0]["BRANCHID"].ToString();
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
                     LoadAccountGroup();
                     if (dt.Rows[0]["ACCCODE"].ToString() != "")
                     {
                         ddlaccountgroup.SelectedValue = dt.Rows[0]["ACCCODE"].ToString();
                     }
                 }

                 pnlAdd.Style["display"] = "";
                 pnlDisplay.Style["display"] = "none";
                 
                 btnaddhide.Style["display"] = "none";
                

             }

             else
             {
                 pnlAdd.Style["display"] = "none";
                 pnlDisplay.Style["display"] = "";
                 btnaddhide.Style["display"] = "";
                 LoadInsuranceMaster();

             }
         }
         catch (Exception ex)
         {
             string message = "alert('" + ex.Message.Replace("'", "") + "')";
             ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
         }
     }


     protected void gvInsurancecompany_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Y")
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

}
