using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using WorkFlow;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using System.Collections;



public partial class VIEW_frmPurchaseOrder_KKG : System.Web.UI.Page
{
    decimal CONVERTIONQTY = 0;
    string Checker = string.Empty;
    string Label = string.Empty;
    Hashtable ht = new Hashtable();
    Hashtable RptHt = new Hashtable();
    //private object ReportViewer1;


    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                Label = Request.QueryString["LABEL"].ToString().Trim();
                ViewState["Checker"] = Checker;

                if (Checker == "TRUE")
                {

                    this.divpono.Style["display"] = "none";
                    this.divponoheader.Style["display"] = "none";
                    this.InputTable.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.rdbNoPlanned.Checked = true;
                    this.tdLable.Visible = false;
                    this.tdControl.Visible = false;
                    this.tdDate.Visible = false;
                    this.tdCalenderFrom.Visible = false;
                    this.tdCalenderTo.Visible = false;
                    this.tdReqSearch.Visible = false;
                    this.ddlReqNo.Enabled = true;
                    this.btnReqSearch.Enabled = true;
                    this.rdbPlanned.Enabled = true;
                    this.rdbNoPlanned.Enabled = true;
                    DateTime dtcurr = DateTime.Now;
                    string date = "dd/MM/yyyy";
                    this.txtpodate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtReqFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtReqToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtqutrefdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtreqfromdateset.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.txtreqtotodateset.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.CalendarExtender3.EndDate = DateTime.Now;
                    this.CalendarExtender6.EndDate = DateTime.Now;
                    this.CalendarExtender7.EndDate = DateTime.Now;
                    this.CalendarExtender4.EndDate = DateTime.Now;
                    this.LoadPacksize();
                    this.LoadPO(Label);
                    this.LoadTPUName();
                    this.divbtnapprove.Style["display"] = "none";
                    this.divbtnreject.Style["display"] = "none";
                    this.divbtnsave.Style["display"] = "";
                    this.divbtnHoldsave.Style["display"] = "";
                    this.divadd.Style["display"] = "";
                    //this.gvpodetails.Columns[9].Visible = true;
                    //this.gvpodetails.Columns[11].Visible = true;
                }

