//using BAL;
//using DAL;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Web;
//using System.Web.Script.Serialization;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Utility;
//using WorkFlow;

//public partial class VIEW_frmDashBoardContent : System.Web.UI.Page
//{
//    ClsUserLoginame clsFinYear = new ClsUserLoginame();
//    string date = "dd/MM/yyyy";
//    string userid = "";
//    string finyear = "";
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        try
//        {
//            if (!IsPostBack)
//            {
//                 userid = HttpContext.Current.Session["USERID"].ToString().Trim();
//                 finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
//                this.LoadAdminBranch();
//            }
//        }
//        catch (Exception ex)
//        {
//            CreateLogFiles Errlog = new CreateLogFiles();
//            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
//        }

//    }

//    public void LoadAdminBranch()
//    {
//        try
//        {

//            this.ddlBranch.Items.Clear();
//            this.ddlBranch.DataSource = clsFinYear.BindBranch();
//            this.ddlBranch.DataTextField = "BRNAME";
//            this.ddlBranch.DataValueField = "BRID";
//            this.ddlBranch.SelectedValue = Session["DEPOTID"].ToString();
//            this.ddlBranch.DataBind();
//        }
//        catch (Exception ex)
//        {
//            string msg = ex.Message;
//        }
//    }

//    public void LoadTPU()
//    {
//        try
//        {
//            this.ddlBranch.Items.Clear();
//            if (Session["TPU"].ToString() == "F")
//            {
//                this.ddlBranch.Items.Add(new ListItem("-SELECT FACTORY-", "-1"));
//            }
//            else if (Session["TPU"].ToString() == "T")
//            {
//                this.ddlBranch.Items.Add(new ListItem("-SELECT TPU-", "-1"));
//            }
//            else
//            {
//                this.ddlBranch.Items.Add(new ListItem("-SELECT TPU/FACTORY-", "-1"));


//            }
//            this.ddlBranch.AppendDataBoundItems = true;
//            this.ddlBranch.DataSource = clsFinYear.BindTPU(Session["UserID"].ToString(), Session["TPU"].ToString());
//            this.ddlBranch.DataTextField = "VENDORNAME";
//            this.ddlBranch.DataValueField = "VENDORID";
//            this.ddlBranch.SelectedValue = Session["TPUID"].ToString();
//            this.ddlBranch.DataBind();
//        }
//        catch (Exception ex)
//        {
//            string msg = ex.Message;
//        }
//    }

//    protected void btnstart_Click(object sender, EventArgs e)
//    {
//        try
//        {
//            DBUtils db = new DBUtils();

//            string sqlinsert = "UPDATE T_USER_LOG  set USERID='" + Session["UserID"] + "',LASTLOGOUTTIME=GETDATE() where USERID='" + Session["UserID"] + "' ";
//            int result = db.HandleData(sqlinsert);

//            Session.Clear();
//            Session.Abandon();
//            Response.Redirect("frmAdminLogin.aspx");
//        }
//        catch (Exception ex)
//        {
//            CreateLogFiles Errlog = new CreateLogFiles();
//            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
//        }
//    }

//    [System.Web.Services.WebMethod]
//    public static string Main()
//    {
//        ClsUserLoginame clsFinYear = new ClsUserLoginame();
//        DataTable objDt = new DataTable();
//        objDt = clsFinYear.BindDashBoardData(HttpContext.Current.Session["USERID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
//        return getdatatabletojson(objDt);
//    }

//    public static string getdatatabletojson(DataTable dt)
//    {
//        string str = string.Empty;
//        JavaScriptSerializer serializer = new JavaScriptSerializer();
//        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
//        Dictionary<string, object> row;
//        foreach (DataRow dr in dt.Rows)
//        {
//            row = new Dictionary<string, object>();
//            foreach (DataColumn col in dt.Columns)
//            {
//                row.Add(col.ColumnName, dr[col]);
//            }
//            rows.Add(row);
//        }
//        str = serializer.Serialize(rows);
//        return str;

//    }

//}

using BAL;
using DAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using WorkFlow;

public partial class VIEW_frmDashBoardContent : System.Web.UI.Page
{
    ClsUserLoginame clsFinYear = new ClsUserLoginame();
    string date = "dd/MM/yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.heightbaradjust.Style["display"] = "none";
                this.swichbarOther.Style["display"] = "none";
                this.div_customer_details.Style["display"] = "none";
                this.div_Workflow.Style["display"] = "none";
                this.divPurchaseStockReceipt.Style["display"] = "none";
                this.swichbarOther_fac.Style["display"] = "none";
                this.dvGraph1.Style["display"] = "none";
                this.dvGraph2.Style["display"] = "none";
                this.dvGraph3.Style["display"] = "none";
               
                this.TD3.Style["display"] = "none";
                this.TD4.Style["display"] = "none";
                this.div_customer_details.Style["display"] = "none";
                TD1.Visible = false;
                TD2.Visible = false;
                this.categorysale.Style["display"] = "none";
                this.primarysale.Style["display"] = "none";
                this.target.Style["display"] = "none";
                this.div2.Style["display"] = "none";
                this.outPopUp.Style["display"] = "";
                loadDynamicMenu();


                /*New Date Logic added on 28-05-2019*/
                string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
                string startyear = finyear.Substring(0, 4);
                int startyear1 = Convert.ToInt32(startyear);
                string endyear = finyear.Substring(5);
                int endyear1 = Convert.ToInt32(endyear);
                DateTime oDate = new DateTime(startyear1, 04, 01);
                DateTime cDate = new DateTime(endyear1, 03, 31);
                CalendarFromDate.StartDate = oDate;
                CalendarExtenderToDate.StartDate = oDate;
                Calendarstart.StartDate = oDate;
                Calendarend.StartDate = oDate;
                DateTime today1 = DateTime.Now;
                DateTime today2 = DateTime.Now.AddDays(-1);
                if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
                {
                    this.txtFromDate.Text = new DateTime(today1.Year, today1.Month, 1).ToString("dd/MM/yyyy");
                    this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    CalendarFromDate.EndDate = today1;
                    CalendarExtenderToDate.EndDate = today1;
                    this.txtstart.Text = new DateTime(today1.Year, today1.Month, 1).ToString("dd/MM/yyyy");
                    this.txtend.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    Calendarstart.EndDate = today1;
                    Calendarend.EndDate = today1;
                    this.start.Text = new DateTime(today1.Year, today1.Month, 1).ToString("dd/MM/yyyy");
                    this.end.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    CalendarAsm.EndDate = today1;
                    CalendarAsmend.EndDate = today1;

                }
                else
                {
                    this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    CalendarFromDate.EndDate = cDate;
                    CalendarExtenderToDate.EndDate = cDate;
                    this.txtstart.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    this.txtend.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    CalendarFromDate.EndDate = cDate;
                    CalendarExtenderToDate.EndDate = cDate;

                    this.start.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    this.end.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    CalendarAsm.EndDate = cDate;
                    CalendarAsmend.EndDate = cDate;
                }

                this.txtAsOnDate.Text = new DateTime(today2.Year, today2.Month, today2.Day).ToString("dd/MM/yyyy");
                CalendarExtenderAsOnDate.EndDate = today1;

                ViewState["StartDate"] = this.txtFromDate.Text.Replace('-', '/').Trim();
                ViewState["EndDate"] = this.txtToDate.Text.Replace('-', '/').Trim();
                ViewState["AsOnDate"] = this.txtAsOnDate.Text.Replace('-', '/').Trim();

                ViewState["SoStartDate"] = this.txtstart.Text.Replace('-', '/').Trim();
                ViewState["SoEndDate"] = this.txtend.Text.Replace('-', '/').Trim();
                ViewState["AsmStartDate"] = this.start.Text.Replace('-', '/').Trim();
                ViewState["AsmEndDate"] = this.end.Text.Replace('-', '/').Trim();

                if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")/*POC-1*/
                {
                    this.divPoc.Style["display"] = "";
                    this.loadPurchaseOrderForApprove();
                }
                else if (Session["USERTYPE"].ToString() == "6A2A199B-4654-4CD8-BF20-D1ACB52D7432")/*POC-2*/
                {
                    this.divPoc.Style["display"] = "";
                    this.loadPurchaseOrderForConfirm();
                }


