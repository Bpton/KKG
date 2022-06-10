using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using WorkFlow;
using Utility;
using Account;


public partial class VIEW_frmTransporterBillV2 : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlLRNo').multiselect({ includeSelectAllOption: true  });});</script>", false);

        if (!IsPostBack)
        {
            #region QueryString

            string Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {

                btnaddhide.Style["display"] = "none";
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;

                this.imgbtnToCalendar.Visible = false;
                this.divbinadd.Visible = false;
                this.Txtbillno.Enabled = false;
                this.txtBillvalue.Enabled = false;
                this.tdlbllr.Visible = false;
                this.btngrddelete.Enabled = false;
                this.btnaddhide.Style["display"] = "none";
                this.tdbilldetails.Style["display"] = "none";
                //this.gvtransporterbill.Columns[18].HeaderText = "VIEW";
            }
            else
            {
                btnaddhide.Style["display"] = "";
                string VISIBLE = Request.QueryString["VISIBLE"].ToString().Trim();
                if (VISIBLE == "Y")
                {
                    td_chktdslabel.Visible = true;
                    td_chktdsinput.Visible = true;
                    td_chkcstlabel.Visible = true;
                    td_chkcstinput.Visible = true;

                    //chktdsapplicable.Checked = true;
                    chkcstapplicable.Checked = true;
                }
                else
                {
                    td_chktdslabel.Visible = false;
                    td_chktdsinput.Visible = false;
                    td_chkcstlabel.Visible = false;
                    td_chkcstinput.Visible = false;

                    //chktdsapplicable.Checked = true;
                    chkcstapplicable.Checked = true;
                }
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;

                this.imgbtnToCalendar.Visible = true;
                this.divbinadd.Visible = true;
                this.Txtbillno.Enabled = true;
                this.txtBillvalue.Enabled = true;
                this.tdlbllr.Visible = true;
                this.btngrddelete.Enabled = true;
                this.btnaddhide.Style["display"] = "";
                this.tdbilldetails.Style["display"] = "";
                //this.gvtransporterbill.Columns[18].HeaderText = "EDIT";
                this.ViewState["transporterid"] = null;
                this.tr_billno.Visible = false;
            }
            #endregion

            this.btnsubmitdiv.Visible = true;
            this.gvtransporterAdd.Columns[26].Visible = true;
            this.chktdsapplicable1.Enabled = true;
            this.chkReversecharge.Enabled = true;
            this.reason.Style["display"] = "none";
            this.td_rcmreason.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.LoadTransporterBill();
            this.LoadDepot();
            this.LoadState();
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            //this.txtFromDate.Text = dtcurr.ToString(date).Replace('-', '/');
            //this.txtToDate.Text = dtcurr.ToString(date).Replace('-', '/');
            //txtlrdate.Text = dtcurr.ToString(date).Replace('-', '/');

            //          *********  LOCK CALENDAR BEYOND FINANCIAL YEAR  **********      HPD       //

            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            CalendarFromDate.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
           
            DateTime today1 = DateTime.Now;
            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtbillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                // this.txtlrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //this.txtbilldate2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CalendarFromDate.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
                CalendarExtenderDate.EndDate = today1;
                CalendarExtenderDate.StartDate = oDate;
               
            }
            else
            {
                CalendarExtenderDate.StartDate = new DateTime(endyear1, 03, 01);
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txtbillDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                //this.txtlrdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                //this.txtbilldate2.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");


                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtenderDate.EndDate = cDate;
               
            }

            /*Calender Control Date Range Added By Sayan Dey*/
            //CalendarExtenderDate.EndDate = DateTime.Now;
              //CalendarExtender2.EndDate = DateTime.Now;
              //CalendarExtender1.EndDate = DateTime.Now;
            /****************************/

            this.LoadReason();
            this.LoadReasonRCM();
            tdlblexportdepot.Style["display"] = "none";
            tdddlexportdepot.Style["display"] = "none";
            this.LoadExporDepot();
            
            this.BindRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["IUSERID"].ToString());
            this.LoadTransporterBill();
        }
        this.txtBillvalue.Attributes.Add("onkeyup", "calculation()");
        this.TxtTds.Attributes.Add("onkeyup", "calculation()");
        
    }
    #endregion

    #region Add New Record
    protected void btnAddMenu_Click(object sender, EventArgs e)
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tag = string.Empty;
        string tagforuser = string.Empty;
       // tagforuser = transporterbill.GetOfflineStatusUser(HttpContext.Current.Session["UserID"].ToString().Trim());
       // tag = transporterbill.GetOfflineStatusDepot();
        //if (tagforuser == "N")
        //{
            this.LoadAdd();
        //}
        //else
        //{
        //    if (tag == "N")
        //    {
        //        if (HttpContext.Current.Session["UserID"].ToString().Trim() == "8")
        //        {
        //            this.LoadAdd();
        //        }
        //        else
        //        {
        //            MessageBox1.ShowInfo("Bill entry is available in offline system of " + ddldepot.SelectedItem.Text + " ", 50, 400);
        //        }
        //    }
        //    else
        //    {
        //        this.LoadAdd();
        //    }
        //}

    }
    #endregion

    # region TDS Applicable
    protected void chktdsapplicable1_OnCheckedChanged(object sender, EventArgs e)
    {
        
        if (chktdsapplicable1.Checked == false)
        {
            MessageBox1.ShowInfo("TDS is applicable for " + ddltransporter.SelectedItem.Text + " in the master from", 50, 400);
            reason.Style["Display"] = "";
            txttdspercentage.Text = "0";
            TxtTds.Text = "0";
            TDScalculate();            
        }
        else if (chktdsapplicable1.Checked == true)
        {
            
            TDScalculate();
            
        }
        Rcmcheck();
    }
    #endregion

    #region GST Applicable
    protected void chkcstapplicable_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkcstapplicable.Checked == false)
        {
            txtcgstvalue.Text = "0";
            txtcgstpercentage.Text = "0";
            txtsgstpercentage.Text = "0";
            txtsgstvalue.Text = "0";
            txtigstpercentage.Text = "0";
            txtigstvalue.Text = "0";
            LoadTaxes();
        }
        else
        {
            LoadTaxes();
        }
    }
     #endregion

    public void clearsum()
    {
        this.txtsumnetamt.Text = "";
        this.txtsumgrossweight.Text = "";
        this.txtsumtds.Text = "";
   
        this.txtsumtdsdeuctablevalue.Text = "";
        this.txtsumbillvalue.Text = "";
        this.txtsumcgst.Text = "0.00";
        this.txtsumsgst.Text = "0.00";
        this.txtsumigst.Text = "0.00";
        this.txtsumugst.Text = "0.00";

    }
    public void clearadd()
    {

        this.Txtamount.Text = "";

        this.txtCheckerNote.Visible = false;
        this.lblCheckerNote.Visible = false;

        this.txtBillvalue.Text = "";
        this.txttdsdeductablevalue.Text = "";
        this.txtCustomerNameFromInovice.Text = "";
        this.txtInvAmnt.Text = "0";
        this.txtPartyGstNo.Text = "";
        this.txtremarks.Text = "";
        this.txtlrdate.Text = "";
        this.TxtTds.Text = "";
        this.lbltds.Visible = false;

        this.tdsspan.Visible = true;

        this.txtcgstpercentage.Text = "";
        this.txtcgstvalue.Text = "";
        this.hdncgsttaxpercentage.Value = "";


        this.txtsgstpercentage.Text = "";
        this.txtsgstvalue.Text = "";
        this.hdnsgsttaxpercentage.Value = "";

        this.txtigstpercentage.Text = "";
        this.txtigstvalue.Text = "";
        this.hdnigsttaxpercentage.Value = "";

        this.hdnugsttaxpercentage.Value = "";

        this.txtugstpercentage.Text = "0";
        this.txtugstvalue.Text = "0";
        this.hdnugsttaxpercentage.Value = "0";

        this.txttdspercentage.Text = "";

        this.txtgrossweight.Text = "";

        this.gvtransporterbill.ClearPreviousDataSource();
        this.gvtransporterbill.DataSource = null;
        this.gvtransporterbill.DataBind();
        this.gvtransporterbill.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;

        this.ddlinvoice.SelectedValue = "0";
        this.txtlrno.Text = "";



    }

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("LRGRNO", typeof(string)));
        dt.Columns.Add(new DataColumn("LRGRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("INVID", typeof(string)));
        dt.Columns.Add(new DataColumn("INVNO", typeof(string)));
        dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("BILLNO", typeof(string)));
        dt.Columns.Add(new DataColumn("BILLAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("TDSPERCENTRAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TDS", typeof(string)));



        dt.Columns.Add(new DataColumn("CGSTID", typeof(string)));
        dt.Columns.Add(new DataColumn("CGSTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("CGSTAX", typeof(string)));


        dt.Columns.Add(new DataColumn("SGSTID", typeof(string)));
        dt.Columns.Add(new DataColumn("SGSTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("SGSTAX", typeof(string)));

        dt.Columns.Add(new DataColumn("IGSTID", typeof(string)));
        dt.Columns.Add(new DataColumn("IGSTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("IGSTAX", typeof(string)));


        dt.Columns.Add(new DataColumn("UGSTID", typeof(string)));
        dt.Columns.Add(new DataColumn("UGSTPERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("UGSTAX", typeof(string)));

        dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("TDSID", typeof(string)));

        dt.Columns.Add(new DataColumn("BILLDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TDSDEDUCTABLEAMOUNT", typeof(string)));

        dt.Columns.Add(new DataColumn("BILLINGTOSTATEID", typeof(string)));
        dt.Columns.Add(new DataColumn("BILLINGTOSTATENAME", typeof(string)));

        dt.Columns.Add(new DataColumn("TRANSPORTERID", typeof(string)));
        dt.Columns.Add(new DataColumn("TRANSPORTERNAME", typeof(string)));
        HttpContext.Current.Session["TRANSPORTERBILL"] = dt;
        return dt;
    }
    #endregion

    #region Billing For
    protected void rdbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsTransporterBill transporterbill = new ClsTransporterBill();

            if (rdbtype.SelectedValue == "STKINV")
            {
                this.tdmodule.Visible = false;
            }
            else if (rdbtype.SelectedValue == "STKTRN")
            {
                //gvtransporterAdd.Columns[2].HeaderText = "TRANSFER NO";
                gvtransporterAdd.Columns[2].HeaderText = "BILL DATE";
                this.tdmodule.Visible = false;
            }
            else if (rdbtype.SelectedValue == "DPTRCVD")
            {
                //gvtransporterAdd.Columns[2].HeaderText = "RECEIVED NO";
                gvtransporterAdd.Columns[2].HeaderText = "BILL DATE";
                this.tdmodule.Visible = false;
            }
            else if (rdbtype.SelectedValue == "STKDES")
            {
                //gvtransporterAdd.Columns[2].HeaderText = "DESPATCH NO";                
                gvtransporterAdd.Columns[2].HeaderText = "BILL DATE";
                this.tdmodule.Visible = false;
            }

            string depot = string.Empty;
            //if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
            //{
            //    this.allLoadInvoiceNo(this.rdbtype.SelectedValue.Trim(), this.ddltransporter.SelectedValue.Trim(), this.hdntrnsID.Value.Trim(),"E");
            //}
            //else
            //{
            this.allLoadInvoiceNo(this.rdbtype.SelectedValue.Trim(), this.ddltransporter.SelectedValue.Trim(), this.hdntrnsID.Value.Trim());
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
     #endregion

    #region Billing from
      private void LoadState()
    {
        ClsTransporterBill clstranspoter = new ClsTransporterBill();
        DataTable dt = clstranspoter.BindState();
        if (dt.Rows.Count > 0)
        {
            this.ddlfromstate.Items.Clear();
            this.ddlfromstate.Items.Insert(0, new ListItem("Select State", "0"));
            this.ddlfromstate.DataSource = dt;
            this.ddlfromstate.DataTextField = "STATE_NAME";
            this.ddlfromstate.DataValueField = "STATE_ID";
            this.ddlfromstate.DataBind();

            this.ddltostate.Items.Clear();
            this.ddltostate.Items.Insert(0, new ListItem("Select State", "0"));
            this.ddltostate.DataSource = dt;
            this.ddltostate.DataTextField = "STATE_NAME";
            this.ddltostate.DataValueField = "STATE_ID";
            this.ddltostate.DataBind();
        }
        else
        {
            this.ddltostate.Items.Clear();
            this.ddltostate.Items.Insert(0, new ListItem("Select State", "0"));
            this.ddltostate.Enabled = true;

            this.ddltostate.Items.Clear();
            this.ddltostate.Items.Insert(0, new ListItem("Select State", "0"));
            this.ddltostate.Enabled = true;
        }
    }
      #endregion

    #region Depot
      private void LoadDepot()
    {
        ClsTransporterBill clstranspoter = new ClsTransporterBill();
        DataTable dt = clstranspoter.BindDepot(HttpContext.Current.Session["UserID"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddldepot.DataSource = dt;
            this.ddldepot.DataTextField = "BRNAME";
            this.ddldepot.DataValueField = "BRID";
            this.ddldepot.DataBind();


            if (dt.Rows.Count == 1)
            {
                this.ddldepot.SelectedValue = dt.Rows[0]["BRID"].ToString().Trim();
                
                loadtransporter();
                //loadtransporterv2();
                this.ddldepot.Enabled = false;
            }


        }
        else
        {
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddldepot.Enabled = true;
        }
    }
      #endregion

    #region Sending Depot
      private void LoadExporDepot()
    {
        ClsTransporterBill clstranspoter = new ClsTransporterBill();
        DataTable dt = clstranspoter.BindExportDepot();
        if (dt.Rows.Count > 0)
        {
            this.ddlexportdepot.Items.Clear();
            this.ddlexportdepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlexportdepot.DataSource = dt;
            this.ddlexportdepot.DataTextField = "BRPREFIX";
            this.ddlexportdepot.DataValueField = "BRID";
            this.ddlexportdepot.DataBind();
        }
        else
        {
            this.ddlexportdepot.Items.Clear();
            this.ddlexportdepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlexportdepot.Enabled = true;
        }
    }
      #endregion

    protected void ddlfromstate_SelectedIndexChanged(object sender, EventArgs e)
      {
          ClsTransporterBill transporterbill = new ClsTransporterBill();
          
          if (ddlfromstate.SelectedValue != "0")
          {
              #region IGST/UGST/SGST

              string depot = string.Empty;
              if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
              {
                  this.tdlblexportdepot.Style["display"] = "";
                  this.tdddlexportdepot.Style["display"] = "";
                  depot = this.ddlexportdepot.SelectedValue.Trim();


              }
              else
              {
                  this.tdlblexportdepot.Style["display"] = "none";
                  this.tdddlexportdepot.Style["display"] = "none";
                  depot = this.ddldepot.SelectedValue.Trim();
              }

              decimal tdsdeductablevalue = 0;
              string rdbType = string.Empty;
             
              if (this.txttdsdeductablevalue.Text != "")
              {
                  tdsdeductablevalue = Convert.ToDecimal(this.txttdsdeductablevalue.Text);
              }
              if (this.rdbtype.SelectedValue != "")
              {
                  rdbType = Convert.ToString(this.rdbtype.SelectedValue).Trim();
              }
           


              DataTable dt = transporterbill.BindGstPercentage(depot.Trim(), this.ddltransporter.SelectedValue.Trim(), tdsdeductablevalue, Convert.ToString(this.ddlinvoice.SelectedValue), Convert.ToString(rdbtype).Trim(), Convert.ToInt32(this.ddlfromstate.SelectedValue), Convert.ToInt32(this.ddltostate.SelectedValue));

              if (dt.Rows.Count == 1)
              {
                  decimal cgstamt = 0;
                  decimal sgstamt = 0;
                  decimal igst = 0;
                  decimal ugst = 0;

                  this.txtcgstpercentage.Text = Convert.ToString(dt.Rows[0]["CGST_PERCENTAGE"]).Trim();

                  if (chkcstapplicable.Checked == true)
                  {
                      cgstamt = transporterbill.RoundOff(tdsdeductablevalue * Convert.ToDecimal(txtcgstpercentage.Text)) / 100;
                      hdncgsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["CGSTID"]).Trim();
                  }
                  else
                  {
                      this.txtcgstpercentage.Text = "0";
                      hdncgsttaxpercentage.Value = "0";
                  }
                  this.txtcgstvalue.Text = Convert.ToString(cgstamt).Trim();

                  this.txtsgstpercentage.Text = Convert.ToString(dt.Rows[0]["SGST_PERCENTAGE"]).Trim();
                  if (chkcstapplicable.Checked == true)
                  {
                      sgstamt = transporterbill.RoundOff(tdsdeductablevalue * Convert.ToDecimal(txtsgstpercentage.Text)) / 100;
                      hdnsgsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["SGSTID"]).Trim();
                  }
                  else
                  {
                      this.txtsgstpercentage.Text = "0";
                      hdnsgsttaxpercentage.Value = "0";
                  }
                  this.txtsgstvalue.Text = Convert.ToString(sgstamt).Trim();


                  this.txtigstpercentage.Text = Convert.ToString(dt.Rows[0]["IGST_PERCENTAGE"]).Trim();
                  if (chkcstapplicable.Checked == true)
                  {
                      igst = transporterbill.RoundOff( tdsdeductablevalue * Convert.ToDecimal(txtigstpercentage.Text)) / 100;
                      hdnigsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["IGSTID"]).Trim();
                  }
                  else
                  {
                      this.txtigstpercentage.Text = "0";
                      hdnigsttaxpercentage.Value = "0";
                  }
                  this.txtigstvalue.Text = Convert.ToString(igst).Trim();



                  this.txtugstpercentage.Text = Convert.ToString(dt.Rows[0]["UGST_PERCENTAGE"]).Trim();
                  if (chkcstapplicable.Checked == true)
                  {
                      ugst = transporterbill.RoundOff( tdsdeductablevalue * Convert.ToDecimal(txtugstpercentage.Text)) / 100;
                      hdnugsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["UGSTID"]).Trim();
                  }
                  else
                  {
                      this.txtugstpercentage.Text = "0";
                  }
                  this.txtugstvalue.Text = Convert.ToString(ugst).Trim();

              }
              else
              {
                  this.txtcgstpercentage.Text = "0";
                  this.txtsgstpercentage.Text = "0";
                  this.txtigstpercentage.Text = "0";
                  this.txtcgstvalue.Text = "0";
                  this.txtsgstvalue.Text = "0";
                  this.txtigstvalue.Text = "0";
              }
              #endregion
              decimal Billvalue = 0;
              if (this.txtBillvalue.Text.Trim() != "")
              {
                  Billvalue = Convert.ToDecimal(txtBillvalue.Text.Trim());
              }
              Txtamount.Text = (Billvalue + Convert.ToDecimal(txtcgstvalue.Text) + Convert.ToDecimal(txtsgstvalue.Text) + Convert.ToDecimal(txtigstvalue.Text) + Convert.ToDecimal(txtugstvalue.Text)).ToString();
              //Txtamount.Text = (Convert.ToDecimal(txtBillvalue.Text) - Convert.ToDecimal(tdsvalue.Text) + Convert.ToDecimal(txtcgstvalue.Text) + Convert.ToDecimal(txtsgstvalue.Text) + Convert.ToDecimal(txtigstvalue.Text) + Convert.ToDecimal(txtugstvalue.Text)).ToString();
              ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtgrossweight.ClientID + "').focus(); ", true);
              //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.btnADDGrid.ClientID + "').focus(); ", true);
            
          }
      }
     

    private void loadtransporterv2()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();

        string depot = string.Empty;
        if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
        {
            this.tdlblexportdepot.Style["display"] = "";
            this.tdddlexportdepot.Style["display"] = "";
            depot = this.ddlexportdepot.SelectedValue.Trim();

            if (depot == "0")
            {
                MessageBox1.ShowInfo("Please Select Sending Depot");
                return;
            }
        }
        else
        {
            this.tdlblexportdepot.Style["display"] = "none";
            this.tdddlexportdepot.Style["display"] = "none";
            depot = this.ddldepot.SelectedValue.Trim();
        }
        if (depot != "0")
        {
            DataTable dt = transporterbill.BindTransporter(depot.Trim());
            if (dt.Rows.Count > 0)
            {
                ddltransporter.Items.Clear();
                ddltransporter.Items.Insert(0, new ListItem("Select", "0"));
                ddltransporter.DataSource = dt;
                ddltransporter.DataTextField = "NAME";
                ddltransporter.DataValueField = "ID";
                ddltransporter.DataBind();
            }
            else
            {
                ddltransporter.Items.Clear();
                ////////ddltransporter.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        //else
        //{
        //    MessageBox1.ShowInfo("Please select Depot");
        //    return;
        //}
    }

    private void loadtransporter()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        DataTable dt = transporterbill.BindTransporter();
        if (dt.Rows.Count > 0)
        {
            ddltransporter.Items.Clear();
            ddltransporter.Items.Insert(0, new ListItem("Select", "0"));
            ddltransporter.DataSource = dt;
            ddltransporter.DataTextField = "NAME";
            ddltransporter.DataValueField = "ID";
            ddltransporter.DataBind();
        }
        else
        {
            ddltransporter.Items.Clear();
            ddltransporter.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tag = string.Empty;
        tag = transporterbill.GetOfflineStatus(this.ddldepot.SelectedValue.Trim());
        if (tag == "Y")
        {
            MessageBox1.ShowInfo("Bill entry is available in offline system of " + ddldepot.SelectedItem.Text + " ", 50, 400);
            this.ddldepot.SelectedValue = "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670";
            ddldepot.SelectedItem.Attributes.CssStyle.Add("color", "blue");
        }
        else
        {

            if (this.ddldepot.SelectedValue != "0")
            {

                if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
                {
                    this.tdlblexportdepot.Style["display"] = "";
                    this.tdddlexportdepot.Style["display"] = "";

                }
                else
                {
                    this.tdlblexportdepot.Style["display"] = "none";
                    this.tdddlexportdepot.Style["display"] = "none";
                }

                this.ddlinvoice.Items.Clear();
                this.ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        this.loadtransporterv2();
    }


    private void loadlrnowithrespectinvono()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        DataTable dt = transporterbill.Bindlrnowithrespectinvono(this.rdbtype.SelectedValue.Trim(), this.ddlinvoice.SelectedValue.Trim());
        if (dt.Rows.Count > 0)
        {
            //ddlLRNo.Items.Clear();
            //ddlLRNo.Items.Insert(0, new ListItem("Select", "0"));
            //ddlLRNo.DataSource = dt;
            //ddlLRNo.DataTextField = "LRGRNO";
            //ddlLRNo.DataValueField = "LRID";
            //ddlLRNo.DataBind();

            //ddlLRNo.AppendDataBoundItems = true;
        }
        else
        {
            //ddlLRNo.Items.Clear();
            //ddlLRNo.Items.Insert(0, new ListItem("Select", "0"));

        }
    }

    #region ddlinvoice_SelectedIndexChanged
    protected void ddlinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsTransporterBill clstransporter = new ClsTransporterBill();
            DataTable dt = new DataTable();
            DataTable dtInvoice = new DataTable();
            ClsVendor_TPU clsTpu = new ClsVendor_TPU();
            string mode = "billAmnt";
            if (Convert.ToString(ViewState["transporterid"]) == "")
            {
                
                dt = clstransporter.BindTransporterbyInvoice(this.rdbtype.SelectedValue, this.ddlinvoice.SelectedValue.Trim());
                dtInvoice = clsTpu.BindGateEntry(mode, this.ddlinvoice.SelectedValue.Trim());
                loadtransporterv2();
                if (dt.Rows.Count > 0)
                {
                    ddltransporter.SelectedValue = dt.Rows[0]["TRANSPORTERID"].ToString();
                    //ViewState["transporterid"] = ddltransporter.SelectedValue;
                    txtlrno.Text = dt.Rows[0]["LRGRNO"].ToString();
                    txtlrdate.Text = dt.Rows[0]["LRGRDATE"].ToString();
                    txtCustomerNameFromInovice.Text = dt.Rows[0]["DISTRIBUTORNAME"].ToString();
                    txtInvAmnt.Text = dtInvoice.Rows[0]["INVAMNT"].ToString();
                    txtPartyGstNo.Text = dtInvoice.Rows[0]["GSTNO"].ToString();

                }
                else
                {
                    txtlrno.Text = "";
                    txtlrdate.Text = "";    
                    txtCustomerNameFromInovice.Text = "";
                }
            }
            else
            {              
                ddltransporter.SelectedValue = Convert.ToString(ViewState["transporterid"]);
                dt = clstransporter.BindTransporterbyInvoice(this.rdbtype.SelectedValue, this.ddlinvoice.SelectedValue.Trim());
                dtInvoice = clsTpu.BindGateEntry(mode, this.ddlinvoice.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    txtlrno.Text = dt.Rows[0]["LRGRNO"].ToString();
                    txtlrdate.Text = dt.Rows[0]["LRGRDATE"].ToString();
                    txtCustomerNameFromInovice.Text = dt.Rows[0]["DISTRIBUTORNAME"].ToString();
                    txtInvAmnt.Text = dtInvoice.Rows[0]["INVAMNT"].ToString();
                    txtPartyGstNo.Text = dtInvoice.Rows[0]["GSTNO"].ToString();
                }
                else
                {
                    txtlrno.Text = "";
                    txtlrdate.Text = "";
                    txtCustomerNameFromInovice.Text = "";
                    txtInvAmnt.Text = "0";
                    txtPartyGstNo.Text = "";
                }
            }

            GetTdscheck();
            GetReverseCharge();
            GetIsTransferToHo();
            Rcmcheck();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion



    #region LoadTransporterBill
    protected void LoadTransporterBill()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string Checker = string.Empty;
        DataTable dt = (DataTable)HttpContext.Current.Session["TRANSPORTERBILL"];
        Checker = Request.QueryString["CHECKER"].ToString().Trim();

        //if (Session["TPU"].ToString() == "ADMIN")
        //{
        //    dt = transporterbill.BindCheakerTransporterbill(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim());
        //}
        //else
        //{
        //    dt = transporterbill.BindTransporterbill(HttpContext.Current.Session["UserID"].ToString().Trim(), Checker, this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim());

       // }
        dt = transporterbill.BindTransporterbillall(this.ddlregion.SelectedValue.Trim(), this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            //string offline = transporterbill.GetOfflineStatus(this.ddlregion.SelectedValue.Trim());
            //if (offline == "Y")
            //{
            //    this.btnsubmitdiv.Visible = false;
            //    this.gvtransporterbill.Columns[21].Visible = false;
            //}
            //else
            //{
            //    this.btnsubmitdiv.Visible = true;
            //    this.gvtransporterbill.Columns[21].Visible = true;
            //}
            this.gvtransporterbill.DataSource = dt;
            this.gvtransporterbill.DataBind();

            

        }
        else
        {
            this.gvtransporterbill.ClearPreviousDataSource();
            this.gvtransporterbill.DataSource = null;
            this.gvtransporterbill.DataBind();
        }
    }
    #endregion

    #region Search
    protected void btnSearchtranporterbill_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadTransporterBill();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    private void allLoadInvoiceNo(string tag, string transpoter, string trnsID)
    {
        ClsTransporterBill CLSTRANBILL = new ClsTransporterBill();
        DataTable DT = new DataTable();
        string transporterid = string.Empty;       

        string depotid = string.Empty;
        transporterid = transpoter;
      
        DT = CLSTRANBILL.BindAllInvoicenoNEWV2(tag.Trim(), transporterid.Trim(), this.ddldepot.SelectedValue.Trim(), this.hdntrnsID.Value.Trim());
        
        if (DT.Rows.Count > 0)
        {
            ddlinvoice.Items.Clear();
            ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
            ddlinvoice.DataSource = DT;
            ddlinvoice.DataTextField = "INVNO";
            ddlinvoice.DataValueField = "INVID";
            ddlinvoice.DataBind();


        }
        else
        {
            ddlinvoice.Items.Clear();
            ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    private void LoadInvoiceNo(string tag, string lrno)
    {
        ClsTransporterBill CLSTRANBILL = new ClsTransporterBill();
        DataTable DT = CLSTRANBILL.BINDINVOICENOV2(tag.Trim(), lrno.Trim());
        if (DT.Rows.Count > 0)
        {
            ddlinvoice.Items.Clear();
            ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
            ddlinvoice.DataSource = DT;
            ddlinvoice.DataTextField = "INVNO";
            ddlinvoice.DataValueField = "INVID";
            ddlinvoice.DataBind();


        }
        else
        {
            ddlinvoice.Items.Clear();
            ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    #region btnADDGrid_Click
    protected void btnADDGrid_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            ClsTransporterBill transporterbill = new ClsTransporterBill();
            DataTable dttransporterbill = new DataTable();
            DataTable dtsum = new DataTable();
            string TYPEID = string.Empty;
            string TYPENO = string.Empty;
            string Tag = string.Empty;
            string Lrgrno = string.Empty;
            int INVNO = 0;
            int ckhlrgrno = 0;
            string transporter = string.Empty;

            rdbtype.Enabled = false;
            ddltransporter.Enabled = false;
            ddlfromstate.Enabled = false;

            if (txtgrossweight.Text == "")
            {
                this.txtgrossweight.Text = "0";
            }
           
            ddldepot.Enabled = false;

            string depotid = string.Empty;
            
            if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
            {
                
                depotid = this.ddlexportdepot.SelectedValue.Trim();
                ddlexportdepot.Enabled = false;
                
                if(this.ddlexportdepot.SelectedValue=="0")
                {
                    MessageBox1.ShowInfo("Please Select Sending depot");
                }
            }
            else
            {
               
                depotid = this.ddldepot.SelectedValue.Trim();
               
            }

            if (Session["TRANSPORTERBILL"] != null)
            {
                dttransporterbill = (DataTable)HttpContext.Current.Session["TRANSPORTERBILL"];
            }
            gettaxid();

            if (ddlfromstate.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select Billing From");
                return;
            }
            if (this.txtlrno.Text =="")
            {
                MessageBox1.ShowInfo("LrGr no required");
                return;
            }


            string checking = string.Empty;

            //dt = transporterbill.checkLRGRNO(rdbtype.SelectedValue.Trim(),ddltransporter.SelectedValue.Trim(),txtlrno.Text.Trim());

            checking = transporterbill.checkLRGRNOV2(rdbtype.SelectedValue.Trim(), ddltransporter.SelectedValue.Trim(), txtlrno.Text.Trim(),
                                                this.hdntrnsID.Value.Trim(), this.ddlinvoice.SelectedValue.Trim());


            INVNO = dttransporterbill.Select("INVID='" + Convert.ToString(this.ddlinvoice.SelectedValue).Trim() + "'").Length;
            ckhlrgrno = dttransporterbill.Select("INVID='" + Convert.ToString(this.ddlinvoice.SelectedValue).Trim() + "'").Length;
            transporter = Convert.ToString(this.ddltransporter.SelectedValue).Trim();
            if (checking == "0")
            {

                if (ViewState["transporterid"] == null)
                {
                    ViewState["transporterid"] = transporter;
                }

                if (INVNO > 0)
                {
                    if (rdbtype.SelectedValue == "STKINV")
                    {
                        MessageBox1.ShowInfo("Same Invoice No. already exists...");
                    }
                    else if (rdbtype.SelectedValue == "STKTRN")
                    {
                        MessageBox1.ShowInfo("Same Transfer No. already exists...");
                    }
                    else if (rdbtype.SelectedValue == "DPTRCVD")
                    {
                        MessageBox1.ShowInfo("Same Depot Received No. already exists...");
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Same Stock Received No. already exists...");
                    }

                    return;
                }
                else
                {

                    DataRow dr = dttransporterbill.NewRow();

                    dr["GUID"] = Guid.NewGuid();

                   
                        
                        dr["INVID"] = ddlinvoice.SelectedValue.Trim();
                        dr["INVNO"] = ddlinvoice.SelectedItem.ToString().Trim();

                        dr["BILLINGTOSTATEID"] = "0";
                        dr["BILLINGTOSTATENAME"] = "0";
                    
                    dr["LRGRNO"] = this.txtlrno.Text.Trim();
                    dr["LRGRDATE"] = this.txtlrdate.Text.Trim();
                    dr["GROSSWEIGHT"] = Convert.ToDecimal(this.txtgrossweight.Text).ToString().Trim();
                    dr["BILLNO"] = Convert.ToString(this.Txtbillno.Text).Trim();
                    dr["BILLAMOUNT"] = Convert.ToDecimal(this.txtBillvalue.Text).ToString().Trim();
                    dr["TDSPERCENTRAGE"] = Convert.ToDecimal(this.txttdspercentage.Text).ToString().Trim();
                    dr["TDS"] = Convert.ToDecimal(this.TxtTds.Text).ToString().Trim();//tds value

                    dr["CGSTID"] = this.hdncgsttaxpercentage.Value;
                    dr["CGSTPERCENTAGE"] = Convert.ToString(this.txtcgstpercentage.Text).Trim();
                    dr["CGSTAX"] = Convert.ToString(this.txtcgstvalue.Text).Trim(); 

                    dr["SGSTID"] = this.hdnsgsttaxpercentage.Value;
                    dr["SGSTPERCENTAGE"] = Convert.ToString(this.txtsgstpercentage.Text).Trim();
                    dr["SGSTAX"] = Convert.ToString(this.txtsgstvalue.Text).Trim();


                    dr["IGSTID"] = this.hdnigsttaxpercentage.Value;
                    dr["IGSTPERCENTAGE"] = Convert.ToString(this.txtigstpercentage.Text).Trim();
                    dr["IGSTAX"] = Convert.ToString(this.txtigstvalue.Text).Trim();

                    dr["UGSTID"] = this.hdnugsttaxpercentage.Value;
                    dr["UGSTPERCENTAGE"] = Convert.ToString(this.txtugstpercentage.Text).Trim();
                    dr["UGSTAX"] = Convert.ToString(this.txtugstvalue.Text).Trim();

                    dr["NETAMOUNT"] = Convert.ToDecimal(this.Txtamount.Text).ToString().Trim();
                    dr["TDSID"] = Convert.ToString(hdn_tdsis.Value).Trim();

                    dr["BILLDATE"] = Convert.ToString(this.txtbilldate2.Text).Trim();
                    dr["TDSDEDUCTABLEAMOUNT"] = Convert.ToDecimal(this.txttdsdeductablevalue.Text).ToString().Trim();

                    dr["TRANSPORTERID"] = Convert.ToString(this.ddltransporter.SelectedValue).Trim();
                    dr["TRANSPORTERNAME"] = Convert.ToString(this.ddltransporter.SelectedItem.Text).Trim();

                    dttransporterbill.Rows.Add(dr);
                    dttransporterbill.AcceptChanges();

                    decimal sumtdsdeductableamount = 0;
                    decimal sumbillamount = 0;
                    decimal sumnetamount = 0;
                    decimal sumgrossweight = 0;
                    decimal sumtds1 = 0;
                    decimal sumcgst = 0;
                    decimal sumsgst = 0;
                    decimal sumigst = 0;
                    decimal sumugst = 0;
                    //decimal sumnetamountshow = 0;
                    for (int i = 0; i < dttransporterbill.Rows.Count; i++)
                    {


                        sumgrossweight = sumgrossweight + Convert.ToDecimal(dttransporterbill.Rows[i]["GROSSWEIGHT"].ToString().Trim());
                        sumtds1 = sumtds1 + Convert.ToDecimal(dttransporterbill.Rows[i]["TDS"].ToString().Trim());
                        sumcgst = sumcgst + Convert.ToDecimal(dttransporterbill.Rows[i]["CGSTAX"].ToString().Trim());
                        sumsgst = sumsgst + Convert.ToDecimal(dttransporterbill.Rows[i]["SGSTAX"].ToString().Trim());
                        sumigst = sumigst + Convert.ToDecimal(dttransporterbill.Rows[i]["IGSTAX"].ToString().Trim());
                        sumugst = sumugst + Convert.ToDecimal(dttransporterbill.Rows[i]["UGSTAX"].ToString().Trim());
                        sumbillamount = (Convert.ToDecimal(dttransporterbill.Rows[i]["BILLAMOUNT"].ToString().Trim()) + sumbillamount);
                        sumtdsdeductableamount = (Convert.ToDecimal(dttransporterbill.Rows[i]["TDSDEDUCTABLEAMOUNT"].ToString().Trim()) + sumtdsdeductableamount);
                        sumnetamount = sumnetamount + Convert.ToDecimal(dttransporterbill.Rows[i]["NETAMOUNT"].ToString().Trim());
                        //sumnetamountshow = sumnetamountshow + Convert.ToDecimal(dttransporterbill.Rows[i]["NETAMOUNT"].ToString().Trim());

                    }
                    this.txtsumnetamt.Text = sumnetamount.ToString();
                    this.txtsumgrossweight.Text = sumgrossweight.ToString();
                    this.txtsumtds.Text = sumtds1.ToString();
                    this.txtsumcgst.Text = sumcgst.ToString();
                    this.txtsumsgst.Text = sumsgst.ToString();
                    this.txtsumigst.Text = sumigst.ToString();
                    this.txtsumugst.Text = sumugst.ToString();
                    this.txtsumbillvalue.Text = sumbillamount.ToString();
                    this.txtsumtdsdeuctablevalue.Text = sumtdsdeductableamount.ToString();
                    //this.txtsumnetamtshow.Text = sumnetamountshow.ToString();
                    this.clearadd();
                    TDScalculate();
                    reason.Style["Display"] = "none";
                    Rcmcheck();
                   
                  
                }


              

                Session["TRANSPORTERBILL"] = dttransporterbill;
                if (dttransporterbill.Rows.Count > 0)
                {
                    this.gvtransporterAdd.DataSource = dttransporterbill;
                    this.gvtransporterAdd.DataBind();
                    this.Txtbillno.Enabled = false;
                }
                else
                {
                    this.gvtransporterAdd.ClearPreviousDataSource();
                    this.gvtransporterAdd.DataSource = null;
                    this.gvtransporterAdd.DataBind();
                    this.Txtbillno.Enabled = true;
                }
                if (rdbtype.SelectedValue != "Others")
                {

                }
                if (rdbtype.SelectedValue != "Others")
                {
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtlrno.ClientID + "').focus(); ", true);
                    this.txtlrno.Focus();
                }
            }
            else
            {
                MessageBox1.ShowInfo("LRGR No. already exists!!");
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngrddelete_Click
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
           
            ClsTransporterBill transporterbill = new ClsTransporterBill();
            string GUID = Convert.ToString(hdndtDelete.Value);
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["TRANSPORTERBILL"];
           

            DataRow[] drr = dtdeleterecord.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleterecord.AcceptChanges();
            }
           
            
                if (dtdeleterecord.Rows.Count > 0)
                {
                    this.gvtransporterAdd.DataSource = dtdeleterecord;
                    this.gvtransporterAdd.DataBind();
                    
                    this.rdbtype.Enabled = false;
                    this.ddltransporter.Enabled = false;
                    this.ddlfromstate.Enabled = false;
                   
                    this.ddldepot.Enabled = false;
                    this.ddlexportdepot.Enabled = false;
                    this.Txtbillno.Enabled = false;
                }
                else
                {
                    this.gvtransporterAdd.DataSource = null;
                    this.gvtransporterAdd.DataBind();
                    
                    this.rdbtype.Enabled = true;
                    this.ddltransporter.Enabled = true;
                    this.ddlfromstate.Enabled = true;
                    
                    this.ddldepot.Enabled = true;
                    this.ddlexportdepot.Enabled = true;
                    this.Txtbillno.Enabled = true;
                    ViewState["transporterid"] = null;

                }  
            
            decimal sumnetamount = 0;
            decimal sumtdsdeductableamount = 0;
            decimal sumbillamount = 0;
            decimal sumgrossweight = 0;

            
            decimal sumcgst = 0;
            decimal sumsgst = 0;
            decimal sumigst = 0;
            

            for (int i = 0; i < dtdeleterecord.Rows.Count; i++)
            {
                sumnetamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["NETAMOUNT"].ToString().Trim()) + sumnetamount);
                sumgrossweight = (Convert.ToDecimal(dtdeleterecord.Rows[i]["GROSSWEIGHT"].ToString().Trim()) + sumgrossweight);
                //sumtds1 =( Convert.ToDecimal(dtdeleterecord.Rows[i]["TDS"].ToString().Trim()) +sumtds1);
                sumcgst = sumcgst + Convert.ToDecimal(dtdeleterecord.Rows[i]["CGSTAX"].ToString().Trim());
                sumsgst = sumsgst + Convert.ToDecimal(dtdeleterecord.Rows[i]["SGSTAX"].ToString().Trim());
                sumigst = sumigst + Convert.ToDecimal(dtdeleterecord.Rows[i]["IGSTAX"].ToString().Trim());
                //sumugst = sumugst + Convert.ToDecimal(dtdeleterecord.Rows[i]["UGSTAX"].ToString().Trim());
                sumbillamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["BILLAMOUNT"].ToString().Trim()) + sumbillamount);
                sumtdsdeductableamount = (Convert.ToDecimal(dtdeleterecord.Rows[i]["TDSDEDUCTABLEAMOUNT"].ToString().Trim()) + sumtdsdeductableamount);

            }

            if (chktdsapplicable1.Checked == true)
            {
                # region TDS calculate



                decimal tdspercent = 0;
                if (chktdsapplicable1.Checked == true)
                {
                    tdspercent = transporterbill.TDSPercentage(this.ddltransporter.SelectedValue, Convert.ToDecimal(sumtdsdeductableamount));
                    this.txtTDSpercentage1.Text = Convert.ToString(tdspercent) + " %";

                }
                else
                {
                    tdspercent = 0;

                }
                if (tdspercent == 0)
                {
                    hdntdspercentage.Value = tdspercent.ToString();

                    this.TxtTds.Text = "0";
                    this.txttdspercentage.Text = "0";
                    this.lbltds.Visible = true;
                    this.chktdsapplicable1.Checked = false;
                    //this.chktdsapplicable1.Enabled = false;
                    this.tdsspan.Visible = false;
                    this.txtTDSpercentage1.Text = "0.00 %";
                    this.txtsumtds.Text = "0.00";
                    this.txtsumnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtsumbillvalue.Text.Trim()) + Convert.ToDecimal(this.txtsumcgst.Text.Trim()) + Convert.ToDecimal(this.txtsumsgst.Text.Trim()) + Convert.ToDecimal(this.txtsumigst.Text.Trim()) + Convert.ToDecimal(this.txtsumugst.Text.Trim())));
                }
                else
                {
                    decimal tdsvalue = 0;

                    hdntdspercentage.Value = tdspercent.ToString();
                    this.txttdspercentage.Text = hdntdspercentage.Value;

                    if (chktdsapplicable1.Checked == true)
                    {
                        tdsvalue = (Convert.ToDecimal(sumtdsdeductableamount) * Convert.ToDecimal(txttdspercentage.Text.Trim())) / 100;
                        tdsvalue = (Math.Round((tdsvalue), MidpointRounding.AwayFromZero));

                    }
                    else
                    {
                        hdn_tdsis.Value = "0";
                        txttdspercentage.Text = "0";
                        this.txtsumtds.Text = "0";
                    }

                    //decimal tdsroundoffvalue = transporterbill.RoundOff(tdsvalue);
                    if (this.chktdsapplicable1.Checked == true)
                    {
                        double sumtds1 = 0;
                        sumtds1 = Math.Ceiling(Convert.ToDouble(tdsvalue));
                        this.txtsumtds.Text = Convert.ToString(sumtds1);
                        this.txtsumnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(sumnetamount) - Convert.ToDecimal(this.txtsumtds.Text.Trim())));
                        sumnetamount = Convert.ToDecimal(this.txtsumnetamt.Text.Trim());
                    }
                    this.lbltds.Visible = false;
                    this.tdsspan.Visible = true;
                }
                #endregion
            }

            this.txtsumnetamt.Text = sumnetamount.ToString();
            this.txtsumgrossweight.Text = sumgrossweight.ToString();
            //this.txtsumtds.Text = sumtds1.ToString();
            this.txtsumcgst.Text = sumcgst.ToString();
            this.txtsumsgst.Text = sumsgst.ToString();
            this.txtsumigst.Text = sumigst.ToString();
            //this.txtsumugst.Text = sumugst.ToString();
            this.txtsumbillvalue.Text = sumbillamount.ToString();
            this.txtsumtdsdeuctablevalue.Text = sumtdsdeductableamount.ToString();
           
          

            if (dtdeleterecord.Rows.Count == 0)
            {
                chktdsapplicable1.Checked = false;
                this.txtTDSpercentage1.Text = "0";
                this.txtsumnetamt.Text = "0";
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

    #region Conver_To_ISO
    public string ConvertDate(string dt)
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
    #endregion

    #region Transporter Bill Save
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtbillDate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                string menuID = Request.QueryString["MENUID"].ToString().Trim();
                DataTable dt = new DataTable();
                ClsTransporterBill transporterbill = new ClsTransporterBill();
                clsSaleInvoice clsInvc = new clsSaleInvoice();
                string trans2 = ddltransporter.SelectedValue;
                if (Session["TRANSPORTERBILL"] != null)
                {
                    dt = (DataTable)Session["TRANSPORTERBILL"];
                }

                //int success = 0;
                string xml = string.Empty;
                string transporterbillno = string.Empty;
                string Istaxpaid = string.Empty;
                char istransfertoho = 'N';
                string BillDate = string.Empty;
                int ID = 0;
                xml = ConvertDatatableToXML(dt);
                string Mode = string.Empty;
                gettaxid();

                int result = 0;
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Session["INVNO"] = Convert.ToString(dt.Rows[i]["INVNO"]).Trim();
                    count = count + 1;
                    Session["count"] = count;
                    decimal billamt = Convert.ToDecimal(Convert.ToString(dt.Rows[i]["BILLAMOUNT"]).Trim());
                    decimal SGSTPERCENTAGE = Convert.ToDecimal(Convert.ToString(dt.Rows[i]["SGSTPERCENTAGE"]).Trim());
                    decimal SGSTVALUE = transporterbill.RoundOff((billamt * SGSTPERCENTAGE) / 100);
                    decimal CGSTPERCENTAGE = Convert.ToDecimal(Convert.ToString(dt.Rows[i]["CGSTPERCENTAGE"]).Trim());
                    decimal CGSTVALUE = transporterbill.RoundOff((billamt * CGSTPERCENTAGE) / 100);
                    decimal IGSTPERCENTAGE = Convert.ToDecimal(Convert.ToString(dt.Rows[i]["IGSTPERCENTAGE"]).Trim());
                    decimal IGSTVALUE = transporterbill.RoundOff((billamt * IGSTPERCENTAGE) / 100);

                    decimal NETAMOUNT = Convert.ToDecimal(Convert.ToString(dt.Rows[i]["NETAMOUNT"]).Trim());

                    if (NETAMOUNT != (billamt + SGSTVALUE + CGSTVALUE + IGSTVALUE))
                    {
                        result = 1;
                        break;
                    }

                }

                if (result == 0)
                {
                    if (dt.Rows.Count > 0)
                    {

                        //BillDate = ConvertDate(this.txtbillDate.Text.Trim());
                        if (this.hdntrnsID.Value == "")
                        {
                            Mode = "A";
                        }
                        else
                        {
                            Mode = "U";
                        }


                        if (this.chktransferHO.Checked == true)
                        {
                            istransfertoho = 'Y';
                        }
                        else
                        {
                            istransfertoho = 'N';
                        }
                        string rdbtag = string.Empty;


                        rdbtag = this.rdbtype.SelectedValue.Trim();

                        string Reversecharge = string.Empty;
                        if (this.chkReversecharge.Checked == true)
                        {
                            Reversecharge = "Y";
                        }
                        else
                        {
                            Reversecharge = "N";
                        }
                        string tdsapplicable1 = string.Empty;
                        if (chktdsapplicable1.Checked)
                        {
                            tdsapplicable1 = "Y";
                        }
                        else
                        {
                            tdsapplicable1 = "N";
                        }

                        string depotid = string.Empty;
                        string depotname = string.Empty;
                        string VIRTUALdepotid = string.Empty;
                        string EXPORTTAG = string.Empty;
                        if (hdntrnsID.Value == "")
                        {

                            //if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
                            if (this.tdddlexportdepot.Style["display"] == "")
                            {
                                //this.tdlblexportdepot.Style["display"] = "";
                                //this.tdddlexportdepot.Style["display"] = "";
                                depotid = this.ddlexportdepot.SelectedValue.Trim();
                                depotname = this.ddlexportdepot.SelectedItem.ToString().Trim();
                                VIRTUALdepotid = this.ddldepot.SelectedValue.Trim();
                                EXPORTTAG = "Y";
                            }

                            else
                            {
                                //this.tdlblexportdepot.Style["display"] = "none";
                                //this.tdddlexportdepot.Style["display"] = "none";
                                depotid = this.ddldepot.SelectedValue.Trim();
                                depotname = this.ddldepot.SelectedItem.ToString().Trim();
                                VIRTUALdepotid = "0";
                                EXPORTTAG = "N";
                            }
                        }
                        else
                        {
                            DataTable ds = transporterbill.EditTransporterbill(hdntrnsID.Value);

                            //if (transporterbill.ExportDepotCheckingEdit(Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim()) == "1")
                            if (this.tdddlexportdepot.Style["display"] == "")
                            {

                                this.LoadExporDepot();
                                this.ddlexportdepot.SelectedValue = Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim();

                                depotid = this.ddlexportdepot.SelectedValue.Trim();
                                depotname = this.ddlexportdepot.SelectedItem.ToString().Trim();
                                this.ddldepot.SelectedValue = Convert.ToString(ds.Rows[0]["VIRTUALDEPOTID"]).Trim();
                                VIRTUALdepotid = this.ddldepot.SelectedValue;
                                EXPORTTAG = "Y";
                            }
                            else
                            {
                                //LoadDepot();
                                this.ddldepot.SelectedValue = Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim();
                                depotid = this.ddldepot.SelectedValue.Trim();
                                depotname = this.ddldepot.SelectedItem.ToString().Trim();
                                VIRTUALdepotid = "0";
                                EXPORTTAG = "N";
                            }

                        }

                        string GST_Reason = string.Empty;
                        string TDS_Reason = string.Empty;

                        if (td_rcmreason.Style["display"] != "none")
                        {
                            GST_Reason = ddlgstreason.SelectedValue.Trim();
                        }
                        else
                        {
                            GST_Reason = "0";
                        }

                        if (reason.Style["display"] != "none")
                        {
                            TDS_Reason = ddlreason.SelectedValue.Trim();
                        }
                        else
                        {
                            TDS_Reason = "0";
                        }

                        transporterbillno = transporterbill.SaveTransporterbillV2(this.hdntrnsID.Value.ToString(), Mode, ddltransporter.SelectedValue.ToString().Trim(),
                        ddltransporter.SelectedItem.ToString().Trim(), txtbillDate.Text.Trim(), rdbtag.Trim(), txtremarks.Text.Replace("'", "").Trim(),
                        HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), xml, menuID,
                        Convert.ToDecimal(txtsumnetamt.Text.Trim()), Convert.ToDecimal(txtsumtds.Text.Trim()),
                        Convert.ToDecimal(txtsumgrossweight.Text.Trim()), depotid.Trim(), depotname.Trim(), istransfertoho,
                        Convert.ToDecimal(txtsumbillvalue.Text.Trim()), Convert.ToDecimal(txtsumtdsdeuctablevalue.Text.Trim()), Convert.ToDecimal(this.txtsumcgst.Text.Trim()),
                        Convert.ToDecimal(this.txtsumsgst.Text.Trim()), Convert.ToDecimal(this.txtsumigst.Text.Trim()), Convert.ToDecimal(this.txtsumugst.Text.Trim()),
                        this.ddlfromstate.SelectedValue.Trim(), Reversecharge, txtGNGNO.Text.Trim(), tdsapplicable1.Trim(),
                         Convert.ToDecimal(this.txtTDSpercentage1.Text.Replace("%", "").Trim()), hdn_tdsis.Value.Trim(), VIRTUALdepotid.Trim(), EXPORTTAG, TDS_Reason, GST_Reason);


                        if (!string.IsNullOrEmpty(transporterbillno))
                        {
                            ID = transporterbill.UpdateLrGrNoFromTransporterBill(depotid.Trim(), xml, this.rdbtype.SelectedValue.Trim());
                            this.pnlAdd.Style["display"] = "none";
                            this.pnlDisplay.Style["display"] = "";
                            this.btnaddhide.Style["display"] = "";
                            this.hdnMode.Value = "";
                            //this.TxtTds.Enabled = true;
                            this.rdbtype.Enabled = true;
                            this.ddltransporter.Enabled = true;
                            ddlfromstate.Enabled = true;
                            this.lbltds.Visible = false;
                            this.tdsspan.Visible = true;
                            this.gvtransporterbill.ClearPreviousDataSource();
                            this.gvtransporterbill.DataSource = null;
                            this.gvtransporterbill.DataBind();
                            this.gvtransporterbill.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                            LoadTransporterBill();
                            // this.hdnservicetaxpercentage.Value = "0";
                            this.txtTDSpercentage1.Text = "0.00 %";
                            this.hdntdspercentage.Value = "0";
                            this.ddldepot.SelectedValue = "0";
                            this.ddldepot.Enabled = true;
                            this.chktransferHO.Checked =false;
                            //this.chkReversecharge.Checked = true;
                            this.hdntrnsID.Value = "";
                            this.ViewState["transporterid"] = null;
                            ddlexportdepot.SelectedValue = "0";


                            if (this.hdntrnsID.Value == "")
                            {
                                MessageBox1.ShowSuccess("<b>Transporter BillNo <font color='green'>" + transporterbillno + "</font> save successfully</b>", 50, 600);
                            }
                            else
                            {
                                MessageBox1.ShowSuccess("<b>Transporter BillNo <font color='green'>" + transporterbillno + "</font> updated successfully</b>", 50, 600);
                            }

                            this.hdntrnsID.Value = "";
                        }
                        else
                        {
                            // string message = "alert('Error on Saving record..')";
                            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            MessageBox1.ShowError("Error on  Saving record..");
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Please add atleast 1 record");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please check calulation of bill no: <b><font color='red'>" + Session["INVNO"] + "</font> in sl no: <font color='red'>" + Session["count"] + "</font> </b>", 50, 600);
                }
            }
            else
            {
                MessageBox1.ShowInfo("Entry Date is Locked, Please Contact to Admin", 60, 500);
            }
          

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Transporter Bill Edit
    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTransporterBill transporterbill = new ClsTransporterBill();
            string Checker = Request.QueryString["CHECKER"].ToString().Trim();

            /*if (Checker == "FALSE")
            {
               if (transporterbill.Getstatus(this.hdntrnsID.Value.Trim()) == "1")
                {
                    MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                    return;
                }
            }*/

            this.tr_billno.Visible = true;

            clearadd();

            this.hdntdspercentage.Value = "0";
            this.Txtbillno.Enabled = false;
            gvtransporterAdd.ClearPreviousDataSource();
            gvtransporterAdd.DataSource = null;
            gvtransporterAdd.DataBind();
            DataTable ds = new DataTable();
            CreateDataTable();
            rdbtype.Enabled = false;
            ddltransporter.Enabled = false;
            ddlfromstate.Enabled = false;
            this.ddlexportdepot.Enabled = false;
            this.ddldepot.Enabled = false;
           
            DataTable dtTransporterEdit = new DataTable();
            if (Session["TRANSPORTERBILL"] != null)
            {
                dtTransporterEdit = (DataTable)HttpContext.Current.Session["TRANSPORTERBILL"];
            }

            if (Checker == "TRUE")/*CHECKER*/
            {
                this.btnsubmitdiv.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.lblCheckerNote.Visible = false;
                this.txtCheckerNote.Visible = false;
                this.txtremarks.Enabled = false;
            }
            else
            {
                this.btnsubmitdiv.Visible = true;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.lblCheckerNote.Visible = true;
                this.txtCheckerNote.Visible = true;
                this.txtremarks.Enabled = true;

                if (transporterbill.Getstatusforedit(this.hdntrnsID.Value.Trim()) == "1" && transporterbill.Getstatus(this.hdntrnsID.Value.Trim()) == "1")
                {
                    this.btnsubmitdiv.Visible = false;
                    this.gvtransporterAdd.Columns[26].Visible = false;
                    this.chktdsapplicable1.Enabled = false;
                    this.chkReversecharge.Enabled = false;

                }
                else if (transporterbill.Getstatusforedit(this.hdntrnsID.Value.Trim()) == "0" && transporterbill.Getstatus(this.hdntrnsID.Value.Trim()) == "0")
                {
                    this.btnsubmitdiv.Visible = true;
                    this.gvtransporterAdd.Columns[26].Visible = true;
                    this.chktdsapplicable1.Enabled = true;
                    this.chkReversecharge.Enabled = true;
                }
                else
                {
                    this.btnsubmitdiv.Visible = false;
                    this.gvtransporterAdd.Columns[26].Visible = false;
                    this.chktdsapplicable1.Enabled = false;
                    this.chkReversecharge.Enabled = false;
                }
            }

            //***** Creating DataTable Structure************//
            //DataTable dtTransporterEdit = new DataTable();
            //dtTransporterEdit.Columns.Add(new DataColumn("LRGRNO", typeof(string)));
            //dtTransporterEdit.Columns.Add(new DataColumn("INVID", typeof(string)));
            //dtTransporterEdit.Columns.Add(new DataColumn("INVNO", typeof(string)));           
            //HttpContext.Current.Session["TRANSPORTERBILL"] = dt;
            //return dt;
            //dtTerritorryEdit = (DataTable)Session["CITYRECORDS"];
            //************************************************//
            string TransporterID = Convert.ToString(hdntrnsID.Value).Trim();
           
            ds = transporterbill.EditTransporterbill(TransporterID);

            if (ds.Rows.Count > 0)
            {
                DateTime dtcurr1 = DateTime.Now;
                string date1 = "dd/MM/yyyy";
                //this.txtbilldate2.Text = dtcurr1.ToString(date1).Replace('-', '/');
                this.txtbillentryno.Text = Convert.ToString(ds.Rows[0]["TRANSPORTERBILLNO"]).Trim();
                this.txtbillDate.Text = Convert.ToString(ds.Rows[0]["TRANSPORTERBILLDATE"]).Trim();
                this.txtGNGNO.Text = Convert.ToString(ds.Rows[0]["GNGNO"]).Trim();
                if (Convert.ToString(ds.Rows[0]["TDS_REASONID"]).Trim() != "")
                {
                    if (Convert.ToString(ds.Rows[0]["TDS_REASONID"]).Trim() != "0")
                    {
                        this.reason.Style["Display"] = "";
                        this.ddlreason.SelectedValue = Convert.ToString(ds.Rows[0]["TDS_REASONID"]).Trim();
                    }
                    else
                    {
                        this.reason.Style["Display"] = "none";
                    }
                }
                else
                {
                    this.reason.Style["Display"] = "none";
                }
                if (Convert.ToString(ds.Rows[0]["GST_REASONID"]).Trim() != "")
                {
                    if (Convert.ToString(ds.Rows[0]["GST_REASONID"]).Trim() != "0")
                    {
                        this.td_rcmreason.Style["Display"] = "";
                        this.ddlgstreason.SelectedValue = Convert.ToString(ds.Rows[0]["GST_REASONID"]).Trim();
                    }
                    else
                    {
                        this.td_rcmreason.Style["Display"] = "none";
                    }
                }
                else
                {
                    this.td_rcmreason.Style["Display"] = "none";
                }
                string rdbtypevalue = Convert.ToString(ds.Rows[0]["BILLTYPEID"]).Trim();
                this.rdbtype.SelectedValue = rdbtypevalue.Trim();
               
                if (rdbtype.SelectedValue == "STKINV")
                {
                    gvtransporterAdd.Columns[2].HeaderText = "INVOICE DATE";
                    gvtransporterAdd.Columns[6].HeaderText = "INVOICE NO";

                }
                else if (rdbtype.SelectedValue == "STKTRN")
                {
                    gvtransporterAdd.Columns[2].HeaderText = "TRANSFER DATE";
                    gvtransporterAdd.Columns[6].HeaderText = "TRANSFER NO";
                }
                else if (rdbtype.SelectedValue == "Others")
                {
                    gvtransporterAdd.Columns[2].HeaderText = "INVOICE DATE";
                    gvtransporterAdd.Columns[6].HeaderText = "INVOICE NO";
                }
                else
                {
                    gvtransporterAdd.Columns[2].HeaderText = "DESPATCH DATE";
                    gvtransporterAdd.Columns[6].HeaderText = "DESPATCH NO";
                }
                this.txtremarks.Text = Convert.ToString(ds.Rows[0]["REMARKS"]).Trim();
                this.txtCheckerNote.Text = Convert.ToString(ds.Rows[0]["NOTE"]).Trim();
                this.rdbtype_SelectedIndexChanged(sender, e);
                
                if ((Convert.ToString(ds.Rows[0]["EXPORTTAG"]).Trim()) == "Y")
                {
                    LoadDepot();
                    this.tdlblexportdepot.Style["display"] = "";
                    this.tdddlexportdepot.Style["display"] = "";
                    this.LoadExporDepot();
                    this.ddlexportdepot.SelectedValue = Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim();
                    this.ddlexportdepot.Enabled = false;
                    this.ddldepot.SelectedValue = Convert.ToString(ds.Rows[0]["VIRTUALDEPOTID"]).Trim();
                   

                }
                else
                {
                    this.tdlblexportdepot.Style["display"] = "none";
                    this.tdddlexportdepot.Style["display"] = "none";
                    LoadDepot();
                    this.ddldepot.SelectedValue = Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim();
                   
                }

                loadtransporter();
               
                this.ddltransporter.SelectedValue = Convert.ToString(ds.Rows[0]["TRANSPORTERID"]).Trim();
                ViewState["transporterid"] = this.ddltransporter.SelectedValue.Trim();

                this.LoadState();
                this.ddlfromstate.SelectedValue = Convert.ToString(ds.Rows[0]["BILLINGFROMSTATEID"]).Trim();
                this.Txtbillno.Text = Convert.ToString(ds.Rows[0]["BILLNO"]).Trim();

                if (rdbtype.SelectedValue != "Others")
                {

                    allLoadInvoiceNo(this.rdbtype.SelectedValue.Trim(), this.ddltransporter.SelectedValue.Trim(), TransporterID);
                }

                if (Convert.ToString(ds.Rows[0]["ISTRANSFERTOHO"]).Trim() == "Y")
                {
                    this.chktransferHO.Checked = true;
                }
                else
                {
                    this.chktransferHO.Checked = false;
                }


                if (Convert.ToString(ds.Rows[0]["REVERSECHARGE"]).Trim() == "Y")
                {
                    this.chkReversecharge.Checked = true;
                }
                else
                {
                    this.chkReversecharge.Checked = false;
                }

                string tag1 = string.Empty;
                tag1 = transporterbill.GetOfflineStatusDepot();
                if (tag1 == "Y")
                {
                    this.btnSubmit.Style["display"] = "";
                    this.btnsubmitdiv.Style["display"] = "";
                }
                else
                {
                    
                        this.btnSubmit.Style["display"] = "";
                        this.btnsubmitdiv.Style["display"] = "";
                    
                    
                }

                this.txtsumnetamt.Text = Convert.ToDecimal(ds.Rows[0]["TOTALNETAMOUNT"]).ToString().Trim();
                this.txtsumgrossweight.Text = Convert.ToDecimal(ds.Rows[0]["TOTALGROSSWEIGHT"]).ToString().Trim();
                this.txtsumtds.Text = Convert.ToDecimal(ds.Rows[0]["TOTALTDS"]).ToString().Trim();

                this.txtsumtdsdeuctablevalue.Text = Convert.ToDecimal(ds.Rows[0]["TOTALTDSDEDUCTABLE"]).ToString().Trim();
                this.txtsumbillvalue.Text = Convert.ToDecimal(ds.Rows[0]["TOTALBILLAMOUNT"]).ToString().Trim();

                this.txtsumcgst.Text = Convert.ToDecimal(ds.Rows[0]["TOTALCGST"]).ToString().Trim();
                this.txtsumsgst.Text = Convert.ToDecimal(ds.Rows[0]["TOTALSGST"]).ToString().Trim();
                this.txtsumigst.Text = Convert.ToDecimal(ds.Rows[0]["TOTALIGST"]).ToString().Trim();

                //this.LoadDepot();
                //this.ddldepot.SelectedValue = Convert.ToString(ds.Rows[0]["DEPOTID"]).Trim();



            }
            if (ds.Rows.Count > 0)
            {

                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    DataRow dr = dtTransporterEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["LRGRNO"] = Convert.ToString(ds.Rows[i]["LRGRNO"]).Trim();
                    dr["LRGRDATE"] = Convert.ToString(ds.Rows[i]["LRGRDATE"]).Trim();
                    dr["INVID"] = Convert.ToString(ds.Rows[i]["STOCKBILLID"]).Trim();
                    dr["INVNO"] = Convert.ToString(ds.Rows[i]["STOCKBILLNO"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToDecimal(ds.Rows[i]["GROSSWEIGHT"]).ToString().Trim();
                    dr["BILLNO"] = Convert.ToString(ds.Rows[i]["BILLNO"]).Trim();
                    dr["BILLAMOUNT"] = Convert.ToDecimal(ds.Rows[i]["BILLAMOUNT"]).ToString().Trim();
                    dr["TDSPERCENTRAGE"] = Convert.ToDecimal(ds.Rows[i]["TDSPERCENTRAGE"]).ToString().Trim();
                    dr["TDS"] = Convert.ToDecimal(ds.Rows[i]["TDS"]).ToString().Trim();//tds value

                    dr["CGSTID"] = Convert.ToString(ds.Rows[i]["CGSTID"]).Trim();
                    dr["CGSTPERCENTAGE"] = Convert.ToString(ds.Rows[i]["CGSTPERCENTAGE"]).Trim();
                    dr["CGSTAX"] = Convert.ToString(ds.Rows[i]["CGSTAX"]).Trim();

                    dr["SGSTID"] = Convert.ToString(ds.Rows[i]["SGSTID"]).Trim();
                    dr["SGSTPERCENTAGE"] = Convert.ToString(ds.Rows[i]["SGSTPERCENTAGE"]).Trim();
                    dr["SGSTAX"] = Convert.ToString(ds.Rows[i]["SGSTAX"]).Trim();


                    dr["IGSTID"] = Convert.ToString(ds.Rows[i]["IGSTID"]).Trim();
                    dr["IGSTPERCENTAGE"] = Convert.ToString(ds.Rows[i]["IGSTPERCENTAGE"]).Trim();
                    dr["IGSTAX"] = Convert.ToString(ds.Rows[i]["IGSTAX"]).Trim();

                    dr["UGSTID"] = Convert.ToString(ds.Rows[i]["UGSTID"]).Trim();
                    dr["UGSTPERCENTAGE"] = Convert.ToString(ds.Rows[i]["UGSTPERCENTAGE"]).Trim();
                    dr["UGSTAX"] = Convert.ToString(ds.Rows[i]["UGSTAX"]).Trim();

                    dr["NETAMOUNT"] = Convert.ToDecimal(ds.Rows[i]["NETAMOUNT"]).ToString().Trim();
                    dr["TDSID"] = Convert.ToString(ds.Rows[i]["TDSID"]).Trim();

                    dr["BILLDATE"] = Convert.ToString(ds.Rows[i]["BILLDATE"]).Trim();
                    dr["TDSDEDUCTABLEAMOUNT"] = Convert.ToString(ds.Rows[i]["TDSDEDUCTABLEAMOUNT"]).Trim();

                    dr["BILLINGTOSTATEID"] = Convert.ToString(ds.Rows[i]["BILLINGTOSTATEID"]).Trim();
                    dr["BILLINGTOSTATENAME"] = Convert.ToString(ds.Rows[i]["BILLINGTOSTATENAME"]).Trim();

                    dr["TRANSPORTERID"] = Convert.ToString(ds.Rows[i]["TRANSPORTERID"]).Trim();
                    dr["TRANSPORTERNAME"] = Convert.ToString(ds.Rows[i]["TRANSPORTERNAME"]).Trim();

                    dtTransporterEdit.Rows.Add(dr);
                    dtTransporterEdit.AcceptChanges();
                }
            }

            HttpContext.Current.Session["TRANSPORTERBILL"] = dtTransporterEdit;
            LoadTransporterBill();
            this.gvtransporterAdd.DataSource = dtTransporterEdit;
            this.gvtransporterAdd.DataBind();
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            btnaddhide.Style["display"] = "none";


            if (Convert.ToString(ds.Rows[0]["REVERSECHARGE"]).Trim() == "Y")
            {
                this.chkReversecharge.Checked = true;
            }
            else
            {
                this.chkReversecharge.Checked = false;
            }
            if (Convert.ToString(ds.Rows[0]["TDSAPPLICABLE"]).Trim() == "Y")
            {
                this.chktdsapplicable1.Checked = true;
            }
            else
            {
                this.chktdsapplicable1.Checked = false;
            }

            //if (Convert.ToString(ds.Rows[0]["TDSAPPLICABLE"]).Trim() == "")
            //if (string.IsNullOrEmpty(ds.Rows[0]["TDSAPPLICABLE"]).Trim())
            if (string.IsNullOrEmpty(Convert.ToString(ds.Rows[0]["TDSAPPLICABLE"]).Trim()))
            {
                decimal tdspercentage = transporterbill.Edittdsid(TransporterID);
                this.txtTDSpercentage1.Text = Convert.ToString(tdspercentage) + " % ";
                if (this.txtTDSpercentage1.Text.Replace("%", "").Trim() == "0.00")
                {
                    this.chktdsapplicable1.Checked = false;
                }
                else
                {
                    this.chktdsapplicable1.Checked = true;
                }
            }
            else
            {
                this.txtTDSpercentage1.Text = Convert.ToString(ds.Rows[0]["TDSPECENTAGE"]).Trim() + " % ";
            }

            this.txtBillvalue.Attributes.Add("onkeyup", "calculation()");
            this.TxtTds.Attributes.Add("onkeyup", "calculation()"); 
            //GetTdscheck();
            Rcmcheck();

            string status = Convert.ToString(hdnStatus.Value).Trim();
            if(status != "PENDING")
            {
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtTDSpercentage1.Text = "0.00 %";
            //this.txtbillDate.Text = "";
            this.Txtbillno.Text = "";
            this.txtBillvalue.Text = "";
            this.hdntrnsID.Value = "";
            this.txtGNGNO.Text = "";
            this.TxtTds.Enabled = true;
            this.lbltds.Visible = false;
            this.tdsspan.Visible = true;
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            this.rdbtype.Enabled = true;
            this.ddltransporter.Enabled = true;
            this.ddlexportdepot.Enabled = true;
            ddlfromstate.Enabled = true;
            this.chktdsapplicable.Enabled = true;
            this.hdntdspercentage.Value = "0";
            this.ddldepot.SelectedValue = "0";
            this.Txtbillno.Enabled = true;
            //LoadBranchMaster();
            LoadTransporterBill();
            this.ddldepot.Enabled = true;
            string Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE")
            {
                this.btnaddhide.Style["display"] = "none";
                this.gvtransporterbill.Columns[18].HeaderText = "VIEW";
            }
            else
            {
                this.btnaddhide.Style["display"] = "";
                this.gvtransporterbill.Columns[18].HeaderText = "EDIT";
            }
            this.ddlinvoice.SelectedValue = "0";
            if (rdbtype.SelectedValue == "Others")
            {


                tdddlinvoice.Style["display"] = "none";
                this.ddlinvoice.SelectedValue = "0";
            }
            else
            {

                tdddlinvoice.Style["display"] = "";
            }
            this.btnsubmitdiv.Visible = true;
            this.gvtransporterAdd.Columns[26].Visible = true;
            this.chktdsapplicable1.Enabled = true;
            this.chkReversecharge.Enabled = true;
            this.chktdsapplicable1.Checked = false;
            this.txtTDSpercentage1.Text = "0.00 %";
            this.chkReversecharge.Checked = false;
            this.ddlinvoice.SelectedValue = "0";
            this.ddlexportdepot.SelectedValue = "0";
           
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
       
    }
    #endregion

    #region Transporter Bill Delete
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsTransporterBill transporterbill = new ClsTransporterBill();

            if (Request.QueryString["CHECKER"].ToString().Trim() == "TRUE")
            {
                e.Record["Error"] = "Not Allow To Delete.";
                return;
            }

            else
            {

                if (transporterbill.Getstatusforedit(e.Record["TRANSPORTERBILLID"].ToString()) == "1")
                {

                    e.Record["Error"] = "Transporter Bill Approved,Not Allow To Delete.";
                    return;
                }
                else
                {
                    if (e.Record["TRANSPORTERBILLID"] != "")
                    {
                        int ID = 0;
                        ID = transporterbill.DeleteTransporterbill(e.Record["TRANSPORTERBILLID"].ToString());
                        if (ID > 0)
                        {
                            e.Record["Error"] = "Record Deleted Successfully. ";
                            LoadTransporterBill();
                        }
                        else
                        {
                            e.Record["Error"] = "Error On Deleting. ";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnApprove_Click
    // Added By Ajoy Rana On 28-06-2016
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPurchaseStockReceipt clsPurchaseStockReceipt = new ClsPurchaseStockReceipt();
            int flag = 0;
            string receivedID = Convert.ToString(hdntrnsID.Value).Trim();
            string istransfertoho = "N";
            if (this.chktransferHO.Checked == true)
            {
                istransfertoho = "Y";
            }
            else
            {
                istransfertoho = "N";
            }

            string Reversecharge = string.Empty;
            if (this.chkReversecharge.Checked == true)
            {
                Reversecharge = "Y";
            }
            else
            {
                Reversecharge = "N";
            }

            flag = clsPurchaseStockReceipt.ApproveTransporterBill(receivedID, Session["FINYEAR"].ToString(), Session["USERID"].ToString(), this.Txtbillno.Text.Trim(), this.txtbillDate.Text.Trim(), istransfertoho, Reversecharge);

            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.LoadTransporterBill();
                this.hdntrnsID.Value = "";
                MessageBox1.ShowSuccess("<b>Transporter Bill: <font color='green'>" + this.Txtbillno.Text + "</font> approved and accounts entry(s) passed successfully.</b>", 50, 700);
            }
            else if (flag == 0)
            {
                this.pnlDisplay.Style["display"] = "none";
                this.pnlAdd.Style["display"] = "";
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
    // Added By Ajoy Rana On 28-06-2016
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {           
            this.pnlAdd.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion        

    #region gvtransporterbill_RowDataBound
    protected void gvtransporterbill_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[17] as GridDataControlFieldCell;
                GridDataControlFieldCell cell5 = e.Row.Cells[8] as GridDataControlFieldCell;
                GridDataControlFieldCell cell17 = e.Row.Cells[18] as GridDataControlFieldCell;

                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;

                string status = cell.Text.Trim().ToUpper();
                string billtype = cell5.Text.Trim().ToUpper();
                string dayend = cell17.Text.Trim().ToUpper();

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

                if (billtype == "STOCK TRANSFER")
                {
                    cell5.ForeColor = Color.CadetBlue;
                }
                else if (billtype == "SALE INVOICE")
                {
                    cell5.ForeColor = Color.Green;
                }
                else if (billtype == "TPU DESPATCH")
                {
                    cell5.ForeColor = Color.SeaGreen;
                }
                else
                {
                    cell5.ForeColor = Color.Green;
                }

                if (dayend == "PENDING")
                {
                    cell17.ForeColor = Color.Blue;
                }
                else
                {
                    cell17.ForeColor = Color.Green;
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

    public void LoadTaxes()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();

        if (this.ddltransporter.SelectedValue == "0")
        {
            MessageBox1.ShowInfo("Please Select Transporter");
            return;
        }

        if (txtBillvalue.Text == "")
        {
            MessageBox1.ShowInfo("Please enter bill value");
            return;
        }

        if (txtBillvalue.Text == "0")
        {
            MessageBox1.ShowInfo("Please enter bill value");
            return;
        }

        if (this.rdbtype.SelectedValue == "Others")
        {
            if (this.ddlfromstate.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select from state");
                return;
            }
            if (this.ddltostate.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select to state");
                return;
            }
        }

        //# region TDS calculate

        //decimal tdspercent = 0;
        //if (chktdsapplicable.Checked == true)
        //{
        //    tdspercent = transporterbill.TDSPercentage(this.ddltransporter.SelectedValue, Convert.ToDecimal(txttdsdeductablevalue.Text));
        //}
        //else
        //{
        //    tdspercent = 0;
        //}
        //if (tdspercent == 0)
        //{
        //    hdntdspercentage.Value = tdspercent.ToString();

        //    this.TxtTds.Text = "0";
        //    this.txttdspercentage.Text = "0";
        //    this.lbltds.Visible = true;
        //    this.chktdsapplicable.Checked = false;
        //    this.chktdsapplicable.Enabled = false;
        //    this.tdsspan.Visible = false;
        //}
        //else
        //{
        //    decimal tdsvalue = 0;
        //    hdntdspercentage.Value = tdspercent.ToString();
        //    this.txttdspercentage.Text = hdntdspercentage.Value;

        //    if (chktdsapplicable.Checked == true)
        //    {
        //        tdsvalue = (Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txttdspercentage.Text)) / 100;
        //    }
        //    else
        //    {
        //        hdn_tdsis.Value = "0";
        //        txttdspercentage.Text = "0";
        //    }

        //    //decimal tdsroundoffvalue = transporterbill.RoundOff(tdsvalue);
        //    this.TxtTds.Text = Convert.ToString(tdsvalue);

        //    this.lbltds.Visible = false;
        //    this.tdsspan.Visible = true;
        //}
        //#endregion

        #region IGST/UGST/SGST

        string depot = string.Empty;
        if (transporterbill.ExportDepotChecking(this.ddldepot.SelectedValue.Trim()) == "1")
        {
            this.tdlblexportdepot.Style["display"] = "";
            this.tdddlexportdepot.Style["display"] = "";
            depot = this.ddlexportdepot.SelectedValue.Trim();

          
        }
        else
        {
            this.tdlblexportdepot.Style["display"] = "none";
            this.tdddlexportdepot.Style["display"] = "none";
            depot = this.ddldepot.SelectedValue.Trim();
        }

        DataTable dt = transporterbill.BindGstPercentage(depot.Trim(), this.ddltransporter.SelectedValue.Trim(), Convert.ToDecimal(this.txttdsdeductablevalue.Text), Convert.ToString(this.ddlinvoice.SelectedValue), Convert.ToString(this.rdbtype.SelectedValue).Trim(), Convert.ToInt32(this.ddlfromstate.SelectedValue), Convert.ToInt32(this.ddltostate.SelectedValue));

        if (dt.Rows.Count == 1)
        {
            decimal cgstamt = 0;
            decimal sgstamt = 0;
            decimal igst = 0;
            decimal ugst = 0;

            this.txtcgstpercentage.Text = Convert.ToString(dt.Rows[0]["CGST_PERCENTAGE"]).Trim();

            if (chkcstapplicable.Checked == true)
            {
                cgstamt = transporterbill.RoundOff(Convert.ToDecimal((Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txtcgstpercentage.Text)) / 100));
                hdncgsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["CGSTID"]).Trim();
            }
            else
            {
                this.txtcgstpercentage.Text = "0";
                hdncgsttaxpercentage.Value = "0";
            }
            this.txtcgstvalue.Text = Convert.ToString(cgstamt).Trim();

            this.txtsgstpercentage.Text = Convert.ToString(dt.Rows[0]["SGST_PERCENTAGE"]).Trim();
            if (chkcstapplicable.Checked == true)
            {
                sgstamt = transporterbill.RoundOff(Convert.ToDecimal((Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txtsgstpercentage.Text)) / 100));
                hdnsgsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["SGSTID"]).Trim();
            }
            else
            {
                this.txtsgstpercentage.Text = "0";
                hdnsgsttaxpercentage.Value = "0";
            }
            this.txtsgstvalue.Text = Convert.ToString(sgstamt).Trim();


            this.txtigstpercentage.Text = Convert.ToString(dt.Rows[0]["IGST_PERCENTAGE"]).Trim();
            if (chkcstapplicable.Checked == true)
            {
                igst = transporterbill.RoundOff(Convert.ToDecimal((Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txtigstpercentage.Text)) / 100));
                hdnigsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["IGSTID"]).Trim();
            }
            else
            {
                this.txtigstpercentage.Text = "0";
                hdnigsttaxpercentage.Value = "0";
            }
            this.txtigstvalue.Text = Convert.ToString(igst).Trim();



            this.txtugstpercentage.Text = Convert.ToString(dt.Rows[0]["UGST_PERCENTAGE"]).Trim();
            if (chkcstapplicable.Checked == true)
            {
                ugst = transporterbill.RoundOff(Convert.ToDecimal((Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txtugstpercentage.Text)) / 100));
                hdnugsttaxpercentage.Value = Convert.ToString(dt.Rows[0]["UGSTID"]).Trim();
            }
            else
            {
                this.txtugstpercentage.Text = "0";
            }
            this.txtugstvalue.Text = Convert.ToString(ugst).Trim();

        }
        else
        {
            this.txtcgstpercentage.Text = "0";
            this.txtsgstpercentage.Text = "0";
            this.txtigstpercentage.Text = "0";
            this.txtcgstvalue.Text = "0";
            this.txtsgstvalue.Text = "0";
            this.txtigstvalue.Text = "0";
        }
        #endregion

        Txtamount.Text = (Convert.ToDecimal(txtBillvalue.Text) + Convert.ToDecimal(txtcgstvalue.Text) + Convert.ToDecimal(txtsgstvalue.Text) + Convert.ToDecimal(txtigstvalue.Text) + Convert.ToDecimal(txtugstvalue.Text)).ToString();
        //Txtamount.Text = (Convert.ToDecimal(txtBillvalue.Text) - Convert.ToDecimal(tdsvalue.Text) + Convert.ToDecimal(txtcgstvalue.Text) + Convert.ToDecimal(txtsgstvalue.Text) + Convert.ToDecimal(txtigstvalue.Text) + Convert.ToDecimal(txtugstvalue.Text)).ToString();
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtgrossweight.ClientID + "').focus(); ", true);
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.btnADDGrid.ClientID + "').focus(); ", true);
    }

    public void TDScalculate()
    {
        # region TDS calculate

        ClsTransporterBill transporterbill = new ClsTransporterBill();

        decimal tdspercent = 0;
        if (chktdsapplicable1.Checked == true)
        {
            tdspercent = transporterbill.TDSPercentage(this.ddltransporter.SelectedValue, Convert.ToDecimal(txtsumtdsdeuctablevalue.Text.Trim()));
            this.txtTDSpercentage1.Text = Convert.ToString(tdspercent) + " %";
            reason.Style["Display"] = "none";
        }
        else
        {
            //reason.Style["Display"] = "none";
            tdspercent = 0;

        }
        if (tdspercent == 0)
        {
            hdntdspercentage.Value = tdspercent.ToString();
            //reason.Style["Display"] = "none";
            this.TxtTds.Text = "0";
            this.txttdspercentage.Text = "0";
            this.lbltds.Visible = true;
            this.chktdsapplicable1.Checked = false;
            //this.chktdsapplicable1.Enabled = false;
            this.tdsspan.Visible = false;
            this.txtTDSpercentage1.Text = "0.00 %";
            this.txtsumtds.Text = "0.00";
            this.txtsumnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtsumbillvalue.Text.Trim()) + Convert.ToDecimal(this.txtsumcgst.Text.Trim()) + Convert.ToDecimal(this.txtsumsgst.Text.Trim()) + Convert.ToDecimal(this.txtsumigst.Text.Trim()) + Convert.ToDecimal(this.txtsumugst.Text.Trim())));
        }
        else
        {
            decimal tdsvalue = 0;
            hdntdspercentage.Value = tdspercent.ToString();
            this.txttdspercentage.Text = hdntdspercentage.Value;

            if (chktdsapplicable1.Checked == true)
            {
                tdsvalue = (Convert.ToDecimal(txtsumtdsdeuctablevalue.Text.Trim()) * Convert.ToDecimal(txttdspercentage.Text.Trim())) / 100;
                tdsvalue = (Math.Round((tdsvalue),MidpointRounding.AwayFromZero));
               // TaxValue = Convert.ToDouble(Math.Round((AfterRebateAmount * ProductWiseTax) / 100, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                hdn_tdsis.Value = "0";
                txttdspercentage.Text = "0";
                this.txtsumtds.Text = "0";
            }

            //decimal tdsroundoffvalue = transporterbill.RoundOff(tdsvalue);
            if (this.chktdsapplicable1.Checked == true)
            {
                double sumtds1 = 0;
                sumtds1 = Math.Ceiling(Convert.ToDouble(tdsvalue));
                this.txtsumtds.Text = Convert.ToString(sumtds1);

                this.txtsumnetamt.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtsumnetamt.Text.Trim()) - Convert.ToDecimal(this.txtsumtds.Text.Trim())));
                //reason.Style["Display"] = "";
            }
            this.lbltds.Visible = false;
            this.tdsspan.Visible = true;
        }
        #endregion
    }

    #region txtBillvalue_TextChanged
    protected void txtBillvalue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlfromstate.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select Billing From");
                return;
            }
            else
            {
                LoadTaxes();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region txttdsdeductablevalue_TextChanged
    protected void txttdsdeductablevalue_TextChanged(object sender, EventArgs e)
    {
        try
        {

            decimal tdspercent = 0;
            ClsTransporterBill transporterbill = new ClsTransporterBill();
            if (ddlfromstate.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select Billing From");
                return;
            }

            if (txtBillvalue.Text == "")
                return;

            if (txtBillvalue.Text == "0")
                return;
            if (this.ddltransporter.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select Transporter...");
                return;
            }
            if (chktdsapplicable.Checked == true)
            {
                tdspercent = transporterbill.TDSPercentage(this.ddltransporter.SelectedValue, Convert.ToDecimal(txttdsdeductablevalue.Text));
            }
            else
            {
                tdspercent = 0;
            }
            if (tdspercent == 0)
            {
                hdntdspercentage.Value = tdspercent.ToString();

                this.TxtTds.Text = "0";
                this.txttdspercentage.Text = "0";
                this.lbltds.Visible = true;
                this.tdsspan.Visible = false;
            }
            else
            {
                hdntdspercentage.Value = tdspercent.ToString();
                this.txttdspercentage.Text = hdntdspercentage.Value;
                decimal tdsvalue = (Convert.ToDecimal(txttdsdeductablevalue.Text) * Convert.ToDecimal(txttdspercentage.Text)) / 100;

                //decimal tdsroundoffvalue = transporterbill.RoundOff(tdsvalue);
                this.TxtTds.Text = Convert.ToString(tdsvalue);

                this.lbltds.Visible = false;
                this.tdsspan.Visible = true;
            }

            //Txtamount.Text = (Convert.ToDecimal(txtBillvalue.Text) - Convert.ToDecimal(TxtTds.Text) + Convert.ToDecimal(Txtservicetax.Text)).ToString();
            Txtamount.Text = (Convert.ToDecimal(txtBillvalue.Text) + Convert.ToDecimal(txtcgstvalue.Text) + Convert.ToDecimal(txtsgstvalue.Text) + Convert.ToDecimal(txtigstvalue.Text) + Convert.ToDecimal(txtugstvalue.Text)).ToString();

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtgrossweight.ClientID + "').focus(); ", true);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.btnADDGrid.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
    
    private void gettaxid()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tdsid = transporterbill.TDSidFROMAPP(this.ddltransporter.SelectedValue, 0);

        if (chktdsapplicable1.Checked == true)
        {
            if (tdsid == "0")
            {
                hdn_tdsis.Value = tdsid;
            }
            else
            {
                hdn_tdsis.Value = tdsid;
            }
        }
        else
        {
            hdn_tdsis.Value = "0";
        }

    }

    public void GetReverseCharge()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tag = string.Empty;
        tag = transporterbill.GetReverseCharge(ddltransporter.SelectedValue.Trim());

        if (tag == "Y")
        {
            this.chkReversecharge.Checked = true;
        }
        else
        {
            this.chkReversecharge.Checked = false;
        }
        
       
    }

    public void GetIsTransferToHo()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tag = string.Empty;
        tag = transporterbill.GetIsTransferToHo(ddltransporter.SelectedValue.Trim());

        if (tag == "Y")
        {
            this.chktransferHO.Checked = true;
        }
        else
        {
            this.chktransferHO.Checked = false;
        }


    }

    public void GetTdscheck()
    {
        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string tag = string.Empty;
        tag = transporterbill.GetTdscheck(ddltransporter.SelectedValue.Trim());

        if (tag == "Y")
        {
            this.chktdsapplicable1.Checked = true;
        }
        else
        {
            this.chktdsapplicable1.Checked = false;
        }
        
       
    }

    #region ddltransporter_OnSelectedIndexChanged
    protected void ddltransporter_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetReverseCharge();
        GetTdscheck();
        GetIsTransferToHo();
        Rcmcheck();
    }
    #endregion
    
    #region chkReversecharge_OnCheckedChanged
    protected void chkReversecharge_OnCheckedChanged(object sender, EventArgs e)
    {
        Rcmcheck();

        ClsTransporterBill transporterbill = new ClsTransporterBill();
        string RCM = transporterbill.GetRCMheck(ddltransporter.SelectedValue.Trim());

        if (chkReversecharge.Checked == true && RCM == "Y")
        {
            td_rcmreason.Style["Display"] = "none";
        }
        if (chkReversecharge.Checked == false && RCM == "Y")
        {
            td_rcmreason.Style["Display"] = "";
            MessageBox1.ShowInfo("Reverse Charge  for the transporter : "+ddltransporter.SelectedItem.Text+" is applicabe in the master form",40,750);
        }
        if (chkReversecharge.Checked == true && RCM == "N")
        {
            td_rcmreason.Style["Display"] = "";
        }
        if (chkReversecharge.Checked == false && RCM == "N")
        {
            td_rcmreason.Style["Display"] = "none";
        }
    }
    #endregion

    public void Rcmcheck()
    {
        
        if (chkReversecharge.Checked == true)
        {
            lblsgst.Text = "SGST AMT(RCM).";
            lblcgst.Text = "CGST AMT(RCM).";
            lbligst.Text = "IGST AMT(RCM).";
            lblugst.Text = "UGST AMT(RCM).";
            this.txtsumnetamtshow.Text = Convert.ToString(Convert.ToDecimal(txtsumbillvalue.Text.Trim()) - Convert.ToDecimal(txtsumtds.Text.Trim()));
        }
        else
        {
            lblsgst.Text = "SGST AMT.";
            lblcgst.Text = "CGST AMT.";
            lbligst.Text = "IGST AMT.";
            lblugst.Text = "UGST AMT.";
            this.txtsumnetamtshow.Text = Convert.ToString(Convert.ToDecimal(txtsumbillvalue.Text.Trim()) - Convert.ToDecimal(txtsumtds.Text.Trim()) + Convert.ToDecimal(txtsumcgst.Text.Trim()) + Convert.ToDecimal(txtsumsgst.Text.Trim()) + Convert.ToDecimal(txtsumigst.Text.Trim()) + Convert.ToDecimal(txtsumugst.Text.Trim()));
        }
    }

    

    #region LoadReason
    private void LoadReason()
    {
        ClsTransporterBill clstranspoter = new ClsTransporterBill();
        DataTable dt = clstranspoter.BindReason(Request.QueryString["MENUID"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            this.ddlreason.Items.Clear();
            this.ddlreason.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlreason.DataSource = dt;
            this.ddlreason.DataTextField = "NAME";
            this.ddlreason.DataValueField = "REASONID";
            this.ddlreason.DataBind();
        }
        else
        {
            this.ddlreason.Items.Clear();
            this.ddlreason.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlreason.Enabled = true;
        }
    }
    #endregion

    #region LoadReasonRCM
    private void LoadReasonRCM()
    {
        ClsTransporterBill clstranspoter = new ClsTransporterBill();
        DataTable dt = clstranspoter.BindReason(Request.QueryString["MENUID"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            this.ddlgstreason.Items.Clear();
            this.ddlgstreason.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlgstreason.DataSource = dt;
            this.ddlgstreason.DataTextField = "NAME";
            this.ddlgstreason.DataValueField = "REASONID";
            this.ddlgstreason.DataBind();
        }
        else
        {
            this.ddlgstreason.Items.Clear();
            this.ddlgstreason.Items.Insert(0, new ListItem("Select", "0"));
            this.ddlgstreason.Enabled = true;
        }
    }
    #endregion

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTransporterBill clstranspoter = new ClsTransporterBill();
            ClsStockReport clsInvc = new ClsStockReport();
            DataSet dt = new DataSet();
            string upath = string.Empty;
            string StnId = string.Empty;
            string tag = Request.QueryString["TAG"];
            string AccEntryID = string.Empty;

            AccEntryID = clstranspoter.GetAccEntryID(hdntrnsID.Value.Trim());
            if (AccEntryID != null)
            {
                upath = "frmRptInvoicePrint.aspx?Stnid=" + hdntrnsID.Value.Trim() + "&&TAG=TB&&MenuId=45";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);

            }
            else
            {
                MessageBox1.ShowInfo("This Bill is yet to be approved...");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    
    protected void BindRegion(string UserType, string UserID)
    {
        try
        {
            //ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
            //DataTable dt = new DataTable();
            //dt = clsVoucher.BindDepot(HttpContext.Current.Session["UserID"].ToString().Trim());//Region(UserType, UserID);
            ClsTransporterBill clstranspoter = new ClsTransporterBill();
            DataTable dt = clstranspoter.BindDepot(HttpContext.Current.Session["UserID"].ToString().Trim());
            this.ddlregion.DataSource = dt;
            this.ddlregion.DataTextField = "BRNAME";
            this.ddlregion.DataValueField = "BRID";
            this.ddlregion.DataBind();

         
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadAdd()
    {

        this.btnSubmit.Style["display"] = "";
        this.btnsubmitdiv.Style["display"] = "";
        this.gvtransporterAdd.Columns[26].Visible = true;
        this.chktdsapplicable1.Enabled = true;
        this.chkReversecharge.Enabled = true;
        this.pnlAdd.Style["display"] = "";
        this.pnlDisplay.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "none";
        this.rdbtype.Enabled = true;
        this.ddltransporter.Enabled = true;
        this.ddlfromstate.Enabled = false;
        this.Txtamount.Text = "";
        //this.txtbillDate.Text = "";
        this.txtCheckerNote.Visible = false;
        this.lblCheckerNote.Visible = false;
        this.Txtbillno.Text = "";
        this.txtBillvalue.Text = "";
        this.txtremarks.Text = "";
        this.TxtTds.Text = "";
        this.lbltds.Visible = false;

        this.tdsspan.Visible = true;
        this.ddltransporter.Items.Clear();
        this.ddltransporter.Items.Insert(0, new ListItem("Select", "0"));
        this.ddltransporter.AppendDataBoundItems = true;
        this.rdbtype.ClearSelection();
        this.ViewState["transporterid"] = null;

        this.gvtransporterAdd.DataSource = null;
        this.gvtransporterAdd.DataBind();
        this.CreateDataTable();
        this.txttdspercentage.Text = "";

        this.hdntdspercentage.Value = "0";
        this.txtsumnetamt.Text = "0.00";
        this.txtsumgrossweight.Text = "0.00";
        this.txtsumtdsdeuctablevalue.Text = "0.00";
        this.txtsumbillvalue.Text = "0.00";
        this.txtsumtds.Text = "0.00";
        this.txtsumcgst.Text = "0.00";
        this.txtsumsgst.Text = "0.00";
        this.txtsumigst.Text = "0.00";
        this.txtsumugst.Text = "0.00";
        this.txtsumnetamtshow.Text = "0.00";
        rdbtype.Enabled = true;

        ddltransporter.Enabled = true;
        ddlfromstate.Enabled = true;
        this.txtGNGNO.Text = "";
        this.txttdspercentage.Text = "";
        this.txtgrossweight.Text = "";
        DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        //this.txtbillDate.Text = dtcurr.ToString(date).Replace('-', '/');
        //this.txtbilldate2.Text = dtcurr.ToString(date).Replace('-', '/');

        this.ddldepot.Enabled = true;
        this.ddldepot.SelectedValue = "0";
        this.LoadDepot();
        this.ddlinvoice.SelectedValue = "0";

        tdddlinvoice.Style["display"] = "";

        this.ddlfromstate.SelectedValue = "0";
        this.ddltostate.SelectedValue = "0";
        this.hdntrnsID.Value = "";
        this.tr_billno.Visible = false;
        this.Txtbillno.Text = "";
        this.chktdsapplicable1.Checked = false;
        this.chkReversecharge.Checked = false;
        this.txtTDSpercentage1.Text = "0.00 %";
        this.ddlexportdepot.Enabled = true;
        tdlblexportdepot.Style["display"] = "none";
        tdddlexportdepot.Style["display"] = "none";
        this.ddlinvoice.SelectedValue = "0";
        this.ddlexportdepot.SelectedValue = "0";
        this.ddlinvoice.Items.Clear();
        this.ddlinvoice.Items.Insert(0, new ListItem("Select", "0"));
        this.td_rcmreason.Style["Display"] = "none";
       
        this.Txtbillno.Enabled = true;
    }

}

           

       
         



 