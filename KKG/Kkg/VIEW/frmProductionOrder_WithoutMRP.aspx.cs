using BAL;
using Obout.Grid;
using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Net;

public partial class VIEW_frmProductionOrder_WithoutMRP : System.Web.UI.Page
{
    ProcessFramework oProcessFramework = new ProcessFramework();
    ClsProductionOrder oClsProductionOrder = new ClsProductionOrder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    DataTable dtGenerateScheduleTime = new DataTable();
    DataRow dr;
    decimal ProductQTY;
    int TimeInHour, TotalConsumption;
    decimal ActualTime;
    int startTime, endTime, estimatedTime, timeDifferent, addDays;
    string strScheduleEndDate, strEndHour, strEndMinute;
    DataTable dtProdOrderBOM = new DataTable();
    DataTable dtREQUISITION = new DataTable();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$(function () {$('#ContentPlaceHolder1_ddlFG').multiselect({includeSelectAllOption: true});});</script>", false);
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TYPE"] == "PRODUCTIONLEDGER")
                {
                    //pnlAdd.Style["display"] = "";
                    pnlDisplay.Style["display"] = "";
                    Div_Cancel.Visible = false;
                    GridEdit(Request.QueryString["ProductionID"]);
                }
                else if (Request.QueryString["Name"] == "Itemledger")
                {
                    DataTable dtpo = new DataTable();
                    string ReturnidID = Request.QueryString["VCHID"].ToString();
                    dtpo = oClsProductionOrder.BindPoidReturntWise(ReturnidID);
                    if (dtpo.Rows.Count > 0)
                    {
                        string ProductionID = dtpo.Rows[0]["PRODUCTIONORDER_ID"].ToString();
                        pnlDisplay.Style["display"] = "";
                        Div_Cancel.Visible = false;
                        GridEdit(ProductionID);
                    }
                }
                else if (Request.QueryString["MENUID"] != "1638")
                {
                    Div_Submit.Visible = false;
                    DateLock();
                    pnlAdd.Style["display"] = "none";
                    ddlbatch.Visible = false;
                    lblbatch.Visible = false;
                    txtbatch.Visible = false;
                    tdbatch.Visible = false;
                    BindProduct();
                    BindGrid();
                }
                else if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"].ToString().Trim() == "dashboard")
                    {
                        this.LoadMRP();
                        this.LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");
                        this.trAutoPoNo.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        this.pnlDisplay.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "none";
                        this.td_gridview.Style["display"] = "none";
                        this.divcancelorder.Visible = false;
                        this.Hdn_Fld.Value = "";
                        this.gvItem.DataSource = null;
                        this.gvItem.DataBind();
                        this.gvBOM.DataSource = null;
                        this.gvBOM.DataBind();
                        this.Generate.Visible = true;
                        this.gvPlannedOrder.DataSource = null;
                        this.gvPlannedOrder.DataBind();
                        this.Generate.Visible = true;
                        this.txtStartTime.Text = "00:00:00";
                        this.ddlframework.Items.Clear();
                        this.ddlPlant.Items.Clear();
                        this.ddlprocess.Items.Clear();
                        this.ddlType.SelectedValue = "0";
                        this.rdbatch.SelectedValue = "FG";
                    }
                }
                else
                {
                    DateLock();
                    pnlAdd.Style["display"] = "none";
                    ddlbatch.Visible = false;
                    //txtbatch.Visible = true;
                    lblbatch.Visible = false;
                    txtbatch.Visible = false;
                    tdbatch.Visible = false;
                    BindProduct();
                    BindGrid();
                    this.trUseQtyDtls.Visible = false;
                    this.trBatchDtls.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Events

    public void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = oClsProductionOrder.BindProductionOrderGridWithoutMRP(txtFromDateSearch.Text, txtToDateSearch.Text, ddlproduct.SelectedValue.ToString(), HttpContext.Current.Session["DEPOTID"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.gvCompany.DataSource = dt;
                this.gvCompany.DataBind();
            }
            else
            {
                this.gvCompany.DataSource = null;
                this.gvCompany.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void TXTQTY_OnTextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtBalanceQty = new DataTable();
        string fg = BindFINISHEDGOOD();
        decimal BULKQTY = 0, INPUTQTY = 0, BALANCEQTY = 0;
        string BulkName = string.Empty;
        TextBox TxtMarks = (TextBox)sender;
        TextBox txt = (TextBox)TxtMarks.FindControl("TXTQTY");
        decimal QTY = Convert.ToDecimal(txt.Text);


        if (this.txtProductionQty.Text == "")
        {
            this.txtProductionQty.Text = "0";
        }

        decimal planningQty = Convert.ToDecimal(this.txtProductionQty.Text);

        if (ddlProductionType.SelectedValue == "WP" && this.ddlType.SelectedValue=="FG" && QTY > planningQty)
        {
            MessageBox1.ShowWarning("Production Qty Cannot be greater than planning qty");
            foreach (GridViewRow gvRow in gvItem.Rows)
            {
                TextBox t = (TextBox)gvRow.FindControl("TXTQTY");
                t.Text = "0";
            }
            
            return;
        }
      
        decimal ConQty =0;

        foreach (GridViewRow gvRow in gvItem.Rows)
        {
            Label lblConversionQty = (Label)gvRow.FindControl("lblConversionQty");
            ConQty = Convert.ToDecimal(lblConversionQty.Text);
        }

        decimal qtyInbox = (QTY / ConQty);

        var rows = gvItem.Rows;
        foreach (GridViewRow row in rows)
        {
            TextBox t = (TextBox)row.FindControl("txtQtyinKg");
            t.Text =Convert.ToString(qtyInbox);
        }


        dtBalanceQty = oClsProductionOrder.ChkBulkBalanceQty("", ddlbatch.SelectedValue.ToString().Trim(), ddlframework.SelectedValue.ToString().Trim(), ddlprocess.SelectedValue.ToString().Trim(), QTY, ddlType.SelectedValue.ToString());
        if (dtBalanceQty.Rows.Count > 0)
        {
            BULKQTY = Convert.ToDecimal(dtBalanceQty.Rows[0]["BULKQTY"].ToString());
            INPUTQTY = Convert.ToDecimal(dtBalanceQty.Rows[0]["INPUTQTY"].ToString());
            BALANCEQTY = Convert.ToDecimal(dtBalanceQty.Rows[0]["BALANCEQTY"].ToString());
            //BulkName = dtBalanceQty.Rows[0]["NAME"].ToString();
            if (INPUTQTY <= BALANCEQTY)
            {
                dt = oClsProductionOrder.BindBOM(ddlprocess.SelectedValue, QTY, fg, ddlframework.SelectedValue.Trim(), Hdn_Fld.Value.Trim());
                if (dt.Rows.Count > 0)
                {
                    this.gvBOM.DataSource = dt;
                    this.gvBOM.DataBind();
                }
            }
            else
            {
                MessageBox1.ShowInfo("Your Remaing Qty is <font color='red'>" + "" + " (" + BALANCEQTY + ")." + "</font>" + "Please Give <= from Remaing Qty. ", 60, 500);
                this.gvBOM.DataSource = null;
                this.gvBOM.DataBind();
            }
        }
        else
        {
            //dt = oClsProductionOrder.BindBOM(ddlprocess.SelectedValue, QTY, fg, ddlframework.SelectedValue.Trim(), this.ddlfactory.SelectedValue.Trim());
            //if (dt.Rows.Count > 0)
            //{
            //    this.gvBOM.DataSource = dt;
            //    this.gvBOM.DataBind();
            //}
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
        }
    }

    protected void ddlMRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlProductionType.SelectedValue=="0")
            {
                MessageBox1.ShowWarning("Please Select Production Type First");
                return;
            }
            else if(this.ddlProductionType.SelectedValue== "WP" && ddlType.SelectedValue == "SFG")
            {
               
                this.ddlFG.SelectedValue = "0";
                this.ddlframework.SelectedValue = "0";
                this.ddlprocess.Items.Clear();
                this.ddlPlanningNumber.SelectedValue = "0";
                this.gvItem.DataSource = null;
                this.gvItem.DataBind();

                this.gvBOM.DataSource = null;
                this.gvBOM.DataBind();
                MessageBox1.ShowWarning("Please Check Production Type");
                return;
            }
            else if (this.ddlProductionType.SelectedValue == "WP" && ddlType.SelectedValue == "FG")
            {
                if (ddlType.SelectedValue == "FG")
                {
                    Label2.Text = "FINISHED GOODS";
                }
                else
                {
                    Label2.Text = "SEMI FINISHED GOODS";
                }
                this.ddlPlanningNumber.Enabled = true;
                loadPlannigNumber();
            }
            else
            {
                if (ddlType.SelectedValue == "FG")
                {
                    Label2.Text = "FINISHED GOODS";
                }
                else
                {
                    Label2.Text = "SEMI FINISHED GOODS";
                }
                LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");
            }
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddlFG_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
            this.gvItem.DataSource = null;
            this.gvItem.DataBind();
            string fg = BindFINISHEDGOOD();
            LoadPlantBasisOnMRPFG(ddlMRP.SelectedValue.ToString(), fg.ToString(), "S");
            if (rdbatch.SelectedValue == "0")
            {
                GenerateNo(fg, HttpContext.Current.Session["FINYEAR"].ToString(), txtentrydt.Text);
            }
            else if (rdbatch.SelectedValue == "1")
            {
                BindBatch();
            }
            txtFromDate.Text = txtentrydt.Text;



            txtbulkqty.Text = "";
            txtuseqty.Text = "";
            txtbalanceqty.Text = "";

            ClsProductionOrder oClsProductionOrder = new ClsProductionOrder();
            DataTable dtBalanceQty = new DataTable();
            txtbatch.Text = ddlbatch.SelectedValue.Trim();
            dtBalanceQty = oClsProductionOrder.ChkBulkBalanceQty("", ddlbatch.SelectedValue.ToString().Trim(), ddlframework.SelectedValue.ToString().Trim(), ddlprocess.SelectedValue.ToString().Trim(), 0, ddlType.SelectedValue.ToString());
            if (dtBalanceQty.Rows.Count > 0)
            {
                txtbulkqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["BULKQTY"].ToString());
                txtuseqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["USEQTY"].ToString());
                txtbalanceqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["BALANCEQTY"].ToString());
            }
            this.trUseQtyDtls.Visible = false;

            if (this.ddlProductionType.SelectedValue == "WP" && this.ddlType.SelectedValue == "FG")
            {
                loadPlanningQty();
            }





        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void loadPlannigNumber()
    {
        try
        {
            DataTable objDt = new DataTable();
            objDt = oClsProductionOrder.fetchPlanningNumber(this.ddlFG.SelectedValue.ToString());
            if (objDt.Rows.Count > 0)
            {
                this.ddlPlanningNumber.Items.Clear();
                this.ddlPlanningNumber.Items.Insert(0, new ListItem("SELECT NUMBER", "0"));
                this.ddlPlanningNumber.DataSource = objDt;
                this.ddlPlanningNumber.DataTextField = "PRODUCTIONNO";
                this.ddlPlanningNumber.DataValueField = "PRODUCTIONID";
                this.ddlPlanningNumber.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }


    protected void GenerateNo(string product, string finyr, string EntryDt)
    {
        try
        {
            ClsProductionOrder ProductionOrder = new ClsProductionOrder();
            DataTable DtBulkQty = new DataTable();
            string Batchno = ProductionOrder.GenerateBatchNo(product, finyr, EntryDt);
            string[] ret = Batchno.Split('~');
            //txtbatch.Text = ProductionOrder.GenerateBatchNo(product, finyr, EntryDt);
            txtbatch.Text = ret[0].ToString();
            txtpreviousBatch.Text = ret[1].ToString();
            DtBulkQty = ProductionOrder.BINDPREVIOUSBATCH_BULKQTY(ddlFG.SelectedValue.ToString(), txtpreviousBatch.Text);
            if (DtBulkQty.Rows.Count > 0)
            {
                txtBatchQty.Text = DtBulkQty.Rows[0]["QTYINKG"].ToString();
            }
            else
            {
                txtBatchQty.Text = "0.00";
            }
            txtbatch.Enabled = false;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAddnewRecord_Click(object sender, EventArgs e)
    {
        try
        {
            this.ddlProductionType.Enabled = true;
            this.ddlType.Enabled = true;
            btnSubmit.BackColor = Color.LimeGreen;
            btnSubmit.Text = "PROCESS START";
            this.Label11.Text = "PROCSSS START DATE";
            txtbulkqty.Text = "";
            txtuseqty.Text = "";
            txtbalanceqty.Text = "";
            this.ddlFG.Enabled = true;
            this.LoadMRP();
           // this.LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");
            this.trAutoPoNo.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.td_gridview.Style["display"] = "none";
            this.divcancelorder.Visible = false;
            this.Hdn_Fld.Value = "";
            this.gvItem.DataSource = null;
            this.gvItem.DataBind();
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
            this.Generate.Visible = true;
            this.gvPlannedOrder.DataSource = null;
            this.gvPlannedOrder.DataBind();
            this.Generate.Visible = true;
            this.txtStartTime.Text = "00:00:00";
            this.ddlframework.Items.Clear();
            this.ddlPlant.Items.Clear();
            this.ddlprocess.Items.Clear();
            this.ddlFG.Items.Clear();
            //this.ddlType.SelectedValue = "FG";
            this.tdbatch.Visible = false;
            this.tdddlbatch.Visible = false;
            this.ddlbatch.SelectedValue = "0";
            this.trReject.Visible = false;
            //this.DateLock();
            //this.rdbatch.SelectedValue = "1";
            if (Request.QueryString["MENUID"] != "1638")
            {
                Div_Submit.Visible = false;
            }
            else
            {
                Div_Submit.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private string BindFINISHEDGOOD()
    {
        string FGID = "";
        var query1 = from ListItem item in ddlFG.Items where item.Selected select item;
        foreach (ListItem item in query1)
        {
            // item ...
            FGID += item.Value + ',';
        }
        if (FGID.Length > 0)
        {
            FGID = FGID.Substring(0, FGID.Length - 1);
        }
        return FGID;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string fg = BindFINISHEDGOOD();
            ds = oClsProductionOrder.FetchMRPRecord(ddlMRP.SelectedValue.ToString(), fg.ToString(), ddlPlant.SelectedValue.ToString(), null, null, txtFromDate.Text, null, "S", ddlType.SelectedValue);
            if (ds.Tables[3].Rows.Count > 0)
            {
                gvPlannedOrder.DataSource = ds.Tables[3];
                gvPlannedOrder.DataBind();
            }
            else
            {
                gvPlannedOrder.DataSource = null;
                gvPlannedOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnGenerate_Click
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {

            if(this.ddlframework.SelectedValue=="0")
            {
                MessageBox1.ShowInfo("Please Select Framework");
                return;
            }
            if ((ddlProductionType.SelectedValue == "WP" && this.ddlType.SelectedValue == "FG" )&& this.ddlPlanningNumber.SelectedValue=="" || this.ddlPlanningNumber.SelectedValue == "0")
            {
                MessageBox1.ShowWarning("Please Select Planning Number");
                return;
            }

            this.grdStoreWiseStock.DataSource = null;
            this.grdStoreWiseStock.DataBind();
            this.td_gridview.Style["display"] = "";
            string strProcessFrameworkID = string.Empty;
            ClsProductionOrder oClsProductionOrder = new ClsProductionOrder();
            string fg = BindFINISHEDGOOD();

            ds = oClsProductionOrder.FetchRecordWithOutMrp(ddlprocess.SelectedValue.ToString(), fg, "", ddlPlant.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvItem.DataSource = ds.Tables[0];
                this.gvItem.DataBind();
            }
            else
            {
                this.gvItem.DataSource = null;
                this.gvItem.DataBind();
            }

            DataTable dt = new DataTable();
            dt = oClsProductionOrder.BindBOM(this.ddlprocess.SelectedValue.Trim(),
                                                Convert.ToDecimal(ds.Tables[0].Rows[0]["Consumption"].ToString().Trim()), fg,
                                                this.ddlframework.SelectedValue.Trim(), Hdn_Fld.Value.Trim());

            string USERNAME = HttpContext.Current.Session["FNAME"].ToString();

            if (dt.Rows[0]["response"].ToString() == "STORE NOT FOUND")
            {
                MessageBox1.ShowInfo("No Storelocation is Mapped With"+ ":" + USERNAME);
                return;
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvBOM.DataSource = dt;
                    this.gvBOM.DataBind();
                }
                else
                {
                    this.gvBOM.DataSource = null;
                    this.gvBOM.DataBind();
                }
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void gvWorkstaion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProcessID = (Label)e.Row.FindControl("lblProcessID");
            Label lblConsumption = (Label)e.Row.FindControl("lblConsumption");
            Label lblTimeInHour = (Label)e.Row.FindControl("lblTimeInHour");
            DropDownList ddlWorkStation = (DropDownList)e.Row.FindControl("ddlWorkStation");

            ds = oClsProductionOrder.FetchMRPRecord(ddlMRP.SelectedValue.ToString(), ddlFG.SelectedValue.ToString(), ddlPlant.SelectedValue.ToString(), Convert.ToString(ViewState["strProcessFrameworkID"]), lblProcessID.Text, txtFromDate.Text, txtToDate.Text, "S");

            lblConsumption.Text = ds.Tables[7].Rows[0][2].ToString();
            lblTimeInHour.Text = ds.Tables[7].Rows[0][3].ToString();
            if (ds.Tables[7].Rows.Count > 0)
            {
                ddlWorkStation.Items.Clear();
                ddlWorkStation.DataSource = ds.Tables[7];
                ddlWorkStation.DataTextField = "WorkStationName";

                ddlWorkStation.DataValueField = "WorkstationID";
                ddlWorkStation.DataBind();
                ddlWorkStation.Enabled = true;
            }
        }
    }

    protected void ddlWorkStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlWorkStation = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlWorkStation.Parent.Parent;
            DropDownList Drop_g = (DropDownList)sender;
            string WorkStationID = ddlWorkStation.SelectedValue.ToString();
            Label lblConsumption = (Label)ddlWorkStation.FindControl("lblConsumption");
            Label lblTimeInHour = (Label)ddlWorkStation.FindControl("lblTimeInHour");
            ds = oClsProductionOrder.FetchMRPRecord(null, null, null, null, WorkStationID, null, null, "S");
            if (ds.Tables[8].Rows.Count > 0)
            {
                lblConsumption.Text = ds.Tables[8].Rows[0][2].ToString();
                lblTimeInHour.Text = ds.Tables[8].Rows[0][3].ToString();
                hdfTabID.Value = "2";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.ddlProductionType.Enabled = false;
            this.ddlType.Enabled = false;
            string POOrder = oClsProductionOrder.ProductionOrder(Hdn_Fld.Value.Trim());
            if (POOrder == "1")
            {
                
                Div_Submit.Visible = false;
                Clear();
            }
            else
            {
                string mode = "PROCESSSTATUS";
                string status = oClsProductionOrder.StatusCheck(mode, Hdn_Fld.Value.Trim(), "", "");
                if (status == "1")
                {
                    //MessageBox1.ShowInfo("You Can Not Edit Because Process Complete of this Order:- <b><font color='green'>" + "abc" + "</font></b>.", 60, 600);
                    //MessageBox1.ShowInfo("You Can Not Edit Because Process Complete of this Order.", 60, 600);
                    Div_Submit.Visible = false;
                    Clear();
                }
                else
                {
                    Div_Submit.Visible = true;
                }
            }
            
          
            tdbatch.Visible = false;
            tdddlbatch.Visible = false;
            this.divcancelorder.Visible = true;
            this.trReject.Visible = true;
            this.Generate.Visible = false;
            this.td_gridview.Style["display"] = "";
            this.trAutoPoNo.Style["display"] = "";
            txtbulkqty.Text = "";
            txtuseqty.Text = "";
            txtbalanceqty.Text = "";
            DataSet dsfetch = new DataSet();
            dsfetch = oClsProductionOrder.EditProductionOrderWithoutMRP(Hdn_Fld.Value.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());
            if (dsfetch.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTION_START_DATE"].ToString().Trim();
                txtentrydt.Text = dsfetch.Tables["HEADER"].Rows[0]["ENTRY_DATE"].ToString().Trim();
                txtbatch.Text = dsfetch.Tables["HEADER"].Rows[0]["BATCHNO"].ToString().Trim();
                this.txtStartTime.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTION_START_TIME"].ToString().Trim();
                txtPoNo.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTIONORDER"].ToString().Trim();
                txtshowbatchno.Text = dsfetch.Tables["HEADER"].Rows[0]["BATCHNO"].ToString().Trim();
                txtRejectRemarks.Text = dsfetch.Tables["HEADER"].Rows[0]["REMARKS"].ToString().Trim();

                txtRejectRemarks.Text = dsfetch.Tables["HEADER"].Rows[0]["REMARKS"].ToString().Trim();
                /*Commented By Avishek Ghosh On 19-08-2017*/
                /*ddlMRP.Items.Clear();
                string Mrp =dsfetch.Tables["HEADER"].Rows[0]["MRP"].ToString().Trim();
                string MrpID = dsfetch.Tables["HEADER"].Rows[0]["MRPID"].ToString().Trim();
                ddlMRP.Items.Add(Mrp);*/
                /*=======================================*/

                this.ddlprocess.Items.Clear();
                this.ddlprocess.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ProcessName"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ProcessID"]).Trim()));
                this.ddlprocess.AppendDataBoundItems = true;

                this.ddlPlant.Items.Clear();
                this.ddlPlant.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["WorkStationName"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["WorkstationID"]).Trim()));
                this.ddlPlant.AppendDataBoundItems = true;

                this.ddlframework.Items.Clear();
                this.ddlframework.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FRAMEWORKNAME"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FRAMEWORKID"]).Trim()));
                this.ddlframework.AppendDataBoundItems = true;

                string TypeValue = dsfetch.Tables["HEADER"].Rows[0]["TYPEVALUE"].ToString().Trim().ToUpper();
                if (TypeValue == "SEMI FINISHED GOODS")
                {
                    ddlType.SelectedValue = "SFG";
                    Label2.Text = "SEMI FINISHED GOODS";
                    rdbatch.SelectedValue = "0";
                }
                else
                {
                    this.ddlType.SelectedValue = "FG";
                    this.Label2.Text = "FINISHED GOODS";
                    rdbatch.SelectedValue = "1";

                    DataTable dtBalanceQtyEdit = new DataTable();
                    dtBalanceQtyEdit = oClsProductionOrder.ChkBulkBalanceQty("", txtshowbatchno.Text, Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FRAMEWORKID"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ProcessID"]).Trim(), 0, ddlType.SelectedValue.ToString());
                    if (dtBalanceQtyEdit.Rows.Count > 0)
                    {
                        txtbulkqty.Text = Convert.ToString(dtBalanceQtyEdit.Rows[0]["BULKQTY"].ToString());
                        txtuseqty.Text = Convert.ToString(dtBalanceQtyEdit.Rows[0]["USEQTY"].ToString());
                        txtbalanceqty.Text = Convert.ToString(dtBalanceQtyEdit.Rows[0]["BALANCEQTY"].ToString());
                    }
                }
                string PRODUCTIONTPE= Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["PRODUCTIONTYPE"]).Trim();
                if(PRODUCTIONTPE=="WP")
                {
                    this.ddlProductionType.SelectedValue = "WP";
                }
                else
                {
                    this.ddlProductionType.SelectedValue = "WOP";
                }

                if(this.ddlProductionType.SelectedValue == "WP" && ddlType.SelectedValue== "FG")
                {
                    this.divPlanningNummer.Visible = true;
                    this.divProductionQty.Visible = true;

                    this.loadPlannigNumber();
                    this.ddlPlanningNumber.SelectedValue = Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["PLANNINGID"]).Trim();
                    this.ddlPlanningNumber.Enabled = false;
                }
                else
                {
                    this.divPlanningNummer.Visible = false;
                    this.divProductionQty.Visible = false;
                  
                    
                }


                //rdbatch.SelectedIndex = 0;
                this.ddlFG.Items.Clear();
                this.ddlFG.DataSource = dsfetch.Tables[2];
                this.ddlFG.DataTextField = "NAME";
                this.ddlFG.DataValueField = "PRODUCTID";
                this.ddlFG.DataBind();
                this.ddlFG.AppendDataBoundItems = true;
                
                // this.LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "");
                this.txtProductionQty.Text = Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ORDERQTY"]);

                var myList = new List<string>(dsfetch.Tables["HEADER"].Rows[0]["FGNAME"].ToString().Trim().Split(','));

                if (dsfetch.Tables[0].Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlFG.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlFG.Items[innercounter].Text)
                            {
                                ddlFG.Items[innercounter].Selected = true;
                            }
                        }
                    }
                }

                if (dsfetch.Tables[1].Rows.Count > 0)
                {
                    this.gvPlannedOrder.DataSource = dsfetch.Tables[1];
                    this.gvPlannedOrder.DataBind();
                }
                else
                {
                    this.gvPlannedOrder.DataSource = null;
                    this.gvPlannedOrder.DataBind();
                }

                if (dsfetch.Tables["HEADER"].Rows[0]["ISCANCELLED"].ToString().Trim() == "N")
                {
                    this.chkCancelled.Checked = false;
                }
                else
                {
                    this.chkCancelled.Checked = true;
                }

                this.ddlfactory.Items.Clear();
                this.ddlfactory.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["VENDORNAME"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FACTORYID"]).Trim()));
                this.ddlfactory.AppendDataBoundItems = true;


                ds = oClsProductionOrder.FetchWITHOUTMRPRecord(null, "", HttpContext.Current.Session["DEPOTID"].ToString(), null, null, null, null, "edit", this.ddlType.SelectedValue.Trim());
                this.ddlFG.DataSource = ds.Tables[1];
                this.ddlFG.DataTextField = "NAME";
                this.ddlFG.DataValueField = "PRODUCTID";
                this.ddlFG.DataBind();
                this.ddlFG.Enabled = false;


                if (dsfetch.Tables[3].Rows.Count > 0)
                {
                    this.gvItem.DataSource = dsfetch.Tables[3];
                    this.gvItem.DataBind();
                    
                }
                else
                {
                    this.gvItem.DataSource = null;
                    this.gvItem.DataBind();
                }
                if (dsfetch.Tables[4].Rows.Count > 0)
                {
                    this.gvBOM.DataSource = dsfetch.Tables[4];
                    this.gvBOM.DataBind();
                }
                else
                {
                    this.gvBOM.DataSource = null;
                    this.gvBOM.DataBind();
                }

                this.pnlAdd.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.btnaddhide.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            decimal globalqty = 0;
            string xmlProdOrderBOM = string.Empty;
            string xmlREQUISITION = string.Empty;
            string IsCancelled = string.Empty;
            string Flag = string.Empty;
            dtProdOrderBOM = CreateDataTableForProductionOrderBOM();
            dtREQUISITION = CreateDataTableForRequisition();
            foreach (GridViewRow row in gvBOM.Rows)
            {
                Label lblCATID = (Label)row.FindControl("lblCATID");
                Label lblCATNAME = (Label)row.FindControl("lblCATNAME");
                Label lblID = (Label)row.FindControl("lblID");
                Label lblName = (Label)row.FindControl("lblName");
                Label lblUOMID = (Label)row.FindControl("lblUOMID");
                Label lblUOMNAME = (Label)row.FindControl("lblUOMNAME");
                TextBox lblQty = (TextBox)row.FindControl("lblQty");
                TextBox txtBufferQty = (TextBox)row.FindControl("txtBufferQty");
                TextBox txtNetQty = (TextBox)row.FindControl("txtNetQty");
                string Product = ((Label)row.FindControl("lblName")).Text.Trim();

                /*NEW ADD FOR AUTO REQUISTION*/
                string SourceLocationId = ((Label)row.FindControl("lblSourceLocationId")).Text.Trim();
                string MappedLocationId = ((Label)row.FindControl("lblMappedLocationId")).Text.Trim();
                TextBox lblRequisitionQty = (TextBox)row.FindControl("lblRequisitionQty");
                Label lblSendDepartMent = (Label)row.FindControl("lblSendDepartMent");
                Label lblSendDepartMentName = (Label)row.FindControl("lblSendDepartMentName");
              


                decimal STOCKQTY = Convert.ToDecimal(((Label)row.FindControl("lblStockQTY")).Text);
                decimal netQty = Convert.ToDecimal(txtNetQty.Text);
                if(STOCKQTY >= netQty)
                {
                    TextBox t = (TextBox)row.FindControl("lblRequisitionQty");
                    Label t1 = (Label)row.FindControl("lblApplicable");
                    t.Text = "0";
                    t1.Text = "N";
                }
                else
                {
                    string sendDepartMent = lblSendDepartMent.Text;
                    if (sendDepartMent == "0" || sendDepartMent == "")
                    {
                        MessageBox1.ShowWarning("Select department:- <font color='red'>" + Product + "</font>", 50, 400);
                        return;
                    }
                }
                

                string lblApplicable =Convert.ToString(((Label)row.FindControl("lblApplicable")).Text);
                decimal NetQty = Convert.ToDecimal(((TextBox)row.FindControl("txtNetQty")).Text);
                decimal RequisitionQty = Convert.ToDecimal(((TextBox)row.FindControl("lblRequisitionQty")).Text);

                //if((SourceLocationId== MappedLocationId) && (NetQty > STOCKQTY))
                //{
                //    MessageBox1.ShowInfo("Stock Not Available for:- <font color='red'>" + Product + "</font>", 50, 400);
                //    return;
                //}
                    dr = dtProdOrderBOM.NewRow();
                    dr[0] = lblCATID.Text;
                    dr[1] = lblCATNAME.Text;
                    dr[2] = lblID.Text;
                    dr[3] = lblName.Text;
                    dr[4] = lblUOMID.Text;
                    dr[5] = lblUOMNAME.Text;
                    dr[6] = lblQty.Text;
                    dr[7] = txtBufferQty.Text;
                    dr[8] = txtNetQty.Text;
                    dr[9] = SourceLocationId;
                    dr[10] = MappedLocationId;
                    dr[11] = RequisitionQty;
                    dr[12] = lblSendDepartMent.Text;
                    dr[13] = lblSendDepartMentName.Text;
                    dr[14] = lblApplicable;
                    dtProdOrderBOM.Rows.Add(dr);
                
            }
            xmlProdOrderBOM = ConvertDatatableToXML(dtProdOrderBOM);

            foreach (GridViewRow row in gvItem.Rows)
            {
               
                Label lblID = (Label)row.FindControl("lblItemID");
                Label lblName = (Label)row.FindControl("lblItem");
                Label lblUOMID = (Label)row.FindControl("lblUnitID");
                Label lblUOMNAME = (Label)row.FindControl("lblUnit");
                Label lblMRPQty = (Label)row.FindControl("lblMRPQTY");
                TextBox textQty = (TextBox)row.FindControl("TXTQTY");
                TextBox txtQtyinKg = (TextBox)row.FindControl("txtQtyinKg");
                Label lblSpecificGravity = (Label)row.FindControl("lblSpecificGravity");


                globalqty = Convert.ToDecimal(textQty.Text);
                if(globalqty == 0 )
                {
                    MessageBox1.ShowWarning("Qty cannont Be blank Please Check");
                    dtProdOrderBOM.Clear();
                    return;
                }

                dr = dtREQUISITION.NewRow();
                dr[0] = lblID.Text;
                dr[1] = lblName.Text;
                dr[2] = lblUOMID.Text;
                dr[3] = lblUOMNAME.Text;
                dr[4] = String.Format("{0:#0.000}", lblMRPQty.Text);
                dr[5] = String.Format("{0:#0.000}", textQty.Text);
                dr[6] = String.Format("{0:#0.000}", txtQtyinKg.Text);
                dr[7] = lblSpecificGravity.Text;
                dtREQUISITION.Rows.Add(dr);
            }

            xmlREQUISITION = ConvertDatatableToXML(dtREQUISITION);
            string FG = BindFINISHEDGOOD();
            string PRONo = string.Empty;

            if (chkCancelled.Checked == true)
            {
                IsCancelled = "Y";
            }
            else
            {
                IsCancelled = "N";
            }

            if (Hdn_Fld.Value.Trim() == "")
            {
                Flag = "I";
            }
            else
            {
                Flag = "U";
            }

            int GridCount = gvBOM.Rows.Count;
            string ProductionOrder = oClsProductionOrder.ProductionOrderID(this.Hdn_Fld.Value.Trim());
            if (GridCount <= 0)
            {
                MessageBox1.ShowInfo("Please Generate Bom Then Save");
            }
            else
            {
                PRONo = oClsProductionOrder.InsertUpdateDeleteProductionOrder(Hdn_Fld.Value.Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), null, null, txtFromDate.Text.Trim(), null,
                                                                                HttpContext.Current.Session["UserID"].ToString().Trim(), HttpContext.Current.Session["UserID"].ToString().Trim(),
                                                                                txtbatch.Text.Trim(), FG, ddlPlant.SelectedValue.ToString().Trim(),
                                                                                "0", xmlProdOrderBOM, xmlREQUISITION, Flag,
                                                                                "", ddlprocess.SelectedValue.Trim(), ddlfactory.SelectedValue.Trim(),
                                                                                ddlframework.SelectedValue.Trim(), IsCancelled, txtentrydt.Text, "",this.ddlPlanningNumber.SelectedValue, globalqty);
              
                if (PRONo.Trim() != "")
                {
                    MessageBox1.ShowSuccess("Production Order:  <b><font color='green'>" + PRONo + "</font></b>  saved successfully", 80, 550);
                }
                else
                {
                    MessageBox1.ShowError("<b><font color=red>Error on Saving record!</font></b>");
                }
                this.BindGrid();
                Clear();
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "none";
            this.trAutoPoNo.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.btnaddhide.Style["display"] = "";
            this.td_gridview.Style["display"] = "none";
            this.divcancelorder.Visible = false;
            this.Hdn_Fld.Value = "";
            this.gvItem.DataSource = null;
            this.gvItem.DataBind();
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
            this.Generate.Visible = true;
            Clear();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    #endregion

    public DataTable CreateDataTableForProcessFramework()
    {
        DataTable dtProcessFramework = new DataTable();
        dtProcessFramework.Clear();
        dtProcessFramework.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtProcessFramework.Columns.Add(new DataColumn("ProcessCode", typeof(String)));
        dtProcessFramework.Columns.Add(new DataColumn("ProcessName", typeof(String)));
        dtProcessFramework.Columns.Add(new DataColumn("ProcessTotalDuration", typeof(String)));
        HttpContext.Current.Session["ProcessFramework"] = dtProcessFramework;
        return dtProcessFramework;
    }

    public DataTable CreateDataTableForGenerateSchedule()
    {
        DataTable dtGenerateSchedule = new DataTable();
        dtGenerateSchedule.Clear();
        dtGenerateSchedule.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProcessName", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProcessWorkstationID", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProcessWorkstationName", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("Capacity", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("TimeInHour", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProductID", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProductName", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ProductQTY", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("UnitID", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("UnitName", typeof(String)));
        dtGenerateSchedule.Columns.Add(new DataColumn("ActualTimeInHour", typeof(String)));
        return dtGenerateSchedule;
    }

    public void LoadMRP()
    {
        try
        {
            this.ddlMRP.Items.Clear();
            ds = oClsProductionOrder.FetchMRPRecord(null, null, null, null, null, null, null, "S");
            this.ddlMRP.DataSource = ds.Tables[0];
            this.ddlMRP.DataTextField = "MRP_NO";
            this.ddlMRP.DataValueField = "MRP_ID";
            this.ddlMRP.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    public void LoadFGBasisOnMRP(string strMRPID, string Product, string Flag)
    {
        try
        {
            BAL.ClsCommonFunction ClsCommon = new BAL.ClsCommonFunction();

           
                string userid = HttpContext.Current.Session["USERID"].ToString();
                ds = oClsProductionOrder.FetchWITHOUTMRPRecord(this.ddlPlanningNumber.SelectedValue, Product, HttpContext.Current.Session["DEPOTID"].ToString(), this.ddlProductionType.SelectedValue, null, null, userid, Flag, this.ddlType.SelectedValue.Trim());
            
            

            //ds = oClsProductionOrder.FetchMRPRecord(strMRPID, Product, null, null, null, null, null, Flag, ddlType.SelectedValue);


                this.ddlFG.Items.Clear();
                this.ddlFG.Items.Insert(0, new ListItem("-Select FG/SFG-", "0"));
                this.ddlFG.DataSource = ds.Tables[1];
                this.ddlFG.DataBind();
                this.ddlFG.DataTextField = "NAME";
                this.ddlFG.DataValueField = "PRODUCTID";

            foreach (ListItem li in ddlFG.Items)
            {

                li.Text = Convert.ToString(li.Text);
                var x = li.Text;
                x = x.Substring(0,3);
                if (x != "(R)")
                {
                    li.Attributes.Add("style", "color:red;");
                }
                else
                {
                    li.Attributes.Add("style", "color:green;");
                }
                    
            }




            this.ddlfactory.Items.Clear();
            dt = ClsCommon.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
            this.ddlfactory.DataSource = dt;//ds.Tables[0];
            this.ddlfactory.DataTextField = "BRNAME";
            this.ddlfactory.DataValueField = "BRID";
            this.ddlfactory.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }


    //private iTextSharp.text.List finalColorList()
    //{
    //    string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
    //    string[] systemEnvironmentColors =
    //        new string[(
    //        typeof(System.Drawing.SystemColors)).GetProperties().Length];

    //    int index = 0;

    //    foreach (MemberInfo member in (
    //        typeof(System.Drawing.SystemColors)).GetProperties())
    //    {
    //        systemEnvironmentColors[index++] = member.Name;
    //    }

    //    iTextSharp.text.List finalColorList = new iTextSharp.text.List();

    //    foreach (string color in allColors)
    //    {
    //        if (Array.IndexOf(systemEnvironmentColors, color) < 0)
    //        {
    //            finalColorList.Add(color);
    //        }
    //    }
    //    return finalColorList;
    //}

    //private void colorManipulation()
    //{
    //    int row;
    //    for (row = 0; row < ddlFG.Items.Count - 1; row++)
    //    {
    //        ddlFG.Items[row].Attributes.Add("style",
    //            "background-color:" + ddlFG.Items[row].Value);
    //    }
    //    ddlFG.BackColor =
    //        Color.FromName(ddlFG.SelectedItem.Text);
    //}


    public void LoadPlantBasisOnMRPFG(string strMRPID, string strProduct, string Flag)
    {
        try
        {
            
            DataSet dsframework = new DataSet();
            string itemId = this.ddlFG.SelectedValue.ToString();
            string itemName = this.ddlFG.SelectedItem.ToString();
            string mode = "STATUS";
            ds = oClsProductionOrder.FetchWorkStation(mode, itemId);
            string status = ds.Tables[0].Rows[0]["NAME"].ToString();
            if (status=="START")
            {
                MessageBox1.ShowWarning("Production Start for"+":"+ itemName);
                ddlframework.Items.Clear();
                this.ddlframework.SelectedValue = "0";
                return;
            }
            else if (status=="WIP")
            {
                MessageBox1.ShowWarning("Production Already Start for"+":"+ itemName);
                ddlframework.Items.Clear();
                this.ddlframework.SelectedValue = "0";
                return;
            }

            string FG = BindFINISHEDGOOD();
            ddlPlant.Items.Clear();
            ddlPlant.Items.Insert(0, new ListItem("-Select Plant-", "0"));
            ds = oClsProductionOrder.FetchWorkStation(FG);
            /*Bind Framework*/
            ddlframework.Items.Clear();
            ddlframework.Items.Insert(0, new ListItem("-Select Framework-", "0"));
            dsframework = oClsProductionOrder.FetchFrameWork(FG);
            ddlframework.DataSource = ds.Tables[1];
            ddlframework.DataTextField = "FRAMEWORKNAME";
            ddlframework.DataValueField = "FRAMEWORKID";
            ddlframework.DataBind();

           

            ddlprocess.Items.Clear();
            ddlprocess.DataSource = ds.Tables[0];
            ddlprocess.DataTextField = "ProcessName";
            ddlprocess.DataValueField = "ProcessID";
            ddlprocess.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    public void LoadProcessFrameworkGrid(string ProcessFrameworkID)
    {
        try
        {
            //ds = oProcessFramework.FetchProcessFrameworkByID(ProcessFrameworkID);

            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    gvProcessFramework.DataSource = ds.Tables[1];
            //    gvProcessFramework.DataBind();
            //}

            //else
            //{
            //    gvProcessFramework.DataSource = null;
            //    gvProcessFramework.DataBind();
            //}
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    public DataTable CreateDataTableForProductionOrderBOM()
    {
        DataTable dtProductionOrderBOM = new DataTable();
        dtProductionOrderBOM.Clear();
        dtProductionOrderBOM.Columns.Add(new DataColumn("CATID", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("CATNAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("ID", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("BUFFERQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("NETQTY", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("SOURCELOCATION", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("MAPPEDLOCATION", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("REQUISITIONQTY", typeof(decimal)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("TODEPARTMENT", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("TODEPARTMENTNAME", typeof(String)));
        dtProductionOrderBOM.Columns.Add(new DataColumn("APPLICABLE", typeof(String)));
        return dtProductionOrderBOM;
    }
    public DataTable CreateDataTableForRequisition()
    {
        DataTable dtRequisition = new DataTable();
        dtRequisition.Clear();

        dtRequisition.Columns.Add(new DataColumn("MATERIALID", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("MRPQTY", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("QTY", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("QTYINKG", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("SPECIFICGRAVITY", typeof(String)));
        return dtRequisition;
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
    protected void BindBatch()
    {
        //DataTable dt = oClsProductionOrder.BindBatch(ddlMRP.SelectedValue);
        this.ddlbatch.Items.Clear();
        DataTable dt = oClsProductionOrder.BindBatchWithOutMRP(ddlFG.SelectedValue.ToString().Trim(), ddlfactory.SelectedValue.ToString());
        ddlbatch.DataSource = dt;
        this.ddlbatch.Items.Insert(0, new ListItem("-Select FG-", "0"));
        ddlbatch.DataTextField = "BatchNO";
        ddlbatch.DataValueField = "BatchNO";
        ddlbatch.DataBind();
        txtbatch.Text = ddlbatch.SelectedValue;
    }

    protected void rdbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (Convert.ToInt16(rdbatch.SelectedValue))
        {
            case 1:
                lblbatch.Visible = true;
                ddlbatch.Visible = true;
                txtbatch.Visible = false;
                tdbatch.Visible = true;
                tdddlbatch.Visible = true;
                trBatchDtls.Visible = false;
                txtbatch.Text = "";
                txtpreviousBatch.Text = "";
                txtuseqty.Text = "";
                ddlFG.SelectedValue = "0";
                ddlbatch.SelectedValue = "0";
                break;
            case 0:
                lblbatch.Visible = false;
                ddlbatch.Visible = false;
                txtbatch.Visible = true;
                trBatchDtls.Visible = true;
                tdddlbatch.Visible = false;
                txtbulkqty.Text = "";
                txtuseqty.Text = "";
                txtbalanceqty.Text = "";
                ddlFG.SelectedValue = "0";
                txtbatch.Text = "";
                trUseQtyDtls.Visible = false;
                break;
        }
    }

    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbulkqty.Text = "";
        txtuseqty.Text = "";
        txtbalanceqty.Text = "";

        ClsProductionOrder oClsProductionOrder = new ClsProductionOrder();
        DataTable dtBalanceQty = new DataTable();
        txtbatch.Text = ddlbatch.SelectedValue.Trim();
        dtBalanceQty = oClsProductionOrder.ChkBulkBalanceQty("", ddlbatch.SelectedValue.ToString().Trim(), ddlframework.SelectedValue.ToString().Trim(), ddlprocess.SelectedValue.ToString().Trim(), 0, ddlType.SelectedValue.ToString());
        if (dtBalanceQty.Rows.Count > 0)
        {
            txtbulkqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["BULKQTY"].ToString());
            txtuseqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["USEQTY"].ToString());
            txtbalanceqty.Text = Convert.ToString(dtBalanceQty.Rows[0]["BALANCEQTY"].ToString());
        }
        this.trUseQtyDtls.Visible = false;
    }

    #region gvBOM_RowDataBound
    protected void gvBOM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string fg = BindFINISHEDGOOD();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStockQTY = (Label)e.Row.FindControl("lblStockQTY");
                Label lblID = (Label)e.Row.FindControl("lblID");
                decimal QTY = Convert.ToDecimal(((TextBox)e.Row.FindControl("lblQTY")).Text);
                decimal NetQty = Convert.ToDecimal(((TextBox)e.Row.FindControl("txtNetQty")).Text);
                decimal STOCKQTY = Convert.ToDecimal(((Label)e.Row.FindControl("lblStockQTY")).Text);
                decimal AlreadyRequQty = Convert.ToDecimal(((Label)e.Row.FindControl("lblAlreadyRequQty")).Text);
                string sendRequest = Convert.ToString(((Label)e.Row.FindControl("lblApplicable")).Text);
             
               
                if (NetQty > STOCKQTY)
                {
                    lblStockQTY.ForeColor = Color.Red;
                }

                //DropDownList ddlSendDepartMent = e.Row.FindControl("ddlSendDepartMent") as DropDownList;


                //if (Hdn_Fld.Value.Trim() == "")
                //{
                    //ddlSendDepartMent.Items.Clear();
                    //ddlSendDepartMent.Items.Insert(0, new ListItem("Please Select", "0"));
                    //ddlSendDepartMent.AppendDataBoundItems = true;
                    //ddlSendDepartMent.DataSource = oClsProductionOrder.BindDepartMent(ddlprocess.SelectedValue, 0, fg, ddlframework.SelectedValue.Trim(), this.ddlfactory.SelectedValue.Trim());
                    //ddlSendDepartMent.DataValueField = "DEPTID";
                    //ddlSendDepartMent.DataTextField = "DEPTNAME";
                    //ddlSendDepartMent.DataBind();


                    string toLocation = ((Label)e.Row.FindControl("lblMappedLocation")).Text;
                    string SourceLocationId = ((Label)e.Row.FindControl("lblSourceLocationId")).Text;
                    string MappedLocationId = ((Label)e.Row.FindControl("lblMappedLocationId")).Text;

                    //if (toLocation == "")
                    //{
                    //    ddlSendDepartMent.SelectedValue = "0";
                    //    ddlSendDepartMent.Enabled = false;
                    //}


                    if (SourceLocationId != MappedLocationId)
                    {
                        if (STOCKQTY < NetQty)
                        {
                            decimal _value = 0;
                            if (AlreadyRequQty > 0)
                            {
                                _value = (STOCKQTY - AlreadyRequQty);
                            }
                            else
                            {
                                _value = (STOCKQTY);
                            }
                            if (_value < NetQty)
                            {
                                Label t = (Label)e.Row.FindControl("lblApplicable");
                                TextBox t1 = (TextBox)e.Row.FindControl("lblRequisitionQty");
                                t.Text = "Y";
                                t1.Text = Convert.ToString((NetQty - (STOCKQTY - AlreadyRequQty)));
                            }
                            else if (_value > NetQty)
                            {
                                Label t = (Label)e.Row.FindControl("lblApplicable");
                                TextBox t1 = (TextBox)e.Row.FindControl("lblRequisitionQty");
                                t.Text = "Y";
                                t1.Text = Convert.ToString(((STOCKQTY) - NetQty));
                            }
                            else
                            {
                                Label t = (Label)e.Row.FindControl("lblApplicable");
                                t.Text = "N";
                            }
                        }
                        
                    }

                    if(sendRequest=="N")
                    {
                        TextBox requisitionQty = (TextBox)e.Row.FindControl("lblRequisitionQty");
                         requisitionQty.Text = "0";
                    }



               // }
                //else
                //{
                //    //string mode = "edit";
                //    //string mode1 = "SingleId";
                //    //DataTable dt = oClsProductionOrder.BindDataModeWise(mode,Hdn_Fld.Value.Trim(),"");
                //   /// DataTable dt1 = oClsProductionOrder.BindDataModeWise(mode1, Hdn_Fld.Value.Trim(), lblID.Text);
                //    //ddlSendDepartMent.DataTextField = "DEPTNAME";
                //    //ddlSendDepartMent.DataValueField = "DEPTID";
                //    //ddlSendDepartMent.DataSource = dt;
                //    //ddlSendDepartMent.DataBind();
                //    //DataRowView dr = e.Row.DataItem as DataRowView;
                //    //ddlSendDepartMent.SelectedValue = dr["DEPTID"].ToString();



                //    //Find the DropDownList in the Row.
                //    //DropDownList ddlSendDepartMent1 = (e.Row.FindControl("ddlSendDepartMent") as DropDownList);
                //    //ddlSendDepartMent1.DataSource = dt;
                //    //ddlSendDepartMent1.DataTextField = "DEPTNAME";
                //    //ddlSendDepartMent1.DataValueField = "DEPTID";
                //    //ddlSendDepartMent.DataBind();

                //    //////Add Default Item in the DropDownList.
                //    ////ddlCountries.Items.Insert(0, new ListItem("Please select"));

                //    ////Select the Country of Customer in DropDownList.
                //    //string lblSendDepartMent = (e.Row.FindControl("lblSendDepartMent") as Label).Text;
                //    //ddlSendDepartMent1.Items.FindByValue(lblSendDepartMent).Selected = true;


                //    //ddlSendDepartMent.Enabled = false;
                //}

                
               


            }
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    #endregion

    public void Clear()
    {
        this.chkCancelled.Checked = false;
        rdbatch.ClearSelection();
        txtbatch.Text = "";
        this.txtProductionQty.Text = "0";
        ddlType.SelectedValue = "0";
        
        ddlProductionType.SelectedValue = "0";
        ddlPlanningNumber.Items.Clear();
        txtpreviousBatch.Text = "";
        txtBatchQty.Text = "";
        trBatchDtls.Visible = false;
        trUseQtyDtls.Visible = false;
    }

    #region Search Production
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region
    public void BindProduct()
    {
        try
        {
            DataTable dtProduct = new DataTable();
            this.ddlproduct.Items.Clear();
            this.ddlproduct.Items.Insert(0, new ListItem("All", "0"));
            dtProduct = oClsProductionOrder.BindProductName(HttpContext.Current.Session["DEPOTID"].ToString());
            this.ddlproduct.DataSource = dtProduct;
            this.ddlproduct.DataTextField = "PRODUCTALIAS";
            this.ddlproduct.DataValueField = "ID";
            this.ddlproduct.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void txtQtyinKg_OnTextChanged(object sender, EventArgs e)
    {
        decimal BulkQty = 0, QtyKg = 0, QtyPcs, PerfumeBulkQty = 0;
        string BoxValue = "", PcsValue = "";
        TextBox txtQtyInPcs = new TextBox();
        if (ddlType.SelectedValue == "FG")
        {
            decimal ConversionQty = 0, UnitValue = 0, SpecificGravity = 0;
            string fg = BindFINISHEDGOOD();
            foreach (GridViewRow gvRow in gvItem.Rows)
            {
                Label lblConversionQty = (Label)gvRow.FindControl("lblConversionQty");
                ConversionQty = Convert.ToDecimal(lblConversionQty.Text);

                Label lblUnitValue = (Label)gvRow.FindControl("lblUnitValue");
                UnitValue = Convert.ToDecimal(lblUnitValue.Text);

                Label lblSpecificGravity = (Label)gvRow.FindControl("lblSpecificGravity");
                SpecificGravity = Convert.ToDecimal(lblSpecificGravity.Text);
            }
            TextBox QtyInKg = (TextBox)sender;
            TextBox txtQtyInKg = (TextBox)QtyInKg.FindControl("txtQtyinKg");
            QtyKg = Convert.ToDecimal(txtQtyInKg.Text);
            if (txtQtyInKg.Text.Contains("."))
            {
                string[] commandArgs = Convert.ToString(QtyKg).ToString().Split(new char[] { '.' });
                BoxValue = commandArgs[0];
                PcsValue = commandArgs[1];
            }
            else
            {
                BoxValue = Convert.ToString(QtyKg);
                PcsValue = "0";
            }
            TextBox QtyInPcs = (TextBox)sender;
            txtQtyInPcs = (TextBox)QtyInPcs.FindControl("TXTQTY");
            QtyPcs = Convert.ToDecimal(txtQtyInPcs.Text);
            PerfumeBulkQty = ((QtyKg * ConversionQty * UnitValue) / 1000);
            BulkQty = (PerfumeBulkQty * Convert.ToDecimal(SpecificGravity));

            if(txtbalanceqty.Text.ToString()=="")
            {
                txtbalanceqty.Text ="0";
            }

            if (BulkQty <= Convert.ToDecimal(txtbalanceqty.Text))
            {
                //txtQtyInPcs.Text = Convert.ToString(ConversionQty * QtyKg);
                txtQtyInPcs.Text = Convert.ToString(ConversionQty * Convert.ToDecimal(BoxValue) + Convert.ToDecimal(PcsValue));
                dt = oClsProductionOrder.BindBOM(ddlprocess.SelectedValue, Convert.ToDecimal(txtQtyInPcs.Text), fg, ddlframework.SelectedValue.Trim(), Hdn_Fld.Value.Trim());
                if (dt.Rows.Count > 0)
                {
                    this.gvBOM.DataSource = dt;
                    this.gvBOM.DataBind();
                }
        }
        else
        {
            MessageBox1.ShowInfo("Your Remaing Qty is <font color='red'>" + "" + " (" + txtbalanceqty.Text + ")." + "</font>" + "Please Give <= from Remaing Qty. ", 60, 500);
            this.gvItem.DataSource = null;
            this.gvItem.DataBind();
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
        }
    }
        else
        {
            string fg = BindFINISHEDGOOD();
            TextBox QtyInKg = (TextBox)sender;
            TextBox txtQtyInKg = (TextBox)QtyInKg.FindControl("txtQtyinKg");
            QtyKg = Convert.ToDecimal(txtQtyInKg.Text);

            TextBox QtyInPcs = (TextBox)sender;
            txtQtyInPcs = (TextBox)QtyInPcs.FindControl("TXTQTY");
            txtQtyInPcs.Text = Convert.ToString(QtyKg);
            dt = oClsProductionOrder.BindBOM(ddlprocess.SelectedValue, Convert.ToDecimal(txtQtyInPcs.Text), fg, ddlframework.SelectedValue.Trim(), Hdn_Fld.Value.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvBOM.DataSource = dt;
                this.gvBOM.DataBind();
            }
        }
    }

    #region GridEdit
    public void GridEdit(string ProductionID)
    {
        try
        {
            Hdn_Fld.Value = ProductionID;
            string POOrder = oClsProductionOrder.ProductionOrder(Hdn_Fld.Value.Trim());
            if (POOrder == "1")
            {
                //MessageBox1.ShowInfo("You Can Not Edit Because Process Complete of this Order:- <b><font color='green'>" + "abc" + "</font></b>.", 60, 600);
                //MessageBox1.ShowInfo("You Can Not Edit Because Process Complete of this Order.", 60, 600);
                Div_Submit.Visible = false;
                Clear();
            }
            else
            {
                Div_Submit.Visible = true;
            }
            tdbatch.Visible = false;
            tdddlbatch.Visible = false;
            this.divcancelorder.Visible = true;
            this.Generate.Visible = false;
            this.td_gridview.Style["display"] = "";
            this.trAutoPoNo.Style["display"] = "";
            DataSet dsfetch = new DataSet();
            dsfetch = oClsProductionOrder.EditProductionOrderWithoutMRP(Hdn_Fld.Value.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());
            if (dsfetch.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTION_START_DATE"].ToString().Trim();
                txtentrydt.Text = dsfetch.Tables["HEADER"].Rows[0]["ENTRY_DATE"].ToString().Trim();
                txtbatch.Text = dsfetch.Tables["HEADER"].Rows[0]["BATCHNO"].ToString().Trim();
                this.txtStartTime.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTION_START_TIME"].ToString().Trim();
                txtPoNo.Text = dsfetch.Tables["HEADER"].Rows[0]["PRODUCTIONORDER"].ToString().Trim();
                txtshowbatchno.Text = dsfetch.Tables["HEADER"].Rows[0]["BATCHNO"].ToString().Trim();
                /*Commented By Avishek Ghosh On 19-08-2017*/
                /*ddlMRP.Items.Clear();
                string Mrp =dsfetch.Tables["HEADER"].Rows[0]["MRP"].ToString().Trim();
                string MrpID = dsfetch.Tables["HEADER"].Rows[0]["MRPID"].ToString().Trim();
                ddlMRP.Items.Add(Mrp);*/
                /*=======================================*/

                this.ddlprocess.Items.Clear();
                this.ddlprocess.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ProcessName"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["ProcessID"]).Trim()));
                this.ddlprocess.AppendDataBoundItems = true;

                this.ddlPlant.Items.Clear();
                this.ddlPlant.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["WorkStationName"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["WorkstationID"]).Trim()));
                this.ddlPlant.AppendDataBoundItems = true;

                this.ddlframework.Items.Clear();
                this.ddlframework.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FRAMEWORKNAME"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FRAMEWORKID"]).Trim()));
                this.ddlframework.AppendDataBoundItems = true;

                string TypeValue = dsfetch.Tables["HEADER"].Rows[0]["TYPEVALUE"].ToString().Trim();
                if (TypeValue == "SEMI FINISHED GOODS")
                {
                    ddlType.SelectedValue = "SFG";
                    Label2.Text = "SEMI FINISHED GOODS";
                }
                else
                {
                    this.ddlType.SelectedValue = "FG";
                    this.Label2.Text = "FINISHED GOODS";
                }
                rdbatch.SelectedIndex = 0;
                this.ddlFG.Items.Clear();
                this.ddlFG.DataSource = dsfetch.Tables[2];
                this.ddlFG.DataTextField = "NAME";
                this.ddlFG.DataValueField = "PRODUCTID";
                this.ddlFG.DataBind();
                this.ddlFG.AppendDataBoundItems = true;
                this.LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");

                var myList = new List<string>(dsfetch.Tables["HEADER"].Rows[0]["FGNAME"].ToString().Trim().Split(','));

                if (dsfetch.Tables[0].Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlFG.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlFG.Items[innercounter].Text)
                            {
                                ddlFG.Items[innercounter].Selected = true;
                            }
                        }
                    }
                }

                if (dsfetch.Tables[1].Rows.Count > 0)
                {
                    this.gvPlannedOrder.DataSource = dsfetch.Tables[1];
                    this.gvPlannedOrder.DataBind();
                }
                else
                {
                    this.gvPlannedOrder.DataSource = null;
                    this.gvPlannedOrder.DataBind();
                }
                if (dsfetch.Tables["HEADER"].Rows[0]["ISCANCELLED"].ToString().Trim() == "N")
                {
                    this.chkCancelled.Checked = false;
                }
                else
                {
                    this.chkCancelled.Checked = true;
                }
                this.ddlfactory.Items.Clear();
                this.ddlfactory.Items.Add(new ListItem(Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["VENDORNAME"]).Trim(), Convert.ToString(dsfetch.Tables["HEADER"].Rows[0]["FACTORYID"]).Trim()));
                this.ddlfactory.AppendDataBoundItems = true;

                if (dsfetch.Tables[3].Rows.Count > 0)
                {
                    this.gvItem.DataSource = dsfetch.Tables[3];
                    this.gvItem.DataBind();
                }
                else
                {
                    this.gvItem.DataSource = null;
                    this.gvItem.DataBind();
                }
                if (dsfetch.Tables[4].Rows.Count > 0)
                {
                    this.gvBOM.DataSource = dsfetch.Tables[4];
                    this.gvBOM.DataBind();
                }
                else
                {
                    this.gvBOM.DataSource = null;
                    this.gvBOM.DataBind();
                }
                this.pnlAdd.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.btnaddhide.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        DataTable dtEntryDt = new DataTable();
        dtEntryDt = oClsProductionOrder.BINDPRODUCTION_ENTRY_GRACEDT();
        int GraceDt = Convert.ToInt32(dtEntryDt.Rows[0]["PRODUCTION_ENTRY_GRACEDT"].ToString());
        DateTime datevalidation = DateTime.Today.Date;
        DateTime beforedays = datevalidation.AddDays(GraceDt);
        CalendarExtender2.EndDate = datevalidation;
        CalendarExtender2.StartDate = beforedays;

        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        ClendFromDateSearch.StartDate = oDate;
        CalenToDateSearch.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtFromDateSearch.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDateSearch.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            ClendFromDateSearch.EndDate = today1;
            CalenToDateSearch.EndDate = today1;
            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;
        }
        else
        {
            this.txtFromDateSearch.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDateSearch.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            ClendFromDateSearch.EndDate = cDate;
            CalenToDateSearch.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
    }
    #endregion

    #region gvCompany_RowDataBound
    protected void gvCompany_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[11] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();
                if (status == "END")
                {
                    cell.ForeColor = Color.Red;
                }
                else if (status == "WIP")
                {
                    cell.ForeColor = Color.Green;
                }
                else if (status == "START")
                {
                    cell.ForeColor = Color.Blue;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnViewStock_Click(object sender, EventArgs e)
    {
        string itemId = "";
        DataTable dt = new DataTable();
        try
        {
            this.divStoreLocationWiseStock.Style["display"] = "";
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblID = (Label)gvr.FindControl("lblID");
            itemId = lblID.Text.Trim();
            dt = oClsProductionOrder.showStockQtyStorelocationWise(itemId);
            if(dt.Rows.Count>0)
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

        catch(Exception ex)
        {
            string msg = ex.ToString();
        }
       
    }



    protected void btnGriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = "CONDITION";
            string status = oClsProductionOrder.StatusCheck(mode, Hdn_Fld.Value.Trim(), "", "");
            if ((status) == "WIP")
            {
                MessageBox1.ShowInfo("Production Already Start Not Allow to Delete");
                return;
            }
            else if (status == "END")
            {
                MessageBox1.ShowWarning("Production End Not Allow to Delete");
                return;
            }
            else
            {
                string status1 = oClsProductionOrder.DeleteProductionOrder(Hdn_Fld.Value.Trim(), "", "");
                if (status1 == "done")
                {
                    MessageBox1.ShowSuccess("Delete SucessFull");
                    BindGrid();
                    return;
                }
                else if (status1 == "error")
                {
                    MessageBox1.ShowSuccess("Requisition Done Not Allow Delete");
                    BindGrid();
                    return;
                }
                else
                {
                    MessageBox1.ShowError("Error,Please Contact To Support Team");
                    return;
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            //code specifically for a ArgumentNullException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (WebException ex)
        {
            //code specifically for a WebException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
           // dt?.Dispose();
        }


    }

    protected void ddlProductionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlFG.Items.Clear();
        this.ddlType.SelectedValue = "0";
        if(ddlProductionType.SelectedValue=="WP")
        {
            ddlType.Items[1].Attributes["enabled"] = "enabled";
            ddlType.Items[2].Attributes["disabled"] = "disabled";
            this.divPlanningNummer.Visible = true;
            this.divProductionQty.Visible = true;

            // ListItem removeItem = ddlType.Items.FindByText("SEMI FINISHED GOODS");
            // ddlType.Items.Remove(removeItem);
        }
        else if (ddlProductionType.SelectedValue== "WOP")
        {
            ddlType.Items[1].Attributes["enabled"] = "enabled";
            ddlType.Items[2].Attributes["enabled"] = "enabled";
            this.divPlanningNummer.Visible = false;
            this.divProductionQty.Visible = false;
            //ListItem removeItem = ddlType.Items.FindByText("SEMI FINISHED GOODS");
            //ddlType.Items.Remove(removeItem);
        }
        else
        {
            this.divPlanningNummer.Visible = false;
            this.divProductionQty.Visible = false;
            ddlType.Items[1].Attributes["disabled"] = "disabled";
            ddlType.Items[2].Attributes["disabled"] = "disabled";
            
        }
       
    }

    protected void ddlPlanningNumber_SelectedIndexChanged(object sender, EventArgs e)
    {

        // this.loadPlanningQty();
        LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");
        this.gvItem.DataSource = null;
        this.gvItem.DataBind();
        this.gvBOM.DataSource = null;
        this.gvBOM.DataBind();


    }

    public void loadPlanningQty()
    {
        string mode = "pplaning";
        DataTable objDt = new DataTable();
        string PRODUCTID = this.ddlFG.SelectedValue.ToString();
        objDt = oClsProductionOrder.loadstorefromdepartment(mode, this.ddlPlanningNumber.SelectedValue.ToString(), PRODUCTID);
        if (objDt.Rows.Count > 0)
        {
            this.txtProductionQty.Text = objDt.Rows[0]["PRODUCTIONQTY"].ToString();
        }
        else
        {
            this.txtProductionQty.Text = "0";
        }
    }

    protected void refer_Click(object sender, EventArgs e)
    {
        this.LoadFGBasisOnMRP(ddlMRP.SelectedValue.ToString(), null, "S");
    }
}