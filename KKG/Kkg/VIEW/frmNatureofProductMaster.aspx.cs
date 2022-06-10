using BAL;
using Obout.Grid;
using System;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmNatureofProductMaster : System.Web.UI.Page
{
    ClsNatureofProductMaster ClsNOPMaster = new ClsNatureofProductMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadNatureofProduct();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadNatureofProduct()
    {
        try
        {
            gvNOP.DataSource = ClsNOPMaster.BindNatureofProductMasterGrid();
            gvNOP.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddNatureofProduct_Click(object sender, EventArgs e)
    {
        try
        {
            txtNOPCode.Text = "";
            txtNOPName.Text = "";
            txtDescription.Text = "";
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
    protected void btnNOPSubmit_Click(object sender, EventArgs e)
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
            ID = ClsNOPMaster.SaveNatureofProductMaster(Hdn_Fld.Value, txtNOPCode.Text.ToString(), txtNOPName.Text.ToString(), txtDescription.Text.ToString(), Mode, chkActive.Checked.ToString());
            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadNatureofProduct();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                //    string message = "alert('Code already exist..')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
    protected void btnNOPCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtNOPCode.Text = "";
            txtNOPName.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadNatureofProduct();
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
            if (e.Record["NOPID"] != "")
            {
                int ID = 0;
                ID = ClsNOPMaster.DeleteNOPMaster(e.Record["NOPID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadNatureofProduct();
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

    protected void gvNOP_RowDataBound(object sender, GridRowEventArgs e)
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