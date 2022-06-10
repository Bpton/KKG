using BAL;
using Obout.Grid;
using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmInterStoreReturn : System.Web.UI.Page
{
    string Checker;
    string MENUID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadFactory();
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                MENUID = Request.QueryString["MENUID"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    if (Session["SESSION_INTERSTORERETURN"] != null)
                    {
                        Session["SESSION_INTERSTORERETURN"] = null;
                    }
                    this.btnaddhide.Visible = false;
                    this.divbtnSubmit.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                    LoadTransferDetails();


                }
                else
                {
                    if (Session["SESSION_INTERSTORERETURN"] != null)
                    {
                        Session["SESSION_INTERSTORERETURN"] = null;
                    }
                    this.btnaddhide.Visible = true;
                    this.divbtnSubmit.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                    LoadTransferDetails();

                }
                #region Add FinYear Wise Date Lock
                
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
                CalendarExtender1.StartDate = oDate;

                if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
                {
                    this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtReturnDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    Calendar1.EndDate = today1;
                    CalendarExtender4.EndDate = today1;
                    CalendarExtender1.EndDate = today1;
                }
                else
                {
                    this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtReturnDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    Calendar1.EndDate = cDate;
                    CalendarExtender4.EndDate = cDate;
                    CalendarExtender1.EndDate = cDate;
                }
                #endregion
               
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadTransferDetails()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            gvOpenStock.DataSource = ClsStockJournal.BindInterTransferDetails(this.txtfromdateins.Text.Trim(), this.txttodateins.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Checker);
            gvOpenStock.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadFactory
    public void LoadFactory()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            DataTable dt = new DataTable();
            this.ddlDeptName.Items.Clear();
            dt = ClsStockJournal.BindFactory(Session["UserID"].ToString());
            this.ddlDeptName.DataSource = dt;
            this.ddlDeptName.DataValueField = "VENDORID";
            this.ddlDeptName.DataTextField = "VENDORNAME";
            this.ddlDeptName.DataBind();
            
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
        ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
        ddlreason.Items.Clear();
        ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
        ddlreason.AppendDataBoundItems = true;
        ddlreason.DataSource = clsReceivedStock.BindReason("1594");
        ddlreason.DataValueField = "ID";
        ddlreason.DataTextField = "DESCRIPTION";
        ddlreason.DataBind();
    }
    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearData();
            this.CreateInterStoreReturnTable();
            this.loadProduct();
            FromLoadStoreloacation();
            ToLoadStoreloacation();
            LoadReason();
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.ddlToLocation.Enabled = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void ResetTable()
    {
        Session.Remove("SESSION_INTERSTORERETURN");
    }

    public void ClearData()
    {
        this.hdnDepotID.Value = "";
        this.hdn_adjustmentid.Value = "";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.btnaddhide.Style["display"] = "";
        this.tradjunmentheader.Style["display"] = "none";
        this.ddlProduct.SelectedValue = "0";
        this.ddlPackingSize.SelectedValue = "0";
        this.txtStockqty.Text = "";
        this.grdReturn.DataSource = null;
        this.grdReturn.DataBind();
        this.ddlToLocation.SelectedValue = "0";
        this.ddlDeptName.Enabled = true;
        /*DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        this.txtopeningdate.Text = dtcurr.ToString(date).Replace('-', '/');*/
        this.ResetTable();
        txtstockinhand.Text = "0";
        txtTotockInHand.Text = "0";
        txtPosPCS.Text = "0";
        txtNegPCS.Text = "0";
    }

    public void loadProduct()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            ddlProduct.AppendDataBoundItems = true;
            ddlProduct.DataSource = ClsStockJournal.BindProduct("-1", "-1");
            ddlProduct.DataTextField = "NAME";
            ddlProduct.DataValueField = "ID";
            ddlProduct.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void ToLoadStoreloacation()
    {
        string mode = "ToLocation";
        ClsVendor_TPU ClsStockJournal = new ClsVendor_TPU();
        DataTable Dt = new DataTable();
        Dt = ClsStockJournal.BindPoFromTpu(mode,this.ddlfromstorelocation.SelectedValue);
        if (Dt.Rows.Count > 0)
        {
            ddlToLocation.Items.Clear();
            ddlToLocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
            ddlToLocation.AppendDataBoundItems = true;
            ddlToLocation.DataSource = Dt;

            ddlToLocation.DataValueField = "ID";
            ddlToLocation.DataTextField = "Name";
            ddlToLocation.DataBind();
        }
    }

    public void FromLoadStoreloacation()
    {
        string mode ="";
        if (hdn_adjustmentid.Value == "")
        {
            mode = "fromLocation";
        }
        else
        {
            mode = "ToLocation";
        }
        
        ClsVendor_TPU ClsStockJournal = new ClsVendor_TPU();
        DataTable Dt = new DataTable();
        Dt = ClsStockJournal.BindPoFromTpu(mode,Session["UserID"].ToString());
        if (Dt.Rows.Count > 0)
        {
            ddlfromstorelocation.Items.Clear();
            ddlfromstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
            ddlfromstorelocation.AppendDataBoundItems = true;
            ddlfromstorelocation.DataSource = Dt;

            ddlfromstorelocation.DataValueField = "ID";
            ddlfromstorelocation.DataTextField = "Name";
            ddlfromstorelocation.DataBind();

            if (Dt.Rows.Count == 1)
            {
                this.ddlfromstorelocation.SelectedValue = Convert.ToString(Dt.Rows[0]["ID"]);
            }
        }
    }

    protected void CreateInterStoreReturnTable()
    {
        DataTable dtInterStoreReturn = new DataTable();

        dtInterStoreReturn.Columns.Add("GUID");
        dtInterStoreReturn.Columns.Add("PRODUCTID");
        dtInterStoreReturn.Columns.Add("PRODUCTNAME");
        dtInterStoreReturn.Columns.Add("BATCHNO");
        dtInterStoreReturn.Columns.Add("PACKINGSIZEID");
        dtInterStoreReturn.Columns.Add("PACKINGSIZENAME");
        dtInterStoreReturn.Columns.Add("ADJUSTMENTQTY");
        dtInterStoreReturn.Columns.Add("PRICE");
        dtInterStoreReturn.Columns.Add("AMOUNT");
        dtInterStoreReturn.Columns.Add("REASONID");
        dtInterStoreReturn.Columns.Add("REASONNAME");
        dtInterStoreReturn.Columns.Add("STORELOCATIONID");
        dtInterStoreReturn.Columns.Add("STORELOCATIONNAME");
        dtInterStoreReturn.Columns.Add("MFDATE");
        dtInterStoreReturn.Columns.Add("EXPRDATE");
        dtInterStoreReturn.Columns.Add("ASSESMENTPERCENTAGE");
        dtInterStoreReturn.Columns.Add("MRP");
        dtInterStoreReturn.Columns.Add("WEIGHT");

        HttpContext.Current.Session["SESSION_INTERSTORERETURN"] = dtInterStoreReturn;
    }


    protected void ddlToLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProduct.SelectedValue != "")
            {
                this.txtstockinhand.Text=Convert.ToString(getStockQty(this.ddlToLocation.SelectedValue.ToString()));
                this.lblstockinhand.Text = Convert.ToString(this.ddlToLocation.SelectedItem.Text + "  " + "Stock");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public decimal getStockQty(string id)
    {
        
        decimal stockQty = 0;
        ClsStockJournal ClsStockJournal = new ClsStockJournal();
        stockQty = ClsStockJournal.GetQty(this.ddlDeptName.SelectedValue,this.ddlProduct.SelectedValue, id);
        return stockQty;
    }


    protected void btnAddStock_Click(object sender, EventArgs e)
    {
        try
        {
            decimal adjustmentqty = Convert.ToDecimal(txtStockqty.Text.Trim());
            decimal stockqty = Convert.ToDecimal(txtTotockInHand.Text.Trim());
            decimal remainingqty = 0;
            if (Session["SESSION_INTERSTORERETURN"] == null)
            {
                this.CreateInterStoreReturnTable();
            }
            DataTable dtProductdetails = (DataTable)HttpContext.Current.Session["SESSION_INTERSTORERETURN"];
           
            if (ddlreason.SelectedValue != "0")
            {
                
                    remainingqty = (stockqty-Math.Abs(adjustmentqty));

                    if (adjustmentqty == 0)
                    {
                        MessageBox1.ShowInfo("<b>Adjustment Qty should be greater than<font color='red'> 0</font></b> !", 40, 500);
                        return;
                    }
                    else if (remainingqty < 0 )
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Stock qty Cannot be negaative</font></b> !", 40, 450);
                        return;
                    }
                    else
                    {
                    string  id= Convert.ToString(Guid.NewGuid());
                    for (int i = 0; i <= 1; i++)
                    {
                        DataRow dr = dtProductdetails.NewRow();
                        dr["GUID"] = id;
                        dr["PRODUCTID"] = Convert.ToString(this.ddlProduct.SelectedValue);
                        dr["PRODUCTNAME"] = Convert.ToString(this.ddlProduct.SelectedItem.Text);
                        dr["BATCHNO"] = "NA";
                        dr["PACKINGSIZEID"] = Convert.ToString(this.ddlPackingSize.SelectedValue);
                        dr["PACKINGSIZENAME"] = Convert.ToString(this.ddlPackingSize.SelectedItem.Text);
                       
                        dr["PRICE"] = Convert.ToString(0);
                        dr["AMOUNT"] = Convert.ToString(0);
                        dr["REASONID"] = Convert.ToString(this.ddlreason.SelectedValue);
                        dr["REASONNAME"] = Convert.ToString(this.ddlreason.SelectedItem.Text);
                        if (i == 0)
                        {
                            dr["STORELOCATIONID"] = Convert.ToString(this.ddlToLocation.SelectedValue.ToString());
                            dr["STORELOCATIONNAME"] = Convert.ToString(this.ddlToLocation.SelectedItem.Text);
                            dr["ADJUSTMENTQTY"] = Convert.ToString(Convert.ToDecimal(txtStockqty.Text.Trim()));
                        }
                        else
                        {
                            dr["STORELOCATIONID"] = Convert.ToString(this.ddlfromstorelocation.SelectedValue.ToString());
                            dr["STORELOCATIONNAME"] = Convert.ToString(this.ddlfromstorelocation.SelectedItem.Text);
                            dr["ADJUSTMENTQTY"] = Convert.ToString(-1*Convert.ToDecimal(txtStockqty.Text.Trim()));
                        }
                        
                        dr["MFDATE"] = "01/01/1900";
                        dr["EXPRDATE"] = "01/01/1900";
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                        dr["MRP"] = Convert.ToString(0);
                        dr["WEIGHT"] = Convert.ToString(0);
                    
                        dtProductdetails.Rows.Add(dr);
                        dtProductdetails.AcceptChanges();
                    
                    }
                        

                        if (dtProductdetails.Rows.Count > 0)
                        {
                            this.grdReturn.DataSource = dtProductdetails;
                            this.grdReturn.DataBind();
                            UpdateRowCount();

                        if(this.txtNegPCS.Text.Trim()=="")
                        {
                            this.txtNegPCS.Text = "0";
                        }
                        if (this.txtPosPCS.Text.Trim() == "")
                        {
                            this.txtPosPCS.Text = "0";
                        }

                        for (int i = 0; i < dtProductdetails.Rows.Count; i++)
                            {
                                decimal AdjustmentQty = 0;
                                AdjustmentQty = Convert.ToDecimal(dtProductdetails.Rows[i]["ADJUSTMENTQTY"].ToString());
                                if (AdjustmentQty > 0)
                                {
                                    this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                                }
                                else
                                {
                                    this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                                }
                            }
                        }
                        else
                        {
                            this.grdReturn.DataSource = null;
                            this.grdReturn.DataBind();
                            this.txtPosPCS.Text = "0";
                            this.txtNegPCS.Text = "0";
                        }

                        this.ddlProduct.SelectedValue = "0";
                        this.ddlPackingSize.SelectedValue = "0";
                        //this.ddlToLocation.SelectedValue = "0";
                        this.txtStockqty.Text = "";
                    }
                
               
            }

            else
            {
                MessageBox1.ShowInfo("<b> Select Reason!</b>", 40, 300);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlToLocation.ClientID + "').focus(); ", true);
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void UpdateRowCount()
    {
        decimal Count= Convert.ToDecimal(grdReturn.Rows.Count);
        if (Count > 0) 
        {
            this.ddlToLocation.Enabled = false;
        }
        else
        {
            this.ddlToLocation.Enabled = true;
        }
    }

    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblGUID = (Label)gvr.FindControl("lblGUID");
            this.Hdn_Fld.Value = lblGUID.Text.Trim();
            string sGUID = Convert.ToString(this.Hdn_Fld.Value);
            DataTable dtdeleteStock = new DataTable();
            dtdeleteStock = (DataTable)Session["SESSION_INTERSTORERETURN"];

            DataRow[] drr = dtdeleteStock.Select("GUID='" + sGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteStock.AcceptChanges();
            }
            Session["SESSION_INTERSTORERETURN"] = dtdeleteStock;

            if (dtdeleteStock.Rows.Count > 0)
            {
                this.grdReturn.DataSource = dtdeleteStock;
                this.grdReturn.DataBind();

                for (int i = 0; i < dtdeleteStock.Rows.Count; i++)
                {
                    decimal AdjustmentQty = 0;
                    AdjustmentQty = Convert.ToDecimal(dtdeleteStock.Rows[i]["ADJUSTMENTQTY"].ToString());
                    if (AdjustmentQty > 0)
                    {
                        this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                    }
                    else
                    {
                        this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                    }
                }
            }
            else
            {
                this.grdReturn.DataSource = null;
                this.grdReturn.DataBind();
                this.txtPosPCS.Text = "0";
                this.txtNegPCS.Text = "0";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string stockadjustmentno = string.Empty;
            string mode = string.Empty;
            DataTable dtadjustment = new DataTable();
            dtadjustment = (DataTable)HttpContext.Current.Session["SESSION_INTERSTORERETURN"];
            if (dtadjustment.Rows.Count > 0)
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();
                string xml = ConvertDatatableToXML(dtadjustment);
                stockadjustmentno = ClsStockJournal.InsertInterStoreReturnDetails(this.txtReturnDate.Text.Trim(), this.ddlDeptName.SelectedValue.ToString(), this.ddlDeptName.SelectedItem.Text, HttpContext.Current.Session["IUserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Convert.ToString(hdn_adjustmentid.Value), xml, this.txtRemarks.Text.Trim(), MENUID,this.ddlfromstorelocation.SelectedValue.ToString(),this.ddlToLocation.SelectedValue.ToString());

                if (stockadjustmentno != "")
                {
                    if (Convert.ToString(hdn_adjustmentid.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Return No :  <b><font color='green'>" + stockadjustmentno + "</font></b>  saved successfully", 60, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Return No :  <b><font color='green'>" + stockadjustmentno + "</font></b> updated successfully", 60, 550);
                    }

                    this.LoadTransferDetails();
                    this.ClearData();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful..</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please entry atleast 1 product</b>");
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
            this.ClearData();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            string Adjustmentid = hdn_adjustmentid.Value.ToString();
            int flag = 0;
            string MODE = "APPROVE";
            string USERID = HttpContext.Current.Session["IUserID"].ToString();
            flag = ClsStockJournal.ApproveRejectinterStore(MODE,Adjustmentid, USERID);
            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.LoadTransferDetails();
                this.hdn_adjustmentid.Value = "";
                MessageBox1.ShowSuccess(" <b><font color='green'>" + this.txtadjustmentno.Text + "</font></b> Approved Successfully.", 60, 550);
            }
            else
            {
                MessageBox1.ShowError("Error on Approve Record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        
        try
        {
            string USERID = HttpContext.Current.Session["IUserID"].ToString();
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            string Adjustmentid = hdn_adjustmentid.Value.ToString();
            int flag = 0;
            string MODE = "REJECT";
            flag = ClsStockJournal.ApproveRejectinterStore(MODE, Adjustmentid, USERID);
            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.LoadTransferDetails();
                this.hdn_adjustmentid.Value = "";
                MessageBox1.ShowSuccess("<b><font color='green'>" + this.txtadjustmentno.Text + "</font></b> Approved Successfully.", 60, 550);
            }
            else
            {
                MessageBox1.ShowError("Error on Approve Record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void  btnSTfind_Click(object sender, EventArgs e)
    {
        this.LoadTransferDetails();
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SESSION_INTERSTORERETURN"] != null)
            {
                Session["SESSION_INTERSTORERETURN"] = null;
            }
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lbldrguid = (Label)gvr.FindControl("lbldrguid");
            Label lblSTATUS = (Label)gvr.FindControl("lblSTATUS");
            this.hdn_adjustmentid.Value = lbldrguid.Text.Trim();
            this.hdn_statusId.Value = lblSTATUS.Text.Trim(); ;
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            if (Session["SESSION_INTERSTORERETURN"] == null)
            {
                this.CreateInterStoreReturnTable();
            }
            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["SESSION_INTERSTORERETURN"];
            this.tradjunmentheader.Style["display"] = "";
            string adjustmentid = hdn_adjustmentid.Value.ToString();
            

            DataSet ds = new DataSet();
            ds = ClsStockJournal.EditReturnDetails(hdn_adjustmentid.Value);
            FromLoadStoreloacation();
            ToLoadStoreloacation();
            loadProduct();
            LoadReason();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtadjustmentno.Text = ds.Tables[0].Rows[0]["ADJUSTMENTNO"].ToString();
                this.txtReturnDate.Text = ds.Tables[0].Rows[0]["ADJUSTMENTDATE"].ToString();
                this.ddlDeptName.SelectedValue = ds.Tables[0].Rows[0]["DEPOTID"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                this.ddlfromstorelocation.SelectedValue = ds.Tables[0].Rows[0]["FRMSTOREID"].ToString();
                this.ddlToLocation.SelectedValue = ds.Tables[0].Rows[0]["TOSTOREID"].ToString();
                this.ddlToLocation.Enabled = false;
                this.ddlfromstorelocation.Enabled = false;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtadjustment.NewRow();
                        dr["GUID"] = Convert.ToString(ds.Tables[1].Rows[i]["GUID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                        dr["ADJUSTMENTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ADJUSTMENTQTY"]);
                        dr["PRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRICE"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONNAME"]);
                        dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);

                        dtadjustment.Rows.Add(dr);
                        dtadjustment.AcceptChanges();
                    }

                    if (dtadjustment.Rows.Count > 0)
                    {
                        this.grdReturn.DataSource = dtadjustment;
                        this.grdReturn.DataBind();
                        HttpContext.Current.Session["SESSION_INTERSTORERETURN"] = dtadjustment;

                        this.txtPosPCS.Text = "0";
                        this.txtNegPCS.Text = "0";
                        for (int i = 0; i < dtadjustment.Rows.Count; i++)
                        {
                            decimal AdjustmentQty = 0;
                            AdjustmentQty = Convert.ToDecimal(dtadjustment.Rows[i]["ADJUSTMENTQTY"].ToString());
                            if (AdjustmentQty > 0)
                            {
                                this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                            }
                            else
                            {
                                this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                            }
                        }
                    }
                    else
                    {
                        this.grdReturn.DataSource = null;
                        this.grdReturn.DataBind();
                        this.txtPosPCS.Text = "0";
                        this.txtNegPCS.Text = "0";
                    }
                }
            }
            this.ddlDeptName.Enabled = false;
            this.btnaddhide.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE" && ClsStockJournal.GetApproveStatus1(adjustmentid) == "1")
            {
                this.divbtnSubmit.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.divadd.Visible = false;
                string hdn_status = hdn_statusId.Value.ToString();
                if (hdn_status == "PENDING")
                {
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                }
                else
                {
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                }
                #endregion
            }
            else if (Checker == "TRUE" && ClsStockJournal.GetApproveStatus1(adjustmentid) == "0")
            {
                this.divbtnSubmit.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.divadd.Visible = false;
                string hdn_status = hdn_statusId.Value.ToString();
                if (hdn_status == "PENDING")
                {
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                }
                else
                {
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                }
               
            }

            else
            {
                if (ClsStockJournal.GetApproveStatus1(adjustmentid) == "1")
                {
                    this.divbtnSubmit.Visible = false;
                }
                else
                {
                    this.divbtnSubmit.Visible = true;
                }
            }
           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lbldrguid = (Label)gvr.FindControl("lbldrguid");
            this.hdn_adjustmentid.Value = lbldrguid.Text.Trim();
            string upath = string.Empty;
            upath = "frmRptInvoicePrint_FAC.aspx?StkJrnl_ID=" + hdn_adjustmentid.Value.Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            hdn_adjustmentid.Value = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

  
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsStockJournal ClsStockJournal = new ClsStockJournal();
        ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
        ddlPackingSize.Items.Clear();
        DataTable dtPackSize = new DataTable();
        dtPackSize = ClsStockJournal.BindPackingSize(ddlProduct.SelectedValue.ToString()); ;
        ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
        ddlPackingSize.AppendDataBoundItems = true;
        ddlPackingSize.DataSource = dtPackSize;
        ddlPackingSize.DataValueField = "PACKSIZEID_FROM";
        ddlPackingSize.DataTextField = "PACKSIZEName_FROM";
        ddlPackingSize.DataBind();

        if (dtPackSize.Rows.Count == 1)
        {
            this.ddlPackingSize.SelectedValue = dtPackSize.Rows[0]["PACKSIZEID_FROM"].ToString();
        }
        this.txtTotockInHand.Text=Convert.ToString(getStockQty(this.ddlfromstorelocation.SelectedValue));
        this.lblToStockInHand.Text = Convert.ToString(this.ddlfromstorelocation.SelectedItem.Text + "  " + "Stock");
        if(this.ddlToLocation.SelectedValue!="0")
        {
            this.txtstockinhand.Text = Convert.ToString(getStockQty(this.ddlToLocation.SelectedValue.ToString()));
            this.lblstockinhand.Text = Convert.ToString(this.ddlToLocation.SelectedItem.Text + "  " + "Stock");
        }
    }
}