                else
                {
                    if (Label == "POC-1")
                    {
                        this.divpono.Style["display"] = "none";
                        this.divponoheader.Style["display"] = "none";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.rdbNoPlanned.Checked = true;
                        this.tdLable.Visible = false;
                        this.tdControl.Visible = false;
                        this.tdDate.Visible = false;
                        this.tdCalenderFrom.Visible = false;
                        this.tdCalenderTo.Visible = false;
                        this.tdReqSearch.Visible = false;
                        this.ddlReqNo.Enabled = true;
                        this.btnReqSearch.Enabled = true;
                        this.rdbPlanned.Enabled = true;
                        this.rdbNoPlanned.Enabled = true;
                        DateTime dtcurr = DateTime.Now;
                        string date = "dd/MM/yyyy";
                        this.txtpodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtReqFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtReqToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtqutrefdate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtreqfromdateset.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtreqtotodateset.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.CalendarExtender3.EndDate = DateTime.Now;
                        this.CalendarExtender6.EndDate = DateTime.Now;
                        this.CalendarExtender7.EndDate = DateTime.Now;
                        this.CalendarExtender4.EndDate = DateTime.Now;
                        this.LoadPacksize();
                        this.LoadPO(Label);
                        this.LoadTPUName();
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "";
                        this.divbtnreject.Style["display"] = "";
                        this.divadd.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "none";

                        //this.gvpodetails.Columns[9].Visible = false;
                        //this.gvpodetails.Columns[11].Visible = false;

                    }
                    else if (Label == "POC-2")
                    {
                        this.divpono.Style["display"] = "none";
                        this.divponoheader.Style["display"] = "none";
                        this.InputTable.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.rdbNoPlanned.Checked = true;
                        this.tdLable.Visible = false;
                        this.tdControl.Visible = false;
                        this.tdDate.Visible = false;
                        this.tdCalenderFrom.Visible = false;
                        this.tdCalenderTo.Visible = false;
                        this.tdReqSearch.Visible = false;
                        this.ddlReqNo.Enabled = true;
                        this.btnReqSearch.Enabled = true;
                        this.rdbPlanned.Enabled = true;
                        this.rdbNoPlanned.Enabled = true;
                        DateTime dtcurr = DateTime.Now;
                        string date = "dd/MM/yyyy";
                        this.txtpodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtReqFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtReqToDate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtqutrefdate.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtreqfromdateset.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.txtreqtotodateset.Text = dtcurr.ToString(date).Replace('-', '/');
                        this.CalendarExtender3.EndDate = DateTime.Now;
                        this.CalendarExtender6.EndDate = DateTime.Now;
                        this.CalendarExtender7.EndDate = DateTime.Now;
                        this.CalendarExtender4.EndDate = DateTime.Now;
                        this.LoadPacksize();
                        this.LoadPO(Label);
                        this.LoadTPUName();
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "";
                        this.divbtnreject.Style["display"] = "";
                        this.divadd.Style["display"] = "none";
                        //this.gvpodetails.Columns[9].Visible = false;
                        //this.gvpodetails.Columns[11].Visible = false;
                    }

                }
                this.loadtype();/*for load SUPLIEDITEM without fg*/



            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadPO
    public void LoadPO(string Label)
    {
        try
        {
            string type = this.ddlType.SelectedValue;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            gvpodetails.DataSource = clspurchaseorder.BindPO_kkg(txtfromdate.Text, txttodate.Text, Session["FINYEAR"].ToString(), Label, type);
            gvpodetails.DataBind();
            //if(Label!="POC")
            //{
            //    this.gvpodetails.Columns[9].Visible = false;
            //    this.gvpodetails.Columns[11].Visible = false;
            //}
            //else
            //{
            //    this.gvpodetails.Columns[9].Visible = true;
            //    this.gvpodetails.Columns[11].Visible = true;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadPOGrid
    public void LoadPOGrid()
    {
        try
        {
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            gvPurchaseOrder.DataSource = clspurchaseorder.BindPOGrid(hdn_pofield.Value.ToString());
            gvPurchaseOrder.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadTPU
    public void LoadTPUName()  /*load tpu */
    {
        try
        {
            string mode = "1";
            ddlTPUName.Items.Clear();
            ddlTPUName.Items.Add(new ListItem("SELECT TPU NAME", "0"));
            ddlTPUName.AppendDataBoundItems = true;
            ClsMMPoOrder clsmmpo = new ClsMMPoOrder();
            ddlTPUName.DataSource = clsmmpo.BindVendorCurrencey(mode);
            ddlTPUName.DataValueField = "VENDORID";
            ddlTPUName.DataTextField = "VENDORNAME";
            ddlTPUName.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Requirment
    public void LoadRequirment()
    {
        try
        {
            ClsPoOrder obj = new ClsPoOrder();
            this.ddlReqNo.Items.Clear();
            this.ddlReqNo.Items.Add(new ListItem("Select Req. No.", "0"));
            this.ddlReqNo.AppendDataBoundItems = true;
            this.ddlReqNo.DataSource = obj.BindRequirement(this.txtReqFromDate.Text.Trim(), this.txtReqToDate.Text.Trim(), Convert.ToString(Session["FINYEAR"]).Trim());
            this.ddlReqNo.DataValueField = "REQID";
            this.ddlReqNo.DataTextField = "REQNUMBER";
            this.ddlReqNo.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    #region LoadPacksize
    public void LoadPacksize()
    {
        try
        {
            ddlpacksize.Items.Clear();
            //ddlpacksize.Items.Add(new ListItem("SELECT", "0"));
            ddlpacksize.AppendDataBoundItems = true;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            ddlpacksize.DataSource = clspurchaseorder.BindPacksize();
            ddlpacksize.DataValueField = "PACKSIZEID";
            ddlpacksize.DataTextField = "PACKSIZENAME";
            ddlpacksize.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region type
    public void loadtype()
    {
        try
        {
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem("SELECT ALL", "0"));
            ddlType.AppendDataBoundItems = true;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            ddlType.DataSource = clspurchaseorder.bindTypeWithOutFg();
            ddlType.DataValueField = "ID";
            ddlType.DataTextField = "ITEM_Name";
            ddlType.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region setLAstRate
    public void ProductWiseLastRateBind()
    {
        DataTable dtrate = new DataTable();
        ClsPoOrder clspurchaseorder = new ClsPoOrder();
        string POID = hdn_pofield.Value.ToString().Trim();
        try
        {
            if (POID == "")
            {
                MessageBox1.ShowWarning("No Purchase Order Avilabe For Duplicate Copy");
                return;
            }
            else
            {
                dtrate = clspurchaseorder.GetProductWiseRate(POID);
                if (dtrate.Rows.Count > 0)
                {
                    this.gvPurchaseOrder.DataSource = dtrate;
                    this.gvPurchaseOrder.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion


    #region ddlTPUName_SelectedIndexChanged
    protected void ddlTPUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.rdbNoPlanned.Checked == true)
            {
                if (ddlTPUName.SelectedValue == "0")
                {

                    this.grdpodetailsadd.DataSource = null;
                    this.grdpodetailsadd.DataBind();

                }
                else
                {
                    this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim());
                    if (this.grdpodetailsadd.Rows.Count > 0)
                    {
                        if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                        {
                            this.grdpodetailsadd.HeaderRow.Cells[8].Text = "TRANSFER RATE";
                        }
                        else
                        {
                            this.grdpodetailsadd.HeaderRow.Cells[8].Text = "PURCHASE COST";
                        }
                    }
                    else
                    {
                        MessageBox1.ShowWarning("Product not mapped with this vendor");
                        return;
                    }

                }
            }
            else
            {
                if (ddlTPUName.SelectedValue == "0")
                {
                    this.grdpodetailsadd.DataSource = null;
                    this.grdpodetailsadd.DataBind();
                }
                else
                {
                    this.RequirementProductDetails(this.ddlTPUName.SelectedValue.Trim(), this.ddlpacksize.SelectedValue.Trim(),
                                                    this.ddlReqNo.SelectedValue.Trim(), this.txtpodate.Text.Trim()
                                                  );

                    if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                    {
                        this.grdpodetailsadd.HeaderRow.Cells[8].Text = "TRANSFER RATE";
                    }
                    else
                    {
                        this.grdpodetailsadd.HeaderRow.Cells[8].Text = "PURCHASE COST";
                    }

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



    #region btn Schedule date
    protected void btnscheduledate_Click(object sender, EventArgs e)
    {
        try
        {
            int delfromdate = Convert.ToInt32(Conver_To_ISO(this.txtreqfromdateset.Text.Trim()));
            int deltodate = Convert.ToInt32(Conver_To_ISO(this.txtreqtotodateset.Text.Trim()));
            int podate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));


            if (this.ddlTPUName.SelectedValue.Trim() == "9BCE2A2E-18A2-4EBD-AA3B-69471965B1E8" && this.ddlpacksize.SelectedValue.Trim() != "71B973E8-28E3-4F3A-A86E-9475AF2D14EE")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Purchase unit should be BOX for Bindi</font></b>", 60, 500);
                return;
            }

            if (deltodate < delfromdate)
            {
                MessageBox1.ShowInfo("<b><font color='red'>Delivery schedule Fromdate should be greater than Todate</font></b>", 60, 500);
                return;
            }
            if (deltodate >= delfromdate)
            {
                if (grdpodetailsadd.Rows.Count > 0)
                {
                    this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim());
                    for (int grddate = 0; grddate < grdpodetailsadd.Rows.Count; grddate++)
                    {
                        TextBox grdtxtdeliverydatefrom = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydatefrom");
                        TextBox grdtxtdeliverydateto = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydateto");
                        grdtxtdeliverydatefrom.Text = this.txtreqfromdateset.Text.Trim();
                        grdtxtdeliverydateto.Text = this.txtreqtotodateset.Text.Trim();
                        Label grdlblpacksizeid = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizeid");
                        grdlblpacksizeid.Text = this.ddlpacksize.SelectedValue.Trim();
                        Label grdlblpacksizename = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizename");
                        grdlblpacksizename.Text = this.ddlpacksize.SelectedItem.ToString().Trim();

                    }
                }

            }

            if
              (this.txtSearchBox.Text != "")

                this.BindGrid();

            for (int grddate = 0; grddate < grdpodetailsadd.Rows.Count; grddate++)
            {
                TextBox grdtxtdeliverydatefrom = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydatefrom");
                TextBox grdtxtdeliverydateto = (TextBox)grdpodetailsadd.Rows[grddate].FindControl("grdtxtdeliverydateto");
                grdtxtdeliverydatefrom.Text = this.txtreqfromdateset.Text.Trim();
                grdtxtdeliverydateto.Text = this.txtreqtotodateset.Text.Trim();
                Label grdlblpacksizeid = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizeid");
                grdlblpacksizeid.Text = this.ddlpacksize.SelectedValue.Trim();
                Label grdlblpacksizename = (Label)grdpodetailsadd.Rows[grddate].FindControl("grdlblpacksizename");
                grdlblpacksizename.Text = this.ddlpacksize.SelectedItem.ToString().Trim();

            }
            if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
            {
                this.grdpodetailsadd.HeaderRow.Cells[8].Text = "TRANSFER RATE";
            }
            else
            {
                this.grdpodetailsadd.HeaderRow.Cells[8].Text = "PURCHASE COST";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    #endregion



    #region Add New Record
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            chkDuplicatepo.Checked = false;
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            ViewState["Checker"] = Checker;
            this.ddlReqNo.Enabled = true;
            this.btnReqSearch.Enabled = true;
            this.rdbPlanned.Enabled = true;
            this.rdbNoPlanned.Enabled = true;
            this.rdbNoPlanned.Checked = true;
            this.rdbPlanned.Checked = false;
            this.div_btnPrint.Style["display"] = "none";
            this.divProductDetails.Style["display"] = "";
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";
            this.hdn_convertionqty.Value = "";
            this.Hdn_Fld.Value = "";
            this.hdn_podelete.Value = "";
            this.hdn_pofield.Value = "";
            this.hdnExcise.Value = "";
            this.ddlTPUName.Enabled = true;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divpono.Style["display"] = "none";
            this.divponoheader.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.InputTable.Enabled = true;
            this.imgPopuppodate.Visible = true;
            this.rdbNoPlanned.Checked = true;
            this.tdLable.Visible = false;
            this.tdControl.Visible = false;
            this.tdDate.Visible = false;
            this.tdCalenderFrom.Visible = false;
            this.tdCalenderTo.Visible = false;
            this.tdReqSearch.Visible = false;
            clspurchaseorder.BindPurchaseOrderGrid();
            this.TaxPercentage();
            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";
            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";


            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            this.divbtnsave.Style["display"] = "";
            this.divbtnHoldsave.Style["display"] = "";
            this.tblADD.Style["display"] = "";

            this.LoadTPUName();
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            Session["PORECORDSV2"] = null;
            this.txtqutrefdate.Text = "";
            this.txtreqtotodateset.Text = "";
            this.trscheduledate.Style["display"] = "";

            if (Checker == "TRUE")
            {
                this.divbtnapprove.Style["display"] = "none";
                this.divbtnreject.Style["display"] = "none";
                this.divBtnConfirm.Style["display"] = "none";
                this.divbtnsave.Style["display"] = "";
                this.divbtnHoldsave.Style["display"] = "";
            }
            else
            {
                this.divbtnsave.Style["display"] = "none";
                this.divbtnHoldsave.Style["display"] = "none";
                this.divbtnapprove.Style["display"] = "";
                this.divbtnreject.Style["display"] = "";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region TaxPercentage
    protected void TaxPercentage()
    {
        try
        {
            DataTable dt = new DataTable();
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            dt = clspurchaseorder.TaxPercentage();
            if (dt.Rows.Count > 0)
            {
                this.hdnExcise.Value = dt.Rows[0]["PERCENTAGE"].ToString();
                this.hdnCST.Value = dt.Rows[1]["PERCENTAGE"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ResetAll
    public void ResetAll()
    {
        try
        {
            this.txtcasepack.Text = "";
            this.btnsave.Enabled = true;
            this.hdn_pofield.Value = "";
            this.hdn_podelete.Value = "";
            this.hdnCST.Value = "";
            this.hdn_convertionqty.Value = "";
            this.hdn_podelete.Value = "";
            this.hdn_pofield.Value = "";
            this.hdnExcise.Value = "";
            this.ddlTPUName.SelectedValue = "0";
            this.ddlTPUName.Enabled = true;
            this.txtremarks.Text = "";
            this.txtgrosstotal.Text = "0";
            this.txtadjustment.Text = "0";
            this.txtTotalMRP.Text = "0";
            this.txtpacking.Text = "0";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtnettotal.Text = "0";
            this.txtremarks.Text = "";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtTotalMRP.Text = "0";
            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.divadd.Style["display"] = "";
            this.divpono.Style["display"] = "none";
            this.divponoheader.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            Label = Request.QueryString["LABEL"].ToString().Trim();
            ViewState["Checker"] = Checker;
            this.ddlReqNo.Enabled = true;
            this.btnReqSearch.Enabled = true;
            this.rdbPlanned.Enabled = true;
            this.rdbNoPlanned.Enabled = true;
            this.rdbNoPlanned.Checked = true;
            this.rdbPlanned.Checked = false;
            this.ResetAll();
            this.divProductDetails.Style["display"] = "";
            this.tblADD.Style["display"] = "";
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            this.LoadPO(Label);

            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            Session["PORECORDSV2"] = null;
            this.txtqutrefdate.Text = "";
            this.txtreqtotodateset.Text = "";
            trscheduledate.Style["display"] = "";

            this.tdLable.Visible = false;
            this.tdControl.Visible = false;
            this.tdDate.Visible = false;
            this.tdCalenderFrom.Visible = false;
            this.tdCalenderTo.Visible = false;
            this.tdReqSearch.Visible = false;
            if (Checker == "TRUE")
            {

                this.divProductDetails.Visible = true;
                this.btnadd.Visible = true;
                this.divbtnapprove.Style["display"] = "none";
                this.divBtnConfirm.Style["display"] = "none";
                this.divbtnreject.Style["display"] = "none";
                this.divbtnsave.Style["display"] = "";
                this.divbtnHoldsave.Style["display"] = "";

            }
            else
            {
                if (Label == "POC-1")
                {
                    this.divProductDetails.Visible = false;

                    this.divbtnsave.Style["display"] = "none";
                    this.divbtnHoldsave.Style["display"] = "none";
                    this.divBtnConfirm.Style["display"] = "none";
                    this.divadd.Style["display"] = "none";
                    this.divbtnapprove.Style["display"] = "";
                    this.divbtnreject.Style["display"] = "";
                }
                else if (Label == "POC-2")
                {
                    this.divProductDetails.Visible = false;
                    this.divbtnsave.Style["display"] = "none";
                    this.divbtnHoldsave.Style["display"] = "none";
                    this.divbtnapprove.Style["display"] = "none";
                    this.divadd.Style["display"] = "none";
                    this.divBtnConfirm.Style["display"] = "";
                    this.divbtnreject.Style["display"] = "";
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

    #region CountPOQty
    protected decimal CountPOQty()
    {
        decimal ReturnValue = 0;

        if (grdpodetailsadd.Rows.Count > 0)
        {
            for (int i = 0; i < grdpodetailsadd.Rows.Count; i++)
            {
                TextBox grdtxtpoqty = (TextBox)grdpodetailsadd.Rows[i].FindControl("grdtxtpoqty");

                if (grdtxtpoqty.Text != "" && Convert.ToDecimal(grdtxtpoqty.Text) > 0)
                {
                    ReturnValue += Convert.ToDecimal(grdtxtpoqty.Text.Trim());

                }
            }
        }
        return ReturnValue;
    }
    #endregion

    #region BindPurchaseOrderGridStructure
    public DataTable BindPurchaseOrderGridStructure()
    {

        DataTable dtPORecord = new DataTable();
        dtPORecord.Clear();
        dtPORecord.Columns.Add(new DataColumn("DIVISIONID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("DIVISIONNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("NATUREOFPRODUCTID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("NATUREOFPRODUCTNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTCODE", typeof(string)));//PRODUCTCODE
        dtPORecord.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dtPORecord.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));//PRODUCTNAME
        dtPORecord.Columns.Add(new DataColumn("PRODUCTQTY", typeof(string)));//PRODUCTQTY
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPACKINGSIZEID", typeof(string)));//PRODUCTPACKINGSIZEID
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPACKINGSIZE", typeof(string)));//PRODUCTPACKINGSIZE
        dtPORecord.Columns.Add(new DataColumn("PRODUCTCONVERSIONQTY", typeof(string)));//PRODUCTCONVERSIONQTY
        dtPORecord.Columns.Add(new DataColumn("PRODUCTPRICE", typeof(string)));//PRODUCTPRICE
        dtPORecord.Columns.Add(new DataColumn("PRODUCTAMOUNT", typeof(string)));//PRODUCTAMOUNT
        dtPORecord.Columns.Add(new DataColumn("MRP", typeof(string)));//MRP
        dtPORecord.Columns.Add(new DataColumn("MRPVALUE", typeof(string)));//MRPVALUE
        dtPORecord.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));//ASSESMENTPERCENTAGE
        dtPORecord.Columns.Add(new DataColumn("EXCISE", typeof(string)));//EXCISE
        dtPORecord.Columns.Add(new DataColumn("CST", typeof(string)));//CST
        dtPORecord.Columns.Add(new DataColumn("REQUIREDDATE", typeof(string)));//REQUIREDDATE
        dtPORecord.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));//REQUIREDTODATE
        dtPORecord.Columns.Add(new DataColumn("MODE", typeof(string)));//MODE
        dtPORecord.Columns.Add(new DataColumn("DISCPER", typeof(string)));//DISCPER
        dtPORecord.Columns.Add(new DataColumn("DISCAMNT", typeof(string)));//DISCAMNT
        dtPORecord.Columns.Add(new DataColumn("LASTRATE", typeof(string)));//LASTRATE

        dtPORecord.Columns.Add(new DataColumn("CGSTID", typeof(string)));//CGSTID
        dtPORecord.Columns.Add(new DataColumn("CGSTPER", typeof(string)));//CGSTPER
        dtPORecord.Columns.Add(new DataColumn("CGSTAMNT", typeof(string)));//CGSTAMNT
        dtPORecord.Columns.Add(new DataColumn("SGSTID", typeof(string)));//SGSTID
        dtPORecord.Columns.Add(new DataColumn("SGSTPER", typeof(string)));//SGSTPER
        dtPORecord.Columns.Add(new DataColumn("SGSTAMNT", typeof(string)));//SGSTAMNT
        dtPORecord.Columns.Add(new DataColumn("IGSTID", typeof(string)));//IGSTID
        dtPORecord.Columns.Add(new DataColumn("IGSTPER", typeof(string)));//IGSTPER
        dtPORecord.Columns.Add(new DataColumn("IGSTAMNT", typeof(string)));//IGSTAMNT
        dtPORecord.Columns.Add(new DataColumn("VALUEWITHTAX", typeof(string)));//VALUEWITHTAX
        HttpContext.Current.Session["PORECORDSV2"] = dtPORecord;
        return dtPORecord;
    }
    #endregion

    #region CalculateTotalAmount
    decimal CalculateTotalAmount(DataTable dt)
    {
        decimal GrossTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["VALUEWITHTAX"]);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return GrossTotal;
    }
    #endregion

    #region CalculateTotalMRP
    decimal CalculateTotalMRP(DataTable dt)
    {
        decimal MRPTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                MRPTotal += Convert.ToDecimal(dt.Rows[Counter]["MRPVALUE"]);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return MRPTotal;
    }
    #endregion

    #region CalculateTotalExcise
    decimal CalculateTotalExcise(DataTable dt)
    {
        decimal ExciseTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                ExciseTotal += Convert.ToDecimal(dt.Rows[Counter]["MRPVALUE"]) * (Convert.ToDecimal(dt.Rows[Counter]["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dt.Rows[Counter]["EXCISE"]) / 100);


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return ExciseTotal;
    }
    #endregion

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDecimal(this.txtDiscountAmnt.Text.ToString()) > 0)
            {
                MessageBox1.ShowWarning("Please Make Discount Amount zero then add");
                txtDiscountAmnt.BackColor = Color.Red;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtDiscountAmnt.ClientID + "').focus(); ", true);
                return;
            }
            else if (Convert.ToDecimal(this.txtFreightAmnt.Text.ToString()) > 0)
            {
                MessageBox1.ShowWarning("Please Make Freight Amount zero then add");
                txtFreightAmnt.BackColor = Color.Red;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtFreightAmnt.ClientID + "').focus(); ", true);
                return;
            }
            else
            {
                txtDiscountAmnt.BackColor = Color.White;
                txtFreightAmnt.BackColor = Color.White;
            }
            ClsPoOrder clspurchaseorder = new ClsPoOrder();

            if (HttpContext.Current.Session["PORECORDSV2"] == null)
            {
                this.BindPurchaseOrderGridStructure();
            }
            DataTable dtPurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

            int dtpoEntrydate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));

            int flag = 0;
            decimal TotPOQty = 0;
            int STATUS = 0;
            string ProductName = string.Empty;
            decimal GrossTotal = 0;
            decimal NetTotal = 0;
            decimal TotalMRP = 0;
            decimal TotalExcise = 0;
            decimal TotalCST = 0;

            TotPOQty = CountPOQty();
            if (TotPOQty == 0)
            {
                MessageBox1.ShowInfo("<b><font color='red'>Please enter PO Quantity..!</font></b>", 50, 500);
                return;
            }

            if (grdpodetailsadd.Rows.Count > 0)
            {
                for (int grdloop = 0; grdloop < grdpodetailsadd.Rows.Count; grdloop++)
                {

                    Label grdlblproductid = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblproductid");
                    Label grdlblproductname = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblproductname");
                    Label grdlblcode = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblcode");
                    Label grdlblpacksizeid = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpacksizeid");
                    Label grdlblpacksizename = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpacksizename");
                    Label grdlblpcsqty = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpcsqty");
                    TextBox grdlblpurchasecost = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblpurchasecost");
                    TextBox grdtxtpoqty = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtpoqty");
                    TextBox grdTxtDiscPer = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdTxtDiscPer");//new add grdlblmrp
                    TextBox grdTxtDiscAmnt = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdTxtDiscAmnt");//new add
                    TextBox grdlblbasiccostvalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblbasiccostvalue");
                    Label grdlblmrp = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblmrp");
                    TextBox grdlblmrpvalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblmrpvalue");
                    Label grdlblassesmentpercent = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblassesmentpercent");
                    Label grdlblexcise = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblexcise");
                    Label grdlblcst = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblcst");
                    TextBox grdlblexcisevalue = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdlblexcisevalue");
                    TextBox grdtxtdeliverydatefrom = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtdeliverydatefrom");
                    TextBox grdtxtdeliverydateto = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdtxtdeliverydateto");

                    Label grdlblDIVISIONID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblDIVISIONID");
                    Label grdlblDIVISIONNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblDIVISIONNAME");
                    Label grdlblCATEGORYID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblCATEGORYID");
                    Label grdlblCATEGORYNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblCATEGORYNAME");
                    Label grdlblNATUREOFPRODUCTID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblNATUREOFPRODUCTID");
                    Label grdlblNATUREOFPRODUCTNAME = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblNATUREOFPRODUCTNAME");
                    Label grdlblUOMID = (Label)grdpodetailsadd.Rows[grdloop].FindControl("grdlblUOMID");
                    DropDownList grdlblUOMNAME = (DropDownList)grdpodetailsadd.Rows[grdloop].FindControl("grdlblUOMNAME");
                    object uomname = grdlblUOMNAME.SelectedItem;
                    object uomid = grdlblUOMNAME.SelectedValue;


                    TextBox grdCGSTID = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdCGSTID");
                    TextBox grdCGSTPER = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdCGSTPER");
                    TextBox grdCGSTAMNT = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdCGSTAMNT");
                    TextBox grdSGSTID = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdSGSTID");
                    TextBox grdSGSTPER = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdSGSTPER");
                    TextBox grdSGSTAMNT = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdSGSTAMNT");
                    TextBox grdIGSTID = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdIGSTID");
                    TextBox grdIGSTPER = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdIGSTPER");
                    TextBox grdIGSTAMNT = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdIGSTAMNT");
                    TextBox grdVALUEWITHTAX = (TextBox)grdpodetailsadd.Rows[grdloop].FindControl("grdVALUEWITHTAX");





                    if (Convert.ToDecimal(grdtxtpoqty.Text.Trim()) != 0 && grdtxtpoqty.Text.Trim() != "")
                    {
                        int dtpofromdate = 0;
                        int dtpotodate = 0;
                        if (txtreqfromdateset.Text.Trim() != "")
                        {
                            dtpofromdate = Convert.ToInt32(Conver_To_ISO(txtreqfromdateset.Text.Trim()));
                        }
                        if (txtreqtotodateset.Text.Trim() != "")
                        {
                            dtpotodate = Convert.ToInt32(Conver_To_ISO(txtreqtotodateset.Text.Trim()));
                        }


                        flag = clspurchaseorder.PORecordsCheckV2(grdlblproductid.Text.Trim(), grdlblpacksizeid.Text.Trim(), txtreqfromdateset.Text.Trim(),
                                                                  txtreqtotodateset.Text.Trim(), hdn_pofield.Value.ToString());



                        if (flag == 0)
                        {

                            hdn_convertionqty.Value = Convert.ToString(Convert.ToDecimal(grdtxtpoqty.Text.Trim()) * Convert.ToDecimal(grdlblpcsqty.Text.Trim()));

                            #region ADD MODE
                            if (this.hdn_pofield.Value.ToString() == "")
                            {
                                if (STATUS == 0)
                                {
                                    if (grdlblDIVISIONID.Text.Trim() != "6B869907-14E0-44F6-9BA5-57F4DA726686")/*Deeva Brand*/
                                    {
                                        if (Convert.ToDecimal(grdlblpurchasecost.Text.Trim()) == 0)
                                        {
                                            MessageBox1.ShowWarning("Zero rate Billing not allow Please review rate");
                                            return;
                                        }
                                        else
                                        {

                                            DataRow dr = dtPurchaseOrderDetails.NewRow();
                                            dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                            dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                            dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                            dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                            dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                            dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                            dr["UOMID"] = uomid;
                                            //Convert.ToString(grdlblUOMID.Text.Trim());
                                            dr["UOMNAME"] = uomname;
                                            //Convert.ToString(grdlblUOMNAME.Text.Trim());
                                            dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                            dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                            dr["PRODUCTCODE"] = Convert.ToString(grdlblcode.Text.Trim());
                                            dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                            dr["DISCPER"] = Convert.ToString(grdTxtDiscPer.Text.Trim());//NEW ADD
                                            dr["DISCAMNT"] = Convert.ToString(grdTxtDiscAmnt.Text.Trim());//NEW ADD
                                            dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(grdlblUOMID.Text.Trim());//grdlblpacksizeid
                                            dr["PRODUCTPACKINGSIZE"] = Convert.ToString(grdlblUOMNAME.SelectedItem.Text.ToString());//grdlblpacksizename
                                            dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                            dr["PRODUCTPRICE"] = Convert.ToString(grdlblpurchasecost.Text.Trim());
                                            dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                            dr["MRP"] = 0;
                                            //Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));
                                            dr["MRPVALUE"] = 0;
                                            //Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                            dr["ASSESMENTPERCENTAGE"] = 0;
                                            //Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                            dr["EXCISE"] = 0;
                                            //Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                            dr["CST"] = 0;
                                            //Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                            dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                            dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                            dr["MODE"] = "A";
                                            dr["LASTRATE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));

                                            dr["CGSTID"] = Convert.ToString(grdCGSTID.Text.Trim());
                                            dr["CGSTPER"] = Convert.ToDecimal(grdCGSTPER.Text.Trim());
                                            dr["CGSTAMNT"] = Convert.ToDecimal(grdCGSTAMNT.Text.Trim());

                                            dr["SGSTID"] = Convert.ToString(grdSGSTID.Text.Trim());
                                            dr["SGSTPER"] = Convert.ToDecimal(grdSGSTPER.Text.Trim());
                                            dr["SGSTAMNT"] = Convert.ToDecimal(grdSGSTAMNT.Text.Trim());

                                            dr["IGSTID"] = Convert.ToString(grdIGSTID.Text.Trim());
                                            dr["IGSTPER"] = Convert.ToDecimal(grdIGSTPER.Text.Trim());
                                            dr["IGSTAMNT"] = Convert.ToDecimal(grdIGSTAMNT.Text.Trim());
                                            dr["VALUEWITHTAX"] = Convert.ToDecimal(grdVALUEWITHTAX.Text.Trim());

                                            dtPurchaseOrderDetails.Rows.Add(dr);
                                            dtPurchaseOrderDetails.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        //if (grdtxtdeliverydatefrom.Text.Trim() != "" && grdtxtdeliverydateto.Text.Trim() != "" && dtpoEntrydate <= dtpofromdate
                                        //     && dtpofromdate <= dtpotodate)
                                        //{

                                        DataRow dr = dtPurchaseOrderDetails.NewRow();
                                        dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                        dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                        dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                        dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                        dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                        dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                        dr["UOMID"] = Convert.ToString(grdlblUOMID.Text.Trim());
                                        dr["UOMNAME"] = Convert.ToString(grdlblUOMNAME.Text.Trim());
                                        dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                        dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                        dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                        dr["DISCPER"] = Convert.ToString(grdTxtDiscPer.Text.Trim());//NEW ADD
                                        dr["DISCAMNT"] = Convert.ToString(grdTxtDiscAmnt.Text.Trim());//NEW ADD
                                        dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(grdlblpacksizeid.Text.Trim());
                                        dr["PRODUCTPACKINGSIZE"] = Convert.ToString(grdlblpacksizename.Text.Trim());
                                        dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                        dr["PRODUCTPRICE"] = Convert.ToString(grdlblpurchasecost.Text.Trim());
                                        dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                        dr["MRP"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));
                                        dr["MRPVALUE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                        dr["EXCISE"] = Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                        dr["CST"] = Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                        dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                        dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                        dr["MODE"] = "A";
                                        dr["LASTRATE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));


                                        dr["CGSTID"] = Convert.ToString(grdCGSTID.Text.Trim());
                                        dr["CGSTPER"] = Convert.ToDecimal(grdCGSTPER.Text.Trim());
                                        dr["CGSTAMNT"] = Convert.ToDecimal(grdCGSTAMNT.Text.Trim());

                                        dr["SGSTID"] = Convert.ToString(grdSGSTID.Text.Trim());
                                        dr["SGSTPER"] = Convert.ToDecimal(grdSGSTPER.Text.Trim());
                                        dr["SGSTAMNT"] = Convert.ToDecimal(grdSGSTAMNT.Text.Trim());

                                        dr["IGSTID"] = Convert.ToString(grdIGSTID.Text.Trim());
                                        dr["IGSTPER"] = Convert.ToDecimal(grdIGSTPER.Text.Trim());
                                        dr["IGSTAMNT"] = Convert.ToDecimal(grdIGSTAMNT.Text.Trim());
                                        dr["VALUEWITHTAX"] = Convert.ToDecimal(grdVALUEWITHTAX.Text.Trim());

                                        dtPurchaseOrderDetails.Rows.Add(dr);
                                        dtPurchaseOrderDetails.AcceptChanges();
                                        //}
                                    }
                                }
                            }
                            #endregion

                            #region EDIT MODE
                            else
                            {
                                if (Convert.ToDecimal(grdtxtpoqty.Text.Trim()) != 0 && grdtxtpoqty.Text.Trim() != "") /*&& grdtxtdeliverydatefrom.Text.Trim() != "" && grdtxtdeliverydateto.Text.Trim() != ""*/
                                {
                                    if (this.txtreqfromdateset.Text.Trim() == "")
                                    {
                                        ProductName = grdlblproductname.Text.Trim();
                                        STATUS = 1;

                                        DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                        dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                        DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                        for (int i = 0; i < drr.Length; i++)
                                        {
                                            drr[i].Delete();
                                            dtdeletePurchaseOrderDetails.AcceptChanges();
                                        }

                                        HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                        break;
                                    }

                                    if (txtreqtotodateset.Text.Trim() == "")
                                    {

                                        ProductName = grdlblproductname.Text.Trim();
                                        STATUS = 2;
                                        DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                        dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                        DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                        for (int i = 0; i < drr.Length; i++)
                                        {
                                            drr[i].Delete();
                                            dtdeletePurchaseOrderDetails.AcceptChanges();
                                        }

                                        HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                        break;
                                    }

                                    if (dtpofromdate < dtpoEntrydate)
                                    {

                                        ProductName = grdlblproductname.Text.Trim();
                                        STATUS = 3;
                                        DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                        dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                        DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                        for (int i = 0; i < drr.Length; i++)
                                        {
                                            drr[i].Delete();
                                            dtdeletePurchaseOrderDetails.AcceptChanges();
                                        }

                                        HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                        break;
                                    }

                                    if (dtpotodate < dtpofromdate)
                                    {

                                        ProductName = grdlblproductname.Text.Trim();
                                        STATUS = 4;
                                        DataTable dtdeletePurchaseOrderDetails = new DataTable();
                                        dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

                                        DataRow[] drr = dtdeletePurchaseOrderDetails.Select("MODE='E'");
                                        for (int i = 0; i < drr.Length; i++)
                                        {
                                            drr[i].Delete();
                                            dtdeletePurchaseOrderDetails.AcceptChanges();
                                        }

                                        HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;
                                        break;
                                    }

                                    if (STATUS == 0)
                                    {
                                        if (grdlblDIVISIONID.Text.Trim() != "6B869907-14E0-44F6-9BA5-57F4DA726686")/*Deeva Brand*/
                                        {
                                            if (txtreqfromdateset.Text.Trim() != "" && txtreqtotodateset.Text.Trim() != "" && dtpoEntrydate <= dtpofromdate
                                             && dtpofromdate <= dtpotodate && Convert.ToDecimal(grdlblpurchasecost.Text.Trim()) > 0)
                                            {

                                                DataRow dr = dtPurchaseOrderDetails.NewRow();
                                                dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                                dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                                dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                                dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                                dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                                dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                                dr["UOMID"] = uomid;
                                                dr["UOMNAME"] = uomname;
                                                dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                                dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                                dr["PRODUCTCODE"] = Convert.ToString(grdlblcode.Text.Trim());
                                                dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                                dr["DISCPER"] = Convert.ToString(grdTxtDiscPer.Text.Trim());//NEW ADD
                                                dr["DISCAMNT"] = Convert.ToString(grdTxtDiscAmnt.Text.Trim());//NEW ADD
                                                dr["PRODUCTPACKINGSIZEID"] = uomid;
                                                dr["PRODUCTPACKINGSIZE"] = uomname;
                                                dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                                dr["PRODUCTPRICE"] = Convert.ToString(grdlblpurchasecost.Text.Trim());
                                                dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                                dr["MRP"] = 0;
                                                dr["MRPVALUE"] = 0;
                                                //Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                                dr["ASSESMENTPERCENTAGE"] = 0;
                                                //Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                                dr["EXCISE"] = 0;
                                                // Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                                dr["CST"] = 0;
                                                // Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                                dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                                dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                                dr["MODE"] = "E";
                                                dr["LASTRATE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));

                                                dr["CGSTID"] = Convert.ToString(grdCGSTID.Text.Trim());
                                                dr["CGSTPER"] = Convert.ToDecimal(grdCGSTPER.Text.Trim());
                                                dr["CGSTAMNT"] = Convert.ToDecimal(grdCGSTAMNT.Text.Trim());

                                                dr["SGSTID"] = Convert.ToString(grdSGSTID.Text.Trim());
                                                dr["SGSTPER"] = Convert.ToDecimal(grdSGSTPER.Text.Trim());
                                                dr["SGSTAMNT"] = Convert.ToDecimal(grdSGSTAMNT.Text.Trim());

                                                dr["IGSTID"] = Convert.ToString(grdIGSTID.Text.Trim());
                                                dr["IGSTPER"] = Convert.ToDecimal(grdIGSTPER.Text.Trim());
                                                dr["IGSTAMNT"] = Convert.ToDecimal(grdIGSTAMNT.Text.Trim());
                                                dr["VALUEWITHTAX"] = Convert.ToDecimal(grdVALUEWITHTAX.Text.Trim());

                                                dtPurchaseOrderDetails.Rows.Add(dr);
                                                dtPurchaseOrderDetails.AcceptChanges();
                                            }
                                        }
                                        else
                                        {
                                            //if (grdtxtdeliverydatefrom.Text.Trim() != "" && grdtxtdeliverydateto.Text.Trim() != "" && dtpoEntrydate <= dtpofromdate
                                            // && dtpofromdate <= dtpotodate)
                                            {

                                                DataRow dr = dtPurchaseOrderDetails.NewRow();
                                                dr["DIVISIONID"] = Convert.ToString(grdlblDIVISIONID.Text.Trim());
                                                dr["DIVISIONNAME"] = Convert.ToString(grdlblDIVISIONNAME.Text.Trim());
                                                dr["CATEGORYID"] = Convert.ToString(grdlblCATEGORYID.Text.Trim());
                                                dr["CATEGORYNAME"] = Convert.ToString(grdlblCATEGORYNAME.Text.Trim());
                                                dr["NATUREOFPRODUCTID"] = Convert.ToString(grdlblNATUREOFPRODUCTID.Text.Trim());
                                                dr["NATUREOFPRODUCTNAME"] = Convert.ToString(grdlblNATUREOFPRODUCTNAME.Text.Trim());
                                                dr["UOMID"] = Convert.ToString(grdlblUOMID.Text.Trim());
                                                dr["UOMNAME"] = Convert.ToString(grdlblUOMNAME.Text.Trim());
                                                dr["PRODUCTID"] = Convert.ToString(grdlblproductid.Text.Trim());
                                                dr["PRODUCTNAME"] = Convert.ToString(grdlblproductname.Text.Trim());
                                                dr["PRODUCTQTY"] = Convert.ToString(grdtxtpoqty.Text.Trim());
                                                dr["DISCPER"] = Convert.ToString(grdTxtDiscPer.Text.Trim());//NEW ADD
                                                dr["DISCAMNT"] = Convert.ToString(grdTxtDiscAmnt.Text.Trim());//NEW ADD
                                                dr["PRODUCTPACKINGSIZEID"] = Convert.ToString(grdlblpacksizeid.Text.Trim());
                                                dr["PRODUCTPACKINGSIZE"] = Convert.ToString(grdlblpacksizename.Text.Trim());
                                                dr["PRODUCTCONVERSIONQTY"] = Convert.ToString(hdn_convertionqty.Value.Trim());
                                                dr["PRODUCTPRICE"] = Convert.ToString(grdlblpurchasecost.Text.Trim());
                                                dr["PRODUCTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdlblbasiccostvalue.Text.Trim()));
                                                dr["MRP"] = 0;
                                                dr["MRPVALUE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrpvalue.Text.Trim()));
                                                dr["ASSESMENTPERCENTAGE"] = Convert.ToString(String.Format("{0:0.00}", grdlblassesmentpercent.Text.Trim()));
                                                dr["EXCISE"] = Convert.ToString(String.Format("{0:0.00}", grdlblexcise.Text.Trim()));
                                                dr["CST"] = Convert.ToString(String.Format("{0:0.00}", grdlblcst.Text.Trim()));
                                                dr["REQUIREDDATE"] = Convert.ToString(grdtxtdeliverydatefrom.Text.Trim());
                                                dr["REQUIREDTODATE"] = Convert.ToString(grdtxtdeliverydateto.Text.Trim());
                                                dr["MODE"] = "E";
                                                dr["LASTRATE"] = Convert.ToString(String.Format("{0:0.00}", grdlblmrp.Text.Trim()));


                                                dr["CGSTID"] = Convert.ToString(grdCGSTID.Text.Trim());
                                                dr["CGSTPER"] = Convert.ToDecimal(grdCGSTPER.Text.Trim());
                                                dr["CGSTAMNT"] = Convert.ToDecimal(grdCGSTAMNT.Text.Trim());

                                                dr["SGSTID"] = Convert.ToString(grdSGSTID.Text.Trim());
                                                dr["SGSTPER"] = Convert.ToDecimal(grdSGSTPER.Text.Trim());
                                                dr["SGSTAMNT"] = Convert.ToDecimal(grdSGSTAMNT.Text.Trim());

                                                dr["IGSTID"] = Convert.ToString(grdIGSTID.Text.Trim());
                                                dr["IGSTPER"] = Convert.ToDecimal(grdIGSTPER.Text.Trim());
                                                dr["IGSTAMNT"] = Convert.ToDecimal(grdIGSTAMNT.Text.Trim());
                                                dr["VALUEWITHTAX"] = Convert.ToDecimal(grdVALUEWITHTAX.Text.Trim());

                                                dtPurchaseOrderDetails.Rows.Add(dr);
                                                dtPurchaseOrderDetails.AcceptChanges();
                                            }
                                        }
                                    }
                                }

                            }
                            #endregion

                            #region Amount Calculation
                            if (dtPurchaseOrderDetails.Rows.Count > 0)
                            {
                                GrossTotal = CalculateTotalAmount(dtPurchaseOrderDetails);
                                NetTotal = GrossTotal;
                                TotalMRP = CalculateTotalMRP(dtPurchaseOrderDetails);
                                TotalExcise = CalculateTotalExcise(dtPurchaseOrderDetails);
                                TotalCST = ((GrossTotal + TotalExcise) * (Convert.ToDecimal(dtPurchaseOrderDetails.Rows[0]["CST"]) / 100));

                                txtgrosstotal.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                                txtBasicAmnt.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                                txtTotalMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                                txtexercise.Text = Convert.ToString(String.Format("{0:0.00}", TotalExcise));
                                txtsaletax.Text = Convert.ToString(String.Format("{0:0.00}", TotalCST));
                                txttotalamount.Text = Convert.ToString(String.Format("{0:0.00}", NetTotal));
                            }

                            #endregion

                            #region Total Case
                            txtcasepack.Text = Convert.ToString(TotalCasePack(hdn_pofield.Value.ToString()));
                            #endregion
                        }
                        else
                        {
                            ProductName = grdlblproductname.Text.Trim();
                            STATUS = 5;
                            break;
                        }
                    }

                }

                if (STATUS == 5)
                {

                    MessageBox1.ShowInfo("<b><font color='red'>Record already exists</font> for </br></br><font color='green'> " + ProductName + "</font><font color='red'></font></b>", 60, 650);
                    if (dtPurchaseOrderDetails.Rows.Count > 0)
                    {
                        this.gvPurchaseOrder.DataSource = dtPurchaseOrderDetails;
                        this.gvPurchaseOrder.DataBind();
                    }
                    else
                    {

                        this.gvPurchaseOrder.DataSource = null;
                        this.gvPurchaseOrder.DataBind();
                    }
                    var rows = grdpodetailsadd.Rows;
                    foreach (GridViewRow row in rows)
                    {
                        TextBox t = (TextBox)row.FindControl("grdtxtpoqty");
                        t.Text = "0";
                        TextBox t1 = (TextBox)row.FindControl("grdTxtDiscPer");
                        t1.Text = "0";
                        TextBox t2 = (TextBox)row.FindControl("grdTxtDiscAmnt");
                        TextBox t3 = (TextBox)row.FindControl("grdlblbasiccostvalue");
                        t2.Text = "0";
                        t3.Text = "0";

                    }
                    return;
                }
                if (dtPurchaseOrderDetails.Rows.Count > 0)
                {
                    this.gvPurchaseOrder.DataSource = dtPurchaseOrderDetails;
                    this.gvPurchaseOrder.DataBind();
                    var rows = grdpodetailsadd.Rows;
                    foreach (GridViewRow row in rows)
                    {
                        TextBox t = (TextBox)row.FindControl("grdtxtpoqty");
                        t.Text = "0";
                        TextBox t1 = (TextBox)row.FindControl("grdTxtDiscPer");
                        t1.Text = "0";
                        TextBox t2 = (TextBox)row.FindControl("grdTxtDiscAmnt");
                        TextBox t3 = (TextBox)row.FindControl("grdlblbasiccostvalue");
                        t2.Text = "0";
                        t3.Text = "0";

                    }
                }
                else
                {
                    this.gvPurchaseOrder.DataSource = null;
                    this.gvPurchaseOrder.DataBind();
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

    #region TotalCasePack
    public decimal TotalCasePack(string hdnvalue)
    {
        decimal casepack = 0;
        decimal convertioncasepack = 0;
        ClsPoOrder clspurchaseorder = new ClsPoOrder();


        DataTable dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDSV2"];
        foreach (DataRow dr in dtPORecord.Rows)
        {
            String[] productid = dr["PRODUCTID"].ToString().Trim().Split('#');
            string pid = productid[0].Trim();/*for total qty if case or pcs will be separate then open the comment section by p.basu on 17032020*/
            //if (this.ddlpacksize.SelectedValue.Trim() != "B1D2969B-78D3-43A6-A26F-70E702203EF0")
            //{
            //    convertioncasepack = clspurchaseorder.GetPackingSize_OnCall(pid, dr["PRODUCTPACKINGSIZEID"].ToString().Trim(), Convert.ToDecimal(dr["PRODUCTQTY"].ToString().Trim()));
            //}
            //else
            //{
            convertioncasepack = Convert.ToDecimal(dr["PRODUCTQTY"].ToString().Trim());
            //}
            casepack = casepack + convertioncasepack;
        }


        return casepack;
    }
    #endregion

    #region DeleteRecordPoDetails
    protected void DeleteRecordPoDetails(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string pono = e.Record["PONO"].ToString();
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            if (clspurchaseorder.GetPUstatus(pono) == "1")
            {
                e.Record["Error"] = "Producion Update already done,not allow to delete record!";
                return;

            }

            int flag = 0;
            flag = clspurchaseorder.POEditedRecordDELETE_KKG(e.Record["PONO"].ToString(), Session["FINYEAR"].ToString());

            if (flag == 1)
            {
                LoadPO(Label);
                e.Record["Error"] = "Record Deleted Successfully!";
            }
            else
            {
                e.Record["Error"] = "Error On Deleting!";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Edit Purchase Order
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.chkDuplicatepo.Checked = false;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            DataSet ds = new DataSet();
            string POID = hdn_pofield.Value.ToString().Trim();
            string ISCLOSED = hdn_pofield1.Value.ToString().Trim();
            string status = string.Empty;
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            Label = Request.QueryString["LABEL"].ToString().Trim();
            ViewState["Checker"] = Checker;
            status = clspurchaseorder.FetchPoStatus(POID);
            if (Checker == "TRUE")
            {

                this.divProductDetails.Visible = true;
                this.btnadd.Visible = true;
                this.divbtnapprove.Style["display"] = "none";
                this.divBtnConfirm.Style["display"] = "none";
                this.divbtnreject.Style["display"] = "none";

                //status = clspurchaseorder.FetchPoStatus(POID);
                //{
                if (status == "N" || status == "R" || status == "H")
                {
                    this.divbtnsave.Style["display"] = "";
                    this.divbtnHoldsave.Style["display"] = "";
                }
                else
                {
                    this.divbtnsave.Style["display"] = "none";
                    this.divbtnHoldsave.Style["display"] = "none";
                }
                //}
            }
            else
            {
                if (Label == "POC-1")
                {
                    if (status == "N")
                    {
                        this.divProductDetails.Visible = false;
                        this.btnadd.Visible = false;
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "";
                        this.divbtnreject.Style["display"] = "";
                    }
                    else
                    {
                        this.divProductDetails.Visible = false;
                        this.btnadd.Visible = false;
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "none";
                        this.divbtnreject.Style["display"] = "none";
                    }

                }
                else if (Label == "POC-2")
                {
                    if (status == "N" || status == "C")
                    {
                        this.divProductDetails.Visible = false;
                        this.btnadd.Visible = false;
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "";
                        this.divbtnreject.Style["display"] = "";
                    }
                    else
                    {
                        this.divProductDetails.Visible = false;
                        this.btnadd.Visible = false;
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                        this.divbtnapprove.Style["display"] = "none";
                        this.divBtnConfirm.Style["display"] = "none";
                        this.divbtnreject.Style["display"] = "none";
                    }

                }
            }


            if (clspurchaseorder.GetPUstatusV2(POID) == "1")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Producion Update already done,not allow to edit!.</font></b>", 50, 450);
                return;
            }

            else
            {

                if (this.hdn_pofield1.Value.ToString().Trim() == "CLOSED")
                {
                    MessageBox1.ShowInfo("<b><font color='red'>PO Closed,not allow to edit!.</font></b>", 50, 450);
                    return;
                }

                else
                {

                    ds = clspurchaseorder.BindPurchaseOrderDetailskkg(POID);

                    #region Header Information
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["PLANNED"].ToString().Trim() == "N")
                        {
                            this.LoadTPUName();
                            this.rdbNoPlanned.Checked = true;
                            this.rdbPlanned.Checked = false;
                            this.tdLable.Visible = false;
                            this.tdControl.Visible = false;
                            this.tdDate.Visible = false;
                            this.tdCalenderFrom.Visible = false;
                            this.tdCalenderTo.Visible = false;
                            this.tdReqSearch.Visible = false;
                        }
                        else
                        {
                            this.LoadTPUName();
                            this.rdbNoPlanned.Checked = false;
                            this.rdbPlanned.Checked = true;
                            this.tdLable.Visible = true;
                            this.tdControl.Visible = true;
                            this.tdDate.Visible = true;
                            this.tdCalenderFrom.Visible = true;
                            this.tdCalenderTo.Visible = true;
                            this.tdReqSearch.Visible = true;
                            this.ddlReqNo.Items.Clear();
                            this.ddlReqNo.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["REQNO"]).Trim(), Convert.ToString(ds.Tables[0].Rows[0]["REQID"]).Trim()));
                        }

                        this.LoadPacksize();
                        this.ddlTPUName.SelectedValue = ds.Tables[0].Rows[0]["TPUID"].ToString().Trim();

                        this.txtpodate.Text = ds.Tables[0].Rows[0]["PODATE"].ToString().Trim();
                        this.txtcasepack.Text = ds.Tables[0].Rows[0]["TOTALCASEPACK"].ToString().Trim();
                        this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString().Trim();
                        this.txtpono.Text = ds.Tables[0].Rows[0]["PONO"].ToString().Trim();
                        this.txtreqfromdateset.Text = ds.Tables[0].Rows[0]["REQUIREDDATE"].ToString().Trim();
                        this.txtreqtotodateset.Text = ds.Tables[0].Rows[0]["REQUIREDTODATE"].ToString().Trim();
                        this.txtPaymentTerms.Text = ds.Tables[0].Rows[0]["PAYMENTTERMS"].ToString().Trim();
                        if (ds.Tables[0].Rows[0]["PLANNED"].ToString().Trim() == "")
                        {
                            this.BindProductDetails(this.ddlTPUName.SelectedValue.Trim()
                                                   );
                        }
                        else
                        {
                            this.RequirementProductDetails(this.ddlTPUName.SelectedValue.Trim(), this.ddlpacksize.SelectedValue.Trim(),
                                                            this.ddlReqNo.SelectedValue.Trim(), ds.Tables[0].Rows[0]["PODATE"].ToString().Trim()
                                                  );
                        }
                        if (ds.Tables[0].Rows[0]["FILEUPLOADTAG"].ToString().Trim() == "Y")
                        {
                            chkfileupload.Checked = true;
                            divshow.Style["display"] = "";
                        }
                        else
                        {
                            chkfileupload.Checked = false;
                            divshow.Style["display"] = "none";
                        }

                        if (ds.Tables[0].Rows[0]["EXTRAFREIGHT"].ToString().Trim() == "Y")
                        {
                            CheckBox2.Checked = true;
                        }
                        else
                        {
                            CheckBox2.Checked = false;
                        }
                        //if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                        //{
                        //    this.grdpodetailsadd.HeaderRow.Cells[8].Text = "TRANSFER RATE";
                        //}
                        //else
                        //{
                        //    this.grdpodetailsadd.HeaderRow.Cells[8].Text = "PURCHASE COST";
                        //}
                    }
                    #endregion

                    #region Details Information

                    this.BindPurchaseOrderGridStructure();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dtPOEdit = (DataTable)Session["PORECORDSV2"];
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            DataRow drEditPO = dtPOEdit.NewRow();
                            drEditPO["DIVISIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONID"]);
                            drEditPO["DIVISIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONNAME"]);
                            drEditPO["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                            drEditPO["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                            drEditPO["NATUREOFPRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTID"]);
                            drEditPO["NATUREOFPRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTNAME"]);
                            drEditPO["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                            drEditPO["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                            drEditPO["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                            drEditPO["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                            drEditPO["PRODUCTCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCODE"]);
                            drEditPO["PRODUCTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTQTY"]);
                            drEditPO["PRODUCTPACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZEID"]);
                            drEditPO["PRODUCTPACKINGSIZE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZE"]);
                            drEditPO["PRODUCTCONVERSIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCONVERSIONQTY"]);
                            drEditPO["PRODUCTPRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPRICE"]);
                            drEditPO["PRODUCTAMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTAMOUNT"]);
                            drEditPO["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                            drEditPO["MRPVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["MRPVALUE"]);
                            drEditPO["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                            drEditPO["EXCISE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXCISE"]);
                            drEditPO["CST"] = Convert.ToString(ds.Tables[1].Rows[i]["CST"]);
                            drEditPO["REQUIREDDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDDATE"]);
                            drEditPO["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                            drEditPO["DISCPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCPER"]);
                            drEditPO["DISCAMNT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCAMNT"]);
                            drEditPO["MODE"] = "V";
                            drEditPO["LASTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["LASTRATE"]);

                            drEditPO["CGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["CGSTID"]);
                            drEditPO["CGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTPER"]);
                            drEditPO["CGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTAMNT"]);
                            drEditPO["SGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["SGSTID"]);
                            drEditPO["SGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTPER"]);
                            drEditPO["SGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTAMNT"]);
                            drEditPO["IGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["IGSTID"]);
                            drEditPO["IGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTPER"]);
                            drEditPO["IGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTAMNT"]);
                            drEditPO["VALUEWITHTAX"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["VALUEWITHTAX"]);


                            dtPOEdit.Rows.Add(drEditPO);
                            dtPOEdit.AcceptChanges();
                        }


                        HttpContext.Current.Session["PORECORDSV2"] = dtPOEdit;
                        this.gvPurchaseOrder.DataSource = dtPOEdit;
                        this.gvPurchaseOrder.DataBind();

                        //if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                        //{
                        //    this.gvPurchaseOrder.HeaderRow.Cells[5].Text = "TRANSFER RATE";
                        //}
                        //else
                        //{
                        //    this.gvPurchaseOrder.HeaderRow.Cells[].Text = "PURCHASE COST";
                        //}

                    }
                    else
                    {
                        this.gvPurchaseOrder.DataSource = null;
                        this.gvPurchaseOrder.DataBind();
                    }
                    #endregion

                    #region Footer Information
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.txtgrosstotal.Text = ds.Tables[2].Rows[0]["GROSSTOTAL"].ToString();
                        this.txtTotalMRP.Text = ds.Tables[2].Rows[0]["MRPTOTAL"].ToString();
                        this.txtFreightAmnt.Text = ds.Tables[2].Rows[0]["ADJUSTMENT"].ToString();
                        this.txtexercise.Text = ds.Tables[2].Rows[0]["EXERCISE"].ToString();
                        this.txtsaletax.Text = ds.Tables[2].Rows[0]["CST"].ToString();
                        this.txttotalamount.Text = ds.Tables[2].Rows[0]["TOTALAMOUNT"].ToString();
                        this.txtDiscountAmnt.Text = ds.Tables[2].Rows[0]["DISCOUNT"].ToString();
                        this.txtItemWiseTotalDisc.Text = ds.Tables[2].Rows[0]["ITEMWISETOTALDISCAMNT"].ToString();
                        this.txtOtherCharges.Text = ds.Tables[2].Rows[0]["OTHERCHARGES"].ToString();
                        decimal basicAmnt = Convert.ToDecimal(this.txtgrosstotal.Text) + Convert.ToDecimal(this.txtDiscountAmnt.Text) + Convert.ToDecimal(this.txtFreightAmnt.Text);
                        this.txtBasicAmnt.Text = basicAmnt.ToString();
                        decimal finalAmnt = Convert.ToDecimal(this.txtgrosstotal.Text) + Convert.ToDecimal(this.txtOtherCharges.Text);
                        this.txtgrosstotal.Text = Convert.ToString(finalAmnt);
                    }
                    #endregion
                }
            }

            this.trscheduledate.Style["display"] = "";
            this.ddlTPUName.Enabled = false;
            this.txtpodate.Enabled = false;
            this.div_btnPrint.Style["display"] = "none";
            this.divProductDetails.Style["display"] = "";
            this.divpono.Style["display"] = "";
            this.divponoheader.Style["display"] = "";
            this.btnsave.Enabled = true;
            this.ddlTPUName.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.btnsave.Visible = true;
            this.btncancel.Visible = true;
            this.tblADD.Style["display"] = "";

            this.txtremarks.Enabled = true;
            this.gvPurchaseOrder.Columns[22].Visible = true;
            this.imgPopuppodate.Visible = true;
            this.ddlReqNo.Enabled = false;
            this.btnReqSearch.Enabled = false;
            this.rdbPlanned.Enabled = false;
            this.rdbNoPlanned.Enabled = false;

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region View Purchase Order
    protected void btngrdview_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            DataSet ds = new DataSet();
            string POID = hdn_pofield.Value.ToString().Trim();
            string ISCLOSED = hdn_pofield1.Value.ToString().Trim();
            string status = string.Empty;
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            Label = Request.QueryString["LABEL"].ToString().Trim();
            ViewState["Checker"] = Checker;
            if (Checker == "TRUE")
            {

                this.divProductDetails.Visible = true;
                this.btnadd.Visible = true;
                this.divbtnapprove.Style["display"] = "none";
                this.divBtnConfirm.Style["display"] = "none";
                this.divbtnreject.Style["display"] = "none";

                status = clspurchaseorder.FetchPoStatus(POID);
                {
                    if (status == "N" || status == "R")
                    {
                        this.divbtnsave.Style["display"] = "";
                        this.divbtnHoldsave.Style["display"] = "";
                    }
                    else
                    {
                        this.divbtnsave.Style["display"] = "none";
                        this.divbtnHoldsave.Style["display"] = "none";
                    }
                }
            }
            else
            {
                if (Label == "POC-1")
                {
                    this.divProductDetails.Visible = false;
                    this.btnadd.Visible = false;
                    this.divbtnsave.Style["display"] = "none";
                    this.divbtnHoldsave.Style["display"] = "none";
                    this.divBtnConfirm.Style["display"] = "none";
                    this.divbtnapprove.Style["display"] = "";
                    this.divbtnreject.Style["display"] = "";
                }
                else if (Label == "POC-2")
                {
                    this.divProductDetails.Visible = false;
                    this.btnadd.Visible = false;
                    this.divbtnsave.Style["display"] = "none";
                    this.divbtnHoldsave.Style["display"] = "none";
                    this.divbtnapprove.Style["display"] = "none";
                    this.divBtnConfirm.Style["display"] = "";
                    this.divbtnreject.Style["display"] = "";
                }
            }


            ds = clspurchaseorder.BindPurchaseOrderDetailskkg(POID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PLANNED"].ToString().Trim() == "N")
                {
                    this.LoadTPUName();
                    this.rdbNoPlanned.Checked = true;
                    this.rdbPlanned.Checked = false;
                    this.tdLable.Visible = false;
                    this.tdControl.Visible = false;
                    this.tdDate.Visible = false;
                    this.tdCalenderFrom.Visible = false;
                    this.tdCalenderTo.Visible = false;
                    this.tdReqSearch.Visible = false;
                }
                else
                {
                    this.LoadTPUName();
                    this.rdbNoPlanned.Checked = false;
                    this.rdbPlanned.Checked = true;
                    this.tdLable.Visible = true;
                    this.tdControl.Visible = true;
                    this.tdDate.Visible = true;
                    this.tdCalenderFrom.Visible = true;
                    this.tdCalenderTo.Visible = true;
                    this.tdReqSearch.Visible = true;
                    this.ddlReqNo.Items.Clear();
                    this.ddlReqNo.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["REQNO"]).Trim(), Convert.ToString(ds.Tables[0].Rows[0]["REQID"]).Trim()));
                }
                if (ds.Tables[0].Rows[0]["FILEUPLOADTAG"].ToString().Trim() == "Y")
                {
                    chkfileupload.Checked = true;
                    divshow.Style["display"] = "";
                }
                else
                {
                    chkfileupload.Checked = false;
                    divshow.Style["display"] = "none";
                }
                this.ddlTPUName.SelectedValue = ds.Tables[0].Rows[0]["TPUID"].ToString().Trim();

                this.txtpodate.Text = ds.Tables[0].Rows[0]["PODATE"].ToString().Trim();
                this.txtcasepack.Text = ds.Tables[0].Rows[0]["TOTALCASEPACK"].ToString().Trim();
                this.txtremarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString().Trim();
                this.txtpono.Text = ds.Tables[0].Rows[0]["PONO"].ToString().Trim();
                this.txtPaymentTerms.Text = ds.Tables[0].Rows[0]["PAYMENTTERMS"].ToString().Trim();

                if (ds.Tables[0].Rows[0]["EXTRAFREIGHT"].ToString().Trim() == "Y")
                {
                    CheckBox2.Checked = true;
                }
                else
                {
                    CheckBox2.Checked = false;
                }
            }
            #endregion

            #region Details Information

            this.BindPurchaseOrderGridStructure();
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtPOEdit = (DataTable)Session["PORECORDSV2"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow drEditPO = dtPOEdit.NewRow();
                    drEditPO["DIVISIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONID"]);
                    drEditPO["DIVISIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["DIVISIONNAME"]);
                    drEditPO["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                    drEditPO["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                    drEditPO["NATUREOFPRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTID"]);
                    drEditPO["NATUREOFPRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["NATUREOFPRODUCTNAME"]);
                    drEditPO["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                    drEditPO["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                    drEditPO["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                    drEditPO["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                    drEditPO["PRODUCTCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCODE"]);
                    drEditPO["PRODUCTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTQTY"]);
                    drEditPO["PRODUCTPACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZEID"]);
                    drEditPO["PRODUCTPACKINGSIZE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPACKINGSIZE"]);
                    drEditPO["PRODUCTCONVERSIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTCONVERSIONQTY"]);
                    drEditPO["PRODUCTPRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTPRICE"]);
                    drEditPO["PRODUCTAMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTAMOUNT"]);
                    drEditPO["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                    drEditPO["MRPVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["MRPVALUE"]);
                    drEditPO["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                    drEditPO["EXCISE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXCISE"]);
                    drEditPO["CST"] = Convert.ToString(ds.Tables[1].Rows[i]["CST"]);
                    drEditPO["REQUIREDDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDDATE"]);
                    drEditPO["REQUIREDTODATE"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUIREDTODATE"]);
                    drEditPO["DISCPER"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCPER"]);
                    drEditPO["DISCAMNT"] = Convert.ToString(ds.Tables[1].Rows[i]["DISCAMNT"]);
                    drEditPO["LASTRATE"] = Convert.ToString(ds.Tables[1].Rows[i]["LASTRATE"]);

                    drEditPO["CGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["CGSTID"]);
                    drEditPO["CGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTPER"]);
                    drEditPO["CGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTAMNT"]);
                    drEditPO["SGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["SGSTID"]);
                    drEditPO["SGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTPER"]);
                    drEditPO["SGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTAMNT"]);
                    drEditPO["IGSTID"] = Convert.ToString(ds.Tables[1].Rows[i]["IGSTID"]);
                    drEditPO["IGSTPER"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTPER"]);
                    drEditPO["IGSTAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTAMNT"]);
                    drEditPO["VALUEWITHTAX"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["VALUEWITHTAX"]);
                    drEditPO["MODE"] = "V";
                    dtPOEdit.Rows.Add(drEditPO);
                    dtPOEdit.AcceptChanges();
                }
                #region grdAddDespatch DataBind
                HttpContext.Current.Session["PORECORDSV2"] = dtPOEdit;
                this.gvPurchaseOrder.DataSource = dtPOEdit;
                this.gvPurchaseOrder.DataBind();

                if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                {
                    this.gvPurchaseOrder.HeaderRow.Cells[5].Text = "TRANSFER RATE";
                }
                else
                {
                    this.gvPurchaseOrder.HeaderRow.Cells[5].Text = "DISC(%)";
                }
                #endregion

            }
            else
            {
                this.gvPurchaseOrder.DataSource = null;
                this.gvPurchaseOrder.DataBind();
            }
            #endregion

