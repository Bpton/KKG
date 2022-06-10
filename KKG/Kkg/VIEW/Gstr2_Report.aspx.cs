
#region Namespace
using BAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion


public partial class VIEW_Gstr2_Report : System.Web.UI.Page
{
    public string ServiceURL
    {
        get
        {
            return this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/")).LastIndexOf("/")) + "/Services/";
        }
    }
   
}