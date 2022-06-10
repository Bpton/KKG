using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPBLL;
using Utility;
using Newtonsoft.Json;
using System.Web.Services;

public partial class VIEW_frmIssueApprove : System.Web.UI.Page
{
    public class JsonResult
    {
        public int success { get; set; }
        public Object obj { get; set; }
        public Object obj2 { get; set; }
        public string Msg { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
            }
        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }

    }

    [WebMethod(EnableSession = true)]
    public static string AppFormInitilize(int Opertion, string FromDate, string ToDate)
    {
        JsonResult r = new JsonResult();
        try
        {
               
           ClsIssue objClass = new ClsIssue();
           DataSet objds = new DataSet();
            objds = objClass.LoadPendingIssue(Opertion,FromDate, ToDate, HttpContext.Current.Session["USERID"].ToString().Trim(),HttpContext.Current.Session["FINYEAR"].ToString().Trim());
            r.success = 1;
            r.obj = objds.Tables;
           
        }
        catch (Exception ex)
        {
            r.success = 0;
            r.Msg = ex.Message;
        }
        return JsonConvert.SerializeObject(r);
    }
}