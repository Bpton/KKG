using System;
using System.Data;
using System.Web.UI;
using PPBLL;

public partial class VIEW_frmProcessMaster : System.Web.UI.Page
{
    Process oProcess = new Process();
    DataTable dt = new DataTable();

    #region Page Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                LoadProcessMaster();
                this.lblProcessCode.Style["display"] = "none";
                this.txtProcessCode.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.ToString();
        }
    }
    #endregion

    #region Events
    /// <summary>
    /// Add process
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProcess_Click(object sender, EventArgs e)
    {
        try
        {
            ResetAll();
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            this.lblProcessCode.Style["display"] = "none";
            this.txtProcessCode.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    /// <summary>
    /// Cancel Record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtProcessCode.Text = "";
            txtProcessName.Text = "";
            txtDescription.Text = "";
            hdnfProcessID.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            this.lblProcessCode.Style["display"] = "none";
            this.txtProcessCode.Style["display"] = "none";
            LoadProcessMaster();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfProcessID.Value == "")
            {
                ViewState["mode"] = "I";
            }
            else
            {
                ViewState["mode"] = "U";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;

            ID = oProcess.InsertUpdateProcessMaster(hdnfProcessID.Value, txtProcessCode.Text.ToString(), txtProcessName.Text.ToString(), txtDescription.Text.ToString(), Mode, chkActive.Checked);

            if (ID == 1)
            {
                if (Mode == "I")
                {
                    MessageBox1.ShowSuccess("Record Saved Successfully!");
                }
                else
                {
                    MessageBox1.ShowSuccess("Record Updated Successfully!");
                }
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadProcessMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                hdnfProcessID.Value = "";
            }
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
            if (hdnfProcessID.Value != "")
            {
                string id = hdnfProcessID.Value.ToString();
                this.lblProcessCode.Style["display"] = "";
                this.txtProcessCode.Style["display"] = "";
                dt = oProcess.FetchProcessMasterByID(id);
                if (dt.Rows.Count > 0)
                {
                    txtProcessCode.Text = dt.Rows[0]["ProcessCode"].ToString().Trim();
                    txtProcessName.Text = dt.Rows[0]["ProcessName"].ToString().Trim();
                    txtDescription.Text = dt.Rows[0]["ProcessDescription"].ToString().Trim();
                    if (dt.Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    pnlAdd.Style["display"] = "";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    LoadProcessMaster();
                }
                else
                {
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    LoadProcessMaster();
                }
            }
            else
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                LoadProcessMaster();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region public Method
    public void LoadProcessMaster()
    {
        try
        {
            gvProcess.DataSource = oProcess.FetchProcessMaster();
            gvProcess.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void ResetAll()
    {
        txtProcessCode.Text = "";
        txtProcessName.Text = "";
        txtDescription.Text = "";
        chkActive.Checked = true;
        hdnfProcessID.Value = "";
        pnlAdd.Style["display"] = "none";
        btnaddhide.Style["display"] = "none";
    }
    #endregion

    #region Protected Method

    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ProcessID"] != "")
            {
                int ID = 0;
                ID = oProcess.DeleteProcessRecord(e.Record["ProcessID"].ToString());
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Deleted Successfully!");
                    LoadProcessMaster();
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
    #endregion
}