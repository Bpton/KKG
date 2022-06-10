using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptMaterialConsumption : System.Web.UI.Page
{
    public string ServiceURL
    {
        get
        {
            return this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/")).LastIndexOf("/")) + "/Services/";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}