using BAL;
using System;
using System.Data;
using System.Web;
using Utility;
using DAL;

public partial class VIEW_ErpKkg : System.Web.UI.MasterPage
{
    ClsUserLoginame clsFinYear = new ClsUserLoginame();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IUserID"] == null)
            {
                Session.Abandon();
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.Redirect("frmsession_expired.aspx", true);
            }

            if (!IsPostBack)
            {
                this.Attributes.Add("onLoad", "DisplayDateTime()");
                this.lblUser.Text = Session["FNAME"].ToString();
                this.lblfinyear.Text = Session["FINYEAR"].ToString();
                DataTable dt = new DataTable();
                dt = clsFinYear.POCOUNT(HttpContext.Current.Session["UserID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    this.lblnotify.Text = dt.Rows[0]["POCOUNT"].ToString();
                }
                dt = clsFinYear.GRNCOUNT(HttpContext.Current.Session["FINYEAR"].ToString());
                if (dt.Rows.Count > 0)
                {
                    this.lblgrn.Text = dt.Rows[0]["REJECTIONNUMBER"].ToString();
                }

                if (Convert.ToString(Session["TPU"]).Trim() == "T" || Convert.ToString(Session["TPU"]).Trim() == "QC") // TPU & QC
                {
                    this.dvScheme.Style["display"] = "none";
                }
                else
                {
                    this.dvScheme.Style["display"] = "";
                }
                ClsDashBoard clsdash = new ClsDashBoard();
                dt = clsdash.ALERTSHOW(HttpContext.Current.Session["UserID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    this.dvNotify.Style["display"] = "";

                }
                else
                {
                    this.dvNotify.Style["display"] = "none";
                }

            }
        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }

    }

    protected void btnstart_Click(object sender, EventArgs e)
    {
        try
        {
            DBUtils db = new DBUtils();

            string sqlinsert = "UPDATE T_USER_LOG  set USERID='" + Session["UserID"] + "',LASTLOGOUTTIME=GETDATE() where USERID='" + Session["UserID"] + "' ";
            int result = db.HandleData(sqlinsert);

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

    //protected void btnImg1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Process p = Process.Start(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe");
    //        Thread.Sleep(500); // Allow the process to open it's window
    //        //SetParent(p.MainWindowHandle, this.Handle);
    //    }
    //    catch (Exception ex)
    //    {
    //        CreateLogFiles Errlog = new CreateLogFiles();
    //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
    //    }
    //}
}
