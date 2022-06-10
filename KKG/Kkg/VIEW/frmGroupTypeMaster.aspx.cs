using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class VIEW_frmGroupTypeMaster : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key3", "<script>MakeStaticHeader_voucher('" + gvAdvType.ClientID + "', 400, '100%' , 25 ,false); </script>", false);
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.LoadGroupTypeGridView();


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnSearch_Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnamesearch.Text != "")
            {
                ClsGroupTypeMaster clsgrouptype = new ClsGroupTypeMaster();
                DataTable dt = clsgrouptype.BindGroupTypeGridView();
                var rows = dt.Select("GROUP_TYPENAME LIKE '%" + txtnamesearch.Text + "%'");
                if (rows.Any())
                {
                    DataTable dtResult = rows.CopyToDataTable();
                    this.gvAdvType.DataSource = dtResult;
                    this.gvAdvType.DataBind();
                }
                else
                {
                    MessageBox1.ShowError("Please Enter Valid Groupname.......");
                }

            }
            else
            {
                this.LoadGroupTypeGridView();
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadGroupTypeGridView
    public void LoadGroupTypeGridView()
    {
        try
        {
            ClsGroupTypeMaster clsgrouptype = new ClsGroupTypeMaster();
            DataTable dt = clsgrouptype.BindGroupTypeGridView();
            if (dt.Rows.Count > 0)
            {
                this.gvAdvType.DataSource = dt;
                this.gvAdvType.DataBind();
            }
            else
            {
                this.gvAdvType.DataSource = null;
                this.gvAdvType.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnAddnewRecord_Click
    protected void btnAddnewRecord_Click(object sender, EventArgs e)
    {
        try
        {

            txtCode.Text = "";
            txtName.Text = "";
            this.chkActive.Checked = true;
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            lblCode.Style["display"] = "none";
            txtCode.Style["display"] = "none";



        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnGroupTypeSubmit_Click
    protected void btnGroupTypeSubmit_Click(object sender, EventArgs e)
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

            int ID = 0;

            string cbu = HttpContext.Current.Session["UserID"].ToString();
            string Status = string.Empty;
            if (chkActive.Checked)
            {
                Status = "Y";
            }
            else
            {
                Status = "N";
            }

            ClsGroupTypeMaster clsgrouptype = new ClsGroupTypeMaster();
            ID = clsgrouptype.SaveGroupTypeAMaster(Hdn_Fld.Value, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtDescription.Text.Trim(), cbu, Status, Mode);
            if (ID == 1)

            {
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadGroupTypeGridView();
                pnlAdd.Style["display"] = "none";

                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                txtDescription.Text = "";

            }
            else if (ID == 2)
            {

                MessageBox1.ShowInfo("Code already exist..");
            }
            else if (ID == 3)
            {

                MessageBox1.ShowInfo("Name already exist..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnGroupTypeCancel_Click
    protected void btnGroupTypeCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            this.chkActive.Checked = false;
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadGroupTypeGridView();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region DeleteRecord
    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button delete = (Button)sender;
            GridViewRow gvr = delete.NamingContainer as GridViewRow;
            Label lblGROUP_TYPEID = (Label)gvr.FindControl("lblGROUP_TYPEID");
            if (lblGROUP_TYPEID.Text.Trim() != "")
            {
                int ID = 0;
                ClsGroupTypeMaster clsgrouptype = new ClsGroupTypeMaster();
                ID = clsgrouptype.DeleteGroupTypeMaster(lblGROUP_TYPEID.Text.Trim());
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Deleted Successfully.");
                    LoadGroupTypeGridView();
                }
                else
                {
                    MessageBox1.ShowSuccess("Error On Deleting");
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngridedit_Click
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            txtCode.Style["display"] = "none";
            txtCode.Text = "";

            Button edit = (Button)sender;
            GridViewRow gvr = edit.NamingContainer as GridViewRow;
            Label lblGROUP_TYPEID = (Label)gvr.FindControl("lblGROUP_TYPEID");
            ClsGroupTypeMaster clsadvtype = new ClsGroupTypeMaster();
            DataTable dt = new DataTable();
            Hdn_Fld.Value = lblGROUP_TYPEID.Text.Trim();
            dt = clsadvtype.BindGroupTypeGridViewEdit(lblGROUP_TYPEID.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                this.txtCode.Text = Convert.ToString(dt.Rows[0]["GROUP_CODE"]).Trim();
                this.txtName.Text = Convert.ToString(dt.Rows[0]["GROUP_TYPENAME"]).Trim();
                this.txtDescription.Text = Convert.ToString(dt.Rows[0]["DESCRIPTIONS"]).Trim();
                string chkstatus = Convert.ToString(dt.Rows[0]["STATUS"]).Trim();
                if (chkstatus == "Y")
                {
                    this.chkActive.Checked = true;
                }
                else
                {
                    this.chkActive.Checked = false;
                }
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                this.gvAdvType.DataSource = dt;
                this.gvAdvType.DataBind();
                LoadGroupTypeGridView();
            }
            else
            {
                txtCode.Text = "";
                txtName.Text = "";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    #endregion
}