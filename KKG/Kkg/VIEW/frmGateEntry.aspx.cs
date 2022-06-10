using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_Default : System.Web.UI.Page
{
    ClsVendor_TPU clsGetEntry = new ClsVendor_TPU();
    DateTime today1 = DateTime.Now;
    string date = "dd/MM/yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlPono').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.DateLock();
                date = Convert.ToString(today1);
                this.loadVendor();
                this.loadGetEntry();
                this.txtBilldate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtEntryDate.Text = DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/');
                this.txtBilldate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');


            }
        }
        catch(Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }
    #region loadMethods
    public void loadVendor()
    {
        try
        { 
             
              DataTable dt = new DataTable();
              dt = clsGetEntry.BindAllTpuForGateEntry();
              if (dt.Rows.Count > 0)
              {
                  this.ddlVendor.Items.Clear();
                  this.ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
                  this.ddlVendor.AppendDataBoundItems = true;
                  this.ddlVendor.DataSource = dt;
                  this.ddlVendor.DataTextField = "VENDORNAME";
                  this.ddlVendor.DataValueField = "VENDORID";
                  this.ddlVendor.DataBind();
              }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadGetEntry()
    {
        string mode = "Show";
        DataTable dataTable = new DataTable();
        dataTable = clsGetEntry.BindGateEntry(mode, HttpContext.Current.Session["UserID"].ToString());
        if (dataTable.Rows.Count > 0)
        {
            grvGateEntry.DataSource = dataTable;
            grvGateEntry.DataBind();
        }
        else
        {
            grvGateEntry.DataSource = null;
            grvGateEntry.DataBind();
        }
    }

    public void Clearall()
    {
        try
        {
            this.Hdn_Fld.Value = "";
            this.ddlVendor.SelectedValue = "0";
            this.ddlPono.Items.Clear();
            this.txtBillNumber.Text = "";
            this.txtEntryDate.Text = "";
            this.txtBilldate.Text = "";
            this.txtTotalvalue.Text = "0";
            this.txtRemarks.Text = "";
            this.txtEnrtyNumber.Text = "";
            this.save.Style["display"] = "";
            this.ddlEntryType.SelectedValue = "1";
            this.tdLblPo.Visible = true;
            this.tdDdlPo.Visible = true;
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.Clearall();
            this.DateLock();
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            this.trBtn.Visible = false;
            this.entryNumber.Visible = false;
            this.txtBilldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = string.Empty;
            string getEntryId = string.Empty;
            string vendorId = string.Empty;
            string vendorName = string.Empty;
            string billNo = string.Empty;
            string poId = string.Empty;
            string poNo = string.Empty;
            string billlDate = string.Empty;
            string reamrks = string.Empty;
            string ISEXISTS = string.Empty;
            string BILLMODE = string.Empty;
            decimal totalValue=0 ;
            vendorId = this.ddlVendor.SelectedValue.ToString();
            vendorName = this.ddlVendor.SelectedItem.ToString();
            if(vendorId=="0")
            {
                MessageBox1.ShowError("Please Select Vendor");
                return;
            }
            if(ddlEntryType.SelectedValue == "1")
            {
                var query = from ListItem item in ddlPono.Items where item.Selected select item;
                foreach (ListItem item in query)
                {
                    poId += item.Value + ',';
                    poNo += item.Text + ',';
                }
                poId = poId.Substring(0, poId.Length - 1);
                poNo = poNo.Substring(0, poNo.Length - 1);


                if (poId == "0")
                {
                    MessageBox1.ShowError("Please Select Pono");
                    return;
                }
            }
            else
            {
                poId = "0";
                poNo = "0";
            }
           

            billNo =  this.txtBillNumber.Text;

            if (billNo == "")
            {
                MessageBox1.ShowError("Please Enter Billno");
                return;
            }

            billlDate = this.txtBilldate.Text;
            reamrks = this.txtRemarks.Text;
            totalValue =Convert.ToDecimal(this.txtTotalvalue.Text);

            if (totalValue == 0)
            {
                MessageBox1.ShowError("Value cannot be zero");
                return;
            }

            BILLMODE = "BILLEXISTS";
            ISEXISTS = clsGetEntry.BillStatusCheck(BILLMODE, vendorId, billNo);
            getEntryId = this.Hdn_Fld.Value;
            if(getEntryId=="")
            {
                mode = "I";
                if (ISEXISTS == "Y")
                {
                    MessageBox1.ShowWarning("Bill No Is Already Exists With This Vendor :" + vendorName);
                    return;
                }
            }
            else
            {
                mode = "U";
            }
            DataTable dtSave = new DataTable();
            dtSave = clsGetEntry.SaveGetEntry(mode, getEntryId, vendorId, vendorName, poId, poNo, billNo, billlDate, totalValue, reamrks,this.txtEntryDate.Text);
            if(dtSave.Rows[0]["MESSAGE"].ToString()=="New")
            {
                MessageBox1.ShowSuccess("Entry Sucesfull");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.loadGetEntry();
                this.trBtn.Visible = true;
                return;
            }
            else if(dtSave.Rows[0]["MESSAGE"].ToString() == "Update")
            {
                MessageBox1.ShowSuccess("Update Sucesfull");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.loadGetEntry();
                this.trBtn.Visible = true;
                return;
            }
            else
            {
                MessageBox1.ShowError("Error On Saving");
                return;
            }

        }

        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.Clearall();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            this.trBtn.Visible = true;
            //this.loadGetEntry();
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void grvGateEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSTATUS = (Label)e.Row.FindControl("lblSTATUS");
                string LBLSTATUS = lblSTATUS.Text.Trim().ToUpper();

                if (lblSTATUS.Text == "Y")
                {
                    lblSTATUS.Text = "GRN DONE";
                    lblSTATUS.ForeColor = Color.Green;
                }
                else if (lblSTATUS.Text == "N")
                {
                    lblSTATUS.Text = "GRN PENDING";
                    lblSTATUS.ForeColor = Color.Blue;

                }
                else if (lblSTATUS.Text == "C")
                {
                    lblSTATUS.Text = "CANCEL";
                    lblSTATUS.ForeColor = Color.Red;

                }

            }
          

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }

    protected void btnEntryedit_Click(object sender, EventArgs e)
    {
        try
        {
             this.entryNumber.Visible = true;
             string mode = "status";
             Button btn_views = (Button)sender;
             GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
             Label lblENTRYID = (Label)gvr.FindControl("lblENTRYID");
             this.Hdn_Fld.Value = lblENTRYID.Text.Trim();
             string ID = "";
             string Pomode = "";
             Pomode = "EditPo";
             ID = clsGetEntry.editGateEntryCheck(mode,this.Hdn_Fld.Value);
             
             DataTable dtEdit = new DataTable();
             dtEdit = clsGetEntry.editGateEntry(this.Hdn_Fld.Value);
             if (dtEdit.Rows.Count > 0)
             {
                 this.txtEnrtyNumber.Text = dtEdit.Rows[0]["ENTNUMBER"].ToString();
                 this.ddlVendor.SelectedValue = dtEdit.Rows[0]["VENDORID"].ToString();
                 this.loadPoNo(Pomode,this.ddlVendor.SelectedValue);

                var myList = new List<string>(dtEdit.Rows[0]["POID"].ToString().Split(','));
                if (dtEdit.Rows.Count > 0)
                {
                    if(dtEdit.Rows[0]["POID"].ToString()=="0")
                    {
                        this.ddlEntryType.SelectedValue = "2";
                        this.tdLblPo.Visible = false;
                        this.tdDdlPo.Visible = false;
                    }
                    else
                    {
                        this.ddlEntryType.SelectedValue = "1";
                        this.tdLblPo.Visible = true;
                        this.tdDdlPo.Visible = true;
                    }

                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int i = 0; i < ddlPono.Items.Count; i++)
                        {
                            if (myList[counter] == ddlPono.Items[i].Value)
                            {
                                ddlPono.Items[i].Selected = true;
                            }
                        }
                    }
                }
                


                 this.txtBillNumber.Text = dtEdit.Rows[0]["BILLNO"].ToString();
                 this.txtBilldate.Text = dtEdit.Rows[0]["BILLDATE"].ToString();
                 this.txtTotalvalue.Text = dtEdit.Rows[0]["TOTOALVALUE"].ToString();
                 this.txtRemarks.Text = dtEdit.Rows[0]["REMARKS"].ToString();
                 this.txtEntryDate.Text = dtEdit.Rows[0]["ENTRYDATE"].ToString();
                 pnlAdd.Style["display"] = "";
                 pnlDisplay.Style["display"] = "none";
                 if (ID == "Y")
                 {
                    this.save.Style["display"] = "none";
                 }
                 else
                 {
                    this.save.Style["display"] = "";
                 }
             }
             else
             {
                 MessageBox1.ShowError("No data Found");
                 return;
             }
        }
        catch(Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
        
    }

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        Button btn_views = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
        Label lblENTRYID = (Label)gvr.FindControl("lblENTRYID");
        this.Hdn_Fld.Value = lblENTRYID.Text.Trim();
        string mode = "status";
        ID = clsGetEntry.editGateEntryCheck(mode, this.Hdn_Fld.Value);
        if (ID == "Y")
        {
            MessageBox1.ShowWarning("GRN DONE DELTE NOT ALLOWED");
            return;
        }
        else
        {
            string deleteMode = "Delete";
            string delete = string.Empty;
            delete= clsGetEntry.editGateEntryCheck(deleteMode, this.Hdn_Fld.Value);
            if(delete=="Delete")
            {
                MessageBox1.ShowSuccess("Gate Entry Delete Done");
                this.loadGetEntry();
                return;
            }

        }
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

        if(this.ddlEntryType.SelectedValue=="1")
        {
            string vendorid = this.ddlVendor.SelectedValue;
            string mode = string.Empty;
            if (this.Hdn_Fld.Value == "")
            {
                mode = "AddPono";
            }
            else
            {
                mode = "EditPo";
            }
            this.loadPoNo(mode, vendorid);
        }
    }

    public void loadPoNo(string mode,string vendorid)
    {
        try
        {
           
             
            DataTable dt = new DataTable();
            dt = clsGetEntry.BindPoFromTpu(mode, vendorid);
            if (dt.Rows.Count > 0)
            {
                this.ddlPono.Items.Clear();
                this.ddlPono.AppendDataBoundItems = true;
                this.ddlPono.DataSource = dt;
                this.ddlPono.DataTextField = "PONO";
                this.ddlPono.DataValueField = "POID";
                this.ddlPono.DataBind();
            }
            else
            {
                this.ddlPono.Items.Clear();
                MessageBox1.ShowInfo("No Purchase Order Aviilabe");

            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }


    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            CalendarFromDate.StartDate = oDate;
            CalendarExtender1.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
            CalendarExtenderEntryDate.StartDate = oDate;


            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtBilldate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtEntryDate.Text = DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/');

                CalendarFromDate.EndDate = today1;
                CalendarExtender1.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
                CalendarExtenderEntryDate.EndDate = today1;

            }
            else
            {
                this.txtBilldate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtEntryDate.Text = new DateTime(endyear1, 03, 31).ToString("MM/dd/yyyy").Replace('-', '/');

                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;
                CalendarExtenderEntryDate.EndDate = cDate;

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string fromdate = string.Empty;
        string toDate = string.Empty;
        string status = string.Empty;
        fromdate = this.txtFromDate.Text;
        toDate = this.txtToDate.Text;
        status = this.ddlstatus.SelectedValue;
        this.searchGateEntry(fromdate, toDate, status);
    }

    public void searchGateEntry(string fromdate, string toDate, string status)
    {
        try
        {
            string mode = "SearchGateEntry";
            DataTable dataTable = new DataTable();
            dataTable = clsGetEntry.SearchGateEntry(mode, fromdate, toDate, status);
            if (dataTable.Rows.Count > 0)
            {
                grvGateEntry.DataSource = dataTable;
                grvGateEntry.DataBind();
            }
            else
            {
                grvGateEntry.DataSource = null;
                grvGateEntry.DataBind();
            }
        }
        catch(Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void ddlEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        { 
            if(ddlEntryType.SelectedValue=="2")
            {
                this.tdLblPo.Visible = false;
                this.tdDdlPo.Visible = false;
            }
            else
            {
                this.tdLblPo.Visible = true;
                this.tdDdlPo.Visible = true;
            }

        }
        catch(Exception ex)
        {

        }
    }
}