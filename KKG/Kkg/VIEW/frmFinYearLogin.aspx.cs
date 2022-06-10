using BAL;
using DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmFinYearLogin : System.Web.UI.Page
{
    ClsUserLoginame clsFinYear = new ClsUserLoginame();
    string _UserName = string.Empty, _Password = string.Empty, _Address = string.Empty, _ErrorPath = string.Empty;
    int _port = 0;

    DBUtils db = new DBUtils();
    int flag = 0;
    string Updatequery = string.Empty, NOTIFY_SYNC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:detectPopupBlocker(); ", true);
            if (!IsPostBack)
            {
                LoadFinYear();

                if (Session["TPU"].ToString() == "F") // FACTORY ENTRY OFFICER
                {
                    LoadTPU();
                }
                else if (Session["TPU"].ToString() == "T") // TPU 
                {
                    LoadTPU();
                }
                else if (Session["TPU"].ToString() == "QC") // TPU 
                {
                    LoadTPU();
                }
                else if (Session["TPU"].ToString() == "FQA") // FACTORY QC
                {
                    LoadTPU();
                }
                else if (Session["TPU"].ToString() == "D" || Session["TPU"].ToString() == "EXPU") // DEPOT/MOTHERDEPOT/EXPORT
                {
                    LoadDEPOT();
                }
                else if (Session["TPU"].ToString() == "MC" || Session["TPU"].ToString() == "ADMIN") // COMPANY USER
                {
                    LoadBranch();
                }
                else if (Session["TPU"].ToString() == "RT" || Session["TPU"].ToString() == "DI" || Session["TPU"].ToString() == "SUDI" || Session["TPU"].ToString() == "ST" || Session["TPU"].ToString() == "SST")
                {
                    LoadUser(Session["UserID"].ToString());
                }
                else
                {
                    LoadOtherUser();
                }
            }

            foreach (ListItem item in ddlFinYear.Items)
            {
                item.Attributes.CssStyle.Add("color", "black");
            }


            string lastvalue = string.Empty;

            foreach (ListItem item in ddlBranch.Items)
            {
                //item.Attributes.Add("style", "font-weight:bold");
                item.Attributes.CssStyle.Add("color", "black");
                if (item.Value == "-1" || item.Value == "-2" || item.Value == "-4" || item.Value == "-3" || item.Value == "-8")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "blue");
                    lastvalue = item.Value;
                }
            }

            if (lastvalue == "-1" || lastvalue == "-2" || lastvalue == "-4" || lastvalue == "-3" || lastvalue == "-8")
            {
                ddlBranch.Items.Remove(ddlBranch.Items.FindByValue(lastvalue));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnFinYrSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFinYear.SelectedValue == "-- SELECT YEAR --")
            {
                LblMsg.Text = "Please Select Year!";
            }
            else if (ddlBranch.SelectedValue == "-1" && ((Session["TPU"].ToString() == "T") || (Session["TPU"].ToString() == "QC") || (Session["TPU"].ToString() == "FQA") || (Session["TPU"].ToString() == "F")))
            {
                LblMsg.Text = "Please Select TPU/FACTORY!";
            }
            else if (ddlBranch.SelectedValue == "-1" && ((Session["TPU"].ToString() != "T") || (Session["TPU"].ToString() != "QC") || (Session["TPU"].ToString() == "FQA") || (Session["TPU"].ToString() != "F")))
            {
                LblMsg.Text = "Please Select Branch!";
            }
            else
            {
                if ((Session["TPU"].ToString() == "T") || (Session["TPU"].ToString() == "QC") || (Session["TPU"].ToString() == "FQA") || (Session["TPU"].ToString() == "F"))
                {
                    Session["TPUID"] = ddlBranch.SelectedValue.ToString();
                    Session["TPUNAME"] = ddlBranch.SelectedItem.Text;
                }
                else
                {
                    Session["DEPOTID"] = ddlBranch.SelectedValue.ToString();
                    Session["DEPOTNAME"] = ddlBranch.SelectedItem.Text;
                }
                Session["FINYEAR"] = ddlFinYear.SelectedItem.Text;
                Notification();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadFinYear()
    {
        try
        {
            ddlFinYear.DataSource = clsFinYear.BindFinYear();
            ddlFinYear.DataTextField = "FINYEAR";
            ddlBranch.DataValueField = "ID";
            ddlFinYear.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }

    public void LoadBranch()
    {
        try
        {
            ddlBranch.DataSource = clsFinYear.BindBranch();
            ddlBranch.DataTextField = "BRNAME";
            ddlBranch.DataValueField = "BRID";
            ddlBranch.DataBind();

            //if (this.Session["UTNAME"].ToString().ToLower().Trim() == "admin")
            //{
            //    if (this.ddlBranch.Items.FindByValue("24E1EF07-0A41-4470-B745-9E4BA164C837").Value == "24E1EF07-0A41-4470-B745-9E4BA164C837")
            //    {
            //        this.ddlBranch.SelectedValue = "24E1EF07-0A41-4470-B745-9E4BA164C837";      // HO ID
            //    }
            //}
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadUser(string userid)
    {
        try
        {
            DataTable dt = clsFinYear.BindUser(userid);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "NAME";
            ddlBranch.DataValueField = "USERID";
            ddlBranch.DataBind();
            Session["USERID"] = dt.Rows[0]["USERID"].ToString();
            Session["UTNAME"] = dt.Rows[0]["NAME"].ToString();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadOtherUser()
    {
        try
        {
            ddlBranch.DataSource = clsFinYear.BindOtherUser(Session["USERID"].ToString(), Session["UTNAME"].ToString());
            ddlBranch.DataTextField = "UTNAME";
            ddlBranch.DataValueField = "USERID";
            ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadTPU()
    {
        try
        {
            ddlBranch.Items.Clear();
            if (Session["TPU"].ToString() == "F")
            {
                ddlBranch.Items.Add(new ListItem("-- SELECT FACTORY --", "-1"));
            }
            else if (Session["TPU"].ToString() == "T")
            {
                ddlBranch.Items.Add(new ListItem("-- SELECT TPU --", "-1"));
            }
            else
            {
                ddlBranch.Items.Add(new ListItem("-- SELECT TPU/FACTORY --", "-1"));
            }
            ddlBranch.AppendDataBoundItems = true;
            ddlBranch.DataSource = clsFinYear.BindTPU(Session["UserID"].ToString(), Session["TPU"].ToString());
            ddlBranch.DataTextField = "VENDORNAME";
            ddlBranch.DataValueField = "VENDORID";
            ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadDEPOT()
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlBranch.AppendDataBoundItems = true;
            ddlBranch.DataSource = clsFinYear.BindDepot(Session["UserID"].ToString(), Session["TPU"].ToString());
            ddlBranch.DataTextField = "BRNAME";
            ddlBranch.DataValueField = "BRID";
            ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region Open Update Popup
    public void Notification()
    {
        //Sync syn = new Sync();
        //string status = "N";
        //status = syn.Notify_Sync();
        //if (status == "Y")
        //{
        //    this.popup.Show();
        //}
        //else
        //{
        //    Response.Redirect("frmDashBoard.aspx", false);
        //    LblMsg.Text = "";
        //}
        Response.Redirect("frmDashBoard.aspx", false);
    }
    #endregion

    //#region Download File From FTP Server
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        CreateLogFiles Errlog = new CreateLogFiles();
    //        System.Threading.Thread.Sleep(5000);
    //        //string Physical_Path = HttpContext.Current.Request.PhysicalApplicationPath;
    //        string Physical_Path = @"E:\teSt_ftp";

    //        _UserName = ConfigurationManager.AppSettings["ftpUserName"].ToString();
    //        _Password = ConfigurationManager.AppSettings["ftpPassword"].ToString();
    //        _Address = ConfigurationManager.AppSettings["ftpserver"].ToString();
    //        Sync ObjSync = new Sync();
    //        int status = ObjSync.FTPDownload(_UserName, _Password, _Address, Physical_Path);
    //        if (status == -1)
    //        {
    //            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", status + " Path :Page_Load()");
    //            lblerrorMsg.Text = "Check your internet connection !";
    //            lblnetspeed.Text = "May be Your Internet Bandwidth is Less than 2 Mbps. OR Your Antivirus is Blocking this Sync Program.";
    //            //btnCancel.Visible = true;
    //        }
    //        else
    //        {
    //            Response.Redirect("frmAdminLogin.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        CreateLogFiles Errlog = new CreateLogFiles();
    //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
    //    }
    //}
    //#endregion

    #region Download File From FTP Server
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //Sync ObjSync = new Sync();
        //CreateLogFiles Errlog = new CreateLogFiles();
        //try
        //{
        //    this.btnUpdate.Enabled = false;
        //    this.lblerrorMsg.Text = "";
        //    this.lblnetspeed.Text = "";
        //    this.lblantiwires.Text = "";

        //    System.Threading.Thread.Sleep(5000);
        //    string root = @"C:\PRAYAAS\FTP_LOG";
        //    if (!Directory.Exists(root))
        //    {
        //        Directory.CreateDirectory(root);
        //    }

        //    string Physical_Path = HttpContext.Current.Request.PhysicalApplicationPath;
        //    //string Physical_Path = @"E:\teSt_ftp";
        //    /*
        //    _UserName = ConfigurationManager.AppSettings["ftpUserName"].ToString();
        //    _Password = ConfigurationManager.AppSettings["ftpPassword"].ToString();
        //    _Address = ConfigurationManager.AppSettings["ftpserver"].ToString();
        //    _port = 21;
        //    */
        //    /*_ErrorPath = ConfigurationManager.AppSettings["Path"].ToString();*/   /* Please check web.config app key exists or not*/

        //    /*---- THIS LOGIC NEEDED CAUSE WE HAVE ALREADY PUT APP KEY VALUE 'ftp://ifernatix.com/' TO DMS WEB.CONFIG AND FROM VALUE ftp:// WORD MUST BE REMOVED OTHERWISE ERROR ----*/
        //    /*            
        //    char[] commaSeparator = new char[] { '/' };
        //    string[] authors = _Address.Split(commaSeparator, StringSplitOptions.None);
        //    if (authors.Length == 1)
        //    {
        //        _Address = Convert.ToString(authors[0]).Trim();
        //    }
        //    else
        //    {
        //        _Address = Convert.ToString(authors[2]).Trim();
        //    }
        //    */

        //    _ErrorPath = root;
        //    DataTable dtFTPDetails = new DataTable();
        //    dtFTPDetails = ObjSync.FTPDetails();
        //    if (dtFTPDetails.Rows.Count > 0)
        //    {
        //        _Address = Convert.ToString(dtFTPDetails.Rows[0]["ftpserver"]);
        //        _port = Convert.ToInt16(dtFTPDetails.Rows[0]["ftpPort"]);
        //        _UserName = Convert.ToString(dtFTPDetails.Rows[0]["ftpUserName"]);
        //        _Password = Convert.ToString(dtFTPDetails.Rows[0]["ftpPassword"]);
        //    }

        //    int status = ObjSync.FTPDownload(_UserName, _Password, _Address, _port, Physical_Path, _ErrorPath);
        //    if (status == -1)
        //    {
        //        this.btnUpdate.Enabled = true;
        //        Errlog.ErrorLog(_ErrorPath, "FTPDownload methode return flag " + status.ToString());
        //        this.lblerrorMsg.Text = "Please check your internet connection !";
        //        this.lblnetspeed.Text = "May be your internet bandwidth is low or antivirus/firewall is blocking this process.....";
        //        //btnCancel.Visible = true;
        //    }
        //    else
        //    {
        //        this.btnUpdate.Enabled = true;
        //        Session.Abandon();
        //        Response.Buffer = true;
        //        Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
        //        Response.Expires = 0;
        //        Response.CacheControl = "no-cache";
        //        Response.Redirect("frmAdminLogin.aspx");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    this.btnUpdate.Enabled = true;
        //    Errlog.ErrorLog(_ErrorPath, " FTP download error:- " + ex.Message);
        //}
    }
    #endregion
}