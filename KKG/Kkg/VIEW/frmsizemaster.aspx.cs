using BAL;
using Obout.Grid;
using System;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmsizemaster : System.Web.UI.Page
{
    ClsItemTypeMaster ClsITMaster = new ClsItemTypeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadSizeType();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadSizeType()
    {
        try
        {
            gvittype.DataSource = ClsITMaster.BindSizeMasterGrid();
            gvittype.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    protected void btnAddItemType_Click(object sender, EventArgs e)
    {
        try
        {
            txtITCode.Text = "";
            txtITName.Text = "";

            txtDescription.Text = "";
            //chkActive.Checked = true;
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
    protected void btnITSubmit_Click(object sender, EventArgs e)
    {
        try
        {
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

                ID = ClsITMaster.SaveSizeTypeMasterGrid(Hdn_Fld.Value, txtITCode.Text.ToString(), txtITName.Text.ToString(), txtDescription.Text.ToString(), Mode);

                if (ID == 1)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully!");
                    LoadSizeType();
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
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnITCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtITCode.Text = "";
            txtITName.Text = "";

            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadSizeType();
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
                int ID = 0;
                ID = ClsITMaster.DeleteSIZEMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";

                    LoadSizeType();
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
    protected void gvittype_RowDataBound(object sender, GridRowEventArgs e)
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