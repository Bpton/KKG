using System;
using System.Web.UI;

public partial class VIEW_frmPrintPopUp_FAC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["BSID"].ToString() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")/*EXPORT GST*/
        {
            chkPrintButton.Items[3].Text = "";
            chkPrintButton.Items[3].Selected = false;
            chkPrintButton.Items[3].Enabled = false;
            //csdbatchdiv.Visible = false;
            //chkPrintButton.Items[5].Text = "";
            //chkPrintButton.Items[5].Selected = false;
            //chkPrintButton.Items[5].Enabled = false;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string upath = "";
        try
        {
            int original = 0;
            int duplicate = 0;
            int triplicate = 0;
            int backpsheet = 0;
            int count = 0;

            if (chkPrintButton.Items[0].Selected)
            {
                original = 1;
                count = count + 1;
            }
            if (chkPrintButton.Items[1].Selected)
            {
                duplicate = 2;
                count = count + 1;
            }
            if (chkPrintButton.Items[2].Selected)
            {
                triplicate = 3;
                count = count + 1;
            }

            if (chkPrintButton.Items[3].Selected)
            {
                backpsheet = 4;
                count = count + 1;
            }
            if (Request.QueryString["BSID"].ToString() == "2E96A0A4-6256-472C-BE4F-C59599C948B0" && Request.QueryString["PSID"].ToString() == "EXPORTGST")
            {
                upath = "frmRptInvoicePrint_FAC.aspx?pid=" + Request.QueryString["pid"].ToString() + "&&BSID=" + Request.QueryString["BSID"].ToString() + "&&PSID=" + "EXPORTGST" + "&&original=" + original.ToString() + "&&duplicate=" + duplicate.ToString() + "&&triplicate=" + triplicate.ToString() + "&&backpsheet=" + backpsheet.ToString() + "&&pcount=" + count.ToString() + "";
            }
            else if (Request.QueryString["pid"].ToString() != "1")
            {
                upath = "frmRptInvoicePrint_FAC.aspx?pid=" + Request.QueryString["pid"].ToString() + "&&BSID=" + Request.QueryString["BSID"].ToString() + "&&original=" + original.ToString() + "&&duplicate=" + duplicate.ToString() + "&&triplicate=" + triplicate.ToString() + "&&backpsheet=" + backpsheet.ToString() + "&&pcount=" + count.ToString() + "";
            }
            else
            {
                upath = "frmRptInvoicePrint_FAC.aspx?Stnid=" + Request.QueryString["Stnid"].ToString() + "&&BSID=" + Request.QueryString["BSID"].ToString() + "&&original=" + original.ToString() + "&&duplicate=" + duplicate.ToString() + "&&triplicate=" + triplicate.ToString() + "&&backpsheet=" + backpsheet.ToString() + "&&pcount=" + count.ToString() + "&&MenuId=" + "46" + "";
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}