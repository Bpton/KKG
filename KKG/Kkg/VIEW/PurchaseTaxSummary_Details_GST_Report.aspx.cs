using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class VIEW_PurchaseTaxSummary_Details_GST_Report : System.Web.UI.Page
{

    #region Properties

    public string ServiceURL
    {
        get
        {
            return this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.Substring(0, this.Context.Request.Url.AbsoluteUri.LastIndexOf("/")).LastIndexOf("/")) + "/Services/";
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadDepotName();
        this.LoadTpuDepot();
        ddldetails.SelectedValue = "1";
    }

    #endregion

    #region Load Depot
    public void LoadDepotfortype()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindDepot_Primary();
            ddlparty.Items.Clear();
            ddlparty.DataSource = dt;
            ddlparty.DataTextField = "BRNAME";
            ddlparty.DataValueField = "BRID";
            ddlparty.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    #endregion

    #region Load Tpu_Depot
    public void LoadTpuDepot()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindTpu_Depot();
            ddlparty.Items.Clear();
            ddlparty.DataSource = dt;
            ddlparty.DataTextField = "BRNAME";
            ddlparty.DataValueField = "ID";
            ddlparty.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Party
    public void LoadDistributor()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindTPUPatyName();
            ddlparty.Items.Clear();
            ddlparty.DataSource = dt;
            ddlparty.DataTextField = "VENDORNAME";
            ddlparty.DataValueField = "VENDORID";
            ddlparty.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = clsreport.BindDepot();
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = clsreport.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Invoice Type Dropdown change event
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "1")/*all*/
        {
            this.LoadTpuDepot();
        }
        else if (ddltype.SelectedValue == "2")/*Purchase*/
        {
            this.LoadDistributor();
        }
        else if (ddltype.SelectedValue == "3")/*Receive*/
        {
            this.LoadDepotfortype();
        }
        else if (ddltype.SelectedValue == "4")/*Purchase Return*/
        {
            this.LoadDistributor();
        }

    }
    #endregion
}