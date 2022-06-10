using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class VIEW_SalesTaxSummary_Details_GST_Report : System.Web.UI.Page
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
        LoadDepot();
        LoadBusinessSegment();
    }

    #endregion

    #region LoadBusinessSegment
    public void LoadBusinessSegment()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            DataTable dt = clsreport.BindBusinessegment();
            ddlbsegment.Items.Clear();
            ddlbsegment.Items.Add(new ListItem("ALL", "0"));
            ddlbsegment.AppendDataBoundItems = true;
            ddlbsegment.DataSource = dt;
            ddlbsegment.DataValueField = "ID";
            ddlbsegment.DataTextField = "NAME";
            ddlbsegment.DataBind();
            dt.Clear();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            DataTable dtdist = new DataTable();
            ClsStockReport clsrpt = new ClsStockReport();
            string Session = HttpContext.Current.Session["USERID"].ToString().Trim();
            if (Session == "8")
            {
                this.ddldepot.Items.Clear();
                this.ddldepot.DataSource = clsrpt.BindDepot();
                this.ddldepot.DataTextField = "BRNAME";
                this.ddldepot.DataValueField = "BRID";
                this.ddldepot.DataBind();
                dtdist.Clear();
            }
            else
            {
                dtdist = clsrpt.BindDepot(HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                if (dtdist.Rows.Count > 0)
                {
                    if (dtdist.Rows.Count == 1)
                    {
                        this.ddldepot.Items.Clear();
                        this.ddldepot.DataSource = dtdist;
                        this.ddldepot.DataTextField = "BRNAME";
                        this.ddldepot.DataValueField = "BRID";
                        this.ddldepot.DataBind();
                    }
                    if (dtdist.Rows.Count > 1)
                    {
                        this.ddldepot.Items.Clear();
                        this.ddldepot.DataSource = dtdist;
                        this.ddldepot.DataTextField = "BRNAME";
                        this.ddldepot.DataValueField = "BRID";
                        this.ddldepot.DataBind();
                        this.ddldepot.Enabled = true;
                    }
                    dtdist.Clear();
                }

                else
                {
                    this.ddldepot.Items.Clear();
                    this.ddldepot.DataSource = clsrpt.BindDepot();
                    this.ddldepot.DataTextField = "BRNAME";
                    this.ddldepot.DataValueField = "BRID";
                    this.ddldepot.DataBind();
                    this.ddldepot.Enabled = true;
                }
            }
            this.LoadParty();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadParty
    public void LoadParty()
    {
        try
        {
            DataTable dtdist = new DataTable();
            ClsStockReport clsrpt = new ClsStockReport();

            if (Session["TPU"].ToString() == "ADMIN")
            {
                string DepotID = string.Empty;
                // var query = from ListItem item in C where item.Selected select item;
                foreach (ListItem item in ddldepot.Items)
                {
                    // item ...
                    DepotID += item.Value + ",";

                }

                if (DepotID.Length > 1)
                {
                    DepotID = DepotID.Substring(0, DepotID.Length - 1);
                }
                if (DepotID.ToString() == "")
                {
                    dtdist = clsrpt.BindDistributorbyAdmin();
                }
                else
                {
                    if (ddltype_invoice.SelectedValue == "1")
                    {
                        dtdist = clsrpt.BindDistributorbyAdminWithDepot(DepotID.Trim());
                    }
                    if (ddltype_invoice.SelectedValue == "2")
                    {
                        dtdist = clsrpt.BindCustomerbyDepot(DepotID.Trim());
                    }
                    if (ddltype_invoice.SelectedValue == "3")
                    {
                        dtdist = clsrpt.BindDepotWithoutDepot(DepotID.Trim());
                    }

                }

                this.ddlparty.Items.Clear();
                this.ddlparty.DataSource = dtdist;
                this.ddlparty.DataTextField = "CUSTOMERNAME";
                this.ddlparty.DataValueField = "CUSTOMERID";
                this.ddlparty.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                if (ddltype_invoice.SelectedValue == "1")
                {
                    dtdist = clsrpt.BindDistributorbyAdminWithDepot(HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                }
                if (ddltype_invoice.SelectedValue == "2")
                {
                    dtdist = clsrpt.BindCustomerbyDepot(HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                }
                if (ddltype_invoice.SelectedValue == "3")
                {
                    dtdist = clsrpt.BindDepotWithoutDepot(HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                }

                if (dtdist.Rows.Count > 0)
                {
                    if (dtdist.Rows.Count == 1)
                    {
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                    if (dtdist.Rows.Count > 1)
                    {
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                }
                else
                {
                    string DepotID = string.Empty;
                    var query = from ListItem item in ddldepot.Items where item.Selected select item;
                    foreach (ListItem item in query)
                    {
                        // item ...
                        DepotID += item.Value + ",";

                    }

                    if (DepotID.Length > 1)
                    {
                        DepotID = DepotID.Substring(0, DepotID.Length - 1);
                        dtdist = clsrpt.BindCustomerbyDepot(DepotID.Trim());
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                    else
                    {
                        dtdist = clsrpt.BindDistributorbyAdmin();
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                }
            }
            else
            {
                string DepotID = string.Empty;
                // var query = from ListItem item in C where item.Selected select item;
                foreach (ListItem item in ddldepot.Items)
                {
                    // item ...
                    DepotID += item.Value + ",";

                }

                if (DepotID.Length > 1)
                {
                    DepotID = DepotID.Substring(0, DepotID.Length - 1);
                }
                if (DepotID.ToString() == "")
                {
                    dtdist = clsrpt.BindDistributorbyAdmin();
                }
                else
                {
                    if (ddltype_invoice.SelectedValue == "1")
                    {
                        dtdist = clsrpt.BindDistributorbyAdminWithDepot(DepotID);
                    }
                    if (ddltype_invoice.SelectedValue == "2")
                    {
                        dtdist = clsrpt.BindCustomerbyDepot(DepotID);
                    }
                    if (ddltype_invoice.SelectedValue == "3")
                    {
                        dtdist = clsrpt.BindDepotWithoutDepot(DepotID);
                    }
                }
                
                if (dtdist.Rows.Count > 0)
                {
                    if (dtdist.Rows.Count == 1)
                    {
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                    if (dtdist.Rows.Count > 1)
                    {
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                }
                else
                {

                    if (DepotID.Length > 1)
                    {
                        DepotID = DepotID.Substring(0, DepotID.Length - 1);
                        dtdist = clsrpt.BindCustomerbyDepot(DepotID.Trim());
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                    else
                    {
                        dtdist = clsrpt.BindDistributorbyAdmin();
                        this.ddlparty.Items.Clear();
                        this.ddlparty.DataSource = dtdist;
                        this.ddlparty.DataTextField = "CUSTOMERNAME";
                        this.ddlparty.DataValueField = "CUSTOMERID";
                        this.ddlparty.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}