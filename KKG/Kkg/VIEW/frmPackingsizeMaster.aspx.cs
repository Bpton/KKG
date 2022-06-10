using BAL;
using Obout.Grid;
using System;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmPackingsizeMaster : System.Web.UI.Page
{
    ClsPackingsizeMaster ClsPSMaster = new ClsPackingsizeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadPackingsizeMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadPackingsizeMaster()
    {
        try
        {
            gvPackingsize.DataSource = ClsPSMaster.BindPackingsizeMasterGrid();
            gvPackingsize.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnAddPackingsize_Click(object sender, EventArgs e)
    {
        try
        {
            txtPSCode.Text = "";
            txtPackingsizeName.Text = "";
            chkActive.Checked = true;
            txtDescription.Text = "";
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
    protected void btnPSSubmit_Click(object sender, EventArgs e)
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

            ID = ClsPSMaster.SavePackingsizeMaster(Hdn_Fld.Value, txtPSCode.Text.ToString(), txtPackingsizeName.Text.ToString(), txtDescription.Text.ToString(), Mode, chkActive.Checked.ToString());

            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadPackingsizeMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
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
                MessageBox1.ShowInfo("Name already exist..");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnPSCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtPSCode.Text = "";
            txtPackingsizeName.Text = "";

            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadPackingsizeMaster();
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

                e.Record["Error"] = "Predefined PackingSize can't be deleted. ";
            }
            else
            {
                if (e.Record["PSID"] != "")
                {
                    int ID = 0;
                    ID = ClsPSMaster.DeletePackingsizeMaster(e.Record["PSID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";
                        LoadPackingsizeMaster();
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
    protected void gvPackingsize_RowDataBound(object sender, GridRowEventArgs e)
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
}