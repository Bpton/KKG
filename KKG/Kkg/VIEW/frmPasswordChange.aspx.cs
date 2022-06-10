using BAL;
using System;
using System.Data;
using System.Web.UI;

public partial class VIEW_frmPasswordChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UTNAME"].ToString() != null)
                {
                    FetchUserInformation();
                    lblusertype.Text = Session["UTNAME"].ToString();
                }
                else
                {
                    Response.Redirect("frmAdminLogin.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void FetchUserInformation()
    {
        try
        {
            ClsUserLoginame userlogin = new ClsUserLoginame();
            DataTable dt = new DataTable();
            dt = userlogin.BindUserDetails(Session["IUSERID"].ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                lblname.Text = dt.Rows[0]["FULLNAME"].ToString().Trim();
                lblusername.Text = dt.Rows[0]["USERNAME"].ToString().Trim();
                if (!string.IsNullOrEmpty(dt.Rows[0]["LASTPASSWORDCHANGE"].ToString().Trim()))
                {
                    lbllastpasswordchange.Text = dt.Rows[0]["LASTPASSWORDCHANGE"].ToString().Trim();
                }
                else
                {
                    lbllastpasswordchange.Text = "NIL";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUserLoginame userlogin = new ClsUserLoginame();
            if (Session["IUserID"].ToString().Trim() != "8")
            {
                int flag = userlogin.PasswordChange(Session["IUserID"].ToString().Trim(), txt_cpassword.Text.Trim(), txt_ccpassword.Text.Trim());
                if (flag > 0)
                {
                    MessageBox1.ShowSuccess("<b><font color='green'>Password change successfully!</font></b>");
                    FetchUserInformation();
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Please enter correct current password!</font></b>", 60, 400);
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        try
        {
            this.txt_cpassword.Text = "";
            this.txt_ccpassword.Text = "";
            this.txt_npassword.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}