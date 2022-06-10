using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmTpuPOClosing : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlADD.Style["display"] = "";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.LoadTPUName();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadPurchaseOrder
    public void LoadPurchaseOrder()
    {
        try
        {
            ClsDepotOrderClosing clsPOC = new ClsDepotOrderClosing();
            DataTable dt = clsPOC.FetchPoOrder(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), this.ddlTPUName.SelectedValue.Trim(), this.ddlstatus.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvPurchaseOrderClose.DataSource = dt;
                this.gvPurchaseOrderClose.DataBind();
            }
            else
            {
                this.gvPurchaseOrderClose.DataSource = null;
                this.gvPurchaseOrderClose.DataBind();
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
            LoadPurchaseOrder();
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
        dt.Columns.Add(new DataColumn("POID", typeof(string)));
        dt.Columns.Add(new DataColumn("ISCLOSED", typeof(string)));
        dt.Columns.Add(new DataColumn("CLOSEDDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("CLOSEINGREASON", typeof(string)));
        dt.Columns.Add(new DataColumn("CLOSEINGREMARKS", typeof(string)));
        HttpContext.Current.Session["PURCHASEORDERCLOSE"] = dt;
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

    #region Save Event
    protected void btnsave_Click(object sender, EventArgs e)
    {
        ClsDepotOrderClosing clsPOC = new ClsDepotOrderClosing();
        int id = 0;
        if (HttpContext.Current.Session["PURCHASEORDERCLOSE"] == null)
        {
            CreateDataTable();
        }
        DataTable DT = (DataTable)HttpContext.Current.Session["PURCHASEORDERCLOSE"];
        int count = 0;
        string XML = string.Empty;
        foreach (GridViewRow gvrow in gvPurchaseOrderClose.Rows)
        {
            CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
            if (chkBx.Checked)
            {
                Label lblPOID = (Label)gvrow.FindControl("lblPOID");
                TextBox grdtxtClosingdate = (TextBox)gvrow.FindControl("grdtxtClosingdate");
                Label lblPONO = (Label)gvrow.FindControl("lblPONO");
                DropDownList ddlClosingReason = (DropDownList)gvrow.FindControl("ddlClosingReason");
                TextBox grdtxtRemarks = (TextBox)gvrow.FindControl("grdtxtRemarks");
                //decimal lblREMAININGQTY = Convert.ToDecimal(((Label)gvrow.FindControl("lblREMAININGQTY")).Text);
                string PONO = ((Label)gvrow.FindControl("lblPONO")).Text.Trim();

                //if (lblREMAININGQTY > 0)
                //{
                //    MessageBox1.ShowInfo("Remaining Qty is Pending for This PO:- <font color='red'>" + PONO + "</font>", 55, 600);
                //    return;
                //}
                //else
                //{
                count = count + 1;
                DataRow dr = DT.NewRow();
                dr["POID"] = lblPOID.Text.Trim();
                dr["ISCLOSED"] = "Y";
                dr["CLOSEDDATE"] = grdtxtClosingdate.Text.Trim();
                dr["CLOSEINGREASON"] = ddlClosingReason.SelectedItem.ToString();
                dr["CLOSEINGREMARKS"] = grdtxtRemarks.Text.Trim();
                DT.Rows.Add(dr);
                DT.AcceptChanges();
                //}
            }
        }

        if (count > 0)
        {
            HttpContext.Current.Session["PURCHASEORDERCLOSE"] = DT;
            XML = ConvertDatatable(DT);
            id = clsPOC.SaveTpuPoOrderClosing(XML, this.ddlstatus.SelectedValue.Trim(), Convert.ToString(Session["IUserID"]).Trim());
            this.gvPurchaseOrderClose.DataSource = null;
            this.gvPurchaseOrderClose.DataBind();
        }
        if (count == 0)
        {
            MessageBox1.ShowInfo("Plese Select atleast 1 record..");
        }
        else
        {
            if (id > 0)
            {
                MessageBox1.ShowSuccess("Record saved successfully..");
                //MessageBox1.ShowSuccess("<b><font color='#666'>Requesting Approval : " + totalapproval + "</font><br><br> <font color='green'>Successful approval : " + successcount + "</font><br><br> <font color='maroon'>Failure approval : " + failcount + "</font></b>", 80, 350);
                this.gvPurchaseOrderClose.DataSource = null;
                this.gvPurchaseOrderClose.DataBind();
                HttpContext.Current.Session["PURCHASEORDERCLOSE"] = null;
                Session["PONO"] = null;
            }
            else
            {
                MessageBox1.ShowInfo("Error Saving Record");
                HttpContext.Current.Session["PURCHASEORDERCLOSE"] = null;
            }
        }
    }
    #endregion

    #region ddlTPUName_SelectedIndexChanged
    protected void ddlTPUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CreateDataTable();
            LoadPurchaseOrder();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion  

    #region LoadTPU
    public void LoadTPUName()
    {
        ClsDepotOrderClosing clsPOC = new ClsDepotOrderClosing();
        try
        {
            DataTable dt = clsPOC.BindTPU();
            if (dt.Rows.Count > 0)
            {
                this.ddlTPUName.Items.Clear();
                this.ddlTPUName.Items.Insert(0, new ListItem("ALL", "0"));
                this.ddlTPUName.AppendDataBoundItems = true;
                this.ddlTPUName.DataSource = dt;
                this.ddlTPUName.DataValueField = "VENDORID";
                this.ddlTPUName.DataTextField = "VENDORNAME";
                this.ddlTPUName.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion
}