            #region Footer Information
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.txtgrosstotal.Text = ds.Tables[2].Rows[0]["GROSSTOTAL"].ToString();
                this.txtTotalMRP.Text = ds.Tables[2].Rows[0]["MRPTOTAL"].ToString();
                this.txtFreightAmnt.Text = ds.Tables[2].Rows[0]["ADJUSTMENT"].ToString();
                this.txtexercise.Text = ds.Tables[2].Rows[0]["EXERCISE"].ToString();
                this.txtsaletax.Text = ds.Tables[2].Rows[0]["CST"].ToString();
                this.txttotalamount.Text = ds.Tables[2].Rows[0]["TOTALAMOUNT"].ToString();
                this.txtDiscountAmnt.Text = ds.Tables[2].Rows[0]["DISCOUNT"].ToString();
                this.txtItemWiseTotalDisc.Text = ds.Tables[2].Rows[0]["ITEMWISETOTALDISCAMNT"].ToString();
                decimal basicAmnt = Convert.ToDecimal(this.txtgrosstotal.Text) + Convert.ToDecimal(this.txtDiscountAmnt.Text) + Convert.ToDecimal(this.txtFreightAmnt.Text);
                this.txtBasicAmnt.Text = basicAmnt.ToString();

            }
            #endregion

            this.trscheduledate.Style["display"] = "none";
            this.divbtnsave.Style["display"] = "none";
            this.divbtnHoldsave.Style["display"] = "none";
            this.div_btnPrint.Style["display"] = "none";
            this.divProductDetails.Style["display"] = "none";
            this.divpono.Style["display"] = "";
            this.divponoheader.Style["display"] = "";
            this.ddlTPUName.Enabled = false;
            this.txtpodate.Enabled = false;
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.tblADD.Style["display"] = "none";
            this.btnsave.Visible = true;
            this.btncancel.Visible = true;
            this.imgPopuppodate.Visible = false;

            this.txtremarks.Enabled = false;
            this.gvPurchaseOrder.Columns[22].Visible = false;
            this.ddlReqNo.Enabled = false;
            this.btnReqSearch.Enabled = false;
            this.rdbPlanned.Enabled = false;
            this.rdbNoPlanned.Enabled = false;


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btn_TempDelete_Click
    protected void btn_TempDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblproductid1 = gvr.Cells[1].Controls[0].FindControl("grdlblproductid1") as Label;
            Label grdlblFromDate1 = gvr.Cells[12].Controls[0].FindControl("grdlblFromDate1") as Label;
            Label grdlblToDate1 = gvr.Cells[13].Controls[0].FindControl("grdlblToDate1") as Label;

            this.hdn_podelete.Value = grdlblproductid1.Text.Trim();
            this.hdn_FromDate.Value = grdlblFromDate1.Text.Trim();
            this.hdn_ToDate.Value = grdlblToDate1.Text.Trim();


            string productid = hdn_podelete.Value.ToString().Trim();
            string FromDate = hdn_FromDate.Value.Trim();
            string ToDate = hdn_ToDate.Value.Trim();

            decimal GrossTotal = 0;
            decimal NetTotal = 0;
            decimal TotalMRP = 0;
            decimal TotalExcise = 0;
            decimal TotalCST = 0;
            DataTable dtdeletePurchaseOrderDetails = new DataTable();
            dtdeletePurchaseOrderDetails = (DataTable)Session["PORECORDSV2"];

            DataRow[] drr = dtdeletePurchaseOrderDetails.Select("PRODUCTID='" + productid + "' AND REQUIREDDATE = '" + FromDate + "' AND REQUIREDTODATE = '" + ToDate + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeletePurchaseOrderDetails.AcceptChanges();
            }

            HttpContext.Current.Session["PORECORDSV2"] = dtdeletePurchaseOrderDetails;


            #region Amount Calculation
            if (dtdeletePurchaseOrderDetails.Rows.Count > 0)
            {
                GrossTotal = CalculateTotalAmount(dtdeletePurchaseOrderDetails);
                NetTotal = GrossTotal;
                TotalMRP = CalculateTotalMRP(dtdeletePurchaseOrderDetails);
                TotalExcise = CalculateTotalExcise(dtdeletePurchaseOrderDetails);
                TotalCST = ((GrossTotal + TotalExcise) * (Convert.ToDecimal(dtdeletePurchaseOrderDetails.Rows[0]["CST"]) / 100));

                this.txtgrosstotal.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.txtTotalMRP.Text = Convert.ToString(String.Format("{0:0.00}", TotalMRP));
                this.txtexercise.Text = Convert.ToString(String.Format("{0:0.00}", TotalExcise));
                this.txtsaletax.Text = Convert.ToString(String.Format("{0:0.00}", TotalCST));
                this.txttotalamount.Text = Convert.ToString(String.Format("{0:0.00}", NetTotal));

                #region Total Case
                this.txtcasepack.Text = Convert.ToString(TotalCasePack(hdn_pofield.Value.ToString()));
                #endregion
            }
            else
            {
                this.txtgrosstotal.Text = "0";
                this.txtTotalMRP.Text = "0";
                this.txtexercise.Text = "0";
                this.txtsaletax.Text = "0";
                this.txttotalamount.Text = "0";
                this.txtcasepack.Text = "0";
            }

            #endregion

            if (dtdeletePurchaseOrderDetails.Rows.Count > 0)
            {
                gvPurchaseOrder.DataSource = dtdeletePurchaseOrderDetails;
                gvPurchaseOrder.DataBind();

                if (this.ddlTPUName.SelectedValue == "14857CFC-2450-4D52-B93A-486D9507A1BE" || this.ddlTPUName.SelectedValue == "FFC65354-AB46-4983-A67F-111486EC3D39")
                {
                    this.gvPurchaseOrder.HeaderRow.Cells[5].Text = "TRANSFER RATE";
                }
                else
                {
                    this.gvPurchaseOrder.HeaderRow.Cells[5].Text = "PURCHASE COST";
                    this.gvPurchaseOrder.HeaderRow.Cells[6].Text = "PURCHASE RATE";
                }
            }
            else
            {
                gvPurchaseOrder.DataSource = null;
                gvPurchaseOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Convert DataTable To XML
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

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            string PONo = string.Empty;
            string xml = string.Empty;
            string Planned = string.Empty;
            string ReqID = string.Empty;
            string ReqNo = string.Empty;
            DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["PORECORDSV2"];

            string fileuploadtag = string.Empty;
            string xmlupload = string.Empty;

            DataTable dtupload = new DataTable();
            if (chkfileupload.Checked)
            {
                dtupload = (DataTable)(Session["UPLOADFILENAME"]);
                xmlupload = ConvertDatatableToXML(dtupload);
                fileuploadtag = "Y";
            }
            else
            {
                fileuploadtag = "N";
                // clsmmpp.FileDelete(hdnqcid.Value.Trim());
            }

            if (dtRecordsCheck.Rows.Count > 0)
            {
                if (Session["PORECORDSV2"] != null)
                {
                    xml = ConvertDatatableToXML(dtRecordsCheck);
                }
                if (gvPurchaseOrder.Rows.Count > 0)
                {
                    if (rdbNoPlanned.Checked == true)
                    {
                        Planned = "N";
                        ReqID = "0";
                        ReqNo = "NA";
                    }
                    else
                    {
                        Planned = "Y";
                        ReqID = this.ddlReqNo.SelectedValue.Trim();
                        ReqNo = this.ddlReqNo.SelectedItem.ToString().Trim();
                    }
                    string delivaryFromDate = this.txtreqfromdateset.Text.ToString();
                    string delivaryToDate = this.txtreqtotodateset.Text.ToString();
                    if (string.IsNullOrEmpty(this.txtreqfromdateset.Text))
                    {
                        MessageBox1.ShowWarning("Delivery from Date Should not be blank!!");
                        return;
                    }
                    if (string.IsNullOrEmpty(this.txtreqtotodateset.Text))
                    {
                        MessageBox1.ShowWarning("Delivery to Date Should not be blank!!");
                        return;
                    }

                    int dtpoEntrydate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));
                    int dtFromDate = Convert.ToInt32(Conver_To_ISO(this.txtreqfromdateset.Text.Trim()));
                    int dtToDate = Convert.ToInt32(Conver_To_ISO(this.txtreqtotodateset.Text.Trim()));

                    if (dtFromDate < dtpoEntrydate)
                    {
                        MessageBox1.ShowWarning("Delivary From Date " + this.txtreqfromdateset.Text.Trim() + " <br/> cannot be less than Entrydate " + this.txtpodate.Text.Trim() + " Please Check your dates");
                        return;
                    }
                    else if (dtFromDate > dtToDate)
                    {
                        MessageBox1.ShowWarning("Delivary From Date " + this.txtreqfromdateset.Text.Trim() + " <br/> cannot be greater than Delivary Todate " + this.txtreqtotodateset.Text.Trim() + " Please Check your dates");
                        return;
                    }
                    string extraFreight = string.Empty;
                    if (this.CheckBox2.Checked)
                    {
                        extraFreight = "Y";
                    }
                    else
                    {
                        extraFreight = "N";
                    }

                    if (this.chkDuplicatepo.Checked == true)
                    {
                        this.hdn_pofield.Value = "newpo" + this.hdn_pofield.Value;
                    }

                    decimal adjustmentAmnt = Convert.ToDecimal(this.txtOtherCharges.Text);

                    decimal discountAmnt = Convert.ToDecimal(this.txtDiscountAmnt.Text.ToString());
                    decimal freightAmnt = Convert.ToDecimal(this.txtFreightAmnt.Text.ToString());

                    string paymentTerms = this.txtPaymentTerms.Text;



                    PONo = clspurchaseorder.InsertMMPODetails(this.hdn_pofield.Value.ToString().Trim(), this.txtpodate.Text.Trim().Trim(),
                                                             this.ddlTPUName.SelectedValue.ToString().Trim(), this.ddlTPUName.SelectedItem.ToString().Trim(),
                                                             this.txtremarks.Text.Trim(), Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString().Trim()),
                                                             Convert.ToDecimal(this.txtgrosstotal.Text.Trim()), Convert.ToDecimal(this.txtexercise.Text.Trim()),
                                                             Convert.ToDecimal(this.txtsaletax.Text.Trim()), Convert.ToDecimal(this.txttotalamount.Text.Trim()),
                                                             HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                             Convert.ToDecimal(this.txtTotalMRP.Text.Trim()), Convert.ToDecimal(this.txtcasepack.Text.Trim()),
                                                             this.txtqutrefno.Text, this.txtqutrefno.Text, xml,
                                                             Planned, ReqID, ReqNo, HttpContext.Current.Session["DEPOTID"].ToString().Trim(), xmlupload, fileuploadtag, delivaryFromDate, delivaryToDate, discountAmnt, freightAmnt, extraFreight, "", adjustmentAmnt, paymentTerms);
                    if (PONo != "")
                    {
                        if (Convert.ToString(hdn_pofield.Value) == "")
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> saved Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            div_btnPrint.Style["display"] = "none";
                            this.divbtnsave.Style["display"] = "none";
                            this.divbtnHoldsave.Style["display"] = "none";
                            trscheduledate.Style["display"] = "none";
                            this.tblADD.Style["display"] = "";
                            ResetAll();
                            LoadPO(Label);

                        }
                        else
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> updated Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            this.divbtnsave.Style["display"] = "none";
                            this.divbtnHoldsave.Style["display"] = "none";
                            trscheduledate.Style["display"] = "none";
                            this.tblADD.Style["display"] = "none";
                            ResetAll();
                            LoadPO(Label);

                        }

                        this.grdpodetailsadd.DataSource = null;
                        this.grdpodetailsadd.DataBind();
                        //this.gvpodetails.DataSource = null;
                        //this.gvpodetails.DataBind();
                        this.divProductDetails.Style["display"] = "none";
                    }
                    else
                    {
                        this.divProductDetails.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please enter Purchase Order details.", 60, 450);
                    return;
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

    #region Clear Control
    public void clear()
    {
        try
        {
            this.ddlTPUName.SelectedValue = "0";
            this.ddlTPUName.Enabled = true;
            chkDuplicatepo.Checked = false;
            this.txtremarks.Text = "";
            this.txtgrosstotal.Text = "0";
            this.txtadjustment.Text = "0";
            this.txtTotalMRP.Text = "0";
            this.txtpacking.Text = "0";
            this.txtsaletax.Text = "0";
            this.txtexercise.Text = "0";
            this.txttotalamount.Text = "0";
            this.txtnettotal.Text = "0";
            this.txtgrosstotal.Text = "0";
            this.txtDiscountAmnt.Text = "0";
            this.txtcasepack.Text = "0";
            this.txtOtherCharges.Text = "0";
            this.btnnewentry.Visible = true;
            HttpContext.Current.Session["UPLOADFILENAME"] = null;
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            clspurchaseorder.ResetDataTables();   // Reset all Datatables
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
            LoadPO(Label);
            this.txtItemWiseTotalDisc.Text = "0";
            this.txtDiscountAmnt.Text = "0";
            this.txtFreightAmnt.Text = "0";
            btnsave.Enabled = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region BindProductDetails
    protected void BindProductDetails(string TPUID)
    {
        ClsPoOrder clspurchaseorder = new ClsPoOrder();
        DataTable dt = clspurchaseorder.FetchProductdetailsKKGNEW(TPUID);
        if (dt.Rows.Count > 0)
        {
            this.grdpodetailsadd.DataSource = dt;
            this.grdpodetailsadd.DataBind();
        }
        else
        {
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
        }
    }
    #endregion

    #region Bind Requirement ProductDetails
    protected void RequirementProductDetails(string TPUID, string PacksizeID, string ReqNo, string PODate)
    {
        ClsPoOrder obj = new ClsPoOrder();
        DataTable dt = obj.FetchRequirementProductdetails(TPUID, PacksizeID, ReqNo, PODate);
        if (dt.Rows.Count > 0)
        {
            this.grdpodetailsadd.DataSource = dt;
            this.grdpodetailsadd.DataBind();
        }
        else
        {
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
        }
    }
    #endregion

    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {
            Label = Request.QueryString["LABEL"].ToString().Trim();
            LoadPO(Label);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string upath = "frmRptPurchaseOrder.aspx?pid=" + hdn_pofield.Value;
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        //}
        //catch (Exception ex)
        //{
        //    string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //}
    }

    #region btnPrint_Click
    protected void btnPOPrint_Click(object sender, EventArgs e)
    {

        try
        {
            //    ClsTransporterBill clstranspoter = new ClsTransporterBill();
            //    ClsStockReport clsInvc = new ClsStockReport();
            //    DataSet dt = new DataSet();
            //    string upath = string.Empty;
            //    string StnId = string.Empty;
            //    string tag = Request.QueryString["TAG"];


            //    upath = "frmRptInvoicePrint.aspx?PurchaseOrderId=" + hdn_pofield.Value.Trim() + "&&TAG=PO&&MenuId=29";
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?PurchaseOrderId=" + hdn_pofield.Value.Trim() + "&&TAG=PO&&MenuId=29";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region rdbNoPlanned_CheckedChanged
    protected void rdbNoPlanned_CheckedChanged(object sender, EventArgs e)
    {
        this.tdLable.Visible = false;
        this.tdControl.Visible = false;
        this.tdDate.Visible = false;
        this.tdCalenderFrom.Visible = false;
        this.tdCalenderTo.Visible = false;
        this.tdReqSearch.Visible = false;
        this.LoadTPUName();

    }
    #endregion

    #region rdbPlanned_CheckedChanged
    protected void rdbPlanned_CheckedChanged(object sender, EventArgs e)
    {
        this.tdLable.Visible = true;
        this.tdControl.Visible = true;
        this.tdDate.Visible = true;
        this.tdCalenderFrom.Visible = true;
        this.tdCalenderTo.Visible = true;
        this.tdReqSearch.Visible = true;

    }
    #endregion

    #region btnReqSearch_Click
    protected void btnReqSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadRequirment();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void grdlblUOMNAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reference the DropDownList.
        ClsMMPoOrder clsmo = new ClsMMPoOrder();
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        int idx = row.RowIndex;
        Label txtProductId = (Label)row.FindControl("grdlblproductid");
        string _productid = txtProductId.Text.ToString();
        decimal purchaserate = 0;
        TextBox lblproductamt = (TextBox)row.FindControl("grdlblpurchasecost");
        TextBox txt = (TextBox)row.FindControl("grdlblpurchasecost");
        purchaserate = clsmo.GetPurchasecost(this.ddlTPUName.SelectedValue, _productid);
        if (purchaserate == 0)
        {
            MessageBox1.ShowWarning("Rate not set for this product in Ratesheetmaster");
            DropDownList abc = row.FindControl("grdlblUOMNAME") as DropDownList;
            abc.Enabled = false;
            return;
        }
        else
        {
            DropDownList abc = row.FindControl("grdlblUOMNAME") as DropDownList;
            abc.Enabled = true;

        }
        decimal Conver = 0;
        Conver = clsmo.Getconvertionqty(_productid);

        TextBox txtamnt = (TextBox)row.FindControl("grdlblbasiccostvalue");
        TextBox txtpoqty = (TextBox)row.FindControl("grdtxtpoqty");
        Label lbluomid = (Label)row.FindControl("grdlblUOMID");
        string uomid;
        uomid = Convert.ToString(lbluomid.Text);
        txtamnt.Text = "0";
        txtpoqty.Text = "0";
        decimal amount = 0;
        amount = Convert.ToDecimal(lblproductamt.Text);
        if (purchaserate != amount)
        {
            txt.Text = Convert.ToString(purchaserate);
        }
        else
        {
            decimal purchasecost = 0;
            purchasecost = amount / Conver;
            txt.Text = Convert.ToString(purchasecost);
        }

    }

    protected void btnapprove_Click(object sender, EventArgs e)/*mode 1 for confirm*/
    {
        try
        {
            Label = Request.QueryString["LABEL"].ToString().Trim();
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "1", "");
            this.hdn_pofield.Value = "";
            if (flag == 1)
            {
                MessageBox1.ShowSuccess("Purchase Order: <b><font color='green'>" + this.txtpono.Text + "</font></b> approved.", 60, 700);
                pnlDisplay.Style["display"] = "";
                InputTable.Style["display"] = "none";
                divpono.Style["display"] = "none";
                divadd.Style["display"] = "none";
                LoadPO(Label);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                //hdn_pofield.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e) /*mode 2 for reject*/
    {
        ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
        Label = Request.QueryString["LABEL"].ToString().Trim();
        int flag = 0;
        string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
        flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "2", "");
        this.hdn_pofield.Value = "";
        if (flag == 1)
        {
            MessageBox1.ShowSuccess("Purchase Order: <b><font color='red'>" + this.txtpono.Text + "</font></b> Rejected.", 60, 700);
            pnlDisplay.Style["display"] = "";
            InputTable.Style["display"] = "none";
            divpono.Style["display"] = "none";
            divadd.Style["display"] = "none";
            LoadPO(Label);
        }
        else if (flag == 0)
        {
            pnlDisplay.Style["display"] = "none";
            //hdn_pofield.Style["display"] = "";
            MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e) /*mode 3 for approved*/
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            Label = Request.QueryString["LABEL"].ToString().Trim();
            int flag = 0;
            string PurchaseID = Convert.ToString(hdn_pofield.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseOrderMM(PurchaseID, HttpContext.Current.Session["UserID"].ToString(), "3", "");
            this.hdn_pofield.Value = "";
            if (flag == 1)
            {
                MessageBox1.ShowSuccess("Purchase Order: <b><font color='green'>" + this.txtpono.Text + "</font></b> approved.", 60, 700);
                pnlDisplay.Style["display"] = "";
                InputTable.Style["display"] = "none";
                divpono.Style["display"] = "none";
                divadd.Style["display"] = "none";
                LoadPO(Label);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                //hdn_pofield.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void Search(object sender, EventArgs e) /*add by Subhendu on 17/11/2020*/
    {
        this.BindGrid();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpodetailsadd.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }



    protected void BindGrid()
    {
        string TPUID = string.Empty;
        TPUID = this.ddlTPUName.SelectedValue.ToString();

        ClsPoOrder clspurchaseorder = new ClsPoOrder();
        DataTable dt = clspurchaseorder.FetchSearchProductdetails(TPUID, txtSearchBox.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            this.grdpodetailsadd.DataSource = dt;
            this.grdpodetailsadd.DataBind();


        }
        else
        {
            this.grdpodetailsadd.DataSource = null;
            this.grdpodetailsadd.DataBind();
        }
    }

    protected void chkfileupload_check(object sender, EventArgs e)
    {
        try
        {
            if (chkfileupload.Checked == true)
            {
                Session["UPLOADFILENAME"] = null;
                string strPopup = string.Empty;
                if (this.hdn_pofield.Value.Trim() != "")
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmPOFileUpload_FAC.aspx?POID=" + hdn_pofield.Value.Trim() + ""
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                else
                {
                    strPopup = "<script language='javascript' ID='script2'>"
                    + "window.open('frmPOFileUpload_FAC.aspx?POID= "
                    + "','new window', 'top=200, left=700, width=600, height=450, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                    + "</script>";
                }
                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
                divshow.Style["display"] = "";
            }
            else
            {
                divshow.Style["display"] = "none";
                ClsPurchaseStockReceipt_FAC clsqc = new ClsPurchaseStockReceipt_FAC();
                clsqc.FileDelete(hdn_pofield.Value.Trim());
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            string strPopup = string.Empty;
            if (this.hdn_pofield.Value.Trim() != "")
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmPOFileUpload_FAC.aspx?POID=" + hdn_pofield.Value.Trim() + "&MODE=PO"
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            else
            {
                strPopup = "<script language='javascript' ID='script2'>"
                + "window.open('frmPOFileUpload_FAC.aspx?POID= "
                + "','new window', 'top=200, left=700, width=600, height=320, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
                + "</script>";
            }
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "script2", strPopup, false);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void txtDiscountAmnt_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.txtDiscountAmnt.Text))
        {
            this.txtDiscountAmnt.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtFreightAmnt.Text))
        {
            this.txtFreightAmnt.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtgrosstotal.Text))
        {
            this.txtgrosstotal.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtBasicAmnt.Text))
        {
            this.txtBasicAmnt.Text = "0";
        }
        decimal discount = Convert.ToDecimal(this.txtDiscountAmnt.Text.ToString());
        decimal freightAmnt = Convert.ToDecimal(this.txtFreightAmnt.Text.ToString());
        decimal basicAmnt = Convert.ToDecimal(this.txtBasicAmnt.Text.ToString());
        decimal NetAmnt = 0;
        NetAmnt = (basicAmnt - (discount + freightAmnt));
        this.txtgrosstotal.Text = Convert.ToString(NetAmnt);
        if (NetAmnt < 0)
        {
            MessageBox1.ShowWarning("Please Check Your NetAmount!!!");
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtFreightAmnt.ClientID + "').focus(); ", true);
    }

    protected void txtFreightAmnt_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.txtDiscountAmnt.Text))
        {
            this.txtDiscountAmnt.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtFreightAmnt.Text))
        {
            this.txtFreightAmnt.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtgrosstotal.Text))
        {
            this.txtgrosstotal.Text = "0";
        }
        if (string.IsNullOrEmpty(this.txtBasicAmnt.Text))
        {
            this.txtBasicAmnt.Text = "0";
        }
        decimal discount = Convert.ToDecimal(this.txtDiscountAmnt.Text.ToString());
        decimal freightAmnt = Convert.ToDecimal(this.txtFreightAmnt.Text.ToString());
        decimal basicAmnt = Convert.ToDecimal(this.txtBasicAmnt.Text.ToString());
        decimal NetAmnt = 0;
        NetAmnt = ((basicAmnt + freightAmnt) + -1 * (discount));
        this.txtgrosstotal.Text = Convert.ToString(NetAmnt);
        if (NetAmnt < 0)
        {
            MessageBox1.ShowWarning("Please Check Your NetAmount!!!");
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtremarks.ClientID + "').focus(); ", true);
    }


    #region grdpodetailsadd_RowDataBound
    protected void grdpodetailsadd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Style.Add("height", "50px");
                }

                if (rdbNoPlanned.Checked == true)
                {
                    Label lblpcsqty = (Label)e.Row.FindControl("grdlblpcsqty");
                    TextBox lblpurchasecost = (TextBox)e.Row.FindControl("grdlblpurchasecost");
                    Label lblproductid = (Label)e.Row.FindControl("grdlblproductid");
                    TextBox lblproductamt = (TextBox)e.Row.FindControl("grdlblbasiccostvalue");
                    TextBox poqty = (TextBox)e.Row.FindControl("grdtxtpoqty");
                    Label lastRate = (Label)e.Row.FindControl("grdlblmrp");
                    string productid = Convert.ToString(lblproductid.Text);

                    Double pcsqty = Convert.ToDouble(lblpcsqty.Text);
                    decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text);
                    decimal Rate = Convert.ToDecimal(lastRate.Text);


                    if (purchasecost != Rate)
                    {
                        TextBox t = (TextBox)e.Row.FindControl("grdlblpurchasecost");
                        t.BackColor = Color.LightCoral;
                    }

                    if (purchasecost == 0)
                    {

                        DropDownList abc = e.Row.FindControl("grdlblUOMNAME") as DropDownList;
                        abc.Enabled = false;

                    }
                    else
                    {
                        DropDownList abc = e.Row.FindControl("grdlblUOMNAME") as DropDownList;
                        abc.Enabled = true;

                    }

                    DropDownList grdlblUOMNAME = (e.Row.FindControl("grdlblUOMNAME") as DropDownList);
                    grdlblUOMNAME.Items.Clear();
                    //ddlpacksize.Items.Add(new ListItem("SELECT", "0"));
                    grdlblUOMNAME.AppendDataBoundItems = true;
                    ClsPoOrder clspurchaseorder = new ClsPoOrder();
                    grdlblUOMNAME.DataSource = clspurchaseorder.BindPacksizeProductwise(productid);
                    grdlblUOMNAME.DataValueField = "PACKSIZEID";
                    grdlblUOMNAME.DataTextField = "PACKSIZENAME";
                    grdlblUOMNAME.DataBind();



                    //poqty.Attributes.Add("onkeyup", "Calpurchaseorderdetails( " + pcsqty + "," + purchasecost + ",'" + poqty.ClientID + "'," + lblproductamt.ClientID + ");");
                    //poqty.Attributes.Add("onkeypress", "return isNumberKey('" + poqty.ClientID + "')");
                }
                else
                {
                    Label lblpcsqty = (Label)e.Row.FindControl("grdlblpcsqty");
                    TextBox lblpurchasecost = (TextBox)e.Row.FindControl("grdlblpurchasecost");
                    TextBox lblproductamt = (TextBox)e.Row.FindControl("grdlblbasiccostvalue");
                    TextBox txtpoqty = (TextBox)e.Row.FindControl("grdtxtpoqty");
                    TextBox lbltotalmrp = (TextBox)e.Row.FindControl("grdlblmrpvalue");
                    Label lbmrp = (Label)e.Row.FindControl("grdlblmrp");
                    Label lblproductid = (Label)e.Row.FindControl("grdlblproductid");
                    string productid = Convert.ToString(lblproductid.Text);
                    decimal pcsqty = Convert.ToDecimal(lblpcsqty.Text.Trim());
                    decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text.Trim());

                    if (purchasecost == 0)
                    {

                        DropDownList abc = e.Row.FindControl("grdlblUOMNAME") as DropDownList;
                        abc.Enabled = false;

                    }
                    else
                    {
                        DropDownList abc = e.Row.FindControl("grdlblUOMNAME") as DropDownList;
                        abc.Enabled = true;

                    }




                    DropDownList grdlblUOMNAME = (e.Row.FindControl("grdlblUOMNAME") as DropDownList);
                    grdlblUOMNAME.Items.Clear();
                    //ddlpacksize.Items.Add(new ListItem("SELECT", "0"));
                    grdlblUOMNAME.AppendDataBoundItems = true;
                    ClsPoOrder clspurchaseorder = new ClsPoOrder();
                    grdlblUOMNAME.DataSource = clspurchaseorder.BindPacksizeProductwise(productid);
                    grdlblUOMNAME.DataValueField = "PACKSIZEID";
                    grdlblUOMNAME.DataTextField = "PACKSIZENAME";
                    grdlblUOMNAME.DataBind();
                    decimal mrp = Convert.ToDecimal(lbmrp.Text.Trim());
                    decimal tpoqty = Convert.ToDecimal(txtpoqty.Text.Trim());
                    decimal Totalbasiccostvalue = pcsqty * purchasecost * tpoqty;
                    lblproductamt.Text = Convert.ToString(String.Format("{0:0.00}", Totalbasiccostvalue));
                    //txtpoqty.Attributes.Add("onkeyup", "Calpurchaseorderdetails( " + pcsqty + "," + purchasecost + ",'" + txtpoqty.ClientID + "'," + lblproductamt.ClientID + ");");
                    //txtpoqty.Attributes.Add("onkeypress", "return isNumberKey('" + txtpoqty.ClientID + "')");

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


    //protected void CheckBox1_OnCheckedChanged(object sender, EventArgs e)
    //{
    //        CheckBox ck1 = (CheckBox)sender;
    //        GridViewRow gdrow = (GridViewRow)ck1.NamingContainer;
    //        var rows = grdpodetailsadd.Rows;
    //        if (((CheckBox)gdrow.FindControl("CheckBox1")).Checked)
    //        {
    //            TextBox t = (TextBox)gdrow.FindControl("grdtxtpoqty");
    //            TextBox t1 = (TextBox)gdrow.FindControl("grdTxtDiscPer");
    //            TextBox t2 = (TextBox)gdrow.FindControl("grdTxtDiscAmnt");
    //            TextBox t3 = (TextBox)gdrow.FindControl("grdlblbasiccostvalue");
    //            t.BackColor = Color.Aqua;
    //            t1.BackColor = Color.Aqua;
    //            t2.BackColor = Color.Aqua;
    //            t3.BackColor = Color.Aqua;

    //           TextBox lblpurchasecost = (TextBox)gdrow.FindControl("grdlblpurchasecost"); 
    //           Label lastRate = (Label)gdrow.FindControl("grdlblmrp");

    //           decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text);
    //           decimal Rate = Convert.ToDecimal(lastRate.Text);

    //           if (purchasecost != Rate)
    //           {
    //             TextBox tr = (TextBox)gdrow.FindControl("grdlblpurchasecost");
    //             tr.BackColor = Color.LightCoral;
    //           }
    //        }
    //        else
    //        {
    //            TextBox t = (TextBox)gdrow.FindControl("grdtxtpoqty");
    //            TextBox t1 = (TextBox)gdrow.FindControl("grdTxtDiscPer");
    //            TextBox t2 = (TextBox)gdrow.FindControl("grdTxtDiscAmnt");
    //            TextBox t3 = (TextBox)gdrow.FindControl("grdlblbasiccostvalue");
    //            t.BackColor = Color.WhiteSmoke;
    //            t1.BackColor = Color.WhiteSmoke;
    //            t2.BackColor = Color.WhiteSmoke;
    //            t3.BackColor = Color.WhiteSmoke;
    //        }
    //}

    protected void PurchaseOrderCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ck1 = (CheckBox)sender;
        GridViewRow gdrow = (GridViewRow)ck1.NamingContainer;
        var rows = grdpodetailsadd.Rows;
        if (((CheckBox)gdrow.FindControl("PurchaseOrderCheckBox")).Checked)
        {
            Label t = (Label)gdrow.FindControl("grdlblQTY1");
            Label t1 = (Label)gdrow.FindControl("grdlblDiscPer");
            Label t2 = (Label)gdrow.FindControl("grdlblDiscAmnt");
            Label t3 = (Label)gdrow.FindControl("grdlblAmount1");
            t.BackColor = Color.Aqua;
            t1.BackColor = Color.Aqua;
            t2.BackColor = Color.Aqua;
            t3.BackColor = Color.Aqua;

            Label lblpurchasecost = (Label)gdrow.FindControl("grdlblpurchasecost1");
            Label lastRate = (Label)gdrow.FindControl("grdlblLastRate");

            decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text);
            decimal Rate = Convert.ToDecimal(lastRate.Text);

            if (purchasecost != Rate)
            {
                Label tr = (Label)gdrow.FindControl("grdlblpurchasecost1");
                tr.ForeColor = Color.Red;
            }
        }
        else
        {
            Label t = (Label)gdrow.FindControl("grdlblQTY1");
            Label t1 = (Label)gdrow.FindControl("grdlblDiscPer");
            Label t2 = (Label)gdrow.FindControl("grdlblDiscAmnt");
            Label t3 = (Label)gdrow.FindControl("grdlblAmount1");
            t.BackColor = Color.WhiteSmoke;
            t1.BackColor = Color.WhiteSmoke;
            t2.BackColor = Color.WhiteSmoke;
            t3.BackColor = Color.WhiteSmoke;
        }
    }
    protected void gvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Style.Add("height", "50px");
                }
                Label lblpurchasecost = (Label)e.Row.FindControl("grdlblpurchasecost1");
                Label lastRate = (Label)e.Row.FindControl("grdlblLastRate");

                decimal purchasecost = Convert.ToDecimal(lblpurchasecost.Text);
                decimal Rate = Convert.ToDecimal(lastRate.Text);

                if (purchasecost != Rate)
                {
                    Label T = (Label)e.Row.FindControl("grdlblpurchasecost1");
                    T.ForeColor = Color.Red;
                }

            }
        }
        catch (Exception EX)
        {

        }

    }

    protected void gvpodetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();

                if (status == "PENDING")
                {
                    cell.ForeColor = Color.HotPink;
                }
                else if (status == "REJECT")
                {
                    cell.ForeColor = Color.Red;
                }
                else if (status == "CONFIRM")
                {
                    cell.ForeColor = Color.Green;
                }
                else if (status == "APPROVED")
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

    protected void btnPOExcel_Click(object sender, EventArgs e)

    {
        //BAL.ClsStockReport clsrpt = new BAL.ClsStockReport();
        string POID = hdn_pofield.Value.ToString().Trim();
        //string PONO = hdn_pofieNo.Value.ToString().Trim();
        //DataTable table = clsrpt.BindPODetailsExcel(POID);
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

        //HttpContext.Current.Response.Charset = "utf-8";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        ////sets font
        //HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        //HttpContext.Current.Response.Write("<BR><BR><BR>");
        ////sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        //HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
        //  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
        //  "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

        //HttpContext.Current.Response.Write("</TR>");

        //foreach (DataRow row in table.Rows)
        //{//write in new row
        //    HttpContext.Current.Response.Write("<TR>");
        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        HttpContext.Current.Response.Write("<Td>");
        //        HttpContext.Current.Response.Write(row[i].ToString());
        //        HttpContext.Current.Response.Write("</Td>");

        //    }

        //    HttpContext.Current.Response.Write("</TR>");
        //}
        //HttpContext.Current.Response.Write("</Table>");
        //HttpContext.Current.Response.Write("</font>");
        //HttpContext.Current.Response.Flush();
        //HttpContext.Current.Response.End();
        string upath = string.Empty;


        upath = "frmpoexceldownload.aspx?POID=" + hdn_pofield.Value.Trim() + "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

    }




    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {

            ClsStockReport clsInvc = new ClsStockReport();
            DataTable table = clsInvc.BindPoInfoExcel(txtfromdate.Text, txttodate.Text, Session["FINYEAR"].ToString());
            table.Columns.AddRange(new[] { new DataColumn(""), new DataColumn("") });
            // This actually makes your HTML output to be downloaded as .xls file
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=PurchaseOrder from '" + txtfromdate.Text + "' to '" + txttodate.Text + "'.xls");

            // Create a dynamic control, populate and render it
            GridView excel = new GridView();
            excel.DataSource = table;
            excel.DataBind();
            excel.RenderControl(new HtmlTextWriter(Response.Output));

            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as System.Web.UI.Control), this.GetType(), "alert", message, true);
        }
    }



    protected void btnHoldsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPoOrder clspurchaseorder = new ClsPoOrder();
            string PONo = string.Empty;
            string xml = string.Empty;
            string Planned = string.Empty;
            string ReqID = string.Empty;
            string ReqNo = string.Empty;
            DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["PORECORDSV2"];

            string fileuploadtag = string.Empty;
            string xmlupload = string.Empty;

            DataTable dtupload = new DataTable();
            if (chkfileupload.Checked)
            {
                dtupload = (DataTable)(Session["UPLOADFILENAME"]);
                xmlupload = ConvertDatatableToXML(dtupload);
                fileuploadtag = "Y";
            }
            else
            {
                fileuploadtag = "N";
                // clsmmpp.FileDelete(hdnqcid.Value.Trim());
            }

            if (dtRecordsCheck.Rows.Count > 0)
            {
                if (Session["PORECORDSV2"] != null)
                {
                    xml = ConvertDatatableToXML(dtRecordsCheck);
                }
                if (gvPurchaseOrder.Rows.Count > 0)
                {
                    if (rdbNoPlanned.Checked == true)
                    {
                        Planned = "N";
                        ReqID = "0";
                        ReqNo = "NA";
                    }
                    else
                    {
                        Planned = "Y";
                        ReqID = this.ddlReqNo.SelectedValue.Trim();
                        ReqNo = this.ddlReqNo.SelectedItem.ToString().Trim();
                    }
                    string delivaryFromDate = this.txtreqfromdateset.Text.ToString();
                    string delivaryToDate = this.txtreqtotodateset.Text.ToString();
                    if (string.IsNullOrEmpty(this.txtreqfromdateset.Text))
                    {
                        MessageBox1.ShowWarning("Delivery from Date Should not be blank!!");
                        return;
                    }
                    if (string.IsNullOrEmpty(this.txtreqtotodateset.Text))
                    {
                        MessageBox1.ShowWarning("Delivery to Date Should not be blank!!");
                        return;
                    }

                    int dtpoEntrydate = Convert.ToInt32(Conver_To_ISO(this.txtpodate.Text.Trim()));
                    int dtFromDate = Convert.ToInt32(Conver_To_ISO(this.txtreqfromdateset.Text.Trim()));
                    int dtToDate = Convert.ToInt32(Conver_To_ISO(this.txtreqtotodateset.Text.Trim()));

                    if (dtFromDate < dtpoEntrydate)
                    {
                        MessageBox1.ShowWarning("Delivary From Date " + this.txtreqfromdateset.Text.Trim() + " <br/> cannot be less than Entrydate " + this.txtpodate.Text.Trim() + " Please Check your dates");
                        return;
                    }
                    else if (dtFromDate > dtToDate)
                    {
                        MessageBox1.ShowWarning("Delivary From Date " + this.txtreqfromdateset.Text.Trim() + " <br/> cannot be greater than Delivary Todate " + this.txtreqtotodateset.Text.Trim() + " Please Check your dates");
                        return;
                    }
                    string extraFreight = string.Empty;
                    if (this.CheckBox2.Checked)
                    {
                        extraFreight = "Y";
                    }
                    else
                    {
                        extraFreight = "N";
                    }

                    string isVerified = "H";


                    decimal discountAmnt = Convert.ToDecimal(this.txtDiscountAmnt.Text.ToString());
                    decimal freightAmnt = Convert.ToDecimal(this.txtFreightAmnt.Text.ToString());
                    decimal OtherCharges = Convert.ToDecimal(this.txtOtherCharges.Text.ToString());

                    string paymentsTerms = this.txtPaymentTerms.Text;
                    PONo = clspurchaseorder.InsertMMPODetails(this.hdn_pofield.Value.ToString().Trim(), this.txtpodate.Text.Trim().Trim(),
                                                             this.ddlTPUName.SelectedValue.ToString().Trim(), this.ddlTPUName.SelectedItem.ToString().Trim(),
                                                             this.txtremarks.Text.Trim(), Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString().Trim()),
                                                             Convert.ToDecimal(this.txtgrosstotal.Text.Trim()), Convert.ToDecimal(this.txtexercise.Text.Trim()),
                                                             Convert.ToDecimal(this.txtsaletax.Text.Trim()), Convert.ToDecimal(this.txttotalamount.Text.Trim()),
                                                             HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                             Convert.ToDecimal(this.txtTotalMRP.Text.Trim()), Convert.ToDecimal(this.txtcasepack.Text.Trim()),
                                                             this.txtqutrefno.Text, this.txtqutrefno.Text, xml,
                                                             Planned, ReqID, ReqNo, HttpContext.Current.Session["DEPOTID"].ToString().Trim(), xmlupload, fileuploadtag, delivaryFromDate, delivaryToDate, discountAmnt, freightAmnt, extraFreight, isVerified, OtherCharges, paymentsTerms);
                    if (PONo != "")
                    {
                        if (Convert.ToString(hdn_pofield.Value) == "")
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> saved Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            div_btnPrint.Style["display"] = "none";
                            this.divbtnsave.Style["display"] = "none";
                            this.divbtnHoldsave.Style["display"] = "none";
                            trscheduledate.Style["display"] = "none";
                            this.tblADD.Style["display"] = "";
                            ResetAll();
                            LoadPO(Label);

                        }
                        else
                        {
                            MessageBox1.ShowSuccess("PO No :<b><font color='green'> " + PONo + "</font></b> updated Successfully!", 60, 550);
                            txtpono.Text = PONo;
                            this.divbtnsave.Style["display"] = "none";
                            this.divbtnHoldsave.Style["display"] = "none";
                            trscheduledate.Style["display"] = "none";
                            this.tblADD.Style["display"] = "none";
                            ResetAll();
                            LoadPO(Label);

                        }

                        this.grdpodetailsadd.DataSource = null;
                        this.grdpodetailsadd.DataBind();
                        //this.gvpodetails.DataSource = null;
                        //this.gvpodetails.DataBind();
                        this.divProductDetails.Style["display"] = "none";
                    }
                    else
                    {
                        this.divProductDetails.Style["display"] = "";
                        MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please enter Purchase Order details.", 60, 450);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private void ShowReport_PO(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string POID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_Comp_infofactoryid = new DataTable();

        BAL.ClsStockReport clsrpt = new BAL.ClsStockReport();
        dtdetails = clsrpt.BindPODetails(POID);
        dtheader = clsrpt.BindPOHeader(POID);
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        dt_Comp_infofactoryid = clsrpt.Bind_CompanyInfofactoryid(POID); /*new add for factory wise address*/

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));//
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFOFACTORYWISE", dt_Comp_infofactoryid));/*unit one and unit 2*/

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", POID);
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
    }

    protected void chkDuplicatepo_CheckedChanged(object sender, EventArgs e)
    {

        try
        {
            this.divbtnsave.Style["display"] = "";
            this.gvPurchaseOrder.DataSource = null;
            this.gvPurchaseOrder.DataBind();
            ProductWiseLastRateBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void txtOtherCharges_TextChanged(object sender, EventArgs e)
    {
        decimal OrtherChargers = Convert.ToDecimal(this.txtOtherCharges.Text);
        if (OrtherChargers > 0)
        {
            decimal BasicAmnt = Convert.ToDecimal(this.txtBasicAmnt.Text);
            decimal DiscountAmnt = Convert.ToDecimal(this.txtDiscountAmnt.Text);
            decimal ItemWiseTotalDisc = Convert.ToDecimal(this.txtItemWiseTotalDisc.Text);
            decimal FreightAmnt = Convert.ToDecimal(this.txtFreightAmnt.Text);
            decimal finalAmnt = (BasicAmnt + -1 * (DiscountAmnt) + -1 * (ItemWiseTotalDisc) + FreightAmnt + OrtherChargers);
            this.txtgrosstotal.Text = Convert.ToString(finalAmnt);
            ;
        }
    }
}