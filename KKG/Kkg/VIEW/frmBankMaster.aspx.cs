using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmBankMaster : System.Web.UI.Page
{
    ClsBankMaster ClsBankMaster = new ClsBankMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadBankMaster();
                this.LoadGroup();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadBankMaster()
    {
        try
        {
            gvBank.DataSource = ClsBankMaster.BindBankMasterGrid();
            gvBank.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region LoadGroup
    public void LoadGroup()
    {
        try
        {
            ClsBankMaster clsbank = new ClsBankMaster();
            DataTable dt = new DataTable();
            dt = clsbank.GetAccountGroupID();
            ddlgroup.Items.Clear();
            //ddlgroup.Items.Insert(0, new ListItem("Select", "0"));
            ddlgroup.DataSource = dt;
            ddlgroup.DataTextField = "GRPNAME";
            ddlgroup.DataValueField = "CODE";
            ddlgroup.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion
    protected void btnAddBank_Click(object sender, EventArgs e)
    {
        try
        {
            txtBankName.Text = "";
            txtbankcode.Text = "";
            chkActive.Checked = true;
            this.txtaddress.Text = "";
            this.txtifsccode.Text = "";
            this.txtswiftcode.Text = "";
            this.txtbranch.Text = "";
            this.txtacountno.Text = "";
            this.chkexport.Checked = false;
            this.chktarget.Checked = false;

            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnBankSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string export = string.Empty;
            string target = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
            }
            else
            {
                ViewState["mode"] = "U";
            }

            if (this.chkexport.Checked == true)
            {
                export = "Y";
            }
            else
            {
                export = "N";
            }

            if (chktarget.Checked)
            {
                target = "Y";
            }
            else
            {
                target = "N";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;
            string bankid = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                //Mode = "A";
                bankid = Guid.NewGuid().ToString().ToUpper(); 
            }
            else
            {
                //Mode = "U";
                bankid = Hdn_Fld.Value.Trim();
               
            }

            ID = ClsBankMaster.SaveBankMaster(Hdn_Fld.Value, txtBankName.Text.ToString(), Mode, chkActive.Checked.ToString(), this.txtaddress.Text.Trim(),
                                              this.txtifsccode.Text.Trim(), this.txtswiftcode.Text.Trim(), this.txtbranch.Text.Trim(), this.txtacountno.Text.Trim(),
                                              export.Trim(), target.Trim(), txtbankcode.Text.Trim(),this.ddlgroup.SelectedValue.Trim(),this.ddlgroup.SelectedItem.ToString(),bankid);

            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadBankMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                //string message = "alert('Code already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowWarning("Code already exist..");

            }
            else if (ID == 3)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadBankMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 6)
            {
                MessageBox1.ShowWarning("Name already exist..");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnBankCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtBankName.Text = "";
            Hdn_Fld.Value = "";
            this.txtaddress.Text = "";
            this.txtifsccode.Text = "";
            this.txtswiftcode.Text = "";
            this.txtbranch.Text = "";
            this.txtacountno.Text = "";
            this.chkexport.Checked = false;
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadBankMaster();
            this.chktarget.Checked = false;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsBankMaster clsbank = new ClsBankMaster();
            DataTable dt = clsbank.BindBankMasterGridEdit(Hdn_Fld.Value);

            if (dt.Rows.Count > 0)
            {
                this.txtBankName.Text = Convert.ToString(dt.Rows[0]["BANKNAME"]).Trim();
                this.txtaddress.Text = Convert.ToString(dt.Rows[0]["ADDRESS"]).Trim();
                this.txtifsccode.Text = Convert.ToString(dt.Rows[0]["IFSCCODE"]).Trim();
                this.txtswiftcode.Text = Convert.ToString(dt.Rows[0]["SWIFTCODE"]).Trim();
                this.txtbranch.Text = Convert.ToString(dt.Rows[0]["BRANCH"]).Trim();
                this.txtacountno.Text = Convert.ToString(dt.Rows[0]["ACOUNTNO"]).Trim();

                if (Convert.ToString(dt.Rows[0]["ACTIVE"]).Trim() == "True")
                {
                    this.chkActive.Checked = true;
                }
                else
                {
                    this.chkActive.Checked = false;
                }
                if (Convert.ToString(dt.Rows[0]["EXPORTTAG"]).Trim() == "Y")
                {
                    this.chkexport.Checked = true;
                }
                else
                {
                    this.chkexport.Checked = false;
                }
                if (Convert.ToString(dt.Rows[0]["ISFORTARGET"]).Trim() == "Y")
                {
                    this.chktarget.Checked = true;
                }
                else
                {
                    this.chktarget.Checked = false;
                }

            }
            else
            {
                txtBankName.Text = "";
                chkActive.Checked = true;
                this.txtaddress.Text = "";
                this.txtifsccode.Text = "";
                this.txtswiftcode.Text = "";
            }
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

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
                ClsBankMaster clsbank = new ClsBankMaster();
                string ProductCheck = clsbank.BankDeleteCheck(e.Record["ID"].ToString());
                if (ProductCheck == "Y")
                {
                    e.Record["Error"] = " You can't delete this bank ledger,it has number of accounts transaction";
                    //MessageBox1.ShowWarning("You can't delete this bank ledger,it has number of accounts transaction");
                    LoadBankMaster();
                }
                else
                {
                    int ID = 0;
                    ID = ClsBankMaster.DeleteBankMaster(e.Record["ID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";

                        LoadBankMaster();
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

    protected void gvBank_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[5] as GridDataControlFieldCell;
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
}