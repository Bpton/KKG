using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmPrimaryItemTypeMaster : System.Web.UI.Page
{
    ClsItemType clsItemType = new ClsItemType();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadPrimaryItemType();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadPrimaryItemType()
    {
        try
        {
            gvDivision.DataSource = clsItemType.BindPrimaryItemType();
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
            txtItemTypeName.Text = "";
            txtTypeCode.Text = "";
            chkActive.Checked = true;
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            this.chkservicetype.Checked = false;
            this.txtTypeCode.Enabled = true;
            this.txtItemTypeName.Enabled = true;
            this.txtDescription.Enabled = true;
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
            DataTable dtPrimaryCode = new DataTable();
            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
                //dtPrimaryCode = clsItemType.GeneratePrimaryItemCode();
                //this.txtTypeCode.Text = dtPrimaryCode.Rows[0]["CODE"].ToString().Trim();
            }
            else
            {
                ViewState["mode"] = "U";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;
            string Active = string.Empty;
            string servicetype = string.Empty;
            if (this.chkActive.Checked == true)
            {
                Active = "Y";
            }
            else
            {
                Active = "N";
            }

            if (this.chkservicetype.Checked == true)
            {
                servicetype = "Y";
            }
            else
            {
                servicetype = "N";
            }
            ID = clsItemType.SavePrimaryItemTypeMaster(Hdn_Fld.Value, txtTypeCode.Text.Trim(), txtItemTypeName.Text.ToString(), txtDescription.Text.ToString(), Mode, "N", Active, servicetype.Trim(), ddlitemowner.SelectedValue.ToString());
            if (ID == 1)
            {
                MessageBox1.ShowSuccess("</b> Record Saved Successfully!");
                LoadPrimaryItemType();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                this.chkservicetype.Checked = false;
            }
            else if (ID == 3)
            {
                MessageBox1.ShowInfo("</b> Name already exist..");
            }
            else if (ID == 4)
            {
                MessageBox1.ShowInfo("</b> Code already exist..");
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
            txtItemTypeName.Text = "";
            txtTypeCode.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadPrimaryItemType();
            this.chkservicetype.Checked = false;
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
            if (hdnPredefine.Value == "N")
            {
                if (e.Record["ID"] != "")
                {
                    ClsItemType clsItemType = new ClsItemType();
                    int ID = 0;
                    ID = clsItemType.DeletePrimaryItemType(e.Record["ID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";

                        LoadPrimaryItemType();
                    }
                    else
                    {
                        e.Record["Error"] = "Error On Deleting. ";
                    }
                }
            }
            else
            {
                e.Record["Error"] = "Predefined Item Type can't be deleted.";
                //MessageBox1.ShowInfo("Predefine Item Type can't be deleted.");
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
                GridDataControlFieldCell cell = e.Row.Cells[6] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "ACTIVE")
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

    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            //string Predefine = Convert.ToString(hdnPredefine.Value).Trim();
            //if (Predefine == "N")
            //{

                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
                btnaddhide.Style["display"] = "none";
                this.txtTypeCode.Enabled = false;
               this.txtItemTypeName.Enabled = false;
               this.txtDescription.Enabled = false;
                string ID = Convert.ToString(Hdn_Fld.Value).Trim();
                ClsItemType clsItemType = new ClsItemType();
                DataTable dt = new DataTable();
                dt = clsItemType.EditPrimaryItemType(ID);
                if (dt.Rows.Count > 0)
                {
                    //this.ddlitemowner.SelectedValue = Convert.ToString(dt.Rows[0]["ITEMOWNER"]).Trim();
                    this.txtTypeCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]).Trim();
                    this.txtItemTypeName.Text = Convert.ToString(dt.Rows[0]["ITEM_Name"]).Trim();
                    this.txtDescription.Text = Convert.ToString(dt.Rows[0]["ITEMDESC"]).Trim();
                    if (Convert.ToString(dt.Rows[0]["ACTIVE"]).Trim() == "Y")
                    {
                        this.chkActive.Checked = true;
                    }
                    else
                    {
                        this.chkActive.Checked = false;
                    }
                    if (Convert.ToString(dt.Rows[0]["ISSERVICE"]).Trim() == "Y")
                    {
                        this.chkservicetype.Checked = true;
                    }
                    else
                    {
                        this.chkservicetype.Checked = false;
                    }
                }
            //}
            //else
            //{
            //    MessageBox1.ShowInfo("Predefined Item Type can't be edited!");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}