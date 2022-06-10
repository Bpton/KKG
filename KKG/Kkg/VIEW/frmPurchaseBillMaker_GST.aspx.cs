
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow;
using PPBLL;
public partial class FACTORY_frmPurchaseBillMaker_GST : System.Web.UI.Page
{
    #region Global Variable
    string Checker = string.Empty;
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    string menuID = string.Empty;
    decimal CONVERTIONQTY = 0;
    DataTable DtEditPuMaker = new DataTable();
    DataTable dtTaxCount = new DataTable();// for Tax Count
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TYPE"] != "LGRREPORT")
                {

                    #region Checker Work
                    Checker = Request.QueryString["CHECKER"].ToString().Trim();
                    if (Checker == "TRUE")
                    {
                        divadd.Style["display"] = "none";
                        this.btnsubmitdiv.Visible = false;
                        this.divbtnapprove.Visible = true;
                        this.divbtnreject.Visible = true;
                        this.lblCheckerNote.Visible = false;
                        this.txtCheckerNote.Visible = false;
                        this.tdlblledger.Visible = true;
                        this.tdddlledger.Visible = true;
                        this.trdate.Visible = false;
                        //gvPurchasebill.Columns[0].Visible = false;
                        gvpodetails.Columns[8].Visible = false;
                        this.div_TDS.Visible = false;
                    }
                    else
                    {
                        divadd.Style["display"] = "";
                        this.btnsubmitdiv.Visible = true;
                        this.divbtnapprove.Visible = false;
                        this.divbtnreject.Visible = false;
                        this.lblCheckerNote.Visible = false;
                        this.txtCheckerNote.Visible = false;
                        this.tdlblledger.Visible = false;
                        this.tdddlledger.Visible = false;
                        this.trdate.Visible = true;
                        gvPurchasebill.Columns[0].Visible = true;
                        gvpodetails.Columns[8].Visible = true;
                        //gvpodetails.Columns[10].Visible = false;
                        this.div_TDS.Visible = true;
                    }
                    #endregion

                    InputTable.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    divpobillno.Style["display"] = "none";
                    menuID = Request.QueryString["MENUID"].ToString().Trim();
                    ViewState["menuID"] = menuID;
                    /*txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
                    txtentrydt.Text = dtcurr.ToString(date).Replace('-', '/');
                    txtdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    txtfromdt.Text = dtcurr.ToString(date).Replace('-', '/');
                    txtrefdt.Text = dtcurr.ToString(date).Replace('-', '/');*/
                    LoadVendorName();
                    DateLock();
                }
                else
                {
                    divadd.Style["display"] = "none";
                    this.btnsubmitdiv.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                    this.tdlblledger.Visible = true;
                    this.tdddlledger.Visible = true;
                    this.trdate.Visible = false;
                    //gvPurchasebill.Columns[0].Visible = false;
                    gvpodetails.Columns[8].Visible = false;
                    this.div_TDS.Visible = false;
                    Btn_View(Request.QueryString["InvId"]);

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

    public void Btn_View(string _InvoiceID)
    {
       
        try
        {
            DataSet dseditedporecord = new DataSet();
            ClsPurchaseBillMaker_GST clsmmBill = new ClsPurchaseBillMaker_GST();
            DataTable DtEditGridData = new DataTable();
            /*if (HttpContext.Current.Session["GRNDETAILS"] == null)
            {
                CreateDataTable();
            }
            DtEditGridData = (DataTable)HttpContext.Current.Session["GRNDETAILS"];*/
            string Verify = "";
            string PURBILLNO = hdn_PoBillNo.Value.ToString();
            divpobillno.Style["display"] = "";
            divadd.Style["display"] = "none";

            Verify = clsmmBill.CheckVerify(_InvoiceID);
            if (Verify == "Y")
            {
                //btnsubmitdiv.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
            }
            else
            {
                //btnsubmitdiv.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                divbtnreject.Style["display"] = "";
            }
            this.btnsave.Enabled = true;
            btnsubmitdiv.Style["display"] = "none";
            dseditedporecord = clsmmBill.EditPUBILL(_InvoiceID, HttpContext.Current.Session["FINYEAR"].ToString(),0);
            if (dseditedporecord.Tables[0].Rows.Count > 0)
            {
                txtPbillno.Text = dseditedporecord.Tables[0].Rows[0]["PUBNO"].ToString();
                txtentrydt.Text = dseditedporecord.Tables[0].Rows[0]["ENTRYDATE"].ToString();
                txtrefno.Text = dseditedporecord.Tables[0].Rows[0]["REFNO"].ToString();
                txtrefdt.Text = dseditedporecord.Tables[0].Rows[0]["REFDATE"].ToString();
                txtremarks.Text = dseditedporecord.Tables[0].Rows[0]["REMARKS"].ToString();

                ddlvendor2.SelectedValue = dseditedporecord.Tables[0].Rows[0]["VENDORID"].ToString();

                /*TDS related data*/
                this.lblDeductableAmount.Text = dseditedporecord.Tables[0].Rows[0]["DEDUCTABLEAMOUNT"].ToString();
                this.lblPercentage.Text = dseditedporecord.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                this.lblDeductionAmount.Text = dseditedporecord.Tables[0].Rows[0]["DEDUCTIONAMOUNT"].ToString();
                this.lblNetAmount.Text = dseditedporecord.Tables[0].Rows[0]["NETAMOUNT"].ToString();

                /*if (dseditedporecord.Tables[1].Rows.Count > 0)
                {
                    DtEditGridData = (DataTable)HttpContext.Current.Session["GRNDETAILS"];
                    for (int i = 0; i < dseditedporecord.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = DtEditGridData.NewRow();
                        dr["GRNID"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNID"]).Trim();
                        dr["GRNNO"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNNO"]).Trim();
                        dr["GRNDATE"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNDATE"]).Trim();
                        dr["NETAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["NETAMT"]).Trim();
                        dr["BILLDATE"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["BILLDATE"]).Trim();
                        dr["BILLAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["BILLAMT"]).Trim();
                        dr["ROUNDOFF"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["ROUNDOFF"]).Trim();
                        dr["TAXAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["TAXVALUE"]).Trim();
                        dr["INPUTCGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTCGST"]).Trim();
                        dr["INPUTSGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTSGST"]).Trim();
                        dr["INPUTIGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTIGST"]).Trim();
                        dr["ISSELECTED"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["ISSELECTED"]).Trim();

                        DtEditGridData.Rows.Add(dr);
                        DtEditGridData.AcceptChanges();
                    }
                }*/
                this.gvPurchasebill.DataSource = dseditedporecord.Tables[1];/*DtEditGridData;*/
                this.gvPurchasebill.DataBind();

                DataTable dtfetchGRN = dseditedporecord.Tables[1];
                decimal BasicAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("BASICAMT"));
                decimal NetAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("NETAMT"));
                decimal RoundOff = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("ROUNDOFF"));
                decimal TAXVALUE = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("TAXVALUE"));
                decimal INPUTCGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTCGST"));
                decimal INPUTSGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTSGST"));
                decimal INPUTIGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTIGST"));
                gvPurchasebill.FooterRow.Cells[4].Text = "Total :-";
                gvPurchasebill.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                gvPurchasebill.FooterRow.Cells[4].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[11].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[12].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[13].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[14].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[15].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[17].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[18].ForeColor = Color.Blue;

