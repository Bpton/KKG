using Account;
using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
public partial class VIEW_frmMMRatesheet : System.Web.UI.Page
{
    ClsMMRatesheet clsprinciple = new ClsMMRatesheet();
    string menuID = string.Empty;
    string bsID = string.Empty;
    string Checker = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlSunItemType').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlcategory').multiselect({ includeSelectAllOption: true  }); $('#ContentPlaceHolder1_ddlsupplingdepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsegment').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {

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
                this.txtEntryDate.Style.Add("color", "#000 !important");
                InisiliseDate();
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                //ViewState["menuID"] = menuID;
                if (Checker == "TRUE")
                {
                    btnaddhide.Style["display"] = "none";
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    LoadBusiness();
                    LoadCustomer(ddlBusiness.SelectedValue);
                    LoadPrimaryItemType();
                    LoadCurrency();
                    LoadPrincipleRatesheetMaster();
                    LoadSearchParty(ddlBusiness.SelectedValue);
                    this.Column15.Visible = true;
                    this.Column16.Visible = false;
                }
                else
                {
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    LoadBusiness();
                    LoadCustomer(ddlBusiness.SelectedValue);
                    LoadPrimaryItemType();
                    LoadCurrency();
                    LoadPrincipleRatesheetMaster();
                    LoadSearchParty(ddlBusiness.SelectedValue);
                    this.Column15.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
        


        //       ******  END LOCK CAELENDER   ***********      SOUMITRA MONDAL    //
        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
           
            this.txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarExtenderVoucherDate.EndDate = today1;
        }
        else
        {
            this.txtEntryDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtenderVoucherDate.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }


    public void LoadPrincipleRatesheetMaster()
    {
        try
        {
            gvPrincipleRatesheet.DataSource = clsprinciple.RateSheetGridBusinesssegmentWise(ddlSearchParty.SelectedValue.Trim());
            gvPrincipleRatesheet.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region LoadBusiness
    public void LoadBusiness()
    {
        try
        {
            DataTable dt = clsprinciple.BindBusinessSegment();
            if (dt.Rows.Count > 0)
            {
                ddlBusiness.Items.Clear();
                ddlBusiness.Items.Insert(0, new ListItem("Select Business Segment", "0"));
                ddlBusiness.DataSource = dt;
                ddlBusiness.DataTextField = "BSNAME";
                ddlBusiness.DataValueField = "BSID";
                ddlBusiness.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlBusiness.SelectedValue = Convert.ToString(dt.Rows[0]["BSID"]).Trim();
                }
            }
            else
            {
                ddlBusiness.Items.Clear();
                ddlBusiness.Items.Insert(0, new ListItem("Select Business Segment", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region Load Customer
    public void LoadCustomer(string BSID)
    {
        try
        {
            DataTable dt = clsprinciple.BindCustomer(BSID);
            if (dt.Rows.Count > 0)
            {
                ddlparty.Items.Clear();
                ddlparty.Items.Insert(0, new ListItem("Select Party", "0"));
                ddlparty.DataSource = dt;
                ddlparty.DataTextField = "CUSTOMERNAME";
                ddlparty.DataValueField = "CUSTOMERID";
                ddlparty.DataBind();
            }
            else
            {
                ddlparty.Items.Clear();
                ddlparty.Items.Insert(0, new ListItem("Select Party", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadCurrency
    public void LoadCurrency()
    {
        try
        {
            DataTable dt = clsprinciple.BindCurrency();
            if (dt.Rows.Count > 0)
            {
                this.ddlcurrency.Items.Clear();
                this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
                this.ddlcurrency.DataSource = dt;
                this.ddlcurrency.DataTextField = "CURRENCYNAME";
                this.ddlcurrency.DataValueField = "CURRENCYID";
                this.ddlcurrency.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlcurrency.SelectedValue = Convert.ToString(dt.Rows[0]["CURRENCYID"]).Trim();
                }
            }
            else
            {
                this.ddlcurrency.Items.Clear();
                this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadPrimary Item Type
    public void LoadPrimaryItemType()
    {
        try
        {
            DataTable dtPrimary = new DataTable();
            dtPrimary = clsprinciple.BindPrimaryitemtype();
            if (dtPrimary.Rows.Count > 0)
            {
                this.ddlPrimaryItemType.Items.Clear();
                this.ddlPrimaryItemType.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlPrimaryItemType.AppendDataBoundItems = true;
                this.ddlPrimaryItemType.DataSource = dtPrimary;
                this.ddlPrimaryItemType.DataTextField = "DIVNAME";
                this.ddlPrimaryItemType.DataValueField = "DIVID";
                this.ddlPrimaryItemType.DataBind();
            }
            else
            {
                this.ddlPrimaryItemType.Items.Clear();
                this.ddlPrimaryItemType.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
                this.ddlPrimaryItemType.AppendDataBoundItems = true;
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadSubItem
    public void LoadSubItem()
    {
        try
        {
            DataTable dt = new DataTable();
            //String[] PrimaryID = this.ddlPrimaryItemType.SelectedValue.Trim().Split('~');
            //string ID = PrimaryID[1].Trim();
            dt = clsprinciple.BindSubitemtype(ddlPrimaryItemType.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSunItemType.Items.Clear();
                ddlSunItemType.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlSunItemType.AppendDataBoundItems = true;
                ddlSunItemType.DataSource = dt;
                ddlSunItemType.DataTextField = "CATNAME";
                ddlSunItemType.DataValueField = "CATID";
                ddlSunItemType.DataBind();
            }
            else
            {
                ddlSunItemType.Items.Clear();
                ddlSunItemType.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlSunItemType.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlPrimaryItemType_SelectedIndexChanged
    protected void ddlPrimaryItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPrimaryItemType.SelectedValue != "0")
            {
                LoadSubItem();
            }
            else
            {
                ddlSunItemType.Items.Clear();
                ddlSunItemType.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
                this.ddlSunItemType.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PoPulateGrid
    private void PoPulateGrid(string DIVISIONID, string CATAGORYID, string BusinessID, string GROUPID, string CurrencyID)
    {
        try
        {
            string CATAGORY = "";
            string CATAGORYNAME = "";
            var query = from ListItem item in ddlSunItemType.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                CATAGORY += item.Value + ',';
                CATAGORYNAME += item.Text + ',';
            }
            CATAGORY = CATAGORY.Substring(0, CATAGORY.Length - 1);
            CATAGORYNAME = CATAGORYNAME.Substring(0, CATAGORYNAME.Length - 1);



            DataTable dtratesheet = new DataTable();
            dtratesheet = clsprinciple.FetchProductINFO(DIVISIONID, CATAGORY, BusinessID, GROUPID, CurrencyID);
            gvPrincipleRSMap.DataSource = dtratesheet;
            gvPrincipleRSMap.DataBind();
            ddlBusiness.Enabled = true;
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region btnsearch_Click
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            //String[] PrimaryID = this.ddlPrimaryItemType.SelectedValue.Trim().Split('~');
            //string ID = PrimaryID[1].Trim();
            PoPulateGrid(ddlPrimaryItemType.SelectedValue.Trim(), ddlSunItemType.SelectedValue, ddlBusiness.SelectedValue, ddlparty.SelectedValue, this.ddlcurrency.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region SAVE
    protected void btnprincipleSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ID = 0;
            string Mode = "A";
            string ENTRYDATE = string.Empty;
            ENTRYDATE = this.txtEntryDate.Text;
            //String[] PrimaryID = this.ddlPrimaryItemType.SelectedValue.Trim().Split('~');
            //string PID = PrimaryID[1].Trim();
            //string PNAME = PrimaryID[0].Trim();

            for (int i = 0; i < gvPrincipleRSMap.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = gvPrincipleRSMap.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;
                GridDataControlFieldCell cellpcs = gvPrincipleRSMap.RowsInViewState[i].Cells[3] as GridDataControlFieldCell;
                GridDataControlFieldCell celluomid = gvPrincipleRSMap.RowsInViewState[i].Cells[4] as GridDataControlFieldCell;
                GridDataControlFieldCell celluomname = gvPrincipleRSMap.RowsInViewState[i].Cells[5] as GridDataControlFieldCell;
                HiddenField hiddenField = cell.FindControl("hdnPS") as HiddenField;
                HiddenField hiddenFieldpid = cell.FindControl("hdnpid") as HiddenField;
                TextBox txtmrp = cell.FindControl("txtmrpcost") as TextBox;
                TextBox txtpcs = cellpcs.FindControl("txtpcs") as TextBox;
                HiddenField hdnuomid = cell.FindControl("hdnuomid") as HiddenField;
                HiddenField hdnuomname = cell.FindControl("hdnuomname") as HiddenField;
                string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

                if (txtmrp.Text.Trim() != "")
                {
                    if ((Convert.ToDecimal(txtmrp.Text.Trim()) != 0))
                    {
                       // clsprinciple.DeleteRateMaster(ddlPrimaryItemType.SelectedValue.Trim(), ddlSunItemType.SelectedValue, ddlBusiness.SelectedValue, ddlparty.SelectedValue, hiddenFieldpid.Value.ToString());
                        ID = clsprinciple.SavePrincipleRateSheetMap(Hdn_Fld.Value, ddlBusiness.SelectedValue.ToString(), ddlBusiness.SelectedItem.ToString(), ddlPrimaryItemType.SelectedValue.Trim().ToString(),
                                                                   ddlPrimaryItemType.SelectedItem.ToString(), ddlSunItemType.SelectedValue.ToString(), ddlSunItemType.SelectedItem.ToString(), hiddenField.Value,
                                                                   txtmrp.ToolTip, Convert.ToDecimal(txtmrp.Text.ToString()), Mode, Convert.ToDecimal(txtpcs.Text.ToString()), this.ddlcurrency.SelectedValue.Trim(),
                                                                   this.ddlcurrency.SelectedItem.ToString().Trim(), ddlparty.SelectedValue.ToString(), ddlparty.SelectedItem.ToString(), hdnuomid.Value.Trim(),
                                                                   hdnuomname.Value.Trim(), ENTRYDATE, finyear);
                        txtmrp.Text = "0";
                    }
                }
            }
            if (ID == 1)
            {
                MessageBox1.ShowSuccess("<b><font color='green'>Record Saved Successfully!</font></b>");
                LoadPrincipleRatesheetMaster();
                Hdn_Fld.Value = "";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                gvPrincipleRSMap.DataSource = null;
                gvPrincipleRSMap.DataBind();
                ddlBusiness.Enabled = true;
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please Insert RATE!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region DELETE
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = clsprinciple.PrincipleRatesheetDelete(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
                LoadPrincipleRatesheetMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ADD NEW RECORED
    protected void btnAddprincipleSheet_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            btnsearch.Visible = true;
            LoadPrincipleRatesheetMaster();
            gvPrincipleRSMap.DataSource = null;
            gvPrincipleRSMap.DataBind();
            this.ddlcurrency.Items.Clear();
            this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
            this.ddlBusiness.Items.Clear();
            this.ddlBusiness.Items.Insert(0, new ListItem("Select BusinessSegment", "0"));
            this.ddlPrimaryItemType.Items.Clear();
            this.ddlPrimaryItemType.Items.Insert(0, new ListItem("--Select Primary Item--", "0"));
            ddlSunItemType.Items.Clear();
            ddlSunItemType.Items.Insert(0, new ListItem("--Select Sub Item--", "0"));
            ddlparty.Items.Clear();
            ddlparty.Items.Insert(0, new ListItem("Select Party", "0"));

            LoadCurrency();
            LoadBusiness();
            LoadPrimaryItemType();
            this.LoadCustomer(ddlBusiness.SelectedValue);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CANCEL
    protected void btnprincipleCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            ddlBusiness.Enabled = true;
            btnaddhide.Style["display"] = "";
            this.ddlcurrency.Items.Clear();
            this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    public void LoadSearchParty(string BSID)
    {
        try
        {
            DataTable dt = clsprinciple.BindBusinessSegment();
            if (dt.Rows.Count > 0)
            {
                ddlSearchParty.Items.Clear();
                ddlSearchParty.Items.Insert(0, new ListItem("Select Business Segment", "0"));
                ddlSearchParty.DataSource = dt;
                ddlSearchParty.DataTextField = "BSNAME";
                ddlSearchParty.DataValueField = "BSID";
                ddlSearchParty.DataBind();

                if (dt.Rows.Count == 1)
                {
                    this.ddlSearchParty.SelectedValue = Convert.ToString(dt.Rows[0]["BSID"]).Trim();
                }
            }
            else
            {
                ddlSearchParty.Items.Clear();
                ddlSearchParty.Items.Insert(0, new ListItem("Select Business Segment", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            //string pid = hdn_pid.Value.ToString();
            //string bsid = hdn_bsid.Value.ToString();
            //DataTable dtratesheet = new DataTable();
            //string principalID = ddlSearchPrinciple.SelectedValue;
            //dtratesheet = clsprinciple.FetchProductDetails(pid,principalID,bsid);
            //ddlprinciple.SelectedValue = principalID;

            //ddlBusiness.SelectedValue = dtratesheet.Rows[0]["BSID"].ToString();            
            //ddlDivision.SelectedValue = dtratesheet.Rows[0]["DIVISIONID"].ToString();
            //ddlCategory.SelectedValue = dtratesheet.Rows[0]["CATAGORYID"].ToString();
            //LoadPrinciple(dtratesheet.Rows[0]["BSID"].ToString());
            //this.LoadCurrency(this.ddlprinciple.SelectedValue.Trim());
            //gvPrincipleRSMap.DataSource = dtratesheet;
            //gvPrincipleRSMap.DataBind();
            //LoadPrincipleRatesheetMaster();
            //ddlprinciple.Enabled = false;
            //ddlBusiness.Enabled = false;
            //ddlDivision.Enabled = false;
            //ddlCategory.Enabled = false;
            //pnlAdd.Style["display"] = "";
            //pnlDisplay.Style["display"] = "none";
            //btnaddhide.Style["display"] = "none";
            //btnsearch.Visible = false;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlSearchParty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadPrincipleRatesheetMaster();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gvProduct_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
                string STATUS = cell.Text;

                if (STATUS == "APPROVE")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #region btngrdApprove_Click
    protected void btngrdApprove_Click(object sender, EventArgs e)
    {
        ClsMMRatesheet clsrateapprove = new ClsMMRatesheet();
        DataTable dt = new DataTable();
        string pid = hdn_pid.Value.ToString();
        string bsid = hdn_bsid.Value.ToString();
        string groupid = hdn_group.Value.ToString();
        string currency = hdn_currency.Value.ToString();
        dt = clsrateapprove.TradingSaleRateApprove(pid, bsid);

        if (dt.Rows.Count > 0)
        {
            MessageBox1.ShowSuccess("<b><font color='green'>Approve Successfully!</font></b>");
            LoadPrincipleRatesheetMaster();
        }
        else
        {
            // MessageBox1.ShowInfo("<b><font color='red'>Allready Confirmed!</font></b>");
            MessageBox1.ShowSuccess("<b><font color='green'>Approve Successfully!</font></b>");
            LoadPrincipleRatesheetMaster();
        }


    }
    #endregion
}