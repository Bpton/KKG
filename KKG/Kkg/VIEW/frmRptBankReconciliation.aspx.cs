#region Namespace
using Account;
using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class VIEW_frmRptBankReconciliation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlVouchers').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                /*ClsStockReport clsstartfy = new ClsStockReport();
                string STARTDATE = clsstartfy.GetStartDateOfFinYear(Session["FINYEAR"].ToString());
                DateTime oDate = Convert.ToDateTime(STARTDATE);
                DateTime cDate = oDate.AddSeconds(31535999);

                CalendarExtender2.StartDate = oDate;
                CalendarExtender2.EndDate = cDate;

                CalendarExtender3.EndDate = cDate;
                CalendarExtender3.StartDate = oDate;*/

                this.pnlDisplay.Style["display"] = "";
                this.LoadLedgeraccount();
                this.LoadVouchers();
                this.LoadRegion(this.Session["UTNAME"].ToString().ToLower().Trim(), this.Session["USERID"].ToString().Trim());
                this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.grdledger.DataSource = null;
                this.grdledger.DataBind();

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    #region LoadLedgeraccount
    public void LoadLedgeraccount()
    {
        try
        {
            DataTable dt = new DataTable();
            ClsRptAccount clsrptacc = new ClsRptAccount();
            dt = clsrptacc.BindLedgeraccount();
            if (dt.Rows.Count > 0)
            {
                ddlledgeraccount.Items.Clear();
                ddlledgeraccount.Items.Insert(0, new ListItem("Select", "0"));
                ddlledgeraccount.AppendDataBoundItems = true;
                ddlledgeraccount.DataSource = dt;
                ddlledgeraccount.DataTextField = "BANKNAME";
                ddlledgeraccount.DataValueField = "REFERENCE_LEDGERID";
                ddlledgeraccount.DataBind();
            }
            else
            {
                ddlledgeraccount.Items.Clear();
                ddlledgeraccount.Items.Insert(0, new ListItem("Select", "0"));
                ddlledgeraccount.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadVouchers
    public void LoadVouchers()
    {
        try
        {
            DataTable dt = new DataTable();
            ClsRptAccount clsrptacc = new ClsRptAccount();
            dt = clsrptacc.BindVoucherType();
            if (dt.Rows.Count > 0)
            {
                ddlVouchers.Items.Clear();
                ddlVouchers.AppendDataBoundItems = true;
                ddlVouchers.Items.Insert(0, new ListItem("ALL", "-1"));
                ddlVouchers.DataSource = dt;
                ddlVouchers.DataTextField = "NAME";
                ddlVouchers.DataValueField = "ID";
                ddlVouchers.DataBind();
            }
            else
            {
                //ddlledgeraccount.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadRegion
    public void LoadRegion(string UserTypeid, string UserID)
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataTable depot = new DataTable();
            depot = clsrpt.Region_foraccounts(UserTypeid, UserID);

            if (depot.Rows.Count > 0)
            {
                this.ddldepot.AppendDataBoundItems = true;
                this.ddldepot.DataSource = depot;
                this.ddldepot.DataTextField = "BRNAME";
                this.ddldepot.DataValueField = "BRID";
                this.ddldepot.DataBind();
            }
            else
            {
                this.ddldepot.Items.Clear();
                this.ddldepot.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    private void BindGrid(string SearchExpression, string Search)
    {
        try
        {
            ClsRptAccount clsrptacc = new ClsRptAccount();
            DataSet dt = new DataSet();
            this.grdledger.DataSource = null;
            this.grdledger.DataBind();
            string finyearid = clsrptacc.GetFinYearID(HttpContext.Current.Session["FINYEAR"].ToString().Trim());
            dt = clsrptacc.BindBankReport(txtfromdate.Text.Trim(), txttodate.Text.Trim(), ddlledgeraccount.SelectedValue.Trim(), finyearid, Convert.ToInt32(this.ddlVouchers.SelectedValue.Trim()), this.ddlReconciledVouchers.SelectedValue.Trim(), this.ddldepot.SelectedValue.Trim(), SearchExpression, Search);
            if (dt.Tables[0].Rows.Count > 0)
            {
                this.grdledger.DataSource = dt.Tables[0];
                this.grdledger.DataBind();

                if (ddlNarrations.SelectedValue.Trim() == "1")//--NO
                {
                    grdledger.Columns[14].Visible = false;
                }
                else
                {
                    grdledger.Columns[14].Visible = true;
                }
            }
            else
            {
                this.grdledger.DataSource = null;
                this.grdledger.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";

        }
    }

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("AccEntryID", typeof(string)));
        dt.Columns.Add(new DataColumn("LedgerId", typeof(string)));
        dt.Columns.Add(new DataColumn("ChequeRealisedDate", typeof(string)));
        dt.Columns.Add(new DataColumn("ChequeNo", typeof(string)));   /* Add column for cheque No by D.Mondal on 22/10/2018 */

        HttpContext.Current.Session["BANKDETAILS"] = dt;


    }
    #endregion



    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchExpression = null;
            string Search = "N";
            this.BindGrid(SearchExpression, Search);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void searchButton_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlsearch.SelectedValue == "0")
            {
                MessageBox1.ShowError("Please Select Region Of Search.");
                return;
            }
            else
            {
                if (txtsearchBox.Text == "")
                {
                    MessageBox1.ShowError("Please Type Search Value.");
                    return;
                }
                else
                {
                    if (ddlsearch.SelectedValue == "5")
                    {
                        if (ddlNarrations.SelectedValue.Trim() == "2")
                        {
                            string Search = "5";
                            string SearchExpression = null;


                            if (!String.IsNullOrEmpty(txtsearchBox.Text))
                            {

                                SearchExpression = txtsearchBox.Text;
                            }

                            BindGrid(SearchExpression, Search);
                        }
                        else
                        {
                            this.grdledger.DataSource = null;
                            this.grdledger.DataBind();
                        }
                    }
                    else if (ddlsearch.SelectedValue == "1")
                    {
                        string Search = "1";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }
                        BindGrid(SearchExpression, Search);
                    }

                    else if (ddlsearch.SelectedValue == "2")
                    {
                        string Search = "2";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }

                        BindGrid(SearchExpression, Search);



                    }
                    else if (ddlsearch.SelectedValue == "3")
                    {
                        string Search = "3";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }

                        BindGrid(SearchExpression, Search);



                    }
                    else if (ddlsearch.SelectedValue == "4")
                    {
                        string Search = "4";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }

                        BindGrid(SearchExpression, Search);



                    }
                    else if (ddlsearch.SelectedValue == "6")
                    {
                        string Search = "6";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }

                        BindGrid(SearchExpression, Search);



                    }
                    else if (ddlsearch.SelectedValue == "7")
                    {
                        string Search = "7";
                        string SearchExpression = null;


                        if (!String.IsNullOrEmpty(txtsearchBox.Text))
                        {

                            SearchExpression = txtsearchBox.Text;
                        }

                        BindGrid(SearchExpression, Search);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string bankdate = string.Empty;
            string lblAccid = string.Empty;
            string lblledgerid = string.Empty;
            string XML = string.Empty;
            ClsRptAccount clsrptacc = new ClsRptAccount();
            int id = 0;
            HttpContext.Current.Session["BANKDETAILS"] = null;

            if (HttpContext.Current.Session["BANKDETAILS"] == null)
            {
                CreateDataTable();
            }
            DataTable DT = (DataTable)HttpContext.Current.Session["BANKDETAILS"];

            foreach (GridViewRow gvrow in grdledger.Rows)
            {
                Label lblAccidname = (Label)gvrow.FindControl("lblLedgerName");
                if (lblAccidname.Text != "Opening Balance")
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
                    Label lblAccid1 = (Label)gvrow.FindControl("lblAcc");
                    //Label lblAccidname = (Label)gvrow.FindControl("lblLedgerName");
                    TextBox grdtxtbankdate = (TextBox)gvrow.FindControl("grdtxtbankdate");
                    Label lblChequeNo = (Label)gvrow.FindControl("lbl791");  /* Add column for cheque No by D.Mondal on 22/10/2018 */


                    lblAccid = lblAccid1.Text.Trim();

                    if (chkBx.Checked)
                    {
                        bankdate = grdtxtbankdate.Text.Trim();
                        if (bankdate.Trim() != "")
                        {
                            bankdate = grdtxtbankdate.Text.Trim();
                        }
                        else
                        {
                            bankdate = "01/01/1900";
                        }
                        if (bankdate != "")
                        {
                            count = count + 1;
                            DataRow dr = DT.NewRow();
                            dr["AccEntryID"] = lblAccid1.Text.Trim();
                            dr["LedgerId"] = this.ddlledgeraccount.SelectedValue.Trim();
                            dr["ChequeRealisedDate"] = bankdate.Trim();
                            dr["ChequeNo"] = lblChequeNo.Text.Trim();  /* Add column for cheque No by D.Mondal on 22/10/2018 */
                            DT.Rows.Add(dr);
                            DT.AcceptChanges();
                        }
                    }
                }
            }
            HttpContext.Current.Session["BANKDETAILS"] = DT;

            if (count > 0)
            {
                XML = ConvertDatatable(DT);
                id = clsrptacc.SaveBankConcilation(XML);
            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Error Saving Record ");
            }
            else
            {
                if (id > 0)
                {
                    MessageBox1.ShowSuccess("Record saved successfully..");
                    this.grdledger.DataSource = null;
                    this.grdledger.DataBind();
                    HttpContext.Current.Session["BANKDETAILS"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Session["BANKDETAILS"] = null;
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}