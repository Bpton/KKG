using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmDivisionMaster : System.Web.UI.Page
{
    ClsDivisionMaster ClsDivMaster = new ClsDivisionMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadDIVMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadDIVMaster()
    {
        try
        {
            gvDivision.DataSource = ClsDivMaster.BindDIVMasterGrid();
            gvDivision.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            txtDIVCode.Text = "";
            txtDIVName.Text = "";

            chkActive.Checked = true;
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

            txtDIVCode.Enabled = true;
            txtDIVName.Enabled = true;

            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            btnDIVSubmit.Enabled = true;

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnDIVSubmit_Click(object sender, EventArgs e)
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
            ID = ClsDivMaster.SaveDivMaster(Hdn_Fld.Value, txtDIVCode.Text.ToString(), txtDIVName.Text.ToString(), txtDescription.Text.ToString(), Mode, chkActive.Checked.ToString());
            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("</b> Record Saved Successfully!");
                LoadDIVMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
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
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnDIVCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtDIVCode.Text = "";
            txtDIVName.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadDIVMaster();

            txtDIVCode.Enabled = true;
            txtDIVName.Enabled = true;

            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            btnDIVSubmit.Enabled = true;
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
            if (this.Hdn_Predefined.Value.Trim() == "Y")
            {
                e.Record["Error"] = "Predefined Division can't be delete";
            }
            else
            {
                if (e.Record["DIVID"] != "")
                {
                    int ID = 0;
                    ID = ClsDivMaster.DeleteDIVMaster(e.Record["DIVID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";

                        LoadDIVMaster();
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
    protected void gvDivision_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
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

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            btnDIVSubmit.Enabled = false;
            txtDIVCode.Enabled = false;
            txtDIVName.Enabled = false;
            txtDescription.Enabled = false;
            chkActive.Enabled = false;

            if (Hdn_Fld.Value != "")
            {
                string id = Hdn_Fld.Value.ToString();

                DataTable dt = new DataTable();
                dt = ClsDivMaster.BindDIVMastergrideditbyid(id);
                if (dt.Rows.Count > 0)
                {
                    this.txtDIVCode.Text = dt.Rows[0]["DIVCODE"].ToString().Trim();
                    this.txtDIVName.Text = dt.Rows[0]["DIVNAME"].ToString().Trim();
                    this.txtDescription.Text = dt.Rows[0]["DIVDESCRIPTION"].ToString().Trim();

                    if (dt.Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }

                    this.pnlAdd.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.btnaddhide.Style["display"] = "none";
                }
                else
                {
                    this.pnlAdd.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "";
                }
            }
            else
            {
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}