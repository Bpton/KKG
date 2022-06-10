using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_transporterFromSaleOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["MENU"] == "TransporterMaster-Inv")
                {

                }
            }
            else
            {
               
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        ClsTransporter clstrans = new ClsTransporter();
        dt = clstrans.insertTransporter(this.txtTransporterName.Text);
        if(dt.Rows.Count>0)
        {
            if (dt.Rows[0]["MESSAGE"].ToString()== "Done")
            {
                string strPopup = "<script language='javascript' ID='script1'>"
                   + "window.close()"
                   + "</script>";
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else if(dt.Rows[0]["MESSAGE"].ToString() == "Already exists")
            {
                this.lblShowMessage.Text = "Already exists";
                return;
            }
            else
            {
                this.lblShowMessage.Text = "Error Occured";
                return;
            }
           
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"
                   + "window.close()"
                   + "</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
}