using BAL;
using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmFactoryProductionUpdate : System.Web.UI.Page
{
    ClsFactoryProductionOrderUpdate oFactoryUpdate = new ClsFactoryProductionOrderUpdate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Action"] == "Production")
            {
               
                this.DateLock();
                this.divbtnSubmit.Visible = true;
                this.divbtnapprove.Visible = false;
                this.trHeader.Visible = true;
            }
            else
            {
                this.divbtnSubmit.Visible = false;
                this.divbtnapprove.Visible = true;
                this.trHeader.Visible = false;
                this.BindStoreReturn();
            }
        }
    }
    public void StartProductionOrder()
    {
        DataTable dt = new DataTable();

        #region Drop Down List Style

        dt = oFactoryUpdate.BindProctuctionOrder(HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlbatch.Items.Clear();
            this.ddlbatch.Items.Add(new ListItem("Select", "0"));
            ddlbatch.DataTextField = "BATCHNO";
            ddlbatch.DataValueField = "PRODUCTION_ORDERID";
            ddlbatch.DataSource = dt;
            ddlbatch.DataBind();
            grdQtySchemeDetails.Visible = false;
            gvProcessEndItem.Visible = false;
            grdStoreReturn.Visible = false;
            string ProcessState = "";
            ProcessState = dt.Rows[i]["PROCESSSTATE"].ToString();
            //ddlbatch.Font.Bold = true;
            foreach (ListItem item in ddlbatch.Items)
            {
                if (ProcessState == "I")
                {
                    item.Attributes.CssStyle.Add("color", "Red");
                }
                else if (ProcessState == "O")
                {
                    item.Attributes.CssStyle.Add("color", "Pink");
                }
                else
                {
                    item.Attributes.CssStyle.Add("color", "Black");
                }
            }
        }
        #endregion
    }

    public void EndProductionOrder()
    {
        DataTable dt = new DataTable();

        #region Drop Down List Style

        dt = oFactoryUpdate.BindEndProctuctionOrder(HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlbatch.Items.Clear();
            this.ddlbatch.Items.Add(new ListItem("Select", "0"));
            ddlbatch.DataTextField = "BATCHNO";
            ddlbatch.DataValueField = "PRODUCTION_ORDERID";
            ddlbatch.DataSource = dt;
            ddlbatch.DataBind();
            grdQtySchemeDetails.Visible = false;
            gvProcessEndItem.Visible = false;
            grdStoreReturn.Visible = false;
            string ProcessState = "";
            ProcessState = dt.Rows[i]["PROCESSSTATE"].ToString();
            //ddlbatch.Font.Bold = true;
            foreach (ListItem item in ddlbatch.Items)
            {
                if (ProcessState == "I")
                {
                    item.Attributes.CssStyle.Add("color", "Red");
                }
                else if (ProcessState == "O")
                {
                    item.Attributes.CssStyle.Add("color", "Pink");
                }
                else
                {
                    item.Attributes.CssStyle.Add("color", "Black");
                }
            }
        }
        #endregion
    }

    public DataTable CreateDataTableForRequisition()
    {
        DataTable dtRequisition = new DataTable();
        dtRequisition.Clear();
        dtRequisition.Columns.Add(new DataColumn("CATEGORYID", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("CATEGORYNAME", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("MATERIALID", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("QTY", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("BUFFERQTY", typeof(String)));
        dtRequisition.Columns.Add(new DataColumn("NETQTY", typeof(String)));
        return dtRequisition;
    }

    public DataTable CreateDataTableFormNotification()
    {
        DataTable dtNotification = new DataTable();
        dtNotification.Clear();
        dtNotification.Columns.Add(new DataColumn("CATEGORYID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("CATEGORYNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("MATERIALID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("PRODUCEQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("REJECTEDQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("RETURNQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("YEILD", typeof(String)));
        return dtNotification;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataRow dr;
        DataTable dtREQUISITION = CreateDataTableForRequisition();
        DataTable dtNotification = CreateDataTableFormNotification();
        string POOrder = oFactoryUpdate.BatchNo(this.ddlbatch.SelectedValue.Trim());

        if (POOrder == "1" && ddlstate.SelectedValue == "I")
        {
            MessageBox1.ShowInfo("Allready Exist This Production Order:- <b><font color='green'>" + ddlbatch.SelectedItem.ToString() + "</font></b> Plz Check Issue.", 60, 600);
            Clear();
        }
        else
        {
            //clsfactory.InSertNotification(ddlbatch.SelectedValue, ddlProcess.SelectedValue, ddlstate.SelectedValue);
            int result = 0;
            string MRPNo = "";
            int GridCount = grdQtySchemeDetails.Rows.Count;
            int a = gvProcessEndItem.Rows.Count;
            if (ddlbatch.SelectedValue == "-1")
            {
                MessageBox1.ShowInfo("Select BatchNo");
            }
            else if (ddlProcess.SelectedValue == "-1")
            {
                MessageBox1.ShowInfo("Select Process");
            }
            else if (ddlstate.SelectedValue == "I" && grdQtySchemeDetails.Rows.Count <= 0)
            {
                MessageBox1.ShowInfo("First Search Then Save.");
            }
            else if (ddlstate.SelectedValue == "O" && gvProcessEndItem.Rows.Count <= 0)
            {
                MessageBox1.ShowInfo("First Search Then Save.");
            }
            else
            {
                #region Process Start Section

                if (ddlstate.SelectedValue == "I")
                {
                    oFactoryUpdate.InSertNotification(ddlbatch.SelectedValue, ddlProcess.SelectedValue, ddlstate.SelectedValue, txtRemarks.Text);
                    foreach (GridViewRow row in grdQtySchemeDetails.Rows)
                    {
                        string lblCATNAME = grdQtySchemeDetails.Rows[row.RowIndex].Cells[2].Text.ToString();
                        Label lblCATID = (Label)row.FindControl("lblcatid");
                        Label lblID = (Label)row.FindControl("lblItemid");

                        string lblName = grdQtySchemeDetails.Rows[row.RowIndex].Cells[4].Text;
                        // string lblUOMID = grdQtySchemeDetails.Rows[row.RowIndex].Cells[5].Text;
                        Label lblUOMID = (Label)row.FindControl("lblunit");

                        string lblUOMNAME = grdQtySchemeDetails.Rows[row.RowIndex].Cells[7].Text;
                        Label lblBomQty = (Label)row.FindControl("lblBomQty");
                        TextBox txtproduceQty = (TextBox)row.FindControl("txtproduceQty");
                        Label lblBufferQty = (Label)row.FindControl("lblBufferQty");
                        Label lblNetQty = (Label)row.FindControl("lblNetQty");

                        decimal TotalQty;
                        dr = dtREQUISITION.NewRow();
                        dr[0] = lblCATID.Text;
                        dr[1] = lblCATNAME;
                        dr[2] = lblID.Text;
                        dr[3] = lblName;
                        dr[4] = lblUOMID.Text;
                        dr[5] = lblUOMNAME;
                        //TotalQty = ((Convert.ToDecimal(txtproduceQty.Text)));
                        dr[6] = String.Format("{0:#0.000}", (Convert.ToDecimal(lblBomQty.Text)));
                        dr[7] = "";
                        dr[8] = "";
                        dr[9] = String.Format("{0:#0.000}", (Convert.ToDecimal(lblBufferQty.Text)));
                        dr[10] = String.Format("{0:#0.000}", (Convert.ToDecimal(lblNetQty.Text)));
                        dtREQUISITION.Rows.Add(dr);
                    }
                    string xmlREQUISITION = ConvertDatatableToXML(dtREQUISITION);
                    MRPNo = oFactoryUpdate.InsertUpdateDelete(xmlREQUISITION, ddlbatch.SelectedValue, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString());
                }
                #endregion Process Start Section End

                else if (ddlstate.SelectedValue == "O")
                {
                    #region Process End Requestion Section
                    oFactoryUpdate.InSertNotification(ddlbatch.SelectedValue, ddlProcess.SelectedValue, ddlstate.SelectedValue, txtRemarks.Text);
                    foreach (GridViewRow row in gvProcessEndItem.Rows)
                    {
                        string lblCATNAME = gvProcessEndItem.Rows[row.RowIndex].Cells[3].Text.ToString();
                        Label lblCATID = (Label)row.FindControl("lblcatid");
                        Label lblID = (Label)row.FindControl("lblItemid");
                        Label lblName = (Label)row.FindControl("lblItemName");
                        Label lblUOMID = (Label)row.FindControl("lblunit");
                        Label lblUOMNAME = (Label)row.FindControl("lblUomName");
                        TextBox txtproduceQty = (TextBox)row.FindControl("txtProducedQty");
                        TextBox txtRejectedQty = (TextBox)row.FindControl("txtRejectedQty");

                        if (txtRejectedQty.Text.Trim() != "")
                        {
                            if (Convert.ToDecimal(txtproduceQty.Text.Trim()) < Convert.ToDecimal(txtRejectedQty.Text.Trim()))
                            {
                                result = 1;
                                break;
                            }
                        }
                        decimal TotalQty;
                        dr = dtREQUISITION.NewRow();
                        dr[0] = lblCATID.Text;
                        dr[1] = lblCATNAME;
                        dr[2] = lblID.Text;
                        dr[3] = lblName.Text;
                        dr[4] = lblUOMID.Text;
                        dr[5] = lblUOMNAME.Text;
                        TotalQty = ((Convert.ToDecimal(txtproduceQty.Text)));
                        dr[6] = String.Format("{0:#0.000}", TotalQty);
                        dr[7] = "";
                        dr[8] = "";
                        dtREQUISITION.Rows.Add(dr);
                    }
                    string xmlREQUISITION = ConvertDatatableToXML(dtREQUISITION);
                    #endregion Process End Requestion Section

                    #region Process End Notification Section
                    foreach (GridViewRow row in gvProcessEndItem.Rows)
                    {
                        string lblCATNAME = gvProcessEndItem.Rows[row.RowIndex].Cells[3].Text.ToString();
                        Label lblCATID = (Label)row.FindControl("lblcatid");
                        Label lblID = (Label)row.FindControl("lblItemid");
                        Label lblName = (Label)row.FindControl("lblItemName");
                        Label lblUOMID = (Label)row.FindControl("lblunit");
                        Label lblUOMNAME = (Label)row.FindControl("lblUomName");
                        TextBox txtproduceQty = (TextBox)row.FindControl("txtProducedQty");
                        TextBox txtRejectedQty = (TextBox)row.FindControl("txtRejectedQty");
                        Label lblYield = (Label)row.FindControl("lblYield");
                        CheckBox chkBx = (CheckBox)row.FindControl("chkyn");
                        decimal EstimatedQty = Convert.ToDecimal(((Label)row.FindControl("lblEstimatedQty")).Text);
                        decimal ProducedQty = Convert.ToDecimal(((TextBox)row.FindControl("txtProducedQty")).Text);
                        decimal RejectedQty = Convert.ToDecimal(((TextBox)row.FindControl("txtRejectedQty")).Text);
                        decimal ProducedWithRejectedQty = (ProducedQty + RejectedQty);

                        if (txtRejectedQty.Text == "")
                        {
                            txtRejectedQty.Text = "0";
                        }
                        if (lblYield.Text == "")
                        {
                            lblYield.Text = "0";
                        }
                        //if (EstimatedQty >= ProducedWithRejectedQty)
                        if (chkBx.Checked == true && lblCATID.Text != "4")
                        {
                            decimal TotalQty;
                            dr = dtNotification.NewRow();
                            dr[0] = lblCATID.Text;
                            dr[1] = lblCATNAME;
                            dr[2] = lblID.Text;
                            dr[3] = lblName.Text;
                            dr[4] = lblUOMID.Text;
                            dr[5] = lblUOMNAME.Text;
                            TotalQty = ((Convert.ToDecimal(txtproduceQty.Text)));
                            dr[6] = String.Format("{0:#0.000}", TotalQty);
                            dr[7] = txtRejectedQty.Text;
                            dr[8] = "N";
                            dr[9] = lblYield.Text;
                            dtNotification.Rows.Add(dr);
                        }
                        else if (chkBx.Checked == false && lblCATID.Text == "4")
                        {
                            decimal TotalQty;
                            dr = dtNotification.NewRow();
                            dr[0] = lblCATID.Text;
                            dr[1] = lblCATNAME;
                            dr[2] = lblID.Text;
                            dr[3] = lblName.Text;
                            dr[4] = lblUOMID.Text;
                            dr[5] = lblUOMNAME.Text;
                            TotalQty = ((Convert.ToDecimal(txtproduceQty.Text)));
                            dr[6] = String.Format("{0:#0.000}", TotalQty);
                            dr[7] = txtRejectedQty.Text;
                            dr[8] = "N";
                            dr[9] = lblYield.Text;
                            dtNotification.Rows.Add(dr);
                        }
                        else if (chkBx.Checked == true && lblCATID.Text == "4" && lblUOMNAME.Text == "PCS")
                        {
                            decimal TotalQty;
                            dr = dtNotification.NewRow();
                            dr[0] = lblCATID.Text;
                            dr[1] = lblCATNAME;
                            dr[2] = lblID.Text;
                            dr[3] = lblName.Text;
                            dr[4] = lblUOMID.Text;
                            dr[5] = lblUOMNAME.Text;
                            TotalQty = ((Convert.ToDecimal(txtproduceQty.Text)));
                            dr[6] = String.Format("{0:#0.000}", TotalQty);
                            dr[7] = txtRejectedQty.Text;
                            dr[8] = "N";
                            dr[9] = lblYield.Text;
                            dtNotification.Rows.Add(dr);
                        }
                        else
                        {
                            MessageBox1.ShowInfo("Wastage is Required", 40, 250);
                            return;
                        }
                    }
                    string xmlNotification = ConvertDatatableToXML(dtNotification);
                    #endregion

                    if (result == 0)
                    {
                        if (string.IsNullOrEmpty(Session["XML_ISSUE"] as string))
                        {
                            MRPNo = oFactoryUpdate.InsertUpdateDeleteRETURN(xmlREQUISITION, xmlNotification, "", ddlbatch.SelectedValue, ddlProcess.SelectedValue, HttpContext.Current.Session["FINYEAR"].ToString(), "NR");
                        }
                        else
                        {
                            MRPNo = oFactoryUpdate.InsertUpdateDeleteRETURN(xmlREQUISITION, xmlNotification, Session["XML_ISSUE"].ToString(), ddlbatch.SelectedValue, ddlProcess.SelectedValue, HttpContext.Current.Session["FINYEAR"].ToString(), "R");
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Rejected Qty must be less than or equal to Actual Qty ", 60, 500);
                    }
                }
                MessageBox1.ShowSuccess("Production Update/Requisition No: <b><font color='green'>" + MRPNo + "</font></b> saved successfully", 80, 550);
                if (ddlstate.SelectedValue == "I")
                {
                    DataTable dt = new DataTable();
                    ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                    dt = clsMMPo.Bind_Sms_Mobno("K", ddlbatch.SelectedValue.Trim());
                    foreach (DataRow row in dt.Rows)
                    {
                        this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                    }
                    DataTable dt1 = new DataTable();
                    dt1 = clsMMPo.Bind_Sms_Mobno("L", ddlbatch.SelectedValue.Trim());
                }
                else if (ddlstate.SelectedValue == "O")
                {
                    DataTable dt = new DataTable();
                    ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                    dt = clsMMPo.Bind_Sms_Mobno("1", ddlbatch.SelectedValue.Trim());
                    foreach (DataRow row in dt.Rows)
                    {
                        this.SMS_Block(row["MOBILE"].ToString(), row["MESSAGE"].ToString());
                    }
                    DataTable dt1 = new DataTable();
                    dt1 = clsMMPo.Bind_Sms_Mobno("2", ddlbatch.SelectedValue.Trim());
                }
                Clear();
               // ProductionOrder();
            }
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

    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = oFactoryUpdate.BindProcess(ddlbatch.SelectedValue);
        if (dt.Rows.Count > 1)
        {
            ddlProcess.Items.Clear();
            ddlProcess.DataTextField = "ProcessName";
            ddlProcess.DataValueField = "ProcessID";
            ddlProcess.DataSource = dt;
            ddlProcess.DataBind();
            txtProductiondt.Text = dt.Rows[1]["PRODUCTIONDATE"].ToString();
            //DataTable dtBatch = oFactoryUpdate.FetchBatch(ddlbatch.SelectedValue);
        }
        else
        {
            ddlProcess.Items.Clear();
            grdQtySchemeDetails.DataSource = null;
            grdQtySchemeDetails.DataBind();
            gvProcessEndItem.DataSource = null;
            gvProcessEndItem.DataBind();
        }
    }

    protected void btnAddnewRecord_Click(object sender, EventArgs e)
    {

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        grdQtySchemeDetails.Visible = false;
        gvProcessEndItem.Visible = false;
        grdStoreReturn.Visible = false;

        if (ddlstate.SelectedValue == "I")
        {
            DataTable dt = oFactoryUpdate.BindProduct(ddlbatch.SelectedValue, HttpContext.Current.Session["DEPOTID"].ToString());
            string SFGProcess = oFactoryUpdate.SFGPending(this.ddlbatch.SelectedValue.Trim());
            if (SFGProcess == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>SFG Process is Pending.</font></b>", 40, 350);
                return;
            }
            else
            {
                grdQtySchemeDetails.DataSource = dt;
                grdQtySchemeDetails.DataBind();
                grdQtySchemeDetails.Visible = true;
            }
        }
        else
        {
            string IssueQC = oFactoryUpdate.ChkIssueQC(this.ddlbatch.SelectedValue.Trim());
            string ProductionOrder = oFactoryUpdate.BatchNo(this.ddlbatch.SelectedValue.Trim());
            //if (IssueQC == "0")
            //{
            //    MessageBox1.ShowInfo("<b><font color='red'>Issue QC is Pending.</font></b>", 40, 350);
            //    return;
            //}
             if (ProductionOrder == "0")
            {
                MessageBox1.ShowInfo("<b><font color='red'>Please Complete Process Start.</font></b>", 40, 350);
                return;
            }
            else
            {
                DataTable dt = oFactoryUpdate.BindProductOutPut(ddlbatch.SelectedValue, HttpContext.Current.Session["DEPOTID"].ToString());
                gvProcessEndItem.DataSource = dt;
                gvProcessEndItem.DataBind();
                gvProcessEndItem.Visible = true;
            }
        }
    }

    protected void gvProcessEndItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string BatchNO = commandArgs[0];
        string ProductionOrderID = commandArgs[1];

        if (e.CommandName == "BATCHNO")
        {
            DataTable dt = new DataTable();
            foreach (GridViewRow gvr in gvProcessEndItem.Rows)
            {
                Label lblProductionID = (Label)gvr.FindControl("lblProductionID");
                LinkButton lnkBatchNo = (LinkButton)gvr.FindControl("lnkBatchNo");
                ShowProduction.Style["display"] = "block";
                //light2.Style["display"] = "block";
                dt = oFactoryUpdate.BindProductionQty(lblProductionID.Text, HttpContext.Current.Session["DEPOTID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    GvProductionQty.DataSource = dt;
                    GvProductionQty.DataBind();
                }
                else
                {
                    GvProductionQty.DataSource = null;
                    GvProductionQty.DataBind();
                }
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ShowProduction.Style["display"] = "none";
    }

    protected void txtProducedQtyKg_TextChanged(object sender, EventArgs e)
    {
        decimal ProducedQty = 0;
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        Label lblItemName = (Label)currentRow.FindControl("lblItemName");
        Label lblConversionQty = (Label)currentRow.FindControl("lblConversionQty");
        TextBox txtProducedQtyKg = (TextBox)currentRow.FindControl("txtProducedQtyKg");
        TextBox txtProducedQty = (TextBox)currentRow.FindControl("txtProducedQty");
        Label lblEstimatedQtyKG = (Label)currentRow.FindControl("lblEstimatedQtyKG");

        decimal EstimatedQty = Convert.ToDecimal(lblEstimatedQtyKG.Text);
        decimal ProducedQtyKg = Convert.ToDecimal(txtProducedQtyKg.Text);

        if (EstimatedQty >= ProducedQtyKg)
        {
            ProducedQty = Convert.ToDecimal(lblConversionQty.Text) * Convert.ToDecimal(txtProducedQtyKg.Text);
            txtProducedQty.Text = Convert.ToString(ProducedQty);
        }
        else
        {
            MessageBox1.ShowInfo("Actual Produced Qty can not be grater than Estimated Qty <font color='red'>" + lblItemName.Text + "</font>", 60, 700);
            txtProducedQtyKg.Text = Convert.ToString(lblEstimatedQtyKG.Text);
            return;
        }
    }

    protected void txtRejectedQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtRejectedQtytxt = (TextBox)currentRow.FindControl("txtRejectedQty");
        TextBox txtProducedQty = (TextBox)currentRow.FindControl("txtProducedQty");
        Label lblYield = (Label)currentRow.FindControl("lblYield");
        lblYield.Text = String.Format("{0:#0.000}", (Convert.ToDecimal(txtProducedQty.Text) / (Convert.ToDecimal(txtProducedQty.Text) + Convert.ToDecimal(txtRejectedQtytxt.Text)) * 100));
    }

    protected void chkyn_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        foreach (GridViewRow gvr in gvProcessEndItem.Rows)
        {
            TextBox txtRejectedQtytxt = (TextBox)gvr.FindControl("txtRejectedQty");
            TextBox txtProducedQty = (TextBox)gvr.FindControl("txtProducedQty");

            //if (txtRejectedQtytxt.Text != "" && txtRejectedQtytxt.Text == "0")
            //{
            light2.Style["display"] = "block";
            decimal WastageQty = Convert.ToDecimal(txtProducedQty.Text) + (Convert.ToDecimal(txtRejectedQtytxt.Text));
            dt = oFactoryUpdate.BindLightBox_IssueDetails(ddlbatch.SelectedValue.Trim(), ddlProcess.SelectedValue.Trim(), WastageQty);
            if (dt.Rows.Count > 0)
            {
                GV_ISSUE.DataSource = dt;
                GV_ISSUE.DataBind();
                divbtnSubmit1.Visible = true;
                Session["BINDISSUE"] = dt;
                foreach (GridViewRow Gvissue in GV_ISSUE.Rows)
                {
                    Label lblCATEGORYID = (Label)Gvissue.FindControl("lblCATEGORYID");
                    TextBox txtCONSUMABLESQTY = (TextBox)Gvissue.FindControl("txtCONSUMABLESQTY");
                    //string a = lblCATEGORYID.Text;
                    if (lblCATEGORYID.Text == "12" || lblCATEGORYID.Text == "285" || lblCATEGORYID.Text == "286" || lblCATEGORYID.Text == "290")
                    {
                        txtCONSUMABLESQTY.Enabled = true;
                    }
                }
            }
            else
            {
                GV_ISSUE.DataSource = null;
                GV_ISSUE.DataBind();
            }
            //}
            //else
            //{
            //    MessageBox1.ShowInfo("Sample Qty Can not be blank. <b><font color='red'> ");
            //}
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        light2.Style["display"] = "none";
        pnlAdd.Style["display"] = "";
        pnlDisplay.Style["display"] = "none";
        foreach (GridViewRow gvrow in gvProcessEndItem.Rows)
        {
            CheckBox chkBx = (CheckBox)gvrow.FindControl("chkyn");
            chkBx.Checked = false;
        }
    }

    #region Create DataTable Structure
    public DataTable CreateDataTableForIssue()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALID", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dt.Columns.Add(new DataColumn("CONSUMABLESQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("WASTAGEQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("WIPQTY", typeof(string)));
        HttpContext.Current.Session["ISSUE"] = dt;
        return dt;
    }
    #endregion

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            Session["ISSUE"] = null;
            if (Session["ISSUE"] == null)
            {
                CreateDataTableForIssue();
            }
            string xml = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)Session["ISSUE"];
            int result = 0;
            if (GV_ISSUE.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in GV_ISSUE.Rows)
                {
                    Label lblCATEGORYID = (Label)gvrow.FindControl("lblCATEGORYID");
                    Label lblCATEGORYNAME = (Label)gvrow.FindControl("lblCATEGORYNAME");
                    Label lblMATERIALID = (Label)gvrow.FindControl("lblMATERIALID");
                    Label lblMATERIALNAME = (Label)gvrow.FindControl("lblMATERIALNAME");
                    Label lblUOMID = (Label)gvrow.FindControl("lblUOMID");
                    Label lblUOMNAME = (Label)gvrow.FindControl("lblUOMNAME");
                    TextBox lblISSUEQTY = (TextBox)gvrow.FindControl("lblISSUEQTY");
                    TextBox txtCONSUMABLESQTY = (TextBox)gvrow.FindControl("txtCONSUMABLESQTY");
                    TextBox txtWastage = (TextBox)gvrow.FindControl("txtWastage");
                    TextBox lblReturnQty = (TextBox)gvrow.FindControl("lblReturnQty");
                    TextBox txtWipQty = (TextBox)gvrow.FindControl("txtWipQty");
                    //decimal FinalQty = Convert.ToDecimal(lblISSUEQTY.Text.Trim()) - Convert.ToDecimal(txtRejectedQty.Text.Trim());
                    if (Convert.ToDecimal(lblISSUEQTY.Text.Trim()) >= Convert.ToDecimal(txtWastage.Text.Trim()))
                    {
                        if (Convert.ToDecimal(txtWipQty.Text) >= 0)
                        {
                            DataRow drQty = dt.NewRow();
                            drQty["CATEGORYID"] = Convert.ToString(lblCATEGORYID.Text).Trim();
                            drQty["CATEGORYNAME"] = Convert.ToString(lblCATEGORYNAME.Text).Trim();
                            drQty["MATERIALID"] = Convert.ToString(lblMATERIALID.Text).Trim();
                            drQty["MATERIALNAME"] = Convert.ToString(lblMATERIALNAME.Text).Trim();
                            drQty["QTY"] = Convert.ToString(lblReturnQty.Text).Trim();//String.Format("{0:#0.000}", FinalQty);//Convert.ToString(lblISSUEQTY.Text).Trim();
                            drQty["UOMID"] = Convert.ToString(lblUOMID.Text).Trim();
                            drQty["UOMNAME"] = Convert.ToString(lblUOMNAME.Text).Trim();
                            drQty["REQUIREDFROMDATE"] = "";
                            drQty["REQUIREDTODATE"] = "";
                            drQty["CONSUMABLESQTY"] = Convert.ToDecimal(txtCONSUMABLESQTY.Text);
                            drQty["WASTAGEQTY"] = Convert.ToDecimal(txtWastage.Text);
                            drQty["WIPQTY"] = Convert.ToDecimal(txtWipQty.Text);
                            dt.Rows.Add(drQty);
                            dt.AcceptChanges();
                        }
                        else
                        {
                            MessageBox1.ShowInfo("Wip Qty Can not be less than issue Qty <b><font color='red'></font></b>... ", 60, 500);
                            light2.Style["display"] = "block";
                            return;
                        }
                    }
                    else
                    {
                        Session["MATERIALNAME"] = lblMATERIALNAME.Text.Trim();
                        result = 1;
                        break;
                    }
                }
            }
            if (result == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    xml = ConvertDatatableToXML(dt);
                    Session["XML_ISSUE"] = xml;
                }
                light2.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                result = 0;
            }
            else
            {
                MessageBox1.ShowInfo("Issue Qty must be less than or equal to Rejected Qty <b><font color='red'> " + Session["MATERIALNAME"] + " </font></b>... ", 60, 500);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void Clear()
    {
        ddlProcess.SelectedValue = "-1";
        ddlstate.SelectedValue = "I";
        grdQtySchemeDetails.DataSource = null;
        grdQtySchemeDetails.DataBind();
        gvProcessEndItem.DataSource = null;
        gvProcessEndItem.DataBind();
        Session["XML_ISSUE"] = null;
        txtProductiondt.Text = "";
        txtRemarks.Text = "";
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
        CalendarExtender2.StartDate = oDate;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtProductiondt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender2.EndDate = today1;
        }
        else
        {
            this.txtProductiondt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender2.EndDate = cDate;
        }
    }
    #endregion

    #region Return Approve
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (grdStoreReturn.Rows.Count <= 0)
        {
            MessageBox1.ShowWarning("<b><font color='Red'>No Record Found For Store Return.!</font></b>");
        }
        else
        {
            foreach (GridViewRow gvr in grdStoreReturn.Rows)
            {
                CheckBox chkBx = (CheckBox)gvr.FindControl("chkreq");
                if (chkBx.Checked == true)
                {
                    Label lblProductionID = (Label)gvr.FindControl("lblProductionID");
                    string a = lblProductionID.Text;
                    oFactoryUpdate.StoreReturnApprove(a);
                    MessageBox1.ShowSuccess("<b><font color='green'>Store Return approved successfully!</font></b>");
                    grdStoreReturn.DataSource = null;
                    grdStoreReturn.DataBind();
                }
                else
                {
                    MessageBox1.ShowInfo("Plese Select atleast 1 record... ");
                }
            }
        }
    }
    #endregion
    public void BindStoreReturn()
    {
        DataTable dt = new DataTable();
        dt = oFactoryUpdate.BindStoreReturn(HttpContext.Current.Session["FINYEAR"].ToString(), HttpContext.Current.Session["DEPOTID"].ToString());
        if (dt.Rows.Count > 0)
        {
            grdStoreReturn.DataSource = dt;
            grdStoreReturn.DataBind();
            grdStoreReturn.Visible = true;
        }
    }

    protected void chkRS_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        foreach (GridViewRow gvr in grdStoreReturn.Rows)
        {
            CheckBox chkBx = (CheckBox)gvr.FindControl("chkRS");
            if (chkBx.Checked)
            {
                TextBox txtRejectedQtytxt = (TextBox)gvr.FindControl("txtRejectedQty");
                TextBox txtProducedQty = (TextBox)gvr.FindControl("txtProducedQty");
                Label lblProductionID = (Label)gvr.FindControl("lblProductionID");
                decimal WastageQty = Convert.ToDecimal(txtProducedQty.Text) + (Convert.ToDecimal(txtRejectedQtytxt.Text));
                light2.Style["display"] = "block";
                dt = oFactoryUpdate.BindWestageDtls(lblProductionID.Text, WastageQty);
                if (dt.Rows.Count > 0)
                {
                    GV_ISSUE.DataSource = dt;
                    GV_ISSUE.DataBind();
                    divbtnSubmit1.Visible = false;
                }
                else
                {
                    GV_ISSUE.DataSource = null;
                    GV_ISSUE.DataBind();
                }
            }
        }
    }
    protected void GV_ISSUE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtwipQty = (TextBox)e.Row.FindControl("txtwipQty");
            Label lblMATERIALNAME = (Label)e.Row.FindControl("lblMATERIALNAME");

            if (Convert.ToDecimal(txtwipQty.Text) < 0)
            {
                //e.Row.ForeColor = System.Drawing.Color.Red;
                txtwipQty.BackColor = System.Drawing.Color.Yellow;
                txtwipQty.Font.Size = 9;
                lblMATERIALNAME.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ProcessMode = this.ddlstate.SelectedValue;
        if(ProcessMode=="I") /*process Start*/
        {
            this.StartProductionOrder();
        }
        else if (ProcessMode == "O") /*Process End*/
        {
            this.EndProductionOrder();
        }
    }
}