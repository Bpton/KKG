using BAL;
using System;

public partial class VIEW_frmAddCity : System.Web.UI.Page
{
    ClsCityMaster clscity = new ClsCityMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btncitySubmit_Click(object sender, EventArgs e)
    {
        try
        {
            String Mode = "A";
            string sid = Request.QueryString["stateid"].ToString();
            string did = Request.QueryString["distid"].ToString();
            int ID = 0;

            ID = clscity.SaveCityMaster(Convert.ToString(""), Convert.ToInt32(sid), Convert.ToInt32(did), txtcity.Text.Trim(), Mode);

            if (ID == 1)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record Saved Scucessfully!";
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcity.Text = "";
                btncitySubmit.Enabled = false;
            }
            else if (ID == 2)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "City already exist!";
                btncitySubmit.Enabled = true;
                //string message = "alert('City already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
}