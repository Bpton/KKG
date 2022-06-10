using System;
using System.Web.UI;

public partial class VIEW_frmChildMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>var myPopup = window.open('{0}', 'Cliente','toolbar=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=no','720', '600', 'true');if(!myPopup)alert('a popup was blocked. please make an exception for this site in your popup blocker and try again');myPopup.close();</script>", ""));
            if (Session["IUserID"] == null)
            {
                Session.Abandon();
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.Redirect("frmsession_expired.aspx", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}
