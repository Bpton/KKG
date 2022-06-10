#region Developer Info
/*
 * name:Pritam Basu
 * start date:09-01-2021
 * purpose:Requestion Order Update
 */
#endregion


using BAL;
using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRequestionsApproval : System.Web.UI.Page
{
    DateTime today1 = DateTime.Now;
    string date = "dd/MM/yyyy";
    ClsRequisitionMaster oRequisition = new ClsRequisitionMaster();
    ClsProductionOrder oClsProductionOrder = new ClsProductionOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtSearchFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtSearchToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.DateLock();
            }

        }
        catch(Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }
    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            CalendarImageSearch2Extender2.StartDate = oDate;
            CalendarSearchExtender3.StartDate = oDate;
            CalendarExtender1.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtSearchFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtSearchToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarImageSearch2Extender2.EndDate = today1;
                CalendarSearchExtender3.EndDate = today1;
                CalendarExtender1.EndDate = today1;
            }
            else
            {
                this.txtSearchFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtSearchToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarImageSearch2Extender2.EndDate = cDate;
                CalendarSearchExtender3.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            //loadStoreLocation();
            loadDepartMent();
            loadToDepartMent();
            this.DateLock();

        }
        catch(Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadStoreLocation()
    {
        try
        {
            string mode = "";

            if(this.bulkRequistionId.Value=="")
            {
                mode = "I";
            }
            else
            {
                mode = "U";
            }
            string deptId = this.ddlToDepartment.SelectedValue;
            string Mode = string.Empty;
            string userId = HttpContext.Current.Session["USERID"].ToString();
            DataTable dtobj = new DataTable();
            this.ddlStoreLocation.Items.Clear();
            this.ddlStoreLocation.Items.Add(new ListItem("Select", "0"));
            this.ddlStoreLocation.DataSource = oRequisition.loadstorelocationUserWise(mode,deptId);
            this.ddlStoreLocation.DataValueField = "ID";
            this.ddlStoreLocation.DataTextField = "NAME";
            this.ddlStoreLocation.DataBind();
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadDepartMent()
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindDepartment(HttpContext.Current.Session["USERID"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.ddlDepartMent.Items.Clear();
                this.ddlDepartMent.AppendDataBoundItems = true;
                this.ddlDepartMent.DataSource = dt;
                this.ddlDepartMent.DataTextField = "DEPTNAME";
                this.ddlDepartMent.DataValueField = "DEPTID";
                this.ddlDepartMent.DataBind();
            }
            else
            {
                this.ddlDepartMent.Items.Clear();
                this.ddlDepartMent.Items.Add(new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    public void loadToDepartMent()
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindRequistionDepartment();
            if (dt.Rows.Count > 0)
            {
                this.ddlToDepartment.Items.Clear();
                this.ddlToDepartment.AppendDataBoundItems = true;
                this.ddlToDepartment.DataSource = dt;
                this.ddlToDepartment.DataTextField = "DEPTNAME";
                this.ddlToDepartment.DataValueField = "DEPTID";
                this.ddlToDepartment.DataBind();
            }
            else
            {
                this.ddlToDepartment.Items.Clear();
                this.ddlToDepartment.Items.Add(new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    public void loadRequisition(string mode,string userId,string fromDate,string toDate)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = oRequisition.fetchRequisition(mode, userId, fromDate, toDate);
            if(dt.Rows.Count>0)
            {
                this.grdRequisition.DataSource = dt;
                this.grdRequisition.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = "Search";
            string fromDate = this.txtFromDate.Text;
            string toDate = this.txtToDate.Text;
            string userId = HttpContext.Current.Session["UserID"].ToString();
            loadRequisition(mode, userId,fromDate, toDate);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    protected void PurchaseOrderCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnRequisitionDetails_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            string requId = "";
            string requ = "";

            // Select the checkboxes from the GridView control
            for (int i = 0; i < grdRequisition.Rows.Count; i++)
            {
                GridViewRow row = grdRequisition.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("PurchaseOrderCheckBox")).Checked;

                if (isChecked)
                {
                    Label t = (Label)row.FindControl("lblREQUISITIONID");
                    requ = Convert.ToString(t.Text);
                    requId += requ + ',';
                }
            }

            if(requId=="")
            {
                MessageBox1.ShowWarning("Please Check at least one record");
                return;
            }
            string stroeId = this.ddlStoreLocation.SelectedValue.ToString();
            if(stroeId=="0")
            {
                MessageBox1.ShowWarning("Please Select Storelocation first");
                return;
            }
            dt = oRequisition.FetchRequistionDetails(requId, HttpContext.Current.Session["USERID"].ToString(), stroeId);
            if(dt.Rows.Count>0)
            {
                this.grdRequistionDetails.DataSource = dt;
                this.grdRequistionDetails.DataBind();
            }
            else
            {
                this.grdRequistionDetails.DataSource = null;
                this.grdRequistionDetails.DataBind();
            }
            
           

        }
        catch(Exception ex)
        {

        }
    }


    protected void btnViewStock_Click(object sender, EventArgs e)
    {
        string itemId = "";
        DataTable dt = new DataTable();
        try
        {
            this.divStoreLocationWiseStock.Style["display"] = "";
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblID = (Label)gvr.FindControl("lblMATERIALID");
            itemId = lblID.Text.Trim();
            dt = oClsProductionOrder.showStockQtyStorelocationWise(itemId);
            if (dt.Rows.Count > 0)
            {
                this.grdStoreWiseStock.DataSource = dt;
                this.grdStoreWiseStock.DataBind();
            }
            else
            {
                MessageBox1.ShowWarning("Stock Not Avilable");
                this.grdStoreWiseStock.DataSource = null;
                this.grdStoreWiseStock.DataBind();
                return;
            }

        }

        catch (Exception ex)
        {
            string msg = ex.ToString();
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StringBuilder str = new StringBuilder();
        string requId = "";
        string requ = "";
        string pId = "";
        string p = "";
        string pName = "";
        string pN = "";
        string xmlRequistion = string.Empty;
        // Select the checkboxes from the GridView control
        for (int i = 0; i < grdRequisition.Rows.Count; i++)
        {
            GridViewRow row = grdRequisition.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("PurchaseOrderCheckBox")).Checked;

            if (isChecked)
            {
                Label t = (Label)row.FindControl("lblREQUISITIONID");
                Label productId = (Label)row.FindControl("lblMATERIALID");
                Label productName = (Label)row.FindControl("lblMATERIALNAME");

                requ = Convert.ToString(t.Text);
                requId += requ + ',';

                p = Convert.ToString(productId.Text);
                pId += p + ',';

                pN = Convert.ToString(productName.Text);
                pName += pN + ',';
            }
        }

        string mainDepartment = this.ddlDepartMent.SelectedValue.ToString();
        string toDepartment = this.ddlToDepartment.SelectedValue.ToString();
        string storeLocation = this.ddlStoreLocation.SelectedValue.ToString();

        DataTable dtRequistion = new DataTable();
        dtRequistion = CreateTableForBulkRequistion();

        /*for getting data from grid and store into datatable using session*/
        foreach (GridViewRow row in grdRequistionDetails.Rows)
        {
            Label lblMATERIALID = (Label)row.FindControl("lblMATERIALID");
            Label lblMATERIALNAME = (Label)row.FindControl("lblMATERIALNAME");
            Label lblUOMID = (Label)row.FindControl("lblUOMID");
            Label lblUOMNAME = (Label)row.FindControl("lblUOMNAME");
            Label lblQTY = (Label)row.FindControl("lblQTY");
            Label lblBUFFERQTY = (Label)row.FindControl("lblBUFFERQTY");
            Label lblNETQTY = (Label)row.FindControl("lblNETQTY");
            Label lblSTOCKQTY = (Label)row.FindControl("lblSTOCKQTY");
            TextBox txtREQUIREDQTY = (TextBox)row.FindControl("txtREQUIREDQTY");

            
            decimal Qty = Convert.ToDecimal(((Label)row.FindControl("lblQTY")).Text);
            decimal BUFFERQTY = Convert.ToDecimal(((Label)row.FindControl("lblBUFFERQTY")).Text);
            decimal NETQTY = Convert.ToDecimal(((Label)row.FindControl("lblNETQTY")).Text.Trim());
            decimal STOCKQTY = Convert.ToDecimal(((Label)row.FindControl("lblSTOCKQTY")).Text.Trim());
            decimal REQUIREDQTY = Convert.ToDecimal(((TextBox)row.FindControl("txtREQUIREDQTY")).Text.Trim());

            //if (NETQTY < REQUIREDQTY)
            //{
            //    MessageBox1.ShowInfo("Stock Not Available for:- <font color='red'>" + lblMATERIALNAME + "</font>", 50, 400);
            //    return;
            //}
            //else
            //{
                DataRow dr = dtRequistion.NewRow();
                dr[0] = lblMATERIALID.Text;
                dr[1] = lblMATERIALNAME.Text;
                dr[2] = lblUOMID.Text;
                dr[3] = lblUOMNAME.Text;
                dr[4] = lblQTY.Text;
                dr[5] = lblBUFFERQTY.Text;
                dr[6] = lblNETQTY.Text;
                dr[7] = lblSTOCKQTY.Text;
                dr[8] = txtREQUIREDQTY.Text;
                dtRequistion.Rows.Add(dr);
            //}
        }

        xmlRequistion = ConvertDatatableToXML(dtRequistion);

        /*getting data from session*/
        string createdByUserid = string.Empty;
        string finYear = string.Empty;
        string bulkRequistionId = string.Empty;
        createdByUserid = HttpContext.Current.Session["USERID"].ToString();
        finYear = HttpContext.Current.Session["FINYEAR"].ToString();
        /*getting data from session*/

        bulkRequistionId = this.bulkRequistionId.Value.Trim();
        string result = string.Empty;
        result = oRequisition.insertUpdateBulkRequistion(bulkRequistionId, requId, pId, pName, mainDepartment, toDepartment, storeLocation, xmlRequistion, createdByUserid, finYear);
        if (result != "")
        {
            MessageBox1.ShowSuccess("Your Requistion Number Is:" + result);
            close();
            clearControls();
            return;
        }
        else
        {
            MessageBox1.ShowError("Error On Saving Record");
            return;
        }
       

    }

    #region Convert Datatable To XML
    public string ConvertDatatableToXML(DataTable dt)
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

    public DataTable CreateTableForBulkRequistion()
    {
        DataTable dtProductionOrderBOM = new DataTable();
        dtProductionOrderBOM.Clear();
        dtProductionOrderBOM.Columns.Add(new DataColumn("MATERIALID", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("QTY", typeof(decimal)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("BUFFERQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("NETQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("STOCKQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("REQUIREDQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("STORELOCATION_STOCKQTY", typeof(String)));
        return dtProductionOrderBOM;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        close();
        clearControls();
    }

    protected void ddlStoreLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.grdRequistionDetails.DataSource = null;
        this.grdRequistionDetails.DataBind();
    }

    public void close()
    {
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
    }

    public void clearControls()
    {
        this.grdRequisition.DataSource = null;
        this.grdRequisition.DataBind();
        this.grdRequistionDetails.DataSource = null;
        this.grdRequistionDetails.DataBind();
        this.grdStoreWiseStock.DataSource = null;
        this.grdStoreWiseStock.DataBind();
        this.bulkRequistionId.Value = "";
        this.ddlStoreLocation.SelectedValue = "0";


    }

    public void LoadBulkRequistion()
    {
        string mode = "loadRequistion";
        string userid = HttpContext.Current.Session["USERID"].ToString();
        string fromdate = this.txtSearchFromDate.Text;
        string todate = this.txtSearchToDate.Text;
        DataTable dt = new DataTable();
        dt = oRequisition.fetchRequisition(mode, userid, fromdate, todate);
        if(dt.Rows.Count>0)
        {
            this.grdLoadRequistion.DataSource = dt;
            this.grdLoadRequistion.DataBind();
        }
        else
        {
            this.grdLoadRequistion.DataSource = null;
            this.grdLoadRequistion.DataBind();
            MessageBox1.ShowInfo("No Data Found");
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        LoadBulkRequistion();
    }

    protected void btnGridEdit_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.entryNumber.Visible = true;
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblENTRYID = (Label)gvr.FindControl("lblREQUISTIONID");
            this.bulkRequistionId.Value = lblENTRYID.Text.Trim();
            DataSet ds = new DataSet();
            ds = oRequisition.fetchBulkRequistionDetails(this.bulkRequistionId.Value);
            loadStoreLocation();
            loadDepartMent();
            
            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtEnrtyNumber.Text = ds.Tables[0].Rows[0]["REQUISTIONNUM"].ToString().Trim();
                this.ddlDepartMent.SelectedValue = ds.Tables[0].Rows[0]["DEPTID"].ToString().Trim();
                loadToDepartMent();
                this.ddlToDepartment.SelectedValue = ds.Tables[0].Rows[0]["TDEPTID"].ToString().Trim();
                
                this.ddlStoreLocation.SelectedValue = ds.Tables[0].Rows[0]["STOREID"].ToString().Trim();
                this.ddlStoreLocation.Enabled = false;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.grdRequisition.DataSource = ds.Tables[1];
                this.grdRequisition.DataBind();
            }
            else
            {
                MessageBox1.ShowError("Header Info Not found Please Contact To admin");
                return;
            }
            #endregion

            #region Details Information

            DataTable dtRequistion = new DataTable();
            dtRequistion = CreateTableForBulkRequistion();
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    DataRow drEditPO = dtRequistion.NewRow();
                    drEditPO["MATERIALID"] = Convert.ToString(ds.Tables[2].Rows[i]["MATERIALID"]);
                    drEditPO["MATERIALNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["MATERIALNAME"]);
                    drEditPO["UOMID"] = Convert.ToString(ds.Tables[2].Rows[i]["UOMID"]);
                    drEditPO["UOMNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["UOMNAME"]);
                    drEditPO["QTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["QTY"]);
                    drEditPO["BUFFERQTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["BUFFERQTY"]);
                    drEditPO["NETQTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["NETQTY"]);
                    drEditPO["STOCKQTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["STOCKQTY"]);
                    drEditPO["REQUIREDQTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["REQUIREDQTY"]);
                    drEditPO["REQUIREDQTY"] = Convert.ToDecimal(ds.Tables[2].Rows[i]["REQUIREDQTY"]);
                    dtRequistion.Rows.Add(drEditPO);
                    dtRequistion.AcceptChanges();
                }

                this.grdRequistionDetails.DataSource = dtRequistion;
                this.grdRequistionDetails.DataBind();
            }
            else
            {
                MessageBox1.ShowError("Details Info Not found Please Contact To admin");
                return;
            }
            #endregion


        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void ddlToDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlStoreLocation.Enabled = true;
        loadStoreLocation();

    }
}