﻿using BAL;
using PPBLL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmMMIssue : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["mode"]) == "dashboard")
                {
                    divbtnreject.Visible = false;
                    divbtnapprove.Visible = false;
                    divbtnsave.Visible = true;
                    divbtncancel.Visible = true;
                    divadd.Style["display"] = "";
                    TDlblststus.Visible = true;
                    TDddltatus.Visible = true;
                }
                else
                {
                    DateLock();
                    ViewState["Name"] = Request.QueryString["Name"].ToString().Trim();
                    divpono.Style["display"] = "none";
                    InputTable.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    //this.LoadIssue();
                    this.LoadDepartment();
                    this.LoadAllDepartment();
                    this.BindFactoryName();
                }
                if (Convert.ToString(Request.QueryString["Name"]) == "Unchk")
                {
                    divbtnreject.Visible = false;
                    divbtnapprove.Visible = false;
                    divbtnsave.Visible = true;
                    divbtncancel.Visible = true;
                    divadd.Style["display"] = "";
                    TDlblststus.Visible = true;
                    TDddltatus.Visible = true;
                }
                else
                {
                    divbtnreject.Visible = true;
                    divbtnapprove.Visible = true;
                    divbtnsave.Visible = false;
                    divbtncancel.Visible = true;
                    divadd.Style["display"] = "none";
                    TDlblststus.Visible = false;
                    TDddltatus.Visible = false;
                }
                if (Convert.ToString(Request.QueryString["Name"]) == "Itemledger")
                {
                    string IssueID = Convert.ToString(Request.QueryString["VCHID"]);
                    divbtnreject.Visible = false;
                    divbtnapprove.Visible = false;
                    divbtnsave.Visible = false;
                    divbtncancel.Visible = false;
                    ItemLedger_Issue(IssueID);
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


    #region all department

    private void LoadAllDepartment()
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindRequistionDepartment();
            if (dt.Rows.Count > 0)
            {
                this.ddlRequistionDepartMent.Items.Clear();
                this.ddlRequistionDepartMent.Items.Add(new ListItem("Select Department", "0"));
                this.ddlRequistionDepartMent.AppendDataBoundItems = true;
                this.ddlRequistionDepartMent.DataSource = dt;
                this.ddlRequistionDepartMent.DataTextField = "DEPTNAME";
                this.ddlRequistionDepartMent.DataValueField = "DEPTID";
                this.ddlRequistionDepartMent.DataBind();
            }
            else
            {
                this.ddlRequistionDepartMent.Items.Clear();
                this.ddlRequistionDepartMent.Items.Add(new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    #endregion

    #region add new record
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.divbtnsave.Style["display"] = "";
            this.trAutoIssueNo.Style["display"] = "none";
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            this.CLEARALL();
            //LoadStoreLocation();
            this.ddlRequistionDepartMent.Enabled = true;
            this.ddlRequistionDepartMent.SelectedValue = "0";
            this.ddlStore.Enabled = false;
            this.ddlStore.SelectedValue = "0";

            DateTime dtcurr = DateTime.Now;
            //string date = "dd/MM/yyyy";            
            //this.txtrequifromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            //this.txtrequitodate.Text = dtcurr.ToString(date).Replace('-', '/');
            HttpContext.Current.Session["MATERIALDETAILS"] = null;
            this.ddldepartment.Enabled = true;
            this.ddlfactory.Enabled = true;
            divbtnsave.Visible = true;
            if (ViewState["Name"].ToString() == "Unchk") // MAKER
            {
                this.Div1.Style["display"] = "";
                this.divMaterialDetails.Style["display"] = "";
                //this.gvissueorderdetails.Columns[13].Visible = true;
                this.gvrequistiondetails.Columns[0].Visible = true;
            }
            else // CHECKER
            {
                this.Div1.Style["display"] = "none";
                this.divMaterialDetails.Style["display"] = "none";
                //this.gvissueorderdetails.Columns[13].Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Bind Issue
    private void LoadIssue()
    {
        ClsIssue clsissue = new ClsIssue();
        if (ViewState["Name"].ToString() == "Unchk") // MAKER
        {
            DataTable dt = clsissue.BindIssuegrdid(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ViewState["Name"].ToString().Trim(), "", HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                if (ddltatus.SelectedValue == "Y")
                {
                    this.gvIssuegrid.Columns[5].Visible = false;
                    this.gvIssuegrid.Columns[6].Visible = false;
                    this.gvIssuegrid.Columns[7].Visible = false;
                    this.gvIssuegrid.Columns[8].Visible = true;
                    this.gvIssuegrid.DataSource = dt;
                    this.gvIssuegrid.DataBind();
                }
                else
                {
                    //this.gvIssuegrid.Columns[5].Visible = true;
                    //this.gvIssuegrid.Columns[6].Visible = true;
                    //this.gvIssuegrid.Columns[7].Visible = false;
                    //this.gvIssuegrid.Columns[8].Visible = false;
                    this.gvIssuegrid.DataSource = dt;
                    this.gvIssuegrid.DataBind();
                }
            }
            else
            {
                if (ddltatus.SelectedValue == "Y")
                {
                    this.gvIssuegrid.Columns[5].Visible = false;
                    this.gvIssuegrid.Columns[6].Visible = false;
                    this.gvIssuegrid.Columns[7].Visible = true;
                    this.gvIssuegrid.DataSource = null;
                    this.gvIssuegrid.DataBind();
                }
                else
                {
                    this.gvIssuegrid.Columns[5].Visible = true;
                    this.gvIssuegrid.Columns[6].Visible = true;
                    this.gvIssuegrid.Columns[7].Visible = false;
                    this.gvIssuegrid.DataSource = null;
                    this.gvIssuegrid.DataBind();
                }
            }
        }
        else // CHECKER
        {
            DataTable dt = clsissue.BindIssuegrdid(txtfromdate.Text, txttodate.Text, ViewState["Name"].ToString(), "N", HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvIssuegrid.Columns[5].Visible = true;
                //this.gvIssuegrid.Columns[6].Visible = false;
                this.gvIssuegrid.Columns[7].Visible = false;
                this.gvIssuegrid.Columns[8].Visible = false;
                this.gvIssuegrid.DataSource = dt;
                this.gvIssuegrid.DataBind();
            }
            else
            {
                this.gvIssuegrid.Columns[5].Visible = true;
                //this.gvIssuegrid.Columns[6].Visible = false;
                this.gvIssuegrid.Columns[7].Visible = false;
                this.gvIssuegrid.Columns[8].Visible = false;
                this.gvIssuegrid.DataSource = null;
                this.gvIssuegrid.DataBind();
            }
        }
    }
    #endregion

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.trAutoIssueNo.Style["display"] = "none";
            this.InputTable.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.LoadIssue();
            CLEARALL();
            // this.ddldepartment.SelectedValue = "";
            //this.ddlfactory.SelectedValue = "0";
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtrequifromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            this.txtrequitodate.Text = dtcurr.ToString(date).Replace('-', '/');
            HttpContext.Current.Session["MATERIALDETAILS"] = null;
            this.ddldepartment.Enabled = true;
            if (ViewState["Name"].ToString() == "Unchk") // MAKER
            {
                this.Div1.Style["display"] = "";
                this.divMaterialDetails.Style["display"] = "";
                this.gvissueorderdetails.Columns[13].Visible = true;
                this.divadd.Style["display"] = "";
            }
            else // CHECKER
            {
                this.Div1.Style["display"] = "none";
                this.divMaterialDetails.Style["display"] = "none";
                this.gvissueorderdetails.Columns[13].Visible = false;
                this.divadd.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Load Department
    private void LoadDepartment()
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindDepartment(HttpContext.Current.Session["USERID"].ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                this.ddldepartment.Items.Clear();
                //this.ddldepartment.Items.Add(new ListItem("Select Department", "0"));
                this.ddldepartment.AppendDataBoundItems = true;
                this.ddldepartment.DataSource = dt;
                this.ddldepartment.DataTextField = "DEPTNAME";
                this.ddldepartment.DataValueField = "DEPTID";
                this.ddldepartment.DataBind();
            }
            else
            {
                this.ddldepartment.Items.Clear();
                this.ddldepartment.Items.Add(new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion

    #region Load Requision details
    private void LoadRequisionDetails(string DEPTID, string REQUISITIONFROMDATE, string REQUISITIONTODATE, string deptid, string FactoryID)
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.BindRequesitionDetails(DEPTID, REQUISITIONFROMDATE, REQUISITIONTODATE, deptid, FactoryID, this.ddlRequisitionFrom.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                gvrequistiondetails.DataSource = dt;
                gvrequistiondetails.DataBind();
            }
            else
            {
                gvrequistiondetails.DataSource = null;
                gvrequistiondetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion

    #region Btn Requisition Search_Click
    protected void btnrequisitionsearch_Click(object sender, EventArgs e)

    {
        string _txtissuedate = this.txtissuedate.Text.ToString();
        string _txtrequitodate = this.txtrequitodate.Text.ToString();

        if (Convert.ToDateTime(_txtissuedate) < Convert.ToDateTime(_txtrequitodate))

        {
            MessageBox1.ShowError("Entry Date & To Date Must Be The Same");

        }
        else

        {
            divadd.Style["display"] = "none";
            if (this.ddlRequistionDepartMent.SelectedValue.ToString() == "0")
            {
                MessageBox1.ShowWarning("Please Select Destinastion Department");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlRequistionDepartMent.ClientID + "').focus(); ", true);
                return;
            }
            else
            {
                if (this.ddldepartment.SelectedValue != "0" && this.ddlfactory.SelectedValue != "0")
                {
                    gvmaterialdetails.DataSource = null;
                    gvmaterialdetails.DataBind();
                    gvissueorderdetails.DataSource = null;
                    gvmaterialdetails.DataBind();
                    string ISSUEID = string.Empty;
                    LoadRequisionDetails(this.ddldepartment.SelectedValue.Trim(), this.txtrequifromdate.Text.Trim(), this.txtrequitodate.Text.Trim(), this.ddlRequistionDepartMent.SelectedValue.ToString(), this.ddlfactory.SelectedValue.Trim());
                }
                else
                {
                    MessageBox1.ShowInfo("Select Factory OR Department");
                    return;
                }

            }

        }

    }
    #endregion

    #region Selected Requisition
    public string SelectedRequisition()
    {
        string requisitionid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvrequistiondetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                if (chk.Checked == true)
                {
                    Label lblREQUISITIONID = (Label)gvr.FindControl("lblREQUISITIONID");
                    requisitionid += lblREQUISITIONID.Text.Trim() + ",";
                }
            }

            if (!string.IsNullOrEmpty(requisitionid))
            {
                requisitionid = requisitionid.Substring(0, requisitionid.Length - 1);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return requisitionid;
    }
    #endregion

    #region Bind Material
    protected void BindMaterial(string RequisitionID, string IssueNO, string FactoryID)
    {
        try
        {
            ClsIssue clsissue = new ClsIssue();
            DataTable dt = clsissue.BindMaterilaDetails(RequisitionID, IssueNO, FactoryID);
            if (dt.Rows.Count > 0)
            {
                this.gvmaterialdetails.DataSource = dt;
                this.gvmaterialdetails.DataBind();
            }
            else
            {
                this.gvmaterialdetails.DataSource = null;
                this.gvmaterialdetails.DataBind();

                this.gvissueorderdetails.DataSource = null;
                this.gvissueorderdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CLEARALL
    private void CLEARALL()
    {
        gvmaterialdetails.DataSource = null;
        gvmaterialdetails.DataBind();
        gvissueorderdetails.DataSource = null;
        gvissueorderdetails.DataBind();
        gvrequistiondetails.DataSource = null;
        gvrequistiondetails.DataBind();
        HttpContext.Current.Session["MATERIALDETAILS"] = null;
        this.Hdn_Fld.Value = "";
        this.txtremarks.Text = "";
        //this.ddlRequistionDepartMent.SelectedValue = "0";
        //this.ddlStore.SelectedValue = "0";
    }
    #endregion

    //#region Requisition Checked Change
    //protected void chkreq_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string dept = string.Empty;
    //        CheckBox ck1 = (CheckBox)sender;
    //        GridViewRow gdrow = (GridViewRow)ck1.NamingContainer;
    //        var rows = gvrequistiondetails.Rows;
    //        HttpContext.Current.Session["MATERIALDETAILS"] = null;
    //        string requisitionid = string.Empty;
    //        requisitionid = SelectedRequisition();
    //        if (requisitionid != "")
    //        {
    //            dept = checkDepartMent(requisitionid);
    //        }
    //        if (dept == "DIFDEPT")/*if department are not same*/
    //        {
    //            MessageBox1.ShowWarning("only same department can go for issue at same time");
    //            ck1.Checked = false;
    //            return;
    //        }
    //        else if (dept == "OVERFLOW")/*if selection is more than 5*/
    //        {
    //            MessageBox1.ShowWarning("you cannot select more than five requisition at once");
    //            ck1.Checked = false;
    //            return;
    //        }
    //        this.LoadAllDepartment();
    //        this.BIndDepartMentWithStoreLocation(requisitionid);
    //        if(this.ddlRequisitionFrom.SelectedValue== "ALL")
    //        {
    //            this.LoadStoreLocation(requisitionid);
    //        }

    //        this.BindMaterial(requisitionid, "", ddlfactory.SelectedValue.ToString());
    //        this.divadd.Style["display"] = "none";
    //        this.ddlRequistionDepartMent.Enabled = false;
    //        this.ddlStore.Enabled = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    }
    //}
    //#endregion

    #region Create DataTable Structure
    public DataTable CreateDataTableMaterial()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALID", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REQITIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("ISSUEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("GRNNO", typeof(string)));
        dt.Columns.Add(new DataColumn("QCNO", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUITIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("ISCLOSED", typeof(string)));

        HttpContext.Current.Session["MATERIALDETAILS"] = dt;
        return dt;
    }
    #endregion

    public string checkDepartMent(string id)
    {
        string x = string.Empty;
        string mode = "deptCheck";
        ClsIssue clsIssue = new ClsIssue();
        x = clsIssue.fetchId(mode, id);
        return x;
    }

    #region btnadd_Click
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {

            ClsIssue clsissu = new ClsIssue();
            int numbercheack = 0;
            if (HttpContext.Current.Session["MATERIALDETAILS"] == null)
            {
                CreateDataTableMaterial();
            }
            DataTable dtadd = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];
            foreach (GridViewRow gvRow in gvmaterialdetails.Rows)
            {
                //decimal QTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblQTY")).Text);
                decimal QTY = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtissueqty")).Text);
                decimal STOCKQTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblStockQTY")).Text);
                //decimal STOCKQTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblStockQTY")).Text);
                string Product = ((Label)gvRow.FindControl("lblMATERIALNAME")).Text.Trim();
                decimal FIRSTISSQTY = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtissueqty")).Text);

                if (FIRSTISSQTY > 0)
                {

                    if (QTY > STOCKQTY)
                    {
                        MessageBox1.ShowInfo("Stock No Available for <font color='red'>" + Product + "</font>", 60, 700);
                        return;
                    }
                }


            }
            foreach (GridViewRow gvRow in gvmaterialdetails.Rows)
            {
                string QTY12 = ((TextBox)gvRow.FindControl("txtissueqty")).Text.ToString();
                if (QTY12 != "")
                {
                    numbercheack = dtadd.Select("MATERIALID='" + ((Label)gvRow.FindControl("lblMATERIALID")).Text + "' AND REQUITIONID = '" + ((Label)gvRow.FindControl("lblREQUITIONID")).Text + "'").Length;
                    if (numbercheack > 0)
                    {
                        MessageBox1.ShowInfo("Material <b><font color='red'>" + ((Label)gvRow.FindControl("lblMATERIALNAME")).Text + "</font></b> Already exit");
                        break;
                    }
                    else
                    {
                        string QTY1 = ((TextBox)gvRow.FindControl("txtissueqty")).Text.ToString();
                        decimal QTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblQTY")).Text);
                        decimal BALANCEQTY = Convert.ToDecimal(((Label)gvRow.FindControl("lblbalanceQty")).Text);
                        if (QTY1 != "")
                        {
                            decimal ISSQTY = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtissueqty")).Text);
                            //if (ISSQTY >= 0 && QTY >= ISSQTY)
                            if (BALANCEQTY >= 0 && BALANCEQTY >= ISSQTY)
                            {

                                DataRow dr = dtadd.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["CATEGORYID"] = ((Label)gvRow.FindControl("lblCATEGORYID")).Text;
                                dr["CATEGORYNAME"] = ((Label)gvRow.FindControl("lblCATEGORYNAME")).Text;
                                dr["MATERIALID"] = ((Label)gvRow.FindControl("lblMATERIALID")).Text;
                                dr["MATERIALNAME"] = ((Label)gvRow.FindControl("lblMATERIALNAME")).Text;
                                dr["UOMID"] = ((Label)gvRow.FindControl("lblUOMID")).Text;
                                dr["UOMNAME"] = ((Label)gvRow.FindControl("lblUOMNAME")).Text;
                                dr["REQITIONQTY"] = ((Label)gvRow.FindControl("lblQTY")).Text;
                                dr["ISSUEQTY"] = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtissueqty")).Text);
                                dr["RATE"] = Convert.ToDecimal(((Label)gvRow.FindControl("lblRATE")).Text);
                                dr["Amount"] = Convert.ToDecimal(((Label)gvRow.FindControl("lblRATE")).Text) * Convert.ToDecimal(((TextBox)gvRow.FindControl("txtissueqty")).Text);
                                dr["GRNNO"] = Convert.ToString(((TextBox)gvRow.FindControl("TXTGRNNO")).Text);
                                dr["QCNO"] = Convert.ToString(((TextBox)gvRow.FindControl("txtqcno")).Text);
                                dr["REQUITIONID"] = Convert.ToString(((Label)gvRow.FindControl("lblREQUITIONID")).Text);
                                dr["ISCLOSED"] = Convert.ToString(((TextBox)gvRow.FindControl("txtClose")).Text);

                                dtadd.Rows.Add(dr);
                                dtadd.AcceptChanges();




                            }
                            else
                            {
                                MessageBox1.ShowInfo("Issue Quantity Can not be greater than Balance Qty.", 60, 450);
                            }
                        }
                    }
                }
            }
            HttpContext.Current.Session["MATERIALDETAILS"] = dtadd;
            if (dtadd.Rows.Count > 0)
            {
                this.gvissueorderdetails.DataSource = dtadd;
                this.gvissueorderdetails.DataBind();
            }
            else
            {
                this.gvissueorderdetails.DataSource = null;
                this.gvissueorderdetails.DataBind();
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

    #region Selected Requisition Department
    public string SelectedRequisitionDept()
    {
        string deptid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvrequistiondetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                if (chk.Checked == true)
                {
                    Label lblDEPTID = (Label)gvr.FindControl("lblDEPTID");
                    deptid += lblDEPTID.Text.Trim() + ",";
                }
            }

            if (!string.IsNullOrEmpty(deptid))
            {
                deptid = deptid.Substring(0, deptid.Length - 1);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return deptid;
    }
    #endregion

    #region SAVE ISSUE
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlRequisitionFrom.SelectedValue == "ALL")
            {
                if (this.ddlStore.SelectedValue == "0")
                {
                    MessageBox1.ShowWarning("Please Select Storelocation");
                    return;
                }
            }

            if (this.ddlRequistionDepartMent.SelectedValue == "0")
            {
                MessageBox1.ShowWarning("Please Select Department");
                return;
            }

            if (gvissueorderdetails.Rows.Count > 0)
            {
                ClsIssue clsissue = new ClsIssue();
                DataTable dt = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];
                string xmlMATERIALDETAILS = ConvertDatatableToXML(dt);
                string iuserid = HttpContext.Current.Session["IUserID"].ToString().Trim();
                string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
                string selectedrequuisionid = SelectedRequisition();
                string issueno = string.Empty;
                string deptid = string.Empty;
                string DeptName = string.Empty;

                foreach (GridViewRow gvr in gvrequistiondetails.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                    if (chk.Checked == true)
                    {

                        deptid = this.ddlRequistionDepartMent.SelectedValue;
                        DeptName = this.ddlRequistionDepartMent.SelectedItem.ToString();
                    }
                }
                issueno = clsissue.InsertIssuetDetails(this.Hdn_Fld.Value.Trim(), this.txtissuedate.Text.Trim(), iuserid.Trim(), finyear.Trim(), this.txtremarks.Text.Trim(),
                                            xmlMATERIALDETAILS, selectedrequuisionid, deptid.Trim(), DeptName.Trim(), ddlfactory.SelectedValue.Trim(), this.ddlStore.SelectedValue);


                if (issueno.Contains("Stock Qty No Avilable for this Product"))
                {
                    MessageBox1.ShowSuccess("<b><font color='green'>" + issueno + "</font></b>", 40, 550);
                    return;
                }

                if (!string.IsNullOrEmpty(issueno))
                {
                    if (Convert.ToString(Hdn_Fld.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + issueno + "</font></b> saved successfully,", 40, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Requisition No : <b><font color='green'>" + issueno + "</font></b> updated successfully", 40, 550);
                    }

                    this.InputTable.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.divadd.Style["display"] = "none";
                    if (this.ddltatus.SelectedValue.Trim() != "0")
                    {
                        this.LoadIssue();
                    }
                    this.CLEARALL();
                    this.ddlRequistionDepartMent.Enabled = true;
                    LoadAllDepartment();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color=red>Error on Saving record!</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowError("<b><font color=red>Add at least one record!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Selected Requisition Edit
    public string SelectedRequisitionEdit(ref DataTable dt)
    {
        string requisitionid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvrequistiondetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                Label lblREQUISITIONID = (Label)gvr.FindControl("lblREQUISITIONID");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lblREQUISITIONID.Text.Trim() == dt.Rows[i]["REQUISITIONID"].ToString().Trim())
                    {
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return requisitionid;
    }
    #endregion

    #region Issue Edit
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.divbtnsave.Style["display"] = "none";
            ClsIssue clsissue = new ClsIssue();
            DataSet ds = clsissue.EditIssueGrid(Hdn_Fld.Value);
            this.trAutoIssueNo.Style["display"] = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtShowIssueNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUENO"]).Trim();
                this.txtissuedate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUEDATE"]).Trim();
                this.txtremarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.LoadDepartment();
                this.LoadAllDepartment();
                this.ddlRequistionDepartMent.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                string requisitionId = getRequistionID(Hdn_Fld.Value);

                string orderType = Convert.ToString(ds.Tables[2].Rows[0]["WORKORDERNO"]).Trim();

                if (orderType != "")
                {
                    orderType = orderType.Substring(0, 2);
                }



                if (orderType != "WO")
                {
                    this.LoadStoreLocation(this.ddlRequistionDepartMent.SelectedValue.ToString());
                    this.ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ID"]).Trim();
                }

                this.BindFactoryName();
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();
                this.ddldepartment.Enabled = false;
                this.ddlRequistionDepartMent.Enabled = false;
                this.ddlStore.Enabled = false;
                this.ddlfactory.Enabled = false;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    CreateDataTableMaterial();
                    DataTable dtedit = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtedit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        //drEdit["REQ_DETAILSID"] = Convert.ToString(ds.Tables[1].Rows[i]["REQ_DETAILSID"]);
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["MATERIALID"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALID"]);
                        drEdit["MATERIALNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALNAME"]);
                        drEdit["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEdit["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEdit["REQITIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REQITIONQTY"]);
                        drEdit["ISSUEQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ISSUEQTY"]);
                        drEdit["Rate"] = "0";
                        drEdit["Amount"] = Convert.ToString(Convert.ToDecimal(ds.Tables[1].Rows[i]["ISSUEQTY"]) * 0);
                        drEdit["GRNNO"] = Convert.ToString(ds.Tables[1].Rows[i]["GRNNO"]);
                        drEdit["QCNO"] = Convert.ToString(ds.Tables[1].Rows[i]["QCNO"]);
                        drEdit["REQUITIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUITIONID"]);
                        drEdit["ISCLOSED"] = Convert.ToString(ds.Tables[1].Rows[i]["ISCLOSED"]);
                        dtedit.Rows.Add(drEdit);
                        dtedit.AcceptChanges();
                    }
                    HttpContext.Current.Session["MATERIALDETAILS"] = dtedit;
                    if (dtedit.Rows.Count > 0)
                    {
                        this.gvissueorderdetails.DataSource = dtedit;
                        this.gvissueorderdetails.DataBind();
                    }
                    else
                    {
                        this.gvissueorderdetails.DataSource = null;
                        this.gvissueorderdetails.DataBind();
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.gvrequistiondetails.Columns[0].Visible = true;
                        this.gvrequistiondetails.DataSource = ds.Tables[2];
                        this.gvrequistiondetails.DataBind();
                        DataTable dt = ds.Tables[2];
                        string requiestion = SelectedRequisitionEdit(ref dt);
                        string requisitionid = string.Empty;
                        requisitionid = SelectedRequisition();
                        this.BindMaterial(requisitionid, Hdn_Fld.Value, ddlfactory.SelectedValue.ToString());
                    }
                }
            }
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            if (ViewState["Name"].ToString() == "Unchk") // MAKER
            {
                this.Div1.Style["display"] = "";
                divbtnsave.Visible = true;
                this.divMaterialDetails.Style["display"] = "";
                //this.gvissueorderdetails.Columns[13].Visible = true;
            }
            else // CHECKER
            {
                this.Div1.Style["display"] = "none";
                this.divMaterialDetails.Style["display"] = "none";
                //this.gvissueorderdetails.Columns[13].Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete Issue order grid from Grid
    public int RecordsDelete(string GUID)
    {
        int delflag = 0;
        DataTable dt = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];
        int i = dt.Rows.Count - 1;
        while (i >= 0)
        {
            if (dt.Rows[i]["GUID"].ToString().Trim() == GUID)
            {
                dt.Rows[i].Delete();
                dt.AcceptChanges();

                delflag = 1;
                break;
            }
            i--;
        }

        if (delflag > 0)
        {
            HttpContext.Current.Session["MATERIALDETAILS"] = dt;
            gvissueorderdetails.DataSource = dt;
            gvissueorderdetails.DataBind();
        }
        return delflag;
    }
    #endregion

    #region Delete Reqisition Grid Details
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            Button btn_views = (Button)sender;
            GridViewRow GVR = (GridViewRow)btn_views.NamingContainer;

            Label lblguid = (Label)GVR.FindControl("lblGUID");
            flag = RecordsDelete(lblguid.Text.Trim());

            if (flag == 1)
            {
                MessageBox1.ShowSuccess("<b><font color='green'>Record deleted successfully!</font></b>");
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Record deleted unsuccessful!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Delete Requisition
    protected void btnfinalgrdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsIssue clsissu = new ClsIssue();
            int flag = 0;
            flag = clsissu.DeleteIssue(Hdn_Fld.Value.Trim());
            if (flag > 0)
            {
                MessageBox1.ShowSuccess("<b><font color=green>Record deleted successfully.</font></b>");
                this.Hdn_Fld.Value = "";
                this.LoadIssue();
            }
            else
            {
                MessageBox1.ShowError("<b><font color=red>Error on deleteing.</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnApprove_Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsIssue oissue = new ClsIssue();
            oissue.Accepted(this.Hdn_Fld.Value.Trim(), ddlfactory.SelectedValue.Trim());
            MessageBox1.ShowSuccess("<b><font color='green'>Issue approved successfully!</font></b>");

            DataTable DT = new DataTable();
            ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
            DT = clsMMPo.Bind_Sms_Mobno("3", this.Hdn_Fld.Value.Trim());
            foreach (DataRow row in DT.Rows)
            {
                this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
            }
            DataTable dt1 = new DataTable();
            dt1 = clsMMPo.Bind_Sms_Mobno("4", this.Hdn_Fld.Value.Trim());

            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            divadd.Style["display"] = "none";
            this.LoadIssue();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnReject_Click
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            MessageBox1.ShowSuccess("<b><font color='green'>Issue rejected successfully!</font></b>");
            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            divadd.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region gvmaterialdetails_RowDataBound
    protected void gvmaterialdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStockQTY = (Label)e.Row.FindControl("lblStockQTY");
                TextBox t = (TextBox)e.Row.FindControl("txtissueqty");
                decimal stockQty = Convert.ToDecimal(lblStockQTY.Text);
                CheckBox chk = (CheckBox)e.Row.FindControl("isclosedChkreq");
                if (stockQty <= 0)
                {
                    lblStockQTY.ForeColor = Color.Red;
                    t.Text = "0";
                }

            }
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    #endregion

    #region gvIssuegrid_RowDataBound Commented
    //protected void gvIssuegrid_RowDataBound(object sender, GridRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            GridDataControlFieldCell cell4 = e.Row.Cells[4] as GridDataControlFieldCell;
    //            string QcStatus = cell4.Text.Trim().ToUpper();
    //            if (QcStatus == "PENDING")
    //            {
    //                cell4.ForeColor = Color.Blue;
    //                cell4.Font.Bold = true;
    //            }
    //            else
    //            {
    //                cell4.ForeColor = Color.Green;
    //                cell4.Font.Bold = true;
    //            }
    //        }
    //    }
    //    catch (Exception EX)
    //    {
    //        throw EX;
    //    }
    //}
    #endregion

    #region Btn Issue Search_Click
    protected void btngvfill_Click(object sender, EventArgs e)
    {
        if (ViewState["Name"].ToString() == "Unchk") // Unchk = ISSUE MAKER
        {
            this.LoadIssue();
            //if (ddltatus.SelectedValue.Trim() != "0")
            //{

            //}
            //else
            //{
            //    MessageBox1.ShowInfo("Please select QC Status", 60, 450);
            //}
        }
        else // ISSUE CHECKER
        {
            this.LoadIssue();
        }
    }
    #endregion

    #region View Issue
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            this.divbtnsave.Style["display"] = "none";
            ClsIssue clsissue = new ClsIssue();
            DataSet ds = clsissue.EditIssueGrid(Hdn_Fld.Value);
            this.trAutoIssueNo.Style["display"] = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtShowIssueNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUENO"]).Trim();
                this.txtissuedate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUEDATE"]).Trim();
                this.txtremarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.LoadDepartment();
                this.LoadAllDepartment();
                this.ddlRequistionDepartMent.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                string requisitionId = getRequistionID(Hdn_Fld.Value);

                string orderType = Convert.ToString(ds.Tables[2].Rows[0]["WORKORDERNO"]).Trim();

                if (orderType != "")
                {
                    orderType = orderType.Substring(0, 2);
                }



                if (orderType != "WO")
                {
                    this.LoadStoreLocation(this.ddlRequistionDepartMent.SelectedValue.ToString());
                    this.ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ID"]).Trim();
                }

                this.BindFactoryName();
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();
                this.ddldepartment.Enabled = false;
                this.ddlRequistionDepartMent.Enabled = false;
                this.ddlStore.Enabled = false;
                this.ddlfactory.Enabled = false;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    CreateDataTableMaterial();
                    DataTable dtedit = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtedit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        //drEdit["REQ_DETAILSID"] = Convert.ToString(ds.Tables[1].Rows[i]["REQ_DETAILSID"]);
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["MATERIALID"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALID"]);
                        drEdit["MATERIALNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALNAME"]);
                        drEdit["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEdit["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEdit["REQITIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REQITIONQTY"]);
                        drEdit["ISSUEQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ISSUEQTY"]);
                        drEdit["Rate"] = "0";
                        drEdit["Amount"] = Convert.ToString(Convert.ToDecimal(ds.Tables[1].Rows[i]["ISSUEQTY"]) * 0);
                        drEdit["GRNNO"] = Convert.ToString(ds.Tables[1].Rows[i]["GRNNO"]);
                        drEdit["QCNO"] = Convert.ToString(ds.Tables[1].Rows[i]["QCNO"]);
                        drEdit["REQUITIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUITIONID"]);
                        drEdit["ISCLOSED"] = Convert.ToString(ds.Tables[1].Rows[i]["ISCLOSED"]);
                        dtedit.Rows.Add(drEdit);
                        dtedit.AcceptChanges();
                    }
                    HttpContext.Current.Session["MATERIALDETAILS"] = dtedit;
                    if (dtedit.Rows.Count > 0)
                    {
                        this.gvissueorderdetails.DataSource = dtedit;
                        this.gvissueorderdetails.DataBind();
                    }
                    else
                    {
                        this.gvissueorderdetails.DataSource = null;
                        this.gvissueorderdetails.DataBind();
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.gvrequistiondetails.Columns[0].Visible = true;
                        this.gvrequistiondetails.DataSource = ds.Tables[2];
                        this.gvrequistiondetails.DataBind();
                        DataTable dt = ds.Tables[2];
                        string requiestion = SelectedRequisitionEdit(ref dt);
                        string requisitionid = string.Empty;
                        requisitionid = SelectedRequisition();
                        this.BindMaterial(requisitionid, Hdn_Fld.Value, ddlfactory.SelectedValue.ToString());
                    }
                }
            }
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            if (ViewState["Name"].ToString() == "Unchk") // MAKER
            {
                this.Div1.Style["display"] = "";
                divbtnsave.Visible = true;
                this.divMaterialDetails.Style["display"] = "";
                //this.gvissueorderdetails.Columns[13].Visible = true;
            }
            else // CHECKER
            {
                this.Div1.Style["display"] = "none";
                this.divMaterialDetails.Style["display"] = "none";
                //this.gvissueorderdetails.Columns[13].Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Bind Factory
    public void BindFactoryName()
    {

        DataTable dt = new DataTable();
        BAL.ClsCommonFunction ClsCommon = new BAL.ClsCommonFunction();
        this.ddlfactory.Items.Clear();
        dt = ClsCommon.BindFactory(HttpContext.Current.Session["IUSERID"].ToString());
        this.ddlfactory.DataSource = dt;
        this.ddlfactory.DataTextField = "VENDORNAME";
        this.ddlfactory.DataValueField = "VENDORID";
        this.ddlfactory.DataBind();
    }
    #endregion

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender1.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;
        CalendarExtender8.StartDate = oDate;
        CalendarExtender9.StartDate = oDate;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtissuedate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrequifromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrequitodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender1.EndDate = today1;
            CalendarExtender2.EndDate = today1;
            CalendarExtender3.EndDate = today1;
            CalendarExtender8.EndDate = today1;
            CalendarExtender9.EndDate = today1;
        }
        else
        {
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtissuedate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrequifromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrequitodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender1.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
            CalendarExtender8.EndDate = cDate;
            CalendarExtender9.EndDate = cDate;
        }
    }
    #endregion

    public void ItemLedger_Issue(string IssueID)
    {
        try
        {
            ClsIssue clsissue = new ClsIssue();
            DataSet ds = clsissue.EditIssueGrid(IssueID);
            this.trAutoIssueNo.Style["display"] = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtShowIssueNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUENO"]).Trim();
                this.txtissuedate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUEDATE"]).Trim();
                this.txtremarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]).Trim();
                this.LoadDepartment();
                this.ddldepartment.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DEPTID"]).Trim();
                this.BindFactoryName();
                this.ddlfactory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FACTORYID"]).Trim();
                this.ddldepartment.Enabled = false;
                this.ddlfactory.Enabled = false;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    CreateDataTableMaterial();
                    DataTable dtedit = (DataTable)HttpContext.Current.Session["MATERIALDETAILS"];

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow drEdit = dtedit.NewRow();
                        drEdit["GUID"] = Guid.NewGuid();
                        //drEdit["REQ_DETAILSID"] = Convert.ToString(ds.Tables[1].Rows[i]["REQ_DETAILSID"]);
                        drEdit["CATEGORYID"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYID"]);
                        drEdit["CATEGORYNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["CATEGORYNAME"]);
                        drEdit["MATERIALID"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALID"]);
                        drEdit["MATERIALNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["MATERIALNAME"]);
                        drEdit["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]);
                        drEdit["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]);
                        drEdit["REQITIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REQITIONQTY"]);
                        drEdit["ISSUEQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ISSUEQTY"]);
                        drEdit["Rate"] = "0";
                        drEdit["Amount"] = Convert.ToString(Convert.ToDecimal(ds.Tables[1].Rows[i]["ISSUEQTY"]) * 0);
                        drEdit["GRNNO"] = Convert.ToString(ds.Tables[1].Rows[i]["GRNNO"]);
                        dtedit.Rows.Add(drEdit);
                        dtedit.AcceptChanges();
                    }
                    HttpContext.Current.Session["MATERIALDETAILS"] = dtedit;
                    if (dtedit.Rows.Count > 0)
                    {
                        this.gvissueorderdetails.DataSource = dtedit;
                        this.gvissueorderdetails.DataBind();
                    }
                    else
                    {
                        this.gvissueorderdetails.DataSource = null;
                        this.gvissueorderdetails.DataBind();
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.gvrequistiondetails.Columns[0].Visible = true;
                        this.gvrequistiondetails.DataSource = ds.Tables[2];
                        this.gvrequistiondetails.DataBind();
                        DataTable dt = ds.Tables[2];
                        string requiestion = SelectedRequisitionEdit(ref dt);
                        string requisitionid = string.Empty;
                        requisitionid = SelectedRequisition();
                        this.BindMaterial(requisitionid, Hdn_Fld.Value, ddlfactory.SelectedValue.ToString());
                    }
                }
            }
            this.InputTable.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.divadd.Style["display"] = "none";
            if (ViewState["Name"].ToString() == "Unchk") // MAKER
            {
                this.Div1.Style["display"] = "";
                divbtnsave.Visible = true;
                this.divMaterialDetails.Style["display"] = "";
                //this.gvissueorderdetails.Columns[13].Visible = true;
            }
            else // CHECKER
            {
                this.Div1.Style["display"] = "none";
                this.divMaterialDetails.Style["display"] = "none";
                //this.gvissueorderdetails.Columns[13].Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region SMS
    public string SMS_Block(string numbers, string message)
    {
        string result;
        string sender = "MCNROE";
        string apiKey = "6NWhmRBnhD8-mDxNax8Q8a1R53Ouxmr7HGIu1CMKWu";
        String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&sender=" + sender + "&numbers=" + numbers + "&message=" + message;
        System.IO.StreamWriter myWriter = null;
        System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

        objRequest.Method = "POST";
        objRequest.ContentLength = System.Text.Encoding.UTF8.GetByteCount(url);
        objRequest.ContentType = "application/x-www-form-urlencoded";
        try
        {
            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(url);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }

        System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
        using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            // Close and clean up the StreamReader
            sr.Close();
        }
        return result;
    }
    #endregion

    #region btnPrint_Click
    protected void btnIssuePrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?ISSUEIvoicveid=" + Hdn_Fld.Value.Trim() + "&&TAG=ISS&&MenuId=519";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region storelocation

    private void LoadStoreLocation(string deptid)
    {
        try
        {

            string Mode = string.Empty;
            Mode = "S";

            ClsIssue clsobj = new ClsIssue();
            DataTable dt = clsobj.loadstorelocation(Mode, deptid);
            if (dt.Rows.Count > 0)
            {
                this.ddlStore.Items.Clear();
                this.ddlStore.AppendDataBoundItems = true;
                this.ddlStore.DataSource = dt;
                this.ddlStore.DataTextField = "NAME";
                this.ddlStore.DataValueField = "ID";
                this.ddlStore.DataBind();
            }
            else
            {
                this.ddlStore.Items.Clear();
                this.ddlStore.Items.Add(new ListItem("Select storelocation", "0"));
                MessageBox1.ShowWarning("Storelocation Not Mapped with Any user");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    #endregion

    public string getRequistionID(string reuisitionId)
    {
        string mode = "Requ";
        ClsIssue objIssue = new ClsIssue();
        string requisitionId = string.Empty;
        requisitionId = objIssue.fetchId(mode, reuisitionId);
        return requisitionId;
    }


    protected void ddlRequistionDepartMent_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlStore.Enabled = true;
        LoadStoreLocation(this.ddlRequistionDepartMent.SelectedValue.ToString());
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblREQUISITIONID = (Label)gvr.FindControl("lblREQUISITIONID");
            this.Hdn_Fld_requisitionid.Value = lblREQUISITIONID.Text.Trim();
            string upath = string.Empty;
            string tag = Request.QueryString["TAG"];

            upath = "frmRptInvoicePrint_FAC.aspx?REQSIvoicveid=" + Hdn_Fld_requisitionid.Value + "&&TAG=REQS&&MenuId=518";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void BIndDepartMentWithStoreLocation()
    {
        try
        {
            string mode = "AllDeptid";
            ClsIssue clsissu = new ClsIssue();
            DataTable dt = clsissu.loadstorelocation(mode, this.ddldepartment.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                this.ddlRequistionDepartMent.Items.Clear();
                this.ddlRequistionDepartMent.AppendDataBoundItems = true;
                this.ddlRequistionDepartMent.DataSource = dt;
                this.ddlRequistionDepartMent.DataTextField = "DEPTNAME";
                this.ddlRequistionDepartMent.DataValueField = "DEPTID";
                this.ddlRequistionDepartMent.DataBind();
            }
            else
            {
                this.ddlRequistionDepartMent.Items.Clear();
                this.ddlRequistionDepartMent.Items.Add(new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }




    protected void isclosedChkreq_CheckedChanged(object sender, EventArgs e)
    {
        string requisitionid = string.Empty;
        //GridViewRow gvr in gvmaterialdetails.Rows;
        CheckBox btn_views = (CheckBox)sender;
        GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
        CheckBox chk1 = (CheckBox)gvr.FindControl("isclosedChkreq");
        TextBox txtClose = (TextBox)gvr.FindControl("txtClose");
        if (chk1.Checked == true)
        {
            Label lblREQUISITIONID = (Label)gvr.FindControl("lblREQUITIONID");
            txtClose.Text = "Y";
        }
        else
        {
            chk1.Checked = false;
            txtClose.Text = "N";
        }
 
    }

    protected void btnGetRequisitionDetails_Click(object sender, EventArgs e)
    {
        string requisitionid = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvrequistiondetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkreq");
                if (chk.Checked == true)
                {
                    Label lblREQUISITIONID = (Label)gvr.FindControl("lblREQUISITIONID");
                    requisitionid += lblREQUISITIONID.Text.Trim() + ",";
                }
            }

            if (!string.IsNullOrEmpty(requisitionid))
            {
                requisitionid = requisitionid.Substring(0, requisitionid.Length - 1);
            }

            this.BindMaterial(requisitionid, "", ddlfactory.SelectedValue.ToString());
            this.divadd.Style["display"] = "none";
            this.ddlRequistionDepartMent.Enabled = false;
            this.ddlStore.Enabled = true;



        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    protected void btnShoWProduction_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = "Production";
            this.divDataShow.Visible = true;
            DataTable dt = new DataTable();
            ClsVendor_TPU objColsiisue = new ClsVendor_TPU();
            dt = objColsiisue.BindGateEntry(mode, this.Hdn_Fld.Value);
            this.grvPopUp.DataSource = dt;
            this.grvPopUp.DataBind();


        }
        catch (Exception ex)
        {


        }

    }

    protected void btnShoWRequisition_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = "Requisition";
            this.divDataShow.Visible = true;
            DataTable dt = new DataTable();
            ClsVendor_TPU objColsiisue = new ClsVendor_TPU();
            dt = objColsiisue.BindGateEntry(mode, this.Hdn_Fld.Value);
            this.grvPopUp.DataSource = dt;
            this.grvPopUp.DataBind();

        }
        catch (Exception ex)
        {


        }
    }

    protected void txtissueqty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvmaterialdetails.Rows)
        {

            TextBox txtRequQty = (TextBox)gvRow.FindControl("txtRequQty");
            TextBox txtissueqty = (TextBox)gvRow.FindControl("txtissueqty");
            TextBox txtClose = (TextBox)gvRow.FindControl("txtClose");
            TextBox txtissedQTY = (TextBox)gvRow.FindControl("txtissedQTY");

            if (Convert.ToDecimal(txtissueqty.Text) + Convert.ToDecimal(txtissedQTY.Text) < Convert.ToDecimal(txtRequQty.Text))
            {
                txtClose.Text = "N";
            }
            else if (Convert.ToDecimal(txtissueqty.Text) + Convert.ToDecimal(txtissedQTY.Text) == Convert.ToDecimal(txtRequQty.Text))
            {
                txtClose.Text = "Y";
            }
        }
    }
}
