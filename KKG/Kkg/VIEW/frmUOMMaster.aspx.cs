using BAL;
using Obout.Grid;
using System;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmUOMMaster : System.Web.UI.Page
{
    ClsUOMMaster ClsUomMaster = new ClsUOMMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadBranchMaster();
            }
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
            gvUom.DataSource = ClsUomMaster.BindUOMMasterGrid();
            gvUom.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddUom_Click(object sender, EventArgs e)
    {
        try
        {
            txtUomCode.Text = "";
            txtUomName.Text = "";
            txtUomDescription.Text = "";
            chkActive.Checked = true;
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
    protected void btnUomSubmit_Click(object sender, EventArgs e)
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

            ID = ClsUomMaster.SaveUOMMaster(Hdn_Fld.Value, txtUomCode.Text.ToString(), txtUomName.Text.ToString(), txtUomDescription.Text.ToString(), Mode, chkActive.Checked.ToString());

            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);      
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadBranchMaster();
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
    protected void btnUomCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtUomCode.Text = "";
            txtUomName.Text = "";
            txtUomDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadBranchMaster();
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
            if (this.Hdn_PREDEFINED.Value.Trim() == "Y")
            {
                e.Record["Error"] = "Predefined UOM can't be deleted. ";
            }
            else
            {
                if (e.Record["UOMID"] != "")
                {
                    int ID = 0;
                    ID = ClsUomMaster.DeleteUOMMaster(e.Record["UOMID"].ToString());
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
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void gvUom_RowDataBound(object sender, GridRowEventArgs e)
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