                gvPurchasebill.FooterRow.Cells[11].Text = BasicAmt.ToString("N2");
                gvPurchasebill.FooterRow.Cells[12].Text = INPUTCGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[13].Text = INPUTSGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[14].Text = INPUTIGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[15].Text = TAXVALUE.ToString("N2");
                gvPurchasebill.FooterRow.Cells[17].Text = RoundOff.ToString("N2");
                gvPurchasebill.FooterRow.Cells[18].Text = NetAmt.ToString("N2");

                Session["PURCHASEMAKERDETAILS"] = dtfetchGRN;
                divpobillno.Style["display"] = "";
                InputTable.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                this.FetchLedger();
            }
            else
            {
                this.gvPurchasebill.DataSource = null;
                this.gvPurchasebill.DataBind();
            }
        }
        catch (Exception ex)
        {
            //string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    #region LoadVendor
    public void LoadVendorName()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            ddlvendor.Items.Clear();
            ddlvendor.Items.Add(new ListItem("SELECT VENDOR NAME", "0"));
            ddlvendor.AppendDataBoundItems = true;
            ddlvendor.DataSource = ClsCommon.BindVendor();
            ddlvendor.DataValueField = "VENDORID";
            ddlvendor.DataTextField = "VENDORNAME";
            ddlvendor.DataBind();

            ddlvendor2.Items.Clear();
            ddlvendor2.Items.Add(new ListItem("SELECT VENDOR NAME", "0"));
            ddlvendor2.AppendDataBoundItems = true;
            ddlvendor2.DataSource = ClsCommon.BindVendor();
            ddlvendor2.DataValueField = "VENDORID";
            ddlvendor2.DataTextField = "VENDORNAME";
            ddlvendor2.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadPurchaseBill
    public void LoadPurchaseBill()
    {
        DataTable dtpurshow = new DataTable();
        ClsPurchaseBillMaker_GST clsGRN = new ClsPurchaseBillMaker_GST();
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        dtpurshow = clsGRN.BindPurBill(ddlvendor.SelectedValue.ToString().Trim(), txtfromdate.Text, txttodate.Text, Checker, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
        if (dtpurshow.Rows.Count > 0)
        {
            gvpodetails.DataSource = dtpurshow;
            gvpodetails.DataBind();
        }
        else
        {
            gvpodetails.DataSource = null;
            gvpodetails.DataBind();
        }
    }
    #endregion

    #region btngrdedit_Click
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dseditedporecord = new DataSet();
            ClsPurchaseBillMaker_GST clsmmBill = new ClsPurchaseBillMaker_GST();
            DataTable DtEditGridData = new DataTable();
            /*if (HttpContext.Current.Session["GRNDETAILS"] == null)
            {
                CreateDataTable();
            }
            DtEditGridData = (DataTable)HttpContext.Current.Session["GRNDETAILS"];*/
            string Verify = "";
            string PURBILLNO = hdn_PoBillNo.Value.ToString();
            divpobillno.Style["display"] = "";
            divadd.Style["display"] = "none";

            Verify = clsmmBill.CheckVerify(PURBILLNO);
            if (Verify == "Y")
            {
                //btnsubmitdiv.Style["display"] = "none";
                divbtnapprove.Style["display"] = "none";
                divbtnreject.Style["display"] = "none";
            }
            else
            {
                //btnsubmitdiv.Style["display"] = "";
                divbtnapprove.Style["display"] = "";
                divbtnreject.Style["display"] = "";
            }
            this.btnsave.Enabled = true;
            btnsubmitdiv.Style["display"] = "none";
            dseditedporecord = clsmmBill.EditPUBILL(PURBILLNO, HttpContext.Current.Session["FINYEAR"].ToString(), 0);
            if (dseditedporecord.Tables[0].Rows.Count > 0)
            {
                txtPbillno.Text = dseditedporecord.Tables[0].Rows[0]["PUBNO"].ToString();
                txtentrydt.Text = dseditedporecord.Tables[0].Rows[0]["ENTRYDATE"].ToString();
                txtrefno.Text = dseditedporecord.Tables[0].Rows[0]["REFNO"].ToString();
                txtrefdt.Text = dseditedporecord.Tables[0].Rows[0]["REFDATE"].ToString();
                txtremarks.Text = dseditedporecord.Tables[0].Rows[0]["REMARKS"].ToString();

                ddlvendor2.SelectedValue = dseditedporecord.Tables[0].Rows[0]["VENDORID"].ToString();

                /*TDS related data*/
                this.lblDeductableAmount.Text = dseditedporecord.Tables[0].Rows[0]["DEDUCTABLEAMOUNT"].ToString();
                this.lblPercentage.Text = dseditedporecord.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                this.lblDeductionAmount.Text = dseditedporecord.Tables[0].Rows[0]["DEDUCTIONAMOUNT"].ToString();
                this.lblNetAmount.Text = dseditedporecord.Tables[0].Rows[0]["NETAMOUNT"].ToString();

                /*if (dseditedporecord.Tables[1].Rows.Count > 0)
                {
                    DtEditGridData = (DataTable)HttpContext.Current.Session["GRNDETAILS"];
                    for (int i = 0; i < dseditedporecord.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = DtEditGridData.NewRow();
                        dr["GRNID"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNID"]).Trim();
                        dr["GRNNO"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNNO"]).Trim();
                        dr["GRNDATE"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["GRNDATE"]).Trim();
                        dr["NETAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["NETAMT"]).Trim();
                        dr["BILLDATE"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["BILLDATE"]).Trim();
                        dr["BILLAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["BILLAMT"]).Trim();
                        dr["ROUNDOFF"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["ROUNDOFF"]).Trim();
                        dr["TAXAMT"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["TAXVALUE"]).Trim();
                        dr["INPUTCGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTCGST"]).Trim();
                        dr["INPUTSGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTSGST"]).Trim();
                        dr["INPUTIGST"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["INPUTIGST"]).Trim();
                        dr["ISSELECTED"] = Convert.ToString(dseditedporecord.Tables[1].Rows[i]["ISSELECTED"]).Trim();

                        DtEditGridData.Rows.Add(dr);
                        DtEditGridData.AcceptChanges();
                    }
                }*/
                this.gvPurchasebill.DataSource = dseditedporecord.Tables[1];/*DtEditGridData;*/
                this.gvPurchasebill.DataBind();

                DataTable dtfetchGRN = dseditedporecord.Tables[1];
                decimal BasicAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("BASICAMT"));
                decimal NetAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("NETAMT"));
                decimal RoundOff = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("ROUNDOFF"));
                decimal TAXVALUE = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("TAXVALUE"));
                decimal INPUTCGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTCGST"));
                decimal INPUTSGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTSGST"));
                decimal INPUTIGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTIGST"));
                gvPurchasebill.FooterRow.Cells[4].Text = "Total :-";
                gvPurchasebill.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                gvPurchasebill.FooterRow.Cells[4].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[11].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[12].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[13].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[14].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[15].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[17].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[18].ForeColor = Color.Blue;

                gvPurchasebill.FooterRow.Cells[11].Text = BasicAmt.ToString("N2");
                gvPurchasebill.FooterRow.Cells[12].Text = INPUTCGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[13].Text = INPUTSGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[14].Text = INPUTIGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[15].Text = TAXVALUE.ToString("N2");
                gvPurchasebill.FooterRow.Cells[17].Text = RoundOff.ToString("N2");
                gvPurchasebill.FooterRow.Cells[18].Text = NetAmt.ToString("N2");

                Session["PURCHASEMAKERDETAILS"] = dtfetchGRN;
                divpobillno.Style["display"] = "";
                InputTable.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                this.FetchLedger();
            }
            else
            {
                this.gvPurchasebill.DataSource = null;
                this.gvPurchasebill.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion    

    #region btnnewentry_Click
    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            InputTable.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            divpobillno.Style["display"] = "none";
            divadd.Style["display"] = "none";
            ClearControls();
            LoadVendorName();
            ResetSession();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngvfill_Click
    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadPurchaseBill();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region DeleteRecordPoDetails
    protected void DeleteRecordPoDetails(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsPurchaseBillMaker_GST clsGRN = new ClsPurchaseBillMaker_GST();
            string Verify = "";
            string pubid = e.Record["PUBID"].ToString();
            int flag = 0;
            int TDSflag = 0;
            Verify = clsGRN.CheckVerify(pubid);
            TDSflag = clsGRN.TDSVerify(pubid);
            if (TDSflag > 0)
            {
                if (Verify == "Y")
                {
                    // MessageBox1.ShowInfo("Approve Purchase Bill Can not be Delete");                
                    e.Record["Error"] = "Approve Purchase Bill Can not be Delete";
                }
                else
                {
                    flag = clsGRN.DeletePUBILL(e.Record["PUBID"].ToString(), Session["FINYEAR"].ToString());
                    if (flag == 1)
                    {
                        //LoadPO();
                        e.Record["Error"] = "Record Deleted Successfully!";
                        this.LoadPurchaseBill();
                    }
                    else
                    {
                        e.Record["Error"] = "Error On Deleting!";
                    }
                }
            }
            else
            {
                e.Record["Error"] = "You can only delete the TDS last transaction !";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Grn Search
    protected void btngrnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtDespatch = (DataTable)Session["GRNDETAILS"];
            DataTable dtfetchGRN = new DataTable();
            ClsPurchaseBillMaker_GST clsGRN = new ClsPurchaseBillMaker_GST();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            dtfetchGRN = clsGRN.BindReceived(txtfromdt.Text.Trim(), txtdate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Checker, /*Convert.ToString(Session["IUserID"]), */"", ddlvendor2.SelectedValue.ToString().Trim());
            if (dtfetchGRN.Rows.Count > 0)
            {
                this.gvPurchasebill.DataSource = dtfetchGRN; //dtDespatch;
                this.gvPurchasebill.DataBind();

                decimal BasicAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("BASICAMT"));
                decimal INPUTCGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTCGST"));
                decimal INPUTSGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTSGST"));
                decimal INPUTIGST = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("INPUTIGST"));
                decimal TAXVALUE = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("TAXVALUE"));
                decimal RoundOff = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("ROUNDOFF"));
                decimal NetAmt = dtfetchGRN.AsEnumerable().Sum(row => row.Field<decimal>("NETAMT"));

                gvPurchasebill.FooterRow.Cells[4].Text = "Total :-";
                gvPurchasebill.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Hdn_BasicAmt.Value = BasicAmt.ToString();
                Hdn_NetAmt.Value = NetAmt.ToString();
                Hdn_TDSFlag.Value = "0";

                /*gvPurchasebill.FooterRow.Cells[4].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[5].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[6].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[7].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[8].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[9].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[10].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[11].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[12].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[13].ForeColor = Color.Blue;

                gvPurchasebill.FooterRow.Cells[5].Text = BasicAmt.ToString("N2");
                gvPurchasebill.FooterRow.Cells[6].Text = INPUTCGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[7].Text = INPUTSGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[8].Text = INPUTIGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[9].Text = TAXVALUE.ToString("N2");
                gvPurchasebill.FooterRow.Cells[12].Text = RoundOff.ToString("N2");
                gvPurchasebill.FooterRow.Cells[13].Text = NetAmt.ToString("N2");*/

                gvPurchasebill.FooterRow.Cells[4].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[11].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[12].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[13].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[14].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[15].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[17].ForeColor = Color.Blue;
                gvPurchasebill.FooterRow.Cells[18].ForeColor = Color.Blue;

                gvPurchasebill.FooterRow.Cells[11].Text = BasicAmt.ToString("N2");
                gvPurchasebill.FooterRow.Cells[12].Text = INPUTCGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[13].Text = INPUTSGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[14].Text = INPUTIGST.ToString("N2");
                gvPurchasebill.FooterRow.Cells[15].Text = TAXVALUE.ToString("N2");
                gvPurchasebill.FooterRow.Cells[17].Text = RoundOff.ToString("N2");
                gvPurchasebill.FooterRow.Cells[18].Text = NetAmt.ToString("N2");

                Session["PURCHASEMAKERDETAILS"] = dtfetchGRN;
            }
            else
            {
                this.gvPurchasebill.DataSource = null;
                this.gvPurchasebill.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            this.LoadPurchaseBill();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                //gvpodetails.Columns[7].HeaderText = "VIEW";
                divadd.Style["display"] = "none";
                //gvpodetails.Columns[8].Visible = false;
            }
            else
            {
                //gvpodetails.Columns[7].HeaderText = "EDIT";
                divadd.Style["display"] = "";
                //gvpodetails.Columns[8].Visible = true;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string xml = string.Empty;
            string SaveID = string.Empty;
            DataTable DtAddGridData = new DataTable();
            DataTable DtSaveData = new DataTable();
            ClsPurchaseBillMaker_GST clsPOBILL = new ClsPurchaseBillMaker_GST();
            if (HttpContext.Current.Session["GRNDETAILS"] == null)
            {
                CreateDataTable();
            }
            DtAddGridData = (DataTable)HttpContext.Current.Session["GRNDETAILS"];
            foreach (GridViewRow row in gvPurchasebill.Rows)
            //for (int i = 0; i < gvPurchasebill.RowsInViewState.Count; i++)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkreq") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Label lblGrnID = (row.Cells[1].FindControl("lblGrnID") as Label);
                        Label lblgrnno = (row.Cells[3].FindControl("lblgrnno") as Label);
                        Label lblgrndt = (row.Cells[4].FindControl("lblgrndt") as Label);
                        Label lblLrno = (row.Cells[5].FindControl("lblLrno") as Label);
                        Label lblVechlno = (row.Cells[6].FindControl("lblVechlno") as Label);
                        Label lblTransID = (row.Cells[7].FindControl("lblTransID") as Label);
                        TextBox txtExchangeRate = (row.Cells[9].FindControl("txtExchangeRate") as TextBox);
                        Label lblBasicAmt = (row.Cells[10].FindControl("lblBasicAmt") as Label);
                        Label lblCGSTAmt = (row.Cells[11].FindControl("lblCGST") as Label);
                        Label lblSGSTAmt = (row.Cells[12].FindControl("lblSGST") as Label);
                        Label lblIGSTAmt = (row.Cells[13].FindControl("lblIGST") as Label);
                        Label lbltaxvalue = (row.Cells[14].FindControl("lbltaxvalue") as Label);
                        Label lblbilldt = (row.Cells[15].FindControl("lblbilldt") as Label);
                        Label lblroundoff = (row.Cells[16].FindControl("lblroundoff") as Label);
                        Label lblnetAmt = (row.Cells[17].FindControl("lblnetAmt") as Label);

                        DataRow dr = DtAddGridData.NewRow();
                        dr["GRNID"] = lblGrnID.Text.Trim();
                        dr["GRNNO"] = lblgrnno.Text.Trim();
                        dr["GRNDATE"] = lblgrndt.Text.Trim();
                        dr["BASICAMT"] = lblBasicAmt.Text.Trim();

                        dr["NETAMT"] = lblnetAmt.Text.Trim();
                        dr["BILLDATE"] = lblbilldt.Text.Trim();
                        dr["ROUNDOFF"] = lblroundoff.Text.Trim();
                        dr["TAXAMT"] = lbltaxvalue.Text.Trim();
                        dr["INPUTCGST"] = lblCGSTAmt.Text.Trim();
                        dr["INPUTSGST"] = lblSGSTAmt.Text.Trim();
                        dr["INPUTIGST"] = lblIGSTAmt.Text.Trim();
                        dr["ISSELECTED"] = 1;
                        dr["LRGRNO"] = lblLrno.Text.Trim();
                        dr["VEHICHLENO"] = lblVechlno.Text.Trim();
                        dr["TRANSPORTER"] = lblTransID.Text.Trim();
                        dr["EXCHANGERATE"] = txtExchangeRate.Text.Trim();
                        DtAddGridData.Rows.Add(dr);
                        DtAddGridData.AcceptChanges();

                        HttpContext.Current.Session["GRNDETAILS"] = DtAddGridData;
                        xml = ConvertDatatable(DtAddGridData);
                    }
                    //else
                    //{
                    //    MessageBox1.ShowError("<b>Please Select GRN Details!");
                    //    return;
                    //}
                }
            }
            if (gvPurchasebill.Rows.Count > 0)
            {
                SaveID = clsPOBILL.SAVEPURCHASEBILLMAKER(hdn_PoBillNo.Value.Trim(),
                                                    this.txtentrydt.Text.Trim(),
                                                    this.txtrefno.Text.Trim(),
                                                    this.txtrefdt.Text.Trim(),
                                                    "", "", "",
                                                    this.ddlvendor2.SelectedValue.ToString(),
                                                    this.ddlvendor2.SelectedItem.ToString(),
                                                    Convert.ToInt32(HttpContext.Current.Session["USERID"].ToString().Trim()),
                                                    Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim(),
                                                    this.txtremarks.Text.Trim(),
                                                    Convert.ToString(HttpContext.Current.Session["DEPOTID"]).Trim(),
                                                    Convert.ToString(HttpContext.Current.Session["TPUNAME"]).Trim(),
                                                    xml, this.Hdn_TDSID.Value.Trim(),Convert.ToDecimal(this.lblDeductableAmount.Text),
                                                    Convert.ToDecimal(this.lblPercentage.Text), 
                                                    Convert.ToDecimal(this.lblDeductionAmount.Text),
                                                    Convert.ToDecimal(this.lblNetAmount.Text),
                                                    Convert.ToDecimal(this.Hdn_TDS_DefaultAmt.Value.Trim()),
                                                    Convert.ToDecimal(this.Hdn_BasicAmt.Value.Trim())
                                                    );

                if (SaveID != "")
                {
                    gvPurchasebill.DataSource = null;
                    gvPurchasebill.DataBind();

                    if (Convert.ToString(hdn_PoBillNo.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Purchase Bill No :  <b><font color='green'>" + SaveID + "</font></b>  Saved Successfully", 60, 550);
                        InputTable.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        divadd.Style["display"] = "";
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Purchase Bill No :  <b><font color='green'>" + SaveID + "</font></b> Updated Successfully", 60, 550);
                        InputTable.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                    }

                    this.hdn_PoBillNo.Value = "";
                    this.ResetSession();
                    this.ClearControls();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    this.ResetSession();
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please Select 1 GRN !");
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
    // Added By Rajeev Kumar On 22-07-2017
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdn_PoBillNo.Value).Trim();
            flag = clsPurchaseStockReceipt.ApprovePurchaseBillReceivedMM(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtPbillno.Text.Trim(), this.txtentrydt.Text.Trim(), ddlledger.SelectedValue.ToString().Trim());

            if (flag >= 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.InputTable.Style["display"] = "none";
                this.LoadPurchaseBill();
                MessageBox1.ShowSuccess("Purchase Stock Received: <b><font color='green'>" + this.txtPbillno.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 700);
                this.hdn_PoBillNo.Value = "";
            }
            else if (flag == 0)
            {
                this.pnlDisplay.Style["display"] = "none";
                this.InputTable.Style["display"] = "";
                MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnReject_Click
    // Added By Rajeev On 22-07-2017
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            InputTable.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtInner = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GRNID", typeof(string)));            //2
        dt.Columns.Add(new DataColumn("GRNNO", typeof(string)));            //3
        dt.Columns.Add(new DataColumn("GRNDATE", typeof(string)));          //4
        dt.Columns.Add(new DataColumn("LRGRNO", typeof(string)));           //5
        dt.Columns.Add(new DataColumn("VEHICHLENO", typeof(string)));       //6
        dt.Columns.Add(new DataColumn("TRANSPORTER", typeof(string)));      //7

        dt.Columns.Add(new DataColumn("EXCHANGERATE", typeof(string)));     //8
        dt.Columns.Add(new DataColumn("BASICAMT", typeof(string)));         //9
        dt.Columns.Add(new DataColumn("INPUTCGST", typeof(string)));        //10
        dt.Columns.Add(new DataColumn("INPUTSGST", typeof(string)));        //11
        dt.Columns.Add(new DataColumn("INPUTIGST", typeof(string)));        //12
        dt.Columns.Add(new DataColumn("TAXAMT", typeof(string)));           //13
        dt.Columns.Add(new DataColumn("ROUNDOFF", typeof(string)));         //14
        dt.Columns.Add(new DataColumn("NETAMT", typeof(string)));           //15
        dt.Columns.Add(new DataColumn("BILLDATE", typeof(string)));         //16        
        dt.Columns.Add(new DataColumn("ISSELECTED", typeof(int)));          //17
        //dt.Columns.Add(new DataColumn("BILLAMT", typeof(string)));          //18

        #region Loop For Adding Itemwise Tax Component
        /*if (Hdn_Fld.Value == "")
        {
            string flag = clsgrnmm.BindRegion(this.ddlvendor2.SelectedValue, HttpContext.Current.Session["DEPOTID"].ToString().Trim());

            if (string.IsNullOrEmpty(flag))
            {
                dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlvendor2.SelectedValue.Trim(), "0", HttpContext.Current.Session["DEPOTID"].ToString().Trim());
            }
            else
            {
                dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlvendor2.SelectedValue.Trim(), "0", HttpContext.Current.Session["DEPOTID"].ToString().Trim());
            }
            Session["dtTaxCount"] = dtTaxCount;
            for (int k = 0; k < dtTaxCount.Rows.Count; k++)
            {
                if (Convert.ToDecimal(dtTaxCount.Rows[k]["PERCENTAGE"].ToString()) > 0)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
        }
        else
        {
            DataSet ds = new DataSet();
            string despatchID = Convert.ToString(Hdn_Fld.Value).Trim();
            ds = clsgrnmm.EditReceivedDetails(despatchID);
            Session["dtTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                if (Convert.ToDecimal(ds.Tables[2].Rows[k]["PERCENTAGE"].ToString()) > 0)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
        }*/
        #endregion

        HttpContext.Current.Session["GRNDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region gvPurchasebill_OnRowDataBound
    protected void gvPurchasebill_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        if (/*e.Row.RowType == DataControlRowType.Header ||*/ e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Checker == "TRUE")
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkreq");
                //chk.Attributes["onclick"] = string.Format("ChangeQuantityEnable('{0}', this.checked);", txt.ClientID);               
                chk.Checked = true;
                chk.Enabled = false;
            }
            else
            {
            }
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

    #region ResetSession
    public void ResetSession()
    {
        Session["GRNDETAILS"] = null;
        Session["PURCHASEMAKERDETAILS"] = null;
    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        this.hdn_PoBillNo.Value = "";
        this.txtrefno.Text = "";        
        /*txtentrydt.Text = dtcurr.ToString(date).Replace('-', '/');
        txtdate.Text = dtcurr.ToString(date).Replace('-', '/');
        txtfromdt.Text = dtcurr.ToString(date).Replace('-', '/');
        txtrefdt.Text = dtcurr.ToString(date).Replace('-', '/');*/
        this.txtremarks.Text = "";
        gvPurchasebill.DataSource = null;
        gvPurchasebill.DataBind();
        this.DateLock();

        this.Hdn_BasicAmt.Value = "0";
        this.Hdn_NetAmt.Value = "0";
        this.Hdn_TDSFlag.Value = "0";
        this.lblDeductableAmount.Text = "0";
        this.lblPercentage.Text = "0";
        this.lblDeductionAmount.Text = "0";
        this.lblNetAmount.Text = "0";
        this.Hdn_TDSID.Value = "";
        this.Hdn_TDS_DefaultAmt.Value = "0";

    }
    #endregion

    #region LoadLedger
    public void FetchLedger()
    {
        ClsCommonFunction ClsCommon = new ClsCommonFunction();
        ddlledger.Items.Clear();
        ddlledger.Items.Add(new ListItem("SELECT LEDGER NAME", "0"));
        ddlledger.AppendDataBoundItems = true;
        ddlledger.DataSource = ClsCommon.LoadLedger();
        ddlledger.DataValueField = "ID";
        ddlledger.DataTextField = "NAME";
        ddlledger.DataBind();
    }
    #endregion

    #region View Purchase Bill
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dseditedporecord = new DataSet();
            ClsPurchaseBillMaker_GST clsmmBill = new ClsPurchaseBillMaker_GST();
            string PURBILLNO = hdn_PoBillNo.Value.ToString();
            divpobillno.Style["display"] = "";
            this.btnsave.Enabled = true;
            dseditedporecord = clsmmBill.EditPUBILL(PURBILLNO, HttpContext.Current.Session["FINYEAR"].ToString(), 0);
            if (dseditedporecord.Tables[0].Rows.Count > 0)
            {
                txtPbillno.Text = dseditedporecord.Tables[0].Rows[0]["PUBNO"].ToString();
                txtentrydt.Text = dseditedporecord.Tables[0].Rows[0]["ENTRYDATE"].ToString();
                txtrefno.Text = dseditedporecord.Tables[0].Rows[0]["REFNO"].ToString();
                txtrefdt.Text = dseditedporecord.Tables[0].Rows[0]["REFDATE"].ToString();
                txtremarks.Text = dseditedporecord.Tables[0].Rows[0]["REMARKS"].ToString();
                this.gvPurchasebill.DataSource = dseditedporecord.Tables[1];
                this.gvPurchasebill.DataBind();

                divpobillno.Style["display"] = "";
                InputTable.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                this.FetchLedger();
                divbtnapprove.Visible = false;
                divbtnreject.Visible = false;
            }
            else
            {
                this.gvPurchasebill.DataSource = null;
                this.gvPurchasebill.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region grdDespatchHeader_RowDataBound
    protected void gvpodetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[6] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();

                if (status == "PENDING")
                {
                    cell.ForeColor = Color.Blue;
                }
                else if (status == "REJECTED")
                {
                    cell.ForeColor = Color.Red;
                }
                else if (status == "HOLD")
                {
                    cell.ForeColor = Color.Black;
                }
                else
                {
                    cell.ForeColor = Color.Green;
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

    protected void txtExchangeRate_OnTextChanged(object sender, EventArgs e)
    {
        int ind = ((sender as TextBox).NamingContainer as GridViewRow).RowIndex;
        int latInd = gvPurchasebill.Rows.Count;
        TextBox txtExchangeRate = (TextBox)gvPurchasebill.Rows[ind].Cells[9].FindControl("txtExchangeRate");
        // TextBox percentage = (TextBox)gVAppraisal.Rows[((sender as TextBox).NamingContainer as GridViewRow).RowIndex].Cells[2].FindControl("txtPercentage");
        Label lblBasicAmt = (Label)gvPurchasebill.Rows[ind].Cells[10].FindControl("lblBasicAmt");
        Label lblnetAmt = (Label)gvPurchasebill.Rows[ind].Cells[17].FindControl("lblnetAmt");

        if ((txtExchangeRate.Text.Equals(string.Empty)))
            txtExchangeRate.Text = "0";
        decimal NetAmt = (Convert.ToDecimal(txtExchangeRate.Text) * (Convert.ToDecimal(lblBasicAmt.Text)));
        lblnetAmt.Text = Convert.ToString(String.Format("{0:0.000}", (NetAmt)));
    }

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedID = Convert.ToString(hdn_PoBillNo.Value);
            string path = "frmRptPurchaseBillMM.aspx?id=" + receivedID + "&&MenuId=" + Request.QueryString["MenuId"].ToString() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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
        CalendarExtender5.StartDate = oDate;
        CalendarExtender7.StartDate = oDate;
        CalendarExtender4.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;
        CalendarExtender1.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrefdt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender5.EndDate = today1;
            CalendarExtender7.EndDate = today1;
            CalendarExtender4.EndDate = today1;
            CalendarExtender3.EndDate = today1;
            CalendarExtender1.EndDate = today1;
            CalendarExtender2.EndDate = today1;
        }
        else
        {
            this.txtfromdt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtrefdt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender5.EndDate = cDate;
            CalendarExtender7.EndDate = cDate;
            CalendarExtender4.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
        }        
    }
    #endregion

    #region btnTDS_Click
    protected void btnTDS_Click(object sender, EventArgs e)
    {
        try
        {
            this.Hdn_TDSFlag.Value = "1";
            ClsPurchaseBillMaker_GST clsmmBill = new ClsPurchaseBillMaker_GST();           
            DataTable dtTax = clsmmBill.TaxApplicable(this.ddlvendor2.SelectedValue, this.txtentrydt.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), Convert.ToDecimal(Hdn_BasicAmt.Value), HttpContext.Current.Session["DEPOTID"].ToString(), "99", "Cr", -1);

            if (dtTax.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(Convert.ToString(dtTax.Rows[0]["DEDUCTABLEAMOUNT"])))
                {
                    this.lblDeductableAmount.Text = "0";
                }
                else
                {
                    this.lblDeductableAmount.Text = Convert.ToString(dtTax.Rows[0]["DEDUCTABLEAMOUNT"]);
                }
                if (string.IsNullOrEmpty(Convert.ToString(dtTax.Rows[0]["BYDEFAULTAMOUNT"])))
                {
                    this.Hdn_TDS_DefaultAmt.Value = "0";
                }
                else
                {
                    this.Hdn_TDS_DefaultAmt.Value  = Convert.ToString(dtTax.Rows[0]["BYDEFAULTAMOUNT"]);
                }

                
                if (string.IsNullOrEmpty(Convert.ToString(dtTax.Rows[0]["DEDUCTABLEPERCENT"])))
                {
                    this.lblPercentage.Text = "0";
                }
                else
                {
                    this.lblPercentage.Text = Convert.ToString(dtTax.Rows[0]["DEDUCTABLEPERCENT"]);
                }

                if (string.IsNullOrEmpty(Convert.ToString(dtTax.Rows[0]["TDSAMOUNT"])))
                {
                    this.lblDeductionAmount.Text = "0";
                }
                else
                {
                    this.lblDeductionAmount.Text = Convert.ToString(dtTax.Rows[0]["TDSAMOUNT"]);
                    this.lblNetAmount.Text = Convert.ToString(Convert.ToDecimal(Hdn_NetAmt.Value) - Convert.ToDecimal(dtTax.Rows[0]["TDSAMOUNT"]));
                }
                this.Hdn_TDSID.Value = Convert.ToString(dtTax.Rows[0]["ID"]);


            }
            else
            {
                this.lblDeductableAmount.Text = "0";
                this.lblPercentage.Text = "0";
                this.lblDeductionAmount.Text = "0";
                this.lblNetAmount.Text = Convert.ToString(Convert.ToDecimal(Hdn_NetAmt.Value));
                this.Hdn_TDSID.Value = "";
                this.Hdn_TDS_DefaultAmt.Value = "0";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
}

