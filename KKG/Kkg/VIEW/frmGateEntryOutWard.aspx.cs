using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class VIEW_frmGateEntryOutWard : System.Web.UI.Page
{
    ClsVendor_TPU clsGateEntry = new ClsVendor_TPU();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlInvoiceNo').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.loadGetEntryOutWard();
                this.loadCustomer();
                this.DateLock();
            }

        }
        catch(Exception ex)
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
            CalendarExtender1.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;
            CalendarInvocieDate.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtInvocieDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
                CalendarInvocieDate.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtInvocieDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarExtender1.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
                CalendarInvocieDate.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion
    public void loadCustomer()
    {
        try
        {
            string mode = "LoadCustomer";
            dt = clsGateEntry.BindPoFromTpu(mode,"");
            if (dt.Rows.Count > 0)
            {
                this.ddlCustomer.Items.Clear();
                this.ddlCustomer.Items.Add(new ListItem("Select Customer", "0"));
                this.ddlCustomer.AppendDataBoundItems = true;
                this.ddlCustomer.DataSource = dt;
                this.ddlCustomer.DataTextField = "CUSTOMERNAME";
                this.ddlCustomer.DataValueField = "CUSTOMERID";
                this.ddlCustomer.DataBind();
            }
            else
            {
                this.ddlCustomer.Items.Clear();
                MessageBox1.ShowInfo("No Customer Aviilabe");

            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadInvoice()
    {
        try
        {
            string mode = "LoadInvoice";
            string customerid = this.ddlCustomer.SelectedValue;
            dt = clsGateEntry.BindPoFromTpu(mode, customerid);
            if (dt.Rows.Count > 0)
            {
                this.ddlInvoiceNo.Items.Clear();
                this.ddlInvoiceNo.AppendDataBoundItems = true;
                this.ddlInvoiceNo.DataSource = dt;
                this.ddlInvoiceNo.DataTextField = "SALEINVOICENO";
                this.ddlInvoiceNo.DataValueField = "SALEINVOICEID";
                this.ddlInvoiceNo.DataBind();
            }
            else
            {
                this.ddlInvoiceNo.Items.Clear();
                MessageBox1.ShowInfo("No Invoice Aviilabe");

            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadGetEntryOutWard()
    {
        try
        {
            string mode = "ShowOutWard";
            DataTable dataTable = new DataTable();
            dataTable = clsGateEntry.BindGateEntry(mode, HttpContext.Current.Session["UserID"].ToString());
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
        
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void Clearall()
    {
        try
        {
            this.Hdn_Fld.Value = "";
            this.ddlCustomer.SelectedValue = "0";
            this.ddlInvoiceNo.Items.Clear();
            this.txtTransporterName.Text = "";
            this.txtInvocieDate.Text = "";
            this.txtTotalvalue.Text = "0";
            this.txtRemarks.Text = "";
            this.txtEnrtyNumber.Text = "";
            this.txtVehicleNo.Text = "";
            this.save.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }


    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.loadInvoice();
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
            string customerId = string.Empty;
            string customerName = string.Empty;
            string transporterName = string.Empty;
            string vechileNo = string.Empty;
            string invId = string.Empty;
            string invNo = string.Empty;
            string InvDate = string.Empty;
            string reamrks = string.Empty;
            string ISEXISTS = string.Empty;
            string BILLMODE = string.Empty;
            decimal totalValue = 0;
            customerId = this.ddlCustomer.SelectedValue.ToString();
            customerName = this.ddlCustomer.SelectedItem.ToString();
            if (customerId == "0")
            {
                MessageBox1.ShowError("Please Select Customer");
                return;
            }
            var query = from ListItem item in ddlInvoiceNo.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                invId += item.Value + ',';
                invNo += item.Text + ',';
            }
            invId = invId.Substring(0, invId.Length - 1);
            invNo = invNo.Substring(0, invNo.Length - 1);


            if (invId == "0")
            {
                MessageBox1.ShowError("Please Select Invoice");
                return;
            }

            vechileNo = this.txtVehicleNo.Text;
            InvDate = this.txtInvocieDate.Text;
            reamrks = this.txtRemarks.Text;
            transporterName = this.txtTransporterName.Text;
            totalValue = Convert.ToDecimal(this.txtTotalvalue.Text);

            if (totalValue == 0)
            {
                MessageBox1.ShowError("Value cannot be zero");
                return;
            }
            getEntryId = this.Hdn_Fld.Value;
            if (getEntryId == "")
            {
                mode = "I";
            }
            else
            {
                mode = "U";
            }
            DataTable dtSave = new DataTable();
            dtSave = clsGateEntry.SaveGaetEntryOutWard(mode, getEntryId, customerId, customerName, invId, invNo, InvDate, transporterName, vechileNo,totalValue, reamrks);
            if (dtSave.Rows[0]["MESSAGE"].ToString() == "New")
            {
                MessageBox1.ShowSuccess("Entry Sucesfull");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.loadGetEntryOutWard();
                this.trBtn.Visible = true;
                return;
            }
            else if (dtSave.Rows[0]["MESSAGE"].ToString() == "Update")
            {
                MessageBox1.ShowSuccess("Update Sucesfull");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.loadGetEntryOutWard();
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
            this.loadGetEntryOutWard();
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.Clearall();
        pnlAdd.Style["display"] = "";
        pnlDisplay.Style["display"] = "none";
        this.trBtn.Visible = false;
        this.entryNumber.Visible = false;
        this.txtInvocieDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        Button btn_views = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
        Label lblENTRYID = (Label)gvr.FindControl("lblENTRYID");
        this.Hdn_Fld.Value = lblENTRYID.Text.Trim();
        string mode = "status";
        //ID = clsGetEntry.editGateEntryCheck(mode, this.Hdn_Fld.Value);
        //if (ID == "Y")
        //{
        //    MessageBox1.ShowWarning("GRN DONE DELTE NOT ALLOWED");
        //    return;
        //}
        //else
        //{
            string deleteMode = "Deleteoutward";
            string delete = string.Empty;
            delete = clsGateEntry.editGateEntryCheck(deleteMode, this.Hdn_Fld.Value);
            if (delete == "Delete")
            {
                MessageBox1.ShowSuccess("Gate Entry Delete Done");
                this.loadGetEntryOutWard();
                return;
            }

        //}
    }

    protected void btnEntryedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.entryNumber.Visible = true;
            string mode = "outwardstatus";
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblENTRYID = (Label)gvr.FindControl("lblENTRYID");
            this.Hdn_Fld.Value = lblENTRYID.Text.Trim();
            string ID = "";
            string Pomode = "";
            Pomode = "EditPo";
            ID = clsGateEntry.editGateEntryCheck(mode, this.Hdn_Fld.Value);

            DataTable dtEdit = new DataTable();
            dtEdit = clsGateEntry.editGateEntryOutWard(this.Hdn_Fld.Value);
            if (dtEdit.Rows.Count > 0)
            {
                this.txtEnrtyNumber.Text = dtEdit.Rows[0]["ENTNUMBER"].ToString();
                this.ddlCustomer.SelectedValue = dtEdit.Rows[0]["CUSTOMERID"].ToString();
                this.loadInvoice();

                var myList = new List<string>(dtEdit.Rows[0]["INVOICEID"].ToString().Split(','));
                if (dtEdit.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int i = 0; i < ddlInvoiceNo.Items.Count; i++)
                        {
                            if (myList[counter] == ddlInvoiceNo.Items[i].Value)
                            {
                                ddlInvoiceNo.Items[i].Selected = true;
                            }
                        }
                    }
                }


                this.txtVehicleNo.Text = dtEdit.Rows[0]["VECHILENO"].ToString();
                this.txtTransporterName.Text = dtEdit.Rows[0]["TRANSPOTERNAME"].ToString();
                this.txtTotalvalue.Text = dtEdit.Rows[0]["TOTOALVALUE"].ToString();
                this.txtInvocieDate.Text = dtEdit.Rows[0]["INVOICEDATE"].ToString();
                this.txtRemarks.Text = dtEdit.Rows[0]["REMARKS"].ToString();
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
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void grvGateEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}