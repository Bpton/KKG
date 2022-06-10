
public partial class VIEW_frmRptAgeinganalysis : System.Web.UI.Page
{
    public string ServiceURL
    {
        get
        {
            return this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/")).LastIndexOf("/")) + "/Services/";
        }
    }
}