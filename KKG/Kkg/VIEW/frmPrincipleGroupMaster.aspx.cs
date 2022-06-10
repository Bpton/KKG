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

public partial class VIEW_frmPrincipleGroupMaster : System.Web.UI.Page
{
    ClsPrincipleGroupMaster ClscatMaster = new ClsPrincipleGroupMaster();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                plnProductMapping.Style["display"] = "none";
                LoadCatagoryMaster();
                LoadBusinessSegment();
                LoadDivision();
                LoadCategory();
                HttpContext.Current.Session["PRODUCTGROUP"] = null;
                this.LoadCurrency();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    public void LoadCatagoryMaster()
    {
        try
        {
            gvCATAGORY.DataSource = ClscatMaster.BindCatagoryMastergrid();
            gvCATAGORY.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }

    public void LoadBusinessSegment()
    {
        try
        {
            ddlbusinesssegment.Items.Clear();
            ddlbusinesssegment.Items.Insert(0, new ListItem("--Select Business Segment--", "0"));
            ddlbusinesssegment.DataSource = ClscatMaster.BindBusinessSegment();
            ddlbusinesssegment.DataTextField = "BSNAME";
            ddlbusinesssegment.DataValueField = "BSID";
            ddlbusinesssegment.DataBind();


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }


    }
    public void LoadCurrency()
    {

        try
        {
            ClsPrincipleGroupMaster ClscatMaster = new ClsPrincipleGroupMaster();
            DataTable dt = ClscatMaster.BindCurrency();
            if (dt.Rows.Count > 0)
            {
                this.ddlcurency.Items.Clear();
                this.ddlcurency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
                this.ddlcurency.DataSource = dt;
                this.ddlcurency.DataTextField = "CURRENCYNAME";
                this.ddlcurency.DataValueField = "CURRENCYID";
                this.ddlcurency.DataBind();
            }


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnAddCatagory_Click(object sender, EventArgs e)
    {
        try
        {
            txtcatcode.Text = "";
            txtcatname.Text = "";
            txtDescription.Text = "";
            ddlbusinesssegment.SelectedValue = "0";
            chkActive.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            this.ddlcurency.SelectedValue = "0";
            this.ddlcurency.Enabled = true;
            this.chktransfertoho.Checked = false;
            trledger.Style["display"] = "none";
            this.chkchain.Checked = false;
            this.chkclaimchain.Checked = false;
            ddlledger.SelectedValue = "0";

        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadLedger()
    {
        try
        {
            ClsPrincipleGroupMaster ClscatMaster = new ClsPrincipleGroupMaster();
            DataTable dt = ClscatMaster.BindLedger();

            if (dt.Rows.Count > 0)
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
                this.ddlledger.DataSource = dt;
                this.ddlledger.DataTextField = "NAME";
                this.ddlledger.DataValueField = "ID";
                this.ddlledger.DataBind();
            }
            else
            {
                this.ddlledger.Items.Clear();
                this.ddlledger.Items.Insert(0, new ListItem("Select Ledger", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void chktransfertoho_CheckedChanged(object sender, EventArgs e)
    {
        if (chktransfertoho.Checked)
        {

            this.LoadLedger();

            trledger.Style["display"] = " ";

        }
        else
        {
            trledger.Style["display"] = "none";
            this.ddlledger.SelectedValue = "0";
        }
    }
    protected void btnCATsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string chkActiveval = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
            }
            else
            {
                ViewState["mode"] = "U";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;
            if (chkActive.Checked == true)
            {
                chkActiveval = "T";
            }
            else
            {
                chkActiveval = "F";
            }
            string transfertoho = string.Empty;
            if (chktransfertoho.Checked)
            {
                transfertoho = "Y";
                if (ddlledger.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please select Ledger");
                    return;
                }

            }
            else
            {
                transfertoho = "N";
            }

            string chain = string.Empty;
            if (chkchain.Checked)
            {
                chain = "Y";
            }
            else
            {
                chain = "N";
            }
            string claimchain = string.Empty;
            if (chkclaimchain.Checked)
            {
                claimchain = "Y";
            }
            else
            {
                claimchain = "N";
            }





            ID = ClscatMaster.SaveCatagoryMaster(Hdn_Fld.Value, txtcatcode.Text.ToString(), txtcatname.Text.ToString(),
                txtDescription.Text.ToString(), ddlbusinesssegment.SelectedValue.ToString(), ddlbusinesssegment.SelectedItem.Text.ToString(),
                Mode, chkActiveval, this.ddlcurency.SelectedValue.Trim(), this.ddlcurency.SelectedItem.ToString().Trim(), transfertoho.Trim(), chain.Trim(),
                claimchain.Trim(), ddlledger.SelectedValue.Trim());

            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadCatagoryMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                this.chkchain.Checked = false;
                this.chkclaimchain.Checked = false;


            }
            else if (ID == 2)
            {
                //string message = "alert('Code already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("</b> Code already exist..");
            }
            else if (ID == 3)
            {
                //string message = "alert('Name already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("</b> Name already exist..");
            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCATcancel_click(object sender, EventArgs e)
    {
        try
        {
            this.ddlcurency.Enabled = true;
            txtcatcode.Text = "";
            txtcatname.Text = "";
            txtDescription.Text = "";
            ddlbusinesssegment.SelectedValue = "0";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadCatagoryMaster();

            this.chktransfertoho.Checked = false;
            trledger.Style["display"] = "none";
            this.chkchain.Checked = false;
            this.chkclaimchain.Checked = false;

        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (this.HdnPredined.Value.Trim() == "Y")
            {
                e.Record["Error"] = "Principle Group can't be deleted. ";
            }
            else
            {

                if (e.Record["DIS_CATID"] != "")
                {
                    int ID = 0;
                    ID = ClscatMaster.DeleteCatagoryMaster(e.Record["DIS_CATID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";
                        LoadCatagoryMaster();
                    }
                    else
                    {
                        e.Record["Error"] = "Error On Deleting. ";
                    }
                }
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }



    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClscatMaster.BindEdidGridByid(userid);
            if (dt.Rows.Count > 0)
            {
                txtcatcode.Text = dt.Rows[0]["DIS_CATCODE"].ToString();
                txtcatname.Text = dt.Rows[0]["DIS_CATNAME"].ToString();
                txtDescription.Text = dt.Rows[0]["DIS_CATDESCRIPTION"].ToString();
                ddlbusinesssegment.SelectedValue = dt.Rows[0]["BUSINESSSEGMENTID"].ToString();
                if (dt.Rows[0]["ACTIVE"].ToString() == "T")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                this.LoadCurrency();
                if (Convert.ToString(dt.Rows[0]["CURRENCYID"]).Trim() != "")
                {
                    this.ddlcurency.SelectedValue = Convert.ToString(dt.Rows[0]["CURRENCYID"]).Trim();
                }
                else
                {
                    this.ddlcurency.SelectedValue = "0";
                }

                if (dt.Rows[0]["ISFINANCE_HO"].ToString() == "Y")
                {
                    chktransfertoho.Checked = true;
                }
                else
                {
                    chktransfertoho.Checked = false;
                }

                if (chktransfertoho.Checked)
                {
                    trledger.Style["display"] = " ";
                    this.LoadLedger();

                    this.ddlledger.SelectedValue = dt.Rows[0]["REFERENCELEDGERID_TOHO"].ToString();

                }
                else
                {
                    trledger.Style["display"] = "none";

                }
                if (dt.Rows[0]["ISCHAIN"].ToString() == "Y")
                {
                    chkchain.Checked = true;
                }
                else
                {
                    chkchain.Checked = false;
                }
                if (dt.Rows[0]["ISCLAIMCHAIN"].ToString() == "Y")
                {
                    chkclaimchain.Checked = true;
                }
                else
                {
                    chkclaimchain.Checked = false;
                }


            }
            this.ddlcurency.Enabled = false;
            Hdn_Fld.Value = Convert.ToString(userid);
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void gvCATAGORY_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
                GridDataControlFieldCell cell1 = e.Row.Cells[8] as GridDataControlFieldCell;
                GridDataControlFieldCell cell2 = e.Row.Cells[9] as GridDataControlFieldCell;
                string status = cell.Text;
                string transfertoho = cell1.Text;
                string tranischain = cell2.Text;

                if (status == "Active")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
                if (transfertoho == "Yes")
                {
                    cell1.ForeColor = Color.Green;
                }
                else
                {
                    cell1.ForeColor = Color.Red;
                }
                if (tranischain == "Yes")
                {
                    cell2.ForeColor = Color.Green;
                }
                else
                {
                    cell2.ForeColor = Color.Red;
                }

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    #region Product Mapping
    //==============================Product Mapping Add by Subhodip De on 10.04.2016==========================//


    #region LoadDivision
    public void LoadDivision()
    {
        ClsPrincipleGroupMaster ClsPGMaster = new ClsPrincipleGroupMaster();
        ddldivision.Items.Clear();
        ddldivision.Items.Insert(0, new ListItem("-Select Brand-", "0"));
        ddldivision.DataSource = ClsPGMaster.BindDivision();
        ddldivision.DataTextField = "DIVNAME";
        ddldivision.DataValueField = "DIVID";
        ddldivision.DataBind();
    }
    #endregion

    #region LoadCategory
    public void LoadCategory()
    {
        ClsPrincipleGroupMaster ClsPGMaster = new ClsPrincipleGroupMaster();
        ddlCategory.Items.Clear();
        ddlCategory.Items.Insert(0, new ListItem("-Select Category-", "0"));
        ddlCategory.DataSource = ClsPGMaster.BindCategory();
        ddlCategory.DataTextField = "CATNAME";
        ddlCategory.DataValueField = "CATID";
        ddlCategory.DataBind();
    }
    #endregion

    #region LoadProduct
    public void LoadProduct(string catid, string divid)
    {
        ClsPrincipleGroupMaster ClsPGMaster = new ClsPrincipleGroupMaster();
        DataTable dt = new DataTable();
        dt = ClsPGMaster.BindGridProduct(catid, divid);
        if (dt.Rows.Count > 0)
        {
            gvproductmapping.DataSource = dt;
            gvproductmapping.DataBind();
        }
        else
        {
            gvproductmapping.DataSource = null;
            gvproductmapping.DataBind();
        }
    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldivision.SelectedValue != "0")
            {
                LoadProduct(this.ddlCategory.SelectedValue.Trim(), this.ddldivision.SelectedValue.Trim());
            }
            else
            {
                MessageBox1.ShowInfo(" Please Select Brand");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region btnProductMapping_Click
    protected void btnProductMapping_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPrincipleGroupMaster ClsPGMaster = new ClsPrincipleGroupMaster();
            CreateDataTable();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            plnProductMapping.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            ddldivision.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
            gvproductadd.ClearPreviousDataSource();
            gvproductadd.DataSource = null;
            gvproductadd.DataBind();
            gvproductmapping.ClearPreviousDataSource();
            gvproductmapping.DataSource = null;
            gvproductmapping.DataBind();

            DataTable dtadd = (DataTable)HttpContext.Current.Session["PRODUCTGROUP"];

            DataTable dt = new DataTable();
            dt = ClsPGMaster.Productmapping(hdnBSid.Value, Hdn_Fld.Value);


            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtadd.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["BSID"] = Convert.ToString(dt.Rows[i]["BSID"]).Trim();
                    dr["BSNAME"] = Convert.ToString(dt.Rows[i]["BSNAME"]).Trim();
                    dr["GROUPID"] = Convert.ToString(dt.Rows[i]["GROUPID"]).Trim();
                    dr["GROUPNAME"] = Convert.ToString(dt.Rows[i]["GROUPNAME"]).Trim();
                    dr["BRANDID"] = Convert.ToString(dt.Rows[i]["BRANDID"]).Trim();
                    dr["BRANDNAME"] = Convert.ToString(dt.Rows[i]["BRANDNAME"]).Trim();
                    dr["CATEGORYID"] = Convert.ToString(dt.Rows[i]["CATEGORYID"]).Trim();
                    dr["CATEGORYNAME"] = Convert.ToString(dt.Rows[i]["CATEGORYNAME"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(dt.Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(dt.Rows[i]["PRODUCTNAME"]).Trim();
                    dtadd.Rows.Add(dr);
                    dtadd.AcceptChanges();
                }
                HttpContext.Current.Session["PRODUCTGROUP"] = dtadd;
                gvproductadd.DataSource = dtadd;
                gvproductadd.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("<b>No records found!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }



    }
    #endregion

    #region btnproductmappingcancel_Click
    protected void btnproductmappingcancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            plnProductMapping.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("BSID", typeof(string)));
        dt.Columns.Add(new DataColumn("BSNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPID", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANDNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));


        HttpContext.Current.Session["PRODUCTGROUP"] = dt;
        return dt;
    }
    #endregion

    #region ADD Btn
    protected void btnproductadd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            int Count = 0;
            int numberOfRecords = 0;
            bool FLAG = false;
            int state = 0;
            string PRODUCTID = string.Empty;
            string PRODUCTNAME = string.Empty;
            if (HttpContext.Current.Session["PRODUCTGROUP"] != null)
            {
                dt = (DataTable)HttpContext.Current.Session["PRODUCTGROUP"];
            }

            #region Commented
            //if (gvproductmapping.TotalRowCount > 0)
            //{
            //    for (int i = 0; i < gvproductmapping.RowsInViewState.Count; i++)
            //    {
            //        GridDataControlFieldCell cell = gvproductmapping.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;
            //        CheckBox chk = cell.FindControl("ChkID") as CheckBox;
            //        HiddenField hdnproduct = cell.FindControl("ProductName") as HiddenField;
            //        if (chk.Checked == true)
            //        {

            //            numberOfRecords = dt.Select("PRODUCTID = '" + chk.ToolTip + "' AND BRANDID = '" + ddldivision.SelectedValue + "' AND CATEGORYID = '" + ddlCategory.SelectedValue + "'  ").Length;
            //            if (numberOfRecords > 0)
            //            {
            //                MessageBox1.ShowError("<b><font color='red'> '" + hdnproduct.Value + "' Product already exists..!</font></b>",60,790);
            //            }
            //            else
            //            {
            //                Count = Count + 1;
            //                DataRow dr = dt.NewRow();
            //                dr["GUID"] = Guid.NewGuid();
            //                dr["BSID"] = Convert.ToString(hdnBSid.Value).Trim();
            //                dr["BSNAME"] = Convert.ToString(this.txtBSname.Text).Trim();
            //                dr["GROUPID"] = Convert.ToString(Hdn_Fld.Value).Trim();
            //                dr["GROUPNAME"] = Convert.ToString(txtgroup.Text).Trim();
            //                dr["BRANDID"] = Convert.ToString(this.ddldivision.SelectedValue).Trim(); ;
            //                dr["BRANDNAME"] = Convert.ToString(this.ddldivision.SelectedItem.Text).Trim();
            //                dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue).Trim();
            //                dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem.Text).Trim();
            //                dr["PRODUCTID"] = chk.ToolTip;
            //                dr["PRODUCTNAME"] = hdnproduct.Value;
            //                dt.Rows.Add(dr);
            //                dt.AcceptChanges();
            //            }
            //        }

            //    }
            //    if (Count == 0 && numberOfRecords == 0)
            //    {
            //        MessageBox1.ShowInfo("Please select 1 product ");
            //    }

            //    Session["PRODUCTGROUP"] = dt;
            //    if (dt.Rows.Count > 0)
            //    {
            //        gvproductadd.DataSource = dt;
            //        gvproductadd.DataBind();
            //        gvproductmapping.DataSource = null;
            //        gvproductmapping.DataBind();
            //        ddldivision.SelectedValue = "0";
            //        ddlCategory.SelectedValue = "0";

            //    }

            //    else
            //    {
            //        gvproductadd.DataSource = null;
            //        gvproductadd.DataBind();
            //    }

            //}
            #endregion
            if (dt.Rows.Count == 0)
            {
                if (gvproductmapping.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvproductmapping.SelectedRecords)
                    {
                        Count = Count + 1;
                        DataRow dr = dt.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["BSID"] = Convert.ToString(hdnBSid.Value).Trim();
                        dr["BSNAME"] = Convert.ToString(this.txtBSname.Text).Trim();
                        dr["GROUPID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                        dr["GROUPNAME"] = Convert.ToString(txtgroup.Text).Trim();
                        dr["BRANDID"] = Convert.ToString(this.ddldivision.SelectedValue).Trim(); ;
                        dr["BRANDNAME"] = Convert.ToString(this.ddldivision.SelectedItem.Text).Trim();
                        dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue).Trim();
                        dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem.Text).Trim();
                        dr["PRODUCTID"] = Convert.ToString(row["ID"]); ;
                        dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }
                }


                if (Count == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast 1 Product..");
                }
                else
                {
                    ddldivision.SelectedValue = "0";
                    ddlCategory.SelectedValue = "0";
                    this.gvproductmapping.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvproductmapping.ClearPreviousDataSource();
                    this.gvproductmapping.DataSource = null;
                    this.gvproductmapping.DataBind();
                }
            }
            else if (dt.Rows.Count > 0)
            {
                FLAG = false;
                PRODUCTNAME = "";
                if (gvproductmapping.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvproductmapping.SelectedRecords)
                    {
                        for (int k = 0; k < gvproductadd.Rows.Count; k++)
                        {
                            PRODUCTID = gvproductadd.Rows[k].Cells[10].Text.ToString();
                            Count = Count + 1;
                            if (PRODUCTID == Convert.ToString(row["ID"]))
                            {
                                FLAG = true;
                                state = 2;
                                PRODUCTNAME = PRODUCTNAME + "," + Convert.ToString(row["NAME"]);
                                break;

                            }
                        }
                        if (FLAG == false)
                        {
                            DataRow dr = dt.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["BSID"] = Convert.ToString(hdnBSid.Value).Trim();
                            dr["BSNAME"] = Convert.ToString(this.txtBSname.Text).Trim();
                            dr["GROUPID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                            dr["GROUPNAME"] = Convert.ToString(txtgroup.Text).Trim();
                            dr["BRANDID"] = Convert.ToString(this.ddldivision.SelectedValue).Trim(); ;
                            dr["BRANDNAME"] = Convert.ToString(this.ddldivision.SelectedItem.Text).Trim();
                            dr["CATEGORYID"] = Convert.ToString(this.ddlCategory.SelectedValue).Trim();
                            dr["CATEGORYNAME"] = Convert.ToString(this.ddlCategory.SelectedItem.Text).Trim();
                            dr["PRODUCTID"] = Convert.ToString(row["ID"]); ;
                            dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                }

                ddldivision.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                this.gvproductmapping.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvproductmapping.ClearPreviousDataSource();
                this.gvproductmapping.DataSource = null;
                this.gvproductmapping.DataBind();
                if (Count == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast 1 Product..");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            PRODUCTNAME = PRODUCTNAME.Substring(0, PRODUCTNAME.Length - 1);
                            MessageBox1.ShowInfo("" + PRODUCTNAME + " already exists", 60, 750);
                        }

                    }
                    else
                    {
                        ddldivision.SelectedValue = "0";
                        ddlCategory.SelectedValue = "0";
                        this.gvproductmapping.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                        this.gvproductmapping.ClearPreviousDataSource();
                        this.gvproductmapping.DataSource = null;
                        this.gvproductmapping.DataBind();
                    }
                }
            }

            Session["PRODUCTGROUP"] = dt;
            if (dt.Rows.Count > 0)
            {
                this.gvproductadd.DataSource = dt;
                this.gvproductadd.DataBind();
                this.gvproductmapping.DataSource = null;
                this.gvproductmapping.DataBind();
                this.ddldivision.SelectedValue = "0";
                this.ddlCategory.SelectedValue = "0";

            }

            else
            {
                this.gvproductadd.DataSource = null;
                this.gvproductadd.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }



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

    #region Save_Click
    protected void btnproductmappingsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPrincipleGroupMaster ClsPGMaster = new ClsPrincipleGroupMaster();
            int ID = 0;
            string xml = string.Empty;
            DataTable dtproductcheckrecord = (DataTable)HttpContext.Current.Session["PRODUCTGROUP"];
            //if (dtproductcheckrecord.Rows.Count > 0)
            //{
            xml = ConvertDatatableToXML(dtproductcheckrecord);

            ID = ClsPGMaster.Saveproductmapping(Hdn_Fld.Value, xml);

            if (ID > 0)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadCatagoryMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                plnProductMapping.Style["display"] = "none";
                btnaddhide.Style["display"] = "";
                ddldivision.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                Hdn_Fld.Value = "";
                gvproductmapping.ClearPreviousDataSource();

                gvproductmapping.DataSource = null;
                gvproductmapping.DataBind();

            }
            else
            {
                MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
            }
            // }
            //else
            //{
            //    MessageBox1.ShowError("<b><font color='red'>Plese select 1 product and save ....</font></b>");
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion


    # region ProductDelete
    protected void btnproductdelete_Click(object sender, EventArgs e)
    {
        try
        {

            string productGUID = Convert.ToString(hdnProductDelete.Value);
            DataTable dtdeleteProductrecord = new DataTable();
            dtdeleteProductrecord = (DataTable)Session["PRODUCTGROUP"];

            DataRow[] drr = dtdeleteProductrecord.Select("GUID='" + productGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteProductrecord.AcceptChanges();
            }
            this.gvproductadd.DataSource = dtdeleteProductrecord;
            this.gvproductadd.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #endregion
}