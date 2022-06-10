using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow;

public partial class FACTORY_frmDepoReceived_FAC : System.Web.UI.Page
{
    ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
    DataTable dtreceivedrecord = new DataTable();
    DataTable dtTaxCount = new DataTable();// for Tax Count
    ArrayList Arry = new ArrayList();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
    string Checker = string.Empty;
    string MENUID = string.Empty;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TYPE"] != "LGRREPORT")
            {
                #region QueryString

                MENUID = Request.QueryString["MENUID"].ToString().Trim();
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    btnaddhide.Style["display"] = "none";
                    this.divbtnsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                }
                else
                {
                    btnaddhide.Style["display"] = "";
                    this.divbtnsave.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    this.lblCheckerNote.Visible = false;
                    this.txtCheckerNote.Visible = false;
                }
                #endregion

                ddltodepot.Enabled = false;
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                /*txtreceiveddate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtfromdateins.Text = dtcurr.ToString(date).Replace('-', '/');
                txttodateins.Text = dtcurr.ToString(date).Replace('-', '/');*/
                this.DateLock();
                LoadReceivedDetails();
            }
            else
            {
                btnaddhide.Style["display"] = "none";
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;

                ddltodepot.Enabled = false;
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
                //pnlAddEdit.Style["display"] = "";

                txtreceiveddate.Text = dtcurr.ToString(date).Replace('-', '/');
                txtfromdateins.Text = dtcurr.ToString(date).Replace('-', '/');
                txttodateins.Text = dtcurr.ToString(date).Replace('-', '/');
                //LoadReceivedDetails();
                Btn_View(Request.QueryString["InvId"]);
                //ViewState["BSID"] = Convert.ToString("33F6AC5E-1F37-4B0F-B959-D1C900BB43A5").Trim();        /* BS - OTHERTS */
                //pnlDisplay.Style["display"] = "none";
                //btnaddhide.Style["display"] = "";
                //this.divsave.Visible = false;
                //this.divbtnCancel.Visible = false;
                //this.divbtnapprove.Visible = false;
                //this.divbtnreject.Visible = false;
                //this.lblCheckerNote.Visible = false;
                //this.txtCheckerNote.Visible = false;
                //this.trBtn.Visible = false;
                //Btn_View(Request.QueryString["InvId"]);
            }
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvreceived.ClientID + "', 300, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region Btn_view added by HPDAS
    public void Btn_View(string _InvoiceID)
    {
        try
        {
            hdn_receivedid.Value = _InvoiceID;
            //ResetSession();

            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            string DepotID = string.Empty;
            //if (Checker == "FALSE")
            //{
            //    if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "1")
            //    {
            //        MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
            //        return;
            //    }
            //}
            if (Checker == "FALSE")
            {
                if (Session["TPU"].ToString().Trim() == "F" || Session["TPU"].ToString().Trim() == "EXPU")
                {
                    DepotID = HttpContext.Current.Session["DEPOTID"].ToString();
                }
                else
                {
                    DataTable dtDepot = new DataTable();
                    dtDepot = clsdeporeceived.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                    DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                }
            }
            else
            {
                DepotID = this.hdnDepotID.Value.Trim();
            }
            LoadReceivedDepo();
            ddltodepot.SelectedValue = DepotID;
            LoadDepo();
            HttpContext.Current.Session["DEPORECEIVEDRECORD"] = null;
            if (Session["DEPORECEIVEDRECORD"] == null)
            {
                this.CreateReceivedGrid();
            }
            if (Session["DRTAXCOMPONENTDETAILS"] == null)
            {
                this.CreateDataTableTaxComponent();
            }
            this.LoadReason();
            this.trreceivedno.Style["display"] = "";

            dtreceivedrecord = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            #region QueryString

            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "1" && clsdeporeceived.GetFinancestatus(this.hdn_receivedid.Value.Trim()) == "1")
                {
                    this.divbtnsave.Visible = false;
                    this.gvreceived.Columns[1].Visible = false;
                }
                else if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "0" && clsdeporeceived.GetFinancestatus(this.hdn_receivedid.Value.Trim()) == "0")
                {
                    this.divbtnsave.Visible = true;
                    this.gvreceived.Columns[1].Visible = true;
                }
                else
                {
                    this.divbtnsave.Visible = false;
                    this.gvreceived.Columns[1].Visible = false;
                }
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            string receivedid = hdn_receivedid.Value.ToString();

            DataSet ds = new DataSet();
            ds = clsdeporeceived.BindTransferReceivedInfo(receivedid);
            decimal TotalAmount = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hdnSaleOrderID.Value = ds.Tables[0].Rows[0]["SALEORDERID"].ToString();
                this.hdnSaleOrderNo.Value = ds.Tables[0].Rows[0]["SALEORDERNO"].ToString();
                txtdeporeceivedno.Text = ds.Tables[0].Rows[0]["STOCKDEPORECEIVEDNO"].ToString();
                this.LoadEditedTransferNo();
                ddltransferno.SelectedValue = ds.Tables[0].Rows[0]["STOCKDESPATCHNO"].ToString();
                txttransferdate.Text = ds.Tables[0].Rows[0]["STOCKTRANSFERDATE"].ToString();
                txtwaybillno.Text = ds.Tables[0].Rows[0]["WAYBILLNO"].ToString();
                this.hdnWaybillKey.Value = ds.Tables[0].Rows[0]["WAYBILLKEY"].ToString();
                txttranspoter.Text = ds.Tables[0].Rows[0]["TRANSPORTERNAME"].ToString();
                txtinsuranceno.Text = ds.Tables[0].Rows[0]["INSURANCENO"].ToString();
                txtmodeoftransport.Text = ds.Tables[0].Rows[0]["MODEOFTRANSPORT"].ToString();
                txtreceiveddate.Text = ds.Tables[0].Rows[0]["STOCKDEPORECEIVEDDATE"].ToString();
                ddlfromdepot.SelectedValue = ds.Tables[0].Rows[0]["MOTHERDEPOTID"].ToString();
                ddltodepot.SelectedValue = ds.Tables[0].Rows[0]["RECEIVEDDEPOTID"].ToString();
                txtvehicleno.Text = ds.Tables[0].Rows[0]["VEHICHLENO"].ToString();
                txtlrgrno.Text = ds.Tables[0].Rows[0]["LRGRNO"].ToString();
                txtlrgrdate.Text = ds.Tables[0].Rows[0]["LRGRDATE"].ToString();
                txtchallanno.Text = ds.Tables[0].Rows[0]["CHALLANNO"].ToString();
                txtchallandate.Text = ds.Tables[0].Rows[0]["CHALLANDATE"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                LoadInsurance();
                //this.FetchLedger();
                ddlInsurance.SelectedValue = ds.Tables[0].Rows[0]["INSURANCECOMPID"].ToString();
                this.txtCheckerNote.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();
                txtfformno.Text = ds.Tables[0].Rows[0]["FFORMNO"].ToString();
                if (ds.Tables[0].Rows[0]["FFORMDATE"].ToString() == "01/01/1900")
                {
                    txtfformdate.Text = "";
                }
                else
                {
                    txtfformdate.Text = ds.Tables[0].Rows[0]["FFORMDATE"].ToString();
                }

                txtgateno.Text = ds.Tables[0].Rows[0]["GATEPASSNO"].ToString();
                if (ds.Tables[0].Rows[0]["GATEPASSDATE"].ToString() == "01/01/1900")
                {
                    txtgatedate.Text = "";
                }
                else
                {
                    txtgatedate.Text = ds.Tables[0].Rows[0]["GATEPASSDATE"].ToString();
                }

                this.hdnTransporterID.Value = ds.Tables[0].Rows[0]["TRANSPORTERID"].ToString();
                this.txtTotalCase.Text = ds.Tables[0].Rows[0]["TOTALCASE"].ToString();
                this.txtTotalPCS.Text = ds.Tables[0].Rows[0]["TOTALPCS"].ToString();

                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtDRTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtreceivedrecord.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        string TaxGuid = dr["GUID"].ToString().Trim();
                        dr["STOCKTRANSFERID"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKTRANSFERID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                        dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                        dr["TRANSFERQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["TRANSFERQTY"]);
                        dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                        dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                        dr["TOTALMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALMRP"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["TOTALASSESMENTVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                        dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                        dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["NETWEIGHT"]);
                        dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                        decimal BillTaxAmt = 0;

                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables[3].Rows[k]["RELATEDTO"]))
                            {
                                case "1":

                                    TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = clsInvc.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]), this.txtreceiveddate.Text.Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                        }
                        #endregion

                        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim())));

                        #region Product Wise Tax Details
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            DataTable dtTaxComponentEdit = (DataTable)Session["DRTAXCOMPONENTDETAILS"];
                            for (int T = 0; T < ds.Tables[4].Rows.Count; T++)
                            {
                                DataRow drTax = dtTaxComponentEdit.NewRow();
                                drTax["SALEORDERID"] = Convert.ToString(ds.Tables[4].Rows[T]["SALEORDERID"]);
                                drTax["PRODUCTID"] = Convert.ToString(ds.Tables[4].Rows[T]["PRODUCTID"]);
                                drTax["BATCHNO"] = Convert.ToString(ds.Tables[4].Rows[T]["BATCHNO"]);
                                drTax["TAXID"] = Convert.ToString(ds.Tables[4].Rows[T]["TAXID"]);
                                drTax["PERCENTAGE"] = Convert.ToString(ds.Tables[4].Rows[T]["PERCENTAGE"]);
                                drTax["TAXVALUE"] = Convert.ToString(ds.Tables[4].Rows[T]["TAXVALUE"]);
                                drTax["MRP"] = Convert.ToString(ds.Tables[4].Rows[T]["MRP"]);
                                dtTaxComponentEdit.Rows.Add(drTax);
                                dtTaxComponentEdit.AcceptChanges();
                            }
                            HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
                        }
                        #endregion

                        dtreceivedrecord.Rows.Add(dr);
                        dtreceivedrecord.AcceptChanges();
                        TotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()) + TotalAmount;
                    }

                    #region Rejection Details
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.CreateRejectionTotalDataTable();
                        DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            DataRow dr = dttotalrejection.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["DEPOTRECEIVEDID"] = Convert.ToString(ds.Tables[2].Rows[i]["DEPOTRECEIVEDID"]);
                            dr["STOCKTRANSFERID"] = Convert.ToString(ds.Tables[2].Rows[i]["STOCKTRANSFERID"]);
                            dr["PRODUCTID"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTID"]);
                            dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTNAME"]);
                            dr["BATCHNO"] = Convert.ToString(ds.Tables[2].Rows[i]["BATCHNO"]);
                            dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[2].Rows[i]["REJECTIONQTY"]);
                            dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[2].Rows[i]["PACKINGSIZEID"]);
                            dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[2].Rows[i]["PACKINGSIZENAME"]);
                            dr["RATE"] = Convert.ToString(ds.Tables[2].Rows[i]["RATE"]);
                            dr["TOTALRATE"] = Convert.ToString(ds.Tables[2].Rows[i]["TOTALRATE"]);
                            dr["REASONID"] = Convert.ToString(ds.Tables[2].Rows[i]["REASONID"]);
                            dr["REASONNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["REASONNAME"]);
                            dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONID"]);
                            dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONNAME"]);
                            // Added By Avishek Ghosh On 18-03-2016
                            dr["MFDATE"] = Convert.ToString(ds.Tables[2].Rows[i]["MFDATE"]);
                            dr["EXPRDATE"] = Convert.ToString(ds.Tables[2].Rows[i]["EXPRDATE"]);
                            dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[2].Rows[i]["ASSESMENTPERCENTAGE"]);
                            dr["MRP"] = Convert.ToString(ds.Tables[2].Rows[i]["MRP"]);
                            dr["WEIGHT"] = Convert.ToString(ds.Tables[2].Rows[i]["WEIGHT"]);
                            dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[2].Rows[i]["DEPOTRATE1"]);

                            dttotalrejection.Rows.Add(dr);
                            dttotalrejection.AcceptChanges();
                        }
                        Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                    }
                    #endregion

                    #region Footer Details
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        txtbasicamt.Text = ds.Tables[5].Rows[0]["BASICAMT"].ToString();
                        txttaxamt.Text = ds.Tables[5].Rows[0]["TOTALTAXAMT"].ToString();
                        txttotal.Text = ds.Tables[5].Rows[0]["GROSSAMOUNT"].ToString();
                        txtTotalGross.Text = ds.Tables[5].Rows[0]["GROSSAMOUNT"].ToString();
                        this.txtnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtnetamt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim())));
                    }
                    #endregion

                    this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                    this.gvreceived.DataSource = dtreceivedrecord;
                    this.gvreceived.DataBind();
                    this.GridCalculation();
                    HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtreceivedrecord;

                    this.ddltransferno.Enabled = false;
                    this.ddlfromdepot.Enabled = false;
                    this.btnnewentry.Visible = false;
                    this.pnlAdd.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.btncancel.Visible = true;
                    this.pnlAdd.Enabled = true;
                    this.btnaddhide.Visible = false;
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Product details not found!</b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Depot receipt details not found!</b>");
            }
        }
        catch (Exception ex)
        {
            //string message = "alert('" + ex.Message.Replace("'", "") + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnRejectionNoteSubmit_Click
    // Added By Avishek Ghosh On 15-03-2016
    protected void btnRejectionNoteSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdn_receivedid.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectDepotReceived(receivedID, this.txtRejectionNote.Text.Trim());
            this.hdn_receivedid.Value = "";

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                LoadReceivedDetails();
                MessageBox1.ShowSuccess("Depot Stock Received: <b><font color='green'>" + this.txtdeporeceivedno.Text + "</font></b> rejected successfully.", 60, 500);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "block";
                this.fadeRejectionNote.Style["display"] = "block";

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

    #region btnRejectionCloseLightbox_Click
    // Added By Avishek Ghosh On 15-03-2016
    protected void btnRejectionCloseLightbox_Click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "none";
            this.fadeRejectionNote.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.txtRejectionNote.Text = "";
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region imgrejectionbtn_click
    protected void imgrejectionbtn_click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "none";
            this.fadeRejectionNote.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.txtRejectionNote.Text = "";
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindReceivedDetailsGrid
    public void BindReceivedDetailsGrid()
    {
        try
        {
            DataTable dtdeporeceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];
            gvreceived.DataSource = dtdeporeceived;
            gvreceived.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Depo
    public void LoadDepo()
    {
        ddlfromdepot.Items.Clear();
        ddlfromdepot.Items.Add(new ListItem("DEPOT NAME", "0"));
        ddlfromdepot.AppendDataBoundItems = true;
        ddlfromdepot.DataSource = clsdeporeceived.BindDepo();
        ddlfromdepot.DataValueField = "BRID";
        ddlfromdepot.DataTextField = "BRNAME";
        ddlfromdepot.DataBind();
    }
    #endregion

    #region LoadInsurance
    public void LoadInsurance()
    {
        try
        {
            ClsDepoReceived_FAC clsReceivedDepot = new ClsDepoReceived_FAC();
            this.ddlInsurance.Items.Clear();
            this.ddlInsurance.Items.Add(new ListItem("Insurance Co. Name", "0"));
            this.ddlInsurance.AppendDataBoundItems = true;
            this.ddlInsurance.DataSource = clsReceivedDepot.BindInsurancename();
            this.ddlInsurance.DataValueField = "ID";
            this.ddlInsurance.DataTextField = "COMPANY_NAME";
            this.ddlInsurance.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    #region Load Received Depo
    public void LoadReceivedDepo()
    {
        ddltodepot.Items.Clear();
        ddltodepot.Items.Add(new ListItem("-- SELECT DEPOT NAME --", "0"));
        ddltodepot.AppendDataBoundItems = true;
        ddltodepot.DataSource = clsdeporeceived.BindDepo();
        ddltodepot.DataValueField = "BRID";
        ddltodepot.DataTextField = "BRNAME";
        ddltodepot.DataBind();
    }
    #endregion

    #region Load Transfer No
    public void LoadTransferNo()
    {
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        string DepotID = string.Empty;
        if (Checker == "FALSE")
        {
            if (Session["TPU"].ToString().Trim() == "D" || Session["TPU"].ToString().Trim() == "EXPU")
            {
                DataTable dtDepot = new DataTable();
                dtDepot = clsdeporeceived.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);

                ddltodepot.Items.Clear();
                ddltodepot.Items.Add(new ListItem(Convert.ToString(dtDepot.Rows[0]["BRNAME"]), Convert.ToString(dtDepot.Rows[0]["BRID"])));
                ddltodepot.AppendDataBoundItems = true;
                ddltodepot.SelectedValue = Convert.ToString(dtDepot.Rows[0]["BRID"]);
            }
            else
            {
                DataTable dtDepot = new DataTable();
                dtDepot = clsdeporeceived.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);

                ddltodepot.Items.Clear();
                ddltodepot.Items.Add(new ListItem(Convert.ToString(dtDepot.Rows[0]["BRNAME"]), Convert.ToString(dtDepot.Rows[0]["BRID"])));
                ddltodepot.AppendDataBoundItems = true;
                ddltodepot.SelectedValue = Convert.ToString(dtDepot.Rows[0]["BRID"]);
            }
        }
        else
        {
            DepotID = this.hdnDepotID.Value.Trim();
        }
        ddltransferno.Items.Clear();
        ddltransferno.Items.Add(new ListItem("-- SELECT TRANSFER NO --", "0"));
        ddltransferno.AppendDataBoundItems = true;
        ddltransferno.DataSource = clsdeporeceived.BindTransferNo(ddltodepot.SelectedValue);
        ddltransferno.DataValueField = "STOCKDESPATCHID";
        ddltransferno.DataTextField = "STOCKTRANSFERNO";
        ddltransferno.DataBind();
    }
    #endregion

    #region LoadEditedTransferNo
    public void LoadEditedTransferNo()
    {
        try
        {
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            string DepotID = string.Empty;
            if (Checker == "FALSE")
            {
                DepotID = HttpContext.Current.Session["DEPOTID"].ToString();
            }
            else
            {
                DepotID = this.hdnDepotID.Value.Trim();
            }
            this.ddltransferno.Items.Clear();
            this.ddltransferno.Items.Add(new ListItem("-- SELECT TRANSFER NO --", "0"));
            this.ddltransferno.AppendDataBoundItems = true;
            this.ddltransferno.DataSource = clsdeporeceived.BindEditedTransferNo(DepotID);
            this.ddltransferno.DataValueField = "STOCKDESPATCHID";
            this.ddltransferno.DataTextField = "STOCKDESPATCHNO";
            this.ddltransferno.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadReason
    public void LoadReason()
    {
        ddlrejectionreason.Items.Clear();
        ddlrejectionreason.Items.Add(new ListItem("-- SELECT REJECTION REASON NAME --", "0"));
        ddlrejectionreason.AppendDataBoundItems = true;
        ddlrejectionreason.DataSource = clsdeporeceived.BindReason(Request.QueryString["MENUID"].ToString());
        ddlrejectionreason.DataValueField = "ID";
        ddlrejectionreason.DataTextField = "DESCRIPTION";
        ddlrejectionreason.DataBind();
    }
    #endregion

    #region ddltransferno_SelectedIndexChanged
    protected void ddltransferno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltransferno.SelectedValue == "0")
            {
                this.hdnSaleOrderID.Value = "";
                this.hdnSaleOrderNo.Value = "";
                this.ddlfromdepot.SelectedValue = "0";
                this.txttransferdate.Text = "";
                this.txtwaybillno.Text = "";
                this.txtinsuranceno.Text = "";
                this.txttranspoter.Text = "";
                this.txtmodeoftransport.Text = "";
                this.txtvehicleno.Text = "";
                this.txtlrgrno.Text = "";
                this.txtlrgrdate.Text = "";
                this.txtchallanno.Text = "";
                this.txtchallandate.Text = "";
                this.txtfformno.Text = "";
                this.txtfformdate.Text = "";
                this.txtgateno.Text = "";
                this.txtgatedate.Text = "";
                this.txtTotalCase.Text = "";
                this.txtTotalPCS.Text = "";
                this.txtdeliverydate.Text = "";
                this.ddlInsurance.SelectedValue = "0";
                this.gvreceived.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvreceived.DataSource = null;
                this.gvreceived.DataBind();
                //this.txtgateentryno.Text= "";
                //this.txtgateenrydate.Text ="";
                DataTable dtreceived = (DataTable)Session["DEPORECEIVEDRECORD"];
                dtreceived.Clear();
                Session["DEPORECEIVEDRECORD"] = dtreceived;
            }
            else
            {
                HttpContext.Current.Session["DEPORECEIVEDRECORD"] = null;
                if (Session["DEPORECEIVEDRECORD"] == null)
                {
                    this.CreateReceivedGrid();
                    this.CreateDataTableTaxComponent();//For Tax Calculation Method
                    this.CreateRejectionTotalDataTable();//For Rejection Method
                    this.CreateRejectionInnerGridDataTable();// Creating Rejection Inner Grid DataTable Structure
                }
                
                DataTable dttransferinfo = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];
                DataSet ds = new DataSet();
                decimal TotalAmount = 0;
                string TAXID = string.Empty;
                decimal ProductWiseTax = 0;
                clsSaleInvoice clsInvc = new clsSaleInvoice();
                string transferID = Convert.ToString(ddltransferno.SelectedValue).Trim();
                ds = clsdeporeceived.BindTransferInfo(transferID);
                this.gvreceived.DataSource = null;
                this.gvreceived.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlfromdepot.SelectedValue = ds.Tables[0].Rows[0]["MOTHERDEPOTID"].ToString();
                    txttransferdate.Text = ds.Tables[0].Rows[0]["STOCKDESPATCHDATE"].ToString();
                    txtwaybillno.Text = ds.Tables[0].Rows[0]["WAYBILLNO"].ToString();
                    this.hdnWaybillKey.Value = ds.Tables[0].Rows[0]["WAYBILLKEY"].ToString();
                    txtinsuranceno.Text = ds.Tables[0].Rows[0]["INSURANCENUMBER"].ToString();
                    txttranspoter.Text = ds.Tables[0].Rows[0]["TRANSPORTERNAME"].ToString();
                    txtmodeoftransport.Text = ds.Tables[0].Rows[0]["MODEOFTRANSPORT"].ToString();
                    txtvehicleno.Text = ds.Tables[0].Rows[0]["VEHICHLENO"].ToString();
                    txtlrgrno.Text = ds.Tables[0].Rows[0]["LRGRNO"].ToString();
                    txtlrgrdate.Text = ds.Tables[0].Rows[0]["LRGRDATE"].ToString();
                    txtchallanno.Text = ds.Tables[0].Rows[0]["CHALLANNO"].ToString();
                    txtchallandate.Text = ds.Tables[0].Rows[0]["CHALLANDATE"].ToString();
                    LoadInsurance();
                    ddlInsurance.SelectedValue = ds.Tables[0].Rows[0]["INSURANCECOMPID"].ToString();
                    txtfformno.Text = ds.Tables[0].Rows[0]["FFORMNO"].ToString();
                    this.txtdeliverydate.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["FFORMDATE"].ToString() == "01/01/1900")
                    {
                        txtfformdate.Text = "";
                    }
                    else
                    {
                        txtfformdate.Text = ds.Tables[0].Rows[0]["FFORMDATE"].ToString();
                    }

                    this.hdnTransporterID.Value = ds.Tables[0].Rows[0]["TRANSPORTERID"].ToString();
                    this.txtTotalCase.Text = ds.Tables[0].Rows[0]["TOTALCASE"].ToString();
                    this.txtTotalPCS.Text = ds.Tables[0].Rows[0]["TOTALPCS"].ToString();

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtDRTaxCount"];
                    ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                    }
                    #endregion

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        this.hdnSaleOrderID.Value = ds.Tables[1].Rows[0]["SALEORDERID"].ToString();
                        this.hdnSaleOrderNo.Value = ds.Tables[1].Rows[0]["SALEORDERNO"].ToString();
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            DataRow dr = dttransferinfo.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            string TaxGuid = dr["GUID"].ToString().Trim();
                            dr["STOCKTRANSFERID"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKDESPATCHID"]);
                            dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                            dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                            dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                            dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                            dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                            dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                            dr["TRANSFERQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["TRANSFERQTY"]);
                            dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                            dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                            dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]);
                            dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                            dr["TOTALMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]);
                            dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                            dr["TOTALASSESMENTVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                            dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);
                            dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                            dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                            dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                            TotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()) + TotalAmount;
                            decimal BillTaxAmt = 0;

                            #region Loop For Adding Itemwise Tax Component
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                                {
                                    switch (Convert.ToString(ds.Tables[2].Rows[k]["RELATEDTO"]))
                                    {
                                        case "1":

                                            TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                            ProductWiseTax = clsInvc.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]), this.txtreceiveddate.Text.Trim());
                                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                            BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                            break;
                                    }

                                    CreateTaxDatatable("0",
                                           Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim(),
                                           Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim(),
                                           dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                           Convert.ToString(ProductWiseTax),
                                           dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                           TAXID, TaxGuid, Convert.ToString(ds.Tables[1].Rows[i]["TOTALMRP"]));
                                }
                            }
                            #endregion

                            dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim())));
                            dttransferinfo.Rows.Add(dr);
                            dttransferinfo.AcceptChanges();
                        }
                        this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));

                        gvreceived.DataSource = dttransferinfo;
                        gvreceived.DataBind();
                        GridCalculation();
                        HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dttransferinfo;
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        txtbasicamt.Text = ds.Tables[4].Rows[0]["BASICAMT"].ToString();
                        txttaxamt.Text = ds.Tables[4].Rows[0]["TOTALTAXAMT"].ToString();
                        txttotal.Text = ds.Tables[4].Rows[0]["GROSSAMOUNT"].ToString();
                        txtTotalGross.Text = ds.Tables[4].Rows[0]["GROSSAMOUNT"].ToString();

                        this.txtnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtnetamt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim())));
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Product details not found</font></b>");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Waybill No</font></b> not found, please updated before Depot received", 80, 500);
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

    #region Create DataTable Structure
    public DataTable CreateReceivedGrid()
    {
        DataTable dtdeporeceived = new DataTable();
        dtdeporeceived.Clear();
        dtdeporeceived.Columns.Add("GUID");                 //2
        dtdeporeceived.Columns.Add("STOCKTRANSFERID");      //3       
        dtdeporeceived.Columns.Add("PRODUCTID");            //4
        dtdeporeceived.Columns.Add("HSNCODE");              //5 Add By Rajeev (26-06-2017)
        dtdeporeceived.Columns.Add("PRODUCTNAME");          //6
        dtdeporeceived.Columns.Add("PACKINGSIZEID");        //7
        dtdeporeceived.Columns.Add("PACKINGSIZENAME");      //8
        dtdeporeceived.Columns.Add("BATCHNO");              //9
        dtdeporeceived.Columns.Add("TRANSFERQTY");          //10
        dtdeporeceived.Columns.Add("RECEIVEDQTY");          //11
        dtdeporeceived.Columns.Add("MRP");                  //12
        dtdeporeceived.Columns.Add("RATE");                 //13
        dtdeporeceived.Columns.Add("AMOUNT");               //14
        dtdeporeceived.Columns.Add("TOTALMRP");             //15
        dtdeporeceived.Columns.Add("ASSESMENTPERCENTAGE");  //16
        dtdeporeceived.Columns.Add("TOTALASSESMENTVALUE");  //17
        dtdeporeceived.Columns.Add("NETWEIGHT");            //18
        dtdeporeceived.Columns.Add("GROSSWEIGHT");          //19
        dtdeporeceived.Columns.Add("MFDATE");               //20
        dtdeporeceived.Columns.Add("EXPRDATE");             //21

        #region Loop For Adding Itemwise Tax Component (Add By Rajeev)
        DataSet ds = new DataSet();
        //string TransferID = Convert.ToString(hdn_receivedid.Value).Trim();       
        ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
        if (this.hdn_receivedid.Value.Trim() == "")
        {
            ds = clsdeporeceived.BindTransferInfo(ddltransferno.SelectedValue.Trim());
            Session["dtDRTaxCount"] = ds.Tables[2];
            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
            {
                dtdeporeceived.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dtdeporeceived.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[2].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }
        else
        {
            ds = clsdeporeceived.BindTransferReceivedInfo(this.hdn_receivedid.Value.Trim());
            Session["dtDRTaxCount"] = ds.Tables["TaxCountDetails"];
            for (int k = 0; k < ds.Tables["TaxCountDetails"].Rows.Count; k++)
            {
                dtdeporeceived.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCountDetails"].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                dtdeporeceived.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables["TaxCountDetails"].Rows[k]["NAME"]) + "", typeof(string)));
            }
        }

        #endregion

        dtdeporeceived.Columns.Add("NETAMOUNT");             
        
        HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtdeporeceived;
        return dtdeporeceived;
    }
    #endregion   
 
    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();        
        if (hdn_transferid.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("TAXGUID", typeof(string)));
            dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"] = dt;
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("TAXGUID", typeof(string)));
            dt.Columns.Add(new DataColumn("SALEORDERID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"] = dt;
        }
        return dt;
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string SALEORDERID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string TAXID, string TaxGuid,string MRP)
    {
        DataTable dt = (DataTable)Session["DRTAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["TAXGUID"] = TaxGuid;
        dr["SALEORDERID"] = SALEORDERID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        dr["TAXID"] = TAXID;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["MRP"] = MRP;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion

    #region Create Rejection InnerGrid DataTable Structure
    public DataTable CreateRejectionInnerGridDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKTRANSFERID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTALRATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));
        //Added By Avishek Ghosh On 18-03-2016
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
        //Added By Avishek Ghosh On 04-04-2016
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));

        HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dt;

        return dt;
    }
    #endregion

    #region Create Rejection Total DataTable Structure
    public DataTable CreateRejectionTotalDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKTRANSFERID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
        dt.Columns.Add(new DataColumn("REJECTIONQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZEID", typeof(string)));
        dt.Columns.Add(new DataColumn("PACKINGSIZENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTALRATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REASONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));
        //Added By Avishek Ghosh On 18-03-2016
        dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("WEIGHT", typeof(string)));
        //Added By Avishek Ghosh On 04-04-2016
        dt.Columns.Add(new DataColumn("DEPOTRATE1", typeof(string)));
        HttpContext.Current.Session["TOTALREJECTIONDETAILS"] = dt;
        return dt;
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

    #region ConvertDatatableToXMLItemWiseTaxDetails
    public string ConvertDatatableToXMLItemWiseTaxDetails(DataTable dt)
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

    #region btnsave_Click
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            int flag = 0;
            decimal TotalCase = 0;
            decimal TotalPCS = 0;
            DataTable dtdepotreceived = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];
            DataTable dtrejectiondetails = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];

            for (int i = 0; i < dtdepotreceived.Rows.Count; i++)
            {
                string ReceivedPacksizeid = dtdepotreceived.Rows[i]["PACKINGSIZEID"].ToString();
                string BatchNO = Convert.ToString(dtdepotreceived.Rows[i]["BATCHNO"].ToString());
                string ProductID = Convert.ToString(dtdepotreceived.Rows[i]["PRODUCTID"].ToString());

                decimal transferqty = Convert.ToDecimal(dtdepotreceived.Rows[i]["TRANSFERQTY"].ToString());
                decimal receivedqty = Convert.ToDecimal(dtdepotreceived.Rows[i]["TRANSFERQTY"].ToString().Trim());

                decimal totalrejectionqty = 0;
                decimal rejectionqty = 0;

                if (dtrejectiondetails != null)
                {
                    if (dtrejectiondetails.Rows.Count > 0)
                    {
                        for (int p = 0; p < dtrejectiondetails.Rows.Count; p++)
                        {
                            if (dtrejectiondetails.Rows[p]["PRODUCTID"].ToString().Trim() == ProductID && dtrejectiondetails.Rows[p]["BATCHNO"].ToString().Trim() == BatchNO)
                            {
                                rejectionqty = clsdeporeceived.ConvertionQty(ProductID, dtrejectiondetails.Rows[p]["PACKINGSIZEID"].ToString(), ReceivedPacksizeid, Convert.ToDecimal(dtrejectiondetails.Rows[p]["REJECTIONQTY"]));
                            }
                            totalrejectionqty = totalrejectionqty + rejectionqty;
                        }
                    }
                }
                if (totalrejectionqty > receivedqty)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Rejection qty more than received qty</font></b> for product <b><font color='green'>" + dtdepotreceived.Rows[i]["PRODUCTNAME"] + "</font></b>!", 80, 750);
                    flag = 1;
                    break;
                }
            }

            if (flag == 0)
            {
                string stockreceivedno = string.Empty;
                string mode = string.Empty;
                string xmlRejectionDetails = string.Empty;
                string xmlTax = string.Empty;

                DataTable dtRejectionDetailsTotal = (DataTable)HttpContext.Current.Session["TOTALREJECTIONDETAILS"];
                dtreceivedrecord = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];
                DataTable dtTaxComponent = (DataTable)HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"];
                if (dtreceivedrecord.Rows.Count > 0)
                {
                    for (int i = 0; i < dtreceivedrecord.Rows.Count; i++)
                    {
                        decimal receivedqty = Convert.ToDecimal(dtdepotreceived.Rows[i]["RECEIVEDQTY"].ToString().Trim());
                        dtreceivedrecord.Rows[i]["RECEIVEDQTY"] = Convert.ToDecimal(receivedqty);
                        dtreceivedrecord.AcceptChanges();
                    }
                    HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtreceivedrecord;
                    int receiveddate = Convert.ToInt32(Conver_To_ISO(this.txtreceiveddate.Text.Trim()));
                    int despatchdate = Convert.ToInt32(Conver_To_ISO(this.txttransferdate.Text.Trim()));

                    if (despatchdate > receiveddate)
                    {
                        MessageBox1.ShowWarning("Received date can not less than StockTransfer date: <b><font color='red'> " + txttransferdate.Text + "</font></b>!", 80, 550);
                    }
                    else
                    {
                        if (dtRejectionDetailsTotal != null)
                        {
                            if (dtRejectionDetailsTotal.Rows.Count > 0)
                            {
                                xmlRejectionDetails = ConvertDatatableToXML(dtRejectionDetailsTotal);
                            }
                        }
                        string xml = ConvertDatatableToXML(dtreceivedrecord);
                        xmlTax = ConvertDatatableToXMLItemWiseTaxDetails(dtTaxComponent);
                        MENUID = Request.QueryString["MENUID"].ToString().Trim();

                        if (this.txtTotalCase.Text.Trim() != "")
                        {
                            TotalCase = Convert.ToDecimal(this.txtTotalCase.Text.Trim());
                        }
                        if (this.txtTotalPCS.Text.Trim() != "")
                        {
                            TotalPCS = Convert.ToDecimal(this.txtTotalPCS.Text.Trim());
                        }
                        /*Add Some Parameter For Depot Footer Insert (Rajeev)*/
                        stockreceivedno = clsdeporeceived.InsertReceivedDetails(txtreceiveddate.Text.Trim(), ddltodepot.SelectedValue.ToString(), ddltodepot.SelectedItem.Text,
                                                                                ddlfromdepot.SelectedValue.ToString(), ddlfromdepot.SelectedItem.Text, txttransferdate.Text, txtwaybillno.Text.Trim(),
                                                                                this.hdnWaybillKey.Value.Trim(), txtinsuranceno.Text, txttranspoter.Text, txtmodeoftransport.Text, txtvehicleno.Text,
                                                                                txtlrgrno.Text, txtlrgrdate.Text, txtchallanno.Text.Trim(), txtchallandate.Text.Trim(),
                                                                                HttpContext.Current.Session["UserID"].ToString(),
                                                                                HttpContext.Current.Session["FINYEAR"].ToString(), ddltransferno.SelectedValue,
                                                                                Convert.ToString(hdn_receivedid.Value), xml, xmlRejectionDetails, xmlTax, txtfformno.Text.Trim(),
                                                                                txtfformdate.Text.Trim(), txtgateno.Text.Trim(), txtgatedate.Text.Trim(),
                                                                                this.txtRemarks.Text.Trim(), this.ddlInsurance.SelectedValue.ToString(),
                                                                                this.ddlInsurance.SelectedItem.ToString().Trim(), this.hdnTransporterID.Value.Trim(),
                                                                                MENUID, this.hdnSaleOrderID.Value.Trim(), this.hdnSaleOrderNo.Value.Trim(), TotalCase, TotalPCS, Convert.ToDecimal(txtTotalGross.Text),
                                                                                Convert.ToDecimal(txtRoundoff.Text), Convert.ToDecimal(txtnetamt.Text), Convert.ToDecimal(txttotal.Text), Convert.ToDecimal(txtbasicamt.Text),
                                                                                Convert.ToInt16(ViewState["Invoice_Type"]), "N", this.txtdeliverydate.Text.Trim()); 

                        if (stockreceivedno != "")
                        {
                            if (Convert.ToString(hdn_receivedid.Value) == "")
                            {
                                MessageBox1.ShowSuccess("Depot receipt No : <b><font color='green'> " + stockreceivedno + "</font></b> saved successfully", 60, 600);
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("Depot receipt No : <b><font color='green'> " + stockreceivedno + "</font></b> updated successfully", 60, 600);
                            }
                        }
                        else
                        {
                            MessageBox1.ShowError("<b><font color='red'>Error on Saving record!</font></b>");
                        }
                        clsdeporeceived.ResetDataTables();   // Reset all Datatables
                        this.hdn_receivedid.Value = "";
                        this.btnnewentry.Visible = true;
                        this.LoadReceivedDetails();
                        this.ResetTable();
                        this.pnlAdd.Style["display"] = "none";
                        this.pnlDisplay.Style["display"] = "";
                        this.txtdeporeceivedno.Text = "";
                        this.txtgateno.Text = "";
                        this.txtgatedate.Text = "";
                        this.divbtnsave.Visible = false;
                        this.btncancel.Visible = false;
                        this.btnnewentry.Visible = true;
                        this.pnlAdd.Enabled = false;
                        this.hdn_receivedid.Value = "";
                        this.hdn_stocktransferid.Value = "";
                        this.hdn_lockreceivedqty.Value = "0";
                        this.hdn_guid.Value = "";
                        this.ClearControls();
                        this.gvreceived.DataSource = null;
                        this.gvreceived.DataBind();
                        this.btnaddhide.Visible = true;
                        this.txtdeliverydate.Text = "";
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Please add atleast 1 record!</b>");
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

    public void ResetTable()
    {
        dtreceivedrecord.Clear();       
    }

    #region Clear Control
    public void ClearControls()
    {
        //this.ddlfromdepot.Items.Clear();
        //this.ddlfromdepot.Items.Add(new ListItem("DEPOT NAME", "0"));
        //this.ddlfromdepot.AppendDataBoundItems = true;
        //this.ddltodepot.Items.Clear();
        //this.ddltodepot.Items.Add(new ListItem("-- SELECT DEPOT NAME --", "0"));
        //this.ddltodepot.AppendDataBoundItems = true;
        this.ddlInsurance.SelectedValue = "0";
        this.txttransferdate.Text = "";
        this.txtwaybillno.Text = "";
        this.txtinsuranceno.Text = "";
        this.txttranspoter.Text = "";
        this.txtmodeoftransport.Text = "";
        this.txtvehicleno.Text = "";
        this.txtlrgrno.Text = "";
        this.txtchallanno.Text = "";        
        this.txtlrgrdate.Text = "";
        this.txtchallandate.Text = "";
        this.txtfformno.Text = "";
        this.txtfformdate.Text = "";
        this.txtgateno.Text = "";
        this.txtgatedate.Text = "";
        this.txtRemarks.Text = "";
        this.txtCheckerNote.Text = "";
        this.hdnTransporterID.Value = "";
        this.hdnSaleOrderNo.Value = "";
        this.hdnSaleOrderID.Value = "";
        this.txtTotalCase.Text = "";
        this.txtTotalPCS.Text = "";
        this.txtbasicamt.Text = "";
        this.txttaxamt.Text = "";
        this.txttotal.Text = "";
        this.txtTotalGross.Text = "";
        this.txtRoundoff.Text = "";
        this.txtnetamt.Text = "";
        this.txtFinalAmt.Text = "";
        this.DateLock();

        Session.Remove("REJECTIONDINNERGRIDDETAILS");
        Session.Remove("TOTALREJECTIONDETAILS");
        Session.Remove("DEPORECEIVEDRECORD");
        Session.Remove("DRTAXCOMPONENTDETAILS");
    }
    #endregion

    protected void btnSTfind_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReceivedDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadReceivedDetails()
    {
        try
        {
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            gvdepotreceivedDetails.DataSource = clsdeporeceived.BindReceivedControl(txtfromdateins.Text, txttodateins.Text, HttpContext.Current.Session["DEPOTID"].ToString(), Checker, Convert.ToString(Session["IUserID"]), HttpContext.Current.Session["FINYEAR"].ToString());
            gvdepotreceivedDetails.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region btncancel_Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {            
            this.hdnSaleOrderNo.Value = "";
            this.hdnSaleOrderID.Value = "";
            hdn_receivedid.Value = "";
            hdn_stocktransferid.Value = "";
            hdn_lockreceivedqty.Value = "0";
            hdn_guid.Value = "";
            btnnewentry.Visible = true;
            LoadReceivedDetails();
            clsdeporeceived.ResetDataTables();   // Reset all Datatables
            ResetTable();            
            this.gvreceived.DataSource = null;
            this.gvreceived.DataBind();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            this.ClearControls();
            this.btnaddhide.Visible = true;
            this.txtdeliverydate.Text = "";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                btnaddhide.Style["display"] = "none";
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                btnaddhide.Style["display"] = "";
            }
            #endregion            
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
            this.hdnSaleOrderNo.Value = "";
            this.hdnSaleOrderID.Value = "";
            this.txtTotalCase.Text = "";
            this.txtTotalPCS.Text = "";

            LoadDepo();
            LoadTransferNo();
            LoadReason();
            imgPopuppodate.Visible = true;
            ddltransferno.Enabled = true;
            btnnewentry.Visible = false;
            pnlAdd.Enabled = true;
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            this.trreceivedno.Style["display"] = "none";
            divbtnsave.Visible = true;
            btncancel.Visible = true;
            ResetTable();
            clsdeporeceived.ResetDataTables();
            this.ClearControls();
            dtreceivedrecord = null;
            gvreceived.DataSource = null;
            gvreceived.DataBind();
            txtdeporeceivedno.Text = "";
            ddltransferno.SelectedValue = "0";
            hdn_receivedid.Value = "";
            hdn_stocktransferid.Value = "";
            hdn_lockreceivedqty.Value = "0";
            hdn_guid.Value = "";
            HttpContext.Current.Session["DEPORECEIVEDRECORD"] = null;
            //this.CreateReceivedGrid();
            this.btnaddhide.Visible = false;
            this.txtdeliverydate.Text = "";
            //this.txtgateentryno.Enabled = true;
            //this.txtgateenrydate.Enabled = true;

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                this.divbtnsave.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = true;
            }
            #endregion
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnApprove_Click
    // Added By Avishek Ghosh On 16-03-2016
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
            int flag = 0;
            string receivedID = Convert.ToString(hdn_receivedid.Value).Trim();
            flag = clsPurchaseStockReceipt.ApproveStockDepotReceived(receivedID, Session["FINYEAR"].ToString().Trim(), Session["USERID"].ToString().Trim(), this.txtdeporeceivedno.Text.Trim(),this.ddlInsurance.SelectedValue.Trim(),this.ddlInsurance.SelectedItem.ToString().Trim(),this.txttransferdate.Text.Trim(),this.txtinsuranceno.Text.Trim(),this.ddltodepot.SelectedValue.Trim(),this.ddltodepot.SelectedItem.ToString().Trim());
            this.hdn_receivedid.Value = "";

            if (flag == 1)
            {
                pnlDisplay.Style["display"] = "";
                pnlAdd.Style["display"] = "none";
                LoadReceivedDetails();
                MessageBox1.ShowSuccess("Depot Stock Received: <b><font color='green'>" + this.txtdeporeceivedno.Text + "</font></b> approved and accounts entry(s) passed successfully.", 60, 700);
            }
            else if (flag == 0)
            {
                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
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
    // Added By Avishek Ghosh On 11-03-2016
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            this.lightRejectionNote.Style["display"] = "block";
            this.fadeRejectionNote.Style["display"] = "block";
            this.pnlAdd.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Edit Depot Received
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {            
            string TAXID = string.Empty;
            decimal ProductWiseTax = 0;
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            string DepotID = string.Empty;
           
            //if (Checker == "FALSE")
            //{
            //    if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "1")
            //    {
            //        MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
            //        return;
            //    }
            //}
            if (Checker == "FALSE")
            {                
                if (Session["TPU"].ToString().Trim() == "F" || Session["TPU"].ToString().Trim() == "EXPU")
                {
                    DepotID = HttpContext.Current.Session["DEPOTID"].ToString();
                }
                else
                {
                    DataTable dtDepot = new DataTable();
                    dtDepot = clsdeporeceived.BindDepotBasedOnUser(Convert.ToString(Session["IUserID"]).Trim());
                    DepotID = Convert.ToString(dtDepot.Rows[0]["BRID"]);
                }
            }
            else
            {
                DepotID = this.hdnDepotID.Value.Trim();
            }
            LoadReceivedDepo();
            ddltodepot.SelectedValue = DepotID;
            LoadDepo();
            HttpContext.Current.Session["DEPORECEIVEDRECORD"] = null;
            if (Session["DEPORECEIVEDRECORD"] == null)
            {
                this.CreateReceivedGrid();
            }
            HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"] = null;
            if (Session["DRTAXCOMPONENTDETAILS"] == null)
            {
                this.CreateDataTableTaxComponent();
            }
            this.LoadReason();
            this.trreceivedno.Style["display"] = "";
            
            dtreceivedrecord = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];

            #region QueryString
            
            if (Checker == "TRUE")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtRemarks.Enabled = false;
            }
            else
            {
                if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "1" && clsdeporeceived.GetFinancestatus(this.hdn_receivedid.Value.Trim()) == "1")
                {
                    this.divbtnsave.Visible = false;
                    this.gvreceived.Columns[1].Visible = false;
                }
                else if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "0" && clsdeporeceived.GetFinancestatus(this.hdn_receivedid.Value.Trim()) == "0")
                {
                    this.divbtnsave.Visible = true;
                    this.gvreceived.Columns[1].Visible = true;
                }
                else
                {
                    this.divbtnsave.Visible = false;
                    this.gvreceived.Columns[1].Visible = false;
                }
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtRemarks.Enabled = true;
            }
            #endregion

            string receivedid = hdn_receivedid.Value.ToString();

            DataSet ds = new DataSet();  
            ds = clsdeporeceived.BindTransferReceivedInfo(receivedid);
            decimal TotalAmount = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hdnSaleOrderID.Value = ds.Tables[0].Rows[0]["SALEORDERID"].ToString();
                this.hdnSaleOrderNo.Value = ds.Tables[0].Rows[0]["SALEORDERNO"].ToString();
                txtdeporeceivedno.Text = ds.Tables[0].Rows[0]["STOCKDEPORECEIVEDNO"].ToString();
                this.LoadEditedTransferNo();
                ddltransferno.SelectedItem.Text= ds.Tables[0].Rows[0]["STOCKDESPATCHNO"].ToString();
                txttransferdate.Text = ds.Tables[0].Rows[0]["STOCKTRANSFERDATE"].ToString();
                txtwaybillno.Text = ds.Tables[0].Rows[0]["WAYBILLNO"].ToString();
                this.hdnWaybillKey.Value = ds.Tables[0].Rows[0]["WAYBILLKEY"].ToString();
                txttranspoter.Text = ds.Tables[0].Rows[0]["TRANSPORTERNAME"].ToString();
                txtinsuranceno.Text = ds.Tables[0].Rows[0]["INSURANCENO"].ToString();
                txtmodeoftransport.Text = ds.Tables[0].Rows[0]["MODEOFTRANSPORT"].ToString();
                txtreceiveddate.Text = ds.Tables[0].Rows[0]["STOCKDEPORECEIVEDDATE"].ToString();
                ddlfromdepot.SelectedValue = ds.Tables[0].Rows[0]["MOTHERDEPOTID"].ToString();
                ddltodepot.SelectedValue = ds.Tables[0].Rows[0]["RECEIVEDDEPOTID"].ToString();
                txtvehicleno.Text = ds.Tables[0].Rows[0]["VEHICHLENO"].ToString();
                txtlrgrno.Text = ds.Tables[0].Rows[0]["LRGRNO"].ToString();
                txtlrgrdate.Text = ds.Tables[0].Rows[0]["LRGRDATE"].ToString();
                txtchallanno.Text = ds.Tables[0].Rows[0]["CHALLANNO"].ToString();
                txtchallandate.Text = ds.Tables[0].Rows[0]["CHALLANDATE"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                LoadInsurance();
                //this.FetchLedger();
                ddlInsurance.SelectedValue = ds.Tables[0].Rows[0]["INSURANCECOMPID"].ToString();
                this.txtCheckerNote.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();
                txtfformno.Text = ds.Tables[0].Rows[0]["FFORMNO"].ToString();
                this.txtdeliverydate.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString().Trim();
                if (ds.Tables[0].Rows[0]["FFORMDATE"].ToString() == "01/01/1900")
                {
                    txtfformdate.Text = "";
                }
                else
                {
                    txtfformdate.Text = ds.Tables[0].Rows[0]["FFORMDATE"].ToString();
                }
                
                txtgateno.Text = ds.Tables[0].Rows[0]["GATEPASSNO"].ToString();
                if (ds.Tables[0].Rows[0]["GATEPASSDATE"].ToString() == "01/01/1900")
                {
                    txtgatedate.Text = "";
                }
                else
                {
                    txtgatedate.Text = ds.Tables[0].Rows[0]["GATEPASSDATE"].ToString();
                }

                this.hdnTransporterID.Value = ds.Tables[0].Rows[0]["TRANSPORTERID"].ToString();
                this.txtTotalCase.Text = ds.Tables[0].Rows[0]["TOTALCASE"].ToString();
                this.txtTotalPCS.Text = ds.Tables[0].Rows[0]["TOTALPCS"].ToString();
                
                #region Loop For Adding Itemwise Tax Component
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtDRTaxCount"];
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                }
                #endregion
                
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtreceivedrecord.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        string TaxGuid = dr["GUID"].ToString().Trim();
                        dr["STOCKTRANSFERID"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKTRANSFERID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                        dr["HSNCODE"] = Convert.ToString(ds.Tables[1].Rows[i]["HSNCODE"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                        dr["TRANSFERQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["TRANSFERQTY"]);
                        dr["RECEIVEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["RECEIVEDQTY"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                        dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                        dr["TOTALMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALMRP"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["TOTALASSESMENTVALUE"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                        dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                        dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["NETWEIGHT"]);
                        dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]);
                        decimal BillTaxAmt = 0;
                        
                        #region Loop For Adding Itemwise Tax Component
                        for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                        {
                            switch (Convert.ToString(ds.Tables[3].Rows[k]["RELATEDTO"]))
                            {
                                case "1":

                                    TAXID = clsInvc.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                                    ProductWiseTax = clsInvc.GetHSNTax(TAXID, Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]), this.txtreceiveddate.Text.Trim());
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                                    dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(dr["AMOUNT"].ToString().Trim()) * ProductWiseTax / 100));
                                    BillTaxAmt += Convert.ToDecimal(dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""]);
                                    break;
                            }
                        }
                        #endregion
                        
                        dr["NETAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", BillTaxAmt + Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim())));

                        #region Product Wise Tax Details
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            DataTable dtTaxComponentEdit = (DataTable)Session["DRTAXCOMPONENTDETAILS"];
                            for (int T = 0; T < ds.Tables[4].Rows.Count; T++)
                            {
                                DataRow drTax = dtTaxComponentEdit.NewRow();
                                drTax["SALEORDERID"] = Convert.ToString(ds.Tables[4].Rows[T]["SALEORDERID"]);
                                drTax["PRODUCTID"] = Convert.ToString(ds.Tables[4].Rows[T]["PRODUCTID"]);
                                drTax["BATCHNO"] = Convert.ToString(ds.Tables[4].Rows[T]["BATCHNO"]);
                                drTax["TAXID"] = Convert.ToString(ds.Tables[4].Rows[T]["TAXID"]);
                                drTax["PERCENTAGE"] = Convert.ToString(ds.Tables[4].Rows[T]["PERCENTAGE"]);
                                drTax["TAXVALUE"] = Convert.ToString(ds.Tables[4].Rows[T]["TAXVALUE"]);
                                drTax["MRP"] = Convert.ToString(ds.Tables[4].Rows[T]["MRP"]);
                                dtTaxComponentEdit.Rows.Add(drTax);
                                dtTaxComponentEdit.AcceptChanges();
                            }
                            HttpContext.Current.Session["DRTAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
                        }
                        #endregion

                        dtreceivedrecord.Rows.Add(dr);
                        dtreceivedrecord.AcceptChanges();
                        TotalAmount = Convert.ToDecimal(ds.Tables[1].Rows[i]["AMOUNT"].ToString().Trim()) + TotalAmount;
                    }

                    #region Rejection Details
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        this.CreateRejectionTotalDataTable();
                        DataTable dttotalrejection = (DataTable)Session["TOTALREJECTIONDETAILS"];

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            DataRow dr = dttotalrejection.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["DEPOTRECEIVEDID"] = Convert.ToString(ds.Tables[2].Rows[i]["DEPOTRECEIVEDID"]);
                            dr["STOCKTRANSFERID"] = Convert.ToString(ds.Tables[2].Rows[i]["STOCKTRANSFERID"]);
                            dr["PRODUCTID"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTID"]);
                            dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTNAME"]);
                            dr["BATCHNO"] = Convert.ToString(ds.Tables[2].Rows[i]["BATCHNO"]);
                            dr["REJECTIONQTY"] = Convert.ToString(ds.Tables[2].Rows[i]["REJECTIONQTY"]);
                            dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[2].Rows[i]["PACKINGSIZEID"]);
                            dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[2].Rows[i]["PACKINGSIZENAME"]);
                            dr["RATE"] = Convert.ToString(ds.Tables[2].Rows[i]["RATE"]);
                            dr["TOTALRATE"] = Convert.ToString(ds.Tables[2].Rows[i]["TOTALRATE"]);
                            dr["REASONID"] = Convert.ToString(ds.Tables[2].Rows[i]["REASONID"]);
                            dr["REASONNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["REASONNAME"]);
                            dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONID"]);
                            dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONNAME"]);
                            // Added By Avishek Ghosh On 18-03-2016
                            dr["MFDATE"] = Convert.ToString(ds.Tables[2].Rows[i]["MFDATE"]);
                            dr["EXPRDATE"] = Convert.ToString(ds.Tables[2].Rows[i]["EXPRDATE"]);
                            dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[2].Rows[i]["ASSESMENTPERCENTAGE"]);
                            dr["MRP"] = Convert.ToString(ds.Tables[2].Rows[i]["MRP"]);
                            dr["WEIGHT"] = Convert.ToString(ds.Tables[2].Rows[i]["WEIGHT"]);
                            dr["DEPOTRATE1"] = Convert.ToString(ds.Tables[2].Rows[i]["DEPOTRATE1"]);

                            dttotalrejection.Rows.Add(dr);
                            dttotalrejection.AcceptChanges();
                        }
                        Session["TOTALREJECTIONDETAILS"] = dttotalrejection;
                    }
                    #endregion

                    #region Footer Details
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        txtbasicamt.Text = ds.Tables[5].Rows[0]["BASICAMT"].ToString();
                        txttaxamt.Text = ds.Tables[5].Rows[0]["TOTALTAXAMT"].ToString();
                        txttotal.Text = ds.Tables[5].Rows[0]["GROSSAMOUNT"].ToString();
                        txtTotalGross.Text = ds.Tables[5].Rows[0]["GROSSAMOUNT"].ToString();
                        this.txtnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim()))));
                        this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtnetamt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim() == "" ? "0.00" : this.txtTotalGross.Text.Trim())));
                    }
                    #endregion

                    this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", TotalAmount));
                    this.gvreceived.DataSource = dtreceivedrecord;
                    this.gvreceived.DataBind();
                    this.GridCalculation();
                    HttpContext.Current.Session["DEPORECEIVEDRECORD"] = dtreceivedrecord;                    

                    this.ddltransferno.Enabled = false;
                    this.ddlfromdepot.Enabled = false;
                    this.btnnewentry.Visible = false;
                    this.pnlAdd.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.btncancel.Visible = true;
                    this.pnlAdd.Enabled = true;
                    this.btnaddhide.Visible = false;
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Product details not found!</b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Depot receipt details not found!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Rejection delete 
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            DataTable dtinnerrejectiongrid = (DataTable)HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"];

            int flag = 0;
            string guid = hdn_guid.Value.ToString();
            flag = clsdeporeceived.RejectionRecordsDelete(guid, dtinnerrejectiongrid);

            if (flag > 0)
            {
                gvrejectiondetails.DataSource = dtinnerrejectiongrid;
                gvrejectiondetails.DataBind();

                HttpContext.Current.Session["REJECTIONDINNERGRIDDETAILS"] = dtinnerrejectiongrid;
                MessageBox1.ShowSuccess("Record deleted successfully!");

                this.popup.Show();
            }
            else
            {                
                MessageBox1.ShowError("Record deleted unsuccessful!");
                this.popup.Show();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnrejectiondetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TOTALREJECTIONDETAILS"] == null)
            {
                this.CreateRejectionTotalDataTable();
            }
            this.CreateRejectionInnerGridDataTable();

            /*Added By Avishek Ghosh (27-06-2017)*/
                        ImageButton btn_Rejection = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn_Rejection.NamingContainer;
            this.hdn_guid.Value = gvr.Cells[2].Text.Trim();
            this.hdn_transferid.Value = gvr.Cells[3].Text.Trim();
            this.hdnproductid.Value = gvr.Cells[4].Text.Trim();
            this.hdnproductname.Value = gvr.Cells[6].Text.Trim();
            this.hdnpacksizeid.Value = gvr.Cells[7].Text.Trim();
            this.hdnbatchno.Value = gvr.Cells[9].Text.Trim();
            this.hdnMRP.Value = gvr.Cells[12].Text.Trim();
            this.hdnproductrate.Value = gvr.Cells[13].Text.Trim();
            this.hdnMFDATE.Value = gvr.Cells[20].Text.Trim();
            this.hdnEXPRDATE.Value = gvr.Cells[21].Text.Trim();
            /*==================================*/

            string guid = hdn_guid.Value.ToString().Trim();
            this.txtproductnameonrejection.Text = this.hdnproductname.Value.ToString().Trim();
            this.txtproductbatchnoonrejection.Text = this.hdnbatchno.Value.ToString().Trim();

            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            this.ddlrejectionpacksize.Items.Clear();
            this.ddlrejectionpacksize.Items.Add(new ListItem("-- SELECT PACKING SIZE --", "0"));
            this.ddlrejectionpacksize.AppendDataBoundItems = true;
            this.ddlrejectionpacksize.DataSource = clsdeporeceived.BindPackingSize(this.hdnproductid.Value.ToString().Trim());
            this.ddlrejectionpacksize.DataValueField = "PACKSIZEID_FROM";
            this.ddlrejectionpacksize.DataTextField = "PACKSIZEName_FROM";
            this.ddlrejectionpacksize.DataBind();
            this.ddlrejectionpacksize.SelectedValue = this.hdnpacksizeid.Value.ToString().Trim();

            this.txtrejectionqty.Text = "";
            this.ddlrejectionreason.SelectedValue = "0";
            this.gvrejectiondetails.ClearPreviousDataSource();
            this.gvrejectiondetails.DataSource = null;
            this.gvrejectiondetails.DataBind();

            DataTable dtinnergridrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
            if (dtrejectiontotal.Rows.Count > 0)
            {
                for (int i = 0; i < dtrejectiontotal.Rows.Count; i++)
                {
                    if (dtrejectiontotal.Rows[i]["PRODUCTID"].ToString() == hdnproductid.Value.ToString().Trim() && dtrejectiontotal.Rows[i]["BATCHNO"].ToString().Trim() == txtproductbatchnoonrejection.Text.Trim())
                    {
                        DataRow dr = dtinnergridrejection.NewRow();
                        dr["GUID"] = dtrejectiontotal.Rows[i]["GUID"].ToString().Trim();
                        dr["DEPOTRECEIVEDID"] = dtrejectiontotal.Rows[i]["DEPOTRECEIVEDID"].ToString().Trim();
                        dr["STOCKTRANSFERID"] = dtrejectiontotal.Rows[i]["STOCKTRANSFERID"].ToString().Trim();
                        dr["PRODUCTID"] = dtrejectiontotal.Rows[i]["PRODUCTID"].ToString().Trim();
                        dr["PRODUCTNAME"] = dtrejectiontotal.Rows[i]["PRODUCTNAME"].ToString().Trim();
                        dr["BATCHNO"] = dtrejectiontotal.Rows[i]["BATCHNO"].ToString().Trim();
                        dr["REJECTIONQTY"] = dtrejectiontotal.Rows[i]["REJECTIONQTY"].ToString().Trim();
                        dr["PACKINGSIZEID"] = dtrejectiontotal.Rows[i]["PACKINGSIZEID"].ToString().Trim();
                        dr["PACKINGSIZENAME"] = dtrejectiontotal.Rows[i]["PACKINGSIZENAME"].ToString().Trim();
                        dr["RATE"] = dtrejectiontotal.Rows[i]["RATE"].ToString().Trim();
                        dr["TOTALRATE"] = dtrejectiontotal.Rows[i]["TOTALRATE"].ToString().Trim();
                        dr["REASONID"] = dtrejectiontotal.Rows[i]["REASONID"].ToString().Trim();
                        dr["REASONNAME"] = dtrejectiontotal.Rows[i]["REASONNAME"].ToString().Trim();
                        dr["STORELOCATIONID"] = dtrejectiontotal.Rows[i]["STORELOCATIONID"].ToString().Trim();
                        dr["STORELOCATIONNAME"] = dtrejectiontotal.Rows[i]["STORELOCATIONNAME"].ToString().Trim();
                        //Added By Avishek On 18-03-2016
                        dr["MFDATE"] = dtrejectiontotal.Rows[i]["MFDATE"].ToString().Trim();
                        dr["EXPRDATE"] = dtrejectiontotal.Rows[i]["EXPRDATE"].ToString().Trim();
                        dr["ASSESMENTPERCENTAGE"] = dtrejectiontotal.Rows[i]["ASSESMENTPERCENTAGE"].ToString().Trim();
                        dr["MRP"] = dtrejectiontotal.Rows[i]["MRP"].ToString().Trim();
                        dr["WEIGHT"] = dtrejectiontotal.Rows[i]["WEIGHT"].ToString().Trim();
                        dr["DEPOTRATE1"] = dtrejectiontotal.Rows[i]["DEPOTRATE1"].ToString().Trim();
                        dtinnergridrejection.Rows.Add(dr);
                        dtinnergridrejection.AcceptChanges();
                    }
                }

                Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;
                this.gvrejectiondetails.DataSource = dtinnergridrejection;
                this.gvrejectiondetails.DataBind();
            }
            this.popup.Show();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void btnrejectionadd_click(object sender, EventArgs e)
    {
        try
        {
            ClsDepoReceived_FAC clsdeporeceived = new ClsDepoReceived_FAC();
            if (Session["REJECTIONDINNERGRIDDETAILS"] == null)
            {
                this.CreateRejectionInnerGridDataTable();
            }
            int flag = 0;
            DataTable dtinnergridrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];

            if (dtinnergridrejection.Rows.Count > 0)
            {
                for (int i = 0; i < dtinnergridrejection.Rows.Count; i++)
                {
                    int NumberofRecord = dtinnergridrejection.Select("PRODUCTID='" + hdnproductid.Value.ToString().Trim() + "' AND BATCHNO='" + hdnbatchno.Value.ToString().Trim() + "' AND REASONID='" + ddlrejectionreason.SelectedValue.Trim() + "' AND REASONNAME='" + ddlrejectionreason.SelectedItem.Text.ToString().Trim() + "'").Length;
                    if (NumberofRecord > 0)
                    {
                        MessageBox1.ShowInfo("<b>This reason already exist with this product!</b>", 60, 400);
                        flag = 1;
                        break;
                    }
                }
            }

            if (flag == 0)
            {
                string mfDate = string.Empty;
                string exprDate = string.Empty;
                string assesment = string.Empty;
                string mrp = string.Empty;
                string weight = string.Empty;
                decimal DepotRate = 0;
                decimal DepotRate1 = 0;
                ClsStockAdjustment_FAC clsAdj = new ClsStockAdjustment_FAC();
                ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
                DataTable dtStoreLocation = new DataTable();
                DataTable dtProductdetails = new DataTable();
                dtStoreLocation = clsReceivedStock.StoreLocationDetails(this.ddlrejectionreason.SelectedValue.Trim());

                // Added By Avishek Ghosh On 18-03-2016
                dtProductdetails = clsAdj.BindPRoductDetails(hdnproductid.Value.Trim(), txtproductbatchnoonrejection.Text.Trim(),Convert.ToDecimal(this.hdnMRP.Value.Trim()));
                if (dtProductdetails.Rows.Count > 0)
                {
                    mfDate = Convert.ToString(this.hdnMFDATE.Value).Trim();
                    exprDate = Convert.ToString(this.hdnEXPRDATE.Value).Trim();
                    assesment = Convert.ToString(dtProductdetails.Rows[0]["ASSESSABLEPERCENT"]).Trim();
                    mrp = Convert.ToString(this.hdnMRP.Value).Trim();
                    weight = Convert.ToString(dtProductdetails.Rows[0]["WEIGHT"]).Trim();
                }
                else
                {
                    mfDate = "";
                    exprDate = "";
                    assesment = "0";
                    mrp = "0";
                    weight = "";
                }

                string DebitedTo = clsReceivedStock.DebitedTo(this.ddlrejectionreason.SelectedValue.Trim());
                if (DebitedTo.Trim() == "0")
                {
                    DepotRate = 0;
                    DepotRate1 = Convert.ToDecimal(hdnproductrate.Value.Trim());
                }
                else
                {
                    DepotRate = Convert.ToDecimal(hdnproductrate.Value.Trim());
                    DepotRate1 = Convert.ToDecimal(hdnproductrate.Value.Trim());
                }

                DataRow dr = dtinnergridrejection.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["DEPOTRECEIVEDID"] = this.hdn_receivedid.Value.ToString().Trim();
                dr["STOCKTRANSFERID"] = this.hdn_transferid.Value.ToString().Trim();
                dr["PRODUCTID"] = this.hdnproductid.Value.ToString().Trim();
                dr["PRODUCTNAME"] = this.hdnproductname.Value.ToString().Trim();
                dr["BATCHNO"] = this.hdnbatchno.Value.ToString().Trim();
                dr["REJECTIONQTY"] = Convert.ToString(this.txtrejectionqty.Text.Trim());
                dr["PACKINGSIZEID"] = this.ddlrejectionpacksize.SelectedValue.ToString().Trim();
                dr["PACKINGSIZENAME"] = this.ddlrejectionpacksize.SelectedItem.Text.Trim();
                dr["RATE"] = Convert.ToString(DepotRate).Trim();
                decimal convertionqty = clsdeporeceived.ConvertionQty(hdnproductid.Value.ToString().Trim(), this.ddlrejectionpacksize.SelectedValue.ToString().Trim(), Convert.ToDecimal(this.txtrejectionqty.Text.Trim()));
                dr["TOTALRATE"] = Convert.ToString(DepotRate * convertionqty);
                dr["REASONID"] = this.ddlrejectionreason.SelectedValue.ToString();
                dr["REASONNAME"] = this.ddlrejectionreason.SelectedItem.Text;
                if (dtStoreLocation.Rows.Count > 0)
                {
                    dr["STORELOCATIONID"] = Convert.ToString(dtStoreLocation.Rows[0]["ID"]);
                    dr["STORELOCATIONNAME"] = Convert.ToString(dtStoreLocation.Rows[0]["NAME"]);
                }
                else
                {
                    dr["STORELOCATIONID"] = "";
                    dr["STORELOCATIONNAME"] = "";
                }

                //Added By Avishek On 18-03-2016
                dr["MFDATE"] = mfDate;
                dr["EXPRDATE"] = exprDate;
                dr["ASSESMENTPERCENTAGE"] = assesment;
                dr["MRP"] = mrp;
                dr["WEIGHT"] = weight;
                dr["DEPOTRATE1"] = Convert.ToString(DepotRate1);

                dtinnergridrejection.Rows.Add(dr);
                dtinnergridrejection.AcceptChanges();

                Session["REJECTIONDINNERGRIDDETAILS"] = dtinnergridrejection;

                this.gvrejectiondetails.DataSource = dtinnergridrejection;
                this.gvrejectiondetails.DataBind();
                this.txtrejectionqty.Text = "";
                this.ddlrejectionreason.SelectedValue = "0";
            }
            this.popup.Show();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnrejectionsubmit_click
    public void btnrejectionsubmit_click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TOTALREJECTIONDETAILS"] == null)
            {
                this.CreateRejectionTotalDataTable();
            }

            DataTable dtinnerrejection = (DataTable)Session["REJECTIONDINNERGRIDDETAILS"];
            int j = 0;

            if (dtinnerrejection.Rows.Count > 0)
            {
                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    for (int i = 0; i < dtinnerrejection.Rows.Count; i++)
                    {
                        while (j > -1 && j < dtrejectiontotal.Rows.Count)
                        {
                            int NumberofRecord = dtrejectiontotal.Select("PRODUCTID='" + dtinnerrejection.Rows[i]["PRODUCTID"] + "' AND BATCHNO='" + dtinnerrejection.Rows[i]["BATCHNO"] + "'").Length;
                            if (NumberofRecord > 0)
                            {
                                string PID = dtinnerrejection.Rows[i]["PRODUCTID"].ToString();
                                string BATCHNO = dtinnerrejection.Rows[i]["BATCHNO"].ToString();

                                for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                                {
                                    if (dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == PID && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == BATCHNO)
                                    {
                                        dtrejectiontotal.Rows[k].Delete();
                                        dtrejectiontotal.AcceptChanges();
                                    }
                                }
                            }
                            j++;
                        }
                    }
                }

                for (int i = 0; i < dtinnerrejection.Rows.Count; i++)
                {
                    DataRow dr = dtrejectiontotal.NewRow();
                    dr["GUID"] = dtinnerrejection.Rows[i]["GUID"].ToString().Trim();
                    dr["DEPOTRECEIVEDID"] = dtinnerrejection.Rows[i]["DEPOTRECEIVEDID"].ToString().Trim();
                    dr["STOCKTRANSFERID"] = dtinnerrejection.Rows[i]["STOCKTRANSFERID"].ToString().Trim();
                    dr["PRODUCTID"] = dtinnerrejection.Rows[i]["PRODUCTID"].ToString().Trim();
                    dr["PRODUCTNAME"] = dtinnerrejection.Rows[i]["PRODUCTNAME"].ToString().Trim();
                    dr["BATCHNO"] = dtinnerrejection.Rows[i]["BATCHNO"].ToString().Trim();
                    dr["REJECTIONQTY"] = dtinnerrejection.Rows[i]["REJECTIONQTY"].ToString().Trim();
                    dr["PACKINGSIZEID"] = dtinnerrejection.Rows[i]["PACKINGSIZEID"].ToString().Trim();
                    dr["PACKINGSIZENAME"] = dtinnerrejection.Rows[i]["PACKINGSIZENAME"].ToString().Trim();
                    dr["RATE"] = dtinnerrejection.Rows[i]["RATE"].ToString().Trim();
                    dr["TOTALRATE"] = dtinnerrejection.Rows[i]["TOTALRATE"].ToString().Trim();
                    dr["REASONID"] = dtinnerrejection.Rows[i]["REASONID"].ToString().Trim();
                    dr["REASONNAME"] = dtinnerrejection.Rows[i]["REASONNAME"].ToString().Trim();
                    dr["STORELOCATIONID"] = dtinnerrejection.Rows[i]["STORELOCATIONID"].ToString().Trim();
                    dr["STORELOCATIONNAME"] = dtinnerrejection.Rows[i]["STORELOCATIONNAME"].ToString().Trim();
                    //Added By Avishek Ghosh On 18-03-2016
                    dr["MFDATE"] = dtinnerrejection.Rows[i]["MFDATE"].ToString().Trim();
                    dr["EXPRDATE"] = dtinnerrejection.Rows[i]["EXPRDATE"].ToString().Trim();
                    dr["ASSESMENTPERCENTAGE"] = dtinnerrejection.Rows[i]["ASSESMENTPERCENTAGE"].ToString().Trim();
                    dr["MRP"] = dtinnerrejection.Rows[i]["MRP"].ToString().Trim();
                    dr["WEIGHT"] = dtinnerrejection.Rows[i]["WEIGHT"].ToString().Trim();
                    dr["DEPOTRATE1"] = dtinnerrejection.Rows[i]["DEPOTRATE1"].ToString().Trim();
                    dtrejectiontotal.Rows.Add(dr);
                    dtrejectiontotal.AcceptChanges();
                }

                if (dtinnerrejection.Rows.Count == 0)
                {
                    if (dtrejectiontotal.Rows.Count > 0)
                    {
                        while (j > -1 && j < dtrejectiontotal.Rows.Count)
                        {
                            int NumberofRecord = dtrejectiontotal.Select("PRODUCTID='" + hdnproductid.Value.Trim() + "' AND BATCHNO='" + hdnbatchno.Value.Trim() + "'").Length;
                            if (NumberofRecord > 0)
                            {
                                for (int k = dtrejectiontotal.Rows.Count - 1; k >= 0; k--)
                                {
                                    if (dtrejectiontotal.Rows[k]["PRODUCTID"].ToString().Trim() == hdnproductid.Value.Trim() && dtrejectiontotal.Rows[k]["BATCHNO"].ToString().Trim() == hdnbatchno.Value.Trim())
                                    {
                                        dtrejectiontotal.Rows[k].Delete();
                                        dtrejectiontotal.AcceptChanges();
                                    }
                                }
                            }
                            j++;
                        }
                    }
                }

                Session["TOTALREJECTIONDETAILS"] = dtrejectiontotal;
                Session["REJECTIONDINNERGRIDDETAILS"] = null;
                dtinnerrejection.Clear();
                this.txtrejectionqty.Text = "";
                this.ddlrejectionreason.SelectedValue = "0";
                this.gvrejectiondetails.ClearPreviousDataSource();
                this.gvrejectiondetails.DataSource = null;
                this.gvrejectiondetails.DataBind();
                MessageBox1.ShowInfo("Rejection reason for product <b><font color='green'>" + this.txtproductnameonrejection.Text.Trim() + "</font></b> added successfully!", 80, 750);

                DataTable dttransferinfo = (DataTable)HttpContext.Current.Session["DEPORECEIVEDRECORD"];
                if (dttransferinfo.Rows.Count > 0)
                {
                    gvreceived.DataSource = dttransferinfo;
                    gvreceived.DataBind();
                    this.GridCalculation();
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please add atleast 1 rejection reason!");
                this.popup.Show();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region gvdepotreceivedDetails_RowDataBound
    protected void gvdepotreceivedDetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[10] as GridDataControlFieldCell;
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

    #region Delete Depot Received
    protected void DeleteRecordReceived(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsDepoReceived_FAC clsDepoReceived = new ClsDepoReceived_FAC();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                e.Record["Error"] = "Delete not allowed..!";
            }
            else
            {
                if (clsdeporeceived.GetFinancestatus(this.hdn_receivedid.Value.Trim()) == "1")
                {
                    e.Record["Error"] = "Finance posting already done,not allow to delete.";
                    return;
                }
                if (clsdeporeceived.Getstatus(this.hdn_receivedid.Value.Trim()) == "1")
                {
                    e.Record["Error"] = "Day End Operation already done,not allow to delete.";
                    return;
                }
                int flag = 0;
                flag = clsDepoReceived.DeleteDepotReceived(e.Record["STOCKDEPORECEIVEDID"].ToString());
                this.hdn_receivedid.Value = "";


                if (flag == 1)
                {
                    this.LoadReceivedDetails();
                    e.Record["Error"] = "Record Deleted Successfully. ";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string upath = "frmRptInvoicePrint_FAC.aspx?Stnid=" + hdn_transferid.Value.Trim() + "&MenuId=" + this.Request.QueryString["MENUID"].ToString().Trim() + "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CalculateTotalNetAmount
    decimal CalculateTotalNetAmount(DataTable dt)
    {
        decimal NetAmount = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            NetAmount += Convert.ToDecimal(dt.Rows[Counter]["NETAMOUNT"]);
        }
        return NetAmount;
    }
    #endregion

    #region GridCalculation
    protected void GridCalculation()
    {
        DataTable dtDEPOTRCVDDETAILS = new DataTable();
        decimal TotalGridAmount = 0;
        decimal TotalNetAmt = 0;

        if (Session["DEPORECEIVEDRECORD"] != null)
        {
            dtDEPOTRCVDDETAILS = (DataTable)Session["DEPORECEIVEDRECORD"];
        }
        TotalNetAmt = CalculateTotalNetAmount(dtDEPOTRCVDDETAILS);
        
        #region Header
        this.gvreceived.HeaderRow.Cells[6].Text = "PRODUCT";
        this.gvreceived.HeaderRow.Cells[8].Text = "PACK SIZE";
        this.gvreceived.HeaderRow.Cells[9].Text = "BATCH";
        this.gvreceived.HeaderRow.Cells[10].Text = "TRNSF.QTY";
        this.gvreceived.HeaderRow.Cells[11].Text = "RCVD.QTY";
        this.gvreceived.HeaderRow.Cells[20].Text = "MFG DATE";
        this.gvreceived.HeaderRow.Cells[21].Text = "EXPR DATE";

        this.gvreceived.HeaderRow.Cells[1].Visible = false;
        this.gvreceived.HeaderRow.Cells[2].Visible = false;
        this.gvreceived.HeaderRow.Cells[3].Visible = false;
        this.gvreceived.HeaderRow.Cells[4].Visible = false;
        this.gvreceived.HeaderRow.Cells[7].Visible = false;
        this.gvreceived.HeaderRow.Cells[15].Visible = false;
        this.gvreceived.HeaderRow.Cells[16].Visible = false;
        this.gvreceived.HeaderRow.Cells[17].Visible = false;
        this.gvreceived.HeaderRow.Cells[18].Visible = false;
        this.gvreceived.HeaderRow.Cells[19].Visible = false;

        this.gvreceived.HeaderRow.Cells[1].Wrap = false;
        this.gvreceived.HeaderRow.Cells[2].Wrap = false;
        this.gvreceived.HeaderRow.Cells[3].Wrap = false;
        this.gvreceived.HeaderRow.Cells[4].Wrap = false;
        this.gvreceived.HeaderRow.Cells[5].Wrap = false;
        this.gvreceived.HeaderRow.Cells[6].Wrap = false;
        this.gvreceived.HeaderRow.Cells[7].Wrap = false;
        this.gvreceived.HeaderRow.Cells[8].Wrap = false;
        this.gvreceived.HeaderRow.Cells[9].Wrap = false;
        this.gvreceived.HeaderRow.Cells[10].Wrap = false;
        this.gvreceived.HeaderRow.Cells[11].Wrap = false;
        this.gvreceived.HeaderRow.Cells[12].Wrap = false;
        this.gvreceived.HeaderRow.Cells[13].Wrap = false;
        this.gvreceived.HeaderRow.Cells[14].Wrap = false;
        this.gvreceived.HeaderRow.Cells[15].Wrap = false;
        this.gvreceived.HeaderRow.Cells[16].Wrap = false;
        this.gvreceived.HeaderRow.Cells[17].Wrap = false;
        this.gvreceived.HeaderRow.Cells[18].Wrap = false;
        this.gvreceived.HeaderRow.Cells[19].Wrap = false;
        this.gvreceived.HeaderRow.Cells[20].Wrap = false;
        this.gvreceived.HeaderRow.Cells[21].Wrap = false;

        #endregion

        #region Footer Style
        this.gvreceived.FooterRow.Cells[1].Visible = false;
        this.gvreceived.FooterRow.Cells[2].Visible = false;
        this.gvreceived.FooterRow.Cells[3].Visible = false;
        this.gvreceived.FooterRow.Cells[4].Visible = false;
        this.gvreceived.FooterRow.Cells[7].Visible = false;
        this.gvreceived.FooterRow.Cells[15].Visible = false;
        this.gvreceived.FooterRow.Cells[16].Visible = false;
        this.gvreceived.FooterRow.Cells[17].Visible = false;
        this.gvreceived.FooterRow.Cells[18].Visible = false;
        this.gvreceived.FooterRow.Cells[19].Visible = false;

        this.gvreceived.FooterRow.Cells[1].Wrap = false;
        this.gvreceived.FooterRow.Cells[2].Wrap = false;
        this.gvreceived.FooterRow.Cells[3].Wrap = false;
        this.gvreceived.FooterRow.Cells[4].Wrap = false;
        this.gvreceived.FooterRow.Cells[5].Wrap = false;
        this.gvreceived.FooterRow.Cells[6].Wrap = false;
        this.gvreceived.FooterRow.Cells[7].Wrap = false;
        this.gvreceived.FooterRow.Cells[8].Wrap = false;
        this.gvreceived.FooterRow.Cells[9].Wrap = false;
        this.gvreceived.FooterRow.Cells[10].Wrap = false;
        this.gvreceived.FooterRow.Cells[11].Wrap = false;
        this.gvreceived.FooterRow.Cells[12].Wrap = false;
        this.gvreceived.FooterRow.Cells[13].Wrap = false;
        this.gvreceived.FooterRow.Cells[14].Wrap = false;
        this.gvreceived.FooterRow.Cells[15].Wrap = false;
        this.gvreceived.FooterRow.Cells[16].Wrap = false;
        this.gvreceived.FooterRow.Cells[17].Wrap = false;
        this.gvreceived.FooterRow.Cells[18].Wrap = false;
        this.gvreceived.FooterRow.Cells[19].Wrap = false;
        this.gvreceived.FooterRow.Cells[20].Wrap = false;
        this.gvreceived.FooterRow.Cells[21].Wrap = false;
        this.gvreceived.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
        this.gvreceived.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;
        this.gvreceived.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Center;
        this.gvreceived.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
        this.gvreceived.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        this.gvreceived.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        this.gvreceived.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
        this.gvreceived.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
        this.gvreceived.FooterRow.Cells[20].HorizontalAlign = HorizontalAlign.Center;
        this.gvreceived.FooterRow.Cells[21].HorizontalAlign = HorizontalAlign.Center;
        #endregion

        foreach (GridViewRow row in gvreceived.Rows)
        {
            row.Cells[1].Visible = false;
            row.Cells[2].Visible = false;
            row.Cells[3].Visible = false;
            row.Cells[4].Visible = false;
            row.Cells[7].Visible = false;
            row.Cells[15].Visible = false;
            row.Cells[16].Visible = false;
            row.Cells[17].Visible = false;
            row.Cells[18].Visible = false;
            row.Cells[19].Visible = false;


            row.Cells[1].Wrap = false;
            row.Cells[2].Wrap = false;
            row.Cells[3].Wrap = false;
            row.Cells[4].Wrap = false;
            row.Cells[5].Wrap = false;
            row.Cells[6].Wrap = false;
            row.Cells[7].Wrap = false;
            row.Cells[8].Wrap = false;
            row.Cells[9].Wrap = false;
            row.Cells[10].Wrap = false;
            row.Cells[11].Wrap = false;
            row.Cells[12].Wrap = false;
            row.Cells[13].Wrap = false;
            row.Cells[14].Wrap = false;
            row.Cells[15].Wrap = false;
            row.Cells[16].Wrap = false;
            row.Cells[17].Wrap = false;
            row.Cells[18].Wrap = false;
            row.Cells[19].Wrap = false;
            row.Cells[20].Wrap = false;
            row.Cells[21].Wrap = false;

            row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[20].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[21].HorizontalAlign = HorizontalAlign.Center;

            TotalGridAmount += Convert.ToDecimal(row.Cells[14].Text.Trim());
            txtbasicamt.Text = TotalGridAmount.ToString("#.00");

            int count = 21;
            DataTable dt = (DataTable)Session["dtDRTaxCount"];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                count = count + 1;
                this.gvreceived.HeaderRow.Cells[count].Wrap = false;
            }

            #region Rejection Block

            if (Session["TOTALREJECTIONDETAILS"] != null)
            {
                DataTable dtrejectiontotal = (DataTable)Session["TOTALREJECTIONDETAILS"];
                if (dtrejectiontotal.Rows.Count > 0)
                {
                    int NumberofRecord = 0;
                    
                    NumberofRecord = dtrejectiontotal.Select("PRODUCTNAME='" + row.Cells[6].Text.Trim() + "' AND BATCHNO='" + row.Cells[9].Text.Trim() + "'").Length;
                    Label lblrejectionqty = (Label)row.FindControl("lblrejvalue");
                    if (NumberofRecord > 0)
                    {
                        lblrejectionqty.Text = Convert.ToString(NumberofRecord);
                        lblrejectionqty.CssClass = "badge green";
                    }
                    else
                    {
                        lblrejectionqty.Text = "0";
                        lblrejectionqty.CssClass = "badge red";
                    }
                }
            }
            #endregion
        }

        #region TotalTax Footer
        int TotalRows = gvreceived.Rows.Count;
        int count1 = 0;
        DataTable dtTaxCountDataAddition1 = (DataTable)Session["dtDRTaxCount"];
        if (dtTaxCountDataAddition1.Rows.Count > 0)
        {
            for (int i = 23; i <= (23 + dtTaxCountDataAddition1.Rows.Count); i += 2)
            {
                double sum = 0.00;
                for (int j = 0; j < TotalRows; j++)
                {
                    sum += gvreceived.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(gvreceived.Rows[j].Cells[i].Text) : 0.00;
                    gvreceived.Rows[j].Cells[i].Wrap = false;
                    gvreceived.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                this.gvreceived.FooterRow.Cells[i].Text = sum.ToString("#.00");
                this.gvreceived.FooterRow.Cells[i].Font.Bold = true;
                this.gvreceived.FooterRow.Cells[i].ForeColor = Color.Blue;
                this.gvreceived.FooterRow.Cells[i].Wrap = false;
                this.gvreceived.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                count1 = count1 + 1;
            }
        }
        #endregion

        if (TotalGridAmount == 0)
        {
            this.gvreceived.FooterRow.Cells[14].Text = "0.00";
        }
        else
        {
            this.gvreceived.FooterRow.Cells[14].Text = TotalGridAmount.ToString("#.00");
        }
        this.gvreceived.FooterRow.Cells[14].Font.Bold = true;
        this.gvreceived.FooterRow.Cells[14].ForeColor = Color.Blue;
        this.gvreceived.FooterRow.Cells[14].Wrap = false;

        this.gvreceived.FooterRow.Cells[13].Text = "Total : ";
        this.gvreceived.FooterRow.Cells[13].Font.Bold = true;
        this.gvreceived.FooterRow.Cells[13].ForeColor = Color.Blue;

        #region Net Amount
        if (dtTaxCountDataAddition1.Rows.Count == 2)
        {
            if (TotalNetAmt == 0)
            {
                gvreceived.FooterRow.Cells[24 + count1].Text = "0.00";
            }
            else
            {
                gvreceived.FooterRow.Cells[24 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvreceived.FooterRow.Cells[24 + count1].Font.Bold = true;
            gvreceived.FooterRow.Cells[24 + count1].ForeColor = Color.Blue;
            gvreceived.FooterRow.Cells[24 + count1].Wrap = false;
            gvreceived.FooterRow.Cells[24 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvreceived.Rows)
            {
                row.Cells[24 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvreceived.HeaderRow.Cells[24 + count1].Text = "NET AMOUNT";
            this.gvreceived.HeaderRow.Cells[24 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 1)
        {
            if (TotalNetAmt == 0)
            {
                gvreceived.FooterRow.Cells[23 + count1].Text = "0.00";
            }
            else
            {
                gvreceived.FooterRow.Cells[23 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvreceived.FooterRow.Cells[23 + count1].Font.Bold = true;
            gvreceived.FooterRow.Cells[23 + count1].ForeColor = Color.Blue;
            gvreceived.FooterRow.Cells[23 + count1].Wrap = false;
            gvreceived.FooterRow.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvreceived.Rows)
            {
                row.Cells[23 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvreceived.HeaderRow.Cells[23 + count1].Text = "NET AMOUNT";
            this.gvreceived.HeaderRow.Cells[23 + count1].Wrap = false;
        }
        if (dtTaxCountDataAddition1.Rows.Count == 0)
        {
            if (TotalNetAmt == 0)
            {
                gvreceived.FooterRow.Cells[22 + count1].Text = "0.00";
            }
            else
            {
                gvreceived.FooterRow.Cells[22 + count1].Text = TotalNetAmt.ToString("#.00");
            }
            gvreceived.FooterRow.Cells[22 + count1].Font.Bold = true;
            gvreceived.FooterRow.Cells[22 + count1].ForeColor = Color.Blue;
            gvreceived.FooterRow.Cells[22 + count1].Wrap = false;
            gvreceived.FooterRow.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;

            foreach (GridViewRow row in gvreceived.Rows)
            {
                row.Cells[22 + count1].HorizontalAlign = HorizontalAlign.Right;
            }
            this.gvreceived.HeaderRow.Cells[22 + count1].Text = "NET AMOUNT";
            this.gvreceived.HeaderRow.Cells[22 + count1].Wrap = false;
        }
        #endregion
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
        Calendar1.StartDate = oDate;
        CalendarExtender4.StartDate = oDate;
        CalendarExtender3.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtreceiveddate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            Calendar1.EndDate = today1;
            CalendarExtender4.EndDate = today1;
            CalendarExtender3.EndDate = today1;
        }
        else
        {
            this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtreceiveddate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            Calendar1.EndDate = cDate;
            CalendarExtender4.EndDate = cDate;
            CalendarExtender3.EndDate = cDate;
        }
    }
    #endregion
}