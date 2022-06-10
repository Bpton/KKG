using BAL;
using Obout.Grid;
using System;
using System.Drawing;
using System.Web.UI;
public partial class VIEW_frmFragranceMaster : System.Web.UI.Page
{
    ClsFragnanceMaster ClsfrgMaster = new ClsFragnanceMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadFragnanceMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadFragnanceMaster()
    {
        try
        {

            gvfrg.DataSource = ClsfrgMaster.BindFragnanceMastergrid();
            gvfrg.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    protected void btnAddfrg_Click(object sender, EventArgs e)
    {
        try
        {

            txtfrgCode.Text = "";
            txtfrgName.Text = "";
            txtfrgDescription.Text = "";
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
    protected void btnfrgSubmit_Click(object sender, EventArgs e)
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


            ID = ClsfrgMaster.SaveFragnanceMaster(Hdn_Fld.Value, txtfrgCode.Text.ToString(), txtfrgName.Text.ToString(), txtfrgDescription.Text.ToString(), Mode, chkActive.Checked.ToString());

            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadFragnanceMaster();
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

    protected void btnfrgCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtfrgCode.Text = "";
            txtfrgName.Text = "";

            txtfrgDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadFragnanceMaster();
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

            if (e.Record["FRGID"] != "")
            {
                int ID = 0;
                ID = ClsfrgMaster.DeleteFragnanceMaster(e.Record["FRGID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadFragnanceMaster();
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

    protected void gvfrg_RowDataBound(object sender, GridRowEventArgs e)
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