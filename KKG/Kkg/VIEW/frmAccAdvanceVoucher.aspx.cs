using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Account;
using WorkFlow;
using Utility;
using System.Drawing;
using BAL;

public partial class VIEW_frmAccAdvanceVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                /* This logic for Back Date Entry By D.Mondal On 27/11/2018 */
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                int Flag = clsVoucher.CheckDate();
                if (Flag > 0)
                {
                    this.imgPopuppodate.Enabled = true;
                    this.CalendarExtenderVoucherDate.Enabled = true;
                }
                else
                {
                    this.imgPopuppodate.Enabled = true;
                    this.CalendarExtenderVoucherDate.Enabled = true;
                }


                ViewState["OnlyBillTagFromPage"] = "N";
                ViewState["DrCrAmountEqual"] = "Y";
                this.btnSave.Text = "Save";

                btndivsave.Visible = true;
                InisiliseDate();/*only for ji agencey approved by sukhdev sir 28.05.2020 for backdate open*/


                //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      HPD       //

                //////// ClsStockReport clsstartfy = new ClsStockReport();
                //////// string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());

                //////// DateTime oDate = Convert.ToDateTime(STARTDATE);

                //////// ClsRptAccount clsVoucherEntry = new ClsRptAccount();
                //////// string EndDate = clsVoucherEntry.GetEndDateOfFinYear(Session["FINYEAR"].ToString());
                //////// ViewState["EndDate"] = EndDate;  /* 02/04/2019 */
                ////////DateTime cDate = Convert.ToDateTime(EndDate);

                //////// CalendarExtender3.StartDate = oDate;
                //////// CalendarExtender3.EndDate = cDate;

                //////// CalendarExtenderSrcToDate.StartDate = oDate;
                //////// CalendarExtenderSrcToDate.EndDate = cDate;
                //////// CalendarExtenderVoucherDate.StartDate = oDate;
                //////// CalendarExtenderVoucherDate.EndDate = cDate;

                //            ******  END LOCK CAELENDER   ***********      HPD    //
                if (Request.QueryString["TYPE"] != "LGRREPORT")
                {
                    if (this.Request.QueryString["CHECKER"].ToString().Trim() == "FALSE")
                    {
                        this.divbtnapprove.Style["display"] = "none";
                        this.divbtnreject.Style["display"] = "none";
                        this.btndivsave.Style["display"] = "";
                        this.divsavewithprint.Style["display"] = "";
                        this.btnaddhide.Style["display"] = "";
                    }
                    else
                    {
                        this.divbtnapprove.Style["display"] = "";
                        this.divbtnreject.Style["display"] = "";
                        this.btndivsave.Style["display"] = "none";
                        this.divsavewithprint.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "none";
                        this.divbtnpaidamount.Style["display"] = "none";

                        // Btn add paid amount
                    }

                    this.HOLedgerID();      /* Store HOID to ViewState */
                    this.pnlDisplay.Style["display"] = "";
                    this.pnlAdd.Style["display"] = "none";

                    /* Get Last date for non cuurent financial year : on 27/03/2019 */

                    /* 02/04/2019 */
                    //////DateTime dtcurr = DateTime.Now;
                    //////if (Session["FINYEAR"].ToString() == "2018-2019")
                    //////{
                    //////    dtcurr = Convert.ToDateTime(ViewState["EndDate"].ToString());
                    //////}
                    //////else
                    //////{
                    //////    this.CalendarExtenderSrcToDate.EndDate = DateTime.Now;
                    //////    CalendarExtender3.EndDate = DateTime.Now;
                    //////    CalendarExtenderVoucherDate.EndDate = DateTime.Now;
                    //////}
                    /* end 02/04/2019 */

                    //string LastDate = clsVoucherEntry.GetStartDateOfFinYear(Session["FINYEAR"].ToString());
                    //DateTime dtcurr = DateTime.Now;
                    //if ((LastDate) != "")
                    //{
                    //    dtcurr = Convert.ToDateTime(LastDate);
                    //}
                    //else
                    //{
                    //    this.CalendarExtenderSrcToDate.EndDate = DateTime.Now;
                    //    CalendarExtender3.EndDate = DateTime.Now;
                    //    CalendarExtenderVoucherDate.EndDate = DateTime.Now;
                    //}


                    /* End */



                    string date = "dd/MM/yyyy";
                    this.txtvoucherdate.Style.Add("color", "#000 !important");
                    this.ddlvouchertype.Style.Add("color", "#000 !important");
                    this.ddlregion.Style.Add("color", "#000 !important");
                    this.ddlMode.Style.Add("color", "#000 !important");
                    this.txtcredittotalamount.Style.Add("color", "#000 !important");
                    this.txtdebittotalamount.Style.Add("color", "#000 !important");
                    //////////this.txtsearchfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    //////////this.txtsearchtodate.Text = dtcurr.ToString(date).Replace('-', '/');
                    //////////this.txtvoucherdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    //////////this.txtdebitchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                    //////////this.txtcreditchequedate.Text = dtcurr.ToString(date).Replace('-', '/');

                    /*Calender Control Date Range*/
                    //this.CalendarExtenderVoucherDate.EndDate = DateTime.Now;
                    //this.CalendarExtenderDrChqDate.EndDate = DateTime.Now;
                    //this.CalendarExtenderCrChqDate.EndDate = DateTime.Now;
                    //this.CalendarExtenderSrcToDate.EndDate = DateTime.Now;


                    //////////this.CalendarExtenderCCToDate.EndDate = DateTime.Now;
                    //////////this.CalendarExtenderOthersDate.EndDate = DateTime.Now;
                    //////////this.CalendarExtenderGRDate.EndDate = DateTime.Now;
                    //////////this.CalendarExtenderWaybillDate.EndDate = DateTime.Now;
                    //////////this.CalendarExtenderRealisedDate.EndDate = DateTime.Now;


                    /*LOCK CALENDAR*/
                    ////////////DateTime oDate1 = Convert.ToDateTime(txtsearchfromdate.Text);
                    ////////////CalendarExtenderSrcToDate.StartDate = oDate1;

                    ////////////DateTime oDate2 = Convert.ToDateTime(txtsearchtodate.Text);
                    ////////////CalendarExtender3.EndDate = oDate2;
                    /////////////****************************/
                    ddlVoucher.Enabled = true;
                    this.gvparentvoucher.DataSource = null;
                    this.gvparentvoucher.DataBind();
                    /*this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(), this.ddldepot.SelectedValue);*/
                    this.BindCostCenter();
                    this.BindCostCenterCatagory();
                    this.BindBrand();
                    this.BindProduct();
                    this.BindDepartment();
                    //this.txtcostcenterfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                    //this.txtcostcentertodate.Text = dtcurr.ToString(date).Replace('-', '/');

                    this.BindRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["IUSERID"].ToString());
                    this.BindPaymentType();
                    ViewState["AccType"] = "Normal";    /* It is changed to auto when payment from HO to depot only */
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdcostcenter.ClientID + "', 200, '99%' , 30 ,false); </script>", false);
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>MakeStaticHeader_invoice('" + grd_debitcredit_invoice.ClientID + "', 200, '99%' , 30 ,false); </script>", false);

                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key2", "<script>MakeStaticHeader_InvoiceDetails('" + grd_InvoiceDetails.ClientID + "', 220, '99%' , 30 ,false); </script>", false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + Btnadd.ClientID + "').focus(); ", true);
                }
                else
                {
                    this.divbtnapprove.Style["display"] = "none";
                    this.divbtnreject.Style["display"] = "none";
                    this.btndivsave.Style["display"] = "none";
                    this.divsavewithprint.Style["display"] = "none";
                    this.btnaddhide.Style["display"] = "none";
                    this.divbtnpaidamount.Style["display"] = "none";
                    this.divbtnpaidamountclear.Style["display"] = "none";
                    this.divclose.Style["display"] = "none";
                    Btn_View(Request.QueryString["InvId"]);
                    this.BindRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["IUSERID"].ToString());

                    /* Change by HPDAS on 30.10.2018 */
                    this.gvdebit.Enabled = true;
                    this.gvcredit.Enabled = true;

                    foreach (GridViewRow row in gvdebit.Rows)
                    {
                        ((TextBox)row.FindControl("txtdramount")).ReadOnly = true;
                    }
                    foreach (GridViewRow row in gvcredit.Rows)
                    {
                        ((TextBox)row.FindControl("txtcramount")).ReadOnly = true;
                    }

                    foreach (GridViewRow row in grdcostcenter.Rows)
                    {
                        ((TextBox)row.FindControl("txtamount")).ReadOnly = false;
                    }

                    grdcostcenter.Columns[18].Visible = false;
                    this.btncostcentersave.Visible = false;
                    this.btncostcenteradd.Visible = false;

                    /* End Change */
                }
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key3", "<script>MakeStaticHeader_voucher('" + gvparentvoucher.ClientID + "', 420, '100%' , 30 ,false); </script>", false);
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
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
        CalendarExtender3.StartDate = oDate;
        CalendarExtenderSrcToDate.StartDate = oDate;
        /*back date*/// CalendarExtenderVoucherDate.StartDate = oDate;
        CalendarExtenderRealisedDate.StartDate = oDate;
        CalendarExtenderWaybillDate.StartDate = oDate;
        CalendarExtenderGRDate.StartDate = oDate;
        CalendarExtenderOthersDate.StartDate = oDate;
        CalendarExtenderCCToDate.StartDate = oDate;


        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtsearchfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtsearchtodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtRealisedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtwaybilldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtgrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtbilldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtcostcentertodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtcreditchequedate.Text = DateTime.Now.ToString("dd/MM/yyyy");


            CalendarExtender3.EndDate = today1;
            CalendarExtenderSrcToDate.EndDate = today1;
            CalendarExtenderVoucherDate.EndDate = today1;
            CalendarExtenderRealisedDate.EndDate = today1;
            CalendarExtenderWaybillDate.EndDate = today1;
            CalendarExtenderGRDate.EndDate = today1;
            CalendarExtenderOthersDate.EndDate = today1;
            CalendarExtenderCCToDate.EndDate = today1;

        }
        else
        {
            this.txtsearchfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtsearchtodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtvoucherdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtRealisedDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtwaybilldate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtgrdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtbilldate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtcostcentertodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtcreditchequedate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtender3.EndDate = cDate;
            CalendarExtenderSrcToDate.EndDate = cDate;
            CalendarExtenderVoucherDate.EndDate = cDate;
            CalendarExtenderRealisedDate.EndDate = cDate;
            CalendarExtenderWaybillDate.EndDate = cDate;
            CalendarExtenderGRDate.EndDate = cDate;
            CalendarExtenderOthersDate.EndDate = cDate;
            CalendarExtenderCCToDate.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }

    //protected void txtsearchfromdate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txtsearchfromdate.Text);
    //    CalendarExtenderSrcToDate.StartDate = oDate1;
    //}
    //protected void txtsearchtodate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime oDate1 = Convert.ToDateTime(txtsearchtodate.Text);
    //    CalendarExtender3.EndDate = oDate1;
    //}

    public void HOLedgerID()
    {
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        string HOID = clsVoucher.HO_LedgerID();
        ViewState["HOID"] = HOID.ToString();
    }

    public void NewAddMethod()
    {
        this.ClearGridWithSession();
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.innertr.Style["display"] = "none";
        this.trpaymentmode.Style["display"] = "none";
        this.trvoucherno.Style["display"] = "none";
        this.trdebitbank.Style["display"] = "none";
        this.trdebitchequeno.Style["display"] = "none";
        this.trcreditbank.Style["display"] = "none";
        this.trcreditchequeno.Style["display"] = "none";
        this.innertr.Style["display"] = "";
        this.trinvoicedetails.Style["display"] = "none";
        this.trcreditbtn.Style["display"] = "";
        this.trdebitbtn.Style["display"] = "";
        this.CreateVoucherTable();
        this.Create_DebitCreditInvoiceTable();
        this.ddlvouchertype.SelectedValue = Request.QueryString["VOUCHERID"].ToString().Trim();
        this.btnaddhide.Style["display"] = "none";
        this.divbtnpaidamount.Style["display"] = "";

        /* 02/04/2019 Start*/
        //this.txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
        //string date = "dd/MM/yyyy";
        //DateTime dtcurr = DateTime.Now;
        //if (Session["FINYEAR"].ToString() == "2018-2019")
        //{
        //    dtcurr = Convert.ToDateTime(ViewState["EndDate"].ToString());
        //    this.txtvoucherdate.Text = dtcurr.ToString(date).Replace('-', '/');
        //}
        //else
        //{
        //    this.txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
        //}
        /* 02/04/2019 End*/

        InisiliseDate();

    }

    public void NewVoucherBind()
    {
        try
        {
            this.BindVoucherType();
            this.VoucherTypeIndexChanged();
            if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16")
            {
                // Nothing for Payment/Receipt/Advance Payment/Advance Receipt
            }
            else
            {
                this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
            }
            if (this.ddlvouchertype.SelectedValue == "2")
            {
                this.divbtnotherdetails.Style["display"] = "";
            }
            else
            {
                this.divbtnotherdetails.Style["display"] = "none";
            }
            this.gvdebit.Columns[13].Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearData();
            this.NewAddMethod();
            this.BindState();
            this.BindParty();
            this.NewVoucherBind();
            ViewState["AccType"] = "Normal";
            btngstonly.Visible = true;
            tdDropdown.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlregion.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void BindPaymentType()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = clsVoucher.PaymentType();

            this.rdbcreditpaymenttype.Items.Clear();
            this.rdbcreditpaymenttype.DataSource = dt;
            this.rdbcreditpaymenttype.DataTextField = "PAYMENTTYPENAME";
            this.rdbcreditpaymenttype.DataValueField = "PAYMENTTYPEID";
            this.rdbcreditpaymenttype.DataBind();

            this.rdbcreditpaymenttype.SelectedValue = "2";

            this.rdbdebitpaymenttype.Items.Clear();
            this.rdbdebitpaymenttype.DataSource = dt;
            this.rdbdebitpaymenttype.DataTextField = "PAYMENTTYPENAME";
            this.rdbdebitpaymenttype.DataValueField = "PAYMENTTYPEID";
            this.rdbdebitpaymenttype.DataBind();

            this.rdbdebitpaymenttype.SelectedValue = "2";
        }
        catch (Exception ex)
        {
            CreateLogFiles Errlog = new CreateLogFiles();
            Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :PaymentType Error");
        }
    }

    protected void BindVoucherDetails(string FromDate, string ToDate, string UserID, string FinYear, string VoucherID, string DepotID, string Advancevouchertype) //Last parameter add by Romaprosad Dropdownvouchertype 16092019

    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.gvparentvoucher.DataSource = clsVoucher.VoucherHeaderDetails(FromDate, ToDate, UserID, FinYear, VoucherID, this.Request.QueryString["CHECKER"].ToString().Trim(), DepotID, "N", this.Dropdownadvancevouchertype.SelectedValue.Trim());
            this.gvparentvoucher.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindRegion(string UserType, string UserID)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = new DataTable();
            dt = clsVoucher.Region(UserType, UserID);
            //this.ddlregion.Items.Add(new ListItem("Select Region", "0"));
            //this.ddlregion.AppendDataBoundItems = true;
            this.ddlregion.DataSource = dt;
            this.ddlregion.DataTextField = "BRNAME";
            this.ddlregion.DataValueField = "BRID";
            this.ddlregion.DataBind();

            this.ddlbranch.Items.Clear();
            this.ddlbranch.Items.Add(new ListItem("Select Branch", "0"));
            this.ddlbranch.AppendDataBoundItems = true;
            this.ddlbranch.DataSource = dt;
            this.ddlbranch.DataTextField = "BRNAME";
            this.ddlbranch.DataValueField = "BRID";
            this.ddlbranch.DataBind();

            this.ddldepot.Items.Clear();
            this.ddldepot.DataSource = dt;
            this.ddldepot.DataTextField = "BRNAME";
            this.ddldepot.DataValueField = "BRID";
            this.ddldepot.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindCostCenterCatagory()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlcatagory.Items.Clear();
            this.ddlcatagory.Items.Add(new ListItem("NA", "0"));
            this.ddlcatagory.AppendDataBoundItems = true;
            this.ddlcatagory.DataSource = clsVoucher.BussinessSegment();
            this.ddlcatagory.DataTextField = "COSTCATNAME";
            this.ddlcatagory.DataValueField = "COSTCATID";
            this.ddlcatagory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindBrand()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlbrand.Items.Clear();
            this.ddlbrand.Items.Add(new ListItem("NA", "0"));
            this.ddlbrand.AppendDataBoundItems = true;
            this.ddlbrand.DataSource = clsVoucher.Brand();
            this.ddlbrand.DataTextField = "BRANDNAME";
            this.ddlbrand.DataValueField = "BRANDID";
            this.ddlbrand.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void BindProduct()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlproduct.Items.Clear();
            this.ddlproduct.Items.Add(new ListItem("NA", "0"));
            this.ddlproduct.AppendDataBoundItems = true;
            this.ddlproduct.DataSource = clsVoucher.ProductMaster();
            this.ddlproduct.DataTextField = "PRODUCTNAME";
            this.ddlproduct.DataValueField = "PRODUCTID";
            this.ddlproduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindDepartment()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddldepartment.Items.Clear();
            this.ddldepartment.Items.Add(new ListItem("Select Department", "0"));
            this.ddldepartment.AppendDataBoundItems = true;
            this.ddldepartment.DataSource = clsVoucher.Department();
            this.ddldepartment.DataTextField = "DEPTNAME";
            this.ddldepartment.DataValueField = "DEPTID";
            this.ddldepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindCostCenter()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlcentername.Items.Clear();
            this.ddlcentername.Items.Add(new ListItem("NA", "0"));
            this.ddlcentername.AppendDataBoundItems = true;
            this.ddlcentername.DataSource = clsVoucher.BindCostCenter();
            this.ddlcentername.DataTextField = "COSTCENTRENAME";
            this.ddlcentername.DataValueField = "COSTCENTREID";
            this.ddlcentername.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindVoucherType()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlvouchertype.Items.Clear();
            //this.ddlvouchertype.Items.Add(new ListItem("Select Voucher Type", "0"));
            //this.ddlvouchertype.AppendDataBoundItems = true;
            this.ddlvouchertype.DataSource = clsVoucher.VoucherType();
            this.ddlvouchertype.DataTextField = "VoucherName";
            this.ddlvouchertype.DataValueField = "Id";
            this.ddlvouchertype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindAccountType(string VoucherType, string RegionID)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

            this.ddlAccTypeDr.Items.Clear();
            this.ddlAccTypeDr.Items.Add(new ListItem("Select Account", "0"));
            this.ddlAccTypeDr.AppendDataBoundItems = true;
            this.ddlAccTypeDr.DataSource = clsVoucher.VoucherType_Account(VoucherType, "D", RegionID);
            this.ddlAccTypeDr.DataTextField = "name";
            this.ddlAccTypeDr.DataValueField = "Id";
            this.ddlAccTypeDr.DataBind();

            this.ddlAcctypeCr.Items.Clear();
            this.ddlAcctypeCr.Items.Add(new ListItem("Select Account", "0"));
            this.ddlAcctypeCr.AppendDataBoundItems = true;
            this.ddlAcctypeCr.DataSource = clsVoucher.VoucherType_Account(VoucherType, "C", RegionID);
            this.ddlAcctypeCr.DataTextField = "name";
            this.ddlAcctypeCr.DataValueField = "Id";
            this.ddlAcctypeCr.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Bind_Receipt_Cash_Account(string VoucherTypeID, String PaymentMode)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

            if (VoucherTypeID == "10" || VoucherTypeID == "16")
            {
                this.ddlAccTypeDr.Items.Clear();
                this.ddlAccTypeDr.Items.Add(new ListItem("Select Account", "0"));
                this.ddlAccTypeDr.AppendDataBoundItems = true;
                this.ddlAccTypeDr.DataSource = clsVoucher.Receipt_Type_Account(VoucherTypeID, PaymentMode);
                this.ddlAccTypeDr.DataTextField = "name";
                this.ddlAccTypeDr.DataValueField = "Id";
                this.ddlAccTypeDr.DataBind();
            }
            if (VoucherTypeID == "9" || VoucherTypeID == "15")
            {
                this.ddlAcctypeCr.Items.Clear();
                this.ddlAcctypeCr.Items.Add(new ListItem("Select Account", "0"));
                this.ddlAcctypeCr.AppendDataBoundItems = true;
                this.ddlAcctypeCr.DataSource = clsVoucher.Receipt_Type_Account(VoucherTypeID, PaymentMode);
                this.ddlAcctypeCr.DataTextField = "name";
                this.ddlAcctypeCr.DataValueField = "Id";
                this.ddlAcctypeCr.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Bind_Receipt_Bank_Account(string VoucherTypeID, String PaymentMode)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

            if (VoucherTypeID == "10" || VoucherTypeID == "16")
            {
                this.ddlAccTypeDr.Items.Clear();
                this.ddlAccTypeDr.Items.Add(new ListItem("Select Account", "0"));
                this.ddlAccTypeDr.AppendDataBoundItems = true;
                this.ddlAccTypeDr.DataSource = clsVoucher.Receipt_Type_Account(VoucherTypeID, PaymentMode);
                this.ddlAccTypeDr.DataTextField = "name";
                this.ddlAccTypeDr.DataValueField = "Id";
                this.ddlAccTypeDr.DataBind();
            }
            if (VoucherTypeID == "9" || VoucherTypeID == "15")
            {
                this.ddlAcctypeCr.Items.Clear();
                this.ddlAcctypeCr.Items.Add(new ListItem("Select Account", "0"));
                this.ddlAcctypeCr.AppendDataBoundItems = true;
                this.ddlAcctypeCr.DataSource = clsVoucher.Receipt_Type_Account(VoucherTypeID, PaymentMode);
                this.ddlAcctypeCr.DataTextField = "name";
                this.ddlAcctypeCr.DataValueField = "Id";
                this.ddlAcctypeCr.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BankNameForReceipt()
    {
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        this.ddldebitbankname.Items.Clear();
        this.ddldebitbankname.Items.Add(new ListItem("Select Bank Name", "0"));
        this.ddldebitbankname.AppendDataBoundItems = true;
        this.ddldebitbankname.DataSource = clsVoucher.BankNameForReceipt();
        this.ddldebitbankname.DataTextField = "BANKNAME";
        this.ddldebitbankname.DataValueField = "ID";
        this.ddldebitbankname.DataBind();

        this.ddlcreditbankname.Items.Clear();
        this.ddlcreditbankname.Items.Add(new ListItem("Select Bank Name", "0"));
        this.ddlcreditbankname.AppendDataBoundItems = true;
        this.ddlcreditbankname.DataSource = clsVoucher.BankNameForReceipt();
        this.ddlcreditbankname.DataTextField = "BANKNAME";
        this.ddlcreditbankname.DataValueField = "ID";
        this.ddlcreditbankname.DataBind();
    }

    #region Create DataTable Structure
    public DataTable CreateVoucherTable()
    {
        DataTable dtvoucher = new DataTable();
        dtvoucher.Clear();
        dtvoucher.Columns.Add("GUID");
        dtvoucher.Columns.Add("LedgerId");
        dtvoucher.Columns.Add("LedgerName");
        dtvoucher.Columns.Add("TxnType");
        dtvoucher.Columns.Add("Amount", typeof(decimal));
        dtvoucher.Columns.Add("BankID");
        dtvoucher.Columns.Add("BankName");
        dtvoucher.Columns.Add("PAYMENTTYPEID");
        dtvoucher.Columns.Add("PAYMENTTYPENAME");
        dtvoucher.Columns.Add("ChequeNo");
        dtvoucher.Columns.Add("ChequeDate");
        dtvoucher.Columns.Add("IsChequeRealised");
        dtvoucher.Columns.Add("Remarks");
        dtvoucher.Columns.Add("ChequeRealisedNo");
        dtvoucher.Columns.Add("ChequeRealisedDate");
        dtvoucher.Columns.Add("DeductableAmount");
        dtvoucher.Columns.Add("DeductablePercentage");
        dtvoucher.Columns.Add("DeductableLedgerId");
        dtvoucher.Columns.Add("IsCostCenter");
        dtvoucher.Columns.Add("IsTagInvoice");

        HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;
        return dtvoucher;
    }
    #endregion

    #region Create Invoice DataTable Structure
    public DataTable CreateInvoiceTable()
    {
        DataTable dtinvoice = new DataTable();
        dtinvoice.Clear();
        dtinvoice.Columns.Add("GUID");
        dtinvoice.Columns.Add("LedgerId");
        dtinvoice.Columns.Add("LedgerName");
        dtinvoice.Columns.Add("InvoiceID");
        dtinvoice.Columns.Add("InvoiceNo");
        dtinvoice.Columns.Add("InvoiceDate");
        dtinvoice.Columns.Add("InvoiceOthers");
        dtinvoice.Columns.Add("InvoiceBranchID");
        dtinvoice.Columns.Add("InvoiceBranchName");
        dtinvoice.Columns.Add("VoucherType");
        dtinvoice.Columns.Add("BranchID");
        dtinvoice.Columns.Add("InvoiceAmt", typeof(decimal));
        dtinvoice.Columns.Add("AlreadyAmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("ReturnAmt", typeof(decimal));        /* Add new column ReturnAmt by D.Mondal on 11/10/2018 */
        dtinvoice.Columns.Add("RemainingAmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("AmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("Type");

        HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoice;
        return dtinvoice;
    }
    #endregion


    #region Create Invoice DataTable Structure
    public DataTable CreateInvoiceTableForDuplicate()
    {
        DataTable dtinvoice = new DataTable();
        dtinvoice.Clear();
        dtinvoice.Columns.Add("GUID");
        dtinvoice.Columns.Add("LedgerId");
        dtinvoice.Columns.Add("LedgerName");
        dtinvoice.Columns.Add("InvoiceID");
        dtinvoice.Columns.Add("InvoiceNo");
        dtinvoice.Columns.Add("InvoiceDate");
        dtinvoice.Columns.Add("InvoiceOthers");
        dtinvoice.Columns.Add("InvoiceBranchID");
        dtinvoice.Columns.Add("InvoiceBranchName");
        dtinvoice.Columns.Add("VoucherType");
        dtinvoice.Columns.Add("BranchID");
        dtinvoice.Columns.Add("InvoiceAmt", typeof(decimal));
        dtinvoice.Columns.Add("AlreadyAmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("ReturnAmt", typeof(decimal));        /* Add new column ReturnAmt by D.Mondal on 11/10/2018 */
        dtinvoice.Columns.Add("RemainingAmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("AmtPaid", typeof(decimal));
        dtinvoice.Columns.Add("Type");

        HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoice;
        return dtinvoice;
    }
    #endregion


    #region Create Invoice Debit-Credit Not DataTable Structure
    public DataTable Create_DebitCreditInvoiceTable()
    {
        DataTable dtinvoice = new DataTable();
        dtinvoice.Clear();
        dtinvoice.Columns.Add("GUID");
        dtinvoice.Columns.Add("LedgerId");
        dtinvoice.Columns.Add("LedgerName");
        dtinvoice.Columns.Add("InvoiceID");
        dtinvoice.Columns.Add("InvoiceNo");
        dtinvoice.Columns.Add("InvoiceDate");
        dtinvoice.Columns.Add("InvoiceAmt", typeof(decimal));
        dtinvoice.Columns.Add("TAG");

        HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"] = dtinvoice;
        return dtinvoice;
    }
    #endregion

    #region CREAT_CREDITLEDGERID
    public DataTable CREAT_CREDITLEDGERID() /// CREATE THIS FOR TABLE FOR CREDIT SIDE LEDGER ID  ADDED ON 11072019///
    {
        DataTable dtcredit = new DataTable();
        dtcredit.Clear();
        dtcredit.Columns.Add("CREDITORSID");
        HttpContext.Current.Session["CREDIT_LEDGERID"] = dtcredit;
        return dtcredit;
    }
    #endregion 

    #region CREAT_DEBITLEDGERID
    public DataTable CREAT_DEBITLEDGERID() ///* CREATE THIS FOR TABLE FOR DEBIT SIDE LEDGER ID ADDED ON 11072019 * ///
    {
        DataTable dtdebit = new DataTable();
        dtdebit.Clear();
        dtdebit.Columns.Add("DEBITORSID");
        HttpContext.Current.Session["DEBIT_LEDGERID"] = dtdebit;
        return dtdebit;
    }
    #endregion 

    #region Create CostCenter DataTable Structure
    public DataTable CreateCostCenterTable()
    {
        DataTable dtcostcenter = new DataTable();
        dtcostcenter.Clear();
        dtcostcenter.Columns.Add("GUID");
        dtcostcenter.Columns.Add("LedgerId");
        dtcostcenter.Columns.Add("LedgerName");
        dtcostcenter.Columns.Add("CostCatagoryID");
        dtcostcenter.Columns.Add("CostCatagoryName");
        dtcostcenter.Columns.Add("CostCenterID");
        dtcostcenter.Columns.Add("CostCenterName");
        dtcostcenter.Columns.Add("BranchID");
        dtcostcenter.Columns.Add("BranchName");
        dtcostcenter.Columns.Add("amount", typeof(decimal));
        dtcostcenter.Columns.Add("BrandID");
        dtcostcenter.Columns.Add("BrandName");
        dtcostcenter.Columns.Add("ProductID");
        dtcostcenter.Columns.Add("ProductName");
        dtcostcenter.Columns.Add("DepartmentID");
        dtcostcenter.Columns.Add("DepartmentName");
        dtcostcenter.Columns.Add("FromDate");
        dtcostcenter.Columns.Add("ToDate");
        dtcostcenter.Columns.Add("Narration");
        dtcostcenter.Columns.Add("TxnType");

        HttpContext.Current.Session["COSTCENTERDETAILS"] = dtcostcenter;
        return dtcostcenter;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.ddlvouchertype.SelectedValue.Trim() == "13" || this.ddlvouchertype.SelectedValue.Trim() == "14")
            {
                if (ViewState["DrCrAmountEqual"].ToString() == "Y")
                {
                    btn_Save("Save");
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Adjusted amount can not be greater than transaction amount .</b>", 40, 500);
                }
            }
            else
            {
                btn_Save("Save");
            }
            ViewState["OnlyBillTagFromPage"] = "N";
            ViewState["DrCrAmountEqual"] = "Y";
            this.btnSave.Text = "Save";
            btndivsave.Visible = true;

        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 40, 500);
        }

    }
    protected void btnSavewithPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlvouchertype.SelectedValue.Trim() == "13" || this.ddlvouchertype.SelectedValue.Trim() == "14")
            {
                if (ViewState["DrCrAmountEqual"].ToString() == "Y")
                {
                    btn_Save("Savewithprint");
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Adjusted amount can not be greater than transaction amount .</b>", 40, 500);
                }
            }
            else
            {
                btn_Save("Savewithprint");
            }
            ViewState["OnlyBillTagFromPage"] = "N";
            ViewState["DrCrAmountEqual"] = "Y";
            this.btnSave.Text = "Save";
            btndivsave.Visible = true;



        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 40, 500);
        }

    }

    public void btn_Save(string _Savewithprint)
    {
        try
        {

            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtvoucherdate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                decimal debitamount = 0;
                decimal creditamount = 0;
                decimal amount = 0;
                decimal costamount = 0;

                if ((Convert.ToDecimal(this.txtdebittotalamount.Text.Trim()) == Convert.ToDecimal(this.txtcredittotalamount.Text.Trim())) && Convert.ToDecimal(this.txtdebittotalamount.Text.Trim()) != 0 && Convert.ToDecimal(this.txtcredittotalamount.Text.Trim()) != 0)
                {
                    ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                    DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
                    DataTable dtinvoice = new DataTable();
                    string xmlcostcenter = null;
                    string xml = null;
                    string xmlDebitCredit = null;
                    string xmlCreditors = null;
                    string xmlDebitors = null;
                    DataTable dtcostcenter = new DataTable();
                    string crchqno = "";
                    string drchqno = "";

                    if (HttpContext.Current.Session["COSTCENTERDETAILS"] != null)
                    {
                        dtcostcenter = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];
                        if (dtcostcenter.Rows.Count > 0)
                        {
                            xmlcostcenter = ConvertDatatableToXML(dtcostcenter);
                        }
                    }

                    if (HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"] != null)
                    {
                        DataTable dtdebitcredit_invoice = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];
                        if (dtdebitcredit_invoice.Rows.Count > 0)
                        {
                            xmlDebitCredit = ConvertDatatableToXML(dtdebitcredit_invoice);
                        }
                    }
                    if (HttpContext.Current.Session["DEBIT_LEDGERID"] == null)
                    {
                        CREAT_DEBITLEDGERID();
                    }
                    DataTable dtdebit = (DataTable)HttpContext.Current.Session["DEBIT_LEDGERID"];  /* this session use for debitors id  by p.basu*/

                    foreach (GridViewRow gvrow in gvdebit.Rows)
                    {
                        Label lblledgerid = (Label)gvrow.FindControl("lbldrLedgerId");
                        DataRow dr = dtdebit.NewRow();
                        dr["DEBITORSID"] = lblledgerid.Text;
                        Label lbldrledgerName = (Label)gvrow.FindControl("lbldrledgerName");
                        TextBox txtamount = (TextBox)gvrow.FindControl("txtdramount");
                        amount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                        drchqno = gvdebit.Rows[gvrow.RowIndex].Cells[7].Text.Trim().Replace("&nbsp;", "");

                        foreach (DataRow row in dtvoucher.Rows)
                        {
                            if (Convert.ToString(row["LedgerId"]).Trim() == lblledgerid.Text.Trim() && Convert.ToString(row["ChequeNo"]).Trim() == drchqno && Convert.ToString(row["TxnType"]).Trim() == "0")
                            {
                                row["Amount"] = Convert.ToString(amount);
                                debitamount = debitamount + Convert.ToDecimal(amount);
                                costamount = CalculateTotalAmountCostCenter(dtcostcenter, lblledgerid.Text.Trim());

                                if (costamount != amount && costamount != 0)
                                {
                                    MessageBox1.ShowInfo("<b>Please check debit ledger <font color='green'>" + lbldrledgerName.Text + "</font> cost center amount which not same with ledger amount Rs: " + amount.ToString() + "/-</b>", 50, 560);
                                    return;
                                }
                            }
                        }
                        dtdebit.Rows.Add(dr);
                        dtdebit.AcceptChanges();
                    }
                    xmlDebitors = ConvertDatatableToXML(dtdebit);

                    if (HttpContext.Current.Session["CREDIT_LEDGERID"] == null)
                    {
                        CREAT_CREDITLEDGERID();
                    }
                    DataTable dtcredit = (DataTable)HttpContext.Current.Session["CREDIT_LEDGERID"];  /* this session use for creditors id  by p.basu*/

                    foreach (GridViewRow gvrow in gvcredit.Rows)
                    {
                        Label lblledgerid = (Label)gvrow.FindControl("lblcrLedgerId");
                        DataRow dr = dtcredit.NewRow();
                        dr["CREDITORSID"] = lblledgerid.Text;  /* dtdebit datatable is used for capture ledgerid into this datatable by p.basu */
                        Label lblcrledgerName = (Label)gvrow.FindControl("lblcrledgerName");
                        TextBox txtamount = (TextBox)gvrow.FindControl("txtcramount");
                        amount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                        crchqno = gvcredit.Rows[gvrow.RowIndex].Cells[7].Text.Trim().Replace("&nbsp;", "");

                        foreach (DataRow row in dtvoucher.Rows)
                        {
                            if (Convert.ToString(row["LedgerId"]).Trim() == lblledgerid.Text.Trim() && Convert.ToString(row["ChequeNo"]).Trim() == crchqno && Convert.ToString(row["TxnType"]).Trim() == "1")
                            {
                                row["Amount"] = Convert.ToString(amount);
                                creditamount = creditamount + Convert.ToDecimal(amount);
                                costamount = CalculateTotalAmountCostCenter(dtcostcenter, lblledgerid.Text.Trim());

                                if (costamount != amount && costamount != 0)
                                {
                                    MessageBox1.ShowInfo("<b>Please check credit ledger <font color='green'>" + lblcrledgerName.Text + "</font> cost center amount which not same with ledger amount Rs: " + amount.ToString() + "/-</b>", 50, 560);
                                    return;
                                }
                            }
                        }
                        dtcredit.Rows.Add(dr);
                        dtcredit.AcceptChanges();
                    }
                    xmlCreditors = ConvertDatatableToXML(dtcredit);  /* xmlDebitors is used for capture dtcredit datatable ledger id value by p.basu*/


                    dtvoucher.AcceptChanges();
                    HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;

                    if (debitamount != creditamount)
                    {
                        MessageBox1.ShowInfo("<b><font color='black'>Please check total debit and credit amount or please refresh this page and try again</font></b>", 40, 600);
                        return;
                    }

                    if (Session["INVOICEDETAILS"] == null)
                    {
                        this.CreateInvoiceTable();
                    }

                    dtinvoice = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];


                    if (dtvoucher.Rows.Count > 0)
                    {
                        if (this.ddlvouchertype.SelectedValue.Trim() == "9" || this.ddlvouchertype.SelectedValue.Trim() == "15")
                        {
                            if (dtinvoice.Rows.Count > 0)
                            {
                                for (int count = 0; count < dtvoucher.Rows.Count; count++)
                                {
                                    if (Convert.ToString(dtvoucher.Rows[count]["TxnType"]).Trim() == "0")
                                    {
                                        string ledgerid = Convert.ToString(dtvoucher.Rows[count]["LedgerId"]).Trim();
                                        string ledgername = Convert.ToString(dtvoucher.Rows[count]["LedgerName"]).Trim();
                                        dtvoucher.DefaultView.RowFilter = "TxnType = '0'";
                                        decimal sumdebitbasedonledger = (decimal)dtvoucher.Compute("Sum(Amount)", "LedgerId = '" + ledgerid + "'");
                                        int NumberofRecord = dtinvoice.Select("VoucherType='9' AND LedgerId='" + ledgerid + "'").Length;
                                        if (NumberofRecord > 0)
                                        {
                                            decimal paidamountbasedonledger = (decimal)dtinvoice.Compute("Sum(AmtPaid)", "LedgerId = '" + ledgerid + "'");

                                            if (sumdebitbasedonledger < paidamountbasedonledger)
                                            {
                                                MessageBox1.ShowInfo("<b>Adjusted amount can not be greater than paid amount for <font color='red'>" + ledgername + "</font>!</b>", 40, 500);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (this.ddlvouchertype.SelectedValue.Trim() == "10" || this.ddlvouchertype.SelectedValue.Trim() == "16")
                        {
                            if (dtinvoice.Rows.Count > 0)
                            {
                                for (int count = 0; count < dtvoucher.Rows.Count; count++)
                                {
                                    if (Convert.ToString(dtvoucher.Rows[count]["TxnType"]).Trim() == "1")
                                    {
                                        string ledgerid = Convert.ToString(dtvoucher.Rows[count]["LedgerId"]).Trim();
                                        string ledgername = Convert.ToString(dtvoucher.Rows[count]["LedgerName"]).Trim();
                                        dtvoucher.DefaultView.RowFilter = "TxnType = '1'";
                                        decimal sumcreditbasedonledger = (decimal)dtvoucher.Compute("Sum(Amount)", "LedgerId = '" + ledgerid + "'");
                                        int NumberofRecord = dtinvoice.Select("VoucherType='10' AND LedgerId='" + ledgerid + "'").Length;
                                        if (NumberofRecord > 0)
                                        {
                                            decimal paidamountbasedonledger = (decimal)dtinvoice.Compute("Sum(AmtPaid)", "LedgerId = '" + ledgerid + "'");

                                            if (sumcreditbasedonledger < paidamountbasedonledger)
                                            {
                                                MessageBox1.ShowInfo("<b>Adjusted amount can not be greater than receipt amount for <font color='red'>" + ledgername + "</font>!</b>", 40, 500);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        string VoucherNo = string.Empty;
                        string mode = string.Empty;
                        string PayementMode = string.Empty;

                        xml = ConvertDatatableToXML(dtvoucher);

                        if (hdn_accid.Value.ToString() == "")
                        {
                            mode = "A";
                        }
                        else
                        {
                            mode = "U";
                        }
                        if (ddlvouchertype.SelectedValue.Trim() == "9" || ddlvouchertype.SelectedValue.Trim() == "10" || ddlvouchertype.SelectedValue.Trim() == "15" || ddlvouchertype.SelectedValue.Trim() == "16")
                        {
                            PayementMode = this.ddlMode.SelectedValue.Trim();
                        }
                        else
                        {
                            PayementMode = "";
                        }
                        if (this.ddlVoucher.SelectedValue == "1")  /* 1 for normal */
                        {
                            ID = clsVoucher.CheckVoucherType(xmlDebitors, xmlCreditors);
                            if (ID == "1")
                            {
                                MessageBox1.ShowInfo("Please Select Voucher Type GST");
                                return;
                            }

                        }
                        else if (this.ddlVoucher.SelectedValue == "2" || this.ddlVoucher.SelectedValue == "3")  /* 2 for gst */
                        {
                            ID = clsVoucher.CheckVoucherType(xmlDebitors, xmlCreditors);
                            if (ID == "2")
                            {
                                MessageBox1.ShowInfo("Please Select Voucher Type NORMAL");
                                return;
                            }
                        }
                        if (this.ddlVoucher.SelectedValue == "2")
                        {
                            if (this.ddlPlaceofSupply.SelectedValue == "0")
                            {
                                MessageBox1.ShowWarning("Please Select place of supply");
                                return;
                            }
                            else if (this.ddlPartyName.SelectedValue == "0")
                            {
                                MessageBox1.ShowWarning("Please Select Party name");
                                return;
                            }
                            else if (this.txtInvoiceNo.Text == "")
                            {
                                MessageBox1.ShowWarning("Please Fill Invoice No");
                                return;
                            }
                            else if (Convert.ToString(this.txtTaxPercent.Text) == "")
                            {
                                MessageBox1.ShowWarning("Tax Percent can't be blank");
                                return;
                            }
                            else if (Convert.ToDecimal(this.txtTaxPercent.Text) == 0)
                            {
                                MessageBox1.ShowWarning("Tax Percent can't be Zero");
                                return;
                            }
                            else if (Convert.ToString(this.txtTaxValue.Text) == "")
                            {
                                MessageBox1.ShowWarning("Tax value  can't be blank");
                                return;
                            }
                            else if (Convert.ToDecimal(this.txtTaxValue.Text) == 0)
                            {
                                MessageBox1.ShowWarning("Tax value can't be Zero");
                                return;
                            }
                            else if (this.txtHsn.Text == "" || this.txtHsn.Text == "0")
                            {
                                MessageBox1.ShowWarning("Please Fill HSN");
                                return;
                            }
                            int gstin = txtGstNo.Text.Length; /*if gstin number length less than 15 then you cannot save those voucher*/
                            if (gstin < 15)
                            {
                                MessageBox1.ShowWarning("GSTIN Number is Not Correct");
                                return;
                            }
                        }

                        if (Convert.ToString(this.txtTaxValue.Text) == "")
                        {
                            this.txtTaxValue.Text = "0";
                        }

                        if (Convert.ToString(this.txtTaxPercent.Text) == "")
                        {
                            this.txtTaxPercent.Text = "0";
                        }

                        VoucherNo = clsVoucher.InsertBillTagingVoucherDetails(this.ddlvouchertype.SelectedValue, this.ddlvouchertype.SelectedItem.Text, this.ddlregion.SelectedValue, this.ddlregion.SelectedItem.Text, PayementMode, this.txtvoucherdate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), this.txtNarration.Text.Replace("'", "").Trim(), HttpContext.Current.Session["IUSERID"].ToString(), mode, hdn_accid.Value.ToString(), xml, "", xmlcostcenter, "N", "Y", xmlDebitCredit, ViewState["OnlyBillTagFromPage"].ToString(), this.ddlPlaceofSupply.SelectedValue, this.ddlPartyName.SelectedValue,
                                            this.txtGstNo.Text.Trim(), this.txtInvociedate.Text.Trim(), this.txtInvoiceNo.Text.Trim(), Convert.ToString(this.txtTaxPercent.Text),
                                            Convert.ToString(this.txtTaxValue.Text), this.txtHsn.Text.Trim(), this.ddlVoucher.SelectedValue);   // LAST -1 PARAMETER MEANS IS FROM ACCOUNTS PAGE

                        if (VoucherNo == "2")
                        {
                            MessageBox1.ShowInfo("<b>Ledger details not found from system</b>");
                            return;
                        }
                        else if (VoucherNo == "3")
                        {
                            MessageBox1.ShowInfo("<b>Please check total Debit and credit amount</b>", 40, 400);
                            return;
                        }
                        else if (VoucherNo == "4" && this.ddlMode.SelectedValue == "B")
                        {
                            MessageBox1.ShowInfo("<b>Please check BANK ledger on your voucher</b>", 40, 400);
                            return;
                        }
                        else if (VoucherNo == "4" && this.ddlMode.SelectedValue == "C")
                        {
                            MessageBox1.ShowInfo("<b>Please check CASH ledger on your voucher</b>", 40, 400);
                            return;
                        }

                        if (VoucherNo != "")
                        {
                            String[] voucher = VoucherNo.Split('|');
                            string voucherid = voucher[0].Trim();
                            string voucherno = voucher[1].Trim();

                            if (dtinvoice.Rows.Count > 0)
                            {
                                string Invoicexml = string.Empty;

                                //if (mode == "A")
                                //{
                                //    int P = dtinvoice.Rows.Count - 1;

                                //    while (P >= 0)
                                //    {
                                //        if (dtinvoice.Rows[P]["InvoiceID"] == "")
                                //        {
                                //            dtinvoice.Rows[P]["InvoiceID"] = voucherid;
                                //            dtinvoice.Rows[P]["InvoiceNo"] = voucherno;
                                //            dtinvoice.AcceptChanges();
                                //        }
                                //        P--;
                                //    }
                                //}

                                //if (mode == "U")
                                //{
                                int P = dtinvoice.Rows.Count - 1;

                                while (P >= 0)
                                {
                                    if (Convert.ToDecimal(dtinvoice.Rows[P]["AmtPaid"]) == 0)
                                    {
                                        dtinvoice.Rows[P].Delete();
                                        dtinvoice.AcceptChanges();
                                    }
                                    P--;
                                }
                                //}

                                Invoicexml = ConvertDatatableToXML(dtinvoice);

                                clsVoucher.VoucherDetailsInsert(voucherid, mode, Invoicexml);

                            }


                            if (mode == "A")
                            {
                                if (_Savewithprint == "Save")
                                {
                                    MessageBox1.ShowSuccess("Voucher No : <b><font color='green'> " + voucherno + "</font></b> saved successfully", 40, 500);
                                    this.btnSave.Text = "Save"; /* Add by D.Mondal on 27/10/2018 */
                                    btndivsave.Visible = true;
                                    Session["CREDIT_LEDGERID"] = null;
                                    Session["DEBIT_LEDGERID"] = null;

                                }
                                else if (_Savewithprint == "Savewithprint")
                                {
                                    string upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + ddlvouchertype.SelectedValue.ToString() + "";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                                    this.btnSave.Text = "Save"; /* Add by D.Mondal on 27/10/2018 */
                                    btndivsave.Visible = true;

                                }
                            }
                            else
                            {
                                if (_Savewithprint == "Save")
                                {
                                    MessageBox1.ShowSuccess("Voucher No : <b><font color='green'> " + voucherno + "</font></b> updated successfully", 40, 500);
                                    this.btnSave.Text = "Save"; /* Add by D.Mondal on 27/10/2018 */
                                    btndivsave.Visible = true;
                                    ddlVoucher.Enabled = true;

                                }
                                else if (_Savewithprint == "Savewithprint")
                                {
                                    string upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + voucherid + "&Voucherid=" + ddlvouchertype.SelectedValue.ToString() + "";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
                                    this.btnSave.Text = "Save"; /* Add by D.Mondal on 27/10/2018 */
                                    btndivsave.Visible = true;
                                    ddlVoucher.Enabled = true;

                                }
                            }

                            this.trvoucherno.Style["display"] = "none";
                            this.btnaddhide.Style["display"] = "";
                            this.trvoucherno.Style["display"] = "none";
                            this.ddlregion.Enabled = true;
                            this.imgPopuppodate.Visible = true;

                            this.OtherDetailsReset();
                            this.hdn_accid.Value = "";
                            //pnlDisplay.Style["display"] = "";
                            //pnlAdd.Style["display"] = "none";
                            //this.ClearGridWithSession();
                            //this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(),  this.ddldepot.SelectedValue);


                            if (Convert.ToString(ViewState["AccType"]) == "Auto")
                            {
                                this.ddlregion.SelectedValue = Convert.ToString(ViewState["HOID"]);
                            }
                            else if (this.ddlregion.SelectedValue == Convert.ToString(ViewState["HOID"]) && this.ddlvouchertype.SelectedValue == "15")
                            {
                                DataSet dtvoucherdetails = new DataSet();
                                dtvoucherdetails = clsVoucher.PaymentVoucherDetails(voucherid);

                                if (dtvoucherdetails.Tables.Count != 0)
                                {
                                    if (dtvoucherdetails.Tables[1].Rows.Count > 0)
                                    {
                                        string strPopup = "<script language='javascript' ID='script1'>window.open('frmAccVoucher.aspx?VOUCHERID=2&CHECKER=FALSE&AUTOOPEN=TRUE&AUTOVOUCHERID=" + voucherid + "&AUTOVOUCHERDATE=" + this.txtvoucherdate.Text.Trim() + "&MODE=" + mode + "','new window', 'top=30, left=30, width=1200, height=550, toolbar=0,location=0, directories=0, status=0, menubar=0','_blank')</script>";
                                        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                                    }
                                }
                            }
                            this.ClearData();
                            this.NewAddMethod();
                            this.NewVoucherBind();
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlAccTypeDr.ClientID + "').focus(); ", true);

                        }
                        else
                        {
                            this.pnlDisplay.Style["display"] = "";
                            this.pnlAdd.Style["display"] = "none";
                            this.btnaddhide.Style["display"] = "";
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='black'>Please add atleast 1 debit or credit entry!</font></b>", 40, 400);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlAccTypeDr.ClientID + "').focus(); ", true);
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='black'>Total debit and credit amount must be same or not zero!</font></b>", 40, 500);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlAccTypeDr.ClientID + "').focus(); ", true);
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Voucher Date is Locked, Please Contact to Admin</b>", 40, 420);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 40, 500);
        }


    }

    public void AutoJournaltoDepot(string AccEntryID)
    {
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        DataSet dtvoucherdetails = new DataSet();
        dtvoucherdetails = clsVoucher.PaymentVoucherDetails(AccEntryID);

        if (dtvoucherdetails.Tables[1].Rows.Count > 0)
        {
            this.CreateVoucherTable();
            this.CreateInvoiceTable();
            this.Create_DebitCreditInvoiceTable();
            DataTable dtVoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            DataTable dtinvoice = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
            DataTable dtdebitcredit = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

            for (int i = 0; i < dtvoucherdetails.Tables[1].Rows.Count; i++)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["LedgerId"]).Trim();
                dr["LedgerName"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["LedgerName"]).Trim();
                dr["TxnType"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["TxnType"]).Trim();
                dr["Amount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["Amount"]).Trim();
                dr["BankID"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["BankID"]);
                dr["BankName"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["BankName"]);
                dr["PAYMENTTYPEID"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["PAYMENTTYPEID"]);
                dr["PAYMENTTYPENAME"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["PAYMENTTYPENAME"]);
                dr["ChequeNo"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["ChequeNo"]);
                dr["ChequeDate"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["ChequeDate"]);
                dr["IsChequeRealised"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["IsChequeRealised"]);
                dr["Remarks"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["Remarks"]);
                dr["DeductableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableAmount"]);
                dr["DeductableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableAmount"]);
                dr["DeductableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableAmount"]);
                dr["DeductableAmount"] = Convert.ToString(dtvoucherdetails.Tables[1].Rows[i]["DeductableAmount"]);

                dtVoucher.Rows.Add(dr);
                dtVoucher.AcceptChanges();
            }

            gvparentvoucher.DataSource = dtVoucher;
            gvparentvoucher.DataBind();
            HttpContext.Current.Session["VOUCHERDETAILS"] = dtVoucher;

            //------------------ For Credit Portion -----------------//
            dtVoucher.DefaultView.RowFilter = "TxnType = '1'";
            decimal sumcredit = (decimal)dtVoucher.Compute("Sum(Amount)", "TxnType = '1'");
            this.txtcredittotalamount.Text = sumcredit.ToString();

            gvcredit.DataSource = dtVoucher;
            gvcredit.DataBind();
            //------------------------------------------------------//

            ViewState["AccType"] = "Auto";

            this.ddlregion.SelectedValue = Convert.ToString(dtvoucherdetails.Tables[0].Rows[0]["LEDGERID"]).Trim();
        }
        else
        {
            this.ddlregion.SelectedValue = Convert.ToString(ViewState["HOID"]);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            this.Bindclose();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void Bindclose()
    {
        try
        {
            this.ddlVoucher.Enabled = true;
            this.ddlVoucher.SelectedValue = "1";
            this.trvoucherno.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.trvoucherno.Style["display"] = "none";
            this.ddlregion.Enabled = true;
            this.imgPopuppodate.Visible = true;
            this.ClearData();
            this.OtherDetailsReset();
            this.ClearGridWithSession();
            // this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(), this.ddldepot.SelectedValue,"");
            this.divbtnpaidamount.Style["display"] = "";
            ViewState["AccType"] = "Normal";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnotherdetails_Click(object sender, EventArgs e)
    {
        try
        {
            this.Popupotherdetails.Show();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 600);
        }
    }

    protected void BindMode()
    {
        try
        {
            if (this.ddlMode.SelectedValue == "C")
            {
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
                this.btnaddhide.Style["display"] = "none";
                this.trdebitbank.Style["display"] = "none";
                this.trdebitchequeno.Style["display"] = "none";
                this.trcreditbank.Style["display"] = "none";
                this.trcreditchequeno.Style["display"] = "none";

                //this.Bind_Receipt_Cash_Account(this.ddlvouchertype.SelectedValue, this.ddlMode.SelectedValue);
                this.gvdebit.Columns[4].Visible = false;
                this.gvdebit.Columns[5].Visible = false;
                this.gvdebit.Columns[6].Visible = false;
                this.gvcredit.Columns[4].Visible = false;
                this.gvcredit.Columns[5].Visible = false;
                this.gvcredit.Columns[6].Visible = false;
                this.gvcredit.Columns[7].Visible = false;
                this.gvcredit.Columns[8].Visible = false;
                this.gvcredit.Columns[9].Visible = false;
            }
            else
            {
                //BankNameForReceipt();
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
                this.btnaddhide.Style["display"] = "none";

                if (this.ddlvouchertype.SelectedValue.Trim() == "9" || this.ddlvouchertype.SelectedValue.Trim() == "15")
                {
                    this.trdebitbank.Style["display"] = "none";
                    this.trdebitchequeno.Style["display"] = "none";
                    this.trcreditbank.Style["display"] = "none";
                    this.trcreditchequeno.Style["display"] = "";

                    this.gvdebit.Columns[4].Visible = false;
                    this.gvdebit.Columns[5].Visible = false;
                    this.gvdebit.Columns[6].Visible = false;

                    this.gvcredit.Columns[4].Visible = false;
                    this.gvcredit.Columns[5].Visible = false;
                    this.gvcredit.Columns[6].Visible = true;
                    this.gvcredit.Columns[7].Visible = true;
                    this.gvcredit.Columns[8].Visible = true;
                    this.gvcredit.Columns[9].Visible = false;
                }
                else if (this.ddlvouchertype.SelectedValue.Trim() == "10" || this.ddlvouchertype.SelectedValue.Trim() == "16")
                {
                    this.trdebitbank.Style["display"] = "none";
                    this.trdebitchequeno.Style["display"] = "";
                    this.trcreditbank.Style["display"] = "none";
                    this.trcreditchequeno.Style["display"] = "none";

                    this.gvdebit.Columns[4].Visible = false;
                    this.gvdebit.Columns[5].Visible = false;
                    this.gvdebit.Columns[6].Visible = true;
                    this.gvdebit.Columns[7].Visible = true;
                    this.gvdebit.Columns[8].Visible = true;
                    this.gvdebit.Columns[9].Visible = false;

                    this.gvcredit.Columns[4].Visible = false;
                    this.gvcredit.Columns[5].Visible = false;
                    this.gvcredit.Columns[6].Visible = false;
                    this.gvcredit.Columns[7].Visible = false;
                    this.gvcredit.Columns[8].Visible = false;
                    this.gvcredit.Columns[9].Visible = true;
                }
                //this.Bind_Receipt_Bank_Account(this.ddlvouchertype.SelectedValue, this.ddlMode.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "15")
            {
                this.rdbcreditpaymenttype.SelectedValue = "2";

                this.txtcreditchequeno.SelectedValue = "";
                this.txtNeftNo.Text = "";
                //DateTime dtcurr = DateTime.Now;
                //string date = "dd/MM/yyyy";
                //this.txtcreditchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.lblcreditpaymentno.Text = "NEFT No";
                this.lblcreditpaymentdate.Text = "NEFT Date";
                this.tddebitsideoutstanding.Style["display"] = "";

                if (this.ddlMode.SelectedValue == "C")
                {
                    this.tdcreditsideoutstanding.Style["display"] = "";
                }
                else
                {
                    this.tdcreditsideoutstanding.Style["display"] = "none";
                }
            }
            else if (this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "16")
            {
                this.rdbdebitpaymenttype.SelectedValue = "2";
                this.txtdebitchequeno.Text = "";
                //DateTime dtcurr = DateTime.Now;
                //string date = "dd/MM/yyyy";
                //this.txtdebitchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.lbldebitpaymentno.Text = "NEFT No";
                this.lbldebitpaymentdate.Text = "NEFT Date";
            }
            this.BindMode();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void ClearGridWithSession()
    {
        this.Session["VOUCHERDETAILS"] = null;
        this.Session["INVOICEDETAILS"] = null;
        this.Session["COSTCENTERDETAILS"] = null;
        this.Session["INVOICEDETAILS_FOR_DUPLICATE"] = null;


        this.gvcredit.DataSource = null;
        this.gvcredit.DataBind();


        this.gvdebit.DataSource = null;
        this.gvdebit.DataBind();

        this.grd_InvoiceDetails.DataSource = null;
        this.grd_InvoiceDetails.DataBind();

        this.gvdebit.Columns[4].Visible = false;
        this.gvdebit.Columns[5].Visible = false;
        this.gvdebit.Columns[6].Visible = false;
        this.gvdebit.Columns[7].Visible = false;
        this.gvdebit.Columns[8].Visible = false;
        this.gvcredit.Columns[4].Visible = false;
        this.gvcredit.Columns[5].Visible = false;
        this.gvcredit.Columns[6].Visible = false;
        this.gvcredit.Columns[7].Visible = false;
        this.gvcredit.Columns[8].Visible = false;

        this.txtdebittotalamount.Text = "0";
        this.txtcredittotalamount.Text = "0";
        this.txtpaidamount.Text = "0";
    }

    protected void ClearData()
    {
        Session["CREDIT_LEDGERID"] = null;
        Session["DEBIT_LEDGERID"] = null;

        this.ddlPlaceofSupply.SelectedValue = "26";
        this.ddlPartyName.SelectedValue = "0";
        this.txtGstNo.Text = "";
        this.txtInvociedate.Text = "";
        this.txtInvoiceNo.Text = "";
        this.txtTaxPercent.Text = "";
        this.txtTaxValue.Text = "";
        this.txtGstNo.Text = "";
        this.txtHsn.Text = "";

        this.btnSave.Text = "Save";
        this.btnSave.Visible = true;
        this.divsavewithprint.Visible = true;
        this.hdn_accid.Value = "";
        this.hdn_creditguid.Value = "";
        this.hdn_debitguid.Value = "";
        this.hdn_creditamount.Value = "";
        this.hdn_debitamount.Value = "";
        this.txtNarration.Text = "";
        this.hdn_approved.Value = "";
        this.hdn_dayend.Value = "";

        this.ddldebitbankname.SelectedValue = "0";
        this.txtdebitchequeno.Text = "";
        //DateTime dtcurr = DateTime.Now;
        //string date = "dd/MM/yyyy";
        //this.txtdebitchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.ddlAccTypeDr.SelectedValue = "0";
        this.txtAmtDr.Text = "";
        this.txtdebittotalamount.Text = "0";

        this.ddlcreditbankname.SelectedValue = "0";
        this.txtcreditchequeno.SelectedValue = "";
        this.txtNeftNo.Text = "";
        //this.txtcreditchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
        this.ddlAcctypeCr.SelectedValue = "0";
        this.txtAmntCr.Text = "";
        this.txtcredittotalamount.Text = "0";
        this.txtpaidamount.Text = "0";

        //---------------- Reset True For Approved -----------------
        this.btndivsave.Visible = true;
        this.divsavewithprint.Visible = true;
        this.traccountinfocredit.Visible = true;
        this.traccountinfodebit.Visible = true;
        this.txtNarration.Enabled = true;
        this.divsavewithprint.Visible = true;
        this.gvdebit.Columns[9].Visible = true;
        this.gvcredit.Columns[9].Visible = true;
        this.gvdebit.Columns[13].Visible = false;
        this.gvdebit.Columns[14].Visible = true;  // DELETE BUTTON SHOW 
        this.gvcredit.Columns[11].Visible = true; // DELETE BUTTON SHOW

        this.gvcredit.Enabled = true;
        this.gvdebit.Enabled = true;
        this.imgPopuppodate.Visible = true;
        //----------------------------------------------------------

        this.txtcostcenteraccountname.Text = "";
        this.ddlcatagory.SelectedValue = "0";
        this.ddlcentername.SelectedValue = "0";
        this.ddlbranch.SelectedValue = "0";
        this.txtRealisedno.Text = "";
        this.txtRealisedDate.Text = "";
        this.txtCheckerNote.Text = "";
        this.tdcheckernote.Style["display"] = "none";

        /* 02/04/2019 Start*/
        //this.txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
        //string date = "dd/MM/yyyy";
        //DateTime dtcurr = DateTime.Now;
        //if (Session["FINYEAR"].ToString() == "2018-2019")
        //{
        //    dtcurr = Convert.ToDateTime(ViewState["EndDate"].ToString());
        //    this.txtvoucherdate.Text = dtcurr.ToString(date).Replace('-', '/');
        //}
        //else
        //{
        //    this.txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
        //}
        /* 02/04/2019 End*/
        InisiliseDate();
    }


    public void VoucherTypeIndexChanged()
    {
        try
        {
            // This is for Payment=9, Receipt=10, Advance Payment=15, Advance Receipt=16
            if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16")
            {
                this.ClearGridWithSession();    // Clear all Grid Value with Session

                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
                this.btnaddhide.Style["display"] = "none";
                this.innertr.Style["display"] = "";
                this.trpaymentmode.Style["display"] = "";

                this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);


                //---------------------------- Must be placed before BindMode() Method --------------------------------//
                if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "15")
                {
                    this.rdbcreditpaymenttype.SelectedValue = "2";
                    this.txtcreditchequeno.SelectedValue = "";
                    this.txtNeftNo.Text = "";
                    //DateTime dtcurr = DateTime.Now;
                    //string date = "dd/MM/yyyy";
                    //this.txtcreditchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.lblNftNo.Text = "NEFT No";
                    this.lblcreditpaymentdate.Text = "NEFT Date";
                    this.tddebitsideoutstanding.Style["display"] = "";
                    this.tdcreditsideoutstanding.Style["display"] = "none";
                    this.lbldebitoutstanding.Text = "0.00";
                    this.lblcreditoutstanding.Text = "0.00";

                    if (this.ddlvouchertype.SelectedValue == "9")
                    {
                        this.gvdebit.Columns[9].Visible = true;
                        this.gvcredit.Columns[9].Visible = false;
                    }
                    else
                    {
                        this.gvdebit.Columns[9].Visible = true;
                        this.gvcredit.Columns[9].Visible = false;
                    }
                }
                else if (this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "16")
                {
                    this.rdbdebitpaymenttype.SelectedValue = "2";
                    this.txtdebitchequeno.Text = "";
                    //DateTime dtcurr = DateTime.Now;
                    //string date = "dd/MM/yyyy";
                    //this.txtdebitchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                    this.lbldebitpaymentno.Text = "NEFT No";
                    this.lbldebitpaymentdate.Text = "NEFT Date";
                    this.tddebitsideoutstanding.Style["display"] = "none";
                    this.tdcreditsideoutstanding.Style["display"] = "";
                    this.lbldebitoutstanding.Text = "0.00";
                    this.lblcreditoutstanding.Text = "0.00";

                    if (this.ddlvouchertype.SelectedValue == "10")
                    {
                        this.gvdebit.Columns[9].Visible = false;
                        this.gvcredit.Columns[9].Visible = true;
                    }
                    else
                    {
                        this.gvdebit.Columns[9].Visible = false;
                        this.gvcredit.Columns[9].Visible = true;
                    }
                }

                //---------------------------- ------------------------------- --------------------------------//


                this.BindMode();
                this.trinvoicedetails.Style["display"] = "none";
                this.grd_InvoiceDetails.DataSource = null;
                this.grd_InvoiceDetails.DataBind();
            }
            else
            {
                this.ClearGridWithSession();        // Clear all Grid Value with Session

                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
                this.btnaddhide.Style["display"] = "none";
                this.trdebitbank.Style["display"] = "none";
                this.trdebitchequeno.Style["display"] = "none";
                this.trcreditbank.Style["display"] = "none";
                this.trcreditchequeno.Style["display"] = "none";
                this.innertr.Style["display"] = "";
                this.trpaymentmode.Style["display"] = "none";

                this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                this.ddlMode.SelectedValue = "C";
                this.BindMode();


                this.trinvoicedetails.Style["display"] = "none";
                Session["INVOICEDETAILS"] = null;
                //this.grd_InvoiceDetails.ClearPreviousDataSource();
                this.grd_InvoiceDetails.DataSource = null;
                this.grd_InvoiceDetails.DataBind();

                this.txtpaidamount.Text = "0";

                this.tddebitsideoutstanding.Style["display"] = "none";
                this.tdcreditsideoutstanding.Style["display"] = "none";
                this.lbldebitoutstanding.Text = "0.00";
                this.lblcreditoutstanding.Text = "0.00";

                if (this.ddlvouchertype.SelectedValue.Trim() == "13")
                {
                    this.gvdebit.Columns[9].Visible = false;
                    this.gvcredit.Columns[9].Visible = true;
                }
                else if (this.ddlvouchertype.SelectedValue.Trim() == "14")
                {
                    this.gvdebit.Columns[9].Visible = true;
                    this.gvcredit.Columns[9].Visible = false;
                }
                else
                {
                    this.gvdebit.Columns[9].Visible = false;
                    this.gvcredit.Columns[9].Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlvouchertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.VoucherTypeIndexChanged();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected int InvoiceDetailsBasedonLeadger(string AccountID, string AccountName, string AccountType, string ButtonClick, string BranchID)
    {
        int flag = 0;
        this.hdn_accounttype.Value = AccountType;
        this.hdn_accountid.Value = AccountID;
        this.txtaccountname.Text = AccountName;
        string vouhcerDate = this.txtvoucherdate.Text;

        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        if (Session["INVOICEDETAILS"] == null)
        {
            this.CreateInvoiceTable();
            /*new add by p.basu on 27-09-2021*/
            this.CreateInvoiceTableForDuplicate();
            /*new add by p.basu on 27-09-2021*/
        }



        DataTable dtinvoicedetails = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
        /*new add by p.basu on 27-09-2021*/
        DataTable dtinvoicedetailsforduplicate = (DataTable)HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"];
        /*new add by p.basu on 27-09-2021*/

        if (dtinvoicedetails.Rows.Count > 0)
        {
            int NumberofRecord = dtinvoicedetails.Select("LedgerId='" + AccountID + "'").Length;
            if (NumberofRecord > 0)
            {
                flag = 1;
            }
        }

        if (flag == 0)
        {
            string voucherid = hdn_accid.Value.ToString();
            string leadgerid = string.Empty;

            leadgerid = AccountID;
            DataTable dt = new DataTable();

            if (this.ddlvouchertype.SelectedValue == "15")  // 15=Advance Payment
            {
                dt = clsVoucher.InvoiceDetails(voucherid, leadgerid, "9", BranchID, vouhcerDate);    // 9=Payment
            }
            else if (this.ddlvouchertype.SelectedValue == "16")  // 16=Advance Receipt
            {
                dt = clsVoucher.InvoiceDetails(voucherid, leadgerid, "10", BranchID, vouhcerDate);    // 10=Receipt
            }
            else
            {
                /* Other Voucher will take care in SP please check */
                dt = clsVoucher.InvoiceDetails(voucherid, leadgerid, this.ddlvouchertype.SelectedValue, BranchID, vouhcerDate);
            }

            if (dt.Rows.Count > 0)
            {
                for (int counter = 0; counter < dt.Rows.Count; counter++)
                {
                    DataRow dr1 = dtinvoicedetails.NewRow();
                    dr1["GUID"] = Guid.NewGuid();
                    dr1["LedgerId"] = dt.Rows[counter]["LedgerId"].ToString().Trim();
                    dr1["LedgerName"] = dt.Rows[counter]["name"].ToString().Trim();
                    dr1["InvoiceID"] = dt.Rows[counter]["InvoiceID"].ToString().Trim();
                    dr1["InvoiceNo"] = dt.Rows[counter]["InvoiceNo"].ToString().Trim();
                    dr1["InvoiceDate"] = dt.Rows[counter]["InvoiceDate"].ToString().Trim();
                    dr1["InvoiceOthers"] = dt.Rows[counter]["InvoiceOthers"].ToString().Trim();
                    dr1["InvoiceBranchID"] = dt.Rows[counter]["InvoiceBranchID"].ToString().Trim();
                    dr1["InvoiceBranchName"] = dt.Rows[counter]["InvoiceBranchName"].ToString().Trim();
                    dr1["VoucherType"] = dt.Rows[counter]["VoucherType"].ToString().Trim();
                    dr1["BranchID"] = dt.Rows[counter]["BranchID"].ToString().Trim();
                    dr1["InvoiceAmt"] = dt.Rows[counter]["InvoiceAmt"].ToString().Trim();
                    dr1["AlreadyAmtPaid"] = dt.Rows[counter]["AlreadyAmtPaid"].ToString().Trim();
                    dr1["ReturnAmt"] = dt.Rows[counter]["ReturnAmt"].ToString().Trim();   /* Add ReturnAmt by D.Mondal on 11/10/2018  */
                    dr1["RemainingAmtPaid"] = dt.Rows[counter]["RemainingAmt"].ToString().Trim();
                    dr1["AmtPaid"] = dt.Rows[counter]["AmtPaid"].ToString().Trim();
                    dr1["Type"] = dt.Rows[counter]["Type"].ToString().Trim();

                    dtinvoicedetails.Rows.Add(dr1);
                    dtinvoicedetails.AcceptChanges();
                }
                HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoicedetails;
                /*new add by p.basu on 27-09-2021*/
                HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoicedetails;
                /*new add by p.basu on 27-09-2021*/
                flag = 1;
            }
        }

        if (flag == 1)
        {
            DataTable dtledgerinvoice = new DataTable();
            dtledgerinvoice = dtinvoicedetails.Clone();
            dtinvoicedetailsforduplicate = dtledgerinvoice.Clone();

            decimal paidamount = 0;

            if (dtinvoicedetails.Rows.Count > 0)
            {
                for (int counter = 0; counter < dtinvoicedetails.Rows.Count; counter++)
                {
                    if (dtinvoicedetails.Rows[counter]["LedgerId"].ToString().Trim() == AccountID)
                    {
                        DataRow dr1 = dtledgerinvoice.NewRow();
                        dr1["GUID"] = Guid.NewGuid();
                        dr1["LedgerId"] = dtinvoicedetails.Rows[counter]["LedgerId"].ToString().Trim();
                        dr1["LedgerName"] = dtinvoicedetails.Rows[counter]["LedgerName"].ToString().Trim();
                        dr1["InvoiceID"] = dtinvoicedetails.Rows[counter]["InvoiceID"].ToString().Trim();
                        dr1["InvoiceNo"] = dtinvoicedetails.Rows[counter]["InvoiceNo"].ToString().Trim();
                        dr1["InvoiceDate"] = dtinvoicedetails.Rows[counter]["InvoiceDate"].ToString().Trim();
                        dr1["InvoiceOthers"] = dtinvoicedetails.Rows[counter]["InvoiceOthers"].ToString().Trim();
                        dr1["InvoiceBranchID"] = dtinvoicedetails.Rows[counter]["InvoiceBranchID"].ToString().Trim();
                        dr1["InvoiceBranchName"] = dtinvoicedetails.Rows[counter]["InvoiceBranchName"].ToString().Trim();
                        dr1["VoucherType"] = dtinvoicedetails.Rows[counter]["VoucherType"].ToString().Trim();
                        dr1["BranchID"] = dtinvoicedetails.Rows[counter]["BranchID"].ToString().Trim();
                        dr1["InvoiceAmt"] = dtinvoicedetails.Rows[counter]["InvoiceAmt"].ToString().Trim();
                        dr1["AlreadyAmtPaid"] = dtinvoicedetails.Rows[counter]["AlreadyAmtPaid"].ToString().Trim();
                        dr1["ReturnAmt"] = dtinvoicedetails.Rows[counter]["ReturnAmt"].ToString().Trim();   /* Add ReturnAmt by D.Mondal on 11/10/2018  */
                        dr1["RemainingAmtPaid"] = dtinvoicedetails.Rows[counter]["RemainingAmtPaid"].ToString().Trim();
                        dr1["AmtPaid"] = dtinvoicedetails.Rows[counter]["AmtPaid"].ToString().Trim();
                        dr1["Type"] = dtinvoicedetails.Rows[counter]["Type"].ToString().Trim();

                        dtledgerinvoice.Rows.Add(dr1);
                        dtledgerinvoice.AcceptChanges();
                        paidamount = paidamount + Convert.ToDecimal(dtinvoicedetails.Rows[counter]["AmtPaid"].ToString().Trim());
                    }





                    if (dtinvoicedetails.Rows[counter]["LedgerId"].ToString().Trim() == AccountID)
                    {
                        DataRow dr1 = dtinvoicedetailsforduplicate.NewRow();
                        dr1["GUID"] = Guid.NewGuid();
                        dr1["LedgerId"] = dtinvoicedetails.Rows[counter]["LedgerId"].ToString().Trim();
                        dr1["LedgerName"] = dtinvoicedetails.Rows[counter]["LedgerName"].ToString().Trim();
                        dr1["InvoiceID"] = dtinvoicedetails.Rows[counter]["InvoiceID"].ToString().Trim();
                        dr1["InvoiceNo"] = dtinvoicedetails.Rows[counter]["InvoiceNo"].ToString().Trim();
                        dr1["InvoiceDate"] = dtinvoicedetails.Rows[counter]["InvoiceDate"].ToString().Trim();
                        dr1["InvoiceOthers"] = dtinvoicedetails.Rows[counter]["InvoiceOthers"].ToString().Trim();
                        dr1["InvoiceBranchID"] = dtinvoicedetails.Rows[counter]["InvoiceBranchID"].ToString().Trim();
                        dr1["InvoiceBranchName"] = dtinvoicedetails.Rows[counter]["InvoiceBranchName"].ToString().Trim();
                        dr1["VoucherType"] = dtinvoicedetails.Rows[counter]["VoucherType"].ToString().Trim();
                        dr1["BranchID"] = dtinvoicedetails.Rows[counter]["BranchID"].ToString().Trim();
                        dr1["InvoiceAmt"] = dtinvoicedetails.Rows[counter]["InvoiceAmt"].ToString().Trim();
                        dr1["AlreadyAmtPaid"] = dtinvoicedetails.Rows[counter]["AlreadyAmtPaid"].ToString().Trim();
                        dr1["ReturnAmt"] = dtinvoicedetails.Rows[counter]["ReturnAmt"].ToString().Trim();   /* Add ReturnAmt by D.Mondal on 11/10/2018  */
                        dr1["RemainingAmtPaid"] = dtinvoicedetails.Rows[counter]["RemainingAmtPaid"].ToString().Trim();
                        dr1["AmtPaid"] = dtinvoicedetails.Rows[counter]["AmtPaid"].ToString().Trim();
                        dr1["Type"] = dtinvoicedetails.Rows[counter]["Type"].ToString().Trim();

                        dtinvoicedetailsforduplicate.Rows.Add(dr1);
                        dtinvoicedetailsforduplicate.AcceptChanges();
                        paidamount = paidamount + Convert.ToDecimal(dtinvoicedetails.Rows[counter]["AmtPaid"].ToString().Trim());
                    }

                 
                }
            }


            /*new add by p.basu on 27-09-2021*/
            int P = dtinvoicedetailsforduplicate.Rows.Count - 1;

            if(this.ddlvouchertype.SelectedValue == "15")/*ADVANCE PAYMENT*/
            {
                while (P >= 0)
                {
                    if (Convert.ToString(dtinvoicedetailsforduplicate.Rows[P]["LedgerId"]) != this.ddlAccTypeDr.SelectedValue.ToString())
                    {
                        dtinvoicedetailsforduplicate.Rows[P].Delete();
                        dtinvoicedetailsforduplicate.AcceptChanges();
                    }
                    P--;
                }
            }
            else if (this.ddlvouchertype.SelectedValue == "15")/*ADVANCE PAYMENT*/
            {
                while (P >= 0)
                {
                    if (Convert.ToString(dtinvoicedetailsforduplicate.Rows[P]["LedgerId"]) != this.ddlAcctypeCr.SelectedValue.ToString())
                    {
                        dtinvoicedetailsforduplicate.Rows[P].Delete();
                        dtinvoicedetailsforduplicate.AcceptChanges();
                    }
                    P--;
                }
            }
            
            HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoicedetailsforduplicate;

            /*used for data view for future if its required*/
            DataView view = new DataView(dtinvoicedetailsforduplicate);
            DataTable distinctValues = new DataTable();
            distinctValues = view.ToTable(true, "GUID", "LedgerId", "LedgerName","InvoiceID", "InvoiceNo", "InvoiceDate", "InvoiceOthers", "InvoiceBranchID",
               "InvoiceBranchName", "VoucherType", "BranchID", "InvoiceAmt", "AlreadyAmtPaid", "ReturnAmt", "RemainingAmtPaid",
               "AmtPaid", "Type");
            /*used for data view for future if its required*/



            decimal grandTotal = 0;

            for (int counter = 0; counter < dtledgerinvoice.Rows.Count; counter++)
            {
                if (Convert.ToDecimal(dtledgerinvoice.Rows[counter]["AmtPaid"]) != 0)
                {
                    grandTotal += Convert.ToDecimal(dtledgerinvoice.Rows[counter]["AmtPaid"].ToString().Trim());
                }

            }
            /*new add by p.basu on 27-09-2021*/

            this.lblledgername.Text = "";
            this.grd_InvoiceDetails.DataSource = distinctValues;
            this.grd_InvoiceDetails.DataBind();
            this.trinvoicedetails.Style["display"] = "";
            this.lblledgername.Text = AccountName;
                //dtinvoicedetails.Rows[0]["LedgerName"].ToString().Trim();
            this.txtpaidamount.Text = grandTotal.ToString();
            /*if (ButtonClick == "Y")
            {
                this.divbtnpaidamount.Style["display"] = "none";
            }
            else
            {
                this.divbtnpaidamount.Style["display"] = "";
            }*/
        }
        else if (ButtonClick == "Y")
        {
            MessageBox1.ShowInfo("<b><font color='red'>No invoice found!</font></b>");
            flag = 2;
        }
        else
        {
            this.grd_InvoiceDetails.DataSource = null;
            this.grd_InvoiceDetails.DataBind();
            this.trinvoicedetails.Style["display"] = "none";
            this.lblledgername.Text = "";
        }
        return flag;
    }

    protected DataTable InvoiceDetails_Debit_Credit_Leadger(string AccountID, string AccountName)
    {
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        string voucherid = hdn_accid.Value.ToString();
        DataTable dt = new DataTable();

        if (this.ddlvouchertype.SelectedValue == "14")  // 14=Debit Note
        {
            dt = clsVoucher.InvoiceDetails_Debit_Credit(voucherid, AccountID, "14");
        }
        else if (this.ddlvouchertype.SelectedValue == "13")  // 14=Credit Note
        {
            dt = clsVoucher.InvoiceDetails_Debit_Credit(voucherid, AccountID, "13");
        }

        if (dt.Rows.Count > 0)
        {
            this.grd_debitcredit_invoice.DataSource = dt;
            this.grd_debitcredit_invoice.DataBind();

            DataTable dtinvoice = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

            if (dtinvoice.Rows.Count > 0)
            {
                int NumberofRecord = dtinvoice.Select("LedgerId='" + AccountID + "'").Length;
                if (NumberofRecord > 0)
                {

                    foreach (GridViewRow row in grd_debitcredit_invoice.Rows)
                    {
                        CheckBox chk = row.FindControl("chkinv") as CheckBox;

                        Label lbldebitcreditledgerid = row.FindControl("lbldebitcreditledgerid") as Label;
                        Label lbldebitcreditinvoiceid = row.FindControl("lbldebitcreditinvoiceid") as Label;

                        for (int i = 0; i < dtinvoice.Rows.Count; i++)
                        {
                            string invoiceid = "";
                            string ledgerid = "";

                            invoiceid = dtinvoice.Rows[i]["InvoiceID"].ToString().Trim();
                            ledgerid = dtinvoice.Rows[i]["LedgerId"].ToString().Trim();

                            if (invoiceid == lbldebitcreditinvoiceid.Text.Trim() && ledgerid == lbldebitcreditledgerid.Text.Trim())
                            {
                                chk.Checked = true;
                            }
                            else
                            {
                                chk.Checked = false;
                            }
                        }
                    }
                }
            }

            this.lbldebitledgername.Text = AccountName;
            this.popupinvoicedebitcredit.Show();
        }

        return dt;
    }

    public int CostCenterApplicable(string AccountID, string AccountName, decimal Amount, char IsForButtonClick)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            int flag = 0;
            flag = clsVoucher.CostCenterApplicable(AccountID);
            if (flag == 1)
            {
                if (HttpContext.Current.Session["COSTCENTERDETAILS"] == null)
                {
                    this.CreateCostCenterTable();
                }

                DataTable dt = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

                this.hdn_ledgercostcenter.Value = AccountID;
                this.hdn_accountid.Value = AccountID;
                this.txtcostcenteraccountname.Text = AccountName;

                dt.DefaultView.RowFilter = "LedgerId = '" + AccountID + "'";

                this.grdcostcenter.DataSource = dt;
                this.grdcostcenter.DataBind();

                CostcenterFooter(ref dt, this.hdn_ledgercostcenter.Value.Trim());

                this.lblcostcenteramountshow.Text = "";
                this.lblcostcenteramountshow.Text = Amount.ToString();
                this.Popupcostcenter.Show();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcatagory.ClientID + "').focus(); ", true);
            }
            else
            {
                if (IsForButtonClick == 'Y')
                {
                    MessageBox1.ShowInfo("<b>Cost center not available!</b>");
                }
            }
            return flag;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void btntaxadd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            int flag = 0;
            if (Session["VOUCHERDETAILS"] == null)
            {
                this.CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            decimal taxamount = 0;
            int count = 0;
            string AcountID = "";
            string AcountName = "";
            decimal AccountAmount = 0;

            if (this.hdn_accounttype.Value.Trim() == "0")
            {
                AcountID = this.ddlAccTypeDr.SelectedValue.Trim();
                AcountName = this.ddlAccTypeDr.SelectedItem.Text.Trim();
                AccountAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtAmtDr.Text.Trim()) ? "0" : this.txtAmtDr.Text.Trim());
            }
            else
            {
                AcountID = this.ddlAcctypeCr.SelectedValue.Trim();
                AcountName = this.ddlAcctypeCr.SelectedItem.Text.Trim();
                AccountAmount = Convert.ToDecimal(string.IsNullOrEmpty(this.txtAmntCr.Text.Trim()) ? "0" : this.txtAmntCr.Text.Trim());
            }

            if (AcountID != "0")
            {
                foreach (GridViewRow row in grdapplicabletax.Rows)
                {
                    TextBox txttdsamount = row.FindControl("txtdeductableamount") as TextBox;
                    CheckBox chk = row.FindControl("chktaxid") as CheckBox;
                    Label lbltaxname = row.FindControl("lbltaxname") as Label;
                    Label lbltaxpercent = row.FindControl("lbltaxpercent") as Label;
                    Label lbltaxid = row.FindControl("lbltaxid") as Label;

                    if (chk.Checked == true)
                    {
                        count = count + 1;
                        if (dtvoucher.Rows.Count > 0)
                        {
                            if (this.hdn_accounttype.Value.Trim() == "0")
                            {
                                flag = DatatableCheck(lbltaxid.Text.Trim(), lbltaxname.Text.Trim(), "", "0");
                            }
                            else
                            {
                                flag = DatatableCheck(lbltaxid.Text.Trim(), lbltaxname.Text.Trim(), "", "1");
                            }
                        }
                        if (flag == 0)
                        {
                            //taxamount = clsVoucher.TaxPercentage(chk.ToolTip, Convert.ToDecimal(this.hdn_paidamount.Value.Trim()));
                            taxamount = Math.Round((Convert.ToDecimal(lbltaxpercent.Text) * Convert.ToDecimal(txttdsamount.Text)) / 100);

                            string TaxAction = clsVoucher.TaxAction(lbltaxid.Text.Trim());
                            String[] Tax = TaxAction.Split('|');
                            string isnegative = Tax[0].Trim();
                            string isdrcr = Tax[1].Trim();

                            if (this.hdn_accounttype.Value.Trim() == "0")
                            {
                                //taxamount = clsVoucher.TaxAmount(lbltaxid.Text.Trim(), Convert.ToDecimal(txttdsamount.Text.Trim()), this.ddlAccTypeDr.SelectedValue);

                                if (isdrcr == "Dr")         /* TDS ALWAYS POSTING TO CREDIT SIDE WHEN TAX POP UP OPEN */
                                {
                                    this.Debitadd(lbltaxid.Text.Trim(), lbltaxname.Text.Trim(), taxamount, Convert.ToDecimal(txttdsamount.Text.Trim()), Convert.ToDecimal(lbltaxpercent.Text.Trim()), AcountID, "N", "N", 'Y');
                                }
                                else
                                {
                                    this.Creditadd(lbltaxid.Text.Trim(), lbltaxname.Text.Trim(), taxamount, Convert.ToDecimal(txttdsamount.Text.Trim()), Convert.ToDecimal(lbltaxpercent.Text.Trim()), AcountID, "N", "N", 'Y');
                                }

                                if (isnegative == "-" && isdrcr == "Dr")    /*(ONLY FOR DR SIDE CONDITION NOT FOR CR SIDE) IF PARTY ON DEBIT SIDE AND TDS DEDUCTED THEN AMOUNT FOR PARTY SAME ON DEBIT SIDE */
                                {
                                    this.Debitadd(AcountID, AcountName, (AccountAmount - taxamount), Convert.ToDecimal(txttdsamount.Text.Trim()), 0, "", "N", "N", 'Y');
                                }
                                else
                                {
                                    this.Debitadd(AcountID, AcountName, AccountAmount, AccountAmount, 0, "", "N", "N", 'Y');
                                }
                            }
                            else
                            {
                                //taxamount = clsVoucher.TaxAmount(lbltaxid.Text.Trim(), Convert.ToDecimal(txttdsamount.Text.Trim()), this.ddlAcctypeCr.SelectedValue);
                                this.Creditadd(lbltaxid.Text.Trim(), lbltaxname.Text.Trim(), taxamount, Convert.ToDecimal(txttdsamount.Text.Trim()), Convert.ToDecimal(lbltaxpercent.Text.Trim()), AcountID, "N", "N", 'Y');

                                if (isnegative == "-")
                                {
                                    this.Creditadd(AcountID, AcountName, (AccountAmount - taxamount), Convert.ToDecimal(txttdsamount.Text.Trim()), Convert.ToDecimal(lbltaxpercent.Text.Trim()), AcountID, "N", "N", 'Y');
                                }
                                else
                                {
                                    this.Creditadd(AcountID, AcountName, AccountAmount, AccountAmount, Convert.ToDecimal(lbltaxpercent.Text.Trim()), AcountID, "N", "N", 'Y');
                                }
                            }
                        }
                        else
                        {
                            MessageBox1.ShowInfo("<b><font color='green'>Account '" + lbltaxname.Text.Trim() + "' already exists!</font></b>", 60, 500);

                            this.PopupTax.Show();
                        }
                    }
                }

                if (count == 0)
                {
                    MessageBox1.ShowInfo("<b>Please choose atleast 1 Tax!</b>");
                    this.PopupTax.Show();
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Deductable ledger not found.</b>");
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void btnclosetax_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "";
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }


    public void TaxApplicable(string AccountID, string AccountName, string AccountType, decimal Amount, string BranchID)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            //decimal amount = 0;

            //if (AccountType == "0")
            //{
            //    amount = Convert.ToDecimal(this.txtAmtDr.Text.Trim());
            //}
            //else
            //{
            //    amount = Convert.ToDecimal(this.txtAmntCr.Text.Trim());
            //}

            DataTable dtTax = clsVoucher.TaxApplicable(AccountID, this.txtvoucherdate.Text.Trim(), Convert.ToString(Session["FINYEAR"]), Amount, BranchID);

            if (dtTax.Rows.Count > 0)
            {
                this.hdn_ledgercostcenter.Value = AccountID;
                this.hdn_accountid.Value = AccountID;
                this.txtcostcenteraccountname.Text = AccountName;

                this.grdapplicabletax.DataSource = dtTax;
                this.grdapplicabletax.DataBind();

                foreach (GridViewRow row in grdapplicabletax.Rows)
                {
                    TextBox txttdsamount = row.FindControl("txtdeductableamount") as TextBox;

                    //if (AccountType == "0")
                    //{
                    //    txttdsamount.Text = txtAmtDr.Text;
                    //}
                    //else
                    //{
                    //    txttdsamount.Text = txtAmntCr.Text;
                    //}
                }
                this.hdn_accounttype.Value = AccountType;
                this.PopupTax.Show();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void btncostcenterncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "";
            this.hdn_btncostcenterclick.Value = "N";
            decimal amtpaid = Convert.ToDecimal(string.IsNullOrEmpty(lblcostcenteramountshow.Text.Trim()) ? "0" : lblcostcenteramountshow.Text.Trim());
            this.TaxApplicable(this.hdn_accountid.Value.Trim(), this.txtcostcenteraccountname.Text, this.hdn_accounttype.Value, amtpaid, this.ddlregion.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void btncostcenteradd_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            if (Session["COSTCENTERDETAILS"] == null)
            {
                this.CreateCostCenterTable();
            }

            DataTable dtcost = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

            if (dtcost.Rows.Count > 0)
            {
                /* Add Segment (ddlcategory) for validation 06/10/2016 by Dhananjoy */
                //flag = Checkforcostcentre(this.hdn_ledgercostcenter.Value.Trim(), this.ddlcentername.SelectedValue.Trim());
                flag = Checkforcostcentre(this.hdn_ledgercostcenter.Value.Trim(), this.ddlcentername.SelectedValue.Trim(), this.ddlcatagory.SelectedValue.Trim());

            }
            if (flag == 0)
            {
                foreach (GridViewRow gvrow in grdcostcenter.Rows)
                {
                    Label lblCostcenterGUID = (Label)gvrow.FindControl("lblCostcenterGUID");
                    Label lblledgerid = (Label)gvrow.FindControl("lblledgerid");
                    TextBox txtamount = (TextBox)gvrow.FindControl("txtamount");
                    decimal Costamount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());

                    foreach (DataRow row in dtcost.Rows)
                    {
                        if ((Convert.ToString(row["GUID"]).Trim() == lblCostcenterGUID.Text.Trim()) && (Convert.ToString(row["LedgerId"]).Trim() == this.hdn_ledgercostcenter.Value.Trim()))
                        {
                            row["amount"] = Convert.ToString(Costamount);
                            dtcost.AcceptChanges();
                        }
                    }
                }

                decimal amount = string.IsNullOrEmpty(this.txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtamount.Text.Trim());
                DataRow dr = dtcost.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = this.hdn_ledgercostcenter.Value.Trim();
                dr["LedgerName"] = this.txtcostcenteraccountname.Text.Trim();
                dr["CostCatagoryID"] = this.ddlcatagory.SelectedValue.Trim();
                dr["CostCatagoryName"] = this.ddlcatagory.SelectedItem.Text;
                dr["CostCenterID"] = this.ddlcentername.SelectedValue.Trim();
                dr["CostCenterName"] = this.ddlcentername.SelectedItem.Text;
                dr["BranchID"] = this.ddlbranch.SelectedValue.Trim();
                dr["BranchName"] = this.ddlbranch.SelectedItem.Text;
                dr["amount"] = Convert.ToString(amount);
                dr["BrandID"] = this.ddlbrand.SelectedValue.Trim();
                dr["BrandName"] = this.ddlbrand.SelectedItem.Text;
                dr["ProductID"] = this.ddlproduct.SelectedValue.Trim();
                dr["ProductName"] = this.ddlproduct.SelectedItem.Text;
                dr["DepartmentID"] = this.ddldepartment.SelectedValue.Trim();
                dr["DepartmentName"] = this.ddldepartment.SelectedItem.Text;
                dr["FromDate"] = this.txtcostcenterfromdate.Text.Trim();
                dr["ToDate"] = this.txtcostcentertodate.Text.Trim();
                dr["Narration"] = "";
                dr["TxnType"] = Convert.ToString(this.hdn_accounttype.Value.Trim());

                dtcost.Rows.Add(dr);
                dtcost.AcceptChanges();

                grdcostcenter.DataSource = dtcost;
                HttpContext.Current.Session["COSTCENTERDETAILS"] = dtcost;
                grdcostcenter.DataBind();

                CostcenterFooter(ref dtcost, this.hdn_ledgercostcenter.Value.Trim());

                this.ddlcentername.SelectedValue = "0";
                //this.ddlcatagory.SelectedValue = "0";
                //this.ddlbranch.SelectedValue = "0";
                this.txtcostcenterfromdate.Text = "";
                this.txtcostcentertodate.Text = "";
                this.txtamount.Text = "";
                //this.ddlbrand.SelectedValue = "0";
                this.ddlproduct.SelectedValue = "0";
                //this.ddldepartment.SelectedValue = "0";
                this.Popupcostcenter.Show();

                if (this.hdn_btncostcenterclick.Value == "Y")
                {
                    this.CostCenterTagChaged(this.hdn_accounttype.Value.Trim(), this.hdn_accountid.Value.Trim(), Convert.ToDecimal(this.lblcostcenteramountshow.Text));
                }

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcatagory.ClientID + "').focus(); ", true);
            }
            else
            {
                this.Popupcostcenter.Show();
                MessageBox1.ShowInfo("<b>Cost Center is already exists</b>", 50, 400);
            }
        }

        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void CostcenterFooter(ref DataTable dtcost, string CostcenterLedgerid)
    {
        try
        {
            if (dtcost.Rows.Count > 0)
            {
                int NumberofRecord = dtcost.Select("LedgerId='" + CostcenterLedgerid + "'").Length;
                if (NumberofRecord > 0)
                {
                    decimal sumamt = (decimal)dtcost.Compute("Sum(amount)", "LedgerId = '" + CostcenterLedgerid + "'");
                    Label footeramount = grdcostcenter.FooterRow.FindControl("lblfooteramount") as Label;
                    if (sumamt != 0)
                    {
                        footeramount.Text = sumamt.ToString();
                    }
                    else
                    {
                        footeramount.Text = "0.00";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /* Add Degment ID for validation by D.Mondal on 06/10/2018 */
    public int Checkforcostcentre(string Ledgerid, string CostCenterID, string SegmentID)
    {
        int flagcostcentrecheck = 0;
        DataTable dtcostcheck = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

        if (dtcostcheck.Rows.Count > 0)
        {
            //int NumberofRecord = dtcostcheck.Select("LedgerId='" + Ledgerid + "' AND CostCenterID='" + CostCenterID + "'").Length;
            int NumberofRecord = dtcostcheck.Select("LedgerId='" + Ledgerid + "' AND CostCenterID='" + CostCenterID + "' AND CostCatagoryID = '" + SegmentID + "'").Length;
            if (NumberofRecord > 0)
            {
                flagcostcentrecheck = 1;

            }
        }
        return flagcostcentrecheck;
    }

    //protected void txtamount_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    this.VoucherTypeIndexChanged();
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
    //    //}

    //}


    public void btncostcentersave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtcost = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

            if (dtcost != null)
            {
                if (dtcost.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in grdcostcenter.Rows)
                    {
                        Label lblCostcenterGUID = (Label)gvrow.FindControl("lblCostcenterGUID");
                        Label lblledgerid = (Label)gvrow.FindControl("lblledgerid");
                        TextBox txtamount = (TextBox)gvrow.FindControl("txtamount");
                        decimal Costamount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());

                        foreach (DataRow row in dtcost.Rows)
                        {
                            if ((Convert.ToString(row["GUID"]).Trim() == lblCostcenterGUID.Text.Trim()) && (Convert.ToString(row["LedgerId"]).Trim() == this.hdn_ledgercostcenter.Value.Trim()))
                            {
                                row["amount"] = Convert.ToString(Costamount);
                                dtcost.AcceptChanges();
                            }
                        }
                    }

                    decimal costamount = CalculateTotalAmountCostCenter(dtcost, this.hdn_accountid.Value.Trim());
                    decimal amtpaid = Convert.ToDecimal(string.IsNullOrEmpty(lblcostcenteramountshow.Text.Trim()) ? "0" : lblcostcenteramountshow.Text.Trim());
                    if (costamount == amtpaid)
                    {
                        this.pnlAdd.Style["display"] = "";
                        this.TaxApplicable(this.hdn_accountid.Value.Trim(), this.txtcostcenteraccountname.Text, this.hdn_accounttype.Value, amtpaid, this.ddlregion.SelectedValue);

                        if (this.hdn_accounttype.Value == "0")
                        {
                            this.DebitAddGrid('Y', Convert.ToChar(string.IsNullOrEmpty(this.hdn_btncostcenterclick.Value) ? "N" : this.hdn_btncostcenterclick.Value));
                            /*ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btndebitadd.ClientID + "').focus(); ", true);*/
                        }
                        else
                        {
                            this.CreditAddGrid('Y', Convert.ToChar(string.IsNullOrEmpty(this.hdn_btncostcenterclick.Value) ? "N" : this.hdn_btncostcenterclick.Value));
                            /*ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btncreditadd.ClientID + "').focus(); ", true);*/
                        }

                        this.hdn_btncostcenterclick.Value = "N";
                    }
                    else
                    {
                        this.Popupcostcenter.Show();
                        MessageBox1.ShowInfo("<b>Please check Cost center total amount which must be same as ledger <font color='green'>" + txtcostcenteraccountname.Text.Trim() + "</font> amount Rs: " + amtpaid.ToString() + "/-</b>", 50, 560);
                        CostcenterFooter(ref dtcost, this.hdn_ledgercostcenter.Value.Trim());
                    }
                }
                else
                {
                    this.Popupcostcenter.Show();
                    MessageBox1.ShowInfo("<b>Please add atleast 1 cost center details</b>", 50, 400);
                }
            }
            else
            {
                this.Popupcostcenter.Show();
                MessageBox1.ShowInfo("<b>Please add atleast 1 cost center details</b>", 50, 400);
            }

        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public int DatatableCheck(string Ledgerid, string BankId, string ChequeNo, string TxnType)
    {
        int flag = 0;
        DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

        if (dtvoucher.Rows.Count > 0)
        {
            int NumberofRecord = dtvoucher.Select("LedgerId='" + Ledgerid + "' AND ChequeNo='" + ChequeNo + "' AND TxnType='" + TxnType + "'").Length;
            if (NumberofRecord > 0)
            {
                flag = 1;
            }
        }
        return flag;
    }

    public string IsCostCenterExists(string Ledgerid)
    {
        string flag = "N";
        if (HttpContext.Current.Session["COSTCENTERDETAILS"] != null)
        {
            DataTable dtcostcenter = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

            if (dtcostcenter.Rows.Count > 0)
            {
                int NumberofRecord = dtcostcenter.Select("LedgerId='" + Ledgerid + "'").Length;
                if (NumberofRecord > 0)
                {
                    flag = "Y";
                }
            }
        }
        return flag;
    }

    public int DatatableGuidCheck(string Guid)
    {
        int flag = 0;
        DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
        int i = dtvoucher.Rows.Count - 1;

        while (i >= 0)
        {
            if (Convert.ToString(dtvoucher.Rows[i]["GUID"]) == Guid)
            {
                dtvoucher.Rows[i].Delete();
                dtvoucher.AcceptChanges();
                flag = 1;
            }
            i--;
        }
        HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;
        return flag;
    }

    public int DatatableCostCenterCheck(string LedgerId, char IsFromPopUP)
    {
        int flag = 0;
        DataTable dtcostcenter = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];
        int i = dtcostcenter.Rows.Count - 1;

        while (i >= 0)
        {
            if (IsFromPopUP == 'N')         /* Basically call when total ledger delete from debit or credit side */
            {
                if (Convert.ToString(dtcostcenter.Rows[i]["LedgerId"]) == LedgerId)
                {
                    dtcostcenter.Rows[i].Delete();
                    dtcostcenter.AcceptChanges();
                    flag = 1;
                }
            }
            else                           /* Basically call from cost center pop up delete button */
            {
                if (Convert.ToString(dtcostcenter.Rows[i]["GUID"]) == LedgerId)
                {
                    dtcostcenter.Rows[i].Delete();
                    dtcostcenter.AcceptChanges();
                    flag = 1;
                }
            }
            i--;
        }
        HttpContext.Current.Session["COSTCENTERDETAILS"] = dtcostcenter;
        return flag;
    }

    public void CostCenterTagChaged(string TxnType, string LedgerID, decimal Amount)
    {
        if (Session["VOUCHERDETAILS"] != null)
        {
            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

            string CostcenterFlag = "N";

            foreach (DataRow row in dtvoucher.Rows)
            {
                if (Convert.ToString(row["LedgerId"]).Trim() == LedgerID && Convert.ToString(row["TxnType"]).Trim() == TxnType)
                {
                    CostcenterFlag = IsCostCenterExists(LedgerID);
                    row["Amount"] = Convert.ToString(Amount);
                    row["IsCostCenter"] = Convert.ToString(CostcenterFlag);
                }
            }

            dtvoucher.AcceptChanges();
            HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;

            if (TxnType == "1")
            {
                dtvoucher.DefaultView.RowFilter = "TxnType = '" + TxnType + "'";

                this.gvcredit.DataSource = dtvoucher;
                this.gvcredit.DataBind();
            }
            else
            {
                dtvoucher.DefaultView.RowFilter = "TxnType = '" + TxnType + "'";

                this.gvdebit.DataSource = dtvoucher;
                this.gvdebit.DataBind();
            }
        }
    }

    public int InvoicetableDelete(string LedgerID)
    {
        decimal paidamount = Convert.ToDecimal(this.txtpaidamount.Text.Trim());
        decimal deleteamt = 0;
        int flag = 0;
        DataTable dtInvoice = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
        if (dtInvoice != null)
        {
            int i = dtInvoice.Rows.Count - 1;

            while (i >= 0)
            {
                if (Convert.ToString(dtInvoice.Rows[i]["LedgerId"]) == LedgerID)
                {
                    deleteamt = Convert.ToDecimal(dtInvoice.Rows[i]["AmtPaid"].ToString().Trim());
                    paidamount = paidamount - deleteamt;

                    dtInvoice.Rows[i].Delete();
                    dtInvoice.AcceptChanges();
                    flag = 1;
                    this.trinvoicedetails.Style["display"] = "none";
                }
                i--;
            }
            HttpContext.Current.Session["INVOICEDETAILS"] = dtInvoice;
            this.txtpaidamount.Text = paidamount.ToString();
        }
        return flag;
    }

    protected int Debitadd(string AccountID, string AccountName, decimal DebitAmount, decimal DeductableAmount, decimal DeductablePercent, string DeductableLedgerID, string IsCostCenter, string IsTagInvoice, char Isautoposting)
    {
        try
        {
            int flag = 0;
            decimal totaldebitamount = 0;
            //totaldebitamount = Convert.ToDecimal(this.txtdebittotalamount.Text.Trim());

            if (Session["VOUCHERDETAILS"] == null)
            {
                this.CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableCheck(AccountID, AccountName, this.txtdebitchequeno.Text.Trim(), "0");
            }
            if (flag == 0)
            {
                IsCostCenter = IsCostCenterExists(AccountID);

                DataRow dr = dtvoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = AccountID;
                dr["LedgerName"] = AccountName;
                dr["TxnType"] = Convert.ToString("0");
                dr["Amount"] = Convert.ToString(DebitAmount);
                dr["BankID"] = Convert.ToString(this.ddldebitbankname.SelectedValue);
                dr["BankName"] = "";
                if (!string.IsNullOrEmpty(this.rdbdebitpaymenttype.SelectedValue.Trim()) && Isautoposting == 'N')
                {
                    dr["PAYMENTTYPEID"] = Convert.ToString(this.rdbdebitpaymenttype.SelectedValue.Trim());
                    dr["PAYMENTTYPENAME"] = Convert.ToString(this.rdbdebitpaymenttype.SelectedItem.Text.Trim());
                    dr["ChequeNo"] = Convert.ToString(this.txtdebitchequeno.Text.Trim());
                    dr["ChequeDate"] = Convert.ToString(this.txtdebitchequedate.Text.Trim());
                }
                else
                {
                    dr["PAYMENTTYPEID"] = "";
                    dr["PAYMENTTYPENAME"] = "";
                    dr["ChequeNo"] = "";
                    dr["ChequeDate"] = "";
                }
                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString(this.txtdebitremarks.Text.Trim());
                if (DeductablePercent == 0)
                {
                    dr["DeductableAmount"] = Convert.ToString(0);
                }
                else
                {
                    dr["DeductableAmount"] = Convert.ToString(DeductableAmount);
                }
                dr["DeductablePercentage"] = Convert.ToString(DeductablePercent);
                dr["DeductableLedgerId"] = Convert.ToString(DeductableLedgerID);
                dr["IsCostCenter"] = Convert.ToString(IsCostCenter);
                dr["IsTagInvoice"] = Convert.ToString(IsTagInvoice);

                dtvoucher.Rows.Add(dr);
                dtvoucher.AcceptChanges();

                HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;

                for (int d = 0; d < dtvoucher.Rows.Count; d++)
                {
                    if (Convert.ToString(dtvoucher.Rows[d]["TxnType"]).Trim() == "0")
                    {
                        totaldebitamount = totaldebitamount + Convert.ToDecimal(dtvoucher.Rows[d]["Amount"]);
                    }
                }


                dtvoucher.DefaultView.RowFilter = "TxnType = '0'";

                this.gvdebit.DataSource = dtvoucher;
                this.gvdebit.DataBind();


                // -------------------------------------------- Invoice Details Add -------------------------------------------------// 

                //if (this.ddlvouchertype.SelectedValue.Trim() == "14")           // ONLY FOR DEBIT NOTE
                //{
                //    DataRow dr1 = dtinvoicedetails.NewRow();
                //    dr1["GUID"] = Guid.NewGuid();
                //    dr1["LedgerId"] = AccountID;
                //    dr1["LedgerName"] = AccountName;
                //    dr1["InvoiceID"] = "";
                //    dr1["InvoiceNo"] = "";
                //    dr1["VoucherType"] = this.ddlvouchertype.SelectedValue.Trim();
                //    dr1["BranchID"] = this.ddlregion.SelectedValue.Trim();
                //    dr1["InvoiceAmt"] = Convert.ToString(DebitAmount);
                //    dr1["AlreadyAmtPaid"] = Convert.ToString(0.00);
                //    dr1["RemainingAmtPaid"] = Convert.ToString(0.00);
                //    dr1["AmtPaid"] = Convert.ToString(0.00);
                //    dr1["InvoiceDate"] = "";
                //    dr1["InvoiceBranchID"] = "";
                //    dr1["InvoiceOthers"] = "";

                //    dtinvoicedetails.Rows.Add(dr1);
                //    dtinvoicedetails.AcceptChanges();

                //    HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoicedetails;
                //}

                //   -----------------------------------------------------------------------------------------------------------



                //totaldebitamount = totaldebitamount + DebitAmount;
                this.txtdebittotalamount.Text = Convert.ToString(totaldebitamount);

                this.ddldebitbankname.SelectedValue = "0";
                this.txtdebitchequeno.Text = "";
                this.txtdebitremarks.Text = "";
                //DateTime dtcurr = DateTime.Now;
                //string date = "dd/MM/yyyy";
                //this.txtdebitchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.ddlAccTypeDr.SelectedValue = "0";
                this.txtAmtDr.Text = "";
            }
            else
            {

                //MessageBox1.ShowInfo("<b><font color='red'>Cheque no already exists with same bank!</font></b>",60,400);
                MessageBox1.ShowInfo("<b>This account already exists!</b>");
            }
            return flag;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int Creditadd(string AccountID, string AccountName, decimal CreditAmount, decimal DeductableAmount, decimal DeductablePercent, string DeductableLedgerID, string IsCostCenter, string IsTagInvoice, char Isautoposting)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            int flag = 0;
            decimal totalcreditamount = 0;
            //totalcreditamount = Convert.ToDecimal(this.txtcredittotalamount.Text.Trim());

            string chequeNo = string.Empty;
            if (this.txtcreditchequeno.SelectedValue.Trim() == "" || this.txtcreditchequeno.SelectedValue.Trim() == "0")
            {
                chequeNo = this.txtNeftNo.Text;
            }
            else
            {
                chequeNo = this.txtcreditchequeno.SelectedValue.Trim();
            }

            if (Session["VOUCHERDETAILS"] == null)
            {
                this.CreateVoucherTable();
            }
            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableCheck(AccountID, this.ddlcreditbankname.SelectedValue, chequeNo, "1");
            }
            if (flag == 0)
            {
                IsCostCenter = IsCostCenterExists(AccountID);

                DataRow dr = dtvoucher.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["LedgerId"] = AccountID;
                dr["LedgerName"] = AccountName;
                dr["TxnType"] = Convert.ToString("1");
                dr["Amount"] = Convert.ToString(CreditAmount);
                dr["BankID"] = Convert.ToString(this.ddlcreditbankname.SelectedValue);
                dr["BankName"] = "";

                if (!string.IsNullOrEmpty(this.rdbcreditpaymenttype.SelectedValue.Trim()) && Isautoposting == 'N')
                {




                    dr["PAYMENTTYPEID"] = Convert.ToString(this.rdbcreditpaymenttype.SelectedValue.Trim());
                    dr["PAYMENTTYPENAME"] = Convert.ToString(this.rdbcreditpaymenttype.SelectedItem.Text.Trim());
                    dr["ChequeNo"] = Convert.ToString(chequeNo);
                    dr["ChequeDate"] = Convert.ToString(this.txtcreditchequedate.Text.Trim());
                    /*Validation for Cheque Number 01/10/2019 */
                    if (this.rdbcreditpaymenttype.SelectedItem.Text.Trim() != "ON LINE")
                    {
                        int BankFlag = clsVoucher.CheckBANKID(AccountID);
                        if (Convert.ToString(chequeNo) == "" && BankFlag == 1 && this.ddlvouchertype.SelectedValue == "15")
                        {
                            MessageBox1.ShowInfo("<b>Select cheque number !</b>");
                            return 0;
                        }
                    }
                }
                else
                {
                    dr["PAYMENTTYPEID"] = "";
                    dr["PAYMENTTYPENAME"] = "";
                    dr["ChequeNo"] = "";
                    dr["ChequeDate"] = "";
                }

                dr["IsChequeRealised"] = Convert.ToString("N");
                dr["Remarks"] = Convert.ToString(this.txtcreditremarks.Text.Trim());
                if (DeductablePercent == 0)
                {
                    dr["DeductableAmount"] = Convert.ToString(0);
                }
                else
                {
                    dr["DeductableAmount"] = Convert.ToString(DeductableAmount);
                }
                dr["DeductablePercentage"] = Convert.ToString(DeductablePercent);
                dr["DeductableLedgerId"] = Convert.ToString(DeductableLedgerID);
                dr["IsCostCenter"] = Convert.ToString(IsCostCenter);
                dr["IsTagInvoice"] = Convert.ToString(IsTagInvoice);

                dtvoucher.Rows.Add(dr);
                dtvoucher.AcceptChanges();

                HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;

                for (int c = 0; c < dtvoucher.Rows.Count; c++)
                {
                    if (Convert.ToString(dtvoucher.Rows[c]["TxnType"]).Trim() == "1")
                    {
                        totalcreditamount = totalcreditamount + Convert.ToDecimal(dtvoucher.Rows[c]["Amount"]);
                    }
                }

                dtvoucher.DefaultView.RowFilter = "TxnType = '1'";

                gvcredit.DataSource = dtvoucher;
                gvcredit.DataBind();


                // -------------------------------------------- Invoice Details Add -------------------------------------------------// 

                //if (this.ddlvouchertype.SelectedValue.Trim() == "13")           // ONLY FOR CREDIT NOTE
                //{
                //    DataRow dr1 = dtinvoicedetails.NewRow();
                //    dr1["GUID"] = Guid.NewGuid();
                //    dr1["LedgerId"] = AccountID;
                //    dr1["LedgerName"] = AccountName;
                //    dr1["InvoiceID"] = "";
                //    dr1["InvoiceNo"] = "";
                //    dr1["VoucherType"] = this.ddlvouchertype.SelectedValue.Trim();
                //    dr1["BranchID"] = this.ddlregion.SelectedValue.Trim();
                //    dr1["InvoiceAmt"] = Convert.ToString(CreditAmount);
                //    dr1["AlreadyAmtPaid"] = Convert.ToString(0.00);
                //    dr1["RemainingAmtPaid"] = Convert.ToString(0.00);
                //    dr1["AmtPaid"] = Convert.ToString(0.00);
                //    dr1["InvoiceDate"] = "";
                //    dr1["InvoiceBranchID"] = "";
                //    dr1["InvoiceOthers"] = "";

                //    dtinvoicedetails.Rows.Add(dr1);
                //    dtinvoicedetails.AcceptChanges();

                //    HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoicedetails;
                //}

                //   -----------------------------------------------------------------------------------------------------------

                //totalcreditamount = totalcreditamount + CreditAmount;
                this.txtcredittotalamount.Text = Convert.ToString(totalcreditamount);

                this.ddlcreditbankname.SelectedValue = "0";

                this.txtcreditchequeno.SelectedValue = "";
                this.txtNeftNo.Text = "";
                this.txtcreditremarks.Text = "";
                //DateTime dtcurr = DateTime.Now;
                //string date = "dd/MM/yyyy";
                //this.txtcreditchequedate.Text = dtcurr.ToString(date).Replace('-', '/');
                // this.ddlAcctypeCr.SelectedValue = "0";
                this.txtAmntCr.Text = "";
            }
            else
            {
                //MessageBox1.ShowInfo("<b><font color='red'>Cheque no already exists with same bank!</font></b>", 60, 400);
                MessageBox1.ShowInfo("<b>This account already exists!</b>");
            }
            return flag;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btndebitadd_Click(object sender, EventArgs e)
    {
        try
        {
            this.DebitAddGrid('N', 'N');
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btncreditadd_Click(object sender, EventArgs e)
    {
        try
        {
            this.CreditAddGrid('N', 'N');
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    private void DebitAddGrid(char IsCallFromCostcenterPopup, char IsCallFromCostcenterViewbtn)
    {
        try
        {
            if (IsCallFromCostcenterViewbtn == 'N')
            {
                if (this.ddlregion.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("<b>Please select region.</b>");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlregion.ClientID + "').focus(); ", true);
                    return;
                }

                this.Debitadd(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text, Convert.ToDecimal(this.txtAmtDr.Text.Trim()), Convert.ToDecimal(this.txtAmtDr.Text.Trim()), 0, "", "N", "N", 'N');
                this.lbldebitoutstanding.Text = "0.00";
                this.lblcreditoutstanding.Text = "0.00";
                this.AutoNarration(this.ddlvouchertype.SelectedValue);

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlAcctypeCr.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreditAddGrid(char IsCallFromCostcenterPopup, char IsCallFromCostcenterViewbtn)
    {
        try
        {
            if (IsCallFromCostcenterViewbtn == 'N')
            {
                if (this.ddlregion.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("<b>Please select region.</b>");
                    return;
                }

                if (this.ddlMode.SelectedValue == "C")
                {
                    ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                    string IsNegativeBal = clsVoucher.IsNegativeBal(this.ddlAcctypeCr.SelectedValue, this.ddlvouchertype.SelectedValue, "1", this.ddlMode.SelectedValue);

                    if (IsNegativeBal == "Y")
                    {
                        if (Convert.ToDecimal(this.txtAmntCr.Text.Trim()) > Convert.ToDecimal(this.lblcreditoutstanding.Text))
                        {
                            MessageBox1.ShowInfo("<b>Amount exceeds the maximum limit of balance</b>", 40, 400);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtAmntCr.ClientID + "').focus(); ", true);
                            return;
                        }
                    }
                }

                this.Creditadd(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text, Convert.ToDecimal(this.txtAmntCr.Text.Trim()), Convert.ToDecimal(this.txtAmntCr.Text.Trim()), 0, "", "N", "N", 'N');
                this.lbldebitoutstanding.Text = "0.00";
                this.lblcreditoutstanding.Text = "0.00";
                this.AutoNarration(this.ddlvouchertype.SelectedValue);

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtNarration.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void AutoNarration(string VoucherType)
    {
        try
        {
            string Narration = "";
            int count = 0;
            if (VoucherType == "15" || VoucherType == "16") /* Payment || Receipt */
            {
                if (HttpContext.Current.Session["VOUCHERDETAILS"] != null)
                {
                    DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
                    for (int c = 0; c < dtvoucher.Rows.Count; c++)
                    {
                        if (Convert.ToString(dtvoucher.Rows[c]["ChequeNo"]).Trim() != "")
                        {
                            if (count > 0)  /* This comma separator only for more then 1 CHQ/NEFT/RTGS no available into this voucher */
                            {
                                Narration = Narration + ", ";
                            }

                            Narration = Narration + Convert.ToString(dtvoucher.Rows[c]["PAYMENTTYPENAME"]).Trim() + " NO. " + Convert.ToString(dtvoucher.Rows[c]["ChequeNo"]).Trim() + " DATED : " + Convert.ToString(dtvoucher.Rows[c]["ChequeDate"]).Trim() + " OF " + Convert.ToString(dtvoucher.Rows[c]["LedgerName"]).Trim() + " ";
                            count = count + 1;
                        }
                    }
                }
                this.txtNarration.Text = Narration;   /* 05/10/2018 */

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnpaidamt_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtinvoicedetails = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
            /*new add by p.basu on 27-09-2021*/
            DataTable dtinvoicedetailsforduplicate1 = (DataTable)HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"];
       
            if (dtinvoicedetailsforduplicate1 == null)
            {
                CreateInvoiceTableForDuplicate();
            }

            DataTable dtinvoicedetailsforduplicate = (DataTable)HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"];
            /*new add by p.basu on 27-09-2021*/

            string accountledgerid = string.Empty;
            string accountledgername = string.Empty;

            if (this.hdn_accounttype.Value == "0")
            {
                if (this.ddlAccTypeDr.SelectedValue == "0")
                {
                    accountledgerid = this.hdn_ledgercostcenter.Value.Trim();
                    accountledgername = this.hdn_ledgername.Value.Trim();
                }
                else
                {
                    accountledgerid = this.ddlAccTypeDr.SelectedValue.Trim();
                    accountledgername = this.ddlAccTypeDr.SelectedItem.Text;
                }

                if (accountledgerid == "")
                {
                    foreach (GridViewRow gvrow in gvdebit.Rows)
                    {
                        Label lblledgerid = (Label)gvrow.FindControl("lbldrLedgerId");
                        Label lblledgername = (Label)gvrow.FindControl("lbldrledgername");

                        accountledgerid = lblledgerid.Text.Trim();
                        accountledgername = lblledgername.Text.Trim();

                        break;
                    }
                }
            }
            else
            {
                if (this.ddlAcctypeCr.SelectedValue == "0")
                {
                    accountledgerid = this.hdn_ledgercostcenter.Value.Trim();
                    accountledgername = this.hdn_ledgername.Value.Trim();
                }
                else
                {
                    accountledgerid = this.ddlAcctypeCr.SelectedValue.Trim();
                    accountledgername = this.ddlAcctypeCr.SelectedItem.Text;
                }

                if (accountledgerid == "")
                {
                    foreach (GridViewRow gvrow in gvcredit.Rows)
                    {
                        Label lblledgerid = (Label)gvrow.FindControl("lblcrLedgerId");
                        Label lblledgername = (Label)gvrow.FindControl("lblcrledgerName");

                        accountledgerid = lblledgerid.Text.Trim();
                        accountledgername = lblledgername.Text.Trim();

                        break;
                    }
                }
            }

            int P = dtinvoicedetails.Rows.Count - 1;

            while (P >= 0)
            {
                if (dtinvoicedetails.Rows[P]["LedgerId"].ToString().Trim() == accountledgerid)
                {
                    dtinvoicedetails.Rows[P]["AmtPaid"] = 0;
                    dtinvoicedetails.AcceptChanges();
                }
                P--;
            }

            HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoicedetails;
            HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoicedetailsforduplicate;

            decimal paidamount = 0;
            decimal remainingamt = 0;
            decimal textdrcramount = 0;
            int ledgerfoundfromgrid = 0;


            if (this.hdn_accounttype.Value == "0")
            {
                foreach (GridViewRow gvrow in gvdebit.Rows)
                {
                    Label lblledgerid = (Label)gvrow.FindControl("lbldrLedgerId");
                    if (accountledgerid == lblledgerid.Text.Trim())
                    {
                        TextBox txtamount = (TextBox)gvrow.FindControl("txtdramount");
                        textdrcramount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                        ledgerfoundfromgrid = 1;
                        break;
                    }
                }

                if (ledgerfoundfromgrid == 0)
                {
                    textdrcramount = Convert.ToDecimal(string.IsNullOrEmpty(txtAmtDr.Text.Trim()) ? "0" : txtAmtDr.Text.Trim());
                }
            }
            else
            {
                foreach (GridViewRow gvrow in gvcredit.Rows)
                {
                    Label lblledgerid = (Label)gvrow.FindControl("lblcrLedgerId");
                    if (accountledgerid == lblledgerid.Text.Trim())
                    {
                        TextBox txtamount = (TextBox)gvrow.FindControl("txtcramount");
                        textdrcramount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                        ledgerfoundfromgrid = 1;
                        break;
                    }
                }

                if (ledgerfoundfromgrid == 0)
                {
                    textdrcramount = Convert.ToDecimal(string.IsNullOrEmpty(txtAmntCr.Text.Trim()) ? "0" : txtAmntCr.Text.Trim());
                }
            }

            /*new add by p.basu on 27-09-2021*/
            if (dtinvoicedetails.Rows.Count > 0 && dtinvoicedetailsforduplicate == null|| dtinvoicedetailsforduplicate.Rows.Count == 0)
            {
                int P1 = dtinvoicedetails.Rows.Count - 1;

                while (P1 >= 0)
                {
                    if (Convert.ToString(dtinvoicedetails.Rows[P1]["LedgerId"]) != accountledgerid)
                    {
                        dtinvoicedetails.Rows[P1].Delete();
                        dtinvoicedetails.AcceptChanges();
                    }
                    P1--;
                }
                dtinvoicedetailsforduplicate.Merge(dtinvoicedetails, true);

            }
            /*new add by p.basu on 27-09-2021*/


            /*new add for getting row number by p.basu as per sukhdev sir on 23-09-2021*/
            for (int i = 0; i < dtinvoicedetailsforduplicate.Rows.Count; i++)
            {

                
                string chkinvoiceno = "";
                string chkledgerId = "";
                if (grd_InvoiceDetails.Rows.Count <= i)
                {
                    MessageBox1.ShowInfo("<b><font color='green'> Please re-load invoice for this party : " + accountledgername + " for bill tagging </font></b>", 60, 450);
                    return;
                }
                GridViewRow row = grd_InvoiceDetails.Rows[i];

                CheckBox chkBx = (CheckBox)row.FindControl("chkinv");
             
                string ID = Convert.ToString(dtinvoicedetailsforduplicate.Rows[i]["LedgerId"]);
                if (chkBx.Checked && accountledgerid == ID)
                {
                    Label ckinvoiceno = row.FindControl("lblinvoiceno") as Label;
                    Label ledgerId = row.FindControl("lblledgerid") as Label;
                    chkinvoiceno = Convert.ToString(ckinvoiceno.Text);
                    chkledgerId = Convert.ToString(ledgerId.Text);


                    Label lblremainingamt = row.FindControl("lblremainingamtpaid") as Label;
                    Label invoiceno = row.FindControl("lblinvoiceno") as Label;
                    string InvoiceNumber = Convert.ToString(invoiceno.Text);
                    remainingamt = Convert.ToDecimal(lblremainingamt.Text.Trim());

                    /*accountledgerid == chkledgerId && && chkinvoiceno == InvoiceNumber new add for checking*/
                    if (accountledgerid == chkledgerId && Convert.ToDecimal(txtpaidamount.Text.Trim()) != 0 && chkinvoiceno == InvoiceNumber)
                    {
                        TextBox textamtPaid = row.FindControl("txtamtpaid") as TextBox;

                        decimal amtpaid = Convert.ToDecimal(string.IsNullOrEmpty(textamtPaid.Text.Trim()) ? "0" : textamtPaid.Text.Trim());
                        if (amtpaid > remainingamt)
                        {
                            paidamount = 0;
                            MessageBox1.ShowInfo("<b><font color='green'>Paid amount not greater then remaining amount for invoice no: " + invoiceno.Text + "</font></b>", 60, 450);
                            return;
                        }

                        dtinvoicedetailsforduplicate.Rows[i]["AmtPaid"] = amtpaid;
                        dtinvoicedetailsforduplicate.AcceptChanges();

                        paidamount = paidamount + amtpaid;

                    }


                }
                else
                {
                    dtinvoicedetailsforduplicate.Rows[i]["AmtPaid"] = 0;
                    dtinvoicedetailsforduplicate.AcceptChanges();
                }
            }

            if (hdn_accid.Value.ToString() != "")    /* Check and edit time get totalcr amount by D.Mondal on 21/11/2018 */
            {
                if (!string.IsNullOrEmpty(txtcredittotalamount.Text))
                    textdrcramount = Convert.ToDecimal(txtcredittotalamount.Text.Trim());
            }

            /*new add by p.basu on 27-09-2021*/
            foreach(GridViewRow gvrow in gvcredit.Rows)
            {
                Label lblledgerid = (Label)gvrow.FindControl("lblcrLedgerId");
                if (accountledgerid == lblledgerid.Text.Trim())
                {
                    TextBox txtamount = (TextBox)gvrow.FindControl("txtcramount");
                    textdrcramount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                }
            }
            /*new add by p.basu on 27-09-2021*/



            if (paidamount > textdrcramount)
            {
                MessageBox1.ShowInfo("<b>Adjusted amount : " + paidamount + "/- can not be greater than paid/receipt amount " + textdrcramount + "/-</b>", 40, 700);
                ViewState["DrCrAmountEqual"] = "N";
                return;
            }
            else if (paidamount < textdrcramount)
            {
                MessageBox1.ShowWarning("<b>You have not fully adjusted paid/receipt amount " + textdrcramount + "/-</b>", 40, 550);
                ViewState["DrCrAmountEqual"] = "Y";
            }
            else if (paidamount == textdrcramount)
            {
                MessageBox1.ShowWelcome("<b>You have fully adjusted paid/receipt amount " + textdrcramount + "/-<br><br>Thank you..</b>", 40, 550);
                ViewState["DrCrAmountEqual"] = "Y";
            }

            /*merge data from one datatable to another datatable for getting perticular values from grid by pritam basu on 24-09-2021*/

            /*new add by p.basu on 27-09-2021*/
            Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoicedetailsforduplicate;

            /*this loop is used for removing extra data*/
            int P12 = dtinvoicedetails.Rows.Count - 1;
            while (P12 >= 0)
            {
                if (Convert.ToString(dtinvoicedetails.Rows[P12]["LedgerId"]) == accountledgerid)
                {
                    dtinvoicedetails.Rows[P12].Delete();
                    dtinvoicedetails.AcceptChanges();
                }
                P12--;
            }

            dtinvoicedetailsforduplicate.Merge(dtinvoicedetails, true);
            dtinvoicedetails = dtinvoicedetailsforduplicate.Copy();
            /*new add by p.basu on 27-09-2021*/

            Session["INVOICEDETAILS"] = dtinvoicedetails;
            /**********/


            this.txtpaidamount.Text = paidamount.ToString();


            if (this.hdn_accounttype.Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btndebitadd.ClientID + "').focus(); ", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btncreditadd.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btninvoiceclear_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["DrCrAmountEqual"] = "Y";
            DataTable dtinvoicedetails = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];

            foreach (GridViewRow row in grd_InvoiceDetails.Rows)
            {
                CheckBox chk = row.FindControl("chkinv") as CheckBox;
                TextBox txtamtpaid = row.FindControl("txtamtpaid") as TextBox;
                txtamtpaid.Text = "0.00";
                chk.Checked = false;
            }

            foreach (DataRow row1 in dtinvoicedetails.Rows)
            {
                row1["AmtPaid"] = Convert.ToString(0);
                dtinvoicedetails.AcceptChanges();
            }

            HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoicedetails;
            txtpaidamount.Text = "0.00";
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btndebitcredit_invoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["INVOICE_CREDITDEBITDETAILS"] == null)
            {
                this.Create_DebitCreditInvoiceTable();
            }

            DataTable dt_creditdebit = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

            if (grd_debitcredit_invoice.Rows.Count > 0)
            {
                int totalcount = 0;

                foreach (GridViewRow row in grd_debitcredit_invoice.Rows)
                {
                    CheckBox chk = row.FindControl("chkinv") as CheckBox;

                    Label lbldebitcreditledgerid = row.FindControl("lbldebitcreditledgerid") as Label;
                    Label lbldebitcreditledgername = row.FindControl("lbldebitcreditledgername") as Label;
                    Label lbldebitcreditinvoiceid = row.FindControl("lbldebitcreditinvoiceid") as Label;
                    Label lbldebitcreditinvoiceno = row.FindControl("lbldebitcreditinvoiceno") as Label;

                    if (chk.Checked == true)
                    {
                        int NumberofRecord = dt_creditdebit.Select("LedgerId='" + lbldebitcreditledgerid.Text.Trim() + "' AND InvoiceID= '" + lbldebitcreditinvoiceid.Text.Trim() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            // Nothing
                        }
                        else
                        {
                            DataRow dr = dt_creditdebit.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = lbldebitcreditledgerid.Text.Trim();
                            dr["LedgerName"] = lbldebitcreditledgername.Text.Trim();
                            dr["InvoiceID"] = lbldebitcreditinvoiceid.Text.Trim();
                            dr["InvoiceNo"] = lbldebitcreditinvoiceno.Text.Trim();
                            dr["TAG"] = "T";

                            dt_creditdebit.Rows.Add(dr);
                            dt_creditdebit.AcceptChanges();

                            Session["INVOICE_CREDITDEBITDETAILS"] = dt_creditdebit;
                        }
                    }
                    else
                    {
                        dt_creditdebit = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];
                        int i = dt_creditdebit.Rows.Count - 1;

                        while (i >= 0)
                        {
                            if (Convert.ToString(dt_creditdebit.Rows[i]["LedgerId"]) == lbldebitcreditledgerid.Text.Trim() && Convert.ToString(dt_creditdebit.Rows[i]["InvoiceID"]) == lbldebitcreditinvoiceid.Text.Trim())
                            {
                                dt_creditdebit.Rows[i].Delete();
                                dt_creditdebit.AcceptChanges();
                            }
                            i--;
                        }
                        HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"] = dt_creditdebit;
                    }
                    totalcount = totalcount + 1;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>No invoice found.!</font></b>");
                this.popupinvoicedebitcredit.Show();
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }

    }


    #region grd_InvoiceDetails_RowDataBound
    protected void grd_InvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grd_InvoiceDetails.Columns[2].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[3].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[4].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[5].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[6].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[7].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[8].ItemStyle.Wrap = false;
                grd_InvoiceDetails.Columns[9].ItemStyle.Wrap = false;

                grd_InvoiceDetails.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[4].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[5].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                grd_InvoiceDetails.Columns[4].HeaderStyle.Wrap = true;
                grd_InvoiceDetails.Columns[5].HeaderStyle.Wrap = true;
                grd_InvoiceDetails.Columns[6].HeaderStyle.Wrap = true;
                grd_InvoiceDetails.Columns[7].HeaderStyle.Wrap = true;
                grd_InvoiceDetails.Columns[8].HeaderStyle.Wrap = true;
                grd_InvoiceDetails.Columns[9].HeaderStyle.Wrap = true;

                grd_InvoiceDetails.Columns[6].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[7].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[8].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[9].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[10].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                grd_InvoiceDetails.Columns[11].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                grd_InvoiceDetails.Columns[12].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;  /* Add Column for Return Value Change By D.Mondal on 11/10/2018 */
                grd_InvoiceDetails.Columns[13].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;  /* Change Position 12 to 13 Change By D.Mondal on 11/10/2018 */
                grd_InvoiceDetails.Columns[14].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;  /* Change Position 12 to 13 Change By D.Mondal on 11/10/2018 */




                grd_InvoiceDetails.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[9].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grd_InvoiceDetails.Columns[10].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grd_InvoiceDetails.Columns[11].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grd_InvoiceDetails.Columns[12].ItemStyle.HorizontalAlign = HorizontalAlign.Right; /* Add Column for Return Value Change By D.Mondal on 11/10/2018 */
                grd_InvoiceDetails.Columns[13].ItemStyle.HorizontalAlign = HorizontalAlign.Right; /* Change Position 12 to 13 Change By D.Mondal on 11/10/2018 */
                grd_InvoiceDetails.Columns[14].ItemStyle.HorizontalAlign = HorizontalAlign.Right; /* Change Position 12 to 13 Change By D.Mondal on 11/10/2018 */




                txtpaidamount.Attributes.Add("onkeypress", "return isNumberKey('" + txtpaidamount.ClientID + "')");

                CheckBox chk = e.Row.FindControl("chkinv") as CheckBox;
                TextBox txtamtpaid = e.Row.FindControl("txtamtpaid") as TextBox;

                if (Convert.ToDecimal(txtamtpaid.Text.Trim()) == 0)
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion


    protected void btncreditgrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            TextBox txtcreditamount = (TextBox)gvr.FindControl("txtcramount");
            Label lbldrLedgerId = (Label)gvr.FindControl("lblcrLedgerId");
            Label lblcrguid = (Label)gvr.FindControl("lblcrguid");

            this.hdn_creditamount.Value = Convert.ToString(string.IsNullOrEmpty(txtcreditamount.Text.Trim()) ? "0" : txtcreditamount.Text.Trim());
            this.hdn_creditguid.Value = lblcrguid.Text;
            this.hdn_ledgercostcenter.Value = lbldrLedgerId.Text;


            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            string guid = hdn_creditguid.Value;
            decimal creditamount = Convert.ToDecimal(hdn_creditamount.Value);
            int flag = 0;
            decimal totalcreditamount = 0;
            totalcreditamount = Convert.ToDecimal(this.txtcredittotalamount.Text.Trim());

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableGuidCheck(guid);
                if (flag == 1)
                {
                    dtvoucher.DefaultView.RowFilter = "TxnType = '1'";

                    this.gvcredit.DataSource = dtvoucher;
                    this.gvcredit.DataBind();

                    totalcreditamount = totalcreditamount - creditamount;
                    this.txtcredittotalamount.Text = Convert.ToString(totalcreditamount);

                    MessageBox1.ShowSuccess("<b><font color='green'>Record deleted successfully!</font></b>");

                    this.CostCenterDelete(hdn_ledgercostcenter.Value.Trim(), 'N');
                    this.InvoicetableDelete(hdn_ledgercostcenter.Value.Trim());
                    /*  by D.Mondal On 26/12/2018 */
                    //this.AutoNarration(this.ddlvouchertype.SelectedValue);
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Record deleted unsuccessful!</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>No records found!</font></b>");
            }

        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btndebitgrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            TextBox txtdebitamount = (TextBox)gvr.FindControl("txtdramount");
            Label lbldrLedgerId = (Label)gvr.FindControl("lbldrLedgerId");
            Label lbldrguid = (Label)gvr.FindControl("lbldrguid");

            this.hdn_debitamount.Value = Convert.ToString(string.IsNullOrEmpty(txtdebitamount.Text.Trim()) ? "0" : txtdebitamount.Text.Trim());
            this.hdn_debitguid.Value = lbldrguid.Text;
            this.hdn_ledgercostcenter.Value = lbldrLedgerId.Text;


            DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            string guid = hdn_debitguid.Value;
            decimal debitamount = Convert.ToDecimal(hdn_debitamount.Value.ToString());
            int flag = 0;
            decimal totaldebitamount = 0;
            totaldebitamount = Convert.ToDecimal(this.txtdebittotalamount.Text.Trim());

            if (dtvoucher.Rows.Count > 0)
            {
                flag = DatatableGuidCheck(guid);
            }

            if (flag == 1)
            {
                dtvoucher.DefaultView.RowFilter = "TxnType = '0'";

                this.gvdebit.DataSource = dtvoucher;
                this.gvdebit.DataBind();

                totaldebitamount = totaldebitamount - debitamount;
                this.txtdebittotalamount.Text = Convert.ToString(totaldebitamount);

                MessageBox1.ShowSuccess("Record deleted successfully!");

                this.CostCenterDelete(hdn_ledgercostcenter.Value.Trim(), 'N');
                this.InvoicetableDelete(hdn_ledgercostcenter.Value.Trim());
                this.AutoNarration(this.ddlvouchertype.SelectedValue);
            }
            else
            {
                MessageBox1.ShowError("Record deleted unsuccessful!");
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void CostCenterDelete(string Ledger_or_GUID, char FromPopUp)
    {
        try
        {
            DataTable dtcostcenter = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

            foreach (GridViewRow gvrow in grdcostcenter.Rows)
            {
                Label lblCostcenterGUID = (Label)gvrow.FindControl("lblCostcenterGUID");
                Label lblledgerid = (Label)gvrow.FindControl("lblledgerid");
                TextBox txtamount = (TextBox)gvrow.FindControl("txtamount");
                decimal Costamount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());

                foreach (DataRow row in dtcostcenter.Rows)
                {
                    if ((Convert.ToString(row["GUID"]).Trim() == lblCostcenterGUID.Text.Trim()) && (Convert.ToString(row["LedgerId"]).Trim() == this.hdn_ledgercostcenter.Value.Trim()))
                    {
                        row["amount"] = Convert.ToString(Costamount);
                        dtcostcenter.AcceptChanges();
                    }
                }
            }
            if (dtcostcenter != null)
            {
                int flag = 0;
                if (dtcostcenter.Rows.Count > 0)
                {
                    flag = DatatableCostCenterCheck(Ledger_or_GUID, FromPopUp);
                    if (flag == 1)
                    {
                        this.grdcostcenter.DataSource = dtcostcenter;
                        this.grdcostcenter.DataBind();
                        CostcenterFooter(ref dtcostcenter, this.hdn_ledgercostcenter.Value.Trim());

                        this.MessageBox1.ShowSuccess("<b><font color='green'>Record deleted successfully!</font></b>");

                        if (FromPopUp == 'Y')
                        {
                            this.Popupcostcenter.Show();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btncostcenterdelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblledgerid = (Label)gvr.FindControl("lblledgerid");
            Label lblCostcenterGUID = (Label)gvr.FindControl("lblCostcenterGUID");

            this.CostCenterDelete(lblCostcenterGUID.Text.Trim(), 'Y');

            if (this.hdn_btncostcenterclick.Value == "Y")
            {
                this.CostCenterTagChaged(this.hdn_accounttype.Value.Trim(), this.hdn_accountid.Value.Trim(), Convert.ToDecimal(this.lblcostcenteramountshow.Text));
            }
            this.Popupcostcenter.Show();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            btngstonly.Visible = true;
            ddlVoucher.Enabled = false;
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblAccEntryID = (Label)gvr.FindControl("lblAccEntryID");
            Label lblVoucherApproved = (Label)gvr.FindControl("lblVoucherApproved");
            Label lblVoucherDayEnd = (Label)gvr.FindControl("lblVoucherDayEnd");

            this.hdn_accid.Value = lblAccEntryID.Text.Trim();
            this.hdn_approved.Value = lblVoucherApproved.Text.Trim();
            this.hdn_dayend.Value = lblVoucherDayEnd.Text.Trim();

            ViewState["AccType"] = "Normal";
            this.divbtnpaidamount.Style["display"] = "";  /* add by D.Mondal on 27/10/2018 */
            //this.btndivsave.Visible = true; /* add this line by D.Mondal on 27/10/2018 */
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.CreateVoucherTable();
            this.CreateInvoiceTable();
            this.Create_DebitCreditInvoiceTable();
            DataTable dtVoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            DataTable dtinvoice = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
            //DataTable dtinvoiceDuplicate = (DataTable)HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"];
            DataTable dtdebitcredit = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

            string voucherid = hdn_accid.Value.ToString();
            string approved = hdn_approved.Value.ToString();
            int Invoicetagledgercount = 0;

            DataSet ds = new DataSet();
            ds = clsVoucher.VoucherDetails(voucherid);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.BindVoucherType();
                    if (ds.Tables[0].Rows[0]["VOUCHERTYPE"].ToString().Trim() != "0")
                    {
                        btngstonly.Visible = true;
                        this.ddlVoucher.SelectedValue = ds.Tables[0].Rows[0]["VOUCHERTYPE"].ToString().Trim();
                        this.txtInvoiceNo.Text = ds.Tables[0].Rows[0]["INVOICENO"].ToString().Trim();
                        this.BindState();
                        this.ddlPlaceofSupply.SelectedValue = ds.Tables[0].Rows[0]["PLACEOFSUPPLY"].ToString();
                        this.txtGstNo.Text = ds.Tables[0].Rows[0]["GSTNO"].ToString().Trim();
                        this.BindParty();
                        this.ddlPartyName.SelectedValue = ds.Tables[0].Rows[0]["PARTYID"].ToString();
                        this.txtInvociedate.Text = ds.Tables[0].Rows[0]["INVDATE"].ToString().Trim();
                        this.txtTaxPercent.Text = ds.Tables[0].Rows[0]["TAXPERCENT"].ToString().Trim();
                        this.txtTaxValue.Text = ds.Tables[0].Rows[0]["TAXVALUE"].ToString().Trim();
                        this.txtHsn.Text = ds.Tables[0].Rows[0]["HSNCODE"].ToString().Trim();
                    }

                    this.ddlVoucher.SelectedValue = ds.Tables[0].Rows[0]["VOUCHERTYPE"].ToString().Trim();
                    this.txtvoucherno.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString().Trim();
                    this.trvoucherno.Style["display"] = "";
                    this.ddlvouchertype.SelectedValue = ds.Tables[0].Rows[0]["VoucherTypeID"].ToString().Trim();
                    //this.ddlvouchertype.Enabled = false;

                    if (this.ddlvouchertype.SelectedValue == "2")
                    {
                        this.divbtnotherdetails.Style["display"] = "";
                    }
                    else
                    {
                        this.divbtnotherdetails.Style["display"] = "none";
                    }

                    this.ddlregion.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString().Trim();
                    this.ddlregion.Enabled = false;
                    this.txtvoucherdate.Text = ds.Tables[0].Rows[0]["Date"].ToString().Trim();
                    //this.imgPopuppodate.Visible = false;
                    this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                    this.ddlregion.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["Mode"].ToString().Trim() != "")
                    {
                        this.ddlMode.SelectedValue = ds.Tables[0].Rows[0]["Mode"].ToString().Trim();
                    }

                    this.txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString().Trim();
                    this.txtCheckerNote.Text = ds.Tables[0].Rows[0]["RejectionNote"].ToString().Trim();

                    if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16")
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        this.btnaddhide.Style["display"] = "none";
                        this.innertr.Style["display"] = "";
                        this.trpaymentmode.Style["display"] = "";
                        this.trdebitbank.Style["display"] = "";
                        this.trdebitchequeno.Style["display"] = "";
                        this.trcreditbank.Style["display"] = "";
                        this.trcreditchequeno.Style["display"] = "";
                        this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                        this.BindMode();
                    }
                    else
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        this.btnaddhide.Style["display"] = "none";
                        this.trdebitbank.Style["display"] = "none";
                        this.trdebitchequeno.Style["display"] = "none";
                        this.trcreditbank.Style["display"] = "none";
                        this.trcreditchequeno.Style["display"] = "none";
                        this.innertr.Style["display"] = "";
                        this.trpaymentmode.Style["display"] = "none";
                        this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                        this.BindMode();
                    }

                    if (this.ddlvouchertype.SelectedValue.Trim() == "13")
                    {
                        this.gvdebit.Columns[9].Visible = false;
                        this.gvcredit.Columns[9].Visible = true;
                    }
                    else if (this.ddlvouchertype.SelectedValue.Trim() == "14")
                    {
                        this.gvdebit.Columns[9].Visible = true;
                        this.gvcredit.Columns[9].Visible = false;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            DataRow dr = dtVoucher.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                            dr["LedgerName"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                            dr["TxnType"] = Convert.ToString(ds.Tables[1].Rows[i]["TxnType"]).Trim();
                            dr["Amount"] = Convert.ToString(ds.Tables[1].Rows[i]["Amount"]).Trim();
                            dr["BankID"] = Convert.ToString(ds.Tables[1].Rows[i]["BankID"]);
                            dr["BankName"] = Convert.ToString(ds.Tables[1].Rows[i]["BankName"]);
                            dr["PAYMENTTYPEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPEID"]);
                            dr["PAYMENTTYPENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPENAME"]);
                            dr["ChequeNo"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeNo"]);
                            dr["ChequeDate"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeDate"]);
                            dr["IsChequeRealised"] = Convert.ToString(ds.Tables[1].Rows[i]["IsChequeRealised"]);
                            dr["Remarks"] = Convert.ToString(ds.Tables[1].Rows[i]["Remarks"]);
                            dr["DeductableAmount"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableAmount"]);
                            dr["DeductablePercentage"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductablePercentage"]);
                            dr["DeductableLedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableLedgerId"]);
                            dr["IsCostCenter"] = Convert.ToString(ds.Tables[1].Rows[i]["IsCostCenter"]);
                            dr["IsTagInvoice"] = Convert.ToString(ds.Tables[1].Rows[i]["IsTagInvoice"]);

                            dtVoucher.Rows.Add(dr);
                            dtVoucher.AcceptChanges();

                            if (Invoicetagledgercount == 0)     /*---- if count set flag 1 then break ----*/
                            {
                                if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "15")
                                {
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), "9", this.ddlregion.SelectedValue);
                                }
                                else if (this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "16")
                                {
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), "10", this.ddlregion.SelectedValue);
                                }
                                else
                                {
                                    /* Other Voucher will take care in SP please check */
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.hdn_ledgercostcenter.Value = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                                    this.hdn_ledgername.Value = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                                    this.lblledgername.Text = Convert.ToString(this.hdn_ledgername.Value).Trim();
                                    Invoicetagledgercount = 1;
                                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                                    {
                                        DataRow dr1 = dtinvoice.NewRow();
                                        dr1["GUID"] = Guid.NewGuid();
                                        dr1["LedgerId"] = dt.Rows[counter]["LedgerId"].ToString().Trim();
                                        dr1["LedgerName"] = dt.Rows[counter]["name"].ToString().Trim();
                                        dr1["InvoiceID"] = dt.Rows[counter]["InvoiceID"].ToString().Trim();
                                        dr1["InvoiceNo"] = dt.Rows[counter]["InvoiceNo"].ToString().Trim();
                                        dr1["InvoiceDate"] = dt.Rows[counter]["InvoiceDate"].ToString().Trim();
                                        dr1["InvoiceOthers"] = dt.Rows[counter]["InvoiceOthers"].ToString().Trim();
                                        dr1["InvoiceBranchID"] = dt.Rows[counter]["InvoiceBranchID"].ToString().Trim();
                                        dr1["InvoiceBranchName"] = dt.Rows[counter]["InvoiceBranchName"].ToString().Trim();
                                        dr1["VoucherType"] = dt.Rows[counter]["VoucherType"].ToString().Trim();
                                        dr1["BranchID"] = dt.Rows[counter]["BranchID"].ToString().Trim();
                                        dr1["InvoiceAmt"] = dt.Rows[counter]["InvoiceAmt"].ToString().Trim();
                                        dr1["AlreadyAmtPaid"] = dt.Rows[counter]["AlreadyAmtPaid"].ToString().Trim();
                                        dr1["ReturnAmt"] = dt.Rows[counter]["ReturnAmt"].ToString().Trim();   /* Add ReturnAmt by D.Mondal on 11/10/2018  */
                                        dr1["RemainingAmtPaid"] = dt.Rows[counter]["RemainingAmt"].ToString().Trim();
                                        dr1["AmtPaid"] = dt.Rows[counter]["AmtPaid"].ToString().Trim();
                                        dr1["Type"] = dt.Rows[counter]["Type"].ToString().Trim();

                                        dtinvoice.Rows.Add(dr1);
                                        dtinvoice.AcceptChanges();
                                    }
                                    HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoice;
                                    //HttpContext.Current.Session["INVOICEDETAILS_FOR_DUPLICATE"] = dtinvoice;
                                }
                            }
                        }

                        HttpContext.Current.Session["VOUCHERDETAILS"] = dtVoucher;

                        //------------------ For Debit Portion -----------------//
                        dtVoucher.DefaultView.RowFilter = "TxnType = '0'";
                        decimal sumdebit = (decimal)dtVoucher.Compute("Sum(Amount)", "TxnType = '0'");
                        this.txtdebittotalamount.Text = sumdebit.ToString();

                        this.gvdebit.DataSource = dtVoucher;
                        this.gvdebit.DataBind();
                        //------------------------------------------------------//

                        //this.InvoiceDetailsBasedonLeadger("N");

                        //------------------ For Credit Portion -----------------//
                        dtVoucher.DefaultView.RowFilter = "TxnType = '1'";
                        decimal sumcredit = (decimal)dtVoucher.Compute("Sum(Amount)", "TxnType = '1'");
                        this.txtcredittotalamount.Text = sumcredit.ToString();

                        this.gvcredit.DataSource = dtVoucher;
                        this.gvcredit.DataBind();
                        //-------------------------------------------------------//

                        //this.InvoiceDetailsBasedonLeadger("N");

                        //------------------ For Invoice Details Portion -----------------//
                        //dtinvoice.DefaultView.RowFilter = "TxnType = '1'";
                        //decimal sumpaidamount = (decimal)dtinvoice.Compute("Sum(AmtPaid)", "");
                        //this.txtpaidamount.Text = sumpaidamount.ToString();

                        decimal paidamount = 0;

                        for (int i = 0; i < dtinvoice.Rows.Count; i++)
                        {
                            paidamount = paidamount + Convert.ToDecimal(dtinvoice.Rows[i]["AmtPaid"].ToString().Trim());
                        }

                        this.txtpaidamount.Text = paidamount.ToString();


                        if (dtinvoice.Rows.Count > 0)
                        {
                            this.grd_InvoiceDetails.DataSource = dtinvoice;
                            this.grd_InvoiceDetails.DataBind();
                            this.trinvoicedetails.Style["display"] = "";
                        }
                        else
                        {
                            this.grd_InvoiceDetails.DataSource = null;
                            this.grd_InvoiceDetails.DataBind();
                            this.trinvoicedetails.Style["display"] = "none";
                        }

                        //-------------------------------------------------------//

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            this.CreateCostCenterTable();
                            DataTable dtcost = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                DataRow dr = dtcost.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerId"]);
                                dr["LedgerName"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerName"]);
                                dr["CostCatagoryID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryID"]);
                                dr["CostCatagoryName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryName"]);
                                dr["CostCenterID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterID"]);
                                dr["CostCenterName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterName"]);
                                dr["BranchID"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchID"]);
                                dr["BranchName"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchName"]);
                                dr["amount"] = Convert.ToString(ds.Tables[2].Rows[i]["amount"]);
                                dr["BrandID"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandID"]);
                                dr["BrandName"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandName"]);
                                dr["ProductID"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductID"]);
                                dr["ProductName"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductName"]);
                                dr["DepartmentID"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentID"]);
                                dr["DepartmentName"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentName"]);
                                dr["FromDate"] = Convert.ToString(ds.Tables[2].Rows[i]["FromDate"]);
                                dr["ToDate"] = Convert.ToString(ds.Tables[2].Rows[i]["ToDate"]);
                                dr["Narration"] = Convert.ToString(ds.Tables[2].Rows[i]["NARRATION"]);
                                dr["TxnType"] = Convert.ToString(ds.Tables[2].Rows[i]["TxnType"]);

                                dtcost.Rows.Add(dr);
                                dtcost.AcceptChanges();
                            }
                            this.grdcostcenter.DataSource = dtcost;
                            this.grdcostcenter.DataBind();
                            HttpContext.Current.Session["COSTCENTERDETAILS"] = dtcost;
                        }

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            DataTable dtdebitinvoice = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

                            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                            {
                                DataRow dr = dtdebitinvoice.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(ds.Tables[4].Rows[i]["LedgerId"]);
                                dr["LedgerName"] = Convert.ToString(ds.Tables[4].Rows[i]["LedgerName"]);
                                dr["InvoiceID"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceID"]);
                                dr["InvoiceNo"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceNo"]);
                                dr["InvoiceAmt"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceAmt"]);
                                dr["TAG"] = Convert.ToString(ds.Tables[4].Rows[i]["TAG"]);

                                dtdebitinvoice.Rows.Add(dr);
                                dtdebitinvoice.AcceptChanges();
                            }

                            HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"] = dtdebitinvoice;
                        }

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            this.txtbillno.Text = Convert.ToString(ds.Tables[3].Rows[0]["BillNo"]);
                            this.txtbilldate.Text = Convert.ToString(ds.Tables[3].Rows[0]["BillDate"]);
                            this.txtgrno.Text = Convert.ToString(ds.Tables[3].Rows[0]["GRNo"]);
                            this.txtgrdate.Text = Convert.ToString(ds.Tables[3].Rows[0]["GRDate"]);
                            this.txtvehicleno.Text = Convert.ToString(ds.Tables[3].Rows[0]["VehicleNo"]);
                            this.txttransport.Text = Convert.ToString(ds.Tables[3].Rows[0]["Transport"]);
                            this.txtwaybillno.Text = Convert.ToString(ds.Tables[3].Rows[0]["WayBillNo"]);
                            this.txtwaybilldate.Text = Convert.ToString(ds.Tables[3].Rows[0]["WayBillDate"]);
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Voucher details not found!</font></b>");
                        return;
                    }

                    this.ControlApproval();                     // For A/c Approval
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Voucher details not found!</b>");
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>No records found!</b>");
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btngrdvoucherdelete_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblAccEntryID = (Label)gvr.FindControl("lblAccEntryID");
            Label lblVoucherApproved = (Label)gvr.FindControl("lblVoucherApproved");
            Label lblVoucherDayEnd = (Label)gvr.FindControl("lblVoucherDayEnd");

            this.hdn_accid.Value = lblAccEntryID.Text.Trim();
            this.hdn_approved.Value = lblVoucherApproved.Text.Trim();
            this.hdn_dayend.Value = lblVoucherDayEnd.Text.Trim();

            string ip_address = Request.UserHostAddress.ToString().Trim();

            if (this.hdn_dayend.Value.Trim() == "Yes")
            {
                MessageBox1.ShowInfo("<b><font color='blue'>Voucher status dayend, you can't <font color='red'>delete</font> this!</font></b>", 60, 450);
            }
            else if (this.hdn_approved.Value.Trim() == "No" || (this.hdn_approved.Value.Trim() == "Reject"))
            {
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                /* Add logic : when delete the record then insert in backup history  By D.Mondal on 26/11/2018 */
                int deleteflag = clsVoucher.VoucherDelete(this.hdn_accid.Value.Trim(), this.Session["IUSERID"].ToString().Trim(), ip_address);
                if (deleteflag > 0)
                {
                    MessageBox1.ShowSuccess("<b><font color='green'>Voucher deleted successfully!</font></b>");
                    this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(), this.ddldepot.SelectedValue, "");
                }
                else
                {
                    MessageBox1.ShowInfo("<b><font color='green'> This Voucher may be assign with Proforma Invoice or delete unsuccessful</font></b>", 60, 450);
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='blue'>Voucher status apporved, you can't <font color='red'>delete</font> this!</font></b>", 60, 450);
            }

            this.hdn_accid.Value = "";
            this.hdn_approved.Value = "";
            this.hdn_dayend.Value = "";
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 600);
        }
    }

    protected void btnvouchersearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(), this.ddldepot.SelectedValue, "");
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    #region imgrejectionbtn_click
    protected void imgrejectionbtn_click(object sender, EventArgs e)
    {
        try
        {
            if (this.hdn_accounttype.Value == "0")
            {
                decimal amount = string.IsNullOrEmpty(this.txtAmtDr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmtDr.Text.Trim());
                this.CostCenterApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), amount, 'N');
                //this.ddlAccTypeDr.SelectedValue = "0";
                //this.lbldebitoutstanding.Text = "0.00";
            }
            else
            {
                decimal amount = string.IsNullOrEmpty(this.txtAmntCr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmntCr.Text.Trim());
                this.CostCenterApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), amount, 'N');
                //this.ddlAcctypeCr.SelectedValue = "0";
                //this.lblcreditoutstanding.Text = "0.00";
            }
            this.pnlAdd.Style["display"] = "";
            this.divbtnpaidamount.Style["display"] = "";
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region imgrejectionotherdetailsbtn_click
    protected void imgrejectionotherdetailsbtn_click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "";
            this.divbtnpaidamount.Style["display"] = "";
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region imgrejectioncostcenterbtn_click
    protected void imgrejectioncostcenterbtn_click(object sender, EventArgs e)
    {
        try
        {
            if (this.hdn_accounttype.Value == "0")
            {
                if (this.ddlAccTypeDr.SelectedValue != "0")
                {
                    decimal amount = string.IsNullOrEmpty(this.txtAmtDr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmtDr.Text.Trim());
                    this.TaxApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", amount, this.ddlregion.SelectedValue);
                }
            }
            else
            {
                if (this.ddlAccTypeDr.SelectedValue != "0")
                {
                    decimal amount = string.IsNullOrEmpty(this.txtAmntCr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmntCr.Text.Trim());
                    this.TaxApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", amount, this.ddlregion.SelectedValue);
                }
            }

            this.pnlAdd.Style["display"] = "";
            this.divbtnpaidamount.Style["display"] = "";
            this.hdn_btncostcenterclick.Value = "N";
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region btndebitgrdinvoice_Click
    public void btndebitgrdinvoice_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lbldrledgername = (Label)gvr.FindControl("lbldrledgername");
            Label lbldrLedgerId = (Label)gvr.FindControl("lbldrLedgerId");

            this.hdn_ledgername.Value = lbldrledgername.Text.Trim();
            this.hdn_ledgercostcenter.Value = lbldrLedgerId.Text;

            this.InvoiceDetailsBasedonLeadger(this.hdn_ledgercostcenter.Value.Trim(), this.hdn_ledgername.Value.Trim(), "0", "Y", this.ddlregion.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region btncreditgrdinvoice_Click
    public void btncreditgrdinvoice_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblcrledgerName = (Label)gvr.FindControl("lblcrledgerName");
            Label lblcrLedgerId = (Label)gvr.FindControl("lblcrLedgerId");

            this.hdn_ledgername.Value = lblcrledgerName.Text.Trim();
            this.hdn_ledgercostcenter.Value = lblcrLedgerId.Text.Trim();
            this.ddlAcctypeCr.SelectedValue = this.hdn_ledgercostcenter.Value;

            this.InvoiceDetailsBasedonLeadger(this.hdn_ledgercostcenter.Value.Trim(), this.hdn_ledgername.Value.Trim(), "1", "Y", this.ddlregion.SelectedValue);
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region rdbcreditpaymenttype_SelectedIndexChanged
    protected void rdbcreditpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

            if (rdbcreditpaymenttype.SelectedValue.Trim() == "1")
            {
                this.tdtxtBox.Visible = false;
                this.tdDropdown.Visible = true;
                this.lblcreditpaymentno.Text = "Cheque No";
                this.lblcreditpaymentdate.Text = "Cheque Date";
            }
            else if (rdbcreditpaymenttype.SelectedValue.Trim() == "2")
            {
                this.tdDropdown.Visible = false;
                this.tdtxtBox.Visible = true;
                this.lblNftNo.Text = "NEFT No";
                this.lblcreditpaymentdate.Text = "NEFT Date";
            }
            else if (rdbcreditpaymenttype.SelectedValue.Trim() == "3")
            {
                this.tdDropdown.Visible = false;
                this.tdtxtBox.Visible = true;
                this.lblNftNo.Text = "RTGS No";
                this.lblcreditpaymentdate.Text = "RTGS Date";
            }
            else if (rdbcreditpaymenttype.SelectedValue.Trim() == "4")
            {
                this.tdDropdown.Visible = false;
                this.tdtxtBox.Visible = true; ;
                this.lblNftNo.Text = "ONLINE No";
                this.lblcreditpaymentdate.Text = "ONLINE Date";
            }
            else
            {
                this.tdtxtBox.Visible = true;
                this.tdDropdown.Visible = false;
                this.lblcreditpaymentno.Text = "No";
                this.lblcreditpaymentdate.Text = "Date";
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region rdbdebitpaymenttype_SelectedIndexChanged
    protected void rdbdebitpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

            if (rdbdebitpaymenttype.SelectedValue.Trim() == "1")
            {
                this.lbldebitpaymentno.Text = "Chque No";
                this.lbldebitpaymentdate.Text = "Cheque Date";
            }
            else if (rdbdebitpaymenttype.SelectedValue.Trim() == "2")
            {
                this.lbldebitpaymentno.Text = "NEFT No";
                this.lbldebitpaymentdate.Text = "NEFT Date";
            }
            else if (rdbdebitpaymenttype.SelectedValue.Trim() == "3")
            {
                this.lbldebitpaymentno.Text = "RTGS No";
                this.lbldebitpaymentdate.Text = "RTGS Date";
            }
            else if (rdbdebitpaymenttype.SelectedValue.Trim() == "4")
            {
                this.lbldebitpaymentno.Text = "DD No";
                this.lbldebitpaymentdate.Text = "DD Date";
            }
            else
            {
                this.lbldebitpaymentno.Text = "No";
                this.lbldebitpaymentdate.Text = "Date";
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    #region btnApprove_Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            int flag = 0;


            if (this.ddlvouchertype.SelectedValue == "16")
            {
                string status = "0";
                status = clsVoucher.DepotFlag(this.ddlregion.SelectedValue.Trim());

                if (status == "1")
                {
                    //if (!string.IsNullOrEmpty(this.txtRealisedno.Text.Trim()) && !string.IsNullOrEmpty(this.txtRealisedDate.Text.Trim()))
                    //{
                    ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
                    flag = clsPurchaseStockReceipt.ApproveBankReceipt(this.hdn_accid.Value.ToString(), Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.txtvoucherno.Text.Trim(), this.txtRealisedDate.Text.Trim(), this.txtRealisedno.Text.Trim(), this.ddlregion.SelectedValue.Trim(), this.ddlregion.SelectedItem.ToString().Trim());
                    //}
                    //else
                    //{
                    //    MessageBox1.ShowInfo("<b>Please enter release Details Information!</b>",40,400);
                    //    return;
                    //}
                }

            }

            flag = clsVoucher.VoucherApproved(this.hdn_accid.Value.ToString(), this.ddlvouchertype.SelectedValue.Trim());
            if (flag > 0)
            {
                MessageBox1.ShowSuccess("<b><font color='green'>Account's approved successfully</font></b>");
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.trvoucherno.Style["display"] = "none";
                this.ClearData();
                this.ClearGridWithSession();
                this.BindVoucherDetails(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim(), this.Session["IUSERID"].ToString().Trim(), this.Session["FINYEAR"].ToString(), this.Request.QueryString["VOUCHERID"].ToString().Trim(), this.ddldepot.SelectedValue, "");
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>Account's approved unsuccessful!</font></b>");
                this.txtRealisedno.Text = "";
                this.txtRealisedDate.Text = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ControlApproval
    public void ControlApproval()
    {
        string approved = this.hdn_approved.Value.ToString();
        string dayend = this.hdn_dayend.Value.ToString();


        if (approved == "Reject" && (this.Request.QueryString["CHECKER"].ToString().Trim() == "FALSE"))
        {
            this.gvdebit.Columns[13].Visible = false;
            this.gvdebit.Columns[14].Visible = true;  // DELETE BUTTON SHOW 
            this.gvcredit.Columns[11].Visible = true; // DELETE BUTTON SHOW
            this.gvcredit.Enabled = true;
            this.gvdebit.Enabled = true;
            this.imgPopuppodate.Visible = true;
            this.tdcheckernote.Style["display"] = "";
        }
        else if (approved == "Yes" || dayend == "Yes" || (this.Request.QueryString["CHECKER"].ToString().Trim() == "TRUE"))
        {
            this.trcreditbtn.Style["display"] = "none";
            this.trdebitbtn.Style["display"] = "none";
            //this.btndivsave.Visible = false;
            this.btndivsave.Visible = true;
            this.divsavewithprint.Visible = false;
            this.traccountinfocredit.Visible = false;
            this.traccountinfodebit.Visible = false;
            //this.txtNarration.Enabled = false;
            this.txtNarration.Enabled = true;
            this.ddlMode.Enabled = false;
            this.gvdebit.Columns[14].Visible = false;
            this.gvcredit.Columns[11].Visible = false;
            this.trdebitchequeno.Style["display"] = "none";
            this.trcreditchequeno.Style["display"] = "none";
            /*this.divbtnpaidamount.Style["display"] = "none";         */   // Btn add paid amount
            this.divbtnpaidamount.Style["display"] = "";
            this.gvcredit.Enabled = true;
            this.gvdebit.Enabled = true;
            this.imgPopuppodate.Visible = false;
            this.tdcheckernote.Style["display"] = "none";
            this.btnSave.Text = "Save Bill Taging";    /*add by D.Mondal on 27/10/2018 */
            ViewState["OnlyBillTagFromPage"] = "Y";  /* add by D.Mondal on 27/10/2018 */


        }
        else
        {
            //if (this.ddlvouchertype.SelectedValue.Trim() == "13" || this.ddlvouchertype.SelectedValue.Trim() == "14")
            //{
            //    this.trinvoicedetails.Style["display"] = "none";
            //}
            this.gvdebit.Columns[13].Visible = false;
            this.gvdebit.Columns[14].Visible = true;  // DELETE BUTTON SHOW 
            this.gvcredit.Columns[11].Visible = true; // DELETE BUTTON SHOW 
            this.gvcredit.Enabled = true;
            this.gvdebit.Enabled = true;
            this.imgPopuppodate.Visible = true;
            this.tdcheckernote.Style["display"] = "none";
        }
        if ((this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16") && approved == "No" && dayend == "No")
        {
            this.btnSave.Text = "Save";    /*add by D.Mondal on 20/11/2018 */
            btndivsave.Visible = true;
        }
        else if ((this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16") && approved == "Yes" && dayend == "No")
        {
            this.btnSave.Text = "Save Bill Tagging";    /*add by D.Mondal on 20/11/2018 */
            btndivsave.Visible = true;
        }
        else if ((this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16") && approved == "Yes" && dayend == "No")
        {
            this.btnSave.Text = "Save Bill Tagging";    /*add by D.Mondal on 20/11/2018 */
            btndivsave.Visible = true;
        }
        else if ((this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16") && approved == "No" && dayend == "Yes")
        {
            btndivsave.Visible = false;    /*add by D.Mondal on 20/11/2018 */
        }

        /* Add for Isautovoucher yes or No By D.Mondal on 04/01/2018  */
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        int Flag = clsVoucher.CheckIsAutovoucher(this.hdn_accid.Value);
        if (Flag > 0)
        {
            this.btndivsave.Visible = false;
            this.divsavewithprint.Visible = false;
        }
        else
        {
            this.btndivsave.Visible = true;
            this.divsavewithprint.Visible = true;
        }
    }
    #endregion

    #region btnotherdetailssave_Click
    protected void btnotherdetailssave_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "";
            this.divbtnpaidamount.Style["display"] = "";
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    public void OtherDetailsReset()
    {
        this.txtbillno.Text = "";
        this.txtbilldate.Text = "";
        this.txtgrno.Text = "";
        this.txtgrdate.Text = "";
        this.txtvehicleno.Text = "";
        this.txttransport.Text = "";
        this.txtwaybillno.Text = "";
        this.txtwaybilldate.Text = "";
    }

    #region grd_debitcredit_invoice_RowDataBound
    protected void grd_debitcredit_invoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

    protected void btndebitgrdcheckRealised_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lbldrguid = (Label)gvr.FindControl("lbldrguid");
            TextBox txtdebitamount = (TextBox)gvr.FindControl("txtdramount");
            Label lbldrLedgerId = (Label)gvr.FindControl("lbldrLedgerId");
            Label lblRealisedno = (Label)gvr.FindControl("lblRealisedno");
            Label lblRealiseddate = (Label)gvr.FindControl("lblRealiseddate");

            this.hdn_debitguid.Value = lbldrguid.Text.Trim();
            this.hdn_debitamount.Value = Convert.ToString(string.IsNullOrEmpty(txtdebitamount.Text.Trim()) ? "0" : txtdebitamount.Text.Trim());
            this.hdn_ledgercostcenter.Value = lbldrLedgerId.Text;
            this.hdn_RealisedNo.Value = lblRealisedno.Text;
            this.hdn_RealisedDate.Value = lblRealiseddate.Text;

            this.popupChequeRealised.Show();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void btnChequeRealised_Click(object sender, EventArgs e)
    {
        if (Session["VOUCHERDETAILS"] == null)
        {
            this.CreateVoucherTable();
        }
        DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

        foreach (DataRow row in dtvoucher.Rows)
        {
            if (row["GUID"].ToString() == hdn_debitguid.Value.ToString())
            {
                row["IsChequeRealised"] = "Y";
                row["ChequeRealisedDate"] = this.txtRealisedDate.Text.Trim();
                row["ChequeRealisedNo"] = this.txtRealisedno.Text.Trim();
            }
        }
        HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;

        this.RestChequeRealished();
    }

    protected void imgcloserealished_click(object sender, EventArgs e)
    {
        try
        {
            this.RestChequeRealished();
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    public void RestChequeRealished()
    {
        this.hdn_RealisedDate.Value = "";
        this.hdn_RealisedNo.Value = "";
        this.pnlAdd.Style["display"] = "";
        this.pnlChequeRealised.Style["display"] = "none";
    }

    public void txtAmntCr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["VOUCHERID"] == "16")
            {
                this.hdn_accounttype.Value = "1";
                if (this.ddlAcctypeCr.SelectedValue != "0")
                {
                    ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

                    decimal outstanding = clsVoucher.Outstanding(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlregion.SelectedValue, this.txtvoucherdate.Text, this.txtvoucherdate.Text, Convert.ToString(Session["FINYEAR"]));
                    this.lblcreditoutstanding.Text = outstanding.ToString();

                    int costcenterflag = 0;
                    int flag = 0;

                    //if ((this.ddlvouchertype.SelectedValue == "9") || (this.ddlvouchertype.SelectedValue == "15") || (this.ddlvouchertype.SelectedValue == "10") || (this.ddlvouchertype.SelectedValue == "16") || (this.ddlvouchertype.SelectedValue == "13"))
                    //{
                    //    flag = this.InvoiceDetailsBasedonLeadger(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", "N", this.ddlregion.SelectedValue);
                    //}
                    //if (flag == 0)
                    //{
                    decimal amount = string.IsNullOrEmpty(this.txtAmntCr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmntCr.Text.Trim());
                    costcenterflag = this.CostCenterApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), amount, 'N');
                    if (costcenterflag == 0)
                    {
                        this.TaxApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", amount, this.ddlregion.SelectedValue);
                    }
                    //}

                    if (HttpContext.Current.Session["INVOICEDETAILS"] != null)
                    {
                        DataTable dt = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
                        if (dt.Rows.Count > 0)
                        {
                            //if (this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14")      // 13 - CREDIT, 14 - DEBIT
                            //{
                            //    this.trinvoicedetails.Style["display"] = "none";
                            //}
                            //else
                            //{
                            int NumberofRecord = dt.Select("LedgerId='" + this.ddlAcctypeCr.SelectedValue.Trim() + "'").Length;
                            if (NumberofRecord > 0)
                            {
                                this.trinvoicedetails.Style["display"] = "";
                            }
                            else
                            {
                                this.trinvoicedetails.Style["display"] = "none";
                            }
                            //}
                        }
                        else
                        {
                            this.trinvoicedetails.Style["display"] = "none";
                        }
                    }

                    //if (this.ddlvouchertype.SelectedValue == "13")      // CREDIT NOTE
                    //{
                    //    this.InvoiceDetails_Debit_Credit_Leadger(this.ddlAcctypeCr.SelectedValue, this.ddlAcctypeCr.SelectedItem.Text);
                    //}

                    if ((this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "9") && this.ddlMode.SelectedValue == "B")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtcreditchequeno.ClientID + "').focus(); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btncreditadd.ClientID + "').focus(); ", true);
                    }
                }
                else
                {
                    this.lbldebitoutstanding.Text = "0.00";
                }
            }

            else
            {
                this.hdn_accounttype.Value = "1";
                if (this.ddlAcctypeCr.SelectedValue != "0")
                {
                    ClsVoucherEntry clsVoucher = new ClsVoucherEntry();

                    decimal outstanding = clsVoucher.Outstanding(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlregion.SelectedValue, this.txtvoucherdate.Text, this.txtvoucherdate.Text, Convert.ToString(Session["FINYEAR"]));
                    this.lblcreditoutstanding.Text = outstanding.ToString();

                    int costcenterflag = 0;
                    int flag = 0;

                    //if ((this.ddlvouchertype.SelectedValue == "9") || (this.ddlvouchertype.SelectedValue == "15") || (this.ddlvouchertype.SelectedValue == "10") || (this.ddlvouchertype.SelectedValue == "16") || (this.ddlvouchertype.SelectedValue == "13"))
                    //{
                    //    flag = this.InvoiceDetailsBasedonLeadger(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", "N", this.ddlregion.SelectedValue);
                    //}
                    //if (flag == 0)
                    //{
                    decimal amount = string.IsNullOrEmpty(this.txtAmntCr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmntCr.Text.Trim());
                    costcenterflag = this.CostCenterApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), amount, 'N');
                    if (costcenterflag == 0)
                    {
                        this.TaxApplicable(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", amount, this.ddlregion.SelectedValue);
                    }
                    //}

                    if (HttpContext.Current.Session["INVOICEDETAILS"] != null)
                    {
                        DataTable dt = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
                        if (dt.Rows.Count > 0)
                        {
                            //if (this.ddlvouchertype.SelectedValue == "13" || this.ddlvouchertype.SelectedValue == "14")      // 13 - CREDIT, 14 - DEBIT
                            //{
                            //    this.trinvoicedetails.Style["display"] = "none";
                            //}
                            //else
                            //{
                            //int NumberofRecord = dt.Select("LedgerId='" + this.ddlAcctypeCr.SelectedValue.Trim() + "'").Length;
                            //if (NumberofRecord > 0)
                            //{
                            //    this.trinvoicedetails.Style["display"] = "";
                            //}
                            //else
                            //{
                            //    this.trinvoicedetails.Style["display"] = "none";
                            //}
                            //}
                        }
                        else
                        {
                            this.trinvoicedetails.Style["display"] = "none";
                        }
                    }

                    //if (this.ddlvouchertype.SelectedValue == "13")      // CREDIT NOTE
                    //{
                    //    this.InvoiceDetails_Debit_Credit_Leadger(this.ddlAcctypeCr.SelectedValue, this.ddlAcctypeCr.SelectedItem.Text);
                    //}

                    if ((this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "9") && this.ddlMode.SelectedValue == "B")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtcreditchequeno.ClientID + "').focus(); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btncreditadd.ClientID + "').focus(); ", true);
                    }
                }
                else
                {
                    this.lbldebitoutstanding.Text = "0.00";
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    /*pop up not showing on debit site*/
    //public void txtAmtDr_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.hdn_accounttype.Value = "0";

    //        if (this.ddlAccTypeDr.SelectedValue != "0")
    //        {
    //            if (this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "14")      /* ADVANCE PAYMENT || DEBIT NOTE */
    //            {
    //                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
    //                decimal outstanding = clsVoucher.Outstanding(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlregion.SelectedValue, this.txtvoucherdate.Text, this.txtvoucherdate.Text, Convert.ToString(Session["FINYEAR"]));

    //                this.lbldebitoutstanding.Text = outstanding.ToString();
    //                int costcenterflag = 0;
    //                int flag = 0;

    //                if ((this.ddlvouchertype.SelectedValue == "9") || (this.ddlvouchertype.SelectedValue == "15") || (this.ddlvouchertype.SelectedValue == "10") || (this.ddlvouchertype.SelectedValue == "16") || (this.ddlvouchertype.SelectedValue == "14"))
    //                {
    //                    flag = this.InvoiceDetailsBasedonLeadger(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", "N", this.ddlregion.SelectedValue);
    //                }

    //                //if (flag == 0)    /* THIS BLOCK OPEN CAUSE IF USER NOT WANT TO TAG INVOICE, IF CLOSEED IT THEN YOU ARE MANDATORY TO TAG INVOICE THE TAX OR COST CENTER POP UP OPEN (SO PROBLEM IT) */
    //                //{
    //                    decimal amount = string.IsNullOrEmpty(this.txtAmtDr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmtDr.Text.Trim());
    //                    costcenterflag = this.CostCenterApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), amount, 'N');
    //                    if (costcenterflag == 0)
    //                    {
    //                        this.TaxApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", amount, this.ddlregion.SelectedValue);
    //                    }
    //                //}
    //                if (flag == 2)
    //                {
    //                    MessageBox1.ShowInfo("<b><font color='red'>No invoice found!</font></b>");
    //                }
    //            }

    //            if (HttpContext.Current.Session["INVOICEDETAILS"] != null)
    //            {
    //                DataTable dt = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
    //                if (dt.Rows.Count > 0)
    //                {
    //                    int NumberofRecord = dt.Select("LedgerId='" + this.ddlAccTypeDr.SelectedValue.Trim() + "'").Length;
    //                    if (NumberofRecord > 0)
    //                    {
    //                        this.trinvoicedetails.Style["display"] = "";
    //                    }
    //                    else
    //                    {
    //                        this.trinvoicedetails.Style["display"] = "none";
    //                    }
    //                }
    //                else
    //                {
    //                    this.trinvoicedetails.Style["display"] = "none";
    //                }
    //            }

    //            //if (this.ddlvouchertype.SelectedValue == "14")      // DEBIT NOTE
    //            //{
    //            //    this.InvoiceDetails_Debit_Credit_Leadger(this.ddlAccTypeDr.SelectedValue, this.ddlAccTypeDr.SelectedItem.Text);
    //            //}

    //            if ((this.ddlvouchertype.SelectedValue == "16" || this.ddlvouchertype.SelectedValue == "10") && this.ddlMode.SelectedValue == "B")
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtdebitchequeno.ClientID + "').focus(); ", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btndebitadd.ClientID + "').focus(); ", true);
    //            }
    //        }
    //        else
    //        {
    //            this.lbldebitoutstanding.Text = "0.00";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
    //    }
    //} 

    public void txtAmtDr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            this.hdn_accounttype.Value = "0";

            if (this.ddlAccTypeDr.SelectedValue != "0")
            {
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                decimal outstanding = clsVoucher.Outstanding(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlregion.SelectedValue, this.txtvoucherdate.Text, this.txtvoucherdate.Text, Convert.ToString(Session["FINYEAR"]));

                this.lbldebitoutstanding.Text = outstanding.ToString();
                int costcenterflag = 0;
                //int flag = 0;
                //flag = this.InvoiceDetailsBasedonLeadger(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", "N", this.ddlregion.SelectedValue);
                decimal amount = string.IsNullOrEmpty(this.txtAmtDr.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.txtAmtDr.Text.Trim());
                costcenterflag = this.CostCenterApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), amount, 'N');
                if (costcenterflag == 0)
                {
                    this.TaxApplicable(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", amount, this.ddlregion.SelectedValue);
                }

                //if (flag == 2)
                //{
                //    MessageBox1.ShowInfo("<b><font color='red'>No invoice found!</font></b>");
                //}

                if (HttpContext.Current.Session["ACC_INVOICEDETAILS"] != null)
                {
                    DataTable dt = (DataTable)HttpContext.Current.Session["ACC_INVOICEDETAILS"];
                    if (dt.Rows.Count > 0)
                    {
                        int NumberofRecord = dt.Select("LedgerId='" + this.ddlAccTypeDr.SelectedValue.Trim() + "'").Length;
                        if (NumberofRecord > 0)
                        {
                            this.trinvoicedetails.Style["display"] = "";
                            //this.popup.Show();
                        }
                        else
                        {
                            this.trinvoicedetails.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        this.trinvoicedetails.Style["display"] = "none";
                    }
                }

                if ((this.ddlvouchertype.SelectedValue == "16" || this.ddlvouchertype.SelectedValue == "10") && this.ddlMode.SelectedValue == "B")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtdebitchequeno.ClientID + "').focus(); ", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + btndebitadd.ClientID + "').focus(); ", true);
                }
            }
            else
            {
                this.lbldebitoutstanding.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    #region CalculateTotalAmountCostCenter
    decimal CalculateTotalAmountCostCenter(DataTable dt, string LedgerID)
    {
        decimal Total = 0;

        for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
        {
            if (dt.Rows[Counter]["LedgerId"].ToString().Trim() == LedgerID)
            {
                Total += Convert.ToDecimal(dt.Rows[Counter]["amount"]);
            }
        }

        return Total;
    }
    #endregion


    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblinvoiceID = (Label)gvr.FindControl("lblAccEntryID");
            this.hdn_transferid.Value = lblinvoiceID.Text.Trim();

            string upath = "frmRptInvoicePrint.aspx?AdvRecpt_ID=" + hdn_transferid.Value.Trim() + "&Voucherid=" + this.Request.QueryString["VOUCHERID"].ToString().Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            this.hdn_transferid.Value = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
            this.txtAmtDr.Text = "";
            this.txtAmntCr.Text = "";
            this.lblcreditoutstanding.Text = "0.00";
            this.lbldebitoutstanding.Text = "0.00";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btncrrecalculation_Click(object sender, EventArgs e)
    {
        try
        {
            decimal totalcreditamount = 0;
            string crchqno = "";
            if (Session["VOUCHERDETAILS"] != null)
            {
                DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

                decimal amount = 0;

                foreach (GridViewRow gvrow in gvcredit.Rows)
                {
                    Label lblledgerid = (Label)gvrow.FindControl("lblcrLedgerId");
                    TextBox txtamount = (TextBox)gvrow.FindControl("txtcramount");
                    amount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                    crchqno = gvcredit.Rows[gvrow.RowIndex].Cells[7].Text.Trim().Replace("&nbsp;", "");

                    foreach (DataRow row in dtvoucher.Rows)
                    {
                        if (Convert.ToString(row["LedgerId"]).Trim() == lblledgerid.Text.Trim() && Convert.ToString(row["ChequeNo"]).Trim() == crchqno && Convert.ToString(row["TxnType"]).Trim() == "1")
                        {
                            row["Amount"] = Convert.ToString(amount);
                            totalcreditamount = totalcreditamount + Convert.ToDecimal(amount);
                        }
                    }
                }

                dtvoucher.AcceptChanges();
                HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;
            }
            this.txtcredittotalamount.Text = Convert.ToString(totalcreditamount);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btndrrecalculation_Click(object sender, EventArgs e)
    {
        try
        {
            decimal totaldebitamount = 0;
            string drchqno = "";
            if (Session["VOUCHERDETAILS"] != null)
            {
                DataTable dtvoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];

                decimal amount = 0;

                foreach (GridViewRow gvrow in gvdebit.Rows)
                {
                    Label lblledgerid = (Label)gvrow.FindControl("lbldrLedgerId");
                    TextBox txtamount = (TextBox)gvrow.FindControl("txtdramount");
                    amount = string.IsNullOrEmpty(txtamount.Text.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(txtamount.Text.Trim());
                    drchqno = gvdebit.Rows[gvrow.RowIndex].Cells[7].Text.Trim().Replace("&nbsp;", "");

                    foreach (DataRow row in dtvoucher.Rows)
                    {
                        if (Convert.ToString(row["LedgerId"]).Trim() == lblledgerid.Text.Trim() && Convert.ToString(row["ChequeNo"]).Trim() == drchqno && Convert.ToString(row["TxnType"]).Trim() == "0")
                        {
                            row["Amount"] = Convert.ToString(amount);
                            totaldebitamount = totaldebitamount + Convert.ToDecimal(amount);
                        }
                    }
                }

                dtvoucher.AcceptChanges();
                HttpContext.Current.Session["VOUCHERDETAILS"] = dtvoucher;
            }
            this.txtdebittotalamount.Text = Convert.ToString(totaldebitamount);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btndebitgrdcostcenter_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            TextBox txtdebitamount = (TextBox)gvr.FindControl("txtdramount");
            Label lbldrLedgerId = (Label)gvr.FindControl("lbldrLedgerId");
            Label lbldrledgername = (Label)gvr.FindControl("lbldrledgername");

            this.hdn_ledgername.Value = lbldrledgername.Text.Trim();
            this.hdn_ledgercostcenter.Value = lbldrLedgerId.Text;
            this.hdn_accountid.Value = lbldrLedgerId.Text;
            this.hdn_accounttype.Value = "0";
            this.hdn_btncostcenterclick.Value = "Y";

            this.CostCenterApplicable(this.hdn_accountid.Value, this.hdn_ledgername.Value, Convert.ToDecimal(string.IsNullOrEmpty(txtdebitamount.Text.Trim()) ? "0" : txtdebitamount.Text.Trim()), 'Y');
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcatagory.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btncreditgrdcostcenter_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            TextBox txtcreditamount = (TextBox)gvr.FindControl("txtcramount");
            Label lblcrLedgerId = (Label)gvr.FindControl("lblcrLedgerId");
            Label lblcrledgerName = (Label)gvr.FindControl("lblcrledgerName");

            this.hdn_ledgername.Value = lblcrledgerName.Text.Trim();
            this.hdn_ledgercostcenter.Value = lblcrLedgerId.Text;
            this.hdn_accountid.Value = lblcrLedgerId.Text;
            this.hdn_accounttype.Value = "1";
            this.hdn_btncostcenterclick.Value = "Y";

            this.CostCenterApplicable(this.hdn_accountid.Value, this.hdn_ledgername.Value, Convert.ToDecimal(string.IsNullOrEmpty(txtcreditamount.Text.Trim()) ? "0" : txtcreditamount.Text.Trim()), 'Y');
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlcatagory.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnReject_Click
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

    #region btnRejectionNoteSubmit_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnRejectionNoteSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
            int flag = 0;
            string voucherid = Convert.ToString(this.hdn_accid.Value).Trim();
            flag = clsPurchaseStockReceipt.RejectAccounts(voucherid, this.txtRejectionNote.Text.Trim(), this.ddlvouchertype.SelectedValue);

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.lightRejectionNote.Style["display"] = "none";
                this.fadeRejectionNote.Style["display"] = "none";
                this.txtRejectionNote.Text = "";
                MessageBox1.ShowSuccess("Voucher : <b><font color='green'>" + this.txtvoucherno.Text + "</font></b> rejected successfully.", 60, 500);
                this.Bindclose();
            }
            else if (flag == 0)
            {
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "none";
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
    // Added By Ajoy Rana On 28-06-2016
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

    public void Btn_View(string _VoucherId)
    {
        try
        {
            string BranchID = Request.QueryString["BRANCHID"].ToString();
            ViewState["AccType"] = "Normal";
            this.divbtnpaidamount.Style["display"] = "";
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.CreateVoucherTable();
            this.CreateInvoiceTable();
            this.Create_DebitCreditInvoiceTable();
            DataTable dtVoucher = (DataTable)HttpContext.Current.Session["VOUCHERDETAILS"];
            DataTable dtinvoice = (DataTable)HttpContext.Current.Session["INVOICEDETAILS"];
            DataTable dtdebitcredit = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

            hdn_accid.Value = _VoucherId;
            string voucherid = hdn_accid.Value.ToString();
            string approved = hdn_approved.Value.ToString();
            int Invoicetagledgercount = 0;

            DataSet ds = new DataSet();
            ds = clsVoucher.VoucherDetails(voucherid);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.BindVoucherType();
                    this.txtvoucherno.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString().Trim();
                    this.trvoucherno.Style["display"] = "";
                    this.ddlvouchertype.SelectedValue = ds.Tables[0].Rows[0]["VoucherTypeID"].ToString().Trim();
                    //this.ddlvouchertype.Enabled = false;

                    if (this.ddlvouchertype.SelectedValue == "2")
                    {
                        this.divbtnotherdetails.Style["display"] = "";
                    }
                    else
                    {
                        this.divbtnotherdetails.Style["display"] = "none";
                    }

                    this.ddlregion.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString().Trim();
                    this.ddlregion.Enabled = false;
                    this.txtvoucherdate.Text = ds.Tables[0].Rows[0]["Date"].ToString().Trim();
                    //this.imgPopuppodate.Visible = false;
                    this.BindAccountType(this.ddlvouchertype.SelectedValue, BranchID);
                    this.ddlregion.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["Mode"].ToString().Trim() != "")
                    {
                        this.ddlMode.SelectedValue = ds.Tables[0].Rows[0]["Mode"].ToString().Trim();
                    }

                    this.txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString().Trim();
                    this.txtCheckerNote.Text = ds.Tables[0].Rows[0]["RejectionNote"].ToString().Trim();

                    if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "15" || this.ddlvouchertype.SelectedValue == "16")
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        this.btnaddhide.Style["display"] = "none";
                        this.innertr.Style["display"] = "";
                        this.trpaymentmode.Style["display"] = "";
                        this.trdebitbank.Style["display"] = "";
                        this.trdebitchequeno.Style["display"] = "";
                        this.trcreditbank.Style["display"] = "";
                        this.trcreditchequeno.Style["display"] = "";
                        this.BindAccountType(this.ddlvouchertype.SelectedValue, BranchID);
                        this.BindMode();
                    }
                    else
                    {
                        this.pnlDisplay.Style["display"] = "none";
                        this.pnlAdd.Style["display"] = "";
                        this.btnaddhide.Style["display"] = "none";
                        this.trdebitbank.Style["display"] = "none";
                        this.trdebitchequeno.Style["display"] = "none";
                        this.trcreditbank.Style["display"] = "none";
                        this.trcreditchequeno.Style["display"] = "none";
                        this.innertr.Style["display"] = "";
                        this.trpaymentmode.Style["display"] = "none";
                        //this.BindAccountType(this.ddlvouchertype.SelectedValue, this.ddlregion.SelectedValue);
                        this.BindAccountType(this.ddlvouchertype.SelectedValue, BranchID);
                        this.BindMode();
                    }

                    if (this.ddlvouchertype.SelectedValue.Trim() == "13")
                    {
                        this.gvdebit.Columns[9].Visible = false;
                        this.gvcredit.Columns[9].Visible = true;
                    }
                    else if (this.ddlvouchertype.SelectedValue.Trim() == "14")
                    {
                        this.gvdebit.Columns[9].Visible = true;
                        this.gvcredit.Columns[9].Visible = false;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            DataRow dr = dtVoucher.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["LedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                            dr["LedgerName"] = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                            dr["TxnType"] = Convert.ToString(ds.Tables[1].Rows[i]["TxnType"]).Trim();
                            dr["Amount"] = Convert.ToString(ds.Tables[1].Rows[i]["Amount"]).Trim();
                            dr["BankID"] = Convert.ToString(ds.Tables[1].Rows[i]["BankID"]);
                            dr["BankName"] = Convert.ToString(ds.Tables[1].Rows[i]["BankName"]);
                            dr["PAYMENTTYPEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPEID"]);
                            dr["PAYMENTTYPENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PAYMENTTYPENAME"]);
                            dr["ChequeNo"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeNo"]);
                            dr["ChequeDate"] = Convert.ToString(ds.Tables[1].Rows[i]["ChequeDate"]);
                            dr["IsChequeRealised"] = Convert.ToString(ds.Tables[1].Rows[i]["IsChequeRealised"]);
                            dr["Remarks"] = Convert.ToString(ds.Tables[1].Rows[i]["Remarks"]);
                            dr["DeductableAmount"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableAmount"]);
                            dr["DeductablePercentage"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductablePercentage"]);
                            dr["DeductableLedgerId"] = Convert.ToString(ds.Tables[1].Rows[i]["DeductableLedgerId"]);
                            dr["IsCostCenter"] = Convert.ToString(ds.Tables[1].Rows[i]["IsCostCenter"]);
                            dr["IsTagInvoice"] = Convert.ToString(ds.Tables[1].Rows[i]["IsTagInvoice"]);

                            dtVoucher.Rows.Add(dr);
                            dtVoucher.AcceptChanges();

                            if (Invoicetagledgercount == 0)     /*---- if count set flag 1 then break ----*/
                            {
                                if (this.ddlvouchertype.SelectedValue == "9" || this.ddlvouchertype.SelectedValue == "15")
                                {
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), "9", BranchID);
                                }
                                else if (this.ddlvouchertype.SelectedValue == "10" || this.ddlvouchertype.SelectedValue == "16")
                                {
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), "10", BranchID);
                                }
                                else
                                {
                                    /* Other Voucher will take care in SP please check */
                                    dt = clsVoucher.InvoiceDetails(voucherid, ds.Tables[1].Rows[i]["LedgerId"].ToString().Trim(), this.ddlvouchertype.SelectedValue, BranchID);
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.hdn_ledgercostcenter.Value = Convert.ToString(ds.Tables[1].Rows[i]["LedgerId"]).Trim();
                                    this.hdn_ledgername.Value = Convert.ToString(ds.Tables[1].Rows[i]["LedgerName"]).Trim();
                                    this.lblledgername.Text = Convert.ToString(this.hdn_ledgername.Value).Trim();
                                    Invoicetagledgercount = 1;
                                    for (int counter = 0; counter < dt.Rows.Count; counter++)
                                    {
                                        DataRow dr1 = dtinvoice.NewRow();
                                        dr1["GUID"] = Guid.NewGuid();
                                        dr1["LedgerId"] = dt.Rows[counter]["LedgerId"].ToString().Trim();
                                        dr1["LedgerName"] = dt.Rows[counter]["name"].ToString().Trim();
                                        dr1["InvoiceID"] = dt.Rows[counter]["InvoiceID"].ToString().Trim();
                                        dr1["InvoiceNo"] = dt.Rows[counter]["InvoiceNo"].ToString().Trim();
                                        dr1["InvoiceDate"] = dt.Rows[counter]["InvoiceDate"].ToString().Trim();
                                        dr1["InvoiceOthers"] = dt.Rows[counter]["InvoiceOthers"].ToString().Trim();
                                        dr1["InvoiceBranchID"] = dt.Rows[counter]["InvoiceBranchID"].ToString().Trim();
                                        dr1["InvoiceBranchName"] = dt.Rows[counter]["InvoiceBranchName"].ToString().Trim();
                                        dr1["VoucherType"] = dt.Rows[counter]["VoucherType"].ToString().Trim();
                                        dr1["BranchID"] = dt.Rows[counter]["BranchID"].ToString().Trim();
                                        dr1["InvoiceAmt"] = dt.Rows[counter]["InvoiceAmt"].ToString().Trim();
                                        dr1["AlreadyAmtPaid"] = dt.Rows[counter]["AlreadyAmtPaid"].ToString().Trim();
                                        dr1["RemainingAmtPaid"] = dt.Rows[counter]["RemainingAmt"].ToString().Trim();
                                        dr1["AmtPaid"] = dt.Rows[counter]["AmtPaid"].ToString().Trim();
                                        dr1["Type"] = dt.Rows[counter]["Type"].ToString().Trim();

                                        dtinvoice.Rows.Add(dr1);
                                        dtinvoice.AcceptChanges();
                                    }
                                    HttpContext.Current.Session["INVOICEDETAILS"] = dtinvoice;
                                }
                            }
                        }

                        HttpContext.Current.Session["VOUCHERDETAILS"] = dtVoucher;

                        //------------------ For Debit Portion -----------------//
                        dtVoucher.DefaultView.RowFilter = "TxnType = '0'";
                        decimal sumdebit = (decimal)dtVoucher.Compute("Sum(Amount)", "TxnType = '0'");
                        this.txtdebittotalamount.Text = sumdebit.ToString();

                        this.gvdebit.DataSource = dtVoucher;
                        this.gvdebit.DataBind();
                        //------------------------------------------------------//

                        //this.InvoiceDetailsBasedonLeadger("N");

                        //------------------ For Credit Portion -----------------//
                        dtVoucher.DefaultView.RowFilter = "TxnType = '1'";
                        decimal sumcredit = (decimal)dtVoucher.Compute("Sum(Amount)", "TxnType = '1'");
                        this.txtcredittotalamount.Text = sumcredit.ToString();

                        this.gvcredit.DataSource = dtVoucher;
                        this.gvcredit.DataBind();
                        //-------------------------------------------------------//

                        //this.InvoiceDetailsBasedonLeadger("N");

                        //------------------ For Invoice Details Portion -----------------//
                        //dtinvoice.DefaultView.RowFilter = "TxnType = '1'";
                        //decimal sumpaidamount = (decimal)dtinvoice.Compute("Sum(AmtPaid)", "");
                        //this.txtpaidamount.Text = sumpaidamount.ToString();

                        decimal paidamount = 0;

                        for (int i = 0; i < dtinvoice.Rows.Count; i++)
                        {
                            paidamount = paidamount + Convert.ToDecimal(dtinvoice.Rows[i]["AmtPaid"].ToString().Trim());
                        }

                        this.txtpaidamount.Text = paidamount.ToString();


                        if (dtinvoice.Rows.Count > 0)
                        {
                            this.grd_InvoiceDetails.DataSource = dtinvoice;
                            this.grd_InvoiceDetails.DataBind();
                            this.trinvoicedetails.Style["display"] = "";
                        }
                        else
                        {
                            this.grd_InvoiceDetails.DataSource = null;
                            this.grd_InvoiceDetails.DataBind();
                            this.trinvoicedetails.Style["display"] = "none";
                        }

                        //-------------------------------------------------------//

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            this.CreateCostCenterTable();
                            DataTable dtcost = (DataTable)HttpContext.Current.Session["COSTCENTERDETAILS"];

                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                DataRow dr = dtcost.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerId"]);
                                dr["LedgerName"] = Convert.ToString(ds.Tables[2].Rows[i]["LedgerName"]);
                                dr["CostCatagoryID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryID"]);
                                dr["CostCatagoryName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCatagoryName"]);
                                dr["CostCenterID"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterID"]);
                                dr["CostCenterName"] = Convert.ToString(ds.Tables[2].Rows[i]["CostCenterName"]);
                                dr["BranchID"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchID"]);
                                dr["BranchName"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchName"]);
                                dr["amount"] = Convert.ToString(ds.Tables[2].Rows[i]["amount"]);
                                dr["BrandID"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandID"]);
                                dr["BrandName"] = Convert.ToString(ds.Tables[2].Rows[i]["BrandName"]);
                                dr["ProductID"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductID"]);
                                dr["ProductName"] = Convert.ToString(ds.Tables[2].Rows[i]["ProductName"]);
                                dr["DepartmentID"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentID"]);
                                dr["DepartmentName"] = Convert.ToString(ds.Tables[2].Rows[i]["DepartmentName"]);
                                dr["FromDate"] = Convert.ToString(ds.Tables[2].Rows[i]["FromDate"]);
                                dr["ToDate"] = Convert.ToString(ds.Tables[2].Rows[i]["ToDate"]);
                                dr["Narration"] = Convert.ToString(ds.Tables[2].Rows[i]["NARRATION"]);
                                dr["TxnType"] = Convert.ToString(ds.Tables[2].Rows[i]["TxnType"]);

                                dtcost.Rows.Add(dr);
                                dtcost.AcceptChanges();
                            }
                            this.grdcostcenter.DataSource = dtcost;
                            this.grdcostcenter.DataBind();
                            HttpContext.Current.Session["COSTCENTERDETAILS"] = dtcost;
                        }

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            DataTable dtdebitinvoice = (DataTable)HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"];

                            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                            {
                                DataRow dr = dtdebitinvoice.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["LedgerId"] = Convert.ToString(ds.Tables[4].Rows[i]["LedgerId"]);
                                dr["LedgerName"] = Convert.ToString(ds.Tables[4].Rows[i]["LedgerName"]);
                                dr["InvoiceID"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceID"]);
                                dr["InvoiceNo"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceNo"]);
                                dr["InvoiceAmt"] = Convert.ToString(ds.Tables[4].Rows[i]["InvoiceAmt"]);
                                dr["TAG"] = Convert.ToString(ds.Tables[4].Rows[i]["TAG"]);

                                dtdebitinvoice.Rows.Add(dr);
                                dtdebitinvoice.AcceptChanges();
                            }

                            HttpContext.Current.Session["INVOICE_CREDITDEBITDETAILS"] = dtdebitinvoice;
                        }

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            this.txtbillno.Text = Convert.ToString(ds.Tables[3].Rows[0]["BillNo"]);
                            this.txtbilldate.Text = Convert.ToString(ds.Tables[3].Rows[0]["BillDate"]);
                            this.txtgrno.Text = Convert.ToString(ds.Tables[3].Rows[0]["GRNo"]);
                            this.txtgrdate.Text = Convert.ToString(ds.Tables[3].Rows[0]["GRDate"]);
                            this.txtvehicleno.Text = Convert.ToString(ds.Tables[3].Rows[0]["VehicleNo"]);
                            this.txttransport.Text = Convert.ToString(ds.Tables[3].Rows[0]["Transport"]);
                            this.txtwaybillno.Text = Convert.ToString(ds.Tables[3].Rows[0]["WayBillNo"]);
                            this.txtwaybilldate.Text = Convert.ToString(ds.Tables[3].Rows[0]["WayBillDate"]);
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("<b><font color='red'>Voucher details not found!</font></b>");
                        return;
                    }

                    this.ControlApproval();                     // For A/c Approval
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Voucher details not found!</b>");
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>No records found!</b>");
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }

    protected void BindState() /* load all state mode 1 for all state ,1 from sp */
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlPlaceofSupply.Items.Clear();
            this.ddlPlaceofSupply.Items.Add(new ListItem("Select State", "0"));
            this.ddlPlaceofSupply.AppendDataBoundItems = true;
            this.ddlPlaceofSupply.DataSource = clsVoucher.BindStateParty("1", this.Session["USERID"].ToString(), "");
            this.ddlPlaceofSupply.DataTextField = "State_Name";
            this.ddlPlaceofSupply.DataValueField = "STATE_ID";
            this.ddlPlaceofSupply.DataBind();
            this.ddlPlaceofSupply.SelectedValue = "26";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindParty() /* load party mode 2 for  party,2 from sp  */
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            this.ddlPartyName.Items.Clear();
            this.ddlPartyName.Items.Add(new ListItem("Select Party", "0"));
            this.ddlPartyName.AppendDataBoundItems = true;
            this.ddlPartyName.DataSource = clsVoucher.BindStateParty("2", HttpContext.Current.Session["DEPOTID"].ToString(), "");
            this.ddlPartyName.DataTextField = "LEDGERNAME";
            this.ddlPartyName.DataValueField = "LEDGERID";
            this.ddlPartyName.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindgstno()
    {
        try
        {
            ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            DataTable dt = new DataTable();
            dt = clsVoucher.BindStateParty("3", "", this.ddlPartyName.SelectedValue);
            if (dt.Rows[0]["GSTNO"].ToString() != "")
            {
                txtGstNo.Text = dt.Rows[0]["GSTNO"].ToString();
                this.txtGstNo.Enabled = false;
            }
            else
            {
                this.txtGstNo.Enabled = true;
                this.txtGstNo.Text = "";
            }

        }
        catch (Exception ex)
        {

        }
    }

    #region tab change
    protected void ddlPlaceofSupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlPartyName.ClientID + "').focus(); ", true);
    }

    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.bindgstno();
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtInvoiceNo.ClientID + "').focus(); ", true);

    }
    protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtTaxPercent.ClientID + "').focus(); ", true);
    }

    protected void txtTaxPercent_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtTaxValue.ClientID + "').focus(); ", true);
    }
    protected void txtTaxValue_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtHsn.ClientID + "').focus(); ", true);
    }
    #endregion

    protected void ddlVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (this.ddlVoucher.SelectedValue == "1" || this.ddlVoucher.SelectedValue == "3")
        //{
        //    btngstonly.Visible = false;
        //}
        //else
        //{
        btngstonly.Visible = true;
        this.txtGstNo.ForeColor = Color.Blue;
        this.txtTaxValue.ForeColor = Color.Blue;
        this.txtTaxPercent.ForeColor = Color.Blue;
        this.txtHsn.ForeColor = Color.Blue;
        this.txtInvoiceNo.ForeColor = Color.Blue;
        this.BindState();
        this.BindParty();
        this.txtGstNo.Text = "";
        this.txtHsn.Text = "";
        this.txtInvoiceNo.Text = "";
        this.txtTaxPercent.Text = "";
        this.txtTaxValue.Text = "";
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlPlaceofSupply.ClientID + "').focus(); ", true);
        //}
    }



    protected void ddlAccTypeDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mode = "DrClosing";
        if (Request.QueryString["VOUCHERID"] == "16")
        {

        }
        else
        {
            this.txtAmtDr.Text = "0";
            int flag = 0;
            flag = this.InvoiceDetailsBasedonLeadger(this.ddlAccTypeDr.SelectedValue.Trim(), this.ddlAccTypeDr.SelectedItem.Text.Trim(), "0", "N", this.ddlregion.SelectedValue);
            if (flag == 2)
            {
                MessageBox1.ShowInfo("<b><font color='red'>No invoice found!</font></b>");
            }
        }
        lblDrClosingBalance.Text = Convert.ToString(getClosingBalance(mode, this.ddlAccTypeDr.SelectedValue.Trim()));

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtAmtDr.ClientID + "').focus(); ", true);

    }

    public decimal getClosingBalance(string mode, string ledgerid)
    {
        decimal value = 0;
        DataTable dt = new DataTable();
        ClsVendor_TPU objClsVendor_TPU = new ClsVendor_TPU();
        dt = objClsVendor_TPU.BindGateEntry(mode, ledgerid);
        value = Convert.ToDecimal(dt.Rows[0]["AMNT"]);
        return value;
    }

    protected void ddlAcctypeCr_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
        if (Request.QueryString["VOUCHERID"] == "16")
        {
            int flag = 0;
            if ((this.ddlvouchertype.SelectedValue == "9") || (this.ddlvouchertype.SelectedValue == "15") || (this.ddlvouchertype.SelectedValue == "10") || (this.ddlvouchertype.SelectedValue == "16") || (this.ddlvouchertype.SelectedValue == "13"))
            {
                flag = this.InvoiceDetailsBasedonLeadger(this.ddlAcctypeCr.SelectedValue.Trim(), this.ddlAcctypeCr.SelectedItem.Text.Trim(), "1", "N", this.ddlregion.SelectedValue);
            }
            this.lblledgername.Text = this.ddlAcctypeCr.SelectedItem.Text.ToString();
        }
        DataTable dt = new DataTable();
        dt = clsVoucher.BindCHEQUENOLIST(ddlAcctypeCr.SelectedValue.ToString(), this.ddlregion.SelectedValue);

        this.txtcreditchequeno.Items.Clear();
        this.txtcreditchequeno.Items.Add(new ListItem("SELECT", ""));
        this.txtcreditchequeno.AppendDataBoundItems = true;
        this.txtcreditchequeno.DataSource = dt;
        this.txtcreditchequeno.DataTextField = "CHEQUENOWITHBANK";
        this.txtcreditchequeno.DataValueField = "CHEQUENO";
        this.txtcreditchequeno.DataBind();
        string mode = "CrClosing";
        lblCrClosingBalance.Text = Convert.ToString(getClosingBalance(mode, this.ddlAcctypeCr.SelectedValue.Trim()));


    }

    protected void grvaccentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVoucherApproved = (Label)e.Row.FindControl("lblVoucherApproved");
                string LBLSTATUS = lblVoucherApproved.Text.Trim().ToUpper();

                if (lblVoucherApproved.Text.Trim() == "Yes")
                {

                    lblVoucherApproved.ForeColor = Color.Green;
                }
                else if (lblVoucherApproved.Text.Trim() == "No")
                {

                    lblVoucherApproved.ForeColor = Color.Red;

                }
                else if (lblVoucherApproved.Text.Trim()== "Reject")
                {
                
                    lblVoucherApproved.ForeColor = Color.HotPink;

                }

            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }



    protected void txtvoucherdate_TextChanged(object sender, EventArgs e)
    {
        Session["INVOICEDETAILS"] = null;
        if (this.ddlvouchertype.SelectedValue == "15")/*ADVANCE PAYMENT*/
        {
            this.ddlAccTypeDr.SelectedValue = "0";
        }
        else if (this.ddlvouchertype.SelectedValue == "16")/*ADVANCE receipt*/
        {
            this.ddlAcctypeCr.SelectedValue = "0";
        }
        
      
        this.grd_InvoiceDetails.DataSource = null;
        this.grd_InvoiceDetails.DataBind();

    }
}