using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmProductionOrderClosingMM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlADD.Style["display"] = "";

                #region Add FinYear Wise Date Lock
                string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
                string startyear = finyear.Substring(0, 4);
                int startyear1 = Convert.ToInt32(startyear);
                string endyear = finyear.Substring(5);
                int endyear1 = Convert.ToInt32(endyear);
                DateTime oDate = new DateTime(startyear1, 04, 01);
                DateTime cDate = new DateTime(endyear1, 03, 31);
                DateTime today1 = DateTime.Now;
                CalendarFromDate.StartDate = oDate;
                CalendarExtenderToDate.StartDate = oDate;
                if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
                {
                    this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    CalendarFromDate.EndDate = today1;
                    CalendarExtenderToDate.EndDate = today1;
                }
                else
                {
                    this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    CalendarFromDate.EndDate = cDate;
                    CalendarExtenderToDate.EndDate = cDate;
                }
                #endregion
                LoadBulkProduct();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region LoadProductioOrderClosing
    public void LoadProductioOrderClosing()
    {
        try
        {
            ClsProductionOrderClosingMM clsPOC = new ClsProductionOrderClosingMM();
            DataTable dt = clsPOC.BindProductionOrder(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString(), ddlBulkProduct.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvProductionOrderClose.DataSource = dt;
                this.gvProductionOrderClose.DataBind();
            }
            else
            {
                this.gvProductionOrderClose.DataSource = null;
                this.gvProductionOrderClose.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadBulkProduct
    public void LoadBulkProduct()
    {
        try
        {
            ClsProductionOrderClosingMM clsPOC = new ClsProductionOrderClosingMM();
            DataTable dtBP = new DataTable();
            string DepotId = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
            dtBP = clsPOC.BindBulkProduct(DepotId);
            if (dtBP.Rows.Count > 0)
            {
                if (dtBP.Rows.Count == 1)
                {
                    this.ddlBulkProduct.Items.Clear();
                    this.ddlBulkProduct.AppendDataBoundItems = true;
                    this.ddlBulkProduct.Items.Add(new ListItem("Select All", "0"));
                    this.ddlBulkProduct.DataSource = dtBP;
                    this.ddlBulkProduct.DataValueField = "ID";
                    this.ddlBulkProduct.DataTextField = "PRODUCTALIAS";
                    this.ddlBulkProduct.DataBind();
                    this.ddlBulkProduct.SelectedValue = Convert.ToString(dtBP.Rows[0]["ID"]);
                }
                if (dtBP.Rows.Count > 1)
                {
                    this.ddlBulkProduct.Items.Clear();
                    this.ddlBulkProduct.AppendDataBoundItems = true;
                    this.ddlBulkProduct.Items.Add(new ListItem("Select All", "0"));
                    this.ddlBulkProduct.DataSource = dtBP;
                    this.ddlBulkProduct.DataValueField = "ID";
                    this.ddlBulkProduct.DataTextField = "PRODUCTALIAS";
                    this.ddlBulkProduct.DataBind();

                }
            }
            else
            {
                this.ddlBulkProduct.Items.Clear();
                this.ddlBulkProduct.AppendDataBoundItems = true;
                this.ddlBulkProduct.Items.Add(new ListItem("Select BS", "0"));
                this.ddlBulkProduct.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }


    }

    #endregion

    #region Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CreateDataTable();
            LoadProductioOrderClosing();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatable(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.TableName = "XMLData";
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    #endregion

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("PRODUCTION_ORDERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CLOSEDDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCEQTY", typeof(string)));
        HttpContext.Current.Session["PRODUCTIOORDERCLOSE"] = dt;
    }
    #endregion

    #region Conver_To_ISO
    public string Conver_To_ISO(string dt)
    {
        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + month + day;
        return dt;
    }
    #endregion

    protected void btnsave_Click(object sender, EventArgs e)
    {
        ClsProductionOrderClosingMM clsPOC = new ClsProductionOrderClosingMM();
        int id = 0;
        if (HttpContext.Current.Session["PRODUCTIOORDERCLOSE"] == null)
        {
            CreateDataTable();
        }
        DataTable DT = (DataTable)HttpContext.Current.Session["PRODUCTIOORDERCLOSE"];
        int count = 0;
        string XML = string.Empty;
        int result = 0;

        string SALEINVOICEID = string.Empty;
        foreach (GridViewRow gvrow in gvProductionOrderClose.Rows)
        {
            CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
            if (chkBx.Checked)
            {
                Label lblPRODUCTION_ORDERID = (Label)gvrow.FindControl("lblPRODUCTION_ORDERID");
                TextBox grdtxtClosingdate = (TextBox)gvrow.FindControl("grdtxtClosingdate");
                Label lblENTRYDATE = (Label)gvrow.FindControl("lblENTRYDATE");
                Label lblPRODUCTIONNO = (Label)gvrow.FindControl("lblPRODUCTIONNO");
                Label lblBalanceqty = (Label)gvrow.FindControl("lblBalanceqty");

                int ENTRYDATE = Convert.ToInt32(Conver_To_ISO(lblENTRYDATE.Text.Trim()));
                int Closingdate = Convert.ToInt32(Conver_To_ISO(grdtxtClosingdate.Text.Trim()));
                if (ENTRYDATE <= Closingdate)
                {
                    count = count + 1;
                    DataRow dr = DT.NewRow();
                    dr["PRODUCTION_ORDERID"] = lblPRODUCTION_ORDERID.Text.Trim();
                    dr["CLOSEDDATE"] = grdtxtClosingdate.Text.Trim();
                    dr["BALANCEQTY"] = lblBalanceqty.Text.Trim();
                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                }
                else
                {
                    Session["PRODUCTIONNO"] = lblPRODUCTIONNO.Text.Trim();
                    result = 1;
                    break;
                }
            }
        }
        if (result == 1)
        {
            MessageBox1.ShowInfo("Closing date  must be greater than Entry Date of  <b><font color='red'> " + Session["PRODUCTIONNO"] + " </font></b>... ", 60, 500);
        }
        else
        {
            if (count > 0)
            {
                HttpContext.Current.Session["PRODUCTIOORDERCLOSE"] = DT;
                XML = ConvertDatatable(DT);
                id = clsPOC.saveproductionOrderclosing(XML);
                this.gvProductionOrderClose.DataSource = null;
                this.gvProductionOrderClose.DataBind();
            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Plese Select atleast 1 record... ");
            }
            else
            {
                if (id > 0)
                {
                    MessageBox1.ShowSuccess("Record saved successfully..");
                    this.gvProductionOrderClose.DataSource = null;
                    this.gvProductionOrderClose.DataBind();
                    HttpContext.Current.Session["PRODUCTIOORDERCLOSE"] = null;
                    Session["PRODUCTIONNO"] = null;
                    result = 0;
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record ");
                    HttpContext.Current.Session["PRODUCTIOORDERCLOSE"] = null;
                    result = 0;
                }
            }
        }
    }
}