                if (Session["TPU"].ToString() == "T") // TPU ENTRY OFFICER
                {
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadTPU();
                }
                else if (Session["TPU"].ToString() == "QC") // TPU - QC
                {
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadTPU();
                }
                else if (Session["TPU"].ToString() == "D" || Session["TPU"].ToString() == "EXPU") // DEPOT/MOTHERDEPOT/EXPORT
                {
                    this.swichbarOther.Style["display"] = "";
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadDEPOT();
                }
                else if (Session["TPU"].ToString() == "F") // TPU - QC
                {
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadTPU();
                }
                else if (Session["TPU"].ToString() == "SO") // SO
                {
                    this.heightbaradjust.Style["display"] = "";
                    this.content.Style["display"] = "none";
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                   
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph1.Style["display"] = "";
                    this.sograph.Style["display"] = "";
                    this.dvGraph2.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.LoadCustomerInvoiceStatus();
                    BindChartSo();
                    BindChartSoprimary();
                }
                else if (Session["TPU"].ToString() == "ASM") // ASM
                {
                    this.heightbaradjust.Style["display"] = "";
                    this.content.Style["display"] = "none";
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                   
                    this.div2.Style["display"] = "";
                   
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph1.Style["display"] = "";
                    this.sograph.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "";
                    this.dvGraph2.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "";
                    this.primarysale.Style["display"] = "";
                    this.target.Style["display"] = "";
                    this.invoice.Style["display"] = "none";
                    this.LoadCustomerInvoiceStatus();
                    BindChartAsm();
                    BindChartAsmPrimary();
                }
                else if (Session["TPU"].ToString() == "ADMIN")// ADMIN
                {
                    this.swichbarOther.Style["display"] = "";
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "";
                    this.sograph.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadAdminBranch();
                    this.BindFilterOption();
                    this.BindForeCastChart();
                }
                else if (Session["TPU"].ToString() == "SCM")// Supply Chain Manager
                {
                    this.swichbarOther.Style["display"] = "";
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "";
                    this.sograph.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.LoadAdminBranch();
                    this.BindFilterOption();
                    this.BindForeCastChart();
                }
                else if (Session["TPU"].ToString().Contains("OFF")) // OFFICE
                {
                    this.swichbarOther.Style["display"] = "";
                    this.dvGraph1.Style["display"] = "none";
                    this.dvGraph2.Style["display"] = "none";
                   
                    this.TD3.Style["display"] = "none";
                    this.TD4.Style["display"] = "none";
                    this.sograph.Style["display"] = "none";
                    this.div_customer_details.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";
                    this.primarysale.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                }
                else if (Session["TPU"].ToString() == "MIS C.O.") // MIS.C.O.
                {
                    this.swichbarOther.Style["display"] = "";
                    this.BindChart();
                    this.dvGraph1.Style["display"] = "";
                    this.dvGraph2.Style["display"] = "";
                    this.TD3.Style["display"] = "none";   /*for so dashbord*/
                    this.TD4.Style["display"] = "none";   /*for so dashbord */
                    this.div_customer_details.Style["display"] = "none";    /*for so dashbord*/
                       /*for so dashbord*/
                    this.sograph.Style["display"] = "none";
                    this.dvGraph3.Style["display"] = "none";
                    this.categorysale.Style["display"] = "none";  /*asm*/
                    this.primarysale.Style["display"] = "none";
                    this.asmgraph.Style["display"] = "none";
                    this.target.Style["display"] = "none";
                    this.invoice.Style["display"] = "none";
                    this.div2.Style["display"] = "none";
                }
                else
                {
                    this.LoadBranchForOtherUserType(Convert.ToString(Session["IUserID"]).Trim());
                    if (Session["UTNAME"].ToString() == "Factory Accounts" || Session["UTNAME"].ToString() == "Sr. Account Manager" || Session["TPU"].ToString() == "STORE INCHARGE" || Session["TPU"].ToString() == "FQA-2" || Session["TPU"].ToString() == "PRODUCTION MANAGER" || Session["UTNAME"].ToString() == "Store Executive" || Session["UTNAME"].ToString() == "QC Chemist" || Session["UTNAME"].ToString() == "QC Manager" || Session["UTNAME"].ToString() == "QC Manager Unit-1")//factory qa // FACTORY QC
                    {
                        this.swichbarOther_fac.Style["display"] = "";
                    }

                }
            }
            if (IsPostBack)
            {
                ViewState["StartDate"] = this.txtFromDate.Text.Replace('-', '/').Trim();
                ViewState["EndDate"] = this.txtToDate.Text.Replace('-', '/').Trim();
                ViewState["AsOnDate"] = this.txtAsOnDate.Text.Replace('-', '/').Trim();
                ViewState["SoStartDate"] = this.txtstart.Text.Replace('-', '/').Trim();
                ViewState["SoEndDate"] = this.txtend.Text.Replace('-', '/').Trim();
                ViewState["AsmStartDate"] = this.start.Text.Replace('-', '/').Trim();
                ViewState["AsmEndDate"] = this.end.Text.Replace('-', '/').Trim();
            }
            foreach (ListItem item in ddlBranch.Items)
            {
                if (item.Text == "-SELECT TPU-" || item.Text == "-- SELECT BRANCH --" || item.Text == "---- MOTHERDEPOT ----" || item.Text == "---- DEPOT ----" || item.Text == "---- OFFICE ----")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "Blue");
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

    public void LoadAdminBranch()
    {
        try
        {

            this.ddlBranch.Items.Clear();
            this.ddlBranch.DataSource = clsFinYear.BindBranch();
            this.ddlBranch.DataTextField = "BRNAME";
            this.ddlBranch.DataValueField = "BRID";
            this.ddlBranch.SelectedValue = Session["DEPOTID"].ToString();
            this.ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadBranchForOtherUserType(string UserID)
    {
        try
        {
            DataTable dtDepot = new DataTable();
            dtDepot = clsFinYear.BindDepotExceptAdmin(UserID);
            if (dtDepot.Rows.Count > 0)
            {
                this.ddlBranch.Items.Clear();
                this.ddlBranch.DataSource = dtDepot;
                if (dtDepot.Rows.Count == 1)
                {
                    Session["DEPOTID"] = dtDepot.Rows[0]["BRID"].ToString();
                    Session["DEPOTNAME"] = dtDepot.Rows[0]["BRNAME"].ToString();
                    this.ddlBranch.DataTextField = "BRNAME";
                    this.ddlBranch.DataValueField = "BRID";
                    this.ddlBranch.SelectedValue = Session["DEPOTID"].ToString();
                    this.ddlBranch.DataBind();
                }

                this.ddlBranch.DataTextField = "BRNAME";
                this.ddlBranch.DataValueField = "BRID";

                this.ddlBranch.DataBind();
            }
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
            this.ddlBranch.Items.Clear();
            this.ddlBranch.AppendDataBoundItems = true;
            this.ddlBranch.DataSource = clsFinYear.BindDepot(Session["UserID"].ToString(), Session["DEPOTID"].ToString());
            this.ddlBranch.DataTextField = "BRNAME";
            this.ddlBranch.DataValueField = "BRID";
            this.ddlBranch.SelectedValue = Session["DEPOTID"].ToString();
            this.ddlBranch.DataBind();
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
            this.ddlBranch.Items.Clear();
            if (Session["TPU"].ToString() == "F")
            {
                this.ddlBranch.Items.Add(new ListItem("-SELECT FACTORY-", "-1"));
            }
            else if (Session["TPU"].ToString() == "T")
            {
                this.ddlBranch.Items.Add(new ListItem("-SELECT TPU-", "-1"));
            }
            else
            {
                this.ddlBranch.Items.Add(new ListItem("-SELECT TPU/FACTORY-", "-1"));


            }
            this.ddlBranch.AppendDataBoundItems = true;
            this.ddlBranch.DataSource = clsFinYear.BindTPU(Session["UserID"].ToString(), Session["TPU"].ToString());
            this.ddlBranch.DataTextField = "VENDORNAME";
            this.ddlBranch.DataValueField = "VENDORID";
            this.ddlBranch.SelectedValue = Session["TPUID"].ToString();
            this.ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void loadDynamicMenu()
    {
        string USERTYPE = Session["USERTYPE"].ToString();
        DataTable objDt = new DataTable();
        ClsVendor_TPU objVendor_tpu = new ClsVendor_TPU();
        objDt = objVendor_tpu.BindPoFromTpu("DYNAMICMENU", USERTYPE);
        if (objDt.Rows.Count > 0)
        {
            this.grdDynamic.DataSource = objDt;
            this.grdDynamic.DataBind();
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["TPU"].ToString() == "T") // TPU ENTRY OFFICER
        {
            Session["TPUID"] = this.ddlBranch.SelectedValue;
        }
        else if (Session["TPU"].ToString() == "QC") // TPU - QC
        {
            Session["TPUID"] = this.ddlBranch.SelectedValue;
        }
        else if (Session["TPU"].ToString() == "FQA") // FACTORY - QC
        {
            Session["TPUID"] = this.ddlBranch.SelectedValue;
        }
        else if (Session["TPU"].ToString() == "D" || Session["TPU"].ToString() == "EXPU") // DEPOT/MOTHERDEPOT/EXPORT
        {
            Session["DEPOTID"] = this.ddlBranch.SelectedValue;
        }
        else if (Session["TPU"].ToString() == "F") // FACTORY
        {
            Session["TPUID"] = this.ddlBranch.SelectedValue;
        }
        else // OTHERS
        {
            Session["DEPOTID"] = this.ddlBranch.SelectedValue;
        }
    }

    #region purchase order notification for poc-1
    /*purchase order notification by p.basu*/
    public void loadPurchaseOrderForApprove()
    {
        try
        {
            string mode = "poc-1";
            DataTable dt = new DataTable();
            BAL.ClsMMPoOrder clsinvcount = new BAL.ClsMMPoOrder();
            dt = clsinvcount.FetchPurchaseOrder(mode, HttpContext.Current.Session["FINYEAR"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.grdPurchaseOrder.DataSource = dt;
                this.grdPurchaseOrder.DataBind();
            }
            else
            {
                MessageBox.Show("No Pending Purchase Order avilable");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void lblPONOVIEW_Click(object sender, EventArgs e)
    {
        try
        {

            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblPOID = (Label)gvr.FindControl("lblPOID");
            string poid = lblPOID.Text.Trim();
            string upath = string.Empty;

            if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
            {
                upath = "frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + poid + "&&TAG=PO&&MenuId=29";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            }
            else if (Session["USERTYPE"].ToString() == "6A2A199B-4654-4CD8-BF20-D1ACB52D7432")
            {
                upath = "frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + poid + "&&TAG=PO&&MenuId=29";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            }

            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lblApproved_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblPOID = (Label)gvr.FindControl("lblPOID");
            Label lblPONOVIEW = (Label)gvr.FindControl("lblPONO");
            string poid = lblPOID.Text.Trim();
            string pono = lblPONOVIEW.Text.Trim();

            string mode = string.Empty;
            if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
            {
                mode = "1";
            }
            else
            {
                mode = "3";
            }

            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(poid, HttpContext.Current.Session["UserID"].ToString(), mode, "");
            if (flag == 1)
            {
                MessageBox.Show("Purchase Order" + pono + "successfully approved.");
                if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
                {
                    loadPurchaseOrderForApprove();
                }
                else
                {
                    loadPurchaseOrderForConfirm();
                }


            }
            else if (flag == 0)
            {
                MessageBox.Show("<b>Error saving record..!</b>");
                if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
                {
                    loadPurchaseOrderForApprove();
                }
                else
                {
                    loadPurchaseOrderForConfirm();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void lblReject_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblPOID = (Label)gvr.FindControl("lblPOID");
            Label lblPONOVIEW = (Label)gvr.FindControl("lblPONO");
            string poid = lblPOID.Text.Trim();
            string pono = lblPONOVIEW.Text.Trim();

            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(poid, HttpContext.Current.Session["UserID"].ToString(), "2", "");
            if (flag == 1)
            {
                MessageBox.Show("Purchase Order" + pono + "reject approved.");
                if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
                {
                    loadPurchaseOrderForApprove();
                }
                else
                {
                    loadPurchaseOrderForConfirm();
                }
            }
            else if (flag == 0)
            {
                MessageBox.Show("<b>Error saving record..!");
                if (Session["USERTYPE"].ToString() == "00FB599D-009A-458F-9CC4-107F27947B80")
                {
                    loadPurchaseOrderForApprove();
                }
                else
                {
                    loadPurchaseOrderForConfirm();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region purchase order notification for poc-2
    public void loadPurchaseOrderForConfirm()
    {
        try
        {
            string mode = "poc-2";
            DataTable dt = new DataTable();
            BAL.ClsMMPoOrder clsinvcount = new BAL.ClsMMPoOrder();
            dt = clsinvcount.FetchPurchaseOrder(mode, HttpContext.Current.Session["FINYEAR"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.grdPurchaseOrder.DataSource = dt;
                this.grdPurchaseOrder.DataBind();
            }
            else
            {
                MessageBox.Show("No Pending Purchase Order avilable");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion


    protected void btnWorkflow_Click(object sender, EventArgs e)
    {
        //this.div_Workflow.Style["Display"] = "";
        //ClsClaim clsDisplayClaim = new ClsClaim();
        //DataTable dt = new DataTable();
        //if (Session["USERTYPE"].ToString() == "B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86" || Session["USERTYPE"].ToString() == "9BF42AA9-0734-4A6A-B835-0885FBCF26F5" || Session["USERTYPE"].ToString() == "A8274DED-8B5B-4A58-9E10-098F3BDF9F25" || Session["USERTYPE"].ToString() == "FE89F22F-CC62-4E7D-B68A-2FE460DC2470")
        //{
        //    this.grdUserlist.DataSource = null;
        //    this.grdUserlist.DataBind();
        //}
        //else
        //{
        //    dt = clsDisplayClaim.BindClaimTypeInGrid(Convert.ToInt32(Session["UserID"].ToString()), Session["FINYEAR"].ToString().Trim());
        //    if (dt.Rows.Count > 0)
        //    {
        //        this.grdUserlist.DataSource = dt;
        //        this.grdUserlist.DataBind();
        //    }
        //    else
        //    {
        //        this.grdUserlist.DataSource = null;
        //        this.grdUserlist.DataBind();
        //    }
        //}
        //#region Stock Received Workflow
        //// Added By Avishek On 15-03-2016
        //ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
        //this.divPurchaseStockReceipt.Style["Display"] = "";
        //DataTable dtPurchaseStockReceipt = new DataTable();
        //dtPurchaseStockReceipt = clsPurchaseStockReceipt.PendingPurchaseStockReceiptVoucher(Convert.ToString(Session["IUserID"]));
        //if (dtPurchaseStockReceipt.Rows.Count > 0)
        //{
        //    this.grdPendingStockReceived.DataSource = dtPurchaseStockReceipt;
        //    this.grdPendingStockReceived.DataBind();
        //}
        //else
        //{
        //    this.grdPendingStockReceived.DataSource = null;
        //    this.grdPendingStockReceived.DataBind();
        //}
        //#endregion
    }
    #region factory
    protected void btnWorkflowfac_Click(object sender, EventArgs e)
    {
        //if (Session["TPU"].ToString() == "FQA-2" || Session["TPU"].ToString() == "PRODUCTION MANAGER" || Session["TPU"].ToString() == "STORE INCHARGE" || Session["UTNAME"].ToString() == "QC Manager" || Session["UTNAME"].ToString() == "QC Manager Unit-1") // FACTORY QC
        //{
        //    ClsMMPPQualityControl Obj = new ClsMMPPQualityControl();
        //    TD1.Visible = true;
        //    TD2.Visible = false;
        //    // this.swichbarOther_fac.Style["display"] = "none";

        //    DataTable dtQC = new DataTable();
        //    dtQC = Obj.PendingQC(Convert.ToString(Session["DEPOTID"]), Convert.ToString(Session["FINYEAR"]), "C");
        //    if (dtQC.Rows.Count > 0)
        //    {
        //        GridView1.DataSource = dtQC;
        //        GridView1.DataBind();
        //    }
        //    else
        //    {
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();
        //    }

        //}
        //else
        //{
        //    MessageBox.Show("This menu is not available for you.");
        //}
    }
    protected void lblPRODUCTION_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "PRODUCTION MANAGER")
            {
                Response.Redirect("~/FACTORY/frmProductionOrder_WithoutMRP.aspx?mode=dashboard");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblIssuePending_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "PRODUCTION MANAGER" || Session["TPU"].ToString() == "STORE INCHARGE")
            {
                Response.Redirect("~/FACTORY/frmMMIssue.aspx?mode=dashboard");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblPENDINGQA_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "FQA-2" || Session["UTNAME"].ToString() == "QC Manager" || Session["UTNAME"].ToString() == "QC Manager Unit-1")
            {
                Response.Redirect("frmrptqapending.aspx");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void REQUISITIONPENDING_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "PRODUCTION MANAGER")
            {
                Response.Redirect("~/FACTORY/frmFactoryProductionUpdate.aspx");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnWorkflowfac_qc_Click(object sender, EventArgs e)
    {
        /*if (Session["UTNAME"].ToString() == "Factory Accounts" || Session["UTNAME"].ToString() == "Store Executive" || Session["UTNAME"].ToString() == "QC Chemist" || Session["UTNAME"].ToString() == "Sr. Account Manager" || Session["TPU"].ToString() == "STORE INCHARGE" || Session["UTNAME"].ToString() == "QC Manager" || Session["UTNAME"].ToString() == "QC Manager Unit-1") //factory qa
        {
            ClsMMPPQualityControl Obj = new ClsMMPPQualityControl();
            TD1.Visible = false;
            TD2.Visible = true;

            DataTable dtQC = new DataTable();
            dtQC = Obj.PendingQC(Convert.ToString(Session["DEPOTID"]), Convert.ToString(Session["FINYEAR"]), "A");
            if (dtQC.Rows.Count > 0)
            {
                GridView2.DataSource = dtQC;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }

        }
        else
        {
            MessageBox.Show("This menu is not available for you.");
        }*/
    }
    protected void lblTOTALGRNDONE_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "STORE INCHARGE" || Session["UTNAME"].ToString() == "Store Executive")
            {
                Response.Redirect("~/FACTORY/frmGRNMM.aspx?MENUID=1474&CHECKER=FALSE&FG=FALSE&OP=MAKER");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void lblTOTALQCPENDING_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UTNAME"].ToString() == "QC Chemist" || Session["UTNAME"].ToString() == "QC Manager" || Session["UTNAME"].ToString() == "QC Manager Unit-1")
            {
                Response.Redirect("~/FACTORY/frmMMPPQualityControl.aspx?CHECKER=dashboard");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblTOTALSTOCKINPENDING_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPU"].ToString() == "STORE INCHARGE" || Session["UTNAME"].ToString() == "Store Executive")
            {
                Response.Redirect("~/FACTORY/frmGRNMM.aspx?MENUID=1635&CHECKER=FALSE&FG=FALSE&OP=QC");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblCHECKER1APPROVEDPENDING_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UTNAME"].ToString() == "Factory Accounts")
            {
                Response.Redirect("~/FACTORY/frmGRNMM.aspx?MENUID=1636&CHECKER=FALSE&FG=FALSE&OP=Checker1");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lblFINALAPPROVEDPENDING_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UTNAME"].ToString() == "Sr. Account Manager")
            {
                Response.Redirect("~/FACTORY/frmStockReceived_FAC.aspx?MENUID=2809&CHECKER=TRUE");
            }
            else
            {
                MessageBox.Show("This Menu Is Not Available For You.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    public void LoadCustomerInvoiceStatus()
    {
        /*this.div_customer_details.Style["Display"] = "";
        BLL.ClsStockReport clsinvcount = new BLL.ClsStockReport();
        DataTable dt = new DataTable();
        dt = clsinvcount.BindCustomerInvoiceCount(Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
        if (dt.Rows.Count > 0)
        {
            this.gvCustomerInvoice.DataSource = dt;
            this.gvCustomerInvoice.DataBind();
        }*/
    }

    //#region LoadComplaintDeta Commented
    //public void LoadComplaintDetails()
    //{
    //    try
    //    {

    //        ClsCustomerMaster clsInvocie = new ClsCustomerMaster();
    //        DataTable dataTable = new DataTable();
    //        dataTable = clsInvocie.BindComplaintDetailsfordashbord(Session["UserID"].ToString());
    //        if (dataTable.Rows.Count > 0)
    //        {

    //            grvComplainTransfer.DataSource = dataTable;
    //            grvComplainTransfer.DataBind();
    //        }
    //        else
    //        {
    //            grvComplainTransfer.DataSource = null;
    //            grvComplainTransfer.DataBind();
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message.Replace("'", "");
    //    }
    //}
    //#endregion
    //protected void gvComplaintDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = e.Row.DataItem as DataRowView;

    //        if (drv["COMPLAINID"].ToString().ToUpper().Contains("-"))
    //        {
    //            e.Row.ForeColor = System.Drawing.Color.Black;
    //            e.Row.Font.Bold = true;
    //            LinkButton lbtn = new LinkButton();
    //            lbtn = (LinkButton)e.Row.FindControl("lnkWorkStationPending");
    //            lbtn.Enabled = true;
    //        }
    //    }
    //}

    //protected void gvComplaintDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "SelectTicket")
    //    {

    //        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
    //        string str = commandArgs[0];
    //        string strdate = commandArgs[1].ToString();
    //        string upath = string.Empty;

    //        upath = "frmComplainGenerate.aspx?complainid=" + str + "&&menuid=AddResolve";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1200,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

    //    }
    //}

    #region btnSearch_Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtFromDate.Text != "" && this.txtToDate.Text != "")
            {
                if (Session["TPU"].ToString().Trim() == "MIS C.O.")
                {
                    this.BindChart();
                }
            }


        }


        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }
    }
    protected void btnSo_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtstart.Text != "" && this.txtend.Text != "")
            {
                if (Session["TPU"].ToString().Trim() == "SO")
                {
                    this.BindChartSo();
                    this.BindChartSoprimary();
                }
            }


        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }

    }

    protected void btnAsm_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.start.Text != "" && this.end.Text != "")
            {
                if (Session["TPU"].ToString().Trim() == "ASM")
                {
                    BindChartAsm();
                    BindChartAsmPrimary();
                }
            }


        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }

    }
    #endregion
    #region btnForecastSearch_Click
    protected void btnForecastSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtAsOnDate.Text != "")
            {
                if (Session["TPU"].ToString().Trim() == "ADMIN" || Session["TPU"].ToString().Trim() == "SCM")
                {
                    this.BindForeCastChart();
                }
            }
        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :Page_Load()");
        }
    }
    #endregion

    #region Bind Chart
    private void BindChart()
    {
        //StringBuilder strScriptcategorysale = new StringBuilder();
        //StringBuilder strBrandsale = new StringBuilder();
        //StringBuilder strStateSale = new StringBuilder();
        //StringBuilder strOrderInvoice = new StringBuilder();
        //StringBuilder strDistributor = new StringBuilder();
        //StringBuilder strProduct = new StringBuilder();
        //StringBuilder strBrandSKU = new StringBuilder();
        //StringBuilder strMonthSaleTrend = new StringBuilder();
        //StringBuilder strWeekSaleTrend = new StringBuilder();
        //StringBuilder strTgtAch = new StringBuilder();
        //try
        //{
        //    ClsDashBoard clsreport = new ClsDashBoard();
        //    DashboardGraph obj1 = new DashboardGraph();
        //    if (ViewState["StartDate"].ToString() == "" && ViewState["EndDate"].ToString() == "")
        //    {
        //        DateTime now = DateTime.Now;
        //        DateTime startDate = new DateTime(now.Year, now.Month, 1);
        //        DateTime endDate = DateTime.Now;
        //        ViewState["StartDate"] = startDate.ToString(date).Replace('-', '/');
        //        ViewState["EndDate"] = endDate.ToString(date).Replace('-', '/');


        //        obj1 = clsreport.dashBoardChart(Session["DEPOTID"].ToString(),
        //                                                        ViewState["StartDate"].ToString().Trim(),
        //                                                        ViewState["EndDate"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                        "7F62F951-9D1F-4B8D-803B-74EEBA468CEE"
        //                                                      );
        //    }
        //    else if (ViewState["StartDate"].ToString() != "" && ViewState["EndDate"].ToString() != "")
        //    {
        //        obj1 = clsreport.dashBoardChart(Session["DEPOTID"].ToString(),
        //                                                        ViewState["StartDate"].ToString().Trim(),
        //                                                        ViewState["EndDate"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                        "7F62F951-9D1F-4B8D-803B-74EEBA468CEE"
        //                                                      );
        //    }


        //    List<CategoryWiseSale> objCategorySale = new List<CategoryWiseSale>();
        //    objCategorySale = obj1.CategoryMonthSale;
        //    List<BrandWiseSale> objBrandSale = new List<BrandWiseSale>();
        //    objBrandSale = obj1.BrandMonthSale;
        //    List<StateWiseSale> objStateSale = new List<StateWiseSale>();
        //    objStateSale = obj1.StateMonthSale;
        //    List<BrandSale> objBrandBarSale = new List<BrandSale>();
        //    objBrandBarSale = obj1.BrandBarSale;
        //    List<DistributorSale> objDistributorSale = new List<DistributorSale>();
        //    objDistributorSale = obj1.DistributorMonthSale;
        //    List<ProductSale> objProductSale = new List<ProductSale>();
        //    objProductSale = obj1.ProductMonthSale;
        //    List<TargetAch> objTargetAch = new List<TargetAch>();
        //    objTargetAch = obj1.TargetAchValue;
        //    List<MonthlySaleTrend> objMonthlySaleTrend = new List<MonthlySaleTrend>();
        //    objMonthlySaleTrend = obj1.MonthlySaleTrendValue;
        //    List<WeeklySaleTrend> objWeeklySaleTrend = new List<WeeklySaleTrend>();
        //    objWeeklySaleTrend = obj1.WeeklySaleTrendValue;


        //    #region Pie Chart

        //    #region Category Sale
        //    strScriptcategorysale.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objCategorySale = google.visualization.arrayToDataTable([  
        //        ['MonthSale','Item'],");

        //    foreach (var list in objCategorySale)
        //    {
        //        strScriptcategorysale.Append("['" + list.Item + "'," + list.MonthSale + "],");
        //    }


        //    if (objCategorySale[0].Item == "No Category Sale Founds" && objCategorySale[0].MonthSale == 0)
        //    {
        //        strScriptcategorysale.Append("['No data found', {v: 1, f: 'No data found'}]");
        //    }
        //    else
        //    {
        //        strScriptcategorysale.Remove(strScriptcategorysale.Length - 1, 1);
        //    }
        //    strScriptcategorysale.Append("]);");
        //    strScriptcategorysale.Append(@"var options = { title : 'Category wise Sale(GT) " + this.txtFromDate.Text.Trim() + " to " + this.txtToDate.Text.Trim() + "', vAxis: {title: 'Item'},  hAxis: {title: 'MonthSale'},'width':'100%', is3D:true,sliceVisibilityThreshold :0};");
        //    strScriptcategorysale.Append(@"var chart = new google.visualization.PieChart(document.getElementById('chart_divcategorywisesale'));  chart.draw(objCategorySale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strScriptcategorysale.Append(" </script>");

        //    ltCategorywisesale.Text = strScriptcategorysale.ToString();

        //    #endregion

        //    #region Brand Sale
        //    strBrandsale.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objBrandSale = google.visualization.arrayToDataTable([  
        //        ['MonthSale', 'Item'],");

        //    foreach (var list in objBrandSale)
        //    {
        //        strBrandsale.Append("['" + list.Item + "'," + list.MonthSale + "],");
        //    }
        //    if (objBrandSale[0].Item == "No Brand Sale Founds" && objBrandSale[0].MonthSale == 0)
        //    {
        //        strBrandsale.Append("['No data found', {v: 1, f: 'No data found'}]");
        //    }
        //    else
        //    {
        //        strBrandsale.Remove(strBrandsale.Length - 1, 1);
        //    }
        //    strBrandsale.Append("]);");
        //    strBrandsale.Append(@"var options = { title : 'Brand wise Sale(GT) " + this.txtFromDate.Text.Trim() + " to " + this.txtToDate.Text.Trim() + "', vAxis: {title: 'Item'},  hAxis: {title: 'MonthSale'},'width':'100%', is3D:true,sliceVisibilityThreshold :0};");
        //    strBrandsale.Append(@"var chart = new google.visualization.PieChart(document.getElementById('chart_divBrandsale'));  chart.draw(objBrandSale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strBrandsale.Append(" </script>");

        //    ltBrandwisesalegraph.Text = strBrandsale.ToString();

        //    #endregion

        //    #region State Sale
        //    strStateSale.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objStateSale = google.visualization.arrayToDataTable([  
        //        ['MonthSale', 'Item'],");

        //    foreach (var list in objStateSale)
        //    {
        //        strStateSale.Append("['" + list.Item + "'," + list.MonthSale + "],");
        //    }
        //    if (objStateSale[0].Item == "No State Sale Founds" && objStateSale[0].MonthSale == 0)
        //    {
        //        strStateSale.Append("['No data found', {v: 1, f: 'No data found'}]");
        //    }
        //    else
        //    {
        //        strStateSale.Remove(strStateSale.Length - 1, 1);
        //    }
        //    strStateSale.Append("]);");
        //    strStateSale.Append(@"var options = { title : 'State wise Sale(GT) " + this.txtFromDate.Text.Trim() + " to " + this.txtToDate.Text.Trim() + "', vAxis: {title: 'Item'},  hAxis: {title: 'MonthSale'},'width':'100%', is3D:true,sliceVisibilityThreshold :0};");
        //    strStateSale.Append(@"var chart = new google.visualization.PieChart(document.getElementById('chart_divStatesale'));  chart.draw(objStateSale, options); } google.setOnLoadCallback(drawVisualization);");

        //    strStateSale.Append(" </script>");

        //    ltStatewisesalegraph.Text = strStateSale.ToString();

        //    #endregion

        //    #endregion

        //    #region Bar Chart

        //    #region  Brand Sale Commented
        //    strOrderInvoice.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objBrandBarSale = google.visualization.arrayToDataTable([  
        //        ['Month','WildStone','Secrete','Heavens'],");

        //    foreach (var list in objBrandBarSale)
        //    {
        //        strOrderInvoice.Append("['" + list.Month + "'," + list.WildStone + "," + list.Secret + "," + list.Heavens + "],");
        //    }
        //    strOrderInvoice.Remove(strOrderInvoice.Length - 1, 1);
        //    strOrderInvoice.Append("]);");

        //    strOrderInvoice.Append(@"var options = { title : 'Brand value Last 3 Months (GT)', vAxis: {title: 'Order vs Invoice'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'bars', series: {3: {type: 'area'}}};");
        //    strOrderInvoice.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divOrderInvoice'));  chart.draw(objBrandBarSale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strOrderInvoice.Append(" </script>");

        //    ltOrderInvoice.Text = strOrderInvoice.ToString();

        //    #endregion

        //    #region Top 10 Distributor
        //    strDistributor.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objDistributorSale = google.visualization.arrayToDataTable([  
        //        ['Item', 'MonthSale'],");

        //    foreach (var list in objDistributorSale)
        //    {
        //        strDistributor.Append("['" + list.Item + "'," + list.MonthSale + "],");
        //    }
        //    strDistributor.Remove(strDistributor.Length - 1, 1);
        //    strDistributor.Append("]);");
        //    strDistributor.Append(@"var options = { title : 'Top 10 Distributor(GT) " + this.txtFromDate.Text.Trim() + " to " + this.txtToDate.Text.Trim() + "', vAxis: {title: 'Sale'},  hAxis: {title: 'Distributor'}, 'width':'100%', seriesType: 'bars', series: {1: {type: 'area'}},colors: ['green'] };");
        //    strDistributor.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divDistributor'));  chart.draw(objDistributorSale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strDistributor.Append(" </script>");
        //    ltDistributor.Text = strDistributor.ToString();
        //    #endregion

        //    #region Top 10 Product
        //    strProduct.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objProductSale = google.visualization.arrayToDataTable([  
        //        ['Item','Value'],");

        //    foreach (var list in objProductSale)
        //    {
        //        strProduct.Append("['" + list.Item + "'," + list.Achievement + "],");
        //    }
        //    strProduct.Remove(strProduct.Length - 1, 1);
        //    strProduct.Append("]);");
        //    strProduct.Append(@"var options = { title : 'Top 10 Product(GT) " + this.txtFromDate.Text.Trim() + " to " + this.txtToDate.Text.Trim() + "', vAxis: {title: 'Value'},  hAxis: {title: 'Product'}, 'width':'100%', seriesType: 'bars', series: {1: {type: 'area'}},colors: ['violet'] };");
        //    strProduct.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divproduct'));  chart.draw(objProductSale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strProduct.Append(" </script>");
        //    ltproduct.Text = strProduct.ToString();
        //    #endregion

        //    #region  Target vs Achievement
        //    strTgtAch.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objTargetAch = google.visualization.arrayToDataTable([  
        //        ['Month','Target','Achievement'],");

        //    foreach (var list in objTargetAch)
        //    {
        //        strTgtAch.Append("['" + list.Month + "'," + list.Target + "," + list.Achievement + "],");
        //    }
        //    strTgtAch.Remove(strTgtAch.Length - 1, 1);
        //    strTgtAch.Append("]);");

        //    strTgtAch.Append(@"var options = { title : 'Target vs Achievement Last 3 Months (GT)', vAxis: {title: 'Order vs Invoice'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'bars', series: {2: {type: 'area'}}};");
        //    strTgtAch.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divTgtAch'));  chart.draw(objTargetAch, options); } google.setOnLoadCallback(drawVisualization);");
        //    strTgtAch.Append(" </script>");

        //    ltTgtAch.Text = strTgtAch.ToString();

        //    #endregion

        //    #endregion

        //    #region Line Chart 

        //    #region Monthly Trend
        //    strMonthSaleTrend.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objMonthlySaleTrend = google.visualization.arrayToDataTable([  
        //        ['Month','PrimarySale','SecondarySale'],");

        //    foreach (var list in objMonthlySaleTrend)
        //    {
        //        strMonthSaleTrend.Append("['" + list.Month + "'," + list.PrimarySale + "," + list.SecondarySale + "],");
        //    }
        //    strMonthSaleTrend.Remove(strMonthSaleTrend.Length - 1, 1);
        //    strMonthSaleTrend.Append("]);");
        //    strMonthSaleTrend.Append(@"var options = { title : 'Primary & Secondary Sales Trend(GT)', vAxis: {title: 'Sale'},  hAxis: {title: 'Month'}, 'backgroundColor': 'white', 'width':'100%', seriesType: 'lines', series: {0: {type: 'area'}} };");
        //    strMonthSaleTrend.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divMonthTrendsale'));  chart.draw(objMonthlySaleTrend, options); } google.setOnLoadCallback(drawVisualization);");
        //    strMonthSaleTrend.Append("</script>");

        //    ltMonthTrend.Text = strMonthSaleTrend.ToString();
        //    #endregion

        //    #region Weekly Trend
        //    strWeekSaleTrend.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objWeeklySaleTrend = google.visualization.arrayToDataTable([  
        //        ['Month','PrimarySale','SecondarySale'],");

        //    foreach (var list in objWeeklySaleTrend)
        //    {
        //        strWeekSaleTrend.Append("['" + list.Month + "'," + list.PrimarySale + "," + list.SecondarySale + "],");
        //    }
        //    strWeekSaleTrend.Remove(strWeekSaleTrend.Length - 1, 1);
        //    strWeekSaleTrend.Append("]);");
        //    strWeekSaleTrend.Append(@"var options = { title : 'Weekly Primary & Secondary Sales(GT) MTD', vAxis: {title: 'Sale'},  hAxis: {title: 'Date Range'}, 'backgroundColor': 'white', 'width':'100%', seriesType: 'lines', series: {0: {type: 'area'}} };");
        //    strWeekSaleTrend.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divWeeklyTrendsale'));  chart.draw(objWeeklySaleTrend, options); } google.setOnLoadCallback(drawVisualization);");
        //    strWeekSaleTrend.Append("</script>");

        //    ltWeeklyTrend.Text = strWeekSaleTrend.ToString().Trim();
        //    #endregion

        //    #endregion

        //}
        //catch (Exception ex)
        //{
        //    string msg = ex.Message.Replace("'", "");
        //}
        //finally
        //{
        //    strScriptcategorysale.Clear();
        //    strBrandsale.Clear();
        //    strStateSale.Clear();
        //    strOrderInvoice.Clear();
        //    strDistributor.Clear();
        //    strProduct.Clear();
        //    strBrandSKU.Clear();
        //    strMonthSaleTrend.Clear();
        //    strWeekSaleTrend.Clear();
        //    strTgtAch.Clear();
        //}
    }
    #endregion

    #region BindFilterOption
    private void BindFilterOption()
    {
        //ClsDashBoard clsreport = new ClsDashBoard();
        //DashboardGraphFilter obj1 = new DashboardGraphFilter();
        //obj1 = clsreport.forecastStockChartFilter();

        //#region Populating Segment
        ///*Segment*/
        //if (obj1.GraphBSItem.Count > 0)
        //{
        //    this.ddlSegment.Items.Clear();
        //    this.ddlSegment.Items.Add(new ListItem("Select Segment", "0"));
        //    this.ddlSegment.AppendDataBoundItems = true;
        //    this.ddlSegment.DataSource = obj1.GraphBSItem;
        //    this.ddlSegment.DataValueField = "BSID";
        //    this.ddlSegment.DataTextField = "BSNAME";
        //    this.ddlSegment.DataBind();
        //}
        //else
        //{
        //    this.ddlSegment.Items.Clear();
        //    this.ddlSegment.Items.Add(new ListItem("Select Segment", "0"));
        //    this.ddlSegment.AppendDataBoundItems = true;
        //}
        //#endregion

        //#region Populating Brand
        ///*Brand*/
        //if (obj1.GraphBrandItem.Count > 0)
        //{
        //    this.ddlBrand.Items.Clear();
        //    this.ddlBrand.Items.Add(new ListItem("Select Brand", "0"));
        //    this.ddlBrand.AppendDataBoundItems = true;
        //    this.ddlBrand.DataSource = obj1.GraphBrandItem;
        //    this.ddlBrand.DataValueField = "DIVID";
        //    this.ddlBrand.DataTextField = "DIVNAME";
        //    this.ddlBrand.DataBind();
        //}
        //else
        //{
        //    this.ddlBrand.Items.Clear();
        //    this.ddlBrand.Items.Add(new ListItem("Select Brand", "0"));
        //    this.ddlBrand.AppendDataBoundItems = true;
        //}
        //#endregion

        //#region Populating Category
        ///*Category*/
        //if (obj1.GraphCategoryItem.Count > 0)
        //{
        //    this.ddlCategory.Items.Clear();
        //    this.ddlCategory.Items.Add(new ListItem("Select Category", "0"));
        //    this.ddlCategory.AppendDataBoundItems = true;
        //    this.ddlCategory.DataSource = obj1.GraphCategoryItem;
        //    this.ddlCategory.DataValueField = "CATID";
        //    this.ddlCategory.DataTextField = "CATNAME";
        //    this.ddlCategory.DataBind();
        //}
        //else
        //{
        //    this.ddlCategory.Items.Clear();
        //    this.ddlCategory.Items.Add(new ListItem("Select Category", "0"));
        //    this.ddlCategory.AppendDataBoundItems = true;
        //}
        //#endregion

        //#region Populating Product
        ///*Product*/
        //if (obj1.GraphProductItem.Count > 0)
        //{
        //    this.ddlProduct.Items.Clear();
        //    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
        //    this.ddlProduct.AppendDataBoundItems = true;
        //    this.ddlProduct.DataSource = obj1.GraphProductItem;
        //    this.ddlProduct.DataValueField = "PRODUCTID";
        //    this.ddlProduct.DataTextField = "PRODUCTNAME";
        //    this.ddlProduct.DataBind();
        //}
        //else
        //{
        //    this.ddlProduct.Items.Clear();
        //    this.ddlProduct.Items.Add(new ListItem("Select Product", "0"));
        //    this.ddlProduct.AppendDataBoundItems = true;
        //}
        //#endregion
    }
    #endregion      

    #region Bind Forecast Chart
    private void BindForeCastChart()
    {
        //StringBuilder strScriptForecast = new StringBuilder();

        //try
        //{
        //    ClsDashBoard clsreport = new ClsDashBoard();
        //    ForecastStockGraph obj1 = new ForecastStockGraph();
        //    if (ViewState["AsOnDate"].ToString() == "")
        //    {
        //        DateTime now = DateTime.Now.AddDays(-1);
        //        DateTime AsOnDateDate = new DateTime(now.Year, now.Month, now.Day);
        //        ViewState["AsOnDate"] = AsOnDateDate.ToString(date).Replace('-', '/');


        //        obj1 = clsreport.forecastStockChart(HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                ViewState["AsOnDate"].ToString().Trim(),
        //                                                this.ddlBrand.SelectedValue.Trim(),
        //                                                this.ddlCategory.SelectedValue.Trim(),
        //                                                this.ddlSegment.SelectedValue.Trim(),
        //                                                this.ddlProduct.SelectedValue.Trim(),
        //                                                "0"
        //                                            );
        //    }
        //    else if (ViewState["AsOnDate"].ToString() != "")
        //    {
        //        obj1 = clsreport.forecastStockChart(HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                ViewState["AsOnDate"].ToString().Trim(),
        //                                                this.ddlBrand.SelectedValue.Trim(),
        //                                                this.ddlCategory.SelectedValue.Trim(),
        //                                                this.ddlSegment.SelectedValue.Trim(),
        //                                                this.ddlProduct.SelectedValue.Trim(),
        //                                                "0"
        //                                            );
        //    }


        //    List<ForeCastStock> objForeCastStock = new List<ForeCastStock>();
        //    objForeCastStock = obj1.ForeCastStockStatus;



        //    #region Bar Chart

        //    #region  Forecast & Stock 
        //    strScriptForecast.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var objForeCastStock = google.visualization.arrayToDataTable([  
        //        ['DEPOT','FORECAST_QTY','STOCK_QTY','TRANSIT_QTY'],");

        //    foreach (var list in objForeCastStock)
        //    {
        //        strScriptForecast.Append("['" + list.DEPOT + "'," + list.FORECAST_QTY + "," + list.STOCK_QTY + "," + list.TRANSIT_QTY + "],");
        //    }

        //    string Segment = string.Empty;
        //    string Brand = string.Empty;
        //    string Category = string.Empty;
        //    string Product = string.Empty;
        //    if (this.ddlSegment.SelectedValue != "0")
        //    {
        //        Segment = this.ddlSegment.SelectedItem.ToString().Trim();
        //    }
        //    else
        //    {
        //        Segment = "All";
        //    }
        //    if (this.ddlBrand.SelectedValue != "0")
        //    {
        //        Brand = this.ddlBrand.SelectedItem.ToString().Trim();
        //    }
        //    else
        //    {
        //        Brand = "All";
        //    }
        //    if (this.ddlCategory.SelectedValue != "0")
        //    {
        //        Category = this.ddlCategory.SelectedItem.ToString().Trim();
        //    }
        //    else
        //    {
        //        Category = "All";
        //    }
        //    if (this.ddlProduct.SelectedValue != "0")
        //    {
        //        Product = this.ddlProduct.SelectedItem.ToString().Trim();
        //    }
        //    else
        //    {
        //        Product = "All";
        //    }
        //    strScriptForecast.Remove(strScriptForecast.Length - 1, 1);
        //    strScriptForecast.Append("]);");
        //    strScriptForecast.Append(@"var options = { title : 'Forecast vs Stock & Transit. Filter Parameter Segment - " + Segment + ",Brand - " + Brand + ",Category - " + Category + ",Product - " + Product + "', vAxis: {title: 'Forecast vs Stock'},  hAxis: {title: 'Depot'}, 'width':'100%', seriesType: 'bars', series: {3: {type: 'area'}}};");
        //    strScriptForecast.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divForecast'));  chart.draw(objForeCastStock, options); } google.setOnLoadCallback(drawVisualization);");
        //    strScriptForecast.Append(" </script>");

        //    ltForecast.Text = strScriptForecast.ToString();

        //    #endregion


        //    #endregion



        //}
        //catch (Exception ex)
        //{
        //    string msg = ex.Message.Replace("'", "");
        //}
        //finally
        //{
        //    strScriptForecast.Clear();
        //}
    }
    #endregion

    #region bind chart so for tsi,distributor,category

    private void BindChartSo()
    {
        //DataSet ds = new DataSet();
        //DataTable dtmonthsale = new DataTable();
        //DataTable dt12monthproduct = new DataTable();
        ////DataTable dtcurrentproduct = new DataTable();
        ////DataTable dtretailersale = new DataTable();
        ////DataTable dttsisale = new DataTable();
        //DataTable dtcategorysale = new DataTable();
        //StringBuilder strScriptmonthsale = new StringBuilder();
        //StringBuilder str12monthproductsale = new StringBuilder();
        ////StringBuilder strScriptcurrentproductsale = new StringBuilder();
        ////StringBuilder strRetailer = new StringBuilder();
        ////StringBuilder strtsisale = new StringBuilder();
        //StringBuilder strcategorysale = new StringBuilder();
        //try
        //{
        //    ClsReports clsreport = new ClsReports();
        //    if (ViewState["SoStartDate"].ToString() == "" && ViewState["SoEndDate"].ToString() == "")
        //    {
        //        DateTime now = DateTime.Now;
        //        DateTime startDate = new DateTime(now.Year, now.Month, 1);
        //        DateTime endDate = DateTime.Now;
        //        ViewState["SoStartDate"] = startDate.ToString(date).Replace('-', '/');
        //        ViewState["SoEndDate"] = endDate.ToString(date).Replace('-', '/');

        //        ds = clsreport.Bindchart(this.Session["UserID"].ToString());

        //    }
        //    else if (ViewState["SoStartDate"].ToString() != "" && ViewState["SoEndDate"].ToString() != "")
        //    {
        //        ds = clsreport.Bindchart(this.Session["UserID"].ToString());
        //    }


        //    dtmonthsale = ds.Tables[1];
        //    dt12monthproduct = ds.Tables[6];
        //    //dtcurrentproduct = ds.Tables[2];
        //    //dtretailersale = ds.Tables[3];
        //    //dttsisale = ds.Tables[4];
        //    dtcategorysale = ds.Tables[5];

        //    str12monthproductsale.Append(@"<script type='text/javascript'>  
        //            google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    <script type='text/javascript'>  
        //            function drawVisualization() {         
        //            var dt12monthproduct = google.visualization.arrayToDataTable([  
        //            ['category', 'TARGET_VALUE','ACH_VALUE'],");

        //    foreach (DataRow row in dt12monthproduct.Rows)
        //    {
        //        str12monthproductsale.Append("['" + row["category"] + "'," + row["TARGET_VALUE"] + "," + row["ACH_VALUE"] + "],");
        //    }

        //    str12monthproductsale.Remove(str12monthproductsale.Length - 1, 1);
        //    str12monthproductsale.Append("]);");

        //    str12monthproductsale.Append(@"var options = { title : 'MTD CAT.TGT VS ACMT', vAxis: {title: 'ACHIVMENT'},  hAxis: {title: 'CATEGORY'}, 'width':'100%', seriesType: 'bars', };");
        //    str12monthproductsale.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_div12monthproduct'));  chart.draw(dt12monthproduct, options); } google.setOnLoadCallback(drawVisualization);");
        //    str12monthproductsale.Append(" </script>");

        //    lt12monthproduct.Text = str12monthproductsale.ToString();

        //    /*DISTRIBUTOR WISE SALE*/
        //    //strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //    //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    //        <script type='text/javascript'>  
        //    //        function drawVisualization() {         
        //    //        var dtmonthsale = google.visualization.arrayToDataTable([  
        //    //        ['Month','Bindal Agency','Garg Enterprises (Sohna)','Jaina Genaral Store','Jindal Distributor','Khwaish Enterprises New','Madhur Marketing Co.','Rahul Traders(Haryana)','Singla Agencies'],");

        //    //foreach (DataRow row in dtmonthsale.Rows)
        //    //{
        //    //    strScriptmonthsale.Append("['" + row["Month"] + "'," + row["Bindal Agency"] + "," + row["Garg Enterprises (Sohna)"] + "," + row["Jaina Genaral Store"] + "," + row["Jindal Distributor"] + "," + row["Khwaish Enterprises New"] + "," + row["Madhur Marketing Co."] + "," + row["Rahul Traders(Haryana)"] + "," + row["Singla Agencies"] + "],");

        //    //}

        //    strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dtmonthsale = google.visualization.arrayToDataTable([[");

        //    foreach (DataColumn column in dtmonthsale.Columns)
        //    {
        //        strScriptmonthsale.Append("'");
        //        strScriptmonthsale.Append(column.ColumnName);
        //        strScriptmonthsale.Append("',");
        //    }

        //    strScriptmonthsale.Remove(strScriptmonthsale.Length - 1, 1);

        //    strScriptmonthsale.Append("],");

        //    foreach (DataRow row in dtmonthsale.Rows)
        //    {
        //        strScriptmonthsale.Append("['" + row[dtmonthsale.Columns[0].ColumnName] + "',");

        //        foreach (DataColumn column in dtmonthsale.Columns)
        //        {
        //            if (column.ColumnName != "Month")
        //            {
        //                strScriptmonthsale.Append("" + row[column] + ",");
        //            }
        //        }
        //        strScriptmonthsale.Remove(strScriptmonthsale.Length - 1, 1);
        //        strScriptmonthsale.Append("],");
        //    }

        //    strScriptmonthsale.Remove(strScriptmonthsale.Length - 1, 1);
        //    strScriptmonthsale.Append("]);");

        //    strScriptmonthsale.Append(@"var options = { title : 'Distributors Sale Last Two Month', vAxis: {title: 'Sale'},  hAxis: {title: 'Last Two Month Sale'}, 'width':'100%', seriesType: 'bars', series: {4: {type: 'Area'}} };");
        //    strScriptmonthsale.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divmonthsale'));  chart.draw(dtmonthsale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strScriptmonthsale.Append(" </script>");

        //    ltmonthsale.Text = strScriptmonthsale.ToString();

        //    /*DISTRIBUTOR WISE SALE*/

        //    strcategorysale.Append(@"<script type='text/javascript'>    
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dtcategorysale = google.visualization.arrayToDataTable([  
        //        ['MonthSale', 'Item'],");

        //    foreach (DataRow row in dtcategorysale.Rows)
        //    {
        //        strcategorysale.Append("['" + row["Item"] + "'," + row["MonthSale"] + "],");
        //    }

        //    strcategorysale.Remove(strcategorysale.Length - 1, 1);

        //    strcategorysale.Append("]);");

        //    strcategorysale.Append(@"var options = { title : 'MTD TSI SALE CONTRIBUTION', vAxis: {title: 'MonthSale'},  hAxis: {title: 'Item'},'width':'100%', is3D:true ,sliceVisibilityThreshold :0};");

        //    strcategorysale.Append(@"var chart = new google.visualization.PieChart(document.getElementById('chart_divcategorysale'));  chart.draw(dtcategorysale, options); } google.setOnLoadCallback(drawVisualization);");
        //    strcategorysale.Append(" </script>");

        //    ltcategorysalegraph.Text = strcategorysale.ToString();
        //}
        //catch
        //{

        //}
        //finally
        //{
        //    ds.Dispose();
        //    dtmonthsale.Dispose();
        //    dt12monthproduct.Dispose();
        //    //dtcurrentproduct.Dispose();
        //    //dtretailersale.Dispose();
        //    //dttsisale.Dispose();
        //    dtcategorysale.Dispose();

        //    strScriptmonthsale.Clear();
        //    str12monthproductsale.Clear();
        //    //strScriptcurrentproductsale.Clear();
        //    //strRetailer.Clear();
        //    //strtsisale.Clear();
        //    strcategorysale.Clear();
        //}
    }
    #endregion

    #region bindchartsoprimary for so primary and secondary sale for month and weekly,target vs achv 
    private void BindChartSoprimary()
    {
        //DataSet ds = new DataSet();
        //DataTable dtprimarysaleformonth = new DataTable();
        //DataTable dtprimarysaleforweek = new DataTable();
        //DataTable dttargatemonth = new DataTable();
        ////DataTable dtcurrentproduct = new DataTable();
        ////DataTable dtretailersale = new DataTable();
        ////DataTable dttsisale = new DataTable();
        //StringBuilder strprimarysaleformonth = new StringBuilder();
        //StringBuilder strrimarysaleforweek = new StringBuilder();
        //StringBuilder strtargatemonth = new StringBuilder();
        ////StringBuilder strScriptcurrentproductsale = new StringBuilder();
        ////StringBuilder strRetailer = new StringBuilder();
        ////StringBuilder strtsisale = new StringBuilder();
        //try
        //{
        //    ClsReports clsreport = new ClsReports();
        //    if (ViewState["SoStartDate"].ToString() != "" && ViewState["SoEndDate"].ToString() != "")
        //    {
        //        DateTime now = DateTime.Now;
        //        DateTime startDate = new DateTime(now.Year, now.Month, 1);
        //        DateTime endDate = DateTime.Now;
        //        ViewState["SoStartDate"] = startDate.ToString(date).Replace('-', '/');
        //        ViewState["SoEndDate"] = endDate.ToString(date).Replace('-', '/');

        //        ds = clsreport.BindchartPrimarySecondary(ViewState["SoStartDate"].ToString().Trim(), ViewState["SoEndDate"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                        "7F62F951-9D1F-4B8D-803B-74EEBA468CEE");
        //    }
        //    else if (ViewState["SoStartDate"].ToString() == "" && ViewState["SoEndDate"].ToString() == "")
        //    {

        //        ds = clsreport.BindchartPrimarySecondary(ViewState["SoStartDate"].ToString().Trim(), ViewState["SoEndDate"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                        "7F62F951-9D1F-4B8D-803B-74EEBA468CEE");
        //    }


        //    dtprimarysaleformonth = ds.Tables[0];
        //    dtprimarysaleforweek = ds.Tables[1];
        //    dttargatemonth = ds.Tables[2];
        //    //dtcurrentproduct = ds.Tables[2];
        //    //dtretailersale = ds.Tables[3];
        //    //dttsisale = ds.Tables[4];

        //    strprimarysaleformonth.Append(@"<script type='text/javascript'>  
        //            google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    <script type='text/javascript'>  
        //            function drawVisualization() {         
        //            var dtprimarysaleformonth = google.visualization.arrayToDataTable([  
        //            [ 'Month','PrimarySale','SecondarySale'],");

        //    foreach (DataRow row in dtprimarysaleformonth.Rows)
        //    {
        //        strprimarysaleformonth.Append("['" + row["Month"] + "'," + row["PrimarySale"] + "," + row["SecondarySale"] + "],");
        //    }
        //    strprimarysaleformonth.Remove(strprimarysaleformonth.Length - 1, 1);
        //    strprimarysaleformonth.Append("]);");

        //    strprimarysaleformonth.Append(@"var options = { title : 'YTD. Primary And Secondary Sale', vAxis: {title: 'SecondarySale'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'line', series: {0: {type: 'area'}}};");
        //    strprimarysaleformonth.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divprimarysalemonth'));  chart.draw(dtprimarysaleformonth, options); } google.setOnLoadCallback(drawVisualization);");
        //    strprimarysaleformonth.Append(" </script>");

        //    LiteralPrimarymonthly.Text = strprimarysaleformonth.ToString();

        //    /*DISTRIBUTOR WISE SALE*/
        //    //strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //    //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    //        <script type='text/javascript'>  
        //    //        function drawVisualization() {         
        //    //        var dtmonthsale = google.visualization.arrayToDataTable([  
        //    //        ['Month','Bindal Agency','Garg Enterprises (Sohna)','Jaina Genaral Store','Jindal Distributor','Khwaish Enterprises New','Madhur Marketing Co.','Rahul Traders(Haryana)','Singla Agencies'],");

        //    //foreach (DataRow row in dtmonthsale.Rows)
        //    //{
        //    //    strScriptmonthsale.Append("['" + row["Month"] + "'," + row["Bindal Agency"] + "," + row["Garg Enterprises (Sohna)"] + "," + row["Jaina Genaral Store"] + "," + row["Jindal Distributor"] + "," + row["Khwaish Enterprises New"] + "," + row["Madhur Marketing Co."] + "," + row["Rahul Traders(Haryana)"] + "," + row["Singla Agencies"] + "],");

        //    //}

        //    strrimarysaleforweek.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dtprimarysaleforweek = google.visualization.arrayToDataTable([  
        //            [ 'Month','PrimarySale','SecondarySale'],");

        //    foreach (DataRow row in dtprimarysaleforweek.Rows)
        //    {
        //        strrimarysaleforweek.Append("['" + row["Month"] + "'," + row["PrimarySale"] + "," + row["SecondarySale"] + "],");
        //    }
        //    strrimarysaleforweek.Remove(strrimarysaleforweek.Length - 1, 1);
        //    strrimarysaleforweek.Append("]);");

        //    strrimarysaleforweek.Append(@"var options = { title : 'MTD Primary and Secondary sale Weekly', vAxis: {title: 'SecondarySale'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'line', series: {0: {type: 'area'}}};");
        //    strrimarysaleforweek.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divprimarysaleweek'));  chart.draw(dtprimarysaleforweek, options); } google.setOnLoadCallback(drawVisualization);");
        //    strrimarysaleforweek.Append(" </script>");
        //    LiteralPrimaryweekly.Text = strrimarysaleforweek.ToString();

        //    strtargatemonth.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dttargatemonth = google.visualization.arrayToDataTable([  
        //            [ 'Month','Target','Achievement'],");

        //    foreach (DataRow row in dttargatemonth.Rows)
        //    {
        //        strtargatemonth.Append("['" + row["Month"] + "'," + row["Target"] + "," + row["Achievement"] + "],");
        //    }
        //    strtargatemonth.Remove(strtargatemonth.Length - 1, 1);
        //    strtargatemonth.Append("]);");

        //    strtargatemonth.Append(@"var options = { title : 'MTD Target vs Achievement', vAxis: {title: 'Target vs Achievement'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'bars',};");
        //    strtargatemonth.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divtargatechv'));  chart.draw(dttargatemonth, options); } google.setOnLoadCallback(drawVisualization);");
        //    strtargatemonth.Append(" </script>");
        //    lttarget.Text = strtargatemonth.ToString();
        //}
        //catch
        //{

        //}
        //finally
        //{
        //    ds.Dispose();
        //    dtprimarysaleformonth.Dispose();
        //    dtprimarysaleforweek.Dispose();
        //    dttargatemonth.Dispose();
        //    //dtcurrentproduct.Dispose();
        //    //dtretailersale.Dispose();
        //    //dttsisale.Dispose();
        //    strprimarysaleformonth.Clear();
        //    strrimarysaleforweek.Clear();
        //    strtargatemonth.Clear();
        //    //strScriptcurrentproductsale.Clear();
        //    //strRetailer.Clear();
        //    //strtsisale.Clear();
        //}
    }
    #endregion

    #region bind chart asm for so,distributor,category
    private void BindChartAsm()
    {
        //DataSet ds = new DataSet();
        //DataTable dtmonthsaleasm = new DataTable();
        //DataTable dt12monthproductasm = new DataTable();
        ////DataTable dtcurrentproduct = new DataTable();
        ////DataTable dtretailersale = new DataTable();
        ////DataTable dttsisale = new DataTable();
        //DataTable dtcategorysaleasm = new DataTable();
        //StringBuilder strScriptmonthsale = new StringBuilder();
        //StringBuilder str12monthproductsale = new StringBuilder();
        ////StringBuilder strScriptcurrentproductsale = new StringBuilder();
        ////StringBuilder strRetailer = new StringBuilder();
        ////StringBuilder strtsisale = new StringBuilder();
        //StringBuilder strcategorysale = new StringBuilder();
        //try
        //{
        //    ClsReports clsreport = new ClsReports();
        //    if (ViewState["AsmStartDate"].ToString() != "" && ViewState["AsmEndDate"].ToString() != "")
        //    {
        //        DateTime now = DateTime.Now;
        //        DateTime startDate = new DateTime(now.Year, now.Month, 1);
        //        DateTime endDate = DateTime.Now;
        //        ViewState["AsmStartDate"] = startDate.ToString(date).Replace('-', '/');
        //        ViewState["AsmEndDate"] = endDate.ToString(date).Replace('-', '/');

        //        ds = clsreport.BindchartAsm(this.Session["UserID"].ToString());

        //    }
        //    else if (ViewState["AsmStartDate"].ToString() == "" && ViewState["AsmEndDate"].ToString() == "")
        //    {
        //        ds = clsreport.BindchartAsm(this.Session["UserID"].ToString());
        //    }


        //    dtmonthsaleasm = ds.Tables[0];
        //    dt12monthproductasm = ds.Tables[1];
        //    //dtcurrentproduct = ds.Tables[2];
        //    //dtretailersale = ds.Tables[3];
        //    //dttsisale = ds.Tables[4];
        //    dtcategorysaleasm = ds.Tables[2];

        //    strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //            google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    <script type='text/javascript'>  
        //            function drawVisualization() {         
        //            var dtcategorysaleasm = google.visualization.arrayToDataTable([  
        //            ['category', 'TARGET_VALUE','ACH_VALUE'],");

        //    foreach (DataRow row in dtcategorysaleasm.Rows)
        //    {
        //        strScriptmonthsale.Append("['" + row["category"] + "'," + row["TARGET_VALUE"] + "," + row["ACH_VALUE"] + "],");
        //    }

        //    strScriptmonthsale.Remove(strScriptmonthsale.Length - 1, 1);
        //    strScriptmonthsale.Append("]);");

        //    strScriptmonthsale.Append(@"var options = { title : 'MTD CAT.TGT VS ACMT', vAxis: {title: 'ACHIVMENT'},  hAxis: {title: 'CATEGORY'}, 'width':'100%', seriesType: 'bars', };");
        //    strScriptmonthsale.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divcategorysaleasm'));  chart.draw(dtcategorysaleasm, options); } google.setOnLoadCallback(drawVisualization);");
        //    strScriptmonthsale.Append(" </script>");

        //    Literalcategorysaleasm.Text = strScriptmonthsale.ToString();

        //    /*DISTRIBUTOR WISE SALE*/
        //    //strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //    //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //    //        <script type='text/javascript'>  
        //    //        function drawVisualization() {         
        //    //        var dtmonthsale = google.visualization.arrayToDataTable([  
        //    //        ['Month','Bindal Agency','Garg Enterprises (Sohna)','Jaina Genaral Store','Jindal Distributor','Khwaish Enterprises New','Madhur Marketing Co.','Rahul Traders(Haryana)','Singla Agencies'],");

        //    //foreach (DataRow row in dtmonthsale.Rows)
        //    //{
        //    //    strScriptmonthsale.Append("['" + row["Month"] + "'," + row["Bindal Agency"] + "," + row["Garg Enterprises (Sohna)"] + "," + row["Jaina Genaral Store"] + "," + row["Jindal Distributor"] + "," + row["Khwaish Enterprises New"] + "," + row["Madhur Marketing Co."] + "," + row["Rahul Traders(Haryana)"] + "," + row["Singla Agencies"] + "],");

        //    //}

        //    str12monthproductsale.Append(@"<script type='text/javascript'>  
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dt12monthproductasm = google.visualization.arrayToDataTable([[");

        //    foreach (DataColumn column in dt12monthproductasm.Columns)
        //    {
        //        str12monthproductsale.Append("'");
        //        str12monthproductsale.Append(column.ColumnName);
        //        str12monthproductsale.Append("',");
        //    }

        //    str12monthproductsale.Remove(str12monthproductsale.Length - 1, 1);

        //    str12monthproductsale.Append("],");

        //    foreach (DataRow row in dt12monthproductasm.Rows)
        //    {
        //        str12monthproductsale.Append("['" + row[dt12monthproductasm.Columns[0].ColumnName] + "',");

        //        foreach (DataColumn column in dt12monthproductasm.Columns)
        //        {
        //            if (column.ColumnName != "Month")
        //            {
        //                str12monthproductsale.Append("" + row[column] + ",");
        //            }
        //        }
        //        str12monthproductsale.Remove(str12monthproductsale.Length - 1, 1);
        //        str12monthproductsale.Append("],");
        //    }

        //    str12monthproductsale.Remove(str12monthproductsale.Length - 1, 1);
        //    str12monthproductsale.Append("]);");

        //    str12monthproductsale.Append(@"var options = { title : 'Distributors Sale Last Two Month', vAxis: {title: 'Sale'},  hAxis: {title: 'Last Two Month Sale'}, 'width':'100%', seriesType: 'bars', series: {4: {type: 'Area'}} };");
        //    str12monthproductsale.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divmonthsaleasm'));  chart.draw(dt12monthproductasm, options); } google.setOnLoadCallback(drawVisualization);");
        //    str12monthproductsale.Append(" </script>");

        //    Literalmonthsaleasm.Text = str12monthproductsale.ToString();

        //    /*DISTRIBUTOR WISE SALE*/

        //    strcategorysale.Append(@"<script type='text/javascript'>    
        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //        function drawVisualization() {         
        //        var dtmonthsaleasm = google.visualization.arrayToDataTable([  
        //        ['MonthSale', 'Item'],");

        //    foreach (DataRow row in dtmonthsaleasm.Rows)
        //    {
        //        strcategorysale.Append("['" + row["Item"] + "'," + row["MonthSale"] + "],");
        //    }
        //    strcategorysale.Remove(strcategorysale.Length - 1, 1);

        //    strcategorysale.Append("]);");

        //    strcategorysale.Append(@"var options = { title : 'MTD SO SALE CONTRIBUTION', vAxis: {title: 'MonthSale'},  hAxis: {title: 'Item'},'width':'100%', is3D:true ,sliceVisibilityThreshold :0};");

        //    strcategorysale.Append(@"var chart = new google.visualization.PieChart(document.getElementById('chart_divprimarysaleweekasm'));  chart.draw(dtmonthsaleasm, options); } google.setOnLoadCallback(drawVisualization);");
        //    strcategorysale.Append(" </script>");

        //    Literalprimarysaleweekasm.Text = strcategorysale.ToString();
        //}
        //catch
        //{

        //}
        //finally
        //{
        //    ds.Dispose();
        //    dtmonthsaleasm.Dispose();
        //    dt12monthproductasm.Dispose();
        //    //dtcurrentproduct.Dispose();
        //    //dtretailersale.Dispose();
        //    //dttsisale.Dispose();
        //    dtcategorysaleasm.Dispose();

        //    strScriptmonthsale.Clear();
        //    str12monthproductsale.Clear();
        //    //strScriptcurrentproductsale.Clear();
        //    //strRetailer.Clear();
        //    //strtsisale.Clear();
        //    strcategorysale.Clear();
        //}
    }
    #endregion

    #region bindchartasmprimary for asm primary and secondary sale for month and weekly,target vs achv 
    private void BindChartAsmPrimary()
    {
        //    DataSet ds = new DataSet();
        //    DataTable dtprimarysaleformonthasm = new DataTable();
        //    DataTable dtprimarysaleforweekasm = new DataTable();
        //    DataTable dttargatemonthasm = new DataTable();
        //    //DataTable dtcurrentproduct = new DataTable();
        //    //DataTable dtretailersale = new DataTable();
        //    //DataTable dttsisale = new DataTable();
        //    StringBuilder strprimarysaleformonth = new StringBuilder();
        //    StringBuilder strrimarysaleforweek = new StringBuilder();
        //    StringBuilder strtargatemonth = new StringBuilder();
        //    //StringBuilder strScriptcurrentproductsale = new StringBuilder();
        //    //StringBuilder strRetailer = new StringBuilder();
        //    //StringBuilder strtsisale = new StringBuilder();
        //    try
        //    {
        //        ClsReports clsreport = new ClsReports();
        //        if (ViewState["AsmStartDate"].ToString() == "" && ViewState["AsmEndDate"].ToString() == "")
        //        {
        //            DateTime now = DateTime.Now;
        //            DateTime startDate = new DateTime(now.Year, now.Month, 1);
        //            DateTime endDate = DateTime.Now;
        //            ViewState["AsmStartDate"] = startDate.ToString(date).Replace('-', '/');
        //            ViewState["AsmEndDate"] = endDate.ToString(date).Replace('-', '/');

        //            ds = clsreport.BindchartPrimarySecondary(ViewState["AsmStartDate"].ToString().Trim(), ViewState["AsmEndDate"].ToString().Trim(),
        //                                                            HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                            HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                            "7F62F951-9D1F-4B8D-803B-74EEBA468CEE");
        //        }
        //        else if (ViewState["AsmStartDate"].ToString() != "" && ViewState["AsmEndDate"].ToString() != "")
        //        {

        //            ds = clsreport.BindchartPrimarySecondary(ViewState["AsmStartDate"].ToString().Trim(), ViewState["AsmEndDate"].ToString().Trim(),
        //                                                            HttpContext.Current.Session["UserID"].ToString().Trim(),
        //                                                            HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
        //                                                            "7F62F951-9D1F-4B8D-803B-74EEBA468CEE");
        //        }
        //        dtprimarysaleformonthasm = ds.Tables[0];
        //        dtprimarysaleforweekasm = ds.Tables[1];
        //        dttargatemonthasm = ds.Tables[2];
        //        //dtcurrentproduct = ds.Tables[2];
        //        //dtretailersale = ds.Tables[3];
        //        //dttsisale = ds.Tables[4];

        //        strprimarysaleformonth.Append(@"<script type='text/javascript'>  
        //                google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        <script type='text/javascript'>  
        //                function drawVisualization() {         
        //                var dtprimarysaleformonthasm = google.visualization.arrayToDataTable([  
        //                [ 'Month','PrimarySale','SecondarySale'],");

        //        foreach (DataRow row in dtprimarysaleformonthasm.Rows)
        //        {
        //            strprimarysaleformonth.Append("['" + row["Month"] + "'," + row["PrimarySale"] + "," + row["SecondarySale"] + "],");
        //        }
        //        strprimarysaleformonth.Remove(strprimarysaleformonth.Length - 1, 1);
        //        strprimarysaleformonth.Append("]);");

        //        strprimarysaleformonth.Append(@"var options = { title : 'YTD. Primary And Secondary Sale', vAxis: {title: 'SecondarySale'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'line', series: {0: {type: 'area'}}};");
        //        strprimarysaleformonth.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divprimarysalemonthasm'));  chart.draw(dtprimarysaleformonthasm, options); } google.setOnLoadCallback(drawVisualization);");
        //        strprimarysaleformonth.Append(" </script>");

        //        Literalprimarysalemonthasm.Text = strprimarysaleformonth.ToString();

        //        /*DISTRIBUTOR WISE SALE*/
        //        //strScriptmonthsale.Append(@"<script type='text/javascript'>  
        //        //        google.load('visualization', '1', {packages: ['corechart']});</script>  
        //        //        <script type='text/javascript'>  
        //        //        function drawVisualization() {         
        //        //        var dtmonthsale = google.visualization.arrayToDataTable([  
        //        //        ['Month','Bindal Agency','Garg Enterprises (Sohna)','Jaina Genaral Store','Jindal Distributor','Khwaish Enterprises New','Madhur Marketing Co.','Rahul Traders(Haryana)','Singla Agencies'],");

        //        //foreach (DataRow row in dtmonthsale.Rows)
        //        //{
        //        //    strScriptmonthsale.Append("['" + row["Month"] + "'," + row["Bindal Agency"] + "," + row["Garg Enterprises (Sohna)"] + "," + row["Jaina Genaral Store"] + "," + row["Jindal Distributor"] + "," + row["Khwaish Enterprises New"] + "," + row["Madhur Marketing Co."] + "," + row["Rahul Traders(Haryana)"] + "," + row["Singla Agencies"] + "],");

        //        //}

        //        strrimarysaleforweek.Append(@"<script type='text/javascript'>  
        //            google.load('visualization', '1', {packages: ['corechart']});</script>  
        //            <script type='text/javascript'>  
        //            function drawVisualization() {         
        //            var dtprimarysaleforweekasm = google.visualization.arrayToDataTable([  
        //                [ 'Month','PrimarySale','SecondarySale'],");

        //        foreach (DataRow row in dtprimarysaleforweekasm.Rows)
        //        {
        //            strrimarysaleforweek.Append("['" + row["Month"] + "'," + row["PrimarySale"] + "," + row["SecondarySale"] + "],");
        //        }
        //        strrimarysaleforweek.Remove(strrimarysaleforweek.Length - 1, 1);
        //        strrimarysaleforweek.Append("]);");

        //        strrimarysaleforweek.Append(@"var options = { title : 'MTD Primary and Secondary sale Weekly', vAxis: {title: 'SecondarySale'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'line', series: {0: {type: 'area'}}};");
        //        strrimarysaleforweek.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_div12monthproductasm'));  chart.draw(dtprimarysaleforweekasm, options); } google.setOnLoadCallback(drawVisualization);");
        //        strrimarysaleforweek.Append(" </script>");
        //        Literalmonthproductasm.Text = strrimarysaleforweek.ToString();

        //        strtargatemonth.Append(@"<script type='text/javascript'>  
        //            google.load('visualization', '1', {packages: ['corechart']});</script>  
        //            <script type='text/javascript'>  
        //            function drawVisualization() {         
        //            var dttargatemonthasm = google.visualization.arrayToDataTable([  
        //                [ 'Month','Target','Achievement'],");

        //        foreach (DataRow row in dttargatemonthasm.Rows)
        //        {
        //            strtargatemonth.Append("['" + row["Month"] + "'," + row["Target"] + "," + row["Achievement"] + "],");
        //        }
        //        strtargatemonth.Remove(strtargatemonth.Length - 1, 1);
        //        strtargatemonth.Append("]);");

        //        strtargatemonth.Append(@"var options = { title : 'MTD Target vs Achievement', vAxis: {title: 'Target vs Achievement'},  hAxis: {title: 'Month'}, 'width':'100%', seriesType: 'bars',};");
        //        strtargatemonth.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_divtargatechvasm'));  chart.draw(dttargatemonthasm, options); } google.setOnLoadCallback(drawVisualization);");
        //        strtargatemonth.Append(" </script>");
        //        Literaltargatechvasm.Text = strtargatemonth.ToString();
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        ds.Dispose();
        //        dtprimarysaleformonthasm.Dispose();
        //        dtprimarysaleforweekasm.Dispose();
        //        dttargatemonthasm.Dispose();
        //        //dtcurrentproduct.Dispose();
        //        //dtretailersale.Dispose();
        //        //dttsisale.Dispose();

        //        strprimarysaleformonth.Clear();
        //        strrimarysaleforweek.Clear();
        //        strtargatemonth.Clear();
        //        //strScriptcurrentproductsale.Clear();
        //        //strRetailer.Clear();
        //        //strtsisale.Clear();
        //    }
        //}
        #endregion
    }
}