using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using WorkFlow;
using Account;
using Utility;


public partial class VIEW_frmcheckerApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlADD.Style["display"] = "";
                InisiliseDate();
                //////this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                //////this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                ///////******************  LOCK CALENDAR BEYOND FINANCIAL YEAR  *********************/

                //////ClsStockReport clsstartfy = new ClsStockReport();
                //////string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

                //////DateTime oDate = Convert.ToDateTime(STARTDATE);

                //////ClsRptAccount clsVoucherEntry = new ClsRptAccount();
                //////string EndDate = clsVoucherEntry.GetEndDateOfFinYear(Session["FINYEAR"].ToString());
                //////DateTime cDate = Convert.ToDateTime(EndDate);

                //////CalendarFromDate.StartDate = oDate;
                //////CalendarFromDate.EndDate = cDate;

                //////CalendarExtenderToDate.StartDate = oDate;
                //////CalendarExtenderToDate.EndDate = cDate;            

                /* Get Last date for non cuurent financial year : on 27/03/2019 */

                //////string LastDate = clsVoucherEntry.GetStartDateOfFinYear(Session["FINYEAR"].ToString());
                //////DateTime dtcurr = DateTime.Now;
                //////if ((LastDate) != "")
                //////{
                //////    dtcurr = Convert.ToDateTime(LastDate);
                //////}
                //////else
                //////{
                //////    this.CalendarExtenderToDate.EndDate = DateTime.Now;
                //////    CalendarFromDate.EndDate = DateTime.Now;
                //////}
                /* End */
                //////string date = "dd/MM/yyyy";
                //////this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
                //////this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');

                /*LOCK CALENDAR*/
                ////////DateTime oDate1 = Convert.ToDateTime(txtFromDate.Text);
                ////////CalendarExtenderToDate.StartDate = oDate1;

                ////////DateTime oDate2 = Convert.ToDateTime(txtToDate.Text);
                ////////CalendarFromDate.EndDate = oDate2;
                /****************************/

                /*********End*********  LOCK CALENDAR BEYOND FINANCIAL YEAR  *********************/

                this.Loaddepot();
                this.trinvoice.Style["display"] = "";
                this.trvoucher.Style["display"] = "none";
                this.Bindmodule();
                this.divbtnreject.Style["display"] = "none";
                
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    private void InisiliseDate()
    {
        /* New code for date on 02/04/2019 */
        //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      SOUMITRA MONDAL       //

        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        CalendarFromDate.StartDate = oDate;
        CalendarExtenderToDate.StartDate = oDate;

        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");           

            CalendarFromDate.EndDate = today1;
            CalendarExtenderToDate.EndDate = today1;           

        }
        else
        {
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarFromDate.EndDate = cDate;
            CalendarExtenderToDate.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }

   

    private void Bindmodule()
    {
        clsSaleInvoice clsinvoice = new clsSaleInvoice();
        string offline = clsinvoice.getofflinetag();
        if (offline == "Y")
        {
            this.ddlModule.Items.Clear();
            this.ddlModule.Items.Add(new ListItem("Select", "0"));
            this.ddlModule.Items.Add(new ListItem("Sale Invoice", "1"));
            this.ddlModule.Items.Add(new ListItem("Voucher Journal", "2"));
            this.ddlModule.Items.Add(new ListItem("Voucher Payment", "15"));
            this.ddlModule.Items.Add(new ListItem("Voucher Receipt", "16"));
            this.ddlModule.Items.Add(new ListItem("Debit Note", "14"));
            this.ddlModule.Items.Add(new ListItem("Credit Note", "13"));
            this.ddlModule.Items.Add(new ListItem("Contra", "17"));
            //this.ddlModule.Items.Add(new ListItem("Trading Sale Invoice", "10"));
            this.ddlModule.Items.Add(new ListItem("Transporter Bill", "25"));
            this.ddlModule.Items.Add(new ListItem("Invoice Sale Return", "26"));
            this.ddlModule.Items.Add(new ListItem("Gross Sale Return", "27"));
            this.ddlModule.Items.Add(new ListItem("Purchase Received", "28"));
        }
        else
        {
            this.ddlModule.Items.Clear();
            this.ddlModule.Items.Add(new ListItem("Select", "0"));
            this.ddlModule.Items.Add(new ListItem("Sale Invoice", "1"));
            this.ddlModule.Items.Add(new ListItem("Depot Stock Received", "30"));
            this.ddlModule.Items.Add(new ListItem("Depot Stock Transfer", "29"));
            this.ddlModule.Items.Add(new ListItem("Factory Stock Received", "31"));
            this.ddlModule.Items.Add(new ListItem("Gross Sale Return", "27"));
            this.ddlModule.Items.Add(new ListItem("Invoice Sale Return", "26"));
            this.ddlModule.Items.Add(new ListItem("Purchase Received", "28"));
            this.ddlModule.Items.Add(new ListItem("Purchase Return", "35"));
            this.ddlModule.Items.Add(new ListItem("Trading Sale Invoice", "10"));
            this.ddlModule.Items.Add(new ListItem("Transporter Bill", "25"));
            this.ddlModule.Items.Add(new ListItem("Voucher Journal", "2"));
            this.ddlModule.Items.Add(new ListItem("Voucher Payment", "15"));
            this.ddlModule.Items.Add(new ListItem("Voucher Receipt", "16"));
            this.ddlModule.Items.Add(new ListItem("Debit Note", "14"));
            this.ddlModule.Items.Add(new ListItem("Credit Note", "13"));
            this.ddlModule.Items.Add(new ListItem("Contra", "17"));
        }

    }

    private void Loaddepot()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = clsVoucher.BindDepot(Convert.ToString(Session["IUserID"]).Trim());
            if (dt.Rows.Count > 0)
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select", "0"));
                this.ddlDepot.AppendDataBoundItems = true;
                this.ddlDepot.DataSource = dt;
                this.ddlDepot.DataValueField = "BRID";
                this.ddlDepot.DataTextField = "BRNAME";
                this.ddlDepot.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlDepot.SelectedValue = Convert.ToString(dt.Rows[0]["BRID"]);
                }
            }
            else
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlModule.SelectedValue == "1" || this.ddlModule.SelectedValue == "26" || this.ddlModule.SelectedValue == "27" || this.ddlModule.SelectedValue == "10") //  Sale invoice || Invoice Sale Return || Gross Sale Return || Trading Sale
        {
            this.tdBusinessSegment.Visible = true;
            this.tdgroup.Visible = true;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
            this.LoadBusinessSegment();
        }
        else if (this.ddlModule.SelectedValue == "25" || this.ddlModule.SelectedValue == "28" || this.ddlModule.SelectedValue == "29" || this.ddlModule.SelectedValue == "30" || this.ddlModule.SelectedValue == "31" || this.ddlModule.SelectedValue == "35")  //  Transporter Bill  || Purchase Received  || Stock Transfer  || Depot Stock Received  || Factory Stock Received || Purchase Return
        {
            this.tdBusinessSegment.Visible = false;
            this.tdgroup.Visible = false;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
        }
        else
        {
            this.tdBusinessSegment.Visible = false;
            this.tdgroup.Visible = false;
            this.trinvoice.Style["display"] = "none";
            this.trvoucher.Style["display"] = "";
        }

        this.ResetGrid();
        this.btnrejectvisibility();
    }

    private void LoadBusinessSegment()
    {
        try
        {
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = clsInvc.BindBusinessSegmentOnlineOffline(this.ddlModule.SelectedValue.Trim(),this.ddlDepot.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.ddlBusinessSegment.Items.Clear();
                this.ddlBusinessSegment.Items.Add(new ListItem("Select Group", "0"));
                this.ddlBusinessSegment.AppendDataBoundItems = true;
                this.ddlBusinessSegment.DataSource = dt;
                this.ddlBusinessSegment.DataValueField = "BSID";
                this.ddlBusinessSegment.DataTextField = "BSNAME";
                this.ddlBusinessSegment.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlBusinessSegment.SelectedValue = Convert.ToString(dt.Rows[0]["BSID"]).Trim();
                }
            }
            else
            {
                this.ddlBusinessSegment.Items.Clear();
                this.ddlBusinessSegment.Items.Add(new ListItem("Select Group", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadTransfer
    public void LoadStockTransfer()
    {
        try
        {
            ClsStockTransfer CLSTRANSFER = new ClsStockTransfer();
            DataTable dt = CLSTRANSFER.BindStockTranferApproval(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTransferStockReceived
    public void LoadTransferStockReceived()
    {
        try
        {
            ClsDepoReceived ClsDepotreceived = new ClsDepoReceived();
            DataTable dt = ClsDepotreceived.BindTranferStockReceivedApproval(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadInvoice
    public void LoadInvoice()
    {
        try
        {
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = clsInvc.BindInvoiceApproval(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadSaleReturn
    public void LoadSaleReturn(string ReturnType)
    {
        try
        {
            ClsSaleReturn clsReturn = new ClsSaleReturn();
            DataTable dt = clsReturn.BindSaleReturnApproval(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString(),ReturnType);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadPurchase_FactoryReceived
    public void LoadPurchase_FactoryReceived(string PurchaseType, string IsFromFactory)
    {
        try
        {
            ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
            DataTable dt = clsReceivedStock.BindPurchaseApproval(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString(), IsFromFactory);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadPurchaseReturn
    public void LoadPurchaseReturn(string PurchaseType)
    {
        try
        {
            ClsPurchaseReturn clsReceivedStock = new ClsPurchaseReturn();
            DataTable dt = clsReceivedStock.BindPurchaseReturnApproval(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTransporterBill
    protected void LoadTransporterBill()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        DataTable dt = new DataTable();
        dt = transporterbill.BindTransporterbillApproval(HttpContext.Current.Session["UserID"].ToString().Trim(), "TRUE", this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(),this.ddlDepot.SelectedValue.Trim());

        if (dt.Rows.Count > 0)
        {
            this.gvUnlockInvoice.DataSource = dt;
            this.gvUnlockInvoice.DataBind();

            foreach (TableCell headerCell in gvUnlockInvoice.HeaderRow.Cells)
            {
                // or access Controls with index
                //     headerCell.Controls[INDEX]
                CheckBox lblHeader = headerCell.FindControl("chkAllSelectinvREVERSE") as CheckBox;
                if (ddlDepot.SelectedItem.Text != "KOLKATA")
                {
                    lblHeader.Enabled = false;
                }
                else
                {
                    lblHeader.Enabled = true;
                }

            }

            foreach (GridViewRow gvrow in gvUnlockInvoice.Rows)
            {
                CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("chkSelectinv1");
                CheckBox chkSelectReverse = (CheckBox)gvrow.FindControl("chkSelectReverse");
                Label lblSALEINVOICEID = (Label)gvrow.FindControl("lblSALEINVOICEID");

                if (ddlDepot.SelectedItem.Text != "KOLKATA")
                {
                    chkSelectReverse.Enabled = false;
                }
                else
                {
                    chkSelectReverse.Enabled = true;
                }
                // CheckBox chkSelectReverse= (CheckBox)gvrow.FindControl("chkSelectReverse");
                string rcm = transporterbill.GetRCMheck_bill(lblSALEINVOICEID.Text);
                string tag = transporterbill.GetToHOStatus(lblSALEINVOICEID.Text);

                if (rcm == "Y")
                {
                    chkSelectReverse.Checked = true;

                }
                else
                {
                    chkSelectReverse.Checked = false;
                }

                if (tag == "Y")
                {
                    chkBxtoho.Checked = true;

                }
                else
                {
                    chkBxtoho.Checked = false;
                }

            }
        }
        else
        {
            this.ResetGrid();
        }
    }
    #endregion

    #region LoadVoucher
    public void LoadVoucher(string VoucherID)
    {
        try
        {
            DataTable dt = new DataTable();
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            if (ddlModule.SelectedValue == "13" || ddlModule.SelectedValue == "14")
            {
                dt = clsVoucher.VoucherHeaderDetails_CreditNote(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), VoucherID, "TRUE", this.ddlDepot.SelectedValue.Trim(), "N");
            }            
            else
            {
                dt = clsVoucher.VoucherHeaderDetails(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), VoucherID, "TRUE", this.ddlDepot.SelectedValue.Trim(), "N","");
            }
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockVoucher.DataSource = dt;
                this.gvUnlockVoucher.DataBind();
                /* Start  Adding by D.Mondal 20/09/2018  for : Adding Transfer to HO  */
                foreach (GridViewRow gvrow in gvUnlockVoucher.Rows)
                {
                    CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("chkSelectvou1");
                    Label lbISTRANSFERTOHO = (Label)gvrow.FindControl("lblISTRANSFERTOHO");


                    string tag = lbISTRANSFERTOHO.Text;
                    if (tag == "Y")
                    {
                        chkBxtoho.Checked = true;

                    }
                    else
                    {
                        chkBxtoho.Checked = false;
                    }

                }
                /* End Adding by D.Mondal 20/09/2018  for : Adding Transfer to HO */
            }
            else
            {
                this.gvUnlockVoucher.DataSource = null;
                this.gvUnlockVoucher.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadTradingInvoice
    public void LoadTradingInvoice()
    {
        try
        {
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = clsInvc.BindTradingInvoiceApproval(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(), "TRUE", HttpContext.Current.Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ISREVERSEAPPLICABLE", typeof(string)));
                dt.Columns["ISREVERSEAPPLICABLE"].DefaultValue = "N";
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                this.ResetGrid();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Search
    public void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlModule.SelectedValue == "1" || this.ddlModule.SelectedValue == "26" || this.ddlModule.SelectedValue == "27" || this.ddlModule.SelectedValue == "28" || this.ddlModule.SelectedValue == "10" || this.ddlModule.SelectedValue == "35") //  Sale invoice || Invoice Sale Return || Gross Sale Return || Purchase Received || Purchase Return
            {
                this.gvUnlockInvoice.Columns[3].Visible = false;
                this.gvUnlockInvoice.Columns[4].Visible = false;
                this.gvUnlockInvoice.Columns[8].Visible = false;
                this.gvUnlockInvoice.Columns[9].Visible = true;
                this.gvUnlockInvoice.Columns[10].Visible = false;
                this.gvUnlockInvoice.Columns[11].Visible = false;
                this.gvUnlockInvoice.Columns[12].Visible = false;
                this.gvUnlockInvoice.Columns[13].Visible = false;
                this.gvUnlockInvoice.Columns[14].Visible = true;
                this.gvUnlockInvoice.Columns[15].Visible = true;
                this.gvUnlockInvoice.Columns[16].Visible = true;
                this.gvUnlockInvoice.Columns[17].Visible = true;

                if (this.ddlModule.SelectedValue == "1")                //  Sale invoice
                {
                    this.LoadInvoice();
                }
                else if (this.ddlModule.SelectedValue == "10")         //  Trading invoice
                {
                    this.LoadTradingInvoice();
                }
                else if (this.ddlModule.SelectedValue == "28")          //  Purchase Received
                {
                    this.gvUnlockInvoice.Columns[8].Visible = false;
                    this.gvUnlockInvoice.Columns[10].Visible = false;
                    this.gvUnlockInvoice.Columns[11].Visible = false;
                    this.gvUnlockInvoice.Columns[13].Visible = false;
                    this.LoadPurchase_FactoryReceived("", "T");                         // T - FROM TPU RECEIVED
                }
                else if (this.ddlModule.SelectedValue == "35")          //  Purchase Return
                {
                    this.gvUnlockInvoice.Columns[8].Visible = false;
                    this.gvUnlockInvoice.Columns[10].Visible = false;
                    this.gvUnlockInvoice.Columns[11].Visible = false;
                    this.gvUnlockInvoice.Columns[13].Visible = false;
                    this.LoadPurchaseReturn("");                         // T - FROM TPU RECEIVED
                }
                else if (this.ddlModule.SelectedValue == "26")          //  Invoice Sale Return
                {
                    this.gvUnlockInvoice.Columns[10].Visible = false;
                    this.gvUnlockInvoice.Columns[11].Visible = false;
                    this.gvUnlockInvoice.Columns[13].Visible = false;
                    this.LoadSaleReturn("N");
                }
                else                                                   //   Gross Sale Return
                {
                    this.gvUnlockInvoice.Columns[10].Visible = false;
                    this.gvUnlockInvoice.Columns[11].Visible = false;
                    this.gvUnlockInvoice.Columns[13].Visible = false;
                    this.LoadSaleReturn("Y");
                }
            }
            else if (this.ddlModule.SelectedValue == "15")  //Advance Payment
            {
                this.LoadVoucher(this.ddlModule.SelectedValue);
                this.gvUnlockVoucher.Columns[3].Visible = true;
            }
            else if (this.ddlModule.SelectedValue == "16")  //Advance Receipt
            {
                this.LoadVoucher(this.ddlModule.SelectedValue);
                this.gvUnlockVoucher.Columns[3].Visible = false;
            }
            else if (this.ddlModule.SelectedValue == "13")  //Credit Note
            {
                this.LoadVoucher(this.ddlModule.SelectedValue);
                this.gvUnlockVoucher.Columns[3].Visible = true;     /* Visible= True : Added by D.Mondal 20/09/2018  */
            }
            else if (this.ddlModule.SelectedValue == "14")  //Debit Note
            {
                this.LoadVoucher(this.ddlModule.SelectedValue);
                this.gvUnlockVoucher.Columns[3].Visible = true;     /* Visible= True : Added by D.Mondal 20/09/2018  */
            }
            else if (this.ddlModule.SelectedValue == "17")  //Contra
            {
                string VoucherID = "1";
                this.LoadVoucher(VoucherID);
                this.gvUnlockVoucher.Columns[3].Visible = false;
            }
            else if (this.ddlModule.SelectedValue == "2")   //Voucher Journal
            {
                this.LoadVoucher(this.ddlModule.SelectedValue);
                this.gvUnlockVoucher.Columns[3].Visible = true;      /* Visible= True : Added by D.Mondal 01/11/2018  */   /*Block auto transfer to HO As instructed by Sandip Biswas talk with Sukhdev on 10-10-2017 */
            }
            else if (this.ddlModule.SelectedValue == "25")   //Transporter Bill
            {
                this.gvUnlockInvoice.Columns[3].Visible = true;
                this.gvUnlockInvoice.Columns[4].Visible = true;
                this.gvUnlockInvoice.Columns[8].Visible = true;
                this.gvUnlockInvoice.Columns[9].Visible = false;
                this.gvUnlockInvoice.Columns[10].Visible = false;
                this.gvUnlockInvoice.Columns[11].Visible = false;
                this.gvUnlockInvoice.Columns[12].Visible = true;
                this.gvUnlockInvoice.Columns[13].Visible = true;
                this.gvUnlockInvoice.Columns[14].Visible = false;
                this.gvUnlockInvoice.Columns[15].Visible = true;
                this.gvUnlockInvoice.Columns[16].Visible = true;
                this.gvUnlockInvoice.Columns[17].Visible = true;

                this.LoadTransporterBill();
            }
            else if (this.ddlModule.SelectedValue == "29" || this.ddlModule.SelectedValue == "30" || this.ddlModule.SelectedValue == "31")   //Stock Transfer || Depot Transfer Stock Received || Factory Transfer Stock Received
            {
                if (this.ddlModule.SelectedValue == "29")
                {
                    this.LoadStockTransfer();
                }
                else if (this.ddlModule.SelectedValue == "30")
                {
                    this.LoadTransferStockReceived();
                }
                else if (this.ddlModule.SelectedValue == "31")
                {
                    this.LoadPurchase_FactoryReceived("", "F");                         // T - FROM FACTORY RECEIVED
                }
                this.gvUnlockInvoice.Columns[3].Visible = false;
                this.gvUnlockInvoice.Columns[4].Visible = false;
                this.gvUnlockInvoice.Columns[8].Visible = false;
                this.gvUnlockInvoice.Columns[9].Visible = true;
                this.gvUnlockInvoice.Columns[10].Visible = false;
                this.gvUnlockInvoice.Columns[11].Visible = false;
                this.gvUnlockInvoice.Columns[12].Visible = false;
                this.gvUnlockInvoice.Columns[13].Visible = false;
                this.gvUnlockInvoice.Columns[14].Visible = true;
                this.gvUnlockInvoice.Columns[15].Visible = true;
                this.gvUnlockInvoice.Columns[16].Visible = true;
                this.gvUnlockInvoice.Columns[17].Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.ResetGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void ResetGrid()
    {
        try
        {
            this.gvUnlockInvoice.DataSource = null;
            this.gvUnlockInvoice.DataBind();
            this.gvUnlockVoucher.DataSource = null;
            this.gvUnlockVoucher.DataBind();
            this.txtRejectionNote.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsSMS sms = new ClsSMS();
            int totalcount = 0;
            string Realisedno = "x";
            string module = Convert.ToString(this.ddlModule.SelectedValue.Trim());
            GridView gvunlock = gvUnlockInvoice;
            string gvchkbox = "";
            string gvchktranferHO = "";
            string gvchkReverse = "";
            string gvinvoiceid = "";
            string gvinvoiceno = "";
            string gvinvoicedate = "";
            string gvbilltype = "";

            if (module == "1" || module == "10" || module == "25" || module == "26" || module == "27" || module == "28" || module == "29" || module == "30" || module == "31" || module == "35")
            /* Sale Invoice =1 && TransporterBill = 25 && Invoice Sale Return =26 && Gross Sale Return = 27 && Purchase = 28 && Stock Transfer = 29 && Transfer Stock Received = 30 && Factory Stock Received = 31 */
            {
                gvunlock = gvUnlockInvoice;
                gvchkbox = "chkSelect";
                gvchktranferHO = "chkSelectinv1";
                gvchkReverse = "chkSelectReverse";
                gvinvoiceid = "lblSALEINVOICEID";
                gvinvoiceno = "lblSALEINVOICENO";
                gvinvoicedate = "lblSALEINVOICEDATE";
                gvbilltype = "lblBILLTYPE";
            }
            else if (module == "15" || module == "16" || module == "2" || module == "13" || module == "14" || module == "17")
            {
                gvunlock = gvUnlockVoucher;
                gvchkbox = "chkSelectvou";
                gvchktranferHO = "chkSelectvou1";
                gvinvoiceid = "lblAccEntryID";
                gvinvoiceno = "lblVOUCHERNO";
                gvinvoicedate = "lblVOUCHERDate";
            }

            foreach (GridViewRow gvrow in gvunlock.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                if (chkBx.Checked)
                {
                    totalcount += 1;
                }
            }

            if (totalcount <= 300)
            {
                ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                int flag = 0;
                int totalapproval = 0;
                int successcount = 0;
                int failcount = 0;
                string SALEINVOICEID = string.Empty;
                string IstransferHO = "N";
                string Isreverse = "N";
                string FGStatus = "TRUE";
                string VoucherBranchID = "";
                string VoucherBranchName = "";
                string ChequeRealishedDate = "";
                string ExistsChequeRealishedDate = "";
                string IPAddress = Request.UserHostAddress.ToString().Trim();

                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblSALEINVOICEID = (Label)gvrow.FindControl("" + gvinvoiceid + "");
                    Label lblSALEINVOICENO = (Label)gvrow.FindControl("" + gvinvoiceno + "");
                    Label lblSALEINVOICEDATE = (Label)gvrow.FindControl("" + gvinvoicedate + "");
                    if (chkBx.Checked)
                    {
                        ChequeRealishedDate = DateTime.Now.ToString("dd/MM/yyy");       /* AT TIME OF RECEIPT APPROVAL ChequeRealishedDate SAME AS APPROVED DATE */

                        totalapproval += 1;

                        /*Add for VOUCHER_DELETE_PREAPPROVED by D.Mondal on 27/03/2019 */
                        int vda = clsVoucher.VOUCHER_DELETE_PREAPPROVED(Session["USERID"].ToString(), lblSALEINVOICEID.Text.Trim(), IPAddress);
                        if (module == "1")
                        {
                            flag = clsPurchaseStockReceipt.ApproveSaleInvoice(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), "1");
                        }
                        else if (module == "10")
                        {
                            flag = clsPurchaseStockReceipt.ApproveSaleInvoice(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), "2");
                        }
                        else if (module == "28")/*Purchase Stock Received*/
                        {
                            Label lblBILLTYPE = (Label)gvrow.FindControl("" + gvbilltype + "");
                            if (Convert.ToString(lblBILLTYPE.Text).Trim() == "FG")
                            {
                                FGStatus = "TRUE";
                            }
                            else if (Convert.ToString(lblBILLTYPE.Text).Trim() == "POP")
                            {
                                FGStatus = "FALSE";
                            }
                            else 
                            {
                                FGStatus = "FALSE";
                            }
                            flag = clsPurchaseStockReceipt.ApproveStockReceived(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), "0", "", lblSALEINVOICEDATE.Text.Trim(), "", this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), FGStatus, "");
                        }
                        else if (module == "35")/*Purchase Return*/
                        {
                            Label lblBILLTYPE = (Label)gvrow.FindControl("" + gvbilltype + "");
                            if (Convert.ToString(lblBILLTYPE.Text).Trim() == "FG")
                            {
                                FGStatus = "TRUE";
                            }
                            else if (Convert.ToString(lblBILLTYPE.Text).Trim() == "POP")
                            {
                                FGStatus = "FALSE";
                            }
                            else
                            {
                                FGStatus = "FALSE";
                            }
                            flag = clsPurchaseStockReceipt.ApprovePurchaseReturn(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim(), FGStatus,"");
                        }
                        else if (module == "26" || module == "27")
                        {
                            flag = clsPurchaseStockReceipt.ApproveSaleReturn(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
                        }
                        else if (this.ddlModule.SelectedValue == "29")                          //Stock Transfer
                        {
                            flag = clsPurchaseStockReceipt.ApproveStockTransfer(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString());
                        }
                        else if (this.ddlModule.SelectedValue == "30")                          //Transfer Stock Received
                        {
                            flag = clsPurchaseStockReceipt.ApproveStockDepotReceived(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString().Trim(), Session["USERID"].ToString().Trim(), "", "0", "", "", "", this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
                        }
                        else if (this.ddlModule.SelectedValue == "31")                          //Factory Stock Received
                        {
                            flag = clsPurchaseStockReceipt.ApproveFactoryStockDepotReceived(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString().Trim(), Session["USERID"].ToString().Trim(), "", "0", "", "", "", this.ddlDepot.SelectedValue.Trim(), this.ddlDepot.SelectedItem.ToString().Trim());
                        }
                        else if (module == "15")
                        {
                            CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("" + gvchktranferHO + "");
                            if (chkBxtoho.Checked)
                            {
                                IstransferHO = "Y";
                            }
                            else
                            {
                                IstransferHO = "N";
                            }

                            VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                            flag = clsPurchaseStockReceipt.ApproveBankPayment(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), Realisedno, VoucherBranchID, VoucherBranchName, IstransferHO);
                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        }
                        else if (module == "16")
                        {
                            VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                            ExistsChequeRealishedDate = clsPurchaseStockReceipt.CHEQUEREALISEDDATE(lblSALEINVOICEID.Text.Trim(), "16");   /* If unlocked parent recipt and again approved then exists ChequeRealishedDate pass as parameter */
                            if (!string.IsNullOrEmpty(ExistsChequeRealishedDate))
                            {
                                ChequeRealishedDate = ExistsChequeRealishedDate;
                            }

                            flag = clsPurchaseStockReceipt.ApproveBankReceipt(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), ChequeRealishedDate, Realisedno, VoucherBranchID, VoucherBranchName);
                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change

                            if (flag == 1)
                            {
                                sms.ACCOUNTS_SMS(lblSALEINVOICEID.Text.Trim());
                            }
                        }
                        //else if (module == "14")
                        //{
                        //    VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                        //    flag = clsPurchaseStockReceipt.ApproveBankReceipt(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), ChequeRealishedDate, Realisedno, VoucherBranchID, VoucherBranchName);
                        //    flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        //}
                        //else if (module == "13")
                        //{
                        //    VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                        //    flag = clsPurchaseStockReceipt.ApproveBankReceipt(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), ChequeRealishedDate, Realisedno, VoucherBranchID, VoucherBranchName);
                        //    flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        //}

                        else if (module == "14")    /* Modify By: D.Mondal  DEBIT NOTE 20/09/2018 */
                        {
                            string ChkFlag = "N";                            
                            VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                            CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("" + gvchktranferHO + "");
                            if (chkBxtoho.Checked)
                            {
                                ChkFlag = "Y";                               
                            }
                           
                            flag = clsPurchaseStockReceipt.ApproveVoucherDebit_CreditNote(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), VoucherBranchID, ChkFlag, "14");

                            ClsTransporterBill transporterbill = new ClsTransporterBill();
                            string tag = transporterbill.GetToHOStatus(lblSALEINVOICEID.Text);

                            //flag = clsPurchaseStockReceipt.UpdateAccEntryIsTranferToHO(lblSALEINVOICEID.Text.Trim(), tag, ChkFlag);

                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        }
                        else if (module == "13")    /* Modify By: D.Mondal CREADIT NOTE 20/09/2018 */
                        {
                            string ChkFlag = "N";
                            VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                            CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("" + gvchktranferHO + "");
                            if (chkBxtoho.Checked)
                            {
                                ChkFlag = "Y";
                            }
                           
                            flag = clsPurchaseStockReceipt.ApproveVoucherDebit_CreditNote(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), VoucherBranchID, ChkFlag, "13");

                            ClsTransporterBill transporterbill = new ClsTransporterBill();
                            string tag = transporterbill.GetToHOStatus(lblSALEINVOICEID.Text);

                            //flag = clsPurchaseStockReceipt.UpdateAccEntryIsTranferToHO(lblSALEINVOICEID.Text.Trim(), tag, ChkFlag);

                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        }
                        else if (module == "2")
                        {
                            IstransferHO = "N";
                            //IstransferHO = clsVoucher.IsTransferToHO_Ledger(lblSALEINVOICEID.Text.Trim());
                            //flag = clsPurchaseStockReceipt.ApproveVoucherJournal(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), IstransferHO);
                            //flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                            string IstransferHOTag = "N";

                            string tag;
                            CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("" + gvchktranferHO + "");
                            if (chkBxtoho.Checked)
                            {
                                IstransferHO = clsVoucher.IsTransferToHO_Ledger(lblSALEINVOICEID.Text.Trim(),"Y");
                                IstransferHOTag = "Y";
                            }

                            flag = clsPurchaseStockReceipt.ApproveVoucherJournal(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), IstransferHO);

                           
                             tag = clsPurchaseStockReceipt.GET_ISTRANSFERTOHO(lblSALEINVOICEID.Text);



                            flag = clsPurchaseStockReceipt.UpdateAccEntryIsTranferToHO(lblSALEINVOICEID.Text.Trim(), tag, IstransferHOTag);

                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        }

                        else if (module == "17")
                        {
                            VoucherBranchID = clsPurchaseStockReceipt.VoucherBranchID(lblSALEINVOICEID.Text.Trim());        /* Needed from bulk approval, All chhose from depot dropdown */

                            flag = clsPurchaseStockReceipt.ApproveBankReceipt(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), ChequeRealishedDate, Realisedno, VoucherBranchID, VoucherBranchName);
                            flag = clsVoucher.VoucherApproved(lblSALEINVOICEID.Text.Trim(), module);    // module id not change
                        }
                       
                        else if (module == "25")
                        {
                            CheckBox chkBxtoho = (CheckBox)gvrow.FindControl("" + gvchktranferHO + "");
                            CheckBox chkBxreverse = (CheckBox)gvrow.FindControl("" + gvchkReverse + "");                           

                            if (chkBxtoho.Checked)
                            {
                                IstransferHO = "Y";
                            }
                            else
                            {
                                IstransferHO = "N";
                            }

                            if (chkBxreverse.Checked)
                            {
                                Isreverse = "Y";
                            }
                            else
                            {
                                Isreverse = "N";
                            }

                            flag = clsPurchaseStockReceipt.ApproveTransporterBill(lblSALEINVOICEID.Text.Trim(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), lblSALEINVOICENO.Text.Trim(), lblSALEINVOICEDATE.Text.Trim(), IstransferHO, Isreverse);
                        }
                        if (flag > 0)
                        {
                            successcount += 1;
                        }
                        else if (flag == 0)
                        {
                            failcount += 1;
                        }
                        else
                        {
                            failcount += 1;
                        }
                    }
                }

                if (totalapproval > 0)
                {
                    if (successcount > 0)
                    {
                        MessageBox1.ShowSuccess("<b><font color='#666'>Requesting Approval : " + totalapproval + "</font><br><br> <font color='green'>Successful approval : " + successcount + "</font><br><br> <font color='maroon'>Failure approval : " + failcount + "</font></b>", 80, 350);

                        this.ResetGrid();

                        if (module == "1" || module == "10")
                        {
                            this.LoadInvoice();
                        }
                        else if (module == "28")                                //  Purchase received
                        {
                            this.LoadPurchase_FactoryReceived("", "T");         // T - FROM TPU RECEIVED                          
                        }
                        else if (module == "31")                                //  Purchase received
                        {
                            this.LoadPurchase_FactoryReceived("", "F");         // F - FROM FACTORY RECEIVED                          
                        }
                        else if (module == "26")
                        {
                            this.LoadSaleReturn("N");                           //  Invoice Sale Return
                        }
                        else if (module == "27")
                        {
                            this.LoadSaleReturn("Y");                           //   Gross Sale Return
                        }
                        else if (module == "25")
                        {
                            this.LoadTransporterBill();

                            //foreach (GridViewRow gvrow in gvUnlockInvoice.Rows)
                            //{
                            //    CheckBox chkBxreverse = (CheckBox)gvrow.FindControl("chkSelectReverse");
                            //    chkBxreverse.Checked = true;
                            //}
                        }
                        else if (this.ddlModule.SelectedValue == "29")          //Stock Transfer
                        {
                            this.LoadStockTransfer();
                        }
                        else if (this.ddlModule.SelectedValue == "30")          //Transfer Stock Received
                        {
                            this.LoadTransferStockReceived();
                        }
                        else if (module == "15" || module == "16" || module == "2" || module == "13" || module == "14" || module == "17")
                        {
                            this.LoadVoucher(module);
                        }
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='#666'>Requesting Approval : " + totalapproval + "</font><br><br> <font color='green'>Successful approval : " + successcount + "</font><br><br> <font color='maroon'>Failure approval : " + failcount + "</font></b><br/><br/><b><font color='maroon'>Something wrong with approval.....! </font><br><br>Please try again or inform to administrator</b>", 120, 450);
                        return;
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='green'>Sorry, please select atlease 1 invoice for approval</font></b>", 40, 450);
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='maroon'>Note :</font> <font color='gray'>You can approved maximum 300 invoices at a time.</font></b>", 40, 510);
                return;
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
            int totalcount = 0;
            string Realisedno = "x";
            string module = Convert.ToString(this.ddlModule.SelectedValue.Trim());
            GridView gvunlock = gvUnlockInvoice;
            string gvchkbox = "";
            string gvchktranferHO = "";
            string gvchkReverse = "";
            string gvinvoiceid = "";
            string gvinvoiceno = "";
            string gvinvoicedate = "";
            string gvbilltype = "";

            if (module == "1" || module == "25" || module == "26" || module == "27" || module == "28" || module == "29" || module == "30" || module == "31")
            /* Sale Invoice =1 && TransporterBill = 25 && Invoice Sale Return =26 && Gross Sale Return = 27 && Purchase = 28 && Stock Transfer = 29 && Transfer Stock Received = 30 && Factory Stock Received = 31 */
            {
                gvunlock = gvUnlockInvoice;
                gvchkbox = "chkSelect";
                gvchktranferHO = "chkSelectinv1";
                gvchkReverse = "chkSelectReverse";
                gvinvoiceid = "lblSALEINVOICEID";
                gvinvoiceno = "lblSALEINVOICENO";
                gvinvoicedate = "lblSALEINVOICEDATE";
                gvbilltype = "lblBILLTYPE";
            }
            else if (module == "15" || module == "16" || module == "2" || module == "13" || module == "14" || module == "17")
            {
                gvunlock = gvUnlockVoucher;
                gvchkbox = "chkSelectvou";
                gvchktranferHO = "chkSelectvou1";
                gvinvoiceid = "lblAccEntryID";
                gvinvoiceno = "lblVOUCHERNO";
                gvinvoicedate = "lblVOUCHERDate";
            }

            foreach (GridViewRow gvrow in gvunlock.Rows)
            {
                CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                if (chkBx.Checked)
                {
                    totalcount += 1;
                }
            }

            if (totalcount <= 300)
            {
                ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                int flag = 0;
                int totalapproval = 0;
                int successcount = 0;
                int failcount = 0;
                string SALEINVOICEID = string.Empty;

                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblSALEINVOICEID = (Label)gvrow.FindControl("" + gvinvoiceid + "");
                    Label lblSALEINVOICENO = (Label)gvrow.FindControl("" + gvinvoiceno + "");
                    Label lblSALEINVOICEDATE = (Label)gvrow.FindControl("" + gvinvoicedate + "");
                    if (chkBx.Checked)
                    {
                        totalapproval += 1;

                        if (module == "1")
                        {
                            flag = clsPurchaseStockReceipt.RejectSaleInvoice(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim(), "1");
                        }
                        else if (module == "28")
                        {
                            flag = clsPurchaseStockReceipt.RejectStockReceived(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        else if (module == "26" || module == "27")
                        {
                            flag = clsPurchaseStockReceipt.RejectSaleReturn(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        else if (this.ddlModule.SelectedValue == "29")                          //Stock Transfer
                        {
                            flag = clsPurchaseStockReceipt.RejectStockTransfer(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        else if (this.ddlModule.SelectedValue == "30")                          //Transfer Stock Received
                        {
                            flag = clsPurchaseStockReceipt.RejectDepotReceived(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        else if (this.ddlModule.SelectedValue == "31")                          //Factory Stock Received
                        {
                            flag = clsPurchaseStockReceipt.RejectStockReceived(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        else if ((module == "15") || (module == "16") || (module == "2") || (module == "13") || (module == "14") || (module == "17"))
                        {
                            flag = clsPurchaseStockReceipt.RejectAccounts(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim(), module);
                        }
                        else if (module == "25")
                        {
                            flag = clsPurchaseStockReceipt.RejectTransporterBill(lblSALEINVOICEID.Text.Trim(), this.txtRejectionNote.Text.Trim());
                        }
                        if (flag > 0)
                        {
                            successcount += 1;
                        }
                        else if (flag == 0)
                        {
                            failcount += 1;
                        }
                        else
                        {
                            failcount += 1;
                        }
                    }
                }

                if (totalapproval > 0)
                {
                    if (successcount > 0)
                    {
                        MessageBox1.ShowSuccess("<b><font color='#666'>Requesting Rejection : " + totalapproval + "</font><br><br> <font color='green'>Successful Rejection : " + successcount + "</font><br><br> <font color='maroon'>Failure Rejection : " + failcount + "</font></b>", 80, 350);

                        this.ResetGrid();

                        if (module == "1")
                        {
                            this.LoadInvoice();
                        }
                        else if (module == "28")                                //  Purchase received
                        {
                            this.LoadPurchase_FactoryReceived("", "T");         // T - FROM TPU RECEIVED                          
                        }
                        else if (module == "31")                                //  Purchase received
                        {
                            this.LoadPurchase_FactoryReceived("", "F");         // F - FROM FACTORY RECEIVED                          
                        }
                        else if (module == "26")
                        {
                            this.LoadSaleReturn("N");                           //  Invoice Sale Return
                        }
                        else if (module == "27")
                        {
                            this.LoadSaleReturn("Y");                           //   Gross Sale Return
                        }
                        else if (module == "25")
                        {
                            this.LoadTransporterBill();

                            //foreach (GridViewRow gvrow in gvUnlockInvoice.Rows)
                            //{
                            //    CheckBox chkBxreverse = (CheckBox)gvrow.FindControl("chkSelectReverse");
                            //    chkBxreverse.Checked = true;
                            //}
                        }
                        else if (this.ddlModule.SelectedValue == "29")          //Stock Transfer
                        {
                            this.LoadStockTransfer();
                        }
                        else if (this.ddlModule.SelectedValue == "30")          //Transfer Stock Received
                        {
                            this.LoadTransferStockReceived();
                        }
                        else if (module == "15" || module == "16" || module == "2" || module == "13" || module == "14" || module == "17")
                        {
                            this.LoadVoucher(module);
                        }
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='#666'>Requesting Rejection : " + totalapproval + "</font><br><br> <font color='green'>Successful Rejection : " + successcount + "</font><br><br> <font color='maroon'>Failure Rejection : " + failcount + "</font></b><br/><br/><b><font color='maroon'>Something wrong with approval.....! </font><br><br>Please try again or inform to administrator</b>", 120, 450);
                        return;
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='green'>Sorry, please select atlease 1 invoice for Rejection</font></b>", 40, 450);
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='maroon'>Note :</font> <font color='gray'>You can Rejection maximum 300 invoices at a time.</font></b>", 40, 510);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblid = gvr.Cells[1].Controls[0].FindControl("lblSALEINVOICEID") as Label;

            string upath = "";


            if (this.ddlModule.SelectedValue == "1") /* Sale invoice */
            {
                upath = "frmPrintPopUp.aspx?pid=" + grdlblid.Text + "&&BSID=" + this.ddlBusinessSegment.SelectedValue + "";
            }
            else if (this.ddlModule.SelectedValue == "28")  /* Purchase Recieved */
            {
                upath = "frmRptPurchasePrint.aspx?pid=" + grdlblid.Text + ""; /* TPU Recieved */
            }
            else if (this.ddlModule.SelectedValue == "29")   /*  Stock Transfer */
            {
                upath = "frmPrintPopUp.aspx?Stnid=" + grdlblid.Text + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=" + "46" + " ";
            }

            else if (this.ddlModule.SelectedValue == "30")  /*  Depot Stock Received  */
            {
                upath = "frmRptInvoicePrint.aspx?Stnid=" + grdlblid.Text + "&MenuId=" + "65" + "";

            }
            else if (this.ddlModule.SelectedValue == "25")  /*Transporter Bill */
            {
                upath = "frmRptInvoicePrint.aspx?Stnid=" + grdlblid.Text + "&&TAG=TB&&MenuId=45";

            }
            else if (this.ddlModule.SelectedValue == "26") /* Sale Return */
            {
                upath = "frmPrintPopUp.aspx?pid=" + grdlblid.Text + "&&BSID=" + this.ddlBusinessSegment.SelectedValue + "";
            }
            else if (this.ddlModule.SelectedValue == "27") /* Groos Return */
            {
                upath = "frmPrintPopUp.aspx?pid=" + grdlblid.Text + "&&BSID=" + this.ddlBusinessSegment.SelectedValue + "";
            }



            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnvoucherPrint_Click
    protected void btnvoucherPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblid = gvr.Cells[1].Controls[0].FindControl("lblAccEntryID") as Label;

            string upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + grdlblid.Text.Trim() + "&Voucherid=" + this.ddlModule.SelectedValue.Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    public void btnrejectvisibility()
    {
        try
        {
            string module = Convert.ToString(this.ddlModule.SelectedValue.Trim());
            if (module == "2" || module == "15" || module == "16" || module == "14" || module == "13" || module == "17")
            {
                this.divbtnreject.Style["display"] = "";
            }
            else
            {
                this.divbtnreject.Style["display"] = "none";
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlModule.SelectedValue == "1" || this.ddlModule.SelectedValue == "26" || this.ddlModule.SelectedValue == "27" || this.ddlModule.SelectedValue == "10") //  Sale invoice || Invoice Sale Return || Gross Sale Return || Trading Sale
        {
            this.tdBusinessSegment.Visible = true;
            this.tdgroup.Visible = true;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
            this.LoadBusinessSegment();
        }
        else if (this.ddlModule.SelectedValue == "25" || this.ddlModule.SelectedValue == "28" || this.ddlModule.SelectedValue == "29" || this.ddlModule.SelectedValue == "30" || this.ddlModule.SelectedValue == "31" || this.ddlModule.SelectedValue == "35")  //  Transporter Bill  || Purchase Received  || Stock Transfer  || Depot Stock Received  || Factory Stock Received || Purchase Return
        {
            this.tdBusinessSegment.Visible = false;
            this.tdgroup.Visible = false;
            this.trinvoice.Style["display"] = "";
            this.trvoucher.Style["display"] = "none";
        }
        else
        {
            this.tdBusinessSegment.Visible = false;
            this.tdgroup.Visible = false;
            this.trinvoice.Style["display"] = "none";
            this.trvoucher.Style["display"] = "";
        }

        this.ResetGrid();
        this.btnrejectvisibility();
    }

}