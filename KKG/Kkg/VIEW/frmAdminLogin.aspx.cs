using BAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Utility;
public partial class VIEW_frmAdminLogin : System.Web.UI.Page
{
    ClsUserLoginame clsLogin = new ClsUserLoginame();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["IsLoggedIn"] = "false";
                Session["TPUID"] = null;
                Session["TPUNAME"] = null;
                if (Request.Cookies["userInfo"] != null)
                {
                    Response.Cookies["userInfo"].Expires = DateTime.Now.AddHours(-1);
                }
                this.LblMsg.Visible = false;
            }

            if (!Page.User.Identity.IsAuthenticated)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>preventBack(); </script>", false);
            }
        }

        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserID"] == null)
            {
                DataTable dt = clsLogin.UserLoginINFO(txtUserName.Text, txtPassword.Text);

                if (dt.Rows.Count > 0)
                {
                    HttpCookie userInfo = new HttpCookie("userInfo");

                    Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                    userInfo["UserID"] = dt.Rows[0]["UserID"].ToString();
                    Session["USERTYPE"] = dt.Rows[0]["USERTYPE"].ToString();
                    userInfo["USERTYPE"] = dt.Rows[0]["UserID"].ToString();
                    Session["UTypeId"] = dt.Rows[0]["USERTYPE"].ToString();
                    userInfo["UTypeId"] = dt.Rows[0]["USERTYPE"].ToString();
                    Session["UTNAME"] = dt.Rows[0]["UTNAME"].ToString();
                    userInfo["UTNAME"] = dt.Rows[0]["UTNAME"].ToString();
                    Session["FNAME"] = dt.Rows[0]["FNAME"].ToString();
                    userInfo["FNAME"] = dt.Rows[0]["FNAME"].ToString();
                    Session["UserName"] = txtUserName.Text.ToString();
                    userInfo["UserName"] = txtUserName.Text.ToString();
                    Session["APPLICABLETO"] = dt.Rows[0]["APPLICABLETO"].ToString().Trim();
                    userInfo["APPLICABLETO"] = dt.Rows[0]["APPLICABLETO"].ToString().Trim();
                    Session["TPU"] = dt.Rows[0]["TPU"].ToString();
                    userInfo["TPU"] = dt.Rows[0]["TPU"].ToString().Trim();
                    Session["IUserID"] = dt.Rows[0]["IUserID"].ToString();
                    userInfo["IUserID"] = dt.Rows[0]["IUserID"].ToString().Trim();
                    Session["USERTAG"] = dt.Rows[0]["USERTAG"].ToString();
                    userInfo["USERTAG"] = dt.Rows[0]["USERTAG"].ToString().Trim();
                    Session["DEPARTMENTID"] = dt.Rows[0]["DEPARTMENTID"].ToString();
                    userInfo["DEPARTMENTID"] = dt.Rows[0]["DEPARTMENTID"].ToString();
                    Response.Cookies.Add(userInfo);
                    Response.Redirect("frmFinYearLogin.aspx", false);

                    lblmsg1.Text = "";
                    this.LblMsg.Visible = false;
                }
                else
                {
                    this.LblMsg.Visible = true;
                    lblmsg1.Text = "Invalid Password!";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            else
            {
                this.LblMsg.Visible = true;
                lblmsg1.Text = "" + Convert.ToString(Session["UserName"]).Trim().ToUpper() + " User is already logged In, kindly use another browser or close the current session.";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            this.LblMsg.Visible = false;
            Session.Clear();
            Session.Abandon();
            Response.Redirect("frmAdminLogin.aspx");
        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            this.lblmsg1.Text = "";
            this.txtUserName.Text = "";
            this.txtPassword.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}